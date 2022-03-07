using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;

namespace ORS_RCM_DL
{
    public class Delivery_DL
    {
        public DataSet SelectAll()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Get_Delivery_Setting", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Connection.Open();
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.SelectCommand.Connection.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(int yshippingno, int rshippingno, string ordersetting, int userid)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Insert_Delivery_Setting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@YShippingNo", yshippingno);
                cmd.Parameters.AddWithValue("@RShippingNo", rshippingno);
                cmd.Parameters.AddWithValue("@OrderSetting", ordersetting);
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (id > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Update(int yshippingno, int rshippingno, string ordersetting, int userid, int id)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Update_Delivery_Setting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@YShippingNo", yshippingno);
                cmd.Parameters.AddWithValue("@RShippingNo", rshippingno);
                cmd.Parameters.AddWithValue("@OrderSetting", ordersetting);
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int ID = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (ID > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Delete_Delivery_Setting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString; ;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CheckExistingData(int yshippingno, int rshippingno, int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Check_Delivery_Setting", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@YShippingNo", yshippingno);
                da.SelectCommand.Parameters.AddWithValue("@RShippingNo", rshippingno);
                da.SelectCommand.Parameters.AddWithValue("@option", option);
                da.SelectCommand.Connection.Open();
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return 0;
                }
                else return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SelectShippingNumber(int yshippingno, int rshippingno, int option, int id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_SelectShippingNumber", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@yshippingno", yshippingno);
                da.SelectCommand.Parameters.AddWithValue("@rshippingno", rshippingno);
                da.SelectCommand.Parameters.AddWithValue("@option", option);
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                da.SelectCommand.Connection.Open();
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return 0;
                }
                else return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
