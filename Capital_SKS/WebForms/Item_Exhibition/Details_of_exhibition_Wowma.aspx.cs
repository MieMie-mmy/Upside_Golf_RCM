using ORS_RCM_BL;
using ORS_RCM_Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Capital_SKS.WebForms.Item_Exhibition
{
    public partial class Detail_of_Exhibition_Wowma : System.Web.UI.Page
    {
        Details_Wowma_Exhibition_BL detailW_bl;
        Details_Wowma_Exhibation_Entity detailW_entity;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    detailW_bl = new Details_Wowma_Exhibition_BL();
                    DataTable dterror = new DataTable();
                    int id = Shop_ID;
                    hfshopid.Value = Convert.ToString(id);
                    int itemid = Item_ID;
                    string itid = Convert.ToString(itemid);
                    string Ctrl_ID = GetData1(itemid, id);

                    if (Ctrl_ID != "d")
                    {
                        DataTable dts = detailW_bl.SelectByExhibitionData(id, itid, "itemselect");
                        gvselect.DataSource = dts;
                        gvselect.DataBind();
                        string ids = Convert.ToString(id);
                        DataTable dtc = detailW_bl.SelectByExhibitionData(id, itid, "itemcat");
                        gvcat.DataSource = dtc;
                        gvcat.DataBind();
                    }

                    ehb = new Exhibition_List_BL();
                    itemcode = lblProductURL.Text; 
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
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }
        public string GetData1(int itemid, int shopid)
        {
            try
            {
                string Ctrl_ID = String.Empty;
                int ids = Shop_ID;
                string itid = Convert.ToString(itemid);
                DataTable dt = detailW_bl.SelectByExhibitionData(ids, itid, "item");

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["コントロールカラム"].ToString() != "d")
                    {
                        Ctrl_ID = dt.Rows[0]["コントロールカラム"].ToString();
                        lblcontrol.Text = dt.Rows[0]["コントロールカラム"].ToString();
                        lblProductURL.Text = dt.Rows[0]["商品管理番号（商品URL）"].ToString();
                        lblproductNo.Text = dt.Rows[0]["商品番号"].ToString();
                        lblSellingPrice.Text = dt.Rows[0]["販売価格"].ToString();
                        lblindprice.Text = dt.Rows[0]["表示価格"].ToString();
                        txtpcforitem.Text = dt.Rows[0]["PC用商品説明文"].ToString();
                        txtmobiledesc.Text = dt.Rows[0]["モバイル用商品説明文"].ToString();
                        txtsmartphoneforitemdesc.Text = dt.Rows[0]["スマートフォン用商品説明文"].ToString();
                        lblproALT.Text = dt.Rows[0]["商品画像名（ALT）"].ToString();
                        lblproductname.Text = dt.Rows[0]["商品名"].ToString();
                        lblBlackmarketpassword.Text = dt.Rows[0]["闇市パスワード"].ToString();
                        lbldeliverycharges.Text = dt.Rows[0]["代引料"].ToString();
                        lblWarehousespecified.Text = dt.Rows[0]["倉庫指定"].ToString();
                        lblPostage.Text = dt.Rows[0]["送料"].ToString();
                        lblExtrashipping.Text = dt.Rows[0]["個別送料"].ToString();
                        lblproductDirectoryID.Text = dt.Rows[0]["全商品ディレクトリID"].ToString();
                        lblPCforsaledesc.Text = dt.Rows[0]["PC用販売説明文"].ToString();
                        lblfeatureperiod.Text = dt.Rows[0]["目玉商品"].ToString();
                        lblpointmagperiod.Text = dt.Rows[0]["ポイント変倍率適用期間"].ToString();
                        lblpointmagnification.Text = dt.Rows[0]["ポイント変倍率"].ToString();
                        lblproimgurl.Text = dt.Rows[0]["商品画像URL"].ToString();
                        lblOrderNoAcceptence.Text = dt.Rows[0]["注文受付数"].ToString();
                        lblSpecified_saleperiod.Text = dt.Rows[0]["販売期間指定"].ToString();
                        lblPCCopy.Text = dt.Rows[0]["PC用キャッチコピー"].ToString();
                        lblmobilecopy.Text = dt.Rows[0]["モバイル用キャッチコピー"].ToString();
                        lblconsumptionTax.Text = dt.Rows[0]["消費税"].ToString();
                        lblreducetaxRate.Text = dt.Rows[0]["軽減税率設定"].ToString();
                        lblTagID.Text = dt.Rows[0]["タグID"].ToString();
                        lblSearchhide.Text = dt.Rows[0]["サーチ非表示"].ToString();
                        lblExpandCode.Text = dt.Rows[0]["のし対応"].ToString();
                        lblshippingcategory1.Text = dt.Rows[0]["送料区分1"].ToString();
                        lblshippingcategory2.Text = dt.Rows[0]["送料区分2"].ToString();
                        lblproductinformation.Text = dt.Rows[0]["商品情報レイアウト"].ToString();
                        lblorder_button.Text = dt.Rows[0]["注文ボタン"].ToString();
                        lblRequestbutton.Text = dt.Rows[0]["資料請求ボタン"].ToString();
                        lblProduct_inquiry_button.Text = dt.Rows[0]["商品問い合わせボタン"].ToString();
                        lblComingsoon_button.Text = dt.Rows[0]["再入荷お知らせボタン"].ToString();
                        lblMobileDisplay.Text = dt.Rows[0]["モバイル表示"].ToString();
                        lblExpandCode.Text = dt.Rows[0]["のし対応"].ToString();
                        lblAnimation.Text = dt.Rows[0]["動画"].ToString();
                        lblStocktype.Text = dt.Rows[0]["在庫タイプ"].ToString();
                        lblStockNumber.Text = dt.Rows[0]["在庫数"].ToString();
                        lblStockNumberdisplay.Text = dt.Rows[0]["在庫数表示"].ToString();
                        lblhorizontal_axis_item_name.Text = dt.Rows[0]["項目選択肢別在庫用横軸項目名"].ToString();
                        lblvertical_axis_item_name.Text = dt.Rows[0]["項目選択肢別在庫用縦軸項目名"].ToString();
                        lblremaining_stock_for_display_threshold.Text = dt.Rows[0]["項目選択肢別在庫用残り表示閾値"].ToString();
                        lblRacNumber.Text = dt.Rows[0]["RAC番号"].ToString();
                        lblCategoryID.Text = dt.Rows[0]["カタログID"].ToString();
                        lblFlag.Text = dt.Rows[0]["在庫戻しフラグ"].ToString();
                        lblOutofStock.Text = dt.Rows[0]["在庫切れ時の注文受付"].ToString();
                        lbldateoutofstock.Text = dt.Rows[0]["在庫切れ時納期管理番号"].ToString();
                        lblControlNo.Text = dt.Rows[0]["在庫あり時納期管理番号"].ToString();
                        lblBookOrderDate.Text = dt.Rows[0]["予約商品発売日"].ToString();
                        lblHeaderFooter.Text = dt.Rows[0]["ヘッダー・フッター・レフトナビ"].ToString();
                        lblDisplayItem.Text = dt.Rows[0]["表示項目の並び順"].ToString();
                        lblCommonDescriptionsmall.Text = dt.Rows[0]["共通説明文(小)"].ToString();
                        lblCommonDescriptionlarge.Text = dt.Rows[0]["共通説明文（大）"].ToString();
                        lblReviewDisplay.Text = dt.Rows[0]["レビュー本文表示"].ToString();
                        lblSitechartlink.Text = dt.Rows[0]["サイズ表リンク"].ToString();
                        lblcatalogid.Text = dt.Rows[0]["カタログIDなしの理由"].ToString();
                        lbleasydeliverynumber.Text = dt.Rows[0]["あす楽配送管理番号"].ToString();

                    }
                    else
                    {
                        Ctrl_ID = dt.Rows[0]["コントロールカラム"].ToString(); // to return value;
                        lblcontrol.Text = dt.Rows[0]["コントロールカラム"].ToString();
                        lblProductURL.Text = dt.Rows[0]["商品管理番号（商品URL）"].ToString();
                    }
                }
                
                return Ctrl_ID;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
        }

        public void GetData(int itemid, int shopid)
        {
            try
            {
                detailW_bl = new Details_Wowma_Exhibition_BL();
                DataTable dt = detailW_bl.SelectbyItemID(itemid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblProductURL.Text = dt.Rows[0]["Item_Code"].ToString();
                    lblproductNo.Text = dt.Rows[0]["Item_Code"].ToString();
                    lblSellingPrice.Text = dt.Rows[0]["Sale_Price"].ToString();
                    lblindprice.Text = dt.Rows[0]["List_Price"].ToString();
                    txtpcforitem.Text = dt.Rows[0]["Item_Description_PC"].ToString();
                    txtmobiledesc.Text = dt.Rows[0]["Item_Description_Mobile"].ToString();
                    txtsmartphoneforitemdesc.Text = dt.Rows[0]["Item_Description_Phone"].ToString();
                    lblproALT.Text = dt.Rows[0]["Item_Name"].ToString();
                    lblproductname.Text = dt.Rows[0]["Item_Name"].ToString();
                    lblBlackmarketpassword.Text = dt.Rows[0]["BlackMarket_Password"].ToString();
                    lbldeliverycharges.Text = dt.Rows[0]["Delivery_Charges"].ToString();
                    lblWarehousespecified.Text = dt.Rows[0]["Warehouse_Specified"].ToString();
                    lblPostage.Text = dt.Rows[0]["Postage"].ToString();
                    lblExtrashipping.Text = dt.Rows[0]["Extra_Shipping"].ToString();
                    lblproductDirectoryID.Text = dt.Rows[0]["Wowma_CategoryID"].ToString();
                    lblPCforsaledesc.Text = dt.Rows[0]["Sale_Description_PC"].ToString();
                    int id = (int)dt.Rows[0]["ID"];
                    DataTable dtimg = detailW_bl.SelectImage(id, shopid);
                    if (dtimg != null && dtimg.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtimg.Rows.Count; i++)
                        {
                            str += dtimg.Rows[i]["Image_URL"].ToString() + "/" + dtimg.Rows[i]["Image_Name"].ToString() + ' ';
                            lblproimgurl.Text = str;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void ShowData(Details_Wowma_Exhibation_Entity detailW_entity)
        {
            try
            {
                if (!String.IsNullOrEmpty(lblconsumptionTax.Text))
                {
                    lblconsumptionTax.Text = (detailW_entity.Consumption_Tax.ToString());

                }

                if (!String.IsNullOrEmpty(lblreducetaxRate.Text))
                {
                    lblreducetaxRate.Text = (detailW_entity.ReduceTax_Rate.ToString());
                }


                lblTagID.Text = detailW_entity.Tag_ID.ToString();

                if (detailW_entity.Searchhide != null)
                {

                    lblSearchhide.Text = (detailW_entity.Searchhide.ToString());
                }

                lblExpandCode.Text = detailW_entity.Expandcope.ToString();

                lblshippingcategory1.Text = (detailW_entity.ShipCat1);

                lblshippingcategory2.Text = (detailW_entity.ShipCat2);

                lblproductinformation.Text = (detailW_entity.Product_Information);

                lblorder_button.Text = (detailW_entity.Orderbuttton.ToString());

                lblRequestbutton.Text = (detailW_entity.Requestbutton.ToString());

                lblProduct_inquiry_button.Text = (detailW_entity.ProductInquerybutton.ToString());

                lblComingsoon_button.Text = (detailW_entity.Comingsoonbut.ToString());

                lblMobileDisplay.Text = (detailW_entity.Mobile.ToString());

                if (detailW_entity.Expandcope != null)
                {

                    lblExpandCode.Text = ((detailW_entity.Expandcope.ToString()));

                }

                lblAnimation.Text = ((detailW_entity.Animation.ToString()));

                lblStocktype.Text = (detailW_entity.Stocktype.ToString());

                lblStockNumber.Text = (detailW_entity.Stockquantity.ToString());

                lblStockNumberdisplay.Text = (detailW_entity.StocknoDisplay.ToString());

                lblhorizontal_axis_item_name.Text = (detailW_entity.Hozitemname.ToString());

                lblvertical_axis_item_name.Text = (detailW_entity.VarItemname.ToString());

                lblremaining_stock_for_display_threshold.Text = (detailW_entity.Remainstock.ToString());

                lblRacNumber.Text = (detailW_entity.RACNO.ToString());

                lblCategoryID.Text = (detailW_entity.CatID.ToString());

                lblFlag.Text = (detailW_entity.Flagback.ToString());

                lblOutofStock.Text = (detailW_entity.Orderrecpt.ToString());

                lbldateoutofstock.Text = detailW_entity.StockDate;

                lblControlNo.Text = (detailW_entity.DelctrNo.ToString());

                lblBookOrderDate.Text = (detailW_entity.Orderrelease.ToString());

                lblHeaderFooter.Text = (detailW_entity.Headfooter.ToString());

                lblDisplayItem.Text = detailW_entity.Displayorder.ToString();

                lblCommonDescriptionsmall.Text = detailW_entity.Commondesc1;

                lblCommonDescriptionlarge.Text = detailW_entity.Commondesc2;

                lblReviewDisplay.Text = detailW_entity.Reviewtax.ToString();

                lblOverseaDeliveryControlNo.Text = detailW_entity.Oversea.ToString();

                lblSitechartlink.Text = detailW_entity.Size_Chartlink.ToString();

                lblDrugDescription.Text = detailW_entity.Drug_Description.ToString();
                lblDrugNote.Text = detailW_entity.Drug_Note.ToString();

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvselect_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                detailW_bl = new Details_Wowma_Exhibition_BL();
                ehb = new Exhibition_List_BL();
                int id = Int16.Parse(hfshopid.Value);
                int itemid = Item_ID;
                string itid = Convert.ToString(itemid);
                gvselect.PageIndex = e.NewPageIndex;
                DataTable dts = detailW_bl.SelectByExhibitionData(id, itid, "itemselect");
                gvselect.DataSource = dts;
                gvselect.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvcat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                detailW_bl = new Details_Wowma_Exhibition_BL();
                ehb = new Exhibition_List_BL();
                int id = Shop_ID;
                int itemid = Item_ID;
                string itid = Convert.ToString(itemid);
                string shopid = hfshopid.Value;
                gvcat.PageIndex = e.NewPageIndex;
                DataTable dtc = detailW_bl.SelectByExhibitionData(id, itid, "itemcat");
                gvcat.DataSource = dtc;
                gvcat.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

    }
}