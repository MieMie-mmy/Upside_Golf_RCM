using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using ORS_RCM_Common;
using System.Data;

namespace ORS_RCM_BL
{
    public class Promotion_BL
    {
        Promotion_DL promotionDL;

        public Promotion_BL()
        {
            promotionDL = new Promotion_DL();
        }

        public int SaveUpdate(Campaign_Entity ce, string option)
        {
            return promotionDL.SaveUpdate(ce, option);
        }

        public DataTable PromotionSearch(Campaign_Entity ce, int pgindex, String psize, int option)
        {
            return promotionDL.PromotionSearch(ce, pgindex, psize, option);
        }

        public Boolean Record_CampaginIDexisted(string campaignID)
        {
            return promotionDL.Check_CampaignIDexisted(campaignID);
        }


        public DataTable Duplicate_CampaignID(string Campaign_ID, int? ID)
        {
            return promotionDL.CampaignID_alreadyUpdate(Campaign_ID, ID);

        }
        public DataTable SelectAll()
        {
            return promotionDL.SelectAll();
        }

        public DataTable SelectForAddCSV(int opt)
        {
            return promotionDL.SelectForAddCSV(opt);
        }

        public DataTable SelectForRemoveCSV()
        {
            return promotionDL.SelectForRemoveCSV();
        }

        public DataTable GetShopNamesByID(int ID)
        {
            promotionDL = new Promotion_DL();
            return promotionDL.GetShopNamesByID(ID);
        }

        public Campaign_Entity SelectByID(int pid)
        {
            return promotionDL.SelectByID(pid);
        }



        //Item code from Exhibition-list to Campaign Promotion form 
        public int SelectPitemID(string Item_Code, string Shop_ID)
        {
            return promotionDL.SelectPitemID(Item_Code, Shop_ID);
        }




        public DataTable Search_CampaignPromotion(string CampaignID, string promotionName, string shopName, string Target_Brand, DateTime? repeatFrom, DateTime? repeatTo, string instructionNo, string campaignTypeID, string priority, string remark, string subject, string item_code, string option1, string option2)
        {

            return promotionDL.Search(CampaignID, promotionName, shopName, Target_Brand, repeatFrom, repeatTo, instructionNo, campaignTypeID, priority, remark, subject, item_code, option1, option2);
        }

        public DataTable SearchChkPresents(string CampaignID, string promotionName, string shopName, string Target_Brand, DateTime? repeatFrom, DateTime? repeatTo, string instructionNo, string campaignTypeID, string priority, string remark, string subject, int option)
        {

            return promotionDL.SearchbyGifts(CampaignID, promotionName, shopName, Target_Brand, repeatFrom, repeatTo, instructionNo, campaignTypeID, priority, remark, subject, option);
        }




        public DataTable GetPromotionForRakuten(string str, string option, int shopid)
        {
            promotionDL = new Promotion_DL();
            return promotionDL.GetPromotionForRakuten(str, option, shopid);
        }
        public DataTable GetPromotionForJisha(string str, string option, int shopid)
        {
            promotionDL = new Promotion_DL();
            return promotionDL.GetPromotionForJisha(str, option, shopid);
        }

        //public DataTable  CampaignID_is(string ID)
        //{
        //    return promotionDL.Check_CampaignID(ID);
        //}


        public DataTable GetPromotionForPonpare(string str, string option)
        {
            promotionDL = new Promotion_DL();
            return promotionDL.GetPromotionForPonpare(str, option);
        }

        public DataTable GetPromotionForYahoo(string str, string option, string shopid)
        {
            promotionDL = new Promotion_DL();
            return promotionDL.GetPromotionForYahoo(str, option, shopid);
        }

        public DataTable GetRakutenPromotionByCampaingTypeID(string str, string option)
        {
            promotionDL = new Promotion_DL();
            return promotionDL.GetRakutenPromotionByCampaingTypeID(str, option);
        }

        public DataTable SelectAttachmentProCSV(int pID, int opt)
        {
            return promotionDL.SelectAllProAttachment(pID, opt);
        }


