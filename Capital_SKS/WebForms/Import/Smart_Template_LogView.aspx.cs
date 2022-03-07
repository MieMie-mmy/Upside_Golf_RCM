/* 
Created By              : Kay Thi Aung
Created Date          : 04/07/2014
Updated By             :
Updated Date         :

 Tables using: Item_ImportLog,Item_Import_ErrorLog
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

namespace ORS_RCM
{
	public partial class Smart_Template_LogView : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                if (Request.QueryString["ErrorLog_ID"] != null && Request.QueryString["Recordcount"] != null)
                {
                    int Logid = Convert.ToInt32(Request.QueryString["ErrorLog_ID"].ToString());

                    Item_ImportLog_BL imbl = new Item_ImportLog_BL();
                    DataTable dt = imbl.ItemErrotLogSelectAll(6, Logid);
                    String[] coltype = { "Error_Message" };
                    DataColumn newcol = new DataColumn("チェック", typeof(String));
                    newcol.DefaultValue = "";
                    dt.Columns.Add(newcol);
                    DataTable dtt = CheckErrorMsg(dt, coltype);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gvlog.DataSource = dtt;
                        gvlog.DataBind();
                    }
                    else { gvlog.DataSource = null; gvlog.DataBind(); }
                }
                else	if (Request.QueryString["LogID"] != null)
				{
					int Logid = Convert.ToInt32(Request.QueryString["LogID"].ToString());

					Item_ImportLog_BL imbl = new Item_ImportLog_BL();
					DataTable dt = imbl.ItemMasterLogSelectAll(6, Logid);
					String[] coltype = { "Error_Message" };
					DataColumn newcol = new DataColumn("チェック", typeof(String));
					newcol.DefaultValue = "";
					dt.Columns.Add(newcol);
					DataTable dtt = CheckErrorMsg(dt, coltype);
					if (dt != null && dt.Rows.Count > 0)
					{
						gvlog.DataSource = dtt;
						gvlog.DataBind();
					}
					else { gvlog.DataSource = null; gvlog.DataBind(); }


					//DataTable dts = itbl.SmartLogData(Logid);

					//gvlog.DataSource = dts;
					//gvlog.DataBind();
				}

			}
		}

		protected void gvlog_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			try
			{
                Item_ImportLog_BL imbl = new Item_ImportLog_BL();
                if (Request.QueryString["ErrorLog_ID"] != null && Request.QueryString["Recordcount"] != null)
                {
                    int Logid = Convert.ToInt32(Request.QueryString["ErrorLog_ID"].ToString());


                    DataTable dt = imbl.ItemErrotLogSelectAll(6, Logid);
                    String[] coltype = { "Error_Message" };
                    DataColumn newcol = new DataColumn("チェック", typeof(String));
                    newcol.DefaultValue = "";
                    dt.Columns.Add(newcol);
                    DataTable dtt = CheckErrorMsg(dt, coltype);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gvlog.DataSource = dtt;
                        gvlog.PageIndex = e.NewPageIndex;
                        gvlog.DataBind();
                    }
                }

                if (Request.QueryString["LogID"] != null)
                {
                    int Logid = Convert.ToInt32(Request.QueryString["LogID"].ToString());


                    DataTable dt = imbl.ItemMasterLogSelectAll(6, Logid);
                    String[] coltype = { "Error_Message" };
                    DataColumn newcol = new DataColumn("チェック", typeof(String));
                    newcol.DefaultValue = "";
                    dt.Columns.Add(newcol);
                    DataTable dtt = CheckErrorMsg(dt, coltype);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gvlog.DataSource = dtt;
                        gvlog.PageIndex = e.NewPageIndex;
                        gvlog.DataBind();
                    }
                }

			}
			catch (Exception ex)
			{
				Session["Exception"] = ex.ToString();
				Response.Redirect("~/CustomErrorPage.aspx?");
			}
		}

		protected DataTable CheckErrorMsg(DataTable dt, String[] col)
		{
			try
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					for (int j = 0; j < col.Length; j++)
					{
						if (!String.IsNullOrWhiteSpace(dt.Rows[i][col[j]].ToString()))
						{
							dt.Rows[i]["チェック"] = "エラー";

						}
					}
				}
				return dt;
			}
			catch (Exception ex)
			{
				Session["Exception"] = ex.ToString();
				Response.Redirect("~/CustomErrorPage.aspx?");
				return dt;
			}
		}
	}
}