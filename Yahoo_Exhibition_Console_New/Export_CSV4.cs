using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Text;
using ORS_RCM_Common;
using ORS_RCM_BL;
using System.Data;
using Ionic.Zip;
using System.Data;
using System.Data.SqlClient;

namespace Yahoo_Exhibition_Console_New
{
    public class Export_CSV4
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        String ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        String BakExportCSVPath = ConfigurationManager.AppSettings["BakExportCSVPath"].ToString();
        String sku = ConfigurationManager.AppSettings["SKUPath"].ToString();
        String ItemImage = ConfigurationManager.AppSettings["ItemImage"].ToString();
        Exhibition_List_BL exhibitBL = new Exhibition_List_BL();
        Log_Exhibition_BL log = new Log_Exhibition_BL();

        public void YahooFilterSKU(DataTable dtImage, DataTable dtMainSku, DataTable dtMainItem, DataTable dtMainCategory, int shop_id)
        {
            if (dtMainItem != null && dtMainItem.Rows.Count > 0)
            {
                for (int j = 0; j < dtMainItem.Rows.Count; j++)
                {
                    //Filter for Image
                    CreateUploadImage(dtImage, dtMainItem.Rows[j]["code"].ToString());
                }
                ImageZip(shop_id);
            }

            #region Ctrl=n
            DataRow[] dr = dtMainItem.Select("[コントロールカラム]='n'");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("[コントロールカラム]='n'").CopyToDataTable();

                string filename = CreateFile(dtItem, "n", "data_add$", shop_id, 0, "_0_0.csv", "1.3.2.5.1"); //1.3.2.5.1
                foreach (DataRow drTmp in dtItem.Rows)
                {
                    SaveItemShopExportedCSVInfo(int.Parse(drTmp["Exhibit_ID"].ToString()), shop_id, filename, "n");
                }
            }
            #endregion

            #region Ctrl=u
            dr = dtMainItem.Select("[コントロールカラム]='u'");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("[コントロールカラム]='u'").CopyToDataTable();

                string filename = CreateFile(dtItem, "u", "data_add$", shop_id, 0, "_1_0.csv", "1.3.2.5.1"); //1.3.2.5.1

