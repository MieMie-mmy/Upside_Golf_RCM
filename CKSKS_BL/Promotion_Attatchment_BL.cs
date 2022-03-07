using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
    public class Promotion_Attatchment_BL
    {
        Promotion_Attatchment_DL promotionAttatchmentDL;

        public Promotion_Attatchment_BL()
        {
            promotionAttatchmentDL = new Promotion_Attatchment_DL();
        }

        public void InsertUpdate(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                promotionAttatchmentDL.DeleteByPromotionID((int)dt.Rows[0]["Promotion_ID"]);
                promotionAttatchmentDL.InsertUpdate(dt);
            }
        }

        public DataTable SelectByPromotionID(int promotionID)
        {
            return promotionAttatchmentDL.SelectByPromotionID(promotionID);
        }
    }
}
