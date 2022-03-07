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
   public  class Details_Ponpare_Exhibition_BL
   {
       Details_Ponpare_Exhibition_DL detailPon_dl;

        public  Details_Ponpare_Exhibition_BL()
        {
            detailPon_dl= new Details_Ponpare_Exhibition_DL();
          }

        //public Details_Ponpare_Exhibation_Entity SelectByID(int id)
        public DataTable SelectByID(int id)
        {

            return detailPon_dl.SelctByID(id);
        }

        public DataTable SelectbyItemID(int itemid) 
        {
            return detailPon_dl.SelectbyItemID(itemid);
        }

        public DataTable SelectbyImage(int itemid, int shopid) 
        {
            return detailPon_dl.SelectbyImage(itemid,shopid);
        }

        public DataTable SelectByExhibitionData(int shop_ID, string itemIDList, string option)
        {
            return detailPon_dl.SelectByExhibitionData(shop_ID, itemIDList, option);
        }
   }
}
