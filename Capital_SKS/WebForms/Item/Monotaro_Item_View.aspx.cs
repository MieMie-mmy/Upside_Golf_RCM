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

namespace ORS_RCM.WebForms.Item
{
    public partial class Monotaro_Item_View : System.Web.UI.Page
    {
        Item_Shop_BL ItemShopBL;
        Item_ExportField_BL itfield_bl;
        String[] Exfield = new String[1000];
        string FilePath = ConfigurationManager.AppSettings["ExportFieldCSVPath"].ToString();
        string imagePath = ConfigurationManager.AppSettings["ItemImage"].ToString();
        string ExcelExport = ConfigurationManager.AppSettings["ExcelExport"].ToString();
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        Item_Master_BL ItemMasterBL;

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
                }
                else
                {
                    String ctrl = getPostBackControlName();
                    if (hfCtrl.Value.ToString().Contains("lnkItemNo") && String.IsNullOrWhiteSpace(ctrl))
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
                            gvItem.DataSource = imbl.Monotaro_SelectAll(ime, index, gvItem.PageSize, 2, 1);
                            gvItem.DataBind();
                        }
                        else
                        {
                            gvItem.DataSource = imbl.Monotaro_SelectAll(ime, index, gvItem.PageSize, 1, 1);
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
                }
                ime.Catalog_Information = txtCatalog_Infromation.Text.Trim();
                ime.Brand_Name = txtBrand_Name.Text.Trim();
                ime.Competition_Name = txtCompetition_Name.Text.Trim();
                ime.Year = txtYear.Text.Trim();
                ime.Season = txtSeason.Text.Trim();
                //if (string.IsNullOrWhiteSpace(ddlSpecial_Flag.SelectedValue))
                //{
                //    ime.Special_Flag = -1;
                //}
                //else
                //{
                //    ime.Special_Flag = Convert.ToInt32(ddlSpecial_Flag.SelectedValue);
                //}
                if (string.IsNullOrWhiteSpace(ddlReservation_Flag.SelectedValue))
                {
                    ime.Reservation_Flag = -1;
                }
                else
                {
                    ime.Reservation_Flag = Convert.ToInt32(ddlReservation_Flag.SelectedValue);
                }
                if (string.IsNullOrWhiteSpace(ddlPerson.SelectedValue))
                {
                    ime.Updated_By = -1;
                }
                else
                {
                    ime.Updated_By = Convert.ToInt32(ddlPerson.SelectedValue);
                }
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
                ime.Ready = Convert.ToInt16(ddlReaddy.SelectedValue);
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
                string shopid = ddlExhibiton.SelectedItem.Value;
                if (chkCode.Checked)
                {
                    gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    DataTable dt = imbl.Monotaro_SelectAll(ime, 1, gvItem.PageSize, 2, 1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like),1=search,0=select all)
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
                    DataTable dt = imbl.Monotaro_SelectAll(ime, 1, gvItem.PageSize, 1, 1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like),1=search,0=select all)
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
                    Label l1 = e.Row.FindControl("lblID") as Label;
                    if (!String.IsNullOrWhiteSpace(l1.Text))
                    {
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

                        #region Delivery
                        Label type = e.Row.FindControl("lbldeliverytype") as Label;
                        Label method = e.Row.FindControl("lbldeliverymethod") as Label;
                        Label cod = e.Row.FindControl("lblCOD") as Label;
                        Label kasama = e.Row.FindControl("lblKasama") as Label;
                        BindDelivery(e,type.Text,method.Text,cod.Text,kasama.Text);
                        #endregion
                    }
                    Label lbl = e.Row.FindControl("lblSKUStatus") as Label;
                    HtmlGenericControl Ppage = e.Row.FindControl("Ppage") as HtmlGenericControl;
                    HtmlGenericControl PWaitSt = e.Row.FindControl("PWaitSt") as HtmlGenericControl;
                    switch (lbl.Text)
                    {
                        case "m":
                            {
                                Ppage.Visible = true;
                                PWaitSt.Visible = false;
                                break;
                            }
                        case "nm":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = true;
                                break;
                            }
                        case "":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = true;
                                break;
                            }
                    }
                    lbl = e.Row.FindControl("lblReadyStatus") as Label;
                    HtmlGenericControl PReady = e.Row.FindControl("PReady") as HtmlGenericControl;
                    HtmlGenericControl PNReady = e.Row.FindControl("PNReady") as HtmlGenericControl;
                    switch (lbl.Text)
                    {
                        case "r":
                            {
                                PReady.Visible = true;
                                PNReady.Visible = false;
                                break;
                            }
                        case "nr":
                            {
                                PReady.Visible = false;
                                PNReady.Visible = true;
                                break;
                            }
                        case "":
                            {
                                PReady.Visible = false;
                                PNReady.Visible = true;
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

        public void BindDelivery(GridViewRowEventArgs e,string type,string method,string cod,string kasama)
        {
            try
            {
                Item_Master_BL imeBL = new Item_Master_BL();

                DropDownList ddldeliverymethod = e.Row.FindControl("ddldeliverymethod") as DropDownList;
                DataTable dtdeliverymethod = imeBL.BindMonotaro("Delivery_Method");
                ddldeliverymethod.DataSource = dtdeliverymethod;
                ddldeliverymethod.DataTextField = "Delivery_Method";
                ddldeliverymethod.DataValueField = "Delivery_Method_ID";
                ddldeliverymethod.DataBind();
                ddldeliverymethod.SelectedValue = method;

                DropDownList ddldeliverytype = e.Row.FindControl("ddldeliverytype") as DropDownList;
                DataTable dtdeliverytype = imeBL.BindMonotaro("Delivery_Type");
                ddldeliverytype.DataSource = dtdeliverytype;
                ddldeliverytype.DataTextField = "Delivery_Type_Name";
                ddldeliverytype.DataBind();
                ddldeliverytype.SelectedValue = type;

                DropDownList ddlCOD = e.Row.FindControl("ddlCOD") as DropDownList;
                DataTable dtdeliveryfees = imeBL.BindMonotaro("COD");
                ddlCOD.DataSource = dtdeliveryfees;
                ddlCOD.DataTextField = "COD_Name";
                ddlCOD.DataValueField = "COD_Value";
                ddlCOD.DataBind();
                ddlCOD.SelectedValue = cod;

                DropDownList ddlKasama = e.Row.FindControl("ddlKasama") as DropDownList;
                DataTable dtksmavaliable = imeBL.BindMonotaro("Customer_Delivery_Type");
                ddlKasama.DataSource = dtksmavaliable;
                ddlKasama.DataTextField = "Customer_Delivery_Type";
                ddlKasama.DataValueField = "Customer_Delivery_Type_ID";
                ddlKasama.DataBind();
                ddlKasama.SelectedValue = kasama;
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
                string itemIDList = null;
                ArrayList CheckBoxArray;
                Exhibition_List_BL ehb = new Exhibition_List_BL();
                if (ViewState["checkedValue"] != null)
                {
                    CheckBoxArray = (ArrayList)ViewState["checkedValue"];
                    for (int i = 0; i < CheckBoxArray.Count; i++)
                    {
                        if (CheckRequiredData(Convert.ToInt32(CheckBoxArray[i].ToString())))   //Check Choice or not Shop
                        {
                            itemIDList += CheckBoxArray[i] + ",";
                        }
                        else
                        {
                            GlobalUI.MessageBox("Data is not Complete to sale!!!");
                            CheckBoxArray = null;
                            break;
                        }
                    }
                    ViewState.Remove("checkedValue");
                }
                if (!string.IsNullOrWhiteSpace(itemIDList))
                {
                    //Remove last comma from string
                    itemIDList = itemIDList.TrimEnd(','); // (OR) itemIDList.Remove(str.Length-1);
                    if (!string.IsNullOrWhiteSpace(itemIDList))
                    {
                        string[] strArr = itemIDList.Split(',');
                        for (int i = 0; i < strArr.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(strArr[i]))
                            {
                                int eid;
                                ehb.InsertMonotaroShop(Convert.ToInt32(strArr[i]),"exhibition");
                                eid = ehb.Exhibition_List_Insert(Convert.ToInt32(strArr[i]), "m", User_ID);
                            }
                        }
                        //DataCollect
                        if (!string.IsNullOrWhiteSpace(itemIDList))
                        {
                            DataTable dtItemMaster = ehb.GetItemData(itemIDList, 3);
                            ehb.SaveLogExhibition(dtItemMaster, itemIDList, 3);
                            ehb.ChangeFlag(itemIDList, 3);
                        }
                        //SelectMonotaroData
                        DataTable dtItem = ehb.SelectLogExhibitionItem(3, 3);
                        if (dtItem != null && dtItem.Rows.Count> 0) 
                        {
                            foreach (DataRow drFlag in dtItem.Rows)
                            {
                                ehb.ChangeIsGeneratedCSVFlag(int.Parse(drFlag["Exhibit_ID"].ToString()), int.Parse(drFlag["Item_ID"].ToString()), 3);
                            }
                            String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                            dtItem = FormatFile(dtItem);
                            string filename = "基礎情報登録$" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx";
                            Excel(dtItem, filename);
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

        public void checkboxclear()
        {
            foreach (GridViewRow row in gvItem.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("chkItem");
                    if (chkrow.Checked)
                        chkrow.Checked = false;
                }
            }
            CheckBox chkrow1 = (CheckBox)gvItem.HeaderRow.FindControl("chkall");
            if (chkrow1.Checked)
                chkrow1.Checked = false;
            
        } 

        public void Excel(DataTable dt, string filename)
        {
            try
            {
                string physicalPath = null;
                string path = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (File.Exists(Server.MapPath("~/Excel_Export/基礎情報登録.xlsx")))
                    {
                        File.Delete(Server.MapPath("~/Excel_Export/基礎情報登録.xlsx"));
                    }
                    physicalPath = ExcelExport + "基礎情報登録.xlsx";
                    path = Server.MapPath("~/Excel_Export/基礎情報登録.xlsx");
                    File.Copy(physicalPath, path);
                    using (XLWorkbook wb = new XLWorkbook(path))
                    {
                        IXLWorksheet ws = wb.Worksheet("基礎情報登録");
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
                                if (i > 1)
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
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                    Response.HeaderEncoding = System.Text.Encoding.GetEncoding("shift_jis");
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift_jis");
                    Response.Flush();
                    Response.TransmitFile(path);
                    //Response.Redirect(path, false);
                    //Context.ApplicationInstance.CompleteRequest();
                    //Response.End();
                    Response.SuppressContent = true;
                    Context.ApplicationInstance.CompleteRequest();
                }

            }
            catch (Exception ex)
            {
                ConsoleWriteLine_Tofile(ex.ToString());
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public DataTable FormatFile(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Remove("Exhibit_ID");
                dt.Columns.Remove("Shop_ID");
                dt.Columns.Remove("Item_ID");
                dt.Columns.Remove("IsSKU");
                dt.Columns.Remove("Ctrl_ID");
                dt.Columns.Remove("Item_Code");
                dt.Columns.Remove("Item_Code_URL");
                dt.Columns.Remove("Item_Name");
                dt.AcceptChanges();
            }
            return dt;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Item_Master_BL imbl = new Item_Master_BL();
                Item_Master_Entity ime = GetData();
                string shopid = ddlExhibiton.SelectedItem.Value;
                if (chkCode.Checked)
                {
                    gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                    DataTable dt = imbl.Monotaro_SelectAll(ime, 1, gvItem.PageSize, 2, 1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like),1=search,0=select all)
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
                    DataTable dt = imbl.Monotaro_SelectAll(ime, 1, gvItem.PageSize, 1, 1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like),1=search,0=select all)
                    int count = 0;
                    if (dt.Rows.Count > 0)
                        count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                    gvItem.DataSource = dt;
                    gvItem.DataBind();
                    gp.CalculatePaging(count, gvItem.PageSize, 1);
                    ViewState["SearchDataID"] = null;
                    ViewState["SearchDataID"] = dt;
                }
                ViewState.Remove("checkedValue"); // After various search and check, clean previous check value.
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
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

        protected Boolean CheckRequiredData(int Item_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                Item_Master_BL im = new Item_Master_BL();
                dt = im.CheckRequiredData(Item_ID);
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