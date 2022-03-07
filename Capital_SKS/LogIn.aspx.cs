/* 
Created By              : Kay Thi Aung
Created Date          : 28/07/2014
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
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;
using ORS_RCM_Common;

namespace ORS_RCM
{
    public partial class LogIn : System.Web.UI.Page
    {
        LogInBL LogBL;
        protected void Page_Load(object sender, EventArgs e)
        {
            username.Focus();
           

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            LogBL = new LogInBL();
            String loginId = username.Value;
            String pass = password.Value;
            //User_entity userInfo = new User_entity();
            //userInfo = LogBL.SelectPassword(loginId);
            //string passwords = GlobalUI.DecryptPassword(userInfo.Password);
            //if (passwords == pass)
            //{
                DataTable dt = LogBL.logincheck(loginId);

                if (dt.Rows.Count > 0 && dt != null)
                {
                    int status = (int)dt.Rows[0]["Status"];
                    if (status == 1)
                    {
                        string p = dt.Rows[0]["Password"].ToString();
                        string name = dt.Rows[0]["Login_ID"].ToString();
                        string isAdmin = dt.Rows[0]["IsAdmin"].ToString();
                        if (p == pass && name == loginId)
                        {
                            int ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                            Session["User_ID"] = ID;
                            Session["User_Name"] = dt.Rows[0]["User_Name"].ToString();
                            Session["IsAdmin"] = isAdmin;

                            Response.Cookies["userInfo"]["username"] = name;
                            //Response.Cookies["userInfo"]["lastVisit"] = DateTime.Now.ToString();
                            Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(1);

                            HttpCookie aCookie = new HttpCookie("userInfo");
                            aCookie.Values["username"] = name;
                            //aCookie.Values["lastVisit"] = DateTime.Now.ToString();
                            aCookie.Expires = DateTime.Now.AddDays(1);
                            Response.Cookies.Add(aCookie);

                            Response.RedirectToRoute("SKSTop"); //url routing , check at Global.asax.cs
                        }
                        else
                        {
                            GlobalUI.MessageBox("Please enter correct user name and password!");
                        }
                    }
                    else { GlobalUI.MessageBox("Invalid User!"); }
                }
                else
                {
                    GlobalUI.MessageBox("Please enter correct user name and password!");
                }

            //}
            //else
            //{
            //    GlobalUI.MessageBox("Please enter correct user name and password!");
            //}
        }
      
       
    }
}