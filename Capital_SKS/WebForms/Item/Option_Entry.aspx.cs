/* 
Created By              : EiPhyo
Created Date          : 2014
Updated By             :
Updated Date         :

 Tables using: Option
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
using System.Data.SqlClient;
using ORS_RCM_Common;
using ORS_RCM_BL;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ORS_RCM.WebForms.Item
{
    public partial class Option_Entry : System.Web.UI.Page
    {
        Option_Entity optentity;
        Option_BL optbl;
        Item_BL  itembl=new Item_BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindDataList();

                }
                else
                {
                    btnpopup.Visible = false;
                    btnsave.Visible = true;
                    Confirm();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            } 
        }

        protected void Confirm()
        {
            try
            {
                lblgroupName.Visible = true;
                lblgroupName.Text = txtgroupName.Text;
            

                txtOptionname.Visible = false;
                txtOptionValue.Visible = false;
                lblOptionname.Visible = true;
                lblOptionValue.Visible = true;
                lblOptionname.Text = txtOptionname.Text;
                lblOptionValue.Text = txtOptionValue.Text;

                txtOptionname1.Visible = false;
                txtOptionValue1.Visible = false;
                lblOptionname1.Visible = true;
                lblOptionValue1.Visible = true;
                lblOptionname1.Text = txtOptionname1.Text;
                lblOptionValue1.Text = txtOptionValue1.Text;

                txtOptionname2.Visible = false;
                txtOptionValue2.Visible = false;
                lblOptionname2.Visible = true;
                lblOptionValue2.Visible = true;
                lblOptionname2.Text = txtOptionname2.Text;
                lblOptionValue2.Text = txtOptionValue2.Text;


                first_header.Visible = false;
                pop.Visible = true;
                sect.Visible = true;
                btnsave.Text = "更新";



                //show label and hide textbox for Confirm
                for (int i = 0; i < DataList1.Items.Count; i++)
                {

                    TextBox txt = DataList1.Items[i].FindControl("txtoptGroup") as TextBox;
                    txt.ReadOnly = true;
                    txt.BorderStyle = BorderStyle.None;

                    txt = DataList1.Items[i].FindControl("txtname1") as TextBox;
                    txt.ReadOnly = true;
                    txt.BorderStyle = BorderStyle.None;

                    txt = DataList1.Items[i].FindControl("txtvalue1") as TextBox;
                    txt.ReadOnly = true;
                    txt.BorderStyle = BorderStyle.None;

                    txt = DataList1.Items[i].FindControl("txtname2") as TextBox;
                    txt.ReadOnly = true;
                    txt.BorderStyle = BorderStyle.None;

                    txt = DataList1.Items[i].FindControl("txtvalue2") as TextBox;
                    txt.ReadOnly = true;
                    txt.BorderStyle = BorderStyle.None;

                    txt = DataList1.Items[i].FindControl("txtname3") as TextBox;
                    txt.ReadOnly = true;
                    txt.BorderStyle = BorderStyle.None;

                    txt = DataList1.Items[i].FindControl("txtvalue3") as TextBox;
                    txt.ReadOnly = true;
                    txt.BorderStyle = BorderStyle.None;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            } 
        }

        public void BindDataList()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable FromTable = new DataTable();
                optbl = new Option_BL();
                DataTable dt = optbl.Search();
                DataList1.DataSource = dt;
                DataList1.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            } 
        }

        protected void setData(Option_Entity optentity)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtgroupName.Text))
                {

                    optentity.Option_GroupName = txtgroupName.Text;

                }

                if (!String.IsNullOrEmpty(txtOptionname.Text))
                {
                    optentity.Option_Name = txtOptionname.Text;

                }


                if (!String.IsNullOrEmpty(txtOptionValue.Text))
                {

                    optentity.Option_Value = txtOptionValue.Text;

                }

                if (!String.IsNullOrEmpty(txtOptionname1.Text))
                {

                    optentity.Option_Name2 = txtOptionname1.Text;


                }

                if (!String.IsNullOrEmpty(txtOptionValue1.Text))
                {

                    optentity.Option_Value2 = txtOptionValue1.Text;

                }

                if (!String.IsNullOrEmpty(txtOptionname2.Text))
                {

                    optentity.Option_Name3 = txtOptionname2.Text;

                }

                if (!String.IsNullOrEmpty(txtOptionValue2.Text))
                {

                    optentity.Option_Value3 = txtOptionValue2.Text;

                }

                if ((!String.IsNullOrWhiteSpace(txtgroupName.Text)) || (txtOptionname2.Text != "") || (txtOptionname1.Text != "") ||
                             (txtOptionname.Text != "") || (txtOptionValue.Text != "") || (txtOptionValue2.Text != "") || (txtOptionValue1.Text != ""))
                {


                    //optbl.Insert(optentity);

                    string i = optbl.Insert(optentity);
                    MessageBox(i);

                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }              
        }

        public void insertDatalist(Option_Entity optentity)
        {
            optbl = new Option_BL();

            optentity = new Option_Entity();
            string msg = string.Empty;

            foreach (DataListItem item in DataList1.Items)
            {
                try
                {
                    TextBox txtoptGroup = (TextBox)(item.FindControl("txtoptGroup"));
                    optentity.Option_GroupName = txtoptGroup.Text;

                    TextBox txtname1 = ((TextBox)item.FindControl("txtname1"));
                    optentity.Option_Name = txtname1.Text;

                    TextBox txtvalue1 = ((TextBox)item.FindControl("txtvalue1"));
                    optentity.Option_Value = txtvalue1.Text;


                    TextBox txtname2 = ((TextBox)item.FindControl("txtname2"));
                    optentity.Option_Name2 = txtname2.Text;

                    TextBox txtvalue2 = ((TextBox)item.FindControl("txtvalue2"));
                    optentity.Option_Value2 = txtvalue2.Text;



                    TextBox txtname3 = ((TextBox)item.FindControl("txtname3"));
                    optentity.Option_Name3 = txtname3.Text;

                    TextBox txtvalue3 = ((TextBox)item.FindControl("txtvalue3"));
                    optentity.Option_Value3 = txtvalue3.Text;

                    Label lblid = item.FindControl("lblID") as Label;

                    optentity.ID = Convert.ToInt32(lblid.Text);

                    Label lblid2 = item.FindControl("lblID2") as Label;

                    optentity.ID2 = Convert.ToInt32(lblid2.Text);
                    Label lblid3 = item.FindControl("lblID3") as Label;

                    optentity.ID3 = Convert.ToInt32(lblid3.Text);
                    Label lblty1 = item.FindControl("lblt1") as Label;
                    optentity.Type1 = Convert.ToInt32(lblty1.Text);
                    Label lblty2 = item.FindControl("lblt2") as Label;
                    optentity.Type2 = Convert.ToInt32(lblty2.Text);
                    Label lblty3 = item.FindControl("lblt3") as Label;
                    optentity.Type3 = Convert.ToInt32(lblty3.Text);
                    msg= optbl.Insert(optentity);
                    if (msg.Equals("group name already exists") || msg.Equals("fill group name"))
                        break;
                }
                catch (Exception ex)
                {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
                } 
            }
            MessageBox(msg);
          }
     
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                optbl = new Option_BL();

                optentity = new Option_Entity();

                itembl = new Item_BL();


                    setData(optentity);
                
                    insertDatalist(optentity);


                    BindDataList();

                    txtgroupName.Visible = true;
                    lblgroupName.Visible = false;
                    txtgroupName.Text = "";

                    txtOptionname.Visible = true;
                    lblOptionname.Visible = false;
                    txtOptionname.Text = "";

                    txtOptionname1.Visible = true;
                    lblOptionname1.Visible = false;
                    txtOptionname1.Text = "";

                    txtOptionname2.Visible = true;
                    lblOptionname2.Visible = false;
                    txtOptionname2.Text = "";

                    txtOptionValue.Visible = true;
                    lblOptionValue.Visible = false;
                    txtOptionValue.Text = "";

                    txtOptionValue1.Visible = true;
                    lblOptionValue1.Visible = false;
                    txtOptionValue1.Text = "";

                    txtOptionValue2.Visible = true;
                    lblOptionValue2.Visible = false;
                    txtOptionValue2.Text = "";

                    btnsave.Visible = false;
                    btnpopup.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            } 
       }

        public void MessageBox(string message)
        {
            try
            {
                string cleanMessage = message.Replace("'", "\\'");
                string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";


                Page page = HttpContext.Current.CurrentHandler as Page;

                if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                {
                    page.ClientScript.RegisterClientScriptBlock(typeof(GlobalUI), "alert", script);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            } 
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }





    }
                   
}