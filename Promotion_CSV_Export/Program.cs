/*
 * Created By            : Aung Kyaw
Created Date          : 
Updated By             :Kay Thi Aung
Updated Date         :
 * For Ccmpaign type    : 0 =商品別ポイント
                                            1=店舗別ポイント
                                            2=商品別クーポン
                                            3=送料
                                            4=即日出荷
                                            5=予約
                                            6=事前告知
                                            7=シークレット
                                            8=プレゼントキャンペーン
 * 
 *  * Storedprocedure using:SP_CampaingPromotion_GetDataForRakuten
 *                                           -SP_CampaingPromotion_GetDataForYahoo
 *                                           -SP_CampaingPromotion_GetDataForPonpare
 *                                           -SP_Select_CampaingPromotionItemCode
 *                                           -SP_Select_CampaingPromotionItemCode
 *                                           -SP_Exhibition_Promotion_CampaingInsert
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using CKSKS_BL;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CKSKS_Common;

namespace Promotion_CSV_Export
{
    public class Program
    {
        static string ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        static string ExportCSVBackPath = ConfigurationManager.AppSettings["ExportCSVBackupPath"].ToString();
        static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        //for Attachment
        public static string rootFolderPath = ConfigurationManager.AppSettings["localFilePath"].ToString();


        private static void Main(string[] args)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtAddPromotion = promotionBL.SelectForAddCSV(1);
            //DataTable dtRemovePromotion = promotionBL.SelectForRemoveCSV();
            DataTable dtRemovePromotion = promotionBL.SelectForAddCSV(2);
            if (dtAddPromotion.Rows.Count > 0)
            {
                Export_Promotion_CSV(dtAddPromotion);
                //    for(int i=0;i<dtAddPromotion.Rows.Count;i++)
                //    {
                //        int id = Convert.ToInt32(dtAddPromotion.Rows[i]["ID"].ToString());
                //    string ItemCode = GetItem_Code(4,id);
                //    string ItemID = GetItem_ID(2,id);
                //    if (!String.IsNullOrWhiteSpace(ItemID) && !String.IsNullOrWhiteSpace(ItemCode))
                //    Exhibition_List_Insert(ItemID, ItemCode,0,ids);
                //}
                ChangeExportStatusFlag(1, dtAddPromotion);
            }
            if (dtRemovePromotion.Rows.Count > 0)
            {
                Remove_Promotion_CSV(dtRemovePromotion);
                //for (int j= 0; j< dtRemovePromotion.Rows.Count; j++)
                //{
                //    int ids = Convert.ToInt32(dtAddPromotion.Rows[j]["ID"].ToString());
                //    string ItemCode = GetItem_Code(3,ids);
                //    string ItemID = GetItem_ID(1,ids);
                //    if (!String.IsNullOrWhiteSpace(ItemID) && !String.IsNullOrWhiteSpace(ItemCode))
                //        Exhibition_List_Insert(ItemID, ItemCode, 1,ids,1);
                //}
                ChangeExportStatusFlag(2, dtRemovePromotion);
                ChangePromotionStatusFlag(1, dtRemovePromotion);
            }
        }

        private static void Export_Promotion_CSV(DataTable dtPromotionCri)
        {
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            DataTable dtRakuten = dtPromotionCri.Clone();
            DataTable dtPonpare = dtPromotionCri.Clone();
            DataTable dtYahoo = dtPromotionCri.Clone();
            DataTable dtJisha = dtPromotionCri.Clone();

            if (dtPromotionCri != null && dtPromotionCri.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPromotionCri.Rows)
                {
                    dtShopList = promotionShopBL.GetShopList(dr["ID"].ToString());
                    foreach (DataRow drShop in dtShopList.Rows)
                    {
                        if (drShop["Mall_ID"].ToString() == "1")
                        {
                            dtRakuten.Rows.Add(dr.ItemArray);
                        }
                        if (drShop["Mall_ID"].ToString() == "2")
                        {
                            dtYahoo.Rows.Add(dr.ItemArray);
                        }
                        if (drShop["Mall_ID"].ToString() == "3")
                        {
                            dtPonpare.Rows.Add(dr.ItemArray);
                        }
                        if (drShop["Mall_ID"].ToString() == "5")
                        {
                            dtJisha.Rows.Add(dr.ItemArray);
                        }
                    }
                }
            }

            if (dtRakuten.Rows.Count > 0)
            {
                CreateCSVsForRakuten(dtRakuten);
            }
            if (dtYahoo.Rows.Count > 0)
            {
                CreateCSVsForYahoo(dtYahoo, "Add");
            }
            if (dtPonpare.Rows.Count > 0)
            {
                CreateCSVsForPonpare(dtPonpare);
            }
            if (dtJisha.Rows.Count > 0)
            {
                CreateCSVsForJisha(dtJisha);
            }

        }



        private static void CreateCSVsForRakuten(DataTable dtPromotionCri)
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
                    for (int i = 0; i < dtPromotionCri.Rows.Count; i++)
                    {
                        int sid = Convert.ToInt32(drPromotion["Shop_ID"].ToString());
                        int id = Convert.ToInt32(dtPromotionCri.Rows[i]["ID"].ToString());
                        string ItemCode = GetItem_Code(4, id);
                        string ItemID = GetItem_ID(2, id);
                        if (!String.IsNullOrWhiteSpace(ItemID) && !String.IsNullOrWhiteSpace(ItemCode))
                            Exhibition_List_Insert(ItemID, ItemCode, 0, id, sid);
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
                                dsItem.Tables[1].Merge(dt);
                            dtSelect.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), "select", int.Parse(drPromotion["Shop_ID"].ToString())));

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
                                // dsItem.Tables[i].Rows.Clear();
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
                                    //dsItem.Tables[i].Rows.Clear();
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
                                        //itemExportQ.File_Name =  filename +"$"+ String.Format("_{0}", y.ToString()) + "." + attname;
                                        //itemExportQ.File_Name = "楽天GOLD$"  + filename +String.Format( "_{0}",y.ToString());
                                        itemExportQ.File_Type = 0;
                                        itemExportQ.ShopID = int.Parse(shop_id);
                                        itemExportQ.IsExport = 0;
                                        itemExportQ.Created_Date = DateTime.Now;
                                        itemExportQ.Export_Type = 2;
                                        InsertItemExportQ(itemExportQ);

                                        File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename

                                    }
                                }

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
                                        //itemExportQ.File_Name = "楽天Cabinet$" + filename + String.Format("_{0}", k.ToString())+"."+attname;;
                                        itemExportQ.File_Name = attn + "$" + filename + String.Format("_{0}", k.ToString()) + "." + attname;
                                        itemExportQ.File_Type = 0;
                                        itemExportQ.ShopID = int.Parse(shop_id);
                                        itemExportQ.IsExport = 0;
                                        itemExportQ.Created_Date = DateTime.Now;
                                        itemExportQ.Export_Type = 2;
                                        InsertItemExportQ(itemExportQ);

                                        File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename

                                    }
                                }

                            }
                        #endregion
                            shopCri += drPromotion["Shop_ID"].ToString() + ",";
                        }


                    }

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

                    for (int j = 0; j < dtPromotionCri.Rows.Count; j++)
                    {
                        int sid = Convert.ToInt32(drPromotion["Shop_ID"].ToString());
                        int ids = Convert.ToInt32(dtPromotionCri.Rows[j]["ID"].ToString());
                        string ItemCode = GetItem_Code(3, ids);
                        string ItemID = GetItem_ID(1, ids);
                        if (!String.IsNullOrWhiteSpace(ItemID) && !String.IsNullOrWhiteSpace(ItemCode))
                            Exhibition_List_Insert(ItemID, ItemCode, 1, ids, sid);
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
                                    dsItem.Tables[1].Merge(dt);
                                dtSelect.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), "select_R", shopid));

                            }
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
                                //dsItem.Tables[i].Rows.Clear();
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
                                    //dsItem.Tables[i].Rows.Clear();
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
                                        //itemExportQ.File_Name =  filename +"$"+ String.Format("_{0}", y.ToString()) + "." + attname;
                                        //itemExportQ.File_Name = "楽天GOLD$"  + filename +String.Format( "_{0}",y.ToString());
                                        itemExportQ.File_Type = 0;
                                        itemExportQ.ShopID = int.Parse(shop_id);
                                        itemExportQ.IsExport = 0;
                                        itemExportQ.Created_Date = DateTime.Now;
                                        itemExportQ.Export_Type = 2;
                                        InsertItemExportQ(itemExportQ);

                                        File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename

                                    }
                                }
                            }
                        }
                    }///for

                    shopCri += drPromotion["Shop_ID"].ToString() + ",";
                }
            }//if condition
        }
        private static void CreateCSVsForPonpare(DataTable dtPromotionCri)
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
                    if (int.Parse(row["Mall_ID"].ToString()) != 3)
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
                }
                else
                {
                    dtPromotionCri.Rows[i]["Shop_ID"] = dtShopList.Rows[0]["ID"].ToString();
                }
            }

            DataSet dsItem = new DataSet();
            for (int i = 0; i < 2; i++)
            {
                dsItem.Tables.Add("dtCampaingType" + i.ToString());
            }
            string shopCri = "";
            Change_Templates changeTemplates = new Change_Templates();
            DataTable dsItematt = new DataTable();

            foreach (DataRow drPromotion in dtPromotionCri.Rows)
            {
                #region
                for (int i = 0; i < dtPromotionCri.Rows.Count; i++)
                {
                    int sid = Convert.ToInt32(drPromotion["Shop_ID"].ToString());
                    int id = Convert.ToInt32(dtPromotionCri.Rows[i]["ID"].ToString());
                    string ItemCode = GetItem_Code(4, id);
                    string ItemID = GetItem_ID(2, id);
                    if (!String.IsNullOrWhiteSpace(ItemID) && !String.IsNullOrWhiteSpace(ItemCode))
                        Exhibition_List_Insert(ItemID, ItemCode, 0, id, sid);
                }
                #endregion

                DataRow[] drPromotionWithSameShop = dtPromotionCri.Select("Shop_ID = " + drPromotion["Shop_ID"].ToString());
                DataTable dtSelect = new DataTable();
                DataTable dtSelectitem = new DataTable();
                DataTable dtitemtype = new DataTable();
                if (!shopCri.Contains(drPromotion["Shop_ID"].ToString()))
                {
                    int pID = Convert.ToInt32(drPromotion["ID"].ToString());
                    DataTable dtproatt = promotionBL.SelectAttachmentProCSV(pID, 3);
                    DataColumn newcol = new DataColumn("Shop_ID", typeof(int));
                    newcol.DefaultValue = (int)drPromotion["Shop_ID"];
                    dtproatt.Columns.Add(newcol);
                    if (dtproatt != null)
                        dsItematt.Merge(dtproatt);

                    foreach (DataRow dr in drPromotionWithSameShop)
                    {
                        string[] campaingTypeList = dr["Campaign_TypeID"].ToString().Split(',');

                        foreach (string campaingType in campaingTypeList)
                        {
                            DataTable dt = new DataTable();

                            dt = changeTemplates.ModifyTable(GetPonpareDataByCampaingType(dr["ID"].ToString(), "item"), int.Parse(drPromotion["Shop_ID"].ToString()));
                            if (dt != null)
                                dsItem.Tables[1].Merge(dt);
                            dtSelect.Merge(GetPonpareDataByCampaingType(dr["ID"].ToString(), "option"));

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
                                //  dsItem.Tables[i].Rows.Clear();
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
                                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "option$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dtSelect, writer, true);
                                    // dsItem.Tables[i].Rows.Clear();
                                }
                                using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "option$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dtSelect, writer, true);
                                    dsItem.Tables[i].Rows.Clear();
                                }
                                Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                                itemExportQII.File_Name = "option$" + filename + String.Format("_{0}.csv", i.ToString());
                                itemExportQII.File_Type = 0;
                                itemExportQII.ShopID = int.Parse(shop_id);
                                itemExportQII.IsExport = 0;
                                itemExportQII.Created_Date = DateTime.Now;
                                itemExportQII.Export_Type = 2;
                                InsertItemExportQ(itemExportQII);
                            }//i =8




                        }////

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
                                        //itemExportQ.File_Name = "geocities" + filename + String.Format("_{0}", y.ToString())+ "." + attname;
                                        itemExportQ.File_Type = 0;
                                        itemExportQ.ShopID = int.Parse(shop_id);
                                        itemExportQ.IsExport = 0;
                                        itemExportQ.Created_Date = DateTime.Now;
                                        itemExportQ.Export_Type = 2;
                                        InsertItemExportQ(itemExportQ);
                                        File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename
                                    }
                                }

                            }
                        }
                        shopCri += drPromotion["Shop_ID"].ToString() + ",";
                    }

                }
            }
        }
        private static void CreateRemoveCSVsForPonpare(DataTable dtPromotionCri)
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
                    if (int.Parse(row["Mall_ID"].ToString()) != 3)
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
            DataTable dsItematt = new DataTable();
            foreach (DataRow drPromotion in dtPromotionCri.Rows)
            {
                #region
                for (int j = 0; j < dtPromotionCri.Rows.Count; j++)
                {
                    int sid = Convert.ToInt32(drPromotion["Shop_ID"].ToString());
                    int ids = Convert.ToInt32(dtPromotionCri.Rows[j]["ID"].ToString());
                    string ItemCode = GetItem_Code(3, ids);
                    string ItemID = GetItem_ID(1, ids);
                    if (!String.IsNullOrWhiteSpace(ItemID) && !String.IsNullOrWhiteSpace(ItemCode))
                        Exhibition_List_Insert(ItemID, ItemCode, 1, ids, sid);
                }
                #endregion
                DataRow[] drPromotionWithSameShop = dtPromotionCri.Select("Shop_ID = " + drPromotion["Shop_ID"].ToString());
                DataTable dtSelect = new DataTable();
                if (!shopCri.Contains(drPromotion["Shop_ID"].ToString()))
                {

                    int pID = Convert.ToInt32(drPromotion["ID"].ToString());
                    DataTable dtproatt = promotionBL.SelectAttachmentProCSV(pID, 3);
                    DataColumn newcol = new DataColumn("Shop_ID", typeof(int));
                    newcol.DefaultValue = (int)drPromotion["Shop_ID"];
                    dtproatt.Columns.Add(newcol);
                    if (dtproatt != null)
                        dsItematt.Merge(dtproatt);

                    foreach (DataRow dr in drPromotionWithSameShop)
                    {
                        DataTable dt = new DataTable();
                        dt = GetPonpareDataByCampaingType(dr["ID"].ToString(), "item_R");
                        if (dt != null)
                            dsItem.Tables[1].Merge(dt);
                        dtSelect.Merge(GetPonpareDataByCampaingType(dr["ID"].ToString(), "option_R"));
                        break;

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
                            // dsItem.Tables[i].Rows.Clear();
                        }
                        using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "item$" + filename + String.Format("_{0}_R.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                        {
                            DataTable dt = changeTemplates.ModifyTable(dsItem.Tables[i], int.Parse(drPromotion["Shop_ID"].ToString()));
                            WriteDataTable(dt, writer, true);
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
                            using (StreamWriter writer = new StreamWriter(ExportCSVPath + "option$" + filename + String.Format("_{0}_R.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                            {
                                WriteDataTable(dtSelect, writer, true);

                            }
                            using (StreamWriter writer = new StreamWriter(ExportCSVBackPath + "option$" + filename + String.Format("_{0}_R.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                            {
                                WriteDataTable(dtSelect, writer, true);
                                dsItem.Tables[i].Rows.Clear();
                            }
                            Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                            itemExportQII.File_Name = "option$" + filename + String.Format("_{0}.csv", i.ToString());
                            itemExportQII.File_Type = 0;
                            itemExportQII.ShopID = int.Parse(shop_id);
                            itemExportQII.IsExport = 0;
                            itemExportQII.Created_Date = DateTime.Now;
                            itemExportQII.Export_Type = 2;
                            InsertItemExportQ(itemExportQII);
                        }
                    }


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
                                    //itemExportQ.File_Name = "geocities" + filename + String.Format("_{0}", y.ToString())+ "." + attname;
                                    itemExportQ.File_Type = 0;
                                    itemExportQ.ShopID = int.Parse(shop_id);
                                    itemExportQ.IsExport = 0;
                                    itemExportQ.Created_Date = DateTime.Now;
                                    itemExportQ.Export_Type = 2;
                                    InsertItemExportQ(itemExportQ);
                                    File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename
                                }
                            }

                        }
                    }/////
                }


                shopCri += drPromotion["Shop_ID"].ToString() + ",";
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
                    for (int i = 0; i < dtPromotionCri.Rows.Count; i++)
                    {
                        int sid = Convert.ToInt32(drPromotion["Shop_ID"].ToString());
                        int id = Convert.ToInt32(dtPromotionCri.Rows[i]["ID"].ToString());
                        string ItemCode = GetItem_Code(4, id);
                        string ItemID = GetItem_ID(2, id);
                        if (!String.IsNullOrWhiteSpace(ItemID) && !String.IsNullOrWhiteSpace(ItemCode))
                            Exhibition_List_Insert(ItemID, ItemCode, 0, id, sid);
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


                    DataRow[] drPromotionWithSameShop = dtPromotionCri.Select("Shop_ID = " + drPromotion["Shop_ID"].ToString());
                    if (!shopCri.Contains(drPromotion["Shop_ID"].ToString()))
                    {
                        foreach (DataRow dr in drPromotionWithSameShop)
                        {
                            if (status.ToLower() == "add")
                            {
                                DataTable dtTmp = promotionBL.GetPromotionForYahoo(dr["ID"].ToString(), "item", drPromotion["Shop_ID"].ToString());
                                if (dtTmp.Rows.Count > 0)
                                    dt.Merge(dtTmp);
                            }
                            else
                            {
                                DataTable dtTmp = promotionBL.GetPromotionForYahoo(dr["ID"].ToString(), "item_remove", drPromotion["Shop_ID"].ToString());
                                dt.Merge(dtTmp);
                            }
                        }
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Change_Templates changeTemplates = new Change_Templates();
                            dt = changeTemplates.ModifyTable(dt, int.Parse(drPromotion["Shop_ID"].ToString()));
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
                                        //itemExportQ.File_Name = "geocities" + filename + String.Format("_{0}", y.ToString())+ "." + attname;
                                        itemExportQ.File_Name = attn + "$" + filename + String.Format("_{0}", y.ToString()) + "." + attname;
                                        itemExportQ.File_Type = 0;
                                        itemExportQ.ShopID = int.Parse(shop_id);
                                        itemExportQ.IsExport = 0;
                                        itemExportQ.Created_Date = DateTime.Now;
                                        itemExportQ.Export_Type = 2;
                                        InsertItemExportQ(itemExportQ);
                                        File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename
                                    }
                                }

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
                                        //itemExportQ.File_Name = "geocities" + filename + String.Format("_{0}", y.ToString())+ "." + attname;
                                        itemExportQ.File_Name = attn + "$" + filename + String.Format("_{0}", y.ToString()) + "." + attname;
                                        itemExportQ.File_Type = 0;
                                        itemExportQ.ShopID = int.Parse(shop_id);
                                        itemExportQ.IsExport = 0;
                                        itemExportQ.Created_Date = DateTime.Now;
                                        itemExportQ.Export_Type = 2;
                                        InsertItemExportQ(itemExportQ);
                                        File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename
                                    }
                                }

                            }
                            #endregion
                        }
                        shopCri += drPromotion["Shop_ID"].ToString() + ",";
                    }
                }//if condition for null check
            }//if condition
        }
        private static void CreateCSVsForJisha(DataTable dtPromotionCri)
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
                    if (int.Parse(row["Mall_ID"].ToString()) != 5)
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
                    for (int i = 0; i < dtPromotionCri.Rows.Count; i++)
                    {
                        int sid = Convert.ToInt32(drPromotion["Shop_ID"].ToString());
                        int id = Convert.ToInt32(dtPromotionCri.Rows[i]["ID"].ToString());
                        string ItemCode = GetItem_Code(4, id);
                        string ItemID = GetItem_ID(2, id);
                        if (!String.IsNullOrWhiteSpace(ItemID) && !String.IsNullOrWhiteSpace(ItemCode))
                            Exhibition_List_Insert(ItemID, ItemCode, 0, id, sid);
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
                                dsItem.Tables[1].Merge(dt);
                            dtSelect.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), "select", int.Parse(drPromotion["Shop_ID"].ToString())));

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
                                // dsItem.Tables[i].Rows.Clear();
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
                                    //dsItem.Tables[i].Rows.Clear();
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
                                        //itemExportQ.File_Name =  filename +"$"+ String.Format("_{0}", y.ToString()) + "." + attname;
                                        //itemExportQ.File_Name = "楽天GOLD$"  + filename +String.Format( "_{0}",y.ToString());
                                        itemExportQ.File_Type = 0;
                                        itemExportQ.ShopID = int.Parse(shop_id);
                                        itemExportQ.IsExport = 0;
                                        itemExportQ.Created_Date = DateTime.Now;
                                        itemExportQ.Export_Type = 2;
                                        InsertItemExportQ(itemExportQ);

                                        File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename

                                    }
                                }

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
                                        //itemExportQ.File_Name = "楽天Cabinet$" + filename + String.Format("_{0}", k.ToString())+"."+attname;;
                                        itemExportQ.File_Name = attn + "$" + filename + String.Format("_{0}", k.ToString()) + "." + attname;
                                        itemExportQ.File_Type = 0;
                                        itemExportQ.ShopID = int.Parse(shop_id);
                                        itemExportQ.IsExport = 0;
                                        itemExportQ.Created_Date = DateTime.Now;
                                        itemExportQ.Export_Type = 2;
                                        InsertItemExportQ(itemExportQ);

                                        File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename

                                    }
                                }

                            }
                        #endregion
                            shopCri += drPromotion["Shop_ID"].ToString() + ",";
                        }


                    }

                }//if condition
            }
        }
        private static void Remove_Promotion_CSV(DataTable dtPromotionCri)
        {
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            DataTable dtRakuten = dtPromotionCri.Clone();
            DataTable dtPonpare = dtPromotionCri.Clone();
            DataTable dtYahoo = dtPromotionCri.Clone();
            string promotionList = "";
            string shopID = "";
            if (dtPromotionCri != null && dtPromotionCri.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPromotionCri.Rows)
                {
                    dtShopList = promotionShopBL.GetShopList(dr["ID"].ToString());
                    foreach (DataRow drShop in dtShopList.Rows)
                    {
                        if (drShop["Mall_ID"].ToString() == "1")
                        {
                            dtRakuten.Rows.Add(dr.ItemArray);
                        }
                        if (drShop["Mall_ID"].ToString() == "2")
                        {
                            dtYahoo.Rows.Add(dr.ItemArray);
                        }
                        if (drShop["Mall_ID"].ToString() == "3")
                        {
                            dtPonpare.Rows.Add(dr.ItemArray);
                        }
                    }
                }
            }

            if (dtRakuten.Rows.Count > 0)
            {
                CreateRemoveCSVsForRakuten(dtRakuten);
            }
            if (dtYahoo.Rows.Count > 0)
            {
                CreateCSVsForYahoo(dtYahoo, "Remove");
            }
            if (dtPonpare.Rows.Count > 0)
            {
                CreateRemoveCSVsForPonpare(dtPonpare);
            }

        }

        private static void CreateCSVsForRakutenold(DataTable dtPromotionCri)
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
            for (int i = 0; i < 9; i++)//i<9
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
                            string[] campaingTypeList = dr["Campaign_TypeID"].ToString().Split(',');

                            foreach (string campaingType in campaingTypeList)
                            {
                                DataTable dt = new DataTable();

                                switch (campaingType)
                                {
                                    case "0":
                                        dt = changeTemplates.ModifyTable(GetRakutenDataByCampaingType(dr["ID"].ToString(), "item_0", int.Parse(drPromotion["Shop_ID"].ToString())), int.Parse(drPromotion["Shop_ID"].ToString()));
                                        if (dt != null)
                                            dsItem.Tables[0].Merge(dt);
                                        break;
                                    case "1":
                                        dt = changeTemplates.ModifyTable(GetRakutenDataByCampaingType(dr["ID"].ToString(), "item_1", int.Parse(drPromotion["Shop_ID"].ToString())), int.Parse(drPromotion["Shop_ID"].ToString()));
                                        if (dt != null)
                                            dsItem.Tables[1].Merge(dt);
                                        break;
                                    case "2":
                                        dt = changeTemplates.ModifyTable(GetRakutenDataByCampaingType(dr["ID"].ToString(), "item_2", int.Parse(drPromotion["Shop_ID"].ToString())), int.Parse(drPromotion["Shop_ID"].ToString()));
                                        if (dt != null)
                                            dsItem.Tables[2].Merge(dt);
                                        break;
                                    case "3":
                                        dt = changeTemplates.ModifyTable(GetRakutenDataByCampaingType(dr["ID"].ToString(), "item_3", int.Parse(drPromotion["Shop_ID"].ToString())), int.Parse(drPromotion["Shop_ID"].ToString()));
                                        if (dt != null)
                                            dsItem.Tables[3].Merge(dt);
                                        break;
                                    case "4":
                                        for (int b = 1; b < 4; b++)
                                        {
                                            string strname = null;
                                            switch (b)
                                            {
                                                case 1: strname = "item_4";
                                                    dt = changeTemplates.ModifyTable(GetRakutenDataByCampaingType(dr["ID"].ToString(), strname, int.Parse(drPromotion["Shop_ID"].ToString())), int.Parse(drPromotion["Shop_ID"].ToString()));
                                                    if (dt != null)
                                                        dsItem.Tables[4].Merge(dt);
                                                    break;
                                                case 2: strname = "item_4.2";

                                                    dt = changeTemplates.ModifyTable(GetRakutenDataByCampaingType(dr["ID"].ToString(), strname, int.Parse(drPromotion["Shop_ID"].ToString())), int.Parse(drPromotion["Shop_ID"].ToString()));
                                                    if (dt != null)
                                                        dtSelect.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), strname, int.Parse(drPromotion["Shop_ID"].ToString())));
                                                    // dsItem.Tables[9].Merge(dt);
                                                    break;
                                                case 3: strname = "item_4.3";
                                                    dt = GetRakutenDataByCampaingType(dr["ID"].ToString(), strname, int.Parse(drPromotion["Shop_ID"].ToString()));
                                                    if (dt != null)
                                                        // dsItem.Tables[10].Merge(dt);
                                                        dtcat.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), strname, int.Parse(drPromotion["Shop_ID"].ToString())));
                                                    break;
                                            }
                                            //dt = changeTemplates.ModifyTable(GetRakutenDataByCampaingType(dr["ID"].ToString(), strname, int.Parse(drPromotion["Shop_ID"].ToString())), int.Parse(drPromotion["Shop_ID"].ToString()));

                                        }
                                        break;
                                    case "5":
                                        dt = changeTemplates.ModifyTable(GetRakutenDataByCampaingType(dr["ID"].ToString(), "item_5", int.Parse(drPromotion["Shop_ID"].ToString())), int.Parse(drPromotion["Shop_ID"].ToString()));
                                        if (dt != null)
                                            dsItem.Tables[5].Merge(dt);
                                        break;
                                    case "6":
                                        dt = changeTemplates.ModifyTable(GetRakutenDataByCampaingType(dr["ID"].ToString(), "item_6", int.Parse(drPromotion["Shop_ID"].ToString())), int.Parse(drPromotion["Shop_ID"].ToString()));
                                        if (dt != null)
                                            dsItem.Tables[6].Merge(dt);
                                        break;
                                    case "7":
                                        dt = changeTemplates.ModifyTable(GetRakutenDataByCampaingType(dr["ID"].ToString(), "item_7", int.Parse(drPromotion["Shop_ID"].ToString())), int.Parse(drPromotion["Shop_ID"].ToString()));
                                        if (dt != null)
                                            dsItem.Tables[7].Merge(dt);
                                        break;
                                    case "8":
                                        dt = changeTemplates.ModifyTable(GetRakutenDataByCampaingType(dr["ID"].ToString(), "item_8", int.Parse(drPromotion["Shop_ID"].ToString())), int.Parse(drPromotion["Shop_ID"].ToString()));
                                        if (dt != null)
                                            dsItem.Tables[8].Merge(dt);
                                        dtSelect.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), "itemselect_8", int.Parse(drPromotion["Shop_ID"].ToString())));
                                        break;
                                }
                            }
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
                            using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
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

                            if (i == 8)
                            {
                                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item_select$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dtSelect, writer, true);
                                    dsItem.Tables[i].Rows.Clear();
                                }
                                Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                                itemExportQII.File_Name = "item_select$" + filename + String.Format("_{0}.csv", i.ToString());
                                itemExportQII.File_Type = 0;
                                itemExportQII.ShopID = int.Parse(shop_id);
                                itemExportQII.IsExport = 0;
                                itemExportQII.Created_Date = DateTime.Now;
                                itemExportQII.Export_Type = 2;
                                InsertItemExportQ(itemExportQII);
                            }//i=8

                            ///change logic
                            if (i == 4)
                            {
                                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "select$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dtSelect, writer, true);

                                }
                                Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                                itemExportQII.File_Name = "select$" + filename + String.Format("_{0}.csv", i.ToString());
                                itemExportQII.File_Type = 0;
                                itemExportQII.ShopID = int.Parse(shop_id);
                                itemExportQII.IsExport = 0;
                                itemExportQII.Created_Date = DateTime.Now;
                                itemExportQII.Export_Type = 2;
                                InsertItemExportQ(itemExportQII);



                                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item-cat$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dtcat, writer, true);
                                    // dsItem.Tables[i].Rows.Clear();
                                }

                                itemExportQII.File_Name = "item-cat$" + filename + String.Format("_{0}.csv", i.ToString());
                                itemExportQII.File_Type = 0;
                                itemExportQII.ShopID = int.Parse(shop_id);
                                itemExportQII.IsExport = 0;
                                itemExportQII.Created_Date = DateTime.Now;
                                itemExportQII.Export_Type = 2;
                                InsertItemExportQ(itemExportQII);

                                dsItem.Tables[i].Rows.Clear();
                            }//i=9


                        }
                    }//

                    #region
                    //Attachment

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
                                //itemExportQ.File_Name =  filename +"$"+ String.Format("_{0}", y.ToString()) + "." + attname;
                                //itemExportQ.File_Name = "楽天GOLD$"  + filename +String.Format( "_{0}",y.ToString());
                                itemExportQ.File_Type = 0;
                                itemExportQ.ShopID = int.Parse(shop_id);
                                itemExportQ.IsExport = 0;
                                itemExportQ.Created_Date = DateTime.Now;
                                itemExportQ.Export_Type = 2;
                                InsertItemExportQ(itemExportQ);
                                File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename
                            }
                        }

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
                                //itemExportQ.File_Name = "楽天Cabinet$" + filename + String.Format("_{0}", k.ToString())+"."+attname;;
                                itemExportQ.File_Name = attn + "$" + filename + String.Format("_{0}", k.ToString()) + "." + attname;
                                itemExportQ.File_Type = 0;
                                itemExportQ.ShopID = int.Parse(shop_id);
                                itemExportQ.IsExport = 0;
                                itemExportQ.Created_Date = DateTime.Now;
                                itemExportQ.Export_Type = 2;
                                InsertItemExportQ(itemExportQ);
                                File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename
                            }
                        }

                    }
                    #endregion
                    shopCri += drPromotion["Shop_ID"].ToString() + ",";
                }




            }//if condition
        }

        private static void CreateCSVsForPonpareold(DataTable dtPromotionCri)
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
                    if (int.Parse(row["Mall_ID"].ToString()) != 3)
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
                }
                else
                {
                    dtPromotionCri.Rows[i]["Shop_ID"] = dtShopList.Rows[0]["ID"].ToString();
                }
            }

            DataSet dsItem = new DataSet();
            for (int i = 0; i < 9; i++)
            {
                dsItem.Tables.Add("dtCampaingType" + i.ToString());
            }
            string shopCri = "";
            Change_Templates changeTemplates = new Change_Templates();
            DataTable dsItematt = new DataTable();

            foreach (DataRow drPromotion in dtPromotionCri.Rows)
            {
                DataRow[] drPromotionWithSameShop = dtPromotionCri.Select("Shop_ID = " + drPromotion["Shop_ID"].ToString());
                DataTable dtSelect = new DataTable();
                DataTable dtSelectitem = new DataTable();
                DataTable dtitemtype = new DataTable();
                if (!shopCri.Contains(drPromotion["Shop_ID"].ToString()))
                {
                    int pID = Convert.ToInt32(drPromotion["ID"].ToString());
                    DataTable dtproatt = promotionBL.SelectAttachmentProCSV(pID, 3);
                    DataColumn newcol = new DataColumn("Shop_ID", typeof(int));
                    newcol.DefaultValue = (int)drPromotion["Shop_ID"];
                    dtproatt.Columns.Add(newcol);
                    if (dtproatt != null)
                        dsItematt.Merge(dtproatt);

                    foreach (DataRow dr in drPromotionWithSameShop)
                    {
                        string[] campaingTypeList = dr["Campaign_TypeID"].ToString().Split(',');

                        foreach (string campaingType in campaingTypeList)
                        {
                            DataTable dt = new DataTable();
                            switch (campaingType)
                            {
                                case "0":
                                    for (int b = 1; b < 3; b++)
                                    {
                                        string strname = null;
                                        switch (b)
                                        {
                                            case 1: strname = "item_0";
                                                dt = changeTemplates.ModifyTable(GetPonpareDataByCampaingType(dr["ID"].ToString(), "item_0"), int.Parse(drPromotion["Shop_ID"].ToString()));
                                                if (dt != null)
                                                    dsItem.Tables[0].Merge(dt);
                                                break;

                                            case 2: strname = "itemselect";
                                                dt = changeTemplates.ModifyTable(GetPonpareDataByCampaingType(dr["ID"].ToString(), "itemselect"), int.Parse(drPromotion["Shop_ID"].ToString()));
                                                if (dt != null)
                                                    dtitemtype.Merge(GetPonpareDataByCampaingType(dr["ID"].ToString(), "itemselect"));
                                                break;
                                        }
                                    }
                                    break;
                                case "1":
                                    dt = changeTemplates.ModifyTable(GetPonpareDataByCampaingType(dr["ID"].ToString(), "item_1"), int.Parse(drPromotion["Shop_ID"].ToString()));
                                    if (dt != null)
                                        dsItem.Tables[1].Merge(dt);
                                    break;
                                case "2":
                                    dt = changeTemplates.ModifyTable(GetPonpareDataByCampaingType(dr["ID"].ToString(), "item_2"), int.Parse(drPromotion["Shop_ID"].ToString()));
                                    if (dt != null)
                                        dsItem.Tables[2].Merge(dt);
                                    break;
                                case "3":
                                    dt = changeTemplates.ModifyTable(GetPonpareDataByCampaingType(dr["ID"].ToString(), "item_3"), int.Parse(drPromotion["Shop_ID"].ToString()));
                                    if (dt != null)
                                        dsItem.Tables[3].Merge(dt);
                                    break;
                                case "4":
                                    for (int b = 1; b < 3; b++)
                                    {
                                        string strname = null;
                                        switch (b)
                                        {
                                            case 1: strname = "item_4";
                                                dt = changeTemplates.ModifyTable(GetPonpareDataByCampaingType(dr["ID"].ToString(), "item_4"), int.Parse(drPromotion["Shop_ID"].ToString()));
                                                if (dt != null)
                                                    dsItem.Tables[4].Merge(dt);
                                                break;

                                            case 2: strname = "item_4.1";
                                                dt = changeTemplates.ModifyTable(GetPonpareDataByCampaingType(dr["ID"].ToString(), "item_4.1"), int.Parse(drPromotion["Shop_ID"].ToString()));
                                                if (dt != null)
                                                    dtSelectitem.Merge(GetPonpareDataByCampaingType(dr["ID"].ToString(), strname));
                                                break;
                                        }
                                    }
                                    break;
                                case "5":
                                    dt = changeTemplates.ModifyTable(GetPonpareDataByCampaingType(dr["ID"].ToString(), "item_5"), int.Parse(drPromotion["Shop_ID"].ToString()));
                                    if (dt != null)
                                        dsItem.Tables[5].Merge(dt);
                                    break;
                                case "6":
                                    dt = changeTemplates.ModifyTable(GetPonpareDataByCampaingType(dr["ID"].ToString(), "item_6"), int.Parse(drPromotion["Shop_ID"].ToString()));
                                    if (dt != null)
                                        dsItem.Tables[6].Merge(dt);
                                    break;
                                case "7":
                                    dt = changeTemplates.ModifyTable(GetPonpareDataByCampaingType(dr["ID"].ToString(), "item_7"), int.Parse(drPromotion["Shop_ID"].ToString()));
                                    if (dt != null)
                                        dsItem.Tables[7].Merge(dt);
                                    break;
                                case "8":
                                    dt = changeTemplates.ModifyTable(GetPonpareDataByCampaingType(dr["ID"].ToString(), "item_8"), int.Parse(drPromotion["Shop_ID"].ToString()));
                                    if (dt != null)
                                        dsItem.Tables[8].Merge(dt);
                                    dtSelect.Merge(GetPonpareDataByCampaingType(dr["ID"].ToString(), "itemselect_8"));
                                    break;
                            }
                        }
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
                        using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
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

                        if (i == 8)
                        {
                            using (StreamWriter writer = new StreamWriter(ExportCSVPath + "option$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                            {
                                WriteDataTable(dtSelect, writer, true);
                                dsItem.Tables[i].Rows.Clear();
                            }
                            Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                            itemExportQII.File_Name = "option$" + filename + String.Format("_{0}.csv", i.ToString());
                            itemExportQII.File_Type = 0;
                            itemExportQII.ShopID = int.Parse(shop_id);
                            itemExportQII.IsExport = 0;
                            itemExportQII.Created_Date = DateTime.Now;
                            itemExportQII.Export_Type = 2;
                            InsertItemExportQ(itemExportQII);
                        }//i =8



                        if (i == 4)
                        {
                            using (StreamWriter writer = new StreamWriter(ExportCSVPath + "category$" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                            {
                                WriteDataTable(dtSelectitem, writer, true);
                                dsItem.Tables[i].Rows.Clear();
                            }
                            Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                            itemExportQII.File_Name = "category$" + filename + String.Format("_{0}.csv", i.ToString());
                            itemExportQII.File_Type = 0;
                            itemExportQII.ShopID = int.Parse(shop_id);
                            itemExportQII.IsExport = 0;
                            itemExportQII.Created_Date = DateTime.Now;
                            itemExportQII.Export_Type = 2;
                            InsertItemExportQ(itemExportQII);
                        }


                        if (i == 0)
                        {
                            using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}_1.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                            {
                                WriteDataTable(dtitemtype, writer, true);
                                dsItem.Tables[i].Rows.Clear();
                            }
                            Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                            itemExportQII.File_Name = "item$" + filename + String.Format("_{0}_1.csv", i.ToString());
                            itemExportQII.File_Type = 0;
                            itemExportQII.ShopID = int.Parse(shop_id);
                            itemExportQII.IsExport = 0;
                            itemExportQII.Created_Date = DateTime.Now;
                            itemExportQII.Export_Type = 2;
                            InsertItemExportQ(itemExportQII);
                        }

                    }
                }////

                //Attachment

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
                            //itemExportQ.File_Name = "geocities" + filename + String.Format("_{0}", y.ToString())+ "." + attname;
                            itemExportQ.File_Type = 0;
                            itemExportQ.ShopID = int.Parse(shop_id);
                            itemExportQ.IsExport = 0;
                            itemExportQ.Created_Date = DateTime.Now;
                            itemExportQ.Export_Type = 2;
                            InsertItemExportQ(itemExportQ);
                            File.Move(ExportCSVPath + fname, ExportCSVPath + itemExportQ.File_Name); //Rename
                        }
                    }

                }
                shopCri += drPromotion["Shop_ID"].ToString() + ",";
            }

        }
        private static void CreateRemoveCSVsForRakutenold(DataTable dtPromotionCri)
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
            for (int i = 0; i < 9; i++)
            {
                dsItem.Tables.Add("dtCampaingType" + i.ToString());
            }
            string shopCri = "";


            foreach (DataRow drPromotion in dtPromotionCri.Rows)
            {
                if (!String.IsNullOrWhiteSpace(drPromotion["Shop_ID"].ToString()))
                {
                    //for Attachment


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
                                switch (campaingType)
                                {
                                    case "0":
                                        dt = GetRakutenDataByCampaingType(dr["ID"].ToString(), "remove_item_0", shopid);
                                        if (dt != null)
                                            dsItem.Tables[0].Merge(dt);
                                        break;
                                    case "1":
                                        dt = GetRakutenDataByCampaingType(dr["ID"].ToString(), "remove_item_1", shopid);
                                        if (dt != null)
                                            dsItem.Tables[1].Merge(dt);
                                        break;
                                    case "2":
                                        dt = GetRakutenDataByCampaingType(dr["ID"].ToString(), "remove_item_2", shopid);
                                        if (dt != null)
                                            dsItem.Tables[2].Merge(dt);
                                        break;
                                    case "3":
                                        dt = GetRakutenDataByCampaingType(dr["ID"].ToString(), "remove_item_3", shopid);
                                        if (dt != null)
                                            dsItem.Tables[3].Merge(dt);
                                        break;
                                    case "4":
                                        dt = GetRakutenDataByCampaingType(dr["ID"].ToString(), "remove_item_4", shopid);
                                        if (dt != null)
                                            dsItem.Tables[4].Merge(dt);
                                        break;
                                    case "5":
                                        dt = GetRakutenDataByCampaingType(dr["ID"].ToString(), "remove_item_5", shopid);
                                        if (dt != null)
                                            dsItem.Tables[5].Merge(dt);
                                        break;
                                    case "6":
                                        dt = GetRakutenDataByCampaingType(dr["ID"].ToString(), "remove_item_6", shopid);
                                        if (dt != null)
                                            dsItem.Tables[6].Merge(dt);
                                        break;
                                    case "7":
                                        dt = GetRakutenDataByCampaingType(dr["ID"].ToString(), "remove_item_7", shopid);
                                        if (dt != null)
                                            dsItem.Tables[7].Merge(dt);
                                        break;
                                    case "8":
                                        dt = GetRakutenDataByCampaingType(dr["ID"].ToString(), "remove_item_8", shopid);
                                        if (dt != null)
                                            dsItem.Tables[8].Merge(dt);
                                        dtSelect.Merge(GetRakutenDataByCampaingType(dr["ID"].ToString(), "remove_itemselect_8", shopid));
                                        break;
                                }
                            }
                        }

                    }

                    string shop_id = drPromotion["Shop_ID"].ToString();
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string filename = shop_id + "_" + date;

                    for (int i = 0; i < dsItem.Tables.Count; i++)
                    {
                        if (dsItem.Tables[i].Rows.Count > 0)
                        {
                            Change_Templates changeTemplates = new Change_Templates();
                            using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}_R.csv", i.ToString()), false, Encoding.GetEncoding(932)))
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

                            if (i == 8)
                            {
                                using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item_select$" + filename + String.Format("_{0}_R.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                                {
                                    WriteDataTable(dtSelect, writer, true);
                                    dsItem.Tables[i].Rows.Clear();
                                }
                                Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                                itemExportQII.File_Name = "item_select$" + filename + String.Format("_{0}_R.csv", i.ToString());
                                itemExportQII.File_Type = 0;
                                itemExportQII.ShopID = int.Parse(shop_id);
                                itemExportQII.IsExport = 0;
                                itemExportQII.Created_Date = DateTime.Now;
                                itemExportQII.Export_Type = 2;
                                InsertItemExportQ(itemExportQII);
                            }
                        }
                    }///

                    shopCri += drPromotion["Shop_ID"].ToString() + ",";
                }
            }//if condition
        }

        private static void CreateRemoveCSVsForPonpareold(DataTable dtPromotionCri)
        {
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            dtPromotionCri.Columns.Add("Shop_ID", typeof(int));

            for (int i = 0; i < dtPromotionCri.Rows.Count; i++)
            {
                dtShopList = promotionShopBL.GetShopList(dtPromotionCri.Rows[i]["ID"].ToString());

                List<DataRow> rows_to_remove = new List<DataRow>();

                foreach (DataRow row in dtShopList.Rows)
                {
                    if (int.Parse(row["Mall_ID"].ToString()) != 3)
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
            for (int i = 0; i < 9; i++)
            {
                dsItem.Tables.Add("dtCampaingType" + i.ToString());
            }
            string shopCri = "";

            foreach (DataRow drPromotion in dtPromotionCri.Rows)
            {
                DataRow[] drPromotionWithSameShop = dtPromotionCri.Select("Shop_ID = " + drPromotion["Shop_ID"].ToString());
                DataTable dtSelect = new DataTable();
                if (!shopCri.Contains(drPromotion["Shop_ID"].ToString()))
                {
                    foreach (DataRow dr in drPromotionWithSameShop)
                    {
                        string[] campaingTypeList = dr["Campaign_TypeID"].ToString().Split(',');

                        foreach (string campaingType in campaingTypeList)
                        {
                            DataTable dt = new DataTable();
                            switch (campaingType)
                            {
                                case "0":
                                    dt = GetPonpareDataByCampaingType(dr["ID"].ToString(), "remove_item_0");
                                    if (dt != null)
                                        dsItem.Tables[0].Merge(dt);
                                    break;
                                case "1":
                                    dt = GetPonpareDataByCampaingType(dr["ID"].ToString(), "remove_item_1");
                                    if (dt != null)
                                        dsItem.Tables[1].Merge(dt);
                                    break;
                                case "2":
                                    dt = GetPonpareDataByCampaingType(dr["ID"].ToString(), "remove_item_2");
                                    if (dt != null)
                                        dsItem.Tables[2].Merge(dt);
                                    break;
                                case "3":
                                    dt = GetPonpareDataByCampaingType(dr["ID"].ToString(), "remove_item_3");
                                    if (dt != null)
                                        dsItem.Tables[3].Merge(dt);
                                    break;
                                case "4":
                                    dt = GetPonpareDataByCampaingType(dr["ID"].ToString(), "remove_item_4");
                                    if (dt != null)
                                        dsItem.Tables[4].Merge(dt);
                                    break;
                                case "5":
                                    dt = GetPonpareDataByCampaingType(dr["ID"].ToString(), "remove_item_5");
                                    if (dt != null)
                                        dsItem.Tables[5].Merge(dt);
                                    break;
                                case "6":
                                    dt = GetPonpareDataByCampaingType(dr["ID"].ToString(), "remove_item_6");
                                    if (dt != null)
                                        dsItem.Tables[6].Merge(dt);
                                    break;
                                case "7":
                                    dt = GetPonpareDataByCampaingType(dr["ID"].ToString(), "remove_item_7");
                                    if (dt != null)
                                        dsItem.Tables[7].Merge(dt);
                                    break;
                                case "8":
                                    dt = GetPonpareDataByCampaingType(dr["ID"].ToString(), "remove_item_8");
                                    if (dt != null)
                                        dsItem.Tables[8].Merge(dt);
                                    dtSelect.Merge(GetPonpareDataByCampaingType(dr["ID"].ToString(), "remove_itemselect_8"));
                                    break;
                            }
                        }
                    }
                }

                string shop_id = drPromotion["Shop_ID"].ToString();
                String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string filename = shop_id + "_" + date;

                for (int i = 0; i < dsItem.Tables.Count; i++)
                {
                    if (dsItem.Tables[i].Rows.Count > 0)
                    {
                        Change_Templates changeTemplates = new Change_Templates();
                        using (StreamWriter writer = new StreamWriter(ExportCSVPath + "item$" + filename + String.Format("_{0}_R.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                        {
                            DataTable dt = changeTemplates.ModifyTable(dsItem.Tables[i], int.Parse(drPromotion["Shop_ID"].ToString()));
                            WriteDataTable(dt, writer, true);
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

                        if (i == 8)
                        {
                            using (StreamWriter writer = new StreamWriter(ExportCSVPath + "option$" + filename + String.Format("_{0}_R.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                            {
                                WriteDataTable(dtSelect, writer, true);
                                dsItem.Tables[i].Rows.Clear();
                            }
                            Item_ExportQ_Entity itemExportQII = new Item_ExportQ_Entity();
                            itemExportQII.File_Name = "option$" + filename + String.Format("_{0}.csv", i.ToString());
                            itemExportQII.File_Type = 0;
                            itemExportQII.ShopID = int.Parse(shop_id);
                            itemExportQII.IsExport = 0;
                            itemExportQII.Created_Date = DateTime.Now;
                            itemExportQII.Export_Type = 2;
                            InsertItemExportQ(itemExportQII);
                        }
                    }
                }/////



                shopCri += drPromotion["Shop_ID"].ToString() + ",";
            }
        }

        private static DataTable GetRakutenDataByCampaingType(string promotionID, string option, int shop_id)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtItem = promotionBL.GetPromotionForRakuten(promotionID, option, shop_id);

            return dtItem;
        }

        private static DataTable GetPonpareDataByCampaingType(string promotionID, string option)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtItem = promotionBL.GetPromotionForPonpare(promotionID, option);

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

        private static void ChangeExportStatusFlag(int status, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string query = "UPDATE Promotion SET "
                + "Export_Status = " + status
                + "WHERE ID = " + dr["ID"].ToString();
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
        private static void ChangePromotionStatusFlag(int IsProstatus, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string query = "UPDATE Promotion SET "
                + "IsPromotionClose=" + IsProstatus
                + "WHERE ID = " + dr["ID"].ToString();
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
        private static void InsertItemExportQ(Item_ExportQ_Entity itemExportQ)
        {
            Item_ExportQ_BL itemExportQBL = new Item_ExportQ_BL();
            itemExportQBL.Save(itemExportQ);
        }

        public static void Move1(string file)
        {
            string path = rootFolderPath + file;
            string[] filename = Directory.GetFiles(rootFolderPath);
            if (filename != null)
            {
                for (int a = 0; a < filename.Count(); a++)
                {
                    if (File.Exists(filename[a]))
                    {
                        if (filename[a].Contains(file))
                        {
                            File.Copy(filename[a], Path.Combine(ExportCSVPath, Path.GetFileName(filename[a])), true);
                            //File.Delete(file);
                        }
                    }
                }
            }//
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
        public static int Exhibition_List_Insert(string item_ID, string Itemcode, int csv, int pid, int sid)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_Exhibition_Promotion_CampaingInsert", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", item_ID);
                cmd.Parameters.AddWithValue("@Item_Code", Itemcode);
                cmd.Parameters.AddWithValue("@csvtype", csv);
                cmd.Parameters.AddWithValue("@pid", pid);
                cmd.Parameters.AddWithValue("@shopid", sid);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int eid = Convert.ToInt32(cmd.Parameters["@result"].Value);
                return eid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static string GetItem_Code(int opt, int id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(ConnectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Select_CampaingPromotionItemCode", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Connection.Open();
                sda.SelectCommand.Parameters.AddWithValue("@option", opt);
                sda.SelectCommand.Parameters.AddWithValue("@ids", id);
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt.Rows[0]["Item_Code"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        static string GetItem_ID(int opt, int ids)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(ConnectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Select_CampaingPromotionItemCode", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Connection.Open();
                sda.SelectCommand.Parameters.AddWithValue("@option", opt);
                sda.SelectCommand.Parameters.AddWithValue("@ids", ids);
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt.Rows[0]["Item_ID"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
