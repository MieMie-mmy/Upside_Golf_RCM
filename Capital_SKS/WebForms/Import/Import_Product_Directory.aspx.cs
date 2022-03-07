using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

namespace Capital_SKS.WebForms.Import
{
    public partial class Import_Product_Directory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {               
                if (fileupPD.HasFile)
                {
                    String[] validFileTypes = {"xlsx"};
                    if (CheckFile(validFileTypes))
                    {
                        fileupPD.SaveAs(Server.MapPath("~/Import_CSV/" + fileupPD.FileName));
                        Response.Redirect("~/WebForms/Import/Product_Directory_Confirm.aspx?Product_Directory="+fileupPD.FileName,false);
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
                string ext = System.IO.Path.GetExtension(fileupPD.PostedFile.FileName);
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