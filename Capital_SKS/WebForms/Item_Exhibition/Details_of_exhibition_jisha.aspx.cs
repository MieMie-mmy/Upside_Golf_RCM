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
using ORS_RCM.WebForms.Item;

namespace ORS_RCM
{
    public partial class Details_of_exhibition_jisha : System.Web.UI.Page
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
        Item_Master_BL imbl; string ItemList;
        Exhibition_List_BL ehb;
        protected void Page_Load(object sender, EventArgs e)
        {
          try
          {
              if (!IsPostBack)
              {
                  gvBind();
                  string Ctrl_ID = GetData();
                  if (Ctrl_ID != "d")
                  {



                      DataTable dterror = new DataTable();
                      string itemcode = lblItemCode.Text;
                      dterror = ehb.Selectexerror(itemcode, Shop_ID, 2);
                      if (dterror != null && dterror.Rows.Count > 0)
                      {

                          lbljitemerror.Text = dterror.Rows[0]["Error_Description"].ToString();
                      }
                      dterror = null;
                      dterror = ehb.Selectexerror(itemcode, Shop_ID, 1);
                      if (dterror != null && dterror.Rows.Count > 0)
                      {

                          lbljselecterror.Text = dterror.Rows[0]["Error_Description"].ToString();
                      }
                      dterror = null;
                      dterror = ehb.Selectexerror(itemcode, Shop_ID, 0);
                      if (dterror != null && dterror.Rows.Count > 0)
                      {

                          lbljcaterror.Text = dterror.Rows[0]["Error_Description"].ToString();
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

        protected void gvBind() 
        {
           try
          {
            ehb = new Exhibition_List_BL();
            int shop_id = Shop_ID;
            string itemid = Convert.ToString(Item_ID);
           
            DataTable dt = ehb.Selectjishadata(shop_id, itemid, "itemcat");
             

            gvcat.DataSource = dt;
            gvcat.DataBind();


            DataTable dtItemSelect = ehb.Selectjishadata(shop_id, itemid,"itemselect");
                
           
            gvoption.DataSource = dtItemSelect;
            gvoption.DataBind();
          }
           catch (Exception ex)
           {
               Session["Exception"] = ex.ToString();
               Response.Redirect("~/CustomErrorPage.aspx?");
           }
        }
        protected string GetData() 
        {
          try
          {
              string Ctrl_ID = string.Empty;
            ehb = new Exhibition_List_BL();
            int shop_id = Shop_ID;

            ItemList =Convert.ToString( Item_ID);


            DataTable dtitem = ehb.SelectByExhibitionData(shop_id, ItemList, "item");
            //Export_CSV3 exportCSV3 = new Export_CSV3();
            //DataTable dtModified = exportCSV3.ModifyTable(dtitem, shop_id);
            //if (dtModified != null)
            //{
            //    dtitem = dtModified;
            //}
            if (dtitem != null && dtitem.Rows.Count > 0)
            {
                if (dtitem.Rows[0]["コントロールカラム"].ToString() != "d")
                {
                    Ctrl_ID = dtitem.Rows[0]["コントロールカラム"].ToString();
                    lblControlColumn.Text = dtitem.Rows[0]["コントロールカラム"].ToString();
                    lblProductURL.Text = dtitem.Rows[0]["商品管理番号（商品URL）"].ToString();
                    lblItemCode.Text = dtitem.Rows[0]["商品番号"].ToString();
                    lblAll_Products_directory_ID.Text = dtitem.Rows[0]["全商品ディレクトリID"].ToString();
                    lblTagID.Text = dtitem.Rows[0]["タグID"].ToString();
                    lblpccatchcopy.Text = dtitem.Rows[0]["PC用キャッチコピー"].ToString();
                    lblMobileCatchCopy.Text = dtitem.Rows[0]["モバイル用キャッチコピー"].ToString();
                    lblItemName.Text = dtitem.Rows[0]["商品名"].ToString();
                    lblSelling_price.Text = dtitem.Rows[0]["販売価格"].ToString();
                    lblIndicated_price.Text = dtitem.Rows[0]["表示価格"].ToString();
                    lblConsumptiontax.Text = dtitem.Rows[0]["消費税"].ToString();
                    lblPostage.Text = dtitem.Rows[0]["送料"].ToString();
                    lblshippingCost.Text = dtitem.Rows[0]["個別送料"].ToString();
                    lblShipping_Category_1.Text = dtitem.Rows[0]["送料区分1"].ToString();
                    lblShipping_Category_2.Text = dtitem.Rows[0]["送料区分2"].ToString();
                    lbl代引料.Text = dtitem.Rows[0]["代引料"].ToString();
                    lblwarehouse_specified.Text = dtitem.Rows[0]["倉庫指定"].ToString();
                    lblproductInformationLayout.Text = dtitem.Rows[0]["商品情報レイアウト"].ToString();
                    lblOrderbutton.Text = dtitem.Rows[0]["注文ボタン"].ToString();
                    lblRequest_button.Text = dtitem.Rows[0]["資料請求ボタン"].ToString();
                    lblProduct_Inquire_Button.Text = dtitem.Rows[0]["商品問い合わせボタン"].ToString();
                    lblComingSoon_button.Text = dtitem.Rows[0]["再入荷お知らせボタン"].ToString();
                    lblmobileDisplay.Text = dtitem.Rows[0]["モバイル表示"].ToString();
                    lblworks_Corresponding.Text = dtitem.Rows[0]["のし対応"].ToString();
                    lblPcForItem_Description.Text = dtitem.Rows[0]["PC用商品説明文"].ToString();
                    lblMobileItemDescription.Text = dtitem.Rows[0]["モバイル用商品説明文"].ToString();
                    lblSmartPhoneFor_ItemDescription.Text = dtitem.Rows[0]["スマートフォン用商品説明文"].ToString();
                    lblPCforSale_Description.Text = dtitem.Rows[0]["PC用販売説明文"].ToString();
                    lblProdcutImage_URL.Text = dtitem.Rows[0]["商品画像URL"].ToString();
                    lblProductImage_Name.Text = dtitem.Rows[0]["商品画像名（ALT)"].ToString();
                    lblAnimation.Text = dtitem.Rows[0]["動画"].ToString();
                    lblSalePeriod_Specified.Text = dtitem.Rows[0]["販売期間指定"].ToString();
                    lblOrder_number_of_acceptances.Text = dtitem.Rows[0]["注文受付数"].ToString();
                    lblStock_Type.Text = dtitem.Rows[0]["在庫タイプ"].ToString();
                    lblStock_Quantity.Text = dtitem.Rows[0]["在庫数"].ToString();
                    lblStockno_Display.Text = dtitem.Rows[0]["在庫数表示"].ToString();
                    lblhorizontal_axis_itemname.Text = dtitem.Rows[0]["項目選択肢別在庫用横軸項目名"].ToString();
                    lblvertical_axis_itemname.Text = dtitem.Rows[0]["項目選択肢別在庫用縦軸項目名"].ToString();
                    lbl_remaining_stock_fordisplaythreshold.Text = dtitem.Rows[0]["項目選択肢別在庫用残り表示閾値"].ToString();
                    lblRacNo.Text = dtitem.Rows[0]["RAC番号"].ToString();
                    lblSearchHide.Text = dtitem.Rows[0]["サーチ非表示"].ToString();
                    lblBlackMarket_Password.Text = dtitem.Rows[0]["闇市パスワード"].ToString();
                    lblCatalogID.Text = dtitem.Rows[0]["カタログID"].ToString();
                    lblFlag_Back_Stock.Text = dtitem.Rows[0]["在庫戻しフラグ"].ToString();
                    lblOrder_accepted_outofstockatthetime.Text = dtitem.Rows[0]["在庫切れ時の注文受付"].ToString();
                    lblDeliverycontrolnumber_whenInStock.Text = dtitem.Rows[0]["在庫あり時納期管理番号"].ToString();
                    lblDelivery_date_management_number_outofstockatthetime.Text = dtitem.Rows[0]["在庫切れ時納期管理番号"].ToString();
                    lblReservation_ItemReleaseDate.Text = dtitem.Rows[0]["予約商品発売日"].ToString();
                    lblPointMagnification.Text = dtitem.Rows[0]["ポイント変倍率"].ToString();
                    lblPointmagnification_applicationperiod.Text = dtitem.Rows[0]["ポイント変倍率適用期間"].ToString();
                    lblHeader_Footer.Text = dtitem.Rows[0]["ヘッダー・フッター・レフトナビ"].ToString();
                    lblOrderoftheDisplayItem.Text = dtitem.Rows[0]["表示項目の並び順"].ToString();
                    lblCommonDescription_Small.Text = dtitem.Rows[0]["共通説明文（小)"].ToString();
                    lblFeature_Product.Text = dtitem.Rows[0]["目玉商品"].ToString();
                    lblCommonDescriptionLarge.Text = dtitem.Rows[0]["共通説明文（大)"].ToString();
                    lblReviewText.Text = dtitem.Rows[0]["レビュー本文表示"].ToString();
                    lblEasier_delivery_management_number_tomorrow.Text = dtitem.Rows[0]["あす楽配送管理番号"].ToString();
                    //lblOverseas_deliverycontrolnumber.Text = dtitem.Rows[0]["海外配送管理番号"].ToString();
                    //lblSizeChartLink.Text = dtitem.Rows[0]["サイズ表リンク"].ToString();
                    //lblDrugDescription.Text = dtitem.Rows[0]["医薬品説明文"].ToString();
                    //lblDrugNote.Text = dtitem.Rows[0]["医薬品注意事項"].ToString();
                }
                else
                {
                    Ctrl_ID = dtitem.Rows[0]["コントロールカラム"].ToString();
                    lblControlColumn.Text = dtitem.Rows[0]["コントロールカラム"].ToString();
                    lblProductURL.Text = dtitem.Rows[0]["商品管理番号（商品URL）"].ToString();
                }
            }
            return Ctrl_ID;
          }
          catch (Exception ex)
          {
              Session["Exception"] = ex.ToString();
              Response.Redirect("~/CustomErrorPage.aspx?");
              return string.Empty;
          }
        }
    }
}