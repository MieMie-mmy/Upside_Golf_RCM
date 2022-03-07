/*
Created By              : kyaw thet paing
Created Date          : 20/06/2014
Updated By             :
Updated Date         :

Tables using:
-
-
-
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ORS_RCM_DL
{
    public class Import_Shop_ItemDL
    {
        //SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
        //-----------------------------------------------------------------------------------------------------
        public Import_Shop_ItemDL()
        { }

        //-----------------------------------------------------------------------------------------------------
        public DataTable Getftp(int mallid)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Shop where Mall_ID =" + mallid, con);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------------------------------------------------------------------------------------
        public void DeleteRakutenData(String shopID,DateTime dt)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("DELETE FROM  Import_ShopItem_Master WHERE Created_Date  < CONVERT(DateTime, '" + dt + "')   AND Shop_ID=" + shopID, con);
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //-----------------------------------------------------------------------------------------------------
        public void InsertRakutenItem(string xml)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Rakutan_Item_Backup_Insert_XML";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

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

        public DataTable GetMallByShopID(String shopID)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SELECT Mall_ID FROM Shop WHERE ID=" + shopID, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------------------------------------------------------------------------------------
        public void DeleteRakutenBackup(int shopid)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Rakutan_Item_Backup_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@shopID", shopid);
                cmd.CommandTimeout = 0;
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

        //-----------------------------------------------------------------------------------------------------
        public DataTable checkShop(String shopid)
        {
            try
            {
                DataTable dt = new DataTable();
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Rakutan_Item_SelectAll ", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@shopID", shopid);
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAllShop()
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Shop_GetAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------------------------------------------------------------------------------------------- 
        public DataTable GetShopByMallID(String mallID)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SELECT ID,Shop_Name FROM Shop WHERE Mall_ID=" + mallID, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------------------------------------------------------------------------------------
        public void InsertCsv(String xml, String StoreProdecure)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Import_ShopItem_Inventory_Backup_Insert_XML", con);
                cmd.CommandType = CommandType.StoredProcedure;
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

        //-----------------------------------------------------------------------------------------------------
        public void DeleteInventoryBackup(int shopid)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Import_ShopItem_Inventory_Backup_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@shopID", shopid);
                cmd.CommandTimeout = 0;
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

        public void InsertRakutenData(String xml, DateTime dtime)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 0;
                cmd.CommandText = "SP_Rakutan_Item_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Parameters.AddWithValue("@CreatedDate", dtime);
                //cmd.Parameters.AddWithValue("@BatchCheckFlag", batchcheckflag);
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

        public static String DataTableToXML(DataTable dtdata)
        {
            dtdata.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dtdata.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            return result;
        }

        public void InsertInventoryData(DataTable dt, String shopid,DateTime dtime)
        {
            DataColumn col = new DataColumn("shopid", typeof(String));
            col.DefaultValue = shopid;
            dt.Columns.Add(col);

            if (dt.Columns.Contains("項目選択肢用コントロールカラム"))
                dt.Columns["項目選択肢用コントロールカラム"].ColumnName = "ctrlid";
            else if (dt.Columns.Contains("コントロールカラム"))
                dt.Columns["コントロールカラム"].ColumnName = "ctrlid";
            else dt.Columns.Add("ctrlid", typeof(String));

            if (dt.Columns.Contains("商品管理ID（商品URL）"))
                dt.Columns["商品管理ID（商品URL）"].ColumnName = "admin";
            else if (dt.Columns.Contains("商品管理番号（商品URL）"))
                dt.Columns["商品管理番号（商品URL）"].ColumnName = "admin";
            else dt.Columns.Add("admin", typeof(String));

            if (dt.Columns.Contains("選択肢タイプ"))
                dt.Columns["選択肢タイプ"].ColumnName = "look";
            else dt.Columns.Add("look", typeof(String));

            if (dt.Columns.Contains("Select/Checkbox用項目名"))
                dt.Columns["Select/Checkbox用項目名"].ColumnName = "itemname";
            else if (dt.Columns.Contains("購入オプション名"))
                dt.Columns["購入オプション名"].ColumnName = "itemname";
            else dt.Columns.Add("itemname", typeof(String));

            if (dt.Columns.Contains("Select/Checkbox用選択肢"))
                dt.Columns["Select/Checkbox用選択肢"].ColumnName = "itemchoice";
            else if (dt.Columns.Contains("オプション項目名"))
                dt.Columns["オプション項目名"].ColumnName = "itemchoice";
            else dt.Columns.Add("itemchoice", typeof(String));

            if (dt.Columns.Contains("項目選択肢別在庫用横軸選択肢"))
                dt.Columns["項目選択肢別在庫用横軸選択肢"].ColumnName = "iX";
            else if (dt.Columns.Contains("SKU横軸項目ID"))
                dt.Columns["SKU横軸項目ID"].ColumnName = "iX";
            else dt.Columns.Add("iX", typeof(String));

            if (dt.Columns.Contains("項目選択肢別在庫用横軸選択肢子番号"))
                dt.Columns["項目選択肢別在庫用横軸選択肢子番号"].ColumnName = "XNo";
            else if (dt.Columns.Contains("SKU横軸項目名"))
                dt.Columns["SKU横軸項目名"].ColumnName = "XNo";
            else dt.Columns.Add("XNo", typeof(String));

            if (dt.Columns.Contains("項目選択肢別在庫用縦軸選択肢"))
                dt.Columns["項目選択肢別在庫用縦軸選択肢"].ColumnName = "iY";
            else if (dt.Columns.Contains("SKU縦軸項目ID"))
                dt.Columns["SKU縦軸項目ID"].ColumnName = "iY";
            else dt.Columns.Add("iY", typeof(String));

            if (dt.Columns.Contains("項目選択肢別在庫用縦軸選択肢子番号"))
                dt.Columns["項目選択肢別在庫用縦軸選択肢子番号"].ColumnName = "YNo";
            else if (dt.Columns.Contains("SKU縦軸項目名"))
                dt.Columns["SKU縦軸項目名"].ColumnName = "YNo";
            else dt.Columns.Add("YNo", typeof(String));

            if (dt.Columns.Contains("項目選択肢別在庫用在庫数"))
                dt.Columns["項目選択肢別在庫用在庫数"].ColumnName = "stockno";
            else if (dt.Columns.Contains("SKU在庫数"))
                dt.Columns["SKU在庫数"].ColumnName = "stockno";
            else dt.Columns.Add("stockno", typeof(String));

            if (dt.Columns.Contains("在庫戻しフラグ"))
                dt.Columns["在庫戻しフラグ"].ColumnName = "flag";
            else dt.Columns.Add("flag", typeof(String));

            if (dt.Columns.Contains("在庫切れ時の注文受付"))
                dt.Columns["在庫切れ時の注文受付"].ColumnName = "order";
            else dt.Columns.Add("order", typeof(String));

            if (dt.Columns.Contains("在庫あり時納期管理番号"))
                dt.Columns["在庫あり時納期管理番号"].ColumnName = "instock";
            else dt.Columns.Add("instock", typeof(String));

            if (dt.Columns.Contains("在庫切れ時納期管理番号"))
                dt.Columns["在庫切れ時納期管理番号"].ColumnName = "outstock";
            else dt.Columns.Add("outstock", typeof(String));

            if (dt.Columns.Contains("項目選択肢別在庫用取り寄せ可能表示"))
                dt.Columns["項目選択肢別在庫用取り寄せ可能表示"].ColumnName = "display";
            else dt.Columns.Add("display", typeof(String));

            String xml = DataTableToXML(dt);

            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Import_ShopItem_Inventory_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@xml", xml);
                cmd.Parameters.AddWithValue("@CreatedDate", dtime);
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


        public void InsertCategoryData(DataTable dt, String shopid,DateTime dtime)
        {
            DataColumn col = new DataColumn("shopid", typeof(String));
            col.DefaultValue = shopid;
            dt.Columns.Add(col);

            if (dt.Columns.Contains("コントロールカラム"))
                dt.Columns["コントロールカラム"].ColumnName = "ctrid";
            else dt.Columns.Add("ctrid", typeof(String));

            if (dt.Columns.Contains("商品管理ID（商品URL）"))
                dt.Columns["商品管理ID（商品URL）"].ColumnName = "admin";
            else if (dt.Columns.Contains("商品管理番号（商品URL）"))
                dt.Columns["商品管理番号（商品URL）"].ColumnName = "admin";
            else dt.Columns.Add("admin", typeof(String));

            if (dt.Columns.Contains("商品名"))
                dt.Columns["商品名"].ColumnName = "itemname";
            else dt.Columns.Add("itemname", typeof(String));

            if (dt.Columns.Contains("表示先カテゴリ"))
                dt.Columns["表示先カテゴリ"].ColumnName = "cat";
            else if (dt.Columns.Contains("ショップ内カテゴリ"))
                dt.Columns["ショップ内カテゴリ"].ColumnName = "cat";
            else dt.Columns.Add("cat", typeof(String));

            if (dt.Columns.Contains("優先度"))
                dt.Columns["優先度"].ColumnName = "pri";
            else if (dt.Columns.Contains("表示順位"))
                dt.Columns["表示順位"].ColumnName = "pri";
            else dt.Columns.Add("pri", typeof(String));

            if (dt.Columns.Contains("URL"))
                dt.Columns["URL"].ColumnName = "url";
            else dt.Columns.Add("url", typeof(String));

            if (dt.Columns.Contains("1ページ複数形式"))
                dt.Columns["1ページ複数形式"].ColumnName = "page";
            else dt.Columns.Add("page", typeof(String));

            if (dt.Columns.Contains("カテゴリセット管理番号"))
                dt.Columns["カテゴリセット管理番号"].ColumnName = "catno";
            else dt.Columns.Add("catno", typeof(String));

            if (dt.Columns.Contains("カテゴリセット名"))
                dt.Columns["カテゴリセット名"].ColumnName = "catname";
            else dt.Columns.Add("catname", typeof(String));

            String xml = DataTableToXML(dt);

            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Import_ShopItem_Category_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@xml", xml);
                cmd.Parameters.AddWithValue("@CreatedDate", dtime);
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

        //-----------------------------------------------------------------------------------------------------
        public DataTable checkInventory(String shopid)
        {
            DataTable dt = new DataTable();
            String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SP_Import_ShopItem_Inventory_SelectAll ", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@shopID", shopid);
            da.SelectCommand.Connection.Open();
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            da.Dispose();
            return dt;

        }

        //-----------------------------------------------------------------------------------------------------
        public void InsertInventory(string xml)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Import_ShopItem_Inventory_Backup_Insert_XML";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
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

        //-----------------------------------------------------------------------------------------------------
        public void DeleteInventoryData(String shopID,DateTime dt)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("DELETE FROM  Import_ShopItem_Inventory WHERE Created_Date  < CONVERT(DateTime, '" + dt + "')   AND Shop_ID=" + shopID, con);
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //-----------------------------------------------------------------------------------------------------
        public void DeleteCategoryData(String shopID,DateTime dt)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("DELETE FROM  Import_ShopItem_Category WHERE Created_Date  < CONVERT(DateTime, '"+ dt +"')   AND Shop_ID=" + shopID, con);
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //-----------------------------------------------------------------------------------------------------
        public void InsertShopCategory(string xml)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Import_ShopItem_Category_Backup_XML";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //-----------------------------------------------------------------------------------------------------
        public void DeleteCategoryBackup(int shopid)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Import_ShopItem_Category_Backup_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@shopID", shopid);
                cmd.CommandTimeout = 0;
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

        //-----------------------------------------------------------------------------------------------------
        public DataTable checkCategory(String shopid)
        {
            try
            {
                DataTable dt = new DataTable();
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Import_ShopItem_Category_SelectAll ", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@shopID", shopid);
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}


