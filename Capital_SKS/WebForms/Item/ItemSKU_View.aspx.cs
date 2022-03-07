/* 
Created By              : Eephyo/Aye Mon
Created Date          : 16/07/2014
Updated By             :
Updated Date         :

 Tables using: Item
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
using ORS_RCM_BL;
using ORS_RCM.WebForms.Item;
using System.Data;

namespace ORS_RCM.WebForms.Item
{
    public partial class ItemSKU_View : System.Web.UI.Page
    {
        //Item_SKUView_BL itSkubl = new Item_SKUView_BL();

        Item_BL item = new Item_BL();

        public string Item_Code
        {
            get
            {
                if (Request.QueryString["Item_Code"] != null)
                {
                    return (Request.QueryString["Item_Code"].ToString());
                }
                else
                {
                    return null;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Item_Code != null)
                    {
                        lblitemname.Text = Item_Code;
                        //gvItem.DataSource = itSkubl.Search(Item_Code);
                        //gvItem.DataBind();
                        //string html = "<table border=1 class=\"listTable\"><tr><th style=\"width:30px\">&nbsp;</th>";
                        string html = "<table class=\"listTable\"><tbody><tr><th>&nbsp;</th>";
                        DataTable dtSKUHeader = item.GetSKUHeader(Item_Code);
                        DataTable dtSKUQuantity = item.GetSKUQuantity(Item_Code);

                        foreach (DataRow dr in dtSKUHeader.Rows)
                        {
                            html += String.Format("<th>{0}<span>{1}</span></th>", dr["Size_Name"].ToString(), dr["Size_Code"].ToString());
                        }
                        html += "</tr>";

                        string criColor = "";
                        dtSKUQuantity.DefaultView.Sort = "Color_Code ASC";
                        dtSKUQuantity = dtSKUQuantity.DefaultView.ToTable();
                        string[] arr = new string[3];

                        DataTable dtTmp = dtSKUQuantity;
                        dtTmp.DefaultView.Sort = "Size_Code ASC";
                        dtTmp = dtTmp.DefaultView.ToTable();                        

                        foreach (DataRow drQty in dtSKUQuantity.Rows)
                        {
                            //if (!String.IsNullOrWhiteSpace(drQty["Color_Code"].ToString()))
                            if(!String.IsNullOrEmpty(drQty["Color_Code"].ToString()))
                            {
                                if (!drQty["Color_Code"].Equals("─"))
                                {
                                    string colorcode = drQty["Color_Code"].ToString();
                                    DataRow[] drTmp = dtTmp.Select("Color_Code = '" + colorcode + "'");

                                    if (criColor != drQty["Color_Code"].ToString())
                                    {
                                        html += "<tr>";
                                        html += String.Format("<td>{0}<span>{1}</span></td>", drQty["Color_Name"].ToString(), drQty["Color_Code"].ToString());


                                        //foreach (DataRow drSize in dtSKUHeader.Rows)
                                        //{
                                        //    if (drSize["Size_Code"].ToString() == drQty["Size_Code"].ToString())
                                        //    {
                                        //        html += String.Format("<td>{0}</td>", drQty["Quantity"].ToString());
                                        //    }
                                        //    else
                                        //    {
                                        //        html += String.Format("<td>{0}</td>", "&nbsp");
                                        //    }
                                        //}

                                        int i = 0;
                                        foreach (DataRow drSize in dtSKUHeader.Rows)
                                        {
                                            if (drSize["Size_Code"].ToString() == drTmp[i]["Size_Code"].ToString())
                                            {
                                                html += String.Format("<td>{0}</td>", drTmp[i]["Quantity"].ToString());
                                                if (i < drTmp.Count() - 1)
                                                {
                                                    i++;
                                                }
                                            }
                                            else
                                            {
                                                html += String.Format("<td>{0}</td>", "&nbsp");
                                            }
                                        }
                                        html += "</tr>";
                                    }

                                    criColor = drQty["Color_Code"].ToString();
                                }
                            }
                        }
                        html += "</tbody></table>";

                        divTable.InnerHtml = html;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            } 
        }

        //protected void gvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    if (Item_Code != null)
        //    {
        //        gvItem.DataSource = itSkubl.Search(Item_Code);
        //        gvItem.PageIndex = e.NewPageIndex;
        //        gvItem.DataBind();
        //    }

        //}

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", String.Format("GetRowValue({0})","test"), true);
        //    string fun = String.Format("GetRowValue({0})", "test");
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", fun, true);
        //}
    }
}

    

        

    
