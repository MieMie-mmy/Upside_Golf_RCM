/* 
Created By              : Eephyo
Created Date          : 01/07/2014
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
    public class Details_Ponpare_Exhibition_DL
    {
        //public Details_Ponpare_Exhibation_Entity SelctByID(int ID)
        public DataTable SelctByID(int ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_DetailsofPonpare_Exhibition", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", ID);

                DataTable dt = new DataTable();

                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                #region
                //Details_Ponpare_Exhibation_Entity detailPon = new Details_Ponpare_Exhibation_Entity();
                //if (dt.Rows.Count > 0)
                //{


                //    detailPon.Shop_ID = Convert.ToInt32(dt.Rows[0]["Shop_ID"].ToString());

                //    detailPon.Shop_Name = dt.Rows[0]["Shop_Name"].ToString();

                //    detailPon.Sale_Status = Convert.ToInt32(dt.Rows[0]["Sale_Status"].ToString());

                //    detailPon.Postage = Convert.ToInt32(dt.Rows[0]["Postage"].ToString());

                //    detailPon.Extra_Shipping = (dt.Rows[0]["Extra_Shipping"].ToString());

                //    detailPon.Delivery_Charges = int.Parse(dt.Rows[0]["Delivery_Charges"].ToString());

                //    detailPon.SaleSecret_Password = (dt.Rows[0]["SaleSecret_Password"].ToString());

                //    detailPon.Consumption_Tax = Convert.ToInt32(dt.Rows[0]["Consumption_Tax"].ToString());

                //    detailPon.Shipping_Group1 = (dt.Rows[0]["Shipping_Group1"].ToString());

                //    detailPon.Shipping_Group2 = (dt.Rows[0]["Shipping_Group2"].ToString());

                //    detailPon.Expand_Cope = (dt.Rows[0]["Expand_Cope"].ToString());

                //    detailPon.Order_Button = (dt.Rows[0]["Order_Button"].ToString());

                //    detailPon.Inquiry_Button = (dt.Rows[0]["Inquiry_Button"].ToString());

                //    detailPon.NoofAcceptances = (dt.Rows[0]["NoofAcceptances"].ToString());

                //    detailPon.Stock_Type = (dt.Rows[0]["Stock_Type"].ToString());

                //    detailPon.Stock_Quantity = (dt.Rows[0]["Stock_Quantity"].ToString());

                //    detailPon.Stock_Display = (dt.Rows[0]["Stock_Display"].ToString());

                //    detailPon.Horizontal_ItemName = (dt.Rows[0]["Horizontal_ItemName"].ToString());

                //    detailPon.Vertical_ItemName = (dt.Rows[0]["Vertical_ItemName"].ToString());

                //    detailPon.Remaining_Stock = (dt.Rows[0]["Remaining_Stock"].ToString());

                //    detailPon.JAN_Code = (dt.Rows[0]["JAN_Code"].ToString());




                //}
                //return detailPon;
                #endregion
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataTable SelectbyItemID(int itemID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SP_Ponpare_SelectbyItemID", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@Item_ID", itemID);
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


        public DataTable SelectbyImage(int itemID, int shopid)
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
                //SqlDataAdapter sda = new SqlDataAdapter("SP_ExhibitionDetailForPonpare", connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectForPonpare", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_ID);
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID",Convert.ToInt32(str));
                //sda.SelectCommand.Parameters.AddWithValue("@strString", str);
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
 