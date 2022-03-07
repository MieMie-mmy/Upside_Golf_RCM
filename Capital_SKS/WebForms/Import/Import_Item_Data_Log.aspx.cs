/* 
Created By              : Kay Thi Aung
Created Date          : 
Updated By             :
Updated Date         :

 Tables using           : Item_ImportLog
 *                                 -Item_Import_ItemLog
 *                                 -
 *                                    
 *                                   
 * 
 *                                  
 * Storedprocedure using:
 *                                           -
 *                                           -
 *                                           -
 *                                           
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

namespace ORS_RCM.WebForms.Import
{
    public partial class Import_Item_Data_Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                try
                {
                    if (Request.QueryString["ErrorLog_ID"] != null)
                    {
                        int Logid = Convert.ToInt32(Request.QueryString["ErrorLog_ID"].ToString());

                        DataTable dts = null;
                       
                        gvitem.DataSource = dts;
                        gvitem.DataBind();
                    }

                    if (Request.QueryString["LogID"] != null)
                    {
                        int Logid = Convert.ToInt32(Request.QueryString["LogID"].ToString());

                        DataTable dts = BindData(Logid);
                        dts = ChangeHeader(dts);
                        Cache.Insert("DataTable", dts, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero);
                        gvitem.DataSource = dts;
                        gvitem.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
                  

                }       
            
            }
        }

     
        protected  DataTable BindData(int id) 
        {
            DataTable dt = new DataTable();
            try
            {
              
                Import_Item_Data_BL itbl = new Import_Item_Data_BL();
              dt = itbl.LogData(id);
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;

            }       
        }

        protected void gvitem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //int Logid = Convert.ToInt32(Request.QueryString["LogID"].ToString());
                DataTable dtdata = new DataTable();
                //dtdata = ChangeHeader(dtdata);
                if (Cache["DataTable"] != null)
                {
                    dtdata = Cache["DataTable"] as DataTable;
                }
                gvitem.DataSource = dtdata;
                gvitem.PageIndex = e.NewPageIndex;
                gvitem.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");


            }       
        }

        protected DataTable ChangeHeader(DataTable dt)
        {
            try
            {
                if (dt.Columns.Contains("Product_Code"))
                {
                    dt.Columns["Product_Code"].ColumnName = "製品コード";

                }
                if (dt.Columns.Contains("Item_Code"))
                {
                    dt.Columns["Item_Code"].ColumnName = "商品番号";
                }
                if (dt.Columns.Contains("Item_Name"))
                {
                    dt.Columns["Item_Name"].ColumnName = "商品名";

                }
                if (dt.Columns.Contains("List_Price"))
                {
                    dt.Columns["List_Price"].ColumnName = "定価";
                }
                if (dt.Columns.Contains("Sale_Price"))
                {
                    dt.Columns["Sale_Price"].ColumnName = "販売価格";
                }
                if (dt.Columns.Contains("Cost"))
                {
                    dt.Columns["Cost"].ColumnName = "原価";
                }
                if (dt.Columns.Contains("Release_Date"))
                {
                    dt.Columns["Release_Date"].ColumnName = "発売日";
                }
                if (dt.Columns.Contains("Post_Available_Date"))
                {
                    dt.Columns["Post_Available_Date"].ColumnName = "掲載可能日";

                }
                if (dt.Columns.Contains("Year"))
                {
                    dt.Columns["Year"].ColumnName = "年度";
                }
                if (dt.Columns.Contains("Season"))
                {
                    dt.Columns["Season"].ColumnName = "シーズン";
                }
                if (dt.Columns.Contains("Brand_Name"))
                {
                    dt.Columns["Brand_Name"].ColumnName = "ブランド名";
                }
                if (dt.Columns.Contains("Brand_Code"))
                {
                    dt.Columns["Brand_Code"].ColumnName = "ブランドコード";
                }
                if (dt.Columns.Contains("Postage"))
                {
                    dt.Columns["Postage"].ColumnName = "送料";

                }
                if (dt.Columns.Contains("Competition_Name"))
                {
                    dt.Columns["Competition_Name"].ColumnName = "競技名";
                }
                if (dt.Columns.Contains("Class_Name"))
                {
                    dt.Columns["Class_Name"].ColumnName = "分類名";
                }
                if (dt.Columns.Contains("Company_Name"))
                {
                    dt.Columns["Company_Name"].ColumnName = "仕入先名";
                }
                if (dt.Columns.Contains("Merchandise_Information"))
                {
                    dt.Columns["Merchandise_Information"].ColumnName = "商品情報";
                }
                if (dt.Columns.Contains("Item_Description_PC"))
                {
                    dt.Columns["Item_Description_PC"].ColumnName = "PC用商品説明文";
                }
                if (dt.Columns.Contains("Item_Description_Phone"))
                {
                    dt.Columns["Item_Description_Phone"].ColumnName = "スマートフォン用商品説明文";

                }
                if (dt.Columns.Contains("Sale_Description_PC"))
                {
                    dt.Columns["Sale_Description_PC"].ColumnName = "PC用販売説明文";
                }
                if (dt.Columns.Contains("Rakuten_CategoryID"))
                {
                    dt.Columns["Rakuten_CategoryID"].ColumnName = "楽天カテゴリID";
                }


                if (dt.Columns.Contains("Yahoo_CategoryID"))
                {
                    dt.Columns["Yahoo_CategoryID"].ColumnName = "ヤフーカテゴリID";
                }


                if (dt.Columns.Contains("Ponpare_CategoryID"))
                {
                    dt.Columns["Ponpare_CategoryID"].ColumnName = "ポンパレカテゴリID";
                }
                if (dt.Columns.Contains("Extra_Shipping"))
                {
                    dt.Columns["Extra_Shipping"].ColumnName = "個別送料";
                }
                if (dt.Columns.Contains("Warehouse_Specified"))
                {
                    dt.Columns["Warehouse_Specified"].ColumnName = "倉庫指定";
                }


                if (dt.Columns.Contains("BlackMarket_Password"))
                {
                    dt.Columns["BlackMarket_Password"].ColumnName = "闇市パスワード";
                }
                if (dt.Columns.Contains("Item_AdminCode"))
                {
                    dt.Columns["Item_AdminCode"].ColumnName = "販売管理番号";
                }
                if (dt.Columns.Contains("Yahoo_URL"))
                {
                    dt.Columns["Yahoo_URL"].ColumnName = "YahooエビデンスURL";
                }

                dt.AcceptChanges();
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

               

                
              

              
             
             
               

            
                

                
              
               
              

               
             

              
               

                
              

              
             
                
               
              

              
               

              

             
             

              
             

              
               

              
              

               

               
             

            
               
            