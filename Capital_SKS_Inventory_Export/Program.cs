/*
    Using table
 * Item 
 * Shop
 * Item_Master
 * 
 *      
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Globalization;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Net.Mail;
using Limilabs.FTP.Client;
using CKSKS_BL;
using CKSKS_Common;

namespace Capital_SKS_Inventory_Export
{
    public class Program
    {
        public static String fileName = String.Empty;
        public static String errLogPath = @"C:\Inventory Update\CSV_Upload\Error_Logs\";
        //public static string ftpfullpath = "ftp://test.capitalk-mm.com/inventory/";
        //public static string testFtpUserID = string.Empty;
        //public static string testFtpPassword = string.Empty;
        //public static string ftpPonparePath = "ftp://ftp.ponparemall.com";
        //public static string ponpareFtpUserID = string.Empty;
        //public static string ponpareFtpPassword = string.Empty;
        //public static string ftpAmazonPath = string.Empty;
        //public static string amazonFtpUserID = string.Empty;
        //public static string amazonFtpPassword = string.Empty;
        //public static string ftpRakutenPath = "";
        //public static string rakutenFtpUserID = "";
        //public static string rakutenFtpPassword = "";
        static string ExportInventoryCSVPath = ConfigurationManager.AppSettings["ExportInventoryCSVPath"].ToString();
        static string BakExportInventoryCSVPath = ConfigurationManager.AppSettings["BakExportInventoryCSVPath"].ToString();
        static string AmazonPath = ConfigurationManager.AppSettings["AmazonPath"].ToString();
        static String ConsoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        
        
        
        static void Main(string[] args)
        {
            try
            {
                //string groupno = null;
                //String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                //string filename = date + ".csv";

              // DataTable dtShop = GetShop();
                Export();
                #region
                //string list = GetItem_IDList();// 27-05-2015

                ////DataTable dtShop = GetInventoryShop(list);// added by KTA 28/05/2015
                //if (dtShop != null && dtShop.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtShop.Rows.Count; i++)
                //    {
                //        int shop_id = int.Parse(dtShop.Rows[i]["ID"].ToString());

                //        switch (dtShop.Rows[i]["Mall_ID"].ToString())  //Check Mall
                //        {
                //            case "1":
                //                DataTable dtRakuten = GetData("Rakuten", shop_id, list);//27-05-2015
                //                if (dtRakuten.Rows.Count > 0)
                //                {
                //                    using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                //                    {
                //                        WriteDataTable(dtRakuten, writer, true);
                //                        SaveItem_ExportQ("select$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
                //                    }
                //                    using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                //                    {
                //                        WriteDataTable(dtRakuten, writer, true);
                //                    }
                //                }
                //                break;
                //            case "2":
                //                DataTable dtYahoo = GetData("Yahoo", shop_id, list);  //27-05-2015
                //                if (dtYahoo.Rows.Count > 0)
                //                {
                //                    using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Yahoo/" + "quantity$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                //                    {
                //                        WriteDataTable(dtYahoo, writer, true);
                //                        SaveItem_ExportQ("quantity$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
                //                    }
                //                    using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Yahoo/" + "quantity$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                //                    {
                //                        WriteDataTable(dtYahoo, writer, true);
                //                    }
                //                }
                //                break;
                //            case "3":
                //                DataTable dtPonpare = GetData("Ponpare", shop_id, list); //27-05-2015
                //                if (dtPonpare.Rows.Count > 0)
                //                {
                //                    using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Ponpare/" + "option$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                //                    {
                //                        WriteDataTable(dtPonpare, writer, true);
                //                        SaveItem_ExportQ("option$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
                //                    }
                //                    using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Ponpare/" + "option$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                //                    {
                //                        WriteDataTable(dtPonpare, writer, true);
                //                    }
                //                }
                //                break;
                //            case "4":
                //                DataTable dtAmazon = GetData("Amazon", shop_id, list);   //27-05-2015
                //                if (dtAmazon.Rows.Count > 0)
                //                {
                //                    using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Amazon/" + "sku$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                //                    {
                //                        WriteDataTable(dtAmazon, writer, true);
                //                        SaveItem_ExportQ("sku$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
                //                    }
                //                    using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Amazon/" + "sku$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                //                    {
                //                        WriteDataTable(dtAmazon, writer, true);
                //                    }
                //                }
                //                break;
                //            case "5":
                //                DataTable dtJisha = GetData("Jisha", shop_id, list); //27-05-2015
                //                if (dtJisha.Rows.Count > 0)
                //                {
                //                    using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Jisha/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                //                    {
                //                        WriteDataTable(dtJisha, writer, true);
                //                        SaveItem_ExportQ("select$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
                //                    }
                //                    using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Jisha/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                //                    {
                //                        WriteDataTable(dtJisha, writer, true);
                //                    }
                //                }
                //                break;
                //        }
                //    }
                //}
                //ChangeFlag(list); //27-05-2015
                #endregion
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

        //public static string GetGroupNo()
        //{
        //    try
        //    {
        //        Item_Master_BL master = new Item_Master_BL();
        //        return master.GetGroupNo();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static DataTable GetData(string option, int shop_id)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        SqlConnection con = GetConnection();
        //        using (SqlDataAdapter da = new SqlDataAdapter("SP_Item_SelectNotUpload", con))
        //        {
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            da.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
        //            da.SelectCommand.Parameters.AddWithValue("@Option", option);
        //            con.Open();                    
        //            da.Fill(dt);
        //            con.Close();
        //        }
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static void Export() 
        {
         
            try
            {
              
                //string groupno = null;
               
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string filename = date + ".csv";
                string amazonfilename = date + ".txt";//20/10/2015

                DataTable dtToChangeFlag = GetQuickShipping();
                string list = GetItem_IDList();// 27-05-2015
                DataTable dtAll = GetInvData("Select", list);

                string item_list = GetItem_Master_IDLists();
                DataTable dtItem = GetItemData(item_list); //for Rakuten and Ponpare Mall
                DataTable dtItemYahoo = GetItemDataYahoo(item_list); //for Yahoo Mall

                DataTable dtShop = GetShop();

                if (dtShop != null && dtShop.Rows.Count > 0)
                {
                    for (int i = 0; i < dtShop.Rows.Count; i++)
                    {
                        int shop_id = int.Parse(dtShop.Rows[i]["ID"].ToString());

                        switch (dtShop.Rows[i]["Mall_ID"].ToString())  //Check Mall
                        {
                            case "1":
                                string groupno = GetGroupNo();
                                string itemCodeDailyDeliverySet = "";
                                #region generate item.csv
                                DataRow[] drRakuten = dtItem.Select("ID =" + shop_id);
                                if (drRakuten.Count() > 0)
                                 {
                                     DataTable dt_item = null;
                                     DataTable dt_select = null;
                                     DataTable dt_cat = null;

                                     dt_item = dtItem.Select("ID =" + shop_id).CopyToDataTable();
                                     dt_select = dtItem.Select("ID =" + shop_id).CopyToDataTable();
                                     dt_cat = dtItem.Select("ID =" + shop_id).CopyToDataTable();


                                     #region ConsoleWriteLineTofile(Rakuten)

                                     foreach (DataRow itemCodeDDelivery in dt_item.Rows)
                                     {
                                         
                                         itemCodeDailyDeliverySet+=itemCodeDDelivery["商品番号"].ToString()+",";
                                     }

                                     ConsoleWriteLine_Tofile("Item Code : " + itemCodeDailyDeliverySet, shop_id);
                                    
                                     #endregion
                                     #region Remove Column
                                     if (dt_item.Rows.Count > 0)
                                    {
                                        dt_item.Columns.Remove("ID");
                                        dt_item.Columns.Remove("Mall_ID");
                                        dt_item.Columns.Remove("項目選択肢用コントロールカラム");
                                        dt_item.Columns.Remove("選択肢タイプ");
                                        dt_item.Columns.Remove("Select/Checkbox用項目名");
                                        dt_item.Columns.Remove("Select/Checkbox用選択肢");
                                        dt_item.Columns.Remove("表示先カテゴリ");
                                        dt_item.Columns.Remove("優先度");
                                        dt_item.Columns.Remove("1ページ複数形式");
                                        dt_item.Columns.Remove("カテゴリセット管理番号");
                                        dt_item.Columns.Remove("カテゴリセット名");
                                        dt_item.Columns.Remove("商品管理ID（商品URL）");
                                        dt_item.Columns.Remove("翌日お届け設定番号");
                                        dt_item.Columns.Remove("選択肢タイプ1");
                                        dt_item.Columns.Remove("購入オプション名");
                                        dt_item.Columns.Remove("オプション項目名");
                                        dt_item.Columns.Remove("SKU横軸項目ID");
                                        dt_item.Columns.Remove("SKU横軸項目名");
                                        dt_item.Columns.Remove("SKU縦軸項目ID");
                                        dt_item.Columns.Remove("SKU縦軸項目名");
                                        dt_item.Columns.Remove("SKU在庫数");
                                        dt_item.Columns.Remove("ショップ内カテゴリ");
                                        dt_item.Columns.Remove("表示順位");
                                        dt_item.AcceptChanges();
                                    }
                                     if (dt_select.Rows.Count > 0)
                                     {
                                         dt_select.Columns.Remove("ID");
                                         dt_select.Columns.Remove("Mall_ID");
                                         dt_select.Columns.Remove("コントロールカラム");
                                         dt_select.Columns.Remove("商品番号");
                                         dt_select.Columns.Remove("あす楽配送管理番号");
                                         dt_select.Columns.Remove("表示先カテゴリ");
                                         dt_select.Columns.Remove("優先度");
                                         dt_select.Columns.Remove("1ページ複数形式");
                                         dt_select.Columns.Remove("カテゴリセット管理番号");
                                         dt_select.Columns.Remove("カテゴリセット名");
                                         dt_select.Columns.Remove("商品管理ID（商品URL）");
                                         dt_select.Columns.Remove("翌日お届け設定番号");
                                         dt_select.Columns.Remove("選択肢タイプ1");
                                         dt_select.Columns.Remove("購入オプション名");
                                         dt_select.Columns.Remove("オプション項目名");
                                         dt_select.Columns.Remove("SKU横軸項目ID");
                                         dt_select.Columns.Remove("SKU横軸項目名");
                                         dt_select.Columns.Remove("SKU縦軸項目ID");
                                         dt_select.Columns.Remove("SKU縦軸項目名");
                                         dt_select.Columns.Remove("SKU在庫数");
                                         dt_select.Columns.Remove("ショップ内カテゴリ");
                                         dt_select.Columns.Remove("表示順位");
                                         dt_select.AcceptChanges();
                                     }
                                     if (dt_cat.Rows.Count > 0)
                                     {
                                         dt_cat.Columns.Remove("ID");
                                         dt_cat.Columns.Remove("Mall_ID");
                                         dt_cat.Columns.Remove("コントロールカラム");
                                         if (dt_cat.Columns.Contains("項目選択肢用コントロールカラム"))
                                         {
                                             dt_cat.Columns["項目選択肢用コントロールカラム"].ColumnName = "コントロールカラム";
                                         }
                                         dt_cat.Columns.Remove("商品番号");
                                         dt_cat.Columns.Remove("あす楽配送管理番号");
                                         dt_cat.Columns.Remove("選択肢タイプ");
                                         dt_cat.Columns.Remove("Select/Checkbox用項目名");
                                         dt_cat.Columns.Remove("Select/Checkbox用選択肢");
                                         dt_cat.Columns.Remove("商品管理ID（商品URL）");
                                         dt_cat.Columns.Remove("翌日お届け設定番号");
                                         dt_cat.Columns.Remove("選択肢タイプ1");
                                         dt_cat.Columns.Remove("購入オプション名");
                                         dt_cat.Columns.Remove("オプション項目名");
                                         dt_cat.Columns.Remove("SKU横軸項目ID");
                                         dt_cat.Columns.Remove("SKU横軸項目名");
                                         dt_cat.Columns.Remove("SKU縦軸項目ID");
                                         dt_cat.Columns.Remove("SKU縦軸項目名");
                                         dt_cat.Columns.Remove("SKU在庫数");
                                         dt_cat.Columns.Remove("ショップ内カテゴリ");
                                         dt_cat.Columns.Remove("表示順位");
                                         dt_cat.Columns.Remove("優先度");
                                         dt_cat.AcceptChanges();
                                     }
                                     #endregion
                                     #region File
                                     if (dt_item.Rows.Count > 0)
                                     {
                                         using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Rakuten/" + "item$" + shop_id + "_" + date + "_" + groupno + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_item, writer, true);
                                             SaveItem_ExportQ("item$" + shop_id + "_" + date + "_" + groupno + ".csv", 2, shop_id, 0, 3);
                                         }
                                         using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Rakuten/" + "item$" + shop_id + "_" + date + "_" + groupno + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_item, writer, true);
                                         }
                                     }
                                     if (dt_select.Rows.Count > 0)
                                     {
                                         using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + date + "_" + groupno + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_select, writer, true);
                                             SaveItem_ExportQ("select$" + shop_id + "_" + date + "_" + groupno + ".csv", 2, shop_id, 0, 3);
                                         }
                                         using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + date + "_" + groupno + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_select, writer, true);
                                         }
                                     }
                                     if (dt_cat.Rows.Count > 0)
                                     {
                                         using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Rakuten/" + "item-cat$" + shop_id + "_" + date + "_" + groupno + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_cat, writer, true);
                                             SaveItem_ExportQ("item-cat$" + shop_id + "_" + date + "_" + groupno + ".csv", 2, shop_id, 0, 3);
                                         }
                                         using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Rakuten/" + "item-cat$" + shop_id + "_" + date + "_" + groupno + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_cat, writer, true);
                                         }
                                     }
                                     #endregion
                                 }
                                #endregion
                                #region generate select.csv
                                DataRow[] dr = dtAll.Select("ID =" + shop_id);
                                if(dr.Count() >0)
                                {
                                    DataTable dtRakutens = null;
                                    DataTable dtrd = null;
                                    DataTable dtRakuten = null;
                                     dtRakutens = dtAll.Select("ID =" + shop_id).CopyToDataTable();
                                    dtrd = dtRakutens.Select("Mall_ID =1").CopyToDataTable();
                                    dtRakuten = RColumname(dtrd);
                                
                                if (dtRakuten.Rows.Count > 0)
                                {
                                    using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_1_" + date + "_" + groupno + ".csv", false, Encoding.GetEncoding(932)))
                                    {
                                        WriteDataTable(dtRakuten, writer, true);
                                        SaveItem_ExportQ("select$" + shop_id + "_1_" + date + "_" + groupno + ".csv", 2, shop_id, 0, 3);
                                    }
                                    using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_1_" + date + "_" + groupno + ".csv", false, Encoding.GetEncoding(932)))
                                    {
                                        WriteDataTable(dtRakuten, writer, true);
                                    }
                                }
                                }
                                #endregion 
                                break;
                            case "2":
                                #region generate item.csv
                                string itemCodeYDailyDeliverySet = "";
                                DataRow[] drYahoo = dtItemYahoo.Select("ID =" + shop_id);
                                if (drYahoo.Count() > 0)
                                 {
                                     DataTable dt_item = null;

                                     dt_item = dtItemYahoo.Select("ID =" + shop_id).CopyToDataTable();

                                     #region ConsoleWriteLineTofile

                                     foreach (DataRow itemCodeDDelivery in dt_item.Rows)
                                     {
                                        
                                         itemCodeYDailyDeliverySet +=itemCodeDDelivery["code"].ToString()+"("+itemCodeDDelivery["sub-code"].ToString()+")"+",";

                                     }

                                     ConsoleWriteLine_Tofile("Item Code : " + itemCodeYDailyDeliverySet, shop_id);

                                     #endregion

                                     #region Remove Column
                                     if (dt_item.Rows.Count > 0)
                                    {
                                        dt_item.Columns.Remove("ID");
                                        dt_item.Columns.Remove("Mall_ID");
                                        dt_item.AcceptChanges();
                                        Export_CSV3 export = new Export_CSV3();
                                        dt_item = export.ModifyTable(dt_item, shop_id);
                                    }
                                     #endregion
                                     #region File
                                     if (dt_item.Rows.Count > 0)
                                     {
                                         using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Yahoo/" + "data_add$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_item, writer, true);
                                             SaveItem_ExportQ("data_add$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
                                         }
                                         using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Yahoo/" + "data_add$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_item, writer, true);
                                         }
                                     }
                                     #endregion
                                 }
                                #endregion
                                #region select.csv
                                DataRow[] drs = dtAll.Select("ID =" + shop_id);
                                  if (drs.Count() > 0)
                                  {
                                      DataTable dtys = null;
                                      DataTable dty = null;
                                      DataTable dtYahoo = null;

                                      dtys = dtAll.Select("ID =" + shop_id).CopyToDataTable();
                                       dty = dtys.Select("Mall_ID =2").CopyToDataTable();
                                      dtYahoo = YColumname(dty);
                                    
                                      if (dtYahoo.Rows.Count > 0)
                                      {
                                          using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Yahoo/" + "quantity$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                                          {
                                              WriteDataTable(dtYahoo, writer, true);
                                              SaveItem_ExportQ("quantity$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
                                          }
                                          using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Yahoo/" + "quantity$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
                                          {
                                              WriteDataTable(dtYahoo, writer, true);
                                          }
                                      }
                                  }
                                #endregion
                                break;
                            case "3":
                                string itemCodePDailyDeliverySet = "";
                                string groupno3 = GetGroupNo();
                                #region generate item.csv
                                DataRow[] drponpare = dtItem.Select("ID =" + shop_id);
                                if (drponpare.Count() > 0)
                                 {
                                     DataTable dt_item = null;
                                     DataTable dt_select = null;
                                     DataTable dt_cat = null;

                                     dt_item = dtItem.Select("ID =" + shop_id).CopyToDataTable();
                                     dt_select = dtItem.Select("ID =" + shop_id).CopyToDataTable();
                                     dt_cat = dtItem.Select("ID =" + shop_id).CopyToDataTable();

                                     #region ConsoleWriteLineTofile

                                     foreach (DataRow itemCodeDDelivery in dt_item.Rows)
                                     {
                                         itemCodePDailyDeliverySet += itemCodeDDelivery["商品番号"].ToString() + ",";

                                     }

                                     ConsoleWriteLine_Tofile("Item Code : " + itemCodePDailyDeliverySet, shop_id);

                                     #endregion

                                     #region Remove Column
                                     if (dt_item.Rows.Count > 0)
                                     {
                                         dt_item.Columns.Remove("ID");
                                         dt_item.Columns.Remove("Mall_ID");
                                         dt_item.Columns.Remove("商品管理番号（商品URL）");
                                         dt_item.Columns.Remove("商品番号");
                                         dt_item.Columns.Remove("あす楽配送管理番号");
                                         dt_item.Columns.Remove("項目選択肢用コントロールカラム");
                                         dt_item.Columns.Remove("選択肢タイプ");
                                         dt_item.Columns.Remove("Select/Checkbox用項目名");
                                         dt_item.Columns.Remove("Select/Checkbox用選択肢");
                                         dt_item.Columns.Remove("表示先カテゴリ");
                                         dt_item.Columns.Remove("1ページ複数形式");
                                         dt_item.Columns.Remove("カテゴリセット管理番号");
                                         dt_item.Columns.Remove("カテゴリセット名");
                                         dt_item.Columns.Remove("選択肢タイプ1");
                                         dt_item.Columns.Remove("購入オプション名");
                                         dt_item.Columns.Remove("オプション項目名");
                                         dt_item.Columns.Remove("SKU横軸項目ID");
                                         dt_item.Columns.Remove("SKU横軸項目名");
                                         dt_item.Columns.Remove("SKU縦軸項目ID");
                                         dt_item.Columns.Remove("SKU縦軸項目名");
                                         dt_item.Columns.Remove("SKU在庫数");
                                         dt_item.Columns.Remove("ショップ内カテゴリ");
                                         dt_item.Columns.Remove("表示順位");
                                         dt_item.Columns.Remove("優先度");
                                         dt_item.AcceptChanges();
                                     }
                                     if (dt_select.Rows.Count > 0)
                                     {
                                         dt_select.Columns.Remove("ID");
                                         dt_select.Columns.Remove("Mall_ID");
                                         dt_select.Columns.Remove("コントロールカラム");
                                         if (dt_select.Columns.Contains("項目選択肢用コントロールカラム"))
                                         {
                                             dt_select.Columns["項目選択肢用コントロールカラム"].ColumnName = "コントロールカラム";
                                         }
                                         dt_select.Columns.Remove("商品管理番号（商品URL）");
                                         dt_select.Columns.Remove("商品番号");
                                         dt_select.Columns.Remove("あす楽配送管理番号");
                                         dt_select.Columns.Remove("Select/Checkbox用項目名");
                                         dt_select.Columns.Remove("Select/Checkbox用選択肢");
                                         dt_select.Columns.Remove("選択肢タイプ");
                                         dt_select.Columns.Remove("表示先カテゴリ");
                                         dt_select.Columns.Remove("1ページ複数形式");
                                         dt_select.Columns.Remove("カテゴリセット管理番号");
                                         dt_select.Columns.Remove("カテゴリセット名");
                                         dt_select.Columns.Remove("翌日お届け設定番号");
                                         if (dt_select.Columns.Contains("選択肢タイプ1"))
                                         {
                                             dt_select.Columns["選択肢タイプ1"].ColumnName = "選択肢タイプ";
                                         }
                                         dt_select.Columns.Remove("ショップ内カテゴリ");
                                         dt_select.Columns.Remove("表示順位");
                                         dt_select.Columns.Remove("優先度");
                                         dt_select.AcceptChanges();
                                     }
                                     if (dt_cat.Rows.Count > 0)
                                     {
                                         dt_cat.Columns.Remove("ID");
                                         dt_cat.Columns.Remove("Mall_ID");
                                         dt_cat.Columns.Remove("コントロールカラム");
                                         if (dt_cat.Columns.Contains("項目選択肢用コントロールカラム"))
                                         {
                                             dt_cat.Columns["項目選択肢用コントロールカラム"].ColumnName = "コントロールカラム";
                                         }
                                         dt_cat.Columns.Remove("商品管理番号（商品URL）");
                                         dt_cat.Columns.Remove("商品番号");
                                         dt_cat.Columns.Remove("あす楽配送管理番号");
                                         dt_cat.Columns.Remove("選択肢タイプ");
                                         dt_cat.Columns.Remove("Select/Checkbox用項目名");
                                         dt_cat.Columns.Remove("Select/Checkbox用選択肢");
                                         dt_cat.Columns.Remove("翌日お届け設定番号");
                                         dt_cat.Columns.Remove("選択肢タイプ1");
                                         dt_cat.Columns.Remove("購入オプション名");
                                         dt_cat.Columns.Remove("オプション項目名");
                                         dt_cat.Columns.Remove("SKU横軸項目ID");
                                         dt_cat.Columns.Remove("SKU横軸項目名");
                                         dt_cat.Columns.Remove("SKU縦軸項目ID");
                                         dt_cat.Columns.Remove("SKU縦軸項目名");
                                         dt_cat.Columns.Remove("SKU在庫数");
                                         dt_cat.Columns.Remove("表示先カテゴリ");
                                         dt_cat.Columns.Remove("1ページ複数形式");
                                         dt_cat.Columns.Remove("カテゴリセット管理番号");
                                         dt_cat.Columns.Remove("カテゴリセット名");
                                         dt_cat.Columns.Remove("優先度");
                                         dt_cat.AcceptChanges();
                                     }
                                     #endregion
                                     #region File
                                     if (dt_item.Rows.Count > 0)
                                     {
                                         using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Ponpare/" + "item$" + shop_id + "_" + date + "_" + groupno3 + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_item, writer, true);
                                             SaveItem_ExportQ("item$" + shop_id + "_" + date + "_" + groupno3 + ".csv", 2, shop_id, 0, 3);
                                         }
                                         using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Ponpare/" + "item$" + shop_id + "_" + date + "_" + groupno3 + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_item, writer, true);
                                         }
                                     }
                                     if (dt_select.Rows.Count > 0)
                                     {
                                         using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Ponpare/" + "option$" + shop_id + "_" + date + "_" + groupno3 + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_select, writer, true);
                                             SaveItem_ExportQ("option$" + shop_id + "_" + date + "_" + groupno3 + ".csv", 2, shop_id, 0, 3);
                                         }
                                         using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Ponpare/" + "option$" + shop_id + "_" + date + "_" + groupno3 + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_select, writer, true);
                                         }
                                     }
                                     if (dt_cat.Rows.Count > 0)
                                     {
                                         using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Ponpare/" + "category$" + shop_id + "_" + date + "_" + groupno3 + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_cat, writer, true);
                                             SaveItem_ExportQ("category$" + shop_id + "_" + date + "_" + groupno3 + ".csv", 2, shop_id, 0, 3);
                                         }
                                         using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Ponpare/" + "category$" + shop_id + "_" + date + "_" + groupno3 + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dt_cat, writer, true);
                                         }
                                     }
                                     #endregion
                                 }
                                 #endregion 
                                #region generate select.csv
                                 DataRow[] drp = dtAll.Select("ID =" + shop_id);
                                 if (drp.Count() > 0)
                                 {
                                     DataTable dtps = null;
                                     DataTable dtp = null;
                                     DataTable dtPonpare = null;

                                     dtps = dtAll.Select("ID =" + shop_id).CopyToDataTable();
                                     dtp = dtps.Select("Mall_ID =3").CopyToDataTable();
                                      dtPonpare = PColumname(dtp);
                                  
                                     if (dtPonpare.Rows.Count > 0)
                                     {
                                         using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Ponpare/" + "option$" + shop_id + "_1_" + date + "_" + groupno3 + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dtPonpare, writer, true);
                                             SaveItem_ExportQ("option$" + shop_id + "_1_" + date + "_" + groupno3 + ".csv", 2, shop_id, 0, 3);
                                         }
                                         using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Ponpare/" + "option$" + shop_id + "_1_" + date + "_" + groupno3 + ".csv", false, Encoding.GetEncoding(932)))
                                         {
                                             WriteDataTable(dtPonpare, writer, true);
                                         }
                                     }
                                 }
                                #endregion
                                break;
                            case "4":
                                #region generate item.csv

                                #endregion
                                #region generate select.csv
                                DataRow[] dra1 = dtAll.Select("ID =" + shop_id);
                                 if (dra1.Count() > 0)
                                 {
                                     DataTable dtas =null;
                                     DataTable dta = null;
                                     DataTable dtAmazon = null;

                                    dtas = dtAll.Select("ID =" + shop_id).CopyToDataTable();
                                    dta = dtas.Select("Mall_ID =4").CopyToDataTable();
                                    dtAmazon = AColumname(dta);

                                    if (dtAmazon.Rows.Count > 0)
                                    {
                                        //string path = ExportInventoryCSVPath + "/Amazon/" + "sku_update$" + shop_id + "_" + amazonfilename;
                                        string path = AmazonPath + "sku_update$" + shop_id + "_" + amazonfilename;
                                        WriteDataToFileForAdd(dtAmazon, path);
                                        SaveItem_ExportQ("sku_update$" + shop_id + "_" + amazonfilename, 2, shop_id, 0, 3);
                                       
                                        string paths = BakExportInventoryCSVPath + "/Amazon/" + "sku_update$" + shop_id + "_" + amazonfilename;
                                        
                                        WriteDataToFileForAdd(dtAmazon, paths);
                                      
                                    }
                                 }
                                #endregion
                                break;
                            case "5":
                                string itemCodeJDailyDeliverySet = "";
                                string groupno5 = GetGroupNo();
                                #region generate item.csv
                                DataRow[] drJisha = dtItem.Select("ID =" + shop_id);
                                if (drJisha.Count() > 0)
                                {
                                    DataTable dt_item = null;
                                    DataTable dt_select = null;
                                    DataTable dt_cat = null;

                                    dt_item = dtItem.Select("ID =" + shop_id).CopyToDataTable();
                                    dt_select = dtItem.Select("ID =" + shop_id).CopyToDataTable();
                                    dt_cat = dtItem.Select("ID =" + shop_id).CopyToDataTable();

                                    #region ConsoleWriteLineTofile

                                    foreach (DataRow itemCodeDDelivery in dt_item.Rows)
                                    {
                                        itemCodeJDailyDeliverySet += itemCodeDDelivery["商品番号"].ToString() + ",";

                                    }

                                    ConsoleWriteLine_Tofile("Item Code : " + itemCodeJDailyDeliverySet, shop_id);
                                    #endregion

                                    #region Remove Column
                                    if (dt_item.Rows.Count > 0)
                                    {
                                        dt_item.Columns.Remove("ID");
                                        dt_item.Columns.Remove("Mall_ID");
                                        dt_item.Columns.Remove("項目選択肢用コントロールカラム");
                                        dt_item.Columns.Remove("選択肢タイプ");
                                        dt_item.Columns.Remove("Select/Checkbox用項目名");
                                        dt_item.Columns.Remove("Select/Checkbox用選択肢");
                                        dt_item.Columns.Remove("表示先カテゴリ");
                                        dt_item.Columns.Remove("1ページ複数形式");
                                        dt_item.Columns.Remove("カテゴリセット管理番号");
                                        dt_item.Columns.Remove("カテゴリセット名");
                                        dt_item.Columns.Remove("商品管理ID（商品URL）");
                                        dt_item.Columns.Remove("翌日お届け設定番号");
                                        dt_item.Columns.Remove("選択肢タイプ1");
                                        dt_item.Columns.Remove("購入オプション名");
                                        dt_item.Columns.Remove("オプション項目名");
                                        dt_item.Columns.Remove("SKU横軸項目ID");
                                        dt_item.Columns.Remove("SKU横軸項目名");
                                        dt_item.Columns.Remove("SKU縦軸項目ID");
                                        dt_item.Columns.Remove("SKU縦軸項目名");
                                        dt_item.Columns.Remove("SKU在庫数");
                                        dt_item.Columns.Remove("ショップ内カテゴリ");
                                        dt_item.Columns.Remove("表示順位");
                                        dt_item.Columns.Remove("優先度");
                                        dt_item.AcceptChanges();
                                    }
                                    if (dt_select.Rows.Count > 0)
                                    {
                                        dt_select.Columns.Remove("ID");
                                        dt_select.Columns.Remove("Mall_ID");
                                        dt_select.Columns.Remove("コントロールカラム");
                                        dt_select.Columns.Remove("商品番号");
                                        dt_select.Columns.Remove("あす楽配送管理番号");
                                        dt_select.Columns.Remove("表示先カテゴリ");
                                        dt_select.Columns.Remove("1ページ複数形式");
                                        dt_select.Columns.Remove("カテゴリセット管理番号");
                                        dt_select.Columns.Remove("カテゴリセット名");
                                        dt_select.Columns.Remove("商品管理ID（商品URL）");
                                        dt_select.Columns.Remove("翌日お届け設定番号");
                                        dt_select.Columns.Remove("選択肢タイプ1");
                                        dt_select.Columns.Remove("購入オプション名");
                                        dt_select.Columns.Remove("オプション項目名");
                                        dt_select.Columns.Remove("SKU横軸項目ID");
                                        dt_select.Columns.Remove("SKU横軸項目名");
                                        dt_select.Columns.Remove("SKU縦軸項目ID");
                                        dt_select.Columns.Remove("SKU縦軸項目名");
                                        dt_select.Columns.Remove("SKU在庫数");
                                        dt_select.Columns.Remove("ショップ内カテゴリ");
                                        dt_select.Columns.Remove("表示順位");
                                        dt_select.Columns.Remove("優先度");
                                        dt_select.AcceptChanges();
                                    }
                                    if (dt_cat.Rows.Count > 0)
                                    {
                                        dt_cat.Columns.Remove("ID");
                                        dt_cat.Columns.Remove("Mall_ID");
                                        dt_cat.Columns.Remove("コントロールカラム");
                                        if (dt_cat.Columns.Contains("項目選択肢用コントロールカラム"))
                                        {
                                            dt_cat.Columns["項目選択肢用コントロールカラム"].ColumnName = "コントロールカラム";
                                        }
                                        dt_cat.Columns.Remove("商品番号");
                                        dt_cat.Columns.Remove("あす楽配送管理番号");
                                        dt_cat.Columns.Remove("選択肢タイプ");
                                        dt_cat.Columns.Remove("Select/Checkbox用項目名");
                                        dt_cat.Columns.Remove("Select/Checkbox用選択肢");
                                        dt_cat.Columns.Remove("商品管理ID（商品URL）");
                                        dt_cat.Columns.Remove("翌日お届け設定番号");
                                        dt_cat.Columns.Remove("選択肢タイプ1");
                                        dt_cat.Columns.Remove("購入オプション名");
                                        dt_cat.Columns.Remove("オプション項目名");
                                        dt_cat.Columns.Remove("SKU横軸項目ID");
                                        dt_cat.Columns.Remove("SKU横軸項目名");
                                        dt_cat.Columns.Remove("SKU縦軸項目ID");
                                        dt_cat.Columns.Remove("SKU縦軸項目名");
                                        dt_cat.Columns.Remove("SKU在庫数");
                                        dt_cat.Columns.Remove("ショップ内カテゴリ");
                                        dt_cat.Columns.Remove("表示順位");
                                        dt_cat.AcceptChanges();
                                    }
                                    #endregion
                                    #region File
                                    if (dt_item.Rows.Count > 0)
                                    {
                                        using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Jisha/" + "deli-item$" + shop_id + "_" + date + "_" + groupno5 + ".csv", false, Encoding.GetEncoding(932)))
                                        {
                                            WriteDataTable(dt_item, writer, true);
                                            SaveItem_ExportQ("deli-item$" + shop_id + "_" + date + "_" + groupno5 + ".csv", 2, shop_id, 0, 3);
                                        }
                                        using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Jisha/" + "deli-item$" + shop_id + "_" + date + "_" + groupno5 + ".csv", false, Encoding.GetEncoding(932)))
                                        {
                                            WriteDataTable(dt_item, writer, true);
                                        }
                                    }
                                    if (dt_select.Rows.Count > 0)
                                    {
                                        using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Jisha/" + "deli-select$" + shop_id + "_" + date + "_" + groupno5 + ".csv", false, Encoding.GetEncoding(932)))
                                        {
                                            WriteDataTable(dt_select, writer, true);
                                            SaveItem_ExportQ("deli-select$" + shop_id + "_" + date + "_" + groupno5 + ".csv", 2, shop_id, 0, 3);
                                        }
                                        using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Jisha/" + "deli-select$" + shop_id + "_" + date + "_" + groupno5 + ".csv", false, Encoding.GetEncoding(932)))
                                        {
                                            WriteDataTable(dt_select, writer, true);
                                        }
                                    }
                                    if (dt_cat.Rows.Count > 0)
                                    {
                                        using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Jisha/" + "deli-item-cat$" + shop_id + "_" + date + "_" + groupno5 + ".csv", false, Encoding.GetEncoding(932)))
                                        {
                                            WriteDataTable(dt_cat, writer, true);
                                            SaveItem_ExportQ("deli-item-cat$" + shop_id + "_" + date + "_" + groupno5 + ".csv", 2, shop_id, 0, 3);
                                        }
                                        using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Jisha/" + "deli-item-cat$" + shop_id + "_" + date + "_" + groupno5 + ".csv", false, Encoding.GetEncoding(932)))
                                        {
                                            WriteDataTable(dt_cat, writer, true);
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                                #region generate select.csv
                                DataRow[] drj = dtAll.Select("ID =" + shop_id);
                                if (drj.Count() > 0)
                                {
                                    DataTable dtjs = null ;
                                    DataTable dtj = null;
                                    DataTable dtJisha = null;

                                     dtjs = dtAll.Select("ID =" + shop_id).CopyToDataTable();
                                     dtj = dtjs.Select("Mall_ID =5").CopyToDataTable();
                                     dtJisha = JColumname(dtj);
                                   
                                    if (dtJisha.Rows.Count > 0)
                                    {
                                        using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Jisha/" + "select$" + shop_id + "_" + date + "_" + groupno5 + ".csv", false, Encoding.GetEncoding(932)))
                                        {
                                            WriteDataTable(dtJisha, writer, true);
                                            SaveItem_ExportQ("select$" + shop_id + "_" + date + "_" + groupno5 + ".csv", 2, shop_id, 0, 3);
                                        }
                                        using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Jisha/" + "select$" + shop_id + "_" + date + "_" + groupno5 + ".csv", false, Encoding.GetEncoding(932)))
                                        {
                                            WriteDataTable(dtJisha, writer, true);
                                        }
                                    }
                                }
                                #endregion
                                break;
                        }
                    }
                }
                //ChangeItem_Master_IsGenerateFlag(item_list); //added by aam
                ChangeItem_Master_IsGenerateFlag(dtToChangeFlag);
                ChangeFlag(list); //27-05-2015
            }
            catch (Exception ex)
            {
                throw ex;
                //SendErrMessage(ex.Message, fileName); //ETZ
                //return;
            }
        
        }

        public static void WriteDataToFileForAdd(DataTable submittedDataTable, string submittedFilePath)
        {
            int i = 0;
            string template = "Offer";
            string ver = "1.4";
            StreamWriter sw = null;

            sw = new StreamWriter(submittedFilePath, false);

            sw.Write("TemplateType=" + template + "\tVersion=" + ver + "\n");
            for (i = 0; i < submittedDataTable.Columns.Count - 1; i++)
            {

                sw.Write(submittedDataTable.Columns[i].ColumnName + "\t");

            }
            sw.Write(submittedDataTable.Columns[i].ColumnName);
            sw.WriteLine();

            foreach (DataRow row in submittedDataTable.Rows)
            {
                object[] array = row.ItemArray;

                for (i = 0; i < array.Length - 1; i++)
                {
                    sw.Write(array[i].ToString() + "\t");
                }
                sw.Write(array[i].ToString());
                sw.WriteLine();

            }

            sw.Close();
        }

        public static DataTable YColumname(DataTable dt) 
        {
            dt.Columns.Remove("ID");
            dt.Columns.Remove("Mall_ID");
            //R
            dt.Columns.Remove("項目選択肢用コントロールカラム");
            dt.Columns.Remove("商品管理番号（商品URL）");
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
           
            //J
            dt.Columns.Remove("Qdata");

            //p
            dt.Columns.Remove("コントロールカラム");
            dt.Columns.Remove("商品管理ID（商品URL）");
            dt.Columns.Remove("選択肢タイプ1");
            dt.Columns.Remove("購入オプション名");
            dt.Columns.Remove("オプション項目名");
            dt.Columns.Remove("SKU横軸項目ID");
            dt.Columns.Remove("SKU横軸項目名");
            dt.Columns.Remove("SKU縦軸項目ID");
            dt.Columns.Remove("SKU縦軸項目名");
            dt.Columns.Remove("SKU在庫数");

            //A
            dt.Columns.Remove("Ctrl");
            dt.Columns.Remove("sku");
            dt.Columns.Remove("price");
            dt.Columns.Remove("quantity1");
            dt.Columns.Remove("product-id");
            dt.Columns.Remove("product-id-type");
            dt.Columns.Remove("condition-type");
            dt.Columns.Remove("condition-note");
            dt.Columns.Remove("ASIN-hint");
            dt.Columns.Remove("title");
            dt.Columns.Remove("operation-type");
            dt.Columns.Remove("sale-price");
            dt.Columns.Remove("sale-start-date");
            dt.Columns.Remove("sale-end-date");
            dt.Columns.Remove("leadtime-to-ship");
            dt.Columns.Remove("launch-date");
            dt.Columns.Remove("is-giftwrap-available");
            dt.Columns.Remove("is-gift-message-available");
            dt.AcceptChanges();
            return dt;
           
        }
        public static DataTable RColumname(DataTable dt)
        {
            dt.Columns.Remove("ID");
            dt.Columns.Remove("Mall_ID");
            //J
            dt.Columns.Remove("Qdata");
            //y
            dt.Columns.Remove("code");
            dt.Columns.Remove("sub-code");
            dt.Columns.Remove("quantity");
            dt.Columns.Remove("mode");
            //p
            dt.Columns.Remove("コントロールカラム");
            dt.Columns.Remove("商品管理ID（商品URL）");
            dt.Columns.Remove("選択肢タイプ1");
            dt.Columns.Remove("購入オプション名");
            dt.Columns.Remove("オプション項目名");
            dt.Columns.Remove("SKU横軸項目ID");
            dt.Columns.Remove("SKU横軸項目名");
            dt.Columns.Remove("SKU縦軸項目ID");
            dt.Columns.Remove("SKU縦軸項目名");
            dt.Columns.Remove("SKU在庫数");

            //A
            dt.Columns.Remove("Ctrl");
            dt.Columns.Remove("sku");
            dt.Columns.Remove("price");
            dt.Columns.Remove("quantity1");
            dt.Columns.Remove("product-id");
            dt.Columns.Remove("product-id-type");
            dt.Columns.Remove("condition-type");
            dt.Columns.Remove("condition-note");
            dt.Columns.Remove("ASIN-hint");
            dt.Columns.Remove("title");
            dt.Columns.Remove("operation-type");
            dt.Columns.Remove("sale-price");
            dt.Columns.Remove("sale-start-date");
            dt.Columns.Remove("sale-end-date");
            dt.Columns.Remove("leadtime-to-ship");
            dt.Columns.Remove("launch-date");
            dt.Columns.Remove("is-giftwrap-available");
            dt.Columns.Remove("is-gift-message-available");
          
            dt.AcceptChanges();
            return dt;
        }
        public static DataTable PColumname(DataTable dt)
        {
            dt.Columns.Remove("ID");
            dt.Columns.Remove("Mall_ID");
            //R
            dt.Columns.Remove("項目選択肢用コントロールカラム");
            dt.Columns.Remove("商品管理番号（商品URL）");
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

            //J
            dt.Columns.Remove("Qdata");

            //y
            dt.Columns.Remove("code");
            dt.Columns.Remove("sub-code");
            dt.Columns.Remove("quantity");
            dt.Columns.Remove("mode");

            //A
            dt.Columns.Remove("Ctrl");
            dt.Columns.Remove("sku");
            dt.Columns.Remove("price");
            dt.Columns.Remove("quantity1");
            dt.Columns.Remove("product-id");
            dt.Columns.Remove("product-id-type");
            dt.Columns.Remove("condition-type");
            dt.Columns.Remove("condition-note");
            dt.Columns.Remove("ASIN-hint");
            dt.Columns.Remove("title");
            dt.Columns.Remove("operation-type");
            dt.Columns.Remove("sale-price");
            dt.Columns.Remove("sale-start-date");
            dt.Columns.Remove("sale-end-date");
            dt.Columns.Remove("leadtime-to-ship");
            dt.Columns.Remove("launch-date");
            dt.Columns.Remove("is-giftwrap-available");
            dt.Columns.Remove("is-gift-message-available");

            if (dt.Columns.Contains("選択肢タイプ1"))
            {
                dt.Columns["選択肢タイプ1"].ColumnName = "選択肢タイプ";
            }
            dt.AcceptChanges();
            return dt;

        }
        public static DataTable AColumname(DataTable dt)
        {
            dt.Columns.Remove("ID");
            dt.Columns.Remove("Mall_ID");
            //J
            dt.Columns.Remove("Qdata");
            //y
            dt.Columns.Remove("code");
            dt.Columns.Remove("sub-code");
            dt.Columns.Remove("quantity");
            dt.Columns.Remove("mode");
            //p
            dt.Columns.Remove("コントロールカラム");
            dt.Columns.Remove("商品管理ID（商品URL）");
            dt.Columns.Remove("選択肢タイプ1");
            dt.Columns.Remove("購入オプション名");
            dt.Columns.Remove("オプション項目名");
            dt.Columns.Remove("SKU横軸項目ID");
            dt.Columns.Remove("SKU横軸項目名");
            dt.Columns.Remove("SKU縦軸項目ID");
            dt.Columns.Remove("SKU縦軸項目名");
            dt.Columns.Remove("SKU在庫数");

            //R
            dt.Columns.Remove("項目選択肢用コントロールカラム");
            dt.Columns.Remove("商品管理番号（商品URL）");
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
            dt.Columns.Remove("Ctrl");
            //if (dt.Columns.Contains("Ctrl"))
            //{
            //    dt.Columns["Ctrl"].ColumnName = "コントロールカラム";
            //}
            if (dt.Columns.Contains("quantity1"))
            {
                dt.Columns["quantity1"].ColumnName = "quantity";
            }
            dt.AcceptChanges();
            return dt;
        }
        public static DataTable JColumname(DataTable dt)
        {
            dt.Columns.Remove("ID");
            dt.Columns.Remove("Mall_ID");
            //R
            dt.Columns.Remove("項目選択肢別在庫用在庫数");
            //y
            dt.Columns.Remove("code");
            dt.Columns.Remove("sub-code");
            dt.Columns.Remove("quantity");
            dt.Columns.Remove("mode");
            //p
            dt.Columns.Remove("コントロールカラム");
            dt.Columns.Remove("商品管理ID（商品URL）");
            dt.Columns.Remove("選択肢タイプ1");
            dt.Columns.Remove("購入オプション名");
            dt.Columns.Remove("オプション項目名");
            dt.Columns.Remove("SKU横軸項目ID");
            dt.Columns.Remove("SKU横軸項目名");
            dt.Columns.Remove("SKU縦軸項目ID");
            dt.Columns.Remove("SKU縦軸項目名");
            dt.Columns.Remove("SKU在庫数");

            //A
            dt.Columns.Remove("Ctrl");
            dt.Columns.Remove("sku");
            dt.Columns.Remove("price");
            dt.Columns.Remove("quantity1");
            dt.Columns.Remove("product-id");
            dt.Columns.Remove("product-id-type");
            dt.Columns.Remove("condition-type");
            dt.Columns.Remove("condition-note");
            dt.Columns.Remove("ASIN-hint");
            dt.Columns.Remove("title");
            dt.Columns.Remove("operation-type");
            dt.Columns.Remove("sale-price");
            dt.Columns.Remove("sale-start-date");
            dt.Columns.Remove("sale-end-date");
            dt.Columns.Remove("leadtime-to-ship");
            dt.Columns.Remove("launch-date");
            dt.Columns.Remove("is-giftwrap-available");
            dt.Columns.Remove("is-gift-message-available");

            if (dt.Columns.Contains("Qdata")) 
            {
                dt.Columns["Qdata"].ColumnName = "項目選択肢別在庫用在庫数";
            }
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

        static string GetItem_Master_IDLists()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = GetConnection();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectNotUploadItem", con);
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

        public static DataTable GetData(string option, int shop_id,string list)

        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = GetConnection();
                using (SqlDataAdapter da = new SqlDataAdapter("SP_Item_SelectNotUploadNew", con))   //sp name changed
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
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

        public static void ChangeFlag(string changeItemlist) //27-05-2015
        {
            try
            {
                //string query = "UPDATE Item SET Order_Flag = 2 WHERE Order_Flag = 1 AND ID IN (SELECT * FROM Split2(@list))";
                //cmd.Parameters.AddWithValue("@list", changeItemlist);
                SqlConnection connection = GetConnection();
                SqlCommand cmd = new SqlCommand("SP_ChangeItem_Master_IsGenerateFlag", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_Master_ID_List", changeItemlist);
                cmd.Parameters.AddWithValue("@Option", 2);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        //added by aam
        //public static void ChangeItem_Master_IsGenerateFlag(string changeItemlist) 
        //{
        //    try
        //    {
        //        SqlConnection connection = GetConnection();
        //        SqlCommand cmd = new SqlCommand("SP_ChangeItem_Master_IsGenerateFlag", connection);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;
        //        cmd.Parameters.AddWithValue("@Item_Master_ID_List", changeItemlist);
        //        cmd.Parameters.AddWithValue("@Option", 1);
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public static void ChangeItem_Master_IsGenerateFlag(DataTable dtToChangeFlag)
        {
            try
            {
                dtToChangeFlag.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dtToChangeFlag.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string xml = writer.ToString();

                SqlConnection connection = GetConnection();
                SqlCommand cmd = new SqlCommand("SP_ChangeItem_Master_QuickShippingIsGenerateFlag", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                //cmd.Parameters.AddWithValue("@Option", 1);
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
                string quary = "SELECT ID,Mall_ID FROM Shop";
                DataTable dt = new DataTable();
                SqlConnection connection = GetConnection();
                SqlDataAdapter sda = new SqlDataAdapter(quary, connection);
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetInventoryShop(string IDlist)//added by KTA
        {
            try
            {
                string quary = "SP_Inventory_Shoplist ";
                DataTable dt = new DataTable();
                SqlConnection connection = GetConnection();
                SqlDataAdapter sda = new SqlDataAdapter(quary, connection);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@ItemID", IDlist);
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

        public static DataTable GetInvData(string option,string list)//added by KTA
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

        public static DataTable GetItemData(string list)//added by aam
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = GetConnection();
                using (SqlDataAdapter da = new SqlDataAdapter("SP_Select_InventoryData", con))   //sp name changed
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.CommandTimeout = 0;
                    da.SelectCommand.Parameters.AddWithValue("@Option", "item");
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

        public static DataTable GetItemDataYahoo(string list)//added by aam
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = GetConnection();
                using (SqlDataAdapter da = new SqlDataAdapter("SP_Select_InventoryData", con))   //sp name changed
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.CommandTimeout = 0;
                    da.SelectCommand.Parameters.AddWithValue("@Option", "item_yahoo");
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

        //public static void SendErrMessage(string errMessage, String csv)
        //{
        //    #region Create Log File
        //    if (File.Exists(errLogPath + "log.txt"))
        //    {
        //        File.Delete(errLogPath + "log.txt");
        //    }
        //    File.AppendAllText(errLogPath + "log.txt", "Date : " + DateTime.Now.ToString() + " " + errMessage);
        //    #endregion

        //    SmtpClient client = new SmtpClient();
        //    client.Port = 587;
        //    client.Host = "smtp.gmail.com";
        //    client.EnableSsl = true;
        //    client.Timeout = 10000;
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = new System.Net.NetworkCredential("capital.zaw@gmail.com", "zawmyolwin123");

        //    MailMessage mm = new MailMessage("capital.zaw@gmail.com", "capital.zaw@gmail.com", "Error Message (Export CSV)", errMessage + " File Name=" + fileName);
        //    mm.BodyEncoding = UTF8Encoding.UTF8;
        //    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

        //    client.Send(mm);
        //}  //ETZ

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

        public static DataTable GetQuickShipping()//added by aam
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = GetConnection();
                using (SqlDataAdapter da = new SqlDataAdapter("SP_Select_QuickShippingData", con))   //sp name changed
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.CommandTimeout = 0;
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

        //public static void ExportCSV()
        //{
        //    try
        //    {
        //        //string groupno = null;
        //        String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
        //        string filename = date + ".csv";

        //        // DataTable dtShop = GetShop();

        //        string list = GetItem_IDList();// 27-05-2015

        //        DataTable dtShop = GetInventoryShop(list);// added by KTA 28/05/2015

        //        if (dtShop != null && dtShop.Rows.Count > 0)
        //        {

        //            DataRow[] drRakuten = dtShop.Select("Mall_ID = 1");
        //            DataRow[] drYahoo = dtShop.Select("Mall_ID = 2");
        //            DataRow[] drPonpare = dtShop.Select("Mall_ID = 3");
        //            DataRow[] drAmazon = dtShop.Select("Mall_ID = 4");
        //            DataRow[] drJisha = dtShop.Select("Mall_ID = 5");
        //            if (drRakuten.Count() > 0)
        //            {
        //                DataTable dtRakutendata = dtShop.Select("Mall_ID=1").CopyToDataTable();
        //                if (dtRakutendata != null && dtRakutendata.Rows.Count > 0)
        //                {
        //                    for (int i = 0; i < dtRakutendata.Rows.Count; i++)
        //                    {
        //                        int shop_id = int.Parse(dtRakutendata.Rows[i]["ID"].ToString());
        //                        DataTable dtRakuten = GetData("Rakuten", shop_id, list);//27-05-2015
        //                        if (dtRakuten.Rows.Count > 0)
        //                        {
        //                            using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtRakuten, writer, true);
        //                                SaveItem_ExportQ("select$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
        //                            }
        //                            using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtRakuten, writer, true);
        //                            }
        //                        }
        //                    }
        //                }
        //            }//for Rakuten

        //            if (drYahoo.Count() > 0)
        //            {
        //                DataTable dtYahoo = GetData("Yahoo", shop_id, list);  //27-05-2015
        //                if (dtYahoo.Rows.Count > 0)
        //                {
        //                    using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Yahoo/" + "quantity$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                    {
        //                        WriteDataTable(dtYahoo, writer, true);
        //                        SaveItem_ExportQ("quantity$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
        //                    }
        //                    using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Yahoo/" + "quantity$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                    {
        //                        WriteDataTable(dtYahoo, writer, true);
        //                    }
        //                }
        //            }//for Yahoo

        //            if (drRakuten.Count() > 0)
        //            {
        //                DataTable dtRakutendata = dtShop.Select("Mall_ID=1").CopyToDataTable();
        //                if (dtRakutendata != null && dtRakutendata.Rows.Count > 0)
        //                {
        //                    for (int i = 0; i < dtRakutendata.Rows.Count; i++)
        //                    {
        //                        int shop_id = int.Parse(dtRakutendata.Rows[i]["ID"].ToString());
        //                        DataTable dtRakuten = GetData("Rakuten", shop_id, list);//27-05-2015
        //                        if (dtRakuten.Rows.Count > 0)
        //                        {
        //                            using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtRakuten, writer, true);
        //                                SaveItem_ExportQ("select$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
        //                            }
        //                            using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtRakuten, writer, true);
        //                            }
        //                        }
        //                    }
        //                }
        //            }//for Ponpare

        //            if (drRakuten.Count() > 0)
        //            {
        //                DataTable dtRakutendata = dtShop.Select("Mall_ID=1").CopyToDataTable();
        //                if (dtRakutendata != null && dtRakutendata.Rows.Count > 0)
        //                {
        //                    for (int i = 0; i < dtRakutendata.Rows.Count; i++)
        //                    {
        //                        int shop_id = int.Parse(dtRakutendata.Rows[i]["ID"].ToString());
        //                        DataTable dtRakuten = GetData("Rakuten", shop_id, list);//27-05-2015
        //                        if (dtRakuten.Rows.Count > 0)
        //                        {
        //                            using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtRakuten, writer, true);
        //                                SaveItem_ExportQ("select$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
        //                            }
        //                            using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtRakuten, writer, true);
        //                            }
        //                        }
        //                    }
        //                }
        //            }//for Amazon

        //            if (drRakuten.Count() > 0)
        //            {
        //                DataTable dtRakutendata = dtShop.Select("Mall_ID=1").CopyToDataTable();
        //                if (dtRakutendata != null && dtRakutendata.Rows.Count > 0)
        //                {
        //                    for (int i = 0; i < dtRakutendata.Rows.Count; i++)
        //                    {
        //                        int shop_id = int.Parse(dtRakutendata.Rows[i]["ID"].ToString());
        //                        DataTable dtRakuten = GetData("Rakuten", shop_id, list);//27-05-2015
        //                        if (dtRakuten.Rows.Count > 0)
        //                        {
        //                            using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtRakuten, writer, true);
        //                                SaveItem_ExportQ("select$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
        //                            }
        //                            using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtRakuten, writer, true);
        //                            }
        //                        }
        //                    }
        //                }
        //            }//for Jisha



        //        }//if shoplist







        //        if (dtShop != null && dtShop.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dtShop.Rows.Count; i++)
        //            {
        //                int shop_id = int.Parse(dtShop.Rows[i]["ID"].ToString());

        //                switch (dtShop.Rows[i]["Mall_ID"].ToString())  //Check Mall
        //                {
        //                    case "1":
        //                        DataTable dtRakuten = GetData("Rakuten", shop_id, list);//27-05-2015
        //                        if (dtRakuten.Rows.Count > 0)
        //                        {
        //                            using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtRakuten, writer, true);
        //                                SaveItem_ExportQ("select$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
        //                            }
        //                            using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Rakuten/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtRakuten, writer, true);
        //                            }
        //                        }
        //                        break;
        //                    case "2":
        //                        DataTable dtYahoo = GetData("Yahoo", shop_id, list);  //27-05-2015
        //                        if (dtYahoo.Rows.Count > 0)
        //                        {
        //                            using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Yahoo/" + "quantity$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtYahoo, writer, true);
        //                                SaveItem_ExportQ("quantity$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
        //                            }
        //                            using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Yahoo/" + "quantity$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtYahoo, writer, true);
        //                            }
        //                        }
        //                        break;
        //                    case "3":
        //                        DataTable dtPonpare = GetData("Ponpare", shop_id, list); //27-05-2015
        //                        if (dtPonpare.Rows.Count > 0)
        //                        {
        //                            using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Ponpare/" + "option$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtPonpare, writer, true);
        //                                SaveItem_ExportQ("option$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
        //                            }
        //                            using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Ponpare/" + "option$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtPonpare, writer, true);
        //                            }
        //                        }
        //                        break;
        //                    case "4":
        //                        DataTable dtAmazon = GetData("Amazon", shop_id, list);   //27-05-2015
        //                        if (dtAmazon.Rows.Count > 0)
        //                        {
        //                            using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Amazon/" + "sku$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtAmazon, writer, true);
        //                                SaveItem_ExportQ("sku$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
        //                            }
        //                            using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Amazon/" + "sku$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtAmazon, writer, true);
        //                            }
        //                        }
        //                        break;
        //                    case "5":
        //                        DataTable dtJisha = GetData("Jisha", shop_id, list); //27-05-2015
        //                        if (dtJisha.Rows.Count > 0)
        //                        {
        //                            using (StreamWriter writer = new StreamWriter(ExportInventoryCSVPath + "/Jisha/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtJisha, writer, true);
        //                                SaveItem_ExportQ("select$" + shop_id + "_" + filename, 2, shop_id, 0, 3);
        //                            }
        //                            using (StreamWriter writer = new StreamWriter(BakExportInventoryCSVPath + "/Jisha/" + "select$" + shop_id + "_" + filename, false, Encoding.GetEncoding(932)))
        //                            {
        //                                WriteDataTable(dtJisha, writer, true);
        //                            }
        //                        }
        //                        break;
        //                }
        //            }
        //        }
        //        ChangeFlag(list); //27-05-2015
        //    }
        //    catch (Exception ex)
        //    {
        //        SendErrMessage(ex.Message, fileName);
        //        return;
        //    }

        //}

       static  void ConsoleWriteLine_Tofile(string traceText, int shop_id)
        {
            //StreamWriter sw = new StreamWriter("ConsoleWriteLine.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
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
