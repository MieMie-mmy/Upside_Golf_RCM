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

namespace ORS_RCM.WebForms.Item
{
    public partial class Item_ExportField : System.Web.UI.Page
    {
        Item_ExportField_BL itbl;
        Item_ExportField_Entity itentity; public string str;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    //   ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                    itbl = new Item_ExportField_BL();
                    itentity = new Item_ExportField_Entity();

                    string ID = hfID.Value.ToString();

                    dtlist.DataSource = itbl.SelectAllData(ID);
                    dtlist.DataBind();

                    dtlist.DataSource = itbl.SelectAll();
                    dtlist.DataBind();
                    head.InnerText = "商品情報エクスポート定義";

                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }         
        }
         
        public void Confirm() 
        {
            try
            {
                head.InnerText = "商品情報エクスポート定義確認";
                txtname.Visible = false;
                lbname.Visible = true;
                lbname.Text = txtname.Text;
                txtfield.Visible = false;
                lblfield.Visible = true;
                lblfield.Text = txtfield.Text;
                chkstatus.Enabled = false;
                if (String.IsNullOrWhiteSpace(txtname.Text.Trim()) && String.IsNullOrWhiteSpace(txtfield.Text.Trim()))
                {
                    chkstatus.Visible = false;
                }

                btnConfirm_Save.Text = "登録";
                for (int i = 0; i < dtlist.Items.Count; i++)
                {
                    TextBox txt = dtlist.Items[i].FindControl("txtexpname") as TextBox;
                    txt.Visible = false;
                    Label lbexpname = dtlist.Items[i].FindControl("lblexname") as Label;
                    lbexpname.Visible = true;

                    CheckBox chk = dtlist.Items[i].FindControl("chk") as CheckBox;
                    chk.Enabled = false;


                    txt = dtlist.Items[i].FindControl("txtexpfield") as TextBox;
                    if (txt.Text.Length == 0)
                        txt.Text = "  ";//to remove placeholder text
                    txt.ReadOnly = true;
                    txt.BorderStyle = BorderStyle.None;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
      
        public void insertTexbox()
        {
            try
            {
                itbl = new Item_ExportField_BL();
                itentity = new Item_ExportField_Entity();

                if (txtname.Text != "")
                {

                    itentity.Name = txtname.Text;
                }

                if (txtfield.Text != "")
                {
                    itentity.Field = txtfield.Text;
                }
                if (chkstatus.Checked)
                {
                    itentity.Status = 1;
                }
                int id = itbl.Insert(itentity);

                hfID.Value += Convert.ToString(id) + ",";
                Session["list"] = hfID.Value;


                Binddata();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }   
        }
        
        protected void Setdata(int id)
        {
            try
            {
                itbl = new Item_ExportField_BL();
                itentity = new Item_ExportField_Entity();

                DataTable dt = itbl.SelectAll();
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Export_Name"].ToString() != String.Empty)
                    {

                        txtname.Text = dt.Rows[0]["Export_Name"].ToString();
                    }
                    if (dt.Rows[0]["Export_Fields"].ToString() != String.Empty)
                    {
                        txtfield.Text = dt.Rows[0]["Export_Fields"].ToString();
                    }
                    if (dt.Rows[0]["Status"].ToString() == "1")
                    {
                        chkstatus.Checked = true;
                    }

                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void Update()
        {
            try
            {
                itbl = new Item_ExportField_BL();

                itentity = new Item_ExportField_Entity();

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Export_Name", typeof(string));
                dt.Columns.Add("Export_Fields", typeof(string));
                dt.Columns.Add("Status", typeof(int));
                #region


                #endregion

                foreach (DataListItem item in dtlist.Items)
                {
                    Label lblID = (Label)item.FindControl("Label6");
                    int ID = Convert.ToInt32(lblID.Text);
                    itentity.ID = ID;

                    TextBox txt = (TextBox)(item.FindControl("txtexpname"));

                    TextBox field = (TextBox)item.FindControl("txtexpfield");



                    if (txt.Text != "" && field.Text != "")
                    {
                        itentity.Name = txt.Text;
                        itentity.Field = field.Text;

                        CheckBox chks = item.FindControl("chk") as CheckBox;
                        if (chks.Checked)
                        {
                            itentity.Status = 1;
                            dt.Rows.Add(ID, txt.Text, field.Text, 1);
                        }
                        else
                        {
                            dt.Rows.Add(ID, txt.Text, field.Text, 0);
                        }


                        itbl.Update(dt);
                        MessageBox("Update Success");
                        //string result = "Update Success";
                        //if (result == "Update Success")
                        //{
                        //    object referrer = ViewState["UrlReferrer"];
                        //    string url = (string)referrer;
                        //    string script = "window.onload = function(){ alert('";
                        //    script += result;
                        //    script += "');";
                        //    script += "window.location = '";
                        //    script += url;
                        //    script += "'; }";
                        //    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                        //}

                    }

                    else
                    {

                        MessageBox("定義名無効");

                    }



                }

                if ((txtname.Text != String.Empty) && (txtfield.Text != String.Empty))
                {
                    insertTexbox();
                    MessageBox("Update Success");
                    //if (result == "Update Success")
                    //{
                    //    object referrer = ViewState["UrlReferrer"];
                    //    string url = (string)referrer;
                    //    string script = "window.onload = function(){ alert('";
                    //    script += result;
                    //    script += "');";
                    //    script += "window.location = '";
                    //    script += url;
                    //    script += "'; }";
                    //    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);


                    //}

                    string id = hfID.Value.ToString();

                    dtlist.DataSource = itbl.SelectAll();
                    dtlist.DataBind();
                    txtname.Text = String.Empty;
                    txtfield.Text = String.Empty;
                    chkstatus.Checked = false;


                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
      
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnConfirm_Save.Text.Equals("確認画面へ"))
                {

                    btnConfirm_Save.Text = "登録";

                }
                else if (btnConfirm_Save.Text.Equals("登録"))
                {
                    Update();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void Binddata()
        {
            try
            {
                itbl = new Item_ExportField_BL();

                string ID = hfID.Value.ToString();
                DataTable dt = itbl.SelectAll();

                dtlist.DataSource = dt;
                dtlist.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void MessageBox(string message)
        {
            try
            {
                if (message == "定義名無効" || message == "Update Success")
                {
                    Session["CatagoryList"] = null;
                    object referrer = ViewState["UrlReferrer"];
                    string url = (string)referrer;
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
                else
                {
                    string cleanMessage = message.Replace("'", "\\'");
                    string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                    {
                        page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void dtlist_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    if (dtlist.Items != null)
                    {
                        CheckBox chkch = e.Item.FindControl("chk") as CheckBox;

                        if (DataBinder.Eval(e.Item.DataItem, "Status").ToString() == "1")
                        {
                            chkch.Checked = true;

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
    }
}
