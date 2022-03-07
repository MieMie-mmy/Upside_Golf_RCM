using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
     public class Details_Amazone_Exhibition_DL
    {

         public DataTable SelectbyID(int id) 
         {
             try
             {
                 SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                 SqlDataAdapter da = new SqlDataAdapter("SP_DetailsofAmazone_Exhibition", connectionString);
                 da.SelectCommand.CommandType = CommandType.StoredProcedure;
                 da.SelectCommand.CommandTimeout = 0;
                 da.SelectCommand.Parameters.AddWithValue("@Shop_ID",id);
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

         public DataTable SelectByExhibitionData(string str)
         {
             try
             {
                 SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                 DataTable dt = new DataTable();
                 SqlDataAdapter sda = new SqlDataAdapter("SP_ExhibitionDetailForAmazon", connectionString);
                 sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                 sda.SelectCommand.CommandTimeout = 0;
                 sda.SelectCommand.Parameters.AddWithValue("@strItemID", str);
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
