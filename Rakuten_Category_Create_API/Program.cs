using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Net;
using System.Threading;
using System.Drawing;
using System.Collections;

namespace Rakuten_Category_Create_API
{
    public class Program
    {
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static DataTable dtCategory = new DataTable();
        static DataTable dtAPIKey = new DataTable();
        static string apiSecret = string.Empty;
        static string apiKey = string.Empty;
        static int ShopID;
        static String ErrorCode = String.Empty;
        static String SetNo = string.Empty;
        public static void Main(string[] args)
        {
            Console.Title = "Rakuten Category Create API";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                dtAPIKey = GetAPIKey();    //select ID,APIKey,APISecret from Shop table where ID=1
                for (int j = 0; j < dtAPIKey.Rows.Count; j++)
                {
                    apiSecret = dtAPIKey.Rows[j]["APISecret"].ToString();
                    apiKey = dtAPIKey.Rows[j]["APIKey"].ToString();
                    ShopID = Convert.ToInt32(dtAPIKey.Rows[j]["ID"].ToString());
                    dtCategory = GetNewCategoryList(ShopID);
                    CreateCategoryToMall(dtCategory);
                }
            }
            catch (Exception ex)
            {
                Save_SYS_Errorlog(ex.ToString());
            }
        }

        public static void CreateCategoryToMall(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dtCategorylist = NewDataTable();      //add new columns to datatable
                DataRow dr = dtCategorylist.NewRow();
                dr["ID"] = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                dr["Path"] = dt.Rows[i]["Path"].ToString();
                dr["Category_ID_New"] = "";
                dr["Description"] = dt.Rows[i]["Description"].ToString();
                dr["ParentID"] = dt.Rows[i]["ParentID"].ToString(); //dt.Rows[i]["ParentID"].ToString();
                dr["Rakutan_CategoryNo"] = dt.Rows[i]["Rakutan_CategoryNo"].ToString();
                dtCategorylist.Rows.Add(dr);
                dtCategorylist.AcceptChanges();

                DataTable dtParentCatID = GetParentCategoryToCreate(dt.Rows[i]["ParentID"].ToString()); 
                while (dtParentCatID.Rows.Count > 0 && String.IsNullOrWhiteSpace(dtParentCatID.Rows[0]["Painttool_Category_ID"].ToString()))
                {
                    DataRow dr1 = dtCategorylist.NewRow();
                    dr1["ID"] = Convert.ToInt32(dtParentCatID.Rows[i]["ID"].ToString());
                    dr1["Path"] = dtParentCatID.Rows[i]["Path"].ToString();
                    dr1["Category_ID_New"] = "";
                    dr1["Description"] = dtParentCatID.Rows[0]["Description"].ToString();
                    dr1["ParentID"] = dtParentCatID.Rows[0]["ParentID"].ToString();
                    dr1["Rakutan_CategoryNo"] = dtParentCatID.Rows[0]["Rakutan_CategoryNo"].ToString();
                    dtCategorylist.Rows.Add(dr1);
                    dtCategorylist.AcceptChanges();

                    dtParentCatID = GetParentCategoryToCreate(dtParentCatID.Rows[0]["ParentID"].ToString());
                }

                PrepareAndUpload(dtCategorylist);

            }
        }

        public static void PrepareAndUpload(DataTable dtCategorylist)
        {
            dtCategorylist.DefaultView.Sort = "ID ASC";
            dtCategorylist = dtCategorylist.DefaultView.ToTable();

            for (int i = 0; i < dtCategorylist.Rows.Count; i++)
            {
                DataTable dtCagetoryID = GetParentCategoryToCreate(dtCategorylist.Rows[i]["ParentID"].ToString());//get categoryID of parent

                #region Prepare XML
                MemoryStream memoryStream = new MemoryStream();
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Encoding = Encoding.UTF8;
                xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
                xmlWriterSettings.Indent = true;
                XmlWriter writer = XmlWriter.Create(memoryStream, xmlWriterSettings);
                writer.WriteStartElement("request");
                writer.WriteStartElement("categoryInsertRequest");
                writer.WriteElementString("categorySetManageNumber", dtCategorylist.Rows[i]["Rakutan_CategoryNo"].ToString());

                if (dtCagetoryID.Rows.Count > 0)
                    writer.WriteElementString("categoryId", dtCagetoryID.Rows[0]["Painttool_Category_ID"].ToString());
                else
                    writer.WriteElementString("categoryId", "0");
                writer.WriteStartElement("category");
                writer.WriteElementString("name", dtCategorylist.Rows[i]["Description"].ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();//categoryInsertRequest
                writer.WriteEndElement();//request

                writer.Flush();
                string xmlString = Encoding.UTF8.GetString(memoryStream.ToArray());
                #endregion

                string apiSecret = dtAPIKey.Rows[0]["APISecret"].ToString();
                string apiKey = dtAPIKey.Rows[0]["APIKey"].ToString();

                #region Uploate/Down CategoryID from Mall
            l1:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.rms.rakuten.co.jp/es/1.0/categoryapi/shop/category/insert");
                byte[] bytes;
                bytes = System.Text.Encoding.UTF8.GetBytes(xmlString);
                request.ContentType = "text/xml; encoding='utf-8'";
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                String encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiSecret + ":" + apiKey));
                request.Headers.Add("Authorization", "ESA " + encoded);
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                Thread.Sleep(2000);
                ConsoleWriteLine_Tofile("3. Sleep : 2s");
                HttpWebResponse response;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream responseStream = response.GetResponseStream();
                        string responseStr = new StreamReader(responseStream).ReadToEnd();
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.LoadXml(responseStr);
                        XmlNodeList list1 = xDoc.GetElementsByTagName("code");
                        ConsoleWriteLine_Tofile("4. ResponseString : " + responseStr);
                        if (list1[0].InnerText.Equals("C302"))
                        {
                            goto l1;
                            
                        }
                        else if (list1[0].InnerText.Equals("C220") && responseStr.Contains("同一カテゴリ内に同じ名前のサブカテゴリを作成することはできません"))
                        {
                            ErrorCode = "C220";
                            if (!SetNo.Contains(dtCategorylist.Rows[i]["Rakutan_CategoryNo"].ToString()))
                            {
                                SetNo += dtCategorylist.Rows[i]["Rakutan_CategoryNo"].ToString() + ",";
                            }
                        }
                        else if (list1[0].InnerText.Equals("N000"))
                        {
                            XmlNodeList list = xDoc.GetElementsByTagName("categoryId");
                            string str = list[0].InnerText;
                            ConsoleWriteLine_Tofile("5. Finished Create");
                            if (!string.IsNullOrWhiteSpace(str))
                            {

                                UpdateCategoryID(str, dtCategorylist.Rows[i]["Path"].ToString(), ShopID);//update created categoryID in Category table
                                UpdateFlag(dtCategorylist.Rows[i]["Path"].ToString(), 1, ShopID);
                                ConsoleWriteLine_Tofile("6. Update CategoryID");
                                ConsoleWriteLine_Tofile("Finished!!!");
                            }

                        }
                        else
                        {
                            string error = xDoc.InnerText.ToString();
                            UpdateFlag(dtCategorylist.Rows[i]["Path"].ToString(), 2, ShopID);
                            Save_SYS_Errorlog(error + " And " + dtCategorylist.Rows[i]["Path"].ToString() + "ShopID :" + ShopID);
                            ConsoleWriteLine_Tofile("5. Error :");

                        }
                    }
                }
                catch (WebException e)
                {
                    ConsoleWriteLine_Tofile("5. Catch Error :" + e.ToString());
                    UpdateFlag(dtCategorylist.Rows[i]["Path"].ToString(), 2, ShopID);
                    Save_SYS_Errorlog(e.ToString());
                }
                finally
                {
                }
                #endregion
                Thread.Sleep(1000);
            }
        }
        

        static void ChangeCategoryFlag(string itemcode)
        {

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_ChangeCategoryFlag", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Itemcode", itemcode);
                cmd.Parameters.AddWithValue("@Shop_ID", 1);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

          static void ConsoleWriteLine_Tofile(string traceText)
        {
            try
            {
                StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Rakuten_Category_Create_API_Log.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
                sw.AutoFlush = true;
                Console.SetOut(sw);
                Console.WriteLine(traceText);
                sw.Close();
                sw.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        static DataTable NewDataTable()
        {
            DataTable dtCategorylist = new DataTable();
            dtCategorylist.Columns.Add(new DataColumn("ID", typeof(int)));
            dtCategorylist.Columns.Add(new DataColumn("Path", typeof(string)));
            dtCategorylist.Columns.Add(new DataColumn("Category_ID_New", typeof(string)));
            dtCategorylist.Columns.Add(new DataColumn("Description", typeof(string)));
            dtCategorylist.Columns.Add(new DataColumn("ParentID", typeof(string)));
            dtCategorylist.Columns.Add(new DataColumn("Rakutan_CategoryNo", typeof(string)));
            dtCategorylist.AcceptChanges();
            return dtCategorylist;
        }

        static DataTable GetNewCategoryList(int ShopID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("GetNewCategory", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", ShopID);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static DataTable GetParentCategoryToCreate(string Parent_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetParentCategorytoCreate", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Parent_ID", Convert.ToInt32(Parent_ID));
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void UpdateFlag(string path, int option, int shopid)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_UpdateFlagNewCategoryLog", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@path", path);
                cmd.Parameters.AddWithValue("@option", option);
                cmd.Parameters.AddWithValue("@shopID", shopid);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Save_SYS_Errorlog(string error)
        {

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", -1);
            cmd.Parameters.AddWithValue("@ErrorDetail", "New_Category_Create_Rakuten : " + error);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        static void UpdateCategoryID(string categoryID, string path, int shopid)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_UpdateCategoryID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                cmd.Parameters.AddWithValue("@path", path);
                cmd.Parameters.AddWithValue("@shopID", shopid);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static DataTable GetAPIKey()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetShop", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
