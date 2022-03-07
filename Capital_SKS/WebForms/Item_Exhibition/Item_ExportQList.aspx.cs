using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;
using System.Globalization;
using System.Collections;
using ORS_RCM.WebForms.Item;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace ORS_RCM
{
    public partial class Item_ExportQList : System.Web.UI.Page
    {
        Item_ExportQList_BL ibl;
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
        String checkdateitem = String.Empty; int count = 0;
        string imagePath = ConfigurationManager.AppSettings["ItemImage"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ItemCheck_Change("check");
                    ibl = new Item_ExportQList_BL();
                    ArrayList arrchk = new ArrayList();
                    ViewState["list"] = arrchk;
                    if (!String.IsNullOrWhiteSpace(txtbrand.Text.Trim()) || !String.IsNullOrWhiteSpace(txtcatno.Text.Trim()) || !String.IsNullOrWhiteSpace(txtitemno.Text.Trim()) || !String.IsNullOrWhiteSpace(txtproductname.Text.Trim()) || !String.IsNullOrWhiteSpace(txtpavadate.Text.Trim()) || !String.IsNullOrWhiteSpace(txtpdateend.Text.Trim()) || !String.IsNullOrWhiteSpace(txtsupplier.Text.Trim()))
                    {
                        DataTable dt = Search(txtbrand.Text.Trim(), txtcatno.Text.Trim(), txtitemno.Text.Trim(), txtproductname.Text.Trim(), Request.Form[txtpavadate.UniqueID], Request.Form[txtpdateend.UniqueID], txtsupplier.Text.Trim());
                    }
                    else
                    {
                        gvpageindex();
                        DataTable dt = ibl.SelectAllData();
                        gvexpQ.DataSource = dt;
                        ViewState["dt"] = dt;
                        gvexpQ.DataBind();
                        gvexpQ.Columns[8].Visible = false;
                        gvexpQ.Columns[9].Visible = false;
                        gp.CalculatePaging(dt.Rows.Count, gvexpQ.PageSize, 1);
                    }
                }
                else
                {
                    String ctrl = getPostBackControlName();
                    if (ctrl.Contains("lnkPaging"))
                    {
                        ibl = new Item_ExportQList_BL();
                        DataTable dt = new DataTable();
                        if (!String.IsNullOrWhiteSpace(txtbrand.Text.Trim()) || !String.IsNullOrWhiteSpace(txtcatno.Text.Trim()) || !String.IsNullOrWhiteSpace(txtitemno.Text.Trim()) || !String.IsNullOrWhiteSpace(txtproductname.Text.Trim()) || !String.IsNullOrWhiteSpace(txtpavadate.Text.Trim()) || !String.IsNullOrWhiteSpace(txtpdateend.Text.Trim()) || !String.IsNullOrWhiteSpace(txtsupplier.Text.Trim()))
                        {
                            dt = Search(txtbrand.Text.Trim(), txtcatno.Text.Trim(), txtitemno.Text.Trim(), txtproductname.Text.Trim(), Request.Form[txtpavadate.UniqueID], Request.Form[txtpdateend.UniqueID], txtsupplier.Text.Trim());
                        }
                        else
                        {
                            dt = ibl.SelectAllData();
                        }
                        gp.LinkButtonClick(ctrl, gvexpQ.PageSize);
                        Label lbl = gp.FindControl("lblCurrent") as Label;
                        int index = Convert.ToInt32(lbl.Text);
                        gvexpQ.PageIndex = Convert.ToInt32(index - 1);
                        gvexpQ.DataSource = dt;
                        gvexpQ.DataBind();
                        ItemCheck_Change("check");
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
                ibl = new Item_ExportQList_BL();
                DataTable dt = ibl.SelectAllData();
                gp.TotalRecord = dt.Rows.Count;
                gp.OnePageRecord = gvexpQ.PageSize;
                int index1 = 0;
                gp.sendIndexToThePage += delegate(int index)
                {
                    index1 = index;
                };
                gvexpQ.PageIndex = index1;
                gvexpQ.DataSource = dt;
                gvexpQ.DataBind();
                ViewState["ItemAll"] = dt;
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
                DataTable dt = new DataTable();
                if (!String.IsNullOrWhiteSpace(txtbrand.Text.Trim()) || !String.IsNullOrWhiteSpace(txtcatno.Text.Trim()) || !String.IsNullOrWhiteSpace(txtitemno.Text.Trim()) || !String.IsNullOrWhiteSpace(txtproductname.Text.Trim()) || !String.IsNullOrWhiteSpace(Request.Form[txtpavadate.UniqueID]) || !String.IsNullOrWhiteSpace(Request.Form[txtpdateend.UniqueID])|| !String.IsNullOrWhiteSpace(txtsupplier.Text.Trim()))
                {
                    dt = Search(txtbrand.Text.Trim(), txtcatno.Text.Trim(), txtitemno.Text.Trim(), txtproductname.Text.Trim(), Request.Form[txtpavadate.UniqueID], Request.Form[txtpdateend.UniqueID],txtsupplier.Text.Trim());
                }
                else 
                {
                    ibl = new Item_ExportQList_BL();
                    dt = ibl.SelectAllData();
                }
                gvexpQ.DataSource = dt;
                gvexpQ.DataBind();
                gp.CalculatePaging(dt.Rows.Count, gvexpQ.PageSize, 1);
                ViewState["dt"] = dt;
                UPanel.Update();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        private DataTable Search(string brand, string cat, string itno, string itname, string fromDate, string toDate, string supplier)
        {
            try
            {
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
                ibl = new Item_ExportQList_BL();
                DataTable dt = ibl.SelectAll(brand, cat, itno, itname, FromDate, ToDate, supplier);
                txtpavadate.Text = hdfFromDate.Value;
                txtpdateend.Text = hdfToDate.Value;
                hdfFromDate.Value = String.Empty;
                hdfToDate.Value = String.Empty;
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

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtpavadate.Text = String.Empty;
                hdfFromDate.Value = Request.Form[txtpavadate.UniqueID];
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
                txtpdateend.Text = String.Empty;
                hdfToDate.Value = Request.Form[txtpdateend.UniqueID];
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnexhibition_Click(object sender, EventArgs e)
        {
            string lists = "1"; DateTime sdate; DateTime currentdate;
            if (ViewState["checkedValue"] != null)
            {
                ArrayList chk = ViewState["checkedValue"] as ArrayList;
                String cklist = String.Join(",", (string[])chk.ToArray(Type.GetType("System.String")));
                if (User_ID != 0)
                {
                    for (int y = 0; y < gvexpQ.Rows.Count; y++)
                    {
                        CheckBox chks = gvexpQ.Rows[y].FindControl("ckItem") as CheckBox;
                        if (chks.Checked == true)
                        {
                            Label lblcode = gvexpQ.Rows[y].FindControl("Label9") as Label;
                            Label lbl = gvexpQ.Rows[y].FindControl("Label8") as Label;
                            string strdate = lbl.Text.ToString();
                            if (!string.IsNullOrWhiteSpace(strdate))
                            {
                                sdate = Convert.ToDateTime(strdate);
                                currentdate = System.DateTime.Now;
                                if (sdate > currentdate) { checkdateitem += lblcode.Text + ","; }
                            }
                            else { checkdateitem += lblcode.Text + ","; }
                        }
                    }
                    if (String.IsNullOrWhiteSpace(checkdateitem))
                    {
                        Session["ItemIDList"] = cklist;
                        string url = "../Item_Exhibition/Exhibition_Confirmation.aspx?IDlist=" + lists;
                        Response.Redirect(url);
                        gvpageindex();
                    }
                    else { GlobalUI.MessageBox(checkdateitem + "" + " can't export because of  掲載可能日 greater than current date time! "); }
                }
            }
            UPanel.Update();
        }

        protected void gvexpQ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DataEdit")
            {
                string Item_Code = e.CommandArgument.ToString();
                Response.Redirect("../Item/Item_Master.aspx?Item_Code=" + Item_Code);
            }
        }

        protected void btnselectall_Click(object sender, EventArgs e)
        {
            try
            {
                CheckChangeGridpage(true);
                ItemCheck_Change("check");
                ArrayList al = ViewState["checkedValue"] as ArrayList;
                if (al.Count > 0)
                {
                    hfNewTab.Text = String.Empty;
                }
                else hfNewTab.Text = "1";//not allow new tab
                UPanel.Update();
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
                CheckChangeGridpage(false);
                ItemCheck_Change("uncheck");
                hfNewTab.Text = "1";
                UPanel.Update();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ItemCheck_Change(string type)
        {
            try
            {
                if (type == "check")
                {
                    if (ViewState["checkedValue"] != null)
                    {
                        ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                        for (int i = 0; i < gvexpQ.Rows.Count; i++)
                        {
                            CheckBox chk = gvexpQ.Rows[i].FindControl("ckItem") as CheckBox;
                            Label lbl = gvexpQ.Rows[i].FindControl("lblID") as Label;
                            if (arrlst.Contains(lbl.Text))
                                chk.Checked = true;
                            else chk.Checked = false;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < gvexpQ.Rows.Count; i++)
                    {
                        CheckBox chk = gvexpQ.Rows[i].FindControl("ckItem") as CheckBox;
                        Label lbl = gvexpQ.Rows[i].FindControl("lblID") as Label;
                        chk.Checked = false;
                    }
                    ViewState["checkedValue"] = null;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void CheckChangeGridpage(Boolean check)
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
                    for (int i = 0; i < gvexpQ.Rows.Count; i++)
                    {
                        Label lbl = gvexpQ.Rows[i].FindControl("lblID") as Label;
                        CheckBox ckb = gvexpQ.Rows[i].FindControl("ckItem") as CheckBox;
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

        protected void CheckChangeGrid(Boolean check)
        {
            try
            {
                ArrayList arrCheckValue = new ArrayList();
                if (check == true)
                {
                    if (ViewState["ItemAll"] != null)
                    {
                        DataTable dtItemAll = ViewState["ItemAll"] as DataTable;
                        for (int i = 0; i < dtItemAll.Rows.Count; i++)
                        {
                            arrCheckValue.Add(dtItemAll.Rows[i]["ID"].ToString());
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

        protected void gvexpQ_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = e.Row.FindControl("lblSKUStatus") as Label;
                    HtmlGenericControl PWaitL = e.Row.FindControl("PWaitL") as HtmlGenericControl;
                    switch (lbl.Text)
                    {
                        case "2":
                            {
                                PWaitL.Visible = true;
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
                    Label l1 = e.Row.FindControl("lblID") as Label;
                    if (!String.IsNullOrWhiteSpace(l1.Text))
                    {
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
                    }
                }
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
                ItemCheck_Change("check");
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
                Label lbl = gvexpQ.Rows[rowIndex].FindControl("lblID") as Label;
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    //  CheckBox chkHeader = gvexpQ.HeaderRow.FindControl("ckall") as CheckBox;
                    if (!chk.Checked)
                    {
                        //if one of check box is unchecked then header checkbox set to uncheck
                        // chkHeader.Checked = false;
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
                    }
                }
                else
                {
                    ArrayList arrlst = new ArrayList();
                    arrlst.Add(lbl.Text);
                    ViewState["checkedValue"] = arrlst;
                }
                ArrayList al = ViewState["checkedValue"] as ArrayList;
                if (al.Count > 0)
                {
                    hfNewTab.Text = String.Empty;
                }
                else hfNewTab.Text = "1";//allow new tab
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

        protected void ItemCheck_Change1()
        {
            try
            {
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    CheckBox chk;
                    Label lbl;
                    for (int i = 0; i < gvexpQ.Rows.Count; i++)
                    {
                        chk = gvexpQ.Rows[i].FindControl("ckItem") as CheckBox;
                        lbl = gvexpQ.Rows[i].FindControl("lblID") as Label;
                        if (arrlst.Contains(lbl.Text))
                            if (count <= 10)
                            {
                                chk.Checked = true; count++;
                            }
                            else chk.Checked = false;
                    }
                    count = 0;
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