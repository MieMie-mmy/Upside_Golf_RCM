using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Collections;
using System.Data;
using System.IO;
using System.Configuration;
using ORS_RCM_Common;

namespace ORS_RCM.WebForms.Promotion
{
    public partial class Promotion_View : System.Web.UI.Page
    {
        Promotion_BL promotionbl;
        Shop_BL shopBL;
        string exportPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();

        public string getPostBackControlName()
        {
            try
            {
                Control control = null;
                //first we will check the "__EVENTTARGET" because if post back made by       the controls
                //which used "_doPostBack" function also available in Request.Form collection.
                string ctrlname = Page.Request.Params["__EVENTTARGET"];
                if (ctrlname != null && ctrlname != String.Empty)
                {
                    control = Page.FindControl(ctrlname);
                }
                // if __EVENTTARGET is null, the control is a button type and we need to
                // iterate over the form collection to find it
                else
                {
                    string ctrlStr = String.Empty;
                    Control c = null;
                    foreach (string ctl in Page.Request.Form)
                    {
                        //handle ImageButton they having an additional "quasi-property" in their Id which identifies
                        //mouse x and y coordinates
                        if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                        {
                            ctrlStr = ctl.Substring(0, ctl.Length - 2);
                            c = Page.FindControl(ctrlStr);
                        }
                        else
                        {
                           c = Page.FindControl(ctl);
                        }
                        if (c is System.Web.UI.WebControls.Button || c is System.Web.UI.WebControls.ImageButton)
                        {
                            control = c;
                            break;
                        }
                    }
                }

                if (control != null)
                    return control.ID;
                else return null;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
                BindShop();
            }
            else
            {
                DataTable dt = Search();

                String ctrl = getPostBackControlName();

                if (ctrl.Contains("lnkPaging"))
                {
                    UCGrid_Paging1.LinkButtonClick(ctrl, gvPromotion.PageSize);

                    Label lbl = UCGrid_Paging1.FindControl("lblCurrent") as Label;
                    int index = Convert.ToInt32(lbl.Text);
                    gvPromotion.PageIndex = index - 1;
                    gvPromotion.DataSource = dt;
                    gvPromotion.DataBind();
                }

            }
           
        }

        protected void Bind()
        {
            DataTable dt = Search();

            gvPromotion.DataSource = dt;
            gvPromotion.DataBind();
            UCGrid_Paging1.CalculatePaging(dt.Rows.Count, gvPromotion.PageSize, 1);

        }

