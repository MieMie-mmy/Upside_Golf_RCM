/*  
     
      Tables using: 
      
    * Item_Master
    * Item_Shop
    
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
using System.Collections;
using System.Web.UI.HtmlControls;

namespace ORS_RCM.WebForms.Promotion
{
    public partial class Product_Search_Popup : System.Web.UI.Page
    {
        Promotion_Point_Entity pime;
        Promotion_Point_BL pbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            if (!IsPostBack) 
            {
                gvitem.DataSource = null;
                gvitem.DataBind();
            }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
              try
            {
            gvitem.DataSource = search();
            gvitem.DataBind();
            }
              catch (Exception ex)
              {
                  Session["Exception"] = ex.ToString();
                  Response.Redirect("~/CustomErrorPage.aspx?");
              }
        }

        protected DataTable search() 
        {
          try
            {
            DataTable dt = new DataTable();
            pbl = new Promotion_Point_BL();
            pime = new Promotion_Point_Entity();
            pime.Itemcode = txtitemcode.Text.Trim();
            pime.Itemname = txtitemname.Text.Trim();
            if (!String.IsNullOrWhiteSpace(ddlshopstatus.SelectedValue.ToString()))
            {
                string str = ddlshopstatus.SelectedValue.ToString();
                if (str.Equals("未掲載")) 
                {
                    pime.Shopnmae = "n";
                }
                else if (str.Equals("掲載中")) 
                {
                     pime.Shopnmae ="u";
                }
                else if (str.Equals("削除"))
                {
                     pime.Shopnmae ="d";
                }
            
            }
            else
            pime.Shopnmae = ddlshopstatus.SelectedValue;
            if (!string.IsNullOrWhiteSpace(txtbrandname.Text.Trim()))
                pime.Brandname = txtbrandname.Text.Trim();
            else
                pime.Brandname = null;
            if (!string.IsNullOrWhiteSpace(txtinstructionno.Text.Trim()))
                pime.InstructionNo = txtinstructionno.Text.Trim();
            else
                pime.InstructionNo = null;
            if (!string.IsNullOrWhiteSpace(txtcompetition.Text.Trim()))
                pime.Competationname = txtcompetition.Text.Trim();
            else
                pime.Competationname = null;
            if (!string.IsNullOrWhiteSpace(txtclassification.Text.Trim()))
                pime.Claffication = txtclassification.Text.Trim();
            else
                pime.Claffication = null;
            if (!string.IsNullOrWhiteSpace(txtyear.Text.Trim()))
                pime.Year = txtyear.Text.Trim();
            else
                pime.Year = null;
            if (!string.IsNullOrWhiteSpace(txtseason.Text.Trim()))
                pime.Season = txtseason.Text.Trim();
            else
                pime.Season = null;

            if (chktype.Checked && !String.IsNullOrWhiteSpace(txtitemcode.Text.Trim()))
            {
                dt = pbl.SelectPopupData(pime,2);
            }
            else

                dt = pbl.SelectPopupData(pime, 1);

            return dt;
            }
                catch (Exception ex)
                {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
                    return new DataTable();
                }
        }

        protected void btnselectall_Click(object sender, EventArgs e)
        {
            try
            {
            CheckChangeGridpage(true);
            ItemCheck_Change();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btncancelall_Click(object sender, EventArgs e)
        {
             try
            {
            CheckChangeGridpage(false);
            ItemCheck_Change();
            }
             catch (Exception ex)
             {
                 Session["Exception"] = ex.ToString();
                 Response.Redirect("~/CustomErrorPage.aspx?");
             }
        }

        protected void ItemCheck_Change()
        {
            try
            {
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    CheckBox chk;
                    Label lbl;
                    for (int i = 0; i < gvitem.Rows.Count; i++)
                    {
                        chk = gvitem.Rows[i].FindControl("chkitem") as CheckBox;
                        lbl = gvitem.Rows[i].FindControl("lblID") as Label;

                        if (arrlst.Contains(lbl.Text))
                            chk.Checked = true;
                        else chk.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void CheckChangeGridpage(Boolean check)
        {
            try
            {
                ArrayList arrCheckValue = new ArrayList(); ArrayList chk = new ArrayList();
                ArrayList arrCheckValueitem = new ArrayList(); 
                if (check == true)
                {

                    if (ViewState["checkedValue"] != null)
                    {
                        chk = ViewState["checkedValue"] as ArrayList;

                        for (int j = 0; j < chk.Count; j++)
                        {
                            arrCheckValue.Add(chk[j].ToString());

                        }

                    }
                    for (int i = 0; i < gvitem.Rows.Count; i++)
                    {
                        Label lblitemcode = gvitem.Rows[i].FindControl("lblitemcode") as Label;
                        Label lbl = gvitem.Rows[i].FindControl("lblID") as Label;

                        CheckBox ckb = gvitem.Rows[i].FindControl("chkitem") as CheckBox;
                        if (ckb.Enabled)
                        {

                            arrCheckValue.Add(lbl.Text);
                            arrCheckValueitem.Add(lblitemcode.Text);
                          
                        }
                    }

                }

                ViewState["checkedValue"] = arrCheckValue;
              ViewState["checkedValueitemcode"] =arrCheckValueitem;
               
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvitem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblshop = (Label)e.Row.FindControl("Label4");
                HtmlGenericControl pWait = e.Row.FindControl("PWait") as HtmlGenericControl;
                HtmlGenericControl pOk = e.Row.FindControl("POk") as HtmlGenericControl;
                HtmlGenericControl pDel = e.Row.FindControl("PDel") as HtmlGenericControl;
                switch (lblshop.Text)
                {
                    case "n":
                        {
                            pWait.Visible = true;
                            pOk.Visible = false;
                            pDel.Visible = false;
                            break;
                        }
                    case "u":
                        {
                            pWait.Visible = false;
                            pOk.Visible = true;
                            pDel.Visible = false;
                            break;
                        }
                    case "d":
                        {
                            pWait.Visible = false;
                            pOk.Visible = false;
                            pDel.Visible = true;
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
        protected void ckItems_Check(object sender, EventArgs e)
        {
            try
            {


       

                CheckBox chk = sender as CheckBox;
                GridViewRow row = chk.NamingContainer as GridViewRow;
                int rowIndex = row.RowIndex;

                Label lbl = gvitem.Rows[rowIndex].FindControl("lblID") as Label;
                Label lblitemcode = gvitem.Rows[rowIndex].FindControl("lblitemcode") as Label;
            
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    ArrayList arrCheckValueitem = ViewState["checkedValueitemcode"] as ArrayList;
                 
                  
                    if (!chk.Checked)
                    {
                        //if one of check box is unchecked then header checkbox set to uncheck
                     
                        if (arrlst.Contains(lbl.Text) && arrCheckValueitem.Contains(lblitemcode.Text))
                        {
                            arrlst.Remove(lbl.Text);
                            arrCheckValueitem.Remove(lblitemcode.Text);
                          
                            ViewState["checkedValue"] = arrlst;
                            ViewState["checkedValueitemcode"] = arrCheckValueitem;
                           
                        }
                    }
                    else
                    {
                        arrlst.Add(lbl.Text);
                        arrCheckValueitem.Add(lblitemcode.Text);
                    
                        ViewState["checkedValue"] = arrlst;
                        ViewState["checkedValueitemcode"] = arrCheckValueitem;
                    
                        ///check all select check box is check && if all check,set header checkbox to checked

                    }
                }
                else
                {
                    ArrayList arrlst = new ArrayList();
                    ArrayList arrCheckValueitem = new ArrayList();
                    ArrayList arrCheckValueshopname = new ArrayList();
                    arrlst.Add(lbl.Text);
                    arrCheckValueitem.Add(lblitemcode.Text);
                  
                    ViewState["checkedValue"] = arrlst;
                    ViewState["checkedValueitemcode"] = arrCheckValueitem;
                 
                }

               
              

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
            if (ViewState["checkedValueitemcode"] != null) 
            {
                Session["ItemCode"] = null;
                DataTable dt = new DataTable();
                dt.Columns.Add("Item_Code", typeof(string));
                ArrayList chk = ViewState["checkedValueitemcode"] as ArrayList;
                for (int y = 0; y < chk.Count; y++)
                {
                    DataRow destRow = dt.NewRow();
                    dt.Rows.Add(destRow);
                    dt.Rows[y]["Item_Code"] = chk[y];
                    
                }

                Session["ItemCode"] = dt;
                Session["btnPopClick"] = "ok";


            }
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack('ItemCode','');window.close()", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvitem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvitem.DataSource = search();
                gvitem.PageIndex = e.NewPageIndex;
                gvitem.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

       
    }
}