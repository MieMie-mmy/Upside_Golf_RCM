/* 
Created By              : Eephyo
Created Date          : 26/06/2014
Updated By             :
Updated Date         :

Tables using: Shop,Code_Setup,Mall_Setting_Yahoo_Default
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
    public partial class Mall_Setting_Yahoo_Default : System.Web.UI.Page
    {
        Mall_Setting_Yahoo_Default_BL ybl;
        Mall_Setting_Yahoo_Default_Entity ydentity;


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
                    ybl = new Mall_Setting_Yahoo_Default_BL();
                    int id = int.Parse(Request.QueryString["Shop_ID"].ToString());
                    ydentity = ybl.SelectByID(id);
                    GetData(ydentity);
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
                #region popup page
                //if (Request.QueryString.AllKeys.Contains("PopupID"))
                btnpopup.Visible = false;
                btnConfirm_Save.Visible = true;
                btnConfirm_Save.Text = "登録";
                Confirm();

                #endregion
            }
        }



        public void Confirm()
        {

            lblShopName.Visible= true;

            lblMallName.Visible = true;

            lblWeight.Visible = true;
            txtweight.Visible = false;

            lblWeight.Text = txtweight.Text;
        }


        public void SetData()
        {
            ydentity = new Mall_Setting_Yahoo_Default_Entity();

            if (!String.IsNullOrWhiteSpace(txtshopID.Text))
            {
                ydentity.Shop_ID = Convert.ToInt32(txtshopID.Text);

            }

            ydentity.Shop_Name = lblShopName.Text;
            ydentity.Mall_Name = lblMallName.Text;

            if (!String.IsNullOrWhiteSpace(txtweight.Text))
            {
                ydentity.Weight = txtweight.Text;


            }

        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (btnConfirm_Save.Text.Equals("確認画面へ"))
            {

                btnConfirm_Save.Text = "登録";

            }
            else if (btnConfirm_Save.Text.Equals("登録"))
            {



                ybl = new Mall_Setting_Yahoo_Default_BL();
                String result = null;
                if (Request.QueryString["Shop_ID"] != null)
                {


                    ybl = new Mall_Setting_Yahoo_Default_BL();
                    SetData();
                    ydentity.ID = int.Parse(Request.QueryString["Shop_ID"].ToString());
                    result = ybl.Update(ydentity);
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
                    result = ybl.Insert(ydentity);
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

        protected void GetData(Mall_Setting_Yahoo_Default_Entity ydentity)
        {
            if (Request.QueryString["Shop_ID"] != null)
            {
                int id = int.Parse(Request.QueryString["Shop_ID"].ToString());


                ydentity.Shop_ID = id;

                txtshopID.Text = Convert.ToString(ydentity.Shop_ID);

                lblShopName.Text = ydentity.Shop_Name;

                lblMallName.Text = ydentity.Mall_Name;


                txtweight.Text = ydentity.Weight;

            }
        }
    }
}
