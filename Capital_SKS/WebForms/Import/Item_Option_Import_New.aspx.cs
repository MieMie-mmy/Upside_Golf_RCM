using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ORS_RCM.WebForms.Import
{
    public partial class Item_Option_Import_New : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOptionImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (uplOption.HasFile)
                {
                    String[] validFileTypes = { "csv" };
                    if (CheckFile(validFileTypes))
                    {
                        uplOption.SaveAs(Server.MapPath("~/Import_CSV/" + uplOption.FileName));
                        Response.Redirect("~/WebForms/Import/Item_Option_Import_Confirm.aspx?Option=" + uplOption.FileName,false);
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
                string ext = System.IO.Path.GetExtension(uplOption.PostedFile.FileName);
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