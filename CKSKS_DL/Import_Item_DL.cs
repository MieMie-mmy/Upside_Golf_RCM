/* 
Created By              : Kay thi Aung
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

namespace ORS_RCM_DL
{
     public class Import_Item_DL
    {
         public Import_Item_DL() { }

        public  void Insert(string xml)
         {
             SqlConnection connection = new SqlConnection(DataConfig.connectionString);
             //SqlCommand cmd = new SqlCommand("SP_Item_SKU_Import_XML", connection);
             SqlCommand cmd = new SqlCommand("SP_Item_SKUUpdate_Import_XML", connection);
             try
             {
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandTimeout = 0;
                 cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                 cmd.Connection.Open();
                 cmd.ExecuteNonQuery();
                
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally 
             {
                 cmd.Connection.Close();
                 cmd.Dispose();
             }
          
         }

        public void MonotaroInsert(string xml)
        {
            SqlConnection connection = new SqlConnection(DataConfig.connectionString);
            //SqlCommand cmd = new SqlCommand("SP_Item_SKU_Import_XML", connection);
            SqlCommand cmd = new SqlCommand("SP_Monotaro_Import_XML", connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }

        }
        public void DeliveryUpdate(string xml)
        {
            SqlConnection connection = new SqlConnection(DataConfig.connectionString);
            //SqlCommand cmd = new SqlCommand("SP_Item_SKU_Import_XML", connection);
            SqlCommand cmd = new SqlCommand("SP_Delivery_Update_XML", connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }

        }
        public  void InvXmlInsert(string xml)
        {
            SqlConnection connection = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand("SP_Inventory_Import_XML", connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        public void Import_RakutenSetting(String xml)
        {
            SqlConnection connection = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand("SP_Import_RakutenImage_Setting_XML", connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        public void ItemMasterXmlInsert(string xml)
        {
            SqlConnection connection = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand("SP_ItemMaster_Import_XML", connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery(); 
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        //written by aam
         public void SportsPlaza_Rakuten_Item_Category_Import_Xml(String xml)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_SportsPlaza_Rakuten_Item_Category_Import_Xml", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                xml = xml.Replace("&#", "$CapitalSports$");
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         public DataTable Check_SportsPlaza_Rakuten_Item_Category(string option)
         {
             try
             {
                 DataTable dt = new DataTable();
                 SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                 SqlDataAdapter sda = new SqlDataAdapter("SP_Check_SportsPlaza_Rakuten_Item_Category", connection);
                 sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                 sda.SelectCommand.CommandTimeout = 0;
                 sda.SelectCommand.Parameters.AddWithValue("@Option",option);
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

         public void Delete_SportsPlaza_Rakuten_Item_Category()
         {
             try
             {
                 SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                 SqlCommand cmd = new SqlCommand("Truncate Table SportsPlaza_Rakuten_Item_Category", connection);
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

         public void UpdateQuantity(string xml)
         {
             SqlConnection connection = new SqlConnection(DataConfig.connectionString);
             SqlCommand cmd = new SqlCommand("SP_Adjust_Quantity_XML", connection);
             try
             {
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandTimeout = 0;
                 cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                 cmd.Connection.Open();
                 cmd.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Connection.Close();
                 cmd.Dispose();
             }
         }

         public DataTable ItemInsertTagID(string Item_Code, string Size_Name, string Color_Name, string Rakuten_CategoryID, string Tag_Name1, string Tag_Name2, string Tag_Name3, string Tag_Name4, string Tag_Name5, string Tag_Name6, string Tag_Name7, string Tag_Name8)
         {
             try
             {
                 SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                 SqlCommand cmd = new SqlCommand("SP_Import_RakutenTagID", connection);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandTimeout = 0;

                 if (!String.IsNullOrWhiteSpace(Item_Code))
                 {
                     cmd.Parameters.Add("@Item_Code", Item_Code);
                 }
                 else
                 { cmd.Parameters.Add("@Item_Code", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Size_Name))
                 {
                     cmd.Parameters.Add("@Size_Name", Size_Name);
                 }
                 else
                 { cmd.Parameters.Add("@Size_Name", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Color_Name))
                 {
                     cmd.Parameters.Add("@Color_Name", Color_Name);
                 }
                 else
                 { cmd.Parameters.Add("@Color_Name", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Rakuten_CategoryID))
                 {
                     cmd.Parameters.Add("@Rakuten_CategoryID", Rakuten_CategoryID);
                 }
                 else
                 { cmd.Parameters.Add("@Rakuten_CategoryID", DBNull.Value); }
                
                
                 if(!String.IsNullOrWhiteSpace(Tag_Name1))
                 {
                    cmd.Parameters.Add("@Tag_Name1", Tag_Name1);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name1", DBNull.Value);}

                 if (!String.IsNullOrWhiteSpace(Tag_Name2))
                 {
                     cmd.Parameters.Add("@Tag_Name2", Tag_Name2);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name2", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name3))
                 {
                     cmd.Parameters.Add("@Tag_Name3", Tag_Name3);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name3", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name4))
                 {
                     cmd.Parameters.Add("@Tag_Name4", Tag_Name4);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name4", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name5))
                 {
                     cmd.Parameters.Add("@Tag_Name5", Tag_Name5);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name5", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name6))
                 {
                     cmd.Parameters.Add("@Tag_Name6", Tag_Name6);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name6", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name7))
                 {
                     cmd.Parameters.Add("@Tag_Name7", Tag_Name7);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name7", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name8))
                 {
                     cmd.Parameters.Add("@Tag_Name8", Tag_Name8);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name8", DBNull.Value); }

                 DataTable dt = new DataTable();
                 SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                 sqlData.Fill(dt);
                 return dt;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         public DataTable ItemCode_Check(String ItemCode)
         {
             try
             {
                 SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                 SqlCommand cmd = new SqlCommand("select Item_Code from Item_Master where Item_Code=@ItemCode", connection);
                 cmd.CommandType = CommandType.Text;
                 cmd.CommandTimeout = 0;

                 if (!String.IsNullOrWhiteSpace(ItemCode))
                 {
                     cmd.Parameters.Add("@ItemCode", ItemCode);
                 }
                 else
                 {
                     cmd.Parameters.Add("@ItemCode", DBNull.Value);
                 }
                 DataTable dt = new DataTable();
                 SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                 sqlData.Fill(dt);
                 return dt;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         public DataSet ErrorCountTagID(String Rakuten_CategoryID, string Tag_Name1, string Tag_Name2, string Tag_Name3, string Tag_Name4, string Tag_Name5, string Tag_Name6, string Tag_Name7, string Tag_Name8)
         {
             try
             {
                 SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                 SqlCommand cmd = new SqlCommand("SP_TagNameCount_Check", connection);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandTimeout = 0;
                 cmd.Parameters.Add("@Rakuten_CategoryID", Rakuten_CategoryID);

                 if (!String.IsNullOrWhiteSpace(Tag_Name1))
                 {
                     cmd.Parameters.Add("@Tag_Name1", Tag_Name1);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name1", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name2))
                 {
                     cmd.Parameters.Add("@Tag_Name2", Tag_Name2);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name2", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name3))
                 {
                     cmd.Parameters.Add("@Tag_Name3", Tag_Name3);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name3", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name4))
                 {
                     cmd.Parameters.Add("@Tag_Name4", Tag_Name4);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name4", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name5))
                 {
                     cmd.Parameters.Add("@Tag_Name5", Tag_Name5);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name5", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name6))
                 {
                     cmd.Parameters.Add("@Tag_Name6", Tag_Name6);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name6", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name7))
                 {
                     cmd.Parameters.Add("@Tag_Name7", Tag_Name7);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name7", DBNull.Value); }

                 if (!String.IsNullOrWhiteSpace(Tag_Name8))
                 {
                     cmd.Parameters.Add("@Tag_Name8", Tag_Name8);
                 }
                 else
                 { cmd.Parameters.Add("@Tag_Name8", DBNull.Value); }

                 DataTable dt = new DataTable();
                 SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                 DataSet ds = new DataSet();
                 sqlData.Fill(ds);
                 return ds;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public DataTable ItemAdminCode_Select()
         {
             try
             {
                 SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                 DataTable dt = new DataTable();
                 SqlDataAdapter sda = new SqlDataAdapter("SP_Select_ItemAdminCode", connectionString);
                 sda.SelectCommand.CommandTimeout = 0;
                 sda.SelectCommand.CommandType = CommandType.StoredProcedure;
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