        public DataTable Campaign_PromotionAll()
        {
            return promotionDL.Select_CampaignAll();
        }



        public DataTable SelectCampaignbyItem(string CampaignID, string promotionName, string shopName, string Target_Brand, DateTime? repeatFrom, DateTime? repeatTo, string instructionNo, string campaignTypeID, string priority, string remark, string subject, string item_code, string option1, string option2)
        {
            return promotionDL.Searchby_ValidItem(CampaignID, promotionName, shopName, Target_Brand, repeatFrom, repeatTo, instructionNo, campaignTypeID, priority, remark, subject, item_code, option1, option2);
        }

        public void Insert_ItemOption_For_New(DataTable dtSelect,int pid,int shopid)
        {
             promotionDL.Insert_ItemOption_For_New(dtSelect,pid,shopid);
        }

        public void Delete_ItemOption_For_New(int pid,int shopid)
        {
            promotionDL.Delete_ItemOption_For_New(pid,shopid);
        }

        public DataTable SelectOptionValue(int pid)
        {
            return promotionDL.SelectOptionValue(pid);
        }
        
        #region For Consoles
        public DataTable Adddatas(int option)
        {
            return promotionDL.Adddata(option);
        }
        public DataTable GetMallIDs(int shopid)
        {
            return promotionDL.GetMallID(shopid);
        }

        public int Exhibition_List_Inserts(int item_ID, string Itemcode, int shopid, string itemname, int csvtype, int option)
        {
            return promotionDL.Exhibition_List_Insert(item_ID, Itemcode, shopid, itemname, csvtype, option);
        }
        public int Exhibition_Promotion_Inserts(int item_ID, string Itemcode, int shopid, string itemname, int csvtype, int option, string ctrl, string purl, int pmag, string pperiod, string pccatch, string mcatch, string pronparecatch)
        {
            return promotionDL.Exhibition_Prolog_Insert(item_ID, Itemcode, shopid, itemname, csvtype, option, ctrl, purl, pmag, pperiod, pccatch, mcatch, pronparecatch);
        }
        public void Exhibition_List_Update(int item_ID, string Itemcode, int shopid, string itemname, string Pc, string mobile)
        {
            promotionDL.Exhibition_Log_Update(item_ID, Itemcode, shopid, itemname, Pc, mobile);
        }
        public int YahooPoint_ExhibitionList_Inserts(int item_ID, string Itemcode, int shopid, string itemname, int csvtype, DataTable dtdata, int promotiontype)
        {
            return promotionDL.PointYahoolog_Insert(item_ID, Itemcode, shopid, itemname, csvtype, dtdata, promotiontype);
        }
        public int Yahoo_Campaignlog_Insert(int csvtype, DataTable dtdata, int promotiontype, int opt)
        {
            return promotionDL.Yahoo_Campaignlog_Insert(csvtype, dtdata, promotiontype, opt);
        }
        public DataTable GetdatafromMaster(string itemcode)
        {
            return promotionDL.GetdatafromMaster(itemcode);
        }
        public DataTable GetdataforRakuten(string list, int sid, int option)
        {
            return promotionDL.GetdataforRakuten(list, sid, option);
        }
        public DataTable Removedata(int option)
        {
            return promotionDL.Removedata(option);
        }
        public DataTable SelectSecondCSVdata(int option)
        {
            return promotionDL.SelectSecondCSVdata(option);
        }
        public void ChangeExportStatusFlag(int status, DataTable dt)
        {
            promotionDL.ChangeExportStatusFlag(status, dt);
        }
        public bool InsertItemExportQ(string filename, int ftype, int sid, int isexp, int exptype)
        {
            return promotionDL.InsertItemExportQ(filename, ftype, sid, isexp, exptype);
        }
        public DataTable GetdataforYahoo(string list, int sid, int option)
        {
            return promotionDL.GetdataforYahoo(list, sid, option);
        }
        public DataTable GetdataforPonpare(string list, int sid, int option)
        {
            return promotionDL.GetdataforPonpare(list, sid, option);
        }

