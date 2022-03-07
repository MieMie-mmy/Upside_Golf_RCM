using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
   public  class Item_ExportField_DL
    {
       
       public Int32  Insert(Item_ExportField_Entity efty)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand("SP_Item_ExportField_SaveUpdate", connectionString);
           try
           {
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Parameters.AddWithValue("@ID", efty.ID);
               cmd.Parameters.AddWithValue("@Export_Name", efty.Name);
               cmd.Parameters.AddWithValue("@field",efty.Field);
               cmd.Parameters.AddWithValue("@status",efty.Status);
               cmd.Parameters.AddWithValue("@result",SqlDbType.Int).Direction =ParameterDirection.Output;
               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               int id =(int)cmd.Parameters["@result"].Value;
               cmd.Connection.Close();
                return id;
           }
           catch (Exception ex) 
           {
               throw ex;
           }
       }

       public void Update(DataTable dt)
       {
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               try
               {
                   SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                   SqlCommand cmd = new SqlCommand("SP_Item_ExportField_SaveUpdate", connectionString);
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.CommandTimeout = 0;
                   cmd.Parameters.AddWithValue("@ID", dt.Rows[i]["ID"].ToString());
                
                       cmd.Parameters.AddWithValue("@Export_Name", dt.Rows[i]["Export_Name"].ToString());
                
                       cmd.Parameters.AddWithValue("@field", dt.Rows[i]["Export_Fields"].ToString());
                   
                   cmd.Parameters.AddWithValue("@status", dt.Rows[i]["Status"].ToString());
                   cmd.Parameters.AddWithValue("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                   cmd.Connection.Open();
                   cmd.ExecuteNonQuery();
                   cmd.Connection.Close();
                   int id = (int)cmd.Parameters["@result"].Value;
                 //  return id;
               }

               catch (Exception ex)
               {
                   throw ex;
               }
           }
       }

       public DataTable SelectAllData(string list)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlDataAdapter da = new SqlDataAdapter("SP_Item_ExportField_SelectAll", connectionString);
           try 
           {
               DataTable dt = new DataTable();
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.Parameters.AddWithValue("@id",list);
              da.SelectCommand.Connection.Open();
              da.Fill(dt);
              da.SelectCommand.Connection.Close();
              return dt;
               
           }
           catch (Exception ex) 
           { throw ex; }
       
       }

       public DataTable SelectAll( )
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlDataAdapter da = new SqlDataAdapter("Select * from Item_ExportField WITH (NOLOCK) Where Status=1", connectionString);
           try
           {
               DataTable dt = new DataTable();
               da.SelectCommand.CommandType = CommandType.Text;
              //da.SelectCommand.Parameters.AddWithValue("@id", id);
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.Connection.Open();
               da.Fill(dt);
               da.SelectCommand.Connection.Close();
               return dt;

           }
           catch (Exception ex)
           { throw ex; }

       }

       public DataTable SelectAllUser()
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlDataAdapter da = new SqlDataAdapter("Select * from [User] WITH (NOLOCK)", connectionString);
           try
           {
               DataTable dt = new DataTable();
               da.SelectCommand.CommandType = CommandType.Text;
               da.SelectCommand.CommandTimeout = 0;
            
               da.SelectCommand.Connection.Open();
               da.Fill(dt);
               da.SelectCommand.Connection.Close();
               return dt;

           }
           catch (Exception ex)
           { throw ex; }
       }

       public DataTable Exportcsv(string str,string option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               using (SqlConnection con = new SqlConnection(DataConfig.connectionString))
               {
                  con.Open();
                  using (SqlDataAdapter da = new SqlDataAdapter("SP_ItemMaster_ExportData", con))
                  {
                       da.SelectCommand.Parameters.AddWithValue("@list", str);
                       da.SelectCommand.Parameters.AddWithValue("@option", option);
                       da.SelectCommand.CommandType = CommandType.StoredProcedure;
                       da.SelectCommand.CommandTimeout = 0;
                       DataTable dt = new DataTable();
                       da.Fill(dt);
                       return dt;
                  }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }

       public DataTable SelectOption_Cat(string str, int ctrl)
       {
           try
           {
               using (SqlConnection con = new SqlConnection(DataConfig.connectionString))
               {
                   con.Open();
                   using (SqlDataAdapter da = new SqlDataAdapter("SP_Item_View2_OptionCSVExport", con))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@Item_ID", str);
                       da.SelectCommand.Parameters.AddWithValue("@ctrl", ctrl);
                       da.SelectCommand.CommandType = CommandType.StoredProcedure;
                       da.SelectCommand.CommandTimeout = 0;
                       da.SelectCommand.CommandTimeout = 0;
                       DataTable dt = new DataTable();
                       da.Fill(dt);
                       return dt;
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       
       }

       public DataTable SelectRakutenImageID(string str)
       {
           try
           {
               using (SqlConnection con = new SqlConnection(DataConfig.connectionString))
               {
                   con.Open();
                   using (SqlDataAdapter da = new SqlDataAdapter("SP_RakutenImagesettingIDdownload", con))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@Item_ID", str);
                      
                       da.SelectCommand.CommandType = CommandType.StoredProcedure;
                       da.SelectCommand.CommandTimeout = 0;
                       da.SelectCommand.CommandTimeout = 0;
                       DataTable dt = new DataTable();
                       da.Fill(dt);
                       return dt;
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }

       public DataTable SelectRakutenTagIDInfo(string str)
       {
           try
           {
               using (SqlConnection con = new SqlConnection(DataConfig.connectionString))
               {
                   con.Open();
                   using (SqlDataAdapter da = new SqlDataAdapter("SP_SelectRakutenTagIDInfo", con))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@Item_ID", str);
                       da.SelectCommand.CommandType = CommandType.StoredProcedure;
                       da.SelectCommand.CommandTimeout = 0;
                       da.SelectCommand.CommandTimeout = 0;
                       DataTable dt = new DataTable();
                       da.Fill(dt);
                       return dt;
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }

       public DataTable Smartcsv(string str,string cols)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               using (SqlConnection con = new SqlConnection(DataConfig.connectionString))
               {
                   con.Open();
                   //using (SqlDataAdapter da = new SqlDataAdapter("SP_Smart_Template_ExportData", con))
                   using (SqlDataAdapter da = new SqlDataAdapter("SP_TestExcelColumn", con))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@list", str);
                       da.SelectCommand.Parameters.AddWithValue("@Columns", cols);
                       //da.SelectCommand.Parameters.AddWithValue("@Columns","N'"+ cols+"'");
                       da.SelectCommand.CommandType = CommandType.StoredProcedure;
                       da.SelectCommand.CommandTimeout = 0;
                       DataTable dt = new DataTable();
                       da.Fill(dt);
                       return dt;
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       public DataTable XanaxSmartcsv(string str, string cols)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               using (SqlConnection con = new SqlConnection(DataConfig.connectionString))
               {
                   con.Open();
           
                   using (SqlDataAdapter da = new SqlDataAdapter("SP_XanaxData", con))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@list", str);
                       da.SelectCommand.Parameters.AddWithValue("@Columns", cols);
                       //da.SelectCommand.Parameters.AddWithValue("@Columns","N'"+ cols+"'");
                       da.SelectCommand.CommandType = CommandType.StoredProcedure;
                       da.SelectCommand.CommandTimeout = 0;
                       DataTable dt = new DataTable();
                       da.Fill(dt);
                       return dt;
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       public DataTable SmartcsvRelateitemImage(string str)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               using (SqlConnection con = new SqlConnection(DataConfig.connectionString))
               {
                   con.Open();
                   using (SqlDataAdapter da = new SqlDataAdapter("SP_Smart_Template_ExportData", con))
                    {
                       da.SelectCommand.Parameters.AddWithValue("@list", str);
                    
                       da.SelectCommand.CommandType = CommandType.StoredProcedure;
                       da.SelectCommand.CommandTimeout = 0;
                       DataTable dt = new DataTable();
                       da.Fill(dt);
                       return dt;
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       public DataTable ExportcsvShop(string str)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               using (SqlConnection con = new SqlConnection(DataConfig.connectionString))
               {
                   con.Open();
                   
                   using (SqlDataAdapter da = new SqlDataAdapter("SP_ItemExport_Data", con))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@list", str);
                       
                       da.SelectCommand.CommandType = CommandType.StoredProcedure;
                       da.SelectCommand.CommandTimeout = 0;
                       DataTable dt = new DataTable();
                       da.Fill(dt);
                       return dt;
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }

       public void SmartTUpdate(DataTable dt)
       {
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               try
               {
                   SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                   SqlCommand cmd = new SqlCommand("SP_Smart_ExportField_SaveUpdate", connectionString);
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.CommandTimeout = 0;
                   cmd.Parameters.AddWithValue("@ID", dt.Rows[i]["ID"].ToString());

                   cmd.Parameters.AddWithValue("@Export_Name", dt.Rows[i]["Export_Name"].ToString());

                   cmd.Parameters.AddWithValue("@field", dt.Rows[i]["Export_Fields"].ToString());

                   cmd.Parameters.AddWithValue("@status", dt.Rows[i]["Status"].ToString());
                   cmd.Parameters.AddWithValue("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                   cmd.Connection.Open();
                   cmd.ExecuteNonQuery();
                   cmd.Connection.Close();
                   int id = (int)cmd.Parameters["@result"].Value;
                   //  return id;
               }

               catch (Exception ex)
               {
                   throw ex;
               }
           }
       }
       public Int32 SmartInsert(Item_ExportField_Entity efty)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand("SP_Smart_ExportField_SaveUpdate", connectionString);
           try
           {
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Parameters.AddWithValue("@ID", efty.ID);
               cmd.Parameters.AddWithValue("@Export_Name", efty.Name);
               cmd.Parameters.AddWithValue("@field", efty.Field);
               cmd.Parameters.AddWithValue("@status", efty.Status);
               cmd.Parameters.AddWithValue("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               int id = (int)cmd.Parameters["@result"].Value;
               cmd.Connection.Close();
               return id;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public DataTable STSelectAll()
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlDataAdapter da = new SqlDataAdapter("Select * from Item_ExportField_SmartTemplate WITH (NOLOCK) Where Status=1", connectionString);
           try
           {
               DataTable dt = new DataTable();
               da.SelectCommand.CommandType = CommandType.Text;
               //da.SelectCommand.Parameters.AddWithValue("@id", id);
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.Connection.Open();
               da.Fill(dt);
               da.SelectCommand.Connection.Close();
               return dt;

           }
           catch (Exception ex)
           { throw ex; }

       }

       public DataTable SmartSelectAllData(string list)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlDataAdapter da = new SqlDataAdapter("SP_Smart_ExportField_SelectAll", connectionString);
           try
           {
               DataTable dt = new DataTable();
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.Parameters.AddWithValue("@id", list);
               da.SelectCommand.Connection.Open();
               da.Fill(dt);
               da.SelectCommand.Connection.Close();
               return dt;

           }
           catch (Exception ex)
           { throw ex; }

       }

       public DataTable GetManualData()
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlDataAdapter da = new SqlDataAdapter("SP_Get_Manual_Data", connectionString);
           try
           {
               DataTable dt = new DataTable();
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.Connection.Open();
               da.Fill(dt);
               da.SelectCommand.Connection.Close();
               return dt;
           }
           catch (Exception ex)
           { throw ex; }
       }
    }
}
