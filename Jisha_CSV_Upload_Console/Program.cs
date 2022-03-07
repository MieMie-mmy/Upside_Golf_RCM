/* 
Created By              :    Aung Kyaw
Created Date          :   16/07/2014
Updated By             :Aye Aye Mon
Updated Date         :23/04/2015

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
using System.Diagnostics;

namespace Jisha_CSV_Upload_Console
{
    public class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //@"C:\My Data\Projects\Capital_SKS\Capital_SKS\Export_Promotion_CSV\";
        static string promotionFilePath = ConfigurationManager.AppSettings["PromotionFilePath"].ToString();
        //@"C:\My Data\Projects\Capital_SKS\Capital_SKS\Export_CSV\";
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
                Console.Title = "Jisha Csv Upload";
                //Select From Item_ExportQ
                //DataTable dtPromotion = GetFiles(2); // Select For Promotion( Export_Type=2 )
                DataTable dtItem = GetFiles(1); // Select For Item( Export_Type=1 )
                if (dtItem.Rows.Count > 0 && dtItem != null)
                {
                    for (int i = 0; i < dtItem.Rows.Count; i++)
                    {
                        if (dtItem.Rows[i]["File_Name"].ToString().Contains(".zip"))
                        {
                            PrepareZip(dtItem.Rows[i]["FTP_Host"].ToString(), dtItem.Rows[i]["FTP_Account"].ToString(), dtItem.Rows[i]["FTP_Password"].ToString(), (string)dtItem.Rows[i]["File_Name"], (int)dtItem.Rows[i]["ID"], itemFilePath, int.Parse(dtItem.Rows[i]["ShopID"].ToString()));
                            ChangeStatus((int)dtItem.Rows[i]["ID"]);
                        }
                        else // .csv
                        {
                            FileToFTP(dtItem.Rows[i]["FTP_Host"].ToString(), dtItem.Rows[i]["FTP_Account"].ToString(), dtItem.Rows[i]["FTP_Password"].ToString(), (string)dtItem.Rows[i]["File_Name"], (int)dtItem.Rows[i]["ID"], itemFilePath);
                            ChangeStatus((int)dtItem.Rows[i]["ID"]);
                            UpdateItemShopExportedDate(int.Parse(dtItem.Rows[i]["ShopID"].ToString()), (string)dtItem.Rows[i]["File_Name"]);
                        }
                    }
                }

                DataTable dtInventory = GetFiles(3);
                if (dtInventory.Rows.Count > 0 && dtInventory != null)
                {
                    //for (int i = 0; i < dtInventory.Rows.Count; i++)
                    //{
                    //    FileToFTP(dtInventory.Rows[i]["FTP_Host"].ToString(), dtInventory.Rows[i]["FTP_Account"].ToString(), dtInventory.Rows[i]["FTP_Password"].ToString(), (string)dtInventory.Rows[i]["File_Name"], (int)dtInventory.Rows[i]["ID"], inventoryFilePath);
                    //    ChangeStatus((int)dtInventory.Rows[i]["ID"]);
                    //}
                    do
                    {
                        int shop_id = int.Parse(dtInventory.Rows[0]["ShopID"].ToString());
                        string[] name = dtInventory.Rows[0]["File_Name"].ToString().Split('_');
                        string groupno = name[name.Length - 1].ToString();
                        DataTable dt = dtInventory.Select("File_Name LIKE '%" + groupno + "%' AND ShopID=" + shop_id + "").CopyToDataTable();
                        //Order by Item_ExportQ.ID ASC
                        dt.DefaultView.Sort = "ID ASC";
                        dt = dt.DefaultView.ToTable(true);
                        dt.AcceptChanges();
                        //Upload Files
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            FileToFTP(dtInventory.Rows[i]["FTP_Host"].ToString(), dtInventory.Rows[i]["FTP_Account"].ToString(), dtInventory.Rows[i]["FTP_Password"].ToString(), (string)dtInventory.Rows[i]["File_Name"], (int)dtInventory.Rows[i]["ID"], inventoryFilePath);
                            ChangeStatus((int)dtInventory.Rows[i]["ID"]);
                        }

                        #region delete row
                        List<DataRow> rows_to_remove = new List<DataRow>();
                        foreach (DataRow row1 in dtInventory.Rows)
                        {
                            if (int.Parse(row1["ShopID"].ToString()) == shop_id)
                            {
                                rows_to_remove.Add(row1);
                            }
                        }

                        foreach (DataRow row in rows_to_remove)
                        {
                            dtInventory.Rows.Remove(row);
                            dtInventory.AcceptChanges();
                        }
                        #endregion
                    } while (dtInventory.Rows.Count > 0);
                }

                //if (dtPromotion.Rows.Count > 0 && dtPromotion != null)
                //{
                //    Promotion p = new Promotion(dtPromotion, promotionFilePath);
                //    // PrepareAndUpload(dtPromotion, promotionFilePath);
                //}

            }
            catch (Exception ex)
            {

                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "Jisha CSV Upload" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();


            }

        }

        private static void PrepareZip(string ftpURI, string userName, string password, string fileName, int ID, string filePath, int shopID)
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

                    UnZip(filePath, realName);

                    string localpath = extractPath + "item_img/"; // local path
                    ftpURI += "cabinet/images/item_img/"; //export path

                    //CreateFileFolder(ftpURI, userName, password);

                    if (Directory.Exists(localpath))
                    {
                        string[] subFolders = Directory.GetDirectories(localpath);

                        foreach (string sub in subFolders)
                        {
                            string subFolder = Path.GetFileName(sub);

                            //CreateFileFolder(ftpURI + subFolder + "/", userName, password);

                            string[] files = Directory.GetFiles(localpath + subFolder);
                            foreach (string path in files)
                            {
                                CreateFileFolder(ftpURI, userName, password);
                                CreateFileFolder(ftpURI + subFolder + "/", userName, password);
                                UploadFiles(ftpURI + subFolder + "/", userName, password, fileName, ID, Path.GetDirectoryName(path) + "/", Path.GetFileName(path));
                                var file = new FileInfo(path);
                                file.Attributes = FileAttributes.Normal;
                                File.Delete(path);
                            }
                        }

                        // Delete local path
                        var dir = new DirectoryInfo(localpath);
                        dir.Attributes = FileAttributes.Normal;
                        dir.Delete(true);
                    }

                    #endregion
                }

                //Delete zip
                if (File.Exists(filePath + realName))
                {
                    File.Delete(filePath + realName);
                }
            }
        }

        static void FileToFTP(string FTP_Host, string FTP_Account, string FTP_Password, string File_Name, int ID, string itemFilePath)
        {

            if (!File.Exists(itemFilePath + File_Name))
            {
                ConsoleWriteLine_Tofile("file not found" + File_Name);
                return;
            }

            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential(FTP_Account, FTP_Password);
            FTP_Host += "/ritem/batch/";

            while (true)
            {
                try
                {
                    ConsoleWriteLine_Tofile("FTP Check" + FTP_Host + File_Name);
                    wc.DownloadFile(FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9_]{1,}", ""), itemFilePath + "Jisha_Download_21");
                }
                catch (Exception ex)
                {
                    ConsoleWriteLine_Tofile("File Check" + FTP_Host + File_Name);
                    ConsoleWriteLine_Tofile(ex.ToString());
                    ConsoleWriteLine_Tofile(FTP_Host + File_Name);
                    ConsoleWriteLine_Tofile(FTP_Account + ":" + FTP_Password);
                    ConsoleWriteLine_Tofile(itemFilePath + File_Name);
                    //  wc.UploadFile(FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9_]{1,}", ""), itemFilePath + File_Name);
                    wc.UploadFile(FTP_Host + File_Name, itemFilePath + File_Name);
                    //DeleteUploadedFileInLocal
                    if (File.Exists(itemFilePath + File_Name))
                    {
                        File.Delete(itemFilePath + File_Name);
                    }
                    return;
                }
                ConsoleWriteLine_Tofile("sleep");
                System.Threading.Thread.Sleep(5000);
            }
        }

        private static void PrepareAndUpload(DataTable dt, string filePath)
        {
            DataTable cri = new DataTable();
            cri.Columns.Add("FTP_Host", typeof(string));
            cri.Columns.Add("File_Type", typeof(int));
            bool condition = false;

            foreach (DataRow dr in dt.Rows)
            {
                string[] name = dr["File_Name"].ToString().Split('$');
                string realName = name[0] + '.' + name[1].Split('.')[1];

                if (!CheckIfFileExist(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), realName, int.Parse(dr["Mall_ID"].ToString())))
                {
                    ConsoleWriteLine_Tofile("File Name : " + dr["File_Name"].ToString());
                    PrepareFiles(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), (string)dr["File_Name"], (int)dr["ID"], filePath, int.Parse(dr["Mall_ID"].ToString()), int.Parse(dr["ShopID"].ToString()));

                    if (!dr["File_Name"].ToString().Contains("#")) //cannot save item# record to exported datatable
                    {
                        cri.Rows.Add((string)dr["FTP_Host"], int.Parse(dr["File_Type"].ToString()));
                    }
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
                            ConsoleWriteLine_Tofile("fileName :" + fileName);// filename to ConsoleWriteLine_Tofile
                            ConsoleWriteLine_Tofile("Date :" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));// Date to  ConsoleWriteLine_Tofile

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
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ConsoleWriteLine_Tofile("Upload Start :" + sw.Elapsed);
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + realName));
            ftp.Credentials = new NetworkCredential(userName, password);
            ftp.UsePassive = true;
            ftp.KeepAlive = false;
            ftp.UseBinary = true;
            ftp.Method = WebRequestMethods.Ftp.UploadFile;

            FileStream fs = File.OpenRead(filePath + realName);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();

            Stream ftpstream = ftp.GetRequestStream();
            ftpstream.Write(buffer, 0, buffer.Length);
            ftpstream.Close();
            ConsoleWriteLine_Tofile("File Name : " + fileName); // FileName for upload to  ConsoleWriteLine_Tofile
            sw.Stop();
            ConsoleWriteLine_Tofile("Upload End : " + sw.Elapsed);
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
            string sqlQuery = "SELECT Item_ExportQ.*, Code_Setup.Code_Description AS Mall_Name, Shop.FTP_Host, Shop.FTP_Account, Shop.FTP_Password, Shop.Mall_ID FROM Item_ExportQ INNER JOIN " +
                                    "Shop ON Item_ExportQ.ShopID = Shop.ID LEFT OUTER JOIN Code_Setup ON Shop.Mall_ID = Code_Setup.Code_ID " +
                                    "WHERE Code_Setup.Code_Type = 1 AND Item_ExportQ.IsExport = 0 AND Export_Type = " + exportType + "AND Code_Setup.Code_Description=N'自社'";

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, connectionString);
            sda.SelectCommand.CommandType = CommandType.Text;
            sda.SelectCommand.CommandTimeout = 0;
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
            //SqlConnection sqlConnection = new SqlConnection(connectionString);
            //SqlCommand cmd = new SqlCommand(query, sqlConnection);
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection;
            //sqlConnection.Open();
            //cmd.ExecuteNonQuery();
            //sqlConnection.Close();
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

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "JishaUpload_ConsoleWriteLine21.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

    }
}
