/**
 Using Table:
 * Promotion
 * 
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CKSKS_BL;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using CKSKS_Common;

namespace Rakuten_CampaingPromotion_Console
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
            DataTable dtAddPromotion = promotionBL.SelectForAddCSV(1);
            DataTable dtRemovePromotion = promotionBL.SelectForAddCSV(2);
            if (dtAddPromotion.Rows.Count > 0)
            {
                Export_Promotion_CSV(dtAddPromotion, 1);
                promotionBL.ChangeCExportStatusFlag(1, dtAddPromotion);
                promotionBL.ChangeCampaignFlag(1, deliveryid, 1);
                deliveryid = null;
            }
            if (dtRemovePromotion.Rows.Count > 0)
            {
                Export_Promotion_CSV(dtRemovePromotion, 2);
                promotionBL.ChangeCExportStatusFlag(2, dtRemovePromotion);
                promotionBL.ChangeCPromotionStatusFlag(1, dtRemovePromotion);
                promotionBL.ChangeCampaignFlag(2, deliveryid, 1);
                deliveryid = null;
            }
        }

        private static void Export_Promotion_CSV(DataTable dtPromotionCri, int option)
        {
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            DataTable dtRakuten = dtPromotionCri.Clone();
            if (dtPromotionCri != null && dtPromotionCri.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPromotionCri.Rows)
                {
                    deliveryid += dr["ID"].ToString() + ',';
                    dtShopList = promotionShopBL.GetShopList(dr["ID"].ToString());
                    foreach (DataRow drShop in dtShopList.Rows)
                    {
                        if (drShop["Mall_ID"].ToString() == "1")
                        {
                            dtRakuten.Rows.Add(dr.ItemArray);
                        }
                    }
                }
            }
            if (dtRakuten != null && dtRakuten.Rows.Count > 0)
            {
                if (option == 1)
                {
                    CreateCSVsForRakuten(dtRakuten);
                }
                else
                {
                    CreateRemoveCSVsForRakuten(dtRakuten);
                }
            }
        }

        private static void CreateCSVsForRakuten(DataTable dtPromotionCri)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            DataTable dtselectdelete = new DataTable();
            dtPromotionCri.Columns.Add("Shop_ID", typeof(int));
            for (int i = 0; i < dtPromotionCri.Rows.Count; i++)
            {
                dtShopList = promotionShopBL.GetShopList(dtPromotionCri.Rows[i]["ID"].ToString());
                List<DataRow> rows_to_remove = new List<DataRow>();
                foreach (DataRow row in dtShopList.Rows)
                {
                    if (int.Parse(row["Mall_ID"].ToString()) != 1)
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

            DataSet dsItem = new DataSet();
            for (int i = 0; i < 2; i++)//i<9
            {
                dsItem.Tables.Add("dtCampaingType" + i.ToString());
            }
            string shopCri = "";
            Change_Templates changeTemplates = new Change_Templates();
            foreach (DataRow drPromotion in dtPromotionCri.Rows)
            {
                if (!String.IsNullOrWhiteSpace(drPromotion["Shop_ID"].ToString()))
                {
                    DataRow[] drPromotionWithSameShop = dtPromotionCri.Select("Shop_ID = " + drPromotion["Shop_ID"].ToString());
                    DataTable dtcat = new DataTable();
                    DataTable dtSelect = new DataTable();
                    int pID = Convert.ToInt32(drPromotion["ID"].ToString());
                    if (!shopCri.Contains(drPromotion["Shop_ID"].ToString()))
                    {
                        foreach (DataRow dr in drPromotionWithSameShop)
                        {
                            DataTable dt = new DataTable();
                            dtselectdelete.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), "selectdelete", int.Parse(drPromotion["Shop_ID"].ToString())));
                            dtSelect.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), "select", int.Parse(drPromotion["Shop_ID"].ToString())));
                        }
                    }
                    string shop_id = drPromotion["Shop_ID"].ToString();
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string filename = shop_id + "_" + date;
                    #region
                    if (dtselectdelete != null && dtselectdelete.Rows.Count > 0)
                    {
                        using (StreamWriter writer = new StreamWriter(ExportCSVPath + "select$" + filename + String.Format("_{0}.csv", 1.ToString()), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtselectdelete, writer, true);
                        }
                        using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "select$" + filename + String.Format("_{0}.csv", 1.ToString()), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtselectdelete, writer, true);
                            dtselectdelete.Rows.Clear();
                        }
                        Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                        itemExportQII.File_Name = "select$" + filename + String.Format("_{0}.csv", 1.ToString());
                        itemExportQII.File_Type = 0;
                        itemExportQII.ShopID = int.Parse(shop_id);
                        itemExportQII.IsExport = 0;
                        itemExportQII.Created_Date = DateTime.Now;
                        itemExportQII.Export_Type = 2;
                        InsertItemExportQ(itemExportQII);
                    }//i=8
                    #endregion

                    if (dtSelect != null && dtSelect.Rows.Count > 0)
                    {
                        using (StreamWriter writer = new StreamWriter(ExportCSVPath + "select$" + filename + String.Format("_{0}.csv", 2.ToString()), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtSelect, writer, true);
                        }
                        using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "select$" + filename + String.Format("_{0}.csv", 2.ToString()), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtSelect, writer, true);
                            dtSelect.Rows.Clear();
                        }
                        Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                        itemExportQII.File_Name = "select$" + filename + String.Format("_{0}.csv", 2.ToString());
                        itemExportQII.File_Type = 0;
                        itemExportQII.ShopID = int.Parse(shop_id);
                        itemExportQII.IsExport = 0;
                        itemExportQII.Created_Date = DateTime.Now;
                        itemExportQII.Export_Type = 2;
                        InsertItemExportQ(itemExportQII);
                        DataTable dtOption = SelectOptionValue(pID);
                        if (dtOption != null && dtOption.Rows.Count > 0)
                        {
                            DeleteItem_Option(pID, int.Parse(shop_id));
                            InsertItem_Option(dtOption, pID, int.Parse(shop_id));
                        }
                    }//i=8
                    shopCri += drPromotion["Shop_ID"].ToString() + ",";
                }//if condition
            }
        }

        private static void CreateRemoveCSVsForRakuten(DataTable dtPromotionCri)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            dtPromotionCri.Columns.Add("Shop_ID", typeof(int));
            for (int i = 0; i < dtPromotionCri.Rows.Count; i++)
            {
                dtShopList = promotionShopBL.GetShopList(dtPromotionCri.Rows[i]["ID"].ToString());
                List<DataRow> rows_to_remove = new List<DataRow>();
                foreach (DataRow row in dtShopList.Rows)
                {
                    if (int.Parse(row["Mall_ID"].ToString()) != 1)
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
            DataSet dsItem = new DataSet();
            for (int i = 0; i < 2; i++)//i<9
            {
                dsItem.Tables.Add("dtCampaingType" + i.ToString());
            }
            string shopCri = "";
            Change_Templates changeTemplates = new Change_Templates();
            foreach (DataRow drPromotion in dtPromotionCri.Rows)
            {
                if (!String.IsNullOrWhiteSpace(drPromotion["Shop_ID"].ToString()))
                {
                    DataRow[] drPromotionWithSameShop = dtPromotionCri.Select("Shop_ID = " + drPromotion["Shop_ID"].ToString());
                    DataTable dtcat = new DataTable();
                    DataTable dtSelect = new DataTable();
                    int pID = Convert.ToInt32(drPromotion["ID"].ToString());
                    if (!shopCri.Contains(drPromotion["Shop_ID"].ToString()))
                    {
                        foreach (DataRow dr in drPromotionWithSameShop)
                        {
                            DataTable dt = new DataTable();
                            dtSelect.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), "select_R", int.Parse(drPromotion["Shop_ID"].ToString())));
                        }
                    }
                    string shop_id = drPromotion["Shop_ID"].ToString();
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string filename = shop_id + "_" + date;
                    if (dtSelect != null && dtSelect.Rows.Count > 0)
                    {
                        using (StreamWriter writer = new StreamWriter(ExportCSVPath + "select$" + filename + String.Format("_{0}_R.csv", 1.ToString()), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtSelect, writer, true);
                        }
                        using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "select$" + filename + String.Format("_{0}_R.csv", 1.ToString()), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtSelect, writer, true);
                            dtSelect.Rows.Clear();
                        }
                        Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                        itemExportQII.File_Name = "select$" + filename + String.Format("_{0}_R.csv", 1.ToString());
                        itemExportQII.File_Type = 0;
                        itemExportQII.ShopID = int.Parse(shop_id);
                        itemExportQII.IsExport = 0;
                        itemExportQII.Created_Date = DateTime.Now;
                        itemExportQII.Export_Type = 2;
                        InsertItemExportQ(itemExportQII);
                    }//i=8
                    shopCri += drPromotion["Shop_ID"].ToString() + ",";
                }//if condition
            }
        }

        private static void CreateRemoveCSVsForRakuten08(DataTable dtPromotionCri)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            dtPromotionCri.Columns.Add("Shop_ID", typeof(int));
            for (int i = 0; i < dtPromotionCri.Rows.Count; i++)
            {
                dtShopList = promotionShopBL.GetShopList(dtPromotionCri.Rows[i]["ID"].ToString());
                List<DataRow> rows_to_remove = new List<DataRow>();
                foreach (DataRow row in dtShopList.Rows)
                {
                    if (int.Parse(row["Mall_ID"].ToString()) != 1)
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
                    for (int j = 0; j < dtShopList.Rows.Count - 1; j++)
                    {
                        dtPromotionCri.Rows.Add(dtPromotionCri.Rows[0].ItemArray);
                    }
                    foreach (DataRow dr in dtShopList.Rows)
                    {
                        dtPromotionCri.Rows[i]["Shop_ID"] = dr["ID"].ToString();
                        i++;
                    }
                }
                else
                {
                    if (dtShopList.Rows.Count > 0)
                    {
                        dtPromotionCri.Rows[i]["Shop_ID"] = dtShopList.Rows[0]["ID"].ToString();
                    }
                }
            }
            DataSet dsItem = new DataSet();
            for (int i = 0; i < 2; i++)
            {
                dsItem.Tables.Add("dtCampaingType" + i.ToString());
            }
            string shopCri = "";
            foreach (DataRow drPromotion in dtPromotionCri.Rows)
            {
                if (!String.IsNullOrWhiteSpace(drPromotion["Shop_ID"].ToString()))
                {
                    DataRow[] drPromotionWithSameShop = dtPromotionCri.Select("Shop_ID = " + drPromotion["Shop_ID"].ToString());
                    DataTable dtSelect = new DataTable();
                    if (!shopCri.Contains(drPromotion["Shop_ID"].ToString()))
                    {
                        int shopid = Convert.ToInt32(drPromotion["Shop_ID"].ToString());
                        foreach (DataRow dr in drPromotionWithSameShop)
                        {
                            string[] campaingTypeList = dr["Campaign_TypeID"].ToString().Split(',');
                            foreach (string campaingType in campaingTypeList)
                            {
                                DataTable dt = new DataTable();
                                dtSelect.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), "select_R", shopid));
                            }
                        }
                    }
                    string shop_id = drPromotion["Shop_ID"].ToString();
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string filename = shop_id + "_" + date;
                    string attname = null; string attn = null;
                    if (dtSelect != null && dtSelect.Rows.Count > 0)
                    {
                        using (StreamWriter writer = new StreamWriter(ExportCSVPath + "select$" + filename + String.Format("_{0}_R.csv", 1.ToString()), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtSelect, writer, true);
                        }
                        using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "select$" + filename + String.Format("_{0}_R.csv", 1.ToString()), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dtSelect, writer, true);
                            dtSelect.Rows.Clear();
                        }
                        Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                        itemExportQII.File_Name = "select$" + filename + String.Format("_{0}_R.csv", 1.ToString());
                        itemExportQII.File_Type = 0;
                        itemExportQII.ShopID = int.Parse(shop_id);
                        itemExportQII.IsExport = 0;
                        itemExportQII.Created_Date = DateTime.Now;
                        itemExportQII.Export_Type = 2;
                        InsertItemExportQ(itemExportQII);
                    }
                    shopCri += drPromotion["Shop_ID"].ToString() + ",";
                }
            }//if condition
        }//Updated date 2015/08/11

        private static void CreateCSVsForRakutenOld(DataTable dtPromotionCri)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            dtPromotionCri.Columns.Add("Shop_ID", typeof(int));
            for (int i = 0; i < dtPromotionCri.Rows.Count; i++)
            {
                dtShopList = promotionShopBL.GetShopList(dtPromotionCri.Rows[i]["ID"].ToString());
                List<DataRow> rows_to_remove = new List<DataRow>();
                foreach (DataRow row in dtShopList.Rows)
                {
                    if (int.Parse(row["Mall_ID"].ToString()) != 1)
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
            DataSet dsItem = new DataSet();
            for (int i = 0; i < 2; i++)//i<9
            {
                dsItem.Tables.Add("dtCampaingType" + i.ToString());
            }
            //For Attachment Pro
            DataTable dsItematt = new DataTable();
            DataTable dsItemattCabinet = new DataTable();
            DataTable dtproatt = new DataTable();
            DataTable dtproatts = new DataTable();
            string shopCri = "";
            Change_Templates changeTemplates = new Change_Templates();
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
                    DataRow[] drPromotionWithSameShop = dtPromotionCri.Select("Shop_ID = " + drPromotion["Shop_ID"].ToString());
                    DataTable dtcat = new DataTable();
                    DataTable dtSelect = new DataTable();
                    //For  Attachment
                    int pID = Convert.ToInt32(drPromotion["ID"].ToString());
                    dtproatt = promotionBL.SelectAttachmentProCSV(pID, 0);
                    if (dtproatt.Columns.Contains("Shop_ID")) { }
                    else
                    {
                        DataColumn newcol = new DataColumn("Shop_ID", typeof(int));
                        newcol.DefaultValue = (int)drPromotion["Shop_ID"];
                        dtproatt.Columns.Add(newcol);
                    }
                    if (dtproatt != null)
                        dsItematt.Merge(dtproatt);
                    dtproatts = promotionBL.SelectAttachmentProCSV(pID, 1);
                    if (dtproatts.Columns.Contains("Shop_ID")) { }
                    else
                    {
                        DataColumn newcols = new DataColumn("Shop_ID", typeof(int));
                        newcols.DefaultValue = (int)drPromotion["Shop_ID"];
                        dtproatts.Columns.Add(newcols);
                    }
                    if (dtproatts != null)
                        dsItemattCabinet.Merge(dtproatts);

                    if (!shopCri.Contains(drPromotion["Shop_ID"].ToString()))
                    {
                        foreach (DataRow dr in drPromotionWithSameShop)
                        {
                            DataTable dt = new DataTable();
                            dt = changeTemplates.ModifyTable(GetRakutenDataByCampaingType(dr["ID"].ToString(), "item", int.Parse(drPromotion["Shop_ID"].ToString())), int.Parse(drPromotion["Shop_ID"].ToString()));
                            if (dt != null)
                            {
                                DataColumn newColunmsd = new DataColumn("Shop_ID", typeof(System.Int32));
                                newColunmsd.DefaultValue = Convert.ToInt32(int.Parse(drPromotion["Shop_ID"].ToString()));
                                dt.Columns.Add(newColunmsd);
                                dsItem.Tables[1].Merge(dt);
                                dt.Columns.Remove("Shop_ID");
                                dt.AcceptChanges();
                                dt.Rows.Clear();
                            }
                            dtSelect.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), "select", int.Parse(drPromotion["Shop_ID"].ToString())));
                        }
                        string ctrl = null; string ctype = null;
                        if (dtSelect != null && dtSelect.Rows.Count > 0 && dsItem.Tables[1] != null && dsItem.Tables[1].Rows.Count > 0)
                        {
                            ctrl = dtSelect.Rows[0]["項目選択肢用コントロールカラム"].ToString();
                            ctype = dtSelect.Rows[0]["選択肢タイプ"].ToString();
                            promotionBL.Rakuten_Campaignlog_Insert(null, null, 0, id, null, dsItem.Tables[1], ctrl, ctype);
                        }
                        if (dsItem.Tables[1] != null && dsItem.Tables[1].Rows.Count > 0)
                        {
                            dsItem.Tables[1].Columns.Remove("PID");
                            dsItem.Tables[1].Columns.Remove("Shop_ID");
                            dsItem.Tables[1].AcceptChanges();
                        }
                    }
                    string shop_id = drPromotion["Shop_ID"].ToString();
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string filename = shop_id + "_" + date;
                    string attname = null; string attn = null;
                    for (int i = 0; i < dsItem.Tables.Count; i++)
                    {
                        if (dsItem.Tables[i].Rows.Count > 0)
                        {
                            if (i == 2) { break; }
                            using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                            {
                                WriteDataTable(dsItem.Tables[i], writer, true);
                            }
                            using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                            {
                                WriteDataTable(dsItem.Tables[i], writer, true);
                                dsItem.Tables[i].Rows.Clear();
                            }
                            Item_ExportQ_Entity itemExportQ = new Item_ExportQ_Entity();
                            itemExportQ.File_Name = "item$" + filename + String.Format("_{0}.csv", i.ToString());
                            itemExportQ.File_Type = 0;
                            itemExportQ.ShopID = int.Parse(shop_id);
                            itemExportQ.IsExport = 0;
                            itemExportQ.Created_Date = DateTime.Now;
                            itemExportQ.Export_Type = 2;
                            InsertItemExportQ(itemExportQ);
                            if (i == 1)
                            {
                                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "select$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dtSelect, writer, true);
                                }
                                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "select$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dtSelect, writer, true);
                                    dsItem.Tables[i].Rows.Clear();
                                }
                                Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                                itemExportQII.File_Name = "select$" + filename + String.Format("_{0}.csv", i.ToString());
                                itemExportQII.File_Type = 0;
                                itemExportQII.ShopID = int.Parse(shop_id);
                                itemExportQII.IsExport = 0;
                                itemExportQII.Created_Date = DateTime.Now;
                                itemExportQII.Export_Type = 2;
                                InsertItemExportQ(itemExportQII);
                            }//i=8
                        }//
                        #region
                        //Attachment
                        if (i != 0)
                        {
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
                            if (dsItemattCabinet.Rows.Count > 0)
                            {
                                for (int k = 0; k < dsItemattCabinet.Rows.Count; k++)
                                {
                                    string fname = dsItemattCabinet.Rows[k]["File_Name"].ToString();
                                    if (fname.Contains('.'))
                                    {
                                        string[] att = fname.Split('.');
                                        attn = att[0].ToString();
                                        attname = att[att.Length - 1].ToString();
                                    }
                                    if (Move(fname) == true)
                                    {
                                        Item_ExportQ_Entity itemExportQ = new Item_ExportQ_Entity();
                                        itemExportQ.File_Name = attn + "$" + filename + String.Format("_{0}", k.ToString()) + "." + attname;
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
                                dsItemattCabinet.Rows.Clear();
                            }
                        #endregion
                            shopCri += drPromotion["Shop_ID"].ToString() + ",";
                        }
                    }
                }//if condition
            }
        }

        private static void CreateRemoveCSVsForRakutenOld(DataTable dtPromotionCri)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            dtPromotionCri.Columns.Add("Shop_ID", typeof(int));
            for (int i = 0; i < dtPromotionCri.Rows.Count; i++)
            {
                dtShopList = promotionShopBL.GetShopList(dtPromotionCri.Rows[i]["ID"].ToString());
                List<DataRow> rows_to_remove = new List<DataRow>();
                foreach (DataRow row in dtShopList.Rows)
                {
                    if (int.Parse(row["Mall_ID"].ToString()) != 1)
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
                    for (int j = 0; j < dtShopList.Rows.Count - 1; j++)
                    {
                        dtPromotionCri.Rows.Add(dtPromotionCri.Rows[0].ItemArray);
                    }
                    foreach (DataRow dr in dtShopList.Rows)
                    {
                        dtPromotionCri.Rows[i]["Shop_ID"] = dr["ID"].ToString();
                        i++;
                    }
                }
                else
                {
                    if (dtShopList.Rows.Count > 0)
                    {
                        dtPromotionCri.Rows[i]["Shop_ID"] = dtShopList.Rows[0]["ID"].ToString();
                    }
                }
            }
            DataSet dsItem = new DataSet();
            for (int i = 0; i < 2; i++)
            {
                dsItem.Tables.Add("dtCampaingType" + i.ToString());
            }
            string shopCri = "";
            //for Attachment
            DataTable dsItematt = new DataTable();
            DataTable dtproatt = new DataTable();
            foreach (DataRow drPromotion in dtPromotionCri.Rows)
            {
                if (!String.IsNullOrWhiteSpace(drPromotion["Shop_ID"].ToString()))
                {
                    string sid = null; string ids = null;
                    for (int j = 0; j < dtPromotionCri.Rows.Count; j++)
                    {
                        sid += drPromotion["Shop_ID"].ToString() + ',';
                        ids += dtPromotionCri.Rows[j]["ID"].ToString() + ',';
                    }
                    if (!String.IsNullOrWhiteSpace(ids) && !String.IsNullOrWhiteSpace(sid))
                    {
                    }
                    //For  Attachment
                    int pID = Convert.ToInt32(drPromotion["ID"].ToString());
                    dtproatt = promotionBL.SelectAttachmentProCSV(pID, 0);
                    if (dtproatt.Columns.Contains("Shop_ID")) { }
                    else
                    {
                        DataColumn newcol = new DataColumn("Shop_ID", typeof(int));
                        newcol.DefaultValue = (int)drPromotion["Shop_ID"];
                        dtproatt.Columns.Add(newcol);
                    }
                    if (dtproatt != null)
                        dsItematt.Merge(dtproatt);
                    DataRow[] drPromotionWithSameShop = dtPromotionCri.Select("Shop_ID = " + drPromotion["Shop_ID"].ToString());
                    DataTable dtSelect = new DataTable();
                    if (!shopCri.Contains(drPromotion["Shop_ID"].ToString()))
                    {
                        int shopid = Convert.ToInt32(drPromotion["Shop_ID"].ToString());
                        foreach (DataRow dr in drPromotionWithSameShop)
                        {
                            string[] campaingTypeList = dr["Campaign_TypeID"].ToString().Split(',');
                            foreach (string campaingType in campaingTypeList)
                            {
                                DataTable dt = new DataTable();
                                dt = GetRakutenDataByCampaingType(dr["ID"].ToString(), "item_R", shopid);
                                if (dt != null)
                                {
                                    DataColumn newColunms = new DataColumn("モバイル用キャッチコピー", typeof(System.String));
                                    newColunms.DefaultValue = DBNull.Value;
                                    dt.Columns.Add(newColunms);
                                    DataColumn newColunmsd = new DataColumn("Shop_ID", typeof(System.Int32));
                                    newColunmsd.DefaultValue = Convert.ToInt32(int.Parse(drPromotion["Shop_ID"].ToString()));
                                    dt.Columns.Add(newColunmsd);
                                    dsItem.Tables[1].Merge(dt);
                                    dt.Columns.Remove("Shop_ID");
                                    dt.Columns.Remove("モバイル用キャッチコピー");
                                    dt.AcceptChanges();
                                    dt.Rows.Clear();
                                }
                                dtSelect.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), "select_R", shopid));
                            }
                        }

                        string ctrl = null; string ctype = null;
                        if ((dtSelect != null && dtSelect.Rows.Count > 0) || (dsItem.Tables[1] != null && dsItem.Tables[1].Rows.Count > 0))
                        {
                            if ((dtSelect != null && dtSelect.Rows.Count > 0))
                            {
                                ctrl = dtSelect.Rows[0]["項目選択肢用コントロールカラム"].ToString();
                                ctype = dtSelect.Rows[0]["選択肢タイプ"].ToString();
                            }
                            promotionBL.Rakuten_Campaignlog_Insert(null, null, 1, ids, null, dsItem.Tables[1], ctrl, ctype);
                        }
                        if (dsItem.Tables[1] != null && dsItem.Tables[1].Rows.Count > 0)
                        {
                            dsItem.Tables[1].Columns.Remove("PID");
                            dsItem.Tables[1].Columns.Remove("Shop_ID");
                            dsItem.Tables[1].Columns.Remove("モバイル用キャッチコピー");
                            dsItem.Tables[1].AcceptChanges();
                        }
                    }
                    string shop_id = drPromotion["Shop_ID"].ToString();
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string filename = shop_id + "_" + date;
                    string attname = null; string attn = null;
                    for (int i = 0; i < dsItem.Tables.Count; i++)
                    {
                        if (dsItem.Tables[i].Rows.Count > 0)
                        {
                            if (i == 2) { break; }
                            Change_Templates changeTemplates = new Change_Templates();
                            using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}_R.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                            {
                                DataTable dt = changeTemplates.ModifyTable(dsItem.Tables[i], int.Parse(drPromotion["Shop_ID"].ToString()));
                                WriteDataTable(dt, writer, true);
                            }
                            using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}_R.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                            {
                                DataTable dt = changeTemplates.ModifyTable(dsItem.Tables[i], int.Parse(drPromotion["Shop_ID"].ToString()));
                                WriteDataTable(dt, writer, true);
                                dsItem.Tables[i].Rows.Clear();
                            }
                            Item_ExportQ_Entity itemExportQ = new Item_ExportQ_Entity();
                            itemExportQ.File_Name = "item$" + filename + String.Format("_{0}_R.csv", i.ToString());
                            itemExportQ.File_Type = 0;
                            itemExportQ.ShopID = int.Parse(shop_id);
                            itemExportQ.IsExport = 0;
                            itemExportQ.Created_Date = DateTime.Now;
                            itemExportQ.Export_Type = 2;
                            InsertItemExportQ(itemExportQ);
                            if (i == 1)
                            {
                                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "select$" + filename + String.Format("_{0}_R.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dtSelect, writer, true);
                                }
                                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "select$" + filename + String.Format("_{0}_R.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dtSelect, writer, true);
                                    dsItem.Tables[i].Rows.Clear();
                                }
                                Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                                itemExportQII.File_Name = "select$" + filename + String.Format("_{0}_R.csv", i.ToString());
                                itemExportQII.File_Type = 0;
                                itemExportQII.ShopID = int.Parse(shop_id);
                                itemExportQII.IsExport = 0;
                                itemExportQII.Created_Date = DateTime.Now;
                                itemExportQII.Export_Type = 2;
                                InsertItemExportQ(itemExportQII);
                            }
                        }//if
                        //Attachment
                        if (i != 0)
                        {
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
                                        itemExportQ.File_Name = attn + "$" + filename + String.Format("_{0}_R", y.ToString()) + "." + attname;
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
                        }
                    }///for
                    shopCri += drPromotion["Shop_ID"].ToString() + ",";
                }
            }//if condition
        }

        private static DataTable GetRakutenDataByCampaingType(string promotionID, string option, int shop_id)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtItem = promotionBL.GetPromotionForRakuten(promotionID, option, shop_id);
            return dtItem;
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

        private static void DeleteItem_Option(int pid,int shopid)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            promotionBL.Delete_ItemOption_For_New(pid,shopid);
        }

        private static DataTable SelectOptionValue(int pid)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            return promotionBL.SelectOptionValue(pid);
        }

        private static void InsertItem_Option(DataTable dtselect, int pid,int shopid)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            promotionBL.Insert_ItemOption_For_New(dtselect, pid,shopid);
        }
    }
}
