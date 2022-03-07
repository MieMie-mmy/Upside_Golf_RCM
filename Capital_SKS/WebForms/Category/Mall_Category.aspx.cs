/* 
Created By              : Kay Thi Aung/Kyaw Thet/Ei Ei Phyo
Created Date          : 30/06/2014
Updated By             :
Updated Date         :

 Tables using: Mall_Category
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
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using ORS_RCM_BL;
using ORS_RCM_Common;
using System.Configuration;
using System.Text;
using System.Net;
using System.Collections;

namespace ORS_RCM
{
    public partial class Mall_Category : System.Web.UI.Page
    {
        
        Mall_Category_BL mbl;
        String ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["sort"] = "Original";
                    if ((Request.QueryString["Mall"] != null))
                    {
                        int id = int.Parse(Request.QueryString["Mall"].ToString());

                        ViewState["currentPage"] = 0;
                        mbl = new Mall_Category_BL();

                        if (id == 1)
                        {
                            Label1.Visible = true;
                            lblRk.Visible = true;
                        }

                        else if (id == 2)
                        {
                            Label2.Visible = true;
                            lblYh.Visible = true;
                        }

                        //else if (id == 3)
                        //{
                        //    Label3.Visible = true;
                        //    lblPom.Visible = true;
                        //}

                        else if (id == 4)
                        {
                            Label4.Visible = true;
                            lblWowma.Visible = true;
                        }
                        else if (id == 7)
                        {
                            Label5.Visible = true;
                            lblTennis.Visible = true;
                        }

                        DataTable dt = mbl.SelectByMallID(id, gvmall.PageIndex + 1, gvmall.PageSize, 1,null,null,null);
                        //DataTable dt = mbl.SelectByMallID(id, gvmall.PageIndex+1, gvmall.PageSize, 1);
                        //ViewState["dt"] = dt;

                        gvmall.DataSource = dt;
                        gvmall.DataBind();

                        DataTable dtRowCount = mbl.SelectByMallID(id, gvmall.PageIndex, gvmall.PageSize, 2,null,null,null);
                        gp.CalculatePaging(int.Parse(dtRowCount.Rows[0]["Count"].ToString()), gvmall.PageSize, 1);
                    }
                }
                    else
                    {
                        String ctrl = getPostBackControlName();
                        if (ctrl.Contains("lnkPaging"))
                        {
                            int id = int.Parse(Request.QueryString["Mall"].ToString());
                            mbl = new Mall_Category_BL();
                            //if (ViewState["dt"] != null)
                            //{
                            //DataTable dt = ViewState["dt"] as DataTable;
                            gp.LinkButtonClick(ctrl, gvmall.PageSize);
                            Label lbl = gp.FindControl("lblCurrent") as Label;
                            int index = Convert.ToInt32(lbl.Text);
                            gvmall.PageIndex = index - 1;
                            getdata(id, gvmall.PageIndex + 1, gvmall.PageSize);
                            ////DataTable dt = mbl.SelectByMallID(id, gvmall.PageIndex+1, gvmall.PageSize, 1,null,null,null);
                            ////gvmall.DataSource = dt;
                            ////gvmall.DataBind();
                            // }
                        }
                    }
                }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public string getPostBackControlName()
        {
            try
            {
                Control control = null;
                //first we will check the "__EVENTTARGET" because if post back made by       the controls
                //which used "_doPostBack" function also available in Request.Form collection.
                string ctrlname = Page.Request.Params["__EVENTTARGET"];
                if (ctrlname != null && ctrlname != String.Empty)
                {
                    control = Page.FindControl(ctrlname);
                }
                // if __EVENTTARGET is null, the control is a button type and we need to
                // iterate over the form collection to find it
                else
                {
                    string ctrlStr = String.Empty;
                    Control c = null;
                    foreach (string ctl in Page.Request.Form)
                    {
                        //handle ImageButton they having an additional "quasi-property" in their Id which identifies
                        //mouse x and y coordinates
                        if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                        {
                            ctrlStr = ctl.Substring(0, ctl.Length - 2);
                            c = Page.FindControl(ctrlStr);
                        }
                        else
                        {
                            c = Page.FindControl(ctl);
                        }
                        if (c is System.Web.UI.WebControls.Button ||
                                 c is System.Web.UI.WebControls.ImageButton)
                        {
                            control = c;
                            break;
                        }
                    }
                }

                if (control != null)
                    return control.ID;
                else return null;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
        }

        protected void getdata(int id,int pindex,int psize) 
        {
            mbl = new Mall_Category_BL();
            ArrayList arrList = new ArrayList();

            string str = null;
            string[] strArr = null;
            str = txtcname.Text;

            char[] splitchar = { ' ', '　' };
            strArr = str.Split(splitchar);

            string category1 = string.Empty;
            string category2 = string.Empty;
            string category3 = string.Empty;

            if (strArr.Length > 0)
            {
                category1 = strArr[0].ToString(); ;
            }
            if (strArr.Length > 1)
            {
                category2 = strArr[1].ToString();
            }
            if (strArr.Length > 2)
            {
                category3 = strArr[2].ToString();
            }

            DataTable dt = new DataTable();
            if ((!String.IsNullOrWhiteSpace(txtcname.Text)))
            {
                dt = mbl.SelectByMallID(id, pindex, psize, 3, category1, category2, category3);
                gvmall.DataSource = dt;
                gvmall.DataBind();
                //gp.CalculatePaging(dt.Rows.Count, gvmall.PageSize, 1);
            }
            else
            {
              //  int mid = Int32.Parse(id);
                //dt = mbl.SelectByMallID(mid);
                dt = mbl.SelectByMallID(id, gvmall.PageIndex + 1, gvmall.PageSize, 1, null, null, null);
                gvmall.DataSource = dt;
                gvmall.DataBind();
                DataTable dtRowCount = mbl.SelectByMallID(id, gvmall.PageIndex, gvmall.PageSize, 2, null, null, null);
                //gp.CalculatePaging(Convert.ToInt32(dtRowCount.Rows[0]["Count"]), gvmall.PageSize, 1);
            }
        
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
             string id = Request.QueryString["Mall"].ToString();

            mbl = new Mall_Category_BL();
            ArrayList arrList = new ArrayList();

            string str = null;
            string[] strArr = null;
            str = txtcname.Text;

            char[] splitchar = { ' ', '　' };                
            strArr = str.Split(splitchar);

            string category1=string.Empty;
            string category2 = string.Empty;
            string category3= string.Empty;

            if (strArr.Length>0)
            {
                 category1 = strArr[0].ToString(); ;
            }
             if (strArr.Length>1)
            {
               category2=strArr[1].ToString();
             }
             if (strArr.Length >2)
             {
                  category3 = strArr[2].ToString();
             }


             DataTable dt = new DataTable();
            if ((!String.IsNullOrWhiteSpace(txtcname.Text)))
            {
                dt = mbl.Search(id, category1,category2,category3);
                gvmall.DataSource = dt;
                gvmall.DataBind();
                gp.CalculatePaging(dt.Rows.Count, gvmall.PageSize, 1);
            }
            else
            {
                int mid = Int32.Parse(id);
                //dt = mbl.SelectByMallID(mid);
                dt = mbl.SelectByMallID(mid, gvmall.PageIndex+1, gvmall.PageSize, 1,null,null,null);
                gvmall.DataSource = dt;
                gvmall.DataBind();
                DataTable dtRowCount = mbl.SelectByMallID(mid, gvmall.PageIndex, gvmall.PageSize, 2,null,null,null);
                gp.CalculatePaging(Convert.ToInt32(dtRowCount.Rows[0]["Count"]), gvmall.PageSize, 1);
            }

            //gvmall.DataSource = dt;
            //gvmall.DataBind();
            //gp.CalculatePaging(dt.Rows.Count, gvmall.PageSize, 1);
            //ViewState["dt"] = dt;
        }

        protected void gvmall_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                string id = Request.QueryString["Mall"].ToString();
                mbl = new Mall_Category_BL();
                DataTable dtmalldata = new DataTable();

             


                ArrayList arrList = new ArrayList();

                string str = null;
                string[] strArr = null; int count = 0;
                str = txtcname.Text; char[] splitchar ={ ' ', '　' };   
                strArr = str.Split(splitchar);

                string a = string.Empty;
                string b = string.Empty;
                string c = string.Empty;

                if (strArr.Length > 0)
                {
                    a = strArr[0].ToString(); ;
                }
                if (strArr.Length > 1)
                {
                    b = strArr[1].ToString();
                }
                if (strArr.Length > 2)
                {
                    c = strArr[2].ToString();
                }
            
            
               
                if ((!String.IsNullOrWhiteSpace(txtcname.Text)) && (e.NewPageIndex > -1) && (e.NewPageIndex > 0))
                {
                    dtmalldata = mbl.Search(id, a, b, c);
                    gvmall.DataSource = dtmalldata;
                    gvmall.PageIndex = e.NewPageIndex;
                    gvmall.DataBind();
                  
                }
                else
                {
                    if ((e.NewPageIndex > -1) && (e.NewPageIndex > 0))
                    {
                        dtmalldata = mbl.Search(null, null, null, null);
                        gvmall.DataSource = dtmalldata;
                        gvmall.PageIndex = e.NewPageIndex;
                        gvmall.DataBind();
                      
                    }

                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Request.QueryString["Mall"] != null))
                {
                    if (upl1.HasFile)
                    {
                        String[] validFileTypes = { "csv" };
                        if (CheckFile(validFileTypes))
                        {
                            int id = int.Parse(Request.QueryString["Mall"].ToString());
                            upl1.SaveAs(Server.MapPath("~/Import_CSV/" + upl1.FileName));
                            Response.Redirect("~/WebForms/Import/MallCategory_Import.aspx?FileName=" + upl1.FileName + "&Mall=" + id,false);
                        }
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
                string ext = System.IO.Path.GetExtension(upl1.PostedFile.FileName);
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Request.QueryString["Mall"] != null))
                {

                    int id = int.Parse(Request.QueryString["Mall"].ToString());

                    mbl = new Mall_Category_BL();
                    DataTable dtExport = new DataTable();
                   
                    dtExport = mbl.Search(null, null, null, null);
                     dtExport = mbl.Mall_CategoryExport(id);
                    

                    //Generate CSV filel
                    if ((dtExport != null && dtExport.Rows.Count > 0) && (id == 1))
                    {
                        // Change encoding to Shift-JIS

                        using (StreamWriter writer = new StreamWriter(Server.MapPath(ExportCSVPath + "MallCategory_RakutenExport.csv"), false, Encoding.UTF8))
                        {
                            
                            lnkdownload.Text = "MallCategory_RakutenExport.csv";

                            WriteDataTable(dtExport, writer, true);
                        }
                    }

                    if ((dtExport != null && dtExport.Rows.Count > 0) && (id == 2))
                    {
                        using (StreamWriter writer = new StreamWriter(Server.MapPath(ExportCSVPath + "MallCategory_YahooExport.csv"), false, Encoding.UTF8))
                        {

                            lnkdownload.Text = "MallCategory_YahooExport.csv";

                            WriteDataTable(dtExport, writer, true);
                        }
                    }

                    if ((dtExport != null && dtExport.Rows.Count > 0) && (id == 3))
                    {
                        using (StreamWriter writer = new StreamWriter(Server.MapPath(ExportCSVPath + "MallCategory_PonpareExport.csv"), false, Encoding.UTF8))
                        {

                            lnkdownload.Text = "MallCategory_PonpareExport.csv";

                            WriteDataTable(dtExport, writer, true);
                        }
                    }

                    if ((dtExport != null && dtExport.Rows.Count > 0) && (id == 4))
                    {
                        using (StreamWriter writer = new StreamWriter(Server.MapPath(ExportCSVPath + "MallCategory_WowmaExport.csv"), false, Encoding.UTF8))
                        {

                            lnkdownload.Text = "MallCategory_WowmaExport.csv";

                            WriteDataTable(dtExport, writer, true);
                        }
                    }

                    if ((dtExport != null && dtExport.Rows.Count > 0) && (id == 7))
                    {
                        using (StreamWriter writer = new StreamWriter(Server.MapPath(ExportCSVPath + "MallCategory_ORSExport.csv"), false, Encoding.UTF8))
                        {

                            lnkdownload.Text = "MallCategory_ORSExport.csv";

                            WriteDataTable(dtExport, writer, true);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            try
            {
                if (includeHeaders)
                {
                    List<string> headerValues = new List<string>();

                    foreach (DataColumn column in sourceTable.Columns)
                    {
                        headerValues.Add(QuoteValue(column.ColumnName.ToLower()));
                    }
                    StringBuilder builder = new StringBuilder();
                    writer.WriteLine(String.Join(",", headerValues.ToArray()));
                }

                string[] items = null;
                foreach (DataRow row in sourceTable.Rows)
                {
                    items = row.ItemArray.Select(o => QuoteValue(o.ToString())).ToArray();
                    writer.WriteLine(String.Join(",", items));
                }

                writer.Flush();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        private string QuoteValue(string value)
        {
            try
            {
                return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
        }
             
        protected void gvmall_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                foreach (TableCell tc in e.Row.Cells)
                {
                    tc.Attributes["style"] = "border-color: #c3cecc";
                }
                // when mouse is over the row, save original color to new attribute, and change it to highlight color
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#CEF0EC'");

                // when mouse leaves the row, change the bg color to its original value  
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void lnkdownload_Click(object sender, EventArgs e)
        {
        //    try
        //    {
                Download(ExportCSVPath + lnkdownload.Text);
            ////}
            //catch (Exception ex)
            //{
            //    Session["Exception"] = ex.ToString();
            //    Response.Redirect("~/CustomErrorPage.aspx?");
            //}

        }

        protected void Download(string filecheck)
        {
            //try
            //{
                if (File.Exists(Server.MapPath(filecheck)))
                {
                    string filename = lnkdownload.Text;
                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    response.ContentType = "application/octet-stream";
                    byte[] data = req.DownloadData(Server.MapPath(filecheck));
                    response.BinaryWrite(data);
                    response.End();
                }
                else
                {
                    GlobalUI.MessageBox("File doesn't exist!");
                }

            }
            //catch (Exception ex) 
            //{
            //    Session["Exception"] = ex.ToString();
            //    Response.Redirect("~/CustomErrorPage.aspx?");
            //}
        }
    }

