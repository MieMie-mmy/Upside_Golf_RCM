using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Promotion
{
    public partial class Details_of_Promotionexhibition_Ponpare : System.Web.UI.Page
    {
        public int Shop_ID
        {
            get
            {
                if (Request.QueryString["Shop_ID"] != null)
                {
                    return int.Parse(Request.QueryString["Shop_ID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Item_ID
        {
            get
            {
                if (Request.QueryString["Item_ID"] != null)
                {
                    return int.Parse(Request.QueryString["Item_ID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Pro_Type
        {
            get
            {
                if (Request.QueryString["Type"] != null)
                {
                    return int.Parse(Request.QueryString["Type"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Csv_Type
        {
            get
            {
                if (Request.QueryString["CSV"] != null)
                {
                    return int.Parse(Request.QueryString["CSV"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }
        Promotion_Detail_BL pbl; String itid = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pbl = new Promotion_Detail_BL();
                if (Item_ID != null)
                    itid = Convert.ToString(Item_ID);
                if (Pro_Type == 1)//Campaign
                {
                    lblSalestatus.Visible = false;
                    // lblproductNo.Visible = false;
                    //   lblcatchcopy.Visible = false;
                    lblsellingprice.Visible = false;
                    lblindicateprice.Visible = false;
                    lblcontax.Visible = false;
                    lblpostage.Visible = false;
                    lblShippinggroup1.Visible = false;
                    lblshippinggroup2.Visible = false;
                    lblextrashipping.Visible = false;
                    lblfee.Visible = false;
                    lblexpanCode.Visible = false;
                    lblOrderButton.Visible = false;
                    lblInquirebutton.Visible = false;
                    lblsaleperiod.Visible = false;
                    lblacceptnumberOforder.Visible = false;
                    //  lblproductNo.Visible = false;
                    // lblcatchcopy.Visible = false;
                    lblsellingprice.Visible = false;
                    lblindicateprice.Visible = false;
                    lblcontax.Visible = false;
                    lblpostage.Visible = false;
                    lblShippinggroup1.Visible = false;
                    lblshippinggroup2.Visible = false;
                    lblextrashipping.Visible = false;
                    lblfee.Visible = false;
                    lblexpanCode.Visible = false;
                    lblOrderButton.Visible = false;
                    lblInquirebutton.Visible = false;
                    lblsaleperiod.Visible = false;
                    lblacceptnumberOforder.Visible = false;
                    lblStype.Visible = false;
                    lblStocknumber.Visible = false;
                    lblstockDisplay.Visible = false;
                    lblprodesc1.Visible = false;
                    lblprodesc2.Visible = false;
                    lblprodesctax.Visible = false;
                    lblimgurl.Visible = false;
                    lblmallgID.Visible = false;
                    lblSecretPassoword.Visible = false;
                    lblpointrate.Visible = false;
                    lblHorizontalItemName.Visible = false;
                    lblVertical.Visible = false;
                    lblRemainingStock.Visible = false;
                    //  lblproforphone.Visible = false;
                    lblacceptnumberOforder.Visible = false;
                    lblJunCode.Visible = false;
                    itcat.Visible = false;
                    category.Visible = false;
                    if (Csv_Type == 0)
                    {
                        DataTable dt = pbl.CamapignPonpare(itid, Shop_ID, "item");
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            vsale.Style.Add("display", "none");
                            vselling.Style.Add("display", "none");
                            vprice.Style.Add("display", "none");
                            vcontax.Style.Add("display", "none");
                            vpostage.Style.Add("display", "none");
                            vshipping1.Style.Add("display", "none");
                            vshipping2.Style.Add("display", "none");
                            veshipping.Style.Add("display", "none");
                            vfee.Style.Add("display", "none");
                            vexpancode.Style.Add("display", "none");
                            vorder.Style.Add("display", "none");
                            vinquire.Style.Add("display", "none");
                            vsaleperiod.Style.Add("display", "none");
                            vacceptno.Style.Add("display", "none");
                            vstype.Style.Add("display", "none");
                            vstockno.Style.Add("display", "none");
                            vstockdisplay.Style.Add("display", "none");
                            vprod1.Style.Add("display", "none");
                            vprod2.Style.Add("display", "none");
                            vprodtax.Style.Add("display", "none");
                            vimgurl.Style.Add("display", "none");
                            vmallid.Style.Add("display", "none");
                            vsecretpass.Style.Add("display", "none");
                            vpoint.Style.Add("display", "none");
                            vhorizontal.Style.Add("display", "none");
                            vvertical.Style.Add("display", "none");
                            vstock.Style.Add("display", "none");
                            vjancode.Style.Add("display", "none");
  
                            lblControlColumn.Text = dt.Rows[0]["コントロールカラム"].ToString();
                            lblProductURL.Text = dt.Rows[0]["商品管理ID（商品URL）"].ToString();
                            lblproductID.Text = dt.Rows[0]["商品ID"].ToString();
                            lblproductNo.Text = dt.Rows[0]["商品名"].ToString();
                            lblcatchcopy.Text = dt.Rows[0]["キャッチコピー"].ToString();
                            lblprodesc1.Text = dt.Rows[0]["商品説明(1)"].ToString();
                            lblprodesc2.Text = dt.Rows[0]["商品説明(2)"].ToString();
                            lblproforphone.Text = dt.Rows[0]["商品説明(スマートフォン用)"].ToString();

                        }
                        DataTable dtopt = pbl.CamapignPonpare(itid, Shop_ID, "option");
                        gvoption.DataSource = dtopt;
                        gvoption.DataBind();
                    
                    }
                             else if (Csv_Type == 1)
                    {
                        DataTable dtc = pbl.CamapignPonpare(itid, Shop_ID, "item_R");
                        if (dtc != null && dtc.Rows.Count > 0)
                        {
                            vsale.Style.Add("display", "none");
                            vselling.Style.Add("display", "none");
                            vprice.Style.Add("display", "none");
                            vcontax.Style.Add("display", "none");
                            vpostage.Style.Add("display", "none");
                            vshipping1.Style.Add("display", "none");
                            vshipping2.Style.Add("display", "none");
                            veshipping.Style.Add("display", "none");
                            vfee.Style.Add("display", "none");
                            vexpancode.Style.Add("display", "none");
                            vorder.Style.Add("display", "none");
                            vinquire.Style.Add("display", "none");
                            vsaleperiod.Style.Add("display", "none");
                            vacceptno.Style.Add("display", "none");
                            vstype.Style.Add("display", "none");
                            vstockno.Style.Add("display", "none");
                            vstockdisplay.Style.Add("display", "none");
                          //  vprod1.Style.Add("display", "none");
                          //  vprod2.Style.Add("display", "none");
                            vprodtax.Style.Add("display", "none");
                            vimgurl.Style.Add("display", "none");
                            vmallid.Style.Add("display", "none");
                            vsecretpass.Style.Add("display", "none");
                            vpoint.Style.Add("display", "none");
                            vhorizontal.Style.Add("display", "none");
                            vvertical.Style.Add("display", "none");
                            vstock.Style.Add("display", "none");
                            vjancode.Style.Add("display", "none");

                            lblControlColumn.Text = dtc.Rows[0]["コントロールカラム"].ToString();
                            lblProductURL.Text = dtc.Rows[0]["商品管理ID（商品URL）"].ToString();
                            lblproductID.Text = dtc.Rows[0]["商品ID"].ToString();
                            lblproductNo.Text = dtc.Rows[0]["商品名"].ToString();
                            lblcatchcopy.Text = dtc.Rows[0]["キャッチコピー"].ToString();
                            lblprodesc1.Text = dtc.Rows[0]["商品説明(1)"].ToString();
                            lblprodesc2.Text = dtc.Rows[0]["商品説明(2)"].ToString();
                            lblproforphone.Text = dtc.Rows[0]["商品説明(スマートフォン用)"].ToString();
                        }
                        DataTable dtselectremove = pbl.CamapignPonpare(itid, Shop_ID, "option_R");
                        gvoption.DataSource = dtselectremove;
                        gvoption.DataBind();
                    }//Remove
                }//Campaign
            
     
                    else if (Pro_Type == 2)//Point
                    {
                        getpointdata(Csv_Type);
                    }
                    else if (Pro_Type == 3)//Delivery
                    {
                        getDeliverydata(Csv_Type);
                    }
                }
        }
        public void getpointdata(int csvtype)
        {
            pbl = new Promotion_Detail_BL();
            itid = Convert.ToString(Item_ID);

            if (csvtype == 0)
            {
                lblSalestatus.Visible = false;
               // lblproductNo.Visible = false;
               // lblcatchcopy.Visible = false;
                lblsellingprice.Visible = false;
                lblindicateprice.Visible = false;
                lblcontax.Visible = false;
                lblpostage.Visible = false;
                lblShippinggroup1.Visible = false;
                lblshippinggroup2.Visible = false;
                lblextrashipping.Visible = false;
                lblfee.Visible = false;
                lblexpanCode.Visible = false;
                lblOrderButton.Visible = false;
                lblInquirebutton.Visible = false;
                lblsaleperiod.Visible = false;
                lblacceptnumberOforder.Visible = false;
            //  lblproductNo.Visible = false;
            // lblcatchcopy.Visible = false;
                lblsellingprice.Visible = false;
                lblindicateprice.Visible = false;
                lblcontax.Visible = false;
                lblpostage.Visible = false;
                lblShippinggroup1.Visible = false;
                lblshippinggroup2.Visible = false;
                lblextrashipping.Visible = false;
                lblfee.Visible = false;
                lblexpanCode.Visible = false;
                lblOrderButton.Visible = false;
                lblInquirebutton.Visible = false;
                lblsaleperiod.Visible = false;
                lblacceptnumberOforder.Visible = false;
                lblStype.Visible = false;
                lblStocknumber.Visible = false;
                lblstockDisplay.Visible = false;
                lblprodesc1.Visible = false;
                lblprodesc2.Visible = false;
                lblprodesctax.Visible = false;
                lblimgurl.Visible = false;
                lblmallgID.Visible = false;
                lblSecretPassoword.Visible = false;
                lblpointrate.Visible = false;
                lblHorizontalItemName.Visible = false;
                lblVertical.Visible = false;
                lblRemainingStock.Visible = false;
              //  lblproforphone.Visible = false;
                lblacceptnumberOforder.Visible = false;
                lblJunCode.Visible = false;
                DataTable dtp = pbl.PointPonpare(itid, Shop_ID, 1);//1.1 item.csv
                if (dtp != null && dtp.Rows.Count > 0)
                {
                    vsale.Style.Add("display", "none");
                    vselling.Style.Add("display", "none");
                    vprice.Style.Add("display", "none");
                    vcontax.Style.Add("display", "none");
                    vpostage.Style.Add("display", "none");
                    vshipping1.Style.Add("display", "none");
                    vshipping2.Style.Add("display", "none");
                    veshipping.Style.Add("display", "none");
                    vfee.Style.Add("display", "none");
                    vexpancode.Style.Add("display", "none");
                    vorder.Style.Add("display", "none");
                    vinquire.Style.Add("display", "none");
                    vsaleperiod.Style.Add("display", "none");
                    vacceptno.Style.Add("display", "none");
                    vstype.Style.Add("display", "none");
                    vstockno.Style.Add("display", "none");
                    vstockdisplay.Style.Add("display", "none");
                    vprod1.Style.Add("display", "none");
                    vprod2.Style.Add("display", "none");
                    vprodtax.Style.Add("display", "none");
                    vimgurl.Style.Add("display", "none");
                    vmallid.Style.Add("display", "none");
                    vsecretpass.Style.Add("display", "none");
                   // vpoint.Style.Add("display", "none");
                    vhorizontal.Style.Add("display", "none");
                    vvertical.Style.Add("display", "none");
                    vstock.Style.Add("display", "none");
                    vjancode.Style.Add("display", "none");

                    lblControlColumn.Text = dtp.Rows[0]["コントロールカラム"].ToString();
                    lblProductURL.Text = dtp.Rows[0]["商品管理ID（商品URL)"].ToString();
                    lblproductID.Text = dtp.Rows[0]["商品ID"].ToString();
                    lblpointrate.Text = dtp.Rows[0]["ポイント率"].ToString();
                    lblpointperiod.Text = dtp.Rows[0]["ポイント率適用期間"].ToString();
                    option.Visible = false;
                    category.Visible = false;
                }//1.1 item.csv

                DataTable dtpt = pbl.PointPonpare(itid, Shop_ID, 2);//1.1 item.csv
                if (dtpt != null && dtpt.Rows.Count > 0)
                {
                    vsale.Style.Add("display", "none");
                    vselling.Style.Add("display", "none");
                    vprice.Style.Add("display", "none");
                    vcontax.Style.Add("display", "none");
                    vpostage.Style.Add("display", "none");
                    vshipping1.Style.Add("display", "none");
                    vshipping2.Style.Add("display", "none");
                    veshipping.Style.Add("display", "none");
                    vfee.Style.Add("display", "none");
                    vexpancode.Style.Add("display", "none");
                    vorder.Style.Add("display", "none");
                    vinquire.Style.Add("display", "none");
                    vsaleperiod.Style.Add("display", "none");
                    vacceptno.Style.Add("display", "none");
                    vstype.Style.Add("display", "none");
                    vstockno.Style.Add("display", "none");
                    vstockdisplay.Style.Add("display", "none");
                    vprod1.Style.Add("display", "none");
                    vprod2.Style.Add("display", "none");
                    vprodtax.Style.Add("display", "none");
                    vimgurl.Style.Add("display", "none");
                    vmallid.Style.Add("display", "none");
                    vsecretpass.Style.Add("display", "none");
                   // vpoint.Style.Add("display", "none");
                    vhorizontal.Style.Add("display", "none");
                    vvertical.Style.Add("display", "none");
                    vstock.Style.Add("display", "none");
                    vjancode.Style.Add("display", "none");

                    option.Visible = false;
                    category.Visible = false;
                    lblControlColumn.Text = dtpt.Rows[0]["コントロールカラム"].ToString();
                    lblProductURL.Text = dtpt.Rows[0]["商品管理ID（商品URL)"].ToString();
                    lblproductID.Text = dtpt.Rows[0]["商品ID"].ToString();
                    lblproductNo.Text = dtpt.Rows[0]["商品名"].ToString();
                    lblcatchcopy.Text = dtpt.Rows[0]["キャッチコピー"].ToString();
                    lblproforphone.Text = dtpt.Rows[0]["商品説明(スマートフォン用)"].ToString();
                }//1.1 item.csv
            }//addcsv
            else
            {
               
                lblSalestatus.Visible = false;
                lblsellingprice.Visible = false;
                lblindicateprice.Visible = false;
                lblcontax.Visible = false;
                lblpostage.Visible = false;
                lblShippinggroup1.Visible = false;
                lblshippinggroup2.Visible = false;
                lblextrashipping.Visible = false;
                lblfee.Visible = false;
                lblexpanCode.Visible = false;
                lblOrderButton.Visible = false;
                lblInquirebutton.Visible = false;
                lblsaleperiod.Visible = false;
                lblacceptnumberOforder.Visible = false;
                //  lblproductNo.Visible = false;
                // lblcatchcopy.Visible = false;
                lblsellingprice.Visible = false;
                lblindicateprice.Visible = false;
                lblcontax.Visible = false;
                lblpostage.Visible = false;
                lblShippinggroup1.Visible = false;
                lblshippinggroup2.Visible = false;
                lblextrashipping.Visible = false;
                lblfee.Visible = false;
                lblexpanCode.Visible = false;
                lblOrderButton.Visible = false;
                lblInquirebutton.Visible = false;
                lblsaleperiod.Visible = false;
                lblacceptnumberOforder.Visible = false;
                lblStype.Visible = false;
                lblStocknumber.Visible = false;
                lblstockDisplay.Visible = false;
                lblprodesc1.Visible = false;
                lblprodesc2.Visible = false;
                lblprodesctax.Visible = false;
                lblimgurl.Visible = false;
                lblmallgID.Visible = false;
                lblSecretPassoword.Visible = false;
              //  lblpointrate.Visible = false;
                lblHorizontalItemName.Visible = false;
                lblVertical.Visible = false;
                lblRemainingStock.Visible = false;
                //  lblproforphone.Visible = false;
                lblacceptnumberOforder.Visible = false;
                lblJunCode.Visible = false;
                DataTable dtpremove = pbl.PointPonpare(itid, Shop_ID, 3);//1.1 item.csv
                if (dtpremove != null && dtpremove.Rows.Count > 0)
                {
                    vsale.Style.Add("display", "none");
                    vselling.Style.Add("display", "none");
                    vprice.Style.Add("display", "none");
                    vcontax.Style.Add("display", "none");
                    vpostage.Style.Add("display", "none");
                    vshipping1.Style.Add("display", "none");
                    vshipping2.Style.Add("display", "none");
                    veshipping.Style.Add("display", "none");
                    vfee.Style.Add("display", "none");
                    vexpancode.Style.Add("display", "none");
                    vorder.Style.Add("display", "none");
                    vinquire.Style.Add("display", "none");
                    vsaleperiod.Style.Add("display", "none");
                    vacceptno.Style.Add("display", "none");
                    vstype.Style.Add("display", "none");
                    vstockno.Style.Add("display", "none");
                    vstockdisplay.Style.Add("display", "none");
                    vprod1.Style.Add("display", "none");
                    vprod2.Style.Add("display", "none");
                    vprodtax.Style.Add("display", "none");
                    vimgurl.Style.Add("display", "none");
                    vmallid.Style.Add("display", "none");
                    vsecretpass.Style.Add("display", "none");
                    vpoint.Style.Add("display", "none");
                    vhorizontal.Style.Add("display", "none");
                    vvertical.Style.Add("display", "none");
                    vstock.Style.Add("display", "none");
                    vjancode.Style.Add("display", "none");
                    option.Visible = false;
                    category.Visible = false;
                    lblControlColumn.Text = dtpremove.Rows[0]["コントロールカラム"].ToString();
                    lblProductURL.Text = dtpremove.Rows[0]["商品管理ID（商品URL)"].ToString();
                    lblproductID.Text = dtpremove.Rows[0]["商品ID"].ToString();
                    lblproductNo.Text = dtpremove.Rows[0]["商品名"].ToString();
                    lblcatchcopy.Text = dtpremove.Rows[0]["キャッチコピー"].ToString();
                    lblproforphone.Text = dtpremove.Rows[0]["商品説明(スマートフォン用)"].ToString();
                    lblpointrate.Text = dtpremove.Rows[0]["ポイント率"].ToString();
                    lblpointperiod.Text = dtpremove.Rows[0]["ポイント率適用期間"].ToString();
                }
            }//removecsv
        }
        public void getDeliverydata(int csvtype)
        {
            pbl = new Promotion_Detail_BL();
            itid = Convert.ToString(Item_ID);
            if (csvtype == 0)
            {
                //lblSalestatus.Visible = false;
                //lblproductNo.Visible = false;
                //lblcatchcopy.Visible = false;
                //lblsellingprice.Visible = false;
                //lblindicateprice.Visible = false;
                //lblcontax.Visible = false;
                //lblpostage.Visible = false;
                //lblShippinggroup1.Visible = false;
                //lblshippinggroup2.Visible = false;
                //lblextrashipping.Visible = false;
                //lblfee.Visible = false;
                //lblexpanCode.Visible = false;
                //lblOrderButton.Visible = false;
                //lblInquirebutton.Visible = false;
                //lblsaleperiod.Visible = false;
                //lblacceptnumberOforder.Visible = false;
                //lblproductNo.Visible = false;
                //lblcatchcopy.Visible = false;
                //lblsellingprice.Visible = false;
                //lblindicateprice.Visible = false;
                //lblcontax.Visible = false;
                //lblpostage.Visible = false;
                //lblShippinggroup1.Visible = false;
                //lblshippinggroup2.Visible = false;
                //lblextrashipping.Visible = false;
                //lblfee.Visible = false;
                //lblexpanCode.Visible = false;
                //lblOrderButton.Visible = false;
                //lblInquirebutton.Visible = false;
                //lblsaleperiod.Visible = false;
                //lblacceptnumberOforder.Visible = false;
                //lblStype.Visible = false;
                //lblStocknumber.Visible = false;
                //lblstockDisplay.Visible = false;
                //lblprodesc1.Visible = false;
                //lblprodesc2.Visible = false;
                //lblprodesctax.Visible = false;
                //lblimgurl.Visible = false;
                //lblmallgID.Visible = false;
                //lblSecretPassoword.Visible = false;
                //lblpointrate.Visible = false;
                //lblHorizontalItemName.Visible = false;
                //lblVertical.Visible = false;
                //lblRemainingStock.Visible = false;
                //lblproforphone.Visible = false;
                //lblacceptnumberOforder.Visible = false;
                //lblJunCode.Visible = false;

                //vproducturl.Style.Add("display", "none");
                //vproductid.Style.Add("display", "none");
                //vpoint.Style.Add("display", "none");
                //vpointperiod.Style.Add("display", "none");
                itemcsv.Visible = false;
                item.Visible = false;
                DataTable dtdelselect = pbl.DeliveryPonpare(itid, Shop_ID, 1);
                if (dtdelselect != null && dtdelselect.Rows.Count > 0)
                {
                    option.Visible = true;
                    campaign.Visible = true;
                    gvoption.DataSource = dtdelselect;
                    gvoption.DataBind();
                  
                }//option.csv
                DataTable dtcat = pbl.DeliveryPonpare(itid, Shop_ID, 2);
                if (dtcat != null && dtcat.Rows.Count > 0)
                {
                    category.Visible = true;
                    itcat.Visible = true;
                    gvcat.DataSource = dtcat;
                    gvcat.DataBind();
                }//itemcategory.csv
            }
            else//for Removecsv
            {
                //lblSalestatus.Visible = false;
                //lblproductNo.Visible = false;
                //lblcatchcopy.Visible = false;
                //lblsellingprice.Visible = false;
                //lblindicateprice.Visible = false;
                //lblcontax.Visible = false;
                //lblpostage.Visible = false;
                //lblShippinggroup1.Visible = false;
                //lblshippinggroup2.Visible = false;
                //lblextrashipping.Visible = false;
                //lblfee.Visible = false;
                //lblexpanCode.Visible = false;
                //lblOrderButton.Visible = false;
                //lblInquirebutton.Visible = false;
                //lblsaleperiod.Visible = false;
                //lblacceptnumberOforder.Visible = false;
                //lblproductNo.Visible = false;
                //lblcatchcopy.Visible = false;
                //lblsellingprice.Visible = false;
                //lblindicateprice.Visible = false;
                //lblcontax.Visible = false;
                //lblpostage.Visible = false;
                //lblShippinggroup1.Visible = false;
                //lblshippinggroup2.Visible = false;
                //lblextrashipping.Visible = false;
                //lblfee.Visible = false;
                //lblexpanCode.Visible = false;
                //lblOrderButton.Visible = false;
                //lblInquirebutton.Visible = false;
                //lblsaleperiod.Visible = false;
                //lblacceptnumberOforder.Visible = false;
                //lblStype.Visible = false;
                //lblStocknumber.Visible = false;
                //lblstockDisplay.Visible = false;
                //lblprodesc1.Visible = false;
                //lblprodesc2.Visible = false;
                //lblprodesctax.Visible = false;
                //lblimgurl.Visible = false;
                //lblmallgID.Visible = false;
                //lblSecretPassoword.Visible = false;
                //lblpointrate.Visible = false;
                //lblHorizontalItemName.Visible = false;
                //lblVertical.Visible = false;
                //lblRemainingStock.Visible = false;
                //lblproforphone.Visible = false;
                //lblacceptnumberOforder.Visible = false;
                //lblJunCode.Visible = false;

                //vproducturl.Style.Add("display", "none");
                //vproductid.Style.Add("display", "none");
                //vpoint.Style.Add("display", "none");
                //vpointperiod.Style.Add("display", "none");
                itemcsv.Visible = false;
                item.Visible = false;
                DataTable dtdeloptionr = pbl.DeliveryPonpare(itid, Shop_ID, 3);
                if (dtdeloptionr != null && dtdeloptionr.Rows.Count > 0)
                {
                    option.Visible = true;
                    campaign.Visible = true;
                    gvoption.DataSource = dtdeloptionr;
                    gvoption.DataBind();
                }//select.csv
                DataTable dtcatr = pbl.DeliveryPonpare(itid, Shop_ID, 4);
                if (dtcatr != null && dtcatr.Rows.Count > 0)
                {
                    category.Visible = true;
                    itcat.Visible = true;
                    gvcat.DataSource = dtcatr;
                    gvcat.DataBind();
                }//item-cat.csv
            }
        }
    }
}