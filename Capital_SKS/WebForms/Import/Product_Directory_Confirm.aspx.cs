using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ORS_RCM_BL;
using System.Text;
using ORS_RCM;
using Microsoft.VisualBasic;
using Excel;
using System.Data.OleDb;
using System.IO;
using System.Configuration;


namespace Capital_SKS.WebForms.Import
{
    public partial class Product_Directory_Confirm : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    if (Request.QueryString["Product_Directory"] != null)
                    {
                        string path = Server.MapPath("~/Import_CSV/")+Request.QueryString["Product_Directory"].ToString();                       
                        System.Data.DataTable dt = new System.Data.DataTable();
                        System.Data.DataTable dt1 = new System.Data.DataTable();
                        dt1 = BindExcelData(path,1);
                        dt= BindExcelData(path,2);
                         
                         string[] colName1 = { "商品番号" };
                        if(CheckColumn(colName1,dt1))
                        {
                            CheckTable(dt1);
                            DataColumn Col = dt1.Columns.Add("チェック", typeof(String));
                            Col.SetOrdinal(0);

                            Item_Option_BL iobl = new Item_Option_BL();
                            System.Data.DataTable dtErr1 = iobl.GetErrorTable();
                            dtErr1 = CopyColumns(dtErr1, dt1, "ErrMsg");

                            String[] col1 = { "商品番号"};
                            dtErr1 = CheckLength(dtErr1, col1);
                            dtErr1.Columns["ErrMsg"].ColumnName = "エラー内容";

                            dtErr1 = changeTextDatatable(dtErr1,2);                       
                            Cache["SaleDecPCImport"] = dtErr1;
                        }                      
                        string[] colName = { "商品番号", "全商品ディレクトリID", "プロダクトカテゴリ", "Wowma_CategoryID", "ORS自社_CategoryID" };
                        if (CheckColumn(colName, dt))
                        {
                            CheckTable(dt);
                            DataColumn Col = dt.Columns.Add("チェック", typeof(String));
                            Col.SetOrdinal(0);

                            Item_Option_BL iobl = new Item_Option_BL();
                            System.Data.DataTable dtErr = iobl.GetErrorTable();
                            dtErr = CopyColumns(dtErr, dt, "ErrMsg");

                            String[] col = { "商品番号", "全商品ディレクトリID", "プロダクトカテゴリ", "Wowma_CategoryID", "ORS自社_CategoryID" };
                            dtErr = CheckLength(dtErr, col);
                            dtErr.Columns["ErrMsg"].ColumnName = "エラー内容";

                            dtErr = changeTextDatatable(dtErr, 6);
                            gdProductDirectory.DataSource = dtErr;
                            Cache["ProductDirectoryImport"] = dtErr;
                            gdProductDirectory.DataBind();
                        }
                        else
                        {
                            if (dt.Rows.Count < 2)
                            {
                                ShowMsg("Sheet 1 has no data, ");
                            }
                            else
                            {
                                ShowMsg("file format was wrong");
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        private System.Data.DataTable BindExcelData(string strFileName,int sheet)
        {
            OleDbDataAdapter objDataAdapter = new OleDbDataAdapter();
            DataSet objDataSet = new DataSet();

            try
            {
                objDataAdapter.SelectCommand = ExcelConnection(strFileName,sheet);

                if (objDataAdapter.SelectCommand != null)
                {
                    objDataAdapter.Fill(objDataSet);

                }
            }
            catch (Exception objException)
            {
                throw objException;
            }          
            return objDataSet.Tables[0];
        }

        private OleDbCommand ExcelConnection(string strFileName,int sheet)
        {
            OleDbCommand objCommand = null;
            OleDbConnection objXConn;
            try
            {

                string path = Server.MapPath("~/Import_CSV/") + Request.QueryString["Product_Directory"].ToString();
                string xConnStr ="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path +";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=2;\"";
                System.Data.DataTable dtSheetNames = new System.Data.DataTable();
                objXConn = new OleDbConnection(xConnStr);
                objXConn.Open();
                dtSheetNames = objXConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (sheet == 1)
                {
                    string filename = dtSheetNames.Rows[0]["TABLE_NAME"].ToString();                   
                    objCommand = new OleDbCommand("SELECT * FROM [" + filename + "]", objXConn);
                }
                if(sheet==2)
                {
                    string filename = dtSheetNames.Rows[1]["TABLE_NAME"].ToString();
                    objCommand = new OleDbCommand("SELECT * FROM [" + filename + "]", objXConn);
                }

            }
            catch (Exception objException)
            {

            }
            return objCommand;
        }           
        protected void CheckTable(System.Data.DataTable dt)
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
        public static System.Data.DataTable Remove_Doublecode(System.Data.DataTable dt)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = dt.Columns[i].ToString().Replace("\"", "");
            }
            int columnCount = dt.Columns.Count;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                bool allNull = true;
                for (int j = 0; j < columnCount; j++)
                {
                    if (dt.Rows[i][j] != DBNull.Value)
                    {
                        allNull = false;
                    }
                }
                if (allNull)
                {
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();
            return dt;
        }
        protected Boolean CheckColumn(String[] colName, System.Data.DataTable dt)
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
        private System.Data.DataTable CopyColumns(System.Data.DataTable source, System.Data.DataTable dest, string columns)
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
        protected System.Data.DataTable CheckLength(System.Data.DataTable dt, String[] col)
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
        protected System.Data.DataTable changeTextDatatable(System.Data.DataTable dt,int errorMsg)
        {
            try
            {
                //int errorMsg = 4;
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

        protected void gdProductDirectory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    
                }
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
                if (Cache["SaleDecPCImport"] != null)
                {

                    System.Data.DataTable dt = Cache["SaleDecPCImport"] as System.Data.DataTable;
                    Cache.Remove("SaleDecPCImport");
                    DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));
                    newColumn.DefaultValue = Userid;
                    dt.Columns.Add(newColumn);
                    Item_Master_BL imbl = new Item_Master_BL();
                    int result = imbl.UpdateSaleDecCSV(dt);
                    if (result > 0)
                    {
                        ShowMsg("Update Successful!");
                    }
                    else
                    {
                        ShowMsg("Update Fail!");
                    }
                }
                if (Cache["ProductDirectoryImport"] != null)
                {
                    System.Data.DataTable dt = Cache["ProductDirectoryImport"] as System.Data.DataTable;
                    Cache.Remove("ProductDirectoryImport");
                    DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));
                    newColumn.DefaultValue = Userid;
                    dt.Columns.Add(newColumn);
                    Item_Master_BL imbl = new Item_Master_BL();
                    int result = imbl.InsertCSV(dt);
                    if (result > 0)
                    {
                        Response.Redirect("../Import/Product_Directory_Log.aspx?Log_ID=" + result, false);
                        ShowMsg("Update Successful!");
                    }
                    else
                    {
                        ShowMsg("Update Fail!");
                    }
                }
              
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gdProductDirectory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            System.Data.DataTable dtproduct = new System.Data.DataTable();
            if (Cache["ProductDirectoryImport"] != null)
            {
                dtproduct = Cache["ProductDirectoryImport"] as System.Data.DataTable;
            }
            gdProductDirectory.DataSource = dtproduct;
            gdProductDirectory.PageIndex = e.NewPageIndex;
            gdProductDirectory.DataBind();
        }
    }
}