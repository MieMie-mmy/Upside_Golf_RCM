/*
 Table Using
 * Promotion_Point
 * Shop
 * Exhibition_Promotion_Item_Master
 * Exhibition_Promotion_Item_Shop
 * Exhibition_Promotion_Item_Category
 * Item_Category
 * Item_Master
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Collections;
using ORS_RCM_BL;

namespace Yahoo_Point_Promotion_Console
{
    public class Program
    {
        static string ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        static string ExportCSVBackPath = ConfigurationManager.AppSettings["ExportCSVBackupPath"].ToString();
        static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        static string addpid = null;
        static string removepid = null;
        public static void Main(string[] args)
        {
            ExportPromotionCSV();
        }
        public static void ExportPromotionCSV()
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtAddYahoo = promotionBL.Adddatas(2);

            DataTable dtRemoveY = promotionBL.Removedata(2);


            if (dtAddYahoo.Rows.Count > 0)
            {
                CreateCSVforYahoo(dtAddYahoo);
                promotionBL.ChangeExportStatusFlag(1, dtAddYahoo);
                promotionBL.CampaignExportStatusFlag(1, addpid, 2);
                addpid = null;
            }
            if (dtRemoveY.Rows.Count > 0)
            {
                RemoveCSVforYahoo(dtRemoveY);
                promotionBL.ChangeExportStatusFlag(2, dtRemoveY);
                promotionBL.CampaignExportStatusFlag(2, removepid, 2);
                removepid = null;
            }

        }
        public static ArrayList RemoveDuplicateRows(DataTable dTable, string colName)
        {
            try
            {
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();

                //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                //And add duplicate item value in arraylist.
                foreach (DataRow drow in dTable.Rows)
                {
                    if (hTable.Contains(drow[colName]))
                        duplicateList.Add(drow[colName]);
                    else
                        hTable.Add(drow[colName], string.Empty);
                }

                //Removing a list of duplicate items from datatable.


                //Datatable which contains unique records will be return as output.
                return duplicateList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void CreateCSVforYahoo(DataTable dt)
        {
            Change_Template changeTemplates = new Change_Template();
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtexport = new DataTable();
            DataTable dtMaster = new DataTable();
            DataTable dtSelect = new DataTable();
            DataTable dtitem2 = new DataTable();
            DataTable dtitem = new DataTable();
            string shopid = null;
            //same shop
            ArrayList Alshop = RemoveDuplicateRows(dt, "Shop_ID");

            for (int k = 0; k < Alshop.Count; k++)
            {
                string sl = Alshop[k].ToString();
                DataRow[] drs = dt.Select("Shop_ID = " + Alshop[k]);
                if (drs.Count() > 0)
                {
                    dtitem2 = dt.Select("Shop_ID = " + Alshop[k]).CopyToDataTable();

                    foreach (DataRow drst in drs)
                    {
                        dt.Rows.Remove(drst);
                        dt.AcceptChanges();
                    }


                    if (dtitem2 != null && dtitem2.Rows.Count > 0)
                    {
                        foreach (DataRow drsame in dtitem2.Rows)
                        {
                            if (drsame["Mall_ID"].ToString() == "2")
                            {
                                if (!String.IsNullOrWhiteSpace(drsame["Shop_ID"].ToString()))
                                {
                                    addpid += drsame["ID"].ToString() + ',';
                                    shopid = drsame["Shop_ID"].ToString();

                                    dtMaster = promotionBL.GetdatafromMaster(drsame["Item_Code"].ToString());
                                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                                    {
                                        dtexport = changeTemplates.ModifyTable(promotionBL.GetdataforYahoo(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(drsame["Shop_ID"].ToString()), 1), int.Parse(drsame["Shop_ID"].ToString()));

                                        if (dtexport != null && dtexport.Rows.Count > 0)
                                        {
                                            if (dtexport.Columns.Contains("name"))
                                            {
                                                int id = promotionBL.YahooPoint_ExhibitionList_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(drsame["Shop_ID"].ToString()), dtexport.Rows[0]["name"].ToString(), 1, dtexport, 2);

                                            }
                                            else
                                            {
                                                int ids = promotionBL.YahooPoint_ExhibitionList_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(drsame["Shop_ID"].ToString()), dtexport.Rows[0]["name"].ToString(), 1, dtexport, 2);

                                            }
                                            dtSelect.Merge(dtexport);
                                        }
                                    }//if
                                }
                            }
                        }
                        if (dtSelect != null && dtSelect.Rows.Count > 0)
                        {
                            string shop_id = shopid;
                            String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                            string filename = shop_id + "_" + date;


                            using (StreamWriter writer = new StreamWriter(ExportCSVPath + "data_add$" + filename + String.Format("_{0}.csv", 1), false, Encoding.GetEncoding(932)))
                            {
                                WriteDataTable(dtSelect, writer, true);

                            }
                            using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "data_add$" + filename + String.Format("_{0}.csv", 1), false, Encoding.GetEncoding(932)))
                            {
                                WriteDataTable(dtSelect, writer, true);
                                dtSelect.Rows.Clear();
                            }

                            string File_Name = "data_add$" + filename + String.Format("_{0}.csv", 1);
                            int File_Type = 0;
                            int ShopID = int.Parse(shop_id);
                            int IsExport = 0;

                            int Export_Type = 2;
                            promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);
                        }
                    }
                }



            }
            //single csv

            foreach (DataRow dr in dt.Rows)
            {

                if (dr["Mall_ID"].ToString() == "2")
                {
                    if (!String.IsNullOrWhiteSpace(dr["Shop_ID"].ToString()))
                    {
                        addpid += dr["ID"].ToString() + ',';
                        shopid = dr["Shop_ID"].ToString();

                        dtMaster = promotionBL.GetdatafromMaster(dr["Item_Code"].ToString());
                        if (dtMaster != null && dtMaster.Rows.Count > 0)
                        {
                            dtexport = changeTemplates.ModifyTable(promotionBL.GetdataforYahoo(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), 1), int.Parse(dr["Shop_ID"].ToString()));

                            if (dtexport != null && dtexport.Rows.Count > 0)
                            {
                                if (dtexport.Columns.Contains("name"))
                                {
                                    int id = promotionBL.YahooPoint_ExhibitionList_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexport.Rows[0]["name"].ToString(), 1, dtexport, 2);

                                }
                                else
                                {
                                    int ids = promotionBL.YahooPoint_ExhibitionList_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexport.Rows[0]["name"].ToString(), 1, dtexport, 2);

                                }
                                string shop_id = shopid;
                                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                                string filename = shop_id + "_" + date;


                                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "data_add$" + filename + String.Format("_{0}.csv", 1), false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dtexport, writer, true);

                                }
                                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "data_add$" + filename + String.Format("_{0}.csv", 1), false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dtexport, writer, true);

                                }

                                string File_Name = "data_add$" + filename + String.Format("_{0}.csv", 1);
                                int File_Type = 0;
                                int ShopID = int.Parse(shop_id);
                                int IsExport = 0;

                                int Export_Type = 2;
                                promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);
                            }
                        }
                    }
                }//if

            }//foreach


        }
        public static void RemoveCSVforYahoo(DataTable dt)
        {
            Change_Template changeTemplates = new Change_Template();
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtexport = new DataTable();
            DataTable dtMaster = new DataTable();
            DataTable dtitemall = new DataTable();
            DataTable dtitem2 = new DataTable();

            string shopid = null;

            //same shop
            ArrayList Alshop = RemoveDuplicateRows(dt, "Shop_ID");

            for (int k = 0; k < Alshop.Count; k++)
            {
                string sl = Alshop[k].ToString();
                DataRow[] drs = dt.Select("Shop_ID = " + Alshop[k]);
                if (drs.Count() > 0)
                {
                    dtitem2 = dt.Select("Shop_ID = " + Alshop[k]).CopyToDataTable();

                    foreach (DataRow drst in drs)
                    {
                        dt.Rows.Remove(drst);
                        dt.AcceptChanges();
                    }


                    if (dtitem2 != null && dtitem2.Rows.Count > 0)
                    {
                        foreach (DataRow drsame in dtitem2.Rows)
                        {
                            if (drsame["Mall_ID"].ToString() == "2")
                            {

                                dtMaster = promotionBL.GetdatafromMaster(drsame["Item_Code"].ToString());
                                if (dtMaster != null && dtMaster.Rows.Count > 0)
                                {
                                    removepid += drsame["ID"].ToString() + ',';
                                    shopid = drsame["Shop_ID"].ToString();
                                    if (!String.IsNullOrWhiteSpace(drsame["Shop_ID"].ToString()))
                                        dtexport = changeTemplates.ModifyTable(promotionBL.GetdataforYahoo(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(drsame["Shop_ID"].ToString()), 2), int.Parse(drsame["Shop_ID"].ToString()));

                                    if (dtexport != null && dtexport.Rows.Count > 0)
                                    {
                                        if (dtexport.Columns.Contains("name"))
                                        {
                                            int id = promotionBL.YahooPoint_ExhibitionList_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(drsame["Shop_ID"].ToString()), dtexport.Rows[0]["name"].ToString(), 1, dtexport, 2);

                                        }

                                        dtitemall.Merge(dtexport);
                                    }
                                }//
                            }
                        }
                        if (dtitemall != null && dtitemall.Rows.Count > 0)
                        {
                            string shop_id = shopid;
                            String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                            string filename = shop_id + "_" + date;


                            using (StreamWriter writer = new StreamWriter(ExportCSVPath + "data_add$" + filename + String.Format("_{0}_R.csv", 2), false, Encoding.GetEncoding(932)))
                            {
                                WriteDataTable(dtitemall, writer, true);

                            }

                            using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "data_add$" + filename + String.Format("_{0}_R.csv", 2), false, Encoding.GetEncoding(932)))
                            {
                                WriteDataTable(dtitemall, writer, true);
                                dtitemall.Rows.Clear();
                            }

                            string File_Name = "data_add$" + filename + String.Format("_{0}_R.csv", 2);
                            int File_Type = 0;
                            int ShopID = int.Parse(shop_id);
                            int IsExport = 0;

                            int Export_Type = 2;
                            promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);

                        }
                    }
                }

            }
            //single csv
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["Mall_ID"].ToString() == "2")
                {

                    dtMaster = promotionBL.GetdatafromMaster(dr["Item_Code"].ToString());
                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {
                        removepid += dr["ID"].ToString() + ',';
                        shopid = dr["Shop_ID"].ToString();
                        if (!String.IsNullOrWhiteSpace(dr["Shop_ID"].ToString()))
                            dtexport = changeTemplates.ModifyTable(promotionBL.GetdataforYahoo(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), 2), int.Parse(dr["Shop_ID"].ToString()));

                        if (dtexport != null && dtexport.Rows.Count > 0)
                        {
                            if (dtexport.Columns.Contains("name"))
                            {
                                int id = promotionBL.YahooPoint_ExhibitionList_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexport.Rows[0]["name"].ToString(), 1, dtexport, 2);

                            }


                        }

                        string shop_id = shopid;
                        String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                        string filename = shop_id + "_" + date;


                        using (StreamWriter writer = new StreamWriter(ExportCSVPath + "data_add$" + filename + String.Format("_{0}_R.csv", 2), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtexport, writer, true);

                        }

                        using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "data_add$" + filename + String.Format("_{0}_R.csv", 2), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtexport, writer, true);

                        }

                        string File_Name = "data_add$" + filename + String.Format("_{0}_R.csv", 2);
                        int File_Type = 0;
                        int ShopID = int.Parse(shop_id);
                        int IsExport = 0;

                        int Export_Type = 2;
                        promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);

                    }//if
                }
            }//foreach
        }
        public static void CreateCSVforYahoo1(DataTable dt)
        {
            Change_Template changeTemplates = new Change_Template();
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtexport = new DataTable();
            DataTable dtMaster = new DataTable();
            DataTable dtSelect = new DataTable();
            string shopid = null;
            foreach (DataRow dr in dt.Rows)
            {
                //DataTable dtmall = GetMallID(Int32.Parse(dr["Mall_ID"].ToString()));
                if (dr["Mall_ID"].ToString() == "2")
                {
                    if (!String.IsNullOrWhiteSpace(dr["Shop_ID"].ToString()))
                    {
                        addpid += dr["ID"].ToString() + ',';
                        shopid = dr["Shop_ID"].ToString();

                        dtMaster = promotionBL.GetdatafromMaster(dr["Item_Code"].ToString());
                        if (dtMaster != null && dtMaster.Rows.Count > 0)
                        {
                            dtexport = changeTemplates.ModifyTable(promotionBL.GetdataforYahoo(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), 1), int.Parse(dr["Shop_ID"].ToString()));
                            // dtexport = promotionBL.GetdataforYahoo(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), 1);
                            if (dtexport != null && dtexport.Rows.Count > 0)
                            {
                                if (dtexport.Columns.Contains("name"))
                                {
                                    int id = promotionBL.YahooPoint_ExhibitionList_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexport.Rows[0]["name"].ToString(), 1, dtexport, 2);
                                    //  int id = promotionBL.Exhibition_List_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexport.Rows[0]["name"].ToString(), 0,1);
                                }
                                else
                                {
                                    int ids = promotionBL.YahooPoint_ExhibitionList_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexport.Rows[0]["name"].ToString(), 1, dtexport, 2);
                                    // int ids = promotionBL.Exhibition_List_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), null, 0,1);
                                }
                                dtSelect.Merge(dtexport);

                            }




                        }
                    }
                }//if
            }//foreach
            if (dtSelect != null && dtSelect.Rows.Count > 0)
            {
                string shop_id = shopid;
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string filename = shop_id + "_" + date;


                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "data_add$" + filename + String.Format("_{0}.csv", 1), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtSelect, writer, true);

                }
                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "data_add$" + filename + String.Format("_{0}.csv", 1), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtSelect, writer, true);

                }

                string File_Name = "data_add$" + filename + String.Format("_{0}.csv", 1);
                int File_Type = 0;
                int ShopID = int.Parse(shop_id);
                int IsExport = 0;

                int Export_Type = 2;
                promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);

            }



        }
        public static void RemoveCSVforYahoo1(DataTable dt)
        {
            Change_Template changeTemplates = new Change_Template();
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtexport = new DataTable();
            DataTable dtMaster = new DataTable();
            DataTable dtitemall = new DataTable();
            string shopid = null;
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["Mall_ID"].ToString() == "2")
                {

                    dtMaster = promotionBL.GetdatafromMaster(dr["Item_Code"].ToString());
                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {
                        removepid += dr["ID"].ToString() + ',';
                        shopid = dr["Shop_ID"].ToString();
                        if (!String.IsNullOrWhiteSpace(dr["Shop_ID"].ToString()))
                            dtexport = changeTemplates.ModifyTable(promotionBL.GetdataforYahoo(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), 2), int.Parse(dr["Shop_ID"].ToString()));
                        //dtexport = promotionBL.GetdataforYahoo(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), 2);
                        if (dtexport != null && dtexport.Rows.Count > 0)
                        {
                            if (dtexport.Columns.Contains("name"))
                            {
                                int id = promotionBL.YahooPoint_ExhibitionList_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexport.Rows[0]["name"].ToString(), 1, dtexport, 2);
                                //int id = promotionBL.Exhibition_List_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexport.Rows[0]["name"].ToString(), 1,1);
                            }
                            else
                            {
                                //  int ids = promotionBL.Exhibition_List_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), null, 1,1);
                            }
                            dtitemall.Merge(dtexport);
                        }


                    }//if
                }
            }//foreach
            if (dtitemall != null && dtitemall.Rows.Count > 0)
            {
                string shop_id = shopid;
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string filename = shop_id + "_" + date;


                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "data_add$" + filename + String.Format("_{0}_R.csv", 2), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtitemall, writer, true);

                }

                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "data_add$" + filename + String.Format("_{0}_R.csv", 2), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtitemall, writer, true);

                }

                string File_Name = "data_add$" + filename + String.Format("_{0}_R.csv", 2);
                int File_Type = 0;
                int ShopID = int.Parse(shop_id);
                int IsExport = 0;

                int Export_Type = 2;
                promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);
            }//
        }

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



    }
}
