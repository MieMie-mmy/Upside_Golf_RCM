using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Upside_Golf_RCM_BL;

namespace Upside_Golf_RCM.WebForms.Jisha
{
    public partial class SideMenu : System.Web.UI.UserControl
    {
        Category_BL Category = new Category_BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Category.SelectForTreeview(1);
            DataList.DataSource = Category.SelectForTreeview(1);
            DataList.DataBind();
        }

        protected void DataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            //Label ID = (Label)e.Item.FindControl("lblID");
            Response.Redirect("~/WebForms/Jisha/Jisha_Item_View.aspx?Category_No=" + e.CommandArgument);
        }
    }
}