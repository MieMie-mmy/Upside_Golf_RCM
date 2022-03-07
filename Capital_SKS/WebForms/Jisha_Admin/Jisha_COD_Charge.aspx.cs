using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;
using ORS_RCM_Common;

namespace ORS_RCM.WebForms.Jisha
{
    public partial class Jisha_COD_Charge : System.Web.UI.Page
    {
        Jisha_Delivery_Charge_BL jhbl;
        Jisha_Delivery_Charge_Entity jhentity;
        string result, prit; DataTable check;
        protected void Page_Load(object sender, EventArgs e)
        {
            jhbl = new Jisha_Delivery_Charge_BL();
            if (!IsPostBack)
            {
                string[] str = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "997", "998", "999" };
                for (int i = 0; i < str.Length; i++)
                {
                    prit += str[i].ToString() + ",";
                }
                check = jhbl.SelectAll(prit, 1);
                if (check != null && check.Rows.Count > 0)
                {
                    SetData(check);
                    
                }
                ddlpriority1.DataSource = jhbl.SlectDivision();
                ddlpriority1.DataTextField = "Division";
                ddlpriority1.DataValueField = "ID";
                ddlpriority1.DataBind();
                ddlpriority1.Items.Insert(0, "都道府県を選択");

                ddlpriority2.DataSource = jhbl.SlectDivision();
                ddlpriority2.DataTextField = "Division";
                ddlpriority2.DataValueField = "ID";
                ddlpriority2.DataBind();
                ddlpriority2.Items.Insert(0, "都道府県を選択");

                ddlpriority3.DataSource = jhbl.SlectDivision();
                ddlpriority3.DataTextField = "Division";
                ddlpriority3.DataValueField = "ID";
                ddlpriority3.DataBind();
                ddlpriority3.Items.Insert(0, "都道府県を選択");

                ddlpriority4.DataSource = jhbl.SlectDivision();
                ddlpriority4.DataTextField = "Division";
                ddlpriority4.DataValueField = "ID";
                ddlpriority4.DataBind();
                ddlpriority4.Items.Insert(0, "都道府県を選択");

                ddlpriority5.DataSource = jhbl.SlectDivision();
                ddlpriority5.DataTextField = "Division";
                ddlpriority5.DataValueField = "ID";
                ddlpriority5.DataBind();
                ddlpriority5.Items.Insert(0, "都道府県を選択");

                ddlpriority6.DataSource = jhbl.SlectDivision();
                ddlpriority6.DataTextField = "Division";
                ddlpriority6.DataValueField = "ID";
                ddlpriority6.DataBind();
                ddlpriority6.Items.Insert(0, "都道府県を選択");

                ddlpriority7.DataSource = jhbl.SlectDivision();
                ddlpriority7.DataTextField = "Division";
                ddlpriority7.DataValueField = "ID";
                ddlpriority7.DataBind();
                ddlpriority7.Items.Insert(0, "都道府県を選択");

                ddlpriority8.DataSource = jhbl.SlectDivision();
                ddlpriority8.DataTextField = "Division";
                ddlpriority8.DataValueField = "ID";
                ddlpriority8.DataBind();
                ddlpriority8.Items.Insert(0, "都道府県を選択");

                ddlpriority9.DataSource = jhbl.SlectDivision();
                ddlpriority9.DataTextField = "Division";
                ddlpriority9.DataValueField = "ID";
                ddlpriority9.DataBind();
                ddlpriority9.Items.Insert(0, "都道府県を選択");

                ddlpriority10.DataSource = jhbl.SlectDivision();
                ddlpriority10.DataTextField = "Division";
                ddlpriority10.DataValueField = "ID";
                ddlpriority10.DataBind();
                ddlpriority10.Items.Insert(0, "都道府県を選択");
            }
            else
            {
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                btnsubmit.Visible = false;
                btnSave.Visible = true;
                Confirm();
            }
        }
        private void Confirm()
        {
            ddlpriority1.Visible = false;
            ddlpriority10.Visible = false;
            ddlpriority2.Visible = false;
            ddlpriority3.Visible = false;
            ddlpriority4.Visible = false;
            ddlpriority5.Visible = false;
            ddlpriority6.Visible = false;
            ddlpriority7.Visible = false;
            ddlpriority8.Visible = false;
            ddlpriority9.Visible = false;

            dp1.Visible = true;
            dp10.Visible = true;
            dp3.Visible = true;
            dp2.Visible = true;
            dp4.Visible = true;
            dp5.Visible = true;
            dp6.Visible = true;
            dp7.Visible = true;
            dp8.Visible = true;
            dp9.Visible = true;


            txtdelivertycharge.Visible = false;
            txtdelivery10.Visible = false;
            txtdelivery2.Visible = false;
            txtdelivery3.Visible = false;
            txtdelivery4.Visible = false;
            txtdelivery5.Visible = false;
            txtdelivery6.Visible = false;
            txtdelivery7.Visible = false;
            txtdelivery8.Visible = false;
            txtdelivery9.Visible = false;
            txtdelivery997.Visible = false;
            txtdelivery998.Visible = false;
            txtdelivery999.Visible = false;

            lbldelivery.Visible = true;
            lbldelivery2.Visible = true;
            lbld3.Visible = true;
            lbld4.Visible = true;
            lbld5.Visible = true;
            lbld6.Visible = true;
            lbld7.Visible = true;
            lbld8.Visible = true;
            lbld9.Visible = true;
            lbld10.Visible = true;
            lbld997.Visible = true;
            lbld998.Visible = true;
            lbld999.Visible = true;

            txttotal.Visible = false;
            txttotal10.Visible = false;
            txttotal2.Visible = false;
            txttotal3.Visible = false;
            txttotal4.Visible = false;
            txttotal5.Visible = false;
            txttotal6.Visible = false;
            txttotal7.Visible = false;
            txttotal8.Visible = false;
            txttotal9.Visible = false;
            txttotal997.Visible = false;
            txttotal998.Visible = false;
            txttotal999.Visible = false;

            lbltotal.Visible = true;
            lbltotal2.Visible = true;
            lbltotal3.Visible = true;
            lbltotal4.Visible = true;
            lblt5.Visible = true;
            lblt6.Visible = true;
            lblt7.Visible = true;
            lblt8.Visible = true;
            lblt9.Visible = true;
            lblt10.Visible = true;
            lblt997.Visible = true;
            lblt998.Visible = true;
            lblt999.Visible = true;

            string value = ddlpriority1.SelectedValue;
            division(value, dp1);

            string value2 = ddlpriority2.SelectedValue;
            division(value2, dp2);
            string value3 = ddlpriority3.SelectedValue;
            division(value3, dp3);
            string value4 = ddlpriority4.SelectedValue;
            division(value4, dp4);
            string value5 = ddlpriority5.SelectedValue;
            division(value5, dp5);

            string value6 = ddlpriority6.SelectedValue;
            division(value6, dp6);
            string value7 = ddlpriority7.SelectedValue;
            division(value7, dp7);
            string value8 = ddlpriority8.SelectedValue;
            division(value8, dp8);
            string value9 = ddlpriority9.SelectedValue;
            division(value9, dp9);
            string value10 = ddlpriority10.SelectedValue;
            division(value10, dp10);

            lbldelivery.Text = txtdelivertycharge.Text;
            lbldelivery2.Text = txtdelivery2.Text;
            lbld3.Text = txtdelivery3.Text;
            lbld4.Text = txtdelivery4.Text;
            lbld5.Text = txtdelivery5.Text;
            lbld6.Text = txtdelivery6.Text;
            lbld7.Text = txtdelivery7.Text;
            lbld8.Text = txtdelivery8.Text;
            lbld9.Text = txtdelivery9.Text;
            lbld10.Text = txtdelivery10.Text;
            lbld997.Text = txtdelivery997.Text;
            lbld998.Text = txtdelivery998.Text;
            lbld999.Text = txtdelivery999.Text;

            lbltotal.Text = txttotal.Text;
            lbltotal2.Text = txttotal2.Text;
            lbltotal3.Text = txttotal3.Text;
            lbltotal4.Text = txttotal4.Text;
            lblt5.Text = txttotal5.Text;
            lblt6.Text = txttotal6.Text;
            lblt7.Text = txttotal7.Text;
            lblt8.Text = txttotal8.Text;
            lblt9.Text = txttotal9.Text;
            lblt10.Text = txttotal10.Text;
            lblt997.Text = txttotal997.Text;
            lblt998.Text = txttotal998.Text;
            lblt999.Text = txttotal999.Text;

        }

