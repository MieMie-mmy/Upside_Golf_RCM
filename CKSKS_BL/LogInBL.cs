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
    public class LogInBL
    {
        LogInDL log;
        public LogInBL()
        {
         log = new LogInDL();
        }

        public DataTable logincheck (string LoginID)
        {
          
        return log.LogINCheck(LoginID);
        }

        public DataTable getAdmin(string loginID)
        {
            return log.getAdmin(loginID);
        }

        public int Check(string loginID,string password)
        {
         
        return  log.Check(loginID,password);
        }

        public bool Check_PageAccess(int id, string url, string pagecode)
        {
            return log.Check_PageAccess(id, url, pagecode);

        }
        public User_entity SelectPassword(string LogIn_ID)
        {
            return log.SelectPassword(LogIn_ID);

        }
    }
}
