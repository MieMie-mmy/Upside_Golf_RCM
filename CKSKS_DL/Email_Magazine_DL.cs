using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_Common;
using System.Data.SqlClient;
using System.Data;

namespace ORS_RCM_DL
{
   public  class Email_Magazine_DL
    {
        public Email_Magazine_DL() { }

        public bool Insert(Email_Magazine_Entity eme)
        {
          SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
          SqlCommand cmd = new SqlCommand();
          try
          {
              cmd.CommandText = "SP_EmailMagazine_Insert";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
              cmd.Parameters.AddWithValue("@emailid",eme.Mail_Magazine_ID);
              cmd.Parameters.AddWithValue("@deliverydate",eme.Delivery_Date);
              cmd.Parameters.AddWithValue("@emailname", eme.Mail_Magazine_Name);
              cmd.Parameters.AddWithValue("@shopid", eme.Shop_ID);
              cmd.Parameters.AddWithValue("@deliverytime", eme.Delivery_Time);
              cmd.Parameters.AddWithValue("@campaign1", eme.Campaign1);
              cmd.Parameters.AddWithValue("@campaign2", eme.Campaign2);
              cmd.Parameters.AddWithValue("@campaign3", eme.Campaign3);
              cmd.Parameters.AddWithValue("@campaign4", eme.Campaign4);
              cmd.Parameters.AddWithValue("@campaign5", eme.Campaign5);
              cmd.Parameters.AddWithValue("@campaign6", eme.Campaign6);
              cmd.Parameters.AddWithValue("@campaign7", eme.Campaign7);
              cmd.Parameters.AddWithValue("@campaign8", eme.Campaign8);
              cmd.Parameters.AddWithValue("@campaign9", eme.Campaign9);
              cmd.Parameters.AddWithValue("@campaign10", eme.Campaign10);
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

        public bool Update(Email_Magazine_Entity eme)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_EmailMagazine_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@ID", eme.ID);
                if (!String.IsNullOrWhiteSpace(eme.Mail_Magazine_ID))
                    cmd.Parameters.AddWithValue("@emailid",eme.Mail_Magazine_ID);
                else
                    cmd.Parameters.AddWithValue("@emailid", DBNull.Value);
                cmd.Parameters.AddWithValue("@deliverydate", eme.Delivery_Date);
                cmd.Parameters.AddWithValue("@emailname", eme.Mail_Magazine_Name);
                cmd.Parameters.AddWithValue("@shopid", eme.Shop_ID);
                cmd.Parameters.AddWithValue("@deliverytime", eme.Delivery_Time);
                cmd.Parameters.AddWithValue("@campaign1", eme.Campaign1);
                cmd.Parameters.AddWithValue("@campaign2", eme.Campaign2);
                cmd.Parameters.AddWithValue("@campaign3", eme.Campaign3);
                cmd.Parameters.AddWithValue("@campaign4", eme.Campaign4);
                cmd.Parameters.AddWithValue("@campaign5", eme.Campaign5);
                cmd.Parameters.AddWithValue("@campaign6", eme.Campaign6);
                cmd.Parameters.AddWithValue("@campaign7", eme.Campaign7);
                cmd.Parameters.AddWithValue("@campaign8", eme.Campaign8);
                cmd.Parameters.AddWithValue("@campaign9", eme.Campaign9);
                cmd.Parameters.AddWithValue("@campaign10", eme.Campaign10);
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

        public int Check_MalID(int lid,string mid)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection Connection = new SqlConnection(DataConfig.connectionString);
            try
            {
                cmd.CommandText = "SP_Check_MallID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = Connection;
                cmd.Parameters.AddWithValue("@ID",lid);
                cmd.Parameters.AddWithValue("@MID", mid);
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

        public DataTable SelectByCName(String cid)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection Connection = new SqlConnection(DataConfig.connectionString);
            try
            {
                cmd.CommandText = "SP_Select_Campaign_Data";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = Connection;
                if(!String.IsNullOrWhiteSpace(cid))
                    cmd.Parameters.AddWithValue("@Campaign_ID", cid);
                else cmd.Parameters.AddWithValue("@Campaign_ID", DBNull.Value);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                cmd.Connection.Open();
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        
        }

        public DataTable SelectAll( )
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Mail_Magazine_SelectAll", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable Search(Email_Magazine_Entity eme, int pindex, int psize)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("SP_Email_Magazine_Search", connectionString);
            sda.SelectCommand.CommandTimeout = 0;
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (!String.IsNullOrWhiteSpace(eme.Mail_Magazine_ID))
            sda.SelectCommand.Parameters.AddWithValue("@mid",eme.Mail_Magazine_ID);
            else
                sda.SelectCommand.Parameters.AddWithValue("@mid", DBNull.Value);
            if (!String.IsNullOrWhiteSpace(eme.Mail_Magazine_Name))
                sda.SelectCommand.Parameters.AddWithValue("@mname", eme.Mail_Magazine_Name);
            else sda.SelectCommand.Parameters.AddWithValue("@mname", DBNull.Value);
            if (!String.IsNullOrWhiteSpace(eme.Shopname))
                sda.SelectCommand.Parameters.AddWithValue("@shopname", eme.Shopname);
            else sda.SelectCommand.Parameters.AddWithValue("@shopname", DBNull.Value);
            if (!String.IsNullOrWhiteSpace(eme.Campaign))
                sda.SelectCommand.Parameters.AddWithValue("@campaign", eme.Campaign);
            else sda.SelectCommand.Parameters.AddWithValue("@campaign", DBNull.Value);
            sda.SelectCommand.Parameters.AddWithValue("@ddate", eme.Delivery_Date);
            sda.SelectCommand.Parameters.AddWithValue("@PageIndex", pindex);
            sda.SelectCommand.Parameters.AddWithValue("@PageSize", psize);
            sda.SelectCommand.Connection.Open();
            sda.Fill(dt);
            sda.SelectCommand.Connection.Close();
            return dt;
        }

        public DataTable SelectByPID(int PID)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_SelectByPID", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@PID", PID);
                DataTable dt = new DataTable();
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

        public Email_Magazine_Entity SelectByID(int ID)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_Magazine_SelectByID", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ID", ID);
                DataTable dt = new DataTable();
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                Email_Magazine_Entity eme = new Email_Magazine_Entity();
                if (dt!=null && dt.Rows.Count > 0)
                {
                    eme.ID = (int)dt.Rows[0]["ID"];
                    eme.Mail_Magazine_ID =dt.Rows[0]["Mail_Magazine_ID"].ToString();
                    eme.Mail_Magazine_Name = dt.Rows[0]["Mail_magazine_Name"].ToString();
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Shop_ID"].ToString()))
                    {
                        eme.Shop_ID = Convert.ToInt32(dt.Rows[0]["Shop_ID"]);
                    }
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Delivery_date"].ToString()))
                    {
                        eme.Delivery_Date = Convert.ToDateTime(dt.Rows[0]["Delivery_date"]);
                    }
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Delivery_time"].ToString()))
                    {
                        eme.Delivery_Time = dt.Rows[0]["Delivery_time"].ToString();
                    }
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Campaign1"].ToString()))
                    {
                        eme.Campaign1 = dt.Rows[0]["Campaign1"].ToString();
                    }
                    eme.Campaign2 = dt.Rows[0]["Campaign2"].ToString();
                    eme.Campaign3 = dt.Rows[0]["Campaign3"].ToString();
                    eme.Campaign4 = dt.Rows[0]["Campaign4"].ToString();
                    eme.Campaign5 = dt.Rows[0]["Campaign5"].ToString();
                    eme.Campaign6 = dt.Rows[0]["Campaign6"].ToString();
                    eme.Campaign7 = dt.Rows[0]["Campaign7"].ToString();
                    eme.Campaign8 = dt.Rows[0]["Campaign8"].ToString();
                    eme.Campaign9 = dt.Rows[0]["Campaign9"].ToString();
                    eme.Campaign10 = dt.Rows[0]["Campaign10"].ToString();
                }
                    return eme;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable EmailMagazineXmlInsert(string xml)
        {
            SqlConnection connection = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand("SP_EmailMagazine_Campaign_XML", connection);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                da.Fill(dt);

                return dt;
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

    }
}
