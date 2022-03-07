/* 
Created By              : Kay Thi Aung
Created Date          : 18/06/2014
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
using ORS_RCM_Common;
using ORS_RCM_BL;

namespace ORS_RCM
{
    public partial class Shop : System.Web.UI.Page
    {
        Shop_Entity sentity;
        Shop_BL shBl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                if (Request.QueryString["Shop_ID"] != null)
                {
                    head.InnerText = "ショップ編集";
                    shBl = new Shop_BL();
                    int id = int.Parse(Request.QueryString["Shop_ID"].ToString());
                    sentity = shBl.SelectByID(id);
                    FillMalldata();
                    Getdata(sentity);
                    btnpopup.Visible = true;
                    btnConfirm_Save.Visible = false;
                }
                else
                {
                    SetData();
                    head.InnerText = "ショップ一登録";
                    FillMalldata();
                }
            }
            else
            {
                #region popup page
                if ((!String.IsNullOrWhiteSpace(txtshopname.Text)) && (!String.IsNullOrWhiteSpace(txtfhost.Text)) && (!String.IsNullOrWhiteSpace(ddlmall.SelectedValue))
                 && (!String.IsNullOrWhiteSpace(txtfacc.Text)) && (!String.IsNullOrWhiteSpace(txtfpass.Text))
                 && (!String.IsNullOrWhiteSpace(txturl.Text)))
                {
                    btnpopup.Visible = false;
                    btnConfirm_Save.Visible = true;
                    Confirm();
                }
                else
                {
                    GlobalUI.MessageBox("Fill Proper data in all Textboxes");
                }
                #endregion
            }
        }

        protected void SetData()
        {
            sentity = new Shop_Entity();
            sentity.ShopID = txtshopid.Text;
            sentity.ShopName = txtshopname.Text;
            sentity.MallOpen = ddlmall.SelectedValue;
            sentity.shpURL = txtshurl.Text;
            sentity.imgURL = txturl.Text;
            sentity.FTPacc = txtfacc.Text;
            sentity.FTPhost = txtfhost.Text;
            sentity.FTPpass = txtfpass.Text;
            if (rdostatus.Checked == true)
            {
                sentity.Status = 1;
            }
            else
            {
                sentity.Status = 0;
            }
            if (rdb3.Checked)
            {
                sentity.Categorycheck1 = 1;
            }
            else
            {
                sentity.Categorycheck1 = 0;
            }
            if (txtfreeShipping.Text != "")
            {
                txtfreeShipping.Text = ((txtfreeShipping.Text.Replace(",", String.Empty)));
                sentity.Shipping_Condition = Convert.ToInt32(txtfreeShipping.Text);
                //updated date 15/06/2015
                sentity.Libftphost = txtliburl.Text.Trim();
                sentity.Libftpacc = txtlibacc.Text.Trim();
                sentity.Libftppass = txtlblpass.Text.Trim();
                sentity.Libdirectory = txtlibdirectory.Text.Trim();
            }
        }

        protected void Getdata(Shop_Entity sentity)
        {
            txtshopid.Text = sentity.ShopID;
            txtshopname.Text = sentity.ShopName;
            txtfacc.Text = sentity.FTPacc;
            txtfhost.Text = sentity.FTPhost;
            txtfpass.Text = sentity.FTPpass;
            txtfreeShipping.Text = Convert.ToString(sentity.Shipping_Condition);
            txtfreeShipping.Text = Convert.ToString((sentity.Shipping_Condition).ToString("#,##0"));
            txtshurl.Text = sentity.shpURL;
            txturl.Text = sentity.imgURL;
            if (Convert.ToInt32(sentity.MallOpen) != 0)
            {
                ddlmall.SelectedValue = sentity.MallOpen;
            }
            if (sentity.Status == 1)
            {
                rdostatus.Checked = true;
            }
            else
            {
                rdostatus1.Checked = true;
            }
            if (sentity.Categorycheck1 == 1)
            {
                rdb3.Checked = true;
            }
            else
            {
                rdb4.Checked = true;
            }
            //updated date 15/06/2015
            txtliburl.Text = sentity.Libftphost;
            txtlibacc.Text = sentity.Libftpacc;
            txtlblpass.Text = sentity.Libftppass;
            txtlibdirectory.Text = sentity.Libdirectory;
        }

        protected void Confirm()
        {
            #region updated date 15/06/2015
            txtliburl.Visible = false;
            lblliburl.Visible = true;
            lblliburl.Text = txtliburl.Text;
            txtlibacc.Visible = false;
            lbllibaacc.Visible = true;
            lbllibaacc.Text = txtlibacc.Text;
            txtlblpass.Visible = false;
            lbllibpass.Visible = true;
            lbllibpass.Text = txtlblpass.Text;
            txtlibdirectory.Visible = false;
            lbllibdirectory.Visible = true;
            lbllibdirectory.Text = txtlibdirectory.Text;
            #endregion
            txtshopname.Visible = false;
            lblShopName.Visible = true;
            lblShopName.Text = txtshopname.Text;
            ddlmall.Visible = false;
            lblmall.Visible = true;
            if (ddlmall.SelectedValue == "1")
            {
                lblmall.Text = "楽天";
            }
            else if (ddlmall.SelectedValue == "2")
            {
                lblmall.Text = "YAHOO";
            }
            else if (ddlmall.SelectedValue == "3")
            {
                lblmall.Text = "ポンパレ";
            }
            else if (ddlmall.SelectedValue == "4")
            {
                lblmall.Text = "自社";
            }
            txtshopid.Visible = false;
            lblShopId.Visible = true;
            lblShopId.Text = txtshopid.Text;
            txtfhost.Visible = false;
            lblftpHost.Visible = true;
            lblftpHost.Text = txtfhost.Text;
            txtfacc.Visible = false;
            lblFtpAccount.Visible = true;
            lblFtpAccount.Text = txtfacc.Text;
            txtfpass.Visible = false;
            lblFTPpassword.Visible = true;
            lblFTPpassword.Text = txtfpass.Text;
            txtshurl.Visible = false;
            txturl.Visible = false;
            lblshpurl.Visible = true;
            lblshpurl.Text = txtshurl.Text;
            lblimgurl.Visible = true;
            lblimgurl.Text = txturl.Text;
            txtfreeShipping.Visible = false;
            lblFreeshipping.Visible = true;
            if (lblFreeshipping.Text.Length > 3)
            {
                lblFreeshipping.Text = Convert.ToDecimal(txtfreeShipping.Text).ToString("#,##0.00");
            }
            else
            {
                lblFreeshipping.Text = txtfreeShipping.Text;
            }
            if (rdostatus.Checked == true)
            {
                rdostatus.Visible = false;
                rdostatus1.Visible = false;
                lblStatus.Visible = true;
                lblStatus.Text = "有効";
            }
            else if (rdostatus1.Checked == true)
            {
                rdostatus.Visible = false;
                rdostatus1.Visible = false;
                lblStatus.Visible = true;
                lblStatus.Text = "無効";
            }
            if (rdb3.Checked == true)
            {
                rdb3.Visible = false;
                rdb4.Visible = false;
                lblcategory.Visible = true;
                lblcategory.Text = "あり";
            }
            else
            {
                rdb3.Visible = false;
                rdb4.Visible = false;
                lblcategory.Visible = true;
                lblcategory.Text = "なし";
            }

            if (Request.QueryString["Shop_ID"] != null)
            {
                edit_Confirm.Visible = true;
                btnConfirm_Save.Text = "反映";
                edit.Visible = false;
            }
            else
            {
                register_Confirm.Visible = true;
                edit_Confirm.Visible = false;
                btnConfirm_Save.Text = "登録";
                edit.Visible = false;
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(txtshopname.Text)) && (String.IsNullOrWhiteSpace(txtfhost.Text)) && (string.IsNullOrWhiteSpace(ddlmall.SelectedValue))
                && (String.IsNullOrWhiteSpace(txtfacc.Text)) && (String.IsNullOrWhiteSpace(txtfpass.Text)) && (String.IsNullOrWhiteSpace(txtfreeShipping.Text))
                && (String.IsNullOrWhiteSpace(txturl.Text)) && (String.IsNullOrWhiteSpace(txtliburl.Text)) && (String.IsNullOrWhiteSpace(txtlibacc.Text)) && (String.IsNullOrWhiteSpace(txtlblpass.Text)) && (String.IsNullOrWhiteSpace(txtlibdirectory.Text)))
            {
                GlobalUI.MessageBox("Fill Proper data in all Textboxes");
            }
            else
            {
                shBl = new Shop_BL();
                sentity = new Shop_Entity();
                if (Request.QueryString["Shop_ID"] != null)
                {
                    String result = null;
                    SetData();
                    sentity.ID = int.Parse(Request.QueryString["Shop_ID"].ToString());
                    result = shBl.Update(sentity);
                    if (result == "Update Successful !")
                    {
                        object referrer = ViewState["UrlReferrer"];
                        string url = (string)referrer;
                        string script = "window.onload = function(){ alert('";
                        script += result;
                        script += "');";
                        script += "window.location = '";
                        script += url;
                        script += "'; }";
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                    }
                    else { GlobalUI.MessageBox("Update Fail!"); }
                }

                else
                {
                    SetData();
                    String result = shBl.Insert(sentity);
                    if (result == "Save Successful !")
                    {
                        object referrer = ViewState["UrlReferrer"];
                        string url = (string)referrer;
                        string script = "window.onload = function(){ alert('";
                        script += result;
                        script += "');";
                        script += "window.location = '";
                        script += url;
                        script += "'; }";
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                    }
                    else { GlobalUI.MessageBox("Save Fail!"); }
                }
            }
        }

        protected void FillMalldata()
        {
            try
            {
                GlobalBL gb = new GlobalBL();
                ddlmall.DataSource = gb.Code_Setup(1);
                ddlmall.DataTextField = "Code_Description";
                ddlmall.DataValueField = "ID";
                ddlmall.DataBind();
                ddlmall.Items.Insert(0, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}