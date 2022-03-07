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

namespace ORS_RCM.WebForms.Promotion
{
    public partial class Promotion : System.Web.UI.Page
    {
        Promotion_Entity promotion;
        Promotion_BL promotionBL;
        Promotion_Item_BL promotionItemBL;
        Promotion_ItemOptionBL optionBL;
        Promotion_Attatchment_BL promotionAttatchmentBL;
        Shop_BL shopBL;
        Promotion_Shop_BL promotionShopBL;

        string promotionAttatchmentPath = ConfigurationManager.AppSettings["PromotionAttatchment"].ToString();

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
            //divOption.Visible = false;

            if (Option != null && Option.Rows.Count > 0)
            {
                DataTable dt = Option as DataTable;
                SetOption(dt);
            }
        }
        string ControlID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            promotion = new Promotion_Entity();
            promotionBL = new Promotion_BL();
            optionBL = new Promotion_ItemOptionBL();
            
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
                if (PID != 0)
                {
                    if (Status != "")
                    {
                        btnSave.Text = "Save";
                        head.InnerText = "プロモーション登録";
                     
                    }
                    else
                    {
                        btnSave.Text = "Update";
                        head.InnerText = "プロモーション編集";
                        Readonlydata();
                    
                    }
                    //promotion = promotionBL.SelectByID(PID);
                    SetPromotionData(promotion);
                    SetPromotionItem();
                    //SetOption(optionBL.SelectByPID(PID));
                    GetOptionSelectByPromotionID();
                    SetShops();
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

                MaintainPeriodStates();

                string name = getPostBackControlName();

                if (name == null)
                {

                    test.Attributes["class"] = "setListBox iconSet iconCheck";
                    head.InnerText = "プロモーション編集確認";
                    btnpopup.Visible = false;
                    Button1.Visible = true;

                    Confirm();


                }


            }
        }

        protected void Confirm() 
        {
           
            if (btnSave.Text == "確 認")
                Button1.Text = "登 録";
            else
                Button1.Text = "反 映";
            promotion = new Promotion_Entity();
            promotion.ID = PID;
            lbltxtCam_Guideline.Visible = true;
            lblProName.Visible = true;
            lblbrand_name.Visible = true;
            lblPeriod_from.Visible = true;
            lblperiod_to.Visible = true;
            lblCampaingtype.Visible = true;
            lblRakuten_ID.Visible = true;
            lblYahoo_ID.Visible = true;
            lblponpare.Visible = true;
            lblSecretID.Visible = true;
            lblSecPassword.Visible = true;
            lblproitem.Visible = true;
            lblprodecoration.Visible = true;
            lblcpydec.Visible = true;
            lblOp1.Visible = true;
            lblOp2.Visible = true;
            lblOp3.Visible = true;
            lblOpVal1.Visible = true;
            lblOpVal2.Visible = true;
            lblOpVal3.Visible = true;
            lblpro_desX.Visible = true;
            lblpro_descY.Visible = true;
            lblsale_descX.Visible = true;
            lblsale_descY.Visible = true;

            lblRakuten_Gold1.Visible = true;
            lblRakuten_Gold2.Visible = true;
            lblRakuten_Gold3.Visible = true;
            
            Label1.Visible = false;
            txtPeriod_From.Visible = false;
            txtPeriod_To.Visible = false;
            fuRakuten_Gold1.Visible = false;
            fuRakuten_Gold2.Visible = false;
            fuRakuten_Gold3.Visible = false;
            fuRakuten_Gold4.Visible = false;
            fuRakuten_Cabinet1.Visible = false;
            fuRakuten_Cabinet2.Visible = false;
            fuRakuten_Cabinet3.Visible = false;
            fuRakuten_Cabinet4.Visible = false;

            fuPonpare1.Visible = false;
            fuPonpare2.Visible = false;
            fuPonpare3.Visible = false;
            fuPonpare4.Visible = false;

            fuYahoo1.Visible = false;
            fuYahoo2.Visible = false;
            fuYahoo3.Visible = false;
            fuYahoo4.Visible = false;
            Label2.Visible = false;

            lblpriority.Visible = true;
            lblpriority.Text = txtPriority.Text;
            txtPriority.Visible = false;

            lblOp1.Text = txtOption_Name1.Text;
            txtOption_Name1.Visible = false;

            lblOp2.Text = txtOption_Name2.Text;
            txtOption_Name2.Visible = false;

            lblOp3.Text = txtOption_Name3.Text;
            txtOption_Name3.Visible = false;

            lblOpVal1.Text = txtOption_Value1.Text;
            txtOption_Value1.Visible = false;

            lblOpVal2.Text = txtOption_Value2.Text;
            txtOption_Value2.Visible = false;

            lblOpVal3.Text = txtOption_Value3.Text;
            txtOption_Value3.Visible = false;

         
            txtPromotion_Name.Visible = false;
            lbltxtCam_Guideline.Text = txtCampaign_Guideline.Text;
            txtCampaign_Guideline.Visible = false;
           lblbrand_name.Text= txtBrand_Name.Text;
           txtBrand_Name.Visible = false;

          
           ddlPeriodFromHour.Visible = false; ddlPeriodFromMinute.Visible = false;

         
           ddlPeriodToHour.Visible = false; ddlPeriodToMinute.Visible = false;
           lblProName.Text = txtPromotion_Name.Text;
           txtpto.Visible = false; txtpfrom.Visible = false;
           if (!string.IsNullOrWhiteSpace(Request.Form[txtPeriod_From.UniqueID]) || !string.IsNullOrWhiteSpace(Request.Form[txtPeriod_To.UniqueID]))
           {
               if (!string.IsNullOrWhiteSpace(Request.Form[txtPeriod_From.UniqueID]))
               {
                   string startDate = Convert.ToDateTime(Request.Form[txtPeriod_From.UniqueID]).ToShortDateString() + "  " + promotion.Period_StartTime;
                   // DateTime periodFromDate = DateTime.Parse(startDate);
                   lblPeriod_from.Text = startDate + "" + ddlPeriodFromHour.SelectedItem.Text + ":" + ddlPeriodFromMinute.SelectedItem.Text;
               }
               else
                   lblPeriod_from.Text = null;
               if (!string.IsNullOrWhiteSpace(Request.Form[txtPeriod_To.UniqueID]))
               {
                   string endDate = Convert.ToDateTime(Request.Form[txtPeriod_To.UniqueID]).ToShortDateString() + "  " + promotion.Period_EndTime;
                   // DateTime periodToDate = DateTime.Parse(endDate);
                   lblperiod_to.Text = endDate + "" + ddlPeriodToHour.SelectedItem.Text + ":" + ddlPeriodToMinute.SelectedItem.Text;
               }
               else
                   lblperiod_to.Text = null;
           }
           else if (!string.IsNullOrWhiteSpace(Request.Form[txtpfrom.UniqueID]) || !string.IsNullOrWhiteSpace(Request.Form[txtpto.UniqueID]))
           {
               if (!string.IsNullOrWhiteSpace(Request.Form[txtpfrom.UniqueID]))
               {
                   string startDate = Convert.ToDateTime(Request.Form[txtpfrom.UniqueID]).ToShortDateString() + "  " + promotion.Period_StartTime;
                   // DateTime periodFromDate = DateTime.Parse(startDate);
                   lblPeriod_from.Text = startDate + "" + ddlPeriodFromHour.SelectedItem.Text + ":" + ddlPeriodFromMinute.SelectedItem.Text;
               }
               else
                   lblPeriod_from.Text = null;
               if (!string.IsNullOrWhiteSpace(Request.Form[txtpto.UniqueID]))
               {
                   string endDate = Convert.ToDateTime(Request.Form[txtpto.UniqueID]).ToShortDateString() + "  " + promotion.Period_EndTime;
                   // DateTime periodToDate = DateTime.Parse(endDate);
                   lblperiod_to.Text = endDate + "" + ddlPeriodToHour.SelectedItem.Text + ":" + ddlPeriodToMinute.SelectedItem.Text;
               }
               else
                   lblperiod_to.Text = null;
           }

            string campaingType = string.Empty;
            for (int i = 0; i < chkCampaingType.Items.Count; i++)
            {
                if (chkCampaingType.Items[i].Selected)
                {
                    campaingType += chkCampaingType.Items[i].Value + ',';
                }
           
            }
            if (campaingType.Length > 0)
            {
                promotion.Campaign_TypeID = campaingType.Remove(campaingType.Length - 1);
                
             
            }

            if (!string.IsNullOrWhiteSpace(ddlRakuten_MagnificationID.SelectedValue))
                lblRakuten_ID.Text = ddlRakuten_MagnificationID.SelectedItem.Text;
            else
                lblRakuten_ID.Text = null;
            ddlRakuten_MagnificationID.Visible = false;

            if (!string.IsNullOrWhiteSpace(ddlYahoo_MagnificationID.SelectedValue))
                lblYahoo_ID.Text = ddlYahoo_MagnificationID.SelectedItem.Text;
            else
                lblYahoo_ID.Text = null;
            ddlYahoo_MagnificationID.Visible = false;

            if (!string.IsNullOrWhiteSpace(ddlPonpare_MagnificationID.SelectedValue))
                lblponpare.Text = ddlPonpare_MagnificationID.SelectedItem.Text;
            else
                lblponpare.Text = null;
            ddlPonpare_MagnificationID.Visible = false;

            lblSecretID.Text= txtSecret_ID.Text;
            txtSecret_ID.Visible = false;

            lblSecPassword.Text = txtSecret_Password.Text;
            txtSecret_Password.Visible = false;


            lblTargetshop.Visible = true;
      
            for (int i = 0; i < lstTargetShop.Items.Count; i++)
            {
                if (lstTargetShop.Items[i].Selected)
                {
                    lblTargetshop.Text +=lstTargetShop.Items[i].Text+"<br/>";
                }
            }
            lstTargetShop.Visible = false;

            lblproitem.Text = txtPromotionItem.Text;
            txtPromotionItem.Visible = false;
            All_Items.Visible = false;
            Designated_goods.Visible = false;


            lblpro_desX.Text = txtProduct_DescriptionX.Text;
            txtProduct_DescriptionX.Visible = false;


            lblpro_descY.Text = txtProduct_DescriptionY.Text;
            txtProduct_DescriptionY.Visible = false;

            lblsale_descX.Text= txtSale_DescriptionX.Text;
            txtSale_DescriptionX.Visible = false;

           lblsale_descY.Text = txtSale_DescriptionY.Text;
           txtSale_DescriptionY.Visible = false;

            promotion.Priority = txtPriority.Text;
            rdolStatus.Visible = false;
            if (!string.IsNullOrWhiteSpace(rdolStatus.SelectedValue))
                lblstatus.Text= rdolStatus.SelectedItem.Text;
                
            else
             lblstatus.Text = null;
            lblproclose.Visible = true;
            if (chkIsPromotionClose.Checked == true)
               lblproclose.Text =chkIsPromotionClose.Text;
            else
                lblproclose.Text = null;
            chkIsPromotionClose.Visible = false;

            lblprodecoration.Visible = true;
            lblprodecoration.Text= txtproductDecoration.Text;
            txtproductDecoration.Visible = false;

            lblcpydec .Text= txtcpyDecoration.Text;
            txtcpyDecoration.Visible = false;
            if (fuRakuten_Gold1.HasFile)
            {
                lblRakuten_Gold1.Text = fuRakuten_Gold1.FileName;
            }
            else { lblRakuten_Gold1.Text = null; }
            if (fuRakuten_Gold2.HasFile) { lblRakuten_Gold2.Text = fuRakuten_Gold2.FileName; } 
            if (fuRakuten_Gold3.HasFile) { lblRakuten_Gold2.Text = fuRakuten_Gold3.FileName; }
            if (fuRakuten_Gold4.HasFile) { lblRakuten_Gold2.Text = fuRakuten_Gold4.FileName; }
            if (fuRakuten_Cabinet1.HasFile) { lblRakuten_Cabinet1.Text = fuRakuten_Cabinet1.FileName; }
            if (fuRakuten_Cabinet2.HasFile) { lblRakuten_Cabinet2.Text = fuRakuten_Cabinet2.FileName; }
            if (fuRakuten_Cabinet3.HasFile) { lblRakuten_Cabinet3.Text = fuRakuten_Cabinet3.FileName; }
            if (fuRakuten_Cabinet4.HasFile) { lblRakuten_Cabinet4.Text = fuRakuten_Cabinet4.FileName; }
            if (fuYahoo1.HasFile) { lblYahoo1.Text = fuYahoo1.FileName; }
            if (fuYahoo2.HasFile) { lblYahoo2.Text = fuYahoo2.FileName; }
            if (fuYahoo3.HasFile) { lblYahoo3.Text = fuYahoo3.FileName; }
            if (fuYahoo4.HasFile) { lblYahoo4.Text = fuYahoo4.FileName; }
            if (fuPonpare1.HasFile) { lblPonpare1.Text = fuPonpare1.FileName; }
            if (fuPonpare2.HasFile) { lblPonpare2.Text = fuPonpare2.FileName; }
            if (fuPonpare3.HasFile) { lblPonpare3.Text = fuPonpare3.FileName; }
            if (fuPonpare4.HasFile) { lblPonpare4.Text = fuPonpare4.FileName; }

            chkCampaingType.Visible = false;
            for (int i = 0; i < chkCampaingType.Items.Count; i++)
            {
                if (chkCampaingType.Items[i].Selected)
                {
                    campaingType += chkCampaingType.Items[i].Value + ',';
                    switch (chkCampaingType.Items[i].Value.ToString()) 
                    {
                        case "0":
                            { lblCampaingtype.Visible = true; lblCampaingtype.Text = chkCampaingType.Items[i].Text; break; }
                        case "1":
                            { lblCampaingtype1.Visible = true; lblCampaingtype1.Text = chkCampaingType.Items[i].Text; break; }
                        case "2":
                            { lblCampaingtype2.Visible = true; lblCampaingtype2.Text = chkCampaingType.Items[i].Text; break; }
                        case "3":
                            { lblCampaingtype3.Visible = true; lblCampaingtype3.Text = chkCampaingType.Items[i].Text; break; }
                        case "4":
                            { lblCampaingtype4.Visible = true; lblCampaingtype4.Text = chkCampaingType.Items[i].Text; break; }
                        case "5":
                            { lblCampaingtype5.Visible = true; lblCampaingtype5.Text = chkCampaingType.Items[i].Text; break; }
                        case "6":
                            { lblCampaingtype6.Visible = true; lblCampaingtype6.Text = chkCampaingType.Items[i].Text; break; }
                        case "7":
                            { lblCampaingtype7.Visible = true; lblCampaingtype7.Text = chkCampaingType.Items[i].Text; break; }
                        case "8":
                            { lblCampaingtype8.Visible = true; lblCampaingtype8.Text = chkCampaingType.Items[i].Text; break; }
                    
                    }
                }
            }

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
          
      

        protected void Save() 
        {
            promotionBL = new Promotion_BL();
            promotionItemBL = new Promotion_Item_BL();
            optionBL = new Promotion_ItemOptionBL();

            if (PID == 0 || Status != "")
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    GetPromotionData();
                   // int promotionID = promotionBL.SaveUpdate(promotion, "Save");
            //        string[] arr = txtPromotionItem.Text.Split(',');
            //        arr = arr.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //        //promotionItemBL.Insert_Promotion_Item(promotionID, arr);
            //        //if (promotionID > 0)
            //        //{
            //        //    SaveUpdateOption(promotionID);
            //        //    SavePromotionAttatchments(promotionID);
            //        //    SaveShops(promotionID);
            //            MessageBox("Saving Successful ! ");
            //        }
            //        tran.Complete();
            //    }
            //}
            //else
            //{
            //    using (TransactionScope tran = new TransactionScope())
            //    {
            //        GetPromotionData();
            //        int promotionID = promotionBL.SaveUpdate(promotion, "Update");
            //        string[] arr = txtPromotionItem.Text.Split(',');
            //        arr = arr.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //        promotionItemBL.Insert_Promotion_Item(promotionID, arr);
            //        SaveUpdateOption(promotionID);
            //        SavePromotionAttatchments(promotionID);
            //        SaveShops(promotionID);
            //        MessageBox("Updating Successful ! ");
            //        tran.Complete();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Button1.Text.Equals("確認画面へ"))
            {
                if (btnSave.Text == "Save")
                    Button1.Text = "登 録";
                else
                    Button1.Text = "反 映";
             
                }
            else if (Button1.Text.Equals("登 録") || Button1.Text.Equals("反 映"))
            {
                if (!String.IsNullOrWhiteSpace(lblProName.Text.Trim()))
                { Save(); }
                else
                { }

            }
        }
              
        

        public void GetPromotionData()
        {
            promotion = new Promotion_Entity();
            promotion.ID = PID;
            promotion.Promotion_Name = txtPromotion_Name.Text;
            promotion.Campaign_Guideline = txtCampaign_Guideline.Text;
            promotion.Target_Brand= txtBrand_Name.Text;

            promotion.Period_StartTime = ddlPeriodFromHour.SelectedItem.ToString() + ':' + ddlPeriodFromMinute.SelectedItem.ToString();
            promotion.Period_EndTime = ddlPeriodToHour.SelectedItem.ToString() + ':' + ddlPeriodToMinute.SelectedItem.ToString();
            #region
            //if (!string.IsNullOrWhiteSpace(Request.Form[txtPeriod_From.UniqueID]))
            //{
            //    string startDate = Convert.ToDateTime(Request.Form[txtPeriod_From.UniqueID]).ToShortDateString() + "  " + promotion.Period_StartTime;
            //    DateTime periodFromDate = DateTime.Parse(startDate);
            //    promotion.Period_From = periodFromDate;
            //}              
            //else
            //    promotion.Period_From = null;
            //if (!string.IsNullOrWhiteSpace(Request.Form[txtPeriod_To.UniqueID]))
            //{
            //    string endDate = Convert.ToDateTime(Request.Form[txtPeriod_To.UniqueID]).ToShortDateString() + "  " + promotion.Period_EndTime;
            //    DateTime periodToDate = DateTime.Parse(endDate);
            //    promotion.Period_To = periodToDate;
            //}               
            //else
            //    promotion.Period_To = null;
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

            string campaingType = string.Empty;
            for (int i = 0; i < chkCampaingType.Items.Count; i++)
            {
                if (chkCampaingType.Items[i].Selected)
                {
                    campaingType += chkCampaingType.Items[i].Value + ',';
                }
            }
            if (campaingType.Length > 0)
            {
                promotion.Campaign_TypeID = campaingType.Remove(campaingType.Length - 1);
            }
            
            if (!string.IsNullOrWhiteSpace(ddlRakuten_MagnificationID.SelectedValue))
                promotion.Rakuten_MagnificationID = int.Parse(ddlRakuten_MagnificationID.SelectedValue);
            else
                promotion.Rakuten_MagnificationID = 0;

            if (!string.IsNullOrWhiteSpace(ddlYahoo_MagnificationID.SelectedValue))
                promotion.Yahoo_MagnificationID = int.Parse(ddlYahoo_MagnificationID.SelectedValue);
            else
                promotion.Yahoo_MagnificationID = 0;

            if (!string.IsNullOrWhiteSpace(ddlPonpare_MagnificationID.SelectedValue))
                promotion.Ponpare_MagnificationID = int.Parse(ddlPonpare_MagnificationID.SelectedValue);
            else
                promotion.Ponpare_MagnificationID = 0;
            promotion.Secret_ID = txtSecret_ID.Text;
            promotion.Secret_Password = txtSecret_Password.Text;
            promotion.PC_Campaign1 = txtProduct_DescriptionX.Text;
            promotion.PC_Campaign2 = txtProduct_DescriptionY.Text;
            promotion.Smart_Campaign1 = txtSale_DescriptionX.Text;
            promotion.Smart_Campaign2 = txtSale_DescriptionY.Text;
            promotion.Priority = txtPriority.Text;
            if (!string.IsNullOrWhiteSpace(rdolStatus.SelectedValue))
                promotion.Status = int.Parse(rdolStatus.SelectedValue);
            else
                promotion.Status = 0;
            if (chkIsPromotionClose.Checked == true)
                promotion.IsPromotionClose = 1;
            else
                promotion.IsPromotionClose = 0;
            promotion.Product_Decoration = txtproductDecoration.Text;
            promotion.Copy_Decoration = txtcpyDecoration.Text;
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

        public void SetPromotionData(Promotion_Entity pe)
        {
            txtPromotion_Name.Text = pe.Promotion_Name;
            txtCampaign_Guideline.Text = pe.Campaign_Guideline;
            txtBrand_Name.Text = pe.Target_Brand;
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
            //rdolCampaign_TypeID.SelectedValue = pe.Campaign_TypeID.ToString();
            string[] arr = pe.Campaign_TypeID.Split(',');
            for (int i = 0; i < arr.Count(); i++)
            {
                foreach (ListItem item in chkCampaingType.Items)
	            {
		            if (arr.Contains(item.Value.ToString()))
	                {
                        item.Selected = true;
	                }
	            }
            }
            ddlRakuten_MagnificationID.SelectedValue = pe.Rakuten_MagnificationID.ToString();
            ddlYahoo_MagnificationID.SelectedValue = pe.Yahoo_MagnificationID.ToString();
            ddlPonpare_MagnificationID.SelectedValue = pe.Ponpare_MagnificationID.ToString();
            txtSecret_ID.Text = pe.Secret_ID.ToString();
            txtSecret_Password.Text = pe.Secret_Password;
            txtProduct_DescriptionX.Text = pe.PC_Campaign1;
            txtProduct_DescriptionY.Text = pe.PC_Campaign2;
            txtSale_DescriptionX.Text = pe.Smart_Campaign1;
            txtSale_DescriptionY.Text = pe.Smart_Campaign2;
            rdolStatus.SelectedValue = pe.Status.ToString();
            if (pe.IsPromotionClose == 1)
                chkIsPromotionClose.Checked = true;
            else
                chkIsPromotionClose.Checked = false;
            txtproductDecoration.Text = pe.Product_Decoration;
            txtcpyDecoration.Text = pe.Copy_Decoration;
            string[] startTime = pe.Period_StartTime.Split(':');
            ddlPeriodFromHour.Text = startTime[0];
            ddlPeriodFromMinute.Text = startTime[1];
            string[] endTime = pe.Period_EndTime.Split(':');
            ddlPeriodToHour.Text = endTime[0];
            ddlPeriodToMinute.Text = endTime[1];
        }

        public void Readonlydata() 
        {
            txtPromotion_Name.ReadOnly = true;
            txtCampaign_Guideline.ReadOnly = true;
            txtBrand_Name.ReadOnly = true;
            txtpfrom.Visible = true;
            txtpfrom.Text = txtPeriod_From.Text;
            txtpto.Visible = true;
            txtpto.Text = txtPeriod_To.Text;
            txtPeriod_From.Visible = false;
            txtPeriod_To.Visible = false;
            chkCampaingType.Enabled =false;
            ddlRakuten_MagnificationID.Enabled = false;
            ddlYahoo_MagnificationID.Enabled = false;
            ddlPonpare_MagnificationID.Enabled = false;
            txtSecret_ID.ReadOnly = true;
            txtSecret_Password.ReadOnly = true;
           
            lstTargetShop.Enabled =false;
            All_Items.Enabled = false;
            Designated_goods.Enabled = false;
            txtPromotionItem.ReadOnly = true;
            txtproductDecoration.ReadOnly = true;
            txtcpyDecoration.ReadOnly = true;
            txtOption_Name1.ReadOnly = true;
            txtOption_Name2.ReadOnly = true;
            txtOption_Name3.ReadOnly = true;
            txtOption_Value1.ReadOnly = true;
            txtOption_Value2.ReadOnly = true;
            txtOption_Value3.ReadOnly = true;
            txtProduct_DescriptionX.ReadOnly = true;
            txtProduct_DescriptionY.ReadOnly = true;
            txtSale_DescriptionX.ReadOnly = true;
            txtSale_DescriptionY.ReadOnly = true;
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
            txtPriority.ReadOnly = true;
            rdolStatus.Enabled = false;
            chkIsPromotionClose.Enabled = false;
            ddlPeriodFromHour.Enabled = false;
            ddlPeriodFromMinute.Enabled = false;
            ddlPeriodToHour.Enabled = false;
            ddlPeriodToMinute.Enabled = false;

        }
        public DataTable GetOption(int promotionID)
        {
            DataTable dt = new DataTable();
            DataRow dr1, dr2, dr3 = null;
            //define the columns
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

        public void SavePromotionAttatchments(int promotionID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID",typeof(int));
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
            dt.Columns.Add("PromotionID",typeof(int));
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
            head.InnerText = "プロモーション登録";
            shopBL = new Shop_BL();
            DataTable dt = shopBL.SelectShopAndMall();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("ID",typeof(int));
            //dt.Columns.Add("Name",typeof(string));
            //foreach (DataRow dr in dtTmp.Rows)
            //{
            //    dt.Rows.Add(dr["ID"].ToString(), dr["Shop_Name"].ToString() + dr["Mall_Name"].ToString());
            //}
            lstTargetShop.DataSource = dt;
            lstTargetShop.DataValueField = "ID";
            lstTargetShop.DataTextField = "Shop_Name";
            lstTargetShop.DataBind();
         
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
                    else { if (btnSave.Text == "Update") { if (item.Selected.Equals(false)) { item.Enabled = false; } } }
                }    
            }
            if (btnSave.Text == "Update") { lstTargetShop.Enabled = false;  }
        }

        public void SetAttatchment()
        {
            promotionAttatchmentBL = new Promotion_Attatchment_BL();
            DataTable dt = promotionAttatchmentBL.SelectByPromotionID(PID);
            #region RakutenGold
            DataRow[] rowsRakutenGold = dt.Select("File_Type = 0");
            for (int i = 0; i < rowsRakutenGold.Length; i++)
            {
                if (i == 0)
                {
                    lblRakuten_Gold1.Text = rowsRakutenGold[i]["File_Name"].ToString();
                }
                if (i == 1)
                {
                    lblRakuten_Gold2.Text = rowsRakutenGold[i]["File_Name"].ToString();
                }
                if (i == 2)
                {
                    lblRakuten_Gold3.Text = rowsRakutenGold[i]["File_Name"].ToString();
                }
                if (i == 3)
                {
                    lblRakuten_Gold4.Text = rowsRakutenGold[i]["File_Name"].ToString();
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
                }
                if (i == 1)
                {
                    lblRakuten_Cabinet2.Text = rowsRakutenCabinet[i]["File_Name"].ToString();
                }
                if (i == 2)
                {
                    lblRakuten_Cabinet3.Text = rowsRakutenCabinet[i]["File_Name"].ToString();
                }
                if (i == 3)
                {
                    lblRakuten_Cabinet4.Text = rowsRakutenCabinet[i]["File_Name"].ToString();
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
                }
                if (i == 1)
                {
                    lblYahoo2.Text = rowsYahoo[i]["File_Name"].ToString();
                }
                if (i == 2)
                {
                    lblYahoo3.Text = rowsYahoo[i]["File_Name"].ToString();
                }
                if (i == 3)
                {
                    lblYahoo4.Text = rowsYahoo[i]["File_Name"].ToString();
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
                }
                if (i == 1)
                {
                    lblPonpare2.Text = rowsPonpare[i]["File_Name"].ToString();
                }
                if (i == 2)
                {
                    lblPonpare3.Text = rowsPonpare[i]["File_Name"].ToString();
                }
                if (i == 3)
                {
                    lblPonpare4.Text = rowsPonpare[i]["File_Name"].ToString();
                }
            }
            #endregion
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

            /*
        
            //string connString = "Data Source=dataserver;Initial Catalog=ORS_RCM;Persist Security Info=True;User ID=sa;Password=12345;Connection Timeout=60000";
            private int Insert_Promotion()
            {
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("SP_Promotion_SaveUpdate", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID",0);
                cmd.Parameters.AddWithValue("@PromotionName", txtPromotion_Name.Text);
                cmd.Parameters.AddWithValue("@Option", "Save");
                cmd.Parameters.Add("@MaxID", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                int promotionID = Convert.ToInt32(cmd.Parameters["@MaxID"].Value);
                return promotionID;
            }

            private void Insert_Promotion_Item(int promotionID, string[] arr)
            {
                foreach (string itemCode in arr)
                {
                    SqlConnection conn = new SqlConnection(connString);
                    SqlCommand cmd = new SqlCommand("SP_Promotion_Item_InsertUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@Promotion_ID", promotionID);
                    cmd.Parameters.AddWithValue("@Item_Code", itemCode);
                    cmd.Parameters.AddWithValue("@Option", "Save");

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();    
                }
            }

            protected void btnSave_Click(object sender, EventArgs e)
            {
              //  string[] arr = txtPromotionItem.Text.Split(',');
                int promotionID = Insert_Promotion();

                //Insert_Promotion_Item(promotionID, arr);
            }
             */



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

                    if (c is System.Web.UI.WebControls.Button ||

                             c is System.Web.UI.WebControls.ImageButton)
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
        
    }
}