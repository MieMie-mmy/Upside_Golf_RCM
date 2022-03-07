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
    public class Mall_Setting_Yahoo_Fixed_BL
    {
        Mall_Setting_Yahoo_Fixed_DL yDL;

        public Mall_Setting_Yahoo_Fixed_BL()
        {
            yDL = new Mall_Setting_Yahoo_Fixed_DL();

        }

        public String Insert(Mall_Setting_Yahoo_Fixed_Entity yhoentity)
        {

            if (yDL.Insert(yhoentity))
            {
                return "Save Successful !";
            }
            else
            {
                return "Save Fail !";
            }
        }


        public string Update(Mall_Setting_Yahoo_Fixed_Entity yhentity)
        {
            if (yDL.Update(yhentity))
            {
                return "Update Successful !";
            }
            else
            {
                return "Update Fail !";
            }

        }
            public Mall_Setting_Yahoo_Fixed_Entity SelectByID(int id)
       {

           return yDL.SelctByID(id);
       }
     
    
    }

    }

