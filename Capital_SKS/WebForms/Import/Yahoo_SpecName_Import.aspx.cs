/* 
Created By              : Kyaw Thet Paing
Created Date          : 
Updated By             :Aye Mon,Kaythi Aung
Updated Date         :
 Tables using           : Yahoo_SpecName
                                     -
                                     -
                                  
 *                                  
 * Storedprocedure using:
 *            
 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using ORS_RCM_BL;
using System.Collections;

namespace ORS_RCM.WebForms.Import
{
    public partial class Yahoo_SpecName_Import : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

     
        protected void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = System.IO.Path.GetFileName(uplYahooSpec.PostedFile.FileName);
                if (check(filename))
                {
                    String path = Server.MapPath("~/Import_CSV/") + Path.GetFileName(filename);
                    uplYahooSpec.SaveAs(Server.MapPath("~/Import_CSV/") + filename);

                    DataTable dt = GlobalUI.CSVToTable(path);
                    dt = GlobalUI.Remove_Doublecode(dt);
                    DataTable dtResult = new DataTable();
                    dtResult.Columns.Add("ヤフーカテゴリID", typeof(String));
                    dtResult.Columns.Add("ヤフーカテゴリ名", typeof(String));
                    dtResult.Columns.Add("スペックID", typeof(String));
                    dtResult.Columns.Add("スペック名", typeof(String));
                    dtResult.Columns.Add("スペック値ID", typeof(String));
                    dtResult.Columns.Add("スペック値名", typeof(String));
                    //int j = 0;
                    //do
                    //{
                    //    String spec_id = dt.Rows[0]["スペックID"].ToString();
                    //    String id = dt.Rows[0]["ヤフーカテゴリID"].ToString();
                    //    if (String.IsNullOrWhiteSpace(id) || String.IsNullOrWhiteSpace(spec_id))
                    //    {
                    //        dt.Rows[0].Delete();
                    //        goto Skip;
                    //    }

                    //    if (!String.IsNullOrWhiteSpace(spec_id))
                    //    {
                    //        DataTable dtSpecID = dt.Select("スペックID='" + spec_id + "' AND ヤフーカテゴリID='" + id + "'").CopyToDataTable();
                    //        dtResult.Rows.Add();
                    //        dtResult.Rows[j]["ヤフーカテゴリID"] = dtSpecID.Rows[0]["ヤフーカテゴリID"].ToString();
                    //        dtResult.Rows[j]["ヤフーカテゴリ名"] = dtSpecID.Rows[0]["ヤフーカテゴリ名"].ToString();
                    //        dtResult.Rows[j]["スペックID"] = dtSpecID.Rows[0]["スペックID"].ToString();
                    //        dtResult.Rows[j]["スペック名"] = dtSpecID.Rows[0]["スペック名"].ToString();
                    //        dtResult.Rows[j]["スペック値ID"] = dtSpecID.Rows[0]["スペック値ID"].ToString();
                    //        dtResult.Rows[j]["スペック値名"] = dtSpecID.Rows[0]["スペック値名"].ToString();

                    //        for (int i = 1; i < dtSpecID.Rows.Count; i++)
                    //        {
                    //            dtResult.Rows[j]["スペック値ID"] = dtResult.Rows[j]["スペック値ID"] + "," + dtSpecID.Rows[i]["スペック値ID"].ToString();
                    //            dtResult.Rows[j]["スペック値名"] = dtResult.Rows[j]["スペック値名"] + "," + dtSpecID.Rows[i]["スペック値名"].ToString();
                    //        }

                    //        j++;
                    //    }

                    //    for (int i = 0; i < dt.Rows.Count; )
                    //    {
                    //        if (dt.Rows[i]["スペックID"].Equals(spec_id) && dt.Rows[i]["ヤフーカテゴリID"].Equals(id))
                    //            dt.Rows.RemoveAt(i);
                    //        else i++;
                    //    }

                    //Skip: ;
                    //} while (dt.Rows.Count != 0);
                    Session["Yahoo_Spec"] = dt;
                    gvYahooSpec.DataSource = dt;
                    gvYahooSpec.DataBind();
                }
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
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (Session["Yahoo_Spec"] != null)
                    dt = Session["Yahoo_Spec"] as DataTable;
                String xml = dtToXml(dt);

                Yahoo_SpecName_BL ysbl = new Yahoo_SpecName_BL();
                if (ysbl.InsertUpdateYahooSpec(xml))
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Save Successfully')", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Save Fail')", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public String dtToXml(DataTable dt)
        {
            try
            {
                dt.TableName = "Yahoo_Spec";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string xml = writer.ToString();
                return xml;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }            
        }

        protected void gvYahooSpec_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt=new DataTable();
             if (Session["Yahoo_Spec"] != null)
                 dt = Session["Yahoo_Spec"] as DataTable;
            gvYahooSpec.DataSource=dt;
            gvYahooSpec.PageIndex=e.NewPageIndex;
            gvYahooSpec.DataBind();
        }
    }
}