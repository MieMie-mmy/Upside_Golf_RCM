using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_Common;
using System.Data.SqlClient;
using System.Data;

namespace ORS_RCM_DL
{
   public class Details_of_Promotion_Yahoo_Exhibition_DL
    {

       public DataTable DetailGetDataPromotionPoint(string strString, int shopId, int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();
               SqlDataAdapter sda = new SqlDataAdapter("SP_DetailGetdata_PromotionPoint_Yahoo", connectionString);
               sda.SelectCommand.CommandType = CommandType.StoredProcedure;
               sda.SelectCommand.CommandTimeout = 0;
               sda.SelectCommand.Parameters.AddWithValue("@strString", strString);
               sda.SelectCommand.Parameters.AddWithValue("@ShopID", shopId);
               sda.SelectCommand.Parameters.AddWithValue("@option", option);
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


       public DataTable DetailGetDataPromotionDelivery(string strString, int shopId, int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();
               SqlDataAdapter sda = new SqlDataAdapter("SP_DetailGetData_PromotionDeliveryYahoo", connectionString);
               sda.SelectCommand.CommandType = CommandType.StoredProcedure;
               sda.SelectCommand.CommandTimeout = 0;
               sda.SelectCommand.Parameters.AddWithValue("@strString", strString);
               sda.SelectCommand.Parameters.AddWithValue("@ShopID", shopId);
               sda.SelectCommand.Parameters.AddWithValue("@option", option);
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

       public DataTable DetailGetDataPromotionCamapign(string strString, int shopId, string option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();
               SqlDataAdapter sda = new SqlDataAdapter("SP_DetailCampaingPromotion_GetDataForYahoo", connectionString);
               sda.SelectCommand.CommandType = CommandType.StoredProcedure;
               sda.SelectCommand.CommandTimeout = 0;
               sda.SelectCommand.Parameters.AddWithValue("@PromotionID", strString);
               sda.SelectCommand.Parameters.AddWithValue("@ShopID", shopId);
               sda.SelectCommand.Parameters.AddWithValue("@option", option);
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
