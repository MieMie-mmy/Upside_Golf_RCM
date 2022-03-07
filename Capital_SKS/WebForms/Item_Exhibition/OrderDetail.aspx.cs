using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ORS_RCM.WebForms.Item_Exhibition
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtdate.Text = String.Empty;
                hdfFromDate.Value = Request.Form[txtdate.UniqueID];
                txtdateapproval.Text = Request.Form[txtdateapproval.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtdateapproval.Text = String.Empty;
                hdfToDate.Value = Request.Form[txtdateapproval.UniqueID];
                txtdate.Text = Request.Form[txtdate.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

    }
}