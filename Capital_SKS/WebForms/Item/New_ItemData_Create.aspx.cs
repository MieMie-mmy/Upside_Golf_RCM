using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ORS_RCM_BL;

namespace Capital_SKS.WebForms.Item
{
    public partial class New_ItemData_Create : System.Web.UI.Page
    {
        Item_Export_ErrorCheck_BL iee = new Item_Export_ErrorCheck_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    Bind();
                else
                {
                    if (ViewState["dt"] != null)
                    {
                        DataTable dt = ViewState["dt"] as DataTable;
                        String ctrl = getPostBackControlName();
                        if (ctrl.Contains("lnkPaging") || ctrl.Contains("lnkpageno"))
                        {
                            gp.LinkButtonClick(ctrl, gvItem_List.PageSize);

                            Label lbl = gp.FindControl("lblCurrent") as Label;
                            int index = Convert.ToInt32(lbl.Text);
                            //gvItem_List.PageIndex = Convert.ToInt32(index);
                            gvItem_List.PageIndex = index-1;
                            gvItem_List.DataSource = dt;
                            gvItem_List.DataBind();
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

        public string getPostBackControlName()
        {
            try
            {
                Control control = null;
                string ctrlname = Page.Request.Params["__EVENTTARGET"];
                if (ctrlname != null && ctrlname != String.Empty)
                {
                    control = Page.FindControl(ctrlname);
                }
                else
                {
                    string ctrlStr = String.Empty;
                    Control c = null;
                    foreach (string ctl in Page.Request.Form)
                    {
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string item_code = string.Empty;
                string item_name = string.Empty;
                if (!string.IsNullOrWhiteSpace(txtItem_Code.Text.Trim()))
                    item_code = txtItem_Code.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txtItem_Name.Text.Trim()))
                    item_name = txtItem_Name.Text.Trim();
                if (iee.CheckItem_Code(item_code))
                {
                    iee.Insert_Created_ItemData(item_code, item_name);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Process Success');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Duplicate Item_Code');", true);
                txtItem_Code.Text = string.Empty;
                txtItem_Name.Text = string.Empty;
                txtItem_Code.Focus();
                Bind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void Bind()
        {
            try
            {
                DataTable dt = iee.Get_NewCreated_ItemData();
                ViewState["dt"] = dt;
                gvItem_List.DataSource = dt;
                gvItem_List.DataBind();
                int count = dt.Rows.Count;
                gp.CalculatePaging(count, gvItem_List.PageSize, 1);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvItem_List_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvItem_List.EditIndex = e.NewEditIndex;
                gvItem_List.DataSource = ViewState["dt"] as DataTable;
                gvItem_List.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvItem_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowindex = e.RowIndex;
                int id = Convert.ToInt32(((Label)gvItem_List.Rows[rowindex].FindControl("lblID")).Text.ToString());
                iee.Delete_Created_ItemData(id);
                Bind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvItem_List_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowindex = e.RowIndex;
                int id = Convert.ToInt32(((Label)gvItem_List.Rows[rowindex].FindControl("lblID")).Text.ToString());
                string item_code = ((TextBox)gvItem_List.Rows[rowindex].FindControl("txtItem_Code")).Text.ToString();
                string item_name = ((TextBox)gvItem_List.Rows[rowindex].FindControl("txtItem_Name")).Text.ToString();
                if (iee.CheckUpdateItem_Code(item_code, id))
                {
                    iee.Update_Created_ItemData(id, item_code, item_name);
                    gvItem_List.EditIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Update Success');", true);
                }
                else
                {
                    if (iee.CheckItem_Code(item_code))
                    {
                        iee.Update_Created_ItemData(id, item_code, item_name);
                        gvItem_List.EditIndex = -1;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Update Success');", true);
                    }
                    else
                    {
                        ((TextBox)gvItem_List.Rows[rowindex].FindControl("txtItem_Code")).Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Duplicate Item_Code');", true);
                    }
                }
                Bind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvItem_List_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvItem_List.EditIndex = -1;
                gvItem_List.DataSource = ViewState["dt"] as DataTable;
                gvItem_List.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}