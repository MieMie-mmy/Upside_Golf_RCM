using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;
using ORS_RCM_BL;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ORS_RCM
{
    public partial class Smart_Template_Entry : System.Web.UI.Page
    {
        Smart_Template_Entity smartTempentity;
        Smart_Template_BL smartTemBl;
        Shop_Template_BL shopTemBl;
        Shop_Template_Entity shopTemEnt;
        string[] sdi = new string[100];
        
        public int ID
        {
            get
            {
                if (Request.QueryString["ID"] != null)
                {
                    return int.Parse(Request.QueryString["ID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region main page
                if (Request.QueryString.AllKeys.Contains("ID"))
                {
                   // ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();

                    smartTemBl = new Smart_Template_BL();

                    if (ID != 0)
                    {
                        head.InnerText = "スマートテンプレート編集";
                        //ID = Convert.ToInt32(Request.QueryString["ID"].ToString());
                        DataTable dt = smartTemBl.SelectbyID(ID);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            txtSmartTemplate.Text = dt.Rows[0]["Template_ID"].ToString();
                            txtname.Text = dt.Rows[0]["Template_name"].ToString();
                            string str = dt.Rows[0]["Status"].ToString();
                            if (str == "1")
                            {
                                chkstatus.Checked = true;
                            }
                            else
                            {
                                chkstatus.Checked = false;
                            }

                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Shop_ID"].ToString()))
                            {

                                DataTable dts = smartTemBl.GetShopTable();

                                for (int i = 0; i < dts.Rows.Count; i++)
                                {
                                    for (int j = 0; j < dt.Rows.Count; j++)
                                    {
                                        #region Cancel
                                        //if (dt.Rows[k]["Shop_ID"].ToString() != dts.Rows[i]["Shop_ID"].ToString())
                                        //{

                                        //    DataRow drTemp = dt.NewRow();
                                        //    //drTemp[0] = dts.Rows[i][0];
                                        //    //drTemp[1] = "";
                                        //    //drTemp[2] = 0;
                                        //    //drTemp[3] = dts.Rows[i]["Template_Description"];
                                        //    //drTemp[4] = dts.Rows[i]["Shop_Name"];
                                        //    //drTemp[5] = dts.Rows[i]["Shop_ID"];
                                        //    dt.Rows.Add(drTemp);



                                        //}
                                        #endregion
                                        if (dts.Rows[i]["Shop_ID"].ToString().Equals(dt.Rows[j]["Shop_ID"].ToString()))
                                        {
                                            dts.Rows[i]["Template_Description"] = dt.Rows[j]["Template_Description"].ToString();
                                            break;
                                        }
                                    }
                                }

                                dlSmartTemplate.DataSource = dts;
                                dlSmartTemplate.DataBind();
                                //btnConfirm_Save.Text = "更新する";
                            }
                            else
                            {
                                getShopList();
                            }
                        }
                        else { getShopList();  }
                    }
                   
                }
                #endregion
                else
                {
                    getShopList();
                    head.InnerText = "スマートテンプレート登録";
                }
            }
            else
            {
                #region popup page
                //if (Request.QueryString.AllKeys.Contains("PopupID"))
                btnpopup.Visible = false;
                btnConfirm_Save.Visible = true;
              //  btnConfirm_Save.Text = "登録";
                Confirm();
             
                #endregion
            }

         }

        protected Boolean Check_SpecialCharacter(String[] columnName, DataTable dt)
        {
            try
            {
                DataColumnCollection col = dt.Columns;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        for (int k = 0; k < columnName.Length; k++)
                        {
                            if (dt.Columns[j].ColumnName == columnName[k])
                            {

                                string input = dt.Rows[i][j].ToString();
                                string specialChar = @"㈰㈪㈫㈬㈭㈮㈯㉀㈷㉂㉃㈹㈺㈱㈾㈴㈲㈻㈶㈳㈵㈼㈽㈿㈸㊤㊥㊦㊧㊨㊩㊖㊝㊘㊞㊙㍾㍽㍼㍻㍉㌢㌔㌖㌅㌳㍎㌃㌶㌘㌕㌧㍑㍊㌹㍗㌍㍂㌣㌦㌻㌫㍍№℡㎜㎟㎝㎠㎤㎡㎥㎞㎢㎎㎏㏄㎖㎗㎘㎳㎲㎱㎰①②③④⑤⑥⑦⑧⑨⑩⑪⑫⑬⑭⑮⑯⑰⑱⑲⑳ⅠⅡⅢⅣⅤⅥⅦⅧⅨⅩⅰⅱⅲⅳⅴⅵⅶⅷⅸⅹ";
                                foreach (var item in specialChar)
                                {
                                    if (input.Contains(item))
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Smart Template description contains special character.');", true);
                                        return true;
                                    }
                                }

                            }
                        }
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

        private void getShopList()
        {
            smartTemBl = new Smart_Template_BL();
            smartTempentity = new Smart_Template_Entity();

            DataTable dt = smartTemBl.GetShopTable();

            dlSmartTemplate.DataSource = dt;
            dlSmartTemplate.DataBind();
        }

        public void insertData(Smart_Template_Entity smartTempentity)
        {
            if (chkstatus.Checked == true)
            {
                smartTempentity.Status = 1;
            }
            else
            {
                smartTempentity.Status = 0;
            }

            if (txtSmartTemplate.Text != null)
            {
                smartTempentity.Template_ID = txtSmartTemplate.Text;
            }



        }

        public void addData(Shop_Template_Entity shopTemEnt)
        {
            shopTemBl = new Shop_Template_BL();
            DataTable dt = new DataTable();

            dt.Columns.Add("Template_ID", typeof(int));
            dt.Columns.Add("Shop_ID", typeof(int));
            dt.Columns.Add("Template_Description", typeof(string));
            dt.Columns.Add("type", typeof(string));
            int TempID = Convert.ToInt32(shopTemEnt.TempID);
            foreach (DataListItem item in dlSmartTemplate.Items)
            {
                Label lbl = item.FindControl("lblShopID") as Label;
                shopTemEnt.Shop_ID = Convert.ToInt32(lbl.Text);

                TextBox field = (TextBox)item.FindControl("txtTemplateDesc");
                shopTemEnt.Template_Description = field.Text;

                dt.Rows.Add(TempID, shopTemEnt.Shop_ID, shopTemEnt.Template_Description);
            }
            if (Request.QueryString["ID"] != null)
            {
                //shopTemBl.Insert(dt);//update
                shopTemBl.Update(dt);
            }
            else
            {
                shopTemBl.Insert(dt);
            }


        }

        protected void Confirm()
        {
            lblname.Visible = true;
            lblname.Text = txtname.Text;
            txtname.Visible = false;
            if (!chkstatus.Checked)
                lblcheckStatus.Text = "なし";
            chkstatus.Visible = false;
            lblcheckStatus.Visible = true;
            lblSmartTemplate.Visible = true;
            lblSmartTemplate.Text = txtSmartTemplate.Text;
            txtSmartTemplate.Visible = false;
            if (ID != 0)
            {
                btnConfirm_Save.Text = "更新";
            }
            else
            {
                btnConfirm_Save.Text = "登録";
            }

            //show label and hide textbox for Confirm
            for (int i = 0; i < dlSmartTemplate.Items.Count; i++)
            {
                TextBox txt = dlSmartTemplate.Items[i].FindControl("txtShop_Name") as TextBox;
                txt.Visible = false;

                Label lbl = dlSmartTemplate.Items[i].FindControl("lblShopName") as Label;
                lbl.Visible = true;

                txt = dlSmartTemplate.Items[i].FindControl("txtTemplateDesc") as TextBox;
                if (txt.Text.Length == 0)
                    txt.Text = "  ";//to remove placeholder text
                txt.ReadOnly = true;
                txt.BorderStyle = BorderStyle.None;
            }

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('../Mall/Smart_Template_Entry.aspx','_newtab');", true);
        }

        protected void Save()
        {
            smartTemBl = new Smart_Template_BL();
            smartTempentity = new Smart_Template_Entity();
            shopTemEnt = new Shop_Template_Entity();

            if (ID != 0)
            {
                //smartTempentity.ID = Convert.ToInt32(Request.QueryString["ID"].ToString());
                smartTempentity.ID = ID;
                smartTempentity.Template_ID = txtSmartTemplate.Text;
                smartTempentity.Templatename = txtname.Text;
                if (chkstatus.Checked == true)
                {
                    smartTempentity.Status = 1;
                }
                else
                {
                    smartTempentity.Status = 0;
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("Template_Description", typeof(string));
                foreach (DataListItem item in dlSmartTemplate.Items)
                {
                    TextBox field = (TextBox)item.FindControl("txtTemplateDesc");
                    shopTemEnt.Template_Description = field.Text;

                    dt.Rows.Add(shopTemEnt.Template_Description);
                }
                String[] colName = { "Template_Description" };
                if (!Check_SpecialCharacter(colName, dt))
                {
                    smartTemBl.Deletedata(ID);
                    int tID = smartTemBl.Insert(smartTempentity);
                    shopTemEnt.TempID = tID;
                    addData(shopTemEnt);

                    ViewState["UrlReferrer"] = "Smart_Template_View.aspx";
                    //GlobalUI.MessageBox("Update Success");
                    string result = "Update Success";
                    if (result == "Update Success")
                    {
                        object referrer = ViewState["UrlReferrer"];
                        string url = (string)referrer;
                        string script = "window.onload = function(){ alert('";
                        script += result;
                        script += "');";
                        script += "window.location = '";
                        script += url;
                        script += "'; }";
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                    }
                }
            }
            else
            {
                insertData(smartTempentity);
                smartTempentity.Template_ID = txtSmartTemplate.Text;
                smartTempentity.Templatename = txtname.Text;
                if (chkstatus.Checked == true)
                {
                    smartTempentity.Status = 1;
                }
                else
                {
                    smartTempentity.Status = 0;
                }
                if (!String.IsNullOrWhiteSpace(txtSmartTemplate.Text.Trim()))
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Template_Description", typeof(string));
                    foreach (DataListItem item in dlSmartTemplate.Items)
                     {
                         TextBox field = (TextBox)item.FindControl("txtTemplateDesc");
                         shopTemEnt.Template_Description = field.Text;

                         dt.Rows.Add(shopTemEnt.Template_Description);
                     }
                    String[] colName = { "Template_Description" };
                    if (!Check_SpecialCharacter(colName, dt))
                    {
                        smartTemBl.Deletedata(ID);
                        int id = smartTemBl.Insert(smartTempentity);
                        shopTemEnt.TempID = id;
                        addData(shopTemEnt);
                        ViewState["UrlReferrer"] = "Smart_Template_View.aspx";
                        string result = "Save Success";
                        if (result == "Save Success")
                        {
                            object referrer = ViewState["UrlReferrer"];
                            string url = (string)referrer;
                            string script = "window.onload = function(){ alert('";
                            script += result;
                            script += "');";
                            script += "window.location = '";
                            script += url;
                            script += "'; }";
                            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                        }
                    }
                }
                else
                    GlobalUI.MessageBox("Please Insert テンプレートID");
            }
        }

        protected void btnConfirm_Save_Click(object sender, EventArgs e)
        {
            //if (btnConfirm_Save.Text.Equals("確認画面へ"))
            //{
             
            //    btnConfirm_Save.Text = "登録";
              
            //}
            //else if (btnConfirm_Save.Text.Equals("登録"))
            //{
                Save();
           // }
        }

        public void AddNewData()
        {
            DataTable dt = new DataTable();

            if (Session["myDatatable"] != null)
            {
                dt = (DataTable)Session["myDatatable"];
            }
            else
            {
                dt.Columns.Add("Status");
                dt.Columns.Add("Template_ID");


                dt.Columns.Add("Shop_ID");
                dt.Columns.Add("Shop_Name");
                dt.Columns.Add("Template_Description");


            }
            DataRow drow = dt.NewRow();

            if (chkstatus.Checked == true)
            {
                drow["Status"] = "有効";

            }
            else
            {
                drow["Status"] = "無効";

            }

            drow["Template_ID"] = txtSmartTemplate.Text.ToString();

            dt.Rows.Add(drow);

                foreach (DataListItem item in dlSmartTemplate.Items)
                {

                   DataRow drows = dt.NewRow();


                   TextBox txtShop_ID= (TextBox)(item.FindControl("txtShop_ID"));
                   drows["Shop_ID"] = Convert.ToInt32(txtShop_ID.Text);

                    Label lblShopName = item.FindControl("lblShopName") as Label;
                    drows["Shop_Name"] = lblShopName.Text;


                    TextBox field = (TextBox)item.FindControl("txtTemplateDesc");
                    drows["Template_Description"] = field.Text;


                    dt.Rows.Add(drows);

                   
                }

                Session["myDatatable"] = dt;

            }

        protected void dlSmartTemplate_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Label lbl = e.Item.FindControl("lbltemplateNo") as Label;
            int i = Convert.ToInt32(hfCount.Value);
            lbl.Text = lbl.Text + i++;
            hfCount.Value = i.ToString();
        }
    }
}




















    


    

