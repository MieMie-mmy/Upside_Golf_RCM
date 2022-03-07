using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using System.Data;

namespace ORS_RCM_BL
{
    public class Details_of_Promotion_Yahoo_Exhibition_BL
    {
        Details_of_Promotion_Yahoo_Exhibition_DL detailsPyedl;

        public Details_of_Promotion_Yahoo_Exhibition_BL()
        {
            detailsPyedl = new Details_of_Promotion_Yahoo_Exhibition_DL();
        }

        public DataTable SelectByDetailGetDataPromotionPoint(string strString, int shopId, int option)
        {
            return detailsPyedl.DetailGetDataPromotionPoint(strString, shopId, option);
        }

        public DataTable SelectByDetailGetDataPromotionDelivery(string strString, int shopId, int option)
        {
            return detailsPyedl.DetailGetDataPromotionDelivery(strString, shopId, option);
        }

        public DataTable SelectByDetailGetDataPromotionCamapign(string strString, int shopId, string option)
        {
            return detailsPyedl.DetailGetDataPromotionCamapign(strString, shopId, option);
        }

    }
}
