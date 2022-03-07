using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using ORS_RCM_Common;
using System.Data;

namespace ORS_RCM.WebForms.Promotion
{
    public partial class Details_of_Promotionexhibition_Yahoo : System.Web.UI.Page
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
        public int Promotion_Type
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

        Details_of_Promotion_Yahoo_Exhibition_BL detailPYbl;
        Details_of_Promotion_Exhibition_Yahoo__Entity detailPYentity;
        String itid = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                detailPYbl = new Details_of_Promotion_Yahoo_Exhibition_BL();
                if (Item_ID != null)
                    itid = Convert.ToString(Item_ID);
                if (Promotion_Type == 1)//Campaign
                {
                    if (Csv_Type == 0)
                    {
                        Label1.Text = "data.csv";
                        data_spy_csv.Text = "data.csv";
                        data_spy_csv1.Text = "data.csv";
                        //Label1.Visible = false;
                        //Label3.Visible = false;
                        //data_spy_csv.Visible = false;
                        //data_add.Visible = false;

                        DataTable dt = detailPYbl.SelectByDetailGetDataPromotionCamapign(itid, Shop_ID,"item");
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            lblpath.Text = dt.Rows[0]["path"].ToString();
                            lblSubCode.Text = dt.Rows[0]["sub-code"].ToString();
                            lblOriginalPrice.Text = dt.Rows[0]["original-price"].ToString();
                            lblregularprice.Text = dt.Rows[0]["price"].ToString();
                            lbloption.Text = dt.Rows[0]["options"].ToString();
                            lblheadline.Text = dt.Rows[0]["headline"].ToString();
                            lblcaption.Text = dt.Rows[0]["caption"].ToString();
                            lblabstract.Text = dt.Rows[0]["abstract"].ToString();
                            lblexplanation.Text = dt.Rows[0]["explanation"].ToString();
                            lblRelevantLink.Text = dt.Rows[0]["relevant-links"].ToString();
                            lblWeight.Text = dt.Rows[0]["ship-weight"].ToString();
                            lblTemporarypoint.Text = dt.Rows[0]["temporary-point-term"].ToString();
                            lblpointCode.Text = dt.Rows[0]["point-code"].ToString();
                            lblmetaKeyword.Text = dt.Rows[0]["meta-key"].ToString();
                            lblmetaDescription.Text = dt.Rows[0]["meta-desc"].ToString();
                            lblReleaseDate.Text = dt.Rows[0]["release-date"].ToString();
                            lblTemplate.Text = dt.Rows[0]["template"].ToString();
                            lblSalePeriodStart.Text = dt.Rows[0]["sale-period-start"].ToString();
                            lblSaleperiodEnd.Text = dt.Rows[0]["sale-period-end"].ToString();
                            lblSaleLimit.Text = dt.Rows[0]["sale-limit"].ToString();
                            lblspcode.Text = dt.Rows[0]["sp-code"].ToString();
                            lblYahooCode.Text = dt.Rows[0]["yahoo-product-code"].ToString();
                            lblProductCode.Text = dt.Rows[0]["product-code"].ToString();
                            lblIsbnCode.Text = dt.Rows[0]["isbn"].ToString();
                            lbldelivery.Text = dt.Rows[0]["delivery"].ToString();
                            lblastk_code.Text = dt.Rows[0]["astk-code"].ToString();
                            lblCondition.Text = dt.Rows[0]["condition"].ToString();
                            lbltao.Text = dt.Rows[0]["taojapan"].ToString();
                            lblproductcat.Text = dt.Rows[0]["product-category"].ToString();
                            lblspec1.Text = dt.Rows[0]["spec1"].ToString();
                            lblspec2.Text = dt.Rows[0]["spec2"].ToString();
                            lblspec3.Text = dt.Rows[0]["spec3"].ToString();
                            lblspec4.Text = dt.Rows[0]["spec4"].ToString();
                            lblspec5.Text = dt.Rows[0]["spec5"].ToString();
                            lblDisplay.Text = dt.Rows[0]["display"].ToString();

                            lblSaleprice.Text = dt.Rows[0]["sale-price"].ToString();
                            lbladdition1.Text = dt.Rows[0]["additional1"].ToString();
                            lbladdition2.Text = dt.Rows[0]["additional2"].ToString();
                            lbladdition3.Text = dt.Rows[0]["additional3"].ToString();
                            lblTaxable.Text = dt.Rows[0]["taxable"].ToString();
                            lblCondition.Text = dt.Rows[0]["condition"].ToString();

                            lblItemCode.Text = dt.Rows[0]["code"].ToString();
                            lblItemName.Text = dt.Rows[0]["name"].ToString();
                            lblJanCode.Text = dt.Rows[0]["jan"].ToString();
                            lblBrandCode.Text = dt.Rows[0]["brand-code"].ToString();

                        }

                    }

