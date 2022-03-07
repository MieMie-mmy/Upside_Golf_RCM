using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;
using System.Collections;
using System.Drawing;

namespace ORS_RCM.WebForms.Item
{
    public partial class Item_View_Quick : System.Web.UI.Page
    {
        Item_Master_BL ItemMasterBL;
        Item_Shop_BL ItemShopBL;

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
                    Bind();
                    ItemCheck_Change();
                }
                else
                {
                    // DataTable dt = Search();
                    DataTable dt;
                    if (ViewState["ItemAll"] == null)
                    {
                        dt = Search(1, 1);
                        ViewState["ItemAll"] = dt;
                    }
                    else
                    {
                        dt = ViewState["ItemAll"] as DataTable;
                    }

                    //gp.TotalRecord = dt.Rows.Count;
                    gp.TotalRecord = Search(1, 2).Rows.Count;
                    gp.OnePageRecord = gvItem.PageSize;

                    gp.sendIndexToThePage += delegate(int index)
                    {
                        gvItem.PageSize = gp.OnePageRecord;
                        gvItem.PageIndex = Convert.ToInt32(index);
                        //gvItem.DataSource = GetTable(dt, index);
                        gvItem.DataSource = Search(index, 1);
                        gvItem.DataBind();

                        //for maintain checkbox value
                        ViewState["ItemAll"] = dt;

                        if (ViewState["checkedValue"] != null)
                        {
                            //if all checkbox is checked--> header checkbox set to check
                            CheckBox chk = gvItem.HeaderRow.FindControl("chkall") as CheckBox;
                            ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                            if (IsAllCheck(arrlst))
                                chk.Checked = true;
                            else chk.Checked = false;
                        }

                        //get checked value
                        ItemCheck_Change();
                    };


                }
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
                ItemMasterBL = new Item_Master_BL();

                DataTable dt = new DataTable();

                if (ViewState["ItemAll"] == null)
                {
                    dt = Search(1, 1);
                    ViewState["ItemAll"] = dt;
                }
                else
                {
                    dt = ViewState["ItemAll"] as DataTable;
                }
                //gp.TotalRecord = dt.Rows.Count;
                gp.TotalRecord = Search(1, 2).Rows.Count;
                gp.OnePageRecord = gvItem.PageSize;

                int index1 = 0;
                gp.sendIndexToThePage += delegate(int index)
                {
                    index1 = index;
                };
                gvItem.PageIndex = index1;
                //gvItem.DataSource = GetTable(dt,index1);            
                gvItem.DataSource = Search(index1, 1);
                gvItem.DataBind();
                ViewState["ItemAll"] = dt;

