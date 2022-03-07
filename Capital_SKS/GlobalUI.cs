/* 
Created By              : Hein Latt Zaw
Created Date          : 18/06/2014
Updated By             :
Updated Date         :

 Tables using: 
    -
    -
    -
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Text;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Configuration;
using System.Data.OleDb;

namespace ORS_RCM
{
    public class GlobalUI
    {
        public static void MessageBox(string message)
        {
            // Cleans the message to allow single quotation marks
            string cleanMessage = message.Replace("'", "\\'");
            string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";

            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(GlobalUI), "alert", script);
            }
        }

        public static DataTable CSVToTable(string filePath)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(filePath,Encoding.GetEncoding(932)))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    //read column names
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        if (column == "PICK UP！\\即日出荷対象商品")
                        {
                            datecolumn.AllowDBNull = true;
                            csvData.Columns.Add("対象商品");
                        }
                        else
                        {
                            datecolumn.AllowDBNull = true;
                            csvData.Columns.Add(datecolumn);
                        }
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

        public static DataTable Import_To_Grid(string FilePath, string Extension)
        {
            string SheetName = "";
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, "Yes");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dtGridView = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            // string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            foreach (DataRow row in dtExcelSchema.Rows)   //updated by ETZ for discard Filter table
            {
                if (row["TABLE_NAME"].ToString() == "Sheet1$")
                {
                    SheetName = "Sheet1$";
                }
                else if (row["TABLE_NAME"].ToString() == "Monotaro$")
                {
                    SheetName = "Monotaro$";
                }
                else if (row["TABLE_NAME"].ToString() == "基礎情報登録$")
                {
                    SheetName = "基礎情報登録$";
                }
            }
            connExcel.Close();
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dtGridView);
            connExcel.Close();
            return dtGridView;
        }

        public static DataTable Remove_Doublecode(DataTable dt)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = dt.Columns[i].ToString().Replace("\"", "");
            }
            int columnCount = dt.Columns.Count;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                bool allNull = true;
                for (int j = 0; j < columnCount; j++)
                {
                    if (dt.Rows[i][j] != DBNull.Value)
                    {
                        allNull = false;
                    }
                }
                if (allNull)
                {
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();
            return dt;
        }

        internal static void MessageBox(string p, string p_2)
        {
            throw new NotImplementedException();
        }

        internal static void MessageBox()
        {
            throw new NotImplementedException();
        }

        //Encrypt Decrypt functions
        public static string EncryptPassword(string txtPassword)
        {
            byte[] passBytes = System.Text.Encoding.Unicode.GetBytes(txtPassword);
            string encryptPassword = Convert.ToBase64String(passBytes);
            return encryptPassword;
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            byte[] passByteData = Convert.FromBase64String(encryptedPassword);
            string originalPassword = System.Text.Encoding.Unicode.GetString(passByteData);
            return originalPassword;
        }
    }
}