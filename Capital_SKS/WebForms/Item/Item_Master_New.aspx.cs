using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using ORS_RCM_Common;
using System.Data;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace ORS_RCM.WebForms.Item
{
    public partial class Item_Master_New : System.Web.UI.Page
    {
        //Global Variables
        Item_Master_Entity ime;
        Item_Master_BL imeBL;
        Item_Category_BL itemCategoryBL;
        Category_BL cbl;
        Item_BL ibl;
        public int index = 0;
        public int extract = 0;
        public String[] ex = new String[6];
        public String[] cx = new String[100];
        public String[] ids = new String[100];
        string treepath = string.Empty;
        string catpath = string.Empty;
        UserRoleBL user;
        public int flag = 2;
        public string ItemCode
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(txtItem_Code.Text))
                {
                    return txtItem_Code.Text.Trim();
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable CategoryList
        {
            get
            {

                if (Session["CategoryList_" + ItemCode] != null)
                {
                    DataTable dt = (DataTable)Session["CategoryList_" + ItemCode];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable ImageList
        {
            get
            {
                if (Session["ImageList_" + ItemCode] != null)
                {
                    DataTable dt = (DataTable)Session["ImageList_" + ItemCode];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable MallCategoryID
        {
            get
            {
                if (Session["Mall_Category_ID_" + ItemCode] != null)
                {
                    return (DataTable)Session["Mall_Category_ID_" + ItemCode];
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable Option
        {
            get
            {
                if (Session["Option_" + ItemCode] != null)
                {
                    DataTable dt = (DataTable)Session["Option_" + ItemCode];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable YahooSpecificValue
        {
            get
            {
                if (Session["YahooSpecificValue_" + ItemCode] != null)
                {
                    return (DataTable)Session["YahooSpecificValue_" + ItemCode];
                }
                else
                {
                    return null;
                }
            }
        }

        public int UserID
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
        string imagePath = ConfigurationManager.AppSettings["ItemImage"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {               
                string ControlID = string.Empty;
                if (!IsPostBack)
                {
                    BindSaleUnit();
                    BindContentUnit1();
                    BindContentUnit2();
                    BindMonotaroddl();
                    ime = new Item_Master_Entity();
                    imeBL = new Item_Master_BL();
                    Item_BL item = new Item_BL();
                    //After Save Successful or Update Successful , Back to pervious page
                    #region BackPage ViewState
                    String backpage = string.Empty;
                    if (Request.UrlReferrer != null)
                    {
                        ViewState.Clear();
                        ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                        backpage = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["UrlReferrer"] = backpage;
                    }
                    BindShop(); //Bind Shop List'
                    ControlPopup();
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(hdfPostDate.Value))
                    {
                        txtPost_Available_Date.Text = hdfPostDate.Value;
                    }
                    if (!String.IsNullOrWhiteSpace(hdfReleaseDate.Value))
                    {
                        txtRelease_Date.Text = hdfReleaseDate.Value;
                    }
                    if (!String.IsNullOrEmpty(CustomHiddenField.Value))
                    {
                        ControlID = CustomHiddenField.Value;
                    }
                    DataTable dt = RebindItemCodeURL(ControlID);
                    if (dlShop.Items.Count == 0 || dt.Rows.Count == 0)
                    {
                        BindShopName();
                    }
                    //ControlPopup();
                    if (ControlID.Contains("lnkAddPhoto"))
                    {
                        ReBindPhotoList();
                    }
                    else if (ControlID.Contains("btnAddOption"))
                    {
                        ShowOption();
                    }
                    else if (ControlID.Contains("btnAddCatagories"))
                    {
                        if (Session["btnPopClick_" + ItemCode] != null && Session["btnPopClick_" + ItemCode].ToString() == "ok")
                        {
                            SetMallCategoryData();
                            SetJishaCategoryData();
                            Session.Remove("btnPopClick_" + ItemCode);
                        }
                        else
                        {
                            Session.Remove("btnPopClick_" + ItemCode);
                        }
                    }
                    else if (ControlID.Contains("btnRakuten_CategoryID"))
                    {
                        DisplayMallCategory(); // for display Mall_Category from popup form
                    }
                    else if (ControlID.Contains("btnYahoo_CategoryID"))
                    {
                        DisplayMallCategory(); // for display Mall_Category from popup form
                    }
                    else if (ControlID.Contains("btnPonpare_CategoryID"))
                    {
                        DisplayMallCategory(); // for display Mall_Category from popup form
                    }
                    else if (ControlID.Contains("imgbYahooSpecValue"))
                    {
                        if (Session["btnYPopClick_" + ItemCode] != null && Session["btnYPopClick_" + ItemCode].ToString() == "ok")
                        {
                            ShowValue();
                            Session.Remove("btnYPopClick_" + ItemCode);
                        }
                        else
                        {
                            Session.Remove("btnYPopClick_" + ItemCode);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void BindSaleUnit()
        {
            imeBL = new Item_Master_BL();
            DataTable dt = imeBL.BindSaleUnit();
            ddlsalesunit.DataSource = dt;
            ddlsalesunit.DataTextField = "Sales_unit";
            ddlsalesunit.DataValueField = "Sales_unit";
            ddlsalesunit.DataBind();
            ddlsalesunit.Items.Insert(0, new ListItem("  ", "0"));
        }

        public void BindMonotaroddl()
        {
            imeBL = new Item_Master_BL();
            DataTable dtdeliverymethod = imeBL.BindMonotaro("Delivery_Method");
            ddldeliverymethod.DataSource = dtdeliverymethod;
            ddldeliverymethod.DataTextField = "Delivery_Method";
            ddldeliverymethod.DataValueField = "Delivery_Method_ID";
            ddldeliverymethod.DataBind();

            DataTable dtdeliverytype = imeBL.BindMonotaro("Delivery_Type");
            ddldeliverytype.DataSource = dtdeliverytype;
            ddldeliverytype.DataTextField = "Delivery_Type_Name";
            ddldeliverytype.DataValueField = "Delivery_Type_ID";
            ddldeliverytype.DataBind();

            DataTable dtdeliveryfees = imeBL.BindMonotaro("COD");
            ddldeliveryfees.DataSource = dtdeliveryfees;
            ddldeliveryfees.DataTextField = "COD_Name";
            ddldeliveryfees.DataValueField = "COD_Value";
            ddldeliveryfees.DataBind();

            DataTable dtksmavaliable = imeBL.BindMonotaro("Customer_Delivery_Type");
            ddlksmavaliable.DataSource = dtksmavaliable;
            ddlksmavaliable.DataTextField = "Customer_Delivery_Type";
            ddlksmavaliable.DataValueField = "Customer_Delivery_Type_ID";
            ddlksmavaliable.DataBind();

            DataTable dtreturnableitem = imeBL.BindMonotaro("Return_Type");
            ddlreturnableitem.DataSource = dtreturnableitem;
            ddlreturnableitem.DataTextField = "Return_Type";
            ddlreturnableitem.DataValueField = "Return_Type_ID";
            ddlreturnableitem.DataBind();

            DataTable dtapplicablelaw = imeBL.BindMonotaro("Applicable_Law");
            ddlnoapplicablelaw.DataSource = dtapplicablelaw;
            ddlnoapplicablelaw.DataTextField = "Applicable_Law_Type";
            ddlnoapplicablelaw.DataValueField = "Applicable_Law_Type_ID";
            ddlnoapplicablelaw.DataBind();

            DataTable dtsalespermission = imeBL.BindMonotaro("Sales_Permission");
            ddlsalespermission.DataSource = dtsalespermission;
            ddlsalespermission.DataTextField = "Sale_Permission_Type";
            ddlsalespermission.DataValueField = "Sale_Permission_Type_ID";
            ddlsalespermission.DataBind();

            DataTable dtlaw = imeBL.BindMonotaro("Law");
            ddllaw.DataSource = dtlaw;
            ddllaw.DataTextField = "Law_And_Regulations_Type";
            ddllaw.DataValueField = "Law_And_Regulations_ID";
            ddllaw.DataBind();

            DataTable dtcustomerassembly = imeBL.BindMonotaro("Customer_Assembly");
            ddlcustomerassembly.DataSource = dtcustomerassembly;
            ddlcustomerassembly.DataTextField = "Customer_Assembly";
            ddlcustomerassembly.DataValueField = "Customer_Assembly_ID";
            ddlcustomerassembly.DataBind();

            DataTable dtfirelaw = imeBL.BindMonotaro("Fire_Service_Law");
            ddlfirelaw.DataSource = dtfirelaw;
            ddlfirelaw.DataTextField = "Fire_Service_Law";
            ddlfirelaw.DataValueField = "Fire_Service_Law_ID";
            ddlfirelaw.DataBind();

            DataTable dtdanggoodsclass = imeBL.BindMonotaro("Dangarous_Goods");
            ddldanggoodsclass.DataSource = dtdanggoodsclass;
            ddldanggoodsclass.DataTextField = "Dangarous_Goods";
            ddldanggoodsclass.DataValueField = "Dangarous_Goods_ID";
            ddldanggoodsclass.DataBind();

            DataTable dtdanggoodsname = imeBL.BindMonotaro("Dangerous_Goods_Name");
            ddldanggoodsname.DataSource = dtdanggoodsname;
            ddldanggoodsname.DataTextField = "Dangerous_Goods_Name";
            ddldanggoodsname.DataValueField = "Dangarous_Goods_Name_ID";
            ddldanggoodsname.DataBind();

            DataTable dtriskrating = imeBL.BindMonotaro("Risk_Rating");
            ddlriskrating.DataSource = dtriskrating;
            ddlriskrating.DataTextField = "Risk_Rating";
            ddlriskrating.DataValueField = "Risk_Rating_ID";
            ddlriskrating.DataBind();

            DataTable dtdanggoodsnature = imeBL.BindMonotaro("Dangerous_Goods_Nature");
            ddldanggoodsnature.DataSource = dtdanggoodsnature;
            ddldanggoodsnature.DataTextField = "Dangerous_Goods_Nature";
            ddldanggoodsnature.DataValueField = "Dangerous_Goods_Nature_ID";
            ddldanggoodsnature.DataBind();
        }

        public void BindContentUnit2()
        {
            imeBL = new Item_Master_BL();
            DataTable dt = imeBL.BindContentunit2();
            ddlcontentunit2.DataSource = dt;
            ddlcontentunit2.DataTextField = "Contents_unit_2";
            ddlcontentunit2.DataValueField = "Contents_unit_2";
            ddlcontentunit2.DataBind();
            ddlcontentunit2.Items.Insert(0, new ListItem("  ", "0"));
        }

        public void BindContentUnit1()
        {
            imeBL = new Item_Master_BL();
            DataTable dt = imeBL.BindContentunit1();
            ddlcontentunit1.DataSource = dt;
            ddlcontentunit1.DataTextField = "Contents_unit_1";
            ddlcontentunit1.DataValueField = "Contents_unit_1";
            ddlcontentunit1.DataBind();
            ddlcontentunit1.Items.Insert(0, new ListItem("  ", "0"));
        }

        public void ControlPopup()
        {
            if (ItemCode != null)
            {
                if (CheckExistsItemCode(ItemCode))
                {
                    GlobalUI.MessageBox("Item_Code Already Exists!!Please Write Another Item Code");
                }
                else
                {
                   // lnkAddPhoto.Visible = true;
                    btnAddOption.Visible = true;
                    btnAddCatagories.Visible = true;
                    btnRakuten_CategoryID.Visible = true;
                    btnYahoo_CategoryID.Visible = true;
                    btnPonpare_CategoryID.Visible = true;
                    imgbYahooSpecValue.Visible = true;
                }
            }
            else
            {
               // lnkAddPhoto.Visible = false;
                btnAddOption.Visible = false;
                btnAddCatagories.Visible = false;
                btnRakuten_CategoryID.Visible = false;
                btnYahoo_CategoryID.Visible = false;
                btnPonpare_CategoryID.Visible = false;
                imgbYahooSpecValue.Visible = false;

            }
        }

        public void BindShop()
        {
            try
            {
                Shop_BL shopBL = new Shop_BL();
                DataTable dt = shopBL.SelectAll();

                DataRow[] dr = dt.Select("SN='1'");
                if (dr.Length > 0)
                {
                    DataTable dtCopy = dt.Select("SN='1'").CopyToDataTable();
                    dlShop1.DataSource = dtCopy;
                    dlShop1.DataBind();
                    lblShopName.Text = dtCopy.Rows[0]["Shop_Name"].ToString();
                }
                else
                { dd1.Visible = false; }
                dr = dt.Select("SN='2'");
                if (dr.Length > 0)
                {
                    DataTable dtCopy = dt.Select("SN='2'").CopyToDataTable();
                    dlShop2.DataSource = dtCopy;
                    dlShop2.DataBind();
                    lblShopName1.Text = dtCopy.Rows[0]["Shop_Name"].ToString();
                }
                else
                { dd2.Visible = false; }
                dr = dt.Select("SN='3'");
                if (dr.Length > 0)
                {
                    DataTable dtCopy = dt.Select("SN='3'").CopyToDataTable();
                    dlShop3.DataSource = dtCopy;
                    dlShop3.DataBind();
                    lblShopName2.Text = dtCopy.Rows[0]["Shop_Name"].ToString();
                }
                else
                { dd3.Visible = false; }

                dr = dt.Select("SN='4'");
                if (dr.Length > 0)
                {
                    DataTable dtCopy = dt.Select("SN='4'").CopyToDataTable();
                    dlShop4.DataSource = dtCopy;
                    dlShop4.DataBind();
                    lblShopName3.Text = dtCopy.Rows[0]["Shop_Name"].ToString();
                }
                else
                { dd4.Visible = false; }

                dr = dt.Select("SN='5'");
                if (dr.Length > 0)
                {
                    DataTable dtCopy = dt.Select("SN='5'").CopyToDataTable();
                    dlShop5.DataSource = dtCopy;
                    dlShop5.DataBind();
                    lblShopName4.Text = dtCopy.Rows[0]["Shop_Name"].ToString();
                }
                else
                { dd5.Visible = false; }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public DataTable RebindItemCodeURL(string ctrl)
        {
            Item_Shop_BL isbl = new Item_Shop_BL();
            DataTable dt = new DataTable();
            dt.Columns.Add("Shop_ID", typeof(int));
            dt.Columns.Add("Item_Code_URL", typeof(string));
            foreach (DataListItem li in dlShop.Items)
            {
                TextBox txtitemcode = li.FindControl("txtItem_CodeList") as TextBox;
                Label shopid = li.FindControl("lblShopID") as Label;
                CheckBox cb = li.FindControl("ckbShop") as CheckBox;
                string icode = ItemCode;
                if (icode == txtitemcode.Text)
                {
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            DataRow dr = dt.NewRow();
                            dr["Item_Code_URL"] = txtitemcode.Text;
                            dr["Shop_ID"] = Convert.ToInt32(shopid.Text);
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else
                {
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            if (ctrl.Contains("txtItem_Code"))
                            {
                                DataRow dr = dt.NewRow();
                                dr["Item_Code_URL"] = ItemCode;
                                dr["Shop_ID"] = Convert.ToInt32(shopid.Text);
                                dt.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = dt.NewRow();
                                dr["Item_Code_URL"] = txtitemcode.Text;
                                dr["Shop_ID"] = Convert.ToInt32(shopid.Text);
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    foreach (DataListItem li in dlShop.Items)
                    {
                        TextBox txtitemcode = li.FindControl("txtItem_CodeList") as TextBox;
                        Label shopid = li.FindControl("lblShopID") as Label;
                        CheckBox cb = li.FindControl("ckbShop") as CheckBox;
                        if (shopid.Text == dt.Rows[i]["Shop_ID"].ToString())
                        {
                            cb.Checked = true;
                            txtitemcode.Text = dt.Rows[i]["Item_Code_URL"].ToString();
                            break;
                        }
                    }
                }
            }
            return dt;
        }

        public void BindShopName()
        {
            try
            {
                Shop_BL shopBL = new Shop_BL();
                Item_Shop_BL isbl = new Item_Shop_BL();
                DataTable dt = shopBL.SelectAll();
                if (ItemCode != null)
                {
                    DataTable dtURL = isbl.SelectItemCodeURL(ItemCode);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Item_Code");
                        if (dtURL.Rows.Count <= 0 && !String.IsNullOrWhiteSpace(txtItem_Code.ToString()))
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            { dt.Rows[i]["Item_Code"] = ItemCode; }

                        }
                        else
                        {
                            foreach (DataListItem li in dlShop.Items)
                            {
                                TextBox txtitemcode = li.FindControl("txtItem_CodeList") as TextBox;
                                Label shopid = li.FindControl("lblShopID") as Label;
                                CheckBox cb = li.FindControl("ckbShop") as CheckBox;
                                if (dtURL != null && dtURL.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtURL.Rows.Count; i++)
                                    {
                                        if ((dtURL.Rows[i]["Item_Code"].ToString() == txtitemcode.Text) && (dtURL.Rows[i]["Shop_ID"].ToString()) == shopid.Text)
                                        {
                                            dt.Rows[i]["Item_Code"] = ItemCode;
                                        }
                                        else
                                        {
                                            if ((dtURL.Rows[i]["Shop_ID"].ToString()) == shopid.Text)
                                            {
                                                dt.Rows[i]["Item_Code"] = txtitemcode.Text;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        dlShop.DataSource = dt;
                        dlShop.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void ReBindPhotoList()
        {
            try
            {
                if (ImageList != null)
                {
                    DataTable dt = ImageList as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        #region Item Image
                        DataRow[] dr = dt.Select("Image_Type='0'");
                        if (dr.Length > 0)
                        {
                            DataTable dtImage = dt.Select("Image_Type='0'").CopyToDataTable();
                            #region Set null value
                            Image1.ImageUrl = "";
                            hlImage1.NavigateUrl = "";
                            Image2.ImageUrl = "";
                            hlImage2.NavigateUrl = "";
                            Image3.ImageUrl = "";
                            hlImage3.NavigateUrl = "";
                            Image4.ImageUrl = "";
                            hlImage4.NavigateUrl = "";
                            Image5.ImageUrl = "";
                            hlImage5.NavigateUrl = "";
                            #endregion

                            #region Set value
                            for (int m = 0; m < dtImage.Rows.Count; m++)
                            {
                                switch (dtImage.Rows[m]["SN"].ToString())
                                {
                                    case "1":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image1.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage1.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image1.ImageUrl = "";
                                            hlImage1.NavigateUrl = "";
                                        }
                                        break;
                                    case "2":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image2.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage2.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image2.ImageUrl = "";
                                            hlImage2.NavigateUrl = "";
                                        }
                                        break;
                                    case "3":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image3.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage3.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image3.ImageUrl = "";
                                            hlImage3.NavigateUrl = "";
                                        }
                                        break;
                                    case "4":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image4.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage4.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image4.ImageUrl = "";
                                            hlImage4.NavigateUrl = "";
                                        }
                                        break;
                                    case "5":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image5.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage5.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image5.ImageUrl = "";
                                            hlImage5.NavigateUrl = "";
                                        }
                                        break;
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        Image1.ImageUrl = "";
                        Image2.ImageUrl = "";
                        Image3.ImageUrl = "";
                        Image4.ImageUrl = "";
                        Image5.ImageUrl = "";
                        hlImage1.NavigateUrl = "";
                        hlImage2.NavigateUrl = "";
                        hlImage3.NavigateUrl = "";
                        hlImage4.NavigateUrl = "";
                        hlImage5.NavigateUrl = "";
                    }
                        #endregion
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void ShowOption()
        {
            try
            {
                if (Option != null && Option.Rows.Count > 0)
                {
                    DataTable dt = Option as DataTable;
                    SetOption(dt);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void SetOption(DataTable dt)
        {
            try
            {
                txtOptionName1.Text = dt.Rows[0]["Name1"].ToString();
                txtOptionName2.Text = dt.Rows[0]["Name2"].ToString();
                txtOptionName3.Text = dt.Rows[0]["Name3"].ToString();
                txtOptionValue1.Text = dt.Rows[0]["Value1"].ToString();
                txtOptionValue2.Text = dt.Rows[0]["Value2"].ToString();
                txtOptionValue3.Text = dt.Rows[0]["Value3"].ToString();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void SetMallCategoryData()
        {
            int rowIndex = 0;
            gvCatagories.DataSource = CategoryList;
            gvCatagories.DataBind();
            if (CategoryList != null && CategoryList.Rows.Count > 0)
            {
                for (int i = rowIndex; i < CategoryList.Rows.Count; i++)
                {
                    Label lblID = (Label)gvCatagories.Rows[rowIndex].Cells[1].FindControl("lblID");
                    TextBox txtValue = (TextBox)gvCatagories.Rows[rowIndex].Cells[1].FindControl("txtCTGName");
                    lblID.Text = CategoryList.Rows[i]["CID"].ToString();
                    txtValue.Text = CategoryList.Rows[i]["CName"].ToString();
                    rowIndex++;
                }
                Category_BL catbl = new Category_BL();
                DataTable dt = new DataTable();
                dt = CategoryList;

                if (dt != null && dt.Rows.Count > 0)
                {
                    String id = (dt.Rows[0]["CID"]).ToString();

                    if (dt.Rows.Count > 0)
                    {
                        DataTable driectdt = new DataTable();
                        driectdt = catbl.Get_CategoryID(id);
                        if (driectdt.Rows.Count > 0)
                        {
                            txtRakuten_CategoryID.Text = driectdt.Rows[0]["Rakutan_DirectoryID"].ToString();
                            txtRakuten_CategoryPath.Text = driectdt.Rows[0]["Rakuten_CategoryName"].ToString();
                            txtYahoo_CategoryID.Text = driectdt.Rows[0]["Yahoo_CategoryID"].ToString();
                            txtYahoo_CategoryPath.Text = driectdt.Rows[0]["Yahoo_CategoryName"].ToString();
                            txtPonpare_CategoryID.Text = driectdt.Rows[0]["Ponpare_CategoryID"].ToString();
                            txtPonpare_CategoryPath.Text = driectdt.Rows[0]["Ponpare_CategoryName"].ToString();
                        }
                    }
                }
                else
                {
                    txtRakuten_CategoryID.Text = string.Empty;
                    txtYahoo_CategoryID.Text = string.Empty;
                    txtPonpare_CategoryID.Text = string.Empty;
                    txtRakuten_CategoryPath.Text = "";
                    txtYahoo_CategoryPath.Text = "";
                    txtPonpare_CategoryPath.Text = "";
                }
            }
            else
            {
                txtRakuten_CategoryID.Text = string.Empty;
                txtYahoo_CategoryID.Text = string.Empty;
                txtPonpare_CategoryID.Text = string.Empty;
                txtRakuten_CategoryPath.Text = "";
                txtYahoo_CategoryPath.Text = "";
                txtPonpare_CategoryPath.Text = "";
            }
        }

        public void SetJishaCategoryData()
        {
            try
            {
                int rowIndex = 0;
                string jid = string.Empty;
                string jname = string.Empty;
                DataTable dtCopy = new DataTable();
                if (CategoryList != null && CategoryList.Rows.Count > 0)
                {
                    dtCopy = CategoryList.Copy();
                    if (CategoryList.Columns.Contains("JishaID"))
                    { }
                    else
                    {
                        dtCopy.Columns.Add("JishaID", typeof(String));
                        dtCopy.Columns.Add("JishaName", typeof(String));
                    }
                    for (int i = 0; i < CategoryList.Rows.Count; i++)
                    {
                        int id = Convert.ToInt32(CategoryList.Rows[i]["CID"]);
                        cbl = new Category_BL();
                        DataTable dt = cbl.SelectForCatalogID(id);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Jisha_DirectoryID"].ToString()) || !String.IsNullOrWhiteSpace(dt.Rows[0]["Jisha_CategoryName"].ToString()))
                            {
                                dtCopy.Rows[i]["JishaID"] = dt.Rows[0]["Jisha_DirectoryID"].ToString();
                                dtCopy.Rows[i]["JishaName"] = dt.Rows[0]["Jisha_CategoryName"].ToString();
                            }
                        }
                    }
                    DataRow[] drr = dtCopy.Select("[JishaID]=' ' OR [JishaID] is null");
                    for (int j = 0; j < drr.Length; j++)
                        drr[j].Delete();
                    dtCopy.AcceptChanges();
                    if (dtCopy != null && dtCopy.Rows.Count > 0)
                    {
                        gvjishacategory.DataSource = dtCopy;
                        gvjishacategory.DataBind();
                        for (int i = rowIndex; i < dtCopy.Rows.Count; i++)
                        {
                            TextBox lblID = (TextBox)gvjishacategory.Rows[rowIndex].Cells[1].FindControl("txtCatno");
                            TextBox txtValue = (TextBox)gvjishacategory.Rows[rowIndex].Cells[1].FindControl("txtCategoryName");
                            lblID.Text = dtCopy.Rows[i]["JishaID"].ToString();
                            txtValue.Text = dtCopy.Rows[i]["JishaName"].ToString();
                            rowIndex++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void DisplayMallCategory()
        {
            try
            {
                if (MallCategoryID != null)
                {
                    DataTable dt = MallCategoryID as DataTable;
                    if (dt.Rows[0]["Mall_ID"].ToString() == "1")
                    {
                        txtRakuten_CategoryID.Text = dt.Rows[0]["Category_ID"].ToString();
                        txtRakuten_CategoryPath.Text = dt.Rows[0]["Category_Path"].ToString();
                    }
                    else if (dt.Rows[0]["Mall_ID"].ToString() == "2")
                    {
                        txtYahoo_CategoryID.Text = dt.Rows[0]["Category_ID"].ToString();
                        txtYahoo_CategoryPath.Text = dt.Rows[0]["Category_Path"].ToString();
                    }
                    else if (dt.Rows[0]["Mall_ID"].ToString() == "3")
                    {
                        txtPonpare_CategoryID.Text = dt.Rows[0]["Category_ID"].ToString();
                        txtPonpare_CategoryPath.Text = dt.Rows[0]["Category_Path"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void ShowValue()
        {
            try
            {
                if (YahooSpecificValue != null && YahooSpecificValue.Rows.Count > 0)
                {
                    DataRow[] dr = YahooSpecificValue.Select("Type = 1");
                    if (dr.Count() > 0)
                        txtYahooValue1.Text = dr[0]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue1.Text = "";

                    dr = YahooSpecificValue.Select("Type = 2");
                    if (dr.Count() > 0)
                        txtYahooValue2.Text = dr[0]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue2.Text = "";

                    dr = YahooSpecificValue.Select("Type = 3");
                    if (dr.Count() > 0)
                        txtYahooValue3.Text = dr[0]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue3.Text = "";

                    dr = YahooSpecificValue.Select("Type = 4");
                    if (dr.Count() > 0)
                        txtYahooValue4.Text = dr[0]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue4.Text = "";

                    dr = YahooSpecificValue.Select("Type = 5");
                    if (dr.Count() > 0)
                        txtYahooValue5.Text = dr[0]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue5.Text = "";
                }
                else
                {
                    //imgbYahooSpecValue.Enabled = false;
                    txtYahooValue1.Text = "";
                    txtYahooValue2.Text = "";
                    txtYahooValue3.Text = "";
                    txtYahooValue4.Text = "";
                    txtYahooValue5.Text = "";
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void btnCategoryAdd_OnClick(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        public void AddNewRowToGrid()
        {
            string catvalue = null;
            int rowindex = 0;
            if (ViewState["PreviousTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["PreviousTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)gvCategory.Rows[rowindex].Cells[0].FindControl("txtCategory");
                        TextBox box2 = (TextBox)gvCategory.Rows[rowindex].Cells[0].FindControl("txtSN");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Category"] = box1.Text.Replace(@"\", "￥");
                        Regex reg = new Regex(@"^[a-zA-Z'.]{1,40}$");
                        if (!Regex.IsMatch(box1.Text, @"^[a-zA-Z'.]{1,40}$"))
                        {
                            GlobalUI.MessageBox("Please Type Valid Format!!");
                        }
                        dtCurrentTable.Rows[i - 1]["SN"] = box2.Text;
                        rowindex++;
                    }
                    drCurrentRow["SN"] = 0;
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["PreviousTable"] = dtCurrentTable;
                    gvCategory.DataSource = dtCurrentTable;
                    gvCategory.DataBind();
                }
            }
            else if (ViewState["CurrentTable"] != null)
            {

                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0 && dtCurrentTable.Rows.Count < 5)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)gvCategory.Rows[rowindex].Cells[0].FindControl("txtCategory");
                        TextBox box2 = (TextBox)gvCategory.Rows[rowindex].Cells[0].FindControl("txtSN");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Category"] = box1.Text.Replace(@"\", "￥");
                        dtCurrentTable.Rows[i - 1]["SN"] = box2.Text;
                        rowindex++;
                        if (String.IsNullOrWhiteSpace(dtCurrentTable.Rows[i - 1]["Category"].ToString()))
                        {
                            catvalue = "empty";
                        }
                    }
                    if (catvalue != "empty")
                    {
                        drCurrentRow["SN"] = 0;
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }
                    ViewState["CurrentTable"] = dtCurrentTable;
                    gvCategory.DataSource = dtCurrentTable;
                    gvCategory.DataBind();
                    SetPreviousData();
                }
                else
                {
                    GlobalUI.MessageBox("Do not Allow More Than five Categories!!");
                }
            }
        }

        private void SetPreviousData()
        {
            try
            {
                int rowindex = 0;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TextBox box1 = (TextBox)gvCategory.Rows[rowindex].Cells[0].FindControl("txtCategory");
                            TextBox box2 = (TextBox)gvCategory.Rows[rowindex].Cells[0].FindControl("txtSN");
                            box1.Text = dt.Rows[i]["Category"].ToString();
                            box2.Text = dt.Rows[i]["SN"].ToString();
                            rowindex++;
                        }
                    }
                    if (dt.Rows.Count > 1)
                    {
                        Button button = gvCategory.HeaderRow.FindControl("btnRemove") as Button;
                        button.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnRemove_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvRow = (GridViewRow)btn.NamingContainer;
            int rowID = gvRow.RowIndex + 1;
            if (ViewState["PreviousTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["PreviousTable"];
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data
                        dt.Rows.Remove(dt.Rows[dt.Rows.Count - 1]);
                    }
                }
                ViewState["PreviousTable"] = dt;
                gvCategory.DataSource = dt;
                gvCategory.DataBind();
                int rowIndex = 0;
                foreach (GridViewRow row in gvCategory.Rows)
                {
                    TextBox box1 = (TextBox)gvCategory.Rows[rowIndex].Cells[0].FindControl("txtCategory");
                    TextBox box2 = (TextBox)gvCategory.Rows[rowIndex].Cells[0].FindControl("txtSN");
                    box1.Text = dt.Rows[rowIndex]["Category"].ToString();
                    box2.Text = dt.Rows[rowIndex]["SN"].ToString();
                    rowIndex++;
                }
                if (dt.Rows.Count > 1)
                {
                    Button button = gvCategory.HeaderRow.FindControl("btnRemove") as Button;
                    button.Visible = true;
                }
            }
            else if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data
                        dt.Rows.Remove(dt.Rows[dt.Rows.Count - 1]);
                    }
                }
                ViewState["CurrentTable"] = dt;
                gvCategory.DataSource = dt;
                gvCategory.DataBind();
                int rowIndex = 0;
                foreach (GridViewRow row in gvCategory.Rows)
                {
                    TextBox box1 = (TextBox)gvCategory.Rows[rowIndex].Cells[0].FindControl("txtCategory");
                    TextBox box2 = (TextBox)gvCategory.Rows[rowIndex].Cells[0].FindControl("txtSN");
                    box1.Text = dt.Rows[rowIndex]["Category"].ToString();
                    box2.Text = dt.Rows[rowIndex]["SN"].ToString();
                    rowIndex++;
                }
                if (dt.Rows.Count > 1)
                {
                    Button button = gvCategory.HeaderRow.FindControl("btnRemove") as Button;
                    button.Visible = true;
                }
            }
        }

        protected void delivery_flag_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;
                imeBL = new Item_Master_BL();
                if (chk.Checked)
                {
                    ViewState["DailyDelivery"] = 1;
                }
                else
                {
                    ViewState["DailyDelivery"] = 0;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void chkInventory_OnCheckChanged(object sender, EventArgs e)
        {
            imeBL = new Item_Master_BL();
            if (chkInventory.Checked)
            {
                imeBL.ItemUpdateInventory(ItemCode, 0);
            }
            else
            {
                imeBL.ItemUpdateInventory(ItemCode, 1);
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList myData = new ArrayList();
                myData.Add(txtItem_Code.Text);
                myData.Add(txtItem_Name.Text);
                myData.Add(txtList_Price.Text);
                myData.Add(txtSale_Price.Text);
                myData.Add(txtItem_Description_PC.Text);
                myData.Add(txtSale_Description_PC.Text);
                myData.Add(txtRelated1.Text);
                myData.Add(txtRelated2.Text);
                myData.Add(txtRelated3.Text);
                myData.Add(txtRelated4.Text);
                myData.Add(txtRelated5.Text);
                myData.Add(txtLibraryImage1.Text);
                myData.Add(txtLibraryImage2);
                myData.Add(txtLibraryImage3);
                myData.Add(txtLibraryImage4);
                myData.Add(txtLibraryImage5);
                Session["myDatatable"] = myData;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('../Item/Item_Preview_Edit.aspx?Item_Code=" + ItemCode + "','_blank');", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                user = new UserRoleBL();
                ime = new Item_Master_Entity();
                imeBL = new Item_Master_BL();
                itemCategoryBL = new Item_Category_BL();
                Item_BL item = new Item_BL();
                 DataTable templatedt = new DataTable();
                templatedt = CreateTemplateTable();
                String[] colName = { "Template" };
                if (!Check_SpecialCharacter(colName, templatedt))
                {
                    if (ItemCode != null)
                    {
                        if (CheckExistsItemCode(ItemCode))
                        {
                            GlobalUI.MessageBox("Item_Code Already Exists!!Please Write Another Item Code");
                        }
                        else
                        {
                            int ItemID;
                            ime.Updated_By = UserID;
                            ime = GetItemData();
                            string str = CheckLength(ime);
                            if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than length bytes!"); }
                            else
                            {
                                if (ValidationUpdate())
                                {
                                    string option = null;
                                    if (!String.IsNullOrWhiteSpace(ItemCode))
                                    {
                                        option = "Save";
                                    }
                                    if (imeBL.SaveEdit(ime, option) > 0)    //btnsave
                                    {
                                        ItemID = imeBL.SelectItemID(ItemCode);
                                        //Insert Category List
                                        GetCategoryValueFromTextBox(ItemID, CategoryList);
                                        //Delete previous shop from Item_Shop table and then insert new shop or not
                                        InsertShopList(ItemID, ItemCode);
                                        //Delete previous photo from Item_Image table and then insert new photo or not
                                        InsertPhoto(ItemID);
                                        //Insert Item table when Itemcode is not exits in ItemTable
                                        InsertItem(ItemCode);
                                        //Insert into Item_Code_URL
                                        InsertItemCodeURL(ItemID);
                                        //Change Shop Status To Gray
                                        if (chkActive.Checked == true)
                                        {
                                            imeBL.ChangeExportStatusToPink(ItemCode, 1);
                                        }
                                        else
                                        {
                                            imeBL.ChangeExportStatusToPink(ItemCode, 2);
                                        }
                                        //Delete previous related item from Item_RelatedItem table and then insert new related item or not
                                        InsertRelatedItem(ItemID);
                                        //Delete previous option from Item_Option table and then insert new option or not
                                        InsertOption(ItemID);
                                        //Delete previous yahoo specific from Item_YahooSpecificValue table and then insert new yahoo specific or not
                                        if (YahooSpecificValue != null)
                                        {
                                            InsertYahooSpecificValue(ItemID);
                                        }
                                        //For sks-587
                                        if (ViewState["DailyDelivery"] != null)
                                        {
                                            imeBL.SetUnsetDailyDelivery(ItemCode, Convert.ToInt32(ViewState["DailyDelivery"]));
                                        }
                                        SaveTemplateDetail(ItemCode); // Insert or Update Template_Detail
                                        object referrer = ViewState["UrlReferrer"];
                                        string url = "Item_Master.aspx?Item_Code=" + ItemCode;
                                        string script = "window.onload = function(){ alert('";
                                        script += "Save Successfully";
                                        script += "');";
                                        script += "window.location = '";
                                        script += url;
                                        script += "'; }";
                                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        GlobalUI.MessageBox("Please Fill Item_Code!!!!");
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected DataTable CreateTemplateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Template");
            dt.Rows.Add(txtDetail_Template1.Text);
            dt.Rows.Add(txtDetail_Template_Content1.Text);
            dt.Rows.Add(txtDetail_Template2.Text);
            dt.Rows.Add(txtDetail_Template_Content2.Text);
            dt.Rows.Add(txtDetail_Template3.Text);
            dt.Rows.Add(txtDetail_Template_Content3.Text);
            dt.Rows.Add(txtDetail_Template4.Text);
            dt.Rows.Add(txtDetail_Template_Content4.Text);
            dt.Rows.Add(txtTemplate1.Text);
            dt.Rows.Add(txtTemplate_Content1.Text);
            dt.Rows.Add(txtTemplate2.Text);
            dt.Rows.Add(txtTemplate_Content2.Text);
            dt.Rows.Add(txtTemplate3.Text);
            dt.Rows.Add(txtTemplate_Content3.Text);
            dt.Rows.Add(txtTemplate4.Text);
            dt.Rows.Add(txtTemplate_Content4.Text);
            dt.Rows.Add(txtTemplate5.Text);
            dt.Rows.Add(txtTemplate_Content5.Text);
            dt.Rows.Add(txtTemplate6.Text);
            dt.Rows.Add(txtTemplate_Content6.Text);
            dt.Rows.Add(txtTemplate7.Text);
            dt.Rows.Add(txtTemplate_Content7.Text);
            dt.Rows.Add(txtTemplate8.Text);
            dt.Rows.Add(txtTemplate_Content8.Text);
            dt.Rows.Add(txtTemplate9.Text);
            dt.Rows.Add(txtTemplate_Content9.Text);
            dt.Rows.Add(txtTemplate10.Text);
            dt.Rows.Add(txtTemplate_Content10.Text);
            dt.Rows.Add(txtTemplate11.Text);
            dt.Rows.Add(txtTemplate_Content11.Text);
            dt.Rows.Add(txtTemplate12.Text);
            dt.Rows.Add(txtTemplate_Content12.Text);
            dt.Rows.Add(txtTemplate13.Text);
            dt.Rows.Add(txtTemplate_Content13.Text);
            dt.Rows.Add(txtTemplate14.Text);
            dt.Rows.Add(txtTemplate_Content14.Text);
            dt.Rows.Add(txtTemplate15.Text);
            dt.Rows.Add(txtTemplate_Content15.Text);
            dt.Rows.Add(txtTemplate16.Text);
            dt.Rows.Add(txtTemplate_Content16.Text);
            dt.Rows.Add(txtTemplate17.Text);
            dt.Rows.Add(txtTemplate_Content17.Text);
            dt.Rows.Add(txtTemplate18.Text);
            dt.Rows.Add(txtTemplate_Content18.Text);
            dt.Rows.Add(txtTemplate19.Text);
            dt.Rows.Add(txtTemplate_Content19.Text);
            dt.Rows.Add(txtTemplate20.Text);
            dt.Rows.Add(txtTemplate_Content20.Text);
            dt.Rows.Add(txtItem_Description_PC.Text);
            dt.Rows.Add(txtSale_Description_PC.Text);
            dt.Rows.Add(txtSmart_Template.Text);
            dt.Rows.Add(txtMerchandise_Information.Text);

            return dt;
        }

        public void MessageBox(string message)
        {
            try
            {
                if (message == "Saving Successful ! " || message == "Updating Successful ! " || message == "Data Complete ! ")
                {
                    string script = "<script type=\"text/javascript\">alert('" + message + "');</script>";
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                    {
                        page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                    }
                }
                else if (message == "Delete Successful ! ")
                {
                    object referrer = ViewState["UrlReferrer"];
                    string url = (string)referrer;
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
                else
                {
                    string cleanMessage = message.Replace("'", "\\'");
                    string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                    {
                        page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public bool CheckExistsItemCode(string ItemCode)
        {
            imeBL = new Item_Master_BL();
            int result = imeBL.CheckExistsItemCode(ItemCode);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Item_Master_Entity GetItemData()
        {
            try
            {
                ime.Ctrl_ID = hdfCtrl_ID.Value;
                if (!String.IsNullOrWhiteSpace(ItemCode))
                {
                    ime.Item_Code = txtItem_Code.Text.TrimStart();
                }
                ime.Updated_By = UserID;
                ime.Item_Name = txtItem_Name.Text.TrimStart();
                ime.Product_Code = txtProduct_Code.Text.TrimStart();
                string release = Request.Form[txtRelease_Date.UniqueID];
                string post = Request.Form[txtPost_Available_Date.UniqueID];
                if (!string.IsNullOrWhiteSpace(release))
                {
                    ime.Release_Date = Convert.ToDateTime(release);
                }
                if (!string.IsNullOrWhiteSpace(post))
                {
                    ime.Post_Available_Date = Convert.ToDateTime(post);
                }
                ime.Season = txtSeason.Text.TrimStart();
                ime.Brand_Name = txtBrand_Name.Text.TrimStart();
                ime.Brand_Code = txtBrand_Code.Text.TrimStart();
                ime.Competition_Name = txtCompetition_Name.Text.TrimStart();
                ime.Class_Name = txtClass_Name.Text.Trim().TrimStart();
                ime.Catalog_Information = txtCatalog_Information.Text.TrimStart();
                ime.Merchandise_Information = txtMerchandise_Information.Text.TrimStart();
                ime.Zett_Item_Description = txtZett_Item_Description.Text.TrimStart();
                ime.Zett_Sale_Description = txtZett_Sale_Description.Text.TrimStart();
                ime.Item_Description_PC = txtItem_Description_PC.Text.TrimStart();
                ime.Sale_Description_PC = txtSale_Description_PC.Text.TrimStart();
                ime.Smart_Template = txtSmart_Template.Text;
                ime.Additional_2 = txtAdditional_2.Text;
                ime.Additional_3 = txtAdditional_3.Text;
                ime.BlackMarket_Password = txtBlackMarket_Password.Text.TrimStart();
                ime.DoublePrice_Ctrl_No = txtDoublePrice_Ctrl_No.Text.TrimStart();
                if (!string.IsNullOrWhiteSpace(txtExtra_Shipping.Text))
                {
                    ime.Extra_Shipping = Convert.ToInt32(txtExtra_Shipping.Text.TrimStart());
                }
                ime.Year = txtYear.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txtList_Price.Text))
                {
                    ime.List_Price = int.Parse(txtList_Price.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtSale_Price.Text))
                {
                    ime.Sale_Price = int.Parse(txtSale_Price.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtJisha_Price.Text))
                {
                    ime.Jisha_Price  = int.Parse(txtJisha_Price.Text.Replace(",", string.Empty));
                }
                ime.Rakuten_CategoryID = txtRakuten_CategoryID.Text;
                ime.Yahoo_CategoryID = txtYahoo_CategoryID.Text;
                ime.Wowma_CategoryID = txtPonpare_CategoryID.Text;
                if (!string.IsNullOrWhiteSpace(ddlShipping_Flag.SelectedValue))
                {
                    ime.Shipping_Flag = int.Parse(ddlShipping_Flag.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlDelivery_Charges.SelectedValue))
                {
                    ime.Delivery_Charges = int.Parse(ddlDelivery_Charges.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlWarehouse_Specified.SelectedValue))
                {
                    ime.Warehouse_Specified = int.Parse(ddlWarehouse_Specified.SelectedValue);
                }
                if (chkActive.Checked == true)
                { ime.Active = 1; }
                else
                { ime.Active = 0; }
                ime.InactiveComment = txtInactive.Text;
                ime.Yahoo_url = txtyahoourl.Text; //for sks-593
                if (rdb1.Checked)
                {
                    ime.Skucheck = 1;
                }
                else
                {
                    ime.Skucheck = 0;
                }
                ime.SalesUnit = ddlsalesunit.SelectedItem.Text;
                ime.ContentQuantityNo1 = txtcontentquantityunitno1.Text;
                ime.ContentQuantityNo2 = txtcontentquantityunitno2.Text;
                ime.ContentUnit1 = ddlcontentunit1.SelectedItem.Text;
                ime.ContentUnit2 = ddlcontentunit2.SelectedItem.Text;
                ime.PC_CatchCopy= txtCatchCopy.Text ;
                ime.PC_CatchCopy_Mobile = txtCatchCopyMobile.Text;
                ime.Maker_Name = txtmakername.Text.TrimStart();
                ime.Comment = txtcomment.Text;
                if (!String.IsNullOrWhiteSpace(txtsellingprice.Text))
                {
                    ime.Selling_Price = int.Parse(txtsellingprice.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(txtpurchaseprice.Text))
                {
                    ime.Purchase_Price = int.Parse(txtpurchaseprice.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(txtsellby.Text))
                {
                    ime.SellBy = int.Parse(txtsellby.Text.TrimStart());
                }
                ime.Selling_Rank = txtsellingrank.Text.TrimStart();
                if (!String.IsNullOrWhiteSpace(txtdeliverydays.Text))
                {
                    ime.Delivery_Days = int.Parse(txtdeliverydays.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(ddlksmavaliable.SelectedValue))
                {
                    ime.KSMDelivery_Type = int.Parse(ddlksmavaliable.SelectedValue);
                }
                if (!String.IsNullOrWhiteSpace(txtksmdeliverydays.Text))
                {
                    ime.KSMDelivery_Days = int.Parse(txtksmdeliverydays.Text.TrimStart());
                }
                ime.Nation_Wide = txtnationwide.Text;
                if (!String.IsNullOrWhiteSpace(txthokkaido.Text))
                {
                    ime.Hokkaido = int.Parse(txthokkaido.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(txtokinawa.Text))
                {
                    ime.Okinawa = int.Parse(txtokinawa.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(txtremoteisland.Text))
                {
                    ime.Remote_Island = int.Parse(txtremoteisland.Text.TrimStart());
                }
                ime.Undelivered_Area = txtundeliveredarea.Text.TrimStart();
                ime.Dangerous_Goods_Contents = txtdangerousgoodscontents.Text;
                if (!string.IsNullOrWhiteSpace(ddldeliverymethod.SelectedValue))
                {
                    ime.Delivery_Method = int.Parse(ddldeliverymethod.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddldeliverytype.SelectedValue))
                {
                    ime.Delivery_Type = int.Parse(ddldeliverytype.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddldeliveryfees.SelectedValue))
                {
                    ime.Delivery_Fees = int.Parse(ddldeliveryfees.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlcustomerassembly.SelectedValue))
                {
                    ime.KSM_Avaliable = int.Parse(ddlcustomerassembly.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlreturnableitem.SelectedValue))
                {
                    ime.Returnable_Item = int.Parse(ddlreturnableitem.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlnoapplicablelaw.SelectedValue))
                {
                    ime.NoApplicable_Law = int.Parse(ddlnoapplicablelaw.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlsalespermission.SelectedValue))
                {
                    ime.Sales_Permission = int.Parse(ddlsalespermission.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddllaw.SelectedValue))
                {
                    ime.Law = int.Parse(ddllaw.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddldanggoodsclass.SelectedValue))
                {
                    ime.Dangoods_Class = int.Parse(ddldanggoodsclass.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddldanggoodsname.SelectedValue))
                {
                    ime.Dangoods_Name = int.Parse(ddldanggoodsname.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlriskrating.SelectedValue))
                {
                    ime.Risk_Rating = int.Parse(ddlriskrating.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddldanggoodsnature.SelectedValue))
                {
                    ime.Dangoods_Nature = int.Parse(ddldanggoodsnature.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlfirelaw.SelectedValue))
                {
                    ime.Fire_Law = int.Parse(ddlfirelaw.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(txtcost.Text))
                {
                    ime.Cost = int.Parse(txtcost.Text.Replace(",", string.Empty));
                }
                return ime;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
                return new Item_Master_Entity();
            }
        }

        protected string CheckLength(Item_Master_Entity ime)
        {
            try
            {
                string msg = string.Empty; int byteLength = 0;
                Encoding enc = Encoding.GetEncoding(932);
                byteLength = enc.GetByteCount(ime.Ctrl_ID);
                if (byteLength > 50)
                {
                    msg += ime.Ctrl_ID + ",";
                }
                byteLength = enc.GetByteCount(ime.Product_Code);
                if (byteLength > 100)
                {
                    msg += "製品コード" + ",";
                }
                byteLength = enc.GetByteCount(ime.Item_Name);
                if (byteLength > 255)
                {
                    msg += "商品名" + ",";
                }
                byteLength = enc.GetByteCount(ime.Year);
                if (byteLength > 20)
                {
                    msg += "年度" + ",";
                }
                byteLength = enc.GetByteCount(ime.Season);
                if (byteLength > 40)
                {
                    msg += "シーズン" + ",";
                }
                byteLength = enc.GetByteCount(ime.Brand_Name);
                if (byteLength > 200)
                {
                    msg += "ブランド名" + ",";
                }
                byteLength = enc.GetByteCount(ime.Brand_Code);
                if (byteLength > 4)
                {
                    msg += "ブランドコード" + ",";
                }
                byteLength = enc.GetByteCount(ime.Competition_Name);
                if (byteLength > 200)
                {
                    msg += "競技名" + ",";
                }
                byteLength = enc.GetByteCount(ime.Class_Name);
                if (byteLength > 200)
                {
                    msg += "分類名" + ",";
                }
                byteLength = enc.GetByteCount(ime.Catalog_Information);
                if (byteLength > 3000)
                {
                    msg += "カタログ情報" + ",";
                }
                byteLength = enc.GetByteCount(ime.Rakuten_CategoryID);
                if (byteLength > 50)
                {
                    msg += "楽天 カテゴリID" + ",";
                }
                byteLength = enc.GetByteCount(ime.Yahoo_CategoryID);
                if (byteLength > 50)
                {
                    msg += "ヤフー カテゴリID" + ",";
                }
                byteLength = enc.GetByteCount(ime.Wowma_CategoryID);
                if (byteLength > 50)
                {
                    msg += "ポンパレ カテゴリID" + ",";
                }
                byteLength = enc.GetByteCount(ime.BlackMarket_Password);
                if (byteLength > 50)
                {
                    msg += "闇市パスワード" + ",";
                }
                byteLength = enc.GetByteCount(ime.DoublePrice_Ctrl_No);
                if (byteLength > 50)
                {
                    msg += "二重価格文書管理番号" + ",";
                }
                return msg;
            }
            catch (Exception ex)
            {
                string str = string.Empty;
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return str;
            }
        }

        public bool ValidationUpdate()
        {
            try
            {
                #region textboxvalidate
                if (String.IsNullOrEmpty(txtItem_Code.Text))
                {
                    MessageBox("Please Enter Item_Code!");
                    return false;
                }
                if (txtItem_Code.Text.Contains(".") || txtItem_Code.Text.Contains(" "))
                {
                    MessageBox("Item_Code format is invalid!");
                    return false;
                }
                #endregion
                #region html field
                int length = Encoding.GetEncoding(932).GetByteCount(txtItem_Description_PC.Text);
                if (length > 5120)
                {
                    MessageBox("PC用商品説明文は5120文字までです。");
                    return false;
                }
                length = Encoding.GetEncoding(932).GetByteCount(txtSale_Description_PC.Text);
                if (length > 5120)
                {
                    MessageBox("PC用販売説明文は5120文字までです。");
                    return false;
                }
                #endregion
                #region Library Image
                if (!string.IsNullOrEmpty(txtLibraryImage1.Text))
                {
                    if (txtLibraryImage1.Text.Length > 4)
                    {
                        if (!txtLibraryImage1.Text.ToLower().Contains(".gif") && !txtLibraryImage1.Text.ToLower().Contains(".jpg") && !txtLibraryImage1.Text.ToLower().Contains(".png"))
                        {
                            MessageBox("Invalid extension of photo in ライブラリ画像 first field ! ");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox("Invalid name of photo in ライブラリ画像 first field ! ");
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtLibraryImage2.Text))
                {
                    if (txtLibraryImage2.Text.Length > 4)
                    {
                        if (!txtLibraryImage2.Text.ToLower().Contains(".gif") && !txtLibraryImage2.Text.ToLower().Contains(".jpg") && !txtLibraryImage2.Text.ToLower().Contains(".png"))
                        {
                            MessageBox("Invalid extension of photo in ライブラリ画像 second field ! ");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox("Invalid name of photo in ライブラリ画像 second field ! ");
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtLibraryImage3.Text))
                {
                    if (txtLibraryImage3.Text.Length > 4)
                    {
                        if (!txtLibraryImage3.Text.ToLower().Contains(".gif") && !txtLibraryImage3.Text.ToLower().Contains(".jpg") && !txtLibraryImage3.Text.ToLower().Contains(".png"))
                        {
                            MessageBox("Invalid extension of photo in ライブラリ画像 third field ! ");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox("Invalid name of photo in ライブラリ画像 third field ! ");
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtLibraryImage4.Text))
                {
                    if (txtLibraryImage4.Text.Length > 4)
                    {
                        if (!txtLibraryImage4.Text.ToLower().Contains(".gif") && txtLibraryImage4.Text.ToLower().Contains(".jpg") && txtLibraryImage4.Text.ToLower().Contains(".png"))
                        {
                            MessageBox("Invalid extension of photo in ライブラリ画像 fourth field ! ");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox("Invalid name of photo in ライブラリ画像 fourth field ! ");
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtLibraryImage5.Text))
                {
                    if (txtLibraryImage5.Text.Length > 4)
                    {
                        if (!txtLibraryImage5.Text.ToLower().Contains(".gif") && !txtLibraryImage5.Text.ToLower().Contains(".jpg") && !txtLibraryImage5.Text.ToLower().Contains(".png"))
                        {
                            MessageBox("Invalid extension of photo in ライブラリ画像 fifth field ! ");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox("Invalid name of photo in ライブラリ画像 fifth field ! ");
                        return false;
                    }
                }
                if (String.IsNullOrEmpty(txtInactive.Text) && chkActive.Checked == true)
                {
                    MessageBox("Write a comment for inactive! ");
                    return false;
                }
                #endregion
                foreach (char url in txtItem_Code.Text)
                {
                    if (url == '.' || char.IsUpper(url) || char.IsWhiteSpace(url))
                    {
                        MessageBox("Item Code is incorrect format.");
                        return false;
                    }
                }
                foreach (DataListItem li in dlShop.Items)
                {
                    TextBox txtitemcode = li.FindControl("txtItem_CodeList") as TextBox;
                    if (String.IsNullOrEmpty(txtitemcode.Text))
                    {
                        MessageBox("Please fill Item_Code_URL textbox for all shop !!");
                        return false;
                    }
                    foreach (char url in txtitemcode.Text)
                    {
                        if (url == '.' || char.IsUpper(url) || char.IsWhiteSpace(url))
                        {
                            MessageBox("Item_Code_URL is incorrect format.");
                            return false;
                        }
                    }
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

        public void GetCategoryValueFromTextBox(int ItemID, DataTable CategoryList)
        {
            itemCategoryBL = new Item_Category_BL();
            CreatenewDataTable();
            string cat = null;
            DataTable dtnew = (DataTable)ViewState["DataTablenew"];
            DataRow dr = null;
            foreach (GridViewRow gvrow in gvCategory.Rows)
            {
                TextBox box1 = gvrow.FindControl("txtCategory") as TextBox;
                TextBox box2 = gvrow.FindControl("txtSN") as TextBox;
                dr = dtnew.NewRow();
                dr["Category"] = box1.Text;
                dr["SN"] = box2.Text;
                if ((!String.IsNullOrWhiteSpace(box1.Text)) && (!String.IsNullOrWhiteSpace(box2.Text)))
                {
                    dtnew.Rows.Add(dr);
                }
            }
            if (dtnew != null && dtnew.Rows.Count > 0)
            {
                cat = dtnew.Rows[0]["Category"].ToString();
            }
            if (!String.IsNullOrWhiteSpace(cat))
            {
                DataTable dt = itemCategoryBL.CheckCategory(ItemID, dtnew);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<DataRow> rowsToDelete = new List<DataRow>();
                    foreach (DataRow drnew in dtnew.Rows)
                    {
                        if (drnew["Category"].ToString() == dt.Rows[0]["CName"].ToString())
                            rowsToDelete.Add(drnew);
                    }
                    foreach (var r in rowsToDelete)
                    {
                        dtnew.Rows.Remove(r);
                    }
                    dt.Merge(CategoryList);
                }
                if (dtnew != null && dtnew.Rows.Count > 0)
                {
                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        string str = dtnew.Rows[i]["Category"].ToString();
                        string catsn = dtnew.Rows[i]["SN"].ToString();
                        int sn = Convert.ToInt32(catsn);
                        string path = null;
                        string[] strsplit = null;
                        if (str.Contains('\\'))
                        {
                            strsplit = str.Split('\\');
                        }
                        if (str.Contains('￥'))
                        {
                            strsplit = str.Split('￥');
                        }
                        if (strsplit != null)
                        {
                            for (int j = 0; j < strsplit.Count() - 1; j++)
                            {
                                string check = strsplit[j];
                                path += strsplit[j] + "\\";
                                if (j == 0)
                                {
                                    int catid = itemCategoryBL.CheckDescription(check, sn, 0, 0, path);
                                    hdfCatID.Value = catid.ToString();
                                }
                                else
                                {
                                    int catno = Convert.ToInt32(hdfCatID.Value);
                                    int catid = itemCategoryBL.CheckDescription(check, sn, 1, catno, path);
                                    hdfCatID.Value = catid.ToString();
                                }
                            }
                            hdfCatID.Value = "";
                        }
                        DataTable dtcopy = itemCategoryBL.CheckCategory(ItemID, dtnew);
                        dt.Merge(dtcopy);
                    }
                    itemCategoryBL.Insert(ItemID, dt);
                    Session.Remove("CategoryList_" + ItemCode);
                }
            }
            else
            {
                if (CategoryList != null)
                {
                    itemCategoryBL.Insert(ItemID, CategoryList);
                    Session.Remove("CategoryList_" + ItemCode);
                }
            }
        }

        public void CreatenewDataTable()
        {
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtnew.Columns.Add(new DataColumn("Category", typeof(string)));
            dtnew.Columns.Add(new DataColumn("SN", typeof(string)));
            ViewState["DataTablenew"] = dtnew;
        }

        public void InsertShopList(int itemID, string itemcode)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemID", typeof(int));
                dt.Columns.Add("ShopID", typeof(int));
                dt.Columns.Add("ItemCode", typeof(string));
                foreach (DataListItem li in dlShop1.Items)
                {
                    Label lbl = li.FindControl("lblMall1ShopID") as Label;
                    CheckBox cb = li.FindControl("ckbMall1Shop") as CheckBox;
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ItemID"] = itemID;
                            dr["ShopID"] = Convert.ToInt32(lbl.Text);
                            dr["ItemCode"] = itemcode;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                foreach (DataListItem li in dlShop2.Items)
                {
                    Label lbl = li.FindControl("lblMall2ShopID") as Label;
                    CheckBox cb = li.FindControl("ckbMall2Shop") as CheckBox;
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ItemID"] = itemID;
                            dr["ShopID"] = Convert.ToInt32(lbl.Text);
                            dr["ItemCode"] = itemcode;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                foreach (DataListItem li in dlShop3.Items)
                {
                    Label lbl = li.FindControl("lblMall3ShopID") as Label;
                    CheckBox cb = li.FindControl("ckbMall3Shop") as CheckBox;
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ItemID"] = itemID;
                            dr["ShopID"] = Convert.ToInt32(lbl.Text);
                            dr["ItemCode"] = itemcode;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                foreach (DataListItem li in dlShop4.Items)
                {
                    Label lbl = li.FindControl("lblMall4ShopID") as Label;
                    CheckBox cb = li.FindControl("ckbMall4Shop") as CheckBox;
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ItemID"] = itemID;
                            dr["ShopID"] = Convert.ToInt32(lbl.Text);
                            dr["ItemCode"] = itemcode;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                foreach (DataListItem li in dlShop5.Items)
                {
                    Label lbl = li.FindControl("lblMall5ShopID") as Label;
                    CheckBox cb = li.FindControl("ckbMall5Shop") as CheckBox;
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ItemID"] = itemID;
                            dr["ShopID"] = Convert.ToInt32(lbl.Text);
                            dr["ItemCode"] = itemcode;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                Item_Shop_BL itemShopBL = new Item_Shop_BL();
                int flg = flag;
                int realflag;
                if (ViewState["flag"] == null)
                {
                    realflag = 2;
                }
                else
                {
                    realflag = Convert.ToInt32(ViewState["flag"]);
                }
                if (realflag != 2)
                {
                    itemShopBL.Check_ItemShopForAmazon(itemID, realflag, UserID);
                }
                itemShopBL.Insert(dt, itemID);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void InsertPhoto(int itemID)
        {
            try
            {
                DataTable dtImage = ImageList as DataTable;
                Item_Image_BL itemImageBL = new Item_Image_BL();
                dtImage = SetLibraryPhoto(dtImage);
                if (dtImage.Rows.Count > 0)
                {
                    DataRow[] dr = dtImage.Select("Image_Type='0'");
                    if (dr.Length > 0)
                    {
                        DataTable dtImage0 = dtImage.Select("Image_Type='0'").CopyToDataTable();

                        List<DataRow> rows_to_remove = new List<DataRow>();
                        foreach (DataRow row1 in dtImage.Rows)
                        {
                            if (int.Parse(row1["Image_Type"].ToString()) == 0)
                            {
                                rows_to_remove.Add(row1);
                            }
                        }
                        foreach (DataRow row in rows_to_remove)
                        {
                            dtImage.Rows.Remove(row);
                            dtImage.AcceptChanges();
                        }

                        dtImage.Merge(RemoveDuplicateRows(dtImage0, "SN"));
                    }
                }
                itemImageBL.Insert(itemID, dtImage);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public DataTable SetLibraryPhoto(DataTable dt)
        {
            try
            {
                if (dt == null) // not exist ImageList
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Item_ID", typeof(int)));
                    dt.Columns.Add(new DataColumn("Image_Name", typeof(string)));
                    dt.Columns.Add(new DataColumn("Image_Type", typeof(int)));
                    dt.Columns.Add(new DataColumn("SN", typeof(int)));

                    #region LibraryImage
                    DataRow dr1 = dt.NewRow();
                    dr1["Image_Name"] = txtLibraryImage1.Text.TrimStart();
                    dr1["Image_Type"] = 1;
                    dr1["SN"] = 1;
                    dt.Rows.Add(dr1);
                    DataRow dr2 = dt.NewRow();
                    dr2["Image_Name"] = txtLibraryImage2.Text.TrimStart();
                    dr2["Image_Type"] = 1;
                    dr2["SN"] = 2;
                    dt.Rows.Add(dr2);
                    DataRow dr3 = dt.NewRow();
                    dr3["Image_Name"] = txtLibraryImage3.Text.TrimStart();
                    dr3["Image_Type"] = 1;
                    dr3["SN"] = 3;
                    dt.Rows.Add(dr3);
                    DataRow dr4 = dt.NewRow();
                    dr4["Image_Name"] = txtLibraryImage4.Text.TrimStart();
                    dr4["Image_Type"] = 1;
                    dr4["SN"] = 4;
                    dt.Rows.Add(dr4);
                    DataRow dr5 = dt.NewRow();
                    dr5["Image_Name"] = txtLibraryImage5.Text.TrimStart();
                    dr5["Image_Type"] = 1;
                    dr5["SN"] = 5;
                    dt.Rows.Add(dr5);
                    DataRow dr6 = dt.NewRow();
                    dr6["Image_Name"] = txtLibraryImage6.Text.TrimStart();
                    dr6["Image_Type"] = 1;
                    dr6["SN"] = 6;
                    dt.Rows.Add(dr6);
                    #endregion

                    #region CampaginImage
                    DataRow dr7 = dt.NewRow();
                    dr7["Image_Name"] = txtCampaignImage1.Text.TrimStart();
                    dr7["Image_Type"] = 2;
                    dr7["SN"] = 1;
                    dt.Rows.Add(dr7);
                    DataRow dr8 = dt.NewRow();
                    dr8["Image_Name"] = txtCampaignImage2.Text.TrimStart();
                    dr8["Image_Type"] = 2;
                    dr8["SN"] = 2;
                    dt.Rows.Add(dr8);
                    DataRow dr9 = dt.NewRow();
                    dr9["Image_Name"] = txtCampaignImage3.Text.TrimStart();
                    dr9["Image_Type"] = 2;
                    dr9["SN"] = 3;
                    dt.Rows.Add(dr9);
                    DataRow dr10 = dt.NewRow();
                    dr10["Image_Name"] = txtCampaignImage4.Text.TrimStart();
                    dr10["Image_Type"] = 2;
                    dr10["SN"] = 4;
                    dt.Rows.Add(dr10);
                    DataRow dr11 = dt.NewRow();
                    dr11["Image_Name"] = txtCampaignImage5.Text.TrimStart();
                    dr11["Image_Type"] = 2;
                    dr11["SN"] = 5;
                    dt.Rows.Add(dr11);
                    #endregion
                    return dt;
                }
                else   //exist ImageList
                {

                    #region delete row
                    DataRow[] dr = dt.Select("Image_Type='1' OR Image_Type='2'");
                    if (dr.Length > 0)
                    {
                        DataTable dtLibrary = dt.Select("Image_Type='1' OR Image_Type='2'").CopyToDataTable();
                        List<DataRow> rows_to_remove = new List<DataRow>();
                        foreach (DataRow row1 in dt.Rows)
                        {
                            if (int.Parse(row1["Image_Type"].ToString()) == 1 || int.Parse(row1["Image_Type"].ToString()) == 2)
                            {
                                rows_to_remove.Add(row1);
                            }
                        }
                        foreach (DataRow row in rows_to_remove)
                        {
                            dt.Rows.Remove(row);
                            dt.AcceptChanges();
                        }
                    }
                    #endregion

                    #region LibraryImage
                    DataRow dr1 = dt.NewRow();
                    dr1["Image_Name"] = txtLibraryImage1.Text;
                    dr1["Image_Type"] = 1;
                    dr1["SN"] = 1;
                    dt.Rows.Add(dr1);
                    DataRow dr2 = dt.NewRow();
                    dr2["Image_Name"] = txtLibraryImage2.Text;
                    dr2["Image_Type"] = 1;
                    dr2["SN"] = 2;
                    dt.Rows.Add(dr2);
                    DataRow dr3 = dt.NewRow();
                    dr3["Image_Name"] = txtLibraryImage3.Text;
                    dr3["Image_Type"] = 1;
                    dr3["SN"] = 3;
                    dt.Rows.Add(dr3);
                    DataRow dr4 = dt.NewRow();
                    dr4["Image_Name"] = txtLibraryImage4.Text;
                    dr4["Image_Type"] = 1;
                    dr4["SN"] = 4;
                    dt.Rows.Add(dr4);
                    DataRow dr5 = dt.NewRow();
                    dr5["Image_Name"] = txtLibraryImage5.Text;
                    dr5["Image_Type"] = 1;
                    dr5["SN"] = 5;
                    dt.Rows.Add(dr5);
                    DataRow dr6 = dt.NewRow();
                    dr6["Image_Name"] = txtLibraryImage6.Text;
                    dr6["Image_Type"] = 1;
                    dr6["SN"] = 6;
                    dt.Rows.Add(dr6);
                    #endregion

                    #region CampaginImage
                    DataRow dr7 = dt.NewRow();
                    dr7["Image_Name"] = txtCampaignImage1.Text;
                    dr7["Image_Type"] = 2;
                    dr7["SN"] = 1;
                    dt.Rows.Add(dr7);
                    DataRow dr8 = dt.NewRow();
                    dr8["Image_Name"] = txtCampaignImage2.Text;
                    dr8["Image_Type"] = 2;
                    dr8["SN"] = 2;
                    dt.Rows.Add(dr8);
                    DataRow dr9 = dt.NewRow();
                    dr9["Image_Name"] = txtCampaignImage3.Text;
                    dr9["Image_Type"] = 2;
                    dr9["SN"] = 3;
                    dt.Rows.Add(dr9);
                    DataRow dr10 = dt.NewRow();
                    dr10["Image_Name"] = txtCampaignImage4.Text;
                    dr10["Image_Type"] = 2;
                    dr10["SN"] = 4;
                    dt.Rows.Add(dr10);
                    DataRow dr11 = dt.NewRow();
                    dr11["Image_Name"] = txtCampaignImage5.Text;
                    dr11["Image_Type"] = 2;
                    dr11["SN"] = 5;
                    dt.Rows.Add(dr11);
                    #endregion
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
                return new DataTable();
            }
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string col_SN)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();
            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if ((hTable.Contains(drow[col_SN])))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[col_SN], string.Empty);
            }
            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);
            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        public void InsertItem(string ItemCode)
        {
            imeBL = new Item_Master_BL();
            imeBL.InsertItemInventory(ItemCode);
        }

        public void InsertItemCodeURL(int ItemID)
        {
            Item_Shop_BL isbl = new Item_Shop_BL();
            DataTable dt = new DataTable();
            dt.Columns.Add("Item_ID", typeof(int));
            dt.Columns.Add("Shop_ID", typeof(int));
            dt.Columns.Add("Item_Code_URL", typeof(string));
            foreach (DataListItem li in dlShop.Items)
            {
                TextBox txtitemcode = li.FindControl("txtItem_CodeList") as TextBox;
                Label shopid = li.FindControl("lblShopID") as Label;
                CheckBox cb = li.FindControl("ckbShop") as CheckBox;
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Item_ID"] = ItemID;
                        dr["Item_Code_URL"] = txtitemcode.Text;
                        dr["Shop_ID"] = Convert.ToInt32(shopid.Text);
                        dt.Rows.Add(dr);
                    }
                }
            }
            isbl.InsertItemCodeURL(dt, ItemID);
        }

        public void InsertRelatedItem(int itemID)
        {
            try
            {
                DataTable dtRelated = new DataTable();
                dtRelated.Columns.Add(new DataColumn("Item_ID", typeof(int)));
                dtRelated.Columns.Add(new DataColumn("Related_ItemCode", typeof(string)));
                dtRelated.Columns.Add(new DataColumn("SN", typeof(int)));
                if (!string.IsNullOrWhiteSpace(txtRelated1.Text))
                {
                    DataRow dr1 = dtRelated.NewRow();
                    dr1["Item_ID"] = itemID;
                    dr1["Related_ItemCode"] = txtRelated1.Text.TrimStart();
                    dr1["SN"] = 1;
                    dtRelated.Rows.Add(dr1);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated2.Text))
                {
                    DataRow dr2 = dtRelated.NewRow();
                    dr2["Item_ID"] = itemID;
                    dr2["Related_ItemCode"] = txtRelated2.Text.TrimStart();
                    dr2["SN"] = 2;
                    dtRelated.Rows.Add(dr2);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated3.Text))
                {
                    DataRow dr3 = dtRelated.NewRow();
                    dr3["Item_ID"] = itemID;
                    dr3["Related_ItemCode"] = txtRelated3.Text.TrimStart();
                    dr3["SN"] = 3;
                    dtRelated.Rows.Add(dr3);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated4.Text))
                {
                    DataRow dr4 = dtRelated.NewRow();
                    dr4["Item_ID"] = itemID;
                    dr4["Related_ItemCode"] = txtRelated4.Text.TrimStart();
                    dr4["SN"] = 4;
                    dtRelated.Rows.Add(dr4);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated5.Text))
                {
                    DataRow dr5 = dtRelated.NewRow();
                    dr5["Item_ID"] = itemID;
                    dr5["Related_ItemCode"] = txtRelated5.Text.TrimStart();
                    dr5["SN"] = 5;
                    dtRelated.Rows.Add(dr5);
                }
                Item_Related_Item_BL ItemRelatedBL = new Item_Related_Item_BL();
                ItemRelatedBL.Insert(itemID, dtRelated);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void InsertOption(int itemID)
        {
            try
            {
                Item_Option_BL ItemOptionBL = new Item_Option_BL();
                GetOption();
                ItemOptionBL.Insert(itemID, Option);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void GetOption()
        {
            try
            {
                if (Option != null)
                {
                    Option.Rows[0]["Name1"] = txtOptionName1.Text;
                    Option.Rows[0]["Name2"] = txtOptionName2.Text;
                    Option.Rows[0]["Name3"] = txtOptionName3.Text;
                    Option.Rows[0]["Value1"] = txtOptionValue1.Text;
                    Option.Rows[0]["Value2"] = txtOptionValue2.Text;
                    Option.Rows[0]["Value3"] = txtOptionValue3.Text;
                    Option.AcceptChanges();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name1", typeof(string));
                    dt.Columns.Add("Value1", typeof(string));
                    dt.Columns.Add("Name2", typeof(string));
                    dt.Columns.Add("Value2", typeof(string));
                    dt.Columns.Add("Name3", typeof(string));
                    dt.Columns.Add("Value3", typeof(string));
                    dt.Rows.Add(txtOptionName1.Text, txtOptionValue1.Text, txtOptionName2.Text, txtOptionValue2.Text, txtOptionName3.Text, txtOptionValue3.Text);
                    dt.AcceptChanges();
                    Session["Option_" + ItemCode] = dt;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void InsertYahooSpecificValue(int ItemID)
        {
            try
            {
                Item_YahooSpecificValue_BL YahooSpecificValueBL = new Item_YahooSpecificValue_BL();
                YahooSpecificValueBL.Insert(ItemID, YahooSpecificValue);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void SaveTemplateDetail(string item_code)
        {
            Template_Detail_Entity tde = new Template_Detail_Entity();
            Template_Detail_BL tdbl = new Template_Detail_BL();

            tde.Template1 = txtTemplate1.Text;
            tde.Template2 = txtTemplate2.Text;
            tde.Template3 = txtTemplate3.Text;
            tde.Template4 = txtTemplate4.Text;
            tde.Template5 = txtTemplate5.Text;
            tde.Template6 = txtTemplate6.Text;
            tde.Template7 = txtTemplate7.Text;
            tde.Template8 = txtTemplate8.Text;
            tde.Template9 = txtTemplate9.Text;
            tde.Template10 = txtTemplate10.Text;
            tde.Template11 = txtTemplate11.Text;
            tde.Template12 = txtTemplate12.Text;
            tde.Template13 = txtTemplate13.Text;
            tde.Template14 = txtTemplate14.Text;
            tde.Template15 = txtTemplate15.Text;
            tde.Template16 = txtTemplate16.Text;
            tde.Template17 = txtTemplate17.Text;
            tde.Template18 = txtTemplate18.Text;
            tde.Template19 = txtTemplate19.Text;
            tde.Template20 = txtTemplate20.Text;

            tde.Template_Content1 = txtTemplate_Content1.Text;
            tde.Template_Content2 = txtTemplate_Content2.Text;
            tde.Template_Content3 = txtTemplate_Content3.Text;
            tde.Template_Content4 = txtTemplate_Content4.Text;
            tde.Template_Content5 = txtTemplate_Content5.Text;
            tde.Template_Content6 = txtTemplate_Content6.Text;
            tde.Template_Content7 = txtTemplate_Content7.Text;
            tde.Template_Content8 = txtTemplate_Content8.Text;
            tde.Template_Content9 = txtTemplate_Content9.Text;
            tde.Template_Content10 = txtTemplate_Content10.Text;
            tde.Template_Content11 = txtTemplate_Content11.Text;
            tde.Template_Content12 = txtTemplate_Content12.Text;
            tde.Template_Content13 = txtTemplate_Content13.Text;
            tde.Template_Content14 = txtTemplate_Content14.Text;
            tde.Template_Content15 = txtTemplate_Content15.Text;
            tde.Template_Content16 = txtTemplate_Content16.Text;
            tde.Template_Content17 = txtTemplate_Content17.Text;
            tde.Template_Content18 = txtTemplate_Content18.Text;
            tde.Template_Content19 = txtTemplate_Content19.Text;
            tde.Template_Content20 = txtTemplate_Content20.Text;

            tde.Detail_Template1 = txtDetail_Template1.Text;
            tde.Detail_Template2 = txtDetail_Template2.Text;
            tde.Detail_Template3 = txtDetail_Template3.Text;
            tde.Detail_Template4 = txtDetail_Template4.Text;

            tde.Detail_Template_Content1 = txtDetail_Template_Content1.Text;
            tde.Detail_Template_Content2 = txtDetail_Template_Content2.Text;
            tde.Detail_Template_Content3 = txtDetail_Template_Content3.Text;
            tde.Detail_Template_Content4 = txtDetail_Template_Content4.Text;

            tdbl.Update(item_code, tde);

        }

        protected Boolean Check_SpecialCharacter(String[] columnName, DataTable dt)
        {
            try
            {
                DataColumnCollection col = dt.Columns;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        for (int k = 0; k < columnName.Length; k++)
                        {
                            if (dt.Columns[j].ColumnName == columnName[k])
                            {

                                string input = dt.Rows[i][j].ToString();
                                string specialChar = @"㈰㈪㈫㈬㈭㈮㈯㉀㈷㉂㉃㈹㈺㈱㈾㈴㈲㈻㈶㈳㈵㈼㈽㈿㈸㊤㊥㊦㊧㊨㊩㊖㊝㊘㊞㊙㍾㍽㍼㍻㍉㌢㌔㌖㌅㌳㍎㌃㌶㌘㌕㌧㍑㍊㌹㍗㌍㍂㌣㌦㌻㌫㍍№℡㎜㎟㎝㎠㎤㎡㎥㎞㎢㎎㎏㏄㎖㎗㎘㎳㎲㎱㎰①②③④⑤⑥⑦⑧⑨⑩⑪⑫⑬⑭⑮⑯⑰⑱⑲⑳ⅠⅡⅢⅣⅤⅥⅦⅧⅨⅩⅰⅱⅲⅳⅴⅵⅶⅷⅸⅹ";
                                string comma = ",";
                                foreach (var item in specialChar)
                                {
                                    if (input.Contains(item))
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Template description contains special character.');", true);
                                        return true;
                                    }
                                }
                            }
                        }
                    }
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
    }
}