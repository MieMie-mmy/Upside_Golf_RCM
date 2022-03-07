using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.IO;
using System.Data;

namespace ORS_RCM
{
    public partial class Jisha_Import : System.Web.UI.Page
    {
        Jisha_Import_BL JishaBL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
            
            }
        }

        protected void btnRead_Click(object sender, EventArgs e)
        {
            //local variables
            string filename;
            string filepath;
            DataTable dt;

            #region Jisha Item Master

            if (fuItem_Master.HasFile)
            {
                 filename = Path.GetFileName(fuItem_Master.PostedFile.FileName);
                if (filename.Contains(".csv"))
                {
                    filepath = Server.MapPath("~/Import_CSV") + filename;

                    fuItem_Master.SaveAs(Server.MapPath("~/Import_CSV") + filename);

                     dt = GlobalUI.CSVToTable(filepath);
                     dt = GlobalUI.Remove_Doublecode(dt);

                    String[] ColumnName = { "コントロールカラム","商品管理番号（商品URL）","商品番号",
                                              "全商品ディレクトリID","タグID","PC用キャッチコピー",
                                              "モバイル用キャッチコピー","商品名","販売価格",
                                              "表示価格","消費税","送料",
                                              "個別送料","送料区分1","送料区分2",
                                              "代引料","倉庫指定","商品情報レイアウト",
                                              "注文ボタン","資料請求ボタン","商品問い合わせボタン",
                                              "再入荷お知らせボタン","モバイル表示","のし対応",
                                              "PC用商品説明文","モバイル用商品説明文","スマートフォン用商品説明文",
                                              "PC用販売説明文","商品画像URL","商品画像名（ALT）",
                                              "動画","販売期間指定","注文受付数",
                                              "在庫タイプ","在庫数","在庫数表示",
                                              "項目選択肢別在庫用横軸項目名","項目選択肢別在庫用縦軸項目名","項目選択肢別在庫用残り表示閾値",
                                              "RAC番号","サーチ非表示","闇市パスワード",
                                              "カタログID","在庫戻しフラグ","在庫切れ時の注文受付",
                                              "在庫あり時納期管理番号","在庫切れ時納期管理番号","予約商品発売日",
                                              "ポイント変倍率","ポイント変倍率適用期間","ヘッダー・フッター・レフトナビ",
                                              "表示項目の並び順","共通説明文（小）","目玉商品",
                                              "共通説明文（大）","レビュー本文表示","あす楽配送管理番号",
                                              "サイズ表リンク","二重価格文言管理番号"
                                          };
                    if (CheckColumn(ColumnName, dt))
                    {
                        gv_Jisha_Item_Master.DataSource = dt;
                        ViewState["dt_Jisha_Item_Master"] = dt;
                        gv_Jisha_Item_Master.DataBind();
                    }
                    else
                    {
                        GlobalUI.MessageBox("File Format Wrong for Item Master!");
                    }
                }
            }

            #endregion

            #region Jisha Item 

            if (fuItem.HasFile)
            {
                filename = Path.GetFileName(fuItem.PostedFile.FileName);
                if (filename.Contains(".csv"))
                {
                    filepath = Server.MapPath("~/Import_CSV") + filename;

                    fuItem.SaveAs(Server.MapPath("~/Import_CSV") + filename);

                    dt = GlobalUI.CSVToTable(filepath);
                    dt = GlobalUI.Remove_Doublecode(dt);

                    String[] ColumnName = { 
                                              "項目選択肢用コントロールカラム","商品管理番号（商品URL）","選択肢タイプ",
                                              "Select/Checkbox用項目名","Select/Checkbox用選択肢","項目選択肢別在庫用横軸選択肢",
                                              "項目選択肢別在庫用横軸選択肢子番号","項目選択肢別在庫用縦軸選択肢","項目選択肢別在庫用縦軸選択肢子番号",
                                              "項目選択肢別在庫用取り寄せ可能表示","項目選択肢別在庫用在庫数","在庫戻しフラグ",
                                              "在庫切れ時の注文受付","在庫あり時納期管理番号","在庫切れ時納期管理番号"
                                          };
                    if (CheckColumn(ColumnName, dt))
                    {
                        gv_Jisha_Item.DataSource = dt;
                        ViewState["dt_Jisha_Item"] = dt;
                        gv_Jisha_Item.DataBind();
                    }
                    else
                    {
                        GlobalUI.MessageBox("File Format Wrong for Item!");
                    }
                }
            }

            #endregion

            #region Jisha Item Category

            if (fuItem_Category.HasFile)
            {
                filename = Path.GetFileName(fuItem_Category.PostedFile.FileName);
                if (filename.Contains(".csv"))
                {
                    filepath = Server.MapPath("~/Import_CSV") + filename;

                    fuItem_Category.SaveAs(Server.MapPath("~/Import_CSV") + filename);

                    dt = GlobalUI.CSVToTable(filepath);

                    dt = GlobalUI.Remove_Doublecode(dt);

                    String[] ColumnName = { 
                                             "コントロールカラム","商品管理番号（商品URL）","表示先カテゴリ",
                                             "優先度","カテゴリセット管理番号","カテゴリセット名"
                                          };
                    if (CheckColumn(ColumnName, dt))
                    {
                        gv_Jisha_Item_Category.DataSource = dt;
                        ViewState["dt_Jisha_Item_Category"] = dt;
                        gv_Jisha_Item_Category.DataBind();
                    }
                    else
                    {
                        GlobalUI.MessageBox("File Format Wrong for Item Category!");
                    }
                }
            }

            #endregion
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            JishaBL = new Jisha_Import_BL();

            #region Jisha Item Master

            if (ViewState["dt_Jisha_Item_Master"] != null)
            {
                dt = ViewState["dt_Jisha_Item_Master"] as DataTable;

                JishaBL.Jisha_Item_Master_Xml(ChangeJishaItemMasterHeader(dt));

                GlobalUI.MessageBox("Save Successful!");

                ViewState["dt_Jisha_Item_Master"] = null;
                gv_Jisha_Item_Master.DataSource = null;
                gv_Jisha_Item_Master.DataBind();
            }

            #endregion

            #region Jisha Item

            if (ViewState["dt_Jisha_Item"] != null)
            {
                dt = ViewState["dt_Jisha_Item"] as DataTable;

                JishaBL.Jisha_Item_Xml(ChangeJishaItemHeader(dt));

                GlobalUI.MessageBox("Save Successful!");

                ViewState["dt_Jisha_Item"] = null;
                gv_Jisha_Item.DataSource = null;
                gv_Jisha_Item.DataBind();
            }
           

            #endregion

            #region Jisha Item Category

            if (ViewState["dt_Jisha_Item_Category"] != null)
            {
                dt = ViewState["dt_Jisha_Item_Category"] as DataTable;

                JishaBL.Jisha_Item_Category_Xml(ChangeJishaItemCategoryHeader(dt));

                GlobalUI.MessageBox("Save Successful!");

                ViewState["dt_Jisha_Item_Category"] = null;
                gv_Jisha_Item_Category.DataSource = null;
                gv_Jisha_Item_Category.DataBind();
            }
            
            #endregion
        }

        protected Boolean CheckColumn(String[] colName, DataTable dt)
        {
            DataColumnCollection col = dt.Columns;
            for (int i = 0; i < colName.Length; i++)
            {
                if (!col.Contains(colName[i]))
                    return false;
            }
            return true;
        }

        protected DataTable ChangeJishaItemMasterHeader(DataTable dt)
        {
            dt.Columns["商品管理番号（商品URL）"].ColumnName = "Item_Code_URL";
            dt.Columns["商品画像名（ALT）"].ColumnName = "Item_Image_Name";
            dt.Columns["ヘッダー・フッター・レフトナビ"].ColumnName = "Header_Footer";
            dt.Columns["共通説明文（小）"].ColumnName = "Common_Description1";
            dt.Columns["共通説明文（大）"].ColumnName = "Common_Description2";
            return dt;
        }

        protected DataTable ChangeJishaItemHeader(DataTable dt)
        {
            dt.Columns["商品管理番号（商品URL）"].ColumnName = "Item_Code";
            dt.Columns["Select/Checkbox用選択肢"].ColumnName = "Option_Value";
            dt.Columns["Select/Checkbox用項目名"].ColumnName = "Option_Name";
            return dt;
        }

        protected DataTable ChangeJishaItemCategoryHeader(DataTable dt)
        {
            dt.Columns["商品管理番号（商品URL）"].ColumnName = "Item_Code";
            return dt;
        }

    }
}