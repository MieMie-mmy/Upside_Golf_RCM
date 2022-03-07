// -----------------------------------------------------------------------
// <copyright file="ErrorLog_DL.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

/* 
Created By              : Kyaw Thet Paing
Created Date          : 04/07/2014
Updated By             :
Updated Date         :

 Tables using: 
    -
    -
    -
*/

namespace ORS_RCM_DL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.OleDb;

    public class ErrorLog_DL
    {
       

        /// <summary>
        /// Insert Error data to ErrorLog table for Item
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="errLog"></param>
        /// <returns></returns>
        //public Boolean InsertErrorLog(DataTable dt,int errLog)
        //{
        //    try
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            SqlConnection con = DataConfig.GetConnectionString();
        //            SqlCommand cmd = new SqlCommand("Item_Import_ErrorLog_Insert", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Item_Code", dt.Rows[i]["商品番号"].ToString());
        //            cmd.Parameters.AddWithValue("@Item_Name", dt.Rows[i]["項目名"].ToString());
        //            cmd.Parameters.AddWithValue("@Type", dt.Rows[i]["Type"].ToString());
        //            cmd.Parameters.AddWithValue("@Error_Message", dt.Rows[i]["エラー内容"].ToString());
        //            cmd.Parameters.AddWithValue("@Item_Import_LogID", errLog);
        //            cmd.Connection.Open();
        //            cmd.ExecuteNonQuery();
        //            cmd.Connection.Close();
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// Insert Error data to ErrorLog table for Category
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="errLog"></param>
        /// <returns></returns>
        public Boolean InsertErrorLog_Category(DataTable dt, int errLog)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    SqlCommand cmd = new SqlCommand("SP_Item_Import_ErrorLog_Insert", connectionString);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@itemcode", dt.Rows[i]["商品番号"].ToString());
                    cmd.Parameters.AddWithValue("@errortype", dt.Rows[i]["Type"].ToString());
                    cmd.Parameters.AddWithValue("@errmsg", dt.Rows[i]["エラー内容"].ToString());
                    cmd.Parameters.AddWithValue("@logID", errLog);
                    if (dt.Columns.Contains("項目名"))
                        cmd.Parameters.AddWithValue("@itemname", dt.Rows[i]["項目名"].ToString());
                    else
                        cmd.Parameters.AddWithValue("@itemname", null);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean InsertErrorLog_CategoryXML(DataTable dt)
        {
            try
            {
                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);

                SqlCommand cmd = new SqlCommand("SP_Item_Import_ErrorLog_Insert_XML", connectionString);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
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
        public Boolean InsertErrorLog_ProductXML(DataTable dt)
        {
            try
            {
                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);

                SqlCommand cmd = new SqlCommand("SP_Item_Import_ErrorLog_XML", connectionString);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
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
    }
}