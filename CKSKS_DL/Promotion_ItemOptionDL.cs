using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ORS_RCM_DL
{
    public class Promotion_ItemOptionDL
    {
        public bool Save(DataTable dtOption)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Promotion_ItemOption_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Parameters.Add("@Promotion_ID", SqlDbType.Int, 50, "Promotion_ID");
                cmd.Parameters.Add("@Option_Value", SqlDbType.NVarChar, 200, "Option_Value");
                cmd.Parameters.Add("@Option_Name", SqlDbType.NVarChar, 200, "Option_Name");
                sda.InsertCommand = cmd;
                sda.UpdateCommand = cmd;
                cmd.Connection.Open();
                sda.Update(dtOption);
                cmd.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int Promotion_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_ChangePromotionoptionflag", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Promotion_ID", Promotion_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectByPID(int pid)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string quary = "SELECT * FROM Promotion_ItemOption WITH(NOLOCK) WHERE Promotion_ID=@ID AND Ctrl_ID='n' ";
                SqlDataAdapter sda = new SqlDataAdapter(quary, connectionString);
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ID", pid);
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

        public DataTable SelectAll()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string quary = "SELECT * FROM Promotion_ItemOption";
                SqlDataAdapter sda = new SqlDataAdapter(quary, connectionString);
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
    }
}
