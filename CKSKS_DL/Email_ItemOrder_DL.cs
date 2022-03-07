/* 
Created By              : Aung Kyaw
Created Date          : 03/07/2014
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
using ORS_RCM_Common;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
    public class Email_ItemOrder_DL
    {
       

        public DataTable SearchItem(string shopName,string itemNumber,DateTime? fromDate, DateTime? toDate,int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Email_ItemOrder_Search", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                if(!String.IsNullOrWhiteSpace(shopName))
                    da.SelectCommand.Parameters.AddWithValue("@ShopName", shopName);
                else da.SelectCommand.Parameters.AddWithValue("@ShopName", DBNull.Value);
                if (!String.IsNullOrWhiteSpace(itemNumber))
                da.SelectCommand.Parameters.AddWithValue("@ItemNumber", itemNumber);
                else da.SelectCommand.Parameters.AddWithValue("@ItemNumber", DBNull.Value);
                da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                da.SelectCommand.Parameters.AddWithValue("@Option", option);
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


       public DataTable Search_ItemSeparatedOrderList(ItemSeparated_OrderList_Entity ise, int pgindex, String psize,int option)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
               
                 String storeProcedureName = String.Empty;
                if (option == 1)
                    storeProcedureName = "SP_ItemSeparatedOrder_equalSearch";
                else storeProcedureName = "SP_ItemSeparatedOrder_LikeSearch";
                SqlCommand cmd = new SqlCommand(storeProcedureName, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                
           
                if (!String.IsNullOrWhiteSpace(ise.Shop))
                 da.SelectCommand.Parameters.AddWithValue("@ShopName",ise.Shop);
                else da.SelectCommand.Parameters.AddWithValue("@ShopName",DBNull.Value);


                if (!String.IsNullOrWhiteSpace(ise.Item_Code))
                    da.SelectCommand.Parameters.AddWithValue("@itemCode",ise.Item_Code);
                else da.SelectCommand.Parameters.AddWithValue("@itemCode", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ise.Brand_Name))
                    da.SelectCommand.Parameters.AddWithValue("@brandname", ise.Brand_Name);
                else da.SelectCommand.Parameters.AddWithValue("@brandname", DBNull.Value);



                if (!String.IsNullOrWhiteSpace(ise.Item_Name))
                    da.SelectCommand.Parameters.AddWithValue("@Item_Name", ise.Item_Name);
                else da.SelectCommand.Parameters.AddWithValue("@Item_Name", DBNull.Value);


                if (!String.IsNullOrWhiteSpace(ise.Competition_Name))
                    da.SelectCommand.Parameters.AddWithValue("@competname", ise.Competition_Name);
                else da.SelectCommand.Parameters.AddWithValue("@competname", DBNull.Value);


                if (!String.IsNullOrWhiteSpace(ise.Year))
                    da.SelectCommand.Parameters.AddWithValue("@year", ise.Year);
                else da.SelectCommand.Parameters.AddWithValue("@year", DBNull.Value);


                if (!String.IsNullOrWhiteSpace(ise.Season))
                    da.SelectCommand.Parameters.AddWithValue("@season", ise.Season);
                else da.SelectCommand.Parameters.AddWithValue("@season", DBNull.Value);

                //if ((ise.Updated_By!=-1))
                    da.SelectCommand.Parameters.AddWithValue("@Updated_By", ise.Updated_By);
               // else da.SelectCommand.Parameters.AddWithValue("@Updated_By", DBNull.Value);
                               

                da.SelectCommand.Parameters.AddWithValue("@startDate", ise.fromDate);
                da.SelectCommand.Parameters.AddWithValue("@endDate", ise.toDate);

                da.SelectCommand.Parameters.AddWithValue("@PageIndex", pgindex);
                da.SelectCommand.Parameters.AddWithValue("@PageSize", psize);
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


       public DataTable Search_SKUSeparatedOrderList(SKUSeparated_OrderList_Entity skuent, int pgindex, String psize, int option)
       {
              try
              {
               SqlConnection con = new SqlConnection(DataConfig.connectionString);

               String storeProcedureName = String.Empty;
               if (option == 1)
                   storeProcedureName = "SP_SKUSeparatedOrder_equalSearch";
               else storeProcedureName = "SP_SKUSeparatedOrder_LikeSearch";
               SqlCommand cmd = new SqlCommand(storeProcedureName, con);
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;




               if (!String.IsNullOrWhiteSpace(skuent.Shop))
                   da.SelectCommand.Parameters.AddWithValue("@ShopName", skuent.Shop);
               else da.SelectCommand.Parameters.AddWithValue("@ShopName", DBNull.Value);


               if (!String.IsNullOrWhiteSpace(skuent.Item_Code))
                   da.SelectCommand.Parameters.AddWithValue("@itemCode", skuent.Item_Code);
               else da.SelectCommand.Parameters.AddWithValue("@itemCode", DBNull.Value);

               if (!String.IsNullOrWhiteSpace(skuent.Brand_Name))
                   da.SelectCommand.Parameters.AddWithValue("@brandname", skuent.Brand_Name);
               else da.SelectCommand.Parameters.AddWithValue("@brandname", DBNull.Value);



               if (!String.IsNullOrWhiteSpace(skuent.Item_Name))
                   da.SelectCommand.Parameters.AddWithValue("@Item_Name", skuent.Item_Name);
               else da.SelectCommand.Parameters.AddWithValue("@Item_Name", DBNull.Value);



               if (!String.IsNullOrWhiteSpace(skuent.Color))
                   da.SelectCommand.Parameters.AddWithValue("@color", skuent.Color);
               else da.SelectCommand.Parameters.AddWithValue("@color", DBNull.Value);


               if (!String.IsNullOrWhiteSpace(skuent.Size))
                   da.SelectCommand.Parameters.AddWithValue("@size", skuent.Size);
               else da.SelectCommand.Parameters.AddWithValue("@size ", DBNull.Value);



               if (!String.IsNullOrWhiteSpace(skuent.Competition_Name))
                   da.SelectCommand.Parameters.AddWithValue("@competname", skuent.Competition_Name);
               else da.SelectCommand.Parameters.AddWithValue("@competname", DBNull.Value);


               if (!String.IsNullOrWhiteSpace(skuent.Year))
                   da.SelectCommand.Parameters.AddWithValue("@year", skuent.Year);
               else da.SelectCommand.Parameters.AddWithValue("@year", DBNull.Value);


               if (!String.IsNullOrWhiteSpace(skuent.Season))
                   da.SelectCommand.Parameters.AddWithValue("@season", skuent.Season);
               else da.SelectCommand.Parameters.AddWithValue("@season", DBNull.Value);

               //if ((ise.Updated_By!=-1))
               da.SelectCommand.Parameters.AddWithValue("@Updated_By", skuent.Updated_By);
               // else da.SelectCommand.Parameters.AddWithValue("@Updated_By", DBNull.Value);


               da.SelectCommand.Parameters.AddWithValue("@startDate", skuent.fromDate);
               da.SelectCommand.Parameters.AddWithValue("@endDate", skuent.toDate);

               da.SelectCommand.Parameters.AddWithValue("@PageIndex", pgindex);
               da.SelectCommand.Parameters.AddWithValue("@PageSize", psize);

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


       

       public DataTable SearchSaleorderlist(Sale_ListScreen_Entity se, int pgindex,String psize)
        {
                    
            try
            {

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Salelist_Search", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;


                if (!String.IsNullOrWhiteSpace(se.Store_Name))
                da.SelectCommand.Parameters.AddWithValue("@ShopName", se.Store_Name);
                else da.SelectCommand.Parameters.AddWithValue("@ShopName",DBNull.Value);

                da.SelectCommand.Parameters.AddWithValue("@FromDate",se.fromDate);
                da.SelectCommand.Parameters.AddWithValue("@ToDate", se.toDate);
                da.SelectCommand.Parameters.AddWithValue("@PageIndex", pgindex);
                da.SelectCommand.Parameters.AddWithValue("@PageSize", psize);

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

        public DataTable SelectAll()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Email_ItemOrder ORDER BY Email_ItemOrder.Email_Date DESC", connectionString);
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
                throw ex;
            }
        }

        public DataTable SelectShop() 
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Shop WITH (NOLOCK) WHERE ID != 3", connectionString);
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
                throw ex;
            }
        
        }
    }
}
