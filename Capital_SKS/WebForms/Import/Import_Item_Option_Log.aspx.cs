/* 
Created By              : Kyaw Thet Paing
Created Date          : 27/11/2014
Updated By             :Kay Thi Aung
Updated Date         :09/01/2015

 Tables using           : Item_ImportLog 
 *                                 -
 *                                 -
 *                                    
 *                                   
 * 
 *                                  
 * Storedprocedure using:
 *                                           -
 *                                           -
 *                                           -
 *                     
*/

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
    public partial class Import_Item_Option_Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Item_ImportLog_BL imlbl = new Item_ImportLog_BL();
                if (Request.QueryString["ErrorLog_ID"] != null)
                {
                    Cache.Remove("dtoption");
                    String logID = Request.QueryString["ErrorLog_ID"].ToString();

                    DataTable dt = imlbl.GetOptionErrorsLog(logID);
                   
                    gvOptionData.DataSource = dt;
                    gvOptionData.DataBind();
                }//for error log

                if (Request.QueryString["Log_ID"] != null)
                {
                    Cache.Remove("dtoption");
                    String logID = Request.QueryString["Log_ID"].ToString();
                  
                    DataTable dt = imlbl.GetOptionLog(logID);
                    Cache.Insert("dtoption", dt, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero);
                    gvOptionData.DataSource = dt;
                    gvOptionData.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvOptionData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = e.Row.FindControl("lblControlID") as Label;
                    if (lbl.Text.Equals("n"))
                        lbl.Text = "新規";
                    else if (lbl.Text.Equals("u"))
                        lbl.Text = "更新";
                    else if (lbl.Text.Equals("d"))
                        lbl.Text = "削除";
                }
            }
            catch (Exception ex)
            {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvOptionData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dtbind = new DataTable();
            if (Request.QueryString["ErrorLog_ID"] != null)
            {
                Item_ImportLog_BL imlbl = new Item_ImportLog_BL();
                String logID = Request.QueryString["ErrorLog_ID"].ToString();
                DataTable dt = imlbl.GetOptionErrorsLog(logID);

                gvOptionData.DataSource = dt;
                gvOptionData.PageIndex = e.NewPageIndex;
                gvOptionData.DataBind();
            }
            if (Cache["dtoption"] != null)
            {
          
                dtbind =Cache["dtoption"] as DataTable;
                   
                gvOptionData.DataSource = dtbind;
                gvOptionData.PageIndex = e.NewPageIndex;
                gvOptionData.DataBind();
            }
           
        }
    }
}