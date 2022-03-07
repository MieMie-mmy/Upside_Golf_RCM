/* 
Created By              :    Aung Kyaw
Created Date          :   16/07/2014
Updated By             :
Updated Date         :   

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

namespace CapitalSKS_CSVExport
{
    public class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //@"C:\My Data\Projects\Capital_SKS\Capital_SKS\Export_Promotion_CSV\";
        static string promotionFilePath = ConfigurationManager.AppSettings["PromotionFilePath"].ToString();
        //@"C:\My Data\Projects\Capital_SKS\Capital_SKS\Export_CSV\";
        static string itemFilePath = ConfigurationManager.AppSettings["ItemFilePath"].ToString();
        //@"C:\Item_Image_Rakutan\";
        static string extractPath = ConfigurationManager.AppSettings["ExtractFilePath"].ToString();

        static bool uploadzip = true;

        static void Main(string[] args)
        {
            try
            {
                //Select From Item_ExportQ
                //DataTable dtItem = GetFiles(1); // Select For Item( Export_Type=1 )
                DataTable dtPromotion = GetFiles(2); // Select For Promotion( Export_Type=2 )
                //  DataTable dtInventory = GetFiles(3); // Select For Inventory( Export_Type=3 )

                //if (dtItem.Rows.Count > 0 && dtItem != null)
                //{
                //    PrepareAndUpload(dtItem, itemFilePath);
                //}

                if (dtPromotion.Rows.Count > 0 && dtPromotion != null)
                {
                    Promotion p = new Promotion(dtPromotion, promotionFilePath);
                    // PrepareAndUpload(dtPromotion, promotionFilePath);
                }

                //if (dtInventory.Rows.Count > 0 && dtInventory != null)
                //{
                //    PrepareAndUpload(dtInventory, itemFilePath);
                //}

            }
            catch (Exception ex)
            {
                SendErrMessage(ex.ToString());
            }

        }

        #region Methods
        private static void PrepareAndUpload(DataTable dt, string filePath)
        {
            DataTable cri = new DataTable();
            cri.Columns.Add("FTP_Host", typeof(string));
            cri.Columns.Add("File_Type", typeof(int));
            bool condition = false;

            foreach (DataRow dr in dt.Rows)
            {
                #region remark exported regord 03/04/2015
                /*
                if (cri.Rows.Count > 0)
                {
                    foreach (DataRow row in cri.Rows)
                    {
                        //skip if second item's shop and file_type are identical to first item and so on
                        if ((string)row["FTP_Host"] == (string)dr["FTP_Host"] && int.Parse(dr["File_Type"].ToString()) == int.Parse(row["File_Type"].ToString()))
                        {
                            if (dr["File_Name"].ToString().Contains("lib_img"))
                            {
                                condition = false;
                                break;
                            }
                            else
                            {
                                condition = true;
                                break;
                            }
                        }
                        else
                        {
                            condition = false;
                        }
                    }
                }
                 */
                #endregion

                #region remark exported regord (New Logic)
                if (cri.Rows.Count > 0)
                {
                    foreach (DataRow row in cri.Rows)
                    {
                        //skip if second item's shop and file_type are identical to first item and so on
                        if ((string)row["FTP_Host"] == (string)dr["FTP_Host"])
                        {
                            if (int.Parse(dr["File_Type"].ToString()) == int.Parse(row["File_Type"].ToString()))
                            {
                                if (dr["File_Name"].ToString().Contains("lib_img"))
                                {
                                    condition = false;
                                    break;
                                }
                                else
                                {
                                    condition = true;
                                    break;
                                }
                            }
                            else
                            {
                                if ((string)dr["FTP_Host"] == "ftps.ponparemall.com" && uploadzip == false)
                                {
                                    condition = true;
                                    break;
                                }
                                else
                                {
                                    condition = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            condition = false;
                        }
                    }
                }
                #endregion

                if (!condition)
                {
                    string[] name = dr["File_Name"].ToString().Split('$');
                    string realName = name[0] + '.' + name[1].Split('.')[1];

                    if (!CheckIfFileExist(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), realName, int.Parse(dr["Mall_ID"].ToString())))
                    {
                        PrepareFiles(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), (string)dr["File_Name"], (int)dr["ID"], filePath, int.Parse(dr["Mall_ID"].ToString()), int.Parse(dr["ShopID"].ToString()));

                        if (!dr["File_Name"].ToString().Contains("#")) //cannot save item# record to exported datatable
                        {
                            cri.Rows.Add((string)dr["FTP_Host"], int.Parse(dr["File_Type"].ToString()));
                        }
                    }
                }

                //if (dr["File_Name"].ToString().Contains("#")) // item#.csv (for update 1st file) *I want to upload only that file.
                //{
                //    //break;
                //    while (CheckFileExistRecursive(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), "item.csv", int.Parse(dr["Mall_ID"].ToString())))
                //    {
                //        CheckFileExistRecursive(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), "item.csv", int.Parse(dr["Mall_ID"].ToString()));
                //    }
                //}
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

                    #region Rakuten Mall
                    if (mall_ID == 1 || mall_ID == 5)
                    {
                        UnZip(filePath, realName);

                        string localpath = extractPath + "item_img/"; // local path
                        ftpURI += "cabinet/images/item_img/"; //export path


                        if (Directory.Exists(localpath))
                        {
                            string[] subFolders = Directory.GetDirectories(localpath);

                            foreach (string sub in subFolders)
                            {
                                string subFolder = Path.GetFileName(sub);

                                CreateFileFolder(ftpURI + subFolder + "/", userName, password);

                                string[] files = Directory.GetFiles(localpath + subFolder);
                                foreach (string path in files)
                                {
                                    UploadFiles(ftpURI + subFolder + "/", userName, password, fileName, ID, Path.GetDirectoryName(path) + "/", Path.GetFileName(path));
                                    var file = new FileInfo(path);
                                    file.Attributes = FileAttributes.Normal;
                                    File.Delete(path);
                                }
                            }

                            ChangeStatus(ID);

                            // Delete local path
                            var dir = new DirectoryInfo(localpath);
                            dir.Attributes = FileAttributes.Normal;
                            dir.Delete(true);
                        }
                    }
                    #endregion
                    #region Ponpare Mall
                    else if (mall_ID == 3)
                    {
                        UnZip(filePath, realName);

                        string localpathpon = extractPath + "item_img/";

                        if (Directory.Exists(localpathpon)) //local
                        {
                            string[] subFolders = Directory.GetDirectories(localpathpon);

                            foreach (string sub in subFolders)
                            {
                                string subFolder = Path.GetFileName(sub);
                                string[] files = Directory.GetFiles(localpathpon + subFolder);

                                using (Ftp client = new Ftp())
                                {
                                    client.Connect(ftpURI);
                                    client.AuthTLS();
                                    client.Login(userName, password);
                                    string imgpath = "/imageUpload/images/";
                                    if (client.FolderExists(imgpath))
                                    {
                                        string itemimg = client.CreateFolder(imgpath + "item_img") + "/";

                                        string str = client.CreateFolder(itemimg + subFolder) + "/";

                                        //string str = client.CreateFolder("item_img/" + subFolder);
                                        foreach (string path in files)
                                        {
                                            if (client.FolderExists(str))
                                            {
                                                //string remotepath = Path.GetDirectoryName(str) + "\\" + Path.GetFileName(path);
                                                string remotepath = str + Path.GetFileName(path);
                                                string localpath = Path.GetDirectoryName(path) + "/" + Path.GetFileName(path);
                                                client.Upload(remotepath, localpath);
                                            }
                                            var file = new FileInfo(path);
                                            file.Attributes = FileAttributes.Normal;
                                            File.Delete(path);
                                        }
                                    }
                                    client.Close();
                                }
                            }
                            uploadzip = false;
                            ChangeStatus(ID);
                            var dir = new DirectoryInfo(localpathpon);
                            dir.Attributes = FileAttributes.Normal;
                            dir.Delete(true);
                            //UpdateItemShopExportedDate(shopID, fileName);
                        }

                    }
                    #endregion
                    else
                    {
                        UploadFiles(ftpURI, userName, password, fileName, ID, Path.GetDirectoryName(filePath) + "/", realName);
                        ChangeStatus(ID);
                        //UpdateItemShopExportedDate(shopID, fileName);
                    }
                }
                else
                {
                    if (mall_ID == 1 || mall_ID == 5)
                    {
                        ftpURI += "/ritem/batch/";
                    }
                    if (mall_ID != 3)
                    {
                        UploadFiles(ftpURI, userName, password, fileName, ID, filePath, realName);
                        ChangeStatus(ID);
                        UpdateItemShopExportedDate(shopID, fileName);
                        #region
                        if (fileName.ToString().Contains("#")) // item#.csv (for update 1st file) *I want to upload only that file.
                        {
                            //break;
                            while (CheckFileExistRecursive(ftpURI, userName, password, "item.csv", mall_ID))
                            {
                                CheckFileExistRecursive(ftpURI, userName, password, "item.csv", mall_ID);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        using (Ftp client = new Ftp())
                        {
                            client.Connect(ftpURI);
                            client.AuthTLS();
                            client.Login(userName, password);
                            //ok = client.FileExists(ftpURI + fileName);
                            // Upload the 'test.zip' file to the current folder on the server 
                            client.Upload(realName, filePath + realName);
                            client.Close();
                            ChangeStatus(ID);
                            UpdateItemShopExportedDate(shopID, fileName);
                            #region
                            if (fileName.ToString().Contains("#")) // item#.csv (for update 1st file) *I want to upload only that file.
                            {
                                //break;
                                while (CheckFileExistRecursive(ftpURI, userName, password, "item.csv", mall_ID))
                                {
                                    CheckFileExistRecursive(ftpURI, userName, password, "item.csv", mall_ID);
                                }
                            }
                            #endregion
                        }
                    }
                }

                //UpdateItemShopExportedDate(shopID, fileName);
            }
        }

        private static void UploadFiles(string ftpURI, string userName, string password, string fileName, int ID, string filePath, string realName)
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

        private static void UnZip(string filePath, string fileName)
        {
            using (ZipFile zip = ZipFile.Read(filePath + fileName))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        public static bool DirectoryExists(string directory, string user, string pass)
        {
            FtpWebRequest ftpRequest;
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(directory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            try
            {
                using (FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally  /* Resource Cleanup */
            {
                ftpRequest = null;
            }
        }

        private static bool CheckIfFileExist(string ftpLocation, string userName, string password, string fileName, int mallID)
        {
            ArrayList fName = new ArrayList();
            bool ok;
            try
            {
                if (fileName.Contains(".zip") && (mallID == 1 || mallID == 5)) //check for img zip
                {
                    fileName = fileName.Remove(fileName.Length - 4);
                    ftpLocation += "cabinet/images/item_img/";
                    CreateFileFolder(ftpLocation, userName, password);
                }
                else if (mallID == 1 || mallID == 5) //check for csv file
                {
                    ftpLocation += "ritem/batch/";
                }

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

        private static void CreateFileFolder(string ftpLocation, string userName, string password)
        {
            try
            {
                if (!DirectoryExists(ftpLocation, userName, password))
                {
                    WebRequest request = WebRequest.Create(ftpLocation);
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.Credentials = new NetworkCredential(userName, password);
                    WebResponse response = request.GetResponse();
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
            //                        "WHERE Code_Setup.Code_Type = 1 AND Item_ExportQ.IsExport = 0 AND Export_Type = " + exportType;
            string sqlQuery = "SP_Promotion_ExportGetFile";
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, connectionString);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.CommandTimeout = 0;
            sda.SelectCommand.Connection.Open();
            sda.Fill(dt);
            sda.SelectCommand.Connection.Close();
            return dt;
        }

        private static void ChangeStatus(int ID)
        {
            string sqlQuery = String.Format("Update Item_ExportQ set IsExport = 1 Where ID={0}", ID);
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
            string query = String.Format("UPDATE Exhibition_Item_Shop SET CSV_ExportedDate = '{0}', Exhibition_Status = 1 WHERE Shop_ID = {1} AND CSV_FileName = '{2}'", DateTime.Now, shopID, csvFileName);
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }
        #endregion
    }
}
