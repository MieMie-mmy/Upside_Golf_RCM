/* 
Created By              : Eephyo
Created Date          : 18/06/2014
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
using ORS_RCM_Common;
using ORS_RCM_BL;

namespace ORS_RCM
{
    public partial class User_Register : System.Web.UI.Page
    {
          User_entity  userentity;
          User_BL  userBl;

        protected void Page_Load(object sender, EventArgs e)
          {
              if (!IsPostBack)
              {


                  ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                  userentity = new User_entity();
                  if (Request.QueryString["ID"] != null)
                  {
                      int id = int.Parse(Request.QueryString["ID"].ToString());
                      userBl = new User_BL();
                  
                          userentity = userBl.SelectByID(id);
                          Getdata(userentity);
                                      
                      txtPassword.Attributes.Add("value", txtPassword.Text);
                      btnsave.Text = "更新する";
                  }
                  else
                      SetData(userentity);

              }
          }

        protected void btnsave_Click(object sender, EventArgs e)
        {
           if ((!string.IsNullOrWhiteSpace(txtPassword.Text))&& (!string.IsNullOrWhiteSpace(txtUserName.Text)) && (!string.IsNullOrWhiteSpace(txtLoginID.Text)))
             {


            userBl = new User_BL();
            userentity = new User_entity();
            if (Request.QueryString["ID"] != null)
            {
                String result = null;
                userBl = new User_BL();

                int id = int.Parse(Request.QueryString["ID"].ToString());

                userentity.ID = int.Parse(Request.QueryString["ID"].ToString());
                SetData(userentity);
                result = userBl.Update(userentity);
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

            

            else
            {
                SetData(userentity);
                String result = userBl.Insert(userentity);
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
            }
           }
                else { GlobalUI.MessageBox("Save Fail!, Some textbox are need to insert"); }
            }                          
        
        protected void SetData(User_entity userentity)
        {
             
            userentity.User_Name = txtUserName.Text;
            userentity.Login_ID = txtLoginID.Text;
            userentity.Password = txtPassword.Text;
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

        protected void Getdata( User_entity  userentity)
        {

            txtUserName.Text = userentity.User_Name;
            txtLoginID.Text = userentity.Login_ID;
            txtPassword.Text =userentity.Password;
            txtPassword.Attributes.Add("value", txtPassword.Text);
            //rdostatus.SelectIndex= (userentity.Status);
            if (userentity.Status==1)
            {
                rdostatus.Checked = true;
            }
            else
            {
                rdostatus1.Checked = true;
            }
            userentity.Updated_Date = Convert.ToDateTime(System.DateTime.Now.ToString());
            //Response.Write(userentity.Created_Date.ToString("dd/MM/yyyy"));
       }

                
        }    

  }
    
