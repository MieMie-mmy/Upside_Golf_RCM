/* 
Created By              : Eephyo
Created Date          : 30/06/2014
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
    public class Details_Rakuten_Exhibation_DL
    {
        public Details_Rakuten_Exhibation_Entity SelctByID(int ID)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter sda = new SqlDataAdapter("SP_DetailsofRakuten_Exhibition", connectionString);
               sda.SelectCommand.CommandType = CommandType.StoredProcedure;
               sda.SelectCommand.CommandTimeout = 0;
               sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", ID);

               DataTable dt = new DataTable();

               sda.SelectCommand.Connection.Open();
               sda.Fill(dt);
               sda.SelectCommand.Connection.Close();
               Details_Rakuten_Exhibation_Entity detailR = new Details_Rakuten_Exhibation_Entity();
               if (dt.Rows.Count > 0)
               {
                   detailR.Shop_ID = dt.Rows[0]["Shop_ID"].ToString();
                   detailR.Shop_Name = dt.Rows[0]["Shop_Name"].ToString();
                   detailR.Tag_ID= dt.Rows[0]["Tag_ID"].ToString();
                   if (detailR.Consumption_Tax != null)
                   {
                       detailR.Consumption_Tax = Convert.ToInt32((dt.Rows[0]["Consumption_Tax"].ToString()));
                   }
                   detailR.Searchhide = Convert.ToInt32((dt.Rows[0]["Search_Hide"].ToString()));//fix
                   detailR.DualPrice = Convert.ToInt32((dt.Rows[0]["DualPricing_Ctrl_No"].ToString()));//fix
                   detailR.ShipCat1 = ((dt.Rows[0]["Shipping_Category1"].ToString()));
                   detailR.ShipCat2 = ((dt.Rows[0]["Shipping_Category2"].ToString()));
                   detailR.Product_Information = ((dt.Rows[0]["Product_Information"].ToString()));
                   detailR.Orderbuttton = Convert.ToInt32((dt.Rows[0]["Order_Button"].ToString()));
                  detailR.Requestbutton = Convert.ToInt32((dt.Rows[0]["Request_Button"].ToString()));
                   detailR.ProductInquerybutton = Convert.ToInt32((dt.Rows[0]["Product_Inquiry_Button"].ToString()));
                   detailR.Comingsoonbut = Convert.ToInt32((dt.Rows[0]["Comingsoon_Button"].ToString()));
                   detailR.Mobile = Convert.ToInt32((dt.Rows[0]["Mobile_Display"].ToString()));
                   detailR.Expandcope = Convert.ToInt32((dt.Rows[0]["Expand_Cope"].ToString()));
                   detailR.Animation = ((dt.Rows[0]["Animation"].ToString()));
                   detailR.AcceptNo = ((dt.Rows[0]["Acceptances_No"].ToString()));
                   detailR.Stocktype = Convert.ToInt32((dt.Rows[0]["Stock_Type"].ToString()));
                   detailR.Stockquantity = Convert.ToInt32((dt.Rows[0]["Stock_Quantity"].ToString()));
                   detailR.StocknoDisplay = ((dt.Rows[0]["Stock_No_Display"].ToString()));
                   detailR.Hozitemname = ((dt.Rows[0]["Horizontal_ItemName"].ToString()));
                   detailR.VarItemname = ((dt.Rows[0]["Vertical_ItemName"].ToString()));
                   detailR.Remainstock = ((dt.Rows[0]["Remaining_Stock"].ToString()));
                   detailR.RACNO = ((dt.Rows[0]["RAC_No"].ToString()));
                   detailR.CatID = ((dt.Rows[0]["Catalog_ID"].ToString()));
                   detailR.Flagback = Convert.ToInt32((dt.Rows[0]["Flagback_Stock"].ToString()));
                   detailR.Orderrecpt = Convert.ToInt32((dt.Rows[0]["Order_Reception"].ToString()));
                   detailR.DelctrNo = ((dt.Rows[0]["Delivery_Ctrl_No"].ToString()));
                   if (!String.IsNullOrWhiteSpace(dt.Rows[0]["BookOrderRelease_Date"].ToString()))
                   detailR.Orderrelease =Convert.ToDateTime(dt.Rows[0]["BookOrderRelease_Date"]);
                   detailR.Headfooter = ((dt.Rows[0]["Header_Footer"].ToString()));
                   detailR.Displayorder = ((dt.Rows[0]["Display_Order"].ToString()));
                   detailR.Commondesc1 = ((dt.Rows[0]["Common_Description1"].ToString()));
                   detailR.Commondesc2 = ((dt.Rows[0]["Common_Description2"].ToString()));
                   detailR.Reviewtax = Convert.ToInt32((dt.Rows[0]["Review_Text"].ToString()));
                   detailR.Oversea=((dt.Rows[0]["Oversea_Ctrl_No"].ToString()));
                   detailR.Size_Chartlink = ((dt.Rows[0]["Size_Chartlink"].ToString()));
                   detailR.Drug_Description = ((dt.Rows[0]["Drug_Description"].ToString()));
                    detailR.Drug_Note = ((dt.Rows[0]["Drug_Note"].ToString()));
                    detailR.StockDate = dt.Rows[0]["Delivery_CtrlNo_Outof_Stock"].ToString();
               }
               return   detailR;
           }
           catch (Exception ex)
           {
               throw ex;
           }
        }

        public DataTable SelectbyItemID(int ItemID) 
        {
            try 
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SP_Details_Rakuten_Exhibition_SelectItemID", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@Item_ID",ItemID);
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

        public DataTable SelectbyImage(int itemID,int shopid)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SP_Rakuten_Exhibition_SelectImage", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@itemid", itemID);
                da.SelectCommand.Parameters.AddWithValue("@shopid", shopid);
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

        public DataTable SelectByExhibitionData(int shop_ID, string str, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                //SqlDataAdapter sda = new SqlDataAdapter("SP_ExhibitionDetailForRakuten", connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectForRakuten", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_ID);
                //sda.SelectCommand.Parameters.AddWithValue("@strString", str);
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", str);
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
