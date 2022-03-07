using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_Common
{
     public class UserRole_Entity
    {
        DataTable dtUserRole;

       public DataTable UserRole
       { 
               get{return dtUserRole;}
               set { dtUserRole = value; }
       }
       public UserRole_Entity()
       {
           dtUserRole = new DataTable();
           dtUserRole.Columns.Add("ID",typeof(int));
           dtUserRole.Columns.Add("UserID",typeof(int));
           dtUserRole.Columns.Add("MenuID", typeof(int));
           dtUserRole.Columns.Add("CanRead", typeof(bool));
           

       }
    }
}
