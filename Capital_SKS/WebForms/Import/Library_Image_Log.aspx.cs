using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Import
{
    public partial class Library_Image_Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["ErrorLog_ID"] != null)
                    {
                        Import_Item_Data_BL itbl = new Import_Item_Data_BL();
                        int logid = Convert.ToInt32(Request.QueryString["ErrorLog_ID"]);
                        gvimg.DataSource = itbl.SelectErrorLibraryimagelogdata(logid);
                        gvimg.DataBind();
                    }
                    if (Request.QueryString["Log_ID"] != null)
                    {
                        Import_Item_Data_BL itbl = new Import_Item_Data_BL();
                        int logid = Convert.ToInt32(Request.QueryString["Log_ID"]);
                        gvimg.DataSource = itbl.SelectLibraryimagedata(logid);
                        gvimg.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
                }
            }

        }

        protected void gvimg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
              try
            {
                Import_Item_Data_BL itbl = new Import_Item_Data_BL();
                if (Request.QueryString["ErrorLog_ID"] != null)
                {
                    int logid = Convert.ToInt32(Request.QueryString["ErrorLog_ID"]);
                    gvimg.DataSource = itbl.SelectErrorLibraryimagelogdata(logid);
                    gvimg.PageIndex = e.NewPageIndex;
                    gvimg.DataBind();
                }
                if (Request.QueryString["Log_ID"] != null)
                {
                    int logid = Convert.ToInt32(Request.QueryString["Log_ID"]);
                    gvimg.DataSource = itbl.SelectLibraryimagedata(logid);
                    gvimg.PageIndex = e.NewPageIndex;
                    gvimg.DataBind();
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
