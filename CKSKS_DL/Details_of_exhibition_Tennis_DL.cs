using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ORS_RCM_DL
{
    public class Details_of_exhibition_Tennis_DL
    {
        public DataTable SelectByExhibitionData(int shop_ID, string str, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectForTennic", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_ID);
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", str);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
