/* 
Created By              :Aye Aye Mon
Created Date          : 04/07/2014
Updated By             :
Updated Date         :

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
using System.Data;
using ORS_RCM_Common;

namespace ORS_RCM
{
    public partial class Item_Choice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Item_Master_BL ItemMasterBL = new Item_Master_BL();

                    Item_Master_Entity ime = new Item_Master_Entity();

                    gvItem.DataSource = ItemMasterBL.SelectAll(ime);
                    gvItem.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRelatedItem = new DataTable();
                dtRelatedItem.Columns.Add(new DataColumn("Related_ItemID", typeof(Int32)));
                dtRelatedItem.Columns.Add(new DataColumn("Related_ItemCode", typeof(string)));

                foreach (GridViewRow row in gvItem.Rows)
                {
                    Label RelatedItemID = row.FindControl("lblID") as Label;
                    Label RelatedItemCode = row.FindControl("lblItemCode") as Label;
                    CheckBox chkBox = row.FindControl("ckbItem") as CheckBox;
                    if (chkBox != null)
                    {
                        if (chkBox.Checked)
                        {
                            dtRelatedItem.Rows.Add(Convert.ToInt32(RelatedItemID.Text), RelatedItemCode.Text);
                        }
                    }
                }

                Session["Related_Item_List"] = dtRelatedItem;

                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        /// <summary>
        /// To Search Item Information by Item Code
        /// </summary>
        /// <returns>DataTable contains Item Information</returns>
        public DataTable Search()
        {
            try
            {
                DataTable dt = new DataTable();
                Item_Master_BL ItemMasterBL = new Item_Master_BL();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    dt = ItemMasterBL.SearchItemCode(txtSearch.Text);
                    return dt;
                }
                else
                {
                    Item_Master_Entity ime = new Item_Master_Entity();
                    dt = ItemMasterBL.SelectAll(ime);
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

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            try
            {
                gvItem.DataSource = Search();
                gvItem.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvItem.DataSource = Search();
                gvItem.PageIndex = e.NewPageIndex;
                gvItem.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}