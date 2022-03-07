/* 
Created By              : Kay Thi Aung
Created Date          : 
Updated By             :
Updated Date         :

 Tables using           : Item_Master
                                     -Item_Image
                                     -Item_Related_Item
                                     -Item_Import_ItemLog
 *                                  -Item_ImportLog
 *                                  
 * Storedprocedure using:SP_Item_Import_ItemLog_Insert_XML
 *                                           -SP_Item_ImportLog_Insert
 *                                           -SP_Import_Item_Data_XML
 *                                           -SP_Import_Item_Image_XML
 *                                           -SP_Import_RelateItemImage_XML
 *                                           -SP_ItemShopImport_XML
 *                                           -SP_ImportLibrary_Image_XML
 *                                           -SP_ImportRelated_Item_XML
 *                                           -SP_Relateitemimport_CtrlXML
 *                                           -SP_LibraryImageImport_Ctrl_XML
 *                                           -SP_ItemShopImportwithCtrl_XML
 * Comments                          :If Ctrl_ID is 'n',check item_code  exist or not in the table .If the item code exist in the table, update data.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Text;
using ORS_RCM_BL;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace ORS_RCM.WebForms.Import
{
    public partial class Import_Item_Data : System.Web.UI.Page
    {
        Import_Item_Data_BL ibl; int count;
        Item_ImportLog_BL itImplog;
        Import_Item_BL imbl;
        String[] Exfield = new String[1000];
        DataTable dtlog = new DataTable();
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
                    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                
                    head.InnerText = "商品情報データインポート";
                    btnConfirm_Save.Visible = false;
                    headcon.Visible = false;
                    par.Visible = false;
                    hcon.Visible = false;
                }
                else
                {
                    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                    btnpopup.Visible = false;
                    btnConfirm_Save.Visible = true;
                    Confirm();
                    btnConfirm_Save.Text = "更 新";
                    btnImport.Visible = false;
                    head.Visible = false;
                    headcon.Visible = true;
                    par.Visible = true;
                    hcon.Visible = true;

                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void Confirm() 
        {
            try
            {
                Label1.Visible = false;
                uplItemData.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }        
        }

        protected DataTable ChangeHeader(DataTable dt)
        {
            try
            {
                if (dt.Columns.Contains("商品名"))
                {
                    dt.Columns["商品名"].ColumnName = "Item_Name";

                }
                dt.Columns["商品番号"].ColumnName = "Item_Code";

                if (dt.Columns.Contains("販売管理番号"))
                {
                    dt.Columns["販売管理番号"].ColumnName = "Item_AdminCode";
                }

                //if (dt.Columns.Contains("PC用販売説明文"))
                //{ dt.Columns.Remove("PC用販売説明文"); dt.AcceptChanges(); }
                //if (dt.Columns.Contains("PC用商品説明文"))
                //{
                //    dt.Columns.Remove("PC用商品説明文"); dt.AcceptChanges();
                //}
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                Cache.Remove("DataTable");
                Cache.Remove("DtLibraryImage");
                Cache.Remove("dtshop");
                Cache.Remove("dtrelateitemonly");
                Cache.Remove("dtLibImage");
                Cache.Remove("dtRTCtrl");
                Cache.Remove("dtLibCtrl");
                Cache.Remove("dtshopCtrl");

                Cache.Remove("dtRakutenpath");

                ibl = new Import_Item_Data_BL();
                itImplog = new Item_ImportLog_BL();
                imbl = new Import_Item_BL();
                if (uplItemData.HasFile)
                {
                    string filename = Path.GetFileName(uplItemData.PostedFile.FileName);
                    if (check(filename) == true)
                    {
                        string path = Server.MapPath("~/Import_CSV/") + filename;
                        uplItemData.SaveAs(Server.MapPath("~/Import_CSV/") + filename);
                        DataTable dt = GlobalUI.CSVToTable(path);
                        dt = GlobalUI.Remove_Doublecode(dt);
                        DataColumnCollection colnames = dt.Columns;
                        if (colnames.Contains("商品番号") && colnames.Contains("フォルダ名") && colnames.Contains("画像名"))
                        {
                            Cache.Insert("dtimage", dt, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            Cache.Insert("DataTableImage", dt, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            ImageLog(dt);
                            Cache.Remove("DataTable");
                        }//Image
                        else if (colnames.Contains("商品番号") && colnames.Contains("商品画像URL") && colnames.Contains("画像名1") && colnames.Contains("画像名2") && colnames.Contains("画像名3") && colnames.Contains("画像名4") && colnames.Contains("画像名5"))
                        {
                            Cache.Insert("dtRakutenpath", dt, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            ImageLog(dt);

                            gvdata.DataSource = dt;
                            gvdata.DataBind();
                            //Cache.Remove("dtimage");

                        }//Item Image
                        else if (colnames.Contains("商品番号") && colnames.Contains("関連商品") && colnames.Contains("ライブラリ画像") )
                        {
                            Cache.Insert("DtLibraryImage", dt, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            ImageLog(dt);
                            
                            gvdata.DataSource = dt;
                            gvdata.DataBind();
                            Cache.Remove("DataTable");

                        }//Library Image
                        else if (colnames.Contains("コントロールカラム") && colnames.Contains("商品番号") && colnames.Contains("出品対象ショップ"))
                        {
                            Cache.Insert("dtshopCtrl", dt, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            ImageLog(dt);

                            gvdata.DataSource = dt;
                            gvdata.DataBind();
                            Cache.Remove("DataTable");
                        }//Item_Shop With Ctrl
                        else if (colnames.Contains("商品番号") && colnames.Contains("出品対象ショップ")) 
                        {
                            Cache.Insert("dtshop", dt, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            ImageLog(dt);

                            gvdata.DataSource = dt;
                            gvdata.DataBind();
                            Cache.Remove("DataTable");
                        }//Item_Shop
                        else if (colnames.Contains("コントロールカラム") && colnames.Contains("商品番号") && colnames.Contains("ライブラリ画像")) //LibraryImage Only with Ctrl
                        {
                            Cache.Insert("dtLibCtrl", dt, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            ImageLog(dt);

                            gvdata.DataSource = dt;
                            gvdata.DataBind();
                            Cache.Remove("DataTable");
                        }
                        else if (colnames.Contains("商品番号") && colnames.Contains("ライブラリ画像")) //LibraryImage Only
                        {
                            Cache.Insert("dtLibImage", dt, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            ImageLog(dt);

                            gvdata.DataSource = dt;
                            gvdata.DataBind();
                            Cache.Remove("DataTable");
                        }
                        else if (colnames.Contains("コントロールカラム") && colnames.Contains("商品番号") && colnames.Contains("関連商品") && colnames.Contains("関連商品番号"))//Relateitem with ctrl
                        {
                            Cache.Insert("dtRTCtrl", dt, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            ImageLog(dt);

                            gvdata.DataSource = dt;
                            gvdata.DataBind();
                            Cache.Remove("DataTable");

                        }
                        else if (colnames.Contains("商品番号") && colnames.Contains("関連商品") && colnames.Contains("関連商品番号"))//Relateitem only
                        {
                            Cache.Insert("dtrelateitemonly", dt, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            ImageLog(dt);

                            gvdata.DataSource = dt;
                            gvdata.DataBind();
                            Cache.Remove("DataTable");
                        
                        }
                       
                        //else if (colnames.Contains("関連商品1")) //test 
                        //{
                        //    Cache.Insert("DTImage", dt, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                        //    ImageLog(dt);

                        //    gvdata.DataSource = dt;
                        //    gvdata.DataBind();
                        //    Cache.Remove("DataTable");
                        //}
                        else
                        {
                            if (dt.Columns.Contains("関連商品") || dt.Columns.Contains("ライブラリ画像") || colnames.Contains("出品対象ショップ")) { GlobalUI.MessageBox("Please insert correct column!"); }
                            else
                            {
                                Cache.Insert("dtsource", dt, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);

                                int recordcount = dt.Rows.Count;
                                int uid = Userid;
                                int importid = itImplog.ImportLogInsert(5, recordcount, 0, uid);
                                hfid.Value = Convert.ToString(importid);
                                DataTable copydt = dt.Copy();
                                DataTable dtErrorlog = ChangeHeader(copydt);


                                DataColumn newColumn = new DataColumn("Item_Import_LogID", typeof(System.String));
                                newColumn.DefaultValue = importid;
                                dtErrorlog.Columns.Add(newColumn);
                                Cache.Insert("ErrorlogTable", dtErrorlog, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                                #region errorlog
                                //DataColumn nc = new DataColumn("Type", typeof(System.Int32));
                                //nc.DefaultValue = 0;
                                //dtErrorlog.Columns.Add(nc);
                                //itImplog.ItemImportLogInsert(dtErrorlog);
                                #endregion
                                // imbl.XmlforItem_Import_ItemLog(dtErrorlog);
                                //ViewState["CSV"] = dt;
                                gvdata.DataSource = dt;
                                gvdata.DataBind();

                                DataColumnCollection cols = dt.Columns;
                                if (cols.Contains("ライブラリ画像１")) { count = 1; }

                                if (cols.Contains("ライブラリ画像2")) { count += 1; }

                                if (cols.Contains("ライブラリ画像3")) { count += 1; }

                                if (cols.Contains("ライブラリ画像4")) { count += 1; }

                                if (cols.Contains("ライブラリ画像5")) { count += 1; }

                                if (cols.Contains("ライブラリ画像6")) { count += 1; }
                                hfcount.Value = count.ToString();
                                String[] colName = { "PC用販売説明文", "商品名", "商品番号", "ブランドコード", "カタログ情報", "分類名", "競技名", "シーズン", "年度",
                                "掲載可能日", "発売日", "フリースペース1", "フリースペース2", "フリースペース3", "SHOP", "闇市パスワード", "倉庫指定", "代引料", 
                                "特記フラグ","個別送料","製品コード","ヤフースペック値","ポンパレカテゴリID","ヤフーカテゴリID","楽天カテゴリID","商品情報",
                                "スマートフォン用商品説明文","モバイル用商品説明文","PC用商品説明文","送料","スマートテンプレート","販売価格","ブランド名","原価",
                                "仕入先名","定価","YahooエビデンスURL","ライブラリ画像１","ライブラリ画像2","ライブラリ画像3","ライブラリ画像4","ライブラリ画像5","ライブラリ画像6" };
                                Exfield = colName;
                                Array.Sort(Exfield);
                                DataTable dtImport = new DataTable();
                                for (int i = 0; i < Exfield.Count(); i++)
                                {
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        if (Exfield[i] == dc.ColumnName.ToString())
                                        {
                                            string colname = Exfield[i].ToString();
                                            if (dtImport.Columns.Contains(Exfield[i]))
                                            { }
                                            else
                                                dtImport.Columns.Add(Exfield[i], typeof(string));

                                        }
                                        else if (dtImport.Columns.Contains(Exfield[i])) { }
                                        else
                                            dtImport.Columns.Add(Exfield[i], typeof(string));
                                    }
                                }
                                CopyColumns(dt, dtImport, colName);
                                //ViewState["DataTable"] = dtImport;
                                Cache.Insert("DataTable", dtImport, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            }//else 関連商品
                            #region
                            //DataColumnCollection cols = dt.Columns;
                            //if (cols.Contains("PC用販売説明文")) { }
                            //else
                            //{
                            //    dt.Columns.Add("PC用販売説明文", typeof(string));
                            //}
                            //if (cols.Contains("商品名")) { }
                            //else
                            //{
                            //    dt.Columns.Add("商品名", typeof(string));
                            //}
                            //if (cols.Contains("商品番号")) { }
                            //else
                            //{
                            //    dt.Columns.Add("商品番号", typeof(string));
                            //}
                            //if (cols.Contains("ブランドコード")) { }
                            //else
                            //{
                            //    dt.Columns.Add("ブランドコード", typeof(string));
                            //}
                            //if (cols.Contains("カタログ情報")) { }
                            //else
                            //{
                            //    dt.Columns.Add("カタログ情報", typeof(string));
                            //}
                            //if (cols.Contains("分類名")) { }
                            //else
                            //{
                            //    dt.Columns.Add("分類名", typeof(string));
                            //}
                            //if (cols.Contains("競技名")) { }
                            //else
                            //{
                            //    dt.Columns.Add("競技名", typeof(string));
                            //}
                            //if (cols.Contains("シーズン")) { }
                            //else
                            //{
                            //    dt.Columns.Add("シーズン", typeof(string));
                            //}
                            //if (cols.Contains("年度")) { }
                            //else
                            //{
                            //    dt.Columns.Add("年度", typeof(string));
                            //}
                            //if (cols.Contains("掲載可能日")) { }
                            //else
                            //{
                            //    dt.Columns.Add("掲載可能日", typeof(string));
                            //}
                            //if (cols.Contains("発売日")) { }
                            //else
                            //{
                            //    dt.Columns.Add("発売日", typeof(string));
                            //}
                            //if (cols.Contains("フリースペース1")) { }
                            //else
                            //{
                            //    dt.Columns.Add("フリースペース1", typeof(string));
                            //}
                            //if (cols.Contains("フリースペース2")) { }
                            //else
                            //{
                            //    dt.Columns.Add("フリースペース2", typeof(string));
                            //}
                            //if (cols.Contains("フリースペース3")) { }
                            //else
                            //{
                            //    dt.Columns.Add("フリースペース3", typeof(string));
                            //}
                            //if (cols.Contains("SHOP")) { }
                            //else
                            //{
                            //    dt.Columns.Add("SHOP", typeof(string));
                            //}
                            //if (cols.Contains("闇市パスワード")) { }
                            //else
                            //{
                            //    dt.Columns.Add("闇市パスワード", typeof(string));
                            //}
                            //if (cols.Contains("倉庫指定")) { }
                            //else
                            //{
                            //    dt.Columns.Add("倉庫指定", typeof(string));
                            //}
                            //if (cols.Contains("代引料")) { }
                            //else
                            //{
                            //    dt.Columns.Add("代引料", typeof(int));
                            //}
                            //if (cols.Contains("特記フラグ")) { }
                            //else
                            //{
                            //    dt.Columns.Add("特記フラグ", typeof(int));
                            //}
                            //if (cols.Contains("個別送料")) { }
                            //else
                            //{
                            //    dt.Columns.Add("個別送料", typeof(string));
                            //}
                            //if (cols.Contains("製品コード")) { }
                            //else
                            //{
                            //    dt.Columns.Add("製品コード", typeof(string));
                            //}
                            //if (cols.Contains("ヤフースペック値")) { }
                            //else
                            //{
                            //    dt.Columns.Add("ヤフースペック値", typeof(string));
                            //}
                            //if (cols.Contains("ポンパレカテゴリID")) { }
                            //else
                            //{
                            //    dt.Columns.Add("ポンパレカテゴリID", typeof(int));
                            //}
                            //if (cols.Contains("ヤフーカテゴリID")) { }
                            //else
                            //{
                            //    dt.Columns.Add("ヤフーカテゴリID", typeof(int));
                            //}
                            //if (cols.Contains("楽天カテゴリID")) { }
                            //else
                            //{
                            //    dt.Columns.Add("楽天カテゴリID", typeof(int));
                            //}
                            //if (cols.Contains("商品情報")) { }
                            //else
                            //{
                            //    dt.Columns.Add("商品情報", typeof(string));
                            //}
                            //if (cols.Contains("スマートフォン用商品説明文")) { }
                            //else
                            //{
                            //    dt.Columns.Add("スマートフォン用商品説明文", typeof(string));
                            //}
                            //if (cols.Contains("モバイル用商品説明文")) { }
                            //else
                            //{
                            //    dt.Columns.Add("モバイル用商品説明文", typeof(string));
                            //}
                            //if (cols.Contains("PC用商品説明文")) { }
                            //else
                            //{
                            //    dt.Columns.Add("PC用商品説明文", typeof(string));
                            //}
                            //if (cols.Contains("送料")) { }
                            //else
                            //{
                            //    dt.Columns.Add("送料", typeof(int));
                            //}
                            //if (cols.Contains("スマートテンプレート")) { }
                            //else
                            //{
                            //    dt.Columns.Add("スマートテンプレート", typeof(string));
                            //}
                            //if (cols.Contains("販売価格")) { }
                            //else
                            //{
                            //    dt.Columns.Add("販売価格", typeof(int));
                            //}
                            //if (cols.Contains("ブランド名")) { }
                            //else
                            //{
                            //    dt.Columns.Add("ブランド名", typeof(int));
                            //}
                            //if (cols.Contains("原価")) { }
                            //else
                            //{
                            //    dt.Columns.Add("原価", typeof(int));
                            //}
                            //if (cols.Contains("仕入先名")) { }
                            //else
                            //{
                            //    dt.Columns.Add("仕入先名", typeof(string));
                            //}
                            //if (cols.Contains("定価")) { }
                            //else
                            //{
                            //    dt.Columns.Add("定価", typeof(int));
                            //}
                            //if (cols.Contains("ライブラリ画像１")) { count = 1; }

                            //if (cols.Contains("ライブラリ画像2")) { count += 1; }

                            //if (cols.Contains("ライブラリ画像3")) { count += 1; }

                            //if (cols.Contains("ライブラリ画像4")) { count += 1; }

                            //if (cols.Contains("ライブラリ画像5")) { count += 1; }

                            //if (cols.Contains("ライブラリ画像6")) { count += 1; }
                            //hfcount.Value = count.ToString();
                            //ViewState["DataTable"] = dt;
                            #endregion
                        }//for dataimport
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
            
        private void CopyColumns(DataTable source, DataTable dest, params string[] columns)
        {
            try
            {
                foreach (DataRow sourcerow in source.Rows)
                {

                    DataRow destRow = dest.NewRow();
                    //  foreach (string colname in columns)
                    for (int i = 0; i < columns.Length; i++)
                    {
                        string colname = columns[i];
                        if (!String.IsNullOrWhiteSpace(colname))
                        {
                            if (ContainColumn(colname, source) && ContainColumn(colname, dest))
                            {
                                destRow[colname] = sourcerow[colname];
                            }

                        }
                    }
                    dest.Rows.Add(destRow);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        private bool ContainColumn(string columnName, DataTable table)
        {
            try
            {
                DataColumnCollection columns = table.Columns;

                if (columns.Contains(columnName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

        protected Boolean CheckColumn(string [] colname,DataTable dt)
        {
            try
            {
                DataColumnCollection cols = dt.Columns;
                for (int i = 0; i < colname.Length; i++)
                {
                    if (!cols.Contains(colname[i]))
                    {
                        return false;
                    }
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
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

        private bool checkid(string it) 
        {
            try
            {
                if (it.Contains("0"))
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

        protected void buttonupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnConfirm_Save.Text.Equals("確認画面へ"))
                {

                    btnConfirm_Save.Text = "登録";

                }
                else if (btnConfirm_Save.Text.Equals("更 新"))
                {
                    update();
                    int id = Convert.ToInt32(hfid.Value);
                    hfid.Value = null;
                    Response.Redirect("../Import/Import_Item_Data_Log.aspx?LogID=" + id , false);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void update() 
        {
            try
            {
                ibl = new Import_Item_Data_BL();
                imbl = new Import_Item_BL();//for errorlog
                if (Cache["DataTable"] != null)
                {
                    if (Cache["ErrorlogTable"] != null) { dtlog =Cache["ErrorlogTable"] as DataTable; }
                    DataTable dt = Cache["DataTable"] as DataTable;
                    int counts = dt.Rows.Count;
                        var option = new TransactionOptions
                        {
                            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                            Timeout = TimeSpan.MaxValue
                        };
                        TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                        using (scope)
                        {
                            do
                            {
                                DataTable dterrorlog = dtlog.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                                DataTable dtTemp = dt.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                                imbl.XmlforItem_Import_ItemLog(dterrorlog);

                                ibl.CreateXmlforItemImport(dtTemp);

                                counts = 0;

                                //while (counts < 1000)
                                //{
                                //    if (dtlog.Rows.Count > 0)
                                //        dtlog.Rows.RemoveAt(0);
                                //    else break;
                                //    counts++;
                                //}
                                while (counts < 1000)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        dt.Rows.RemoveAt(0);
                                        dtlog.Rows.RemoveAt(0);
                                    }
                                    else break;
                                    counts++;
                                }
                            } while (dt.Rows.Count > 0);

                            scope.Complete();
                        }
                   
                }//if
                #region LatestImageinsert
                //if (Cache["DTImage"] != null)
                //{
                   
                //    //if (Cache["ErrorlogTable"] != null) { dtlog = Cache["ErrorlogTable"] as DataTable; }
                //    DataTable dts = Cache["DTImage"] as DataTable;
                //    DataTable dtt = new DataTable(); DataTable dt = new DataTable();
                //    dtt.Columns.Add("関連商品", typeof(String));
                //    dtt.Columns.Add("商品番号", typeof(String));
                //    dt = dtt.Copy();
                //    for (int i = 0; i < dts.Rows.Count; i++)
                //    {
                //        for (int k = 0; k < 5; k++)
                //        {
                //            if (!String.IsNullOrWhiteSpace(dts.Rows[i]["関連商品" + (k + 1)].ToString())) 
                //            {
                //                  DataRow destRow =dtt.NewRow();
                //                  dtt.Rows.Add(destRow);
                //                dtt.Rows[k]["商品番号"] = dts.Rows[i]["商品番号"].ToString();
                //                dtt.Rows[k]["関連商品"] = dts.Rows[i]["関連商品" + (k + 1)].ToString();
                //            }
                            
                //        }
                //        dt.Merge(dtt);
                //        dtt.Rows.Clear();
                //    }
                //    int counts = dt.Rows.Count;
                //    var option = new TransactionOptions
                //    {
                //        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                //        Timeout = TimeSpan.MaxValue
                //    };
                //    TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                //    using (scope)
                //    {
                //        do
                //        {
                //            //DataTable dterrorlog = dtlog.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                //            DataTable dtTemp = dt.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                //           // imbl.XmlforItem_Import_ItemLog(dterrorlog);

                //            ibl.XmlforLatestImage(dtTemp);

                //            counts = 0;

                            
                //            while (counts < 1000)
                //            {
                //                if (dt.Rows.Count > 0)
                //                {
                //                    dt.Rows.RemoveAt(0);
                //                   // dtlog.Rows.RemoveAt(0);
                //                }
                //                else break;
                //                counts++;
                //            }
                //        } while (dt.Rows.Count > 0);

                //        scope.Complete();
                //    }
                //}
                #endregion
                #region ImageInsert
                //  DataColumnCollection cols = dt.Columns;
                  ////  string itemid = ibl.SaleDescInsert(dt);

                  //  string itemid= null;
                  //  if (!String.IsNullOrWhiteSpace(itemid) && checkid(itemid) == false)
                  //  {
                  //      if (cols.Contains("ライブラリ画像1") || cols.Contains("ライブラリ画像2") || cols.Contains("ライブラリ画像3") || cols.Contains("ライブラリ画像4") || cols.Contains("ライブラリ画像5") || cols.Contains("ライブラリ画像6"))
                  //      {
                  //          int count = Convert.ToInt32(hfcount.Value);
                  //          string[] id = itemid.Split(',');
                  //          DataColumn newcol = new DataColumn("Item_ID", typeof(int));


                  //          dt.Columns.Add(newcol);
                  //          for (int i = 0; i < id.Length; i++)
                  //          {
                  //              if (!String.IsNullOrWhiteSpace(id[i].ToString()))
                  //              {
                  //                  dt.Rows[i]["Item_ID"] = Convert.ToInt32(id[i].ToString());
                  //              }
                  //          }
                  //          ibl.ImageInsert(dt, count);
                  //      }
                  //  }
                  //  else
                  //  {
                  //      for (int i = 0; i < dt.Rows.Count; i++)
                  //      {

                  //          if (cols.Contains("ライブラリ画像1") || cols.Contains("ライブラリ画像2") || cols.Contains("ライブラリ画像3") || cols.Contains("ライブラリ画像4") || cols.Contains("ライブラリ画像5") || cols.Contains("ライブラリ画像6"))
                  //          {
                  //              DataColumn newcol = new DataColumn("Item_ID", typeof(int));
                  //              newcol.DefaultValue = 0;
                  //              dt.Columns.Add(newcol);
                  //              int count = Convert.ToInt32(hfcount.Value);

                  //              string itemcode = dt.Rows[i]["商品番号"].ToString();



                  //              DataTable dts = ibl.itemid(itemcode);
                  //              int ids = (int)dts.Rows[0]["ID"];
                  //              dt.Rows[i]["Item_ID"] = ids;
                  //              ibl.ImageInsert(dt, count);
                  //              dt.Columns.Remove("Item_ID");
                  //          }
                  //      }
                    //  }
                    #endregion
                #region RakutenPath
                if (Cache["dtRakutenpath"] != null)
                {
                    string ckcode = string.Empty; int itemid = 0; int Itid = 0; int num = 0;
                    DataTable dtImage = Cache["dtRakutenpath"] as DataTable;
                    DataColumn newcol = new DataColumn("Item_ID", typeof(int));
                    newcol.DefaultValue = 0;
                    dtImage.Columns.Add(newcol);
                    //DataColumn newcols = new DataColumn("SN", typeof(int));
                    //newcols.DefaultValue = 1;
                    //dtImage.Columns.Add(newcols);
                    String errormasage = null;

                    for (int k = 0; k < dtImage.Rows.Count; k++)
                    {
                        if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名1"].ToString()) || !String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名2"].ToString()) || !String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名3"].ToString()) || !String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名4"].ToString()) || !String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名5"].ToString()))
                        {
                            string imageid1 = null;
                            string imageid2 = null;
                            string imageid3 = null;
                            string imageid4 = null;
                            string imageid5 = null;
                            if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名1"].ToString()))
                                imageid1 = dtImage.Rows[k]["画像名1"].ToString();
                            if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名2"].ToString()))
                                imageid2 = dtImage.Rows[k]["画像名2"].ToString();
                            if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名3"].ToString()))
                                imageid3 = dtImage.Rows[k]["画像名3"].ToString();
                            if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名4"].ToString()))
                                imageid4 = dtImage.Rows[k]["画像名4"].ToString();
                            if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名5"].ToString()))
                                imageid5 = dtImage.Rows[k]["画像名5"].ToString();

                            if (!String.IsNullOrWhiteSpace(imageid1))
                            {
                                if (Regex.IsMatch(imageid1, "[0-9,a-z\\-]-[1-5].jpg")) { }
                                else { errormasage = "Image_Name Format Incorrect! "; }
                            }
                            if (!String.IsNullOrWhiteSpace(imageid2))
                            {
                                if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名2"].ToString()))
                                    imageid2 = dtImage.Rows[k]["画像名2"].ToString();
                                if (Regex.IsMatch(imageid2, "[0-9,a-z\\-]-[1-5].jpg")) { }
                                else { errormasage = "Image_Name Format Incorrect! "; }
                            }
                            if (!String.IsNullOrWhiteSpace(imageid3))
                            {
                                if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名3"].ToString()))
                                    imageid3 = dtImage.Rows[k]["画像名3"].ToString();
                                if (Regex.IsMatch(imageid3, "[0-9,a-z\\-]-[1-5].jpg")) { }
                                else { errormasage = "Image_Name Format Incorrect! "; }
                            }
                            if (!String.IsNullOrWhiteSpace(imageid4))
                            {
                                if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名4"].ToString()))
                                    imageid4 = dtImage.Rows[k]["画像名4"].ToString();
                                if (Regex.IsMatch(imageid4, "[0-9,a-z\\-]-[1-5].jpg")) { }
                                else { errormasage = "Image_Name Format Incorrect! "; }
                            }
                            if (!String.IsNullOrWhiteSpace(imageid5))
                            {
                                if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名5"].ToString()))
                                    imageid5 = dtImage.Rows[k]["画像名5"].ToString();
                                if (Regex.IsMatch(imageid5, "[0-9,a-z\\-]-[1-5].jpg")) { }
                                else { errormasage = "Image_Name Format Incorrect! "; }
                            }

                        }
                    }

                    if (!String.IsNullOrWhiteSpace(errormasage))
                    {
                        GlobalUI.MessageBox(errormasage);
                    }
                    else
                    {

                        var option = new TransactionOptions
                       {
                           IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                           Timeout = TimeSpan.MaxValue
                       };
                        TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                        using (scope)
                        {
                            do
                            {
                                DataTable dttemp = dtImage.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                                ibl.XmlforRakutenItem_Image(dttemp);
                                int count = 0;
                                while (count < 1000)
                                {
                                    if (dtImage.Rows.Count > 0)
                                    {
                                        dtImage.Rows.RemoveAt(0);

                                    }
                                    else break;
                                    count++;
                                }
                            } while (dtImage.Rows.Count > 0);

                            scope.Complete();
                        }
                    }
                }//else
                #endregion
                #region Image
                if (Cache["dtimage"] != null) 
                {
                    string ckcode = string.Empty; int itemid = 0; int Itid = 0; int num = 0;
                    DataTable dtImage = Cache["dtimage"] as DataTable;
                    DataColumn newcol = new DataColumn("Item_ID", typeof(int));
                    newcol.DefaultValue = 0;
                    dtImage.Columns.Add(newcol);
                    DataColumn newcols = new DataColumn("SN", typeof(int));
                    newcols.DefaultValue = 1;
                    dtImage.Columns.Add(newcols);
                    for (int k = 0; k < dtImage.Rows.Count; k++)
                    {
                        if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["画像名"].ToString())) 
                        {
                            string imageid = dtImage.Rows[k]["画像名"].ToString();
                            string sid= string.Empty;
                            if (imageid.Contains("-") || imageid.Contains("_"))
                            {
                                string line2 = Regex.Replace(imageid, "-", "_");
                                string[] q = line2.Split('_');
                                //for (int g = q.Length - 1; g < q.Length; g++)
                                //{
                                sid = q[q.Length - 1].ToString(); 
                                    //break;
                              //  }
                                q = null;
                                q = sid.Split('.');
                                string fix = q[0];
                               
                                   if( Regex.IsMatch(fix, @"^\d+$"))
                                    dtImage.Rows[k]["SN"] = q[0];
                                
                               
                            }
                        }
                    }
                     var option = new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                        Timeout = TimeSpan.MaxValue
                    };
                    TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                    using (scope)
                    {
                        do
                        {
                            DataTable dttemp = dtImage.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                            ibl.XmlforItem_Image(dttemp);
                        int  count = 0;
                        while (count < 1000)
                        {
                            if (dtImage.Rows.Count > 0)
                            {
                                dtImage.Rows.RemoveAt(0);
                            
                            }
                            else break;
                            count++;
                        }
                        } while (dtImage.Rows.Count > 0);

                        scope.Complete();
                        }

                        #region
                        //DataColumn newcols = new DataColumn("Count", typeof(int));
                        //newcols.DefaultValue = 1;
                        //dtImage.Columns.Add(newcols);
                        //for (int k = 0; k < dtImage.Rows.Count; k++)
                        //{
                        //    string itemcode = dtImage.Rows[k]["商品番号"].ToString();
                        //    if (ckcode == itemcode)
                        //    {
                        //        dtImage.Rows[k]["Item_ID"] = Itid;
                        //        dtImage.Rows[k]["Count"] = (num+1);
                        //        num = num + 1;
                        //    }
                        //    else
                        //    {
                        //        DataTable dtitem = ibl.ckitemcode(itemcode);
                        //        if (dtitem != null && dtitem.Rows.Count > 0)
                        //            itemid =(int) dtitem.Rows[0]["ID"]; 
                        //        dtImage.Rows[k]["Item_ID"] = itemid;
                        //        num = 1;
                        //    }
                        //    ckcode = itemcode; Itid = itemid; 
                        //}
                        #endregion
                     //   ibl.Imagedatainsert(dtImage);
                    Cache.Remove("dtimage");
                    Cache.Remove("DataTableImage");
                    Cache.Remove("DataTable");
                    Cache.Remove("dtRakutenpath");
                }

                    #endregion
                #region Library_Image &Relate Item
                if (Cache["DtLibraryImage"] != null)
                {
                    string ckcode = string.Empty; int itemid = 0; int Itid = 0; int num = 0;
                    DataTable dtImage = Cache["DtLibraryImage"] as DataTable;
                    changecolname(dtImage, "ライブラリ画像");
                    DataColumn newcol = new DataColumn("Item_ID", typeof(int));
                    newcol.DefaultValue = 0;
                    dtImage.Columns.Add(newcol);
                    DataColumn newcols = new DataColumn("SN", typeof(int));
                    newcols.DefaultValue = 1;
                    dtImage.Columns.Add(newcols);
                    DataColumn newcolss = new DataColumn("Shop_ID", typeof(int));
                    newcolss.DefaultValue = 0;
                    dtImage.Columns.Add(newcolss);
                    DataColumn newcolt = new DataColumn("RSN", typeof(int));
                    newcolt.DefaultValue = 1;
                    dtImage.Columns.Add(newcolt);
                    for (int k = 0; k < dtImage.Rows.Count; k++)
                    {
                        if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["Image_Name"].ToString()))
                        {
                            string imageid = dtImage.Rows[k]["Image_Name"].ToString();
                            string sid = string.Empty;
                            if (imageid.Contains("-") || imageid.Contains("_"))
                            {
                                string line2 = Regex.Replace(imageid, "-", "_");
                                string[] q = line2.Split('_');
                                //for (int g = q.Length - 1; g < q.Length; g++)
                                //{
                                sid = q[q.Length - 1].ToString();
                                    //break;
                              //  }
                                q = null;
                                q = sid.Split('.');
                                string fix = q[0];

                                if (Regex.IsMatch(fix, @"^\d+$"))
                                {
                                    dtImage.Rows[k]["SN"] = q[0];
                                    dtImage.Rows[k]["RSN"] = q[0];
                                }


                            }
                        }
                    }
                    var option = new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                       Timeout = TimeSpan.MaxValue
                   };
                    TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                    using (scope)
                    {
                        do
                        {
                            DataTable dttemp = dtImage.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                            ibl.XmlforLibrary_Image(dttemp);
                            int count = 0;
                            while (count < 1000)
                            {
                                if (dtImage.Rows.Count > 0)
                                {
                                    dtImage.Rows.RemoveAt(0);

                                }
                                else break;
                                count++;
                            }
                        } while (dtImage.Rows.Count > 0);

                        scope.Complete();
                    }
                }
                #endregion
                #region  Item_Shop
                if (Cache["dtshop"] != null)
                {
                    DataTable dshop =Cache["dtshop"] as DataTable;
                    DataColumn newcolt = new DataColumn("Shop_ID", typeof(int));
                    newcolt.DefaultValue = 0;
                    dshop.Columns.Add(newcolt);

                    var option = new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                        Timeout = TimeSpan.MaxValue
                    };
                    TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                    using (scope)
                    {
                        do
                        {
                            DataTable dttemp = dshop.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                            ibl.XmlforItem_Shop(dttemp);
                            int count = 0;
                            while (count < 1000)
                            {
                                if (dshop.Rows.Count > 0)
                                {
                                    dshop.Rows.RemoveAt(0);

                                }
                                else break;
                                count++;
                            }
                        } while (dshop.Rows.Count > 0);

                        scope.Complete();
                    }
                }
                #endregion
                #region Library_Image Only
                if (Cache["dtLibImage"] != null)
                {
                    DataTable dtImage = Cache["dtLibImage"] as DataTable;
                    changecolname(dtImage, "ライブラリ画像");
                    DataColumn newcol = new DataColumn("Item_ID", typeof(int));
                    newcol.DefaultValue = 0;
                    dtImage.Columns.Add(newcol);
                    DataColumn newcols = new DataColumn("SN", typeof(int));
                    newcols.DefaultValue = 1;
                    dtImage.Columns.Add(newcols);
                   
                  
                    for (int k = 0; k < dtImage.Rows.Count; k++)
                    {
                        if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["Image_Name"].ToString()))
                        {
                            string imageid = dtImage.Rows[k]["Image_Name"].ToString();
                            string sid = string.Empty;
                            if (imageid.Contains("-") || imageid.Contains("_"))
                            {
                                string line2 = Regex.Replace(imageid, "-", "_");
                                string[] q = line2.Split('_');
                                //for (int g = q.Length - 1; g < q.Length; g++)
                                //{
                                sid = q[q.Length - 1].ToString(); 
                                    //break;
                               // }
                                q = null;
                                q = sid.Split('.');
                                string fix = q[0];

                                if (Regex.IsMatch(fix, @"^\d+$"))
                                {
                                    dtImage.Rows[k]["SN"] = q[0];
                                   
                                }


                            }
                        }
                    }
                    var option = new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                        Timeout = TimeSpan.MaxValue
                    };
                    TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                    using (scope)
                    {
                        do
                        {
                            DataTable dttemp = dtImage.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                            ibl.Library_Image_Only(dttemp);
                            int count = 0;
                            while (count < 1000)
                            {
                                if (dtImage.Rows.Count > 0)
                                {
                                    dtImage.Rows.RemoveAt(0);

                                }
                                else break;
                                count++;
                            }
                        } while (dtImage.Rows.Count > 0);

                        scope.Complete();
                    }
                }
                #endregion
                #region Related_Item Only
                if (Cache["dtrelateitemonly"] != null)
                {
                   
                    DataTable dtImage = Cache["dtrelateitemonly"] as DataTable;
                    
                    DataColumn newcol = new DataColumn("Item_ID", typeof(int));
                    newcol.DefaultValue = 0;
                    dtImage.Columns.Add(newcol);

                    #region for serial NO
                   // DataColumn newcolt = new DataColumn("RSN", typeof(int));
                   // newcolt.DefaultValue = 1;
                   // dtImage.Columns.Add(newcolt);
                   //string namecount = string.Empty;
                   //int RSNcount = 0;
                   // for (int k = 0; k < dtImage.Rows.Count; k++)
                   // {
                         
                   //     if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["商品番号"].ToString()) && !String.IsNullOrWhiteSpace(dtImage.Rows[k]["関連商品"].ToString()))
                   //     {
                   //         string imageid = dtImage.Rows[k]["商品番号"].ToString();
                   //         if (namecount == imageid)
                   //         {
                   //             dtImage.Rows[k]["RSN"] = RSNcount;
                   //             RSNcount += 1;
                   //         }
                   //         else 
                   //         {
                   //             if (RSNcount >5)
                   //             RSNcount = 2;
                   //             else
                   //                 RSNcount = 2;
                   //         }
                   //         namecount = imageid;

                   //     }

                   // }
                    #endregion
                    var option = new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                        Timeout = TimeSpan.MaxValue
                    };
                    TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                    using (scope)
                    {
                        do
                        {
                            DataTable dttemp = dtImage.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                            ibl.RelatedItem_Only(dttemp);
                            int count = 0;
                            while (count < 1000)
                            {
                                if (dtImage.Rows.Count > 0)
                                {
                                    dtImage.Rows.RemoveAt(0);

                                }
                                else break;
                                count++;
                            }
                        } while (dtImage.Rows.Count > 0);

                        scope.Complete();
                    }
                }
                #endregion
                #region Related_Item Only with Ctrl
                if (Cache["dtRTCtrl"] != null)
                {

                    DataTable dtImage = Cache["dtRTCtrl"] as DataTable;

                    DataColumn newcol = new DataColumn("Item_ID", typeof(int));
                    newcol.DefaultValue = 0;
                    dtImage.Columns.Add(newcol);

                    //DataColumn newcolt = new DataColumn("RSN", typeof(int));
                    //newcolt.DefaultValue = 0;
                    //dtImage.Columns.Add(newcolt);
                    //string namecount = string.Empty;
                    //int RSNcount = 0;

                    //for (int i = 0; i < dtImage.Rows.Count; i++)
                    //{
                    //    if (!String.IsNullOrWhiteSpace(dtImage.Rows[i]["商品番号"].ToString()) && !String.IsNullOrWhiteSpace(dtImage.Rows[i]["関連商品"].ToString()) && dtImage.Rows[i]["コントロールカラム"].ToString() == "n")
                    //    {
                    //        String itemCode = dtImage.Rows[i]["商品番号"].ToString();

                    //        DataRow[] dr = dtImage.Select("商品番号='" + itemCode + "' And コントロールカラム='n' And RSN>0");
                    //        dtImage.Rows[i]["RSN"] = dr.Count() + 1;
                    //    }
                    //}

                    #region  for serialNo
                    //for (int k = 0; k < dtImage.Rows.Count; k++)
                    //{

                    //    if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["商品番号"].ToString()) && !String.IsNullOrWhiteSpace(dtImage.Rows[k]["関連商品"].ToString()) && dtImage.Rows[k]["コントロールカラム"].ToString() == "n")
                    //    {
                    //        string imageid = dtImage.Rows[k]["商品番号"].ToString();
                    //        if (namecount == imageid)
                    //        {
                    //            dtImage.Rows[k]["RSN"] = RSNcount;
                    //            RSNcount += 1;
                    //        }
                    //        else
                    //        {
                    //            if (RSNcount > 5)
                    //                RSNcount = 2;
                    //            else
                    //                RSNcount = 2;
                    //        }
                    //        namecount = imageid;

                    //    }

                    //    else if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["商品番号"].ToString()) && !String.IsNullOrWhiteSpace(dtImage.Rows[k]["関連商品"].ToString()) && dtImage.Rows[k]["コントロールカラム"].ToString() == "u")
                    //    {
                    //        string imageid = dtImage.Rows[k]["商品番号"].ToString();
                    //        if (namecount == imageid)
                    //        {
                    //            dtImage.Rows[k]["RSN"] = RSNcount;
                    //            RSNcount += 1;
                    //        }
                    //        else
                    //        {
                    //            if (RSNcount > 5)
                    //                RSNcount = 2;
                    //            else
                    //                RSNcount = 2;
                    //        }
                    //        namecount = imageid;

                    //    }
                    //}
                    #endregion
                    var option = new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                        Timeout = TimeSpan.MaxValue
                    };
                    TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                    using (scope)
                    {
                        do
                        {
                            DataTable dttemp = dtImage.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                            ibl.RelatedItem_OnlyWithCtrl(dttemp);
                            int count = 0;
                            while (count < 1000)
                            {
                                if (dtImage.Rows.Count > 0)
                                {
                                    dtImage.Rows.RemoveAt(0);

                                }
                                else break;
                                count++;
                            }
                        } while (dtImage.Rows.Count > 0);

                        scope.Complete();
                    }
                }
                #endregion
                #region Library_Image Only with Ctrl
                if (Cache["dtLibCtrl"] != null)
                {
                    DataTable dtImage = Cache["dtLibCtrl"] as DataTable;
                    changecolname(dtImage, "ライブラリ画像");
                    DataColumn newcol = new DataColumn("Item_ID", typeof(int));
                    newcol.DefaultValue = 0;
                    dtImage.Columns.Add(newcol);
                    DataColumn newcols = new DataColumn("SN", typeof(int));
                    newcols.DefaultValue = 1;
                    dtImage.Columns.Add(newcols);


                    for (int k = 0; k < dtImage.Rows.Count; k++)
                    {
                        if (!String.IsNullOrWhiteSpace(dtImage.Rows[k]["Image_Name"].ToString()))
                        {
                            string imageid = dtImage.Rows[k]["Image_Name"].ToString();
                            string sid = string.Empty;
                            if (imageid.Contains("-") || imageid.Contains("_"))
                            {
                                string line2 = Regex.Replace(imageid, "-", "_");
                                string[] q = line2.Split('_');
                              
                                sid = q[q.Length - 1].ToString();
                                 
                                q = null;
                                q = sid.Split('.');
                                string fix = q[0];

                                if (Regex.IsMatch(fix, @"^\d+$"))
                                {
                                    dtImage.Rows[k]["SN"] = q[0];

                                }


                            }
                        }
                    }
                    var option = new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                        Timeout = TimeSpan.MaxValue
                    };
                    TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                    using (scope)
                    {
                        do
                        {
                            DataTable dttemp = dtImage.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                            ibl.Library_Image_OnlyWithCtrl(dttemp);
                            int count = 0;
                            while (count < 1000)
                            {
                                if (dtImage.Rows.Count > 0)
                                {
                                    dtImage.Rows.RemoveAt(0);

                                }
                                else break;
                                count++;
                            }
                        } while (dtImage.Rows.Count > 0);

                        scope.Complete();
                    }
                }
                #endregion
                #region  Item_Shop with Ctrl
                if (Cache["dtshopCtrl"] != null)
                {
                    DataTable dshop = Cache["dtshopCtrl"] as DataTable;
                    DataColumn newcolt = new DataColumn("Shop_ID", typeof(int));
                    newcolt.DefaultValue = 0;
                    dshop.Columns.Add(newcolt);

                    var option = new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                        Timeout = TimeSpan.MaxValue
                    };
                    TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                    using (scope)
                    {
                        do
                        {
                            DataTable dttemp = dshop.Rows.Cast<DataRow>().Take(1000).CopyToDataTable();
                            ibl.XmlforItem_ShopWithCtrl(dttemp);
                            int count = 0;
                            while (count < 1000)
                            {
                                if (dshop.Rows.Count > 0)
                                {
                                    dshop.Rows.RemoveAt(0);

                                }
                                else break;
                                count++;
                            }
                        } while (dshop.Rows.Count > 0);

                        scope.Complete();
                    }
                }
                #endregion
                string result = "Update Success";
                    if (result == "Update Success")
                    {
                        object referrer = ViewState["UrlReferrer"];
                        string url = (string)referrer;
                        string script = "window.onload = function(){ alert('";
                        script += result;
                        script += "');";
                        script += "window.location = '";
                        script += url;
                        script += "'; }";
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                    }
                    // GlobalUI.MessageBox("Update Successful!");
                    ViewState["DataTable"] = null;
                    Cache.Remove("DataTable");
                    Cache.Remove("DtLibraryImage");
                    Cache.Remove("dtshop");
                    Cache.Remove("dtrelateitemonly");
                    Cache.Remove("dtLibImage");
                    Cache.Remove("dtRTCtrl");
                    Cache.Remove("dtLibCtrl");
                    Cache.Remove("dtshopCtrl");
              //  }//if
              
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
               
              
            }        
        }

        protected DataTable changecolname(DataTable dt, string colname) 
        {
            try
            {
                dt.Columns["ライブラリ画像"].ColumnName = "Image_Name";
                dt.AcceptChanges();
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }   
            
        }
        protected void gvdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dts = new DataTable();
                if (Cache["dtsource"] != null)
                { dts = Cache["dtsource"] as DataTable; }
                if (Cache["DataTableImage"] != null)
                { dts = Cache["DataTableImage"] as DataTable; }
                if (Cache["DtLibraryImage"] != null)
                { dts = Cache["DtLibraryImage"] as DataTable; }
                if (Cache["dtshop"] != null)
                { dts = Cache["dtshop"] as DataTable; }
                if (Cache["dtrelateitemonly"] != null)
                { dts = Cache["dtrelateitemonly"] as DataTable; }
                if (Cache["dtLibImage"] != null)
                { dts = Cache["dtLibImage"] as DataTable; }
                if (Cache["dtRTCtrl"] != null)
                { dts = Cache["dtRTCtrl"] as DataTable; }
                if (Cache["dtLibCtrl"] != null)
                { dts = Cache["dtLibCtrl"] as DataTable; }
                if (Cache["dtshopCtrl"] != null)
                { dts = Cache["dtshopCtrl"] as DataTable; }
                gvdata.DataSource = dts;
                gvdata.PageIndex = e.NewPageIndex;
                gvdata.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }   

        }

        protected void ImageLog(DataTable dt) 
        {
            try
            {
                imbl = new Import_Item_BL();
                int recordcount = dt.Rows.Count;
                int uid = Userid;
                int importid = itImplog.ImportLogInsert(5, recordcount, 0, uid);
                hfid.Value = Convert.ToString(importid);
                DataTable copydt = dt.Copy();
                DataTable dtErrorlog = ChangeHeader(copydt);


                DataColumn newColumn = new DataColumn("Item_Import_LogID", typeof(System.String));
                newColumn.DefaultValue = importid;
                dtErrorlog.Columns.Add(newColumn);
                imbl.XmlforItem_Import_ItemLog(dtErrorlog);
                gvdata.DataSource = dt;
                gvdata.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }   
        }
    }
}