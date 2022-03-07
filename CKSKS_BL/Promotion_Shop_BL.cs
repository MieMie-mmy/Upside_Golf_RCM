using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
    public class Promotion_Shop_BL
    {
        Promotion_Shop_DL promotionShopDL;
        public Promotion_Shop_BL()
        {
            promotionShopDL = new Promotion_Shop_DL();
        }

        public void Insert(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                promotionShopDL.DeleteByPromotionID((int)dt.Rows[0]["PromotionID"]);
                promotionShopDL.Insert(dt);
            }
        }

        public DataTable SelectByPromotionID(int promotionID)
        {
            return promotionShopDL.SelectByPromotionID(promotionID);
        }


        public DataTable Search_TargetShop()
        {
            return promotionShopDL.Select_TargetShop();

        }


        public DataTable GetShopList(string promotionID)
        {
            promotionShopDL = new Promotion_Shop_DL();
            return promotionShopDL.GetShopList(promotionID);
        }
    }
}
