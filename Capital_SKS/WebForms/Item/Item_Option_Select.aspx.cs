/* 
Created By              : Kyaw Thet Paing
Created Date          : 03/07/2014
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ORS_RCM
{
    public partial class Item_Option_Select : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ClearValue()", true);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Label"] = txtLabel.Text;
                Session["Option_Value"] = txtOption.Text;
                Session["Option_Choice"] = int.Parse(ddlTheme.SelectedValue.ToString());
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SendValue()", true);
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}