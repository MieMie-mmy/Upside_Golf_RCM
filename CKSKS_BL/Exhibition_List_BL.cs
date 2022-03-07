using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;
using ORS_RCM_Common;

namespace ORS_RCM_BL
{
    public class Exhibition_List_BL
    {
        Exhibition_List_DL ehbd;
        public Exhibition_List_BL()
        {
            ehbd = new Exhibition_List_DL();
        }

        public DataTable SelectAll(string list, string shop, string mall, string code, int ctrl, string shopid, string API,
            string error, string exhibitor, string bcheck, string proname, string catInfo, string brname, string comname, string copname,
            string claname, string year, string season, string remark, DateTime? d1, DateTime? d2)
        {


            return ehbd.SelectAll_Item(list, shop, mall, code, ctrl, shopid, API, error, exhibitor, bcheck, proname, catInfo, brname, comname, copname, claname, year, season, remark, d1, d2);



        }

        public void DeleteUpdateOrder(string list)
        {
            ehbd.DeleteUpdateOrder(list);
        }

        public DataTable SelectMall(int MallID)
        {
            return ehbd.SelectMall(MallID);
        }

        public DataTable SelectShop()
        {
            return ehbd.SelectShop();
        }

        public DataTable SelectbyID()
        {
            return ehbd.SelectbyID();
        }

        public DataTable Exhibitor()
        {
            return ehbd.Selectexhibitor();
        }

        public void ChangeIsGeneratedCSVFlag(int exhibitid,int itemid,int shopid)
        {
            ehbd.ChangeIsGeneratedCSVFlag(exhibitid, itemid, shopid);
        }

        public DataTable Selectjishadata(int shopid, string str, string option)
        {
            return ehbd.SelectJishaData(shopid, str, option);
        }

        public DataTable Selectexerror(string itcode, int sid, int cktype, int exhibition_id)
        {
            return ehbd.Selecterror(itcode, sid, cktype, exhibition_id);
        }

        public DataTable Selectexerror(string itcode, int sid, int cktype)
        {
            return ehbd.Selecterror(itcode, sid, cktype);
        }

        public DataTable Exhibition_Search(Exhibition_Entity ee, string list, int option, int pindex, int psize)
        {
            //return ehbd.Exhibition_Search(ee, list, option, pindex, chkerror, chknotcheck, chkrecovery, psize);
            return ehbd.Exhibition_Search(ee, list, option, pindex, psize);
        }
        public DataTable Exhibitiondownload_Search(Exhibition_Entity ee, string list, int option, int pindex, int psize)
        {
            return ehbd.DownloadExhibition_Search(ee, list, option, pindex, psize);
        }
        public DataTable SelectAllpaging(string list, string shop, string mall, string code, int ctrl, string shopid, string API,
         string error, string exhibitor, string bcheck, string proname, string catInfo, string brname, string comname, string copname,
         string claname, string year, string season, string remark, DateTime? d1, DateTime? d2, int pind, int psize)
        {


            return ehbd.SelectAll_Itempaging(list, shop, mall, code, ctrl, shopid, API, error, exhibitor, bcheck, proname, catInfo, brname, comname, copname, claname, year, season, remark, d1, d2, pind, psize);



        }

        //25-09-2017 Warehouse

        public void ChangeFlagForSoko(int item_ID, int flag)
        {
            try
            {
                ehbd.ChangeFlagForSoko(item_ID, flag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean CheckSKSStatus(string list)
        {
            return ehbd.CheckSKSStatus(list);
        }


        ///////24/12/2014 AAM/////////
        public int Exhibition_List_Insert(int item_ID, string flag, int user_id)
        {
            try
            {
                if (flag == "d")
                {
                    return ehbd.Exhibition_List_DeleteInsert(item_ID, user_id);
                }
                else if (flag == "m")
                {
                    return ehbd.Exhibition_List_Insert(item_ID, user_id,"monotaro");
                }
                else
                {
                    return ehbd.Exhibition_List_Insert(item_ID, user_id,"exhibition");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetItemData(string list,int shopid)
        {
            return ehbd.GetItemData(list,shopid);
        }

        public void ChangeFlag(string list, int shop_ID)
        {
            ehbd.ChangeFlag(list,shop_ID);
        }

        public void SaveLogExhibition(DataTable dt, string list, int shop_id)
        {
            ehbd.SaveLogExhibition(dt, list, shop_id);
        }

        public DataTable SelectLogExhibitionItem(int shop_id, int mall_id)
        {
            return ehbd.SelectLogExhibitionItem(shop_id,mall_id);
        }

        public void InsertMonotaroShop(int item_ID, string flag)
        { 
            ehbd.InsertMonotaroShop(item_ID, flag);
        }

        ///////29/07/2015 AAM/////////
        public int Quick_Exhibition_List_Insert(string item_code, int user_id)
        {
            try
            {
                return ehbd.Quick_Exhibition_List_Insert(item_code, user_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll_Not_Quick_Exhibition()
        {
            return ehbd.SelectAll_Not_Quick_Exhibition();
        }

        public bool Exhibition_Item_Shop_Insert(int EitemID, int itemID)
        {
            try
            {
                return ehbd.Exhibition_Item_Shop_Insert(EitemID, itemID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeStatus(string Item_Code, string Ctrl_ID, int Shop_ID)
        {
            ehbd.ChangeStatus(Item_Code, Ctrl_ID, Shop_ID);
        }

        public DataTable CollectItem(DataTable dtCopy, int filetype)
        {
            try
            {
                dtCopy.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dtCopy.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();
                DataTable dt = ehbd.CollectItem(result, filetype);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable CollectItem_Jisha(DataTable dtCopy, int filetype)
        {
            try
            {
                dtCopy.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dtCopy.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();
                DataTable dt = ehbd.CollectItem_Jisha(result, filetype);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable CollectItemCode(DataTable dtItemSelect, int filetype)
        {
            try
            {
                dtItemSelect.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dtItemSelect.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();
                DataTable dt = ehbd.CollectItemCode(result, filetype);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CollectItemCode_Yahoo(DataTable dtItemSelect, int filetype)
        {
            try
            {
                dtItemSelect.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dtItemSelect.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();
                DataTable dt = ehbd.CollectItemCode_Yahoo(result, filetype);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void SaveExhibitionItemShopExportedCSVInfo(int itemID, int shopID, string csvName)
        //{
        //    ehbd.SaveExhibitionItemShopExportedCSVInfo(itemID, shopID, csvName);
        //}

        public void SaveExhibitionItemShopExportedCSVInfo(int itemID, int shopID, string csvName, string ctrl_id)
        {
            ehbd.SaveExhibitionItemShopExportedCSVInfo(itemID, shopID, csvName, ctrl_id);
        }

        public DataTable SelectByExhibitionData(int shop_ID, string itemIDList, string option)
        {
            return ehbd.SelectByExhibitionData(shop_ID, itemIDList, option);
        }
        public DataTable SelectByItemDataForYahoo(string itemIDList, string option)
        {
            return ehbd.SelectByItemDataForYahoo(itemIDList, option);
        }
        public DataTable SelectByItemDataForYahoo(string itemIDList, string option, int Shop_ID)
        {
            return ehbd.SelectByItemDataForYahoo(itemIDList, option, Shop_ID);
        }

        ///for export 
        public DataTable SelectData(string lists)
        {
            return ehbd.SelectExportdata(lists);
        }
    }
}
