using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Item_Exhibition
{
    public partial class Details_of_exhibition_Amazone_ : System.Web.UI.Page
    {
        Details_Amazone_Exhibition_BL dbl;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            if (!IsPostBack) 
            {
                string itemcode = null;
                Exhibition_List_BL ehb = new Exhibition_List_BL();
                dbl = new Details_Amazone_Exhibition_BL();
                if (Request.QueryString["Item_ID"] != null) 
                {
                    string id = Request.QueryString["Item_ID"].ToString();
                    int itemid = Convert.ToInt32(id);
                    GetData(id);
                    DataTable dt = dbl.SelectbyID(itemid);
                    if(dt!= null && dt.Rows.Count >0)
                        itemcode = dt.Rows[0]["Item_Code"].ToString();
                    DataTable dterror = ehb.Selectexerror(itemcode, Shop_ID,2);
                       if (dterror != null && dterror.Rows.Count > 0) 
                       {
                          
                               lblitemerror.Text = dterror.Rows[0]["Error_Description"].ToString() ;
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

        protected void GetData(string  id) 
        {
           try
          {
            dbl = new Details_Amazone_Exhibition_BL();
            //Item_Master_BL imbl = new Item_Master_BL();

            DataTable dt = dbl.SelectByExhibitionData(id);
            if (dt != null && dt.Rows.Count > 0) 
            {
                if (dt.Rows[0]["IsSKU"].ToString() == "0")
                {
                    lblsku.Text = "Not SKU"; 
                }
                else if (dt.Rows[0]["IsSKU"].ToString() == "1")
                {
                    lblsku.Text = " SKU"; 
                }
                else if (dt.Rows[0]["IsSKU"].ToString() == "2")
                {
                    lblsku.Text = " SKU_Change";
                }

                lblprice.Text = dt.Rows[0]["price"].ToString();
                lblquantity.Text = dt.Rows[0]["quantity"].ToString();
                lblproductid.Text = dt.Rows[0]["product-id"].ToString();
                lblproductidtype.Text = dt.Rows[0]["product-id-type"].ToString();
                lblcondition.Text = dt.Rows[0]["condition-type"].ToString();
                lblconditionnote.Text = dt.Rows[0]["condition-note"].ToString();
                lblasin.Text = dt.Rows[0]["ASIN-hint"].ToString();
                lbltitle.Text = dt.Rows[0]["title"].ToString();
                lblpoartype.Text = dt.Rows[0]["operation-type"].ToString();
                lblsaleprice.Text = dt.Rows[0]["sale-price"].ToString();
                lblsalesdate.Text = dt.Rows[0]["sale-start-date"].ToString();
                lblsaleenddate.Text = dt.Rows[0]["sale-end-date"].ToString();
                lblleadship.Text = dt.Rows[0]["leadtime-to-ship"].ToString();
                lbllunchdate.Text = dt.Rows[0]["launch-date"].ToString();
                lblgiftava.Text = dt.Rows[0]["is-giftwrap-available"].ToString();
                lblgiftmsg.Text = dt.Rows[0]["is-gift-message-available"].ToString();
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