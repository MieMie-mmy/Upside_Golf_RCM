using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ORS_RCM.WebForms.Jisha
{
    public partial class SearchMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            //if (!String.IsNullOrWhiteSpace(txtsearch.Text))
            //{
                string searchitem = txtsearch.Text.Trim();
                Response.Redirect("~/WebForms/Jisha/Jisha_Search_Item.aspx?Searchitem="+searchitem);
          //  }
        }
        protected void imgcart_Click(object sender, EventArgs e) 
        {
            Response.Redirect("~/WebForms/Jisha/Jisha_Shopping_Cart.aspx" );
        }
    }
}