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
   public  class Mall_Setting_Yahoo_Default_BL
    {
       Mall_Setting_Yahoo_Default_DL  ydDL;

        public Mall_Setting_Yahoo_Default_BL()
      {
          ydDL =new  Mall_Setting_Yahoo_Default_DL();

      }

        public String Insert(Mall_Setting_Yahoo_Default_Entity  ydentity)
      {

          if (ydDL.Insert(ydentity))
          {
              return "Save Successful !";
          }
          else
          {
              return "Save Fail !";
          }
      }


          public string Update(Mall_Setting_Yahoo_Default_Entity yhentity)
        {
            if (ydDL.Update(yhentity))
            {
                return "Update Successful !";
            }
            else
            {
                return "Update Fail !";
            }

        }
            public Mall_Setting_Yahoo_Default_Entity SelectByID(int id)
       {

           return ydDL.SelctByID(id);
       }
     
    
    }

    }



    

