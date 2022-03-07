using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ORS_RCM_BL;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using ORS_RCM_Common;

namespace ORS_RCM.WebForms.Item
{
    public class Export_CSV_Delete
    {
        Item_BL ibl = new Item_BL();
        Item_Master_BL imbl = new Item_Master_BL();
        Item_Shop_BL ispBL = new Item_Shop_BL();

        DataTable dtImage = new DataTable();
        DataTable dtShopList = new DataTable();
        DataTable dtItemMaster = new DataTable();
        DataTable dtItemSelect = new DataTable();
        DataTable dtItemCat = new DataTable();

        public Export_CSV_Delete()
        {
        }

        public void CSV_Delete(string ItemIDList)
        {
            //imbl.ChangeCtrl_ID(ItemIDList); //Ctrl_ID=u  -> Ctrl_ID=d Item_Master , Ctrl_ID=n -> Ctrl_ID=d Item, Ctrl_ID=n -> Ctrl_ID=d Item_Category
            //string filename = string.Empty;
            dtShopList = imbl.GetShopList(ItemIDList);  // Get related ShopID List  of Items.
            if (dtShopList != null && dtShopList.Rows.Count > 0)
            {
                for (int i = 0; i < dtShopList.Rows.Count; i++)
                {
                    int shop_id = int.Parse(dtShopList.Rows[i]["Shop_ID"].ToString());

                    switch (dtShopList.Rows[i]["Mall_ID"].ToString())  //Check Mall
                    {
                        case "1":
                            RakutenDataDelete(shop_id, ItemIDList);
                            break;

                        case "2":
                            YahooDataDelete(shop_id, ItemIDList);
                            break;

                        case "3":
                            PonpareDataDelete(shop_id, ItemIDList);
                            break;
                    }

                    ispBL.ChangeCtrl_ID(ItemIDList, shop_id);//Change Ctrl_ID=d from Item_Shop Table And Item_Master Table
                }
            }
            //return filename;
        }

        public void CSV_DeleteforJisha(string ItemIDList)
        {
            JishaDataDelete(21, ItemIDList);
        }

        public void RakutenDataDelete(int shop_id, string ItemIDList)
        {
            // Get Item Info in Selected Item List by each shop ID
            string groupno = GetGroupNo();
            dtItemMaster = imbl.SelectByItemDataForRakuten(shop_id, ItemIDList, "itemdel");
            CreateDeleteFile(dtItemMaster, "item$", shop_id, 0, "_2_0_" + groupno + ".csv", 1);
        }

        public void YahooDataDelete(int shop_id, string ItemIDList)
        {

            dtItemMaster = imbl.SelectByItemDataForYahoo(ItemIDList, "data-del", shop_id);
            CreateDeleteFile(dtItemMaster, "data_del$", shop_id, 0, "_2_0.csv", 2);
            /*
            for (int i = 0; i < dtItemMaster.Rows.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(dtItemMaster.Rows[i]["sub-code"].ToString()))
                {
                    dtItemMaster.Rows[i]["sub-code"] = dtItemMaster.Rows[i]["sub-code"].ToString().Replace("&amp; ", "&");
                    dtItemMaster.Rows[i]["sub-code"] = dtItemMaster.Rows[i]["sub-code"].ToString().Substring(0, dtItemMaster.Rows[i]["sub-code"].ToString().Length - 5);
                }

                if (!string.IsNullOrWhiteSpace(dtItemMaster.Rows[i]["relevant-links"].ToString()))
                {
                    dtItemMaster.Rows[i]["relevant-links"] = dtItemMaster.Rows[i]["relevant-links"].ToString().Replace("&lt;", "<").Replace("&gt;", ">");
                }
            }
            //dtItemSelect = imbl.SelectByItemDataForYahoo(ItemIDList, "quantity");

            Export_CSV3 exportCSV3 = new Export_CSV3();
            DataTable dtModified = exportCSV3.ModifyTable(dtItemMaster, shop_id);
            if (dtModified != null)
            {
                dtItemMaster = dtModified;
            }

            //delete Columns that is not contained in CSV file
            dtItemMaster.Columns.Remove("IsSKU");
            dtItemMaster.Columns.Remove("コントロールカラム");
            dtItemMaster.AcceptChanges();

            CreateDeleteFile(dtItemMaster, "data_del$", shop_id, 0, "_2_0.csv");
             */
        }

        public void PonpareDataDelete(int shop_id, string ItemIDList)
        {
            // Get Item Info in Selected Item List by each shop ID
            string groupno = GetGroupNo();
            dtItemMaster = imbl.SelectByItemDataForPonpare(shop_id, ItemIDList, "itemdel");
            CreateDeleteFile(dtItemMaster, "item$", shop_id, 0, "_2_0_" + groupno + ".csv", 3);
        }

