using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;


namespace ORS_RCM_DL
{
    public class Shop_Template_DL
    {
        public void Insert(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!String.IsNullOrWhiteSpace(dt.Rows[i]["Template_Description"].ToString()))
                {
                    try
                    {
                        SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                        SqlCommand cmd = new SqlCommand("SP_Shop_Template_Insert", connectionString);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@TempID", dt.Rows[i]["Template_ID"].ToString());
                        cmd.Parameters.AddWithValue("@Shop_ID", dt.Rows[i]["Shop_ID"].ToString());
                        cmd.Parameters.AddWithValue("@Template_Description", dt.Rows[i]["Template_Description"].ToString());
                        cmd.Parameters.AddWithValue("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        int id = (int)cmd.Parameters["@result"].Value;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public void  Update(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!String.IsNullOrWhiteSpace(dt.Rows[i]["Template_Description"].ToString()))
                {
                    try
                    {
                        SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                        SqlCommand cmd = new SqlCommand("SP_Shop_Template_Insert", connectionString);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@TempID", (int)dt.Rows[i]["Template_ID"]);
                        cmd.Parameters.AddWithValue("@Shop_ID", (int)dt.Rows[i]["Shop_ID"]);
                        cmd.Parameters.AddWithValue("@Template_Description", dt.Rows[i]["Template_Description"].ToString());
                        cmd.Parameters.AddWithValue("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        int id = (int)cmd.Parameters["@result"].Value;
                        cmd.Connection.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public DataTable GetTemplateDescription(string[] templateID,int shopID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                for (int i = 0; i < templateID.Count(); i++)
                {
                    string query = "SELECT Smart_Template.Template_ID, Shop_Template.Template_Description FROM Shop_Template LEFT OUTER JOIN "
                        + "Smart_Template ON Shop_Template.Template_ID = Smart_Template.ID "
                        + String.Format("WHERE Smart_Template.Template_ID = '{0}'", templateID[i])
                        + " AND Shop_Template.Shop_ID = "+shopID;
                    SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.SelectCommand.CommandTimeout = 0;
                    da.SelectCommand.Connection.Open();
                    da.Fill(dt);
                    da.SelectCommand.Connection.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTemplateZettDescription(string[] templateID, string itemcode, string columnName)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                for (int i = 0; i < templateID.Count(); i++)
                {
                    SqlCommand cmd = new SqlCommand("SP_GetTemplateZettDescription", connectionString);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@templateID", templateID[i]);
                    cmd.Parameters.AddWithValue("@itemcode", itemcode);
                    cmd.Parameters.AddWithValue("@columnName", columnName);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Connection.Open();
                    da.Fill(dt);
                    cmd.Connection.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}








    

