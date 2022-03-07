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
    public partial class Log_Data_Delete : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            else
            {
                hdfToDate.Value = Request.Form[txttodate.UniqueID];
                hdfFromDate.Value = Request.Form[txtfromdate.UniqueID];
                txttodate.Text = hdfToDate.Value;
                txtfromdate.Text = hdfFromDate.Value;
                if ((hdfFromDate.Value != "" && hdfFromDate.Value != null) && (hdfToDate.Value != "" && hdfToDate.Value != null) && hdfTable.Value != "0")
                    btnDelete.Enabled=true;
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    DeleteTable();
                    GlobalUI.MessageBox("Delete Successful ! ");
                    txtfromdate.Text = string.Empty;
                    txttodate.Text = string.Empty;
                    ddlLog.SelectedValue = "0";
                    btnDelete.Enabled = false;
                }
                else
                    btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        
        public void DeleteLogTable(DateTime? fdate, DateTime? tdate, int option)
        {
            try
            {
                SqlConnection connectionString = con;
                SqlCommand cmd = new SqlCommand("SP_DeleteLogTable", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@From_Date", fdate);
                cmd.Parameters.AddWithValue("@To_Date", tdate);
                cmd.Parameters.AddWithValue("@Option", option);
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DateTime DateConverter(string dateTime)
        {
            try
            {
                DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                dtfi.ShortDatePattern = "dd-MM-yyyy";
                dtfi.DateSeparator = "-";
                DateTime objDate = Convert.ToDateTime(dateTime, dtfi);
                string date = Convert.ToDateTime(objDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy");
                objDate = DateTime.ParseExact(date, "MM/dd/yyyy", null);

                return objDate;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DateTime();
            }
        }

        protected void todate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txttodate.Text = String.Empty;
                hdfToDate.Value = Request.Form[txttodate.UniqueID];
                txtfromdate.Text = Request.Form[txtfromdate.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void fromdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtfromdate.Text = String.Empty;
                hdfFromDate.Value = Request.Form[txtfromdate.UniqueID];
                txttodate.Text = Request.Form[txttodate.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public  void DeleteTable()
        {
            try
            {
                DateTime? fdate = null;
                DateTime? tdate = null;

                int option = Convert.ToInt16(ddlLog.SelectedValue);
                string fromDate = Request.Form[txtfromdate.UniqueID];
                string toDate = Request.Form[txttodate.UniqueID];
                hdfFromDate.Value = fromDate;
                hdfToDate.Value = toDate;
                DateTime? FromDate = new DateTime();
                DateTime? ToDate = new DateTime();
                if (!String.IsNullOrEmpty(fromDate))
                {
                    FromDate = DateConverter(fromDate);
                }
                else
                {
                    FromDate = null;
                }
                if (!String.IsNullOrEmpty(toDate))
                {
                    ToDate = DateConverter(toDate);
                }
                else
                {
                    ToDate = null;
                }
                fdate = FromDate;
                tdate = ToDate;
                DeleteLogTable(fdate, tdate, option);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

    }
}