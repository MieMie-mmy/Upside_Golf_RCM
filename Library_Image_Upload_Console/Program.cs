using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Configuration;
using System.Transactions;
using System.Text.RegularExpressions;
using Limilabs.FTP.Client;

namespace Library_Image_Upload_Console
{

    class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static string localPath = ConfigurationManager.AppSettings["localFilePath"].ToString();
        static string MovePath = ConfigurationManager.AppSettings["NGPath"].ToString();
        public static String ftpUserName;
        public static String ftpPassword;
        public static String ftpHost;
        public static String ftpdirectory;
        public static String[] q = new String[20];
        public static int rcount;
        public static int ecount = 0;
        public static int LogID;
        public static DataTable dterror = new DataTable();
        static void Main(string[] args)
        {
            DataTable dt = GetFTPdata();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Mall_ID"] != "3")
                    {
                        if (!String.IsNullOrWhiteSpace(dt.Rows[i]["LibraryFTP_Host"].ToString()) && !String.IsNullOrWhiteSpace(dt.Rows[i]["LibraryFTP_Account"].ToString())
                            && !String.IsNullOrWhiteSpace(dt.Rows[i]["LibraryFTP_Password"].ToString()) && !String.IsNullOrWhiteSpace(dt.Rows[i]["LibraryImage_Directory"].ToString()))
                        {
                            ftpHost = dt.Rows[i]["LibraryFTP_Host"].ToString();
                            ftpUserName = dt.Rows[i]["LibraryFTP_Account"].ToString();
                            ftpPassword = dt.Rows[i]["LibraryFTP_Password"].ToString();
                            ftpdirectory = dt.Rows[i]["LibraryImage_Directory"].ToString();
                            ExportToFtp(localPath, localPath);
                        }
                    }
                    else // ponpare ftps
                    {
                        if (!String.IsNullOrWhiteSpace(dt.Rows[i]["LibraryFTP_Host"].ToString()) && !String.IsNullOrWhiteSpace(dt.Rows[i]["LibraryFTP_Account"].ToString())
                            && !String.IsNullOrWhiteSpace(dt.Rows[i]["LibraryFTP_Password"].ToString()) && !String.IsNullOrWhiteSpace(dt.Rows[i]["LibraryImage_Directory"].ToString()))
                        {
                            ftpHost = dt.Rows[i]["LibraryFTP_Host"].ToString();
                            ftpUserName = dt.Rows[i]["LibraryFTP_Account"].ToString();
                            ftpPassword = dt.Rows[i]["LibraryFTP_Password"].ToString();
                            ftpdirectory = dt.Rows[i]["LibraryImage_Directory"].ToString();
                            ExportToFtps(localPath, localPath);
                        }
                    }
                }//forloop
                #region for Move and delete
                dterror.Columns.Add("Error_Msg", typeof(System.String));
                dterror.Columns.Add("Image_Name", typeof(System.String));
                dterror.Columns.Add("LogID", typeof(Int32));
                DataRow destRow = dterror.NewRow();
                dterror.Rows.Add(destRow);

