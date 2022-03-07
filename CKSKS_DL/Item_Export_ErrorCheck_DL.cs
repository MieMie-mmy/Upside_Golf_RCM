using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
    public class Item_Export_ErrorCheck_DL
    {
        public void Insert(Item_Export_ErrorCheck_Entity itemExportErrorCheck, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Export_ErrorCheck_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ID", itemExportErrorCheck.ID);
                cmd.Parameters.AddWithValue("@Item_Code", itemExportErrorCheck.ItemCode);
                cmd.Parameters.AddWithValue("@Error_Description", itemExportErrorCheck.ErrorDescription);
                cmd.Parameters.AddWithValue("@Check_Type", itemExportErrorCheck.CheckType);
                cmd.Parameters.AddWithValue("@Created_Date", itemExportErrorCheck.CreatedDate);
                cmd.Parameters.AddWithValue("@Option", option);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Get_NewCreated_ItemData()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Get_NewCreated_ItemData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch
            {
                return dt;
            }
        }

        public Boolean CheckItem_Code(string item_code)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("Select Item_Code from Item_Master where Item_Code = '" + item_code + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();
                if (dt.Rows.Count > 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean CheckUpdateItem_Code(string item_code, int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("Select Item_Code from Item_Master where Item_Code = '" + item_code + "' and ID = " + id, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();
                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public void Insert_Created_ItemData(string item_code, string item_name)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Modify_Created_ItemData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", DBNull.Value);
                cmd.Parameters.AddWithValue("@item_code", item_code);
                cmd.Parameters.AddWithValue("@item_name", item_name);
                cmd.Parameters.AddWithValue("@option", 0);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch
            {
                throw;
            }
        }

        public void Delete_Created_ItemData(int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Modify_Created_ItemData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@item_code", DBNull.Value);
                cmd.Parameters.AddWithValue("@item_name", DBNull.Value);
                cmd.Parameters.AddWithValue("@option", 1);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch
            {
                throw;
            }
        }

        public void Update_Created_ItemData(int id, string item_code, string item_name)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Modify_Created_ItemData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@item_code", item_code);
                cmd.Parameters.AddWithValue("@item_name", item_name);
                cmd.Parameters.AddWithValue("@option", 2);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch
            {
                throw;
            }
        }
    }
}