        public void AmazonDataDelete(int shop_id, string ItemIDList)
        {
            //dtItemMaster = imbl.SelectByItemDataForAmazon(ItemIDList);
            //CreateDeleteFile(dtItemMaster, "sku_delete$", shop_id, 0, "_2_0.csv");
        }

        public void JishaDataDelete(int shop_id, string ItemIDList)
        {
            // Get Item Info in Selected Item List by each shop ID
            dtItemMaster = imbl.SelectByItemDataForRakuten(shop_id, ItemIDList, "itemdel");
            if (dtItemMaster != null && dtItemMaster.Rows.Count > 0)
            {
                CreateDeleteFile(dtItemMaster, "item$", shop_id, 0, "_2_0.csv", 5);
            }
        }

        public void ChangeFlag(int SKU_Flag, string Item_Code, string Ctrl_ID)
        {
            if (dtItemSelect != null && dtItemSelect.Rows.Count > 0)
            {
                foreach (DataRow dr in dtItemSelect.Rows)
                {
                    if (dr["商品管理番号（商品URL）"].ToString() == Item_Code)
                    {
                        dr["IsSKU"] = SKU_Flag;
                        dr["コントロールカラム"] = Ctrl_ID;
                        dr["項目選択肢用コントロールカラム"] = Ctrl_ID;
                    }
                }
            }

            if (dtItemCat != null && dtItemCat.Rows.Count > 0)
            {
                foreach (DataRow dr in dtItemCat.Rows)
                {
                    if (dr["商品管理番号（商品URL）"].ToString() == Item_Code)
                    {
                        dr["IsSKU"] = SKU_Flag;
                    }
                }
            }

        }

        public void CreateDeleteFile(DataTable dt, String firstName, int shop_id, int filetype, String extension, int mall_id)
        {
            String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            String filename = firstName + shop_id + "_" + date + extension;
            DataTable dtCopy = dt.Copy();
            //SaveExhibitionData(dt, shop_id, filename);
            GenerateCSV(dtCopy, filename, mall_id);
            //previous condition "SaveItem_ExportQ(filename, filetype, shop_id, 0 , 1); "
            //Place IsExport=2 coz do not want to upload yet.(22.07.2015)
            SaveItem_ExportQ(filename, filetype, shop_id, 2, 1);
            foreach (DataRow drTmp in dt.Rows)
            {
                SaveItemShopExportedCSVInfo(int.Parse(drTmp["ID"].ToString()), shop_id, filename);
            }
            //return filename;
        }

        private void SaveItemShopExportedCSVInfo(int itemID, int shopID, string csvName)
        {
            ispBL.SaveItemShopExportedCSVInfo(itemID, shopID, csvName);
        }

        public void GenerateCSV(DataTable dtInformation, string FileName, int mall_id)
        {
            string ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
            string BakExportCSVPath = ConfigurationManager.AppSettings["BakExportCSVPath"].ToString();
            dtInformation.Columns.Remove("ID");
            dtInformation.Columns.Remove("Exhibit_ID");
            dtInformation.AcceptChanges();
            switch (mall_id.ToString())
            {
                case "1":
                    using (StreamWriter writer = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath(ExportCSVPath + "/Rakuten/" + FileName), false, Encoding.GetEncoding(932)))
                    {
                        WriteDataTable(dtInformation, writer, true);
                    }
                    using (StreamWriter writer = new StreamWriter(BakExportCSVPath + "\\Rakuten\\" + FileName, false, Encoding.GetEncoding(932)))
                    {
                        WriteDataTable(dtInformation, writer, true);
                    }
                    break;
                case "2":
                    using (StreamWriter writer = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath(ExportCSVPath + "/Yahoo/" + FileName), false, Encoding.GetEncoding(932)))
                    {
                        WriteDataTable(dtInformation, writer, true);
                    }
                    using (StreamWriter writer = new StreamWriter(BakExportCSVPath + "\\Yahoo\\" + FileName, false, Encoding.GetEncoding(932)))
                    {
                        WriteDataTable(dtInformation, writer, true);
                    }
                    break;
                case "3":
                    using (StreamWriter writer = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath(ExportCSVPath + "/Jisha/" + FileName), false, Encoding.GetEncoding(932)))
                    {
                        WriteDataTable(dtInformation, writer, true);
                    }
                    using (StreamWriter writer = new StreamWriter(BakExportCSVPath + "\\Jisha\\" + FileName, false, Encoding.GetEncoding(932)))
                    {
                        WriteDataTable(dtInformation, writer, true);
                    }
                    break;
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

        public string GetGroupNo()
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

        #region Comment
        //dtItemSelect = imbl.SelectByItemDataForRakuten(shop_id, ItemIDList, "itemselect");
        //dtItemCat = imbl.SelectByItemDataForRakuten(shop_id, ItemIDList, "itemcat");

        //for (int j = 0; j < dtItemMaster.Rows.Count; j++)
        //{
        //    dtItemMaster.Rows[j]["コントロールカラム"] = "d";
        //    //ChangeFlag(Convert.ToInt32(dtItemMaster.Rows[j]["IsSKU"]), dtItemMaster.Rows[j]["商品番号"].ToString(), dtItemMaster.Rows[j]["コントロールカラム"].ToString());
        //}

