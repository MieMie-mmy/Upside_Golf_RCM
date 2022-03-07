using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
    public class Item_View3_DL
    {
        

        public Item_View3_DL()
        {}

        public Boolean Change_DataComplete(String ItemID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("Update Item_Master Set Export_Status=3 Where ID=" + ItemID, connectionString);
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable GetMallByShopID(String shopID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SELECT Mall_ID,Shop_SiteName FROM Shop WITH (NOLOCK) WHERE Shop_Name=N'" + shopID + "'", connectionString);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
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
