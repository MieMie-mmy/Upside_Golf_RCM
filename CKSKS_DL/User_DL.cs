/* 
Created By              : Eephyo
Created Date          : 19/06/2014
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
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
    public class User_DL
    {

        

        public bool Insert(User_entity  uentity)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_User_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@user_name", uentity.User_Name);
                cmd.Parameters.AddWithValue("@loginID", uentity.Login_ID);
                cmd.Parameters.AddWithValue("@Password", uentity.Password);

                cmd.Parameters.AddWithValue("@status", uentity.Status);
             
                 cmd.Parameters.AddWithValue("@createdDate", System.DateTime.Now.ToString());
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (id > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool Update(User_entity  uentity)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_UserName_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@ID", uentity.ID);
                cmd.Parameters.AddWithValue("@user_name", uentity.User_Name);
                cmd.Parameters.AddWithValue("@loginID", uentity.Login_ID);
                cmd.Parameters.AddWithValue("@password", uentity.Password);
                
                cmd.Parameters.AddWithValue("@status", uentity.Status);
                cmd.Parameters.AddWithValue("@updatedDate", System.DateTime.Now);    
      
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (id > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

       public DataTable Search(string name, string loginID,string  status)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter();
              SqlCommand cmdSelect = new SqlCommand("SP_UserName_Search", connectionString);
              da.SelectCommand = cmdSelect;
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;

              cmdSelect.Parameters.AddWithValue("@user_name", name);

              cmdSelect.Parameters.AddWithValue("@loginID", loginID);
              if(!String.IsNullOrWhiteSpace(status))
              cmdSelect.Parameters.AddWithValue("@status", status);
              
              else
                  cmdSelect.Parameters.AddWithValue("@status", null);
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

       public DataTable Searchzero(string name, string loginID)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter();
              SqlCommand cmdSelect = new SqlCommand("SP_UserName_SearchbyZero", connectionString);
              da.SelectCommand = cmdSelect;
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              cmdSelect.Parameters.AddWithValue("@user_name", name);
              cmdSelect.Parameters.AddWithValue("@loginID", loginID);
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

       public  User_entity SelectByID(int ID) 
      {
          SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
          SqlDataAdapter sda = new SqlDataAdapter("SP_User_SelectAll", connectionString);
       try
       {
           sda.SelectCommand.CommandType = CommandType.StoredProcedure;
           sda.SelectCommand.CommandTimeout = 0;
           sda.SelectCommand.Parameters.AddWithValue("@ID", ID);
          

           DataTable dt = new DataTable();

           sda.SelectCommand.Connection.Open();
           sda.Fill(dt);
            sda.SelectCommand.Connection.Close();
            User_entity uentity = new User_entity();
               if (dt.Rows.Count > 0)
               {
                   uentity.ID = (int)dt.Rows[0]["ID"];
                   uentity.User_Name = dt.Rows[0]["User_Name"].ToString();
                   uentity.Login_ID = dt.Rows[0]["Login_ID"].ToString();
                   uentity.Password=dt.Rows[0]["Password"].ToString();
                   uentity.Status =Convert.ToInt32(dt.Rows[0]["Status"].ToString());
               }

               return uentity;
       }
       catch (Exception ex) 
       {

           throw ex;
       }
      
      }

       public DataTable SelectAllByOne()
          {
              try
              {
                  SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                  DataTable ds = new DataTable();
                  string cmdstr = "select * from [User] where Status=1";
                  SqlDataAdapter adp = new SqlDataAdapter(cmdstr, connectionString);
                  adp.SelectCommand.CommandTimeout = 0;
                  adp.SelectCommand.Connection.Open();
                  adp.Fill(ds);
                  adp.SelectCommand.Connection.Close();
                  return ds;
              }catch(Exception ex)
              {
                  throw ex;
              }
          }

       public User_entity SelectZeroAll(int id)
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter sda = new SqlDataAdapter("SP_User_SelectZeroAll", connectionString);
       try
       {
           sda.SelectCommand.CommandType = CommandType.StoredProcedure;
           sda.SelectCommand.CommandTimeout = 0;

           sda.SelectCommand.Parameters.AddWithValue("@ID", id);

           DataTable dt = new DataTable();

           sda.SelectCommand.Connection.Open();
           sda.Fill(dt);
           sda.SelectCommand.Connection.Close();
           User_entity uentity = new User_entity();
           
               if (dt.Rows.Count > 0)
               {
                   uentity.ID = (int)dt.Rows[0]["ID"];
                   uentity.User_Name = dt.Rows[0]["User_Name"].ToString();
                   uentity.Login_ID = dt.Rows[0]["Login_ID"].ToString();
                   uentity.Password=dt.Rows[0]["Password"].ToString();
                   uentity.Status =Convert.ToInt32(dt.Rows[0]["Status"].ToString());
                
               }

               return uentity;
       }
       catch (Exception ex) 
       {

           throw ex;
       }
      
      }

       public DataSet SelectAll()
          {
              try
              {
                  SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                  DataSet ds = new DataSet();
                  string cmdstr = "select * from [User]";
                  SqlDataAdapter adp = new SqlDataAdapter(cmdstr, connectionString);
                  adp.SelectCommand.CommandTimeout = 0;
                  adp.SelectCommand.Connection.Open();
                  adp.Fill(ds);
                  adp.SelectCommand.Connection.Close();
                  return ds;
              }
              catch (Exception ex)
              {
                  throw ex;
              }
          }

    }
}
