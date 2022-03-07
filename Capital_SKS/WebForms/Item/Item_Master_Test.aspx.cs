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

namespace ORS_RCM.WebForms.Item
{
    public partial class Item_Master_Test : System.Web.UI.Page
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

        public int Item_ID
        {
             get
            {
                if (Request.QueryString["Item_Code"] != null)
                {
                    return imeBL.SelectItemID(ItemCode);
                }
                else
                {
                    return 0;
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
        string imagePath = ConfigurationManager.AppSettings["ItemImage"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string ControlID = string.Empty;
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

                    BindShop(); //Bind Shop List'

                    if (ItemCode != null)    //Come from Item View for edit
                    {
                        //Change button name
                        btnSave.Text = "更新";
                        //lblHeader.Text = "商品情報編集";

                        int ItemID = imeBL.SelectItemID(ItemCode);

                        ime = imeBL.SelectByID(ItemID);  //Select From Item_Master Table
                        SetItemData(ime);

                        SetSelectedShop(ItemID);             //Select From Item_Shop Table

                        //Select From Item_Category Table
                        SetCategoryData();
                        //SetMallCategoryData();

                        SelectByItemID(ItemID);                //Select From Item_Image Table
                        BindPhotoList();

                        SetSelectedRelatedItem(ItemID);   //Select From Item_Related_Item Table
                        //DisplayRelatedItem();

                        gvSKUColor.DataSource = item.SelectSKUColor(ItemCode); //Select From Item Table
                        gvSKUColor.DataBind();

                        gvSKU.DataSource = item.SelectSKU(ItemCode); //Select From Item Table
                        gvSKU.DataBind();

                        DataTable dt = item.SelectSKUSize(ItemCode); //Select From Item Table
                        //dt.Columns.Remove("Color_Code");
                        //dt.Columns.Remove("Color_Name");
                        //dt.AcceptChanges();
                        gvSKUSize.DataSource = dt;
                        gvSKUSize.DataBind();

                        SelectTemplateDetail(ItemCode);  //Select From Template_Detail Table

                        GetOptionSelectByItemID(ItemID);    //Select From Item_Option Table

                        SetYahooSpacificValue(ItemID);   //Select From Item_YahooSpecificValue Table

                        ChangeNUll_To_Space();
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
                    }
                    else if (ControlID.Contains("btnAddCatagories"))
                    {
                        if (Session["btnPopClick_" + ItemCode] != null && Session["btnPopClick_" + ItemCode].ToString() == "ok")
                        {
                            SetMallCategoryData();
                            Session.Remove("btnPopClick_" + ItemCode);
                            //DisplayYahooSpecificValue(txtYahoo_CategoryID.Text); // Auto Display for first specific
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
                        //DisplayYahooSpecificValue(txtYahoo_CategoryID.Text); // Auto Display for first specific
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
                            //DisplayYahooSpecificValue(txtYahoo_CategoryID.Text); // Auto Display for first specific
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
                    string str = CheckLength(ime);
                    if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than length bytes!"); }
                    else
                    {
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

                                SaveTemplateDetail(ItemCode); // Insert or Update Template_Detail

                                #region Clear Session
                                //Session.Remove("CategoryList_"+ItemCode);
                                //Session.Remove("Related_Item_List");
                                //Session.Remove("Mall_Category_ID_"+ItemCode);
                                //Session.Remove("Option_"+ItemCode);
                                //Session.Remove("YahooSpecificValue_"+ItemCode);
                                //Session.Remove("ImageList_" + ItemCode);
                                //Session.Remove("myDatatable"); // for Preview Page
                                #endregion
                                MessageBox("Updating Successful ! ");
                            }
                        }
                    }
                }//
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
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
                    string str = CheckLength(ime);
                    if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than length bytes!"); }
                    else
                    {
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

                                SaveTemplateDetail(ItemCode); // Insert or Update Template_Detail

                                #region Clear Session
                                //Session.Remove("CategoryList_" + ItemCode);
                                //Session.Remove("Related_Item_List");
                                //Session.Remove("Mall_Category_ID_" + ItemCode);
                                //Session.Remove("Option_" + ItemCode);
                                //Session.Remove("YahooSpecificValue_" + ItemCode);
                                //Session.Remove("ImageList_" + ItemCode);
                                //Session.Remove("myDatatable"); // for Preview Page
                                #endregion
                            }

                        }
                        if (imeBL.ChangeExport_Status(txtItem_Code.Text))
                        {
                            MessageBox("Data Complete ! ");
                        }
                    }
                }//
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string confirmValue = Request.Form["confirm_value"];
        //        if (confirmValue == "はい")
        //        {
        //            imeBL = new Item_Master_BL();
        //            imeBL.DeleteItem(ItemCode);
        //            MessageBox("Delete Successful ! ");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["Exception"] = ex.ToString();
        //        Response.Redirect("~/CustomErrorPage.aspx?");
        //    }
        //}

