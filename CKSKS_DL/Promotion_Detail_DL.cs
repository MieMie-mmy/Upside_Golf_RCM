using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
   public  class Promotion_Detail_DL
    {

       public  DataTable GetdataforRakuten(string list, int sid, string option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();

               string query = "SP_PromotionDetailofRakuten";
               SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.AddWithValue("@strString", list);
               da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
               da.SelectCommand.Parameters.AddWithValue("@option", option);
               da.SelectCommand.CommandTimeout = 0;


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
      public DataTable GetdataforPointRakuten(string list, int sid, int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();

               string query = "SP_PromotionPointDetailofRakuten";
               SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.AddWithValue("@strString", list);
               da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
               da.SelectCommand.Parameters.AddWithValue("@option", option);
               da.SelectCommand.CommandTimeout = 0;


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
      public  DataTable GetdataforDeliveryRakuten(string list, int sid, int option)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              DataTable dt = new DataTable();

              string query = "SP_PromotionDeliveryDetailofRakuten";
              SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.Parameters.AddWithValue("@strString", list);
              da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
              da.SelectCommand.Parameters.AddWithValue("@option", option);
              da.SelectCommand.CommandTimeout = 0;


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

      public DataTable GetdataforPonpare(string list, int sid, string option)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              DataTable dt = new DataTable();
              string query = "SP_PromotionDetailofPonpare";
              SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.Parameters.AddWithValue("@strString", list);
              da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
              da.SelectCommand.Parameters.AddWithValue("@option", option);
              da.SelectCommand.CommandTimeout = 0;
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
      public DataTable GetdataforPointPonpare(string list, int sid, int option)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              DataTable dt = new DataTable();

              string query = "SP_PromotionPointDetailofPonpare";
              SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.Parameters.AddWithValue("@strString", list);
              da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
              da.SelectCommand.Parameters.AddWithValue("@option", option);
              da.SelectCommand.CommandTimeout = 0;
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
      public DataTable GetdataforDeliveryPonpare(string list, int sid, int option)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              DataTable dt = new DataTable();
              string query = "SP_PromotionDeliveryDetailofPonpare";
              SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.Parameters.AddWithValue("@strString", list);
              da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
              da.SelectCommand.Parameters.AddWithValue("@option", option);
              da.SelectCommand.CommandTimeout = 0;
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
      public DataTable GetdataforJisha(string list, int sid, int option)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              DataTable dt = new DataTable();
              string query = "SP_DetailDeliveryPromotion_GetDataForJisha";
              SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.Parameters.AddWithValue("@strString", list);
              da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
              da.SelectCommand.Parameters.AddWithValue("@option", option);
              da.SelectCommand.CommandTimeout = 0;
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
    }
}
