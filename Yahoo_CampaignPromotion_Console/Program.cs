using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CKSKS_BL;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CKSKS_Common;

namespace Yahoo_CampaignPromotion_Console
{
    public class Program
    {
        static string ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        static string ExportCSVBackPath = ConfigurationManager.AppSettings["ExportCSVBackupPath"].ToString();
        static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        public static string rootFolderPath = ConfigurationManager.AppSettings["localFilePath"].ToString();
        static string deliveryid = null;
        public static void Main(string[] args)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtAddPromotion = promotionBL.SelectForAddCSV(3);
            DataTable dtRemovePromotion = promotionBL.SelectForAddCSV(6);
            if (dtAddPromotion.Rows.Count > 0)
            {
                Export_Promotion_CSV(dtAddPromotion, 1);
                promotionBL.ChangeCExportStatusFlag(1, dtAddPromotion);
                promotionBL.ChangeCampaignFlag(1, deliveryid, 2);
                deliveryid = null;
            }
            if (dtRemovePromotion.Rows.Count > 0)
            {
                Export_Promotion_CSV(dtRemovePromotion, 2);
                promotionBL.ChangeCExportStatusFlag(2, dtRemovePromotion);
                promotionBL.ChangeCPromotionStatusFlag(1, dtRemovePromotion);
                promotionBL.ChangeCampaignFlag(2, deliveryid, 2);
                deliveryid = null;
            }
        }

