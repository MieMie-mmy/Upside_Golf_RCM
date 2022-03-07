using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
    public class Jisha_Order_Download_DL
    {
        public Jisha_Order_Download_DL() { }

        public DataTable SelectAll() 
        {
            try 
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SP_Jisha_Order_Download", JsDataConfig.GetConnectionString());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                return dt;
            
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable SelectByDate(String date)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_Order_Download_SelectByDate", JsDataConfig.GetConnectionString());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Date", date);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex) { throw ex; }

        }
    }
}
