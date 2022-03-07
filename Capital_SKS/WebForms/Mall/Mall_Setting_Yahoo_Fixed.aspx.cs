/* 
Created By              : Eephyo
Created Date          : 26/06/2014
Updated By             :
Updated Date         :

 Tables using: Mall_Setting_Yahoo_Fixed,Code_Setup,Shop
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
using System.Globalization;


namespace ORS_RCM
{
    public partial class Mall_Setting_Yahoo_Fixed : System.Web.UI.Page
    {
        Mall_Setting_Yahoo_Fixed_BL ybl;
        Mall_Setting_Yahoo_Fixed_Entity yentity;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region BackPage ViewState
                String backpage = string.Empty;
                if (Request.UrlReferrer != null)
                {
                    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                    backpage = Request.UrlReferrer.ToString();
                }
                else
                {
                    ViewState["UrlReferrer"] = backpage;
                }
                #endregion

                if (Request.QueryString["Shop_ID"] != null)
                {

                    ybl = new Mall_Setting_Yahoo_Fixed_BL();

                    int id = int.Parse(Request.QueryString["Shop_ID"].ToString());

                    yentity = ybl.SelectByID(id);
                    GetData(yentity);

                    btnpopup.Visible = true;
                    btnConfirm_Save.Visible = false;

                }
                else
                {
                    SetData();
                }
            }
            else
            {
                string name = getPostBackControlName();
                if (name == null)
                {

                    //if (Request.QueryString.AllKeys.Contains("PopupID"))
                    btnpopup.Visible = false;
                    btnConfirm_Save.Visible = true;
                    btnConfirm_Save.Text = "登録";
                    Confirm();
                }
            }

        }

        public void Confirm()
        {

            txtshopID.Visible = false;

            lblShopName.Visible = true;

            lblMallName.Visible = true;

            
            lblSpecialPrice.Visible = true;
            txtprice.Visible = false;
            lblSpecialPrice.Text=txtprice.Text;

            lblComment.Visible = true;
            txtcomment.Visible= false;
            lblComment.Text = txtcomment.Text;

            ImageButton1.Visible = false;



            rdoTax.Visible = false;
            rdoExempt.Visible = false;
            lblTaxable.Visible = true;

            if (rdoTax.Checked == true)
            {
                lblTaxable.Text = "課税  ";

            }
            else
            {
                lblTaxable.Text = "非課税";
            }


             


            //lblReleaseDate.Visible = true;

            //txtRedate.Visible = false;

            //lblReleaseDate.Text = txtRedate.Text;

            lblReleaseDate.Visible = true;

            ImageButton1.Visible = false;

            lblReleaseDate.Text = txtRedate.Text;
            txtRedate.Visible = false;

            if (!String.IsNullOrWhiteSpace(Request.Form[txtRedate.UniqueID]))
            {
                string strDate = Request.Form[txtRedate.UniqueID];

                DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                dtfi.ShortDatePattern = "dd-MM-yyyy";
                dtfi.DateSeparator = "-";
                DateTime objDate = Convert.ToDateTime(strDate);
                string date = Convert.ToDateTime(objDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy hh:MM:ss");
                objDate = DateTime.ParseExact(date, "MM/dd/yyyy hh:MM:ss", null);
                lblReleaseDate.Text = objDate.ToString();
                CustomHiddenField.Value = date;

            }

            else
            {
                lblReleaseDate.Text = null;
            }
            
            lblProvisionalPeriodPoint.Visible = true;
            txtProperiod.Visible = false;

            lblProvisionalPeriodPoint.Text = txtProperiod.Text;

            lblTemplateInUse.Visible = true;
            txtTemplate.Visible = false;

            lblTemplateInUse.Text = txtTemplate.Text;

            
            lblLimitingthenumberofpurchase.Visible = true;

            txtPurchaseAmount.Visible = false;

            lblLimitingthenumberofpurchase.Text = txtPurchaseAmount.Text;


            rdoUsed.Visible = false;
            rdoNew.Visible = false;
            lblStateofTheProduct.Visible = true;

            if(rdoNew.Checked==true)
            {
                lblStateofTheProduct.Text = "新品 ";
            }
            else  
            {
                lblStateofTheProduct.Text = "中古";

            }


            txtJapanlisting.Visible = false;
            lblListingToJapan.Visible = true;

            lblListingToJapan.Text = txtJapanlisting.Text;
            
        
        }

        public void SetData()
        {
            yentity = new Mall_Setting_Yahoo_Fixed_Entity();

           if (Request.QueryString["Shop_ID"] != null) 
           {
                yentity.Shop_ID = int.Parse(Request.QueryString["Shop_ID"].ToString());
           }

            yentity.Shop_Name = lblShopName.Text;
            yentity.Mall_Name = lblMallName.Text;
            if (!String.IsNullOrWhiteSpace(txtprice.Text))
                yentity.Special_Price = txtprice.Text;
            else { yentity.Special_Price = null; }
            yentity.Word_Comment = txtcomment.Text;

            if (rdoTax.Checked)
                yentity.Taxable = 2;
            else
                yentity.Taxable = 1;
            //yentity.Release_Date = Convert.ToDateTime(System.DateTime.Now.ToString());
            if (!String.IsNullOrWhiteSpace(CustomHiddenField.Value))
            {
                 yentity.Release_Date = Convert.ToDateTime(CustomHiddenField.Value);
                CustomHiddenField.Value = String.Empty;
            }
            else
                yentity.Release_Date=null ;

            //txtRedate.Text = String.Empty;

            //if (!String.IsNullOrWhiteSpace(Request.Form[txtRedate.UniqueID]))
            //{
            //    string strDate = Request.Form[txtRedate.UniqueID];

            //    DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            //    dtfi.ShortDatePattern = "dd-MM-yyyy";
            //    dtfi.DateSeparator = "-";
            //    DateTime objDate = Convert.ToDateTime(strDate);
            //    string date = Convert.ToDateTime(objDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy hh:MM");
            //    objDate = DateTime.ParseExact(date, "MM/dd/yyyy hh:MM", null);
            //    yentity.Release_Date = objDate;

            //     // lblReservationReleasedDate.Text = objDate.ToString();
            //     //CustomHiddenField.Value = date;
            //}
            //else
            //{
            //    yentity.Release_Date =null;
            //}
           // txtRedate.UniqueID = txtRedate.Text;
            yentity.Provisional_Period = txtProperiod.Text;
            yentity.Template = txtTemplate.Text;
            yentity.NoofPurchases = txtPurchaseAmount.Text;
            if (rdoNew.Checked)
                yentity.Product_State = 0;
            else
                yentity.Product_State = 1;
            yentity.Listing_Japan = txtJapanlisting.Text;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //if ((!String.IsNullOrEmpty(txtshopID.Text.Trim()) && (!String.IsNullOrEmpty(txtprice.Text.Trim()) && (!String.IsNullOrEmpty(txtcomment.Text.Trim()) && ((!rdoTax.Checked) && (!rdoExempt.Checked))
            //   && ((!String.IsNullOrEmpty(txtProperiod.Text.Trim()) && ((!String.IsNullOrEmpty(txtTemplate.Text.Trim()) && ((!String.IsNullOrEmpty(txtPurchaseAmount.Text.Trim()) && ((!rdoNew.Checked) && (!rdoUsed.Checked)) && (!String.IsNullOrEmpty(txtJapanlisting.Text.Trim()))))))))))))
            //{
            if (btnConfirm_Save.Text.Equals("確認画面へ"))
            {
                btnConfirm_Save.Text = "登録";
            }
            else if (btnConfirm_Save.Text.Equals("登録"))
            {
                ybl = new Mall_Setting_Yahoo_Fixed_BL();
                String result = null;
                if (Request.QueryString["Shop_ID"] != null)
                {
                    SetData();
                    yentity.ID = int.Parse(Request.QueryString["Shop_ID"].ToString());
                    result = ybl.Update(yentity);
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
                }
                else
                {
                    SetData();
                    result = ybl.Insert(yentity);
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
                }
            }
        }

        protected void GetData(Mall_Setting_Yahoo_Fixed_Entity yhentity)
        {
            if (Request.QueryString["Shop_ID"] != null)
            {
                int id = int.Parse(Request.QueryString["Shop_ID"].ToString());
                lblShopName.Text = yhentity.Shop_Name;
                lblMallName.Text = yhentity.Mall_Name;
                txtshopID.Text = Convert.ToString(yhentity.Shop_ID);
                (txtprice.Text) = Convert.ToString((yhentity.Special_Price));
                txtcomment.Text = yhentity.Word_Comment;
                if (yhentity.Taxable == 2)
                {
                    rdoTax.Checked = true;
                }
                else
                {
                    rdoExempt.Checked = true;
                    rdoTax.Checked = false;
                }
                txtRedate.Text = Convert.ToString(yhentity.Release_Date);
                //tbookorder.Text = Convert.ToString(rentity.Orderrelease);
                txtProperiod.Text = yhentity.Provisional_Period;
                txtTemplate.Text = yhentity.Template;
                txtPurchaseAmount.Text = yhentity.NoofPurchases;
                if (yhentity.Product_State == 1)
                {
                    rdoUsed.Checked = true;
                    rdoNew.Checked = false;
                }
                else
                {
                    rdoNew.Checked = true;
                    
                }
                txtJapanlisting.Text = yhentity.Listing_Japan;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            txtRedate.Text = String.Empty;
        }

        private string getPostBackControlName()
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

    }
}