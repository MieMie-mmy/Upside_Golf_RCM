using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using ORS_RCM_Common;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM.WebForms.Item_Exhibition
{
    public partial class Details_of_Exhibition_ORSKojima : System.Web.UI.Page
    {
        Details_ORS_Exhibition_BL detailO_bl;
        Exhibition_List_BL ehb; string str;
        public string list; string itemcode;
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
        Details_ORS_Exhibition_BL dbl;
        Details_ORS_Exhibition_Entity entry;
        Exhibition_List_BL elb;
        //string strr;
       // public string list;
        string itemcode1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    dbl = new Details_ORS_Exhibition_BL();
                    DataTable dterror = new DataTable();
                    int id = Shop_ID;
                    hfshopid.Value = Convert.ToString(id);
                    int itemid = Item_ID;
                    string itid = Convert.ToString(itemid);
                     GetData(itemid, id);
                   
                        ehb = new Exhibition_List_BL();
                        itemcode1 = lblProductURL.Text;
                        dterror = ehb.Selectexerror(itemcode, id, 2, itemid);
                        if (dterror != null && dterror.Rows.Count > 0)
                        {
                            for (int i = 0; i < dterror.Rows.Count; i++)
                                lblitemerror.Text += dterror.Rows[i]["Error_Description"].ToString() + "\n\n";
                        }
                        dterror = null;
                        dterror = ehb.Selectexerror(itemcode, id, 1, itemid);
                        if (dterror != null && dterror.Rows.Count > 0)
                        {
                            for (int i = 0; i < dterror.Rows.Count; i++)
                                lblselecterror.Text += dterror.Rows[i]["Error_Description"].ToString() + ",\n\n";
                            //lblselecterror.Text = dterror.Rows[0]["Error_Description"].ToString();
                        }
                        dterror = null;
                        dterror = ehb.Selectexerror(itemcode, id, 0, itemid);
                        if (dterror != null && dterror.Rows.Count > 0)
                        {
                            lblcaterror.Text = dterror.Rows[0]["Error_Description"].ToString();
                        }
                    }
                }
           
            catch (Exception ex)
            {
                Response.Redirect("~/CustomErrorPage.aspx?");

            }
        }

        public void GetData(int itemid, int shopid)
        {
            try
            {
                string Ctrl_ID = string.Empty;
                int ids = Shop_ID;
                string itid = Convert.ToString(itemid);
                DataTable dt = dbl.SelectByExhibitionData(ids, itid, "item");
                if (dt != null && dt.Rows.Count > 0)
                {
                    //if (dt.Rows[0]["コントロールカラム"].ToString() != "d")
                    //{
                       // Ctrl_ID = dt.Rows[0]["コントロールカラム"].ToString(); // to return value;
                       // lblcontrol.Text = dt.Rows[0]["コントロールカラム"].ToString();
                        lblOrderCode.Text = dt.Rows[0]["(注文コード)"].ToString();
                        lblshipment.Text = dt.Rows[0]["(年間出荷数もしくは売れ筋A～Dランク)"].ToString();
                        lblCategoryCode.Text = dt.Rows[0]["(カテゴリコード)"].ToString();
                        lblCategory.Text = dt.Rows[0]["(カテゴリ)"].ToString();
                        lblProductGroupCode.Text = dt.Rows[0]["商品グループコード"].ToString();
                        lblProductGroupName.Text = dt.Rows[0]["(商品グループ名)"].ToString();
                        lblManufacturerPartNo.Text = dt.Rows[0]["メーカー品番"].ToString();
                        lblSaleVolume.Text = dt.Rows[0]["販売数"].ToString();
                        lblSaleUnit.Text = dt.Rows[0]["販売単位"].ToString();
                        lblContentQuantityNumber1.Text = dt.Rows[0]["内容量数1"].ToString();
                        lblContentUnit1.Text = dt.Rows[0]["内容量単位1"].ToString();
                        lblContentQuantityNumber2.Text = dt.Rows[0]["内容量数2"].ToString();
                        lblContentUnit2.Text = dt.Rows[0]["内容量単位2"].ToString();
                        lblJanCode.Text = dt.Rows[0]["JANコード"].ToString();
                        lblReferencePrice.Text = dt.Rows[0]["参考基準価格"].ToString();
                        lblOptionCode.Text = dt.Rows[0]["オプションコード"].ToString();
                        lblDeliveryType.Text = dt.Rows[0]["配送種別"].ToString();
                        lblDaysDelivery.Text = dt.Rows[0]["入荷日数"].ToString();
                        lblDaysShipped.Text = dt.Rows[0]["出荷日数"].ToString();
                        lblCustomerAssembly.Text = dt.Rows[0]["お客様組立て"].ToString();
                        lblDeliveryMethod.Text = dt.Rows[0]["引渡方法"].ToString();
                        lblCashDeliveryFree.Text = dt.Rows[0]["代引可否"].ToString();
                        lblPossibilityofReturn.Text = dt.Rows[0]["返品可否"].ToString();
                        lblOpentype.Text = dt.Rows[0]["公開種別"].ToString();
                        lblMarketSellingPrice.Text = dt.Rows[0]["(市場売価)"].ToString();
                        lblSupplierName.Text = dt.Rows[0]["(サプライヤ名)"].ToString();
                        lblManufacturerName.Text = dt.Rows[0]["(メーカー名)"].ToString();
                        lblbrandname.Text = dt.Rows[0]["(ブランド名)"].ToString();
                        lblSupplierPartNumber.Text = dt.Rows[0]["サプライヤ品番"].ToString();
                        lblMinimumOrderQuantity.Text = dt.Rows[0]["最低発注数量"].ToString();
                        lblMinimumOrderUnit.Text = dt.Rows[0]["最低発注単位"].ToString();
                        lblPurchasePrice.Text = dt.Rows[0]["仕入価格"].ToString();
                        lblnationwide.Text = dt.Rows[0]["(送料-全国)"].ToString();
                        lblHokkaido.Text = dt.Rows[0]["(送料-北海道)"].ToString();
                        lblokinawa.Text = dt.Rows[0]["(送料-沖縄)"].ToString();
                        lblRemoteIsland.Text = dt.Rows[0]["(送料-離島)"].ToString();
                        lblDirectDeliveryera.Text = dt.Rows[0]["(直送時代引き)"].ToString();
                        lblUndeliverableArea.Text = dt.Rows[0]["(直送時配送不可地域)"].ToString();
                        lblPackingWeight.Text = dt.Rows[0]["梱包質量(kg)"].ToString();
                        lblPackingDimensionDepth.Text = dt.Rows[0]["梱包寸法(奥行D)(mm)"].ToString();
                        lblPackingDimensionWidth.Text = dt.Rows[0]["梱包寸法(幅W)(mm)"].ToString();
                        lblPackingDimensionHeight.Text = dt.Rows[0]["梱包寸法(高さH)(mm)"].ToString();
                        lblItemDetailRegistrationComment.Text = dt.Rows[0]["商品詳細登録コメント"].ToString();
                        lblFeatures.Text = dt.Rows[0]["(特長)"].ToString();
                        lblApplication.Text = dt.Rows[0]["(用途)"].ToString();
                        lblFeature1.Text = dt.Rows[0]["(特長)1"].ToString();
                        lblImageStoragePlace.Text = dt.Rows[0]["(画像置き場)"].ToString();
                        lblImage1.Text = dt.Rows[0]["(画像1)"].ToString();
                        lblImage1Caption.Text = dt.Rows[0]["(画像1キャプション)"].ToString();
                        lblImage2.Text = dt.Rows[0]["(画像2)"].ToString();
                        lblImage2Caption.Text = dt.Rows[0]["(画像2キャプション)"].ToString();
                        lblImage3.Text = dt.Rows[0]["(画像3)"].ToString();
                        lblImage3Caption.Text = dt.Rows[0]["(画像3キャプション)"].ToString();
                        lblImage4.Text = dt.Rows[0]["(画像4)"].ToString();
                        lblImage4Caption.Text = dt.Rows[0]["(画像4キャプション)"].ToString();
                        lblApplicableLaw.Text = dt.Rows[0]["該当法令"].ToString();
                        lblSales.Text = dt.Rows[0]["販売許可・認可・届出 "].ToString();
                        lblAttributeName1.Text = dt.Rows[0]["(属性名1)"].ToString();
                        lblAttributeValue1.Text = dt.Rows[0]["(属性値1)"].ToString();
                        lblAttributeName2.Text = dt.Rows[0]["(属性名2)"].ToString();
                        lblAttributeValue2.Text = dt.Rows[0]["(属性値2)"].ToString();
                        lblAttributeName3.Text = dt.Rows[0]["(属性名3)"].ToString();
                        lblAttributeValue3.Text = dt.Rows[0]["(属性値3)"].ToString();
                        lblAttributeName4.Text = dt.Rows[0]["(属性名4)"].ToString();
                        lblAttributeValue4.Text = dt.Rows[0]["(属性値4)"].ToString();
                        lblAttributeName5.Text = dt.Rows[0]["(属性名5)"].ToString();
                        lblAttributeValue5.Text = dt.Rows[0]["(属性値5)"].ToString();
                        lblAttributeName6.Text = dt.Rows[0]["(属性名6)"].ToString();
                        lblAttributeValue6.Text = dt.Rows[0]["(属性値6)"].ToString();
                        lblAttributeName7.Text = dt.Rows[0]["(属性名7)"].ToString();
                        lblAttributeValue7.Text = dt.Rows[0]["(属性値7)"].ToString();
                        lblAttributeName8.Text = dt.Rows[0]["(属性名8)"].ToString();
                        lblAttributeValue8.Text = dt.Rows[0]["(属性値8)"].ToString();
                        lblAttributeName9.Text = dt.Rows[0]["(属性名9)"].ToString();
                        lblAttributeValue9.Text = dt.Rows[0]["(属性値9)"].ToString();
                        lblAttributeName10.Text = dt.Rows[0]["(属性名10)"].ToString();
                        lblAttributeValue10.Text = dt.Rows[0]["(属性値10)"].ToString();
                        lblAttributeName11.Text = dt.Rows[0]["(属性名11)"].ToString();
                        lblAttributeValue11.Text = dt.Rows[0]["(属性値11)"].ToString();
                        lblAttributeName12.Text = dt.Rows[0]["(属性名12)"].ToString();
                        lblAttributeValue12.Text = dt.Rows[0]["(属性値12)"].ToString();
                        lblAttributeName13.Text = dt.Rows[0]["(属性名13)"].ToString();
                        lblAttributeValue13.Text = dt.Rows[0]["(属性値13)"].ToString();
                        lblAttributeName14.Text = dt.Rows[0]["(属性名14)"].ToString();
                        lblAttributeValue14.Text = dt.Rows[0]["(属性値14)"].ToString();
                        lblAttributeName15.Text = dt.Rows[0]["(属性名15)"].ToString();
                        lblAttributeValue15.Text = dt.Rows[0]["(属性値15)"].ToString();
                        lblAttributeName16.Text = dt.Rows[0]["(属性名16)"].ToString();
                        lblAttributeValue16.Text = dt.Rows[0]["(属性値16)"].ToString();
                        lblAttributeName17.Text = dt.Rows[0]["(属性名17)"].ToString();
                        lblAttributeValue17.Text = dt.Rows[0]["(属性値17)"].ToString();
                        lblAttributeName18.Text = dt.Rows[0]["(属性名18)"].ToString();
                        lblAttributeValue18.Text = dt.Rows[0]["(属性値18)"].ToString();
                        lblAttributeName19.Text = dt.Rows[0]["(属性名19)"].ToString();
                        lblAttributeValue19.Text = dt.Rows[0]["(属性値19)"].ToString();
                        lblAttributeName20.Text = dt.Rows[0]["(属性名20)"].ToString();
                        lblAttributeValue20.Text = dt.Rows[0]["(属性値20)"].ToString();
                        lblClassification.Text = dt.Rows[0]["危険物の種別"].ToString();
                        lblProductName.Text = dt.Rows[0]["危険物の品名"].ToString();
                        lblRiskClass.Text = dt.Rows[0]["危険等級"].ToString();
                        lblContent.Text = dt.Rows[0]["危険物の含有量"].ToString();
                        lblNature.Text = dt.Rows[0]["危険物の性質"].ToString();
                        lblExpirationDate.Text = dt.Rows[0]["(使用期限)"].ToString();
                    //}
                    //else
                   // {
                    //    Ctrl_ID = dt.Rows[0]["コントロールカラム"].ToString();
                    //}
                }
                //return Ctrl_ID;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
               // return String.Empty;
            }
        }
    }
}