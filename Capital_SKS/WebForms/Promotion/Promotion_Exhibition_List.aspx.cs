/* 
Created By              : Kay Thi Aung
Created Date          : /04/2015
Updated By             :
Updated Date         :

 Tables using           : Exhibition_Promotion_Item_Master,
                                   Exhibition_Promotion_Item_Shop,
                                   Exhibition_Item_Category,
                                   Import_ShopItem_Category,
 *                                
   
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
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace ORS_RCM.WebForms.Promotion
{
    public partial class Promotion_Exhibition_List : System.Web.UI.Page
    {
        Promotion_Point_BL ehbbl = new Promotion_Point_BL();
        Item_ExportField_BL itfield_bl = new Item_ExportField_BL();
        GlobalBL gb;
        public string list; public string csv;
        DateTime? FromDate = null;
        DateTime? ToDate = null; int shid;
        string url = ConfigurationManager.AppSettings["rakuten"].ToString();
        string yurl = ConfigurationManager.AppSettings["yahoo"].ToString();
        string purl = ConfigurationManager.AppSettings["ponpare"].ToString();
        string jurl = ConfigurationManager.AppSettings["jisha"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
              try
           {
            if (!IsPostBack)
            {
               ViewState["lists"]= null;
                itfield_bl = new Item_ExportField_BL();
                ehbbl = new Promotion_Point_BL();
                gb = new GlobalBL();
                gvpageindex();
                // DataTable dtID = ehb.SelectbyID();
                DataTable dtID = ehbbl.SelectAllpaging(null, null, null, null, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, FromDate, ToDate, 1, gvexhibition.PageSize);

                gvexhibition.DataSource = dtID;
                gvexhibition.DataBind();
             //   gp.CalculatePaging(dtID.Rows.Count, gvexhibition.PageSize, 1);

                if (dtID != null && dtID.Rows.Count > 0)
                {
                    for (int j = 0; j < dtID.Rows.Count; j++)
                    {
                        list += dtID.Rows[j]["ID"].ToString() + ',';
                    }
                   ViewState["lists"]= list;
                }
                else
                {ViewState["lists"]= null; }
                ddlmall.DataTextField = "Code_Description";
                ddlmall.DataValueField = "Code_Description";
                ddlmall.DataSource = gb.Code_Setup(1);
                ddlmall.DataBind();
                ddlmall.Items.Insert(0, new ListItem("ショップ選択", ""));

                DataTable dtex = itfield_bl.SelectUser();

                ddlexhibitor.DataTextField = "User_Name";
                ddlexhibitor.DataValueField = "ID";
                ddlexhibitor.DataSource = dtex;
                ddlexhibitor.DataBind();
                ddlexhibitor.Items.Insert(0, "");
            }
            else
            {
                String ctrl = getPostBackControlName();
                if (ctrl.Contains("lnkPaging"))
                {
                    string fromDate = Request.Form[txtexdatetime1.UniqueID];
                    string toDate = Request.Form[txtdatetime2.UniqueID];
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
                    list =ViewState["lists"].ToString();
                 
                    ehbbl = new Promotion_Point_BL();
                    DataTable dts = new DataTable();
                    gvexhibition.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
                    gp.LinkButtonClick(ctrl, gvexhibition.PageSize);
                    Label lbl = gp.FindControl("lblCurrent") as Label;
                    int index = Convert.ToInt32(lbl.Text);
                  
                    gvexhibition.PageIndex = index;
                    if (chkitemcode.Checked)
                        dts = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 6, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                        ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, index, gvexhibition.PageSize);
                    else
                        dts = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 10, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                                                  ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, index, gvexhibition.PageSize);
                    

                    gvexhibition.DataSource = dts;
                    gvexhibition.DataBind();
                }
            }
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
                return String.Empty;
            }
        }

        protected void gvpageindex()
        {
            try
            {
                ehbbl = new Promotion_Point_BL();

                //DataTable dtcount = ehbbl.SelectAllpaging(null, null, null, null, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, FromDate, ToDate,0,0);
            

                gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                DataTable dts = ehbbl.SelectAllpaging(null, null, null, null, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, FromDate, ToDate, 1, gvexhibition.PageSize);
                int count = 0;
                if(dts != null && dts.Rows.Count >0)
                 count = Convert.ToInt32(dts.Rows[0]["Total_Count"].ToString());   
                //gp.TotalRecord = dts.Rows.Count;
                    //gp.OnePageRecord = gvexhibition.PageSize;
                    //int index1 = 0;
                    //gp.sendIndexToThePage += delegate(int index)
                    //{
                    //    index1 = index;
                    //};
                    //gvexhibition.PageIndex = index1;
                    gvexhibition.DataSource = dts;
                    gvexhibition.DataBind();
                    gp.CalculatePaging(count, gvexhibition.PageSize, 1);
               
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }


        protected void gvexhibition_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region
            ehbbl = new Promotion_Point_BL();
            if (e.CommandName == "Detail")
            {

                int Mallid = Convert.ToInt32(e.CommandArgument);
                DataTable dt = ehbbl.SelectMall(Mallid);
                int Shop_ID = (int)dt.Rows[0]["ID"];
                if (Convert.ToString(Mallid) == "1")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblEID");
                    int Item_ID = Convert.ToInt32(lblID.Text);
                    Label lblshopname = (Label)clickedRow.FindControl("Label6");
                    Label lblpromotiontype = (Label)clickedRow.FindControl("lblpromotiontype");
                    int protype = Convert.ToInt32(lblpromotiontype.Text);
                    Label lblcsvtype = (Label)clickedRow.FindControl("lblcsvtype");
                    int csvtype = Convert.ToInt32(lblcsvtype.Text);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Mall_ID"].ToString() == "1" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                            shid = (int)dt.Rows[i]["ID"];
                    }
                    Response.Redirect("Details_of_Promotionexhibition_Rakuten.aspx?Shop_ID=" + shid + "&Item_ID=" + Item_ID + "&Type=" + protype + "&CSV=" + csvtype);
                }
                else if (Convert.ToString(Mallid) == "2")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblEID");
                    int Item_ID = Convert.ToInt32(lblID.Text);
                    Label lblshopname = (Label)clickedRow.FindControl("Label6");
                    Label lblpromotiontype = (Label)clickedRow.FindControl("lblpromotiontype");
                    int protype = Convert.ToInt32(lblpromotiontype.Text);
                    Label lblcsvtype = (Label)clickedRow.FindControl("lblcsvtype");
                    int csvtype = Convert.ToInt32(lblcsvtype.Text);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Mall_ID"].ToString() == "2" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                            shid = (int)dt.Rows[i]["ID"];
                    }

                    Response.Redirect("Details_of_Promotionexhibition_Yahoo.aspx?Shop_ID=" + shid + "&Item_ID=" + Item_ID + "&Type=" + protype + "&CSV=" + csvtype);
                }
                else if (Convert.ToString(Mallid) == "3")
                {

                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblEID");
                    int Item_ID = Convert.ToInt32(lblID.Text);
                    Label lblshopname = (Label)clickedRow.FindControl("Label6");
                    Label lblpromotiontype = (Label)clickedRow.FindControl("lblpromotiontype");
                    int protype = Convert.ToInt32(lblpromotiontype.Text);
                    Label lblcsvtype = (Label)clickedRow.FindControl("lblcsvtype");
                    int csvtype = Convert.ToInt32(lblcsvtype.Text);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Mall_ID"].ToString() == "3" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                            shid = (int)dt.Rows[i]["ID"];
                    }
                    Response.Redirect("Details_of_Promotionexhibition_Ponpare.aspx?Shop_ID=" + shid + "&Item_ID=" + Item_ID + "&Type=" + protype + "&CSV=" + csvtype);
                }

                else if (Convert.ToString(Mallid) == "4")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblEID");
                    int Item_ID = Convert.ToInt32(lblID.Text);
                    Label lblshopname = (Label)clickedRow.FindControl("Label6");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Mall_ID"].ToString() == "4" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                            shid = (int)dt.Rows[i]["ID"];
                    }
                    Response.Redirect("Details_of_exhibition_Amazone.aspx?Item_ID=" + Item_ID + "&Shop_ID=" + shid);
                }
                //Details_of_exhibition_jisha.aspx

                else if (Convert.ToString(Mallid) == "5")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblEID");
                    int Item_ID = Convert.ToInt32(lblID.Text);
                    Label lblshopname = (Label)clickedRow.FindControl("Label6");
                    Label lblpromotiontype = (Label)clickedRow.FindControl("lblpromotiontype");
                    int protype = Convert.ToInt32(lblpromotiontype.Text);
                    Label lblcsvtype = (Label)clickedRow.FindControl("lblcsvtype");
                    int csvtype = Convert.ToInt32(lblcsvtype.Text);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Mall_ID"].ToString() == "5" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                            shid = (int)dt.Rows[i]["ID"];
                    }
                    Response.Redirect("Details_of_Promotionexhibition_jisha.aspx?Shop_ID=" + shid + "&Item_ID=" + Item_ID + "&Type=" + protype + "&CSV=" + csvtype);

                }





            }
            else if (e.CommandName == "ShopInfo")
            {

                int Mallid = Convert.ToInt32(e.CommandArgument);
                DataTable dt = ehbbl.SelectMall(Mallid);
                if (Convert.ToString(Mallid) == "1")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblsitename");
                    Label lblcode = (Label)clickedRow.FindControl("Label2");
                    string itemcode = lblcode.Text;
                    string shopname = lblID.Text;



                    url += shopname + "/" + itemcode;
                    string name = shopname + "/" + itemcode;

                    Response.Redirect(url);


                }
                else if (Convert.ToString(Mallid) == "2")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblsitename");
                    Label lblcode = (Label)clickedRow.FindControl("Label2");
                    string itemcode = lblcode.Text;
                    string shopname = lblID.Text;
                    yurl += shopname + "/" + itemcode + ".html";
                    Response.Redirect(yurl);


                }
                else if (Convert.ToString(Mallid) == "3")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblsitename");
                    Label lblcode = (Label)clickedRow.FindControl("Label2");
                    string itemcode = lblcode.Text;
                    string shopname = lblID.Text;
                    purl += shopname + "/goods/" + itemcode + "/";
                    Response.Redirect(purl);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var win = window.open('" + purl + "','_newtab'); self.focus();", true);
                }
                else if (Convert.ToString(Mallid) == "5")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblsitename");
                    Label lblcode = (Label)clickedRow.FindControl("Label2");
                    string itemcode = lblcode.Text;
                    string shopname = lblID.Text;
                    jurl += itemcode;
                    Response.Redirect(jurl);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var win = window.open('" + purl + "','_newtab'); self.focus();", true);
                }
            }

            else if (e.CommandName == "Edit")
            {
                string Item_Code = e.CommandArgument.ToString();
                int shopid = 0;
                GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                Label lblpromotiontype = (Label)clickedRow.FindControl("lblpromotiontype");
                int protype = Convert.ToInt32(lblpromotiontype.Text);
                Label lblshopname = (Label)clickedRow.FindControl("Label6");
                Label ShopID = (Label)clickedRow.FindControl("lblshopid");
                shopid = Convert.ToInt32(ShopID.Text);
            
                if (protype == 1)
                    Response.Redirect("Campaign_promotion.aspx?Item_Code=" + Item_Code + "&Shop_ID=" + shopid);
                else if(protype ==2)
                    Response.Redirect("Promotion_Point_Entry.aspx?Item_Code=" + Item_Code + "&Shop_ID=" + shopid);
                else if(protype ==3)
                    Response.Redirect("Promotion_Delivery.aspx?Item_Code=" + Item_Code + "&Shop_ID=" + shopid);
               // Response.Redirect("../Item/Item_Master.aspx?Item_Code=" + Item_Code);

            }
            #endregion
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
             try
           {
            ehbbl = new Promotion_Point_BL();
            if (!String.IsNullOrWhiteSpace(txtcode.Text) || ddlmall.SelectedIndex != 0 || ddlAPIcheck.SelectedIndex != 0 || ddlexbresulterror.SelectedIndex != 0 || ddlexhibitor.SelectedIndex != 0 || ddlbatchcheck.SelectedIndex != 0 || !String.IsNullOrWhiteSpace(txtproname.Text) || !String.IsNullOrWhiteSpace(txtcatinfo.Text) || !String.IsNullOrWhiteSpace(txtbrandname.Text) || !String.IsNullOrWhiteSpace(txtcompname.Text) || !String.IsNullOrWhiteSpace(txtcompetitionname.Text) || !String.IsNullOrWhiteSpace(txtclassname.Text) || !String.IsNullOrWhiteSpace(txtyear.Text) || !String.IsNullOrWhiteSpace(txtseason.Text) || !String.IsNullOrWhiteSpace(txtremark.Text) || !String.IsNullOrWhiteSpace(Request.Form[txtexdatetime1.UniqueID]) || !String.IsNullOrWhiteSpace(Request.Form[txtdatetime2.UniqueID]))
            {
              
                string fromDate = Request.Form[txtexdatetime1.UniqueID];
                string toDate = Request.Form[txtdatetime2.UniqueID];
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
                if (ViewState["lists"] != null)
                {
                    list =ViewState["lists"].ToString();
                }
                if (ddlAPIcheck.SelectedItem.Value == "3")//for ponpare and amazone (not include)
                {
                    string value = "2";
                    DataTable dt = new DataTable();
                    if (chkitemcode.Checked)
                    {
                        gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                        //DataTable dtcount = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 16, null, value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                           // ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate,0,0);
                        dt = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 6, null, value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                            ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
                        int count = 0;
                        if(dt != null && dt.Rows.Count >0)
                        count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());


                        gvexhibition.DataSource = dt;
                        gvexhibition.DataBind();
                        gp.CalculatePaging(count, gvexhibition.PageSize, 1);
                    }

                    else
                    {
                        gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                        //DataTable dtc = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 20, null, value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                                                    //ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate,0,0);
                      
                       // int counts = Convert.ToInt32(dtc.Rows[0][0].ToString());
                        dt = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 10, null, value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                                                    ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
                        int counts = 0;
                        if (dt != null && dt.Rows.Count > 0)
                            counts = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["Mall_ID"].ToString() == "3" || dt.Rows[i]["Mall_ID"].ToString() == "4")
                            { }
                            else
                            {
                                dt.Rows[i].Delete();
                                counts = counts - 1;
                            }
                        }
                        dt.AcceptChanges();

                      


                        gvexhibition.DataSource = dt;
                        gvexhibition.DataBind();
                        gp.CalculatePaging(counts, gvexhibition.PageSize, 1);
                    }
                }
                else
                {
                    DataTable dt = new DataTable();
                    if (chkitemcode.Checked)
                    {
                        gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                      // DataTable dc = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 16, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                           //ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate,0,0);
                        dt = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 6, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                           ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
                         int count = 0;
                        if (dt != null && dt.Rows.Count > 0)
                            count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                        gvexhibition.DataSource = dt;
                        gvexhibition.DataBind();
                        gp.CalculatePaging(count, gvexhibition.PageSize, 1);
                    }
                    else
                    {
                        gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                      //  DataTable dtcv = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 20, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                       //  ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate,0,0);
                     //   int count = Convert.ToInt32(dtcv.Rows[0][0].ToString()); 
                        dt = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 10, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                         ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
                        int count = 0;
                        if (dt != null && dt.Rows.Count > 0)
                            count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                        if (ddlAPIcheck.SelectedItem.Value == "2")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i]["Mall_ID"].ToString() == "3" || dt.Rows[i]["Mall_ID"].ToString() == "4")
                                { dt.Rows[i].Delete(); count = count - 1; }
                            }
                            dt.AcceptChanges();
                        }


                        gvexhibition.DataSource = dt;
                        gvexhibition.DataBind();
                        gp.CalculatePaging(count, gvexhibition.PageSize, 1);
                    }
                }
                txtexdatetime1.Text = hdfFromDate.Value;
                txtdatetime2.Text = hdfToDate.Value;
                hdfFromDate.Value = String.Empty;
                hdfToDate.Value = String.Empty;
            }

            else
            {
                if (ViewState["lists"] != null)
                    list = ViewState["lists"].ToString();
                else
                    list = null;
                DataTable dt = new DataTable();
                if (chkitemcode.Checked)
                {
                    gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    //DataTable dtsc = ehbbl.SelectAllpaging(list, null, null, null, 16, null, null, null, null, null, null, null, null, null, null, null, null, null, null, FromDate, ToDate,0,0);
                    dt = ehbbl.SelectAllpaging(list, null, null, null, 6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, FromDate, ToDate, 1, gvexhibition.PageSize);
                    int count = 0;
                    if (dt != null && dt.Rows.Count > 0)
                        count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                   // int count = Convert.ToInt32(dtsc.Rows[0][0].ToString());
                    gvexhibition.DataSource = dt;
                    gvexhibition.DataBind();
                    gp.CalculatePaging(count, gvexhibition.PageSize, 1);
                }
                else
                {
                    gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                   // DataTable dtsxc = ehbbl.SelectAllpaging(list, null, null, null, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, FromDate, ToDate,0,0);
                    dt = ehbbl.SelectAllpaging(list, null, null, null, 10, null, null, null, null, null, null, null, null, null, null, null, null, null, null, FromDate, ToDate, 1, gvexhibition.PageSize);
                  //  int count = Convert.ToInt32(dtsxc.Rows[0][0].ToString());
                    int count = 0;
                    if (dt != null && dt.Rows.Count > 0)
                        count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                    gvexhibition.DataSource = dt;
                    gvexhibition.DataBind();
                    gp.CalculatePaging(count, gvexhibition.PageSize, 1);
                }
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
            try
            {
                DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                dtfi.ShortDatePattern = "dd-MM-yyyy";
                dtfi.DateSeparator = "-";
                DateTime objDate = Convert.ToDateTime(dateTime, dtfi);
                string date = Convert.ToDateTime(objDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy hh:MM:ss");
                objDate = DateTime.ParseExact(date, "MM/dd/yyyy hh:MM:ss", null);

                return objDate;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DateTime();
            }
        }
        protected void gvexhibition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
              
                    if (!String.IsNullOrWhiteSpace(txtcode.Text) || ddlmall.SelectedIndex != -1 || ddlAPIcheck.SelectedIndex != -1 || ddlexbresulterror.SelectedIndex != -1 || ddlexhibitor.SelectedIndex != -1 || ddlbatchcheck.SelectedIndex != -1 || !String.IsNullOrWhiteSpace(txtproname.Text) || !String.IsNullOrWhiteSpace(txtcatinfo.Text) || !String.IsNullOrWhiteSpace(txtbrandname.Text) || !String.IsNullOrWhiteSpace(txtcompname.Text) || !String.IsNullOrWhiteSpace(txtcompetitionname.Text) || !String.IsNullOrWhiteSpace(txtclassname.Text) || !String.IsNullOrWhiteSpace(txtyear.Text) || !String.IsNullOrWhiteSpace(txtseason.Text) || !String.IsNullOrWhiteSpace(txtremark.Text) || !String.IsNullOrWhiteSpace(Request.Form[txtexdatetime1.UniqueID]) || !String.IsNullOrWhiteSpace(Request.Form[txtdatetime2.UniqueID]))
                    {

                        string fromDate = Request.Form[txtexdatetime1.UniqueID];
                        string toDate = Request.Form[txtdatetime2.UniqueID];
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
                        list =ViewState["lists"].ToString();

                        DataTable dt = ehbbl.SelectAll(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 6, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                            ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate);
                        gvexhibition.DataSource = dt;
                        gvexhibition.DataBind();
                        txtexdatetime1.Text = hdfFromDate.Value;
                        txtdatetime2.Text = hdfToDate.Value;
                        hdfFromDate.Value = String.Empty;
                        hdfToDate.Value = String.Empty;
                    }

                    else
                    {
                        list =ViewState["lists"].ToString();

                        DataTable dt = ehbbl.SelectAll(list, null, null, null, 6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, FromDate, ToDate);
                        gvexhibition.DataSource = dt;
                        gvexhibition.DataBind();
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
                gvexhibition.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
             
                    if (!String.IsNullOrWhiteSpace(txtcode.Text) || ddlmall.SelectedIndex != -1 || ddlAPIcheck.SelectedIndex != -1 || ddlexbresulterror.SelectedIndex != -1 || ddlexhibitor.SelectedIndex != -1 || ddlbatchcheck.SelectedIndex != -1 || !String.IsNullOrWhiteSpace(txtproname.Text) || !String.IsNullOrWhiteSpace(txtcatinfo.Text) || !String.IsNullOrWhiteSpace(txtbrandname.Text) || !String.IsNullOrWhiteSpace(txtcompname.Text) || !String.IsNullOrWhiteSpace(txtcompetitionname.Text) || !String.IsNullOrWhiteSpace(txtclassname.Text) || !String.IsNullOrWhiteSpace(txtyear.Text) || !String.IsNullOrWhiteSpace(txtseason.Text) || !String.IsNullOrWhiteSpace(txtremark.Text) || !String.IsNullOrWhiteSpace(Request.Form[txtexdatetime1.UniqueID]) || !String.IsNullOrWhiteSpace(Request.Form[txtdatetime2.UniqueID]))
                    {

                        string fromDate = Request.Form[txtexdatetime1.UniqueID];
                        string toDate = Request.Form[txtdatetime2.UniqueID];
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
                        list =ViewState["lists"].ToString();
                         gvexhibition.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
                       //  DataTable dtsxc = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 16, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                             //ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate,0,0);
                         DataTable dt = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 6, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
                            ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
                         int count = 0;
                        if(dt != null && dt.Rows.Count>0)
                            count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                         gvexhibition.DataSource = dt;
                         gvexhibition.DataBind();
                         gp.CalculatePaging(count, gvexhibition.PageSize, 1);
                        txtexdatetime1.Text = hdfFromDate.Value;
                        txtdatetime2.Text = hdfToDate.Value;
                        hdfFromDate.Value = String.Empty;
                        hdfToDate.Value = String.Empty;
                    }

                    else
                    {
                        gvexhibition.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
                        list =ViewState["lists"].ToString();
                       // DataTable dtsc = ehbbl.SelectAllpaging(null, null, null, null, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, FromDate, ToDate,0,0);
                        DataTable dt = ehbbl.SelectAllpaging(null, null, null, null, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, FromDate, ToDate, 1, gvexhibition.PageSize);
                        int count = 0;
                        if(dt != null && dt.Rows.Count >0)
                            count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                        gvexhibition.DataSource = dt;
                        gvexhibition.DataBind();
                        gp.CalculatePaging(count, gvexhibition.PageSize, 1);
                    }
            
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvexhibition_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
               

                HtmlGenericControl r = e.Row.FindControl("r") as HtmlGenericControl;
                HtmlGenericControl y = e.Row.FindControl("y") as HtmlGenericControl;
                HtmlGenericControl p = e.Row.FindControl("p") as HtmlGenericControl;
                HtmlGenericControl j = e.Row.FindControl("j") as HtmlGenericControl;
                HtmlGenericControl a = e.Row.FindControl("a") as HtmlGenericControl;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(1))
                    {
                        Label lblr = e.Row.FindControl("Label6") as Label;
                        if (!String.IsNullOrWhiteSpace(lblr.Text))
                            lblr.Style.Add("Width", "200px");

                        r.Style.Add("float", "left");


                        lblr.Visible = true;
                        r.Visible = true;
                        y.Visible = false;
                        p.Visible = false;
                        j.Visible = false;
                        a.Visible = false;
                       
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(2))
                    {
                        Label lbly = e.Row.FindControl("Label3") as Label;
                        if (!String.IsNullOrWhiteSpace(lbly.Text))

                            lbly.Style.Add("Width", "200px");

                        y.Style.Add("float", "left");
                        lbly.Visible = true;
                        r.Visible = false;
                        y.Visible = true;
                        p.Visible = false;
                        j.Visible = false;
                        a.Visible = false;
                       
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(3))
                    {
                        Label lblp = e.Row.FindControl("Label4") as Label;
                        if (!String.IsNullOrWhiteSpace(lblp.Text))
                            lblp.Style.Add("Width", "200px");

                        p.Style.Add("float", "left");
                        lblp.Visible = true;
                        r.Visible = false;
                        y.Visible = false;
                        p.Visible = true;
                        j.Visible = false;
                        a.Visible = false;
                      
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(4))
                    {
                        Label lbla = e.Row.FindControl("Label7") as Label;
                        if (!String.IsNullOrWhiteSpace(lbla.Text))
                            lbla.Style.Add("Width", "200px");

                        a.Style.Add("float", "left");
                        lbla.Visible = true;
                        r.Visible = false;
                        y.Visible = false;
                        p.Visible = false;
                        j.Visible = false;
                        a.Visible = true;
                      
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(5))
                    {
                        Label lblj = e.Row.FindControl("Label5") as Label;
                        if (!String.IsNullOrWhiteSpace(lblj.Text))
                            lblj.Style.Add("Width", "200px");

                        j.Style.Add("float", "left");
                        lblj.Visible = true;
                        r.Visible = false;
                        y.Visible = false;
                        p.Visible = false;
                        j.Visible = true;
                        a.Visible = false;
                      
                    }
                    if (DataBinder.Eval(e.Row.DataItem, "API_Check").ToString().ToLower() == Convert.ToString(1))
                    {

                        e.Row.Cells[9].Text = "×";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "API_Check").ToString().ToLower() == Convert.ToString(0))
                    {
                        if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(3) || DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(4))
                        { e.Row.Cells[9].Text = "_"; }
                        else
                        {
                            e.Row.Cells[9].Text = "未";
                        }

                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "API_Check").ToString().ToLower() == Convert.ToString(2))
                    {
                        if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(3) || DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(4))
                        { e.Row.Cells[9].Text = "対象外"; }

                        else { e.Row.Cells[9].Text = "○"; }

                    }


                    if (DataBinder.Eval(e.Row.DataItem, "Batch_Check").ToString().ToLower() == Convert.ToString(1))
                    {

                        e.Row.Cells[10].Text = "×";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Batch_Check").ToString().ToLower() == Convert.ToString(0))
                    {
                        e.Row.Cells[10].Text = "未";

                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Batch_Check").ToString().ToLower() == Convert.ToString(2))
                    {
                        e.Row.Cells[10].Text = "○";

                    }
                    if (DataBinder.Eval(e.Row.DataItem, "ExportError_Check").ToString().ToLower() == Convert.ToString(1))
                    {

                        e.Row.Cells[8].Text = "×";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "ExportError_Check").ToString().ToLower() == Convert.ToString(0))
                    {
                        e.Row.Cells[8].Text = "未";

                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "ExportError_Check").ToString().ToLower() == Convert.ToString(2))
                    {
                        e.Row.Cells[8].Text = "○";

                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtexdatetime1.Text = String.Empty;
                hdfFromDate.Value = Request.Form[txtexdatetime1.UniqueID];
                txtdatetime2.Text = Request.Form[txtdatetime2.UniqueID];
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
                txtdatetime2.Text = String.Empty;
                hdfToDate.Value = Request.Form[txtdatetime2.UniqueID];
                txtexdatetime1.Text = Request.Form[txtexdatetime1.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}