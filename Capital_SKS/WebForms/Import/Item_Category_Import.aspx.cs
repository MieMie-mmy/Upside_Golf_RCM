using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ORS_RCM.WebForms.Import
{
    public partial class Item_Category_Import_New : System.Web.UI.Page
    {
        public int Userid
        {
            get
            {
                if (Session["User_ID"] != null)

                    return Int32.Parse(Session["User_ID"].ToString());
                else
                    return 0;
            }
        }     

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCategoryImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (uplCategory.HasFile)
                {
                    String[] validFileTypes = { "csv" };
                    if (CheckFile(validFileTypes))
                    {
                        uplCategory.SaveAs(Server.MapPath("~/Import_CSV/" + uplCategory.FileName));
                        Response.Redirect("~/WebForms/Import/Item_Category_Import_Confirm.aspx?Category=" + uplCategory.FileName,false);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected Boolean CheckFile(String[] validFileTypes)
        {
            try
            {
                string ext = System.IO.Path.GetExtension(uplCategory.PostedFile.FileName);
                bool isValidFile = false;
                for (int i = 0; i < validFileTypes.Length; i++)
                {
                    if (ext == "." + validFileTypes[i])
                    {
                        isValidFile = true;
                        break;
                    }
                }
                return isValidFile;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }
    }
}