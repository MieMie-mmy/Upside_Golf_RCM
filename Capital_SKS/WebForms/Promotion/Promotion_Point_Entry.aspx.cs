/* 
Created By              : Kay Thi Aung
Created Date          : 03/11/2015
Updated By             :
Updated Date         :

 Tables using           : Item_Master,Item_Shop,Promotion_Point
    -
    -
    -
 * Storedprocedure using:SP_Promotion_Point_LikeSearch
 *                                           -SP_Promotion_Point_EqualSearch
 *                                           -SP_Promotion_Point_SaveUpdate
 *                                           -
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
using System.Timers;

namespace ORS_RCM.WebForms.Promotion
{
    public partial class Promotion_Point_Entry : System.Web.UI.Page
    {
        Promotion_Point_BL pbl;
        Shop_BL shopBL;
        Promotion_Point_Entity ppime;
        
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
           try
           {
               txtitemcode.Attributes.Add("onKeyPress", "doClick('" + btnsearch.ClientID + "',event)");
            if (!IsPostBack)
            {
                pbl = new Promotion_Point_BL();
                if (Request.QueryString["Item_Code"] != null)
                {
                    AllSearchData();
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
                String ctrl = getPostBackControlName();
                if (ctrl.Contains("lnkPaging"))
                {
                    
                    gp.LinkButtonClick(ctrl, gvview.PageSize);

                    Label lbl = gp.FindControl("lblCurrent") as Label;
                    int index = Convert.ToInt32(lbl.Text);
                  
                    SearchDataPaging(index - 1);
                    //dt = SearchData(index - 1);
                   // gvview.DataSource = dt;
                   // gvview.DataBind();
                }
                
                BindDatetime();
            }
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
                if (!string.IsNullOrWhiteSpace(Request.Form[txtRakupointPeriod.UniqueID])) 
                    txtRakupointPeriod.Text = Request.Form[txtRakupointPeriod.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtRakupointPeriod2.UniqueID]))
                    txtRakupointPeriod2.Text = Request.Form[txtRakupointPeriod2.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtyahooperiod.UniqueID]))
                    txtyahooperiod.Text = Request.Form[txtyahooperiod.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtyahooperiod2.UniqueID]))
                    txtyahooperiod2.Text = Request.Form[txtyahooperiod2.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtponpareperiod.UniqueID]))
                    txtponpareperiod.Text = Request.Form[txtponpareperiod.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtponpareperiod2.UniqueID]))
                    txtponpareperiod2.Text = Request.Form[txtponpareperiod2.UniqueID];
                //Save
                if (!string.IsNullOrWhiteSpace(Request.Form[txtRperiodfrom.UniqueID]))
                    txtRperiodfrom.Text = Request.Form[txtRperiodfrom.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtRperiodto.UniqueID]))
                    txtRperiodto.Text = Request.Form[txtRperiodto.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtYperiodfrom.UniqueID]))
                    txtYperiodfrom.Text = Request.Form[txtYperiodfrom.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtYperiodto.UniqueID]))
                    txtYperiodto.Text = Request.Form[txtYperiodto.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtPperiodfrom.UniqueID]))
                    txtPperiodfrom.Text = Request.Form[txtPperiodfrom.UniqueID];
                if (!string.IsNullOrWhiteSpace(Request.Form[txtPperiodto.UniqueID]))
                    txtPperiodto.Text = Request.Form[txtPperiodto.UniqueID];
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
                //listshop.Items.Insert(0, "");
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public Promotion_Point_Entity GetEntity() 
        {
            try 
            {
                string shopnames = null;
                Promotion_Point_Entity ppime = new Promotion_Point_Entity();
                if (!String.IsNullOrWhiteSpace(txtitemcode.Text.Trim()))
                    ppime.Itemcode = txtitemcode.Text.Trim();
                else
                    ppime.Itemcode = null;
                if (!String.IsNullOrWhiteSpace(txtitemname.Text.Trim()))
                    ppime.Itemname = txtitemname.Text.Trim();
                else
                    ppime.Itemname = null;
                if (!String.IsNullOrWhiteSpace(txtyear.Text.Trim()))
                    ppime.Year = txtyear.Text.Trim();
                else
                    ppime.Year = null;
                if (!String.IsNullOrWhiteSpace(txtbrandname.Text.Trim()))
                    ppime.Brandname = txtbrandname.Text.Trim();
                else
                    ppime.Brandname = null;
                if (!String.IsNullOrWhiteSpace(txtseason.Text.Trim()))
                    ppime.Season = txtseason.Text.Trim();
                else
                    ppime.Season = null;
                if (!String.IsNullOrWhiteSpace(txtcopmpetition.Text.Trim()))
                    ppime.Competationname = txtcopmpetition.Text.Trim();
                else
                    ppime.Competationname = null;
                if (!String.IsNullOrWhiteSpace(txtinstructionno.Text.Trim()))
                    ppime.InstructionNo = txtinstructionno.Text.Trim();
                else
                    ppime.InstructionNo = null;
                if (!String.IsNullOrWhiteSpace(txtclassification.Text.Trim()))
                    ppime.Claffication = txtclassification.Text.Trim();
                else
                    ppime.Claffication = null;

                for (int i = 0; i < listshop.Items.Count; i++)
                {
                    if (listshop.Items[i].Selected)
                    {
                        //dtshop.Rows.Add(promotionID, int.Parse(listRshop.Items[i].Value.ToString()));
                        shopnames += listshop.Items[i].Value.ToString() + ",";
                    }
                }
               // if (!String.IsNullOrWhiteSpace(listshop.SelectedItem.ToString()))
                   // ppime.Shopnmae = listshop.SelectedItem.ToString();
                ppime.Shopnmae = shopnames;

                string fromDate = Request.Form[txtRakupointPeriod.UniqueID];
                string toDate = Request.Form[txtRakupointPeriod2.UniqueID];
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
                ppime.Rperiodfrom = FromDate;
                ppime.Rperiodto = ToDate;

                string fromDatey = Request.Form[txtyahooperiod.UniqueID];
                string toDatey = Request.Form[txtyahooperiod2.UniqueID];
                DateTime? FromDatey = new DateTime();
                DateTime? ToDatey = new DateTime();
                if (!String.IsNullOrEmpty(fromDatey))
                {
                    FromDatey = DateConverter(fromDatey);
                }
                else
                {
                    FromDatey = null;
                }
                if (!String.IsNullOrEmpty(toDatey))
                {
                    ToDatey = DateConverter(toDatey);
                }
                else
                {
                    ToDatey = null;
                }
                ppime.Yperiodfrom = FromDatey;
                ppime.Yperiodto = ToDatey;

                string fromDatep = Request.Form[txtponpareperiod.UniqueID];
                string toDatep = Request.Form[txtponpareperiod2.UniqueID];
                DateTime? FromDatep = new DateTime();
                DateTime? ToDatep = new DateTime();
                if (!String.IsNullOrEmpty(fromDatep))
                {
                    FromDatep = DateConverter(fromDatep);
                }
                else
                {
                    FromDatep = null;
                }
                if (!String.IsNullOrEmpty(toDatep))
                {
                    ToDatep = DateConverter(toDatep);
                }
                else
                {
                    ToDatep = null;
                }
                ppime.Pperiodfrom = FromDatep;
                ppime.Pperiodto = ToDatep;

                if (!String.IsNullOrWhiteSpace(ddlRakumgpoint.SelectedValue.ToString()))
                    ppime.RP = Int32.Parse(ddlRakumgpoint.SelectedValue);
                else
                    ppime.RP = 0;

                if (!String.IsNullOrWhiteSpace(ddlyahoomgpoint.SelectedValue.ToString()))
                    ppime.YP = Int32.Parse(ddlyahoomgpoint.SelectedValue);
                else
                    ppime.YP = 0;

                if (!String.IsNullOrWhiteSpace(ddlponparemgpoint.SelectedValue.ToString()))
                    ppime.PP = Int32.Parse(ddlponparemgpoint.SelectedValue);
                else
                    ppime.PP = 0;

                ppime.Shopstatus = ddlshopstatus.SelectedValue;

                return ppime;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new Promotion_Point_Entity();
            }
           
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
                pbl = new Promotion_Point_BL();
              //  Promotion_Point_Entity ppime = GetEntity();
                DataTable dt = new DataTable();
                if (!String.IsNullOrWhiteSpace(hfcheckvalue.Value))
                {

                    Selectcheckdataall(1);

                }
                else
                {
                    Searchdatas();
                    #region
                    //int page_size = Convert.ToInt32(ddlpage.SelectedValue.ToString());
                    //if (Request.QueryString["Item_Code"] != null)
                    //{
                    //    ppime.Shopnmae = Request.QueryString["Shop_ID"] + ',';
                    //    ppime.Itemcode = Request.QueryString["Item_Code"].ToString();
                    //    dt = pbl.SelectallDataEqual(ppime, 0, 1, page_size);
                    //}
                    //else
                    //{
                    //    if (chkcheck.Checked && !String.IsNullOrWhiteSpace(txtitemcode.Text.Trim()))
                    //    {
                    //        //dt = pbl.SelectallData(ppime, 0);//equal
                    //        dt = pbl.SelectallDataEqual(ppime, 0, 1, page_size);
                    //    }
                    //    else
                    //    {
                    //        dt = pbl.SelectallData(ppime, 1, 1, page_size);

                    //    }
                    //}
                    //if (dt != null && dt.Rows.Count > 0)
                    //{

                    //    int count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                    //    gvview.PageSize = int.Parse(ddlpage.SelectedValue.ToString());

                    //    DataTable dts = Search(dt);
                    //    gvview.DataSource = dts;
                    //    gvview.DataBind();
                    //    gp.CalculatePaging(count, gvview.PageSize, 1);
                    //}
                    //else
                    //{
                    //    gvview.DataSource = dt;
                    //    gvview.DataBind();
                    //    gp.Visible = false;
                    //}
                #endregion
                }
            }//
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        
        }

        //updated date 20/07/2015
        protected void Searchdatas() 
        {
            try 
            {
                pbl = new Promotion_Point_BL();
                Promotion_Point_Entity ppime = GetEntity();
                DataTable dt = new DataTable();
                int page_size = Convert.ToInt32(ddlpage.SelectedValue.ToString());
                if (Request.QueryString["Item_Code"] != null)
                {
                    ppime.Shopnmae = Request.QueryString["Shop_ID"] + ',';
                    ppime.Itemcode = Request.QueryString["Item_Code"].ToString();
                    dt = pbl.SelectallDataEqual(ppime, 0, 1, page_size);
                }
                else
                {
                    if (chkcheck.Checked && !String.IsNullOrWhiteSpace(txtitemcode.Text.Trim()))
                    {
                        //dt = pbl.SelectallData(ppime, 0);//equal
                        dt = pbl.SelectallDataEqual(ppime, 0, 1, page_size);
                    }
                    else
                    {
                        dt = pbl.SelectallData(ppime, 1, 1, page_size);

                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {

                    int count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                    gvview.PageSize = int.Parse(ddlpage.SelectedValue.ToString());

                    DataTable dts = Search(dt);
                    gvview.DataSource = dts;
                    gvview.DataBind();
                    gp.CalculatePaging(count, gvview.PageSize, 1);
                }
                else
                {
                    gvview.DataSource = dt;
                    gvview.DataBind();
                    gp.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }
        protected DataTable Search(DataTable dt) 
        {
            try
            {
                string str = null; string sty = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string y = dt.Rows[i]["Mall_ID"].ToString();
                    switch (y)
                    {
                        case "1":
                            {
                                if (dt.Rows[i]["PShopID"].ToString() == dt.Rows[i]["ShopID"].ToString())
                                {
                                    if (!String.IsNullOrWhiteSpace(dt.Rows[i]["Rakuten_StartDate"].ToString()))
                                        str = dt.Rows[i]["Rakuten_StartDate"].ToString().Substring(0, 10);
                                    if (!String.IsNullOrWhiteSpace(dt.Rows[i]["Rakuten_EndDate"].ToString()))
                                        sty = dt.Rows[i]["Rakuten_EndDate"].ToString().Substring(0, 10);

                                    dt.Rows[i]["Point_magnification"] = dt.Rows[i]["Rakuten_Point"].ToString();
                                    //   dt.Rows[i]["Point_period"] = dt.Rows[i]["Rakuten_StartDate"].ToString() + dt.Rows[i]["Rakuten_StartTime"].ToString() + "～" + dt.Rows[i]["Rakuten_EndDate"].ToString() + dt.Rows[i]["Rakuten_EndTime"].ToString();
                                    dt.Rows[i]["Point_period"] = str + " " + dt.Rows[i]["Rakuten_StartTime"].ToString() + "～" + sty + " " + dt.Rows[i]["Rakuten_EndTime"].ToString();
                                }
                                break;
                            }
                        case "2":
                            {
                                if (dt.Rows[i]["PShopID"].ToString() == dt.Rows[i]["ShopID"].ToString())
                                {
                                    if (!String.IsNullOrWhiteSpace(dt.Rows[i]["Yahoo_StartDate"].ToString()))
                                        str = dt.Rows[i]["Yahoo_StartDate"].ToString().Substring(0, 10);
                                    if (!String.IsNullOrWhiteSpace(dt.Rows[i]["Yahoo_EndDate"].ToString()))
                                        sty = dt.Rows[i]["Yahoo_EndDate"].ToString().Substring(0, 10);

                                    dt.Rows[i]["Point_magnification"] = dt.Rows[i]["Yahoo_Point"].ToString();
                                    dt.Rows[i]["Point_period"] = str + " " + dt.Rows[i]["Yahoo_StartTime"].ToString() + "～" + sty + " " + dt.Rows[i]["Yahoo_EndTime"].ToString();
                                    // dt.Rows[i]["Point_period"] = dt.Rows[i]["Yahoo_StartDate"].ToString() + dt.Rows[i]["Yahoo_StartTime"].ToString() + "～" + dt.Rows[i]["Yahoo_EndDate"].ToString() + dt.Rows[i]["Yahoo_EndTime"].ToString();
                                }
                                break;
                            }
                        case "3":
                            {
                                if (dt.Rows[i]["PShopID"].ToString() == dt.Rows[i]["ShopID"].ToString())
                                {
                                    if (!String.IsNullOrWhiteSpace(dt.Rows[i]["Ponpare_StartDate"].ToString()))
                                        str = dt.Rows[i]["Ponpare_StartDate"].ToString().Substring(0, 10);
                                    if (!String.IsNullOrWhiteSpace(dt.Rows[i]["Ponpare_EndDate"].ToString()))
                                        sty = dt.Rows[i]["Ponpare_EndDate"].ToString().Substring(0, 10);

                                    dt.Rows[i]["Point_magnification"] = dt.Rows[i]["Ponpare_Point"].ToString();
                                    dt.Rows[i]["Point_period"] = str + " " + dt.Rows[i]["Ponpare_StartTime"].ToString() + "～" + sty + " " + dt.Rows[i]["Ponpare_EndTime"].ToString();
                                    //   dt.Rows[i]["Point_period"] = dt.Rows[i]["Ponpare_StartDate"].ToString() + dt.Rows[i]["Ponpare_StartTime"].ToString() + "～" + dt.Rows[i]["Ponpare_EndDate"].ToString() + dt.Rows[i]["Ponpare_EndTime"].ToString();
                                }
                                break;
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

        protected void btnselectall_Click(object sender, EventArgs e)
        {
            try
            {
            
                //pbl = new Promotion_Point_BL();
                //Promotion_Point_Entity ppime = GetEntity();
                //DataTable dt = new DataTable();
                //if (chkcheck.Checked && !String.IsNullOrWhiteSpace(txtitemcode.Text.Trim()))
                //{
                  
                //    dt = pbl.SelectallDataCheckbox(ppime, 0);
                //}
                //else
                //{
                //    dt = pbl.SelectallDataCheckbox(ppime, 1);

                //}

                //    //ArrayList arrCheckValue = new ArrayList();

                //    //for (int i = 0; i < dt.Rows.Count; i++)
                //    //{
                //    //    if (dt.Columns.Contains("ID"))
                //    //        arrCheckValue.Add(dt.Rows[i]["ID"].ToString());
                       
                //    //    CheckBox chkbox = (CheckBox)gvview.FindControl("chktype");
                //    //    chkbox.Checked = true;
                      
                //    //}

                //gvview.DataSource = dt;
                //gvview.DataBind();
                //int y = Convert.ToInt32(gvview.Rows.Count);
                //for (int i = 0; i < y; i++)
                //{
                //    CheckBox chk = gvview.Rows[i].FindControl("chktype") as CheckBox;
                //    chk.Checked = true;
                //}
                //    //foreach (GridViewRow row in gvview.Rows)
                //    //{
                //    //    if (row.RowType == DataControlRowType.DataRow)
                //    //    {
                //    //        CheckBox c = (CheckBox)row.FindControl("chktype");
                //    //        c.Checked = true;
                //    //    }
                //    //}

                #region
                hfsetting.Value = null;
                hfcheckvalue.Value = null;
                hfcheckvalue.Value = "1";
                Selectcheckdataall(1);


                for (int i = 0; i < gvview.Rows.Count; i++)
                {
                    CheckBox chk = gvview.Rows[i].FindControl("chktype") as CheckBox;
                    chk.Checked = true;
                }
                #endregion
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
                ViewState["dt"] = DBNull.Value;//updated date 20/07/2015
                hfsetting.Value = null;//updated date 20/07/2015
                ViewState["chkValue"] = null;
                ViewState["shopid"] = null;
                hfcheckvalue.Value = null;
                ViewState["checkedValue"] = null;

                Searchdatas();//updated date 20/07/2015
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

        protected void Selectcheckdataall(int pindex) 
        {
            ArrayList arrlst = new ArrayList();
            ArrayList shopid = new ArrayList();
            pbl = new Promotion_Point_BL();
            Promotion_Point_Entity ppime = GetEntity();
            DataTable dt = new DataTable();
            DataTable dts = new DataTable();
            DataTable dtcheck = new DataTable();
            if (chkcheck.Checked && !String.IsNullOrWhiteSpace(txtitemcode.Text.Trim()))
            {

                dt = pbl.SelectallDataCheckbox(ppime, 0);
            }
            else
            {
                dt = pbl.SelectallDataCheckbox(ppime, 1);

            }
             if(dt != null && dt.Rows.Count >0 )
             {
                 dt.Columns.Add("Check",typeof(int));
            #region
            if (ViewState["chkValue"] != null && !String.IsNullOrWhiteSpace(hfsetting.Value) && hfsetting.Value.Equals("1"))
            {
                dtcheck.Rows.Clear();
                ArrayList archeck = ViewState["chkValue"] as ArrayList;
                for (int y = 0; y < dt.Rows.Count; y++)
                {
                   string id =dt.Rows[y]["ID"].ToString()+"$"+dt.Rows[y]["ShopID"].ToString();
                 
                   if (archeck.Contains(id))
                   {
                       dt.Rows[y]["Check"] = 1;//check
                   }
                    else
                       dt.Rows[y]["Check"] = 0; //uncheck
                    String lblMallID = dt.Rows[y]["Mall_ID"].ToString();
                    if (lblMallID.Equals("1"))
                    {
                        if (!String.IsNullOrWhiteSpace(ddlRpoint.SelectedItem.Text))
                        {
                            String strdateFrom = txtRperiodfrom.Text + " " + ddlRPeriodFromHour.SelectedItem.Text + ":" + ddlRPeriodFromMinute.SelectedItem.Text;
                            DateTime dtFrom = Convert.ToDateTime(strdateFrom);
                            DateTime dtCheck = DateTime.Now.AddHours(3);
                            TimeSpan different = dtCheck - dtFrom;
                            String[] diff = different.ToString().Split(':');
                            double difhour = Convert.ToDouble(diff[0]);
                            if (difhour > 3)
                            {
                                GlobalUI.MessageBox("開始期間は3時間以上先の時刻を設定してください");
                                return;
                            }

                            String strdateTo = txtRperiodto.Text + " " + ddlRperiodtohour.SelectedItem.Text + ":" + ddlRPeriodToMinute.SelectedItem.Text;
                            DateTime dtTo = Convert.ToDateTime(strdateTo);

                            if (dtTo < dtFrom)
                            {
                                GlobalUI.MessageBox("開始期間と終了期間に誤りがあります。");
                                return;
                            }
                            //updated date 15/07/2015 for 24 hours or 60 days
                            TimeSpan? tdiff = dtTo - dtFrom;
                            double difhours = tdiff.Value.TotalHours;
                            if (difhours < 24 || difhours > 1440)
                            {
                                GlobalUI.MessageBox("ポイント変倍率適用期間の期間を、最短24時間以上から最長60日以内の範囲で指定してください。");
                                return;
                            }

                            if (dt.Rows[y]["Check"].ToString() != "0")//check
                            {
                                dt.Rows[y]["Rakuten_Point"] = ddlRpoint.SelectedItem.Text;
                                dt.Rows[y]["Point_magnification"] = ddlRpoint.SelectedItem.Text;
                                string date = strdateFrom + "～" + strdateTo;
                                dt.Rows[y]["Point_period"] = date.Replace("/", "-");

                                dt.AcceptChanges();
                            }

                        }
                    }
                    else if (lblMallID.Equals("2"))
                    {
                        if (!String.IsNullOrWhiteSpace(ddlYpoint.SelectedItem.Text))
                        {
                            String strdateFrom = txtYperiodfrom.Text + " " + ddlYperiodfromhour.SelectedItem.Text + ":" + ddlYperiodFromMinute.SelectedItem.Text;
                            DateTime dtFrom = Convert.ToDateTime(strdateFrom);
                            DateTime dtCheck = DateTime.Now.AddHours(3);
                            TimeSpan different = dtCheck - dtFrom;
                            String[] diff = different.ToString().Split(':');
                            double difhour = Convert.ToDouble(diff[0]);
                            if (difhour > 3)
                            {
                                GlobalUI.MessageBox("開始期間は3時間以上先の時刻を設定してください");
                                return;
                            }

                            String strdateTo = txtYperiodto.Text + " " + ddlYperiodtohour.SelectedItem.Text + ":" + ddlYperiodToMinute.SelectedItem.Text;
                            DateTime dtTo = Convert.ToDateTime(strdateTo);

                            if (dtTo < dtFrom)
                            {
                                GlobalUI.MessageBox("開始期間と終了期間に誤りがあります。");
                                return;
                            }

                            ////updated date 15/07/2015 for 24 hours or 60 days
                            //TimeSpan? tdiff = dtTo - dtFrom;
                            //double difhours = tdiff.Value.TotalHours;
                            //if (difhours < 24 || difhours > 1440)
                            //{
                            //    GlobalUI.MessageBox("ポイント変倍率適用期間の期間を、最短24時間以上から最長60日以内の範囲で指定してください。");
                            //    return;
                            //}

                            if (dt.Rows[y]["Check"].ToString() != "0")//check
                            {
                                dt.Rows[y]["Yahoo_Point"] = ddlYpoint.SelectedItem.Text;
                                dt.Rows[y]["Point_magnification"] = ddlYpoint.SelectedItem.Text;
                                string date = strdateFrom + "～" + strdateTo;
                                dt.Rows[y]["Point_period"] = date.Replace("/", "-");
                                dt.AcceptChanges();
                            }
                        }
                    }
                    else if (lblMallID.Equals("3"))
                    {
                        if (!String.IsNullOrWhiteSpace(ddlPpoint.SelectedItem.Text))
                        {
                            String strdateFrom = txtPperiodfrom.Text + " " + ddlPperiodfromhour.SelectedItem.Text + ":" + ddlPperiodfromMinute.SelectedItem.Text;
                            DateTime dtFrom = Convert.ToDateTime(strdateFrom);
                            DateTime dtCheck = DateTime.Now.AddHours(3);
                            TimeSpan different = dtCheck - dtFrom;
                            String[] diff = different.ToString().Split(':');
                            double difhour = Convert.ToDouble(diff[0]);
                            if (difhour > 3)
                            {
                                GlobalUI.MessageBox("開始期間は3時間以上先の時刻を設定してください");
                                return;
                            }

                            String strdateTo = txtPperiodto.Text + " " + ddlPPeriodtohour.SelectedItem.Text + ":" + ddlPperiodToMinute.SelectedItem.Text;
                            DateTime dtTo = Convert.ToDateTime(strdateTo);

                            if (dtTo < dtFrom)
                            {
                                GlobalUI.MessageBox("開始期間と終了期間に誤りがあります。");
                                return;
                            }

                            ////updated date 15/07/2015 for 24 hours or 60 days
                            //TimeSpan? tdiff = dtTo - dtFrom;
                            //double difhours = tdiff.Value.TotalHours;
                            //if (difhours < 24 || difhours > 1440)
                            //{
                            //    GlobalUI.MessageBox("ポイント変倍率適用期間の期間を、最短24時間以上から最長60日以内の範囲で指定してください。");
                            //    return;
                            //}

                            if (dt.Rows[y]["Check"].ToString() != "0")//check
                            {
                                dt.Rows[y]["Ponpare_Point"] = ddlPpoint.SelectedItem.Text;
                                dt.Rows[y]["Point_magnification"] = ddlPpoint.SelectedItem.Text;
                                string date = strdateFrom + "～" + strdateTo;
                                dt.Rows[y]["Point_period"] = date.Replace("/", "-");
                                dt.AcceptChanges();
                            }
                        }

                    }

                   

                    
                  
                    // ViewState["chkValue"] = arrlst;
                }
                DataRow[] dr = dt.Select("Check=1");
                if (dr.Count() > 0)
                {
                    dts = dt.Select("Check=1").CopyToDataTable();
                    dtcheck.Merge(dts);
                }
                ViewState["dt"] = null;
                ViewState["dt"] = dtcheck;
                if (hfpageno.Value == "1")
                {
                    int count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                    gvview.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    gp.CalculatePaging(count, gvview.PageSize, 1);
                    hfpageno.Value = "0";
                }
                dt = Calculatepaging(dt, pindex);
                gvview.DataSource = dt;
                gvview.DataBind();
            }
            #endregion
#region old updated date 20/07/2015
            //else if(!String.IsNullOrWhiteSpace(hfsetting.Value) && hfsetting.Value.Equals("1"))
            //{
            //    for (int y = 0; y < dt.Rows.Count; y++)
            //    {
            //        string unitcode = dt.Rows[y]["ID"].ToString() + "$" + dt.Rows[y]["ShopID"].ToString();
            //        arrlst.Add(unitcode);
                    
            //        dt.Rows[y]["Check"] = 1;//check
            //        String lblMallID = dt.Rows[y]["Mall_ID"].ToString();
            //        if (lblMallID.Equals("1"))
            //        {
            //            if (!String.IsNullOrWhiteSpace(ddlRpoint.SelectedItem.Text))
            //            {
            //                String strdateFrom = txtRperiodfrom.Text + " " + ddlRPeriodFromHour.SelectedItem.Text + ":" + ddlRPeriodFromMinute.SelectedItem.Text;
            //                DateTime dtFrom = Convert.ToDateTime(strdateFrom);
            //                DateTime dtCheck = DateTime.Now.AddHours(3);
            //                TimeSpan different = dtCheck - dtFrom;
            //                String[] diff = different.ToString().Split(':');
            //                double difhour = Convert.ToDouble(diff[0]);
            //                if (difhour > 3)
            //                {
            //                    GlobalUI.MessageBox("開始期間は3時間以上先の時刻を設定してください");
            //                    return;
            //                }

            //                String strdateTo = txtRperiodto.Text + " " + ddlRperiodtohour.SelectedItem.Text + ":" + ddlRPeriodToMinute.SelectedItem.Text;
            //                DateTime dtTo = Convert.ToDateTime(strdateTo);

            //                if (dtTo < dtFrom)
            //                {
            //                    GlobalUI.MessageBox("開始期間と終了期間に誤りがあります。");
            //                    return;
            //                }
            //                //updated date 15/07/2015 for 24 hours or 60 days
            //                TimeSpan? tdiff = dtTo - dtFrom;
            //                double difhours = tdiff.Value.TotalHours;
            //                if (difhours < 24 || difhours > 1440)
            //                {
            //                    GlobalUI.MessageBox("ポイント変倍率適用期間の期間を、最短24時間以上から最長60日以内の範囲で指定してください。");
            //                    return;
            //                }

            //                dt.Rows[y]["Point_magnification"] = ddlRpoint.SelectedItem.Text;
            //                string date = strdateFrom + "～" + strdateTo;
            //                dt.Rows[y]["Point_period"] = date.Replace("/", "-");

            //                dt.AcceptChanges();


            //            }
            //        }
            //        else if (lblMallID.Equals("2"))
            //        {
            //            if (!String.IsNullOrWhiteSpace(ddlYpoint.SelectedItem.Text))
            //            {
            //                String strdateFrom = txtYperiodfrom.Text + " " + ddlYperiodfromhour.SelectedItem.Text + ":" + ddlYperiodFromMinute.SelectedItem.Text;
            //                DateTime dtFrom = Convert.ToDateTime(strdateFrom);
            //                DateTime dtCheck = DateTime.Now.AddHours(3);
            //                TimeSpan different = dtCheck - dtFrom;
            //                String[] diff = different.ToString().Split(':');
            //                double difhour = Convert.ToDouble(diff[0]);
            //                if (difhour > 3)
            //                {
            //                    GlobalUI.MessageBox("開始期間は3時間以上先の時刻を設定してください");
            //                    return;
            //                }

            //                String strdateTo = txtYperiodto.Text + " " + ddlYperiodtohour.SelectedItem.Text + ":" + ddlYperiodToMinute.SelectedItem.Text;
            //                DateTime dtTo = Convert.ToDateTime(strdateTo);

            //                if (dtTo < dtFrom)
            //                {
            //                    GlobalUI.MessageBox("開始期間と終了期間に誤りがあります。");
            //                    return;
            //                }

            //                //updated date 15/07/2015 for 24 hours or 60 days
            //                TimeSpan? tdiff = dtTo - dtFrom;
            //                double difhours = tdiff.Value.TotalHours;
            //                if (difhours < 24 || difhours > 1440)
            //                {
            //                    GlobalUI.MessageBox("ポイント変倍率適用期間の期間を、最短24時間以上から最長60日以内の範囲で指定してください。");
            //                    return;
            //                }

            //                dt.Rows[y]["Point_magnification"] = ddlYpoint.SelectedItem.Text;
            //                string date = strdateFrom + "～" + strdateTo;
            //                dt.Rows[y]["Point_period"] = date.Replace("/", "-");
            //                dt.AcceptChanges();
            //            }
            //        }
            //        else if (lblMallID.Equals("3"))
            //        {
            //            if (!String.IsNullOrWhiteSpace(ddlPpoint.SelectedItem.Text))
            //            {
            //                String strdateFrom = txtPperiodfrom.Text + " " + ddlPperiodfromhour.SelectedItem.Text + ":" + ddlPperiodfromMinute.SelectedItem.Text;
            //                DateTime dtFrom = Convert.ToDateTime(strdateFrom);
            //                DateTime dtCheck = DateTime.Now.AddHours(3);
            //                TimeSpan different = dtCheck - dtFrom;
            //                String[] diff = different.ToString().Split(':');
            //                double difhour = Convert.ToDouble(diff[0]);
            //                if (difhour > 3)
            //                {
            //                    GlobalUI.MessageBox("開始期間は3時間以上先の時刻を設定してください");
            //                    return;
            //                }

            //                String strdateTo = txtPperiodto.Text + " " + ddlPPeriodtohour.SelectedItem.Text + ":" + ddlPperiodToMinute.SelectedItem.Text;
            //                DateTime dtTo = Convert.ToDateTime(strdateTo);

            //                if (dtTo < dtFrom)
            //                {
            //                    GlobalUI.MessageBox("開始期間と終了期間に誤りがあります。");
            //                    return;
            //                }

            //                //updated date 15/07/2015 for 24 hours or 60 days
            //                TimeSpan? tdiff = dtTo - dtFrom;
            //                double difhours = tdiff.Value.TotalHours;
            //                if (difhours < 24 || difhours > 1440)
            //                {
            //                    GlobalUI.MessageBox("ポイント変倍率適用期間の期間を、最短24時間以上から最長60日以内の範囲で指定してください。");
            //                    return;
            //                }

            //                dt.Rows[y]["Point_magnification"] = ddlPpoint.SelectedItem.Text;
            //                string date = strdateFrom + "～" + strdateTo;
            //                dt.Rows[y]["Point_period"] = date.Replace("/", "-");
            //                dt.AcceptChanges();
            //            }

            //        }
                    
            //    }//
            //    ViewState["dt"] = null;
            //    ViewState["dt"] = dt;
             
            //    gvview.DataSource = dt;

            //    gvview.DataBind();
            //    ViewState["chkValue"] = arrlst;
                  
            //}
    #endregion
                 #region  only check box
            else 
            {
                for (int y = 0; y < dt.Rows.Count; y++)
                {
                    string unitcode = dt.Rows[y]["ID"].ToString() + "$" + dt.Rows[y]["ShopID"].ToString();
                    arrlst.Add(unitcode);

                    dt.Rows[y]["Check"] = 1;//check
                    String lblMallID = dt.Rows[y]["Mall_ID"].ToString();
                    if (lblMallID.Equals("1"))
                    {
                        if (!String.IsNullOrWhiteSpace(ddlRpoint.SelectedItem.Text) || !String.IsNullOrWhiteSpace(txtRperiodfrom.Text) || !String.IsNullOrWhiteSpace(txtRperiodto.Text))
                        {
                            String strdateFrom = txtRperiodfrom.Text + " " + ddlRPeriodFromHour.SelectedItem.Text + ":" + ddlRPeriodFromMinute.SelectedItem.Text;
                            DateTime dtFrom = Convert.ToDateTime(strdateFrom);
                            DateTime dtCheck = DateTime.Now.AddHours(3);
                            TimeSpan different = dtCheck - dtFrom;
                            String[] diff = different.ToString().Split(':');
                            double difhour = Convert.ToDouble(diff[0]);
                            if (difhour > 3)
                            {

                            }

                            String strdateTo = txtRperiodto.Text + " " + ddlRperiodtohour.SelectedItem.Text + ":" + ddlRPeriodToMinute.SelectedItem.Text;
                            DateTime dtTo = Convert.ToDateTime(strdateTo);

                            if (dtTo < dtFrom)
                            {

                            }
                            //updated date 15/07/2015 for 24 hours or 60 days
                            TimeSpan? tdiff = dtTo - dtFrom;
                            double difhours = tdiff.Value.TotalHours;
                            if (difhours < 24 || difhours > 1440)
                            {

                            }



                        }
                        else //for backupdata
                        {
                           
                            dt.Rows[y]["Point_magnification"] = dt.Rows[y]["Rakuten_Point"].ToString() ;
                            if (!String.IsNullOrWhiteSpace(dt.Rows[y]["Rakuten_StartDate"].ToString()) || !String.IsNullOrWhiteSpace(dt.Rows[y]["Rakuten_EndDate"].ToString()))
                            {
                                string date = dt.Rows[y]["Rakuten_StartDate"].ToString() + " " + dt.Rows[y]["Rakuten_StartTime"].ToString() + "～" + dt.Rows[y]["Rakuten_EndDate"].ToString() + " " + dt.Rows[y]["Rakuten_EndTime"].ToString();
                            dt.Rows[y]["Point_period"] = date.Replace("/", "-");
                            }
                            dt.AcceptChanges();
                        }
                    }
                    else if (lblMallID.Equals("2"))
                    {
                        if (!String.IsNullOrWhiteSpace(ddlYpoint.SelectedItem.Text) || !String.IsNullOrWhiteSpace(txtYperiodfrom.Text) || !String.IsNullOrWhiteSpace(txtYperiodto.Text))
                        {
                            String strdateFrom = txtYperiodfrom.Text + " " + ddlYperiodfromhour.SelectedItem.Text + ":" + ddlYperiodFromMinute.SelectedItem.Text;
                            DateTime dtFrom = Convert.ToDateTime(strdateFrom);
                            DateTime dtCheck = DateTime.Now.AddHours(3);
                            TimeSpan different = dtCheck - dtFrom;
                            String[] diff = different.ToString().Split(':');
                            double difhour = Convert.ToDouble(diff[0]);
                            if (difhour > 3)
                            {
                              
                            }

                            String strdateTo = txtYperiodto.Text + " " + ddlYperiodtohour.SelectedItem.Text + ":" + ddlYperiodToMinute.SelectedItem.Text;
                            DateTime dtTo = Convert.ToDateTime(strdateTo);

                            if (dtTo < dtFrom)
                            {
                               
                            }

                            //updated date 15/07/2015 for 24 hours or 60 days
                            TimeSpan? tdiff = dtTo - dtFrom;
                            double difhours = tdiff.Value.TotalHours;
                            if (difhours < 24 || difhours > 1440)
                            {
                               
                            }

                        
                        }
                        else //for backupdata
                        {

                            dt.Rows[y]["Point_magnification"] = dt.Rows[y]["Yahoo_Point"].ToString();
                            if (!String.IsNullOrWhiteSpace(dt.Rows[y]["Yahoo_StartDate"].ToString()) || !String.IsNullOrWhiteSpace(dt.Rows[y]["Yahoo_EndDate"].ToString()))
                            {
                                string date = dt.Rows[y]["Yahoo_StartDate"].ToString() + " " + dt.Rows[y]["Yahoo_StartTime"].ToString() + "～" + dt.Rows[y]["Yahoo_EndDate"].ToString() + " " + dt.Rows[y]["Yahoo_EndTime"].ToString();
                                dt.Rows[y]["Point_period"] = date.Replace("/", "-");
                            }
                            dt.AcceptChanges();
                        }
                    }
                    else if (lblMallID.Equals("3") )
                    {
                        if (!String.IsNullOrWhiteSpace(ddlPpoint.SelectedItem.Text) || !String.IsNullOrWhiteSpace(txtPperiodfrom.Text) || !String.IsNullOrWhiteSpace(txtPperiodto.Text))
                        {
                            String strdateFrom = txtPperiodfrom.Text + " " + ddlPperiodfromhour.SelectedItem.Text + ":" + ddlPperiodfromMinute.SelectedItem.Text;
                            DateTime dtFrom = Convert.ToDateTime(strdateFrom);
                            DateTime dtCheck = DateTime.Now.AddHours(3);
                            TimeSpan different = dtCheck - dtFrom;
                            String[] diff = different.ToString().Split(':');
                            double difhour = Convert.ToDouble(diff[0]);
                            if (difhour > 3)
                            {
                             
                            }

                            String strdateTo = txtPperiodto.Text + " " + ddlPPeriodtohour.SelectedItem.Text + ":" + ddlPperiodToMinute.SelectedItem.Text;
                            DateTime dtTo = Convert.ToDateTime(strdateTo);

                            if (dtTo < dtFrom)
                            {
                              
                            }

                            //updated date 15/07/2015 for 24 hours or 60 days
                            TimeSpan? tdiff = dtTo - dtFrom;
                            double difhours = tdiff.Value.TotalHours;
                            if (difhours < 24 || difhours > 1440)
                            {
                                
                            }

                            
                        }
                        else //for backupdata
                        {

                            dt.Rows[y]["Point_magnification"] = dt.Rows[y]["Ponpare_Point"].ToString();
                            if (!String.IsNullOrWhiteSpace(dt.Rows[y]["Ponpare_StartDate"].ToString()) || !String.IsNullOrWhiteSpace(dt.Rows[y]["Ponpare_EndDate"].ToString()))
                            {
                                string date = dt.Rows[y]["Ponpare_StartDate"].ToString() + " " + dt.Rows[y]["Ponpare_StartTime"].ToString() + "～" + dt.Rows[y]["Ponpare_EndDate"].ToString() + " " + dt.Rows[y]["Ponpare_EndTime"].ToString();
                                dt.Rows[y]["Point_period"] = date.Replace("/", "-");
                            }
                            dt.AcceptChanges();
                        }

                    }

                }//
                DataRow[] dr = dt.Select("Check=1");
                if (dr.Count() > 0)
                {
                    dts = dt.Select("Check=1").CopyToDataTable();
                    dtcheck.Merge(dts);
                }
                ViewState["dt"] = null;
                ViewState["dt"] = dtcheck;
                if (hfpageno.Value == "1")
                {
                    int count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                    gvview.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    gp.CalculatePaging(count, gvview.PageSize, 1);
                    hfpageno.Value = "0";
                }
                dt = Calculatepaging(dt, pindex);
                gvview.DataSource = dt;
                gvview.DataBind();
                ViewState["chkValue"] = arrlst;

            }
                 #endregion
             }
          //  ViewState["chkValue"] = null;
            
           
        }
        protected DataTable Calculatepaging(DataTable dt,int index) 
        {
            try
            {
                DataTable dtdata = dt.Clone();
                int page_size = Convert.ToInt32(ddlpage.SelectedValue.ToString());
                int start = (index-1) * page_size + 1;
                int end = (((index - 1) * page_size + 1) + page_size) - 1;
                var rows = dt.AsEnumerable().Skip(start).Take(end);
                for (int rowIndex = start-1; rowIndex < end; rowIndex++)
                {
                    if (rowIndex > dt.Rows.Count-1)
                        break;
                    else
                    dtdata.Rows.Add(dt.Rows[rowIndex].ItemArray);
                }
                return dtdata;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
                
            }
        }
        protected void ckItem_Check(object sender, EventArgs e)//updated date 13/07/2015
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(hfcheckvalue.Value))
                {
                    CheckBox chk = sender as CheckBox;
                    GridViewRow row = chk.NamingContainer as GridViewRow;
                    int rowIndex = row.RowIndex;

                    Label lbl = gvview.Rows[rowIndex].FindControl("lblID") as Label;

                    Label lblshop = gvview.Rows[rowIndex].FindControl("lblShop_ID") as Label;
                    if (ViewState["chkValue"] != null)
                    {
                       // DataTable dt = ViewState["dt"] as DataTable;
                        string unit = lbl.Text + "$" + lblshop.Text;
                        ArrayList arrlst = ViewState["chkValue"] as ArrayList;
                        CheckBox chkHeader = gvview.Rows[rowIndex].FindControl("chktype") as CheckBox;
                        if (!chk.Checked)
                        {
                            //if one of check box is unchecked then header checkbox set to uncheck
                            chkHeader.Checked = false;
                            if (arrlst.Contains(unit))
                            {
                                arrlst.Remove(unit);
                                ViewState["chkValue"] = arrlst;
                               
                            }
                        }
                        else
                        {
                            arrlst.Add(unit);
                            ViewState["chkValue"] = arrlst;
                           
                        }
                    }
                    //else
                    //{
                    //    ArrayList arrlst = new ArrayList();
                    //    arrlst.Add(lbl.Text);
                    //    ViewState["chkValue"] = arrlst;
                    //}

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
                hfsetting.Value = "1";
                if (!String.IsNullOrWhiteSpace(hfcheckvalue.Value))///Select all data
                {
                    btnSave.Visible = true;
                    Selectcheckdataall(1);
                
                }
                else//
                {
                    for (int i = 0; i < gvview.Rows.Count; i++)
                    {
                        btnSave.Visible = true;
                        CheckBox chk = gvview.Rows[i].FindControl("chktype") as CheckBox;
                        if (chk.Checked)
                        {
                            Label lblMallID = gvview.Rows[i].FindControl("lblMallID") as Label;
                            if (lblMallID.Text.Equals("1"))
                            {
                                if (!String.IsNullOrWhiteSpace(ddlRpoint.SelectedItem.Text))
                                {
                                    String strdateFrom = txtRperiodfrom.Text + " " + ddlRPeriodFromHour.SelectedItem.Text + ":" + ddlRPeriodFromMinute.SelectedItem.Text;
                                    DateTime dtFrom = Convert.ToDateTime(strdateFrom);
                                    DateTime dtCheck = DateTime.Now.AddHours(3);
                                    TimeSpan  different = dtCheck - dtFrom;
                                     String[] diff = different.ToString().Split(':');
                                    double difhour = Convert.ToDouble(diff[0]);
                                    if (difhour > 3)
                                    {
                                        GlobalUI.MessageBox("開始期間は3時間以上先の時刻を設定してください");
                                        return;
                                    }

                                    String strdateTo = txtRperiodto.Text + " " + ddlRperiodtohour.SelectedItem.Text + ":" + ddlRPeriodToMinute.SelectedItem.Text;
                                    DateTime dtTo = Convert.ToDateTime(strdateTo);

                                    if (dtTo < dtFrom)
                                    {
                                        GlobalUI.MessageBox("開始期間と終了期間に誤りがあります。");
                                        return;
                                    }

                                    //updated date 15/07/2015 for 24 hours or 60 days
                                    TimeSpan ? tdiff = dtTo - dtFrom;
                                    double difhours = tdiff.Value.TotalHours;
                                    if (difhours < 24 || difhours >1440)
                                    {
                                        GlobalUI.MessageBox("ポイント変倍率適用期間の期間を、最短24時間以上から最長60日以内の範囲で指定してください。");
                                        return;
                                    }

                                    Label lblpoint = gvview.Rows[i].FindControl("lblPoint") as Label;
                                    lblpoint.Text = ddlRpoint.SelectedItem.Text;

                                    Label lblpointPeriod = gvview.Rows[i].FindControl("lblPointPeriod") as Label;
                                    lblpointPeriod.Text = strdateFrom + "～" + strdateTo;
                                    lblpointPeriod.Text = lblpointPeriod.Text.Replace("/", "-");
                                }
                            }
                            else if (lblMallID.Text.Equals("2"))
                            {
                                if (!String.IsNullOrWhiteSpace(ddlYpoint.SelectedItem.Text))
                                {
                                    String strdateFrom = txtYperiodfrom.Text + " " + ddlYperiodfromhour.SelectedItem.Text + ":" + ddlYperiodFromMinute.SelectedItem.Text;
                                    DateTime dtFrom = Convert.ToDateTime(strdateFrom);
                                    DateTime dtCheck = DateTime.Now.AddHours(3);
                                    TimeSpan different = dtCheck - dtFrom;
                                    String[] diff = different.ToString().Split(':');
                                    double difhour = Convert.ToDouble(diff[0]);
                                    if (difhour > 3)
                                    {
                                        GlobalUI.MessageBox("開始期間は3時間以上先の時刻を設定してください");
                                        return;
                                    }

                                    String strdateTo = txtYperiodto.Text + " " + ddlYperiodtohour.SelectedItem.Text + ":" + ddlYperiodToMinute.SelectedItem.Text;
                                    DateTime dtTo = Convert.ToDateTime(strdateTo);

                                    if (dtTo < dtFrom)
                                    {
                                        GlobalUI.MessageBox("開始期間と終了期間に誤りがあります。");
                                        return;
                                    }

                                    ////updated date 15/07/2015 for 24 hours or 60 days
                                    //TimeSpan? tdiff = dtTo - dtFrom;
                                    //double difhours = tdiff.Value.TotalHours;
                                    //if (difhours < 24 || difhours > 1440)
                                    //{
                                    //    GlobalUI.MessageBox("ポイント変倍率適用期間の期間を、最短24時間以上から最長60日以内の範囲で指定してください。");
                                    //    return;
                                    //}

                                    Label lblpoint = gvview.Rows[i].FindControl("lblPoint") as Label;
                                    lblpoint.Text = ddlYpoint.SelectedItem.Text;

                                    Label lblpointPeriod = gvview.Rows[i].FindControl("lblPointPeriod") as Label;
                                    lblpointPeriod.Text = strdateFrom + "～" + strdateTo;
                                    lblpointPeriod.Text = lblpointPeriod.Text.Replace("/", "-");
                                }
                            }
                            else if (lblMallID.Text.Equals("3"))
                            {
                                if (!String.IsNullOrWhiteSpace(ddlPpoint.SelectedItem.Text))
                                {
                                    String strdateFrom = txtPperiodfrom.Text + " " + ddlPperiodfromhour.SelectedItem.Text + ":" + ddlPperiodfromMinute.SelectedItem.Text;
                                    DateTime dtFrom = Convert.ToDateTime(strdateFrom);
                                    DateTime dtCheck = DateTime.Now.AddHours(3);
                                    TimeSpan different = dtCheck - dtFrom;
                                    String[] diff = different.ToString().Split(':');
                                    double difhour = Convert.ToDouble(diff[0]);
                                    if (difhour > 3)
                                    {
                                        GlobalUI.MessageBox("開始期間は3時間以上先の時刻を設定してください");
                                        return;
                                    }

                                    String strdateTo = txtPperiodto.Text + " " + ddlPPeriodtohour.SelectedItem.Text + ":" + ddlPperiodToMinute.SelectedItem.Text;
                                    DateTime dtTo = Convert.ToDateTime(strdateTo);

                                    if (dtTo < dtFrom)
                                    {
                                        GlobalUI.MessageBox("開始期間と終了期間に誤りがあります。");
                                        return;
                                    }

                                    ////updated date 15/07/2015 for 24 hours or 60 days
                                    //TimeSpan? tdiff = dtTo - dtFrom;
                                    //double difhours = tdiff.Value.TotalHours;
                                    //if (difhours < 24 || difhours > 1440)
                                    //{
                                    //    GlobalUI.MessageBox("ポイント変倍率適用期間の期間を、最短24時間以上から最長60日以内の範囲で指定してください。");
                                    //    return;
                                    //}
                                    Label lblpoint = gvview.Rows[i].FindControl("lblPoint") as Label;
                                    lblpoint.Text = ddlPpoint.SelectedItem.Text;

                                    Label lblpointPeriod = gvview.Rows[i].FindControl("lblPointPeriod") as Label;
                                    lblpointPeriod.Text = strdateFrom + "～" + strdateTo;
                                    lblpointPeriod.Text = lblpointPeriod.Text.Replace("/", "-");
                                }
                            }
                        }
                    }
                }
            }//else
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void SearchDataPaging(int pgindex) 
        {
            try
            {
                pbl = new Promotion_Point_BL();
                Promotion_Point_Entity ppime = GetEntity();
                DataTable dt = new DataTable();

                if (!String.IsNullOrWhiteSpace(hfcheckvalue.Value))
                {
                  
                    Selectcheckdataall(pgindex+1);
                    
                }
                else
                {
                    int page_size = Convert.ToInt32(ddlpage.SelectedValue.ToString());
                    if (chkcheck.Checked && !String.IsNullOrWhiteSpace(txtitemcode.Text.Trim()))
                    {

                        dt = pbl.SelectallDataEqual(ppime, 0, pgindex + 1, page_size);
                    }//equal
                    else
                    {
                        dt = pbl.SelectallData(ppime, 1, pgindex + 1, page_size);

                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        DataTable dts = Search(dt);
                        gvview.DataSource = dts;
                        gvview.DataBind();


                    }
                    else
                    {
                        gvview.DataSource = dt;
                        gvview.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                //return new DataTable();
            }
        
        }
        public DataTable SearchData(int pgindex) 
        {
            try 
            {
                pbl = new Promotion_Point_BL();
                Promotion_Point_Entity ppime = GetEntity();
                DataTable dt = new DataTable();
                int page_size = Convert.ToInt32(ddlpage.SelectedValue.ToString());
                if (chkcheck.Checked && !String.IsNullOrWhiteSpace(txtitemcode.Text.Trim()))
                {
                    
                    dt = pbl.SelectallDataEqual(ppime, 0, pgindex + 1, page_size);
                }//equal
                else
                {
                    dt = pbl.SelectallData(ppime, 1, pgindex + 1, page_size);

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

        public Promotion_Point_Entity Getdata()
        {
            try
            {
            ppime = new Promotion_Point_Entity();

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
            ppime.RPointperiodfrom = FromDate;
            ppime.RPointperiodto = ToDate;

            string fromDatey = Request.Form[txtYperiodfrom.UniqueID];
            string toDatey = Request.Form[txtYperiodto.UniqueID];
            DateTime? FromDatey = new DateTime();
            DateTime? ToDatey = new DateTime();
            if (!String.IsNullOrEmpty(fromDatey))
            {
                FromDatey = DateConverter(fromDatey);
            }
            else
            {
                FromDatey = null;
            }
            if (!String.IsNullOrEmpty(toDatey))
            {
                ToDatey = DateConverter(toDatey);
            }
            else
            {
                ToDatey = null;
            }
            ppime.YPointperiodfrom = FromDatey;
            ppime.YPointperiodto = ToDatey;

            string fromDatep = Request.Form[txtPperiodfrom.UniqueID];
            string toDatep = Request.Form[txtPperiodto.UniqueID];
            DateTime? FromDatep = new DateTime();
            DateTime? ToDatep = new DateTime();
            if (!String.IsNullOrEmpty(fromDatep))
            {
                FromDatep = DateConverter(fromDatep);
            }
            else
            {
                FromDatep = null;
            }
            if (!String.IsNullOrEmpty(toDatep))
            {
                ToDatep = DateConverter(toDatep);
            }
            else
            {
                ToDatep = null;
            }
            ppime.PPointperiodfrom = FromDatep;
            ppime.PPointperiodto = ToDatep;

            if (!String.IsNullOrWhiteSpace(ddlRpoint.SelectedValue.ToString()))
                ppime.Rpimg = Int32.Parse(ddlRpoint.SelectedValue);


            if (!String.IsNullOrWhiteSpace(ddlYpoint.SelectedValue.ToString()))
                ppime.Ypimg = Int32.Parse(ddlYpoint.SelectedValue);
            if (!String.IsNullOrWhiteSpace(ddlPpoint.SelectedValue.ToString()))
                ppime.Ppimg = Int32.Parse(ddlPpoint.SelectedValue);
                
            ppime.Rstart = ddlRPeriodFromHour.SelectedValue + ":" + ddlRPeriodFromMinute.SelectedValue;
            ppime.Rend = ddlRperiodtohour.SelectedValue + ":" + ddlRPeriodToMinute.SelectedValue;
            ppime.Ystart = ddlYperiodfromhour.SelectedValue + ":" + ddlYperiodFromMinute.SelectedValue;
            ppime.Yend = ddlYperiodtohour.SelectedValue + ":" + ddlYperiodToMinute.SelectedValue;
            ppime.Pstart = ddlPperiodfromhour.SelectedValue + ":" + ddlPperiodfromMinute.SelectedValue;
            ppime.Pend = ddlPPeriodtohour.SelectedValue + ":" + ddlPperiodToMinute.SelectedValue;

            
          
                return ppime;
            
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new Promotion_Point_Entity();
            }
        }

        public  DataTable GetData1() 
        {
          try
          {
            if (ViewState["checkedValueitemcode"] != null)
            {
           ppime =Getdata();
           
           
            DataColumn newcols = new DataColumn("Item_Code", typeof(string));
            newcols.DefaultValue = null;
            dt.Columns.Add(newcols);
            dt.Columns.Add("Rakuten_Point", typeof(int));
            dt.Columns.Add("Yahoo_Point", typeof(int));
            dt.Columns.Add("Ponpare_Point", typeof(int));
            dt.Columns.Add("Rakuten_StartDate", typeof(DateTime));
            dt.Columns.Add("Rakuten_EndDate", typeof(DateTime));
            dt.Columns.Add("Yahoo_StartDate", typeof(DateTime));
            dt.Columns.Add("Yahoo_EndDate", typeof(DateTime));
            dt.Columns.Add("Ponpare_StartDate", typeof(DateTime));
            dt.Columns.Add("Ponpare_EndDate", typeof(DateTime));
            dt.Columns.Add("Rakuten_StartTime", typeof(string));
            dt.Columns.Add("Rakuten_EndTime", typeof(string));
            dt.Columns.Add("Yahoo_StartTime", typeof(string));
            dt.Columns.Add("Yahoo_EndTime", typeof(string));
            dt.Columns.Add("Ponpare_StartTime", typeof(string));
            dt.Columns.Add("Ponpare_EndTime", typeof(string));
            dt.Columns.Add("No", typeof(string));
            dt.Columns.Add("Shop_Name", typeof(string));
            dt.Columns.Add("ShopID", typeof(int));
            ArrayList chk = ViewState["checkedValueitemcode"] as ArrayList;
        
            if (chk != null)
            {
                for (int i = 0; i < chk.Count; i++)
                {
                    DataRow destRow = dt.NewRow();
                    dt.Rows.Add(destRow);
                    dt.Rows[i]["Item_Code"] = chk[i];
                    dt.Rows[i]["Rakuten_Point"] = ppime.Rpimg ;
                    dt.Rows[i]["Yahoo_Point"] = ppime.Ypimg;
                    dt.Rows[i]["Ponpare_Point"] =  ppime.Ppimg;
                    if (!String.IsNullOrWhiteSpace(ppime.RPointperiodfrom.ToString()))
                        dt.Rows[i]["Rakuten_StartDate"] = ppime.RPointperiodfrom;
                    else
                        dt.Rows[i]["Rakuten_StartDate"] = DBNull.Value;
                    if (!String.IsNullOrWhiteSpace(ppime.RPointperiodto.ToString()))
                    dt.Rows[i]["Rakuten_EndDate"] =  ppime.RPointperiodto;
                    else
                        dt.Rows[i]["Rakuten_EndDate"] = DBNull.Value;
                    if (!String.IsNullOrWhiteSpace(ppime.YPointperiodfrom.ToString()))
                        dt.Rows[i]["Yahoo_StartDate"] = ppime.YPointperiodfrom;
                    else
                        dt.Rows[i]["Yahoo_StartDate"] = DBNull.Value;
                    if (!String.IsNullOrWhiteSpace(ppime.YPointperiodto.ToString()))
                        dt.Rows[i]["Yahoo_EndDate"] = ppime.YPointperiodto;
                    else
                        dt.Rows[i]["Yahoo_EndDate"] = DBNull.Value;
                    if (!String.IsNullOrWhiteSpace(ppime.PPointperiodfrom.ToString()))
                        dt.Rows[i]["Ponpare_StartDate"] = ppime.PPointperiodfrom;
                    else
                        dt.Rows[i]["Ponpare_StartDate"] = DBNull.Value;
                    if (!String.IsNullOrWhiteSpace(ppime.PPointperiodto.ToString()))
                        dt.Rows[i]["Ponpare_EndDate"] = ppime.PPointperiodto;
                    else
                        dt.Rows[i]["Ponpare_EndDate"] = DBNull.Value;

                    dt.Rows[i]["Rakuten_StartTime"] = ppime.Rstart;
                    dt.Rows[i]["Rakuten_EndTime"] =  ppime.Rend ;
                    dt.Rows[i]["Yahoo_StartTime"] =  ppime.Ystart;
                    dt.Rows[i]["Yahoo_EndTime"] = ppime.Yend;
                    dt.Rows[i]["Ponpare_StartTime"] = ppime.Pstart;
                    dt.Rows[i]["Ponpare_EndTime"] =  ppime.Pend;
                  

                }

                ArrayList chkshop = ViewState["checkedValueshopname"] as ArrayList;
                if (chkshop != null)
                {
                    for (int y = 0; y < chkshop.Count; y++)
                    {
                        dt.Rows[y]["Shop_Name"] = chkshop[y];
                    }
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
        public DataTable GetData()
        {
            try
            {
                if (ViewState["checkedValue"] != null)
                {
                    ppime = Getdata();

                    
                        DataColumn newcols = new DataColumn("Item_Code", typeof(string));
                        newcols.DefaultValue = DBNull.Value;

                        dt.Columns.Add(newcols);
                        dt.Columns.Add("Rakuten_Point", typeof(int));
                        dt.Columns.Add("Yahoo_Point", typeof(int));
                        dt.Columns.Add("Ponpare_Point", typeof(int));
                        dt.Columns.Add("Rakuten_StartDate", typeof(DateTime));
                        dt.Columns.Add("Rakuten_EndDate", typeof(DateTime));
                        dt.Columns.Add("Yahoo_StartDate", typeof(DateTime));
                        dt.Columns.Add("Yahoo_EndDate", typeof(DateTime));
                        dt.Columns.Add("Ponpare_StartDate", typeof(DateTime));
                        dt.Columns.Add("Ponpare_EndDate", typeof(DateTime));
                        dt.Columns.Add("Rakuten_StartTime", typeof(string));
                        dt.Columns.Add("Rakuten_EndTime", typeof(string));
                        dt.Columns.Add("Yahoo_StartTime", typeof(string));
                        dt.Columns.Add("Yahoo_EndTime", typeof(string));
                        dt.Columns.Add("Ponpare_StartTime", typeof(string));
                        dt.Columns.Add("Ponpare_EndTime", typeof(string));
                        dt.Columns.Add("No", typeof(string));
                        dt.Columns.Add("Shop_Name", typeof(string));
                        dt.Columns.Add("ShopID", typeof(int));
                        ArrayList chk = ViewState["checkedValue"] as ArrayList;

                        if (chk != null)
                        {
                            for (int i = 0; i < chk.Count; i++)
                            {
                                DataRow destRow = dt.NewRow();
                                dt.Rows.Add(destRow);
                                dt.Rows[i]["Item_Code"] = chk[i].ToString().Split('$')[0];
                                dt.Rows[i]["Rakuten_Point"] = ppime.Rpimg;
                                dt.Rows[i]["Yahoo_Point"] = ppime.Ypimg;
                                dt.Rows[i]["Ponpare_Point"] = ppime.Ppimg;
                                if (!String.IsNullOrWhiteSpace(ppime.RPointperiodfrom.ToString()))
                                    dt.Rows[i]["Rakuten_StartDate"] = ppime.RPointperiodfrom;
                                else
                                    dt.Rows[i]["Rakuten_StartDate"] = DBNull.Value;
                                if (!String.IsNullOrWhiteSpace(ppime.RPointperiodto.ToString()))
                                    dt.Rows[i]["Rakuten_EndDate"] = ppime.RPointperiodto;
                                else
                                    dt.Rows[i]["Rakuten_EndDate"] = DBNull.Value;
                                if (!String.IsNullOrWhiteSpace(ppime.YPointperiodfrom.ToString()))
                                    dt.Rows[i]["Yahoo_StartDate"] = ppime.YPointperiodfrom;
                                else
                                    dt.Rows[i]["Yahoo_StartDate"] = DBNull.Value;
                                if (!String.IsNullOrWhiteSpace(ppime.YPointperiodto.ToString()))
                                    dt.Rows[i]["Yahoo_EndDate"] = ppime.YPointperiodto;
                                else
                                    dt.Rows[i]["Yahoo_EndDate"] = DBNull.Value;
                                if (!String.IsNullOrWhiteSpace(ppime.PPointperiodfrom.ToString()))
                                    dt.Rows[i]["Ponpare_StartDate"] = ppime.PPointperiodfrom;
                                else
                                    dt.Rows[i]["Ponpare_StartDate"] = DBNull.Value;
                                if (!String.IsNullOrWhiteSpace(ppime.PPointperiodto.ToString()))
                                    dt.Rows[i]["Ponpare_EndDate"] = ppime.PPointperiodto;
                                else
                                    dt.Rows[i]["Ponpare_EndDate"] = DBNull.Value;

                                dt.Rows[i]["Rakuten_StartTime"] = ppime.Rstart;
                                dt.Rows[i]["Rakuten_EndTime"] = ppime.Rend;
                                dt.Rows[i]["Yahoo_StartTime"] = ppime.Ystart;
                                dt.Rows[i]["Yahoo_EndTime"] = ppime.Yend;
                                dt.Rows[i]["Ponpare_StartTime"] = ppime.Pstart;
                                dt.Rows[i]["Ponpare_EndTime"] = ppime.Pend;

                                dt.Rows[i]["Shop_Name"] = chk[i].ToString().Split('$')[1];
                                if (chk[i].ToString().Contains("楽天"))
                                {
                                    dt.Rows[i]["Yahoo_Point"] = 0;
                                    dt.Rows[i]["Ponpare_Point"] = 0;
                                    dt.Rows[i]["Yahoo_StartDate"] = DBNull.Value;
                                    dt.Rows[i]["Yahoo_EndDate"] = DBNull.Value;
                                    dt.Rows[i]["Yahoo_StartTime"] = DBNull.Value;
                                    dt.Rows[i]["Yahoo_EndTime"] = DBNull.Value;
                                    dt.Rows[i]["Ponpare_StartDate"] = DBNull.Value;
                                    dt.Rows[i]["Ponpare_EndDate"] = DBNull.Value;
                                    dt.Rows[i]["Ponpare_StartTime"] = DBNull.Value;
                                    dt.Rows[i]["Ponpare_EndTime"] = DBNull.Value;
                                }
                                else if (chk[i].ToString().Contains("ヤフー"))
                                {
                                    dt.Rows[i]["Rakuten_Point"] = 0;
                                    dt.Rows[i]["Rakuten_StartDate"] = DBNull.Value;
                                    dt.Rows[i]["Rakuten_EndDate"] = DBNull.Value;
                                    dt.Rows[i]["Rakuten_StartTime"] = DBNull.Value;
                                    dt.Rows[i]["Rakuten_EndTime"] = DBNull.Value;
                                    dt.Rows[i]["Ponpare_Point"] = 0;
                                    dt.Rows[i]["Ponpare_StartDate"] = DBNull.Value;
                                    dt.Rows[i]["Ponpare_EndDate"] = DBNull.Value;
                                    dt.Rows[i]["Ponpare_StartTime"] = DBNull.Value;
                                    dt.Rows[i]["Ponpare_EndTime"] = DBNull.Value;
                                }
                                else if (chk[i].ToString().Contains("ポンパレ"))
                                {
                                    dt.Rows[i]["Rakuten_Point"] = 0;
                                    dt.Rows[i]["Rakuten_StartDate"] = DBNull.Value;
                                    dt.Rows[i]["Rakuten_EndDate"] = DBNull.Value;
                                    dt.Rows[i]["Rakuten_StartTime"] = DBNull.Value;
                                    dt.Rows[i]["Rakuten_EndTime"] = DBNull.Value;
                                    dt.Rows[i]["Yahoo_Point"] = 0;
                                    dt.Rows[i]["Yahoo_StartDate"] = DBNull.Value;
                                    dt.Rows[i]["Yahoo_EndDate"] = DBNull.Value;
                                    dt.Rows[i]["Yahoo_StartTime"] = DBNull.Value;
                                    dt.Rows[i]["Yahoo_EndTime"] = DBNull.Value;
                                }
                            }
                        }


                   
                }//if
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            }
        }
        private DateTime DateConverter(string dateTime)
        {
            try
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
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DateTime();
            }
        }
        private string DateConverter1(string dateTime)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd-MM-yyyy";
            dtfi.DateSeparator = "-";
            DateTime objDate = Convert.ToDateTime(dateTime, dtfi);
            //string date = Convert.ToDateTime(objDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy hh:MM:ss");
            //objDate = DateTime.ParseExact(date, "MM/dd/yyyy hh:MM:ss", null);
            string date = Convert.ToDateTime(objDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy ");
            objDate = DateTime.ParseExact(date, "MM/dd/yyyy ", null);
            string d = Convert.ToString(objDate);
            return d;
        }
        public DataTable SaveShops(int promotionID)
        {
            try
            {
                pbl = new Promotion_Point_BL();
                DataTable dtshop = new DataTable();
                //dtshop.Columns.Add("PromotionID", typeof(int));
                dtshop.Columns.Add("ShopID", typeof(int));

                return dtshop;
                // pbl.Insert(dt);
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
            try
            {
                pbl = new Promotion_Point_BL();

                if (btnSave.Text == "確認画面へ")
                {
                    dt.Columns.Add("Item_Code");
                    dt.Columns.Add("Item_Name");
                    dt.Columns.Add("Ctrl_ID");
                    dt.Columns.Add("Shop_Name");
                    dt.Columns.Add("Point_magnification");
                    dt.Columns.Add("Point_period");
                    dt.Columns.Add("Instruction_No");
                    dt.Columns.Add("Brand_Name");
                    dt.Columns.Add("Year");
                    dt.Columns.Add("Season");
                    dt.Columns.Add("ID");
                    dt.Columns.Add("Mall_ID");
                    dt.Columns.Add("Rakuten_StartTime");
                    dt.Columns.Add("Rakuten_EndTime");
                    dt.Columns.Add("Yahoo_StartTime");
                    dt.Columns.Add("Yahoo_EndTime");
                    dt.Columns.Add("Ponpare_StartTime");
                    dt.Columns.Add("Ponpare_EndTime");
                    dt.Columns.Add("Rakuten_StartDate");
                    dt.Columns.Add("Rakuten_EndDate");
                    dt.Columns.Add("Yahoo_StartDate");
                    dt.Columns.Add("Yahoo_EndDate");
                    dt.Columns.Add("Ponpare_StartDate");
                    dt.Columns.Add("Ponpare_EndDate");
                    dt.Columns.Add("Rakuten_Point");
                    dt.Columns.Add("Yahoo_Point");
                    dt.Columns.Add("Ponpare_Point");
                    dt.Columns.Add("ShopID");

                    if (!String.IsNullOrWhiteSpace(hfcheckvalue.Value))
                    {
                      dt=Saveforcheckalldata(dt);
                    }//selectall check box

                    else
                    {
                        int j = 0;
                        for (int i = 0; i < gvview.Rows.Count; i++)
                        {
                            CheckBox chk = gvview.Rows[i].FindControl("chktype") as CheckBox;
                            if (chk.Checked)
                            {
                                Label lblItemCode = gvview.Rows[i].FindControl("lblItemCode") as Label;
                                Label lblItemName = gvview.Rows[i].FindControl("lblItemName") as Label;

                                Label lblPeriod = gvview.Rows[i].FindControl("lblPointPeriod") as Label;
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
                                    startTime = start[start.Length - 1];

                                    String[] end = endDate.Split(' ');
                                    endDate = end[0];
                                    endTime = end[end.Length - 1];
                                }

                                Label lblCtrlID = gvview.Rows[i].FindControl("lblCtrlID") as Label;
                                Label lblShopName = gvview.Rows[i].FindControl("lblShopName") as Label;
                                Label lblPoint = gvview.Rows[i].FindControl("lblPoint") as Label;
                                Label lblBrandName = gvview.Rows[i].FindControl("lblBrandName") as Label;
                                Label lblInstructionNo = gvview.Rows[i].FindControl("lblInstructionNo") as Label;
                                Label lblYear = gvview.Rows[i].FindControl("lblYear") as Label;
                                Label lblSeason = gvview.Rows[i].FindControl("lblSeason") as Label;
                                Label lblID = gvview.Rows[i].FindControl("lblID") as Label;
                                Label lblMallID = gvview.Rows[i].FindControl("lblMallID") as Label;
                                Label lblRStartTime = gvview.Rows[i].FindControl("lblRStartTime") as Label;
                                Label lblREndTime = gvview.Rows[i].FindControl("lblREndTime") as Label;
                                Label lblYStartTime = gvview.Rows[i].FindControl("lblYStartTime") as Label;
                                Label lblYEndTime = gvview.Rows[i].FindControl("lblYEndTime") as Label;
                                Label lblPStartTime = gvview.Rows[i].FindControl("lblPStartTime") as Label;
                                Label lblPEndTime = gvview.Rows[i].FindControl("lblPEndTime") as Label;
                                Label lblRStartDate = gvview.Rows[i].FindControl("lblRStartDate") as Label;
                                Label lblREndDate = gvview.Rows[i].FindControl("lblREndDate") as Label;
                                Label lblYStartDate = gvview.Rows[i].FindControl("lblYStartDate") as Label;
                                Label lblYEndDate = gvview.Rows[i].FindControl("lblYEndDate") as Label;
                                Label lblPStartDate = gvview.Rows[i].FindControl("lblPStartDate") as Label;
                                Label lblPEndDate = gvview.Rows[i].FindControl("lblPEndDate") as Label;
                                Label lblShop_ID = gvview.Rows[i].FindControl("lblShop_ID") as Label;

                                dt.Rows.Add();
                                dt.Rows[j]["Item_Code"] = lblItemCode.Text;
                                dt.Rows[j]["Item_Name"] = lblItemName.Text;
                                dt.Rows[j]["Ctrl_ID"] = lblCtrlID.Text;
                                dt.Rows[j]["Shop_Name"] = lblShopName.Text;

                                if (lblMallID.Text.Equals("1"))
                                {
                                    dt.Rows[j]["Rakuten_Point"] = lblPoint.Text;
                                    dt.Rows[j]["Yahoo_Point"] = DBNull.Value;
                                    dt.Rows[j]["Ponpare_Point"] = DBNull.Value;

                                    dt.Rows[j]["Rakuten_StartDate"] = startDate;
                                    dt.Rows[j]["Rakuten_EndDate"] = endDate;

                                    dt.Rows[j]["Rakuten_StartTime"] = startTime;
                                    dt.Rows[j]["Rakuten_EndTime"] = endTime;
                                }

                                else if (lblMallID.Text.Equals("2"))
                                {
                                    dt.Rows[j]["Rakuten_Point"] = DBNull.Value;
                                    dt.Rows[j]["Yahoo_Point"] = lblPoint.Text;
                                    dt.Rows[j]["Ponpare_Point"] = DBNull.Value;

                                    dt.Rows[j]["Yahoo_StartDate"] = startDate;
                                    dt.Rows[j]["Yahoo_EndDate"] = endDate;

                                    dt.Rows[j]["Yahoo_StartTime"] = startTime;
                                    dt.Rows[j]["Yahoo_EndTime"] = endTime;
                                }

                                else if (lblMallID.Text.Equals("3"))
                                {
                                    dt.Rows[j]["Rakuten_Point"] = DBNull.Value;
                                    dt.Rows[j]["Yahoo_Point"] = DBNull.Value;
                                    dt.Rows[j]["Ponpare_Point"] = lblPoint.Text;

                                    dt.Rows[j]["Ponpare_StartDate"] = startDate;
                                    dt.Rows[j]["Ponpare_EndDate"] = endDate;

                                    dt.Rows[j]["Ponpare_StartTime"] = startTime;
                                    dt.Rows[j]["Ponpare_EndTime"] = endTime;
                                }


                                dt.Rows[j]["Point_magnification"] = lblPoint.Text;
                                dt.Rows[j]["Point_period"] = lblPeriod.Text;
                                dt.Rows[j]["Instruction_No"] = lblInstructionNo.Text;
                                dt.Rows[j]["Brand_Name"] = lblBrandName.Text;
                                dt.Rows[j]["Year"] = lblYear.Text;
                                dt.Rows[j]["Season"] = lblSeason.Text;
                                dt.Rows[j]["ID"] = lblID.Text;
                                dt.Rows[j]["Mall_ID"] = lblMallID.Text;
                                dt.Rows[j]["ShopID"] = lblShop_ID.Text;

                                j++;
                            }
                        }
                    }//else
                    if (dt.Rows.Count > 0)
                    {
                        gvview.DataSource = dt;
                        Cache.Insert("dtSetting", dt);
                        gvview.DataBind();

                        gvview.Columns[0].Visible = false;
                        confirm.Visible = true;
                        title1.Visible = false;
                        title2.Visible = false;
                        btncancelall.Visible = false;
                        btnselectall.Visible = false;
                        btnsearch.Visible = false;
                        btnsetting.Visible = false;
                        gp.Visible = false;

                        b1.Visible = false;
                        b2.Visible = false;
                        b3.Visible = false;
                        b4.Visible = false;
                        b5.Visible = false;
                        b6.Visible = false;
                        b7.Visible = false;
                        ddlpage.Visible = false;
                        btnSave.Text = "確定";
                    }
                    else
                    {
                        GlobalUI.MessageBox("This is no item selected!");
                    }
                }

                else if (btnSave.Text == "確定")
                {
                    int promotinid = 0;
                    DataTable dtInsert = Cache["dtSetting"] as DataTable;
                    int count = dtInsert.Rows.Count;
                    if (dtInsert != null && dtInsert.Rows.Count > 0)
                    {
                        promotinid = pbl.Save(dtInsert);
                        Cache.Remove("dtSetting");
                        btncancelall.Visible = true;
                        btnselectall.Visible = true;
                        btnsearch.Visible = true;
                        btnsetting.Visible = true;
                        gvview.Columns[0].Visible = true;
                        btnSave.Text = "確認画面へ";
                        confirm.Visible = false;
                        title1.Visible = true;
                        title2.Visible = true;
                        b1.Visible = true;
                        b2.Visible = true;
                        b3.Visible = true;
                        b4.Visible = true;
                        b5.Visible = true;
                        b6.Visible = true;
                        b7.Visible = true;
                        gp.Visible = true;
                        ddlpage.Visible = true;

                        object referrer = ViewState["UrlReferrer"];
                        string url = "Promotion_Point_Entry.aspx";
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

        protected DataTable Saveforcheckalldata(DataTable dt) 
        {
            try 
            {
                if (ViewState["dt"] != null)
                { 
                DataTable dts =ViewState["dt"] as DataTable;
                    int j=0;
                       for(int i=0;i<dts.Rows.Count ;i++)
                       {
                                  dt.Rows.Add();
                                dt.Rows[j]["Item_Code"] =dts.Rows[i]["Item_Code"].ToString();
                                dt.Rows[j]["Item_Name"] =dts.Rows[i]["Item_Name"].ToString();
                                dt.Rows[j]["Ctrl_ID"] = dts.Rows[i]["Ctrl_ID"].ToString();
                                dt.Rows[j]["Shop_Name"] = dts.Rows[i]["Shop_Name"].ToString();


                                String lblPeriod = dts.Rows[i]["Point_period"].ToString();
                                String[] date = new String[2];
                                String startDate = String.Empty;
                                String endDate = String.Empty;
                                String startTime = String.Empty;
                                String endTime = String.Empty;
                                if (!String.IsNullOrWhiteSpace(lblPeriod))
                                {
                                    date = lblPeriod.Split('～');
                                    startDate = date[0];
                                    endDate = date[1];

                                    String[] start = startDate.Split(' ');
                                    startDate = start[0];
                                    startTime = start[start.Length - 1];

                                    String[] end = endDate.Split(' ');
                                    endDate = end[0];
                                    endTime = end[end.Length - 1];
                                }


                                String lblMallID=dts.Rows[i]["Mall_ID"].ToString();
                                if (lblMallID.Equals("1"))
                                {
                                    dt.Rows[j]["Rakuten_Point"] = dts.Rows[i]["Rakuten_Point"].ToString();
                                    dt.Rows[j]["Yahoo_Point"] = DBNull.Value;
                                    dt.Rows[j]["Ponpare_Point"] = DBNull.Value;

                                    dt.Rows[j]["Rakuten_StartDate"] = startDate;
                                    dt.Rows[j]["Rakuten_EndDate"] = endDate;

                                    dt.Rows[j]["Rakuten_StartTime"] = startTime;
                                    dt.Rows[j]["Rakuten_EndTime"] = endTime;

                                    dt.AcceptChanges();
                                }

                                else if (lblMallID.Equals("2"))
                                {
                                    dt.Rows[j]["Rakuten_Point"] = DBNull.Value;
                                    dt.Rows[j]["Yahoo_Point"] = dts.Rows[i]["Yahoo_Point"].ToString();
                                    dt.Rows[j]["Ponpare_Point"] = DBNull.Value;

                                    dt.Rows[j]["Yahoo_StartDate"] = startDate;
                                    dt.Rows[j]["Yahoo_EndDate"] = endDate;

                                    dt.Rows[j]["Yahoo_StartTime"] = startTime;
                                    dt.Rows[j]["Yahoo_EndTime"] = endTime;
                                    dt.AcceptChanges();
                                }

                                else if (lblMallID.Equals("3"))
                                {
                                    dt.Rows[j]["Rakuten_Point"] = DBNull.Value;
                                    dt.Rows[j]["Yahoo_Point"] = DBNull.Value;
                                    dt.Rows[j]["Ponpare_Point"] = dts.Rows[i]["Ponpare_Point"].ToString();

                                    dt.Rows[j]["Ponpare_StartDate"] = startDate;
                                    dt.Rows[j]["Ponpare_EndDate"] = endDate;

                                    dt.Rows[j]["Ponpare_StartTime"] = startTime;
                                    dt.Rows[j]["Ponpare_EndTime"] = endTime;
                                    dt.AcceptChanges();
                                }


                                dt.Rows[j]["Point_magnification"] = dts.Rows[i]["Point_magnification"].ToString();
                                dt.Rows[j]["Point_period"] = dts.Rows[i]["Point_period"].ToString();
                                dt.Rows[j]["Instruction_No"] = dts.Rows[i]["Instruction_No"].ToString();
                                dt.Rows[j]["Brand_Name"] = dts.Rows[i]["Brand_Name"].ToString();
                                dt.Rows[j]["Year"] = dts.Rows[i]["Year"].ToString();
                                dt.Rows[j]["Season"] = dts.Rows[i]["Season"].ToString();
                                dt.Rows[j]["ID"] = dts.Rows[i]["ID"].ToString();
                                dt.Rows[j]["Mall_ID"] = dts.Rows[i]["Mall_ID"].ToString();
                                dt.Rows[j]["ShopID"] = dts.Rows[i]["ShopID"].ToString();
                                dt.AcceptChanges();
                                j++;
                            }
              
                }

                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }

        protected void gvview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (!String.IsNullOrWhiteSpace(hfcheckvalue.Value))
                    {
                        if (ViewState["chkValue"] != null)
                        {
                            ArrayList arlist = ViewState["chkValue"] as ArrayList;
                            Label lblid = e.Row.FindControl("lblID") as Label;
                            CheckBox chk = e.Row.FindControl("chktype") as CheckBox;
                            
                            Label lblsid = e.Row.FindControl("lblShop_ID") as Label;
                            string unit = lblid.Text + "$" + lblsid.Text;
                            if (arlist.Contains(unit))
                                chk.Checked = true;
                            else
                                chk.Checked = false;
                        }
                        else
                        {
                            CheckBox chk = e.Row.FindControl("chktype") as CheckBox;
                            chk.Checked = true;
                        }

                    }

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
                    hfpageno.Value = "1";
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

        protected  Promotion_Point_Entity Clear() 
        {
            Promotion_Point_Entity ppime = new Promotion_Point_Entity();
            try
            {
                string shopnames = null;
          
            
                    ppime.Itemcode = null;
            
                    ppime.Itemname = null;
                
                    ppime.Year = null;
             
                    ppime.Brandname =null;
               
                    ppime.Season = null;
               
                    ppime.Competationname = null;
              
                    ppime.InstructionNo = null;
               
                    ppime.Claffication = null;

               
                ppime.Shopnmae = shopnames;


                ppime.Rperiodfrom = null;
                ppime.Rperiodto = null;


                ppime.Yperiodfrom = null;
                ppime.Yperiodto = null;


                ppime.Pperiodfrom = null;
                ppime.Pperiodto = null;

                //if (!String.IsNullOrWhiteSpace(ddlRpoint.SelectedValue.ToString()))
                //    ppime.RP = Int32.Parse(ddlRpoint.SelectedValue);


                //if (!String.IsNullOrWhiteSpace(ddlYpoint.SelectedValue.ToString()))
                //    ppime.YP = Int32.Parse(ddlYpoint.SelectedValue);
                //if (!String.IsNullOrWhiteSpace(ddlPpoint.SelectedValue.ToString()))
                //    ppime.PP = Int32.Parse(ddlPpoint.SelectedValue);

                ppime.Shopstatus = null;
                txtRperiodfrom.Text = null;
                txtRperiodto.Text = null;
                txtYperiodfrom.Text = null;
                txtYperiodto.Text = null;
                txtPperiodfrom.Text = null;
                txtPperiodto.Text = null;
                ddlRpoint.SelectedIndex = 0;
                ddlYpoint.SelectedIndex = 0;
                ddlPpoint.SelectedIndex = 0;
                return ppime;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return ppime;
            }
        
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtRakupointPeriod.Text = String.Empty;
              
                txtRakupointPeriod2.Text = Request.Form[txtRakupointPeriod2.UniqueID];
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
                txtRakupointPeriod2.Text = String.Empty;
             
                txtRakupointPeriod.Text = Request.Form[txtRakupointPeriod.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtyahooperiod.Text = String.Empty;

                txtyahooperiod2.Text = Request.Form[txtyahooperiod2.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtyahooperiod2.Text = String.Empty;

                txtyahooperiod.Text = Request.Form[txtyahooperiod.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtponpareperiod.Text = String.Empty;

                txtponpareperiod2.Text = Request.Form[txtponpareperiod2.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtponpareperiod2.Text = String.Empty;

                txtponpareperiod.Text = Request.Form[txtponpareperiod.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtRperiodfrom.Text = String.Empty;
                ddlRPeriodFromHour.SelectedIndex = -1;
                ddlRPeriodFromMinute.SelectedIndex = -1;
                txtRperiodto.Text = Request.Form[txtRperiodto.UniqueID];

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtRperiodto.Text = String.Empty;
                ddlRperiodtohour.SelectedIndex = -1;
                ddlRPeriodToMinute.SelectedIndex = -1;
                txtRperiodfrom.Text = Request.Form[txtRperiodfrom.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ImageButton11_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtYperiodfrom.Text = String.Empty;
                ddlYperiodfromhour.SelectedIndex = -1;
                ddlYperiodFromMinute.SelectedIndex = -1;
                txtYperiodto.Text = Request.Form[txtYperiodto.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtYperiodto.Text = String.Empty;
                ddlYperiodtohour.SelectedIndex = -1;
                ddlYperiodToMinute.SelectedIndex = -1;
                txtYperiodfrom.Text = Request.Form[txtYperiodfrom.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ImageButton13_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtPperiodfrom.Text = String.Empty;
                ddlPperiodfromhour.SelectedIndex = -1;
                ddlPperiodfromMinute.SelectedIndex = -1;
                txtPperiodto.Text = Request.Form[txtPperiodto.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ImageButton14_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtPperiodto.Text = String.Empty;
                ddlPPeriodtohour.SelectedIndex = -1;
                ddlPperiodToMinute.SelectedIndex = -1;
                txtPperiodfrom.Text = Request.Form[txtPperiodfrom.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
    }
         