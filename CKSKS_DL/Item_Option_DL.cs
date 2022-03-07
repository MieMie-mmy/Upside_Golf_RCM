// -----------------------------------------------------------------------
// <copyright file="Item_Option_DL.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

/* 
Created By              : Kyaw Thet Paing
Created Date          : 02/07/2014
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

    /// <summary>
    /// This class intend to join with sql
    /// </summary>
    public class Item_Option_DL
    {

        public Item_Option_DL() { }

        public Boolean Check_ItemCode(DataTable dt)
        {
            try
            {
                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DeleteTemp();
                SqlCommand cmd = new SqlCommand("SP_CheckItem", connectionString);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = 0;
                //SqlDataAdapter da = new SqlDataAdapter();
                //cmd.Parameters.Add("@ItemCode", SqlDbType.NVarChar, 50, "商品番号");
                //da.InsertCommand = cmd;
                //cmd.Connection.Open();
                //da.Update(dt);
                cmd.Connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean Insert_Update(int id,String v1,String n1,String v2,String n2,String v3,String n3)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Option_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID",id);
                cmd.Parameters.AddWithValue("@Option_Value1",v1);
                cmd.Parameters.AddWithValue("@Option_Name1",n1);
                cmd.Parameters.AddWithValue("@Option_Value2",v2);
                cmd.Parameters.AddWithValue("@Option_Name2",n2);
                cmd.Parameters.AddWithValue("@Option_Value3",v3);
                cmd.Parameters.AddWithValue("@Option_Name3",n3);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool Insert(DataTable dt)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Option_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Parameters.Add("@Item_ID", SqlDbType.Int, 50, "Item_ID");
                cmd.Parameters.Add("@Option_Value1", SqlDbType.NVarChar, 4000, "Value1");
                cmd.Parameters.Add("@Option_Name1", SqlDbType.NVarChar, 200, "Name1");
                cmd.Parameters.Add("@Option_Value2", SqlDbType.NVarChar, 4000, "Value2");
                cmd.Parameters.Add("@Option_Name2", SqlDbType.NVarChar, 200, "Name2");
                cmd.Parameters.Add("@Option_Value3", SqlDbType.NVarChar, 4000, "Value3");
                cmd.Parameters.Add("@Option_Name3", SqlDbType.NVarChar, 200, "Name3");
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

        public void DeleteTemp()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("TRUNCATE TABLE Import_temp", connectionString);
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

        public DataTable GetErrorTable()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Import_temp", connectionString);
                cmd.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
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

        public bool Delete(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //SqlCommand cmd = new SqlCommand("DELETE FROM Item_Option WHERE Item_ID=@Item_ID", DataConfig.GetConnectionString());
                SqlCommand cmd = new SqlCommand("UPDATE Item_Option SET Ctrl_ID='d' WHERE Item_ID=@Item_ID", connectionString);
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

        public bool DeleteByID(int ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("DELETE FROM Item_Option WHERE ID=@ID", connectionString);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ID", ID);
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
                string quary = "SELECT * FROM Item_Option WITH (NOLOCK) WHERE Item_ID = @Item_ID AND Ctrl_ID='n'";
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

        public Boolean InsertCSV(String xml)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 0;
                cmd.CommandText = "SP_Item_Option_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
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

        public void InsertItemImportItemLog(DataTable dt, int errLog)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    SqlCommand cmd = new SqlCommand("SP_ItemOption_Item_Import_ItemLog_Insert", connectionString);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@Item_Code", dt.Rows[i]["商品番号"].ToString());
                    cmd.Parameters.AddWithValue("@Item_Name", dt.Rows[i]["項目名"].ToString());
                    cmd.Parameters.AddWithValue("@Item_Import_LogID", errLog);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                  
    }
}
