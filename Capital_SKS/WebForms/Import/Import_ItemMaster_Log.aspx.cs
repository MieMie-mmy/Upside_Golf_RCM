using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Import
{
    public partial class Import_ItemMaster_Log : System.Web.UI.Page
    {
        Item_ImportLog_BL imbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
             {
                try
                {
                    #region Errorlog
                    if (Request.QueryString["ErrorLog_ID"] != null)
                    {
                        if (Request.QueryString["ErrorImportType"] != null)
                        {
                            String importType = Request.QueryString["ErrorImportType"].ToString();
                            int ID = Int32.Parse(Request.QueryString["ErrorLog_ID"].ToString());
                            if (importType.Equals("0"))
                            {
                                BindErrorItemMaster(ID);
                            }
                            else
                            {
                                headermaster.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                                divmaster.Visible = false;
                            }
                            if (importType.Equals("1"))
                            {
                                BindErrorSku(ID);
                            }
                            else
                            {
                                headersku.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                                divsku.Visible = false;
                            }
                            if (importType.Equals("2"))
                            {
                                BindErrorInventory(ID);
                            }
                            else
                            {
                                headerinventory.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                                divinventory.Visible = false;
                            }
                            if (importType.Equals("9"))
                            {
                                BindErrorMonotaro(ID);
                            }
                            else
                            {
                                headermonotaro.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                                divmonotaro.Visible = false;
                            }
                        }
                    }
                    #endregion
                    if (Request.QueryString["Log_ID"] != null)
                    {
                        if (Request.QueryString["ImportType"] != null)
                        {
                            String importType = Request.QueryString["ImportType"].ToString();
                            int ID = Int32.Parse(Request.QueryString["Log_ID"].ToString());
                            if (importType.Equals("0"))
                            {
                                BindItemMaster(ID);
                            }
                            else
                            {
                                headermaster.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                                divmaster.Visible = false;
                            }
                            if (importType.Equals("1"))
                            {
                                BindSku(ID);
                            }
                            else
                            {
                                headersku.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                                divsku.Visible = false;
                            }
                            if (importType.Equals("2"))
                            {
                                BindInventory(ID);
                            }
                            else
                            {
                                headerinventory.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                                divinventory.Visible = false;
                            }
                            if (importType.Equals("9"))
                            {
                                BindMonotaro(ID);
                            }
                            else
                            {
                                headermonotaro.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                                divmonotaro.Visible = false;
                            }
                        }
                    }
                    if (Request.QueryString["Log_IDs"] != null)
                    {
                        String masterID, skuID, InventoryID, Monotaro = String.Empty;
                        String temp = Request.QueryString["Log_IDs"].ToString();
                        String[] logid = temp.Split(',');
                        masterID = logid[0];
                        if (!String.IsNullOrWhiteSpace(masterID))
                            BindItemMaster(Convert.ToInt32(masterID));
                        else
                        {
                            divmaster.Visible = false;
                            headermaster.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                        }
                        skuID = logid[1];
                        if (!String.IsNullOrWhiteSpace(skuID))
                            BindSku(Convert.ToInt32(skuID));
                        else
                        {
                            divsku.Visible = false;
                            headersku.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                        }
                        InventoryID = logid[2];
                        if (!String.IsNullOrWhiteSpace(InventoryID))
                            BindInventory(Convert.ToInt32(InventoryID));
                        else
                        {
                            divinventory.Visible = false;
                            headerinventory.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                        }
                        Monotaro = logid[3];
                        if (!String.IsNullOrWhiteSpace(Monotaro))
                            BindMonotaro(Convert.ToInt32(Monotaro));
                        else
                        {
                            divmonotaro.Visible = false;
                            headermonotaro.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                        }
                    }
                    DataTable dtTagValue = Session["TagValue"] as DataTable;
                    if (dtTagValue != null)
                    {
                        if (dtTagValue.Rows.Count != 0)
                        {
                            gdvTagID.DataSource = dtTagValue;
                            gdvTagID.DataBind();
                        }
                    }
                    else
                    {
                        headertagID.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                        divtagID.Visible = false;
                    }
                }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
          }
        }

        protected void BindItemMaster(int ID)
        {
            try
            {
                imbl = new Item_ImportLog_BL();
                DataTable dt = imbl.ItemMasterLogSelectAll(0, ID);
                String[] coltype = { "Error_Message" };
                DataColumn newcol = new DataColumn("チェック", typeof(String));
                newcol.DefaultValue = "";
                dt.Columns.Add(newcol);
                DataTable dtt = CheckErrorMsg(dt, coltype);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvmaster.DataSource = dtt;
                    gvmaster.DataBind();
                }
                else { gvmaster.DataSource = null; gvmaster.DataBind(); }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void BindSku(int ID)
        {
            try
            {
                imbl = new Item_ImportLog_BL();
                DataTable dts = imbl.ItemMasterLogSelectAll(1, ID);
                String[] coltypes = { "Error_Message" };
                DataColumn newcols = new DataColumn("チェック", typeof(String));
                newcols.DefaultValue = "";
                dts.Columns.Add(newcols);
                DataTable dtr = CheckErrorMsg(dts, coltypes);
                if (dts != null && dts.Rows.Count > 0)
                {
                    gvsku.DataSource = dtr;
                    gvsku.DataBind();
                }
                else { gvsku.DataSource = null; gvsku.DataBind(); }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void BindInventory(int ID)
        {
            try
            {
                imbl = new Item_ImportLog_BL();
                DataTable dtv = imbl.ItemMasterLogSelectAll(2, ID);
                String[] coltypet = { "Error_Message" };
                DataColumn newcolss = new DataColumn("チェック", typeof(String));
                newcolss.DefaultValue = "";
                dtv.Columns.Add(newcolss);
                DataTable dty = CheckErrorMsg(dtv, coltypet);
                if (dtv != null && dtv.Rows.Count > 0)
                {
                    gvInv.DataSource = dty;
                    gvInv.DataBind();
                }
                else { gvInv.DataSource = null; gvInv.DataBind(); }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void BindMonotaro(int ID)
        {
            try
            {
                imbl = new Item_ImportLog_BL();
                DataTable dtv = imbl.ItemMasterLogSelectAll(9, ID);
                String[] coltypet = { "Error_Message" };
                DataColumn newcolss = new DataColumn("チェック", typeof(String));
                newcolss.DefaultValue = "";
                dtv.Columns.Add(newcolss);
                DataTable dty = CheckErrorMsg(dtv, coltypet);
                if (dtv != null && dtv.Rows.Count > 0)
                {
                    gvmonotaro.DataSource = dty;
                    gvmonotaro.DataBind();
                }
                else { gvmonotaro.DataSource = null; gvmonotaro.DataBind(); }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }


        protected DataTable CheckErrorMsg(DataTable dt, String[] col)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < col.Length; j++)
                    {
                        if (!String.IsNullOrWhiteSpace(dt.Rows[i][col[j]].ToString()))
                        {
                            dt.Rows[i]["チェック"] = "エラー";
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

        protected DataTable BindTagID_Error(DataTable dtTagValue)
        {
            imbl = new Item_ImportLog_BL();
            DataTable dtError = imbl.BindTagID_Error();
            for (int i = 0; i < dtTagValue.Rows.Count; i++)
            {
                string Item_Code=string.Empty;
                DataRow[] allErrorMsg;
                if(!String.IsNullOrWhiteSpace(dtTagValue.Rows[i]["Item_Code"].ToString()))
                    allErrorMsg = dtError.Select("Item_Code='" + dtTagValue.Rows[i]["Item_Code"].ToString() + "'AND Color_Name='" + dtTagValue.Rows[i]["Color_Name"].ToString() + "'AND Size_Name='" + dtTagValue.Rows[i]["Size_Name"].ToString() + "'");
                else
                allErrorMsg = dtError.Select("Item_Code IS NULL AND Color_Name='" + dtTagValue.Rows[i]["Color_Name"].ToString() + "'AND Size_Name='" + dtTagValue.Rows[i]["Size_Name"].ToString() + "'");
                dtTagValue.Rows[i]["Error_Message"] = allErrorMsg[0]["ErrorMessage"].ToString();
            }
            dtTagValue.AcceptChanges();
            return dtTagValue;
        }

        protected void gvmaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Error_Message").ToString() != null)
                {
                    e.Row.Cells[17].Style.Add("color", "red");
                }
                if (DataBinder.Eval(e.Row.DataItem, "チェック").ToString() != null)
                {
                    e.Row.Cells[1].Style.Add("color", "red");
                }
            }
        }

        protected void gvsku_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "Error_Message").ToString() != null)
                    {
                        e.Row.Cells[8].Style.Add("color", "red");
                    }
                    if (DataBinder.Eval(e.Row.DataItem, "チェック").ToString() != null)
                    {
                        e.Row.Cells[0].Style.Add("color", "red");
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvInv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "Error_Message").ToString() != null)
                    {
                        e.Row.Cells[14].Style.Add("color", "red");
                    }
                    if (DataBinder.Eval(e.Row.DataItem, "チェック").ToString() != null)
                    {
                        e.Row.Cells[0].Style.Add("color", "red");
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvMon_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "Error_Message").ToString() != null)
                    {
                        e.Row.Cells[3].Style.Add("color", "red");
                    }
                    if (DataBinder.Eval(e.Row.DataItem, "チェック").ToString() != null)
                    {
                        e.Row.Cells[0].Style.Add("color", "red");
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvmaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                imbl = new Item_ImportLog_BL();
                if (Request.QueryString["ErrorLog_ID"] != null)
                {
                    int ID = Int32.Parse(Request.QueryString["ErrorLog_ID"].ToString());
                    DataTable dt = imbl.ItemErrotLogSelectAll(0, ID);
                    String[] coltype = { "Error_Message" };
                    DataColumn newcol = new DataColumn("チェック", typeof(String));
                    newcol.DefaultValue = "";
                    dt.Columns.Add(newcol);
                    DataTable dtt = CheckErrorMsg(dt, coltype);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gvmaster.DataSource = dtt;
                        gvmaster.PageIndex = e.NewPageIndex;
                        gvmaster.DataBind();
                    }
                }
                if (Request.QueryString["Log_ID"] != null)
                {
                    int ID = Int32.Parse(Request.QueryString["Log_ID"].ToString());
                    DataTable dt = imbl.ItemMasterLogSelectAll(0, ID);
                    String[] coltype = { "Error_Message" };
                    DataColumn newcol = new DataColumn("チェック", typeof(String));
                    newcol.DefaultValue = "";
                    dt.Columns.Add(newcol);
                    DataTable dtt = CheckErrorMsg(dt, coltype);
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        gvmaster.DataSource = dtt;
                        gvmaster.PageIndex = e.NewPageIndex;
                        gvmaster.DataBind();
                    }
                }//
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvsku_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                imbl = new Item_ImportLog_BL();
                if (Request.QueryString["ErrorLog_ID"] != null)
                {
                    int ID = Int32.Parse(Request.QueryString["ErrorLog_ID"].ToString());
                    DataTable dts = imbl.ItemErrotLogSelectAll(1, ID);
                    String[] coltypes = { "Error_Message" };
                    DataColumn newcols = new DataColumn("チェック", typeof(String));
                    newcols.DefaultValue = "";
                    dts.Columns.Add(newcols);
                    DataTable dtr = CheckErrorMsg(dts, coltypes);
                    if (dts != null && dts.Rows.Count > 0)
                    {
                        gvsku.DataSource = dtr;
                        gvsku.PageIndex = e.NewPageIndex;
                        gvsku.DataBind();
                    }
                }

                if (Request.QueryString["Log_ID"] != null)
                {
                    int ID = Int32.Parse(Request.QueryString["Log_ID"].ToString());
                    DataTable dts = imbl.ItemMasterLogSelectAll(1, ID);
                    String[] coltypes = { "Error_Message" };
                    DataColumn newcols = new DataColumn("チェック", typeof(String));
                    newcols.DefaultValue = "";
                    dts.Columns.Add(newcols);
                    DataTable dtr = CheckErrorMsg(dts, coltypes);
                    if (dts != null && dts.Rows.Count > 0)
                    {
                        gvsku.DataSource = dtr;
                        gvsku.PageIndex = e.NewPageIndex;
                        gvsku.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                imbl = new Item_ImportLog_BL();
                if (Request.QueryString["ErrorLog_ID"] != null)
                {
                    int ID = Int32.Parse(Request.QueryString["ErrorLog_ID"].ToString());
                    DataTable dtv = imbl.ItemErrotLogSelectAll(2, ID);
                    String[] coltypet = { "Error_Message" };
                    DataColumn newcolss = new DataColumn("チェック", typeof(String));
                    newcolss.DefaultValue = "";
                    dtv.Columns.Add(newcolss);
                    DataTable dty = CheckErrorMsg(dtv, coltypet);
                    if (dtv != null && dtv.Rows.Count > 0)
                    {
                        gvInv.DataSource = dty;
                        gvInv.PageIndex = e.NewPageIndex;
                        gvInv.DataBind();
                    }
                }

                if (Request.QueryString["Log_ID"] != null)
                {
                    int ID = Int32.Parse(Request.QueryString["Log_ID"].ToString());
                    DataTable dtv = imbl.ItemMasterLogSelectAll(2, ID);
                    String[] coltypet = { "Error_Message" };
                    DataColumn newcolss = new DataColumn("チェック", typeof(String));
                    newcolss.DefaultValue = "";
                    dtv.Columns.Add(newcolss);
                    DataTable dty = CheckErrorMsg(dtv, coltypet);
                    if (dtv != null && dtv.Rows.Count > 0)
                    {
                        gvInv.DataSource = dty;
                        gvInv.PageIndex = e.NewPageIndex;
                        gvInv.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvMon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                imbl = new Item_ImportLog_BL();
                if (Request.QueryString["ErrorLog_ID"] != null)
                {
                    int ID = Int32.Parse(Request.QueryString["ErrorLog_ID"].ToString());
                    DataTable dtv = imbl.ItemErrotLogSelectAll(9, ID);
                    String[] coltypet = { "Error_Message" };
                    DataColumn newcolss = new DataColumn("チェック", typeof(String));
                    newcolss.DefaultValue = "";
                    dtv.Columns.Add(newcolss);
                    DataTable dty = CheckErrorMsg(dtv, coltypet);
                    if (dtv != null && dtv.Rows.Count > 0)
                    {
                        gvmonotaro.DataSource = dty;
                        gvmonotaro.PageIndex = e.NewPageIndex;
                        gvmonotaro.DataBind();
                    }
                }

                if (Request.QueryString["Log_ID"] != null)
                {
                    int ID = Int32.Parse(Request.QueryString["Log_ID"].ToString());
                    DataTable dtv = imbl.ItemMasterLogSelectAll(9, ID);
                    String[] coltypet = { "Error_Message" };
                    DataColumn newcolss = new DataColumn("チェック", typeof(String));
                    newcolss.DefaultValue = "";
                    dtv.Columns.Add(newcolss);
                    DataTable dty = CheckErrorMsg(dtv, coltypet);
                    if (dtv != null && dtv.Rows.Count > 0)
                    {
                        gvmonotaro.DataSource = dty;
                        gvmonotaro.PageIndex = e.NewPageIndex;
                        gvmonotaro.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        #region For errorlog
        protected void BindErrorItemMaster(int ID)
        {
            try
            {
                imbl = new Item_ImportLog_BL();
                DataTable dt = imbl.ItemErrotLogSelectAll(0, ID);
                String[] coltype = { "Error_Message" };
                DataColumn newcol = new DataColumn("チェック", typeof(String));
                newcol.DefaultValue = "";
                dt.Columns.Add(newcol);
                DataTable dtt = CheckErrorMsg(dt, coltype);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvmaster.DataSource = dtt;
                    gvmaster.DataBind();
                }
                else { gvmaster.DataSource = null; gvmaster.DataBind(); }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void BindErrorSku(int ID)
        {
            try
            {
                imbl = new Item_ImportLog_BL();
                DataTable dts = imbl.ItemErrotLogSelectAll(1, ID);
                String[] coltypes = { "Error_Message" };
                DataColumn newcols = new DataColumn("チェック", typeof(String));
                newcols.DefaultValue = "";
                dts.Columns.Add(newcols);
                DataTable dtr = CheckErrorMsg(dts, coltypes);
                if (dts != null && dts.Rows.Count > 0)
                {
                    gvsku.DataSource = dtr;
                    gvsku.DataBind();
                }
                else { gvsku.DataSource = null; gvsku.DataBind(); }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void BindErrorInventory(int ID)
        {
            try
            {
                imbl = new Item_ImportLog_BL();
                DataTable dtv = imbl.ItemErrotLogSelectAll(2, ID);
                String[] coltypet = { "Error_Message" };
                DataColumn newcolss = new DataColumn("チェック", typeof(String));
                newcolss.DefaultValue = "";
                dtv.Columns.Add(newcolss);
                DataTable dty = CheckErrorMsg(dtv, coltypet);
                if (dtv != null && dtv.Rows.Count > 0)
                {
                    gvInv.DataSource = dty;
                    gvInv.DataBind();
                }
                else { gvInv.DataSource = null; gvInv.DataBind(); }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }


        protected void BindErrorMonotaro(int ID)
        {
            try
            {
                imbl = new Item_ImportLog_BL();
                DataTable dtv = imbl.ItemErrotLogSelectAll(9, ID);
                String[] coltypet = { "Error_Message" };
                DataColumn newcolss = new DataColumn("チェック", typeof(String));
                newcolss.DefaultValue = "";
                dtv.Columns.Add(newcolss);
                DataTable dty = CheckErrorMsg(dtv, coltypet);
                if (dtv != null && dtv.Rows.Count > 0)
                {
                    gvmonotaro.DataSource = dty;
                    gvmonotaro.DataBind();
                }
                else { gvmonotaro.DataSource = null; gvmonotaro.DataBind(); }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        #endregion

        protected void gdvTagID_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (Session["TagValue"] != null)
                {
                    DataTable dtTagValue = Session["TagValue"] as DataTable;
                    gdvTagID.DataSource = dtTagValue;
                    gdvTagID.PageIndex = e.NewPageIndex;
                    gdvTagID.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(),"jscript", "ScrollToTop();", true);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        //protected void gridtagIDErrorMsg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        imbl = new Item_ImportLog_BL();
        //        imbl = new Item_ImportLog_BL();

        //        if (Session["TagIDFlag"] != null)
        //        {
        //            DataTable dtError = Session["TagIDFlag"] as DataTable;
        //            gridtagIDErrorMsg.DataSource = dtError;
        //            gridtagIDErrorMsg.PageIndex = e.NewPageIndex;
        //            gridtagIDErrorMsg.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["Exception"] = ex.ToString();
        //        Response.Redirect("~/CustomErrorPage.aspx?");
        //    }
        //}

    }
}