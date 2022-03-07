using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
    public class Jisha_Order_No_Setting_DL
    {
        public DataTable SelectByCurrentDate()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 1 *  FROM Jisha_Order_No_Setting WHERE Date >= convert(date, getdate()) ORDER BY ID DESC", JsDataConfig.GetConnectionString());
                da.SelectCommand.CommandType = CommandType.Text;

                da.SelectCommand.CommandTimeout = 0;

                DataTable dt = new DataTable();
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex) { throw ex; }
        }

        public void Insert(Jisha_Order_No_Setting_Entity jishaOrderNoInfo)
        {
            SqlCommand cmd = new SqlCommand("SP_Jisha_Order_No_Setting_Insert", JsDataConfig.GetConnectionString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@Date", jishaOrderNoInfo.Date);
            cmd.Parameters.AddWithValue("@Order_No", jishaOrderNoInfo.OrderNo);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
