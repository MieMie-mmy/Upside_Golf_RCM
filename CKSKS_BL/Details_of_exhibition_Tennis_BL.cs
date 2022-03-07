using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
    public class Details_of_exhibition_Tennis_BL
    {
        Details_of_exhibition_Tennis_DL tcdl;
        public Details_of_exhibition_Tennis_BL()
        {
            tcdl = new Details_of_exhibition_Tennis_DL();
        }
        public DataTable SelectByExhibitionData(int shop_ID, string itemIDList, string option)
        {
            return tcdl.SelectByExhibitionData(shop_ID, itemIDList, option);
        }
    }
}
