/* 
Created By              :    Aung Kyaw
Created Date          :   16/07/2014
Updated By             :Aye Aye Mon
Updated Date         :   11/05/2015

 Tables using: 
    -Item_ExportQ(Get File_Name,File_Type/Set IsExport)
    -Shop(Get FTP_Host, FTP_Account, FTP_Password)
    -Code_Setup(Get Code_Description)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.OleDb;
using System.Globalization;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using Ionic.Zip;
using System.IO.Compression;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Net.Mail;
using Limilabs.FTP.Client;

namespace Yahoo_CSV_Upload_Console
{
    public class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //@"C:\My Data\Projects\ORS_RCM\ORS_RCM\Export_Promotion_CSV\";
        static string promotionFilePath = ConfigurationManager.AppSettings["PromotionFilePath"].ToString();
        //@"C:\My Data\Projects\ORS_RCM\ORS_RCM\Export_CSV\";
        static string itemFilePath = ConfigurationManager.AppSettings["ItemFilePath"].ToString();
        static string inventoryFilePath = ConfigurationManager.AppSettings["InventoryFilePath"].ToString();
        //@"C:\Item_Image_Rakutan\";
        static string extractPath = ConfigurationManager.AppSettings["ExtractFilePath"].ToString();
        //@"C:\ConsoleWriteLineTofile\";
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Yahoo Csv Upload";
                //Select From Item_ExportQ
                DataTable dtItem = GetFiles(1); // Select For Item( Export_Type=1 )
                foreach (DataRow dr in dtItem.Rows)
                {
                    //ConsoleWriteLine_Tofile(dr["File_Name"].ToString());
                    //PrepareFiles(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), (string)dr["File_Name"], (int)dr["ID"], filePath, int.Parse(dr["Mall_ID"].ToString()), int.Parse(dr["ShopID"].ToString()));
                    FileToFTP(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), (string)dr["File_Name"], (int)dr["ID"], itemFilePath, (int)dr["ShopID"]);
                    ChangeStatus((int)dr["ID"]);
                    UpdateItemShopExportedDate(int.Parse(dr["ShopID"].ToString()), (string)dr["File_Name"]);
                }

                DataTable dtInventory = GetFiles(3); // Select For Inventory( Export_Type=3 )
                foreach (DataRow dr in dtInventory.Rows)
                {
                    FileToFTP(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), (string)dr["File_Name"], (int)dr["ID"], inventoryFilePath, (int)dr["ShopID"]);
                    ChangeStatus((int)dr["ID"]);
                    //UpdateItemShopExportedDate(int.Parse(dr["ShopID"].ToString()), (string)dr["File_Name"]);
                }
                //DataTable dtPromotion = GetFiles(2); // Select For Promotion( Export_Type=2 )
                //ConsoleWriteLine_Tofile(dtPromotion.Rows.Count.ToString());
                //ConsoleWriteLine_Tofile(dtInventory.Rows.Count.ToString());
                //if (dtPromotion.Rows.Count > 0 && dtPromotion != null)
                //{
                //Promotion p = new Promotion(dtPromotion, promotionFilePath); //comment out by inaoka
                // PrepareAndUpload(dtPromotion, promotionFilePath);
                //                }
            }
            catch (Exception ex)
            {
                //ConsoleWriteLine_Tofile(ex.ToString ());
                //SendErrMessage(ex.ToString());

                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "Yahoo CSV Upload" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();


            }

        }

        #region Methods
        private static void PrepareAndUpload(DataTable dt, string filePath)
        {
            //DataTable cri = new DataTable();
            //cri.Columns.Add("FTP_Host", typeof(string));
            //cri.Columns.Add("File_Type", typeof(int));

            foreach (DataRow dr in dt.Rows)
            {
                string[] name = dr["File_Name"].ToString().Split('$');
                string realName = name[0] + '.' + name[1].Split('.')[1];

                if (!CheckIfFileExist(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), realName, int.Parse(dr["Mall_ID"].ToString())))
                {
                    ConsoleWriteLine_Tofile(dr["File_Name"].ToString(), int.Parse(dr["ShopID"].ToString()));
                    PrepareFiles(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), (string)dr["File_Name"], (int)dr["ID"], filePath, int.Parse(dr["Mall_ID"].ToString()), int.Parse(dr["ShopID"].ToString()));

                    //if (!dr["File_Name"].ToString().Contains("#")) //cannot save item# record to exported datatable
                    //{
                    //    cri.Rows.Add((string)dr["FTP_Host"], int.Parse(dr["File_Type"].ToString()));
                    //}
                }
            }
        }

        private static void PrepareFiles(string ftpURI, string userName, string password, string fileName, int ID, string filePath, int mall_ID, int shopID)
        {
            string[] name = fileName.Split('$');

            string realName = name[0] + '.' + name[1].Split('.')[1];

            if (File.Exists(filePath + fileName))
            {
                if (File.Exists(filePath + realName))
                {
                    File.Delete(filePath + realName);
                }

                File.Move(filePath + fileName, filePath + realName); //Rename

                if (realName.Contains(".zip"))
                {
                    string[] folderName = realName.Split('.');

                    UploadFiles(ftpURI, userName, password, fileName, ID, Path.GetDirectoryName(filePath) + "/", realName);
                    ChangeStatus(ID);
                    //UpdateItemShopExportedDate(shopID, fileName);
                }
                else
                {
                    if (mall_ID != 3)
                    {
                        UploadFiles(ftpURI, userName, password, fileName, ID, filePath, realName);
                        ChangeStatus(ID);
                        UpdateItemShopExportedDate(shopID, fileName);
                    }
                }
                //UpdateItemShopExportedDate(shopID, fileName);
            }
        }

        private static void UploadFiles(string ftpURI, string userName, string password, string fileName, int ID, string filePath, string realName)
        {
            try
            {
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + realName));
                ftp.Credentials = new NetworkCredential(userName, password);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                FileStream fs = File.OpenRead(filePath + realName);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool CheckIfFileExist(string ftpLocation, string userName, string password, string fileName, int mallID)
        {
            ArrayList fName = new ArrayList();
            try
            {
                StringBuilder result = new StringBuilder();

                FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpLocation));
                requestDir.Method = WebRequestMethods.Ftp.ListDirectory;
                requestDir.Credentials = new NetworkCredential(userName, password);
                requestDir.UsePassive = true;
                requestDir.UseBinary = true;
                requestDir.KeepAlive = false;
                requestDir.Proxy = null;
                FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(ftpStream, Encoding.ASCII);

                DataTable dt = new DataTable();
                dt.Columns.Add("File_Name", typeof(string));
                while (!reader.EndOfStream)
                {
                    dt.Rows.Add(reader.ReadLine().ToString());
                }
                response.Close();
                ftpStream.Close();
                reader.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["File_Name"].ToString() == fileName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool CheckFileExistRecursive(string ftpLocation, string userName, string password, string fileName, int mallID)
        {
            ArrayList fName = new ArrayList();
            bool ok;
            try
            {
                if (mallID != 3)
                {
                    StringBuilder result = new StringBuilder();

                    FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpLocation));
                    requestDir.Method = WebRequestMethods.Ftp.ListDirectory;
                    requestDir.Credentials = new NetworkCredential(userName, password);
                    requestDir.UsePassive = true;
                    requestDir.UseBinary = true;
                    requestDir.KeepAlive = false;
                    requestDir.Proxy = null;
                    FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                    Stream ftpStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(ftpStream, Encoding.ASCII);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("File_Name", typeof(string));
                    while (!reader.EndOfStream)
                    {
                        dt.Rows.Add(reader.ReadLine().ToString());
                    }
                    response.Close();
                    ftpStream.Close();
                    reader.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["File_Name"].ToString() == fileName)
                        {
                            return true;
                            //CheckFileExistRecursive(ftpLocation, userName, password, fileName, mallID);
                        }
                    }
                    return false;
                }
                else
                {
                    using (Ftp client = new Ftp())
                    {
                        client.Connect(ftpLocation);
                        client.AuthTLS();
                        client.Login(userName, password);
                        ok = client.FileExists(ftpLocation + fileName);
                        // Upload the 'test.zip' file to the current folder on the server 
                        //client.Upload(fileName, FilePath);
                        client.Close();
                    }
                    return ok;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataTable GetFiles(int exportType)
        {
            //string sqlQuery = "SELECT Item_ExportQ.*, Code_Setup.Code_Description AS Mall_Name, Shop.FTP_Host, Shop.FTP_Account, Shop.FTP_Password, Shop.Mall_ID FROM Item_ExportQ INNER JOIN " +
            //                        "Shop ON Item_ExportQ.ShopID = Shop.ID LEFT OUTER JOIN Code_Setup ON Shop.Mall_ID = Code_Setup.Code_ID " +
            //                        "WHERE Code_Setup.Code_Type = 1 AND Item_ExportQ.IsExport = 0 AND Mall_ID=2 AND Export_Type = " + exportType; //Added by inaoka  Mail_ID=2
            //ConsoleWriteLine_Tofile(sqlQuery);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("SP_Yahoo_CSV_Upload", connectionString);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.CommandTimeout = 0;
            sda.SelectCommand.Parameters.AddWithValue("@Export_Type", exportType);
            sda.SelectCommand.Connection.Open();
            sda.Fill(dt);
            sda.SelectCommand.Connection.Close();
            return dt;
        }

        private static void ChangeStatus(int ID)
        {
            string sqlQuery = String.Format("UPDATE Item_ExportQ SET IsExport = 1,Upload_Date = getdate() WHERE ID={0}", ID);
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void SendErrMessage(string errMessage)
        {
            #region Send Email
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 100000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential("capital.zaw@gmail.com", "zawmyolwin123");
            MailMessage mm = new MailMessage("capital.zaw@gmail.com", "capital.zaw@gmail.com", "Error Message (CapitalSKS_CSVExport)", errMessage);

            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
            #endregion

        }

        private static void UpdateItemShopExportedDate(int shopID, string csvFileName)
        {
            //string query = String.Format("UPDATE Exhibition_Item_Shop SET CSV_ExportedDate = '{0}', Exhibition_Status = 1 WHERE Shop_ID = {1} AND CSV_FileName = '{2}'", DateTime.Now, shopID, csvFileName);
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_Exhibition_Item_Shop_ChangeFlag", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Shop_ID", shopID);
            cmd.Parameters.AddWithValue("@CSV_FileName", csvFileName);
            cmd.CommandTimeout = 0;
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }
        #endregion

        static void FileToFTP(string FTP_Host, string FTP_Account, string FTP_Password, string File_Name, int ID, string itemFilePath, int shop_id)
        {
            //ConsoleWriteLine_Tofile(FTP_Host + File_Name);

            if (!File.Exists(itemFilePath + File_Name))
            {
                ConsoleWriteLine_Tofile("file not found" + File_Name, shop_id);
                return;
            }

            //ConsoleWriteLine_Tofile(itemFilePath + File_Name);
            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential(FTP_Account, FTP_Password);

            while (true)
            {
                try
                {
                    ConsoleWriteLine_Tofile("FTP Check" + FTP_Host + File_Name, shop_id);
                    wc.DownloadFile(FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9_]{1,}", ""), itemFilePath + "Yahoo_Download");
                }
                catch (Exception ex)
                {
                    ConsoleWriteLine_Tofile("File Check" + FTP_Host + File_Name, shop_id);
                    ConsoleWriteLine_Tofile(ex.ToString(), shop_id);
                    ConsoleWriteLine_Tofile(FTP_Host + File_Name, shop_id);
                    ConsoleWriteLine_Tofile(FTP_Account + ":" + FTP_Password, shop_id);
                    ConsoleWriteLine_Tofile(itemFilePath + File_Name, shop_id);
                    wc.UploadFile(FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9_]{1,}", ""), itemFilePath + File_Name);
                    //DeleteUploadedFileInLocal
                    if (File.Exists(itemFilePath + File_Name))
                    {
                        File.Delete(itemFilePath + File_Name);
                    }
                    return;
                    //Environment.Exit(0);
                }
                ConsoleWriteLine_Tofile("sleep", shop_id);
                System.Threading.Thread.Sleep(5000);
            }
        }

        /*
        * Created By Inaoka
        * Created Date 2015/04/18
        * Updated By
        * Updated Date
        *
        * Why use this?
        * trace a information.
        * 正しく動作しているかを確認するために必要です。
        * 
        * Description:
        * trace by using the StreamWriter
        * with ConsoleWriteLine_Tofile
        * output ConsoleWriteLIne.txt in currenct directory
        * 
        */
        static void ConsoleWriteLine_Tofile(string traceText, int shop_id)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "YahooUpload_ConsoleWriteLine" + shop_id + ".txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
    }
}
