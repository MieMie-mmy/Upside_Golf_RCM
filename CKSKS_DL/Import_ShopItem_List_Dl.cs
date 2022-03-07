using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;


namespace ORS_RCM_DL
{
    public class Import_ShopItem_List_Dl
    {
       

        public DataTable Select()
        {

            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Import_ShopItem_ALL", connectionString);
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

        public DataTable Import_ShopItem_Search(String itemcode)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandText = "SP_Import_ShopItem_Search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Item_Code", itemcode);



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

    }
}

    

