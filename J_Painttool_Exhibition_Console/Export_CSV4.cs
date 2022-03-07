using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CKSKS_Common;
using CKSKS_BL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Web;

namespace J_Painttool_Exhibition_Console
{
    class Export_CSV4
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        String ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        String BakExportCSVPath = ConfigurationManager.AppSettings["BakExportCSVPath"].ToString();
        Exhibition_List_BL exhibitBL = new Exhibition_List_BL();
        Log_Exhibition_BL log = new Log_Exhibition_BL();

        public void JPainttoolFilterSKU(DataTable dtMainItem, int shop_id)
        {
            //For new
            #region IsSKU=0
            DataRow[] dr = dtMainItem.Select("IsSKU=0");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("IsSKU=0").CopyToDataTable();
                //1.item.csv
                string filename = CreateFile(dtItem, "n", "基礎情報登録$", shop_id, 0, ".xlsx"); //1.3.2.1.1
                foreach (DataRow drTmp in dtItem.Rows)
                {
                    SaveItemShopExportedCSVInfo(int.Parse(drTmp["Exhibit_ID"].ToString()), shop_id, filename, "n");
                }
            }
            #endregion

            #region IsSKU=1
            dr = dtMainItem.Select("IsSKU=1");
            if (dr.Count() > 0)
            {
                DataTable dtItem = dtMainItem.Select("IsSKU=1").CopyToDataTable();
                string filename = CreateFile(dtItem, "u", "基礎情報登録$", shop_id, 0, ".xlsx"); //1.3.2.4.2
                foreach (DataRow drTmp in dtMainItem.Rows)
                {
                    SaveItemShopExportedCSVInfo(int.Parse(drTmp["Exhibit_ID"].ToString()), shop_id, filename, "u");
                }
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

        public string CreateFile(DataTable dt, String CtrlID, String firstName, int shop_id, int filetype, String extension)
        {
            String filename = "";
            if (!string.IsNullOrWhiteSpace(CtrlID))
            {
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    filename = firstName + shop_id + "_" + date + extension;
                    DataTable dtCopy = dt.Copy();
                    dtCopy = FormatFile(dtCopy);
                    GenerateCSV(dtCopy, filename);
                    SaveItem_ExportQ(filename, filetype, shop_id, 0, 1);
            }
            return filename;
        }

        public void GenerateCSV(DataTable dtInformation, string FileName)
        {
            try
            {
                string ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
                string BakExportCSVPath = ConfigurationManager.AppSettings["BakExportCSVPath"].ToString();
                DataColumnCollection dcCollection = dtInformation.Columns;
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                xlWorkBook = ExcelApp.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dtInformation.Rows.Count + 2; i++)
                {
                    for (int j = 1; j < dtInformation.Columns.Count + 1; j++)
                    {
                        if (i == 1)
                            ExcelApp.Cells[i, j] = dcCollection[j - 1].ToString();
                        else
                            ExcelApp.Cells[i, j] = dtInformation.Rows[i - 2][j - 1].ToString();
                    }
                }
                xlWorkBook.SaveAs(ExportCSVPath + FileName);
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable FormatFile(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Remove("Exhibit_ID");
                dt.Columns.Remove("Shop_ID");
                dt.Columns.Remove("Item_ID");
                dt.Columns.Remove("IsSKU");
                dt.Columns.Remove("Ctrl_ID");
                dt.Columns.Remove("Item_Code");
                dt.Columns.Remove("Item_Code_URL");
                dt.Columns.Remove("Item_Name");
                dt.AcceptChanges();
            }
            return dt;
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
    }
}
