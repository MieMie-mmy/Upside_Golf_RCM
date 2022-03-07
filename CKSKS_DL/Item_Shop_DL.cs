/* 
Created By              : Aung Kyaw
Created Date          : 19/06/2014
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
    public class Item_Shop_DL
    {
        public bool Insert(DataTable dt)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Shop_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.Parameters.Add("@itemid", SqlDbType.Int, 50, "ItemID");
                cmd.Parameters.Add("@shopid", SqlDbType.Int, 50, "ShopID");
                cmd.Parameters.Add("@itemcode",SqlDbType.NVarChar, 50,"ItemCode");
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                da.InsertCommand = cmd;
                cmd.Connection.Open();
                da.Update(dt);
                cmd.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void Check_ItemShopForAmazon(int ItemID,int flag,int userid)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_CheckInsertAmazonShop", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@itemid", ItemID);
                cmd.Parameters.AddWithValue("@Flag", flag);
                cmd.Parameters.AddWithValue("@UpdatedBy", userid);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteAll(int id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                int result = 1;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Item_Shop_DeleteAll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                result = (int)cmd.Parameters["@result"].Value;
                return result;
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
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string quary = "SELECT * FROM Item_Shop WITH (NOLOCK) WHERE Item_ID = @Item_ID";
                SqlDataAdapter sda = new SqlDataAdapter(quary, connectionString);
                sda.SelectCommand.CommandType = CommandType.Text;
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

        public DataTable CheckItemCodeURL(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string quary = "SP_Check_Item_Code_URL";
                SqlDataAdapter sda = new SqlDataAdapter(quary, connectionString);
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

        public DataTable SelectShopID(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                //string quary = "Select * From Item_Shop Where Item_ID = @Item_ID";
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Shop_SelectShopName", connectionString);
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

        public DataTable SelectCode(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                //string quary = "Select * From Item_Shop Where Item_ID = @Item_ID";
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectByItemCode", connectionString);
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

        public DataTable SelectMallID(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                //string quary = "Select * From Item_Shop Where Item_ID = @Item_ID";
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Shop_SelectMallID", connectionString);
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

        public Boolean ChangeStatus(string Item_Code, string Ctrl_ID, int Shop_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Shop_ChangeStatus", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_Code",Item_Code);
                cmd.Parameters.AddWithValue("@Ctrl_ID",Ctrl_ID);
                cmd.Parameters.AddWithValue("@Shop_ID", Shop_ID);
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

        public Boolean ChangeCtrl_ID(string Item_IDList, int Shop_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Shop_ChangeCtrl_ID", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_IDList", Item_IDList);
                cmd.Parameters.AddWithValue("@Shop_ID", Shop_ID);
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

        public void SaveItemShopExportedCSVInfo(int itemID, int shopID, string csvName)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string sqlQuery = String.Format("UPDATE Item_Shop SET CSV_FileName = '{0}', CSV_CreatedDate = '{1}' WHERE Item_ID = {2} AND Shop_ID = {3}", csvName, DateTime.Now.Date.ToString(),itemID, shopID);
                SqlCommand cmd = new SqlCommand(sqlQuery, connectionString);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int DeleteAllItemCodeURL(int id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                int result = 1;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Item_Code_URL_DeleteAll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                result = (int)cmd.Parameters["@result"].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertAllItemCodeURL(DataTable dt)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Code_URL_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.Parameters.Add("@itemid", SqlDbType.Int, 50, "Item_ID");
                cmd.Parameters.Add("@shopid", SqlDbType.Int, 50, "Shop_ID");
                cmd.Parameters.Add("@itemcode", SqlDbType.NVarChar, 50, "Item_Code_URL");
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                da.InsertCommand = cmd;
                cmd.Connection.Open();
                da.Update(dt);
                cmd.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable SelectItemCodeURL(string Item_Code)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string quary = "SP_Select_ItemCode_URL";
                SqlDataAdapter sda = new SqlDataAdapter(quary, connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
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
