using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ORS_RCM_BL;
using System.Text;

namespace ORS_RCM.WebForms.Import
{
    public partial class Item_Category_Import_Confirm : System.Web.UI.Page
    {
        public int Userid
        {
            get
            {
                if (Session["User_ID"] != null)

                    return Int32.Parse(Session["User_ID"].ToString());
                else
                    return 0;
            }
        }     

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Category"] != null)
                {
                    String path = Server.MapPath("~/Import_CSV/") + Request.QueryString["Category"].ToString();
                    DataTable dt = GlobalUI.CSVToTable(path);
                    dt = GlobalUI.Remove_Doublecode(dt);
                    String[] colName = { "コントロールフラグ", "商品番号", "商品カテゴリID", "商品カテゴリ名" };
                    if (CheckColumn(colName, dt))
                    {
                        CheckTable(dt);

                        DataColumn Col = dt.Columns.Add("チェック", typeof(String));
                        Col.SetOrdinal(0);

                        Item_Option_BL iobl = new Item_Option_BL();
                        DataTable dtErr = iobl.GetErrorTable();
                        dtErr = CopyColumns(dtErr, dt, "ErrMsg");

                        String[] col = { "商品カテゴリID", "商品カテゴリ名" };
                        dtErr = CheckLength(dtErr, col);
                        dtErr.Columns["ErrMsg"].ColumnName = "エラー内容";

                        dtErr = changeTextDatatable(dtErr);
                        gvCategoryData.DataSource = dtErr;
                        Cache["CategoryImport"] = dtErr;
                        gvCategoryData.DataBind();
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

        protected DataTable changeTextDatatable(DataTable dt)
        {
            try
            {
                int errorMsg = 5;
                int error = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!String.IsNullOrWhiteSpace(dt.Rows[i][errorMsg].ToString()))
                        dt.Rows[i][error] = "エラー";
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
        
        protected DataTable CheckLength(DataTable dt, String[] col)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < col.Length; j++)
                    {
                        Encoding enc = Encoding.GetEncoding(932);
                        int byteLength = enc.GetByteCount(dt.Rows[i][col[j]].ToString());
                        int d = dt.Rows[i][col[j]].ToString().Length;
                        string n = dt.Rows[i][col[j]].ToString();
                        String str = dt.Rows[i]["ErrMsg"].ToString();
                        if (byteLength > 32 && String.IsNullOrWhiteSpace(str))
                        {
                            dt.Rows[i]["チェック"] = "エラー";
                            dt.Rows[i]["ErrMsg"] = col[i] + "フォーマットが不正です。";
                            dt.Rows[i]["Type"] = 0;
                        }
                    }
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

        private DataTable CopyColumns(DataTable source, DataTable dest, string columns)
        {
            try
            {
                if (source.Rows.Count == dest.Rows.Count)
                {
                    dest.Columns.Add("ErrMsg", typeof(String));
                    dest.Columns.Add("Type", typeof(int));
                    for (int i = 0; i < source.Rows.Count; i++)
                    {
                        dest.Rows[i]["ErrMsg"] = source.Rows[i]["ErrMsg"].ToString();
                        dest.Rows[i]["Type"] = source.Rows[i]["Type"].ToString();
                    }
                }
                else
                {
                    dest = null;
                }
                return dest;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return source;
            }
        }

        protected void CheckTable(DataTable dt)
        {
            try
            {
                Item_Option_BL iobl = new Item_Option_BL();
                iobl.Check_ItemCode(dt);
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cache["CategoryImport"] != null)
                {
                    DataTable dt = Cache["CategoryImport"] as DataTable;
                    Cache.Remove("CategoryImport");
                    DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));
                    newColumn.DefaultValue = Userid;
                    dt.Columns.Add(newColumn);
                    Item_Category_BL icbl = new Item_Category_BL();
                    int result = icbl.InsertCSV(dt);
                    if (result > 0)
                    {

                        Response.Redirect("../Import/Item_Category_Import_Log.aspx?Log_ID=" + result, false);
                        ShowMsg("Save Successful!");
                    }
                    else
                    {
                        ShowMsg("Save Fail!");
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvCategoryData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[1].Text.Equals("n"))
                        e.Row.Cells[1].Text = "新規";
                    else if (e.Row.Cells[1].Text.Equals("u"))
                        e.Row.Cells[1].Text = "更新";
                    else if (e.Row.Cells[1].Text.Equals("d"))
                        e.Row.Cells[1].Text = "削除";
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvCategoryData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dtbind =new DataTable();
            if (Cache["CategoryImport"] != null)
            {
            dtbind =Cache["CategoryImport"] as DataTable;
            }
            gvCategoryData.DataSource= dtbind;
            gvCategoryData.PageIndex = e.NewPageIndex;
            gvCategoryData.DataBind();
        }
    }
}