using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
    public class UserRoleDL
    {
        

        public UserRoleDL() { }

        public DataTable UserRoleSelectByID(int id)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_User_Role_Selectby_ID", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                sda.SelectCommand.Parameters.AddWithValue("@ID", id);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);


                return dt;
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

        public void UserRoleInsert(DataTable dtUserRole)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();

                cmd.CommandText = "SP_User_Role_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@UserID", SqlDbType.Int, 10, "UserID");
                cmd.Parameters.Add("@MenuID", SqlDbType.Int, 10, "MenuID");
                cmd.Parameters.Add("@canread", SqlDbType.Bit, 2, "CanRead");
              
                cmd.Connection.Open();
                sda.InsertCommand = cmd;

                sda.Update(dtUserRole);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        public int UserRoleDelete(int id)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                int result = 1;

                cmd.CommandText = "SP_User_Role_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@UserID", id);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                result = (int)cmd.Parameters["@result"].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        public string SelectName(int id)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                string Name = string.Empty;

                cmd.Connection = connectionString;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.CommandText = "SP_Select_UserName";
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Connection.Open();
                Name = (string)cmd.ExecuteScalar();

                return Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        public bool CanRead(int userID, string pageCode)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_UserRole_CanRead", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                sda.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                sda.SelectCommand.Parameters.AddWithValue("@Page_Code", pageCode);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);


                if (dt.Rows.Count == 0)
                    return false;
                else
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

        public bool CanSave(int userID, string pageCode)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_User_Role_SelectCanEdit", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                sda.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                sda.SelectCommand.Parameters.AddWithValue("@Page_Code", pageCode);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);

                if (dt.Rows.Count == 0)
                    return false;
                else
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

        public bool CanDelete(int userID, string pageCode)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_User_Role_SelectCanDelete", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;

                DataTable dt = new DataTable();
                sda.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                sda.SelectCommand.Parameters.AddWithValue("@Page_Code", pageCode);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sda.SelectCommand.Connection.Close();
                sda.SelectCommand.Dispose();
            }
        }

        public DataTable MenuSelectAll()
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_Menu_SelectAll", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                return dt;
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

        public  void  UserInsert(string name, string ID, string password ,int userID) 
        {
            try 
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_UserRole_Insert_User ", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@name",name);
                da.SelectCommand.Parameters.AddWithValue("@ID",ID);
                da.SelectCommand.Parameters.AddWithValue("@password",password);
                da.SelectCommand.Parameters.AddWithValue("@UserID ", userID);
               // da.SelectCommand.Parameters.AddWithValue("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                da.SelectCommand.Connection.Open();
                da.SelectCommand.ExecuteNonQuery();
                da.SelectCommand.Connection.Close();
                //int id = Convert.ToInt32(da.SelectCommand.Parameters["@result"].Value);
                //return id;
            }
            catch (Exception ex) { throw ex; }
        
        }

        public DataTable SelectbyUserRoleID(int MenuID) 
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM SYS_Menu WHERE Menu_ID=" + MenuID, connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);


                return dt;
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

        public DataTable SelectbyUserInfo(int ID) 
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [User] WHERE ID=" + ID, connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);


                return dt;
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

        public  int  Insert(User_entity  uentity)
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
                //if (id > 0)
                //{
                //    return true;
                //}
                //return false;
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


      public   DataTable SelectbyDuplicate(string loginID,int? ID)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_Duplicate_LogIn_Check", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                sda.SelectCommand.Parameters.AddWithValue("@LoginID", loginID);
                sda.SelectCommand.Parameters.AddWithValue("@ID", ID);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);


                return dt;



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
        


       public   DataTable Usertable(string mylogin)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("Select * from [User]  where Login_ID="+mylogin, connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                //sda.SelectCommand.Parameters.AddWithValue("@LoginID", mylogin);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);


                return dt;



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
        }








     
        }

    

