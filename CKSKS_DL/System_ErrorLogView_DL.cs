using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ORS_RCM_DL
{
    public class System_ErrorLogView_DL
    {
        public void UpdateStatus(string id,string status)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("Update SYS_Error_Log SET Status=" + status + " WHERE ID=" + id, con);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
