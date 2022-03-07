/* 
Created By              : Kyaw Thet Paing
Created Date          : 01/07/2014
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
using System.IO;
using System.Collections;
using Microsoft.VisualBasic.FileIO;
using ORS_RCM_BL;

namespace ORS_RCM
{
    public partial class CSV_DataImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["FileName"] != null)
                    {
                        String path = Server.MapPath("~/Import_CSV/") + Request.QueryString["FileName"].ToString();
                        DataTable dt = GlobalUI.CSVToTable(path);
                        dt = GlobalUI.Remove_Doublecode(dt);
                        ViewState["Mall"] = dt;
                        int id = int.Parse(Request.QueryString["Mall"].ToString());
                        if (id == 1)
                        {
                            lblRak.Visible = true;
                            h1Rakuten.Visible = true;
                            divicon.Attributes.Add("class", "setDetailBox mallCate impirtAtt iconSet iconRakuten editBox");
                            String[] colName = { "ディレクトリID", "パス名" };
                            if (CheckColumn(colName, dt))
                            {
                                gvShow.DataSource = dt;
                                gvShow.DataBind();
                                Cache["CategoryImport"] = dt;
                            }
                            else
                            {
                                ShowMsg("File format wrong!");
                                btnUpdate.Visible = false;
                                btnBack.Visible = true;
                            }
                        }

                        else if (id == 2)
                        {
                            lblYah.Visible = true;
                            h1Yahoo.Visible = true;
                            divicon.Attributes.Add("class", "setDetailBox mallCate impirtAtt iconSet iconYahoo editBox");
                            String[] colName = { "プロダクトカテゴリ", "パス名" };
                            if (CheckColumn(colName, dt))
                            {
                                gvShow.DataSource = dt;
                                gvShow.DataBind();
                                Cache["CategoryImport"] = dt;
                            }
                            else                                 
                            {
                                ShowMsg("File format wrong!");
                                btnUpdate.Visible = false;
                                btnBack.Visible = true;
                            }
                        }

                        else if (id == 4)
                        {
                            lblWowma.Visible = true;
                            h1Wowma.Visible = true;
                            divicon.Attributes.Add("class", "setDetailBox mallCate impirtAtt iconSet iconPon editBox");
                            String[] colName = { "ジャンルID", "パス名" };
                            if (CheckColumn(colName, dt))
                            {
                                gvShow.DataSource = dt;
                                gvShow.DataBind();
                                Cache["CategoryImport"] = dt;
                            }
                            else
                            {
                                ShowMsg("File format wrong!");
                                btnUpdate.Visible = false;
                                btnBack.Visible = true;


                            }
                        }
                        else if (id == 7)
                        {
                            lblORS.Visible = true;
                            h1ORS.Visible = true;
                            divicon.Attributes.Add("class", "setDetailBox mallCate impirtAtt iconSet iconPon editBox");
                            String[] colName = { "ジャンルID", "パス名" };
                            if (CheckColumn(colName, dt))
                            {
                                gvShow.DataSource = dt;
                                gvShow.DataBind();
                                Cache["CategoryImport"] = dt;
                            }
                            else
                            {
                                ShowMsg("File format wrong!");
                                btnUpdate.Visible = false;
                                btnBack.Visible = true;


                            }
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

        protected DataTable changeTextDatatable(DataTable dt)
        {
            try
            {
                int columnNumber = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][columnNumber].ToString().Equals("u"))
                    { dt.Rows[i][columnNumber] = "更新"; }
                    else if (dt.Rows[i][columnNumber].ToString().Equals("d"))
                    { dt.Rows[i][columnNumber] = "削除"; }
                    else if (dt.Rows[i][columnNumber].ToString().Equals("n"))
                    { dt.Rows[i][columnNumber] = "新規"; }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }

        public String DataTableToXML(DataTable dt)
        {
            try
            {
                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();
                return result;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty ;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(Request.QueryString["Mall"] != null)
                {
                    int id = int.Parse(Request.QueryString["Mall"].ToString());
                    DataTable dt = (DataTable)ViewState["Mall"];

                    Mall_Category_BL mbl = new Mall_Category_BL();

                    dt.Columns.Add(new DataColumn("Mall_ID", typeof(Int32)));
                    foreach (DataRow row in dt.Rows)
                    {
                        row["Mall_ID"] = id;
                    }

                    if (dt.Columns.Contains("ディレクトリID"))
                    {
                        dt.Columns["ディレクトリID"].ColumnName = "Category_ID";
                    }
                    else if (dt.Columns.Contains("id"))
                    {
                        dt.Columns["id"].ColumnName = "Category_ID";
                    }
                    else if (dt.Columns.Contains("ジャンルID"))
                    {
                        dt.Columns["ジャンルID"].ColumnName = "Category_ID";
                    }
                    else if (dt.Columns.Contains("プロダクトカテゴリ"))
                    {
                        dt.Columns["プロダクトカテゴリ"].ColumnName = "Category_ID";
                    }

                    else if (dt.Columns.Contains("name"))
                    {
                        dt.Columns["name"].ColumnName = "Category_ID";
                    }

                    if (dt.Columns.Contains("パス名"))
                    {
                        dt.Columns["パス名"].ColumnName = "Category_Path";
                    }
                    else if (dt.Columns.Contains("path_name"))
                    {
                        dt.Columns["path_name"].ColumnName = "Category_Path";
                    }

                    String xml = DataTableToXML(dt);
                    //mbl.InsertCSV(dt,id);
                    mbl.InsertMall_Category_XML(xml);
                    Cache.Remove("CategoryImport");
                   // Response.Write("<script>window.location.href='~/ WebForms/Category/Mall_Category.aspx?Mall=' + id;</script>");
                    //Response.Redirect("~/WebForms/Category/Mall_Category.aspx?Mall=" + id, false);
                    //ShowMsg("Save Successful!");

                    ViewState["UrlReferrer"] = "Mall_Category.aspx?Mall=" + id;
                    string result = "Save Successful!";
                    if (result == "Save Successful!")
                    {
                        object referrer = ViewState["UrlReferrer"];
                        string url = "/WebForms/Category/" + (string)referrer;
                        string script = "window.onload = function(){ alert('";
                        script += result;
                        script += "');";
                        script += "window.location.href = '";
                        script += url;
                        script += "'; }";
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                    }

                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }          
        }

        protected void ShowMsg(String str)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert" + UniqueID, "alert('" + str + "');", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }   
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
               if(Request.QueryString["Mall"] != null)
                {
                    int id = int.Parse(Request.QueryString["Mall"].ToString());
                    Response.Redirect("~/WebForms/Category/Mall_Category.aspx?Mall=" + id, false);

               }
        }

        protected void gvShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dtbind = new DataTable();
            if (Cache["CategoryImport"] != null)
            {
                dtbind = Cache["CategoryImport"] as DataTable;
            }
            gvShow.DataSource = dtbind;
            gvShow.PageIndex = e.NewPageIndex;
            gvShow.DataBind();
        }
    }
}