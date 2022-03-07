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
    public partial class Email_Magazine_Preview : System.Web.UI.Page
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
                }
                if (Request.QueryString["PID"] != null)
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    int pid = int.Parse(Request.QueryString["PID"].ToString());
                    eml = new Email_Magazine_BL();
                    dt = eml.SelectByPID(pid);
                    Confirm1(dt, dt1);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void Confirm1(DataTable dt, DataTable dt1)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblEmailMagazineID.Text = dt.Rows[0]["Mail_Magazine_ID"].ToString();
                    txtMailName.Text = dt.Rows[0]["Mail_magazine_Name"].ToString();
                    lblTarget_Shop.Text = dt.Rows[0]["Shop_Name"].ToString();
                    lblDeliveryDate.Text = dt.Rows[0]["Delivery_date"].ToString();
                    lblHour.Text = dt.Rows[0]["Delivery_time"].ToString();
                    lblCpg11.Text = dt.Rows[0]["Campaign1"].ToString();
                    lblCpg12.Text = dt.Rows[0]["Campaign2"].ToString();
                    lblCpg13.Text = dt.Rows[0]["Campaign3"].ToString();
                    lblCpg14.Text = dt.Rows[0]["Campaign4"].ToString();
                    lblCpg15.Text = dt.Rows[0]["Campaign5"].ToString();
                    lblCpg16.Text = dt.Rows[0]["Campaign6"].ToString();
                    lblCpg17.Text = dt.Rows[0]["Campaign7"].ToString();
                    lblCpg18.Text = dt.Rows[0]["Campaign8"].ToString();
                    lblCpg19.Text = dt.Rows[0]["Campaign9"].ToString();
                    lblCpg110.Text = dt.Rows[0]["Campaign10"].ToString();
                    dt1 = eml.SelectByCName(lblCpg11.Text);
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        txtCpgURL1.Text = dt1.Rows[0]["CampaignUrl_PC"].ToString();
                        txtCpg1.Text = dt1.Rows[0]["Promotion_Name"].ToString();
                        txtMailMagazineEvent1.Text = dt1.Rows[0]["Mail_Magazine_Event1"].ToString();
                    }
                    dt1 = eml.SelectByCName(lblCpg12.Text);
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        txtCpgURL2.Text = dt1.Rows[0]["CampaignUrl_PC"].ToString();
                        txtCpg2.Text = dt1.Rows[0]["Promotion_Name"].ToString();
                        txtMailMagazineEvent2.Text = dt1.Rows[0]["Mail_Magazine_Event1"].ToString();
                      
                    }
                    dt1 = eml.SelectByCName(lblCpg13.Text);
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        txtCpgURL3.Text = dt1.Rows[0]["CampaignUrl_PC"].ToString();
                        txtCpg3.Text = dt1.Rows[0]["Promotion_Name"].ToString();
                        txtMailMagazineEvent3.Text = dt1.Rows[0]["Mail_Magazine_Event1"].ToString();
                       
                    }

                    dt1 = eml.SelectByCName(lblCpg14.Text);
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        txtCpgURL4.Text = dt1.Rows[0]["CampaignUrl_PC"].ToString();
                        txtCpg4.Text = dt1.Rows[0]["Promotion_Name"].ToString();
                        txtMailMagazineEvent4.Text = dt1.Rows[0]["Mail_Magazine_Event1"].ToString();
                    }

                    dt1 = eml.SelectByCName(lblCpg15.Text);
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        txtCpgURL5.Text = dt1.Rows[0]["CampaignUrl_PC"].ToString();
                        txtCpg5.Text = dt1.Rows[0]["Promotion_Name"].ToString();
                        txtMailMagazineEvent5.Text = dt1.Rows[0]["Mail_Magazine_Event1"].ToString();
                    }

                    dt1 = eml.SelectByCName(lblCpg16.Text);
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        txtCpgURL6.Text = dt1.Rows[0]["CampaignUrl_PC"].ToString();
                        txtCpg6.Text = dt1.Rows[0]["Promotion_Name"].ToString();
                        txtMailMagazineEvent6.Text = dt1.Rows[0]["Mail_Magazine_Event1"].ToString();
                    }

                    dt1 = eml.SelectByCName(lblCpg17.Text);
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        txtCpgURL7.Text = dt1.Rows[0]["CampaignUrl_PC"].ToString();
                        txtCpg7.Text = dt1.Rows[0]["Promotion_Name"].ToString();
                        txtMailMagazineEvent7.Text = dt1.Rows[0]["Mail_Magazine_Event1"].ToString();
                    }

                    dt1 = eml.SelectByCName(lblCpg18.Text);
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        txtCpgURL8.Text = dt1.Rows[0]["CampaignUrl_PC"].ToString();
                        txtCpg8.Text = dt1.Rows[0]["Promotion_Name"].ToString();
                        txtMailMagazineEvent8.Text = dt1.Rows[0]["Mail_Magazine_Event1"].ToString();
                    }

                    dt1 = eml.SelectByCName(lblCpg19.Text);
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        txtCpgURL9.Text = dt1.Rows[0]["CampaignUrl_PC"].ToString();
                        txtCpg9.Text = dt1.Rows[0]["Promotion_Name"].ToString();
                        txtMailMagazineEvent9.Text = dt1.Rows[0]["Mail_Magazine_Event1"].ToString();
                    }

                    dt1 = eml.SelectByCName(lblCpg110.Text);
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        txtCpgURL10.Text = dt1.Rows[0]["CampaignUrl_PC"].ToString();
                        txtCpg10.Text = dt1.Rows[0]["Promotion_Name"].ToString();
                        txtMailMagazineEvent10.Text = dt1.Rows[0]["Mail_Magazine_Event1"].ToString();
                    }
                  
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Response.Redirect("Email_Magazine_List.aspx");
        }
    }
}