        private void division(string dvalue, Label dppriority)
        {
            switch (dvalue)
            {
                case "都道府県を選択": dppriority.Text = "都道府県を選択"; break;
                case "1": dppriority.Text = "北海道"; break;
                case "2": dppriority.Text = "青森県"; break;
                case "3": dppriority.Text = "岩手県"; break;
                case "4": dppriority.Text = "宮城県"; break;
                case "5": dppriority.Text = "秋田県"; break;
                case "6": dppriority.Text = "山形県"; break;
                case "7": dppriority.Text = "福島県"; break;
                case "8": dppriority.Text = "茨城県"; break;
                case "9": dppriority.Text = "栃木県"; break;
                case "10": dppriority.Text = "群馬県"; break;
                case "11": dppriority.Text = "埼玉県"; break;
                case "12": dppriority.Text = "千葉県"; break;
                case "13": dppriority.Text = "東京都"; break;
                case "14": dppriority.Text = "神奈川"; break;
                case "15": dppriority.Text = "新潟県"; break;
                case "16": dppriority.Text = "富山県"; break;
                case "17": dppriority.Text = "石川県"; break;
                case "18": dppriority.Text = "福井県"; break;
                case "19": dppriority.Text = "山梨県"; break;
                case "20": dppriority.Text = "長野県"; break;
                case "21": dppriority.Text = "岐阜県"; break;
                case "22": dppriority.Text = "静岡県"; break;
                case "23": dppriority.Text = "愛知県"; break;
                case "24": dppriority.Text = "三重県"; break;
                case "25": dppriority.Text = "滋賀県"; break;
                case "26": dppriority.Text = "京都府"; break;
                case "27": dppriority.Text = "大阪府"; break;
                case "28": dppriority.Text = "兵庫県"; break;
                case "29": dppriority.Text = "奈良県"; break;
                case "30": dppriority.Text = "和歌山県"; break;
                case "31": dppriority.Text = "鳥取県"; break;
                case "32": dppriority.Text = "島根県"; break;
                case "33": dppriority.Text = "岡山県"; break;
                case "34": dppriority.Text = "広島県"; break;
                case "35": dppriority.Text = "山口県"; break;
                case "36": dppriority.Text = "徳島県"; break;
                case "37": dppriority.Text = "香川県"; break;
                case "38": dppriority.Text = "愛媛県"; break;
                case "39": dppriority.Text = "高知県"; break;
                case "40": dppriority.Text = "福岡県"; break;
                case "41": dppriority.Text = "佐賀県"; break;
                case "42": dppriority.Text = "長崎県"; break;
                case "43": dppriority.Text = "熊本県"; break;
                case "44": dppriority.Text = "大分県"; break;
                case "45": dppriority.Text = "宮崎県"; break;
                case "46": dppriority.Text = "鹿児島県"; break;
                case "47": dppriority.Text = "沖縄県"; break;
                default: break;

            }

        }
        private DataTable GetData()
        {

            DataTable dt = new DataTable();
            DataTable dtcopy = new DataTable();
            dt.Columns.Add("ChargeCondition", typeof(System.String));
            dt.Columns.Add("Chargetype", typeof(System.Int32));
            dt.Columns.Add("Deliverityfee", typeof(System.Int32));
            dt.Columns.Add("Priority", typeof(System.String));
            dt.Rows.Add();
            dtcopy.Columns.Add("ChargeCondition", typeof(System.String));
            dtcopy.Columns.Add("Chargetype", typeof(System.Int32));
            dtcopy.Columns.Add("Deliverityfee", typeof(System.Int32));
            dtcopy.Columns.Add("Priority", typeof(System.String));
            string[] colname = { "Priority", "Chargetype", "Deliverityfee", "ChargeCondition" };

            if (!String.IsNullOrWhiteSpace(txttotal.Text) || !String.IsNullOrWhiteSpace(txtdelivertycharge.Text))
            {

                dt.Rows[0]["Priority"] = 1;
                 dt.Rows[0]["ChargeCondition"] =txttotal.Text.Trim();
                 if (ddlpriority1.SelectedValue.ToString() == "都道府県を選択")
                     dt.Rows[0]["Chargetype"] = 0;
                 else
                dt.Rows[0]["Chargetype"] = Int32.Parse(ddlpriority1.SelectedValue);
                dt.Rows[0]["Deliverityfee"] = txtdelivertycharge.Text.Trim();
                CopyColumns(dt, dtcopy, colname);

            }
            if (!String.IsNullOrWhiteSpace(txttotal2.Text) || !String.IsNullOrWhiteSpace(txtdelivery2.Text))
            {
                dt.Rows[0]["Priority"] = 2;
                 dt.Rows[0]["ChargeCondition"] = txttotal2.Text.Trim();
                 if (ddlpriority2.SelectedValue.ToString() == "都道府県を選択")
                     dt.Rows[0]["Chargetype"] = 0;
                 else
                dt.Rows[0]["Chargetype"] = Int32.Parse(ddlpriority2.SelectedValue);
                dt.Rows[0]["Deliverityfee"] = txtdelivery2.Text.Trim();
                CopyColumns(dt, dtcopy, colname);

            }
            if (!String.IsNullOrWhiteSpace(txttotal3.Text) || !String.IsNullOrWhiteSpace(txtdelivery3.Text))
            {
                dt.Rows[0]["Priority"] = 3;
                dt.Rows[0]["ChargeCondition"] = txttotal3.Text.Trim();
                if (ddlpriority3.SelectedValue.ToString() == "都道府県を選択")
                    dt.Rows[0]["Chargetype"] = 0;
                else
                dt.Rows[0]["Chargetype"] = Int32.Parse(ddlpriority3.SelectedValue);
                dt.Rows[0]["Deliverityfee"] = txtdelivery3.Text.Trim();
                CopyColumns(dt, dtcopy, colname);
            }
            if (!String.IsNullOrWhiteSpace(txttotal4.Text) || !String.IsNullOrWhiteSpace(txtdelivery4.Text))
            {
                dt.Rows[0]["Priority"] = 4;
                dt.Rows[0]["ChargeCondition"] = txttotal4.Text.Trim();
                if (ddlpriority4.SelectedValue.ToString() == "都道府県を選択")
                    dt.Rows[0]["Chargetype"] = 0;
                else
                dt.Rows[0]["Chargetype"] = Int32.Parse(ddlpriority4.SelectedValue);
                dt.Rows[0]["Deliverityfee"] = txtdelivery4.Text.Trim();
                CopyColumns(dt, dtcopy, colname);
            }
            if (!String.IsNullOrWhiteSpace(txttotal5.Text) || !String.IsNullOrWhiteSpace(txtdelivery5.Text))
            {
                dt.Rows[0]["Priority"] = 5;
                dt.Rows[0]["ChargeCondition"] = txttotal5.Text.Trim();
                if (ddlpriority5.SelectedValue.ToString() == "都道府県を選択")
                    dt.Rows[0]["Chargetype"] = 0;
                else
                dt.Rows[0]["Chargetype"] = Int32.Parse(ddlpriority5.SelectedValue);
                dt.Rows[0]["Deliverityfee"] = txtdelivery5.Text.Trim();
                CopyColumns(dt, dtcopy, colname);
            }
            if (!String.IsNullOrWhiteSpace(txttotal6.Text) || !String.IsNullOrWhiteSpace(txtdelivery6.Text))
            {
                dt.Rows[0]["Priority"] = 6;
                 dt.Rows[0]["ChargeCondition"] = txttotal6.Text.Trim();
                 if (ddlpriority6.SelectedValue.ToString() == "都道府県を選択")
                     dt.Rows[0]["Chargetype"] = 0;
                 else
                dt.Rows[0]["Chargetype"] = Int32.Parse(ddlpriority6.SelectedValue);
                dt.Rows[0]["Deliverityfee"] = txtdelivery6.Text.Trim();
                CopyColumns(dt, dtcopy, colname);
            }
            if (!String.IsNullOrWhiteSpace(txttotal7.Text) || !String.IsNullOrWhiteSpace(txtdelivery7.Text))
            {
                dt.Rows[0]["Priority"] = 7;
                dt.Rows[0]["ChargeCondition"] = txttotal7.Text.Trim();
                if (ddlpriority7.SelectedValue.ToString() == "都道府県を選択")
                    dt.Rows[0]["Chargetype"] = 0;
                else
                dt.Rows[0]["Chargetype"] = Int32.Parse(ddlpriority7.SelectedValue);
                dt.Rows[0]["Deliverityfee"] = txtdelivery7.Text.Trim();
                CopyColumns(dt, dtcopy, colname);
            }
            if (!String.IsNullOrWhiteSpace(txttotal8.Text) || !String.IsNullOrWhiteSpace(txtdelivery8.Text))
            {
                dt.Rows[0]["Priority"] = 8;
                dt.Rows[0]["ChargeCondition"] = txttotal8.Text.Trim();
                if (ddlpriority8.SelectedValue.ToString() == "都道府県を選択")
                    dt.Rows[0]["Chargetype"] = 0;
                else
                dt.Rows[0]["Chargetype"] = Int32.Parse(ddlpriority8.SelectedValue);
                dt.Rows[0]["Deliverityfee"] = txtdelivery8.Text.Trim();
                CopyColumns(dt, dtcopy, colname);
            }
            if (!String.IsNullOrWhiteSpace(txttotal9.Text) || !String.IsNullOrWhiteSpace(txtdelivery9.Text))
            {
                dt.Rows[0]["Priority"] = 9;
                 dt.Rows[0]["ChargeCondition"] = txttotal9.Text.Trim();
                 if (ddlpriority9.SelectedValue.ToString() == "都道府県を選択")
                     dt.Rows[0]["Chargetype"] = 0;
                 else
                dt.Rows[0]["Chargetype"] = Int32.Parse(ddlpriority9.SelectedValue);
                dt.Rows[0]["Deliverityfee"] = txtdelivery9.Text.Trim();
                CopyColumns(dt, dtcopy, colname);
            }
            if (!String.IsNullOrWhiteSpace(txttotal10.Text) || !String.IsNullOrWhiteSpace(txtdelivery10.Text))
            {
                dt.Rows[0]["Priority"] = 10;
                dt.Rows[0]["ChargeCondition"] = txttotal10.Text.Trim();
                if (ddlpriority10.SelectedValue.ToString() == "都道府県を選択")
                    dt.Rows[0]["Chargetype"] = 0;
                else
                dt.Rows[0]["Chargetype"] = Int32.Parse(ddlpriority10.SelectedValue);
                dt.Rows[0]["Deliverityfee"] = txtdelivery10.Text.Trim();
                CopyColumns(dt, dtcopy, colname);
            }
            if (!String.IsNullOrWhiteSpace(txttotal997.Text) || !String.IsNullOrWhiteSpace(txtdelivery997.Text))
            {
                dt.Rows[0]["Priority"] = 997;
                dt.Rows[0]["ChargeCondition"] = txttotal997.Text.Trim();
                dt.Rows[0]["Chargetype"] = DBNull.Value;
                dt.Rows[0]["Deliverityfee"] = txtdelivery997.Text.Trim();
                CopyColumns(dt, dtcopy, colname);
            }
            if (!String.IsNullOrWhiteSpace(txttotal998.Text) || !String.IsNullOrWhiteSpace(txtdelivery998.Text))
            {
                dt.Rows[0]["Priority"] = 998;
                 dt.Rows[0]["ChargeCondition"] = txttotal998.Text.Trim();
                dt.Rows[0]["Chargetype"] = DBNull.Value;
                dt.Rows[0]["Deliverityfee"] = txtdelivery998.Text.Trim();
                CopyColumns(dt, dtcopy, colname);
            }
            if (!String.IsNullOrWhiteSpace(txttotal999.Text) || !String.IsNullOrWhiteSpace(txtdelivery999.Text))
            {
                dt.Rows[0]["Priority"] = 999;
                dt.Rows[0]["ChargeCondition"] = txttotal999.Text.Trim();
                dt.Rows[0]["Chargetype"] = DBNull.Value;
                dt.Rows[0]["Deliverityfee"] = txtdelivery999.Text.Trim();
                CopyColumns(dt, dtcopy, colname);
            }

            return dtcopy;
        }
        private void CopyColumns(DataTable source, DataTable dest, params string[] columns)
        {
            foreach (DataRow sourcerow in source.Rows)
            {

                DataRow destRow = dest.NewRow();
               
                for (int i = 0; i < columns.Length; i++)
                {
                    string colname = columns[i];
                    if (!String.IsNullOrWhiteSpace(colname))
                    {

                        destRow[colname] = sourcerow[colname];

                    }
                }
                dest.Rows.Add(destRow);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            jhbl = new Jisha_Delivery_Charge_BL();
            if (check != null && check.Rows.Count > 0)
            {
                if (btnSave.Text.Equals("確認画面"))
                { btnSave.Text = "更　新"; }
                else
                {
                    DataTable dt = GetData();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                       string result = jhbl.CODInsert(dt,0);
                       string results = "Save Success";
                       if (results == "Save Success")
                       {
                           object referrer = ViewState["UrlReferrer"];
                           string url = (string)referrer;
                           string script = "window.onload = function(){ alert('";
                           script += results;
                           script += "');";
                           script += "window.location = '";
                           script += url;
                           script += "'; }";
                           ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                       }
                        //if (result != null)
                        //    GlobalUI.MessageBox("Save Successful!");
                        //else
                        //    GlobalUI.MessageBox("Save Fail!");
                    }
                    else { GlobalUI.MessageBox("Please Fill Data!"); }
                }
           }

            else
            {
                String[] priotity = new String[100];
                DataTable dt = GetData();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Columns.Contains("Priority"))
                        prit += dt.Rows[j]["Priority"].ToString() + ',';
                }
                check = jhbl.SelectAll(prit, 1);
                dt.Columns.Add("ID", typeof(string));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int k = 0; k < check.Rows.Count; k++)
                    {

                        if (dt.Rows[i]["Priority"].ToString().Equals(check.Rows[k]["Priority"].ToString()))
                        {
                            dt.Rows[i]["ID"] = check.Rows[k]["ID"].ToString();
                            break;
                        }
                    }
                }

                result = jhbl.CODInsert(dt, 1);
                string resultu = "Update Success";
                if (resultu == "Update Success")
                {
                    object referrer = ViewState["UrlReferrer"];
                    string url = (string)referrer;
                    string script = "window.onload = function(){ alert('";
                    script += resultu;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
                //if (result != null)
                //    GlobalUI.MessageBox("Update Successful!");
                //else
                //    GlobalUI.MessageBox("Update Fail!");
            }
        }
        private void SetData(DataTable dt)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string priority = dt.Rows[i]["Priority"].ToString();
                string chargetype = dt.Rows[i]["Charge_Type"].ToString();
                string total = dt.Rows[i]["Charge"].ToString();
                string deliveryfee = dt.Rows[i]["Charge_Condition"].ToString();
                switch (priority)
                {
                    case "1":
                        if (chargetype == "0")
                            ddlpriority1.SelectedIndex = -1;
                        else
                        ddlpriority1.SelectedValue = chargetype;
                         txttotal.Text = total;
                        txtdelivertycharge.Text = deliveryfee; break;
                   
                    case "2":

                        if (chargetype == "0")
                            ddlpriority2.SelectedIndex = -1;
                        else
                        ddlpriority2.SelectedValue = chargetype;
                         txttotal2.Text = total;
                        txtdelivery2.Text = deliveryfee; break;
                           
                     case "3":

                        if (chargetype == "0")
                            ddlpriority3.SelectedIndex = -1;
                        else
                        ddlpriority3.SelectedValue = chargetype;
                         txttotal3.Text = total;
                        txtdelivery3.Text = deliveryfee; break;
                      
                    case "4":

                         if (chargetype == "0")
                             ddlpriority4.SelectedIndex = -1;
                         else
                       ddlpriority4.SelectedValue = chargetype;
                       txttotal4.Text = total;
                        txtdelivery4.Text = deliveryfee; break;
                        

                    case "5":

                        if (chargetype == "0")
                            ddlpriority5.SelectedIndex = -1;
                        else
                        ddlpriority5.SelectedValue = chargetype;
                        txttotal5.Text = total;
                        txtdelivery5.Text = deliveryfee; break;
                      

                    case "6":

                        if (chargetype == "0")
                            ddlpriority6.SelectedIndex = -1;
                        else
                         ddlpriority6.SelectedValue = chargetype;
                        txttotal6.Text = total;
                        txtdelivery6.Text = deliveryfee; break;
                        

                    case "7":

                        if (chargetype == "0")
                            ddlpriority7.SelectedIndex = -1;
                        else
                        ddlpriority7.SelectedValue = chargetype;
                        txttotal7.Text = total;
                        txtdelivery7.Text = deliveryfee; break;
                       

                    case "8":

                        if (chargetype == "0")
                            ddlpriority8.SelectedIndex = -1;
                        else
                       ddlpriority8.SelectedValue = chargetype;
                        txttotal8.Text = total;
                        txtdelivery8.Text = deliveryfee; break;

                       
                    case "9":

                        if (chargetype == "0")
                            ddlpriority9.SelectedIndex = -1;
                        else
                       ddlpriority9.SelectedValue = chargetype;
                        txttotal9.Text = total;
                        txtdelivery9.Text = deliveryfee; break;
                        
                        

                    case "10":

                     if (chargetype == "0")
                         ddlpriority10.SelectedIndex = -1;
                     else
                     ddlpriority10.SelectedValue = chargetype;
                     txttotal10.Text = total;
                     txtdelivery10.Text = deliveryfee; break;
                        

                    case "997":
                        txttotal997.Text = total;
                        txtdelivery997.Text = deliveryfee; break;

                    case "998":
                        txttotal998.Text = total;
                        txtdelivery998.Text = deliveryfee; break;

                    case "999":
                        txttotal999.Text = total;
                        txtdelivery999.Text = deliveryfee; break;
                    default: break;
                }
            }

        }
    }
}