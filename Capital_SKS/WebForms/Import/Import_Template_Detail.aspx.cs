using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

namespace ORS_RCM.WebForms.Import
{
    public partial class Import_Template_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
            
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (FU_Template_Detail.HasFile)
                {
                    string FileName = Path.GetFileName(FU_Template_Detail.PostedFile.FileName);
                    string Extension = Path.GetExtension(FU_Template_Detail.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["ImportPath"];

                    string FilePath = FolderPath + FileName;
                    FU_Template_Detail.SaveAs(FilePath);
                    Response.Redirect("~/WebForms/Import/SmartTemplate_Import_Confirm.aspx?FilePath=" + FilePath + "&Extension=" + Extension, false);
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}