        //Export_CSV3 exportCSV3 = new Export_CSV3();
        //DataTable dtModified = exportCSV3.ModifyTable(dtItemMaster, shop_id);
        //if (dtModified != null)
        //{
        //    dtItemMaster = dtModified;
        //}

        //delete Columns that is not contained in CSV file
        //dtItemMaster.Columns.Remove("IsSKU");
        //dtItemMaster.Columns.Remove("ID");
        //dtItemMaster.Columns.Remove("商品番号");
        //dtItemMaster.Columns.Remove("全商品ディレクトリID");
        //dtItemMaster.Columns.Remove("タグID");
        //dtItemMaster.Columns.Remove("PC用キャッチコピー");
        //dtItemMaster.Columns.Remove("モバイル用キャッチコピー");
        //dtItemMaster.Columns.Remove("商品名");
        //dtItemMaster.Columns.Remove("販売価格");
        //dtItemMaster.Columns.Remove("表示価格");
        //dtItemMaster.Columns.Remove("消費税");
        //dtItemMaster.Columns.Remove("送料");
        //dtItemMaster.Columns.Remove("個別送料");
        //dtItemMaster.Columns.Remove("送料区分1");
        //dtItemMaster.Columns.Remove("送料区分2");
        //dtItemMaster.Columns.Remove("代引料");
        //dtItemMaster.Columns.Remove("倉庫指定");
        //dtItemMaster.Columns.Remove("商品情報レイアウト");
        //dtItemMaster.Columns.Remove("注文ボタン");
        //dtItemMaster.Columns.Remove("資料請求ボタン");
        //dtItemMaster.Columns.Remove("商品問い合わせボタン");
        //dtItemMaster.Columns.Remove("再入荷お知らせボタン");
        //dtItemMaster.Columns.Remove("モバイル表示");
        //dtItemMaster.Columns.Remove("のし対応");
        //dtItemMaster.Columns.Remove("PC用商品説明文");
        //dtItemMaster.Columns.Remove("モバイル用商品説明文");
        //dtItemMaster.Columns.Remove("スマートフォン用商品説明文");
        //dtItemMaster.Columns.Remove("PC用販売説明文");
        //dtItemMaster.Columns.Remove("商品画像URL");
        //dtItemMaster.Columns.Remove("商品画像名（ALT）");
        //dtItemMaster.Columns.Remove("動画");
        //dtItemMaster.Columns.Remove("販売期間指定");
        //dtItemMaster.Columns.Remove("注文受付数");
        //dtItemMaster.Columns.Remove("在庫タイプ");
        //dtItemMaster.Columns.Remove("在庫数");
        //dtItemMaster.Columns.Remove("在庫数表示");
        //dtItemMaster.Columns.Remove("項目選択肢別在庫用横軸項目名");
        //dtItemMaster.Columns.Remove("項目選択肢別在庫用縦軸項目名");
        //dtItemMaster.Columns.Remove("項目選択肢別在庫用残り表示閾値");
        //dtItemMaster.Columns.Remove("RAC番号");
        //dtItemMaster.Columns.Remove("サーチ非表示");
        //dtItemMaster.Columns.Remove("闇市パスワード");
        //dtItemMaster.Columns.Remove("カタログID");
        //dtItemMaster.Columns.Remove("在庫戻しフラグ");
        //dtItemMaster.Columns.Remove("在庫切れ時の注文受付");
        //dtItemMaster.Columns.Remove("在庫あり時納期管理番号");
        //dtItemMaster.Columns.Remove("在庫切れ時納期管理番号");
        //dtItemMaster.Columns.Remove("予約商品発売日");
        //dtItemMaster.Columns.Remove("ポイント変倍率");
        //dtItemMaster.Columns.Remove("ポイント変倍率適用期間");
        //dtItemMaster.Columns.Remove("ヘッダー・フッター・レフトナビ");
        //dtItemMaster.Columns.Remove("表示項目の並び順");
        //dtItemMaster.Columns.Remove("共通説明文（小）");
        //dtItemMaster.Columns.Remove("目玉商品");
        //dtItemMaster.Columns.Remove("共通説明文（大）");
        //dtItemMaster.Columns.Remove("レビュー本文表示");
        //dtItemMaster.Columns.Remove("あす楽配送管理番号");
        //dtItemMaster.Columns.Remove("サイズ表リンク");
        //dtItemMaster.Columns.Remove("二重価格文言管理番号");
        //dtItemMaster.AcceptChanges();


        //    CreateDeleteFile(dtItemSelect, "item_select-", shop_id, 1, "_2_1.csv");
        //    CreateDeleteFile(dtItemCat, "item_cat-", shop_id, 2, "_2_2.csv");
        #endregion

    }
}