using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;

namespace ORS_RCM.WebForms.Jisha
{
    public partial class Jisha_Order_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Jisha_Order_BL jobl = new Jisha_Order_BL();
                DataTable dt = jobl.SelectAll();
                gvOrderList.DataSource = dt;
                gvOrderList.DataBind();

            //    gp.TotalRecord = dt.Rows.Count;
            //    gp.OnePageRecord = gvOrderList.PageSize;


            //    int index1 = 0;
            //    gp.sendIndexToThePage += delegate(int index)
            //    {
            //        index1 = index;
            //    };
            //    gvOrderList.PageIndex = index1;
            //    gvOrderList.DataSource = dt;
            //    gvOrderList.DataBind();
            //}
            //else
            //{
            //    Jisha_Order_BL jobl = new Jisha_Order_BL();
            //    DataTable dt = jobl.SelectAll();

            //    gp.TotalRecord = dt.Rows.Count;
            //    gp.OnePageRecord = gvOrderList.PageSize;

            //    gp.sendIndexToThePage += delegate(int index)
            //    {
            //        gvOrderList.PageSize = gp.OnePageRecord;
            //        gvOrderList.PageIndex = Convert.ToInt32(index);
            //        gvOrderList.DataSource = dt;
            //        ViewState["ItemAll"] = dt;
            //        gvOrderList.DataBind();
            //    };
            }
        }

        protected void btnDetailClick(object sender,EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            int index = row.RowIndex;

            Label lbl = gvOrderList.Rows[index].FindControl("lblOrderID") as Label;

            Response.Redirect("~/WebForms/Jisha_Admin/Jisha_Order_Detail.aspx?OrderID=" + lbl.Text);
        }
    }
}