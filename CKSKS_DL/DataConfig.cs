/* 
Created By              : Kyaw Thet Paing
Created Date          : 20/06/2014
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
using System.Configuration;

namespace ORS_RCM_DL
{
    public class DataConfig
    {
       public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    public class JsDataConfig
    {
        public static string jsconnectionstring = ConfigurationManager.ConnectionStrings["JishaConnectionString"].ConnectionString;
        static SqlConnection jsconnection = null;

        static public SqlConnection GetConnectionString()
        {

            try
            {
                if (jsconnection == null)
                    return jsconnection = new SqlConnection(jsconnectionstring);

                if (jsconnection.State == ConnectionState.Open)
                    jsconnection.Close();

                return jsconnection;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
