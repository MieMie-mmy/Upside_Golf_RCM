/* 
Created By              : Kay Thi Aung
Created Date          : 25/06/2014
Updated By             :
Updated Date         :

 Tables using: Shop,Mall_Setting_Rakhutan_Fixed,Code_Setup 
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
    public partial class Mall_Setting_Rakhutan_Fixed : System.Web.UI.Page
    {
        Mall_Setting_Rakhutan_Fixed_BL rbl;
        Mall_Setting_Rakhutan_Fixed_Entity rentity;
       
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

                    rbl = new Mall_Setting_Rakhutan_Fixed_BL();
                 
                    int id = int.Parse(Request.QueryString["Shop_ID"].ToString());

                 

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
                string name = getPostBackControlName();
                if (name == null)
                {
                    //if (Request.QueryString.AllKeys.Contains("PopupID"))
                    btnpopup.Visible = false;
                    btnConfirm_Save.Visible = true;
                    btnConfirm_Save.Text = "更新";
                    Confirm();
                }
              
            }

        }

    

        protected void Confirm()
        {
            lblShopName.Visible = true;
           
            lblMallName.Visible = true;
            lblTagID.Visible = true;
            lblTagID.Text = txttagiD.Text;
            txttagiD.Visible = false;
            rdoConsumption1.Visible = false;
            rdoConsumption2.Visible = false;

            lblConsumptionTax.Visible = true;

            if (rdoConsumption1.Checked == true)
            {
                lblConsumptionTax.Text = "消費税込み";
            }
             else
            {
                lblConsumptionTax.Text = "消費税別";

            }
           
            lblShippingCategory1.Visible = true;
            lblShippingCategory1.Text = txtship1.Text;
            txtship1.Visible = false;


            lblShippingCategory2.Visible = true;
            lblShippingCategory2.Text = txtship2.Text;
            txtship2.Visible = false;

            lblRacno.Visible = true;
            lblRacno.Text = txtRACno.Text;
            txtRACno.Visible= false;


            rdoOrder1.Visible = false;
            rdoOrder2.Visible = false;
            rdoOrder3.Visible = false;
            lblOrderButton.Visible = true;
            
            if(rdoOrder1.Checked==true)
            {
                lblOrderButton.Text = "ボタンをつけない";
            }
            else if (rdoOrder2.Checked == true)
            {
                lblOrderButton.Text = "通常注文ボタンをつける";
            }
            else if (rdoOrder3.Checked == true)
            {

                lblOrderButton.Text = "予約注文ボタンをつける";

            }


            rdorequest1.Visible = false;
            rdorequest2.Visible = false;
            lblRequest.Visible = true;

            if (rdorequest1.Checked == true)
            {
                lblRequest.Text = "ボタンをつけない";

            }
            else
            {

                lblRequest.Text = "ボタンをつける";
            }

            rdoProduct_Inquire1.Visible = false;
            rdoProduct_Inquire2.Visible = false;

            lblProductinquiry.Visible = true;

            if (rdoProduct_Inquire1.Checked == true)
            {
                lblProductinquiry.Text = "ボタンをつけない";
            }
            else
            {
                lblProductinquiry.Text = "ボタンをつける";
            }

            lblComingSoon.Visible = true;
            rdocoming.Visible = false;
            rdocoming2.Visible = false;

            if (rdocoming.Checked == true)
            {
                lblComingSoon.Text = "ボタンをつけない";
            }
            else
            {
                lblComingSoon.Text = "ボタンをつける";
            }


            rdomobile1.Visible = false;
            rdomobile2.Visible = false;

            lblMobileDisplay.Visible = true;

            if(rdomobile1.Checked==true)
            {
                lblMobileDisplay.Text = "表示する";
            }
            else{

                lblMobileDisplay.Text = "表示しない";
            }

            rdoexpand1.Visible = false;
            rdoexpand2.Visible = false;
            lblworkCorresponding.Visible = true;

            if(rdoexpand2.Checked==true)
            {
                lblworkCorresponding.Text = "対応する";
            }
            else
            {
                lblworkCorresponding.Text = "対応しない";
            }

            txtanimation.Visible = false;
            lblAnimation.Visible = true;
            lblAnimation.Text = txtanimation.Text;

            txtaccept.Visible = false;
            lblnoOfAcceptance.Visible = true;
            lblnoOfAcceptance.Text = txtaccept.Text;


            lblStockType.Visible = true;
            rdoStockType1.Visible = false;
            rdoStockType2.Visible = false;
            rdoStockType3.Visible = false;

            if (rdoStockType1.Checked == true)
            {
                lblStockType.Text = "在庫設定しない";
            }
            else if (rdoStockType2.Checked == true)
            {
                lblStockType.Text = "通常在庫設定";
            }
            else
            {
                lblStockType.Text = "項目選択肢別在庫設定";
            }


            txtstockno.Visible = false;
            lblStockQuantity.Visible = true;
            lblStockQuantity.Text = txtstockno.Text;

            rdostockquantity1.Visible = false;
            rdostockquantity2.Visible = false;
            lblStocknoDisplay.Visible = true;

            if (rdostockquantity1.Checked == true)
            {
                lblStocknoDisplay.Text = "残り在庫数表示しない";
            }
            else
            {
                lblStocknoDisplay.Text = "残り在庫数表示する";

            }

            txthozitemname.Visible = false;

            lblHorizonalItemName.Visible = true;


            lblHorizonalItemName.Text = txthozitemname.Text;


            txtverticalitemname.Visible = false;
            lblVerticalItemName.Visible = true;
            lblVerticalItemName.Text = txtverticalitemname.Text;


            txtremainstock.Visible = false;
            lblRemainStock.Visible = true;

            lblRemainStock.Text = txtremainstock.Text;

            txtRACno.Visible = false;
            lblRacno.Visible = true;

            txtRACno.Text = lblRacno.Text;

            txtcatalogID.Visible = false;
            lblCatalogID.Visible = true;
            lblCatalogID.Text = txtcatalogID.Text;


            rdoFlagback1.Visible = false;
            rdoFlagback2.Visible = false;
            lblFlagBack.Visible = true;

            if (rdoFlagback1.Checked == true)
            {
                lblFlagBack.Text = "利用しない";

            }
            else
            {
                lblFlagBack.Text = "利用する";

            }



            rdoAccept1.Visible = false;
            rdoAccept2.Visible = false;

            lblAcceptAtOutofStockTime.Visible = true;

             if(rdoAccept1.Checked==true)
            {
                lblAcceptAtOutofStockTime.Text = "受け付けない";
            }
            else
            {
                lblAcceptAtOutofStockTime.Text = "受け付ける";
            }


             txtdeliveryctrno.Visible = false;
             lblDeliveryControlNo.Visible = true;
             lblDeliveryControlNo.Text = txtdeliveryctrno.Text;


             txtdeliveryctrno_outofstock.Visible = false;

             lbldeliveryctrno_outofstock.Visible = true;
             lbldeliveryctrno_outofstock.Text = txtdeliveryctrno_outofstock.Text;

             
           
             lblReservationReleasedDate.Visible = true;
          
             ImageButton1.Visible = false;
           
             lblReservationReleasedDate.Text =txtbookorder.Text;
             txtbookorder.Visible = false;

             if (!String.IsNullOrWhiteSpace(Request.Form[txtbookorder.UniqueID]))
             {
                 string strDate = Request.Form[txtbookorder.UniqueID];

                 DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                 dtfi.ShortDatePattern = "dd-MM-yyyy";
                 dtfi.DateSeparator = "-";
                 DateTime objDate = Convert.ToDateTime(strDate);
                 string date = Convert.ToDateTime(objDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy hh:MM:ss");
                 objDate = DateTime.ParseExact(date, "MM/dd/yyyy hh:MM:ss", null);
                 lblReservationReleasedDate.Text = objDate.ToString();
                 CustomHiddenField.Value = date;

             }

             else
             {
                 lblReservationReleasedDate.Text = null;
             }
            
            
            
            txthfooter.Visible = false;
             lblheaderfooter.Visible = true;
             lblheaderfooter.Text = txthfooter.Text;


             txtdisplayorder.Visible = false;
             lblDisplayOrder.Visible = true;
             lblDisplayOrder.Text = txtdisplayorder.Text;


             txtcomon1.Visible = false;
             lblCommomDescri.Visible = true;

             lblCommomDescri.Text = txtcomon1.Text;


             txtcommon2.Visible = false;
             lblCommonDescrLarge.Visible = true;
             lblCommonDescrLarge.Text = txtcommon2.Text;


             rdoReview1.Visible = false;
             rdoReview2.Visible = false;
             rdoReview3.Visible = false;

             lblReviewTextDisplay.Visible = true;

             if (rdoReview1.Checked == true)
             {

                 lblReviewTextDisplay.Text = "表示しない";

             }

             else if (rdoReview2.Checked == true)
             {
                 lblReviewTextDisplay.Text = "表示する";
             }

             else
             {
                 lblReviewTextDisplay.Text = "デザイン設定での設定を使用";
             }



             txtoversea.Visible = false;
             lblOverseaDeliveryCtrlNo.Visible = true;
             lblOverseaDeliveryCtrlNo.Text = txtoversea.Text;

             txtsizechart.Visible = false;
             lblSizeChartLink.Visible = true;
             lblSizeChartLink.Text = txtsizechart.Text;

             lblDrugDescript.Visible = true;
             txtdrugdesc.Visible = false;
             lblDrugDescript.Text = txtdrugdesc.Text;


             txtdrugnote.Visible = false;
             lblDrugNote.Visible = true;
             lblDrugNote.Text = txtdrugnote.Text;


             txtproductinfoLayout.Visible = false;
             lblproInfo.Visible = true;
             lblproInfo.Text = txtproductinfoLayout.Text;
             
        }



        protected void SetData() 
        {

            rentity = new Mall_Setting_Rakhutan_Fixed_Entity();
            if (Request.QueryString["Shop_ID"] != null) 
            {
                rentity.ShopID = Convert.ToInt32(Request.QueryString["Shop_ID"].ToString());
                
            }

            rentity.Mall_Name = lblMallName.Text;

            rentity.Shop_Name = lblShopName.Text;


            rentity.TagID = txttagiD.Text;

           
            if(rdoConsumption1.Checked)
            {
                rentity.Comsumption_Tax =1;
            }
            else
            {
                rentity.Comsumption_Tax =0;
            }
            
           // rentity.Comsumption_Tax = rdoconsumption.SelectedIndex;



            rentity.ShipCat1 = txtship1.Text;
            rentity.ShipCat2 = txtship2.Text;
            rentity.OrderInfo = txtproductinfoLayout.Text;

            if(rdoOrder1.Checked)
            {
                rentity.Orderbuttton=0;
            }
            else if (rdoOrder2.Checked)
            {
                rentity.Orderbuttton = 1;
            }
            else if (rdoOrder3.Checked)
            {
                rentity.Orderbuttton = 2;
            }

        
            //rentity.Orderbuttton = rdoorder.SelectedIndex;

            if(rdorequest1.Checked)
            {
                rentity.Requestbutton = 0;
            }else
            {
                rentity.Requestbutton =1;
            }


            //rentity.Requestbutton = rdorequest.SelectedIndex;

            if(rdoProduct_Inquire1.Checked)
            {

                rentity.ProductInquerybutton = 0;
            }
            else
            {
                rentity.ProductInquerybutton =1;
            }

            //rentity.ProductInquerybutton = rdoproduct.SelectedIndex;

            if(rdocoming2.Checked)
            {
                rentity.Comingsoonbut = 1;
            }
            else
            {
                rentity.Comingsoonbut = 0;
            }


           // rentity.Comingsoonbut = rdocoming.SelectedIndex;

            //rentity.Mobile = rdomobile.SelectedIndex;


            if(rdomobile1.Checked)

            {
                rentity.Mobile=1;
            }
            else
            {
                rentity.Mobile=0;
            }


            //rentity.Expandcope = rdoexpand.SelectedIndex;


            if(rdoexpand1.Checked)
            {

                rentity.Expandcope =0;
            }

            else 
            {
                rentity.Expandcope=1;

            }


            rentity.Animation = txtanimation.Text;

            rentity.AcceptNo = txtaccept.Text;

            if(rdoStockType1.Checked)
            {
                rentity.Stocktype =0;
            }

            else if (rdoStockType2.Checked)
            {
                rentity.Stocktype = 1;

            }

            else
            {
                rentity.Stocktype = 2;
            }
            
            //rentity.Stocktype = rdostocktype.SelectedIndex;
            rentity.StocknoDisplay = txtstockno.Text;

            //rentity.Stockquantity = rdostockquantity.SelectedIndex;

            if(rdostockquantity1.Checked)
            {
                rentity.Stockquantity = 0;
            }

            else if(rdostockquantity2.Checked)
            {
                rentity.Stockquantity = 1;
            }
            rentity.Hozitemname = txthozitemname.Text;
            rentity.VarItemname = txtverticalitemname.Text;
            rentity.Remainstock = txtremainstock.Text;
            rentity.RACNO = txtRACno.Text;
            rentity.CatID = txtcatalogID.Text;

           // rentity.Flagback = rdoflagback.SelectedIndex;

            if(rdoFlagback2.Checked)
            {
                rentity.Flagback =1;

            }
            else
            {
                rentity.Flagback =0;
            }

            //if (rdoOrder1.Checked)
            //{
            //    rentity.Orderrecpt = 0;

            //}
            //else if (rdoOrder2.Checked)
            //{
            //    rentity.Orderrecpt = 1;
            //}
            //else
            //{
            //    rentity.Orderrecpt = 2;

            //}
            if (rdoAccept1.Checked)
            {
                rentity.Orderrecpt = 0;

            }
            else if (rdoAccept2.Checked)
            {
                rentity.Orderrecpt = 1;
            }
         
            
            //rentity.Orderrecpt = rdoorder.SelectedIndex;

            rentity.DelctrNo = txtdeliveryctrno.Text;

            rentity.DelctrNo_outofstock = txtdeliveryctrno_outofstock.Text;

      

            

            if (!String.IsNullOrWhiteSpace(CustomHiddenField.Value))
            {

                rentity.Orderrelease = Convert.ToDateTime(CustomHiddenField.Value);
                CustomHiddenField.Value = String.Empty;
            }
            else

                rentity.Orderrelease = null ;


           
            rentity.Headfooter = txthfooter.Text;
            rentity.Displayorder = txtdisplayorder.Text;
            rentity.Commondesc1 = txtcomon1.Text;
            rentity.Commondesc2 = txtcommon2.Text;

            if(rdoReview1.Checked)
            {
                rentity.Reviewtax=0;
            }
            else if(rdoReview2.Checked)
            {
                rentity.Reviewtax=1;
            }
            else if (rdoReview3.Checked)
            {
                rentity.Reviewtax = 2;

            }


            //rentity.Reviewtax = rdorewiewtext.SelectedIndex;

            rentity.Oversea = txtoversea.Text;
            rentity.Chartlink = txtsizechart.Text;
            rentity.Drugdesc = txtdrugdesc.Text;
            rentity.Drugnote = txtdrugnote.Text;

        
        }

        protected void GetData(Mall_Setting_Rakhutan_Fixed_Entity rentity)
        {
            txttagiD.Text = rentity.TagID;

            //rdoconsumption.SelectedIndex = rentity.Comsumption_Tax;

            lblMallName.Text = rentity.Mall_Name;


            if (rentity.Comsumption_Tax == 1)
            {
                rdoConsumption1.Checked = true;
            }

            else
            {
                rdoConsumption2.Checked = true;
            }

            lblShopName.Text = rentity.Shop_Name;

           // lblMall.Text = rentity.Shop_ID;

            txtship1.Text = rentity.ShipCat1;
            txtship2.Text = rentity.ShipCat2;


            txtproductinfoLayout.Text = rentity.OrderInfo;

            txtdeliveryctrno_outofstock.Text = rentity.DelctrNo_outofstock;
                       
            txtbookorder.Text = Convert.ToString(rentity.Orderrelease);
                     
            //rdoorder.SelectedIndex=   rentity.Orderbuttton ;

            if (rentity.Orderbuttton == 1)
            {
                rdoOrder2.Checked = true;
            }

            else if (rentity.Orderbuttton == 0)
            {
                rdoOrder1.Checked = true;
                rdoOrder2.Checked = false;
            }

            else
            {
                rdoOrder3.Checked = true;
                rdoOrder2.Checked = false;
            }

           //rdorequest.SelectedIndex=  rentity.Requestbutton;

            if (rentity.Requestbutton == 1)
            {
                rdorequest2.Checked = true;
            }
            else
            {
                rdorequest1.Checked = true;
                rdorequest2.Checked = false;
            }

            //rdoproduct.SelectedIndex= rentity.ProductInquerybutton ;

               if (rentity.ProductInquerybutton== 1)
               {

                   rdoProduct_Inquire2.Checked = true;
              }
            else
            {
                rdoProduct_Inquire1.Checked = true;
                rdoProduct_Inquire2.Checked = false;
            }

          
            if (rentity.Comingsoonbut == 1)
               {
                   rdocoming2.Checked= true;

               }
               else
               {

                   rdocoming.Checked = true;
                   rdocoming2.Checked = false;
               }
            //if (rdocoming.Checked)
            //{
            //    rentity.Comingsoonbut = 1;

            //}
            //else
            //{

            //    rentity.Comingsoonbut = 0;

            //}

            if (rentity.Mobile==1)
            {
                rdomobile1.Checked = true;
            }

            else if (rentity.Mobile == 0)
            {
                rdomobile2.Checked = true;
                rdomobile1.Checked = false;
              }

          //  rdomobile.SelectedIndex = rentity.Mobile;

            //rdoexpand.SelectedIndex = rentity.Expandcope;

            if (rentity.Expandcope==1)
            {
                rdoexpand2.Checked = true;
            }

            if (rentity.Expandcope == 0)
            {
                rdoexpand1.Checked = true;
                rdoexpand2.Checked = false;
            }


            txtanimation.Text = rentity.Animation;
            txtaccept.Text = rentity.AcceptNo;


            //rdostocktype.SelectedIndex = rentity.Stocktype;


            if (rentity.Stocktype == 1)
            {
                rdoStockType2.Checked = true;
                rdoStockType3.Checked = false;
            }
            else if (rentity.Stocktype == 0)
            {
                rdoStockType1.Checked = true;
                rdoStockType3.Checked = false;
            }
            else if (rentity.Stocktype == 2)
            {
                rdoStockType3.Checked = true;
            }


            txtstockno.Text = rentity.StocknoDisplay;

            
           // rdostockquantity.SelectedIndex = rentity.Stockquantity;

            if (rentity.Stockquantity==1)
            {
                rdostockquantity2.Checked = true;
            }

            else if (rentity.Stockquantity == 0)
            {
                rdostockquantity1.Checked = true;
                rdostockquantity2.Checked = false;
            }



            txthozitemname.Text = rentity.Hozitemname;
            txtverticalitemname.Text = rentity.VarItemname;
            txtremainstock.Text = rentity.Remainstock;
            txtRACno.Text = rentity.RACNO;
            txtcatalogID.Text = rentity.CatID;

            if (rentity.Flagback==1)
            {

                rdoFlagback2.Checked = true;
            }
            else 
            {
                rdoFlagback1.Checked = true;
                rdoFlagback2.Checked = false;
            }



            //rdoflagback.SelectedIndex = rentity.Flagback;
            // rdoorder.SelectedIndex= rentity.Orderrecpt ;

            if (rentity.Orderrecpt==0)
            {

                rdoAccept1.Checked = true;
                rdoAccept2.Checked = false;
            }

            else if (rentity.Orderrecpt ==1)
            {
                rdoAccept2.Checked = true;

            }

                      
                txtdeliveryctrno.Text = rentity.DelctrNo;

               
                if(rentity.Reviewtax==1)
                {
                    rdoReview2.Checked = true;
                }

                else if(rentity.Reviewtax==0)
                {
                    rdoReview1.Checked = true;
                    rdoReview2.Checked = false;
                }

                else if (rentity.Reviewtax == 2)
                {
                    rdoReview3.Checked = true;
                    rdoReview2.Checked = false;
                }


                txthfooter.Text = rentity.Headfooter;

                txtcomon1.Text = rentity.Commondesc1;

                txtcommon2.Text = rentity.Commondesc2;




                txtoversea.Text = rentity.Oversea;
                txtsizechart.Text = rentity.Chartlink;
                txtdrugdesc.Text = rentity.Drugdesc;
                txtdrugnote.Text = rentity.Drugnote;

                txtdisplayorder.Text = rentity.Displayorder;



                  

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if ((btnConfirm_Save.Text.Equals("確認画面へ")) && (Request.QueryString["Shop_ID"]== null))
            {

                btnConfirm_Save.Text = "登録";

            }
            else if ((btnConfirm_Save.Text.Equals("登録")) || (btnConfirm_Save.Text.Equals("更新")))
            {

                rbl = new Mall_Setting_Rakhutan_Fixed_BL();
                String result = null;
                if (Request.QueryString["Shop_ID"] != null)
                {
                   
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
                    result = rbl.Insert(rentity);
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

    

       protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
       {
           txtbookorder.Text = String.Empty;
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