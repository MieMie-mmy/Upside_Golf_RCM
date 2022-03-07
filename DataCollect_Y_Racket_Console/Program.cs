/*
Created By              :PPK reference by Aye Aye Mon
Created Date          :01/06/2016
Updated By             :
Updated Date         :

Using Stored Procedures :SP_Exhibition_List_SelectItem_IDList_New
                                            SP_Exhibition_List_CollectData_New
                                            SP_Item_Master_SelectForTemplate
                                            SP_Log_Exhibition_Item_Insert
                                            SP_ChangeFlagByShop

Comment : 1.Select string of Item_ID From Exhibition_Item_Master table and Exhibition_Item_Shop table
                    2.Select data from respective tables.
                    3.Change Template
                    4.Insert into Log_Exhibition_Item table , Log_Exhibition_Select table and Log_Exhibition_Category table
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;
using System.IO;

namespace DataCollect_Y_Painttool_Console
{
    public class Program
    {
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Yahoo Racket Data Collect";
                ConsoleWriteLine_Tofile("Yahoo Racket Data Collect : " + DateTime.Now);

                string list = SelectExhibitionItemID();
                ConsoleWriteLine_Tofile("1.SelectExhibitionItemID : " + list);

                if (!string.IsNullOrWhiteSpace(list))
                {
                    DataTable dtItemMaster = GetItemData(list, 2);
                    ConsoleWriteLine_Tofile("2.GetItemData : " + DateTime.Now);

                    Export_CSV3 export = new Export_CSV3();
                    DataTable dt1 = export.ModifyTable(dtItemMaster, 2);
                    ConsoleWriteLine_Tofile("3.ChangeTemplate : " + DateTime.Now);

                    SaveLogExhibition(dt1, list, 2);
                    ConsoleWriteLine_Tofile("4.SaveLogExhibition : " + DateTime.Now);

                    ChangeFlag(list, 2);
                    ConsoleWriteLine_Tofile("5.ChangeFlag : " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "Data Collect Y_Racket Console" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        static string SelectExhibitionItemID()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_List_SelectItem_IDList_New", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", 2);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt.Rows[0]["Item_ID"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static DataTable GetItemData(string list, int shop_id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_List_CollectData_New", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ItemID", list);
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ChangeFlag(string list, int shop_ID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_ChangeFlagByShop", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@List", list);
                cmd.Parameters.AddWithValue("@Shop_ID", shop_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void SaveLogExhibition(DataTable dt, string list, int shop_id)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();
                    SqlConnection conn = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_Insert_Yahoo");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Connection = conn;
                    result = result.Replace("&#", "$CapitalSports$");
                    cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
                    cmd.Parameters.AddWithValue("@strString", list);
                    cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "DataCollectYahooRacket_ConsoleWriteLine2.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
    }
}