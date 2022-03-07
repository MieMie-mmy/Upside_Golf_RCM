using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
   public  class Mall_Setting_Ponpare_Fixed_BL
    {
       Mall_Setting_Ponpare_Fixed_DL pdl;
       public Mall_Setting_Ponpare_Fixed_BL() 
       {
           pdl = new Mall_Setting_Ponpare_Fixed_DL();
       }

       public String Insert(Mall_Setting_Ponpare_Fixed_Entity pentity)
       {

           if (pdl.Insert(pentity))
           {
               return "Save Successful !";
           }
           else
           {
               return "Save Fail !";
           }
       }

       public  Mall_Setting_Ponpare_Fixed_Entity SelectByID(int id)
       {

           return pdl.SelctByID(id);
       }

       public string Update( Mall_Setting_Ponpare_Fixed_Entity  pentity)
       {
           if (pdl.Update(pentity))
           {
               return "Update Successful !";
           }
           else
           {
               return "Update Fail !";
           }
       }
    }
}
