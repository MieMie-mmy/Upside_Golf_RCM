using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_Common;
using ORS_RCM_DL;
using System.Data;
using System.Data.SqlClient;
namespace ORS_RCM_BL
{
    public class Details_of_Yahoo_Exhibition_BL
    {
        Details_Yahoo_Exhibation_DL detailYdl;

        public Details_of_Yahoo_Exhibition_BL()
        {
            detailYdl = new Details_Yahoo_Exhibation_DL();
          }


        public Details_of_exhibition_Yahoo_Entity SelectByID(int id)
        {

            return detailYdl.SelectByID(id);
        }

        public DataTable SelectbyItemID(int itemid) 
        {

            return detailYdl.SelectbyItemID(itemid);
        }

        public DataTable SelectByExhibitionData(string itemIDList, string option)
        {
            return detailYdl.SelectByExhibitionData(itemIDList, option);
        }

    }
}
