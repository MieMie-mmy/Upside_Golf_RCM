using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace J_Painttool_Exhibition_Console
{
    class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static DataTable dtItem = new DataTable();
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "J_Painttool_Exhibition_Console_New";
                DataTable dtShopList = GetShopList();
                if (dtShopList != null && dtShopList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtShopList.Rows)
                    {
                        dtItem = SelectLogExhibitionItem(int.Parse(dr["ID"].ToString()), int.Parse(dr["Mall_ID"].ToString()));
                        Export_CSV4 export = new Export_CSV4();
                        export.JPainttoolFilterSKU(dtItem, int.Parse(dr["ID"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "J_Painttool_Exhibition_Console_New " + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        static DataTable GetShopList()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                string quary = "SELECT ID,Mall_ID FROM Shop WHERE Mall_ID = 3";
                SqlDataAdapter sda = new SqlDataAdapter(quary, connectionstring);
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.CommandTimeout = 0;
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

        static DataTable SelectLogExhibitionItem(int shop_id, int mall_id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectByShop_JPainttool", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID", mall_id);
                sda.SelectCommand.CommandTimeout = 0;
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
