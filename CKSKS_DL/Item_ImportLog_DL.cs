/* 
Created By              : Kay Thi Aung
Created Date          : 04/07/2014
Updated By             :
Updated Date         :

 Tables using: Item_ImportLog,Item_Import_ErrorLog
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
  public  class Item_ImportLog_DL
    {
      

      public Item_ImportLog_DL() { }

      public int ImportLogInsert(int importtype, int reccount, int errCount, int userid)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Item_ImportLog_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@importtype", importtype);//Item_Master or sku or inventory
                cmd.Parameters.AddWithValue("@ErrCount", errCount);//how many error occur in imported file
                cmd.Parameters.AddWithValue("@reccount", reccount);//total records in imported file
                cmd.Parameters.AddWithValue("@importby", userid);//imported user
                cmd.Parameters.AddWithValue("@importeddate", Convert.ToDateTime(System.DateTime.Now.ToString()));//date
                cmd.Parameters.AddWithValue("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                return id;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void ItemImportLogInsert(DataTable dt) 
        {
            try
            {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                         SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "SP_Item_Import_ErrorLog_Insert";
                         cmd.CommandType = CommandType.StoredProcedure;
                         cmd.CommandTimeout = 0;
                         cmd.Connection = connectionString;
                        cmd.Parameters.AddWithValue("@logID", dt.Rows[i]["Item_Import_LogID"].ToString());
                        cmd.Parameters.AddWithValue("@itemcode", dt.Rows[i]["Item_Code"].ToString());
                        cmd.Parameters.AddWithValue("@itemname", dt.Rows[i]["Item_Name"].ToString());
                        cmd.Parameters.AddWithValue("@errortype", dt.Rows[i]["Type"].ToString());
                        cmd.Parameters.AddWithValue("@errmsg", dt.Rows[i]["ErrMsg"].ToString());
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                  
                     }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ErrorLogXmlInsert(string xml)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Import_ErrorLog_Insert_XML";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connectionString;
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

        }

        public void Item_Import_ItemLog(string xml) 
        {
            try 
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Item_Import_ItemLog_Insert_XML";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml",SqlDbType.Xml).Value=xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        
        }

        public void Monotaro_Item_Import_ItemLogXML(string xml)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Monotaro_Import_ItemLog_Insert_XML";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
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

        public void Delivery_Update_XML(string xml)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Delivery_XML";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
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
        public void SmartTemplate_ErrorLog_Insert(String xml)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_SmartTemplate_Error_Log";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
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
        
        public void SmartItem_Import_ItemLog(string xml)
        {
            try
            {
                xml = xml.Replace("&#", "$CapitalSports$");
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_SmartTemplate_Import_Log";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
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

        public DataTable ImportLogSelectAll()
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_Item_ImportLog_SelectAll", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
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

        public DataTable ItemmasterLogSelectAll(int ctrl,int Id)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_ItemMaster_Errorlog_SelectAll", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ctrl",ctrl);
                sda.SelectCommand.Parameters.AddWithValue("@LogID", Id);
                DataTable dt = new DataTable();
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

        public DataTable ErrorLogSelectAll(int ctrl, int Id)//For ErorLog
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_Errorlog_SelectAll", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ctrl", ctrl);
                sda.SelectCommand.Parameters.AddWithValue("@LogID", Id);
                DataTable dt = new DataTable();
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
        //Get Item Option Log by log id
        //Updated by kyaw thet paing
        public DataTable GetOptionLog(string id)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_ItemOptionImportLog_SELECT", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;   
                sda.SelectCommand.Parameters.AddWithValue("@LogID", id);
                DataTable dt = new DataTable();
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

        public DataTable GetOptionErrorLog(string id)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_ItemOptionErrorLog_SELECT", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@LogID", id);
                DataTable dt = new DataTable();
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
        //Get Item Category Log by log id
        //Updated by kyaw thet paing
        public DataTable GetCategoryLog(string id)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_ItemCategoryImportLog_SELECT", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;   
                sda.SelectCommand.Parameters.AddWithValue("@LogID", id);
                DataTable dt = new DataTable();
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

        public DataTable GetCategoryErrorLog(string id)//for errorlog
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_ItemCategoryErrorLog_SELECT", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@LogID", id);

                DataTable dt = new DataTable();

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

        public DataTable BindTagID_Error()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_TagID_Error_Select",connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = 0;
                //cmd.Parameters.AddWithValue("@xml",SqlDbType.Xml).Value=xml;
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

        public void ChangeFlag()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_TagID_ChangeFlag", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
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

      //Get System error log
        public DataTable SystemLogSelectAll(int pageindex, string userid, string status, string detail)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_System_Log_SelectAll", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@PageIndex", pageindex);
                sda.SelectCommand.Parameters.AddWithValue("@PageSize",30);
                if(!string.IsNullOrWhiteSpace(userid))
                    sda.SelectCommand.Parameters.AddWithValue("@UserID", userid);
                else sda.SelectCommand.Parameters.AddWithValue("@UserID", -1);
                sda.SelectCommand.Parameters.AddWithValue("@Status", status);
                if(!string.IsNullOrWhiteSpace(detail))
                    sda.SelectCommand.Parameters.AddWithValue("@Detail", detail);
                else sda.SelectCommand.Parameters.AddWithValue("@Detail", DBNull.Value);
                DataTable dt = new DataTable();
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
        public DataTable GetProductLog(string id)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_ProductImportLog_SELECT", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@LogID", id);
                DataTable dt = new DataTable();
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

      //For Exhibition_Flag_Change.aspx
        public void Update_Exhibited_ErrorItemCodeValue(string itemcode , int shop)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Update_Exhibited_ErrorItemCodeValue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@itemcode", itemcode);
                cmd.Parameters.AddWithValue("@shop", shop);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch
            {
                throw;
            }
        }
    }
}
