using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Item_Image_Import_Console
{
    public class Program
    {
        //static string extractPath = ConfigurationManager.AppSettings["localFilePathImage"].ToString();
        static string localPath = ConfigurationManager.AppSettings["localFilePath"].ToString();
        public static String ftpPath = ConfigurationManager.AppSettings["FTPPath"].ToString();
        public static String ftpUserName = ConfigurationManager.AppSettings["FtpUserName"].ToString();
        public static String ftpPassword = ConfigurationManager.AppSettings["FtpPassword"].ToString();
        public static String[] q = new String[20];
        public static DataTable dterror = new DataTable();
        public static int rcount;
        public static int ecount = 0;
        public static int LogID;
        public static DateTime? startime = null;
        public static DateTime? exetime = null;
        public static int extime = 0;
        public static String[] t = new String[4];

      public  static void Main(string[] args)
        {
            try
            {
                CreateDir(localPath);
                ReadData();
            }
            catch(Exception ex)
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "Item_ImageImport" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
          
        }
      public static void CreateDir(String path)
      {
          if (!Directory.Exists(path))
          {
              Directory.CreateDirectory(path);
          }
      }


      private static bool checkImageformat(string Imgname)
      {
          try
          {
              string sid = string.Empty;
              if (System.Text.RegularExpressions.Regex.IsMatch(Imgname, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
              {
                  if (Imgname.Length < 24)
                  {
                      if (Imgname.Contains('-'))
                      {
                          q = Imgname.Split('-');

                          sid = q[q.Length - 1].ToString();

                          q = null;
                          q = sid.Split('.');
                          string fix = q[0];

                          if (Regex.IsMatch(fix, @"^\d+$"))
                          {
                              if (Convert.ToInt32(fix) > 20)
                                  return false;
                              else
                                  return true;
                          }
                          else return false;
                      }
                      else
                          return false;
                  }//check length
                  else
                      return false;
              }
              else
                  return false;
          }
          catch (Exception msg)
          {
              return false;
          }
      }


      private static DataTable getItemcode(string Imgname, DataTable dt)
      {
          if(Imgname.Length <24)
          {
              if (Imgname.Contains('-'))
              {
                  string sid = string.Empty;
                  string itemcode = string.Empty;
                  q = Imgname.Split('-');


                  for (int g = 0; g < q.Length - 1; g++)
                  {
                      if (g == q.Length - 2)
                      { itemcode += q[g].ToString(); }
                      else
                          itemcode += q[g].ToString() + '-';

                  }

                  sid = q[q.Length - 1].ToString();

                  q = null;
                  q = sid.Split('.');
                  string fix = q[0];

                  if (Regex.IsMatch(fix, @"^\d+$"))
                  {
                      if (Convert.ToInt32(fix) > 20)
                      {
                          dt.Rows[0]["商品番号"] = itemcode;
                          dt.Rows[0]["画像名"] = Imgname;
                          dt.Rows[0]["Error_Msg"] = Imgname + " image name incorrect !";
                      }
                      else
                      {
                          dt.Rows[0]["商品番号"] = itemcode;
                          dt.Rows[0]["画像名"] = Imgname;
                          dt.Rows[0]["SN"] = q[0];

                      }

                  }
              }//check length
              else
              {
                  dt.Rows[0]["商品番号"] = DBNull.Value;
                  dt.Rows[0]["画像名"] = Imgname;
                  dt.Rows[0]["Error_Msg"] = Imgname + " image name length incorrect !";

              }//length

              return dt;
          }//correct img
          else
          {
              if (Imgname.Contains('.'))
              {
                  string sid = string.Empty;
                  q = Imgname.Split('.');
                  string fix = q[0];
                  dt.Rows[0]["商品番号"] = fix;
                  dt.Rows[0]["画像名"] = Imgname;
                  dt.Rows[0]["Error_Msg"] = Imgname + " image name incorrect !";
              }
              else
              {
                  dt.Rows[0]["画像名"] = Imgname;
                  dt.Rows[0]["Error_Msg"] = Imgname + " image name incorrect !";
              }
              return dt;
          }

      }
      private static bool check(String str)
      {
          if (str.Contains(".jpg"))
              return true;
          else
              return false;
      }
      public static void FileMove(String ftpPath, String fileName)
      {
          //Export to Finish path

          FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpPath + "NG/" + fileName);
          ftp.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
          ftp.KeepAlive = true;
          ftp.UseBinary = true;
          ftp.Method = WebRequestMethods.Ftp.UploadFile;
          FileStream fs = File.OpenRead(localPath + fileName);
          //FileStream fs = File.OpenRead(ftpPath);
          byte[] buffer = new byte[fs.Length];
          fs.Read(buffer, 0, buffer.Length);
          fs.Close();
          Stream ftpstream = ftp.GetRequestStream();
          ftpstream.Write(buffer, 0, buffer.Length);
          ftpstream.Close();
          ftpstream.Dispose();
          //Delete
          FtpWebRequest ftpRequestDelete = (FtpWebRequest)WebRequest.Create(ftpPath + fileName);
          ftpRequestDelete.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
          ftpRequestDelete.Method = WebRequestMethods.Ftp.DeleteFile;

          FtpWebResponse response = (FtpWebResponse)ftpRequestDelete.GetResponse();
          response.Close();
      }

      public static void ImageInsert(DataTable dt)
      {
          dt.TableName = "test";
          System.IO.StringWriter writer = new System.IO.StringWriter();
          dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
          string result = writer.ToString();

          try
          {
              SqlConnection connectionString = GetConnection();
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "SP_Import_Item_Image_XML";

              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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
      public static void LogInsert(DataTable dt)
      {
          dt.TableName = "test";
          System.IO.StringWriter writer = new System.IO.StringWriter();
          dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
          string result = writer.ToString();

          try
          {
              SqlConnection connectionString = GetConnection();
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "SP_Item_Image_Log_XML";

              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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
      public static SqlConnection GetConnection()
      {
          String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

          SqlConnection connection = new SqlConnection(connectionString);
          return connection;
      }
      public static int CheckItem_Code(String itemcode)
      {
          SqlConnection con = GetConnection();
          SqlCommand cmd = new SqlCommand("SP_Import_CheckItem_Code", con);
          try
          {
              int count = 0;
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("@itemcode", itemcode);
              cmd.CommandTimeout = 0;
              cmd.Connection.Open();
              string st = Convert.ToString(cmd.ExecuteScalar());
              if (!String.IsNullOrWhiteSpace(st))
                  count = Convert.ToInt32(st);
              cmd.Connection.Close();
              return count;
          }
          catch (Exception ex)
          { throw ex; }
      }
      public static void ReadData()
      {
          startime = DateTime.Now;

          dterror.Columns.Add("商品番号", typeof(System.String));
          dterror.Columns.Add("画像名", typeof(System.String));
          DataColumn newcol = new DataColumn("Item_ID", typeof(Int32));
          newcol.DefaultValue = 0;
          dterror.Columns.Add(newcol);
          dterror.Columns.Add("Error_Msg", typeof(System.String));
          dterror.Columns.Add("SN", typeof(System.Int32));
          dterror.Columns.Add("LogID", typeof(Int32));
          DataRow destRow = dterror.NewRow();
          dterror.Rows.Add(destRow);


          FtpWebRequest ftpRequest;
          ftpRequest = (FtpWebRequest)WebRequest.Create(ftpPath);

          ftpRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
          ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
          FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
          StreamReader streamReader = new StreamReader(ftpResponse.GetResponseStream());
          String[] filelist = streamReader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
          Array.Sort(filelist);


          if (filelist.Length > 1)
          {

              rcount = filelist.Length;
              LogID = ItemLogInsert(0, rcount, ecount);
              rcount = 0;
              for (int j = 0; j < filelist.Length; j++)
              {

                  exetime = DateTime.Now;
                  //  TimeSpan? variable = null;
                  TimeSpan varTime = (DateTime)startime - (DateTime)exetime;
                  string longtime = Convert.ToString(varTime);
                  t = longtime.Split(':');
                  extime = Int32.Parse(t[1]);

                  if (Math.Abs(extime) < 26)
                  {
                      if (check(filelist[j]))
                      {
                          if (filelist[j].ToString() != "." && filelist[j].ToString() != "..")
                          {
                              string imgname = filelist[j].ToString();
                              if (checkImageformat(imgname) == true)
                              {
                                  getItemcode(imgname, dterror);//Get Item_Code
                                  int ItemID = CheckItem_Code(dterror.Rows[0]["商品番号"].ToString());
                                  String paths = localPath + Path.GetFileName(filelist[j]);
                                  if (File.Exists(paths))
                                  {
                                      File.Delete(paths);
                                  }//if delete

                                  String path = Downloadfile(filelist[j]);


                                  if (ItemID != 0)
                                  {
                                      dterror.Rows[0]["Item_ID"] = ItemID;
                                      //Delete img

                                      FtpWebRequest ftpRequestDelete = (FtpWebRequest)WebRequest.Create(ftpPath + imgname);
                                      ftpRequestDelete.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                                      ftpRequestDelete.Method = WebRequestMethods.Ftp.DeleteFile;

                                      FtpWebResponse response = (FtpWebResponse)ftpRequestDelete.GetResponse();
                                      response.Close();

                                      ImageInsert(dterror);//Insert Data
                                      dterror.Rows[0]["Error_Msg"] = DBNull.Value;
                                      dterror.Rows[0]["LogID"] = LogID;
                                      LogInsert(dterror);
                                      rcount += 1;
                                  }//checkitemcode
                                  else //if not exists item_code
                                  {

                                      dterror.Rows[0]["Error_Msg"] = imgname + "Item_Code does not exists!";
                                      dterror.Rows[0]["LogID"] = LogID;
                                      LogInsert(dterror);
                                      ecount += 1;
                                      FileMove(ftpPath, filelist[j]);
                                      File.Delete(path);
                                      rcount += 1;
                                  }
                              }//correct filename

                              else
                              {
                                  if (imgname != "NG")
                                  {
                                      if (imgname.Contains("."))
                                      {
                                          ecount += 1;
                                          dterror.Rows[0]["LogID"] = LogID;
                                          String path = Downloadfile(filelist[j]);
                                          getItemcode(imgname, dterror);

                                          LogInsert(dterror);

                                          FileMove(ftpPath, filelist[j]);
                                          File.Delete(path);
                                          rcount += 1;
                                      }//if it is folder   
                                      else
                                      {
                                          ecount += 1;
                                          dterror.Rows[0]["LogID"] = LogID;
                                          dterror.Rows[0]["商品番号"] = imgname;
                                          dterror.Rows[0]["画像名"] = imgname;
                                          dterror.Rows[0]["Error_Msg"] = imgname + " is folder!";

                                          LogInsert(dterror);
                                          rcount += 1;
                                      }
                                  }
                              }


                          }//if


                      }//checkfilelistif
                      else
                      {
                          string imgname = filelist[j].ToString();
                          if (imgname != "NG" && imgname != "." && imgname != "..")
                          {
                              if (imgname.Contains("."))
                              {
                                  ecount += 1;
                                  dterror.Rows[0]["LogID"] = LogID;
                                  String path = Downloadfile(filelist[j]);
                                  dterror.Rows[0]["商品番号"] = imgname;
                                  dterror.Rows[0]["画像名"] = imgname;
                                  dterror.Rows[0]["Error_Msg"] = imgname + " image name incorrect !";

                                  LogInsert(dterror);

                                  FileMove(ftpPath, filelist[j]);
                                  File.Delete(path);
                                  rcount += 1;
                              }//if it is folder
                              else
                              {
                                  ecount += 1;
                                  dterror.Rows[0]["LogID"] = LogID;
                                  dterror.Rows[0]["商品番号"] = imgname;
                                  dterror.Rows[0]["画像名"] = imgname;
                                  dterror.Rows[0]["Error_Msg"] = imgname + " is folder!";

                                  LogInsert(dterror);
                                  rcount += 1;
                              }
                          }

                      }
                  }//if time
                  else
                  {

                      break;
                  }
              }//if condition

              if (rcount > 0)
              {
                  ItemLogInsert(LogID, rcount, ecount);
              }
              else
              {
                  ItemLogDelete(LogID);
              }

          }//for loop

      }//main

      public static String Downloadfile(string filename)
      {
          try
          {

              FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(ftpPath + filename);
              requestFileDownload.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
              requestFileDownload.KeepAlive = false;
              requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;
              FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();
              //filename = filename.Replace("sks_image/", "");
              Stream responseStream = responseFileDownload.GetResponseStream();

              FileStream writeStream = new FileStream(localPath + filename, FileMode.Create, FileAccess.ReadWrite);

              int Length = 2048;
              Byte[] buffer = new Byte[Length];
              int bytesRead = responseStream.Read(buffer, 0, Length);

              while (bytesRead > 0)
              {
                  writeStream.Write(buffer, 0, bytesRead);
                  bytesRead = responseStream.Read(buffer, 0, Length);
              }

              responseStream.Close();
              writeStream.Close();
              String path = localPath + Path.GetFileName(filename);

              return path;
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

      public static int ItemLogInsert(int IID, int reccount, int errCount)
      {
          SqlConnection connectionString = GetConnection();
          SqlCommand cmd = new SqlCommand();
          try
          {
              cmd.CommandText = "SP_Item_Import_Log_InsertUpdate";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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
          SqlConnection connectionString = GetConnection();
          SqlCommand cmd = new SqlCommand();
          try
          {
              cmd.CommandText = "Delete FROM Item_ImportLog Where ID =" + IID;
              cmd.CommandType = CommandType.Text;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;



              cmd.Connection.Open();
              cmd.ExecuteNonQuery();
              cmd.Connection.Close();



          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }

      }
    }
}
