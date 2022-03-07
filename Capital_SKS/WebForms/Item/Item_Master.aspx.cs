/* 
*
Created By              : Aye Aye Mon
Created Date          :
Updated By             :
Updated Date         :

 Tables using           :  Item_Master
 *                                  - Category
 *                                  -Mall_Category
 *                                  -Item_Image
 *                                  -Template_Detail
 *                                  -Yahoo_SpecName
 * 
 * Storedprocedure using:  
 *                                           - 
 *                                           - 
 *                                           -
                                     
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
using ORS_RCM_Common;
using System.Data;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace ORS_RCM.WebForms.Item
{
    public partial class Item_Master_Edit1 : System.Web.UI.Page
    {
        //Global Variables
        Item_Master_Entity ime;
        Item_Master_BL imeBL; 
        Item_Category_BL itemCategoryBL;
        Category_BL cbl;
        Item_BL ibl;
        Item_BL item = new Item_BL();
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
                if (Request.QueryString["Item_Code"] != null)
                {
                    return Request.QueryString["Item_Code"].ToString();
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
                UserRoleBL user = new UserRoleBL();
                bool resultRead = user.CanRead(UserID, "099");
                if (resultRead)
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }
                if (String.IsNullOrWhiteSpace(txtSale_Price.Text))
                {
                    btnComplete.Enabled = false;
                }
                #region !IsPostBack
                if (!IsPostBack)
                {
                    BindSaleUnit();
                    BindContentUnit1();
                    BindContentUnit2();
                    BindMonotaroddl();
                    BindORSTag();//updated 3/6/2021
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
                    #endregion
                    BindShop(); //Bind Shop List'
                    if (ItemCode != null)    //Come from Item View for edit
                    {
                        //Change button name
                        btnSave.Text = "更新";
                        int ItemID = imeBL.SelectItemID(ItemCode);
                        ime = imeBL.SelectByID(ItemID);  //Select From Item_Master Table
                        SetItemData(ime);
                        SetSelectedShop(ItemID);             //Select From Item_Shop Table
                        SetSelectedCategory(ItemID);      //Select From Item_Category Table
                        SetCategoryData();
                        SetJishaCategoryData();
                        SelectByItemID(ItemID); 
                        //Select From Item_Image Table
                        //ddlsalesunit.Items.Insert(0, "");
                        //ddlcontentunit1.Items.Insert(0, "");
                        //ddlcontentunit2.Items.Insert(0, "");
                        BindPhotoList();
                        BindShopName();
                        SetItemCodeURL();
                        SetSelectedRelatedItem(ItemID);   //Select From Item_Related_Item Table

                        #region EDITED BY T.Z.A 15-03-2019

                        SKU_BIND();

                        //DataTable dtskucolor = item.SelectSKUColor(ItemCode);
                        //gvSKUColor.DataSource = item.SelectSKUColor(ItemCode); //Select From Item Table
                        //gvSKUColor.DataBind();
                        //DataTable dtsku = new DataTable();
                        //DataTable dt = new DataTable();
                        //if (dtskucolor.Rows.Count > 0)
                        //{
                        //    dtsku = item.SelectSKU(ItemCode);
                        //    gvSKU.DataSource = item.SelectSKU(ItemCode); //Select From Item Table
                        //    gvSKU.DataBind();
                        //    dt = item.SelectSKUSize(ItemCode); //Select From Item Table
                        //    gvSKUSize.DataSource = dt;
                        //    gvSKUSize.DataBind();
                        //    rdb1.Checked = true;
                        //}
                        //else
                        //{
                        //    rdb2.Checked = true;
                        //}
                        #endregion

                        BindDailyFlag(ItemCode); //for sks-587
                        SelectTemplateDetail(ItemCode);  //Select From Template_Detail Table
                        GetOptionSelectByItemID(ItemID);    //Select From Item_Option Table
                        SetYahooSpacificValue(ItemID);   //Select From Item_YahooSpecificValue Table
                        ChangeNUll_To_Space();
                    }
                    SetInitialRow();
                    if (ControlID.Contains("btnAddCatagories"))
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
                }
                #endregion
                #region IsPostBack
                else if (IsPostBack)
                {
                    if (!String.IsNullOrEmpty(CustomHiddenField.Value))
                    {
                        ControlID = CustomHiddenField.Value;
                    }
                    DataTable dt = RebindItemCodeURL(ControlID);
                    if (dlShop.Items.Count == 0 || dt.Rows.Count == 0)
                    {
                        BindShopName();
                    }
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
                    else if (ControlID.Contains("btnWowma_CategoryID"))
                    {
                        DisplayMallCategory(); // for display Mall_Category from popup form
                    }
                    else if (ControlID.Contains("btnTennis_CategoryID"))
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
                    else if (ControlID.Contains("btnAdd"))
                    {
                        SKU_BIND();
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

        public void SKU_BIND()
        {
            try
            {
                DataTable dtskucolor = item.SelectSKUColor(ItemCode);
                gvSKUColor.DataSource = item.SelectSKUColor(ItemCode); //Select From Item Table
                gvSKUColor.DataBind();
                DataTable dtsku = new DataTable();
                DataTable dt = new DataTable();
                if (dtskucolor.Rows.Count > 0)
                {
                    dtsku = item.SelectSKU(ItemCode);
                    gvSKU.DataSource = item.SelectSKU(ItemCode); //Select From Item Table
                    gvSKU.DataBind();
                    dt = item.SelectSKUSize(ItemCode); //Select From Item Table
                    gvSKUSize.DataSource = dt;
                    gvSKUSize.DataBind();
                    rdb1.Checked = true;
                }
                else
                {
                    rdb2.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ChangeNUll_To_Space()
        {
            if (txtCampaignImage1.Text.ToLower().Equals("null"))
                txtCampaignImage1.Text = String.Empty;
            if (txtCampaignImage2.Text.ToLower().Equals("null"))
                txtCampaignImage2.Text = String.Empty;
            if (txtCampaignImage3.Text.ToLower().Equals("null"))
                txtCampaignImage3.Text = String.Empty;
            if (txtCampaignImage4.Text.ToLower().Equals("null"))
                txtCampaignImage4.Text = String.Empty;
            if (txtCampaignImage5.Text.ToLower().Equals("null"))
                txtCampaignImage5.Text = String.Empty;
            if (txtRelated1.Text.ToLower().Equals("null"))
                txtRelated1.Text = String.Empty;
            if (txtRelated2.Text.ToLower().Equals("null"))
                txtRelated2.Text = String.Empty;
            if (txtRelated3.Text.ToLower().Equals("null"))
                txtRelated3.Text = String.Empty;
            if (txtRelated4.Text.ToLower().Equals("null"))
                txtRelated4.Text = String.Empty;
            if (txtRelated5.Text.ToLower().Equals("null"))
                txtRelated5.Text = String.Empty;
            if (txtDetail_Template1.Text.ToLower().Equals("null"))
                txtDetail_Template1.Text = String.Empty;
            if (txtDetail_Template2.Text.ToLower().Equals("null"))
                txtDetail_Template2.Text = String.Empty;
            if (txtDetail_Template3.Text.ToLower().Equals("null"))
                txtDetail_Template3.Text = String.Empty;
            if (txtDetail_Template4.Text.ToLower().Equals("null"))
                txtDetail_Template4.Text = String.Empty;
            if (txtDetail_Template_Content1.Text.ToLower().Equals("null"))
                txtDetail_Template_Content1.Text = String.Empty;
            if (txtDetail_Template_Content2.Text.ToLower().Equals("null"))
                txtDetail_Template_Content2.Text = String.Empty;
            if (txtDetail_Template_Content3.Text.ToLower().Equals("null"))
                txtDetail_Template_Content3.Text = String.Empty;
            if (txtDetail_Template_Content4.Text.ToLower().Equals("null"))
                txtDetail_Template_Content4.Text = String.Empty;
            if (txtTemplate1.Text.ToLower().Equals("null"))
                txtTemplate1.Text = String.Empty;
            if (txtTemplate2.Text.ToLower().Equals("null"))
                txtTemplate2.Text = String.Empty;
            if (txtTemplate3.Text.ToLower().Equals("null"))
                txtTemplate3.Text = String.Empty;
            if (txtTemplate4.Text.ToLower().Equals("null"))
                txtTemplate4.Text = String.Empty;
            if (txtTemplate5.Text.ToLower().Equals("null"))
                txtTemplate5.Text = String.Empty;
            if (txtTemplate6.Text.ToLower().Equals("null"))
                txtTemplate6.Text = String.Empty;
            if (txtTemplate7.Text.ToLower().Equals("null"))
                txtTemplate7.Text = String.Empty;
            if (txtTemplate8.Text.ToLower().Equals("null"))
                txtTemplate8.Text = String.Empty;
            if (txtTemplate9.Text.ToLower().Equals("null"))
                txtTemplate9.Text = String.Empty;
            if (txtTemplate10.Text.ToLower().Equals("null"))
                txtTemplate10.Text = String.Empty;
            if (txtTemplate11.Text.ToLower().Equals("null"))
                txtTemplate11.Text = String.Empty;
            if (txtTemplate12.Text.ToLower().Equals("null"))
                txtTemplate12.Text = String.Empty;
            if (txtTemplate13.Text.ToLower().Equals("null"))
                txtTemplate13.Text = String.Empty;
            if (txtTemplate14.Text.ToLower().Equals("null"))
                txtTemplate14.Text = String.Empty;
            if (txtTemplate15.Text.ToLower().Equals("null"))
                txtTemplate15.Text = String.Empty;
            if (txtTemplate16.Text.ToLower().Equals("null"))
                txtTemplate16.Text = String.Empty;
            if (txtTemplate17.Text.ToLower().Equals("null"))
                txtTemplate17.Text = String.Empty;
            if (txtTemplate18.Text.ToLower().Equals("null"))
                txtTemplate18.Text = String.Empty;
            if (txtTemplate19.Text.ToLower().Equals("null"))
                txtTemplate19.Text = String.Empty;
            if (txtTemplate20.Text.ToLower().Equals("null"))
                txtTemplate20.Text = String.Empty;
            if (txtTemplate_Content1.Text.ToLower().Equals("null"))
                txtTemplate_Content1.Text = String.Empty;
            if (txtTemplate_Content2.Text.ToLower().Equals("null"))
                txtTemplate_Content2.Text = String.Empty;
            if (txtTemplate_Content3.Text.ToLower().Equals("null"))
                txtTemplate_Content3.Text = String.Empty;
            if (txtTemplate_Content4.Text.ToLower().Equals("null"))
                txtTemplate_Content4.Text = String.Empty;
            if (txtTemplate_Content5.Text.ToLower().Equals("null"))
                txtTemplate_Content5.Text = String.Empty;
            if (txtTemplate_Content6.Text.ToLower().Equals("null"))
                txtTemplate_Content6.Text = String.Empty;
            if (txtTemplate_Content7.Text.ToLower().Equals("null"))
                txtTemplate_Content7.Text = String.Empty;
            if (txtTemplate_Content8.Text.ToLower().Equals("null"))
                txtTemplate_Content8.Text = String.Empty;
            if (txtTemplate_Content9.Text.ToLower().Equals("null"))
                txtTemplate_Content9.Text = String.Empty;
            if (txtTemplate_Content10.Text.ToLower().Equals("null"))
                txtTemplate_Content10.Text = String.Empty;
            if (txtTemplate_Content11.Text.ToLower().Equals("null"))
                txtTemplate_Content11.Text = String.Empty;
            if (txtTemplate_Content12.Text.ToLower().Equals("null"))
                txtTemplate_Content12.Text = String.Empty;
            if (txtTemplate_Content13.Text.ToLower().Equals("null"))
                txtTemplate_Content13.Text = String.Empty;
            if (txtTemplate_Content14.Text.ToLower().Equals("null"))
                txtTemplate_Content14.Text = String.Empty;
            if (txtTemplate_Content15.Text.ToLower().Equals("null"))
                txtTemplate_Content15.Text = String.Empty;
            if (txtTemplate_Content16.Text.ToLower().Equals("null"))
                txtTemplate_Content16.Text = String.Empty;
            if (txtTemplate_Content17.Text.ToLower().Equals("null"))
                txtTemplate_Content17.Text = String.Empty;
            if (txtTemplate_Content18.Text.ToLower().Equals("null"))
                txtTemplate_Content18.Text = String.Empty;
            if (txtTemplate_Content19.Text.ToLower().Equals("null"))
                txtTemplate_Content19.Text = String.Empty;
            if (txtTemplate_Content20.Text.ToLower().Equals("null"))
                txtTemplate_Content20.Text = String.Empty;
            if (txtZett_Item_Description.Text.ToLower().Equals("null"))
                txtZett_Item_Description.Text = String.Empty;
            if (txtZett_Sale_Description.Text.ToLower().Equals("null"))
                txtZett_Sale_Description.Text = String.Empty;
            if (txtLibraryImage1.Text.ToLower().Equals("null"))
                txtLibraryImage1.Text = String.Empty;
            if (txtLibraryImage2.Text.ToLower().Equals("null"))
                txtLibraryImage2.Text = String.Empty;
            if (txtLibraryImage3.Text.ToLower().Equals("null"))
                txtLibraryImage3.Text = String.Empty;
            if (txtLibraryImage4.Text.ToLower().Equals("null"))
                txtLibraryImage4.Text = String.Empty;
            if (txtLibraryImage5.Text.ToLower().Equals("null"))
                txtLibraryImage5.Text = String.Empty;
            if (txtLibraryImage6.Text.ToLower().Equals("null"))
                txtLibraryImage6.Text = String.Empty;
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

        public void BindSaleUnit()
        {
            imeBL = new Item_Master_BL();
            DataTable dt = imeBL.BindSaleUnit();
            ddlsalesunit.DataSource = dt;
            ddlsalesunit.DataTextField = "Sales_unit";
            ddlsalesunit.DataValueField = "Sales_unit";
            ddlsalesunit.DataBind();
            ddlsalesunit.Items.Insert(0,"");
        }

        public void BindORSTag()
        {
            imeBL = new Item_Master_BL();
            DataTable dt = imeBL.BindORSTag();
            ddlTagInfo.DataSource = dt;
            ddlTagInfo.DataTextField = "Name";
            ddlTagInfo.DataValueField = "Name";
            ddlTagInfo.DataBind();
            ddlTagInfo.Items.Insert(0, "");
        }

        public void BindContentUnit2()
        {
            imeBL = new Item_Master_BL();
            DataTable dt = imeBL.BindContentunit2();
            ddlcontentunit2.DataSource = dt;
            ddlcontentunit2.DataTextField = "Contents_unit_2";
            ddlcontentunit2.DataValueField = "Contents_unit_2";
            ddlcontentunit2.DataBind();
            ddlcontentunit2.Items.Insert(0,"");
        }

        public void BindContentUnit1()
        {
            imeBL = new Item_Master_BL();
            DataTable dt = imeBL.BindContentunit1();
            ddlcontentunit1.DataSource = dt;
            ddlcontentunit1.DataTextField = "Contents_unit_1";
            ddlcontentunit1.DataValueField = "Contents_unit_1";
            ddlcontentunit1.DataBind();
            ddlcontentunit1.Items.Insert(0,"");
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
                string icode = txtItem_Code.Text;
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
                                dr["Item_Code_URL"] = txtItem_Code.Text;
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
                        int ItemID = imeBL.SelectItemID(ItemCode);
                        ime.ID = ItemID;
                        ime.Updated_By = UserID;
                        ime = GetItemData();
                        string str = CheckLength(ime);
                        if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than length bytes!"); }
                        else
                        {
                            if (ValidationUpdate())
                            {
                                string option = null;
                                if (Request.QueryString["Item_Code"] != null)
                                {
                                    option = "Update";
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
                                    MessageBox("Updating Successful ! ");
                                    if (chkActive.Checked == true)
                                    {
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OnCheckedChanged(true);", true);
                                    }
                                    else
                                    {
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OnCheckedChanged(false);", true);
                                    }
                                    ime = new Item_Master_Entity();
                                    ime = imeBL.SelectByID(ItemID);
                                    if (!string.IsNullOrWhiteSpace(ime.Release_Date.ToString()))
                                    {
                                        txtRelease_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Release_Date);
                                    }
                                    else
                                    {
                                        txtRelease_Date.Text = "";
                                    }
                                    if (!string.IsNullOrWhiteSpace(ime.Post_Available_Date.ToString()))   //updated by nandar 05/01/2016
                                    {
                                        txtPost_Available_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Post_Available_Date);
                                    }
                                    else
                                    {
                                        txtPost_Available_Date.Text = "";
                                    }
                                }
                            }
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
                //TextBox txtitemcode = li.FindControl("txtItem_CodeList") as TextBox;
                Label shopid = li.FindControl("lblShopID") as Label;
                CheckBox cb = li.FindControl("ckbShop") as CheckBox;
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Item_ID"] = ItemID;                                               
                        dr["Item_Code_URL"] = txtItem_Code.Text;
                        dr["Shop_ID"] = Convert.ToInt32(shopid.Text);
                        dt.Rows.Add(dr);
                    }
                }
            }
            isbl.InsertItemCodeURL(dt, ItemID);
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
                 cat=dtnew.Rows[0]["Category"].ToString();
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
                byteLength = enc.GetByteCount(ime.PC_CatchCopy);
                if (byteLength > 255)
                {
                    msg += "PC用キャッチコピー" + ",";
                }
                byteLength = enc.GetByteCount(ime.PC_CatchCopy_Mobile);
                if (byteLength > 255)
                {
                    msg += "モバイル用キャッチコピー" + ",";
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

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList myData = new ArrayList();
                myData.Add(txtItem_Code.Text);
                myData.Add(txtItem_Name.Text);
                myData.Add(txtItem_Name.Text);
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

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            try
            {
                imeBL = new Item_Master_BL();
                user = new UserRoleBL();
                ime = new Item_Master_Entity();
                itemCategoryBL = new Item_Category_BL();
                DataTable templatedt = new DataTable();
                templatedt = CreateTemplateTable();
                String[] colName = { "Template" };
                if (!String.IsNullOrEmpty(txtSale_Price.Text))
                {
                    btnComplete.Enabled = true;
                }
                if (!Check_SpecialCharacter(colName, templatedt))
                {
                    Item_BL item = new Item_BL();
                    if (ItemCode != null)
                    {
                        int ItemID = imeBL.SelectItemID(ItemCode);
                        ime.ID = ItemID;
                        DataTable dtshop = CheckConditon(ItemID, ItemCode);
                        DataTable dtImage = ImageList as DataTable;
                        string errMsg = CheckCategoryID(dtshop, dtImage);// check sku and Mall category
                        if (!String.IsNullOrWhiteSpace(errMsg))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + errMsg + "')", true);
                        }
                        else
                        {
                            ime.Updated_By = UserID;
                            ime = GetItemData();
                            string str = CheckLength(ime);
                            if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than length bytes!"); }
                            else
                            {
                                if (ValidationComplete())
                                {
                                    string option = null;
                                    if (Request.QueryString["Item_Code"] != null)
                                    {
                                        option = "Edit";
                                    }
                                    if (imeBL.SaveEdit(ime, option) > 0)
                                    {
                                        ItemID = imeBL.SelectItemID(ItemCode);
                                        //1.Change Ctrl_ID=d from Item_Category table for Previous Category List
                                        //2.Insert Ctrl_ID=n from Item_Category table for New Category List
                                        //Insert Category List
                                        GetCategoryValueFromTextBox(ItemID, CategoryList);
                                        //Delete previous shop from Item_Shop table and then insert new shop or not
                                        InsertShopList(ItemID, ItemCode);
                                        //Delete previous photo from Item_Image table and then insert new photo or not
                                        InsertPhoto(ItemID);
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
                                        //for sks-587
                                        if (ViewState["DailyDelivery"] != null)
                                        {
                                            imeBL.SetUnsetDailyDelivery(ItemCode, Convert.ToInt32(ViewState["DailyDelivery"]));
                                        }
                                        SaveTemplateDetail(ItemCode); // Insert or Update Template_Detail
                                    }
                                }
                                MessageBox("Data Complete ! ");
                                if (chkActive.Checked == true)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OnCheckedChanged(true);", true);
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OnCheckedChanged(false);", true);
                                }
                                ime = new Item_Master_Entity();
                                ime = imeBL.SelectByID(ItemID);
                                if (!string.IsNullOrWhiteSpace(ime.Release_Date.ToString()))
                                {
                                    txtRelease_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Release_Date);
                                }
                                else
                                {
                                    txtRelease_Date.Text = "";
                                }
                                if (!string.IsNullOrWhiteSpace(ime.Post_Available_Date.ToString()))   //updated by nandar 05/01/2016
                                {
                                    txtPost_Available_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Post_Available_Date);
                                }
                                else
                                {
                                    txtPost_Available_Date.Text = "";
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "はい")
                {
                    imeBL = new Item_Master_BL();
                    imeBL.DeleteItem(ItemCode);
                    MessageBox("Delete Successful ! ");
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
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
                        page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script,false);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public bool ValidationUpdate()
        {
            try
            {
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
                //foreach (DataListItem li in dlShop.Items)
                //{
                //    TextBox txtitemcode = li.FindControl("txtItem_CodeList") as TextBox;
                //    if (String.IsNullOrEmpty(txtitemcode.Text))
                //    {
                //        MessageBox("Please fill Item_Code_URL textbox for all shop !!");
                //        return false;
                //    }
                //    foreach (char url in txtitemcode.Text)
                //    {
                //        if (url == '.' || char.IsUpper(url) || char.IsWhiteSpace(url))
                //        {
                //            MessageBox("Item_Code_URL is incorrect format.");
                //            return false;
                //        }
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

        public bool ValidationComplete()
        {
            try
            {
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
                return true;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

        #region Item_Master
        /// <summary>
        /// To get Item Master fields from input form
        /// </summary>
        /// <returns> Item Master Entity </returns>
        public Item_Master_Entity GetItemData()
        {
            try
            {
                ime.Ctrl_ID = hdfCtrl_ID.Value;
                if (Request.QueryString["Item_Code"] != null)
                {
                    ime.Item_Code = txtItem_Code.Text;
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
                ime.Maker_Code = txtmaker_code.Text;
                ime.Year = txtYear.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txtList_Price.Text))
                {
                    ime.List_Price = int.Parse(txtList_Price.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtJisha_Price .Text ))
                {
                    ime.Jisha_Price  = int.Parse(txtJisha_Price.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtSale_Price.Text))
                {
                    ime.Sale_Price = int.Parse(txtSale_Price.Text.Replace(",", string.Empty));
                }

                if (!string.IsNullOrWhiteSpace(txtRakutenPrice.Text))
                {
                    ime.RakutenPrice = int.Parse(txtRakutenPrice.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtYahooPrice.Text))
                {
                    ime.YahooPrice = int.Parse(txtYahooPrice.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtWowmaPrice.Text))
                {
                    ime.WowmaPrice = int.Parse(txtWowmaPrice.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtJishaPrice.Text))
                {
                    ime.JishaPrice = int.Parse(txtJishaPrice.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtTennisPrice.Text))
                {
                    ime.TennisPrice = int.Parse(txtTennisPrice.Text.Replace(",", string.Empty));
                }

                ime.Rakuten_CategoryID = txtRakuten_CategoryID.Text;
                ime.Yahoo_CategoryID = txtYahoo_CategoryID.Text;
                ime.Wowma_CategoryID = txtWowma_CategoryID.Text;
                //ime.Tennis_CategoryID = txtTennis_CategoryID.Text;

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
                //ime.TagInformation=ddlTagInfo.SelectedItem.Text;
                if (!string.IsNullOrWhiteSpace(ddlTagInfo.SelectedValue))
                {
                    ime.TagInformation = ddlTagInfo.SelectedItem.Text;
                }
                ime.ContentQuantityNo1 = txtcontentquantityunitno1.Text;
                ime.ContentQuantityNo2 = txtcontentquantityunitno2.Text;
                ime.ContentUnit1 = ddlcontentunit1.Text;
                ime.ContentUnit2 = ddlcontentunit2.Text;
                ime.PC_CatchCopy = txtCatchCopy.Text;
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
                if (!String.IsNullOrWhiteSpace(txtday_ship .Text))
                {
                    ime.Day_Ship = int.Parse(txtday_ship.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(txtreturn_necessary .Text ))
                {
                    ime.Retrun_Necessary  = int.Parse(txtreturn_necessary.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(txtwarehouse_code.Text))
                {
                    ime.Warehouse_Code = int.Parse(txtwarehouse_code.Text.TrimStart());
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

        public void SetItemData(Item_Master_Entity ime)
        {
            try
            {
                hdfCtrl_ID.Value = ime.Ctrl_ID;
                txtItem_Code.Text = ime.Item_Code;
                txtItem_Name.Text = ime.Item_Name;
                txtProduct_Code.Text = ime.Product_Code;
                if (!string.IsNullOrWhiteSpace(ime.Release_Date.ToString()))
                {
                    txtRelease_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Release_Date);
                }
                else
                {
                    txtRelease_Date.Text = "";
                }
                if (!string.IsNullOrWhiteSpace(ime.Post_Available_Date.ToString()))   //updated by nandar 05/01/2016
                {
                    txtPost_Available_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Post_Available_Date);
                }
                else
                {
                    txtPost_Available_Date.Text = "";
                }
                txtSeason.Text = ime.Season;
                txtBrand_Code.Text = ime.Brand_Code;
                txtBrand_Name.Text = ime.Brand_Name;
                txtCompetition_Name.Text = ime.Competition_Name;
                txtClass_Name.Text = ime.Class_Name;
                txtCatalog_Information.Text = ime.Catalog_Information;
                txtMerchandise_Information.Text = ime.Merchandise_Information;
                txtZett_Item_Description.Text = ime.Zett_Item_Description;
                txtZett_Sale_Description.Text = ime.Zett_Sale_Description;
                txtItem_Description_PC.Text = ime.Item_Description_PC;
                txtSale_Description_PC.Text = ime.Sale_Description_PC;
                txtSmart_Template.Text = ime.Smart_Template;
                txtAdditional_2.Text = ime.Additional_2;
                txtAdditional_3.Text = ime.Additional_3;
                txtList_Price.Text = string.Format("{0:#,#}", ime.List_Price);
                txtSale_Price.Text = string.Format("{0:#,#}", ime.Sale_Price);
                txtJisha_Price.Text = string.Format("{0:#,#}", ime.Jisha_Price);

                if(ime.RakutenPrice !=0 ||ime.YahooPrice !=0 ||ime.WowmaPrice !=0 || ime.JishaPrice !=0 || ime.TennisPrice != 0)
                {
                    priceDiv.Visible = true;

                    txtRakutenPrice.Text = string.Format("{0:#,#}", ime.RakutenPrice);
                    txtYahooPrice.Text = string.Format("{0:#,#}", ime.YahooPrice);
                    txtWowmaPrice.Text = string.Format("{0:#,#}", ime.WowmaPrice);
                    txtJishaPrice.Text = string.Format("{0:#,#}", ime.JishaPrice);
                    //txtTennisPrice.Text = string.Format("{0:#,#}", ime.TennisPrice); hhw
                }
                else
                {
                    priceDiv.Visible = false;
                }

                if (!String.IsNullOrWhiteSpace(txtSale_Price.Text))
                    btnComplete.Enabled = true;
                txtYear.Text = Convert.ToString(ime.Year);
                ddlShipping_Flag.SelectedValue = Convert.ToString(ime.Shipping_Flag);
                ddlDelivery_Charges.SelectedValue = Convert.ToString(ime.Delivery_Charges);
                ddlWarehouse_Specified.SelectedValue = Convert.ToString(ime.Warehouse_Specified);
                txtBlackMarket_Password.Text = ime.BlackMarket_Password;
                txtDoublePrice_Ctrl_No.Text = ime.DoublePrice_Ctrl_No;
                if (ime.Extra_Shipping != 0)
                    txtExtra_Shipping.Text = ime.Extra_Shipping.ToString();
                else txtExtra_Shipping.Text = "";
                txtmaker_code.Text = ime.Maker_Code;
                if (ime.Rakuten_CategoryID == "0")
                {
                    txtRakuten_CategoryID.Text = "";
                    txtRakuten_CategoryPath.Text = string.Empty;
                }
                else
                {
                    txtRakuten_CategoryID.Text = ime.Rakuten_CategoryID.ToString();
                    txtRakuten_CategoryPath.Text = ime.Rakuten_CategoryPath.ToString();
                }
                if (ime.Yahoo_CategoryID == "0")
                {
                    txtYahoo_CategoryID.Text = "";
                    txtYahoo_CategoryPath.Text = string.Empty;
                }
                else
                {
                    txtYahoo_CategoryID.Text = ime.Yahoo_CategoryID.ToString();
                    txtYahoo_CategoryPath.Text = ime.Yahoo_CategoryPath.ToString();
                }
                if (!String.IsNullOrWhiteSpace(ime.Wowma_CategoryID))
                {
                    if (ime.Wowma_CategoryID == "0")
                    {
                        txtWowma_CategoryID.Text = "";
                        txtWowma_CategoryPath.Text = string.Empty;
                    }
                    else
                    {
                        txtWowma_CategoryID.Text = ime.Wowma_CategoryID.ToString();
                        txtWowma_CategoryPath.Text = ime.Wowma_CategoryPath.ToString();
                    }
                }

                //hhw
                //if (!String.IsNullOrWhiteSpace(ime.Tennis_CategoryID))
                //{
                //    if (ime.Tennis_CategoryID == "0")
                //    {
                //        txtTennis_CategoryID.Text = "";
                //        txtTennis_CategoryPath.Text = string.Empty;
                //    }
                //    else
                //    {
                //        txtTennis_CategoryID.Text = ime.Tennis_CategoryID.ToString();
                //        txtTennis_CategoryPath.Text = ime.Tennis_CategoryPath.ToString();
                //    }
                //}
                txtyahoourl.Text = ime.Yahoo_url; //for sks-593
                if (ime.SalesUnit != "")
                {
                    ddlsalesunit.Text = Convert.ToString(ime.SalesUnit);
                }
                else 
                {
                    ddlsalesunit.Text = Convert.ToString(ime.SalesUnit);
                }
                if (ime.TagInformation != "") //update 3/6/2021
                {
                    ddlTagInfo.Text = Convert.ToString(ime.TagInformation);
                }
                else
                {
                    ddlTagInfo.Text = Convert.ToString(ime.TagInformation);
                }
                txtcontentquantityunitno1.Text = Convert.ToString(ime.ContentQuantityNo1);
                txtcontentquantityunitno2.Text = Convert.ToString(ime.ContentQuantityNo2);
                ddlcontentunit1.Text = Convert.ToString(ime.ContentUnit1);
                ddlcontentunit2.Text= Convert.ToString(ime.ContentUnit2);
                txtCatchCopy.Text = ime.PC_CatchCopy;
                txtCatchCopyMobile.Text = ime.PC_CatchCopy_Mobile;
                txtmakername.Text=ime.Maker_Name.ToString();
                txtcomment.Text=ime.Comment;

                if (ime.Selling_Price == 0)
                    txtsellingprice.Text = string.Empty;
                else
                    txtsellingprice.Text = ime.Selling_Price.ToString();

                if (ime.Cost == 0)
                    txtcost.Text = string.Empty;
                else
                    txtcost.Text = string.Format("{0:#,#}", ime.Cost); ;

                if (ime.Purchase_Price == 0)
                    txtpurchaseprice.Text = string.Empty;
                else 
                    txtpurchaseprice.Text = ime.Purchase_Price.ToString();

                if (ime.SellBy == 0)
                    txtsellby.Text = string.Empty;
                else
                    txtsellby.Text = ime.SellBy.ToString();

               
                txtsellingrank.Text=ime.Selling_Rank;
                ddldeliverymethod.SelectedValue =Convert.ToString(ime.Delivery_Method);
                ddldeliverytype.SelectedValue =Convert.ToString(ime.Delivery_Type);

                if (ime.Delivery_Days == 0)
                    txtdeliverydays.Text = string.Empty;
                else 
                    txtdeliverydays.Text = Convert.ToString(ime.Delivery_Days); 
               
                ddldeliveryfees.SelectedValue =Convert.ToString(ime.Delivery_Fees);
                ddlksmavaliable.SelectedValue =Convert.ToString(ime.KSMDelivery_Type);

                if (ime.KSMDelivery_Days == 0)
                    txtksmdeliverydays.Text = string.Empty;
                else
                    txtksmdeliverydays.Text = ime.KSMDelivery_Days.ToString();
               
                ddlreturnableitem.SelectedValue =Convert.ToString(ime.Returnable_Item); 
                ddlnoapplicablelaw.SelectedValue =Convert.ToString(ime.NoApplicable_Law);
                ddlsalespermission.SelectedValue =Convert.ToString(ime.Sales_Permission);
                ddllaw.SelectedValue =Convert.ToString(ime.Law);

                if (ime.Nation_Wide == "0")
                    txtnationwide.Text = string.Empty;
                else
                    txtnationwide.Text= Convert.ToString(ime.Nation_Wide);

                if (ime.Hokkaido == 0)
                    txthokkaido.Text = string.Empty;
                else
                    txthokkaido.Text= Convert.ToString(ime.Hokkaido);

                if (ime.Okinawa == 0)
                    txtokinawa.Text = string.Empty;
                else 
                    txtokinawa.Text = Convert.ToString(ime.Okinawa); 

                if (ime.Remote_Island == 0)
                    txtremoteisland.Text = string.Empty;
                else 
                    txtremoteisland.Text = Convert.ToString(ime.Remote_Island); 
               
                txtundeliveredarea.Text=Convert.ToString(ime.Undelivered_Area);
                txtdangerousgoodscontents.Text=ime.Undelivered_Area.ToString();
                ddldanggoodsclass.SelectedValue =Convert.ToString(ime.Dangoods_Class);
                ddldanggoodsname.SelectedValue = Convert.ToString(ime.Dangoods_Name);
                ddlriskrating.SelectedValue =Convert.ToString(ime.Risk_Rating);
                ddldanggoodsnature.SelectedValue = Convert.ToString(ime.Dangoods_Nature);
                ddlfirelaw.SelectedValue = Convert.ToString(ime.Fire_Law);
                if (!String.IsNullOrWhiteSpace(ime.CostRate))
                {
                    lblcostrate.Text = ime.CostRate + "%";
                }
                if (!String.IsNullOrWhiteSpace(ime.ProfitRate))
                {
                    lblprofitrate.Text = ime.ProfitRate + "%";
                }
                if (!String.IsNullOrWhiteSpace(ime.DiscountRate))
                {
                    lbldiscountrate.Text = ime.DiscountRate + "%";
                }
                if (ime.Day_Ship == 0)
                    txtday_ship .Text = string.Empty;
                else
                    txtday_ship.Text = Convert.ToString(ime.Day_Ship);
                if (ime.Retrun_Necessary  == 0)
                    txtreturn_necessary .Text  = string.Empty;
                else
                    txtreturn_necessary.Text = Convert.ToString(ime.Retrun_Necessary);
                if (ime.Warehouse_Code  == 0)
                    txtwarehouse_code.Text = string.Empty;
                else
                    txtwarehouse_code.Text = Convert.ToString(ime.Warehouse_Code);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        #endregion

        #region Category

        public void SortGridView()
        {
            try
            {
                DataTable dt = CategoryList;
                DataView dv = new DataView(dt);
                gvCatagories.DataSource = dv;
                gvCatagories.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void SetCategoryData()
        {
            try
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
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
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
                            txtWowma_CategoryID.Text = driectdt.Rows[0]["Wowma_CategoryID"].ToString();
                            txtWowma_CategoryPath.Text = driectdt.Rows[0]["Wowma_CategoryName"].ToString();

                            //hhw
                            //txtTennis_CategoryID.Text = driectdt.Rows[0]["Tennis_CategoryID"].ToString();
                            //txtTennis_CategoryPath.Text = driectdt.Rows[0]["Tennis_CategoryName"].ToString();
                        }
                    }
                }
                else
                {
                    txtRakuten_CategoryID.Text = string.Empty;
                    txtYahoo_CategoryID.Text = string.Empty;
                    txtWowma_CategoryID.Text = string.Empty;
                    //txtTennis_CategoryID.Text = string.Empty;
                    txtRakuten_CategoryPath.Text = "";
                    txtYahoo_CategoryPath.Text = "";
                    txtWowma_CategoryPath.Text = "";
                    //txtTennis_CategoryPath.Text = "";
                }
            }
            else
            {
                txtRakuten_CategoryID.Text = string.Empty;
                txtYahoo_CategoryID.Text = string.Empty;
                txtWowma_CategoryID.Text = string.Empty;
                //txtTennis_CategoryID.Text = string.Empty;
                txtRakuten_CategoryPath.Text = "";
                txtYahoo_CategoryPath.Text = "";
                txtWowma_CategoryPath.Text = "";
                //txtTennis_CategoryPath.Text = "";
            }
        }

        public string ShowHierarchy(int CID)
        {
            try
            {
                //Array.Clear(ids, 0, 100);
                index = 0;
                int i = 0;
                extract = 0; int cat = 0;
                ids[index++] = CID.ToString();
                GetCategory(CID);
                while (!String.IsNullOrWhiteSpace(ids[i]) && ids[i].ToString() != "1")
                {
                    DataTable dts = cbl.SelectForCatalogID(Convert.ToInt32(ids[i++]));
                    if (dts != null && dts.Rows.Count > 0)
                    {
                        ex[extract++] = dts.Rows[0]["Description"].ToString();
                        // lblparnode.Text += dts.Rows[0]["Description"].ToString() + ",";
                    }
                }
                for (int x = extract - 1; x >= 0; x--)
                {
                    if (x > 0)
                    {
                        treepath += ex[x] + "\\";
                    }
                    else if (x == 0)
                    {
                        treepath += ex[x];
                    }
                }

                return treepath;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
                return String.Empty;
            }
        }

        public void GetCategory(int id)
        {
            try
            {
                cbl = new Category_BL();
                DataTable dt = cbl.SelectForCatalogID(id);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ParentID"].ToString() != "0")
                    {
                        ids[index++] = dt.Rows[i]["ParentID"].ToString();
                        GetCategory(Convert.ToInt32(dt.Rows[i]["ParentID"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void SetSelectedCategory(int itemID)
        {
            try
            {
                itemCategoryBL = new Item_Category_BL();
                DataTable dt = itemCategoryBL.SelectByItemID(itemID);
                Session["CategoryList_" + ItemCode] = dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        #endregion

        #region Mall_Category
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
                    else if (dt.Rows[0]["Mall_ID"].ToString() == "4")
                    {
                        txtWowma_CategoryID.Text = dt.Rows[0]["Category_ID"].ToString();
                        txtWowma_CategoryPath.Text = dt.Rows[0]["Category_Path"].ToString();
                    }
                    //else if (dt.Rows[0]["Mall_ID"].ToString() == "7")
                    //{
                    //    txtTennis_CategoryID.Text = dt.Rows[0]["Category_ID"].ToString();
                    //    txtTennis_CategoryPath.Text = dt.Rows[0]["Category_Path"].ToString();
                    //}
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        #endregion

        #region Shop
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

                dr = dt.Select("SN='7'");
                if (dr.Length > 0)
                {
                    DataTable dtCopy = dt.Select("SN='7'").CopyToDataTable();
                    dlShop6.DataSource = dtCopy;
                    dlShop6.DataBind();
                    lblShopName6.Text = dtCopy.Rows[0]["Shop_Name"].ToString();
                }
                else
                { dd6.Visible = false; }
            }
            catch (Exception ex) 
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
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
                foreach (DataListItem li in dlShop6.Items)
                {
                    Label lbl = li.FindControl("lblMall6ShopID") as Label;
                    CheckBox cb = li.FindControl("ckbMall6Shop") as CheckBox;
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

        public void SetSelectedShop(int itemID)
        {
            try
            {
                Item_Shop_BL itemShopBL = new Item_Shop_BL();
                DataTable dt = itemShopBL.SelectByItemID(itemID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        foreach (DataListItem li in dlShop1.Items)
                        {
                            Label lbl = li.FindControl("lblMall1ShopID") as Label;
                            CheckBox cb = li.FindControl("ckbMall1Shop") as CheckBox;
                            if (lbl.Text == dt.Rows[i]["Shop_ID"].ToString())
                            {
                                cb.Checked = true;
                                break;
                            }
                        }
                        foreach (DataListItem li in dlShop2.Items)
                        {
                            Label lbl = li.FindControl("lblMall2ShopID") as Label;
                            CheckBox cb = li.FindControl("ckbMall2Shop") as CheckBox;
                            if (lbl.Text == dt.Rows[i]["Shop_ID"].ToString())
                            {
                                cb.Checked = true;
                                break;
                            }
                        }
                        foreach (DataListItem li in dlShop3.Items)
                        {
                            Label lbl = li.FindControl("lblMall3ShopID") as Label;
                            CheckBox cb = li.FindControl("ckbMall3Shop") as CheckBox;
                            if (lbl.Text == dt.Rows[i]["Shop_ID"].ToString())
                            {
                                cb.Checked = true;
                                break;
                            }
                        }
                        foreach (DataListItem li in dlShop4.Items)
                        {
                            Label lbl = li.FindControl("lblMall4ShopID") as Label;
                            CheckBox cb = li.FindControl("ckbMall4Shop") as CheckBox;
                            if (lbl.Text == dt.Rows[i]["Shop_ID"].ToString())
                            {
                                cb.Checked = true;
                                break;
                            }
                        }
                        foreach (DataListItem li in dlShop5.Items)
                        {
                            Label lbl = li.FindControl("lblMall5ShopID") as Label;
                            CheckBox cb = li.FindControl("ckbMall5Shop") as CheckBox;
                            if (lbl.Text == dt.Rows[i]["Shop_ID"].ToString())
                            {
                                cb.Checked = true;
                                break;
                            }
                        }
                        foreach (DataListItem li in dlShop6.Items)
                        {
                            Label lbl = li.FindControl("lblMall6ShopID") as Label;
                            CheckBox cb = li.FindControl("ckbMall6Shop") as CheckBox;
                            if (lbl.Text == dt.Rows[i]["Shop_ID"].ToString())
                            {
                                cb.Checked = true;
                                break;
                            }
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

        #endregion

        #region Photo
        public void BindPhotoList()
        {
            try
            {
                if (ImageList != null)
                {
                    DataTable dt = ImageList as DataTable;
                    /* 06/07/2015 by AM  SKS-26(BackLog)
                    foreach (DataRow dr2 in dt.Rows)
                    {
                        imeBL = new Item_Master_BL();
                        int ItemID = imeBL.SelectItemID(ItemCode);
                        if (String.IsNullOrEmpty(dr2["Item_ID"].ToString()))
                        {
                            dr2["Item_ID"] = ItemID;
                            dr2["ID"] = 0;
                        }
                    }
                    */
                    #region Item Image
                    DataRow[] dr = dt.Select("Image_Type='0'");
                    if (dr.Length > 0)
                    {
                        DataTable dtImage = dt.Select("Image_Type='0'").CopyToDataTable();

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
                                case "6":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image6.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage6.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image6.ImageUrl = "";
                                        hlImage6.NavigateUrl = "";
                                    }
                                    break;
                                case "7":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image7.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage7.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image7.ImageUrl = "";
                                        hlImage7.NavigateUrl = "";
                                    }
                                    break;
                                case "8":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image8.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage8.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image8.ImageUrl = "";
                                        hlImage8.NavigateUrl = "";
                                    }
                                    break;
                                case "9":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image9.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage9.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image9.ImageUrl = "";
                                        hlImage9.NavigateUrl = "";
                                    }
                                    break;
                                case "10":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image10.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage10.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image10.ImageUrl = "";
                                        hlImage10.NavigateUrl = "";
                                    }
                                    break;
                                case "11":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image11.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage11.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image11.ImageUrl = "";
                                        hlImage11.NavigateUrl = "";
                                    }
                                    break;
                                case "12":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image12.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage12.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image12.ImageUrl = "";
                                        hlImage12.NavigateUrl = "";
                                    }
                                    break;
                                case "13":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image13.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage13.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image13.ImageUrl = "";
                                        hlImage13.NavigateUrl = "";
                                    }
                                    break;
                                case "14":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image14.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage14.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image14.ImageUrl = "";
                                        hlImage14.NavigateUrl = "";
                                    }
                                    break;
                                case "15":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image15.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage15.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image15.ImageUrl = "";
                                        hlImage15.NavigateUrl = "";
                                    }
                                    break;
                                case "16":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image16.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage16.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image16.ImageUrl = "";
                                        hlImage16.NavigateUrl = "";
                                    }
                                    break;
                                case "17":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image17.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage17.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image17.ImageUrl = "";
                                        hlImage17.NavigateUrl = "";
                                    }
                                    break;
                                case "18":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image18.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage18.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image18.ImageUrl = "";
                                        hlImage18.NavigateUrl = "";
                                    }
                                    break;
                                case "19":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image19.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage19.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image19.ImageUrl = "";
                                        hlImage19.NavigateUrl = "";
                                    }
                                    break;
                                case "20":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image20.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage20.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                    }
                                    else
                                    {
                                        Image20.ImageUrl = "";
                                        hlImage20.NavigateUrl = "";
                                    }
                                    break;

                            }
                        }
                    }
                    else
                    {
                        Image1.ImageUrl = "";
                        Image2.ImageUrl = "";
                        Image3.ImageUrl = "";
                        Image4.ImageUrl = "";
                        Image5.ImageUrl = "";
                        Image6.ImageUrl = "";
                        Image7.ImageUrl = "";
                        Image8.ImageUrl = "";
                        Image9.ImageUrl = "";
                        Image10.ImageUrl = "";
                        Image11.ImageUrl = "";
                        Image12.ImageUrl = "";
                        Image13.ImageUrl = "";
                        Image14.ImageUrl = "";
                        Image15.ImageUrl = "";
                        Image16.ImageUrl = "";
                        Image17.ImageUrl = "";
                        Image18.ImageUrl = "";
                        Image19.ImageUrl = "";
                        Image20.ImageUrl = "";

                        hlImage1.NavigateUrl = "";
                        hlImage2.NavigateUrl = "";
                        hlImage3.NavigateUrl = "";
                        hlImage4.NavigateUrl = "";
                        hlImage5.NavigateUrl = "";
                        hlImage6.NavigateUrl = "";
                        hlImage7.NavigateUrl = "";
                        hlImage8.NavigateUrl = "";
                        hlImage9.NavigateUrl = "";
                        hlImage10.NavigateUrl = "";
                        hlImage11.NavigateUrl = "";
                        hlImage12.NavigateUrl = "";
                        hlImage13.NavigateUrl = "";
                        hlImage14.NavigateUrl = "";
                        hlImage15.NavigateUrl = "";
                        hlImage16.NavigateUrl = "";
                        hlImage17.NavigateUrl = "";
                        hlImage18.NavigateUrl = "";
                        hlImage19.NavigateUrl = "";
                        hlImage20.NavigateUrl = "";
                    }
                    #endregion

                    #region Library Image
                    dr = dt.Select("Image_Type='1'");
                    if (dr.Length > 0)
                    {
                        DataTable dtLibrary = dt.Select("Image_Type='1'").CopyToDataTable();
                        if (dtLibrary.Rows.Count > 0)
                        {
                            for (int m = 0; m < dtLibrary.Rows.Count; m++)
                            {
                                switch (dtLibrary.Rows[m]["SN"].ToString())
                                {
                                    case "1":
                                        txtLibraryImage1.Text = dtLibrary.Rows[m]["Image_Name"] + "";
                                        break;
                                    case "2":
                                        txtLibraryImage2.Text = dtLibrary.Rows[m]["Image_Name"] + "";
                                        break;
                                    case "3":
                                        txtLibraryImage3.Text = dtLibrary.Rows[m]["Image_Name"] + "";
                                        break;
                                    case "4":
                                        txtLibraryImage4.Text = dtLibrary.Rows[m]["Image_Name"] + "";
                                        break;
                                    case "5":
                                        txtLibraryImage5.Text = dtLibrary.Rows[m]["Image_Name"] + "";
                                        break;
                                    case "6":
                                        txtLibraryImage6.Text = dtLibrary.Rows[m]["Image_Name"] + "";
                                        break;
                                }
                            }
                        }
                    }
                    #endregion

                    #region Campagin Image
                    dr = dt.Select("Image_Type='2'");
                    if (dr.Length > 0)
                    {
                        DataTable dtCampagin = dt.Select("Image_Type='2'").CopyToDataTable();
                        if (dtCampagin.Rows.Count > 0)
                        {
                            for (int m = 0; m < dtCampagin.Rows.Count; m++)
                            {
                                switch (dtCampagin.Rows[m]["SN"].ToString())
                                {
                                    case "1":
                                        txtCampaignImage1.Text = dtCampagin.Rows[m]["Image_Name"] + "";
                                        break;
                                    case "2":
                                        txtCampaignImage2.Text = dtCampagin.Rows[m]["Image_Name"] + "";
                                        break;
                                    case "3":
                                        txtCampaignImage3.Text = dtCampagin.Rows[m]["Image_Name"] + "";
                                        break;
                                    case "4":
                                        txtCampaignImage4.Text = dtCampagin.Rows[m]["Image_Name"] + "";
                                        break;
                                    case "5":
                                        txtCampaignImage5.Text = dtCampagin.Rows[m]["Image_Name"] + "";
                                        break;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    Image1.ImageUrl = "";
                    Image2.ImageUrl = "";
                    Image3.ImageUrl = "";
                    Image4.ImageUrl = "";
                    Image5.ImageUrl = "";
                    Image6.ImageUrl = "";
                    Image7.ImageUrl = "";
                    Image8.ImageUrl = "";
                    Image9.ImageUrl = "";
                    Image10.ImageUrl = "";
                    Image11.ImageUrl = "";
                    Image12.ImageUrl = "";
                    Image13.ImageUrl = "";
                    Image14.ImageUrl = "";
                    Image15.ImageUrl = "";
                    Image16.ImageUrl = "";
                    Image17.ImageUrl = "";
                    Image18.ImageUrl = "";
                    Image19.ImageUrl = "";
                    Image20.ImageUrl = "";

                    hlImage1.NavigateUrl = "";
                    hlImage2.NavigateUrl = "";
                    hlImage3.NavigateUrl = "";
                    hlImage4.NavigateUrl = "";
                    hlImage5.NavigateUrl = "";
                    hlImage6.NavigateUrl = "";
                    hlImage7.NavigateUrl = "";
                    hlImage8.NavigateUrl = "";
                    hlImage9.NavigateUrl = "";
                    hlImage10.NavigateUrl = "";
                    hlImage11.NavigateUrl = "";
                    hlImage12.NavigateUrl = "";
                    hlImage13.NavigateUrl = "";
                    hlImage14.NavigateUrl = "";
                    hlImage15.NavigateUrl = "";
                    hlImage16.NavigateUrl = "";
                    hlImage17.NavigateUrl = "";
                    hlImage18.NavigateUrl = "";
                    hlImage19.NavigateUrl = "";
                    hlImage20.NavigateUrl = "";
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
                            Image6.ImageUrl = "";
                            Image7.ImageUrl = "";
                            Image8.ImageUrl = "";
                            Image9.ImageUrl = "";
                            Image10.ImageUrl = "";
                            Image11.ImageUrl = "";
                            Image12.ImageUrl = "";
                            Image13.ImageUrl = "";
                            Image14.ImageUrl = "";
                            Image15.ImageUrl = "";
                            Image16.ImageUrl = "";
                            Image17.ImageUrl = "";
                            Image18.ImageUrl = "";
                            Image19.ImageUrl = "";
                            Image20.ImageUrl = "";
                            hlImage6.NavigateUrl = "";
                            hlImage7.NavigateUrl = "";
                            hlImage8.NavigateUrl = "";
                            hlImage9.NavigateUrl = "";
                            hlImage10.NavigateUrl = "";
                            hlImage11.NavigateUrl = "";
                            hlImage12.NavigateUrl = "";
                            hlImage13.NavigateUrl = "";
                            hlImage14.NavigateUrl = "";
                            hlImage15.NavigateUrl = "";
                            hlImage16.NavigateUrl = "";
                            hlImage17.NavigateUrl = "";
                            hlImage18.NavigateUrl = "";
                            hlImage19.NavigateUrl = "";
                            hlImage20.NavigateUrl = "";
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
                                    case "6":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image6.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage6.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image6.ImageUrl = "";
                                            hlImage6.NavigateUrl = "";
                                        }
                                        break;
                                    case "7":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image7.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage7.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image7.ImageUrl = "";
                                            hlImage7.NavigateUrl = "";
                                        }
                                        break;
                                    case "8":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image8.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage8.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image8.ImageUrl = "";
                                            hlImage8.NavigateUrl = "";
                                        }
                                        break;
                                    case "9":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image9.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage9.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image9.ImageUrl = "";
                                            hlImage9.NavigateUrl = "";
                                        }
                                        break;
                                    case "10":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image10.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage10.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image10.ImageUrl = "";
                                            hlImage10.NavigateUrl = "";
                                        }
                                        break;
                                    case "11":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image11.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage11.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image11.ImageUrl = "";
                                            hlImage11.NavigateUrl = "";
                                        }
                                        break;
                                    case "12":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image12.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage12.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image12.ImageUrl = "";
                                            hlImage12.NavigateUrl = "";
                                        }
                                        break;
                                    case "13":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image13.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage13.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image13.ImageUrl = "";
                                            hlImage13.NavigateUrl = "";
                                        }
                                        break;
                                    case "14":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image14.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage14.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image14.ImageUrl = "";
                                            hlImage14.NavigateUrl = "";
                                        }
                                        break;
                                    case "15":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image15.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage15.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image15.ImageUrl = "";
                                            hlImage15.NavigateUrl = "";
                                        }
                                        break;
                                    case "16":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image16.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage16.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image16.ImageUrl = "";
                                            hlImage16.NavigateUrl = "";
                                        }
                                        break;
                                    case "17":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image17.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage17.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image17.ImageUrl = "";
                                            hlImage17.NavigateUrl = "";
                                        }
                                        break;
                                    case "18":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image18.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage18.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image18.ImageUrl = "";
                                            hlImage18.NavigateUrl = "";
                                        }
                                        break;
                                    case "19":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image19.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage19.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image19.ImageUrl = "";
                                            hlImage19.NavigateUrl = "";
                                        }
                                        break;
                                    case "20":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image20.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage20.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image20.ImageUrl = "";
                                            hlImage20.NavigateUrl = "";
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
                        Image6.ImageUrl = "";
                        Image7.ImageUrl = "";
                        Image8.ImageUrl = "";
                        Image9.ImageUrl = "";
                        Image10.ImageUrl = "";
                        Image11.ImageUrl = "";
                        Image12.ImageUrl = "";
                        Image13.ImageUrl = "";
                        Image14.ImageUrl = "";
                        Image15.ImageUrl = "";
                        Image16.ImageUrl = "";
                        Image17.ImageUrl = "";
                        Image18.ImageUrl = "";
                        Image19.ImageUrl = "";
                        Image20.ImageUrl = "";

                        hlImage1.NavigateUrl = "";
                        hlImage2.NavigateUrl = "";
                        hlImage3.NavigateUrl = "";
                        hlImage4.NavigateUrl = "";
                        hlImage5.NavigateUrl = "";
                        hlImage6.NavigateUrl = "";
                        hlImage7.NavigateUrl = "";
                        hlImage8.NavigateUrl = "";
                        hlImage9.NavigateUrl = "";
                        hlImage10.NavigateUrl = "";
                        hlImage11.NavigateUrl = "";
                        hlImage12.NavigateUrl = "";
                        hlImage13.NavigateUrl = "";
                        hlImage14.NavigateUrl = "";
                        hlImage15.NavigateUrl = "";
                        hlImage16.NavigateUrl = "";
                        hlImage17.NavigateUrl = "";
                        hlImage18.NavigateUrl = "";
                        hlImage19.NavigateUrl = "";
                        hlImage20.NavigateUrl = "";
                    }
                        #endregion

                    #region
                    /*
                    #region Item Image
                    DataRow[] dr = dt.Select("Image_Type='0'");
                    if (dr.Length > 0)
                    {
                        DataTable dtImage = dt.Select("Image_Type='0'").CopyToDataTable();
                        if (dtImage.Rows.Count > 0 && !string.IsNullOrWhiteSpace(dtImage.Rows[0]["Image_Name"].ToString()))
                        {
                            Image1.ImageUrl = imagePath + dtImage.Rows[0]["Image_Name"] + "";
                            hlImage1.NavigateUrl = imagePath + dtImage.Rows[0]["Image_Name"];
                        }
                        else
                        {
                            Image1.ImageUrl = "";
                            hlImage1.NavigateUrl = "";
                        }
                        if (dtImage.Rows.Count > 1 && !string.IsNullOrWhiteSpace(dtImage.Rows[1]["Image_Name"].ToString()))
                        {
                            Image2.ImageUrl = imagePath + dtImage.Rows[1]["Image_Name"] + "";
                            hlImage2.NavigateUrl = imagePath + dtImage.Rows[1]["Image_Name"];
                        }
                        else
                        {
                            Image2.ImageUrl = "";
                            hlImage2.NavigateUrl = "";
                        }
                        if (dtImage.Rows.Count > 2 && !string.IsNullOrWhiteSpace(dtImage.Rows[2]["Image_Name"].ToString()))
                        {
                            Image3.ImageUrl = imagePath + dtImage.Rows[2]["Image_Name"] + "";
                            hlImage3.NavigateUrl = imagePath + dtImage.Rows[2]["Image_Name"];
                        }
                        else
                        {
                            Image3.ImageUrl = "";
                            hlImage3.NavigateUrl = "";
                        }
                        if (dtImage.Rows.Count > 3 && !string.IsNullOrWhiteSpace(dtImage.Rows[3]["Image_Name"].ToString()))
                        {
                            Image4.ImageUrl = imagePath + dtImage.Rows[3]["Image_Name"] + "";
                            hlImage4.NavigateUrl = imagePath + dtImage.Rows[3]["Image_Name"];
                        }
                        else
                        {
                            Image4.ImageUrl = "";
                            hlImage4.NavigateUrl = "";
                        }
                        if (dtImage.Rows.Count > 4 && !string.IsNullOrWhiteSpace(dtImage.Rows[4]["Image_Name"].ToString()))
                        {
                            Image5.ImageUrl = imagePath + dtImage.Rows[4]["Image_Name"] + "";
                            hlImage5.NavigateUrl = imagePath + dtImage.Rows[4]["Image_Name"];
                        }
                        else
                        {
                            Image5.ImageUrl = "";
                            hlImage5.NavigateUrl = "";
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
                }
                    #endregion
                     */
                    //else
                    //{
                    //    Image1.ImageUrl = "";
                    //    Image2.ImageUrl = "";
                    //    Image3.ImageUrl = "";
                    //    Image4.ImageUrl = "";
                    //    Image5.ImageUrl = "";
                    //    hlImage1.NavigateUrl = "";
                    //    hlImage2.NavigateUrl = "";
                    //    hlImage3.NavigateUrl = "";
                    //    hlImage4.NavigateUrl = "";
                    //    hlImage5.NavigateUrl = "";
                    //}
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
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

        //for new case if Item_Image exist or not
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
                    if (!String.IsNullOrWhiteSpace(txtLibraryImage1.Text))
                    {
                        DataRow dr1 = dt.NewRow();
                        dr1["Image_Name"] = txtLibraryImage1.Text.TrimStart();
                        dr1["Image_Type"] = 1;
                        dr1["SN"] = 1;
                        dt.Rows.Add(dr1);
                    }

                    if (!String.IsNullOrWhiteSpace(txtLibraryImage2.Text))
                    {
                        DataRow dr2 = dt.NewRow();
                        dr2["Image_Name"] = txtLibraryImage2.Text.TrimStart();
                        dr2["Image_Type"] = 1;
                        dr2["SN"] = 2;
                        dt.Rows.Add(dr2);
                    }
                    if (!String.IsNullOrWhiteSpace(txtLibraryImage3.Text))
                    {
                        DataRow dr3 = dt.NewRow();
                        dr3["Image_Name"] = txtLibraryImage3.Text.TrimStart();
                        dr3["Image_Type"] = 1;
                        dr3["SN"] = 3;
                        dt.Rows.Add(dr3);
                    }
                    if (!String.IsNullOrWhiteSpace(txtLibraryImage4.Text))
                    {
                        DataRow dr4 = dt.NewRow();
                        dr4["Image_Name"] = txtLibraryImage4.Text.TrimStart();
                        dr4["Image_Type"] = 1;
                        dr4["SN"] = 4;
                        dt.Rows.Add(dr4);
                    }
                    if (!String.IsNullOrWhiteSpace(txtLibraryImage5.Text))
                    {
                        DataRow dr5 = dt.NewRow();
                        dr5["Image_Name"] = txtLibraryImage5.Text.TrimStart();
                        dr5["Image_Type"] = 1;
                        dr5["SN"] = 5;
                        dt.Rows.Add(dr5);
                    }
                    if (!String.IsNullOrWhiteSpace(txtLibraryImage6.Text))
                    {
                        DataRow dr6 = dt.NewRow();
                        dr6["Image_Name"] = txtLibraryImage6.Text.TrimStart();
                        dr6["Image_Type"] = 1;
                        dr6["SN"] = 6;
                        dt.Rows.Add(dr6);
                    }
                    #endregion

                    #region CampaginImage
                    if (!String.IsNullOrWhiteSpace(txtCampaignImage1.Text))
                    {
                        DataRow dr7 = dt.NewRow();
                        dr7["Image_Name"] = txtCampaignImage1.Text.TrimStart();
                        dr7["Image_Type"] = 2;
                        dr7["SN"] = 1;
                        dt.Rows.Add(dr7);
                    }
                    if (!String.IsNullOrWhiteSpace(txtCampaignImage2.Text))
                    {
                        DataRow dr8 = dt.NewRow();
                        dr8["Image_Name"] = txtCampaignImage2.Text.TrimStart();
                        dr8["Image_Type"] = 2;
                        dr8["SN"] = 2;
                        dt.Rows.Add(dr8);
                    }
                    if (!String.IsNullOrWhiteSpace(txtCampaignImage3.Text))
                    {
                        DataRow dr9 = dt.NewRow();
                        dr9["Image_Name"] = txtCampaignImage3.Text.TrimStart();
                        dr9["Image_Type"] = 2;
                        dr9["SN"] = 3;
                        dt.Rows.Add(dr9);
                    }
                    if (!String.IsNullOrWhiteSpace(txtCampaignImage4.Text))
                    {
                        DataRow dr10 = dt.NewRow();
                        dr10["Image_Name"] = txtCampaignImage4.Text.TrimStart();
                        dr10["Image_Type"] = 2;
                        dr10["SN"] = 4;
                        dt.Rows.Add(dr10);
                    }
                    if (!String.IsNullOrWhiteSpace(txtCampaignImage5.Text))
                    {
                        DataRow dr11 = dt.NewRow();
                        dr11["Image_Name"] = txtCampaignImage5.Text.TrimStart();
                        dr11["Image_Type"] = 2;
                        dr11["SN"] = 5;
                        dt.Rows.Add(dr11);
                    }
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
                    if (!String.IsNullOrWhiteSpace(txtLibraryImage1.Text))
                    {
                        DataRow dr1 = dt.NewRow();
                        dr1["Image_Name"] = txtLibraryImage1.Text.TrimStart();
                        dr1["Image_Type"] = 1;
                        dr1["SN"] = 1;
                        dt.Rows.Add(dr1);
                    }

                    if (!String.IsNullOrWhiteSpace(txtLibraryImage2.Text))
                    {
                        DataRow dr2 = dt.NewRow();
                        dr2["Image_Name"] = txtLibraryImage2.Text.TrimStart();
                        dr2["Image_Type"] = 1;
                        dr2["SN"] = 2;
                        dt.Rows.Add(dr2);
                    }
                    if (!String.IsNullOrWhiteSpace(txtLibraryImage3.Text))
                    {
                        DataRow dr3 = dt.NewRow();
                        dr3["Image_Name"] = txtLibraryImage3.Text.TrimStart();
                        dr3["Image_Type"] = 1;
                        dr3["SN"] = 3;
                        dt.Rows.Add(dr3);
                    }
                    if (!String.IsNullOrWhiteSpace(txtLibraryImage4.Text))
                    {
                        DataRow dr4 = dt.NewRow();
                        dr4["Image_Name"] = txtLibraryImage4.Text.TrimStart();
                        dr4["Image_Type"] = 1;
                        dr4["SN"] = 4;
                        dt.Rows.Add(dr4);
                    }
                    if (!String.IsNullOrWhiteSpace(txtLibraryImage5.Text))
                    {
                        DataRow dr5 = dt.NewRow();
                        dr5["Image_Name"] = txtLibraryImage5.Text.TrimStart();
                        dr5["Image_Type"] = 1;
                        dr5["SN"] = 5;
                        dt.Rows.Add(dr5);
                    }
                    if (!String.IsNullOrWhiteSpace(txtLibraryImage6.Text))
                    {
                        DataRow dr6 = dt.NewRow();
                        dr6["Image_Name"] = txtLibraryImage6.Text.TrimStart();
                        dr6["Image_Type"] = 1;
                        dr6["SN"] = 6;
                        dt.Rows.Add(dr6);
                    }
                    #endregion

                    #region CampaginImage
                    if (!String.IsNullOrWhiteSpace(txtCampaignImage1.Text))
                    {
                        DataRow dr7 = dt.NewRow();
                        dr7["Image_Name"] = txtCampaignImage1.Text.TrimStart();
                        dr7["Image_Type"] = 2;
                        dr7["SN"] = 1;
                        dt.Rows.Add(dr7);
                    }
                    if (!String.IsNullOrWhiteSpace(txtCampaignImage2.Text))
                    {
                        DataRow dr8 = dt.NewRow();
                        dr8["Image_Name"] = txtCampaignImage2.Text.TrimStart();
                        dr8["Image_Type"] = 2;
                        dr8["SN"] = 2;
                        dt.Rows.Add(dr8);
                    }
                    if (!String.IsNullOrWhiteSpace(txtCampaignImage3.Text))
                    {
                        DataRow dr9 = dt.NewRow();
                        dr9["Image_Name"] = txtCampaignImage3.Text.TrimStart();
                        dr9["Image_Type"] = 2;
                        dr9["SN"] = 3;
                        dt.Rows.Add(dr9);
                    }
                    if (!String.IsNullOrWhiteSpace(txtCampaignImage4.Text))
                    {
                        DataRow dr10 = dt.NewRow();
                        dr10["Image_Name"] = txtCampaignImage4.Text.TrimStart();
                        dr10["Image_Type"] = 2;
                        dr10["SN"] = 4;
                        dt.Rows.Add(dr10);
                    }
                    if (!String.IsNullOrWhiteSpace(txtCampaignImage5.Text))
                    {
                        DataRow dr11 = dt.NewRow();
                        dr11["Image_Name"] = txtCampaignImage5.Text.TrimStart();
                        dr11["Image_Type"] = 2;
                        dr11["SN"] = 5;
                        dt.Rows.Add(dr11);
                    }
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

        /// <summary>
        /// To display image when
        /// </summary>
        /// <param name="itemID">By the selected item master id</param>
        public void SelectByItemID(int itemID)
        {
            try
            {
                Item_Image_BL itemImageBL = new Item_Image_BL();
                DataTable dtImage = itemImageBL.SelectByItemID(itemID);
                //added by aam (20/10/2015)
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
                Session["ImageList_" + ItemCode] = dtImage;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
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
        #endregion

        #region Option
        public void ShowOption()
        {
            //divOption.Visible = false;
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
        /// <summary>
        /// To insert or update option and connect to Item_Option table
        /// </summary>
        /// <param name="itemID">To keep item master id</param>
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
        /// <summary>
        /// To display selected options
        /// </summary>
        /// <param name="itemID">By selected item master id</param>
        public void GetOptionSelectByItemID(int itemID)
        {
            try
            {
                Item_Option_BL ItemOptionBL = new Item_Option_BL();
                DataTable dttmp = ItemOptionBL.SelectByItemID(itemID);
                if (dttmp != null && dttmp.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name1", typeof(string));
                    dt.Columns.Add("Value1", typeof(string));
                    dt.Columns.Add("Name2", typeof(string));
                    dt.Columns.Add("Value2", typeof(string));
                    dt.Columns.Add("Name3", typeof(string));
                    dt.Columns.Add("Value3", typeof(string));
                    if (dttmp.Rows.Count > 2)
                    {
                        dt.Rows.Add(dttmp.Rows[0]["Option_Name"].ToString(), dttmp.Rows[0]["Option_Value"].ToString(),
                                              dttmp.Rows[1]["Option_Name"].ToString(), dttmp.Rows[1]["Option_Value"].ToString(),
                                              dttmp.Rows[2]["Option_Name"].ToString(), dttmp.Rows[2]["Option_Value"].ToString());
                    }
                    else if (dttmp.Rows.Count > 1)
                    {
                        dt.Rows.Add(dttmp.Rows[0]["Option_Name"].ToString(), dttmp.Rows[0]["Option_Value"].ToString(), dttmp.Rows[1]["Option_Name"].ToString(), dttmp.Rows[1]["Option_Value"].ToString(), "", "");
                    }
                    else if (dttmp.Rows.Count > 0)
                    {
                        dt.Rows.Add(dttmp.Rows[0]["Option_Name"].ToString(), dttmp.Rows[0]["Option_Value"].ToString(), "", "", "", "");
                    }

                    SetOption(dt);
                    Session["Option_" + ItemCode] = dt;
                }
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

        #endregion

        #region Related_Item
        /// <summary>
        /// Connects to Related Item 
        /// </summary>
        /// <param name="ItemID"> By selected master id</param>
        public void SetSelectedRelatedItem(int ItemID)
        {
            try
            {
                Item_Related_Item_BL ItemRelatedBL = new Item_Related_Item_BL();
                DataTable dt = ItemRelatedBL.SelectByItemID(ItemID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        switch (i)
                        {
                            case 0: txtRelated1.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 1: txtRelated2.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 2: txtRelated3.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 3: txtRelated4.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 4: txtRelated5.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
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

        #endregion

        #region YahooSpecificValue

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

        public void SetYahooSpacificValue(int ItemID)
        {
            try
            {
                Item_YahooSpecificValue_BL YahooSpecificValueBL = new Item_YahooSpecificValue_BL();
                Session["YahooSpecificValue_" + ItemCode] = YahooSpecificValueBL.SelectByItemID(ItemID);
                ShowValue();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
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
                    ShowValue();
                }
                else
                {
                    Session["YahooSpecificValue"] = null;
                    ShowValue();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        #endregion

        #region Template_Detail
        public void SelectTemplateDetail(string item_code)
        {
            try
            {
                Template_Detail_BL template = new Template_Detail_BL();
                DataTable dttemplate = template.SelectByItemCode(item_code);

                if (dttemplate != null && dttemplate.Rows.Count > 0)
                {
                    txtTemplate1.Text = dttemplate.Rows[0]["Template1"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate2.Text = dttemplate.Rows[0]["Template2"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate3.Text = dttemplate.Rows[0]["Template3"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate4.Text = dttemplate.Rows[0]["Template4"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate5.Text = dttemplate.Rows[0]["Template5"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate6.Text = dttemplate.Rows[0]["Template6"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate7.Text = dttemplate.Rows[0]["Template7"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate8.Text = dttemplate.Rows[0]["Template8"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate9.Text = dttemplate.Rows[0]["Template9"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate10.Text = dttemplate.Rows[0]["Template10"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate11.Text = dttemplate.Rows[0]["Template11"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate12.Text = dttemplate.Rows[0]["Template12"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate13.Text = dttemplate.Rows[0]["Template13"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate14.Text = dttemplate.Rows[0]["Template14"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate15.Text = dttemplate.Rows[0]["Template15"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate16.Text = dttemplate.Rows[0]["Template16"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate17.Text = dttemplate.Rows[0]["Template17"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate18.Text = dttemplate.Rows[0]["Template18"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate18.Text = dttemplate.Rows[0]["Template18"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate19.Text = dttemplate.Rows[0]["Template19"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate20.Text = dttemplate.Rows[0]["Template20"].ToString().Replace("$CapitalSports$", "【・】");

                    txtTemplate_Content1.Text = dttemplate.Rows[0]["Template_Content1"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content2.Text = dttemplate.Rows[0]["Template_Content2"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content3.Text = dttemplate.Rows[0]["Template_Content3"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content4.Text = dttemplate.Rows[0]["Template_Content4"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content5.Text = dttemplate.Rows[0]["Template_Content5"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content6.Text = dttemplate.Rows[0]["Template_Content6"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content7.Text = dttemplate.Rows[0]["Template_Content7"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content8.Text = dttemplate.Rows[0]["Template_Content8"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content9.Text = dttemplate.Rows[0]["Template_Content9"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content10.Text = dttemplate.Rows[0]["Template_Content10"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content11.Text = dttemplate.Rows[0]["Template_Content11"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content12.Text = dttemplate.Rows[0]["Template_Content12"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content13.Text = dttemplate.Rows[0]["Template_Content13"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content14.Text = dttemplate.Rows[0]["Template_Content14"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content15.Text = dttemplate.Rows[0]["Template_Content15"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content16.Text = dttemplate.Rows[0]["Template_Content16"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content17.Text = dttemplate.Rows[0]["Template_Content17"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content18.Text = dttemplate.Rows[0]["Template_Content18"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content19.Text = dttemplate.Rows[0]["Template_Content19"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content20.Text = dttemplate.Rows[0]["Template_Content20"].ToString().Replace("$CapitalSports$", "【・】");

                    txtDetail_Template1.Text = dttemplate.Rows[0]["Detail_Template1"].ToString().Replace("$CapitalSports$", "【・】");
                    txtDetail_Template2.Text = dttemplate.Rows[0]["Detail_Template2"].ToString().Replace("$CapitalSports$", "【・】");
                    txtDetail_Template3.Text = dttemplate.Rows[0]["Detail_Template3"].ToString().Replace("$CapitalSports$", "【・】");
                    txtDetail_Template4.Text = dttemplate.Rows[0]["Detail_Template4"].ToString().Replace("$CapitalSports$", "【・】");

                    txtDetail_Template_Content1.Text = dttemplate.Rows[0]["Detail_Template_Content1"].ToString().Replace("$CapitalSports$", "【・】");
                    txtDetail_Template_Content2.Text = dttemplate.Rows[0]["Detail_Template_Content2"].ToString().Replace("$CapitalSports$", "【・】");
                    txtDetail_Template_Content3.Text = dttemplate.Rows[0]["Detail_Template_Content3"].ToString().Replace("$CapitalSports$", "【・】");
                    txtDetail_Template_Content4.Text = dttemplate.Rows[0]["Detail_Template_Content4"].ToString().Replace("$CapitalSports$", "【・】");

                }
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

        #endregion

        protected void CheckedChanged_Amazon(object sender, EventArgs e)

        {
            CheckBox chk = (CheckBox)sender;
            DataListItem datalist = (DataListItem)chk.NamingContainer;
            //CheckBox cb = datalist.FindControl("ckbMall4Shop") as CheckBox;
            string shopList = null;
            foreach (DataListItem li in dlShop1.Items)
            {
                Label lbl = li.FindControl("lblMall1ShopID") as Label;
                CheckBox cb = li.FindControl("ckbMall1Shop") as CheckBox;

                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        shopList += Convert.ToInt32(lbl.Text);
                    }
                }

            }
            if (datalist.ItemIndex == 3)
            {
                  if(shopList==null)
                {
                    flag = 0;
                }
                  else  if (shopList.Contains("4"))
                {
                    flag = 1;
                }
              
                else
                {
                    flag = 0;
                }
            }

            ViewState["flag"] = flag;

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

        public void BindDailyFlag(string ItemCode)
        {
            try
            {
                DataTable dt = imeBL.BindDailyFlag(ItemCode);
                if (dt != null && dt.Rows.Count>0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["Flag"].ToString()) == true)
                        delivery_flag.Checked = true;
                    else
                        delivery_flag.Checked = false;
                    if (Convert.ToBoolean(dt.Rows[0]["Active_Status"].ToString()) == true)
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;
                    if (Convert.ToBoolean(dt.Rows[0]["Inventory_Flag"].ToString()) == true)
                        chkInventory.Checked = true;
                    else
                        chkInventory.Checked = false;
                    txtInactive.Text = dt.Rows[0]["Comment"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CheckConditon(int itemID, string itemcode)
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
                foreach (DataListItem li in dlShop6.Items)
                {
                    Label lbl = li.FindControl("lblMall6ShopID") as Label;
                    CheckBox cb = li.FindControl("ckbMall6Shop") as CheckBox;
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
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String CheckCategoryID(DataTable dtshop, DataTable dtImage)
        {
            String errorMsg = string.Empty;
            DataRow[] rowRakuten = dtshop.Select("ShopID=1 OR ShopID=5 OR ShopID=8 OR ShopID=12 ");
            if (rowRakuten.Count() > 0 && txtRakuten_CategoryID.Text == "")
            {
                errorMsg += "楽天ディレクトリIDを設定してください。, ";
            }
            DataRow[] rowYahoo = dtshop.Select("ShopID=2 OR ShopID=6 OR ShopID=9 OR ShopID=13  OR ShopID=17");
            if (rowYahoo.Count() > 0 && txtYahoo_CategoryID.Text == "")
            {
                errorMsg += "YahooスペックIDを設定してください。, ";
            }          
            DataRow[] rowWomma = dtshop.Select("ShopID = 4");
            if (rowWomma.Count() > 0 && txtWowma_CategoryID.Text == "")
            {
                errorMsg += "WommaカテゴリIDを設定してください。, ";
            }
            DataRow[] rowTennis = dtshop.Select("ShopID = 6");
            //if (rowTennis.Count() > 0 && txtTennis_CategoryID.Text == "")
            //{
            //    errorMsg += "ORS自社カテゴリIDを設定してください。, ";
            //}
            if (dtImage != null && dtImage.Rows.Count > 0)
            {
                DataRow[] rowImage = dtImage.Select("Image_Type='0'");
                if (rowImage.Count() == 0)
                {
                    errorMsg += "Image is Empty  ";
                    btnComplete.Visible = true;
                }
            }
            else
            {
                errorMsg += "Image is Empty  ";
                btnComplete.Visible = true;
            }
            return errorMsg;
        }

        protected void btnToCancelExhibit_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            imeBL = new Item_Master_BL();
            itemCategoryBL = new Item_Category_BL();
            int ItemID = imeBL.SelectItemID(ItemCode);
            DataTable dtshop = CheckConditon(ItemID, ItemCode);
            DataTable dt = itemCategoryBL.SelectByItemID(ItemID);
            DataTable dtImage = ImageList as DataTable;
            DataRow[] rowRakuten = dtshop.Select("ShopID=1 OR ShopID=5 OR ShopID=8 OR ShopID=12 ");
            DataRow[] rowYahoo = dtshop.Select("ShopID=2 OR ShopID=6 OR ShopID=9 OR ShopID=13  OR ShopID=17");
            DataRow[] rowWowma = dtshop.Select("ShopID=4 ");
            DataRow[] rowTennis = dtshop.Select("ShopID=6 ");
            DataRow[] rowImage = dtImage.Select("Image_Type='0'");
            if (confirmValue == "はい")
            {
                if (dt.Rows.Count <= 0 || (rowRakuten.Count() > 0 && txtRakuten_CategoryID.Text == "") || (rowYahoo.Count() > 0 && txtYahoo_CategoryID.Text == "") ||
                    (rowWowma.Count() > 0 && txtWowma_CategoryID.Text == "")||/* (rowTennis.Count() > 0 && txtTennis_CategoryID.Text == "") ||*/ (rowImage.Count() == 0))
                {
                    imeBL.ChangeExportStatusToPink(ItemCode, 0);
                }
            }
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Category", typeof(string)));
            dt.Columns.Add(new DataColumn("SN", typeof(string)));
            dr = dt.NewRow();
            dr["Category"] = string.Empty;
            dr["SN"] = "0";
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            gvCategory.DataSource = dt;
            gvCategory.DataBind();
        }

        public void AddNewRowToGrid()
        {
            string catvalue=null;
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
                        if (!Regex.IsMatch(box1.Text,@"^[a-zA-Z'.]{1,40}$"))
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
                        if (String.IsNullOrWhiteSpace(dtCurrentTable.Rows[i-1]["Category"].ToString()))
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
                            box2.Text=dt.Rows[i]["SN"].ToString();
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

        private void SetPreviousDB()
        {
            int rowindex = 0;
            if (ViewState["PreviousTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["PreviousTable"];
                if (dt.Rows.Count > 0)
                {
                    Response.Write(gvCategory.Rows.Count);
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

        protected void btnCategoryAdd_OnClick(object sender, EventArgs e)
        {
            AddNewRowToGrid();
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

        public void CreatenewDataTable()
        {
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtnew.Columns.Add(new DataColumn("Category", typeof(string)));
            dtnew.Columns.Add(new DataColumn("SN", typeof(string)));
            ViewState["DataTablenew"] = dtnew;
        }

        protected void chkInventory_OnCheckChanged(object sender, EventArgs e)
        {
            imeBL = new Item_Master_BL();
            if (chkInventory.Checked)
            {
                imeBL.ItemUpdateInventory(ItemCode,0);
            }
            else
            {
                imeBL.ItemUpdateInventory(ItemCode,1);
            }
        }

        public void SetItemCodeURL()
        {
            imeBL = new Item_Master_BL();
            Item_Shop_BL isbl = new Item_Shop_BL();
            //int ItemID = imeBL.SelectItemID(ItemCode);
            DataTable dt = isbl.SelectItemCodeURL(ItemCode);
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
        }

        public void BindShopName()
        {
            try
            {
                Shop_BL shopBL = new Shop_BL();
                Item_Shop_BL isbl = new Item_Shop_BL();
                DataTable dt = shopBL.SelectAll_URL();
                if (ItemCode != null)
                {
                    DataTable dtURL = isbl.SelectItemCodeURL(ItemCode);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Item_Code");
                        if (dtURL.Rows.Count <= 0 && !String.IsNullOrWhiteSpace(txtItem_Code.ToString()))
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            { dt.Rows[i]["Item_Code"] = txtItem_Code.Text; }
                            if (!String.IsNullOrEmpty(txtItem_Code.Text))
                            {
                                btnComplete.Enabled = true;
                            }

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

        public void RebindShopName()
        { 
            
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

        protected void txtItem_Code_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSale_Price.Text))
            {
                btnComplete.Enabled = true;
            }
        }

        protected void txtExtra_Shipping_TextChanged(object sender, EventArgs e)
        {
           
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
                                string plusign = "[[(+)]]";
                                string minussign = "[[(-)]]";
                               
                                foreach (var item in specialChar)
                                {
                                    if (input.Contains(item))
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Template description contains special character.');", true);
                                        return true;
                                    }
                                   
                                }
                                if (input.Contains(plusign) || input.Contains(minussign))
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Template description contains special character.');", true);
                                    return true;
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

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            if (ItemCode != null)
            {
                DataTable dtPrice = new DataTable();
                imeBL = new Item_Master_BL();
                priceDiv.Visible = true;
                dtPrice = imeBL.GetPrices(ItemCode);
                if (dtPrice.Rows.Count > 0)               
                {
                    txtRakutenPrice.Text = string.Format("{0:#,#}", dtPrice.Rows[0]["RakutenPrice"].ToString());
                    txtYahooPrice.Text = string.Format("{0:#,#}", dtPrice.Rows[0]["YahooPrice"].ToString());
                    txtWowmaPrice.Text = string.Format("{0:#,#}", dtPrice.Rows[0]["WowmaPrice"].ToString());
                    txtJishaPrice.Text = string.Format("{0:#,#}", dtPrice.Rows[0]["JishaPrice"].ToString());
                    txtTennisPrice.Text = string.Format("{0:#,#}", dtPrice.Rows[0]["TennisPrice"].ToString());
                    //if (dtPrice.Rows[0]["RakutenPrice"].ToString() == "0")
                    //    txtRakutenPrice.Text = string.Empty;
                    //else
                    //    txtRakutenPrice.Text= string.Format("{0:#,#}", dtPrice.Rows[0]["RakutenPrice"].ToString());
                    //if (dtPrice.Rows[0]["YahooPrice"].ToString() == "0")
                    //    txtYahooPrice.Text = string.Empty;
                    //else
                    //    txtYahooPrice.Text = string.Format("{0:#,#}", dtPrice.Rows[0]["YahooPrice"].ToString());
                    //if (dtPrice.Rows[0]["WowmaPrice"].ToString() == "0")
                    //    txtWowmaPrice.Text = string.Empty;
                    //else
                    //    txtWowmaPrice.Text = string.Format("{0:#,#}", dtPrice.Rows[0]["WowmaPrice"].ToString());
                    //if (dtPrice.Rows[0]["JishaPrice"].ToString() == "0")
                    //    txtJishaPrice.Text = string.Empty;
                    //else
                    //    txtJishaPrice.Text = string.Format("{0:#,#}", dtPrice.Rows[0]["JishaPrice"].ToString()); 
                }
            }
        }
    }
}
