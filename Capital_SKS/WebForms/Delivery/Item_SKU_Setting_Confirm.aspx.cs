using ORS_RCM_BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ORS_RCM.WebForms.Delivery
{
    public partial class Item_SKU_Setting_Confirm : System.Web.UI.Page
    {
        Import_Item_BL itbl = new Import_Item_BL();
        Item_BL ibl = new Item_BL();
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        public String LogIds = String.Empty;

        public int Userid
        {
            get
            {
                if (Session["User_ID"] != null)

                    return Int32.Parse(Session["User_ID"].ToString());
                else
                    return 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                   
                    if (Request.QueryString["Delivery"] != null)//check delivery file selected
                    {
                        String deli = Request.QueryString["Delivery"].ToString();
                        if (deli  == "")
                        {
                            headerdelivery.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                            divdelivery.Visible = false;
                        }
                        if (!String.IsNullOrWhiteSpace(deli))
                            DeliveryImport(deli);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void DeliveryImport(string delivery)
        {
          
            String Delivery = Server.MapPath("~/Import_CSV/") + delivery;
            DataTable dt = GlobalUI.CSVToTable(Delivery);
            dt = GlobalUI.Remove_Doublecode(dt);
            //dt = dtSku.Copy();
            //dt = ColumnNameChange(dt);
            String[] colName = { "商品番号", "カラー", "サイズ", "カラーコード", "サイズコード", "楽天発送番号", "ヤフー発送番号" };
            if (CheckColumn(colName, dt))//check datatable column's header is true
            {
                dt = ColumnNameEng(dt);//change japanese header to english
                DataColumn newcol = new DataColumn("チェック", typeof(String));//add check column to datatable that show error or not
                newcol.DefaultValue = "";
                dt.Columns.Add(newcol);//add check column to datatable that show error or not
                DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
                newColumn.DefaultValue = 6;//type 6 = ok , type 5 = error
                dt.Columns.Add(newColumn);//add type column to datatable to seperate error record and  ok record

                DataColumn dc = new DataColumn("エラー内容", typeof(String));
                dc.DefaultValue = "";
                dt.Columns.Add(dc);//add error detail column to datatabel to show error message detail

                String[] colCheckLength = { "Item_Code", "Color_Code", "Size_Code" };//need to check this column value's length is greater than 50
                DataTable dterr = CheckLength(dt, colCheckLength, 50, 1);

                String[] colCheckLength1 = { "Color_Name", "Size_Name" };//need to check this column value's length is greater than 200
                dterr = CheckLength(dterr, colCheckLength1, 200, 1);

                String[] colCheckType = { "Rakuten_ShippingNo", "Yahoo_ShippingNo" };
                dterr = checkIntType(dterr, colCheckType, 0);//need to check this column value is integer


                //dterr = checkShippingNo(dt);
                //checkShippingNo(dt);

                

                gvdelivery.DataSource = dterr;
                Cache.Insert("dtdelivery", dterr, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);//save datatable to cache
                gvdelivery.DataBind();
               
            }
            else
            {
                GlobalUI.MessageBox("File Format Wrong!");
            }
        }
        protected String DeliverySetting()
        {
            try
            {
                Import_Item_BL itbl = new Import_Item_BL();
                Item_ImportLog_BL ItemImbl = new Item_ImportLog_BL();

                if (Cache["dtdelivery"] != null)
                {
                    DataTable dt = Cache["dtdelivery"] as DataTable;
                    DataColumn newColumn = new DataColumn("User_ID", typeof(System.Int32));
                    newColumn.DefaultValue = Userid;
                    dt.Columns.Add(newColumn);//add imported user
                    int result = itbl.Delivery(dt);//insert data
                    if (result > 0)
                    {
                        return result.ToString();
                    }
                    return String.Empty;
                }
                else
                {
                    GlobalUI.MessageBox("Incorrect Delivery File Format!");
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                string line = ex.Message.ToString();
                string replaceWith = "";
                line.Replace(System.Environment.NewLine, "replacement text");
                string line2 = Regex.Replace(line, @"\r\n?|\n", replaceWith);
                hfdmsg.Value = line2;
                return String.Empty;
            }
        }

        
     
        public void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Import_Error.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        //protected void  checkShippingNo(DataTable dt)
        //{
            
        //    string sno = string.Empty;
        //    for (int i = 0; i< dt.Rows.Count;i++)
        //    {
        //        sno = dt.Rows[i]["Rakuten_ShippingNo"].ToString();
        //        DataTable dtu = ibl.ShippingNo_Select(sno,0);
        //        if (dtu.Rows.Count <= 0)
        //        {
        //            dt.Rows.RemoveAt(i);
        //            dt.AcceptChanges();
        //        }
        //        //else
        //        //{
        //        //    sno = dt.Rows[i]["Yahoo_ShippingNo"].ToString();
        //        //    DataTable dty = ibl.ShippingNo_Select(sno, 1);
        //        //    if (dty.Rows.Count <= 0)
        //        //    {
        //        //        dt.Rows.RemoveAt(i);
        //        //        dt.AcceptChanges();
        //        //    }
        //        //}
        //    }
        //}
           

        //check the value is integer

        protected DataTable checkIntType(DataTable dt, String[] col, int type)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < col.Length; j++)
                    {
                        try
                        {
                            if (!String.IsNullOrWhiteSpace(dt.Rows[i][col[j]].ToString()))
                                Convert.ToInt32(dt.Rows[i][col[j]].ToString());//check integer or not**(convert value to int-- if ok --> integer error occur -- go to cache)
                        }
                        catch (Exception)//if not integer
                        {
                            dt.Rows[i]["チェック"] = "エラー";//error 
                            dt.Rows[i]["エラー内容"] = EngToJpHeader(col[j], type) + "のフォーマットが不正です。";//error detail
                            dt.Rows[i]["Type"] = 5;//error type
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

        //check length by byte
        protected DataTable CheckLength(DataTable dt, String[] col, int length, int type)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < col.Length; j++)
                    {
                        Encoding enc = Encoding.GetEncoding(932);
                        int byteLength = enc.GetByteCount(dt.Rows[i][col[j]].ToString());//check length by byte
                        if (byteLength > length)//check value is greater than limit
                        {
                            dt.Rows[i]["チェック"] = "エラー";//error
                            dt.Rows[i]["エラー内容"] = EngToJpHeader(col[j], type) + "-Greater than " + length + " Bytes";//error detail
                            dt.Rows[i]["Type"] = 5;//type
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
       
        //change english header to japanese
        protected String EngToJpHeader(String Eng, int type)
        {
            try
            {
                if (type == 0)//delivery
                {
                    switch (Eng)
                    {

                        case "Item_Code": return "商品番号";
                        case "Size_Name": return "サイズ";
                        case "Color_Name": return "カラー";
                        case "Size_Code": return "サイズコード";
                        case "Color_Code": return "カラーコード";
                        case "Yahoo_ShippingNo": return "ヤフー発送番号";
                        case "Rakuten_ShippingNo": return "楽天発送番号";
                        default: return "";
                    }
                }
                     
                  
                return "";
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return "";
            }
        }

        //check column is contain or not in datatable
        protected Boolean CheckColumn(String[] colName, DataTable dt)
        {
            try
            {
                DataColumnCollection col = dt.Columns;
                for (int i = 0; i < colName.Length; i++)
                {
                    if (!col.Contains(colName[i]))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

      

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cache["dtdelivery"] != null)
                {
                    string delivery_setting = string.Empty;
                    delivery_setting = DeliverySetting();
                    Cache.Remove("dtdelivery");
                    
                }
            }
            catch (Exception ex)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
            Response.Redirect("~/WebForms/Delivery/Item_SKU_Setting.aspx?");
        }


        protected void gvdelivery_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            if (Cache["dtdelivery"] != null)
            {
                DataTable dtm = Cache["dtdelivery"] as DataTable;
                gvdelivery.PageIndex = e.NewPageIndex;
                gvdelivery.DataSource = dtm;
                gvdelivery.DataBind();
            }
        }

        protected DataTable ColumnNameChange(DataTable dt)
        {
            try
            {
                dt.Columns["Item_Code"].ColumnName = "商品番号";
                dt.Columns["Size_Code"].ColumnName = "サイズコード";
                dt.Columns["Color_Code"].ColumnName = "カラーコード";
                dt.Columns["Size_Name"].ColumnName = "サイズ";
                dt.Columns["Color_Name"].ColumnName = "カラー";
                dt.Columns["Rakuten_ShippingNo"].ColumnName = "楽天発送番号";
                dt.Columns["Yahoo_ShippingNo"].ColumnName = "ヤフー発送番号";
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return dt;
            }
        }
        protected DataTable ColumnNameEng(DataTable dt)
        {
            try
            {
                dt.Columns["商品番号"].ColumnName = "Item_Code";
                dt.Columns["サイズコード"].ColumnName = "Size_Code";
                dt.Columns["カラーコード"].ColumnName = "Color_Code";
                dt.Columns["サイズ"].ColumnName = "Size_Name";
                dt.Columns["カラー"].ColumnName = "Color_Name";
                dt.Columns["楽天発送番号"].ColumnName = "Rakuten_ShippingNo";
                dt.Columns["ヤフー発送番号"].ColumnName = "Yahoo_ShippingNo";
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