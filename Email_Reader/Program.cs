using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Email_Reader
{
    public class Program
    {
        public static string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static void Main(string[] args)
        {
            string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
            string userName = ConfigurationManager.AppSettings["UserName"].ToString();
            string password = ConfigurationManager.AppSettings["Password"].ToString();
            //int readCount = Convert.ToInt32(ConfigurationManager.AppSettings["ReadCount"].ToString());
            Pop3Client pop3Client;
            pop3Client = new Pop3Client();
            pop3Client.Connect(hostName, 110, false);
            pop3Client.Authenticate(userName, password);
            emailData ed = new emailData();
            int readCount = pop3Client.GetMessageCount();
            for (int i = 1; i <= 10; i++)
            {
                Message msg = pop3Client.GetMessage(i);
                ed.subject = msg.Headers.Subject;
                ed.host = msg.Headers.From.MailAddress.Host;
                ed.emailFrom = msg.Headers.From == null ? string.Empty : msg.Headers.From.ToString();
                ed.emailDate = msg.Headers.DateSent.AddHours(9);
                ed.body = msg.FindFirstPlainTextVersion().GetBodyAsText();
                ed.replyTo = msg.Headers.ReplyTo == null ? string.Empty : msg.Headers.ReplyTo.ToString();
                ed.displayName = msg.Headers.From.DisplayName;

                if (msg.Headers.From.MailAddress.Host.Equals("rakuten.co.jp"))        //added by nandar 19/01/2016
                {
                    if (!Object.ReferenceEquals(msg.Headers.ReplyTo, null))
                    {
                        string prepareReplyTo = msg.Headers.ReplyTo.ToString();
                        string[] ReplyToprepared = prepareReplyTo.Split('"');
                        string replyToMail = ReplyToprepared[1];
                        if (replyToMail.Contains(','))
                        {
                            string[] replyToMail1 = replyToMail.Split(',');
                            ed.userEmail = replyToMail1[0];
                        }
                        else
                        {
                            ed.userEmail = replyToMail;
                        }
                    }
                    else
                        ed.userEmail = " ";
                }
                //else if (msg.Headers.From.MailAddress.Host.Equals("ponparemall.com"))// added by ETZ
                //    ed.userEmail = msg.Headers.To[0] == null ? string.Empty : msg.Headers.To[0].ToString();
                else if (msg.Headers.From.MailAddress.Host.Equals("orskjm.co.jp"))      ///added by nandar 25/01/2016
                    ed.userEmail = msg.Headers.To[0] == null ? string.Empty : msg.Headers.To[0].ToString();
                else
                    ed.userEmail = "";   /////// added by nandar 19/01/2016

                Insert(ed);
                pop3Client.DeleteMessage(i);
            }
            //int j = 1;
            //while (j <= 10)
            //{
                
            //    j++;
            //}
            pop3Client.Disconnect();
        }

        static Boolean Insert(emailData ed)
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                string query = "Insert Into Email_Data(Subject,Email_Host,Email_From,Email_Body,Email_Date,User_Email,ReplyTo,DisplayName,Updated_Date) " + Environment.NewLine +
                                     "Values(N'" + ed.subject + "',N'" + ed.host + "',N'" + ed.emailFrom + "',N'" + ed.body + "','" + ed.emailDate + "',N'" + ed.userEmail + "',N'" + ed.replyTo + "',N'" + ed.displayName + "',getDate())";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            { return false; }
        }
    }

    class emailData
    {
        public string subject { get; set; }
        public string host { get; set; }
        public string emailFrom { get; set; }
        public DateTime emailDate { get; set; }
        public string body { get; set; }
        public string replyTo { get; set; }
        public string displayName { get; set; }
        public string userEmail { get; set; }
    }
}
