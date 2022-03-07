using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Jisha_Admin
{
    public partial class Jisha_Admin_Login : System.Web.UI.Page
    {
        Jisha_Admin_LoginBL jhbl;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            jhbl = new Jisha_Admin_LoginBL();
            string loginid = txtid.Text;
            string password = txtpassword.Text;
            DataTable dt = jhbl.logincheck(loginid);
            if (dt != null && dt.Rows.Count > 0) 
            {
                string id = dt.Rows[0]["User_ID"].ToString();
                string p = dt.Rows[0]["Password"].ToString();
                if (id == loginid && password == p)
                {
                    int ID = Convert.ToInt32(jhbl.check(loginid, password));
                    Response.Redirect("~/WebForms/Jisha_Admin/Jisha_Order_List.aspx");
                }
                else 
                {
                    GlobalUI.MessageBox("Please enter correct user name and password!");
                }
            
            }
        }
    }
}