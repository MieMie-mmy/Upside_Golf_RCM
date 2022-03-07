/* 
Created By              : EI PHYO
Created Date          : 20/07/2015
Start_Date              :
End_Date                :

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
using ORS_RCM_BL;
using System.Collections;
using System.Data;
using System.IO;
using System.Configuration;
using ORS_RCM_Common;
using System.Globalization;


namespace ORS_RCM.WebForms
{
    public partial class Sale_List_Screen : System.Web.UI.Page
    {
        Email_ItemOrder_BL  eml;
        Sale_ListScreen_Entity se = new Sale_ListScreen_Entity();
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dt = Bind(0);
                gvSale_list.DataSource = dt;
                gvSale_list.DataBind();
                if (dt != null && dt.Rows.Count > 0)
                {
                    int totalcount = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                    int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                    gp2.CalculatePaging(totalcount, pageSize, 1);

                }
                else
                {
                    gp2.CalculatePaging(0, gvSale_list.PageSize, 1);
                }
            }
            else
            {
                BindDatetime();
                String ctrl = getPostBackControlName();
                if (ctrl.Contains("lnkPaging"))
                {
                    gp2.LinkButtonClick(ctrl, gvSale_list.PageSize);

                    Label lbl = gp2.FindControl("lblCurrent") as Label;
                    int index = Convert.ToInt32(lbl.Text);
                    DataTable dt = Bind(index-1);
                    if (dt != null || dt.Rows.Count > 0)
                    {
                        int totalcount = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                        int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                        gvSale_list.DataSource = dt;
                        gvSale_list.DataBind();
                       // gp2.CalculatePaging(totalcount, pageSize, 1);
                    }
                }
            }
        }

        protected void BindDatetime()
        {
            try
            {
                //Save
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
            eml = new   Email_ItemOrder_BL();
            Sale_ListScreen_Entity saleentity= GetSearchData();
            DataTable dt = eml.SearchSalelist(saleentity, index + 1, ddlpage.SelectedValue);// 0 -- like search
            return dt;
        }

        public Sale_ListScreen_Entity GetSearchData()
        {
            se = new Sale_ListScreen_Entity();
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

            se.fromDate=FromDate;

            se.toDate=ToDate;
          

            se.Store_Name = lstShop.Text;
           
            return se;
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
                      
        protected void btnSearch_Click(object sender, EventArgs e)
       {
           ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "SearchClick();", true);     

            DataTable dt = Bind(0);
            if (dt != null && dt.Rows.Count > 0)
            {
                int totalcount = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                gvSale_list.DataSource = dt;
                gvSale_list.DataBind();
                gp2.CalculatePaging(totalcount, pageSize, 1);
            }
            else
            {
                gvSale_list.DataSource = dt;
                gvSale_list.DataBind();
                gp2.CalculatePaging(0, gvSale_list.PageSize, 1);
            }
            UPanel.Update();
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

  
        protected void gvSale_list_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
             try
            {
                gvSale_list.PageIndex = e.NewPageIndex;
                
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
      

        protected void gvSale_list_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //try
            //{
                if (e.CommandName == "DataEdit")
                {
                    //string shop = e.CommandArgument.ToString();
                   
                        Response.Redirect("ItemSeparated_OrderList.aspx");
                   


                }


            //}
            //catch (Exception ex)
            //{
            //    Session["Exception"] = ex.ToString();
            //    Response.Redirect("~/CustomErrorPage.aspx?");
            //}


        }

       
        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
         {
             try
             {
                 gvSale_list.PageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                 if (gvSale_list.Rows.Count > 0)
                 {
                     DataTable dt = Bind(0);
                     if (dt != null || dt.Rows.Count > 0)
                     {
                         int totalcount = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                         int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                         gvSale_list.DataSource = dt;
                         gvSale_list.DataBind();
                         gp2.CalculatePaging(totalcount, pageSize, 1);
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
    }
}