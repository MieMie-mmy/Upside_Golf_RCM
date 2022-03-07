/* 
Created By              :Aye Aye Mon
Created Date          : 04/07/2014
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

namespace ORS_RCM
{
    public partial class Item_Image_View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["ImageName"] != null)
                    {
                        String image = Request.QueryString["ImageName"].ToString();
                        Image.ImageUrl = "~/Item_Image/" + image;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        /// <summary>
        /// Get Image name from url
        /// </summary>
        /// <returns>string of image name</returns>
        public string GetImageURL()
        {
            try
            {
                string imagename = Request.QueryString["ImageName"] as string;
                return "~/Item_Image/" + imagename;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
        }

    }
}