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
    public class Mall_Setting_Ponpare_Default_DL
    {
        

        public Mall_Setting_Ponpare_Default_DL() { }

        public bool Insert( Mall_Setting_Ponpare_Default_Entity pentity)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Mall_Setting_Ponpare_Default_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;

                cmd.Parameters.AddWithValue("@Sale", pentity.Sale_Status);
                cmd.Parameters.AddWithValue("@post ", pentity.Post);
                cmd.Parameters.AddWithValue("@ship", pentity.Ship);
                cmd.Parameters.AddWithValue("@delivery ", pentity.Delivery);
                cmd.Parameters.AddWithValue("@password", pentity.Password);


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

        public bool Update( Mall_Setting_Ponpare_Default_Entity pentity)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Mall_Setting_Ponpare_Default_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@ID", pentity.ID);
                cmd.Parameters.AddWithValue("@shopid", pentity.Shop_ID);
                cmd.Parameters.AddWithValue("@Sale", pentity.Sale_Status);
                cmd.Parameters.AddWithValue("@post ", pentity.Post);
                cmd.Parameters.AddWithValue("@ship", pentity.Ship);
                cmd.Parameters.AddWithValue("@delivery ", pentity.Delivery);
                cmd.Parameters.AddWithValue("@password", pentity.Password);
             
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

        public Mall_Setting_Ponpare_Default_Entity SelctByID(int ID)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_Mall_Setting_Ponpare_Default_SelectbyID", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ID", ID);

                DataTable dt = new DataTable();

                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                Mall_Setting_Ponpare_Default_Entity pentity = new Mall_Setting_Ponpare_Default_Entity();
                if (dt.Rows.Count > 0)
                {
                    pentity.ID = (int)dt.Rows[0]["ID"];

                    pentity.Sale_Status = (int)dt.Rows[0]["Sale_Status"];
                    pentity.Post = (int)dt.Rows[0]["Postage"];
                    pentity.Ship= dt.Rows[0]["Extra_Shipping"].ToString();
                    pentity.Delivery = (int)dt.Rows[0]["Delivery_Charges"];
                    pentity.Password = dt.Rows[0]["SaleSecret_Password"].ToString();

                    pentity.Shopname = dt.Rows[0]["Shop_Name"].ToString();
                    pentity.MallDesc = dt.Rows[0]["Code_Description"].ToString();

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
