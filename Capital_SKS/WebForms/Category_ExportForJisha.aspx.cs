
/*
* Created By Ei Phyo
* Created Date 2015/06/25  11:00
* Why use this Coding
* 
* To export  Category from SKS
* Description: exported category file will be imported from 自社
 *                    :
 *                    :
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
using ORS_RCM_Common;
using ORS_RCM_BL;
using System.Configuration;
using System.Transactions;
using System.Globalization;
using System.Collections;
using System.IO;
using System.Text;
using System.Net;
using Microsoft.VisualBasic.FileIO;

namespace ORS_RCM
{
    public partial class Category_ExportForJisha : System.Web.UI.Page
    {

        Mall_Category_BL mbl;
        String ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        static string localPath = ConfigurationManager.AppSettings["localPath"].ToString();
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        public static String ftpPath = ConfigurationManager.AppSettings["ftpPath"].ToString();
        public static String ftpUserName = ConfigurationManager.AppSettings["ftpUserName"].ToString();
        public static String ftpPassword = ConfigurationManager.AppSettings["ftpPassword"].ToString();
        public static String ftpHost = ConfigurationManager.AppSettings["ftpHost"].ToString();
        public static String csv = String.Empty;

        public static DateTime dtime = DateTime.Now;
   
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //CreateDir(localPath);
            }
            catch (Exception ex)
            {


            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                mbl = new Mall_Category_BL();
                DataTable dtExport = new DataTable();
                dtExport = mbl.CategoryExportFor_Jisha();
                //Generate CSV filel
                if (dtExport != null && dtExport.Rows.Count > 0)
                {
                    // Change encoding to Shift-JIS
                    using (StreamWriter writer = new StreamWriter(localPath + "Jisha_Category_Export.csv", false, Encoding.GetEncoding(932)))
                    {
                        WriteDataTable(dtExport, writer, true);
                    }

                           //C:\MyData\Projects\Export_CategoryForJisha
                  //  localPath = @"C:\MyData\Projects\Export_CategoryForJisha\";

                    localPath = ConfigurationManager.AppSettings["localPath"].ToString();
                    string fileName="Jisha_Category_Export.csv";

                    FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(ftpPath+ fileName);
                    requestFTPUploader.Credentials = new NetworkCredential("jisha_source", "94jk78tg#ego0kif");
                    requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;

                    FileInfo fileInfo = new FileInfo(localPath+fileName);
                    FileStream fileStream = fileInfo.OpenRead();

                    int bufferLength = 2048;
                    byte[] buffer = new byte[bufferLength];

                    Stream uploadStream = requestFTPUploader.GetRequestStream();
                    int contentLength = fileStream.Read(buffer, 0, bufferLength);

                    while (contentLength != 0)
                    {
                        uploadStream.Write(buffer, 0, contentLength);
                        contentLength = fileStream.Read(buffer, 0, bufferLength);
                    }
                     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Exported Successfully')", true);
        
                 
                    uploadStream.Close();
                    fileStream.Close();

                    requestFTPUploader = null;




                }

            }
            catch (Exception ex)
            {

                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }


        }

        public void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            try
            {
                if (includeHeaders)
                {
                    List<string> headerValues = new List<string>();

                    foreach (DataColumn column in sourceTable.Columns)
                    {
                        headerValues.Add(QuoteValue(column.ColumnName.ToLower()));
                    }
                    StringBuilder builder = new StringBuilder();
                    writer.WriteLine(String.Join(",", headerValues.ToArray()));
                }

                string[] items = null;
                foreach (DataRow row in sourceTable.Rows)
                {
                    items = row.ItemArray.Select(o => QuoteValue(o.ToString())).ToArray();
                    writer.WriteLine(String.Join(",", items));
                }

                writer.Flush();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        private string QuoteValue(string value)
        {
            try
            {
                return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
        }
 
       private static bool check(String str)
        {
            if (str.Contains(".csv"))
                return true;
            else
                return false;
        }

        public static Boolean CheckColumn(String[] colName1, DataTable dt)
        {
            DataColumnCollection col = dt.Columns;
            for (int i = 0; i < colName1.Length; i++)
            {
                if (col.Contains(colName1[i]))
                {
                    return true;
                }
            }
            return false;
        }

       public static DataTable CSVToTable(string filePath)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(filePath, Encoding.GetEncoding(932)))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    //read column names
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return csvData;
        }

       protected void ShowMsg(String str)
       {
           try
           {
               Page.ClientScript.RegisterStartupScript(this.GetType(), "alert" + UniqueID, "alert('" + str + "');", true);
           }
           catch (Exception ex)
           {
               Session["Exception"] = ex.ToString();
               Response.Redirect("~/CustomErrorPage.aspx?");
           }
       }


     
       
    }

 }


