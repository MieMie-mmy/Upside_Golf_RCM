//Import_temp

/* 
Created By              : Kyaw Thet Paing
Created Date          : 
Updated By             :Kaythi Aung
Updated Date         :
Tables using           :Import_temp
                        
                                  
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
using ORS_RCM_BL;
using System.Text;

namespace ORS_RCM.WebForms.Import
{
    public partial class Item_Option_Import_Confirm : System.Web.UI.Page
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
                    if (Request.QueryString["Option"] != null)
                    {
                        String path = Server.MapPath("~/Import_CSV/") + Request.QueryString["Option"].ToString();
                        DataTable dt = GlobalUI.CSVToTable(path);
                        dt = GlobalUI.Remove_Doublecode(dt);
                        String[] colName = { "コントロールフラグ", "商品番号", "項目名", "選択肢" };
                        if (CheckColumn(colName, dt))
                        {
                            //check itemcode exist in master table and insert errmessage into temp table
                            CheckTable(dt);

                            DataColumn Col = dt.Columns.Add("チェック", typeof(String));
                            Col.SetOrdinal(0);

                            Item_Option_BL iobl = new Item_Option_BL();
                            DataTable dtErr = iobl.GetErrorTable();
                            dtErr = CopyColumns(dtErr, dt, "ErrMsg");

                            String[] col = { "項目名", "選択肢" };
                            dtErr = CheckLength(dtErr, col);
                            dtErr.Columns["ErrMsg"].ColumnName = "エラー内容";

                            dtErr = changeTextDatatable(dtErr);
                            gvOptionData.DataSource = dtErr;
                            Cache["OptionData"] = dtErr;
                            gvOptionData.DataBind();
                        }
                        else
                        {
                            ShowMsg("File format wrong!");
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cache["OptionData"] != null)
                {
                    DataTable dt = Cache["OptionData"] as DataTable;
                    DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));
                    newColumn.DefaultValue = Userid;
                    dt.Columns.Add(newColumn);

                    Item_Option_BL option = new Item_Option_BL();
                    int result = option.InsertCSV(dt);
                    Cache.Remove("OptionData");
                    if (result > 0)
                    {
                        ShowMsg("Save Successful!");
                        Response.Redirect("../Import/Import_Item_Option_Log.aspx?Log_ID=" + result,false);
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
                string[] k = new string[100];
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string extra = string.Empty;

                        for (int j = 0; j < col.Length; j++)
                        {
                            Encoding enc = Encoding.GetEncoding(932);
                            int byteLength = enc.GetByteCount(dt.Rows[i][col[j]].ToString());

                            //int d = dt.Rows[i][col[j]].ToString().Length;
                            string n = dt.Rows[i][col[j]].ToString();
                            if (byteLength > 32)
                            {
                                if (!String.IsNullOrWhiteSpace(n))
                                    k = n.Split(',');
                                for (int y = 0; y < k.Length; y++)
                                {
                                    string cklength = k[y].ToString();
                                    if (!String.IsNullOrWhiteSpace(cklength))
                                    {
                                        byteLength = enc.GetByteCount(cklength);
                                        if ( byteLength > 32)
                                        {
                                            extra = "extra length";
                                        }
                                    }

                                }
                                k = null;
                            }
                            String str = dt.Rows[i]["Errmsg"].ToString();
                            //if (dt.Rows[i][col[j]].ToString().Length > 50 && String.IsNullOrWhiteSpace(str))
                            if (!String.IsNullOrWhiteSpace(extra) && String.IsNullOrWhiteSpace(str))
                            {

                                dt.Rows[i]["チェック"] = "エラー";
                                dt.Rows[i]["Errmsg"] = col[j] + "フォーマットが不正です。";
                                dt.Rows[i]["Type"] = 0;
                            }
                        }
                    }
                }//if
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
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

        protected void gvOptionData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = e.Row.FindControl("lblControlID") as Label;
                    if (lbl.Text.Equals("n"))
                        lbl.Text = "新規";
                    else if (lbl.Text.Equals("u"))
                        lbl.Text = "更新";
                    else if (lbl.Text.Equals("d"))
                        lbl.Text = "削除";
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvOptionData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dtop = new DataTable();
            if (Cache["OptionData"] != null)
            {
                 dtop = Cache["OptionData"] as DataTable;
            }
            gvOptionData.DataSource = dtop;
            gvOptionData.PageIndex = e.NewPageIndex;
            gvOptionData.DataBind();
        }
    }
}