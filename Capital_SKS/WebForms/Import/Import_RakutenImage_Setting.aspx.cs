using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Transactions;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Import
{
    public partial class Import_RakutenImage_Setting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (upl1.HasFile)
            {
                String filename = Path.GetFileName(upl1.PostedFile.FileName);
                if (check(filename) == true)
                {
                    upl1.SaveAs(Server.MapPath("~/Import_CSV/") + filename);//save file

                    String path = Server.MapPath("~/Import_CSV/") + filename;
                    DataTable dt = GlobalUI.CSVToTable(path);
                    String[] colName = { "商品番号", "フォルダ名", "画像名" };
                    if (CheckColumn(colName, dt))
                    {
                        dt.TableName = "test";
                        System.IO.StringWriter writer = new System.IO.StringWriter();
                        dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                        string result = writer.ToString();

                        var option = new TransactionOptions
                        {
                            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,   
                            Timeout=TimeSpan.MaxValue
                        };

                        TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,option);
                        using (scope)
                        {
                            Import_Item_BL iibl = new Import_Item_BL();
                            iibl.Import_RakutenSetting(result);
                            scope.Complete();
                        }
                    }
                    else
                    {
                        GlobalUI.MessageBox("File Format Wrong!");
                    }
                }
            }
        }

        protected Boolean CheckColumn(String[] colName, DataTable dt)
        {
            try
            {
                DataColumnCollection col = dt.Columns;
                for (int i = 0; i < colName.Length; i++)
                {
                    if (!col.Contains(colName[i]))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
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
    }
}