using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ORS_RCM_DL
{
    public class Exhibition_Error_DL
    {

        public Exhibition_Error_DL()
        {

        }
        

        public DataTable SelectExhibitionError()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                // SqlCommand cmd = new SqlCommand("SELECT * FROM Item_Export_ErrorCheck", DataConfig.GetConnectionString());
                SqlCommand cmd = new SqlCommand("SP_Error_List_SelectAll", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();

                return dt;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectExhibitionInfo()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_ExhibitionResult_Info", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();

                return dt;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectSalePrice()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_SalePrice_Info", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectOrderCount()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_Order_Info", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SelectWaitingItemCode()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_SelectWaitingItemCode", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
