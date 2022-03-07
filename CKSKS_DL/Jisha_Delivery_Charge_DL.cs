using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
   public class Jisha_Delivery_Charge_DL
   {
       string itemid;
       public Jisha_Delivery_Charge_DL() { }

       public  string Insert(DataTable dt,int option) 
       {
          
           try
           {
               for (int i = 0; i < dt.Rows.Count; i++) 
               {
                   SqlDataAdapter da = new SqlDataAdapter("SP_Jisha_Delivery_Charge", JsDataConfig.GetConnectionString());
                   da.SelectCommand.CommandType = CommandType.StoredProcedure;
                   da.SelectCommand.CommandTimeout = 0;
                   da.SelectCommand.Connection.Open();
                   if(dt.Columns.Contains("ID"))
                   da.SelectCommand.Parameters.AddWithValue("@ID", dt.Rows[i]["ID"].ToString());
                   else
                    
                   da.SelectCommand.Parameters.AddWithValue("@ID",DBNull.Value);
                   da.SelectCommand.Parameters.AddWithValue("@chargecondition", dt.Rows[i]["ChargeCondition"].ToString());
                   da.SelectCommand.Parameters.AddWithValue("@chargetype", dt.Rows[i]["Chargetype"].ToString());
                   da.SelectCommand.Parameters.AddWithValue("@deliverycharge", dt.Rows[i]["Deliverityfee"].ToString());
                   da.SelectCommand.Parameters.AddWithValue("@priority", dt.Rows[i]["Priority"].ToString());
                   da.SelectCommand.Parameters.AddWithValue("@option",option);
                   da.SelectCommand.Parameters.AddWithValue("@result",SqlDbType.Int).Direction =ParameterDirection.Output;
                   da.SelectCommand.ExecuteNonQuery();
                   da.SelectCommand.Connection.Close();
                   string str = da.SelectCommand.Parameters["@result"].Value.ToString();
                   itemid += str + ',';
               }

               return itemid;
           }
           catch (Exception ex ) 
           {
               throw ex;
           }
       
       }

       public DataTable SelectAll(string pri,int option) 
       {
           try 
           {
               DataTable dt = new DataTable();
               SqlDataAdapter da = new SqlDataAdapter("SP_Jisha_Delivery_Charge_SelectAll", JsDataConfig.GetConnectionString());
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.Parameters.AddWithValue("@priority",pri);
               da.SelectCommand.Parameters.AddWithValue("@option",option);
               da.SelectCommand.Connection.Open();
               da.Fill(dt);
               return dt;
           
           }
           catch (Exception ex) { throw ex; }
       
       }
       public string CODInsert(DataTable dt,int option)
       {
           try
           {
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   SqlDataAdapter da = new SqlDataAdapter("SP_Jisha_COD_Insert", JsDataConfig.GetConnectionString());
                   da.SelectCommand.CommandType = CommandType.StoredProcedure;
                   da.SelectCommand.CommandTimeout = 0;
                   da.SelectCommand.Connection.Open();
                   if (dt.Columns.Contains("ID"))
                       da.SelectCommand.Parameters.AddWithValue("@ID", dt.Rows[i]["ID"].ToString());
                   else
                       da.SelectCommand.Parameters.AddWithValue("@ID", DBNull.Value);
                   da.SelectCommand.Parameters.AddWithValue("@chargecondition", dt.Rows[i]["ChargeCondition"].ToString());
                   da.SelectCommand.Parameters.AddWithValue("@chargetype", dt.Rows[i]["Chargetype"].ToString());
                   da.SelectCommand.Parameters.AddWithValue("@deliverycharge", dt.Rows[i]["Deliverityfee"].ToString());
                   da.SelectCommand.Parameters.AddWithValue("@priority", dt.Rows[i]["Priority"].ToString());
                   da.SelectCommand.Parameters.AddWithValue("@option", option);
                   da.SelectCommand.Parameters.AddWithValue("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                   da.SelectCommand.ExecuteNonQuery();
                   da.SelectCommand.Connection.Close();
                   string str = da.SelectCommand.Parameters["@result"].Value.ToString();
                   itemid += str + ',';
               }

               return itemid;
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       public DataTable Slectdiv() 
       {
           SqlDataAdapter da = new SqlDataAdapter("Select * From Jisha_Division", DataConfig.connectionString);
           da.SelectCommand.CommandType = CommandType.Text;
           da.SelectCommand.CommandTimeout = 0;
           da.SelectCommand.Connection.Open();
           DataTable dt = new DataTable();
           da.Fill(dt);
           da.SelectCommand.Connection.Close();
           return dt;
       
       }

       ///for Jisha Searchitem
       ///
       public DataTable Search(string itemname) 
       {
           try 
           {
               DataTable dt = new DataTable();
               SqlDataAdapter da = new SqlDataAdapter("SP_Jisha_Search_Items", JsDataConfig.GetConnectionString());
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.Connection.Open();
               da.SelectCommand.Parameters.AddWithValue("@searchitemname", itemname);
          
               da.Fill(dt);
               da.SelectCommand.Connection.Close();
           return dt;
           }
           catch (Exception ex) { throw ex; }
       }

       public DataTable SelectDeliveryChargeByDivisionID(int divID)
       {
           SqlDataAdapter da = new SqlDataAdapter("Select * From Jisha_Delivery_Charge Where Charge_Type = " + divID, JsDataConfig.GetConnectionString());
           da.SelectCommand.CommandType = CommandType.Text;
           da.SelectCommand.CommandTimeout = 0;
           da.SelectCommand.Connection.Open();
           DataTable dt = new DataTable();
           da.Fill(dt);
           da.SelectCommand.Connection.Close();
           return dt;
       }

       public DataTable SelectCODChargeByDivisionID(int divID)
       {
           SqlDataAdapter da = new SqlDataAdapter("Select * From Jisha_COD_Charge Where Charge_Type = " + divID, JsDataConfig.GetConnectionString());
           da.SelectCommand.CommandType = CommandType.Text;
           da.SelectCommand.CommandTimeout = 0;
           da.SelectCommand.Connection.Open();
           DataTable dt = new DataTable();
           da.Fill(dt);
           da.SelectCommand.Connection.Close();
           return dt;
       }

    }
}
