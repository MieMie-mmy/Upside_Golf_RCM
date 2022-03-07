using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Data.Common;

namespace ORS_RCM_DL
{
    public class Log_Exhibition_DL
    {
        public Log_Exhibition_DL() { }

        public void SaveLogExhibition(string xml, string list, int shop_id)
        {


            try
            {
                SqlConnection conn = new SqlConnection(DataConfig.connectionString);
                //SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_Insert");
                SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_Insert_Rakuten");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = conn;
                xml = xml.Replace("&#", "$CapitalSports$");
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@strString", list);
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();


            }
            catch (Exception ex)
            {


                throw ex;

            }

        }

        public void SaveLogExhibitionItem(string xml)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Item_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                xml = xml.Replace("&#", "$CapitalSports$");
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

        public void SaveLogExhibitionSelect(string list)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Select_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@strString", list);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveLogExhibitionSelect(string list, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Select_InsertByShop";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@strString", list);
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveLogExhibitionCategory(string list)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Category_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@strString", list);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveLogExhibitionCategory(string list, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Category_InsertByShop";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@strString", list);
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ChangeFlagLogExhition(int Exhibit_ID, int Item_ID, int Shop_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_ChangeFlag", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Parameters.AddWithValue("@Exhibit_ID", Exhibit_ID);
                cmd.Parameters.AddWithValue("@Item_ID", Item_ID);
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


        public void SaveExhibitionItemFile(string xml, int shop_id)
        {
            try
            {
                //SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_Item_Rakuten_Insert", connectionString);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = 0;
                //SqlDataAdapter sda = new SqlDataAdapter();
                //cmd.Parameters.Add("@Exhibit_ID", SqlDbType.Int, 50, "Exhibit_ID");

                //cmd.Parameters.Add("@Item_ID", SqlDbType.Int, 50, "Item_ID");
                //cmd.Parameters.Add("@IsSKU", SqlDbType.Int, 50, "IsSKU");
                //cmd.Parameters.Add("@Ctrl_ID", SqlDbType.NVarChar, -1, "コントロールカラム");
                //cmd.Parameters.Add("@Item_Code", SqlDbType.NVarChar, 200, "商品番号");
                //cmd.Parameters.Add("@Rakuten_CategoryID", SqlDbType.NVarChar, 4000, "全商品ディレクトリID");
                //cmd.Parameters.Add("@Tag_ID", SqlDbType.NVarChar, 200, "タグID");
                //cmd.Parameters.Add("@Copy_Decoration", SqlDbType.NVarChar, 4000, "PC用キャッチコピー");
                //cmd.Parameters.Add("@Item_Name", SqlDbType.NVarChar, 200, "商品名");
                //cmd.Parameters.Add("@Sale_Price", SqlDbType.NVarChar, 200, "販売価格");
                //cmd.Parameters.Add("@List_Price", SqlDbType.NVarChar, 200, "表示価格");
                //cmd.Parameters.Add("@Consumption_Tax", SqlDbType.NVarChar, 200, "消費税");
                //cmd.Parameters.Add("@Special_Flag", SqlDbType.NVarChar, 200, "送料");
                //cmd.Parameters.Add("@Extra_Shipping", SqlDbType.NVarChar, 200, "個別送料");
                //cmd.Parameters.Add("@Shipping_Category1", SqlDbType.NVarChar, 200, "送料区分1");
                //cmd.Parameters.Add("@Shipping_Category2", SqlDbType.NVarChar, 200, "送料区分2");
                //cmd.Parameters.Add("@Delivery_Charges", SqlDbType.NVarChar, 200, "代引料");
                //cmd.Parameters.Add("@Warehouse_Specified", SqlDbType.NVarChar, 200, "倉庫指定");
                //cmd.Parameters.Add("@Product_Information", SqlDbType.NVarChar, 200, "商品情報レイアウト");
                //cmd.Parameters.Add("@Order_Button", SqlDbType.NVarChar, 200, "注文ボタン");
                //cmd.Parameters.Add("@Request_Button", SqlDbType.NVarChar, 200, "資料請求ボタン");
                //cmd.Parameters.Add("@Product_Inquiry_Button", SqlDbType.NVarChar, 200, "商品問い合わせボタン");
                //cmd.Parameters.Add("@Comingsoon_Button", SqlDbType.NVarChar, 200, "再入荷お知らせボタン");
                //cmd.Parameters.Add("@Mobile_Display", SqlDbType.NVarChar, 200, "モバイル表示");
                //cmd.Parameters.Add("@Corresponding_Work", SqlDbType.NVarChar, 200, "のし対応");
                //cmd.Parameters.Add("@Item_Description_PC", SqlDbType.NVarChar, 1000, "PC用商品説明文");
                //cmd.Parameters.Add("@Item_Description_Mobile", SqlDbType.NVarChar, 200, "モバイル用商品説明文");
                //cmd.Parameters.Add("@Smart_Template", SqlDbType.NVarChar, 200, "スマートフォン用商品説明文");
                //cmd.Parameters.Add("@Sale_Description_PC", SqlDbType.NVarChar, 200, "PC用販売説明文");
                //cmd.Parameters.Add("@Image_URL", SqlDbType.NVarChar, 200, "商品画像URL");
                //cmd.Parameters.Add("@Animation", SqlDbType.NVarChar, 200, "動画");
                //cmd.Parameters.Add("@Period", SqlDbType.NVarChar, 200, "販売期間指定");
                //cmd.Parameters.Add("@Acceptances_No", SqlDbType.NVarChar, 200, "注文受付数");
                //cmd.Parameters.Add("@Stock_Type", SqlDbType.NVarChar, 200, "在庫タイプ");
                //cmd.Parameters.Add("@Stock_Quantity", SqlDbType.NVarChar, 200, "在庫数");
                //cmd.Parameters.Add("@Stock_No_Display", SqlDbType.NVarChar, 200, "在庫数表示");
                //cmd.Parameters.Add("@Horizontal_ItemName", SqlDbType.NVarChar, 200, "項目選択肢別在庫用横軸項目名");
                //cmd.Parameters.Add("@Vertical_ItemName", SqlDbType.NVarChar, 200, "項目選択肢別在庫用縦軸項目名");
                //cmd.Parameters.Add("@Remaining_Stock", SqlDbType.NVarChar, 200, "項目選択肢別在庫用残り表示閾値");
                //cmd.Parameters.Add("@RAC_No", SqlDbType.NVarChar, 200, "RAC番号");
                //cmd.Parameters.Add("@Search_Hide", SqlDbType.NVarChar, 200, "サーチ非表示");
                //cmd.Parameters.Add("@BlackMarket_Password", SqlDbType.NVarChar, 200, "闇市パスワード");
                //cmd.Parameters.Add("@Catalog_ID", SqlDbType.NVarChar, 200, "カタログID");
                //cmd.Parameters.Add("@Flagback_Stock", SqlDbType.NVarChar, 200, "在庫戻しフラグ");
                //cmd.Parameters.Add("@Order_Reception", SqlDbType.NVarChar, 200, "在庫切れ時の注文受付");
                //cmd.Parameters.Add("@Delivery_Ctrl_No", SqlDbType.NVarChar, 200, "在庫あり時納期管理番号");
                //cmd.Parameters.Add("@Delivery_CtrlNo_Outof_Stock", SqlDbType.NVarChar, 200, "在庫切れ時納期管理番号");
                //cmd.Parameters.Add("@Rakuten_MagnificationID", SqlDbType.NVarChar, 200, "ポイント変倍率");
                //cmd.Parameters.Add("@Header_Footer", SqlDbType.NVarChar, 200, "ヘッダー・フッター・レフトナビ");
                //cmd.Parameters.Add("@Display_Order", SqlDbType.NVarChar, 200, "表示項目の並び順");
                //cmd.Parameters.Add("@Common_Description1", SqlDbType.NVarChar, 200, "共通説明文（小）");
                //cmd.Parameters.Add("@Featured_Item", SqlDbType.NVarChar, 200, "目玉商品");
                //cmd.Parameters.Add("@Common_Description2", SqlDbType.NVarChar, 200, "共通説明文（大）");
                //cmd.Parameters.Add("@Review_Text", SqlDbType.NVarChar, 200, "レビュー本文表示");
                //cmd.Parameters.Add("@Campaign_TypeID", SqlDbType.NVarChar, 200, "あす楽配送管理番号");
                //cmd.Parameters.Add("@Size_Chartlink", SqlDbType.NVarChar, 200, "サイズ表リンク");
                //cmd.Parameters.Add("@DualPricing_Ctrl_No", SqlDbType.NVarChar, 200, "二重価格文言管理番号");
                //sda.InsertCommand = cmd;
                //sda.UpdateCommand = cmd;
                //cmd.Connection.Open();
                //sda.Update(dt);
                //cmd.Connection.Close();
                //return true;

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Item_Rakuten_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExhibitionDeleteData(int eid)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Item_Delete_Rakuten_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@Exhibit_ID", eid);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExhibitionSelectFile(string xml, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Select_Rakuten_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExhibitionCategoryFile(string xml, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Category_Rakuten_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SaveExhibitionItemFileYahoo(string xml, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Item_Yahoo_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExhibitionSelectFileYahoo(string xml, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Select_Yahoo_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SaveExhibitionItemFilePonpare(string xml, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Item_Ponpare_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExhibitionSelectFilePonpare(string xml, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Select_Ponpare_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExhibitionCategoryFilePonpare(string xml, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Category_Ponpare_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SaveExhibitionItemFileJisha(string xml, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Item_Jisha_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExhibitionSelectFileJisha(string xml, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Select_Jisha_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExhibitionCategoryFileJisha(string xml, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Log_Exhibition_Category_Jisha_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeDailyDeliveryFlag(string xml)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_ChangeFlag_DailyDelivery", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@Xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable SelectLogExhibitionRakutenData(int eid, int sid, string option)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectForRakuten", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", eid);
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", sid);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
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

        public DataTable SelectLogExhibitionPonpareData(int eid, int sid, string option)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectForPonpare", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", eid);
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", sid);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
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

        public DataTable SelectLogExhibitionYahooData(int eid, int sid, string option)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectForYahoo", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", eid);
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", sid);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
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

        public DataTable SelectLogExhibitionJishaData(int eid, int sid, string option)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectForJisha", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", eid);
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", sid);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
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
