/* 
Created By              : Eephyo
Created Date          : 01/07/2014
Updated By             :
Updated Date         :

 Tables using: Mall_Setting_Ponpare_Default,Mall_Setting_Ponpare_Fixed;
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
    public partial class Details_of_exhibition__Ponpare_ : System.Web.UI.Page
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
       


        Details_Ponpare_Exhibition_BL detailPon_bl;
        Details_Ponpare_Exhibation_Entity detailPon_entity;
        Exhibition_List_BL ehb;
        string str;
        string list;
        protected void Page_Load(object sender, EventArgs e)
        {
          try
          {
            if (!IsPostBack)
            {
                   detailPon_bl = new   Details_Ponpare_Exhibition_BL();
                   ehb = new Exhibition_List_BL();
                   DataTable dterror = new DataTable();
                   int id = Shop_ID;
                   int itemID = Item_ID;

                   //detailPon_entity = detailPon_bl.SelectByID(id);
                    string shop = Convert.ToString(id);
                    int sid = Int32.Parse(shop);
                    string item = Convert.ToString(itemID);
                  // }
            
                   string Ctrl_ID = Getdata(id,Convert.ToString(Item_ID));
                   if (Ctrl_ID != "d")
                   {
                       DataTable dts = detailPon_bl.SelectByExhibitionData(sid, item, "option");
                       //ehb.SelectAll(item, null, null, null, 4, shop, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null); 

                       gvoption.DataSource = dts;
                       gvoption.DataBind();


                       DataTable dtc = detailPon_bl.SelectByExhibitionData(sid, item, "category");
                       //ehb.SelectAll(item, null, null, null, 7, shop, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                       gvcat.DataSource = dtc;
                       gvcat.DataBind();
                   }
                   string itemcode = lblProductURL.Text;

                       dterror = ehb.Selectexerror(itemcode, id, 2, itemID);
                       if (dterror != null && dterror.Rows.Count > 0)
                       {
                           for (int i = 0; i < dterror.Rows.Count; i++ )
                               lblpitemerror.Text += dterror.Rows[i]["Error_Description"].ToString()+"\n\n";
                       }
                       dterror = null;
                       dterror = ehb.Selectexerror(itemcode, id, 1, itemID);
                       if (dterror != null && dterror.Rows.Count > 0)
                       {

                           lblpoptionerror.Text = dterror.Rows[0]["Error_Description"].ToString();
                       }
                       dterror = null;
                       dterror = ehb.Selectexerror(itemcode, id, 0, itemID);
                       if (dterror != null && dterror.Rows.Count > 0)
                       {

                           lblpcaterror.Text = dterror.Rows[0]["Error_Description"].ToString();
                       }
              }
          }
          catch (Exception ex)
          {
              Session["Exception"] = ex.ToString();
              Response.Redirect("~/CustomErrorPage.aspx?");
          }
        }

        protected string Getdata(int shopid, string itemid) 
        {
          try
          {
                string Ctrl_ID = string.Empty;
                detailPon_bl = new Details_Ponpare_Exhibition_BL();
                DataTable dt = detailPon_bl.SelectByExhibitionData(shopid, itemid, "item");
                //Export_CSV3 exportCSV3 = new Export_CSV3();
                //DataTable dtItemMaster = exportCSV3.ModifyTable(dt, shopid);
                //if (dtItemMaster != null)
                //{
                //    dt = dtItemMaster;
                //}
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["コントロールカラム"].ToString() != "d")
                    {
                    Ctrl_ID = dt.Rows[0]["コントロールカラム"].ToString();
                    lblProductURL.Text = dt.Rows[0]["商品管理ID（商品URL）"].ToString();
                    lblControlColumn.Text = dt.Rows[0]["コントロールカラム"].ToString();
                    lblproductID.Text = dt.Rows[0]["商品ID"].ToString();
                    lblproductNo.Text = dt.Rows[0]["商品名"].ToString();
                    lblcatchcopy.Text = dt.Rows[0]["キャッチコピー"].ToString();
                    lblsellingprice.Text = dt.Rows[0]["販売価格"].ToString();
                    lblindicateprice.Text = dt.Rows[0]["表示価格"].ToString();
                    lblprodesc1.Text = dt.Rows[0]["商品説明(1)"].ToString();
                    lblprodesc2.Text = dt.Rows[0]["商品説明(2)"].ToString();
                    lblprodesctax.Text = dt.Rows[0]["商品説明(テキストのみ)"].ToString();
                    lblproforphone.Text = dt.Rows[0]["商品説明(スマートフォン用)"].ToString();
                    lblSalestatus.Text = dt.Rows[0]["販売ステータス"].ToString();
                    lblmallgID.Text = dt.Rows[0]["モールジャンルID"].ToString();
                    lblcontax.Text = dt.Rows[0]["消費税"].ToString();
                    lblpostage.Text = dt.Rows[0]["送料"].ToString();
                    lblShippinggroup1.Text = dt.Rows[0]["独自送料グループ(1)"].ToString();
                    lblshippinggroup2.Text = dt.Rows[0]["独自送料グループ(2)"].ToString();
                    lblextrashipping.Text = dt.Rows[0]["個別送料"].ToString();
                    lblfee.Text = dt.Rows[0]["代引料"].ToString();
                    lblexpanCode.Text = dt.Rows[0]["のし対応"].ToString();
                    lblOrderButton.Text = dt.Rows[0]["注文ボタン"].ToString();
                    lblInquirebutton.Text = dt.Rows[0]["商品問い合わせボタン"].ToString();
                    lblsaleperiod.Text = dt.Rows[0]["販売期間指定"].ToString();
                    lblacceptnumberOforder.Text = dt.Rows[0]["注文受付数"].ToString();
                    lblStype.Text = dt.Rows[0]["在庫タイプ"].ToString();
                    lblStocknumber.Text = dt.Rows[0]["在庫数"].ToString();
                    lblstockDisplay.Text = dt.Rows[0]["在庫表示"].ToString();
                    lblpointrate.Text = dt.Rows[0]["ポイント率"].ToString();
                    lblSecretPassoword.Text = dt.Rows[0]["シークレットセールパスワード"].ToString();
                    lblpointperiod.Text = dt.Rows[0]["ポイント率適用期間"].ToString();
                    lblHorizontalItemName.Text = dt.Rows[0]["SKU横軸項目名"].ToString();
                    lblVertical.Text = dt.Rows[0]["SKU縦軸項目名"].ToString();
                    lblRemainingStock.Text = dt.Rows[0]["SKU在庫用残り表示閾値"].ToString();

                    lblJunCode.Text = dt.Rows[0]["JANコード"].ToString();
                    lblimgurl.Text = dt.Rows[0]["商品画像URL"].ToString();
                }
                else
                {
                    Ctrl_ID = dt.Rows[0]["コントロールカラム"].ToString();
                    lblProductURL.Text = dt.Rows[0]["商品管理ID（商品URL）"].ToString();
                    lblControlColumn.Text = dt.Rows[0]["コントロールカラム"].ToString();
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
        public void GetData(int ItemID,int shopid) 
        {
          try
          {
            detailPon_bl = new Details_Ponpare_Exhibition_BL();
            DataTable dt = detailPon_bl.SelectbyItemID(ItemID);
            if (dt != null && dt.Rows.Count > 0) 
            {
                lblProductURL.Text = dt.Rows[0]["Item_Code"].ToString();
                lblControlColumn.Text = dt.Rows[0]["Ctrl_ID"].ToString();
                lblproductID.Text = dt.Rows[0]["Ponpare_CategoryID"].ToString();
                lblproductNo.Text = dt.Rows[0]["Item_Code"].ToString();
                lblsellingprice.Text = dt.Rows[0]["Sale_Price"].ToString();
                lblindicateprice.Text = dt.Rows[0]["List_Price"].ToString();
                lblprodesc1.Text = dt.Rows[0]["Additional_1"].ToString();
                lblprodesc2.Text = dt.Rows[0]["Additional_2"].ToString();
                lblprodesctax.Text = dt.Rows[0]["Additional_3"].ToString();
                lblproforphone.Text = dt.Rows[0]["Item_Description_Phone"].ToString();


                int id =(int)dt.Rows[0]["ID"];
                DataTable dtimg = detailPon_bl.SelectbyImage(id,shopid);
                if (dt != null && dt.Rows.Count > 0) 
                {
                    for (int i = 0; i < dtimg.Rows.Count; i++)
                    {
                        str += dtimg.Rows[i]["Image_URL"].ToString() + "/" + dtimg.Rows[i]["Image_Name"].ToString() + ' ';
                          lblimgurl.Text= str;
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

       
           public void ShowData(int shopid)
        {
           try
          {
             detailPon_bl = new   Details_Ponpare_Exhibition_BL();
            DataTable dt = detailPon_bl.SelectByID(shopid);
            if (dt != null && dt.Rows.Count > 0) 
            {
              
          
            lblSalestatus.Text = dt.Rows[0]["Sale_Status"].ToString();
            
            lblcontax.Text = dt.Rows[0]["Consumption_Tax"].ToString();
            lblpostage.Text = dt.Rows[0]["Postage"].ToString();
            lblShippinggroup1.Text = dt.Rows[0]["Shipping_Group1"].ToString();
            lblshippinggroup2.Text = dt.Rows[0]["Shipping_Group2"].ToString();
            lblextrashipping.Text = dt.Rows[0]["Extra_Shipping"].ToString();
            lblfee.Text = dt.Rows[0]["Delivery_Charges"].ToString();
            lblexpanCode.Text = dt.Rows[0]["Expand_Cope"].ToString();
            lblOrderButton.Text = dt.Rows[0]["Order_Button"].ToString();
            lblInquirebutton.Text = dt.Rows[0]["Inquiry_Button"].ToString();
            
            lblacceptnumberOforder.Text = dt.Rows[0]["NoofAcceptances"].ToString();
            lblStype.Text = dt.Rows[0]["Stock_Type"].ToString();
            lblStocknumber.Text = dt.Rows[0]["Stock_Quantity"].ToString();
            lblstockDisplay.Text = dt.Rows[0]["Stock_Display"].ToString();
          
            lblSecretPassoword.Text = dt.Rows[0]["SaleSecret_Password"].ToString();
            
            lblHorizontalItemName.Text = dt.Rows[0]["Horizontal_ItemName"].ToString();
            lblVertical.Text = dt.Rows[0]["Vertical_ItemName"].ToString();
            lblRemainingStock.Text = dt.Rows[0]["Remaining_Stock"].ToString();
            
            lblJunCode.Text =dt.Rows[0]["JAN_Code"].ToString();
            }
          }
           catch (Exception ex)
           {
               Session["Exception"] = ex.ToString();
               Response.Redirect("~/CustomErrorPage.aspx?");
           }
            # region
          
            #endregion
        }


    }
}