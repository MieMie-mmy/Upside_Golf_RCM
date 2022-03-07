using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Transactions;
using Newtonsoft.Json.Linq;
using System.Net;
using ORS_RCM_BL;
using System.Reflection;

namespace ORS_RCM.WebForms.Import
{
    /// <summary>
    /// Summary description for Json_Import_Quantity_Adjust
    /// </summary>
    public  class Json_Import_Quantity_Adjust : IHttpHandler
    {
        Import_Item_BL import = new Import_Item_BL();


        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.QueryString["User"] != null && context.Request.QueryString["Password"] != null && context.Request.QueryString["Start"] != null && context.Request.QueryString["End"] != null)
                {
                    LogInBL LogBL = new LogInBL();
                    String loginId = context.Request.QueryString["User"].ToString();
                    String pass = context.Request.QueryString["Password"].ToString();
                    if (context.Request.QueryString["Start"].Contains("_") && context.Request.QueryString["End"].Contains("_"))
                    {
                        string sdate = context.Request.QueryString["Start"].Replace("_", " ");
                        string edate = context.Request.QueryString["End"].Replace("_", " ");
                        DateTime StartDate;
                        DateTime EndDate;
                        if ((DateTime.TryParse(sdate, out StartDate)) && (DateTime.TryParse(edate, out EndDate)))
                        {
                            StartDate = Convert.ToDateTime(sdate);
                            EndDate = Convert.ToDateTime(edate);
                            DataTable dt = LogBL.logincheck(loginId);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                int userID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                                context.Response.ContentType = "text/plain";
                                string response = String.Empty;
                                DataTable dtOrderItem = Order_Item(StartDate, EndDate);
                                if (dtOrderItem != null && dtOrderItem.Rows.Count > 0)
                                {
                                    import.QuantityAdjust(dtOrderItem);
                                    ConsoleWriteLine_Tofile(dtOrderItem);
                                    context.Response.Write("Quantity Adjust Successfull!!!");
                                }//if
                                else
                                    context.Response.Write("There is no Email Order between this datetime range!!!");
                            }//if
                            else
                            {
                                context.Response.Write("Please check user name and password!!!");
                            }
                        }//if
                        else
                        {
                            context.Response.Write("Invalid DateTime Format!!!");
                        }
                    }
                    else
                    {
                        context.Response.Write("Please check valid path!!!");
                    }
                }
            }

            catch (Exception ex)
            {
                string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                SqlConnection con = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "Quantity Adjust: " + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public DataTable Order_Item(DateTime Start, DateTime End)
        {
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                SqlConnection con = new SqlConnection(connection);
                SqlDataAdapter da = new SqlDataAdapter("SP_Select_Email_Order", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.SelectCommand.Connection.Open();
                da.SelectCommand.Parameters.AddWithValue("@StartDate",Start);
                da.SelectCommand.Parameters.AddWithValue("@EndDate", End);
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }

        static void ConsoleWriteLine_Tofile(DataTable dtQtyAdjust)
        {
            string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Quantity_AdjustConsoleToWriteLine.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            String date = DateTime.Now.ToString();
            Console.WriteLine("Import Date: " + date);
            string traceTxt=string.Empty;
            for (int i = 0; i < dtQtyAdjust.Rows.Count; i++)
            {
                traceTxt += "[Item_Code: " + dtQtyAdjust.Rows[i]["Item_Code"].ToString() + " Color Code: " + dtQtyAdjust.Rows[i]["Color_Code"].ToString() + " Size Code: " + dtQtyAdjust.Rows[i]["Size_Code"].ToString()+" Quantity: " + dtQtyAdjust.Rows[i]["quantity"].ToString() + "]";
            }
            Console.WriteLine(traceTxt+Environment.NewLine);
            sw.Close();
            sw.Dispose();
        }
    }
}