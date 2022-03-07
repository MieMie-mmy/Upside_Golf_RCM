using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;
using ORS_RCM_BL;
using System.Drawing;

namespace ORS_RCM.WebForms.Item
{
    public partial class Popup_Option : System.Web.UI.Page
    {

         Option_BL  optbl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
        {
            BindData();
        }
       }



        protected void BindData()
    {
        DataSet ds = new DataSet();
        DataTable FromTable = new DataTable();
        optbl= new  Option_BL();
        
        
        optbl= new  Option_BL();

        DataTable dt =  optbl.Search();
        DataList1.DataSource = dt;
        DataList1.DataBind();      

        }








    }
}

    

