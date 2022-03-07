using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;
namespace ORS_RCM_DL
{
    public class Details_ORS_Exhibition_DL
    {
        public DataTable SelectByExhibitionData(int shop_ID, string str, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                //SqlDataAdapter sda = new SqlDataAdapter("SP_ExhibitionDetailForRakuten", connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectForORS", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_ID);
                //sda.SelectCommand.Parameters.AddWithValue("@strString", str);
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
