using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Delivery
{
    public partial class Delivery_Entry : System.Web.UI.Page
    {
        public int UserID
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
            else
            {
                if (ViewState["dt"] != null)
                {
                    
                    DataTable dt = ViewState["dt"] as DataTable;
                    String ctrl = getPostBackControlName();
                    if (ctrl.Contains("lnkPaging"))
                    {
                        gvdelivery.EditIndex = -1;
                        gp.LinkButtonClick(ctrl, gvdelivery.PageSize);
                        Label lbl = gp.FindControl("lblCurrent") as Label;
                        int index = Convert.ToInt32(lbl.Text);
                        gvdelivery.PageIndex = index - 1;
                        gvdelivery.DataSource = dt;
                        gvdelivery.DataBind();
                    }
                }
            }
        }

        protected void BindGrid()
        {
            Delivery_BL dbl = new Delivery_BL();
            gvdelivery.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
            DataSet ds = dbl.SelectAll();
            DataTable dt = ds.Tables[0];
            int count = 0;
            if (dt.Rows.Count > 0)
                count = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            if (ViewState["btnEvt"] != null)
            {
                gvdelivery.PageIndex = 0;
                gvdelivery.DataSource = dt;
                gvdelivery.DataBind();
                ViewState["dt"] = dt;
                gp.TotalRecord = count;
                gp.OnePageRecord = gvdelivery.PageSize;
                gp.CalculatePaging(count, gvdelivery.PageSize, 1);
                ViewState["btnEvt"] = null;
            }
            else
            {
                gvdelivery.DataSource = dt;
                gvdelivery.DataBind();
                ViewState["dt"] = dt;
                gp.CalculatePaging(count, gvdelivery.PageSize, 1);
            }
        }

        protected void Bind()
        {
            Delivery_BL dbl = new Delivery_BL();
            gvdelivery.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
            DataSet ds = dbl.SelectAll();
            DataTable dt = ds.Tables[0];
            gvdelivery.DataSource = dt;
            ViewState["dt"] = dt;
            gvdelivery.DataBind();


        }
        protected void btnadd_OnClick(object sender, EventArgs e)
        {
            Delivery_BL dbl = new Delivery_BL();
            int yshippingno = Convert.ToInt16(txtyshippingno.Text);

            int rshippingno = Convert.ToInt16(txtrshippingno.Text);

            int check1 = dbl.CheckExistingData(yshippingno, rshippingno, 3);
            if (check1 == 0)
            {
                GlobalUI.MessageBox("Yahoo Shipping Number Already Exisit!!");
            }
            else
            {
                int check2 = dbl.CheckExistingData(yshippingno, rshippingno, 2);
                if (check2 == 0)
                {
                    GlobalUI.MessageBox("Rakuten Shipping Setting Already Exisit!!");
                }
                else
                {
                    string ordersetting = txtestdelivery.Text;
                    int check = dbl.CheckExistingData(yshippingno, rshippingno, 1);
                    if (check == 0)
                    { GlobalUI.MessageBox("Delivery Setting Already Exisit!!"); }
                    else
                    {
                        bool id = dbl.Insert(yshippingno, rshippingno, ordersetting, UserID);
                        if (id)
                        {
                            GlobalUI.MessageBox("Save Successfully!!!");
                            Clear();
                            ViewState["btnEvt"] = "Yes";
                            BindGrid();
                        }
                    }
                }
            }
        }

        public void Clear()
        {
            txtestdelivery.Text = "";
            txtrshippingno.Text = "";
            txtyshippingno.Text = "";
        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvdelivery.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
                BindGrid();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvdelivery_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvdelivery.EditIndex = e.NewEditIndex;
            Bind();
        }

        protected void gvdelivery_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Delivery_BL dbl = new Delivery_BL();
            Label id = gvdelivery.Rows[e.RowIndex].FindControl("lblID") as Label;
            dbl.Delete(Convert.ToInt16(id.Text));
            gvdelivery.EditIndex = -1;
            ViewState["btnEvt"] = "Yes";
            BindGrid();
        }

        protected void gvdelivery_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Delivery_BL dbl = new Delivery_BL();
            string error = null;
            int option = 0;
            Label id = gvdelivery.Rows[e.RowIndex].FindControl("lblID") as Label;
            TextBox estimated = gvdelivery.Rows[e.RowIndex].FindControl("txtestimated") as TextBox;
            TextBox ysetting = gvdelivery.Rows[e.RowIndex].FindControl("txtysetting") as TextBox;
            TextBox rsetting = gvdelivery.Rows[e.RowIndex].FindControl("txtrsetting") as TextBox;
            int check1 = dbl.CheckExistingData(Convert.ToInt16(ysetting.Text), Convert.ToInt16(rsetting.Text), 3);
            //bool ID = dbl.Update(Convert.ToInt16(ysetting.Text), Convert.ToInt16(rsetting.Text), estimated.Text, UserID, Convert.ToInt16(id.Text));
            if (check1 == 0)
            {
                int yahoo = dbl.SelectShippingNumber(Convert.ToInt16(ysetting.Text), Convert.ToInt16(rsetting.Text), 2, Convert.ToInt16(id.Text));
                if (yahoo == 1)
                {
                    GlobalUI.MessageBox("Yahoo Setting already exist!");
                    error = "Yahoo";
                }
            }
            //else
            //{
            if (error == null)
            {
                int check2 = dbl.CheckExistingData(Convert.ToInt16(ysetting.Text), Convert.ToInt16(rsetting.Text), 2);
                if (check2 == 0)
                {
                    int rakuten = dbl.SelectShippingNumber(Convert.ToInt16(ysetting.Text), Convert.ToInt16(rsetting.Text), 1, Convert.ToInt16(id.Text));
                    if (rakuten == 1)
                    {
                        GlobalUI.MessageBox("Rakuten Setting already exist!");
                        error = "rakuten";
                    }
                }
            }
            if (error == null)
            {
                bool ID = dbl.Update(Convert.ToInt16(ysetting.Text), Convert.ToInt16(rsetting.Text), estimated.Text, UserID, Convert.ToInt16(id.Text));
                if (ID)
                {
                    GlobalUI.MessageBox("Update Successfully!!!");
                }
                gvdelivery.EditIndex = -1;
                Bind();
            }
            //}            
        }

        protected void gvdelivery_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvdelivery.EditIndex = -1;
            Bind();
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
    }
}