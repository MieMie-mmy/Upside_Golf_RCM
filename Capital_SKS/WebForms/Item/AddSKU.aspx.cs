using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Item
{
    public partial class AddSKU : System.Web.UI.Page
    {
        static Item_BL ibl = new Item_BL();
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
                    return null;
                }
            }
        }
      
        public static string strItem;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                 DataTable dtItem = ibl.SelectItemData(Item_Code);
                if (dtItem.Rows.Count > 0)
                {
                    ViewState["CurrentTable"] = dtItem;
                    DataTable dt = ViewState["CurrentTable"] as DataTable;
                    gdvAddSku.DataSource = dt;
                    gdvAddSku.DataBind();
                }
                else
                {
                    SetInitialRow();
                }
            }
        }

        private void SetInitialRow()
        {
            if (ViewState["CurrentTable"] == null)
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                //dt.Columns.Add(new DataColumn("Item_AdminCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Item_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Size_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Color_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Size_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Color_Code", typeof(string)));
                //dt.Columns.Add(new DataColumn("Size_Name_Official", typeof(string)));
                //dt.Columns.Add(new DataColumn("Color_Name_Official", typeof(string)));
                dt.Columns.Add(new DataColumn("JAN_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Inventory_Flag", typeof(string)));
                dt.Columns.Add(new DataColumn("Quantity", typeof(string)));

                dr = dt.NewRow();
                dr["Item_Code"] = string.Empty;
                dr["Size_Name"] = string.Empty;
                dr["Color_Name"] = string.Empty;
                dr["Size_Code"] = string.Empty;
                dr["Color_Code"] = string.Empty;
                dr["JAN_Code"] = string.Empty;
                dr["Inventory_Flag"] = string.Empty;
                dr["Quantity"] = string.Empty;

                dt.Rows.Add(dr);
                ViewState["CurrentTable"] = dt;
                gdvAddSku.DataSource = dt;
                gdvAddSku.DataBind();
            }
        }

        protected void btnAddNewRow_Click(object sender, EventArgs e)
        {
            DataTable dtSKU = ViewState["CurrentTable"] as DataTable;
            DataTable dt = GetDataFromGrid(dtSKU,"new");
            gdvAddSku.DataSource = dt;
            gdvAddSku.DataBind();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box2 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[0].FindControl("txtItemCode");
                        TextBox box3 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[1].FindControl("txtSizeName");
                        TextBox box4 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[2].FindControl("txtColorName");
                        TextBox box5 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[3].FindControl("txtSizeCode");
                        TextBox box6 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[4].FindControl("txtColorCode");
                        TextBox box9 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[5].FindControl("txtJanCode");
                        DropDownList ddlqty = (DropDownList)gdvAddSku.Rows[rowIndex].Cells[6].FindControl("ddlQtyFlag");
                        TextBox box10 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[7].FindControl("txtQty");

                        box2.Text = dt.Rows[i]["Item_Code"].ToString();
                        box3.Text = dt.Rows[i]["Size_Name"].ToString();
                        box4.Text = dt.Rows[i]["Color_Name"].ToString();
                        box5.Text = dt.Rows[i]["Size_Code"].ToString();
                        box6.Text = dt.Rows[i]["Color_Code"].ToString();
                        box9.Text = dt.Rows[i]["JAN_Code"].ToString();
                        ddlqty.SelectedValue = dt.Rows[i]["Qty_flag"].ToString();
                        box10.Text = dt.Rows[i]["Quantity"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataRow drCurrentRow = null;
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            int rowIndex = 0;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    TextBox box2 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[0].FindControl("txtItemCode");
                    TextBox box3 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[1].FindControl("txtSizeName");
                    TextBox box4 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[2].FindControl("txtColorName");
                    TextBox box5 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[3].FindControl("txtSizeCode");
                    TextBox box6 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[4].FindControl("txtColorCode");
                    TextBox box9 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[5].FindControl("txtJanCode");
                    DropDownList ddlqty = (DropDownList)gdvAddSku.Rows[rowIndex].Cells[6].FindControl("ddlQtyFlag");
                    TextBox box10 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[7].FindControl("txtQty");

                    drCurrentRow = dtCurrentTable.NewRow();
                    dtCurrentTable.Rows[i - 1]["Item_Code"] = box2.Text.Trim();
                    dtCurrentTable.Rows[i - 1]["Size_Name"] = box3.Text.Trim();
                    dtCurrentTable.Rows[i - 1]["Color_Name"] = box4.Text.Trim();
                    dtCurrentTable.Rows[i - 1]["Size_Code"] = box5.Text.Trim();
                    dtCurrentTable.Rows[i - 1]["Color_Code"] = box6.Text.Trim();
                    dtCurrentTable.Rows[i - 1]["JAN_Code"] = box9.Text.Trim();
                    dtCurrentTable.Rows[i - 1]["Inventory_Flag"] = ddlqty.Text.Trim();
                    if (String.IsNullOrWhiteSpace(box10.Text))
                    {
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alertMessage", "alert('Please,Insert  Quantity!!')", true);
                        
                    }
                    else
                    {
                        dtCurrentTable.Rows[i - 1]["Quantity"] = box10.Text.Trim();
                    }
                    rowIndex++;
                }
                DataRow[] rows;
                rows = dtCurrentTable.Select("Size_Code='' OR Size_Code=null OR Color_Code='' OR Color_Code=null");
                //DataRow[] drrdel = dtCurrentTable.Select("Size_Code='" + sizeCode + "'and Color_Code='" + colorCode + "'");
                //for (int i = 0; i < rows.Length; i++)
                //    rows[i].Delete();
                //dtCurrentTable.AcceptChanges();

                if (rows.Count() >=1)
                {
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alertMessage", "alert('Please,Insert Color code and Size code!!')", true);
                }

                else
                {
                    foreach (DataRow row in dtCurrentTable.Rows)
                    {
                        row["Item_Code"] = Item_Code;

                    }
                    dtCurrentTable.AcceptChanges();
                    ibl.InsertUpdateSKU(dtCurrentTable, Item_Code);
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alertMessage", "alert('Save Sucessfully!!')", true);
                }

                 ViewState["CurrentTable"] = dtCurrentTable;
                gdvAddSku.DataSource = dtCurrentTable;
                gdvAddSku.DataBind();

                //DataTable dtItem = ibl.SelectItemData(Item_Code);
                //if (dtItem.Rows.Count != null)
                //{
                //    ViewState["CurrentTable"] = dtItem;
                //    gdvAddSku.DataSource = dtItem;
                //    gdvAddSku.DataBind();
                //}
                //else
                //    SetPreviousData();

                
            }
        }

        public DataTable GetDataFromGrid(DataTable dtCurrentTable,string option)
        {
            int rowIndex = 0;
            string qty=string.Empty;
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    TextBox box2 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[0].FindControl("txtItemCode");
                    TextBox box3 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[1].FindControl("txtSizeName");
                    TextBox box4 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[2].FindControl("txtColorName");
                    TextBox box5 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[3].FindControl("txtSizeCode");
                    TextBox box6 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[4].FindControl("txtColorCode");
                    TextBox box9 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[5].FindControl("txtJanCode");
                    DropDownList ddlqty = (DropDownList)gdvAddSku.Rows[rowIndex].Cells[6].FindControl("ddlQtyFlag");
                    TextBox box10 = (TextBox)gdvAddSku.Rows[rowIndex].Cells[7].FindControl("txtQty");

                    drCurrentRow = dtCurrentTable.NewRow();
                    dtCurrentTable.Rows[i - 1]["Item_Code"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["Size_Name"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Color_Name"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Size_Code"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["Color_Code"] = box6.Text;
                    dtCurrentTable.Rows[i - 1]["JAN_Code"] = box9.Text;
                    dtCurrentTable.Rows[i - 1]["Inventory_Flag"] = ddlqty.SelectedValue;
                    if (String.IsNullOrWhiteSpace(box10.Text))
                    {
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alertMessage", "alert('Please,Insert  Quantity!!')", true);
                        qty = "aa";
                    }
                    else
                    {
                        dtCurrentTable.Rows[i - 1]["Quantity"] = box10.Text;

                    }
                    rowIndex++;
                }
                if (qty != "aa" && option!="DELETE")
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                }
            }
            
            return dtCurrentTable;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack();window.close()", true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            int rowIndex = row.RowIndex;
            //TextBox txt = gdvAddSku.Rows[rowIndex].FindControl("txtItemAdminCode") as TextBox;
            TextBox txtsizeCode = gdvAddSku.Rows[rowIndex].FindControl("txtSizeCode") as TextBox;
            TextBox txtcolorCode = gdvAddSku.Rows[rowIndex].FindControl("txtColorCode") as TextBox;
            string sizeCode = txtsizeCode.Text;
            string colorCode = txtcolorCode.Text;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                dtCurrentTable = GetDataFromGrid(dtCurrentTable,"DELETE");
                DataRow[] drr = dtCurrentTable.Select("Size_Code='' OR Size_Code=null OR Color_Code='' OR Color_Code=null");
                DataRow[] drr1 = dtCurrentTable.Select("Size_Code=null");
                //string a = dtCurrentTable.Rows[1]["Size_Name"].ToString();
                //DataRow[] drr1 = dtCurrentTable.Select("Size_Name='' OR Size_Name=null OR Color_Name='' OR Color_Name=null");
                //for (int j = 0; j < drr.Length; j++)
                //    drr[j].Delete();
                if (dtCurrentTable.Rows.Count == 1)
                {

                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alertMessage", "alert('SKU must at least one row!!')", true);
                }
                if (dtCurrentTable.Rows.Count > 1 || drr.Length>=1)
                {
                    for (int j = 0; j < drr.Length; j++)
                        drr[j].Delete();
                    dtCurrentTable.AcceptChanges();
                    if (dtCurrentTable.Rows.Count == 1)
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alertMessage", "alert('SKU must at least one row!!')", true);
                    else
                    {
                        DataRow[] drrdel = dtCurrentTable.Select("Size_Code='" + sizeCode + "'and Color_Code='" + colorCode + "'");
                        for (int i = 0; i < drrdel.Length; i++)
                            drrdel[i].Delete();
                        dtCurrentTable.AcceptChanges();
                        foreach (DataRow row1 in dtCurrentTable.Rows)
                        {
                            row1["Item_Code"] = Item_Code;

                        }
                        dtCurrentTable.AcceptChanges();
                        ibl.InsertUpdateSKU(dtCurrentTable, Item_Code);
                        
                    }
                    ViewState["CurrentTable"] = dtCurrentTable;
                    gdvAddSku.DataSource = dtCurrentTable;
                    gdvAddSku.DataBind();
                }
                
            }
        }
        
        protected void ddlQtyFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            TextBox txtQty = (TextBox)row.FindControl("txtQty");
            DropDownList ddlQty = (DropDownList)row.FindControl("ddlQtyFlag");
            if (ddlQty.SelectedItem.Value == "1")
            {
                txtQty.Text = "999";
                txtQty.Enabled = false;
            }
            else if (ddlQty.SelectedItem.Value == "2")
            {
                txtQty.Text = "";
                txtQty.Enabled = true;
            }
            else if (ddlQty.SelectedItem.Value == "3")
            {
                txtQty.Text = "0";
                txtQty.Enabled = false;
            }
            else
            {
                txtQty.Enabled = false;
            }

           
        }

        protected void gdvAddSku_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (!(e.Row.RowType == DataControlRowType.Header) && !(e.Row.RowType == DataControlRowType.Footer))
            {
                TextBox txtqty = (TextBox)e.Row.FindControl("txtQty");
                DropDownList ddlqty =(DropDownList) e.Row.FindControl("ddlQtyFlag");
                if (txtqty.Text == "999")
                {
                    ddlqty.SelectedValue = "1";
                    txtqty.Enabled = false;
                }
                else if (txtqty.Text == "0")
                {
                    ddlqty.SelectedValue = "3";
                    txtqty.Enabled = false;
                }
                else
                    ddlqty.SelectedValue = "2";
            }
            
        }
    }
}