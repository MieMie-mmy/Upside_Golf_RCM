using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
   public  class GlobalBL
    {
       GlobalData globalData;

       public DataTable Code_Setup(int Code_Type)
       {
           try
           {
               globalData = new GlobalData();
               return globalData.Code_Setup( Code_Type);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

    }
}
