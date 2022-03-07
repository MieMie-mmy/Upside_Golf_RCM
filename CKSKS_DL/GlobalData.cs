

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
    public class GlobalData
    {
        public DataTable Code_Setup(int Code_Type)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * From  Code_Setup WHERE  Code_ID !=3 and Code_ID!=5 and Code_Type= " + Code_Type, DataConfig.connectionString);
                da.SelectCommand.CommandType = CommandType.Text;

                da.SelectCommand.CommandTimeout = 0;

                DataTable dt = new DataTable();

                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
