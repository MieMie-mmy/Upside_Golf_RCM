using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Upside_Golf_RCM_Common;
using Upside_Golf_RCM_DL;
using System.Data;
using System.Data.SqlClient;

namespace Upside_Golf_RCM_BL
{
    public class Details_ORS_Exhibition_BL
    {
        Details_ORS_Exhibition_DL detailO_dl;

       public  Details_ORS_Exhibition_BL()
        {
            detailO_dl = new Details_ORS_Exhibition_DL();
          }


        public DataTable SelectByExhibitionData(int shop_ID, string itemIDList, string option)
        {
            return detailO_dl.SelectByExhibitionData(shop_ID, itemIDList, option);
        }
    }
}
