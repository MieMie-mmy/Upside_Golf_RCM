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
using System.Configuration;
using System.Transactions;
using System.Globalization;
using System.Collections;



namespace ORS_RCM.WebForms.Promotion
{
    public partial class Email_Magazine_Entry : System.Web.UI.Page
    {
        Email_Magazine_BL eml;
        Shop_BL shopBL;
        Email_Magazine_Entity eme;
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
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
                    GetShop();
                    shopBL = new Shop_BL();
                       if (Request.QueryString["ID"] != null)
                    {
                        eml = new Email_Magazine_BL();
                        int id = int.Parse(Request.QueryString["ID"].ToString());
                        GetShop();
                        eme = eml.SelectByID(id);
                        GetMagazineData(eme);
                    }
                     
                }

                SetMagazineData();
                BindDatetime();
              
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void BindDatetime()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Request.Form[txtDeliveryDate.UniqueID]))
                    txtDeliveryDate.Text = Request.Form[txtDeliveryDate.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        public void GetShop()
        {
            try
            {
                shopBL = new Shop_BL();
                DataTable dt = shopBL.SelectShopAndMall();
                lstTarget_Shop.DataSource = dt;
                lstTarget_Shop.DataValueField = "ID";
                lstTarget_Shop.DataTextField = "Shop_Name";
                lstTarget_Shop.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public Email_Magazine_Entity SetMagazineData()
        {
            try
            {
                string fromDate = hfdate.Value;
                eme = new Email_Magazine_Entity();
                if (!String.IsNullOrWhiteSpace(txtEmailMagazineID.Text))
                {
                    eme.Mail_Magazine_ID = txtEmailMagazineID.Text;
                }
                else
                    eme.Mail_Magazine_ID = null;
                if (!String.IsNullOrWhiteSpace(txtMailName.Text))
                {
                    eme.Mail_Magazine_Name = txtMailName.Text;
                }
                else
                    eme.Mail_Magazine_Name = null;

                if (!string.IsNullOrWhiteSpace(Request.Form[txtDeliveryDate.UniqueID]))
                {
                    string startDate = Convert.ToDateTime(Request.Form[txtDeliveryDate.UniqueID]).ToShortDateString() + "  " + eme.Delivery_Date;
                    // DateTime periodFromDate = DateTime.Parse(startDate);
                    eme.Delivery_Date = Convert.ToDateTime(startDate);
                    hfdate.Value = startDate;
                }
                else
                    eme.Delivery_Date = null;
              

                eme.Delivery_Time = ddlHour.SelectedItem.ToString() + ':' + ddlMinute.SelectedItem.ToString();
                if (lstTarget_Shop.SelectedItem != null)
                {
                    eme.Shopname = lstTarget_Shop.SelectedItem.Text;
                    eme.Shop_ID = Convert.ToInt32(lstTarget_Shop.SelectedValue);
                }
                else
                {
                    eme.Shopname = "";
                    eme.Shop_ID = 0;
                }
                
                eme.Campaign1 = txtCpg1.Text;
                eme.Campaign2 = txtCpg2.Text;
                eme.Campaign3 = txtCpg3.Text;
                eme.Campaign4 = txtCpg4.Text;
                eme.Campaign5 = txtCpg5.Text;
                eme.Campaign6 = txtCpg6.Text;
                eme.Campaign7 = txtCpg7.Text;
                eme.Campaign8 = txtCpg8.Text;
                eme.Campaign9 = txtCpg9.Text;
                eme.Campaign10 = txtCpg10.Text;
                eme.CampaignURL1 = txtCpgURL1.Text;
                eme.CampaignURL2 = txtCpgURL2.Text;
                eme.CampaignURL3 = txtCpgURL3.Text;
                eme.CampaignURL4 = txtCpgURL4.Text;
                eme.CampaignURL5 = txtCpgURL5.Text;
                eme.CampaignURL6 = txtCpgURL6.Text;
                eme.CampaignURL7 = txtCpgURL7.Text;
                eme.CampaignURL8 = txtCpgURL8.Text;
                eme.CampaignURL9 = txtCpgURL9.Text;
                eme.CampaignURL10 = txtCpgURL10.Text;
                eme.Promotion1 = lblCpg1.Text;
                eme.Promotion2 = lblCpg2.Text;
                eme.Promotion3 = lblCpg3.Text;
                eme.Promotion4 = lblCpg4.Text;
                eme.Promotion5 = lblCpg5.Text;
                eme.Promotion6 = lblCpg6.Text;
                eme.Promotion7 = lblCpg7.Text;
                eme.Promotion8 = lblCpg8.Text;
                eme.Promotion9 = lblCpg9.Text;
                eme.Promotion10 = lblCpg10.Text;
                eme.Mail_Magazine_Event11 = txtMailMagazineEvent11.Text;
                eme.Mail_Magazine_Event21 = txtMailMagazineEvent21.Text;
                eme.Mail_Magazine_Event31 = txtMailMagazineEvent31.Text;
                eme.Mail_Magazine_Event12 = txtMailMagazineEvent12.Text;
                eme.Mail_Magazine_Event22 = txtMailMagazineEvent22.Text;
                eme.Mail_Magazine_Event32 = txtMailMagazineEvent32.Text;
                eme.Mail_Magazine_Event13 = txtMailMagazineEvent13.Text;
                eme.Mail_Magazine_Event23 = txtMailMagazineEvent23.Text;
                eme.Mail_Magazine_Event33 = txtMailMagazineEvent33.Text;
                eme.Mail_Magazine_Event14 = txtMailMagazineEvent14.Text;
                eme.Mail_Magazine_Event24 = txtMailMagazineEvent24.Text;
                eme.Mail_Magazine_Event34 = txtMailMagazineEvent34.Text;
                eme.Mail_Magazine_Event15 = txtMailMagazineEvent15.Text;
                eme.Mail_Magazine_Event25 = txtMailMagazineEvent25.Text;
                eme.Mail_Magazine_Event35 = txtMailMagazineEvent35.Text;
                eme.Mail_Magazine_Event16 = txtMailMagazineEvent16.Text;
                eme.Mail_Magazine_Event26 = txtMailMagazineEvent26.Text;
                eme.Mail_Magazine_Event36 = txtMailMagazineEvent36.Text;
                eme.Mail_Magazine_Event17 = txtMailMagazineEvent17.Text;
                eme.Mail_Magazine_Event27 = txtMailMagazineEvent27.Text;
                eme.Mail_Magazine_Event37 = txtMailMagazineEvent37.Text;
                eme.Mail_Magazine_Event18 = txtMailMagazineEvent18.Text;
                eme.Mail_Magazine_Event28 = txtMailMagazineEvent28.Text;
                eme.Mail_Magazine_Event38 = txtMailMagazineEvent38.Text;
                eme.Mail_Magazine_Event19 = txtMailMagazineEvent19.Text;
                eme.Mail_Magazine_Event29 = txtMailMagazineEvent29.Text;
                eme.Mail_Magazine_Event39 = txtMailMagazineEvent39.Text;
                eme.Mail_Magazine_Event110 = txtMailMagazineEvent110.Text;
                eme.Mail_Magazine_Event210 = txtMailMagazineEvent210.Text;
                eme.Mail_Magazine_Event310 = txtMailMagazineEvent310.Text;
                Session["eme"] = eme;
                return eme;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new Email_Magazine_Entity();
            }
        }

        public void GetMagazineData(Email_Magazine_Entity eme)
        {
            try
            {
                txtEmailMagazineID.Text = eme.Mail_Magazine_ID;
                txtMailName.Text = eme.Mail_Magazine_Name;
                lstTarget_Shop.SelectedValue = eme.Shop_ID.ToString();
                if (eme.Delivery_Date.ToString() != "")
                {
                    DateTime deliverydate = DateTime.Parse(eme.Delivery_Date.ToString());
                    txtDeliveryDate.Text = deliverydate.ToShortDateString();
                    txtDeliveryDate.Text = txtDeliveryDate.Text;
                }
                txtCpg1.Text = eme.Campaign1;
                txtCpg2.Text = eme.Campaign2;
                txtCpg3.Text = eme.Campaign3;
                txtCpg4.Text = eme.Campaign4;
                txtCpg5.Text = eme.Campaign5;
                txtCpg6.Text = eme.Campaign6;
                txtCpg7.Text = eme.Campaign7;
                txtCpg8.Text = eme.Campaign8;
                txtCpg9.Text = eme.Campaign9;
                txtCpg10.Text = eme.Campaign10;
                if (eme.Delivery_Time != null)
                {
                    string[] startTime = eme.Delivery_Time.Split(':');
                    ddlHour.Text = startTime[0];
                    ddlMinute.Text = startTime[1];
                }
                  
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
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

        public DataTable SelectCampaignID()
        {
            dt.Columns.Add("CampaignID");
            if (!String.IsNullOrWhiteSpace(txtCpg1.Text))
                dt.Rows.Add(txtCpg1.Text);

            if (!String.IsNullOrWhiteSpace(txtCpg2.Text))
                dt.Rows.Add(txtCpg2.Text);

            if (!String.IsNullOrWhiteSpace(txtCpg3.Text))
                dt.Rows.Add(txtCpg3.Text);

            if (!String.IsNullOrWhiteSpace(txtCpg4.Text))
                dt.Rows.Add(txtCpg4.Text);

            if (!String.IsNullOrWhiteSpace(txtCpg5.Text))
                dt.Rows.Add(txtCpg5.Text);

            if (!String.IsNullOrWhiteSpace(txtCpg6.Text))
                dt.Rows.Add(txtCpg6.Text);

            if (!String.IsNullOrWhiteSpace(txtCpg7.Text))
                dt.Rows.Add(txtCpg7.Text);

            if (!String.IsNullOrWhiteSpace(txtCpg8.Text))
                dt.Rows.Add(txtCpg8.Text);

            if (!String.IsNullOrWhiteSpace(txtCpg9.Text))
                dt.Rows.Add(txtCpg9.Text);

            if (!String.IsNullOrWhiteSpace(txtCpg10.Text))
                dt.Rows.Add(txtCpg10.Text);

            return dt;
        }

        protected void btnCampaignName_Click(object sender, EventArgs e)
        {
            try
            {
                eml = new Email_Magazine_BL();
                DataTable dt = eml.SelectByCName(txtCpg1.Text);
                if (dt.Rows.Count > 0)
                {
                    lblCpg1.Visible = true;
                    lblCpg1.Text = dt.Rows[0]["Promotion_Name"].ToString();
                    txtCpgURL1.Text = dt.Rows[0]["CampaignUrl_PC"].ToString();
                    txtMailMagazineEvent11.Text = dt.Rows[0]["Mail_Magazine_Event1"].ToString();
                    txtMailMagazineEvent21.Text = dt.Rows[0]["Mail_Magazine_Event2"].ToString();
                    txtMailMagazineEvent31.Text = dt.Rows[0]["Mail_Magazine_Event3"].ToString();
                }
                else
                {
                    txtMailMagazineEvent11.Text = String.Empty;
                    txtMailMagazineEvent21.Text = String.Empty;
                    txtMailMagazineEvent31.Text = String.Empty;
                    lblCpg1.Visible = false;
                    lblCpg1.Text = String.Empty;
                    txtCpgURL1.Text = String.Empty;
                }

                dt = eml.SelectByCName(txtCpg2.Text);
                if (dt.Rows.Count > 0)
                {
                    lblCpg2.Visible = true;
                    lblCpg2.Text = dt.Rows[0]["Promotion_Name"].ToString();
                    txtCpgURL2.Text = dt.Rows[0]["CampaignUrl_PC"].ToString();
                    txtMailMagazineEvent12.Text = dt.Rows[0]["Mail_Magazine_Event1"].ToString();
                    txtMailMagazineEvent22.Text = dt.Rows[0]["Mail_Magazine_Event2"].ToString();
                    txtMailMagazineEvent32.Text = dt.Rows[0]["Mail_Magazine_Event3"].ToString();
                }
                else
                {
                    txtMailMagazineEvent12.Text = String.Empty;
                    txtMailMagazineEvent22.Text = String.Empty;
                    txtMailMagazineEvent32.Text = String.Empty;
                    lblCpg2.Visible = false;
                    lblCpg2.Text = String.Empty;
                    txtCpgURL2.Text = String.Empty;
                }

                dt = eml.SelectByCName(txtCpg3.Text);
                if (dt.Rows.Count > 0)
                {
                    lblCpg3.Visible = true;
                    lblCpg3.Text = dt.Rows[0]["Promotion_Name"].ToString();
                    txtCpgURL3.Text = dt.Rows[0]["CampaignUrl_PC"].ToString();
                    txtMailMagazineEvent13.Text = dt.Rows[0]["Mail_Magazine_Event1"].ToString();
                    txtMailMagazineEvent23.Text = dt.Rows[0]["Mail_Magazine_Event2"].ToString();
                    txtMailMagazineEvent33.Text = dt.Rows[0]["Mail_Magazine_Event3"].ToString();
                }
                else
                {
                    txtMailMagazineEvent13.Text = String.Empty;
                    txtMailMagazineEvent23.Text = String.Empty;
                    txtMailMagazineEvent33.Text = String.Empty;
                    lblCpg3.Visible = false;
                    lblCpg3.Text = String.Empty;
                    txtCpgURL3.Text = String.Empty;
                }

                dt = eml.SelectByCName(txtCpg4.Text);
                if (dt.Rows.Count > 0)
                {
                    lblCpg4.Visible = true;
                    lblCpg4.Text = dt.Rows[0]["Promotion_Name"].ToString();
                    txtCpgURL4.Text = dt.Rows[0]["CampaignUrl_PC"].ToString();
                    txtMailMagazineEvent14.Text = dt.Rows[0]["Mail_Magazine_Event1"].ToString();
                    txtMailMagazineEvent24.Text = dt.Rows[0]["Mail_Magazine_Event2"].ToString();
                    txtMailMagazineEvent34.Text = dt.Rows[0]["Mail_Magazine_Event3"].ToString();
                }
                else
                {
                    txtMailMagazineEvent14.Text = String.Empty;
                    txtMailMagazineEvent24.Text = String.Empty;
                    txtMailMagazineEvent34.Text = String.Empty;
                    lblCpg4.Visible = false;
                    lblCpg4.Text = String.Empty;
                    txtCpgURL4.Text = String.Empty;
                }

                dt = eml.SelectByCName(txtCpg5.Text);
                if (dt.Rows.Count > 0)
                {
                    lblCpg5.Visible = true;
                    lblCpg5.Text = dt.Rows[0]["Promotion_Name"].ToString();
                    txtCpgURL5.Text = dt.Rows[0]["CampaignUrl_PC"].ToString();
                    txtMailMagazineEvent15.Text = dt.Rows[0]["Mail_Magazine_Event1"].ToString();
                    txtMailMagazineEvent25.Text = dt.Rows[0]["Mail_Magazine_Event2"].ToString();
                    txtMailMagazineEvent35.Text = dt.Rows[0]["Mail_Magazine_Event3"].ToString();
                }
                else
                {
                    txtMailMagazineEvent15.Text = String.Empty;
                    txtMailMagazineEvent25.Text = String.Empty;
                    txtMailMagazineEvent35.Text = String.Empty;
                    lblCpg5.Visible = false;
                    lblCpg5.Text = String.Empty;
                    txtCpgURL5.Text = String.Empty;
                }

                dt = eml.SelectByCName(txtCpg6.Text);
                if (dt.Rows.Count > 0)
                {
                    lblCpg6.Visible = true;
                    lblCpg6.Text = dt.Rows[0]["Promotion_Name"].ToString();
                    txtCpgURL6.Text = dt.Rows[0]["CampaignUrl_PC"].ToString();
                    txtMailMagazineEvent16.Text = dt.Rows[0]["Mail_Magazine_Event1"].ToString();
                    txtMailMagazineEvent26.Text = dt.Rows[0]["Mail_Magazine_Event2"].ToString();
                    txtMailMagazineEvent36.Text = dt.Rows[0]["Mail_Magazine_Event3"].ToString();
                }
                else
                {
                    txtMailMagazineEvent16.Text = String.Empty;
                    txtMailMagazineEvent26.Text = String.Empty;
                    txtMailMagazineEvent36.Text = String.Empty;
                    lblCpg6.Visible = false;
                    lblCpg6.Text = String.Empty;
                    txtCpgURL6.Text = String.Empty;
                }

                dt = eml.SelectByCName(txtCpg7.Text);
                if (dt.Rows.Count > 0)
                {
                    lblCpg7.Visible = true;
                    lblCpg7.Text = dt.Rows[0]["Promotion_Name"].ToString();
                    txtCpgURL7.Text = dt.Rows[0]["CampaignUrl_PC"].ToString();
                    txtMailMagazineEvent17.Text = dt.Rows[0]["Mail_Magazine_Event1"].ToString();
                    txtMailMagazineEvent27.Text = dt.Rows[0]["Mail_Magazine_Event2"].ToString();
                    txtMailMagazineEvent37.Text = dt.Rows[0]["Mail_Magazine_Event3"].ToString();
                }
                else
                {
                    txtMailMagazineEvent17.Text = String.Empty;
                    txtMailMagazineEvent27.Text = String.Empty;
                    txtMailMagazineEvent37.Text = String.Empty;
                    lblCpg7.Visible = false;
                    lblCpg7.Text = String.Empty;
                    txtCpgURL7.Text = String.Empty;
                }

                dt = eml.SelectByCName(txtCpg8.Text);
                if (dt.Rows.Count > 0)
                {
                    lblCpg8.Visible = true;
                    lblCpg8.Text = dt.Rows[0]["Promotion_Name"].ToString();
                    txtCpgURL8.Text = dt.Rows[0]["CampaignUrl_PC"].ToString();
                    txtMailMagazineEvent18.Text = dt.Rows[0]["Mail_Magazine_Event1"].ToString();
                    txtMailMagazineEvent28.Text = dt.Rows[0]["Mail_Magazine_Event2"].ToString();
                    txtMailMagazineEvent38.Text = dt.Rows[0]["Mail_Magazine_Event3"].ToString();
                }
                else
                {
                    txtMailMagazineEvent18.Text = String.Empty;
                    txtMailMagazineEvent28.Text = String.Empty;
                    txtMailMagazineEvent38.Text = String.Empty;
                    lblCpg8.Visible = false;
                    lblCpg8.Text = String.Empty;
                    txtCpgURL8.Text = String.Empty;
                }

                dt = eml.SelectByCName(txtCpg9.Text);
                if (dt.Rows.Count > 0)
                {
                    lblCpg9.Text = dt.Rows[0]["Promotion_Name"].ToString();
                    txtCpgURL9.Text = dt.Rows[0]["CampaignUrl_PC"].ToString();
                    txtMailMagazineEvent19.Text = dt.Rows[0]["Mail_Magazine_Event1"].ToString();
                    txtMailMagazineEvent29.Text = dt.Rows[0]["Mail_Magazine_Event2"].ToString();
                    txtMailMagazineEvent39.Text = dt.Rows[0]["Mail_Magazine_Event3"].ToString();
                }
                else
                {
                    lblCpg9.Visible = true;
                    txtMailMagazineEvent19.Text = String.Empty;
                    txtMailMagazineEvent29.Text = String.Empty;
                    txtMailMagazineEvent39.Text = String.Empty;
                    lblCpg9.Visible = false;
                    lblCpg9.Text = String.Empty;
                    txtCpgURL9.Text = String.Empty;
                }

                dt = eml.SelectByCName(txtCpg10.Text);
                if (dt.Rows.Count > 0)
                {
                    lblCpg10.Visible = true;
                    lblCpg10.Text = dt.Rows[0]["Promotion_Name"].ToString();
                    txtCpgURL10.Text = dt.Rows[0]["CampaignUrl_PC"].ToString();
                    txtMailMagazineEvent110.Text = dt.Rows[0]["Mail_Magazine_Event1"].ToString();
                    txtMailMagazineEvent210.Text = dt.Rows[0]["Mail_Magazine_Event2"].ToString();
                    txtMailMagazineEvent310.Text = dt.Rows[0]["Mail_Magazine_Event3"].ToString();
                }
                else
                {
                    txtMailMagazineEvent110.Text = String.Empty;
                    txtMailMagazineEvent210.Text = String.Empty;
                    txtMailMagazineEvent310.Text = String.Empty;
                    lblCpg10.Visible = false;
                    lblCpg10.Text = String.Empty;
                    txtCpgURL10.Text = String.Empty;
                }
               
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnCreateURL1_Click(object sender, EventArgs e)
        {

        }

        protected void btnCreateURL_Click2(object sender, EventArgs e)
        {

        }

        protected void btnCreateURL_Click3(object sender, EventArgs e)
        {

        }

        protected void btnCreateURL_Click4(object sender, EventArgs e)
        {

        }

        protected void btnCreateURL_Click5(object sender, EventArgs e)
        {

        }

        protected void btnCreateURL_Click6(object sender, EventArgs e)
        {

        }

        protected void btnCreateURL_Click7(object sender, EventArgs e)
        {

        }

        protected void btnCreateURL_Click8(object sender, EventArgs e)
        {

        }

        protected void btnCreateURL_Click9(object sender, EventArgs e)
        {

        }

        protected void btnCreateURL_Click10(object sender, EventArgs e)
        {

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
                eml = new Email_Magazine_BL();
                eme = new Email_Magazine_Entity();
                int lid = 0;
                if (Request.QueryString["ID"] != null)
                    lid = Convert.ToInt32(Request.QueryString["ID"].ToString());
                 string mid =txtEmailMagazineID.Text;
                 int count = eml.Check_MalID(lid, mid);
                 if (count > 0)
                 {
                     GlobalUI.MessageBox("そのメールマガジンIDは既に登録されています");
                 }
                 else
                 {
                     DataTable dt = SelectCampaignID();
                     if (dt.Rows.Count == 0)
                     {
                         GlobalUI.MessageBox("Enter CampaignID!!");
                     }
                     else
                     {
                         int ID ;
                         DataTable dtResult = eml.CreateXmlforEmailMagazine(dt);
                         if (dtResult != null && dtResult.Rows.Count > 0)
                         { GlobalUI.MessageBox("CampaignID does not exists!!"); return; }
                         else if(Request.QueryString["ID"]=="0")
                         {
                                   ID = 0;
                                   Response.Redirect("Email_Magazine_Confirm.aspx?ID=" + ID, false);
                         }
                         else
                         {
                              ID = Convert.ToInt32(Session["ID"]);
                             Response.Redirect("Email_Magazine_Confirm.aspx?ID=" + ID, false);
                         }
                     }
                 }
             
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtDeliveryDate.Text = String.Empty;
                hfdate.Value= Request.Form[txtDeliveryDate.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
     }
  }