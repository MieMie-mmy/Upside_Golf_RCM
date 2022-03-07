/* 
Created By              :Kathi Aung
Created Date          :  .7.2014
Updated By             :Ei Phyo,Aye Aye Mon
Updated Date         :16.7.2014,12.8.2014

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
using ORS_RCM_BL;
using ORS_RCM.WebForms.Item;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using ORS_RCM_Common;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Text;
using System.IO;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using Excel;
using System.Diagnostics;
using ClosedXML.Excel;

namespace ORS_RCM
{
    public partial class Item_View : System.Web.UI.Page
    {
        Item_Shop_BL ItemShopBL;
        Item_ExportField_BL itfield_bl;
        String[] Exfield = new String[1000];
        string FilePath = ConfigurationManager.AppSettings["ExportFieldCSVPath"].ToString();
        string imagePath = ConfigurationManager.AppSettings["ItemImage"].ToString();
        string ExcelExport = ConfigurationManager.AppSettings["ExcelExport"].ToString();
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        String rakuten = ConfigurationManager.AppSettings["rakuten"].ToString();
        String yahoo = ConfigurationManager.AppSettings["yahoo"].ToString();
        String ponpare = ConfigurationManager.AppSettings["ponpare"].ToString();
        String jisha = ConfigurationManager.AppSettings["jisha"].ToString();
        String wowma = ConfigurationManager.AppSettings["wowma"].ToString();
        String tennis = ConfigurationManager.AppSettings["tennis"].ToString();
        Item_Master_BL ItemMasterBL;
        Item_Information_BL iteminfo_bl;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            txtItem_Code.Attributes.Add("onKeyPress", "doClick('" + btnSearch.ClientID + "',event)");
            try
            {
                if (!IsPostBack)
                {
                    itfield_bl = new Item_ExportField_BL();
                    Bind();
                    BindPerson();
                    ItemCheck_Change();
                    //for Smartcsv
                    ddlname.DataSource = itfield_bl.SmartSelectAll();
                    ddlname.DataTextField = "Export_Name";
                    ddlname.DataValueField = "ID";
                    ddlname.DataBind();
                    ddlname.Items.Insert(0, "");
                }
                else
                {
                    String ctrl = getPostBackControlName();
                    if (hfCtrl.Value.ToString().Contains("ddlShop") && String.IsNullOrWhiteSpace(ctrl))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];
                        ddlShop_SelectedIndexChanged(Convert.ToInt32(index));
                        hfCtrl.Value = String.Empty;
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
                        Item_Master_BL imbl = new Item_Master_BL();
                        Item_Master_Entity ime = GetData();
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
                        if (chkCode.Checked)
                        {
                            gvItem.DataSource = imbl.SelectAll(ime, index, gvItem.PageSize, 2, 1);
                            gvItem.DataBind();
                        }
                        else
                        {
                            gvItem.DataSource = imbl.SelectAll(ime, index, gvItem.PageSize, 1, 1);
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

        protected Item_Master_Entity GetData()
        {
            try
            {
                Item_Master_Entity ime = new Item_Master_Entity();
                DataTable dt = new DataTable();
                ime.Item_Name = txtItem_Name.Text.Trim();
                if (Request.QueryString["file"] != null)            //05052016 JMS updated
                {
                    string item_code = Request["file"].ToString();
                    String a = item_code as String;
                    ime.Item_Code = a;
                    txtItem_Code.Text = a;   //Newly Added 2015/06/27 for JMS
                    chkCode.Checked = true;
                }
                else
                {
                    ime.Item_Code = txtItem_Code.Text.TrimEnd(',').Trim();   // if Item_Code,
                    string replaceWith = ",";string itemcode = string.Empty;
                    itemcode = txtItem_Code.Text.Trim();
                    ime.Item_Code = itemcode.Replace(",\r\n", replaceWith).Replace(",\n", replaceWith).Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
                    if (ime.Item_Code.EndsWith(","))
                        ime.Item_Code = ime.Item_Code.TrimEnd(',');
                }
                ime.Catalog_Information = txtCatalog_Infromation.Text.Trim();
                ime.Brand_Name = txtBrand_Name.Text.Trim();
                ime.Competition_Name = txtCompetition_Name.Text.Trim();
                ime.Year = txtYear.Text.Trim();
                ime.Season = txtSeason.Text.Trim();
                if (string.IsNullOrWhiteSpace(ddlSpecial_Flag.SelectedValue))
                {
                    ime.Special_Flag = -1;
                }
                else
                {
                    ime.Special_Flag = Convert.ToInt32(ddlSpecial_Flag.SelectedValue);
                }
                if (string.IsNullOrWhiteSpace(ddlReservation_Flag.SelectedValue))
                {
                    ime.Reservation_Flag = -1;
                }
                else
                {
                    ime.Reservation_Flag = Convert.ToInt32(ddlReservation_Flag.SelectedValue);
                }

                if (string.IsNullOrWhiteSpace(ddlExport_Status.SelectedValue))
                {
                    ime.Export_Status = -1;
                }
                else
                {
                    ime.Export_Status = Convert.ToInt32(ddlExport_Status.SelectedValue);
                }

                if (string.IsNullOrWhiteSpace(ddlPerson.SelectedValue))
                {
                    ime.Updated_By = -1;
                }
                else
                {
                    ime.Updated_By = Convert.ToInt32(ddlPerson.SelectedValue);
                }
                ime.Ctrl_ID = ddlCtrl_ID.SelectedValue.Trim();
                ime.Color_Name = txtColor_Name.Text.Trim();
                ime.Image_Name = txtImage_Name.Text.Trim();
                ime.Cate_Name = txtCategory_Name.Text.Trim();
                //MenuSearch
                if (Request.QueryString["SearchKey"] != null)
                {
                    HtmlInputText txt = Master.FindControl("txtKeySearch") as HtmlInputText;
                    txt.Value = Request.QueryString["SearchKey"].ToString();
                    ime.MasterKeyword = txt.Value.Trim();
                }
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

        protected void Bind()
        {
            try
            {
                Item_Master_BL imbl = new Item_Master_BL();
                Item_Master_Entity ime = GetData();
                if (chkCode.Checked)
                {
                    gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    DataTable dt = imbl.SelectAll(ime, 1, gvItem.PageSize, 2, 1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like),1=search,0=select all)
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
                    DataTable dt = imbl.SelectAll(ime, 1, gvItem.PageSize, 1, 1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like),1=search,0=select all)
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

        protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DataEdit")
                {
                    string Item_Code = e.CommandArgument.ToString();
                    Response.Redirect("Item_Master.aspx?Item_Code=" + Item_Code, false);
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
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label LabelItem_Code = e.Row.FindControl("LabelItem_Code") as Label;
                    if (!String.IsNullOrWhiteSpace(LabelItem_Code.Text))
                    {
                        Button btn = e.Row.FindControl("btnSKU") as Button;
                        btn.Attributes.Add("onclick", "javascript:Show('" + LabelItem_Code.Text + "'," + btn.ClientID + ")");
                    }
                    Label l1 = e.Row.FindControl("lblID") as Label;
                    if (!String.IsNullOrWhiteSpace(l1.Text))
                    {
                        #region btnPreview
                        Button btn1 = e.Row.FindControl("btnPreview") as Button;
                        btn1.Attributes.Add("onclick", "javascript:ShowPreview('" + LabelItem_Code.Text + "','" + l1.Text + "'," + btn1.ClientID + ")");
                        #endregion

                        #region image
                        Item_Master_BL imbl = new Item_Master_BL();
                        DataTable dt = imbl.SelectItemImage(l1.Text);
                        if (dt != null)
                        {
                            Image img = new Image();
                            DataRow[] dr = dt.Select("Image_Type = 0");
                            if (dr.Count() > 0)
                            {
                                DataTable dtImage = dt.Select("Image_Type = 0").CopyToDataTable();
                                int i = 0;
                                int row = 0;
                                while (row < 5)
                                {
                                    if (row >= dtImage.Rows.Count)
                                        break;
                                    if (!String.IsNullOrWhiteSpace(dtImage.Rows[row]["Image_Name"].ToString()))
                                    {
                                        img = e.Row.FindControl("Image" + (i + 1)) as Image;
                                        img.ImageUrl = imagePath + dtImage.Rows[row]["Image_Name"].ToString();
                                        i++;
                                    }
                                    row++;
                                }
                            }
                        }
                        #endregion

                        #region Shop
                        Item_Shop_BL itemShopBL = new Item_Shop_BL();
                        DataTable dtShop = itemShopBL.SelectShopID(Convert.ToInt32(l1.Text));

                        BindShop(dtShop, e);
                        #endregion
                    }

                    Label lbl = e.Row.FindControl("lblSKUStatus") as Label;
                    CheckBox chb = e.Row.FindControl("chkItem") as CheckBox;
                    HtmlGenericControl Ppage = e.Row.FindControl("Ppage") as HtmlGenericControl;
                    HtmlGenericControl PWaitSt = e.Row.FindControl("PWaitSt") as HtmlGenericControl;
                    HtmlGenericControl PWaitL = e.Row.FindControl("PWaitL") as HtmlGenericControl;
                    HtmlGenericControl PExhibit = e.Row.FindControl("PExhibit") as HtmlGenericControl;
                    HtmlGenericControl POkSt = e.Row.FindControl("POkSt") as HtmlGenericControl;
                    HtmlGenericControl PNOk = e.Row.FindControl("PNOK") as HtmlGenericControl;
                    switch (lbl.Text)
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

                    lbl = e.Row.FindControl("lblshop") as Label;
                    HtmlGenericControl pWait = e.Row.FindControl("PWait") as HtmlGenericControl;
                    HtmlGenericControl pOk = e.Row.FindControl("POk") as HtmlGenericControl;
                    HtmlGenericControl pDel = e.Row.FindControl("PDel") as HtmlGenericControl;
                    HtmlGenericControl pInactive = e.Row.FindControl("PInactive") as HtmlGenericControl;
                    HtmlGenericControl PWarehouse = e.Row.FindControl("PWarehouse") as HtmlGenericControl;
                    HtmlGenericControl Warehouseerror = e.Row.FindControl("Warehouseerror") as HtmlGenericControl;
                    switch (lbl.Text)
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
                    lbl = e.Row.FindControl("lblMStatus") as Label;
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

        protected void btnCheckAll_Click(object sender, EventArgs e)
        {
            try
            {
                CheckChangeGrid(true);
                ItemCheck_Change();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnCheckCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CheckChangeGrid(false);
                ItemCheck_Change();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btn_Preview_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                GridViewRow row = btn.NamingContainer as GridViewRow;
                int rowindex = row.RowIndex;
                Label lbl = gvItem.Rows[rowindex].FindControl("lblID") as Label;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('../Item/Item_Preview_Form.aspx?ID=" + lbl.Text + "','_blank');", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        //Generate CSV and Exhibition Form
        protected void btnexhibition_Click(object sender, EventArgs e)
        {
            try
            {
                int p = 0;
                int mallID = 0;
                string itemIDList = null;
                string itemCodeList = null;
                string itemCodeYahooList = null;
                string itemCodePonpareList = null;
                string itemCodeAmazonList = null;
                string itemCodeJishaList = null;
                ArrayList CheckBoxArray;
                if (ViewState["checkedValue"] != null)
                {
                    CheckBoxArray = (ArrayList)ViewState["checkedValue"];
                    for (int i = 0; i < CheckBoxArray.Count; i++)
                    {
                        if (IsSelectedShop(Convert.ToInt32(CheckBoxArray[i].ToString())))   //Check Choice or not Shop
                        {
                            if (IsPost_Available_Date(Convert.ToInt32(CheckBoxArray[i].ToString())))
                            {
                                itemIDList += CheckBoxArray[i] + ",";
                            }
                            ItemShopBL = new Item_Shop_BL();
                            DataTable dt = ItemShopBL.CheckItemCodeURL(Convert.ToInt32(CheckBoxArray[i].ToString()));
                            if (dt.Rows.Count < 1)
                            {
                                
                                p++;
                                GlobalUI.MessageBox("There is no itemcode which dose not exist itemcode url cannot allow to exhibit!!");
                                itemIDList = null;
                            }
                           
                        }
                        else
                        {
                            GlobalUI.MessageBox("出品対象ショップが存在しない商品が存在します");
                            itemIDList = null;
                            break;
                        }
                        if (p > 0)
                        {
                            GlobalUI.MessageBox("There is no itemcode which dose not exist itemcode url cannot allow to exhibit!!");
                            break;

                        }
                    }
                   
                    #region ConsoleWriteLineTofile
                    for (int i = 0; i < CheckBoxArray.Count; i++)
                    {
                        DataTable Mall = new DataTable();
                        Mall = ItemShopBL.SelectMallID(Convert.ToInt32(CheckBoxArray[i].ToString()));
                        foreach (DataRow mall in Mall.Rows)
                        {
                            mallID = Convert.ToInt32(mall["Mall_ID"].ToString());
                            if (mallID == 1)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(CheckBoxArray[i].ToString()));
                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeList += itemCode["Item_Code"].ToString() + ",";
                                }
                            }
                            else if (mallID == 2)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(CheckBoxArray[i].ToString()));
                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeYahooList += itemCode["Item_Code"].ToString() + ",";
                                }
                            }
                            else if (mallID == 3)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(CheckBoxArray[i].ToString()));
                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodePonpareList += itemCode["Item_Code"].ToString() + ",";
                                }
                            }
                            else if (mallID == 4)
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(CheckBoxArray[i].ToString()));
                                foreach (DataRow itemCode in Item_Code.Rows)
                                {
                                    itemCodeAmazonList += itemCode["Item_Code"].ToString() + ",";
                                }
                            }
                            else
                            {
                                DataTable Item_Code = new DataTable();
                                Item_Code = ItemShopBL.SelectCode(Convert.ToInt32(CheckBoxArray[i].ToString()));
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
                    if (!string.IsNullOrWhiteSpace(itemCodePonpareList))
                    {
                        ConsoleWriteLine_Tofile("Title for Ponpare");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodePonpareList);
                    }
                    if (!string.IsNullOrWhiteSpace(itemCodeJishaList))
                    {
                        ConsoleWriteLine_Tofile("Title for Jisha");
                        ConsoleWriteLine_Tofile("Exhibition From 商品情報一覧(ページ制作)"); // Title to ConsoleWriteLine_Tofile
                        ConsoleWriteLine_Tofile("Item Code:" + itemCodeJishaList);
                    }
                    ConsoleWriteLine_Tofile("Date : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));// Date to  ConsoleWriteLine_Tofile
                    #endregion
                    ViewState.Remove("checkedValue");
                }
                if (!string.IsNullOrWhiteSpace(itemIDList))
                {
                    //Remove last comma from string
                    itemIDList = itemIDList.TrimEnd(','); // (OR) itemIDList.Remove(str.Length-1); 
                    Session.Remove("ItemIDList");
                    Session["ItemIDList"] = itemIDList;
                    ConsoleWriteLine_Tofile("Process Finish : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); //Process Finish
                    string url = "../Item_Exhibition/Exhibition_Confirmation.aspx?IDlist=" + 1;
                    Response.Redirect(url, false);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnQuickEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string itemIDList = null;
                ArrayList CheckBoxArray;
                if (ViewState["checkedValue"] != null)
                {
                    CheckBoxArray = (ArrayList)ViewState["checkedValue"];
                    for (int i = 0; i < CheckBoxArray.Count; i++)
                    {
                        itemIDList += CheckBoxArray[i] + ",";
                    }
                }
                if (!string.IsNullOrWhiteSpace(itemIDList))
                {
                    //Remove last comma from string
                    itemIDList = itemIDList.TrimEnd(','); // (OR) itemIDList.Remove(str.Length-1); 
                    Session["chkList"] = itemIDList;
                    string url = "../Item/Item_View3.aspx?list=" + itemIDList;
                    Response.Redirect(url, false);
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
            try
            {
                Item_Master_BL imbl = new Item_Master_BL();
                Item_Master_Entity ime = GetData();
                if (chkCode.Checked)
                {
                    gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    DataTable dt = imbl.SelectAll(ime, 1, gvItem.PageSize, 2, 1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like),1=search,0=select all)
                    int count = 0;
                    if(dt.Rows.Count>0)
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
                    DataTable dt = imbl.SelectAll(ime, 1, gvItem.PageSize, 1, 1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like),1=search,0=select all)
                    int count = 0;
                    if(dt.Rows.Count>0)
                        count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                    gvItem.DataSource = dt;
                    gvItem.DataBind();
                    gp.CalculatePaging(count, gvItem.PageSize, 1);
                    ViewState["SearchDataID"] = null;
                    ViewState["SearchDataID"] = dt;
                }
                ddlname.Enabled = true;
                ViewState.Remove("checkedValue"); // After various search and check, clean previous check value.
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string exporttype = null;
                string exportDataList = null;
                ConsoleWriteLine_Tofile("ButonClick!");
                String[] data = new String[100];
                itfield_bl = new Item_ExportField_BL();
                DataTable dtExportdata = new DataTable();
                Item_Master_BL imbl = new Item_Master_BL();
                DataTable dtCSV = new DataTable();
                DataTable dtLibrary = new DataTable();
                string stroption = ViewState["Option"] as string;
                if (ViewState["SearchDataID"] != null)
                {
                    dtExportdata = null;
                    Item_Master_Entity ime = GetData();
                    gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    if (chkCode.Checked)
                        dtExportdata = imbl.SelectAllExport(ime, 1, gvItem.PageSize, 9, 1);
                    else
                    {
                        ConsoleWriteLine_Tofile("Select Search Data");
                        ConsoleWriteLine_Tofile("Start Time:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        dtExportdata = imbl.SelectAllExport(ime, 1, gvItem.PageSize, 8, 1); //export for like search
                        ConsoleWriteLine_Tofile("End Time:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    #region ConsoleWriteLineTofile
                    ConsoleWriteLine_Tofile("Title for ExportData");
                    ConsoleWriteLine_Tofile("Start Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (dtExportdata != null && dtExportdata.Rows.Count > 0)
                    {
                        foreach (DataRow exportData in dtExportdata.Rows)
                        {
                            exportDataList += exportData["Item_Code"].ToString() + ",";
                        }
                    }
                    ConsoleWriteLine_Tofile("Item Code In ExportData: " + exportDataList);// export for like search - ConsoleWriteLineTofile
                    ConsoleWriteLine_Tofile("End Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    #endregion
                    ConsoleWriteLine_Tofile("Get ItemID");
                    ConsoleWriteLine_Tofile("Start Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    String csv = String.Empty;
                    if (dtExportdata != null && dtExportdata.Rows.Count > 0)
                        for (int u = 0; u < dtExportdata.Rows.Count; u++)
                        {
                            if (dtExportdata.Columns.Contains("ID"))
                                csv += dtExportdata.Rows[u]["ID"].ToString() + ",";
                        }
                    ConsoleWriteLine_Tofile("End Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    string ID = ddlname.SelectedItem.Value.ToString();
                    DataTable dtfield = new DataTable();
                    string str1 = "N";
                    if (!String.IsNullOrWhiteSpace(ID))
                    {
                        dtfield = itfield_bl.STSelectAllData(ID);
                        str1 = dtfield.Rows[0]["Export_Fields"].ToString();
                        exporttype=dtfield.Rows[0]["Export_Name"].ToString();
                        if (str1.Contains("商品番号")) { }
                        else str1 += "," + "商品番号";
                        string replaceWiths = "";
                        str1.Replace(System.Environment.NewLine, "replacement text");
                        str1 = Regex.Replace(str1, @"\r\n?|\n", replaceWiths);
                        str1 = Regex.Replace(str1, ",,", ",");
                        str1 = str1.TrimStart(',');
                       str1= str1.TrimEnd(',');

                       if (exporttype == "Xanax_Data")
                       {
                           #region Xanax_Data
                           str1 = str1.Replace("商品番号", "i.Item_Code as '商品番号'").Replace("商品名", "i.Item_Name as '商品名'");
                           str1 = str1.Replace("カタログ情報", "i.Catalog_Information as 'カタログ情報'").Replace("ブランド名", "i.Brand_Name as 'ブランド名'");
                           str1 = str1.Replace("競技名", "i.Competition_Name as '競技名'").Replace("分類名", "i.Class_Name as '分類名'");

                           //  str1 = str1.Replace("カラー", "Color_Name_Official as 'カラー'").Replace("サイズ", "(SELECT STUFF((SELECT DISTINCT ',' + Size_Name_Official FROM Item WITH (NOLOCK) WHERE Item_Code=i.Item_Code  FOR XML PATH('')),1,1,'')) AS 'サイズ'");
                           str1 = str1.Replace("カラー", "(SELECT STUFF((SELECT DISTINCT ',' + Color_Name_Official FROM Item WITH (NOLOCK) WHERE Item_Code=i.Item_Code  FOR XML PATH('')),1,1,'')) AS 'カラー'").Replace("サイズ", "(SELECT STUFF((SELECT DISTINCT ',' + Size_Name_Official FROM Item WITH (NOLOCK) WHERE Item_Code=i.Item_Code  FOR XML PATH('')),1,1,'')) AS 'サイズ'");

                           str1 = str1.Replace("基本情報1", "td.Template1 as '基本情報1'").Replace("基本情報内容1", "td.Template_Content1 as '基本情報内容1'");
                           str1 = str1.Replace("基本情報2", "td.Template2 as '基本情報2'").Replace("基本情報内容2", "td.Template_Content2 as '基本情報内容2'");
                           str1 = str1.Replace("基本情報3", "td.Template3 as '基本情報3'").Replace("基本情報内容3", "td.Template_Content3 as '基本情報内容3'");
                           str1 = str1.Replace("基本情報4", "td.Template4 as '基本情報4'").Replace("基本情報内容4", "td.Template_Content4 as '基本情報内容4'");
                           str1 = str1.Replace("基本情報5", "td.Template5 as '基本情報5'").Replace("基本情報内容5", "td.Template_Content5 as '基本情報内容5'");
                           str1 = str1.Replace("基本情報6", "td.Template6 as '基本情報6'").Replace("基本情報内容6", "td.Template_Content6 as '基本情報内容6'");
                           str1 = str1.Replace("基本情報7", "td.Template7 as '基本情報7'").Replace("基本情報内容7", "td.Template_Content7 as '基本情報内容7'");
                           str1 = str1.Replace("基本情報8", "td.Template8 as '基本情報8'").Replace("基本情報内容8", "td.Template_Content8 as '基本情報内容8'");
                           str1 = str1.Replace("基本情報9", "td.Template9 as '基本情報9'").Replace("基本情報内容9", "td.Template_Content9 as '基本情報内容9'");
                           str1 = str1.Replace("基本情報10", "td.Template_Content10 as '基本情報10' ").Replace("基本情報内容10", "td.Template_Content10 as '基本情報内容10'");
                           str1 = str1.Replace("基本情報11", "td.Template_Content11 as '基本情報11' ").Replace("基本情報内容11", "td.Template_Content11 as '基本情報内容11'");
                           str1 = str1.Replace("基本情報12", "td.Template_Content12 as '基本情報12' ").Replace("基本情報内容12", "td.Template_Content12 as '基本情報内容12'");
                           str1 = str1.Replace("基本情報13", "td.Template_Content13 as '基本情報13' ").Replace("基本情報内容13", "td.Template_Content13 as '基本情報内容13'");
                           str1 = str1.Replace("基本情報14", "td.Template_Content14 as '基本情報14' ").Replace("基本情報内容14", "td.Template_Content14 as '基本情報内容14'");
                           str1 = str1.Replace("基本情報15", "td.Template_Content15 as '基本情報15' ").Replace("基本情報内容15", "td.Template_Content15 as '基本情報内容15'");
                           str1 = str1.Replace("基本情報16", "td.Template_Content16 as '基本情報16' ").Replace("基本情報内容16", "td.Template_Content16 as '基本情報内容16'");
                           str1 = str1.Replace("基本情報17", "td.Template_Content17 as '基本情報17' ").Replace("基本情報内容17", "td.Template_Content17 as '基本情報内容17'");
                           str1 = str1.Replace("基本情報18", "td.Template_Content18 as '基本情報18' ").Replace("基本情報内容18", "td.Template_Content18 as '基本情報内容18'");
                           str1 = str1.Replace("基本情報19", "td.Template_Content19 as '基本情報19' ").Replace("基本情報内容19", "td.Template_Content19 as '基本情報内容19");
                           str1 = str1.Replace("基本情報20", "td.Template_Content20 as '基本情報20' ").Replace("基本情報内容20", "td.Template_Content20 as '基本情報内容20'");
                           str1 = str1.Replace("詳細情報1", "td.Detail_Template1 as '詳細情報1'").Replace("詳細情報内容1", "td.Detail_Template_Content1 as '詳細情報内容1'");
                           str1 = str1.Replace("詳細情報2", "td.Detail_Template2 as '詳細情報2'").Replace("詳細情報内容2", "td.Detail_Template_Content2 as '詳細情報内容2'");
                           str1 = str1.Replace("詳細情報3", "td.Detail_Template3 as '詳細情報3'").Replace("詳細情報内容3", "td.Detail_Template_Content3 as '詳細情報内容3'");
                           str1 = str1.Replace("詳細情報4", "td.Detail_Template4 as '詳細情報4'").Replace("詳細情報内容4", "td.Detail_Template_Content4 as '詳細情報内容4'");
                           str1 = str1.Replace("ゼット用項目（PC商品説明文）", "i.Zett_Item_Description as 'ゼット用項目（PC商品説明文）'").Replace("ゼット用項目（PC販売説明文）", "i.Zett_Sale_Description as 'ゼット用項目（PC販売説明文）'");
                           str1 = str1.Replace("関連商品1", "(SELECT TOP(1) Related_ItemCode FROM Item_Related_Item  WITH (NOLOCK) WHERE Item_ID = i.ID AND  Item_Related_Item.SN =1) as '関連商品1'");
                           str1 = str1.Replace("関連商品2", "(SELECT  TOP(1) Related_ItemCode FROM Item_Related_Item  WITH (NOLOCK) WHERE Item_ID = i.ID AND  Item_Related_Item.SN =2) as '関連商品2'");
                           str1 = str1.Replace("関連商品3", "(SELECT TOP(1) Related_ItemCode FROM Item_Related_Item  WITH (NOLOCK) WHERE Item_ID = i.ID AND  Item_Related_Item.SN =3) as '関連商品3'");
                           str1 = str1.Replace("関連商品4", "(SELECT TOP(1) Related_ItemCode FROM Item_Related_Item  WITH (NOLOCK) WHERE Item_ID = i.ID AND  Item_Related_Item.SN =4) as '関連商品4'");
                           str1 = str1.Replace("関連商品5", "(SELECT  TOP(1) Related_ItemCode FROM Item_Related_Item  WITH (NOLOCK) WHERE Item_ID = i.ID AND  Item_Related_Item.SN =5) as '関連商品5'");

                           str1 = str1.Replace("テクノロジー画像1", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 1 AND Item_Image.SN = 1)as 'テクノロジー画像1'");
                           str1 = str1.Replace("テクノロジー画像2", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 1 AND Item_Image.SN = 2)as 'テクノロジー画像2'");
                           str1 = str1.Replace("テクノロジー画像3", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 1 AND Item_Image.SN = 3)as 'テクノロジー画像3'");
                           str1 = str1.Replace("テクノロジー画像4", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 1 AND Item_Image.SN = 4)as 'テクノロジー画像4'");
                           str1 = str1.Replace("テクノロジー画像5", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 1 AND Item_Image.SN = 5)as 'テクノロジー画像5'");
                           str1 = str1.Replace("テクノロジー画像6", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 1 AND Item_Image.SN = 5)as 'テクノロジー画像6'");

                           str1 = str1.Replace("キャンペーン画像1", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 2 AND Item_Image.SN = 1)as 'キャンペーン画像1'");
                           str1 = str1.Replace("キャンペーン画像2", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 2 AND Item_Image.SN = 2)as 'キャンペーン画像2'");
                           str1 = str1.Replace("キャンペーン画像3", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 2 AND Item_Image.SN = 3)as 'キャンペーン画像3'");
                           str1 = str1.Replace("キャンペーン画像4", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 2 AND Item_Image.SN = 4)as 'キャンペーン画像4'");
                           str1 = str1.Replace("キャンペーン画像5", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 2 AND Item_Image.SN = 5)as 'キャンペーン画像5'");

                           str1 = str1.Replace("td.Template1 as '基本情報1'0", "td.Template10 as  '基本情報10'").Replace("td.Template_Content1 as '基本情報内容1'0", "td.Template_Content10 as  '基本情報内容10'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'1", "td.Template11 as  '基本情報11'").Replace("td.Template_Content1 as '基本情報内容1'1", "td.Template_Content11 as  '基本情報内容11'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'2", "td.Template12 as  '基本情報12'").Replace("td.Template_Content1 as '基本情報内容1'2", "td.Template_Content12 as  '基本情報内容12'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'3", "td.Template13 as  '基本情報13'").Replace("td.Template_Content1 as '基本情報内容1'3", "td.Template_Content13 as  '基本情報内容13'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'4", "td.Template14 as  '基本情報14'").Replace("td.Template_Content1 as '基本情報内容1'4", "td.Template_Content14 as  '基本情報内容14'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'5", "td.Template15 as  '基本情報15'").Replace("td.Template_Content1 as '基本情報内容1'5", "td.Template_Content15 as  '基本情報内容15'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'6", "td.Template16 as  '基本情報16'").Replace("td.Template_Content1 as '基本情報内容1'6", "td.Template_Content16 as  '基本情報内容16'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'7", "td.Template17 as  '基本情報17'").Replace("td.Template_Content1 as '基本情報内容1'7", "td.Template_Content17 as  '基本情報内容17'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'8", "td.Template18 as  '基本情報18'").Replace("td.Template_Content1 as '基本情報内容1'8", "td.Template_Content18 as  '基本情報内容18'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'9", "td.Template19 as  '基本情報19'").Replace("td.Template_Content1 as '基本情報内容1'9", "td.Template_Content19 as  '基本情報内容19'");
                           str1 = str1.Replace("td.Template2 as '基本情報2'0", "td.Template20 as  '基本情報20'").Replace("td.Template_Content2 as '基本情報内容2'0", "td.Template_Content20 as  '基本情報内容20'");

                           str1 = str1.Replace("基本情報1'0", "基本情報10'").Replace("基本情報内容1'0", "基本情報内容10'");
                           str1 = str1.Replace("基本情報1'1", "基本情報11'").Replace("基本情報内容1'1", "基本情報内容11'");
                           str1 = str1.Replace("基本情報1'2", "基本情報12'").Replace("基本情報内容1'2", "基本情報内容12'");
                           str1 = str1.Replace("基本情報1'3", "基本情報13'").Replace("基本情報内容1'3", "基本情報内容13'");
                           str1 = str1.Replace("基本情報1'4", "基本情報14'").Replace("基本情報内容1'4", "基本情報内容14'");
                           str1 = str1.Replace("基本情報1'5", "基本情報15'").Replace("基本情報内容1'5", "基本情報内容15'");
                           str1 = str1.Replace("基本情報1'6", "基本情報16'").Replace("基本情報内容1'6", "基本情報内容16'");
                           str1 = str1.Replace("基本情報1'7", "基本情報17'").Replace("基本情報内容1'7", "基本情報内容17'");
                           str1 = str1.Replace("基本情報1'8", "基本情報18'").Replace("基本情報内容1'8", "基本情報内容18'");
                           str1 = str1.Replace("基本情報1'9", "基本情報19'").Replace("基本情報内容1'9", "基本情報内容19'");
                           str1 = str1.Replace("基本情報2'0", "基本情報20'").Replace("基本情報内容2'0", "基本情報内容20'");

                           #endregion
                       }
                       else
                       {
                           #region

                           str1 = str1.Replace("商品番号", "i.Item_Code as '商品番号'").Replace("商品名", "i.Item_Name as '商品名'");
                           str1 = str1.Replace("カタログ情報", "i.Catalog_Information as 'カタログ情報'").Replace("ブランド名", "i.Brand_Name as 'ブランド名'");
                           str1 = str1.Replace("競技名", "i.Competition_Name as '競技名'").Replace("分類名", "i.Class_Name as '分類名'");

                           //  str1 = str1.Replace("カラー", "Color_Name_Official as 'カラー'").Replace("サイズ", "(SELECT STUFF((SELECT DISTINCT ',' + Size_Name_Official FROM Item WITH (NOLOCK) WHERE Item_Code=i.Item_Code  FOR XML PATH('')),1,1,'')) AS 'サイズ'");
                           //str1 = str1.Replace("カラー", "(SELECT STUFF((SELECT DISTINCT ',' + Color_Name_Official FROM Item WITH (NOLOCK) WHERE Item_Code=i.Item_Code  FOR XML PATH('')),1,1,'')) AS 'カラー'").Replace("サイズ", "(SELECT STUFF((SELECT DISTINCT ',' + Size_Name_Official FROM Item WITH (NOLOCK) WHERE Item_Code=i.Item_Code  FOR XML PATH('')),1,1,'')) AS 'サイズ'");

                           str1 = str1.Replace("基本情報1", "td.Template1 as '基本情報1'").Replace("基本情報内容1", "td.Template_Content1 as '基本情報内容1'");
                           str1 = str1.Replace("基本情報2", "td.Template2 as '基本情報2'").Replace("基本情報内容2", "td.Template_Content2 as '基本情報内容2'");
                           str1 = str1.Replace("基本情報3", "td.Template3 as '基本情報3'").Replace("基本情報内容3", "td.Template_Content3 as '基本情報内容3'");
                           str1 = str1.Replace("基本情報4", "td.Template4 as '基本情報4'").Replace("基本情報内容4", "td.Template_Content4 as '基本情報内容4'");
                           str1 = str1.Replace("基本情報5", "td.Template5 as '基本情報5'").Replace("基本情報内容5", "td.Template_Content5 as '基本情報内容5'");
                           str1 = str1.Replace("基本情報6", "td.Template6 as '基本情報6'").Replace("基本情報内容6", "td.Template_Content6 as '基本情報内容6'");
                           str1 = str1.Replace("基本情報7", "td.Template7 as '基本情報7'").Replace("基本情報内容7", "td.Template_Content7 as '基本情報内容7'");
                           str1 = str1.Replace("基本情報8", "td.Template8 as '基本情報8'").Replace("基本情報内容8", "td.Template_Content8 as '基本情報内容8'");
                           str1 = str1.Replace("基本情報9", "td.Template9 as '基本情報9'").Replace("基本情報内容9", "td.Template_Content9 as '基本情報内容9'");
                           str1 = str1.Replace("基本情報10", "td.Template_Content10 as '基本情報10' ").Replace("基本情報内容10", "td.Template_Content10 as '基本情報内容10'");
                           str1 = str1.Replace("基本情報11", "td.Template_Content11 as '基本情報11' ").Replace("基本情報内容11", "td.Template_Content11 as '基本情報内容11'");
                           str1 = str1.Replace("基本情報12", "td.Template_Content12 as '基本情報12' ").Replace("基本情報内容12", "td.Template_Content12 as '基本情報内容12'");
                           str1 = str1.Replace("基本情報13", "td.Template_Content13 as '基本情報13' ").Replace("基本情報内容13", "td.Template_Content13 as '基本情報内容13'");
                           str1 = str1.Replace("基本情報14", "td.Template_Content14 as '基本情報14' ").Replace("基本情報内容14", "td.Template_Content14 as '基本情報内容14'");
                           str1 = str1.Replace("基本情報15", "td.Template_Content15 as '基本情報15' ").Replace("基本情報内容15", "td.Template_Content15 as '基本情報内容15'");
                           str1 = str1.Replace("基本情報16", "td.Template_Content16 as '基本情報16' ").Replace("基本情報内容16", "td.Template_Content16 as '基本情報内容16'");
                           str1 = str1.Replace("基本情報17", "td.Template_Content17 as '基本情報17' ").Replace("基本情報内容17", "td.Template_Content17 as '基本情報内容17'");
                           str1 = str1.Replace("基本情報18", "td.Template_Content18 as '基本情報18' ").Replace("基本情報内容18", "td.Template_Content18 as '基本情報内容18'");
                           str1 = str1.Replace("基本情報19", "td.Template_Content19 as '基本情報19' ").Replace("基本情報内容19", "td.Template_Content19 as '基本情報内容19");
                           str1 = str1.Replace("基本情報20", "td.Template_Content20 as '基本情報20' ").Replace("基本情報内容20", "td.Template_Content20 as '基本情報内容20'");
                           str1 = str1.Replace("詳細情報1", "td.Detail_Template1 as '詳細情報1'").Replace("詳細情報内容1", "td.Detail_Template_Content1 as '詳細情報内容1'");
                           str1 = str1.Replace("詳細情報2", "td.Detail_Template2 as '詳細情報2'").Replace("詳細情報内容2", "td.Detail_Template_Content2 as '詳細情報内容2'");
                           str1 = str1.Replace("詳細情報3", "td.Detail_Template3 as '詳細情報3'").Replace("詳細情報内容3", "td.Detail_Template_Content3 as '詳細情報内容3'");
                           str1 = str1.Replace("詳細情報4", "td.Detail_Template4 as '詳細情報4'").Replace("詳細情報内容4", "td.Detail_Template_Content4 as '詳細情報内容4'");
                           str1 = str1.Replace("ゼット用項目（PC商品説明文）", "i.Zett_Item_Description as 'ゼット用項目（PC商品説明文）'").Replace("ゼット用項目（PC販売説明文）", "i.Zett_Sale_Description as 'ゼット用項目（PC販売説明文）'");
                           str1 = str1.Replace("関連商品1", "(SELECT TOP(1) Related_ItemCode FROM Item_Related_Item  WITH (NOLOCK) WHERE Item_ID = i.ID AND  Item_Related_Item.SN =1) as '関連商品1'");
                           str1 = str1.Replace("関連商品2", "(SELECT  TOP(1) Related_ItemCode FROM Item_Related_Item  WITH (NOLOCK) WHERE Item_ID = i.ID AND  Item_Related_Item.SN =2) as '関連商品2'");
                           str1 = str1.Replace("関連商品3", "(SELECT TOP(1) Related_ItemCode FROM Item_Related_Item  WITH (NOLOCK) WHERE Item_ID = i.ID AND  Item_Related_Item.SN =3) as '関連商品3'");
                           str1 = str1.Replace("関連商品4", "(SELECT TOP(1) Related_ItemCode FROM Item_Related_Item  WITH (NOLOCK) WHERE Item_ID = i.ID AND  Item_Related_Item.SN =4) as '関連商品4'");
                           str1 = str1.Replace("関連商品5", "(SELECT  TOP(1) Related_ItemCode FROM Item_Related_Item  WITH (NOLOCK) WHERE Item_ID = i.ID AND  Item_Related_Item.SN =5) as '関連商品5'");

                           str1 = str1.Replace("テクノロジー画像1", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 1 AND Item_Image.SN = 1)as 'テクノロジー画像1'");
                           str1 = str1.Replace("テクノロジー画像2", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 1 AND Item_Image.SN = 2)as 'テクノロジー画像2'");
                           str1 = str1.Replace("テクノロジー画像3", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 1 AND Item_Image.SN = 3)as 'テクノロジー画像3'");
                           str1 = str1.Replace("テクノロジー画像4", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 1 AND Item_Image.SN = 4)as 'テクノロジー画像4'");
                           str1 = str1.Replace("テクノロジー画像5", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 1 AND Item_Image.SN = 5)as 'テクノロジー画像5'");
                           str1 = str1.Replace("テクノロジー画像6", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 1 AND Item_Image.SN = 5)as 'テクノロジー画像6'");

                           str1 = str1.Replace("キャンペーン画像1", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 2 AND Item_Image.SN = 1)as 'キャンペーン画像1'");
                           str1 = str1.Replace("キャンペーン画像2", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 2 AND Item_Image.SN = 2)as 'キャンペーン画像2'");
                           str1 = str1.Replace("キャンペーン画像3", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 2 AND Item_Image.SN = 3)as 'キャンペーン画像3'");
                           str1 = str1.Replace("キャンペーン画像4", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 2 AND Item_Image.SN = 4)as 'キャンペーン画像4'");
                           str1 = str1.Replace("キャンペーン画像5", "(SELECT TOP(1) Image_Name  FROM Item_Image WITH (NOLOCK) WHERE Item_ID = i.ID AND Item_Image.Image_Type = 2 AND Item_Image.SN = 5)as 'キャンペーン画像5'");

                           str1 = str1.Replace("td.Template1 as '基本情報1'0", "td.Template10 as  '基本情報10'").Replace("td.Template_Content1 as '基本情報内容1'0", "td.Template_Content10 as  '基本情報内容10'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'1", "td.Template11 as  '基本情報11'").Replace("td.Template_Content1 as '基本情報内容1'1", "td.Template_Content11 as  '基本情報内容11'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'2", "td.Template12 as  '基本情報12'").Replace("td.Template_Content1 as '基本情報内容1'2", "td.Template_Content12 as  '基本情報内容12'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'3", "td.Template13 as  '基本情報13'").Replace("td.Template_Content1 as '基本情報内容1'3", "td.Template_Content13 as  '基本情報内容13'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'4", "td.Template14 as  '基本情報14'").Replace("td.Template_Content1 as '基本情報内容1'4", "td.Template_Content14 as  '基本情報内容14'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'5", "td.Template15 as  '基本情報15'").Replace("td.Template_Content1 as '基本情報内容1'5", "td.Template_Content15 as  '基本情報内容15'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'6", "td.Template16 as  '基本情報16'").Replace("td.Template_Content1 as '基本情報内容1'6", "td.Template_Content16 as  '基本情報内容16'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'7", "td.Template17 as  '基本情報17'").Replace("td.Template_Content1 as '基本情報内容1'7", "td.Template_Content17 as  '基本情報内容17'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'8", "td.Template18 as  '基本情報18'").Replace("td.Template_Content1 as '基本情報内容1'8", "td.Template_Content18 as  '基本情報内容18'");
                           str1 = str1.Replace("td.Template1 as '基本情報1'9", "td.Template19 as  '基本情報19'").Replace("td.Template_Content1 as '基本情報内容1'9", "td.Template_Content19 as  '基本情報内容19'");
                           str1 = str1.Replace("td.Template2 as '基本情報2'0", "td.Template20 as  '基本情報20'").Replace("td.Template_Content2 as '基本情報内容2'0", "td.Template_Content20 as  '基本情報内容20'");

                           str1 = str1.Replace("基本情報1'0", "基本情報10'").Replace("基本情報内容1'0", "基本情報内容10'");
                           str1 = str1.Replace("基本情報1'1", "基本情報11'").Replace("基本情報内容1'1", "基本情報内容11'");
                           str1 = str1.Replace("基本情報1'2", "基本情報12'").Replace("基本情報内容1'2", "基本情報内容12'");
                           str1 = str1.Replace("基本情報1'3", "基本情報13'").Replace("基本情報内容1'3", "基本情報内容13'");
                           str1 = str1.Replace("基本情報1'4", "基本情報14'").Replace("基本情報内容1'4", "基本情報内容14'");
                           str1 = str1.Replace("基本情報1'5", "基本情報15'").Replace("基本情報内容1'5", "基本情報内容15'");
                           str1 = str1.Replace("基本情報1'6", "基本情報16'").Replace("基本情報内容1'6", "基本情報内容16'");
                           str1 = str1.Replace("基本情報1'7", "基本情報17'").Replace("基本情報内容1'7", "基本情報内容17'");
                           str1 = str1.Replace("基本情報1'8", "基本情報18'").Replace("基本情報内容1'8", "基本情報内容18'");
                           str1 = str1.Replace("基本情報1'9", "基本情報19'").Replace("基本情報内容1'9", "基本情報内容19'");
                           str1 = str1.Replace("基本情報2'0", "基本情報20'").Replace("基本情報内容2'0", "基本情報内容20'");
                           #endregion
                       }
                    }

                   // if (ViewState["ExportField"] != null)
                  if (!String.IsNullOrWhiteSpace(ddlname.SelectedItem.Value.ToString()))
                    {
                        ConsoleWriteLine_Tofile("Get Export data");
                        ConsoleWriteLine_Tofile("Start Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        DataTable dtsmart = null;
                      if(exporttype == "Xanax_Data")
                      {
                          dtsmart = itfield_bl.SmartXanaxCSV(csv, str1); 
                      }
                      else
                          dtsmart = itfield_bl.SmartCSV(csv, str1);
                        ConsoleWriteLine_Tofile("End Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        if (dtsmart.Columns.Contains("Item_Description_PC"))
                        {
                            dtsmart.Columns["Item_Description_PC"].ColumnName = "PC商品説明文";
                        }
                        if (dtsmart.Columns.Contains("Sale_Description_PC"))
                        {
                            dtsmart.Columns["Sale_Description_PC"].ColumnName = "PC販売説明文";
                        }
                        ConsoleWriteLine_Tofile("Remove Duplicate");
                        ConsoleWriteLine_Tofile("Start Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        ConsoleWriteLine_Tofile("End Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        DataTable dts = dtsmart.Copy();
                        dtsmart.Clear();
                         DataTable dt = new DataTable();
                         itfield_bl = new Item_ExportField_BL();
                         string fieldid = ddlname.SelectedItem.Value.ToString();
                         dtfield = itfield_bl.STSelectAllData(fieldid);
                        if (dtfield != null && dtfield.Rows.Count > 0)
                        {
                            ConsoleWriteLine_Tofile("Remove Duplicate");
                            ConsoleWriteLine_Tofile("Start Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            string field = dtfield.Rows[0]["Export_Fields"].ToString();
                            if (field.Contains("商品番号")) { }
                            else field += "," + "商品番号";

                            Exfield = field.Split(',');

                            DataTable dtItemImage = new DataTable();

                            for (int i = 0; i < Exfield.Count(); i++)
                            {
                                foreach (DataColumn dc in dtsmart.Columns)
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
                                CopyColumns(dts, dtCSV, Exfield);
                            }

                            ConsoleWriteLine_Tofile("End Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        ConsoleWriteLine_Tofile("ExcelExport");

                        string physicalPath = null;
                        string path = null;
                        if (exporttype == "Xanax_Data")
                        {
                            #region for Xanax
                            if (File.Exists(Server.MapPath("~/Excel_Export/Item_Export.xlsx")))
                            {
                                File.Delete(Server.MapPath("~/Excel_Export/Item_Export.xlsx"));
                            }
                            physicalPath = ExcelExport + "Item_Export.xlsx";
                            path = Server.MapPath("~/Excel_Export/Item_Export.xlsx");
                            File.Copy(physicalPath, path);
                            #endregion
                        }
                        else
                        {
                            #region for smart
                            if (File.Exists(Server.MapPath("~/Excel_Export/Item_Info.xlsx")))
                            {
                                File.Delete(Server.MapPath("~/Excel_Export/Item_Info.xlsx"));
                            }
                            physicalPath = ExcelExport + "Item_Info.xlsx";
                            path = Server.MapPath("~/Excel_Export/Item_Info.xlsx");
                            File.Copy(physicalPath, path);
                            #endregion
                        }//
                        using (XLWorkbook wb = new XLWorkbook(path))
                        {
                            IXLWorksheet ws = wb.Worksheet("商品説明文");
                            for (int i = 0; i < dtCSV.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dtCSV.Columns.Count; j++)
                                {
                                    string value = ws.Cell(1, j).Value.ToString();
                                    if (!String.IsNullOrWhiteSpace(dtCSV.Rows[i][value].ToString()) && !dtCSV.Rows[i][value].ToString().Equals("Null"))
                                        ws.Cell(i + 2, j).Value = "'" + ReplaceBackspace(dtCSV.Rows[i][value].ToString());
                                }
                            }
                            for (int i = 2; i <= 15; i++)
                            {
                                for (int j = 1; j <= dtCSV.Columns.Count; j++)
                                {
                                    IXLCell cell = ws.Cell(i, j);
                                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            wb.SaveAs(path);
                        }
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/msexcel";
                        Response.AddHeader("content-disposition", "attachment;filename=商品説明文" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");
                        Response.HeaderEncoding = System.Text.Encoding.GetEncoding("shift_jis");
                        Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift_jis");
                        Response.Flush();
                        Response.TransmitFile(path);
                        Response.End();
                    }
                    else { GlobalUI.MessageBox("Please Select Export Field!"); }
                }
                else { GlobalUI.MessageBox("Please Select Export Field!"); }
                ddlname.DataSource = itfield_bl.SmartSelectAll();
                ddlname.DataTextField = "Export_Name";
                ddlname.DataValueField = "ID";
                ddlname.DataBind();
                ddlname.Items.Insert(0, "");
            }

            catch (Exception ex)
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "ShopID=Excel_Export" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
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

        protected Boolean CheckColumn(String[] colName, DataTable dt)
        {
            try
            {
                DataColumnCollection col = dt.Columns;
                for (int i = 0; i < colName.Length; i++)
                {
                    if (!col.Contains(colName[i]))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

        protected void Colnamereplace(DataTable dt) 
        {
            if (dt.Columns.Contains("Item_Code"))
            {
                dt.Columns["Item_Code"].ColumnName = "商品番号";
            }
            if (dt.Columns.Contains("Item_Name"))
            {
                dt.Columns["Item_Name"].ColumnName = "商品名";
            }
            if (dt.Columns.Contains("Catalog_Information"))
            {
                dt.Columns["Catalog_Information"].ColumnName = "カタログ情報";
            }
            if (dt.Columns.Contains("Brand_Name"))
            {
                dt.Columns["Brand_Name"].ColumnName = "ブランド名";
            }
            if (dt.Columns.Contains("Competition_Name"))
            {
                dt.Columns["Competition_Name"].ColumnName = "競技名";
            }
            if (dt.Columns.Contains("Class_Name"))
            {
                dt.Columns["Class_Name"].ColumnName = "分類名";
            }
            if (dt.Columns.Contains("Template1"))
            {
                dt.Columns["Template1"].ColumnName = "基本情報1";
            }
            if (dt.Columns.Contains("Template_Content1"))
            {
                dt.Columns["Template_Content1"].ColumnName = "基本情報内容1";
            }
            if (dt.Columns.Contains("Template2"))
            {
                dt.Columns["Template2"].ColumnName = "基本情報2";
            }
            if (dt.Columns.Contains("Template_Content2"))
            {
                dt.Columns["Template_Content2"].ColumnName = "基本情報内容2";
            }
            if (dt.Columns.Contains("Template3"))
            {
                dt.Columns["Template3"].ColumnName = "基本情報3";
            }
            if (dt.Columns.Contains("Template_Content3"))
            {
                dt.Columns["Template_Content3"].ColumnName = "基本情報内容3";
            }
            if (dt.Columns.Contains("Item_Description_PC"))
            {
                dt.Columns["Item_Description_PC"].ColumnName = "PC商品説明文";
            }
            if (dt.Columns.Contains("Template4"))
            {
                dt.Columns["Template4"].ColumnName = "基本情報4";
            }
            if (dt.Columns.Contains("Template_Content4"))
            {
                dt.Columns["Template_Content4"].ColumnName = "基本情報内容4";
            }
            if (dt.Columns.Contains("Template5"))
            {
                dt.Columns["Template5"].ColumnName = "基本情報5";
            }
            if (dt.Columns.Contains("Template_Content5"))
            {
                dt.Columns["Template_Content5"].ColumnName = "基本情報内容5";
            }
            if (dt.Columns.Contains("Template6"))
            {
                dt.Columns["Template6"].ColumnName = "基本情報6";
            }
            if (dt.Columns.Contains("Template_Content6"))
            {
                dt.Columns["Template_Content6"].ColumnName = "基本情報内容6";
            }
            if (dt.Columns.Contains("Template7"))
            {
                dt.Columns["Template7"].ColumnName = "基本情報7";
            }
            if (dt.Columns.Contains("Template_Content7"))
            {
                dt.Columns["Template_Content7"].ColumnName = "基本情報内容7";
            }
            if (dt.Columns.Contains("Template8"))
            {
                dt.Columns["Template8"].ColumnName = "基本情報8";
            }
            if (dt.Columns.Contains("Template_Content8"))
            {
                dt.Columns["Template_Content8"].ColumnName = "基本情報内容8";
            }
            if (dt.Columns.Contains("Template9"))
            {
                dt.Columns["Template9"].ColumnName = "基本情報9";
            }
            if (dt.Columns.Contains("Template_Content9"))
            {
                dt.Columns["Template_Content9"].ColumnName = "基本情報内容9";
            }
            if (dt.Columns.Contains("Template10"))
            {
                dt.Columns["Template10"].ColumnName = "基本情報10";
            }
            if (dt.Columns.Contains("Template_Content10"))
            {
                dt.Columns["Template_Content10"].ColumnName = "基本情報内容10";
            }
            if (dt.Columns.Contains("Template11"))
            {
                dt.Columns["Template11"].ColumnName = "基本情報11";
            }
            if (dt.Columns.Contains("Template_Content11"))
            {
                dt.Columns["Template_Content11"].ColumnName = "基本情報内容11";
            }
            if (dt.Columns.Contains("Template12"))
            {
                dt.Columns["Template12"].ColumnName = "基本情報12";
            }
            if (dt.Columns.Contains("Template_Content12"))
            {
                dt.Columns["Template_Content12"].ColumnName = "基本情報内容12";
            }
            if (dt.Columns.Contains("Template13"))
            {
                dt.Columns["Template13"].ColumnName = "基本情報13";
            }
            if (dt.Columns.Contains("Template_Content13"))
            {
                dt.Columns["Template_Content13"].ColumnName = "基本情報内容13";
            }
            if (dt.Columns.Contains("Template14"))
            {
                dt.Columns["Template14"].ColumnName = "基本情報14";
            }
            if (dt.Columns.Contains("Template_Content14"))
            {
                dt.Columns["Template_Content14"].ColumnName = "基本情報内容14";
            }
            if (dt.Columns.Contains("Template15"))
            {
                dt.Columns["Template15"].ColumnName = "基本情報15";
            }
            if (dt.Columns.Contains("Template_Content15"))
            {
                dt.Columns["Template_Content15"].ColumnName = "基本情報内容15";
            }
            if (dt.Columns.Contains("Template16"))
            {
                dt.Columns["Template16"].ColumnName = "基本情報16";
            }
            if (dt.Columns.Contains("Template_Content16"))
            {
                dt.Columns["Template_Content16"].ColumnName = "基本情報内容16";
            }
            if (dt.Columns.Contains("Template17"))
            {
                dt.Columns["Template17"].ColumnName = "基本情報17";
                if (dt.Columns.Contains("Template_Content17"))
                {
                    dt.Columns["Template_Content17"].ColumnName = "基本情報内容17";
                }
                if (dt.Columns.Contains("Template18"))
                {
                    dt.Columns["Template18"].ColumnName = "基本情報18";
                }
                if (dt.Columns.Contains("Template_Content18"))
                {
                    dt.Columns["Template_Content18"].ColumnName = "基本情報内容18";
                }
                if (dt.Columns.Contains("Template19"))
                {
                    dt.Columns["Template19"].ColumnName = "基本情報19";
                }
                if (dt.Columns.Contains("Template_Content19"))
                {
                    dt.Columns["Template_Content19"].ColumnName = "基本情報内容19";
                }
                if (dt.Columns.Contains("Template20"))
                {
                    dt.Columns["Template20"].ColumnName = "基本情報20";
                }
                if (dt.Columns.Contains("Template_Content20"))
                {
                    dt.Columns["Template_Content20"].ColumnName = "基本情報内容20";
                }
                if (dt.Columns.Contains("Detail_Template1"))
                {
                    dt.Columns["Detail_Template1"].ColumnName = "詳細情報1";
                }
                if (dt.Columns.Contains("Detail_Template_Content1"))
                {
                    dt.Columns["Detail_Template_Content1"].ColumnName = "詳細情報内容1";
                }
                if (dt.Columns.Contains("Detail_Template2"))
                {
                    dt.Columns["Detail_Template2"].ColumnName = "詳細情報2";
                }
                if (dt.Columns.Contains("Detail_Template_Content2"))
                {
                    dt.Columns["Detail_Template_Content2"].ColumnName = "詳細情報内容2";
                }
                if (dt.Columns.Contains("Detail_Template3"))
                {
                    dt.Columns["Detail_Template3"].ColumnName = "詳細情報3";
                }
                if (dt.Columns.Contains("Detail_Template_Content3"))
                {
                    dt.Columns["Detail_Template_Content3"].ColumnName = "詳細情報内容3";
                }
                if (dt.Columns.Contains("Detail_Template4"))
                {
                    dt.Columns["Detail_Template4"].ColumnName = "詳細情報4";
                }
                if (dt.Columns.Contains("Detail_Template_Content4"))
                {
                    dt.Columns["Detail_Template_Content4"].ColumnName = "詳細情報内容4";
                }
                if (dt.Columns.Contains("Item_Description_PC"))
                {
                    dt.Columns["Item_Description_PC"].ColumnName = "PC商品説明文";
                }
                if (dt.Columns.Contains("Sale_Description_PC"))
                {
                    dt.Columns["Sale_Description_PC"].ColumnName = "PC販売説明文";
                }
                if (dt.Columns.Contains("Related_ItemCode"))
                {
                    dt.Columns["Related_ItemCode"].ColumnName = "関連商品1";
                }
                dt.AcceptChanges();
            }
        }

        protected void lnkdownload_Click(object sender, EventArgs e)
        {
            Download(FilePath + lnkdownload.Text);
        }

        protected void chkall_CheckedChanged(object sender, EventArgs e)
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

        protected void chkItem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;
                GridViewRow row = chk.NamingContainer as GridViewRow;
                int rowIndex = row.RowIndex;

                Label lbl = gvItem.Rows[rowIndex].FindControl("lblID") as Label;

                if (ViewState["checkedValue"] != null)
                {
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    CheckBox chkHeader = gvItem.HeaderRow.FindControl("chkall") as CheckBox;
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
                gvItem.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
                Bind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void ddlShop_SelectedIndexChanged(int index)
        {
            try
            {
                //String rakuten = "http://item.rakuten.co.jp/";
                //String yahoo = "http://store.shopping.yahoo.co.jp/";
                //String ponpare = "http://store.ponparemall.com/";
                //String jisha = "http://203.137.92.31:8080/product/";

                DropDownList ddl = gvItem.Rows[index].FindControl("ddlShop") as DropDownList;

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

                LinkButton lnk = gvItem.Rows[index].FindControl("btnEdit") as LinkButton;
                String itemCode = lnk.Text;

                if (mallid.Equals("1"))
                {
                    rakuten += shopName + "/" + itemCode;
                    Response.Redirect(rakuten, false);
                    //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey1", "ShopPreview('"+rakuten+"')", true);
                }
                else if (mallid.Equals("2"))
                {
                    yahoo += shopName + "/" + itemCode + ".html";
                    Response.Redirect(yahoo, false);
                    //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey1", "ShopPreview('" + yahoo + "')", true);
                }
                else if (mallid.Equals("3"))
                {
                    ponpare += shopName + "/goods/" + itemCode + "/";
                    Response.Redirect(ponpare, false);
                    //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey1", "ShopPreview('" + ponpare + "')", true);
                }
                else if (mallid.Equals("4"))
                {
                    String lot = iteminfo_bl.Get_Lot_Number(itemCode);
                    wowma += lot;
                    Response.Redirect(wowma, false);
                }

                else if (mallid.Equals("5"))
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

        public void BindShop(DataTable dt, GridViewRowEventArgs e)
        {
            try
            {
                DropDownList ddlShop = e.Row.FindControl("ddlShop") as DropDownList;
                ddlShop.DataSource = dt;
                ddlShop.DataValueField = "Shop_Name";
                ddlShop.DataBind();
                ddlShop.Items.Insert(0, "ショップ選択");
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

                ddlPerson.DataSource = itbl.bindDDL();
                ddlPerson.DataTextField = "User_Name";
                ddlPerson.DataValueField = "ID";
                ddlPerson.DataBind();
                ddlPerson.Items.Insert(0, "");
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
                        Label lbl = gvItem.Rows[i].FindControl("lblID") as Label;
                        CheckBox ckb = gvItem.Rows[i].FindControl("chkItem") as CheckBox;
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

        protected void ItemCheck_Change()
        {
            try
            {
                if (ViewState["checkedValue"] != null)
                {
                    //checked id list
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    CheckBox chk;
                    Label lbl;

                    //set checkbox as check when gridview row's id is contain in checked id arraylist
                    for (int i = 0; i < gvItem.Rows.Count; i++)
                    {
                        chk = gvItem.Rows[i].FindControl("chkItem") as CheckBox;
                        lbl = gvItem.Rows[i].FindControl("lblID") as Label;

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

        protected Boolean IsAllCheck(ArrayList arrlst)
        {
            try
            {
                if (ViewState["ItemAll"] != null)
                {
                    DataTable dtItemAll = ViewState["ItemAll"] as DataTable;
                    for (int i = 0; i < dtItemAll.Rows.Count; i++)
                    {
                        if (!arrlst.Contains(dtItemAll.Rows[i]["ID"].ToString()))
                            return false;
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

        protected Boolean IsSelectedShop(int Item_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                ItemShopBL = new Item_Shop_BL();
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

        protected Boolean IsPost_Available_Date(int Item_ID)
        {
            try
            {
                ItemMasterBL = new Item_Master_BL();
                return ItemMasterBL.IsPost_Available_Date(Item_ID);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

        protected Boolean IsSelectedCode(int Item_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                ItemMasterBL = new Item_Master_BL();
                ItemShopBL = new Item_Shop_BL();
                dt = ItemShopBL.SelectCode(Item_ID);
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

        protected void lnkItemNo_Click(String index1)
        {
            try
            {
                int index = Convert.ToInt32(index1);
                Label lnk = gvItem.Rows[index].FindControl("lnkItemNo") as Label;
                //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey1", "ShopPreview('" +  + "')", true);
                Response.Redirect("Item_Master.aspx?Item_Code=" + lnk.Text, false);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
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

        protected void CSV(DataTable dt, string name)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath(FilePath + name), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dt, writer, true);
                }
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
                        headerValues.Add(column.ColumnName.ToUpper());
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

        protected void Download(string filecheck)
        {
            if (File.Exists(Server.MapPath(filecheck)))
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
                byte[] data = req.DownloadData(Server.MapPath(filecheck));
                response.BinaryWrite(data);
                response.End();

            }
            else
            {
                GlobalUI.MessageBox("File doesn't exist!");
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
                        string colname = columns[i];
                        if (!String.IsNullOrWhiteSpace(colname))
                        {
                            if (ContainColumn(colname, source) && ContainColumn(colname, dest))
                            {
                                destRow[colname] = sourcerow[colname];
                            }
                            else
                            {
                                GlobalUI.MessageBox(String.Format("Invalid Export Field ({0}) !", colname));
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

        static void ConsoleWriteLine_Tofile(string traceText)
        {

            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "ConsoleWriteLine.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
    }
}