using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Jisha
{
    public partial class Jisha_Search_Item : System.Web.UI.Page
    {
        Jisha_Delivery_Charge_BL jhbl;
        public string Item 
        {
            get 
            {
                if (Request.QueryString["Searchitem"] != null)

                    return Request.QueryString["Searchitem"].ToString();
                else
                    return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack) 
            {
                
                    jhbl = new Jisha_Delivery_Charge_BL();
                    DataTable dt = jhbl.Searchitem(Item);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        datalistshow.DataSource = dt;
                        datalistshow.DataBind();
                    }
                    else
                    {
                        datalistshow.DataSource = null;
                        datalistshow.DataBind();
                    }
            
            }
        }

        protected void datalistshow_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            Response.Redirect("~/WebForms/Jisha/Jisha_Item_Detail.aspx?Item_ID=" + ID);
        }

       
    }
}