        public void MessageBox(string message)
        {
            try
            {
                if (message == "Saving Successful ! " || message == "Updating Successful ! " || message == "Data Complete ! ")
                {
                    //object referrer = ViewState["UrlReferrer"];
                    //string url = (string)referrer;
                    //string script = "window.onload = function(){ alert('";
                    //script += message;
                    //script += "');";
                    //script += "window.location = '";
                    //script += url;
                    //script += "'; }";
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
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
                        page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
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
                ime.Brand_Name = txtBrand_Name.Text;
                ime.Brand_Code = txtBrand_Code.Text;
                ime.Competition_Name = txtCompetition_Name.Text;
                ime.Class_Name = txtClass_Name.Text;
                ime.Catalog_Information = txtCatalog_Information.Text;
                ime.Merchandise_Information = txtMerchandise_Information.Text;
                ime.Zett_Item_Description = txtZett_Item_Description.Text;
                ime.Zett_Sale_Description = txtZett_Sale_Description.Text;
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
                    ime.List_Price = int.Parse(txtList_Price.Text.Replace(",", string.Empty));
                }

                if (!string.IsNullOrWhiteSpace(txtSale_Price.Text))
                {
                    ime.Sale_Price = int.Parse(txtSale_Price.Text.Replace(",", string.Empty));
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
                return ime;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
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
                    //txtRelease_Date.Text = (ime.Release_Date.ToString()).Replace("0:00:00", "");
                    string str = ime.Release_Date.ToString();
                    txtRelease_Date.Text = str.Substring(0, 10);
                }
                else
                {
                    txtRelease_Date.Text = "";
                }
                if (!string.IsNullOrWhiteSpace(ime.Post_Available_Date.ToString()))
                {
                    //txtPost_Available_Date.Text = (ime.Post_Available_Date.ToString()).Replace("0:00:00", "");
                    string str = ime.Post_Available_Date.ToString();
                    txtPost_Available_Date.Text = str.Substring(0, 10);
                }
                else
                {
                    txtPost_Available_Date.Text = "";
                }
                //txtPost_Available_Date.Text = ime.Post_Available_Date.ToString();

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

                //string.Format("{0:#,#}", num)
                //Convert.ToString(ime.List_Price)
                txtList_Price.Text = string.Format("{0:#,#}", ime.List_Price);
                txtSale_Price.Text = string.Format("{0:#,#}", ime.Sale_Price);

                txtYear.Text = Convert.ToString(ime.Year);

                ddlShipping_Flag.SelectedValue = Convert.ToString(ime.Shipping_Flag);
                ddlDelivery_Charges.SelectedValue = Convert.ToString(ime.Delivery_Charges);
                ddlWarehouse_Specified.SelectedValue = Convert.ToString(ime.Warehouse_Specified);

                txtBlackMarket_Password.Text = ime.BlackMarket_Password;
                txtDoublePrice_Ctrl_No.Text = ime.DoublePrice_Ctrl_No;

                if (ime.Extra_Shipping != 0)
                    txtExtra_Shipping.Text = ime.Extra_Shipping.ToString();
                else txtExtra_Shipping.Text = "";

                if (ime.Rakuten_CategoryID == "0")
                    txtRakuten_CategoryID.Text = "";
                else
                    txtRakuten_CategoryID.Text = ime.Rakuten_CategoryID.ToString();
                if (ime.Yahoo_CategoryID == "0")
                    txtYahoo_CategoryID.Text = "";
                else
                    txtYahoo_CategoryID.Text = ime.Yahoo_CategoryID.ToString();
                if (ime.Wowma_CategoryID == "0")
                    txtPonpare_CategoryID.Text = "";
                else
                    txtPonpare_CategoryID.Text = ime.Wowma_CategoryID.ToString();
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
                Response.Redirect("~/CustomErrorPage.aspx?", false);
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
                DataTable dt = SetSelectedCategory(Item_ID); 
                gvCatagories.DataSource = dt;
                gvCatagories.DataBind();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = rowIndex; i < dt.Rows.Count; i++)
                    {
                        Label lblID = (Label)gvCatagories.Rows[rowIndex].Cells[1].FindControl("lblID");
                        TextBox txtValue = (TextBox)gvCatagories.Rows[rowIndex].Cells[1].FindControl("txtCTGName");
                        lblID.Text = dt.Rows[i]["CID"].ToString();
                        txtValue.Text = dt.Rows[i]["CName"].ToString();
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
                            txtYahoo_CategoryID.Text = driectdt.Rows[0]["Yahoo_CategoryID"].ToString();
                            txtPonpare_CategoryID.Text = driectdt.Rows[0]["Wowma_CategoryID"].ToString();
                        }
                    }
                }
                else
                {
                    txtRakuten_CategoryID.Text = string.Empty;
                    txtYahoo_CategoryID.Text = string.Empty;
                    txtPonpare_CategoryID.Text = string.Empty;
                    //MallCategoryID.Clear();
                }
            }
            else
            {
                txtRakuten_CategoryID.Text = string.Empty;
                txtYahoo_CategoryID.Text = string.Empty;
                txtPonpare_CategoryID.Text = string.Empty;
                //MallCategoryID.Clear();
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
                Response.Redirect("~/CustomErrorPage.aspx?", false);
                return String.Empty;
            }
        }

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
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemID"></param>
        public DataTable SetSelectedCategory(int itemID)
        {
            try
            {
                itemCategoryBL = new Item_Category_BL();

                DataTable dt = itemCategoryBL.SelectByItemID(itemID);

                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
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
                //else
                //{
                //    txtRakuten_CategoryID.Text = "";
                //    txtYahoo_CategoryID.Text = "";
                //    txtPonpare_CategoryID.Text = "";
                //}
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
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
                Response.Redirect("~/CustomErrorPage.aspx?", false);
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
                Response.Redirect("~/CustomErrorPage.aspx?", false);
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
                Response.Redirect("~/CustomErrorPage.aspx?", false);
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
                        hlImage1.NavigateUrl = "";
                        hlImage2.NavigateUrl = "";
                        hlImage3.NavigateUrl = "";
                        hlImage4.NavigateUrl = "";
                        hlImage5.NavigateUrl = "";
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
                    hlImage1.NavigateUrl = "";
                    hlImage2.NavigateUrl = "";
                    hlImage3.NavigateUrl = "";
                    hlImage4.NavigateUrl = "";
                    hlImage5.NavigateUrl = "";
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
                //for (int i = 0; i < dtImage.Rows.Count; i++)  //Re-arrange SN 
                //{
                //    dtImage.Rows[i]["SN"] = i + 1;
                //}
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
                    //if (!string.IsNullOrWhiteSpace(txtLibraryImage1.Text))
                    //{
                    DataRow dr1 = dt.NewRow();
                    dr1["Image_Name"] = txtLibraryImage1.Text;
                    dr1["Image_Type"] = 1;
                    dr1["SN"] = 1;
                    dt.Rows.Add(dr1);
                    // }
                    //if (!string.IsNullOrWhiteSpace(txtLibraryImage2.Text))
                    //{
                    DataRow dr2 = dt.NewRow();
                    dr2["Image_Name"] = txtLibraryImage2.Text;
                    dr2["Image_Type"] = 1;
                    dr2["SN"] = 2;
                    dt.Rows.Add(dr2);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtLibraryImage3.Text))
                    //{
                    DataRow dr3 = dt.NewRow();
                    dr3["Image_Name"] = txtLibraryImage3.Text;
                    dr3["Image_Type"] = 1;
                    dr3["SN"] = 3;
                    dt.Rows.Add(dr3);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtLibraryImage4.Text))
                    //{
                    DataRow dr4 = dt.NewRow();
                    dr4["Image_Name"] = txtLibraryImage4.Text;
                    dr4["Image_Type"] = 1;
                    dr4["SN"] = 4;
                    dt.Rows.Add(dr4);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtLibraryImage5.Text))
                    //{
                    DataRow dr5 = dt.NewRow();
                    dr5["Image_Name"] = txtLibraryImage5.Text;
                    dr5["Image_Type"] = 1;
                    dr5["SN"] = 5;
                    dt.Rows.Add(dr5);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtLibraryImage6.Text))
                    //{
                    DataRow dr6 = dt.NewRow();
                    dr6["Image_Name"] = txtLibraryImage6.Text;
                    dr6["Image_Type"] = 1;
                    dr6["SN"] = 6;
                    dt.Rows.Add(dr6);
                    // }
                    #endregion

                    #region CampaginImage
                    //if (!string.IsNullOrWhiteSpace(txtCampaignImage1.Text))
                    //{
                    DataRow dr7 = dt.NewRow();
                    dr7["Image_Name"] = txtCampaignImage1.Text;
                    dr7["Image_Type"] = 2;
                    dr7["SN"] = 1;
                    dt.Rows.Add(dr7);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtCampaignImage2.Text))
                    //{
                    DataRow dr8 = dt.NewRow();
                    dr8["Image_Name"] = txtCampaignImage2.Text;
                    dr8["Image_Type"] = 2;
                    dr8["SN"] = 2;
                    dt.Rows.Add(dr8);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtCampaignImage3.Text))
                    //{
                    DataRow dr9 = dt.NewRow();
                    dr9["Image_Name"] = txtCampaignImage3.Text;
                    dr9["Image_Type"] = 2;
                    dr9["SN"] = 3;
                    dt.Rows.Add(dr9);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtCampaignImage4.Text))
                    //{
                    DataRow dr10 = dt.NewRow();
                    dr10["Image_Name"] = txtCampaignImage4.Text;
                    dr10["Image_Type"] = 2;
                    dr10["SN"] = 4;
                    dt.Rows.Add(dr10);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtCampaignImage5.Text))
                    //{
                    DataRow dr11 = dt.NewRow();
                    dr11["Image_Name"] = txtCampaignImage5.Text;
                    dr11["Image_Type"] = 2;
                    dr11["SN"] = 5;
                    dt.Rows.Add(dr11);
                    // }
                    #endregion

                    return dt;
                }
                else   //exist ImageList
                {
                    //DataTable dtCopy = dt.Copy();
                    //for (int i = 0; i < dtCopy.Rows.Count; i++)
                    //{
                    //    if (dtCopy.Rows[i]["Image_Type"].ToString() == "1" || dtCopy.Rows[i]["Image_Type"].ToString() == "2")
                    //    {
                    //        dt.Rows[i].Delete();


                    //    }

                    //}
                    //  DataTable dtCopy = dt.Copy();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Image_Type"].ToString() == "1" || dt.Rows[i]["Image_Type"].ToString() == "2")
                        {
                            dt.Rows[i].Delete();


                        }

                    }



                    dt.AcceptChanges();

                    #region LibraryImage
                    //if (!string.IsNullOrWhiteSpace(txtLibraryImage1.Text))
                    //{
                    DataRow dr1 = dt.NewRow();
                    dr1["Image_Name"] = txtLibraryImage1.Text;
                    dr1["Image_Type"] = 1;
                    dr1["SN"] = 1;
                    dt.Rows.Add(dr1);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtLibraryImage2.Text))
                    //{
                    DataRow dr2 = dt.NewRow();
                    dr2["Image_Name"] = txtLibraryImage2.Text;
                    dr2["Image_Type"] = 1;
                    dr2["SN"] = 2;
                    dt.Rows.Add(dr2);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtLibraryImage3.Text))
                    //{
                    DataRow dr3 = dt.NewRow();
                    dr3["Image_Name"] = txtLibraryImage3.Text;
                    dr3["Image_Type"] = 1;
                    dr3["SN"] = 3;
                    dt.Rows.Add(dr3);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtLibraryImage4.Text))
                    //{
                    DataRow dr4 = dt.NewRow();
                    dr4["Image_Name"] = txtLibraryImage4.Text;
                    dr4["Image_Type"] = 1;
                    dr4["SN"] = 4;
                    dt.Rows.Add(dr4);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtLibraryImage5.Text))
                    //{
                    DataRow dr5 = dt.NewRow();
                    dr5["Image_Name"] = txtLibraryImage5.Text;
                    dr5["Image_Type"] = 1;
                    dr5["SN"] = 5;
                    dt.Rows.Add(dr5);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtLibraryImage6.Text))
                    //{
                    DataRow dr6 = dt.NewRow();
                    dr6["Image_Name"] = txtLibraryImage6.Text;
                    dr6["Image_Type"] = 1;
                    dr6["SN"] = 6;
                    dt.Rows.Add(dr6);
                    //}
                    #endregion

                    #region CampaginImage
                    //if (!string.IsNullOrWhiteSpace(txtCampaignImage1.Text))
                    //{
                    DataRow dr7 = dt.NewRow();
                    dr7["Image_Name"] = txtCampaignImage1.Text;
                    dr7["Image_Type"] = 2;
                    dr7["SN"] = 1;
                    dt.Rows.Add(dr7);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtCampaignImage2.Text))
                    //{
                    DataRow dr8 = dt.NewRow();
                    dr8["Image_Name"] = txtCampaignImage2.Text;
                    dr8["Image_Type"] = 2;
                    dr8["SN"] = 2;
                    dt.Rows.Add(dr8);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtCampaignImage3.Text))
                    //{
                    DataRow dr9 = dt.NewRow();
                    dr9["Image_Name"] = txtCampaignImage3.Text;
                    dr9["Image_Type"] = 2;
                    dr9["SN"] = 3;
                    dt.Rows.Add(dr9);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtCampaignImage4.Text))
                    //{
                    DataRow dr10 = dt.NewRow();
                    dr10["Image_Name"] = txtCampaignImage4.Text;
                    dr10["Image_Type"] = 2;
                    dr10["SN"] = 4;
                    dt.Rows.Add(dr10);
                    //}
                    //if (!string.IsNullOrWhiteSpace(txtCampaignImage5.Text))
                    //{
                    DataRow dr11 = dt.NewRow();
                    dr11["Image_Name"] = txtCampaignImage5.Text;
                    dr11["Image_Type"] = 2;
                    dr11["SN"] = 5;
                    dt.Rows.Add(dr11);
                    //}
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
                Session["ImageList_" + ItemCode] = dtImage;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        /// <summary>
        /// To delete image
        /// </summary>
        /// <param name="imageID">image id which wants to delete</param>
        //public void DeleteImage(int imageID)
        //{
        //    try
        //    {
        //        Item_Image_BL itemImageBL = new Item_Image_BL();
        //        imeBL = new Item_Master_BL();
        //        itemImageBL.DeleteImage(imageID);
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["Exception"] = ex.ToString();
        //        Response.Redirect("~/CustomErrorPage.aspx?");
        //    }
        //    //BindPhotoList();
        //    //if (ItemCode != null)
        //    //{
        //    //    int ItemID = imeBL.SelectItemID(ItemCode);
        //    //    SelectByItemID(ItemID);
        //    //    BindPhotoList();
        //    //}
        //}

        //public void ReBindPhoto(DataTable dt)
        //{
        //    try
        //    {
        //        #region Item Image
        //        DataRow[] dr = dt.Select("Image_Type='0'");
        //        if (dr.Length > 0)
        //        {
        //            DataTable dtImage = dt.Select("Image_Type='0'").CopyToDataTable();
        //            if (dtImage.Rows.Count > 0)
        //            {
        //                Image1.ImageUrl = imagePath + dtImage.Rows[0]["Image_Name"] + "";
        //                //hlImage1.NavigateUrl = imagePath + dtImage.Rows[0]["Image_Name"] + "";
        //            }
        //            if (dtImage.Rows.Count > 1)
        //            {
        //                Image2.ImageUrl = imagePath + dtImage.Rows[1]["Image_Name"] + "";
        //                hlImage2.NavigateUrl = imagePath + dtImage.Rows[1]["Image_Name"] + "";
        //            }
        //            if (dtImage.Rows.Count > 2)
        //            {
        //                Image3.ImageUrl = imagePath + dtImage.Rows[2]["Image_Name"] + "";
        //                hlImage3.NavigateUrl = imagePath + dtImage.Rows[2]["Image_Name"] + "";
        //            }
        //            if (dtImage.Rows.Count > 3)
        //            {
        //                Image4.ImageUrl = imagePath + dtImage.Rows[3]["Image_Name"] + "";
        //                hlImage4.NavigateUrl = imagePath + dtImage.Rows[3]["Image_Name"] + "";
        //            }
        //            if (dtImage.Rows.Count > 4)
        //            {
        //                Image5.ImageUrl = imagePath + dtImage.Rows[4]["Image_Name"] + "";
        //                hlImage5.NavigateUrl = imagePath + dtImage.Rows[4]["Image_Name"] + "";
        //            }
        //        }
        //        #endregion

        //        #region Library Image
        //        dr = dt.Select("Image_Type='1'");
        //        if (dr.Length > 0)
        //        {
        //            DataTable dtLibrary = dt.Select("Image_Type='1'").CopyToDataTable();
        //            if (dtLibrary.Rows.Count > 0)
        //            {
        //                txtLibraryImage1.Text = dtLibrary.Rows[0]["Image_Name"] + "";
        //            }
        //            if (dtLibrary.Rows.Count > 1)
        //            {
        //                txtLibraryImage2.Text = dtLibrary.Rows[1]["Image_Name"] + "";
        //            }
        //            if (dtLibrary.Rows.Count > 2)
        //            {
        //                txtLibraryImage3.Text = dtLibrary.Rows[2]["Image_Name"] + "";
        //            }
        //            if (dtLibrary.Rows.Count > 3)
        //            {
        //                txtLibraryImage4.Text = dtLibrary.Rows[3]["Image_Name"] + "";
        //            }
        //            if (dtLibrary.Rows.Count > 4)
        //            {
        //                txtLibraryImage5.Text = dtLibrary.Rows[4]["Image_Name"] + "";
        //            }
        //            if (dtLibrary.Rows.Count > 5)
        //            {
        //                txtLibraryImage6.Text = dtLibrary.Rows[5]["Image_Name"] + "";
        //            }
        //        }
        //        #endregion

        //        #region Campagin Image
        //        dr = dt.Select("Image_Type='2'");
        //        if (dr.Length > 0)
        //        {
        //            DataTable dtCampagin = dt.Select("Image_Type='2'").CopyToDataTable();
        //            if (dtCampagin.Rows.Count > 0)
        //            {
        //                txtCampaignImage1.Text = dtCampagin.Rows[0]["Image_Name"] + "";
        //            }
        //            if (dtCampagin.Rows.Count > 1)
        //            {
        //                txtCampaignImage2.Text = dtCampagin.Rows[1]["Image_Name"] + "";
        //            }
        //            if (dtCampagin.Rows.Count > 2)
        //            {
        //                txtCampaignImage3.Text = dtCampagin.Rows[2]["Image_Name"] + "";
        //            }
        //            if (dtCampagin.Rows.Count > 3)
        //            {
        //                txtCampaignImage4.Text = dtCampagin.Rows[3]["Image_Name"] + "";
        //            }
        //            if (dtCampagin.Rows.Count > 4)
        //            {
        //                txtCampaignImage5.Text = dtCampagin.Rows[4]["Image_Name"] + "";
        //            }
        //        }
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["Exception"] = ex.ToString();
        //        Response.Redirect("~/CustomErrorPage.aspx?");
        //    }
        //}

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
                    //imgbYahooSpecValue.Enabled = true;

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
                    txtTemplate1.Text = dttemplate.Rows[0]["Template1"].ToString();
                    txtTemplate2.Text = dttemplate.Rows[0]["Template2"].ToString();
                    txtTemplate3.Text = dttemplate.Rows[0]["Template3"].ToString();
                    txtTemplate4.Text = dttemplate.Rows[0]["Template4"].ToString();
                    txtTemplate5.Text = dttemplate.Rows[0]["Template5"].ToString();
                    txtTemplate6.Text = dttemplate.Rows[0]["Template6"].ToString();
                    txtTemplate7.Text = dttemplate.Rows[0]["Template7"].ToString();
                    txtTemplate8.Text = dttemplate.Rows[0]["Template8"].ToString();
                    txtTemplate9.Text = dttemplate.Rows[0]["Template9"].ToString();
                    txtTemplate10.Text = dttemplate.Rows[0]["Template10"].ToString();
                    txtTemplate11.Text = dttemplate.Rows[0]["Template11"].ToString();
                    txtTemplate12.Text = dttemplate.Rows[0]["Template12"].ToString();
                    txtTemplate13.Text = dttemplate.Rows[0]["Template13"].ToString();
                    txtTemplate14.Text = dttemplate.Rows[0]["Template14"].ToString();
                    txtTemplate15.Text = dttemplate.Rows[0]["Template15"].ToString();
                    txtTemplate16.Text = dttemplate.Rows[0]["Template16"].ToString();
                    txtTemplate17.Text = dttemplate.Rows[0]["Template17"].ToString();
                    txtTemplate18.Text = dttemplate.Rows[0]["Template18"].ToString();
                    txtTemplate19.Text = dttemplate.Rows[0]["Template19"].ToString();
                    txtTemplate20.Text = dttemplate.Rows[0]["Template20"].ToString();

                    txtTemplate_Content1.Text = dttemplate.Rows[0]["Template_Content1"].ToString();
                    txtTemplate_Content2.Text = dttemplate.Rows[0]["Template_Content2"].ToString();
                    txtTemplate_Content3.Text = dttemplate.Rows[0]["Template_Content3"].ToString();
                    txtTemplate_Content4.Text = dttemplate.Rows[0]["Template_Content4"].ToString();
                    txtTemplate_Content5.Text = dttemplate.Rows[0]["Template_Content5"].ToString();
                    txtTemplate_Content6.Text = dttemplate.Rows[0]["Template_Content6"].ToString();
                    txtTemplate_Content7.Text = dttemplate.Rows[0]["Template_Content7"].ToString();
                    txtTemplate_Content8.Text = dttemplate.Rows[0]["Template_Content8"].ToString();
                    txtTemplate_Content9.Text = dttemplate.Rows[0]["Template_Content9"].ToString();
                    txtTemplate_Content10.Text = dttemplate.Rows[0]["Template_Content10"].ToString();
                    txtTemplate_Content11.Text = dttemplate.Rows[0]["Template_Content11"].ToString();
                    txtTemplate_Content12.Text = dttemplate.Rows[0]["Template_Content12"].ToString();
                    txtTemplate_Content13.Text = dttemplate.Rows[0]["Template_Content13"].ToString();
                    txtTemplate_Content14.Text = dttemplate.Rows[0]["Template_Content14"].ToString();
                    txtTemplate_Content15.Text = dttemplate.Rows[0]["Template_Content15"].ToString();
                    txtTemplate_Content16.Text = dttemplate.Rows[0]["Template_Content16"].ToString();
                    txtTemplate_Content17.Text = dttemplate.Rows[0]["Template_Content17"].ToString();
                    txtTemplate_Content18.Text = dttemplate.Rows[0]["Template_Content18"].ToString();
                    txtTemplate_Content19.Text = dttemplate.Rows[0]["Template_Content19"].ToString();
                    txtTemplate_Content20.Text = dttemplate.Rows[0]["Template_Content20"].ToString();

                    txtDetail_Template1.Text = dttemplate.Rows[0]["Detail_Template1"].ToString();
                    txtDetail_Template2.Text = dttemplate.Rows[0]["Detail_Template2"].ToString();
                    txtDetail_Template3.Text = dttemplate.Rows[0]["Detail_Template3"].ToString();
                    txtDetail_Template4.Text = dttemplate.Rows[0]["Detail_Template4"].ToString();

                    txtDetail_Template_Content1.Text = dttemplate.Rows[0]["Detail_Template_Content1"].ToString();
                    txtDetail_Template_Content2.Text = dttemplate.Rows[0]["Detail_Template_Content2"].ToString();
                    txtDetail_Template_Content3.Text = dttemplate.Rows[0]["Detail_Template_Content3"].ToString();
                    txtDetail_Template_Content4.Text = dttemplate.Rows[0]["Detail_Template_Content4"].ToString();

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

        protected void gvSKU_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    GridView HeaderGrid = (GridView)sender;
            //    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //    TableCell HeaderCell = new TableCell();
            //    HeaderCell.Text = "SKU";
            //    HeaderCell.ColumnSpan =10;
            //    HeaderGridRow.Cells.Add(HeaderCell);
            //    HeaderCell.Style.Add("background-color", "#dddddd");
            //    //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //    HeaderCell.Style.Add("text-align", "center");
            //    gvSKU.Controls[0].Controls.AddAt(0, HeaderGridRow);
            //}
        }

        protected void gvSKUSize_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    GridView HeaderGrid = (GridView)sender;
            //    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //    TableCell HeaderCell = new TableCell();
            //    HeaderCell.Text = "SKU（サイズ）";
            //    HeaderCell.ColumnSpan = 10;
            //    HeaderGridRow.Cells.Add(HeaderCell);
            //    HeaderCell.Style.Add("background-color", "#dddddd");
            //    //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //    HeaderCell.Style.Add("text-align", "center");
            //    gvSKUSize.Controls[0].Controls.AddAt(0, HeaderGridRow);
            //}
        }

        protected void gvSKUColor_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    GridView HeaderGrid = (GridView)sender;
            //    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //    TableCell HeaderCell = new TableCell();
            //    HeaderCell.Text = "SKU（カラー）";
            //    HeaderCell.ColumnSpan = 10;
            //    HeaderGridRow.Cells.Add(HeaderCell);
            //    HeaderCell.Style.Add("background-color", "#dddddd");
            //    //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //    HeaderCell.Style.Add("text-align", "center");
            //    gvSKUColor.Controls[0].Controls.AddAt(0, HeaderGridRow);
            //}

        }

    }
}