using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ORS_RCM_BL;


namespace ORS_RCM.WebForms.Jisha
{
    public partial class Jisha_Item_View : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        Category_BL cbl = new Category_BL();
        public int index = 0;
        public String[] ids = new String[100];
        public int extract = 0;
        public String[] ex = new String[600];

        private int ID
        {
            get
            {
                if (Request.QueryString["Category_No"] != null)
                {
                    return GetCategoryID();
                }
                else
                {
                    return 0;
                }
            }
        }

        private string Category_No
        {
            get
            {
                if (Request.QueryString["Category_No"] != null)
                    return Request.QueryString["Category_No"];
                else
                    return "";
            }
        }

        

        Jisha_Item_Master_BL JishaBL= new Jisha_Item_Master_BL() ;

        PagedDataSource _PageDataSource = new PagedDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ID == 0)
                {
                    this.BindItemsList();
                }
                else
                {
                    ids[index++] = ID.ToString();
                    GetCategory(ID);
                    int i = 0;
                    DataTable dts = new DataTable();
                    while (!String.IsNullOrWhiteSpace(ids[i]))
                    {
                        if (dts.Rows.Count > 0)
                            dts.Merge(cbl.SelectForCatalogID(Convert.ToInt32(ids[i++])));
                        else
                            dts = cbl.SelectForCatalogID(Convert.ToInt32(ids[i++]));
                    }

                    dlSiteMap.DataSource = ReverseRowsInDataTable(dts);
                    dlSiteMap.DataBind();
                    BindDataList();
                    BindItemsList();
                }
            }
        }

        protected void GetCategory(int id)
        {
            cbl = new Category_BL();
            DataTable dt = cbl.SelectForCatalogID(id);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ids[index++] = dt.Rows[i]["ParentID"].ToString();
                GetCategory(Convert.ToInt32(dt.Rows[i]["ParentID"].ToString()));
            }
        }

        private void BindDataList()
        {
            DataList.DataSource = cbl.SelectForTreeview(ID);
            DataList.DataBind();
        }


        protected void DataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            //Label ID = (Label)e.Item.FindControl("lblID");
            Response.Redirect("~/WebForms/Jisha/Jisha_Item_View.aspx?Category_No=" + e.CommandArgument);
        }

        protected void dlSiteMap_ItemCommand(object source, DataListCommandEventArgs e)
        {
            //Label ID = (Label)e.Item.FindControl("lblID");
            Response.Redirect("~/WebForms/Jisha/Jisha_Item_View.aspx?Category_No=" + e.CommandArgument);
        }

        protected void dListItems_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Label ID = (Label)e.Item.FindControl("lblID");
            Response.Redirect("~/WebForms/Jisha/Jisha_Item_Detail.aspx?Item_ID=" + ID.Text);
        }

        //protected void btnCart_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Shopping_Cart.aspx");
        //}

        private int CurrentPage
        {
            get
            {
                object objPage = ViewState["_CurrentPage"];
                int _CurrentPage = 0;
                if (objPage == null)
                {
                    _CurrentPage = 0;
                }
                else
                {
                    _CurrentPage = (int)objPage;
                }
                return _CurrentPage;
            }
            set { ViewState["_CurrentPage"] = value; }
        }

        private int fistIndex
        {
            get
            {
                int _FirstIndex = 0;
                if (ViewState["_FirstIndex"] == null)
                {
                    _FirstIndex = 0;
                }
                else
                {
                    _FirstIndex = Convert.ToInt32(ViewState["_FirstIndex"]);
                }
                return _FirstIndex;
            }
            set { ViewState["_FirstIndex"] = value; }
        }

        private int lastIndex
        {
            get
            {
                int _LastIndex = 0;
                if (ViewState["_LastIndex"] == null)
                {
                    _LastIndex = 0;
                }
                else
                {
                    _LastIndex = Convert.ToInt32(ViewState["_LastIndex"]);
                }
                return _LastIndex;
            }
            set { ViewState["_LastIndex"] = value; }
        }

        private void BindItemsList()
        {
            DataTable dataTable = new DataTable();
            if (Category_No != "")
            {
                dataTable = JishaBL.SelectItemByCategory(Category_No);
            }
            else
            {
                dataTable = JishaBL.SelectAll();
            }
            _PageDataSource.DataSource = dataTable.DefaultView;
            _PageDataSource.AllowPaging = true;
            _PageDataSource.PageSize = 4;
            _PageDataSource.CurrentPageIndex = CurrentPage;
            ViewState["TotalPages"] = _PageDataSource.PageCount;
            this.lblPageInfo.Text = "Page " + (CurrentPage + 1) + " of " + _PageDataSource.PageCount;
            this.lbtnPrevious.Enabled = !_PageDataSource.IsFirstPage;
            this.lbtnNext.Enabled = !_PageDataSource.IsLastPage;
            this.lbtnFirst.Enabled = !_PageDataSource.IsFirstPage;
            this.lbtnLast.Enabled = !_PageDataSource.IsLastPage;
            this.dListItems.DataSource = _PageDataSource;
            this.dListItems.DataBind();
            this.doPaging();
        }

        private void doPaging()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");
            fistIndex = CurrentPage - 5;
            if (CurrentPage > 5)
            {
                lastIndex = CurrentPage + 5;
            }
            else
            {
                lastIndex = 10;
            }
            if (lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
            {
                lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
                fistIndex = lastIndex - 10;
            }
            if (fistIndex < 0)
            {
                fistIndex = 0;
            }
            for (int i = fistIndex; i < lastIndex; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }
            this.dlPaging.DataSource = dt;
            this.dlPaging.DataBind();
        }

        protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("Paging"))
            {
                CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
                this.BindItemsList();
            }
        }

        protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
            if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
            {
                lnkbtnPage.Enabled = false;
                lnkbtnPage.Style.Add("fone-size", "14px");
                lnkbtnPage.Font.Bold = true;
            }
        }

        protected void lbtnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            this.BindItemsList();
        }

        protected void lbtnPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            this.BindItemsList();
        }

        protected void lbtnLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            this.BindItemsList();
        }

        protected void lbtnFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            this.BindItemsList();
        }

        protected DataTable ReverseRowsInDataTable(DataTable inputTable)
        {
            DataTable outputTable = inputTable.Clone();

            for (int i = inputTable.Rows.Count - 1; i >= 0; i--)
            {
                outputTable.ImportRow(inputTable.Rows[i]);
            }

            return outputTable;
        }

        protected int GetCategoryID()
        {
            DataTable dt = cbl.GetCategoryID(Category_No);
            if (dt.Rows.Count > 0)
	        {
                return int.Parse(dt.Rows[0]["ID"].ToString());
	        }
            else
            {
                return 0;
            }
        }
    }
}