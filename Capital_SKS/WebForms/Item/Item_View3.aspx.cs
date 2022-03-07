using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using ORS_RCM_Common;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.HtmlControls;
using ORS_RCM.WebForms.Item;
using System.Text;
using System.Web.Services;

namespace ORS_RCM.WebForms.Item
{
    public partial class Item_View3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Item_ExportField_BL iebl = new ORS_RCM_BL.Item_ExportField_BL();
                    //bind user to dropdownlist
                    ddlContactPerson.DataSource = iebl.SelectUser();
                    ddlContactPerson.DataValueField = "ID";
                    ddlContactPerson.DataTextField = "User_Name";
                    ddlContactPerson.DataBind();
                    ddlContactPerson.Items.Insert(0, "");

                    //bind gvItem
                    Bind();

                    //Get check value in gridview
                    ItemCheck_Change();
                }
                else
                {
                    String ctrl = getPostBackControlName();

                    string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

                    if (eventTarget == "ShopCategory")
                    {
                        if (Session["CategoryList"] != null)
                        {
                            DataTable dt = Session["CategoryList"] as DataTable;
                            String index = dt.Rows[0]["Index"].ToString();
                            btnShopCategory_Click(Convert.ToInt32(index));
                            hfCtrl.Value = String.Empty;
                            hfRemoveList.Text = "0";
                        }
                    }
                    else if (eventTarget == "YahooSpec")
                    {
                        if (Session["YahooSpecificValue"] != null)
                        {
                            DataTable dt = Session["YahooSpecificValue"] as DataTable;
                            String index = dt.Rows[0]["Index"].ToString();
                            btnSpec_Click(Convert.ToInt32(index));
                            hfCtrl.Value = String.Empty;
                            hfRemoveList.Text = "0";
                        }
                    }

                    if (ctrl == null)
                    {
                        ctrl = String.Empty;
                    }
                    if (ctrl.Contains("chkItem"))
                    {
                    }
                    else if (ctrl.Contains("lnkItemNo"))
                    {
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

                        if (hfRemoveList.Text.Equals("1"))
                        {
                            String cklist = String.Join(",", (string[])arrlst.ToArray(Type.GetType("System.String")));
                            ime.RemoveList = cklist;
                        }

                        gp.LinkButtonClick(ctrl, gvItem.PageSize);

                        Label lbl = gp.FindControl("lblCurrent") as Label;
                        int index = Convert.ToInt32(lbl.Text);

                        DataTable dt = imbl.SelectAll_ItemView3(ime, index, 10, 1);
                        gvItem.DataSource = dt;
                        gvItem.DataBind();
                        ItemCheck_Change();
                    }
                    //else if (ctrl.Contains("btnSpec"))
                    //{
                    //    String Control = hfCtrl.Value.ToString();
                    //    String[] str = Control.Split('_');
                    //    String ctrlName = str[str.Length - 2];
                    //    String index = str[str.Length - 1];

                    //    btnSpec_Click(Convert.ToInt32(index));
                    //    hfCtrl.Value = String.Empty;
                    //    hfRemoveList.Text = "0";
                    //}

                    //click btnSelectAll 
                    else if (hfCtrl.Value.ToString().Contains("btnSelectAll"))
                    {
                        btnSelectAll_Click();//call btnSelectAll click event
                        hfCtrl.Value = String.Empty;//clear hfCtrl that save current button ID
                        hfRemoveList.Text = "0";//btnSelectItemRemove = 1 ,Otherwise 0
                    }
                    //click btnUnSelectAll
                    else if (hfCtrl.Value.ToString().Contains("btnUnSelectAll"))
                    {
                        btnUnSelectAll_Click();//call btnUnSelectAll click event
                        hfCtrl.Value = String.Empty;//clear hfCtrl that save current button ID
                        hfRemoveList.Text = "0";//btnSelectItemRemove = 1 ,Otherwise 0
                    }
                    //click btnExhibitSelectProduct
                    else if (hfCtrl.Value.ToString().Contains("btnExhibitSelectProduct"))
                    {
                        btnExhibitSelectProduct_Click();//call btnExhibitSelectProduct click event
                        hfCtrl.Value = String.Empty;//clear hfCtrl that save current button ID
                        hfRemoveList.Text = "0";//btnSelectItemRemove = 1 ,Otherwise 0
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnSelectItemNewTab"))
                    {
                        btnSelectItemNewTab_Click();
                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnSelectItemRemove"))
                    {
                        hfRemoveList.Text = "1";
                        btnSelectItemRemove_Click();
                        hfCtrl.Value = String.Empty;
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnPhotoSave"))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        btnPhotoSave_Click(Convert.ToInt32(index));
                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnOption"))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        btnOption_Click(Convert.ToInt32(index));
                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnShopCategory"))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        btnShopCategory_Click(Convert.ToInt32(index));

                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnRakuten"))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        btnRakuten_Click(Convert.ToInt32(index));
                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnYahoo"))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        btnYahoo_Click(Convert.ToInt32(index));
                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnPonpare"))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        btnPonpare_Click(Convert.ToInt32(index));
                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnSpec"))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        btnSpec_Click(Convert.ToInt32(index));
                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnPreview"))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        btnPreview_Click(Convert.ToInt32(index));
                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnClearLog") || hfCtrl.Value.ToString().Contains("Lock") && String.IsNullOrWhiteSpace(ctrl))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        btnClearLog_Click(index);
                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnUpdate") && String.IsNullOrWhiteSpace(ctrl))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        btnUpdate_Click(index);
                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnFinish") && String.IsNullOrWhiteSpace(ctrl))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        btnDataComplete_Click(index);
                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("btnSearch") && String.IsNullOrWhiteSpace(ctrl))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        btnSearch_Click();
                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                    }
                    else if (hfCtrl.Value.ToString().Contains("ddlShop") && String.IsNullOrWhiteSpace(ctrl))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                        ddlShop_SelectedIndexChanged(Convert.ToInt32(index));

                    }
                    else if (hfCtrl.Value.ToString().Contains("lnkItemNo") && String.IsNullOrWhiteSpace(ctrl))
                    {
                        String Control = hfCtrl.Value.ToString();
                        String[] str = Control.Split('_');
                        String ctrlName = str[str.Length - 2];
                        String index = str[str.Length - 1];

                        hfCtrl.Value = String.Empty;
                        hfRemoveList.Text = "0";
                        lnkItemNo_Click(index);
                    }
                }
            }
            catch(Exception ex)
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

        protected void Bind()
        {
            try
            {
                Item_Master_BL imbl = new Item_Master_BL();
                Item_Master_Entity ime = GetData();

                DataTable dt = //imbl.Search(ime,0,2,10);
                    imbl.SelectAll_ItemView3(ime, 0, 10, 2);//Select all count

                int count = Convert.ToInt32(dt.Rows[0][0].ToString());

                gvItem.DataSource = //imbl.Search(ime, -1 , 1, 10);
                    imbl.SelectAll_ItemView3(ime, 1, 10, 1);//Select first 10 row
                gvItem.DataBind();
                gp.CalculatePaging(count, gvItem.PageSize, 1);

                Session.Remove("Option");
                Session.Remove("Mall_Category_ID");
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        //written by aam (28/11/2014)
        protected Item_Master_Entity GetData()
        {
            try
            {
                Item_Master_Entity ime = new Item_Master_Entity();
                ime.Item_Name = txtItemName.Value.Trim();
                ime.Item_Code = txtItemCode.Value.Trim();
                ime.Catalog_Information = txtCatalogInfo.Value.Trim();
                ime.Brand_Name = txtBrandName.Value.Trim();
                ime.Competition_Name = txtCompetitionName.Value.Trim();
                ime.Year = txtYear.Value.Trim();
                ime.Season = txtSeason.Value.Trim();
                if (!string.IsNullOrWhiteSpace(ddlSpecialFlag.SelectedValue))
                    ime.Special_Flag = Convert.ToInt32(ddlSpecialFlag.SelectedValue);
                else
                    ime.Special_Flag = -1;
                if (!string.IsNullOrWhiteSpace(ddlReserveFlag.SelectedValue))
                    ime.Reservation_Flag = Convert.ToInt32(ddlReserveFlag.SelectedValue);
                else
                    ime.Reservation_Flag = -1;
                if (!string.IsNullOrWhiteSpace(ddlSksStatus.SelectedValue))
                    ime.Export_Status = Convert.ToInt32(ddlSksStatus.SelectedValue);
                else
                    ime.Export_Status = -1;
                if (!string.IsNullOrWhiteSpace(ddlContactPerson.SelectedValue))
                    ime.Updated_By = Convert.ToInt32(ddlContactPerson.SelectedValue);
                else
                    ime.Updated_By = -1;
                ime.Ctrl_ID = ddlShopStatus.SelectedValue;
                ime.Color_Name = txtColorName.Value.Trim();
                ime.Image_Name = txtImageFileName.Value.Trim();

                if (hfRemoveList.Text.Equals("1"))
                {
                    if (ViewState["checkedValue"] != null)
                    {
                        ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                        String cklist = String.Join(",", (string[])arrlst.ToArray(Type.GetType("System.String")));
                        ime.RemoveList = cklist;
                        hfRemoveList.Text = "0";
                    }
                }
                else if (Session["chkList"]!=null)
                {
                    String cklist = Session["chkList"].ToString();
                    ime.IdList = cklist;
                    Session["chkList"] = null;
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

        protected void btnSpec_Click(int rowIndex)
        {
            try
            {
                TextBox txt;
                Label lbl;

                Label l1 = gvItem.Rows[rowIndex].FindControl("lblID") as Label;
                Button btn = gvItem.Rows[rowIndex].FindControl("btnSpec") as Button;

                DataTable dt = Session["YahooSpecificValue"] as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        btn.Enabled = true;
                        if (dt.Rows.Count > 0)
                        {
                            //lbl = gvItem.Rows[rowIndex].FindControl("lblName1") as Label;
                            //lbl.Text = dt.Rows[0]["Name"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_ID1") as Label;
                            lbl.Text = dt.Rows[0]["Spec_ID"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_Name1") as Label;
                            lbl.Text = dt.Rows[0]["Spec_Name"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_ValueID1") as Label;
                            lbl.Text = dt.Rows[0]["Spec_ValueID"].ToString();

                            txt = gvItem.Rows[rowIndex].FindControl("txtSpec1") as TextBox;
                            txt.Text = dt.Rows[0]["Spec_ValueName"].ToString();

                            if (txt.Text.Equals("--Select--") || string.IsNullOrWhiteSpace(txt.Text))
                                txt.Text = String.Empty;
                        }
                        else
                        {
                            txt = gvItem.Rows[rowIndex].FindControl("txtSpec1") as TextBox;
                            txt.Text = String.Empty;
                        }
                        if (dt.Rows.Count > 1)
                        {
                            //lbl = gvItem.Rows[rowIndex].FindControl("lblName1") as Label;
                            //lbl.Text = dt.Rows[1]["Name"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_ID2") as Label;
                            lbl.Text = dt.Rows[1]["Spec_ID"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_Name2") as Label;
                            lbl.Text = dt.Rows[1]["Spec_Name"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_ValueID2") as Label;
                            lbl.Text = dt.Rows[1]["Spec_ValueID"].ToString();

                            txt = gvItem.Rows[rowIndex].FindControl("txtSpec2") as TextBox;
                            txt.Text = dt.Rows[1]["Spec_ValueName"].ToString();

                            if (txt.Text.Equals("--Select--") || string.IsNullOrWhiteSpace(txt.Text))
                                txt.Text = String.Empty;
                        }
                        else
                        {
                            txt = gvItem.Rows[rowIndex].FindControl("txtSpec2") as TextBox;
                            txt.Text = String.Empty;
                        }
                        if (dt.Rows.Count > 2)
                        {
                            //lbl = gvItem.Rows[rowIndex].FindControl("lblName1") as Label;
                            //lbl.Text = dt.Rows[2]["Name"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_ID3") as Label;
                            lbl.Text = dt.Rows[2]["Spec_ID"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_Name3") as Label;
                            lbl.Text = dt.Rows[2]["Spec_Name"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_ValueID3") as Label;
                            lbl.Text = dt.Rows[2]["Spec_ValueID"].ToString();

                            txt = gvItem.Rows[rowIndex].FindControl("txtSpec3") as TextBox;
                            txt.Text = dt.Rows[2]["Spec_ValueName"].ToString();

                            if (txt.Text.Equals("--Select--") || string.IsNullOrWhiteSpace(txt.Text))
                                txt.Text = String.Empty;
                        }
                        else
                        {
                            txt = gvItem.Rows[rowIndex].FindControl("txtSpec3") as TextBox;
                            txt.Text = String.Empty;
                        }
                        if (dt.Rows.Count > 3)
                        {
                            //lbl = gvItem.Rows[rowIndex].FindControl("lblName1") as Label;
                            //lbl.Text = dt.Rows[3]["Name"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_ID4") as Label;
                            lbl.Text = dt.Rows[3]["Spec_ID"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_Name4") as Label;
                            lbl.Text = dt.Rows[3]["Spec_Name"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_ValueID4") as Label;
                            lbl.Text = dt.Rows[3]["Spec_ValueID"].ToString();

                            txt = gvItem.Rows[rowIndex].FindControl("txtSpec4") as TextBox;
                            txt.Text = dt.Rows[3]["Spec_ValueName"].ToString();

                            if (txt.Text.Equals("--Select--") || string.IsNullOrWhiteSpace(txt.Text))
                                txt.Text = String.Empty;
                        }
                        else
                        {
                            txt = gvItem.Rows[rowIndex].FindControl("txtSpec4") as TextBox;
                            txt.Text = String.Empty;
                        }
                        if (dt.Rows.Count > 4)
                        {
                            //lbl = gvItem.Rows[rowIndex].FindControl("lblName1") as Label;
                            //lbl.Text = dt.Rows[4]["Name"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_ID5") as Label;
                            lbl.Text = dt.Rows[4]["Spec_ID"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_Name5") as Label;
                            lbl.Text = dt.Rows[4]["Spec_Name"].ToString();

                            lbl = gvItem.Rows[rowIndex].FindControl("lblSpec_ValueID5") as Label;
                            lbl.Text = dt.Rows[4]["Spec_ValueID"].ToString();

                            txt = gvItem.Rows[rowIndex].FindControl("txtSpec5") as TextBox;
                            txt.Text = dt.Rows[4]["Spec_ValueName"].ToString();

                            if (txt.Text.Equals("--Select--") || string.IsNullOrWhiteSpace(txt.Text))
                                txt.Text = String.Empty;
                        }
                        else
                        {
                            txt = gvItem.Rows[rowIndex].FindControl("txtSpec5") as TextBox;
                            txt.Text = String.Empty;
                        }
                    }
                    else
                    {
                        btn.Enabled = false;
                        txt = gvItem.Rows[rowIndex].FindControl("txtSpec1") as TextBox;
                        txt.Text = String.Empty;
                        txt = gvItem.Rows[rowIndex].FindControl("txtSpec2") as TextBox;
                        txt.Text = String.Empty;
                        txt = gvItem.Rows[rowIndex].FindControl("txtSpec3") as TextBox;
                        txt.Text = String.Empty;
                        txt = gvItem.Rows[rowIndex].FindControl("txtSpec4") as TextBox;
                        txt.Text = String.Empty;
                        txt = gvItem.Rows[rowIndex].FindControl("txtSpec5") as TextBox;
                        txt.Text = String.Empty;
                    }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnPhotoSave_Click(int index)
        {
            try
            {
                Label l1 = gvItem.Rows[index].FindControl("lblID") as Label;

                DataList dl = gvItem.Rows[index].FindControl("dlImage") as DataList;

                Item_Master_BL imbl = new Item_Master_BL();
                DataTable dt = imbl.SelectItemImage(l1.Text);

                DataRow[] drImage = dt.Select("Image_Type = 0");
                if (drImage.Count() > 0)
                {
                    DataTable dtItem = dt.Select("Image_Type = 0").CopyToDataTable();
                    //DataList dlImage = gvItem.Rows[index].FindControl("dlImage") as DataList;
                    //dlImage.DataSource = dtItem;
                    //dlImage.DataBind();
                    Image img = new Image();
                    string imagePath = ConfigurationManager.AppSettings["ItemImage"].ToString();
                    //img = gvItem.Rows[index].FindControl("imgItem1") as Image;
                    //img.ImageUrl = imagePath + dtItem.Rows[0]["Image_Name"].ToString();

                    //for (int k = 1; k <= 5; k++)
                    //{
                    //    img = gvItem.Rows[index].FindControl("imgItem" + k) as Image;
                    //    img.ImageUrl = imagePath + "not-img.png";
                    //}

                    int i = 0;
                    int row = 0;
                    while (row < 5)
                    {
                        if (row <= dtItem.Rows.Count)
                        {
                            //if (!dtItem.Rows[row]["Image_Name"].ToString().Equals("not-img.png") && !String.IsNullOrWhiteSpace(dtItem.Rows[row]["Image_Name"].ToString()))
                            //{
                            if (!String.IsNullOrWhiteSpace(dtItem.Rows[row]["Image_Name"].ToString()))
                            {
                                img = gvItem.Rows[index].FindControl("imgItem" + (i + 1)) as Image;
                                img.ImageUrl = imagePath + dtItem.Rows[row]["Image_Name"].ToString();
                                i++;
                            }
                            else
                            {
                                img = gvItem.Rows[index].FindControl("imgItem" + (i + 1)) as Image;
                                img.ImageUrl ="";
                                i++;
                            }


                            row++;
                        }
                    }
                }
                Lock_Unlock(false, index);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }            
        }

        protected void btnOption_Click(int rowIndex)
        {
            try
            {
                Label l1 = gvItem.Rows[rowIndex].FindControl("lblID") as Label;
                if (Session["Option"] != null)
                {
                    DataTable dt = Session["Option"] as DataTable;

                    if (dt.Rows.Count > 0)
                    {
                        TextBox txt = new TextBox();
                        txt = gvItem.Rows[rowIndex].FindControl("txtOptionValue1") as TextBox;
                        txt.Text = dt.Rows[0]["value1"].ToString();

                        txt = gvItem.Rows[rowIndex].FindControl("txtOptionName1") as TextBox;
                        txt.Text = dt.Rows[0]["name1"].ToString();

                        txt = gvItem.Rows[rowIndex].FindControl("txtOptionValue2") as TextBox;
                        txt.Text = dt.Rows[0]["value2"].ToString();

                        txt = gvItem.Rows[rowIndex].FindControl("txtOptionName2") as TextBox;
                        txt.Text = dt.Rows[0]["name2"].ToString();

                        txt = gvItem.Rows[rowIndex].FindControl("txtOptionValue3") as TextBox;
                        txt.Text = dt.Rows[0]["value3"].ToString();

                        txt = gvItem.Rows[rowIndex].FindControl("txtOptionName3") as TextBox;
                        txt.Text = dt.Rows[0]["name3"].ToString();
                    }
                    hfCtrl.Value = "";
                }
                Session.Remove("Option");
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnRakuten_Click(int rowIndex)
        {
            try
            {
                Label l1 = gvItem.Rows[rowIndex].FindControl("lblID") as Label;

                if (Session["Mall_Category_ID"] != null)
                {
                    DataTable dt = Session["Mall_Category_ID"] as DataTable;

                    if (dt.Rows.Count > 0)
                    {
                        TextBox txt = new TextBox();

                        txt = gvItem.Rows[rowIndex].FindControl("txtrakuten") as TextBox;
                        txt.Text = dt.Rows[0]["Category_ID"].ToString();

                        Label lbl = new Label();

                        lbl = gvItem.Rows[rowIndex].FindControl("lblRakutenCategoryID") as Label;
                        lbl.Text = dt.Rows[0]["ID"].ToString();
                    }

                    Session.Remove("Mall_Category_ID");
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnYahoo_Click(int rowIndex)
        {
            try
            {
                Label l1 = gvItem.Rows[rowIndex].FindControl("lblID") as Label;

                if (Session["Mall_Category_ID"] != null)
                {
                    DataTable dt = Session["Mall_Category_ID"] as DataTable;

                    if (dt.Rows.Count > 0)
                    {
                        TextBox txt = new TextBox();

                        txt = gvItem.Rows[rowIndex].FindControl("txtyahoo") as TextBox;
                        txt.Text = dt.Rows[0]["Category_ID"].ToString();

                        Label lbl = new Label();

                        lbl = gvItem.Rows[rowIndex].FindControl("lblYahooCategoryID") as Label;
                        lbl.Text = dt.Rows[0]["ID"].ToString();

                        DisplayYahooSpecificValue(txt.Text);
                        btnSpec_Click(rowIndex);
                    }

                    Session.Remove("Mall_Category_ID");
                }
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

        protected void btnPonpare_Click(int rowIndex)
        {
            try 
            {
                Label l1 = gvItem.Rows[rowIndex].FindControl("lblID") as Label;

                if (Session["Mall_Category_ID"] != null)
                {
                    DataTable dt = Session["Mall_Category_ID"] as DataTable;

                    if (dt.Rows.Count > 0)
                    {
                        TextBox txt = new TextBox();

                        txt = gvItem.Rows[rowIndex].FindControl("txtponpare") as TextBox;
                        txt.Text = dt.Rows[0]["Category_ID"].ToString();

                        Label lbl = new Label();

                        lbl = gvItem.Rows[rowIndex].FindControl("lblPonpareCategoryID") as Label;
                        lbl.Text = dt.Rows[0]["ID"].ToString();
                    }
                }

                Session.Remove("Mall_Category_ID");
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }            
        }

        protected void btnShopCategory_Click(int rowIndex)
        {
            try
            {
                Label l1 = gvItem.Rows[rowIndex].FindControl("lblID") as Label;

                if (Session["CategoryList"] != null)
                {
                    DataTable dtCategory = Session["CategoryList"] as DataTable;

                    //GridView gvShopCategory = gvItem.Rows[rowIndex].FindControl("gvShopCategory") as GridView;
                    //gvShopCategory.DataSource = dt;
                    //gvShopCategory.DataBind();
                    TextBox txt;
                    Label lbl;


                    if (dtCategory.Rows.Count == 0)
                    {
                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory1") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID1") as Label;

                        txt.Text = String.Empty;
                        lbl.Text = String.Empty;

                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory2") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID2") as Label;

                        txt.Text = String.Empty;
                        lbl.Text = String.Empty;

                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory3") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID3") as Label;

                        txt.Text = String.Empty;
                        lbl.Text = String.Empty;

                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory4") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID4") as Label;

                        txt.Text = String.Empty;
                        lbl.Text = String.Empty;

                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory5") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID5") as Label;

                        txt.Text = String.Empty;
                        lbl.Text = String.Empty;

                        TextBox mall = gvItem.Rows[rowIndex].FindControl("txtrakuten") as TextBox;
                        mall.Text = String.Empty;

                        mall = gvItem.Rows[rowIndex].FindControl("txtyahoo") as TextBox;
                        mall.Text = String.Empty;

                        mall = gvItem.Rows[rowIndex].FindControl("txtponpare") as TextBox;
                        mall.Text = String.Empty;
                    }

                    if (dtCategory.Rows.Count > 0)
                    {
                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory1") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID1") as Label;

                        txt.Text = dtCategory.Rows[0]["CName"].ToString();
                        lbl.Text = dtCategory.Rows[0]["CID"].ToString();

                        Category_BL catbl = new Category_BL();
                        DataTable dtCat = catbl.Get_CategoryID(lbl.Text);

                        if (dtCat.Rows.Count > 0)
                        {
                            TextBox mall = gvItem.Rows[rowIndex].FindControl("txtrakuten") as TextBox;
                            mall.Text = dtCat.Rows[0]["Rakutan_DirectoryID"].ToString();

                            //lbl = gvItem.Rows[rowIndex].FindControl("lblRakutenCategoryID") as Label;
                            //lbl.Text = dtCat.Rows[0]["ID"].ToString();

                            mall = gvItem.Rows[rowIndex].FindControl("txtyahoo") as TextBox;
                            mall.Text = dtCat.Rows[0]["Yahoo_CategoryID"].ToString();
                            DisplayYahooSpecificValue(mall.Text);
                            btnSpec_Click(rowIndex);

                            mall = gvItem.Rows[rowIndex].FindControl("txtponpare") as TextBox;
                            mall.Text = dtCat.Rows[0]["Ponpare_CategoryID"].ToString();
                        }
                        else
                        {
                            TextBox mall = gvItem.Rows[rowIndex].FindControl("txtrakuten") as TextBox;
                            mall.Text = String.Empty;

                            mall = gvItem.Rows[rowIndex].FindControl("txtyahoo") as TextBox;
                            mall.Text = String.Empty;

                            mall = gvItem.Rows[rowIndex].FindControl("txtponpare") as TextBox;
                            mall.Text = String.Empty;

                            mall = gvItem.Rows[rowIndex].FindControl("txtSpec1") as TextBox;
                            mall.Text = String.Empty;

                            mall = gvItem.Rows[rowIndex].FindControl("txtSpec2") as TextBox;
                            mall.Text = String.Empty;

                            mall = gvItem.Rows[rowIndex].FindControl("txtSpec3") as TextBox;
                            mall.Text = String.Empty;

                            mall = gvItem.Rows[rowIndex].FindControl("txtSpec4") as TextBox;
                            mall.Text = String.Empty;

                            mall = gvItem.Rows[rowIndex].FindControl("txtSpec5") as TextBox;
                            mall.Text = String.Empty;

                        }
                    }
                    else
                    {
                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory1") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID1") as Label;

                        txt.Text = String.Empty;
                        lbl.Text = String.Empty;
                    }

                    if (dtCategory.Rows.Count > 1)
                    {
                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory2") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID2") as Label;

                        txt.Text = dtCategory.Rows[1]["CName"].ToString();
                        lbl.Text = dtCategory.Rows[1]["CID"].ToString();
                    }
                    else
                    {
                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory2") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID2") as Label;

                        txt.Text = String.Empty;
                        lbl.Text = String.Empty;
                    }

                    if (dtCategory.Rows.Count > 2)
                    {
                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory3") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID3") as Label;

                        txt.Text = dtCategory.Rows[2]["CName"].ToString();
                        lbl.Text = dtCategory.Rows[2]["CID"].ToString();
                    }
                    else
                    {
                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory3") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID3") as Label;

                        txt.Text = String.Empty;
                        lbl.Text = String.Empty;
                    }

                    if (dtCategory.Rows.Count > 3)
                    {
                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory4") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID4") as Label;

                        txt.Text = dtCategory.Rows[3]["CName"].ToString();
                        lbl.Text = dtCategory.Rows[3]["CID"].ToString();
                    }
                    else
                    {
                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory4") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID4") as Label;

                        txt.Text = String.Empty;
                        lbl.Text = String.Empty;
                    }

                    if (dtCategory.Rows.Count > 4)
                    {
                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory5") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID5") as Label;

                        txt.Text = dtCategory.Rows[4]["CName"].ToString();
                        lbl.Text = dtCategory.Rows[4]["CID"].ToString();
                    }
                    else
                    {
                        txt = gvItem.Rows[rowIndex].FindControl("txtShopCategory5") as TextBox;
                        lbl = gvItem.Rows[rowIndex].FindControl("lblSCID5") as Label;

                        txt.Text = String.Empty;
                        lbl.Text = String.Empty;
                    }

                    Session.Remove("CategoryList");
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnDataComplete_Click(String index)
        {
            try
            {
                int rowIndex = Convert.ToInt32(index);

                Label lbl = new Label();
                lbl = gvItem.Rows[rowIndex].FindControl("lblID") as Label;

                btnUpdate_Click(index);

                Item_View3_BL ivbl = new Item_View3_BL();
                ivbl.Change_DataComplete(lbl.Text);

                HtmlGenericControl Ppage = gvItem.Rows[rowIndex].FindControl("Ppage") as HtmlGenericControl;
                HtmlGenericControl PWaitSt = gvItem.Rows[rowIndex].FindControl("PWaitSt") as HtmlGenericControl;
                HtmlGenericControl PWaitL = gvItem.Rows[rowIndex].FindControl("PWaitL") as HtmlGenericControl;
                HtmlGenericControl POkSt = gvItem.Rows[rowIndex].FindControl("POkSt") as HtmlGenericControl;

                Ppage.Visible = false;
                PWaitSt.Visible = true;
                PWaitL.Visible = false;
                POkSt.Visible = false;
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
                String url = "Item_Master.aspx?Item_Code=" + lnk.Text;
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "newpage", "customOpen('" + url + "');", true);
                Response.Redirect("Item_Master.aspx?Item_Code=" + lnk.Text,false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnUpdate_Click(String index1)
        {
            try
            {
                int rowIndex = Convert.ToInt32(index1);
                Label lbl = new Label();
                lbl = gvItem.Rows[rowIndex].FindControl("lblID") as Label;
                int itemId = Convert.ToInt32(lbl.Text);
                TextBox txt = new TextBox();

                if (Validation(rowIndex))
                {
                    #region Item_Option Update
                    Item_Option_BL iobl = new Item_Option_BL();
                    String n1, n2, n3, v1, v2, v3;
                    txt = gvItem.Rows[rowIndex].FindControl("txtOptionName1") as TextBox;
                    n1 = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtOptionName2") as TextBox;
                    n2 = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtOptionName3") as TextBox;
                    n3 = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtOptionValue1") as TextBox;
                    v1 = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtOptionValue2") as TextBox;
                    v2 = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtOptionValue3") as TextBox;
                    v3 = txt.Text;

                    iobl.Insert_Update(itemId, v1, n1, v2, n2, v3, n3);
                    #endregion

                    #region Shop_Category Update
                    Item_Category_BL icbl = new Item_Category_BL();

                    GridView gv = gvItem.Rows[rowIndex].FindControl("gvShopCategory") as GridView;
                    DataTable dt = new DataTable();


                    dt = new DataTable();
                    dt.Columns.Add("Item_ID", typeof(Int32));
                    dt.Columns.Add("CID", typeof(Int32));
                    dt.Columns.Add("Category_SN", typeof(Int32));
                    TextBox txtsc; Label lblid; Label lblsn;

                    for (int i = 1; i <= 5; i++)
                    {
                        dt.Rows.Add();
                        txtsc = gvItem.Rows[rowIndex].FindControl("txtShopCategory" + i) as TextBox;
                        lblid = gvItem.Rows[rowIndex].FindControl("lblSCID" + i) as Label;
                        lblsn = gvItem.Rows[rowIndex].FindControl("lblSN" + i) as Label;

                        if (!String.IsNullOrWhiteSpace(lblid.Text))
                        {
                            dt.Rows[i - 1]["Item_ID"] = itemId;
                            dt.Rows[i - 1]["CID"] = Convert.ToInt32(lblid.Text);

                            if (!String.IsNullOrWhiteSpace(lblsn.Text))
                            {
                                dt.Rows[i - 1]["Category_SN"] = Convert.ToInt32(lblsn.Text);
                            }
                            else dt.Rows[i - 1]["Category_SN"] = 0;
                        }
                    }


                    //for (int i = 0; i < gv.Rows.Count; i++)
                    //{
                    //    dt.Rows.Add();
                    //    dt.Rows[i]["Item_ID"] = itemId;
                    //    Label lblcid = gv.Rows[i].FindControl("lblCID") as Label;
                    //    dt.Rows[i]["CID"] = Convert.ToInt32(lblcid.Text);
                    //    Label lblSN = gv.Rows[i].FindControl("lblSN") as Label;
                    //    if (!string.IsNullOrWhiteSpace(lblSN.Text))
                    //    {
                    //        dt.Rows[i]["Category_SN"] = Convert.ToInt32(lblSN.Text);
                    //    }
                    //    else
                    //    {
                    //        dt.Rows[i]["Category_SN"] = 0;
                    //    }
                    //}
                    icbl.Insert(itemId, dt);

                    #endregion

                    #region Item_Master Update
                    Item_Master_BL imbl = new Item_Master_BL();
                    Item_Master_Entity ime = new Item_Master_Entity();

                    ime.ID = itemId;
                   // LinkButton lnk = new LinkButton();

                  Label  lnk = gvItem.Rows[rowIndex].FindControl("lnkItemNo") as Label;
                    ime.Item_Code = lnk.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtrakuten") as TextBox;
                    //if (!String.IsNullOrWhiteSpace(txt.Text))
                        ime.Rakuten_CategoryID = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtyahoo") as TextBox;
                    //if (!String.IsNullOrWhiteSpace(txt.Text))
                        ime.Yahoo_CategoryID = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtponpare") as TextBox;
                    //if (!String.IsNullOrWhiteSpace(txt.Text))
                        ime.Wowma_CategoryID = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtItemDetail") as TextBox;
                    ime.Item_Description_PC = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtsaleDetail") as TextBox;
                    ime.Sale_Description_PC = txt.Text;

                    CheckBox chk = gvItem.Rows[rowIndex].FindControl("chkRakutenEvidence") as CheckBox;
                    if (chk.Checked)
                        ime.Rakuten_evidence = 1;

                    chk = gvItem.Rows[rowIndex].FindControl("chkCloudshopMode") as CheckBox;
                    if (chk.Checked)
                        ime.Cloudshop_mode = 1;

                    imbl.Update(ime);
                    #endregion

                    #region Item_Image Update
                    Item_Image_BL imbl1 = new Item_Image_BL();

                    txt = gvItem.Rows[rowIndex].FindControl("txtLibraryImage1") as TextBox;
                    String img1 = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtLibraryImage2") as TextBox;
                    String img2 = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtLibraryImage3") as TextBox;
                    String img3 = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtLibraryImage4") as TextBox;
                    String img4 = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtLibraryImage5") as TextBox;
                    String img5 = txt.Text;

                    txt = gvItem.Rows[rowIndex].FindControl("txtLibraryImage6") as TextBox;
                    String img6 = txt.Text;

                    imbl1.UpdateImage(itemId, img1, img2, img3, img4, img5, img6);
                    #endregion

                    #region ItemRelated Update
                    Item_Related_Item_BL irbl = new Item_Related_Item_BL();
                    dt = new DataTable();
                    dt.Columns.Add("Related_ItemCode", typeof(String));
                    dt.Columns.Add("SN", typeof(int));

                    txt = gvItem.Rows[rowIndex].FindControl("txtRelatedItem1") as TextBox;
                    if (!String.IsNullOrWhiteSpace(txt.Text))
                        dt.Rows.Add(txt.Text,1);

                    txt = gvItem.Rows[rowIndex].FindControl("txtRelatedItem2") as TextBox;
                    if (!String.IsNullOrWhiteSpace(txt.Text))
                        dt.Rows.Add(txt.Text,2);

                    txt = gvItem.Rows[rowIndex].FindControl("txtRelatedItem3") as TextBox;
                    if (!String.IsNullOrWhiteSpace(txt.Text))
                        dt.Rows.Add(txt.Text,3);

                    txt = gvItem.Rows[rowIndex].FindControl("txtRelatedItem4") as TextBox;
                    if (!String.IsNullOrWhiteSpace(txt.Text))
                        dt.Rows.Add(txt.Text,4);

                    txt = gvItem.Rows[rowIndex].FindControl("txtRelatedItem5") as TextBox;
                    if (!String.IsNullOrWhiteSpace(txt.Text))
                        dt.Rows.Add(txt.Text,5);

                    txt = gvItem.Rows[rowIndex].FindControl("txtRelatedItem6") as TextBox;
                    if (!String.IsNullOrWhiteSpace(txt.Text))
                        dt.Rows.Add(txt.Text,6);

                    irbl.Insert(itemId, dt);

                    #endregion

                    #region YahooSpecValue Update
                    Item_YahooSpecificValue_BL YahooSpecificValueBL = new Item_YahooSpecificValue_BL();
                    dt = Session["YahooSpecificValue"] as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        YahooSpecificValueBL.Insert(itemId, dt);
                    }
                    else
                    {
                        dt = new DataTable();
                        DataRow dr1 = null;
                        dt.Columns.Add(new DataColumn("Spec_ID", typeof(string)));
                        dt.Columns.Add(new DataColumn("Spec_Name", typeof(string)));
                        dt.Columns.Add(new DataColumn("Spec_ValueID", typeof(string)));
                        dt.Columns.Add(new DataColumn("Spec_ValueName", typeof(string)));
                        dt.Columns.Add(new DataColumn("Type", typeof(int)));

                        dr1 = dt.NewRow();
                        dr1["Spec_ID"] = "";
                        dr1["Spec_Name"] = "";
                        dr1["Spec_ValueID"] = "";
                        dr1["Spec_ValueName"] = "";
                        dr1["Type"] = 1;
                        dt.Rows.Add(dr1);

                        YahooSpecificValueBL.Insert(itemId, dt);
                    }

                    Session.Remove("YahooSpecificValue");
                    #endregion

                    lbl = gvItem.Rows[rowIndex].FindControl("lblLock") as Label;

                    Boolean value = true;

                    if (lbl.Text.Equals("0"))
                        value = false;

                    Lock_Unlock(value, rowIndex);

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('更新が完了');", true);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }              
        }

        protected String getCategory(String id, String category)
        {
            try
            {
                Category_BL cbl = new Category_BL();
                DataTable dt = cbl.SelectForCatalogID(Convert.ToInt32(id));
                if (dt.Rows.Count > 0)
                {
                    if (String.IsNullOrWhiteSpace(category))
                        category = dt.Rows[0]["Description"].ToString();
                    else if (!String.IsNullOrWhiteSpace(category) && !dt.Rows[0]["ParentID"].ToString().Equals("0"))
                        category = dt.Rows[0]["Description"].ToString() + "\\" + category;

                    if (!dt.Rows[0]["ParentID"].ToString().Equals("0"))
                    {
                        category = getCategory(dt.Rows[0]["ParentID"].ToString(), category);
                    }
                }
                return category;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return string.Empty;
            }
        }

        protected void setOption(DataTable dt, GridViewRowEventArgs e)
        {
            try
            {
                TextBox txt = new TextBox();
                if (dt.Rows.Count > 0)
                {
                    txt = e.Row.FindControl("txtOptionValue1") as TextBox;
                    txt.Text = dt.Rows[0]["Option_Value"].ToString();

                    txt = e.Row.FindControl("txtOptionName1") as TextBox;
                    txt.Text = dt.Rows[0]["Option_Name"].ToString();
                }

                if (dt.Rows.Count > 1)
                {
                    txt = e.Row.FindControl("txtOptionValue2") as TextBox;
                    txt.Text = dt.Rows[1]["Option_Value"].ToString();

                    txt = e.Row.FindControl("txtOptionName2") as TextBox;
                    txt.Text = dt.Rows[1]["Option_Name"].ToString();
                }

                if (dt.Rows.Count > 2)
                {
                    txt = e.Row.FindControl("txtOptionValue3") as TextBox;
                    txt.Text = dt.Rows[2]["Option_Value"].ToString();

                    txt = e.Row.FindControl("txtOptionName3") as TextBox;
                    txt.Text = dt.Rows[2]["Option_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }   
        }

        protected void setShopCategory(DataTable dt, GridViewRowEventArgs e)
        {
            try
            {
                DataTable dtCategory = new DataTable();
                dtCategory.Columns.Add("Category_SN", typeof(String));
                dtCategory.Columns.Add("CID", typeof(String));
                dtCategory.Columns.Add("CName", typeof(String));

                int j = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtCategory.Rows.Add();

                    dtCategory.Rows[j]["CName"] = getCategory(dt.Rows[i]["CID"].ToString(), String.Empty);
                    dtCategory.Rows[j]["CID"] = dt.Rows[i]["CID"].ToString();
                    dtCategory.Rows[j]["Category_SN"] = dt.Rows[i]["Category_SN"].ToString();
                    j++;
                }

                TextBox txt;
                Label lbl;

                if (dtCategory.Rows.Count > 0)
                {
                    txt = e.Row.FindControl("txtShopCategory1") as TextBox;
                    lbl = e.Row.FindControl("lblSCID1") as Label;

                    txt.Text = dtCategory.Rows[0]["CName"].ToString();
                    lbl.Text = dtCategory.Rows[0]["CID"].ToString();

                    lbl = e.Row.FindControl("lblSN1") as Label;
                    lbl.Text = dtCategory.Rows[0]["Category_SN"].ToString();
                }

                if (dtCategory.Rows.Count > 1)
                {
                    txt = e.Row.FindControl("txtShopCategory2") as TextBox;
                    lbl = e.Row.FindControl("lblSCID2") as Label;

                    txt.Text = dtCategory.Rows[1]["CName"].ToString();
                    lbl.Text = dtCategory.Rows[1]["CID"].ToString();

                    lbl = e.Row.FindControl("lblSN2") as Label;
                    lbl.Text = dtCategory.Rows[1]["Category_SN"].ToString();
                }

                if (dtCategory.Rows.Count > 2)
                {
                    txt = e.Row.FindControl("txtShopCategory3") as TextBox;
                    lbl = e.Row.FindControl("lblSCID3") as Label;

                    txt.Text = dtCategory.Rows[2]["CName"].ToString();
                    lbl.Text = dtCategory.Rows[2]["CID"].ToString();

                    lbl = e.Row.FindControl("lblSN3") as Label;
                    lbl.Text = dtCategory.Rows[2]["Category_SN"].ToString();
                }

                if (dtCategory.Rows.Count > 3)
                {
                    txt = e.Row.FindControl("txtShopCategory4") as TextBox;
                    lbl = e.Row.FindControl("lblSCID4") as Label;

                    txt.Text = dtCategory.Rows[3]["CName"].ToString();
                    lbl.Text = dtCategory.Rows[3]["CID"].ToString();

                    lbl = e.Row.FindControl("lblSN4") as Label;
                    lbl.Text = dtCategory.Rows[3]["Category_SN"].ToString();
                }

                if (dtCategory.Rows.Count > 4)
                {
                    txt = e.Row.FindControl("txtShopCategory5") as TextBox;
                    lbl = e.Row.FindControl("lblSCID5") as Label;

                    txt.Text = dtCategory.Rows[4]["CName"].ToString();
                    lbl.Text = dtCategory.Rows[4]["CID"].ToString();

                    lbl = e.Row.FindControl("lblSN5") as Label;
                    lbl.Text = dtCategory.Rows[4]["Category_SN"].ToString();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void setMallCategory(Item_Master_Entity ime, GridViewRowEventArgs e)
        {
            try
            {
                TextBox txt = new TextBox();
                Mall_Category_BL mcbl = new Mall_Category_BL();
                DataTable dt = new DataTable();
                Label lbl = new Label();

                txt = e.Row.FindControl("txtrakuten") as TextBox;
                lbl = e.Row.FindControl("lblRakutenCategoryID") as Label;
                /*
                if (ime.Rakuten_CategoryID != 0)
                {
                    txt.Text = ime.Rakuten_CategoryID.ToString();
                    lbl.Text = ime.Rakuten_CategoryID.ToString();
                }
                else
                { 
                     txt.Text ="";
                     lbl.Text = "";
                }
                 */
                //dt = mcbl.SelectByID(ime.Rakuten_CategoryID);
                //if (dt.Rows.Count > 0)
                //{
                //    //txt.Text = dt.Rows[0]["Category_Path"].ToString();
                //    txt.Text = dt.Rows[0]["ID"].ToString();
                //    lbl.Text = dt.Rows[0]["ID"].ToString();
                //}
                //Button btn = e.Row.FindControl("btnSpec") as Button;
                txt = e.Row.FindControl("txtyahoo") as TextBox;
                lbl = e.Row.FindControl("lblYahooCategoryID") as Label;
                /*
                if (ime.Yahoo_CategoryID != 0 && !string.IsNullOrWhiteSpace(ime.Yahoo_CategoryID.ToString()))
                {
                    
                    txt.Text = ime.Yahoo_CategoryID.ToString();
                    lbl.Text = ime.Yahoo_CategoryID.ToString();
                    //if (CheckExistYahooValue(txt.Text))
                    //{
                    //    btn.Enabled = true;
                    //}
                    //else
                    //{
                    //    btn.Enabled = false;
                    //}
                }
                else
                {
                    //btn.Enabled = false;
                    txt.Text = "";
                    lbl.Text = "";
                }
                 */
                //dt = mcbl.SelectByID(ime.Yahoo_CategoryID);
                //if (dt.Rows.Count > 0)
                //{
                //    //txt.Text = dt.Rows[0]["Category_Path"].ToString();
                //    txt.Text = dt.Rows[0]["ID"].ToString();
                //    lbl.Text = dt.Rows[0]["ID"].ToString();
                //}

                txt = e.Row.FindControl("txtponpare") as TextBox;
                lbl = e.Row.FindControl("lblPonpareCategoryID") as Label;
                /*
                if (ime.Ponpare_CategoryID != 0)
                {
                txt.Text = ime.Ponpare_CategoryID.ToString();
                lbl.Text = ime.Ponpare_CategoryID.ToString();
                }
                else
                {
                    txt.Text = "";
                    lbl.Text = "";
                }
                 */
                //dt = mcbl.SelectByID(ime.Ponpare_CategoryID);
                //if (dt.Rows.Count > 0)
                //{
                //    //txt.Text = dt.Rows[0]["Category_Path"].ToString();
                //    txt.Text = dt.Rows[0]["ID"].ToString();
                //    lbl.Text = dt.Rows[0]["ID"].ToString();
                //}
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnClearLog_Click(String index1)
        {
            try
            {
                int index = Convert.ToInt32(index1);

                HtmlGenericControl PLock = gvItem.Rows[index].FindControl("PLock") as HtmlGenericControl;
                PLock.Visible = false;

                Lock_Unlock(false, index);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void Lock_Unlock(Boolean value, int index)
        {
            try
            {
                HtmlGenericControl PLock = gvItem.Rows[index].FindControl("PLock") as HtmlGenericControl;
                PLock.Visible = value;

                Button btn;

                ListBox lst = gvItem.Rows[index].FindControl("lstSize") as ListBox;
                lst.Enabled = !value;
                lst = gvItem.Rows[index].FindControl("lstColor") as ListBox;
                lst.Enabled = !value;

                TextBox txt = gvItem.Rows[index].FindControl("txtLibraryImage1") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtLibraryImage2") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtLibraryImage3") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtLibraryImage4") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtLibraryImage5") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtLibraryImage6") as TextBox;
                txt.ReadOnly = value;

                txt = gvItem.Rows[index].FindControl("txtsaleDetail") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtItemDetail") as TextBox;
                txt.ReadOnly = value;

                //btn = gvItem.Rows[index].FindControl("btnOption") as Button;
                //btn.Enabled = !value;
                HtmlInputButton hbtn = gvItem.Rows[index].FindControl("btnOption") as HtmlInputButton;
                hbtn.Disabled = value;

                hbtn = gvItem.Rows[index].FindControl("btnPhotoSave") as HtmlInputButton;
                hbtn.Disabled = value;


                txt = gvItem.Rows[index].FindControl("txtOptionName1") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtOptionName2") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtOptionName3") as TextBox;
                txt.ReadOnly = value;

                txt = gvItem.Rows[index].FindControl("txtOptionValue1") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtOptionValue2") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtOptionValue3") as TextBox;
                txt.ReadOnly = value;

                btn = gvItem.Rows[index].FindControl("btnShopCategory") as Button;
                btn.Enabled = !value;
                //hbtn = gvItem.Rows[index].FindControl("btnShopCategory") as HtmlInputButton;
                //hbtn.Disabled = value;


                txt = gvItem.Rows[index].FindControl("txtrakuten") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtyahoo") as TextBox;
                txt.ReadOnly = value;
                if (CheckExistYahooValue(txt.Text))
                {
                    btn = gvItem.Rows[index].FindControl("btnSpec") as Button;
                    btn.Enabled = true;
                }
                else
                {
                    btn = gvItem.Rows[index].FindControl("btnSpec") as Button;
                    btn.Enabled = false;
                }

                if(value == true)
                {
                    btn = gvItem.Rows[index].FindControl("btnSpec") as Button;
                    btn.Enabled = false;
                }
                
                txt = gvItem.Rows[index].FindControl("txtponpare") as TextBox;
                txt.ReadOnly = value;

                hbtn = gvItem.Rows[index].FindControl("btnRakuten") as HtmlInputButton;
                hbtn.Disabled = value;

                hbtn = gvItem.Rows[index].FindControl("btnYahoo") as HtmlInputButton;
                hbtn.Disabled = value;

                hbtn = gvItem.Rows[index].FindControl("btnPonpare") as HtmlInputButton;
                hbtn.Disabled = value;

                //btn = gvItem.Rows[index].FindControl("btnRakuten") as Button;
                //btn.Enabled = !value;
                //btn = gvItem.Rows[index].FindControl("btnYahoo") as Button;
                //btn.Enabled = !value;
                //btn = gvItem.Rows[index].FindControl("btnPonpare") as Button;
                //btn.Enabled = !value;

                //hbtn = gvItem.Rows[index].FindControl("btnSpec") as HtmlInputButton;
                //hbtn.Disabled = value;

                txt = gvItem.Rows[index].FindControl("txtSpec1") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtSpec2") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtSpec3") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtSpec4") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtSpec5") as TextBox;
                txt.ReadOnly = value;
                //if (CheckExistYahooValue(txt.Text))
                //{
                //    btn = gvItem.Rows[index].FindControl("btnSpec") as Button;
                //    btn.Enabled = true;
                //}
                //else
                //{
                //    btn = gvItem.Rows[index].FindControl("btnSpec") as Button;
                //    btn.Enabled = false;
                //}

                txt = gvItem.Rows[index].FindControl("txtRelatedItem1") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtRelatedItem2") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtRelatedItem3") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtRelatedItem4") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtRelatedItem5") as TextBox;
                txt.ReadOnly = value;
                txt = gvItem.Rows[index].FindControl("txtRelatedItem6") as TextBox;
                txt.ReadOnly = value;

                HtmlInputButton hib = gvItem.Rows[index].FindControl("btnUpdate") as HtmlInputButton;
                hib.Disabled = value;
                hib = gvItem.Rows[index].FindControl("btnFinish") as HtmlInputButton;
                hib.Disabled = value;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }           
        }

        protected void Lock_Unlock(Boolean value, GridViewRowEventArgs e)
        {
            try
            {
                Button btn; //= e.Row.FindControl("btnPhotoSave") as Button;
                //btn.Enabled = !value;

                ListBox lst = e.Row.FindControl("lstSize") as ListBox;
                lst.Enabled = !value;
                lst = e.Row.FindControl("lstColor") as ListBox;
                lst.Enabled = !value;

                TextBox txt = e.Row.FindControl("txtLibraryImage1") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtLibraryImage2") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtLibraryImage3") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtLibraryImage4") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtLibraryImage5") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtLibraryImage6") as TextBox;
                txt.ReadOnly = value;

                txt = e.Row.FindControl("txtsaleDetail") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtItemDetail") as TextBox;
                txt.ReadOnly = value;

                HtmlInputButton hbtn = e.Row.FindControl("btnOption") as HtmlInputButton;
                hbtn.Disabled = value;

                hbtn = e.Row.FindControl("btnPhotoSave") as HtmlInputButton;
                hbtn.Disabled = value;
                //btn = e.Row.FindControl("btnOption") as Button;
                //btn.Enabled = !value;

                txt = e.Row.FindControl("txtOptionName1") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtOptionName2") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtOptionName3") as TextBox;
                txt.ReadOnly = value;

                txt = e.Row.FindControl("txtOptionValue1") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtOptionValue2") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtOptionValue3") as TextBox;
                txt.ReadOnly = value;

                //btn = e.Row.FindControl("btnShopCategory") as Button;
                //btn.Enabled = !value;
                //btn = e.Row.FindControl("btnShopCategory") as Button;
                //btn.Enabled = !value;

                btn = e.Row.FindControl("btnShopCategory") as Button;
                btn.Enabled = !value;

                txt = e.Row.FindControl("txtrakuten") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtyahoo") as TextBox;
                txt.ReadOnly = value;
                if (CheckExistYahooValue(txt.Text))
                {
                    btn =e.Row.FindControl("btnSpec") as Button;
                    btn.Enabled = true;
                }
                else
                {
                    btn = e.Row.FindControl("btnSpec") as Button;
                    btn.Enabled = false;
                }

                if(value == true)
                {
                    btn = e.Row.FindControl("btnSpec") as Button;
                    btn.Enabled = false;
                }

                txt = e.Row.FindControl("txtponpare") as TextBox;
                txt.ReadOnly = value;

                hbtn = e.Row.FindControl("btnRakuten") as HtmlInputButton;
                hbtn.Disabled = value;

                hbtn = e.Row.FindControl("btnYahoo") as HtmlInputButton;
                hbtn.Disabled = value;

                hbtn = e.Row.FindControl("btnPonpare") as HtmlInputButton;
                hbtn.Disabled = value;

                //btn = e.Row.FindControl("btnRakuten") as Button;
                //btn.Enabled = !value;
                //btn = e.Row.FindControl("btnYahoo") as Button;
                //btn.Enabled = !value;
                //btn = e.Row.FindControl("btnPonpare") as Button;
                //btn.Enabled = !value;

                //hbtn = e.Row.FindControl("btnSpec") as HtmlInputButton;
                //hbtn.Disabled = value;

                

                //if (string.IsNullOrWhiteSpace((e.Row.FindControl("txtSpec1") as TextBox).ToString()) ||
                //    string.IsNullOrWhiteSpace((e.Row.FindControl("txtSpec2") as TextBox).ToString()) ||
                //    string.IsNullOrWhiteSpace((e.Row.FindControl("txtSpec3") as TextBox).ToString()) ||
                //    string.IsNullOrWhiteSpace((e.Row.FindControl("txtSpec4") as TextBox).ToString()) ||
                //    string.IsNullOrWhiteSpace((e.Row.FindControl("txtSpec5") as TextBox).ToString()))
                //{
                //    btn = e.Row.FindControl("btnSpec") as Button;
                //    btn.Enabled = value;
                //}
                //else
                //{
                    //btn = e.Row.FindControl("btnSpec") as Button;
                    //btn.Enabled = !value;
               //}

                txt = e.Row.FindControl("txtSpec1") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtSpec2") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtSpec3") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtSpec4") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtSpec5") as TextBox;
                txt.ReadOnly = value;

                txt = e.Row.FindControl("txtRelatedItem1") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtRelatedItem2") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtRelatedItem3") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtRelatedItem4") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtRelatedItem5") as TextBox;
                txt.ReadOnly = value;
                txt = e.Row.FindControl("txtRelatedItem6") as TextBox;
                txt.ReadOnly = value;

                HtmlInputButton hib = e.Row.FindControl("btnUpdate") as HtmlInputButton;
                hib.Disabled = value;
                hib = e.Row.FindControl("btnFinish") as HtmlInputButton;
                hib.Disabled = value;
            }
            catch (Exception ex)
            {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label l1 = e.Row.FindControl("lblID") as Label;

                    HtmlInputButton hbtn = e.Row.FindControl("btnOption") as HtmlInputButton;
                    hbtn.Attributes.Add("onclick", "javascript:OptionPopUp(" + l1.Text + "," + hbtn.ClientID + ")");

                    hbtn = e.Row.FindControl("btnPhotoSave") as HtmlInputButton;
                    hbtn.Attributes.Add("onclick", "javascript:ImagePopUp(" + l1.Text + "," + hbtn.ClientID + ")");

                    //btn = e.Row.FindControl("btnOption") as Button;
                    //btn.Attributes.Add("onclick", "javascript:OptionPopUp(" + l1.Text +","+btn.ClientID+ ")");

                    //hbtn = e.Row.FindControl("btnSpec") as HtmlInputButton;
                    //hbtn.Attributes.Add("onclick", "javascript:SpecPopUp(" + l1.Text + "," + hbtn.ClientID + ")");

                    //hbtn = e.Row.FindControl("btnSpec") as Button;
                    //hbtn.Attributes.Add("onclick", "javascript:SpecPopUp(" + l1.Text + "," + hbtn.ClientID + ")");

                    //hbtn = e.Row.FindControl("btnShopCategory") as HtmlInputButton;
                    //hbtn.Attributes.Add("onclick", "javascript:ShopCategoryPopUp(" + l1.Text + "," + hbtn.ClientID + ")");
                    //btn = e.Row.FindControl("btnShopCategory") as Button;
                    //btn.Attributes.Add("onclick", "javascript:ShopCategoryPopUp(" + l1.Text + "," + btn.ClientID + ")");

                    hbtn = e.Row.FindControl("btnRakuten") as HtmlInputButton;
                    hbtn.Attributes.Add("onclick", "javascript:MallCategoryPopUp(" + 1 + "," + l1.Text + "," + hbtn.ClientID + ")");

                    hbtn = e.Row.FindControl("btnYahoo") as HtmlInputButton;
                    hbtn.Attributes.Add("onclick", "javascript:MallCategoryPopUp(" + 2 + "," + l1.Text + "," + hbtn.ClientID + ")");

                    hbtn = e.Row.FindControl("btnPonpare") as HtmlInputButton;
                    hbtn.Attributes.Add("onclick", "javascript:MallCategoryPopUp(" + 3 + "," + l1.Text + "," + hbtn.ClientID + ")");

                    //btn = e.Row.FindControl("btnPreview") as Button;
                    //btn.OnClientClick = "target='_blank'";

                    if (!String.IsNullOrWhiteSpace(l1.Text))
                    {
                        DataTable dt = new DataTable();
                        TextBox txt = new TextBox();

                        #region ItemDescription,SaleDescription
                        Item_Master_BL imbl1 = new Item_Master_BL();
                        dt = imbl1.GetItemSaleDescription(l1.Text);
                        if (dt.Rows.Count > 0)
                        {
                            txt = e.Row.FindControl("txtsaleDetail") as TextBox;
                            txt.Text = dt.Rows[0]["Sale_Description_PC"].ToString();

                            txt = e.Row.FindControl("txtItemDetail") as TextBox;
                            txt.Text = dt.Rows[0]["Item_Description_PC"].ToString();
                        }

                        #endregion

                        #region image
                        Item_Master_BL imbl = new Item_Master_BL();
                        dt = imbl.SelectItemImage(l1.Text);
                        if (dt != null)
                        {
                            string imagePath = ConfigurationManager.AppSettings["ItemImage"].ToString();
                            Image img = new Image();
                            DataRow[] drImage = dt.Select("Image_Type = 0");
                            if (drImage.Count() > 0)
                            {
                                DataTable dtItem = dt.Select("Image_Type = 0").CopyToDataTable();
                                //DataList dlImage = e.Row.FindControl("dlImage") as DataList;
                                //dlImage.DataSource = dtItem;
                                //dlImage.DataBind();

                                //for (int k = 1; k <= 5; k++)
                                //{
                                //    img = e.Row.FindControl("imgItem" + k) as Image;
                                //    img.ImageUrl = imagePath + "not-img.png";
                                //}

                                int i = 0;
                                int row = 0;
                                while (row < 5)
                                {
                                    if (row >= dtItem.Rows.Count)
                                        break;
                                    if (!String.IsNullOrWhiteSpace(dtItem.Rows[row]["Image_Name"].ToString()))
                                    {
                                        img = e.Row.FindControl("imgItem" + (i + 1)) as Image;
                                        img.ImageUrl = imagePath + dtItem.Rows[row]["Image_Name"].ToString();
                                        i++;
                                    }
                                    row++;
                                }


                                //img = e.Row.FindControl("imgItem1") as Image;
                                //img.ImageUrl = imagePath + dtItem.Rows[0]["Image_Name"].ToString();

                                //if (dtItem.Rows.Count > 1)
                                //{
                                //    img = e.Row.FindControl("imgItem2") as Image;
                                //    img.ImageUrl = imagePath + dtItem.Rows[1]["Image_Name"].ToString();
                                //}

                                //if (dtItem.Rows.Count > 2)
                                //{
                                //    img = e.Row.FindControl("imgItem3") as Image;
                                //    img.ImageUrl = imagePath + dtItem.Rows[2]["Image_Name"].ToString();
                                //}

                                //if (dtItem.Rows.Count > 3)
                                //{
                                //    img = e.Row.FindControl("imgItem4") as Image;
                                //    img.ImageUrl = imagePath + dtItem.Rows[3]["Image_Name"].ToString();
                                //}

                                //if (dtItem.Rows.Count > 4)
                                //{
                                //    img = e.Row.FindControl("imgItem5") as Image;
                                //    img.ImageUrl = imagePath + dtItem.Rows[4]["Image_Name"].ToString();
                                //}

                            }

                            DataRow[] drLibraryImage = dt.Select("Image_Type=1");
                            if (drLibraryImage.Count() > 0)
                            {
                                DataTable dtImage = dt.Select("Image_Type=1").CopyToDataTable();

                                txt = e.Row.FindControl("txtLibraryImage1") as TextBox;
                                txt.Text = dtImage.Rows[0]["Image_Name"].ToString();

                                if (dtImage.Rows.Count > 1)
                                {
                                    txt = e.Row.FindControl("txtLibraryImage2") as TextBox;
                                    txt.Text = dtImage.Rows[1]["Image_Name"].ToString();
                                }

                                if (dtImage.Rows.Count > 2)
                                {
                                    txt = e.Row.FindControl("txtLibraryImage3") as TextBox;
                                    txt.Text = dtImage.Rows[2]["Image_Name"].ToString();
                                }

                                if (dtImage.Rows.Count > 3)
                                {
                                    txt = e.Row.FindControl("txtLibraryImage4") as TextBox;
                                    txt.Text = dtImage.Rows[3]["Image_Name"].ToString();
                                }

                                if (dtImage.Rows.Count > 4)
                                {
                                    txt = e.Row.FindControl("txtLibraryImage5") as TextBox;
                                    txt.Text = dtImage.Rows[4]["Image_Name"].ToString();
                                }

                                if (dtImage.Rows.Count > 5)
                                {
                                    txt = e.Row.FindControl("txtLibraryImage6") as TextBox;
                                    txt.Text = dtImage.Rows[5]["Image_Name"].ToString();
                                }
                            }
                        }
                        #endregion

                        #region sku
                        Item_BL itemBL = new Item_BL();
                        //DataTable dtSKUColor = itemBL.SelectSKUColor(DataBinder.Eval(e.Row.DataItem, "Item_Code").ToString());
                        DataTable dtSKUSize = itemBL.SelectSKUSize(DataBinder.Eval(e.Row.DataItem, "Item_Code").ToString());

                        ListBox lstSize = new ListBox();
                        ListBox lstColor = new ListBox();

                        if (dtSKUSize != null)
                        {
                            lstSize = e.Row.FindControl("lstSize") as ListBox;
                            lstSize.DataSource = dtSKUSize;
                            lstSize.DataTextField = "正式名称";
                            lstSize.DataValueField = "略称";
                            lstSize.DataBind();
                        }
                        if (dtSKUSize != null)
                        {
                            lstColor = e.Row.FindControl("lstColor") as ListBox;
                            lstColor.DataSource = dtSKUSize;
                            //lstColor.DataTextField = "Color_Name";
                            //lstColor.DataValueField = "Color_Code";

                            lstColor.DataTextField = "正式名称";
                            lstColor.DataValueField = "略称";
                            lstColor.DataBind();
                        }
                        #endregion

                        #region sks status
                        Label lbl = e.Row.FindControl("lblSksStatusID") as Label;
                        Label lblSksStatus = e.Row.FindControl("lblSksStatus") as Label;
                        CheckBox ckb = e.Row.FindControl("chkItem") as CheckBox;

                        HtmlGenericControl Ppage = e.Row.FindControl("Ppage") as HtmlGenericControl;
                        HtmlGenericControl PWaitSt = e.Row.FindControl("PWaitSt") as HtmlGenericControl;
                        HtmlGenericControl PWaitL = e.Row.FindControl("PWaitL") as HtmlGenericControl;
                        HtmlGenericControl POkSt = e.Row.FindControl("POkSt") as HtmlGenericControl;

                        switch (lbl.Text)
                        {
                            case "1":
                                {
                                    Ppage.Visible = true;
                                    PWaitSt.Visible = false;
                                    PWaitL.Visible = false;
                                    POkSt.Visible = false;
                                    break;
                                }
                            case "3":
                                {
                                    Ppage.Visible = false;
                                    PWaitSt.Visible = true;
                                    PWaitL.Visible = false;
                                    POkSt.Visible = false;
                                    break;

                                }
                            case "2":
                                {
                                    Ppage.Visible = false;
                                    PWaitSt.Visible = false;
                                    PWaitL.Visible = true;
                                    POkSt.Visible = false;
                                    ckb.Enabled = false;
                                    break;
                                }
                            case "4":
                                {
                                    Ppage.Visible = false;
                                    PWaitSt.Visible = false;
                                    PWaitL.Visible = false;
                                    POkSt.Visible = true;
                                    break;

                                }
                        }

                        #endregion

                        #region option
                        Item_Option_BL iobl = new Item_Option_BL();
                        DataTable dtOption = iobl.SelectByItemID(Convert.ToInt32(l1.Text));
                        setOption(dtOption, e);

                        #endregion

                        #region shop category

                        Item_Category_BL icbl = new Item_Category_BL();
                        DataTable dtShopCategory = icbl.SelectByItemID(Convert.ToInt32(l1.Text));

                        if (dtShopCategory.Rows.Count > 0)
                        {
                            txt = e.Row.FindControl("txtShopCategory1") as TextBox;
                            lbl = e.Row.FindControl("lblSCID1") as Label;

                            txt.Text = dtShopCategory.Rows[0]["Path"].ToString();
                            txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1).ToString();
                            lbl.Text = dtShopCategory.Rows[0]["CID"].ToString();

                            lbl = e.Row.FindControl("lblSN1") as Label;
                            lbl.Text = dtShopCategory.Rows[0]["Category_SN"].ToString();
                        }

                        if (dtShopCategory.Rows.Count > 1)
                        {
                            txt = e.Row.FindControl("txtShopCategory2") as TextBox;
                            lbl = e.Row.FindControl("lblSCID2") as Label;

                            txt.Text = dtShopCategory.Rows[1]["Path"].ToString();
                            txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1).ToString();
                            lbl.Text = dtShopCategory.Rows[1]["CID"].ToString();

                            lbl = e.Row.FindControl("lblSN2") as Label;
                            lbl.Text = dtShopCategory.Rows[1]["Category_SN"].ToString();
                        }

                        if (dtShopCategory.Rows.Count > 2)
                        {
                            txt = e.Row.FindControl("txtShopCategory3") as TextBox;
                            lbl = e.Row.FindControl("lblSCID3") as Label;

                            txt.Text = dtShopCategory.Rows[2]["Path"].ToString();
                            txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1).ToString();
                            lbl.Text = dtShopCategory.Rows[2]["CID"].ToString();

                            lbl = e.Row.FindControl("lblSN3") as Label;
                            lbl.Text = dtShopCategory.Rows[2]["Category_SN"].ToString();
                        }

                        if (dtShopCategory.Rows.Count > 3)
                        {
                            txt = e.Row.FindControl("txtShopCategory4") as TextBox;
                            lbl = e.Row.FindControl("lblSCID4") as Label;

                            txt.Text = dtShopCategory.Rows[3]["Path"].ToString();
                            txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1).ToString();
                            lbl.Text = dtShopCategory.Rows[3]["CID"].ToString();

                            lbl = e.Row.FindControl("lblSN4") as Label;
                            lbl.Text = dtShopCategory.Rows[3]["Category_SN"].ToString();
                        }

                        if (dtShopCategory.Rows.Count > 4)
                        {
                            txt = e.Row.FindControl("txtShopCategory5") as TextBox;
                            lbl = e.Row.FindControl("lblSCID5") as Label;

                            txt.Text = dtShopCategory.Rows[4]["Path"].ToString();
                            txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1).ToString();
                            lbl.Text = dtShopCategory.Rows[4]["CID"].ToString();

                            lbl = e.Row.FindControl("lblSN5") as Label;
                            lbl.Text = dtShopCategory.Rows[4]["Category_SN"].ToString();
                        }

                        //setShopCategory(dtShopCategory, e);

                        #endregion

                        #region mall category

                        Item_Master_Entity ime = new Item_Master_Entity();
                        ime = imbl.SelectByID(Convert.ToInt32(l1.Text));
                        //ime = imbl.SelectMallByID(Convert.ToInt32(l1.Text));

                        setMallCategory(ime, e);
                        #endregion

                        #region YahooSpec
                        Item_YahooSpecificValue_BL iybl = new Item_YahooSpecificValue_BL();
                        dt = iybl.SelectByItemID(Convert.ToInt32(l1.Text));

                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                //lbl = e.Row.FindControl("lblName1") as Label;
                                //lbl.Text = dt.Rows[0]["Name"].ToString();

                                lbl = e.Row.FindControl("lblSpec_ID1") as Label;
                                lbl.Text = dt.Rows[0]["Spec_ID"].ToString();

                                lbl = e.Row.FindControl("lblSpec_Name1") as Label;
                                lbl.Text = dt.Rows[0]["Spec_Name"].ToString();

                                lbl = e.Row.FindControl("lblSpec_ValueID1") as Label;
                                lbl.Text = dt.Rows[0]["Spec_ValueID"].ToString();

                                txt = e.Row.FindControl("txtSpec1") as TextBox;
                                txt.Text = dt.Rows[0]["Spec_ValueName"].ToString();
                            }

                            if (dt.Rows.Count > 1)
                            {
                                //lbl = e.Row.FindControl("lblName1") as Label;
                                //lbl.Text = dt.Rows[0]["Name"].ToString();

                                lbl = e.Row.FindControl("lblSpec_ID2") as Label;
                                lbl.Text = dt.Rows[1]["Spec_ID"].ToString();

                                lbl = e.Row.FindControl("lblSpec_Name2") as Label;
                                lbl.Text = dt.Rows[1]["Spec_Name"].ToString();

                                lbl = e.Row.FindControl("lblSpec_ValueID2") as Label;
                                lbl.Text = dt.Rows[1]["Spec_ValueID"].ToString();

                                txt = e.Row.FindControl("txtSpec2") as TextBox;
                                txt.Text = dt.Rows[1]["Spec_ValueName"].ToString();
                            }

                            if (dt.Rows.Count > 2)
                            {
                                //lbl = e.Row.FindControl("lblName1") as Label;
                                //lbl.Text = dt.Rows[0]["Name"].ToString();

                                lbl = e.Row.FindControl("lblSpec_ID3") as Label;
                                lbl.Text = dt.Rows[2]["Spec_ID"].ToString();

                                lbl = e.Row.FindControl("lblSpec_Name3") as Label;
                                lbl.Text = dt.Rows[2]["Spec_Name"].ToString();

                                lbl = e.Row.FindControl("lblSpec_ValueID3") as Label;
                                lbl.Text = dt.Rows[2]["Spec_ValueID"].ToString();

                                txt = e.Row.FindControl("txtSpec3") as TextBox;
                                txt.Text = dt.Rows[2]["Spec_ValueName"].ToString();
                            }

                            if (dt.Rows.Count > 3)
                            {
                                //lbl = e.Row.FindControl("lblName1") as Label;
                                //lbl.Text = dt.Rows[0]["Name"].ToString();

                                lbl = e.Row.FindControl("lblSpec_ID4") as Label;
                                lbl.Text = dt.Rows[3]["Spec_ID"].ToString();

                                lbl = e.Row.FindControl("lblSpec_Name4") as Label;
                                lbl.Text = dt.Rows[3]["Spec_Name"].ToString();

                                lbl = e.Row.FindControl("lblSpec_ValueID4") as Label;
                                lbl.Text = dt.Rows[3]["Spec_ValueID"].ToString();

                                txt = e.Row.FindControl("txtSpec4") as TextBox;
                                txt.Text = dt.Rows[3]["Spec_ValueName"].ToString();
                            }

                            if (dt.Rows.Count > 4)
                            {
                                //lbl = e.Row.FindControl("lblName1") as Label;
                                //lbl.Text = dt.Rows[0]["Name"].ToString();

                                lbl = e.Row.FindControl("lblSpec_ID5") as Label;
                                lbl.Text = dt.Rows[4]["Spec_ID"].ToString();

                                lbl = e.Row.FindControl("lblSpec_Name5") as Label;
                                lbl.Text = dt.Rows[4]["Spec_Name"].ToString();

                                lbl = e.Row.FindControl("lblSpec_ValueID5") as Label;
                                lbl.Text = dt.Rows[4]["Spec_ValueID"].ToString();

                                txt = e.Row.FindControl("txtSpec5") as TextBox;
                                txt.Text = dt.Rows[4]["Spec_ValueName"].ToString();
                            }
                        }

                        #endregion

                        #region related item
                        Item_Related_Item_BL ribl = new Item_Related_Item_BL();
                        dt = ribl.SelectByItemID(Convert.ToInt32(l1.Text));

                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                txt = e.Row.FindControl("txtRelatedItem1") as TextBox;
                                txt.Text = dt.Rows[0]["Related_ItemCode"].ToString();
                            }

                            if (dt.Rows.Count > 1)
                            {
                                txt = e.Row.FindControl("txtRelatedItem2") as TextBox;
                                txt.Text = dt.Rows[1]["Related_ItemCode"].ToString();
                            }

                            if (dt.Rows.Count > 2)
                            {
                                txt = e.Row.FindControl("txtRelatedItem3") as TextBox;
                                txt.Text = dt.Rows[2]["Related_ItemCode"].ToString();
                            }

                            if (dt.Rows.Count > 3)
                            {
                                txt = e.Row.FindControl("txtRelatedItem4") as TextBox;
                                txt.Text = dt.Rows[3]["Related_ItemCode"].ToString();
                            }

                            if (dt.Rows.Count > 4)
                            {
                                txt = e.Row.FindControl("txtRelatedItem5") as TextBox;
                                txt.Text = dt.Rows[4]["Related_ItemCode"].ToString();
                            }

                            if (dt.Rows.Count > 5)
                            {
                                txt = e.Row.FindControl("txtRelatedItem6") as TextBox;
                                txt.Text = dt.Rows[5]["Related_ItemCode"].ToString();
                            }
                        }
                        #endregion

                        #region shop status
                        lbl = e.Row.FindControl("lblShop_StatusID") as Label;
                        Label lblShopStatus = e.Row.FindControl("lblShop_Status") as Label;

                        Image imgLog1 = e.Row.FindControl("imgLog1") as Image;

                        HtmlGenericControl pWait = e.Row.FindControl("PWait") as HtmlGenericControl;
                        HtmlGenericControl pOk = e.Row.FindControl("POk") as HtmlGenericControl;
                        HtmlGenericControl pDel = e.Row.FindControl("PDel") as HtmlGenericControl;

                        switch (lbl.Text)
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

                        #endregion

                        #region
                        HtmlGenericControl plock = e.Row.FindControl("PLock") as HtmlGenericControl;
                        lbl = e.Row.FindControl("lblLock") as Label;
                        if (lbl.Text.Equals("0"))
                        {
                            plock.Visible = false;
                            Lock_Unlock(false, e);
                        }
                        else
                        {
                            plock.Visible = true;
                            Lock_Unlock(true, e);
                        }
                        #endregion

                        #region Shop
                        Item_Shop_BL itemShopBL = new Item_Shop_BL();
                        DataTable dtShop = itemShopBL.SelectShopID(Convert.ToInt32(l1.Text));

                        Label lblShopCheck = e.Row.FindControl("lblShopExist") as Label;
                        if (dtShop.Rows.Count > 0)
                            lblShopCheck.Text = "1";
                        else lblShopCheck.Text = "0";

                        BindShop(dtShop, e);
                        #endregion

                    }
                }
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
                String rakuten = "http://item.rakuten.co.jp/";
                String yahoo = "http://store.shopping.yahoo.co.jp/";
                String ponpare = "http://store.ponparemall.com/";

                DropDownList ddl = gvItem.Rows[index].FindControl("ddlShop") as DropDownList;
                //DropDownList ddl = sender as DropDownList;
                //GridViewRow gvrow = ddl.NamingContainer as GridViewRow;
                //int index = gvrow.RowIndex;

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


                Label  lnk = gvItem.Rows[index].FindControl("lnkItemNo") as Label;
                String itemCode = lnk.Text;
                hfCtrl.Value = string.Empty;
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
                else if (mallid.Equals("3"))
                {
                    ponpare += shopName + "/goods/" + itemCode + "/";
                    Response.Redirect(ponpare,false);
                    //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey1", "ShopPreview('" + ponpare + "')", true);
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

        protected void gridviewColumnShowHide(int index, Boolean showhide)
        {
            try
            {
                gvItem.Columns[index].Visible = showhide;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnPreview_Click(int index)
        {
            try
            {
                #region oldcode
                Label lbl = gvItem.Rows[index].FindControl("lblID") as Label;

                Item_Image_BL imgbl = new Item_Image_BL();
                DataTable dt = imgbl.SelectItemPhotoByItemID(Convert.ToInt32(lbl.Text), 0);
                Session["ImageList"] = dt;

                TextBox txt = gvItem.Rows[index].FindControl("txtLibraryImage1") as TextBox;
                if (String.IsNullOrWhiteSpace(txt.Text))
                {
                    txt = gvItem.Rows[index].FindControl("txtLibraryImage2") as TextBox;
                    if (String.IsNullOrWhiteSpace(txt.Text))
                    {
                        txt = gvItem.Rows[index].FindControl("txtLibraryImage3") as TextBox;
                        if (String.IsNullOrWhiteSpace(txt.Text))
                        {
                            txt = gvItem.Rows[index].FindControl("txtLibraryImage4") as TextBox;
                            if (String.IsNullOrWhiteSpace(txt.Text))
                            {
                                txt = gvItem.Rows[index].FindControl("txtLibraryImage5") as TextBox;
                            }
                        }
                    }
                }

                Session["libraryImage"] = txt.Text;

                ArrayList arrlst = new ArrayList();

                txt = gvItem.Rows[index].FindControl("txtsaleDetail") as TextBox;
                arrlst.Add(txt.Text);

                txt = gvItem.Rows[index].FindControl("txtItemDetail") as TextBox;
                arrlst.Add(txt.Text);

                txt = gvItem.Rows[index].FindControl("txtRelatedItem1") as TextBox;
                arrlst.Add(txt.Text);

                txt = gvItem.Rows[index].FindControl("txtRelatedItem2") as TextBox;
                arrlst.Add(txt.Text);

                txt = gvItem.Rows[index].FindControl("txtRelatedItem3") as TextBox;
                arrlst.Add(txt.Text);

                txt = gvItem.Rows[index].FindControl("txtRelatedItem4") as TextBox;
                arrlst.Add(txt.Text);

                txt = gvItem.Rows[index].FindControl("txtRelatedItem5") as TextBox;
                arrlst.Add(txt.Text);

                Session["Description"] = arrlst;


                //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey1", "Preview(" + lbl.Text + ");", true);
                Response.Redirect("../Item/Item_Preview_Form.aspx?ID=" + lbl.Text,false);
                #endregion
            }
            catch (Exception ex)
            {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        protected void btnSelectAll_Click()
        {
            try
            {
                ArrayList arrlst = new ArrayList();
                lblShopCheckArray.Text = String.Empty;
                for(int i=0;i<gvItem.Rows.Count;i++)
                {
                    Label lbl = gvItem.Rows[i].FindControl("lblID") as Label;
                    CheckBox chk = gvItem.Rows[i].FindControl("chkItem") as CheckBox;
                    chk.Checked = true;
                    
                    if(ViewState["checkedValue"]!=null)
                     arrlst= ViewState["checkedValue"] as ArrayList;

                    if (!arrlst.Contains(lbl.Text))
                    {
                        arrlst.Add(lbl.Text);

                        Label lblshopCheck = gvItem.Rows[i].FindControl("lblShopExist") as Label;
                        Label lblItemName = gvItem.Rows[i].FindControl("lnkItemNo") as Label;

                        if (!String.IsNullOrWhiteSpace(lblShopCheckArray.Text))
                        {
                            lblShopCheckArray.Text += ",";
                        }
                        lblShopCheckArray.Text += lbl.Text + "," + lblItemName.Text + "," + lblshopCheck.Text;
                    }

                    if (arrlst.Count > 0)
                    {
                        lblCheck.Text = "1";
                    }
                    else lblCheck.Text = "0";
                }
                ViewState["checkedValue"] = arrlst;
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
                    if (!chk.Checked)
                    {
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

                ArrayList arr = ViewState["checkedValue"] as ArrayList;
                if (arr.Count > 0)
                    lblCheck.Text = "1";
                else lblCheck.Text = "0";

                Label lblshopCheck = gvItem.Rows[rowIndex].FindControl("lblShopExist") as Label;
                Label lblItemName = gvItem.Rows[rowIndex].FindControl("lnkItemNo") as Label;


                if (chk.Checked)
                {
                    if (!lblShopCheckArray.Text.Contains(lbl.Text))
                    {
                        if (!String.IsNullOrWhiteSpace(lblShopCheckArray.Text))
                        {
                            lblShopCheckArray.Text += ",";
                        }
                        lblShopCheckArray.Text += lbl.Text + "," + lblItemName.Text + "," + lblshopCheck.Text;
                    }
                }
                else
                {
                    String[] str = lblShopCheckArray.Text.Split(',');
                    ArrayList itemList = new ArrayList();

                    for (int i = 0; i < str.Length; i += 3)
                    {
                        if (!str[i].Equals(lbl.Text))
                        {
                            itemList.Add(str[i]);
                            itemList.Add(str[i + 1]);
                            itemList.Add(str[i + 2]);
                        }
                    }
                    lblShopCheckArray.Text = String.Join(",", (String[])itemList.ToArray(Type.GetType("System.String")));
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnUnSelectAll_Click()
        {
            try
            {
                ArrayList arrlst = new ArrayList();
                for (int i = 0; i < gvItem.Rows.Count; i++)
                {
                    Label lbl = gvItem.Rows[i].FindControl("lblID") as Label;
                    CheckBox chk = gvItem.Rows[i].FindControl("chkItem") as CheckBox;
                    chk.Checked = false;
                   
                    if (ViewState["checkedValue"] != null)
                        arrlst = ViewState["checkedValue"] as ArrayList;

                    if (arrlst.Contains(lbl.Text))
                    {
                        arrlst.Remove(lbl.Text);
                    }
                    if (arrlst.Count > 0)
                    {
                        lblCheck.Text = "1";
                    }
                    else lblCheck.Text = "0";
                }
                ViewState["checkedValue"] = arrlst;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnSearch_Click()
        {
            try
            {
                Item_Master_BL imbl = new Item_Master_BL();
                Item_Master_Entity ime = GetData();

                DataTable dt = imbl.SelectAll_ItemView3(ime, 0, 10, 2);//Select all count

                int count = Convert.ToInt32(dt.Rows[0][0].ToString());

                gvItem.DataSource = imbl.SelectAll_ItemView3(ime, 1, 10, 1);//Select first 10 row
                gvItem.DataBind();
                gp.CalculatePaging(count,gvItem.PageSize,1);

                ViewState.Remove("checkedValue"); // After various search and check, clean previous check value.
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnExhibitSelectProduct_Click()
        {
            try
            {
                if (ViewState["checkedValue"] != null)
                {
                    string lists = "1";
                    if (ViewState["checkedValue"] != null)
                    {
                        ArrayList chk = ViewState["checkedValue"] as ArrayList;
                        String cklist = String.Join(",", (string[])chk.ToArray(Type.GetType("System.String")));
                        Session["list"] = cklist;
                        int User_ID = 0;
                        if (Session["User_ID"] != null)
                            User_ID = Convert.ToInt32(Session["User_ID"].ToString());
                        // Export_CSV eCSV = new Export_CSV(cklist, User_ID);
                        string url = "../Item_Exhibition/Exhibition_Confirmation.aspx?list=" + lists;                        
                        Response.Redirect(url,false);
                        Session.Remove("chkList");
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnSelectItemNewTab_Click()
        {
            try
            {
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    String cklist = String.Join(",", (string[])arrlst.ToArray(Type.GetType("System.String")));
                    Session["chkList"] = cklist;
                    string url = "../Item/Item_View3.aspx";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var win = window.open('" + url + "','_blank'); self.focus();", true);
                    Response.Redirect(url,false);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnSelectItemRemove_Click()
        {
            try
            {
                if (ViewState["checkedValue"] != null)
                {
                    btnSearch_Click();
                    ViewState["checkedValue"] = null;
                    //ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    //String cklist = String.Join(",", (string[])arrlst.ToArray(Type.GetType("System.String")));

                    //Item_Master_BL imbl = new Item_Master_BL();
                    //Item_Master_Entity ime = new Item_Master_Entity();
                    //ime.Item_Name = txtItemName.Value.Trim();
                    //ime.Item_Code = txtItemCode.Value.Trim();
                    //ime.Catalog_Information = txtCatalogInfo.Value.Trim();
                    //ime.Brand_Name = txtBrandName.Value.Trim();
                    //ime.Competition_Name = txtCompetitionName.Value.Trim();
                    //ime.Year = txtYear.Value.Trim();
                    //ime.Season = txtSeason.Value.Trim();
                    //if (!String.IsNullOrWhiteSpace(ddlSpecialFlag.SelectedValue))
                    //    ime.Special_Flag = Convert.ToInt32(ddlSpecialFlag.SelectedValue);
                    //else ime.Special_Flag = -1;
                    //if (!String.IsNullOrWhiteSpace(ddlReserveFlag.SelectedValue))
                    //    ime.Reservation_Flag = Convert.ToInt32(ddlReserveFlag.SelectedValue);
                    //else ime.Reservation_Flag = -1;
                    //if (!String.IsNullOrWhiteSpace(ddlSksStatus.SelectedValue))
                    //    ime.Export_Status = Convert.ToInt32(ddlSksStatus.SelectedValue);
                    //else ime.Export_Status = -1;
                    //ime.Ctrl_ID = ddlShopStatus.SelectedValue;
                    //ime.Color_Name = txtColorName.Value.Trim();
                    //ime.Image_Name = txtImageFileName.Value.Trim();

                    //if (Request.QueryString["list"] != null)
                    //{
                    //    ime.IdList = Request.QueryString["list"].ToString();
                    //}

                    //ime.RemoveList = cklist;

                    //DataTable dt = imbl.SelectAll(ime);
                    ////ViewState["ItemAll"] = dt;

                    //gp.TotalRecord = dt.Rows.Count;
                    //gp.OnePageRecord = gvItem.PageSize;


                    //int index1 = 0;
                    //gp.sendIndexToThePage += delegate(int index)
                    //{
                    //    index1 = index;
                    //};
                    //gvItem.DataSource = null;

                    //gvItem.PageIndex = index1;
                    //gvItem.DataSource = dt;
                    gvItem.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        //aam
        public bool Validation(int rowIndex)
        {
            try
            {
                TextBox txt = new TextBox();

                txt = gvItem.Rows[rowIndex].FindControl("txtItemDetail") as TextBox;
                int length = Encoding.GetEncoding(932).GetByteCount(txt.Text);
                if (length > 5120)
                {
                    GlobalUI.MessageBox("PC用商品説明文は5120文字までです。");
                    return false;
                }

                txt = gvItem.Rows[rowIndex].FindControl("txtsaleDetail") as TextBox;
                length = Encoding.GetEncoding(932).GetByteCount(txt.Text);
                if (length > 5120)
                {
                    GlobalUI.MessageBox("PC用販売説明文は5120文字までです。");
                    return false;
                }

                txt = gvItem.Rows[rowIndex].FindControl("txtLibraryImage1") as TextBox;
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    if (txt.Text.Length > 4)
                    {
                        if (!txt.Text.ToLower().Contains(".gif") && !txt.Text.ToLower().Contains(".jpg") && !txt.Text.ToLower().Contains(".png"))
                        {
                            GlobalUI.MessageBox("Invalid extension of photo in ライブラリ画像 first field ! ");
                            return false;
                        }
                    }
                    else
                    {
                        GlobalUI.MessageBox("Invalid name of photo in ライブラリ画像 first field ! ");
                        return false;
                    }
                }

                txt = gvItem.Rows[rowIndex].FindControl("txtLibraryImage2") as TextBox;
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    if (txt.Text.Length > 4)
                    {
                        if (!txt.Text.ToLower().Contains(".gif") && !txt.Text.ToLower().Contains(".jpg") && !txt.Text.ToLower().Contains(".png"))
                        {
                            GlobalUI.MessageBox("Invalid extension of photo in ライブラリ画像 second field ! ");
                            return false;
                        }
                    }
                    else
                    {
                        GlobalUI.MessageBox("Invalid name of photo in ライブラリ画像 second field ! ");
                        return false;
                    }
                }

                txt = gvItem.Rows[rowIndex].FindControl("txtLibraryImage3") as TextBox;
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    if (txt.Text.Length > 4)
                    {
                        if (!txt.Text.ToLower().Contains(".gif") && !txt.Text.ToLower().Contains(".jpg") && !txt.Text.ToLower().Contains(".png"))
                        {
                            GlobalUI.MessageBox("Invalid extension of photo in ライブラリ画像 third field ! ");
                            return false;
                        }
                    }
                    else
                    {
                        GlobalUI.MessageBox("Invalid name of photo in ライブラリ画像 third field ! ");
                        return false;
                    }
                }

                txt = gvItem.Rows[rowIndex].FindControl("txtLibraryImage3") as TextBox;
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    if (txt.Text.Length > 4)
                    {
                        if (!txt.Text.ToLower().Contains(".gif") && txt.Text.ToLower().Contains(".jpg") && txt.Text.ToLower().Contains(".png"))
                        {
                            GlobalUI.MessageBox("Invalid extension of photo in ライブラリ画像 fourth field ! ");
                            return false;
                        }
                    }
                    else
                    {
                        GlobalUI.MessageBox("Invalid name of photo in ライブラリ画像 fourth field ! ");
                        return false;
                    }
                }

                txt = gvItem.Rows[rowIndex].FindControl("txtLibraryImage3") as TextBox;
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    if (txt.Text.Length > 4)
                    {
                        if (!txt.Text.ToLower().Contains(".gif") && !txt.Text.ToLower().Contains(".jpg") && !txt.Text.ToLower().Contains(".png"))
                        {
                            GlobalUI.MessageBox("Invalid extension of photo in ライブラリ画像 fifth field ! ");
                            return false;
                        }
                    }
                    else
                    {
                        GlobalUI.MessageBox("Invalid name of photo in ライブラリ画像 fifth field ! ");
                        return false;
                    }
                }

                //GridView gv = gvItem.Rows[rowIndex].FindControl("gvShopCategory") as GridView;
                //if (gv != null && gv.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in gv.Rows)
                //    {
                //        if (StrToByteArray(dr["CName"].ToString()).Length> 60)
                //        {
                //            GlobalUI.MessageBox("Invalid ショップカテゴリ path ! ");
                //            return false;
                //        }
                //    }
                //}

                //for (int i = 1; i <= 5; i++)
                //{
                //    length = Encoding.GetEncoding(932).GetByteCount((gvItem.Rows[rowIndex].FindControl("txtShopCategory" + i) as TextBox).ToString());
                //    if (length > 60)
                //    {
                //        GlobalUI.MessageBox("Invalid ショップカテゴリ path ! ");
                //        return false;
                //    }
                //}


                return true;

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
            try
            {
                int count = 0;
                Item_Master_BL imbl = new Item_Master_BL();
                Item_Master_Entity ime = GetData();
                count = Convert.ToInt32(imbl.SelectAll_ItemView3(ime, 1, 10, 2).Rows[0]["Count"]);
                return count;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return -1;
            }
        }

        public void btnShopCategory_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            Label l1 = row.FindControl("lblID") as Label;

            DataTable dt = new DataTable();
            dt.Columns.Add("CID");
            dt.Columns.Add("CName");
            dt.Columns.Add("Category_SN");

            for (int i = 1; i < 6; i++)
            {
                Label lblID= row.FindControl("lblSCID" + i) as Label;
                TextBox txtName = row.FindControl("txtShopCategory" + i) as TextBox;
                Label lblSN = row.FindControl("lblSN" + i) as Label;

                if (!String.IsNullOrWhiteSpace(txtName.Text))
                {
                    dt.Rows.Add();
                    dt.Rows[i - 1]["CID"] = lblID.Text;
                    dt.Rows[i - 1]["CName"] = txtName.Text;
                    dt.Rows[i - 1]["Category_SN"] = lblSN.Text;
                }

            }

            Session["CategoryList"] = dt;

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "ShopCategoryPopUp(" + l1.Text + ",'" + row.RowIndex + "');", true);
        }

        public void btnSpec_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            Label l1 = row.FindControl("lblID") as Label;
            TextBox txtYahooID = row.FindControl("txtyahoo") as TextBox;
            DataTable dt = new DataTable();
            dt.Columns.Add("YahooMall_CategoryID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Spec_ID");
            dt.Columns.Add("Spec_Name");
            dt.Columns.Add("Spec_ValueID");
            dt.Columns.Add("Spec_ValueName");
            dt.Columns.Add("Type");
            

            for (int i = 1; i < 6; i++)
            {
                TextBox txtYahoo = row.FindControl("txtyahoo") as TextBox;
                Label lblName = row.FindControl("lblName1") as Label;
                Label lblSpec_ID = row.FindControl("lblSpec_ID" + i) as Label;
                Label lblSpec_Name = row.FindControl("lblSpec_Name" + i) as Label;
                Label lblSpec_ValueID = row.FindControl("lblSpec_ValueID" + i) as Label;
                TextBox txtSpec = row.FindControl("txtSpec" + i) as TextBox;

                //if (!String.IsNullOrWhiteSpace(txtSpec.Text))
                //{
                    dt.Rows.Add();
                    dt.Rows[i - 1]["YahooMall_CategoryID"] = txtYahoo.Text;
                    dt.Rows[i - 1]["Name"] = lblName.Text;
                    dt.Rows[i - 1]["Spec_ID"] = lblSpec_ID.Text;
                    dt.Rows[i - 1]["Spec_Name"] = lblSpec_Name.Text;
                    dt.Rows[i - 1]["Spec_ValueID"] = lblSpec_ValueID.Text;
                    dt.Rows[i - 1]["Spec_ValueName"] = txtSpec.Text;
               // }

            }

            Session["YahooSpecificValue"] = dt;

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "SpecPopUp(" + txtYahooID.Text + ",'" + row.RowIndex + "');", true);
        }

        public void DisplayYahooSpecificValue(string yahoomallID)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(yahoomallID) && yahoomallID != "0")
                {
                    Yahoo_SpecName_BL YahooSpecNameBL = new Yahoo_SpecName_BL();
                    DataTable dt = YahooSpecNameBL.DisplayYahooSpecificValue(Convert.ToInt32(yahoomallID));
                    Session["YahooSpecificValue"] = dt;
                }
                else
                { 
                
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public bool CheckExistYahooValue(string yahoomallID)
        {
                if (!string.IsNullOrWhiteSpace(yahoomallID) && yahoomallID != "0")
                {
                    Yahoo_SpecName_BL YahooSpecNameBL = new Yahoo_SpecName_BL();
                    DataTable dt = YahooSpecNameBL.DisplayYahooSpecificValue(Convert.ToInt32(yahoomallID));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    


    }
