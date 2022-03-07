/* 
Created By              : Kay Thi Aung
Created Date          : 25/06/2014
Updated By             :EiPhyo(For Confimation Page)
Updated Date         :

 Tables using: Mall_Setting_Ponpare_Fixed,Shop,Code_Setup
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
    public partial class Mall_Setting_Rakhutan_Default : System.Web.UI.Page
    {
        Mall_Setting_Rakhutan_Default_BL rbl;
        Mall_Setting_Rakhutan_Default_Entity rentity;
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
                    int id = int.Parse(Request.QueryString["Shop_ID"].ToString());

                    rbl = new Mall_Setting_Rakhutan_Default_BL();
                    rentity = rbl.SelectByID(id);

                    GetData(rentity);
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

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (btnConfirm_Save.Text.Equals("確認画面へ"))
            {

                btnConfirm_Save.Text = "登録";

            }
            else if (btnConfirm_Save.Text.Equals("登録"))
            {

                rbl = new Mall_Setting_Rakhutan_Default_BL();
                rentity = new Mall_Setting_Rakhutan_Default_Entity();
                if (Request.QueryString["Shop_ID"] != null)
                {
                    String result = null;


                    SetData();
                    rentity.ID = int.Parse(Request.QueryString["Shop_ID"].ToString());


                    result = rbl.Update(rentity);
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
                    String result = rbl.Insert(rentity);
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

        protected void GetData(Mall_Setting_Rakhutan_Default_Entity rentity)
        {


            lblShopName.Text = rentity.Shop_Name;
            lblMall_Name.Text = rentity.Mall_Name;



            //rdodelivery.SelectedIndex = rentity.Delivery_Charges;

            if (rentity.Delivery_Charges == 0)
            {

                rdodelivery1.Checked = true;

            }

            else
            {
                rdodelivery2.Checked = true;
            }

            rdodualprice.SelectedIndex = rentity.DualPrice;

            //  rdopost.SelectedIndex = rentity.Postage;

            if (rentity.Postage == 0)
            {
                rdopostage1.Checked = true;
            }
            else
            {
                rdopostage2.Checked = true;

            }


            //rdosearch.SelectedIndex = rentity.Searchhide;

            if (rentity.Searchhide == 0)
            {
                rdosearch1.Checked = true;
            }

            else
            {
                rdosearch2.Checked = true;

            }


            // rdowarehouse.SelectedIndex= rentity.Warehouse;


            if (rentity.Warehouse == 0)
            {
                rdowarehouse1.Checked = true;
            }
            else
            {
                rdowarehouse2.Checked = true;

            }
            txtpassword.Text = rentity.Password;
            txtship.Text = rentity.ExtraShop;


            txtFeatureItem.Text = rentity.Featured_Item;

        }

        protected void SetData()
        {
            rentity = new Mall_Setting_Rakhutan_Default_Entity();
            if (Request.QueryString["Shop_ID"] != null)
            {
                rentity.ShopID = Convert.ToInt32(Request.QueryString["Shop_ID"].ToString());
            }

            rentity.Shop_Name = lblShopName.Text;

            rentity.Mall_Name = lblMall_Name.Text;


            if (rdodelivery1.Checked)
            {
                rentity.Delivery_Charges = 0;

            }
            else
            {

                rentity.Delivery_Charges = 1;

            }
            // rentity.Delivery_Charges = rdodelivery.SelectedIndex;

            rentity.DualPrice = rdodualprice.SelectedIndex;

            //rentity.Postage = rdopost.SelectedIndex;



            if (rdopostage1.Checked)
            {
                rentity.Postage = 0;

            }
            else
            {
                rentity.Postage = 1;
            }


            if (rdosearch1.Checked)
            {

                rentity.Searchhide = 0;
            }
            else
            {
                rentity.Searchhide = 1;
            }

            //rentity.Searchhide = rdosearch.SelectedIndex;




            //rentity.Warehouse = rdowarehouse.SelectedIndex;



            if (rdowarehouse1.Checked)
            {
                rentity.Warehouse = 0;

            }

            else
            {
                rentity.Warehouse = 1;
            }

            rentity.Password = txtpassword.Text;
            rentity.ExtraShop = txtship.Text;


            rentity.Featured_Item = txtFeatureItem.Text;

        }


        protected void Confirm()
        {


            lblShopName.Visible = true;
            lblMall_Name.Visible = true;

            lblPostage.Visible = true;
            rdopostage1.Visible = false;
            rdopostage2.Visible = false;

            if(rdopostage1.Checked==true)
            {
                lblPostage.Text = "送料別";
            }
            else
            {
                lblPostage.Text = "送料込";
            }
            
            lblShippingCost.Visible = true;
            txtship.Visible = false;
            lblShippingCost.Text = txtship.Text;


            lblDaibikiryō.Visible = true;

            rdodelivery1.Visible = false;
            rdodelivery2.Visible = false;

            if (rdodelivery1.Checked == true)
            {
                lblDaibikiryō.Text = "代引料別";

            }
            else
            {
                lblDaibikiryō.Text = "代引料込";
            }



            lblWarehousespecified.Visible = true;

            rdowarehouse1.Visible = false;

            rdowarehouse2.Visible = false;


            if (rdowarehouse1.Checked == true)
            {

                lblWarehousespecified.Text = "販売中";
            }

            else
            {
                lblWarehousespecified.Text = "倉庫に入れる";

            }


            lblSearchhide.Visible = true;

            rdosearch1.Visible = false;

            rdosearch2.Visible = false;

            if (rdosearch1.Checked == true)
            {
                lblSearchhide.Text = "表示する";
            }
            else
            {
                lblSearchhide.Text = "表示しない";

            }


            lblBlackmarketPassword.Visible = true;

            txtpassword.Visible = false;

            lblBlackmarketPassword.Text = txtpassword.Text;


            rdodualprice.Visible = false;
            lblDoublepricewordingControlNumber.Visible = true;



            if (rdodualprice.SelectedIndex == 0)
            {
                lblDoublepricewordingControlNumber.Text = "自動選択";
            }
            else if (rdodualprice.SelectedIndex == 1)
            {

                lblDoublepricewordingControlNumber.Text = "当店通常価格";
            }
            else
            {
                lblDoublepricewordingControlNumber.Text = "メーカー希望小売価格";

            }


            lblfeatureItem.Visible = true;
            txtFeatureItem.Visible = false;
            lblfeatureItem.Text = txtFeatureItem.Text;



        }
    }
}