using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
    public class Promotion_Shop_DL
    {
        

        public void Insert(DataTable dt)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Promotion_Shop_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.Parameters.Add("@PromotionID", SqlDbType.NVarChar, 200, "PromotionID");
                cmd.Parameters.Add("@ShopID", SqlDbType.Int, 50, "ShopID");
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                da.InsertCommand = cmd;
                cmd.Connection.Open();
                da.Update(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void DeleteByPromotionID(int promotionID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "DELETE FROM Promotion_Shop WHERE Promotion_ID = " + promotionID;
                SqlCommand cmd = new SqlCommand(query, connectionString);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable SelectByPromotionID(int promotionID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SELECT * FROM Promotion_Shop WHERE Promotion_ID = "+promotionID;
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
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

                throw;
            }
        }


        public DataTable Select_TargetShop()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SELECT * FROM Promotion_Shop";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
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

                throw;
            }
        }






        public DataTable GetShopList(string promotionID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SP_Promotion_GetShopList", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@strString", promotionID);
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
