/* 
Created By              : Kay Thi Aung
Created Date          : 18/06/2014
Updated By             :EiPhyo
Updated Date         :

 Tables using: Shop
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
using ORS_RCM;

namespace ORS_RCM
{
    public partial class Shop_View : System.Web.UI.Page
    {
        Shop_Entity sentity;
        Shop_BL shbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["sort"] = "Original";
                shbl = new Shop_BL();
                DataTable dt = shbl.Search(txtShopname.Text.Trim(), ddlmall.SelectedValue);


                gvshop.DataSource = dt;
                gvshop.DataBind();

                gp.CalculatePaging(dt.Rows.Count, gvshop.PageSize, 1);

                ViewState["dt"] = dt;
                             
                FillMalldata();
            }
            else
            {
                shbl = new Shop_BL();
                if (ViewState["dt"] != null)
                {
                    DataTable dt = ViewState["dt"] as DataTable;


                    String ctrl = getPostBackControlName();

                    if (ctrl.Contains("lnkPaging"))
                    {
                        gp.LinkButtonClick(ctrl, gvshop.PageSize);

                        Label lbl = gp.FindControl("lblCurrent") as Label;
                        int index = Convert.ToInt32(lbl.Text);
                        gvshop.PageIndex = index-1;
                        gvshop.DataSource = dt;
                        gvshop.DataBind();
                    }

                    //gp.TotalRecord = dt.Rows.Count;
                    //gp.OnePageRecord = gvshop.PageSize;

                    //gp.sendIndexToThePage += delegate(int index)
                    //{
                    //    gvshop.PageSize = gp.OnePageRecord;
                    //    gvshop.PageIndex = Convert.ToInt32(index);
                    //    gvshop.DataSource = dt;
                    //    gvshop.DataBind();
                    //};
                    //uc1.Rowcount = dt.Rows.Count;

                    //uc1.sendIndexToThePage += delegate(String index)
                    //{
                    //    gvshop.PageIndex = Convert.ToInt32(index);
                    //    gvshop.DataSource = dt;
                    //    gvshop.DataBind();
                    //};
                }
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

        protected void SortedWith(String column_Name)
        {
            if (ViewState["sort"] != null)
            {
                if (ViewState["sort"].ToString().Equals("Original"))
                {
                    shbl = new Shop_BL();
                    DataTable dt = shbl.Search(txtShopname.Text.Trim(), ddlmall.SelectedValue);
                    DataView dv = new DataView(dt);
                    dv.Sort = column_Name + " ASC";
                    dt = dv.ToTable();
                    ViewState["dt"] = dt;
                    //uc1.setDefault();
                    //uc1.populate_pageNo(dt.Rows.Count.ToString());
                    gvshop.PageIndex = 0;
                    gvshop.DataSource = dt;
                    gvshop.DataBind();
                    ViewState["sort"] = "Asc";
                }
                else if (ViewState["sort"].Equals("Asc"))
                {
                    shbl = new Shop_BL();
                    DataTable dt = shbl.Search(txtShopname.Text.Trim(), ddlmall.SelectedValue);
                    DataView dv = new DataView(dt);
                    dv.Sort = column_Name + " DESC";
                    dt = dv.ToTable();
                    ViewState["dt"] = dt;
                    //uc1.setDefault();
                    //uc1.populate_pageNo(dt.Rows.Count.ToString());
                    gvshop.PageIndex = 0;
                    gvshop.DataSource = dt;
                    gvshop.DataBind();
                    ViewState["sort"] = "Desc";
                }
                else if(ViewState["sort"].Equals("Desc"))
                {
                    Search(0);//search and set pageindex to 0
                    ViewState["sort"] = "Original";
                }
            }
        }
       
        protected void Search(int flag)
        {
            shbl = new Shop_BL();
            String index1 = "0";
            DataTable dt = shbl.Search(txtShopname.Text.Trim(), ddlmall.SelectedValue);
            ViewState["dt"] = dt;
            //uc1.sendIndexToThePage += delegate(String index)
            //{
            //    index1 = index;
            //    uc1.Rowcount = dt.Rows.Count;

            //};
            if (flag == 1)
                gvshop.PageIndex = Convert.ToInt32(index1);
            else
            {
                gvshop.PageIndex = Convert.ToInt32(0);
                //uc1.setDefault();
                //uc1.populate_pageNo(dt.Rows.Count.ToString());
            } 

            gvshop.DataSource = shbl.Search(txtShopname.Text.Trim(), ddlmall.SelectedValue);
            gvshop.DataBind();
            gp.CalculatePaging(dt.Rows.Count, gvshop.PageSize, 1);
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            Search(1);
        }

        protected void gvshop_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            shbl = new Shop_BL();
            if(((!String.IsNullOrWhiteSpace(txtShopname.Text) || ddlmall.SelectedIndex != -1))&&(e.NewPageIndex > -1))
            {
                gvshop.DataSource = shbl.Search(txtShopname.Text.Trim(), ddlmall.SelectedValue);
                gvshop.PageIndex = e.NewPageIndex;
                gvshop.DataBind();
            }

            else 
            {
               if(e.NewPageIndex > -1)
               {
                gvshop.DataSource = shbl.Search(null, null);
                gvshop.PageIndex = e.NewPageIndex;
                gvshop.DataBind();
               }
            }
        }

        protected void FillMalldata() 
        {
            try
            {
                GlobalBL gb = new GlobalBL();
                ddlmall.DataSource = gb.Code_Setup(1);
                ddlmall.DataTextField = "Code_Description";
                ddlmall.DataValueField = "ID";
                ddlmall.DataBind();
                ddlmall.Items.Insert(0,"");
               
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        
        
        }

        protected void gvshop_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
         
            shbl = new Shop_BL();
            sentity = new Shop_Entity();
            try
            {
                if (e.CommandName == "DataEdit") 
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("Shop.aspx?Shop_ID=" + ID);
                }
                else if (e.CommandName == "DefaultSetting")
                {
                       int ID = Convert.ToInt32(e.CommandArgument);
                       sentity = shbl.SelectByID(ID);
                       if (sentity.MallOpen == "1")
                       {
                           Response.Redirect("Mall_Setting_Rakhutan_Default.aspx?Shop_ID=" + ID);
                       }
                       else if (sentity.MallOpen == "2")
                       {
                           Response.Redirect("Mall_Setting_Yahoo_Default.aspx?Shop_ID=" + ID);
                       }
                       else if (sentity.MallOpen == "7")
                       {
                           Response.Redirect("Mall_Setting_Ponpare_Default.aspx?Shop_ID=" + ID);
                       }
                       else 
                       {
                        //GlobalUI.MessageBox("Please Select Mall Opening!");
                        Response.Write("<script type=\"text/javascript\">alert('Please Select Mall Opening');</script>");
                       }
                }

                else if (e.CommandName == "Fixedvalue")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    sentity = shbl.SelectByID(ID);
               
                    if (sentity.MallOpen == "1")
                    {
                        Response.Redirect("Mall_Setting_Rakhutan_Fixed.aspx?Shop_ID=" + ID);
                    }
                    else if (sentity.MallOpen == "2")
                    {
                        Response.Redirect("Mall_Setting_Yahoo_Fixed.aspx?Shop_ID=" + ID);
                    }
                    else if (sentity.MallOpen == "3")
                    {
                        Response.Redirect("Mall_Setting_Ponpare_Fixed.aspx?Shop_ID=" + ID);
                    }
                   
                    else
                    {
                        //GlobalUI.MessageBox("Please Select Mall Opening!");
                        Response.Write("<script>alert('Please Select Mall Opening');</script>");
                    }

                }

                else 
                {
                    DataTable dt = shbl.Search(txtShopname.Text.Trim(), ddlmall.SelectedValue);

                    //uc1.Rowcount = dt.Rows.Count;
                    gvshop.DataSource = dt;
                    gvshop.DataBind();

                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }

        }
       
        protected void btnnew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Shop.aspx");
        }

        protected void Sorting(object sender,EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            if (lnk.Text.Equals("ショップ名"))
            {
                SortedWith("Shop_Name");
            }
        }

        protected void gvshop_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[5].Text = "有効 ";
                    //if (DataBinder.Eval(e.Row.DataItem, "Status").ToString().ToLower() == Convert.ToString(1))
                    //{
                    //    e.Row.Cells[5].Text = "有効 ";
                    //}
                    //else if (DataBinder.Eval(e.Row.DataItem, "Status").ToString().ToLower() == Convert.ToString(0))
                    //{

                    //    e.Row.Cells[5].Text = "無効";
                    //}
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }





        }
    }
}