using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using ORS_RCM_BL;
using System.Globalization;

namespace ORS_RCM.Admin
{
    public partial class Exhibition_Flag_Change : System.Web.UI.Page
    {
        Item_ImportLog_BL itbl = new Item_ImportLog_BL();
        GlobalBL gb = new GlobalBL();
        Item_ExportField_BL itfield_bl = new Item_ExportField_BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ddlMall.DataTextField = "Code_Description";
                    ddlMall.DataValueField = "ID";
                    ddlMall.DataSource = gb.Code_Setup(1);
                    ddlMall.DataBind();
                    ddlMall.Items.Insert(0, new ListItem("ショップ選択", ""));

                    DataTable dtex = itfield_bl.SelectUser();

                    ddlExhibitor.DataTextField = "User_Name";
                    ddlExhibitor.DataValueField = "ID";
                    ddlExhibitor.DataSource = dtex;
                    ddlExhibitor.DataBind();
                    ddlExhibitor.Items.Insert(0, "");
                }
                else
                {
                    if ((itemcode.Value != "" && itemcode.Value != null) && (mall.Value != "" && mall.Value != null))
                        btnChangeFlag.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?"); 
            }
        }

        protected void btnChangeFlag_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    string itemcode = txtItemCode.Text.Trim();
                  

                    //int exhibitor = Convert.ToInt32(ddlExhibitor.SelectedItem.Value);
                    int shop = Convert.ToInt32(ddlMall.SelectedItem.Value);

                    itbl.Update_Exhibited_ErrorItemCodeValue(itemcode, shop);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Process Success')", true);
                    btnChangeFlag.Enabled = false;
                }
                else
                {
                    btnChangeFlag.Enabled = true;
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