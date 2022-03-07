using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
   public class Jisha_Admin_LoginDL
    {
       public Jisha_Admin_LoginDL() { }

       public DataTable LogINCheck(string loginID)
       {
           SqlDataAdapter da = new SqlDataAdapter("SP_Jisha_AdminLoginID_Check", JsDataConfig.GetConnectionString());
           try
           {
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               DataTable dt = new DataTable();
               da.SelectCommand.Connection.Open();
               da.SelectCommand.Parameters.AddWithValue("@LoginID", loginID);
               da.Fill(dt);
               da.SelectCommand.Connection.Close();
               return dt;

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public int Check(string loginID, string password)
       {
           SqlCommand cmd = new SqlCommand("SP_Jisha_AdminLogin_Check", JsDataConfig.GetConnectionString());
           try
           {
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Parameters.AddWithValue("@LoginID", loginID);
               cmd.Parameters.AddWithValue("@password", password);
               cmd.Connection.Open();
               int count = (int)cmd.ExecuteScalar();
               return count;
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
    }
}
