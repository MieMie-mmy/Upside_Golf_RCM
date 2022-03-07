using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ORS_RCM_BL;
using ORS_RCM_Common;
using System.Threading;
using System.Web.Services;

namespace ORS_RCM.WebForms
{
    public partial class SKS_DB_Backup : System.Web.UI.Page
    {
        public static string Connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        public static string BackUpLoc = ConfigurationManager.AppSettings["BackUp_Loc"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void btnBackup_Click(object sender, EventArgs e)
        //{
        //    //BakUp("ORS_RCM");
        //}

        [WebMethod]
        public static string BackupSKS()
        {
            BackUp("ORS_RCM");
            return "true";
        }

        public static void BackUp(string DBName)
        {
            try
            {
                DateTime start = DateTime.Now;
                SqlConnection conn = new SqlConnection(Connection);
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand = new SqlCommand(" BACKUP DATABASE @DBName TO DISK = @PATH ", conn);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.AddWithValue("@DBName", DBName);
                string FormattedDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                string Path = BackUpLoc + "System" + DBName + "(" + FormattedDate + ").bak";
                sqlCommand.Parameters.AddWithValue("@PATH", Path);
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                conn.Close();
                //GlobalUI.MessageBox("Database Backup Successful!!!");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}