using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
 public   class Item_Export_QList_DL
    {

     

     public DataTable SelectAll(string brand,string cat,string itno,string itname,DateTime ?postdate,DateTime ?avadate,string supplier) 
     {
         try {
             SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
             SqlDataAdapter da = new SqlDataAdapter("SP_Item_ExportQList_Search ", connectionString);
             da.SelectCommand.CommandType = CommandType.StoredProcedure;
             da.SelectCommand.CommandTimeout = 0;
             da.SelectCommand.Parameters.AddWithValue("@itemname",itname);
               da.SelectCommand.Parameters.AddWithValue("@itemcode", itno);
               da.SelectCommand.Parameters.AddWithValue("@catno", cat);
               da.SelectCommand.Parameters.AddWithValue("@brand", brand);
               da.SelectCommand.Parameters.AddWithValue("@postdate", postdate);
               da.SelectCommand.Parameters.AddWithValue("@reledate",avadate);
               da.SelectCommand.Parameters.AddWithValue("@supplier", supplier);
             DataTable dt = new DataTable();
             da.SelectCommand.Connection.Open();
             da.SelectCommand.ExecuteNonQuery();
             da.Fill(dt);
             da.SelectCommand.Connection.Close();
             return dt;

         }
         catch (Exception ex) { throw ex; }
     
     }

     public DataTable SelectAllData()
     {
         try
         {
             SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
             //SqlDataAdapter da = new SqlDataAdapter("Select ID,Item_Name,Item_Code,Brand_Name,Post_Available_Date,Catalog_Information   FROM Item_Master WHERE Export_Status ="+2, DataConfig.connectionString);
             SqlDataAdapter da = new SqlDataAdapter("SP_Item_Export_List_SelectAll", connectionString);
             da.SelectCommand.CommandType = CommandType.StoredProcedure;
             da.SelectCommand.CommandTimeout = 0;
           
             DataTable dt = new DataTable();
             da.SelectCommand.Connection.Open();
             da.SelectCommand.ExecuteNonQuery();
             da.Fill(dt);
             da.SelectCommand.Connection.Close();
             return dt;

         }
         catch (Exception ex) { throw ex; }

     }
    }
}
