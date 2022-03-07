/* 
Created By              : Aye Aye Mon
Created Date          : 20/06/2014
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
    public class Item_Category_DL
    {
        

        public Item_Category_DL() { }

        public Boolean InsertUpdate(int ItemID,int CatID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Category_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID",ItemID);
                cmd.Parameters.AddWithValue("@Category_ID",CatID);
                cmd.Parameters.AddWithValue("@Ctrl_ID",'n');

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

        /// <summary>
        /// test xml
        /// </summary>
        /// <param name="CList"></param>
        /// <returns></returns>
        public bool Insert(DataTable CList)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               // CList.Columns.Remove("CName");
                SqlDataAdapter sda = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("SP_Item_Category_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                //cmd.Parameters.AddWithValue("@Item_ID", item_ID);
                cmd.Parameters.Add("@Item_ID", SqlDbType.Int, 10, "Item_ID");
                cmd.Parameters.Add("@Category_ID", SqlDbType.Int, 10, "CID");
                cmd.Parameters.Add("@Category_SN", SqlDbType.Int, 50, "Category_SN");
                cmd.Parameters.Add("@Ctrl_ID", SqlDbType.NVarChar, 50, "Ctrl_ID");
                cmd.Connection.Open();
                sda.InsertCommand = cmd;
                sda.UpdateCommand=cmd;
                sda.Update(CList);
                cmd.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable CheckCategory(int Item_ID,DataTable dtcat)
        {
            try
            {
                DataTable dt = new DataTable();
                for (int i = 0; i < dtcat.Rows.Count; i++)
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    SqlDataAdapter sda = new SqlDataAdapter("SP_Check_Category", connectionString);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.CommandTimeout = 0;
                    sda.SelectCommand.Parameters.AddWithValue("@Item_ID", Item_ID);
                    sda.SelectCommand.Parameters.AddWithValue("@Path", dtcat.Rows[i]["Category"]);
                    sda.SelectCommand.Connection.Open();
                    sda.Fill(dt);
                    sda.SelectCommand.Connection.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CheckDescription(string check,int sn,int option,int parentid,string path)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand("SP_Check_Category_Description", connectionString);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Description", check);
                cmd.Parameters.AddWithValue("@SN", sn);
                cmd.Parameters.AddWithValue("@Parentid", parentid);
                cmd.Parameters.AddWithValue("@Path", path);
                cmd.Parameters.AddWithValue("@Option", option);
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                int id = Convert.ToInt16(cmd.Parameters["@id"].Value);
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
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
                    cmd.Parameters.AddWithValue("@Item_Name", dt.Rows[i]["商品カテゴリID"].ToString());
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
        public void InsertItemImportItemLog1(DataTable dt)
        {
            try
            {
                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);

                SqlCommand cmd = new SqlCommand("SP_ItemImport_ItemLog_XML", connectionString);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean InsertCSV(DataTable dt)
        {
            try
            {
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //    SqlCommand cmd = new SqlCommand("SP_Item_Category_Update", connectionString);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.CommandTimeout = 0;
                //    cmd.Parameters.AddWithValue("@Ctrl_ID", dt.Rows[i]["コントロールフラグ"].ToString());
                //    cmd.Parameters.AddWithValue("@Item_Code", dt.Rows[i]["商品番号"].ToString());
                //    cmd.Parameters.AddWithValue("@Catageory_Code", dt.Rows[i]["商品カテゴリID"].ToString());
                //    cmd.Parameters.AddWithValue("@Category_Name", dt.Rows[i]["商品カテゴリ名"].ToString());

                //    cmd.Connection.Open();
                //    cmd.ExecuteNonQuery();
                //    cmd.Connection.Close();                    
                //}

                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);

                SqlCommand cmd = new SqlCommand("SP_Item_Category_Update_XML", connectionString);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool Delete(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("DELETE FROM Item_Category WHERE Item_ID=@Item_ID", connectionString);
                //SqlCommand cmd = new SqlCommand("Update Item_Category Set Ctrl_ID='d' WHERE Item_ID=@Item_ID", connectionString);
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

        public DataTable SelectAllRootByItemID(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_SELECT_FullCategory_ByItemID", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
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

        public DataTable SelectByItemID(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Category_SelectByItemID", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
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

        public void InsertItem_Import_ItemLog(DataTable dt)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    SqlCommand cmd = new SqlCommand("SP_ItemCategory_ItemImportItemLog_Insert", connectionString);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    
                    cmd.Parameters.AddWithValue("@Item_Code", dt.Rows[i]["商品番号"].ToString());

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

        public DataTable getAllParentsbyName(int CategoryName,String catdesc,String secdesc,String thirddesc,String fourdesc,String fivedesc)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Category_GetParents", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
             //   if(!String.IsNullOrWhiteSpace(CategoryName))
                sda.SelectCommand.Parameters.AddWithValue("@CName", CategoryName);
                //else
                sda.SelectCommand.Parameters.AddWithValue("@Catdesc", catdesc);
                sda.SelectCommand.Parameters.AddWithValue("@Startdesc", secdesc);
                sda.SelectCommand.Parameters.AddWithValue("@Enddesc", thirddesc);
                sda.SelectCommand.Parameters.AddWithValue("@Fourthdesc", fourdesc);
                sda.SelectCommand.Parameters.AddWithValue("@Fithdesc", fivedesc);
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



        public DataTable getAllParentsbyCode(String CategoryCode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Category_Import_GetParentID", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@CID", CategoryCode);
              
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
