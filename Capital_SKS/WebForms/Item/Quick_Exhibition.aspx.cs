using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;

namespace ORS_RCM.WebForms.Item
{
    public partial class Quick_Exhibition : System.Web.UI.Page
    {
        public int User_ID
        {
            get
            {
                if (Session["User_ID"] != null)
                {
                    return Convert.ToInt32(Session["User_ID"]);
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

            }
        }

        protected void btnExhibition_Click(object sender, EventArgs e)
        {
            try
            {
                string list = null;

                if (!string.IsNullOrWhiteSpace(txtItem_Codes.Text))
                {
                    Exhibition_List_BL ehb = new Exhibition_List_BL();

                    string[] strArr = txtItem_Codes.Text.Split(',');

                    for (int i = 0; i < strArr.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(strArr[i]))
                        {
                            int eid;
                            eid = ehb.Quick_Exhibition_List_Insert(strArr[i], User_ID);
                            //eid = SaveExhibitionData(Convert.ToInt32(strArr[i]), null, User_ID);

                            if (eid != 0)
                            {
                                //SaveExhibitionShop(eid, Convert.ToInt32(strArr[i]));
                                list += eid + ",";
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(list))
                    {
                        //Remove last comma from string
                        list = list.TrimEnd(','); // (OR) itemIDList.Remove(str.Length-1); 
                        Session.Remove("list");
                        Session["list"] = list;
                    }

                    string url = "../Item_Exhibition/Exhibition_List_Log.aspx?list=" + 1;
                    Response.Redirect(url);
                }
                else
                {
                    GlobalUI.MessageBox("Please enter item codes to exhibit ! ");
                }

            }
           catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
                }
            }
      }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                Exhibition_List_BL ehb = new Exhibition_List_BL();
                gvquickexhibition.DataSource = ehb.SelectAll_Not_Quick_Exhibition(); 
                gvquickexhibition.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvquickexhibition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Exhibition_List_BL ehb = new Exhibition_List_BL();
            gvquickexhibition.PageIndex = e.NewPageIndex;
            gvquickexhibition.DataSource = ehb.SelectAll_Not_Quick_Exhibition();
            gvquickexhibition.DataBind();
        }
    }
}