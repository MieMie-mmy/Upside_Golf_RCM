
/* 
    Created By            :Kyaw Thet Paing
    Created Date          :01-06-2017
    Updated By            :Ei Thinzar Zaw
    Updated Date          :12-06-2017
    Tables using          : Shop
 *                          Log_Exhibition_Item_Rakuten
 *                          Item_Export_ErrorCheck
 *                          Exhibition_Item_Shop
 *                          Exhibition_Item_Master
 *                          Item_Shop
 *                          Item_Master
 *                          
    Storedprocedure using :  

 */

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
using Ionic.Zip;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;

namespace API_R_Painttool
{
    public class Program
    {
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static String ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        static String Bak_ExportCSVPath = ConfigurationManager.AppSettings["BakExportCSVPath"].ToString();
        static String ItemImage = ConfigurationManager.AppSettings["ItemImage"].ToString();
        static string CtrlID = "";
        static DataTable dtOption;
        static DataTable dtCategory;
        static DataTable dtAPIKey;
        static int count = 0;
        static string errCode = string.Empty;
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "API Rakuten Racket";
                ConsoleWriteLine_Tofile("API Rakuten Racket : " + DateTime.Now);
                dtAPIKey = GetAPIKey(1);
                Delete();
                string list = SelectExhibitionItemID();
                ConsoleWriteLine_Tofile("1.SelectExhibitionItemID : " + list);
                if (!string.IsNullOrWhiteSpace(list))
                {
                    string[] code = list.Split(',');
                    foreach (string itemcode in code)
                    {
                        CheckItemCodeForNewUpdate(itemcode);
                        ConsoleWriteLine_Tofile("2.CheckItemCode for " + itemcode + " : " + DateTime.Now);

                        DataTable dtItemMaster = GetItemData(itemcode, 1);
                        ConsoleWriteLine_Tofile("3.GetItemData : " + DateTime.Now);

                        Export_CSV3 export = new Export_CSV3();
                        DataTable dtItem = export.ModifyTable(dtItemMaster, 1);
                        ConsoleWriteLine_Tofile("4.ChangeTemplate : " + DateTime.Now);

                        SaveLogExhibition(dtItem, itemcode, 1);
                        ConsoleWriteLine_Tofile("5.SaveLogExhibition : " + DateTime.Now);

                        DataTable dtImage = SelectLogExhibitionImage(1, itemcode);
                        ConsoleWriteLine_Tofile("6.GetImageList : " + DateTime.Now);

                        DataTable dtSelect = SelectLogExhibitionSelect(1, 1, itemcode);

                        DataTable dtInventory = new DataTable();
                        DataRow[] dr = dtSelect.Select("選択肢タイプ = 'i'");
                        if (dr.Count() > 0)
                            dtInventory = dtSelect.Select("選択肢タイプ = 'i'").CopyToDataTable();
                        dtOption = new DataTable();
                        dr = dtSelect.Select("選択肢タイプ = 's'");

                        if (dr.Count() > 0)
                            dtOption = dtSelect.Select("選択肢タイプ = 's'").CopyToDataTable();
                        dtCategory = SelectLogExhibitionCategory(1, 1, itemcode);
                        CreateCategoryToMall(dtCategory);   //create category in mall
                        CheckDailyDelivery(itemcode, dtItem, dtOption, dtCategory);// DailyDelivery check
                        if (errCode != "C220")
                        {
                            if (CtrlID.Equals("n"))
                            {
                                UploadItem(dtItem, dtInventory, dtCategory, dtOption, dtImage, itemcode);
                                SaveNewItemHistory(dtItem.Rows[0]["Exhibit_ID"].ToString(), 1);
                            }
                            else
                            {
                                UpdateItem(dtItem, dtInventory, dtCategory, dtOption, dtImage, itemcode);
                            }
                            SaveItemShopAPIInfo(Convert.ToInt32(dtItem.Rows[0]["Exhibit_ID"]), 1, "APIitem$" + DateTime.Now.ToString("ddMMyyyy_HHmmss"), CtrlID);
                            ChangeIsGeneratedCSVFlag(Convert.ToInt32(itemcode), Convert.ToInt32(dtItem.Rows[0]["Exhibit_ID"]), 1);
                            ChangeCtrl_ID(Convert.ToInt32(dtItem.Rows[0]["Item_ID"]), 1, CtrlID);
                            AddExportedDate(1, Convert.ToInt32(dtItem.Rows[0]["Exhibit_ID"]));
                        }
                    }
                    ChangeFlag(list, 1);
                    ConsoleWriteLine_Tofile("7.ChangeFlag : " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                //insert to system error log
                Save_SYS_Errorlog(ex.ToString());
            }
        }

        #region Daily Delivery
        public static void CheckDailyDelivery(string itemID, DataTable dtItem, DataTable dtOption, DataTable dtCategory)
        {
            DataTable dtItemCode = CollectItem(itemID);
            if (dtItemCode.Rows.Count > 0)
            {
                if (dtItem.Rows.Count > 0)
                {
                    dtItem.Rows[0]["Campaign_TypeID"] = "1";
                    dtItem.AcceptChanges();
                }
                if (dtOption.Rows.Count > 0)
                {
                    DataRow row = dtOption.NewRow();
                    row["コントロールカラム"] = "n";
                    row["項目選択肢用コントロールカラム"] = "n";
                    row["商品管理番号（商品URL）"] = dtItemCode.Rows[0]["Item_Code"].ToString();
                    row["選択肢タイプ"] = "s";
                    row["Select/Checkbox用項目名"] = "即日出荷";
                    row["Select/Checkbox用選択肢"] = "対象商品";
                    dtOption.Rows.Add(row);
                    dtOption.AcceptChanges();
                }
                if (dtCategory.Rows.Count > 0)
                {
                    DataRow row = dtCategory.NewRow();
                    row["コントロールカラム"] = "n";
                    row["商品管理番号（商品URL）"] = dtItemCode.Rows[0]["Item_Code"].ToString();
                    row["表示先カテゴリ"] = "PICK UP！" + "\\" + "即日出荷対象商品";
                    row["優先度"] = "1";
                    row["CategoryID"] = "3263";
                    row["1ページ複数形式"] = "";
                    row["カテゴリセット管理番号"] = "26";
                    row["カテゴリセット名"] = "";

                    dtCategory.Rows.Add(row);
                    dtCategory.AcceptChanges();
                }
            }
        }

