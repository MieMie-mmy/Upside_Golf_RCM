/* 
Created By              : Kay Thi Aung
Created Date          : 25/06/2014
Updated By             :
Updated Date         :

 Tables using: Shop,Mall_Setting_Rakhutan_Fixed,Code_Setup 
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
using System.Globalization;

namespace ORS_RCM_DL
{
   public  class Mall_Setting_Rakhutan_Fixed_DL
    {
      

       public Mall_Setting_Rakhutan_Fixed_DL() { }

       public bool Insert(Mall_Setting_Rakhutan_Fixed_Entity rentity)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand();
           try
           {
               cmd.CommandText = "SP_Mall_Setting_Rakhutan_Fixed_insert";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Connection = connectionString;
              
               //cmd.Parameters.AddWithValue("@tag",rentity.TagID );
               cmd.Parameters.AddWithValue("@tag", rentity.TagID);
               cmd.Parameters.AddWithValue("@contax",rentity.Comsumption_Tax);
               cmd.Parameters.AddWithValue("@ship1",rentity.ShipCat1 );
               cmd.Parameters.AddWithValue("@ship2",rentity.ShipCat2 );
               cmd.Parameters.AddWithValue("@prodinfo",rentity.OrderInfo );
               cmd.Parameters.AddWithValue("@orderbut",rentity.Orderbuttton );
               cmd.Parameters.AddWithValue("@requestbut",rentity.Requestbutton );
               cmd.Parameters.AddWithValue("@prodinbut",rentity.ProductInquerybutton );
                cmd.Parameters.AddWithValue("@comingbut",rentity.Comingsoonbut );
               cmd.Parameters.AddWithValue("@moblile",rentity.Mobile);
               cmd.Parameters.AddWithValue("@expand",rentity.Expandcope );
               cmd.Parameters.AddWithValue("@animation",rentity.Animation );
               cmd.Parameters.AddWithValue("@accno",rentity.AcceptNo );
               cmd.Parameters.AddWithValue("@stocktype",rentity.Stocktype );
               cmd.Parameters.AddWithValue("@stockQuan",rentity.Stockquantity );
               cmd.Parameters.AddWithValue("@stocknodis",rentity.StocknoDisplay );
                 cmd.Parameters.AddWithValue("@hitemname",rentity.Hozitemname );
               cmd.Parameters.AddWithValue("@vitemname",rentity.VarItemname);
               cmd.Parameters.AddWithValue("@remainstock",rentity.Remainstock );
               cmd.Parameters.AddWithValue("@RAC", rentity.RACNO);
               cmd.Parameters.AddWithValue("@catid ",rentity.CatID );
               cmd.Parameters.AddWithValue("@flag",rentity.Flagback );
               cmd.Parameters.AddWithValue("@orderrece",rentity.Orderrecpt );
               cmd.Parameters.AddWithValue("@deliveryctrl",rentity.DelctrNo );
               cmd.Parameters.AddWithValue("@deliveryctrl_outofstock",rentity.DelctrNo_outofstock);
                cmd.Parameters.AddWithValue("@bookorder",System.DateTime.Now.ToString());
               cmd.Parameters.AddWithValue("@headfooter",rentity.Headfooter);
               cmd.Parameters.AddWithValue("@displayorder",rentity.Displayorder );
               cmd.Parameters.AddWithValue("@common1",rentity.Commondesc1 );
               cmd.Parameters.AddWithValue("@common2",rentity.Commondesc2 );
               cmd.Parameters.AddWithValue("@reviewtext",rentity.Reviewtax );
               cmd.Parameters.AddWithValue("@oversea",rentity.Oversea );
               cmd.Parameters.AddWithValue("@chartklink",rentity.Chartlink );
               cmd.Parameters.AddWithValue("@drugdesc",rentity.Drugdesc );
               cmd.Parameters.AddWithValue("@drugnote",rentity.Drugnote );
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

       public bool Update(Mall_Setting_Rakhutan_Fixed_Entity rentity)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand();
           try
           {
               cmd.CommandText = "SP_Mall_Setting_Rakhutan_Fixed_Update";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Connection = connectionString;
               cmd.Parameters.AddWithValue("@ID", rentity.ID);
               cmd.Parameters.AddWithValue("@shopid", rentity.ShopID);
               cmd.Parameters.AddWithValue("@tag", rentity.TagID);
               cmd.Parameters.AddWithValue("@contax", rentity.Comsumption_Tax);
               cmd.Parameters.AddWithValue("@ship1", rentity.ShipCat1);
               cmd.Parameters.AddWithValue("@ship2", rentity.ShipCat2);
               cmd.Parameters.AddWithValue("@prodinfo", rentity.OrderInfo);
               cmd.Parameters.AddWithValue("@orderbut", rentity.Orderbuttton);
               cmd.Parameters.AddWithValue("@requestbut", rentity.Requestbutton);
               cmd.Parameters.AddWithValue("@prodinbut", rentity.ProductInquerybutton);
               cmd.Parameters.AddWithValue("@comingbut", rentity.Comingsoonbut);
               cmd.Parameters.AddWithValue("@moblile", rentity.Mobile);
               cmd.Parameters.AddWithValue("@expand", rentity.Expandcope);
               cmd.Parameters.AddWithValue("@animation", rentity.Animation);
               cmd.Parameters.AddWithValue("@accno", rentity.AcceptNo);
               cmd.Parameters.AddWithValue("@stocktype", rentity.Stocktype);
               cmd.Parameters.AddWithValue("@stockQuan", rentity.Stockquantity);
               cmd.Parameters.AddWithValue("@stocknodis", rentity.StocknoDisplay);
               cmd.Parameters.AddWithValue("@hitemname", rentity.Hozitemname);
               cmd.Parameters.AddWithValue("@vitemname", rentity.VarItemname);
               cmd.Parameters.AddWithValue("@remainstock", rentity.Remainstock);
               cmd.Parameters.AddWithValue("@RAC", rentity.RACNO);
               cmd.Parameters.AddWithValue("@catid ", rentity.CatID);
               cmd.Parameters.AddWithValue("@flag", rentity.Flagback);
               cmd.Parameters.AddWithValue("@orderrece", rentity.Orderrecpt);
               cmd.Parameters.AddWithValue("@deliveryctrl", rentity.DelctrNo);
               cmd.Parameters.AddWithValue("@deliveryctrl_outofstock",rentity.DelctrNo_outofstock);

               cmd.Parameters.AddWithValue("@bookorder", rentity.Orderrelease);
           
               cmd.Parameters.AddWithValue("@headfooter", rentity.Headfooter);
               cmd.Parameters.AddWithValue("@displayorder", rentity.Displayorder);
               cmd.Parameters.AddWithValue("@common1", rentity.Commondesc1);
               cmd.Parameters.AddWithValue("@common2", rentity.Commondesc2);
               cmd.Parameters.AddWithValue("@reviewtext", rentity.Reviewtax);
               cmd.Parameters.AddWithValue("@oversea", rentity.Oversea);
               cmd.Parameters.AddWithValue("@chartklink", rentity.Chartlink);
               cmd.Parameters.AddWithValue("@drugdesc", rentity.Drugdesc);
               cmd.Parameters.AddWithValue("@drugnote", rentity.Drugnote);
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

       public DataTable SelectbyShopID(int shopID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SP_ShopName_SelectAll", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@shopID",shopID);
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

       public Mall_Setting_Rakhutan_Fixed_Entity SelctByID(int ID)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlDataAdapter sda = new SqlDataAdapter("SP_Mall_Setting_Rakhutan_Fixed_SelectbyID", connectionString);
           try
           {
               sda.SelectCommand.CommandType = CommandType.StoredProcedure;
               sda.SelectCommand.CommandTimeout = 0;
               sda.SelectCommand.Parameters.AddWithValue("@ID", ID);

               DataTable dt = new DataTable();

               sda.SelectCommand.Connection.Open();
               sda.Fill(dt);
               sda.SelectCommand.Connection.Close();
               Mall_Setting_Rakhutan_Fixed_Entity rentity = new Mall_Setting_Rakhutan_Fixed_Entity();
               if (dt.Rows.Count > 0)
               {
                   rentity.ID = (int)dt.Rows[0]["ID"];
                   rentity.Shop_Name= dt.Rows[0]["Shop_Name"].ToString();
                   rentity.Mall_Name = dt.Rows[0]["Mall_Name"].ToString();
                   rentity.TagID = dt.Rows[0]["Tag_ID"].ToString();
                   rentity.Comsumption_Tax =(int) dt.Rows[0]["Consumption_Tax"];
                   rentity.ShipCat1 = dt.Rows[0]["Shipping_Category1"].ToString();
                   rentity.ShipCat2 = dt.Rows[0]["Shipping_Category2"].ToString();
                   rentity.OrderInfo = dt.Rows[0]["Product_Information"].ToString();
                   rentity.Orderbuttton = (int)dt.Rows[0]["Order_Button"];
                   rentity.Requestbutton = (int)dt.Rows[0]["Request_Button"];
                   rentity.ProductInquerybutton = (int)dt.Rows[0]["Product_Inquiry_Button"];
                   rentity.Comingsoonbut = (int)dt.Rows[0]["Comingsoon_Button"];
                   rentity.Mobile = (int)dt.Rows[0]["Mobile_Display"];
                   rentity.Expandcope = (int)dt.Rows[0]["Expand_Cope"];
                   rentity.Animation = dt.Rows[0]["Animation"].ToString();
                   rentity.AcceptNo = dt.Rows[0]["Acceptances_No"].ToString();
                   rentity.Stocktype = (int)dt.Rows[0]["Stock_Type"];
                   rentity.StocknoDisplay = dt.Rows[0]["Stock_No_Display"].ToString();
                   rentity.Stockquantity = (int)dt.Rows[0]["Stock_Quantity"];
                   rentity.Hozitemname = dt.Rows[0]["Horizontal_ItemName"].ToString();
                   rentity.VarItemname = dt.Rows[0]["Vertical_ItemName"].ToString();
                   rentity.Remainstock = dt.Rows[0]["Remaining_Stock"].ToString();
                   rentity.RACNO = dt.Rows[0]["RAC_No"].ToString();
                   rentity.CatID = dt.Rows[0]["Catalog_ID"].ToString();
                   rentity.Flagback = (int)dt.Rows[0]["Flagback_Stock"];
                   rentity.Orderrecpt = (int)dt.Rows[0]["Order_Reception"];
                   rentity.DelctrNo = dt.Rows[0]["Delivery_Ctrl_No"].ToString();
                   if (!String.IsNullOrWhiteSpace(dt.Rows[0]["BookOrderRelease_Date"].ToString()))
                   {
                       DateTime date = (DateTime)dt.Rows[0]["BookOrderRelease_Date"];
                       rentity.Orderrelease = date;
                   }
                   else
                       rentity.Orderrelease = null;
                   rentity.Headfooter = dt.Rows[0]["Header_Footer"].ToString();
                   rentity.Displayorder = dt.Rows[0]["Display_Order"].ToString();
                   rentity.Commondesc1 = dt.Rows[0]["Common_Description1"].ToString();
                   rentity.Commondesc2 = dt.Rows[0]["Common_Description2"].ToString();
                   rentity.Reviewtax = (int)dt.Rows[0]["Review_Text"];
                   rentity.Oversea = dt.Rows[0]["Oversea_Ctrl_No"].ToString();
                   rentity.Chartlink =dt.Rows[0]["Size_Chartlink"].ToString();
                   rentity.Drugdesc = dt.Rows[0]["Drug_Description"].ToString();
                   rentity.Drugnote = dt.Rows[0]["Drug_Note"].ToString(); 

               }

               return   rentity;
           }
           catch (Exception ex)
           {

               throw ex;
           }

       }

    }
}
