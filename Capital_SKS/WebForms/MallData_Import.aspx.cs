using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace ORS_RCM.WebForms
{
    public partial class MallData_Import : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string Download(String ItemCode,Boolean Racket,Boolean LuckPiece,Boolean SportPlaza,Boolean BaseballPlaza)
        {
            try
            {
                ItemCode=ItemCode.Replace("\t","");
                if (Racket)
                {
                    string[] code = ItemCode.Split(',');
                    for (int i = 0; i < code.Length; i++)
                    {
                        string itemCode = code[i].Trim();
                        DataTable dt = GetData(itemCode, 1);
                        if (dt.Rows.Count > 0)
                        { 
                            string apiKey = dt.Rows[0]["APIKey"].ToString();
                            string apiSecret = dt.Rows[0]["APISecret"].ToString();
                            string shopID = dt.Rows[0]["ShopID"].ToString();
                            string shopUrl = dt.Rows[0]["ShopUrl"].ToString();
                            string itemID = dt.Rows[0]["ID"].ToString();

                            SaveDataFromMall(apiKey, apiSecret, shopID, shopUrl, itemID, itemCode);
                        }
                    }
                }
                if (LuckPiece)
                {
                    string[] code = ItemCode.Split(',');
                    for (int i = 0; i < code.Length; i++)
                    {
                        string itemCode = code[i].Trim();
                        DataTable dt = GetData(itemCode, 5);
                        if (dt.Rows.Count > 0)
                        { 
                            string apiKey = dt.Rows[0]["APIKey"].ToString();
                            string apiSecret = dt.Rows[0]["APISecret"].ToString();
                            string shopID = dt.Rows[0]["ShopID"].ToString();
                            string shopUrl = dt.Rows[0]["ShopUrl"].ToString();
                            string itemID = dt.Rows[0]["ID"].ToString();

                            SaveDataFromMall(apiKey, apiSecret, shopID, shopUrl, itemID, itemCode);
                        }
                    }
                }
                if (SportPlaza)
                {
                    string[] code = ItemCode.Split(',');
                    for (int i = 0; i < code.Length; i++)
                    {
                        string itemCode = code[i].Trim();
                        DataTable dt = GetData(itemCode, 8);
                        if (dt.Rows.Count > 0)
                        { 
                            string apiKey = dt.Rows[0]["APIKey"].ToString();
                            string apiSecret = dt.Rows[0]["APISecret"].ToString();
                            string shopID = dt.Rows[0]["ShopID"].ToString();
                            string shopUrl = dt.Rows[0]["ShopUrl"].ToString();
                            string itemID = dt.Rows[0]["ID"].ToString();
                            SaveDataFromMall(apiKey, apiSecret, shopID, shopUrl, itemID, itemCode);
                            ChangeCtrlID(itemCode, shopID);
                        }                      
                    }
                }
                if (BaseballPlaza)
                {
                    string[] code = ItemCode.Split(',');
                    for (int i = 0; i < code.Length; i++)
                    {
                        string itemCode = code[i].Trim();
                        DataTable dt = GetData(itemCode, 12);
                        if(dt.Rows.Count>0)
                        {
                            string apiKey = dt.Rows[0]["APIKey"].ToString();
                            string apiSecret = dt.Rows[0]["APISecret"].ToString();
                            string shopID = dt.Rows[0]["ShopID"].ToString();
                            string shopUrl = dt.Rows[0]["ShopUrl"].ToString();
                            string itemID = dt.Rows[0]["ID"].ToString();

                            SaveDataFromMall(apiKey, apiSecret, shopID, shopUrl, itemID, itemCode);
                        }
                    }
                }
                return "true";
            }
            catch (Exception ex)
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "ShopID=12" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return "false";
            }
        }

        public static void ChangeCtrlID(string itemCode, string shopID)
        {
            if (shopID.Equals("8"))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_ChangeCtrlFromAPI", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("Item_Code", itemCode);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public static Boolean SaveDataFromMall(string apiKey,string apiSecret,string shopID,string shopUrl,string itemID,string itemCode)
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://api.rms.rakuten.co.jp/es/1.0/item/get?itemUrl={0}", itemCode));
                webRequest.Method = "GET";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(apiSecret + ":" + apiKey));
                webRequest.Headers.Add("Authorization", "ESA " + encoded);
                WebResponse webRes = webRequest.GetResponse();

                XmlDocument resultXml = new XmlDocument();

                using (StreamReader reader = new StreamReader(webRes.GetResponseStream()))
                {
                    resultXml.Load(reader);
                }

                XmlNodeList wordList = resultXml.GetElementsByTagName("message");

                if (wordList[0].InnerText != "OK")
                {
                    return false;
                }

                wordList = resultXml.GetElementsByTagName("code");
                //Item Exists
                if (wordList[0].InnerText == "N000")
                {
                    InsertItemMaster(shopID,itemCode);
                }
                //Item Not Exists
                else if (wordList[0].InnerText == "C001")
                {
                    DeleteItemMaster(shopID,itemCode);
                }

                wordList = resultXml.GetElementsByTagName("option");
                GetOption(wordList,shopID,itemCode);

                wordList = resultXml.GetElementsByTagName("categoryInfo");
                GetCategory(wordList, shopID, itemCode, shopUrl,resultXml);

                return true;
            }
            catch
            {
                return false;
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
        
        public static void GetCategory(XmlNodeList wordList,string shopID,string ItemCode,string shopUrl,XmlDocument resultXml)
        {
            var conDel = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            string sqlDel = "";
            SqlCommand cmdDel = new SqlCommand(sqlDel, conDel);
            sqlDel = string.Format("DELETE FROM Import_ShopItem_Category WHERE Shop_ID={0} AND Item_Code='{1}';", shopID, ItemCode);
            cmdDel = new SqlCommand(sqlDel, conDel);
            conDel.Open();
            cmdDel.ExecuteNonQuery();
            conDel.Close();

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
                                    var conInsert = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                                    String sqlInsert = string.Format("INSERT INTO Import_ShopItem_Category (Shop_ID, Item_Code, Item_Name, Category_No,Category_Name, Created_Date) VALUES({0}, N'{1}', N'{2}', {3}, N'{4}','{5}');", shopID, ItemCode, itemName, categorySetNo, catData, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    SqlCommand cmdInsert = new SqlCommand(sqlInsert, conInsert);
                                    conInsert.Open();
                                    cmdInsert.ExecuteNonQuery();
                                    conInsert.Close();

                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void GetOption(XmlNodeList wordList,string shopID,string itemCode)
        {
            var condelete = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            string sqldelete = "";
            SqlCommand cmddelete = new SqlCommand(sqldelete, condelete);
            sqldelete = string.Format("DELETE FROM Import_ShopItem_Inventory WHERE Choice_Type='s' AND Shop_ID={0} AND Item_Code='{1}';", shopID, itemCode);
            cmddelete = new SqlCommand(sqldelete, condelete);
            condelete.Open();
            cmddelete.ExecuteNonQuery();
            condelete.Close();

            string optionName = "";
            string optionValue = "";
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

                            var conInsert = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                            String sqlInsert = string.Format("INSERT INTO Import_ShopItem_Inventory (Shop_ID, Item_Code, Choice_Type, Item_Name, Item_Choice, Created_Date) VALUES({0}, N'{1}', 's', N'{2}', N'{3}', N'{4}');", shopID, itemCode, optionName, optionValue, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            SqlCommand cmdInsert = new SqlCommand(sqlInsert, conInsert);
                            conInsert.Open();
                            cmdInsert.ExecuteNonQuery();
                            conInsert.Close();
                        }
                    }
                }
            }

        }

        public static void DeleteItemMaster(string shopID,string itemCode)
        {
            try
            {
                var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                string sql = "";
                SqlCommand cmd = new SqlCommand(sql, conn);
                sql = string.Format("DELETE FROM Import_ShopItem_Master WHERE Shop_ID = {0} AND Item_Code = '{1}'", shopID, itemCode);
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertItemMaster(string shopID,string itemCode)
        {
            try
            {
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
                    ;", shopID, itemCode);
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetData(String ItemCode,int ShopID)
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand("SELECT S.APIKey,S.APISecret,S.ID AS ShopID,S.Shop_ID AS ShopUrl,IM.Item_Code,IM.ID from Shop S,Item_Master IM WHERE IM.Item_Code ='"+ItemCode+"' AND S.ID ="+ShopID,con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();

            return dt;
        }
    }
}