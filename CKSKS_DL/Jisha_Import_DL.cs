using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ORS_RCM_DL
{
    public class Jisha_Import_DL
    {

        public Jisha_Import_DL()
        { }

        public void Jisha_Item_Master_Import_Xml(string xml)
        {
            try
            {
                SqlConnection connection = JsDataConfig.GetConnectionString();
                SqlCommand cmd = new SqlCommand("SP_Jisha_Item_Master_Import_Xml", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Jisha_Item_Import_Xml(string xml)
        {
            try
            {
                SqlConnection connection = JsDataConfig.GetConnectionString();
                SqlCommand cmd = new SqlCommand("SP_Jisha_Item_Import_Xml", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Jisha_Item_Category_Import_Xml(string xml)
        {
            try
            {
                SqlConnection connection = JsDataConfig.GetConnectionString();
                SqlCommand cmd = new SqlCommand("SP_Jisha_Item_Category_Import_Xml", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

}
