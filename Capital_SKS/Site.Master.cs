/* 
Created By              :
Created Date          :
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
using ORS_RCM_Common;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Web.Routing;
using System.Text;
using System.Net;

namespace ORS_RCM
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //to maintain Scroll position for long page on postback
            Page.MaintainScrollPositionOnPostBack = true;

            if (Context.Session != null)
            {
                if (Session.IsNewSession)
                {
                    if (Request.Cookies["user_Info"] == null)                                        //05052016 JMS updated
                    {
                        HttpCookie newSessionIdCookie = Request.Cookies["ASP.NET_SessionId"];
                        if (newSessionIdCookie != null)
                        {
                            string newSessionIdCookieValue = newSessionIdCookie.Value;
                            if (newSessionIdCookieValue != string.Empty)
                            {
                                // This means Session was timed Out and New Session was started
                                Response.Redirect("~/Login.aspx");
                            }
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] path = Request.Url.AbsolutePath.Split("/".ToCharArray());
                if (Request.Cookies["user_Info"] != null)
                {
                    HttpCookie myCookie = Request.Cookies["user_Info"];
                    String JMS_username = HttpUtility.UrlDecode(Request.Cookies["user_Info"]["username"]);
                    String JMS_userID = Request.Cookies["user_Info"]["userid"];
                    String loginID = Request.Cookies["user_Info"]["LoginID"];
                    String JMS_Admin = Request.Cookies["user_Info"]["IsAdmin"];
                    Session["User_Name"] = JMS_username;
                    Session["User_ID"] = JMS_userID;
                    Session["IsAdmin"] = JMS_Admin;
                }

                if (Session["User_ID"] != null)      //05052016 JMS updated
                {
                    if (Session["User_ID"] != null)
                    {
                        lblUser.Text = Session["User_Name"].ToString();
                        hdfUserID.Value = Session["User_ID"].ToString();
                        string admin = Session["IsAdmin"].ToString();
                        if (admin.Equals("1"))
                        {
                            HtmlGenericControl li = Page.Master.FindControl("liAdmin") as HtmlGenericControl;
                            li.Style.Add("display", "block");
                            HtmlGenericControl p1 = Page.Master.FindControl("p1") as HtmlGenericControl;
                            HtmlGenericControl p2 = Page.Master.FindControl("p2") as HtmlGenericControl;
                            p1.Style.Add("display", "none");
                            p2.Style.Add("display", "normal");
                        }
                    }
                }
                else Response.Redirect("~/Login.aspx");
               
                if (Request.Cookies["user_Info"] == null)            // 05052016 JMS updated
                {
                    if (path[path.Length - 1].ToString() != "AccessCheck.aspx")
                    {
                        if (path[path.Length - 1].ToString() != "Default.aspx" && path[path.Length - 1].ToString() != "Login.aspx"
                            && path[path.Length - 1].ToString() != "MallData_Import.aspx" && path[path.Length - 1].ToString() != "SKS_DB_Backup.aspx"
                            && path[path.Length - 1].ToString() != "System_ErrorLog_View.aspx" && path[path.Length - 1].ToString() != "index"
                            && path[path.Length - 1].ToString() != "CheckDifferentCategory.aspx" && path[path.Length - 1].ToString() != "CopyImage.aspx"
                            )
                        {
                            if (path[path.Length - 1].ToString() != "CustomErrorPage.aspx")
                            {
                                if (!Check_PageAccess(path[path.Length - 1].ToString()))
                                    Response.Redirect("~/AccessCheck.aspx");
                            }
                        }
                    }
                }
            }
        }

        private bool Check_PageAccess(string url)
        {
            bool result = false;
            int userID = BaseLib.Convert_Int(hdfUserID.Value);
            string pagecod = "001";//testing fixed code / modified later
            LogInBL login = new LogInBL();
            result = login.Check_PageAccess(userID, url, pagecod);
            if (result == false)
            {
                GlobalUI.MessageBox("No authorized!");
                return false;
            }
            return true;
        }

        protected void btnKeySearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/Item/Item_View.aspx?SearchKey=" + txtKeySearch.Value);
        }
    }
}
