/* 
Created By              :Aye Aye Mon
Created Date          : 29/06/2014
Updated By             :Kay Thi Aung
Updated Date         :31/07/2014

 Tables using: 
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

namespace ORS_RCM
{
    public partial class Item : System.Web.UI.Page
    {
        //Global Variables
        Item_Master_Entity ime;
        Item_Master_BL imeBL;
        Item_Category_BL itemCategoryBL;
        Category_BL cbl;
        public int index = 0;
        public int extract = 0;
        public String[] ex = new String[6];
        public String[] cx = new String[100];
        public String[] ids = new String[100];
        string treepath = string.Empty;
        string catpath = string.Empty;

        UserRoleBL user; 
        UserRole_Entity userInfo; 
        User_entity userentity; 
        User_BL UserBL;

        // Request Parameter
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
        // Session Parameter
        public DataTable CategoryList
        {
            get
            {
                if (Session["CategoryList"] != null)
                {
                    DataTable dt = (DataTable)Session["CategoryList"];
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
                if (Session["ImageList"] != null)
                {
                    DataTable dt = (DataTable)Session["ImageList"];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable RelatedItemList
        {
            get
            {
                if (Session["Related_Item_List"] != null)
                {
                    DataTable dt = (DataTable)Session["Related_Item_List"];
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
                if (Session["Mall_Category_ID"] != null)
                {
                    return (DataTable)Session["Mall_Category_ID"];
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
                if (Session["Option"] != null)
                {
                    DataTable dt = (DataTable)Session["Option"];
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
                if (Session["YahooSpecificValue"] != null)
                {
                    return (DataTable)Session["YahooSpecificValue"];
                }
                else
                {
                    return null;
                }
            }
        }
        string imagePath = ConfigurationManager.AppSettings["ItemImage"].ToString();

        // Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string ControlID = string.Empty;
                #region Popup Button Click
                Page.MaintainScrollPositionOnPostBack = true;
                lnkAddPhoto.Attributes.Add("onclick", "javascript:ShowDialog('0'," + lnkAddPhoto.ClientID + ")");
                btnAddOption.Attributes.Add("onclick", "javascript:ShowOption(" + btnAddOption.ClientID + ")");
                btnAddCatagories.Attributes.Add("onclick", "javascript:ShowCatagoryList(" + btnAddCatagories.ClientID + ")");
                btnRakuten_CategoryID.Attributes.Add("onclick", "javascript:ShowMallCategory('1'," + btnRakuten_CategoryID.ClientID + ")");
                btnYahoo_CategoryID.Attributes.Add("onclick", "javascript:ShowMallCategory('2'," + btnYahoo_CategoryID.ClientID + ")");
                btnPonpare_CategoryID.Attributes.Add("onclick", "javascript:ShowMallCategory('3'," + btnPonpare_CategoryID.ClientID + ")");
                imgbYahooSpecValue.Attributes.Add("onclick", "javascript:ShowYahooSpecValue(" + imgbYahooSpecValue.ClientID + ")");
                #endregion
                #region !IsPostBack
                if (!IsPostBack)
                {
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

                    BindShop(); //Bind Shop List

                    if (ItemCode != null)    //Come from Item View for edit
                    {
                        //Change button name
                        btnSave.Text = "更新";
                        //lblHeader.Text = "商品情報編集";

                        int ItemID = imeBL.SelectItemID(ItemCode);

                        ime = imeBL.SelectByID(ItemID);  //Select From Item_Master Table
                        SetItemData(ime);

                        SetSelectedShop(ItemID);             //Select From Item_Shop Table

                        SetSelectedCategory(ItemID);      //Select From Item_Category Table
                        SetCategoryData();

                        SelectByItemID(ItemID);                //Select From Item_Image Table
                        BindPhotoList();

                        SetSelectedRelatedItem(ItemID);   //Select From Item_Related_Item Table
                        //DisplayRelatedItem();

                        gvSKUColor.DataSource = item.SelectSKUColor(ItemCode); //Select From Item Table
                        gvSKUColor.DataBind();

                        gvSKU.DataSource = item.SelectSKU(ItemCode); //Select From Item Table
                        gvSKU.DataBind();

                        DataTable dt  = item.SelectSKUSize(ItemCode); //Select From Item Table
                        dt.Columns.Remove("Color_Code");
                        dt.Columns.Remove("Color_Name");
                        dt.AcceptChanges();
                        gvSKUSize.DataSource = dt;
                        gvSKUSize.DataBind();

                        GetOptionSelectByItemID(ItemID);    //Select From Item_Option Table

                        SetYahooSpacificValue(ItemID);   //Select From Item_YahooSpecificValue Table
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

                    if (ControlID.Contains("lnkAddPhoto"))
                    {
                        //BindPhotoList();//for display Photo choiced from popup form
                        ReBindPhotoList();
                    }
                    else if (ControlID.Contains("btnAddOption"))
                    {
                        ShowOption();
                        btnAddCatagories.Focus();
                    }
                    else if (ControlID.Contains("btnAddCatagories"))
                    {
                        //SetCategoryData(); //for display Catelog choiced from popup form
                        SetMallCategoryData();
                        DisplayYahooSpecificValue(txtYahoo_CategoryID.Text); // Auto Display for first specific
                        btnRakuten_CategoryID.Focus();
                    }
                    else if (ControlID.Contains("btnRakuten_CategoryID"))
                    {
                        DisplayMallCategory(); // for display Mall_Category from popup form
                        btnYahoo_CategoryID.Focus();
                    }
                    else if (ControlID.Contains("btnYahoo_CategoryID"))
                    {
                        DisplayMallCategory(); // for display Mall_Category from popup form
                        DisplayYahooSpecificValue(txtYahoo_CategoryID.Text); // Auto Display for first specific
                        btnPonpare_CategoryID.Focus();
                    }
                    else if (ControlID.Contains("btnPonpare_CategoryID"))
                    {
                        DisplayMallCategory(); // for display Mall_Category from popup form
                        imgbYahooSpecValue.Focus();
                    }
                    else if (ControlID.Contains("imgbYahooSpecValue"))
                    {
                        ShowValue();
                        txtRelated1.Focus();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                user = new UserRoleBL();
                ime = new Item_Master_Entity();
                imeBL = new Item_Master_BL();
                itemCategoryBL = new Item_Category_BL();
                if (ItemCode != null)
                {
                    int ItemID = imeBL.SelectItemID(ItemCode);

                    ime.ID = ItemID;
                    
                    int userID = Convert.ToInt32(Session["User_ID"].ToString());
                    ime.Updated_By = userID;

                    ime = GetItemData();
                    if (Validation())
                    {
                        if (imeBL.SaveEdit(ime, "Edit") > 0)
                        {
                            //1.Change Ctrl_ID=d from Item_Category table for Previous Category List
                            //2.Insert Ctrl_ID=n from Item_Category table for New Category List
                            if (CategoryList != null)
                            {
                                itemCategoryBL.Insert(ItemID, CategoryList);
                            }

                            //Delete previous shop from Item_Shop table and then insert new shop or not
                            InsertShopList(ItemID);

                            //Delete previous photo from Item_Image table and then insert new photo or not
                            InsertPhoto(ItemID);

                            //Delete previous related item from Item_RelatedItem table and then insert new related item or not
                            InsertRelatedItem(ItemID);

                            //Delete previous option from Item_Option table and then insert new option or not
                            InsertOption(ItemID);

                            //Delete previous yahoo specific from Item_YahooSpecificValue table and then insert new yahoo specific or not
                            if (YahooSpecificValue != null)
                            {
                                InsertYahooSpecificValue(ItemID);
                            }

                            #region Clear Session
                            Session.Remove("CategoryList");
                            Session.Remove("Related_Item_List");
                            Session.Remove("Mall_Category_ID");
                            Session.Remove("Option");
                            Session.Remove("YahooSpecificValue");
                            Session.Remove("ImageList");
                            Session.Remove("myDatatable"); // for Preview Page
                            #endregion
                            MessageBox("Updating Successful ! ");
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

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList myData =  new ArrayList();
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

                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('../Item/Item_Preview_Form.aspx','_blank');", true);
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
                if (ItemCode != null)
                {
                    int ItemID = imeBL.SelectItemID(ItemCode);

                    ime.ID = ItemID;

                    int userID = Convert.ToInt32(Session["User_ID"].ToString());
                    ime.Updated_By = userID;

                    ime = GetItemData();
                    if (Validation())
                    {
                        if (imeBL.SaveEdit(ime, "Edit") > 0)
                        {
                            //1.Change Ctrl_ID=d from Item_Category table for Previous Category List
                            //2.Insert Ctrl_ID=n from Item_Category table for New Category List
                            if (CategoryList != null)
                            {
                                itemCategoryBL.Insert(ItemID, CategoryList);
                            }

                            //Delete previous shop from Item_Shop table and then insert new shop or not
                            InsertShopList(ItemID);

                            //Delete previous photo from Item_Image table and then insert new photo or not
                            InsertPhoto(ItemID);

                            //Delete previous related item from Item_RelatedItem table and then insert new related item or not
                            InsertRelatedItem(ItemID);

                            //Delete previous option from Item_Option table and then insert new option or not
                            InsertOption(ItemID);

                            //Delete previous yahoo specific from Item_YahooSpecificValue table and then insert new yahoo specific or not
                            if (YahooSpecificValue != null)
                            {
                                InsertYahooSpecificValue(ItemID);
                            }

                            #region Clear Session
                            Session.Remove("CategoryList");
                            Session.Remove("Related_Item_List");
                            Session.Remove("Mall_Category_ID");
                            Session.Remove("Option");
                            Session.Remove("YahooSpecificValue");
                            Session.Remove("ImageList");
                            Session.Remove("myDatatable"); // for Preview Page
                            #endregion
                        }
                    }
                    if (imeBL.ChangeExport_Status(txtItem_Code.Text))
                    {
                        MessageBox("Data Complete ! ");
                    }
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
                    //Response.Redirect(ViewState["UrlReferrer"].ToString());
                    object referrer = ViewState["UrlReferrer"];
                    string url = (string)referrer;
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
                else
                {
                    string cleanMessage = message.Replace("'", "\\'");
                    string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                    {
                        page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public bool Validation()
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
            #endregion
                    //if (CategoryList != null && CategoryList.Rows.Count > 0)
                    //{
                    //    DataTable dtCategory = CategoryList;
                    //    foreach (DataRow dr in dtCategory.Rows)
                    //    {
                    //        length = Encoding.GetEncoding(932).GetByteCount(dr["CName"].ToString());
                    //        if (length > 60)
                    //        {
                    //            MessageBox("Invalid ショップカテゴリ path ! ");
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

        #region Item_Master

        /// <summary>
        /// To get Item Master fields from input form
        /// </summary>
        /// <returns> Item Master Entity </returns>
        public Item_Master_Entity GetItemData()
        {
            user = new UserRoleBL();
            int userID = Convert.ToInt32(Session["User_ID"].ToString());

            try
            {
                ime.Ctrl_ID = hdfCtrl_ID.Value;
                ime.Item_Code = txtItem_Code.Text;
                ime.Updated_By = userID;
                ime.Item_Name = txtItem_Name.Text;
                ime.Product_Code = txtProduct_Code.Text;
                if (!string.IsNullOrWhiteSpace(txtRelease_Date.Text))
                {
                    ime.Release_Date = Convert.ToDateTime(txtRelease_Date.Text);
                }
                if (!string.IsNullOrWhiteSpace(txtPost_Available_Date.Text))
                {
                    ime.Post_Available_Date = Convert.ToDateTime(txtPost_Available_Date.Text);
                }
                ime.Season = txtSeason.Text;
                ime.Brand_Code = txtBrand_Code.Text;
                ime.Competition_Name = txtCompetition_Name.Text;
                ime.Class_Name = txtClass_Name.Text;
                ime.Catalog_Information = txtCatalog_Information.Text;
                ime.Merchandise_Information = txtMerchandise_Information.Text;
                ime.Item_Description_PC = txtItem_Description_PC.Text;
                ime.Sale_Description_PC = txtSale_Description_PC.Text;
                ime.Smart_Template = txtSmart_Template.Text;
                ime.Additional_2 = txtAdditional_2.Text;
                ime.Additional_3 = txtAdditional_3.Text;
                ime.BlackMarket_Password = txtBlackMarket_Password.Text;
                ime.DoublePrice_Ctrl_No = txtDoublePrice_Ctrl_No.Text;
                if (!string.IsNullOrWhiteSpace(txtExtra_Shipping.Text))
                {
                    ime.Extra_Shipping = Convert.ToInt32(txtExtra_Shipping.Text);
                }

                ime.Year = txtYear.Text;

                if (!string.IsNullOrWhiteSpace(txtList_Price.Text))
                {
                    ime.List_Price = int.Parse(txtList_Price.Text);
                }

                if (!string.IsNullOrWhiteSpace(txtSale_Price.Text))
                {
                    ime.Sale_Price = int.Parse(txtSale_Price.Text);
                }
                /*
                if (!string.IsNullOrWhiteSpace(txtRakuten_CategoryID.Text) && Convert.ToInt32(txtRakuten_CategoryID.Text) != 0)
                {
                    ime.Rakuten_CategoryID = Convert.ToInt32(txtRakuten_CategoryID.Text);
                }

                if (!string.IsNullOrWhiteSpace(txtYahoo_CategoryID.Text) && Convert.ToInt32(txtYahoo_CategoryID.Text) != 0)
                {
                    ime.Yahoo_CategoryID = Convert.ToInt32(txtYahoo_CategoryID.Text);
                }

                if (!string.IsNullOrWhiteSpace(txtPonpare_CategoryID.Text) && Convert.ToInt32(txtPonpare_CategoryID.Text) != 0)
                {
                    ime.Ponpare_CategoryID =Convert.ToInt32(txtPonpare_CategoryID.Text);
                }
                */
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
                return ime;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new Item_Master_Entity();
            }
            
        }

        /// <summary>
        /// To fill values into form from database by editing
        /// </summary>
        /// <param name="ime"></param>
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
                    txtRelease_Date.Text = (ime.Release_Date.ToString()).Replace("0:00:00", "");
                }
                else
                {
                    txtRelease_Date.Text = "";
                }
                if (!string.IsNullOrWhiteSpace(ime.Post_Available_Date.ToString()))
                {
                    txtPost_Available_Date.Text = (ime.Post_Available_Date.ToString()).Replace("0:00:00", "");
                }
                else
                {
                    txtPost_Available_Date.Text = "";
                }
                //txtPost_Available_Date.Text = ime.Post_Available_Date.ToString();

                txtSeason.Text = ime.Season;
                txtBrand_Code.Text = ime.Brand_Code;
                txtCompetition_Name.Text = ime.Competition_Name;
                txtClass_Name.Text = ime.Class_Name;
                txtCatalog_Information.Text = ime.Catalog_Information;
                txtMerchandise_Information.Text = ime.Merchandise_Information;
                txtItem_Description_PC.Text = ime.Item_Description_PC;
                txtSale_Description_PC.Text = ime.Sale_Description_PC;
                txtSmart_Template.Text = ime.Smart_Template;
                txtAdditional_2.Text = ime.Additional_2;
                txtAdditional_3.Text = ime.Additional_3;

                txtList_Price.Text = Convert.ToString(ime.List_Price);
                txtSale_Price.Text = Convert.ToString(ime.Sale_Price);

                txtYear.Text = Convert.ToString(ime.Year);
                //txtCost.Text = Convert.ToString(ime.Cost);

                ddlShipping_Flag.SelectedValue = Convert.ToString(ime.Shipping_Flag);
                ddlDelivery_Charges.SelectedValue = Convert.ToString(ime.Delivery_Charges);
                ddlWarehouse_Specified.SelectedValue = Convert.ToString(ime.Warehouse_Specified);

                txtBlackMarket_Password.Text = ime.BlackMarket_Password;
                txtDoublePrice_Ctrl_No.Text = ime.DoublePrice_Ctrl_No;

                if (ime.Extra_Shipping != 0)
                    txtExtra_Shipping.Text = ime.Extra_Shipping.ToString();
                else txtExtra_Shipping.Text = "";

                /*
                if (ime.Rakuten_CategoryID != 0)
                    txtRakuten_CategoryID.Text = ime.Rakuten_CategoryID.ToString();
                else txtRakuten_CategoryID.Text = "";

                if (ime.Yahoo_CategoryID != 0)
                    txtYahoo_CategoryID.Text = ime.Yahoo_CategoryID.ToString();
                else txtYahoo_CategoryID.Text = "";

                if (ime.Ponpare_CategoryID != 0)
                    txtPonpare_CategoryID.Text = ime.Ponpare_CategoryID.ToString();
                else txtPonpare_CategoryID.Text = "";
                 */
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
                //dv.Sort = "age asc";
                //dv.Sort = Category_SN + " " + SortDirection.Ascending;
                gvCatagories.DataSource = dv;
                gvCatagories.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        /// <summary>
        /// To display category with path in gridview
        /// </summary>
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
                        //Label lblValue = (Label)gvCatagories.Rows[rowIndex].Cells[1].FindControl("lblCTGName");
                        TextBox txtValue = (TextBox)gvCatagories.Rows[rowIndex].Cells[1].FindControl("txtCTGName");
                        lblID.Text = CategoryList.Rows[i]["CID"].ToString();
                        //lblValue.Text = CategoryList.Rows[i]["CName"].ToString();
                        txtValue.Text = CategoryList.Rows[i]["CName"].ToString();
                        rowIndex++;
                     }
                   }
                }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
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
                    //Label lblValue = (Label)gvCatagories.Rows[rowIndex].Cells[1].FindControl("lblCTGName");
                    TextBox txtValue = (TextBox)gvCatagories.Rows[rowIndex].Cells[1].FindControl("txtCTGName");
                    lblID.Text = CategoryList.Rows[i]["CID"].ToString();
                    //lblValue.Text = CategoryList.Rows[i]["CName"].ToString();
                    txtValue.Text = CategoryList.Rows[i]["CName"].ToString();
                    rowIndex++;
                }

                Category_BL catbl = new Category_BL();
                DataTable dt = new DataTable();
                dt = CategoryList;

                if (dt.Rows.Count > 0)
                {
                    String id = (dt.Rows[0]["CID"]).ToString();

                    if (dt.Rows.Count > 0)
                    {
                        DataTable driectdt = new DataTable();
                        driectdt = catbl.Get_CategoryID(id);
                        if (driectdt.Rows.Count > 0)
                        {
                            txtRakuten_CategoryID.Text = driectdt.Rows[0]["Rakutan_DirectoryID"].ToString();
                            txtYahoo_CategoryID.Text = driectdt.Rows[0]["Yahoo_CategoryID"].ToString();
                            txtPonpare_CategoryID.Text = driectdt.Rows[0]["Ponpare_CategoryID"].ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// To display category with their path
        /// </summary>
        /// <param name="CID">To select category name by category id</param>
        /// <example>ブランド別 » サ行 » スリクソン » ウェア</example>
        /// <returns>ブランド別 » サ行 » スリクソン » ウェア as string</returns>
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
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
            
        }

        //public string ShowSLHierarchy(int CID)
        //{
        //    //Array.Clear(ids, 0, 100);
        //    index = 0;
        //    int i = 0;
        //    int cat = 0;
        //    ids[index++] = CID.ToString();
        //    GetCategory(CID);

        //    while (!String.IsNullOrWhiteSpace(ids[i]))
        //    {
        //        DataTable dts = cbl.SelectForCatalogID(Convert.ToInt32(ids[i++]));
        //        if (dts != null && dts.Rows.Count > 0)
        //        {
        //            cx[cat++] = dts.Rows[0]["Category_SN"].ToString();
        //        }
        //    }

        //    for (int y = cat - 1; y >= 0; y--)
        //    {
        //        if (y > 0)
        //        {
        //            catpath += cx[y] + ">>";

        //        }
        //        else if (y == 0)
        //        {
        //            catpath += cx[y];
        //        }
        //    }

        //    return catpath;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
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
                    Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemID"></param>
        public void SetSelectedCategory(int itemID)
        {
            try
            {
                itemCategoryBL = new Item_Category_BL();

                DataTable dt = itemCategoryBL.SelectByItemID(itemID);

                //dt.Columns.Add("Category_SN", typeof(String));
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        treepath = string.Empty; catpath = string.Empty;
                        dt.Rows[i]["CName"] = ShowHierarchy(int.Parse(dt.Rows[i]["CID"].ToString()));
                        if (!string.IsNullOrWhiteSpace(dt.Rows[i]["Category_SN"].ToString()))
                        {
                            dt.Rows[i]["Category_SN"] = int.Parse(dt.Rows[i]["Category_SN"].ToString());
                        }
                        else
                        {
                            dt.Rows[i]["Category_SN"] = 0;
                        }

                        dt.AcceptChanges();
                    }
                }
                Session["CategoryList"] = dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        #endregion

        #region Mall_Category

        /// <summary>
        /// To display Mall Category for Rakuten, Yahoo and Ponpare
        /// </summary>
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
                    }
                    else if (dt.Rows[0]["Mall_ID"].ToString() == "2")
                    {
                        txtYahoo_CategoryID.Text = dt.Rows[0]["Category_ID"].ToString();
                    }
                    else if (dt.Rows[0]["Mall_ID"].ToString() == "3")
                    {
                        txtPonpare_CategoryID.Text = dt.Rows[0]["Category_ID"].ToString();
                    }
                }
                else
                {
                    txtRakuten_CategoryID.Text = "";
                    txtYahoo_CategoryID.Text = "";
                    txtPonpare_CategoryID.Text = "";
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        #endregion

        #region Shop

        /// <summary>
        ///To display shop list in dropdown field by using Shop table
        /// </summary>
        public void BindShop()
        {
            try
            {
                Shop_BL shopBL = new Shop_BL();
                DataTable dt = shopBL.SelectAll();

                DataRow[] dr = dt.Select("Shop_Code='Racket Plaza'");
                if (dr.Length > 0)
                {
                    dlShop1.DataSource = dt.Select("Shop_Code='Racket Plaza'").CopyToDataTable();
                    dlShop1.DataBind();
                }

                dr = dt.Select("Shop_Code='Rack piece'");
                if (dr.Length > 0)
                {
                    dlShop2.DataSource = dt.Select("Shop_Code='Rack piece'").CopyToDataTable();
                    dlShop2.DataBind();
                }

                dr = dt.Select("Shop_Code='Sports Plaza'");
                if (dr.Length > 0)
                {
                    dlShop3.DataSource = dt.Select("Shop_Code='Sports Plaza'").CopyToDataTable();
                    dlShop3.DataBind();
                }

                dr = dt.Select("Shop_Code='Baseball Plaza'");
                if (dr.Length > 0)
                {
                    dlShop4.DataSource = dt.Select("Shop_Code='Baseball Plaza'").CopyToDataTable();
                    dlShop4.DataBind();
                }

                dr = dt.Select("Shop_Code='Table Tennis Honpo'");
                if (dr.Length > 0)
                {
                    dlShop5.DataSource = dt.Select("Shop_Code='Table Tennis Honpo'").CopyToDataTable();
                    dlShop5.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
            
            //cblShopList.DataSource = shopBL.SelectAll();
            //cblShopList.DataTextField = "Shop_Name";
            //cblShopList.DataValueField = "ID";
            //cblShopList.DataBind();
        }

        /// <summary>
        /// get shop id to insert or update in Item_Shop database
        /// </summary>
        /// <returns>Shop list</returns>
        /// 
        public void InsertShopList(int itemID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemID", typeof(int));
                dt.Columns.Add("ShopID", typeof(int));

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
                            dt.Rows.Add(dr);
                        }
                    }
                }

                Item_Shop_BL itemShopBL = new Item_Shop_BL();
                if (dt.Rows.Count < 1)
                {
                    dt.Rows.Add(itemID, 0);
                }
                //itemShopBL.Insert(dt);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        /// <summary>
        /// To display or edit shop list 
        /// </summary>
        /// <param name="itemID">By the seleted item master id</param>
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
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        #endregion

        #region Photo

        /// <summary>
        /// 
        /// </summary>
        public void BindPhotoList()
        {
            try
            {
                if (ImageList != null)
                {
                    DataTable dt = ImageList as DataTable;
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
                    DataRow[] dr = dt.Select("Image_Type='0'");
                    if (dr.Length > 0)
                    {
                        DataTable dtImage = dt.Select("Image_Type='0'").CopyToDataTable();
                        if (dtImage.Rows.Count > 0 && !string.IsNullOrWhiteSpace(dtImage.Rows[0]["Image_Name"].ToString()))
                        {
                            Image1.ImageUrl = imagePath + dtImage.Rows[0]["Image_Name"] + "";
                        }
                        else
                        {
                            Image1.ImageUrl = "";
                        }
                        if (dtImage.Rows.Count > 1 && !string.IsNullOrWhiteSpace(dtImage.Rows[1]["Image_Name"].ToString()))
                        {
                            Image2.ImageUrl = imagePath + dtImage.Rows[1]["Image_Name"] + "";
                        }
                        else
                        {
                            Image2.ImageUrl = "";
                        }
                        if (dtImage.Rows.Count > 2 && !string.IsNullOrWhiteSpace(dtImage.Rows[2]["Image_Name"].ToString()))
                        {
                            Image3.ImageUrl = imagePath + dtImage.Rows[2]["Image_Name"] + "";
                        }
                        else
                        {
                            Image3.ImageUrl = "";
                        }
                        if (dtImage.Rows.Count > 3 && !string.IsNullOrWhiteSpace(dtImage.Rows[3]["Image_Name"].ToString()))
                        {
                            Image4.ImageUrl = imagePath + dtImage.Rows[3]["Image_Name"] + "";
                        }
                        else
                        {
                            Image4.ImageUrl = "";
                        }
                        if (dtImage.Rows.Count > 4 && !string.IsNullOrWhiteSpace(dtImage.Rows[4]["Image_Name"].ToString()))
                        {
                            Image5.ImageUrl = imagePath + dtImage.Rows[4]["Image_Name"] + "";
                        }
                        else
                        {
                            Image5.ImageUrl = "";
                        }
                    }
                    else
                    {
                        Image1.ImageUrl = "";
                        Image2.ImageUrl = "";
                        Image3.ImageUrl = "";
                        Image4.ImageUrl = "";
                        Image5.ImageUrl = "";
                    }

                    dr = dt.Select("Image_Type='1'");
                    if (dr.Length > 0)
                    {
                        DataTable dtLibrary = dt.Select("Image_Type='1'").CopyToDataTable();
                        if (dtLibrary.Rows.Count > 0)
                        {
                            txtLibraryImage1.Text = dtLibrary.Rows[0]["Image_Name"] + "";
                        }
                        if (dtLibrary.Rows.Count > 1)
                        {
                            txtLibraryImage2.Text = dtLibrary.Rows[1]["Image_Name"] + "";
                        }
                        if (dtLibrary.Rows.Count > 2)
                        {
                            txtLibraryImage3.Text = dtLibrary.Rows[2]["Image_Name"] + "";
                        }
                        if (dtLibrary.Rows.Count > 3)
                        {
                            txtLibraryImage4.Text = dtLibrary.Rows[3]["Image_Name"] + "";
                        }
                        if (dtLibrary.Rows.Count > 4)
                        {
                            txtLibraryImage5.Text = dtLibrary.Rows[4]["Image_Name"] + "";
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
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void ReBindPhotoList()
        { 
            try
            {
                if (ImageList != null)
                {
                    DataTable dt = ImageList as DataTable;
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
                    DataRow[] dr = dt.Select("Image_Type='0'");
                    if (dr.Length > 0)
                    {
                        DataTable dtImage = dt.Select("Image_Type='0'").CopyToDataTable();
                        if (dtImage.Rows.Count > 0 && !string.IsNullOrWhiteSpace(dtImage.Rows[0]["Image_Name"].ToString()))
                        {
                            Image1.ImageUrl = imagePath + dtImage.Rows[0]["Image_Name"] + "";
                        }
                        else
                        {
                            Image1.ImageUrl = "";
                        }
                        if (dtImage.Rows.Count > 1 && !string.IsNullOrWhiteSpace(dtImage.Rows[1]["Image_Name"].ToString()))
                        {
                            Image2.ImageUrl = imagePath + dtImage.Rows[1]["Image_Name"] + "";
                        }
                        else
                        {
                            Image2.ImageUrl = "";
                        }
                        if (dtImage.Rows.Count > 2 && !string.IsNullOrWhiteSpace(dtImage.Rows[2]["Image_Name"].ToString()))
                        {
                            Image3.ImageUrl = imagePath + dtImage.Rows[2]["Image_Name"] + "";
                        }
                        else
                        {
                            Image3.ImageUrl = "";
                        }
                        if (dtImage.Rows.Count > 3 && !string.IsNullOrWhiteSpace(dtImage.Rows[3]["Image_Name"].ToString()))
                        {
                            Image4.ImageUrl = imagePath + dtImage.Rows[3]["Image_Name"] + "";
                        }
                        else
                        {
                            Image4.ImageUrl = "";
                        }
                        if (dtImage.Rows.Count > 4 && !string.IsNullOrWhiteSpace(dtImage.Rows[4]["Image_Name"].ToString()))
                        {
                            Image5.ImageUrl = imagePath + dtImage.Rows[4]["Image_Name"] + "";
                        }
                        else
                        {
                            Image5.ImageUrl = "";
                        }
                    }
                    else
                    {
                        Image1.ImageUrl = "";
                        Image2.ImageUrl = "";
                        Image3.ImageUrl = "";
                        Image4.ImageUrl = "";
                        Image5.ImageUrl = "";
                    }
                }
                else
                {
                    Image1.ImageUrl = "";
                    Image2.ImageUrl = "";
                    Image3.ImageUrl = "";
                    Image4.ImageUrl = "";
                    Image5.ImageUrl = "";
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        /// <summary>
        /// To insert and update image name by using Item_Image table
        /// </summary>
        /// <param name="itemID">To keep item master id</param>
        public void InsertPhoto(int itemID)
        {
            try
            {
                DataTable dtImage = ImageList as DataTable;
                Item_Image_BL itemImageBL = new Item_Image_BL();
                dtImage = SetLibraryPhoto(dtImage);
                for (int i = 0; i < dtImage.Rows.Count; i++)  //Re-arrange SN 
                {
                    dtImage.Rows[i]["SN"] = i + 1;
                }
                itemImageBL.Insert(itemID, dtImage);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
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

                    DataRow dr1 = dt.NewRow();
                    dr1["Image_Name"] = txtLibraryImage1.Text;
                    dr1["Image_Type"] = 1;
                    dt.Rows.Add(dr1);
                    DataRow dr2 = dt.NewRow();
                    dr2["Image_Name"] = txtLibraryImage2.Text;
                    dr2["Image_Type"] = 1;
                    dt.Rows.Add(dr2);
                    DataRow dr3 = dt.NewRow();
                    dr3["Image_Name"] = txtLibraryImage3.Text;
                    dr3["Image_Type"] = 1;
                    dt.Rows.Add(dr3);
                    DataRow dr4 = dt.NewRow();
                    dr4["Image_Name"] = txtLibraryImage4.Text;
                    dr4["Image_Type"] = 1;
                    dt.Rows.Add(dr4);
                    DataRow dr5 = dt.NewRow();
                    dr5["Image_Name"] = txtLibraryImage5.Text;
                    dr5["Image_Type"] = 1;
                    dt.Rows.Add(dr5);

                    return dt;
                }
                else   //exist ImageList
                {
                    DataTable dtCopy = dt.Copy();
                    for (int i = 0; i < dtCopy.Rows.Count; i++)
                    {
                        if (dtCopy.Rows[i]["Image_Type"].ToString() == "1")
                        {
                            dt.Rows[i].Delete();

                        }
                    }
                    dt.AcceptChanges();

                    DataRow dr1 = dt.NewRow();
                    dr1["Image_Name"] = txtLibraryImage1.Text;
                    dr1["Image_Type"] = 1;
                    dt.Rows.Add(dr1);

                    DataRow dr2 = dt.NewRow();
                    dr2["Image_Name"] = txtLibraryImage2.Text;
                    dr2["Image_Type"] = 1;
                    dt.Rows.Add(dr2);

                    DataRow dr3 = dt.NewRow();
                    dr3["Image_Name"] = txtLibraryImage3.Text;
                    dr3["Image_Type"] = 1;
                    dt.Rows.Add(dr3);

                    DataRow dr4 = dt.NewRow();
                    dr4["Image_Name"] = txtLibraryImage4.Text;
                    dr4["Image_Type"] = 1;
                    dt.Rows.Add(dr4);

                    DataRow dr5 = dt.NewRow();
                    dr5["Image_Name"] = txtLibraryImage5.Text;
                    dr5["Image_Type"] = 1;
                    dt.Rows.Add(dr5);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
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
                Session["ImageList"] = dtImage;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        /// <summary>
        /// To delete image
        /// </summary>
        /// <param name="imageID">image id which wants to delete</param>
        public void DeleteImage(int imageID)
        {
            try
            {
                Item_Image_BL itemImageBL = new Item_Image_BL();
                imeBL = new Item_Master_BL();
                itemImageBL.DeleteImage(imageID);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
            //BindPhotoList();
            //if (ItemCode != null)
            //{
            //    int ItemID = imeBL.SelectItemID(ItemCode);
            //    SelectByItemID(ItemID);
            //    BindPhotoList();
            //}
        }

        public void ReBindPhoto(DataTable dt)
        {
            try
            {
                DataRow[] dr = dt.Select("Image_Type='0'");
                if (dr.Length > 0)
                {
                    DataTable dtImage = dt.Select("Image_Type='0'").CopyToDataTable();
                    if (dtImage.Rows.Count > 0)
                    {
                        Image1.ImageUrl = imagePath + dtImage.Rows[0]["Image_Name"] + "";
                    }
                    if (dtImage.Rows.Count > 1)
                    {
                        Image2.ImageUrl = imagePath + dtImage.Rows[1]["Image_Name"] + "";
                    }
                    if (dtImage.Rows.Count > 2)
                    {
                        Image3.ImageUrl = imagePath + dtImage.Rows[2]["Image_Name"] + "";
                    }
                    if (dtImage.Rows.Count > 3)
                    {
                        Image4.ImageUrl = imagePath + dtImage.Rows[3]["Image_Name"] + "";
                    }
                    if (dtImage.Rows.Count > 4)
                    {
                        Image5.ImageUrl = imagePath + dtImage.Rows[4]["Image_Name"] + "";
                    }
                }

                dr = dt.Select("Image_Type='1'");
                if (dr.Length > 0)
                {
                    DataTable dtLibrary = dt.Select("Image_Type='1'").CopyToDataTable();
                    if (dtLibrary.Rows.Count > 0)
                    {
                        txtLibraryImage1.Text = dtLibrary.Rows[0]["Image_Name"] + "";
                    }
                    if (dtLibrary.Rows.Count > 1)
                    {
                        txtLibraryImage2.Text = dtLibrary.Rows[1]["Image_Name"] + "";
                    }
                    if (dtLibrary.Rows.Count > 2)
                    {
                        txtLibraryImage3.Text = dtLibrary.Rows[2]["Image_Name"] + "";
                    }
                    if (dtLibrary.Rows.Count > 3)
                    {
                        txtLibraryImage4.Text = dtLibrary.Rows[3]["Image_Name"] + "";
                    }
                    if (dtLibrary.Rows.Count > 4)
                    {
                        txtLibraryImage5.Text = dtLibrary.Rows[4]["Image_Name"] + "";
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
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
                Response.Redirect("~/CustomErrorPage.aspx?");
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
                Response.Redirect("~/CustomErrorPage.aspx?");
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
                    Session["Option"] = dt;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
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

                    Session["Option"] = dt;
                }
            }
            catch (Exception ex)
            {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
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
                Response.Redirect("~/CustomErrorPage.aspx?");
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
                Response.Redirect("~/CustomErrorPage.aspx?");
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
                    dr1["Related_ItemCode"] = txtRelated1.Text.Trim();
                    dr1["SN"] = 1;
                    dtRelated.Rows.Add(dr1);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated2.Text))
                {
                    DataRow dr2 = dtRelated.NewRow();
                    dr2["Item_ID"] = itemID;
                    dr2["Related_ItemCode"] = txtRelated2.Text.Trim();
                    dr2["SN"] = 2;
                    dtRelated.Rows.Add(dr2);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated3.Text))
                {
                    DataRow dr3 = dtRelated.NewRow();
                    dr3["Item_ID"] = itemID;
                    dr3["Related_ItemCode"] = txtRelated3.Text.Trim();
                    dr3["SN"] = 3;
                    dtRelated.Rows.Add(dr3);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated4.Text))
                {
                    DataRow dr4 = dtRelated.NewRow();
                    dr4["Item_ID"] = itemID;
                    dr4["Related_ItemCode"] = txtRelated4.Text.Trim();
                    dr4["SN"] = 4;
                    dtRelated.Rows.Add(dr4);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated5.Text))
                {
                    DataRow dr5 = dtRelated.NewRow();
                    dr5["Item_ID"] = itemID;
                    dr5["Related_ItemCode"] = txtRelated5.Text.Trim();
                    dr5["SN"] = 5;
                    dtRelated.Rows.Add(dr5);
                }

                Item_Related_Item_BL ItemRelatedBL = new Item_Related_Item_BL();
                ItemRelatedBL.Insert(itemID, dtRelated);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        #endregion

        #region YahooSpecificValue

        public void ShowValue()
        {
            try
            {
                if (YahooSpecificValue != null && YahooSpecificValue.Rows.Count > 0 )
                {
                    imgbYahooSpecValue.Disabled = false;

                    if (YahooSpecificValue.Rows.Count > 0)
                        txtYahooValue1.Text = YahooSpecificValue.Rows[0]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue1.Text = "";

                    if (YahooSpecificValue.Rows.Count > 1)
                        txtYahooValue2.Text = YahooSpecificValue.Rows[1]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue2.Text = "";

                    if (YahooSpecificValue.Rows.Count > 2)
                        txtYahooValue3.Text = YahooSpecificValue.Rows[2]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue3.Text = "";

                    if (YahooSpecificValue.Rows.Count > 3)
                        txtYahooValue4.Text = YahooSpecificValue.Rows[3]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue4.Text = "";

                    if (YahooSpecificValue.Rows.Count > 4)
                        txtYahooValue5.Text = YahooSpecificValue.Rows[4]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue5.Text = "";
                }
                else
                {
                    imgbYahooSpecValue.Disabled = true;
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
                Response.Redirect("~/CustomErrorPage.aspx?");
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
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void SetYahooSpacificValue(int ItemID)
        {
            try
            {
                Item_YahooSpecificValue_BL YahooSpecificValueBL = new Item_YahooSpecificValue_BL();
                Session["YahooSpecificValue"] = YahooSpecificValueBL.SelectByItemID(ItemID);
                ShowValue();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
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
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        #endregion

        //protected void btnDetail_Click(object sender, EventArgs e)
        //{
        //    foreach (DataListItem item in dlPhoto.Items)
        //    {
        //        RadioButton radioBtn = item.FindControl("rdoImage") as RadioButton;
        //        Label lmageID = (Label)item.FindControl("lblImage");
        //        if (radioBtn.Checked)
        //        {
        //            Response.Redirect("Item_Image_Detail.aspx?ID=" + lmageID.Text);
        //        }
        //    }

        //}

        //protected void btndivshowhide_Click(object sender, EventArgs e)
        //{
        //    if (btndivshowhide.Text.Equals("+"))
        //    {
        //        hideBox.Visible = true;
        //        btndivshowhide.Text = "-";
        //    }
        //    else if (btndivshowhide.Text.Equals("-"))
        //    {
        //        hideBox.Visible = false;
        //        btndivshowhide.Text = "+";
        //    }
        //}

        //protected void btnView_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        foreach (DataListItem item in dlPhoto.Items)
        //        {
        //            RadioButton radioBtn = item.FindControl("rdoImage") as RadioButton;
        //            Label lmageName = (Label)item.FindControl("lblImageName");
        //            if (radioBtn.Checked)
        //            {
        //                // Image view for new tab
        //                string pageurl = "../Item/Item_Image_View.aspx?ImageName=" + lmageName.Text;
        //                Response.Write("<script> window.open('" + pageurl + "','_blank' ); </script>");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["Exception"] = ex.ToString();
        //        Response.Redirect("~/CustomErrorPage.aspx?");
        //    }
        //}

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable dtImage = ImageList as DataTable;
        //        foreach (DataListItem item in dlPhoto.Items)
        //        {
        //            RadioButton radioBtn = item.FindControl("rdoImage") as RadioButton;
        //            Label lmageID = (Label)item.FindControl("lblImageID");
        //            if (radioBtn.Checked)
        //            {
        //                if (lmageID.Text == "0")
        //                {
        //                    Label lmageName = (Label)item.FindControl("lblImageName");
        //                    if (dtImage != null && dtImage.Rows.Count > 0)
        //                    {
        //                        for (int i = 0; i < dtImage.Rows.Count; i++)
        //                        {
        //                            if (dtImage.Rows[i]["Image_Name"].ToString() == lmageName.Text)
        //                            {
        //                                dtImage.Rows[i].Delete();
        //                                dtImage.AcceptChanges();
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (dtImage != null && dtImage.Rows.Count > 0)
        //                    {
        //                        for (int i = 0; i < dtImage.Rows.Count; i++)
        //                        {
        //                            if (dtImage.Rows[i]["ID"].ToString() == lmageID.Text)
        //                            {
        //                                dtImage.Rows[i].Delete();
        //                                dtImage.AcceptChanges();
        //                                break;
        //                            }
        //                        }
        //                    }
        //                    DeleteImage(Convert.ToInt32(lmageID.Text));
        //                    break;
        //                }
        //            }
        //        }
        //        ReBindPhoto(dtImage);
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["Exception"] = ex.ToString();
        //        Response.Redirect("~/CustomErrorPage.aspx?");
        //    }
        //}

        //public static byte[] StrToByteArray(string str)
        //{
        //    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        //    return encoding.GetBytes(str);
        //}

        // Functions
        //Choice PopForm
        /*
        /// <summary>
        /// To display related item in form
        /// </summary>
        public void DisplayRelatedItem()
        {
            int rowIndex = 0;
            gvRelatedItem.DataSource = RelatedItemList;
            gvRelatedItem.DataBind();
            if (RelatedItemList != null && RelatedItemList.Rows.Count > 0)
            {
                for (int i = rowIndex; i < RelatedItemList.Rows.Count; i++)
                {
                    Label lblRelatedID = (Label)gvRelatedItem.Rows[rowIndex].Cells[1].FindControl("lblRelatedID");
                    Label lblItemCode = (Label)gvRelatedItem.Rows[rowIndex].Cells[1].FindControl("lblItemCode");
                    lblRelatedID.Text = RelatedItemList.Rows[i]["Related_ItemID"].ToString();
                    lblItemCode.Text = RelatedItemList.Rows[i]["Related_ItemCode"].ToString();
                    rowIndex++;
                }
            }
        }

        /// <summary>
        /// To insert or update Related Item
        /// </summary>
        /// <param name="ItemID">To keep item master id</param>
        public void InsertRelatedItem(int ItemID)
        {
            Item_Related_Item_BL ItemRelatedBL = new Item_Related_Item_BL();
            ItemRelatedBL.Insert(ItemID, RelatedItemList);
        }
        */

    }
 }
