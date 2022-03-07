/*
Created By              :Kyaw Thet Paing
Created Date          :
Updated By             :Aye Aye Mon
Updated Date         :01/09/2015

Using Stored Procedures :SP_SelectDataForAPICheck
                                            SP_ChangeCtrlFromAPI
                                            SP_Change_Flag
                                            SP_Save_ImportMallTable
                                            SP_Delete_ImportMallTable
                                            SP_Item_Export_ErrorCheck_InsertUpdate
 * 

Comment : Check Item Code exist or not in mall
*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using ORS_RCM_BL;
using ORS_RCM_Common;
using System.Xml;
using System.IO;
using System.Text;
using System.Threading;

using System.Text.RegularExpressions;
using System.Net;




namespace API_Check
{ 
    public class Program
    {
        static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        static string rakuten = ConfigurationManager.AppSettings["rakuten"].ToString();
        static int APICheckFlag;

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "API Check";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                Console.WriteLine("Get Item Codes!");
                DataTable dt = GetItemCodes();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Mall_ID"].ToString() == "1")
                        {
                            GetItemInformation(dr["APISecret"].ToString(), dr["APIKey"].ToString(), dr["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dr["Item_Code"].ToString());
                            ChangeCtrlID(dr["Item_Code"].ToString(), dr["Shop_ID"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "Api check" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public static void ConsoleWriteLine_Tofile(string traceText)
        {
            string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "API_Check_Log.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        public static string GetItemInformation(string Secret, string Key, string Item_ID, int shop_ID, string item_code)
        {
            try
            {
                string check = ""; //added by aam for SKS-320
                var webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://api.rms.rakuten.co.jp/es/1.0/item/get?itemUrl={0}", item_code.Trim()));
                webRequest.Method = "GET";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(Secret + ":" + Key));
                webRequest.Headers.Add("Authorization", "ESA " + encoded);
                WebResponse webRes = webRequest.GetResponse();
                XmlDocument resultXml = new XmlDocument();
                using (StreamReader reader = new StreamReader(webRes.GetResponseStream()))
                {
                    resultXml.Load(reader);
                }
                //結果XML中の[word]タグのリストを取得する
                XmlNodeList wordList = resultXml.GetElementsByTagName("message");
                ConsoleWriteLine_Tofile(wordList[0].InnerText);
                if (wordList[0].InnerText != "OK")
                {
                    return string.Format("NG:{0}", item_code);
                }
                #region item exists check
                //結果XML中の[word]タグのリストを取得する
                wordList = resultXml.GetElementsByTagName("code");
                ConsoleWriteLine_Tofile(wordList[0].InnerText);
                //Item exists
                if (wordList[0].InnerText == "N000")
                {
                    XmlNodeList wordListSKU = resultXml.GetElementsByTagName("optionNameHorizontal");
                    if (wordListSKU.Count > 0)
                    {
                        XmlNode noteSKU = wordListSKU[0];
                        String sku = noteSKU.InnerText;
                        if (!String.IsNullOrWhiteSpace(sku))
                        {
                            ChangeAPI(Convert.ToInt32(Item_ID), Convert.ToInt32(shop_ID), 2);
                        }
                        else
                            APICheckFlag = Check_Item_SKU(Convert.ToInt32(Item_ID), Convert.ToInt32(shop_ID));// added by ETZ for SKS-420
                    }
                    else
                    {
                        int itemadmincode = CheckSKUExistNotExists(Convert.ToInt32(Item_ID));
                        if (itemadmincode != 0)
                        {
                            Check_Item_SKU(Convert.ToInt32(Item_ID), Convert.ToInt32(shop_ID));// added by ETZ for SKS-420
                        }
                        else
                        {
                            ChangeAPI(Convert.ToInt32(Item_ID), Convert.ToInt32(shop_ID), 2);
                        }
                    }
                    ConsoleWriteLine_Tofile("item exists");
                    var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    string sql = "";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    sql = string.Format(@"
                            MERGE INTO Import_ShopItem_Master AS A
                                USING (SELECT {0} AS Shop_ID,'{1}' AS Item_Code) AS B
                                ON
                                (
                                    A.Shop_ID = B.Shop_ID AND A.Item_Code = B.Item_Code
                                )
                                WHEN MATCHED THEN
                                    UPDATE SET
                                            Created_Date = GETDATE()
                                WHEN NOT MATCHED THEN
                                    INSERT (Shop_ID,Item_Code,Created_Date)
                                    VALUES
                                    (
                                         B.Shop_ID
                                        ,B.Item_Code
                                        ,GETDATE()
                                    )
                            ;", shop_ID, item_code);
                    cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                //Item not exists
                else if (wordList[0].InnerText == "C001")
                {
                    check = wordList[0].InnerText; //added by aam for SKS-320
                    ConsoleWriteLine_Tofile("item not exists");
                    var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    string sql = "";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    sql = string.Format("DELETE FROM Import_ShopItem_Master WHERE Shop_ID = {0} AND Item_Code = '{1}'", shop_ID, item_code);
                    cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    ChangeAPI(Convert.ToInt32(Item_ID), Convert.ToInt32(shop_ID), 1);

                }
                #endregion

                #region Option
                //結果XML中の[word]タグのリストを取得する
                wordList = resultXml.GetElementsByTagName("option");
                string optionName = "";
                string optionValue = "";
                var conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                string sql2 = "";
                SqlCommand cmd2 = new SqlCommand(sql2, conn2);
                sql2 = string.Format("DELETE FROM Import_ShopItem_Inventory WHERE Choice_Type='s' AND Shop_ID={0} AND Item_Code='{1}';", shop_ID, item_code);
                ConsoleWriteLine_Tofile(sql2);
                cmd2 = new SqlCommand(sql2, conn2);
                conn2.Open();
                cmd2.ExecuteNonQuery();
                conn2.Close();
                //[word]以下のノードに含まれる内容をコンソールに出力する
                foreach (XmlNode wordNode in wordList)
                {
                    foreach (XmlNode resultNode in wordNode.ChildNodes)
                    {
                        if (resultNode.Name == "optionName")
                        {
                            optionName = resultNode.InnerText;
                        }
                        else if (resultNode.Name == "optionValues")
                        {
                            string pattern = @"<value>(.*?)</value>";
                            string input = resultNode.InnerXml;
                            MatchCollection matchCollection = Regex.Matches(input, pattern);
                            List<string> optionValues = new List<string>();
                            foreach (Match match in matchCollection)
                            {
                                //optionValues.Add(match.Groups[1].Value);
                                optionValue = match.Groups[1].Value;
                                ConsoleWriteLine_Tofile(optionName);
                                ConsoleWriteLine_Tofile(optionValue);
                                var conn3 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                                String sql3 = string.Format("INSERT INTO Import_ShopItem_Inventory (Shop_ID, Item_Code, Choice_Type, Item_Name, Item_Choice, Created_Date) VALUES({0}, N'{1}', 's', N'{2}', N'{3}', N'{4}');", shop_ID, item_code, optionName, optionValue, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                SqlCommand cmd3 = new SqlCommand(sql3, conn3);
                                conn3.Open();
                                cmd3.ExecuteNonQuery();
                                conn3.Close();
                            }
                        }
                    }
                }
                // Thread.Sleep(1000);
                #endregion

                #region Category
                wordList = resultXml.GetElementsByTagName("categoryInfo");
                var conn4 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                string sql4 = "";
                SqlCommand cmd4 = new SqlCommand(sql4, conn4);
                sql4 = string.Format("DELETE FROM Import_ShopItem_Category WHERE Shop_ID={0} AND Item_Code='{1}';", shop_ID, item_code);
                ConsoleWriteLine_Tofile(sql4);
                cmd4 = new SqlCommand(sql4, conn4);
                conn4.Open();
                cmd4.ExecuteNonQuery();
                conn4.Close();
                string itemName = string.Empty;
                XmlNodeList wordList1 = resultXml.GetElementsByTagName("itemName");
                if (wordList1.Count > 0)
                {
                    XmlNode note = wordList1[0];
                    itemName = note.InnerText;
                }
                string categoryno = string.Empty;
                string categorySetNo = string.Empty;
                foreach (XmlNode wordNode in wordList)
                {
                    foreach (XmlNode resultNode in wordNode.ChildNodes)
                    {
                        if (resultNode.Name == "categorySetManageNumber")
                        {
                            categorySetNo = resultNode.InnerText;
                        }
                        else if (resultNode.Name == "categoryId")
                        {
                            categoryno = resultNode.InnerText;
                            var conn5 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                            SqlCommand cmd5 = new SqlCommand("select Shop_ID from Shop Where ID=" + shop_ID, conn5);
                            SqlDataAdapter da = new SqlDataAdapter(cmd5);
                            DataTable dt = new DataTable();
                            cmd5.Connection.Open();
                            da.Fill(dt);
                            cmd5.Connection.Close();
                            String shopUrl = dt.Rows[0]["Shop_ID"].ToString();
                            using (WebClient client = new WebClient())
                            using (Stream stream = client.OpenRead("http://item.rakuten.co.jp/" + shopUrl + "/c/" + GenerateCategoryID(categoryno)))
                            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("EUC-JP")))
                            {
                                while (!reader.EndOfStream)
                                {
                                    String str = reader.ReadLine();
                                    if (str.Contains("content=\"楽天市場:"))
                                    {
                                        String catData = String.Empty;
                                        str = reader.ReadLine();
                                        while (!str.Contains("一覧。"))
                                        {
                                            catData += str.Replace("&nbsp;", "");
                                            str = reader.ReadLine();
                                        }
                                        str = str.Replace("一覧。", "$");
                                        string[] cat = str.Split('$');
                                        catData += cat[0];
                                        catData = catData.Replace(">", "\\");
                                        itemName = itemName.Replace("'", "''");
                                        if (categorySetNo != "etc")
                                        {
                                            var conn6 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                                            String sql6 = string.Format("INSERT INTO Import_ShopItem_Category (Shop_ID, Item_Code, Item_Name, Category_No,Category_Name, Created_Date) VALUES({0}, N'{1}', N'{2}', {3}, N'{4}','{5}');",
                                            shop_ID, item_code, itemName, categorySetNo, catData, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                            SqlCommand cmd6 = new SqlCommand(sql6, conn6);
                                            conn6.Open();
                                            cmd6.ExecuteNonQuery();
                                            conn6.Close();
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region SKU
                //結果XML中の[word]タグのリストを取得する
                wordList = resultXml.GetElementsByTagName("inventory");
                string quantity = string.Empty;
                string size_code = string.Empty;
                string color_code = string.Empty;
                string size_name = string.Empty;
                string color_name = string.Empty;
                var conn10 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                string sql10 = "";
                SqlCommand cmd10 = new SqlCommand(sql10, conn10);
                sql10 = string.Format("DELETE FROM Import_ShopItem_Inventory WHERE Choice_Type='i' AND Shop_ID={0} AND Item_Code='{1}';", shop_ID, item_code);
                ConsoleWriteLine_Tofile(sql10);
                cmd10 = new SqlCommand(sql10, conn10);
                conn10.Open();
                cmd10.ExecuteNonQuery();
                conn10.Close();
                //[word]以下のノードに含まれる内容をコンソールに出力する
                foreach (XmlNode wordNode in wordList)
                {
                    foreach (XmlNode resultNode in wordNode.ChildNodes)
                    {
                        if (resultNode.Name == "inventoryCount")
                        {
                            quantity = resultNode.InnerText;
                        }
                        if (resultNode.Name == "childNoVertical")
                        {
                            color_code = resultNode.InnerText;
                        }
                        if (resultNode.Name == "childNoHorizontal")
                        {
                            size_code = resultNode.InnerText;
                        }
                        if (resultNode.Name == "optionNameVertical")
                        {
                            color_name = resultNode.InnerText;
                        }
                        if (resultNode.Name == "optionNameHorizontal")
                        {
                            size_name = resultNode.InnerText;
                        }
                    }
                    var conn3 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    String sql3 = string.Format("Insert INTO Import_ShopItem_Inventory (Shop_ID, Item_Code, Choice_Type, Item_X,Item_XNo,Item_Y, Item_YNo,Item_StockNo, Created_Date) VALUES({0}, N'{1}', 'i', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}');", shop_ID, item_code, size_name, size_code, color_name, color_code, quantity, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    SqlCommand cmd3 = new SqlCommand(sql3, conn3);
                    conn3.Open();
                    cmd3.ExecuteNonQuery();
                    conn3.Close();
                }
                #endregion

                XmlNodeList wordList2 = resultXml.GetElementsByTagName("itemPrice");
                int price = 0;
                if (wordList2.Count > 0)
                    if (!string.IsNullOrWhiteSpace(wordList2[0].InnerText))
                        price = int.Parse(wordList2[0].InnerText);
                    else price = 0;
                XmlNodeList wordList3 = resultXml.GetElementsByTagName("isIncludedPostage");
                int postage = 3;
                if (wordList3.Count > 0)
                    if (!string.IsNullOrWhiteSpace(wordList3[0].InnerText) && wordList3[0].InnerText == "true")
                        postage = 1;
                    else
                        postage = 0;
                if (check != "C001") // Do not check for Delete Exhibition
                {
                    MallCheckCount(price, postage, item_code, shop_ID, int.Parse(Item_ID));
                }
                ConsoleWriteLine_Tofile(string.Format("Progress:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                return "done";
            }
            catch (WebException e)
            {
                throw e;
            }
        }

        public static String GenerateCategoryID(String categoryID)
        {
            string result = categoryID;
            for (int i = 0; i < 10 - categoryID.Length; i++)
            {
                result = "0" + result;
            }
            return result;
        }

        public static void ChangeCtrlID(string itemCode, string shopID)
        {
            {
                SqlCommand cmd = new SqlCommand("SP_ChangeCtrlFromAPI", GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("Item_Code", itemCode);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        private static void ChangeAPI(int Item_ID, int Shop_ID, int status)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Change_Flag", GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", Item_ID);
                cmd.Parameters.AddWithValue("@Shop_ID", Shop_ID);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void MallCheckCount(int price, int postage, string item_code, int shop_id, int e_item_id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_API_Check_Count", GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Postage", postage);
                cmd.Parameters.AddWithValue("@Item_Code", item_code.Trim());
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Parameters.AddWithValue("@Exhibition_ID", e_item_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataTable GetItemCodes()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SP_SelectDataForAPICheck", GetConnection());
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

        private static int Check_Item_SKU(int itemid, int shopid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ItemAndSKU_Check", GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ShopID", shopid);
                cmd.Parameters.AddWithValue("@ItemID", itemid);
                cmd.Parameters.AddWithValue("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int apicheckflag = Convert.ToInt32(cmd.Parameters["@result"].Value);
                return apicheckflag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int CheckSKUExistNotExists(int itemid)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("SP_Check_ColorandSize", GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ItemID", itemid);
                cmd.Parameters.AddWithValue("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int admincode = Convert.ToInt32(cmd.Parameters["@result"].Value);
                return admincode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                return connection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
