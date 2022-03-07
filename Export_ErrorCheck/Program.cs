using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net;
using ORS_RCM;
using Limilabs.FTP.Client;

namespace Export_ErrorCheck
{
    class Program
    {
        public static String localPath = ConfigurationManager.AppSettings["ErrorCSV"].ToString();
        public static String csv = String.Empty;
        public static String ftpHost;
        public static String ftpAccount;
        public static String ftpPassword;
        public static String fileType = "0";
        public static void Main(string[] args)
        {
            try
            {
                Console.Title = "Export Error Check";
                CreateDir(localPath);
                DataTable dt = GetShop();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String shopid = dt.Rows[i]["ID"].ToString();
                    ftpHost = dt.Rows[i]["FTP_Host"].ToString();
                    ftpAccount = dt.Rows[i]["FTP_Account"].ToString();
                    ftpPassword = dt.Rows[i]["FTP_Password"].ToString();
                    //ftpHost = "ftp://153.120.170.144//Item/Rakuten//Racket_Plaza/";
                    //ftpAccount = "test_ftp";
                    //ftpPassword = "ldf9t5i82@dfg5jfsd";
                    String MallID = dt.Rows[i]["Mall_ID"].ToString();
                    if (MallID.Equals("1") || MallID.Equals("3"))
                    {
                        ftpHost = dt.Rows[i]["ErrorLog_URL"].ToString();
                        //ftpHost = "ftp://153.120.170.144//Item/Rakuten//Racket_Plaza/";
                    }

                    if (!String.IsNullOrWhiteSpace(ftpHost))
                    {
                        String[] filelist = new String[100];
                        if (MallID.Equals("1"))
                        {
                            FtpWebRequest ftpRequest;
                            //ftpRequest = (FtpWebRequest)WebRequest.Create(ftpHost);
                            ftpRequest = (FtpWebRequest)WebRequest.Create(ftpHost);
                            ftpRequest.Credentials = new NetworkCredential(ftpAccount, ftpPassword);
                            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                            StreamReader streamReader = new StreamReader(ftpResponse.GetResponseStream());
                            filelist = streamReader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                            for (int j = 0; j < filelist.Length; j++)
                            {
                                if (check(filelist[j], MallID))
                                {
                                    FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(ftpHost + filelist[j]);
                                    requestFileDownload.Credentials = new NetworkCredential(ftpAccount, ftpPassword);
                                    requestFileDownload.KeepAlive = false;
                                    requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;
                                    FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();
                                    DateTime createdDate = responseFileDownload.LastModified;
                                    Stream responseStream = responseFileDownload.GetResponseStream();
                                    FileStream writeStream = new FileStream(localPath + filelist[j], FileMode.Create);

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

                                    String path = localPath + Path.GetFileName(filelist[j]);
                                    csv = Path.GetFileName(filelist[j]);


                                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpHost + filelist[j]);
                                    request.Credentials = new NetworkCredential(ftpAccount, ftpPassword);
                                    request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                                    DateTime date = response.LastModified;


                                    //DateTime createdDate = File.GetCreationTime();
                                    //String csv = Path.GetFileName(filelist[2]);

                                    DataTable dtItem = GlobalUI.CSVToTable(path);
                                    //String option = String.Empty;
                                    //if (filelist[j].Contains("item"))
                                    //    option = "1";
                                    //else if (filelist[j].Contains("select"))
                                    //    option = "2";
                                    //else if (filelist[j].Contains("item-cat"))
                                    //    option = "3";
                                    Rakuten(dtItem, shopid, date);
                                  
                                    FileMove();
                                }
                            }
                        }

                        else if (MallID.Equals("3"))
                        {
                            using (Ftp client = new Ftp())
                            {
                                client.Connect(ftpHost);
                                client.AuthTLS();
                                client.Login(ftpAccount, ftpPassword);
                                RemoteSearchOptions options = new RemoteSearchOptions("*.csv", true);
                                client.DownloadFiles(localPath, options);

                                List<FtpItem> items = client.GetList();

                                if (items.Count > 0)
                                {
                                    DataTable dtOrder = new DataTable();
                                    dtOrder.Columns.Add("OrderNo", typeof(int));
                                    dtOrder.Columns.Add("File_Name", typeof(string));
                                    for (int y = 0; y < items.Count; y++)
                                    {
                                        if (items[y].ToString().Contains("item_"))
                                        {
                                            dtOrder.Rows.Add(1, items[y].ToString());
                                            dtOrder.NewRow();
                                        }
                                        else if (items[y].ToString().Contains("option_"))
                                        {
                                            dtOrder.Rows.Add(2, items[y].ToString());
                                            dtOrder.NewRow();
                                        }
                                        else if (items[y].ToString().Contains("category_"))
                                        {
                                            dtOrder.Rows.Add(3, items[y].ToString());
                                            dtOrder.NewRow();
                                        }
                                    }
                                    dtOrder.DefaultView.Sort = "OrderNo ASC";
                                    dtOrder = dtOrder.DefaultView.ToTable(true);

                                    //for (int l = 0; l < items.Count; l++)
                                    for (int l = 0; l < dtOrder.Rows.Count; l++)
                                    {
                                        //if (check(items[l].ToString(), MallID))
                                        if (check(dtOrder.Rows[l]["File_Name"].ToString(), MallID))
                                        {
                                            //String csvpath = localPath + items[l];
                                            String csvpath = localPath + dtOrder.Rows[l]["File_Name"].ToString();

                                            //String path = localPath + Path.GetFileName(items[l].ToString());
                                            String path = localPath + Path.GetFileName(dtOrder.Rows[l]["File_Name"].ToString());
                                            //csv = Path.GetFileName(items[l].ToString());
                                            csv = Path.GetFileName(dtOrder.Rows[l]["File_Name"].ToString());
                                            //String csv = Path.GetFileName(filelist[2]);

                                            //DateTime date = client.GetFileModificationTime(items[l].ToString());
                                            DateTime date = client.GetFileModificationTime(dtOrder.Rows[l]["File_Name"].ToString());
                                            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpHost + items[l]);
                                            //request.Credentials = new NetworkCredential(ftpAccount, ftpPassword);
                                            //request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                                            //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                                            //DateTime date = response.LastModified;
                                            DataTable dtItem = GlobalUI.CSVToTable(path);
                                            Ponpare(dtItem, shopid, date);
                                            
                                            //client.DeleteFile(items[l].ToString());
                                            client.DeleteFile(dtOrder.Rows[l]["File_Name"].ToString());

                                            //Move File
                                            //FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpHost + items[l].ToString());
                                            //ftp.Credentials = new NetworkCredential(ftpAccount, ftpPassword);
                                            //ftp.KeepAlive = true;
                                            //ftp.UseBinary = true;
                                            //ftp.Method = WebRequestMethods.Ftp.UploadFile;
                                            ////FileStream fs = File.OpenRead(localPath + items[l].ToString());
                                            //FileStream fs = File.OpenRead(localPath + dtOrder.Rows[l]["File_Name"].ToString());
                                            //byte[] buffer = new byte[fs.Length];
                                            //fs.Read(buffer, 0, buffer.Length);
                                            //fs.Close();
                                            //Stream ftpstream = ftp.GetRequestStream();
                                            //ftpstream.Write(buffer, 0, buffer.Length);
                                            //ftpstream.Close();
                                        }
                                    }
                                    //client.DeleteFiles(options);
                                    client.Close();
                                    //FileMove();
                                }
                            }
                        }
                    }

