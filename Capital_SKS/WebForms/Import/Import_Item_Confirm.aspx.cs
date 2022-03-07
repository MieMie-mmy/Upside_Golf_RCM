/* 
Created By              : Kyaw Thet Paing
Created Date          : /2014
Updated By             :
Updated Date         :

 Tables using           : Item_ImportLog 
 *                                -Item_Import_ItemLog
    -
    -
    -
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ORS_RCM_BL;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;

namespace ORS_RCM.WebForms.Import
{
    public partial class Import_Item_Confirm : System.Web.UI.Page
    {
        Import_Item_BL itbl = new Import_Item_BL();
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        public String LogIds = String.Empty;

        public int Userid
        {
            get
            {
                if (Session["User_ID"] != null)

                    return Int32.Parse(Session["User_ID"].ToString());
                else
                    return 0;
            }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["Master"] != null)//check master file selected
                    {
                        String master = Request.QueryString["Master"].ToString();
                        if (master == "")
                        {
                            headermaster.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                            divmaster.Visible = false;
                        }
                        if (!String.IsNullOrWhiteSpace(master))
                            ItemMaster(master);
                    }
                    if (Request.QueryString["Sku"] != null)//check sku file selected
                    {
                        String sku = Request.QueryString["Sku"].ToString();
                        if (sku == "")
                        {
                            headersku.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                            divsku.Visible = false;
                        }
                        if (!String.IsNullOrWhiteSpace(sku))
                            Sku(sku);
                    }
                    if (Request.QueryString["Inventory"] != null)//check inventory file selected
                    {
                        String inventory = Request.QueryString["Inventory"].ToString();
                        if (inventory == "")
                        {
                            headerinventory.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                            divinventory.Visible = false;
                        }
                        if (!String.IsNullOrWhiteSpace(inventory))
                            Inventory(inventory);
                    }
                    if (Request.QueryString["tagID"] != null)                  // added by ETZ for sks-390 TagID
                    {
                        String tagID = Request.QueryString["tagID"].ToString();
                        if (tagID == "")
                        {
                            headertagID.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                            divtagID.Visible = false;
                        }
                        if (!String.IsNullOrWhiteSpace(tagID))
                        {
                            Tag_ID(tagID);
                        }
                    }
                    if (Request.QueryString["Monotaro"] != null)
                    {
                        String monotaro = Request.QueryString["Monotaro"].ToString();
                        if (monotaro == "")
                        {
                            headertagID.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                            divmonotaro.Visible = false;
                        }
                        if (!String.IsNullOrWhiteSpace(monotaro))
                        {
                            Monotaro(monotaro);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ItemMaster(String master)
        {
            try
            {
                String MasterPath = Server.MapPath("~/Import_CSV/") + master;//file path
                DataTable dt = GlobalUI.CSVToTable(MasterPath);//read csv file and return to datatable
                dt = GlobalUI.Remove_Doublecode(dt);//remove double code on header column

                String[] colName = { 
                    "販売管理番号", "商品番号", "商品名", "定価", "販売価格", "楽天価格(税抜)","Yahoo価格(税抜)","Wowma価格(税抜)","自社価格(税抜)","ORS価格(税抜)",
                    "原価", "発売日", "掲載可能日", "年度", "シーズン", 
                    "ブランドコード", "ブランド名", "ヤフーブランドコード", "競技コード", "競技名", 
                    "分類コード","分類名","仕入先名","カタログ情報","特記フラグ",
                    "予約フラグ","指示書番号","承認日","備考","メーカー商品コード","販売単位","内容量数1","内容量単位1","内容量数2","内容量単位2","PC用キャッチコピー","モバイル用キャッチコピー"};//true csv header name

                if (CheckColumn(colName, dt))//check datatable column's header is true
                {
                    dt = ChangeItemMasterHeader(dt);//change japanese header to english
                    DataColumn newcol = new DataColumn("チェック", typeof(String));
                    newcol.DefaultValue = "";
                    dt.Columns.Add(newcol);//add check column to datatable that show error or not
                    DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
                    newColumn.DefaultValue = 6;//type 6 = ok , type 5 = error
                    dt.Columns.Add(newColumn);//add type column to datatable to seperate error record and  ok record

                    DataColumn dc = new DataColumn("エラー内容", typeof(String));
                    dc.DefaultValue = "";
                    dt.Columns.Add(dc);//add error detail column to datatabel to show error message detail


                    String[] colItem_Code = { "Item_Code" };
                    DataTable dterrcheck = checkItemCode(dt, colItem_Code,0);

                    String[] colTypeCheck = { "List_Price", "Sale_Price", "RakutenPrice", "YahooPrice", "WowmaPrice", "JishaPrice", "TennisPrice", "Cost", "Special_Flag", "Reservation_Flag" };//need to check item_code format
                    dterrcheck = checkIntType(dt, colTypeCheck, 0);

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
                   
                    gvitemmaster.DataSource = dterrcheck;
                 
                    Cache.Insert("dtmasterok", dterrcheck,null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);//save datatable in cache
                    gvitemmaster.DataBind();
                }
                else
                {
                    GlobalUI.MessageBox("File Format Wrong!");
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void Sku(String sku)
        {
            try
            {
                String SkuPath = Server.MapPath("~/Import_CSV/") + sku;//file path
                DataTable dt = GlobalUI.CSVToTable(SkuPath);//read csv file and return to datatable
                dt = GlobalUI.Remove_Doublecode(dt);//remove double code on header column

                //true csv header name
                String[] colName = { "販売管理番号", "商品番号", "カラー名", "サイズ名", "カラーコード", "サイズコード", "カラー正式名称", "サイズ正式名称", "JANコード" };
                if (CheckColumn(colName, dt))//check datatable column's header is true
                {
                    dt = ChangeSKUHeader(dt);//change japanese header to english
                    DataColumn newcol = new DataColumn("チェック", typeof(String));//add check column to datatable that show error or not
                    newcol.DefaultValue = "";
                    dt.Columns.Add(newcol);//add check column to datatable that show error or not
                    DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
                    newColumn.DefaultValue = 6;//type 6 = ok , type 5 = error
                    dt.Columns.Add(newColumn);//add type column to datatable to seperate error record and  ok record

                    DataColumn dc = new DataColumn("エラー内容", typeof(String));
                    dc.DefaultValue = "";
                    dt.Columns.Add(dc);//add error detail column to datatabel to show error message detail

                    String[] colCheckLength = { "Item_Code", "Color_Code", "Size_Code", "JAN_Code" };//need to check this column value's length is greater than 50
                    DataTable dterr = CheckLength(dt, colCheckLength, 50, 1);

                    String[] colCheckLength1 = { "Color_Name", "Size_Name", "Color_Name_Official", "Size_Name_Official" };//need to check this column value's length is greater than 200
                    dterr = CheckLength(dterr, colCheckLength1, 200, 1);

                    //String[] colItemAdminCode = {"Item_AdminCode"};
                    //dterr = Add_ItemAdminCode (dterr ,colItemAdminCode);


                    gvsku.DataSource = dterr;
                    

                 
                    Cache.Insert("dtskuok", dterr, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);//save datatable to cache
                    gvsku.DataBind();
                }
                else
                {
                    GlobalUI.MessageBox("File Format Wrong!");
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        //public DataTable  Add_ItemAdminCode(DataTable dt,String[] col)
        //{
        //    try
        //    {
        //        DataTable dtcode = itbl.ItemAdminCode_Select();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            string code = dt.Rows[i]["Item_AdminCode"].ToString();
        //            string admincode = string.Empty;
        //            if (code == "")
        //            {
        //                admincode = dtcode.Rows[0]["Item_AdminCode"].ToString();
        //                dt.Rows[i]["Item_AdminCode"] = admincode;
        //            }
        //        }

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["Exception"] = ex.ToString();
        //        Response.Redirect("~/CustomErrorPage.aspx?");
        //        return dt;
        //    }
            
        //}

        protected void Inventory(String inventory)
        {
            try
            {
                Import_Item_BL itbl = new Import_Item_BL();
                Item_ImportLog_BL ItemImbl = new Item_ImportLog_BL();

                String Invpath = Server.MapPath("~/Import_CSV/") + inventory;//file path
                DataTable dt = itbl.InventoryCSVToTable(Invpath, inventory);//read csv file and return to datatable
                dt = GlobalUI.Remove_Doublecode(dt);//remove double code on header column

                //String[] colName = { "販売管理番号", "商品番号", "在庫数" };//true csv header name
                String[] colName = { "販売管理番号", "商品番号", "在庫数", "自社在庫数", "モーカー在庫数", "豊中在庫数", "石橋在庫数", "江坂在庫数", "三宮在庫数" };
                if (CheckColumn(colName, dt))//check datatable column's header is true
                {
                    dt = ChangeInventoryHeader(dt);//change japanese header to english
                    DataColumn newcol = new DataColumn("チェック", typeof(String));
                    newcol.DefaultValue = "";
                    dt.Columns.Add(newcol);//add check column to datatable that show error or not
                    DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
                    newColumn.DefaultValue = 6;//type 6 = ok , type 5 = error
                    dt.Columns.Add(newColumn);//add type column to datatable to seperate error record and  ok record

                    DataColumn dc = new DataColumn("エラー内容", typeof(String));
                    dc.DefaultValue = "";
                    dt.Columns.Add(dc);//add error detail column to datatabel to show error message detail

                    String[] colCheckLength = { "Item_Code" };//need to check this column value's length is greater than 50
                    DataTable dterr = CheckLength(dt, colCheckLength, 50, 2);

                    String[] colCheckType = { "Quantity" };
                    dterr = checkIntType(dterr, colCheckType, 2);//need to check this column value is integer

                    gvInventory.DataSource = dterr;
                 
                    Cache.Insert("dtinventoryok", dterr, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);//save datatabel to cache
                    gvInventory.DataBind();
                }
                else
                {
                    GlobalUI.MessageBox("File Format Wrong!");
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        // added by ETZ for sks-390 TagID
        protected void Tag_ID(string tagID)
        {
            try
            {
                String Tagpath = Server.MapPath("~/Import_CSV/") + tagID;
                DataTable dt = GlobalUI.CSVToTable(Tagpath);
                dt = GlobalUI.Remove_Doublecode(dt);
                String[] colName = { "商品番号", "商品名", "サイズ名", "カラー名", "ディレクトリID", "パス名", "タグ名1", "タグ名2", "タグ名3", "タグ名4", "タグ名5", "タグ名6", "タグ名7", "タグ名8" };
                if (CheckTagIDColumn(colName, dt))
                {
                    DataTable dtTagvalue = ChangeTag_IDHeader(dt);
                    gdvTagID.DataSource = dtTagvalue;
                    Cache.Insert("dtTagData", dtTagvalue, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                    gdvTagID.DataBind();

                }
                else
                {
                    GlobalUI.MessageBox("File Format is wrong!!!");
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        protected void Monotaro(String monotaro)
        {
            try
            {
                String MonotaroPath = Server.MapPath("~/Import_CSV/"+monotaro);//file path
                DataTable dt = GlobalUI.Import_To_Grid(MonotaroPath,".xlsx");
                dt = GlobalUI.Remove_Doublecode(dt);//remove double code on header column
                String[] colName = {"(年間出荷数もしくは売れ筋A～Dランク)","(カテゴリ)","(注文コード)","緊急判定","商品詳細登録要否","依頼コメント","ＷＥＢ公開保留",
                                    "公開予定日","コピー元","画像コピー判定","商品グループコード","(商品グループ名)","メーカー品番","販売数","販売単位","内容量数1",
                                    "内容量単位1","内容量数2","内容量単位2","JANコード","参考基準価格","オプションコード","配送種別","返品承認要否","倉庫コード","(笠間納品可否)","(笠間納品入荷日数)",
                                    "入荷日数","出荷日数","お客様組立て","引渡方法","代引可否","返品可否","(全国)","(北海道)","(沖縄)","(離島)","公開種別","(市場売価)",
                                    "(サプライヤ名)","(メーカー名)","(ブランド名)","サプライヤ品番","最低発注数量","最低発注単位","仕入価格","(直送時代引き)",
                                    "(直送時配送不可地域)","梱包質量(kg)","梱包寸法(奥行D)(mm)","梱包寸法(幅W)(mm)","梱包寸法(高さH)(mm)","商品詳細登録コメント",
                                    "(特長)","(用途)","(注意)","(画像置き場)","(画像1)","(画像1キャプション)","(指示1)","(画像2)","(画像2キャプション)","(指示2)","(画像3)",
                                    "(画像3キャプション)","(指示3)","(画像4)","(画像4キャプション)","(指示4)","該当法令","販売許可・認可・届出","賞味期限","法令・規格",
                                    "消防法上、届出を必要とする物質","危険物の種別","危険物の品名","危険等級","危険物の性質","危険物の含有量","(属性名1)","(属性値1)",
                                    "(属性名2)","(属性値2)","(属性名3)","(属性値3)","(属性名4)","(属性値4)","(属性名5)","(属性値5)","(属性名6)","(属性値6)","(属性名7)",
                                    "(属性値7)","(属性名8)","(属性値8)","(属性名9)","(属性値9)","(属性名10)","(属性値10)","(属性名11)","(属性値11)","(属性名12)",
                                    "(属性値12)","(属性名13)","(属性値13)","(属性名14)","(属性値14)","(属性名15)","(属性値15)","(属性名16)","(属性値16)","(属性名17)",
                                    "(属性値17)","(属性名18)","(属性値18)","(属性名19)","(属性値19)","(属性名20)","(属性値20)"};//true csv header"ブランド名" name
                if (CheckColumn(colName, dt))
                {
                    dt = ChangeMonotaroHeader(dt);//change japanese header to english
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Supplier_Name"].ToString() == "御社名")
                        {
                            dt.Rows.RemoveAt(0);
                        }
                    }
                    DataColumn newcol = new DataColumn("チェック", typeof(String));
                    newcol.DefaultValue = "";
                    dt.Columns.Add(newcol);//add check column to datatable that show error or not
                    DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
                    newColumn.DefaultValue = 6;//type 6 = ok , type 5 = error
                    dt.Columns.Add(newColumn);//add type column to datatable to seperate error record and  ok record
                    DataColumn dc = new DataColumn("エラー内容", typeof(String));
                    dc.DefaultValue = "";
                    dt.Columns.Add(dc);//add error detail column to datatabel to show error message detail
                    String[] colTypeCheck = {"Number_of_sales","Sale_Price", "Delivery_type","Customer_Delivery_Type","Customer_Delivery_Day","Customer_Assembly","Delivery_Method","Cash_On_Delivery_Fee",
                                              "Return_Type","Nation_Wide","Hokkaido","Okinawa","Remote_Island", "Market_Selling_Price","Market_Selling_Price","Purchase_Price",
                                              "Sell_By", "Laws_And_Regulation","Fire_Service_Law","Dangarous_Goods","Dangarous_Goods_Name","Risk_Rating","Dangerous_Goods_Nature","Public_Type",
                                              "Minimum_Order_Quantity","Minimum_Order_Unit"};//need to check this column's value is integer 
                    DataTable dterrcheck = checkIntType(dt, colTypeCheck, 3);
                    String[] colLengthCheck = { "Dangerous_Goods_Contents" };
                    dterrcheck = CheckLength(dterrcheck, colLengthCheck, 10, 3);//need to check this column value's length is greater than 10

                    String[] colLengthCheck1 = { "Selling_Rank", "Maker_Name", "Undelivered_Area","JAN_Code" };//need to check this column value's length is greater than 50
                    dterrcheck = CheckLength(dterrcheck, colLengthCheck1, 50, 3);

                    String[] colLengthCheck8 = { "Brand_Name" };//need to check this column value's length is greater than 200
                    dterrcheck = CheckLength(dterrcheck, colLengthCheck8, 200, 3);

                    DataTable dtmono = dterrcheck.Copy();
                    dtmono.Columns.Remove("Type");
                    ChangeMonotaroEngHeader(dtmono);
                    gvmonotaro.DataSource = dtmono;
                    Cache.Insert("dtmonotaro", dterrcheck, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);//save datatable in cache
                    gvmonotaro.DataBind();
                }
                else
                {
                    GlobalUI.MessageBox("File Format Wrong!");
                }
            }

            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                ConsoleWriteLine_Tofile(ex.ToString());
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Import_Error.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        protected DataTable ChangeTag_IDHeader(DataTable dtTagID)
        {
            try
            {
                dtTagID.Columns["商品番号"].ColumnName = "Item_Code";
                dtTagID.Columns["商品名"].ColumnName = "Item_Name";
                dtTagID.Columns["カラー名"].ColumnName = "Color_Name";
                dtTagID.Columns["サイズ名"].ColumnName = "Size_Name";
                dtTagID.Columns["ディレクトリID"].ColumnName = "Rakuten_CategoryID";
                dtTagID.Columns["パス名"].ColumnName = "Path";
                dtTagID.Columns["タグ名1"].ColumnName = "Tag_Name1";
                dtTagID.Columns["タグ名2"].ColumnName = "Tag_Name2";
                dtTagID.Columns["タグ名3"].ColumnName = "Tag_Name3";
                dtTagID.Columns["タグ名4"].ColumnName = "Tag_Name4";
                dtTagID.Columns["タグ名5"].ColumnName = "Tag_Name5";
                dtTagID.Columns["タグ名6"].ColumnName = "Tag_Name6";
                dtTagID.Columns["タグ名7"].ColumnName = "Tag_Name7";
                dtTagID.Columns["タグ名8"].ColumnName = "Tag_Name8";

                return dtTagID;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dtTagID;
            }
        }

        //change japanese header to english for inventory
        protected DataTable ChangeInventoryHeader(DataTable dt)
        {
            try
            {
                dt.Columns["商品番号"].ColumnName = "Item_Code";
                dt.Columns["販売管理番号"].ColumnName = "Item_AdminCode";
                dt.Columns["在庫数"].ColumnName = "Quantity";

                dt.Columns["自社在庫数"].ColumnName = "Jisha_Quantity";
                dt.Columns["モーカー在庫数"].ColumnName = "Maker_Quantity";
                dt.Columns["豊中在庫数"].ColumnName = "Toyonaka_Quantity";
                dt.Columns["石橋在庫数"].ColumnName = "Ishibashi_Quantity";
                dt.Columns["江坂在庫数"].ColumnName = "Esaka_Quantity";
                dt.Columns["三宮在庫数"].ColumnName = "Sannomiya_Quantity";

                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }

        //change japanese header to english for sku
        protected DataTable ChangeSKUHeader(DataTable dt)
        {
            try
            {
                dt.Columns["販売管理番号"].ColumnName = "Item_AdminCode";
                dt.Columns["商品番号"].ColumnName = "Item_Code";
                dt.Columns["カラー名"].ColumnName = "Color_Name";
                dt.Columns["サイズ名"].ColumnName = "Size_Name";
                dt.Columns["カラーコード"].ColumnName = "Color_Code";
                dt.Columns["サイズコード"].ColumnName = "Size_Code";
                dt.Columns["カラー正式名称"].ColumnName = "Color_Name_Official";
                dt.Columns["サイズ正式名称"].ColumnName = "Size_Name_Official";
                dt.Columns["JANコード"].ColumnName = "JAN_Code";
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }
        protected DataTable ChangeMonotaroHeader(DataTable dt)
        {
            try
            {
                dt.Columns["(年間出荷数もしくは売れ筋A～Dランク)"].ColumnName = "Selling_Rank"; 
                dt.Columns["(カテゴリ)"].ColumnName = "Category";
                dt.Columns["(注文コード)"].ColumnName = "Order_Code";
                dt.Columns["緊急判定"].ColumnName = "Urgent_Judgment";
                dt.Columns["商品詳細登録要否"].ColumnName = "Product_Details_Registration";
                dt.Columns["依頼コメント"].ColumnName = "Request_Comment";
                dt.Columns["ＷＥＢ公開保留"].ColumnName = "WEB_Release_Pending";
                dt.Columns["公開予定日"].ColumnName = "Expected_Release_Date";
                dt.Columns["コピー元"].ColumnName = "Original";
                dt.Columns["画像コピー判定"].ColumnName = "Image_Copy_Determination";
                dt.Columns["商品グループコード"].ColumnName = "Product_Group_Code";
                dt.Columns["(商品グループ名)"].ColumnName = "Item_Name";
                dt.Columns["メーカー品番"].ColumnName = "Manufacturer_Part_Number";
                dt.Columns["販売数"].ColumnName = "Number_of_sales";
                dt.Columns["販売単位"].ColumnName = "Sales_unit";
                dt.Columns["内容量数1"].ColumnName = "Content_quantity_number_1";
                dt.Columns["内容量単位1"].ColumnName = "Contents_unit_1";
                dt.Columns["内容量数2"].ColumnName = "Content_quantity_number_2";
                dt.Columns["内容量単位2"].ColumnName = "Contents_unit_2"; 
                dt.Columns["JANコード"].ColumnName = "JAN_Code";
                dt.Columns["参考基準価格"].ColumnName = "Sale_Price";
                dt.Columns["オプションコード"].ColumnName = "Color_Name";
                dt.Columns["配送種別"].ColumnName = "Delivery_Type";
                dt.Columns["返品承認要否"].ColumnName = "Return_Necessary";
                dt.Columns["倉庫コード"].ColumnName = "Warehouse_Code";
                dt.Columns["(笠間納品可否)"].ColumnName = "Customer_Delivery_Type";
                dt.Columns["(笠間納品入荷日数)"].ColumnName = "Customer_Delivery_Day";
                dt.Columns["入荷日数"].ColumnName = "Delivery_Day";
                dt.Columns["出荷日数"].ColumnName = "Day_Ship";
                dt.Columns["お客様組立て"].ColumnName = "Customer_Assembly";
                dt.Columns["引渡方法"].ColumnName = "Delivery_Method";
                dt.Columns["代引可否"].ColumnName = "Cash_On_Delivery_Fee";
                dt.Columns["返品可否"].ColumnName = "Return_Type";
                dt.Columns["(全国)"].ColumnName = "Nation_Wide";
                dt.Columns["(北海道)"].ColumnName = "Hokkaido";
                dt.Columns["(沖縄)"].ColumnName = "Okinawa";
                dt.Columns["(離島)"].ColumnName = "Remote_Island";
                dt.Columns["公開種別"].ColumnName = "Public_Type";
                dt.Columns["(市場売価)"].ColumnName = "Market_Selling_Price";
                dt.Columns["(サプライヤ名)"].ColumnName = "Supplier_Name";
                dt.Columns["(メーカー名)"].ColumnName = "Maker_Name";
                dt.Columns["(ブランド名)"].ColumnName = "Brand_Name";
                dt.Columns["サプライヤ品番"].ColumnName = "Item_Code";
                dt.Columns["最低発注数量"].ColumnName = "Minimum_Order_Quantity";
                dt.Columns["最低発注単位"].ColumnName = "Minimum_Order_Unit";
                dt.Columns["仕入価格"].ColumnName = "Purchase_Price";
                dt.Columns["(直送時代引き)"].ColumnName = "Direct_Delivery_Area";
                dt.Columns["(直送時配送不可地域)"].ColumnName = "Undelivered_Area"; 
                dt.Columns["梱包質量(kg)"].ColumnName = "Packing_weight_kg";
                dt.Columns["梱包寸法(奥行D)(mm)"].ColumnName = "Packing_dimension_Dmm";
                dt.Columns["梱包寸法(幅W)(mm)"].ColumnName = "Packing_dimensions_Wmm";
                dt.Columns["梱包寸法(高さH)(mm)"].ColumnName = "Packing_dimensions_Hmm";
                dt.Columns["商品詳細登録コメント"].ColumnName = "Item_Details_Registration_Comment"; 
                dt.Columns["(特長)"].ColumnName = "Features";
                dt.Columns["(用途)"].ColumnName = "Application";
                dt.Columns["(注意)"].ColumnName = "Caution";
                dt.Columns["(画像置き場)"].ColumnName = "Image_Storage_Place";
                dt.Columns["(画像1)"].ColumnName = "Image1";
                dt.Columns["(画像1キャプション)"].ColumnName = "Image_Name1";
                dt.Columns["(指示1)"].ColumnName = "Instruction1";
                dt.Columns["(画像2)"].ColumnName = "Image2";
                dt.Columns["(画像2キャプション)"].ColumnName = "Image_Name2";
                dt.Columns["(指示2)"].ColumnName = "Instruction2";
                dt.Columns["(画像3)"].ColumnName = "Image3";
                dt.Columns["(画像3キャプション)"].ColumnName = "Image_Name3";
                dt.Columns["(指示3)"].ColumnName = "Instruction3";
                dt.Columns["(画像4)"].ColumnName = "Image4";
                dt.Columns["(画像4キャプション)"].ColumnName = "Image_Name4";
                dt.Columns["(指示4)"].ColumnName = "Instruction4";
                dt.Columns["該当法令"].ColumnName = "Applicable_Law"; 
                dt.Columns["販売許可・認可・届出"].ColumnName = "Sales_Permission"; 
                dt.Columns["賞味期限"].ColumnName = "Sell_By"; 
                dt.Columns["法令・規格"].ColumnName = "Laws_And_Regulation"; 
                dt.Columns["消防法上、届出を必要とする物質"].ColumnName = "Fire_Service_Law"; 
                dt.Columns["危険物の種別"].ColumnName = "Dangarous_Goods"; 
                dt.Columns["危険物の品名"].ColumnName = "Dangarous_Goods_Name"; 
                dt.Columns["危険等級"].ColumnName = "Risk_Rating"; 
                dt.Columns["危険物の性質"].ColumnName = "Dangerous_Goods_Nature"; 
                dt.Columns["危険物の含有量"].ColumnName = "Dangerous_Goods_Contents"; 
                dt.Columns["(属性名1)"].ColumnName = "Template1";            
                dt.Columns["(属性値1)"].ColumnName = "Template_Content1";
                dt.Columns["(属性名2)"].ColumnName = "Template2";
                dt.Columns["(属性値2)"].ColumnName = "Template_Content2";
                dt.Columns["(属性名3)"].ColumnName = "Template3";
                dt.Columns["(属性値3)"].ColumnName = "Template_Content3";
                dt.Columns["(属性名4)"].ColumnName = "Template4";
                dt.Columns["(属性値4)"].ColumnName = "Template_Content4";
                dt.Columns["(属性名5)"].ColumnName = "Template5";
                dt.Columns["(属性値5)"].ColumnName = "Template_Content5";
                dt.Columns["(属性名6)"].ColumnName = "Template6";
                dt.Columns["(属性値6)"].ColumnName = "Template_Content6";
                dt.Columns["(属性名7)"].ColumnName = "Template7";
                dt.Columns["(属性値7)"].ColumnName = "Template_Content7";
                dt.Columns["(属性名8)"].ColumnName = "Template8";
                dt.Columns["(属性値8)"].ColumnName = "Template_Content8";
                dt.Columns["(属性名9)"].ColumnName = "Template9";
                dt.Columns["(属性値9)"].ColumnName = "Template_Content9";
                dt.Columns["(属性名10)"].ColumnName = "Template10";
                dt.Columns["(属性値10)"].ColumnName = "Template_Content10";
                dt.Columns["(属性名11)"].ColumnName = "Template11";
                dt.Columns["(属性値11)"].ColumnName = "Template_Content11";
                dt.Columns["(属性名12)"].ColumnName = "Template12";
                dt.Columns["(属性値12)"].ColumnName = "Template_Content12";
                dt.Columns["(属性名13)"].ColumnName = "Template13";
                dt.Columns["(属性値13)"].ColumnName = "Template_Content13";
                dt.Columns["(属性名14)"].ColumnName = "Template14";
                dt.Columns["(属性値14)"].ColumnName = "Template_Content14";
                dt.Columns["(属性名15)"].ColumnName = "Template15";
                dt.Columns["(属性値15)"].ColumnName = "Template_Content15";
                dt.Columns["(属性名16)"].ColumnName = "Template16";
                dt.Columns["(属性値16)"].ColumnName = "Template_Content16";
                dt.Columns["(属性名17)"].ColumnName = "Template17";
                dt.Columns["(属性値17)"].ColumnName = "Template_Content17";
                dt.Columns["(属性名18)"].ColumnName = "Template18";
                dt.Columns["(属性値18)"].ColumnName = "Template_Content18";
                dt.Columns["(属性名19)"].ColumnName = "Template19";
                dt.Columns["(属性値19)"].ColumnName = "Template_Content19";
                dt.Columns["(属性名20)"].ColumnName = "Template20";
                dt.Columns["(属性値20)"].ColumnName = "Template_Content20";
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }

        protected DataTable ChangeMonotaroEngHeader(DataTable dt)
        {
            try
            {
                dt.Columns["Selling_Rank"].ColumnName = "(年間出荷数もしくは売れ筋A～Dランク)";
                dt.Columns["Category"].ColumnName = "(カテゴリ)";
                dt.Columns["Order_Code"].ColumnName = "(注文コード)";
                dt.Columns["Urgent_Judgment"].ColumnName = "緊急判定";
                dt.Columns["Product_Details_Registration"].ColumnName = "商品詳細登録要否";
                dt.Columns["Request_Comment"].ColumnName = "依頼コメント";
                dt.Columns["WEB_Release_Pending"].ColumnName = "ＷＥＢ公開保留";
                dt.Columns["Expected_Release_Date"].ColumnName = "公開予定日";
                dt.Columns["Original"].ColumnName = "コピー元";
                dt.Columns["Image_Copy_Determination"].ColumnName = "画像コピー判定";
                dt.Columns["Product_Group_Code"].ColumnName = "商品グループコード";
                dt.Columns["Item_Name"].ColumnName = "商品グループ名";
                dt.Columns["Manufacturer_Part_Number"].ColumnName = "メーカー品番";
                dt.Columns["Number_of_sales"].ColumnName = "販売数";
                dt.Columns["Sales_unit"].ColumnName = "販売単位";
                dt.Columns["Content_quantity_number_1"].ColumnName = "内容量数1";
                dt.Columns["Contents_unit_1"].ColumnName = "内容量単位1";
                dt.Columns["Content_quantity_number_2"].ColumnName = "内容量数2";
                dt.Columns["Contents_unit_2"].ColumnName = "内容量単位2";
                dt.Columns["JAN_Code"].ColumnName = "JANコード";
                dt.Columns["Sale_Price"].ColumnName = "参考基準価格";
                dt.Columns["Color_Name"].ColumnName = "オプションコード";
                dt.Columns["Delivery_Type"].ColumnName = "配送種別";
                dt.Columns["Return_Necessary"].ColumnName = "返品承認要否";
                dt.Columns["Warehouse_Code"].ColumnName = "倉庫コード";
                dt.Columns["Customer_Delivery_Type"].ColumnName = "(笠間納品可否)";
                dt.Columns["Customer_Delivery_Day"].ColumnName = "(笠間納品入荷日数)";
                dt.Columns["Delivery_Day"].ColumnName = "入荷日数";
                dt.Columns["Day_Ship"].ColumnName = "出荷日数";
                dt.Columns["Customer_Assembly"].ColumnName = "お客様組立て";
                dt.Columns["Delivery_Method"].ColumnName = "引渡方法";
                dt.Columns["Cash_On_Delivery_Fee"].ColumnName = "代引可否";
                dt.Columns["Return_Type"].ColumnName = "返品可否";
                dt.Columns["Nation_Wide"].ColumnName = "(全国)";
                dt.Columns["Hokkaido"].ColumnName = "(北海道)";
                dt.Columns["Okinawa"].ColumnName = "(沖縄)";
                dt.Columns["Remote_Island"].ColumnName = "(離島)";
                dt.Columns["Public_Type"].ColumnName = "公開種別";
                dt.Columns["Market_Selling_Price"].ColumnName = "(市場売価)";
                dt.Columns["Supplier_Name"].ColumnName = "(サプライヤ名)";
                dt.Columns["Maker_Name"].ColumnName = "(メーカー名)";
                dt.Columns["Brand_Name"].ColumnName = "(ブランド名)";
                dt.Columns["Item_Code"].ColumnName = "サプライヤ品番";
                dt.Columns["Minimum_Order_Quantity"].ColumnName = "最低発注数量";
                dt.Columns["Minimum_Order_Unit"].ColumnName = "最低発注単位";
                dt.Columns["Purchase_Price"].ColumnName = "仕入価格";
                dt.Columns["Direct_Delivery_Area"].ColumnName = "(直送時代引き)";
                dt.Columns["Undelivered_Area"].ColumnName = "(直送時配送不可地域)";
                dt.Columns["Packing_weight_kg"].ColumnName = "梱包質量(kg)";
                dt.Columns["Packing_dimension_Dmm"].ColumnName = "梱包寸法(奥行D)(mm)";
                dt.Columns["Packing_dimensions_Wmm"].ColumnName = "梱包寸法(幅W)(mm)";
                dt.Columns["Packing_dimensions_Hmm"].ColumnName = "梱包寸法(高さH)(mm)";
                dt.Columns["Item_Details_Registration_Comment"].ColumnName = "商品詳細登録コメント";
                dt.Columns["Features"].ColumnName = "(特長)";
                dt.Columns["Application"].ColumnName = "(用途)";
                dt.Columns["Caution"].ColumnName = "(注意)";
                dt.Columns["Image_Storage_Place"].ColumnName = "(画像置き場)";
                dt.Columns["Image1"].ColumnName = "(画像1)";
                dt.Columns["Image_Name1"].ColumnName = "(画像1キャプション)";
                dt.Columns["Instruction1"].ColumnName = "(指示1)";
                dt.Columns["Image2"].ColumnName = "(画像2)";
                dt.Columns["Image_Name2"].ColumnName = "(画像2キャプション)";
                dt.Columns["Instruction2"].ColumnName = "(指示2)";
                dt.Columns["Image3"].ColumnName = "(画像3)";
                dt.Columns["Image_Name3"].ColumnName = "(画像3キャプション)";
                dt.Columns["Instruction3"].ColumnName = "(指示3)";
                dt.Columns["Image4"].ColumnName = "(画像4)";
                dt.Columns["Image_Name4"].ColumnName = "(画像4キャプション)";
                dt.Columns["Instruction4"].ColumnName = "(指示4)";
                dt.Columns["Applicable_Law"].ColumnName = "該当法令";
                dt.Columns["Sales_Permission"].ColumnName = "販売許可・認可・届出";
                dt.Columns["Sell_By"].ColumnName = "賞味期限";
                dt.Columns["Laws_And_Regulation"].ColumnName = "法令・規格";
                dt.Columns["Fire_Service_Law"].ColumnName = "消防法上、届出を必要とする物質";
                dt.Columns["Dangarous_Goods"].ColumnName = "危険物の種別";
                dt.Columns["Dangarous_Goods_Name"].ColumnName = "危険物の品名";
                dt.Columns["Risk_Rating"].ColumnName = "危険等級";
                dt.Columns["Dangerous_Goods_Nature"].ColumnName = "危険物の性質";
                dt.Columns["Dangerous_Goods_Contents"].ColumnName = "危険物の含有量";
                dt.Columns["Template1"].ColumnName = "(属性名1)";
                dt.Columns["Template_Content1"].ColumnName = "(属性値1)";
                dt.Columns["Template2"].ColumnName = "(属性名2)";
                dt.Columns["Template_Content2"].ColumnName = "(属性値2)";
                dt.Columns["Template3"].ColumnName = "(属性名3)";
                dt.Columns["Template_Content3"].ColumnName = "(属性値3)";
                dt.Columns["Template4"].ColumnName = "(属性名4)";
                dt.Columns["Template_Content4"].ColumnName = "(属性値4)";
                dt.Columns["Template5"].ColumnName = "(属性名5)";
                dt.Columns["Template_Content5"].ColumnName = "(属性値5)";
                dt.Columns["Template6"].ColumnName = "(属性名6)";
                dt.Columns["Template_Content6"].ColumnName = "(属性値6)";
                dt.Columns["Template7"].ColumnName = "(属性名7)";
                dt.Columns["Template_Content7"].ColumnName = "(属性値7)";
                dt.Columns["Template8"].ColumnName = "(属性名8)";
                dt.Columns["Template_Content8"].ColumnName = "(属性値8)";
                dt.Columns["Template9"].ColumnName = "(属性名9)";
                dt.Columns["Template_Content9"].ColumnName = "(属性値9)";
                dt.Columns["Template10"].ColumnName = "(属性名10)";
                dt.Columns["Template_Content10"].ColumnName = "(属性値10)";
                dt.Columns["Template11"].ColumnName = "(属性名11)";
                dt.Columns["Template_Content11"].ColumnName = "(属性値11)";
                dt.Columns["Template12"].ColumnName = "(属性名12)";
                dt.Columns["Template_Content12"].ColumnName = "(属性値12)";
                dt.Columns["Template13"].ColumnName = "(属性名13)";
                dt.Columns["Template_Content13"].ColumnName = "(属性値13)";
                dt.Columns["Template14"].ColumnName = "(属性名14)";
                dt.Columns["Template_Content14"].ColumnName = "(属性値14)";
                dt.Columns["Template15"].ColumnName = "(属性名15)";
                dt.Columns["Template_Content15"].ColumnName = "(属性値15)";
                dt.Columns["Template16"].ColumnName = "(属性名16)";
                dt.Columns["Template_Content16"].ColumnName = "(属性値16)";
                dt.Columns["Template17"].ColumnName = "(属性名17)";
                dt.Columns["Template_Content17"].ColumnName = "(属性値17)";
                dt.Columns["Template18"].ColumnName = "(属性名18)";
                dt.Columns["Template_Content18"].ColumnName = "(属性値18)";
                dt.Columns["Template19"].ColumnName = "(属性名19)";
                dt.Columns["Template_Content19"].ColumnName = "(属性値19)";
                dt.Columns["Template20"].ColumnName = "(属性名20)";
                dt.Columns["Template_Content20"].ColumnName = "(属性値20)";
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }
        protected DataTable checkItemCode(DataTable dt, String[] col, int type)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < col.Length; j++)
                    {
                        string itemcode = dt.Rows[i]["Item_Code"].ToString();
                        if (itemcode.Contains(".") || itemcode.Contains(" ")||itemcode .Contains("+"))
                        {
                            dt.Rows[i]["チェック"] = "エラー";//error 
                            dt.Rows[i]["エラー内容"] = EngToJpHeader(col[j], type) + "のフォーマットが不正です。";//error detail
                            dt.Rows[i]["Type"] = 5;//error type
                        }
                    }
                }
                      
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }
        
        //check the value is integer
        protected DataTable checkIntType(DataTable dt, String[] col, int type)
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
                            dt.Rows[i]["エラー内容"] = EngToJpHeader(col[j], type) + "のフォーマットが不正です。";//error detail
                            dt.Rows[i]["Type"] = 5;//error type
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
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
                            dt.Rows[i]["エラー内容"] = EngToJpHeader(col[j], type) + "-Greater than " + length + " Bytes";//error detail
                            dt.Rows[i]["Type"] = 5;//type
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
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
                                    dt.Rows[i]["エラー内容"] = EngToJpHeader(col[j].ToString(), type) + "のフォーマットが不正です。";//error detail
                                    dt.Rows[i]["Type"] = 5;//type
                                }
                            }
                        }
                        catch (Exception)
                        {
                            dt.Rows[i]["チェック"] = "エラー";
                            dt.Rows[i]["エラー内容"] = EngToJpHeader(col[j].ToString(), type) + "のフォーマットが不正です。";
                            dt.Rows[i]["Type"] = 5;
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }

        //change japanese header to english for Item_Master
        protected DataTable ChangeItemMasterHeader(DataTable dt)
        {
            try
            {
                dt.Columns["販売管理番号"].ColumnName = "Item_AdminCode";
                dt.Columns["商品番号"].ColumnName = "Item_Code";
                dt.Columns["商品名"].ColumnName = "Item_Name";
                dt.Columns["定価"].ColumnName = "List_Price";
                dt.Columns["販売価格"].ColumnName = "Sale_Price";
                dt.Columns["楽天価格(税抜)"].ColumnName = "RakutenPrice";
                dt.Columns["Yahoo価格(税抜)"].ColumnName = "YahooPrice";
                dt.Columns["Wowma価格(税抜)"].ColumnName = "WowmaPrice";
                dt.Columns["自社価格(税抜)"].ColumnName = "JishaPrice";
                dt.Columns["ORS価格(税抜)"].ColumnName = "TennisPrice";
                dt.Columns["原価"].ColumnName = "Cost";
                dt.Columns["発売日"].ColumnName = "Release_Date";
                dt.Columns["掲載可能日"].ColumnName = "Post_Available_Date";
                dt.Columns["年度"].ColumnName = "Year";
                dt.Columns["シーズン"].ColumnName = "Season";
                dt.Columns["ブランドコード"].ColumnName = "Brand_Code";
                dt.Columns["ブランド名"].ColumnName = "Brand_Name";
                dt.Columns["ヤフーブランドコード"].ColumnName = "Brand_Code_Yahoo";
                dt.Columns["競技コード"].ColumnName = "Competition_Code";
                dt.Columns["競技名"].ColumnName = "Competition_Name";
                dt.Columns["分類コード"].ColumnName = "Classification_Code";
                dt.Columns["分類名"].ColumnName = "Class_Name";
                dt.Columns["仕入先名"].ColumnName = "Company_Name";
                dt.Columns["カタログ情報"].ColumnName = "Catalog_Information";
                dt.Columns["特記フラグ"].ColumnName = "Special_Flag";
                dt.Columns["予約フラグ"].ColumnName = "Reservation_Flag";
                dt.Columns["指示書番号"].ColumnName = "Instruction_No";
                dt.Columns["承認日"].ColumnName = "Approve_Date";
                dt.Columns["備考"].ColumnName = "Remarks";
                dt.Columns["メーカー商品コード"].ColumnName = "Product_Code";
                dt.Columns["販売単位"].ColumnName = "Sales_unit";
                dt.Columns["内容量数1"].ColumnName = "Content_quantity_number_1";
                dt.Columns["内容量単位1"].ColumnName = "Contents_unit_1";
                dt.Columns["内容量数2"].ColumnName = "Content_quantity_number_2";
                dt.Columns["内容量単位2"].ColumnName = "Contents_unit_2";
                dt.Columns["PC用キャッチコピー"].ColumnName = "CatchCopy";
                dt.Columns["モバイル用キャッチコピー"].ColumnName = "CatchCopyMobile";

                //dt.Columns["YahooエビデンスURL"].ColumnName = "Yahoo_URl";
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }

        //change english header to japanese
        protected String EngToJpHeader(String Eng, int type)
        {
            try
            {
                if (type == 0)//Item Master
                {
                    switch (Eng)
                    {
                        case "Item_AdminCode": return "販売管理番号";
                        case "Item_Code": return "商品番号";
                        case "Item_Name": return "商品名";
                        case "List_Price": return "定価";
                        case "Sale_Price": return "販売価格";
                        case "Cost": return "原価";
                        case "Release_Date": return "発売日";
                        case "Post_Available_Date": return "掲載可能日";
                        case "YEAR": return "年度";
                        case "Season": return "シーズン";
                        case "Brand_Code": return "ブランドコード";
                        case "Brand_Name": return "ブランド名";
                        case "Brand_Code_Yahoo": return "ヤフーブランドコード";
                        case "Competition_Code": return "競技コード";
                        case "Competition_Name": return "競技名";
                        case "Classification_Code": return "分類コード";
                        case "Class_Name": return "分類名";
                        case "Company_Name": return "仕入先名";
                        case "Catalog_Information": return "カタログ情報";
                        case "Special_Flag": return "特記フラグ";
                        case "Reservation_Flag": return "予約フラグ";
                        case "Instruction_No": return "指示書番号";
                        case "Approve_Date": return "承認日";
                        case "Remarks": return "備考";
                        default: return "";
                    }
                }
                else if (type == 1)//SKU
                {
                    switch (Eng)
                    {
                        case "Item_AdminCode": return "販売管理番号";
                        case "Item_Code": return "商品番号";
                        case "Color_Name": return "カラー名";
                        case "Size_Name": return "サイズ名";
                        case "Color_Code": return "カラーコード";
                        case "Size_Code": return "サイズコード";
                        case "Color_Name_Official": return "カラー正式名称";
                        case "Size_Name_Official": return "サイズ正式名称";
                        case "JAN_Code": return "JANコード";
                    }
                }
                else if (type == 2)//Inventory
                {
                    switch (Eng)
                    {
                        case "Item_Code": return "商品番号";
                        case "Item_AdminCode": return "販売管理番号";
                        case "Quantity": return "在庫数";
                    }
                }
                else if (type == 3)//Monotaro
                {
                    switch (Eng)
                    {
                        case "Item_Code": return "商品番号";
                        case "Item_Name": return "商品名";
                        case "JAN_Code": return "JANコード";
                        case "Brand_Name": return "メーカー名";
                        case "Selling_Rank": return "年間出荷数もしくは売れ筋A～Dランク";
                        case "Delivery_Type": return "配送種別";
                        case "Customer_Delivery_Type": return "笠間納品可否";
                        case "Customer_Delivery_Day": return "笠間納品入荷日数";
                        case "Delivery_Day": return "入荷日数";
                        case "Customer_Assembly": return "お客様組立て";
                        case "Delivery_Method": return "引渡方法";
                        case "Cash_On_Delivery_Fee": return "代引可否";
                        case "Return_Type": return "返品可否";
                        case "Nation_Wide": return "全国";
                        case "Hokkaido": return "北海道";
                        case "Okinawa": return "沖縄";
                        case "Remote_Island": return "離島";
                        case "Market_Selling_Price": return "市場売価";
                        case "Maker_Name": return "メーカー名";
                        case "Purchase_Price": return "仕入価格";
                        case "Undelivered_Area": return "直送時配送不可地域";
                        case "Item_Details_Registration_Comment": return "商品詳細登録コメント";
                        case "Applicable_Law": return "該当法令";
                        case "Sales_Permission": return "販売許可・認可・届出";
                        case "Sell_By": return "賞味期限";
                        case "Laws_And_Regulation": return "法令・規格";
                        case "Fire_Service_Law": return "消防法上、届出を必要とする物質";
                        case "Dangarous_Goods": return "危険物の種別";
                        case "Dangarous_Goods_Name": return "危険物の品名";
                        case "Risk_Rating": return "危険等級";
                        case "Dangerous_Goods_Nature": return "危険物の性質";
                        case "Dangerous_Goods_Contents": return "危険物の含有量";
                        default: return "";
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return "";
            }
        }

        //check column is contain or not in datatable
        protected Boolean CheckColumn(String[] colName, DataTable dt)
        {
            try
            {
                DataColumnCollection col = dt.Columns;
                for (int i = 0; i < colName.Length; i++)
                {
                    if (!col.Contains(colName[i]))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

        protected Boolean CheckTagIDColumn(string[] col, DataTable dt)
        {
            try
            {
                string[] columnNames = (from dc in dt.Columns.Cast<DataColumn>()
                                        select dc.ColumnName).ToArray();
                for (int i = 0; i < col.Length; i++)
                {
                    if (columnNames[i] != col[i])
                        return false;
                }
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnUpdate_Click(object sender,EventArgs e)
        {
            try
            {
                hfdmsg.Value = null;
                String master, sku, inventory, monotaro;
                master = sku = inventory =monotaro= String.Empty;
                if (Cache["dtmasterok"] != null)
                {
                    master = Itemmaster();
                    Cache.Remove("dtmasterok");                  
                }

                if (Cache["dtskuok"] != null)
                {
                    sku = SKU();
                    Cache.Remove("dtskuok");
                }

                if (Cache["dtinventoryok"] != null)
                {
                    inventory = Inventory();
                    Cache.Remove("dtinventoryok");
                }

                // added by ETZ for sks-390 TagID
                if (Cache["dtTagData"] != null)
                {
                    TagID_Insert();
                    Cache.Remove("dtTagData");
                }
                if (Cache["dtmonotaro"] != null)
                {
                    monotaro = Monotaro();
                    Cache.Remove("dtmonotaro");
                }
                if (!String.IsNullOrWhiteSpace(hfdmsg.Value.ToString()) )
                {
                    GlobalUI.MessageBox(hfdmsg.Value.ToString()); 
                    hfdmsg.Value = null;
                    Cache.Remove("dtskuok");
                    btnUpdate.Visible = false;
                }
                else
                {
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Redirect("../Import/Import_ItemMaster_Log.aspx?Log_IDs=" + master + "," + sku + "," + inventory + "," + monotaro, false);
                   // Response.Redirect("~/WebForms/Import/Import_ItemMaster_Log.aspx?Log_IDs=" + master + "," + sku + "," + inventory, false);
                }
            }
            catch (Exception ex)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected String Itemmaster()
        {
            try
            {
                itbl = new Import_Item_BL();
                Item_ImportLog_BL ItemImbl = new Item_ImportLog_BL();

                if (Cache["dtmasterok"] != null)
                {
                    DataTable dt = Cache["dtmasterok"] as DataTable;
                    DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));

                    foreach (DataRow rows in dt.Rows)
                    {
                        if (String.IsNullOrWhiteSpace(rows["Item_Code"].ToString()))
                        {
                            rows.Delete();
                        }
                    }
                    dt.AcceptChanges();
                    if (!dt.Columns.Contains("User_ID"))
                    {
                        newColumn.DefaultValue = Userid;
                        dt.Columns.Add(newColumn);//add imported user
                    }
                    
                    int result = itbl.Itemmaster(dt);//insert data
                    if (result > 0)
                    {
                        return result.ToString();
                    }
                    return String.Empty;
                }
                else
                {
                    GlobalUI.MessageBox("Incorrect   File Format!");
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
        }

        protected String SKU()
        {
            try
            {
                itbl = new Import_Item_BL();
                Item_ImportLog_BL ItemImbl = new Item_ImportLog_BL();

                if (Cache["dtskuok"] != null)
                {
                    DataTable dt = Cache["dtskuok"] as DataTable;
                    DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));
                    newColumn.DefaultValue = Userid;
                    dt.Columns.Add(newColumn);//add imported user
                   int result = itbl.SKU(dt);//insert data
                    if (result > 0)
                    {
                        return result.ToString();
                    }
                    return String.Empty;
                }
                else
                {
                    GlobalUI.MessageBox("Incorrect SKU File Format!");
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                string line = ex.Message.ToString();
                string replaceWith = "";
                line.Replace(System.Environment.NewLine, "replacement text");
                string line2 = Regex.Replace(line, @"\r\n?|\n", replaceWith);
                hfdmsg.Value = line2;
                return String.Empty;
            }
        }

        protected String Monotaro()
        {
            try
            {
                itbl = new Import_Item_BL();
                Item_ImportLog_BL ItemImbl = new Item_ImportLog_BL();

                if (Cache["dtmonotaro"] != null)
                {
                    DataTable dt = Cache["dtmonotaro"] as DataTable;
                    DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));
                    newColumn.DefaultValue = Userid;
                    dt.Columns.Add(newColumn);//add imported user
                    int result = itbl .Monotaro (dt);//insert data
                    if (result > 0)
                    {
                        return result.ToString();
                    }
                    return String.Empty;
                }
                else
                {
                    GlobalUI.MessageBox("Incorrect Monotarou File Format!");
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                string line = ex.Message.ToString();
                string replaceWith = "";
                line.Replace(System.Environment.NewLine, "replacement text");
                string line2 = Regex.Replace(line, @"\r\n?|\n", replaceWith);
                hfdmsg.Value = line2;
                return String.Empty;
            }
        }

        protected String Inventory()
        {
            try
            {
                itbl = new Import_Item_BL();

                if (Cache["dtinventoryok"] != null)
                {
                    DataTable dt = Cache["dtinventoryok"] as DataTable;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    { 
                        if(String.IsNullOrWhiteSpace(dt.Rows[i]["Quantity"].ToString()))
                        {
                            
                            dt.Rows[i]["Quantity"] = 999;
                        }
                    }
                    
                    DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));
                    newColumn.DefaultValue = Userid;
                    if (!dt.Columns.Contains("User_ID"))
                        dt.Columns.Add(newColumn);//add imported user
                    int result = itbl.Inventory(dt);//insert data
                    if (result > 0)
                    {
                        return result.ToString();
                    }
                    return String.Empty;
                }
                else
                {
                    GlobalUI.MessageBox("Incorrect Inventory  File Format!");
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
        }

        protected void TagID_Insert()
        {
            try
            {
                if (Cache["dtTagData"] != null)
                {
                    DataTable dtTagvalue = Cache["dtTagData"] as DataTable;
                    DataColumn newcols = new DataColumn("Error_Message", typeof(String));
                    newcols.DefaultValue = "";
                    dtTagvalue.Columns.Add(newcols);
                    foreach (DataRow row in dtTagvalue.Rows)
                    {
                        DataTable dt = itbl.ItemCode_Check(row["Item_Code"].ToString());  //check ItemCode
                        if (dt.Rows.Count <= 0)
                        {
                            row["Error_Message"] = "商品番号が存在しない場合,";
                        }
                        else
                        {
                            row["Error_Message"] = "";
                        }

                        string tagNameError = ErrorCheck_TagName(dtTagvalue, row);  // check TagName duplicate
                        row["Error_Message"] = row["Error_Message"].ToString() + tagNameError;

                        itbl = new Import_Item_BL();
                        DataTable dtErrorCount = itbl.ItemInsertTagID(row["Item_Code"].ToString(), row["Size_Name"].ToString(), row["Color_Name"].ToString(), row["Rakuten_CategoryID"].ToString(), row["Tag_Name1"].ToString(), row["Tag_Name2"].ToString(), row["Tag_Name3"].ToString(), row["Tag_Name4"].ToString(), row["Tag_Name5"].ToString(), row["Tag_Name6"].ToString(), row["Tag_Name7"].ToString(), row["Tag_Name8"].ToString());
                        if (Convert.ToInt32(dtErrorCount.Rows[0]["ErrorMsg_Count"].ToString()) > 0) // check TagID is null
                        {
                            row["Error_Message"] = row["Error_Message"] + dtErrorCount.Rows[0]["ErrorMsg_Count"].ToString() + "指定したタグ名の値が間違っている場合";
                        }
                       
                    }
                    Session["TagValue"] = dtTagvalue;


                    #region tagnameInsert
                    //for (int i = 0; i < dtTagvalue.Rows.Count; i++)
                    //{
                    //    itbl = new Import_Item_BL();
                    //    //Item_ImportLog_BL ItemImbl = new Item_ImportLog_BL();
                    //    String Item_Code = dtTagvalue.Rows[i]["Item_Code"].ToString();
                    //    String Item_Name = dtTagvalue.Rows[i]["Item_Name"].ToString();
                    //    String Color_Name = dtTagvalue.Rows[i]["Color_Name"].ToString();
                    //    String Size_Name = dtTagvalue.Rows[i]["Size_Name"].ToString();
                    //    String Rakuten_CategoryID = dtTagvalue.Rows[i]["Rakuten_CategoryID"].ToString();
                    //    //String Path = dtTagvalue.Rows[i]["Path"].ToString();
                    //    String Tag_Name1 = dtTagvalue.Rows[i]["Tag_Name1"].ToString();
                    //    String Tag_Name2 = dtTagvalue.Rows[i]["Tag_Name2"].ToString();
                    //    String Tag_Name3 = dtTagvalue.Rows[i]["Tag_Name3"].ToString();
                    //    String Tag_Name4 = dtTagvalue.Rows[i]["Tag_Name4"].ToString();
                    //    String Tag_Name5 = dtTagvalue.Rows[i]["Tag_Name5"].ToString();
                    //    String Tag_Name6 = dtTagvalue.Rows[i]["Tag_Name6"].ToString();
                    //    String Tag_Name7 = dtTagvalue.Rows[i]["Tag_Name7"].ToString();
                    //    String Tag_Name8 = dtTagvalue.Rows[i]["Tag_Name8"].ToString();
                    

                    //    //itbl.ItemInsertTagID(Item_Code, Size_Name, Color_Name, Rakuten_CategoryID, Tag_Name1, Tag_Name2, Tag_Name3, Tag_Name4, Tag_Name5, Tag_Name6,Tag_Name7,Tag_Name8,errorMsg);
                    //    DataTable dtErrorCount=itbl.ItemInsertTagID(Item_Code, Size_Name, Color_Name, Rakuten_CategoryID, Tag_Name1, Tag_Name2, Tag_Name3, Tag_Name4, Tag_Name5, Tag_Name6, Tag_Name7, Tag_Name8);
                    //    if (Convert.ToInt32(dtErrorCount.Rows[0]["ErrorMsg_Count"].ToString()) > 0) // check TagID is null
                    //    {
                    //        dtTagvalue.Rows[i]["Error_Message"] = dtTagvalue.Rows[i]["Error_Message"] + dtErrorCount.Rows[0]["ErrorMsg_Count"].ToString() + "指定したタグ名の値が間違っている場合";
                    //    }
                    //    Session["TagValue"] = dtTagvalue;
                    //
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected String ErrorCheck_TagName(DataTable dtTagvalue, DataRow row)
        {

            DataSet dsErrorCount = itbl.ErrorCountTagID(row["Rakuten_CategoryID"].ToString(), row["Tag_Name1"].ToString(), row["Tag_Name2"].ToString(), row["Tag_Name3"].ToString(), row["Tag_Name4"].ToString(), row["Tag_Name5"].ToString(), row["Tag_Name6"].ToString(), row["Tag_Name7"].ToString(), row["Tag_Name8"].ToString());
            DataTable dtErrorCount1 = dsErrorCount.Tables[0];
            DataTable dtErrorCount2 = dsErrorCount.Tables[1];
            DataTable dtErrorCount3 = dsErrorCount.Tables[2];
            DataTable dtErrorCount4 = dsErrorCount.Tables[3];
            DataTable dtErrorCount5 = dsErrorCount.Tables[4];
            DataTable dtErrorCount6 = dsErrorCount.Tables[5];
            DataTable dtErrorCount7 = dsErrorCount.Tables[6];
            DataTable dtErrorCount8 = dsErrorCount.Tables[7];
            string tagNameError = string.Empty;
            if (Convert.ToInt32(dtErrorCount1.Rows[0]["ErrorCount"].ToString()) > 1)
            {
                tagNameError = tagNameError + row["Tag_Name1"].ToString() + "重複,";
                row["Tag_Name1"] = null;
            }

            if (Convert.ToInt32(dtErrorCount2.Rows[0]["ErrorCount"].ToString()) > 1)
            {
                tagNameError = tagNameError + row["Tag_Name2"].ToString() + "重複,";
                row["Tag_Name2"] = null;
            }

            if (Convert.ToInt32(dtErrorCount3.Rows[0]["ErrorCount"].ToString()) > 1)
            {
                tagNameError = tagNameError + row["Tag_Name3"].ToString() + "重複,";
                row["Tag_Name3"] = null;
            }

            if (Convert.ToInt32(dtErrorCount4.Rows[0]["ErrorCount"].ToString()) > 1)
            {
                tagNameError = tagNameError + row["Tag_Name4"].ToString() + "重複,";
                row["Tag_Name4"] = null;
            }

            if (Convert.ToInt32(dtErrorCount5.Rows[0]["ErrorCount"].ToString()) > 1)
            {
                tagNameError = tagNameError + row["Tag_Name5"].ToString() + "重複,";
                row["Tag_Name5"] = null;
            }

            if (Convert.ToInt32(dtErrorCount6.Rows[0]["ErrorCount"].ToString()) > 1)
            {
                tagNameError = tagNameError + row["Tag_Name6"].ToString() + "重複,";
                row["Tag_Name6"] = null;
            }

            if (Convert.ToInt32(dtErrorCount7.Rows[0]["ErrorCount"].ToString()) > 1)
            {
                tagNameError = tagNameError + row["Tag_Name7"].ToString() + "重複,";
                row["Tag_Name7"] = null;
            }

            if (Convert.ToInt32(dtErrorCount8.Rows[0]["ErrorCount"].ToString()) > 1)
            {
                tagNameError = tagNameError + row["Tag_Name8"].ToString() + "重複,";
                row["Tag_Name8"] = null;
            }
           
            
            return tagNameError;
        }

        protected void gvitemmaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
             if (Cache["dtmasterok"] != null)
             {
                 DataTable dtm =Cache["dtmasterok"] as DataTable;
             gvitemmaster.PageIndex = e.NewPageIndex;
             gvitemmaster.DataSource = dtm;
             gvitemmaster.DataBind();
            }
        }

        protected void gvsku_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            if (Cache["dtskuok"] != null)
            {
                DataTable dt = Cache["dtskuok"] as DataTable;
                gvsku.PageIndex = e.NewPageIndex;
                gvsku.DataSource = dt;
                gvsku.DataBind();
            }
        }

        protected void gvInventory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (Cache["dtinventoryok"] != null)
            {
                DataTable dt = Cache["dtinventoryok"] as DataTable;
                gvInventory.PageIndex = e.NewPageIndex;
                gvInventory.DataSource = dt;
                gvInventory.DataBind();
            }
        }

        protected void gdvTagID_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
                           
            if (Cache["dtTagData"] != null)
            {
                DataTable dt = Cache["dtTagData"] as DataTable;
                gdvTagID.DataSource = dt;
                gdvTagID.PageIndex = e.NewPageIndex;
                gdvTagID.DataBind();

                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "jscript", "ScrollToTop();", true);
               
            }
        }
        protected void gvmonotaro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            if (Cache["dtmonotaro"] != null)
            {
                DataTable dt = Cache["dtmonotaro"] as DataTable;
                gvmonotaro.DataSource = dt;
                gvmonotaro.PageIndex = e.NewPageIndex;
                gvmonotaro.DataBind();

                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "jscript", "ScrollToTop();", true);

            }
        }

       
    }
}