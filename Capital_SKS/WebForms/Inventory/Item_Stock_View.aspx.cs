/* 
Created By              : Aung Kyaw
Created Date          : 03/07/2014
Updated By             :
Updated Date         :Item_Master,Item

 Tables using: 
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
using System.Data;
using Upside_Golf_RCM_BL;
using Upside_Golf_RCM_Common;
using System.Web.Services;
using System.IO;
using System.Configuration;

namespace Upside_Golf_RCM 
{
    public partial class Item_Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtItemNumber.Attributes.Add("onKeyPress", "doClick('" + btnSearch.ClientID + "',event)");
            try
            {
            if (!IsPostBack)
            {
                hdfSearch.Value = "false";
                BindGridViewItem();
            }
            else 
            {
                String ctrl = getPostBackControlName();
                if (ctrl == null)
                {
                    ctrl = String.Empty;
                }

                if (ctrl.Contains("lnkPaging"))
                {
                    Item_BL itemBL = new Item_BL();
                    DataTable dt = new DataTable();

                    gp.LinkButtonClick(ctrl, gvItem.PageSize);

                    Label lbl = gp.FindControl("lblCurrent") as Label;
                    int index = Convert.ToInt32(lbl.Text);

                    dt = Search(index-1, 1);
                    gvItem.DataSource = dt;
                    gvItem.DataBind();

                    };
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }  
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
           try
           {
             Update();
           }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }  
        }

        [WebMethod]
        public static string updateQuantity(string id, string quantity, string jisha_quantity)
        {
            Item_BL itemBL = new Item_BL();
            itemBL.UpdateQuantity(id, quantity, jisha_quantity);
            string qtylog = "ID:" + id + " Quantity:" + quantity + " Jisha_Quantity:" + jisha_quantity+Environment.NewLine;
            ConsoleWriteLine_Tofile(qtylog);
            return "true";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
          try
          {
              BindGridViewItem();
          }
          catch (Exception ex)
          {
            Session["Exception"] = ex.ToString();
            Response.Redirect("~/CustomErrorPage.aspx?");
          }  
        }

        protected void gvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
              {
                gvItem.PageIndex = e.NewPageIndex;
                BindGridViewItem();
              }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }  
        }

        protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txt = e.Row.FindControl("txtStockQuantity") as TextBox;
                    txt.Attributes["onfocus"] = "javascript:this.select();";
                    
                    Label lbl = e.Row.FindControl("lblListPrice") as Label;
                    if (!String.IsNullOrWhiteSpace(lbl.Text))
                    {
                        int i = Convert.ToInt32(lbl.Text);
                        lbl.Text = i.ToString("#,#");
                    }

                    lbl = e.Row.FindControl("lblSalePrice") as Label;
                    if (!String.IsNullOrWhiteSpace(lbl.Text))
                    {
                        int i = Convert.ToInt32(lbl.Text);
                        lbl.Text = i.ToString("#,#");
                    }

                    lbl = e.Row.FindControl("lblCost") as Label;
                    if (!String.IsNullOrWhiteSpace(lbl.Text))
                    {
                        int i = Convert.ToInt32(lbl.Text);
                        lbl.Text = i.ToString("#,#");
                    }
                    Label stockflag = (Label)e.Row.FindControl("LblStockflg");
                    CheckBox chkStockflg = (CheckBox)e.Row.FindControl("chkStockflg");
                    if (stockflag.Text == "0")
                    {
                        chkStockflg.Checked = true;
                    }
                    else
                    {
                        chkStockflg.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }  
        }

        protected void chkStockflg_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Item_BL itemBL = new Item_BL();
                CheckBox chklist = sender as CheckBox;
                GridViewRow row = chklist.NamingContainer as GridViewRow;
                int index = row.RowIndex;
                Label id = (Label)gvItem.Rows[index].FindControl("lblID");
                if (chklist.Checked)
                {
                    itemBL.UpdateStockFlag(Convert.ToInt32(id.Text),0);
                }
                else
                {
                    itemBL.UpdateStockFlag(Convert.ToInt32(id.Text),1);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Information")
                {
                    string Item_Code = e.CommandArgument.ToString();
                    Response.Redirect("../Item/Item_Master.aspx?Item_Code=" + Item_Code,false);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }         
        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindGridViewItem();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            } 
        }

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

        private void BindGridViewItem()
        {
            try
            {
                DataTable dt = new DataTable();
                if (chkCode.Checked)
                {
                    dt = Search(0, 2);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                        gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                        gvItem.DataSource = dt;
                        gvItem.DataBind();
                        gp.CalculatePaging(count, gvItem.PageSize, 1);
                    }
                    else
                    {
                        gvItem.DataSource = dt;
                        gvItem.DataBind();
                        gp.CalculatePaging(0, gvItem.PageSize, 1);
                    }
                }
                else
                {
                    dt = Search(0, 1);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int count = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());
                        gvItem.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
                        gvItem.DataSource = dt;
                        gvItem.DataBind();
                        gp.CalculatePaging(count, gvItem.PageSize, 1);
                    }
                    else
                    {
                        gvItem.DataSource = dt;
                        gvItem.DataBind();
                        gp.CalculatePaging(0, gvItem.PageSize, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void Update()
        {
            try
            {
                Item_BL itemBL = new Item_BL();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Quantity", typeof(int));
                int rowNo = Convert.ToInt32(hfrowNo.Value);

                if (rowNo >= 0)
                { 
                    int ID = Convert.ToInt32(gvItem.DataKeys[rowNo].Values[0]);
                    TextBox txtStockQuantity = (TextBox)gvItem.Rows[rowNo].Cells[1].FindControl("txtStockQuantity");
                    string quantity = txtStockQuantity.Text;
                    if (!String.IsNullOrEmpty(quantity))
                    {
                        dt.Rows.Add(ID, int.Parse(quantity));
                    }

                    itemBL.UpdateItem(dt);

                    String ctrl = hfctrl.Value;
                    if (!String.IsNullOrWhiteSpace(ctrl))
                    {
                        String[] str = ctrl.Split('_');
                        int row = Convert.ToInt32(str[str.Length - 1]);

                        if (row < gvItem.Rows.Count - 1)
                        {
                            row++;
                        }

                        TextBox txt = gvItem.Rows[row].FindControl("txtStockQuantity") as TextBox;
                        txt.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected Boolean IsSearch()
        {
            if (String.IsNullOrWhiteSpace(txtItemNumber.Text) && String.IsNullOrWhiteSpace(txtbrandname.Text) && String.IsNullOrWhiteSpace(txtcatinfo.Text) &&
               String.IsNullOrWhiteSpace(txtcompetitionname.Text) && String.IsNullOrWhiteSpace(txtjancode.Text) && String.IsNullOrWhiteSpace(txtprocode.Text) &&
               String.IsNullOrWhiteSpace(txtProductName.Text) && String.IsNullOrWhiteSpace(txtseason.Text) && String.IsNullOrWhiteSpace(txtyear.Text)
              )
            {
                return false;
            }
            else return true;
        }

        protected DataTable Search(int page_index,int option)
        {
            Item_BL itemBL = new Item_BL();

            String item_code = txtItemNumber.Text.TrimEnd(',').Trim();
            String item_name = txtProductName.Text.Trim();
            String product = txtprocode.Text.Trim();
            String cat_info = txtcatinfo.Text.Trim();
            String brand_name = txtbrandname.Text.Trim();
            String com_name = txtcompetitionname.Text.Trim();
            String year = txtyear.Text.Trim();
            String season = txtseason.Text.Trim();
            String jan_code =  txtjancode.Text.Trim();
            int page_size = Convert.ToInt32(ddlpage.SelectedValue.ToString());

            DataTable dt = itemBL.SearchItem(item_code, item_name, product, cat_info, brand_name, com_name, year, season, jan_code, page_index + 1, page_size, option,IsSearch());

            return dt;
        }

        public void MessageBox(string message)
        {
            try
            {
                if (message == "Saving Successful ! " || message == "Updating Successful ! ")
                {
                    Session["CatagoryList"] = null;
                    object referrer = ViewState["UrlReferrer"];
                    string url = (string)referrer;
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
                else
                {
                    string cleanMessage = message.Replace("'", "\\'");
                    string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                    {
                        page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "ItemShockViewLog.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            String date = DateTime.Now.ToString();
            Console.WriteLine(date);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
    }
}