        public static DataTable CollectItem(string itemID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_DailyDeliverySetting", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@itemID", itemID);
                cmd.CommandTimeout = 0;
                DataTable dt = new DataTable();
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                sqlData.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete
        static void Delete()
        {
            DataTable dtDelList = GetDelList();
            if (dtDelList.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDelList.Rows)
                {
                    try
                    {
                    l1:
                        string xml = GetXmlForDelete(dr["Item_Code"].ToString());
                        string responseStr = Delete_API(xml);
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.LoadXml(responseStr);
                        XmlNodeList list = xDoc.GetElementsByTagName("code");
                        if (list[0].InnerText.Equals("C114"))//still updating data in mall
                        {
                            goto l1;
                        }
                        if (list[0].InnerText.Equals("C001"))
                        {
                            InsertErrorMessage(dr["Item_ID"].ToString(), "指定された商品コードは存在しません。");
                        }
                        if (!list[0].InnerText.Equals("N000"))
                        {
                            XmlNodeList list1 = xDoc.GetElementsByTagName("msg");
                            if (list1.Count > 0)
                                InsertErrorMessage(dr["Item_ID"].ToString(), list1[0].InnerText);
                            else
                                InsertErrorMessage(dr["Item_ID"].ToString(), list[0].InnerText);
                        }
                        else
                        {
                            InsertErrorMessage(dr["Item_ID"].ToString(), "N000");
                        }
                        SaveItemShopAPIInfo(Convert.ToInt32(dr["Exhibit_ID"]), 1, "APIitem$" + DateTime.Now.ToString("ddMMyyyy_HHmmss"), "d");
                        ChangeFlagDeleteItem(Convert.ToInt32(dr["Item_ID"]));
                        AddExportedDate(1, Convert.ToInt32(dr["Exhibit_ID"]));
                        ChangeCtrl_ID(Convert.ToInt32(dr["Item_ID"]), 1, "d");
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
                                    if (error.Contains("message"))
                                    {
                                        XmlDocument xd = new XmlDocument();
                                        xd.LoadXml(error);
                                        error = xd.InnerText;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static string Delete_API(string xml)
        {
            string apiSecret = dtAPIKey.Rows[0]["APISecret"].ToString();
            string apiKey = dtAPIKey.Rows[0]["APIKey"].ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.rms.rakuten.co.jp/es/1.0/item/delete");
            byte[] bytes;
            bytes = System.Text.Encoding.UTF8.GetBytes(xml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            String encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiSecret + ":" + apiKey));
            request.Headers.Add("Authorization", "ESA " + encoded);
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            Thread.Sleep(3000);
            response = (HttpWebResponse)request.GetResponse();
            string responseStr = string.Empty;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                responseStr = new StreamReader(responseStream).ReadToEnd();
            }
            return responseStr;
        }

        static string GetXmlForDelete(string itemCode)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = Encoding.UTF8;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;
            XmlWriter writer = XmlWriter.Create(memoryStream, xmlWriterSettings);
            writer.WriteStartElement("request");
            writer.WriteStartElement("itemDeleteRequest");
            writer.WriteStartElement("item");
            writer.WriteElementString("itemUrl", itemCode);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            string xmlString = Encoding.UTF8.GetString(memoryStream.ToArray());
            return xmlString;
        }

        static DataTable GetDelList()
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand("Log_Exhibition_Item_Rakuten_SelectDelList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShopID", "1");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }
        #endregion

        #region FlagChange

