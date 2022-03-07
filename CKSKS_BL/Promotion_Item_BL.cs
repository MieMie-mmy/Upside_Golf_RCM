using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using ORS_RCM_Common;
using System.Data;


namespace ORS_RCM_BL
{
    public class Promotion_Item_BL
    {
        Promotion_Item_DL promotionItemDL;

        public Promotion_Item_BL()
        {
            promotionItemDL = new Promotion_Item_DL();
        }

        public void Insert_Promotion_Item(int promotionID, string[] arr)
        {
            promotionItemDL.DeleteByPromotionID(promotionID);
            promotionItemDL.Insert_Promotion_Item(promotionID, arr);
        }

        public void Insert_CampaignPromotion_Item(int promotionID,DataTable dt)
        {
            promotionItemDL.DeleteByPromotionID(promotionID);
            promotionItemDL.Insert_CAMPromotion_Item(promotionID, dt);
        }
        public void Update_Promotion_Item(int promotionID, string[] arr)
        {
            promotionItemDL.Update_Promotion_Item(promotionID, arr);
        }

        public DataTable SelectByPromotionID(int promotionID)
        {
            return promotionItemDL.SelectByPromotionID(promotionID);
        }
    }
}
