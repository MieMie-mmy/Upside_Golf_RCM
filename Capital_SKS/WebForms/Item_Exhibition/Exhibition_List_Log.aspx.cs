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
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Excel;
using System.Diagnostics;
using Ionic.Zip;
using ClosedXML.Excel;
using ORS_RCM;

namespace ORS_RCM.WebForms.Item_Exhibition
{
    public partial class Exhibition_List_Log : System.Web.UI.Page
    {
        Exhibition_List_BL ehbbl = new Exhibition_List_BL();
        Item_ExportField_BL itfield_bl = new Item_ExportField_BL();
        Item_Information_BL iteminfo_bl= new Item_Information_BL();
        GlobalBL gb;

        public string list; public string csv;
        DateTime? FromDate = null;
        DateTime? ToDate = null; int shid;
        string url = ConfigurationManager.AppSettings["rakuten"].ToString();
        string yurl = ConfigurationManager.AppSettings["yahoo"].ToString();
        string wurl = ConfigurationManager.AppSettings["wowma"].ToString();
        string jurl = ConfigurationManager.AppSettings["jisha"].ToString();
        string turl = ConfigurationManager.AppSettings["tennis"].ToString();
        string UploadPath = ConfigurationManager.AppSettings["UploadPath"].ToString();

        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        string FilePath = ConfigurationManager.AppSettings["ExportFieldCSVPath"].ToString();
        static string menulist = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtcode.Attributes.Add("onKeyPress", "doClick('" + btnsearch.ClientID + "',event)");
                // this.Form.DefaultFocus = txtcode.ClientID;
                Page.Form.DefaultButton = btnsearch.UniqueID;
                if (!IsPostBack)
                {
                    itfield_bl = new Item_ExportField_BL();
                    ehbbl = new Exhibition_List_BL();
                    gb = new GlobalBL();
                    if (Request.QueryString["list"] != null)
                    {
                        list = Session["list"].ToString();
                        ddlexhibitioncheck.SelectedValue = null;
                    }
                    else
                    {
                        list = string.Empty;
                        Session.Remove("list");
                    }

                    ddlmall.DataTextField = "Code_Description";
                    ddlmall.DataValueField = "ID";
                    ddlmall.DataSource = gb.Code_Setup(1);
                    ddlmall.DataBind();
                    ddlmall.Items.Insert(0, new ListItem("ショップ選択", ""));

                    DataTable dtex = itfield_bl.SelectUser();

                    ddlexhibitor.DataTextField = "User_Name";
                    ddlexhibitor.DataValueField = "ID";
                    ddlexhibitor.DataSource = dtex;
                    ddlexhibitor.DataBind();
                    ddlexhibitor.Items.Insert(0, "");

                    itfield_bl = new Item_ExportField_BL();
                    ehbbl = new Exhibition_List_BL();
                    gb = new GlobalBL();

                    Bind();
                }
                else
                {
                    String ctrl = getPostBackControlName();
                    if (ctrl.Contains("lnkPaging"))
                    {
                        Forpaging(ctrl);
                    }
                    //for pageno
                    else if (ctrl.Contains("lnkpageno"))
                    {
                        Forpaging(ctrl);
                    }

                }
            }//main
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void Forpaging(string ctrl)
        {
            if (Request.QueryString["list"] != null)
            {
                list = Session["list"].ToString();
            }
            else
            {
                list = string.Empty;
                Session.Remove("list");
            }
            gvexhibition.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
            gp.LinkButtonClick(ctrl, gvexhibition.PageSize);
            Label lbl = gp.FindControl("lblCurrent") as Label;
            int index = Convert.ToInt32(lbl.Text);
            gvexhibition.PageIndex = index;
            Exhibition_Entity ee = GetSearchData();
            int option = 1;//like
            if (chkitemcode.Checked)
                option = 0;//equal
            int chkerror = 0;
            //if (chkErrSearch.Checked)
            //    chkerror = 1;
            int chknotcheck = 0;
            //if (chkSearch.Checked)
            //    chknotcheck = 1;
            int chkrecovery = 0;
            //if (chkReExhibit.Checked)
            //    chkrecovery = 1;
            //DataTable dt = ehbbl.Exhibition_Search(ee, list, option, index, chkerror, chknotcheck, chkrecovery, gvexhibition.PageSize);
            DataTable dt = ehbbl.Exhibition_Search(ee, list, option, index, gvexhibition.PageSize);
            gvexhibition.DataSource = dt;
            gvexhibition.DataBind();

        }

