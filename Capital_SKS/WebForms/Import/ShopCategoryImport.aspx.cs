/* 
Created By              : Kyaw Thet Paing
Created Date          : 01/07/2014
Updated By             :
Updated Date         :

 Tables using: Category
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
    public partial class ShopCategoryImport : System.Web.UI.Page
    {
        Category_BL cbl;
        String desc = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["FileName"] != null)
                {
                    String path = Server.MapPath("~/Import_CSV/") + Request.QueryString["FileName"].ToString();
                    DataTable dt = GlobalUI.CSVToTable(path);
                    dt = GlobalUI.Remove_Doublecode(dt);
                    String[] colName = { "コントロールカラム", "カテゴリID", "パス名", "親カテゴリID" };
                    if (CheckColumn(colName, dt))
                    {
                        dt = changeTextDatatable(dt);
                        gvShow.DataSource = dt;
                        gvShow.DataBind();
                    }
                    else
                    {
                        ShowMsg("File format wrong!");
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Item_Category_BL icbl = new Item_Category_BL();
                DataTable dt = gvShow.DataSource as DataTable;
               
                ShopCategoryBL scbl = new ShopCategoryBL();
                //for insert description hierarchy
                DataColumn newColumns = new DataColumn("商品カテゴリ名", typeof(System.String));
                newColumns.DefaultValue = DBNull.Value;
                dt.Columns.Add(newColumns);

                #region
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    String ccode = dt.Rows[i]["カテゴリID"].ToString();
                //    DataTable dts = icbl.getAllParentsbyCode(ccode);
                //    if (dts.Rows.Count > 0 && dts != null)
                //    {
                //        for (int y = dts.Rows.Count - 1; y >= 0; y--)
                //        {
                //            desc += dts.Rows[y]["Description"].ToString() + ">>";
                //            dt.Rows[i]["商品カテゴリ名"] = desc;
                //        }
                //        desc = String.Empty;
                //    }
                //}
                #endregion

                DataColumn newCol = new DataColumn("Parent", typeof(System.Int32));
                newCol.DefaultValue = 1;
                dt.Columns.Add(newCol);
                if (scbl.InsertCSV(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String ccode = dt.Rows[i]["カテゴリID"].ToString();

                        DataTable dts = icbl.getAllParentsbyCode(ccode);
                        if (dts.Rows.Count > 0 && dts != null)
                        {
                            for (int y = dts.Rows.Count - 1; y >= 0; y--)
                            {
                                if ((Convert.ToInt32(dts.Rows[y]["ParentID"])) != 0)
                                {
                                    desc += dts.Rows[y]["Description"].ToString() + "\\";
                                    dt.Rows[i]["商品カテゴリ名"] = desc;
                                }
                            }
                            desc = String.Empty;
                        }
                        else
                        {
                            dt.Rows[i]["商品カテゴリ名"] = dt.Rows[i]["パス名"].ToString(); ;
                        }
                    }
                   // scbl.UpdateCSV(dt);
                    scbl.InsertCSV(dt);
                    //ShowMsg("Save Successful!");
                    //Response.Redirect("../Category/Category_View.aspx",false);
                   
                    string result = "Save Successful!";
                    if (result == "Save Successful!")
                    {
                        string url = "/WebForms/Category/Category_View.aspx" ;
                        string script = "window.onload = function(){ alert('";
                        script += result;
                        script += "');";
                        script += "window.location.href = '";
                        script += url;
                        script += "'; }";
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                    }
                }
                else
                {
                    ShowMsg("Save Fail!");
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