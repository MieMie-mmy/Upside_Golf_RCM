using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Upside_Golf_RCM_BL;
using Upside_Golf_RCM_Common;
using Yahoo_API;

namespace Inventory_Update_Console
{
    public class Program
    {
        public static String fileName = String.Empty;
        public static String errLogPath = @"C:\Inventory Update\CSV_Upload\Error_Logs\";
        static string ExportInventoryCSVPath = ConfigurationManager.AppSettings["ExportInventoryCSVPath"].ToString();
        static string BakExportInventoryCSVPath = ConfigurationManager.AppSettings["BakExportInventoryCSVPath"].ToString();
        static string Rpath = ConfigurationManager.AppSettings["RPath"].ToString();
        static String ConsoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
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
                cmd.Parameters.AddWithValue("@ErrorDetail", "SKS Inventory" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public static void Export()
        {
            try
            {
                String dateTennis = DateTime.Now.ToString("yyyyMMddHHmmss");
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string filename = date + ".csv";
                string list = GetItem_IDList();
                DataTable dtAll = GetInvData("Select", list);
                DataTable dtTennis = GetInvDataTennis(list);
                DataTable dtShop = GetShop();
                if (dtShop != null && dtShop.Rows.Count > 0)
                {
                    for (int i = 0; i < dtShop.Rows.Count; i++)
                    {
                        int shop_id = int.Parse(dtShop.Rows[i]["ID"].ToString());
                        string shopname = Convert.ToString(dtShop.Rows[i]["Shop_ID"].ToString());
                        switch (dtShop.Rows[i]["Mall_ID"].ToString())  //Check Mall
                        {
                            case "1":
                                #region generate select.csv
                                string groupno = GetGroupNo();
                                DataRow[] dr = dtAll.Select("ID =" + shop_id + "and Item_AdminCode IS NULL OR Item_AdminCode=''");
                                if (dr.Count() > 0)
                                {
                                    DataTable dtRakutenitem = dtAll.Select("ID =" + shop_id + "and Item_AdminCode IS NULL OR Item_AdminCode=''").CopyToDataTable();
                                    DataTable dtItem = RColumn(dtRakutenitem);
                                    if (dtItem.Rows.Count > 0)
                                    {
                                        using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Rakuten/" + "item$" + shop_id + "_1_" + date + "_" + groupno + ".csv", false, Encoding.GetEncoding(932)))
                                        {
                                            WriteDataTable(dtItem, writer, true);
                                            SaveItem_ExportQ("item$" + shop_id + "_1_" + date + "_" + groupno + ".csv", 2, shop_id, 0, 3);
                                        }
                                        using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Rakuten/" + "item$" + shop_id + "_1_" + date + "_" + groupno + ".csv", false, Encoding.GetEncoding(932)))
                                        {
                                            WriteDataTable(dtItem, writer, true);
                                        }
                                    }
                                }
                                dr = dtAll.Select("ID =" + shop_id + "and Item_AdminCode IS NOT NULL OR Item_AdminCode<>''");
                                if (dr.Count() > 0)
                                {
                                    DataTable dtRakutenselect = dtAll.Select("ID =" + shop_id + "and Item_AdminCode IS NOT NULL").CopyToDataTable();
                                    DataTable dtSelect = RColumname(dtRakutenselect);
                                    if (dtSelect.Rows.Count > 0)
                                    {
                                        using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_1_" + date + "_" + groupno + ".csv", false, Encoding.GetEncoding(932)))
                                        {
                                            WriteDataTable(dtSelect, writer, true);
                                            SaveItem_ExportQ("select$" + shop_id + "_1_" + date + "_" + groupno + ".csv", 2, shop_id, 0, 3);
                                        }
                                        using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_1_" + date + "_" + groupno + ".csv", false, Encoding.GetEncoding(932)))
                                        {
                                            WriteDataTable(dtSelect, writer, true);
                                        }
                                    }
                                }
                                #endregion
                                break;
                            case "2":
                                #region quantity
                                DataRow[] drs = dtAll.Select("ID =" + shop_id);
                                if (drs.Count() > 0)
                                {
                                    string qtycode = "";
                                    DataTable dtYahoo = dtAll.Select("ID =" + shop_id).CopyToDataTable();
                                    #region ConsoleWriteLineTofile
                                    foreach (DataRow qtyitemcode in dtYahoo.Rows)
                                    {
                                        qtycode += qtyitemcode["code"].ToString() + "(" + qtyitemcode["sub-code"].ToString() + ")" + ",";
                                    }
                                    ConsoleWriteLine_Tofile("Item Code : " + qtycode, shop_id);
                                    #endregion
                                    if (dtYahoo.Rows.Count > 0)
                                    {
                                        UploadInventoryToAPI(dtYahoo, shopname, shop_id);
                                    }
                                }
                                #endregion
                                ChangeReflectionFlag(shopname);
                                break;
                            case "7":
                                string groupno8 = GetGroupNo();
                                #region Generate DetailSKU.csv
                                
                                if (dtTennis.Rows.Count > 0 && dtTennis != null)
                                {
                                    if (dtTennis.Rows.Count > 0)
                                    {
                                        using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/TennisClassic/" + "DetailSKU" + dateTennis + ".csv", false, Encoding.UTF8))
                                        {
                                            WriteDataTable_WithComma(dtTennis, writer, true);
                                            SaveItem_ExportQ("DetailSKU" + dateTennis + ".csv", 2, shop_id, 0, 3);
                                        }
                                        using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/TennisClassic/" + "DetailSKU" + dateTennis + ".csv", false, Encoding.UTF8))
                                        {
                                            WriteDataTable_WithComma(dtTennis, writer, true);
                                        }
                                    }
                                }
                                #endregion
                                break;
                        }
                    }
                }
                ChangeFlag(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static string ChangeReflectionFlag(string shopname)
        {
            try
            {
                YahooAPI yahooAPI = new YahooAPI();
                string access_token = yahooAPI.GetAccessToken();
                string postData = string.Format("seller_id={0}&mode={1}&reserve_time={2}", shopname, "1", "");
                var webRequest = (HttpWebRequest)WebRequest.Create("https://circus.shopping.yahooapis.jp/ShoppingWebService/V1/reservePublish");
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(yahooAPI.GetAppID() + ":" + yahooAPI.GetSecretKey()));
                webRequest.Headers.Add("Authorization", "Bearer " + access_token);
                using (var s = webRequest.GetRequestStream())
                using (var sw = new StreamWriter(s, new UTF8Encoding(false)))
                    sw.Write(postData.ToString());
                using (var webResponse = webRequest.GetResponse())
                {
                    var responseStream = webResponse.GetResponseStream();
                    if (responseStream == null)
                        return null;
                    using (var reader = new StreamReader(responseStream))
                    {
                        var response = reader.ReadToEnd();
                        XmlDocument xd = new XmlDocument();
                        xd.LoadXml(response);
                        response = xd.InnerText;
                        return response.ToString();
                    }
                }
            }
            catch (WebException e)
            {
                string error = "";
                if (e.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)e.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            error = reader.ReadToEnd();
                            //TODO: use JSON.net to parse this string and look at the error message
                            if (error.Contains("Error"))
                            {
                                XmlDocument xd = new XmlDocument();
                                xd.LoadXml(error);
                                error = xd.InnerText;
                            }
                        }
                    }
                }
                return error;
            }
        }

