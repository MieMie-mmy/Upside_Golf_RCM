/*
Created By              :PPK reference by Aye Aye Mon
Created Date          :01/06/2015
Updated By             :
Updated Date         :
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;

namespace Rakuten_Exhibition_Console_New
{
    public class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static DataTable dtImage = new DataTable();
        static DataTable dtItem = new DataTable();
        static DataTable dtSelect = new DataTable();
        static DataTable dtCategory = new DataTable();
        static DataTable dtItemCode = new DataTable();

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Rakuten_Exhibition_Console_New";
                DataTable dtShopList = GetShopList();
                if (dtShopList != null && dtShopList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtShopList.Rows)
                    {
                        string list = SelectExhibitItemID(int.Parse(dr["ID"].ToString()));
                        if (!string.IsNullOrWhiteSpace(list))
                        {
                            dtImage = SelectLogExhibitionImage(int.Parse(dr["ID"].ToString()),list);
                            dtItem = SelectLogExhibitionItem(int.Parse(dr["ID"].ToString()), int.Parse(dr["Mall_ID"].ToString()),list);
                            dtSelect = SelectLogExhibitionSelect(int.Parse(dr["ID"].ToString()), int.Parse(dr["Mall_ID"].ToString()),list);
                            dtCategory = SelectLogExhibitionCategory(int.Parse(dr["ID"].ToString()), int.Parse(dr["Mall_ID"].ToString()),list);
                            Export_CSV4 export = new Export_CSV4();
                            export.RakutenFilterSKU(dtImage, dtSelect, dtItem, dtCategory, int.Parse(dr["ID"].ToString()));
                        }
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
                cmd.Parameters.AddWithValue("@ErrorDetail", "Rakuten_Exhibition_Console_New " + ex.ToString());
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
                string quary = "SELECT ID,Mall_ID FROM Shop WHERE Mall_ID = 1";
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

        static DataTable SelectLogExhibitionItem(int shop_id, int mall_id,string list)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectByShop_Rakuten", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID", mall_id);
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", list);
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

        static DataTable SelectLogExhibitionSelect(int shop_id, int mall_id,string list)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Select_SelectByShop_Rakuten", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID", mall_id);
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", list);
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

        static DataTable SelectLogExhibitionCategory(int shop_id, int mall_id,string list)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Category_SelectByShop_Rakuten", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID", mall_id);
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", list);
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

        static DataTable SelectLogExhibitionImage(int shop_id,string list)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetImageList_Select_By_Mall", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", list);
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

        static DataTable SelectItemCodeList()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_SelectItemCodeList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                DataTable dt = new DataTable();
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                sqlData.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static string SelectExhibitItemID(int shopid)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_ItemID_List", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID",shopid);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt.Rows[0]["Exhibit_ID"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
