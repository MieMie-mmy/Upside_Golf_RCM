using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;

namespace ORS_RCM.WebForms.Import
{
    public partial class Item_Category_Import_Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Item_ImportLog_BL imlbl = new Item_ImportLog_BL();
                    #region For Errorlog
                    if (Request.QueryString["ErrorLog_ID"] != null)
                    {


                        DataTable dt = imlbl.GetCategoryErrorLog(Request.QueryString["ErrorLog_ID"].ToString());
                       
                        gvCategoryData.DataSource = dt;
                        gvCategoryData.DataBind();
                    }
                    #endregion
                    if (Request.QueryString["Log_ID"] != null)
                    {
                        Cache.Remove("dtcat");
                      
                        DataTable dt = imlbl.GetCategoryLog(Request.QueryString["Log_ID"].ToString());
                        Cache.Insert("dtcat", dt, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero);
                        gvCategoryData.DataSource = dt;
                        gvCategoryData.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvCategoryData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[1].Text.Equals("n"))
                        e.Row.Cells[1].Text = "新規";
                    else if (e.Row.Cells[1].Text.Equals("u"))
                        e.Row.Cells[1].Text = "更新";
                    else if (e.Row.Cells[1].Text.Equals("d"))
                        e.Row.Cells[1].Text = "削除";
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvCategoryData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Item_ImportLog_BL imlbl = new Item_ImportLog_BL();
            if (Request.QueryString["ErrorLog_ID"] != null)
            {

                DataTable dt = imlbl.GetCategoryErrorLog(Request.QueryString["ErrorLog_ID"].ToString());

                gvCategoryData.DataSource = dt;
                gvCategoryData.PageIndex = e.NewPageIndex;
                gvCategoryData.DataBind();
            }
          
            if(Cache["dtcat"] != null)
            {
                
                DataTable dt = Cache["dtcat"] as DataTable;
                    
                gvCategoryData.DataSource = dt;
                gvCategoryData.PageIndex = e.NewPageIndex;
                gvCategoryData.DataBind();
            }
        }
    }
}