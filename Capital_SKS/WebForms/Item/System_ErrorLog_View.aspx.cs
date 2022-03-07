using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ORS_RCM_BL;
using System.Data.SqlClient;
using System.Web.Services;

namespace ORS_RCM.WebForms.Item
{
    public partial class System_ErrorLog_View : System.Web.UI.Page
    {
        Item_ImportLog_BL itbl;
        protected void Page_Load(object sender, EventArgs e)
        {
             try
            {
            if (!IsPostBack) 
            {
                User_BL ubl = new User_BL();
                DataSet ds = ubl.SelectAll();
                DataTable dtUser = ds.Tables[0];

                ddlUserList.DataSource = dtUser;
                ddlUserList.DataBind();
                ddlUserList.DataTextField = "User_Name";
                ddlUserList.DataValueField = "ID";
                ddlUserList.DataBind();
                ddlUserList.Items.Insert(0, "");

                DataTable dt = SelectAll(1);
                int count = 0;
                if (dt.Rows.Count > 0)
                {
                    count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                }

                gvlog.DataSource = dt;
                gvlog.DataBind();
                gp.CalculatePaging(count, gvlog.PageSize, 1);
            }
            else
            {
                String ctrl = getPostBackControlName();
                if (ctrl.Contains("lnkPaging"))
                {
                    gp.LinkButtonClick(ctrl, gvlog.PageSize);
                    Label lbl = gp.FindControl("lblCurrent") as Label;
                    int index = Convert.ToInt32(lbl.Text);
                    DataTable dt = SelectAll(index);

                    gvlog.DataSource = dt;
                    gvlog.DataBind();                  
                }
            }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public string getPostBackControlName()
        {
            try
            {
                Control control = null;
                //first we will check the "__EVENTTARGET" because if post back made by       the controls
                //which used "_doPostBack" function also available in Request.Form collection.
                string ctrlname = Page.Request.Params["__EVENTTARGET"];
                if (ctrlname != null && ctrlname != String.Empty)
                {
                    control = Page.FindControl(ctrlname);
                }
                // if __EVENTTARGET is null, the control is a button type and we need to
                // iterate over the form collection to find it
                else
                {
                    string ctrlStr = String.Empty;
                    Control c = null;
                    foreach (string ctl in Page.Request.Form)
                    {
                        //handle ImageButton they having an additional "quasi-property" in their Id which identifies
                        //mouse x and y coordinates
                        if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                        {
                            ctrlStr = ctl.Substring(0, ctl.Length - 2);
                            c = Page.FindControl(ctrlStr);
                        }
                        else
                        {
                            c = Page.FindControl(ctl);
                        }
                        if (c is System.Web.UI.WebControls.Button ||
                                 c is System.Web.UI.WebControls.ImageButton)
                        {
                            control = c;
                            break;
                        }
                    }
                }

                if (control != null)
                    return control.ID;
                else return null;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
        }

        protected DataTable SelectAll(int pageIndex)
        {
            try
            {
                itbl = new Item_ImportLog_BL();
                string userid = ddlUserList.SelectedValue;
                string status = ddlStatus.SelectedValue;
                string detail = txtDetail.Text;
                return itbl.SystemlogSelectAll(pageIndex,userid,status,detail);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            }
        }

        [WebMethod]
        public static void UpdateStatus(string id,string status)
        {
            System_ErrorLogView_BL errBl = new System_ErrorLogView_BL();
            errBl.UpdateStatus(id, status);
        }

        protected void gvlog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = e.Row.FindControl("lblStatus") as Label;
                DropDownList ddl = e.Row.FindControl("ddlStatus") as DropDownList;
                ddl.SelectedValue = lbl.Text;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = SelectAll(1);
            gvlog.DataSource = dt;
            gvlog.DataBind();

            int count = 0;
            if (dt.Rows.Count > 0)
            {
                count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
            }
            gp.CalculatePaging(count, gvlog.PageSize, 1);
        }
    }
}