        protected void gvPromotion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DataEdit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("~/WebForms/Promotion/Promotion.aspx?ID=" + ID);
            }
            if (e.CommandName == "DataCopy")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect(String.Format("~/WebForms/Promotion/Promotion.aspx?ID={0}&status={1}", ID, "copy"));
            }
        }

        protected void gvPromotion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            promotionbl = new Promotion_BL();
            gvPromotion.DataSource = promotionbl.SelectAll();
            gvPromotion.DataBind();
            gvPromotion.PageIndex = e.NewPageIndex;
            gvPromotion.DataBind();
            if (ViewState["CheckBoxArray"] != null)
            {
                ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
                bool contains = CheckBoxArray.Contains("chkAll");
                if (contains)
                {
                    CheckBox chkAll = (CheckBox)gvPromotion.HeaderRow.Cells[0].FindControl("chkAll");
                    chkAll.Checked = true;

                    for (int i = 0; i < gvPromotion.Rows.Count; i++)
                    {
                        if (gvPromotion.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chk = (CheckBox)gvPromotion.Rows[i].Cells[0].FindControl("chkExport");
                            chk.Checked = true;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < gvPromotion.Rows.Count; i++)
                    {
                        int CheckBoxIndex = gvPromotion.PageSize * (gvPromotion.PageIndex) + (i + 1);
                        if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1)
                        {
                            CheckBox chk = (CheckBox)gvPromotion.Rows[i].Cells[0].FindControl("chkExport");
                            chk.Checked = true;
                        }
                    }
                }
            }
        }

        public void GetShop()
        {
            shopBL = new Shop_BL();
            shopBL.SelectAll();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/Promotion/Promotion.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = Search();
            gvPromotion.DataSource = dt;
            gvPromotion.DataBind();
            UCGrid_Paging1.CalculatePaging(dt.Rows.Count, gvPromotion.PageSize, 1);
        }

        private DataTable Search()
        {
            promotionbl = new Promotion_BL();
            DataTable dt = new DataTable();
            int? period = null;
            string status = "";

            if (rdolPeriod.SelectedValue == "1")
            {
                period = 30;
            }
            if (rdolPeriod.SelectedValue == "2")
            {
                period = 90;
            }

            foreach (ListItem item in chklStatus.Items)
            {
                if (item.Selected)
                {
                    status += item.Value + ",";
                }
            }
            status = status.TrimEnd(',');
            if (status == "")
            {
                status = "0,1,2,3";
            }
            
            //dt = promotionbl.SearchPromotion(txtPromotion_Name.Text, ddlShop_Name.SelectedValue.ToString(), ddlType.SelectedValue.ToString(), txtBrand_Name.Text, period, status);
            dt.Columns.Add("No", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["No"] = i + 1;
            }

            return dt;
        }

        private string MakeAcronym(string input)
        {
            var chars = input.Where(Char.IsUpper).ToArray();
            return new String(chars);
        }

        protected void gvPromotion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Image rbase =(Image) e.Row.FindControl("rbseball");
            Image ybp = (Image)e.Row.FindControl("by");
            Image yhp = (Image)e.Row.FindControl("hp");
            Image Rlp = (Image)e.Row.FindControl("rlp");
            Image Ylp = (Image)e.Row.FindControl("ylp");
            Image rrp = (Image)e.Row.FindControl("rrp");
            Image ablc = (Image)e.Row.FindControl("ablack");
            Image Yrp = (Image)e.Row.FindControl("yrp");
            Image Prp = (Image)e.Row.FindControl("prp");
            Image Rsp = (Image)e.Row.FindControl("rsp");
            Image Ysp = (Image)e.Row.FindControl("ysp");
            Image Pbp = (Image)e.Row.FindControl("pbp");
            Image Abp = (Image)e.Row.FindControl("abp");
            Image Rhp = (Image)e.Row.FindControl("rhp");
            Image Ajp = (Image)e.Row.FindControl("ajp");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int promotionID = int.Parse(gvPromotion.DataKeys[e.Row.RowIndex].Value.ToString());
                promotionbl = new Promotion_BL();
                DataTable dt = promotionbl.GetShopNamesByID(promotionID);

                DropDownList ddlShop = e.Row.FindControl("ddlShop") as DropDownList;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlShop.Items.Insert(i, dt.Rows[i]["Shop_Code"].ToString());

                    string mallid = dt.Rows[i]["Mall_ID"].ToString();
                    //string shopname =dt.Rows[i]["Shop_Name"].ToString();
                    string shopname = dt.Rows[i]["Shop_Code"].ToString();
                    string shopid = dt.Rows[i]["Shop_Code"].ToString();
                    if (mallid == "1")
                    {
                        if (shopname.Trim() == "Racket Plaza")
                        { rrp.Visible = true; }
                        else if (shopname.Trim() == "Sports Plaza")
                            Rsp.Visible = true;
                        else if (shopname.Trim() == "Baseball Plaza")
                            rbase.Visible = true;
                        else if (shopname.Trim() == "Table Tennis Honpo")
                            Rhp.Visible = true;
                        else if (shopname.Trim() == "Rack piece")
                            Rlp.Visible = true;
                    }
                    else if (mallid == "2") 
                    {
                      
                        //if (shopname == "Racket Plaza")
                        if( String.Equals(shopname.Trim(),"Racket Plaza"))
                        { Yrp.Visible = true; }
                        else if (shopname.Trim() == "Sports Plaza")
                            Ysp.Visible = true;
                        else if (shopname.Trim() == "Baseball Plaza")
                            ybp.Visible = true;
                        else if (shopname.Trim() == "Table Tennis Honpo")
                            yhp.Visible = true;
                        else if (shopname.Trim() == "Rack piece")
                            Ylp.Visible = true;
                    }
                    else if (mallid == "3")
                    {
                        if (shopname.Trim() == "Racket Plaza")
                        { Prp.Visible = true; }
                        else if (shopname.Trim() == "Sports Plaza")
                            e.Row.Cells[5].Text = "Sports Plaza Ponpare";
                        else if (shopname.Trim() == "Baseball Plaza")
                            Pbp.Visible = true;
                        else if (shopname.Trim() == "Table Tennis Honpo")
                            e.Row.Cells[5].Text = "Table Tennis Honpo Ponpare";
                        else if (shopname.Trim() == "Rack piece")
                            e.Row.Cells[5].Text = "Rack piece Ponpare";
                    }
                    else if (mallid == "4")
                    {
                        if (shopname.Trim() == "Racket Plaza")
                        { ablc.Visible = true; }
                        else if (shopname.Trim() == "Sports Plaza")
                            e.Row.Cells[5].Text = "Sports Plaza Amazone";
                        else if (shopname.Trim() == "Baseball Plaza")
                            Abp.Visible = true;
                        else if (shopname.Trim() == "Table Tennis Honpo")
                            e.Row.Cells[5].Text = "Table Tennis Honpo Amazone";
                        else if (shopname.Trim() == "Rack piece")
                            e.Row.Cells[5].Text = "Rack piece Amazone";
                    }
                    else if (mallid == "5")
                    {
                        if (shopname.Trim() == "Racket Plaza")
                            Ajp.Visible = true;
                        else if (shopname.Trim() == "Sports Plaza")
                            e.Row.Cells[5].Text = shopid;
                        else if (shopname.Trim() == "Baseball Plaza")
                            e.Row.Cells[5].Text = shopid;
                        else if (shopname.Trim() == "Table Tennis Honpo")
                            e.Row.Cells[5].Text = shopid;
                        else if (shopname.Trim() == "Rack piece")
                            e.Row.Cells[5].Text = shopid;
                    }

                }

                Label lblStatus = e.Row.FindControl("lblStatus") as Label;
                Promotion_Entity promotionInfo = new Promotion_Entity();
               // promotionInfo = promotionbl.SelectByID(promotionID);

                switch (promotionInfo.Status)
                {
                    case 0:
                        lblStatus.Text = "開催前";
                        break;
                    case 1:
                        lblStatus.Text = "開催中";
                        break;
                    case 2:
                        lblStatus.Text = "終了";
                        break;
                    case 3:
                        lblStatus.Text = "中止";
                        break;
                }
            }
        }

        private void BindShop()
        {
             shopBL = new Shop_BL();
            DataTable dt = shopBL.SelectShopAndMall();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("ID",typeof(int));
            //dt.Columns.Add("Name",typeof(string));
            //foreach (DataRow dr in dtTmp.Rows)
            //{
            //    dt.Rows.Add(dr["ID"].ToString(), dr["Shop_Name"].ToString() + " " + dr["Mall_Name"].ToString());
            //}
            ddlShop_Name.DataSource = dt;
            ddlShop_Name.DataValueField = "ID";
            ddlShop_Name.DataTextField = "Shop_Name";
            //ddlShop_Name.DataTextField = "Shop_ID";
            ddlShop_Name.DataBind();
        }

    }
}