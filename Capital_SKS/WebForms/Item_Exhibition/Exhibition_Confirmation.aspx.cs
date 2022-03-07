using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;
using ORS_RCM.WebForms.Item;
using System.IO;
using System.Configuration;

namespace ORS_RCM.WebForms.Item_Exhibition
{
    public partial class Exhibition_Confirmation : System.Web.UI.Page
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
        Exhibition_List_BL ehb;
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        string list; string list1;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["list"] != null && Request.QueryString["delete"] != null)
                    {
                        deleteexb.Visible = true;
                        ehb = new Exhibition_List_BL();
                        list = null;
                        list = Session["ItemIDDeleteList"].ToString();
                        DataTable dt = ehb.SelectAll(list, null, null, null, 8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                        gvlist.DataSource = dt;
                        gvlist.DataBind();
                    }
                    else if (Request.QueryString["IDlist"] != null)
                    {
                        exb.Visible = true;
                        ehb = new Exhibition_List_BL();
                        list = null;
                        list = Session["ItemIDList"].ToString();
                        DataTable dt = ehb.SelectAll(list, null, null, null, 8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dt.Rows[i]["Export_Status"].ToString()) != 3 && Convert.ToInt32(dt.Rows[i]["Export_Status"].ToString()) != 4)
                            {
                                dt.Rows[i]["Permission"] = "Item_Code have no permission for process";
                            }
                        }
                        gvlist.DataSource = dt;
                        gvlist.DataBind();
                    }
                    //update by Kay Thi 
                    else if (Request.QueryString["Viewlist"] != null)
                    {
                        exb.Visible = true;
                        ehb = new Exhibition_List_BL();
                        list = null;
                        list = Session["Item2IDlist"].ToString();
                        DataTable dt = ehb.SelectAll(list, null, null, null, 8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dt.Rows[i]["Export_Status"].ToString()) != 3 && Convert.ToInt32(dt.Rows[i]["Export_Status"].ToString()) != 4)
                            {
                                dt.Rows[i]["Permission"] = "Item_Code have no permission for process";
                            }
                        }
                        gvlist.DataSource = dt;
                        gvlist.DataBind();
                    }
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ExhibitionData();
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

        protected int SaveExhibitionData(int itemID, string flag, int user_id)
        {
            ehb = new Exhibition_List_BL();
            return ehb.Exhibition_List_Insert(itemID, flag, user_id);
        }

        protected void SaveExhibitionShop(int EitemID, int itemID)
        {
            ehb = new Exhibition_List_BL();
            ehb.Exhibition_Item_Shop_Insert(EitemID, itemID);
        }

        protected void ExhibitionData()
        {
            try
            {
                int count = 0;  
                ehb = new Exhibition_List_BL();
                if (Request.QueryString["delete"] != null)
                {
                    list = null;
                    list = Session["ItemIDDeleteList"].ToString();
                    Session.Remove("ItemIDDeleteList");
                    Export_CSV_Delete CSVDelete = new Export_CSV_Delete();
                    CSVDelete.CSV_Delete(list);
                    if (!string.IsNullOrWhiteSpace(list))
                    {
                        ehb = new Exhibition_List_BL();
                        ehb.DeleteUpdateOrder(list); // Ei Thinzar Zaw (for SKS-250)
                        string[] strArr = list.Split(',');
                        list = null;
                        for (int i = 0; i < strArr.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(strArr[i]))
                            {
                                int eid;
                                eid = SaveExhibitionData(Convert.ToInt32(strArr[i]), Request.QueryString["delete"].ToString(), User_ID);
                                if (eid != 0)
                                {
                                    SaveExhibitionShop(eid, Convert.ToInt32(strArr[i]));
                                    SaveExhibitionItemDeleteFile(eid);
                                    list += eid + ",";
                                }
                            }
                        }
                    }
                    Item_ExportQ_BL ieBL = new Item_ExportQ_BL();
                    ieBL.ChangeIsExportFlag(); //IsExport=0 Where IsExport=2
                }//for delete

                if (Request.QueryString["IDlist"] != null)//for itemview
                {
                    list = null;
                    list = Session["ItemIDList"].ToString();
                    Session.Remove("ItemIDList");
                    if (!string.IsNullOrWhiteSpace(list))
                    {
                        string[] strArr = list.Split(',');
                        list = null;
                        for (int i = 0; i < strArr.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(strArr[i]))
                            {
                                if (CheckSKSStatus(strArr[i]))
                                {
                                    int eid;
                                    ChangeFlagForSoko(Convert.ToInt32(strArr[i]), 1);//25-09-2017 Warehouse
                                    eid = SaveExhibitionData(Convert.ToInt32(strArr[i]), null, User_ID);

                                    if (eid != 0)
                                    {
                                        list += eid + ",";
                                    }
                                }
                                
                            }
                        }
                    }
                }
                if (Request.QueryString["Viewlist"] != null)//for itemview2
                {
                    list = null;                
                    list = Session["Item2IDlist"].ToString();
                    Session.Remove("Item2IDlist");
                    if (!string.IsNullOrWhiteSpace(list))
                    {
                        string[] strArr = list.Split(',');
                        list = null;
                        list1 = list;
                        for (int i = 0; i < strArr.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(strArr[i]))
                            {
                                if (CheckSKSStatus(strArr[i]))
                                {
                                    int eid;
                                    int flag = Convert.ToInt16(Request.QueryString["Viewlist"]);
                                    ChangeFlagForSoko(Convert.ToInt32(strArr[i]), flag);
                                    eid = SaveExhibitionData(Convert.ToInt32(strArr[i]), null, User_ID);
                                    if (eid != 0)
                                    {
                                        list += eid + ",";
                                    }
                                    count++;
                                }
                            }
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(list))
                {
                    //Remove last comma from string
                    list = list.TrimEnd(','); // (OR) itemIDList.Remove(str.Length-1);  
                    Session.Remove("list");
                    Session["list"] = list;
                    string url = "../Item_Exhibition/Exhibition_List_Log.aspx?list=" + 1;
                    Response.Redirect(url);
                }
                if (count > 0)
                {
                    //string url = "../Item_Exhibition/Exhibition_List_Log.aspx?list=" + 1;
                    //Response.Redirect(url);
                    //Response.Write("<script language='javascript'>window.alert('Process is Successful');window.location='" + url + "';</script>");
                    GlobalUI.MessageBox("Process is Successful. Item Code will be in auto exhibition list");
                }
                else
                {
                    //Response.Write("<script language='javascript'>window.alert('Process Fail');</script>");
                    GlobalUI.MessageBox("Process Fail");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ConsoleWriteLine_Tofile("Exhibition Confirmation Log: " + ex);
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
                }
            }
        }

        protected void SaveExhibitionItemDeleteFile(int eid)
        {
            Log_Exhibition_BL Log = new Log_Exhibition_BL();
            Log.SaveExhibitionDeleteData(eid);
        }

        protected void ChangeFlagForSoko(int eid, int flag)
        {
            ehb = new Exhibition_List_BL();
            ehb.ChangeFlagForSoko(eid, flag);
        }

        protected Boolean CheckSKSStatus(string list)
        {
            ehb = new Exhibition_List_BL();
            return ehb.CheckSKSStatus(list);
        }

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Exhibition_List_Log_Error.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
    }
}