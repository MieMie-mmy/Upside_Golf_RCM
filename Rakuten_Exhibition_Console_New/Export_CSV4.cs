using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Configuration;
using ORS_RCM_Common;
using ORS_RCM_BL;
using Ionic.Zip;
using System.Data;
using System.Data.SqlClient;

namespace Rakuten_Exhibition_Console_New
{
    public class Export_CSV4
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static Exhibition_List_BL exhibitBL = new Exhibition_List_BL();
        static Log_Exhibition_BL log = new Log_Exhibition_BL();
        String ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        String BakExportCSVPath = ConfigurationManager.AppSettings["BakExportCSVPath"].ToString();
        String ItemImage = ConfigurationManager.AppSettings["ItemImage"].ToString();
        String ConsoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        static DataTable dtItem1, dtItem;

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

        public void RakutenFilterSKU(DataTable dtImage, DataTable dtMainSku, DataTable dtMainItem, DataTable dtMainCategory, int shop_id)
        {
            string groupno = null;
            bool groupflag = false;

            //For Image Zip
            #region Image
            //CreateRakutenUpoladImage(dtImage, shop_id);
            if (dtImage != null && dtImage.Rows.Count > 0)
            {
                groupno = GetGroupNo();
                groupflag = true;

                string path = "";
                string folderName = "";
                string image_name = "";
                string sku = ExportCSVPath + shop_id + "/mem_item/";

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
                        String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                        zipfile.AddDirectory(sku, "mem_item");
                        zipfile.Save(ExportCSVPath + shop_id + "/" + folderName + "$" + shop_id + "_" + date + "_" + groupno + ".zip");
                        zipfile.Save(BakExportCSVPath + folderName + "$" + shop_id + "_" + date + "_" + groupno + ".zip");
                        SaveItem_ExportQ(folderName + "$" + shop_id + "_" + date + "_" + groupno + ".zip", 3, shop_id, 0, 1);
                    }
                    DeleteDirectory(sku, true);
                }
            }
            #endregion

            //For CSV
            #region Item
            if (groupflag == false)
            {
                groupno = GetGroupNo();
            }
            DataRow[] dr = dtMainItem.Select("IsSKU=1");
            if (dr.Count() > 0)
            {
                dtItem = dtMainItem.Select("IsSKU=1").CopyToDataTable();
                //倉庫指定=1, 在庫タイプ=0 change by csv format 
                DataTable dtEdit = dtItem.Copy();
                foreach (DataRow drEdit in dtEdit.Rows)
                {
                    drEdit["倉庫指定"] = 1;
                    drEdit["在庫タイプ"] = 1;
                    drEdit["在庫数"] = 0;
                }
                //1.item.csv
                CreateFile(dtEdit, "u", "item$", shop_id, 0, "_1_1_" + groupno + ".csv", "1.3.2.4.1"); //1.3.2.4.1
            }
            //2.item2.csv
            string filename = CreateFile(dtMainItem, "u", "item$", shop_id, 0, "_1_2_" + groupno + ".csv", "1.3.2.1.1"); //1.3.2.4.2
            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                foreach (DataRow drTmp in dtItem.Rows)
                {
                    SaveItemShopExportedCSVInfo(int.Parse(drTmp["Exhibit_ID"].ToString()), shop_id, filename, "u");
                }
            }
            DataRow[] dr1 = dtMainItem.Select("IsSKU=0");
            if (dr1.Count() > 0)
            {
                dtItem1 = dtMainItem.Select("IsSKU=0").CopyToDataTable();
            }
            if (dtItem1 != null && dtItem1.Rows.Count > 0)
            {
                foreach (DataRow drTmp in dtItem1.Rows)
                {
                    SaveItemShopExportedCSVInfo(int.Parse(drTmp["Exhibit_ID"].ToString()), shop_id, filename, "n");
                }
            }
            #endregion

            #region Select
            dr = dtMainSku.Select("IsSKU=1");
            if (dr.Count() > 0)
            {
                DataTable dtSku = dtMainSku.Select("IsSKU=1").CopyToDataTable();
                // added by ETZ for sks-500
                DataView dv = new DataView(dtSku);
                DataTable dtItemSearchForSKU = dv.ToTable(true, "商品管理番号（商品URL）");
                dtItemSearchForSKU.Columns["商品管理番号（商品URL）"].ColumnName = "Item_Code";
                DataTable dtItemCodeDaily_Dev = CollectItem(dtItemSearchForSKU, 2);
                if (dtItemCodeDaily_Dev.Rows.Count > 0)
                {
                    for (int i = 0; i < dtItemCodeDaily_Dev.Rows.Count; i++)
                    {
                        DataRow row = dtMainSku.NewRow();
                        row["コントロールカラム"] = "n";
                        row["項目選択肢用コントロールカラム"] = "n";
                        row["商品管理番号（商品URL）"] = dtItemCodeDaily_Dev.Rows[i]["Item_Code"].ToString();
                        row["選択肢タイプ"] = "s";
                        row["Select/Checkbox用項目名"] = "即日出荷";
                        row["Select/Checkbox用選択肢"] = "対象商品";
                        dtMainSku.Rows.Add(row);
                        dtMainSku.AcceptChanges();
                    }
                }

                //3.item_select.csv (選択肢タイプ=s)
                DataTable dtSelect;
                dr = dtSku.Select("[選択肢タイプ]='s'");
                if (dr.Count() > 0)
                {
                    dtSelect = dtSku.Select("[選択肢タイプ]='s'").CopyToDataTable();
                    DataRow[] drow = dtSelect.Select("[コントロールカラム] ='d'");
                    if (drow.Count() > 0)
                    {
                        DataTable dt = dtSelect.Select("[コントロールカラム] ='d'").CopyToDataTable();
                        CreateFile(dt, "d", "select$", shop_id, 2, "_1_1_" + groupno + ".csv", "1.3.2.1.2"); //1.3.2.4.4
                    }
                }
            }
            //4.item_select.csv (選択肢タイプ=i  and 選択肢タイプ=s)

            DataRow[] drselect = dtMainSku.Select("[コントロールカラム] ='n'");
            if (drselect.Count() > 0)
            {
                DataTable dt = dtMainSku.Select("[コントロールカラム] ='n'").CopyToDataTable();
                CreateFile(dt, "n", "select$", shop_id, 2, "_1_2_" + groupno + ".csv", "1.3.2.1.2"); //1.3.2.4.5
            }

            #endregion

            #region Category
            dr = dtMainCategory.Select("IsSKU=1");
            if (dr.Count() > 0)
            {
                DataTable dtCategory = dtMainCategory.Select("IsSKU=1").CopyToDataTable();
                // added by ETZ for sks-500
                DataView dv = new DataView(dtCategory);
                DataTable dtItemSearch = dv.ToTable(true, "商品管理番号（商品URL）");
                dtItemSearch.Columns["商品管理番号（商品URL）"].ColumnName = "Item_Code";
                DataTable dtItemCodeSelect = CollectItem(dtItemSearch, 1);
                if (dtItemCodeSelect.Rows.Count > 0)
                {
                    for (int i = 0; i < dtItemCodeSelect.Rows.Count; i++)
                    {
                        if (dtMainCategory.Columns.Contains("URL"))
                        {
                            DataRow row = dtMainCategory.NewRow();
                            row["コントロールカラム"] = "n";
                            row["商品管理番号（商品URL）"] = dtItemCodeSelect.Rows[i]["Item_Code"].ToString();
                            row["商品名"] = dtItemCodeSelect.Rows[i]["Item_Code"].ToString(); 
                            row["表示先カテゴリ"] = "PICK UP！" + "\\" + "即日出荷対象商品";
                            row["優先度"] = "1";
                            row["URL"] = "";
                            row["1ページ複数形式"] = "";
                            dtMainCategory.Rows.Add(row);
                            dtMainCategory.AcceptChanges();
                        }
                        else
                        {
                            DataRow row = dtMainCategory.NewRow();
                            row["コントロールカラム"] = "n";
                            row["商品管理番号（商品URL）"] = dtItemCodeSelect.Rows[i]["Item_Code"].ToString(); ;
                            row["表示先カテゴリ"] = "PICK UP！" + "\\" + "即日出荷対象商品";
                            row["優先度"] = "1";
                            row["1ページ複数形式"] = "";
                            row["カテゴリセット管理番号"] = "26";
                            row["カテゴリセット名"] = "";
                            dtMainCategory.Rows.Add(row);
                            dtMainCategory.AcceptChanges();
                        }
                    }
                }
                DataTable dtcatno = new DataTable();
                DataTable dtnocat = new DataTable();
                if (dtMainCategory.Columns.Contains("URL"))
                {
                    DataRow[] drnocat = dtCategory.Select("[コントロールカラム] ='d'");
                    if (drnocat.Count() > 0)
                    {
                        dtnocat = dtCategory.Select("[コントロールカラム] ='d'").CopyToDataTable();
                    }
                }
                else
                {
                    DataRow[] drcatno = dtCategory.Select("[コントロールカラム] ='d'");
                    if (drcatno.Count() > 0)
                    {
                        dtcatno = dtCategory.Select("[コントロールカラム] ='d'").CopyToDataTable();
                    }
                }
                if (dtnocat != null && dtnocat.Rows.Count > 0)
                {
                    CreateFile(dtnocat, "d", "item-cat$", shop_id, 1, "_1_1_" + groupno + ".csv", "1.3.2.2.4");
                }
                if (dtcatno != null && dtcatno.Rows.Count > 0)
                {
                    do
                    {
                        string cat_no = dtcatno.Rows[0]["カテゴリセット管理番号"].ToString();
                        DataTable dtTemp = dtcatno.Select("[カテゴリセット管理番号] = '" + cat_no + "'").CopyToDataTable();
                        DataRow[] drcat = dtTemp.Select("[コントロールカラム] ='d'");
                        if (drcat.Count() > 0)
                        {
                            DataTable dt = dtTemp.Select("[コントロールカラム] ='d'").CopyToDataTable();
                            //6.item_cat.csv
                            CreateFile(dt, "d", "item-cat$", shop_id, 1, "_1_1_" + cat_no + "_" + groupno + ".csv", "1.3.2.1.4"); //1.3.2.4.6
                        }
                        #region delete row
                        List<DataRow> rows_to_remove = new List<DataRow>();
                        foreach (DataRow row1 in dtcatno.Rows)
                        {
                            if (row1["カテゴリセット管理番号"].ToString() == cat_no)
                            {
                                rows_to_remove.Add(row1);
                            }
                        }

                        foreach (DataRow row in rows_to_remove)
                        {
                            dtcatno.Rows.Remove(row);
                            dtcatno.AcceptChanges();
                        }
                        #endregion
                    } while (dtcatno.Rows.Count > 0);
                }
            }
            DataTable dtcatno1 = new DataTable();
            DataTable dtnocat1 = new DataTable();
            if (dtMainCategory.Columns.Contains("URL"))
            {
                DataRow[] drnocat1 = dtMainCategory.Select("[コントロールカラム] ='n'");
                if (drnocat1.Count() > 0)
                {
                    dtnocat1 = dtMainCategory.Select("[コントロールカラム] ='n'").CopyToDataTable();
                }
            }
            else
            {
                DataRow[] drcatno1 = dtMainCategory.Select("[コントロールカラム] ='n'");
                if (drcatno1.Count() > 0)
                {
                    dtcatno1 = dtMainCategory.Select("[コントロールカラム] ='n'").CopyToDataTable();
                }
            }
            if(dtcatno1!=null && dtcatno1.Rows.Count>0)
            {
                do
                {
                    string cat_no = dtcatno1.Rows[0]["カテゴリセット管理番号"].ToString();
                    DataTable dtTemp = dtcatno1.Select("[カテゴリセット管理番号] = '" + cat_no + "'").CopyToDataTable();
                        //7.item_cat.csv
                        DataRow[] drcat = dtTemp.Select("[コントロールカラム] ='n'");
                        if (drcat.Count() > 0)
                        {
                            DataTable dt = dtTemp.Select("[コントロールカラム] ='n'").CopyToDataTable();
                            CreateFile(dt, "n", "item-cat$", shop_id, 1, "_1_2_" + cat_no + "_" + groupno + ".csv", "1.3.2.1.3"); //1.3.2.4.7
                        }
                        #region delete row
                        List<DataRow> rows_to_remove = new List<DataRow>();
                        foreach (DataRow row1 in dtcatno1.Rows)
                        {
                            if (row1["カテゴリセット管理番号"].ToString() == cat_no)
                            {
                                rows_to_remove.Add(row1);
                            }
                        }

                        foreach (DataRow row in rows_to_remove)
                        {
                            dtcatno1.Rows.Remove(row);
                            dtcatno1.AcceptChanges();
                        }
                } while (dtcatno1.Rows.Count > 0);
                    #endregion
            }
            if (dtnocat1 != null && dtnocat1.Rows.Count > 0)
            {
                CreateFile(dtnocat1, "n", "item-cat$", shop_id, 1, "_1_2_" + groupno + ".csv", "1.3.2.2.3"); //1.3.2.4.7
            }
            #endregion
            //ChangeLogExhibitionFlag
            if (dtMainItem != null)
            {
                foreach (DataRow drFlag in dtMainItem.Rows)
                {
                    ChangeIsGeneratedCSVFlag(int.Parse(drFlag["Exhibit_ID"].ToString()), int.Parse(drFlag["Item_ID"].ToString()), shop_id);
                }
            }
        }

        static void ChangeIsGeneratedCSVFlag(int Exhibit_ID, int Item_ID, int Shop_ID)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_ChangeFlag_ByMall", connectionstring);
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

        public string CreateFile(DataTable dt, String CtrlID, String firstName, int shop_id, int filetype, String extension, String fileNo)
        {
            String filename = "";
            if (!string.IsNullOrWhiteSpace(CtrlID))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataTable dtCopy = dt.Copy();
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    filename = firstName + shop_id + "_" + date + extension;
                    dtCopy = FormatFile(dtCopy, fileNo);
                    GenerateCSV(dtCopy, filename);
                    ConsoleWriteLine_Tofile("File Name : " + filename, shop_id);
                    SaveItem_ExportQ(filename, filetype, shop_id, 0, 1);
                }
            }
            return filename;
        }

        public static DataTable CollectItem(DataTable dtCopy, int filetype)
        {
            DataTable dtItemCodeList = exhibitBL.CollectItem(dtCopy, filetype);
            return dtItemCodeList;
        }

        public void GenerateCSV(DataTable dtInformation, string FileName)
        {
            using (StreamWriter writer = new StreamWriter(ExportCSVPath + FileName, false, Encoding.GetEncoding(932)))
            {
                WriteDataTable(dtInformation, writer, true);
            }
            using (StreamWriter writer = new StreamWriter(BakExportCSVPath + FileName, false, Encoding.GetEncoding(932)))
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
                            dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("Item_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.4.1": // item.csv (ctrl = u)
                        {
                            dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("Item_ID");
                            dt.Columns.Remove("IsSKU");
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
                            //dt.Columns.Remove("在庫数");
                            dt.Columns.Remove("在庫数表示");
                            dt.Columns.Remove("項目選択肢別在庫用横軸項目名");
                            dt.Columns.Remove("項目選択肢別在庫用縦軸項目名");
                            dt.Columns.Remove("項目選択肢別在庫用残り表示閾値");
                            dt.Columns.Remove("RAC番号");
                            dt.Columns.Remove("サーチ非表示");
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
                    case "1.3.2.1.2":  //select.csv (ctrl_ID = n , u , d)
                        {
                            dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("ID");
                            dt.Columns.Remove("コントロールカラム");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.1.3":  //item_cat.csv (Ctrl_ID = n with catno)
                        {
                            dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.1.4":  //item_cat.csv (Ctrl_ID = d with catno)
                        {
                            dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("優先度");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.2.4": //item_cat.csv (Ctrl_ID=d without catno)
                        {
                            dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("優先度");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.2.3":    //item_cat.csv (Ctrl_ID = n without catno)
                        {
                            dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.AcceptChanges();
                            break;
                        }
                }
            }
            return dt;
        }

        public void ConsoleWriteLine_Tofile(string traceText, int shop_id)
        {
            StreamWriter sw = new StreamWriter(ConsoleWriteLinePath + "RakutenExhibition_ConsoleWriteLine" + shop_id + ".txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        private void SaveItemShopExportedCSVInfo(int itemID, int shopID, string csvName, string ctrl_id)
        {
            exhibitBL.SaveExhibitionItemShopExportedCSVInfo(itemID, shopID, csvName, ctrl_id);
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
    }
}