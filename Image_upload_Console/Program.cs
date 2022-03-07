using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Image_upload_Console
{
    public class Program
    {      
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static String ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        static String Bak_ExportCSVPath = ConfigurationManager.AppSettings["BakExportCSVPath"].ToString();
        static String ItemImage = ConfigurationManager.AppSettings["ItemImage"].ToString();
     
        static void Main(string[] args)
        {
            try
            {
                string list = Select_ExhibitionList_ForImage(1);
                if (!string.IsNullOrWhiteSpace(list))
                {
                    string[] code = list.Split(',');
                    foreach (string itemcode in code)
                    {
                        DataTable dtImage = Select_ExhibitionImage_Upload(1, itemcode);                        
                        UpdateImageZip(dtImage, itemcode);
                        Change_ImageFlag(itemcode, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                //insert to system error log
                Save_SYS_Errorlog(ex.ToString());
            }
        }

        static string Select_ExhibitionList_ForImage(int shopID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_List_Image", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shopID);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt.Rows[0]["Item_ID"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static DataTable Select_ExhibitionImage_Upload(int shop_id, string itemcode)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetImageList_upload", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", itemcode);
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

        static void UpdateImageZip(DataTable dtImage, string itemCode)
        {
            if (dtImage != null && dtImage.Rows.Count > 0)
            {
                string path = "";
                string folderName = "";
                string image_name = "";
                string sku = ExportCSVPath + "1" + "/mem_item/";
                foreach (DataRow drImage in dtImage.Rows)
                {
                    if (drImage["Image_Name"].ToString() != "")
                    {
                        image_name = drImage["Image_Name"].ToString();
                        folderName = drImage["Folder_Name"].ToString();
                        path = sku + folderName + "/";
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        //Save image into folder
                        if (Directory.Exists(path))
                        {
                            if (File.Exists(ItemImage + image_name))
                            {
                                if (!File.Exists(path + image_name))
                                    File.Copy(ItemImage + image_name, path + image_name);
                            }
                        }
                    }
                }
                if (Directory.Exists(sku))
                {
                    using (ZipFile zipfile = new ZipFile())
                    {
                        String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                        zipfile.AddDirectory(sku, "mem_item");
                        zipfile.Save(ExportCSVPath + "1" + "/" + folderName + "$" + "1" + "_" + date + ".zip");
                        zipfile.Save(Bak_ExportCSVPath + folderName + "$" + "1" + "_" + date + ".zip");
                        SaveItem_ExportQ(folderName + "$" + "1" + "_" + date + ".zip", 3, 1, 0, 1);
                    }
                    DeleteDirectory(sku, true);
                }
            }
        }

        public static void SaveItem_ExportQ(string FileName, int FileType, int ShopID, int IsExport, int Export_Type)
        {
            SqlConnection connectionstring = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Item_ExportQ_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionstring;
                cmd.Parameters.AddWithValue("@File_Name", FileName);
                cmd.Parameters.AddWithValue("@File_Type", FileType);
                cmd.Parameters.AddWithValue("@ShopID", ShopID);
                cmd.Parameters.AddWithValue("@IsExport", IsExport);
                cmd.Parameters.AddWithValue("@Export_Type", Export_Type);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteDirectory(string path, bool recursive)
        {
            // Delete all files and sub-folders?
            if (recursive)
            {
                // Yep... Let's do this
                var subfolders = Directory.GetDirectories(path);
                foreach (var s in subfolders)
                {
                    DeleteDirectory(s, recursive);
                }
            }

            // Get all files of the folder
            var files = Directory.GetFiles(path);
            foreach (var f in files)
            {
                // Get the attributes of the file
                var attr = File.GetAttributes(f);

                // Is this file marked as 'read-only'?
                if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // Yes... Remove the 'read-only' attribute, then
                    File.SetAttributes(f, attr ^ FileAttributes.ReadOnly);
                }
                // Delete the file
                File.Delete(f);
            }
            // When we get here, all the files of the folder were
            // already deleted, so we just delete the empty folder   
            Directory.Delete(path);
        }

        static void Change_ImageFlag(string itemcode, int shop_ID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Change_ImageFlag", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@itemcode", itemcode);
                cmd.Parameters.AddWithValue("@Shop_ID", shop_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Save_SYS_Errorlog(string error)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", -1);
            cmd.Parameters.AddWithValue("@ErrorDetail", "APIRakutenPainttool" + error);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