                    else if (Csv_Type == 1)
                       
                    {
                        Label1.Text = "data.csv";
                        data_spy_csv.Text = "data.csv";
                        data_spy_csv1.Text = "data.csv";
                        DataTable dtc = detailPYbl.SelectByDetailGetDataPromotionCamapign(itid, Shop_ID,"item_R");
                        if (dtc != null && dtc.Rows.Count > 0)
                        {
                            lblpath.Text = dtc.Rows[0]["path"].ToString();
                            lblSubCode.Text = dtc.Rows[0]["sub-code"].ToString();
                            lblOriginalPrice.Text = dtc.Rows[0]["original-price"].ToString();
                            lblregularprice.Text = dtc.Rows[0]["price"].ToString();
                            lbloption.Text = dtc.Rows[0]["options"].ToString();
                            lblheadline.Text = dtc.Rows[0]["headline"].ToString();
                            lblcaption.Text = dtc.Rows[0]["caption"].ToString();
                            lblabstract.Text = dtc.Rows[0]["abstract"].ToString();
                            lblexplanation.Text = dtc.Rows[0]["explanation"].ToString();
                            lblRelevantLink.Text = dtc.Rows[0]["relevant-links"].ToString();
                            lblWeight.Text = dtc.Rows[0]["ship-weight"].ToString();
                            lblTemporarypoint.Text = dtc.Rows[0]["temporary-point-term"].ToString();
                            lblpointCode.Text = dtc.Rows[0]["point-code"].ToString();
                            lblmetaKeyword.Text = dtc.Rows[0]["meta-key"].ToString();
                            lblmetaDescription.Text = dtc.Rows[0]["meta-desc"].ToString();
                            lblReleaseDate.Text = dtc.Rows[0]["release-date"].ToString();
                            lblTemplate.Text = dtc.Rows[0]["template"].ToString();
                            lblSalePeriodStart.Text = dtc.Rows[0]["sale-period-start"].ToString();
                            lblSaleperiodEnd.Text = dtc.Rows[0]["sale-period-end"].ToString();
                            lblSaleLimit.Text = dtc.Rows[0]["sale-limit"].ToString();
                            lblspcode.Text = dtc.Rows[0]["sp-code"].ToString();
                            lblYahooCode.Text = dtc.Rows[0]["yahoo-product-code"].ToString();
                            lblProductCode.Text = dtc.Rows[0]["product-code"].ToString();
                            lblIsbnCode.Text = dtc.Rows[0]["isbn"].ToString();
                            lbldelivery.Text = dtc.Rows[0]["delivery"].ToString();
                            lblastk_code.Text = dtc.Rows[0]["astk-code"].ToString();
                            lblCondition.Text = dtc.Rows[0]["condition"].ToString();
                            lbltao.Text = dtc.Rows[0]["taojapan"].ToString();
                            lblproductcat.Text = dtc.Rows[0]["product-category"].ToString();
                            lblspec1.Text = dtc.Rows[0]["spec1"].ToString();
                            lblspec2.Text = dtc.Rows[0]["spec2"].ToString();
                            lblspec3.Text = dtc.Rows[0]["spec3"].ToString();
                            lblspec4.Text = dtc.Rows[0]["spec4"].ToString();
                            lblspec5.Text = dtc.Rows[0]["spec5"].ToString();
                            lblDisplay.Text = dtc.Rows[0]["display"].ToString();

                            lblSaleprice.Text = dtc.Rows[0]["sale-price"].ToString();
                            lbladdition1.Text = dtc.Rows[0]["additional1"].ToString();
                            lbladdition2.Text = dtc.Rows[0]["additional2"].ToString();
                            lbladdition3.Text = dtc.Rows[0]["additional3"].ToString();
                            lblTaxable.Text = dtc.Rows[0]["taxable"].ToString();
                            lblCondition.Text = dtc.Rows[0]["condition"].ToString();

                            lblItemCode.Text = dtc.Rows[0]["code"].ToString();
                            lblItemName.Text = dtc.Rows[0]["name"].ToString();
                            lblJanCode.Text = dtc.Rows[0]["jan"].ToString();
                            lblBrandCode.Text = dtc.Rows[0]["brand-code"].ToString();
                        }

                    }// remove
                }
                else if (Promotion_Type == 2)//Point
                {
                    getPromotionPointData(Csv_Type);

                }
                else if (Promotion_Type == 3)//Delivery
                {
                    getDeliverydata(Csv_Type);
                }
            }

        }
        public void getPromotionPointData(int csvtype)
        {
            detailPYbl = new Details_of_Promotion_Yahoo_Exhibition_BL();
            itid = Convert.ToString(Item_ID);

            if (csvtype == 0)
            {
                Label1.Text="data_spy.csv";
                data_spy_csv.Text = "data_spy.csv";
                data_spy_csv1.Text="data_spy.csv";
                //Label2.Visible = false;
                //data_add.Visible = false;
                //Label3.Visible = false;
                //data.Visible = false;
                DataTable dt = detailPYbl.SelectByDetailGetDataPromotionPoint(itid, Shop_ID, 1);//1.1 item.csv
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblpath.Text = dt.Rows[0]["path"].ToString();
                    lblSubCode.Text = dt.Rows[0]["sub-code"].ToString();
                    lblOriginalPrice.Text = dt.Rows[0]["original-price"].ToString();
                    lblregularprice.Text = dt.Rows[0]["price"].ToString();
                    lbloption.Text = dt.Rows[0]["options"].ToString();
                    lblheadline.Text = dt.Rows[0]["headline"].ToString();
                    lblcaption.Text = dt.Rows[0]["caption"].ToString();
                    lblabstract.Text = dt.Rows[0]["abstract"].ToString();
                    lblexplanation.Text = dt.Rows[0]["explanation"].ToString();
                    lblRelevantLink.Text = dt.Rows[0]["relevant-links"].ToString();
                    lblWeight.Text = dt.Rows[0]["ship-weight"].ToString();
                    lblTemporarypoint.Text = dt.Rows[0]["temporary-point-term"].ToString();
                    lblpointCode.Text = dt.Rows[0]["point-code"].ToString();
                    lblmetaKeyword.Text = dt.Rows[0]["meta-key"].ToString();
                    lblmetaDescription.Text = dt.Rows[0]["meta-desc"].ToString();
                    lblReleaseDate.Text = dt.Rows[0]["release-date"].ToString();
                    lblTemplate.Text = dt.Rows[0]["template"].ToString();
                    lblSalePeriodStart.Text = dt.Rows[0]["sale-period-start"].ToString();
                    lblSaleperiodEnd.Text = dt.Rows[0]["sale-period-end"].ToString();
                    lblSaleLimit.Text = dt.Rows[0]["sale-limit"].ToString();
                    lblspcode.Text = dt.Rows[0]["sp-code"].ToString();
                    lblYahooCode.Text = dt.Rows[0]["yahoo-product-code"].ToString();
                    lblProductCode.Text = dt.Rows[0]["product-code"].ToString();
                    lblIsbnCode.Text = dt.Rows[0]["isbn"].ToString();
                    lbldelivery.Text = dt.Rows[0]["delivery"].ToString();
                    lblastk_code.Text = dt.Rows[0]["astk-code"].ToString();
                    lblCondition.Text = dt.Rows[0]["condition"].ToString();
                    lbltao.Text = dt.Rows[0]["taojapan"].ToString();
                    lblproductcat.Text = dt.Rows[0]["product-category"].ToString();
                    lblspec1.Text = dt.Rows[0]["spec1"].ToString();
                    lblspec2.Text = dt.Rows[0]["spec2"].ToString();
                    lblspec3.Text = dt.Rows[0]["spec3"].ToString();
                    lblspec4.Text = dt.Rows[0]["spec4"].ToString();
                    lblspec5.Text = dt.Rows[0]["spec5"].ToString();
                    lblDisplay.Text = dt.Rows[0]["display"].ToString();

                    lblSaleprice.Text = dt.Rows[0]["sale-price"].ToString();
                    lbladdition1.Text = dt.Rows[0]["additional1"].ToString();
                    lbladdition2.Text = dt.Rows[0]["additional2"].ToString();
                    lbladdition3.Text = dt.Rows[0]["additional3"].ToString();
                    lblTaxable.Text = dt.Rows[0]["taxable"].ToString();
                    lblCondition.Text = dt.Rows[0]["condition"].ToString();

                    lblItemCode.Text = dt.Rows[0]["code"].ToString();
                    lblItemName.Text = dt.Rows[0]["name"].ToString();
                    lblJanCode.Text = dt.Rows[0]["jan"].ToString();
                    lblBrandCode.Text = dt.Rows[0]["brand-code"].ToString();


                }
            }
            else
            {
                Label1.Text="item.csv";
                data_spy_csv.Text = "item.csv";
                data_spy_csv1.Text="item.csv";
                //Label2.Visible = false;
                //data_add.Visible = false;
                //Label3.Visible = false;
                //data.Visible = false;
                DataTable dtpRemove = detailPYbl.SelectByDetailGetDataPromotionPoint(itid, Shop_ID, 2);///1.1 item.csv
                if (dtpRemove != null && dtpRemove.Rows.Count > 0)
                {
                    lblpath.Text = dtpRemove.Rows[0]["path"].ToString();
                    lblSubCode.Text = dtpRemove.Rows[0]["sub-code"].ToString();
                    lblOriginalPrice.Text = dtpRemove.Rows[0]["original-price"].ToString();
                    lblregularprice.Text = dtpRemove.Rows[0]["price"].ToString();
                    lbloption.Text = dtpRemove.Rows[0]["options"].ToString();
                    lblheadline.Text = dtpRemove.Rows[0]["headline"].ToString();
                    lblcaption.Text = dtpRemove.Rows[0]["caption"].ToString();
                    lblabstract.Text = dtpRemove.Rows[0]["abstract"].ToString();
                    lblexplanation.Text = dtpRemove.Rows[0]["explanation"].ToString();
                    lblRelevantLink.Text = dtpRemove.Rows[0]["relevant-links"].ToString();
                    lblWeight.Text = dtpRemove.Rows[0]["ship-weight"].ToString();
                    lblTemporarypoint.Text = dtpRemove.Rows[0]["temporary-point-term"].ToString();
                    lblpointCode.Text = dtpRemove.Rows[0]["point-code"].ToString();
                    lblmetaKeyword.Text = dtpRemove.Rows[0]["meta-key"].ToString();
                    lblmetaDescription.Text = dtpRemove.Rows[0]["meta-desc"].ToString();
                    lblReleaseDate.Text = dtpRemove.Rows[0]["release-date"].ToString();
                    lblTemplate.Text = dtpRemove.Rows[0]["template"].ToString();
                    lblSalePeriodStart.Text = dtpRemove.Rows[0]["sale-period-start"].ToString();
                    lblSaleperiodEnd.Text = dtpRemove.Rows[0]["sale-period-end"].ToString();
                    lblSaleLimit.Text = dtpRemove.Rows[0]["sale-limit"].ToString();
                    lblspcode.Text = dtpRemove.Rows[0]["sp-code"].ToString();
                    lblYahooCode.Text = dtpRemove.Rows[0]["yahoo-product-code"].ToString();
                    lblProductCode.Text = dtpRemove.Rows[0]["product-code"].ToString();
                    lblIsbnCode.Text = dtpRemove.Rows[0]["isbn"].ToString();
                    lbldelivery.Text = dtpRemove.Rows[0]["delivery"].ToString();
                    lblastk_code.Text = dtpRemove.Rows[0]["astk-code"].ToString();
                    lblCondition.Text = dtpRemove.Rows[0]["condition"].ToString();
                    lbltao.Text = dtpRemove.Rows[0]["taojapan"].ToString();
                    lblproductcat.Text = dtpRemove.Rows[0]["product-category"].ToString();
                    lblspec1.Text = dtpRemove.Rows[0]["spec1"].ToString();
                    lblspec2.Text = dtpRemove.Rows[0]["spec2"].ToString();
                    lblspec3.Text = dtpRemove.Rows[0]["spec3"].ToString();
                    lblspec4.Text = dtpRemove.Rows[0]["spec4"].ToString();
                    lblspec5.Text = dtpRemove.Rows[0]["spec5"].ToString();
                    lblDisplay.Text = dtpRemove.Rows[0]["display"].ToString();

                    lblSaleprice.Text = dtpRemove.Rows[0]["sale-price"].ToString();
                    lbladdition1.Text = dtpRemove.Rows[0]["additional1"].ToString();
                    lbladdition2.Text = dtpRemove.Rows[0]["additional2"].ToString();
                    lbladdition3.Text = dtpRemove.Rows[0]["additional3"].ToString();
                    lblTaxable.Text = dtpRemove.Rows[0]["taxable"].ToString();
                    lblCondition.Text = dtpRemove.Rows[0]["condition"].ToString();

                    lblItemCode.Text = dtpRemove.Rows[0]["code"].ToString();
                    lblItemName.Text = dtpRemove.Rows[0]["name"].ToString();
                    lblJanCode.Text = dtpRemove.Rows[0]["jan"].ToString();
                    lblBrandCode.Text = dtpRemove.Rows[0]["brand-code"].ToString();
                }

            }//remove


        }
        public void getDeliverydata(int csvtype)
        {
            detailPYbl = new Details_of_Promotion_Yahoo_Exhibition_BL();
            itid = Convert.ToString(Item_ID);
            if (csvtype == 0)
            {
                Label1.Text="datadd.csv";
                data_spy_csv.Text = "dtaadd.csv";
                data_spy_csv1.Text="datadd.csv";
                DataTable dtdelivery = detailPYbl.SelectByDetailGetDataPromotionDelivery(itid, Shop_ID, 1);
                if (dtdelivery != null && dtdelivery.Rows.Count > 0)
                {
                    lblpath.Text = dtdelivery.Rows[0]["path"].ToString();
                    lblSubCode.Text = dtdelivery.Rows[0]["sub-code"].ToString();
                    lblOriginalPrice.Text = dtdelivery.Rows[0]["original-price"].ToString();
                    lblregularprice.Text = dtdelivery.Rows[0]["price"].ToString();
                    lbloption.Text = dtdelivery.Rows[0]["options"].ToString();
                    lblheadline.Text = dtdelivery.Rows[0]["headline"].ToString();
                    lblcaption.Text = dtdelivery.Rows[0]["caption"].ToString();
                    lblabstract.Text = dtdelivery.Rows[0]["abstract"].ToString();
                    lblexplanation.Text = dtdelivery.Rows[0]["explanation"].ToString();
                    lblRelevantLink.Text = dtdelivery.Rows[0]["relevant-links"].ToString();
                    lblWeight.Text = dtdelivery.Rows[0]["ship-weight"].ToString();
                    lblTemporarypoint.Text = dtdelivery.Rows[0]["temporary-point-term"].ToString();
                    lblpointCode.Text = dtdelivery.Rows[0]["point-code"].ToString();
                    lblmetaKeyword.Text = dtdelivery.Rows[0]["meta-key"].ToString();
                    lblmetaDescription.Text = dtdelivery.Rows[0]["meta-desc"].ToString();
                    lblReleaseDate.Text = dtdelivery.Rows[0]["release-date"].ToString();
                    lblTemplate.Text = dtdelivery.Rows[0]["template"].ToString();
                    lblSalePeriodStart.Text = dtdelivery.Rows[0]["sale-period-start"].ToString();
                    lblSaleperiodEnd.Text = dtdelivery.Rows[0]["sale-period-end"].ToString();
                    lblSaleLimit.Text = dtdelivery.Rows[0]["sale-limit"].ToString();
                    lblspcode.Text = dtdelivery.Rows[0]["sp-code"].ToString();
                    lblYahooCode.Text = dtdelivery.Rows[0]["yahoo-product-code"].ToString();
                    lblProductCode.Text = dtdelivery.Rows[0]["product-code"].ToString();
                    lblIsbnCode.Text = dtdelivery.Rows[0]["isbn"].ToString();
                    lbldelivery.Text = dtdelivery.Rows[0]["delivery"].ToString();
                    lblastk_code.Text = dtdelivery.Rows[0]["astk-code"].ToString();
                    lblCondition.Text = dtdelivery.Rows[0]["condition"].ToString();
                    lbltao.Text = dtdelivery.Rows[0]["taojapan"].ToString();
                    lblproductcat.Text = dtdelivery.Rows[0]["product-category"].ToString();
                    lblspec1.Text = dtdelivery.Rows[0]["spec1"].ToString();
                    lblspec2.Text = dtdelivery.Rows[0]["spec2"].ToString();
                    lblspec3.Text = dtdelivery.Rows[0]["spec3"].ToString();
                    lblspec4.Text = dtdelivery.Rows[0]["spec4"].ToString();
                    lblspec5.Text = dtdelivery.Rows[0]["spec5"].ToString();
                    lblDisplay.Text = dtdelivery.Rows[0]["display"].ToString();

                    lblSaleprice.Text = dtdelivery.Rows[0]["sale-price"].ToString();
                    lbladdition1.Text = dtdelivery.Rows[0]["additional1"].ToString();
                    lbladdition2.Text = dtdelivery.Rows[0]["additional2"].ToString();
                    lbladdition3.Text = dtdelivery.Rows[0]["additional3"].ToString();
                    lblTaxable.Text = dtdelivery.Rows[0]["taxable"].ToString();
                    lblCondition.Text = dtdelivery.Rows[0]["condition"].ToString();

                    lblItemCode.Text = dtdelivery.Rows[0]["code"].ToString();
                    lblItemName.Text = dtdelivery.Rows[0]["name"].ToString();
                    lblJanCode.Text = dtdelivery.Rows[0]["jan"].ToString();
                    lblBrandCode.Text = dtdelivery.Rows[0]["brand-code"].ToString();
                }
            }//item.csv
            else//for Removecsv
            {
                Label1.Text="dataadd.csv";
                data_spy_csv.Text = "dataadd.csv";
                data_spy_csv1.Text="dataadd.csv";
                DataTable dtdeliveryr = detailPYbl.SelectByDetailGetDataPromotionDelivery(itid, Shop_ID, 2);
                if (dtdeliveryr != null && dtdeliveryr.Rows.Count > 0)
                {
                    lblpath.Text = dtdeliveryr.Rows[0]["path"].ToString();
                    lblSubCode.Text = dtdeliveryr.Rows[0]["sub-code"].ToString();
                    lblOriginalPrice.Text = dtdeliveryr.Rows[0]["original-price"].ToString();
                    lblregularprice.Text = dtdeliveryr.Rows[0]["price"].ToString();
                    lbloption.Text = dtdeliveryr.Rows[0]["options"].ToString();
                    lblheadline.Text = dtdeliveryr.Rows[0]["headline"].ToString();
                    lblcaption.Text = dtdeliveryr.Rows[0]["caption"].ToString();
                    lblabstract.Text = dtdeliveryr.Rows[0]["abstract"].ToString();
                    lblexplanation.Text = dtdeliveryr.Rows[0]["explanation"].ToString();
                    lblRelevantLink.Text = dtdeliveryr.Rows[0]["relevant-links"].ToString();
                    lblWeight.Text = dtdeliveryr.Rows[0]["ship-weight"].ToString();
                    lblTemporarypoint.Text = dtdeliveryr.Rows[0]["temporary-point-term"].ToString();
                    lblpointCode.Text = dtdeliveryr.Rows[0]["point-code"].ToString();
                    lblmetaKeyword.Text = dtdeliveryr.Rows[0]["meta-key"].ToString();
                    lblmetaDescription.Text = dtdeliveryr.Rows[0]["meta-desc"].ToString();
                    lblReleaseDate.Text = dtdeliveryr.Rows[0]["release-date"].ToString();
                    lblTemplate.Text = dtdeliveryr.Rows[0]["template"].ToString();
                    lblSalePeriodStart.Text = dtdeliveryr.Rows[0]["sale-period-start"].ToString();
                    lblSaleperiodEnd.Text = dtdeliveryr.Rows[0]["sale-period-end"].ToString();
                    lblSaleLimit.Text = dtdeliveryr.Rows[0]["sale-limit"].ToString();
                    lblspcode.Text = dtdeliveryr.Rows[0]["sp-code"].ToString();
                    lblYahooCode.Text = dtdeliveryr.Rows[0]["yahoo-product-code"].ToString();
                    lblProductCode.Text = dtdeliveryr.Rows[0]["product-code"].ToString();
                    lblIsbnCode.Text = dtdeliveryr.Rows[0]["isbn"].ToString();
                    lbldelivery.Text = dtdeliveryr.Rows[0]["delivery"].ToString();
                    lblastk_code.Text = dtdeliveryr.Rows[0]["astk-code"].ToString();
                    lblCondition.Text = dtdeliveryr.Rows[0]["condition"].ToString();
                    lbltao.Text = dtdeliveryr.Rows[0]["taojapan"].ToString();
                    lblproductcat.Text = dtdeliveryr.Rows[0]["product-category"].ToString();
                    lblspec1.Text = dtdeliveryr.Rows[0]["spec1"].ToString();
                    lblspec2.Text = dtdeliveryr.Rows[0]["spec2"].ToString();
                    lblspec3.Text = dtdeliveryr.Rows[0]["spec3"].ToString();
                    lblspec4.Text = dtdeliveryr.Rows[0]["spec4"].ToString();
                    lblspec5.Text = dtdeliveryr.Rows[0]["spec5"].ToString();
                    lblDisplay.Text = dtdeliveryr.Rows[0]["display"].ToString();

                    lblSaleprice.Text = dtdeliveryr.Rows[0]["sale-price"].ToString();
                    lbladdition1.Text = dtdeliveryr.Rows[0]["additional1"].ToString();
                    lbladdition2.Text = dtdeliveryr.Rows[0]["additional2"].ToString();
                    lbladdition3.Text = dtdeliveryr.Rows[0]["additional3"].ToString();
                    lblTaxable.Text = dtdeliveryr.Rows[0]["taxable"].ToString();
                    lblCondition.Text = dtdeliveryr.Rows[0]["condition"].ToString();

                    lblItemCode.Text = dtdeliveryr.Rows[0]["code"].ToString();
                    lblItemName.Text = dtdeliveryr.Rows[0]["name"].ToString();
                    lblJanCode.Text = dtdeliveryr.Rows[0]["jan"].ToString();
                    lblBrandCode.Text = dtdeliveryr.Rows[0]["brand-code"].ToString();
                }//item.csv

            }
        }
    }


}
