using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
    public class Jisha_Tax_Setting_DL
    {
        public DataTable SelectLatestJishaTaxSetting()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 1 *  FROM Jisha_Tax_Setting ORDER BY ID DESC", JsDataConfig.GetConnectionString());
                da.SelectCommand.CommandType = CommandType.Text;

                DataTable dt = new DataTable();
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
