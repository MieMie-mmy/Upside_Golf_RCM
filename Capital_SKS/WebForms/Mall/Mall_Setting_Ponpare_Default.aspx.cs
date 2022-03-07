/* 
Created By              : Kay Thi Aung
Created Date          : 27/06/2014
Updated By             :
Updated Date         :

 Tables using: Mall_Setting_Ponpare_Default,Shop,Code_Setup 
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
    public partial class Mall_Setting_Ponpare_Default : System.Web.UI.Page
    {
        Mall_Setting_Ponpare_Default_BL pbl;
        Mall_Setting_Ponpare_Default_Entity pentity;
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

                    pbl = new Mall_Setting_Ponpare_Default_BL();

                    int id = int.Parse(Request.QueryString["Shop_ID"].ToString());

                    pentity = pbl.SelectByID(id);
                    GetData(pentity);
                }
                else
                {
                    SetData();

                }
            }
           
            else 
            {
                btnpopup.Visible = false;
              btnsave.Visible = true;
              Confirm();
            }
        }

        protected void Confirm()
        {
            if (Request.QueryString["Shop_ID"] != null)
            {
                btnsave.Text = "更新";



                lbldelivery.Visible = true;
                lblexship.Visible = true;
                lblpassword.Visible = true;
                lblpost.Visible = true;
                lblsstatue.Visible = true;

                //rdosalestatus.Visible = false;
                rdosalestatus1.Visible = false;
                rdosalestatus2.Visible = false;
                //rdopost.Visible = false;
                rdopost1.Visible = false;
                rdopost2.Visible = false;
                //rdodelivery.Visible = false;
                rdodelivery1.Visible = false;
                rdodelivery2.Visible = false;
                txtexship.Visible = false;
                txtpassword.Visible = false;

                //if (rdosalestatus.SelectedItem.Value.ToString() == "1") 
                //{
                //    lblsstatue.Text = "未販売 ";
                //}
                //    else
                //    lblsstatue.Text = "販売中";

                //if (rdopost.SelectedItem.Value.ToString() == "0")
                //    lblpost.Text = "送料別 ";
                //else
                //    lblpost.Text = "送料込";


                //if (rdodelivery.SelectedItem.Value.ToString() == "0")
                //    lbldelivery.Text = "代引料別 ";
                //else
                //    lbldelivery.Text = "代引料込";

                if (rdosalestatus1.Checked)
                {
                    lblsstatue.Text = "未販売 ";
                }
                else
                    lblsstatue.Text = "販売中";

                if (rdopost1.Checked)
                {
                    lblpost.Text = "送料別 ";
                }
                else
                    lblpost.Text = "送料込";

                if (rdodelivery1.Checked)
                {
                    lbldelivery.Text = "代引料別 ";
                }
                else
                    lbldelivery.Text = "代引料込";

                lblexship.Text = txtexship.Text;
                lblpassword.Text = txtpassword.Text;
            }
        }

        protected void GetData(Mall_Setting_Ponpare_Default_Entity pentity ) 
        {
            //rdosalestatus.SelectedValue = "1";
            //rdosalestatus.SelectedValue = "2";
            //rdopost.SelectedIndex = pentity.Post;
            //rdodelivery.SelectedIndex = pentity.Delivery;
            if (pentity.Sale_Status == 1)
                rdosalestatus1.Checked = true;
            else if (pentity.Sale_Status == 2)
                rdosalestatus2.Checked = true;
            if (pentity.Post == 0)
                rdopost1.Checked = true;
            else if (pentity.Post == 1)
                rdopost2.Checked = true;
            if (pentity.Delivery == 0)
                rdodelivery1.Checked = true;
            else if (pentity.Delivery == 1)
                rdodelivery2.Checked = true;
            txtexship.Text = pentity.Ship;
            txtpassword.Text = pentity.Password;
            lblshopname.Text = pentity.Shopname;
            lblmall.Text = pentity.MallDesc;
        }

        protected void SetData() 
        {
            pentity = new Mall_Setting_Ponpare_Default_Entity();
            if (Request.QueryString["Shop_ID"] != null)
            {
                pentity.Shop_ID = int.Parse(Request.QueryString["Shop_ID"].ToString());

            }
            if (rdosalestatus1.Checked == true)
            {
                pentity.Sale_Status = 1;
            }
            else
            {
                pentity.Sale_Status = 2;
            }
            //pentity.Sale_Status =Convert.ToInt32(rdosalestatus.SelectedValue);
            if (rdopost1.Checked == true)
            {
                pentity.Post = 0;
            }
            else
            {
                pentity.Post = 1;
            }
            //pentity.Post = rdopost.SelectedIndex;
            pentity.Ship = txtexship.Text;
            //pentity.Delivery = rdodelivery.SelectedIndex;
            if (rdodelivery1.Checked == true)
            {
                pentity.Delivery = 0;
            }
            else
            {
                pentity.Delivery = 1;
            }
            pentity.Password = txtpassword.Text;
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (btnsave.Text.Equals("確認画面へ"))
            {

                btnsave.Text = "更新";
               
            }
            else if (btnsave.Text.Equals("更新"))
            {
                Save();
            } 
        }

        protected void Save() 
        {
            pbl = new Mall_Setting_Ponpare_Default_BL();
            if (Request.QueryString["Shop_ID"] != null)
            {
                String result = null;


                SetData();
                pentity.ID = int.Parse(Request.QueryString["Shop_ID"].ToString());


                result = pbl.Update(pentity);
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
                String result = pbl.Insert(pentity);
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
   
}