        private static void Export_Promotion_CSV(DataTable dtPromotionCri, int option)
        {
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            DataTable dtYahoo = dtPromotionCri.Clone();
            if (dtPromotionCri != null && dtPromotionCri.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPromotionCri.Rows)
                {
                    deliveryid += dr["ID"].ToString() + ',';
                    dtShopList = promotionShopBL.GetShopList(dr["ID"].ToString());
                    foreach (DataRow drShop in dtShopList.Rows)
                    {
                        if (drShop["Mall_ID"].ToString() == "2")
                        {
                            dtYahoo.Rows.Add(dr.ItemArray);
                        }
                    }
                }
            }
            if (dtYahoo != null && dtYahoo.Rows.Count > 0)
            {
                if (option == 1)
                {
                    CreateCSVsForYahoo(dtYahoo, "Add");
                }
                else
                {
                    CreateCSVsForYahoo(dtYahoo, "Remove");
                }
            }
        }

        private static void CreateCSVsForYahoo(DataTable dtPromotionCri, string status)
        {

            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtShopList = new DataTable();
            dtPromotionCri.Columns.Add("Shop_ID", typeof(int));
            for (int i = 0; i < dtPromotionCri.Rows.Count; i++)
            {
                dtShopList = promotionShopBL.GetShopList(dtPromotionCri.Rows[i]["ID"].ToString());
                List<DataRow> rows_to_remove = new List<DataRow>();
                foreach (DataRow row in dtShopList.Rows)
                {
                    if (int.Parse(row["Mall_ID"].ToString()) != 2)
                    {
                        rows_to_remove.Add(row);
                    }
                }
                foreach (DataRow row in rows_to_remove)
                {
                    dtShopList.Rows.Remove(row);
                    dtShopList.AcceptChanges();
                }
                if (dtShopList.Rows.Count > 1)
                {
                    foreach (DataRow dr in dtShopList.Rows)
                    {
                        dtPromotionCri.Rows[i]["Shop_ID"] = dr["ID"].ToString();
                        i++;
                    }
                    i--;//for index missing
                }
                else
                {
                    dtPromotionCri.Rows[i]["Shop_ID"] = dtShopList.Rows[0]["ID"].ToString();
                }
            }
            string shopCri = "";
            DataTable dt = new DataTable(); DataTable dsItematt = new DataTable();
            foreach (DataRow drPromotion in dtPromotionCri.Rows)
            {
                if (!String.IsNullOrWhiteSpace(drPromotion["Shop_ID"].ToString()))
                {
                    #region
                    string sid = null; string id = null;
                    for (int i = 0; i < dtPromotionCri.Rows.Count; i++)
                    {
                        sid += drPromotion["Shop_ID"].ToString() + ',';
                        id += dtPromotionCri.Rows[i]["ID"].ToString() + ',';
                    }
                    if (!String.IsNullOrWhiteSpace(id) && !String.IsNullOrWhiteSpace(sid))
                    {
                    }
                    #endregion
                    //for Attachment
                    int pID = Convert.ToInt32(drPromotion["ID"].ToString());
                    DataTable dtproatt = promotionBL.SelectAttachmentProCSV(pID, 2);
                    DataColumn newcol = new DataColumn("Shop_ID", typeof(int));
                    newcol.DefaultValue = (int)drPromotion["Shop_ID"];
                    dtproatt.Columns.Add(newcol);
                    if (dtproatt != null)
                        dsItematt.Merge(dtproatt);
                    int protype = 2;
                    DataRow[] drPromotionWithSameShop = dtPromotionCri.Select("Shop_ID = " + drPromotion["Shop_ID"].ToString());
                    if (!shopCri.Contains(drPromotion["Shop_ID"].ToString()))
                    {
                        foreach (DataRow dr in drPromotionWithSameShop)
                        {
                            if (status.ToLower() == "add")
                            {
                                DataTable dtTmp = promotionBL.GetPromotionForYahoo(dr["ID"].ToString(), "item", drPromotion["Shop_ID"].ToString());
                                if (dtTmp.Rows.Count > 0)
                                {
                                    dt.Merge(dtTmp);
                                    protype = 0;
                                }
                            }
                            else
                            {
                                DataTable dtTmp = promotionBL.GetPromotionForYahoo(dr["ID"].ToString(), "item_remove", drPromotion["Shop_ID"].ToString());
                                dt.Merge(dtTmp);
                                protype = 1;
                            }
                        }
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Change_Templates changeTemplates = new Change_Templates();
                            dt = changeTemplates.ModifyTable(dt, int.Parse(drPromotion["Shop_ID"].ToString()));
                            DataColumn newColunmsd = new DataColumn("Shop_ID", typeof(System.Int32));
                            newColunmsd.DefaultValue = Convert.ToInt32(int.Parse(drPromotion["Shop_ID"].ToString()));
                            dt.Columns.Add(newColunmsd);
                            int idresult = promotionBL.Yahoo_Campaignlog_Insert(protype, dt, 1, 1);
                            dt.Columns.Remove("Shop_ID");
                            dt.AcceptChanges();
                        }//if condition
                    }
                    string shop_id = drPromotion["Shop_ID"].ToString();
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string filename = shop_id + "_" + date;
                    string attname = null; string attn = null;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Columns.Contains("ID"))
                        {
                            dt.Columns.Remove("ID");
                        }
                        if (dt.Columns.Contains("IsSKU"))
                        {
                            dt.Columns.Remove("IsSKU");
                        }
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (status.ToLower() == "add")
                            {
                                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "data_add$" + filename + ".csv", false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dt, writer, true);
                                }
                                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "data_add$" + filename + ".csv", false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dt, writer, true);
                                    dt.Rows.Clear();
                                }
                                Item_ExportQ_Entity itemExportQ = new Item_ExportQ_Entity();
                                itemExportQ.File_Name = "data_add$" + filename + ".csv";
                                itemExportQ.File_Type = 0;
                                itemExportQ.ShopID = int.Parse(shop_id);
                                itemExportQ.IsExport = 0;
                                itemExportQ.Created_Date = DateTime.Now;
                                itemExportQ.Export_Type = 2;
                                InsertItemExportQ(itemExportQ);
                            }
                            else
                            {
                                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "data_add$" + filename + "_R.csv", false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dt, writer, true);
                                }
                                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "data_add$" + filename + "_R.csv", false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dt, writer, true);
                                    dt.Rows.Clear();
                                }
                                Item_ExportQ_Entity itemExportQ = new Item_ExportQ_Entity();
                                itemExportQ.File_Name = "data_add$" + filename + "_R.csv";
                                itemExportQ.File_Type = 0;
                                itemExportQ.ShopID = int.Parse(shop_id);
                                itemExportQ.IsExport = 0;
                                itemExportQ.Created_Date = DateTime.Now;
                                itemExportQ.Export_Type = 2;
                                InsertItemExportQ(itemExportQ);
                            }
                        }///
                        if (status.ToLower() == "add")
                        {
                            #region
                            //for Attachment
                            if (dsItematt != null && dsItematt.Rows.Count > 0)
                            {
                                for (int y = 0; y < dsItematt.Rows.Count; y++)
                                {
                                    string fname = dsItematt.Rows[y]["File_Name"].ToString();
                                    if (fname.Contains('.'))
                                    {
                                        string[] att = fname.Split('.');
                                        attn = att[0].ToString();
                                        attname = att[att.Length - 1].ToString();
                                    }
                                    if (Move(fname) == true)
                                    {
                                        Item_ExportQ_Entity itemExportQ = new Item_ExportQ_Entity();
                                        itemExportQ.File_Name = attn + "$" + filename + String.Format("_{0}", y.ToString()) + "." + attname;
                                        itemExportQ.File_Type = 0;
                                        itemExportQ.ShopID = int.Parse(shop_id);
                                        itemExportQ.IsExport = 0;
                                        itemExportQ.Created_Date = DateTime.Now;
                                        itemExportQ.Export_Type = 2;
                                        InsertItemExportQ(itemExportQ);
                                        if (File.Exists(ExportCSVPath + itemExportQ.File_Name)) { }
                                        else
                                            File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename
                                    }
                                }
                                dsItematt.Rows.Clear();
                            }
                            #endregion
                        }
                        else
                        {
                            #region
                            //for Attachment
                            if (dsItematt != null && dsItematt.Rows.Count > 0)
                            {
                                for (int y = 0; y < dsItematt.Rows.Count; y++)
                                {
                                    string fname = dsItematt.Rows[y]["File_Name"].ToString();
                                    if (fname.Contains('.'))
                                    {
                                        string[] att = fname.Split('.');
                                        attn = att[0].ToString();
                                        attname = att[att.Length - 1].ToString();
                                    }
                                    if (Move(fname) == true)
                                    {
                                        Item_ExportQ_Entity itemExportQ = new Item_ExportQ_Entity();
                                        itemExportQ.File_Name = attn + "$" + filename + String.Format("_{0}", y.ToString()) + "." + attname;
                                        itemExportQ.File_Type = 0;
                                        itemExportQ.ShopID = int.Parse(shop_id);
                                        itemExportQ.IsExport = 0;
                                        itemExportQ.Created_Date = DateTime.Now;
                                        itemExportQ.Export_Type = 2;
                                        InsertItemExportQ(itemExportQ);
                                        if (File.Exists(ExportCSVPath + itemExportQ.File_Name)) { }
                                        else
                                            File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename
                                    }
                                }
                                dsItematt.Rows.Clear();
                            }
                            #endregion
                        }
                        shopCri += drPromotion["Shop_ID"].ToString() + ",";
                    }
                }//if condition for null check
            }//if condition
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

        private static void InsertItemExportQ(Item_ExportQ_Entity itemExportQ)
        {
            Item_ExportQ_BL itemExportQBL = new Item_ExportQ_BL();
            itemExportQBL.Save(itemExportQ);
        }

        public static bool Move(string file)
        {
            string path = rootFolderPath + file;
            if (File.Exists(path))
            {
                File.Copy(path, Path.Combine(ExportCSVPath, Path.GetFileName(path)), true);
                File.Copy(path, Path.Combine(ExportCSVBackPath, Path.GetFileName(path)), true);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
