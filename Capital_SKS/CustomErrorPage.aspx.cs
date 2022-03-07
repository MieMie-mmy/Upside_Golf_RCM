using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.SessionState;

namespace ORS_RCM
{
    public partial class CustomErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowDetailedError();
        }

        protected void ShowDetailedError()
        {
            if (Session["Exception"] != null)
            {
                String e = Session["Exception"].ToString();
                SaveErrorLog(e);
                Session.Remove("Exception");
            }
        }

        protected void SaveErrorLog(String e)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
            cmd.CommandType = CommandType.StoredProcedure;

            if(Session["User_ID"]!=null)
                cmd.Parameters.AddWithValue("@UserID", Session["User_ID"].ToString());
            else cmd.Parameters.AddWithValue("@UserID", -1);
            
            cmd.Parameters.AddWithValue("@ErrorDetail", e);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}