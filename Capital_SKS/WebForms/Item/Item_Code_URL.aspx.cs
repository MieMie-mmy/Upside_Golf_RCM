using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;

namespace ORS_RCM.WebForms.Item
{
    public partial class Item_Code_URL : System.Web.UI.Page
    {
        Item_Master_BL imeBL;
        Item_Shop_BL isbl;
        public string Item_Code
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

        protected void Page_Load(object sender, EventArgs e)
        {
                BindShopName();
                SetItemCodeURL();
        }

        public void SetItemCodeURL()
        {
            imeBL = new Item_Master_BL();
            isbl = new Item_Shop_BL();
            //int ItemID = imeBL.SelectItemID(Item_Code);
            DataTable dt = isbl.SelectItemCodeURL(Item_Code);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    foreach (DataListItem li in dlShop.Items)
                    {
                        TextBox txtitemcode = li.FindControl("txtItem_Code") as TextBox;
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
                DataTable dt = shopBL.SelectAll();
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Columns.Add("Item_Code");
                    var col = dt.Columns["Item_Code"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["Item_Code"] = Item_Code;
                    }
                    dlShop.DataSource = dt;
                    dlShop.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            imeBL = new Item_Master_BL();
            isbl = new Item_Shop_BL();
            int ItemID = imeBL.SelectItemID(Item_Code);
            DataTable dt=new DataTable();
            dt.Columns.Add("Item_ID", typeof(int));
            dt.Columns.Add("Shop_ID", typeof(int));
            dt.Columns.Add("Item_Code_URL", typeof(string));
            foreach (DataListItem li in dlShop.Items)
            {
                TextBox txtitemcode = li.FindControl("txtItem_Code") as TextBox;
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
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack();window.close()", true);
        }
    }
}