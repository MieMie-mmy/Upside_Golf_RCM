using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
  public   class Import_Item_Data_DL
    {
      string itemid;
      
      public Import_Item_Data_DL() { }

      public  string  SaleDescInsert(DataTable dt) 
      {
          try
          {
              for (int i = 0; i < dt.Rows.Count;i++ )
              {
                  SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                  SqlDataAdapter da = new SqlDataAdapter("SP_Import_Item_Data_Insert", connectionString);
                  da.SelectCommand.CommandType = CommandType.StoredProcedure;
                  da.SelectCommand.CommandTimeout = 0;
                  
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["PC用販売説明文"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@saledesc", dt.Rows[i]["PC用販売説明文"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@saledesc", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["製品コード"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Product_Code", dt.Rows[i]["製品コード"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Product_Code", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["商品番号"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Item_Code", dt.Rows[i]["商品番号"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Item_Code", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["商品名"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Item_Name", dt.Rows[i]["商品名"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Item_Name", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["定価"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@List_Price",dt.Rows[i]["定価"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@List_Price", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["販売価格"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Sale_Price", dt.Rows[i]["販売価格"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Sale_Price", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["原価"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Cost",dt.Rows[i]["原価"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Cost", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["発売日"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Release_Date", dt.Rows[i]["発売日"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Release_Date", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["掲載可能日"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Post_Available_Date", dt.Rows[i]["掲載可能日"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Post_Available_Date", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["年度"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Year", dt.Rows[i]["年度"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Year", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["シーズン"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Season", dt.Rows[i]["シーズン"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Season", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["ブランド名"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Brand_Name", dt.Rows[i]["ブランド名"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Brand_Name", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["ブランドコード"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Brand_Code", dt.Rows[i]["ブランドコード"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Brand_Code", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["ヤフースペック値"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Brand_Code_Yahoo", dt.Rows[i]["ヤフースペック値"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Brand_Code_Yahoo", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["競技名"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Competition_Name", dt.Rows[i]["競技名"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Competition_Name", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["分類名"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Class_Name", dt.Rows[i]["分類名"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Class_Name", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["カタログ情報"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Catalog_Information", dt.Rows[i]["カタログ情報"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Catalog_Information", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["特記フラグ"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@special_flag", dt.Rows[i]["特記フラグ"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@special_flag", null); }


                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["商品情報"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Merchandise_Information", dt.Rows[i]["商品情報"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Merchandise_Information", null); }
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["PC用商品説明文"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Item_Description_PC", dt.Rows[i]["PC用商品説明文"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Item_Description_PC", null); }
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["モバイル用商品説明文"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Item_Description_Mobile", dt.Rows[i]["モバイル用商品説明文"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Item_Description_Mobile", null); }
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["スマートフォン用商品説明文"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Item_Description_Phone", dt.Rows[i]["スマートフォン用商品説明文"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Item_Description_Phone", null); }
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["スマートテンプレート"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Smart_Template", dt.Rows[i]["スマートテンプレート"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Smart_Template", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["楽天カテゴリID"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Rakuten_CategoryID", dt.Rows[i]["楽天カテゴリID"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Rakuten_CategoryID", null); }
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["ヤフーカテゴリID"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Yahoo_CategoryID", dt.Rows[i]["ヤフーカテゴリID"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Yahoo_CategoryID", null); }
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["ポンパレカテゴリID"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Ponpare_CategoryID", dt.Rows[i]["ポンパレカテゴリID"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Ponpare_CategoryID", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["フリースペース1"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Additional_1", dt.Rows[i]["フリースペース1"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Additional_1", null); }
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["フリースペース2"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Additional_2", dt.Rows[i]["フリースペース2"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Additional_2", null); }
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["フリースペース3"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Additional_3", dt.Rows[i]["フリースペース3"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Additional_3", null); }
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["送料"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Postage", dt.Rows[i]["送料"].ToString()); }
                  else { da.SelectCommand.Parameters.AddWithValue("@Postage", null); }

                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["個別送料"].ToString()))
                  { da.SelectCommand.Parameters.AddWithValue("@Extra_Shipping", dt.Rows[i]["個別送料"].ToString()); }
                    else { da.SelectCommand.Parameters.AddWithValue("@Extra_Shipping", null); }

                   if (!String.IsNullOrWhiteSpace(dt.Rows[i]["代引料"].ToString()))
                   { da.SelectCommand.Parameters.AddWithValue("@Delivery_Charges", dt.Rows[i]["代引料"].ToString()); }
                   else { da.SelectCommand.Parameters.AddWithValue("@Delivery_Charges", null); }

                   if (!String.IsNullOrWhiteSpace(dt.Rows[i]["倉庫指定"].ToString()))
                   { da.SelectCommand.Parameters.AddWithValue("@Warehouse_Specified", dt.Rows[i]["倉庫指定"].ToString()); }
                   else { da.SelectCommand.Parameters.AddWithValue("@Warehouse_Specified", null); }

                   if (!String.IsNullOrWhiteSpace(dt.Rows[i]["闇市パスワード"].ToString()))
                   { da.SelectCommand.Parameters.AddWithValue("@BlackMarket_Password", dt.Rows[i]["闇市パスワード"].ToString()); }
                   else { da.SelectCommand.Parameters.AddWithValue("@BlackMarket_Password", null); }

                   //if (!String.IsNullOrWhiteSpace(dt.Rows[i]["SHOP"].ToString()))
                   //{ da.SelectCommand.Parameters.AddWithValue("@ctrl_ID", dt.Rows[i]["SHOP"].ToString()); }
                   //else { da.SelectCommand.Parameters.AddWithValue("@ctrl_ID", null); }

                   if (!String.IsNullOrWhiteSpace(dt.Rows[i]["仕入先名"].ToString()))
                   { da.SelectCommand.Parameters.AddWithValue("@company_name", dt.Rows[i]["仕入先名"].ToString()); }
                   else { da.SelectCommand.Parameters.AddWithValue("@company_name", null); }

                   da.SelectCommand.Parameters.AddWithValue("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                   da.SelectCommand.Connection.Open();
                   da.SelectCommand.ExecuteNonQuery();
                   da.SelectCommand.Connection.Close();
                   string str = da.SelectCommand.Parameters["@result"].Value.ToString();
                   itemid += str + ',';
              }

              return itemid;
          }
          catch (Exception ex)
          {
              throw ex;
          }

      }

      public  DataTable SelectItemID(string itemcode) 
      {
          try {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              DataTable dt = new DataTable();
              SqlDataAdapter da = new SqlDataAdapter("SP_Item_Import_Data_SelectID", connectionString);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@itemcode", itemcode);
              
              da.SelectCommand.Connection.Open();
            
              da.SelectCommand.ExecuteNonQuery();
              da.Fill(dt);
              da.SelectCommand.Connection.Close();

              return dt;
          }
          catch (Exception ex) { throw ex; }
      
      }              

      public  void ImageInsert(DataTable dt,int count)
      {
          
          try 
          {

              for(int i=0;i<dt.Rows.Count;i++)
              {
                  if (dt.Rows[i]["Item_ID"].ToString() != "0")
                  {
                      for (int j = 0; j < count; j++)
                      {
                          SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                          SqlCommand cmd = new SqlCommand("SP_Import_Item_Data_Image_Insert", connectionString);
                          cmd.CommandType = CommandType.StoredProcedure;
                          cmd.CommandTimeout = 0;
                          cmd.Parameters.AddWithValue("@itemid", dt.Rows[i]["Item_ID"].ToString());

                          cmd.Parameters.AddWithValue("@SN", j + 1);

                          cmd.Parameters.AddWithValue("@imgname", dt.Rows[i]["ライブラリ画像" + (j + 1)].ToString());

                          cmd.Parameters.AddWithValue("@imgtype", 1);
                          cmd.Connection.Open();
                          cmd.ExecuteNonQuery();
                          cmd.Connection.Close();
                      }
                  }
              }
          }
          catch (Exception ex) 
          {
              throw ex;
          }
      }

      public void ItemImportXmlInsert(string xml)
      {
          SqlConnection connection = new SqlConnection(DataConfig.connectionString);
          SqlCommand cmd = new SqlCommand("SP_Import_Item_Data_XML", connection);
          try
          {
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
              cmd.Connection.Open();
              cmd.ExecuteNonQuery();


          }
          catch (Exception ex)
          {

              throw ex;
          }
          finally
          {
              cmd.Connection.Close();
              cmd.Dispose();
          }
      }

      public DataTable SelectLogData(int id) 
      {
          try 
          {
              SqlConnection con = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter("SP_SelectImportdataLog", con);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@logid", id);
              DataTable dt = new DataTable();
              da.SelectCommand.Connection.Open();
              
              da.SelectCommand.ExecuteNonQuery();
              da.Fill(dt);
              da.SelectCommand.Connection.Close();

              return dt;
          
          }
          catch (Exception ex) { throw ex; }
      
      }

      public  DataTable CheckItemCode(string Itemcode) 
      {
          try 
          {
              DataTable dt = new DataTable();
              SqlConnection con = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter("SP_Check_ItemCode", con);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@Item_Code", Itemcode);
              da.SelectCommand.Connection.Open();
              da.Fill(dt);
              da.SelectCommand.Connection.Close();
              return dt;
          }
          catch (Exception ex) { throw ex; }
      }

      public void InsertImageData(DataTable dt) 
      {
          try
          {

              for (int i = 0; i < dt.Rows.Count; i++)
              {
                  if (dt.Rows[i]["Item_ID"].ToString() != "0")
                  {
                     
                          SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                          SqlCommand cmd = new SqlCommand("SP_Import_Item_Data_Image_Insert", connectionString);
                          cmd.CommandType = CommandType.StoredProcedure;
                          cmd.CommandTimeout = 0;
                          cmd.Parameters.AddWithValue("@itemid", dt.Rows[i]["Item_ID"].ToString());

                          cmd.Parameters.AddWithValue("@SN", dt.Rows[i]["Count"].ToString());

                          cmd.Parameters.AddWithValue("@imgname", dt.Rows[i]["画像名"].ToString());

                          cmd.Parameters.AddWithValue("@imgtype", 0);
                          cmd.Connection.Open();
                          cmd.ExecuteNonQuery();
                          cmd.Connection.Close();
                     
                  }
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }


      public void Item_Import_Image(string xml)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "SP_Import_Item_Image_XML ";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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
      public void RakutenItem_Import_Image(string xml)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "SP_RakutenPath_XML ";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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
      public void Item_Import_LibraryImage(string xml)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "SP_Import_RelateItemImage_XML";
             // cmd.CommandText = "SP_MigrationData_XML";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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

      public void LatestdataImage(string xml)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "SP_Import_ItemData_DataImageXML";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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

      public void Item_Shop_Import(string xml)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "SP_ItemShopImport_XML";
              // cmd.CommandText = "SP_MigrationData_XML";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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

      public DataTable SmartSelectLogData(int id)
      {
          try
          {
              SqlConnection con = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter("SP_SelectAll_SmarttemplateLog", con);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@logid", id);
              DataTable dt = new DataTable();
              da.SelectCommand.Connection.Open();

              da.SelectCommand.ExecuteNonQuery();
              da.Fill(dt);
              da.SelectCommand.Connection.Close();

              return dt;

          }
          catch (Exception ex) { throw ex; }

      }
      public void Import_LibraryImage_Only(string xml)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "SP_ImportLibrary_Image_XML";
            
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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
      public void Import_RelateItem_Only(string xml)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "SP_ImportRelated_Item_XML";

              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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

      public DataTable ImageSelectLogData(int id)
      {
          try
          {
              SqlConnection con = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter("SP_ItemImageLog_SelectAll", con);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@ID", id);
              DataTable dt = new DataTable();
              da.SelectCommand.Connection.Open();

              da.SelectCommand.ExecuteNonQuery();
              da.Fill(dt);
              da.SelectCommand.Connection.Close();

              return dt;

          }
          catch (Exception ex) { throw ex; }

      }
      public DataTable ImageSelectErrorLogData(int id)//for error log
      {
          try
          {
              SqlConnection con = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter("SP_ItemImageErrorLog_SelectAll", con);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@ID", id);
              DataTable dt = new DataTable();
              da.SelectCommand.Connection.Open();

              da.SelectCommand.ExecuteNonQuery();
              da.Fill(dt);
              da.SelectCommand.Connection.Close();

              return dt;

          }
          catch (Exception ex) { throw ex; }

      }
      public void Import_RelateItem_Onlywithctrl(string xml)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "SP_Relateitemimport_CtrlXML";

              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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
      public void Import_LibraryImage_Onlywithctrl(string xml)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "SP_LibraryImageImport_Ctrl_XML";

              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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

      public void Item_Shop_Importwithctrl(string xml)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "SP_ItemShopImportwithCtrl_XML";
              // cmd.CommandText = "SP_MigrationData_XML";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
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

      public DataTable LibraryImageLogData(int id)
      {
          try
          {
              SqlConnection con = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter("SP_LibraryImageLog_SelectAll", con);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@ID", id);
              DataTable dt = new DataTable();
              da.SelectCommand.Connection.Open();

              da.SelectCommand.ExecuteNonQuery();
              da.Fill(dt);
              da.SelectCommand.Connection.Close();

              return dt;

          }
          catch (Exception ex) { throw ex; }

      }
      public DataTable LibraryImageLogErrorData(int id)//for errorlog
      {
          try
          {
              SqlConnection con = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter("SP_LibraryImageErrorLog_SelectAll", con);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@ID", id);
              DataTable dt = new DataTable();
              da.SelectCommand.Connection.Open();

              da.SelectCommand.ExecuteNonQuery();
              da.Fill(dt);
              da.SelectCommand.Connection.Close();

              return dt;

          }
          catch (Exception ex) { throw ex; }

      }
    }
}
