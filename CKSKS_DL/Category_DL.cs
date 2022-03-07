/* 
Created By              : Kay Thi Aung
Created Date          : 19/06/2014
Updated By             :
Updated Date         :

 Tables using: 
    -Category
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
   public class Category_DL
    {
       public DataTable SelectForTreeview1(int i)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter("SELECT top(1)* FROM Category WITH (NOLOCK) Where ParentID= " + i + " Order by Category_SN", connectionString);
               da.SelectCommand.CommandType = CommandType.Text;
               da.SelectCommand.CommandTimeout = 0;

               DataTable dt = new DataTable();

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

       public DataTable SelectForTreeview(int i)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Category WITH (NOLOCK) Where ParentID= " + i + " Order by Category_SN", connectionString);
               da.SelectCommand.CommandType = CommandType.Text;
               da.SelectCommand.CommandTimeout = 0;

               DataTable dt = new DataTable();

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

       public DataTable  ForTreeview(int i)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter("SELECT *,Description As CName FROM Category Where ParentID= " + i + "Order by Category_SN", connectionString);
               da.SelectCommand.CommandType = CommandType.Text;
               da.SelectCommand.CommandTimeout = 0;


               DataTable dt = new DataTable();

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
       
       public void CategoryInsert(int childID, String text,String CID,String RID,String RCNo,String Yahoo,String Wowma,String Tennis,String JID,String JCNo,int  Serial,String path)
       {
           SqlConnection connection = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand();
           try
           {
               cmd.CommandText = "SP_Category_Update ";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Connection = connection;
               cmd.Parameters.AddWithValue("@CategoryID",CID);
               cmd.Parameters.AddWithValue("@childID", childID);
               cmd.Parameters.AddWithValue("@Description", text);
               cmd.Parameters.AddWithValue("@RakutenDID", RID);
               cmd.Parameters.AddWithValue("@RakutenCNo", RCNo);
               cmd.Parameters.AddWithValue("@Yahoo ", Yahoo);
                cmd.Parameters.AddWithValue("@Wowma", Wowma);
                cmd.Parameters.AddWithValue("@Tennis", Tennis);
                cmd.Parameters.AddWithValue("@JishaDID", JID);
               cmd.Parameters.AddWithValue("@JishaCNo", JCNo);
               cmd.Parameters.AddWithValue("@Serial", Serial);
               cmd.Parameters.AddWithValue("@path", path);
               cmd.Parameters.AddWithValue("@option", 0);
               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               cmd.Connection.Close();
           }
           catch (Exception ex)
           {
               throw ex;

           }

       }


        public void Insert_NewRakuten_Category(string path)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_NewRakuten_Category_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@shop_id", 1);
                cmd.Parameters.AddWithValue("@path", path);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DataTable GetRootPath(string path)
       {
           SqlConnection connection = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand();
           try
           {
               cmd.CommandText = "SP_Category_Path_Check";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Connection = connection;
               DataTable dt = new DataTable();
               SqlDataAdapter sqlData = new SqlDataAdapter(cmd);

               cmd.Parameters.AddWithValue("@Path", path);
               cmd.Connection.Open();
               sqlData.Fill(dt);
               cmd.Connection.Close();

               return dt;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public void CategoryUpdate(int childID, String text, String CID, String RID, String RCNo, String Yahoo,String Wowma, String Tennis,String JID,String JCNo,int Serial,String pathdesc)
       {
           SqlConnection connection = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand();
           try
           {
               cmd.CommandText = "SP_Category_Update";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Connection = connection;
               cmd.Parameters.AddWithValue("@CategoryID", CID);
               cmd.Parameters.AddWithValue("@childID", childID);
               cmd.Parameters.AddWithValue("@Description", text);
               cmd.Parameters.AddWithValue("@RakutenDID", RID);
               cmd.Parameters.AddWithValue("@RakutenCNo", RCNo);
               cmd.Parameters.AddWithValue("@Yahoo ", Yahoo);
                cmd.Parameters.AddWithValue("@Wowma", Wowma);
                cmd.Parameters.AddWithValue("@Tennis", Tennis);
                cmd.Parameters.AddWithValue("@JishaDID", JID);
               cmd.Parameters.AddWithValue("@JishaCNo", JCNo);
               cmd.Parameters.AddWithValue("@Serial", Serial);
               cmd.Parameters.AddWithValue("@path", pathdesc);
               cmd.Parameters.AddWithValue("@option", 1);
               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               cmd.Connection.Close();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void CatalogSerialnoUpdate(int serialno, int id)
       {
           SqlConnection connection = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand();
           try
           {
               cmd.CommandText = "SP_Category_Serialno_Update";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Connection = connection;

               cmd.Parameters.AddWithValue("@Serialno", serialno);
               cmd.Parameters.AddWithValue("@parid", id);
               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               cmd.Connection.Close();
           }
           catch (Exception ex) { throw ex; }
       }

       public void UpdateSerialno(DataTable dt)
       {
           try
           {
               foreach (DataRow dr in dt.Rows)
               {
                   SqlConnection sqlConnection = new SqlConnection(DataConfig.connectionString);
                   SqlDataAdapter Adaptor = new SqlDataAdapter();
                   SqlCommand cmd = new SqlCommand("UPDATE Category SET Category_SN= @Serialno WHERE ID = @ID", sqlConnection);
                   cmd.CommandTimeout = 0;
                   cmd.CommandType = CommandType.Text;
                   cmd.Parameters.AddWithValue("@Serialno", int.Parse(dr["Category_SN"].ToString()));
                   cmd.Parameters.AddWithValue("@ID", int.Parse(dr["ID"].ToString()));
                   cmd.Connection = sqlConnection;
                   sqlConnection.Open();
                   cmd.ExecuteNonQuery();
                   sqlConnection.Close();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public DataTable SelectAllForSerial(int serialno,int parid)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Category  where Category_SN=" + serialno + " and ParentID=" + parid, connectionString);
               da.SelectCommand.CommandType = CommandType.Text;
               da.SelectCommand.CommandTimeout = 0;


               DataTable dt = new DataTable();

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
      
       public void Delete(int id)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand();
           try
           {
               cmd.CommandText = "SP_Category_Delete";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Connection = connectionString;

               cmd.Parameters.AddWithValue("@ID", id);
               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               cmd.Connection.Close();


           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
   
       public DataTable SelectForCatalogID(int i)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Category WITH (NOLOCK) Where ID=" + i, connectionString);
               da.SelectCommand.CommandType = CommandType.Text;

               da.SelectCommand.CommandTimeout = 0;

               DataTable dt = new DataTable();

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
     
       public int  check(String CID) 
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand();
           try
           {
               cmd.Connection = connectionString;
               cmd.CommandText = "SP_Category_Check";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Parameters.AddWithValue("@CategoryID",CID);
               cmd.Connection.Open();
               int count = (int)cmd.ExecuteScalar();
               cmd.Connection.Close();
               return count;
           }
           catch (Exception ex) 
           {
               throw ex;
           }
       }

       public DataTable SearchTree(String cID, String cname)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               cmd.CommandText = "SP_CategoryTreeView_Search";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Connection = connectionString;
               DataTable dt = new DataTable();
               cmd.Parameters.AddWithValue("@CategoryID", cID);
               cmd.Parameters.AddWithValue("@Description", cname);
               cmd.Connection.Open();
               da.Fill(dt);
               cmd.Connection.Close();
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public DataTable Search( String CID,String Desc,String startdesc,String enddesc,String foutdesc,String fithdesc) 
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               cmd.CommandText = "SP_Category_Search";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Connection = connectionString;
               DataTable dt = new DataTable();
               cmd.Parameters.AddWithValue("@CategoryID", CID);
               cmd.Parameters.AddWithValue("@Description", Desc);
               cmd.Parameters.AddWithValue("@Startdesc",startdesc);
              cmd.Parameters.AddWithValue("@Enddesc",enddesc);
              cmd.Parameters.AddWithValue("@Fourdesc", foutdesc);
              cmd.Parameters.AddWithValue("@Fithdesc", fithdesc);
               cmd.Connection.Open();
               da.Fill(dt);
               cmd.Connection.Close();
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public DataTable ExCSV(int catid,int ctrl) 
       {
           try {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter("SP_Shop_Category_ExportCSV", connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.Parameters.AddWithValue("@catID",null);
               da.SelectCommand.Parameters.AddWithValue("@ctrl",0);
               DataTable dt = new DataTable();
               da.SelectCommand.Connection.Open();
               da.Fill(dt);
               da.SelectCommand.Connection.Close();
               return dt;
           }
           catch (Exception ex) { throw ex; }
       }

       public DataTable Excsvsearch(int id) 
       {
           DataTable dts = new DataTable();
               try
               {
                   SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                   SqlDataAdapter da = new SqlDataAdapter("SP_Shop_Category_ExportCSV", connectionString);
                   da.SelectCommand.CommandType = CommandType.StoredProcedure;
                   da.SelectCommand.CommandTimeout = 0;
                   da.SelectCommand.Parameters.AddWithValue("@catID", id);
                   da.SelectCommand.Parameters.AddWithValue("@ctrl",1);
                   da.SelectCommand.Connection.Open();
                   da.Fill(dts);
                   da.SelectCommand.Connection.Close();
                   return dts;
               }
               catch (Exception ex) { throw ex; }
       }

       public DataTable GetDirectory_ID(String Id)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter("SP_Category_PathID", connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.Parameters.AddWithValue("@cate_pathID", Id);
               DataTable dt = new DataTable();
               da.SelectCommand.Connection.Open();
               da.Fill(dt);
               da.SelectCommand.Connection.Close();
               return dt;
           }
           catch (Exception ex) { throw ex; }
       }

       public DataTable GetCategoryID(string CategoryNo)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           DataTable dts = new DataTable();
           try
           {
               string query = String.Format("SELECT * FROM Category WHERE Category_ID = '{0}'", CategoryNo);
               SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
               da.SelectCommand.CommandType = CommandType.Text;
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.Connection.Open();
               da.Fill(dts);
               da.SelectCommand.Connection.Close();
               return dts;
           }
           catch (Exception ex) { throw ex; }
       }

       public DataTable GetCategoryIDAuto()
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           DataTable dts = new DataTable();

           try
           {
               string query = String.Format("SP_CategoryID_Autogenerate");
               SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.Connection.Open();
               da.Fill(dts);
               da.SelectCommand.Connection.Close();
               return dts;

           }
           catch (Exception ex) { throw ex; }
       }

       public DataTable getAllParentsbyCID(int ID)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();
               SqlDataAdapter sda = new SqlDataAdapter("SP_ShopCatrgory_Path", connectionString);
               sda.SelectCommand.CommandType = CommandType.StoredProcedure;
               sda.SelectCommand.CommandTimeout = 0;
               sda.SelectCommand.Parameters.AddWithValue("@ID", ID);
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

       public void UpdatePath(DataTable dt)
       {
           try
           {
               dt.TableName = "test";
               System.IO.StringWriter writer = new System.IO.StringWriter();
               dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
               string result = writer.ToString();
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlCommand cmd = new SqlCommand("SP_Path_updateXML", connectionString);
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

       public DataTable getpath(String ID)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();
               SqlDataAdapter sda = new SqlDataAdapter("SP_GetPath", connectionString);
               sda.SelectCommand.CommandType = CommandType.StoredProcedure;
               sda.SelectCommand.CommandTimeout = 0;
               sda.SelectCommand.Parameters.AddWithValue("@PID", ID);
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
