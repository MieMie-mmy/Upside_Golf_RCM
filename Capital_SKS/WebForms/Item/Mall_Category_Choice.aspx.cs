/* 
Created By              :Aye Aye Mon
Created Date          : 08/07/2014
Updated By             :
Updated Date         :

 Tables using: Mall_Category
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
using ORS_RCM_BL;
using System.Data;
using System.Collections;

namespace ORS_RCM
{
    public partial class Mall_Category_Choice : System.Web.UI.Page
    {
        public int MallID
        {
            get
            {
                if (Request.QueryString["Mall_ID"] != null)
                {
                    return int.Parse(Request.QueryString["Mall_ID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        public string Item_Code
        {
            get
            {
                if (Request.QueryString["Item_Code"] != null)
                {
                    return Request.QueryString["Item_Code"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (MallID != 0)
                    {
                        switch (MallID)
                        {
                            case 1: lblheader.Text = "楽天モールカテゴリ";
                                break;
                            case 2: lblheader.Text = "Yahooモールカテゴリ";
                                break;
                            case 3: lblheader.Text = "ポンパレモールカテゴリ";
                                break;
                            case 4:
                                lblheader.Text = "Wowmaモールカテゴリ";
                                break;
                            case 7:
                                lblheader.Text = "ORS自社モールカテゴリ";
                                break;
                        }

                        gvMallCategory.DataSource = SelectByMallID(MallID);
                        gvMallCategory.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public DataTable SelectByMallID(int mallID)
        {
            try
            {
                DataTable dt = new DataTable();
                Mall_Category_BL MallCategoryBL = new Mall_Category_BL();
                dt = MallCategoryBL.SelectByMallID(mallID);

                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            }
        }

        public DataTable Search()
        {
            try
            {


            ArrayList arrList = new ArrayList();

            string str = null;
            string[] strArr = null;
            str = txtSearch.Text;

            char[] splitchar = { ' ', '　' }; 
            strArr = str.Split(splitchar);

            string category1 = string.Empty;
            string category2 = string.Empty;
            string category3 = string.Empty;

            if (strArr.Length > 0)
            {
                category1 = strArr[0].ToString(); 
            }
            if (strArr.Length > 1)
            {
                category2 = strArr[1].ToString();
            }
            if (strArr.Length > 2)
            {
                category3 = strArr[2].ToString();
            }


            DataTable dt = new DataTable();
            Mall_Category_BL mbl = new Mall_Category_BL();
            mbl = new Mall_Category_BL();


            if (!string.IsNullOrWhiteSpace(txtSearch.Text) && MallID != 0)
            {
                dt = mbl.SearchCat(MallID,txtSearch.Text.Trim(), category1, category2, category3);
                return dt;
            }
            else if (string.IsNullOrWhiteSpace(txtSearch.Text) && MallID != 0)
            {
                dt = mbl.SelectByMallID(MallID);
                return dt;
            }
            else
            {
                return dt;
            }
        }
        catch (Exception ex)
        {
            Session["Exception"] = ex.ToString();
            Response.Redirect("~/CustomErrorPage.aspx?");
            return new DataTable();
        }
}

        protected void gvMallCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMallCategory.DataSource = Search();
                gvMallCategory.PageIndex = e.NewPageIndex;
                gvMallCategory.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                gvMallCategory.DataSource = Search();
                gvMallCategory.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(Int32));
                dt.Columns.Add("Mall_ID", typeof(Int16));
                dt.Columns.Add("Category_ID", typeof(string));
                dt.Columns.Add("Category_Path", typeof(String));
                DataRow dr = dt.NewRow();

                foreach (GridViewRow row in gvMallCategory.Rows)
                {
                    Label lbltype = (Label)row.FindControl("lblID");
                    Label lblCategory_ID = (Label)row.FindControl("lblCategory_ID");
                    Label lblPath = (Label)row.FindControl("lblCategoryPath");

                    if (((RadioButton)row.FindControl("rdo")).Checked)
                    {
                        dr["ID"] = int.Parse(lbltype.Text);
                        dr["Mall_ID"] = MallID;
                        dr["Category_ID"] = lblCategory_ID.Text;
                        dr["Category_Path"] = lblPath.Text;
                        dt.Rows.Add(dr);

                        Session["Mall_Category_ID_"+Item_Code] = dt;

                    }
                }

                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack();window.close()", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

    }
}