        public Exhibition_Entity GetSearchData()
        {
            Exhibition_Entity ee = new Exhibition_Entity();
            string itemcode = string.Empty;
            ee.Item_Code = txtcode.Text.Trim();
            string replaceWith = ",";
            itemcode = txtcode.Text.Trim();
            ee.Item_Code = itemcode.Replace(",\r\n", replaceWith).Replace(",\n", replaceWith).Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
            if (ee.Item_Code.EndsWith(","))
                ee.Item_Code = ee.Item_Code.TrimEnd(',');

            ee.Item_Name = txtproname.Text.Trim();
            ee.CatalogInfo = txtcatinfo.Text.Trim();
            ee.BrandName = txtbrandname.Text.Trim();
            ee.CompetitionName = txtcompetitionname.Text.Trim();
            ee.CompanyName = txtcompname.Text.Trim();
            ee.ClassName = txtclassname.Text.Trim();
            ee.Year = txtyear.Text.Trim();

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
            ee.StartDate = FromDate;
            ee.EndDate = ToDate;
            ee.Season = txtseason.Text.Trim();
            if (!String.IsNullOrWhiteSpace(ddlexhibitor.SelectedValue))
                ee.ExhibitedUser = Convert.ToInt32(ddlexhibitor.SelectedValue);
            else
                ee.ExhibitedUser = -1;

            ee.Mall = ddlmall.SelectedItem.Text;

            ee.Remark = txtremark.Text;
            if (!String.IsNullOrWhiteSpace(ddlexbresulterror.SelectedValue))
                ee.ErrorCheck = Convert.ToInt32(ddlexbresulterror.SelectedValue);
            else
                ee.ErrorCheck = -1;
            if (!String.IsNullOrWhiteSpace(ddlAPIcheck.SelectedValue))
            {
                ee.ApiCheck = Convert.ToInt32(ddlAPIcheck.SelectedValue);
            }
            else
                ee.ApiCheck = -1;
            if (!String.IsNullOrWhiteSpace(ddlexhibitioncheck.SelectedValue))
                ee.ExhibitionCheck = Convert.ToInt32(ddlexhibitioncheck.SelectedValue);
            else
                ee.ExhibitionCheck = -1;
            ee.Instructionno = txtinstructionno.Text.Trim();
            return ee;
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

        protected void gvexhibition_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            ehbbl = new Exhibition_List_BL();
            Log_Exhibition_BL Log = new Log_Exhibition_BL();
            #region Detail
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
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Mall_ID"].ToString() == "1" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                            shid = (int)dt.Rows[i]["ID"];
                    }
                    Response.Redirect("Details_of_exhibition_Rakuten.aspx?Shop_ID=" + shid + "&Item_ID=" + Item_ID);
                }
                else if (Convert.ToString(Mallid) == "2")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblEID");
                    int Item_ID = Convert.ToInt32(lblID.Text);
                    Label lblshopname = (Label)clickedRow.FindControl("Label3");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Mall_ID"].ToString() == "2" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                            shid = (int)dt.Rows[i]["ID"];
                    }

                    Response.Redirect("Details_of_exhibition_Yahoo.aspx?Shop_ID=" + shid + "&Item_ID=" + Item_ID);
                }
                else if (Convert.ToString(Mallid) == "4")
                {

                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblEID");
                    int Item_ID = Convert.ToInt32(lblID.Text);
                    Label lblshopname = (Label)clickedRow.FindControl("Label4");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Mall_ID"].ToString() == "4" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                            shid = (int)dt.Rows[i]["ID"];
                    }
                    
                    Response.Redirect("Details_of_exhibition_Wowma.aspx?Shop_ID=" + shid + "&Item_ID=" + Item_ID);
                }

                //hhw2021-12-15
                //else if (Convert.ToString(Mallid) == "5")
                //{
                //    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                //    Label lblID = (Label)clickedRow.FindControl("lblEID");
                //    int Item_ID = Convert.ToInt32(lblID.Text);
                //    Label lblshopname = (Label)clickedRow.FindControl("Label5");
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        if (dt.Rows[i]["Mall_ID"].ToString() == "5" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                //            shid = (int)dt.Rows[i]["ID"];
                //    }
                //    Response.Redirect("Details_of_exhibition_Amazone.aspx?Item_ID=" + Item_ID + "&Shop_ID=" + shid);
                //}
                //else if (Convert.ToString(Mallid) == "6")
                //{
                //    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                //    Label lblID = (Label)clickedRow.FindControl("lblEID");
                //    int Item_ID = Convert.ToInt32(lblID.Text);
                //    Label lblshopname = (Label)clickedRow.FindControl("Label7");
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        if (dt.Rows[i]["Mall_ID"].ToString() == "6" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                //            shid = (int)dt.Rows[i]["ID"];
                //    }
                //    Response.Redirect("Details_of_exhibition_jisha.aspx?Shop_ID=" + shid + "&Item_ID=" + Item_ID);
                //}

                else if (Convert.ToString(Mallid) == "7")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblEID");
                    int Item_ID = Convert.ToInt32(lblID.Text);
                    Label lblshopname = (Label)clickedRow.FindControl("Label8");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Mall_ID"].ToString() == "7" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                            shid = (int)dt.Rows[i]["ID"];
                    }
                    Response.Redirect("Details_of_exhibition_Tennis.aspx?Shop_ID=" + shid + "&Item_ID=" + Item_ID);
                }
                //Details_of_exhibition_jisha.aspx
            }
            #endregion
            #region Export
            else if (e.CommandName == "Export")
            {
                int Mallid = Convert.ToInt32(e.CommandArgument);
                DataTable dt = ehbbl.SelectMall(Mallid);
                int Shop_ID = (int)dt.Rows[0]["ID"];
                if (Convert.ToString(Mallid) == "1")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblEID");
                    Label lblshopname = (Label)clickedRow.FindControl("Label6");
                    Label lblitem_code = (Label)clickedRow.FindControl("Label2");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Mall_ID"].ToString() == "1" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                            shid = (int)dt.Rows[i]["ID"];
                    }

                    DataTable dtSelect = Log.SelectLogExhibitionRakutenData(int.Parse(lblID.Text), shid, "itemselect");
                    DataTable dtCategory = Log.SelectLogExhibitionRakutenData(int.Parse(lblID.Text), shid, "itemcat");

                    #region new case
                    DataRow[] dr = dtSelect.Select("IsSKU=0 AND Shop_ID=" + shid + "");
                    if (dr.Count() > 0)
                    {
                        DataTable dtSku = dtSelect.Select("IsSKU=0 AND Shop_ID=" + shid + "").CopyToDataTable();

                        //2.select.csv
                        CreateFile(dtSku, "n", "select$", shid, 2, "_0_0.csv", "1.3.2.1.1", lblshopname.Text); //1.3.2.1.2
                    }
                    dr = dtCategory.Select("IsSKU=0");
                    if (dr.Count() > 0)
                    {
                        DataTable dtCate = dtCategory.Select("IsSKU=0").CopyToDataTable();
                        do
                        {
                            string cat_no = dtCate.Rows[0]["カテゴリセット管理番号"].ToString();

                            DataTable dtTemp = dtCate.Select("[カテゴリセット管理番号] = '" + cat_no + "'").CopyToDataTable();

                            //3.item_cat.csv カテゴリセット管理番号
                            CreateFile(dtTemp, "n", "item-cat$", shid, 1, "_0_0_" + cat_no + ".csv", "1.3.2.1.3", lblshopname.Text); //1.3.2.1.3
                            #region delete row
                            List<DataRow> rows_to_remove = new List<DataRow>();
                            foreach (DataRow row1 in dtCate.Rows)
                            {
                                if (row1["カテゴリセット管理番号"].ToString() == cat_no)
                                {
                                    rows_to_remove.Add(row1);
                                }
                            }

                            foreach (DataRow row in rows_to_remove)
                            {
                                dtCate.Rows.Remove(row);
                                dtCate.AcceptChanges();
                            }
                            #endregion

                        } while (dtCate.Rows.Count > 0);
                    }
                    #endregion

                    #region update case

                    dr = dtSelect.Select("IsSKU=1 AND Shop_ID=" + shid + "");
                    if (dr.Count() > 0)
                    {
                        DataTable dtSku = dtSelect.Select("IsSKU=1 AND Shop_ID=" + shid + "").CopyToDataTable();

                        //3.item_select.csv (選択肢タイプ=i)
                        DataTable dtItem;
                        dr = dtSku.Select("[選択肢タイプ]='i'");
                        if (dr.Count() > 0)
                        {
                            dtItem = dtSku.Select("[選択肢タイプ]='i'").CopyToDataTable();
                            CreateFile(dtItem, "n", "select$", shid, 2, "_1_1.csv", "1.3.2.1.1", lblshopname.Text); //1.3.2.4.3
                        }

                        //4.item_select.csv (選択肢タイプ=s)
                        dr = dtSku.Select("[選択肢タイプ]='s'");
                        if (dr.Count() > 0)
                        {
                            dtItem = dtSku.Select("[選択肢タイプ]='s'").CopyToDataTable();
                            CreateFile(dtItem, "d", "select$", shid, 2, "_1_2.csv", "1.3.2.1.1", lblshopname.Text); //1.3.2.4.4

                            //5.item_select.csv (選択肢タイプ=s)
                            CreateFile(dtItem, "n", "select$", shid, 2, "_1_3.csv", "1.3.2.1.1", lblshopname.Text); //1.3.2.4.5
                        }
                    }

                    dr = dtCategory.Select("IsSKU=1 AND [コントロールカラム]='d'");
                    if (dr.Count() > 0)
                    {
                        DataTable dtCate = dtCategory.Select("IsSKU=1").CopyToDataTable();
                        do
                        {
                            string cat_no = dtCate.Rows[0]["カテゴリセット管理番号"].ToString();

                            DataTable dtTemp = dtCate.Select("[カテゴリセット管理番号] = '" + cat_no + "'").CopyToDataTable();
                            //6.item_cat.csv
                            CreateFile(dtTemp, "d", "item-cat$", shid, 1, "_1_1_" + cat_no + ".csv", "1.3.2.1.4", lblshopname.Text); //1.3.2.4.6

                            //7.item_cat.csv
                            CreateFile(dtTemp, "n", "item-cat$", shid, 1, "_1_2_" + cat_no + ".csv", "1.3.2.1.3", lblshopname.Text); //1.3.2.4.7

                            #region delete row
                            List<DataRow> rows_to_remove = new List<DataRow>();
                            foreach (DataRow row1 in dtCate.Rows)
                            {
                                if (row1["カテゴリセット管理番号"].ToString() == cat_no)
                                {
                                    rows_to_remove.Add(row1);
                                }
                            }

                            foreach (DataRow row in rows_to_remove)
                            {
                                dtCate.Rows.Remove(row);
                                dtCate.AcceptChanges();
                            }
                            #endregion

                        } while (dtCate.Rows.Count > 0);
                    }
                    #endregion

                    #region zip
                    string path = UploadPath + "/" + lblshopname.Text + "/" + "csv" + "/";
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string filename = lblitem_code.Text + "_" + lblshopname.Text + "$" + date + ".zip";
                    if (!Directory.Exists(Server.MapPath(path)))
                        Directory.CreateDirectory(Server.MapPath(path));
                    if (Directory.Exists(Server.MapPath(path)))
                    {
                        using (ZipFile zipfile = new ZipFile())
                        {
                            zipfile.AddDirectory(Server.MapPath(path), "csv");
                            zipfile.Save(Server.MapPath(UploadPath) + "/" + filename);
                            lnkdownload.Text = filename;
                        }
                        DeleteDirectory(Server.MapPath(path), true);
                    }
                    #endregion
                }
                else if (Convert.ToString(Mallid) == "2")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblEID");
                    Label lblshopname = (Label)clickedRow.FindControl("Label3");
                    Label lblitem_code = (Label)clickedRow.FindControl("Label2");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Mall_ID"].ToString() == "2" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                            shid = (int)dt.Rows[i]["ID"];
                    }
                    /*
                    DataTable dtMainItem = Log.SelectLogExhibitionYahooData(int.Parse(lblID.Text), shid, "item");
                    DataTable dtMainSku = Log.SelectLogExhibitionYahooData(int.Parse(lblID.Text), shid, "quantity");
                    #region new case
                    DataRow[] dr = dtMainItem.Select("[コントロールカラム]='n'");
                    if (dr.Count() > 0)
                    {
                        DataTable dtItem = dtMainItem.Select("[コントロールカラム]='n'").CopyToDataTable();

                        CreateFile(dtItem, "n", "data_add$", shid, 0, "_0_0.csv", "1.3.2.5.1", lblshopname.Text); //1.3.2.5.1
                        
                    }
                    #endregion

                    #region update case
                    dr = dtMainItem.Select("[コントロールカラム]='u'");
                    if (dr.Count() > 0)
                    {
                        DataTable dtItem = dtMainItem.Select("[コントロールカラム]='u'").CopyToDataTable();

                        CreateFile(dtItem, "u", "data_add$", shid, 0, "_1_0.csv", "1.3.2.5.1", lblshopname.Text); //1.3.2.5.1
                    }
                    #endregion

                    CreateFile(dtMainSku, "", "quantity$", shid, 2, "_0_0.csv", "1.3.2.5.2", lblshopname.Text);
                    */
                }
                else if (Convert.ToString(Mallid) == "4")
                {

                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblEID");
                    Label lblshopname = (Label)clickedRow.FindControl("Label4");
                    Label lblitem_code = (Label)clickedRow.FindControl("Label2");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Mall_ID"].ToString() == "4" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                            shid = (int)dt.Rows[i]["ID"];
                    }

                    DataTable dtSelect = Log.SelectLogExhibitionPonpareData(int.Parse(lblID.Text), shid, "option");
                    DataTable dtMainCategory = Log.SelectLogExhibitionPonpareData(int.Parse(lblID.Text), shid, "category");

                    //#region new case
                    //DataRow[] dr = dtSelect.Select("IsSKU=0 AND Shop_ID=" + shid + "");
                    //if (dr.Count() > 0)
                    //{
                    //    DataTable dtSku = dtSelect.Select("IsSKU=0 AND Shop_ID=" + shid + "").CopyToDataTable();
                    //    //2.option.csv
                    //    CreateFile(dtSku, "n", "option$", shid, 2, "_0_0.csv", "1.3.2.1.2", lblshopname.Text); //1.3.2.1.2
                    //}

                    //dr = dtMainCategory.Select("IsSKU=0");
                    //if (dr.Count() > 0)
                    //{
                    //    DataTable dtCategory = dtMainCategory.Select("IsSKU=0").CopyToDataTable();
                    //    //3.category.csv
                    //    CreateFile(dtCategory, "n", "category$", shid, 1, "_0_0.csv", "1.3.2.1.3", lblshopname.Text); //1.3.2.1.3
                    //}
                    //#endregion

                    //#region update case

                    //dr = dtSelect.Select("IsSKU=1 AND Shop_ID=" + shid + "");
                    //if (dr.Count() > 0)
                    //{
                    //    DataTable dtSku = dtSelect.Select("IsSKU=1 AND Shop_ID=" + shid + "").CopyToDataTable();
                    //    DataTable dtItem;

                    //    dr = dtSku.Select("[選択肢タイプ]='s'");
                    //    if (dr.Count() > 0)
                    //    {
                    //        dtItem = dtSku.Select("[選択肢タイプ]='s'").CopyToDataTable();
                    //        //3.option.csv(item insert)
                    //        CreateFile(dtItem, "n", "option$", shid, 2, "_1_1.csv", "1.3.2.1.2", lblshopname.Text); //1.3.2.4.3
                    //    }

                    //    dr = dtSku.Select("[選択肢タイプ]='o'");
                    //    if (dr.Count() > 0)
                    //    {
                    //        dtItem = dtSku.Select("[選択肢タイプ]='o'").CopyToDataTable();
                    //        //4.option.csv(option delete)
                    //        CreateFile(dtItem, "d", "option$", shid, 2, "_1_2.csv", "1.3.2.1.2", lblshopname.Text); //1.3.2.4.4

                    //        //5.option.csv(option insert)
                    //        CreateFile(dtItem, "n", "option$", shid, 2, "_1_3.csv", "1.3.2.1.2", lblshopname.Text); //1.3.2.4.5
                    //    }
                    //}

                    //dr = dtMainCategory.Select("IsSKU=1 AND [コントロールカラム]='d'");
                    //if (dr.Count() > 0)
                    //{
                    //    DataTable dtCategory = dtMainCategory.Select("IsSKU=1").CopyToDataTable();
                    //    //6.category.csv
                    //    CreateFile(dtCategory, "d", "category$", shid, 1, "_1_1.csv", "1.3.2.1.3", lblshopname.Text); //1.3.2.4.6

                    //    //7.category.csv
                    //    CreateFile(dtCategory, "n", "category$", shid, 1, "_1_2.csv", "1.3.2.1.3", lblshopname.Text); //1.3.2.4.7
                    //}
                    //#endregion

                    //#region zip
                    //string path = UploadPath + "/" + lblshopname.Text + "/" + "csv" + "/";
                    //String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    //string filename = lblitem_code.Text + "_" + lblshopname.Text + "$" + date + ".zip";
                    //if (!Directory.Exists(Server.MapPath(path)))
                    //    Directory.CreateDirectory(Server.MapPath(path));
                    //if (Directory.Exists(Server.MapPath(path)))
                    //{
                    //    using (ZipFile zipfile = new ZipFile())
                    //    {
                    //        zipfile.AddDirectory(Server.MapPath(path), "csv");
                    //        zipfile.Save(Server.MapPath(UploadPath) + "/" + filename);
                    //        lnkdownload.Text = filename;
                    //    }
                    //    DeleteDirectory(Server.MapPath(path), true);
                    //}
                    //#endregion

                }

                //hhw2021-12-15
                //else if (Convert.ToString(Mallid) == "5")
                //{
                //    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                //    Label lblID = (Label)clickedRow.FindControl("lblEID");
                //    Label lblshopname = (Label)clickedRow.FindControl("Label5");
                //    Label lblitem_code = (Label)clickedRow.FindControl("Label2");
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        if (dt.Rows[i]["Mall_ID"].ToString() == "5" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                //            shid = (int)dt.Rows[i]["ID"];
                //    }
                //}
                //else if (Convert.ToString(Mallid) == "6")
                //{
                //    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                //    Label lblID = (Label)clickedRow.FindControl("lblEID");
                //    Label lblshopname = (Label)clickedRow.FindControl("Label7");
                //    Label lblitem_code = (Label)clickedRow.FindControl("Label2");
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        if (dt.Rows[i]["Mall_ID"].ToString() == "6" && dt.Rows[i]["Shop_Name"].ToString() == lblshopname.Text)
                //            shid = (int)dt.Rows[i]["ID"];
                //    }
                //}
                    /*
                    DataTable dtSelect = Log.SelectLogExhibitionJishaData(int.Parse(lblID.Text), shid, "itemselect");
                    DataTable dtCategory = Log.SelectLogExhibitionJishaData(int.Parse(lblID.Text), shid, "itemcat");

                    #region new case
                    DataRow[] dr = dtSelect.Select("IsSKU=0 AND Shop_ID=" + shid + "");
                    if (dr.Count() > 0)
                    {
                        DataTable dtSku = dtSelect.Select("IsSKU=0 AND Shop_ID=" + shid + "").CopyToDataTable();

                        //2.select.csv
                        CreateFile(dtSku, "n", "select$", shid, 2, "_0_0.csv", "1.3.2.1.2", lblshopname.Text); //1.3.2.1.2
                    }
                    dr = dtCategory.Select("IsSKU=0");
                    if (dr.Count() > 0)
                    {
                        DataTable dtCate = dtCategory.Select("IsSKU=0").CopyToDataTable();
                        do
                        {
                            string cat_no = dtCate.Rows[0]["カテゴリセット管理番号"].ToString();

                            DataTable dtTemp = dtCate.Select("[カテゴリセット管理番号] = '" + cat_no + "'").CopyToDataTable();

                            //3.item_cat.csv カテゴリセット管理番号
                            CreateFile(dtTemp, "n", "item-cat$", shid, 1, "_0_0_" + cat_no + ".csv", "1.3.2.1.3", lblshopname.Text); //1.3.2.1.3
                            #region delete row
                            List<DataRow> rows_to_remove = new List<DataRow>();
                            foreach (DataRow row1 in dtCate.Rows)
                            {
                                if (row1["カテゴリセット管理番号"].ToString() == cat_no)
                                {
                                    rows_to_remove.Add(row1);
                                }
                            }

                            foreach (DataRow row in rows_to_remove)
                            {
                                dtCate.Rows.Remove(row);
                                dtCate.AcceptChanges();
                            }
                            #endregion

                        } while (dtCate.Rows.Count > 0);
                    }
                    #endregion

                    #region update case

                    dr = dtSelect.Select("IsSKU=1 AND Shop_ID=" + shid + "");
                    if (dr.Count() > 0)
                    {
                        DataTable dtSku = dtSelect.Select("IsSKU=1 AND Shop_ID=" + shid + "").CopyToDataTable();

                        //3.item_select.csv (選択肢タイプ=i)
                        DataTable dtItem;
                        dr = dtSku.Select("[選択肢タイプ]='i'");
                        if (dr.Count() > 0)
                        {
                            dtItem = dtSku.Select("[選択肢タイプ]='i'").CopyToDataTable();
                            CreateFile(dtItem, "n", "select$", shid, 2, "_1_1.csv", "1.3.2.1.2", lblshopname.Text); //1.3.2.4.3
                        }

                        //4.item_select.csv (選択肢タイプ=s)
                        dr = dtSku.Select("[選択肢タイプ]='s'");
                        if (dr.Count() > 0)
                        {
                            dtItem = dtSku.Select("[選択肢タイプ]='s'").CopyToDataTable();
                            CreateFile(dtItem, "d", "select$", shid, 2, "_1_2.csv", "1.3.2.1.2", lblshopname.Text); //1.3.2.4.4

                            //5.item_select.csv (選択肢タイプ=s)
                            CreateFile(dtItem, "n", "select$", shid, 2, "_1_3.csv", "1.3.2.1.2", lblshopname.Text); //1.3.2.4.5
                        }
                    }

                    dr = dtCategory.Select("IsSKU=1 AND [コントロールカラム]='d'");
                    if (dr.Count() > 0)
                    {
                        DataTable dtCate = dtCategory.Select("IsSKU=1").CopyToDataTable();
                        do
                        {
                            string cat_no = dtCate.Rows[0]["カテゴリセット管理番号"].ToString();

                            DataTable dtTemp = dtCate.Select("[カテゴリセット管理番号] = '" + cat_no + "'").CopyToDataTable();
                            //6.item_cat.csv
                            CreateFile(dtTemp, "d", "item-cat$", shid, 1, "_1_1_" + cat_no + ".csv", "1.3.2.1.4", lblshopname.Text); //1.3.2.4.6

                            //7.item_cat.csv
                            CreateFile(dtTemp, "n", "item-cat$", shid, 1, "_1_2_" + cat_no + ".csv", "1.3.2.1.3", lblshopname.Text); //1.3.2.4.7

                            #region delete row
                            List<DataRow> rows_to_remove = new List<DataRow>();
                            foreach (DataRow row1 in dtCate.Rows)
                            {
                                if (row1["カテゴリセット管理番号"].ToString() == cat_no)
                                {
                                    rows_to_remove.Add(row1);
                                }
                            }

                            foreach (DataRow row in rows_to_remove)
                            {
                                dtCate.Rows.Remove(row);
                                dtCate.AcceptChanges();
                            }
                            #endregion

                        } while (dtCate.Rows.Count > 0);
                    }
                    #endregion
                    */
                

                //GlobalUI.MessageBox("Download Success!");
            }
            #endregion
            #region ShopInfo
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
                    // string sname = String.Join("", shopname.Where(c => !char.IsWhiteSpace(c)));


                    url += shopname + "/" + itemcode;
                    string name = shopname + "/" + itemcode;
                    // Page.ClientScript.RegisterStartupScript( this.GetType(),"OpenWindow", "var win = window.open('" + url + "','_newtab'); self.focus();", true);
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('http://item.rakuten.co.jp/','_blank');", true);
                    Response.Redirect(url);
                    //clickedRow.Cells[11].Attributes.Add("onclick", "javascript:openWindow('http://item.rakuten.co.jp/" + shopname + '/' + "" + itemcode + "');");
                    // clickedRow.Attributes.Add("onclick", "javascript:openWindow('http://item.rakuten.co.jp/" + shopname + "" + itemcode + "');");

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
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var win = window.open('" + yurl + "','_newtab'); self.focus();", true);

                }
                else if (Convert.ToString(Mallid) == "4")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblsitename");
                    Label lblcode = (Label)clickedRow.FindControl("Label2");
                    string itemcode = lblcode.Text;
                    string shopname = lblID.Text;
                    string lot_Number = iteminfo_bl.Get_Lot_Number(itemcode);
                    if (lot_Number != null || lot_Number != "")
                    {
                        wurl += lot_Number;
                        Response.Redirect(wurl);
                    }
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var win = window.open('" + purl + "','_newtab'); self.focus();", true);
                }
                else if (Convert.ToString(Mallid) == "6")
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
                else if (Convert.ToString(Mallid) == "7")
                {
                    GridViewRow clickedRow = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                    Label lblID = (Label)clickedRow.FindControl("lblsitename");
                    Label lblcode = (Label)clickedRow.FindControl("Label2");
                    string itemcode = lblcode.Text;
                    string shopname = lblID.Text;
                    turl += itemcode;
                    Response.Redirect(turl);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var win = window.open('" + purl + "','_newtab'); self.focus();", true);
                }
            }
            #endregion
            else if (e.CommandName == "Edit")
            {
                string Item_Code = e.CommandArgument.ToString();
                Response.Redirect("../Item/Item_Master.aspx?Item_Code=" + Item_Code);

            }

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            Bind();
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

        protected void Bind()
        {
            if (Request.QueryString["list"] != null)
            {
                list = Session["list"].ToString();
            }
            else
            {
                list = string.Empty;
                Session.Remove("list");
            }
            gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());

            Exhibition_Entity ee = GetSearchData();
            int option = 1;//like
            if (chkitemcode.Checked)
                option = 0;//equal
            //int chkerror = 0;
            //if (chkErrSearch.Checked)
            //    chkerror = 1;
            //int chknotcheck = 0;
            //if (chkSearch.Checked)
            //   chknotcheck = 1;
            //int chkrecovery = 0;
            //if (chkReExhibit.Checked)
            //    chkrecovery = 1;
            //DataTable dt = ehbbl.Exhibition_Search(ee, list, option, 1, chkerror, chknotcheck, chkrecovery, gvexhibition.PageSize);
            DataTable dt = ehbbl.Exhibition_Search(ee, list, option, 1, gvexhibition.PageSize);
            int count = 0;

            if (dt != null & dt.Rows.Count > 0)
                count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());

            gp.TotalRecord = count;
            gp.OnePageRecord = gvexhibition.PageSize;
            int index1 = 0;
            gp.sendIndexToThePage += delegate(int index)
            {
                index1 = index;
            };
            gvexhibition.PageIndex = index1;

            gvexhibition.DataSource = dt;
            gvexhibition.DataBind();
            gp.CalculatePaging(count, gvexhibition.PageSize, 1);
            txtexdatetime1.Text = hdfFromDate.Value;
            txtdatetime2.Text = hdfToDate.Value;
        }

        //protected void Bind() 

        //{
        //    try
        //    {
        //        ehbbl = new Exhibition_List_BL();
        //        if (Request.QueryString["list"] != null)
        //        {

        //                string fromDate = Request.Form[txtexdatetime1.UniqueID];
        //                string toDate = Request.Form[txtdatetime2.UniqueID];
        //                hdfFromDate.Value = fromDate;
        //                hdfToDate.Value = toDate;
        //                DateTime? FromDate = new DateTime();
        //                DateTime? ToDate = new DateTime();
        //                if (!String.IsNullOrEmpty(fromDate))
        //                {
        //                    FromDate = DateConverter(fromDate);
        //                }
        //                else
        //                {
        //                    FromDate = null;
        //                }
        //                if (!String.IsNullOrEmpty(toDate))
        //                {
        //                    ToDate = DateConverter(toDate);
        //                }
        //                else
        //                {
        //                    ToDate = null;
        //                }
        //                list = null;
        //                list = Session["list"].ToString();
        //                DataTable dt = new DataTable();
        //                if (chkitemcode.Checked)//equal search
        //                {
        //                    gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());


        //                    dt = null;
        //                    dt = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 1, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
        //                        ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);


        //                    int count = 0;
        //                    if (dt != null && dt.Rows.Count > 0)
        //                        count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());


        //                    gvexhibition.DataSource = dt;
        //                    gvexhibition.DataBind();
        //                    gp.CalculatePaging(count, gvexhibition.PageSize, 1);
        //                }
        //                else // like search
        //                {
        //                    gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());

        //                    dt = null;
        //                    dt = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 9, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
        //                                          ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
        //                    int count = 0;
        //                    if (dt != null && dt.Rows.Count > 0)
        //                        count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());

        //                    gvexhibition.DataSource = dt;
        //                    gvexhibition.DataBind();
        //                    gp.CalculatePaging(count, gvexhibition.PageSize, 1);
        //                }
        //                txtexdatetime1.Text = hdfFromDate.Value;
        //                txtdatetime2.Text = hdfToDate.Value;
        //                hdfFromDate.Value = String.Empty;
        //                hdfToDate.Value = String.Empty;



        //        }
        //        else //for menu
        //        {

        //                Session.Remove("list");
        //                string fromDate = Request.Form[txtexdatetime1.UniqueID];
        //                string toDate = Request.Form[txtdatetime2.UniqueID];
        //                hdfFromDate.Value = fromDate;
        //                hdfToDate.Value = toDate;
        //                DateTime? FromDate = new DateTime();
        //                DateTime? ToDate = new DateTime();
        //                if (!String.IsNullOrEmpty(fromDate))
        //                {
        //                    FromDate = DateConverter(fromDate);
        //                }
        //                else
        //                {
        //                    FromDate = null;
        //                }
        //                if (!String.IsNullOrEmpty(toDate))
        //                {
        //                    ToDate = DateConverter(toDate);
        //                }
        //                else
        //                {
        //                    ToDate = null;
        //                }

        //                if (ddlAPIcheck.SelectedItem.Value == "3")//for ponpare and amazone (not include)
        //                {
        //                    string value = "2";
        //                    DataTable dt = new DataTable();
        //                    if (chkitemcode.Checked)
        //                    {
        //                        gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());

        //                        dt = null;
        //                        dt = ehbbl.SelectAllpaging(menulist, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 6, null, value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
        //                            ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
        //                        int count = 0;
        //                        if (dt != null && dt.Rows.Count > 0)
        //                            count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());


        //                        gvexhibition.DataSource = dt;
        //                        gvexhibition.DataBind();
        //                        gp.CalculatePaging(count, gvexhibition.PageSize, 1);
        //                    }
        //                    else
        //                    {
        //                        gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());


        //                        dt = null;
        //                        dt = ehbbl.SelectAllpaging(menulist, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 10, null, value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
        //                                                    ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
        //                        int counts = 0;
        //                        if (dt != null && dt.Rows.Count > 0)
        //                            counts = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
        //                        for (int i = 0; i < dt.Rows.Count; i++)
        //                        {
        //                            if (dt.Rows[i]["Mall_ID"].ToString() == "3" || dt.Rows[i]["Mall_ID"].ToString() == "4")
        //                            { }
        //                            else
        //                            {
        //                                dt.Rows[i].Delete();
        //                                counts = counts - 1;
        //                            }

        //                        }
        //                        dt.AcceptChanges();




        //                        gvexhibition.DataSource = dt;
        //                        gvexhibition.DataBind();
        //                        gp.CalculatePaging(counts, gvexhibition.PageSize, 1);
        //                    }

        //                }
        //                else
        //                {
        //                    DataTable dt = new DataTable();
        //                    if (chkitemcode.Checked)
        //                    {
        //                        gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());

        //                        dt = null;
        //                        dt = ehbbl.SelectAllpaging(menulist, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 6, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
        //                           ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
        //                        int count = 0;
        //                        if (dt != null && dt.Rows.Count > 0)
        //                            count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());


        //                        gvexhibition.DataSource = dt;
        //                        gvexhibition.DataBind();
        //                        gp.CalculatePaging(count, gvexhibition.PageSize, 1);
        //                    }
        //                    else
        //                    {
        //                        gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());

        //                        dt = null;
        //                        dt = ehbbl.SelectAllpaging(menulist, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 10, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
        //                         ddlbatchcheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
        //                        int count = 0;
        //                        if (dt != null && dt.Rows.Count > 0)
        //                            count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
        //                        if (ddlAPIcheck.SelectedItem.Value == "2")
        //                        {
        //                            for (int i = 0; i < dt.Rows.Count; i++)
        //                            {
        //                                if (dt.Rows[i]["Mall_ID"].ToString() == "3" || dt.Rows[i]["Mall_ID"].ToString() == "4")
        //                                { dt.Rows[i].Delete(); count = count - 1; }
        //                            }
        //                            dt.AcceptChanges();
        //                        }





        //                        gvexhibition.DataSource = dt;
        //                        gvexhibition.DataBind();
        //                        gp.CalculatePaging(count, gvexhibition.PageSize, 1);
        //                    }
        //                }
        //                txtexdatetime1.Text = hdfFromDate.Value;
        //                txtdatetime2.Text = hdfToDate.Value;
        //                hdfFromDate.Value = String.Empty;
        //                hdfToDate.Value = String.Empty;





        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["Exception"] = ex.ToString();
        //        Response.Redirect("~/CustomErrorPage.aspx?");
        //    }


        //}
        protected void gvexhibition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvexhibition.PageSize = Convert.ToInt32(ddlpage.SelectedValue);

                Bind();
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
                //Image im = (Image)e.Row.FindControl("r");
                //Image imy = (Image)e.Row.FindControl("y");
                //Image imp = (Image)e.Row.FindControl("p");
                //Image imj = (Image)e.Row.FindControl("j");
                //Image ima = (Image)e.Row.FindControl("a");

                HtmlGenericControl r = e.Row.FindControl("r") as HtmlGenericControl;
                HtmlGenericControl y = e.Row.FindControl("y") as HtmlGenericControl;
                HtmlGenericControl w = e.Row.FindControl("w") as HtmlGenericControl;
               // HtmlGenericControl j = e.Row.FindControl("j") as HtmlGenericControl;
               // HtmlGenericControl a = e.Row.FindControl("a") as HtmlGenericControl;hhw
                HtmlGenericControl t = e.Row.FindControl("t") as HtmlGenericControl;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (string.IsNullOrWhiteSpace(DataBinder.Eval(e.Row.DataItem, "Exhibition_Date").ToString()))
                    {
                        Button btn = e.Row.FindControl("btndetail") as Button;
                        btn.Enabled = false;
                        Button btn1 = e.Row.FindControl("btnExport") as Button;
                        btn1.Enabled = false;
                        //Label lbl = e.Row.FindControl("lblexhibidateandtime") as Label;
                        //e.Row.Style.Value = "text-decoration:line-through;";
                        //e.Row.Style.TextDecorationLineThrough = true;
                    }

                    #region Strike Line
                    if (string.IsNullOrWhiteSpace(DataBinder.Eval(e.Row.DataItem, "CSV_FileName").ToString()) && string.IsNullOrWhiteSpace(DataBinder.Eval(e.Row.DataItem, "Ctrl_ID").ToString()))
                    {

                        if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(1) && DataBinder.Eval(e.Row.DataItem, "IsR1_Collect").ToString() == "2")
                        {
                            e.Row.Style.Value = "text-decoration:line-through;";
                            Button btn = e.Row.FindControl("btnExport") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btndetail") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btnEdit") as Button;
                            btn.Enabled = false;
                        }
                        //if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(5) && DataBinder.Eval(e.Row.DataItem, "IsR5_Collect").ToString() == "2")
                        //{
                        //    e.Row.Style.Value = "text-decoration:line-through;";
                        //    Button btn = e.Row.FindControl("btnExport") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btndetail") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btnEdit") as Button;
                        //    btn.Enabled = false;
                        //}
                        //if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(8) && DataBinder.Eval(e.Row.DataItem, "IsR8_Collect").ToString() == "2")
                        //{
                        //    e.Row.Style.Value = "text-decoration:line-through;";
                        //    Button btn = e.Row.FindControl("btnExport") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btndetail") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btnEdit") as Button;
                        //    btn.Enabled = false;
                        //}
                        //if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(12) && DataBinder.Eval(e.Row.DataItem, "IsR12_Collect").ToString() == "2")
                        //{
                        //    e.Row.Style.Value = "text-decoration:line-through;";
                        //    Button btn = e.Row.FindControl("btnExport") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btndetail") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btnEdit") as Button;
                        //    btn.Enabled = false;
                        //}
                        if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(2) && DataBinder.Eval(e.Row.DataItem, "IsY2_Collect").ToString() == "2")
                        {
                            e.Row.Style.Value = "text-decoration:line-through;";
                            Button btn = e.Row.FindControl("btnExport") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btndetail") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btnEdit") as Button;
                            btn.Enabled = false;
                        }
                        //if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(6) && DataBinder.Eval(e.Row.DataItem, "IsY6_Collect").ToString() == "2")
                        //{
                        //    e.Row.Style.Value = "text-decoration:line-through;";
                        //    Button btn = e.Row.FindControl("btnExport") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btndetail") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btnEdit") as Button;
                        //    btn.Enabled = false;
                        //}
                        //if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(9) && DataBinder.Eval(e.Row.DataItem, "IsY9_Collect").ToString() == "2")
                        //{
                        //    e.Row.Style.Value = "text-decoration:line-through;";
                        //    Button btn = e.Row.FindControl("btnExport") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btndetail") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btnEdit") as Button;
                        //    btn.Enabled = false;
                        //}
                        //if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(13) && DataBinder.Eval(e.Row.DataItem, "IsY13_Collect").ToString() == "2")
                        //{
                        //    e.Row.Style.Value = "text-decoration:line-through;";
                        //    Button btn = e.Row.FindControl("btnExport") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btndetail") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btnEdit") as Button;
                        //    btn.Enabled = false;
                        //}
                        //if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(17) && DataBinder.Eval(e.Row.DataItem, "IsY17_Collect").ToString() == "2")
                        //{
                        //    e.Row.Style.Value = "text-decoration:line-through;";
                        //    Button btn = e.Row.FindControl("btnExport") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btndetail") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btnEdit") as Button;
                        //    btn.Enabled = false;
                        //}
                        if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(3) && DataBinder.Eval(e.Row.DataItem, "IsP3_Collect").ToString() == "2")
                        {
                            e.Row.Style.Value = "text-decoration:line-through;";
                            Button btn = e.Row.FindControl("btnExport") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btndetail") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btnEdit") as Button;
                            btn.Enabled = false;
                        }
                        if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(4) && DataBinder.Eval(e.Row.DataItem, "IsW4_Collect").ToString() == "2")
                        {
                            e.Row.Style.Value = "text-decoration:line-through;";
                            Button btn = e.Row.FindControl("btnExport") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btndetail") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btnEdit") as Button;
                            btn.Enabled = false;
                        }
                        //hhw2021-12-16
                        //if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(21) && DataBinder.Eval(e.Row.DataItem, "IsJ21_Collect").ToString() == "2")
                        //{
                        //    e.Row.Style.Value = "text-decoration:line-through;";
                        //    Button btn = e.Row.FindControl("btnExport") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btndetail") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btnEdit") as Button;
                        //    btn.Enabled = false;
                        //}

                        //if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(4) && DataBinder.Eval(e.Row.DataItem, "IsA4_Collect").ToString() == "2")
                        //{
                        //    e.Row.Style.Value = "text-decoration:line-through;";
                        //    Button btn = e.Row.FindControl("btnExport") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btndetail") as Button;
                        //    btn.Enabled = false;
                        //    btn = e.Row.FindControl("btnEdit") as Button;
                        //    btn.Enabled = false;
                        //}
                        if (DataBinder.Eval(e.Row.DataItem, "Shop_ID").ToString().ToLower() == Convert.ToString(6) && DataBinder.Eval(e.Row.DataItem, "IsT6_Collect").ToString() == "2")
                        {
                            e.Row.Style.Value = "text-decoration:line-through;";
                            Button btn = e.Row.FindControl("btnExport") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btndetail") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btnEdit") as Button;
                            btn.Enabled = false;
                        }
                        /*
                        if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(1) && DataBinder.Eval(e.Row.DataItem, "IsRakutenCSV_Generate").ToString() == "1")
                        {
                            e.Row.Style.Value = "text-decoration:line-through;";
                            Button btn = e.Row.FindControl("btnExport") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btndetail") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btnEdit") as Button;
                            btn.Enabled = false;
                        }
                        else if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(2) && DataBinder.Eval(e.Row.DataItem, "IsYahooCSV_Generate").ToString() == "1")
                        {
                            e.Row.Style.Value = "text-decoration:line-through;";
                            Button btn = e.Row.FindControl("btnExport") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btndetail") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btnEdit") as Button;
                            btn.Enabled = false;
                        }
                        else if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(3) && DataBinder.Eval(e.Row.DataItem, "IsPonpareCSV_Generate").ToString() == "1")
                        {
                            e.Row.Style.Value = "text-decoration:line-through;";
                            Button btn = e.Row.FindControl("btnExport") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btndetail") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btnEdit") as Button;
                            btn.Enabled = false;
                        }
                        else if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(4) && DataBinder.Eval(e.Row.DataItem, "IsAmazonCSV_Generate").ToString() == "1")
                        {
                            e.Row.Style.Value = "text-decoration:line-through;";
                            Button btn = e.Row.FindControl("btnExport") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btndetail") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btnEdit") as Button;
                            btn.Enabled = false;
                        }
                        else if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(5) && DataBinder.Eval(e.Row.DataItem, "IsJishaCSV_Generate").ToString() == "1")
                        {
                            e.Row.Style.Value = "text-decoration:line-through;";
                            Button btn = e.Row.FindControl("btnExport") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btndetail") as Button;
                            btn.Enabled = false;
                            btn = e.Row.FindControl("btnEdit") as Button;
                            btn.Enabled = false;
                        }
                        //e.Row.Style.Value = "text-decoration:line-through;";
                        */
                    }
                    #endregion


                    if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(1))
                    {
                        Label lblr = e.Row.FindControl("Label6") as Label;
                        if (!String.IsNullOrWhiteSpace(lblr.Text))
                            lblr.Style.Add("Width", "200px");

                        r.Style.Add("float", "left");


                        lblr.Visible = true;
                        r.Visible = true;
                        y.Visible = false;
                        w.Visible = false;
                        //j.Visible = false;
                        //a.Visible = false;
                        //  im.Visible = true;
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
                        w.Visible = false;
                        //j.Visible = false;
                        //a.Visible = false;
                        t.Visible = false;
                        //  imy.Visible = true;
                        Button btn1 = e.Row.FindControl("btnExport") as Button;
                        btn1.Visible = false;

                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(4))
                    {
                        Label lblw = e.Row.FindControl("Label4") as Label;
                        if (!String.IsNullOrWhiteSpace(lblw.Text))
                            lblw.Style.Add("Width", "200px");

                        w.Style.Add("float", "left");
                        lblw.Visible = true;
                        r.Visible = false;
                        y.Visible = false;
                        w.Visible = true;
                        //j.Visible = false;
                        //a.Visible = false;
                        t.Visible = false;
                        // imp.Visible = true;
                        Button btn1 = e.Row.FindControl("btnExport") as Button;
                        btn1.Visible = false;
                    }
                    //hhw2021-12-16
                    //else if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(5))
                    //{
                    //    Label lblj = e.Row.FindControl("Label5") as Label;
                    //    if (!String.IsNullOrWhiteSpace(lblj.Text))
                    //        lblj.Style.Add("Width", "200px");

                    //    j.Style.Add("float", "left");
                    //    lblj.Visible = true;
                    //    r.Visible = false;
                    //    y.Visible = false;
                    //    w.Visible = false;
                    //    j.Visible = true ;
                    //    //a.Visible = false;
                    //    t.Visible = false;
                    //    // ima.Visible = true;
                    //    Button btn1 = e.Row.FindControl("btnExport") as Button;
                    //    btn1.Visible = false;
                    //}
                    //else if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(6))
                    //{
                    //    Label lbla = e.Row.FindControl("Label7") as Label;
                    //    if (!String.IsNullOrWhiteSpace(lbla.Text))
                    //        lbla.Style.Add("Width", "200px");

                    //    a.Style.Add("float", "left");
                    //    lbla.Visible = true;
                    //    r.Visible = false;
                    //    y.Visible = false;
                    //    w.Visible = false;
                    //    j.Visible = false;
                    //    a.Visible = true;
                    //    t.Visible = false;
                    //    // imj.Visible = true;
                    //    Button btn1 = e.Row.FindControl("btnExport") as Button;
                    //    btn1.Visible = false;
                    //}
                    else if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(7))
                    {
                        Label lblt = e.Row.FindControl("Label8") as Label;
                        if (!String.IsNullOrWhiteSpace(lblt.Text))
                            lblt.Style.Add("Width", "200px");

                        t.Style.Add("float", "left");
                        lblt.Visible = true;
                        r.Visible = false;
                        y.Visible = false;
                        w.Visible = false;
                        //j.Visible = false;
                        //a.Visible = false;
                        t.Visible = true;
                        // imj.Visible = true;
                        Button btn1 = e.Row.FindControl("btnExport") as Button;
                        btn1.Visible = false;
                    }


                    if (DataBinder.Eval(e.Row.DataItem, "API_Check").ToString().ToLower() == Convert.ToString(1))
                    {
                        Label lblAPIcheck = (Label)e.Row.FindControl("lblAPIcheck");
                        lblAPIcheck.Text = "×";
                        //   e.Row.Cells[9].Text = "×";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "API_Check").ToString().ToLower() == Convert.ToString(0))
                    {
                        if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(5) || DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(6))
                        {
                            Label lblAPIcheck = (Label)e.Row.FindControl("lblAPIcheck");
                            lblAPIcheck.Text = "_";
                            //e.Row.Cells[9].Text = "_"; 
                        }
                        else
                        {
                            Label lblAPIcheck = (Label)e.Row.FindControl("lblAPIcheck");
                            lblAPIcheck.Text = "未";
                            //e.Row.Cells[9].Text = "未";
                        }

                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "API_Check").ToString().ToLower() == Convert.ToString(2))
                    {
                        if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(5))
                        {
                            Label lblAPIcheck = (Label)e.Row.FindControl("lblAPIcheck");
                            lblAPIcheck.Text = "対象外";
                            //e.Row.Cells[9].Text = "対象外";
                        }
                        else
                        {
                            Label lblAPIcheck = (Label)e.Row.FindControl("lblAPIcheck");
                            lblAPIcheck.Text = "○";
                            //e.Row.Cells[9].Text = "○";
                        }
                    }

                    #region Batch Check

                    //if (DataBinder.Eval(e.Row.DataItem, "Batch_Check").ToString().ToLower() == Convert.ToString(1))
                    //{
                    //    Label lblbatchcheck = (Label)e.Row.FindControl("lblbatchcheck");
                    //    lblbatchcheck.Text = "×";
                    //    //e.Row.Cells[10].Text = "×";
                    //}
                    //else if (DataBinder.Eval(e.Row.DataItem, "Batch_Check").ToString().ToLower() == Convert.ToString(0))
                    //{
                    //    Label lblbatchcheck = (Label)e.Row.FindControl("lblbatchcheck");
                    //    lblbatchcheck.Text = "未";
                    //    //e.Row.Cells[10].Text = "未";

                    //}
                    //else if (DataBinder.Eval(e.Row.DataItem, "Batch_Check").ToString().ToLower() == Convert.ToString(2))
                    //{
                    //    Label lblbatchcheck = (Label)e.Row.FindControl("lblbatchcheck");
                    //    lblbatchcheck.Text = "○";
                    //    //e.Row.Cells[10].Text = "○";
                    //}
                    #endregion

                    if (DataBinder.Eval(e.Row.DataItem, "ExportError_Check").ToString().ToLower() == Convert.ToString(1))
                    {
                        Label lblexhiresulterror = (Label)e.Row.FindControl("lblexhiresulterror");
                        lblexhiresulterror.Text = "×";
                        //e.Row.Cells[8].Text = "×";
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "ExportError_Check").ToString().ToLower() == Convert.ToString(0))
                    {
                        //e.Row.Cells[8].Text = "未";
                        //updated by hlz.
                        if (DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(6) || DataBinder.Eval(e.Row.DataItem, "Mall_ID").ToString().ToLower() == Convert.ToString(5))
                        {
                            Label lblexhiresulterror = (Label)e.Row.FindControl("lblexhiresulterror");
                            lblexhiresulterror.Text = "_";
                            //e.Row.Cells[8].Text = "_"; 
                        }
                        else
                        {
                            Label lblexhiresulterror = (Label)e.Row.FindControl("lblexhiresulterror");
                            lblexhiresulterror.Text = "未";
                            //e.Row.Cells[8].Text = "未";
                        }
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "ExportError_Check").ToString().ToLower() == Convert.ToString(2))
                    {
                        Label lblexhiresulterror = (Label)e.Row.FindControl("lblexhiresulterror");
                        lblexhiresulterror.Text = "○";
                        //e.Row.Cells[8].Text = "○";
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

        protected void Downloaddata()
        {
            Exhibition_List_BL ehbbl = new Exhibition_List_BL();
            if (Request.QueryString["list"] != null)
            {
                list = Session["list"].ToString();
            }
            else
            {
                list = string.Empty;
                Session.Remove("list");
            }
            gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());

            Exhibition_Entity ee = GetSearchData();
            int option = 1;//like
            if (chkitemcode.Checked)
                option = 0;//equal
            DataTable dt = ehbbl.Exhibitiondownload_Search(ee, list, option, 1, gvexhibition.PageSize);
            if (dt != null && dt.Rows.Count > 0)
            {
                //dt.Columns.Remove("No");
                dt.Columns.Remove("Total_Count");
                dt.AcceptChanges();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "出品説明文");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=出品説明文.xlsx");
                    Response.HeaderEncoding = System.Text.Encoding.GetEncoding("shift_jis");
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift_jis");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }


            }//if condition
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                Downloaddata();
            }
            catch (Exception ex)
            {
                // Session["Exception"] = ex.ToString();
                //// Response.Redirect("~/CustomErrorPage.aspx?");
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

        
        //protected void btnDownload_Click1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ehbbl = new Exhibition_List_BL();
        //        DataTable dt = new DataTable();
        //        if (Request.QueryString["list"] != null)
        //        {
        //            string fromDate = Request.Form[txtexdatetime1.UniqueID];
        //            string toDate = Request.Form[txtdatetime2.UniqueID];
        //            hdfFromDate.Value = fromDate;
        //            hdfToDate.Value = toDate;
        //            DateTime? FromDate = new DateTime();
        //            DateTime? ToDate = new DateTime();
        //            if (!String.IsNullOrEmpty(fromDate))
        //            {
        //                FromDate = DateConverter(fromDate);
        //            }
        //            else
        //            {
        //                FromDate = null;
        //            }
        //            if (!String.IsNullOrEmpty(toDate))
        //            {
        //                ToDate = DateConverter(toDate);
        //            }
        //            else
        //            {
        //                ToDate = null;
        //            }
        //            list = null;
        //            list = Session["list"].ToString();
        //            dt = null;
        //            if (chkitemcode.Checked)//equal search
        //            {
        //                gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());

        //                dt = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 21, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
        //                    ddlexhibitioncheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
        //            }
        //            else
        //            {
        //                gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
        //                dt = ehbbl.SelectAllpaging(list, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 29, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
        //                ddlexhibitioncheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
        //            }
        //            ///for excelexport
        //            DataTable dtCSV = new DataTable();
        //            String csv = String.Empty;
        //            if (dt != null && dt.Rows.Count > 0)
        //                //for (int u = 0; u < dt.Rows.Count; u++)
        //                //{
        //                //    if (dt.Columns.Contains("ID"))
        //                //        csv += dt.Rows[u]["ID"].ToString() + ",";
        //                //}
        //                dtCSV = dt.Copy();
        //            //  dtCSV = ehbbl.SelectData(csv);
        //            if (dtCSV != null && dtCSV.Rows.Count > 0)
        //            {
        //                #region
        //                //  ConsoleWriteLine_Tofile("ExcelExport");
        //                //  string path = "C:/Users/Administrator/Documents/SKS/";
        //                ////  string path = "C:/TestExcel/";
        //                // // string path = excelurl;
        //                //  DataColumnCollection dcCollection = dtCSV.Columns;
        //                //  Excel.Application myExcelApp;
        //                //  Excel.Workbooks myExcelWorkbooks;
        //                //  Excel.Workbook myExcelWorkbook;
        //                //  Excel.Worksheet myExcelWorksheet;

        //                //  //misValue = System.Reflection.Missing.Value;
        //                //  ConsoleWriteLine_Tofile("new Excel.Application");
        //                //  myExcelApp = new Excel.Application();
        //                //  //                   myExcelApp.Visible = true;

        //                //  myExcelWorkbooks = myExcelApp.Workbooks;
        //                //  ConsoleWriteLine_Tofile("myExcelApp.Workbooks");
        //                //  String TemplateFileName = "Template/ItemExport.xlsx";
        //                //  String ExportFileName = "出品説明文" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
        //                //  System.IO.File.Copy(@path + TemplateFileName, @path + "Export/" + ExportFileName, true);

        //                //  myExcelWorkbook = myExcelWorkbooks.Open(path + "Export/" + ExportFileName);
        //                //  ConsoleWriteLine_Tofile("Open success");
        //                //  myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

        //                //  //             myExcelWorksheet = (Excel._Worksheet)myExcelWorkbook.ActiveSheet;

        //                //  for (int i = 1; i < dtCSV.Rows.Count + 2; i++)
        //                //  {
        //                //      for (int j = 1; j < dtCSV.Columns.Count + 1; j++)
        //                //      {
        //                //          if (i == 1)
        //                //          {
        //                //              myExcelWorksheet.Cells[i, j] = dcCollection[j - 1].ToString();
        //                //          }
        //                //          else
        //                //              myExcelWorksheet.Cells[i, j] = dtCSV.Rows[i - 2][j - 1].ToString();
        //                //      }
        //                //  }
        //                //  ConsoleWriteLine_Tofile("Edit success");
        //                //  //myExcelWorkbook.SaveAs(path + "Export/" + "商品説明文" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");    // Excel保存
        //                //  myExcelWorkbook.Save();
        //                //  myExcelWorkbook.Saved = true;
        //                //  myExcelWorkbook.Close(false, Type.Missing, Type.Missing);
        //                //  myExcelWorkbooks.Close();
        //                //  myExcelApp.Quit();
        //                //  ConsoleWriteLine_Tofile("Save success");
        //                //  //Marshal.ReleaseComObject(myExcelWorksheet);
        //                //  //Marshal.ReleaseComObject(myExcelWorkbooks);
        //                //  //Marshal.ReleaseComObject(myExcelApp);

        //                //  // Response情報クリア
        //                //  Response.ClearContent();
        //                //  // HTTPヘッダー情報設定
        //                //  Response.AddHeader("Content-Disposition", "attachment;filename=" + ExportFileName);
        //                //  Response.ContentType = "application/msexcel";
        //                //  Response.HeaderEncoding = System.Text.Encoding.GetEncoding("shift_jis");
        //                //  Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift_jis");
        //                //  Response.Flush();
        //                //  // ファイル書込(データによりResponse.WriteFile()、Response.Write()、Response.BinaryWrite()を使い分ける。)
        //                //  Response.TransmitFile(path + "Export/" + ExportFileName);
        //                //  // レスポンス終了
        //                //  Response.End();

        //                //  return;
        //                #endregion

        //                using (XLWorkbook wb = new XLWorkbook())
        //                {
        //                    wb.Worksheets.Add(dtCSV, "出品説明文");

        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.Charset = "";
        //                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //                    Response.AddHeader("content-disposition", "attachment;filename=出品説明文.xlsx");
        //                    Response.HeaderEncoding = System.Text.Encoding.GetEncoding("shift_jis");
        //                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift_jis");
        //                    using (MemoryStream MyMemoryStream = new MemoryStream())
        //                    {
        //                        wb.SaveAs(MyMemoryStream);
        //                        MyMemoryStream.WriteTo(Response.OutputStream);
        //                        Response.Flush();
        //                        Response.End();
        //                    }
        //                }
        //            }//if condition
        //        }//if condition end
        //        else // for menu
        //        {
        //            Session.Remove("list");
        //            string fromDate = Request.Form[txtexdatetime1.UniqueID];
        //            string toDate = Request.Form[txtdatetime2.UniqueID];
        //            hdfFromDate.Value = fromDate;
        //            hdfToDate.Value = toDate;
        //            DateTime? FromDate = new DateTime();
        //            DateTime? ToDate = new DateTime();
        //            if (!String.IsNullOrEmpty(fromDate))
        //            {
        //                FromDate = DateConverter(fromDate);
        //            }
        //            else
        //            {
        //                FromDate = null;
        //            }
        //            if (!String.IsNullOrEmpty(toDate))
        //            {
        //                ToDate = DateConverter(toDate);
        //            }
        //            else
        //            {
        //                ToDate = null;
        //            }


        //            if (ddlAPIcheck.SelectedItem.Value == "3")//for ponpare and amazone (not include)
        //            {
        //                string value = "2";
        //                dt = null;
        //                if (chkitemcode.Checked)
        //                {
        //                    gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());

        //                    dt = ehbbl.SelectAllpaging(menulist, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 26, null, value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
        //                        ddlexhibitioncheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
        //                }
        //                else
        //                {
        //                    gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
        //                    dt = ehbbl.SelectAllpaging(menulist, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 30, null, value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
        //                    ddlexhibitioncheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
        //                    for (int i = 0; i < dt.Rows.Count; i++)
        //                    {
        //                        if (dt.Rows[i]["Mall_ID"].ToString() == "3" || dt.Rows[i]["Mall_ID"].ToString() == "4")
        //                        { }
        //                        else
        //                        {
        //                            dt.Rows[i].Delete();

        //                        }
        //                    }
        //                    dt.AcceptChanges();
        //                }
        //            }
        //            else
        //            {
        //                dt = null;
        //                if (chkitemcode.Checked)
        //                {
        //                    gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
        //                    dt = ehbbl.SelectAllpaging(menulist, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 26, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
        //                       ddlexhibitioncheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
        //                }
        //                else
        //                {
        //                    gvexhibition.PageSize = int.Parse(ddlpage.SelectedValue.ToString());

        //                    dt = ehbbl.SelectAllpaging(menulist, null, ddlmall.SelectedItem.Value, txtcode.Text.Trim(), 30, null, ddlAPIcheck.SelectedItem.Value, ddlexbresulterror.SelectedItem.Value, ddlexhibitor.SelectedItem.Value,
        //                     ddlexhibitioncheck.SelectedItem.Value, txtproname.Text.Trim(), txtcatinfo.Text.Trim(), txtbrandname.Text.Trim(), txtcompname.Text.Trim(), txtcompetitionname.Text.Trim(), txtclassname.Text.Trim(), txtyear.Text.Trim(), txtseason.Text.Trim(), txtremark.Text.Trim(), FromDate, ToDate, 1, gvexhibition.PageSize);
        //                    if (ddlAPIcheck.SelectedItem.Value == "2")
        //                    {
        //                        for (int i = 0; i < dt.Rows.Count; i++)
        //                        {
        //                            if (dt.Rows[i]["Mall_ID"].ToString() == "3" || dt.Rows[i]["Mall_ID"].ToString() == "4")
        //                            { dt.Rows[i].Delete(); }
        //                        }
        //                        dt.AcceptChanges();
        //                    }
        //                }
        //            }





        //            ///for excelexport
        //            String csv = String.Empty;
        //            DataTable dtCSV = new DataTable();
        //            if (dt != null && dt.Rows.Count > 0)
        //                //for (int u = 0; u < dt.Rows.Count; u++)
        //                //{
        //                //    if (dt.Columns.Contains("ID"))
        //                //        csv += dt.Rows[u]["ID"].ToString() + ",";
        //                //}


        //                //  dtCSV = ehbbl.SelectData(csv);
        //                dtCSV = dt.Copy();

        //            if (dtCSV != null && dtCSV.Rows.Count > 0)
        //            {
        //                #region
        //                // //for csv test
        //                // string filenames = "Exhibition_Excel"+ DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv";


        //                // ConsoleWriteLine_Tofile("ExcelExport");
        //                // string path = "C:/Users/Administrator/Documents/SKS/";
        //                ////string path = "C:/TestExcel/";
        //                //// string path = excelurl;
        //                // DataColumnCollection dcCollection = dtCSV.Columns;
        //                // Excel.Application myExcelApp;
        //                // Excel.Workbooks myExcelWorkbooks;
        //                // Excel.Workbook myExcelWorkbook;
        //                // Excel.Worksheet myExcelWorksheet;

        //                // //misValue = System.Reflection.Missing.Value;
        //                // ConsoleWriteLine_Tofile("new Excel.Application");
        //                // myExcelApp = new Excel.Application();
        //                // //                   myExcelApp.Visible = true;

        //                // myExcelWorkbooks = myExcelApp.Workbooks;
        //                // ConsoleWriteLine_Tofile("myExcelApp.Workbooks");
        //                // String TemplateFileName = "Template/ItemExport.xlsx";
        //                // String ExportFileName = "出品説明文" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
        //                // System.IO.File.Copy(@path + TemplateFileName, @path + "Export/" + ExportFileName, true);

        //                // myExcelWorkbook = myExcelWorkbooks.Open(path + "Export/" + ExportFileName);
        //                // ConsoleWriteLine_Tofile("Open success");
        //                // myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

        //                // //             myExcelWorksheet = (Excel._Worksheet)myExcelWorkbook.ActiveSheet;

        //                // for (int i = 1; i < dtCSV.Rows.Count + 2; i++)
        //                // {
        //                //     for (int j = 1; j < dtCSV.Columns.Count + 1; j++)
        //                //     {
        //                //         if (i == 1)
        //                //         {
        //                //             myExcelWorksheet.Cells[i, j] = dcCollection[j - 1].ToString();
        //                //         }
        //                //         else
        //                //             myExcelWorksheet.Cells[i, j] = dtCSV.Rows[i - 2][j - 1].ToString();
        //                //     }
        //                // }
        //                // ConsoleWriteLine_Tofile("Edit success");
        //                // //myExcelWorkbook.SaveAs(path + "Export/" + "商品説明文" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");    // Excel保存
        //                // myExcelWorkbook.Save();
        //                // myExcelWorkbook.Saved = true;
        //                // myExcelWorkbook.Close(false, Type.Missing, Type.Missing);
        //                // myExcelWorkbooks.Close();
        //                // myExcelApp.Quit();
        //                // ConsoleWriteLine_Tofile("Save success");
        //                // //Marshal.ReleaseComObject(myExcelWorksheet);
        //                // //Marshal.ReleaseComObject(myExcelWorkbooks);
        //                // //Marshal.ReleaseComObject(myExcelApp);

        //                // // Response情報クリア
        //                // Response.ClearContent();
        //                // // HTTPヘッダー情報設定
        //                // Response.AddHeader("Content-Disposition", "attachment;filename=" + ExportFileName);
        //                // Response.ContentType = "application/msexcel";
        //                // Response.HeaderEncoding = System.Text.Encoding.GetEncoding("shift_jis");
        //                // Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift_jis");
        //                // Response.Flush();
        //                // // ファイル書込(データによりResponse.WriteFile()、Response.Write()、Response.BinaryWrite()を使い分ける。)
        //                // Response.TransmitFile(path + "Export/" + ExportFileName);
        //                // // レスポンス終了
        //                // Response.End();

        //                // return;
        //                #endregion
        //                using (XLWorkbook wb = new XLWorkbook())
        //                {
        //                    wb.Worksheets.Add(dtCSV, "出品説明文");

        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.Charset = "";
        //                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //                    Response.AddHeader("content-disposition", "attachment;filename=出品説明文.xlsx");
        //                    Response.HeaderEncoding = System.Text.Encoding.GetEncoding("shift_jis");
        //                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift_jis");
        //                    using (MemoryStream MyMemoryStream = new MemoryStream())
        //                    {
        //                        wb.SaveAs(MyMemoryStream);
        //                        MyMemoryStream.WriteTo(Response.OutputStream);
        //                        Response.Flush();
        //                        Response.End();
        //                    }
        //                }

        //            }//if condition


        //        }//else condition
        //    }
        //    catch (Exception ex)
        //    {
        //        // Session["Exception"] = ex.ToString();

        //        //// Response.Redirect("~/CustomErrorPage.aspx?");
        //        String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //        SqlConnection con = new SqlConnection(connectionString);

        //        SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@UserID", -1);
        //        cmd.Parameters.AddWithValue("@ErrorDetail", "ShopID=Excel_Export" + ex.ToString());
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();
        //    }
        //}

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "ConsoleWriteLine.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        public string CreateFile(DataTable dt, String CtrlID, String firstName, int shop_id, int filetype, String extension, String fileNo, String shop_name)
        {
            String filename = "";
            if (!string.IsNullOrWhiteSpace(CtrlID))
            {
                DataRow[] dr = dt.Select("[コントロールカラム] = '" + CtrlID + "'");
                if (dr.Count() > 0)
                {
                    dt = dt.Select("[コントロールカラム] = '" + CtrlID + "'").CopyToDataTable();
                    DataTable dtCopy = dt.Copy();
                    String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    filename = firstName + shop_id + "_" + date + extension;
                    dtCopy = FormatFile(dtCopy, fileNo);
                    GenerateCSV(dtCopy, filename, shop_name);
                    //SaveItem_ExportQ(filename, filetype, shop_id, 0, 1);
                }
            }
            return filename;
        }

        public DataTable FormatFile(DataTable dt, String fileNo)
        {
            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrWhiteSpace(fileNo))
            {
                switch (fileNo)
                {
                    case "1.3.2.1.1":  // select.csv, item-cat.csv rakuten
                        {
                            //dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("Shop_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("コントロールカラム");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.4.1": // item.csv (ctrl = u)
                        {
                            //dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("ID");
                            dt.Columns.Remove("サーチ非表示");
                            dt.Columns.Remove("商品番号");
                            dt.Columns.Remove("全商品ディレクトリID");
                            dt.Columns.Remove("タグID");
                            dt.Columns.Remove("PC用キャッチコピー");
                            dt.Columns.Remove("モバイル用キャッチコピー");
                            dt.Columns.Remove("商品名");
                            dt.Columns.Remove("販売価格");
                            dt.Columns.Remove("表示価格");
                            dt.Columns.Remove("消費税");
                            dt.Columns.Remove("送料");
                            dt.Columns.Remove("個別送料");
                            dt.Columns.Remove("送料区分1");
                            dt.Columns.Remove("送料区分2");
                            dt.Columns.Remove("代引料");
                            dt.Columns.Remove("商品情報レイアウト");
                            dt.Columns.Remove("注文ボタン");
                            dt.Columns.Remove("資料請求ボタン");
                            dt.Columns.Remove("商品問い合わせボタン");
                            dt.Columns.Remove("再入荷お知らせボタン");
                            dt.Columns.Remove("モバイル表示");
                            dt.Columns.Remove("のし対応");
                            dt.Columns.Remove("PC用商品説明文");
                            dt.Columns.Remove("モバイル用商品説明文");
                            dt.Columns.Remove("スマートフォン用商品説明文");
                            dt.Columns.Remove("PC用販売説明文");
                            dt.Columns.Remove("商品画像URL");
                            dt.Columns.Remove("商品画像名（ALT）");
                            dt.Columns.Remove("動画");
                            dt.Columns.Remove("販売期間指定");
                            dt.Columns.Remove("注文受付数");
                            dt.Columns.Remove("在庫数");
                            dt.Columns.Remove("在庫数表示");
                            dt.Columns.Remove("項目選択肢別在庫用横軸項目名");
                            dt.Columns.Remove("項目選択肢別在庫用縦軸項目名");
                            dt.Columns.Remove("項目選択肢別在庫用残り表示閾値");
                            dt.Columns.Remove("RAC番号");
                            dt.Columns.Remove("闇市パスワード");
                            dt.Columns.Remove("カタログID");
                            dt.Columns.Remove("在庫戻しフラグ");
                            dt.Columns.Remove("在庫切れ時の注文受付");
                            dt.Columns.Remove("在庫あり時納期管理番号");
                            dt.Columns.Remove("在庫切れ時納期管理番号");
                            dt.Columns.Remove("予約商品発売日");
                            dt.Columns.Remove("ポイント変倍率");
                            dt.Columns.Remove("ポイント変倍率適用期間");
                            dt.Columns.Remove("ヘッダー・フッター・レフトナビ");
                            dt.Columns.Remove("表示項目の並び順");
                            dt.Columns.Remove("共通説明文（小）");
                            dt.Columns.Remove("目玉商品");
                            dt.Columns.Remove("共通説明文（大）");
                            dt.Columns.Remove("レビュー本文表示");
                            dt.Columns.Remove("あす楽配送管理番号");
                            dt.Columns.Remove("サイズ表リンク");
                            dt.Columns.Remove("二重価格文言管理番号");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.9.1": //item.csv(for ponpare mall) 1st update file
                        {
                            //dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("ID");
                            dt.Columns.Remove("販売ステータス");
                            dt.Columns.Remove("商品ID");
                            dt.Columns.Remove("商品名");
                            dt.Columns.Remove("キャッチコピー");
                            dt.Columns.Remove("販売価格");
                            dt.Columns.Remove("表示価格");
                            dt.Columns.Remove("消費税");
                            dt.Columns.Remove("送料");
                            dt.Columns.Remove("独自送料グループ(1)");
                            dt.Columns.Remove("独自送料グループ(2)");
                            dt.Columns.Remove("個別送料");
                            dt.Columns.Remove("代引料");
                            dt.Columns.Remove("のし対応");
                            dt.Columns.Remove("商品問い合わせボタン");
                            dt.Columns.Remove("販売期間指定");
                            dt.Columns.Remove("注文受付数");
                            dt.Columns.Remove("在庫数");
                            dt.Columns.Remove("在庫表示");
                            dt.Columns.Remove("商品説明(1)");
                            dt.Columns.Remove("商品説明(2)");
                            dt.Columns.Remove("商品説明(テキストのみ)");
                            dt.Columns.Remove("商品画像URL");
                            dt.Columns.Remove("モールジャンルID");
                            dt.Columns.Remove("シークレットセールパスワード");
                            dt.Columns.Remove("ポイント率");
                            dt.Columns.Remove("ポイント率適用期間");
                            dt.Columns.Remove("SKU横軸項目名");
                            dt.Columns.Remove("SKU縦軸項目名");
                            dt.Columns.Remove("SKU在庫用残り表示閾値");
                            dt.Columns.Remove("商品説明(スマートフォン用)");
                            dt.Columns.Remove("JANコード");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.1.2":  //select.csv (ctrl_ID = n , u , d)
                        {
                            //dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("Shop_ID");
                            dt.Columns.Remove("IsSKU");
                            //dt.Columns.Remove("コントロールカラム");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.1.3":  //item_cat.csv (Ctrl_ID = n)
                        {
                            //dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.1.4":  //item_cat.csv (Ctrl_ID = d)
                        {
                            //dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("優先度");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.5.2": //Yahoo_quantity
                        {
                            //dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("ID");
                            dt.AcceptChanges();
                            break;
                        }
                    case "1.3.2.5.1"://Yahoo_data
                        {
                            //dt.Columns.Remove("Exhibit_ID");
                            dt.Columns.Remove("ID");
                            dt.Columns.Remove("IsSKU");
                            dt.Columns.Remove("コントロールカラム");
                            dt.AcceptChanges();
                            break;
                        }

                }
            }
            return dt;
        }

        public void GenerateCSV(DataTable dtInformation, string FileName, string shop_name)
        {
            ConsoleWriteLine_Tofile("File Name : " + FileName);
            string path = UploadPath + "/" + shop_name + "/" + "csv" + "/";
            if (!Directory.Exists(Server.MapPath(path)))
                Directory.CreateDirectory(Server.MapPath(path));
            using (StreamWriter writer = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath(path + FileName), false, Encoding.GetEncoding(932)))
            {
                WriteDataTable(dtInformation, writer, true);
            }
        }

        public void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                List<string> headerValues = new List<string>();

                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerValues.Add(QuoteValue(column.ColumnName));
                }
                StringBuilder builder = new StringBuilder();
                writer.WriteLine(String.Join(",", headerValues.ToArray()));
            }

            string[] items = null;
            foreach (DataRow row in sourceTable.Rows)
            {
                items = row.ItemArray.Select(o => QuoteValue(o.ToString())).ToArray();
                writer.WriteLine(String.Join(",", items));
            }

            writer.Flush();
        }

        private string QuoteValue(string value)
        {
            return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
        }

        protected void lnkdownload_Click(object sender, EventArgs e)
        {
            Download(UploadPath + lnkdownload.Text);
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
                response.ContentType = "application/octet-stream";
                byte[] data = req.DownloadData(Server.MapPath(filecheck));
                response.BinaryWrite(data);
                response.End();
            }
            else
            {
                //GlobalUI.MessageBox("File doesn't exist!"); 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MESSAGE", "alert('File doesn't exist!');", true);
            }
        }

        public static void DeleteDirectory(string path, bool recursive)
        {
            // Delete all files and sub-folders?
            if (recursive)
            {
                // Yep... Let's do this
                var subfolders = Directory.GetDirectories(path);
                foreach (var s in subfolders)
                {
                    DeleteDirectory(s, recursive);
                }
            }

            // Get all files of the folder
            var files = Directory.GetFiles(path);
            foreach (var f in files)
            {
                // Get the attributes of the file
                var attr = File.GetAttributes(f);

                // Is this file marked as 'read-only'?
                if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // Yes... Remove the 'read-only' attribute, then
                    File.SetAttributes(f, attr ^ FileAttributes.ReadOnly);
                }

                // Delete the file
                File.Delete(f);
            }

            // When we get here, all the files of the folder were
            // already deleted, so we just delete the empty folder
            Directory.Delete(path);
        }
    }
}