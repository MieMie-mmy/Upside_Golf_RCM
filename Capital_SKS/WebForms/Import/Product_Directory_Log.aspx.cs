using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;

namespace Capital_SKS.WebForms.Import
{
    public partial class Product_Directory_Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Item_ImportLog_BL imlbl = new Item_ImportLog_BL();
                    #region For Errorlog
                    //if (Request.QueryString["ErrorLog_ID"] != null)
                    //{


                    //    DataTable dt = imlbl.GetCategoryErrorLog(Request.QueryString["ErrorLog_ID"].ToString());

                    //    gdproduct.DataSource = dt;
                    //    gdproduct.DataBind();
                    //}
                    #endregion
                    if (Request.QueryString["Log_ID"] != null)
                    {
                        Cache.Remove("dtproduct");

                        DataTable dt = imlbl.GetProductLog(Request.QueryString["Log_ID"].ToString());
                        Cache.Insert("dtproduct", dt, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero);
                        gdproduct.DataSource = dt;
                        gdproduct.DataBind();
                    }
                }
            }
            catch(Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gdproduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Item_ImportLog_BL imlbl = new Item_ImportLog_BL();
            //if (Request.QueryString["ErrorLog_ID"] != null)
            //{

            //    DataTable dt = imlbl.GetCategoryErrorLog(Request.QueryString["ErrorLog_ID"].ToString());

            //    gvCategoryData.DataSource = dt;
            //    gvCategoryData.PageIndex = e.NewPageIndex;
            //    gvCategoryData.DataBind();
            //}

            if (Cache["dtproduct"] != null)
            {

                DataTable dt = Cache["dtproduct"] as DataTable;

                gdproduct.DataSource = dt;
                gdproduct.PageIndex = e.NewPageIndex;
                gdproduct.DataBind();
            }
        }
    }
}