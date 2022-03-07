/* 
*
Created By              : KayThiAung
Created Date          :04/2015
Updated By             :
Updated Date         :

 Tables using           :  Promotion 
 *                                  - Promotion_Item
 *                                  -Exhibition_Promotion_Item_Master
 *                                  -Promotion_Item
 *                                  -Exhibition_Promotion_Item_Option
 * 
 * Storedprocedure using:  SP_PromotionDetailofRakuten
 *                                           - 
 *                                           - 
 *                                           -
                                     
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
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Promotion
{
	public partial class Details_of_Promotionexhibition_Rakuten : System.Web.UI.Page
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
            try
            {
                if (!IsPostBack)
                {
                    pbl = new Promotion_Detail_BL();
                    if (Item_ID != null)
                        itid = Convert.ToString(Item_ID);
                    if (Pro_Type == 1)//Campaign
                    {
                        if (Csv_Type == 0)
                        {
                            vpointmag.Style.Add("display", "none");
                            vpointperiod.Style.Add("display", "none");
                            vdelmagno.Style.Add("display", "none");
                            DataTable dt = pbl.CamapignRakuten(itid, Shop_ID, "item");
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                lblcontrol.Text = dt.Rows[0]["コントロールカラム"].ToString();
                                lblProductURL.Text = dt.Rows[0]["商品管理番号（商品URL）"].ToString();
                                lblproductNo.Text = dt.Rows[0]["商品番号"].ToString();
                                lblproductname.Text = dt.Rows[0]["商品名"].ToString();
                                lblPCcatch.Text = dt.Rows[0]["PC用キャッチコピー"].ToString();
                                lblmobilecatch.Text = dt.Rows[0]["スマートフォン用キャッチコピー"].ToString();
                                lblsmartdesc.Text = dt.Rows[0]["スマートフォン用商品説明文"].ToString();
                                lblpcitdescription.Text = dt.Rows[0]["PC用商品説明文"].ToString();
                                lblsaledescpc.Text = dt.Rows[0]["PC用販売説明文"].ToString();
                                lblblackpass.Text = dt.Rows[0]["闇市パスワード"].ToString();
                            }

                            DataTable dtselect = pbl.CamapignRakuten(itid, Shop_ID, "select");
                            gvselect.DataSource = dtselect;
                            gvselect.DataBind();
                        }//for add csv
                        else if (Csv_Type == 1)
                        {
                            vpointmag.Style.Add("display", "none");
                            vpointperiod.Style.Add("display", "none");
                            vdelmagno.Style.Add("display", "none");
                            DataTable dtc = pbl.CamapignRakuten(itid, Shop_ID, "item_R");
                            if (dtc != null && dtc.Rows.Count > 0)
                            {
                                lblcontrol.Text = dtc.Rows[0]["コントロールカラム"].ToString();
                                lblProductURL.Text = dtc.Rows[0]["商品管理番号（商品URL）"].ToString();
                                lblproductNo.Text = dtc.Rows[0]["商品番号"].ToString();
                                lblproductname.Text = dtc.Rows[0]["商品名"].ToString();
                                lblPCcatch.Text = dtc.Rows[0]["PC用キャッチコピー"].ToString();
                                //  lblmobilecatch.Text = dtc.Rows[0]["スマートフォン用キャッチコピー"].ToString();
                                //  lblsmartdesc.Text = dtc.Rows[0]["スマートフォン用商品説明文"].ToString();
                                lblpcitdescription.Text = dtc.Rows[0]["PC用商品説明文"].ToString();
                                lblsaledescpc.Text = dtc.Rows[0]["PC用販売説明文"].ToString();
                                lblblackpass.Text = dtc.Rows[0]["闇市パスワード"].ToString();
                            }
                            DataTable dtselectremove = pbl.CamapignRakuten(itid, Shop_ID, "select_R");
                            gvselect.DataSource = dtselectremove;
                            gvselect.DataBind();
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
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }   
		}

        public void getpointdata(int csvtype) 
        {
            try
            {
            pbl = new Promotion_Detail_BL();
            itid = Convert.ToString(Item_ID);
            
             if (csvtype == 0)
             {
                 name.Visible = false;
                 pccatch.Visible = false;
                 mobilecatch.Visible = false;
                 lblsmart.Visible = false;
                 lblpcdesc.Visible = false;
                 lblsaldesc.Visible = false;
                 lblpassword.Visible = false;
                 lblproductname.Visible = false;
                 lblPCcatch.Visible = false;
                 lblmobilecatch.Visible = false;
                 lblsmartdesc.Visible = false;
                 lblpcitdescription.Visible = false;
                 lblsaledescpc.Visible = false;
                 lblblackpass.Visible = false;
                 selectcsv.Visible = false;
                 campaign.Visible = false;
                 DataTable dtp = pbl.PointRakuten(itid, Shop_ID, 1);//1.1 item.csv
                 if (dtp != null && dtp.Rows.Count > 0)
                 {
                     lblpointperiod.Visible = true;
                     vname.Style.Add("display", "none");
                     vpccatch.Style.Add("display", "none");
                     vmobilecatch.Style.Add("display", "none");
                     vsmart.Style.Add("display", "none");
                     vpcdesc.Style.Add("display", "none");
                     vsaldesc.Style.Add("display", "none");
                     vpassword.Style.Add("display", "none");
                     pointmag.Visible = true;
                     lblpointmag.Visible = true;
                     pointperiod.Visible = true;
                     lblcontrol.Text = dtp.Rows[0]["コントロールカラム"].ToString();
                     lblProductURL.Text = dtp.Rows[0]["商品管理番号（商品URL）"].ToString();
                     lblproductNo.Text = dtp.Rows[0]["商品番号"].ToString();
                     lblpointmag.Text = dtp.Rows[0]["ポイント変倍率"].ToString();
                     lblpointperiod.Text = dtp.Rows[0]["ポイント変倍率適用期間"].ToString();
                 }//1.1 item.csv

                 DataTable dtpt = pbl.PointRakuten(itid, Shop_ID, 2);//1.1 item.csv
                 if (dtpt != null && dtpt.Rows.Count > 0)
                 {
                     item2.Visible = true;
                     pointcsv.Visible = true;
                     gvpointitem.DataSource = dtpt;
                     gvpointitem.DataBind();
                 }//1.1 item.csv
             }//addcsv
             else
             {

                 vsmart.Style.Add("display", "none");
                 vpcdesc.Style.Add("display", "none");
                 vsaldesc.Style.Add("display", "none");
                 vpassword.Style.Add("display", "none");
                 vpointmag.Style.Add("display", "none");
                 vpointperiod.Style.Add("display", "none");
                 vdelmagno.Style.Add("display", "none");
               
                 lblsmart.Visible = false;
                 lblpcdesc.Visible = false;
                 lblsaldesc.Visible = false;
                 lblpassword.Visible = false;
                 lblsmartdesc.Visible = false;
                 lblpcitdescription.Visible = false;
                 lblsaledescpc.Visible = false;
                 lblblackpass.Visible = false;
                 selectcsv.Visible = false;
                 campaign.Visible = false;
                 DataTable dtpremove = pbl.PointRakuten(itid, Shop_ID, 3);//1.1 item.csv
                 if (dtpremove != null && dtpremove.Rows.Count > 0)
                 {
                     lblpointmag.Visible = false;
                     lblpointperiod.Visible= false;
                     lblcontrol.Text = dtpremove.Rows[0]["コントロールカラム"].ToString();
                     lblProductURL.Text = dtpremove.Rows[0]["商品管理番号（商品URL）"].ToString();
                     lblproductNo.Text = dtpremove.Rows[0]["商品番号"].ToString();
                     lblPCcatch.Text = dtpremove.Rows[0]["PC用キャッチコピー"].ToString();
                     lblmobilecatch.Text = dtpremove.Rows[0]["スマートフォン用キャッチコピー"].ToString();
                     lblproductname.Text = dtpremove.Rows[0]["商品名"].ToString();
                 }
             }//removecsv
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }   
        }
        public void getDeliverydata(int csvtype) 
        {
          try
          {
            pbl = new Promotion_Detail_BL();
            itid = Convert.ToString(Item_ID);
            if (csvtype == 0)
            {
                name.Visible = false;
                pccatch.Visible = false;
                mobilecatch.Visible = false;
                lblsmart.Visible = false;
                lblpcdesc.Visible = false;
                lblsaldesc.Visible = false;
                lblpassword.Visible = false;
                lblproductname.Visible = false;
                lblPCcatch.Visible = false;
                lblmobilecatch.Visible = false;
                lblsmartdesc.Visible = false;
                lblpcitdescription.Visible = false;
                lblsaledescpc.Visible = false;
                lblblackpass.Visible = false;
                selectcsv.Visible = false;
                campaign.Visible = false;
                 lblPCcatch.Visible=false;
                 lblmobilecatch.Visible=false;
                 lblproductname.Visible = false;

                 vname.Style.Add("display", "none");
                 vpccatch.Style.Add("display", "none");
                 vmobilecatch.Style.Add("display", "none");
                 vsmart.Style.Add("display", "none");
                 vpcdesc.Style.Add("display", "none");
                 vsaldesc.Style.Add("display", "none");
                 vpassword.Style.Add("display", "none");
                 vpointmag.Style.Add("display", "none");
                 vpointperiod.Style.Add("display", "none");
                DataTable dtdelivery = pbl.DeliveryRakuten(itid,Shop_ID,1);
                if (dtdelivery != null && dtdelivery.Rows.Count > 0)
                {
                    delmagno.Visible = true;
                    lbldelivemngno.Visible = true;
                    lblcontrol.Text = dtdelivery.Rows[0]["コントロールカラム"].ToString();
                    lblProductURL.Text = dtdelivery.Rows[0]["商品管理番号（商品URL）"].ToString();
                    lblproductNo.Text = dtdelivery.Rows[0]["商品番号"].ToString();
                    lbldelivemngno.Text = dtdelivery.Rows[0]["あす楽配送管理番号"].ToString();
                }//item.csv
                DataTable dtdelselect = pbl.DeliveryRakuten(itid, Shop_ID, 2);
                if (dtdelselect != null && dtdelselect.Rows.Count > 0)
                {
                    selectcsv.Visible = true;
                    campaign.Visible = true;
                    gvselect.DataSource = dtdelselect;
                    gvselect.DataBind();
                }//select.csv
                DataTable dtcat = pbl.DeliveryRakuten(itid, Shop_ID, 3);
                if (dtcat != null && dtcat.Rows.Count > 0)
                {
                    itemcat.Visible = true;
                    itcat.Visible = true;
                    gvitemcat.DataSource = dtcat;
                    gvitemcat.DataBind();
                }//item-cat.csv
            }
            else//for Removecsv
            {
                name.Visible = false;
                pccatch.Visible = false;
                mobilecatch.Visible = false;
                lblsmart.Visible = false;
                lblpcdesc.Visible = false;
                lblsaldesc.Visible = false;
                lblpassword.Visible = false;
                lblproductname.Visible = false;
                lblPCcatch.Visible = false;
                lblmobilecatch.Visible = false;
                lblsmartdesc.Visible = false;
                lblpcitdescription.Visible = false;
                lblsaledescpc.Visible = false;
                lblblackpass.Visible = false;
                selectcsv.Visible = false;
                campaign.Visible = false;
                lblPCcatch.Visible = false;
                lblmobilecatch.Visible = false;
                lblproductname.Visible = false;

                vname.Style.Add("display", "none");
                vpccatch.Style.Add("display", "none");
                vmobilecatch.Style.Add("display", "none");
                vsmart.Style.Add("display", "none");
                vpcdesc.Style.Add("display", "none");
                vsaldesc.Style.Add("display", "none");
                vpassword.Style.Add("display", "none");
                vpointmag.Style.Add("display", "none");
                vpointperiod.Style.Add("display", "none");
                DataTable dtdeliveryr = pbl.DeliveryRakuten(itid, Shop_ID, 4);
                if (dtdeliveryr != null && dtdeliveryr.Rows.Count > 0)
                {
                    delmagno.Visible = true;
                    lbldelivemngno.Visible = true;
                    lblcontrol.Text = dtdeliveryr.Rows[0]["コントロールカラム"].ToString();
                    lblProductURL.Text = dtdeliveryr.Rows[0]["商品管理番号（商品URL）"].ToString();
                    lblproductNo.Text = dtdeliveryr.Rows[0]["商品番号"].ToString();
                    lbldelivemngno.Text = dtdeliveryr.Rows[0]["あす楽配送管理番号"].ToString();
                }//item.csv
                DataTable dtdelselectr = pbl.DeliveryRakuten(itid, Shop_ID, 5);
                if (dtdelselectr != null && dtdelselectr.Rows.Count > 0)
                {
                    selectcsv.Visible = true;
                    campaign.Visible = true;
                    gvselect.DataSource = dtdelselectr;
                    gvselect.DataBind();
                }//select.csv
                DataTable dtcatr = pbl.DeliveryRakuten(itid, Shop_ID, 6);
                if (dtcatr != null && dtcatr.Rows.Count > 0)
                {
                    itemcat.Visible = true;
                    itcat.Visible = true;
                    gvitemcat.DataSource = dtcatr;
                    gvitemcat.DataBind();
                }//item-cat.csv
            }
          }
          catch (Exception ex)
          {
              Session["Exception"] = ex.ToString();
              Response.Redirect("~/CustomErrorPage.aspx?");
          }   
        }
           

	}
}