/* 
Created By              :EiPhyo
Created Date          : 26/08/2014
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

namespace ORS_RCM.WebForms.Item
{
    public partial class Import_ShopItem_List : System.Web.UI.Page
    {
        
        Import_Shop_ItemList_BL importshBl;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    importshBl = new Import_Shop_ItemList_BL();

                    DataTable dt = importshBl.SelectAll();

                    gvImportshop.DataSource = dt;
                    gvImportshop.DataBind();

                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                importshBl = new Import_Shop_ItemList_BL();
                if (!String.IsNullOrWhiteSpace(txtItemCode.Text))
                {

                    dt = importshBl.Import_ShopItemSearch(txtItemCode.Text.Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        gvImportshop.DataSource = dt;

                        gvImportshop.DataBind();

                    }


                }
                else
                {
                    dt = importshBl.SelectAll();
                    gvImportshop.DataSource = dt;

                    gvImportshop.DataBind();



                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvImportShop_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                importshBl = new Import_Shop_ItemList_BL();
                DataTable dt = new DataTable();
                if ((((!String.IsNullOrWhiteSpace(txtItemCode.Text)) && (e.NewPageIndex > -1))))
                {
                    dt = importshBl.Import_ShopItemSearch(txtItemCode.Text.Trim());

                    gvImportshop.DataSource = dt;
                    gvImportshop.PageIndex = e.NewPageIndex;
                    gvImportshop.DataBind();

                }
                else
                {
                    if (e.NewPageIndex > -1)
                    {
                        dt = importshBl.Import_ShopItemSearch(txtItemCode.Text.Trim());
                        gvImportshop.DataSource = dt;
                        gvImportshop.PageIndex = e.NewPageIndex;
                        gvImportshop.DataBind();

                    }
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
