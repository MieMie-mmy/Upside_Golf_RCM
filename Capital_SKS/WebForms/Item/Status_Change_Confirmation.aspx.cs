using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;
using ORS_RCM.WebForms.Item;
using System.IO;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace ORS_RCM.WebForms.Item
{
    public partial class Status_Change_Confirmation : System.Web.UI.Page
    {
        public int User_ID
        {
            get
            {
                if (Session["User_ID"] != null)
                {
                    return Convert.ToInt32(Session["User_ID"]);
                }
                else
                {
                    return 0;
                }
            }
        }
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        string list;
        Item_Master_BL imbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                     if (Request.QueryString["IDlist"] != null && Request.QueryString["rdoStatus"] != null)
                    {
                        int rdostatus = Convert.ToInt32(Request.QueryString["rdoStatus"]);
                        if (rdostatus == 1)
                        {
                            exb.Visible = true;
                        }
                        else
                        {
                            deleteexb.Visible = true;
                        }
                        imbl = new Item_Master_BL();
                        list = null;
                        list = Session["ItemIDList"].ToString();
                        DataTable dt = imbl.SelectForExportStatusChange(list);
                        gvlist.DataSource = dt;
                        gvlist.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
                }
            }
        }

        protected void gvlist_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = e.Row.FindControl("lblSKUStatus") as Label;
                    HtmlGenericControl Ppage = e.Row.FindControl("Ppage") as HtmlGenericControl;
                    HtmlGenericControl PWaitSt = e.Row.FindControl("PWaitSt") as HtmlGenericControl;
                    HtmlGenericControl PWaitL = e.Row.FindControl("PWaitL") as HtmlGenericControl;
                    HtmlGenericControl PExhibit1 = e.Row.FindControl("PExhibit") as HtmlGenericControl;
                    HtmlGenericControl POkSt = e.Row.FindControl("POkSt") as HtmlGenericControl;
                    switch (lbl.Text)
                    {
                        case "1":
                            {
                                Ppage.Visible = true;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = false;
                                POkSt.Visible = false;
                                PExhibit1.Visible = false;
                                break;
                            }
                        case "3":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = true;
                                PWaitL.Visible = false;
                                POkSt.Visible = false;
                                PExhibit1.Visible = false;
                                break;
                            }
                        case "2":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = true;
                                POkSt.Visible = false;
                                PExhibit1.Visible = false;
                                break;
                            }
                        case "4":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = false;
                                POkSt.Visible = true;
                                PExhibit1.Visible = false;
                                break;
                            }
                        case "5":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = false;
                                POkSt.Visible = false;
                                PExhibit1.Visible = true;
                                break;
                            }
                    }
                    lbl = e.Row.FindControl("lblshop") as Label;
                    HtmlGenericControl pWait = e.Row.FindControl("PWait") as HtmlGenericControl;
                    HtmlGenericControl pOk = e.Row.FindControl("POk") as HtmlGenericControl;
                    HtmlGenericControl pDel = e.Row.FindControl("PDel") as HtmlGenericControl;
                    HtmlGenericControl pInactive = e.Row.FindControl("PInactive") as HtmlGenericControl;
                    switch (lbl.Text)
                    {
                        case "n":
                            {
                                pWait.Visible = true;
                                pOk.Visible = false;
                                pDel.Visible = false;
                                pInactive.Visible = false;
                                break;
                            }
                        case "u":
                            {
                                pWait.Visible = false;
                                pOk.Visible = true;
                                pDel.Visible = false;
                                pInactive.Visible = false;
                                break;
                            }
                        case "d":
                            {
                                pWait.Visible = false;
                                pOk.Visible = false;
                                pDel.Visible = true;
                                pInactive.Visible = false;
                                break;
                            }
                        case "g":
                            {
                                pWait.Visible = false;
                                pOk.Visible = false;
                                pDel.Visible = false;
                                pInactive.Visible = true;
                                break;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                imbl = new Item_Master_BL();
                if (Request.QueryString["IDlist"] != null && Request.QueryString["rdoStatus"] != null)
                {
                    int rdostatus = Convert.ToInt32(Request.QueryString["rdoStatus"]);
                    list = Session["ItemIDList"].ToString();
                    imbl.ChangeExportStatus(list, rdostatus);
                    string url = "../Item/Item_Status_Change.aspx?list=" + 1;
                    Response.Redirect(url);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
                }
            }
        }
    }
}