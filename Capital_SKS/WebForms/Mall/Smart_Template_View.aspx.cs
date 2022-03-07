/* 
Created By              : K Thi Aung
Created Date          : 06/08/2014
Updated By             :
Updated Date         :

 Tables using: Smart_Template/Shop
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
    public partial class Smart_Template_View : System.Web.UI.Page
    {
        Smart_Template_BL sbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ViewState["sort"] = "Original";
                sbl = new Smart_Template_BL();
                DataTable dt = sbl.GetTextbox();

                gvSmt.DataSource = dt;
                gvSmt.DataBind();

                gp.CalculatePaging(dt.Rows.Count, gvSmt.PageSize, 1);

                ViewState["dt"] = dt;

                //gp.TotalRecord = dt.Rows.Count;
                //gp.OnePageRecord = gvSmt.PageSize;


                //int index1 = 0;
                //gp.sendIndexToThePage += delegate(int index)
                //{
                //    index1 = index;
                //};


                //gvSmt.PageIndex = index1;

                //gvSmt.DataSource = dt;
                //gvSmt.DataBind();

            }
            else
            {
                if (ViewState["dt"] != null)
                {
                    DataTable dt = ViewState["dt"] as DataTable;

                    String ctrl = getPostBackControlName();

                    if (ctrl.Contains("lnkPaging"))
                    {
                        gp.LinkButtonClick(ctrl, gvSmt.PageSize);

                        Label lbl = gp.FindControl("lblCurrent") as Label;
                        int index = Convert.ToInt32(lbl.Text);
                        gvSmt.PageIndex = index - 1;
                        gvSmt.DataSource = dt;
                        gvSmt.DataBind();
                    }

                    //gp.TotalRecord = dt.Rows.Count;
                    //gp.OnePageRecord = gvSmt.PageSize;

                    //gp.sendIndexToThePage += delegate(int index)
                    //{
                    //    gvSmt.PageSize = gp.OnePageRecord;
                    //    gvSmt.PageIndex = Convert.ToInt32(index);
                    //    gvSmt.DataSource = dt;
                    //    gvSmt.DataBind();
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

        protected void btnnew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Smart_Template_Entry.aspx");
        }

        protected void gvSmt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DataEdit") 
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("Smart_Template_Entry.aspx?ID="+ID);
       


            }
        }

        protected void SortedWith(String column_Name)
        {
            if (ViewState["sort"] != null)
            {
                if (ViewState["sort"].ToString().Equals("Original"))
                {
                    sbl = new Smart_Template_BL();
                    DataTable dt = sbl.GetTextbox();
                    DataView dv = new DataView(dt);
                    dv.Sort = column_Name + " ASC";
                    dt = dv.ToTable();
                    ViewState["dt"] = dt;
                    //uc1.setDefault();
                    //uc1.populate_pageNo(dt.Rows.Count.ToString());
                    gvSmt.PageIndex = 0;
                    gvSmt.DataSource = dt;
                    gvSmt.DataBind();
                    ViewState["sort"] = "Asc";
                }
                else if (ViewState["sort"].Equals("Asc"))
                {
                    sbl = new Smart_Template_BL();
                    DataTable dt = sbl.GetTextbox();
                    DataView dv = new DataView(dt);
                    dv.Sort = column_Name + " DESC";
                    dt = dv.ToTable();
                    ViewState["dt"] = dt;
                    //uc1.setDefault();
                    //uc1.populate_pageNo(dt.Rows.Count.ToString());
                    gvSmt.PageIndex = 0;
                    gvSmt.DataSource = dt;
                    gvSmt.DataBind();
                    ViewState["sort"] = "Desc";
                }
              
            }
        }
       
        protected void gvSmt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) 
            {
                if (DataBinder.Eval(e.Row.DataItem, "Status").ToString().ToLower() == Convert.ToString(1))
                {
                    e.Row.Cells[6].Text = "有効";
                }
                else 
                {
                    e.Row.Cells[6].Text = "無効";
                }
            }
        }

        protected void gvSmt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

          
           
         
               if(e.NewPageIndex > -1)
               {
                   Smart_Template_BL sbl = new Smart_Template_BL();



                gvSmt.DataSource = sbl.GetTextbox();
                gvSmt.PageIndex = e.NewPageIndex;
                gvSmt.DataBind();
               }





        }

      
    }
}