/* 
*
Created By              : EiPhyo
Created Date          :04/2015
Updated By             :
Updated Date         :

 Tables using:  Promotion
 *                       -Promotion_Shop
 *                        -Promotion_Item
 * 
 * 
 * 
 * Storedprocedure using: SP_CampaignPromotion_EqualSearch
 *                                           -SP_CampaignPromotion_LikeSearch
 *                                           -SP_Campaing_ID
 *                                           -SP_Duplicate_Campaign_ID_Check
 *                                         
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
using ORS_RCM_BL;
using System.Collections;
using System.Data;
using System.IO;
using System.Configuration;
using ORS_RCM_Common;
using System.Globalization;


namespace ORS_RCM.WebForms.Promotion
{
    public partial class Campaign_promotion_View : System.Web.UI.Page
    {
        /*  
 
         Tables using: 
      
       * Promotion table
       * Promotion_Shop table
       * Promotion_Item table
       * 
    
     */  

        Promotion_BL promotionbl;
        Shop_BL shopBL;
        Promotion_Shop_BL promotionShopBL;

        string exportPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();

        public string getPostBackControlName()
        {
            try
            {
                Control control = null;
                //first we will check the "__EVENTTARGET" because if post back made by       the controls
                //which used "_doPostBack" function also available in Request.Form collection.
                string ctrlname = Page.Request.Params["__EVENTTARGET"];
                if (ctrlname != null && ctrlname != String.Empty)
                {
                    control = Page.FindControl(ctrlname);
                }
                // if __EVENTTARGET is null, the control is a button type and we need to
                // iterate over the form collection to find it
                else
                {
                    string ctrlStr = String.Empty;
                    Control c = null;
                    foreach (string ctl in Page.Request.Form)
                    {
                        //handle ImageButton they having an additional "quasi-property" in their Id which identifies
                        //mouse x and y coordinates
                        if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                        {
                            ctrlStr = ctl.Substring(0, ctl.Length - 2);
                            c = Page.FindControl(ctrlStr);
                        }
                        else
                        {
                            c = Page.FindControl(ctl);
                        }
                        if (c is System.Web.UI.WebControls.Button || c is System.Web.UI.WebControls.ImageButton)
                        {
                            control = c;
                            break;
                        }
                    }
                }

                if (control != null)
                    return control.ID;
                else return null;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtshippingno.Attributes.Add("onKeyPress", "doClick('" + btnSearch.ClientID + "',event)");
            if (!Page.IsPostBack)
            {
                DataTable dt = Bind(0);
                if (dt!=null && dt.Rows.Count > 0)
                {
                 
                    int totalcount = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                    int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                    gvPromotion.PageSize = pageSize;
                    gvPromotion.DataSource = dt;
                    gvPromotion.DataBind();
                    gp1.CalculatePaging(totalcount, pageSize, 1);
                   
                }
                else gp1.CalculatePaging(0, gvPromotion.PageSize, 1);

                SetShops();
                GetShops();

            }
            else
            {
                BindDatetime();
                String ctrl = getPostBackControlName();
                if (ctrl.Contains("lnkPaging"))
                {
                    gp1.LinkButtonClick(ctrl, gvPromotion.PageSize);

                    Label lbl = gp1.FindControl("lblCurrent") as Label;
                    int index = Convert.ToInt32(lbl.Text);
                    DataTable dt = Bind(index-1);
                    if (dt != null || dt.Rows.Count > 0)
                    {
                        int totalcount = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                        int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                        gvPromotion.DataSource = dt;
                        gvPromotion.DataBind();
                        gp1.CalculatePaging(totalcount, pageSize, 1);
                    }
                }                
            }
        }

        public void GetShop()
        {
            shopBL = new Shop_BL();
            shopBL.SelectAll();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/Promotion/Campaign_promotion.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "SearchClick();", true);     

            DataTable dt = Bind(0);
            if (dt != null && dt.Rows.Count > 0)
            {
                int totalcount = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                gvPromotion.DataSource = dt;
                gvPromotion.DataBind();
                gp1.CalculatePaging(totalcount, pageSize, 1);
            }
            else
            {
                gvPromotion.DataSource = dt;
                gvPromotion.DataBind();
                gp1.CalculatePaging(0, gvPromotion.PageSize, 1);
            }
        }

        public void SaveShops(int promotionID)
        {
            promotionShopBL = new Promotion_Shop_BL();
            DataTable dt = new DataTable();
            dt.Columns.Add("PromotionID", typeof(int));
            dt.Columns.Add("ShopID", typeof(int));
            for (int i = 0; i < lstTargetShop.Items.Count; i++)
            {
                if (lstTargetShop.Items[i].Selected)
                {
                    dt.Rows.Add(promotionID, int.Parse(lstTargetShop.Items[i].Value.ToString()));
                }
            }
            promotionShopBL.Insert(dt);
        }
      
        protected void gvPromotion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DataEdit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("~/WebForms/Promotion/Campaign_promotion.aspx?ID=" + ID);
            }

            if (e.CommandName == "DataCopy")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect(String.Format("~/WebForms/Promotion/Campaign_promotion.aspx?ID={0}&status={1}", ID, "copy"));
            }
        }

        public void GetShops()
        {
            shopBL = new Shop_BL();
            DataTable dt = shopBL.SelectShopAndMall();
            lstTargetShop.DataSource = dt;
            lstTargetShop.DataValueField = "ID";
            lstTargetShop.DataTextField = "Shop_Name";
            lstTargetShop.DataBind();

        }

        private DateTime DateConverter(string dateTime)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd-MM-yyyy";
            dtfi.DateSeparator = "-";
            DateTime objDate = Convert.ToDateTime(dateTime, dtfi);
            string date = Convert.ToDateTime(objDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy hh:MM:ss");
            objDate = DateTime.ParseExact(date, "MM/dd/yyyy hh:MM:ss", null);

            return objDate;
        }

        public Campaign_Entity GetSearchData()
        {
            string fromDate = Request.Form[txtPeriod_From.UniqueID];
            string toDate = Request.Form[txtPeriod_To.UniqueID];
            hdfFromDate.Value = fromDate;
            hdfToDate.Value = toDate;
            DateTime? FromDate = new DateTime();
            DateTime? ToDate = new DateTime();
            if (!String.IsNullOrEmpty(fromDate))
            {
                FromDate = DateConverter(fromDate);
            }
            else
            {
                FromDate = null;
            }
            if (!String.IsNullOrEmpty(toDate))
            {
                ToDate = DateConverter(toDate);
            }
            else
            {
                ToDate = null;
            }

            Campaign_Entity ce = new Campaign_Entity();
            ce.Campaign_ID = txtCampaign_ID.Text;
            ce.Promotion_Name = txtCampaign_Name.Text;

            string campaignType = string.Empty;
    

            for (int i = 0; i < lstCampaignType.Items.Count; i++)
            {
                if (lstCampaignType.Items[i].Selected)
                {
                    if (String.IsNullOrWhiteSpace(campaignType))
                        campaignType = lstCampaignType.Items[i].Value;
                    else
                        campaignType = campaignType + "," + lstCampaignType.Items[i].Value;
                }
            }
         

            ce.Campaign_Guideline = campaignType;
            
            string campaignShop = string.Empty;
            for (int i = 0; i < lstTargetShop.Items.Count; i++)
            {
                if (lstTargetShop.Items[i].Selected)
                {
                    if (String.IsNullOrWhiteSpace(campaignShop))
                        campaignShop = lstTargetShop.Items[i].Value;
                    else
                        campaignShop = campaignShop + "," + lstTargetShop.Items[i].Value;
                }
            }

            ce.Shop_Name = campaignShop;
            ce.Subjects = txtSubject.Text;
            ce.Target_Brand = txtTarget_Brand.Text;
            ce.Instruction_No = txtInstruction_No.Text;
            ce.Priority = ddlPrioritites.SelectedItem.ToString();
            ce.Remark = txtRemark.Text;
            
            ce.Period_From = FromDate;
            ce.Period_To = ToDate;


            if (chkPublic.Checked)
                ce.IsPublic = true;
            else ce.IsPublic = false;

            if (chkPresent.Checked)
                ce.IsPresent = true;
            else ce.IsPresent = false;

            ce.Item = txtshippingno.Text;
            
            return ce;
        }

        protected DataTable Bind(int index)
        {
            promotionbl = new Promotion_BL();
            Campaign_Entity ce = GetSearchData();
            DataTable dt;
            if (chkFull.Checked)
            {
                dt = promotionbl.PromotionSearch(ce, index+1, ddlpage.SelectedValue,1);//1 -- equal search
            }
            else
                dt = promotionbl.PromotionSearch(ce, index + 1, ddlpage.SelectedValue, 0);// 0 -- like search
            return dt;
        }
  

        protected void gvPromotion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Image rbase = (Image)e.Row.FindControl("rbseball");
            Image ybp = (Image)e.Row.FindControl("by");
            Image yhp = (Image)e.Row.FindControl("hp");
            Image Rlp = (Image)e.Row.FindControl("rlp");
            Image Ylp = (Image)e.Row.FindControl("ylp");
            Image rrp = (Image)e.Row.FindControl("rrp");
            Image ablc = (Image)e.Row.FindControl("ablack");
            Image Yrp = (Image)e.Row.FindControl("yrp");
            Image Prp = (Image)e.Row.FindControl("prp");
            Image Rsp = (Image)e.Row.FindControl("rsp");
            Image Ysp = (Image)e.Row.FindControl("ysp");
            Image Pbp = (Image)e.Row.FindControl("pbp");
            Image Abp = (Image)e.Row.FindControl("abp");
            Image Rhp = (Image)e.Row.FindControl("rhp");
            Image Ajp = (Image)e.Row.FindControl("ajp");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int promotionID = int.Parse(gvPromotion.DataKeys[e.Row.RowIndex].Value.ToString());

                //promotionbl = new Promotion_BL();
                //DataTable IDdt = new DataTable();
                //IDdt = Search();
                 DataTable dt = promotionbl.GetShopNamesByID(promotionID);

                DropDownList ddlShop = e.Row.FindControl("ddlShop") as DropDownList;

                ListBox lstShop = e.Row.FindControl("lstTargetShop") as ListBox;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlShop.Items.Insert(i, dt.Rows[i]["Shop_Code"].ToString());
                    string mallid = dt.Rows[i]["Mall_ID"].ToString();
                    //string shopname =dt.Rows[i]["Shop_Name"].ToString();
                    string shopname = dt.Rows[i]["Shop_Code"].ToString();
                    string shopid = dt.Rows[i]["Shop_Code"].ToString();
                    if (mallid == "1")
                    {
                        if (shopname.Trim() == "Racket Plaza")
                        { rrp.Visible = true; }
                        else if (shopname.Trim() == "Sports Plaza")
                            Rsp.Visible = true;
                        else if (shopname.Trim() == "Baseball Plaza")
                            rbase.Visible = true;
                        else if (shopname.Trim() == "Table Tennis Honpo_delete")
                            Rhp.Visible = true;
                        else if (shopname.Trim() == "Table Tennis Honpo")
                            Rhp.Visible = true;
                        else if (shopname.Trim() == "Rack piece")
                            Rlp.Visible = true;
                    }
                    else if (mallid == "2")
                    {

                        //if (shopname == "Racket Plaza")
                        if (String.Equals(shopname.Trim(), "Racket Plaza"))
                        { Yrp.Visible = true; }
                        else if (shopname.Trim() == "Sports Plaza")
                            Ysp.Visible = true;
                        else if (shopname.Trim() == "Baseball Plaza")
                            ybp.Visible = true;
                        else if (shopname.Trim() == "Table Tennis Honpo_delete")
                            yhp.Visible = true;
                        else if (shopname.Trim() == "Table Tennis Honpo")
                            yhp.Visible = true;
                        else if (shopname.Trim() == "Rack piece")
                            Ylp.Visible = true;
                    }
                    else if (mallid == "3")
                    {
                        if (shopname.Trim() == "Racket Plaza")
                        { Prp.Visible = true; }
                        else if (shopname.Trim() == "Sports Plaza")
                            e.Row.Cells[5].Text = "Sports Plaza Ponpare";
                        else if (shopname.Trim() == "Baseball Plaza")
                            Pbp.Visible = true;
                        else if (shopname.Trim() == "Table Tennis Honpo")
                            e.Row.Cells[5].Text = "Table Tennis Honpo Ponpare";
                        else if (shopname.Trim() == "Rack piece")
                            e.Row.Cells[5].Text = "Rack piece Ponpare";
                    }
                    else if (mallid == "4")
                    {
                        if (shopname.Trim() == "Racket Plaza")
                        { ablc.Visible = true; }
                        else if (shopname.Trim() == "Sports Plaza")
                            e.Row.Cells[5].Text = "Sports Plaza Amazone";
                        else if (shopname.Trim() == "Baseball Plaza")
                            Abp.Visible = true;
                        else if (shopname.Trim() == "Table Tennis Honpo")
                            e.Row.Cells[5].Text = "Table Tennis Honpo Amazone";
                        else if (shopname.Trim() == "Rack piece")
                            e.Row.Cells[5].Text = "Rack piece Amazone";
                    }
                    else if (mallid == "5")
                    {
                        if (shopname.Trim() == "Racket Plaza")
                            Ajp.Visible = true;
                        else if (shopname.Trim() == "Sports Plaza")
                            e.Row.Cells[5].Text = shopid;
                        else if (shopname.Trim() == "Baseball Plaza")
                            e.Row.Cells[5].Text = shopid;
                        else if (shopname.Trim() == "Table Tennis Honpo")
                            e.Row.Cells[5].Text = shopid;
                        else if (shopname.Trim() == "Rack piece")
                            e.Row.Cells[5].Text = shopid;
                    }

                }

                if (DataBinder.Eval(e.Row.DataItem, "Campaign_TypeID").ToString() == "0")
                {
                    e.Row.Cells[3].Text = "商品別ポイント";
                }

                else if (DataBinder.Eval(e.Row.DataItem, "Campaign_TypeID").ToString() == "1")
                {
                    e.Row.Cells[3].Text = "店舗別ポイント";
                }


                else if (DataBinder.Eval(e.Row.DataItem, "Campaign_TypeID").ToString() == "2")
                {
                    e.Row.Cells[3].Text = "商品別クーポン";

                }

                else if (DataBinder.Eval(e.Row.DataItem, "Campaign_TypeID").ToString() == "3")
                {
                    e.Row.Cells[3].Text = "送料";
                }

                else if (DataBinder.Eval(e.Row.DataItem, "Campaign_TypeID").ToString() == "4")
                {
                    e.Row.Cells[3].Text = "即日出荷";
                }

                else if (DataBinder.Eval(e.Row.DataItem, "Campaign_TypeID").ToString() == "5")
                {
                    e.Row.Cells[3].Text = "予約";
                }
                else if (DataBinder.Eval(e.Row.DataItem, "Campaign_TypeID").ToString() == "6")
                {
                    e.Row.Cells[3].Text = "事前告知";
                }

                else if (DataBinder.Eval(e.Row.DataItem, "Campaign_TypeID").ToString() == "7")
                {
                    e.Row.Cells[3].Text = "シークレット";
                }

                else if (DataBinder.Eval(e.Row.DataItem, "Campaign_TypeID").ToString() == "8")
                {
                    e.Row.Cells[3].Text = "プレゼントキャンペーン";
                }

                else e.Row.Cells[3].Text= String.Empty;
            }

        }

       public void SetShops()
        {
            promotionShopBL = new Promotion_Shop_BL();
            DataTable dt = promotionShopBL.Search_TargetShop();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                foreach (ListItem item in lstTargetShop.Items)
                {
                    if (dt.Rows[i]["Shop_ID"].ToString() == item.Value)
                    {
                        item.Selected = true; item.Enabled = true;
                    }

                }
            }

        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvPromotion.PageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                if (gvPromotion.Rows.Count > 0)
                {
                    DataTable dt = Bind(0);
                    if (dt != null || dt.Rows.Count > 0)
                    {
                        int totalcount = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                        int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                        gvPromotion.DataSource = dt;
                        gvPromotion.DataBind();
                        gp1.CalculatePaging(totalcount, pageSize, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }


        //protected void BindPage_dropdownlist()
        //      {
        //          try
        //          {
        //          promotionbl = new Promotion_BL();
        //          DataTable dt = new DataTable();



        //          string fromDate = Request.Form[txtPeriod_From.UniqueID];
        //          string toDate = Request.Form[txtPeriod_To.UniqueID];
        //          hdfFromDate.Value = fromDate;
        //          hdfToDate.Value = toDate;
        //          DateTime? FromDate = new DateTime();
        //          DateTime? ToDate = new DateTime();
        //          if (!String.IsNullOrEmpty(fromDate))
        //          {
        //              FromDate = DateConverter(fromDate);
        //          }
        //          else
        //          {
        //              FromDate = null;
        //          }
        //          if (!String.IsNullOrEmpty(toDate))
        //          {
        //              ToDate = DateConverter(toDate);
        //          }
        //          else
        //          {
        //              ToDate = null;
        //          }


        //         dt = promotionbl.Search_CampaignPromotion(txtCampaign_ID.Text, txtCampaign_Name.Text, lstTargetShop.Text, txtTarget_Brand.Text, FromDate, ToDate,txtInstruction_No.Text); 

        //          gvPromotion.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
        //          //gvItem.DataSource = Search(0, 1);
        //          gvPromotion.DataSource = promotionbl.Search_CampaignPromotion(txtCampaign_ID.Text, txtCampaign_Name.Text, lstTargetShop.Text, txtTarget_Brand.Text, FromDate, ToDate,txtInstruction_No.Text);//Select all c;
        //          gvPromotion.DataBind();
        //          //gvPromotion.CalculatePaging(count, gvPromotion.PageSize, 1);
        //          UCGrid_Paging1.CalculatePaging(dt.Rows.Count, gvPromotion.PageSize, 1);

        //          }
        //          catch (Exception ex)
        //          {
        //              Session["Exception"] = ex.ToString();
        //              Response.Redirect("~/CustomErrorPage.aspx?");
        //          }
        //      }


        protected void BindDatetime()
        {
            try
            {
                //Save
                if (!string.IsNullOrWhiteSpace(Request.Form[txtPeriod_From.UniqueID]))
                    txtPeriod_From.Text = Request.Form[txtPeriod_From.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtPeriod_To.UniqueID]))
                    txtPeriod_To.Text = Request.Form[txtPeriod_To.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }

        protected void btnCampaign_Schedule_Click(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtPeriod_From.Text = String.Empty;

                txtPeriod_To.Text = Request.Form[txtPeriod_To.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtPeriod_To.Text = String.Empty;

                txtPeriod_From.Text = Request.Form[txtPeriod_From.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

    }
}








