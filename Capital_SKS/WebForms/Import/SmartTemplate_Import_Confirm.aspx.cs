/* 
Created By              : Kay Thi Aung
Created Date          : 04/07/2014
Updated By             :
Updated Date         :

 Tables using: Item_ImportLog,Item_Import_ErrorLog
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
using ORS_RCM_BL;
using System.Text;
using System.Transactions;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace ORS_RCM.WebForms.Import
{
    public partial class SmartTemplate_Import_Confirm : System.Web.UI.Page
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
            ConsoleWriteLine_Tofile("---excel import Start!" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (!IsPostBack)
            {
                try
                {
                    #region
                    if (Request.QueryString["FilePath"] != null && Request.QueryString["Extension"] != null)
                    {
                        string FilePath = Request.QueryString["FilePath"].ToString();
                        string Extension = Request.QueryString["Extension"].ToString();

                        DataTable dt = Import_To_Grid(FilePath, Extension);
                        //ConsoleWriteLine_Tofile(dt.Rows.Count .ToString ());

                        //foreach (DataRow row in dt.Rows)
                        //{
                        //    foreach (DataColumn col in dt.Columns)
                        //    {
                        //        ConsoleWriteLine_Tofile(row[col].ToString());
                        //    }
                        //}

                        //if (Request.QueryString["SmartTemplate"] != null)
                        //{
                        //String path = Server.MapPath("~/Import_CSV/") + Request.QueryString["SmartTemplate"].ToString();
                        //DataTable dt = GlobalUI.CSVToTable(path);
                        //dt = GlobalUI.Remove_Doublecode(dt);
                        //String[] colName = {    
                        //                    "商品番号", "基本情報1", "基本情報内容1", "基本情報2", "基本情報内容2", "基本情報3", "基本情報内容3",
                        //                    "基本情報4", "基本情報内容4","基本情報5", "基本情報内容5","基本情報6", "基本情報内容6",
                        //                    "基本情報7", "基本情報内容7","基本情報8", "基本情報内容8","基本情報9", "基本情報内容9",
                        //                    "基本情報10", "基本情報内容10","基本情報11", "基本情報内容11","基本情報12", "基本情報内容12",
                        //                    "基本情報13", "基本情報内容13","基本情報14", "基本情報内容14","基本情報15", "基本情報内容15",
                        //                    "基本情報16", "基本情報内容16","基本情報17", "基本情報内容17","基本情報18", "基本情報内容18",
                        //                    "基本情報19", "基本情報内容19","基本情報20", "基本情報内容20","詳細情報1", "詳細情報内容1",
                        //                    "詳細情報2", "詳細情報内容2","詳細情報3", "詳細情報内容3","詳細情報4", "詳細情報内容4",
                        //                    "ゼット用項目（PC商品説明文）","ゼット用項目（PC販売説明文）",
                        //                    "関連商品1","関連商品2","関連商品3","関連商品4","関連商品5","テクノロジー画像1","テクノロジー画像2",
                        //                    "テクノロジー画像3","テクノロジー画像4","テクノロジー画像5","テクノロジー画像6","キャンペーン画像1",
                        //                    "キャンペーン画像2","キャンペーン画像3","キャンペーン画像4","キャンペーン画像5"
                        //               };

                        //if (CheckColumn(colName, dt))
                        //{
                        //    DataColumn newcol = new DataColumn("チェック", typeof(String));
                        //    newcol.DefaultValue = "";
                        //    dt.Columns.Add(newcol);

                        DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
                        newColumn.DefaultValue = 6;
                        dt.Columns.Add(newColumn);

                        //    DataColumn dc = new DataColumn("エラー内容", typeof(String));
                        //    dc.DefaultValue = "";
                        //    dt.Columns.Add(dc);

                        //    String[] col = {
                        //                    "基本情報1", "基本情報内容1", "基本情報2", "基本情報内容2", "基本情報3", "基本情報内容3",
                        //                    "基本情報4", "基本情報内容4","基本情報5", "基本情報内容5","基本情報6", "基本情報内容6",
                        //                    "基本情報7", "基本情報内容7","基本情報8", "基本情報内容8","基本情報9", "基本情報内容9",
                        //                    "基本情報10", "基本情報内容10","基本情報11", "基本情報内容11","基本情報12", "基本情報内容12",
                        //                    "基本情報13", "基本情報内容13","基本情報14", "基本情報内容14","基本情報15", "基本情報内容15",
                        //                    "基本情報16", "基本情報内容16","基本情報17", "基本情報内容17","基本情報18", "基本情報内容18",
                        //                    "基本情報19", "基本情報内容19","基本情報20", "基本情報内容20","詳細情報1","詳細情報2",
                        //                    "詳細情報3","詳細情報4"                                      
                        //               };
                        //    dt = CheckLength(dt, col, 60);

                        //    String[] col1 = { "商品番号" };
                        //    dt = CheckLength(dt, col1, 254);

                        //    String[] col2 = { "詳細情報内容1", "詳細情報内容2", "詳細情報内容3", "詳細情報内容4" };
                        //    dt = CheckLength(dt, col2, 3000);

                        //    String[] col3 = { "ゼット用項目（PC商品説明文）", "ゼット用項目（PC販売説明文）" };
                        //    dt = CheckLength(dt, col3, 10240);

                        //    String[] col4 = { "関連商品1", "関連商品2", "関連商品3", "関連商品4", "関連商品5" };
                        //    dt = CheckLength(dt, col4, 254);

                        //    String[] col5 = {
                        //                    "テクノロジー画像1","テクノロジー画像2",
                        //                    "テクノロジー画像3","テクノロジー画像4","テクノロジー画像5","テクノロジー画像6","キャンペーン画像1",
                        //                    "キャンペーン画像2","キャンペーン画像3","キャンペーン画像4","キャンペーン画像5"
                        //                };
                        //    dt = CheckLength(dt, col5, 200);
                        ConsoleWriteLine_Tofile("OK?");

                        gvSmartTemplate.DataSource = dt;
                        gvSmartTemplate.DataBind();
                        Cache["dtSmart"] = dt;

                        //}
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    Session["Exception"] = ex.ToString();
                    ConsoleWriteLine_Tofile(ex.ToString());
                    Response.Redirect("~/CustomErrorPage.aspx?");
                }
            }
        }

        public DataTable Import_To_Grid(string FilePath, string Extension)
        {
            ConsoleWriteLine_Tofile(FilePath);
            ConsoleWriteLine_Tofile(Extension);
            string SheetName = "";
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }

            conStr = String.Format(conStr, FilePath, "Yes");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dtGridView = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
           // string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            foreach (DataRow row in dtExcelSchema.Rows)   //updated by ETZ for discard Filter table
            {
                if (row["TABLE_NAME"].ToString() == "商品説明文$")
                {
                    SheetName = "商品説明文$";
                }
            }
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dtGridView);
            connExcel.Close();

            return dtGridView;
            

        }

        protected DataTable CheckLength(DataTable dt, String[] col, int length)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < col.Length; j++)
                    {
                        Encoding enc = Encoding.GetEncoding(932);
                        int byteLength = enc.GetByteCount(dt.Rows[i][col[j]].ToString());
                        if (byteLength > length)
                        {
                            dt.Rows[i]["チェック"] = "エラー";
                            dt.Rows[i]["エラー内容"] = col[j] + "-Greater than " + length + " Bytes";
                            dt.Rows[i]["Type"] = 5;
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

        protected void gvSmartTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dtbind = new DataTable();
            if (Cache["dtSmart"] != null)
            {
                dtbind = Cache["dtSmart"] as DataTable;
            }

            gvSmartTemplate.DataSource = dtbind;
            gvSmartTemplate.PageIndex = e.NewPageIndex;
            gvSmartTemplate.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = Cache["dtSmart"] as DataTable;
                String[] colName = {
                                     "基本情報1", "基本情報内容1", "基本情報2", "基本情報内容2", "基本情報3", "基本情報内容3",
                                     "基本情報4", "基本情報内容4","基本情報5", "基本情報内容5","基本情報6", "基本情報内容6",
                                     "基本情報7", "基本情報内容7","基本情報8", "基本情報内容8","基本情報9", "基本情報内容9",
                                     "基本情報10", "基本情報内容10","基本情報11", "基本情報内容11","基本情報12", "基本情報内容12",
                                     "基本情報13", "基本情報内容13","基本情報14", "基本情報内容14","基本情報15", "基本情報内容15",
                                     "基本情報16", "基本情報内容16","基本情報17", "基本情報内容17","基本情報18", "基本情報内容18",
                                     "基本情報19", "基本情報内容19","基本情報20", "基本情報内容20","詳細情報1", "詳細情報内容1",
                                     "詳細情報2", "詳細情報内容2","詳細情報3", "詳細情報内容3","詳細情報4", "詳細情報内容4"                                  
                                   };

                if (!Check_SpecialCharacter(colName, dt))
                {
                    Smart_Template_BL stbl = new Smart_Template_BL();
                    ConsoleWriteLine_Tofile("btnUpdate_Click");
                    var option = new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                        Timeout = TimeSpan.MaxValue
                    };
                    TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                    using (scope)
                    {
                        stbl.Insert_SmartTemplate(dt);
                        ItemLog(dt);
                        scope.Complete();
                    }
                    int id = Convert.ToInt32(hflog.Value);
                    hflog.Value = null;
                    Response.Redirect("../Import/Smart_Template_LogView.aspx?LogID=" + id, false);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void ItemLog(DataTable dt)
        {
            Import_Item_BL imbl = new Import_Item_BL();
            Item_ImportLog_BL itImplog = new Item_ImportLog_BL();
            int recordcount = dt.Rows.Count;
            int uid = Userid;
            DataRow[] drcheck = dt.Select("Type = 5");
            int importid = itImplog.ImportLogInsert(6, recordcount, drcheck.Count(), uid);
            hflog.Value = Convert.ToString(importid);

            DataColumn newColumn = new DataColumn("Item_Import_LogID", typeof(System.String));
            newColumn.DefaultValue = importid;
            dt.Columns.Add(newColumn);

            DataTable dterror = new DataTable();
            DataRow[] dr = dt.Select("Type = 5");
            if (dr.Count() > 0)
            {
                dterror = dt.Select("Type=5").CopyToDataTable();
                if (dterror != null && dterror.Rows.Count > 0)
                {
                    for (int i = 0; i < dterror.Rows.Count; i++)
                    {
                        dterror.Rows[i]["Type"] = 0;
                    }
                }
                Item_ImportLog_BL imlbl = new Item_ImportLog_BL();
                imlbl.SmartTemplate_ErrorLog_Insert(dterror);
            }

            DataTable dtCorrect = new DataTable();
            dr = dt.Select("Type = 6");
            if (dr.Count() > 0)
            {
                dtCorrect = dt.Select("Type=6").CopyToDataTable();
                if (dtCorrect != null && dtCorrect.Rows.Count > 0)
                {
                    imbl.XmlforSmart_Import_ItemLog(dtCorrect);
                }
            }
        }

        protected Boolean Check_SpecialCharacter(String[] columnName, DataTable dt)
        {
            try
            {

                DataColumnCollection col = dt.Columns;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        for (int k = 0; k < columnName.Length; k++)
                        {
                            if (dt.Columns[j].ColumnName == columnName[k])
                            {
                                string input = dt.Rows[i][j].ToString();
                                string specialChar = @"㈰㈪㈫㈬㈭㈮㈯㉀㈷㉂㉃㈹㈺㈱㈾㈴㈲㈻㈶㈳㈵㈼㈽㈿㈸㊤㊥㊦㊧㊨㊩㊖㊝㊘㊞㊙㍾㍽㍼㍻㍉㌢㌔㌖㌅㌳㍎㌃㌶㌘㌕㌧㍑㍊㌹㍗㌍㍂㌣㌦㌻㌫㍍№℡㎜㎟㎝㎠㎤㎡㎥㎞㎢㎎㎏㏄㎖㎗㎘㎳㎲㎱㎰①②③④⑤⑥⑦⑧⑨⑩⑪⑫⑬⑭⑮⑯⑰⑱⑲⑳ⅠⅡⅢⅣⅤⅥⅦⅧⅨⅩⅰⅱⅲⅳⅴⅵⅶⅷⅸⅹ";
                                string plusign = "[[(+)]]";
                                string minussign = "[[(-)]]";
                                foreach (var item in specialChar)
                                {
                                    if (input.Contains(item))
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('The template contains special character.');", true);
                                        return true;
                                    }
                                }
                                if (input.Contains(plusign) || input.Contains(minussign))
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Template description contains special character.');", true);
                                    return true;
                                }

                            }
                        }
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }
        /*
        * Created By Inaoka
        * Created Date 2015/04/18
        * Updated By
        * Updated Date
        *
        * Description:
        * trace by using the StreamWriter
        * with ConsoleWriteLine_Tofile
        * output ConsoleWriteLIne.txt in currenct directory
        * 
        */
        static void ConsoleWriteLine_Tofile(string traceText)
        {
            //StreamWriter sw = new StreamWriter("C:/Users/Administrator/Documents/SKS/ConsoleWriteLine.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            //sw.AutoFlush = true;
            //Console.SetOut(sw);
            //Console.WriteLine(traceText);
            //sw.Close();
            //sw.Dispose();
        }


    }
}