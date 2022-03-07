using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using ORS_RCM_Common;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Promotion
{
    public class Export_Promotion_CSV
    {
        string ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();

        public Export_Promotion_CSV()
        {

        }

        public Export_Promotion_CSV(DataTable dtPromotionCri)
        {
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            string promotionList = "";
            if (dtPromotionCri != null && dtPromotionCri.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPromotionCri.Rows)
	            {
		            promotionList += (int)dr["ID"] + ",";
	            }
            }

            dtShopList = promotionShopBL.GetShopList(promotionList);

            if (dtShopList != null && dtShopList.Rows.Count > 0)
            {
                foreach (DataRow dr in dtShopList.Rows)
                {
                    string shop_id = dtShopList.Rows[0]["ID"].ToString();
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    String filename = shop_id + "_" + date;
                    switch (dr["Mall_ID"].ToString())
                    {
                        case "1":
                            CreateCSVsForRakuten(dtPromotionCri);
                            break;
                        case "2":
                            CreateCSVsForYahoo(promotionList, filename );
                            break;
                        case "3":
                            break;
                        case "4":
                            break;
                    }
                }
            }
        }

        public void CreateRemoveCSV(DataTable dtPromotionCri)
        {
            string promotionList = "";
            if (dtPromotionCri != null && dtPromotionCri.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPromotionCri.Rows)
                {
                    promotionList += (int)dr["ID"] + ",";
                }
            }
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            dtShopList = promotionShopBL.GetShopList(promotionList);
            string shop_id = dtShopList.Rows[0]["ID"].ToString();
            String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            String filename = shop_id + "_" + date + ".csv";
            if (dtShopList.Rows.Count > 0)
            {
                foreach (DataRow dr in dtShopList.Rows)
                {
                    switch (dr["Mall_ID"].ToString())
                    {
                        case "1":
                            CreateRemoveCSVsForRakuten(dtPromotionCri);
                            break;
                        case "2":
                            break;
                        case "3":
                            break;
                        case "4":
                            break;
                    }
                }
            }
        }

        public void CreateCSVsForRakuten(DataTable dtPromotionCri)
        {
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            int shopid=0;
            DataSet dsItem = new DataSet();
            for (int i = 0; i < 9; i++)
            {
                dsItem.Tables.Add("dtCampaingType" + i.ToString());
            }
            
            string filename = "";
            if (dtPromotionCri != null && dtPromotionCri.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPromotionCri.Rows)
                {
                
                    string[] campaingTypeList = dr["Campaign_TypeID"].ToString().Split(',');
                    dtShopList = promotionShopBL.GetShopList(dr["ID"].ToString());

                    if (dtShopList != null && dtShopList.Rows.Count > 0)
                    {
                        string shop_id = dtShopList.Rows[0]["ID"].ToString();
                        String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                        filename = shop_id + "_" + date;
                        shopid = Convert.ToInt32(dtShopList.Rows[0]["ID"].ToString());
                    }
                    foreach (string campaingType in campaingTypeList)
                    {
                        switch (campaingType)
                        {
                            case "0":
                                dsItem.Tables[0].Merge(GetDataByCampaingType(dr["ID"].ToString(), "item_0",shopid));
                                break;
                            case "1":
                                dsItem.Tables[1].Merge(GetDataByCampaingType(dr["ID"].ToString(), "item_1", shopid));
                                break;
                            case "2":
                                dsItem.Tables[2].Merge(GetDataByCampaingType(dr["ID"].ToString(), "item_2", shopid));
                                break;
                            case "3":
                                dsItem.Tables[3].Merge(GetDataByCampaingType(dr["ID"].ToString(), "item_3", shopid));
                                break;
                            case "4":
                                dsItem.Tables[4].Merge(GetDataByCampaingType(dr["ID"].ToString(), "item_4", shopid));
                                break;
                            case "5":
                                dsItem.Tables[5].Merge(GetDataByCampaingType(dr["ID"].ToString(), "item_5", shopid));
                                break;
                            case "6":
                                dsItem.Tables[6].Merge(GetDataByCampaingType(dr["ID"].ToString(), "item_6", shopid));
                                break;
                            case "7":
                                dsItem.Tables[7].Merge(GetDataByCampaingType(dr["ID"].ToString(), "item_7", shopid));
                                break;
                            case "8":
                                dsItem.Tables[8].Merge(GetDataByCampaingType(dr["ID"].ToString(), "item_8", shopid));
                                break;
                        }
                    }
                }
                for (int i = 0; i < dsItem.Tables.Count; i++)
                {
                    if (dsItem.Tables[i].Rows.Count > 0)
                    {
                        using (StreamWriter writer = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath(ExportCSVPath) + "item" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dsItem.Tables[i], writer, true);
                        }
                    }
                }
            }
        }

        public void CreateCSVsForYahoo(string promotionList, string fileName)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtData = promotionBL.GetPromotionForYahoo(promotionList, "item","2");

            using (StreamWriter writer = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath(ExportCSVPath) + "data-" + fileName + ".csv", false, Encoding.GetEncoding(932)))
            {
                WriteDataTable(dtData, writer, true);
            }
        }

        private void CreateRemoveCSVsForRakuten(DataTable dtPromotionCri)
        {
            Promotion_Shop_BL promotionShopBL = new Promotion_Shop_BL();
            DataTable dtShopList = new DataTable();
            int Shop_ID=0;
            DataSet dsItem = new DataSet();
            for (int i = 0; i < 9; i++)
            {
                dsItem.Tables.Add("dtCampaingType" + i.ToString());
            }

            string filename = "";
            if (dtPromotionCri != null && dtPromotionCri.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPromotionCri.Rows)
                {
                    string[] campaingTypeList = dr["Campaign_TypeID"].ToString().Split(',');
                    dtShopList = promotionShopBL.GetShopList(dr["ID"].ToString());
                    
                    if (dtShopList != null && dtShopList.Rows.Count > 0)
                    {
                        string shop_id = dtShopList.Rows[0]["ID"].ToString();
                        String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                        filename = shop_id + "_" + date;
                        Shop_ID = Convert.ToInt32(dtShopList.Rows[0]["ID"].ToString());
                    }
                    foreach (string campaingType in campaingTypeList)
                    {
                        switch (campaingType)
                        {
                            case "0":
                                dsItem.Tables[0].Merge(GetDataByCampaingType(dr["ID"].ToString(), "remove_item_0", Shop_ID));
                                break;
                            case "1":
                                dsItem.Tables[1].Merge(GetDataByCampaingType(dr["ID"].ToString(), "remove_item_1", Shop_ID));
                                break;
                            case "2":
                                dsItem.Tables[2].Merge(GetDataByCampaingType(dr["ID"].ToString(), "remove_item_2", Shop_ID));
                                break;
                            case "3":
                                dsItem.Tables[3].Merge(GetDataByCampaingType(dr["ID"].ToString(), "remove_item_3", Shop_ID));
                                break;
                            case "4":
                                dsItem.Tables[4].Merge(GetDataByCampaingType(dr["ID"].ToString(), "remove_item_4", Shop_ID));
                                break;
                            case "5":
                                dsItem.Tables[5].Merge(GetDataByCampaingType(dr["ID"].ToString(), "remove_item_5", Shop_ID));
                                break;
                            case "6":
                                dsItem.Tables[6].Merge(GetDataByCampaingType(dr["ID"].ToString(), "remove_item_6", Shop_ID));
                                break;
                            case "7":
                                dsItem.Tables[7].Merge(GetDataByCampaingType(dr["ID"].ToString(), "remove_item_7", Shop_ID));
                                break;
                            case "8":
                                dsItem.Tables[8].Merge(GetDataByCampaingType(dr["ID"].ToString(), "remove_item_8", Shop_ID));
                                break;
                        }
                    }
                }
                for (int i = 0; i < dsItem.Tables.Count; i++)
                {
                    if (dsItem.Tables[i].Rows.Count > 0)
                    {
                        using (StreamWriter writer = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath(ExportCSVPath) + "item" + filename + String.Format("_{0}.csv", i.ToString()), false, Encoding.GetEncoding(932)))
                        {
                            WriteDataTable(dsItem.Tables[i], writer, true);
                        }
                    }
                }
            }
        }

        public DataTable GetDataByCampaingType(string promotionID, string option,int shopID)
        {
            Promotion_BL promotionBL = new Promotion_BL();
            DataTable dtItem = promotionBL.GetPromotionForRakuten(promotionID, option,shopID);

            return dtItem;
        }

        public void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                List<string> headerValues = new List<string>();
                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerValues.Add(column.ColumnName);
                }

                writer.WriteLine(String.Join(",", headerValues.ToArray()));
            }

            string[] items = null;
            foreach (DataRow row in sourceTable.Rows)
            {
                items = row.ItemArray.Select(o => o.ToString()).ToArray();
                writer.WriteLine(String.Join(",", items));
            }

            writer.Flush();
        }


    }
}