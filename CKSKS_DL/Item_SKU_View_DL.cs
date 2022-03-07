/* 
Created By              : Ee Phyo
Created Date          : 16/07/2014
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
  public  class Item_SKU_View_DL
    {

      

      public DataTable View(string itemcode)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter();
              SqlCommand cmdSelect = new SqlCommand("SP_Item_Popup", connectionString);
              da.SelectCommand = cmdSelect;
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;

              cmdSelect.Parameters.AddWithValue("@Itemcode", itemcode);

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
