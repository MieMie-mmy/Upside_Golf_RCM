using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ORS_RCM_BL;
using System.IO;
using System.Text;
using System.Configuration;
using ORS_RCM_Common;

namespace ORS_RCM.WebForms.Item
{
    /// <summary>
    /// divided by type and export data file
    /// </summary>
    public class Export_CSV2
    {
        /// <summary>
        /// old version -currenly not using. ExportCSV4 is working .
        /// export csvs to rakuten shops
        /// </summary>
        /// <param name="dtMainSku">datable for Inventory</param>
        /// <param name="dtMainItem">datable for Item Master</param>
        /// <param name="dtMainCategory">datatable for Item Category</param>
        /// <param name="shop_id">shop id to export</param>
        public void RakutenFilterSKU(DataTable dtMainSku,DataTable dtMainItem,DataTable dtMainCategory,int shop_id)
        {
            #region IsSKU=2
            DataRow[] dr = dtMainItem.Select("IsSKU=2");
            if (dr.Count() > 0)
            {
                dr = dtMainSku.Select("IsSKU_Update=1 AND IsSKU=2");
                if (dr.Count() > 0)
                {
                    dr = dtMainItem.Select("IsSKU=2");
                    if (dr.Count() > 0)
                    {
                        DataTable dtItem = dtMainItem.Select("IsSKU=2").CopyToDataTable();
                        //1.item.csv
                        CreateFile(dtItem, "u", "item-", shop_id, 0, "_2_1.csv", "1.3.2.4.1"); //1.3.2.4.1

                        //2.item2.csv
                        CreateFile(dtItem, "u", "item-", shop_id, 0, "_2_2.csv", "1.3.2.1.1"); //1.3.2.4.2
                    }

                    DataTable dtSku = dtMainSku.Select("IsSKU_Update=1 AND IsSKU=2").CopyToDataTable();
                    //3.item_select.csv
                    CreateFile(dtSku, "n", "item_select-", shop_id, 2, "_2_1.csv", "1.3.2.1.2"); //1.3.2.4.3

                    //4.item_select.csv
                    CreateFile(dtSku, "d", "item_select-", shop_id, 2, "_2_2.csv", "1.3.2.1.2"); //1.3.2.4.4

                    //5.item_select.csv
                    CreateFile(dtSku, "n", "item_select-", shop_id, 2, "_2_3.csv", "1.3.2.1.2"); //1.3.2.4.5

                    dr = dtMainCategory.Select("IsSKU=2");
                    if (dr.Count() > 0)
                    {
                        DataTable dtCategory = dtMainCategory.Select("IsSKU=2").CopyToDataTable();
                        //6.item_cat.csv
                        CreateFile(dtCategory, "d", "item_cat-", shop_id, 1, "_2_1.csv", "1.3.2.1.3"); //1.3.2.4.6

                        //7.item_cat.csv
                        CreateFile(dtCategory, "n", "item_cat-", shop_id, 1, "_2_2.csv", "1.3.2.1.3"); //1.3.2.4.7
                    }
                }
            }
            #endregion

            #region IsSKU=1
            dr = dtMainSku.Select("IsSKU_Update=0 AND IsSKU=1");
            if (dr.Count() > 0)
            {
                dr = dtMainItem.Select("IsSKU=1");
                if (dr.Count() > 0)
                {
                    DataTable dtItem = dtMainItem.Select("IsSKU=1").CopyToDataTable();
                    //1.item.csv
                    CreateFile(dtItem, "u", "item-", shop_id, 0, "_1_1.csv", "1.3.2.1.1");//1.3.2.3.1
                }

                DataTable dtSku = dtMainSku.Select("IsSKU_Update=0 AND IsSKU=1").CopyToDataTable();
                //2.item_select.csv
                CreateFile(dtSku, "d", "item_select-", shop_id, 2, "_1_1.csv", "1.3.2.1.2"); // 1.3.2.3.2

                //3.item_select.csv
                CreateFile(dtSku, "n", "item_select-", shop_id, 2, "_1_2.csv", "1.3.2.1.2"); // 1.3.2.3.3

                dr = dtMainCategory.Select("IsSKU=1");
                if (dr.Count() > 0)
                {
                    DataTable dtCategory = dtMainCategory.Select("IsSKU=1").CopyToDataTable();
                    //4.item_cat.csv
                    CreateFile(dtCategory, "d", "item_cat-", shop_id, 1, "_1_1.csv", "1.3.2.1.3"); // 1.3.2.3.4

                    //5.item_cat.csv
                    CreateFile(dtCategory, "n", "item_cat-", shop_id, 1, "_1_2.csv", "1.3.2.1.3"); //1.3.2.3.5
                }
            }
            #endregion

            #region IsSKU=0
            dr = dtMainItem.Select("IsSKU=0");
            if (dr.Count() > 0)
            {
                dr = dtMainItem.Select("IsSKU=0");//edited by hlz, previously it is check IsSKU=1.
                if (dr.Count() > 0)
                {
                    DataTable dtItem = dtMainItem.Select("IsSKU=0").CopyToDataTable();
                    
                    //1.item.csv
                    CreateFile(dtItem, "n", "item-", shop_id, 0, "_0_1.csv", "1.3.2.1.1"); //1.3.2.1.1
                }

                dr = dtMainSku.Select("IsSKU=0");
                if (dr.Count() > 0)
                {
                    DataTable dtSku = dtMainSku.Select("IsSKU=0").CopyToDataTable();
                    //2.item_select.csv
                    CreateFile(dtSku, "n", "item_select-", shop_id, 2, "_0_1.csv", "1.3.2.1.2"); //1.3.2.1.2
                }

                dr = dtMainCategory.Select("IsSKU=0");
                if (dr.Count() > 0)
                {
                    DataTable dtCategory = dtMainCategory.Select("IsSKU=0").CopyToDataTable();
                    //3.item_cat.csv
                    CreateFile(dtCategory, "n", "item_cat-", shop_id, 1, "_0_1.csv", "1.3.2.1.3"); //1.3.2.1.3
                }
            }
            #endregion
        }

