using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Globalization;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Net.Mail;
using System.Xml;
using System.Collections;

namespace Wowma_API_Inventory
{
    public class Program
    {

        //  static string WowmaAPIKey = ConfigurationManager.AppSettings["WowmaAPIKey"].ToString();
        static string WowmaShopID = ConfigurationManager.AppSettings["WowmaShopID"].ToString();
        static String ConsoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        static string apiKey = "";
        static void Main(string[] args)
        {
            try
            {

                Export();
            }
            catch (Exception ex)
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "SKS Wowma Inventory" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public static void Export()
        {
            try
            {
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");

                string list = GetItem_IDList();// 27-05-2015
                                               //   string list = "405867,405940,405941,405964,405866";// "406626,405964";
                DataTable dtAll = GetInvData("Select", list);
                DataTable dtShop = GetShopInfo();
                apiKey = dtShop.Rows[0]["APIKey"].ToString();
                int shop_id = int.Parse(dtShop.Rows[0]["ID"].ToString());
                string shopname = Convert.ToString(dtShop.Rows[0]["Shop_ID"].ToString());


                //for wowma by MieMie 13-09-2018
                #region wowma_quantity
                DataRow[] drw = dtAll.Select("ID =" + shop_id);
                if (drw.Count() > 0)
                {
                    DataTable dtWowma = dtAll.Select("ID =" + shop_id).CopyToDataTable();

                    if (dtWowma.Rows.Count > 0)
                    {
                        dtWowma = RemoveDuplicateRows(dtWowma, "code");
                        do
                        {
                            DataTable dtTemp = dtWowma.Rows.Cast<DataRow>().Take(100).CopyToDataTable();
                            DataTable dtInventory = dtAll.Select("ID =" + shop_id).CopyToDataTable();
                            if (dtInventory.Rows.Count > 0)
                            {
                                UploadWowmaInventoryToAPI(dtTemp, dtInventory, shopname, shop_id);
                            }
                            int count = 0;
                            while (count < 100)
                            {
                                if (dtWowma.Rows.Count > 0)
                                    dtWowma.Rows.RemoveAt(0);
                                else break;
                                count++;
                            }
                        } while (dtWowma.Rows.Count > 0);

                    }
                    ChangeFlag(list); //27-05-2015
                }

                #endregion


            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public static void UploadWowmaInventoryToAPI(DataTable dt, DataTable dtA, string shopname, int shop_id)
        {
            try
            {
                string xmlString = GetXMl_NewUpdate(dt, dtA, shopname, shop_id);
                //string apiKey = WowmaAPIKey;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://api.manager.wowma.jp/wmshopapi/updateStock"));
                byte[] bytes;
                bytes = System.Text.Encoding.UTF8.GetBytes(xmlString);
                request.ContentType = "application/xml; charset=utf-8";
                request.ContentLength = bytes.Length;
                request.Method = "POST";

                request.Headers.Add("Authorization", "Bearer " + apiKey);

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);

                requestStream.Close();
                HttpWebResponse response;

                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(responseStr);
                    responseStr = xDoc.InnerText;
                    XmlNodeList list1 = xDoc.GetElementsByTagName("status");
                    if (list1[0].InnerText.Equals("1"))
                    {
                        list1 = xDoc.GetElementsByTagName("updateResult");

                        foreach (XmlNode xn in list1)
                        {
                            string errmsg = "";
                            string code = xn.ChildNodes[0].InnerText;
                            ConsoleWriteLine_Tofile("Code : " + code, shop_id);
                            if (xn.ChildNodes.Count > 1)
                            {
                                errmsg = xn.ChildNodes[1].InnerText;
                                ConsoleWriteLine_Tofile("ErrMsg : " + errmsg, shop_id);
                                //  SaveInventoryErrorLog(code, errmsg, 26, DateTime.Now);
                            }

                        }
                    }
                    ConsoleWriteLine_Tofile(responseStr, shop_id);
                }
                else
                {
                    ConsoleWriteLine_Tofile("Result : Not OK", shop_id);
                }
            }
            catch (WebException e)
            {
                using (WebResponse response1 = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response1;
                    using (Stream data = response1.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        if (text.Contains("message"))
                        {
                            XmlDocument xd = new XmlDocument();
                            xd.LoadXml(text);
                            XmlNodeList list1 = xd.GetElementsByTagName("status");

                            if (list1[0].InnerText.Equals("1"))
                            {
                                list1 = xd.GetElementsByTagName("updateResult");
                                if (list1.Count <= 0)
                                {
                                    string msg = xd.InnerText;
                                    SaveInventoryErrorLog("", msg, 4, DateTime.Now);
                                }
                                else
                                {
                                    foreach (XmlNode xn in list1)
                                    {
                                        string errmsg = "";
                                        string code = xn.ChildNodes[0].InnerText;
                                        if (xn.ChildNodes.Count > 1)
                                        {
                                            errmsg = xn.ChildNodes[1].InnerText;
                                            SaveInventoryErrorLog(code, errmsg, 4, DateTime.Now);
                                        }

                                    }
                                }
                            }
                        }
                        ConsoleWriteLine_Tofile("Result : " + text, 4);
                    }
                }
            }
        }

      

        private static string GetXMl_NewUpdate(DataTable dtWowInventory, DataTable dtA, string shopname, int shop_id)
        {
            string wowmaShopID = WowmaShopID;

            MemoryStream memoryStream = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = Encoding.UTF8;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;
            XmlWriter writer = XmlWriter.Create(memoryStream, xmlWriterSettings);

            #region Inventory
            writer.WriteStartElement("request");
            writer.WriteElementString("shopId", wowmaShopID);



            foreach (DataRow ItemCodeWow in dtWowInventory.Rows)
            {
                string Wcode = "";
                Wcode = ItemCodeWow["code"].ToString();

                if (!String.IsNullOrWhiteSpace(Wcode))
                {
                    DataTable dtSKU = dtA.Select("code ='" + Wcode + "'").CopyToDataTable();
                    writer.WriteStartElement("stockUpdateItem");
                    writer.WriteElementString("itemCode", dtSKU.Rows[0]["商品管理番号（商品URL）"].ToString());//商品番号
                    writer.WriteElementString("saleStatus", dtSKU.Rows[0]["Warehouse_Specified"].ToString());//倉庫指定

                    if (!string.IsNullOrWhiteSpace(dtSKU.Rows[0]["Wo_Stock_Type"].ToString()))
                    {
                        if (dtSKU.Rows[0]["Wo_Stock_Type"].ToString() == "1")
                        {
                            writer.WriteElementString("stockSegment", dtSKU.Rows[0]["Wo_Stock_Type"].ToString());//在庫タイプ
                            writer.WriteElementString("stockCount", dtSKU.Rows[0]["項目選択肢別在庫用在庫数"].ToString());//在庫数
                        }
                        else
                        {
                            writer.WriteElementString("stockSegment", dtSKU.Rows[0]["Wo_Stock_Type"].ToString());//在庫タイプ
                            for (int i = 0; i < dtSKU.Rows.Count; i++)
                            {
                                writer.WriteStartElement("choicesStocks");
                                writer.WriteElementString("choicesStockHorizontalCode", dtSKU.Rows[i]["項目選択肢別在庫用横軸選択肢子番号"].ToString());//項目選択肢別在庫用横軸選択肢子番号
                                writer.WriteElementString("choicesStockVerticalCode", dtSKU.Rows[i]["項目選択肢別在庫用縦軸選択肢子番号"].ToString());//項目選択肢別在庫用縦軸選択肢子番号
                                if (Convert.ToInt32(dtSKU.Rows[i]["項目選択肢別在庫用在庫数"]) >= 1000)
                                {
                                    writer.WriteElementString("choicesStockCount", "999");
                                }
                                else
                                {
                                    writer.WriteElementString("choicesStockCount", dtSKU.Rows[i]["項目選択肢別在庫用在庫数"].ToString());
                                }

                                writer.WriteEndElement();
                            }
                            //writer.WriteElementString("choicesStockUpperDescription", "在庫下説明");
                            //writer.WriteElementString("choicesStockLowerDescription", "在庫下説明");
                            //writer.WriteElementString("displayChoicesStockSegment", "1");
                            //writer.WriteElementString("displayChoicesStockThreshold", "");
                            //writer.WriteElementString("displayBackorderMessage", "Message");

                        }
                    }
                }
            }
            #endregion

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            return Encoding.UTF8.GetString(memoryStream.ToArray());

        }

        static DataTable RemoveDuplicateRows(DataTable table, string DistinctColumn)
        {
            try
            {
                ArrayList UniqueRecords = new ArrayList();
                ArrayList DuplicateRecords = new ArrayList();

                // Check if records is already added to UniqueRecords otherwise,
                // Add the records to DuplicateRecords
                foreach (DataRow dRow in table.Rows)
                {
                    if (UniqueRecords.Contains(dRow[DistinctColumn]))
                        DuplicateRecords.Add(dRow);
                    else
                        UniqueRecords.Add(dRow[DistinctColumn]);
                }

                // Remove duplicate rows from DataTable added to DuplicateRecords
                foreach (DataRow dRow in DuplicateRecords)
                {
                    table.Rows.Remove(dRow);
                }

                // Return the clean DataTable which contains unique records.
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        static string GetItem_IDList()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = GetConnection();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_SelectNotUploadItem_Wowma", con);    //sp name changed
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt.Rows[0]["ID"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void ChangeFlag(string changeItemlist) //27-05-2015
        {
            try
            {
                SqlConnection connection = GetConnection();
                SqlCommand cmd = new SqlCommand("SP_ChangeItem_Master_IsGenerateFlag", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_Master_ID_List", changeItemlist);
                cmd.Parameters.AddWithValue("@Option", 1);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static DataTable GetShopInfo()
        {
            try
            {
                string quary = "SP_Shop_SelectWowma ";
                DataTable dt = new DataTable();
                SqlConnection connection = GetConnection();
                SqlDataAdapter sda = new SqlDataAdapter(quary, connection);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
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

        public static DataTable GetInvData(string option, string list)//added by KTA
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = GetConnection();
                using (SqlDataAdapter da = new SqlDataAdapter("SP_Select_InventoryData_Wowma", con))   //sp name changed
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.CommandTimeout = 0;
                    da.SelectCommand.Parameters.AddWithValue("@Option", option);
                    da.SelectCommand.Parameters.AddWithValue("@ID", list);
                    con.Open();
                    da.Fill(dt);
                    con.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveInventoryErrorLog(string itemcode, string filename, int shopID, DateTime Created_Date)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("SP_SaveInventoryErrorfile", GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@itemcode", itemcode);
                cmd.Parameters.AddWithValue("@filename", filename);
                cmd.Parameters.AddWithValue("@Shop_ID", shopID);
                cmd.Parameters.AddWithValue("@Created_Date", Created_Date);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static SqlConnection GetConnection()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        private static string QuoteValue(string value)
        {
            return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
        }


        static void ConsoleWriteLine_Tofile(string traceText, int shop_id)
        {
            StreamWriter sw = new StreamWriter(ConsoleWriteLinePath + "DailyDelivery_Set_ConsoleWriteLine" + shop_id + ".txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            String date = DateTime.Now.ToString();
            Console.WriteLine("Shop ID:" + shop_id + " " + date);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
    }
}