        public static void UploadInventoryToAPI(DataTable dtSelect, string shopname, int shopid)
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.AutoIncrement = true;
            column.ColumnName = "No";
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            DataTable dtcopy = new DataTable("table");
            dtcopy.Columns.Add(column);
            dtcopy.Merge(dtSelect);
            int er = dtcopy.Rows.Count;
            int sre = er / 1000;
            int de = er % 1000;
            if (de != 0)
            {
                sre = sre + 1;
            }
            int j = 1;
            for (int i = 1; i <= sre; i++)
            {
                DataTable dty = dtcopy.AsEnumerable().OrderBy(o => o.Field<int>("No")).Skip(j - 1).Take(1000).CopyToDataTable();
                j = j + 1000;
                YahooAPI yahooAPI = new YahooAPI();
                if (yahooAPI.YahooAPIAuth(Rpath).ToString() != "success")
                {
                    return;
                }
                string code = "", subcode = "", quantity = "";
                Dictionary<string, string> yahooItemRefInfo = new Dictionary<string, string>();
                foreach (DataRow quantitylist in dty.Rows)
                {
                    code += quantitylist["code"].ToString() + ",";
                    subcode += quantitylist["code"].ToString() + ":" + quantitylist["sub-code"].ToString() + ",";
                    quantity += quantitylist["quantity"].ToString() + ",";
                }
                subcode = subcode.TrimEnd(',');
                quantity = quantity.TrimEnd(',');
                yahooItemRefInfo["Item_code"] = subcode;
                yahooItemRefInfo["Quantity"] = quantity;
            inventorylabel:
                string result = UploadToInventoryYahooAPI(yahooItemRefInfo, shopname);
                if (result.Contains("反映またはアップロード中のため更新ができません。"))
                {
                    goto inventorylabel;
                }
                if (result.Contains("st-02999") || result.Contains("st-02000") || result.Contains("st-02100") || result.Contains("st-02101") || result.Contains("st-02102") || result.Contains("st-02103") || result.Contains("st-02104") || result.Contains("st-02105") || result.Contains("st-02106") || result.Contains("st-02107"))
                {
                    ConsoleWriteLine_Tofile("Error:" + result, shopid);
                }
                else
                {
                    ConsoleWriteLine_Tofile("Item_Code:" + subcode + "," + "Result:OK", shopid);
                }
            }
        }

