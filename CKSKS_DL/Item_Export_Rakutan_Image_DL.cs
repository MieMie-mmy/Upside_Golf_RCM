using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
    public class Item_Export_Rakutan_Image_DL
    {
        

        public void Insert(Item_Export_Rakutan_Image_Entity rakutenImageInfo)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "INSERT INTO Item_Export_Rakutan_Image (Folder_Name,File_Count,Active,Shop_ID) "
                    + "VALUES (@Folder_Name,@File_Count,@Active,@Shop_ID)";
                SqlCommand cmd = new SqlCommand(query, connectionString);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Folder_Name", rakutenImageInfo.Folder_Name);
                cmd.Parameters.AddWithValue("@File_Count", rakutenImageInfo.File_Count);
                cmd.Parameters.AddWithValue("@Active", rakutenImageInfo.Active);
                cmd.Parameters.AddWithValue("@Shop_ID", rakutenImageInfo.Shop_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetFolderList()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string query = "SELECT * FROM Item_Export_Rakutan_Image";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandTimeout = 0;
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

        public void Update(Item_Export_Rakutan_Image_Entity rakutenImageInfo)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "UPDATE Item_Export_Rakutan_Image SET "
                    + "File_Count = " + rakutenImageInfo.File_Count
                    + " ,Active = " + rakutenImageInfo.Active
                    + " ,Shop_ID = " + rakutenImageInfo.Shop_ID
                    + String.Format(" WHERE Folder_Name = '{0}'", rakutenImageInfo.Folder_Name);
                SqlCommand cmd = new SqlCommand(query, connectionString);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
