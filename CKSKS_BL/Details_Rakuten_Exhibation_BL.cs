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
  public  class Details_Rakuten_Exhibation_BL
    {
      Details_Rakuten_Exhibation_DL detailR_dl;

       public  Details_Rakuten_Exhibation_BL()
        {
            detailR_dl = new Details_Rakuten_Exhibation_DL();
          }

       public Details_Rakuten_Exhibation_Entity SelectByID(int id)
       {

           return   detailR_dl.SelctByID(id);
       }

       public DataTable SelectbyItemID(int itemid) 
       {
           return detailR_dl.SelectbyItemID(itemid);
       }

       public DataTable SelectImage(int itemid,int shopid) 
       {
           return detailR_dl.SelectbyImage(itemid,shopid);
       }

       public DataTable SelectByExhibitionData(int shop_ID, string itemIDList, string option)
       {
           return detailR_dl.SelectByExhibitionData(shop_ID, itemIDList, option);
       }

    }
}
