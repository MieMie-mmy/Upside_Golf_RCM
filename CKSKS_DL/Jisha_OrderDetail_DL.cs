using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
    public class Jisha_OrderDetail_DL
    {
        public DataTable SelectAll()
        {
            String query = "SELECT * FROM Jisha_OrderDetail";
            SqlCommand cmd = new SqlCommand(query, JsDataConfig.GetConnectionString());
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();

            return dt;
        }
    }
}
