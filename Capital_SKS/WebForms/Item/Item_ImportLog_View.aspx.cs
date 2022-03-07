/* 
Created By              : Kay Thi Aung
Created Date          : 04/07/2014
Updated By             :
Updated Date         :

 Tables using: 
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


namespace ORS_RCM
{
    public partial class Item_ImportLog_View : System.Web.UI.Page
    {
        Item_ImportLog_BL  itbl;
     
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //gvpageindex();
                    DataTable dt = SelectAll();
                    gvimportlog.DataSource = dt;
                    gvimportlog.DataBind();
                    gp.CalculatePaging(dt.Rows.Count, gvimportlog.PageSize, 1);
                }
                else
                {
                    String ctrl = getPostBackControlName();
                    if (ctrl.Contains("lnkPaging") || ctrl.Contains("lnkpageno"))
                    {
                        DataTable dt = SelectAll();

                        gp.LinkButtonClick(ctrl, gvimportlog.PageSize);
                        Label lbl = gp.FindControl("lblCurrent") as Label;
                        int index = Convert.ToInt32(lbl.Text);
                        gvimportlog.PageIndex = index-1;
                        gvimportlog.DataSource = dt;
                        gvimportlog.DataBind();
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
                        if (c is System.Web.UI.WebControls.Button ||
                                 c is System.Web.UI.WebControls.ImageButton)
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
                return String.Empty;
            }
        }

        protected void gvpageindex()
        {
            try
            {
                DataTable dts = SelectAll();
                gp.TotalRecord = dts.Rows.Count;
                gp.OnePageRecord = gvimportlog.PageSize;
                int index1 = 0;
                gp.sendIndexToThePage += delegate(int index)
                {
                    index1 = index;
                };
                gvimportlog.PageIndex = index1;
                gvimportlog.DataSource = dts;
                gvimportlog.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvimportlog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvimportlog.DataSource = SelectAll();
                gvimportlog.PageIndex = e.NewPageIndex;
                gvimportlog.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected DataTable SelectAll() 
        {
            try
            {
                itbl = new Item_ImportLog_BL();
                return itbl.ImportLogSelectAll();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            }
        }

        protected void gvimportlog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType ==  DataControlRowType.DataRow && e.Row.DataItem != null) 
                {
                    if (DataBinder.Eval(e.Row.DataItem, "Import_Type").ToString().ToLower() == Convert.ToString(0)) 
                    {
                    e.Row.Cells[4].Text ="Item_Master";
                    }
                    else if(DataBinder.Eval(e.Row.DataItem, "Import_Type").ToString().ToLower() == Convert.ToString(1)) 
                    {
                    e.Row.Cells[4].Text ="SKU";
                    }
                    else if(DataBinder.Eval(e.Row.DataItem, "Import_Type").ToString().ToLower() == Convert.ToString(2)) 
                    {
                    e.Row.Cells[4].Text ="Inventory";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Import_Type").ToString().ToLower() == Convert.ToString(3))
                    {
                        e.Row.Cells[4].Text = "Item_Category";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Import_Type").ToString().ToLower() == Convert.ToString(4))
                    {
                        e.Row.Cells[4].Text = "Item_Option";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Import_Type").ToString().ToLower() == Convert.ToString(5))
                    {
                        e.Row.Cells[4].Text = "Item_Data";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Import_Type").ToString().ToLower() == Convert.ToString(6))
                    {
                        e.Row.Cells[4].Text = "Smart Template Detail";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Import_Type").ToString().ToLower() == Convert.ToString(7))
                    {
                        e.Row.Cells[4].Text = "Item_Image";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Import_Type").ToString().ToLower() == Convert.ToString(8))
                    {
                        e.Row.Cells[4].Text = "Library_Image";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Import_Type").ToString().ToLower() == Convert.ToString(9))
                    {
                        e.Row.Cells[4].Text = "Monotaro_Item_Master";
                    }

                }
            }
            catch (Exception ex) 
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void lnkmaster_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("../Import/Import_Item.aspx",false);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void lnkcategory_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("../Import/Item_Category_Import.aspx",false);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void lnkdata_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("../Import/Item_Option_Import_New.aspx",false);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvimportlog_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DataEdit")
                {
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    TextBox myTextBox = row.FindControl("MyTextBoxId") as TextBox;
                    Label type = row.FindControl("lblID") as Label;
            
                    string Importtype = type.Text;
                    if (Importtype == "0" || Importtype == "1" || Importtype == "2" || Importtype == "9")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Import_ItemMaster_Log.aspx?Log_ID=" + Log_ID+"&ImportType="+Importtype,false);
                    }

                    else if (Importtype == "3")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Item_Category_Import_Log.aspx?Log_ID=" + Log_ID,false);
                    }
                    else if (Importtype == "4")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Import_Item_Option_Log.aspx?Log_ID=" + Log_ID,false);
                    }
                    else if (Importtype == "5")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Import_Item_Data_Log.aspx?LogID=" + Log_ID, false);
                    }
                    else if (Importtype == "6")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Smart_Template_LogView.aspx?LogID=" + Log_ID, false);
                    }
                    else if (Importtype == "7")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Item_Image_Log.aspx?Log_ID=" + Log_ID, false);
                    }
                    else if (Importtype == "8")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Library_Image_Log.aspx?Log_ID=" + Log_ID, false);
                    }
                }

                else if (e.CommandName == "Datashow")
                {
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    TextBox myTextBox = row.FindControl("MyTextBoxId") as TextBox;
                    Label type = row.FindControl("lblID") as Label;

                    string Importtype = type.Text;
                    if (Importtype == "0" || Importtype == "1" || Importtype == "2" || Importtype == "9")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Import_ItemMaster_Log.aspx?ErrorLog_ID=" + Log_ID + "&ErrorImportType=" + Importtype, false);
                    }

                    else if (Importtype == "3")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Item_Category_Import_Log.aspx?ErrorLog_ID=" + Log_ID, false);
                    }
                    else if (Importtype == "4")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Import_Item_Option_Log.aspx?ErrorLog_ID=" + Log_ID, false);
                    }
                    else if (Importtype == "5")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Import_Item_Data_Log.aspx?ErrorLog_ID=" + Log_ID, false);
                    }
                    else if (Importtype == "6")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Smart_Template_LogView.aspx?ErrorLog_ID=" + Log_ID + "&Recordcount=" + "1", false);
                    }
                    else if (Importtype == "7")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Item_Image_Log.aspx?ErrorLog_ID=" + Log_ID, false);
                    }
                    else if (Importtype == "8")
                    {
                        int Log_ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("../Import/Library_Image_Log.aspx?ErrorLog_ID=" + Log_ID, false);
                    }
                }
            }
            catch (Exception ex) 
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void lnkInfodata_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("../Import/Import_Item_Data.aspx",false);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void lnkSmartTemplateImport_Click(object sender, EventArgs e)
        {
            try
            {
                //Response.Redirect("../Import/SmartTemplate_Improt.aspx", false);
                Response.Redirect("../Import/Import_Template_Detail.aspx", false);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void lnkProductDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("../Import/Import_Product_Directory.aspx", false);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}