/* 
Created By              : Eephyo
Created Date          : 26/06/2014
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
    public class Mall_Setting_Yahoo_Fixed_DL
    {
        

        public bool Insert(Mall_Setting_Yahoo_Fixed_Entity Yhoentity)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Jisha_Order_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@shopid", Yhoentity.Shop_ID);
                cmd.Parameters.AddWithValue("@special_price", Yhoentity.Special_Price);
                cmd.Parameters.AddWithValue("@comment", Yhoentity.Word_Comment);
                cmd.Parameters.AddWithValue("@tax", Yhoentity.Taxable);
                cmd.Parameters.AddWithValue("@Release_Date", System.DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@provisional_period", Yhoentity.Provisional_Period);
                cmd.Parameters.AddWithValue("@template", Yhoentity.Template);
                cmd.Parameters.AddWithValue("@NumofPurchases", Yhoentity.NoofPurchases);
                cmd.Parameters.AddWithValue("@product_state", Yhoentity.Product_State);
                cmd.Parameters.AddWithValue("@Listing_Japan", Yhoentity.Listing_Japan);

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

        public bool Update(Mall_Setting_Yahoo_Fixed_Entity yhentity)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Mall_Setting_Yahoo_Fixed_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@ID", yhentity.ID);
                cmd.Parameters.AddWithValue("@shopid", yhentity.Shop_ID);
                cmd.Parameters.AddWithValue("@special_Price", yhentity.Special_Price);
                cmd.Parameters.AddWithValue("@word_Comment", yhentity.Word_Comment);
                cmd.Parameters.AddWithValue("@taxable", yhentity.Taxable);
                cmd.Parameters.AddWithValue("@release_date", yhentity.Release_Date);
                cmd.Parameters.AddWithValue("@provisional_period", yhentity.Provisional_Period);
                cmd.Parameters.AddWithValue("@template", yhentity.Template);
                cmd.Parameters.AddWithValue("@noofPurchases", yhentity.NoofPurchases);
                cmd.Parameters.AddWithValue("@product_state", yhentity.Product_State);
                cmd.Parameters.AddWithValue("@listing_Japan", yhentity.Listing_Japan);
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

        public Mall_Setting_Yahoo_Fixed_Entity SelctByID(int ID)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlDataAdapter sda = new SqlDataAdapter("SP_Mall_Setting_Yahoo_Fixed_SelectbyID", connectionString);
           try
           {
               sda.SelectCommand.CommandType = CommandType.StoredProcedure;
               sda.SelectCommand.CommandTimeout = 0;
               sda.SelectCommand.Parameters.AddWithValue("@ID", ID);

               DataTable dt = new DataTable();

               sda.SelectCommand.Connection.Open();
               sda.Fill(dt);
               sda.SelectCommand.Connection.Close();
               Mall_Setting_Yahoo_Fixed_Entity yhentity = new Mall_Setting_Yahoo_Fixed_Entity();
               if (dt.Rows.Count > 0)
               {
                 yhentity.ID = (int)dt.Rows[0]["ID"];

                 yhentity.Shop_Name = dt.Rows[0]["Shop_Name"].ToString();

                 yhentity.Mall_Name = dt.Rows[0]["Mall_Name"].ToString();

                 //if (dt.Rows[0]["Special_Price"].ToString()!="")
                 //{
                     yhentity.Special_Price = dt.Rows[0]["Special_Price"].ToString();

                // }
                 yhentity.Word_Comment = (dt.Rows[0]["Word_Comment"].ToString());
                 yhentity.Taxable = (int)dt.Rows[0]["Taxable"];


                 //dt.Rows[0]["Release_Date"]= String.Empty;
                 if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Release_Date"].ToString()))
                   {
                        
                       DateTime date = (DateTime)dt.Rows[0]["Release_Date"];
                       yhentity.Release_Date = date;
                   }
                 yhentity.Provisional_Period = dt.Rows[0]["Provisional_Period"].ToString();
                 yhentity.Template = dt.Rows[0]["Template"].ToString();
                 yhentity.NoofPurchases = dt.Rows[0]["NoofPurchases"].ToString();
                 yhentity.Product_State =Convert.ToInt32(dt.Rows[0]["Product_State"].ToString());
                 yhentity.Listing_Japan = dt.Rows[0]["Listing_Japan"].ToString();
               }
               return   yhentity;
           }
           catch (Exception ex)
           {
               throw ex;
           }
        }

    }
}
