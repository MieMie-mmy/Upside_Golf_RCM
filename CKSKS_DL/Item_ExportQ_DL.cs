/* 
Created By              : Aye Aye Mon
Created Date          : 15/07/2014
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
using System.Data.SqlClient;
using System.Data;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
    public class Item_ExportQ_DL
    {
        

        public Item_ExportQ_DL()
        { 
        
        }

        public bool Save(Item_ExportQ_Entity ie)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Item_ExportQ_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@File_Name", ie.File_Name);
                cmd.Parameters.AddWithValue("@File_Type", ie.File_Type);
                cmd.Parameters.AddWithValue("@ShopID", ie.ShopID);
                cmd.Parameters.AddWithValue("@IsExport", ie.IsExport);
                cmd.Parameters.AddWithValue("@Export_Type", ie.Export_Type);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ChangeIsExportFlag()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Item_ExportQ_ChangeIsExportFlag";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