                string[] files = Directory.GetFiles(localPath);
                rcount = files.Length - 1;
                LogID = ItemLogInsert(0, rcount, ecount);
                rcount = 0;
                foreach (string file in files)
                {
                    string fileName = null;

                    fileName = Path.GetFileName(file);
                    if (checkImageformat(fileName) == true)
                    {
                        if (fileName.Equals("vssver2.scc")) { }
                        else if (fileName.Contains(".jpg") || fileName.Contains(".gif") || fileName.Contains(".png"))
                        {


                            String paths = localPath + Path.GetFileName(fileName);
                            String mpath = MovePath + Path.GetFileName(fileName);
                            if (File.Exists(mpath))
                            {
                                File.Delete(mpath);
                            }//if delete

                            Move(localPath, fileName);
                            File.Delete(paths);
                            rcount += 1;
                            dterror.Rows[0]["Image_Name"] = fileName;
                            dterror.Rows[0]["Error_Msg"] = DBNull.Value;
                            dterror.Rows[0]["LogID"] = LogID;
                            LogInsert(dterror);
                        }//else
                        else//incorrect image format 
                        {
                            if (fileName != "NG" || fileName != "vssver2.scc")
                            {
                                String paths = localPath + Path.GetFileName(fileName);
                                String mpath = MovePath + Path.GetFileName(fileName);
                                if (File.Exists(mpath))
                                {
                                    File.Delete(mpath);
                                }//if delete

                                Move(localPath, fileName);
                                File.Delete(paths);
                                rcount += 1;
                                ecount += 1;

                                dterror.Rows[0]["Image_Name"] = fileName;
                                dterror.Rows[0]["Error_Msg"] = "Incorrect  image name!";
                                dterror.Rows[0]["LogID"] = LogID;
                                LogInsert(dterror);
                            }
                        }
                    }//checkimageformat
                    else//incorrect image format 
                    {
                        if (fileName != "NG" || fileName != "vssver2.scc")
                        {
                            String paths = localPath + Path.GetFileName(fileName);
                            String mpath = MovePath + Path.GetFileName(fileName);
                            if (File.Exists(mpath))
                            {
                                File.Delete(mpath);
                            }//if delete

                            Move(localPath, fileName);
                            File.Delete(paths);
                            rcount += 1;
                            ecount += 1;

                            dterror.Rows[0]["Image_Name"] = fileName;
                            dterror.Rows[0]["Error_Msg"] = "Incorrect  image name!";
                            dterror.Rows[0]["LogID"] = LogID;
                            LogInsert(dterror);
                        }
                    }
                }//foreach

