/* 
Created By              : Eephyo
Created Date          : 19/06/2014
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
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;
using ORS_RCM_BL;

namespace ORS_RCM
{
    public partial class User_View : System.Web.UI.Page
    {

        User_BL ubl;

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
                        if (c is System.Web.UI.WebControls.Button || c is System.Web.UI.WebControls.ImageButton)
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
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["sort"] = "Original";

                //ViewState["currentPage"] = 0;
                ubl = new User_BL();
                DataTable dt = ubl.Search(txtuser.Text.Trim(), txtlogin.Text.Trim(),null);

                gvuser.DataSource = dt;
                gvuser.DataBind();

                gp.CalculatePaging(dt.Rows.Count, gvuser.PageSize, 1);

                ViewState["dt"] = dt;


                //gp.TotalRecord = dt.Rows.Count;
                //gp.OnePageRecord = gvuser.PageSize;

                //int index1 = 0;
                //gp.sendIndexToThePage += delegate(int index)
                //{
                //    index1 = index;
                //};
                //gvuser.PageIndex = index1;

            }

            else
            {
                ubl = new User_BL();
                if (ViewState["dt"] != null)
                {
                    DataTable dt = ViewState["dt"] as DataTable;

                    String ctrl = getPostBackControlName();

                    if (ctrl.Contains("lnkPaging"))
                    {
                        gp.LinkButtonClick(ctrl, gvuser.PageSize);

                        Label lbl = gp.FindControl("lblCurrent") as Label;
                        int index = Convert.ToInt32(lbl.Text);
                        gvuser.PageIndex = index - 1;
                        gvuser.DataSource = dt;
                        gvuser.DataBind();
                    }

                    //gp.TotalRecord = dt.Rows.Count;
                    //gp.OnePageRecord = gvuser.PageSize;

                    //gp.sendIndexToThePage += delegate(int index)
                    //{
                    //    gvuser.PageSize = gp.OnePageRecord;
                    //    gvuser.PageIndex = Convert.ToInt32(index);
                    //    gvuser.DataSource = dt;
                    //    gvuser.DataBind();
                    //};

                }
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ubl = new User_BL();
            DataTable dt = new DataTable();
            if ((!String.IsNullOrWhiteSpace(txtuser.Text) || !String.IsNullOrWhiteSpace(txtlogin.Text)) || (RadioButton3.Checked) || (RadioButton1.Checked) || (RadioButton2.Checked))
            {
                if (RadioButton1.Checked == true)
                {
                    dt = ubl.Search(txtuser.Text.Trim(), txtlogin.Text.Trim(),"1");
                }
                else   if (RadioButton2.Checked == true)
                {
                    dt = ubl.Search(txtuser.Text.Trim(), txtlogin.Text.Trim(), "0");
                }
                else
                    dt = ubl.Search(txtuser.Text.Trim(), txtlogin.Text.Trim(),null);

                ViewState["dt"] = dt;

                gvuser.DataSource = dt;
                gvuser.DataBind();
                gp.CalculatePaging(dt.Rows.Count, gvuser.PageSize, 1);
                
            }



        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserRole.aspx");

        }

        protected void gvuser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ubl = new User_BL();
            DataTable dt = new DataTable();
            if ((((!String.IsNullOrWhiteSpace(txtuser.Text)) || (!String.IsNullOrWhiteSpace(txtlogin.Text)) || (RadioButton3.Checked)) && (e.NewPageIndex > 0)) && (e.NewPageIndex > -1) || (RadioButton1.Checked) && ((e.NewPageIndex > -1)) || e.NewPageIndex > -1)
            {
                if (RadioButton1.Checked == true)
                {
                    dt = ubl.Search(txtuser.Text.Trim(), txtlogin.Text.Trim(), "1");
                }
                else if (RadioButton2.Checked == true)
                {
                    dt = ubl.Search(txtuser.Text.Trim(), txtlogin.Text.Trim(), "0");
                }
                else
                    dt = ubl.Search(txtuser.Text.Trim(), txtlogin.Text.Trim(), null);
              
                
                gvuser.DataSource = dt;
                gvuser.PageIndex = e.NewPageIndex;
                gvuser.DataBind();
                
            }

            //else if (((!String.IsNullOrWhiteSpace(txtuser.Text) || !String.IsNullOrWhiteSpace(txtlogin.Text)) || (RadioButton1.Checked)) && ((e.NewPageIndex > -1)))
            //{
            //    dt = ubl.SelectAllByOne();
            //    gvuser.DataSource = dt;
            //    gvuser.DataBind();
                
            //}
            //else
            //{
            //    if (e.NewPageIndex > -1)
            //    {
            //        dt = ubl.Searchzero(txtuser.Text.Trim(), txtlogin.Text.Trim());
            //        gvuser.DataSource = dt;
            //        gvuser.PageIndex = e.NewPageIndex;
            //        gvuser.DataBind();
                    
            //    }
            //}
        }

        protected void gvuser_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if (e.CommandName == "DataEdit")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/UserRole.aspx?ID=" + ID);
                }
                else 
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/UserRole.aspx?ID=" + ID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void gvuser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "Status").ToString().ToLower() == Convert.ToString(1))
                    {
                        e.Row.Cells[3].Text = "有効 ";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Status").ToString().ToLower() == Convert.ToString(0))
                    {

                        e.Row.Cells[3].Text = "無効";
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

    }

        }
    



