using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
   
    public class Jisha_Order_Download_BL
    {
        Jisha_Order_Download_DL jhdl;
        public Jisha_Order_Download_BL()
        {
            jhdl = new Jisha_Order_Download_DL();
        }

        public DataTable SelectAll() 
        {
            return jhdl.SelectAll();
        }

        public DataTable SelectByDate(String dt)
        {
            return jhdl.SelectByDate(dt);
        }
    }
}