        /// <summary>
        ///  export csv to ponpare shops
        /// </summary>
        /// <param name="dtMainSku">datable for Inventory</param>
        /// <param name="dtMainItem">datable for Item Master</param>
        /// <param name="dtMainCategory">datatable for Item Category</param>
        /// <param name="shop_id"></param>
        public void PonpareFilterSKU(DataTable dtMainSku, DataTable dtMainItem, DataTable dtMainCategory, int shop_id)
        {
            #region IsSKU=2
            DataRow[] dr = dtMainItem.Select("IsSKU=2");
            if (dr.Count() > 0)
            {
                dr = dtMainSku.Select("IsSKU_Update=1 AND IsSKU=2");
                if (dr.Count() > 0)
                {
                    dr = dtMainItem.Select("IsSKU=2");
                    if (dr.Count() > 0)
                    {
                        DataTable dtItem = dtMainItem.Select("IsSKU=2").CopyToDataTable();
                        //1.item.csv
                        CreateFile(dtItem, "u", "item-", shop_id, 0, "_2_1.csv", "1.3.2.4.1"); //1.3.2.4.1

                        //2.item2.csv
                        CreateFile(dtItem, "u", "item-", shop_id, 0, "_2_2.csv", "1.3.2.1.1"); //1.3.2.4.2
                    }

                    DataTable dtSku = dtMainSku.Select("IsSKU_Update=1 AND IsSKU=2").CopyToDataTable();
                    //3.option.csv
                    CreateFile(dtSku, "n", "option-", shop_id, 2, "_2_1.csv", "1.3.2.1.2"); //1.3.2.4.3

                    //4.option.csv
                    CreateFile(dtSku, "d", "option-", shop_id, 2, "_2_2.csv", "1.3.2.1.2"); //1.3.2.4.4

                    //5.option.csv
                    CreateFile(dtSku, "n", "option-", shop_id, 2, "_2_3.csv", "1.3.2.1.2"); //1.3.2.4.5

                    dr = dtMainCategory.Select("IsSKU=2");
                    if (dr.Count() > 0)
                    {
                        DataTable dtCategory = dtMainCategory.Select("IsSKU=2").CopyToDataTable();
                        //6.category.csv
                        CreateFile(dtCategory, "d", "category-", shop_id, 1, "_2_1.csv", "1.3.2.1.3"); //1.3.2.4.6

                        //7.category.csv
                        CreateFile(dtCategory, "n", "category-", shop_id, 1, "_2_2.csv", "1.3.2.1.3"); //1.3.2.4.7
                    }
                }
            }
            #endregion

            #region IsSKU=1
            dr = dtMainSku.Select("IsSKU_Update=0 AND IsSKU=1");
            if (dr.Count() > 0)
            {
                dr = dtMainItem.Select("IsSKU=1");
                if (dr.Count() > 0)
                {
                    DataTable dtItem = dtMainItem.Select("IsSKU=1").CopyToDataTable();
                    //1.item.csv
                    CreateFile(dtItem, "u", "item-", shop_id, 0, "_1_1.csv", "1.3.2.1.1");//1.3.2.3.1
                }

                DataTable dtSku = dtMainSku.Select("IsSKU_Update=0 AND IsSKU=1").CopyToDataTable();
                //2.option.csv
                CreateFile(dtSku, "d", "option-", shop_id, 2, "_1_1.csv", "1.3.2.1.2"); // 1.3.2.3.2

                //3.option.csv
                CreateFile(dtSku, "n", "option-", shop_id, 2, "_1_2.csv", "1.3.2.1.2"); // 1.3.2.3.3

                dr = dtMainCategory.Select("IsSKU=1");
                if (dr.Count() > 0)
                {
                    DataTable dtCategory = dtMainCategory.Select("IsSKU=1").CopyToDataTable();
                    //4.category.csv
                    CreateFile(dtCategory, "d", "category-", shop_id, 1, "_1_1.csv", "1.3.2.1.3"); // 1.3.2.3.4

                    //5.category.csv
                    CreateFile(dtCategory, "n", "category-", shop_id, 1, "_1_2.csv", "1.3.2.1.3"); //1.3.2.3.5
                }
            }
            #endregion

            #region IsSKU=0
            dr = dtMainItem.Select("IsSKU=0");
            if (dr.Count() > 0)
            {
                dr = dtMainItem.Select("IsSKU=0");//edited by hlz, previously it is check IsSKU=1.
                if (dr.Count() > 0)
                {
                    DataTable dtItem = dtMainItem.Select("IsSKU=0").CopyToDataTable();
                    //1.item.csv
                    CreateFile(dtItem, "n", "item-", shop_id, 0, "_0_1.csv", "1.3.2.1.1"); //1.3.2.1.1
                }

                dr = dtMainSku.Select("IsSKU=0");
                if (dr.Count() > 0)
                {
                    DataTable dtSku = dtMainSku.Select("IsSKU=0").CopyToDataTable();
                    //2.option.csv
                    CreateFile(dtSku, "n", "option-", shop_id, 2, "_0_1.csv", "1.3.2.1.2"); //1.3.2.1.2
                }

                dr = dtMainCategory.Select("IsSKU=0");
                if (dr.Count() > 0)
                {
                    DataTable dtCategory = dtMainCategory.Select("IsSKU=0").CopyToDataTable();
                    //3.category.csv
                    CreateFile(dtCategory, "n", "category-", shop_id, 1, "_0_1.csv", "1.3.2.1.3"); //1.3.2.1.3
                }
            }
            #endregion
        }