        static void ChangeIsGeneratedCSVFlag(int Item_ID, int Exhibit_ID, int Shop_ID)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_ChangeFlag_ByMall_API", connectionstring);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Parameters.AddWithValue("@Item_ID", Item_ID);
                cmd.Parameters.AddWithValue("@Exhibit_ID", Exhibit_ID);
                cmd.Parameters.AddWithValue("@Shop_ID", Shop_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ChangeFlag(string list, int shop_ID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_ChangeFlagByShop_API", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@List", list);
                cmd.Parameters.AddWithValue("@Shop_ID", shop_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ChangeFlagDeleteItem(int itemid)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_DeleteItem_ChangeFlag", connectionstring);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Parameters.AddWithValue("@Item_ID", itemid);
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

        static void ChangeCtrl_ID(int Item_ID, int Shop_ID, string ctrlid)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Shop_ChangeCtrl_ID_API", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", Item_ID);
                cmd.Parameters.AddWithValue("@Shop_ID", Shop_ID);
                cmd.Parameters.AddWithValue("@Ctrl_ID", ctrlid);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
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

        #endregion

        #region UploadAPI
        static string GetXMl_NewUpdate(DataTable dtItem, DataTable dtInventory, DataTable dtCategory, DataTable dtOption, string itemCode, int mode)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = Encoding.UTF8;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;
            XmlWriter writer = XmlWriter.Create(memoryStream, xmlWriterSettings);

            #region Item
            writer.WriteStartElement("request");
            if (mode == 0)
                writer.WriteStartElement("itemInsertRequest");
            else
                writer.WriteStartElement("itemUpdateRequest");
            writer.WriteStartElement("item");
            writer.WriteElementString("itemUrl", dtItem.Rows[0]["Item_Code"].ToString());//商品管理番号（商品URL）
            writer.WriteElementString("itemNumber", dtItem.Rows[0]["Item_Code"].ToString());//商品番号
            writer.WriteElementString("itemName", dtItem.Rows[0]["Item_Name"].ToString());//商品名
            writer.WriteElementString("itemPrice", dtItem.Rows[0]["Sale_Price"].ToString());//販売価格
            writer.WriteElementString("genreId", dtItem.Rows[0]["Rakuten_CategoryID"].ToString());//dtItem.Rows[0]["Rakuten_CategoryID"].ToString());//全商品ディレクトリID
            writer.WriteElementString("catalogId", "");//dtItem.Rows[0]["RF_Catalog_ID"].ToString());//カタログID
            writer.WriteElementString("catalogIdExemptionReason", "4");
            string str = dtItem.Rows[0]["R_Image_URL"].ToString();
            string[] arr = str.Split(' ');
            if (arr.Length > 0)
            {
                writer.WriteStartElement("images");
                foreach (string img in arr)
                {
                    if (!img.Equals("0"))
                    {
                        writer.WriteStartElement("image");
                        writer.WriteElementString("imageUrl", img);//商品画像URL
                        //writer.WriteElementString("imageAlt", "");//
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
            }
            writer.WriteElementString("descriptionForPC", dtItem.Rows[0]["Item_Description_PC"].ToString());//PC用商品説明文
            writer.WriteElementString("descriptionForMobile", dtItem.Rows[0]["Item_Description_Mobile"].ToString());//モバイル用商品説明文
            writer.WriteElementString("descriptionForSmartPhone", dtItem.Rows[0]["Smart_Template"].ToString());//スマートフォン用商品説明文
            writer.WriteElementString("movieUrl", dtItem.Rows[0]["RF_Animation"].ToString());//動画
            //writer.WriteElementString("tagID",dtItem.Rows[0]["R_Tag_ID"].ToString());
            #endregion

            #region Option
            if (dtOption.Rows.Count > 0)
            {
                dtOption.Columns["Select/Checkbox用項目名"].ColumnName = "OptionName";
                dtOption.Columns["Select/Checkbox用選択肢"].ColumnName = "OptionValue";
                writer.WriteStartElement("options");
                while (dtOption.Rows.Count > 0)
                {
                    DataTable dtTemp = dtOption.Select("OptionName = '" + dtOption.Rows[0]["OptionName"].ToString() + "'").CopyToDataTable();
                    string optionName = dtOption.Rows[0]["OptionName"].ToString();
                    writer.WriteStartElement("option");
                    writer.WriteElementString("optionName", dtOption.Rows[0]["OptionName"].ToString());//Select/Checkbox用項目名
                    //writer.WriteElementString("optionName"," ");
                    writer.WriteElementString("optionStyle", "1");//選択肢スタイル
                    writer.WriteStartElement("optionValues");
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        writer.WriteStartElement("optionValue");
                        writer.WriteElementString("value", dtTemp.Rows[i]["OptionValue"].ToString());//Select/Checkbox用選択肢
                        //writer.WriteElementString("value"," ");
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    DataRow[] rows = dtOption.Select("OptionName = '" + dtOption.Rows[0]["OptionName"].ToString() + "'");
                    foreach (var row in rows)
                        row.Delete();
                    dtOption.AcceptChanges();
                }
                writer.WriteEndElement();
            }
            else
            {
                writer.WriteStartElement("options");
                writer.WriteEndElement();
            }

            writer.WriteStartElement("tagIds");
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["R_Tag_ID"].ToString()))
            {
                string tag = dtItem.Rows[0]["R_Tag_ID"].ToString();
                string[] tagid = tag.Split('/');
                for (int j = 0; j < tagid.Length; j++)
                {
                    writer.WriteElementString("tagId", tagid[j].ToString());
                }
            }
            writer.WriteEndElement();//tagIds

            writer.WriteElementString("catchCopyForPC", dtItem.Rows[0]["R_CatchCopy"].ToString());//PC用キャッチコピー
            ////writer.WriteElementString("catchCopyForMobile", "");//モバイル用キャッチコピー
            writer.WriteElementString("descriptionBySalesMethod", dtItem.Rows[0]["Sale_Description_PC"].ToString());//PC用販売説明文
            writer.WriteElementString("isSaleButton", dtItem.Rows[0]["RF_Order_Button"].ToString());//注文ボタン
            writer.WriteElementString("isDocumentButton", dtItem.Rows[0]["RF_Request_Button"].ToString());//資料請求ボタン
            writer.WriteElementString("isInquiryButton", dtItem.Rows[0]["RF_Product_Inquiry_Button"].ToString());//商品問い合わせボタン
            writer.WriteElementString("isStockNoticeButton", dtItem.Rows[0]["RF_Comingsoon_Button"].ToString());//再入荷のお知らせボタン
            writer.WriteElementString("itemLayout", dtItem.Rows[0]["RF_Product_Information"].ToString());//商品情報レイアウト
            writer.WriteElementString("isIncludedTax", dtItem.Rows[0]["RF_Consumption_Tax"].ToString());//消費税
            writer.WriteElementString("isIncludedPostage", dtItem.Rows[0]["R_Special_Flag"].ToString());//送料
            writer.WriteElementString("isIncludedCashOnDeliveryPostage", dtItem.Rows[0]["Delivery_Charges"].ToString());//代引き手数料
            writer.WriteElementString("displayPrice", dtItem.Rows[0]["R_List_Price"].ToString());//表示価格
            writer.WriteElementString("orderLimit", "-1");//dtItem.Rows[0]["RF_Acceptances_No"].ToString());//注文受付数
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["Extra_Shipping"].ToString()))
                writer.WriteElementString("postage", dtItem.Rows[0]["Extra_Shipping"].ToString());//個別送料
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["RF_Shipping_Category1"].ToString()))
                writer.WriteElementString("postageSegment1", dtItem.Rows[0]["RF_Shipping_Category1"].ToString());//送料区分1
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["RF_Shipping_Category2"].ToString()))
                writer.WriteElementString("postageSegment2", dtItem.Rows[0]["RF_Shipping_Category2"].ToString());//送料区分2
            writer.WriteElementString("isNoshiEnable", dtItem.Rows[0]["RF_Corresponding_Work"].ToString());//のし対応
            writer.WriteElementString("isUnavailableForSearch", dtItem.Rows[0]["RD_Search_Hide"].ToString());//サーチ非表示
            writer.WriteElementString("limitedPasswd", dtItem.Rows[0]["BlackMarket_Password"].ToString());//闇市パスワード
            writer.WriteElementString("isAvailableForMobile", dtItem.Rows[0]["RF_Mobile_Display"].ToString());//モバイル表示
            writer.WriteElementString("isDepot", dtItem.Rows[0]["Warehouse_Specified"].ToString());//倉庫指定
            //writer.WriteElementString("releaseDate", "");//予約商品発売日
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["Rakuten_MagnificationID"].ToString()))
            {
                writer.WriteStartElement("point");
                writer.WriteElementString("pointRate", dtItem.Rows[0]["Rakuten_MagnificationID"].ToString());//ポイント変倍率
                writer.WriteEndElement();//point
            }
            writer.WriteStartElement("itemInventory");
            writer.WriteElementString("inventoryType", "2");//在庫タイプdtItem.Rows[0]["RF_Stock_Type"].ToString()
            //writer.WriteEndElement();
            #endregion

            #region Inventory
            if (dtInventory.Rows.Count > 0)
            {
                writer.WriteStartElement("inventories");
                for (int i = 0; i < dtInventory.Rows.Count; i++)
                {
                    writer.WriteStartElement("inventory");
                    writer.WriteElementString("inventoryCount", dtInventory.Rows[i]["項目選択肢別在庫用在庫数"].ToString());//在庫数
                    writer.WriteElementString("childNoVertical", dtInventory.Rows[i]["項目選択肢別在庫用縦軸選択肢子番号"].ToString());//項目選択肢別在庫用縦軸選択肢子番号
                    writer.WriteElementString("childNoHorizontal", dtInventory.Rows[i]["項目選択肢別在庫用横軸選択肢子番号"].ToString());//項目選択肢別在庫用横軸選択肢子番号
                    writer.WriteElementString("optionNameVertical", dtInventory.Rows[i]["項目選択肢別在庫用縦軸選択肢"].ToString());//項目選択肢別在庫用縦軸選択肢
                    writer.WriteElementString("optionNameHorizontal", dtInventory.Rows[i]["項目選択肢別在庫用横軸選択肢"].ToString());//項目選択肢別在庫用横軸選択肢
                    writer.WriteElementString("isBackorderAvailable", dtInventory.Rows[i]["項目選択肢別在庫用取り寄せ可能表示"].ToString());//項目選択肢別在庫用取り寄せ可能表示
                    writer.WriteElementString("normalDeliveryDateId", dtInventory.Rows[i]["在庫あり時納期管理番号"].ToString());//在庫あり時納期管理番号
                    writer.WriteElementString("backorderDeliveryDateId", dtInventory.Rows[i]["在庫切れ時納期管理番号"].ToString());//在庫切れ時納期管理番号
                    writer.WriteElementString("isBackorder", dtInventory.Rows[i]["在庫切れ時の注文受付"].ToString());//在庫切れ時の注文受付
                    writer.WriteElementString("isRestoreInventoryFlag", dtInventory.Rows[i]["在庫戻しフラグ"].ToString());//在庫戻し設定
                    //writer.WriteEndElement();

                    //writer.WriteStartElement("images");
                    //writer.WriteElementString("image", "");
                    //writer.WriteEndElement();

                    writer.WriteStartElement("tagIds");
                    string tag = dtInventory.Rows[i]["タグID"].ToString();
                    if (!string.IsNullOrWhiteSpace(tag.ToString()) & tag != "0")
                    {
                        string[] tagid = tag.Split('/');
                        for (int j = 0; j < tagid.Length; j++)
                        {
                            writer.WriteElementString("tagId", tagid[j].ToString());
                        }
                    }
                    writer.WriteEndElement();//tagIds

                    writer.WriteEndElement();//inventory
                }
                writer.WriteEndElement();
            }
            writer.WriteElementString("verticalName", dtItem.Rows[0]["RF_Vertical_ItemName"].ToString());//項目選択肢別在庫用縦軸項目名
            writer.WriteElementString("horizontalName", dtItem.Rows[0]["RF_Horizontal_ItemName"].ToString());//項目選択肢別在庫用横軸項目名
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["RF_Stock_No_Display"].ToString()))
                writer.WriteElementString("inventoryQuantityFlag", dtItem.Rows[0]["RF_Stock_No_Display"].ToString());//在庫数表示
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["RF_Remaining_Stock"].ToString()))
                writer.WriteElementString("inventoryDisplayFlag", dtItem.Rows[0]["RF_Remaining_Stock"].ToString());//項目選択肢別在庫用残り表示閾値
            writer.WriteEndElement();//itemInventory
            writer.WriteElementString("asurakuDeliveryId", dtItem.Rows[0]["Campaign_TypeID"].ToString());//あす楽配送管理番号
            writer.WriteElementString("sizeChartLinkCode", dtItem.Rows[0]["RF_Size_Chartlink"].ToString());//サイズ表リンク
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["RF_Review_Text"].ToString()))
                writer.WriteElementString("reviewDisp", dtItem.Rows[0]["RF_Review_Text"].ToString());//レビュー本文表示
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["RD_DualPricing_Ctrl_No"].ToString()))
                writer.WriteElementString("displayPriceId", dtItem.Rows[0]["RD_DualPricing_Ctrl_No"].ToString());//二重価格文言管理番号
            #endregion

            #region Category
            if (dtCategory.Rows.Count > 0)
            {
                writer.WriteStartElement("categories");
                for (int i = 0; i < dtCategory.Rows.Count; i++)
                {
                    writer.WriteStartElement("categoryInfo");
                    writer.WriteElementString("categorySetManageNumber", dtCategory.Rows[i]["カテゴリセット管理番号"].ToString());//カテゴリセット管理番号
                    //string categoryID = GetCategory(dtCategory.Rows[i]["カテゴリセット管理番号"].ToString(), dtCategory.Rows[i]["表示先カテゴリ"].ToString());
                    //writer.WriteElementString("categoryId", categoryID);
                    if (!String.IsNullOrWhiteSpace(dtCategory.Rows[i]["CategoryID"].ToString()))
                        writer.WriteElementString("categoryId", dtCategory.Rows[i]["CategoryID"].ToString());
                    else count = 1;
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["RF_Header_Footer"].ToString()))
                writer.WriteElementString("layoutCommonId", dtItem.Rows[0]["RF_Header_Footer"].ToString());//ヘッダー・フッター・レフトナビ
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["RF_Display_Order"].ToString()))
                writer.WriteElementString("layoutMapId", dtItem.Rows[0]["RF_Display_Order"].ToString());//表示項目の並び順
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["RF_Common_Description1"].ToString()))
                writer.WriteElementString("textSmallId", dtItem.Rows[0]["RF_Common_Description1"].ToString());//共通説明文（小）
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["RD_Featured_Item"].ToString()))
                writer.WriteElementString("lossLeaderId", dtItem.Rows[0]["RD_Featured_Item"].ToString());//目玉商品
            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["RF_Common_Description2"].ToString()))
                writer.WriteElementString("textLargeId", dtItem.Rows[0]["RF_Common_Description2"].ToString());//共通説明文（大）
            #endregion

            writer.WriteEndElement();//item
            writer.WriteEndElement();//itemInsertRequest
            writer.WriteEndElement();//request
            writer.Flush();
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        static void UploadItem(DataTable dtItem, DataTable dtInventory, DataTable dtCategory, DataTable dtOption, DataTable dtImage, string itemCode)
        {
            #region Image
            string shopid = dtItem.Rows[0]["Shop_ID"].ToString();
            string itemcode = dtItem.Rows[0]["Item_Code"].ToString();
            string itemid = itemCode;
            DataTable dtPromotionID = GetPromotionItemCode(itemcode, shopid);
            DataTable dtItemimage = new DataTable();
            DataTable dtimage = new DataTable();
            String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            String promotionID = dtPromotionID.Rows[0]["PromotionID"].ToString();
            if (!string.IsNullOrWhiteSpace(promotionID))
            {
                dtItemimage = GetItemImage(itemid);
                dtimage = GetCampaignImage(itemid, Convert.ToInt32(shopid), promotionID);
                ImageZipFile(dtItemimage, dtimage, shopid, date);
                string folderName = dtItemimage.Rows[0]["Folder_Name"].ToString();
                ZipImage(shopid, folderName, date);
            }
            else
            {
                if (dtImage != null && dtImage.Rows.Count > 0)
                {

                    string path = "";
                    string folderName = "";
                    string image_name = "";
                    string sku = ExportCSVPath + shopid + "/item_img/";

                    foreach (DataRow drImage in dtImage.Rows)
                    {
                        if (drImage["Image_Name"].ToString() != "")
                        {
                            image_name = drImage["Image_Name"].ToString();
                            folderName = drImage["Folder_Name"].ToString();

                            path = sku + folderName + "/";

                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);
                            //Save image into folder

                            if (Directory.Exists(path))
                            {
                                if (File.Exists(ItemImage + image_name))
                                {
                                    if (!File.Exists(path + image_name))
                                        File.Copy(ItemImage + image_name, path + image_name);
                                }
                            }
                        }
                    }

                    if (Directory.Exists(sku))
                    {
                        using (ZipFile zipfile = new ZipFile())
                        {
                            int File_Type = 3;
                            int ShopID = int.Parse(shopid);
                            int IsExport = 0;
                            int Export_Type = 1;

                            zipfile.AddDirectory(sku, "item_img");
                            zipfile.Save(ExportCSVPath + shopid + "/" + folderName + "$" + shopid + "_" + date + ".zip");
                            zipfile.Save(Bak_ExportCSVPath + folderName + "$" + shopid + "_" + date + ".zip");
                            SaveItem_ExportQ(folderName + "$" + shopid + "_" + date + ".zip", File_Type, ShopID, IsExport, Export_Type);
                        }
                        DeleteDirectory(sku, true);
                    }
                }
            }


            //if (dtImage != null && dtImage.Rows.Count > 0)
            //{
            //    string path = "";
            //    string folderName = "";
            //    string image_name = "";
            //    string sku = ExportCSVPath + "1" + "/item_img/";
            //    foreach (DataRow drImage in dtImage.Rows)
            //    {
            //        if (drImage["Image_Name"].ToString() != "")
            //        {
            //            image_name = drImage["Image_Name"].ToString();
            //            folderName = drImage["Folder_Name"].ToString();
            //            path = sku + folderName + "/";
            //            if (!Directory.Exists(path))
            //                Directory.CreateDirectory(path);
            //            //Save image into folder
            //            if (Directory.Exists(path))
            //            {
            //                if (File.Exists(ItemImage + image_name))
            //                {
            //                    if (!File.Exists(path + image_name))
            //                        File.Copy(ItemImage + image_name, path + image_name);
            //                }
            //            }
            //        }
            //    }
            //    if (Directory.Exists(sku))
            //    {
            //        using (ZipFile zipfile = new ZipFile())
            //        {
            //            String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            //            zipfile.AddDirectory(sku, "item_img");
            //            zipfile.Save(ExportCSVPath + "1" + "/" + folderName + "$" + "1" + "_" + date + ".zip");
            //            zipfile.Save(Bak_ExportCSVPath + folderName + "$" + "1" + "_" + date + ".zip");
            //            SaveItem_ExportQ(folderName + "$" + "1" + "_" + date + ".zip", 3, 1, 0, 1);
            //        }
            //        DeleteDirectory(sku, true);
            //    }
            //}
            #endregion
            string xmlString = GetXMl_NewUpdate(dtItem, dtInventory, dtCategory, dtOption, itemCode, 0);
            string apiSecret = dtAPIKey.Rows[0]["APISecret"].ToString();
            string apiKey = dtAPIKey.Rows[0]["APIKey"].ToString();
            if (count == 1)
            {
                InsertErrorMessage(itemCode, "CategoryID is null");
                //goto label;
            }
            else
            {
            l1:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.rms.rakuten.co.jp/es/1.0/item/insert");
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
                        XmlNodeList list = xDoc.GetElementsByTagName("code");
                        if (list[0].InnerText.Equals("C114"))//still updating data in mall
                        {
                            goto l1;
                        }
                        else if (!list[0].InnerText.Equals("N000"))
                        {
                            XmlNodeList list1 = xDoc.GetElementsByTagName("msg");
                            if ((list1[0].InnerText.Equals("C219")) || (list1[0].InnerText.Equals("1つのカテゴリに同じ商品が属するような設定はできません。")))
                                InsertErrorMessage(itemCode, "N000");//to change exhibition recovery flag

                            else if (list1.Count > 0)
                                InsertErrorMessage(itemCode, list1[0].InnerText);//to change ErrorCheck flag=1
                            else
                            {
                                foreach (XmlNode node in list1)
                                {
                                    if (
                                       !node.InnerText.Contains("商品管理番号（商品URL）欄にすでに登録済みのものは指定できません。重複がありましたのでご確認ください。")
                                       && !node.InnerText.Contains("商品管理番号（商品URL）欄で指定した該当の商品には対象となる項目選択肢別在庫の項目・選択肢の登録はありません。")
                                       && !node.InnerText.Contains("項目選択肢別在庫が指定されています。倉庫指定欄で倉庫に入れる指定がない場合でも登録完了時に項目選択肢別在庫(select.csv)が正しく設定されていない間は倉庫に入った状態となり、商品ページにアクセスすることはできない可能性があります。")
                                       && !node.InnerText.Contains("こちらの商品は商品登録・更新時に倉庫指定欄で倉庫に入れる指定をしていませんでした。項目選択肢別在庫(select.csv)が正しく設定されていない間は倉庫に入った状態となり、商品ページにアクセスすることはできない可能性があります。正しく登録された場合は倉庫から自動的に出しています。")
                                       && !node.InnerText.Contains("項目選択肢別在庫が指定されています。倉庫指定欄で倉庫に入れる指定がない場合でも登録完了時に項目選択肢別在庫(select.csv)が正しく設定されていない間は倉庫に入った状態となり、商品ページにアクセスすることはできない可能性があります。")
                                       && !node.InnerText.Contains("項目別選択肢在庫用選択肢の組み合わせが足りません。各選択肢の組み合わせをご確認ください。")
                                       && !node.InnerText.Contains("1つのカテゴリに同じ商品が属するような設定はできません。")
                                       && !node.InnerText.Contains("商品管理番号欄（商品URL）で指定された商品が見つかりません。新規登録、更新・変更、削除の場合は、存在する商品の商品管理番号（商品URL）をご指定ください。")
                                        && !node.InnerText.Contains("コントロールカラム欄に入力可能な文字はn（新規）、u（更新・変更）、d（削除）だけです。" +
                                        "※オークション商品の一括登録では、u（更新・変更）は入力できません。")
                                       && !node.InnerText.Contains("商品管理番号（商品URL)欄には既に登録済みの商品を指定してください。")
                                       && !node.InnerText.Contains("指定されたカテゴリがカテゴリセット管理番号24のセットに存在しません。")
                                       && !node.InnerText.Contains("指定されたカテゴリがカテゴリセット管理番号20のセットに存在しません。")
                                       && !node.InnerText.Contains("Select/Checkbox用選択肢欄に既に登録されているものがあります。同じ選択肢名は登録できません。")

                                      )
                                    {
                                        InsertErrorMessage(itemCode, node.InnerText);//to change ErrorCheck flag and Insert Error Msg
                                    }
                                }
                            }
                        }
                        else
                        {
                            InsertErrorMessage(itemCode, "N000");//to change exhibition recovery flag
                        }
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
                            InsertErrorMessage(itemCode, text);
                        }
                    }
                }
            }
        }

        static void CheckItemCodeForNewUpdate(string ItemList)
        {
            string itemID = ItemList.Trim();
            DataTable dt = GetData(itemID, 1);
            if (dt.Rows.Count > 0)
            {
                string apiKey = dt.Rows[0]["APIKey"].ToString();
                string apiSecret = dt.Rows[0]["APISecret"].ToString();
                string itemCode = dt.Rows[0]["Item_Code"].ToString();
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
                wordList = resultXml.GetElementsByTagName("code");
                //Item Exists
                if (wordList[0].InnerText == "N000")
                {
                    CtrlID = "u";
                }
                //Item Not Exists
                else if (wordList[0].InnerText == "C001")
                {
                    CtrlID = "n";
                }
            }
        }

        static DataTable GetPromotionItemCode(string itemcode, string shopid)
        {

            DataTable dtpromotion = new DataTable();
            string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(con);
            SqlDataAdapter sda = new SqlDataAdapter("SP_GetPromotionItemCode", conn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.CommandTimeout = 0;
            sda.SelectCommand.Parameters.AddWithValue("@itemcode", itemcode);
            sda.SelectCommand.Parameters.AddWithValue("@ShopID", shopid);
            sda.SelectCommand.Connection.Open();
            sda.Fill(dtpromotion);
            sda.SelectCommand.Connection.Close();
            return dtpromotion;
        }

        public static void ImageZipFile(DataTable dtItemimage, DataTable dtimage, string shop_id, string date)
        {
            string path = "";
            string folderName = "";
            string image_name = "";
            string image1 = "";
            string image2 = "";
            string image3 = "";
            string img1 = "";
            ArrayList img2 = new ArrayList();
            string sku = ExportCSVPath + shop_id + "/item_img/";

            if (dtItemimage != null && dtItemimage.Rows.Count > 0)
            {
                img1 = dtItemimage.Rows[0]["Image_Name"].ToString();
            }
            if (dtimage != null && dtimage.Rows.Count > 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    img2.Add(dtimage.Rows[i]["Image_Name"].ToString());
                }
            }
            foreach (DataRow drImage in dtItemimage.Rows)
            {
                if (img1 != null && drImage["SN"].ToString() == "1")
                {
                    if (File.Exists(ItemImage + img1))
                    {
                        image1 = ItemImage + img1;
                    }
                    image2 = ItemImage + img2[0].ToString();
                    image3 = ItemImage + img2[1].ToString();
                    Bitmap second = SetImageOpacity((Bitmap)Image.FromFile(image2));
                    Bitmap third = SetImageOpacity((Bitmap)Image.FromFile(image3));

                    Bitmap first = new Bitmap(SetImageOpacity((Bitmap)Image.FromFile(image1)), new Size(500, 500));

                    if (second.Width > third.Width)
                    {
                        Bitmap sec = new Bitmap(second, new Size(500, 100));
                        Bitmap thd = new Bitmap(third, new Size(100, 600));
                        Bitmap result1 = new Bitmap(Math.Max(first.Width, sec.Width), first.Height + sec.Height);
                        Bitmap result2 = new Bitmap(result1.Width + thd.Width, Math.Max(result1.Height, thd.Height));

                        Graphics g = Graphics.FromImage(result2);
                        g.DrawImage(thd, 0, 0);
                        g.DrawImage(first, thd.Width, 0);
                        g.DrawImage(sec, thd.Width, first.Height);

                        image_name = img1;
                        folderName = drImage["Folder_Name"].ToString();

                        path = sku + folderName + "/";

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        //Save image into folder

                        if (Directory.Exists(path))
                        {
                            if (File.Exists(ItemImage + image_name))
                            {
                                if (!File.Exists(path + image_name))
                                {
                                    result2.Save(path + img1, ImageFormat.Jpeg);
                                }
                            }
                        }
                    }

                    else
                    {
                        Bitmap sec = new Bitmap(third, new Size(500, 100));
                        Bitmap thd = new Bitmap(second, new Size(100, 600));
                        Bitmap result1 = new Bitmap(Math.Max(first.Width, sec.Width), first.Height + sec.Height);
                        Bitmap result2 = new Bitmap(result1.Width + thd.Width, Math.Max(result1.Height, thd.Height));

                        Graphics g = Graphics.FromImage(result2);
                        g.DrawImage(thd, 0, 0);
                        g.DrawImage(first, thd.Width, 0);
                        g.DrawImage(sec, thd.Width, first.Height);

                        image_name = img1;
                        folderName = drImage["Folder_Name"].ToString();

                        path = sku + folderName + "/";

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        //Save image into folder

                        if (Directory.Exists(path))
                        {
                            if (File.Exists(ItemImage + image_name))
                            {
                                if (!File.Exists(path + image_name))
                                {
                                    result2.Save(path + img1, ImageFormat.Jpeg);
                                }
                            }
                        }
                    }
                }
                else
                {
                    image_name = drImage["Image_Name"].ToString();
                    folderName = drImage["Folder_Name"].ToString();

                    path = sku + folderName + "/";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //Save image into folder

                    if (Directory.Exists(path))
                    {
                        if (File.Exists(ItemImage + image_name))
                        {
                            if (!File.Exists(path + image_name))
                            {
                                File.Copy(ItemImage + image_name, path + image_name);
                            }
                        }
                    }
                }
            }

        }

        public static void ZipImage(string shop_id, string folderName, string date)
        {
            string sku = ExportCSVPath + shop_id + "/item_img/";

            if (Directory.Exists(sku))
            {
                using (ZipFile zipfile = new ZipFile())
                {
                    zipfile.AddDirectory(sku, "item_img");
                    zipfile.Save(ExportCSVPath + shop_id + "/" + folderName + "$" + shop_id + "_" + date + ".zip");
                    zipfile.Save(Bak_ExportCSVPath + folderName + "$" + shop_id + "_" + date + ".zip");
                    string File_Name = folderName + "$" + shop_id + "_" + date + ".zip";
                    int File_Type = 3;
                    int ShopID = int.Parse(shop_id);
                    int IsExport = 0;
                    int Export_Type = 1;
                    SaveItem_ExportQ(folderName + "$" + "1" + "_" + date + ".zip", File_Type, ShopID, IsExport, Export_Type);
                }
                DeleteDirectory(sku, true);
            }
        }

        public static Bitmap SetImageOpacity(Image image)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);

            try
            {
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    ColorMatrix matrix = new ColorMatrix();
                    ImageAttributes attributes = new ImageAttributes();
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            catch (Exception ex)
            {

                string error = ex.Message;
            }
            return bmp;
        }

        public static DataTable GetItemImage(string list)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                string query = "GetItemImage_Rakuten";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@strString", list);
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

        public static DataTable GetCampaignImage(string list, int sid, string pid)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                string query = "GetImage_CampaignPromotion_Rakuten";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@strString", list);
                da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
                da.SelectCommand.Parameters.AddWithValue("@PromotionID", pid);
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

        static void UpdateItem(DataTable dtItem, DataTable dtInventory, DataTable dtCategory, DataTable dtOption, DataTable dtImage, string itemCode)
        {
            #region Image

            string shopid = dtItem.Rows[0]["Shop_ID"].ToString();
            string itemcode = dtItem.Rows[0]["Item_Code"].ToString();
            string itemid = itemCode;
            DataTable dtPromotionID = GetPromotionItemCode(itemcode, shopid);
            DataTable dtItemimage = new DataTable();
            DataTable dtimage = new DataTable();
            String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            String promotionID = dtPromotionID.Rows[0]["PromotionID"].ToString();
            if (!string.IsNullOrWhiteSpace(promotionID))
            {
                dtItemimage = GetItemImage(itemid);
                dtimage = GetCampaignImage(itemid, Convert.ToInt32(shopid), promotionID);
                ImageZipFile(dtItemimage, dtimage, shopid, date);
                string folderName = dtItemimage.Rows[0]["Folder_Name"].ToString();
                ZipImage(shopid, folderName, date);
            }
            else
            {
                if (dtImage != null && dtImage.Rows.Count > 0)
                {

                    string path = "";
                    string folderName = "";
                    string image_name = "";
                    string sku = ExportCSVPath + shopid + "/item_img/";

                    foreach (DataRow drImage in dtImage.Rows)
                    {
                        if (drImage["Image_Name"].ToString() != "")
                        {
                            image_name = drImage["Image_Name"].ToString();
                            folderName = drImage["Folder_Name"].ToString();

                            path = sku + folderName + "/";

                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);
                            //Save image into folder

                            if (Directory.Exists(path))
                            {
                                if (File.Exists(ItemImage + image_name))
                                {
                                    if (!File.Exists(path + image_name))
                                        File.Copy(ItemImage + image_name, path + image_name);
                                }
                            }
                        }
                    }

                    if (Directory.Exists(sku))
                    {
                        using (ZipFile zipfile = new ZipFile())
                        {
                            int File_Type = 3;
                            int ShopID = int.Parse(shopid);
                            int IsExport = 0;
                            int Export_Type = 1;

                            zipfile.AddDirectory(sku, "item_img");
                            zipfile.Save(ExportCSVPath + shopid + "/" + folderName + "$" + shopid + "_" + date + ".zip");
                            zipfile.Save(Bak_ExportCSVPath + folderName + "$" + shopid + "_" + date + ".zip");
                            SaveItem_ExportQ(folderName + "$" + shopid + "_" + date + ".zip", File_Type, ShopID, IsExport, Export_Type);
                        }
                        DeleteDirectory(sku, true);
                    }
                }
            }


            //if (dtImage != null && dtImage.Rows.Count > 0)
            //{
            //    string path = "";
            //    string folderName = "";
            //    string image_name = "";
            //    string sku = ExportCSVPath + "1" + "/item_img/";
            //    foreach (DataRow drImage in dtImage.Rows)
            //    {
            //        if (drImage["Image_Name"].ToString() != "")
            //        {
            //            image_name = drImage["Image_Name"].ToString();
            //            folderName = drImage["Folder_Name"].ToString();
            //            path = sku + folderName + "/";
            //            if (!Directory.Exists(path))
            //                Directory.CreateDirectory(path);
            //            //Save image into folder
            //            if (Directory.Exists(path))
            //            {
            //                if (File.Exists(ItemImage + image_name))
            //                {
            //                    if (!File.Exists(path + image_name))
            //                        File.Copy(ItemImage + image_name, path + image_name);
            //                }
            //            }
            //        }
            //    }
            //    if (Directory.Exists(sku))
            //    {
            //        using (ZipFile zipfile = new ZipFile())
            //        {
            //            String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            //            zipfile.AddDirectory(sku, "item_img");
            //            zipfile.Save(ExportCSVPath + "1" + "/" + folderName + "$" + "1" + "_" + date + ".zip");
            //            zipfile.Save(Bak_ExportCSVPath + folderName + "$" + "1" + "_" + date + ".zip");
            //            SaveItem_ExportQ(folderName + "$" + "1" + "_" + date + ".zip", 3, 1, 0, 1);
            //        }
            //        DeleteDirectory(sku, true);
            //    }
            //}
            #endregion
            string xmlString = GetXMl_NewUpdate(dtItem, dtInventory, dtCategory, dtOption, itemCode, 1);
            string apiSecret = dtAPIKey.Rows[0]["APISecret"].ToString();
            string apiKey = dtAPIKey.Rows[0]["APIKey"].ToString();
            if (count == 1)
            {
                InsertErrorMessage(itemCode, "CategoryID is null");
                //goto label;
            }
            else
            {
            l1:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.rms.rakuten.co.jp/es/1.0/item/update");
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
                        XmlNodeList list = xDoc.GetElementsByTagName("code");
                        if (list[0].InnerText.Equals("C114"))//still updating data in mall
                        {
                            goto l1;
                        }
                        else if (!list[0].InnerText.Equals("N000"))
                        {
                            XmlNodeList list1 = xDoc.GetElementsByTagName("msg");
                            if ((list1[0].InnerText.Equals("C219")) || (list1[0].InnerText.Equals("1つのカテゴリに同じ商品が属するような設定はできません。")))
                                InsertErrorMessage(itemCode, "N000");//to change exhibition recovery flag

                            else if (list1.Count > 0)
                                InsertErrorMessage(itemCode, list1[0].InnerText);//to change ErrorCheck flag=1
                            else
                            {
                                foreach (XmlNode node in list1)
                                {
                                    if (
                                        !node.InnerText.Contains("商品管理番号（商品URL）欄にすでに登録済みのものは指定できません。重複がありましたのでご確認ください。")
                                       && !node.InnerText.Contains("商品管理番号（商品URL）欄で指定した該当の商品には対象となる項目選択肢別在庫の項目・選択肢の登録はありません。")
                                       && !node.InnerText.Contains("項目選択肢別在庫が指定されています。倉庫指定欄で倉庫に入れる指定がない場合でも登録完了時に項目選択肢別在庫(select.csv)が正しく設定されていない間は倉庫に入った状態となり、商品ページにアクセスすることはできない可能性があります。")
                                       && !node.InnerText.Contains("こちらの商品は商品登録・更新時に倉庫指定欄で倉庫に入れる指定をしていませんでした。項目選択肢別在庫(select.csv)が正しく設定されていない間は倉庫に入った状態となり、商品ページにアクセスすることはできない可能性があります。正しく登録された場合は倉庫から自動的に出しています。")
                                       && !node.InnerText.Contains("項目選択肢別在庫が指定されています。倉庫指定欄で倉庫に入れる指定がない場合でも登録完了時に項目選択肢別在庫(select.csv)が正しく設定されていない間は倉庫に入った状態となり、商品ページにアクセスすることはできない可能性があります。")
                                       && !node.InnerText.Contains("項目別選択肢在庫用選択肢の組み合わせが足りません。各選択肢の組み合わせをご確認ください。")
                                       && !node.InnerText.Contains("1つのカテゴリに同じ商品が属するような設定はできません。")
                                       && !node.InnerText.Contains("商品管理番号欄（商品URL）で指定された商品が見つかりません。新規登録、更新・変更、削除の場合は、存在する商品の商品管理番号（商品URL）をご指定ください。")
                                        && !node.InnerText.Contains("コントロールカラム欄に入力可能な文字はn（新規）、u（更新・変更）、d（削除）だけです。" +
                                        "※オークション商品の一括登録では、u（更新・変更）は入力できません。")
                                       && !node.InnerText.Contains("商品管理番号（商品URL)欄には既に登録済みの商品を指定してください。")
                                       && !node.InnerText.Contains("指定されたカテゴリがカテゴリセット管理番号24のセットに存在しません。")
                                       && !node.InnerText.Contains("指定されたカテゴリがカテゴリセット管理番号20のセットに存在しません。")
                                       && !node.InnerText.Contains("Select/Checkbox用選択肢欄に既に登録されているものがあります。同じ選択肢名は登録できません。")
                                       && !node.InnerText.Contains("1つの商品は６カテゴリ以上に属する事はできません。")
                                       )
                                    {
                                        InsertErrorMessage(itemCode, node.InnerText);//to change ErrorCheck flag and Insert Error Msg
                                    }
                                }
                            }
                        }
                        else
                        {
                            InsertErrorMessage(itemCode, "N000");//to change exhibition recovery flag
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
                                if (error.Contains("message"))
                                {
                                    XmlDocument xd = new XmlDocument();
                                    xd.LoadXml(error);
                                    error = xd.InnerText;
                                    InsertErrorMessage(itemCode, error);
                                }
                            }
                        }
                    }
                    //return error;
                }
                finally
                {
                }
            }
        }

        static string GetCategory(string CatsetNo, string catName)
        {
            string[] cat = catName.Split('\\');
            catName = cat[cat.Length - 1];
            string apiSecret = dtAPIKey.Rows[0]["APISecret"].ToString();
            string apiKey = dtAPIKey.Rows[0]["APIKey"].ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.rms.rakuten.co.jp/es/1.0/categoryapi/shop/categories/get?categorySetManageNumber=" + CatsetNo);
            request.Method = "GET";
            String encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(apiSecret + ":" + apiKey));
            request.Headers.Add("Authorization", "ESA " + encoded);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string xml = string.Empty;
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                xml = reader.ReadToEnd();
            }
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xml);
            XmlNode node = xDoc.SelectSingleNode("result/categoriesGetResult/categoryList");
            DataSet ds = new DataSet();
            ds.ReadXml(new XmlNodeReader(node));
            string categoryId = string.Empty;
            DataTable dt = ds.Tables[0];
            DataRow[] dr = dt.Select("name = '" + catName + "'");
            if (dr.Count() > 0)
                categoryId = dr[0]["categoryId"].ToString();
            if (String.IsNullOrWhiteSpace(categoryId))
                count = 1;
            return categoryId;
        }

        public static void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                List<string> headerValues = new List<string>();

                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerValues.Add(QuoteValue(column.ColumnName));
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

        private static string QuoteValue(string value)
        {
            return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
        }


        public static void DeleteDirectory(string path, bool recursive)
        {
            // Delete all files and sub-folders?
            if (recursive)
            {
                // Yep... Let's do this
                var subfolders = Directory.GetDirectories(path);
                foreach (var s in subfolders)
                {
                    DeleteDirectory(s, recursive);
                }
            }

            // Get all files of the folder
            var files = Directory.GetFiles(path);
            foreach (var f in files)
            {
                // Get the attributes of the file
                var attr = File.GetAttributes(f);

                // Is this file marked as 'read-only'?
                if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // Yes... Remove the 'read-only' attribute, then
                    File.SetAttributes(f, attr ^ FileAttributes.ReadOnly);
                }
                // Delete the file
                File.Delete(f);
            }
            // When we get here, all the files of the folder were
            // already deleted, so we just delete the empty folder   
            Directory.Delete(path);
        }

        static DataTable GetData(String ItemID, int ShopID)
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand("SELECT S.APIKey,S.APISecret,S.ID AS ShopID,S.Shop_ID AS ShopUrl,IM.Item_Code,IM.ID from Shop S,Item_Master IM WHERE IM.ID ='" + ItemID + "' AND S.ID =" + ShopID, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public static void CreateCategoryToMall(DataTable dtCategory)
        {
            DataRow[] rowRakuten = dtCategory.Select("CategoryID=null or CategoryID=''");

            if (rowRakuten.Count() > 0)
            {
                DataTable dt = dtCategory.Select("CategoryID=null or CategoryID=''").CopyToDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dtDescPID = SelectDescription(dt.Rows[i]["表示先カテゴリ"].ToString());
                    DataTable dtCategorylist = NewDataTable();
                    DataRow dr = dtCategorylist.NewRow();
                    dr["ID"] = Convert.ToInt32(dtDescPID.Rows[i]["ID"].ToString());
                    dr["Path"] = dt.Rows[i]["表示先カテゴリ"].ToString();
                    dr["Category_ID_New"] = "";
                    dr["Description"] = dtDescPID.Rows[0]["Description"].ToString();
                    dr["ParentID"] = dtDescPID.Rows[0]["ParentID"].ToString(); //dt.Rows[i]["ParentID"].ToString();
                    dr["Rakutan_CategoryNo"] = dt.Rows[i]["カテゴリセット管理番号"].ToString();
                    dtCategorylist.Rows.Add(dr);
                    dtCategorylist.AcceptChanges();

                    DataTable dtParentCatID = GetParentCategoryToCreate(dtDescPID.Rows[0]["ParentID"].ToString());
                    while (dtParentCatID.Rows.Count > 0 && String.IsNullOrWhiteSpace(dtParentCatID.Rows[0]["Category_ID_New"].ToString()))
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
        }

        public static void PrepareAndUpload(DataTable dtCategorylist)
        {
            //DataView dv = new DataView(dtCategorylist);
            //dv.Sort = "ParentID";
            //dtCategorylist = dv.ToTable();

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
                    writer.WriteElementString("categoryId", dtCagetoryID.Rows[0]["Category_ID_New"].ToString());
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
                        if (list1[0].InnerText.Equals("C220"))
                        {
                            errCode = "C220";
                            ChangeCategoryFlag(dtCategory.Rows[0]["商品管理番号（商品URL）"].ToString());
                            break;
                        }
                        else
                        {
                            XmlNodeList list = xDoc.GetElementsByTagName("categoryId");
                            string str = list[0].InnerText;
                            if (!string.IsNullOrWhiteSpace(str))
                            {
                                foreach (DataRow dr in dtCategory.Rows) // search whole table
                                {
                                    if (dr["表示先カテゴリ"] == dtCategorylist.Rows[i]["Path"].ToString())
                                    {
                                        dr["CategoryID"] = str;     //bind new category
                                        dtCategory.AcceptChanges();
                                        break;
                                    }
                                }
                                UpdateCategoryID(str, dtCategorylist.Rows[i]["Path"].ToString());//update created categoryID in Category table
                            }
                        }
                    }
                }
                catch (WebException e)
                {
                }
                finally
                {
                }
                #endregion
            }
        }

        #endregion

        #region SelectData


        static DataTable GetAPIKey(int shopID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetAPIKey", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shopID);
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

        static string SelectExhibitionItemID()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_List_SelectItem_IDList_New", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", 1);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt.Rows[0]["Item_ID"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static DataTable GetItemData(string list, int shop_id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_List_CollectData_New_Rakuten_API", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ItemID", list);
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
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

        static DataTable SelectLogExhibitionSelect(int shop_id, int mall_id, string itemID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Select_SelectByShop_Rakuten_API", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID", mall_id);
                sda.SelectCommand.Parameters.AddWithValue("@itemID", itemID);
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

        static DataTable SelectLogExhibitionCategory(int shop_id, int mall_id, string itemID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Category_SelectByShop_Rakuten_API", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID", mall_id);
                sda.SelectCommand.Parameters.AddWithValue("@itemID", itemID);
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

        static DataTable SelectLogExhibitionImage(int shop_id, string itemcode)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetImageList_Select_By_Mall_API", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", itemcode);
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

        static DataTable SelectDescription(string path)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_SelectDescriptionByPath", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@path", path);
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


        static void UpdateCategoryID(string categoryID, string path)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_UpdateCategoryID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                cmd.Parameters.AddWithValue("@path", path);
                cmd.Parameters.AddWithValue("@shopID", 1);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region SaveData
        public static void Save_SYS_Errorlog(string error)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", -1);
            cmd.Parameters.AddWithValue("@ErrorDetail", "APIRakutenRacket" + error);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }


        public static void SaveItem_ExportQ(string FileName, int FileType, int ShopID, int IsExport, int Export_Type)
        {
            SqlConnection connectionstring = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Item_ExportQ_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionstring;
                cmd.Parameters.AddWithValue("@File_Name", FileName);
                cmd.Parameters.AddWithValue("@File_Type", FileType);
                cmd.Parameters.AddWithValue("@ShopID", ShopID);
                cmd.Parameters.AddWithValue("@IsExport", IsExport);
                cmd.Parameters.AddWithValue("@Export_Type", Export_Type);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static void SaveNewItemHistory(string exhibitID, int shopID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_SaveNewItemHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@exhibitID", exhibitID);
                cmd.Parameters.AddWithValue("@shopID", shopID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void InsertErrorMessage(string itemID, string msg)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("Export_ErrorCheck_InsertAPI", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ItemID", itemID);
                cmd.Parameters.AddWithValue("@Message", msg);
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

        static void SaveLogExhibition(DataTable dt, string list, int shop_id)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();
                    SqlConnection conn = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_Insert_Rakuten_API");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Connection = conn;
                    result = result.Replace("&#", "$CapitalSports$");
                    cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
                    cmd.Parameters.AddWithValue("@strString", list);
                    cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void SaveItemShopAPIInfo(int Exhibit_ID, int shopID, string csvName, string ctrl_id)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Exhibition_Item_Shop_UpdateInfo_API", connectionstring);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Exhibit_ID", Exhibit_ID);
                cmd.Parameters.AddWithValue("@Shop_ID", shopID);
                cmd.Parameters.AddWithValue("@CSV_Name", csvName);
                cmd.Parameters.AddWithValue("@Ctrl_ID", ctrl_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void AddExportedDate(int shopID, int exhibitid)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Exhibition_Item_Shop_ChangeFlag_API", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Shop_ID", shopID);
                cmd.Parameters.AddWithValue("@Exhibit_ID", exhibitid);
                cmd.CommandTimeout = 0;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "API_RakutenRacket.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
        #endregion
    }
}
