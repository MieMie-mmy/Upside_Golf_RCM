using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ORS_RCM.Admin
{
    public partial class Query_Search : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string query = txtQuery.Text.Trim();
                if (!string.IsNullOrWhiteSpace(query))
                {
                    DataTable dt = new DataTable();
                    string type = query.Substring(0, 6);
                    if (type.ToLowerInvariant().Contains("select"))
                    { 
                        dt = ProcessSelectQuery(query);
                        gvQueryData.DataSource = dt;
                        gvQueryData.DataBind();
                        gvQueryData.Visible = true;
                    }
                    else
                    {
                        gvQueryData.DataSource = dt;
                        gvQueryData.DataBind();
                        gvQueryData.Visible = false;
                        if (ProcessDUIQuery(query))
                            MessageBox("Process Successful ! ");
                        else
                            MessageBox("Process Fail ! ");
                    }
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public DataTable ProcessSelectQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SP_ProcessQuery", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@query", query);
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                return dt;
            }
            catch(Exception ex)
            {
                MessageBox(ex.ToString());
                return dt;
            }
        }
        
        public Boolean ProcessDUIQuery(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProcessQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@query", query);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox(ex.ToString());
                return false;
            }
        }

        public void MessageBox(string message)
        {
            if ((message == "Process Successful ! ") || (message == "Process Fail ! "))
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