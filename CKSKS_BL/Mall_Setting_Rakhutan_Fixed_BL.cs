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
   public  class Mall_Setting_Rakhutan_Fixed_BL
    {
       Mall_Setting_Rakhutan_Fixed_DL rdl;
       public Mall_Setting_Rakhutan_Fixed_BL() 
       {
           rdl = new Mall_Setting_Rakhutan_Fixed_DL();
       }
       public String Insert(Mall_Setting_Rakhutan_Fixed_Entity rentity)
       {

           if (rdl.Insert(rentity))
           {
               return "Save Successful !";
           }
           else
           {
               return "Save Fail !";
           }
       }

       public Mall_Setting_Rakhutan_Fixed_Entity SelectByID(int id)
       {
          
           return rdl.SelctByID(id);
       }

             

       public string Update(Mall_Setting_Rakhutan_Fixed_Entity rentity)
       {
           if (rdl.Update(rentity))
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
