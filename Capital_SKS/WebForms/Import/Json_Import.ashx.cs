using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
//using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Transactions;
using Newtonsoft.Json.Linq;
using System.Net;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Import
{
    /// <summary>
    /// Summary description for Json_Import
    /// </summary>
    public class Json_Import : IHttpHandler
    {
        Import_Item_BL import = new Import_Item_BL();

        public class Item
        {
            public string Item_AdminCode;
            public string Item_Code;
            public string Item_Name;
            public string List_Price;
            public string Sale_Price;
            public string Cost;
            public string Release_Date;
            public string Post_Available_Date;
            public string Year;
            public string Season;
            public string Brand_Code;
            public string Brand_Name;
            public string Brand_Code_Yahoo;
            public string Competition_Code;
            public string Competition_Name;
            public string Classification_Code;
            public string Class_Name;
            public string Company_Name;
            public string Catalog_Information;
            public string Special_Flag;
            public string Reservation_Flag;
            public string Instruction_No;
            public string Approve_Date;
            public string Remarks;
            public string Product_Code;
        }

        public class SKU
        {
            public string Item_AdminCode;
            public string Item_Code;
            public string Color_Name;
            public string Size_Name;
            public string Color_Code;
            public string Size_Code;
            public string Color_Name_Official;
            public string Size_Name_Official;
            public string JAN_Code;
            public string Rakuten_Size_Tag_Name;
            public string Rakuten_Color_Tag_Name;
            public string Rakuten_Shoe_Size_Tag_Name;
        }

        public class Inventory
        {
            public string Item_AdminCode;
            public string Item_Code;
            public string Quantity;
            public string Jisha_Quantity;
            public string Maker_Quantity;
            public string Toyonaka_Quantity;
            public string Ishibashi_Quantity;
            public string Esaka_Quantity;
            public string Sannomiya_Quantity;
        }


        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.QueryString["User"] != null && context.Request.QueryString["Password"] != null)
                {
                    LogInBL LogBL = new LogInBL();
                    String loginId = context.Request.QueryString["User"].ToString();
                    String pass = context.Request.QueryString["Password"].ToString();
                    DataTable dt = LogBL.logincheck(loginId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int userID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                        context.Response.ContentType = "text/plain";
                        //context.Response.Write("Hello World");

                        //string response = @"{""item"":[{""Item_AdminCode"":""794524;794525;794526;794527;794528;794531;794545;794510;794511;794512;794513;794514;794515;794516;794517;794518;794519;794520;794521;794522;794523;794532;794533;794534;794535;794536;794537;794538;794539;794541;794543;794544;794529;794530;794542;794540"",""Item_Code"":""ass-xss096"",""Item_Name"":""\u30b9\u30c8\u30c3\u30ad\u30f3\u30b0\uff0f\u30e6\u30cb\u30bb\u30c3\u30af\u30b9\uff08XSS096\uff09"",""List_Price"":1728,""Sale_Price"":1382,""Cost"":960,""Release_Date"":null,""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":23,""Brand_Name"":""\u30a2\u30b7\u30c3\u30af\u30b9"",""Brand_Code_Yahoo"":134,""Competition_Code"":150,""Competition_Name"":""\u30b5\u30c3\u30ab\u30fc"",""Classification_Code"":80,""Class_Name"":""\u30a6\u30a7\u30a2\uff08\u30e1\u30f3\u30ba\/\u30e6\u30cb\uff09"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015ass-fw-05-2-216 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-12-15 16:32:00"",""Remarks"":"",""Product_Code"":""XSS096""}]}";
                        //string response = @"{""item"":[{""Item_AdminCode"":""392068;248420;248421;248422;248423;248424;392065;392066;248419;460591;460592;460593;460589;460590;392067"",""Item_Code"":""pg938"",""Item_Name"":""\u30ec\u30c7\u30a3\u30fc\u30b9 \u7a74\u958b\u304dUV \u534a\u6307\u30b0\u30ed\u30fc\u30d6\uff08PG938\uff09"",""List_Price"":2052,""Sale_Price"":1641,""Cost"":1026,""Release_Date"":""2013-03-05"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""SS Q1"",""Brand_Code"":5,""Brand_Name"":""\u30d7\u30ea\u30f3\u30b9"",""Brand_Code_Yahoo"":4185,""Competition_Code"":10,""Competition_Name"":""\u30c6\u30cb\u30b9"",""Classification_Code"":130,""Class_Name"":""\u30a2\u30af\u30bb\u30b5\u30ea\u30fb\u5c0f\u7269"",""Company_Name"":""\uff78\uff9e\uff9b\uff70\uff8c\uff9e\uff97\uff72\uff84\uff9e(\u682a)"",""Catalog_Information"":""2015pri-ss-02-048 2014pri-ss-03-048 2013pri-fw-02-011 2013pri-ss-02-052 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-01-30 10:30:00"",""Remarks"":""2013pri-ss-02\u3088\u308a\u8272or\u30b5\u30a4\u30ba\u8ffd\u52a0"",""Product_Code"":""PG938""}],""sku"":[{""Item_AdminCode"":460590,""Item_Code"":""pg938"",""Size_Name"":""S"",""Color_Name"":""\uff08127\uff09\u30cd\u30a4\u30d3\u30fc"",""Size_Code"":""0001"",""Color_Code"":""0001"",""Size_Name_Official"":""S"",""Color_Name_Official"":""\uff08127\uff09\u30cd\u30a4\u30d3\u30fc"",""JAN_Code"":""4989723261515""},{""Item_AdminCode"":460593,""Item_Code"":""pg938"",""Size_Name"":""S"",""Color_Name"":""\uff08146\uff09\u30db\u30ef\u30a4\u30c8"",""Size_Code"":""0001"",""Color_Code"":""0002"",""Size_Name_Official"":""S"",""Color_Name_Official"":""\uff08146\uff09\u30db\u30ef\u30a4\u30c8"",""JAN_Code"":""4989723261553""},{""Item_AdminCode"":460589,""Item_Code"":""pg938"",""Size_Name"":""S"",""Color_Name"":""\uff08155\uff09\u30b7\u30eb\u30d0\u30fc\u30b0\u30ec\u30fc"",""Size_Code"":""0001"",""Color_Code"":""0003"",""Size_Name_Official"":""S"",""Color_Name_Official"":""\uff08155\uff09\u30b7\u30eb\u30d0\u30fc\u30b0\u30ec\u30fc"",""JAN_Code"":""4989723261508""},{""Item_AdminCode"":460591,""Item_Code"":""pg938"",""Size_Name"":""S"",""Color_Name"":""\uff08193\uff09\u30d6\u30e9\u30c3\u30af\u30c1\u30a7\u30c3\u30af"",""Size_Code"":""0001"",""Color_Code"":""0004"",""Size_Name_Official"":""S"",""Color_Name_Official"":""\uff08193\uff09\u30d6\u30e9\u30c3\u30af\u30c1\u30a7\u30c3\u30af"",""JAN_Code"":""4989723261522""},{""Item_AdminCode"":460592,""Item_Code"":""pg938"",""Size_Name"":""S"",""Color_Name"":""\uff08196\uff09\u30cd\u30a4\u30d3\u30fc\u30c9\u30c3\u30c8"",""Size_Code"":""0001"",""Color_Code"":""0005"",""Size_Name_Official"":""S"",""Color_Name_Official"":""\uff08196\uff09\u30cd\u30a4\u30d3\u30fc\u30c9\u30c3\u30c8"",""JAN_Code"":""4989723261539""},{""Item_AdminCode"":248423,""Item_Code"":""pg938"",""Size_Name"":""M"",""Color_Name"":""\uff08127\uff09\u30cd\u30a4\u30d3\u30fc"",""Size_Code"":""0002"",""Color_Code"":""0001"",""Size_Name_Official"":""M"",""Color_Name_Official"":""\uff08127\uff09\u30cd\u30a4\u30d3\u30fc"",""JAN_Code"":""4989723255583""},{""Item_AdminCode"":248419,""Item_Code"":""pg938"",""Size_Name"":""M"",""Color_Name"":""\uff08146\uff09\u30db\u30ef\u30a4\u30c8"",""Size_Code"":""0002"",""Color_Code"":""0002"",""Size_Name_Official"":""M"",""Color_Name_Official"":""\uff08146\uff09\u30db\u30ef\u30a4\u30c8"",""JAN_Code"":""4989723255545""},{""Item_AdminCode"":248421,""Item_Code"":""pg938"",""Size_Name"":""M"",""Color_Name"":""\uff08155\uff09\u30b7\u30eb\u30d0\u30fc\u30b0\u30ec\u30fc"",""Size_Code"":""0002"",""Color_Code"":""0003"",""Size_Name_Official"":""M"",""Color_Name_Official"":""\uff08155\uff09\u30b7\u30eb\u30d0\u30fc\u30b0\u30ec\u30fc"",""JAN_Code"":""4989723255569""},{""Item_AdminCode"":392065,""Item_Code"":""pg938"",""Size_Name"":""M"",""Color_Name"":""\uff08193\uff09\u30d6\u30e9\u30c3\u30af\u30c1\u30a7\u30c3\u30af"",""Size_Code"":""0002"",""Color_Code"":""0004"",""Size_Name_Official"":""M"",""Color_Name_Official"":""\uff08193\uff09\u30d6\u30e9\u30c3\u30af\u30c1\u30a7\u30c3\u30af"",""JAN_Code"":""4989723259925""},{""Item_AdminCode"":392067,""Item_Code"":""pg938"",""Size_Name"":""M"",""Color_Name"":""\uff08196\uff09\u30cd\u30a4\u30d3\u30fc\u30c9\u30c3\u30c8"",""Size_Code"":""0002"",""Color_Code"":""0005"",""Size_Name_Official"":""M"",""Color_Name_Official"":""\uff08196\uff09\u30cd\u30a4\u30d3\u30fc\u30c9\u30c3\u30c8"",""JAN_Code"":""4989723259949""},{""Item_AdminCode"":248424,""Item_Code"":""pg938"",""Size_Name"":""L"",""Color_Name"":""\uff08127\uff09\u30cd\u30a4\u30d3\u30fc"",""Size_Code"":""0003"",""Color_Code"":""0001"",""Size_Name_Official"":""L"",""Color_Name_Official"":""\uff08127\uff09\u30cd\u30a4\u30d3\u30fc"",""JAN_Code"":""4989723255590""},{""Item_AdminCode"":248420,""Item_Code"":""pg938"",""Size_Name"":""L"",""Color_Name"":""\uff08146\uff09\u30db\u30ef\u30a4\u30c8"",""Size_Code"":""0003"",""Color_Code"":""0002"",""Size_Name_Official"":""L"",""Color_Name_Official"":""\uff08146\uff09\u30db\u30ef\u30a4\u30c8"",""JAN_Code"":""4989723255552""},{""Item_AdminCode"":248422,""Item_Code"":""pg938"",""Size_Name"":""L"",""Color_Name"":""\uff08155\uff09\u30b7\u30eb\u30d0\u30fc\u30b0\u30ec\u30fc"",""Size_Code"":""0003"",""Color_Code"":""0003"",""Size_Name_Official"":""L"",""Color_Name_Official"":""\uff08155\uff09\u30b7\u30eb\u30d0\u30fc\u30b0\u30ec\u30fc"",""JAN_Code"":""4989723255576""},{""Item_AdminCode"":392066,""Item_Code"":""pg938"",""Size_Name"":""L"",""Color_Name"":""\uff08193\uff09\u30d6\u30e9\u30c3\u30af\u30c1\u30a7\u30c3\u30af"",""Size_Code"":""0003"",""Color_Code"":""0004"",""Size_Name_Official"":""L"",""Color_Name_Official"":""\uff08193\uff09\u30d6\u30e9\u30c3\u30af\u30c1\u30a7\u30c3\u30af"",""JAN_Code"":""4989723259932""},{""Item_AdminCode"":392068,""Item_Code"":""pg938"",""Size_Name"":""L"",""Color_Name"":""\uff08196\uff09\u30cd\u30a4\u30d3\u30fc\u30c9\u30c3\u30c8"",""Size_Code"":""0003"",""Color_Code"":""0005"",""Size_Name_Official"":""L"",""Color_Name_Official"":""\uff08196\uff09\u30cd\u30a4\u30d3\u30fc\u30c9\u30c3\u30c8"",""JAN_Code"":""4989723259956""}],""inventory"":[{""Item_AdminCode"":460590,""Item_Code"":""pg938"",""Quantity"":75,""Jisha_Quantity"":4,""Maker_Quantity"":71,""Toyonaka_Quantity"":0,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":460593,""Item_Code"":""pg938"",""Quantity"":73,""Jisha_Quantity"":6,""Maker_Quantity"":67,""Toyonaka_Quantity"":0,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":460589,""Item_Code"":""pg938"",""Quantity"":64,""Jisha_Quantity"":6,""Maker_Quantity"":58,""Toyonaka_Quantity"":0,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":460591,""Item_Code"":""pg938"",""Quantity"":43,""Jisha_Quantity"":5,""Maker_Quantity"":38,""Toyonaka_Quantity"":0,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":460592,""Item_Code"":""pg938"",""Quantity"":92,""Jisha_Quantity"":3,""Maker_Quantity"":89,""Toyonaka_Quantity"":0,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":248423,""Item_Code"":""pg938"",""Quantity"":42,""Jisha_Quantity"":4,""Maker_Quantity"":34,""Toyonaka_Quantity"":4,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":248419,""Item_Code"":""pg938"",""Quantity"":10,""Jisha_Quantity"":4,""Maker_Quantity"":100,""Toyonaka_Quantity"":6,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":248421,""Item_Code"":""pg938"",""Quantity"":6,""Jisha_Quantity"":4,""Maker_Quantity"":100,""Toyonaka_Quantity"":2,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":392065,""Item_Code"":""pg938"",""Quantity"":70,""Jisha_Quantity"":5,""Maker_Quantity"":60,""Toyonaka_Quantity"":5,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":392067,""Item_Code"":""pg938"",""Quantity"":83,""Jisha_Quantity"":5,""Maker_Quantity"":75,""Toyonaka_Quantity"":3,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":248424,""Item_Code"":""pg938"",""Quantity"":111,""Jisha_Quantity"":7,""Maker_Quantity"":100,""Toyonaka_Quantity"":4,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":248420,""Item_Code"":""pg938"",""Quantity"":111,""Jisha_Quantity"":5,""Maker_Quantity"":100,""Toyonaka_Quantity"":6,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":248422,""Item_Code"":""pg938"",""Quantity"":106,""Jisha_Quantity"":3,""Maker_Quantity"":100,""Toyonaka_Quantity"":3,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":392066,""Item_Code"":""pg938"",""Quantity"":109,""Jisha_Quantity"":6,""Maker_Quantity"":100,""Toyonaka_Quantity"":3,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":392068,""Item_Code"":""pg938"",""Quantity"":52,""Jisha_Quantity"":6,""Maker_Quantity"":44,""Toyonaka_Quantity"":2,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0}]}";
                        //string response = @"{""sku"":[{""Item_AdminCode"":450811,""Item_Code"":""test_eithinzar22"",""Size_Name"":""S"",""Color_Name"":""\uff08127\uff09\u30cd\u30a4\u30d3\u30fc"",""Size_Code"":""0001"",""Color_Code"":""0001"",""Size_Name_Official"":""S"",""Color_Name_Official"":""\uff08127\uff09\u30cd\u30a4\u30d3\u30fc"",""JAN_Code"":""1114112117"",""Rakuten_Size_Tag_Name"":""LL"",""Rakuten_Color_Tag_Name"":""ブラック"",""Rakuten_Shoe_Size_Tag_Name"":""aaaa""}]}";
                        string response = String.Empty;
                        context.Request.InputStream.Position = 0;
                        using (StreamReader inputStream = new StreamReader(context.Request.InputStream))
                        {
                            response = inputStream.ReadToEnd();
                        }
                        //string response = new StreamReader(context.Request.InputStream).ReadToEnd();
                        
                        if (!string.IsNullOrWhiteSpace(response))
                        {
                            Save_Data(response, userID);
                            //response = response.Replace("\"\"", "\"");
                            DataTable dtItem = Item_Data(response);
                            DataTable dtSKU = SKU_Data(response);
                            DataTable dtInventory = Inventory_Data(response);

                            if (dtItem.Rows.Count > 0 && dtItem != null)
                            {
                                dtItem = Check_Item(dtItem);
                                DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));
                                if (!dtItem.Columns.Contains("User_ID"))
                                {
                                    newColumn.DefaultValue = userID;
                                    dtItem.Columns.Add(newColumn);//add imported user
                                }

                                import.Itemmaster(dtItem);
                            }

                            if (dtSKU.Rows.Count > 0 && dtSKU != null)
                            {
                                dtSKU = Check_SKU(dtSKU);
                                DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));
                                if (!dtSKU.Columns.Contains("User_ID"))
                                {
                                    newColumn.DefaultValue = userID;
                                    dtSKU.Columns.Add(newColumn);//add imported user
                                }
                                import.SKU(dtSKU);
                            }

                            if (dtInventory.Rows.Count > 0 && dtInventory != null)
                            {
                                dtInventory = Check_Inventory(dtInventory);
                                DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));
                                if (!dtInventory.Columns.Contains("User_ID"))
                                {
                                    newColumn.DefaultValue = userID;
                                    dtInventory.Columns.Add(newColumn);//add imported user
                                }
                                import.Inventory(dtInventory);
                            }
                        }
                        context.Response.Write(" Import Successful.");
                    }
                    else
                    {
                        context.Response.Write("Please check user name and password.");
                    }
                }
            }
            catch (Exception ex)
            {
                string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                SqlConnection con = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "Json Import: " + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public DataTable Item_Data(string response)
        {
            try
            {
                IDictionary<string, ICollection<Item>> items;
                items = JsonConvert.DeserializeObject<IDictionary<string, ICollection<Item>>>(response);
                var dataSet = new DataSet();
                var dataTable = dataSet.Tables.Add();
                //dataTable.Columns.Add("from");
                dataTable.Columns.Add("Item_AdminCode");
                dataTable.Columns.Add("Item_Code");
                dataTable.Columns.Add("Item_Name");
                dataTable.Columns.Add("List_Price");
                dataTable.Columns.Add("Sale_Price");
                dataTable.Columns.Add("Cost");
                dataTable.Columns.Add("Release_Date");
                dataTable.Columns.Add("Post_Available_Date");
                dataTable.Columns.Add("Year");
                dataTable.Columns.Add("Season");
                dataTable.Columns.Add("Brand_Code");
                dataTable.Columns.Add("Brand_Name");
                dataTable.Columns.Add("Brand_Code_Yahoo");
                dataTable.Columns.Add("Competition_Code");
                dataTable.Columns.Add("Competition_Name");
                dataTable.Columns.Add("Classification_Code");
                dataTable.Columns.Add("Class_Name");
                dataTable.Columns.Add("Company_Name");
                dataTable.Columns.Add("Catalog_Information");
                dataTable.Columns.Add("Special_Flag");
                dataTable.Columns.Add("Reservation_Flag");
                dataTable.Columns.Add("Instruction_No");
                dataTable.Columns.Add("Approve_Date");
                dataTable.Columns.Add("Remarks");
                dataTable.Columns.Add("Product_Code");

                foreach (var itemsGroup in items)
                {
                    // foreach TValue in ICollection<TValue>
                    if (itemsGroup.Key == "item" && itemsGroup.Value != null)
                    {
                        foreach (var item in itemsGroup.Value)
                        {
                            dataTable.LoadDataRow(
                                new object[]
                        {
                            //itemsGroup.Key,
                            item.Item_AdminCode,
                            item.Item_Code,
                            item.Item_Name,
                            item.List_Price,
                            item.Sale_Price,
                            item.Cost,
                            item.Release_Date,
                            item.Post_Available_Date,
                            item.Year,
                            item.Season,
                            item.Brand_Code,
                            item.Brand_Name,
                            item.Brand_Code_Yahoo,
                            item.Competition_Code,
                            item.Competition_Name,
                            item.Classification_Code,
                            item.Class_Name,
                            item.Company_Name,
                            item.Catalog_Information,
                            item.Special_Flag,
                            item.Reservation_Flag,
                            item.Instruction_No,
                            item.Approve_Date,
                            item.Remarks,
                            item.Product_Code
                        },
                                LoadOption.PreserveChanges);
                        }
                    }
                }

                int count = dataTable.Rows.Count;
                return dataTable;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SKU_Data(string response)
        {
            try
            {
                IDictionary<string, ICollection<SKU>> items;
                items = JsonConvert.DeserializeObject<IDictionary<string, ICollection<SKU>>>(response);
                var dataSet = new DataSet();
                var dataTable = dataSet.Tables.Add();
                //dataTable.Columns.Add("from");
                dataTable.Columns.Add("Item_AdminCode");
                dataTable.Columns.Add("Item_Code");
                dataTable.Columns.Add("Color_Name");
                dataTable.Columns.Add("Size_Name");
                dataTable.Columns.Add("Color_Code");
                dataTable.Columns.Add("Size_Code");
                dataTable.Columns.Add("Color_Name_Official");
                dataTable.Columns.Add("Size_Name_Official");
                dataTable.Columns.Add("JAN_Code");
                dataTable.Columns.Add("Rakuten_Size_Tag_Name");
                dataTable.Columns.Add("Rakuten_Color_Tag_Name");
                dataTable.Columns.Add("Rakuten_Shoe_Size_Tag_Name");

                foreach (var itemsGroup in items)
                {
                    // foreach TValue in ICollection<TValue>
                    if (itemsGroup.Key == "sku" && itemsGroup.Value != null)
                    {
                        foreach (var item in itemsGroup.Value)
                        {
                            dataTable.LoadDataRow(
                                new object[]
                            {
                                //itemsGroup.Key,
                                item.Item_AdminCode,
                                item.Item_Code,
                                item.Color_Name,
                                item.Size_Name,
                                item.Color_Code,
                                item.Size_Code,
                                item.Color_Name_Official,
                                item.Size_Name_Official,
                                item.JAN_Code,
                                item.Rakuten_Size_Tag_Name,
                                item.Rakuten_Color_Tag_Name,
                                item.Rakuten_Shoe_Size_Tag_Name
                            },
                                LoadOption.PreserveChanges);
                        }
                    }
                }
                int count = dataTable.Rows.Count;
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Inventory_Data(string response)
        {
            try
            {
                IDictionary<string, ICollection<Inventory>> items;
                items = JsonConvert.DeserializeObject<IDictionary<string, ICollection<Inventory>>>(response);
                if (items.Count > 0)
                {
                    var dataSet = new DataSet();
                    var dataTable = dataSet.Tables.Add();
                    //dataTable.Columns.Add("from");
                    dataTable.Columns.Add("Item_AdminCode");
                    dataTable.Columns.Add("Item_Code");
                    dataTable.Columns.Add("Quantity");
                    dataTable.Columns.Add("CSV_FileName");
                    dataTable.Columns.Add("Jisha_Quantity");
                    dataTable.Columns.Add("Maker_Quantity");
                    dataTable.Columns.Add("Toyonaka_Quantity");
                    dataTable.Columns.Add("Ishibashi_Quantity");
                    dataTable.Columns.Add("Esaka_Quantity");
                    dataTable.Columns.Add("Sannomiya_Quantity");

                    foreach (var itemsGroup in items)
                    {
                        if (itemsGroup.Key == "inventory" && itemsGroup.Value != null)
                        {
                            // foreach TValue in ICollection<TValue>
                            foreach (var item in itemsGroup.Value)
                            {
                                dataTable.LoadDataRow(
                                    new object[]
                        {
                            //itemsGroup.Key,
                            item.Item_AdminCode,
                            item.Item_Code,
                            item.Quantity,
                            "link",
                            item.Jisha_Quantity,
                            item.Maker_Quantity,
                            item.Toyonaka_Quantity,
                            item.Ishibashi_Quantity,
                            item.Esaka_Quantity,
                            item.Sannomiya_Quantity
                        },
                             LoadOption.PreserveChanges);
                            }
                        }
                    }
                    int count = dataTable.Rows.Count;
                    return dataTable;
                }
                return new DataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Check_Item(DataTable dt)
        {
            try
            {
                DataColumn newcol = new DataColumn("チェック", typeof(String));
                newcol.DefaultValue = "";
                dt.Columns.Add(newcol);//add check column to datatable that show error or not
                DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
                newColumn.DefaultValue = 6;//type 6 = ok , type 5 = error
                dt.Columns.Add(newColumn);//add type column to datatable to separate error record and  ok record

                DataColumn dc = new DataColumn("エラー内容", typeof(String));
                dc.DefaultValue = "";
                dt.Columns.Add(dc);//add error detail column to datatable to show error message detail

                String[] colTypeCheck = { "List_Price", "Sale_Price", "Cost", "Special_Flag", "Reservation_Flag" };//need to check this column's value is integer 
                DataTable dterrcheck = CheckIntType(dt, colTypeCheck, 0);

                String[] colLengthCheck2 = { "Product_Code" };//need to check this column value's length is greater than 100
                dterrcheck = CheckLength(dterrcheck, colLengthCheck2, 100, 0);

                String[] colLengthCheck3 = { "Item_AdminCode" };//need to check this column value's length is greater than 1300
                dterrcheck = CheckLength(dterrcheck, colLengthCheck3, 1300, 0);

                String[] colLengthCheck4 = { "Item_Code" };//need to check this column value's length is greater than 32
                dterrcheck = CheckLength(dterrcheck, colLengthCheck4, 32, 0);

                String[] colLengthCheck5 = { "Item_Name" };//need to check this column value's length is greater than 255
                dterrcheck = CheckLength(dterrcheck, colLengthCheck5, 255, 0);

                String[] colLengthCheck6 = { "Brand_Code", "Competition_Code", "Classification_Code" };//need to check this column value's length is greater than 4
                dterrcheck = CheckLength(dterrcheck, colLengthCheck6, 4, 0);

                String[] colLengthCheck7 = { "Season" };//need to check this column value's length is greater than 40
                dterrcheck = CheckLength(dterrcheck, colLengthCheck7, 40, 0);

                String[] colLengthCheck8 = { "Brand_Name", "Competition_Name", "Class_Name", "Company_Name" };//need to check this column value's length is greater than 200
                dterrcheck = CheckLength(dterrcheck, colLengthCheck8, 200, 0);

                String[] colLengthCheck9 = { "Brand_Code_Yahoo" };//need to check this column value's length is greater than 6
                dterrcheck = CheckLength(dterrcheck, colLengthCheck9, 6, 0);

                String[] colLengthCheck10 = { "Catalog_Information", "Remarks" };//need to check this column value's length is greater than 3000
                dterrcheck = CheckLength(dterrcheck, colLengthCheck10, 3000, 0);

                String[] colLengthCheck11 = { "Instruction_No" };//need to check this column value's length is greater than 4000
                dterrcheck = CheckLength(dterrcheck, colLengthCheck11, 4000, 0);

                String[] colLengthCheck12 = { "Year" };//need to check this column value's length is greater than 20
                dterrcheck = CheckLength(dterrcheck, colLengthCheck12, 20, 0);

                String[] colDateCheck = { "Release_Date", "Post_Available_Date", "Approve_Date" };//need to check this column value's is date time 
                dterrcheck = CheckDate(dterrcheck, colDateCheck, 0);

                return dterrcheck;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Check_SKU(DataTable dt)
        {
            try
            {
                DataColumn newcol = new DataColumn("チェック", typeof(String));//add check column to datatable that show error or not
                newcol.DefaultValue = "";
                dt.Columns.Add(newcol);//add check column to datatable that show error or not
                DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
                newColumn.DefaultValue = 6;//type 6 = ok , type 5 = error
                dt.Columns.Add(newColumn);//add type column to datatable to separate error record and  ok record

                DataColumn dc = new DataColumn("エラー内容", typeof(String));
                dc.DefaultValue = "";
                dt.Columns.Add(dc);//add error detail column to datatable to show error message detail

                String[] colCheckLength = { "Item_Code", "Color_Code", "Size_Code", "JAN_Code" };//need to check this column value's length is greater than 50
                DataTable dterrcheck = CheckLength(dt, colCheckLength, 50, 1);

                String[] colCheckLength1 = { "Color_Name", "Size_Name", "Color_Name_Official", "Size_Name_Official", "Rakuten_Size_Tag_Name", "Rakuten_Color_Tag_Name", "Rakuten_Shoe_Size_Tag_Name" };//need to check this column value's length is greater than 200
                dterrcheck = CheckLength(dterrcheck, colCheckLength1, 200, 1);

                return dterrcheck;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Check_Inventory(DataTable dt)
        {
            try
            {
                DataColumn newcol = new DataColumn("チェック", typeof(String));
                newcol.DefaultValue = "";
                dt.Columns.Add(newcol);//add check column to datatable that show error or not
                DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
                newColumn.DefaultValue = 6;//type 6 = ok , type 5 = error
                dt.Columns.Add(newColumn);//add type column to datatable to separate error record and  ok record

                DataColumn dc = new DataColumn("エラー内容", typeof(String));
                dc.DefaultValue = "";
                dt.Columns.Add(dc);//add error detail column to datatable to show error message detail

                String[] colCheckLength = { "Item_Code" };//need to check this column value's length is greater than 50
                DataTable dterrcheck = CheckLength(dt, colCheckLength, 50, 2);

                String[] colCheckType = { "Quantity" };
                dterrcheck = CheckIntType(dterrcheck, colCheckType, 2);//need to check this column value is integer

                return dterrcheck;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //check the value is integer
        protected DataTable CheckIntType(DataTable dt, String[] col, int type)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < col.Length; j++)
                    {
                        try
                        {
                            if (!String.IsNullOrWhiteSpace(dt.Rows[i][col[j]].ToString()))
                                Convert.ToInt32(dt.Rows[i][col[j]].ToString());//check integer or not**(convert value to int-- if ok --> integer error occur -- go to cache)
                        }
                        catch (Exception)//if not integer
                        {
                            dt.Rows[i]["チェック"] = "エラー";//error 
                            dt.Rows[i]["エラー内容"] = col[j].ToString() + "のフォーマットが不正です。";//error detail
                            dt.Rows[i]["Type"] = 5;//error type
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        //check length by byte
        protected DataTable CheckLength(DataTable dt, String[] col, int length, int type)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < col.Length; j++)
                    {
                        Encoding enc = Encoding.GetEncoding(932);
                        int byteLength = enc.GetByteCount(dt.Rows[i][col[j]].ToString());//check length by byte
                        if (byteLength > length)//check value is greater than limit
                        {
                            dt.Rows[i]["チェック"] = "エラー";//error
                            dt.Rows[i]["エラー内容"] = col[j].ToString() + "-Greater than " + length + " Bytes";//error detail
                            dt.Rows[i]["Type"] = 5;//type
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        //check the value is date
        protected DataTable CheckDate(DataTable dt, String[] col, int type)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < col.Length; j++)
                    {
                        try
                        {
                            if (!String.IsNullOrWhiteSpace(dt.Rows[i][col[j]].ToString()))
                            {
                                DateTime dateTime = Convert.ToDateTime(dt.Rows[i][col[j]].ToString());
                                DateTime dtMin = DateTime.MinValue;
                                DateTime dtMax = DateTime.MaxValue;

                                dtMin = new DateTime(1753, 1, 1);//minimum date
                                dtMax = new DateTime(9999, 12, 31, 23, 59, 59, 997);//maximum date

                                if (dateTime < dtMin || dateTime > dtMax)//check value is not between minimum or maximum
                                {
                                    dt.Rows[i]["チェック"] = "エラー";//error
                                    dt.Rows[i]["エラー内容"] = col[j].ToString() + "のフォーマットが不正です。";//error detail
                                    dt.Rows[i]["Type"] = 5;//type
                                }
                            }
                        }
                        catch (Exception)
                        {
                            dt.Rows[i]["チェック"] = "エラー";
                            dt.Rows[i]["エラー内容"] = col[j].ToString() + "のフォーマットが不正です。";
                            dt.Rows[i]["Type"] = 5;
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public void Save_Data(string response, int userID)
        {
            try
            {

                //string connect = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                //SqlConnection connMaker = new SqlConnection(connect);

                //SqlCommand cmd = new SqlCommand("INSERT INTO SYS_Error_Log(ErrorDetail)VALUES(@i);", connMaker);
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandTimeout = 0;
                //cmd.Parameters.AddWithValue("@i", response);
                //cmd.Connection.Open();
                //cmd.ExecuteNonQuery();
                //cmd.Connection.Close();
                string UserInfo = "User ID:" + userID + "  Import Date:" + System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")+System.Environment.NewLine+response;
                ConsoleWriteLine_ToFile(UserInfo);

                string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                SqlConnection con = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand("SP_Insert_Json_Import_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@ErrorDetail", response);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        ////public void ProcessRequest(HttpContext context)
        ////{
        ////    try
        ////    {
        ////        context.Response.ContentType = "text/plain";
        ////        context.Response.Write("Hello World");
        ////        //string response = context.Request["vc_user_shohin"];
        ////        //string response = context;
        ////        //string response = @"{""item"":[{""nc_hanbai_kanri"":""770470;770466;770467;770469;770472;770473;770474;770475;770476;770471;770461;770462;770463;770468;770465;770464"",""vc_user_shohin"":""pum-341044-992"",""vm_hanbai_shohin"":""TX-3\uff0f\u30e6\u30cb\u30bb\u30c3\u30af\u30b9\uff08341044\uff09"",""ns_teika"":""9180"",""ns_hanbai_kakaku"":""7803"",""ns_genka"":""5525"",""vo_hatsubaibi"":""2015-07-25"",""vo_keisaikanoubi"":""null"",""vm_nendo"":""2015\u5e74\u5ea6"",""vm_season"":""FW Q3"",""nc_brand"":""68"",""vm_brand"":""\u30d7\u30fc\u30de"",""nc_yahoo_brand"":""1611"",""nc_sports"":""998"",""vm_sports"":""\u30e9\u30a4\u30d5\u30b9\u30bf\u30a4\u30eb"",""nc_bunrui"":""20"",""vm_bunrui"":""\u30b7\u30e5\u30fc\u30ba"",""vm_supplier"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""vo_catalog"":""2015pum-fw-02-019 "",""nf_maker_hachu_huka"":""0"",""nk_yoyaku_shohin"":""1"",""vo_shijisho"":""null"",""dt_shouninbi"":""2015-11-06 15:04:00"",""vo_bikou"":"""",""vc_maker_shohin"":""341044""}]}";
        ////         //context.Items["oneItem"] = @"{""item"":[{""Item_AdminCode"":""417951;444529;417945;417947;444532;421423;444534;444533;421422;417952;421421;444537;417950;417946;444535;444530;444531;417948;444536;417949"",""Item_Code"":""miz-2zk-470-473"",""Item_Name"":""\u9774\u7d10\uff0f\u5e73\u4e38\u7d10\uff082ZK-470\uff0f2ZK-471\uff0f2ZK-472\uff0f2ZK-473\uff09"",""List_Price"":324,""Sale_Price"":275,""Cost"":193,""Release_Date"":null,""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""SS Q1"",""Brand_Code"":64,""Brand_Name"":""\u30df\u30ba\u30ce"",""Brand_Code_Yahoo"":1958,""Competition_Code"":120,""Competition_Name"":""\u91ce\u7403"",""Classification_Code"":130,""Class_Name"":""\u30a2\u30af\u30bb\u30b5\u30ea\u30fb\u5c0f\u7269"",""Company_Name"":""\u7f8e\u6d25\u6fc3(\u682a)"",""Catalog_Information"":""2015miz-ss-03-\u30b7\u30e5\u30fc\u30ba-169 2014miz-ss-08-\u30b7\u30e5\u30fc\u30ba-125 2013miz-ss-04-\u30b7\u30e5\u30fc\u30ba-128 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":""20151106\u639b\u7387\u5909\u66f4\uff082015-0355\uff09"",""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":""\u30b5\u30ab\u30bf\u30b9\u30dd\u30fc\u30c4\u5f15\u8d8a 20151014\u5728\u5eab\u9023\u52d5\u958b\u59cb"",""Product_Code"":""2ZK-47101""},{""Item_AdminCode"":""489819;489818;421143;421145;421144;421142;489820;489821"",""Item_Code"":""miz-2ya-357-358"",""Item_Name"":""\u30d7\u30ed\u30c6\u30af\u30bf\u30fc\uff0f\u8edf\u5f0f\u7528\uff0f\u30ad\u30e3\u30c3\u30c1\u30e3\u30fc\u7528\u9632\u5177\uff082YA357\uff0f2YA358\uff09"",""List_Price"":7884,""Sale_Price"":4730,""Cost"":3285,""Release_Date"":null,""Post_Available_Date"":null,""Year"":""2014\u5e74\u5ea6"",""Season"":""SS Q1"",""Brand_Code"":64,""Brand_Name"":""\u30df\u30ba\u30ce"",""Brand_Code_Yahoo"":1958,""Competition_Code"":122,""Competition_Name"":""\u8edf\u5f0f\u91ce\u7403"",""Classification_Code"":240,""Class_Name"":""\u30b0\u30e9\u30f3\u30c9\u7528\u54c1"",""Company_Name"":""\u7f8e\u6d25\u6fc3(\u682a)"",""Catalog_Information"":""2014miz-ss-08-\u30ad\u30e3\u30c3\u30c1\u30e3\u30fc\u7528\u9632\u5177-160 2013miz-ss-04-\u30ad\u30e3\u30c3\u30c1\u30e3\u30fc\u7528\u9632\u5177-163 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":""20151106\u639b\u7387\u30fb\u58f2\u4fa1\u5909\u66f4\uff082015-0355\uff09"",""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":""20151014\u5728\u5eab\u9023\u52d5\u958b\u59cb"",""Product_Code"":""2YA35862""},{""Item_AdminCode"":""489898;421174;421173;489897;489900;489901;489899;421172"",""Item_Code"":""miz-2yl-557-558"",""Item_Name"":""\u30ec\u30ac\u30fc\u30ba\uff0f\u30bd\u30d5\u30c8\u30dc\u30fc\u30eb\u7528\uff0f\u30ad\u30e3\u30c3\u30c1\u30e3\u30fc\u7528\u9632\u5177\uff082YL557\uff0f2YL558\uff09"",""List_Price"":10260,""Sale_Price"":6156,""Cost"":4275,""Release_Date"":null,""Post_Available_Date"":null,""Year"":""2014\u5e74\u5ea6"",""Season"":""SS Q1"",""Brand_Code"":64,""Brand_Name"":""\u30df\u30ba\u30ce"",""Brand_Code_Yahoo"":1958,""Competition_Code"":121,""Competition_Name"":""\u30bd\u30d5\u30c8\u30dc\u30fc\u30eb"",""Classification_Code"":240,""Class_Name"":""\u30b0\u30e9\u30f3\u30c9\u7528\u54c1"",""Company_Name"":""\u7f8e\u6d25\u6fc3(\u682a)"",""Catalog_Information"":""2014miz-ss-08-\u30ad\u30e3\u30c3\u30c1\u30e3\u30fc\u7528\u9632\u5177-161 2013miz-ss-04-\u30ad\u30e3\u30c3\u30c1\u30e3\u30fc\u7528\u9632\u5177-164 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":""20151106\u639b\u7387\u30fb\u58f2\u4fa1\u5909\u66f4\uff082015-0355\uff09"",""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":""20151014\u5728\u5eab\u9023\u52d5\u958b\u59cb"",""Product_Code"":""2YL55709""},{""Item_AdminCode"":""788368;788356;788355;788361;788362;788363;788364;788365;788366;788369;788351;788352;788353;788354;788357;788358;788359;788360;788349;788350;788370;788371;788372;788367"",""Item_Code"":""tsp-032407"",""Item_Name"":""\u30ec\u30c7\u30a3\u30fc\u30b9 \u30aa\u30eb\u30d3\u30c3\u30c8\u30b7\u30e3\u30c4\uff08032407\uff09"",""List_Price"":6480,""Sale_Price"":5184,""Cost"":3480,""Release_Date"":""2015-10-05"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":90,""Class_Name"":""\u30a6\u30a7\u30a2\uff08\u30ec\u30c7\u30a3\u30fc\u30b9\uff09"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-007 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""032407""},{""Item_AdminCode"":""788414;788413"",""Item_Code"":""tsp-044714"",""Item_Name"":""TSP\u30b5\u30dd\u30fc\u30bf\u30fc \u3072\u3056\u7528\uff0f1\u672c\u5165\u308a\uff08044714\uff09"",""List_Price"":1512,""Sale_Price"":1209,""Cost"":812,""Release_Date"":""2015-10-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":990,""Competition_Name"":""\u30aa\u30fc\u30eb\u30b9\u30dd\u30fc\u30c4"",""Classification_Code"":110,""Class_Name"":""\u30b5\u30dd\u30fc\u30bf\u30fc\u30b1\u30a2\u5546\u54c1"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-008 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""044714""},{""Item_AdminCode"":""788415;788416"",""Item_Code"":""tsp-044715"",""Item_Name"":""TSP\u30b5\u30dd\u30fc\u30bf\u30fc \u3075\u304f\u3089\u306f\u304e\u7528\uff0f\uff11\u672c\u5165\u308a\uff08044715\uff09"",""List_Price"":1512,""Sale_Price"":1209,""Cost"":812,""Release_Date"":""2015-10-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":990,""Competition_Name"":""\u30aa\u30fc\u30eb\u30b9\u30dd\u30fc\u30c4"",""Classification_Code"":110,""Class_Name"":""\u30b5\u30dd\u30fc\u30bf\u30fc\u30b1\u30a2\u5546\u54c1"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-008 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""044715""},{""Item_AdminCode"":""788417"",""Item_Code"":""tsp-010045"",""Item_Name"":""40mm\uff0b \u30c8\u30ec\u30fc\u30cb\u30f3\u30b0\u30dc\u30fc\u30eb10\u30c0\u30fc\u30b9\u5165\u308a\uff0f120\u7403\uff08010045\uff09"",""List_Price"":11664,""Sale_Price"":9331,""Cost"":6264,""Release_Date"":""2015-12-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":50,""Class_Name"":""\u30dc\u30fc\u30eb"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-009 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""010045""},{""Item_AdminCode"":""788299;788296;788298;788287;788288;788289;788290;788295;788297;788300;788301;788302;788303;788304;788305;788285;788286;788291;788292;788293;788294"",""Item_Code"":""vic-031454"",""Item_Name"":""V-SW023 \u30b2\u30fc\u30e0\u30b7\u30e3\u30c4\uff0f\u30e6\u30cb\u30bb\u30c3\u30af\u30b9\uff08031454\uff09"",""List_Price"":8424,""Sale_Price"":7160,""Cost"":5070,""Release_Date"":""2015-10-15"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":160,""Brand_Name"":""\u30d3\u30a3\u30af\u30bf\u30b9"",""Brand_Code_Yahoo"":null,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":80,""Class_Name"":""\u30a6\u30a7\u30a2\uff08\u30e1\u30f3\u30ba\/\u30e6\u30cb\uff09"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-004 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""031454""},{""Item_AdminCode"":""788390;788391;788392;788387;788388;788389;788398;788399;788400;788393;788394;788395;788396;788397"",""Item_Code"":""tsp-033885"",""Item_Name"":""\u30a4\u30f3\u30f4\u30a7\u30eb\u30ce\u30d1\u30f3\u30c4\uff0f\u30e6\u30cb\u30bb\u30c3\u30af\u30b9\uff08033885\uff09"",""List_Price"":7560,""Sale_Price"":6048,""Cost"":4060,""Release_Date"":""2015-10-05"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":80,""Class_Name"":""\u30a6\u30a7\u30a2\uff08\u30e1\u30f3\u30ba\/\u30e6\u30cb\uff09"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-007 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""033885""},{""Item_AdminCode"":""788403;788405;788404"",""Item_Code"":""tsp-042407"",""Item_Name"":""TSP\u30d0\u30c3\u30af\u30d1\u30c3\u30af\uff08042407\uff09"",""List_Price"":6480,""Sale_Price"":5184,""Cost"":3480,""Release_Date"":""2015-10-15"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":120,""Class_Name"":""\u30d0\u30c3\u30b0"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-008 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""042407""},{""Item_AdminCode"":""788406;788407;788408"",""Item_Code"":""tsp-042408"",""Item_Name"":""TSP\u30b7\u30e5\u30fc\u30ba\u888b\uff08042408\uff09"",""List_Price"":594,""Sale_Price"":475,""Cost"":319,""Release_Date"":""2015-10-05"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":120,""Class_Name"":""\u30d0\u30c3\u30b0"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-008 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""042408""},{""Item_AdminCode"":""788401;788402"",""Item_Code"":""tsp-044405"",""Item_Name"":""\u30a2\u30fc\u30ac\u30a4\u30ebJQ\u30b9\u30dd\u30fc\u30c4\u30bf\u30aa\u30eb\uff08044405\uff09"",""List_Price"":2916,""Sale_Price"":2332,""Cost"":1566,""Release_Date"":""2015-10-05"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":130,""Class_Name"":""\u30a2\u30af\u30bb\u30b5\u30ea\u30fb\u5c0f\u7269"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-008 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""044405""},{""Item_AdminCode"":""788409;788410"",""Item_Code"":""tsp-044712"",""Item_Name"":""TSP\u30a2\u30fc\u30e0\u30a6\u30a9\u30fc\u30de\u30fc\uff0f2\u672c\u5165\u308a\uff08044712\uff09"",""List_Price"":1404,""Sale_Price"":1123,""Cost"":754,""Release_Date"":""2015-10-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":130,""Class_Name"":""\u30a2\u30af\u30bb\u30b5\u30ea\u30fb\u5c0f\u7269"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-008 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""044712""},{""Item_AdminCode"":""788411;788412"",""Item_Code"":""tsp-044713"",""Item_Name"":""TSP\u30b5\u30dd\u30fc\u30bf\u30fc \u3072\u3058\u7528\uff0f1\u672c\u5165\u308a\uff08044713\uff09"",""List_Price"":1296,""Sale_Price"":1036,""Cost"":696,""Release_Date"":""2015-10-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":990,""Competition_Name"":""\u30aa\u30fc\u30eb\u30b9\u30dd\u30fc\u30c4"",""Classification_Code"":110,""Class_Name"":""\u30b5\u30dd\u30fc\u30bf\u30fc\u30b1\u30a2\u5546\u54c1"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-008 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""044713""},{""Item_AdminCode"":""788308;788307;788309;788310;788311;788312;788313;788314;788315;788316;788317;788318;788319;788323;788324;788325;788326;788327;788320;788321;788322"",""Item_Code"":""tsp-031412"",""Item_Name"":""\u30a8\u30a2\u30fc\u30d5\u30ed\u30fc\u30b7\u30e3\u30c4\uff0f\u30e6\u30cb\u30bb\u30c3\u30af\u30b9\uff08031412\uff09"",""List_Price"":8208,""Sale_Price"":6566,""Cost"":4408,""Release_Date"":""2015-10-15"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":80,""Class_Name"":""\u30a6\u30a7\u30a2\uff08\u30e1\u30f3\u30ba\/\u30e6\u30cb\uff09"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-006 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""031412""},{""Item_AdminCode"":""788332;788328;788329;788330;788331;788333;788334;788335;788336;788337;788338;788339;788340;788341;788343;788344;788345;788346;788347;788348;788342"",""Item_Code"":""tsp-031413"",""Item_Name"":""\u30aa\u30eb\u30d3\u30c3\u30c8\u30b7\u30e3\u30c4\uff0f\u30e6\u30cb\u30bb\u30c3\u30af\u30b9\uff08031413\uff09"",""List_Price"":7020,""Sale_Price"":5616,""Cost"":3770,""Release_Date"":""2015-10-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":80,""Class_Name"":""\u30a6\u30a7\u30a2\uff08\u30e1\u30f3\u30ba\/\u30e6\u30cb\uff09"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-006 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""031413""},{""Item_AdminCode"":""788379;788378;788380;788381;788382;788383;788384;788385;788386;788373;788374;788375;788376;788377"",""Item_Code"":""tsp-033884"",""Item_Name"":""\u30a4\u30f3\u30f4\u30a7\u30eb\u30ce\u30b8\u30e3\u30b1\u30c3\u30c8\uff0f\u30e6\u30cb\u30bb\u30c3\u30af\u30b9\uff08033884\uff09"",""List_Price"":10800,""Sale_Price"":8640,""Cost"":5800,""Release_Date"":""2015-10-05"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":80,""Class_Name"":""\u30a6\u30a7\u30a2\uff08\u30e1\u30f3\u30ba\/\u30e6\u30cb\uff09"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-007 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""033884""},{""Item_AdminCode"":""788284"",""Item_Code"":""vic-026664"",""Item_Name"":""\u677e\u4e0b\u6d69\u4e8c\u30b9\u30da\u30b7\u30e3\u30eb\uff0f\u30d5\u30ec\u30a2\uff08026664\uff09"",""List_Price"":12960,""Sale_Price"":11016,""Cost"":7800,""Release_Date"":""2015-12-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":160,""Brand_Name"":""\u30d3\u30a3\u30af\u30bf\u30b9"",""Brand_Code_Yahoo"":null,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-003 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""026664""},{""Item_AdminCode"":""788283"",""Item_Code"":""vic-026665"",""Item_Name"":""\u677e\u4e0b\u6d69\u4e8c\u30b9\u30da\u30b7\u30e3\u30eb\uff0f\u30b9\u30c8\u30ec\u30fc\u30c8\uff08026665\uff09"",""List_Price"":12960,""Sale_Price"":11016,""Cost"":7800,""Release_Date"":""2015-12-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":160,""Brand_Name"":""\u30d3\u30a3\u30af\u30bf\u30b9"",""Brand_Code_Yahoo"":null,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-003 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""026665""},{""Item_AdminCode"":""706475;706476;706477;706478"",""Item_Code"":""vic-020461"",""Item_Name"":""V15\uff1e\u30a8\u30ad\u30b9\u30c8\u30e9\uff08020461\uff09"",""List_Price"":6480,""Sale_Price"":5508,""Cost"":3900,""Release_Date"":""2015-07-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":160,""Brand_Name"":""\u30d3\u30a3\u30af\u30bf\u30b9"",""Brand_Code_Yahoo"":null,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":70,""Class_Name"":""\u30e9\u30d0\u30fc"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-002 2015vic-ss-01-003 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""020461""},{""Item_AdminCode"":""484300;484294;484295;484296;484297;484298;484299;788438;788432;788433;788434;788435;788436;788437"",""Item_Code"":""vic-33135"",""Item_Name"":""V-JJ012\uff0f\u30b8\u30e3\u30fc\u30b8\u30b8\u30e3\u30b1\u30c3\u30c8\uff0f\u30e6\u30cb\u30bb\u30c3\u30af\u30b9\uff08033135\uff09"",""List_Price"":8856,""Sale_Price"":7527,""Cost"":5330,""Release_Date"":""2015-10-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":160,""Brand_Name"":""\u30d3\u30a3\u30af\u30bf\u30b9"",""Brand_Code_Yahoo"":null,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":80,""Class_Name"":""\u30a6\u30a7\u30a2\uff08\u30e1\u30f3\u30ba\/\u30e6\u30cb\uff09"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-005 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:18:00"",""Remarks"":""2015tsp-fw-01\u3088\u308a\u8272or\u30b5\u30a4\u30ba\u8ffd\u52a0"",""Product_Code"":""033135""},{""Item_AdminCode"":""484306;484307;484301;484302;484303;484304;484305;788445;788439;788440;788441;788442;788443;788444"",""Item_Code"":""vic-33145"",""Item_Name"":""V-JP013\uff0f\u30b8\u30e3\u30fc\u30b8\u30d1\u30f3\u30c4\uff0f\u30e6\u30cb\u30bb\u30c3\u30af\u30b9\uff08033145\uff09"",""List_Price"":6696,""Sale_Price"":5691,""Cost"":4030,""Release_Date"":""2015-10-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":160,""Brand_Name"":""\u30d3\u30a3\u30af\u30bf\u30b9"",""Brand_Code_Yahoo"":null,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":80,""Class_Name"":""\u30a6\u30a7\u30a2\uff08\u30e1\u30f3\u30ba\/\u30e6\u30cb\uff09"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-005 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:18:00"",""Remarks"":""2015tsp-fw-01\u3088\u308a\u8272or\u30b5\u30a4\u30ba\u8ffd\u52a0"",""Product_Code"":""033145""},{""Item_AdminCode"":""68614;68615;68613;68616;68617;68612"",""Item_Code"":""sr700"",""Item_Name"":""R2X\u3000700\uff08SR700\uff09"",""List_Price"":0,""Sale_Price"":6300,""Cost"":3750,""Release_Date"":null,""Post_Available_Date"":null,""Year"":""2010\u5e74\u5ea6\u4ee5\u524d"",""Season"":""\u305d\u306e\u4ed6"",""Brand_Code"":35,""Brand_Name"":""\u30b4\u30fc\u30bb\u30f3"",""Brand_Code_Yahoo"":15591,""Competition_Code"":20,""Competition_Name"":""\u30bd\u30d5\u30c8\u30c6\u30cb\u30b9"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7a\uff9e\uff70\uff7e\uff9d"",""Catalog_Information"":null,""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":""20151106\u5358\u767a\u4f01\u753b\uff082015-0362\uff09"",""Approve_Date"":""2015-11-06 16:55:00"",""Remarks"":""\u30dd\u30f3\u79fb\u884c ReFAX 120328\u4fa1\u683c\u5909\u66f4 Refax,20150806WEB\u53d6\u6271\u4e0d\u53ef\uff08SKS\u30c7\u30fc\u30bf\u306a\u3057\uff09 \u5b9a\u4fa1\u5909\u66f4\uff08\u5143\u5b9a\u4fa1\uff1a15000\uff09"",""Product_Code"":""SR700""},{""Item_AdminCode"":""706550;706472;706473;706474"",""Item_Code"":""vic-020451"",""Item_Name"":""V15\uff1e\u30ea\u30f3\u30d0\u30fc\uff08020451\uff09"",""List_Price"":6480,""Sale_Price"":5508,""Cost"":3900,""Release_Date"":""2015-07-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":160,""Brand_Name"":""\u30d3\u30a3\u30af\u30bf\u30b9"",""Brand_Code_Yahoo"":null,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":70,""Class_Name"":""\u30e9\u30d0\u30fc"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-002 2015vic-ss-01-003 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""020451""},{""Item_AdminCode"":""702494"",""Item_Code"":""tsp-014025"",""Item_Name"":""40mm\uff0b 3\u30b9\u30bf\u30fc\u30dc\u30fc\u30eb\uff0f1\u30c0\u30fc\u30b9\u5165\uff08014025\uff09"",""List_Price"":4665,""Sale_Price"":4198,""Cost"":2505,""Release_Date"":null,""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":50,""Class_Name"":""\u30dc\u30fc\u30eb"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-009 2015tsp-ss-01-004 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""014025""},{""Item_AdminCode"":""788306"",""Item_Code"":""vic-042700"",""Item_Name"":""V-SB024 \u9060\u5f81\u30d0\u30c3\u30b0\uff08042700\uff09"",""List_Price"":9504,""Sale_Price"":8078,""Cost"":5720,""Release_Date"":""2015-10-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":160,""Brand_Name"":""\u30d3\u30a3\u30af\u30bf\u30b9"",""Brand_Code_Yahoo"":null,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":120,""Class_Name"":""\u30d0\u30c3\u30b0"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-005 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""042700""},{""Item_AdminCode"":""598453"",""Item_Code"":""tsp-011050"",""Item_Name"":""40mm\uff0b 1\u30b9\u30bf\u30fc\u30dc\u30fc\u30eb 5\u30c0\u30fc\u30b9\u5165\uff0f60\u7403\uff08011050\uff09"",""List_Price"":7128,""Sale_Price"":5702,""Cost"":3828,""Release_Date"":""2014-09-10"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":50,""Class_Name"":""\u30dc\u30fc\u30eb"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-009 2015tsp-ss-01-004 2014tsp-fw-01-004 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""011050""},{""Item_AdminCode"":""593392"",""Item_Code"":""tsp-14035"",""Item_Name"":""40mm\uff0b 3\u30b9\u30bf\u30fc\u30dc\u30fc\u30eb 3\u30f6\u5165\uff08014035\uff09"",""List_Price"":1166,""Sale_Price"":932,""Cost"":626,""Release_Date"":""2014-08-31"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":50,""Class_Name"":""\u30dc\u30fc\u30eb"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-009 2015tsp-ss-01-004 2014tsp-fw-01-004 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""014035""},{""Item_AdminCode"":""788428"",""Item_Code"":""tsp-026684"",""Item_Name"":""\u30b9\u30ef\u30c3\u30c8 \u30d1\u30ef\u30fc FL\uff0f\u30d5\u30ec\u30a2\uff08026684\uff09"",""List_Price"":10800,""Sale_Price"":8640,""Cost"":5800,""Release_Date"":""2015-12-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-010 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""026684""},{""Item_AdminCode"":""788429"",""Item_Code"":""tsp-026685"",""Item_Name"":""\u30b9\u30ef\u30c3\u30c8 \u30d1\u30ef\u30fc ST\uff0f\u30b9\u30c8\u30ec\u30fc\u30c8\uff08026685\uff09"",""List_Price"":10800,""Sale_Price"":8640,""Cost"":5800,""Release_Date"":""2015-12-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-010 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""026685""},{""Item_AdminCode"":""788430"",""Item_Code"":""tsp-026693"",""Item_Name"":""\u30d6\u30ed\u30c3\u30af\u30de\u30f32 CHN\uff0f\u4e2d\u56fd\u5f0f\u30da\u30f3\uff08026693\uff09"",""List_Price"":7020,""Sale_Price"":5616,""Cost"":3770,""Release_Date"":""2015-11-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-011 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""026693""},{""Item_AdminCode"":""788431"",""Item_Code"":""tsp-026694"",""Item_Name"":""\u30d6\u30ed\u30c3\u30af\u30de\u30f32 FL\uff0f\u30d5\u30ec\u30a2\uff08026694\uff09"",""List_Price"":7020,""Sale_Price"":5616,""Cost"":3770,""Release_Date"":""2015-11-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-011 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""026694""},{""Item_AdminCode"":""788418"",""Item_Code"":""tsp-025480"",""Item_Name"":""\u30b8\u30e3\u30a4\u30a2\u30f3\u30c8\u30d7\u30e9\u30b9 \u30b7\u30a7\u30fc\u30af\u30cf\u30f3\u30c9 140S\uff08025480\uff09"",""List_Price"":1512,""Sale_Price"":1209,""Cost"":812,""Release_Date"":""2015-11-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-009 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""025480""},{""Item_AdminCode"":""788419"",""Item_Code"":""tsp-025490"",""Item_Name"":""\u30b8\u30e3\u30a4\u30a2\u30f3\u30c8\u30d7\u30e9\u30b9 \u30b7\u30a7\u30fc\u30af\u30cf\u30f3\u30c9 160S\uff08025490\uff09"",""List_Price"":1728,""Sale_Price"":1382,""Cost"":928,""Release_Date"":""2015-11-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-009 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""025490""},{""Item_AdminCode"":""788420"",""Item_Code"":""tsp-025500"",""Item_Name"":""\u30b8\u30e3\u30a4\u30a2\u30f3\u30c8\u30d7\u30e9\u30b9 \u30b7\u30a7\u30fc\u30af\u30cf\u30f3\u30c9 180S\uff08025500\uff09"",""List_Price"":1944,""Sale_Price"":1555,""Cost"":1044,""Release_Date"":""2015-11-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-009 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""025500""},{""Item_AdminCode"":""788421"",""Item_Code"":""tsp-025510"",""Item_Name"":""\u30b8\u30e3\u30a4\u30a2\u30f3\u30c8\u30d7\u30e9\u30b9 \u30b7\u30a7\u30fc\u30af\u30cf\u30f3\u30c9 200S\uff08025510\uff09"",""List_Price"":2160,""Sale_Price"":1728,""Cost"":1160,""Release_Date"":""2015-11-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-009 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""025510""},{""Item_AdminCode"":""788422"",""Item_Code"":""tsp-025520"",""Item_Name"":""\u30b8\u30e3\u30a4\u30a2\u30f3\u30c8\u30d7\u30e9\u30b9 \u30da\u30f3\u30db\u30eb\u30c0\u30fc 140S\uff08025520\uff09"",""List_Price"":1512,""Sale_Price"":1209,""Cost"":812,""Release_Date"":""2015-11-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-009 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""025520""},{""Item_AdminCode"":""788423"",""Item_Code"":""tsp-025530"",""Item_Name"":""\u30b8\u30e3\u30a4\u30a2\u30f3\u30c8\u30d7\u30e9\u30b9 \u30da\u30f3\u30db\u30eb\u30c0\u30fc 160S\uff08025530\uff09"",""List_Price"":1728,""Sale_Price"":1382,""Cost"":928,""Release_Date"":""2015-11-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-009 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""025530""},{""Item_AdminCode"":""788424"",""Item_Code"":""tsp-025540"",""Item_Name"":""\u30b8\u30e3\u30a4\u30a2\u30f3\u30c8\u30d7\u30e9\u30b9 \u30da\u30f3\u30db\u30eb\u30c0\u30fc 180S\uff08025540\uff09"",""List_Price"":1944,""Sale_Price"":1555,""Cost"":1044,""Release_Date"":""2015-11-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-009 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""025540""},{""Item_AdminCode"":""788425"",""Item_Code"":""tsp-025550"",""Item_Name"":""\u30b8\u30e3\u30a4\u30a2\u30f3\u30c8\u30d7\u30e9\u30b9 \u30da\u30f3\u30db\u30eb\u30c0\u30fc 200S\uff08025550\uff09"",""List_Price"":2160,""Sale_Price"":1728,""Cost"":1160,""Release_Date"":""2015-11-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-009 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""025550""},{""Item_AdminCode"":""788426"",""Item_Code"":""tsp-026674"",""Item_Name"":""\u30b9\u30ef\u30c3\u30c8 \u30b9\u30d4\u30fc\u30c9 FL\uff0f\u30d5\u30ec\u30a2\uff08026674\uff09"",""List_Price"":10800,""Sale_Price"":8640,""Cost"":5800,""Release_Date"":""2015-12-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-010 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""026674""},{""Item_AdminCode"":""788427"",""Item_Code"":""tsp-026675"",""Item_Name"":""\u30b9\u30ef\u30c3\u30c8 \u30b9\u30d4\u30fc\u30c9 ST\uff0f\u30b9\u30c8\u30ec\u30fc\u30c8\uff08026675\uff09"",""List_Price"":10800,""Sale_Price"":8640,""Cost"":5800,""Release_Date"":""2015-12-25"",""Post_Available_Date"":null,""Year"":""2015\u5e74\u5ea6"",""Season"":""FW Q3"",""Brand_Code"":50,""Brand_Name"":""TSP"",""Brand_Code_Yahoo"":4189,""Competition_Code"":50,""Competition_Name"":""\u5353\u7403"",""Classification_Code"":10,""Class_Name"":""\u30e9\u30b1\u30c3\u30c8"",""Company_Name"":""(\u682a)\uff7b\uff9e\uff85\uff6f\uff78\uff7d"",""Catalog_Information"":""2015tsp-fw-01-010 "",""Special_Flag"":0,""Reservation_Flag"":1,""Instruction_No"":null,""Approve_Date"":""2015-11-06 16:06:00"",""Remarks"":"""",""Product_Code"":""026675""}],""sku"":null,""inventory"":[{""Item_AdminCode"":489818,""Item_Code"":""miz-2ya-357-358"",""Quantity"":0,""Jisha_Quantity"":0,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":489819,""Item_Code"":""miz-2ya-357-358"",""Quantity"":0,""Jisha_Quantity"":0,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":489820,""Item_Code"":""miz-2ya-357-358"",""Quantity"":23,""Jisha_Quantity"":0,""Maker_Quantity"":23,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":489821,""Item_Code"":""miz-2ya-357-358"",""Quantity"":2,""Jisha_Quantity"":0,""Maker_Quantity"":2,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":421142,""Item_Code"":""miz-2ya-357-358"",""Quantity"":0,""Jisha_Quantity"":0,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":421143,""Item_Code"":""miz-2ya-357-358"",""Quantity"":0,""Jisha_Quantity"":0,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":421144,""Item_Code"":""miz-2ya-357-358"",""Quantity"":0,""Jisha_Quantity"":0,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":421145,""Item_Code"":""miz-2ya-357-358"",""Quantity"":0,""Jisha_Quantity"":0,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":489898,""Item_Code"":""miz-2yl-557-558"",""Quantity"":48,""Jisha_Quantity"":0,""Maker_Quantity"":48,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":489899,""Item_Code"":""miz-2yl-557-558"",""Quantity"":11,""Jisha_Quantity"":0,""Maker_Quantity"":11,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":489900,""Item_Code"":""miz-2yl-557-558"",""Quantity"":4,""Jisha_Quantity"":0,""Maker_Quantity"":4,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":489901,""Item_Code"":""miz-2yl-557-558"",""Quantity"":8,""Jisha_Quantity"":0,""Maker_Quantity"":8,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":421172,""Item_Code"":""miz-2yl-557-558"",""Quantity"":0,""Jisha_Quantity"":0,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":421173,""Item_Code"":""miz-2yl-557-558"",""Quantity"":24,""Jisha_Quantity"":1,""Maker_Quantity"":23,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":489897,""Item_Code"":""miz-2yl-557-558"",""Quantity"":3,""Jisha_Quantity"":0,""Maker_Quantity"":3,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":421174,""Item_Code"":""miz-2yl-557-558"",""Quantity"":23,""Jisha_Quantity"":0,""Maker_Quantity"":23,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":421421,""Item_Code"":""miz-2zk-470-473"",""Quantity"":40,""Jisha_Quantity"":0,""Maker_Quantity"":30,""Toyonaka_Quantity"":10,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":421422,""Item_Code"":""miz-2zk-470-473"",""Quantity"":177,""Jisha_Quantity"":0,""Maker_Quantity"":170,""Toyonaka_Quantity"":7,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":444529,""Item_Code"":""miz-2zk-470-473"",""Quantity"":410,""Jisha_Quantity"":0,""Maker_Quantity"":400,""Toyonaka_Quantity"":10,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":444530,""Item_Code"":""miz-2zk-470-473"",""Quantity"":120,""Jisha_Quantity"":0,""Maker_Quantity"":120,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":444531,""Item_Code"":""miz-2zk-470-473"",""Quantity"":397,""Jisha_Quantity"":0,""Maker_Quantity"":389,""Toyonaka_Quantity"":8,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":417949,""Item_Code"":""miz-2zk-470-473"",""Quantity"":368,""Jisha_Quantity"":0,""Maker_Quantity"":360,""Toyonaka_Quantity"":8,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":417952,""Item_Code"":""miz-2zk-470-473"",""Quantity"":1059,""Jisha_Quantity"":0,""Maker_Quantity"":1050,""Toyonaka_Quantity"":9,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":444532,""Item_Code"":""miz-2zk-470-473"",""Quantity"":140,""Jisha_Quantity"":0,""Maker_Quantity"":129,""Toyonaka_Quantity"":11,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":444533,""Item_Code"":""miz-2zk-470-473"",""Quantity"":139,""Jisha_Quantity"":0,""Maker_Quantity"":130,""Toyonaka_Quantity"":9,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":444534,""Item_Code"":""miz-2zk-470-473"",""Quantity"":40,""Jisha_Quantity"":0,""Maker_Quantity"":40,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":417948,""Item_Code"":""miz-2zk-470-473"",""Quantity"":416,""Jisha_Quantity"":0,""Maker_Quantity"":410,""Toyonaka_Quantity"":6,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":417951,""Item_Code"":""miz-2zk-470-473"",""Quantity"":964,""Jisha_Quantity"":0,""Maker_Quantity"":960,""Toyonaka_Quantity"":4,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":421423,""Item_Code"":""miz-2zk-470-473"",""Quantity"":145,""Jisha_Quantity"":7,""Maker_Quantity"":130,""Toyonaka_Quantity"":8,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":444535,""Item_Code"":""miz-2zk-470-473"",""Quantity"":264,""Jisha_Quantity"":0,""Maker_Quantity"":250,""Toyonaka_Quantity"":14,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":417946,""Item_Code"":""miz-2zk-470-473"",""Quantity"":236,""Jisha_Quantity"":0,""Maker_Quantity"":230,""Toyonaka_Quantity"":6,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":417947,""Item_Code"":""miz-2zk-470-473"",""Quantity"":235,""Jisha_Quantity"":3,""Maker_Quantity"":230,""Toyonaka_Quantity"":2,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":417950,""Item_Code"":""miz-2zk-470-473"",""Quantity"":322,""Jisha_Quantity"":0,""Maker_Quantity"":310,""Toyonaka_Quantity"":12,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":444536,""Item_Code"":""miz-2zk-470-473"",""Quantity"":162,""Jisha_Quantity"":0,""Maker_Quantity"":160,""Toyonaka_Quantity"":2,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":444537,""Item_Code"":""miz-2zk-470-473"",""Quantity"":184,""Jisha_Quantity"":9,""Maker_Quantity"":168,""Toyonaka_Quantity"":7,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":417945,""Item_Code"":""miz-2zk-470-473"",""Quantity"":225,""Jisha_Quantity"":0,""Maker_Quantity"":221,""Toyonaka_Quantity"":4,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":68612,""Item_Code"":""sr700"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":68613,""Item_Code"":""sr700"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":68614,""Item_Code"":""sr700"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":68615,""Item_Code"":""sr700"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":68616,""Item_Code"":""sr700"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":68617,""Item_Code"":""sr700"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788417,""Item_Code"":""tsp-010045"",""Quantity"":999,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":598453,""Item_Code"":""tsp-011050"",""Quantity"":999999,""Jisha_Quantity"":0,""Maker_Quantity"":100,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":702494,""Item_Code"":""tsp-014025"",""Quantity"":999999,""Jisha_Quantity"":0,""Maker_Quantity"":100,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788418,""Item_Code"":""tsp-025480"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788419,""Item_Code"":""tsp-025490"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788420,""Item_Code"":""tsp-025500"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788421,""Item_Code"":""tsp-025510"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788422,""Item_Code"":""tsp-025520"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788423,""Item_Code"":""tsp-025530"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788424,""Item_Code"":""tsp-025540"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788425,""Item_Code"":""tsp-025550"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788426,""Item_Code"":""tsp-026674"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788427,""Item_Code"":""tsp-026675"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788428,""Item_Code"":""tsp-026684"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788429,""Item_Code"":""tsp-026685"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788430,""Item_Code"":""tsp-026693"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788431,""Item_Code"":""tsp-026694"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788307,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788314,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788321,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788308,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788315,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788322,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788309,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788316,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788323,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788310,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788317,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788324,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788311,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788318,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788325,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788312,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788319,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788326,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788313,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788320,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788327,""Item_Code"":""tsp-031412"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788328,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788335,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788342,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788329,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788336,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788343,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788330,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788337,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788344,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788331,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788338,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788345,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788332,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788339,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788346,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788333,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788340,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788347,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788334,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788341,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788348,""Item_Code"":""tsp-031413"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788349,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788357,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788365,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788350,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788358,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788366,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788351,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788359,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788367,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788352,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788360,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788368,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788353,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788361,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788369,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788354,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788362,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788370,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788355,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788363,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788371,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788356,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788364,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788372,""Item_Code"":""tsp-032407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788373,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788380,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788374,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788381,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788375,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788382,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788376,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788383,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788377,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788384,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788378,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788385,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788379,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788386,""Item_Code"":""tsp-033884"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788387,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788394,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788388,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788395,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788389,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788396,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788390,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788397,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788391,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788398,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788392,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788399,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788393,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788400,""Item_Code"":""tsp-033885"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788403,""Item_Code"":""tsp-042407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788404,""Item_Code"":""tsp-042407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788405,""Item_Code"":""tsp-042407"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788406,""Item_Code"":""tsp-042408"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788407,""Item_Code"":""tsp-042408"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788408,""Item_Code"":""tsp-042408"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788401,""Item_Code"":""tsp-044405"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788402,""Item_Code"":""tsp-044405"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788409,""Item_Code"":""tsp-044712"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788410,""Item_Code"":""tsp-044712"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788411,""Item_Code"":""tsp-044713"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788412,""Item_Code"":""tsp-044713"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788413,""Item_Code"":""tsp-044714"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788414,""Item_Code"":""tsp-044714"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788415,""Item_Code"":""tsp-044715"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788416,""Item_Code"":""tsp-044715"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":593392,""Item_Code"":""tsp-14035"",""Quantity"":999999,""Jisha_Quantity"":304,""Maker_Quantity"":100,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":706550,""Item_Code"":""vic-020451"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":706473,""Item_Code"":""vic-020451"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":706472,""Item_Code"":""vic-020451"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":706474,""Item_Code"":""vic-020451"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":706475,""Item_Code"":""vic-020461"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":706477,""Item_Code"":""vic-020461"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":706476,""Item_Code"":""vic-020461"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":706478,""Item_Code"":""vic-020461"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788284,""Item_Code"":""vic-026664"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788283,""Item_Code"":""vic-026665"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788285,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788292,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788299,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788286,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788293,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788300,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788287,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788294,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788301,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788288,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788295,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788302,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788289,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788296,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788303,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788290,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788297,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788304,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788291,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788298,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788305,""Item_Code"":""vic-031454"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788306,""Item_Code"":""vic-042700"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788432,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484298,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788433,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484297,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788434,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484295,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788435,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484294,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788436,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484296,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788437,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484299,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788438,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484300,""Item_Code"":""vic-33135"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788439,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484305,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788440,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484304,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788441,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484302,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788442,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484301,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788443,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484303,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788444,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484306,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":788445,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0},{""Item_AdminCode"":484307,""Item_Code"":""vic-33145"",""Quantity"":0,""Jisha_Quantity"":null,""Maker_Quantity"":null,""Toyonaka_Quantity"":null,""Ishibashi_Quantity"":0,""Esaka_Quantity"":0,""Sannomiya_Quantity"":0}]}";
        ////        //WebClient client = new WebClient();
        ////        //Stream stream = client.OpenRead("http://localhost:49811/WebForms/Import/Json_Import.ashx?i=item&&s=sku&&q=inventory");
        ////        //StreamReader reader = new StreamReader(stream);
        ////        //var response = reader.ReadToEnd();
        ////        //var json = JObject.Parse(response);
        ////        //string accessToken = json.Value<string>("vc_user_shohin");
        ////        //Item objUser = Deserialize<Item>(context);
        ////         string response = new StreamReader(context.Request.InputStream).ReadToEnd();
        ////         if (!string.IsNullOrWhiteSpace(response))
        ////         {
        ////             DataTable dtItem = Item_Data(response);
        ////             DataTable dtSKU = SKU_Data(response);
        ////             DataTable dtInventory = Inventory_Data(response);

        ////             if (dtItem.Rows.Count > 0 && dtItem != null)
        ////             {
        ////                 //Inventory(dt_Item);
        ////                 //dtItem = Master_Check(dtItem);

        ////                 string xml = CreateXml(dtItem);
        ////                 string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        ////                 SqlConnection connectionString = new SqlConnection(connection);
        ////                 SqlCommand cmd = new SqlCommand("SP_ItemMaster_Import_XML", connectionString);
        ////                 cmd.CommandType = CommandType.StoredProcedure;
        ////                 cmd.CommandTimeout = 0;
        ////                 cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
        ////                 cmd.Connection.Open();
        ////                 cmd.ExecuteNonQuery();
        ////                 cmd.Connection.Close();
        ////                 cmd.Dispose();
        ////             }

        ////             if (dtSKU.Rows.Count > 0 && dtSKU != null)
        ////             {
        ////                 //Inventory(dt_Item);
        ////                 string xml = CreateXml(dtSKU);
        ////                 string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        ////                 SqlConnection connectionString = new SqlConnection(connection);
        ////                 SqlCommand cmd = new SqlCommand("SP_Item_SKUUpdate_Import_XML", connectionString);
        ////                 cmd.CommandType = CommandType.StoredProcedure;
        ////                 cmd.CommandTimeout = 0;
        ////                 cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
        ////                 cmd.Connection.Open();
        ////                 cmd.ExecuteNonQuery();
        ////                 cmd.Connection.Close();
        ////                 cmd.Dispose();
        ////             }

        ////             if (dtInventory.Rows.Count > 0 && dtInventory != null)
        ////             {
        ////                 //Inventory(dt_Item);
        ////                 string xml = CreateXml(dtInventory);
        ////                 string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        ////                 SqlConnection connectionString = new SqlConnection(connection);
        ////                 SqlCommand cmd = new SqlCommand("SP_Inventory_Import_XML", connectionString);
        ////                 cmd.CommandType = CommandType.StoredProcedure;
        ////                 cmd.CommandTimeout = 0;
        ////                 cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
        ////                 cmd.Connection.Open();
        ////                 cmd.ExecuteNonQuery();
        ////                 cmd.Connection.Close();
        ////                 cmd.Dispose();
        ////             }
        ////         }
        ////}
        ////catch (Exception ex)
        ////{
        ////    string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        ////    SqlConnection con = new SqlConnection(connection);
        ////    SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
        ////    cmd.CommandType = CommandType.StoredProcedure;
        ////    cmd.Parameters.AddWithValue("@UserID", -1);
        ////    cmd.Parameters.AddWithValue("@ErrorDetail", "Json Import: " + ex.ToString());
        ////    cmd.Connection.Open();
        ////    cmd.ExecuteNonQuery();
        ////    cmd.Connection.Close();
        ////}
        ////}

        ////public bool IsReusable
        ////{
        ////    get
        ////    {
        ////        return false;
        ////    }
        ////}

        ////public DataTable Item_Data(string response)
        ////{
        ////    try
        ////    {
        ////        /*
        ////        string connect = ConfigurationManager.ConnectionStrings["MakerConnectionString"].ToString();
        ////        SqlConnection connMaker = new SqlConnection(connect);
        ////        //SqlDataAdapter da = new SqlDataAdapter("SELECT vc_user_shohin,vc_usize,vc_ucolor,vm_size,vm_color,no_commit_zaiko,no_maker_zaiko, no_dealer_zaiko FROM Maker_Information", connMaker);
        ////        SqlDataAdapter da = new SqlDataAdapter("SP_Item_Master_Data", connMaker);
        ////        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        ////        da.SelectCommand.CommandTimeout = 0;
        ////        DataTable dt_Item = new DataTable();
        ////        da.SelectCommand.Connection.Open();
        ////        da.Fill(dt_Item);
        ////        da.SelectCommand.Connection.Close();
        ////        return dt_Item;
        ////        */
        ////        IDictionary<string, ICollection<Item>> items;
        ////        items = JsonConvert.DeserializeObject<IDictionary<string, ICollection<Item>>>(response);
        ////        var dataSet = new DataSet();
        ////        var dataTable = dataSet.Tables.Add();
        ////        //dataTable.Columns.Add("from");
        ////        dataTable.Columns.Add("Item_AdminCode");
        ////        dataTable.Columns.Add("Item_Code");
        ////        dataTable.Columns.Add("Item_Name");
        ////        dataTable.Columns.Add("List_Price");
        ////        dataTable.Columns.Add("Sale_Price");
        ////        dataTable.Columns.Add("Cost");
        ////        dataTable.Columns.Add("Release_Date");
        ////        dataTable.Columns.Add("Post_Available_Date");
        ////        dataTable.Columns.Add("Year");
        ////        dataTable.Columns.Add("Season");
        ////        dataTable.Columns.Add("Brand_Code");
        ////        dataTable.Columns.Add("Brand_Name");
        ////        dataTable.Columns.Add("Brand_Code_Yahoo");
        ////        dataTable.Columns.Add("Competition_Code");
        ////        dataTable.Columns.Add("Competition_Name");
        ////        dataTable.Columns.Add("Classification_Code");
        ////        dataTable.Columns.Add("Class_Name");
        ////        dataTable.Columns.Add("Company_Name");
        ////        dataTable.Columns.Add("Catalog_Information");
        ////        dataTable.Columns.Add("Special_Flag");
        ////        dataTable.Columns.Add("Reservation_Flag");
        ////        dataTable.Columns.Add("Instruction_No");
        ////        dataTable.Columns.Add("Approve_Date");
        ////        dataTable.Columns.Add("Remarks");
        ////        dataTable.Columns.Add("Product_Code");

        ////        foreach (var itemsGroup in items)
        ////        {
        ////            // foreach TValue in ICollection<TValue>
        ////            if (itemsGroup.Key == "item" && itemsGroup.Value != null)
        ////            {
        ////                foreach (var item in itemsGroup.Value)
        ////                {
        ////                    dataTable.LoadDataRow(
        ////                        new object[]
        ////                {
        ////                    //itemsGroup.Key,
        ////                    item.Item_AdminCode,
        ////                    item.Item_Code,
        ////                    item.Item_Name,
        ////                    item.List_Price,
        ////                    item.Sale_Price,
        ////                    item.Cost,
        ////                    item.Release_Date,
        ////                    item.Post_Available_Date,
        ////                    item.Year,
        ////                    item.Season,
        ////                    item.Brand_Code,
        ////                    item.Brand_Name,
        ////                    item.Brand_Code_Yahoo,
        ////                    item.Competition_Code,
        ////                    item.Competition_Name,
        ////                    item.Classification_Code,
        ////                    item.Class_Name,
        ////                    item.Company_Name,
        ////                    item.Catalog_Information,
        ////                    item.Special_Flag,
        ////                    item.Reservation_Flag,
        ////                    item.Instruction_No,
        ////                    item.Approve_Date,
        ////                    item.Remarks,
        ////                    item.Product_Code
        ////                },
        ////                        LoadOption.PreserveChanges);
        ////                }
        ////            }
        ////        }

        ////        int count = dataTable.Rows.Count;
        ////        return dataTable;
        ////    }

        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}


        ////        IDictionary<string, ICollection<SKU>> items;
        ////        items = JsonConvert.DeserializeObject<IDictionary<string, ICollection<SKU>>>(response);
        ////        var dataSet = new DataSet();
        ////        var dataTable = dataSet.Tables.Add();
        ////        //dataTable.Columns.Add("from");
        ////        dataTable.Columns.Add("Item_AdminCode");
        ////        dataTable.Columns.Add("Item_Code");
        ////        dataTable.Columns.Add("Color_Name");
        ////        dataTable.Columns.Add("Size_Name");
        ////        dataTable.Columns.Add("Color_Code");
        ////        dataTable.Columns.Add("Size_Code");
        ////        dataTable.Columns.Add("Color_Name_Official");
        ////        dataTable.Columns.Add("Size_Name_Official");
        ////        dataTable.Columns.Add("JAN_Code");

        ////        foreach (var itemsGroup in items)
        ////        {
        ////            // foreach TValue in ICollection<TValue>
        ////            if (itemsGroup.Key == "sku" && itemsGroup.Value != null)
        ////            {
        ////                foreach (var item in itemsGroup.Value)
        ////                {
        ////                    dataTable.LoadDataRow(
        ////                        new object[]
        ////                    {
        ////                        //itemsGroup.Key,
        ////                        item.Item_AdminCode,
        ////                        item.Item_Code,
        ////                        item.Color_Name,
        ////                        item.Size_Name,
        ////                        item.Color_Code,
        ////                        item.Size_Code,
        ////                        item.Color_Name_Official,
        ////                        item.Size_Name_Official,
        ////                        item.JAN_Code,
        ////                    },
        ////                        LoadOption.PreserveChanges);
        ////                }
        ////            }
        ////        }
        ////        int count = dataTable.Rows.Count;
        ////        return dataTable;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}

        ////public DataTable Inventory_Data(string response)
        ////{
        ////    try
        ////    {
        ////        /*
        ////        string connect = ConfigurationManager.ConnectionStrings["MakerConnectionString"].ToString();
        ////        SqlConnection connMaker = new SqlConnection(connect);
        ////        SqlDataAdapter da = new SqlDataAdapter("SP_Quantity_Data", connMaker);
        ////        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        ////        da.SelectCommand.CommandTimeout = 0;
        ////        DataTable dt_Item = new DataTable();
        ////        da.SelectCommand.Connection.Open();
        ////        da.Fill(dt_Item);
        ////        da.SelectCommand.Connection.Close();
        ////        return dt_Item;
        ////        */
        ////        IDictionary<string, ICollection<Inventory>> items;
        ////        items = JsonConvert.DeserializeObject<IDictionary<string, ICollection<Inventory>>>(response);
        ////        if (items.Count > 0)
        ////        {
        ////            var dataSet = new DataSet();
        ////            var dataTable = dataSet.Tables.Add();
        ////            //dataTable.Columns.Add("from");
        ////            dataTable.Columns.Add("Item_AdminCode");
        ////            dataTable.Columns.Add("Item_Code");
        ////            dataTable.Columns.Add("Quantity");
        ////            dataTable.Columns.Add("CSV_FileName");
        ////            dataTable.Columns.Add("Jisha_Quantity");
        ////            dataTable.Columns.Add("Maker_Quantity");
        ////            dataTable.Columns.Add("Toyonaka_Quantity");
        ////            dataTable.Columns.Add("Ishibashi_Quantity");
        ////            dataTable.Columns.Add("Esaka_Quantity");
        ////            dataTable.Columns.Add("Sannomiya_Quantity");

        ////            foreach (var itemsGroup in items)
        ////            {
        ////                if (itemsGroup.Key == "inventory" && itemsGroup.Value != null)
        ////                {
        ////                    // foreach TValue in ICollection<TValue>
        ////                    foreach (var item in itemsGroup.Value)
        ////                    {
        ////                        dataTable.LoadDataRow(
        ////                            new object[]
        ////                {
        ////                    //itemsGroup.Key,
        ////                    item.Item_AdminCode,
        ////                    item.Item_Code,
        ////                    item.Quantity,
        ////                    "link",
        ////                    item.Jisha_Quantity,
        ////                    item.Maker_Quantity,
        ////                    item.Toyonaka_Quantity,
        ////                    item.Ishibashi_Quantity,
        ////                    item.Esaka_Quantity,
        ////                    item.Sannomiya_Quantity
        ////                },
        ////                     LoadOption.PreserveChanges);
        ////                    }
        ////                }
        ////            }
        ////            int count = dataTable.Rows.Count;
        ////            return dataTable;
        ////        }
        ////        return new DataTable();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}

        ////public string CreateXml(DataTable dt)
        ////{
        ////    dt.TableName = "test";
        ////    System.IO.StringWriter writer = new System.IO.StringWriter();
        ////    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
        ////    string result = writer.ToString();
        ////    result = result.Replace("&#", "$CapitalSports$");
        ////    return result;
        ////}

        ////public T Deserialize<T>(HttpContext context)
        ////{
        ////    //read the json string
        ////    string jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();

        ////    //cast to specified objectType
        ////    var obj = (T)new JavaScriptSerializer().Deserialize<T>(jsonData);

        ////    //return the object
        ////    return obj;
        ////}

        //////public DataTable Master_Check(DataTable dt)
        //////{
        //////    DataColumn newcol = new DataColumn("チェック", typeof(String));
        //////    newcol.DefaultValue = "";
        //////    dt.Columns.Add(newcol);//add check column to datatable that show error or not
        //////    DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
        //////    newColumn.DefaultValue = 6;//type 6 = ok , type 5 = error
        //////    dt.Columns.Add(newColumn);//add type column to datatable to seperate error record and  ok record

        //////    DataColumn dc = new DataColumn("エラー内容", typeof(String));
        //////    dc.DefaultValue = "";
        //////    dt.Columns.Add(dc);//add error detail column to datatabel to show error message detail

        //////    String[] colTypeCheck = { "List_Price", "Sale_Price", "Cost", "Special_Flag", "Reservation_Flag" };//need to check this column's value is integer 
        //////    DataTable dterrcheck = checkIntType(dt, colTypeCheck, 0);

        //////    String[] colLengthCheck2 = { "Product_Code" };//need to check this column value's length is greater than 100
        //////    dterrcheck = CheckLength(dterrcheck, colLengthCheck2, 100, 0);

        //////    String[] colLengthCheck3 = { "Item_AdminCode" };//need to check this column value's length is greater than 1300
        //////    dterrcheck = CheckLength(dterrcheck, colLengthCheck3, 1300, 0);

        //////    String[] colLengthCheck4 = { "Item_Code" };//need to check this column value's length is greater than 32
        //////    dterrcheck = CheckLength(dterrcheck, colLengthCheck4, 32, 0);

        //////    String[] colLengthCheck5 = { "Item_Name" };//need to check this column value's length is greater than 255
        //////    dterrcheck = CheckLength(dterrcheck, colLengthCheck5, 255, 0);

        //////    String[] colLengthCheck6 = { "Brand_Code", "Competition_Code", "Class_Code" };//need to check this column value's length is greater than 4
        //////    dterrcheck = CheckLength(dterrcheck, colLengthCheck6, 4, 0);

        //////    String[] colLengthCheck7 = { "Season" };//need to check this column value's length is greater than 40
        //////    dterrcheck = CheckLength(dterrcheck, colLengthCheck7, 40, 0);

        //////    String[] colLengthCheck8 = { "Brand_Name", "Competition_Name", "Class_Name", "Company_Name" };//need to check this column value's length is greater than 200
        //////    dterrcheck = CheckLength(dterrcheck, colLengthCheck8, 200, 0);

        //////    String[] colLengthCheck9 = { "Brand_Code_Yahoo" };//need to check this column value's length is greater than 6
        //////    dterrcheck = CheckLength(dterrcheck, colLengthCheck9, 6, 0);

        //////    String[] colLengthCheck10 = { "Catalog_Info", "Remarks" };//need to check this column value's length is greater than 3000
        //////    dterrcheck = CheckLength(dterrcheck, colLengthCheck10, 3000, 0);

        //////    String[] colLengthCheck11 = { "Instruction_No" };//need to check this column value's length is greater than 4000
        //////    dterrcheck = CheckLength(dterrcheck, colLengthCheck11, 4000, 0);

        //////    String[] colLengthCheck12 = { "Year" };//need to check this column value's length is greater than 20
        //////    dterrcheck = CheckLength(dterrcheck, colLengthCheck12, 20, 0);

        //////    String[] colDateCheck = { "Release_Date", "Post_Available_Date", "Approve_Date" };//need to check this column value's is date time 
        //////    dterrcheck = CheckDate(dterrcheck, colDateCheck, 0);

        //////    return dterrcheck;
        //////}

        //////protected DataTable checkIntType(DataTable dt, String[] col, int type)
        //////{
        //////    try
        //////    {
        //////        for (int i = 0; i < dt.Rows.Count; i++)
        //////        {
        //////            for (int j = 0; j < col.Length; j++)
        //////            {
        //////                try
        //////                {
        //////                    if (!String.IsNullOrWhiteSpace(dt.Rows[i][col[j]].ToString()))
        //////                        Convert.ToInt32(dt.Rows[i][col[j]].ToString());//check integer or not**(convert value to int-- if ok --> integer error occur -- go to cache)
        //////                }
        //////                catch (Exception)//if not integer
        //////                {
        //////                    dt.Rows[i]["チェック"] = "エラー";//error 
        //////                    dt.Rows[i]["エラー内容"] = EngToJpHeader(col[j], type) + "のフォーマットが不正です。";//error detail
        //////                    dt.Rows[i]["Type"] = 5;//error type
        //////                }
        //////            }
        //////        }
        //////        return dt;
        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        return dt;
        //////    }
        //////}

        ////public class Item
        ////{
        ////    public string Item_AdminCode{ get; set; }
        ////    public string Item_Code{ get; set; }
        ////    public string Item_Name{ get; set; }
        ////    public string List_Price{ get; set; }
        ////    public string Sale_Price{ get; set; }
        ////    public string Cost{ get; set; }
        ////    public string Release_Date{ get; set; }
        ////    public string Post_Available_Date{ get; set; }
        ////    public string Year{ get; set; }
        ////    public string Season{ get; set; }
        ////    public string Brand_Code { get; set; }
        ////    public string Brand_Name{ get; set; }
        ////    public string Brand_Code_Yahoo { get; set; }
        ////    public string Competition_Code { get; set; }
        ////    public string Competition_Name { get; set; }
        ////    public string Classification_Code { get; set; }
        ////    public string Class_Name { get; set; }
        ////    public string Company_Name { get; set; }
        ////    public string Catalog_Information { get; set; }
        ////    public string Special_Flag { get; set; }
        ////    public string Reservation_Flag { get; set; }
        ////    public string Instruction_No { get; set; }
        ////    public string Approve_Date { get; set; }
        ////    public string Remarks { get; set; }
        ////    public string Product_Code { get; set; }
        ////}

        ////public class SKU
        ////{
        ////    public string Item_AdminCode;
        ////    public string Item_Code;
        ////    public string Color_Name;
        ////    public string Size_Name;
        ////    public string Color_Code;
        ////    public string Size_Code;
        ////    public string Color_Name_Official;
        ////    public string Size_Name_Official;
        ////    public string JAN_Code;
        ////}

        ////public class Inventory
        ////{
        ////    public string Item_AdminCode;
        ////    public string Item_Code;
        ////    public string Quantity;
        ////    public string Jisha_Quantity;
        ////    public string Maker_Quantity;
        ////    public string Toyonaka_Quantity;
        ////    public string Ishibashi_Quantity;
        ////    public string Esaka_Quantity;
        ////    public string Sannomiya_Quantity;
        ////}
        static void ConsoleWriteLine_ToFile(string traceText)
        {
            string Json = ConfigurationManager.AppSettings["JSONImport"].ToString();
            StreamWriter sw = new StreamWriter(Json + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
    }
}