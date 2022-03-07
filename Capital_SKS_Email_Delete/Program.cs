using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace Capital_SKS_Email_Delete
{
    class Program
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static DataTable dtError = new DataTable();
        public static string errLogPath = @"C:\Inventory Update\Email\Error_Logs\";
        public static string attatchmentPath = @"C:\Inventory Update\Email\Attatchment\";
        private static int errorIndex = 0;
        static void Main(string[] args)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string userName = ConfigurationManager.AppSettings["UserName"].ToString();
                string password = ConfigurationManager.AppSettings["Password"].ToString();
                int maxDelCount = Convert.ToInt32(ConfigurationManager.AppSettings["DelCount"].ToString());
                int startIndex = Convert.ToInt32(ConfigurationManager.AppSettings["startIndex"].ToString());

                Pop3Client pop3Client;
                pop3Client = new Pop3Client();
                pop3Client.Connect(hostName, 110, false);
                pop3Client.Authenticate(userName, password);

                int count = pop3Client.GetMessageCount();
                Message message = pop3Client.GetMessage(count);
                DateTime lastdate = message.Headers.DateSent;

                int i = 8, delcount = 0;
                while (startIndex <= maxDelCount)//delete count
                {
                    try
                    {
                        message = pop3Client.GetMessage(startIndex);
                        DateTime date1 = message.Headers.DateSent;

                        if ((lastdate - date1).TotalDays > 100)
                        {
                            //mark as delete
                            pop3Client.DeleteMessage(startIndex);
                            delcount++;
                        }

                        startIndex++;
                    }
                    catch (Exception errMessage)
                    {
                        String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        SqlConnection con = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", -1);
                        cmd.Parameters.AddWithValue("@ErrorDetail", "Email Delete" + errMessage.ToString());
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }
                //commit delete
                pop3Client.Disconnect();
                SaveDeletedEmailsCount(delcount);
            }
            catch (Exception errMessage)
            {

                int test = errorIndex;
                SendErrMessage(errMessage.Message);
                return;
            }

        }

        private static void SaveDeletedEmailsCount(int delcount)
        {
            if (delcount > 0)
            {
                int TotalCount = 0;
                DataTable dt = GetDeletedEmailsCount();
                if (dt.Rows.Count > 0)
                {
                    TotalCount = int.Parse(dt.Rows[0]["Total_Count"].ToString()) + delcount;
                }
                else
                {
                    TotalCount = delcount;
                }
                string date = "'" + DateTime.Now.ToString() + "'";
                string sql = string.Format("Insert Into Email_DeletedIndex (Deleted_Email_Count,Total_Count,Date) Values ({0},{1},{2})", delcount, TotalCount, date);
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private static DataTable GetDeletedEmailsCount()
        {
            string sql = "SELECT * FROM  Email_DeletedIndex WHERE ID=(SELECT max(ID) FROM Email_DeletedIndex)";
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da.SelectCommand = cmd;
            da.SelectCommand.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            da.SelectCommand.Connection.Open();
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            return dt;
        }
        public static void SendErrMessage(string errMessage)
        {
            #region Send Email
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 100000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("capital.zaw@gmail.com", "zawmyolwin123");

            MailMessage mm = new MailMessage("capital.zaw@gmail.com", "capital.zaw@gmail.com", "Error Message (Email_Reader)", errMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(attatchmentPath + "Attatchment.csv");
            mm.Attachments.Add(attachment);
            client.Send(mm);
            #endregion
            #region Create Log File
            if (File.Exists(errLogPath + "Delete_Email(log).txt"))
            {
                File.Delete(errLogPath + "Delete_Email(log).txt");
            }

            if (dtError.Rows.Count > 0)
            {
                for (int i = 0; i < dtError.Rows.Count; i++)
                {
                    //string errorMessage = "From :" + dtError.Rows[i]["EmailFrom"] + Environment.NewLine + "Subject :" + dtError.Rows[i]["EmailSubject"] + Environment.NewLine +
                    //    "Date :" + dtError.Rows[i]["EmailDate"] + Environment.NewLine + errMessage + Environment.NewLine + "--------------------" + Environment.NewLine;
                    File.AppendAllText(errLogPath + "Delete_Email(log).txt", errMessage);
                }
            }
            #endregion
        }
    }
}
