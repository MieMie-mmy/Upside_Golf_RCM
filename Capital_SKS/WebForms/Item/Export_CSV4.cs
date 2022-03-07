using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using ORS_RCM_Common;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Item
{
    public class Export_CSV4
    {
        Item_Shop_BL ISBL = new Item_Shop_BL();
        //Item_Master_BL IMBL = new Item_Master_BL();

        public void RakutenFilterSKU(DataTable dtMainSku, DataTable dtMainItem, DataTable dtMainCategory, int shop_id)
        {
            //For new series
            #region IsSKU=0

            DataRow[]  dr = dtMainItem.Select("IsSKU=0");
                if (dr.Count() > 0)
                {
                    DataTable dtItem = dtMainItem.Select("IsSKU=0").CopyToDataTable();

                    //1.item.csv
                    string filename = CreateFile(dtItem, "n", "item$", shop_id, 0, "_0_0.csv", "1.3.2.1.1"); //1.3.2.1.1
                    
                    foreach (DataRow drTmp in dtItem.Rows)
                    {
                        SaveItemShopExportedCSVInfo(int.Parse(drTmp["ID"].ToString()), shop_id,filename);
                    }
                }

                //Filter by shop id
                dr = dtMainSku.Select("IsSKU=0 AND Shop_ID=" + shop_id + "");
                if (dr.Count() > 0)
                {
                    DataTable dtSku = dtMainSku.Select("IsSKU=0 AND Shop_ID=" + shop_id + "").CopyToDataTable();

                    //2.select.csv
                    CreateFile(dtSku, "n", "select$", shop_id, 2, "_0_0.csv", "1.3.2.1.2"); //1.3.2.1.2
                }

                dr = dtMainCategory.Select("IsSKU=0");
                if (dr.Count() > 0)
                {
                    DataTable dtCategory = dtMainCategory.Select("IsSKU=0").CopyToDataTable();

                    //3.item_cat.csv
                    CreateFile(dtCategory, "n", "item-cat$", shop_id, 1, "_0_0.csv", "1.3.2.1.3"); //1.3.2.1.3
                }
            
            #endregion

            //For update series
            #region IsSKU=1
            dr = dtMainItem.Select("IsSKU=1");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("IsSKU=1").CopyToDataTable();

                //倉庫指定=1, 在庫タイプ=0 change by csv format 
                DataTable dtEdit = dtItem.Copy();
                foreach (DataRow drEdit in dtEdit.Rows)
                {
                    drEdit["倉庫指定"] = 1;
                    drEdit["在庫タイプ"] = 0;
                }
                //1.item.csv
                CreateFile(dtEdit, "u", "item$#", shop_id, 0, "_1_1.csv", "1.3.2.4.1"); //1.3.2.4.1

                //2.item2.csv
                CreateFile(dtItem, "u", "item$", shop_id, 0, "_1_2.csv", "1.3.2.1.1"); //1.3.2.4.2
            }

            //dr = dtMainSku.Select("IsSKU=1 AND [コントロールカラム]='d' AND Shop_ID=" + shop_id + "");
            dr = dtMainSku.Select("IsSKU=1 AND Shop_ID=" + shop_id + "");
            if(dr.Count() > 0)
            {
                DataTable dtSku = dtMainSku.Select("IsSKU=1 AND Shop_ID=" + shop_id + "").CopyToDataTable();

                //3.item_select.csv (選択肢タイプ=i)
                DataTable dtItem;
                dr = dtSku.Select("[選択肢タイプ]='i'");
                if (dr.Count() > 0)
                {
                    dtItem = dtSku.Select("[選択肢タイプ]='i'").CopyToDataTable();
                    CreateFile(dtItem, "n", "select$", shop_id, 2, "_1_1.csv", "1.3.2.1.2"); //1.3.2.4.3
                }

                //4.item_select.csv (選択肢タイプ=s)
                dr = dtSku.Select("[選択肢タイプ]='s'");
                if (dr.Count() > 0)
                {
                    dtItem = dtSku.Select("[選択肢タイプ]='s'").CopyToDataTable();
                    CreateFile(dtItem, "d", "select$", shop_id, 2, "_1_2.csv", "1.3.2.1.2"); //1.3.2.4.4

                    //5.item_select.csv (選択肢タイプ=s)
                    CreateFile(dtItem, "n", "select$", shop_id, 2, "_1_3.csv", "1.3.2.1.2"); //1.3.2.4.5
                }
            }

            dr = dtMainCategory.Select("IsSKU=1 AND [コントロールカラム]='d'");
            if (dr.Count() > 0)
            {
                DataTable dtCategory = dtMainCategory.Select("IsSKU=1").CopyToDataTable();
                //6.item_cat.csv
                CreateFile(dtCategory, "d", "item-cat$", shop_id, 1, "_1_1.csv", "1.3.2.1.3"); //1.3.2.4.6

                //7.item_cat.csv
                CreateFile(dtCategory, "n", "item-cat$", shop_id, 1, "_1_2.csv", "1.3.2.1.3"); //1.3.2.4.7
            }
            #endregion

            //Change Ctrl_ID = @Ctrl_ID,Exhibition_Status = 1,API_Check=0,Batch_Check=0,Error_Check=0 accoding to shop id and item code from Item_Shop table
            if (dtMainItem != null && dtMainItem.Rows.Count > 0)
            {
                for (int i = 0; i < dtMainItem.Rows.Count; i++)
                {
                    ISBL.ChangeStatus(dtMainItem.Rows[i]["商品番号"].ToString(), dtMainItem.Rows[i]["コントロールカラム"].ToString(), shop_id);
                }
            }
        }

        public void YahooFilterSKU(DataTable dtMainSku, DataTable dtMainItem, DataTable dtMainCategory, int shop_id)
        {
            #region Ctrl=n
            DataRow[] dr = dtMainItem.Select("[コントロールカラム]='n'");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("[コントロールカラム]='n'").CopyToDataTable();

                string filename = CreateFile(dtItem, "n", "data_add$", shop_id, 0, "_0_0.csv", "1.3.2.5.1"); //1.3.2.5.1
                foreach (DataRow drTmp in dtItem.Rows)
                {
                    SaveItemShopExportedCSVInfo(int.Parse(drTmp["ID"].ToString()), shop_id,filename);
                }
            }
            #endregion  

            #region Ctrl=u
            dr = dtMainItem.Select("[コントロールカラム]='u'");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("[コントロールカラム]='u'").CopyToDataTable();

                CreateFile(dtItem, "u", "data$", shop_id, 0, "_0_0.csv", "1.3.2.5.1"); //1.3.2.5.1
            }
            #endregion

            //YahooQuantity
            CreateFile(dtMainSku, "", "quantity$", shop_id, 2, "_0_0.csv", "1.3.2.5.2");

            //Change Ctrl_ID = @Ctrl_ID,Exhibition_Status = 1,API_Check=0,Batch_Check=0,Error_Check=0 accoding to shop id and item code from Item_Shop table
            if (dtMainItem != null && dtMainItem.Rows.Count > 0)
            {
                for (int i = 0; i < dtMainItem.Rows.Count; i++)
                {
                    ISBL.ChangeStatus(dtMainItem.Rows[i]["code"].ToString(), dtMainItem.Rows[i]["コントロールカラム"].ToString(), shop_id);
                }
            }
        }

        public void PonpareFilterSKU(DataTable dtMainSku, DataTable dtMainItem, DataTable dtMainCategory, int shop_id)
        {
            //For new series
            #region IsSKU=0

            DataRow[] dr = dtMainItem.Select("IsSKU=0");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("IsSKU=0").CopyToDataTable();
                //1.item.csv
                string filename = CreateFile(dtItem, "n", "item$", shop_id, 0, "_0_0.csv", "1.3.2.1.1"); //1.3.2.1.1

                foreach (DataRow drTmp in dtItem.Rows)
                {
                    SaveItemShopExportedCSVInfo(int.Parse(drTmp["ID"].ToString()), shop_id, filename);
                }
            }

            //Filter by shop id
            dr = dtMainSku.Select("IsSKU=0 AND Shop_ID=" + shop_id + "");
            if (dr.Count() > 0)
            {
                DataTable dtSku = dtMainSku.Select("IsSKU=0 AND Shop_ID=" + shop_id + "").CopyToDataTable();
                //2.option.csv
                CreateFile(dtSku, "n", "option$", shop_id, 2, "_0_0.csv", "1.3.2.6.2"); //1.3.2.1.2
            }

            dr = dtMainCategory.Select("IsSKU=0");
            if (dr.Count() > 0)
            {
                DataTable dtCategory = dtMainCategory.Select("IsSKU=0").CopyToDataTable();
                //3.category.csv
                CreateFile(dtCategory, "n", "category$", shop_id, 1, "_0_0.csv", "1.3.2.1.3"); //1.3.2.1.3
            }

            #endregion

            //For update series
            #region IsSKU=1
            dr = dtMainItem.Select("IsSKU=1");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("IsSKU=1").CopyToDataTable();

                DataTable dtEdit = dtItem.Copy();
                foreach (DataRow drEdit in dtEdit.Rows)
                {
                    drEdit["注文ボタン"] = 0;
                    drEdit["在庫タイプ"] = 3;
                }
                //1.item.csv
                CreateFile(dtEdit, "u", "item$#", shop_id, 0, "_1_1.csv", "1.3.2.9.1"); //1.3.2.9.1

                //2.item2.csv
                CreateFile(dtItem, "u", "item$", shop_id, 0, "_1_2.csv", "1.3.2.1.1"); //1.3.2.4.2
            }

            //dr = dtMainSku.Select("IsSKU=1 AND [コントロールカラム]='d' AND Shop_ID=" + shop_id + "");
            dr = dtMainSku.Select("IsSKU=1 AND Shop_ID=" + shop_id + "");
            if (dr.Count() > 0)
            {
                DataTable dtSku = dtMainSku.Select("IsSKU=1 AND Shop_ID=" + shop_id + "").CopyToDataTable();
                DataTable dtItem;
                
                dr = dtSku.Select("[選択肢タイプ]='s'");
                if (dr.Count() > 0)
                {
                    dtItem = dtSku.Select("[選択肢タイプ]='s'").CopyToDataTable();
                    //3.option.csv(item insert)
                    CreateFile(dtItem, "n", "option$", shop_id, 2, "_1_1.csv", "1.3.2.6.2"); //1.3.2.4.3
                }

                dr = dtSku.Select("[選択肢タイプ]='o'");
                if (dr.Count() > 0)
                {
                    dtItem = dtSku.Select("[選択肢タイプ]='o'").CopyToDataTable();
                    //4.option.csv(option delete)
                    CreateFile(dtItem, "d", "option$", shop_id, 2, "_1_2.csv", "1.3.2.6.2"); //1.3.2.4.4

                    //5.option.csv(option insert)
                    CreateFile(dtItem, "n", "option$", shop_id, 2, "_1_3.csv", "1.3.2.6.2"); //1.3.2.4.5
                }
            }

            dr = dtMainCategory.Select("IsSKU=1 AND [コントロールカラム]='d'");
            if (dr.Count() > 0)
            {
                DataTable dtCategory = dtMainCategory.Select("IsSKU=1").CopyToDataTable();
                //6.category.csv
                CreateFile(dtCategory, "d", "category$", shop_id, 1, "_1_1.csv", "1.3.2.1.3"); //1.3.2.4.6

                //7.category.csv
                CreateFile(dtCategory, "n", "category$", shop_id, 1, "_1_2.csv", "1.3.2.1.3"); //1.3.2.4.7
            }
            #endregion

            //Change Ctrl_ID = @Ctrl_ID,Exhibition_Status = 1,API_Check=0,Batch_Check=0,Error_Check=0 accoding to shop id and item code from Item_Shop table
            if (dtMainItem != null && dtMainItem.Rows.Count > 0)
            {
                for (int i = 0; i < dtMainItem.Rows.Count; i++)
                {
                    ISBL.ChangeStatus(dtMainItem.Rows[i]["商品ID"].ToString(), dtMainItem.Rows[i]["コントロールカラム"].ToString(), shop_id);
                }
            }
        }

        public void AmazonFilterSKU(DataTable dtMainSku, DataTable dtMainItem, DataTable dtMainCategory, int shop_id)
        {
            #region Ctrl=n
            DataRow[] dr = dtMainItem.Select("[コントロールカラム]='n'");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("[コントロールカラム]='n'").CopyToDataTable();
                string filename = CreateFile(dtItem, "n", "sku_add$", shop_id, 0, "_0_0.csv", "1.3.2.10.1"); //1.3.2.10.1
                foreach (DataRow drTmp in dtItem.Rows)
                {
                    SaveItemShopExportedCSVInfo(int.Parse(drTmp["ID"].ToString()), shop_id, filename);
                }
            }
            #endregion

            #region Ctrl=u
            dr = dtMainItem.Select("[コントロールカラム]='u'");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("[コントロールカラム]='u'").CopyToDataTable();
                CreateFile(dtItem, "u", "sku$", shop_id, 0, "_0_0.csv", "1.3.2.10.1"); //1.3.2.1.1
            }
            #endregion

            if (dtMainItem != null && dtMainItem.Rows.Count > 0)
            {
                for (int i = 0; i < dtMainItem.Rows.Count; i++)
                {
                    ISBL.ChangeStatus(dtMainItem.Rows[i]["sku"].ToString(), dtMainItem.Rows[i]["コントロールカラム"].ToString(), shop_id);
                }
            }
        }

        public void JiShaFilterSKU(DataTable dtMainSku, DataTable dtMainItem, DataTable dtMainCategory, int shop_id)
        {
            //For new
            #region IsSKU=0

            DataRow[] dr = dtMainItem.Select("IsSKU=0");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("IsSKU=0").CopyToDataTable();
                //for (int i = 0; i < dtItem.Rows.Count; i++)
                //{
                //    if (!string.IsNullOrWhiteSpace(dtItem.Rows[i]["PC用販売説明文"].ToString()))
                //    {
                //        dtItem.Rows[i]["PC用販売説明文"] = dtItem.Rows[i]["PC用販売説明文"].ToString().Replace("&lt;", "<").Replace("&gt;", ">");
                //    }
                //}

                //1.item.csv
                CreateFile(dtItem, "n", "item$", shop_id, 0, "_0_0.csv", "1.3.2.1.1"); //1.3.2.1.1
            }

            #region check shop id
            dr = dtMainSku.Select("IsSKU=0 AND Shop_ID=" + shop_id + "");
            #endregion
            //dr = dtMainSku.Select("IsSKU=0");
            if (dr.Count() > 0)
            {
                DataTable dtSku = dtMainSku.Select("IsSKU=0 AND Shop_ID=" + shop_id + "").CopyToDataTable();

                //2.item_select.csv
                CreateFile(dtSku, "n", "select$", shop_id, 2, "_0_0.csv", "1.3.2.1.2"); //1.3.2.1.2
            }

            dr = dtMainCategory.Select("IsSKU=0");
            if (dr.Count() > 0)
            {
                DataTable dtCategory = dtMainCategory.Select("IsSKU=0").CopyToDataTable();
                //3.item_cat.csv
                CreateFile(dtCategory, "n", "item-cat$", shop_id, 1, "_0_0.csv", "1.3.2.1.3"); //1.3.2.1.3
            }

            #endregion

            #region IsSKU=1
            dr = dtMainItem.Select("IsSKU=1");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("IsSKU=1").CopyToDataTable();

                //1.item.csv
                //CreateFile(dtItem, "u", "item$", shop_id, 0, "_1_1.csv", "1.3.2.4.1"); //1.3.2.4.1

                //2.item2.csv
                CreateFile(dtItem, "u", "item$", shop_id, 0, "_1_2.csv", "1.3.2.1.1"); //1.3.2.4.2
            }

            //dr = dtMainSku.Select("IsSKU=1 AND [コントロールカラム]='d' AND Shop_ID=" + shop_id + "");
            dr = dtMainSku.Select("IsSKU=1 AND Shop_ID=" + shop_id + "");
            if (dr.Count() > 0)
            {
                DataTable dtSku = dtMainSku.Select("IsSKU=1 AND Shop_ID=" + shop_id + "").CopyToDataTable();
                DataTable dtItem;
                
                dr = dtSku.Select("[選択肢タイプ]='i'");
                if (dr.Count() > 0)
                {
                    dtItem = dtSku.Select("[選択肢タイプ]='i'").CopyToDataTable();
                    //3.select.csv
                    CreateFile(dtItem, "n", "select$", shop_id, 2, "_1_1.csv", "1.3.2.1.2"); //1.3.2.4.3
                }

                dr = dtSku.Select("[選択肢タイプ]='s'");
                if (dr.Count() > 0)
                {
                    dtItem = dtSku.Select("[選択肢タイプ]='s'").CopyToDataTable();
                    //4.select.csv
                    CreateFile(dtItem, "d", "select$", shop_id, 2, "_1_2.csv", "1.3.2.1.2"); //1.3.2.4.4

                    //5.select.csv 
                    CreateFile(dtItem, "n", "select$", shop_id, 2, "_1_3.csv", "1.3.2.1.2"); //1.3.2.4.5
                }
            }

            dr = dtMainCategory.Select("IsSKU=1 AND [コントロールカラム]='d'");
            if (dr.Count() > 0)
            {
                DataTable dtCategory = dtMainCategory.Select("IsSKU=1").CopyToDataTable();
                //6.item_cat.csv
                CreateFile(dtCategory, "d", "item-cat$", shop_id, 1, "_1_1.csv", "1.3.2.1.3"); //1.3.2.4.6

                //7.item_cat.csv
                CreateFile(dtCategory, "n", "item-cat$", shop_id, 1, "_1_2.csv", "1.3.2.1.3"); //1.3.2.4.7
            }
            #endregion

            if (dtMainItem != null && dtMainItem.Rows.Count > 0)
            {
                for (int i = 0; i < dtMainItem.Rows.Count; i++)
                {
                    ISBL.ChangeStatus(dtMainItem.Rows[i]["商品番号"].ToString(), dtMainItem.Rows[i]["コントロールカラム"].ToString(), shop_id);
                }
            }
        }

        public string CreateFile(DataTable dt, String CtrlID, String firstName, int shop_id, int filetype, String extension, String fileNo)
        {
            String filename = "";
            if (!string.IsNullOrWhiteSpace(CtrlID))
            {
                DataRow[] dr = dt.Select("[コントロールカラム] = '" + CtrlID + "'");
                if (dr.Count() > 0)
                {
                    dt = dt.Select("[コントロールカラム] = '" + CtrlID + "'").CopyToDataTable();
                    DataTable dtCopy = dt.Copy();
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    filename = firstName + shop_id + "_" + date + extension;
                    dtCopy = FormatFile(dtCopy, fileNo);
                    GenerateCSV(dtCopy, filename);
                    SaveItem_ExportQ(filename, filetype, shop_id, 0,1);
                }
            }
            else
            {
                DataTable dtCopy = dt.Copy();
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                filename = firstName + shop_id + "_" + date + extension;
                dtCopy = FormatFile(dtCopy, fileNo);
                GenerateCSV(dtCopy, filename);
                SaveItem_ExportQ(filename, filetype, shop_id, 0,1);                
            }
            return filename;
        }

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

        public void SaveItem_ExportQ(string FileName, int FileType, int ShopID, int IsExport, int Export_Type)
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

        public DataTable FormatFile(DataTable dt, String fileNo)
        {
            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrWhiteSpace(fileNo))
            {
                switch (fileNo)
                {
                    case "1.3.2.1.1":  // item.csv (ctrl = n , u)
                        {
                            dt.Columns.Remove("ID");
                            dt.Columns.Remove("IsSKU");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.4.1": // item.csv (ctrl = u)
                        {
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("ID");
                            dt.Columns.Remove("サーチ非表示");
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
                    case "1.3.2.9.1": //item.csv(for ponpare mall) 1st update file
                        {
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("ID");
                            dt.Columns.Remove("販売ステータス");
                            dt.Columns.Remove("商品ID");
                            dt.Columns.Remove("商品名");
                            dt.Columns.Remove("キャッチコピー");
                            dt.Columns.Remove("販売価格");
                            dt.Columns.Remove("表示価格");
                            dt.Columns.Remove("消費税");
                            dt.Columns.Remove("送料");
                            dt.Columns.Remove("独自送料グループ(1)");
                            dt.Columns.Remove("独自送料グループ(2)");
                            dt.Columns.Remove("個別送料");
                            dt.Columns.Remove("代引料");
                            dt.Columns.Remove("のし対応");
                            dt.Columns.Remove("商品問い合わせボタン");
                            dt.Columns.Remove("販売期間指定");
                            dt.Columns.Remove("注文受付数");
                            dt.Columns.Remove("在庫数");
                            dt.Columns.Remove("在庫表示");
                            dt.Columns.Remove("商品説明(1)");
                            dt.Columns.Remove("商品説明(2)");
                            dt.Columns.Remove("商品説明(テキストのみ)");
                            dt.Columns.Remove("商品画像URL");
                            dt.Columns.Remove("モールジャンルID");
                            dt.Columns.Remove("シークレットセールパスワード");
                            dt.Columns.Remove("ポイント率");
                            dt.Columns.Remove("ポイント率適用期間");
                            dt.Columns.Remove("SKU横軸項目名");
                            dt.Columns.Remove("SKU縦軸項目名");
                            dt.Columns.Remove("SKU在庫用残り表示閾値");
                            dt.Columns.Remove("商品説明(スマートフォン用)");
                            dt.Columns.Remove("JANコード");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.1.2":  //select.csv (ctrl_ID = n , u , d)
                        {
                            dt.Columns.Remove("Shop_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("コントロールカラム");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.1.3":  //item_cat.csv (Ctrl_ID = n,u,d)
                        {
                            dt.Columns.Remove("IsSKU");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.6.2":  //option.csv (ctrl_ID = n , u , d)
                        {
                            dt.Columns.Remove("Shop_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.5.2": //Yahoo_quantity
                        {
                            break;
                        }
                    case "1.3.2.5.1"://Yahoo_data
                        {
                            dt.Columns.Remove("ID");
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("コントロールカラム");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.10.1"://Amazon_sku
                        {
                            dt.Columns.Remove("ID");
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("コントロールカラム");
                            dt.AcceptChanges();
                            break;
                        }

                }
            }
            return dt;
        }

        private void SaveItemShopExportedCSVInfo(int itemID, int shopID, string csvName)
        {
            ISBL.SaveItemShopExportedCSVInfo(itemID, shopID, csvName);
        }
    }
}