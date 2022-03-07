/**
 Using Table
 * Promotion_Point
 * Shop
 * Item_Master
 * 
 * */
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


namespace Rakuten_Point_Promotion_Console
{
    public class Program
    {
        static string ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        static string ExportCSVBackPath = ConfigurationManager.AppSettings["ExportCSVBackupPath"].ToString();
        static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        static string addpid = null;
        static string addpidsecond = null;
        static string removepid = null;
        static void Main(string[] args)
        {
            ExportPromotionCSV();
        }
        public static void ExportPromotionCSV()
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtAddRakuten = promotionBL.Adddatas(1);
            DataTable dtAddRakutensecond = promotionBL.SelectSecondCSVdata(1);
            DataTable dtRemoveR = promotionBL.Removedata(1);

            if (dtAddRakuten.Rows.Count > 0)
            {
                CreateCSVforRakuten(dtAddRakuten);
                promotionBL.ChangeExportStatusFlag(1, dtAddRakuten);
                promotionBL.CampaignExportStatusFlag(1, addpid, 1);
                addpid = null;
            }
            if (dtAddRakutensecond != null && dtAddRakutensecond.Rows.Count > 0)
            {
                CreateCSVforRakutenSecond(dtAddRakutensecond);
                promotionBL.ChangeExportStatusFlag(3, dtAddRakutensecond);
                promotionBL.CampaignExportStatusFlag(3, addpidsecond, 1);
                addpidsecond = null;
            }
            if (dtRemoveR.Rows.Count > 0)
            {
                RemoveCSVforRakuten(dtRemoveR);
                promotionBL.ChangeExportStatusFlag(2, dtRemoveR);
                promotionBL.CampaignExportStatusFlag(2, removepid, 1);
                removepid = null;
            }

        }
        public static void CreateCSVforRakuten(DataTable dt)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtexport = new DataTable();
            DataTable dtMaster = new DataTable();
            DataTable dtitemall = new DataTable();
            DataTable dtitem2 = new DataTable();
            DataTable dtsecond = new DataTable();

            DataTable dtexb = new DataTable();
            DataColumn newColumn = new DataColumn("PC用キャッチコピー", typeof(System.String));
            newColumn.DefaultValue = DBNull.Value;
            dtexb.Columns.Add(newColumn);
            DataColumn newColumns = new DataColumn("モバイル用キャッチコピー", typeof(System.String));
            newColumns.DefaultValue = DBNull.Value;
            dtexb.Columns.Add(newColumns);
            DataColumn newColums = new DataColumn("商品名", typeof(System.String));
            newColums.DefaultValue = DBNull.Value;
            dtexb.Columns.Add(newColums);
            DataColumn newColumss = new DataColumn("Promotion_CSVtype", typeof(System.Int32));
            newColumss.DefaultValue = 0;
            dtexb.Columns.Add(newColumss);
            string shopid = null;
            ArrayList Alshop = RemoveDuplicateRows(dt, "Shop_ID");

