/*
 Using Table_
 * Exhibition_Item_Shop
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Limilabs.FTP.Client;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using Ionic.Zip;
using System.IO.Compression;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Net.Mail;
using System.Configuration;

namespace CapitalSKS_CSVExport
{
    public class Promotion
    {
        #region 16/06/2015
        static string promotionFilePathR = ConfigurationManager.AppSettings["PromotionFilePathR"].ToString();
        static string promotionFilePathY = ConfigurationManager.AppSettings["PromotionFilePathY"].ToString();
        static string promotionFilePathP = ConfigurationManager.AppSettings["PromotionFilePathP"].ToString();
        static string promotionFilePathJ = ConfigurationManager.AppSettings["PromotionFilePathJ"].ToString();
        static string ConsoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        #endregion
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public Promotion(DataTable dt, string filePath)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int checkfilepath = int.Parse(dt.Rows[i]["Mall_ID"].ToString());
                switch (checkfilepath)
                {
                    case 1: filePath = null;
                        filePath = promotionFilePathR;
                        break;

                    case 2: filePath = null;
                        filePath = promotionFilePathY;
                        break;

                    case 3: filePath = null;
                        filePath = promotionFilePathP;
                        break;

                    case 5: filePath = null;
                        filePath = promotionFilePathJ;
                        break;
                }

                if (dt.Rows[i]["Mall_ID"].ToString() == "1" || dt.Rows[i]["Mall_ID"].ToString() == "5")
                {
                    FileToFTP(dt.Rows[i]["FTP_Host"].ToString(), dt.Rows[i]["FTP_Account"].ToString(), dt.Rows[i]["FTP_Password"].ToString(), (string)dt.Rows[i]["File_Name"], (int)dt.Rows[i]["ID"], filePath);
                    ChangeStatus((int)dt.Rows[i]["ID"]);
                }
                else if (dt.Rows[i]["Mall_ID"].ToString() == "3")
                {
                    FileToFTP_Ponpare(dt.Rows[i]["FTP_Host"].ToString(), dt.Rows[i]["FTP_Account"].ToString(), dt.Rows[i]["FTP_Password"].ToString(), (string)dt.Rows[i]["File_Name"], (int)dt.Rows[i]["ID"], filePath);
                    ChangeStatus((int)dt.Rows[i]["ID"]);
                }
                else if (dt.Rows[i]["Mall_ID"].ToString() == "2")
                {
                    FileToFTP_Yahoo(dt.Rows[i]["FTP_Host"].ToString(), dt.Rows[i]["FTP_Account"].ToString(), dt.Rows[i]["FTP_Password"].ToString(), (string)dt.Rows[i]["File_Name"], (int)dt.Rows[i]["ID"], filePath, (int)dt.Rows[i]["ShopID"]);
                    ChangeStatus((int)dt.Rows[i]["ID"]);
                }
            }

            #region
            //foreach (DataRow dr in dt.Rows)
            //{
            //    int checkfilepath = int.Parse(dr["Mall_ID"].ToString());
            //    switch (checkfilepath)
            //    {
            //        case 1: filePath = null;
            //            filePath = promotionFilePathR;
            //            break;

            //        case 2: filePath = null;
            //            filePath = promotionFilePathY;
            //            break;

            //        case 3: filePath = null;
            //            filePath = promotionFilePathP;
            //            break;

            //        case 5: filePath = null;
            //            filePath = promotionFilePathJ;
            //            break;
            //    }
            #region remark exported record
            //if (cri.Rows.Count > 0)
            //{
            //    foreach (DataRow row in cri.Rows)
            //    {
            //        //skip if second item's shop and file_type are identical to first item and so on
            //        if ((string)row["FTP_Host"] == (string)dr["FTP_Host"] && int.Parse(dr["File_Type"].ToString()) == int.Parse(row["File_Type"].ToString()))
            //        {
            //            //if ((dr["File_Name"].ToString().Contains("GOLD")) || (dr["File_Name"].ToString().Contains("Cabinet")))
            //            if ((dr["File_Name"].ToString().Contains(".csv")))
            //            {
            //                condition = true;
            //                break;
            //            }
            //            else
            //            {
            //                condition = false;

            //                break;
            //            }
            //        }
            //        else
            //        {
            //            condition = false;
            //        }
            //    }
            //}
            #endregion

            //if (!condition)
            //{

            //if (dr["File_Name"].ToString().Contains(".csv"))
            //{
            //    string[] name = dr["File_Name"].ToString().Split('$');
            //    string realName = name[0] + '.' + name[1].Split('.')[1];

            //    if (!CheckIfFileExist(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), realName, int.Parse(dr["Mall_ID"].ToString())))
            //    {
            //        PrepareFiles(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), (string)dr["File_Name"], (int)dr["ID"], filePath, int.Parse(dr["Mall_ID"].ToString()), int.Parse(dr["ShopID"].ToString()));

            //        //if (!dr["File_Name"].ToString().Contains("#")) //cannot save item# record to exported datatable
            //        //{
            //        //    cri.Rows.Add((string)dr["FTP_Host"], int.Parse(dr["File_Type"].ToString()));
            //        //}
            //    }
            //}//else

            //else
            //{
            //    string[] name = dr["File_Name"].ToString().Split('$');
            //    string realName = name[0] + '.' + name[1].Split('.')[1];

            //    if (!CheckIfFileExist(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), realName, int.Parse(dr["Mall_ID"].ToString())))
            //    {
            //        PrepareFilesAttachment(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), (string)dr["File_Name"], (int)dr["ID"], filePath, int.Parse(dr["Mall_ID"].ToString()), int.Parse(dr["ShopID"].ToString()));


            //    }
            //}
            //}


            //if (dr["File_Name"].ToString().Contains("#")) // item#.csv (for update 1st file) *I want to upload only that file.
            //{
            //    //break;
            //    while (CheckFileExistRecursive(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), "item.csv", int.Parse(dr["Mall_ID"].ToString())))
            //    {
            //        CheckFileExistRecursive(dr["FTP_Host"].ToString(), dr["FTP_Account"].ToString(), dr["FTP_Password"].ToString(), "item.csv", int.Parse(dr["Mall_ID"].ToString()));
            //    }
            //}

            //}
            #endregion
        }

        private static void UploadFiles(string ftpURI, string userName, string password, string fileName, int ID, string filePath)
        {
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + System.Text.RegularExpressions.Regex.Replace(fileName, @"\$[0-9a-z_A-Z_]{1,}", "")));
            ftp.Credentials = new NetworkCredential(userName, password);

            ftp.KeepAlive = true;
            ftp.UseBinary = true;
            ftp.Method = WebRequestMethods.Ftp.UploadFile;

            FileStream fs = File.OpenRead(filePath + fileName);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();

            Stream ftpstream = ftp.GetRequestStream();
            ftpstream.Write(buffer, 0, buffer.Length);
            ftpstream.Close();
        }

        private static void FileToFTP(string FTP_Host, string FTP_Account, string FTP_Password, string File_Name, int ID, string itemFilePath)
        {
            if (!File.Exists(itemFilePath + File_Name))
            {
                ConsoleWriteLine_Tofile("file not found" + File_Name);
                return;
            }

            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential(FTP_Account, FTP_Password);

            FTP_Host += "/ritem/batch/";
            bool exist = true;
            //string[] name = File_Name.Split('$');
            //string file_name = name[0] + '.' + name[1].Split('.')[1];

            while (true && exist)
            {
                try
                {
                    ConsoleWriteLine_Tofile("FTP Check" + FTP_Host + File_Name);
                    wc.DownloadFile(FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_A-Z_]{1,}", ""), itemFilePath + "Promotion_Rakuten_Download");
                }
                catch (Exception ex)
                {
                    ConsoleWriteLine_Tofile("1st Check IsFileExist : False ; " + ex.ToString());
                    if (!CheckIfFileExist(FTP_Host, FTP_Account, FTP_Password, System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_A-Z_]{1,}", "")))
                    {
                        ConsoleWriteLine_Tofile("sleep 3000");
                        System.Threading.Thread.Sleep(3000);
                        ConsoleWriteLine_Tofile("File Check" + FTP_Host + File_Name);
                        ConsoleWriteLine_Tofile(ex.ToString());
                        ConsoleWriteLine_Tofile(FTP_Host + File_Name);
                        ConsoleWriteLine_Tofile(FTP_Account + ":" + FTP_Password);
                        ConsoleWriteLine_Tofile(itemFilePath + File_Name);
                        ConsoleWriteLine_Tofile("Now Upload file is " + File_Name);
                        //wc.UploadFile(FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_]{1,}", ""), itemFilePath + File_Name);
                        //DeleteUploadedFileInLocal

                        FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_A-Z_]{1,}", "")));
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

                        if (File.Exists(itemFilePath + File_Name))
                        {
                            File.Delete(itemFilePath + File_Name);
                        }
                        exist = false;
                        return;
                    }
                    else //Exists file
                    {
                        exist = true;
                    }
                }
                ConsoleWriteLine_Tofile("1st Check IsFileExist : True ; " + FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_A-Z_]{1,}", ""));
                ConsoleWriteLine_Tofile("sleep 5000 ; Wait to upload file : " + File_Name);
                System.Threading.Thread.Sleep(5000);
            }
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

        static void FileToFTP_Ponpare(string FTP_Host, string FTP_Account, string FTP_Password, string File_Name, int ID, string itemFilePath)
        {
            if (!File.Exists(itemFilePath + File_Name))
            {
                ConsoleWriteLine_Tofile("file not found" + File_Name);
                //return; //false
            }
            else
            {
                bool fileExist = false;  // Ftp flag

                using (Ftp client = new Ftp())
                {
                    client.Connect(FTP_Host);
                    client.AuthTLS();
                    client.Login(FTP_Account, FTP_Password);
                    do
                    {
                        List<FtpItem> arr = new List<FtpItem>();
                        arr = client.GetList("/");
                        for (int i = 0; i < arr.Count; i++)
                        {
                            if (arr[i].Name == System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_A-Z_]{1,}", ""))
                            {
                                fileExist = true;
                                break;
                            }
                            else
                            {
                                fileExist = false;
                            }
                        }
                    } while (fileExist);
                    client.Close();
                }


                if (!fileExist) // Upload
                {
                    using (Ftp client = new Ftp())
                    {
                        client.Connect(FTP_Host);
                        client.AuthTLS();
                        client.Login(FTP_Account, FTP_Password);
                        client.Upload(System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_A-Z_]{1,}", ""), itemFilePath + File_Name);
                        ConsoleWriteLine_Tofile("fileName :" + File_Name);// filename to ConsoleWriteLine_Tofile
                        client.Close();
                    }
                    //DeleteUploadedFileInLocal
                    if (File.Exists(itemFilePath + File_Name))
                    {
                        File.Delete(itemFilePath + File_Name);
                    }
                }

                ConsoleWriteLine_Tofile("sleep");
                System.Threading.Thread.Sleep(5000);
            }
        }

        static void FileToFTP_Yahoo(string FTP_Host, string FTP_Account, string FTP_Password, string File_Name, int ID, string itemFilePath, int shop_id)
        {
            //ConsoleWriteLine_Tofile(FTP_Host + File_Name);

            if (!File.Exists(itemFilePath + File_Name))
            {
                ConsoleWriteLine_Tofile("file not found" + File_Name);
                return;
            }

            //ConsoleWriteLine_Tofile(itemFilePath + File_Name);
            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential(FTP_Account, FTP_Password);

            while (true)
            {
                try
                {
                    ConsoleWriteLine_Tofile("FTP Check" + FTP_Host + File_Name);
                    wc.DownloadFile(FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_A-Z_]{1,}", ""), itemFilePath + "Promotion_Yahoo_Download");
                }
                catch (Exception ex)
                {
                    ConsoleWriteLine_Tofile("File Check" + FTP_Host + File_Name);
                    ConsoleWriteLine_Tofile(ex.ToString());
                    ConsoleWriteLine_Tofile(FTP_Host + File_Name);
                    ConsoleWriteLine_Tofile(FTP_Account + ":" + FTP_Password);
                    ConsoleWriteLine_Tofile(itemFilePath + File_Name);
                    wc.UploadFile(FTP_Host + System.Text.RegularExpressions.Regex.Replace(File_Name, @"\$[0-9a-z_A-Z_]{1,}", ""), itemFilePath + File_Name);
                    //DeleteUploadedFileInLocal
                    if (File.Exists(itemFilePath + File_Name))
                    {
                        File.Delete(itemFilePath + File_Name);
                    }
                    return;
                    //Environment.Exit(0);
                }
                ConsoleWriteLine_Tofile("sleep");
                System.Threading.Thread.Sleep(5000);
            }
        }

        private static void ChangeStatus(int ID)
        {
            string sqlQuery = String.Format("UPDATE Item_ExportQ SET IsExport = 1,Upload_Date = getdate() WHERE ID={0}", ID);
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(ConsoleWriteLinePath + "Promotion_Upload_ConsoleWriteLine.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        //private static void Upload(DataTable dt,string filePath)
        //{
        //    try
        //    {

        //        for(int i=0; i<dt.Rows.Count; i++)
        //        {
        //            int checkfilepath = int.Parse(dt.Rows[i]["Mall_ID"].ToString());
        //            switch (checkfilepath)
        //            {
        //                case 1: filePath = null;
        //                    filePath = promotionFilePathR;
        //                    break;

        //                case 2: filePath = null;
        //                    filePath = promotionFilePathY;
        //                    break;

        //                case 3: filePath = null;
        //                    filePath = promotionFilePathP;
        //                    break;

        //                case 5: filePath = null;
        //                    filePath = promotionFilePathJ;
        //                    break;
        //            }

        //            if (dt.Rows[i]["File_Name"].ToString().Contains(".csv"))
        //            {

        //                    FileToFTP(dt.Rows[i]["FTP_Host"].ToString(), dt.Rows[i]["FTP_Account"].ToString(), dt.Rows[i]["FTP_Password"].ToString(), (string)dt.Rows[i]["File_Name"], (int)dt.Rows[i]["ID"],(int)dt.Rows[i]["Mall_ID"], filePath);


        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private static void FileToFTP(string ftpHost, string ftpAcc, string ftpPassword, string fileName, int id, int mallID,string filePath)
        //{ 

        //    if (!File.Exists(filePath + fileName))
        //    {

        //        return;
        //    }

        //        WebClient wc = new WebClient();
        //        wc.Credentials = new NetworkCredential(ftpAcc, ftpPassword);
        //         bool exist = true;

        //         while (true && exist)
        //         {
        //             try
        //             {
        //                 wc.DownloadFile(ftpHost + System.Text.RegularExpressions.Regex.Replace(fileName, @"\$[0-9a-z_]{1,}", ""), filePath + "Promotion_Download_1");
        //             }
        //             catch (Exception ex)
        //             {
        //                 string name = System.Text.RegularExpressions.Regex.Replace(fileName, @"\$[0-9a-z_]{1,}", "");
        //                 if (!CheckIfFileExist1(ftpHost, ftpAcc, ftpPassword, name, mallID, filePath))
        //                 {
        //                    // ConsoleWriteLine_Tofile("sleep 3000");
        //                     System.Threading.Thread.Sleep(3000);
        //                     UploadFiles1(ftpHost, ftpAcc, ftpPassword,fileName, id, mallID, filePath);

        //                         if (File.Exists(filePath + fileName))
        //                         {
        //                             File.Delete(filePath + fileName);
        //                         }
        //                     exist = false;
        //                     return;
        //                 }

        //                 else
        //                 {
        //                    exist=true;
        //                 }
        //             }

        //             System.Threading.Thread.Sleep(5000);
        //         }

        //    //}
        //}
        //private static bool CheckIfFileExist1(string ftpHost, string ftpAcc, string ftpPassword, string fileName, int mallID, string filePath)
        //{

        //        ArrayList fName = new ArrayList();
        //        bool ok=false;
        //        try
        //        {
        //            if (mallID != 3)
        //            {
        //                ftpHost += "/ritem/batch/";

        //                StringBuilder result = new StringBuilder();

        //                FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpHost));
        //                requestDir.Method = WebRequestMethods.Ftp.ListDirectory;
        //                requestDir.Credentials = new NetworkCredential(ftpAcc, ftpPassword);
        //                requestDir.UsePassive = true;
        //                requestDir.UseBinary = true;
        //                requestDir.KeepAlive = false;
        //                requestDir.Proxy = null;
        //                FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
        //                Stream ftpStream = response.GetResponseStream();
        //                StreamReader reader = new StreamReader(ftpStream, Encoding.ASCII);

        //                DataTable dt = new DataTable();
        //                dt.Columns.Add("File_Name", typeof(string));
        //                while (!reader.EndOfStream)
        //                {
        //                    dt.Rows.Add(reader.ReadLine().ToString());
        //                }
        //                response.Close();
        //                ftpStream.Close();
        //                reader.Close();

        //                foreach (DataRow dr in dt.Rows)
        //                {
        //                    if (dr["File_Name"].ToString() == fileName)
        //                    {
        //                        return true;
        //                    }
        //                }

        //                return false;
        //            }

        //            else
        //            {
        //                //using (Ftp client = new Ftp())
        //                //{
        //                //    client.Connect(ftpHost);
        //                //    client.AuthTLS();
        //                //    client.Login(ftpAcc, ftpPassword);
        //                //    List<FtpItem> arr = new List<FtpItem>();
        //                //    arr = client.GetList("/");
        //                //    for (int i = 0; i < arr.Count; i++)
        //                //    {
        //                //        if (arr[i].Name == fileName)
        //                //        {
        //                //            ok = true;
        //                //            break;
        //                //        }
        //                //    }

        //                //    client.Close();
        //                //}
        //                //return ok;

        //                bool fileExist = false;  // Ftp flag

        //                using (Ftp client = new Ftp())
        //                {
        //                    client.Connect(ftpHost);
        //                    client.AuthTLS();
        //                    client.Login(ftpAcc, ftpPassword);
        //                    do
        //                    {
        //                        List<FtpItem> arr = new List<FtpItem>();
        //                        arr = client.GetList("/");
        //                        for (int i = 0; i < arr.Count; i++)
        //                        {
        //                            if (arr[i].Name == fileName)
        //                            {
        //                                fileExist = true;
        //                                break;
        //                            }
        //                            else
        //                            {
        //                                fileExist = false;
        //                            }
        //                        }
        //                    } while (fileExist);
        //                    client.Close();
        //                }
        //                return fileExist;

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //}
        //private static void UploadFiles1(string ftpHost, string ftpAcc, string ftpPassword, string fileName,int id, int mallID, string filePath)
        //{
        //    try
        //    {

        //        if (mallID != 3)
        //        {
        //            ftpHost += "/ritem/batch/";
        //            UploadFiles(ftpHost, ftpAcc, ftpPassword, fileName, mallID, filePath);
        //            ChangeStatus(id);
        //        }
        //        else
        //        {
        //            using (Ftp client = new Ftp())
        //            {
        //                client.Connect(ftpHost);
        //                client.AuthTLS();
        //                client.Login(ftpAcc, ftpPassword);
        //                client.Upload(System.Text.RegularExpressions.Regex.Replace(fileName, @"\$[0-9a-z_]{1,}", ""), filePath + fileName);
        //               // ConsoleWriteLine_Tofile("fileName :" + fileName);// filename to ConsoleWriteLine_Tofile
        //                client.Close();
        //            }
        //            //DeleteUploadedFileInLocal
        //            if (File.Exists(filePath + fileName))
        //            {
        //                File.Delete(filePath + fileName);
        //            }
        //            ChangeStatus(id);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private static bool CheckIfFileExist(string ftpLocation, string userName, string password, string fileName, int mallID)
        //{
        //    ArrayList fName = new ArrayList();
        //    bool ok;
        //    try
        //    {
        //        if (fileName.Contains(".zip") && (mallID == 1 || mallID == 5)) //check for img zip
        //        {
        //            fileName = fileName.Remove(fileName.Length - 4);
        //            ftpLocation += "cabinet/images/item_img/";
        //            CreateFileFolder(ftpLocation, userName, password);
        //        }
        //        else if (mallID == 1 || mallID == 5) //check for csv file
        //        {
        //            ftpLocation += "ritem/batch/";
        //        }

        //        if (mallID != 3)
        //        {
        //            StringBuilder result = new StringBuilder();

        //            FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpLocation));
        //            requestDir.Method = WebRequestMethods.Ftp.ListDirectory;
        //            requestDir.Credentials = new NetworkCredential(userName, password);
        //            requestDir.UsePassive = true;
        //            requestDir.UseBinary = true;
        //            requestDir.KeepAlive = false;
        //            requestDir.Proxy = null;
        //            FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
        //            Stream ftpStream = response.GetResponseStream();
        //            StreamReader reader = new StreamReader(ftpStream, Encoding.ASCII);

        //            DataTable dt = new DataTable();
        //            dt.Columns.Add("File_Name", typeof(string));
        //            while (!reader.EndOfStream)
        //            {
        //                string replace = null;
        //                string str = reader.ReadLine().ToString();
        //                if (str.Contains("??????"))
        //                {
        //                    replace = str.Replace("??????", "楽天");
        //                }
        //                if (string.IsNullOrWhiteSpace(replace))
        //                { dt.Rows.Add(str); }
        //                else
        //                    dt.Rows.Add(replace);
        //            }
        //            response.Close();
        //            ftpStream.Close();
        //            reader.Close();

        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                if (dr["File_Name"].ToString() == fileName)
        //                {
        //                    return true;
        //                }
        //            }
        //            return false;
        //        }
        //        else
        //        {
        //            using (Ftp client = new Ftp())
        //            {
        //                client.Connect(ftpLocation);
        //                client.AuthTLS();
        //                client.Login(userName, password);
        //                ok = client.FileExists(ftpLocation + fileName);
        //                // Upload the 'test.zip' file to the current folder on the server 
        //                //client.Upload(fileName, FilePath);
        //                client.Close();
        //            }
        //            return ok;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private static void CreateFileFolder(string ftpLocation, string userName, string password)
        //{
        //    try
        //    {
        //        if (!DirectoryExists(ftpLocation, userName, password))
        //        {
        //            WebRequest request = WebRequest.Create(ftpLocation);
        //            request.Method = WebRequestMethods.Ftp.MakeDirectory;
        //            request.Credentials = new NetworkCredential(userName, password);
        //            WebResponse response = request.GetResponse();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public static bool DirectoryExists(string directory, string user, string pass)
        //{
        //    FtpWebRequest ftpRequest;
        //    /* Create an FTP Request */
        //    ftpRequest = (FtpWebRequest)FtpWebRequest.Create(directory);
        //    /* Log in to the FTP Server with the User Name and Password Provided */
        //    ftpRequest.Credentials = new NetworkCredential(user, pass);
        //    /* Specify the Type of FTP Request */
        //    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
        //    try
        //    {
        //        using (FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse())
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    finally  /* Resource Cleanup */
        //    {
        //        ftpRequest = null;
        //    }
        //}
        //private static void PrepareFilesAttachment(string ftpURI, string userName, string password, string fileName, int ID, string filePath, int mall_ID, int shopID)
        //{
        //    //string[] name = fileName.Split('$');

        //    //string realName = name[0] + '.' + name[1].Split('.')[1];

        //    //if (File.Exists(filePath + fileName))
        //    //{
        //    //    if (File.Exists(filePath + realName))
        //    //    {
        //    //        File.Delete(filePath + realName);
        //    //    }

        //    //    File.Move(filePath + fileName, filePath + realName); //Rename

        //    //    if (realName.Contains(".zip"))
        //    //    {
        //    //        string[] folderName = realName.Split('.');

        //    //        #region Rakuten Mall
        //    //        if (mall_ID == 1 || mall_ID == 5)
        //    //        {
        //    //            UnZip(filePath, realName);
        //    //            string localpath = null;
        //    //            //string localpath = extractPath + "item_img/"; // local path
        //    //            ftpURI += "cabinet/images/item_img/"; //export path


        //    //            if (Directory.Exists(localpath))
        //    //            {
        //    //                string[] subFolders = Directory.GetDirectories(localpath);

        //    //                foreach (string sub in subFolders)
        //    //                {
        //    //                    string subFolder = Path.GetFileName(sub);

        //    //                    CreateFileFolder(ftpURI + subFolder + "/", userName, password);

        //    //                    string[] files = Directory.GetFiles(localpath + subFolder);
        //    //                    foreach (string path in files)
        //    //                    {
        //    //                        UploadFiles(ftpURI + subFolder + "/", userName, password, fileName, ID, Path.GetDirectoryName(path) + "/", Path.GetFileName(path));
        //    //                        var file = new FileInfo(path);
        //    //                        file.Attributes = FileAttributes.Normal;
        //    //                        File.Delete(path);
        //    //                    }
        //    //                }

        //    //                ChangeStatus(ID);

        //    //                // Delete local path
        //    //                var dir = new DirectoryInfo(localpath);
        //    //                dir.Attributes = FileAttributes.Normal;
        //    //                dir.Delete(true);
        //    //            }
        //    //        }
        //    //        #endregion
        //    //        #region Ponpare Mall
        //    //        else if (mall_ID == 3)
        //    //        {
        //    //            UnZip(filePath, realName);
        //    //            string localpathpon = null;
        //    //            //string localpathpon = extractPath + "item_img/";

        //    //            if (Directory.Exists(localpathpon)) //local
        //    //            {
        //    //                string[] subFolders = Directory.GetDirectories(localpathpon);

        //    //                foreach (string sub in subFolders)
        //    //                {
        //    //                    string subFolder = Path.GetFileName(sub);
        //    //                    string[] files = Directory.GetFiles(localpathpon + subFolder);

        //    //                    using (Ftp client = new Ftp())
        //    //                    {
        //    //                        client.Connect(ftpURI);
        //    //                        client.AuthTLS();
        //    //                        client.Login(userName, password);
        //    //                        string imgpath = "/imageUpload/images/";
        //    //                        if (client.FolderExists(imgpath))
        //    //                        {
        //    //                            string itemimg = client.CreateFolder(imgpath + "item_img") + "/";

        //    //                            string str = client.CreateFolder(itemimg + subFolder) + "/";

        //    //                            //string str = client.CreateFolder("item_img/" + subFolder);
        //    //                            foreach (string path in files)
        //    //                            {
        //    //                                if (client.FolderExists(str))
        //    //                                {
        //    //                                    //string remotepath = Path.GetDirectoryName(str) + "\\" + Path.GetFileName(path);
        //    //                                    string remotepath = str + Path.GetFileName(path);
        //    //                                    string localpath = Path.GetDirectoryName(path) + "/" + Path.GetFileName(path);
        //    //                                    client.Upload(remotepath, localpath);
        //    //                                }
        //    //                                var file = new FileInfo(path);
        //    //                                file.Attributes = FileAttributes.Normal;
        //    //                                File.Delete(path);
        //    //                            }
        //    //                        }
        //    //                        client.Close();
        //    //                    }
        //    //                }
        //    //                ChangeStatus(ID);
        //    //                var dir = new DirectoryInfo(localpathpon);
        //    //                dir.Attributes = FileAttributes.Normal;
        //    //                dir.Delete(true);
        //    //                //UpdateItemShopExportedDate(shopID, fileName);
        //    //            }

        //    //        }
        //    //        #endregion
        //    //        else
        //    //        {
        //    //            UploadFiles(ftpURI, userName, password, fileName, ID, Path.GetDirectoryName(filePath) + "/", realName);
        //    //            ChangeStatus(ID);
        //    //            //UpdateItemShopExportedDate(shopID, fileName);
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        if (mall_ID == 1 || mall_ID == 5)
        //    //        {
        //    //            ftpURI += "/ritem/batch/";
        //    //        }
        //    //        if (mall_ID != 3)
        //    //        {
        //    //            UploadFiles(ftpURI, userName, password, fileName, ID, filePath, realName);
        //    //            ChangeStatus(ID);
        //    //            // UpdateItemShopExportedDate(shopID, fileName);
        //    //            #region
        //    //            if (fileName.ToString().Contains("#")) // item#.csv (for update 1st file) *I want to upload only that file.
        //    //            {
        //    //                //break;
        //    //                while (CheckFileExistRecursive(ftpURI, userName, password, "item.csv", mall_ID))
        //    //                {
        //    //                    CheckFileExistRecursive(ftpURI, userName, password, "item.csv", mall_ID);
        //    //                }
        //    //            }
        //    //            #endregion
        //    //        }
        //    //        else
        //    //        {
        //    //            using (Ftp client = new Ftp())
        //    //            {
        //    //                client.Connect(ftpURI);
        //    //                client.AuthTLS();
        //    //                client.Login(userName, password);
        //    //                //ok = client.FileExists(ftpURI + fileName);
        //    //                // Upload the 'test.zip' file to the current folder on the server 
        //    //                client.Upload(realName, filePath + realName);
        //    //                client.Close();
        //    //                ChangeStatus(ID);
        //    //                //UpdateItemShopExportedDate(shopID, fileName);
        //    //                #region
        //    //                if (fileName.ToString().Contains("#")) // item#.csv (for update 1st file) *I want to upload only that file.
        //    //                {
        //    //                    //break;
        //    //                    while (CheckFileExistRecursive(ftpURI, userName, password, "item.csv", mall_ID))
        //    //                    {
        //    //                        CheckFileExistRecursive(ftpURI, userName, password, "item.csv", mall_ID);
        //    //                    }
        //    //                }
        //    //                #endregion
        //    //            }
        //    //        }
        //    //    }

        //    //    //UpdateItemShopExportedDate(shopID, fileName);
        //    //}
        //}
        //private static void PrepareFiles(string ftpURI, string userName, string password, string fileName, int ID, string filePath, int mall_ID, int shopID)
        //{
        //    //string[] name = fileName.Split('$');

        //    //string realName = name[0] + '.' + name[1].Split('.')[1];

        //    //if (File.Exists(filePath + fileName))
        //    //{
        //    //    if (File.Exists(filePath + realName))
        //    //    {
        //    //        File.Delete(filePath + realName);
        //    //    }

        //    //    File.Move(filePath + fileName, filePath + realName); //Rename

        //    //    if (realName.Contains(".zip"))
        //    //    {
        //    //        string[] folderName = realName.Split('.');

        //    //        #region Rakuten Mall
        //    //        if (mall_ID == 1 || mall_ID == 5)
        //    //        {
        //    //            UnZip(filePath, realName);
        //    //            string localpath = null;
        //    //            //string localpath = extractPath + "item_img/"; // local path
        //    //            ftpURI += "cabinet/images/item_img/"; //export path


        //    //            if (Directory.Exists(localpath))
        //    //            {
        //    //                string[] subFolders = Directory.GetDirectories(localpath);

        //    //                foreach (string sub in subFolders)
        //    //                {
        //    //                    string subFolder = Path.GetFileName(sub);

        //    //                    CreateFileFolder(ftpURI + subFolder + "/", userName, password);

        //    //                    string[] files = Directory.GetFiles(localpath + subFolder);
        //    //                    foreach (string path in files)
        //    //                    {
        //    //                        UploadFiles(ftpURI + subFolder + "/", userName, password, fileName, ID, Path.GetDirectoryName(path) + "/", Path.GetFileName(path));
        //    //                        var file = new FileInfo(path);
        //    //                        file.Attributes = FileAttributes.Normal;
        //    //                        File.Delete(path);
        //    //                    }
        //    //                }

        //    //                ChangeStatus(ID);

        //    //                // Delete local path
        //    //                var dir = new DirectoryInfo(localpath);
        //    //                dir.Attributes = FileAttributes.Normal;
        //    //                dir.Delete(true);
        //    //            }
        //    //        }
        //    //        #endregion
        //    //        #region Ponpare Mall
        //    //        else if (mall_ID == 3)
        //    //        {
        //    //            UnZip(filePath, realName);
        //    //            string localpathpon = null;
        //    //            //string localpathpon = extractPath + "item_img/";

        //    //            if (Directory.Exists(localpathpon)) //local
        //    //            {
        //    //                string[] subFolders = Directory.GetDirectories(localpathpon);

        //    //                foreach (string sub in subFolders)
        //    //                {
        //    //                    string subFolder = Path.GetFileName(sub);
        //    //                    string[] files = Directory.GetFiles(localpathpon + subFolder);

        //    //                    using (Ftp client = new Ftp())
        //    //                    {
        //    //                        client.Connect(ftpURI);
        //    //                        client.AuthTLS();
        //    //                        client.Login(userName, password);
        //    //                        string imgpath = "/imageUpload/images/";
        //    //                        if (client.FolderExists(imgpath))
        //    //                        {
        //    //                            string itemimg = client.CreateFolder(imgpath + "item_img") + "/";

        //    //                            string str = client.CreateFolder(itemimg + subFolder) + "/";

        //    //                            //string str = client.CreateFolder("item_img/" + subFolder);
        //    //                            foreach (string path in files)
        //    //                            {
        //    //                                if (client.FolderExists(str))
        //    //                                {
        //    //                                    //string remotepath = Path.GetDirectoryName(str) + "\\" + Path.GetFileName(path);
        //    //                                    string remotepath = str + Path.GetFileName(path);
        //    //                                    string localpath = Path.GetDirectoryName(path) + "/" + Path.GetFileName(path);
        //    //                                    client.Upload(remotepath, localpath);
        //    //                                }
        //    //                                var file = new FileInfo(path);
        //    //                                file.Attributes = FileAttributes.Normal;
        //    //                                File.Delete(path);
        //    //                            }
        //    //                        }
        //    //                        client.Close();
        //    //                    }
        //    //                }
        //    //                ChangeStatus(ID);
        //    //                var dir = new DirectoryInfo(localpathpon);
        //    //                dir.Attributes = FileAttributes.Normal;
        //    //                dir.Delete(true);
        //    //                //UpdateItemShopExportedDate(shopID, fileName);
        //    //            }

        //    //        }
        //    //        #endregion
        //    //        else
        //    //        {
        //    //            UploadFiles(ftpURI, userName, password, fileName, ID, Path.GetDirectoryName(filePath) + "/", realName);
        //    //            ChangeStatus(ID);
        //    //            //UpdateItemShopExportedDate(shopID, fileName);
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        if (mall_ID == 1 || mall_ID == 5)
        //    //        {
        //    //            ftpURI += "/ritem/batch/";
        //    //        }
        //    //        if (mall_ID != 3)
        //    //        {
        //    //            UploadFiles(ftpURI, userName, password, fileName, ID, filePath, realName);
        //    //            ChangeStatus(ID);
        //    //            // UpdateItemShopExportedDate(shopID, fileName);
        //    //            #region
        //    //            if (fileName.ToString().Contains("#")) // item#.csv (for update 1st file) *I want to upload only that file.
        //    //            {
        //    //                //break;
        //    //                while (CheckFileExistRecursive(ftpURI, userName, password, "item.csv", mall_ID))
        //    //                {
        //    //                    CheckFileExistRecursive(ftpURI, userName, password, "item.csv", mall_ID);
        //    //                }
        //    //            }
        //    //            #endregion
        //    //        }
        //    //        else
        //    //        {
        //    //            using (Ftp client = new Ftp())
        //    //            {
        //    //                client.Connect(ftpURI);
        //    //                client.AuthTLS();
        //    //                client.Login(userName, password);
        //    //                //ok = client.FileExists(ftpURI + fileName);
        //    //                // Upload the 'test.zip' file to the current folder on the server 
        //    //                client.Upload(realName, filePath + realName);
        //    //                client.Close();
        //    //                ChangeStatus(ID);
        //    //                // UpdateItemShopExportedDate(shopID, fileName);
        //    //                #region
        //    //                if (fileName.ToString().Contains("#")) // item#.csv (for update 1st file) *I want to upload only that file.
        //    //                {
        //    //                    //break;
        //    //                    while (CheckFileExistRecursive(ftpURI, userName, password, "item.csv", mall_ID))
        //    //                    {
        //    //                        CheckFileExistRecursive(ftpURI, userName, password, "item.csv", mall_ID);
        //    //                    }
        //    //                }
        //    //                #endregion
        //    //            }
        //    //        }
        //    //    }

        //    //    //UpdateItemShopExportedDate(shopID, fileName);
        //    //}
        //}
        //private static bool CheckFileExistRecursive(string ftpLocation, string userName, string password, string fileName, int mallID)
        //{
        //    ArrayList fName = new ArrayList();
        //    bool ok;
        //    try
        //    {
        //        if (mallID != 3)
        //        {
        //            StringBuilder result = new StringBuilder();

        //            FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpLocation));
        //            requestDir.Method = WebRequestMethods.Ftp.ListDirectory;
        //            requestDir.Credentials = new NetworkCredential(userName, password);
        //            requestDir.UsePassive = true;
        //            requestDir.UseBinary = true;
        //            requestDir.KeepAlive = false;
        //            requestDir.Proxy = null;
        //            FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
        //            Stream ftpStream = response.GetResponseStream();
        //            StreamReader reader = new StreamReader(ftpStream, Encoding.ASCII);

        //            DataTable dt = new DataTable();
        //            dt.Columns.Add("File_Name", typeof(string));
        //            while (!reader.EndOfStream)
        //            {
        //                dt.Rows.Add(reader.ReadLine().ToString());
        //            }
        //            response.Close();
        //            ftpStream.Close();
        //            reader.Close();

        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                if (dr["File_Name"].ToString() == fileName)
        //                {
        //                    return true;
        //                    //CheckFileExistRecursive(ftpLocation, userName, password, fileName, mallID);
        //                }
        //            }
        //            return false;
        //        }
        //        else
        //        {
        //            using (Ftp client = new Ftp())
        //            {
        //                client.Connect(ftpLocation);
        //                client.AuthTLS();
        //                client.Login(userName, password);
        //                ok = client.FileExists(ftpLocation + fileName);
        //                // Upload the 'test.zip' file to the current folder on the server 
        //                //client.Upload(fileName, FilePath);
        //                client.Close();
        //            }
        //            return ok;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private static void UnZip(string filePath, string fileName)
        //{
        //    using (ZipFile zip = ZipFile.Read(filePath + fileName))
        //    {
        //        foreach (ZipEntry e in zip)
        //        {
        //            //  e.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
        //        }
        //    }
        //}
        //public static void SendErrMessage(string errMessage)
        //{
        //    #region Send Email
        //    SmtpClient client = new SmtpClient();
        //    client.Port = 587;
        //    client.Host = "smtp.gmail.com";
        //    client.EnableSsl = true;
        //    client.Timeout = 100000;
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    client.UseDefaultCredentials = false;

        //    client.Credentials = new System.Net.NetworkCredential("capital.zaw@gmail.com", "zawmyolwin123");
        //    MailMessage mm = new MailMessage("capital.zaw@gmail.com", "capital.zaw@gmail.com", "Error Message (CapitalSKS_CSVExport)", errMessage);

        //    mm.BodyEncoding = UTF8Encoding.UTF8;
        //    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

        //    client.Send(mm);
        //    #endregion

        //}
        //private static void UpdateItemShopExportedDate(int shopID, string csvFileName)
        //{
        //    string query = String.Format("UPDATE Exhibition_Item_Shop SET CSV_ExportedDate = '{0}', Exhibition_Status = 1 WHERE Shop_ID = {1} AND CSV_FileName = '{2}'", DateTime.Now, shopID, csvFileName);
        //    SqlConnection sqlConnection = new SqlConnection(connectionString);
        //    SqlCommand cmd = new SqlCommand(query, sqlConnection);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = sqlConnection;
        //    sqlConnection.Open();
        //    cmd.ExecuteNonQuery();
        //    sqlConnection.Close();
        //}

    }
}