        public void RakutenCampaignExbInsert(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            promotionDL.CamRExbImportXmlInsert(result);
        }
        //for delivery
        public DataTable GetdataforDeliveryRakuten(string list, int sid, int option)
        {
            return promotionDL.GetdataforDeliveryRakuten(list, sid, option);
        }
        public DataTable RemoveCSVdataforDeliveryRakuten(string list, int sid, int option)
        {
            return promotionDL.RemoveCSVdataforDeliveryRakuten(list, sid, option);
        }
        public DataTable Selectdeliverydata(int option)
        {
            return promotionDL.Selectdeliverydata(option);
        }
        public DataTable GetdataforDeliveryYahoo(string list, int sid, int option)
        {
            return promotionDL.GetdataforDeliveryYahoo(list, sid, option);
        }
        public void ChangeDeliveryExportStatusFlag(int status, DataTable dt)
        {
            promotionDL.ChangeDeliveryExportStatusFlag(status, dt);
        }
        public DataTable GetdataforDelveryPonpare(string list, int sid, int option)
        {
            return promotionDL.GetdataforDelveryPonpare(list, sid, option);
        }
        public DataTable GetdataforDeliveryJisha(string list, int sid, int option)
        {
            return promotionDL.GetdataforDeliveryJisha(list, sid, option);
        }
        public void DeliveryExportStatusFlag(int status, string did, int option)
        {
            promotionDL.ChangeDeliveryExportFlag(status, did, option);
        }
        //updated date 18/06/2015
        public int RakutenDeliverylog_Insert(int csvtype, string ictrlid, string demagno, string sctrlId, string stype, string svalue, string sname, DataTable dt)
        {
            return promotionDL.Rakuten_Deliverylog_Insert(csvtype, ictrlid, demagno, sctrlId, stype, svalue, sname, dt);
        }
        public int PonpareDeliverylog_Insert(int csvtype, DataTable dt, string choicetype, string shopcategory, string catctrl)
        {
            return promotionDL.Ponpare_Deliverylog_Insert(csvtype, dt, choicetype, shopcategory, catctrl);
        }
        //for campaign
        public string GetItem_Code(int opt, string id)
        {
            return promotionDL.GetItem_Code(opt, id);
        }
        public string GetItem_ID(int opt, string ids)
        {
            return promotionDL.GetItem_ID(opt, ids);
        }
        public int CExhibition_List_Insert(string item_ID, string Itemcode, int csv, string pid, string sid)
        {
            return promotionDL.Exhibition_List_Insert(item_ID, Itemcode, csv, pid, sid);
        }
        public void ChangeCExportStatusFlag(int status, DataTable dt)
        {
            promotionDL.ChangeExportStatusFlags(status, dt);
        }
        public void ChangeCampaignFlag(int status, string pid, int option)
        {
            promotionDL.CampaignExportStatusFlags(status, pid, option);
        }
        public void ChangeCPromotionStatusFlag(int IsProstatus, DataTable dt)
        {
            promotionDL.ChangePromotionStatusFlags(IsProstatus, dt);
        }
        //updated date 18/06/2015
        public int Rakuten_Campaignlog_Insert(string item_ID, string Itemcode, int csv, string pid, string sid, DataTable dtdata, string sctrl, string ctype)
        {
            return promotionDL.Rakuten_Campaignlog_Insert(item_ID, Itemcode, csv, pid, sid, dtdata, sctrl, ctype);
        }
        public int Ponpare_Campaignlog_Insert(int csv, DataTable dtdata, string sctrl, string ctype)
        {
            return promotionDL.Ponpare_Campaignlog_Insert(csv, dtdata, sctrl, ctype);
        }
        //updated date 16/06/2015
        public void CampaignExportStatusFlag(int status, string pid, int option)
        {

            promotionDL.ChangestatusFlag(status, pid, option);
        }
        #endregion
    }
}

