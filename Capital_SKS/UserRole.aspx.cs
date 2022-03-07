/* 
Created By              : Kay Thi Aung
Created Date          : 28/07/2014
Updated By             :
Updated Date         :

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
using System.Data.SqlClient;
using ORS_RCM_BL;
using ORS_RCM_Common;

namespace ORS_RCM
{
    public partial class UserRole : System.Web.UI.Page
    {
        bool Read = false; bool Edit; bool Delete = false;
        CheckBox ckbRead, ckbEdit, ckbDelete;
        int countRead, countEdit, countDelete = 0;
        int menuID = 0; int y;

        UserRoleBL user; UserRole_Entity userInfo; User_entity userentity; User_BL UserBL;

        protected void Page_Load(object sender, EventArgs e)
        {

            user = new UserRoleBL();
            if (Session["User_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["User_ID"].ToString());

                bool resultRead = user.CanRead(userID, "011");
                if (resultRead)
                {

                    btnAddRole.Visible = false;
                }
            }
            #region Edit,Delete
            //bool resultEdit = user.CanSave(userID, "011");
            //if (resultEdit)
            //{
            //    //btnEdit.Visible = true;
            //    btnAddRole.Visible = true;
            //}
            //else
            //    btnAddRole.Visible = false;
            #endregion
            if (!IsPostBack)
            {
                #region BackPage ViewState
                String backpage = string.Empty;
                if (Request.UrlReferrer != null)
                {
                    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                    backpage = Request.UrlReferrer.ToString();
                }
                else
                {
                    ViewState["UrlReferrer"] = backpage;
                }
                #endregion

                user = new UserRoleBL();
                gvUserRole.DataSource = user.MenuSelectAll();
                gvUserRole.DataBind();

                if (Request.QueryString["ID"] != null)
                {

                    int id = Convert.ToInt32(Request.QueryString["ID"].ToString());
                    // lblName.Text = "User Name : " + user.SelectName(id) + " for Permission.";
                    DataTable dt = new DataTable();
                    DataTable dtuser = user.SelectUserInfo(id);

                    if (dtuser != null && dtuser.Rows.Count > 0)
                    {
                        txtname.Text = dtuser.Rows[0]["User_Name"].ToString();
                        txtID.Text = dtuser.Rows[0]["Login_ID"].ToString();
                        //txtpassword.Text =GlobalUI.DecryptPassword( dtuser.Rows[0]["Password"].ToString());
                        txtpassword.Text = dtuser.Rows[0]["Password"].ToString();
                        txtpassword.Attributes.Add("value", txtpassword.Text);
                        string status = dtuser.Rows[0]["Status"].ToString();
                        if (status == "1")
                        {
                            rdostatus.Checked = true;
                        }
                        else
                        {
                            rdostatus1.Checked = true;
                        }
                    }
                    dt = user.SelectByID(id);

                    // DataRow dr = dt.Select("ID" + gvUserRole.Rows[i]);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvRow in gvUserRole.Rows)
                        {
                            menuID = Convert.ToInt32(((Label)gvRow.FindControl("lblMenuID")).Text);


                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (menuID == (int)dt.Rows[i]["ID"])
                                {
                                    if (Convert.ToInt32(dt.Rows[i]["Can_Read"]) != 0)
                                    {
                                        ckbRead = (CheckBox)gvRow.FindControl("ckbRead");




                                        ckbRead.Checked = true;
                                        countRead++;
                                    }

                                }
                            }

                            #region Edit,Delete
                            //if ((bool)dt.Rows[i]["CanEdit"])
                            //{
                            //    ckbEdit = (CheckBox)gvRow.FindControl("ckbEdit");
                            //    ckbEdit.Checked = true;
                            //    countEdit++;
                            //}
                            //if ((bool)dt.Rows[i]["CanDelete"])
                            //{
                            //    ckbDelete = (CheckBox)gvRow.FindControl("ckbDelete");
                            //    ckbDelete.Checked = true;
                            //    countDelete++;
                            //}
                            #endregion
                        }
                    }

                    if (countRead == gvUserRole.Rows.Count - 3)
                    {
                        ((CheckBox)gvUserRole.HeaderRow.FindControl("chkAllRead")).Checked = true;
                    }
                    //if (countEdit == gvUserRole.Rows.Count - 3)
                    //{
                    //    ((CheckBox)gvUserRole.HeaderRow.FindControl("chkAllEdit")).Checked = true;
                    //}
                    //if (countDelete == gvUserRole.Rows.Count - 3)
                    //{
                    //    ((CheckBox)gvUserRole.HeaderRow.FindControl("chkAllDelete")).Checked = true;
                    //}

                    head.InnerText = "ユーザ編集";
                    regi.Visible = true;

                }
                else
                {


                    regi.Visible = true;

                    head.InnerText = "ユーザ登録";
                    Clear();
                }
            }

         
            
    }
     

        

            
        

        protected void SetData(User_entity userentity)
        {

            userentity.User_Name = txtname.Text;
            userentity.Login_ID = txtID.Text;
            if (!string.IsNullOrWhiteSpace(txtpassword.Text))
                userentity.Password = txtpassword.Text;
            else
                userentity.Password = lblPassword.Text;
            if (rdostatus.Checked == true)
            {
                userentity.Status = 1;
            }
            else
            {
                userentity.Status = 0;
            }
            userentity.Created_Date = Convert.ToDateTime(System.DateTime.Now.ToString());
            //Response.Write(userentity.Created_Date.ToString("dd/MM/yyyy"));

        }


        public void Confirmation()
        {

            // gvUserRole.Visible = false;
            divID.Visible = false;
            txtID.Visible = false;
            lblID.Visible = true;
            lblID.Text = txtID.Text;



            txtname.Visible = false;
            lbluserName.Visible = true;
            lbluserName.Text = txtname.Text;

            divPass.Visible = false;
            txtpassword.Visible = false;
            lblPassword.Visible = true;
            lblPassword.Text = txtpassword.Text;


            lblStatus.Visible = true;
            rdostatus.Visible = false;
            rdostatus1.Visible = false;

            if (rdostatus.Checked)
            {
                lblStatus.Text = "有効";

            }
            else if (rdostatus1.Checked)
            {
                lblStatus.Text = "無効";

            }


            if (Request.QueryString["ID"] != null)
            {

                edi.Visible = true;

                btnAddRole.Text = "反映";

                regi.Visible = false;
            }
            else
            {
                regi.Visible = false;
                edi.Visible = false;

                btnAddRole.Text = "登録";

                reg_Confirm.Visible = true;
            }

            for (int i = 0; i < gvUserRole.Rows.Count; i++)
            {
                CheckBox chk = gvUserRole.Rows[i].FindControl("ckbRead") as CheckBox;
                if (chk.Checked == true)
                  
                    chk.Enabled = false;
                else
                    chk.Enabled = false;
                    
            }


        }


        protected void btnAddRole_Click(object sender, EventArgs e)
        {

            if (btnAddRole.Text == "確認画面へ")
            {
                Confirmation();

            }
            else if ((btnAddRole.Text == "反映") || (btnAddRole.Text == "登録"))
            {
                userentity = new User_entity();
                user = new UserRoleBL();
                UserBL = new User_BL();
                if (Request.QueryString["ID"] != null)
                {
                    int userid = Int32.Parse(Request.QueryString["ID"].ToString());
                    string name = txtname.Text;
                    string ID = txtID.Text;
                    string password = txtpassword.Text;
                    userentity.Password = lblPassword.Text;
                    userentity.User_Name = txtID.Text;
                    userentity.ID = int.Parse(Request.QueryString["ID"].ToString());

                    SetData(userentity);

                    DataTable dt = new DataTable();
                    DataTable dtduplicate = user.Duplicate_loginID(txtID.Text, userid);


          

                    if(dtduplicate.Rows.Count == 1)
                        {

                            GlobalUI.MessageBox("Login_ID already existed");
                        }
                    
                    else
                    {

                        user.Insert(name, ID, password, userid);
                        int id = Convert.ToInt32(Request.QueryString["ID"].ToString());
                        RoleInsert(userid);
                    
                        string result = UserBL.Update(userentity);
                        if (result == "Update Successful !")
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
                        else { GlobalUI.MessageBox("Update Fail!"); }
                    }
                }
                else
                {
                    SetData(userentity);

                    DataTable dt = new DataTable();
                    DataTable dtduplicate = user.Duplicate_loginID(txtID.Text,0);

                    if (dtduplicate.Rows.Count > 0)
                    {

                   //     if (userentity.User_Name == dtduplicate.Rows[0]["Login_ID"].ToString())
                      //  {

                            GlobalUI.MessageBox("Login_ID already existed");
                      //  }
                    }
                    else
                    {
                        int userid = user.UserInsert(userentity);
                        RoleInsert(userid);
                        String result = "Save Successful !";

                        if (result == "Save Successful !")
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

                        else
                        {
                            GlobalUI.MessageBox("Save Fail!, Some textbox are need to insert");
                        }
                    }
                }


            }
        }
                    
       
        protected void Clear() 
        {
            txtID.Text = String.Empty;
            txtname.Text = String.Empty;
            txtpassword.Text = String.Empty;
        
        }




        protected void RoleInsert(int id)
        {
          
                user = new UserRoleBL();
                userInfo = new UserRole_Entity();
                for (int i = 0; i < gvUserRole.Rows.Count; i++)
                {
                    menuID = Convert.ToInt32(((Label)gvUserRole.Rows[i].Cells[0].FindControl("lblMenuID")).Text);
                    if (menuID == 1) // for home page 
                    {
                        userInfo.UserRole.Rows.Add(0, id, menuID, true);
                    }
                    else
                    {
                        CheckBox read = (CheckBox)gvUserRole.Rows[i].Cells[2].FindControl("ckbRead");



                        if (read != null) if (read.Checked) Read = true;


                        if (Read == true || Edit == true || Delete == true)
                        {
                            userInfo.UserRole.Rows.Add(0, id, menuID, Read);
                        }
                    }
                    Read = false; Edit = false; Delete = false;

                }

                user.Insert(userInfo.UserRole, id);
                // GlobalUI.MessageBox("Insert Successful!");
            }

        
        
        protected void ckbRead_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;

            

            GridViewRow row = (GridViewRow)chk.NamingContainer;
            int rowIndex = row.RowIndex;
            Label lbl = gvUserRole.Rows[rowIndex].FindControl("lblMenu") as Label;
            String MenuID = lbl.Text;
            lbl = gvUserRole.Rows[rowIndex].FindControl("lblMasterID") as Label;
            String parentID = lbl.Text;

            if (parentID.Equals("0"))
            {
                for (int i = 0; i < gvUserRole.Rows.Count; i++)
                {
                    lbl = gvUserRole.Rows[i].FindControl("lblMenu") as Label;
                    if (lbl.Text.Equals(MenuID))
                    {
                      
                        CheckBox chk1 = gvUserRole.Rows[i].FindControl("ckbRead") as CheckBox;
                        chk1.Checked = chk.Checked;
                        
                        }
                          }
                    }
                }
            
        

        protected void chkReadSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)gvUserRole.HeaderRow.FindControl("chkAllRead");
            foreach (GridViewRow row in gvUserRole.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbRead");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }

        protected void chkEditSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)gvUserRole.HeaderRow.FindControl("chkAllEdit");
            foreach (GridViewRow row in gvUserRole.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbEdit");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }

        protected void chkDeleteSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)gvUserRole.HeaderRow.FindControl("chkAllDelete");
            foreach (GridViewRow row in gvUserRole.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbDelete");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                    ChkBoxRows.Visible = false;

                }
            }
        }

        protected void gvUserRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblParent = e.Row.FindControl("lblMasterID") as Label;
                if (lblParent.Text.Equals("0"))
                {
                    Label lblPadding = e.Row.FindControl("lblPadding") as Label;

                    e.Row.Cells[3].Attributes["style"] = "border-bottom: 1px dashed #333;";
                }
                else
                {
                    Label lblPadding = e.Row.FindControl("lblPadding") as Label;
                    lblPadding.Visible = true;
                    lblPadding.Width = 50;
                }
            }
        }

        protected void gvUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {

           

        }

        public CheckBox sender { get; set; }

        private string getPostBackControlName()
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

                    if (c is System.Web.UI.WebControls.Button ||

                             c is System.Web.UI.WebControls.ImageButton)
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
    }
}

        




        

    
