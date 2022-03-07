using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;

namespace ORS_RCM.WebForms.Item
{
    public partial class Item_Option_Select1 : System.Web.UI.Page
    {
        Option_BL optbl;
        DataTable dt = new DataTable();
        public string Item_Code
        {
            get
            {
                if (Request.QueryString["Item_Code"] != null)
                {
                    return Request.QueryString["Item_Code"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindData();

                    int i = 1;
                    foreach (DataListItem item in DataList1.Items)
                    {
                        Label lbl = item.FindControl("lblType") as Label;
                        lbl.Text += i;
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void BindData()
        {
            try
            {
                //DataSet ds = new DataSet();
                //DataTable FromTable = new DataTable();
                optbl = new Option_BL();

                DataTable dt = optbl.Search();
                DataList1.DataSource = dt;
                DataList1.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                optbl = new Option_BL();
                DataTable dt = optbl.Search();
                foreach (DataListItem item in DataList1.Items)
                {
                    RadioButton radioBtn = item.FindControl("rdoOption") as RadioButton;
                    Label lblGroup = (Label)item.FindControl("lblGroup");
                    if (radioBtn.Checked)
                    {

                        //DataTable dtOption = dt.Select("Option_GroupName=" + lblGroup.Text).CopyToDataTable();
                        DataRow[] dr = dt.Select("Option_GroupName='" + lblGroup.Text + "'");
                        if (dr.Count() > 0)
                        {
                            DataTable dtOption = dt.Select("Option_GroupName='" + lblGroup.Text + "'").CopyToDataTable();
                            //dtOption.Columns.Add("ID", typeof(int));
                            //dtOption.Rows[0]["ID"] = 1;
                            Session["Option_"+Item_Code] = dtOption;
                            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack();window.close()", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        //}
        
    }
}