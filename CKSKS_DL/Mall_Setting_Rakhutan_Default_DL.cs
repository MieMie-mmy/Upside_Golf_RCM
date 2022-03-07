/* 
Created By              : Kay Thi Aung
Created Date          : 25/06/2014
Updated By             :
Updated Date         :

 Tables using: 
    -
    -
    -
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
   public  class Mall_Setting_Rakhutan_Default_DL
    {
       

       public Mall_Setting_Rakhutan_Default_DL(){}

       public bool Insert(Mall_Setting_Rakhutan_Default_Entity Rentity)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand();
           try
           {
               cmd.CommandText = "SP_mall_setting_Rakuten_Default_Insert";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Connection = connectionString;
               cmd.Parameters.AddWithValue("@post", Rentity.Postage);
               cmd.Parameters.AddWithValue("@shop", Rentity.ExtraShop);
               cmd.Parameters.AddWithValue("@delivery", Rentity.Delivery_Charges);
               cmd.Parameters.AddWithValue("@warehouse", Rentity.Warehouse);
               cmd.Parameters.AddWithValue("@search", Rentity.Searchhide);
               cmd.Parameters.AddWithValue("@password", Rentity.Password);
               cmd.Parameters.AddWithValue("@dual", Rentity.DualPrice);
               cmd.Parameters.AddWithValue("@featureItem", Rentity.Featured_Item);
               
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

       public bool Update(Mall_Setting_Rakhutan_Default_Entity Rentity)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand();
           try
           {
               cmd.CommandText = "SP_mall_setting_Rakuten_Default_Update";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Connection = connectionString;
               cmd.Parameters.AddWithValue("@ID", Rentity.ID);
               cmd.Parameters.AddWithValue("@Shop_ID", Rentity.ShopID);
               cmd.Parameters.AddWithValue("@post", Rentity.Postage);
               cmd.Parameters.AddWithValue("@shop", Rentity.ExtraShop);
               cmd.Parameters.AddWithValue("@delivery", Rentity.Delivery_Charges);
               cmd.Parameters.AddWithValue("@warehouse", Rentity.Warehouse);
               cmd.Parameters.AddWithValue("@search", Rentity.Searchhide);
               cmd.Parameters.AddWithValue("@password", Rentity.Password);
               cmd.Parameters.AddWithValue("@dual", Rentity.DualPrice);
               cmd.Parameters.AddWithValue("@featureItem", Rentity.Featured_Item);
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

       public Mall_Setting_Rakhutan_Default_Entity SelctByID(int Shop_ID)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlDataAdapter sda = new SqlDataAdapter("SP_mall_setting_Rakuten_Default_SelectbyID", connectionString);
           try
           {
               sda.SelectCommand.CommandType = CommandType.StoredProcedure;
               sda.SelectCommand.CommandTimeout = 0;
               sda.SelectCommand.Parameters.AddWithValue("@ID", Shop_ID);

               DataTable dt = new DataTable();

               sda.SelectCommand.Connection.Open();
               sda.Fill(dt);
               sda.SelectCommand.Connection.Close();
               Mall_Setting_Rakhutan_Default_Entity rentity = new Mall_Setting_Rakhutan_Default_Entity();
               if (dt.Rows.Count > 0)
               {
                   rentity.ID = (int)dt.Rows[0]["ID"];
                   rentity.Shop_Name = dt.Rows[0]["Shop_Name"].ToString();
                   rentity.Mall_Name = dt.Rows[0]["Mall_Name"].ToString();
                   rentity.Postage = (int)dt.Rows[0]["Postage"];
                   rentity.ExtraShop = dt.Rows[0]["Extra_Shopping"].ToString();
                   rentity.Delivery_Charges=(int) dt.Rows[0]["Delivery_Charges"];
                   rentity.Warehouse =(int) dt.Rows[0]["Warehouse_Specified"];
                   rentity.Searchhide =(int) dt.Rows[0]["Search_Hide"];
                   rentity.Password = dt.Rows[0]["BlackMarket_Password"].ToString();
                   rentity.DualPrice =(int) dt.Rows[0]["DualPricing_Ctrl_No"];
                   rentity.Featured_Item = dt.Rows[0]["Featured_Item"].ToString();
               }

               return rentity;
           }
           catch (Exception ex)
           {

               throw ex;
           }

       }

    }
}
