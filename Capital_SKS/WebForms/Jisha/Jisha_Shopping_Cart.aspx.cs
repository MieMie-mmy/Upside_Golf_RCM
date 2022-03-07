using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ORS_RCM.WebForms.Jisha
{
    public partial class Jisha_Item_OrderDetail : System.Web.UI.Page
    {
        public DataTable AddToCart
        {
            get
            {
                if (Session["AddToCart"] != null)
                {
                    DataTable dt = (DataTable)Session["AddToCart"];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }
        decimal totalQuantity = 0M;
        decimal totalAmount = 0M;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AddToCart != null)
                {
                    btnOrder.Visible = true;

                    gvCart.DataSource = AddToCart;
                    gvCart.DataBind();
                }
                else
                {
                    btnOrder.Visible = false;
                }
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/Jisha/Jisha_Item_View.aspx");
        }

        protected void gvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            AddToCart.Rows.RemoveAt(e.RowIndex);
            AddToCart.AcceptChanges();
            gvCart.DataSource = AddToCart;
            gvCart.DataBind();

        }

        protected void gvCart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblQuantity = (Label)e.Row.FindControl("lblQuantity");
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");

                decimal Qty = Decimal.Parse(lblQuantity.Text);
                decimal Amt = Decimal.Parse(lblAmount.Text);

                totalQuantity += Qty;
                totalAmount += Amt;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalQuantity = (Label)e.Row.FindControl("lblTotalQuantity");
                Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");

                lblTotalQuantity.Text = totalQuantity.ToString();
                lblTotalAmount.Text = totalAmount.ToString();

                //lblAveragePrice.Text = (totalPrice / totalItems).ToString("F");
            }
             
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/Jisha/Jisha_Order.aspx");
        }
    }
}