                foreach (DataRow drTmp in dtItem.Rows)
                {
                    SaveItemShopExportedCSVInfo(int.Parse(drTmp["Exhibit_ID"].ToString()), shop_id, filename, "u");
                }
            }
            #endregion

            //YahooQuantity
            if (dtMainSku != null && dtMainSku.Rows.Count > 0)
            {
                CreateFile(dtMainSku, "", "quantity$", shop_id, 2, "_0_0.csv", "1.3.2.5.2");
            }

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

        public void CreateUploadImage(DataTable dt, string Item_Code)
        {
            //Create new folder for upload image
            //String folderpath = ConfigurationManager.AppSettings["ImagePath"].ToString();
            String folderpath = ExportCSVPath + "\\Image\\";
            if (!Directory.Exists(folderpath))
                Directory.CreateDirectory(folderpath);

            //String folder2path = ConfigurationManager.AppSettings["LibraryPath"].ToString();
            String folder2path = ExportCSVPath + "\\LibImage\\";
            if (!Directory.Exists(folder2path))
                Directory.CreateDirectory(folder2path);

            //Upload Image
            DataRow[] dr;

            #region Image
            dr = dt.Select("Item_Code='" + Item_Code + "' AND Image_Type=0");
            if (dr.Count() > 0)
            {
                DataTable dtImage = dt.Select("Item_Code='" + Item_Code + "' AND Image_Type=0").CopyToDataTable();
                for (int i = 0; i < dtImage.Rows.Count; i++)
                {

                    string str = ItemImage + dtImage.Rows[i]["Image_Name"].ToString();

                    //Save image into folder
                    if (Directory.Exists(folderpath))
                    {
                        if (File.Exists(str))
                        {
                            if (!File.Exists(folderpath + dtImage.Rows[i]["Image_Name"].ToString()))
                            {
                                if (i == 0)
                                    File.Copy(str, folderpath + dtImage.Rows[i]["Item_Code"].ToString() + ".jpg");
                                else
                                    File.Copy(str, folderpath + dtImage.Rows[i]["Item_Code"].ToString() + "_" + i + ".jpg");
                            }
                        }
                    }
                }
            }
            #endregion

            #region Library Image
            dr = dt.Select("Item_Code='" + Item_Code + "' AND Image_Type=1");
            if (dr.Count() > 0)
            {
                DataTable dtImage = dt.Select("Item_Code='" + Item_Code + "' AND Image_Type=1").CopyToDataTable();
                for (int i = 0; i < dtImage.Rows.Count; i++)
                {
                    string str = ItemImage + dtImage.Rows[i]["Image_Name"].ToString();

                    //Save image into folder
                    if (Directory.Exists(folder2path))
                    {
                        if (File.Exists(str))
                        {
                            if (!File.Exists(folder2path + dtImage.Rows[i]["Image_Name"].ToString()))
                                File.Copy(str, folder2path + dtImage.Rows[i]["Image_Name"].ToString());
                        }
                    }
                }
            }
            #endregion

        }

        public void ImageZip(int shop_id)
        {
            //Image Zip
            string date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            String pathName = ExportCSVPath + "\\Image\\";
            string[] fileNames = Directory.GetFiles(pathName);

            if (fileNames != null && fileNames.Length > 0)
            {
                using (ZipFile zipfile = new ZipFile())
                {
                    zipfile.AddFiles(fileNames, "");
                    zipfile.Save(ExportCSVPath + "img$" + shop_id + "_" + date + ".zip");
                    zipfile.Save(BakExportCSVPath + "img$" + shop_id + "_" + date + ".zip");
                    SaveItem_ExportQ("img$" + shop_id + "_" + date + ".zip", 3, shop_id, 0, 1);
                }
            }

            //Library Image Zip
            pathName = ExportCSVPath + "\\LibImage\\";
            fileNames = Directory.GetFiles(pathName);
            if (fileNames != null && fileNames.Length > 0)
            {
                using (ZipFile zipfile = new ZipFile())
                {
                    zipfile.AddFiles(fileNames, "");
                    zipfile.Save(ExportCSVPath + "lib_img$" + shop_id + "_" + date + ".zip");
                    zipfile.Save(BakExportCSVPath + "lib_img$" + shop_id + "_" + date + ".zip");
                    SaveItem_ExportQ("lib_img$" + shop_id + "_" + date + ".zip", 3, shop_id, 0, 1);
                }
            }

            DeleteFilesFromDirectory();
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
                    // added by ETZ for sks-500
                    DataView dv = new DataView(dtCopy);
                    DataTable dtItemSelect = dv.ToTable(true, "code");
                    dtItemSelect.Columns["code"].ColumnName = "Item_Code";

                    if (fileNo == "1.3.2.5.1" && extension.Contains("_1_0.csv"))
                    {
                        DataTable dtItemSelect1 = exhibitBL.CollectItemCode_Yahoo(dtItemSelect, filetype);
                        for (int j = 0; j < dtItemSelect1.Rows.Count; j++)
                        {
                            for (int i = 0; i < dtCopy.Rows.Count; i++)
                            {
                                if (dtCopy.Rows[i]["code"].ToString() == dtItemSelect1.Rows[j]["Item_Code"].ToString())
                                {

                                    dtCopy.Rows[i]["path"] = dtCopy.Rows[i]["path"].ToString() + "\nPICK UP！" + "\\" + "即日出荷対象商品";
                                    dtCopy.Rows[i]["options"] = dtCopy.Rows[i]["options"].ToString() + "\n\n即日出荷 対象商品";
                                    dtCopy.AcceptChanges();
                                }
                            }
                        }
                    }
                    GenerateCSV(dtCopy, filename);
                    SaveItem_ExportQ(filename, filetype, shop_id, 0, 1);
                }
            }
            else
            {
                DataTable dtCopy = dt.Copy();
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                filename = firstName + shop_id + "_" + date + extension;
                dtCopy = FormatFile(dtCopy, fileNo);
                GenerateCSV(dtCopy, filename);
                SaveItem_ExportQ(filename, filetype, shop_id, 0, 1);
            }
            return filename;
        }

        public void GenerateCSV(DataTable dtInformation, string FileName)
        {
            string ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
            string BakExportCSVPath = ConfigurationManager.AppSettings["BakExportCSVPath"].ToString();
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
                    case "1.3.2.5.2": //Yahoo_quantity
                        {
                            dt.Columns.Remove("Exhibit_ID");  //For Download
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.5.1"://Yahoo_data
                        {
                            dt.Columns.Remove("Exhibit_ID");  //For Download
                            dt.Columns.Remove("Item_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("コントロールカラム");
                            dt.AcceptChanges();
                            break;
                        }

                }
            }
            return dt;
        }

        private void SaveItemShopExportedCSVInfo(int itemID, int shopID, string csvName, string ctrl_id)
        {
            exhibitBL.SaveExhibitionItemShopExportedCSVInfo(itemID, shopID, csvName, ctrl_id);
        }

        public void DeleteFilesFromDirectory()
        {
            String path = ExportCSVPath + "\\Image\\";
            string[] files = Directory.GetFiles(path);
            foreach (string pathFile in files)
            {
                var file = new FileInfo(pathFile);
                file.Attributes = FileAttributes.Normal;
                File.Delete(pathFile);
            }
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }
            path = ExportCSVPath + "\\LibImage\\";
            files = Directory.GetFiles(path);
            foreach (string pathFile in files)
            {
                var file = new FileInfo(pathFile);
                file.Attributes = FileAttributes.Normal;
                File.Delete(pathFile);
            }
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }

        }
    }
}
