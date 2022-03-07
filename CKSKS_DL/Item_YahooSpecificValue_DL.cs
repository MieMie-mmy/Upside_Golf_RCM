using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
    public class Item_YahooSpecificValue_DL
    {
        

        public Item_YahooSpecificValue_DL()
        { 
        
        }

        public bool Insert(DataTable dt)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_YahooSpecificValue_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Parameters.Add("@Item_ID", SqlDbType.Int, 50, "Item_ID");
                cmd.Parameters.Add("@Spec_ID", SqlDbType.NVarChar, 50, "Spec_ID");
                cmd.Parameters.Add("@Spec_Name", SqlDbType.NVarChar, 50, "Spec_Name");
                cmd.Parameters.Add("@Spec_ValueID", SqlDbType.NVarChar, 50, "Spec_ValueID");
                cmd.Parameters.Add("@Spec_ValueName", SqlDbType.NVarChar, 50, "Spec_ValueName");
                cmd.Parameters.Add("@Type", SqlDbType.Int, 50, "Type");
                sda.InsertCommand = cmd;
                sda.UpdateCommand = cmd;
                cmd.Connection.Open();
                sda.Update(dt);
                cmd.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("DELETE FROM Item_YahooSpecificValue WHERE Item_ID=@Item_ID", connectionString);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", Item_ID);
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

        public DataTable SelectByItemID(int Item_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Item_YahooSpecificValue WITH (NOLOCK) WHERE Item_ID=@Item_ID ORDER BY Type", connectionString);
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_ID", Item_ID);
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
