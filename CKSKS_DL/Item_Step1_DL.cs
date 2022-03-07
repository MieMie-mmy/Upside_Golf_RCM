// -----------------------------------------------------------------------
// <copyright file="Item_Step1_DL.cs" company="Capital knowledge">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

/* 
Created By              : Kyaw Thet Paing
Created Date          : 23/06/2014
Updated By             :
Updated Date         :

 Tables using: 
    -
    -
    -
*/

namespace ORS_RCM_DL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.SqlClient;
    using System.Data;
    using ORS_RCM_Common;
    using System.Transactions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Item_Step1_DL
    {
        

        public Boolean Item_Step1_Update(Item_Step1_Entity ise, DataTable dt)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    SqlCommand cmd = new SqlCommand("Item_Step1_Update", connectionString);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@Item_Code", ise.ItemCode);
                    cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    int result = -1;
                    if (cmd.Parameters["@result"].Value != DBNull.Value)
                        result = Convert.ToInt32(cmd.Parameters["@result"].Value.ToString());
                    cmd.Connection.Close();
                    if (result == -1)
                        return false;
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                cmd = new SqlCommand("Item_Image_Update", connectionString);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Item_ID", result);
                                cmd.Parameters.AddWithValue("@Image_Name", dt.Rows[i]["Name"].ToString());
                                cmd.Parameters.AddWithValue("@Image_Type", dt.Rows[i]["Type"].ToString());
                                cmd.Connection.Open();
                                cmd.ExecuteNonQuery();
                                cmd.Connection.Close();
                            }
                        }

                        scope.Complete();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
            }


   }
}
