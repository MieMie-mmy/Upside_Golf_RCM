/* 
Created By              : Aung Kyaw
Created Date          : 03/07/2014
Updated By             :
Updated Date         :

 Tables using: Shop,Email_ItemOrder
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
using Upside_Golf_RCM_BL;
using System.Globalization;

namespace Upside_Golf_RCM
{
    public partial class Email_ItemOrder_View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtItemNumber.Attributes.Add("onKeyPress", "doClick('" + btnSearch.ClientID + "',event)");
            Email_ItemOrder_BL emailBL= new Email_ItemOrder_BL();
            try
          {
            if (!IsPostBack)
            {
                hdfSearch.Value = "false";
                BindGridEmailItem();
                ddlShopName.DataSource = emailBL.SelectShop();
                ddlShopName.DataTextField = "Shop_EmailName";
                ddlShopName.DataValueField = "Shop_EmailName";
                ddlShopName.DataBind();
                ddlShopName.Items.Insert(0,"");
            }
            else 
            {
                String ctrl = getPostBackControlName();
                if (ctrl.Contains("lnkPaging"))
                {
                    DataTable dt = Search(ddlShopName.SelectedValue, txtItemNumber.Text, Request.Form[txtFromDate.UniqueID], Request.Form[txtToDate.UniqueID]);
                    gp.LinkButtonClick(ctrl, gvEmailItem.PageSize);
                    Label lbl = gp.FindControl("lblCurrent") as Label;
                    int index = Convert.ToInt32(lbl.Text);
                    gvEmailItem.PageIndex = index - 1;
                    gvEmailItem.DataSource = dt;
                    gvEmailItem.DataBind();
                }
            }
          }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }  
        }

        //Delete txtFromDate
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

        //Delete txtToDate
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           try
          {
              DataTable dt = new DataTable();
              if (ddlShopName.SelectedValue == "ラケットプラザアマゾン")
              {
                  dt = Search("Amazon", txtItemNumber.Text, Request.Form[txtFromDate.UniqueID], Request.Form[txtToDate.UniqueID]);
              }
              else
              {
                  dt = Search(ddlShopName.SelectedValue, txtItemNumber.Text, Request.Form[txtFromDate.UniqueID], Request.Form[txtToDate.UniqueID]);
              }
            
            gvEmailItem.DataSource = dt;
            gvEmailItem.DataBind();
            gp.CalculatePaging(dt.Rows.Count, gvEmailItem.PageSize, 1);
            hdfSearch.Value = "true";
          }
           catch (Exception ex)
           {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
           }  
        }

        protected void gvEmailItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvEmailItem.PageIndex = e.NewPageIndex;
                BindGridEmailItem();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvEmailItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DataEdit")
                {
                    string Item_Code = e.CommandArgument.ToString();
                    if (!string.IsNullOrWhiteSpace(Item_Code))
                    {
                        if (Item_Code.Length > 13)
                        {
                            Item_Code = Item_Code.Substring(0, Item_Code.Length - 8);
                        }
                        Response.Redirect("../Item/Item_Master.aspx?Item_Code=" + Item_Code, false);
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

        private void BindGridEmailItem()
        {
            try
            {
                if (bool.Parse(hdfSearch.Value))
                {
                    DataTable dt = Search(ddlShopName.SelectedValue, txtItemNumber.Text, Request.Form[txtFromDate.UniqueID], Request.Form[txtToDate.UniqueID]);
                    gvEmailItem.DataSource = dt;
                    gvEmailItem.DataBind();
                    gp.CalculatePaging(dt.Rows.Count, gvEmailItem.PageSize, 1);
                }
                else
                {
                    Email_ItemOrder_BL emailBL = new Email_ItemOrder_BL();
                    DataTable dt = emailBL.SelectAll();
                    gvEmailItem.DataSource = dt;
                    gvEmailItem.DataBind();
                    gp.CalculatePaging(dt.Rows.Count, gvEmailItem.PageSize, 1);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        private DataTable Search(string shopName,string itemNumber,string fromDate,string toDate)
        {
            try
            {
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
                Email_ItemOrder_BL emailBL = new Email_ItemOrder_BL();
                DataTable dt = null;

                if (shopName.Contains("ラケットプラザアマゾン"))
                {
                    shopName = "Amazon";
                }
                    if (chkCode.Checked)
                    {
                        dt = emailBL.SearchItem(shopName, itemNumber, FromDate, ToDate, 2);
                    }
                    else
                    {
                        dt = emailBL.SearchItem(shopName, itemNumber, FromDate, ToDate, 1);
                    }

                    txtFromDate.Text = hdfFromDate.Value;
                    txtToDate.Text = hdfToDate.Value;
                    hdfFromDate.Value = String.Empty;
                    hdfToDate.Value = String.Empty;
                    return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
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
    }
}