        /// <summary>
        /// change data of csv file
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileNo"></param>
        /// <returns></returns>
        public DataTable FormatFile(DataTable dt ,String fileNo)
        {
            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrWhiteSpace(fileNo))
            {
                switch (fileNo)
                {
                    case "1.3.2.1.1":    // item.csv (ctrl = n , u)
                        {
                            //dt.Columns.Remove("IsSKU");
                            //dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.4.1": // item.csv (ctrl = u)
                        {
                            //dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("商品番号");
                            dt.Columns.Remove("全商品ディレクトリID");
                            dt.Columns.Remove("タグID");
                            dt.Columns.Remove("PC用キャッチコピー");
                            dt.Columns.Remove("モバイル用キャッチコピー");
                            dt.Columns.Remove("商品名");
                            dt.Columns.Remove("販売価格");
                            dt.Columns.Remove("表示価格");
                            dt.Columns.Remove("消費税");
                            dt.Columns.Remove("送料");
                            dt.Columns.Remove("個別送料");
                            dt.Columns.Remove("送料区分1");
                            dt.Columns.Remove("送料区分2");
                            dt.Columns.Remove("代引料");
                            dt.Columns.Remove("商品情報レイアウト");
                            dt.Columns.Remove("注文ボタン");
                            dt.Columns.Remove("資料請求ボタン");
                            dt.Columns.Remove("商品問い合わせボタン");
                            dt.Columns.Remove("再入荷お知らせボタン");
                            dt.Columns.Remove("モバイル表示");
                            dt.Columns.Remove("のし対応");
                            dt.Columns.Remove("PC用商品説明文");
                            dt.Columns.Remove("モバイル用商品説明文");
                            dt.Columns.Remove("スマートフォン用商品説明文");
                            dt.Columns.Remove("PC用販売説明文");
                            dt.Columns.Remove("商品画像URL");
                            dt.Columns.Remove("商品画像名（ALT）");
                            dt.Columns.Remove("動画");
                            dt.Columns.Remove("販売期間指定");
                            dt.Columns.Remove("注文受付数");
                            dt.Columns.Remove("在庫数");
                            dt.Columns.Remove("在庫数表示");
                            dt.Columns.Remove("項目選択肢別在庫用横軸項目名");
                            dt.Columns.Remove("項目選択肢別在庫用縦軸項目名");
                            dt.Columns.Remove("項目選択肢別在庫用残り表示閾値");
                            dt.Columns.Remove("RAC番号");
                            dt.Columns.Remove("闇市パスワード");
                            dt.Columns.Remove("カタログID");
                            dt.Columns.Remove("在庫戻しフラグ");
                            dt.Columns.Remove("在庫切れ時の注文受付");
                            dt.Columns.Remove("在庫あり時納期管理番号");
                            dt.Columns.Remove("在庫切れ時納期管理番号");
                            dt.Columns.Remove("予約商品発売日");
                            dt.Columns.Remove("ポイント変倍率");
                            dt.Columns.Remove("ポイント変倍率適用期間");
                            dt.Columns.Remove("ヘッダー・フッター・レフトナビ");
                            dt.Columns.Remove("表示項目の並び順");
                            dt.Columns.Remove("共通説明文（小）");
                            dt.Columns.Remove("目玉商品");
                            dt.Columns.Remove("共通説明文（大）");
                            dt.Columns.Remove("レビュー本文表示");
                            dt.Columns.Remove("あす楽配送管理番号");
                            dt.Columns.Remove("サイズ表リンク");
                            dt.Columns.Remove("二重価格文言管理番号");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.1.2":  //item_select.csv (ctrl_ID = n , u , d)
                        {
                            //dt.Columns.Remove("IsSKU");
                            //dt.Columns.Remove("IsSKU_Update");
                            //dt.Columns.Remove("コントロールカラム");
                            //dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.1.3":  //item_cat.csv (Ctrl_ID = n,u,d)
                        {
                            //dt.Columns.Remove("IsSKU");
                            //dt.AcceptChanges();
                            break;
                        }
                }
            }
            return dt;
        }

        /// <summary>
        /// create cs file
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="CtrlID"></param>
        /// <param name="firstName"></param>
        /// <param name="shop_id"></param>
        /// <param name="filetype"></param>
        /// <param name="extension"></param>
        /// <param name="fileNo"></param>
        public void CreateFile(DataTable dt,String CtrlID,String firstName,int shop_id,int filetype,String extension,String fileNo)
        {
            if (!string.IsNullOrWhiteSpace(CtrlID))
            {
                DataRow[] dr = dt.Select("[コントロールカラム] = '" + CtrlID + "'");
                if (dr.Count() > 0)
                {
                    dt = dt.Select("[コントロールカラム] = '" + CtrlID + "'").CopyToDataTable();
                    DataTable dtCopy = dt.Copy();
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    String filename = firstName + shop_id + "_" + date + extension;
                    dtCopy = FormatFile(dtCopy, fileNo);
                    GenerateCSV(dtCopy, filename);
                    SaveItem_ExportQ(filename, filetype, shop_id, 0);
                }
            }
            else
            {
                DataTable dtCopy = dt.Copy();
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                String filename = firstName + shop_id + "_" + date + extension;
                dtCopy = FormatFile(dtCopy, fileNo);
                GenerateCSV(dtCopy, filename);
                SaveItem_ExportQ(filename, filetype, shop_id, 0);
            }
        }

        /// <summary>
        /// insert
        /// </summary>
        /// <param name="dtInformation"></param>
        /// <param name="FileName"></param>
        public void GenerateCSV(DataTable dtInformation, string FileName)
        {
            string ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
            using (StreamWriter writer = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath(ExportCSVPath + FileName), false, Encoding.GetEncoding(932)))
            {
                WriteDataTable(dtInformation, writer, true);
            }
        }

        private string QuoteValue(string value)
        {
            return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="writer"></param>
        /// <param name="includeHeaders"></param>
        public void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
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

        /// <summary>
        /// Insert into qtable to use when  console  system export file to related shop.
        /// </summary>
        /// <param name="FileName">csv file name</param>
        /// <param name="FileType">csv file type-Item,Inventory,Category</param>
        /// <param name="ShopID">shopid to export</param>
        /// <param name="IsExport">default is 0</param>
        public void SaveItem_ExportQ(string FileName, int FileType, int ShopID, int IsExport)
        {
            Item_ExportQ_Entity ie = new Item_ExportQ_Entity();
            Item_ExportQ_BL ieBL = new Item_ExportQ_BL();

            ie.File_Name = FileName;
            ie.File_Type = FileType;
            ie.ShopID = ShopID;
            ie.IsExport = IsExport;

            ieBL.Save(ie);
        }
    }
}