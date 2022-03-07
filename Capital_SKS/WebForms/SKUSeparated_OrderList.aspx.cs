/* 
Created By              : EiPhyo
Created Date          : 30/07/2015
Updated By             :
Updated Date         :Item_Master,Item

 Tables using: 
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
using ORS_RCM_BL;
using ORS_RCM_Common;
using System.Web.Services;
using System.Globalization;

namespace ORS_RCM
{
    public partial class SKUSeparated_OrderList : System.Web.UI.Page
    {
        Email_ItemOrder_BL eml;
        SKUSeparated_OrderList_Entity skuentity = new SKUSeparated_OrderList_Entity();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    BindPerson();
                    DataTable dt = Bind(0);
                    gvSKU_SeparatedOrder.DataSource = dt;
                    gvSKU_SeparatedOrder.DataBind();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int totalcount = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                        int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                        gvSKU_SeparatedOrder.PageSize = pageSize;
                        gp.CalculatePaging(totalcount, pageSize, 1);

                    }
                    else gp.CalculatePaging(0, gvSKU_SeparatedOrder.PageSize, 1);

                }
                else
                {
                    BindDatetime();
                    String ctrl = getPostBackControlName();
                    if (ctrl.Contains("lnkPaging"))
                    {
                        gp.LinkButtonClick(ctrl, gvSKU_SeparatedOrder.PageSize);

                        Label lbl = gp.FindControl("lblCurrent") as Label;
                        int index = Convert.ToInt32(lbl.Text);
                        DataTable dt = Bind(index - 1);
                        if (dt != null || dt.Rows.Count > 0)
                        {
                            int totalcount = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                            int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                            gvSKU_SeparatedOrder.DataSource = dt;
                            gvSKU_SeparatedOrder.DataBind();
                            //gp.CalculatePaging(totalcount, pageSize, 1);
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

        public void BindPerson()
        {
            try
            {
                Item_BL itbl = new Item_BL();

                ddlPerson.DataSource = itbl.bindDDL();
                ddlPerson.DataTextField = "User_Name";
                ddlPerson.DataValueField = "ID";
                ddlPerson.DataBind();
                ddlPerson.Items.Insert(0, "");
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

        protected void BindDatetime()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Request.Form[txtFromDate.UniqueID]))
                    txtFromDate.Text = Request.Form[txtFromDate.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtToDate.UniqueID]))
                    txtToDate.Text = Request.Form[txtToDate.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected DataTable Bind(int index)
        {
            eml = new Email_ItemOrder_BL();
            skuentity = GetSearchData();

            DataTable dt = new DataTable();

            if (chkCode.Checked)
            {
                dt = eml.Search_SKUSeparatedOrderlist(skuentity, index + 1, ddlpage.SelectedValue, 1);//1 -- equal search
                return dt;
            }
            else
                dt = eml.Search_SKUSeparatedOrderlist(skuentity, index + 1, ddlpage.SelectedValue, 0);// 0 -- like search
            return dt;
        }

        public SKUSeparated_OrderList_Entity GetSearchData()
        {
            skuentity = new SKUSeparated_OrderList_Entity();
            string fromDate = Request.Form[txtFromDate.UniqueID];
            string toDate = Request.Form[txtToDate.UniqueID];
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

            skuentity.fromDate = FromDate;
            skuentity.toDate = ToDate;
            skuentity.Item_Code = txtItemCode.Text.Trim();
            skuentity.Brand_Name = txtbrandname.Text.Trim();
            skuentity.Item_Name = txtItem_Name.Text.Trim();
            skuentity.Competition_Name = txtcompetitionname.Text.Trim();
            skuentity.Season = txtseason.Text.Trim();
            skuentity.Year = txtyear.Text.Trim();
            if (string.IsNullOrWhiteSpace(ddlPerson.SelectedValue)) {
                skuentity.Updated_By = -1;
            }
            else
            {
                skuentity.Updated_By = Convert.ToInt32(ddlPerson.SelectedValue);
            }
            string shop = string.Empty;
            for (int i = 0; i < lstShop.Items.Count; i++)
            {
                if (lstShop.Items[i].Selected)
                {
                    if (String.IsNullOrWhiteSpace(shop))
                        shop = lstShop.Items[i].Value;
                    else
                        shop = shop + "," + lstShop.Items[i].Value;
                }
            }
            skuentity.Shop = shop;
            return skuentity;
        }

        private DateTime DateConverter(string dateTime)
        {
            try
            {
                DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                dtfi.ShortDatePattern = "dd-MM-yyyy";
                dtfi.DateSeparator = "-";
                DateTime objDate = Convert.ToDateTime(dateTime, dtfi);
                string date = Convert.ToDateTime(objDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy hh:MM:ss");
                objDate = DateTime.ParseExact(date, "MM/dd/yyyy hh:MM:ss", null);
                return objDate;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DateTime();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "SearchClick();", true);
                DataTable dt = Bind(0);
                gvSKU_SeparatedOrder.DataSource = dt;
                gvSKU_SeparatedOrder.DataBind();
                if (dt != null && dt.Rows.Count > 0)
                {
                    int totalcount = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                    int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                    gp.CalculatePaging(totalcount, pageSize, 1);
                }
                else
                {
                    gp.CalculatePaging(0, gvSKU_SeparatedOrder.PageSize, 1);
                }
                UPanel.Update();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvSKU_SeparatedOrder.PageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                if (gvSKU_SeparatedOrder.Rows.Count > 0)
                {
                    DataTable dt = Bind(0);
                    if (dt != null || dt.Rows.Count > 0)
                    {
                        int totalcount = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                        int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                        gvSKU_SeparatedOrder.DataSource = dt;
                        gvSKU_SeparatedOrder.DataBind();
                        gp.CalculatePaging(totalcount, pageSize, 1);
                    }
                }
                UPanel.Update();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtFromDate.Text = String.Empty;
                hdfFromDate.Value = Request.Form[txtFromDate.UniqueID];
                txtToDate.Text = Request.Form[txtToDate.UniqueID];
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
                txtToDate.Text = String.Empty;
                hdfToDate.Value = Request.Form[txtToDate.UniqueID];
                txtFromDate.Text = Request.Form[txtFromDate.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvSKU_SeparatedOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DataEdit")
                {
                    string Item_Code = e.CommandArgument.ToString();
                    Response.Redirect("Item/Item_Master.aspx?Item_Code=" + Item_Code, false);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        protected void gvSKU_SeparatedOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvSKU_SeparatedOrder.PageIndex = e.NewPageIndex;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}