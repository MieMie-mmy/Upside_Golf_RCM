using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ORS_RCM.WebForms.Jisha
{
    public partial class Jisha_Item_Detail : System.Web.UI.Page
    {

        // Request Parameter
        public int Item_ID
        {
            get
            {
                if (Request.QueryString["Item_ID"] != null)
                {
                    return Convert.ToInt32(Request.QueryString["Item_ID"].ToString());
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
        Jisha_Item_Master_BL JishaBL = new Jisha_Item_Master_BL();
        Category_BL cbl;
        DataTable dt = new DataTable();
        public int index = 0;
        public String[] ids = new String[100];
        public int extract = 0;
        public String[] ex = new String[600];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AddToCart"] != null)
                    ///read Basket_DataTable from session if exist
                    dt = (DataTable)Session["AddToCart"];
                else
                {
                    //create an empty DataTable and Add some columns to it
                    dt = new DataTable();
                    dt.Columns.Add(new System.Data.DataColumn("Item_ID", typeof(Int16)));
                    dt.Columns.Add(new System.Data.DataColumn("Quantity", typeof(Int16)));
                    dt.Columns.Add(new System.Data.DataColumn("Image_Name", typeof(string)));
                    dt.Columns.Add(new System.Data.DataColumn("Item_Code", typeof(string)));
                    dt.Columns.Add(new System.Data.DataColumn("Item_Name", typeof(string)));
                    dt.Columns.Add(new System.Data.DataColumn("Size_Code", typeof(string)));
                    dt.Columns.Add(new System.Data.DataColumn("Size_Name", typeof(string)));
                    dt.Columns.Add(new System.Data.DataColumn("Color_Code", typeof(string)));
                    dt.Columns.Add(new System.Data.DataColumn("Color_Name", typeof(string)));
                    dt.Columns.Add(new System.Data.DataColumn("Price", typeof(Int16)));
                    dt.Columns.Add(new System.Data.DataColumn("Amount", typeof(ulong)));
                    Session["AddToCart"] = dt;
                }

                if (Item_ID != 0)
                {
                    dt = JishaBL.SelectByItemID(Item_ID);
                    DisplayItem(dt);
                }
                else
                {
                    DisplayItemOption(lblItem_Code2.Text);
                }

            }
        }

        protected void DisplayItem(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                lt_Item_Description.Text = dt.Rows[0]["Item_Description_PC"].ToString();
                lt_Sale_Description.Text = dt.Rows[0]["Sale_Description_PC"].ToString();
                lblItem_Name.Text = dt.Rows[0]["Item_Name"].ToString();
                lblItem_Name1.Text = dt.Rows[0]["Item_Name"].ToString();
                lblItem_Code2.Text = dt.Rows[0]["Item_Code"].ToString();
                lblList_Price.Text = dt.Rows[0]["List_Price"].ToString();
                lblPrice.Text = dt.Rows[0]["Sale_Price"].ToString();
                imgItem.ImageUrl = "~/Item_Image/" + dt.Rows[0]["Image_Name1"].ToString();
                Image1.ImageUrl = "~/Item_Image/" + dt.Rows[0]["Image_Name1"].ToString();

                DisplayCategoryPath(lblItem_Code2.Text);
                DisplayItemImage(dt.Rows[0]["Image_Name"].ToString());
                DisplaySKU(lblItem_Code2.Text);
                DisplayItemOption(lblItem_Code2.Text);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Request.Form["radio"] != null)
            {
                string selectedGender = Request.Form["radio"].ToString();
                string[] words = selectedGender.Split(',');
                int quantity = 0;
                if (!string.IsNullOrWhiteSpace(txtQuantity.Text) && Item_ID != 0)
                {
                    DataRow[] dr = ((DataTable)Session["AddToCart"]).Select("Item_Code='" + lblItem_Code2.Text + "' AND Size_Code='" + words[1] + "' AND Color_Code='" + words[3] + "'");
                    if (dr.Count() > 0)
                    {
                        DataTable dtAddToCart = ((DataTable)Session["AddToCart"]).Select("Item_Code='" + lblItem_Code2.Text + "' AND Size_Code='" + words[1] + "' AND Color_Code='" + words[3] + "'").CopyToDataTable();
                        for (int i = 0; i < dtAddToCart.Rows.Count; i++)
                        {
                            quantity += Convert.ToInt32(dtAddToCart.Rows[i]["Quantity"]);
                        }
                    }
                    if (Convert.ToInt32(txtQuantity.Text) > 0 && ((Convert.ToInt32(txtQuantity.Text) + quantity) <= Convert.ToInt32(words[0])))
                    {
                        ((DataTable)Session["AddToCart"]).Rows.Add(Item_ID,
                                                                                                        int.Parse(txtQuantity.Text),
                                                                                                        imgItem.ImageUrl,
                                                                                                        lblItem_Code2.Text,
                                                                                                        lblItem_Name.Text,
                                                                                                        words[1], //Size_Code
                                                                                                        words[2], //Size_Name
                                                                                                        words[3], //Color_Code
                                                                                                        words[4], //Color_Name
                                                                                                        lblPrice.Text,
                                                                                                        int.Parse(txtQuantity.Text) * Convert.ToInt32(lblPrice.Text));
                        Response.Redirect("~/WebForms/Jisha/Jisha_Shopping_Cart.aspx");
                    }
                    else
                    {
                        int avaliableQty = Convert.ToInt32(words[0]) - (Convert.ToInt32(txtQuantity.Text) + quantity);
                        if ((Convert.ToInt32(txtQuantity.Text) + avaliableQty) > 0 && Convert.ToInt32(txtQuantity.Text) == 0)
                        {
                            GlobalUI.MessageBox("Invaid Quantity.You can order up to a maximum of " + avaliableQty + " pieces now.");
                        }
                        else if ((Convert.ToInt32(txtQuantity.Text) + avaliableQty) > 0)
                        {
                            GlobalUI.MessageBox("Invaid Quantity.You can order up to a maximum of " + (Convert.ToInt32(txtQuantity.Text) + avaliableQty) + " pieces now.");
                        }
                        else
                        {
                            GlobalUI.MessageBox("There is no goods for sale.");
                        }
                    }
                }
                else
                {
                    GlobalUI.MessageBox("Quantity is required.");
                }
            }
            else
            {
                GlobalUI.MessageBox("Please choose the type of goods from the table above.");
            }

        }

        protected void DisplayItemOption(string Item_Code)
        {
            dt = JishaBL.GetJishaItemOption(Item_Code);
            if (dt != null && dt.Rows.Count > 0)
            {
                string ddlID = String.Empty;
                if (dt != null)
                {
                    for (int j = dt.Rows.Count - 1; j >= 0; j--)
                    {
                        ddlID = dt.Rows[j]["ID"].ToString();

                        Label lbl = new Label();
                        lbl.ID = "lblDDL-" + ddlID;
                        lbl.Text = dt.Rows[j]["Option_Name"].ToString() + "&nbsp;&nbsp;";
                        PanelOption.Controls.Add(lbl);

                        string value = dt.Rows[j]["Option_Value"].ToString();
                        DropDownList ddl = new DropDownList();
                        string iD = ddlID;
                        ddl.ID = iD;
                        //ddl.Items.Add(new ListItem("--Select--", ""));
                        string[] arr = value.Split(',');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            ddl.Items.Add(new ListItem(arr[i].ToString()));
                        }
                        ddl.AutoPostBack = true;
                        PanelOption.Controls.Add(ddl);
                        Literal ltl = new Literal();
                        ltl.Text = "<br/><br/>";
                        PanelOption.Controls.Add(ltl);
                    }
                }
            }
        }

        protected void DisplaySKU(string Item_Code)
        {

            //gvItem.DataSource = itSkubl.Search(Item_Code);
            //gvItem.DataBind();
            //string html = "<table border=1 class=\"listTable\"><tr><th style=\"width:30px\">&nbsp;</th>";

            string html = "<table class=\"listTable\" id=\"SKU\"><tbody><tr><th>&nbsp;</th>";
            DataTable dtSKUHeader = JishaBL.GetSKUHeader(Item_Code);
            DataTable dtSKUQuantity = JishaBL.GetSKUQuantity(Item_Code);
            if (dtSKUHeader != null && dtSKUHeader.Rows.Count > 0 && dtSKUQuantity != null && dtSKUQuantity.Rows.Count > 0)
            {
                Label3.Visible = true;
                txtQuantity.Visible = true;
                btnAdd.Visible = true;

                foreach (DataRow dr in dtSKUHeader.Rows)
                {
                    html += String.Format("<td>{0}<br/><span>{1}</span></td>", dr["Size_Name"].ToString(), dr["Size_Code"].ToString());
                }
                html += "</tr>";
                foreach (DataRow drQty in dtSKUQuantity.Rows)
                {
                    html += "<tr>";
                    html += String.Format("<td>{0}<br/><span>{1}</span></td>", drQty["Color_Name"].ToString(), drQty["Color_Code"].ToString());
                    foreach (DataRow drSize in dtSKUHeader.Rows)
                    {
                        if (drSize["Size_Code"].ToString() == drQty["Size_Code"].ToString())
                        {
                            html += String.Format("<td>&nbsp;<input type='radio' name='radio' value='{0},{1},{2},{3},{4}'/></td>", drQty["Quantity"], drQty["Size_Code"], drQty["Size_Name"], drQty["Color_Code"], drQty["Color_Name"]);
                        }
                        else
                        {
                            html += String.Format("<td>{0}</td>", "&nbsp;X");
                        }
                    }
                    html += "</tr>";
                }
                html += "</tbody></table>";

                divSKUTable.InnerHtml = html;
            }
            else
            {
                Label3.Visible = false;
                txtQuantity.Visible = false;
                btnAdd.Visible = false;
            }
        }

        protected void DisplayItemImage(string Item_Image)
        {
            if (!string.IsNullOrWhiteSpace(Item_Image))
            {
                string html = null;
                string[] Image = Item_Image.TrimEnd(',').Split(',');
                for (int i = 0; i < Image.Length; i++)
                {
                    //html += String.Format("<asp:Image ID=\"{0}\" runat=\"server\" Width=\"50px\" Height=\"50px\" ImageUrl=\"{1}\" />", "img" + i, "~/Item_Image/" + Image[i]);
                    html += String.Format("<img src=\"{0}\" width=\"80\" height=\"80\" alt=\"{1}\">", "../../Item_Image/" + Image[i].Trim(), "img" + i);
                }
                divItem_Image.InnerHtml = html;
            }

        }

        protected void DisplayCategoryPath(string Item_Code)
        {
            dt = JishaBL.SelectByCategoryPath(Item_Code);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvCategory.DataSource = dt;
                gvCategory.DataBind();
            }
        }

        protected void DataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Response.Redirect("~/WebForms/Jisha/Jisha_Item_View.aspx?Category_No=" + e.CommandArgument);
        }

        protected void dlSiteMap_ItemCommand(object source, DataListCommandEventArgs e)
        {
            //Label ID = (Label)e.Item.FindControl("lblID");
            Response.Redirect("~/WebForms/Jisha/Jisha_Item_View.aspx?Category_No=" + e.CommandArgument);
        }

        protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCategory_No = e.Row.FindControl("lblCategory_No") as Label;
                if (!String.IsNullOrWhiteSpace(lblCategory_No.Text))
                {
                    BindCategory(lblCategory_No.Text, e);
                }
            }
        }

        protected void BindCategory(string Category_No, GridViewRowEventArgs e)
        {
                DataList dlSiteMap = e.Row.FindControl("dlSiteMap") as DataList;
                index = 0;
                ids[index++] = GetCategoryID(Category_No).ToString();
                GetCategory(GetCategoryID(Category_No));
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

        protected void GetCategory(int id)
        {
            cbl = new Category_BL();
            DataTable dt = cbl.SelectForCatalogID(id);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ids[index++] = dt.Rows[i]["ParentID"].ToString();
                GetCategory(Convert.ToInt32(dt.Rows[i]["ParentID"].ToString()));
            }
            //return index;
        }

        protected int GetCategoryID(string Category_No)
        {
            cbl = new Category_BL();
             dt = cbl.GetCategoryID(Category_No);
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