/* 
*
Created By              : Pyae Phyo Khine
Created Date          :04/2015
Updated By             :
Updated Date         :

 Tables using: Promotion_Delivery
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
using ORS_RCM_Common;
using System.Collections;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace ORS_RCM.WebForms.Promotion
{
    public partial class Promotion_Delivery : System.Web.UI.Page
    {
        Promotion_Delivery_BL pdbl;
        Shop_BL shopBL;
        Promotion_Delivery_Entity pde;
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtitemcode.Attributes.Add("onKeyPress", "doClick('" + btnsearch.ClientID + "',event)");
                if (!IsPostBack)
                {                 
                    pdbl = new Promotion_Delivery_BL();
                    if (Request.QueryString["Item_Code"] != null)
                    {
                        AllSearchData();
                        GetShop();
                        gp.Visible = false;
                    }
                    else
                    {
                        GetShop();
                        gp.Visible = false;
                    }
                }
                else
                {
                    gp.Visible = true;
                    pde = new Promotion_Delivery_Entity();
                    
                    String ctrl = getPostBackControlName();
                    if (!String.IsNullOrWhiteSpace(ctrl))
                    {
                        if (ctrl.Contains("lnkPaging"))
                        {
                            DataTable dt = new DataTable();

                            gp.LinkButtonClick(ctrl, gvview.PageSize);

                            Label lbl = gp.FindControl("lblCurrent") as Label;
                            int index = Convert.ToInt32(lbl.Text);

                            dt = SearchData(index);
                            gvview.DataSource = dt;
                            gvview.DataBind();
                        }

                        BindDatetime();

                    }

                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        public DataTable SearchData(int pgindex)
        {
            try
            {
                pdbl = new Promotion_Delivery_BL();
                Promotion_Delivery_Entity pde = GetEntity();
                DataTable dt = new DataTable();
                int page_size = Convert.ToInt32(ddlpage.SelectedValue.ToString());
                if (chkcheck.Checked && !String.IsNullOrWhiteSpace(txtitemcode.Text.Trim()))
                {
                    //dt = pbl.SelectallData(ppime, 0);//equal
                    //dt = pdbl.SelectallDataEqual(pde, 0, pgindex + 1, page_size);
                    if (pde.Shipping == "0")
                    {
                        dt = pdbl.SelectallDataEqual(pde, 0, pgindex, page_size);
                    }
                    else
                    {
                        dt = pdbl.DeliveryEqualSearch(pde, 0, pgindex, page_size);
                    }
                }
                else
                {
                    //dt = pdbl.SelectallData(pde, 1, pgindex + 1, page_size);
                    if (pde.Shipping == "0")
                    {
                        dt = pdbl.SelectallData(pde, 1, pgindex, page_size);
                    }
                    else
                    {
                        dt = pdbl.SelectIsDelivery(pde, 1, pgindex, page_size);
                    }

                }

                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            }
        }

        public void GetShop()
        {
            try
            {
                shopBL = new Shop_BL();
                DataTable dt = shopBL.SelectShopAndMall();
                listshop.DataSource = dt;
                listshop.DataValueField = "ID";
                listshop.DataTextField = "Shop_Name";
                listshop.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

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
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return null;
            }
        }

        protected void BindDatetime()
        {
            try
            {
                //Save
                if (!string.IsNullOrWhiteSpace(Request.Form[txtRperiodfrom.UniqueID]))
                    txtRperiodfrom.Text = Request.Form[txtRperiodfrom.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtRperiodto.UniqueID]))
                    txtRperiodto.Text = Request.Form[txtRperiodto.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        public Promotion_Delivery_Entity GetEntity()
        {
            try
            {
                string shopnames = null;
                //string shipping=null;
                Promotion_Delivery_Entity pde = new Promotion_Delivery_Entity();
                pde.Itemcode = txtitemcode.Text.Trim();
                pde.Itemname = txtitemname.Text.Trim();
                pde.Brandname = txtbrandname.Text.Trim();
                for (int i = 0; i < listshop.Items.Count; i++)
                {
                    if (listshop.Items[i].Selected)
                    {
                        //dtshop.Rows.Add(promotionID, int.Parse(listRshop.Items[i].Value.ToString()));
                        shopnames+= listshop.Items[i].Value.ToString() + ",";
                    }
                }
                // if (!String.IsNullOrWhiteSpace(listshop.SelectedItem.ToString()))
                // ppime.Shopnmae = listshop.SelectedItem.ToString();
                pde.Shopnmae = shopnames;
                if ((rdoship1.Checked) || (rdoship2.Checked))
                {
                    if (rdoship1.Checked == true)
                    {
                        //shipping = Convert.ToString(true);
                        pde.Shipping = "1";
                    }
                    else
                    {
                        pde.Shipping = "0";
                    }

                }

                DateTime? FromDate = new DateTime();
                DateTime? ToDate = new DateTime();
                pde.Rperiodfrom = FromDate;
                pde.Rperiodto = ToDate;
                return pde;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new Promotion_Delivery_Entity();
            }

        }

        public DataTable GetData()
        {
            try
            {
                if (ViewState["checkedValue"] != null)
                {
                    pde = Getdata();
                    DataColumn newcols = new DataColumn("Item_Code", typeof(string));
                    DataColumn newcols1 = new DataColumn("IsDelivery", typeof(string));
                    newcols.DefaultValue = null;
                    newcols1.DefaultValue = null;
                    dt.Columns.Add(newcols);
                    dt.Columns.Add(newcols1);
                    dt.Columns.Add("Delivery_StartDate", typeof(DateTime));
                    dt.Columns.Add("Delivery_EndDate", typeof(DateTime));
                    dt.Columns.Add("Delivery_StartTime", typeof(string));
                    dt.Columns.Add("Delivery_EndTime", typeof(string));
                    // dt.Columns.Add("Shop_ID", typeof(string));
                    dt.Columns.Add("No", typeof(string));
                    dt.Columns.Add("Shop_Name", typeof(string));
                    dt.Columns.Add("Shop_ID", typeof(string));
                    ArrayList chk = ViewState["checkedValue"] as ArrayList;

                    if (chk != null)
                    {
                        for (int i = 0; i < chk.Count; i++)
                        {
                            DataRow destRow = dt.NewRow();
                            dt.Rows.Add(destRow);
                            dt.Rows[i]["Item_Code"] = chk[i].ToString().Split('$')[0];
                            dt.Rows[i]["Shop_Name"] = chk[i].ToString().Split('$')[1];
                            dt.Rows[i]["IsDelivery"] = pde.Shipping;
                            if (!String.IsNullOrWhiteSpace(pde.RPointperiodfrom.ToString()))
                                dt.Rows[i]["Delivery_StartDate"] = pde.RPointperiodfrom;
                            else
                                dt.Rows[i]["Delivery_StartDate"] = DBNull.Value;
                            if (!String.IsNullOrWhiteSpace(pde.RPointperiodto.ToString()))
                                dt.Rows[i]["Delivery_EndDate"] = pde.RPointperiodto;
                            else
                                dt.Rows[i]["Delivery_EndDate"] = DBNull.Value;
                            dt.Rows[i]["Delivery_StartTime"] = pde.Rstart;
                            dt.Rows[i]["Delivery_EndTime"] = pde.Rend;


                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            }
        }

        public Promotion_Delivery_Entity Getdata()
        {
            try
            {
                pde = new Promotion_Delivery_Entity();
                // int Shipping=;
                string fromDate = Request.Form[txtRperiodfrom.UniqueID];
                string toDate = Request.Form[txtRperiodto.UniqueID];
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
                pde.RPointperiodfrom = FromDate;
                pde.RPointperiodto = ToDate;

                pde.Rstart = ddlPeriodFromHour.SelectedValue + ":" + ddlRPeriodFromMinute.SelectedValue;
                pde.Rend = ddlperiodtohour.SelectedValue + ":" + ddlRPeriodToMinute.SelectedValue;
                pde.Shopnmae = listshop.SelectedValue;
                if ((rdoship3.Checked) || (rdoship4.Checked))
                {
                    if (rdoship3.Checked == true)
                    {
                        pde.Shipping = "1";
                    }
                    else
                    {
                        pde.Shipping = "0";
                    }

                }
                //if(rdoship3.Checked==
                //pde.Shipping = rdoship1.Checked.ToString();
                return pde;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new Promotion_Delivery_Entity();
            }
        }

        private DateTime DateConverter(string dateTime)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd-MM-yyyy";
            dtfi.DateSeparator = "-";
            DateTime objDate = Convert.ToDateTime(dateTime, dtfi);
            //string date = Convert.ToDateTime(objDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy hh:MM:ss");
            //objDate = DateTime.ParseExact(date, "MM/dd/yyyy hh:MM:ss", null);
            string date = Convert.ToDateTime(objDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy ");
            objDate = DateTime.ParseExact(date, "MM/dd/yyyy ", null);
            return objDate;
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                AllSearchData();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void AllSearchData()
        {
            try
            {
                pdbl = new Promotion_Delivery_BL();
                Promotion_Delivery_Entity pde = GetEntity();
                DataTable dt = new DataTable();
                int page_size = Convert.ToInt32(ddlpage.SelectedValue.ToString());
                if (Request.QueryString["Item_Code"] != null)
                {
                    pde.Shopnmae = Request.QueryString["Shop_ID"] + ',';
                    pde.Itemcode = Request.QueryString["Item_Code"].ToString();
                    pde.Shipping = "1";
                    dt = pdbl.DeliveryEqualSearch(pde, 0, 1, page_size);
                }
                else
                {
                    if ((!String.IsNullOrWhiteSpace(txtitemcode.Text.Trim()) && chkcheck.Checked) || (rdoship1.Checked) || (rdoship2.Checked))
                    {
                        if (chkcheck.Checked)
                        {
                            // dt = pdbl.SelectallData(ppime, 0);//equal
                            if (pde.Shipping == "0")
                            {
                                dt = pdbl.SelectallDataEqual(pde, 0, 1, page_size);
                            }
                            else
                            {
                                dt = pdbl.DeliveryEqualSearch(pde, 0, 1, page_size);
                            }
                        }
                        else
                        {
                            if (pde.Shipping == "0")
                            {
                                dt = pdbl.SelectallData(pde, 1, 1, page_size);
                            }
                            else
                            {
                                dt = pdbl.SelectIsDelivery(pde, 1, 1, page_size);
                            }
                        }

                    }
                }//
                if (dt != null && dt.Rows.Count > 0)
                {
                    int count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                    gvview.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    //DataTable dts1 = Search(dt);
                    gvview.DataSource = dt;
                    gvview.DataBind();
                    gp.CalculatePaging(count, gvview.PageSize, 1);
                    DataTable dts = GetData();
                }
                else
                {
                    gvview.DataSource = dt;
                    gvview.DataBind();
                    gp.CalculatePaging(0, gvview.PageSize, 1);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnsetting_Click(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < gvview.Rows.Count; j++)
                    {
                        String strdateFrom = txtRperiodfrom.Text + " " + ddlPeriodFromHour.SelectedItem.Text + ":" + ddlRPeriodFromMinute.SelectedItem.Text;
                        DateTime dtFrom = Convert.ToDateTime(strdateFrom);
                        //DateTime dtCheck = DateTime.Now.AddHours(3);
                        //TimeSpan different = dtCheck - dtFrom;
                        //String[] diff = different.ToString().Split(':');
                        //double difhour = Convert.ToDouble(diff[0]);
                        //if (difhour > 3)
                        //{
                        //    GlobalUI.MessageBox("開始期間は3時間以上先の時刻を設定してください");
                        //    return;
                        //}

                        String strdateTo = txtRperiodto.Text + " " + ddlperiodtohour.SelectedItem.Text + ":" + ddlRPeriodToMinute.SelectedItem.Text;
                        DateTime dtTo = Convert.ToDateTime(strdateTo);

                        if (dtTo < dtFrom)
                        {
                            GlobalUI.MessageBox("開始期間と終了期間に誤りがあります。");
                            return;
                        }
                        
                        CheckBox chk = gvview.Rows[j].FindControl("chktype") as CheckBox;

                        if (chk.Checked)
                        {
                            Label lblShipping = gvview.Rows[j].FindControl("lblIsDelivery") as Label;
                            if (rdoship4.Checked)
                            {
                                lblShipping.Text = "なし";
                            }
                            else lblShipping.Text = "あり";

                            Label lblPeriod = gvview.Rows[j].FindControl("lblHeldPeriod") as Label;
                            lblPeriod.Text = strdateFrom + "～" + strdateTo;
                            lblPeriod.Text = lblPeriod.Text.Replace("/", "-");
                        }   
                    }
                btnSave.Visible = true;
            }

            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        protected void btnselectall_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvview.Rows.Count; i++)
                {
                    CheckBox chk = gvview.Rows[i].FindControl("chktype") as CheckBox;
                    chk.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btncancelall_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvview.Rows.Count; i++)
                {
                    CheckBox chk = gvview.Rows[i].FindControl("chktype") as CheckBox;
                    chk.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                pdbl = new Promotion_Delivery_BL();

                if (btnSave.Text == "確認画面へ")
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Item_Code");
                        dt.Columns.Add("Item_Name");
                        dt.Columns.Add("Delivery_StartDate");
                        dt.Columns.Add("Delivery_EndDate");
                        dt.Columns.Add("Delivery_StartTime");
                        dt.Columns.Add("Delivery_EndTime");
                        dt.Columns.Add("IsDelivery");
                        dt.Columns.Add("Shop_ID");
                        dt.Columns.Add("Shop_Name");
                        dt.Columns.Add("Ctrl_ID");
                        dt.Columns.Add("Brand_Name");
                        dt.Columns.Add("ID");
                        dt.Columns.Add("Held_period");

                        int j = 0;
                        for (int i = 0; i < gvview.Rows.Count; i++)
                        {
                            CheckBox chk = gvview.Rows[i].FindControl("chktype") as CheckBox;
                            if (chk.Checked)
                            {
                                Label lblItemCode = gvview.Rows[i].FindControl("lblItemCode") as Label;
                                Label lblItemName = gvview.Rows[i].FindControl("lblItemName") as Label;
                                Label lblPeriod = gvview.Rows[i].FindControl("lblHeldPeriod") as Label;
                                String[] date = new String[2];
                                String startDate = String.Empty;
                                String endDate = String.Empty;
                                String startTime = String.Empty;
                                String endTime = String.Empty;
                                if (!String.IsNullOrWhiteSpace(lblPeriod.Text))
                                {
                                    date = lblPeriod.Text.Split('～');
                                    startDate = date[0];
                                    endDate = date[1];

                                    String[] start = startDate.Split(' ');
                                    startDate = start[0];
                                    startTime = start[start.Length-1];

                                    String[] end = endDate.Split(' ');
                                    endDate = end[0];
                                    endTime = end[end.Length-1];
                                }

                                Label lblIsDelivery = gvview.Rows[i].FindControl("lblIsDelivery") as Label;
                                Label lblShopID = gvview.Rows[i].FindControl("lblShopID") as Label;
                                Label lblShopName = gvview.Rows[i].FindControl("lblShopName") as Label;
                                Label lblCtrl = gvview.Rows[i].FindControl("lblCtrlID") as Label;
                                Label lblBrandName = gvview.Rows[i].FindControl("lblBrandName") as Label;
                                Label lblID = gvview.Rows[i].FindControl("lblID") as Label;

                                dt.Rows.Add();
                                dt.Rows[j]["Item_Code"] = lblItemCode.Text;
                                dt.Rows[j]["Item_Name"] = lblItemName.Text;
                                dt.Rows[j]["Delivery_StartDate"] = startDate;
                                dt.Rows[j]["Delivery_EndDate"] = endDate;
                                dt.Rows[j]["Delivery_StartTime"] = startTime;
                                dt.Rows[j]["Delivery_EndTime"] = endTime;

                                if (lblIsDelivery.Text.Trim().Equals("あり"))
                                    dt.Rows[j]["IsDelivery"] = "1";
                                else dt.Rows[j]["IsDelivery"] = "0";
                                dt.Rows[j]["Shop_ID"] = lblShopID.Text;

                                dt.Rows[j]["Shop_Name"] = lblShopName.Text;
                                dt.Rows[j]["Ctrl_ID"] = lblCtrl.Text;
                                dt.Rows[j]["Brand_Name"] = lblBrandName.Text;
                                dt.Rows[j]["ID"] = lblID.Text;

                                j++;
                            }
                        }

                        if (dt.Rows.Count > 0)
                        {
                            gvview.DataSource = dt;
                            Cache.Insert("dtSetting", dt);
                            gvview.DataBind();

                            gvview.Columns[0].Visible = false;
                            btncancelall.Visible = false;
                            btnselectall.Visible = false;
                            btnsearch.Visible = false;
                            btnsetting.Visible = false;
                            ddlpage.Visible = false;
                            gp.Visible = false;
                            confirm.Visible = true;
                            ddlpage.Visible = false;
                            b1.Visible = false;
                            b2.Visible = false;
                            b3.Visible = false;
                            title1.Visible = false;
                            title2.Visible = false;
                            btnSave.Text = "確定";
                        }
                        else
                        {
                            GlobalUI.MessageBox("This is no item selected!");
                        }
                    }//check confirm

                else if (btnSave.Text == "確定")
                    {
                        int promotinid = 0;

                        DataTable dtInsert = Cache["dtSetting"] as DataTable;

                        if (dtInsert != null && dtInsert.Rows.Count > 0)
                        {
                            promotinid = pdbl.Save(dtInsert);
                            Cache.Remove("dtSetting");
                            //GlobalUI.MessageBox("Save Success!");
                            btncancelall.Visible = true;
                            btnselectall.Visible = true;
                            btnsearch.Visible = true;
                            btnsetting.Visible = true;
                            btnSave.Visible = true;
                            ddlpage.Visible = true;
                            title1.Visible = true;
                            title2.Visible = true;
                            b1.Visible = true;
                            b2.Visible = true;
                            b3.Visible = true;
                            confirm.Visible = false;
                            gvview.Columns[0].Visible = true;
                            btnSave.Text = "確認画面へ";

                            object referrer = ViewState["UrlReferrer"];
                            string url = "Promotion_Delivery.aspx";
                            string script = "window.onload = function(){alert ('";
                            script += "Save Success";
                            script += "');";
                            script += "window.location = '";
                            script += url;
                            script += "'; }";
                            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true); 
                        }
                    }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblshop = (Label)e.Row.FindControl("lblCtrlID");
                    HtmlGenericControl pWait = e.Row.FindControl("PWait") as HtmlGenericControl;
                    HtmlGenericControl pOk = e.Row.FindControl("POk") as HtmlGenericControl;
                    HtmlGenericControl pDel = e.Row.FindControl("PDel") as HtmlGenericControl;
                    switch (lblshop.Text)
                    {
                        case "n":
                            {
                                pWait.Visible = true;
                                pOk.Visible = false;
                                pDel.Visible = false;
                                break;
                            }
                        case "u":
                            {
                                pWait.Visible = false;
                                pOk.Visible = true;
                                pDel.Visible = false;
                                break;
                            }
                        case "d":
                            {
                                pWait.Visible = false;
                                pOk.Visible = false;
                                pDel.Visible = true;
                                break;
                            }
                    }
                    Label lblDel = (Label)e.Row.FindControl("lblIsDelivery");
                    if (lblDel.Text == "True" || lblDel.Text.Equals("1"))
                    {
                        lblDel.Text = "あり";
                    }
                    else if (lblDel.Text == "False" || lblDel.Text.Equals("0"))
                    {
                        lblDel.Text = "なし";
                    }

                    Label lblDstartDate = e.Row.FindControl("lblStartDate") as Label;
                    Label lblDendDate = e.Row.FindControl("lblEndDate") as Label;
                    Label lblPeriod = e.Row.FindControl("lblHeldPeriod") as Label;
                    Label lblStartTime = e.Row.FindControl("lblStartTime") as Label;
                    Label lblEndTime = e.Row.FindControl("lblEndTime") as Label;

                    if (!String.IsNullOrWhiteSpace(lblDstartDate.Text) || !String.IsNullOrWhiteSpace(lblDendDate.Text))
                    {
                        lblPeriod.Text = lblDstartDate.Text + " " + lblStartTime.Text + "～" + lblDendDate.Text + " " + lblEndTime.Text;
                        lblPeriod.Text = lblPeriod.Text.Replace("/", "-");
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvview.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
                if (gvview.Rows.Count > 0)
                {
                    AllSearchData();
                }
                else
                {
                    int pageSize = Convert.ToInt32(ddlpage.SelectedItem.Text);
                    gp.CalculatePaging(0, pageSize, 1);
                }
                    
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected Promotion_Delivery_Entity Clear()
        {
            Promotion_Delivery_Entity pde = new Promotion_Delivery_Entity();
            try
            {
                string shopnames = null;
                pde.Itemcode = null;
                pde.Itemname = null;
                pde.Brandname = null;
                pde.Shopnmae = shopnames;
                pde.Rperiodfrom = null;
                pde.Rperiodto = null;
                pde.Shopnmae = null;
                txtRperiodfrom.Text = null;
                txtRperiodto.Text = null;
                return pde;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return pde;
            }

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtRperiodfrom.Text = String.Empty;
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
                txtRperiodto.Text = String.Empty;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}