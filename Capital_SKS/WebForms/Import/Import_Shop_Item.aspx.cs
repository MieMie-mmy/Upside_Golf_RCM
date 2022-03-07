using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.VisualBasic.FileIO;
using ORS_RCM_BL;
using System.Collections;

namespace ORS_RCM.WebForms.Import
{
    public partial class Shop_Import : System.Web.UI.Page
    {
        public static DateTime dtime = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Import_Shop_ItemBL isbl = new Import_Shop_ItemBL();
                    lbShop.DataSource = isbl.GetAllShop();
                    lbShop.DataTextField = "Shop_Description";
                    lbShop.DataValueField = "ID";
                    lbShop.DataBind();
                } 
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }  
        }

        protected Boolean CheckFile(String[] validFileTypes,FileUpload upl)
        {
            try
            {
                string ext = System.IO.Path.GetExtension(upl.PostedFile.FileName);
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

        public String DataTableToXML(DataTable dtdata)
        {
            try
            {
                dtdata.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dtdata.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();
                return result;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
        }

        protected ArrayList GetSelectedItems()
        {
            try
            {
                ArrayList arrItem = new ArrayList();
                foreach (ListItem li in lbShop.Items)
                {
                    if (li.Selected == true)
                    {
                        // get the value of the item in your loop
                        arrItem.Add(li.Text + "," + li.Value);
                    }
                }
                return arrItem;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new ArrayList();
            }
        }

        protected DataTable RemoveColumn(DataTable dt,String[] col)
        {
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Boolean exist = false;
                    for (int j = 0; j < col.Count(); j++)
                    {
                        if (dt.Columns[i].ColumnName.ToString().Equals(col[j]))
                        {
                            exist = true;
                            break;
                        }
                    }
                    if (!exist)
                    {
                        dt.Columns.Remove(dt.Columns[i]);
                        i--;
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

        protected void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                Import_Shop_ItemBL isbl = new Import_Shop_ItemBL();
                ArrayList arrlst = GetSelectedItems();
                if (arrlst.Count > 0)
                {

                    if (uplShopItem.HasFile)
                    {
                        String[] validFileTypes = { "csv" };
                        if (CheckFile(validFileTypes, uplShopItem))
                        {
                            uplShopItem.SaveAs(Server.MapPath("~/Import_CSV/" + uplShopItem.FileName));
                            String path = Server.MapPath("~/Import_CSV/") + uplShopItem.FileName;
                            DataTable dt = GlobalUI.CSVToTable(path);
                            dt = GlobalUI.Remove_Doublecode(dt);

                            String[] str = arrlst[0].ToString().Split(',');
                            String shopid = str[2];

                            Import_Shop_ItemBL isibl = new Import_Shop_ItemBL();
                            DataTable dtMallID = isibl.GetMallByShopID(shopid);
                            if (dtMallID.Rows.Count > 0)
                            {
                                String Mall = dtMallID.Rows[0]["Mall_ID"].ToString();

                                //Rakuten　楽天
                                if (Mall.ToLower().Equals("1"))
                                {
                                    String[] colName = { "商品番号", "商品名", "コントロールカラム", "商品管理番号（商品URL）", "ポイント変倍率", "ポイント変倍率適用期間" };
                                    if (CheckColumn(colName, dt))
                                    {
                                        dt = RemoveColumn(dt, colName);
                                        Session["shopitem"] = dt;
                                        gvShopItem.DataSource = dt;
                                        gvShopItem.DataBind();
                                    }
                                    else
                                    {
                                        ShowMsg("Shop Item File format wrong!");
                                    }
                                }

                                //Ponpare ポンパール
                                if (Mall.ToLower().Equals("3"))
                                {
                                    String[] colName = { "商品ID", "商品名", "コントロールカラム", "商品管理ID（商品URL）", "ポイント率", "ポイント率適用期間" };

                                    if (CheckColumn(colName, dt))
                                    {
                                        dt = RemoveColumn(dt, colName);
                                        dt.Columns["商品ID"].ColumnName = "商品番号";
                                        dt.Columns["商品管理ID（商品URL）"].ColumnName = "商品管理番号（商品URL）";
                                        dt.Columns["ポイント率"].ColumnName = "ポイント変倍率";
                                        dt.Columns["ポイント率適用期間"].ColumnName = "ポイント変倍率適用期間";

                                        ViewState["shopitem"] = dt;
                                        gvShopItem.DataSource = dt;
                                        gvShopItem.DataBind();

                                        Label lbl = gvShopItem.HeaderRow.FindControl("lblHdItemCode") as Label;
                                        lbl.Text = "商品ID";

                                        lbl = gvShopItem.HeaderRow.FindControl("lblHDItemAdminCode") as Label;
                                        lbl.Text = "商品管理ID（商品URL）";

                                        lbl = gvShopItem.HeaderRow.FindControl("lblHdPoint") as Label;
                                        lbl.Text = "ポイント率";

                                        lbl = gvShopItem.HeaderRow.FindControl("lblHdPointTerm") as Label;
                                        lbl.Text = "ポイント率適用期間";
                                    }
                                    else
                                    {
                                        ShowMsg("Shop Item File format wrong!");
                                    }
                                }

                                //Yahoo
                                if (Mall.ToLower().Equals("2"))
                                {
                                    String[] colName = { "name", "code", "point-code", "temporary-point-term" };

                                    if (CheckColumn(colName, dt))
                                    {
                                        dt = RemoveColumn(dt, colName);

                                        dt.Columns["code"].ColumnName = "商品番号";
                                        dt.Columns["name"].ColumnName = "商品名";

                                        DataColumn dc = new DataColumn("商品管理番号（商品URL）", typeof(String));
                                        dt.Columns.Add(dc);

                                        dc = new DataColumn("コントロールカラム", typeof(String));
                                        dt.Columns.Add(dc);

                                        dt.Columns["point-code"].ColumnName = "ポイント変倍率";
                                        dt.Columns["temporary-point-term"].ColumnName = "ポイント変倍率適用期間";

                                        Session["shopitem"] = dt;
                                        gvShopItem.DataSource = dt;
                                        gvShopItem.DataBind();

                                        Label lbl = gvShopItem.HeaderRow.FindControl("lblHdItemCode") as Label;
                                        lbl.Text = "code";

                                        lbl = gvShopItem.HeaderRow.FindControl("lblHdItemName") as Label;
                                        lbl.Text = "code";

                                        lbl = gvShopItem.HeaderRow.FindControl("lblHdPoint") as Label;
                                        lbl.Text = "point-code";

                                        lbl = gvShopItem.HeaderRow.FindControl("lblHdPointTerm") as Label;
                                        lbl.Text = "temporary-point-term";

                                        gvShopItem.Columns[0].Visible = false;
                                        gvShopItem.Columns[3].Visible = false;
                                    }
                                    else
                                    {
                                        ShowMsg("Shop Item File format wrong!");
                                    }
                                }
                            }
                        }
                    }

                    if (uplShopCategory.HasFile)
                    {
                        String[] validFileTypes = { "csv" };
                        if (CheckFile(validFileTypes, uplShopCategory))
                        {
                            uplShopCategory.SaveAs(Server.MapPath("~/Import_CSV/" + uplShopCategory.FileName));
                            String path = Server.MapPath("~/Import_CSV/") + uplShopCategory.FileName;
                            DataTable dt = GlobalUI.CSVToTable(path);
                            dt = GlobalUI.Remove_Doublecode(dt);
                            String[] colName = { "コントロールカラム", "商品管理番号（商品URL）", "商品名", "表示先カテゴリ", "優先度", "URL", "1ページ複数形式", "カテゴリセット管理番号", "カテゴリセット名" };
                            if (CheckColumn(colName, dt))
                            {
                                ViewState["shopCategory"] = dt;
                                gvShopCategory.DataSource = dt;
                                gvShopCategory.DataBind();
                            }
                            else
                            {
                                ShowMsg("Shop Category File format wrong!");
                            }
                        }
                    }

                    if (uplShopInvertory.HasFile)
                    {
                        String[] validFileTypes = { "csv" };
                        if (CheckFile(validFileTypes, uplShopInvertory))
                        {
                            uplShopInvertory.SaveAs(Server.MapPath("~/Import_CSV/" + uplShopInvertory.FileName));
                            String path = Server.MapPath("~/Import_CSV/") + uplShopInvertory.FileName;
                            DataTable dt = GlobalUI.CSVToTable(path);
                            dt = GlobalUI.Remove_Doublecode(dt);
                            String[] colName = { "項目選択肢用コントロールカラム", "商品管理番号（商品URL）", "項目選択肢別在庫用在庫数" };
                            if (CheckColumn(colName, dt))
                            {
                                ViewState["shopInventory"] = dt;
                                gvShopInventory.DataSource = dt;
                                gvShopInventory.DataBind();
                            }
                            else
                            {
                                ShowMsg("Shop Inverntory File format wrong!");
                            }
                        }
                    }
                }
                else
                {
                    ShowMsg("Select at least One Shop!");
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
                Import_Shop_ItemBL isbl = new Import_Shop_ItemBL();
                ArrayList arrlst = GetSelectedItems();
                if (arrlst.Count > 0)
                {
                    DataTable dtShopItem = new DataTable();
                    DataTable dtShopCategory = new DataTable();
                    DataTable dtShopInventory = new DataTable();

                    if (Session["shopitem"] != null)
                        dtShopItem = Session["shopitem"] as DataTable;

                    Session.Remove("shopitem");

                    if (ViewState["shopCategory"] != null)
                        dtShopCategory = ViewState["shopCategory"] as DataTable;

                    if (ViewState["shopInventory"] != null)
                        dtShopInventory = ViewState["shopInventory"] as DataTable;
                    for (int i = 0; i < arrlst.Count; i++)
                    {
                        String[] str = arrlst[i].ToString().Split(',');
                        String shopid = str[2];
                        if (dtShopItem.Rows.Count > 0)
                        {
                            DataTable dt = isbl.checkShop(shopid);
                            if (dt.Rows.Count > 0)
                            {
                                isbl.DeleteRakutenBackup(Convert.ToInt32(shopid));
                                //String xml = DataTableToXML(dt);
                                //isbl.InsertRakutenItem(xml);
                                isbl.DeleteRakutenData(shopid,dtime);
                            }

                            isbl.InsertRakutenData(dtShopItem, shopid,dtime);
                        }

                        if (dtShopCategory.Rows.Count > 0)
                        {
                            DataTable dt = isbl.checkCategory(shopid);
                            if (dt.Rows.Count > 0)
                            {
                                isbl.DeleteCategoryBackup(Convert.ToInt32(shopid));
                                //String xml = DataTableToXML(dt);
                                //isbl.InsertShopCategory(xml);
                                isbl.DeleteCategoryData(shopid,dtime);
                            }
                            isbl.InsertCategoryData(dtShopCategory, shopid,dtime);
                        }

                        if (dtShopInventory.Rows.Count > 0)
                        {
                            DataTable dt = isbl.checkInventory(shopid);
                            if (dt.Rows.Count > 0)
                            {
                                isbl.DeleteInventoryBackup(Convert.ToInt32(shopid));
                                //String xml = DataTableToXML(dt);
                                //isbl.InsertInventory(xml);
                                isbl.DeleteInventoryData(shopid,dtime);
                            }
                            isbl.InsertInventoryData(dtShopInventory, shopid,dtime);
                        }
                    }
                    Session.Remove("shopitem");
                    ViewState.Remove("shopCategory");
                    ViewState.Remove("shopInventory");

                    gvShopItem.DataSource = null;
                    gvShopItem.DataBind();

                    gvShopCategory.DataSource = null;
                    gvShopCategory.DataBind();

                    gvShopInventory.DataSource = null;
                    gvShopInventory.DataBind();

                    ShowMsg("Sucessfully Import!");
                }
                else
                {
                    ShowMsg("Select at least One Shop!");
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvShopItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
    }
}