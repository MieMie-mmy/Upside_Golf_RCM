/* 
*
Created By              :Pyae Phyo Khine
Created Date          :  /04/2015
Updated By             :
Updated Date         :

 Tables using: Mail_Magzine
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
using ORS_RCM_BL;
using ORS_RCM_Common;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace ORS_RCM.WebForms.Promotion
{
    public partial class Email_Magazine_List : System.Web.UI.Page
    {
        /*  
 
      Tables using:   
    *Mail_Magazine
    * 
     */  


        Email_Magazine_BL eml;
        Email_Magazine_Entity eme;
        Shop_BL shbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["sort"] = "Original";
                    eml = new Email_Magazine_BL();
                    eme = new Email_Magazine_Entity();
                    //DataTable dt = eml.SelectAll( );
                    eme = GetData();
                    int page_size = Convert.ToInt32(ddlpage.SelectedValue.ToString());
                    DataTable dt = eml.Search(eme, 1, page_size);
                    GetShop();
                    if (dt.Rows.Count > 0)
                    {
                        gvEmailMagazine.DataSource = dt;
                        gvEmailMagazine.DataBind();

                    }
                    else
                    {
                        gvEmailMagazine.DataSource = dt;
                        gvEmailMagazine.DataBind();
                    }
                    gp.CalculatePaging(dt.Rows.Count, gvEmailMagazine.PageSize, 1);
                    ViewState["dt"] = dt;
                 
                }
                else
                {
                    eme = new Email_Magazine_Entity();
                    string name = getPostBackControlName();
                    if (name == "btnSearch")
                    {
            
                    }
                        if (ViewState["dt"] != null)
                        {
                            DataTable dt = ViewState["dt"] as DataTable;
                            String ctrl = getPostBackControlName();
                            if (ctrl.Contains("lnkPaging"))
                            {
                                gp.LinkButtonClick(ctrl, gvEmailMagazine.PageSize);
                                Label lbl = gp.FindControl("lblCurrent") as Label;
                                int index = Convert.ToInt32(lbl.Text);
                                gvEmailMagazine.PageIndex = index - 1;
                                gvEmailMagazine.DataSource = dt;
                                gvEmailMagazine.DataBind();
                            }
                            BindDatetime();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
         
        }

        protected void BindDatetime()
        {
            try
            {
                //Save
                if (!string.IsNullOrWhiteSpace(Request.Form[txtDeliveryDate.UniqueID]))
                    txtDeliveryDate.Text = Request.Form[txtDeliveryDate.UniqueID];
             
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

        public void GetShop()
        {
            try
            {
                shbl = new Shop_BL();
                DataTable dt = shbl.SelectShopAndMall();
                lstTarget_Shop.DataSource = dt;
                lstTarget_Shop.DataValueField = "ID";
                lstTarget_Shop.DataTextField = "Shop_Name";
                lstTarget_Shop.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected Email_Magazine_Entity GetData()
        {
            try
            {
                string shopnames = null;
                Email_Magazine_Entity eme = new Email_Magazine_Entity();
                DataTable dt = new DataTable();
                if (!String.IsNullOrWhiteSpace(txtMagazineID.Text.Trim()))
                    eme.Mail_Magazine_ID = txtMagazineID.Text.Trim();
            
                eme.Mail_Magazine_Name = txtMagazineName.Text.TrimEnd(',').Trim();
                string fromDate = Request.Form[txtDeliveryDate.UniqueID];
                DateTime? FromDate = new DateTime();
                if (!String.IsNullOrEmpty(fromDate))
                {
                    FromDate = DateConverter(fromDate);
                }
                else
                {
                    FromDate = null;
                }
                eme.Delivery_Date = FromDate;
               // eme.Delivery_Date = Convert.ToDateTime(txtDeliveryDate.Text.Trim());
                eme.Campaign = txtCampaignID.Text.Trim();
                for (int i = 0; i < lstTarget_Shop.Items.Count; i++)
                {
                    if (lstTarget_Shop.Items[i].Selected)
                    {
                        //dtshop.Rows.Add(promotionID, int.Parse(listRshop.Items[i].Value.ToString()));
                        shopnames += lstTarget_Shop.Items[i].Value.ToString() + ","; 
                    }
                }
                eme.Shopname = shopnames;
                return eme;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new Email_Magazine_Entity();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                AllSearchData();
                //Email_Magazine_BL eml = new Email_Magazine_BL();
                //Email_Magazine_Entity eme = GetData();
                //int page_size = Convert.ToInt32(ddlpage.SelectedValue.ToString());
                //if (((!String.IsNullOrWhiteSpace(txtMagazineID.Text) || !String.IsNullOrWhiteSpace(txtMagazineName.Text) || !String.IsNullOrWhiteSpace(txtDeliveryDate.Text) || !String.IsNullOrWhiteSpace(txtCampaignID.Text) || lstTarget_Shop.SelectedIndex != -1)))
                //{
                //    DataTable dt = eml.Search(eme,1,page_size);
                //    ViewState["dt"] = dt;
                //    gvEmailMagazine.DataSource = dt;
                //    gvEmailMagazine.DataBind();
                //    gp.CalculatePaging(dt.Rows.Count, gvEmailMagazine.PageSize, 1);
                //    hdfSearch.Value = "true";
                //}
                //else
                //{
                //    DataTable dt = eml.Search(eme, 1, page_size);
                //    ViewState["dt"] = dt;
                //    gvEmailMagazine.DataSource = dt;
                //    gvEmailMagazine.DataBind();
                //    gp.CalculatePaging(dt.Rows.Count, gvEmailMagazine.PageSize, 1);
                //    hdfSearch.Value = "true";
                //}
            }

            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

         protected void AllSearchData()
        {
            try
            {
                Email_Magazine_BL eml = new Email_Magazine_BL();
                Email_Magazine_Entity eme = GetData();
                int page_size = Convert.ToInt32(ddlpage.SelectedValue.ToString());
                if (((!String.IsNullOrWhiteSpace(txtMagazineID.Text) || !String.IsNullOrWhiteSpace(txtMagazineName.Text) || !String.IsNullOrWhiteSpace(txtDeliveryDate.Text) || !String.IsNullOrWhiteSpace(txtCampaignID.Text) || lstTarget_Shop.SelectedIndex != -1)))
                {
                    DataTable dt = eml.Search(eme, 1, page_size);
                    ViewState["dt"] = dt;
                    gvEmailMagazine.DataSource = dt;
                    gvEmailMagazine.DataBind();
                    gp.CalculatePaging(dt.Rows.Count, gvEmailMagazine.PageSize, 1);
                    hdfSearch.Value = "true";
                }
                else
                {
                    DataTable dt = eml.Search(eme, 1, page_size);
                    ViewState["dt"] = dt;
                    gvEmailMagazine.DataSource = dt;
                    gvEmailMagazine.DataBind();
                    gp.CalculatePaging(dt.Rows.Count, gvEmailMagazine.PageSize, 1);
                    hdfSearch.Value = "true";
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvEmailMagazine_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                eml = new Email_Magazine_BL();
                int page_size = Convert.ToInt32(ddlpage.SelectedValue.ToString());
                DataTable dt = new DataTable();
                if (((!String.IsNullOrWhiteSpace(txtMagazineID.Text)) || (!String.IsNullOrWhiteSpace(txtMagazineName.Text)) || (!String.IsNullOrWhiteSpace(txtDeliveryDate.Text)) || (!String.IsNullOrWhiteSpace(txtCampaignID.Text)) || ((lstTarget_Shop.SelectedIndex != -1) && (e.NewPageIndex > -1))))
                {
                    dt = eml.Search(eme, 1, page_size);
                    gvEmailMagazine.DataSource = dt;
                    gvEmailMagazine.PageIndex = e.NewPageIndex;
                    gvEmailMagazine.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
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

        protected void gvEmailMagazine_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                eml = new Email_Magazine_BL();
                eme = new Email_Magazine_Entity();
                if (e.CommandName == "DataEdit")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    Session["ID"] = ID;
                    Response.Redirect("../Promotion/Email_Magazine_Entry.aspx?ID=" + ID , false);

                }
                else if (e.CommandName == "DataDisplay")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    //eme = eml.SelectByID(ID);
                    Response.Redirect("../Promotion/Email_Magazine_Preview.aspx?PID=" + ID, false);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvEmailMagazine_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = e.Row.FindControl("lblCampaign") as Label;
                    String[] str = lbl.Text.Split(',');
                    
                    String temp = lbl.Text;
                    temp = String.Empty;   

                    for (int i = 0; i < str.Length; i++)
                    {
                        if (!String.IsNullOrWhiteSpace(str[i]))
                        {
                            if (String.IsNullOrWhiteSpace(temp))
                                temp = str[i];
                            else
                                temp += "," + str[i];
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(temp))
                        lbl.Text = temp;

                    lbl = e.Row.FindControl("lblShopName") as Label;
                    if (!String.IsNullOrWhiteSpace(lbl.Text))
                    { 
                        Shop_BL sbl = new Shop_BL();
                        Shop_Entity se = sbl.SelectByID(Convert.ToInt32(lbl.Text));
                        lbl.Text = se.ShopName;
                    }
                }

                foreach (TableCell tc in e.Row.Cells)
                {
                    tc.Attributes["style"] = "border-color: #c3cecc";
                }
                // when mouse is over the row, save original color to new attribute, and change it to highlight color
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#CEF0EC'");

                // when mouse leaves the row, change the bg color to its original value  
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Email_Magazine_Entry.aspx?ID=" + 0);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtDeliveryDate.Text = String.Empty;
                hdfDeliveryDate.Value = Request.Form[txtDeliveryDate.UniqueID];
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
                gvEmailMagazine.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
                if (gvEmailMagazine.Rows.Count > 0)
                {
                    AllSearchData();
                }
                else
                {
                    int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                    gp.CalculatePaging(0, pageSize, 1);
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}