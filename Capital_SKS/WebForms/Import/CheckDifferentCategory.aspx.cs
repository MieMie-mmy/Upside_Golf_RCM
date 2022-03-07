using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Import
{
    public partial class CheckDifferentCategory : System.Web.UI.Page
    {
        Import_Item_BL import = new Import_Item_BL();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuCategory.HasFile)//check item master file select
                {
                    String filename = Path.GetFileName(fuCategory.PostedFile.FileName);

                    if (check(filename) == true)
                    {
                        fuCategory.SaveAs(Server.MapPath("~/Import_CSV/") + filename);//save file

                        String LocalPath = Server.MapPath("~/Import_CSV/") + filename;//file path

                        DataTable dt = GlobalUI.CSVToTable(LocalPath);//read csv file and return to datatable
                        dt.Columns["商品管理番号（商品URL）"].ColumnName = "Item_Code";
                        dt.AcceptChanges();
                        string xml = DataTableToXml(dt);

                        import.SportsPlaza_Rakuten_Item_Category_Import_Xml(xml);

                        GlobalUI.MessageBox("Save Successful " + filename);

                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = import.Check_SportsPlaza_Rakuten_Item_Category(rdoList.SelectedValue);
                gvCategory.DataSource = dt;
                gvCategory.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnTruncate_Click(object sender, EventArgs e)
        {
            try
            {
                import.Delete_SportsPlaza_Rakuten_Item_Category();
                GlobalUI.MessageBox("Delete Successful !");
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        private bool check(String str)
        {
            try
            {
                if (str.Contains(".csv"))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static String DataTableToXml(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            return result;
        }

        protected void rdoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoList.SelectedValue == "2") //ImportTable
            {
                btnTruncate.Visible = true;
                fuCategory.Enabled = true;
                btnImport.Enabled = true;
            }
            else //BatchCheckTable
            {
                btnTruncate.Visible = false;
                fuCategory.Enabled = false;
                btnImport.Enabled = false;
            }
        }

    }
}