                if (ViewState["checkedValue"] != null)
                {
                    CheckBox chk = gvItem.HeaderRow.FindControl("chkall") as CheckBox;
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    if (IsAllCheck(arrlst))
                        chk.Checked = true;
                    else chk.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public DataTable Search(int pageIndex, int option)
        {
            try
            {
                pageIndex = pageIndex + 1;
                ItemMasterBL = new Item_Master_BL();
                DataTable dt = ItemMasterBL.SearchForViewQuick(txtItem_Name.Text, txtItem_Code.Text, pageIndex, int.Parse(ddlpage.SelectedValue.ToString()), option);
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

        public DataTable SelectAll()
        {
            try
            {
                ItemMasterBL = new Item_Master_BL();
                DataTable dt = ItemMasterBL.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            }
        }

        protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DataEdit")
                {
                    string Item_Code = e.CommandArgument.ToString();
                    Response.Redirect("Item_Master.aspx?Item_Code=" + Item_Code,false);
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

                    //if (!String.IsNullOrWhiteSpace(LabelItem_Code.Text))
                    //{
                    //    Button btn = e.Row.FindControl("btnSKU") as Button;
                    //    btn.Attributes.Add("onclick", "javascript:Show('" + LabelItem_Code.Text + "'," + btn.ClientID + ")");
                    //}

                    Label lbl = e.Row.FindControl("lblSKUStatus") as Label;
                    lbl.ForeColor = Color.White;
                    lbl.Width = 20;

                    switch (lbl.Text)
                    {
                        case "1":
                            {
                                lbl.Text = "制";
                                lbl.Style.Add("background-color", "#ff3399");
                                lbl.Style.Add("text-align", "center");
                                break;
                            }
                        case "3":
                            {
                                lbl.Style.Add("background-color", "red");
                                lbl.Style.Add("text-align", "center");
                                lbl.Text = "末";
                                break;
                            }
                        case "2":
                            {
                                lbl.Style.Add("background-color", "yellow");
                                lbl.Style.Add("text-align", "center");
                                lbl.Text = "期";
                                break;
                            }
                        case "4":
                            {
                                lbl.Style.Add("background-color", "blue");
                                lbl.Style.Add("text-align", "center");
                                lbl.Text = "掲";
                                break;
                            }
                    }

                    lbl = e.Row.FindControl("lblshop") as Label;
                    lbl.ForeColor = Color.White;
                    lbl.Width = 20;

                    switch (lbl.Text)
                    {
                        case "n":
                            {
                                lbl.Text = "末";
                                lbl.Style.Add("text-align", "center");
                                lbl.Style.Add("background-color", "red");
                                break;
                            }
                        case "u":
                            {
                                lbl.Text = "掲";
                                lbl.Style.Add("text-align", "center");
                                lbl.Style.Add("background-color", "blue");
                                break;
                            }
                        case "d":
                            {
                                lbl.Text = "削";
                                lbl.Style.Add("text-align", "center");
                                lbl.Style.Add("background-color", "black");
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

                ArrayList al = ViewState["checkedValue"] as ArrayList;
                //hfNewTab.Text = String.Empty;
                if (al.Count > 0)
                {
                    for (int i = 0; i < al.Count; i++)
                    {
                        DataTable dtItemAll = ViewState["ItemAll"] as DataTable;
                        DataRow[] dr = dtItemAll.Select("ID='" + al[i] + "'");
                        if (dr.Count() > 0)
                        {
                            dtItemAll = dtItemAll.Select("ID='" + al[i] + "'").CopyToDataTable();
                            int rowno = Convert.ToInt32(dtItemAll.Rows[0]["No"].ToString());
                            if (rowno % Convert.ToInt32(ddlpage.SelectedValue) == 0)
                                rowno = Convert.ToInt32(ddlpage.SelectedValue);
                            else rowno = rowno % Convert.ToInt32(ddlpage.SelectedValue);
                            //DropDownList ddl = gvItem.Rows[rowno - 1].FindControl("ddlshop") as DropDownList;
                            //if (ddl.Items.Count <= 1)
                            //{
                            //    //hfNewTab.Text = "1";
                            //    break;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
            
            //else hfNewTab.Text = "1";//allow new tab
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
            //hfNewTab.Text = "1";
        }

        protected void btnexhibition_Click(object sender, EventArgs e)
        {
            try
            {
                string itemIDList = null;
                if (gvItem.HeaderRow != null)
                {
                    CheckBox chkAll = (CheckBox)gvItem.HeaderRow.FindControl("chkAll");
                    //string checkAllIndex = "chkAll";
                    if (chkAll.Checked)
                    {
                        DataTable dt = (DataTable)ViewState["ItemAll"];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (IsSelectedShop(Convert.ToInt32(dt.Rows[i]["ID"].ToString())))   //Check Choice or not Shop
                            {
                                itemIDList += dt.Rows[i]["ID"].ToString() + ",";
                            }
                            else
                            {
                                ItemMasterBL = new Item_Master_BL();
                                string Item_Code = ItemMasterBL.SelectByItemCode(Convert.ToInt32(dt.Rows[i]["ID"].ToString()));
                                MessageBox(Item_Code + " does not choice Shop.");
                                itemIDList = null;
                                break;
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
                                    ItemMasterBL = new Item_Master_BL();
                                    string Item_Code = ItemMasterBL.SelectByItemCode(Convert.ToInt32(CheckBoxArray[i].ToString()));
                                    MessageBox(Item_Code + " does not choice Shop.");
                                    itemIDList = null;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(itemIDList))
                {
                    //Remove last comma from string
                    itemIDList = itemIDList.TrimEnd(','); // (OR) itemIDList.Remove(str.Length-1); 
                    if (User_ID != 0)
                    {
                        //Export_CSV eCSV = new Export_CSV(itemIDList, User_ID);
                        Session["list"] = itemIDList;
                        string url = "../Item_Exhibition/Exhibition_Confirmation.aspx?list=" + itemIDList;
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.location('../Item_Exhibition/Exhibition_List.aspx?list=" + itemIDList + "','_new');", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var win = window.open('" + url + "','_newtab'); self.focus();", true);
                        //Response.Redirect("../Item_Exhibition/Exhibition_List.aspx?list=" + itemIDList);
                        Response.Redirect(url,false);
                        Bind();//Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        MessageBox("Please you should start Login Page!");
                    }
                    //Export_CSV eCSV = new Export_CSV(itemIDList);
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
                if (gvItem.HeaderRow != null)
                {
                    CheckBox chkAll = (CheckBox)gvItem.HeaderRow.FindControl("chkAll");
                    //string checkAllIndex = "chkAll";
                    if (chkAll.Checked)
                    {
                        DataTable dt = (DataTable)ViewState["ItemAll"];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            itemIDList += dt.Rows[i]["ID"].ToString() + ",";
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
                                itemIDList += CheckBoxArray[i] + ",";
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(itemIDList))
                {
                    //Remove last comma from string
                    itemIDList = itemIDList.TrimEnd(','); // (OR) itemIDList.Remove(str.Length-1); 
                    if (User_ID != 0)
                    {
                        //Export_CSV eCSV = new Export_CSV(itemIDList, User_ID);
                        Session["list"] = itemIDList;
                        string url = "../Item/Item_View3.aspx?list=" + itemIDList;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var win = window.open('" + url + "','_newtab'); self.focus();", true);
                        //Response.Redirect(url);
                        Bind();//Response.Redirect(Request.RawUrl);
                        //Response.Redirect("../Item_Exhibition/Exhibition_List.aspx?list=" + itemIDList);

                    }
                    else
                    {
                        MessageBox("Please you should start Login Page!");
                    }
                    //Export_CSV eCSV = new Export_CSV(itemIDList);
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
                gvItem.DataSource = Search(1, 1);
                gvItem.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
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


                ArrayList al = ViewState["checkedValue"] as ArrayList;
                ////hfNewTab.Text = String.Empty;
                if (al.Count > 0)
                {
                    for (int i = 0; i < al.Count; i++)
                    {
                        DataTable dtItemAll = ViewState["ItemAll"] as DataTable;
                        DataRow[] dr = dtItemAll.Select("No='" + al[i] + "'");
                        if (dr.Count() > 0)
                        {
                            int rowno = Convert.ToInt32(dr[0]["No"].ToString());
                            //DropDownList ddl = gvItem.Rows[rowno - 1].FindControl("ddlShop") as DropDownList;
                            //if (ddl.Items.Count <= 1)

                            //{
                            //    hfNewTab.Text = "1";
                            //    break;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
            //else hfNewTab.Text = "1";//not allow new tab
        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvItem.PageSize = Convert.ToInt32(ddlpage.SelectedValue);

                gvItem.DataSource = Search(0, 1);
                gvItem.DataBind();
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

        private DataTable GetTable(DataTable dt, int index)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    DataTable dtTmp = dt.Clone();
                    int rowCount = (index + 1) * 30;
                    int skipRow = rowCount - (rowCount - 30);
                    if (index == 0)
                    {
                        return dt.Rows.Cast<System.Data.DataRow>().Take(30).CopyToDataTable();
                    }
                    else
                    {
                        return dt.Rows.Cast<System.Data.DataRow>().Skip(skipRow).Take(rowCount).CopyToDataTable();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return null;
            }
        }

        public void MessageBox(string message)
        {
            //if (message == "Saving Successful ! " || message == "Updating Successful ! ")
            //{
            //    Session["CatagoryList"] = null;
            //    object referrer = ViewState["UrlReferrer"];
            //    string url = (string)referrer;
            //    string script = "window.onload = function(){ alert('";
            //    script += message;
            //    script += "');";
            //    script += "window.location = '";
            //    script += url;
            //    script += "'; }";
            //    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            //}
            //else
            //{
            try
            {
                string cleanMessage = message.Replace("'", "\\'");
                string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
                Page page = HttpContext.Current.CurrentHandler as Page;
                if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                {
                    page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
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