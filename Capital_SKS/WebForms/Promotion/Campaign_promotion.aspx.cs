/* 
*
Created By              : EiPhyo
Created Date          :04/2015
Updated By             :
Updated Date         :

 Tables using:   Promotion
 *                       - Promotion_Shop
 *                        -Promotion_Item
 *                        -Item_Master
 * 
 * Storedprocedure using: SP_Promotion_Item_InsertUpdate
 *                                           -SP_Promotion_ItemOption_InsertUpdate
 *                                           -SP_Promotion_Attatchment_InsertUpdate
 *                                           -SP_Select_CampaingPromotionItemCode
 *                                           -SP_Promotion_InsertUpdate
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
using ORS_RCM_Common;
using ORS_RCM_BL;
using System.Data;
using System.Configuration;
using System.Transactions;
using System.Collections;

namespace ORS_RCM
{
    public partial class Campaign_promotion : System.Web.UI.Page
    {
        /* 
 
 Tables using: 
      
         * Promotion table
         * Promotion_Shop table
         * Promotion_Item table
         * 
     
    */
        Promotion_Entity promotion;
        Campaign_Entity campaign;
        Promotion_BL promotionBL;
        Promotion_Item_BL promotionItemBL;
        Promotion_ItemOptionBL optionBL;
        Promotion_Attatchment_BL promotionAttatchmentBL;
        Shop_BL shopBL;
        Promotion_Shop_BL promotionShopBL;
        string imagePath = ConfigurationManager.AppSettings["ItemImage"].ToString();
        string promotionAttatchmentPath = ConfigurationManager.AppSettings["PromotionAttatchment"].ToString();
        string ControlID = "";
        public DataTable ItemList
        {
            get
            {
                if (Session["ItemCode"] != null)
                {

                    DataTable dt = (DataTable)Session["ItemCode"];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public int PID
        {
            get
            {
                if (Request.QueryString["ID"] != null)
                {
                    return int.Parse(Request.QueryString["ID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        public string Status
        {
            get
            {
                if (Request.QueryString["status"] != null)
                {
                    return Request.QueryString["status"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        public DataTable ImageList
        {
            get
            {
                if (Session["ImageList"] != null)
                {
                    DataTable dt = (DataTable)Session["ImageList"];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public string ItemCode
        {
            get
            {
                if (Request.QueryString["Item_Code"] != null)
                {
                    return Request.QueryString["Item_Code"].ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public string Shop_ID
        {
            get
            {
                if (Request.QueryString["Shop_ID"] != null)
                {
                    return Request.QueryString["Shop_ID"].ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable Option
        {
            get
            {
                if (Session["Option"] != null)
                {
                    DataTable dt = (DataTable)Session["Option"];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public void ShowOption()
        {
            if (Option != null && Option.Rows.Count > 0)
            {
                DataTable dt = Option as DataTable;
                SetOption(dt);
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            promotion = new Promotion_Entity();
            campaign = new Campaign_Entity();
            promotionBL = new Promotion_BL();
            optionBL = new Promotion_ItemOptionBL();
            btnAddItemCode.Attributes.Add("onclick", "javascript:ShowItemList(" + btnAddItemCode.ClientID + ")");
            lnkAddPhoto.Attributes.Add("onclick", "javascript:ShowDialog('0'," + lnkAddPhoto.ClientID + ")");
            int j = 0;
            if (!IsPostBack)
            {
                //After Save Successful or Update Successful , Back to pervious page
                #region BackPage ViewState
                String backpage = string.Empty;
                if (Request.UrlReferrer != null)
                {
                    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                    backpage = Request.UrlReferrer.ToString();
                }
                else
                {
                    ViewState["UrlReferrer"] = backpage;
                }
                #endregion
                GetShops();
                if (ItemCode != null)    //Come from  Exhibition-List for edit
                {
                    int CampID = promotionBL.SelectPitemID(ItemCode, Shop_ID);
                    campaign = promotionBL.SelectByID(CampID);
                    SetCampaignData(campaign);
                    SetPromotionItem();
                    SetShopsfromExhibition();
                    BindPhotoList();
                }
                if (PID != 0)
                {
                    campaign = promotionBL.SelectByID(PID);
                    SetCampaignData(campaign);
                    SetPromotionItem();
                    SetShops();
                    BindPhotoList();
                    if (Status != "")
                    {
                        head.InnerText = "キャンペーン プロモーション登録";
                        Readonlydata();
                        BindPhotoList();
                        SetPromotionItem();
                    }
                    else
                    {
                        head.InnerText = "キャンペーン プロモーション編集";
                    }
                    GetOptionSelectByPromotionID();
                    BindPhotoList();
                    SetAttatchment();
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(Request.Form[CustomHiddenField.UniqueID]))
                {
                    ControlID = Request.Form[CustomHiddenField.UniqueID];
                }
                if (ControlID.Contains("btnAddOption"))
                {
                    ShowOption();
                }
                if (ControlID.Contains("lnkAddPhoto"))
                {
                    BindPhotoList();
                }
                if (ControlID.Contains("btnAddItemCode"))
                {
                    if (Session["btnPopClick"] != null && Session["btnPopClick"].ToString() == "ok")
                    {
                        GETItemCode();
                        Session.Remove("btnPopClick");
                    }
                }
                MaintainPeriodStates();
                string name = getPostBackControlName();
                string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
                if (name == null)
                {
                    if (name == null && eventTarget == "Item_Code")
                    {
                        if (j == 0)
                        {
                            j = 1;
                        }
                        else if (j == 1)
                        {
                            test.Attributes["class"] = "setListBox iconSet iconCheck";
                            head.InnerText = "キャンペーン プロモーション編集確認";
                        }
                    }
                    else if (name == null && eventTarget == "")
                    {
                        test.Attributes["class"] = "setListBox iconSet iconCheck";
                        head.InnerText = "キャンペーン プロモーション編集確認";
                    }
                }
            }
        }

        protected DataTable GETItemCode()
        {
            promotionItemBL = new Promotion_Item_BL();
            DataTable dtM = new DataTable();
            dtM = promotionItemBL.SelectByPromotionID(PID);
            if (ItemList != null)
            {
                dtM.Merge(ItemList);
                dtM = RemoveDuplicateRows(dtM, "Item_Code");
                String ItemCode = String.Empty;
                if (dtM.Rows.Count > 0)
                {
                    for (int i = 0; i < dtM.Rows.Count; i++)
                    {
                        ItemCode += dtM.Rows[i][2].ToString() + ",";
                    }
                    String item = ItemCode.Remove(ItemCode.Length - 1);
                    txtPromotionItem.Text = item;
                }
                return dtM;
            }
            else return null;
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            try
            {
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();
                //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                //And add duplicate item value in arraylist.
                foreach (DataRow drow in dTable.Rows)
                {
                    if (hTable.Contains(drow[colName]))
                        duplicateList.Add(drow);
                    else
                        hTable.Add(drow[colName], string.Empty);
                }
                //Removing a list of duplicate items from datatable.
                foreach (DataRow dRow in duplicateList)
                    dTable.Rows.Remove(dRow);
                //Datatable which contains unique records will be return as output.
                return dTable;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }

        protected void Save()
        {
            promotionBL = new Promotion_BL();
            promotionItemBL = new Promotion_Item_BL();
            optionBL = new Promotion_ItemOptionBL();
            if (PID == 0)
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    GetPromotionData();
                    if ((!String.IsNullOrWhiteSpace(txtCampaignID.Text)) && (promotionBL.Record_CampaginIDexisted(txtCampaignID.Text)))
                    {
                        MessageBox("Campaign_ID already exists.");
                    }
                    else
                    {
                        int promotionID = promotionBL.SaveUpdate(campaign, "Save");
                        string[] arr = txtPromotionItem.Text.Split(',');
                        arr = arr.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        DataTable dt = GETItemCode();
                        if (promotionID > 0)
                        {
                            SaveUpdateOption(promotionID);
                            SavePromotionAttatchments(promotionID);
                            SaveShops(promotionID);
                            MessageBox("Saving Successful ! ");
                        }
                        tran.Complete();
                    }
                }
            }
            else
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    GetPromotionData();
                    int promotionID = promotionBL.SaveUpdate(campaign, "Update");
                    promotionBL = new Promotion_BL();
                    DataTable dtduplicate = promotionBL.Duplicate_CampaignID(txtCampaignID.Text, promotionID);
                    if (dtduplicate.Rows.Count == 1)
                    {
                        GlobalUI.MessageBox("Campaign_ID already existed");
                    }
                    else
                    {
                        SaveUpdateOption(promotionID);
                        SavePromotionAttatchments(promotionID);
                        SaveShops(promotionID);
                        MessageBox("Updating Successful ! ");
                        tran.Complete();
                    }
                }
            }
        }

        public void SetShopsfromExhibition()
        {
            promotionShopBL = new Promotion_Shop_BL();
            if (ItemCode != null)    //Come from  Exhibition-List for edit
            {
                int CampID = promotionBL.SelectPitemID(ItemCode, Shop_ID);
                DataTable dt = promotionShopBL.SelectByPromotionID(CampID);
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
        }

        public void SetShops()
        {
            promotionShopBL = new Promotion_Shop_BL();
            DataTable dt = promotionShopBL.SelectByPromotionID(PID);
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

        public void Readonlydata()
        {
            txtCampaign_Name.ReadOnly = true;
            txtCampaign_Guideline.ReadOnly = true;
            txtBrand_Name.ReadOnly = true;
            txtcampaignSmrt_url.ReadOnly = true;
            txtCampaignID.ReadOnly = true;
            txtCampaign_url.ReadOnly = true;
            txtpfrom.Visible = true;
            txtpfrom.Text = txtPeriod_From.Text;
            txtpto.Visible = true;
            txtpto.Text = txtPeriod_To.Text;
            txtPeriod_From.Visible = false;
            txtPeriod_To.Visible = false;
            txtSecret_ID.ReadOnly = true;
            txtSecret_Password.ReadOnly = true;
            lstTargetShop.Enabled = false;
            txtPromotionItem.ReadOnly = true;
            txtOption_Name1.ReadOnly = true;
            txtOption_Name2.ReadOnly = true;
            txtOption_Name3.ReadOnly = true;
            txtOption_Value1.ReadOnly = true;
            txtOption_Value2.ReadOnly = true;
            txtOption_Value3.ReadOnly = true;
            txtPCCampaig1.ReadOnly = true;
            txtPC_Campaign2.ReadOnly = true;
            txtSmart_Campaign1.ReadOnly = true;
            txtSmart_Campaign2.ReadOnly = true;
            fuRakuten_Gold1.Enabled = false;
            fuRakuten_Gold2.Enabled = false;
            fuRakuten_Gold3.Enabled = false;
            fuRakuten_Gold4.Enabled = false;
            fuRakuten_Cabinet1.Enabled = false;
            fuRakuten_Cabinet2.Enabled = false;
            fuRakuten_Cabinet3.Enabled = false;
            fuRakuten_Cabinet4.Enabled = false;
            fuYahoo1.Enabled = false;
            fuYahoo2.Enabled = false;
            fuYahoo3.Enabled = false;
            fuYahoo4.Enabled = false;
            fuPonpare1.Enabled = false;
            fuPonpare2.Enabled = false;
            fuPonpare3.Enabled = false;
            fuPonpare4.Enabled = false;
            rdolStatus.Enabled = false;
            chkIsPromotionClose.Enabled = false;
            ddlPeriodFromHour.Enabled = false;
            ddlPeriodFromMinute.Enabled = false;
            ddlPeriodToHour.Enabled = false;
            ddlPeriodToMinute.Enabled = false;
            ddlBlackMarket.Enabled = false;
            ddlPriorities.Enabled = false;
            ddlPublication.Enabled = false;
            txtemailmagzine1.ReadOnly = true;
            txtemailmagzine2.ReadOnly = true;
            txtemailmagzine3.ReadOnly = true;
            txtGiftContent.ReadOnly = true;
            txtGiftway.ReadOnly = true;
            txtInstructionNo.ReadOnly = true;
            lstCampaignType.Enabled = false;
            txtRemark.ReadOnly = true;
            txtproduction_detail.ReadOnly = true;
            txtRelated_information.ReadOnly = true;
            txtProduction_target.ReadOnly = true;
            txtsubject.ReadOnly = true;
            txtItem_Memo.ReadOnly = true;
            btnAddItemCode.Enabled = false;
            lnkAddPhoto.Enabled = false;
            chkGift.Enabled = false;
            txtproductname_decoration.ReadOnly = true;
            txtSmart_Catch_Copy.ReadOnly = true;
            txtpc_catchCopy.ReadOnly = true;
            txtApplicationmethod.ReadOnly = true;
        }

        private void MaintainPeriodStates()
        {
            if (!String.IsNullOrWhiteSpace(lblPeriod_from.Text.Trim()))
            {
                txtPeriod_From.Text = lblPeriod_from.Text;
                hfperiod.Value = txtPeriod_From.Text;
            }
            if (!String.IsNullOrWhiteSpace(lblperiod_to.Text.Trim()))
            {
                txtPeriod_To.Text = lblperiod_to.Text;
                hfperioto.Value = txtPeriod_To.Text;
            }
            else
            {
                txtPeriod_From.Text = hfperiod.Value;
                txtPeriod_To.Text = hfperioto.Value;
            }

            if (Request.Form[txtPeriod_From.UniqueID] != null && Request.Form[txtPeriod_To.UniqueID] != null)
            {
                txtPeriod_From.Text = Request.Form[txtPeriod_From.UniqueID].ToString();
                txtPeriod_To.Text = Request.Form[txtPeriod_To.UniqueID].ToString();
            }
        }

        public void GetOptionSelectByPromotionID()
        {
            DataTable dttmp = optionBL.SelectByPID(PID);
            if (dttmp != null && dttmp.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Name1", typeof(string));
                dt.Columns.Add("Value1", typeof(string));
                dt.Columns.Add("Name2", typeof(string));
                dt.Columns.Add("Value2", typeof(string));
                dt.Columns.Add("Name3", typeof(string));
                dt.Columns.Add("Value3", typeof(string));
                dt.Rows.Add(dttmp.Rows[0]["Option_Name"].ToString(), dttmp.Rows[0]["Option_Value"].ToString(),
                                        dttmp.Rows[1]["Option_Name"].ToString(), dttmp.Rows[1]["Option_Value"].ToString(),
                                        dttmp.Rows[2]["Option_Name"].ToString(), dttmp.Rows[2]["Option_Value"].ToString());
                SetOption(dt);
                Session["Option"] = dt;
            }
        }

        private string getPostBackControlName()
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

        public void SetPromotionData(Promotion_Entity pe)
        {
            txtsubject.Text = pe.Subjects;

            txtCampaign_Name.Text = pe.Promotion_Name;

            txtCampaignID.Text = pe.Campaign_ID;

            txtCampaign_url.Text = pe.CampaignUrl_PC;

            txtcampaignSmrt_url.Text = pe.CampaignUrl_Smart;

            txtCampaign_Guideline.Text = pe.Campaign_Guideline;

            txtRemark.Text = pe.Remark;

            txtproduction_detail.Text = pe.Production_Detail;

            txtsubject.Text = pe.Subjects;

            txtemailmagzine1.Text = pe.Mail_Magazine_Event1;

            txtemailmagzine2.Text = pe.Mail_Magazine_Event2;

            txtemailmagzine3.Text = pe.Mail_Magazine_Event3;

            txtproductname_decoration.Text = pe.Product_Decoration;

            txtpc_catchCopy.Text = pe.PC_Copy_Decoration;

            txtSmart_Catch_Copy.Text = pe.Smart_Copy_Decoration;

            DataTable dt = new DataTable();

            dt.Columns.Add("Item_Image", typeof(String));

            if (!String.IsNullOrWhiteSpace(promotion.Campaign_Image1))
            {
                dt.Rows[0]["Item_Image"] = promotion.Campaign_Image1;
            }

            if (!String.IsNullOrWhiteSpace(promotion.Campaign_Image2))
            {
                dt.Rows[1]["Item_Image"] = promotion.Campaign_Image2;
            }

            if (!String.IsNullOrWhiteSpace(promotion.Campaign_Image3))
            {
                dt.Rows[2]["Item_Image"] = promotion.Campaign_Image3;
            }

            if (!String.IsNullOrWhiteSpace(promotion.Campaign_Image4))
            {
                dt.Rows[3]["Item_Image"] = promotion.Campaign_Image4;
            }

            if (!String.IsNullOrWhiteSpace(promotion.Campaign_Image5))
            {
                dt.Rows[4]["Item_Image"] = promotion.Campaign_Image5;
            }

            Session["ImageList"] = dt;
            if (pe.Campaign_TypeID == "0")
            {
                lstCampaignType.SelectedValue = Convert.ToString("0");
            }

            else if (pe.Campaign_TypeID == "1")
            {
                lstCampaignType.SelectedValue = Convert.ToString("1");
            }

            else if (pe.Campaign_TypeID == "2")
            {
                lstCampaignType.SelectedValue = Convert.ToString("2");
            }

            else if (pe.Campaign_TypeID == "3")
            {
                lstCampaignType.SelectedValue = Convert.ToString("3");
            }

            else if (pe.Campaign_TypeID == "4")
            {
                lstCampaignType.SelectedValue = Convert.ToString("4");
            }

            else if (pe.Campaign_TypeID == "5")
            {
                lstCampaignType.SelectedValue = Convert.ToString("5");
            }

            else if (pe.Campaign_TypeID == "6")
            {
                lstCampaignType.SelectedValue = Convert.ToString("6");
            }

            else if (pe.Campaign_TypeID == "7")
            {
                lstCampaignType.SelectedValue = Convert.ToString("7");
            }

            else if (pe.Campaign_TypeID == "プレゼントキャンペーン")
            {
                lstCampaignType.SelectedValue = Convert.ToString("8");
            }

            txtBrand_Name.Text = pe.Target_Brand;

            txtInstructionNo.Text = pe.Instruction_No;

            txtApplicationmethod.Text = pe.Application_Method;

            txtGiftContent.Text = pe.Present_Contents;

            txtGiftway.Text = pe.Present_Method;

            txtProduction_target.Text = pe.Production_Target;

            if (pe.IsPresent == true)
            {
                chkGift.Checked = true;
            }
            else
            {
                chkGift.Checked = false;
            }

            if (pe.IsPublic == true)
            {
                ddlPublication.SelectedIndex = 1;
            }
            else
            {
                ddlPublication.SelectedIndex = 0;
            }
            txtCampaign_Guideline.Text = pe.Campaign_Guideline;
            if (pe.Period_From.ToString() != "")
            {
                DateTime periodFrom = DateTime.Parse(pe.Period_From.ToString());
                txtPeriod_From.Text = periodFrom.ToShortDateString();
                txtpfrom.Text = txtPeriod_From.Text;
            }
            if (pe.Period_To.ToString() != "")
            {
                DateTime periodTo = DateTime.Parse(pe.Period_To.ToString());
                txtPeriod_To.Text = periodTo.ToShortDateString();
                txtpto.Text = txtPeriod_To.Text;
            }
            txtSecret_ID.Text = pe.Secret_ID;
            txtSecret_Password.Text = pe.Secret_Password;
            txtPCCampaig1.Text = pe.PC_Campaign1;
            txtPC_Campaign2.Text = pe.PC_Campaign2;
            txtSmart_Campaign1.Text = pe.Smart_Campaign1;
            txtSmart_Campaign2.Text = pe.Smart_Campaign2;
            rdolStatus.SelectedValue = pe.Status.ToString();
            if (pe.IsPromotionClose == 1)
                chkIsPromotionClose.Checked = true;
            else
                chkIsPromotionClose.Checked = false;

            if (pe.Period_StartTime != null)
            {
                string[] startTime = pe.Period_StartTime.Split(':');
                ddlPeriodFromHour.Text = startTime[0];
                ddlPeriodFromMinute.Text = startTime[1];
            }

            if (pe.Period_EndTime != null)
            {
                string[] endTime = pe.Period_EndTime.Split(':');
                ddlPeriodToHour.Text = endTime[0];
                ddlPeriodToMinute.Text = endTime[1];
            }
            Session["pe"] = pe;
        }

        public void SetCampaignData(Campaign_Entity campaign)
        {
            txtsubject.Text = campaign.Subjects;

            txtCampaign_Name.Text = campaign.Promotion_Name;

            txtCampaignID.Text = campaign.Campaign_ID;

            txtCampaign_url.Text = campaign.CampaignUrl_PC;

            txtcampaignSmrt_url.Text = campaign.CampaignUrl_Smart;

            txtCampaign_Guideline.Text = campaign.Campaign_Guideline;

            txtRemark.Text = campaign.Remark;

            txtproduction_detail.Text = campaign.Production_Detail;

            txtsubject.Text = campaign.Subjects;

            txtemailmagzine1.Text = campaign.Mail_Magazine_Event1;

            txtemailmagzine2.Text = campaign.Mail_Magazine_Event2;

            txtemailmagzine3.Text = campaign.Mail_Magazine_Event3;

            txtproductname_decoration.Text = campaign.Product_Decoration;

            txtpc_catchCopy.Text = campaign.PC_Copy_Decoration;

            txtSmart_Catch_Copy.Text = campaign.Smart_Copy_Decoration;

            DataTable dt = new DataTable();

            //dt.NewRow();
            dt.Columns.Add("Item_Image1", typeof(String));

            dt.Columns.Add("Item_Image2", typeof(String));

            dt.Columns.Add("Item_Image3", typeof(String));

            dt.Columns.Add("Item_Image4", typeof(String));

            dt.Columns.Add("Item_Image5", typeof(String));

            DataRow dr = dt.NewRow();

            if (!String.IsNullOrWhiteSpace(campaign.Campaign_Image1))
            {
                dr[0] = campaign.Campaign_Image1;
            }
            else
            {
                dr[0] = "";
            }

            if (!String.IsNullOrWhiteSpace(campaign.Campaign_Image2))
            {
                dr[1] = campaign.Campaign_Image2;
            }
            else
            {
                dr[1] = "";
            }


            if (!String.IsNullOrWhiteSpace(campaign.Campaign_Image3))
            {
                dr[2] = campaign.Campaign_Image3;
            }
            else
            {
                dr[2] = "";
            }

            if (!String.IsNullOrWhiteSpace(campaign.Campaign_Image4))
            {
                dr[3] = campaign.Campaign_Image4;
            }
            else
            {
                dr[3] = "";
            }

            if (!String.IsNullOrWhiteSpace(campaign.Campaign_Image5))
            {
                dr[4] = campaign.Campaign_Image5;
            }
            else
            {
                dr[4] = "";
            }

            dt.Rows.Add(dr);

            Session["ImageList"] = dt;

            if (campaign.Campaign_TypeID == "0")
            {
                lstCampaignType.SelectedValue = Convert.ToString("0");
            }

            else if (campaign.Campaign_TypeID == "1")
            {
                lstCampaignType.SelectedValue = Convert.ToString("1");
            }

            else if (campaign.Campaign_TypeID == "2")
            {
                lstCampaignType.SelectedValue = Convert.ToString("2");
            }

            else if (campaign.Campaign_TypeID == "3")
            {
                lstCampaignType.SelectedValue = Convert.ToString("3");
            }

            else if (campaign.Campaign_TypeID == "4")
            {
                lstCampaignType.SelectedValue = Convert.ToString("4");
            }

            else if (campaign.Campaign_TypeID == "5")
            {
                lstCampaignType.SelectedValue = Convert.ToString("5");
            }

            else if (campaign.Campaign_TypeID == "6")
            {
                lstCampaignType.SelectedValue = Convert.ToString("6");
            }

            else if (campaign.Campaign_TypeID == "7")
            {
                lstCampaignType.SelectedValue = Convert.ToString("7");
            }

            else if (campaign.Campaign_TypeID == "プレゼントキャンペーン")
            {
                lstCampaignType.SelectedValue = Convert.ToString("8");
            }

            txtBrand_Name.Text = campaign.Target_Brand;

            txtInstructionNo.Text = campaign.Instruction_No;

            txtApplicationmethod.Text = campaign.Application_Method;

            txtGiftContent.Text = campaign.Present_Contents;

            txtGiftway.Text = campaign.Present_Method;

            txtProduction_target.Text = campaign.Production_Target;

            if (campaign.IsPresent == true)
            {
                chkGift.Checked = true;
            }
            else
            {
                chkGift.Checked = false;
            }

            if (campaign.IsPublic == true)
            {
                ddlPublication.SelectedValue = "1";
            }
            else
            {
                ddlPublication.SelectedValue = "0";
            }
            txtCampaign_Guideline.Text = campaign.Campaign_Guideline;
            if (campaign.Period_From.ToString() != "")
            {
                DateTime periodFrom = DateTime.Parse(campaign.Period_From.ToString());
                txtPeriod_From.Text = periodFrom.ToShortDateString();
                txtpfrom.Text = txtPeriod_From.Text;
            }
            if (campaign.Period_To.ToString() != "")
            {
                DateTime periodTo = DateTime.Parse(campaign.Period_To.ToString());
                txtPeriod_To.Text = periodTo.ToShortDateString();
                txtpto.Text = txtPeriod_To.Text;
            }

            txtSecret_ID.Text = campaign.Secret_ID;
            txtSecret_Password.Text = campaign.Secret_Password;
            txtPCCampaig1.Text = campaign.PC_Campaign1;
            txtPC_Campaign2.Text = campaign.PC_Campaign2;
            txtSmart_Campaign1.Text = campaign.Smart_Campaign1;
            txtSmart_Campaign2.Text = campaign.Smart_Campaign2;
            rdolStatus.SelectedValue = campaign.Status.ToString();

            if (campaign.IsPromotionClose == 1)
                chkIsPromotionClose.Checked = true;
            else
                chkIsPromotionClose.Checked = false;

            if (!String.IsNullOrWhiteSpace(campaign.Period_StartTime))
            {
                string[] startTime = campaign.Period_StartTime.Split(':');

                ddlPeriodFromHour.Text = startTime[0];
                ddlPeriodFromMinute.Text = startTime[1];
            }

            if (!String.IsNullOrWhiteSpace(campaign.Period_EndTime))
            {
                string[] endTime = campaign.Period_EndTime.Split(':');
                ddlPeriodToHour.Text = endTime[0];
                ddlPeriodToMinute.Text = endTime[1];
            }

            txtPromotionItem.Text = campaign.Item;

            Session["pe"] = campaign;
        }
        
        public void GetPromotionData()
        {
            promotion = new Promotion_Entity();
            promotion.Campaign_ID = txtCampaignID.Text;
            promotion.CampaignUrl_PC = txtCampaign_url.Text;
            promotion.CampaignUrl_Smart = txtcampaignSmrt_url.Text;
            promotion.Campaign_Guideline = txtCampaign_Guideline.Text;
            promotion.Remark = txtRemark.Text;
            promotion.Production_Detail = txtproduction_detail.Text;
            promotion.PC_Copy_Decoration = txtpc_catchCopy.Text;
            promotion.Product_Decoration = txtproductname_decoration.Text;
            promotion.Smart_Copy_Decoration = txtSmart_Catch_Copy.Text;
            promotion.Mail_Magazine_Event1 = txtemailmagzine1.Text;
            promotion.Mail_Magazine_Event2 = txtemailmagzine2.Text;
            promotion.Mail_Magazine_Event3 = txtemailmagzine3.Text;
            promotion.Subjects = txtsubject.Text;
            promotion.Target_Brand = txtBrand_Name.Text;
            promotion.Instruction_No = txtInstructionNo.Text;
            promotion.Application_Method = txtApplicationmethod.Text;
            promotion.Present_Contents = txtGiftContent.Text;
            promotion.Present_Method = txtGiftway.Text;
            if (ImageList != null && (ImageList.Rows.Count > 0))
            {
                DataTable dtImage = ImageList as DataTable;
                if (dtImage.Rows.Count > 0)
                {
                    promotion.Campaign_Image1 = dtImage.Rows[0]["Item_Image"] + "";
                }
                if (dtImage.Rows.Count > 1)
                {
                    promotion.Campaign_Image2 = dtImage.Rows[1]["Item_Image"] + "";
                }
                if (dtImage.Rows.Count > 2)
                {
                    promotion.Campaign_Image3 = dtImage.Rows[2]["Item_Image"] + "";
                }
                if (dtImage.Rows.Count > 3)
                {
                    promotion.Campaign_Image4 = dtImage.Rows[3]["Item_Image"] + "";
                }
                if (dtImage.Rows.Count > 4)
                {
                    promotion.Campaign_Image5 = dtImage.Rows[4]["Item_Image"] + "";
                }
            }
            promotion.Production_Target = txtProduction_target.Text;
            if (chkGift.Checked == true)
            {
                promotion.IsPresent = true;
            }
            else
            {
                promotion.IsPresent = false;
            }
            if (ddlPublication.SelectedIndex == 0)
            {
                promotion.IsPublic = true;
            }
            else
            {
                promotion.IsPublic = false;
            }
            if (ddlBlackMarket.SelectedIndex == 0)
            {
                promotion.Black_market_Setting = true;
            }
            else
            {
                promotion.Black_market_Setting = false;
            }
            if (!string.IsNullOrWhiteSpace(rdolStatus.SelectedValue))
                promotion.Status = int.Parse(rdolStatus.SelectedValue);
            else
                promotion.Status = 0;
            promotion.Promotion_Name = txtCampaign_Name.Text;
            promotion.Campaign_Guideline = txtCampaign_Guideline.Text;
            promotion.Target_Brand = txtBrand_Name.Text;
            promotion.Period_StartTime = ddlPeriodFromHour.SelectedItem.ToString() + ':' + ddlPeriodFromMinute.SelectedItem.ToString();
            promotion.Period_EndTime = ddlPeriodToHour.SelectedItem.ToString() + ':' + ddlPeriodToMinute.SelectedItem.ToString();
            #region

            #endregion
            if (!String.IsNullOrWhiteSpace(txtPeriod_From.Text))
            {
                promotion.Period_From = Convert.ToDateTime(txtPeriod_From.Text);
            }
            else
                promotion.Period_From = null;
            if (!String.IsNullOrWhiteSpace(txtPeriod_To.Text))
            {
                promotion.Period_To = Convert.ToDateTime(txtPeriod_To.Text);
            }
            else
                promotion.Period_To = null;
            promotion.Secret_ID = txtSecret_ID.Text;
            promotion.Secret_Password = txtSecret_Password.Text;
            promotion.PC_Campaign1 = txtPCCampaig1.Text;
            promotion.PC_Campaign2 = txtPC_Campaign2.Text;
            promotion.Smart_Campaign1 = txtSmart_Campaign1.Text;
            promotion.Smart_Campaign2 = txtSmart_Campaign2.Text;
            if (ddlPriorities.SelectedValue == "0")
            {
                promotion.Priority = "特";
            }
            else if (ddlPriorities.SelectedValue == "1")
            {
                promotion.Priority = "高";
            }
            else if (ddlPriorities.SelectedValue == "2")
            {
                promotion.Priority = "中";
            }
            else
            {
                promotion.Priority = "低";
            }
            if (lstCampaignType.SelectedItem != null)
            {
                if (lstCampaignType.SelectedValue == "0")
                {
                    promotion.Campaign_TypeID = "0";
                }
                else
                    if (lstCampaignType.SelectedValue == "1")
                    {
                        promotion.Campaign_TypeID = "1";
                    }
                    else
                        if (lstCampaignType.SelectedValue == "2")
                        {
                            promotion.Campaign_TypeID = "2";
                        }
                        else
                            if (lstCampaignType.SelectedValue == "3")
                            {
                                promotion.Campaign_TypeID = "3";
                            }
                            else
                                if (lstCampaignType.SelectedValue == "4")
                                {
                                    promotion.Campaign_TypeID = "4";
                                }
                                else
                                    if (lstCampaignType.SelectedValue == "5")
                                    {
                                        promotion.Campaign_TypeID = "5";
                                    }
                                    else
                                        if (lstCampaignType.SelectedValue == "6")
                                        {
                                            promotion.Campaign_TypeID = "6";
                                        }
                                        else
                                            if (lstCampaignType.SelectedValue == "7")
                                            {
                                                promotion.Campaign_TypeID = "7";
                                            }
                                            else
                                                if (lstCampaignType.SelectedValue == "8")
                                                {
                                                    promotion.Campaign_TypeID = "8";
                                                }
            }
            if (chkIsPromotionClose.Checked == true)
                promotion.IsPromotionClose = 1;
            else
                promotion.IsPromotionClose = 0;
            promotion.Subjects = txtsubject.Text;
            for (int i = 0; i < lstTargetShop.Items.Count; i++)
            {
                if (lstTargetShop.Items[i].Selected)
                {
                    lblTargetshop.Text += lstTargetShop.Items[i].Text + "<br/>";
                }
            }
        }

        public void SetPromotionItem()
        {
            promotionItemBL = new Promotion_Item_BL();
            DataTable dt = promotionItemBL.SelectByPromotionID(PID);
            string ItemCode = string.Empty;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ItemCode += dr["Item_Code"].ToString() + ',';
                }
                txtPromotionItem.Text = ItemCode.Remove(ItemCode.Length - 1);
            }
        }

        public void MessageBox(string message)
        {
            if (message == "Saving Successful ! " || message == "Updating Successful ! ")
            {
                Session["CatagoryList"] = null;
                object referrer = ViewState["UrlReferrer"];
                string url = (string)referrer;
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }
            else
            {
                string cleanMessage = message.Replace("'", "\\'");
                string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
                Page page = HttpContext.Current.CurrentHandler as Page;
                if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                {
                    page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                }
            }
        }

        public void SetOption(DataTable dt)
        {
            txtOption_Name1.Text = dt.Rows[0]["Name1"].ToString();
            txtOption_Name2.Text = dt.Rows[0]["Name2"].ToString();
            txtOption_Name3.Text = dt.Rows[0]["Name3"].ToString();
            txtOption_Value1.Text = dt.Rows[0]["Value1"].ToString();
            txtOption_Value2.Text = dt.Rows[0]["Value2"].ToString();
            txtOption_Value3.Text = dt.Rows[0]["Value3"].ToString();
        }

        public void SaveUpdateOption(int promotionID)
        {
            optionBL = new Promotion_ItemOptionBL();
            optionBL.Save(promotionID, GetOption(promotionID));
        }

        public DataTable GetOption(int promotionID)
        {
            DataTable dt = new DataTable();
            DataRow dr1, dr2, dr3 = null;
            dt.Columns.Add(new DataColumn("Promotion_ID", typeof(int)));
            dt.Columns.Add(new DataColumn("Option_Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Option_Value", typeof(string)));
            //create new row
            dr1 = dt.NewRow();
            dr2 = dt.NewRow();
            dr3 = dt.NewRow();
            //add values to each rows
            if (PID == 0)
            {
                dr1["Promotion_ID"] = promotionID;
            }
            else
            {
                dr1["Promotion_ID"] = PID;
            }
            dr1["Option_Name"] = txtOption_Name1.Text;
            dr1["Option_Value"] = txtOption_Value1.Text;
            //add the row to DataTable
            dt.Rows.Add(dr1);
            if (PID == 0)
            {
                dr2["Promotion_ID"] = promotionID;
            }
            else
            {
                dr2["Promotion_ID"] = PID;
            }
            dr2["Option_Name"] = txtOption_Name2.Text;
            dr2["Option_Value"] = txtOption_Value2.Text;
            dt.Rows.Add(dr2);
            if (PID == 0)
            {
                dr3["Promotion_ID"] = promotionID;
            }
            else
            {
                dr3["Promotion_ID"] = PID;
            }
            dr3["Option_Name"] = txtOption_Name3.Text;
            dr3["Option_Value"] = txtOption_Value3.Text;
            dt.Rows.Add(dr3);
            return dt;
        }

        public void SavePromotionAttatchments(int promotionID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Promotion_ID", typeof(int));
            dt.Columns.Add("File_Name", typeof(string));
            dt.Columns.Add("File_Type", typeof(int));
            string fullAttatchmentPath = Server.MapPath(promotionAttatchmentPath);
            if (fuRakuten_Gold1.HasFile)
            {
                fuRakuten_Gold1.SaveAs(fullAttatchmentPath + fuRakuten_Gold1.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Gold1.FileName, 0);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Gold1.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Gold1.Text, 0);
            }
            if (fuRakuten_Gold2.HasFile)
            {
                fuRakuten_Gold2.SaveAs(fullAttatchmentPath + fuRakuten_Gold2.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Gold2.FileName, 0);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Gold2.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Gold2.Text, 0);
            }
            if (fuRakuten_Gold3.HasFile)
            {
                fuRakuten_Gold3.SaveAs(fullAttatchmentPath + fuRakuten_Gold3.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Gold3.FileName, 0);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Gold3.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Gold3.Text, 0);
            }
            if (fuRakuten_Gold4.HasFile)
            {
                fuRakuten_Gold4.SaveAs(fullAttatchmentPath + fuRakuten_Gold4.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Gold4.FileName, 0);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Gold4.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Gold4.Text, 0);
            }
            if (fuRakuten_Cabinet1.HasFile)
            {
                fuRakuten_Cabinet1.SaveAs(fullAttatchmentPath + fuRakuten_Cabinet1.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Cabinet1.FileName, 1);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Cabinet1.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Cabinet1.Text, 1);
            }
            if (fuRakuten_Cabinet2.HasFile)
            {
                fuRakuten_Cabinet2.SaveAs(fullAttatchmentPath + fuRakuten_Cabinet2.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Cabinet2.FileName, 1);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Cabinet2.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Cabinet2.Text, 1);
            }
            if (fuRakuten_Cabinet3.HasFile)
            {
                fuRakuten_Cabinet3.SaveAs(fullAttatchmentPath + fuRakuten_Cabinet3.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Cabinet3.FileName, 1);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Cabinet3.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Cabinet3.Text, 1);
            }
            if (fuRakuten_Cabinet4.HasFile)
            {
                fuRakuten_Cabinet4.SaveAs(fullAttatchmentPath + fuRakuten_Cabinet4.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Cabinet4.FileName, 1);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Cabinet4.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Cabinet4.Text, 1);
            }
            if (fuYahoo1.HasFile)
            {
                fuYahoo1.SaveAs(fullAttatchmentPath + fuYahoo1.FileName);
                dt.Rows.Add(0, promotionID, fuYahoo1.FileName, 2);
            }
            else if (!String.IsNullOrEmpty(lblYahoo1.Text))
            {
                dt.Rows.Add(0, promotionID, lblYahoo1.Text, 2);
            }
            if (fuYahoo2.HasFile)
            {
                fuYahoo2.SaveAs(fullAttatchmentPath + fuYahoo2.FileName);
                dt.Rows.Add(0, promotionID, fuYahoo2.FileName, 2);
            }
            else if (!String.IsNullOrEmpty(lblYahoo2.Text))
            {
                dt.Rows.Add(0, promotionID, lblYahoo2.Text, 2);
            }
            if (fuYahoo3.HasFile)
            {
                fuYahoo3.SaveAs(fullAttatchmentPath + fuYahoo3.FileName);
                dt.Rows.Add(0, promotionID, fuYahoo3.FileName, 2);
            }
            else if (!String.IsNullOrEmpty(lblYahoo3.Text))
            {
                dt.Rows.Add(0, promotionID, lblYahoo3.Text, 2);
            }
            if (fuYahoo4.HasFile)
            {
                fuYahoo4.SaveAs(fullAttatchmentPath + fuYahoo4.FileName);
                dt.Rows.Add(0, promotionID, fuYahoo4.FileName, 2);
            }
            else if (!String.IsNullOrEmpty(lblYahoo4.Text))
            {
                dt.Rows.Add(0, promotionID, lblYahoo4.Text, 2);
            }
            if (fuPonpare1.HasFile)
            {
                fuPonpare1.SaveAs(fullAttatchmentPath + fuPonpare1.FileName);
                dt.Rows.Add(0, promotionID, fuPonpare1.FileName, 3);
            }
            else if (!String.IsNullOrEmpty(lblPonpare1.Text))
            {
                dt.Rows.Add(0, promotionID, lblPonpare1.Text, 3);
            }
            if (fuPonpare2.HasFile)
            {
                fuPonpare2.SaveAs(fullAttatchmentPath + fuPonpare2.FileName);
                dt.Rows.Add(0, promotionID, fuPonpare2.FileName, 3);
            }
            else if (!String.IsNullOrEmpty(lblPonpare2.Text))
            {
                dt.Rows.Add(0, promotionID, lblPonpare2.Text, 3);
            }
            if (fuPonpare3.HasFile)
            {
                fuPonpare3.SaveAs(fullAttatchmentPath + fuPonpare3.FileName);
                dt.Rows.Add(0, promotionID, fuPonpare3.FileName, 3);
            }
            else if (!String.IsNullOrEmpty(lblPonpare3.Text))
            {
                dt.Rows.Add(0, promotionID, lblPonpare3.Text, 3);
            }
            if (fuPonpare4.HasFile)
            {
                fuPonpare4.SaveAs(fullAttatchmentPath + fuPonpare4.FileName);
                dt.Rows.Add(0, promotionID, fuPonpare4.FileName, 3);
            }
            else if (!String.IsNullOrEmpty(lblPonpare4.Text))
            {
                dt.Rows.Add(0, promotionID, lblPonpare4.Text, 3);
            }
            promotionAttatchmentBL = new Promotion_Attatchment_BL();
            promotionAttatchmentBL.InsertUpdate(dt);
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

        public void GetShops()
        {
            head.InnerText = "キャンペーン登録";
            shopBL = new Shop_BL();
            DataTable dt = shopBL.SelectShopAndMall();
            lstTargetShop.DataSource = dt;
            lstTargetShop.DataValueField = "ID";
            lstTargetShop.DataTextField = "Shop_Name";
            lstTargetShop.DataBind();
        }

        public void SetAttatchment()
        {
            promotionAttatchmentBL = new Promotion_Attatchment_BL();
            DataTable dt = promotionAttatchmentBL.SelectByPromotionID(PID);
            Campaign_Entity ce = new Campaign_Entity();
            #region RakutenGold
            DataRow[] rowsRakutenGold = dt.Select("File_Type = 0");
            for (int i = 0; i < rowsRakutenGold.Length; i++)
            {
                if (i == 0)
                {
                    lblRakuten_Gold1.Text = rowsRakutenGold[i]["File_Name"].ToString();
                    fuRakuten_Gold1.Width = 40;
                }

                if (i == 1)
                {
                    lblRakuten_Gold2.Text = rowsRakutenGold[i]["File_Name"].ToString();
                    fuRakuten_Gold2.Width = 40;
                }
                if (i == 2)
                {
                    lblRakuten_Gold3.Text = rowsRakutenGold[i]["File_Name"].ToString();
                    fuRakuten_Gold3.Width = 40;
                }
                if (i == 3)
                {
                    lblRakuten_Gold4.Text = rowsRakutenGold[i]["File_Name"].ToString();
                    fuRakuten_Gold4.Width = 40;
                }
            }
            #endregion
            #region RakutenCabinet
            DataRow[] rowsRakutenCabinet = dt.Select("File_Type = 1");
            for (int i = 0; i < rowsRakutenCabinet.Length; i++)
            {
                if (i == 0)
                {
                    lblRakuten_Cabinet1.Text = rowsRakutenCabinet[i]["File_Name"].ToString();
                    fuRakuten_Cabinet1.Width = 40;
                }
                if (i == 1)
                {
                    lblRakuten_Cabinet2.Text = rowsRakutenCabinet[i]["File_Name"].ToString();
                    fuRakuten_Cabinet2.Width = 40;
                }
                if (i == 2)
                {
                    lblRakuten_Cabinet3.Text = rowsRakutenCabinet[i]["File_Name"].ToString();
                    fuRakuten_Cabinet3.Width = 40;
                }
                if (i == 3)
                {
                    lblRakuten_Cabinet4.Text = rowsRakutenCabinet[i]["File_Name"].ToString();
                    fuRakuten_Cabinet4.Width = 40;
                }
            }
            #endregion
            #region Yahoo
            DataRow[] rowsYahoo = dt.Select("File_Type = 2");
            for (int i = 0; i < rowsYahoo.Length; i++)
            {
                if (i == 0)
                {
                    lblYahoo1.Text = rowsYahoo[i]["File_Name"].ToString();
                    fuYahoo1.Width = 40;
                }
                if (i == 1)
                {
                    lblYahoo2.Text = rowsYahoo[i]["File_Name"].ToString();
                    fuYahoo2.Width = 40;
                }
                if (i == 2)
                {
                    lblYahoo3.Text = rowsYahoo[i]["File_Name"].ToString();
                    fuYahoo3.Width = 40;
                }
                if (i == 3)
                {
                    lblYahoo4.Text = rowsYahoo[i]["File_Name"].ToString();
                    fuYahoo4.Width = 40;
                }
            }
            #endregion
            #region Ponpare
            DataRow[] rowsPonpare = dt.Select("File_Type = 3");
            for (int i = 0; i < rowsPonpare.Length; i++)
            {
                if (i == 0)
                {
                    lblPonpare1.Text = rowsPonpare[i]["File_Name"].ToString();
                    fuPonpare1.Width = 40;
                }
                if (i == 1)
                {
                    lblPonpare2.Text = rowsPonpare[i]["File_Name"].ToString();
                    fuPonpare2.Width = 40;
                }
                if (i == 2)
                {
                    lblPonpare3.Text = rowsPonpare[i]["File_Name"].ToString();
                    fuPonpare3.Width = 40;
                }
                if (i == 3)
                {
                    lblPonpare4.Text = rowsPonpare[i]["File_Name"].ToString();
                    fuPonpare4.Width = 40;
                }
            }
            #endregion
        }

        public void BindPhotoList()
        {
            promotion = new Promotion_Entity();
            promotionBL = new Promotion_BL();
            if (ImageList != null)
            {
                DataTable dt = ImageList as DataTable;
                #region Item Image
                #region Set value
                if ((dt.Rows.Count > 0) && (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image1"].ToString())))
                {
                    Image1.ImageUrl = imagePath + dt.Rows[0]["Item_Image1"] + "";
                    hlImage1.NavigateUrl = imagePath + dt.Rows[0]["Item_Image1"] + "";
                }
                else
                {
                    Image1.ImageUrl = "";
                    hlImage1.NavigateUrl = "";
                }
                if ((dt.Rows.Count > 0) && (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image2"].ToString())))
                {
                    Image2.ImageUrl = imagePath + dt.Rows[0]["Item_Image2"] + "";
                    hlImage2.NavigateUrl = imagePath + dt.Rows[0]["Item_Image2"];
                }
                else
                {
                    Image2.ImageUrl = "";
                    hlImage2.NavigateUrl = "";
                }
                if ((dt.Rows.Count > 0) && (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image3"].ToString())))
                {
                    Image3.ImageUrl = imagePath + dt.Rows[0]["Item_Image3"] + "";
                    hlImage3.NavigateUrl = imagePath + dt.Rows[0]["Item_Image3"];
                }
                else
                {
                    Image3.ImageUrl = "";
                    hlImage3.NavigateUrl = "";
                }
                if ((dt.Rows.Count > 0) && (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image4"].ToString())))
                {
                    Image4.ImageUrl = imagePath + dt.Rows[0]["Item_Image4"] + "";
                    hlImage4.NavigateUrl = imagePath + dt.Rows[0]["Item_Image4"];
                }
                else
                {
                    Image4.ImageUrl = "";
                    hlImage4.NavigateUrl = "";
                }
                if ((dt.Rows.Count > 0) && (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image5"].ToString())))
                {
                    Image5.ImageUrl = imagePath + dt.Rows[0]["Item_Image5"] + "";
                    hlImage5.NavigateUrl = imagePath + dt.Rows[0]["Item_Image5"];
                }
                else
                {
                    Image5.ImageUrl = "";
                    hlImage5.NavigateUrl = "";
                }
                #endregion
            }
            else
            {
                Image1.ImageUrl = "";
                Image2.ImageUrl = "";
                Image3.ImageUrl = "";
                Image4.ImageUrl = "";
                Image5.ImageUrl = "";
                hlImage1.NavigateUrl = "";
                hlImage2.NavigateUrl = "";
                hlImage3.NavigateUrl = "";
                hlImage4.NavigateUrl = "";
                hlImage5.NavigateUrl = "";
            }
                #endregion

            #region
            #endregion
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Campaign_Entity ce = new Campaign_Entity();
            ce.ID = PID;
            ce.Campaign_ID = txtCampaignID.Text;
            ce.Promotion_Name = txtCampaign_Name.Text;
            if (!String.IsNullOrWhiteSpace(lstCampaignType.SelectedValue))
            {
                ce.Campaign_TypeID = lstCampaignType.SelectedValue;
            }
            else
            {
                ce.Campaign_TypeID = "-1";
            }
            ce.CampaignUrl_PC = txtCampaign_url.Text;
            ce.CampaignUrl_Smart = txtcampaignSmrt_url.Text;
            ce.Campaign_Guideline = txtCampaign_Guideline.Text;
            ce.Mail_Magazine_Event1 = txtemailmagzine1.Text;
            ce.Mail_Magazine_Event2 = txtemailmagzine2.Text;
            ce.Mail_Magazine_Event3 = txtemailmagzine3.Text;
            if (!string.IsNullOrWhiteSpace(Request.Form[txtPeriod_From.UniqueID]) || !string.IsNullOrWhiteSpace(Request.Form[txtPeriod_To.UniqueID]))
            {
                if (!string.IsNullOrWhiteSpace(Request.Form[txtPeriod_From.UniqueID]))
                {
                    string startDate = Convert.ToDateTime(Request.Form[txtPeriod_From.UniqueID]).ToShortDateString() + "  " + promotion.Period_StartTime;
                    lblPeriod_from.Text = startDate;
                    ce.Period_From = Convert.ToDateTime(lblPeriod_from.Text);
                }
                else
                    lblPeriod_from.Text = null;
                if (!string.IsNullOrWhiteSpace(Request.Form[txtPeriod_To.UniqueID]))
                {
                    string endDate = Convert.ToDateTime(Request.Form[txtPeriod_To.UniqueID]).ToShortDateString() + "  " + promotion.Period_EndTime;
                    lblperiod_to.Text = endDate;
                    ce.Period_To = Convert.ToDateTime(lblperiod_to.Text);
                }
                else
                    lblperiod_to.Text = null;
            }
            else if (!string.IsNullOrWhiteSpace(Request.Form[txtpfrom.UniqueID]) || !string.IsNullOrWhiteSpace(Request.Form[txtpto.UniqueID]))
            {
                if (!string.IsNullOrWhiteSpace(Request.Form[txtpfrom.UniqueID]))
                {
                    string startDate = Convert.ToDateTime(Request.Form[txtpfrom.UniqueID]).ToShortDateString() + "  " + promotion.Period_StartTime;
                    lblPeriod_from.Text = startDate + "" + ddlPeriodFromHour.SelectedItem.Text + ":" + ddlPeriodFromMinute.SelectedItem.Text;
                    ce.Period_From = Convert.ToDateTime(lblPeriod_from.Text);
                }
                else
                    lblPeriod_from.Text = null;
                if (!string.IsNullOrWhiteSpace(Request.Form[txtpto.UniqueID]))
                {
                    string endDate = Convert.ToDateTime(Request.Form[txtpto.UniqueID]).ToShortDateString() + "  " + promotion.Period_EndTime;
                    lblperiod_to.Text = endDate + "" + ddlPeriodToHour.SelectedItem.Text + ":" + ddlPeriodToMinute.SelectedItem.Text;
                    ce.Period_To = Convert.ToDateTime(lblperiod_to.Text);
                }
                else
                    lblperiod_to.Text = null;
            }
            String start = txtPeriod_From.Text + "  " + ddlPeriodFromHour.SelectedItem.Text + ":" + ddlPeriodFromMinute.SelectedItem.Text;
            DateTime dtfrom = Convert.ToDateTime(start);
            string end = txtPeriod_To.Text + " " + ddlPeriodToHour.SelectedItem.Text + ":" + ddlPeriodToMinute.SelectedItem.Text;
            DateTime dtTo = Convert.ToDateTime(end);
            if (dtTo < dtfrom)
            {
                GlobalUI.MessageBox("開始期間と終了期間に誤りがあります。");
                return;
            }
            ce.Period_StartTime = ddlPeriodFromHour.SelectedItem.Text + ":" + ddlPeriodFromMinute.SelectedItem.Text;
            ce.Period_EndTime = ddlPeriodToHour.SelectedItem.Text + ":" + ddlPeriodToMinute.SelectedItem.Text;
            ce.Status = Convert.ToInt32(rdolStatus.SelectedValue);
            if (chkIsPromotionClose.Checked)
                ce.IsPromotionClose = 1;
            else ce.IsPromotionClose = 0;
            ce.Subjects = txtsubject.Text;
            ce.Target_Brand = txtBrand_Name.Text;
            ce.Instruction_No = txtInstructionNo.Text;
            ce.Item = txtPromotionItem.Text;
            ce.Item_Memo = txtItem_Memo.Text;
            ce.Production_Detail = txtproduction_detail.Text;
            ce.Production_Target = txtProduction_target.Text;
            for (int i = 0; i < lstTargetShop.Items.Count; i++)
            {
                if (lstTargetShop.Items[i].Selected)
                {
                    if (String.IsNullOrWhiteSpace(lblTargetshop.Text))
                        lblTargetshop.Text = lstTargetShop.Items[i].Value;
                    else lblTargetshop.Text = lblTargetshop.Text + "," + lstTargetShop.Items[i].Value;
                }
            }
            ce.Shop = lblTargetshop.Text;
            string shopname = "";
            for (int i = 0; i < lstTargetShop.Items.Count; i++)
            {
                if (lstTargetShop.Items[i].Selected)
                {
                    if (String.IsNullOrWhiteSpace(lblTargetshop.Text))
                        shopname = lstTargetShop.Items[i].Text;
                    else shopname = shopname + "," + lstTargetShop.Items[i].Text;
                }
            }
            ce.Shop_Name = shopname;
            ce.Application_Method = txtApplicationmethod.Text;
            ce.Present_Method = txtGiftway.Text;
            ce.Present_Contents = txtGiftContent.Text;
            ce.Related_Info_Ref = txtRelated_information.Text;
            if (chkGift.Checked == true)
            {
                ce.IsPresent = true;
            }
            else
            { ce.IsPresent = false; }
            ce.Remark = txtRemark.Text;
            ce.Production_Detail = txtproduction_detail.Text;
            if (fuRakuten_Gold1.HasFile)
            {
                fuRakuten_Gold1.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "g1" + fuRakuten_Gold1.FileName));
                ce.Gold_attach1 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "g1" + fuRakuten_Gold1.FileName);
                lblRakuten_Gold1.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "g1" + fuRakuten_Gold1.FileName);
            }
            else { lblRakuten_Gold1.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "g1" + fuRakuten_Gold1.FileName); }

            if (fuRakuten_Gold2.HasFile)
            {
                fuRakuten_Gold2.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "g2" + fuRakuten_Gold2.FileName));
                ce.Gold_attach2 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "g2" + fuRakuten_Gold2.FileName);
                lblRakuten_Gold2.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "g2" + fuRakuten_Gold2.FileName);
            }

            if (fuRakuten_Gold3.HasFile)
            {
                fuRakuten_Gold3.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "g3" + fuRakuten_Gold3.FileName));
                ce.Gold_attach3 = txtCampaignID.Text + "g3" + fuRakuten_Gold3.FileName;
                lblRakuten_Gold3.Text = txtCampaignID.Text + "g3" + fuRakuten_Gold3.FileName; ;
            }

            if (fuRakuten_Gold4.HasFile)
            {
                fuRakuten_Gold4.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "g4" + fuRakuten_Gold4.FileName));
                ce.Gold_attach4 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "g4" + fuRakuten_Gold4.FileName);
                lblRakuten_Gold4.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "g4" + fuRakuten_Gold4.FileName);
            }

            if (fuRakuten_Cabinet1.HasFile)
            {
                fuRakuten_Cabinet1.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "C1" + fuRakuten_Cabinet1.FileName));
                ce.Cabinet_attach1 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "C1" + fuRakuten_Cabinet1.FileName);
                lblRakuten_Cabinet1.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "C1" + fuRakuten_Cabinet1.FileName);
            }

            if (fuRakuten_Cabinet2.HasFile)
            {
                fuRakuten_Cabinet2.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "C2" + fuRakuten_Cabinet2.FileName));
                ce.Cabinet_attach2 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "C2" + fuRakuten_Cabinet2.FileName);
                lblRakuten_Cabinet2.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "C2" + fuRakuten_Cabinet2.FileName);
            }

            if (fuRakuten_Cabinet3.HasFile)
            {
                fuRakuten_Cabinet3.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "C3" + fuRakuten_Cabinet3.FileName));
                ce.Cabinet_attach3 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "C3" + fuRakuten_Cabinet3.FileName);
                lblRakuten_Cabinet3.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "C3" + fuRakuten_Cabinet3.FileName);
            }

            if (fuRakuten_Cabinet4.HasFile)
            {
                fuRakuten_Cabinet4.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "C4" + fuRakuten_Cabinet4.FileName));
                ce.Cabinet_attach4 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "C4" + fuRakuten_Cabinet4.FileName);
                lblRakuten_Cabinet4.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "C4" + fuRakuten_Cabinet4.FileName);
            }

            if (fuYahoo1.HasFile)
            {
                fuYahoo1.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "Y1" + fuYahoo1.FileName));
                ce.Geocities_attach1 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "Y1" + fuYahoo1.FileName);
                lblYahoo1.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "Y1" + fuYahoo1.FileName);
            }

            if (fuYahoo2.HasFile)
            {
                fuYahoo2.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "Y2" + fuYahoo2.FileName));
                ce.Geocities_attach2 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "Y2" + fuYahoo2.FileName);
                lblYahoo2.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "Y2" + fuYahoo2.FileName);

            }

            if (fuYahoo3.HasFile)
            {
                fuYahoo3.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "Y3" + fuYahoo3.FileName));
                ce.Geocities_attach3 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "Y3" + fuYahoo3.FileName);
                lblYahoo3.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "Y3" + fuYahoo3.FileName);
            }

            if (fuYahoo4.HasFile)
            {
                fuYahoo4.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "Y4" + fuYahoo4.FileName));
                ce.Geocities_attach4 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "Y4" + fuYahoo4.FileName);
                lblYahoo4.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "Y4" + fuYahoo4.FileName);
            }

            if (fuPonpare1.HasFile)
            {
                fuPonpare1.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "P1" + fuPonpare1.FileName));
                ce.Ponpare_attach1 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "P1" + fuPonpare1.FileName);
                lblPonpare1.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "P1" + fuPonpare1.FileName);
            }

            if (fuPonpare2.HasFile)
            {
                fuPonpare2.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "P2" + fuPonpare2.FileName));
                ce.Ponpare_attach2 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "P2" + fuPonpare2.FileName);
                lblPonpare2.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "P2" + fuPonpare2.FileName);
            }

            if (fuPonpare3.HasFile)
            {
                fuPonpare3.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "P3" + fuPonpare3.FileName));
                ce.Ponpare_attach3 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "P3" + fuPonpare3.FileName);
                lblPonpare3.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "P3" + fuPonpare3.FileName);
            }
            if (fuPonpare4.HasFile)
            {
                fuPonpare4.SaveAs(Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "P4" + fuPonpare4.FileName));
                ce.Ponpare_attach4 = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "P4" + fuPonpare4.FileName);
                lblPonpare4.Text = Server.MapPath(promotionAttatchmentPath + txtCampaignID.Text + "P4" + fuPonpare4.FileName);
            }

            ce.PC_Campaign1 = txtPCCampaig1.Text;
            ce.PC_Campaign2 = txtPC_Campaign2.Text;
            ce.Smart_Campaign1 = txtSmart_Campaign1.Text;
            ce.Smart_Campaign2 = txtSmart_Campaign2.Text;
            ce.Product_Decoration = txtproductname_decoration.Text;
            ce.PC_Copy_Decoration = txtpc_catchCopy.Text;
            ce.Smart_Copy_Decoration = txtSmart_Catch_Copy.Text;
            if (ddlPublication.SelectedValue == "1")
            {
                ce.IsPublic = true;
            }
            else
            {
                ce.IsPublic = false;
            }

            if (ddlBlackMarket.SelectedIndex == 0)
            {
                ce.Black_market_Setting = false;
            }
            else
            {
                ce.Black_market_Setting = true;
            }

            if (ddlPriorities.SelectedValue == "0")
            {
                ce.Priority = "特";
            }
            else if (ddlPriorities.SelectedValue == "1")
            {
                ce.Priority = "高";
            }
            else if (ddlPriorities.SelectedValue == "2")
            {
                ce.Priority = "中";
            }
            else
            {
                ce.Priority = "低";
            }

            ce.Secret_ID = txtSecret_ID.Text;
            ce.Secret_Password = txtSecret_Password.Text;
            if (rdolStatus.SelectedValue == "0")
            {
                ce.Status = 0;
            }
            else if (rdolStatus.SelectedValue == "1")
            {
                ce.Status = 1;
            }
            if (rdolStatus.SelectedValue == "2")
            {
                ce.Status = 2;
            }
            else if (rdolStatus.SelectedValue == "3")
            {
                ce.Status = 3;
            }
            if (chkIsPromotionClose.Checked)
            {
                ce.IsPromotionClose = 1;
            }
            else
            {
                ce.IsPromotionClose = 0;
            }

            if (!String.IsNullOrWhiteSpace(txtOption_Name1.Text))
            {
                if (txtOption_Name1.Text == txtOption_Name2.Text)
                {
                    goto a;
                }
                else if (txtOption_Name1.Text == txtOption_Name3.Text)
                {
                    goto a;
                }
            }

            if (!String.IsNullOrWhiteSpace(txtOption_Name2.Text))
            {
                if (txtOption_Name2.Text == txtOption_Name3.Text)
                {
                    goto a;
                }
            }

            ce.OptionName1 = txtOption_Name1.Text;
            ce.OptionName2 = txtOption_Name2.Text;
            ce.OptionName3 = txtOption_Name3.Text;
            ce.OptionValue1 = txtOption_Value1.Text;
            ce.OptionValue2 = txtOption_Value2.Text;
            ce.OptionValue3 = txtOption_Value3.Text;

            if (PID == 0)
            {
                Session["Campaign_entity"] = ce;
                Response.Redirect("Campaign_promotion_Confirm.aspx");
            }
            else if (PID != 0)
            {
                Session["Campaign_entity"] = ce;
                Response.Redirect("Campaign_promotion_Confirm.aspx?ID=" + PID);
            }

        a:
            MessageBox("Option Name is Duplicate");
        }
    }
}



        


       








   






    

