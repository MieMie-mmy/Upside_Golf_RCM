using ORS_RCM_DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ORS_RCM_BL
{
    public class Details_Wowma_Exhibition_BL
    {
        Details_Wowma_Exhibition_DL detailW_dl;
        public Details_Wowma_Exhibition_BL()
        {
            detailW_dl = new Details_Wowma_Exhibition_DL();
        }

        public DataTable SelectByExhibitionData(int shop_ID, string itemIDList, string option)
        {

            return detailW_dl.SelectByExhibitionData(shop_ID, itemIDList, option);
        }



        public DataTable SelectbyItemID(int itemid)
        {
            return detailW_dl.SelectbyItemID(itemid);
        }

        public DataTable SelectImage(int itemid, int shopid)
        {
            return detailW_dl.SelectbyImage(itemid, shopid);
        }
    }
}
