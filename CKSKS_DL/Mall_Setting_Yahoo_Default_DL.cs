/* 
Created By              : Eephyo
Created Date          : 27/06/2014
Updated By             :
Updated Date         :

 Tables using: Shop,Mall_Setting_Yahoo_Default,Code_Setup
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
using System.Globalization;

namespace ORS_RCM_DL
{ 
    public class Mall_Setting_Yahoo_Default_DL
    {
        

        public bool Insert(Mall_Setting_Yahoo_Default_Entity  Ydentity)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            {
                 try
           {
               cmd.CommandText = "SP_Mall_Setting_Yahoo_Default_Insert";
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Connection = connectionString;
               cmd.Parameters.AddWithValue("@shopid", Ydentity.Shop_ID);
               cmd.Parameters.AddWithValue("@weight", Ydentity.Weight);
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
        }

        public bool Update(Mall_Setting_Yahoo_Default_Entity yhentity)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Mall_Setting_Yahoo_Default_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@ID", yhentity.ID);
                cmd.Parameters.AddWithValue("@shopid", yhentity.Shop_ID);
                cmd.Parameters.AddWithValue("@weight", yhentity.Weight);
               
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

         public Mall_Setting_Yahoo_Default_Entity SelctByID(int ID)
       {
           SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
           SqlDataAdapter sda = new SqlDataAdapter("SP_Mall_Setting_Yahoo_Default_SelectbyID", connectionString);
           try
           {
               sda.SelectCommand.CommandType = CommandType.StoredProcedure;
               sda.SelectCommand.CommandTimeout = 0;
               sda.SelectCommand.Parameters.AddWithValue("@ID", ID);

               DataTable dt = new DataTable();

               sda.SelectCommand.Connection.Open();
               sda.Fill(dt);
               sda.SelectCommand.Connection.Close();
               Mall_Setting_Yahoo_Default_Entity yhentity = new Mall_Setting_Yahoo_Default_Entity();
               if (dt.Rows.Count > 0)
               {
                 yhentity.ID = (int)dt.Rows[0]["ID"];

                 yhentity.Shop_Name = (dt.Rows[0]["Shop_Name"].ToString());

                 yhentity.Mall_Name = (dt.Rows[0]["Mall_Name"].ToString());
                             
                 yhentity.Weight = (dt.Rows[0]["Weight"].ToString());
                        
               }

               return   yhentity;
           }
           catch (Exception ex)
           {
               throw ex;
           }
        }

    }
}