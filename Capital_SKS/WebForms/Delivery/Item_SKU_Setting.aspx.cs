
using ORS_RCM_BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ORS_RCM.WebForms.Delivery
{
    public partial class Item_SKU_Setting : System.Web.UI.Page
    {
        static Item_BL ibl = new Item_BL();
        static DataTable dtSku = new DataTable();
        private string objDate;
        string FilePath = ConfigurationManager.AppSettings["ExportFieldCSVPath"].ToString();
        public int Userid
        {
            get
            {
                if (Session["User_ID"] != null)

                    return Int32.Parse(Session["User_ID"].ToString());
                else
                    return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPageloadItem();
            }
            else
            {
                //if (ViewState["dt"] != null)
                //{
                    ibl = new Item_BL();
                    String ctrl = getPostBackControlName();
                    if (ctrl.Contains("lnkPaging"))
                    {
                        gp.LinkButtonClick(ctrl, gdvItemSetting.PageSize);
                        Label lbl = gp.FindControl("lblCurrent") as Label;
                        int index = Convert.ToInt32(lbl.Text);
                        //DataSet dt1 = ibl.BindItem(txtBrandName.Text, txtItemCode.Text, gdvItemSetting.PageSize, index);
                        ArrayList arrlst = new ArrayList();
                        if (ViewState["checkedValue"] != null)
                        {
                            arrlst = ViewState["checkedValue"] as ArrayList;
                        }
                        if (ViewState["checkedValue"] != null)
                        {
                            string option = "2";

                            if (txtItemCode.Text != "")
                            {
                                DataSet dt1 = ibl.BindItem(txtItemCode.Text,txtItemName.Text,txtBrandName.Text, gdvItemSetting.PageSize, index, option);
                                dtSku = dt1.Tables[0];
                                gdvItemSetting.DataSource = dtSku;
                                gdvItemSetting.DataBind();
                            }
                            else if (txtItemName.Text != "")
                            {
                                DataSet dt2 = ibl.BindItemName(txtItemName.Text);
                                dtSku = dt2.Tables[0];
                                gdvItemSetting.DataSource = dtSku;
                                gdvItemSetting.DataBind();
                            }
                            else if (txtBrandName.Text != "")
                            {
                                DataSet dt3 = ibl.BindBrandName(txtBrandName.Text);
                                dtSku = dt3.Tables[0];
                                gdvItemSetting.DataSource = dtSku;
                                gdvItemSetting.DataBind();
                            }
                            else
                            {
                                DataSet dt4 = ibl.BindAllItem(gdvItemSetting.PageSize, index);
                                dtSku = dt4.Tables[0];
                                gdvItemSetting.DataSource = dtSku;
                                gdvItemSetting.DataBind();
                            }

                        }
                        else
                        {
                            string option = "3";

                            if (txtItemCode.Text != "")
                            {
                                DataSet dt1 = ibl.BindItem(txtItemCode.Text,txtItemName.Text,txtBrandName.Text, gdvItemSetting.PageSize, index, option);
                                dtSku = dt1.Tables[0];
                                gdvItemSetting.DataSource = dtSku;
                                gdvItemSetting.DataBind();
                            }
                            else if (txtItemName.Text != "")
                            {
                                DataSet dt2 = ibl.BindItemName(txtItemName.Text);
                                dtSku = dt2.Tables[0];
                                gdvItemSetting.DataSource = dtSku;
                                gdvItemSetting.DataBind();
                            }
                            else if (txtBrandName.Text != "")
                            {
                                DataSet dt3 = ibl.BindBrandName(txtBrandName.Text);
                                dtSku = dt3.Tables[0];
                                gdvItemSetting.DataSource = dtSku;
                                gdvItemSetting.DataBind();
                            }
                            else
                            {
                                DataSet dt4 = ibl.BindAllItem(gdvItemSetting.PageSize, index);
                                dtSku = dt4.Tables[0];
                                gdvItemSetting.DataSource = dtSku;
                                gdvItemSetting.DataBind();
                            }
                        }
                }
            }
        }
       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindItem();
        }

        public void BindItem()
        {
            int count = 0;
            string option = string.Empty ;
            if (chkItemCode.Checked == true)
                option = "2";
            else
                option = "3";
            
            gdvItemSetting.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
            int pagesize = int.Parse(ddlpage.SelectedValue.ToString());
            //DataSet dsSku = ibl.BindItem(txtBrandName.Text, txtItemCode.Text, gdvItemSetting.PageSize, 1);
            if (txtItemCode.Text != "")
            {
                DataSet dsSku1 = ibl.BindItem(txtItemCode.Text,txtItemName.Text,txtBrandName.Text, gdvItemSetting.PageSize, 1, option);
                dtSku = dsSku1.Tables[0];
                if (dtSku.Rows.Count > 0)
                    count = Convert.ToInt32(dsSku1.Tables[1].Rows[0][0]);
                gdvItemSetting.DataSource = dtSku;
                gdvItemSetting.DataBind();
                ViewState["dt"] = dtSku;
                gp.CalculatePaging(count, gdvItemSetting.PageSize, 1); 
          
            }
            else if (txtItemName.Text != "")
            {
                DataSet dsSku2 = ibl.BindItemName(txtItemName.Text);
                dtSku = dsSku2.Tables[0];
                if (dtSku.Rows.Count > 0)
                    count = Convert.ToInt32(dsSku2.Tables[1].Rows[0][0]);
                gdvItemSetting.DataSource = dtSku;
                gdvItemSetting.DataBind();
                ViewState["dt"] = dtSku;
                gp.CalculatePaging(count, gdvItemSetting.PageSize, 1);
             
            }
            else if (txtBrandName.Text != "")
            {
                DataSet dsSku3 = ibl.BindBrandName(txtBrandName.Text);
                dtSku = dsSku3.Tables[0];
                if (dtSku.Rows.Count > 0)
                    count = Convert.ToInt32(dsSku3.Tables[1].Rows[0][0]);
                gdvItemSetting.DataSource = dtSku;
                gdvItemSetting.DataBind();
                ViewState["dt"] = dtSku;
                gp.CalculatePaging(count, gdvItemSetting.PageSize, 1);

            }
            else
            {
                BindAllItem();
            }
            ViewState.Remove("checkedValue");
            
        }
        public void BindAllItem()
        {
            gdvItemSetting.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
            int pagesize = int.Parse(ddlpage.SelectedValue.ToString());
            DataSet dsSku = ibl.BindAllItem(gdvItemSetting.PageSize, 1);
            dtSku = dsSku.Tables[0];
            int count = 0;
            if (dtSku.Rows.Count > 0)
                count = Convert.ToInt32(dsSku.Tables[1].Rows[0][0]);
            gdvItemSetting.DataSource = dtSku;
            gdvItemSetting.DataBind();
            ViewState["dt"] = dtSku;
            gp.CalculatePaging(count, gdvItemSetting.PageSize, 1);
        }

        public void BindPageloadItem()
        {
            gdvItemSetting.PageSize = int.Parse(ddlpage.SelectedValue.ToString());
            int pagesize = int.Parse(ddlpage.SelectedValue.ToString());
            //DataSet dsSku = ibl.BindPageloadItem(txtBrandName.Text, txtItemCode.Text, gdvItemSetting.PageSize, 1, "1");
            DataSet dsSku = ibl.BindPageloadItem(txtItemCode.Text, gdvItemSetting.PageSize, 1, "1");
            dtSku = dsSku.Tables[0];
            int count = 0;
            if (dtSku.Rows.Count > 0)
                count = Convert.ToInt32(dsSku.Tables[1].Rows[0][0]);
            gdvItemSetting.DataSource = dtSku;
            gdvItemSetting.DataBind();
            ViewState["dt"] = dtSku;
            gp.CalculatePaging(count, gdvItemSetting.PageSize, 1);
        }

        protected void gdvItemSetting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlR = (DropDownList)e.Row.FindControl("ddlRShipping");
                ddlR.DataSource = ibl.DDLRShipping();
                ddlR.DataTextField = "Order";
                ddlR.DataValueField = "Rakuten_Shipping_Number";
                ddlR.DataBind();
                ddlR.Items.Insert(0, "");
                int index = e.Row.RowIndex;
                if (dtSku.Rows[index]["Rakuten_ShippingNo"].ToString() != "")
                    ddlR.Items.FindByValue(dtSku.Rows[index]["Rakuten_ShippingNo"].ToString()).Selected = true;
               
                DropDownList ddlY = (DropDownList)e.Row.FindControl("ddlYShipping");
                ddlY.DataSource = ibl.DDLYShipping();
                ddlY.DataTextField = "Order";
                ddlY.DataValueField = "Yahoo_Shipping_Number";
                ddlY.DataBind();
                ddlY.Items.Insert(0, "");
                if (dtSku.Rows[index]["Yahoo_ShippingNo"].ToString() != "")
                    ddlY.Items.FindByValue(dtSku.Rows[index]["Yahoo_ShippingNo"].ToString()).Selected = true;


            }
        }

        protected void chk_SKU(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;
                GridViewRow row = chk.NamingContainer as GridViewRow;
                int rowIndex = row.RowIndex;
                Label lblID = gdvItemSetting.Rows[rowIndex].FindControl("lblID") as Label;
                if (ViewState["CheckValue"] != null)
                {
                    ArrayList arrlist = ViewState["CheckValue"] as ArrayList;
                    CheckBox chkcheck = gdvItemSetting.Rows[rowIndex].FindControl("chk") as CheckBox;
                    if (!chkcheck.Checked)
                    {
                        arrlist.Remove(lblID.Text);
                        ViewState["checkedValue"] = arrlist;

                    }
                    else if (!arrlist.Contains(lblID.Text))
                    {
                        arrlist.Add(lblID.Text);
                        ViewState["checkedValue"] = arrlist;
                    }
                }
                else
                {
                    ArrayList arrlst = new ArrayList();
                    arrlst.Add(lblID.Text);
                    ViewState["CheckValue"] = arrlst;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAllCheck_Click(object sender, EventArgs e)
        {
            CheckAllGrid(true);
            CheckAllCheckBox();
        }

        protected void btnAllCancel_Click(object sender, EventArgs e)
        {
            CheckAllGrid(false);
            CheckAllCheckBox();
        }

        public void CheckAllGrid(Boolean check)
        {
            ArrayList arrlst = new ArrayList();
            if (check == true)
            {
                for (int i = 0; i < gdvItemSetting.Rows.Count; i++)
                {
                    Label lblID = gdvItemSetting.Rows[i].FindControl("lblID") as Label;
                    CheckBox chk = gdvItemSetting.Rows[i].FindControl("chk") as CheckBox;

                    if (chk.Enabled)
                    {
                        arrlst.Add(lblID.Text);

                    }

                }

            }
            else
            {
                for (int i = 0; i < gdvItemSetting.Rows.Count; i++)
                {
                    Label lbl = gdvItemSetting.Rows[i].FindControl("lblID") as Label;
                    CheckBox chk = gdvItemSetting.Rows[i].FindControl("chk") as CheckBox;
                    if (chk.Enabled)
                    {
                        arrlst.Remove(lbl.Text);
                    }

                }
            }
            ViewState["CheckValue"] = arrlst;
        }

        public void CheckAllCheckBox()
        {
            if (ViewState["CheckValue"] != null)
            {
                ArrayList arrlst = ViewState["CheckValue"] as ArrayList;
                CheckBox chkbox;
                Label lbl;

                for (int i = 0; i < gdvItemSetting.Rows.Count; i++)
                {
                    chkbox = gdvItemSetting.Rows[i].FindControl("chk") as CheckBox;
                    lbl = gdvItemSetting.Rows[i].FindControl("lblID") as Label;
                    if (arrlst.Contains(lbl.Text))
                    {

                        chkbox.Checked = true;
                    }
                    else
                        chkbox.Checked = false;
                }
            }
        }

        public DataTable NewDataTable()
        {
            DataTable dtSKUlist = new DataTable();
            dtSKUlist.Columns.Add(new DataColumn("ID", typeof(Int32)));
            dtSKUlist.Columns.Add(new DataColumn("Rakuten_Shipping", typeof(string)));
            dtSKUlist.Columns.Add(new DataColumn("Yahoo_Shipping", typeof(string)));
            dtSKUlist.Columns.Add(new DataColumn("Delivery_SettingDate", typeof(string)));
            dtSKUlist.AcceptChanges();
            return dtSKUlist;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ViewState["CheckValue"] != null)
            {
                ArrayList arrlist = ViewState["CheckValue"] as ArrayList;
                string idlist = string.Empty;
                // add ID to string with comma
                if (arrlist.Count > 0)
                {
                    for (int j = 0; j < arrlist.Count; j++)
                    {
                        if (j == arrlist.Count - 1)
                        {
                            idlist += arrlist[j].ToString();
                        }
                        else
                        {
                            idlist += arrlist[j].ToString() + ",";
                        }
                    }
                    DataTable dtSKUlist = NewDataTable();
                    DataRow dr = null;
                    for (int i = 0; i < gdvItemSetting.Rows.Count; i++)
                    {
                        Label lblID = gdvItemSetting.Rows[i].FindControl("lblID") as Label;
                        DropDownList ddlRShipping = (DropDownList)gdvItemSetting.Rows[i].FindControl("ddlRShipping");
                        DropDownList ddlYShipping = (DropDownList)gdvItemSetting.Rows[i].FindControl("ddlYShipping");
                        TextBox lbldate = gdvItemSetting.Rows[i].FindControl("txteddate") as TextBox;
                        if (arrlist.Contains(lblID.Text))
                        {
                            dr = dtSKUlist.NewRow();
                            dr["ID"] = lblID.Text;
                            dr["Rakuten_Shipping"] = ddlRShipping.SelectedValue;
                            dr["Yahoo_Shipping"] = ddlYShipping.SelectedValue;
                            //dr["Delivery_SettingDate"] = lbldate.Text;
                            dtSKUlist.Rows.Add(dr);
                            dtSKUlist.AcceptChanges();
                        }
                    }
                    ibl.SKU_Save(dtSKUlist);
                    CheckAllGrid(false);
                    CheckAllCheckBox();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Save Successful!!')", true);
                    BindPageloadItem();
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Must Check to Update SKU!!')", true);
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            DataSet dsExport = new DataSet();
            if (chkItemCode.Checked == true)
            {
                dsExport = ibl.BindItemExport(txtItemCode.Text, 1);
            }
            else { dsExport = ibl.BindItemExport(txtItemCode.Text,2); }
            DataTable dtcsv = dsExport.Tables[0];
                
            if (dtcsv.Columns.Contains("ID"))
                dtcsv.Columns.Remove("ID");
            if (dtcsv.Columns.Contains("No"))
                dtcsv.Columns.Remove("No");
            ColumnNameChange(dtcsv);
            string filename = "Delivery_Export" + ".csv";
            CSV(dtcsv, filename);
            lnkdownload.Text = filename;
            //ViewState["Option"] = String.Empty;
            GlobalUI.MessageBox("Successful Export CSV!");
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            String Delivery = String.Empty;

            if (uplDelivery_Import.HasFile)//check item master file select
            {
                String filename = Path.GetFileName(uplDelivery_Import.PostedFile.FileName);
                if (check(filename) == true)
                {
                    uplDelivery_Import.SaveAs(Server.MapPath("~/Import_CSV/") + filename);//save file
                    Delivery = filename;
                }
                Response.Redirect("~/WebForms/Delivery/Item_SKU_Setting_Confirm.aspx?Delivery=" +Delivery , false);
            }
          
           
        }
        //protected void DeliveryImport(string delivery)
        //{
        //    String Delivery = Server.MapPath("~/Import_CSV/") + delivery;
        //    DataTable dt = GlobalUI.CSVToTable(Delivery);
        //    dt = GlobalUI.Remove_Doublecode(dt);
        //    //dt = dtSku.Copy();
        //    //dt = ColumnNameChange(dt);
        //    String[] colName = { "商品番号", "カラー", "サイズ", "カラーコード", "サイズコード", "楽天発送番号", "ヤフー発送番号"};
        //    if (CheckColumn(colName, dt))//check datatable column's header is true
        //    {
        //        dt = ColumnNameEng(dt);//change japanese header to english
        //        DataColumn newcol = new DataColumn("チェック", typeof(String));//add check column to datatable that show error or not
        //        newcol.DefaultValue = "";
        //        dt.Columns.Add(newcol);//add check column to datatable that show error or not
        //        DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
        //        newColumn.DefaultValue = 6;//type 6 = ok , type 5 = error
        //        dt.Columns.Add(newColumn);//add type column to datatable to seperate error record and  ok record

        //        DataColumn dc = new DataColumn("エラー内容", typeof(String));
        //        dc.DefaultValue = "";
        //        dt.Columns.Add(dc);//add error detail column to datatabel to show error message detail

        //        String[] colCheckLength = { "Item_Code", "Color_Code", "Size_Code" };//need to check this column value's length is greater than 50
        //        DataTable dterr = CheckLength(dt, colCheckLength, 50, 1);

        //        String[] colCheckLength1 = { "Color_Name", "Size_Name" };//need to check this column value's length is greater than 200
        //        dterr = CheckLength(dterr, colCheckLength1, 200, 1);

        //        String[] colCheckType = { "Rakuten_ShippingNo","Yahoo_ShippingNo" };
        //        dterr = checkIntType(dterr, colCheckType, 2);//need to check this column value is integer 

                

        //        gdvItemSetting.DataSource = dterr;

               


        //        Cache.Insert("dtdelivery", dterr, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);//save datatable to cache
        //        BindPageloadItem();
        //    }
        //    else
        //    {
        //        GlobalUI.MessageBox("File Format Wrong!");
        //    }



        //    if (Cache["dtdelivery"] != null)
        //    {
        //        string delivery_setting = string.Empty;
        //        delivery_setting = DeliverySetting();
        //        Cache.Remove("dtdelivery");
        //    }
        //}
        //protected   String  DeliverySetting()
        //{
        //    try
        //    {
        //        Import_Item_BL itbl = new Import_Item_BL();
        //        Item_ImportLog_BL ItemImbl = new Item_ImportLog_BL();

        //        if (Cache["dtdelivery"] != null)
        //        {
        //            DataTable dt = Cache["dtdelivery"] as DataTable;
        //            DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));
        //            newColumn.DefaultValue = Userid;
        //            dt.Columns.Add(newColumn);//add imported user
        //            int result = itbl.Delivery (dt);//insert data
        //            if (result > 0)
        //            {
        //                return result.ToString();
        //            }
        //            return String.Empty;
        //        }
        //        else
        //        {
        //            GlobalUI.MessageBox("Incorrect Delivery File Format!");
        //            return String.Empty;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //string line = ex.Message.ToString();
        //        //string replaceWith = "";
        //        //line.Replace(System.Environment.NewLine, "replacement text");
        //        //string line2 = Regex.Replace(line, @"\r\n?|\n", replaceWith);
        //        //hfdmsg.Value = line2;
        //        return String.Empty;
        //    }
        //}
    

        protected DataTable  ColumnNameChange(DataTable dt)
        {
            try
            {
                dt.Columns["Item_Code"].ColumnName = "商品番号";
                dt.Columns["Size_Code"].ColumnName = "サイズコード";
                dt.Columns["Color_Code"].ColumnName = "カラーコード";
                dt.Columns["Size_Name"].ColumnName = "サイズ";
                dt.Columns["Color_Name"].ColumnName = "カラー";
                dt.Columns["Rakuten_ShippingNo"].ColumnName = "楽天発送番号";
                dt.Columns["Yahoo_ShippingNo"].ColumnName = "ヤフー発送番号";
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }
        protected DataTable  ColumnNameEng(DataTable dt)
        {
            try
            {
                dt.Columns["商品番号"].ColumnName = "Item_Code";
                dt.Columns["サイズコード"].ColumnName = "Size_Code";
                dt.Columns["カラーコード"].ColumnName = "Color_Code";
                dt.Columns["サイズ"].ColumnName = "Size_Name";
                dt.Columns["カラー"].ColumnName = "Color_Name";
                dt.Columns["楽天発送番号"].ColumnName = "Rakuten_ShippingNo";
                dt.Columns["ヤフー発送番号"].ColumnName = "Yahoo_ShippingNo";
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }

        protected void CSV(DataTable dt, string name)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter((FilePath + name), false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dt, writer, true);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            try
            {

                if (includeHeaders)
                {
                    List<string> headerValues = new List<string>();

                    foreach (DataColumn column in sourceTable.Columns)
                    {
                        headerValues.Add(column.ColumnName.ToUpper());
                    }
                  

                    StringBuilder builder = new StringBuilder();
                    writer.WriteLine(String.Join(",", headerValues.ToArray()));

                }


                string[] items = null;
                foreach (DataRow row in sourceTable.Rows)
                {
                    //items = row.ItemArray.Select(o => o.ToString()).ToArray();
                    items = row.ItemArray.Select(o => QuoteValue(o.ToString())).ToArray();
                    writer.WriteLine(String.Join(",", items));
                }

                writer.Flush();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        private string QuoteValue(string value)
        {
            return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gdvItemSetting.PageSize = Convert.ToInt32(ddlpage.SelectedValue);
                if (String.IsNullOrWhiteSpace(txtItemCode.Text))
                {
                    BindPageloadItem();
                }
                else
                {
                    BindItem();
                }

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
                string ctrlname = Page.Request.Params["__EVENTTARGET"];
                if (ctrlname != null && ctrlname != String.Empty)
                {
                    control = Page.FindControl(ctrlname);
                }
                else
                {
                    string ctrlStr = String.Empty;
                    Control c = null;
                    foreach (string ctl in Page.Request.Form)
                    {
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

        protected void lnkdownload_Click(object sender, EventArgs e)
        {
            //try
            //{
            Download(FilePath + lnkdownload.Text);

            //}
            //catch (Exception ex)
            //{
            //    Session["Exception"] = ex.ToString();
            //    Response.Redirect("~/CustomErrorPage.aspx?");
            //}
        }

        protected void Download(string filecheck)
        {
            //try
            //{
            if (File.Exists(filecheck))
            {
                string filename = lnkdownload.Text;
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                //response.AddHeader("Content-Disposition","attachment;filename=\""+filecheck+"\"");
                response.ContentType = "application/octet-stream";
                byte[] data = req.DownloadData(filecheck);
                response.BinaryWrite(data);
                response.End();

            }
            else
            {
                GlobalUI.MessageBox("File doesn't exist!");
            }

            // }
            //catch (Exception ex)
            //{
            //    Session["Exception"] = ex.ToString();
            //    Response.Redirect("~/CustomErrorPage.aspx?");
            //}
        }

        private bool check(String str)
        {
            try
            {
                if (str.Contains(".csv"))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}