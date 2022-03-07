using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;
using System.Net.Sockets;
using WinSCP;

namespace TennisClassic_Upload_Console
{
    class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        static string itemFilePath = ConfigurationManager.AppSettings["ItemFilePath"].ToString();
        static string extractPath = ConfigurationManager.AppSettings["ExtractFilePath"].ToString();
        static string inventoryFilePath = ConfigurationManager.AppSettings["InventoryFilePath"].ToString();
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "ORS自社 csv Upload";
                //Select Export_Type=1 Item
                DataTable dtItem = GetFiles(1);
                if (dtItem.Rows.Count > 0 && dtItem != null)
                {
                    ConsoleWriteLine_Tofile("Title for ORS自社 CSV Upload");
                    //Date for upload to  ConsoleWriteLine_Tofile
                    ConsoleWriteLine_Tofile("Process Start :" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Upload(dtItem);
                    //Date for upload to  ConsoleWriteLine_Tofile
                    ConsoleWriteLine_Tofile("Process End :" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                DataTable dtInventory = GetFiles(3);
                if (dtInventory.Rows.Count > 0 && dtInventory != null)
                {
                    do
                    {
                        int shop_id = int.Parse(dtInventory.Rows[0]["ShopID"].ToString());
                        //string[] name = dtInventory.Rows[0]["File_Name"].ToString().Split('_');
                        //string groupno = name[name.Length - 1].ToString();
                        DataTable dt = dtInventory.Select("File_Name LIKE 'DetailSKU%' AND ShopID=" + shop_id + "").CopyToDataTable();
                        //Order by Item_ExportQ.ID ASC
                        dt.DefaultView.Sort = "ID ASC";
                        dt = dt.DefaultView.ToTable(true);
                        dt.AcceptChanges();
                        //Upload Files
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            FileToFTP(dtInventory.Rows[i]["FTP_Host"].ToString(), dtInventory.Rows[i]["FTP_Account"].ToString(), dtInventory.Rows[i]["FTP_Password"].ToString(), (string)dtInventory.Rows[i]["File_Name"], (int)dtInventory.Rows[i]["ID"], inventoryFilePath,2);
                            ChangeStatus((int)dtInventory.Rows[i]["ID"]);
                        }

                        #region delete row
                        List<DataRow> rows_to_remove = new List<DataRow>();
                        foreach (DataRow row1 in dtInventory.Rows)
                        {
                            //to remove rows only with same ShopID and groupno
                            if ((int.Parse(row1["ShopID"].ToString()) == shop_id) && row1["File_Name"].ToString().Contains("DetailSKU"))
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
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("トランスポート ストリームから予期しない EOF または 0 バイトを受信しました。"))
                { }
                else
                {
                    String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", -1);
                    cmd.Parameters.AddWithValue("@ErrorDetail", "ORS自社 CSV Upload" + ex.ToString());
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
        }

        private static DataTable GetFiles(int exportType)
        {
            string sqlQuery = "SELECT Item_ExportQ.*, Code_Setup.Code_Description AS Mall_Name, Shop.FTP_Host, Shop.FTP_Account, Shop.FTP_Password, Shop.Mall_ID FROM Item_ExportQ INNER JOIN " +
                                    "Shop ON Item_ExportQ.ShopID = Shop.ID LEFT OUTER JOIN Code_Setup ON Shop.Mall_ID = Code_Setup.Code_ID " +
                                    "WHERE Code_Setup.Code_Type = 1 AND Item_ExportQ.IsExport = 0 AND Export_Type = " + exportType + "AND Code_Setup.Code_Description=N'ORS自社' order by Item_ExportQ.ID asc";

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, connectionString);
            sda.SelectCommand.CommandType = CommandType.Text;
            sda.SelectCommand.CommandTimeout = 0;
            sda.SelectCommand.Connection.Open();
            sda.Fill(dt);
            sda.SelectCommand.Connection.Close();
            return dt;
        }

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Upload_ConsoleWriteLine29.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
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
                do
                {
                    if (dtItem.Rows[0]["File_Name"].ToString().Contains(".zip"))
                    {

                        PrepareZip(dtItem.Rows[0]["FTP_Host"].ToString(), dtItem.Rows[0]["FTP_Account"].ToString(), dtItem.Rows[0]["FTP_Password"].ToString(), (string)dtItem.Rows[0]["File_Name"], (int)dtItem.Rows[0]["ID"], itemFilePath, int.Parse(dtItem.Rows[0]["ShopID"].ToString()));
                        ChangeStatus((int)dtItem.Rows[0]["ID"]);
                        #region Delete row
                        dtItem.Rows.Remove(dtItem.Rows[0]);
                        dtItem.AcceptChanges();
                        #endregion
                    }
                    else
                    {
                       // string[] name = dtItem.Rows[0]["File_Name"].ToString().Split('_');
                       //string groupno = name[name.Length - 1].ToString();
                        int shop_id = int.Parse(dtItem.Rows[0]["ShopID"].ToString());
                        DataTable dt = dtItem.Select("File_Name LIKE 'HeaderItem%' OR File_Name LIKE 'DetailSKU%'  AND ShopID=" + shop_id + "").CopyToDataTable();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            FileToFTP(dtItem.Rows[i]["FTP_Host"].ToString(), dtItem.Rows[i]["FTP_Account"].ToString(), dtItem.Rows[i]["FTP_Password"].ToString(), (string)dtItem.Rows[i]["File_Name"], (int)dtItem.Rows[i]["ID"], itemFilePath,1);
                            ChangeStatus((int)dtItem.Rows[i]["ID"]);
                            UpdateItemShopExportedDate(int.Parse(dtItem.Rows[i]["ShopID"].ToString()), (string)dtItem.Rows[i]["File_Name"]);
                        }
                        #region delete row
                        List<DataRow> rows_to_remove = new List<DataRow>();
                        foreach (DataRow row1 in dtItem.Rows)
                        {
                            //to remove rows only with same ShopID and groupno
                            if ((int.Parse(row1["ShopID"].ToString()) == shop_id) && (row1["File_Name"].ToString().Contains("HeaderItem") || row1["File_Name"].ToString().Contains("DetailSKU")))
                            {
                                rows_to_remove.Add(row1);
                            }
                        }
                        foreach (DataRow row in rows_to_remove)
                        {
                            dtItem.Rows.Remove(row);
                            dtItem.AcceptChanges();
                        }
                        #endregion
                    }

                } while (dtItem.Rows.Count > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void PrepareZip(string ftpURI, string userName, string password, string fileName, int ID, string filePath, int shopID)
        {
            string[] name = fileName.Split('$');
            string realName = name[0] + '.' + name[1].Split('.')[1];
            if (File.Exists(filePath + fileName))
            {
                File.Move(filePath + fileName, filePath + realName); //Rename
                if (realName.Contains(".zip"))
                {
                    string[] folderName = realName.Split('.');
                    #region Tennis
                    UnZip(filePath, realName);
                    string localpathpon = extractPath + @"item_img\";
                    if (Directory.Exists(localpathpon)) //local
                    {

                        #region upload ftp mm                        
                        SessionOptions sessionOptions = new SessionOptions
                        {
                            Protocol = Protocol.Ftp,
                            HostName = ftpURI,
                            UserName = userName,
                            Password = password,
                            PortNumber = 21,

                        };

                        using (Session session = new Session())
                        {
                            session.Open(sessionOptions);

                            string imgpath = "/www/html/upload/save_image/";
                            bool FolderExist = session.FileExists(imgpath);
                            if (FolderExist == true)
                            {
                                string[] files = Directory.GetFiles(localpathpon);
                                foreach (string img in files)
                                {

                                    string remotepath = imgpath + Path.GetFileName(img);
                                    string localpath = Path.GetDirectoryName(img) + "\\" + Path.GetFileName(img);
                                    session.PutFiles(localpath, remotepath, false, null);
                                    var file = new FileInfo(img);
                                    file.Attributes = FileAttributes.Normal;
                                    File.Delete(img);
                                }
                            }
                            session.Close();
                        }
                        #endregion

                        var dir = new DirectoryInfo(localpathpon);
                        dir.Attributes = FileAttributes.Normal;
                        dir.Delete(true);
                    }
                    #endregion
                }
                // Delete zip 
                if (File.Exists(filePath + realName))
                {
                    File.Delete(filePath + realName);
                }
            }
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

        static void FileToFTP(string FTP_Host, string FTP_Account, string FTP_Password, string File_Name, int ID, string itemFilePath, int Option)
        {
            if (!File.Exists(itemFilePath + File_Name))
            {
                ConsoleWriteLine_Tofile("file not found" + File_Name);
                //return; //false
            }
            else
            {
                #region new code 
                bool fileExist = false;
                string remotePath = "";
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = FTP_Host,
                    UserName = FTP_Account,
                    Password = FTP_Password,
                    PortNumber = 21,
                    FtpSecure = FtpSecure.Explicit,
                    TlsHostCertificateFingerprint = "02:62:82:7b:4a:47:d0:bf:9a:6f:34:21:ba:f7:f2:5c:0d:93:89:7d"

                };
                using (Session session = new Session())
                {
                    // Connect
                    do
                    {
                        int attempts = 3;
                        do
                        {
                            try
                            {
                                session.Open(sessionOptions);
                            }
                            catch (Exception e)
                            {
                                if (attempts == 0)
                                {
                                    throw;
                                }
                            }
                            attempts--;
                        }
                        while (!session.Opened);
                        // const string remotePath = "/www/html/admin/product_head_csv";

                        if (Option == 1)    //  Option : 1 => for exhibition 
                        {
                            if (File_Name.Contains("HeaderItem"))
                            {
                                remotePath = "/www/html/admin/product_head_csv";
                            }
                            else
                            {
                                remotePath = "/www/html/admin/product_detail_csv";
                            }
                        }
                        else
                        {  //  Option : 2 => for inventory
                            remotePath = "/www/html/admin/stock_csv";

                        }
                        String name = "";
                        if (File_Name.Contains("HeaderItem"))
                        {
                            name = "HeaderItem.csv";
                        }
                        else
                        {
                            name = "DetailSKU.csv";
                        }
                        RemoteDirectoryInfo directory = session.ListDirectory(remotePath);
                        fileExist = session.FileExists(remotePath + "/" + name);
                    } while (fileExist);
                    session.Close();
                }

                if (!fileExist)     // Upload
                {
                    using (Session session = new Session())
                    {
                        ConsoleWriteLine_Tofile("sleep 5000");
                        System.Threading.Thread.Sleep(3000);
                        session.Open(sessionOptions);
                        session.PutFiles(itemFilePath + File_Name, remotePath + "/" + File_Name, false, null);

                        ConsoleWriteLine_Tofile("fileName :" + File_Name);// filename to ConsoleWriteLine_Tofile
                        session.Close();
                    }
                    //DeleteUploadedFileInLocal
                    if (File.Exists(itemFilePath + File_Name))
                    {
                        File.Delete(itemFilePath + File_Name);
                    }

                }
                ConsoleWriteLine_Tofile("sleep");
                System.Threading.Thread.Sleep(5000);
                #endregion

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
    }
}
