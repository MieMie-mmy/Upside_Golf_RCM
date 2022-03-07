using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_Common;
using System.Data.SqlClient;
using System.Data;

namespace ORS_RCM_DL
{
    public class Promotion_Item_DL
    {
        public void Insert_CAMPromotion_Item(int promotionID, DataTable dt)
        {
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SP_Promotion_Item_InsertUpdate";
                    cmd.Connection = connectionString;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@Promotion_ID", promotionID);
                    cmd.Parameters.AddWithValue("@Item_Code", dr["Item_Code"]);
                    cmd.Parameters.AddWithValue("@Option", "Save");

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert_Promotion_Item(int promotionID, string[] arr)
        {
            try
            {
                foreach (string itemCode in arr)
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SP_Promotion_Item_InsertUpdate";
                    cmd.Connection = connectionString;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@Promotion_ID", promotionID);
                    cmd.Parameters.AddWithValue("@Item_Code", itemCode);
                    cmd.Parameters.AddWithValue("@Option", "Save");

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_Promotion_Item(int promotionID, string[] arr)
        {
            try
            {
            foreach (string itemCode in arr)
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Promotion_Item_InsertUpdate";
                cmd.Connection = connectionString;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.AddWithValue("@ID", 0);
                cmd.Parameters.AddWithValue("@Promotion_ID", promotionID);
                cmd.Parameters.AddWithValue("@Item_Code", itemCode);
                cmd.Parameters.AddWithValue("@Option", "Update");

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectByPromotionID(int promotionID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Promotion_Item WHERE Promotion_ID = " + promotionID, connectionString);
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

        public void DeleteByPromotionID(int promotionID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "DELETE FROM Promotion_Item WHERE Promotion_ID = "+promotionID;
                SqlCommand cmd = new SqlCommand(query, connectionString);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
