/* 
Created By              : Kay Thi Aung
Created Date          : 30/06/2014
Updated By             :
Updated Date         :

 Tables using: Mall_Category
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
    public class Mall_Category_DL
    {
        

        public DataTable SearchDesc(string mid, string cname1, string cname2, string cname3)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmdSelect = new SqlCommand("SP_Mall_Category_Search", connection);
                da.SelectCommand = cmdSelect;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;

                if (String.IsNullOrWhiteSpace(cname1))
                {
                    cmdSelect.Parameters.AddWithValue("@cname1", "");
                }
                else
                    cmdSelect.Parameters.AddWithValue("@cname1", cname1);

                if (String.IsNullOrWhiteSpace(cname2))
                {
                    cmdSelect.Parameters.AddWithValue("@cname2", "");
                }
                else
                    cmdSelect.Parameters.AddWithValue("@cname2", cname2);

                if (String.IsNullOrWhiteSpace(cname3))
                {
                    cmdSelect.Parameters.AddWithValue("@cname3", "");
                }
                else
                    cmdSelect.Parameters.AddWithValue("@cname3", cname3);


                //if (String.IsNullOrWhiteSpace(cname4))
                //{
                //    cmdSelect.Parameters.AddWithValue("@cname4", "");
                //}
                //else
                //    cmdSelect.Parameters.AddWithValue("@cname4", cname4);



                if (String.IsNullOrWhiteSpace(mid))
                {
                    cmdSelect.Parameters.AddWithValue("@mallid", "");
                }
                else
                {
                    int mallid = Int32.Parse(mid);
                    cmdSelect.Parameters.AddWithValue("@mallid",mallid);
                }
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

        public DataTable SelectByID(int ID)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string query = "Select * from Mall_Category Where ID=" + ID;
                SqlDataAdapter sda = new SqlDataAdapter(query, connection);
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.CommandTimeout = 0;
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

        public Boolean InsertMall_Category_XML(String xml)
        {
            SqlConnection connection = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand("SP_Mall_Category_Update", connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandTimeout = 0;

                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                return true;
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

        public Boolean InsertCSV(DataTable dt,int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Mall_Category_Update", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter();

                if(dt.Columns.Contains("ジャンルID"))
                    cmd.Parameters.Add("@Category_ID", SqlDbType.NVarChar, 50, "ジャンルID");

               else if (dt.Columns.Contains("プロダクトカテゴリ"))
                    cmd.Parameters.Add("@Category_ID", SqlDbType.NVarChar, 50, "プロダクトカテゴリ");
                else
                    cmd.Parameters.Add("@Category_ID", SqlDbType.NVarChar, 50, "ディレクトリID");
                cmd.Parameters.Add("@Category_Path", SqlDbType.NVarChar, 50, "パス名");
                cmd.Parameters.Add("@Mall_ID", SqlDbType.Int, 50, "Mall_ID");
                da.InsertCommand = cmd;
                cmd.Connection.Open();
                da.Update(dt);
                cmd.Connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable SelectByMallID(int mallID, int pageindex, int pagesize, int option, string cname1, string cname2, string cname3)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                //string query = "Select * from Mall_Category WITH (NOLOCK) Where Mall_ID=" + mallID;
                SqlDataAdapter sda = new SqlDataAdapter("SP_Mall_Category_SelectByMall_ID", connection);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID",mallID);
                sda.SelectCommand.Parameters.AddWithValue("@PageIndex",pageindex);
                sda.SelectCommand.Parameters.AddWithValue("@PageSize",pagesize);
                sda.SelectCommand.Parameters.AddWithValue("@Option", option);
                if (String.IsNullOrWhiteSpace(cname1))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@cname1", "");
                }
                else
                      sda.SelectCommand.Parameters.AddWithValue("@cname1", cname1);

                if (String.IsNullOrWhiteSpace(cname2))
                {
                     sda.SelectCommand.Parameters.AddWithValue("@cname2", "");
                }
                else
                    sda.SelectCommand.Parameters.AddWithValue("@cname2", cname2);

                if (String.IsNullOrWhiteSpace(cname3))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@cname3", "");
                }
                else
                    sda.SelectCommand.Parameters.AddWithValue("@cname3", cname3);
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

        public DataTable SelectByMallID(int mallID)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string query = "Select * from Mall_Category WITH (NOLOCK) Where Mall_ID=" + mallID;
                SqlDataAdapter sda = new SqlDataAdapter(query, connection);
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.CommandTimeout = 0;
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

        public DataTable  ExportMall(int mallID)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmdSelect = new SqlCommand("SP_Mall_Category_Export", connection);
                da.SelectCommand = cmdSelect;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                cmdSelect.Parameters.AddWithValue("@Mall_ID", mallID);
                
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


        public DataTable ExportCategory()
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmdSelect = new SqlCommand("SP_Category_Export_ForJisha", connection);
                da.SelectCommand = cmdSelect;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
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


        public DataTable SearchCategoID(int  mid, string cid, string cname1, string cname2, string cname3)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmdSelect = new SqlCommand("SP_Mall_Category_SearchByMallID", connection);
                da.SelectCommand = cmdSelect;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@Mall_ID", mid);

                da.SelectCommand.Parameters.AddWithValue("@Catalog_ID", cid);
              
                if (String.IsNullOrWhiteSpace(cname1))
                {
                    cmdSelect.Parameters.AddWithValue("@cname1", "");
                }
                else
                    cmdSelect.Parameters.AddWithValue("@cname1", cname1);

                if (String.IsNullOrWhiteSpace(cname2))
                {
                    cmdSelect.Parameters.AddWithValue("@cname2", "");
                }
                else
                    cmdSelect.Parameters.AddWithValue("@cname2", cname2);

                if (String.IsNullOrWhiteSpace(cname3))
                {
                    cmdSelect.Parameters.AddWithValue("@cname3", "");
                }
                else
                    cmdSelect.Parameters.AddWithValue("@cname3", cname3);


                //if (String.IsNullOrWhiteSpace(cname4))
                //{
                //    cmdSelect.Parameters.AddWithValue("@cname4", "");
                //}
                //else
                //    cmdSelect.Parameters.AddWithValue("@cname4", cname4);



                //if ((mid!=0))
                //{
                //    cmdSelect.Parameters.AddWithValue("@Mall_ID", "");
                //}
                //else
                //{
                //    int mallid = mid;
                //    cmdSelect.Parameters.AddWithValue("@Mall_ID", mid);
                //}
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

    }
}
