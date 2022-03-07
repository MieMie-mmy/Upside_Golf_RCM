/*
    Using table_
 * Email_ReadIndex
 * Email_DeletedIndex
 * Email_ItemOrder 
     
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Collections;
using System.Net.Mail;
using System.Globalization;
using System.Security.Authentication;

namespace ORS_RCM_Email_Reader
{
    public class Program
    {
        //@"C:\ConsoleWriteLineTofile\";
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static string attatchmentPath = @"C:\CapitalSKS_Temp\Inventory Update\Email\Attatchment\";
        public static string errLogPath = @"C:\CapitalSKS_Temp\Inventory Update\Email\Error_Logs\";
        public static DataTable dtError = new DataTable();
        public static string[] errorEmailInfo = new string[6];
        public static string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
        public static string userName = ConfigurationManager.AppSettings["UserName"].ToString();
        public static string password = ConfigurationManager.AppSettings["Password"].ToString();
        private static int port = 110;
       // private static string hostName = "mail.capitalk-mm.com";
       // private static int port = 110;
       // private static string userID = "order@capitalk-mm.com";
       // private static string userID = "order2@capitalk-mm.com";
       // private static string password = "oeui39@efad";
        static int cur_count = 0; //added by aam
        static int count;
        static int maxIndex;
        static int checkIndex;
        static void Main(string[] args)
        {
            try
            {
                ReadEmails(0);
            }

            catch (Exception errMessage)
            {
                string errorMessage = errMessage.Message;
                if (errorEmailInfo[0] != null)
                {
                    if (!string.IsNullOrEmpty(errorEmailInfo[0].ToString()))
                    {
                        errorMessage += String.Format("Subject : {0}  DateSent : {1}  SentFrom : {2}  MessageNumber : {3}  StoreName : {4}  OrderNo : {5} ", errorEmailInfo[0].ToString(), errorEmailInfo[1].ToString(), errorEmailInfo[2].ToString(), errorEmailInfo[3].ToString(), errorEmailInfo[4].ToString(), errorEmailInfo[5].ToString());
                    }
                }
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "Email Reader :" + errMessage.ToString() + " Reason :" + errorMessage.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                ReadEmails(checkIndex);
                
            }
        }


        private static void ReadEmails(int errorIndex)
        {
            Pop3Client pop3Client;
            pop3Client = new Pop3Client();
            pop3Client.Connect(hostName, port, false);
            pop3Client.Authenticate(userName, password);
            count = pop3Client.GetMessageCount();
            DataTable dtMessages = new DataTable();
            dtMessages.Columns.Add("MessageNumber");
            dtMessages.Columns.Add("From");
            dtMessages.Columns.Add("Subject");
            dtMessages.Columns.Add("DateSent");
            dtMessages.Columns.Add("MessageBody");
            dtMessages.Columns.Add("User_Email");
            dtMessages.Columns.Add("StoreName");
            dtMessages.Columns.Add("OrderNo");
            maxIndex = GetIndex();
            int counter = 0;
            for (int i = maxIndex; i <= count; i++)
            {
                checkIndex = i;
                if (i == errorIndex)
                {
                    counter++;
                }
                if (i != errorIndex)
                {
                    byte[] str = pop3Client.GetMessageAsBytes(i);//added by KTA for spam mail
                    string strs = Encoding.GetEncoding(50222).GetString(str).TrimEnd();//added by KTA for spam mail
                    if (ReadError(strs))
                    {
                        Message message = pop3Client.GetMessage(i);
                        byte[] body = message.MessagePart.Body;
                        dtMessages.Rows.Add();
                        dtMessages.Rows[dtMessages.Rows.Count - 1]["MessageNumber"] = i;
                        dtMessages.Rows[dtMessages.Rows.Count - 1]["Subject"] = message.Headers.Subject;
                        dtMessages.Rows[dtMessages.Rows.Count - 1]["DateSent"] = message.Headers.DateSent.AddHours(9);
                        dtMessages.Rows[dtMessages.Rows.Count - 1]["From"] = message.Headers.From;
                        if (message.Headers.From.MailAddress.Host.Equals("rakuten.co.jp"))        //added by nandar 19/01/2016
                        {
                            if (!Object.ReferenceEquals(message.Headers.ReplyTo, null))
                            {
                                string prepareReplyTo = message.Headers.ReplyTo.ToString();

                                string[] ReplyToprepared = prepareReplyTo.Split('"');
                                string replyToMail = ReplyToprepared[1];
                                if (replyToMail.Contains(','))
                                {
                                    string[] replyToMail1 = replyToMail.Split(',');
                                    dtMessages.Rows[dtMessages.Rows.Count - 1]["User_Email"] = replyToMail1[0];
                                }
                                else
                                {
                                    dtMessages.Rows[dtMessages.Rows.Count - 1]["User_Email"] = replyToMail;
                                }
                            }
                            else
                            {
                                dtMessages.Rows[dtMessages.Rows.Count - 1]["User_Email"] = " ";
                            }
                        }
                        else if (message.Headers.From.MailAddress.Host.Equals("orskjm.co.jp"))          //added by miemie for wowma email
                        {
                            dtMessages.Rows[dtMessages.Rows.Count - 1]["User_Email"] = message.Headers.To[0];
                        }
                        else
                        {
                            dtMessages.Rows[dtMessages.Rows.Count - 1]["User_Email"] = message.Headers.To[0];
                        }
                        if (body != null)
                        {
                            dtMessages.Rows[dtMessages.Rows.Count - 1]["MessageBody"] = Encoding.GetEncoding(50222).GetString(body).TrimEnd();
                            //by miemie for wowma
                            string msgbody = dtMessages.Rows[dtMessages.Rows.Count - 1]["MessageBody"].ToString();
                            string msgheader = dtMessages.Rows[dtMessages.Rows.Count - 1]["From"].ToString();
                            //  Encoding.GetEncoding(65001).GetString(str).TrimEnd();
                            if (msgheader.Contains("Yahoo!ショッピング"))    // for yahoo 
                            {
                                dtMessages.Rows[dtMessages.Rows.Count - 1]["MessageBody"] = Encoding.GetEncoding(65001).GetString(body).TrimEnd();
                            }
                            if ((msgbody.Contains("受注番号") == false && msgheader.Contains("office@racket.co.jp")) || (msgbody.Contains("wowma") == true && msgheader.Contains("office@capitalsports.jp")))
                            {
                                dtMessages.Rows[dtMessages.Rows.Count - 1]["MessageBody"] = Encoding.GetEncoding(65001).GetString(body).TrimEnd();
                            }
                        }
                        else
                        {
                            MessagePart Body = message.FindFirstPlainTextVersion();
                            if (Body != null)
                            {
                                dtMessages.Rows[dtMessages.Rows.Count - 1]["MessageBody"] = Body.GetBodyAsText();
                            }
                        }
                        counter++;
                    }//if condition
                    else
                        counter++;
                }
                //***** TESTING ONLY (Limits number of emails to read) *****
                if (counter > 21)
                {
                    break;
                }
            }
            dtError = dtMessages;
            RetrieveInfo(dtMessages, counter);
        }

        private static bool ReadError(string emailBody)
        {
            string line;
            Boolean start = false;
            String[] str = new String[30];
            int error = 0;
            using (StringReader fileReader = new StringReader(emailBody))
            {
                while ((line = fileReader.ReadLine()) != null)
                {
                    if (line.Contains("Subject"))
                    {
                        str = line.Split('：');
                        string sub = str[0];
                        if (sub.Contains("Want to be my new f#ckbuddy"))
                        {
                            error = 1;
                        }
                    }
                    if (line.Contains("charset"))
                    {
                        str = line.Split('：');
                        string sub = str[0];
                        if (sub.Contains("iso-8859-14"))
                        {
                            error = 1;
                        }
                    }
                }
            }
            if (error != 0)
                return false;
            else
                return true;
        }

        private static void RetrieveInfo(DataTable dt, int counter)
        {
            try
            {
                String CheckRakuTan = "【楽天市場】注文内容ご確認（自動配信メール）";
                String CheckRakuTanFrom = "order@rakuten.co.jp";
                String CheckWowmaSubject = " 購入者決定"; //wowma bymiemie
                String CheckWowma = "honsya37@orskjm.co.jp"; //wowma by miemie
                DataTable dtEmailArchive = new DataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cur_count = i + 1;
                    #region Rakuten
                    if (String.Equals(dt.Rows[i]["From"].ToString(), CheckRakuTanFrom) && String.Equals(dt.Rows[i]["Subject"].ToString(), CheckRakuTan))
                    {
                        GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString(), dt.Rows[i]["StoreName"].ToString(), dt.Rows[i]["OrderNo"].ToString());
                        DateTime dateSent = DateTime.Parse(dt.Rows[i]["DateSent"].ToString());
                        DataTable dtEmailInfo = GetDataFromEmail("rakuten", CheckRakuTan, dateSent, dt.Rows[i]["MessageBody"].ToString(), int.Parse(dt.Rows[i]["MessageNumber"].ToString()), dt.Rows[i]["User_Email"].ToString());
                        if (dtEmailInfo.Rows.Count > 0)
                        {
                            if (dtEmailArchive == null)
                            {
                                dtEmailArchive = dtEmailInfo;
                            }
                            else
                            {
                                dtEmailArchive.Merge(dtEmailInfo);
                            }
                        }
                    }
                    #endregion
                   
                    #region Yahoo
                    
                    else if (dt.Rows[i]["Subject"].ToString().Contains("【Yahoo!ショッピング】注文確認"))
                    {
                        GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString(), dt.Rows[i]["StoreName"].ToString(), dt.Rows[i]["OrderNo"].ToString());
                        DateTime dateSent = DateTime.Parse(dt.Rows[i]["DateSent"].ToString());
                        DataTable dtEmailInfo = GetDataFromEmail("yahoo", dt.Rows[i]["Subject"].ToString(), dateSent, dt.Rows[i]["MessageBody"].ToString(), int.Parse(dt.Rows[i]["MessageNumber"].ToString()), dt.Rows[i]["User_Email"].ToString());
                        if (dtEmailInfo.Rows.Count > 0)
                        {
                            if (dtEmailArchive == null)
                            {
                                dtEmailArchive = dtEmailInfo;
                            }
                            else
                            {
                                dtEmailArchive.Merge(dtEmailInfo);
                            }
                        }
                    }
                    #endregion

                    #region wowma 
                    //added by miemie
                    else if (dt.Rows[i]["From"].ToString().Contains(CheckWowma) && dt.Rows[i]["Subject"].ToString().Contains(CheckWowmaSubject))
                    {
                        GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString(), dt.Rows[i]["StoreName"].ToString(), dt.Rows[i]["OrderNo"].ToString());
                        DateTime dateSent = DateTime.Parse(dt.Rows[i]["DateSent"].ToString());
                        DataTable dtEamilInfo = GetDataFromEmail("Wowma", dt.Rows[i]["Subject"].ToString(), dateSent, dt.Rows[i]["MessageBody"].ToString(), int.Parse(dt.Rows[i]["MessageNumber"].ToString()), dt.Rows[i]["User_Email"].ToString());
                        if (dtEamilInfo.Rows.Count > 0)
                        {
                            string subject = dtEamilInfo.Rows[0]["EmailSubject"].ToString();

                            if (dtEmailArchive == null)
                            {
                                dtEmailArchive = dtEamilInfo;
                            }
                            else
                            {
                                dtEmailArchive.Merge(dtEamilInfo);
                            }
                        }
                    }
                    #endregion

                }

                Boolean result = true;
                if (dtEmailArchive != null && dtEmailArchive.Rows.Count > 0)
                {
                    if (!ContainColumn("ItemName", dtEmailArchive))
                    {
                        dtEmailArchive.Columns.Add("ItemName", typeof(String));

                    }
                    dtEmailArchive.Columns.Add("Remark", typeof(System.String));
                    dtEmailArchive.Columns.Add("UpdatedDate", typeof(System.DateTime));
                    dtEmailArchive.Columns.Add("IsUploaded", typeof(System.Int16));
                    if (dtEmailArchive.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtEmailArchive.Rows.Count; j++)
                        {
                            dtEmailArchive.Rows[j]["UpdatedDate"] = DateTime.Now;
                            dtEmailArchive.Rows[j]["IsUploaded"] = 1;
                        }
                    }
                    for (int i = 0; i < dtEmailArchive.Rows.Count; i++)
                    {
                    }
                    result = Insert(dtEmailArchive, counter);
                }

                #region Email_Index
                if (result)
                {
                    DataTable dtEmail = GetEmailsCount();
                    int emailCount = 0;
                    if (dtEmail.Rows.Count > 0)
                    {
                        int TotalCount = 0;
                        if (dtEmail.Rows[0]["Total_Count"].ToString() != "")
                        {
                            TotalCount = int.Parse(dtEmail.Rows[0]["Total_Count"].ToString());
                        }
                        emailCount = TotalCount + counter;
                    }
                    else
                    {
                        emailCount = counter;
                    }
                    SqlConnection connection = new SqlConnection(connectionString);
                    string date = "'" + DateTime.Now.ToString() + "'";
                    string sql = string.Format("Insert Into Email_ReadIndex (Count,Total_Count,Date) Values ({0},{1},{2})", counter, emailCount, date);
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandType = CommandType.Text;
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                #endregion

                #region Email_ReadIndex_Log
                string EmailReadRecord = "";
                string getdate = DateTime.Now.ToString();
                EmailReadRecord += "Read Count:" + counter + "," + "Max Index:" + maxIndex + "," + "Total_Email_Count:" + count + "," + "Date:" + getdate;
                ConsoleWriteLine_Tofile1("Email Record: " + EmailReadRecord);
                #endregion
            }
            catch (Exception errMessage)
            {
                //throw errMessage;
                string errorMessage = errMessage.Message;
                if (errorEmailInfo[0] != null)
                {
                    if (!string.IsNullOrEmpty(errorEmailInfo[0].ToString()))
                    {
                        errorMessage += String.Format("Subject : {0}  DateSent : {1}  SentFrom : {2}  MessageNumber : {3}  StoreName : {4}  OrderNo : {5} ", errorEmailInfo[0].ToString(), errorEmailInfo[1].ToString(), errorEmailInfo[2].ToString(), errorEmailInfo[3].ToString(), errorEmailInfo[4].ToString(), errorEmailInfo[5].ToString());
                    }
                }
                ConsoleWriteLine_Tofile(DateTime.Now + " " + errorMessage);
                #region readindex+1
                cur_count = cur_count + 1;
                DataTable dtEmail = GetEmailsCount();
                int emailCount = 0;
                if (dtEmail.Rows.Count > 0)
                {
                    int TotalCount = 0;
                    if (dtEmail.Rows[0]["Total_Count"].ToString() != "")
                    {
                        TotalCount = int.Parse(dtEmail.Rows[0]["Total_Count"].ToString());
                    }
                    emailCount = TotalCount + cur_count;
                }
                else
                {
                    emailCount = cur_count;
                }
                SqlConnection connection = new SqlConnection(connectionString);
                string date = "'" + DateTime.Now.ToString() + "'";
                string sql = string.Format("Insert Into Email_ReadIndex (Count,Total_Count,Date) Values ({0},{1},{2})", cur_count, emailCount, date);
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
                #endregion
            }
        }

        public static DataTable GetDataFromEmail(string emailFrom, string emailSubject, DateTime emailDate, string emailBody, int EmailIndex, string customer_email)
        {
            try
            {
                string ItemCode = string.Empty;
                string OrderNo = string.Empty;
                string Quantity = string.Empty;
                string Price = string.Empty;
                string ItemName = string.Empty;
                string ponpareColor = string.Empty;
                string ponpareSize = string.Empty;
                string storeName = string.Empty;
                string colorName = string.Empty;
                string sizeName = string.Empty;
                string customer_name = string.Empty;
                string sale_price = string.Empty;

                GetError(emailSubject, emailDate.ToString(), emailFrom, EmailIndex.ToString(), storeName, OrderNo);
                if (emailFrom.Equals("rakuten"))
                {
                    #region Rakuten
                    String line;
                    int count = 0;
                    int cCount = 0;
                    int sCount = 0;
                    Boolean start = false;
                    String[] str = new String[30];
                    using (StringReader fileReader = new StringReader(emailBody))
                    {
                        while ((line = fileReader.ReadLine()) != null)
                        {
                            if (line.Contains("[注文者]")) //added by aam
                            {
                                customer_name = line.Replace("[注文者]", "");
                            }
                            if (line.Contains("[ショップ名]"))
                            {
                                str = line.Split(']');
                                storeName = str[1].Trim();
                            }
                            if (line.Contains("******"))
                                start = false;
                            
                            if (start)
                            {
                                if (count == 1)
                                {
                                    str = line.Split('(');

                                    if (!String.IsNullOrWhiteSpace(str[str.Length - 1]))
                                    {
                                        if (!String.IsNullOrWhiteSpace(ItemCode))
                                            ItemCode += ",";
                                        ItemCode += str[str.Length - 1].Remove(str[str.Length - 1].Length - 1, 1);
                                        count = 0;
                                        cCount = 1;
                                        sCount = 1;
                                    }
                                }
                                if (line.Contains("---------"))
                                {
                                    count = 1;
                                    cCount = 0;
                                    sCount = 0;
                                }
                            }
                            if (line.Contains("[商品]"))
                            {
                                count++;
                                start = true;
                            }
                            if (line.Contains("[受注番号]") || line.Contains("[注文番号]"))
                            {
                                if (line.Contains("】 "))
                                {
                                    str = line.Split('】');
                                    OrderNo = str[1].Trim();
                                }
                                else if (line.Contains(']'))
                                {
                                    str = line.Split(']');
                                    OrderNo = str[1].Trim();
                                }

                            }

                            if (cCount == 1)
                            {
                                if (line.Contains("カラー:"))
                                {
                                    str = line.Split(':');
                                    if (!String.IsNullOrWhiteSpace(colorName))
                                    {
                                        colorName += ",";
                                    }
                                    colorName += str[str.Length - 1];
                                    cCount = 0;
                                }
                                else if (count == 0 && line.Contains("価格") && line.Contains("x"))
                                {
                                    if (!String.IsNullOrWhiteSpace(colorName))
                                    {
                                        colorName += ",";
                                    }
                                    colorName += "ー";
                                    cCount = 0;
                                }
                                sCount = 1;
                            }
                            
                            if (sCount == 1)
                            {
                                if (line.Contains("サイズ") || line.Contains("厚さ") || line.Contains("ゲージ"))
                                {
                                    str = line.Split(':');
                                    if (!String.IsNullOrWhiteSpace(sizeName))
                                    {
                                        sizeName += ",";
                                    }
                                    sizeName += str[str.Length - 1];
                                    sCount = 0;
                                }
                                else if (count == 0 && cCount == 0 && (line.Contains("価格") && line.Contains("x") || line.Contains("****")))
                                {
                                    if (!String.IsNullOrWhiteSpace(sizeName))
                                    {
                                        sizeName += ",";
                                    }
                                    sizeName += "ー";
                                    sCount = 0;
                                }
                            }
                            if (line.Contains("価格") && line.Contains("x"))
                            {
                                str = line.Split('x');
                                if (!String.IsNullOrWhiteSpace(sale_price))
                                    sale_price += "/";
                                sale_price += str[0].ToString(); //added by aam
                                int index = str[1].IndexOf('(');
                                string str1 = str[1].Substring(0, index);
                                if (!String.IsNullOrWhiteSpace(Quantity))
                                    Quantity += ",";
                                Quantity += str1;

                                for (int k = 0; k < str.Length; k++)
                                    str[k] = null;
                            }
                            //if (line.Contains("********"))
                            //    break;
                        }
                    }
                    if ((storeName.Contains("ペイントアンドツール")))
                    {
                        storeName = "ペイントアンドツール";
                    }
                    //else if (storeName.Contains(" テニス・バドミントン"))
                    //{
                    //    storeName = "ラックピース楽天";
                    //}
                    //else if (storeName.Contains("スポーツプラザ"))
                    //{
                    //    storeName = "スポーツプラザ楽天";
                    //}
                    //else if (storeName.Contains("ベースボールプラザ"))
                    //{
                    //    storeName = "ベースボールプラザ楽天";
                    //}
                    #endregion
                }
                else if (emailFrom.Equals("yahoo"))
                {
                    emailFrom = "yahoo";
                    #region Yahoo
                    String line;
                    Boolean start = false;
                    int cCount = 0;
                    int sCount = 0;
                    String[] str = new String[20];
                    using (StringReader fileReader = new StringReader(emailBody))
                    {
                        while ((line = fileReader.ReadLine()) != null)
                        {
                            if (start)
                            {
                                if (line.Contains("===================================================="))
                                    break;
                                if (line.Equals(""))
                                    break;
                                    line = line.Trim();
                                    if (!String.IsNullOrWhiteSpace(ItemCode))
                                    {
                                        ItemCode += ",";
                                    }
                                    ItemCode += line;
                                    start = false;
                                    cCount = 1;
                            }
                            if (line.Contains("（"))
                            {
                                start = true;
                                cCount = 0;
                                sCount = 0;

                            }
                            if (cCount == 1)
                            {
                                if (line.Contains("カラー"))
                                {
                                    str = line.Split('：');
                                    if (!String.IsNullOrWhiteSpace(colorName))
                                        colorName += ",";
                                    colorName += str[1];
                                    cCount = 0;
                                }
                                else if (line.Contains("円×"))
                                {
                                    if (!String.IsNullOrWhiteSpace(colorName))
                                        colorName += ",";
                                    colorName += "ー";
                                    cCount = 0;
                                }
                                sCount = 1;
                            }
                            if (sCount == 1)
                            {
                                if (line.Contains("サイズ") || line.Contains("厚さ"))
                                {
                                    str = line.Split('：');
                                    if (!String.IsNullOrWhiteSpace(sizeName))
                                        sizeName += ",";
                                    sizeName += str[1];
                                    sCount = 0;
                                }
                                else if (cCount == 0 && line.Contains("円×"))
                                {
                                    if (!String.IsNullOrWhiteSpace(sizeName))
                                        sizeName += ",";
                                    sizeName += "ー";
                                    sCount = 0;
                                }
                            }
                            if (line.Contains("円×"))
                            {
                                str = line.Split('＝');
                                str = str[0].Split('×');
                                if (!String.IsNullOrWhiteSpace(sale_price))
                                    sale_price += "/";
                                sale_price += "価格 " + str[0].ToString().Replace("円", "(円)"); // added by aam
                               
                                if (!String.IsNullOrWhiteSpace(Quantity))
                                    Quantity += ",";
                                Quantity += str[1];
                            }
                            if (line.Contains("注文ID"))
                            {
                                str = line.Split('：');
                                storeName = str[1].Split('-')[0];
                                OrderNo = str[1];
                            }
                        }
                    }
                    #endregion
                }
                else if (emailFrom.Equals("Wowma"))
                {
                    #region wowma
                    DataTable dtwowma = new DataTable();
                    dtwowma.Columns.Add("EmailFrom", typeof(String));
                    dtwowma.Columns.Add("EmailSubject", typeof(String));
                    dtwowma.Columns.Add("EmailDate", typeof(DateTime));
                    dtwowma.Columns.Add("StoreName", typeof(String));
                    dtwowma.Columns.Add("ItemCode", typeof(String));
                    dtwowma.Columns.Add("LotNo", typeof(String));
                    dtwowma.Columns.Add("OrderNo", typeof(String));
                    dtwowma.Columns.Add("Quantity", typeof(String));
                    dtwowma.Columns.Add("Item_Name", typeof(String));
                    dtwowma.Columns.Add("SizeName", typeof(String));
                    dtwowma.Columns.Add("Sale_Price", typeof(String));
                    dtwowma.Columns.Add("ColorName", typeof(String));
                    dtwowma.Columns.Add("EmailIndex", typeof(int));
                    dtwowma.Columns.Add("Customer_Email", typeof(string));
                    dtwowma.Columns.Add("Customer_Name", typeof(string));
                    String line; int count = 0;
                    int j = 0;
                    String[] str = new String[30];
                    using (StringReader fileReader = new StringReader(emailBody))
                    {
                        while ((line = fileReader.ReadLine()) != null)
                        {
                            if (line.Contains("ロットナンバー"))
                            {
                                if (count > 0)
                                {
                                    dtwowma.Rows.Add();
                                }
                                str = line.Split('：');
                                string log = str[1].Trim();

                                dtwowma.Rows[j]["LotNo"] = log;

                            }
                            else if (line.Contains("商品名"))
                            {
                                str = line.Split('：');
                                ItemName = str[1].Trim();
                                dtwowma.Rows[j]["Item_Name"] = ItemName;
                            }
                            else if (line.Contains("カラー:"))
                            {
                                str = line.Split(':');
                                colorName = str[1].Trim();
                                dtwowma.Rows[j]["ColorName"] = colorName;
                            }
                            else if (line.Contains("サイズ:"))
                            {
                                str = line.Split(':');
                                sizeName = str[1].Trim();
                                dtwowma.Rows[j]["SizeName"] = sizeName;
                            }
                            else if (line.Contains("販売単価"))
                            {
                                str = line.Split('：');
                                sale_price = str[1].Trim();
                                dtwowma.Rows[j]["Sale_Price"] = sale_price;
                            }
                            else if (line.Contains("取引ナンバ"))
                            {
                                dtwowma.Rows.Add();
                                str = line.Split('：');
                                OrderNo = str[1].Trim();
                                dtwowma.Rows[j]["OrderNo"] = OrderNo;
                            }
                            else if (line.Contains("管理ID："))  //  add  by ct  for  size code and color code split
                            {
                                str = line.Split('：');
                                string code = str[1].Trim();

                                var color = code.Substring(0, (int)(code.Length / 2));
                                var size = code.Substring((int)(code.Length / 2), (int)(code.Length / 2));

                                DataTable dtLotNo = GetItemCode(dtwowma.Rows[j]["LotNo"].ToString(), color, size);
                                string itemcode = dtLotNo.Rows[0]["Item_Code"].ToString();
                                itemcode = itemcode + size + color;
                                dtwowma.Rows[j]["ItemCode"] = itemcode;

                            }
                            else if (line.Contains("販売個数"))
                            {
                                str = line.Split('：');
                                Quantity = str[1].Trim();
                                //Quantity = str[1].Remove(str[1].Length - 2);
                                dtwowma.Rows[j]["Quantity"] = Quantity;
                                dtwowma.Rows[j]["EmailFrom"] = emailFrom;
                                dtwowma.Rows[j]["EmailSubject"] = emailSubject;
                                dtwowma.Rows[j]["EmailDate"] = emailDate;
                                dtwowma.Rows[j]["StoreName"] = "W PaintTool";
                                storeName = "W PaintTool";
                                dtwowma.Rows[j]["EmailIndex"] = EmailIndex;
                                dtwowma.Rows[j]["Customer_Email"] = customer_email;
                                dtwowma.Rows[j]["Customer_Name"] = "";
                                if (j > 0)
                                {
                                    dtwowma.Rows[j]["OrderNo"] = OrderNo;
                                }
                                //DataTable dtLotNo = GetItemCode(dtwowma.Rows[j]["LotNo"].ToString(), dtwowma.Rows[j]["ColorName"].ToString(), dtwowma.Rows[j]["SizeName"].ToString());
                                //string color = dtLotNo.Rows[0]["Color_Code"].ToString();
                                //string size = dtLotNo.Rows[0]["Size_Code"].ToString();
                                //string code = dtLotNo.Rows[0]["Item_Code"].ToString();
                                //code = code + size + color;
                                //dtwowma.Rows[j]["ItemCode"] = code;
                                j++;
                                count += 1;
                            }
                            else if (line.Contains("販売総額"))
                            {
                                break;
                            }

                        }
                    }
                    if (String.IsNullOrWhiteSpace(dtwowma.Rows[j]["ItemCode"].ToString()))
                    {
                        DataTable dtLotNo = GetItemCode(dtwowma.Rows[j]["LotNo"].ToString(),"－", "－");
                        string itemcode = dtLotNo.Rows[0]["Item_Code"].ToString();
                       // itemcode = itemcode + size + color;
                        dtwowma.Rows[j]["ItemCode"] = itemcode;
                    }


                    return dtwowma;
                    #endregion
                }
                //added by aam
                int difference = 0, currentValue = 0;
                String[] arrCode = ItemCode.Split(',');
                for (int y = 0; y < arrCode.Length; y++)
                {
                    if (arrCode[y].Contains("イケダ トモヒロ)"))
                    {
                        difference += 1;
                    }
                }
                String[] newArray = new String[arrCode.Length - difference];
                for (int i = 0; i < arrCode.Length; i++)
                {
                    if (!arrCode[i].Contains("イケダ トモヒロ)"))
                    {
                        newArray[currentValue] = arrCode[i];
                        currentValue += 1;
                    }
                }
                arrCode = newArray;
                String[] arrQuantity = Quantity.Split(',');
                String[] arrSizeName = sizeName.Split(',');
                String[] arrColorName = colorName.Split(',');
                String[] arrSalePrice = sale_price.Split('/');
                DataTable dt = new DataTable();
                dt.Columns.Add("EmailFrom", typeof(string));
                dt.Columns.Add("EmailSubject", typeof(string));
                dt.Columns.Add("EmailDate", typeof(DateTime));
                dt.Columns.Add("StoreName", typeof(string));
                dt.Columns.Add("ItemCode", typeof(string));
                dt.Columns.Add("Quantity", typeof(string));
                dt.Columns.Add("EmailIndex", typeof(int));
                dt.Columns.Add("OrderNo", typeof(string));
                dt.Columns.Add("SizeName", typeof(string));
                dt.Columns.Add("ColorName", typeof(string));
                dt.Columns.Add("Customer_Email", typeof(string));
                dt.Columns.Add("Customer_Name", typeof(string));
                dt.Columns.Add("Item_Name", typeof(string));
                dt.Columns.Add("Sale_Price", typeof(string));
                for (int i = 0; i < arrCode.Length; i++)
                {
                    try
                    {
                        dt.Rows.Add(emailFrom, emailSubject, emailDate, storeName, arrCode[i], arrQuantity[i], EmailIndex, OrderNo, arrSizeName[i], arrColorName[i], customer_email, customer_name, ItemName, arrSalePrice[i]);
                    }
                    catch (Exception)
                    {
                        return new DataTable();
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        static DataTable GetItemCode(string lotno, string colorname, string sizename)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlData = new SqlDataAdapter("SP_GetItemCodeFromLogNo", conn);
                sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlData.SelectCommand.CommandTimeout = 0;
                sqlData.SelectCommand.Parameters.AddWithValue("@lotno", lotno);
                sqlData.SelectCommand.Parameters.AddWithValue("@sizename", sizename);
                sqlData.SelectCommand.Parameters.AddWithValue("@colorname", colorname);

                sqlData.SelectCommand.Connection.Open();
                sqlData.Fill(dt);
                sqlData.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetLast(string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }

        public static Boolean Insert(DataTable dt, int counter)
        {
            int tempGroup = 0;
            int tempGroupCount = 0;
            dt.Columns.Add("CreatedDate", typeof(DateTime));
            foreach (DataRow dr in dt.Rows)
            {
                dr["CreatedDate"] = DateTime.Now;
            }
            dt.Columns.Add("CountGroup", typeof(int));          // CountGroupforTansactionByGroup
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["EmailIndex"].ToString() == tempGroup.ToString())
                {
                    dt.Rows[i]["CountGroup"] = tempGroupCount;
                    tempGroup = Convert.ToInt32(dt.Rows[i]["EmailIndex"]);
                }
                else
                {
                    tempGroupCount += 1;
                    dt.Rows[i]["CountGroup"] = tempGroupCount;
                    tempGroup = Convert.ToInt32(dt.Rows[i]["EmailIndex"]);
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                #region Product_Quantity_Update
                String code = String.Empty;
                String color = String.Empty;
                String size = String.Empty;
                DataTable dtCountGroup = null;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow[] dr = dt.Select("CountGroup=" + (j + 1));
                    if (dr.Count() > 0)
                    {
                        dtCountGroup = dt.Select("CountGroup=" + (j + 1)).CopyToDataTable();
                        connection.Open();
                        SqlTransaction tran = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
                        try
                        {
                            for (int i = 0; i < dtCountGroup.Rows.Count; i++)
                            {
                                GetError(dtCountGroup.Rows[i]["EmailSubject"].ToString(), dtCountGroup.Rows[i]["EmailDate"].ToString(), dtCountGroup.Rows[i]["EmailFrom"].ToString(), dtCountGroup.Rows[i]["EmailIndex"].ToString(), dtCountGroup.Rows[i]["StoreName"].ToString(), dtCountGroup.Rows[i]["OrderNo"].ToString());
                                if (dtCountGroup.Rows[i]["StoreName"].ToString().Equals("racket"))
                                {
                                    code = dtCountGroup.Rows[i]["ItemCode"].ToString();
                                }
                                else
                                {
                                    string fullcode = dtCountGroup.Rows[i]["ItemCode"].ToString();
                                    fullcode = fullcode.Replace("(", "");
                                    fullcode = fullcode.Replace(")", "");
                                    DataTable dtcode = CheckedItemCodeExist(fullcode);
                                    if (dtcode.Rows.Count > 0)
                                    {
                                        code = dtCountGroup.Rows[i]["ItemCode"].ToString();
                                        dtCountGroup.Rows[i]["ItemCode"] = code + "──";
                                    }
                                    else
                                    {
                                        fullcode = fixedCode(fullcode);
                                        if (!IsCode(fullcode))
                                        {
                                            color = dtCountGroup.Rows[i]["ItemCode"].ToString().Substring(dtCountGroup.Rows[i]["ItemCode"].ToString().Length - 4, 4);
                                            size = dtCountGroup.Rows[i]["ItemCode"].ToString().Substring(dtCountGroup.Rows[i]["ItemCode"].ToString().Length - 8, 4);
                                            code = dtCountGroup.Rows[i]["ItemCode"].ToString().Remove(dtCountGroup.Rows[i]["ItemCode"].ToString().Length - 8);
                                        }
                                        else
                                        {
                                            code = dtCountGroup.Rows[i]["ItemCode"].ToString();
                                        }
                                    }
                                }
                                SqlCommand cmd = new SqlCommand("SP_Item_Update", connection);
                                cmd.Transaction = tran;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@StoreName", dtCountGroup.Rows[i]["StoreName"].ToString());
                                cmd.Parameters.AddWithValue("@Item_Code", code);
                                cmd.Parameters.AddWithValue("@Color", color);
                                cmd.Parameters.AddWithValue("@Size", size);
                                cmd.Parameters.AddWithValue("@Quantity", int.Parse(dtCountGroup.Rows[i]["Quantity"].ToString()));
                                cmd.Parameters.AddWithValue("@Updated_Date", DateTime.Now);
                                cmd.Parameters.AddWithValue("@Order_Flag", 1);
                                cmd.Parameters.AddWithValue("@ColorName", dtCountGroup.Rows[i]["ColorName"].ToString());
                                cmd.Parameters.AddWithValue("@SizeName", dtCountGroup.Rows[i]["SizeName"].ToString());
                                cmd.ExecuteNonQuery();
                                #region EmailOrder_Quantity_Insert

                                SqlCommand cmdInsert = new SqlCommand("SP_Email_ItemOrder_Insert", connection);
                                cmdInsert.Transaction = tran;
                                cmdInsert.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;
                                cmdInsert.Parameters.AddWithValue("@Item_Code", dtCountGroup.Rows[i]["ItemCode"].ToString());
                                cmdInsert.Parameters.AddWithValue("@Color_Name", dtCountGroup.Rows[i]["ColorName"].ToString());
                                cmdInsert.Parameters.AddWithValue("@Size_Name", dtCountGroup.Rows[i]["SizeName"].ToString());
                                cmdInsert.Parameters.AddWithValue("@Quantity", int.Parse(dtCountGroup.Rows[i]["Quantity"].ToString()));
                                cmdInsert.Parameters.AddWithValue("@Order_No", dtCountGroup.Rows[i]["OrderNo"].ToString());
                                cmdInsert.Parameters.AddWithValue("@Store_Name", dtCountGroup.Rows[i]["StoreName"].ToString());
                                cmdInsert.Parameters.AddWithValue("@Email_Date", Convert.ToDateTime(dtCountGroup.Rows[i]["EmailDate"].ToString()));
                                cmdInsert.Parameters.AddWithValue("@Subject", dtCountGroup.Rows[i]["EmailSubject"].ToString());
                                cmdInsert.Parameters.AddWithValue("@Email_From", dtCountGroup.Rows[i]["EmailFrom"].ToString());
                                cmdInsert.Parameters.AddWithValue("@Remark", dtCountGroup.Rows[i]["Remark"].ToString());
                                cmdInsert.Parameters.AddWithValue("@Created_Date", Convert.ToDateTime(dtCountGroup.Rows[i]["CreatedDate"].ToString()));
                                cmdInsert.ExecuteNonQuery();
                                #endregion
                            }
                            tran.Commit();
                        }
                #endregion
                        catch (Exception errMessage)
                        {
                            tran.Rollback();
                            if (dtError != null)
                            {
                                int count = dtError.Rows.Count;
                            }
                            string errorMessage = errMessage.Message;
                            if (errorEmailInfo != null)
                            {
                                if (!string.IsNullOrEmpty(errorEmailInfo[0].ToString()))
                                {
                                    errorMessage += String.Format("Subject : {0}  DateSent : {1}  SentFrom : {2}  MessageNumber : {3}  StoreName : {4}  OrderNo : {5} ", errorEmailInfo[0].ToString(), errorEmailInfo[1].ToString(), errorEmailInfo[2].ToString(), errorEmailInfo[3].ToString(), errorEmailInfo[4].ToString(), errorEmailInfo[5].ToString());
                                }
                            }
                            SqlConnection con = new SqlConnection(connectionString);
                            SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@UserID", -1);
                            cmd.Parameters.AddWithValue("@ErrorDetail", "Email Reader:" + errMessage.ToString() + "Reason :" + errorMessage.ToString());
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
            return true;
        }

        public static String GenerateZero(String value)
        {
            string result = value;
            for (int i = 0; i < 4 - value.Length; i++)
            {
                result = "0" + result;
            }
            return result;
        }

        public static DataTable CheckedItemCodeExist(string code)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                string quary = "SP_ItemCode_Match_EmailItemCode";
                SqlDataAdapter sda = new SqlDataAdapter(quary, connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", code);
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
        public static String fixedCode(String code)
        {
            try
            {
                if (code.Length >= 10)
                {
                    String check = code.Substring(code.Length - 8, 8);
                    Boolean result = Regex.IsMatch(check, @"\d");
                    if (result)
                    {
                        check = code.Substring(code.Length - 4, 4);
                        Convert.ToInt32(check);
                        check = code.Substring(code.Length - 8, 4);
                        Convert.ToInt32(check);
                    }
                }
                return code;
            }
            catch (Exception e)
            {
                string colorcode = code.Substring(code.Length - 1, 1);
                string sizecode = code.Substring(code.Length - 2, 1);
                colorcode = GenerateZero(colorcode);
                sizecode = GenerateZero(sizecode);
                code = code.Substring(0, code.Length - 2);
                return code + sizecode + colorcode;
            }
        }

        public static Boolean IsNumeric(String str)
        {
            try
            {
                int i = Convert.ToInt32(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Boolean IsCode(String code)
        {
            if (code.Length >= 10)
            {
                String check = code.Substring(code.Length - 8, 8);
                Boolean result = Regex.IsMatch(check, @"\d");
                if (result)
                {
                    check = code.Substring(code.Length - 4, 4);
                    if (Convert.ToInt32(check) > 99)
                        return true;
                    check = code.Substring(code.Length - 8, 4);
                    if (Convert.ToInt32(check) > 99)
                        return true;
                    return false;
                }
                else return true;
            }
            else return true;
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
            client.Send(mm);
            #endregion
        }

        public static void SendEmail(string emailfrom, string Shop_Name, string Item_Code,string Size_Name, string Color_Name, string Num, string customer_email, string customer_name, string sale_price, string Order_No, string emaildate,string color,string size)
        {
            try
            {
                #region Send Email
                string Item_Name = GetItem_Name(Item_Code);
                if (string.IsNullOrWhiteSpace(customer_name))
                {
                    customer_name = "ご購入者様";
                }
                #region Body
                string errMessage = customer_name.Trim() + "　（" + Order_No + "）\n\n"

                + string.Format("この度は" + Shop_Name + "ご利用頂きまして誠に有難うございます。\n\n")

                + string.Format("お急ぎのところ誠に申し訳ございませんが、お客様のご注文いただきました\n")
                + string.Format("以下の「即日出荷対応」商品ですが、別のお客様がご購入後、在庫数がサイト上反映\n")
                + string.Format("される前にお客様にご注文いただきましたので、現在、当店の即日出荷可能在庫が無く\n")
                + string.Format("即日出荷のご対応が出来ない状況でございます。\n\n")

                + string.Format("サイトにも記載しておりますが、在庫数をサイトに反映する間に当店在庫の無い商品を\n")
                + string.Format("ご注文いただいた場合は先着順でのご注文処理となってしまいますので、\n")
                + string.Format("このようなご迷惑をおかけする状況となってしまいました。\n\n")

                + string.Format("ご注文頂きました商品につきましては、メーカーに在庫の有無や納期等の確認したうえで\n")
                + string.Format("状況が分かり次第改めてご連絡を差し上げます。\n")
                + string.Format("今一度お時間を頂戴しますことを重ねてお詫び申し上げます。\n\n")

                + string.Format("先着順でのご注文処理となってしまい、ご迷惑をおかけする状況となってしまいました。\n\n")

                + string.Format("お急ぎのなかで申し訳ございませんが、何卒よろしくお願いいたします。\n\n")

                + string.Format("ご注文頂きました欠品中の商品は下記の通りとなります。ご確認をお願い致します。\n\n")

                + string.Format("------------------------------------------------------------\n\n")

                + string.Format("商品番号:" + Item_Code + "\n")
                + string.Format("商品名:" + Item_Name + "\n")
                + string.Format("サイズ:" + Size_Name + "　カラー:" + Color_Name + "\n")
                + string.Format("個数 " + Num + "個\n")
                + string.Format(sale_price + "\n\n")

                + string.Format("------------------------------------------------------------\n\n")

                + string.Format("[このメールはお客様にすぐご連絡させて頂くため弊社システムより\n")
                + string.Format("自動でお送りしております。翌営業日に弊社スタッフより改めてご連絡\n")
                + string.Format("差し上げますので何卒よろしくお願いいたします。]\n\n\n")

                + string.Format("==============================================================\n")
                + string.Format("　■　" + Shop_Name + "店　■\n")
                + string.Format("==============================================================\n")
                + string.Format("  " + Shop_Name + "店 \n")
                + string.Format("  運営：株式会社キャピタルスポーツ   \n")
                + string.Format("　〒560-0022　大阪府豊中市北桜塚3-1-5\n")
                + string.Format("　━━━━━━━━━━━━　　 \n")
                + string.Format("  mail:office@caitalsports.jp\n")
                + string.Format("　━━━━━━━━━━━━　　 \n\n\n");
                #endregion


                #region Send Email     //updated by nandar 01/02/2016
                if (emailfrom.Contains("rakuten"))
                {
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("office@capitalsports.jp");
                    msg.To.Add(customer_email);
                    msg.CC.Add("order@capitalsports.jp");                                   // changed by ETZ for SKS-394
                    msg.Subject = "即日出荷対応商品在庫切れにつきまして（" + Shop_Name + "店）";
                    msg.Body = errMessage;
                    using (SmtpClient client = new SmtpClient())
                    {
                        client.EnableSsl = false;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("210943", "icxKA3cJJ4");
                        client.Host = "sub.fw.rakuten.ne.jp";
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(msg);
                    }
                }
                else if (emailfrom.Contains("ponpare") || emailfrom.Contains("racket"))     //changed by nandar 02022016
                {
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("office@capitalsports.jp");
                    msg.To.Add(customer_email);
                    msg.CC.Add("order@capitalsports.jp");                                   // changed by ETZ for SKS-394
                    msg.Subject = "即日出荷対応商品在庫切れにつきまして（" + Shop_Name + "店）";
                    msg.Body = errMessage;
                    using (SmtpClient client = new SmtpClient())
                    {
                        client.EnableSsl = false;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("office@capitalsports.jp", "capital0013");
                        client.Host = "mail.capitalsports.jp";
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(msg);
                    }
                }
                else
                {
                    MailMessage msg = new MailMessage();

                    msg.From = new MailAddress("office@capitalsports.jp");
                    msg.To.Add("order@capitalsports.jp");
                    msg.Subject = "即日出荷対応商品在庫切れにつきまして（" + Shop_Name + "店）";
                    msg.Body = errMessage;
                    using (SmtpClient client = new SmtpClient())
                    {
                        client.EnableSsl = false;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("office@capitalsports.jp", "capital0013");
                        client.Host = "mail.capitalsports.jp";
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(msg);
                    }
                }
                #endregion

                #region
                //if (emailfrom.Contains("rakuten") || emailfrom.Contains("racket"))
                //{

                //    SmtpClient client = new SmtpClient();
                //    client.Port = 587;
                //    //client.Host = "smtp.gmail.com";
                //    client.Host = "sub.fw.rakuten.ne.jp";
                //    client.EnableSsl = true;
                //    client.Timeout = 100000;
                //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    client.UseDefaultCredentials = true; // ***************** important line *******************
                //    //client.Credentials = new System.Net.NetworkCredential("capital.zaw@gmail.com", "zawmyolwin123");
                //    //MailMessage mm = new MailMessage("capital.zaw@gmail.com", "capital.zaw@gmail.com", "即日出荷対応商品在庫切れにつきまして（" + Shop_Name + "店）", errMessage);
                //    client.Credentials = new System.Net.NetworkCredential("210943", "icxKA3cJJ4"); //office@capitalsports.jp 210943
                //    //MailMessage mm = new MailMessage("office@capitalsports.jp", "f957e3def597612840a227178f957aces1@pc.fw.rakuten.ne.jp", "ご注文頂きました商品の在庫につきまして（" + Shop_Name + "店）", errMessage);
                //    MailMessage mm = new MailMessage("office@capitalsports.jp", "inaoka@racket.co.jp", "即日出荷対応商品在庫切れにつきまして（" + Shop_Name + "店）", errMessage);
                //    mm.CC.Add("order@capitalsports.jp");
                //    //mm.CC.Add("office@capitalk-mm.com");
                //    //mm.Bcc.Add("dreamfairy.91@gmail.com");
                //    mm.BodyEncoding = UTF8Encoding.UTF8;

                //    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                //    //if (File.Exists(attatchmentPath + "Attatchment.csv"))
                //    //{

                //    //    System.Net.Mail.Attachment attachment;
                //    //    attachment = new System.Net.Mail.Attachment(attatchmentPath + "Attatchment.csv");
                //    //    mm.Attachments.Add(attachment);
                //    //}

                //    client.Send(mm);
                //}       
                //else
                //{
                //    SmtpClient client = new SmtpClient();
                //    client.Port = 587;
                //    //client.Host = "smtp.gmail.com";
                //    client.Host = "mail.capitalsports.jp";
                //    client.EnableSsl = true;
                //    client.Timeout = 100000;
                //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    client.UseDefaultCredentials = true; // ***************** important line *******************
                //    //client.Credentials = new System.Net.NetworkCredential("capital.zaw@gmail.com", "zawmyolwin123");
                //    //MailMessage mm = new MailMessage("capital.zaw@gmail.com", "capital.zaw@gmail.com", "即日出荷対応商品在庫切れにつきまして（" + Shop_Name + "店）", errMessage);
                //    client.Credentials = new System.Net.NetworkCredential("office@capitalsports.jp", "capital0013"); //office@capitalsports.jp 210943
                //    //MailMessage mm = new MailMessage("office@capitalsports.jp", "f957e3def597612840a227178f957aces1@pc.fw.rakuten.ne.jp", "ご注文頂きました商品の在庫につきまして（" + Shop_Name + "店）", errMessage);
                //    MailMessage mm = new MailMessage("office@capitalsports.jp", "inaoka@racket.co.jp", "即日出荷対応商品在庫切れにつきまして（" + Shop_Name + "店）", errMessage);
                //    mm.CC.Add("order@capitalsports.jp");
                //    //mm.CC.Add("office@capitalk-mm.com");
                //    //mm.Bcc.Add("dreamfairy.91@gmail.com");
                //    mm.BodyEncoding = UTF8Encoding.UTF8;

                //    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                //    //if (File.Exists(attatchmentPath + "Attatchment.csv"))
                //    //{

                //    //    System.Net.Mail.Attachment attachment;
                //    //    attachment = new System.Net.Mail.Attachment(attatchmentPath + "Attatchment.csv");
                //    //    mm.Attachments.Add(attachment);
                //    //}

                //    client.Send(mm);
                //}

                #endregion
                #endregion

                #region ConsoleWriteLineTofile
                string MailInformation = "";
                MailInformation += "Item_Code:" + Item_Code + "," + "Customer_Name:" + customer_name + ", Order No:" + Order_No + ",Customer Email:" + customer_email + ",Email From: " + emailfrom + "," + "Email Date:" + emaildate + ",Send Date:" + System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                ConsoleWriteLine_Tofile2("Send Mail Information : " + MailInformation);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static DataTable GetQuantityJisha(string Item_Code, string Color_Code, string Size_Code)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlData = new SqlDataAdapter("SP_GetQuantityInfo", conn);
                sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlData.SelectCommand.CommandTimeout = 0;
                sqlData.SelectCommand.Parameters.AddWithValue("@ItemCode", Item_Code);
                sqlData.SelectCommand.Parameters.AddWithValue("@color", Color_Code);
                sqlData.SelectCommand.Parameters.AddWithValue("@size", Size_Code);
                sqlData.SelectCommand.Connection.Open();
                sqlData.Fill(dt);
                sqlData.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ConsoleWriteLine_Tofile3(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Qunatity_Information.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "EmailReader_ConsoleWriteLine.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        private static void CreateCSV(DataTable dt)
        {
            try
            {
                if (File.Exists(attatchmentPath + "Attatchment.csv"))
                {
                    File.Delete(attatchmentPath + "Attatchment.csv");
                }
                using (StreamWriter writer = new StreamWriter(attatchmentPath + "Attatchment.csv", false, Encoding.GetEncoding(932)))
                {
                    WriteDataTable(dt, writer, true);
                }
            }
            catch (Exception)
            {

            }
        }

        public static void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                List<string> headerValues = new List<string>();

                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerValues.Add(QuoteValue(column.ColumnName));
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

        private static string QuoteValue(string value)
        {
            return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
        }

        private static DataTable GetDeletedEmailsCount()
        {
            string sql = "SELECT Total_Count FROM Email_DeletedIndex WHERE ID=(SELECT max(ID) FROM Email_DeletedIndex)";
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

        private static DataTable GetEmailsCount()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM  Email_ReadIndex WHERE ID = (SELECT max(ID) FROM Email_ReadIndex)";
            SqlDataAdapter da = new SqlDataAdapter(sql, connectionString);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Connection.Open();
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            return dt;
        }

        private static string GetItem_Name(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT Item_Name FROM Item_Master WITH (NOLOCK) WHERE Item_Code=@Item_Code";
                SqlDataAdapter da = new SqlDataAdapter(sql, connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Item_Name"].ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int GetIndex()
        {
            DataTable dtDeletedEmailsCount = GetDeletedEmailsCount();
            DataTable dtEmailsCount = GetEmailsCount();
            int readCount = 0;
            int deletedCount = 0;
            if (dtEmailsCount.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(dtEmailsCount.Rows[0]["Total_Count"].ToString()))
                {
                    readCount = int.Parse(dtEmailsCount.Rows[0]["Total_Count"].ToString());
                }
            }
            if (dtDeletedEmailsCount.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(dtDeletedEmailsCount.Rows[0]["Total_Count"].ToString()))
                {
                    deletedCount = int.Parse(dtDeletedEmailsCount.Rows[0]["Total_Count"].ToString());
                }
            }
            int newIndex = readCount - deletedCount;
            return newIndex + 1;
        }

        public static void Error()
        {
            object m = null;
            string s = m.ToString();
        }

        static void ConsoleWriteLine_Tofile2(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Send_Email_Information.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        static void ConsoleWriteLine_Tofile1(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Email_Read_Record.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        private static bool ContainColumn(string columnName, DataTable table)
        {
            DataColumnCollection columns = table.Columns;

            if (columns.Contains(columnName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void GetError(string subject, string dateSent, string from, string messageNumber, string storeName, string orderNo)
        {
            errorEmailInfo[0] = subject;
            errorEmailInfo[1] = dateSent;
            errorEmailInfo[2] = from;
            errorEmailInfo[3] = messageNumber;
            errorEmailInfo[4] = storeName;
            errorEmailInfo[5] = orderNo;
        }
    }

    public static class StringExtension
    {
        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }
    }
}
