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


namespace ORS_RCM.WebForms.Mall
{
    public partial class Smart_Template_Edit_Confirmatiion : System.Web.UI.Page
    {
        Smart_Template_Entity smartTempentity;
        Smart_Template_BL smartTemBl;
        Shop_Template_BL shopTemBl;
        Shop_Template_Entity shopTemEnt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                smartTemBl = new Smart_Template_BL();

              
                 BindMyDataListview();



            }

        }    

            
    



        private void getShopList()
        {
            smartTemBl = new Smart_Template_BL();
            smartTempentity = new Smart_Template_Entity();

            DataTable dt = smartTemBl.GetShopTable();

            DataList1.DataSource = dt;
            DataList1.DataBind();
        }


        public void BindMyDataListview()
        {

          
            if (Session["myDatatable"] != null)
            {
                  DataTable dt=new  DataTable();
                  dt = (DataTable)Session["myDatatable"];

                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    DataList1.Visible = true;
                    DataList1.DataSource = dt;
                     DataList1.DataBind();
                }
                else
                {
                   DataList1.Visible = false;
                }
            }

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

            smartTemBl = new Smart_Template_BL();
            smartTempentity = new Smart_Template_Entity();
            shopTemEnt = new Shop_Template_Entity();



            foreach (DataListItem item in DataList1.Items)
            {
                Label lblTemplateID = (Label)item.FindControl("lblTemplateID");

                if (lblTemplateID.Text != String.Empty)
                {
                    smartTempentity.Template_ID = lblTemplateID.Text;

                }

                Label lblStatus = (Label)item.FindControl("lblStatus");

                if (lblStatus.Text == "有効")
                {
                    smartTempentity.Status = 1;
                }
                else
                {
                    smartTempentity.Status = 0;
                }


                int tID = smartTemBl.Insert(smartTempentity);
                shopTemEnt.TempID = tID;
                addData(shopTemEnt);

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
        
        

            public void addData(Shop_Template_Entity shopTemEnt)
        {     

            shopTemBl = new Shop_Template_BL();
            DataTable dt = new DataTable();
            dt.Columns.Add("Template_ID", typeof(int));
            dt.Columns.Add("Shop_ID", typeof(int));
            dt.Columns.Add("Template_Description", typeof(string));
            dt.Columns.Add("type", typeof(string));
            int TempID = Convert.ToInt32(shopTemEnt.TempID);
            foreach (DataListItem item in DataList1.Items)
            {


                Label lblShopID = (Label)item.FindControl("lblShopID");
                if ((lblShopID.Text)!=String.Empty)
                {
                shopTemEnt.Shop_ID = Convert.ToInt32(lblShopID.Text);
                }



                Label lblTemDescription = (Label)item.FindControl("lblTemDescription");
                shopTemEnt.Template_Description = (lblTemDescription.Text);


                dt.Rows.Add(TempID, shopTemEnt.Shop_ID,shopTemEnt.Template_Description);
            }

            shopTemBl.Insert(dt);//update
            ////}
            ////else
                //{
                
            shopTemBl.Update(dt);
            ////}


        }









        }







       
      


        }







    


