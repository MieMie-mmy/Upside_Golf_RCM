/* 
Created By              : Aye Aye Mon
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
using System.Data.SqlClient;


namespace ORS_RCM_DL
{
    public class Item_Image_DL
    {

        public Item_Image_DL() { }

        public bool Insert(DataTable dt)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("Item_Image_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Parameters.Add("@Item_ID", SqlDbType.Int, 50, "Item_ID");
                cmd.Parameters.Add("@Image_Name", SqlDbType.NVarChar, 50, "Image_Name");
                cmd.Parameters.Add("@Image_Type", SqlDbType.Int, 50, "Image_Type");
                cmd.Parameters.Add("@SN", SqlDbType.Int, 50, "SN");
                sda.InsertCommand = cmd;
                sda.UpdateCommand = cmd;
                cmd.Connection.Open();
                sda.Update(dt);
                cmd.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public DataTable SelectItemPhotoByItemID(int Item_ID,int type)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string query = "Select * From Item_Image Where Item_ID =" + Item_ID + " AND Image_Type="+type;
                SqlDataAdapter sda = new SqlDataAdapter(query, connectionString);
                sda.SelectCommand.CommandType = CommandType.Text;
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

        public bool DeleteLibraryImage(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("DELETE FROM Item_Image WHERE Item_ID=@Item_ID AND Image_Type=1", connectionString);
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

        public bool Delete(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("DELETE FROM Item_Image WHERE Item_ID=@Item_ID", connectionString);
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

        public bool DeleteImage(int imageID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("DELETE FROM Item_Image WHERE ID=@ID", connectionString);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ID", imageID);
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

        public DataTable SelectByItemID(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string quary = "Select * From Item_Image WITH (NOLOCK) Where Item_ID = @Item_ID ORDER BY Image_Type,SN";
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

        public DataTable GetImageList(int shop_id,string StringItemID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetImageList", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@StringItemID", StringItemID);
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
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

        public DataTable SelectImageList(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                //string quary = "Select * From Item_Image Where Item_ID = @Item_ID";
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Image_SelectImageList", connectionString);
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

        public DataTable SelectAllWithItemID()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string query = "SELECT Item_Image.Item_ID, Item_Image.Image_Name FROM  Item_Image INNER JOIN "
                    +"Item_Master ON Item_Image.Item_ID = Item_Master.ID";
                SqlDataAdapter sda = new SqlDataAdapter(query, connectionString);
                sda.SelectCommand.CommandType = CommandType.Text;
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

        public void DeletebyItemID(String ItemID,int ImageType)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                String query="DELETE FROM Item_Image WHERE Item_ID="+ItemID+" AND Image_Type="+ImageType;
                SqlCommand cmd = new SqlCommand(query, connectionString);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean Update(int itemid,String img)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("Item_Image_Update", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", itemid);
                cmd.Parameters.AddWithValue("@image", img);
                cmd.Parameters.AddWithValue("@image_Type", 1);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();                  
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable SelectImageName(string Item_Code, int ImageType)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                //string quary = "SELECT Image_Name,SN FROM Item_Image WHERE Item_ID = (SELECT ID FROM Item_Master WHERE Item_Code = '" + Item_Code + "') AND Image_Type=" + ImageType + " ORDER BY SN ASC;";
                SqlDataAdapter sda = new SqlDataAdapter("SP_SelectImageName", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@ImageType", ImageType);
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