                if (rcount > 0)
                {
                    ItemLogInsert(LogID, rcount, ecount);
                }
                else
                {
                    ItemLogDelete(LogID);
                }
                #endregion
            }
        }
        public static void ExportToFtp(string source, string path)
        {


            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                string fileName = null;

                fileName = Path.GetFileName(file);
                if (checkImageformat(fileName) == true)
                {
                    if (fileName.Equals("vssver2.scc")) { }
                    else if (fileName.Contains(".jpg") || fileName.Contains(".gif") || fileName.Contains(".png"))
                    {
                        //FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpHost + "/Test/" + fileName);
                        FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpdirectory + fileName);
                        ftp.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

                        ftp.KeepAlive = true;
                        ftp.UseBinary = true;
                        ftp.Method = WebRequestMethods.Ftp.UploadFile;

                        FileStream fs = File.OpenRead(source + fileName);
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        fs.Close();

                        Stream ftpstream = ftp.GetRequestStream();
                        ftpstream.Write(buffer, 0, buffer.Length);
                        ftpstream.Close();


                    }//else
                    else//incorrect image format 
                    {

                    }
                }//checkimageformat
                else//incorrect image format 
                {

                }
            }//foreach


        }
        public static void ExportToFtps(string source, string path)
        {
            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                string fileName = null;

                fileName = Path.GetFileName(file);
                if (checkImageformat(fileName) == true)
                {
                    if (fileName.Equals("vssver2.scc")) { }
                    else if (fileName.Contains(".jpg") || fileName.Contains(".gif") || fileName.Contains(".png"))
                    {
                        //FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpHost + "/Test/" + fileName);

                        using (Ftp client = new Ftp())
                        {
                            client.Connect(ftpHost);
                            client.AuthTLS();
                            client.Login(ftpUserName, ftpPassword);

                            client.Upload(ftpdirectory + fileName, source + fileName);

                            client.Close();
                        }

                    }//else
                    else//incorrect image format 
                    {

                    }
                }//checkimageformat
                else//incorrect image format 
                {

                }
            }//foreach


        }
        public static void ExportToFtp1(string source, string path)
        {
            dterror.Columns.Add("Error_Msg", typeof(System.String));
            dterror.Columns.Add("Image_Name", typeof(System.String));
            dterror.Columns.Add("LogID", typeof(Int32));
            DataRow destRow = dterror.NewRow();
            dterror.Rows.Add(destRow);

            string[] files = Directory.GetFiles(path);
            rcount = files.Length;
            LogID = ItemLogInsert(0, rcount, ecount);
            foreach (string file in files)
            {
                string fileName = null;

                fileName = Path.GetFileName(file);
                if (checkImageformat(fileName) == true)
                {
                    if (fileName.Equals("vssver2.scc")) { }
                    else if (fileName.Contains(".jpg") || fileName.Contains(".gif") || fileName.Contains(".png"))
                    {
                        FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpHost + "/Test/" + fileName);
                        ftp.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

                        ftp.KeepAlive = true;
                        ftp.UseBinary = true;
                        ftp.Method = WebRequestMethods.Ftp.UploadFile;

                        FileStream fs = File.OpenRead(source + fileName);
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        fs.Close();

                        Stream ftpstream = ftp.GetRequestStream();
                        ftpstream.Write(buffer, 0, buffer.Length);
                        ftpstream.Close();

                        String paths = localPath + Path.GetFileName(fileName);
                        String mpath = MovePath + Path.GetFileName(fileName);
                        if (File.Exists(mpath))
                        {
                            File.Delete(mpath);
                        }//if delete

                        Move(localPath, fileName);
                        File.Delete(paths);
                        rcount += 1;
                        dterror.Rows[0]["Image_Name"] = fileName;
                        dterror.Rows[0]["Error_Msg"] = DBNull.Value;
                        dterror.Rows[0]["LogID"] = LogID;
                        LogInsert(dterror);
                    }//else
                    else//incorrect image format 
                    {
                        if (fileName != "NG")
                        {
                            String paths = localPath + Path.GetFileName(fileName);
                            String mpath = MovePath + Path.GetFileName(fileName);
                            if (File.Exists(mpath))
                            {
                                File.Delete(mpath);
                            }//if delete

                            Move(localPath, fileName);
                            File.Delete(paths);
                            rcount += 1;
                            ecount += 1;

                            dterror.Rows[0]["Image_Name"] = fileName;
                            dterror.Rows[0]["Error_Msg"] = "Incorrect  image name!";
                            dterror.Rows[0]["LogID"] = LogID;
                            LogInsert(dterror);
                        }
                    }
                }//checkimageformat
                else//incorrect image format 
                {
                    if (fileName != "NG")
                    {
                        String paths = localPath + Path.GetFileName(fileName);
                        String mpath = MovePath + Path.GetFileName(fileName);
                        if (File.Exists(mpath))
                        {
                            File.Delete(mpath);
                        }//if delete

                        Move(localPath, fileName);
                        File.Delete(paths);
                        rcount += 1;
                        ecount += 1;

                        dterror.Rows[0]["Image_Name"] = fileName;
                        dterror.Rows[0]["Error_Msg"] = "Incorrect  image name!";
                        dterror.Rows[0]["LogID"] = LogID;
                        LogInsert(dterror);
                    }
                }
            }//foreach

            if (rcount > 0)
            {
                ItemLogInsert(LogID, rcount, ecount);
            }
            else
            {
                ItemLogDelete(LogID);
            }
        }
        public static bool Move(string lpath, string file)
        {
            string path = lpath + file;

            if (File.Exists(path))
            {
                File.Copy(path, Path.Combine(MovePath, Path.GetFileName(path)), true);

                return true;
            }
            else
            {
                return false;
            }

        }
        public static DataTable GetFTPdata()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SP_ShopdataforLibraryimage_Upload", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private static bool checkImageformat(string Imgname)
        {
            try
            {
                string sid = string.Empty;
                //if (Imgname.Length < 24)
                //{
                if (Imgname.Contains('.'))
                {
                    q = Imgname.Split('.');


                    string fix = q[0];



                    if (Regex.IsMatch(fix, @"^[\P{L}\p{IsBasicLatin}]+$") || Regex.IsMatch(fix, @"^\d+$"))
                    {

                        return true;
                    }
                    //if (Regex.IsMatch(fix, @"[^a-zA-Z]")||Regex.IsMatch(fix, @"^\d+$"))
                    //else  if (Regex.IsMatch(fix, @"[^a-zA-Z0-9\s]") )
                    //{

                    //        return true;
                    //}

                    else return false;
                }
                else
                    return false;
                // }//check length
                //  else
                //return false;
            }
            catch (Exception msg)
            {
                return false;
            }
        }
        public static int ItemLogInsert(int IID, int reccount, int errCount)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Image_uploadlog_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ID", IID);
                cmd.Parameters.AddWithValue("@ErrCount", errCount);
                cmd.Parameters.AddWithValue("@reccount", reccount);

                cmd.Parameters.AddWithValue("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                return id;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void ItemLogDelete(int IID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "Delete FROM Item_ImportLog Where ID =" + IID;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.Connection = connection;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void LogInsert(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Library_Image_Log_XML";

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connection;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
