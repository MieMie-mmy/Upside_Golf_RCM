/* 
Created By              : Aye Mon 
Created Date          : 2014
Updated By             :
Updated Date         :

 Tables using: Yahoo_SpecName
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
using ORS_RCM_BL;

namespace ORS_RCM.WebForms.Item
{
    public partial class Item_PopUp : System.Web.UI.Page
    {
        //Global Variables
        public string YahooMallCategoryID
        {
            get
            {
                if (Request.QueryString["YahooMallCategoryID"] != null)
                {
                    return Request.QueryString["YahooMallCategoryID"].ToString();
                }
                else
                {
                    return null;
                }
            }
        }

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

        public DataTable YahooSpecificValue
        {
            get
            {
                if (Session["YahooSpecificValue_"+Item_Code] != null)
                {
                    return (DataTable)Session["YahooSpecificValue_"+Item_Code];
                }
                else
                {
                    return null;
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

        //Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (YahooMallCategoryID != null)
                    {
                        Yahoo_SpecName_BL YahooSpecNameBL = new Yahoo_SpecName_BL();
                        DataTable dt = YahooSpecNameBL.SelectByYahooMallCategoryID(YahooMallCategoryID);

                        //if (Item_ID == 0)
                        //{
                        BindYahooSpecData(dt);
                        //}
                        //else
                        //{
                        //    Item_YahooSpecificValue_BL itemyahoo = new Item_YahooSpecificValue_BL();
                        //    DataTable dtitemyahoo = itemyahoo.SelectByItemID(Item_ID);
                        //    BindYahooSpecDataView3(dt, dtitemyahoo);
                        //}
                    }
                }
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
                
                Session["YahooSpecificValue_"+Item_Code] = GetYahooSpecificValue();
                Session["btnYPopClick_" + Item_Code] = "ok";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack('YahooSpec','');window.close()", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            } 
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Session["btnYPopClick_" + Item_Code] = "cancel";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        //Functions
        public DataTable GetYahooSpecificValue()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr1, dr2, dr3, dr4, dr5 = null;
                //define the columns
                dt.Columns.Add(new DataColumn("Spec_ID", typeof(string)));
                dt.Columns.Add(new DataColumn("Spec_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Spec_ValueID", typeof(string)));
                dt.Columns.Add(new DataColumn("Spec_ValueName", typeof(string)));
                dt.Columns.Add(new DataColumn("Type", typeof(int)));
                //create new row
                dr1 = dt.NewRow();
                dr2 = dt.NewRow();
                dr3 = dt.NewRow();
                dr4 = dt.NewRow();
                dr5 = dt.NewRow();
                //add values to each rows
                dr1["Spec_ID"] = hfSpec_ID1.Value;
                dr1["Spec_Name"] = lblSpec_Name1.Text;
                dr1["Spec_ValueID"] = ddlSpec_ValueName1.SelectedValue;
                dr1["Spec_ValueName"] = ddlSpec_ValueName1.SelectedItem;
                dr1["Type"] = 1;
                //add the row to DataTable
                dt.Rows.Add(dr1);

                dr2["Spec_ID"] = hfSpec_ID2.Value;
                dr2["Spec_Name"] = lblSpec_Name2.Text;
                dr2["Spec_ValueID"] = ddlSpec_ValueName2.SelectedValue;
                dr2["Spec_ValueName"] = ddlSpec_ValueName2.SelectedItem;
                dr2["Type"] = 2;
                dt.Rows.Add(dr2);

                dr3["Spec_ID"] = hfSpec_ID3.Value;
                dr3["Spec_Name"] = lblSpec_Name3.Text;
                dr3["Spec_ValueID"] = ddlSpec_ValueName3.SelectedValue;
                dr3["Spec_ValueName"] = ddlSpec_ValueName3.SelectedItem;
                dr3["Type"] = 3;
                dt.Rows.Add(dr3);

                dr4["Spec_ID"] = hfSpec_ID4.Value;
                dr4["Spec_Name"] = lblSpec_Name4.Text;
                dr4["Spec_ValueID"] = ddlSpec_ValueName4.SelectedValue;
                dr4["Spec_ValueName"] = ddlSpec_ValueName4.SelectedItem;
                dr4["Type"] = 4;
                dt.Rows.Add(dr4);

                dr5["Spec_ID"] = hfSpec_ID5.Value;
                dr5["Spec_Name"] = lblSpec_Name5.Text;
                dr5["Spec_ValueID"] = ddlSpec_ValueName5.SelectedValue;
                dr5["Spec_ValueName"] = ddlSpec_ValueName5.SelectedItem;
                dr5["Type"] = 5;
                dt.Rows.Add(dr5);

                if (Request.QueryString["row"] != null)
                {
                    String row = Request.QueryString["row"].ToString();
                    DataColumn col = new DataColumn();
                    col.ColumnName = "Index";
                    col.DefaultValue = row;
                    if (dt.Rows.Count == 0)
                        dt.Rows.Add();
                    dt.Columns.Add(col);
                }

                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            } 
        }

        public void BindYahooSpecData(DataTable dt)
        {
            try
            {
                DataTable dtyahoospecific = YahooSpecificValue as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                {
                    string value_name, value_id;
                    string[] arr_vname, arr_vid;

                    lblName.Text = dt.Rows[0]["Name"].ToString();

                    if (dt.Rows.Count > 0)
                    {
                        lblSpec_Name1.Text = dt.Rows[0]["Spec_Name"].ToString();
                        hfSpec_ID1.Value = dt.Rows[0]["Spec_ID"].ToString();

                        DataTable temp_dt = new DataTable();
                        temp_dt.Columns.Add("Spec_ValueID", typeof(string));
                        temp_dt.Columns.Add("Spec_ValueName", typeof(string));

                        value_id = dt.Rows[0]["Spec_ValueID"].ToString();
                        value_name = dt.Rows[0]["Spec_ValueName"].ToString();

                        string temp_value_name = string.Join(",", value_name.Split(',').Distinct().ToArray());
                        string temp_value_id = string.Join(",", value_id.Split(',').Distinct().ToArray());

                        arr_vname = temp_value_name.Split(',');
                        arr_vid = temp_value_id.Split(',');

                        for (int i = 0; i < arr_vname.Length; i++)
                        {
                            temp_dt.Rows.Add(arr_vid[i].ToString(), arr_vname[i].ToString());
                        }

                        ddlSpec_ValueName1.DataSource = temp_dt;
                        ddlSpec_ValueName1.DataValueField = "Spec_ValueID";
                        ddlSpec_ValueName1.DataTextField = "Spec_ValueName";
                        ddlSpec_ValueName1.DataBind();
                        ddlSpec_ValueName1.Items.Insert(0, "");

                    }

                    if (dt.Rows.Count > 1)
                    {
                        lblSpec_Name2.Text = dt.Rows[1]["Spec_Name"].ToString();
                        hfSpec_ID2.Value = dt.Rows[1]["Spec_ID"].ToString();

                        DataTable temp_dt = new DataTable();
                        temp_dt.Columns.Add("Spec_ValueID", typeof(string));
                        temp_dt.Columns.Add("Spec_ValueName", typeof(string));

                        value_id = dt.Rows[1]["Spec_ValueID"].ToString();
                        value_name = dt.Rows[1]["Spec_ValueName"].ToString();

                        //arr_vname = value_name.Split(',');
                        //arr_vid = value_id.Split(',');
                        string temp_value_name = string.Join(",", value_name.Split(',').Distinct().ToArray());
                        string temp_value_id = string.Join(",", value_id.Split(',').Distinct().ToArray());

                        arr_vname = temp_value_name.Split(',');
                        arr_vid = temp_value_id.Split(',');

                        for (int i = 0; i < arr_vname.Length; i++)
                        {
                            temp_dt.Rows.Add(arr_vid[i].ToString(), arr_vname[i].ToString());
                        }

                        ddlSpec_ValueName2.DataSource = temp_dt;
                        ddlSpec_ValueName2.DataValueField = "Spec_ValueID";
                        ddlSpec_ValueName2.DataTextField = "Spec_ValueName";
                        ddlSpec_ValueName2.DataBind();
                        ddlSpec_ValueName2.Items.Insert(0, "");

                    }


                    if (dt.Rows.Count > 2)
                    {
                        lblSpec_Name3.Text = dt.Rows[2]["Spec_Name"].ToString();
                        hfSpec_ID3.Value = dt.Rows[2]["Spec_ID"].ToString();

                        DataTable temp_dt = new DataTable();
                        temp_dt.Columns.Add("Spec_ValueID", typeof(string));
                        temp_dt.Columns.Add("Spec_ValueName", typeof(string));

                        value_id = dt.Rows[2]["Spec_ValueID"].ToString();
                        value_name = dt.Rows[2]["Spec_ValueName"].ToString();

                        //arr_vname = value_name.Split(',');
                        //arr_vid = value_id.Split(',');
                        string temp_value_name = string.Join(",", value_name.Split(',').Distinct().ToArray());
                        string temp_value_id = string.Join(",", value_id.Split(',').Distinct().ToArray());

                        arr_vname = temp_value_name.Split(',');
                        arr_vid = temp_value_id.Split(',');

                        for (int i = 0; i < arr_vname.Length; i++)
                        {
                            temp_dt.Rows.Add(arr_vid[i].ToString(), arr_vname[i].ToString());
                        }

                        ddlSpec_ValueName3.DataSource = temp_dt;
                        ddlSpec_ValueName3.DataValueField = "Spec_ValueID";
                        ddlSpec_ValueName3.DataTextField = "Spec_ValueName";
                        ddlSpec_ValueName3.DataBind();
                        ddlSpec_ValueName3.Items.Insert(0, "");

                    }

                    if (dt.Rows.Count > 3)
                    {
                        lblSpec_Name4.Text = dt.Rows[3]["Spec_Name"].ToString();
                        hfSpec_ID4.Value = dt.Rows[3]["Spec_ID"].ToString();

                        DataTable temp_dt = new DataTable();
                        temp_dt.Columns.Add("Spec_ValueID", typeof(string));
                        temp_dt.Columns.Add("Spec_ValueName", typeof(string));

                        value_id = dt.Rows[3]["Spec_ValueID"].ToString();
                        value_name = dt.Rows[3]["Spec_ValueName"].ToString();

                        //arr_vname = value_name.Split(',');
                        //arr_vid = value_id.Split(',');
                        string temp_value_name = string.Join(",", value_name.Split(',').Distinct().ToArray());
                        string temp_value_id = string.Join(",", value_id.Split(',').Distinct().ToArray());

                        arr_vname = temp_value_name.Split(',');
                        arr_vid = temp_value_id.Split(',');
                        for (int i = 0; i < arr_vname.Length; i++)
                        {
                            temp_dt.Rows.Add(arr_vid[i].ToString(), arr_vname[i].ToString());
                        }

                        ddlSpec_ValueName4.DataSource = temp_dt;
                        ddlSpec_ValueName4.DataValueField = "Spec_ValueID";
                        ddlSpec_ValueName4.DataTextField = "Spec_ValueName";
                        ddlSpec_ValueName4.DataBind();
                        ddlSpec_ValueName4.Items.Insert(0, "");

                    }

                    if (dt.Rows.Count > 4)
                    {
                        lblSpec_Name5.Text = dt.Rows[4]["Spec_Name"].ToString();
                        hfSpec_ID5.Value = dt.Rows[4]["Spec_ID"].ToString();

                        DataTable temp_dt = new DataTable();
                        temp_dt.Columns.Add("Spec_ValueID", typeof(string));
                        temp_dt.Columns.Add("Spec_ValueName", typeof(string));

                        value_id = dt.Rows[4]["Spec_ValueID"].ToString();
                        value_name = dt.Rows[4]["Spec_ValueName"].ToString();

                        //arr_vname = value_name.Split(',');
                        //arr_vid = value_id.Split(',');
                        string temp_value_name = string.Join(",", value_name.Split(',').Distinct().ToArray());
                        string temp_value_id = string.Join(",", value_id.Split(',').Distinct().ToArray());

                        arr_vname = temp_value_name.Split(',');
                        arr_vid = temp_value_id.Split(',');
                        for (int i = 0; i < arr_vname.Length; i++)
                        {
                            temp_dt.Rows.Add(arr_vid[i].ToString(), arr_vname[i].ToString());
                        }

                        ddlSpec_ValueName5.DataSource = temp_dt;
                        ddlSpec_ValueName5.DataValueField = "Spec_ValueID";
                        ddlSpec_ValueName5.DataTextField = "Spec_ValueName";
                        ddlSpec_ValueName5.DataBind();
                        ddlSpec_ValueName5.Items.Insert(0, "");

                    }

                    DataRow[] dr = dtyahoospecific.Select("Spec_ID='" + hfSpec_ID1.Value + "'");
                    if (dr.Count() > 0)
                    {
                        if (ddlSpec_ValueName1.Items.FindByValue(dr[0]["Spec_ValueID"].ToString()) != null)
                            ddlSpec_ValueName1.Items.FindByValue(dr[0]["Spec_ValueID"].ToString()).Selected = true;
                    }

                    dr = dtyahoospecific.Select("Spec_ID='" + hfSpec_ID2.Value + "'");
                    if (dr.Count() > 0)
                    {
                        if (ddlSpec_ValueName2.Items.FindByValue(dr[0]["Spec_ValueID"].ToString()) != null)
                            ddlSpec_ValueName2.Items.FindByValue(dr[0]["Spec_ValueID"].ToString()).Selected = true;
                    }
                    dr = dtyahoospecific.Select("Spec_ID='" + hfSpec_ID3.Value + "'");
                    if (dr.Count() > 0)
                    {
                        if (ddlSpec_ValueName3.Items.FindByValue(dr[0]["Spec_ValueID"].ToString()) != null)
                            ddlSpec_ValueName3.Items.FindByValue(dr[0]["Spec_ValueID"].ToString()).Selected = true;
                    }
                    dr = dtyahoospecific.Select("Spec_ID='" + hfSpec_ID4.Value + "'");
                    if (dr.Count() > 0)
                    {
                        if (ddlSpec_ValueName4.Items.FindByValue(dr[0]["Spec_ValueID"].ToString()) != null)
                            ddlSpec_ValueName4.Items.FindByValue(dr[0]["Spec_ValueID"].ToString()).Selected = true;
                    }
                    dr = dtyahoospecific.Select("Spec_ID='" + hfSpec_ID5.Value + "'");
                    if (dr.Count() > 0)
                    {
                        if (ddlSpec_ValueName5.Items.FindByValue(dr[0]["Spec_ValueID"].ToString()) != null)
                            ddlSpec_ValueName5.Items.FindByValue(dr[0]["Spec_ValueID"].ToString()).Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void BindYahooSpecDataView3(DataTable dt,DataTable dtview3)
        {
            try
            {
                DataTable dtview = dtview3 as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                {
                    string value_name, value_id;
                    string[] arr_vname, arr_vid;


                    lblName.Text = dt.Rows[0]["Name"].ToString();

                    if (dt.Rows.Count > 0)
                    {
                        lblSpec_Name1.Text = dt.Rows[0]["Spec_Name"].ToString();
                        hfSpec_ID1.Value = dt.Rows[0]["Spec_ID"].ToString();

                        DataTable temp_dt = new DataTable();
                        temp_dt.Columns.Add("Spec_ValueID", typeof(string));
                        temp_dt.Columns.Add("Spec_ValueName", typeof(string));

                        value_id = dt.Rows[0]["Spec_ValueID"].ToString();
                        value_name = dt.Rows[0]["Spec_ValueName"].ToString();

                        //arr_vname = value_name.Split(',');
                        //arr_vid = value_id.Split(',');
                        string temp_value_name = string.Join(",", value_name.Split(',').Distinct().ToArray());
                        string temp_value_id = string.Join(",", value_id.Split(',').Distinct().ToArray());

                        arr_vname = temp_value_name.Split(',');
                        arr_vid = temp_value_id.Split(',');
                        for (int i = 0; i < arr_vname.Length; i++)
                        {
                            temp_dt.Rows.Add(arr_vid[i].ToString(), arr_vname[i].ToString());
                        }


                        ddlSpec_ValueName1.DataSource = temp_dt;
                        ddlSpec_ValueName1.DataValueField = "Spec_ValueID";
                        ddlSpec_ValueName1.DataTextField = "Spec_ValueName";
                        ddlSpec_ValueName1.DataBind();
                        if (dtview != null && dtview.Rows.Count > 0 && !string.IsNullOrWhiteSpace(dtview.Rows[0]["Spec_ValueID"].ToString()))
                        {
                            ddlSpec_ValueName1.Items.FindByValue(dtview.Rows[0]["Spec_ValueID"].ToString()).Selected = true;
                        }
                        else
                        {
                            ddlSpec_ValueName1.Items.Insert(0, new ListItem("--Select--", ""));
                        }

                    }

                    if (dt.Rows.Count > 1)
                    {
                        lblSpec_Name2.Text = dt.Rows[1]["Spec_Name"].ToString();
                        hfSpec_ID2.Value = dt.Rows[1]["Spec_ID"].ToString();

                        DataTable temp_dt = new DataTable();
                        temp_dt.Columns.Add("Spec_ValueID", typeof(string));
                        temp_dt.Columns.Add("Spec_ValueName", typeof(string));

                        value_id = dt.Rows[1]["Spec_ValueID"].ToString();
                        value_name = dt.Rows[1]["Spec_ValueName"].ToString();

                        //arr_vname = value_name.Split(',');
                        //arr_vid = value_id.Split(',');
                        string temp_value_name = string.Join(",", value_name.Split(',').Distinct().ToArray());
                        string temp_value_id = string.Join(",", value_id.Split(',').Distinct().ToArray());

                        arr_vname = temp_value_name.Split(',');
                        arr_vid = temp_value_id.Split(',');
                        for (int i = 0; i < arr_vname.Length; i++)
                        {
                            temp_dt.Rows.Add(arr_vid[i].ToString(), arr_vname[i].ToString());
                        }

                        ddlSpec_ValueName2.DataSource = temp_dt;
                        ddlSpec_ValueName2.DataValueField = "Spec_ValueID";
                        ddlSpec_ValueName2.DataTextField = "Spec_ValueName";
                        ddlSpec_ValueName2.DataBind();
                        if (dtview != null && dtview.Rows.Count > 1 && !string.IsNullOrWhiteSpace(dtview.Rows[1]["Spec_ValueID"].ToString()))
                        {
                            ddlSpec_ValueName2.Items.FindByValue(dtview.Rows[1]["Spec_ValueID"].ToString()).Selected = true;
                        }
                        else
                        {
                            ddlSpec_ValueName2.Items.Insert(0, new ListItem("--Select--", ""));
                        }
                    }

                    if (dt.Rows.Count > 2)
                    {
                        lblSpec_Name3.Text = dt.Rows[2]["Spec_Name"].ToString();
                        hfSpec_ID3.Value = dt.Rows[2]["Spec_ID"].ToString();

                        DataTable temp_dt = new DataTable();
                        temp_dt.Columns.Add("Spec_ValueID", typeof(string));
                        temp_dt.Columns.Add("Spec_ValueName", typeof(string));

                        value_id = dt.Rows[2]["Spec_ValueID"].ToString();
                        value_name = dt.Rows[2]["Spec_ValueName"].ToString();

                        //arr_vname = value_name.Split(',');
                        //arr_vid = value_id.Split(',');
                        string temp_value_name = string.Join(",", value_name.Split(',').Distinct().ToArray());
                        string temp_value_id = string.Join(",", value_id.Split(',').Distinct().ToArray());

                        arr_vname = temp_value_name.Split(',');
                        arr_vid = temp_value_id.Split(',');
                        for (int i = 0; i < arr_vname.Length; i++)
                        {
                            temp_dt.Rows.Add(arr_vid[i].ToString(), arr_vname[i].ToString());
                        }

                        ddlSpec_ValueName3.DataSource = temp_dt;
                        ddlSpec_ValueName3.DataValueField = "Spec_ValueID";
                        ddlSpec_ValueName3.DataTextField = "Spec_ValueName";
                        ddlSpec_ValueName3.DataBind();
                        if (dtview != null && dtview.Rows.Count > 2 && !string.IsNullOrWhiteSpace(dtview.Rows[2]["Spec_ValueID"].ToString()))
                        {
                            ddlSpec_ValueName3.Items.FindByValue(dtview.Rows[2]["Spec_ValueID"].ToString()).Selected = true;
                        }
                        else
                        {
                            ddlSpec_ValueName3.Items.Insert(0, new ListItem("--Select--", ""));
                        }
                    }

                    if (dt.Rows.Count > 3)
                    {
                        lblSpec_Name4.Text = dt.Rows[3]["Spec_Name"].ToString();
                        hfSpec_ID4.Value = dt.Rows[3]["Spec_ID"].ToString();

                        DataTable temp_dt = new DataTable();
                        temp_dt.Columns.Add("Spec_ValueID", typeof(string));
                        temp_dt.Columns.Add("Spec_ValueName", typeof(string));

                        value_id = dt.Rows[3]["Spec_ValueID"].ToString();
                        value_name = dt.Rows[3]["Spec_ValueName"].ToString();

                        //arr_vname = value_name.Split(',');
                        //arr_vid = value_id.Split(',');
                        string temp_value_name = string.Join(",", value_name.Split(',').Distinct().ToArray());
                        string temp_value_id = string.Join(",", value_id.Split(',').Distinct().ToArray());

                        arr_vname = temp_value_name.Split(',');
                        arr_vid = temp_value_id.Split(',');
                        for (int i = 0; i < arr_vname.Length; i++)
                        {
                            temp_dt.Rows.Add(arr_vid[i].ToString(), arr_vname[i].ToString());
                        }

                        ddlSpec_ValueName4.DataSource = temp_dt;
                        ddlSpec_ValueName4.DataValueField = "Spec_ValueID";
                        ddlSpec_ValueName4.DataTextField = "Spec_ValueName";
                        ddlSpec_ValueName4.DataBind();
                        if (dtview != null && dtview.Rows.Count > 3 && !string.IsNullOrWhiteSpace(dtview.Rows[3]["Spec_ValueID"].ToString()))
                        {
                            ddlSpec_ValueName4.Items.FindByValue(dtview.Rows[3]["Spec_ValueID"].ToString()).Selected = true;
                        }
                        else
                        {
                            ddlSpec_ValueName4.Items.Insert(0, new ListItem("--Select--", ""));
                        }
                    }

                    if (dt.Rows.Count > 4)
                    {
                        lblSpec_Name5.Text = dt.Rows[4]["Spec_Name"].ToString();
                        hfSpec_ID5.Value = dt.Rows[4]["Spec_ID"].ToString();

                        DataTable temp_dt = new DataTable();
                        temp_dt.Columns.Add("Spec_ValueID", typeof(string));
                        temp_dt.Columns.Add("Spec_ValueName", typeof(string));

                        value_id = dt.Rows[4]["Spec_ValueID"].ToString();
                        value_name = dt.Rows[4]["Spec_ValueName"].ToString();

                        //arr_vname = value_name.Split(',');
                        //arr_vid = value_id.Split(',');
                        string temp_value_name = string.Join(",", value_name.Split(',').Distinct().ToArray());
                        string temp_value_id = string.Join(",", value_id.Split(',').Distinct().ToArray());

                        arr_vname = temp_value_name.Split(',');
                        arr_vid = temp_value_id.Split(',');
                        for (int i = 0; i < arr_vname.Length; i++)
                        {
                            temp_dt.Rows.Add(arr_vid[i].ToString(), arr_vname[i].ToString());
                        }

                        ddlSpec_ValueName5.DataSource = temp_dt;
                        ddlSpec_ValueName5.DataValueField = "Spec_ValueID";
                        ddlSpec_ValueName5.DataTextField = "Spec_ValueName";
                        ddlSpec_ValueName5.DataBind();
                        if (dtview != null && dtview.Rows.Count > 4 && !string.IsNullOrWhiteSpace(dtview.Rows[4]["Spec_ValueID"].ToString()))
                        {
                            ddlSpec_ValueName5.Items.FindByValue(dtview.Rows[4]["Spec_ValueID"].ToString()).Selected = true;
                        }
                        else
                        {
                            ddlSpec_ValueName5.Items.Insert(0, new ListItem("--Select--", ""));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

    }
}