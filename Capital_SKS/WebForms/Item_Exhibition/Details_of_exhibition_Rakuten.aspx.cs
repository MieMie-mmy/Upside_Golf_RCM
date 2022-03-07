/* 
Created By              : Eephyo
Created Date          : 30/06/2014
Updated By             :
Updated Date         :

 Tables using: Mall_Setting_Rakhutan_Default,Mall_Setting_Rakhutan_Fixed;
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
using ORS_RCM.WebForms.Item;

namespace ORS_RCM
{
    public partial class Details_of_exhibition__Rakuten_ : System.Web.UI.Page
    {
         public int  Shop_ID
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

        Details_Rakuten_Exhibation_BL   detailR_bl;
        Details_Rakuten_Exhibation_Entity  detailR_entity;
        Exhibition_List_BL ehb; string str;
        public string list; string itemcode;

        protected void Page_Load(object sender, EventArgs e)
        {
          try
          {
              if (!IsPostBack)
              {

                  detailR_bl = new Details_Rakuten_Exhibation_BL();
                  DataTable dterror = new DataTable();
                  int id = Shop_ID;
                  hfshopid.Value = Convert.ToString(id);
                  int itemid = Item_ID;
                  string itid = Convert.ToString(itemid);
                  //detailR_entity = detailR_bl.SelectByID(id);

                  //ShowData(detailR_entity);
                  string Ctrl_ID = GetData1(itemid, id);
                  //if (Session["list"].ToString() != null)
                  //{
                  if (Ctrl_ID != "d")
                  {
                     
                      // list = Session["list"].ToString();
                      //DataTable dts = ehb.SelectAll(list, null, null, null, 2);
                      DataTable dts = detailR_bl.SelectByExhibitionData(id, itid, "itemselect");
                      gvselect.DataSource = dts;
                      gvselect.DataBind();

                      string ids = Convert.ToString(id);
                      //DataTable dtc = ehb.SelectAll(itid, null, null, null, 2, ids, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                      DataTable dtc = detailR_bl.SelectByExhibitionData(id, itid, "itemcat");
                      gvcat.DataSource = dtc;
                      gvcat.DataBind();

                      //if (dts != null && dts.Rows.Count > 0)
                      //{
                      //    if (!String.IsNullOrWhiteSpace(dts.Rows[0]["商品管理番号（商品URL）"].ToString()))
                      //        itemcode = dts.Rows[0]["商品管理番号（商品URL）"].ToString();
                      //}
                  }
                      ehb = new Exhibition_List_BL();
                      itemcode = lblProductURL.Text; //added by hlz.

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
              //}
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
            //if (Session["list"].ToString() != null)
            //{

            //    list = Session["list"].ToString();
                string itid = Convert.ToString(itemid);
                DataTable dt = detailR_bl.SelectByExhibitionData(ids, itid, "item");
                //Export_CSV3 exportCSV3 = new Export_CSV3();
                //DataTable dtItemMaster = exportCSV3.ModifyTable(dt, shopid);
                //if (dtItemMaster != null)
                //{
                //    dt= dtItemMaster;
                //}
                if (dt != null && dt.Rows.Count > 0)
                {
                    //lblPCforsaledesc.Text=;
                    // lblMobileDisplay.TabIndex=;
                    if (dt.Rows[0]["コントロールカラム"].ToString() != "d")
                    {
                        Ctrl_ID = dt.Rows[0]["コントロールカラム"].ToString(); // to return value;
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
                        //int id = (int)dt.Rows[0]["ID"];
                        //DataTable dtimg = detailR_bl.SelectImage(id, shopid);
                        //if (dtimg != null && dtimg.Rows.Count > 0)
                        //{
                        //    for (int i = 0; i < dtimg.Rows.Count; i++)
                        //    {
                        //        str += dtimg.Rows[i]["Image_URL"].ToString() + "/" + dtimg.Rows[i]["Image_Name"].ToString() + ' ';
                        //        lblproimgurl.Text = str;
                        //    }
                        //}
                        lblfeatureperiod.Text = dt.Rows[0]["目玉商品"].ToString();
                        lblpointmagperiod.Text = dt.Rows[0]["ポイント変倍率適用期間"].ToString();
                        lblpointmagnification.Text = dt.Rows[0]["ポイント変倍率"].ToString();
                        lblproimgurl.Text = dt.Rows[0]["商品画像URL"].ToString();
                        lblOrderNoAcceptence.Text = dt.Rows[0]["注文受付数"].ToString();
                        lblSpecified_saleperiod.Text = dt.Rows[0]["販売期間指定"].ToString();
                        lblPCCopy.Text = dt.Rows[0]["PC用キャッチコピー"].ToString();
                        lblmobilecopy.Text = dt.Rows[0]["モバイル用キャッチコピー"].ToString();
                        lblconsumptionTax.Text = dt.Rows[0]["消費税"].ToString();
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
                        //  lblOrder_Reception.Text = 
                        lbldateoutofstock.Text = dt.Rows[0]["在庫切れ時納期管理番号"].ToString();
                        lblControlNo.Text = dt.Rows[0]["在庫あり時納期管理番号"].ToString();

                        lblBookOrderDate.Text = dt.Rows[0]["予約商品発売日"].ToString();

                        lblHeaderFooter.Text = dt.Rows[0]["ヘッダー・フッター・レフトナビ"].ToString();

                        lblDisplayItem.Text = dt.Rows[0]["表示項目の並び順"].ToString();

                        lblCommonDescriptionsmall.Text = dt.Rows[0]["共通説明文(小)"].ToString();

                        lblCommonDescriptionlarge.Text = dt.Rows[0]["共通説明文（大）"].ToString();

                        lblReviewDisplay.Text = dt.Rows[0]["レビュー本文表示"].ToString();

                        //  lblOverseaDeliveryControlNo.Text = dt.Rows[0]["海外配送管理番号"].ToString();

                        lblSitechartlink.Text = dt.Rows[0]["サイズ表リンク"].ToString();

                        //   lblDrugDescription.Text = dt.Rows[0]["Sale_Description_PC"].ToString();
                        //  lblDrugNote.Text = dt.Rows[0]["Sale_Description_PC"].ToString();
                        lbleasydeliverynumber.Text = dt.Rows[0]["あす楽配送管理番号"].ToString();
                    }
                    else
                    {
                        Ctrl_ID = dt.Rows[0]["コントロールカラム"].ToString(); // to return value;
                        lblcontrol.Text = dt.Rows[0]["コントロールカラム"].ToString();
                        lblProductURL.Text = dt.Rows[0]["商品管理番号（商品URL）"].ToString();
                    }
                }
            //}
                return Ctrl_ID;
          }
          catch (Exception ex)
          {
              Session["Exception"] = ex.ToString();
              Response.Redirect("~/CustomErrorPage.aspx?");
              return String.Empty;
          }
        }

        public void GetData(int itemid,int shopid) 
        {
            try
            {
            detailR_bl = new Details_Rakuten_Exhibation_BL();
            DataTable dt = detailR_bl.SelectbyItemID(itemid);
            if (dt != null && dt.Rows.Count > 0)
            {
               //lblPCforsaledesc.Text=;
               // lblMobileDisplay.TabIndex=;
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
               lblproductDirectoryID.Text = dt.Rows[0]["Rakuten_CategoryID"].ToString();
               lblPCforsaledesc.Text = dt.Rows[0]["Sale_Description_PC"].ToString();
               int id = (int)dt.Rows[0]["ID"];
               DataTable dtimg = detailR_bl.SelectImage(id,shopid);
               if (dtimg != null && dtimg.Rows.Count > 0)
               {
                   for (int i = 0; i < dtimg.Rows.Count; i++)
                   {
                       str += dtimg.Rows[i]["Image_URL"].ToString() + "/" + dtimg.Rows[i]["Image_Name"].ToString()+' ';
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

        public void ShowData(Details_Rakuten_Exhibation_Entity detailR_entity)
        {

             //lblShopName.Text = (detailR_entity.Shop_Name.ToString());
           try
            {
            if (!String.IsNullOrEmpty(lblconsumptionTax.Text))
            {
                lblconsumptionTax.Text = (detailR_entity.Consumption_Tax.ToString());

            }

               lblTagID.Text = detailR_entity.Tag_ID.ToString();

               if (detailR_entity.Searchhide!=null)
               {

                   lblSearchhide.Text =(detailR_entity.Searchhide.ToString());
            }

             

               lblExpandCode.Text = detailR_entity.Expandcope.ToString();
               
                //lblDualControlNumber.Text=(detailR_entity.DualPrice.ToString());

                lblshippingcategory1.Text = (detailR_entity.ShipCat1);

                lblshippingcategory2.Text = (detailR_entity.ShipCat2);

                lblproductinformation.Text = (detailR_entity.Product_Information);

                

                lblorder_button.Text =(detailR_entity.Orderbuttton.ToString());

                lblRequestbutton.Text = (detailR_entity.Requestbutton.ToString());

                lblProduct_inquiry_button.Text = (detailR_entity.ProductInquerybutton.ToString());

                lblComingsoon_button.Text = (detailR_entity.Comingsoonbut.ToString());

                lblMobileDisplay.Text = (detailR_entity.Mobile.ToString());

                if (detailR_entity.Expandcope!=null)
                {

                     lblExpandCode.Text=((detailR_entity.Expandcope.ToString()));

                }

                lblAnimation.Text = ((detailR_entity.Animation.ToString()));

              //lblAcceptanceNo.Text=(detailR_entity.AcceptNo.ToString());

              lblStocktype.Text = (detailR_entity.Stocktype.ToString());

              lblStockNumber.Text = (detailR_entity.Stockquantity.ToString());

              lblStockNumberdisplay.Text = (detailR_entity.StocknoDisplay.ToString());

              lblhorizontal_axis_item_name.Text = (detailR_entity.Hozitemname.ToString());

              lblvertical_axis_item_name.Text = (detailR_entity.VarItemname.ToString());

              lblremaining_stock_for_display_threshold.Text = (detailR_entity.Remainstock.ToString());

              lblRacNumber.Text = (detailR_entity.RACNO.ToString());

              lblCategoryID.Text = (detailR_entity.CatID.ToString());

              lblFlag.Text = (detailR_entity.Flagback.ToString());
              lblOutofStock.Text = (detailR_entity.Orderrecpt.ToString());
            //  lblOrder_Reception.Text = 
              lbldateoutofstock.Text = detailR_entity.StockDate;
              lblControlNo.Text = (detailR_entity.DelctrNo.ToString());

              lblBookOrderDate.Text = (detailR_entity.Orderrelease.ToString());

              lblHeaderFooter.Text = (detailR_entity.Headfooter.ToString());

             lblDisplayItem.Text=detailR_entity.Displayorder.ToString();
              
            lblCommonDescriptionsmall.Text=detailR_entity.Commondesc1;

            lblCommonDescriptionlarge.Text = detailR_entity.Commondesc2;

            lblReviewDisplay.Text = detailR_entity.Reviewtax.ToString();

            lblOverseaDeliveryControlNo.Text = detailR_entity.Oversea.ToString();

            lblSitechartlink.Text = detailR_entity.Size_Chartlink.ToString();

            lblDrugDescription.Text = detailR_entity.Drug_Description.ToString();
            lblDrugNote.Text = detailR_entity.Drug_Note.ToString();

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
                detailR_bl = new Details_Rakuten_Exhibation_BL();
            ehb = new Exhibition_List_BL();
            int id = Int16.Parse(hfshopid.Value);
            //list = Session["list"].ToString();
            int itemid = Item_ID;
            string itid = Convert.ToString(itemid);
            gvselect.PageIndex = e.NewPageIndex;
            DataTable dts = detailR_bl.SelectByExhibitionData(id, itid, "itemselect");
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
              detailR_bl = new Details_Rakuten_Exhibation_BL();
            ehb = new Exhibition_List_BL();
            int id = Shop_ID;
            int itemid = Item_ID;
            string itid = Convert.ToString(itemid);
            string shopid = hfshopid.Value;
          //  list = Session["list"].ToString();
            gvcat.PageIndex = e.NewPageIndex;
            //DataTable dtc = ehb.SelectAll(list, null, null, null, 2, shopid, null, null, null, null, null, null, null, null, null, null, null, null, null,null,null);
            DataTable dtc = detailR_bl.SelectByExhibitionData(id, itid, "itemcat");
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