                    CheckErrorExists(shopid);
                    //CheckErrorExists();
                }
            }
            catch (Exception ex)
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "ErrorCheck Error:" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        private static void CheckErrorExists(string shopid)
        {
            try
            {
                SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand("SP_Check_Error_Exists", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ShopID",Convert.ToInt32(shopid));
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataTable checkShop(String shopid)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter("SP_Rakutan_Item_SelectAll ", GetConnection());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@shopID", shopid);
            da.SelectCommand.Connection.Open();
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            return dt;
        }

        private static bool check(String str, String mallID)
        {
            if (str.Contains(".csv"))
            {
                str = str.Replace(".csv", "");
                if (mallID.Equals("1"))
                {
                    if (str.Contains("item."))
                    {
                        fileType = "1";
                        return true;
                    }
                    if (str.Contains("select"))
                    {
                        fileType = "2";
                        return true;
                    }
                    if (str.Contains("item-cat"))
                    {
                        fileType = "3";
                        return true;
                    }
                    return false;
                }
                else if (mallID.Equals("3"))
                {
                    if (str.Contains("item_"))
                    {
                        fileType = "1";
                        return true;
                    }
                    if (str.Contains("option_"))
                    {
                        fileType = "2";
                        return true;
                    }
                    if (str.Contains("category_"))
                    {
                        fileType = "3";
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        public static DataTable GetShop()
        {
            DataTable dt = new DataTable();
          //  SqlDataAdapter da = new SqlDataAdapter("Select * from Shop Where ID=1", GetConnection());
            SqlDataAdapter da = new SqlDataAdapter("Select * from Shop Where Mall_ID=1 OR Mall_ID=3", GetConnection());
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Connection.Open();
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            return dt;
        }

        public static void CreateDir(String path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }


        public static void Rakuten(DataTable dt, String shopid, DateTime date)
        {
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dt.Columns[0].ColumnName;
                dr[1] = dt.Columns[1].ColumnName;
              
                dt.Columns[0].ColumnName = "Item_Code";
                dt.Columns[1].ColumnName = "Description";
                if (dt.Columns.Count > 2)
                {
                    if (dt.Columns[2].ColumnName.Contains("対象商品"))
                    {
                        dr[2] = dt.Columns[2].ColumnName;
                        dt.Columns[2].ColumnName = "DeliveryError";
                    }
                }
                dt.Rows.InsertAt(dr, 0);

                //dt = ConcatRakutenDescription(dt);
                DataColumn dc = new DataColumn("ShopID", typeof(Int32));
                dc.DefaultValue = shopid;
                dt.Columns.Add(dc);

                dc = new DataColumn("FtpDate", typeof(DateTime));
                dc.DefaultValue = date;
                dt.Columns.Add(dc);

                DataColumn dc1 = new DataColumn("FileType", typeof(int));
                dc1.DefaultValue = fileType;
                dt.Columns.Add(dc1);

                String xml1 = DatatableToXML(dt);

                InsertItemExportError(xml1);
            }

        }

        public static void Ponpare(DataTable dt, String shopid, DateTime date)
        {
            if (dt.Rows.Count > 0)
            {
                //DataColumn dc = new DataColumn("ShopID", typeof(Int32));
                //dc.DefaultValue = shopid;
                //dt.Columns.Add(dc);

                //DataTable dtOK = SelectOkValue(dt);
                //String xml = String.Empty;
                //if (dtOK != null)
                //{ 
                //    dtOK.Columns[0].ColumnName = "Description";
                //    dtOK.Columns[1].ColumnName = "Item_Code";
                //    xml = DatatableToXML(dtOK);
                //    changExportErrorFlag(xml);

                //    dt = RemoveOkItem(dt);
                //}
                //dt = ConcatPonpareDescription(dt);
                //xml = DatatableToXML(dt);
                //InsertItemExportError(xml);



                for (int i = 0; i < dt.Rows.Count; )
                {
                    if (dt.Rows[i][0].ToString().Contains("OK"))
                    {
                        dt.Rows.RemoveAt(i);
                    }
                    else i++;
                }
                String xml = String.Empty;
                dt.Columns[0].ColumnName = "Description";
                dt.Columns[1].ColumnName = "Item_Code";
                //dt = ConcatPonpareDescription(dt);
                DataColumn dc = new DataColumn("ShopID", typeof(Int32));
                dc.DefaultValue = shopid;
                dt.Columns.Add(dc);


                dc = new DataColumn("FtpDate", typeof(DateTime));
                dc.DefaultValue = date;
                dt.Columns.Add(dc);

                DataColumn dc1 = new DataColumn("FileType", typeof(int));
                dc1.DefaultValue = fileType;
                dt.Columns.Add(dc1);

                if (dt.Columns.Contains("選択肢タイプ"))
                    dt.Columns.Remove("選択肢タイプ");

                if (dt.Columns.Contains("購入オプション名"))
                    dt.Columns.Remove("購入オプション名");

                if (dt.Columns.Contains("オプション項目名"))
                    dt.Columns.Remove("オプション項目名");

                if (dt.Columns.Contains("SKU横軸項目ID"))
                    dt.Columns.Remove("SKU横軸項目ID");

                if (dt.Columns.Contains("SKU横軸項目名"))
                    dt.Columns.Remove("SKU横軸項目名");

                if (dt.Columns.Contains("SKU縦軸項目ID"))
                    dt.Columns.Remove("SKU縦軸項目ID");

                if (dt.Columns.Contains("SKU縦軸項目名"))
                    dt.Columns.Remove("SKU縦軸項目名");

                if (dt.Columns.Contains("SKU在庫数"))
                    dt.Columns.Remove("SKU在庫数");

                xml = DatatableToXML(dt);
                InsertItemExportError(xml);
            }
        }

        public static DataTable SelectOkValue(DataTable dt)
        {
            String ok = "OK";
            DataRow[] dr = dt.Select("コントロールカラム like  '%" + ok + "%'");
            if (dr.Count() > 0)
            {
                dt = dt.Select("コントロールカラム like  '%" + ok + "%'").CopyToDataTable();
                return dt;
            }
            else return null;
        }

        public static DataTable RemoveOkItem(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; )
            {
                if (dt.Columns.Contains("コントロールカラム"))
                {
                    if (dt.Rows[i]["コントロールカラム"].ToString().Contains("OK"))
                        dt.Rows.RemoveAt(i);
                    else i++;
                }
                else if (dt.Columns.Contains("Description"))
                {
                    if (dt.Rows[i]["Description"].ToString().Contains("OK"))
                        dt.Rows.RemoveAt(i);
                    else i++;
                }
            }
            return dt;
        }

        public static void DeleteRakutenBackup()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Rakutan_Item_Backup_Delete", GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void BackupXmlInsert(string xml)
        {
            try
            {
                SqlConnection connection = GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Rakutan_Item_Backup_Insert_XML";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteRakutenData(String shopID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM  Import_ShopItem WHERE Shop_ID=" + shopID, GetConnection());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void changExportErrorFlag(String xml)
        {
            try
            {
                SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 0;
                cmd.CommandText = "SP_Item_Export_ErrorCheck";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertItemExportError(String xml)
        {
            try
            {
                //xml = xml.Replace("商品管理番号（商品URL）欄にすでに登録済みのものは指定できません。重複がありましたのでご確認ください。,項目選択肢別在庫が指定されています。倉庫指定欄で倉庫に入れる指定がない場合でも登録完了時に項目選択肢別在庫(select.csv)が正しく設定されていない間は倉庫に入った状態となり、商品ページにアクセスすることはできない可能性があります。", "");
                xml = xml.Replace("+09:00", "");
                SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 0;
                cmd.CommandText = "SP_Item_Export_ErrorCheck";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ConcatPonpareDescription(DataTable dt)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("Item_Code", typeof(String));
            dtResult.Columns.Add("Description", typeof(String));
            int j = 0;
            do
            {
                String Item_Code = dt.Rows[0]["商品管理ID（商品URL）"].ToString();
                if (!String.IsNullOrEmpty(Item_Code))
                {
                    DataTable dtSpecID = dt.Select("商品管理ID（商品URL）='" + Item_Code + "'").CopyToDataTable();
                    //dtResult.Rows.Add();
                    //dtResult.Rows[j]["Item_Code"] = dtSpecID.Rows[0]["商品管理ID（商品URL）"].ToString();
                    //dtResult.Rows[j]["Description"] = dtSpecID.Rows[0]["コントロールカラム"].ToString();

                    for (int i = 0; i < dtSpecID.Rows.Count; i++)
                    {
                        if (dtSpecID.Rows[i]["コントロールカラム"].ToString().Contains("商品管理番号（商品URL）欄にすでに登録済みのものは指定できません。重複がありましたのでご確認ください。")
                           || dtSpecID.Rows[i]["コントロールカラム"].ToString().Contains("項目選択肢別在庫が指定されています。倉庫指定欄で倉庫に入れる指定がない場合でも登録完了時に項目選択肢別在庫(select.csv)が正しく設定されていない間は倉庫に入った状態となり、商品ページにアクセスすることはできない可能性があります。")
                           )
                        { }
                        else
                        {
                            dtResult.Rows.Add();
                            dtResult.Rows[j]["Item_Code"] = dtSpecID.Rows[i]["商品管理ID（商品URL）"].ToString();
                            if (!String.IsNullOrWhiteSpace(dtResult.Rows[j]["Description"].ToString()))
                                dtResult.Rows[j]["Description"] = dtResult.Rows[j]["Description"] + "," + dtSpecID.Rows[i]["コントロールカラム"].ToString();
                            else
                                dtResult.Rows[j]["Description"] = dtSpecID.Rows[i]["コントロールカラム"].ToString();
                        }
                    }

                    j++;
                }

                for (int i = 0; i < dt.Rows.Count; )
                {
                    if (dt.Rows[i]["商品管理ID（商品URL）"].Equals(Item_Code))
                        dt.Rows.RemoveAt(i);
                    else i++;
                }
            } while (dt.Rows.Count != 0);

            return dtResult;
        }

        public static DataTable ConcatRakutenDescription(DataTable dt)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("Item_Code", typeof(String));
            dtResult.Columns.Add("Description", typeof(String));
            int j = 0;
            do
            {
                String Item_Code = dt.Rows[0]["Item_Code"].ToString();
                if (!String.IsNullOrEmpty(Item_Code))
                {
                    DataTable dtSpecID = dt.Select("Item_Code='" + Item_Code + "'").CopyToDataTable();

                    //dtResult.Rows[j]["Item_Code"] = dtSpecID.Rows[0]["Item_Code"].ToString();
                    //dtResult.Rows[j]["Description"] = dtSpecID.Rows[0]["Description"].ToString();

                    for (int i = 0; i < dtSpecID.Rows.Count; i++)
                    {
                        if (dtSpecID.Rows[i]["Description"].ToString().Contains("商品管理番号（商品URL）欄にすでに登録済みのものは指定できません。重複がありましたのでご確認ください。")
                           || dtSpecID.Rows[i]["Description"].ToString().Contains("項目選択肢別在庫が指定されています。倉庫指定欄で倉庫に入れる指定がない場合でも登録完了時に項目選択肢別在庫(select.csv)が正しく設定されていない間は倉庫に入った状態となり、商品ページにアクセスすることはできない可能性があります。")
                           )
                        { }
                        else
                        {
                            dtResult.Rows.Add();
                            dtResult.Rows[j]["Item_Code"] = dtSpecID.Rows[i]["Item_Code"].ToString();
                            if (!String.IsNullOrWhiteSpace(dtResult.Rows[j]["Description"].ToString()))
                                dtResult.Rows[j]["Description"] = dtResult.Rows[j]["Description"] + "," + dtSpecID.Rows[i]["Description"].ToString();
                            else
                                dtResult.Rows[j]["Description"] = dtSpecID.Rows[i]["Description"].ToString();
                        }
                    }
                    j++;
                }

                for (int i = 0; i < dt.Rows.Count; )
                {
                    if (dt.Rows[i]["Item_Code"].Equals(Item_Code))
                        dt.Rows.RemoveAt(i);
                    else i++;
                }
            } while (dt.Rows.Count != 0);

            return dtResult;
        }

        public static String DatatableToXML(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            return result;
        }

        public static SqlConnection GetConnection()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }


        public static void FileMove()
        {
            //ExportToFtp(localPath + csv, csv);
            //String host = "ftp://test.capitalk-mm.com/";
            //String acc = "test@test.capitalk-mm.com";
            //String pass = "Sa7wdmxB";
            //Delete
            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpHost + "/TestRakutenImport/" + csv);
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpHost + csv);
            request.Credentials = new NetworkCredential(ftpAccount, ftpPassword);
            request.Method = WebRequestMethods.Ftp.DeleteFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
        }

        public static void ExportToFtp(string source, string fileName)
        {

            //String host = "ftp://test.capitalk-mm.com/";
            //String acc = "test@test.capitalk-mm.com";
            //String pass = "Sa7wdmxB";
            //FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpHost + "/Trash/" + fileName);
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpHost + fileName);
            ftp.Credentials = new NetworkCredential(ftpAccount, ftpPassword);

            ftp.KeepAlive = true;
            ftp.UseBinary = true;
            ftp.Method = WebRequestMethods.Ftp.UploadFile;

            FileStream fs = File.OpenRead(source);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();

            Stream ftpstream = ftp.GetRequestStream();
            ftpstream.Write(buffer, 0, buffer.Length);
            ftpstream.Close();
        }
    }
}
