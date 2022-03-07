/* 
Created By              : Aye Aye Mon
Created Date          : 04/07/2014
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

namespace ORS_RCM_DL
{
    public class Item_Related_Item_DL
    {
        

        public Item_Related_Item_DL()
        { }

        public bool Insert(DataTable dt)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    Item_Master_DL imdl = new Item_Master_DL();
                    int itemID = imdl.SelectItemID(dt.Rows[i]["Related_ItemCode"].ToString());//related item id
                    SqlCommand cmd = new SqlCommand("SP_Item_Related_Item_InsertUpdate", connectionString);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@Item_ID", dt.Rows[i]["Item_ID"]);
                    cmd.Parameters.AddWithValue("@Related_ItemCode", dt.Rows[i]["Related_ItemCode"]);
                    cmd.Parameters.AddWithValue("@Related_ItemID", itemID);
                    cmd.Parameters.AddWithValue("@SN", dt.Rows[i]["SN"]);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool Delete(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("DELETE FROM Item_Related_Item WHERE Item_ID=@Item_ID", connectionString);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", Item_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectRelatedValuebyItemID(int Item_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Related_Item_SelectValue", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_ID", Item_ID);
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

        public DataTable SelectByItemID(int Item_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Related_Item_SelectByItemID", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_ID", Item_ID);
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

        public DataTable SelectRelatedCode(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //string quary = "SELECT Related_ItemCode,SN FROM Item_Related_Item Where Item_ID = (SELECT ID From Item_Master Where Item_Code = '" + Item_Code + "') ORDER BY SN ASC;";
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_RelatedItem_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code",Item_Code);
                //sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", Shop_ID);
                sda.SelectCommand.CommandTimeout = 0;
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
