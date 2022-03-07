/* 
Created By              : Eephyo
Created Date          : 09/07/2014
Updated By             :
Updated Date         :

 Tables using: Mall_Setting_Yahoo_Fixed,Mall_Setting_Yahoo_Default
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
    public partial class Details_of_exhibition_Yahoo_ : System.Web.UI.Page
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
      
    

        Details_of_Yahoo_Exhibition_BL detailYbl;
        Details_of_exhibition_Yahoo_Entity detailYentity;
        Item_Master_BL itmbl; string itid, itemcode;
        protected void Page_Load(object sender, EventArgs e)
        {
            itmbl = new Item_Master_BL();
           try
           {
               if (!IsPostBack)
               {
                   Exhibition_List_BL ehb = new Exhibition_List_BL();
                   detailYbl = new Details_of_Yahoo_Exhibition_BL();
                   DataTable dterror = new DataTable();
                   int id = Shop_ID;
                   if (Request.QueryString["Item_ID"] != null)
                   {
                       itid = Request.QueryString["Item_ID"].ToString();
                   }

                   int itemid = Item_ID;

                   detailYentity = detailYbl.SelectByID(id);

                   //ShowData(detailYentity);
                   // GetData(itemid);
                   string Ctrl_ID = GetYahooData(itid);
                   if (Ctrl_ID != "d")
                   {
                       DataTable dt = ehb.SelectByItemDataForYahoo(itid, "quantity",id);
                       gvquantity.DataSource = dt;
                       gvquantity.DataBind();

                       DataTable dts = detailYbl.SelectbyItemID(itemid);
                       if (dts != null && dts.Rows.Count > 0)
                       {
                           if (!String.IsNullOrWhiteSpace(dts.Rows[0]["Item_Code"].ToString()))
                               itemcode = dts.Rows[0]["Item_Code"].ToString();
                       }
                   }
                       itemcode = lblItemCode.Text; //added by hlz.

                       dterror = ehb.Selectexerror(itemcode, id, 2, itemid);
                       if (dterror != null && dterror.Rows.Count > 0)
                       {
                           for (int i = 0; i < dterror.Rows.Count; i++)
                               lblyitemerror.Text += dterror.Rows[i]["Error_Description"].ToString() + "\n\n";
                       }
                       dterror = null;
                       dterror = ehb.Selectexerror(itemcode, id, 1, itemid);
                       if (dterror != null && dterror.Rows.Count > 0)
                       {

                           lblyselecterror.Text = dterror.Rows[0]["Error_Description"].ToString();
                       }
                   }
               }
           catch (Exception ex)
           {
               Session["Exception"] = ex.ToString();
               Response.Redirect("~/CustomErrorPage.aspx?");
           }
        }

        protected string GetYahooData(string itid) 
        {
           try
           {
               Exhibition_List_BL ehb = new Exhibition_List_BL();
             itmbl = new Item_Master_BL();
            int shid =Shop_ID;
            string Ctrl_ID = string.Empty;
             //DataTable dt = itmbl.SelectByItemDataForYahoo(itid,"item");
            DataTable dt = ehb.SelectByItemDataForYahoo(itid, "item", shid);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["コントロールカラム"].ToString() == "d")
                {
                    Ctrl_ID = "d";
                    dt = ehb.SelectByItemDataForYahoo(itid, "data-del", shid);
                    lblpath.Text = dt.Rows[0]["path"].ToString();
                    lblItemCode.Text = dt.Rows[0]["code"].ToString();
                    lblItemName.Text = dt.Rows[0]["name"].ToString();
                    lblregularprice.Text = dt.Rows[0]["price"].ToString();
                    
                }
                else
                {
                    Ctrl_ID = "n";
                    Export_CSV3 exportCSV3 = new Export_CSV3();
                    DataTable dtItemMaster = exportCSV3.ModifyTable(dt, shid);
                    if (dtItemMaster != null)
                    {
                        dt = dtItemMaster;
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblpath.Text = dt.Rows[0]["path"].ToString();
                        lblSubCode.Text = dt.Rows[0]["sub-code"].ToString();
                        lblsubcodeparam.Text = dt.Rows[0]["subcode_param"].ToString();
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
                        lblspadditional.Text = dt.Rows[0]["sp-additional"].ToString();
                    }
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
       
        protected void GetData(int itemid) 
        {
          try
           {
            detailYbl = new Details_of_Yahoo_Exhibition_BL();
            DataTable dt = detailYbl.SelectbyItemID(itemid);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblItemCode.Text = dt.Rows[0]["Item_Code"].ToString();
                lblItemName.Text = dt.Rows[0]["Item_Name"].ToString();
             
                if (dt.Rows[0]["JAN_Code"].ToString() != null)
                {
                    lblJanCode.Text = dt.Rows[0]["JAN_Code"].ToString();
                }
            
                lblBrandCode.Text = dt.Rows[0]["Brand_Code_Yahoo"].ToString();
            }
           }
             catch (Exception ex)
             {
                 Session["Exception"] = ex.ToString();
                 Response.Redirect("~/CustomErrorPage.aspx?");
             }
        }
        public void ShowData(Details_of_exhibition_Yahoo_Entity detailYentity)
        {

          
           
          
         }
    }
}