/* 
Created By              : Eephyo
Created Date          : 31/07/2014
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
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;



namespace ORS_RCM_DL
{
    public class Option_DL
    {
        public bool checkGpName(String gpName,string id)
        { 
            SqlConnection con  = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM [Option] WHERE Option_GroupName=N'"+gpName+"' AND ID<>'"+id+"'", con);
            SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 2)
            {
                return true;
            }
            else return false;
        }


        public bool Insert(Option_Entity optEnt)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Option_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@ID", optEnt.ID);
                cmd.Parameters.AddWithValue("@ID2", optEnt.ID2);
                cmd.Parameters.AddWithValue("@ID3", optEnt.ID3);
                cmd.Parameters.AddWithValue("@Type1",optEnt.Type1);

                cmd.Parameters.AddWithValue("@Type2",optEnt.Type2);
                cmd.Parameters.AddWithValue("@Type3", optEnt.Type3);
                    cmd.Parameters.AddWithValue("@Option_GroupName", optEnt.Option_GroupName);
                
            
                    cmd.Parameters.AddWithValue("@Option_Name", optEnt.Option_Name);
              
            
                    cmd.Parameters.AddWithValue("@Option_Value", optEnt.Option_Value);
              

              
                    cmd.Parameters.AddWithValue("@Option_Name2", optEnt.Option_Name2);
                
               
                    cmd.Parameters.AddWithValue("@Option_Value2", optEnt.Option_Value2);
                

                
                    cmd.Parameters.AddWithValue("@Option_Name3", optEnt.Option_Name3);
                
                    cmd.Parameters.AddWithValue("@Option_Value3", optEnt.Option_Value3);
             
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (id > 0)
                {
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        
        public bool  Edit(Option_Entity optEnt)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Option_Edit";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@ID", optEnt.ID);
                cmd.Parameters.AddWithValue("@ID2", optEnt.ID2);
                cmd.Parameters.AddWithValue("@ID3", optEnt.ID3);
                cmd.Parameters.AddWithValue("@Type1", optEnt.Type1);

                cmd.Parameters.AddWithValue("@Type2", optEnt.Type2);
                cmd.Parameters.AddWithValue("@Type3", optEnt.Type3);
                cmd.Parameters.AddWithValue("@Option_GroupName", optEnt.Option_GroupName);


                cmd.Parameters.AddWithValue("@Option_Name", optEnt.Option_Name);


                cmd.Parameters.AddWithValue("@Option_Value", optEnt.Option_Value);



                cmd.Parameters.AddWithValue("@Option_Name2", optEnt.Option_Name2);


                cmd.Parameters.AddWithValue("@Option_Value2", optEnt.Option_Value2);



                cmd.Parameters.AddWithValue("@Option_Name3", optEnt.Option_Name3);

                cmd.Parameters.AddWithValue("@Option_Value3", optEnt.Option_Value3);

                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (id > 0)
                {
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool InsertNew(Option_Entity optEnt)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Option_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;

                cmd.Parameters.AddWithValue("@Option_GroupName", optEnt.Option_GroupName);

                cmd.Parameters.AddWithValue("@Option_Name", optEnt.Option_Name);

                cmd.Parameters.AddWithValue("@Option_Value", optEnt.Option_Value);



                cmd.Parameters.AddWithValue("@Option_Name2", optEnt.Option_Name2);

                cmd.Parameters.AddWithValue("@Option_Value2", optEnt.Option_Value2);



                cmd.Parameters.AddWithValue("@Option_Name3", optEnt.Option_Name3);

                cmd.Parameters.AddWithValue("@Option_Value3", optEnt.Option_Value3);


                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (id > 0)
                {
                    return true;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public DataTable Search()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmdSelect = new SqlCommand("SP_Option_byType", connectionString);
                da.SelectCommand = cmdSelect;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
               // cmdSelect.Parameters.AddWithValue("@type",type);
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
