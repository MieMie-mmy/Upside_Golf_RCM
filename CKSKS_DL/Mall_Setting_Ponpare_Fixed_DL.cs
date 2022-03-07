/* 
Created By              : Kay Thi Aung
Created Date          : 27/06/2014
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
    public class Mall_Setting_Ponpare_Fixed_DL
    {
        

        public Mall_Setting_Ponpare_Fixed_DL() { }

        public bool Insert(Mall_Setting_Ponpare_Fixed_Entity pentity)
        {
      
        SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
        SqlCommand cmd = new SqlCommand();
        try
        {
        cmd.CommandText = "SP_Mall_Setting_Ponpare_Fixed_Insert";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;
        cmd.Connection = connectionString;

        cmd.Parameters.AddWithValue("@contax", pentity.Comtax);
        cmd.Parameters.AddWithValue("@ship1", pentity.Shipg1);
        cmd.Parameters.AddWithValue("@ship2", pentity.Shipg2);
        cmd.Parameters.AddWithValue("@expand",  pentity.Expandcope);
        cmd.Parameters.AddWithValue("@orderbut", pentity.Orderbut);
        cmd.Parameters.AddWithValue("@inqbut", pentity.Inqbut);
        cmd.Parameters.AddWithValue("@noaccept", pentity.NoAccept);
        cmd.Parameters.AddWithValue("@stype", pentity.Stocktype);
        cmd.Parameters.AddWithValue("@squantity", pentity.Stockquantity);
        cmd.Parameters.AddWithValue("@sdiplay", pentity.Stockdisplay);
        cmd.Parameters.AddWithValue("@hitem", pentity.Hitemname);
        cmd.Parameters.AddWithValue("@vitem ", pentity.Vitemname);
        cmd.Parameters.AddWithValue("@remaining", pentity.Remaining);
        cmd.Parameters.AddWithValue("@jan ", pentity.Jan);

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

        public bool Update(Mall_Setting_Ponpare_Fixed_Entity pentity)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
        SqlCommand cmd = new SqlCommand();
        try
        {
        cmd.CommandText = "SP_Mall_Setting_Ponpare_Fixed_Insert_Update";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;
        cmd.Connection = connectionString;
        cmd.Parameters.AddWithValue("@ID",pentity.ID);
        cmd.Parameters.AddWithValue("@shopid", pentity.Shop_ID);
        cmd.Parameters.AddWithValue("@contax", pentity.Comtax);
        cmd.Parameters.AddWithValue("@ship1", pentity.Shipg1);
        cmd.Parameters.AddWithValue("@ship2", pentity.Shipg2);
        cmd.Parameters.AddWithValue("@expand", pentity.Expandcope);
        cmd.Parameters.AddWithValue("@orderbut", pentity.Orderbut);
        cmd.Parameters.AddWithValue("@inqbut", pentity.Inqbut);
        cmd.Parameters.AddWithValue("@noaccept", pentity.NoAccept);
        cmd.Parameters.AddWithValue("@stype", pentity.Stocktype);
        cmd.Parameters.AddWithValue("@squantity", pentity.Stockquantity);
        cmd.Parameters.AddWithValue("@sdiplay", pentity.Stockdisplay);
        cmd.Parameters.AddWithValue("@hitem", pentity.Hitemname);
        cmd.Parameters.AddWithValue("@vitem ", pentity.Vitemname);
        cmd.Parameters.AddWithValue("@remaining", pentity.Remaining);
        cmd.Parameters.AddWithValue("@jan ", pentity.Jan);
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

        public Mall_Setting_Ponpare_Fixed_Entity SelctByID(int ID)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_Mall_Setting_Ponpare_Fixed_SelectbyID", connectionString);
        try
        {
        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        sda.SelectCommand.CommandTimeout = 0;
        sda.SelectCommand.Parameters.AddWithValue("@ID", ID);

        DataTable dt = new DataTable();

        sda.SelectCommand.Connection.Open();
        sda.Fill(dt);
        sda.SelectCommand.Connection.Close();
        Mall_Setting_Ponpare_Fixed_Entity pentity = new Mall_Setting_Ponpare_Fixed_Entity();
        if (dt.Rows.Count > 0)
        {
        pentity.ID = (int)dt.Rows[0]["ID"];

        pentity.Comtax= (int)dt.Rows[0]["Consumption_Tax"];
        pentity.Shipg1= dt.Rows[0]["Shipping_Group1"].ToString();
        pentity.Shipg2= dt.Rows[0]["Shipping_Group2"].ToString();
        pentity.Expandcope= (int)dt.Rows[0]["Expand_Cope"];
        pentity.Orderbut = (int)dt.Rows[0]["Order_Button"];
        pentity.Inqbut = (int)dt.Rows[0]["Inquiry_Button"];
        pentity.NoAccept = dt.Rows[0]["NoofAcceptances"].ToString();
        pentity.Stocktype = (int)dt.Rows[0]["Stock_Type"];
        pentity.Stockquantity = dt.Rows[0]["Stock_Quantity"].ToString();
        pentity.Stockdisplay = (int)dt.Rows[0]["Stock_Display"];
        pentity.Hitemname = dt.Rows[0]["Horizontal_ItemName"].ToString();
        pentity.Vitemname = dt.Rows[0]["Vertical_ItemName"].ToString();
        pentity.Remaining = dt.Rows[0]["Remaining_Stock"].ToString();
        pentity.Jan = dt.Rows[0]["JAN_Code"].ToString();

        pentity.ShopName =dt.Rows[0]["Shop_Name"].ToString();
        pentity.MallName = dt.Rows[0]["Code_Description"].ToString();
                

        }

        return pentity;
        }
        catch (Exception ex)
        {

        throw ex;
        }

        }

    }
}
