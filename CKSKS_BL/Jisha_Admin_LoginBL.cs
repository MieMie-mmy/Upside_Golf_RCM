using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
    public class Jisha_Admin_LoginBL
    {
        Jisha_Admin_LoginDL jhdl;
        public Jisha_Admin_LoginBL() 
        {
            jhdl = new Jisha_Admin_LoginDL();
        }
        public int  check(string LoginID,string password)
        {

            return jhdl.Check(LoginID,password);
        }
        public DataTable logincheck(string LoginID)
        {

            return jhdl.LogINCheck(LoginID);
        }
    }
}
