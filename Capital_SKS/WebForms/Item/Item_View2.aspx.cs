/* 


 Tables using: Item,Item_Master.
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
using System.Collections;
using System.Configuration;
using System.Text;
using System.IO;
using System.Globalization;
using System.Net;
using System.Drawing;
using ORS_RCM.WebForms.Item;
using ORS_RCM_Common;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using ClosedXML.Excel;
using Newtonsoft.Json;

namespace ORS_RCM
{
    public partial class Item_View2 : System.Web.UI.Page
    {
        public int User_ID
        {
            get
            {
                if (Session["User_ID"] != null)
                {
                    return Convert.ToInt32(Session["User_ID"]);
                }
                else
                {
                    return 0;
                }
            }
        }
        Item_Master_BL itemMasterBL;
        Item_BL itbl;
        Item_Information_BL iteminfo_bl;
        Item_ExportField_BL itfield_bl; 
        String[] Exfield = new String[1000]; 
        DataTable dtExport = null;
        DataTable dtExportdata = null; DataTable ds = null; 
        Item_Master_BL imbl = new Item_Master_BL();
        Item_Shop_BL ItemShopBL = new Item_Shop_BL();
        string FilePath = ConfigurationManager.AppSettings["ExportFieldCSVPath"].ToString();
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        String rakuten = ConfigurationManager.AppSettings["rakuten"].ToString();
        String yahoo = ConfigurationManager.AppSettings["yahoo"].ToString();
        String wowma = ConfigurationManager.AppSettings["wowma"].ToString();
        String jisha = ConfigurationManager.AppSettings["jisha"].ToString();
        String tennis = ConfigurationManager.AppSettings["tennis"].ToString();
        string ExcelExport = ConfigurationManager.AppSettings["ExcelExport"].ToString();

        string search = string.Empty;
        int pagesize = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtitemno.Attributes.Add("onKeyPress", "doClick('" + btnsearch.ClientID + "',event)");
                if (!IsPostBack)
                {
                    if (Request.Cookies["userInfo"] != null)
                    {
                        HttpCookie aaCookie = Request.Cookies["userInfo"];
                        HttpCookie infoCookie = Request.Cookies["popupinfo"];
                        HttpCookie Cookie = Request.Cookies["lastuser"];
                        if (Request.Cookies["popupinfo"] != null)
                        {
                        //    lbltest.Text = Server.HtmlEncode(aaCookie.Value) + "$" + Server.HtmlEncode(infoCookie.Value);
                            string hfval = Server.HtmlEncode(infoCookie.Value);
                            string[] xt = hfval.Split('=');
                            if (Request.Cookies["lastuser"] != null)
                            {
                                string username = Server.HtmlEncode(aaCookie.Value);
                                string[] uname = username.Split('=');
                                string lastuser = Server.HtmlEncode(Cookie.Value);
                                string[] lusername = lastuser.Split('=');
                                if (uname[1].ToString().Equals(lusername[2].ToString()))
                                {
                                    if (xt.Count() >1)
                                    {
                                        hfShowHide.Value = xt[1].ToString();
                                        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "SearchClick();", true);
                                    }//
                                }
                            }
                        }
                        if (Request.Cookies["userInfo"] != null)
                        {
                           string lbltest = Server.HtmlEncode(aaCookie.Value);
                            Response.Cookies["lastuser"]["lusername"] = lbltest;
                            HttpCookie aCookie = new HttpCookie("lastuser");
                            aCookie.Values["lusername"] = lbltest;
                            aCookie.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(aCookie);
                        }
                    }
                  


                    Item_Information_BL iibl = new Item_Information_BL();
                    //Bind();//for gvpaging
                    ItemView2_PageLoad(); // added by aam
                    //gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    ////gvItem.DataBind();
                    //ViewState["pagesize"] = Convert.ToInt32(ddlpage.SelectedValue);
                    //int count = 0;
                    //if (gvItem.Rows.Count > 0)
                    //{
                    //    Label tc = (Label)gvItem.Rows[0].FindControl("lblTotalCount");
                    //    count = Convert.ToInt32(tc.Text);
                    //}
                    //gp.CalculatePaging(count, gvItem.PageSize, 1);
                    
                    ItemCheck_Change();//for checkbox
                    ArrayList arrchk = new ArrayList();
                    ViewState["list"] = arrchk;

                    BindDropDownListForShop();
                    itbl = new Item_BL();

                    iteminfo_bl = new Item_Information_BL();
                    itfield_bl = new Item_ExportField_BL();

                    //gvItem.Columns[22].Visible = false; //ami

                    ddlname.DataSource = itfield_bl.SelectAll();
                    ddlname.DataTextField = "Export_Name";
                    ddlname.DataValueField = "ID";
                    ddlname.DataBind();
                    ddlname.Items.Insert(0, "");
                    //ddlname.Items.Insert(1, "Item_Option");
                    //ddlname.Items.Insert(2, "Item_Category");
                    //ddlname.Items.Insert(3, "Item_Image");
                }
                else
                {
                    String ctrl = getPostBackControlName();
                    if (ctrl != null && ctrl.Contains("btnSKU"))
                    { }
                    else if (ctrl != null && ctrl.Contains("ddlpage")) { }
                    else if (ctrl != null && ctrl.Contains("btndelete")) { }
                    
                    else if (hfCtrl.Value.ToString().Contains("ddlshoppage") && String.IsNullOrWhiteSpace(ctrl))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        ddlshoppage_SelectedIndexChanged(Convert.ToInt32(index));
                        hfCtrl.Value = String.Empty;
                        //hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("lnkItemNo") && String.IsNullOrWhiteSpace(ctrl))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        hfCtrl.Value = String.Empty;

                        lnkItemNo_Click(index);
                    }
                    else if (ctrl.Contains("lnkPaging"))
                    {
                        iteminfo_bl = new Item_Information_BL();
                        Item_Master_Entity ime = new Item_Master_Entity();
                        string search;
                        if (ViewState["search"] != null && !ViewState["search"].Equals("-1"))
                        {
                            search = ViewState["search"].ToString();
                            if (ViewState["search"] == search)
                            {
                                string json = (string)ViewState["btnsearch"];
                                ime = JsonConvert.DeserializeObject<Item_Master_Entity>(json);
                            }
                        }
                        else
                        {
                            ime = GetEntity();
                            string json1 = JsonConvert.SerializeObject(ime);
                            ViewState["bind"] = json1;
                            string json = (string)ViewState["bind"];
                            ime = JsonConvert.DeserializeObject<Item_Master_Entity>(json);
                        }
                        DataTable dt = new DataTable();

                        ArrayList arrlst = new ArrayList();
                        if (ViewState["checkedValue"] != null)
                        {
                            arrlst = ViewState["checkedValue"] as ArrayList;
                        }

                        gvItem.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
                        gp.LinkButtonClick(ctrl, gvItem.PageSize);

                        Label lbl = gp.FindControl("lblCurrent") as Label;
                        int index = Convert.ToInt32(lbl.Text);

                        gvItem.PageIndex = Convert.ToInt32(index);
                        if (chkno.Checked)
                        {
                            gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                            dt = iteminfo_bl.ItemView2_Search(ime, index, gvItem.PageSize, 2);//(searchkey,pageIndex,pagesize,option(2=equal,1=like))
                            int count = 0;
                            if (dt.Rows.Count > 0)
                                count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());

                            gvItem.DataSource = dt;
                            gvItem.DataBind();
                        }
                        else
                        {
                            gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                            dt = iteminfo_bl.ItemView2_Search(ime, index, gvItem.PageSize, 1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like))
                            int count = 0;
                            if (dt.Rows.Count > 0)
                                count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());

                            gvItem.DataSource = dt;
                            gvItem.DataBind();
                        }                     
                        ItemCheck_Change();
                    }                   
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void BindPerson()
        {
            try
            {
                Item_BL itbl = new Item_BL();

                ddlpersonincharge.DataSource = itbl.bindDDL();
                ddlpersonincharge.DataTextField = "User_Name";
                ddlpersonincharge.DataValueField = "ID";
                ddlpersonincharge.DataBind();
                ddlpersonincharge.Items.Insert(0, "");
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void lnkItemNo_Click(String index1)
        {
            try
            {
                int index = Convert.ToInt32(index1);
                Label lnk = gvItem.Rows[index].FindControl("lnkItemNo") as Label;
                //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey1", "ShopPreview('" +  + "')", true);
                Response.Redirect("Item_Master.aspx?Item_Code=" + lnk.Text,false);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void ddlshoppage_SelectedIndexChanged(int index)
        {
            try
            {
                DropDownList ddl = gvItem.Rows[index].FindControl("ddlshoppage") as DropDownList;

                String shopID = ddl.SelectedValue.ToString();
                String mallid = "0";
                Item_View3_BL iv3bl = new Item_View3_BL();
                iteminfo_bl = new Item_Information_BL();
                DataTable dt = iv3bl.GetMallByShopID(shopID);
                String shopName = String.Empty;
                if (dt.Rows.Count > 0)
                {
                    mallid = dt.Rows[0]["Mall_ID"].ToString();
                    shopName = dt.Rows[0]["Shop_SiteName"].ToString();
                }


                Label lnk = gvItem.Rows[index].FindControl("lnkItemNo") as Label;
                String itemCode = lnk.Text;

                if (mallid.Equals("1"))
                {
                    rakuten += shopName + "/" + itemCode;
                    Response.Redirect(rakuten,false);
                    //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey1", "ShopPreview('"+rakuten+"')", true);
                }
                else if (mallid.Equals("2"))
                {
                    yahoo += shopName + "/" + itemCode + ".html";
                    Response.Redirect(yahoo,false);
                    //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey1", "ShopPreview('" + yahoo + "')", true);
                }
                else if (mallid.Equals("4"))
                {
                    String lot = iteminfo_bl.Get_Lot_Number(itemCode);
                    wowma += lot;
                    Response.Redirect(wowma, false);

                    //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey1", "ShopPreview('" + ponpare + "')", true);
                }
                else if (mallid.Equals("6"))
                {
                    jisha += itemCode;
                    Response.Redirect(jisha, false);
                }
                else if (mallid.Equals("7"))
                {
                    tennis += itemCode;
                    Response.Redirect(tennis, false);
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

        public Item_Master_Entity GetEntity()
        {
            try
            {
                Item_Master_Entity ime = new Item_Master_Entity();
                string itemcode = string.Empty;                
                itemcode = txtitemno.Text.Trim();   // if Item_Code,
                string replace = ",";
                ime.Item_Code = itemcode.Replace(",\r\n", replace).Replace(",\n", replace).Replace("\r\n", replace).Replace("\n", replace).Replace("\r", replace);
                if (ime.Item_Code.EndsWith(","))
                    ime.Item_Code = ime.Item_Code.TrimEnd(',');
                ime.InstructionNo = txtinstrauctionno.Text.Trim();
                //ime.Item_Code = txtitemno.Text.Trim();
                ime.Brand_Name = txtbrandname.Text.Trim();
                ime.Catalog_Information = txtcatinfo.Text.Trim();
                if (!String.IsNullOrWhiteSpace(ddlsksstatus.SelectedValue.ToString()))
                    ime.Export_Status = Convert.ToInt32(ddlsksstatus.SelectedValue);
                ime.ProductName = txtproductname.Text.Trim();
                ime.Product_Code = txtmanproductcode.Text.Trim();
                ime.Company_Name = txtconmpanyname.Text.Trim();
                ime.Competition_Name = txtcompetitionname.Text.Trim();
                ime.Class_Name = txtclassname.Text.Trim();
                if (!String.IsNullOrWhiteSpace(ddlspecialflag.SelectedValue.ToString()))
                    ime.Special_Flag = Convert.ToInt32(ddlspecialflag.SelectedValue);
                if (!String.IsNullOrWhiteSpace(ddlreservationflag.SelectedValue.ToString()))
                    ime.Reservation_Flag = Convert.ToInt32(ddlreservationflag.SelectedValue);
                ime.Year = txtyear.Text.Trim();
                ime.Season = txtseason.Text.Trim();
                if (!String.IsNullOrWhiteSpace(ddlshopstatus.SelectedValue.ToString()))
                    ime.Ctrl_ID = ddlshopstatus.SelectedValue;
                ime.Remark = txtremark.Text.Trim();
                ime.JanCode = txtjancode.Text.Trim();
                ime.Sale_Code = txtsalemanagementcode.Text.Trim();
                string fromDate = Request.Form[txtdate.UniqueID];
                string toDate = Request.Form[txtdateapproval.UniqueID];
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

                ime.FromDate = FromDate;
                ime.ToDate = ToDate;
                ime.PersonInCharge = ddlpersonincharge.SelectedValue;
                if (string.IsNullOrWhiteSpace(ddlSellingPrice.SelectedValue))
                {
                    ime.Price = -1;
                }
                else
                {
                    ime.Price = Convert.ToInt32(ddlSellingPrice.SelectedValue);
                }
                ime.ShopID = Convert.ToInt16(ddlExhibiton.SelectedValue);
                return ime;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new Item_Master_Entity();
            }
        }

        protected void btnsearch_Click(object sender,EventArgs e)
        {
            try
            {
                string hidden = hfShowHide.Value;
                Response.Cookies["popupinfo"]["lastinfo"] = hidden;
                HttpCookie aCookie = new HttpCookie("popupinfo");
                aCookie.Values["lastinfo"] = hidden;
                aCookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(aCookie);
               ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "SearchClick();", true);
                Item_Master_Entity ime = GetEntity();
                string json = JsonConvert.SerializeObject(ime);
                ViewState["btnsearch"] = json;
                iteminfo_bl = new Item_Information_BL();

                if (chkno.Checked)
                {
                    gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    DataTable dt = iteminfo_bl.ItemView2_Search(ime, 1, gvItem.PageSize, 2);//(searchkey,pageIndex,pagesize,option(2=equal,1=like),1=search,0=select all)
                    int count = 0;
                    if (dt.Rows.Count > 0)
                        count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());

                    gvItem.DataSource = dt;
                    gvItem.DataBind();
                    gp.CalculatePaging(count, gvItem.PageSize, 1);
                    ViewState["SearchDataID"] = null;
                    ViewState["SearchDataID"] = dt;
                }
                else
                {
                    gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    DataTable dt = iteminfo_bl.ItemView2_Search(ime, 1, gvItem.PageSize, 1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like),1=search,0=select all)
                    int count = 0;
                    if (dt.Rows.Count > 0)
                        count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());

                    gvItem.DataSource = dt;
                    gvItem.DataBind();
                    gp.CalculatePaging(count, gvItem.PageSize, 1);
                    ViewState["SearchDataID"] = null;
                    ViewState["SearchDataID"] = dt;
                }
                string search = "search";
                ViewState["search"] = search;
                txtdate.Text = hdfFromDate.Value;
                txtdateapproval.Text = hdfToDate.Value;
                ddlname.Enabled = true;
                lnkdownload.Text = String.Empty;
                ViewState.Remove("checkedValue"); // After various search and check, clean previous check value.

                //string item=string.Empty;
                //if (!string.IsNullOrWhiteSpace(ViewState["item"].ToString()))
                //    item=ViewState["item"].ToString();
                //gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                //gvItem.PageIndex = 0;
                //gvItem.DataBind();
                ////ViewState["search"] = iteminfo_bl.SearchItem_View2_Data(search, item, gvItem.PageSize, 1);
                //ViewState["search"] = search;
                //int count = 0;
                //if (gvItem.Rows.Count > 0)
                //{
                //    Label tc = (Label)gvItem.Rows[0].FindControl("lblTotalCount");
                //    count = Convert.ToInt32(tc.Text);
                //}
                //gp.CalculatePaging(count, gvItem.PageSize, 1);
                
                //txtdate.Text = hdfFromDate.Value;
                //txtdateapproval.Text = hdfToDate.Value;
                //ddlname.Enabled = true;
                //lnkdownload.Text = String.Empty;

                //ViewState.Remove("checkedValue"); // After various search and check, clean previous check value.
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void Bind()
        {
            try
            {              
                Item_Master_Entity ime = GetEntity();
                iteminfo_bl = new Item_Information_BL();
                string json = JsonConvert.SerializeObject(ime);
                ViewState["bind"] = json;
                if (chkno.Checked)
                {
                    gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    DataTable dt = iteminfo_bl.ItemView2_Search(ime, 1, gvItem.PageSize, 2);//(searchkey,pageIndex,pagesize,option(2=equal,1=like))
                    int count = 0;
                    if (dt.Rows.Count > 0)
                        count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());

                    gvItem.DataSource = dt;
                    gvItem.DataBind();
                    gp.CalculatePaging(count, gvItem.PageSize, 1);
                }
                else
                {
                    gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    DataTable dt = iteminfo_bl.ItemView2_Search(ime, 1, gvItem.PageSize, 1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like))
                    int count = 0;
                    if (dt.Rows.Count > 0)
                        count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());

                    gvItem.DataSource = dt;
                    gvItem.DataBind();
                    gp.CalculatePaging(count, gvItem.PageSize, 1);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ItemView2_PageLoad()
        {
            try
            {
                Item_Master_Entity ime = GetEntity();
                iteminfo_bl = new Item_Information_BL();

                gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                //DataTable dt = iteminfo_bl.ItemView2_PageLoad(ime, 1, gvItem.PageSize, 2);//(searchkey,pageIndex,pagesize,option(2=equal,1=like))
                DataTable dt = iteminfo_bl.ItemView2_Search(ime, 1, gvItem.PageSize, 3);
                int count = 0;
                if (dt.Rows.Count > 0)
                    count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());

                gvItem.DataSource = dt;
                gvItem.DataBind();
                gp.CalculatePaging(count, gvItem.PageSize, 1);

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected Boolean IsAllCheck(ArrayList arrlst)
        {
            if (ViewState["ItemAll"] != null)
            {
                DataTable dtItemAll = ViewState["ItemAll"] as DataTable;
                if(dtItemAll!=null && dtItemAll.Rows.Count>0)
                for (int i = 0; i < dtItemAll.Rows.Count; i++)
                {
                    if (dtItemAll.Columns.Contains("ID"))
                    {
                        if (!arrlst.Contains(dtItemAll.Rows[i]["ID"].ToString()))
                            return false;
                    }
                }
                return true;
            }
            return false;
        }

        protected void ItemCheck_Change()
        {
            try
            {
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    CheckBox chk;
                    Label lbl;
                    for (int i = 0; i < gvItem.Rows.Count; i++)
                    {
                        chk = gvItem.Rows[i].FindControl("ckItem") as CheckBox;
                        lbl = gvItem.Rows[i].FindControl("Label45") as Label;

                        if (arrlst.Contains(lbl.Text))
                            chk.Checked = true;
                        else chk.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ckItem_Check(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;
                GridViewRow row = chk.NamingContainer as GridViewRow;
                int rowIndex = row.RowIndex;

                Label lbl = gvItem.Rows[rowIndex].FindControl("Label45") as Label;

                if (ViewState["checkedValue"] != null)
                {
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    CheckBox chkHeader = gvItem.HeaderRow.FindControl("ckall") as CheckBox;
                    if (!chk.Checked)
                    {
                        //if one of check box is unchecked then header checkbox set to uncheck
                        chkHeader.Checked = false;
                        if (arrlst.Contains(lbl.Text))
                        {
                            arrlst.Remove(lbl.Text);
                            ViewState["checkedValue"] = arrlst;
                        }
                    }
                    else
                    {
                        arrlst.Add(lbl.Text);
                        ViewState["checkedValue"] = arrlst;

                        //check all select check box is check && if all check,set header checkbox to checked
                        if (IsAllCheck(arrlst))
                            chkHeader.Checked = true;
                        else
                            chkHeader.Checked = false;
                    }
                }
                else
                {
                    ArrayList arrlst = new ArrayList();
                    arrlst.Add(lbl.Text);
                    ViewState["checkedValue"] = arrlst;
                }

                ArrayList al = ViewState["checkedValue"] as ArrayList;
                hfNewTab.Text = String.Empty;
                if (al.Count > 0)
                {
                    for (int i = 0; i < al.Count; i++)
                    {
                        DataTable dtItemAll = new DataTable();
                        //if(Cache["SearchItem"]!=null)
                        if (ViewState["ItemAll"] != null)
                            dtItemAll = ViewState["ItemAll"] as DataTable;
                        if (dtItemAll.Columns.Contains("ID"))
                        {
                            DataRow[] dr = dtItemAll.Select("ID='" + al[i] + "'");
                            if (dr.Count() > 0)
                            {
                                dtItemAll = dtItemAll.Select("ID='" + al[i] + "'").CopyToDataTable();
                                int rowno = Convert.ToInt32(dtItemAll.Rows[0]["No"].ToString());
                                if (rowno % Convert.ToInt32(ddlpage.SelectedValue) == 0)
                                    rowno = Convert.ToInt32(ddlpage.SelectedValue);
                                else rowno = rowno % Convert.ToInt32(ddlpage.SelectedValue);
                                DropDownList ddl = gvItem.Rows[rowno - 1].FindControl("ddlshoppage") as DropDownList;
                                if (ddl.Items.Count <= 1)
                                {
                                    hfNewTab.Text = "1";
                                    break;
                                }
                            }
                        }
                    }
                }
                else hfNewTab.Text = "1";//allow new tab
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void CheckChangeGrid(Boolean check)
        {
            try
            {
                ArrayList arrCheckValue = new ArrayList();
                ArrayList chk = new ArrayList();
                if (check == true)
                {
                    ////if (ViewState["ItemAll"] != null)
                    ////{
                    ////    DataTable dtItemAll = ViewState["ItemAll"] as DataTable;
                    //DataTable dtItemAll = new DataTable();
                    //if (ViewState["ItemAll"] != null)
                    //{
                    //    dtItemAll = ViewState["ItemAll"] as DataTable;
                    //    for (int i = 0; i < dtItemAll.Rows.Count; i++)
                    //    {
                    //        if (dtItemAll.Columns.Contains("ID"))
                    //            arrCheckValue.Add(dtItemAll.Rows[i]["ID"].ToString());
                    //    }
                    //}
                    //updated date 11/6/15
                    if (ViewState["checkedValue"] != null)
                    {
                        chk = ViewState["checkedValue"] as ArrayList;

                        for (int j = 0; j < chk.Count; j++)
                        {
                            arrCheckValue.Add(chk[j].ToString());

                        }

                    }
                    for (int i = 0; i < gvItem.Rows.Count; i++)
                    {
                        Label lbl = gvItem.Rows[i].FindControl("Label45") as Label;
                        CheckBox ckb = gvItem.Rows[i].FindControl("ckItem") as CheckBox;
                        if (ckb.Enabled)
                        {
                            arrCheckValue.Add(lbl.Text);
                        }
                    }
                }

                ViewState["checkedValue"] = arrCheckValue;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    
        protected void ckall_Check(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;
                CheckChangeGrid(chk.Checked);
                ItemCheck_Change();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    
        protected void gvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //iteminfo_bl = new Item_Information_BL();
            //if (!String.IsNullOrWhiteSpace(txtitemno.Text.Trim()) || !String.IsNullOrWhiteSpace(txtbrandname.Text.Trim()) || !String.IsNullOrWhiteSpace(txtcatinfo.Text.Trim()) || !String.IsNullOrWhiteSpace(txtdate.Text.Trim()) || !String.IsNullOrWhiteSpace(txtdateapproval.Text.Trim()) || !String.IsNullOrWhiteSpace(txtproductname.Text.Trim()) || !String.IsNullOrWhiteSpace(txtmanproductcode.Text.Trim()) || !String.IsNullOrWhiteSpace(txtconmpanyname.Text.Trim()) ||
            //    !String.IsNullOrWhiteSpace(txtcompetitionname.Text.Trim()) || !String.IsNullOrWhiteSpace(txtclassname.Text.Trim()) || !String.IsNullOrWhiteSpace(txtyear.Text.Trim()) || !String.IsNullOrWhiteSpace(txtseason.Text.Trim()) || !String.IsNullOrWhiteSpace(txtremark.Text.Trim()) || !String.IsNullOrWhiteSpace(txtjancode.Text.Trim()) || !String.IsNullOrWhiteSpace(txtsalemanagementcode.Text.Trim()) || ddlpersonincharge.SelectedIndex != -1 || ddlsksstatus.SelectedIndex != -1 || ddlspecialflag.SelectedIndex != -1 || ddlreservationflag.SelectedIndex != -1 || ddlshopstatus.SelectedIndex != -1|| !String.IsNullOrWhiteSpace(Request.Form[txtdate.UniqueID]) || !String.IsNullOrWhiteSpace(Request.Form[txtdateapproval.UniqueID]))
            //{
            //    string fromDate = Request.Form[txtdate.UniqueID];
            //    string toDate = Request.Form[txtdateapproval.UniqueID];
            //    hdfFromDate.Value = fromDate;
            //    hdfToDate.Value = toDate;
            //    DateTime? FromDate = new DateTime();
            //    DateTime? ToDate = new DateTime();
            //    if (!String.IsNullOrEmpty(fromDate))
            //    {
            //        FromDate = DateConverter(fromDate);
            //    }
            //    else
            //    {
            //        FromDate = null;
            //    }
            //    if (!String.IsNullOrEmpty(toDate))
            //    {
            //        ToDate = DateConverter(toDate);
            //    }
            //    else
            //    {
            //        ToDate = null;
            //    }
            //    DataTable dt = iteminfo_bl.SearchbyItem(txtitemno.Text.Trim(), txtbrandname.Text.Trim(), txtcatinfo.Text.Trim(), ddlsksstatus.SelectedItem.Value, txtproductname.Text.Trim(), txtmanproductcode.Text.Trim(), txtconmpanyname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), ddlspecialflag.SelectedItem.Value, ddlreservationflag.SelectedItem.Value, txtyear.Text.Trim(), txtseason.Text.Trim(), ddlsksstatus.SelectedItem.Value, txtremark.Text.Trim(), txtjancode.Text.Trim(), txtsalemanagementcode.Text.Trim(),FromDate,ToDate,ddlpersonincharge.SelectedItem.Value);
            //    gvItem.DataSource = dt;
            //    ViewState["dt"] = dt;
           
            //    gvItem.PageIndex = e.NewPageIndex;
            //    gvItem.DataBind();
            //    txtdate.Text = hdfFromDate.Value;
            //    txtdateapproval.Text = hdfToDate.Value;
            //    hdfFromDate.Value = String.Empty;
            //    hdfToDate.Value = String.Empty;
               
            //}
            //else
            //{
            //    DataTable dt = iteminfo_bl.SearchbyItem(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,null,null,null);
            //    gvItem.DataSource = dt;
            //    ViewState["dt"] = dt;
                
            //    gvItem.PageIndex = e.NewPageIndex;
            //    gvItem.DataBind();
                

            //}

           

            
            //getCheckValue();
        }

        protected void getCheckValue()
        {
            try
            {
                if (ViewState["list"] != null)
                {
                    ArrayList arrchk = ViewState["list"] as ArrayList;
                    if (ViewState["dt"] != null)
                    {
                        DataTable dt = ViewState["dt"] as DataTable;
                        if (dt.Rows.Count == arrchk.Count)
                        {
                            CheckBox chk = gvItem.HeaderRow.FindControl("ckall") as CheckBox;
                            chk.Checked = true;

                        }
                        else if (arrchk.Count == 0)
                        {
                            CheckBox chk = gvItem.HeaderRow.FindControl("ckall") as CheckBox;
                            chk.Checked = false;

                            for (int i = 0; i < gvItem.Rows.Count; i++)
                            {
                                chk = gvItem.Rows[i].FindControl("ckItem") as CheckBox;
                                chk.Checked = false;

                            }
                        }
                    }


                    //for (int i = 0; i < gvItem.Rows.Count; i++)
                    //{
                    //    for (int j = 0; j < arrchk.Count; j++)
                    //    {
                    //        string id = gvItem.Rows[i].Cells[19].Text;
                    //        CheckBox chk = gvItem.Rows[i].FindControl("ckbItem") as CheckBox;
                    //        if (id.Equals(arrchk[j]))
                    //        {  
                    //            chk.Checked = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    CheckBox ChkBoxHeader = (CheckBox)gvItem.HeaderRow.FindControl("ckall");
                    foreach (GridViewRow row in gvItem.Rows)
                    {
                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckItem");
                        if (ChkBoxHeader.Checked == true)
                        {
                            ChkBoxRows.Checked = true;

                        }
                        else
                        {
                            ChkBoxRows.Checked = false;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
        }

        protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DataEdit")
                {
                    string Item_Code = e.CommandArgument.ToString();
                   // string url = "Item_Master.aspx?Item_Code=" + Item_Code;
                    Response.Redirect("Item_Master.aspx?Item_Code=" + Item_Code,false);
                   // Response.Redirect(url);
                 
                }
                //if (e.CommandName == "SKU")
                //{
                //    hfview.Value = "test";
                //    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                //    string it_code = e.CommandArgument.ToString();
                //    Button btn = (Button)clickedRow.FindControl("btnSKU");
                //    btn.OnClientClick = "Show('" + it_code.ToString() + "')";
                //}

            }
            catch (Exception ex) {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
   
        protected void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Item_Master.aspx",false);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnGenerate1_Click(object sender, EventArgs e)
        {
            try
            {
                String[] data = new String[100];
                iteminfo_bl = new Item_Information_BL();
                itfield_bl = new Item_ExportField_BL();
                dtExport = new DataTable();
                itemMasterBL = new Item_Master_BL();
                DataTable dtCSV = new DataTable();
                // string stroption = ViewState["Option"] as string;
                //   string stroption = hfoption.Value.ToString();
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList arrchk = ViewState["checkedValue"] as ArrayList;
                    String csv = String.Join(",", (string[])arrchk.ToArray(Type.GetType("System.String")));
                    if (ViewState["Option"].ToString() == "1")
                    {
                        DataTable dtoption = itfield_bl.Selectoption_Cat(csv, 1);
                        string filename = ddlname.SelectedItem.ToString() + ".csv";
                        CSV(dtoption, filename);
                        lnkdownload.Text = filename;
                        //ViewState["Option"] = String.Empty;
                        //GlobalUI.MessageBox("Successful Export CSV!"); 
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Successful Export CSV!');", true);

                    }
                    //if (stroption == "2")
                    else if (ViewState["Option2"].ToString() == "2")
                    {

                        DataTable dtoption = itfield_bl.Selectoption_Cat(csv, 0);
                        string filename = ddlname.SelectedItem.ToString() + ".csv";
                        CSV(dtoption, filename);
                        lnkdownload.Text = filename;
                        //ViewState["Option"] = String.Empty;
                        //GlobalUI.MessageBox("Successful Export CSV!");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Successful Export CSV!');", true);
                    }
                }
                else
                {
                    String csv = String.Empty;
                    if (ViewState["ExportField"] != null)
                    {

                        if (ViewState["SearchDataID"] != null)
                        {
                            Item_Master_Entity imes = GetEntity();

                            dtExportdata = iteminfo_bl.SearchbyItem(imes);
                        }
                        ds = dtExportdata.Copy();
                        if (dtExportdata.Columns.Contains("ID"))
                            dtExportdata.Columns.Remove("ID");
                        //DataTable dtgv = iteminfo_bl.AllItemView(); 
                        Item_Master_Entity ime = GetEntity();
                        DataTable dtgv = new DataTable();
                        if (ViewState["ItemAll"] != null)
                            dtgv = ViewState["ItemAll"] as DataTable;
                        //else dtgv = iteminfo_bl.SearchbyItem(ime);
                        else dtgv = Search(1, 1);
                        //DataTable dtgv = iteminfo_bl.SearchbyItem(ime);

                        if (ViewState["ExportField"] != null)
                        {

                            DataTable dt = itfield_bl.ExportCSV(csv,"monotaro");
                            dt.Clear();
                            DataTable dtfield = ViewState["ExportField"] as DataTable;
                            if (dtfield != null && dtfield.Rows.Count > 0)
                            {
                                string field = dtfield.Rows[0]["Export_Fields"].ToString();
                                Exfield = field.Split(',');
                                //Array.Sort(Exfield);
                                DataTable dtItemImage = new DataTable();

                                for (int i = 0; i < Exfield.Count(); i++)
                                {
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        if (Exfield[i] == dc.ColumnName.ToString())
                                        {
                                            dtCSV.Columns.Add(Exfield[i], typeof(string));
                                        }
                                    }
                                }



                                if (Exfield.Count() > 0)
                                {
                                    CopyColumns(ds, dtCSV, Exfield);
                                    string filename = ddlname.SelectedItem.ToString() + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv";
                                    CSV(dtCSV, filename);
                                    lnkdownload.Text = filename;
                                    // ViewState["ExportField"] = null;
                                    //GlobalUI.MessageBox("Successful Export CSV!");
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Successful Export CSV!');", true);
                                }
                                else
                                {
                                    data = null;
                                    //GlobalUI.MessageBox("Invalid Export Field !");
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Invalid Export Field!');", true);
                                }
                            }
                        }

                    }
                }//expfield
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public  DataTable DtTbl(DataTable[] dtToJoin)
        {
            DataTable dtJoined = new DataTable();

            foreach (DataColumn dc in dtToJoin[0].Columns)
                dtJoined.Columns.Add(dc.ColumnName);

            foreach (DataTable dt in dtToJoin)
            {
                foreach (DataRow dr1 in dt.Rows)
                {
                    DataRow dr = dtJoined.NewRow();
                    foreach (DataColumn dc in dtToJoin[0].Columns)
                        dr[dc.ColumnName] = dr1[dc.ColumnName];

                    dtJoined.Rows.Add(dr);
                }
            }

            return dtJoined;
        }

        private void CopyColumns2(DataTable source, DataTable dest, params string[] columns)
        {
            try
            {
                //foreach (DataRow sourcerow in source.Rows)
                for (int k = 0; k < source.Rows.Count; k++)
                {
                    //DataRow destRow = dest.NewRow();
                    for (int y = 0; y < dest.Rows.Count;y++ )
                    {
                   
                        for (int i = 0; i < columns.Length; i++)
                        {
                            string colname = columns[i];
                            if (!String.IsNullOrWhiteSpace(colname))
                            {
                                if (ContainColumn(colname, source) && ContainColumn(colname, dest))
                                {
                                    if (colname.Equals("ライブラリ画像") || colname.Equals("関連商品"))
                                    {
                                        if (dest.Rows[y]["商品番号"].ToString().Equals(source.Rows[k]["商品番号"].ToString()))
                                        dest.Rows[y][colname] = source.Rows[k][colname];
                                    }
                                }
                                else
                                {
                                    //GlobalUI.MessageBox(String.Format("Invalid Export Field ({0}) !", colname));
                                    string msg = String.Format("Invalid Export Field ({0}) !", colname);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('"+ msg +"');", true);
                                }
                            }
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

        private void CopyColumns3(DataTable source, DataTable dest, params string[] columns)
        {
            try
            {
                //foreach (DataRow sourcerow in source.Rows)
                for (int k = 0; k < source.Rows.Count; k++)
                {
                    for (int y = 0; y < dest.Rows.Count; y++)
                    {

                        for (int i = 0; i < columns.Length; i++)
                        {
                            string colname = columns[i];
                            if (!String.IsNullOrWhiteSpace(colname))
                            {
                                if (ContainColumn(colname, source) && ContainColumn(colname, dest))
                                {
                                   
                                    if (dest.Rows[y]["商品番号"].ToString().Equals(source.Rows[k]["商品番号"].ToString()))
                                        dest.Rows[y][colname] = source.Rows[k][colname];
                                   
                                }
                                else
                                {
                                    //GlobalUI.MessageBox(String.Format("Invalid Export Field ({0}) !", colname));
                                    string msg = String.Format("Invalid Export Field ({0}) !", colname);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('" + msg + "');", true);
                                }
                            }
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

        private void CopyColumns(DataTable source, DataTable dest, params string[] columns)
        {
            try
            {
                foreach (DataRow sourcerow in source.Rows)
                {

                    DataRow destRow = dest.NewRow();
                    //  foreach (string colname in columns)
                    for (int i = 0; i < columns.Length; i++)
                    {
                        string colname = columns[i].Trim();
                        if (!String.IsNullOrWhiteSpace(colname))
                        {
                            if (ContainColumn(colname, source) && ContainColumn(colname, dest))
                            {
                                destRow[colname] = sourcerow[colname];
                            }
                            else
                            {
                                //GlobalUI.MessageBox(String.Format("Invalid Export Field ({0}) !", colname));
                                string msg = String.Format("Invalid Export Field ({0}) !", colname);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('" + msg + "');", true);
                            }
                        }
                    }
                    dest.Rows.Add(destRow);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        private bool ContainColumn(string columnName, DataTable table)
        {
            DataColumnCollection columns = table.Columns;

            if (columns.Contains(columnName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void CSV(DataTable dt, string name) 
        {
            try
            {
                using (StreamWriter writer = new StreamWriter((FilePath + name), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dt, writer, true);
                }
                //using (StreamWriter writer = new StreamWriter(Server.MapPath(FilePath) + name, false, Encoding.GetEncoding(932)))
                //{
                //    WriteDataTable(dt, writer, true);
                //}
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        private string QuoteValue(string value)
        {
            return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
        }

        protected void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            try
            {
                if (includeHeaders)
                {
                    List<string> headerValues = new List<string>();

                    foreach (DataColumn column in sourceTable.Columns)
                    {
                        if (column.ColumnName.Equals("path") || column.ColumnName.Equals("name") || column.ColumnName.Equals("code") || column.ColumnName.Equals("original-price") || column.ColumnName.Equals("price") || column.ColumnName.Equals("sale-price"))
                        {
                            headerValues.Add(column.ColumnName.ToLower());
                        }
                        else
                        {
                            headerValues.Add(column.ColumnName.ToUpper());
                        }
                    }
                    StringBuilder builder = new StringBuilder();
                    writer.WriteLine(String.Join(",", headerValues.ToArray()));
                }

                string[] items = null;
                foreach (DataRow row in sourceTable.Rows)
                {
                    //items = row.ItemArray.Select(o => o.ToString()).ToArray();
                    items = row.ItemArray.Select(o => QuoteValue(o.ToString())).ToArray();
                    writer.WriteLine(String.Join(",", items));
                }
                writer.Flush();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void BindDropDownListForShop()
        {
            try
            {
                GlobalBL gb = new GlobalBL();
                Item_BL itbl = new Item_BL();
              
                //ddlshoppage.DataSource = itbl.bindDDforShopstatus();
                //ddlshoppage.DataTextField = "Status";
                //ddlshoppage.DataValueField = "ID";
                //ddlshoppage.DataBind();
                //ddlshoppage.Items.Insert(0, "");
            }

            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnexhibition_Click(object sender, EventArgs e)
        {
            try
            {
                int p = 0;
                int mallID = 0;
                string itemCodeList = null;
                string itemCodeYahooList = null;
                string itemCodeWowmaList = null;
                string itemCodeAmazonList = null;
                string itemCodeJishaList = null;
                string itemCodeTennisList = null;
                string lists = "1";
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList chk = ViewState["checkedValue"] as ArrayList;
                    String cklist = null;
                    for (int i = 0; i < chk.Count; i++)
                    {
                        if (IsSelectedShop(Convert.ToInt32(chk[i].ToString())))   //Check Choice or not Shop
                        {
                            if (IsPost_Available_Date(Convert.ToInt32(chk[i].ToString())))
                            {
                                cklist += chk[i] + ",";
                            }
                            ItemShopBL = new Item_Shop_BL();
                            DataTable dt = ItemShopBL.CheckItemCodeURL(Convert.ToInt32(chk[i].ToString()));
                            if (dt.Rows.Count <1)
                            {
                                p++;
                                //GlobalUI.MessageBox("There is no itemcode which dose not exist itemcode url cannot allow to exhibit!!");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('There is no itemcode which dose not exist itemcode url cannot allow to exhibit!!');", true);
                                cklist = null;
                            }
                        }
                        else
                        {
                            //GlobalUI.MessageBox("出品対象ショップが存在しない商品が存在します");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('出品対象ショップが存在しない商品が存在します');", true);
                            cklist = null;
                            break;
                        }
                        if (p > 0)
                        {
                            //GlobalUI.MessageBox("There is no itemcode which dose not exist itemcode url cannot allow to exhibit!!");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('There is no itemcode which dose not exist itemcode url cannot allow to exhibit!!');", true);
                            break;
                        }
                    }
                    #region ConsoleWriteLineTofile
                    for (int i = 0; i < chk.Count; i++)
                    {
                        DataTable Mall = new DataTable();
                        Mall = ItemShopBL.SelectMallID(Convert.ToInt32(chk[i].ToString()));

                        foreach (DataRow mall in Mall.Rows)
                        {
                            mallID = Convert.ToInt32(mall["Mall_ID"].ToString());


                            if (mallID == 1)
                            {

                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));

                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeList += itemCode["Item_Code"].ToString() + ",";

                                }

                            }
                            else if (mallID == 2)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));

                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeYahooList += itemCode["Item_Code"].ToString() + ",";

                                }

                            }
                            else if (mallID == 4)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));

                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeWowmaList += itemCode["Item_Code"].ToString() + ",";

                                }

                            }
                            else if (mallID == 5)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));

                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeAmazonList += itemCode["Item_Code"].ToString() + ",";

                                }

                            }
                            else if (mallID == 7)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));

                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeTennisList += itemCode["Item_Code"].ToString() + ",";

                                }

                            }
                            else
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));

                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeJishaList += itemCode["Item_Code"].ToString() + ",";

                                }

                            }
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeList))
                    {
                        ConsoleWriteLine_Tofile("Title for Rakuten");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeYahooList))
                    {
                        ConsoleWriteLine_Tofile("Title for Yahoo");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeYahooList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeAmazonList))
                    {
                        ConsoleWriteLine_Tofile("Title for Amazon");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeAmazonList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeWowmaList))
                    {
                        ConsoleWriteLine_Tofile("Title for Wowma");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeWowmaList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeJishaList))
                    {
                        ConsoleWriteLine_Tofile("Title for Jisha");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeJishaList);
                    }


                    if(!string.IsNullOrWhiteSpace(itemCodeTennisList))
                    {
                        ConsoleWriteLine_Tofile("Title for Tennis");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeTennisList);
                    }

                    ConsoleWriteLine_Tofile("Date : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));// Date to  ConsoleWriteLine_Tofile
                    #endregion
                    if (!string.IsNullOrWhiteSpace(cklist))
                    {
                        Session.Remove("Item2IDlist");
                        Session["Item2IDlist"] = cklist;
                        ConsoleWriteLine_Tofile("Process Finish : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); //Process Finish
                        if (User_ID != 0)
                        {
                            DataTable dtShopList = imbl.GetShopList(cklist);
                            if (dtShopList != null && dtShopList.Rows.Count > 0)
                            {
                                string url = "../Item_Exhibition/Exhibition_Confirmation.aspx?Viewlist=" + lists;
                                Response.Redirect(url,false);
                                Bind();
                                ArrayList arrlst = new ArrayList();
                                ViewState["checkedValue"] = arrlst;
                            }
                            else
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('You must select shop name!');", true);
                                //GlobalUI.MessageBox("You must select shop name!");
                        }
                        else
                        {
                            //GlobalUI.MessageBox("Please you should start Login Page!");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Please you should start Login Page!');", true);
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

        protected void btnwarehouse_Click(object sender, EventArgs e)
        {
            try
            {
                int mallID = 0;
                string itemCodeList = null;
                string itemCodeYahooList = null;
                string itemCodeWowmaList = null;
                string itemCodeAmazonList = null;
                string itemCodeJishaList = null;
                string itemCodeTennisList = null;
                string lists = "2";
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList chk = ViewState["checkedValue"] as ArrayList;
                    String cklist = null;
                    for (int i = 0; i < chk.Count; i++)
                    {
                        if (IsSelectedShop(Convert.ToInt32(chk[i].ToString())))   //Check Choice or not Shop
                        {
                            if (IsPost_Available_Date(Convert.ToInt32(chk[i].ToString())))
                            {
                                cklist += chk[i] + ",";
                            }
                        }
                        else
                        {
                            //GlobalUI.MessageBox("出品対象ショップが存在しない商品が存在します");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('出品対象ショップが存在しない商品が存在します');", true);
                            cklist = null;
                            break;
                        }
                    }
                    #region ConsoleWriteLineTofile
                    for (int i = 0; i < chk.Count; i++)
                    {
                        DataTable Mall = new DataTable();
                        Mall = ItemShopBL.SelectMallID(Convert.ToInt32(chk[i].ToString()));
                        foreach (DataRow mall in Mall.Rows)
                        {
                            mallID = Convert.ToInt32(mall["Mall_ID"].ToString());
                            if (mallID == 1)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));
                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeList += itemCode["Item_Code"].ToString() + ",";
                                }
                            }
                            else if (mallID == 2)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));
                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeYahooList += itemCode["Item_Code"].ToString() + ",";
                                }
                            }
                            else if (mallID == 4)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));
                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeWowmaList += itemCode["Item_Code"].ToString() + ",";
                                }
                            }
                            else if (mallID == 5)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));
                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeAmazonList += itemCode["Item_Code"].ToString() + ",";
                                }
                            }
                            else if (mallID == 7)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));
                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeTennisList += itemCode["Item_Code"].ToString() + ",";
                                }
                            }
                            else
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));
                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeJishaList += itemCode["Item_Code"].ToString() + ",";
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeList))
                    {
                        ConsoleWriteLine_Tofile("Title for Rakuten");
                        ConsoleWriteLine_Tofile("Warehouse Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeYahooList))
                    {
                        ConsoleWriteLine_Tofile("Title for Yahoo");
                        ConsoleWriteLine_Tofile("Warehouse Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeYahooList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeAmazonList))
                    {
                        ConsoleWriteLine_Tofile("Title for Amazon");
                        ConsoleWriteLine_Tofile("Warehouse Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeAmazonList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeWowmaList))
                    {
                        ConsoleWriteLine_Tofile("Title for Wowma");
                        ConsoleWriteLine_Tofile("Warehouse Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeWowmaList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeJishaList))
                    {
                        ConsoleWriteLine_Tofile("Title for Jisha");
                        ConsoleWriteLine_Tofile("Warehouse Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeJishaList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeTennisList))
                    {
                        ConsoleWriteLine_Tofile("Title for Tennis");
                        ConsoleWriteLine_Tofile("Warehouse Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeTennisList);
                    }
                    ConsoleWriteLine_Tofile("Date : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));// Date to  ConsoleWriteLine_Tofile
                    #endregion
                    if (!string.IsNullOrWhiteSpace(cklist))
                    {
                        Session.Remove("Item2IDlist");
                        Session["Item2IDlist"] = cklist;
                        ConsoleWriteLine_Tofile("Process Finish : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); //Process Finish
                        if (User_ID != 0)
                        {
                            DataTable dtShopList = imbl.GetShopList(cklist);
                            if (dtShopList != null && dtShopList.Rows.Count > 0)
                            {
                                string url = "../Item_Exhibition/Exhibition_Confirmation.aspx?Viewlist=" + lists;
                                Response.Redirect(url, false);
                                Bind();
                                ArrayList arrlst = new ArrayList();
                                ViewState["checkedValue"] = arrlst;
                            }
                            else
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('You must select shop name!');", true);
                                //GlobalUI.MessageBox("You must select shop name!");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Please you should start Login Page!');", true);
                            //GlobalUI.MessageBox("Please you should start Login Page!");
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

        protected void ddlname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                itfield_bl = new Item_ExportField_BL();
                if (ddlname.SelectedValue == "Item_Option")
                {
                    // checkoption = 1;
                    //hfoption.Value = "1";
                    ViewState["Option"] = "1";

                }
                else if (ddlname.SelectedValue == "Item_Category")
                {
                    //  checkoption = 2;
                    // hfoption.Value = "2";
                    ViewState["Option"] = "2";
                }
                else if (ddlname.SelectedValue == "Item_Image")
                {
                   
                    ViewState["Option"] = "3";
                }
                else if (ddlname.SelectedValue == "29")      // added by ETZ for sks-390 TagID
                {

                    ViewState["Option"] = "4";
                }
                else
                {
                    string ID = ddlname.SelectedItem.Value.ToString();
                    dt = itfield_bl.SelectAllData(ID);
                    ViewState["ExportField"] = dt;


                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Item_Shop_BL itemShopBL = new Item_Shop_BL();
                iteminfo_bl = new Item_Information_BL();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblid = e.Row.FindControl("Label45") as Label;
                    if (!String.IsNullOrWhiteSpace(lblid.Text))
                    {
                        DataTable dt = new DataTable();
                        //if (Cache["Shop"] != null)
                        //{
                        dt = itemShopBL.SelectShopID(Convert.ToInt32(lblid.Text));
                        //Cache["Shop"] as DataTable;

                        // int id = Convert.ToInt32(lblid.Text);
                        //DataRow[] dr = dt.Select("Item_ID=" + id);
                        //if (dr.Count() > 0)
                        //{
                        //    dt = dt.Select("Item_ID=" + id).CopyToDataTable();
                        //}
                        //else dt = new DataTable();
                        BindShop(dt, e);
                        // }
                    }

                    Label lblItem_Name = e.Row.FindControl("lblItemName") as Label;
                    if (!String.IsNullOrWhiteSpace(lblItem_Name.Text))
                    {
                        Button btn = e.Row.FindControl("btnSKU") as Button;
                        btn.Attributes.Add("onclick", "javascript:Show('" + lblItem_Name.Text + "'," + btn.ClientID + ")");
                    }

                    #region Status
                    //if (DataBinder.Eval(e.Row.DataItem, "SKSST").ToString().ToLower() == Convert.ToString(1)) 
                    //{
                    //    e.Row.Cells[1].Text = "ページ制作";
                    //}
                    //else if (DataBinder.Eval(e.Row.DataItem, "SKSST").ToString().ToLower() == Convert.ToString(2))
                    //{
                    //    e.Row.Cells[1].Text = "出品待ち";
                    //}
                    //else if (DataBinder.Eval(e.Row.DataItem, "SKSST").ToString().ToLower() == Convert.ToString(3))
                    //{
                    //    e.Row.Cells[1].Text = "期日出品待ち";
                    //}
                    //else if (DataBinder.Eval(e.Row.DataItem, "SKSST").ToString().ToLower() == Convert.ToString(4))
                    //{
                    //    e.Row.Cells[1].Text = "出品済";
                    //}

                    //if (DataBinder.Eval(e.Row.DataItem, "SHOP").ToString().ToLower() == "n")
                    //{
                    //    e.Row.Cells[2].Text = "未掲載";
                    //}
                    //else if (DataBinder.Eval(e.Row.DataItem, "SHOP").ToString().ToLower() =="u")
                    //{
                    //    e.Row.Cells[2].Text = "掲載中";
                    //}
                    //else if (DataBinder.Eval(e.Row.DataItem, "SHOP").ToString().ToLower() == "d")
                    //{
                    //    e.Row.Cells[2].Text = "削除 ";
                    //}
                    #endregion

                    Label lblsks = e.Row.FindControl("Label4") as Label;
                    CheckBox chksks = e.Row.FindControl("ckItem") as CheckBox;
                    HtmlGenericControl Ppage = e.Row.FindControl("Ppage") as HtmlGenericControl;
                    HtmlGenericControl PWaitSt = e.Row.FindControl("PWaitSt") as HtmlGenericControl;
                    HtmlGenericControl PWaitL = e.Row.FindControl("PWaitL") as HtmlGenericControl;
                    HtmlGenericControl PExhibit = e.Row.FindControl("PExhibit") as HtmlGenericControl;
                    HtmlGenericControl POkSt = e.Row.FindControl("POkSt") as HtmlGenericControl;
                    HtmlGenericControl PNOk = e.Row.FindControl("PNOK") as HtmlGenericControl;
                    switch (lblsks.Text)
                    {
                        case "1":
                            {
                                Ppage.Visible = true;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = false;
                                POkSt.Visible = false;
                                PExhibit.Visible = false;
                                PNOk.Visible = false;
                                break;
                            }
                        case "3":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = true;
                                PWaitL.Visible = false;
                                POkSt.Visible = false;
                                PExhibit.Visible = false;
                                PNOk.Visible = false;
                                break;
                            }
                        case "2":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = true;
                                POkSt.Visible = false;
                                PExhibit.Visible = false;
                                PNOk.Visible = false;
                                break;
                            }
                        case "4":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = false;
                                POkSt.Visible = true;
                                PExhibit.Visible = false;
                                PNOk.Visible = false;
                                break;

                            }
                        case "5":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = false;
                                POkSt.Visible = false;
                                PExhibit.Visible = true;
                                PNOk.Visible = false;
                                break;
                            }
                        case "6":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = false;
                                POkSt.Visible = false;
                                PExhibit.Visible = false;
                                PNOk.Visible = true;
                                break;
                            }
                    }

                    Label lblshop = (Label)e.Row.FindControl("Label6");
                    HtmlGenericControl pWait = e.Row.FindControl("PWait") as HtmlGenericControl;
                    HtmlGenericControl pOk = e.Row.FindControl("POk") as HtmlGenericControl;
                    HtmlGenericControl pDel = e.Row.FindControl("PDel") as HtmlGenericControl;
                    HtmlGenericControl pInactive = e.Row.FindControl("PInactive") as HtmlGenericControl;
                    HtmlGenericControl PWarehouse = e.Row.FindControl("PWarehouse") as HtmlGenericControl;
                    HtmlGenericControl Warehouseerror = e.Row.FindControl("Warehouseerror") as HtmlGenericControl;
                    switch (lblshop.Text)
                    {
                        case "n":
                            {
                                pWait.Visible = true;
                                pOk.Visible = false;
                                pDel.Visible = false;
                                pInactive.Visible = false;
                                PWarehouse.Visible = false;
                                Warehouseerror.Visible = false;
                                break;
                            }
                        case "u":
                            {
                                pWait.Visible = false;
                                pOk.Visible = true;
                                pDel.Visible = false;
                                pInactive.Visible = false;
                                PWarehouse.Visible = false;
                                Warehouseerror.Visible = false;
                                break;
                            }
                        case "d":
                            {
                                pWait.Visible = false;
                                pOk.Visible = false;
                                pDel.Visible = true;
                                pInactive.Visible = false;
                                PWarehouse.Visible = false;
                                Warehouseerror.Visible = false;
                                break;
                            }
                        case "g":
                            {
                                pWait.Visible = false;
                                pOk.Visible = false;
                                pDel.Visible = false;
                                pInactive.Visible = true;
                                PWarehouse.Visible = false;
                                Warehouseerror.Visible = false;
                                break;
                            }
                        case "w":
                            {
                                pWait.Visible = false;
                                pOk.Visible = false;
                                pDel.Visible = false;
                                pInactive.Visible = false;
                                PWarehouse.Visible = true;
                                Warehouseerror.Visible = false;
                                break;
                            }
                        case "e":
                            {
                                pWait.Visible = false;
                                pOk.Visible = false;
                                pDel.Visible = false;
                                pInactive.Visible = false;
                                PWarehouse.Visible = false;
                                Warehouseerror.Visible = true;
                                break;
                            }
                    }
                    Label lbl = e.Row.FindControl("lblMStatus") as Label;
                    HtmlGenericControl Mpage = e.Row.FindControl("Mpage") as HtmlGenericControl;
                    HtmlGenericControl MWaitSt = e.Row.FindControl("MWaitSt") as HtmlGenericControl;
                    switch (lbl.Text)
                    {
                        case "m":
                            {
                                Mpage.Visible = true;
                                MWaitSt.Visible = false;
                                break;
                            }
                        case "nm":
                            {
                                Mpage.Visible = false;
                                MWaitSt.Visible = true;
                                break;
                            }
                        case "":
                            {
                                Mpage.Visible = false;
                                MWaitSt.Visible = true;
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

        public void BindShop(DataTable dt, GridViewRowEventArgs e)
        {
            try
            {
                DropDownList ddlshop = (DropDownList)e.Row.FindControl("ddlshoppage");
                ddlshop.DataSource = dt;
                ddlshop.DataValueField = "Shop_Name";
                ddlshop.DataBind();
                ddlshop.Items.Insert(0, "ショップ選択");
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnsearch_Click1(object sender, EventArgs e)
        {
            //Cache.Remove("SearchItem");
            //Item_Master_Entity ime = GetEntity();
            //DataTable dt = iteminfo_bl.SearchbyItem(ime);

            //gp.TotalRecord = dt.Rows.Count;
            //gp.OnePageRecord = gvItem.PageSize;

            //int index1 = 0;
            //gp.sendIndexToThePage += delegate(int index)
            //{
            //    index1 = index;
            //    gvItem.PageIndex = index1;
            //    gvItem.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
            //};
            //gvItem.DataSource = dt;
            //gvItem.DataBind();
            //ViewState["ItemAll"] = dt;
            //Cache.Insert("SearchItem", dt, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero);
            //txtdate.Text = hdfFromDate.Value;
            //txtdateapproval.Text = hdfToDate.Value;
        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //gvItem.PageIndex = 0;
                //gvItem.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
                //if (ViewState["search"] != null)
                //{
                //    search = ViewState["search"].ToString();
                //    ViewState["index"] = "index";
                //}
                ////gvItem.DataBind();
                //ViewState["pagesize"] = Convert.ToInt32(ddlpage.SelectedValue);
                //int count = 0;
                //if (gvItem.Rows.Count > 0)
                //{
                //    Label tc = (Label)gvItem.Rows[0].FindControl("lblTotalCount");
                //    count = Convert.ToInt32(tc.Text);
                //}
                //gp.CalculatePaging(count, gvItem.PageSize, 1);
                ////Bind();
                gvItem.PageSize = Convert.ToInt32(ddlpage.SelectedValue); //ami
                Bind();//ami
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected string FormatstringUrl(string url)
        {

            url = Server.MapPath("~/Item_Image/" + url);
            if (url != null & url.Length > 0)
            {
                return url;

            }
            else
                return null;

        }

        protected void btndivshowhide_Click(object sender, EventArgs e)
        {
            //if (btndivshowhide.Text.Equals("+")) 
            //{
            //    hidBox.Visible = true;
            //    btndivshowhide.Text = "-";
            //}
            //else if(btndivshowhide.Text.Equals("-"))
            //{
            //    hidBox.Visible = false;
            //    btndivshowhide.Text = "+";
            //}
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtdate.Text = String.Empty;
                hdfFromDate.Value = Request.Form[txtdate.UniqueID];
                txtdateapproval.Text = Request.Form[txtdateapproval.UniqueID];
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
                txtdateapproval.Text = String.Empty;
                hdfToDate.Value = Request.Form[txtdateapproval.UniqueID];
                txtdate.Text = Request.Form[txtdate.UniqueID];
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

        protected void lnkdownload_Click(object sender, EventArgs e)
        {
            //try
            //{
                Download(FilePath + lnkdownload.Text);

            //}
            //catch (Exception ex)
            //{
            //    Session["Exception"] = ex.ToString();
            //    Response.Redirect("~/CustomErrorPage.aspx?");
            //}
        }

        protected void Download(string filecheck) 
        {
            //try
            //{
            if (File.Exists(filecheck))
            {
                string filename = lnkdownload.Text;
              WebClient req = new WebClient();
              HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                //response.AddHeader("Content-Disposition","attachment;filename=\""+filecheck+"\"");
                response.ContentType = "application/octet-stream";
                byte[] data = req.DownloadData(filecheck);
                response.BinaryWrite(data);
                response.End();
               
            }
            else
            {
                //GlobalUI.MessageBox("File doesn't exist!");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('File doesn't exist!');", true);
            }
            
           // }
            //catch (Exception ex)
            //{
            //    Session["Exception"] = ex.ToString();
            //    Response.Redirect("~/CustomErrorPage.aspx?");
            //}
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            /*
            string itemIDList = null;
            string lists = "1";
            if (gvItem.HeaderRow != null)
            {
                CheckBox chkAll = (CheckBox)gvItem.HeaderRow.FindControl("ckall");
                //string checkAllIndex = "chkAll";
                if (chkAll.Checked)
                {
                    DataTable dt = (DataTable)ViewState["ItemAll"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Columns.Contains("ID"))
                        {
                            if (IsSelectedShop(Convert.ToInt32(dt.Rows[i]["ID"].ToString())))     //Check Choice or not Shop
                            {
                                itemIDList += dt.Rows[i]["ID"].ToString() + ",";
                            }
                            else
                            {
                                itemMasterBL = new Item_Master_BL();
                                string Item_Code = itemMasterBL.SelectByItemCode(Convert.ToInt32(dt.Rows[i]["ID"].ToString()));
                                GlobalUI.MessageBox(Item_Code + " does not choice Shop.");
                                itemIDList = null;
                                break;
                            }
                        }
                    }
                   
                }
                else
                {
                    ArrayList CheckBoxArray;
                    if (ViewState["checkedValue"] != null)
                    {
                        CheckBoxArray = (ArrayList)ViewState["checkedValue"];
                        for (int i = 0; i < CheckBoxArray.Count; i++)
                        {
                            if (IsSelectedShop(Convert.ToInt32(CheckBoxArray[i].ToString())))   //Check Choice or not Shop
                            {
                                itemIDList += CheckBoxArray[i] + ",";
                            }
                            else
                            {
                                itemMasterBL = new Item_Master_BL();
                                string Item_Code = itemMasterBL.SelectByItemCode(Convert.ToInt32(CheckBoxArray[i].ToString()));
                                GlobalUI.MessageBox(Item_Code + " does not choice Shop.");
                                itemIDList = null;
                                break;
                            }
                        }
                    }
                   
                }

                if (!string.IsNullOrWhiteSpace(itemIDList))
                {
                    DataTable dtShopList = imbl.GetShopList(itemIDList);
                    if (dtShopList != null && dtShopList.Rows.Count > 0)
                    {
                        //Remove last comma from string
                        itemIDList = itemIDList.TrimEnd(','); // (OR) itemIDList.Remove(str.Length-1); 
                        ////Export_CSV_Delete CSVDelete = new Export_CSV_Delete();
                        ////string filename = CSVDelete.CSV_Delete(itemIDList);
                        Session["list"] = itemIDList;
                      //  string url = "../Item_Exhibition/Exhibition_List.aspx?list=" + lists;
                        string url = "../Item_Exhibition/Exhibition_Confirmation.aspx?list=" + lists+"&delete="+"d";
                        Response.Redirect(url,false);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var win = window.open('" + url + "','_newtab'); self.focus();", true);
                        Bind();
                        //Response.Redirect(Request.RawUrl);
                       

                    }
                    else
                        GlobalUI.MessageBox("You must select shop name!");
                }
               
            }*/
         
            string confirmValue = Request.Form["confirm_value"];
            int mallID = 0;
            string itemCodeList = null;
            string itemCodeYahooList = null;
            string itemCodeWowmaList = null;
            string itemCodeAmazonList = null;
            string itemCodeJishaList = null;
            string itemCodeTennisList = null;

            if (confirmValue == "はい")
            {

               
                string lists = "1";
                //if (ViewState["list"] != null)
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList chk = ViewState["checkedValue"] as ArrayList;
                    String cklist = null;
                    for (int i = 0; i < chk.Count; i++)
                    {
                        if (IsSelectedShop(Convert.ToInt32(chk[i].ToString())))   //Check Choice or not Shop
                        {
                            cklist += chk[i] + ",";
                        }
                        else
                        {
                            //itemMasterBL = new Item_Master_BL();
                            //string Item_Code = itemMasterBL.SelectByItemCode(Convert.ToInt32(chk[i].ToString()));
                            //GlobalUI.MessageBox(Item_Code + " does not choice Shop.");
                            //GlobalUI.MessageBox("出品対象ショップが存在しない商品が存在します");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('出品対象ショップが存在しない商品が存在します');", true);
                            cklist = null;
                            break;
                        }
                    }

                    #region ConsoleWriteLineTofile
                    for (int i = 0; i < chk.Count; i++)
                    {
                        
                        DataTable Mall = new DataTable();
                        Mall = ItemShopBL.SelectMallID(Convert.ToInt32(chk[i].ToString()));

                        foreach (DataRow mall in Mall.Rows)
                        {
                            mallID = Convert.ToInt32(mall["Mall_ID"].ToString());


                            if (mallID == 1)
                            {

                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));

                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeList += itemCode["Item_Code"].ToString() + ",";

                                }

                            }
                            else if (mallID == 2)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));

                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeYahooList += itemCode["Item_Code"].ToString() + ",";

                                }

                            }
                            else if (mallID == 4)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));

                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeWowmaList += itemCode["Item_Code"].ToString() + ",";

                                }

                            }
                            else if (mallID == 5)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));

                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeAmazonList += itemCode["Item_Code"].ToString() + ",";

                                }

                            }
                            else if (mallID == 7)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));

                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeTennisList += itemCode["Item_Code"].ToString() + ",";

                                }

                            }
                            else
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(chk[i].ToString()));

                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeJishaList += itemCode["Item_Code"].ToString() + ",";

                                }

                            }
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeList))
                    {
                        ConsoleWriteLine_Tofile("Title for Rakuten");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeYahooList))
                    {
                        ConsoleWriteLine_Tofile("Title for Yahoo");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeYahooList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeAmazonList))
                    {
                        ConsoleWriteLine_Tofile("Title for Amazon");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeAmazonList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeWowmaList))
                    {
                        ConsoleWriteLine_Tofile("Title for Wowma");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeWowmaList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeJishaList))
                    {
                        ConsoleWriteLine_Tofile("Title for Jisha");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeJishaList);
                    }


                    if (!string.IsNullOrWhiteSpace(itemCodeTennisList))
                    {
                        ConsoleWriteLine_Tofile("Title for Tennis");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeTennisList);
                    }

                    ConsoleWriteLine_Tofile("Date : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));// Date to  ConsoleWriteLine_Tofile
                    #endregion

                    //String cklist = String.Join(",", (string[])chk.ToArray(Type.GetType("System.String")));
                    if (!string.IsNullOrWhiteSpace(cklist))
                    {
                        Session.Remove("ItemIDDeleteList");
                        Session["ItemIDDeleteList"] = cklist;
                        if (User_ID != 0)
                        {
                            DataTable dtShopList = imbl.GetShopList(cklist);
                            if (dtShopList != null && dtShopList.Rows.Count > 0)
                            {
                                //Export_CSV eCSV = new Export_CSV(cklist, User_ID);
                                string url = "../Item_Exhibition/Exhibition_Confirmation.aspx?list=" + lists + "&delete=" + "d";
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var win = window.open('" + url + "','_blank'); self.focus();", true);
                          
                                Response.Redirect(url, false);
                             
                                Bind();
                                ArrayList arrlst = new ArrayList();

                                ViewState["checkedValue"] = arrlst;
                            }
                            else
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('You must select shop name!');", true);
                                //GlobalUI.MessageBox("You must select shop name!");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Please you should start Login Page!');", true);
                            //GlobalUI.MessageBox("Please you should start Login Page!");
                        }
                    }
                }
            }//msg
           
        }

        protected void CheckChangeGridpage(Boolean check)
        {
            try
            {
                ArrayList arrCheckValue = new ArrayList(); ArrayList chk = new ArrayList();
                if (check == true)
                {
                    //if (ViewState["ItemAll"] != null)
                    //{
                    //    DataTable dtItemAll = ViewState["ItemAll"] as DataTable;
                    //for (int i = 0; i < dtItemAll.Rows.Count; i++)
                    if (ViewState["checkedValue"] != null)
                    {
                         chk = ViewState["checkedValue"] as ArrayList;

                         for (int j = 0; j < chk.Count; j++) 
                         {
                             arrCheckValue.Add(chk[j].ToString());
                         
                         }
                    
                    }
                    for (int i = 0; i < gvItem.Rows.Count; i++)
                    {
                        //arrCheckValue.Add(dtItemAll.Rows[i]["ID"].ToString());
                        Label lbl = gvItem.Rows[i].FindControl("Label45") as Label;
                        CheckBox ckb = gvItem.Rows[i].FindControl("ckItem") as CheckBox;
                        if (ckb.Enabled)
                        {
                            
                            arrCheckValue.Add(lbl.Text);
                        }
                    }
                    // }
                }

                ViewState["checkedValue"] = arrCheckValue;
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
                CheckChangeGrid(true);
                ItemCheck_Change();

                txtdate.Text = hdfFromDate.Value;
                txtdateapproval.Text = hdfToDate.Value;
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
                CheckChangeGrid(false);
                ItemCheck_Change();
                hfNewTab.Text = "1";

                txtdate.Text = hdfFromDate.Value;
                txtdateapproval.Text = hdfToDate.Value;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected Boolean IsSelectedShop(int Item_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                Item_Shop_BL ItemShopBL = new Item_Shop_BL();
                dt = ItemShopBL.SelectShopID(Item_ID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

        public int SelectByRowCount()
        {
            int count = 0;
           
            count = Convert.ToInt32(Search(1, 2).Rows[0]["Count"]);
            
            return count;
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

        public DataTable Search(int pageIndex, int option)
        {
            try
            {
                DataTable dt = new DataTable();
                Item_Master_Entity ime = GetEntity();
                pageIndex = pageIndex + 1;
                iteminfo_bl = new Item_Information_BL();


                if (option == 3 || option == 4 || option == 5)//like search
                {
                    dt = iteminfo_bl.SearchbyItemlike(ime, pageIndex, int.Parse(ddlpage.SelectedValue.ToString()), option);
                }
                else
                {
                    dt = iteminfo_bl.SearchbyItem2(ime, pageIndex, int.Parse(ddlpage.SelectedValue.ToString()), option, 1);
                }
                if (option != 2)
                    ViewState["ItemAll"] = dt;
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                String[] data = new String[100];
                iteminfo_bl = new Item_Information_BL();
                itfield_bl = new Item_ExportField_BL();
                dtExport = new DataTable();
                itemMasterBL = new Item_Master_BL();
                DataTable dtCSV = new DataTable();
                DataTable dtmanual = new DataTable();
                DataTable dtLibrary = new DataTable();
                string exportfiletype=".csv";
                string stroption = ViewState["Option"] as string;
                if (ViewState["search"] != null)
                {
                    Item_Master_Entity imes = GetEntity();
                    if ((chkno.Checked == true && !String.IsNullOrWhiteSpace(txtitemno.Text.Trim())))//equal search
                    {
                        dtExportdata = iteminfo_bl.SearchbyItem2(imes, 0, 30, 9, 1);
                    }//equal search
                    else
                    {
                        //dtExportdata = iteminfo_bl.ItemView2_Search(imes, 1, gvItem.PageSize, 9);
                        dtExportdata = Search(0, 5); //export for like search
                    }
                    String csv = String.Empty;
                    if (dtExportdata != null && dtExportdata.Rows.Count > 0)
                        for (int u = 0; u < dtExportdata.Rows.Count; u++)
                        {
                            if (dtExportdata.Columns.Contains("ID"))
                                csv += dtExportdata.Rows[u]["ID"].ToString() + ",";
                        }
                    if (stroption == "1")
                    {
                        DataTable dtoption = itfield_bl.Selectoption_Cat(csv, 1);
                        string filename = ddlname.SelectedItem.ToString() + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv";
                        CSV(dtoption, filename);
                        lnkdownload.Text = filename;
                        ViewState["Option"] = String.Empty;
                        //GlobalUI.MessageBox("Successful Export CSV!");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Successful Export CSV!');", true);
                    }
                    else if (stroption == "2")
                    {
                        DataTable dtoption = itfield_bl.Selectoption_Cat(csv, 0);
                        string filename = ddlname.SelectedItem.ToString() + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv";
                        CSV(dtoption, filename);
                        lnkdownload.Text = filename;
                        ViewState["Option"] = String.Empty;
                        //GlobalUI.MessageBox("Successful Export CSV!");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Successful Export CSV!');", true);
                    }
                    else if (stroption == "3")
                    {
                        DataTable dtoption = itfield_bl.SelectRakutenImagesetting(csv);
                        string filename = ddlname.SelectedItem.ToString() + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv";
                        CSV(dtoption, filename);
                        lnkdownload.Text = filename;
                        ViewState["Option"] = String.Empty;
                        //GlobalUI.MessageBox("Successful Export CSV!");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Successful Export CSV!');", true);
                    }
                    else if (stroption == "4")                                 // added by ETZ for sks-390 TagID
                    {
                        DataTable dtoption = itfield_bl.SelectRakutenTagIDInfo(csv);
                        string filename = ddlname.SelectedItem.ToString() + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv";
                        CSV(dtoption, filename);
                        lnkdownload.Text = filename;
                        ViewState["Option"] = String.Empty;
                        //GlobalUI.MessageBox("Successful Export CSV!");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Successful Export CSV!');", true);
                    }
                    else
                    {
                        if (ViewState["ExportField"] != null)
                        {
                            ds = dtExportdata.Copy();
                            if (dtExportdata.Columns.Contains("ID"))
                                dtExportdata.Columns.Remove("ID");
                            Item_Master_Entity ime = GetEntity();
                            DataTable dtgv = new DataTable();
                            if (ViewState["ItemAll"] != null)
                                dtgv = ViewState["ItemAll"] as DataTable;
                            else dtgv = Search(1, 1);
                            if (ViewState["ExportField"] != null)
                            {
                                DataTable dt = itfield_bl.ExportCSV(csv,"monotaro");
                                dtLibrary = dt.Copy();
                                dt.Clear();
                                DataTable dtfield = ViewState["ExportField"] as DataTable;
                                if (dtfield != null && dtfield.Rows.Count > 0)
                                {
                                    string field = dtfield.Rows[0]["Export_Fields"].ToString();
                                    if (field.Contains("商品番号")) { }
                                    else field += "," + "商品番号";
                                    Exfield = field.Split(',');
                                    DataTable dtItemImage = new DataTable();
                                    for (int i = 0; i < Exfield.Count(); i++)
                                    {
                                        foreach (DataColumn dc in dt.Columns)
                                        {
                                            string line = Exfield[i];
                                            string replaceWith = "";
                                            line.Replace(System.Environment.NewLine, "replacement text");
                                            string line2 = Regex.Replace(line, @"\r\n?|\n", replaceWith);
                                            if (line2 == dc.ColumnName.ToString())
                                            {
                                                if (dtCSV.Columns.Contains(line2)) { }
                                                else
                                                {
                                                    dtCSV.Columns.Add(line2, typeof(string));
                                                }
                                            }
                                        }
                                    }
                                    if (Exfield.Count() > 0 && !String.IsNullOrWhiteSpace(ddlname.SelectedItem.ToString()))
                                    {
                                        if (checkexfield(Exfield))
                                        {
                                            #region Monotaro
                                            if (Exfield.Contains("(サプライヤ名)"))
                                            {
                                                dtCSV.Clear();
                                                dtCSV.Merge(dtLibrary, true, MissingSchemaAction.Ignore);
                                                if (dtCSV.Columns.Contains("商品番号"))
                                                {
                                                    dtCSV.Columns.Remove("商品番号");
                                                    dtmanual = itfield_bl.GetManualData();
                                                    dtmanual.Merge(dtCSV, true, MissingSchemaAction.Ignore);
                                                }
                                                exportfiletype = ".xlsx";
                                            }
                                            #endregion
                                            if (!Exfield.Contains("path"))
                                            { CopyColumns(ds, dtCSV, Exfield); }
                                            DataTable dtemptydata = dtCSV.Copy();
                                            if (Exfield.Contains("出品対象ショップ"))
                                            {
                                                DataTable dtshop = itfield_bl.ExportCSVShop(csv);//
                                                if (dtshop != null && dtshop.Rows.Count > 0)
                                                {
                                                    dtCSV.Clear();
                                                    dtCSV = dtshop.Clone();
                                                    dtCSV.Merge(dtshop);
                                                }
                                            }
                                            #region For data_spy
                                            if (Exfield.Contains("path") || Exfield.Contains("code"))
                                            {
                                                DataTable dtspy = itfield_bl.ExportCSV(csv, "data_spy");
                                                dtCSV.Clear();
                                                dtCSV = dtspy.Clone();
                                                dtCSV.Merge(dtspy);
                                            }
                                            #endregion
                                            #region for Image&Relate Item
                                            else if (Exfield.Contains("ライブラリ画像") || Exfield.Contains("関連商品"))//for library  image
                                            {
                                                dtLibrary = RemoveDuplicateRows(dtLibrary, "商品番号");
                                                DataTable dtexp = dtCSV.Copy();
                                                DataTable dtt = dtCSV.Clone();
                                                dtCSV.Clear();
                                                for (int i = 0; i < dtLibrary.Rows.Count; i++)
                                                {
                                                    for (int k = 0; k < 5; k++)
                                                    {
                                                        if (Exfield.Contains("関連商品") && Exfield.Contains("ライブラリ画像"))
                                                        {
                                                            if (!String.IsNullOrWhiteSpace(dtLibrary.Rows[i]["ライブラリ画像" + (k + 1)].ToString()) || !String.IsNullOrWhiteSpace(dtLibrary.Rows[i]["関連商品" + (k + 1)].ToString()))
                                                            {
                                                                DataRow destRow = dtt.NewRow();
                                                                dtt.Rows.Add(destRow);
                                                                dtt.Rows[k]["商品番号"] = dtLibrary.Rows[i]["商品番号"].ToString();
                                                                if (Exfield.Contains("関連商品"))
                                                                {
                                                                    dtt.Rows[k]["関連商品"] = dtLibrary.Rows[i]["関連商品" + (k + 1)].ToString();
                                                                    if (Exfield.Contains("関連商品番号"))
                                                                        dtt.Rows[k]["関連商品番号"] = k + 1;
                                                                }
                                                                if (Exfield.Contains("ライブラリ画像"))
                                                                {
                                                                    dtt.Rows[k]["ライブラリ画像"] = dtLibrary.Rows[i]["ライブラリ画像" + (k + 1)].ToString();
                                                                }
                                                            }//if
                                                            else
                                                            {
                                                                DataRow destRow = dtt.NewRow();
                                                                dtt.Rows.Add(destRow);
                                                            }//else
                                                        }//check contain 2 fields
                                                        else if (Exfield.Contains("関連商品"))
                                                        {
                                                            if (!String.IsNullOrWhiteSpace(dtLibrary.Rows[i]["関連商品" + (k + 1)].ToString()))
                                                            {
                                                                DataRow destRow = dtt.NewRow();
                                                                dtt.Rows.Add(destRow);
                                                                dtt.Rows[k]["商品番号"] = dtLibrary.Rows[i]["商品番号"].ToString();
                                                                if (Exfield.Contains("関連商品"))
                                                                {
                                                                    dtt.Rows[k]["関連商品"] = dtLibrary.Rows[i]["関連商品" + (k + 1)].ToString();
                                                                    if (Exfield.Contains("関連商品番号"))
                                                                        dtt.Rows[k]["関連商品番号"] = k + 1;
                                                                }
                                                            }//if
                                                            else
                                                            {
                                                                DataRow destRow = dtt.NewRow();
                                                                dtt.Rows.Add(destRow);
                                                            }//else
                                                        }//check relateitem
                                                        else if (Exfield.Contains("ライブラリ画像"))
                                                        {
                                                            if (!String.IsNullOrWhiteSpace(dtLibrary.Rows[i]["ライブラリ画像" + (k + 1)].ToString()))
                                                            {
                                                                DataRow destRow = dtt.NewRow();
                                                                dtt.Rows.Add(destRow);
                                                                dtt.Rows[k]["商品番号"] = dtLibrary.Rows[i]["商品番号"].ToString();
                                                                if (Exfield.Contains("ライブラリ画像"))
                                                                {
                                                                    dtt.Rows[k]["ライブラリ画像"] = dtLibrary.Rows[i]["ライブラリ画像" + (k + 1)].ToString();
                                                                }

                                                            }//if
                                                        }//check  library image 
                                                    }
                                                    RemoveNullColumnFromDataTable(dtt);
                                                    dtCSV.Merge(dtt);
                                                    dtt.Rows.Clear();
                                                }
                                                if (dtCSV != null && dtCSV.Rows.Count > 0) { }
                                                else
                                                { dtCSV = dtemptydata; }
                                            }
                                            #endregion
                                            string filename = ddlname.SelectedItem.ToString() + DateTime.Now.ToString("ddMMyyyy_HHmmss") + exportfiletype;
                                            if (exportfiletype == ".xlsx")
                                            {
                                                Excel(dtmanual, filename);
                                                //GlobalUI.MessageBox("Successful Export Excel!");
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Successful Export CSV!');", true);
                                            }
                                            else
                                            {
                                                CSV(dtCSV, filename);
                                                //GlobalUI.MessageBox("Successful Export CSV!");
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Successful Export CSV!');", true);
                                            }
                                            lnkdownload.Text = filename;
                                            ViewState["ExportField"] = null;
                                            
                                        }//chkexfield
                                        else
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Please select correct export field!');", true);
                                            //GlobalUI.MessageBox("Please select correct export field!");
                                    }//if
                                    else
                                    {
                                        data = null;
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Please Select Export Field!');", true);
                                        //GlobalUI.MessageBox("Please Select Export Field!");
                                    }
                                }
                            }
                        }//expfield
                        else
                        {
                            //GlobalUI.MessageBox("Please Select Export Field!");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('Please Select Export Field!');", true);
                        }
                    }
                    ddlname.DataSource = itfield_bl.SelectAll();
                    ddlname.DataTextField = "Export_Name";
                    ddlname.DataValueField = "ID";
                    ddlname.DataBind();
                    ddlname.Items.Insert(0, "");
                    ddlname.Items.Insert(1, "Item_Option");
                    ddlname.Items.Insert(2, "Item_Category");
                    ddlname.Items.Insert(3, "Item_Image");
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void Excel(DataTable dt, string filename)
        {
            try
            {
                string physicalPath = null;
                string path = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (File.Exists(Server.MapPath("~/Excel_Export/Monotaro.xlsx")))
                    {
                        File.Delete(Server.MapPath("~/Excel_Export/Monotaro.xlsx"));
                    }
                    physicalPath = ExcelExport + "Monotaro.xlsx";
                    path = MapPath("~/Excel_Export/Monotaro.xlsx");
                    File.Copy(physicalPath, path);
                    using (XLWorkbook wb = new XLWorkbook(path))
                    {
                        IXLWorksheet ws = wb.Worksheet("Monotaro");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 1; j <= dt.Columns.Count; j++)
                            {
                                string value = ws.Cell(1, j).Value.ToString();
                                if (!String.IsNullOrWhiteSpace(dt.Rows[i][value].ToString()) && !dt.Rows[i][value].ToString().Equals("Null"))
                                    ws.Cell(i + 2, j).Value = "'" + ReplaceBackspace(dt.Rows[i][value].ToString());
                            }
                        }
                        for (int i = 2; i <= 15; i++)
                        {
                            for (int j = 1; j <= dt.Columns.Count; j++)
                            {
                                IXLCell cell = ws.Cell(i, j);
                                if (i > 2)
                                {
                                    ws.Rows(i, j).Height = 40;
                                }
                                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }
                        }
                        wb.SaveAs(path);
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/msexcel";
                    Response.AddHeader("content-disposition", "attachment;filename=Monotaro" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");
                    Response.HeaderEncoding = System.Text.Encoding.GetEncoding("shift_jis");
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift_jis");
                    Response.Flush();
                    Response.TransmitFile(path);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ConsoleWriteLine_Tofile(ex.ToString());
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public string ReplaceBackspace(string hasBackspace)
        {
            if (string.IsNullOrEmpty(hasBackspace))
                return hasBackspace;
            StringBuilder result = new StringBuilder(hasBackspace.Length);
            foreach (char c in hasBackspace)
            {
                if (c == '\b')
                {
                    if (result.Length > 0)
                        result.Length--;
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public DataTable RemoveColumn(DataTable dt) 
        {
            try
            {
                dt.Columns.Remove("SHOP");
                dt.Columns.Remove("Sale_Code");
                dt.Columns.Remove("製品コード");
                dt.Columns.Remove("商品名");
                dt.Columns.Remove("Item_AdminCode");

                dt.Columns.Remove("定価");
                dt.Columns.Remove("販売価格");
                dt.Columns.Remove("原価");
                dt.Columns.Remove("発売日");
                dt.Columns.Remove("掲載可能日");
                dt.Columns.Remove("年度");
                dt.Columns.Remove("シーズン");
                dt.Columns.Remove("ブランド名");
                dt.Columns.Remove("ブランドコード");
                dt.Columns.Remove("競技名");
                dt.Columns.Remove("分類名");
                dt.Columns.Remove("仕入先名");
                dt.Columns.Remove("カタログ情報");
                dt.Columns.Remove("商品情報");
                dt.Columns.Remove("PC用商品説明文");
                dt.Columns.Remove("スマートフォン用商品説明文");
                dt.Columns.Remove("PC用販売説明文");
                dt.Columns.Remove("楽天カテゴリID");
                dt.Columns.Remove("ヤフーカテゴリID");
                dt.Columns.Remove("ポンパレカテゴリID");
                dt.Columns.Remove("フリースペース1");
                dt.Columns.Remove("フリースペース2");
                dt.Columns.Remove("フリースペース3");
                dt.Columns.Remove("送料");
                dt.Columns.Remove("個別送料");
                dt.Columns.Remove("倉庫指定");
                dt.Columns.Remove("闇市パスワード");
                dt.Columns.Remove("SKU_Status");
                dt.Columns.Remove("SKSST");
                dt.Columns.Remove("クラウドショップモード");
                dt.Columns.Remove("エビデンス");

                dt.Columns.Remove("PC用キャッチコピー");
                dt.Columns.Remove("モバイル用キャッチコピー");
                dt.AcceptChanges();
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }

        public bool checkexfield(String[] field) 
        {
            if ((field.Contains("出品対象ショップ") && field.Contains("ライブラリ画像")) || (field.Contains("出品対象ショップ") && field.Contains("関連商品")))
                return false;
            else
                return true;
        }

       public void RemoveNullColumnFromDataTable(DataTable dt)
        {
             for (int i = dt.Rows.Count - 1; i >= 0; i--)
                 {
                    if (dt.Rows[i][1] == DBNull.Value)
                        dt.Rows[i].Delete();
                  }
                 dt.AcceptChanges();
        }
       /*
* Created By Inaoka
* Created Date 2015/04/18
* Updated By
* Updated Date
*
* Description:
* trace by using the StreamWriter
* with ConsoleWriteLine_Tofile
* output ConsoleWriteLIne.txt in currenct directory
* 
*/
       protected Boolean IsPost_Available_Date(int Item_ID)
       {
           try
           {
               Item_Master_BL ItemMasterBL = new Item_Master_BL();
               return ItemMasterBL.IsPost_Available_Date(Item_ID);
           }
           catch (Exception ex)
           {
               Session["Exception"] = ex.ToString();
               Response.Redirect("~/CustomErrorPage.aspx?");
               return false;
           }
       }

       public void ConsoleWriteLine_Tofile(string traceText)
       {

           StreamWriter sw = new StreamWriter(consoleWriteLinePath + "ConsoleWriteLine_ForDeleteCheck.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
           sw.AutoFlush = true;
           Console.SetOut(sw);
           Console.WriteLine(traceText);
           sw.Close();
           sw.Dispose();
       }

       protected void ObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
       {
           try
           {
               if (ViewState["index"] == "index")
               {
                   search = ViewState["search"].ToString();
                   string item = ViewState["item"].ToString();
                   e.InputParameters["item"] = item;
                   e.InputParameters["search"] = search;
                   e.InputParameters["pagesize"] = gvItem.PageSize;
               }
               else
               {
                   char[] charsToTrim = { ',', '.', ' ' };
                   e.InputParameters["item"] = string.Empty;
                   ViewState["item"] = string.Empty;
                   search = "";
                   string InstructionNo = txtinstrauctionno.Text.Trim();
                    string Item_Code = txtitemno.Text.Trim();
                    string itemcode = string.Empty;
                    itemcode = txtitemno.Text.Trim();   // if Item_Code,
                    string replace = ",";
                    Item_Code = itemcode.Replace(",\r\n", replace).Replace(",\n", replace).Replace("\r\n", replace).Replace("\n", replace).Replace("\r", replace);
                    if (Item_Code.EndsWith(","))
                      Item_Code =Item_Code.TrimEnd(',');
                     

                    
                   string Brand_Name = txtbrandname.Text.Trim();
                   string Catalog_Information = txtcatinfo.Text.Trim();
                   int Export_Status = 0;
                   if (!String.IsNullOrWhiteSpace(ddlsksstatus.SelectedValue.ToString()))
                       Export_Status = Convert.ToInt32(ddlsksstatus.SelectedValue.ToString());
                   string ProductName = txtproductname.Text.Trim();
                    string Product_Code = txtmanproductcode.Text.Trim();
                   string Company_Name = txtconmpanyname.Text.Trim();
                   string Competition_Name = txtcompetitionname.Text.Trim();
                   string Class_Name = txtclassname.Text.Trim();
                   int Special_Flag = 0;
                   if (!String.IsNullOrWhiteSpace(ddlspecialflag.SelectedValue.ToString()))
                       Special_Flag = Convert.ToInt32(ddlspecialflag.SelectedValue);
                   int Reservation_Flag = 0;
                   if (!String.IsNullOrWhiteSpace(ddlreservationflag.SelectedValue.ToString()))
                       Reservation_Flag = Convert.ToInt32(ddlreservationflag.SelectedValue);
                   string Year = txtyear.Text.Trim();
                   string Season = txtseason.Text.Trim();
                   string Ctrl_ID = string.Empty;
                   if (!String.IsNullOrWhiteSpace(ddlshopstatus.SelectedValue.ToString()))
                       Ctrl_ID = ddlshopstatus.SelectedValue;
                   string Remark = txtremark.Text.Trim();
                   string JanCode = txtjancode.Text.Trim();
                   string Sale_Code = txtsalemanagementcode.Text.Trim();
                   string fromDate = Request.Form[txtdate.UniqueID];
                   string toDate = Request.Form[txtdateapproval.UniqueID];
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
                   string PersonInCharge = ddlpersonincharge.SelectedValue;
                   int Price = 0;
                   if (string.IsNullOrWhiteSpace(ddlSellingPrice.SelectedValue))
                   {
                       Price = -1;
                   }
                   else
                   {
                       Price = Convert.ToInt32(ddlSellingPrice.SelectedValue);
                   }
                   int ShopID = Convert.ToInt16(ddlExhibiton.SelectedValue);

                   if (chkno.Checked)
                   {
                       if (!string.IsNullOrWhiteSpace(Item_Code))
                       {
                           search += (search == "" ? "" : " AND ") + "Item_Code IN (SELECT rtrim(ltrim(Value)) as Item_Code FROM dbo.StringSplit('" + Item_Code + "'))";
                       }
                   }
                   else
                   {
                       if (!string.IsNullOrWhiteSpace(Item_Code))
                       {
                           e.InputParameters["item"] = Item_Code.TrimEnd(charsToTrim);
                           ViewState["item"] = Item_Code.TrimEnd(charsToTrim);
                           //search += (search == "" ? "" : " AND ") + "IM.ID IN (SELECT im.ID FROM Item_Master im WHERE im.Item_Code LIKE (SELECT '%'+rtrim(ltrim(Value))+'%' as Item_Code FROM dbo.StringSplit('" + Item_Code + "')))";
                       }
                   }
                   if (!string.IsNullOrWhiteSpace(Brand_Name))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Brand_Name LIKE '%' + N'" + Brand_Name + "'+ '%'";
                   }
                   if (!string.IsNullOrWhiteSpace(Catalog_Information))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Catalog_Information LIKE '%' + N'" + Catalog_Information + "'+ '%'";
                   }
                   if (Export_Status != 0)
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Export_Status = " + Export_Status;
                   }
                   if (!string.IsNullOrWhiteSpace(ProductName))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Item_Name LIKE '%' + N'" + ProductName + "'+ '%'";
                   }
                   if (!string.IsNullOrWhiteSpace(Product_Code))
                   {
                       search += (search == "" ? "" : " AND ") + "(IM.ID IN (SELECT rtrim(ltrim(Value)) as ID FROM dbo.StringSplit('" + Product_Code + "')))";
                   }
                   if (!string.IsNullOrWhiteSpace(Company_Name))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Company_Name LIKE '%' + N'" + Company_Name + "'+ '%'";
                   }
                   if (!string.IsNullOrWhiteSpace(Competition_Name))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Competition_Name LIKE '%' + N'" + Competition_Name + "'+ '%'";
                   }
                   if (!string.IsNullOrWhiteSpace(Class_Name))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Class_Name LIKE '%' + N'" + Class_Name + "'+ '%'";
                   }
                   if(Special_Flag != 0)
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Special_Flag =" + Special_Flag;
                   }
                   if (Reservation_Flag != 0)
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Reservation_Flag =" + Reservation_Flag;
                   }
                   if (!string.IsNullOrWhiteSpace(Year))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Year LIKE '%' +'" + Year + "'+ '%'";
                   }
                   if (!string.IsNullOrWhiteSpace(Season))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Season LIKE '%' + N'" + Season + "'+ '%'";
                   }
                   if (!string.IsNullOrWhiteSpace(Ctrl_ID))
                   {
                       search += (search == "" ? "" : " AND ") + "((IM.Ctrl_ID ='" + Ctrl_ID + "') OR ('" + Ctrl_ID + "'='nu' AND IM.Ctrl_ID != 'd' AND IM.Ctrl_ID != 'g'))";
                   }
                   if (!string.IsNullOrWhiteSpace(Remark))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Remarks LIKE '%' + N'" + Remark + "'+ '%'";
                   }
                   if (!string.IsNullOrWhiteSpace(JanCode))
                   {
                       search += (search == "" ? "" : " AND ") + "(IM.Item_Code IN (SELECT rtrim(ltrim(Value)) as Item_Code FROM dbo.StringSplit('" + JanCode + "')))";
                   }
                   if (!string.IsNullOrWhiteSpace(Sale_Code))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Sale_Code LIKE '%' + N'" + Sale_Code + "'+ '%'";
                   }
                   if (!string.IsNullOrWhiteSpace(FromDate.ToString()))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Approve_Date >= Convert(nvarchar(10),'" + FromDate + "',111)";
                   }
                   if (!string.IsNullOrWhiteSpace(ToDate.ToString()))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Approve_Date <= Convert(nvarchar(10),'" + ToDate + "',111)";
                   }
                   if (!string.IsNullOrWhiteSpace(PersonInCharge))
                   {
                       search += (search == "" ? "" : " AND ") + "IM.Updated_By =" + PersonInCharge;
                   }
                   if (Price != -1)
                   {
                       search += (search == "" ? "" : " AND ") + "(" + Price + "= -1 OR IM.Sale_Price = 0)";
                   }
                   if (!string.IsNullOrWhiteSpace(ShopID.ToString()))
                   {
                       if (ShopID == 3)
                       {
                           search += (search == "" ? "" : " AND ") + "(" + ShopID + "=-1 OR (IM.ID IN (SElect Item_ID FROM Item_Shop where Shop_ID=3)))";
                       }
                       else
                       {
                           search += (search == "" ? "" : " AND ") + "(" + ShopID + "=-1 OR (IM.ID IN (SELECT imr.ID AS Item_ID FROM Item_Master imr WHERE imr.ID NOT IN (SELECT Item_ID FROM Item_Shop WHERE Shop_ID=3))))";
                       }
                   }

                   e.InputParameters["search"] = search;
                   e.InputParameters["pagesize"] = gvItem.PageSize;
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