            for (int k = 0; k < Alshop.Count;k++ )
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
                    addpid += drsame["ID"].ToString() + ',';
                    dtMaster = promotionBL.GetdatafromMaster(drsame["Item_Code"].ToString());

                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {
                        for (int i = 1; i < 2; i++)
                        {
                            dtexport = promotionBL.GetdataforRakuten(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(drsame["Shop_ID"].ToString()), i);
                            shopid = drsame["Shop_ID"].ToString();

                            if (dtexport != null && dtexport.Rows.Count > 0)
                            {

                                if (i == 1)
                                {

                                    dtexb.Merge(dtexport);
                                    DataColumn newColunmsd = new DataColumn("Shop_ID", typeof(System.Int32));
                                    newColunmsd.DefaultValue = Convert.ToInt32(shopid);
                                    dtexb.Columns.Add(newColunmsd);


                                    int ids = promotionBL.Exhibition_Promotion_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(drsame["Shop_ID"].ToString()), null, 0, 1, dtexb.Rows[0]["コントロールカラム"].ToString(), dtexb.Rows[0]["商品管理番号（商品URL）"].ToString(), Convert.ToInt32(dtexb.Rows[0]["ポイント変倍率"].ToString()), dtexb.Rows[0]["ポイント変倍率適用期間"].ToString(), dtexb.Rows[0]["PC用キャッチコピー"].ToString(), dtexb.Rows[0]["モバイル用キャッチコピー"].ToString(), null);
                                    dtexb.Columns.Remove("Shop_ID");
                                    dtexb.AcceptChanges();
                                    dtexb.Rows.Clear();
                                    dtitemall.Merge(dtexport);

                                }


                            }
                        }//if i

                    }
                }

                if (dtitemall != null && dtitemall.Rows.Count > 0)
                {
                        string shop_id = shopid;
                        String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                        string filename = shop_id + "_" + date;


                        using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}.csv", 1), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtitemall, writer, true);

                        }

                        using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}.csv", 1), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtitemall, writer, true);
                            dtitemall.Rows.Clear();
                        }

                        string File_Name = "item$" + filename + String.Format("_{0}.csv", 1);
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

                if (dr["Mall_ID"].ToString() == "1")
                {
                    addpid += dr["ID"].ToString() + ',';
                    dtMaster = promotionBL.GetdatafromMaster(dr["Item_Code"].ToString());

                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {
                        for (int i = 1; i < 2; i++)
                        {
                            dtexport = promotionBL.GetdataforRakuten(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), i);
                            shopid = dr["Shop_ID"].ToString();

                            if (dtexport != null && dtexport.Rows.Count > 0)
                            {

                                if (i == 1)
                                {
                                   
                                        dtexb.Merge(dtexport);
                                        DataColumn newColunmsd = new DataColumn("Shop_ID", typeof(System.Int32));
                                        newColunmsd.DefaultValue = Convert.ToInt32(shopid);
                                        dtexb.Columns.Add(newColunmsd);
                                       

                                        int ids = promotionBL.Exhibition_Promotion_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), null, 0, 1, dtexb.Rows[0]["コントロールカラム"].ToString(), dtexb.Rows[0]["商品管理番号（商品URL）"].ToString(), Convert.ToInt32(dtexb.Rows[0]["ポイント変倍率"].ToString()), dtexb.Rows[0]["ポイント変倍率適用期間"].ToString(), dtexb.Rows[0]["PC用キャッチコピー"].ToString(), dtexb.Rows[0]["モバイル用キャッチコピー"].ToString(), null);
                                        dtexb.Columns.Remove("Shop_ID");
                                        dtexb.AcceptChanges();
                                        dtexb.Rows.Clear();
                                   
                                    //dtitemall.Merge(dtexport);
                                }


                            }
                        }//if i


               
            //if (dtitemall != null && dtitemall.Rows.Count > 0)
            //{
                string shop_id = shopid;
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string filename = shop_id + "_" + date;


                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}.csv", 1), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtexport, writer, true);

                }

                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}.csv", 1), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtexport, writer, true);

                }

                string File_Name = "item$" + filename + String.Format("_{0}.csv", 1);
                int File_Type = 0;
                int ShopID = int.Parse(shop_id);
                int IsExport = 0;

                int Export_Type = 2;
                promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);
           // }//
                    }//if
                }
            }//foreach
            
        }
        public static void RemoveCSVforRakuten(DataTable dt)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtexport = new DataTable();
            DataTable dtMaster = new DataTable();
            DataTable dtall = new DataTable();
            DataTable dtitem2 = new DataTable();
            DataTable dtexb = new DataTable();
            DataColumn newColumn = new DataColumn("ポイント変倍率", typeof(System.Int32));
            newColumn.DefaultValue = 0;
            dtexb.Columns.Add(newColumn);
            DataColumn newColumns = new DataColumn("ポイント変倍率適用期間", typeof(System.String));
            newColumn.DefaultValue = DBNull.Value;
            dtexb.Columns.Add(newColumns);
            DataColumn newColumss = new DataColumn("Promotion_CSVtype", typeof(System.Int32));
            newColumn.DefaultValue = 1;
            dtexb.Columns.Add(newColumss);
            string shopid = null;

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
                    removepid += drsame["ID"].ToString() + ',';
                    dtMaster = promotionBL.GetdatafromMaster(drsame["Item_Code"].ToString());

                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {
                        dtexport = promotionBL.GetdataforRakuten(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(drsame["Shop_ID"].ToString()), 3);
                        shopid = drsame["Shop_ID"].ToString();
                        if (dtexport != null && dtexport.Rows.Count > 0)
                        {
                            if (dtexport.Columns.Contains("商品名"))
                            {
                                dtexb.Merge(dtexport);
                                DataColumn newColunmsd = new DataColumn("Shop_ID", typeof(System.Int32));
                                newColunmsd.DefaultValue = Convert.ToInt32(shopid);
                                dtexb.Columns.Add(newColunmsd);


                                int ids = promotionBL.Exhibition_Promotion_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(drsame["Shop_ID"].ToString()), dtexb.Rows[0]["商品名"].ToString(), 1, 1, dtexb.Rows[0]["コントロールカラム"].ToString(), dtexb.Rows[0]["商品管理番号（商品URL）"].ToString(), Convert.ToInt32(dtexb.Rows[0]["ポイント変倍率"].ToString()), dtexb.Rows[0]["ポイント変倍率適用期間"].ToString(), dtexb.Rows[0]["PC用キャッチコピー"].ToString(), dtexb.Rows[0]["モバイル用キャッチコピー"].ToString(), null);
                                dtexb.Columns.Remove("Shop_ID");
                                dtexb.AcceptChanges();
                                dtexb.Rows.Clear();
                            }

                            dtall.Merge(dtexport);
                            dtexport.Rows.Clear();
                        }
                    }
                }
                if (dtall != null && dtall.Rows.Count > 0)
                {
                    string shop_id = shopid;
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string filename = shop_id + "_" + date;


                    using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}_R.csv", 3), false, Encoding.GetEncoding(932)))
                    {
                        WriteDataTable(dtall, writer, true);

                    }

                    using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}_R.csv", 3), false, Encoding.GetEncoding(932)))
                    {
                        WriteDataTable(dtall, writer, true);
                        dtall.Rows.Clear();
                    }

                    string File_Name = "item$" + filename + String.Format("_{0}_R.csv", 3);
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

                    if (dr["Mall_ID"].ToString() == "1")
                    {
                        removepid += dr["ID"].ToString() + ',';
                        dtMaster = promotionBL.GetdatafromMaster(dr["Item_Code"].ToString());
                        if (dtMaster != null && dtMaster.Rows.Count > 0)
                        {

                            dtexport = promotionBL.GetdataforRakuten(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), 3);
                            shopid = dr["Shop_ID"].ToString();
                            if (dtexport != null && dtexport.Rows.Count > 0)
                            {
                                if (dtexport.Columns.Contains("商品名"))
                                {
                                    dtexb.Merge(dtexport);
                                    DataColumn newColunmsd = new DataColumn("Shop_ID", typeof(System.Int32));
                                    newColunmsd.DefaultValue = Convert.ToInt32(shopid);
                                    dtexb.Columns.Add(newColunmsd);


                                    int ids = promotionBL.Exhibition_Promotion_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexb.Rows[0]["商品名"].ToString(), 1, 1, dtexb.Rows[0]["コントロールカラム"].ToString(), dtexb.Rows[0]["商品管理番号（商品URL）"].ToString(), Convert.ToInt32(dtexb.Rows[0]["ポイント変倍率"].ToString()), dtexb.Rows[0]["ポイント変倍率適用期間"].ToString(), dtexb.Rows[0]["PC用キャッチコピー"].ToString(), dtexb.Rows[0]["モバイル用キャッチコピー"].ToString(), null);
                                    dtexb.Columns.Remove("Shop_ID");
                                    dtexb.AcceptChanges();
                                    dtexb.Rows.Clear();
                                }

                                //  dtall.Merge(dtexport);
                            }

                        }



                        string shop_id = shopid;
                        String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                        string filename = shop_id + "_" + date;


                        using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}_R.csv", 3), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtexport, writer, true);

                        }

                        using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}_R.csv", 3), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtexport, writer, true);

                        }

                        string File_Name = "item$" + filename + String.Format("_{0}_R.csv", 3);
                        int File_Type = 0;
                        int ShopID = int.Parse(shop_id);
                        int IsExport = 0;

                        int Export_Type = 2;
                        promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);
                    }
                
            }//foreach
        }
        public static void CreateCSVforRakutenSecond(DataTable dt)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtexport = new DataTable();
            DataTable dtMaster = new DataTable();
            DataTable dtitem2 = new DataTable();
            DataTable dtitem = new DataTable();
            DataTable dtexb = new DataTable();
            DataColumn newColumn = new DataColumn("ポイント変倍率", typeof(System.Int32));
            newColumn.DefaultValue = 0;
            dtexb.Columns.Add(newColumn);
            DataColumn newColumns = new DataColumn("ポイント変倍率適用期間", typeof(System.String));
            newColumn.DefaultValue = DBNull.Value;
            dtexb.Columns.Add(newColumns);
            DataColumn newColumss = new DataColumn("Promotion_CSVtype", typeof(System.Int32));
            newColumn.DefaultValue = 0;
            dtexb.Columns.Add(newColumss);
            string shopid = null;
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
                    addpidsecond += drsame["ID"].ToString() + ',';
                    dtMaster = promotionBL.GetdatafromMaster(drsame["Item_Code"].ToString());

                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {
                        dtexport = promotionBL.GetdataforRakuten(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(drsame["Shop_ID"].ToString()), 2);
                        shopid = drsame["Shop_ID"].ToString();
                        if (dtexport != null && dtexport.Rows.Count > 0)
                        {
                            {
                                if (dtexport.Columns.Contains("商品名"))//update item_name
                                {
                                    dtexb.Merge(dtexport);
                                    DataColumn newColunmsd = new DataColumn("Shop_ID", typeof(System.Int32));
                                    newColunmsd.DefaultValue = Convert.ToInt32(shopid);
                                    dtexb.Columns.Add(newColunmsd);


                                    int ids = promotionBL.Exhibition_Promotion_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(drsame["Shop_ID"].ToString()), dtexb.Rows[0]["商品名"].ToString(), 0, 4, dtexb.Rows[0]["コントロールカラム"].ToString(), dtexb.Rows[0]["商品管理番号（商品URL）"].ToString(), Convert.ToInt32(dtexb.Rows[0]["ポイント変倍率"].ToString()), dtexb.Rows[0]["ポイント変倍率適用期間"].ToString(), dtexb.Rows[0]["PC用キャッチコピー"].ToString(), dtexb.Rows[0]["モバイル用キャッチコピー"].ToString(), null);
                                    dtexb.Columns.Remove("Shop_ID");
                                    dtexb.AcceptChanges();
                                    dtexb.Rows.Clear();
                                  
                                }
                                dtitem.Merge(dtexport);
                            }

                        }

                    }
                }

                if (dtitem != null && dtitem.Rows.Count > 0)
                {
                    string shop_id = shopid;
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string filename = shop_id + "_" + date;


                    using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}.csv", 2), false, Encoding.GetEncoding(932)))
                    {
                        WriteDataTable(dtitem, writer, true);

                    }

                    using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}.csv", 2), false, Encoding.GetEncoding(932)))
                    {
                        WriteDataTable(dtitem, writer, true);
                        dtitem.Rows.Clear();
                    }

                    string File_Name = "item$" + filename + String.Format("_{0}.csv", 2);
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

                if (dr["Mall_ID"].ToString() == "1")
                {
                    addpidsecond += dr["ID"].ToString() + ',';
                    dtMaster = promotionBL.GetdatafromMaster(dr["Item_Code"].ToString());
                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {
                        dtexport = promotionBL.GetdataforRakuten(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), 2);
                        shopid = dr["Shop_ID"].ToString();
                        if (dtexport != null && dtexport.Rows.Count > 0)
                        {
                            {
                                if (dtexport.Columns.Contains("商品名"))//update item_name
                                {
                                    dtexb.Merge(dtexport);
                                    DataColumn newColunmsd = new DataColumn("Shop_ID", typeof(System.Int32));
                                    newColunmsd.DefaultValue = Convert.ToInt32(shopid);
                                    dtexb.Columns.Add(newColunmsd);


                                    int ids = promotionBL.Exhibition_Promotion_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexb.Rows[0]["商品名"].ToString(), 0, 4, dtexb.Rows[0]["コントロールカラム"].ToString(), dtexb.Rows[0]["商品管理番号（商品URL）"].ToString(), Convert.ToInt32(dtexb.Rows[0]["ポイント変倍率"].ToString()), dtexb.Rows[0]["ポイント変倍率適用期間"].ToString(), dtexb.Rows[0]["PC用キャッチコピー"].ToString(), dtexb.Rows[0]["モバイル用キャッチコピー"].ToString(), null);
                                    dtexb.Columns.Remove("Shop_ID");
                                    dtexb.AcceptChanges();
                                    dtexb.Rows.Clear();
                                  
                                }
                            
                            }

                        }
                    }

                }//if
                string shop_id = shopid;
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string filename = shop_id + "_" + date;


                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}.csv", 2), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtexport, writer, true);

                }
                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}.csv", 2), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtexport, writer, true);

                }
                string File_Name = "item$" + filename + String.Format("_{0}.csv", 2);
                int File_Type = 0;
                int ShopID = int.Parse(shop_id);
                int IsExport = 0;

                int Export_Type = 2;
                promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);
         
            }//foreach
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
        public static void CreateCSVforRakuten1(DataTable dt)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtexport = new DataTable();
            DataTable dtMaster = new DataTable();
            DataTable dtitemall = new DataTable();
            DataTable dtitem2 = new DataTable();
            DataTable dtsecond = new DataTable();

            DataTable dtexb = new DataTable();
            DataColumn newColumn = new DataColumn("PC用キャッチコピー", typeof(System.String));
            newColumn.DefaultValue = DBNull.Value;
            dtexb.Columns.Add(newColumn);
            DataColumn newColumns = new DataColumn("モバイル用キャッチコピー", typeof(System.String));
            newColumns.DefaultValue = DBNull.Value;
            dtexb.Columns.Add(newColumns);
            DataColumn newColums = new DataColumn("商品名", typeof(System.String));
            newColums.DefaultValue = DBNull.Value;
            dtexb.Columns.Add(newColums);
            DataColumn newColumss = new DataColumn("Promotion_CSVtype", typeof(System.Int32));
            newColumss.DefaultValue = 0;
            dtexb.Columns.Add(newColumss);
            string shopid = null;
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["Mall_ID"].ToString() == "1")
                {
                    addpid += dr["ID"].ToString() + ',';
                    dtMaster = promotionBL.GetdatafromMaster(dr["Item_Code"].ToString());

                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {
                        for (int i = 1; i < 2; i++)
                        {
                            dtexport = promotionBL.GetdataforRakuten(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), i);
                            shopid = dr["Shop_ID"].ToString();
                           
                            if (dtexport != null && dtexport.Rows.Count > 0)
                            {

                                if (i == 1)
                                {
                                    if (dtexport.Columns.Contains("商品名"))
                                    {
                                     //  int id = promotionBL.Exhibition_List_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexport.Rows[0]["商品名"].ToString(), 0, 1);
                                    }
                                    else
                                    {
                                        dtexb.Merge(dtexport);
                                        DataColumn newColunmsd = new DataColumn("Shop_ID", typeof(System.Int32));
                                        newColunmsd.DefaultValue = Convert.ToInt32(shopid);
                                        dtexb.Columns.Add(newColunmsd);
                                      //  promotionBL.RakutenCampaignExbInsert(dtexb);

                                        int ids = promotionBL.Exhibition_Promotion_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), null, 0, 1, dtexb.Rows[0]["コントロールカラム"].ToString(), dtexb.Rows[0]["商品管理番号（商品URL）"].ToString(), Convert.ToInt32(dtexb.Rows[0]["ポイント変倍率"].ToString()), dtexb.Rows[0]["ポイント変倍率適用期間"].ToString(), dtexb.Rows[0]["PC用キャッチコピー"].ToString(), dtexb.Rows[0]["モバイル用キャッチコピー"].ToString(),null);
                                        dtexb.Columns.Remove("Shop_ID");
                                        dtexb.AcceptChanges();
                                        dtexb.Rows.Clear();
                                    }
                                    dtitemall.Merge(dtexport);
                                }
                                 
                            
                            }
                        }//if i

                       
                    }//if
                }
            }//foreach
            //if (dtexb != null && dtexb.Rows.Count > 0)
            //{
            //    promotionBL.RakutenCampaignExbInsert(dtexb);
            //    dtexb = null; 
            //}
            if (dtitemall != null && dtitemall.Rows.Count > 0)
            {
                string shop_id = shopid;
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string filename = shop_id + "_" + date;


                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}.csv", 1), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtitemall, writer, true);

                }

                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}.csv", 1), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtitemall, writer, true);

                }

                string File_Name = "item$" + filename + String.Format("_{0}.csv", 1);
                int File_Type = 0;
                int ShopID = int.Parse(shop_id);
                int IsExport = 0;

                int Export_Type = 2;
                promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);
            }//
            if (dtitem2 != null && dtitem2.Rows.Count > 0)
            {
                string shop_id = shopid;
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string filename = shop_id + "_" + date;


                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}.csv", 2), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtitem2, writer, true);

                }
                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}.csv", 2), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtitem2, writer, true);

                }
                string File_Name = "item$" + filename + String.Format("_{0}.csv", 2);
                int File_Type = 0;
                int ShopID = int.Parse(shop_id);
                int IsExport = 0;

                int Export_Type = 2;
                promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);

            }//
        }
        public static void CreateCSVforRakutenSecond1(DataTable dt)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtexport = new DataTable();
            DataTable dtMaster = new DataTable();
            DataTable dtitem2 = new DataTable();

            DataTable dtexb = new DataTable();
            DataColumn newColumn = new DataColumn("ポイント変倍率", typeof(System.Int32));
            newColumn.DefaultValue = 0;
            dtexb.Columns.Add(newColumn);
            DataColumn newColumns = new DataColumn("ポイント変倍率適用期間", typeof(System.String));
            newColumn.DefaultValue = DBNull.Value;
            dtexb.Columns.Add(newColumns);
            DataColumn newColumss = new DataColumn("Promotion_CSVtype", typeof(System.Int32));
            newColumn.DefaultValue = 0;
            dtexb.Columns.Add(newColumss);

            string shopid = null;
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["Mall_ID"].ToString() == "1")
                {
                    addpidsecond += dr["ID"].ToString() + ','; 
                    dtMaster = promotionBL.GetdatafromMaster(dr["Item_Code"].ToString());
                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {
                        dtexport = promotionBL.GetdataforRakuten(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), 2);
                        shopid = dr["Shop_ID"].ToString();
                        if (dtexport != null && dtexport.Rows.Count > 0)
                        {
                            {
                                if (dtexport.Columns.Contains("商品名"))//update item_name
                                {
                                    dtexb.Merge(dtexport);
                                    DataColumn newColunmsd = new DataColumn("Shop_ID", typeof(System.Int32));
                                    newColunmsd.DefaultValue = Convert.ToInt32(shopid);
                                    dtexb.Columns.Add(newColunmsd);


                                    int ids = promotionBL.Exhibition_Promotion_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexb.Rows[0]["商品名"].ToString(), 0, 4, dtexb.Rows[0]["コントロールカラム"].ToString(), dtexb.Rows[0]["商品管理番号（商品URL）"].ToString(), Convert.ToInt32(dtexb.Rows[0]["ポイント変倍率"].ToString()), dtexb.Rows[0]["ポイント変倍率適用期間"].ToString(), dtexb.Rows[0]["PC用キャッチコピー"].ToString(), dtexb.Rows[0]["モバイル用キャッチコピー"].ToString(), null);
                                    dtexb.Columns.Remove("Shop_ID");
                                    dtexb.AcceptChanges();
                                    dtexb.Rows.Clear();
                                    //promotionBL.Exhibition_List_Update(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexport.Rows[0]["商品名"].ToString(), dtexport.Rows[0]["PC用キャッチコピー"].ToString(), dtexport.Rows[0]["モバイル用キャッチコピー"].ToString());
                                }
                                dtitem2.Merge(dtexport);
                            }
                           
                        }
                    }

                }//if

            }//foreach
           
            if (dtitem2 != null && dtitem2.Rows.Count > 0)
            {
                string shop_id = shopid;
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string filename = shop_id + "_" + date;


                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}.csv", 2), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtitem2, writer, true);

                }
                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}.csv", 2), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtitem2, writer, true);

                }
                string File_Name = "item$" + filename + String.Format("_{0}.csv", 2);
                int File_Type = 0;
                int ShopID = int.Parse(shop_id);
                int IsExport = 0;

                int Export_Type = 2;
                promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);
            }//
        }
        public static void RemoveCSVforRakuten1(DataTable dt)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtexport = new DataTable();
            DataTable dtMaster = new DataTable();
            DataTable dtall = new DataTable();

            DataTable dtexb = new DataTable();
            DataColumn newColumn = new DataColumn("ポイント変倍率", typeof(System.Int32));
            newColumn.DefaultValue = 0;
            dtexb.Columns.Add(newColumn);
            DataColumn newColumns = new DataColumn("ポイント変倍率適用期間", typeof(System.String));
            newColumn.DefaultValue = DBNull.Value;
            dtexb.Columns.Add(newColumns);
            DataColumn newColumss = new DataColumn("Promotion_CSVtype", typeof(System.Int32));
            newColumn.DefaultValue = 1;
            dtexb.Columns.Add(newColumss);
            string shopid = null;
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["Mall_ID"].ToString() == "1")
                {
                    removepid += dr["ID"].ToString()+','; 
                    dtMaster = promotionBL.GetdatafromMaster(dr["Item_Code"].ToString());
                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {

                        dtexport = promotionBL.GetdataforRakuten(dtMaster.Rows[0]["ID"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), 3);
                        shopid = dr["Shop_ID"].ToString();
                        if (dtexport != null && dtexport.Rows.Count > 0)
                        {
                            if (dtexport.Columns.Contains("商品名"))
                            {
                                dtexb.Merge(dtexport);
                                DataColumn newColunmsd = new DataColumn("Shop_ID", typeof(System.Int32));
                                newColunmsd.DefaultValue = Convert.ToInt32(shopid);
                                dtexb.Columns.Add(newColunmsd);


                                int ids = promotionBL.Exhibition_Promotion_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), dtexb.Rows[0]["商品名"].ToString(), 1, 1, dtexb.Rows[0]["コントロールカラム"].ToString(), dtexb.Rows[0]["商品管理番号（商品URL）"].ToString(), Convert.ToInt32(dtexb.Rows[0]["ポイント変倍率"].ToString()), dtexb.Rows[0]["ポイント変倍率適用期間"].ToString(), dtexb.Rows[0]["PC用キャッチコピー"].ToString(), dtexb.Rows[0]["モバイル用キャッチコピー"].ToString(), null);
                                dtexb.Columns.Remove("Shop_ID");
                                dtexb.AcceptChanges();
                                dtexb.Rows.Clear();
                            }
                            else
                            {
                                //int ids = promotionBL.Exhibition_List_Inserts(Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString()), dtMaster.Rows[0]["Item_Code"].ToString(), Convert.ToInt32(dr["Shop_ID"].ToString()), null, 1, 1);
                            }
                            dtall.Merge(dtexport);
                        }
                        
                    }
                }
            }//foreach
          
            if (dtall != null && dtall.Rows.Count > 0)
            {
                string shop_id = shopid;
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string filename = shop_id + "_" + date;


                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}_R.csv", 3), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtall, writer, true);

                }

                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}_R.csv", 3), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dtall, writer, true);

                }

                string File_Name = "item$" + filename + String.Format("_{0}_R.csv", 3);
                int File_Type = 0;
                int ShopID = int.Parse(shop_id);
                int IsExport = 0;

                int Export_Type = 2;
                promotionBL.InsertItemExportQ(File_Name, File_Type, ShopID, IsExport, Export_Type);
            }

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
