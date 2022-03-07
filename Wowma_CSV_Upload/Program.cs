

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
using System.Threading;

namespace Wowma_CSV_Upload_Console
{
 public class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static string itemFilePath = ConfigurationManager.AppSettings["ItemFilePath"].ToString();
        static string inventoryFilePath = ConfigurationManager.AppSettings["InventoryFilePath"].ToString();
        static string itemZipPath = ConfigurationManager.AppSettings["ItemZipPath"].ToString();
        static string extractPath = ConfigurationManager.AppSettings["ExtractFilePath"].ToString();
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Wowma Racket Csv Upload";
                DataTable dtItem = GetFiles();

                if (dtItem.Rows.Count > 0 && dtItem != null)
                {
                    ConsoleWriteLine_Tofile("Title for Wowma CSV Upload");
                    ConsoleWriteLine_Tofile("Process Start :" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//Date for upload to  ConsoleWriteLine_Tofile
                    Upload(dtItem);
                    ConsoleWriteLine_Tofile("Process End :" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//Date for upload to  ConsoleWriteLine_Tofile
                }
            }
            catch (Exception ex)
            {
                Save_SYS_Errorlog(ex.ToString());
            }

        }

        private static DataTable GetFiles()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("SP_Wowma_CSV_Upload_ByShop", connectionString);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@Shop_Name", "W PaintTool");
            sda.SelectCommand.Connection.Open();
            sda.Fill(dt);
            sda.SelectCommand.Connection.Close();
            return dt;
        }
        private static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "WowmaUpload_ConsoleWriteLine1.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
        private static void Upload(DataTable dtItem)
        {
            try
            {
                    if (dtItem.Rows[0]["File_Name"].ToString().Contains(".zip"))
                    {
                        //Upload Zip
                        PrepareZip(dtItem.Rows[0]["FTP_Host"].ToString(), dtItem.Rows[0]["FTP_Account"].ToString(), dtItem.Rows[0]["FTP_Password"].ToString(), (string)dtItem.Rows[0]["File_Name"], (int)dtItem.Rows[0]["ID"], itemZipPath, int.Parse(dtItem.Rows[0]["ShopID"].ToString()));
                       
                        //#region Delete row
                        //dtItem.Rows.Remove(dtItem.Rows[0]);
                        //dtItem.AcceptChanges();
                        //#endregion
                    }
                
           
                
            }
            catch(Exception ex) 
            {
                Save_SYS_Errorlog(ex.ToString());
            }

        }

        private static void FileToFTP(string FTP_Host, string FTP_Account, string FTP_Password, string File_Name, int ID, string itemFilePath)
        {
            if (!File.Exists(itemFilePath + File_Name))
            {
                ConsoleWriteLine_Tofile("File not found in local : " + File_Name);
                return;
            }
            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential(FTP_Account, FTP_Password);

            FTP_Host += "/ritem/batch/";
            bool exist = true;

            while (true && exist)
            {
                try
                {
                    //ConsoleWriteLine_Tofile("FTP Check" + FTP_Host + File_Name);
                    wc.DownloadFile(FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_]{1,}", ""), itemFilePath + "Racket_Rakuten_Download_1");
                }
                catch (Exception ex) //Does not exist
                //catch (WebException e) 
                {
                    //ConsoleWriteLine_Tofile("1st Check IsFileExist : False ; " + ((FtpWebResponse)e.Response).StatusDescription);
                    ConsoleWriteLine_Tofile("1st Check IsFileExist : False ; " + ex.ToString());
                    if (!CheckIfFileExist(FTP_Host, FTP_Account, FTP_Password, System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_]{1,}", "")))
                    {
                        //Does not exist

                        ConsoleWriteLine_Tofile("sleep 3000");
                        System.Threading.Thread.Sleep(3000);
                        ConsoleWriteLine_Tofile(FTP_Host + File_Name);
                        ConsoleWriteLine_Tofile(FTP_Account + ":" + FTP_Password);
                        ConsoleWriteLine_Tofile(itemFilePath + File_Name);
                        ConsoleWriteLine_Tofile("Now Upload file is " + File_Name);
                        // wc.UploadFile(FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_]{1,}", ""), itemFilePath + File_Name);
                        FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_]{1,}", "")));
                        ftp.Credentials = new NetworkCredential(FTP_Account, FTP_Password);
                        ftp.Method = WebRequestMethods.Ftp.UploadFile;
                        ftp.KeepAlive = true;
                        ftp.UseBinary = true;

                        FileStream fs = File.OpenRead(itemFilePath + File_Name);
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        fs.Close();

                        Stream ftpstream = ftp.GetRequestStream();
                        ftpstream.Write(buffer, 0, buffer.Length);
                        ftpstream.Close();

                        FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                        ConsoleWriteLine_Tofile(response.StatusDescription);
                        response.Close();
                        //DeleteUploadedFileInLocal
                        if (File.Exists(itemFilePath + File_Name))
                        {
                            File.Delete(itemFilePath + File_Name);
                        }
                        exist = false;
                        return;
                    }
                    else //Exists
                    {
                        exist = true;
                    }
                }
                //Exists
                ConsoleWriteLine_Tofile("1st Check IsFileExist : True ; " + FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_]{1,}", ""));
                ConsoleWriteLine_Tofile("sleep 5000 ; Wait to upload file : " + File_Name);
                System.Threading.Thread.Sleep(5000);
            }
        }
        public static void Save_SYS_Errorlog(string error)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", -1);
            cmd.Parameters.AddWithValue("@ErrorDetail", "Wowma_Image_Upload" + error);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static bool CheckIfFileExist(string ftpLocation, string userName, string password, string fileName)
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
                        ConsoleWriteLine_Tofile("2nd Check IsFileExist : True ; " + ftpLocation + fileName);
                        return true;
                    }
                }
                ConsoleWriteLine_Tofile("2nd Check IsFileExist : False ; " + ftpLocation + fileName);
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void UpdateItemShopExportedDate(int shopID, string csvFileName)
        {
            
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
        private static void ChangeStatus(int ID)
        {
            //string sqlQuery = String.Format("Update Item_ExportQ set IsExport = 1 Where ID={0}", ID);
            string sqlQuery = String.Format("UPDATE Item_ExportQ SET IsExport = 1,Upload_Date = getdate() WHERE ID={0}", ID);
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        private static void PrepareZip(string ftpURI, string userName, string password, string fileName, int ID, string filePath, int shopID)
        {
            try
            {
                string[] name = fileName.Split('$');

                string realName = name[0] + '.' + name[1].Split('.')[1];
                // string[] zipFiles = Directory.GetFiles(fileName);


                if (!DirectoryExists("ftp://u58726125@img.manager.wowma.jp/" + realName, userName, password))
                {

                    if (File.Exists(filePath + fileName))
                    {
                        if (File.Exists(filePath + realName))
                        {
                            File.Delete(filePath + realName);
                        }
                        File.Move(filePath + fileName, filePath + realName);

                        if (realName.Contains(".zip"))
                        {
                            //if (!DirectoryExists("ftp://u39998948@img.manager.wowma.jp/" + realName, userName, password))
                            //{

                            Uploadzip(ftpURI, userName, password, ID, filePath, realName);

                            //   ChangeStatus((int)dtItem.Rows[0]["ID"]);
                            ChangeStatus(ID);

                        }

                        ConsoleWriteLine_Tofile(TestOnFTP(ftpURI, userName, password));
                    }
                    // Delete zip 
                    if (File.Exists(filePath + realName))
                    {
                        File.Delete(filePath + realName);
                    }

                }
            }
            catch (Exception ex)
            {
                Save_SYS_Errorlog(ex.ToString());
            }
        }
        private static void Uploadzip(string ftpURI, string userName, string password, int ID, string filePath, string realName)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                ConsoleWriteLine_Tofile("Upload Start :" + sw.Elapsed);

               // CreateFileFolder(ftpURI, userName, password);
               // CreateFileFolder(ftpURI+realName, userName, password);
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
                ConsoleWriteLine_Tofile("File Name : " + realName); // FileName for upload to  ConsoleWriteLine_Tofile
                sw.Stop();
                ConsoleWriteLine_Tofile("Upload End : " + sw.Elapsed);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void UploadFiles(string ftpURI, string foldername, string userName, string password, string fileName, int ID, string filePath, string realName)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                ConsoleWriteLine_Tofile("Upload Start :" + sw.Elapsed);

                CreateFileFolder(ftpURI, userName, password);
                CreateFileFolder(ftpURI + foldername, userName, password);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + foldername + realName));
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
        private static bool DirectoryExists(string directory, string user, string pass)
        {
            FtpWebRequest ftpRequest;
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(directory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* Specify the Type of FTP Request */
            //ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
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

        private static string TestOnFTP(string path, string userName, string password)
        {
            ////bool IsExist=false;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
                request.Credentials = new NetworkCredential(userName, password);
                request.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;


                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return response.StatusDescription + path;

            }
            catch (WebException ex)
            {

                return null;
            }

            //FtpWebRequest ftpRequest;

            //ftpRequest = (FtpWebRequest)FtpWebRequest.Create(path);

            //ftpRequest.Credentials = new NetworkCredential(userName, password);

            //ftpRequest.Method = WebRequestMethods.File.DownloadFile;
            //try
            //{
            //    using (FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse())
            //    {
            //        return response.StatusDescription;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            //FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            //ftp.Credentials = new NetworkCredential(userName, password);
            //ftp.Method = WebRequestMethods.Ftp.MakeDirectory;
            //ftp.KeepAlive = true;
            //ftp.UseBinary = true;
            //FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
            //ConsoleWriteLine_Tofile(response.StatusDescription);
            //response.Close();

        }
    }
}
