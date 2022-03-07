using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ORS_RCM.WebForms
{
    public partial class QuerySearch : System.Web.UI.Page
    {
        private string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            if (txtQuery.Text != "")
            {
                gvResult.DataSource = GetDataByQuery(txtQuery.Text);
                gvResult.DataBind();
            }
        }

        private DataTable GetDataByQuery(string query)
        {
            try
            {
               
                SqlConnection con = new SqlConnection(connection);
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.CommandType = CommandType.Text;

                DataTable dt = new DataTable();
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();

                return dt;
                
            }
            catch (Exception ex) 
            {
                MessageBox(ex.Message);
                return null;
            }
        }

        public void MessageBox(string message)
        {
            if (message == "Saving Successful ! " || message == "Updating Successful ! ")
            {
                Session["CatagoryList"] = null;
                object referrer = ViewState["UrlReferrer"];
                string url = (string)referrer;
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }
            else
            {
                string cleanMessage = message.Replace("'", "\\'");
                string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
                Page page = HttpContext.Current.CurrentHandler as Page;
                if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                {
                    page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                }
            }
        }

    }
}