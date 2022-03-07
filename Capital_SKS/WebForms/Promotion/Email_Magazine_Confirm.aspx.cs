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

namespace ORS_RCM
{
    public partial class Email_Magazine_Confirm : System.Web.UI.Page
    {
        Email_Magazine_BL eml;
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
                    Email_Magazine_Entity eme1 = Session["eme"] as Email_Magazine_Entity;
                    Confirm(eme1);
                }
               
                
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        public void Confirm(Email_Magazine_Entity eme1)
        {
            try
            {
                lblEmailMagazineID.Text = eme1.Mail_Magazine_ID;
                lblMailName.Text = eme1.Mail_Magazine_Name;
                string startDate = Convert.ToDateTime(eme1.Delivery_Date).ToShortDateString();
                lblDeliveryDate.Text = startDate;
               // lblDeliveryDate.Text = eme1.Delivery_Date.ToString();
                lblHour.Text = eme1.Delivery_Time;
                lblTarget_Shop.Text = eme1.Shopname;
                lblCpg11.Text = eme1.Campaign1;
                lblCpg12.Text = eme1.Campaign2;
                lblCpg13.Text = eme1.Campaign3;
                lblCpg14.Text = eme1.Campaign4;
                lblCpg15.Text = eme1.Campaign5;
                lblCpg16.Text = eme1.Campaign6;
                lblCpg17.Text = eme1.Campaign7;
                lblCpg18.Text = eme1.Campaign8;
                lblCpg19.Text = eme1.Campaign9;
                lblCpg110.Text = eme1.Campaign10;
                lblCpgURL1.Text = eme1.CampaignURL1;
                lblCpgURL2.Text = eme1.CampaignURL2;
                lblCpgURL3.Text = eme1.CampaignURL3;
                lblCpgURL4.Text = eme1.CampaignURL4;
                lblCpgURL5.Text = eme1.CampaignURL5;
                lblCpgURL6.Text = eme1.CampaignURL6;
                lblCpgURL7.Text = eme1.CampaignURL7;
                lblCpgURL8.Text = eme1.CampaignURL8;
                lblCpgURL9.Text = eme1.CampaignURL9;
                lblCpgURL10.Text = eme1.CampaignURL10;
                lblCpg1.Text = eme1.Promotion1;
                lblCpg2.Text = eme1.Promotion2;
                lblCpg3.Text = eme1.Promotion3;
                lblCpg4.Text = eme1.Promotion4;
                lblCpg5.Text = eme1.Promotion5;
                lblCpg6.Text = eme1.Promotion6;
                lblCpg7.Text = eme1.Promotion7;
                lblCpg8.Text = eme1.Promotion8;
                lblCpg9.Text = eme1.Promotion9;
                lblCpg10.Text = eme1.Promotion10;
                txtMailMagazineEvent11.Text = eme1.Mail_Magazine_Event11;
                txtMailMagazineEvent21.Text=eme1.Mail_Magazine_Event21;
                txtMailMagazineEvent31.Text=eme1.Mail_Magazine_Event31;                                
                txtMailMagazineEvent12.Text=eme1.Mail_Magazine_Event12;
                txtMailMagazineEvent22.Text=eme1.Mail_Magazine_Event22;
                txtMailMagazineEvent32.Text=eme1.Mail_Magazine_Event32;
                txtMailMagazineEvent13.Text=eme1.Mail_Magazine_Event13;
                txtMailMagazineEvent23.Text=eme1.Mail_Magazine_Event23;
                txtMailMagazineEvent33.Text=eme1.Mail_Magazine_Event33;
                txtMailMagazineEvent14.Text=eme1.Mail_Magazine_Event14;
                txtMailMagazineEvent24.Text=eme1.Mail_Magazine_Event24;
                txtMailMagazineEvent34.Text=eme1.Mail_Magazine_Event34;
                txtMailMagazineEvent15.Text = eme1.Mail_Magazine_Event15;
                txtMailMagazineEvent25.Text = eme1.Mail_Magazine_Event25;
                txtMailMagazineEvent35.Text = eme1.Mail_Magazine_Event35;
                txtMailMagazineEvent16.Text = eme1.Mail_Magazine_Event16;
                txtMailMagazineEvent26.Text = eme1.Mail_Magazine_Event26;
                txtMailMagazineEvent36.Text = eme1.Mail_Magazine_Event36;
                txtMailMagazineEvent17.Text = eme1.Mail_Magazine_Event17;
                txtMailMagazineEvent27.Text = eme1.Mail_Magazine_Event27;
                txtMailMagazineEvent37.Text = eme1.Mail_Magazine_Event37;
                txtMailMagazineEvent18.Text = eme1.Mail_Magazine_Event18;
                txtMailMagazineEvent28.Text = eme1.Mail_Magazine_Event28;
                txtMailMagazineEvent38.Text = eme1.Mail_Magazine_Event38;
                txtMailMagazineEvent19.Text = eme1.Mail_Magazine_Event19;
                txtMailMagazineEvent29.Text = eme1.Mail_Magazine_Event29;
                txtMailMagazineEvent39.Text = eme1.Mail_Magazine_Event39;
                txtMailMagazineEvent110.Text = eme1.Mail_Magazine_Event110;
                txtMailMagazineEvent210.Text = eme1.Mail_Magazine_Event210;
                txtMailMagazineEvent310.Text = eme1.Mail_Magazine_Event310;
                
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ddlSearch89_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            eml = new Email_Magazine_BL();
            Email_Magazine_Entity eme2 = Session["eme"] as Email_Magazine_Entity;
            if (Request.QueryString["ID"]!=null && Request.QueryString["ID"]!="0")
            {
                String result = null;
                //eme = SetMagazineData();
                eme2.ID = int.Parse(Request.QueryString["ID"].ToString());
                result = eml.Update(eme2);
                if (result == "Update Successful !")
                {
                    object referrer = ViewState["UrlReferrer"];
                    string url = "Email_Magazine_List.aspx";
                    string script = "window.onload = function(){alert ('";
                    script += result;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true); 

                }//if
                else { GlobalUI.MessageBox("Update Fail!"); }
            }//if
            else
            {
                String result = eml.Insert(eme2);
                if (result == "Save Successful !")
                {
                    object referrer = ViewState["UrlReferrer"];
                    string url = "Email_Magazine_List.aspx";
                    string script = "window.onload = function(){ alert('";
                    script += result;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                }//if
                else { GlobalUI.MessageBox("Save Fail!"); }
            }
        }
    }
}