        static string UploadToInventoryYahooAPI(Dictionary<string, string> yahooItemRefInfo, string shopname)
        {
            try
            {
                YahooAPI yahooAPI = new YahooAPI();
                string access_token = yahooAPI.GetAccessToken();
                string postData = string.Format("seller_id={0}&item_code={1}&quantity={2}",
                        shopname, yahooItemRefInfo["Item_code"].ToString(), yahooItemRefInfo["Quantity"].ToString());
                var webRequest = (HttpWebRequest)WebRequest.Create("https://circus.shopping.yahooapis.jp/ShoppingWebService/V1/setStock");
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(yahooAPI.GetAppID() + ":" + yahooAPI.GetSecretKey()));
                webRequest.Headers.Add("Authorization", "Bearer " + access_token);
                using (var s = webRequest.GetRequestStream())
                using (var sw = new StreamWriter(s, new UTF8Encoding(false)))
                    sw.Write(postData.ToString());
                using (var webResponse = webRequest.GetResponse())
                {
                    var responseStream = webResponse.GetResponseStream();
                    if (responseStream == null)
                        return null;
                    using (var reader = new StreamReader(responseStream))
                    {
                        var response = reader.ReadToEnd();
                        XmlDocument xd = new XmlDocument();
                        xd.LoadXml(response);
                        response = xd.InnerText;
                        return response.ToString();
                    }
                }
            }
            catch (WebException e)
            {
                string error = "";
                if (e.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)e.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            error = reader.ReadToEnd();
                            //TODO: use JSON.net to parse this string and look at the error message
                            if (error.Contains("Error"))
                            {
                                XmlDocument xd = new XmlDocument();
                                xd.LoadXml(error);
                                error = xd.InnerText;
                            }
                        }
                    }
                }
                return error;
            }
            finally
            {
                yahooItemRefInfo["Status"] = "NG";
            }
        }

        public static DataTable RColumn(DataTable dt)
        {
            dt.Columns.Remove("ID");
            dt.Columns.Remove("Mall_ID");
            dt.Columns.Remove("code");
            dt.Columns.Remove("sub-code");
            dt.Columns.Remove("quantity");
            dt.Columns.Remove("mode");
            dt.Columns.Remove("Item_AdminCode");
            dt.Columns.Remove("選択肢タイプ");
            dt.Columns.Remove("Select/Checkbox用項目名");
            dt.Columns.Remove("Select/Checkbox用選択肢");
            dt.Columns.Remove("項目選択肢別在庫用横軸選択肢");
            dt.Columns.Remove("項目選択肢別在庫用横軸選択肢子番号");
            dt.Columns.Remove("項目選択肢別在庫用縦軸選択肢");
            dt.Columns.Remove("項目選択肢別在庫用縦軸選択肢子番号");
            dt.Columns.Remove("項目選択肢別在庫用取り寄せ可能表示");
            dt.Columns.Remove("項目選択肢別在庫用在庫数");
            dt.Columns.Remove("在庫戻しフラグ");
            dt.Columns.Remove("在庫切れ時の注文受付");
            dt.Columns.Remove("在庫あり時納期管理番号");
            dt.Columns.Remove("在庫切れ時納期管理番号");
            dt.Columns.Remove("項目選択肢用コントロールカラム");
            dt.AcceptChanges();
            return dt;
        }

        public static DataTable RColumname(DataTable dt)
        {
            dt.Columns.Remove("ID");
            dt.Columns.Remove("Mall_ID");
            dt.Columns.Remove("code");
            dt.Columns.Remove("sub-code");
            dt.Columns.Remove("quantity");
            dt.Columns.Remove("mode");
            dt.Columns.Remove("Item_AdminCode");
            dt.Columns.Remove("商品番号");
            dt.Columns.Remove("商品名");
            dt.Columns.Remove("在庫数");
            dt.Columns.Remove("コントロールカラム");
            dt.AcceptChanges();
            return dt;
        }

        static string GetItem_IDList()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = GetConnection();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_SelectNotUploadItem", con);    //sp name changed
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
                cmd.Parameters.AddWithValue("@Option", 2);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetShop()
        {
            try
            {
                string quary = "SELECT ID,Mall_ID,Shop_ID FROM Shop order by Shop_ID";
                DataTable dt = new DataTable();
                SqlConnection connection = GetConnection();
                SqlDataAdapter sda = new SqlDataAdapter(quary, connection);
                sda.SelectCommand.CommandType = CommandType.Text;
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
                using (SqlDataAdapter da = new SqlDataAdapter("SP_Select_InventoryData", con))   //sp name changed
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

        private static void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                List<string> headerValues = new List<string>();
                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerValues.Add(column.ColumnName);
                }
                StringBuilder builder = new StringBuilder();
                writer.WriteLine(String.Join(",", headerValues.ToArray()));
            }
            string[] items = null;
            foreach (DataRow row in sourceTable.Rows)
            {
                items = row.ItemArray.Select(o => QuoteValue(o.ToString())).ToArray();
                writer.WriteLine(String.Join(",", items));
            }
            writer.Flush();
        }

        public static void SaveItem_ExportQ(string FileName, int FileType, int ShopID, int IsExport, int Export_Type)
        {
            Item_ExportQ_Entity ie = new Item_ExportQ_Entity();
            Item_ExportQ_BL ieBL = new Item_ExportQ_BL();
            ie.File_Name = FileName;
            ie.File_Type = FileType;
            ie.ShopID = ShopID;
            ie.IsExport = IsExport;
            ie.Export_Type = Export_Type;
            ieBL.Save(ie);
        }

        public static string GetGroupNo()
        {
            try
            {
                Item_Master_BL master = new Item_Master_BL();
                return master.GetGroupNo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ConsoleWriteLine_Tofile(string traceText, int shop_id)
        {
            StreamWriter sw = new StreamWriter(ConsoleWriteLinePath + "Inventory_ConsoleWriteLine" + shop_id + ".txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            String date = DateTime.Now.ToString();
            Console.WriteLine("Shop ID:" + shop_id + " " + date);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        //for Tennis Classic
        public static DataTable GetInvDataTennis(string list)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = GetConnection();
                using (SqlDataAdapter da = new SqlDataAdapter("SP_Select_InventoryDataTennis", con))   //sp name changed
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.CommandTimeout = 0;
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

        public static void WriteDataTable_WithComma(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                List<string> headerValues = new List<string>();

                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerValues.Add(column.ColumnName);

                }
                StringBuilder builder = new StringBuilder();
                writer.WriteLine(String.Join(",", headerValues.ToArray()));
            }

            string[] items = null;
            foreach (DataRow row in sourceTable.Rows)
            {
                items = row.ItemArray.Select(o => QuoteValue(o.ToString())).ToArray();
                writer.WriteLine(String.Join(",", items));
            }

            writer.Flush();
        }
    }
}
