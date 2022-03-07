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
    public class Promotion_Point_BL
    {
        Promotion_Point_DL pdl;

        public Promotion_Point_BL() 
        {
            pdl = new Promotion_Point_DL();
        }


        public DataTable SelectallData(Promotion_Point_Entity ppime, int opt, int pgindex, int psize) 
        {
            return pdl.SelectAll(ppime, opt, pgindex, psize);
        }

        public DataTable SelectallDataEqual(Promotion_Point_Entity ppime, int opt,int pgindex,int psize)
        {
            return pdl.SelectAllEqual(ppime, opt,pgindex,psize);
        }
        public int Save(DataTable dt) 
        {
            return pdl.SaveUpdate(dt);
        }
        public void Insert(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                pdl.DeleteByPromotionID((int)dt.Rows[0]["PromotionID"]);
               // pdl.Insert(dt);
            }
        }
        public DataTable SelectallDataConfirm(string list)
        {
            return pdl.SelectbyID(list);
        }

        public DataTable  SelectPopupData(Promotion_Point_Entity ppime, int opt)
        {
            return pdl.PopupSelectall(ppime, opt);
        }

        //updated date 13/07/2015
        public DataTable SelectallDataCheckbox(Promotion_Point_Entity ppime, int opt)
        {
            return pdl.SelectAllCheckdata(ppime, opt);
        }

        ////for exhibition
        public DataTable SelectAll(string list, string shop, string mall, string code, int ctrl, string shopid, string API,
             string error, string exhibitor, string bcheck, string proname, string catInfo, string brname, string comname, string copname,
             string claname, string year, string season, string remark, DateTime? d1, DateTime? d2)
        {


            return pdl.SelectAll_Item(list, shop, mall, code, ctrl, shopid, API, error, exhibitor, bcheck, proname, catInfo, brname, comname, copname, claname, year, season, remark, d1, d2);



        }
        public DataTable SelectMall(int MallID)
        {
            return pdl.SelectMall(MallID);
        }
        public DataTable SelectAllpaging(string list, string shop, string mall, string code, int ctrl, string shopid, string API,
         string error, string exhibitor, string bcheck, string proname, string catInfo, string brname, string comname, string copname,
         string claname, string year, string season, string remark, DateTime? d1, DateTime? d2,int pindex,int psize)
        {


            return pdl.SelectAll_Itempaging(list, shop, mall, code, ctrl, shopid, API, error, exhibitor, bcheck, proname, catInfo, brname, comname, copname, claname, year, season, remark, d1, d2,pindex,psize);



        }
    }
}
