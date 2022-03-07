using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;
using System.Globalization;
using System.Text;
using System.IO;

namespace ORS_RCM.WebForms.Jisha
{
    public partial class Jisha_Order_Download : System.Web.UI.Page
    {
        Jisha_Order_Download_BL jhbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                jhbl = new Jisha_Order_Download_BL();
                gvorder.DataSource = jhbl.SelectAll();
                gvorder.DataBind();
            }
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            txtexdatetime1.Text = String.Empty;
            hdfFromDate.Value = Request.Form[txtexdatetime1.UniqueID];
            txtdatetime2.Text = Request.Form[txtdatetime2.UniqueID];
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            txtdatetime2.Text = String.Empty;
            hdfToDate.Value = Request.Form[txtdatetime2.UniqueID];
            txtexdatetime1.Text = Request.Form[txtexdatetime1.UniqueID];
        }

        protected DataTable changeHeader(DataTable dt)
        {
            dt.Columns["Order_ID"].ColumnName = "受注番号";
            dt.Columns["Order_Date"].ColumnName = "注文日時";
            dt.Columns["Item_Code"].ColumnName = "商品番号";
            dt.Columns["Item_Name"].ColumnName = "商品名";
            dt.Columns["Quantity"].ColumnName = "個数";
            dt.Columns["Price"].ColumnName = "単価";
            dt.Columns["SKU_Option"].ColumnName = "項目・選択肢";
            dt.Columns["Bill_LastName"].ColumnName = "注文者名字";
            dt.Columns["Bill_FirstName"].ColumnName = "注文者名前";
            dt.Columns["Bill_LastName_Kana"].ColumnName = "注文者名字フリガナ";
            dt.Columns["Bill_FirstName_Kana"].ColumnName = "注文者名前フリガナ";
            dt.Columns["Bill_MailAddress"].ColumnName = "メールアドレス";
            dt.Columns["Bill_ZipCode"].ColumnName = "注文者郵便番号１";
            dt.Columns["Bill_Prefecture"].ColumnName = "注文者住所：都道府県";
            dt.Columns["Bill_City"].ColumnName = "注文者住所：都市区";
            dt.Columns["Bill_Address1"].ColumnName = "注文者住所：町以降";
            dt.Columns["Bill_Address2"].ColumnName = "注文者住所：ビル・マンション名";
            dt.Columns["Bill_PhoneNo"].ColumnName = "注文者電話番号１";
            dt.Columns["Bill_Emg_PhoneNo"].ColumnName = "注文者電話番号２";
            dt.Columns["Ship_LastName"].ColumnName = "送付先名字";
            dt.Columns["Ship_FirstName"].ColumnName = "送付先名前";
            dt.Columns["Order_ID"].ColumnName = "受注番号";
            dt.Columns["Order_ID"].ColumnName = "受注番号";
            dt.Columns["Order_ID"].ColumnName = "受注番号";
            return dt;
        }

        protected void btnOrderDownload_Click(object sender,EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            int index = row.RowIndex;

            Label lbl = gvorder.Rows[index].FindControl("lblDate") as Label;
            String date = ConvertDateFormat(lbl.Text, "MM/dd/yyyy hh:mm:ss");

            Jisha_Order_Download_BL jodbl = new Jisha_Order_Download_BL();
            DataTable dt = jodbl.SelectByDate(date);


            date=date.Replace("/", "_");
            using (StreamWriter writer = new StreamWriter(Server.MapPath("~/Export_CSV/OrderDownload_" + date + ".csv"),false,Encoding.GetEncoding(932)))
            {
                WriteDataTable(dt, writer, true);
            }


            LinkButton lnk = gvorder.Rows[index].FindControl("lnkDownload") as LinkButton;
            lnk.Visible = true;
            lnk.Text = "OrderDownload_" + date + ".csv";
        }

        protected String ConvertDateFormat(String datetime, String Format)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            String str = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ToString();

            if (str.Equals("dd/MM/yyyy"))
                dtfi.ShortDatePattern = "dd-MM-yyyy";
            else if (str.Equals("MM/dd/yyyy"))
                dtfi.ShortDatePattern = "MM-dd-yyyy";

            dtfi.DateSeparator = "-";
            DateTime dt = Convert.ToDateTime(datetime, dtfi);
            string datevalue = Convert.ToDateTime(dt, CultureInfo.GetCultureInfo("en-US")).ToString(Format);
            return datevalue;
        }

        protected void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                List<string> headerValues = new List<string>();
                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerValues.Add(QuoteValue(column.ColumnName));
                }

                writer.WriteLine(String.Join(",", headerValues.ToArray()));
            }

            String[] items = null;
            foreach (DataRow row in sourceTable.Rows)
            {
                items = row.ItemArray.Select(o => QuoteValue(o.ToString())).ToArray();
                writer.WriteLine(String.Join(",", items));
            }

            writer.Flush();
        }

        protected String QuoteValue(string value)
        {
            return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            int index = row.RowIndex;

            string filePath = Server.MapPath("~/Export_CSV/"+btn.Text);
            Response.ContentType = "csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
            Response.TransmitFile(filePath);
            Response.End();
        }   
    }
}