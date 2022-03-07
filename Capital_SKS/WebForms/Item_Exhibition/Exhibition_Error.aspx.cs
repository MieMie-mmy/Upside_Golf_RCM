using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;
using System.Drawing;

namespace ORS_RCM.WebForms.Item_Exhibition
{
    public partial class Exhibition_Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
           {
            if (!IsPostBack)
            {
                Exhibition_Error_BL exErrbl = new Exhibition_Error_BL();
                DataTable dt = exErrbl.SelectExhibitionError();
                DataTable dtExhibitInfo = exErrbl.SelectExhibitionInfo();
                GridBind(dt);
               
                //if (dt != null )
                //{
                //    gp.TotalRecord = dt.Rows.Count;
                //    gp.OnePageRecord = gvExhibitionError.PageSize;

                //    int index1 = 0;
                //    gp.sendIndexToThePage += delegate(int index)
                //    {
                //        index1 = index;
                //    };
                //    gvExhibitionError.PageIndex = index1;

                //    gvExhibitionError.DataSource = dt;
                //    gvExhibitionError.DataBind();
                //}
                if (dtExhibitInfo != null)
                {
                    gdvExhibitInfo.DataSource = dtExhibitInfo;
                    gdvExhibitInfo.DataBind();
                }
               
            }
            else
            {
                Exhibition_Error_BL exErrbl = new Exhibition_Error_BL();
                DataTable dt = exErrbl.SelectExhibitionError();

                if (dt.Rows.Count > 0)
                {
                    gp.TotalRecord = dt.Rows.Count;
                    gp.OnePageRecord = gvExhibitionError.PageSize;

                    gp.sendIndexToThePage += delegate(int index)
                    {
                        gvExhibitionError.PageSize = gp.OnePageRecord;
                        gvExhibitionError.PageIndex = Convert.ToInt32(index);
                        gvExhibitionError.DataSource = dt;
                        gvExhibitionError.DataBind();
                    };
                }                
            }
           }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvExhibitionError_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          try
           {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lbl = e.Row.Cells[3].FindControl("lblCheckType") as Label;
                //if (lbl.Text.Equals("0"))
                //    lbl.Text = "Batchcheck";
                //else if (lbl.Text.Equals("1"))
                //    lbl.Text = "APIcheck ";
                //else if (lbl.Text.Equals("2"))
                //    lbl.Text = "Errorcheck";
                if (DataBinder.Eval(e.Row.DataItem, "Check_Type").ToString().ToLower() == Convert.ToString(0))
                {
                    Label lbl = e.Row.Cells[3].FindControl("lblckcount") as Label;
                    e.Row.Cells[2].Text = "バッチチェックで" + lbl.Text + "件のエラーが発生しました";
                }
                else    if (DataBinder.Eval(e.Row.DataItem, "Check_Type").ToString().ToLower() == Convert.ToString(1))
                {
                    Label lbl = e.Row.Cells[3].FindControl("lblckcount") as Label;
                    e.Row.Cells[2].Text = "	APIチェックで" + lbl.Text + "件のエラーが発生しました";
                }
                else    if (DataBinder.Eval(e.Row.DataItem, "Check_Type").ToString().ToLower() == Convert.ToString(2))
                {
                    Label lbl = e.Row.Cells[3].FindControl("lblckcount") as Label;
                    e.Row.Cells[2].Text = "エラーチェック" + lbl.Text + "件のエラーが発生しました";
                }
            }
           }
          catch (Exception ex)
          {
              Session["Exception"] = ex.ToString();
              Response.Redirect("~/CustomErrorPage.aspx?");
          }
        }

        protected void gvExhibitionError_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
             try
           {
            Exhibition_Error_BL exErrbl = new Exhibition_Error_BL();
            DataTable dt = exErrbl.SelectExhibitionError();
            gvExhibitionError.PageIndex = e.NewPageIndex;
            gvExhibitionError.DataSource = dt;
            gvExhibitionError.DataBind();
           }
             catch (Exception ex)
             {
                 Session["Exception"] = ex.ToString();
                 Response.Redirect("~/CustomErrorPage.aspx?");
             }

        }


        #region HomePage
        protected void gdvExhibitInfo_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = " ";
                HeaderCell.ColumnSpan = 1;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "掲載商品";
                HeaderCell.Font.Bold = true;
                HeaderCell.ColumnSpan = 4;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "出品数";
                HeaderCell.ColumnSpan = 5;
                HeaderCell.Font.Bold = true;
                HeaderGridRow.Cells.Add(HeaderCell);

                //HeaderCell = new TableCell();
                //HeaderCell.Text = "エラー情報";
                //HeaderCell.ColumnSpan = 3;
                //HeaderCell.Font.Bold = true;
                //HeaderGridRow.Cells.Add(HeaderCell);

                gdvExhibitInfo.Controls[0].Controls.AddAt(0, HeaderGridRow);

            }
        }

        protected void gdvExhibitInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                Exhibition_Error_BL exErrbl = new Exhibition_Error_BL();
                DataTable dtExhibitInfo = exErrbl.SelectExhibitionInfo();
                gdvExhibitInfo.PageIndex = e.NewPageIndex;
                gdvExhibitInfo.DataSource = dtExhibitInfo;
                gdvExhibitInfo.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        public void GridBind(DataTable dt)
        {
            if (dt != null)
            {
                gp.TotalRecord = dt.Rows.Count;
                gp.OnePageRecord = gvExhibitionError.PageSize;

                int index1 = 0;
                gp.sendIndexToThePage += delegate(int index)
                {
                    index1 = index;
                };
                gvExhibitionError.PageIndex = index1;

                gvExhibitionError.DataSource = dt;
                gvExhibitionError.DataBind();
            }
        }

        protected void btnOrderCount_Click(object sender, EventArgs e)
        {
            Exhibition_Error_BL exErrbl = new Exhibition_Error_BL();
            DataTable dtOrderCount = exErrbl.SelectOrderCount();
            gdvSalePrice.DataSource = dtOrderCount;
            gdvSalePrice.DataBind();
            int total = 0;
            if (dtOrderCount.Rows.Count > 0)
            {
                gdvSalePrice.FooterRow.Cells[0].Text = "合計";
                for (int k = 1; k < dtOrderCount.Columns.Count; k++)
                {
                    total = dtOrderCount.AsEnumerable().Sum(row => row.Field<Int32>(dtOrderCount.Columns[k].ToString()));
                    gdvSalePrice.FooterRow.Cells[k].Text = total.ToString();
                    gdvSalePrice.FooterRow.Cells[k].Font.Bold = true;
                }
            }
            DataTable dt = exErrbl.SelectExhibitionError();
            GridBind(dt);
            UPanel.Update();
        }

        protected void btnTotalAmount_Click(object sender, EventArgs e)
        {
            Exhibition_Error_BL exErrbl = new Exhibition_Error_BL();
            DataTable dtSalePrice = exErrbl.SelectSalePrice();
            gdvSalePrice.DataSource = dtSalePrice;
            gdvSalePrice.DataBind();
            int total = 0;
            if (dtSalePrice.Rows.Count > 0)
            {
                gdvSalePrice.FooterRow.Cells[0].Text = "合計";
                for (int k = 1; k < dtSalePrice.Columns.Count; k++)
                {
                    total = dtSalePrice.AsEnumerable().Sum(row => row.Field<Int32>(dtSalePrice.Columns[k].ToString()));
                    gdvSalePrice.FooterRow.Cells[k].Text = total.ToString();
                    gdvSalePrice.FooterRow.Cells[k].Font.Bold = true;
                }
            }
            DataTable dt = exErrbl.SelectExhibitionError();
            GridBind(dt);
            UPanel.Update();
        }

        protected void btnWaitingStatus_Click(object sender, EventArgs e)
        {
            Exhibition_Error_BL exErrbl = new Exhibition_Error_BL();
            DataTable dtWaitingItem = exErrbl.SelectWaitingItemCode();
            gdvSalePrice.DataSource = dtWaitingItem;
            gdvSalePrice.DataBind();
            DataTable dt = exErrbl.SelectExhibitionError();
            GridBind(dt);
            UPanel.Update();
        }

        protected void gdvSalePrice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                Exhibition_Error_BL exErrbl = new Exhibition_Error_BL();
                DataTable dtWaitingItem = exErrbl.SelectWaitingItemCode();
                gdvSalePrice.PageIndex = e.NewPageIndex;
                gdvSalePrice.DataSource = dtWaitingItem;
                gdvSalePrice.DataBind();
                UPanel.Update();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        #endregion
    }
}