using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ORS_RCM_DL
{
   public class Promotion_Attatchment_DL
    {
       

       public void InsertUpdate(DataTable dt)
       {
           try
           {
               foreach (DataRow dr in dt.Rows)
               {
                   SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                   SqlCommand cmd = new SqlCommand();
                   cmd.CommandText = "SP_Promotion_Attatchment_InsertUpdate";
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.CommandTimeout = 0;
                   cmd.Connection = connectionString;
                   cmd.Parameters.AddWithValue("@ID", dr["ID"]);
                   cmd.Parameters.AddWithValue("@Promotion_ID", dr["Promotion_ID"]);
                   cmd.Parameters.AddWithValue("@File_Name", dr["File_Name"]);
                   cmd.Parameters.AddWithValue("@File_Type", dr["File_Type"]);
                   cmd.Parameters.AddWithValue("@Option", "Save");
                   cmd.Connection.Open();
                   cmd.ExecuteNonQuery();
                   cmd.Connection.Close();
               }
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
               string query = "DELETE FROM Promotion_Attachment WHERE Promotion_ID = " + promotionID;
               SqlCommand cmd = new SqlCommand(query, connectionString);
               cmd.CommandType = CommandType.Text;
               cmd.CommandTimeout = 0;
               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               cmd.Connection.Close();
           }
           catch (Exception ex)
           {
               throw;
           }
       }

       public DataTable SelectByPromotionID(int promotionID)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               string query = "SELECT * FROM Promotion_Attachment WHERE Promotion_ID = " + promotionID;
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

    }
}
