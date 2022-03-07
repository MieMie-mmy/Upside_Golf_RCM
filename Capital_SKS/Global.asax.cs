﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace ORS_RCM
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapPageRoute("SKSTop", "admin/index", "~/WebForms/Item_Exhibition/Exhibition_Error.aspx");
            RouteTable.Routes.MapPageRoute("JishaTop", "Jisha/", "~/WebForms/Jisha/Jisha_Item_View.aspx");
            RouteTable.Routes.MapPageRoute("JishaAdminTop", "Jisha/admin/", "~/WebForms/Jisha_Admin/Jisha_Admin_Login.aspx");
        }
        
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}