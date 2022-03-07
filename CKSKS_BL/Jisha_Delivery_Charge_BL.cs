using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;
using ORS_RCM_Common;

namespace ORS_RCM_BL
{
    public class Jisha_Delivery_Charge_BL
    {
        Jisha_Delivery_Charge_DL jhdl;
        public Jisha_Delivery_Charge_BL() 
        {
            jhdl = new Jisha_Delivery_Charge_DL();
        }

        public  string Insert( DataTable dt,int option) 
        {
         return jhdl.Insert(dt,option);
        //if (result)
        //    return true;
        //else
        //    return false;
        }

        public DataTable SelectAll( string priority,int option) 
        {
            return jhdl.SelectAll(priority,option);
        }
        public DataTable SlectDivision() 
        {
            return jhdl.Slectdiv();
        }

        public DataTable Searchitem(string name) 
        {
            return jhdl.Search(name);
        }
        public string  CODInsert(DataTable dt,int option) 
        {
             return  jhdl.CODInsert(dt,option);
            
      }

        public DataTable SelectDeliveryChargeByDivisionID(int divID)
        {
            return jhdl.SelectDeliveryChargeByDivisionID(divID);
        }

        public DataTable SelectCODChargeByDivisionID(int divID)
        {
            return jhdl.SelectCODChargeByDivisionID(divID);
        }
    }
}
