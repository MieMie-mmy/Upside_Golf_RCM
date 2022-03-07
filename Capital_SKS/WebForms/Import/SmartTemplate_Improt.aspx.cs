using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ORS_RCM.WebForms.Import
{
    public partial class SmartTemplate_Improt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected Boolean CheckFile(String[] validFileTypes)
        {
            try
            {
                string ext = System.IO.Path.GetExtension(uplSmartTemplate.PostedFile.FileName);
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

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (uplSmartTemplate.HasFile)
                {
                    String[] validFileTypes = { "csv" };
                    if (CheckFile(validFileTypes))
                    {
                        uplSmartTemplate.SaveAs(Server.MapPath("~/Import_CSV/" + uplSmartTemplate.FileName));
                        Response.Redirect("~/WebForms/Import/SmartTemplate_Import_Confirm.aspx?SmartTemplate=" + uplSmartTemplate.FileName, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}