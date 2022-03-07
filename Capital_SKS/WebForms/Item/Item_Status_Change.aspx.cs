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
using System.Diagnostics;
using ClosedXML.Excel;
using Excel;

namespace ORS_RCM.WebForms.Item
{
    public partial class Item_Status_Change : System.Web.UI.Page
    {
        Item_Shop_BL ItemShopBL;
        Item_ExportField_BL itfield_bl;
        String[] Exfield = new String[1000];
        string imagePath = ConfigurationManager.AppSettings["ItemImage"].ToString();
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        String rakuten = ConfigurationManager.AppSettings["rakuten"].ToString();
        String yahoo = ConfigurationManager.AppSettings["yahoo"].ToString();
        String ponpare = ConfigurationManager.AppSettings["ponpare"].ToString();
        String jisha = ConfigurationManager.AppSettings["jisha"].ToString();
        Item_Master_BL imbl;

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
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["list"] != null)
                    {
                        string list = null;
                        imbl = new Item_Master_BL();
                        list = Session["ItemIDList"].ToString();
                        DataTable dt = new DataTable();
                        gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                        dt = imbl.SelectChangeStatusItem(list, 1, gvItem.PageSize);
                        int count = 0;
                        if (dt.Rows.Count > 0)
                            count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                        gvItem.DataSource = dt;
                        gvItem.DataBind();
                        gp.CalculatePaging(count, gvItem.PageSize, 1);
                    }
                    else
                    {
                        txtItem_Code.Attributes.Add("onKeyPress", "doClick('" + btnSearch.ClientID + "',event)");
                        DataTable dt = new DataTable();
                        gvItem.DataSource = dt;
                        gvItem.DataBind();
                        gp.Visible = false;
                    }
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
                            gvItem.DataSource = imbl.SelectAllByStatus(ime,index,gvItem.PageSize,2,1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like),1=search,0=select all)
                            gvItem.DataBind();
                        }
                        else
                        {
                            gvItem.DataSource = imbl.SelectAllByStatus(ime,index,gvItem.PageSize,1,1);
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
                if (rdoSKSStauts.Checked == true)
                {
                     ime.Export_Status = Convert.ToInt32(ddlExport_Status.SelectedValue);
                     ime.Ctrl_ID = null;
                }
                else if (rdoShopStatus.Checked == true)
                {
                    ime.Export_Status = -1;
                    ime.Ctrl_ID = ddlCtrl_ID.SelectedValue.Trim();
                }
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
                DataTable dt = new DataTable();
                gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                if (chkCode.Checked)
                {
                    dt = imbl.SelectAllByStatus(ime, 1, gvItem.PageSize, 2, 1);//(searchkey,pageIndex,pagesize,option(2=equal,1=like),1=search,0=select all)
                }
                else
                {
                    dt = imbl.SelectAllByStatus(ime, 1, gvItem.PageSize, 1, 1);
                }
                int count = 0;
                if (dt.Rows.Count > 0)
                    count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                gvItem.DataSource = dt;
                gvItem.DataBind();
                gp.CalculatePaging(count, gvItem.PageSize, 1);
                ViewState["SearchDataID"] = null;
                ViewState["SearchDataID"] = dt;
                ViewState.Remove("checkedValue"); // After various search and check, clean previous check value.
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
                    HtmlGenericControl PExhibit1 = e.Row.FindControl("PExhibit") as HtmlGenericControl;
                    HtmlGenericControl POkSt = e.Row.FindControl("POkSt") as HtmlGenericControl;

                    switch (lbl.Text)
                    {
                        case "1":
                            {
                                Ppage.Visible = true;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = false;
                                POkSt.Visible = false;
                                PExhibit1.Visible = false;
                                break;
                            }
                        case "3":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = true;
                                PWaitL.Visible = false;
                                POkSt.Visible = false;
                                PExhibit1.Visible = false;
                                break;
                            }
                        case "2":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = true;
                                POkSt.Visible = false;
                                PExhibit1.Visible = false;
                                break;
                            }
                        case "4":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = false;
                                POkSt.Visible = true;
                                PExhibit1.Visible = false;
                                break;
                            }
                        case "5":
                            {
                                Ppage.Visible = false;
                                PWaitSt.Visible = false;
                                PWaitL.Visible = false;
                                POkSt.Visible = false;
                                PExhibit1.Visible = true;
                                break;
                            }
                    }
                    lbl = e.Row.FindControl("lblshop") as Label;
                    HtmlGenericControl pWait = e.Row.FindControl("PWait") as HtmlGenericControl;
                    HtmlGenericControl pOk = e.Row.FindControl("POk") as HtmlGenericControl;
                    HtmlGenericControl pDel = e.Row.FindControl("PDel") as HtmlGenericControl;
                    HtmlGenericControl pInactive = e.Row.FindControl("PInactive") as HtmlGenericControl;
                    switch (lbl.Text)
                    {
                        case "n":
                            {
                                pWait.Visible = true;
                                pOk.Visible = false;
                                pDel.Visible = false;
                                pInactive.Visible = false;
                                break;
                            }
                        case "u":
                            {
                                pWait.Visible = false;
                                pOk.Visible = true;
                                pDel.Visible = false;
                                pInactive.Visible = false;
                                break;
                            }
                        case "d":
                            {
                                pWait.Visible = false;
                                pOk.Visible = false;
                                pDel.Visible = true;
                                pInactive.Visible = false;
                                break;
                            }
                        case "g":
                            {
                                pWait.Visible = false;
                                pOk.Visible = false;
                                pDel.Visible = false;
                                pInactive.Visible = true;
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

        protected void btn_ChangeStatus_Click(object sender, EventArgs e)
        {
            try
            {
                string itemIDList = null;
                string itemCodeList = null;
                int rdostatus=0;
                imbl = new Item_Master_BL();
                ArrayList CheckBoxArray;
                if (ViewState["checkedValue"] != null)
                {
                    CheckBoxArray = (ArrayList)ViewState["checkedValue"];
                    for (int i = 0; i < CheckBoxArray.Count; i++)
                    {
                          itemIDList += CheckBoxArray[i] + ",";
                    }
                    ViewState.Remove("checkedValue");
                }
                if (!string.IsNullOrWhiteSpace(itemIDList))
                {
                    //Remove last comma from string
                    itemIDList = itemIDList.TrimEnd(','); // (OR) itemIDList.Remove(str.Length-1); 
                    Session.Remove("ItemIDList");
                    Session["ItemIDList"] = itemIDList;
                    DataTable Item_Code = new DataTable();
                    Item_Code = imbl.SelectForExportStatusChange(itemIDList);
                    foreach (DataRow itemCode in Item_Code.Rows)
                    {
                        itemCodeList += itemCode["Item_Code"].ToString() + ",";
                    }
                    if (rdoSKSStauts.Checked)
                    { rdostatus = 1; }
                    else
                    { rdostatus = 2; }
                    ConsoleWriteLine_Tofile("Change Shop Status and Export Status From Item_Status_Change");
                    ConsoleWriteLine_Tofile("Item Code:" + itemCodeList);
                    ConsoleWriteLine_Tofile("Process Finish : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); //Process Finish
                    string url = "../Item/Status_Change_Confirmation.aspx?IDlist=" + 1+"&&rdoStatus="+ rdostatus;
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
            if (rdoShopStatus.Checked == true || rdoSKSStauts.Checked == true)
            {
                Bind();
            }
            else
            {
                Session.Remove("ItemIDList");
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
                DropDownList ddl = gvItem.Rows[index].FindControl("ddlShop") as DropDownList;
                String shopID = ddl.SelectedValue.ToString();
                String mallid = "0";
                Item_View3_BL iv3bl = new Item_View3_BL();
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
                }
                else if (mallid.Equals("2"))
                {
                    yahoo += shopName + "/" + itemCode + ".html";
                    Response.Redirect(yahoo, false);
                }
                else if (mallid.Equals("3"))
                {
                    ponpare += shopName + "/goods/" + itemCode + "/";
                    Response.Redirect(ponpare, false);
                }
                else if (mallid.Equals("5"))
                {
                    jisha += itemCode;
                    Response.Redirect(jisha, false);
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

        protected void rdoSKS_OnCheckChange(object sender, EventArgs e)
        {
            ddlExport_Status.Visible = true;
            ddlExport_Status.Enabled = false;
            ddlCtrl_ID.Visible = false;
        }

        protected void rdoShop_OnCheckChange(object sender, EventArgs e)
        {
            ddlExport_Status.Visible = false;
            ddlCtrl_ID.Visible = true;
            ddlCtrl_ID.Enabled = false;
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
                imbl = new Item_Master_BL();
                return imbl.IsPost_Available_Date(Item_ID);
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
                imbl = new Item_Master_BL();
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