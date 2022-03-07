/* 
Created By              : EiPhyo
Created Date          : 4/08/2014
Updated By             :
Updated Date         :

 Tables using: Smart_Template
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
    public class Smart_Template_DL
    {
        

        public Smart_Template_DL() { }

        public void Insert_SmartTemplate(String xml)
        {
            xml = xml.Replace("&#", "$CapitalSports$");
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Smart_Template_Detail_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;

                cmd.Parameters.AddWithValue("@xml",xml);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetShopList()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("Shop_Template_Detail", connectionString);
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
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

        public DataTable GetTextboxList()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("Select * From Smart_Template WITH (NOLOCK) ORDER BY ID DESC", connectionString);

                da.SelectCommand.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
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

        public DataTable SelectbyID(int ID) 
        {
            //Smart_Template_Entity sentity = new Smart_Template_Entity();
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_SmartTemplate_SelectbyID", connectionString);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@ID",ID);
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                //if (dt != null && dt.Rows.Count > 0) 
                //{
                //    sentity.Template_ID = dt.Rows[0]["Template_ID"].ToString();
                //    sentity.Status = (int)dt.Rows[0]["Status"];
                //}

                //return sentity;
                return dt;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        
        }

        public DataTable SelectByTemplateID(string templateID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SELECT * FROM Smart_Template WHERE Template_ID = " + templateID;
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                DataTable dt = new DataTable();
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

        public  int  Insert(Smart_Template_Entity  smtEnt)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Smart_Template_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;

                cmd.Parameters.AddWithValue("@ID", smtEnt.ID);

                cmd.Parameters.AddWithValue("@Status", smtEnt.Status);
                cmd.Parameters.AddWithValue("@Template_ID", smtEnt.Template_ID);
                cmd.Parameters.AddWithValue("@Templatename", smtEnt.Templatename);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
                        

                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                return id;
                //if (id > 0)
                //{
                //    return true;
                //}

                //return true;
               
              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool Delete(int ID) 
        {
            try 
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("Delete FROM Shop_Template WHERE Template_ID =" + ID, connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Connection.Open();
                da.SelectCommand.ExecuteNonQuery();
                da.SelectCommand.Connection.Close();
                return true;
            }
            catch (Exception) { return false; }
        
        }

    }
}
