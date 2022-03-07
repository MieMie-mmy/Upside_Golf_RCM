using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
    public class LogInDL
    {
        

        public DataTable LogINCheck(string loginID) 
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SP_LogIn_LoginID_Check", connectionString);
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

        public DataTable getAdmin(string loginID)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("Select IsAdmin From [User] Where ID ="+loginID, connectionString);
            try
            {
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

        public int Check(string loginID, string password) 
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand("SP_LogIn_User_Check", connectionString);
            try 
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@LoginID", loginID);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Connection.Open();
                int count =(int) cmd.ExecuteScalar();
                cmd.Connection.Close();
                return count;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        
        }

        public bool Check_PageAccess(int id, string url, string pagecode)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_PageAccess_Check", connectionString);
            try
            {

                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;

                sda.SelectCommand.Parameters.AddWithValue("@userID", id);
                sda.SelectCommand.Parameters.AddWithValue("@url", url);
                sda.SelectCommand.Parameters.AddWithValue("@Page_Code", pagecode);

                DataTable dt = new DataTable();

                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                

                if (dt.Rows.Count == 0)
                {

                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                sda.SelectCommand.Connection.Close();
                sda.Dispose();

            }

        }
        public User_entity SelectPassword(string LogIn_ID)
        {
            SqlDataAdapter da = new SqlDataAdapter("SP_LogIN_SelectAll", DataConfig.connectionString);
            try
            {

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.SelectCommand.Parameters.AddWithValue("@LogIn_ID", LogIn_ID);

                da.SelectCommand.Connection.Open();
                da.Fill(dt);


                User_entity userInfo = new User_entity();

                if (dt.Rows.Count > 0)
                {
                    userInfo.Password = dt.Rows[0]["Password"].ToString();
                    userInfo.User_Name = dt.Rows[0]["User_Name"].ToString();

                }
                else
                {
                    userInfo.Login_ID = string.Empty;
                    userInfo.Password = string.Empty;
                }
                da.SelectCommand.Connection.Close();
                return userInfo;
            }
            catch (Exception ex)
            {
                throw ex;


            }
            finally
            {
                da.SelectCommand.Connection.Close();
                da.Dispose();
            }

        }
    }
}
