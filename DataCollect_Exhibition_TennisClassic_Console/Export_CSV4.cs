using Ionic.Zip;
using ORS_RCM_BL;
using ORS_RCM_Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect_Exhibition_TennisClassic_Console
{
    class Export_CSV4
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        String ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        String BakExportCSVPath = ConfigurationManager.AppSettings["BakExportCSVPath"].ToString();
        String sku = ConfigurationManager.AppSettings["SKUPath"].ToString();
        String ItemImage = ConfigurationManager.AppSettings["ItemImage"].ToString();
        String ConsoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();

        Exhibition_List_BL exhibitBL = new Exhibition_List_BL();
        Log_Exhibition_BL log = new Log_Exhibition_BL();
        DataTable dtItem, dtItem1;
        string filename;
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

        public void TennisFilterSKU(DataTable dtImage, DataTable dtMainSku, DataTable dtMainItem, int shop_id)
        {
            //For Image Zip
            #region Image

            if (dtImage != null && dtImage.Rows.Count > 0)
            {
                string path = "";
                string folderName = "";
                string image_name = "";

                foreach (DataRow drImage in dtImage.Rows)
                {
                    if (drImage["Image_Name"].ToString() != "")
                    {
                        image_name = drImage["Image_Name"].ToString();
                        folderName = drImage["Folder_Name"].ToString();
                        path = sku + folderName + "/";

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        //string localPath = "/Item_Image/";
                        //string imgPath = Server.MapPath(localPath) + dr["Image_Name"].ToString();

                        //Save image into folder

                        if (Directory.Exists(path))
                        {
                            string a = ItemImage + image_name;
                            if (File.Exists(ItemImage + image_name))
                            {
                                if (!File.Exists(path + image_name))
                                    File.Copy(ItemImage + image_name, path + image_name.Replace('-', '_'));
                            }
                        }
                    }
                }

                if (Directory.Exists(sku))
                {
                    using (ZipFile zipfile = new ZipFile())
                    {
                        String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                        zipfile.AddDirectory(sku, "item_img");
                        zipfile.Save(ExportCSVPath + folderName + "$" + shop_id + "_" + date + ".zip");
                        zipfile.Save(BakExportCSVPath + folderName + "$" + shop_id + "_" + date + ".zip");
                        SaveItem_ExportQ(folderName + "$" + shop_id + "_" + date + ".zip", 3, shop_id, 0, 1);
                    }
                    DeleteDirectory(sku, true);
                }
            }
            #endregion

            #region Item
            string groupno = null;
            bool groupflag = false;
            if (groupflag == false)
            {
                groupno = GetGroupNo();
            }


            if (dtMainItem.Rows.Count > 0)
            {

                filename = CreateFile(dtMainItem, "u", "HeaderItem$", shop_id, 0, "_1_1_" + groupno + ".csv", "Item");
            }
            if (dtMainItem != null && dtMainItem.Rows.Count > 0)
            {
                foreach (DataRow drtemp in dtMainItem.Rows)
                {
                    SaveItemShopExportedCSVInfo(int.Parse(drtemp["Exhibit_ID"].ToString()), shop_id, filename, drtemp["Ctrl_ID"].ToString());
                }
            }

            #region Select
            if (dtMainSku.Rows.Count > 0)
            {
                CreateFile(dtMainSku, "u", "DetailSKU$", shop_id, 2, "_1_1_" + groupno + ".csv", "Select"); //1.3.2.4.5
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
        private void SaveItemShopExportedCSVInfo(int itemID, int shopID, string csvName, string ctrl_id)
        {
            exhibitBL.SaveExhibitionItemShopExportedCSVInfo(itemID, shopID, csvName, ctrl_id);
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

        public void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                List<string> headerValues = new List<string>();

                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerValues.Add(column.ColumnName);
                    //headerValues.Add(QuoteValue(column.ColumnName));

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
        private string QuoteValue(string value)
        {
            return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
        }
        public void ConsoleWriteLine_Tofile(string traceText, int shop_id)
        {
            StreamWriter sw = new StreamWriter(ConsoleWriteLinePath + "Tennis_ConsoleWriteLine" + shop_id + ".txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        public void GenerateCSV(DataTable dtInformation, string FileName)
        {
            string ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
            string BakExportCSVPath = ConfigurationManager.AppSettings["BakExportCSVPath"].ToString();
            using (StreamWriter writer = new StreamWriter(ExportCSVPath + FileName, false, Encoding.UTF8))
            {
                WriteDataTable(dtInformation, writer, true);
            }
            using (StreamWriter writer = new StreamWriter(BakExportCSVPath + FileName, false, Encoding.UTF8))
            {
                WriteDataTable(dtInformation, writer, true);
            }
        }
        public DataTable FormatFile(DataTable dt, String fileNo)
        {
            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrWhiteSpace(fileNo))
            {
                switch (fileNo)
                {
                    case "Item":  // item.csv (ctrl = n , u)
                        {
                            dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("Item_ID");
                            dt.Columns.Remove("Ctrl_ID");
                            dt.AcceptChanges();
                            break;
                        }
                    case "Select":  //select.csv 
                        {
                            dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("ID");
                            dt.AcceptChanges();
                            break;
                        }
                }
            }
            return dt;
        }

    }
}
#endregion