// -----------------------------------------------------------------------
// <copyright file="ShopCategoryDL.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

/* 
Created By              : Kyaw Thet Paing
Created Date          : 01/07/2014
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
    using System.Data.SqlClient;
    using System.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ShopCategoryDL
    {
       

        public Boolean InsertCSV1(DataTable dt)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Shop_Category_Update", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.Parameters.Add("@Category_ID", SqlDbType.NVarChar, 50, "カテゴリID");
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 50, "パス名");
                cmd.Parameters.Add("@Parent_ID", SqlDbType.NVarChar, 50, "親カテゴリID");
                cmd.Parameters.Add("@Deschierarchy", SqlDbType.NVarChar,200, "商品カテゴリ名");
               
               da.InsertCommand = cmd;
                cmd.Connection.Open();
                da.Update(dt);
                cmd.Connection.Close();
                return true;
            }
            catch (Exception )
            {
               
                return false;
            }
        }
        public Boolean UpdateCSV(DataTable dt)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Shop_Category_Update_Description", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.Parameters.Add("@Category_ID", SqlDbType.NVarChar, 50, "カテゴリID");
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 50, "パス名");
               // cmd.Parameters.Add("@Parent_ID", SqlDbType.NVarChar, 50, "親カテゴリID");
                cmd.Parameters.Add("@Deschierarchy", SqlDbType.NVarChar, 200, "商品カテゴリ名");

                da.UpdateCommand = cmd;
                cmd.Connection.Open();
                da.Update(dt);
                cmd.Connection.Close();
                return true;
            }
            catch (Exception )
            {

                return false;
            }
        }
        public Boolean InsertCSV(DataTable dt)
        {          
            try
            {
                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);

                SqlCommand cmd = new SqlCommand("SP_Category_InsertXML", connectionString);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
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
    }
}
