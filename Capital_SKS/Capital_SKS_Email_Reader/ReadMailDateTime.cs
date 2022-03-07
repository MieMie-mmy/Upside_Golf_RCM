using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActiveUp.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Collections;
using System.Net.Mail;
using System.Globalization;


namespace ORS_RCM_Email_Reader
{

   public  class ReadMailDateTime
    {
       public Imap4Client Client = new Imap4Client();
       public static string[] errorEmailInfo = new string[4];
       public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
       public static string attatchmentPath = @"C:\CapitalSKS_Temp\Inventory Update\Email\Attatchment\";
       public static DataTable dtError = new DataTable();
       public ReadMailDateTime(string hostName, int port, string userID, string password) 
       {
           DataTable dtMessages = new DataTable();
           dtMessages.Columns.Add("MessageNumber");
           dtMessages.Columns.Add("From");
           dtMessages.Columns.Add("Subject");
           dtMessages.Columns.Add("DateSent");
           dtMessages.Columns.Add("MessageBody");

           ReadEmail(hostName, port, false, userID, password);
           int counter = 0;int i=0;
         foreach (Message email in GetUnreadMails("inbox"))
         {
             i +=1;
             string emailBody = email.BodyText.Text;
            
             dtMessages.Rows[dtMessages.Rows.Count - 1]["MessageNumber"] =i;
             dtMessages.Rows[dtMessages.Rows.Count - 1]["Subject"] = email.Subject.ToString();
             dtMessages.Rows[dtMessages.Rows.Count - 1]["DateSent"] = email.Date.AddHours(9);
             dtMessages.Rows[dtMessages.Rows.Count - 1]["From"] = email.From.ToString();
             if (emailBody != null)
             {
                 dtMessages.Rows[dtMessages.Rows.Count - 1]["MessageBody"] = emailBody.TrimEnd();
             }
             counter++;
             //***** TESTING ONLY (Limits number of emails to read) *****
             if (counter > 20)
             {
                 break;
             }
         }
         RetrieveInfo(dtMessages, counter);
       }
       private static void RetrieveInfo(DataTable dt, int counter)
       {
           try
           {
               String CheckRakuTan = "【楽天市場】注文内容ご確認（自動配信メール）";
               String CheckRakuTanFrom = "order@rakuten.co.jp";
               String CheckRakuTanPhone = "【楽天】注文内容ご確認(携帯)";
               String CheckPonpareFrom = "order@ponparemall.com";
               //String CheckPonpareFrom = "order@capitalk-mm.com";
               String CheckPonpareSubject = "【ポンパレモール】ご注文内容の確認";
               String CheckAmazonFrom = "do-not-reply@amazon.co.jp";
               //String CheckAmazonFrom = "order@capitalk-mm.com";
               String CheckAmazonSubject = "商品を出荷してください";
               String CheckRacket = "order@racket.co.jp";
               //String CheckRacket = "order@capitalk-mm.com";
               String CheckRacketSubject = "ご注文ありがとうございます";
               DataTable dtEmailArchive = new DataTable();

               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   #region Rakuten
                   if (String.Equals(dt.Rows[i]["From"].ToString(), CheckRakuTanFrom) && String.Equals(dt.Rows[i]["Subject"].ToString(), CheckRakuTan))
                   {
                       GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString());
                       DateTime dateSent = DateTime.Parse(dt.Rows[i]["DateSent"].ToString());
                       DataTable dtEmailInfo = GetDataFromEmail("rakuten", CheckRakuTan, dateSent, dt.Rows[i]["MessageBody"].ToString(), int.Parse(dt.Rows[i]["MessageNumber"].ToString()));
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
                   #region Rakuten Phone
                   else if (String.Equals(dt.Rows[i]["From"].ToString(), CheckRakuTanFrom) && String.Equals(dt.Rows[i]["Subject"].ToString(), CheckRakuTanPhone))
                   {
                       GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString());
                       DateTime dateSent = DateTime.Parse(dt.Rows[i]["DateSent"].ToString());
                       DataTable dtEmailInfo = GetDataFromEmail("rakuten_phone", dt.Rows[i]["Subject"].ToString(), dateSent, dt.Rows[i]["MessageBody"].ToString(), int.Parse(dt.Rows[i]["MessageNumber"].ToString()));
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
                   else if (dt.Rows[i]["Subject"].ToString().Contains("でのご注文:"))
                   {
                       GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString());
                       DateTime dateSent = DateTime.Parse(dt.Rows[i]["DateSent"].ToString());
                       DataTable dtEmailInfo = GetDataFromEmail("yahoo", dt.Rows[i]["Subject"].ToString(), dateSent, dt.Rows[i]["MessageBody"].ToString(), int.Parse(dt.Rows[i]["MessageNumber"].ToString()));
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

                   else if (dt.Rows[i]["Subject"].ToString().Contains("【Yahoo!ショッピング】注文確認"))
                   {
                       GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString());
                       DateTime dateSent = DateTime.Parse(dt.Rows[i]["DateSent"].ToString());
                       DataTable dtEmailInfo = GetDataFromEmail("yahooNew", dt.Rows[i]["Subject"].ToString(), dateSent, dt.Rows[i]["MessageBody"].ToString(), int.Parse(dt.Rows[i]["MessageNumber"].ToString()));
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
                   #region Ponpare
                   else if (String.Equals(dt.Rows[i]["From"].ToString(), CheckPonpareFrom) && String.Equals(dt.Rows[i]["Subject"].ToString(), CheckPonpareSubject))
                   {
                       GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString());
                       DateTime dateSent = DateTime.Parse(dt.Rows[i]["DateSent"].ToString());
                       DataTable dtEmailInfo = GetDataFromEmail("ponpare", dt.Rows[i]["Subject"].ToString(), dateSent, dt.Rows[i]["MessageBody"].ToString(), int.Parse(dt.Rows[i]["MessageNumber"].ToString()));
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
                   #region Amazon
                   else if (dt.Rows[i]["From"].ToString().Contains(CheckAmazonFrom) && dt.Rows[i]["Subject"].ToString().Contains(CheckAmazonSubject))
                   {
                       GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString());
                       DateTime dateSent = DateTime.Parse(dt.Rows[i]["DateSent"].ToString());
                       if (!String.IsNullOrEmpty(dt.Rows[i]["MessageBody"].ToString()))
                       {
                           DataTable dtEmailInfo = GetDataFromEmail("amazon", dt.Rows[i]["Subject"].ToString(), dateSent, dt.Rows[i]["MessageBody"].ToString(), int.Parse(dt.Rows[i]["MessageNumber"].ToString()));
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
                   }
                   #endregion
                   #region Racket
                   else if (dt.Rows[i]["From"].ToString().Contains(CheckRacket) && dt.Rows[i]["Subject"].ToString().Contains(CheckRacketSubject))
                   {
                       GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString());
                       DateTime dateSent = DateTime.Parse(dt.Rows[i]["DateSent"].ToString());
                       DataTable dtEmailInfo = GetDataFromEmail("racket", dt.Rows[i]["Subject"].ToString(), dateSent, dt.Rows[i]["MessageBody"].ToString(), int.Parse(dt.Rows[i]["MessageNumber"].ToString()));
                       if (dtEmailInfo.Rows.Count > 0)
                       {
                           string subject = dtEmailInfo.Rows[0]["EmailSubject"].ToString();
                           string orderNo = Regex.Match(subject, @"\d+").Value;
                           dtEmailInfo.Columns.Add("OrderNo", typeof(String));
                           for (int j = 0; j < dtEmailInfo.Rows.Count; j++)
                           {
                               dtEmailInfo.Rows[j]["OrderNo"] = orderNo;
                           }
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
               }

               Boolean result = true;
               if (dtEmailArchive != null && dtEmailArchive.Rows.Count > 0)
               {
                   if (!ContainColumn("ItemName", dtEmailArchive))
                   {
                       dtEmailArchive.Columns.Add("ItemName", typeof(String));
                       //dtEmailArchive.Columns.Add("SizeName", typeof(String));
                       //dtEmailArchive.Columns.Add("ColorName", typeof(String));
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
           }
           catch (Exception errMessage)
           {
               throw errMessage;
           }

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
       public static Boolean Insert(DataTable dt, int counter)
       {
           dt.Columns.Add("CreatedDate", typeof(DateTime));
           foreach (DataRow dr in dt.Rows)
           {
               dr["CreatedDate"] = DateTime.Now;
           }
           using (SqlConnection connection = new SqlConnection(connectionString))
           {
               connection.Open();

               // Start a local transaction.
               SqlTransaction tran = connection.BeginTransaction(IsolationLevel.ReadUncommitted);

               try
               {
                   #region EmailOrder_Quantity_Insert
                   SqlDataAdapter da = new SqlDataAdapter();
                   SqlCommand cmdInsert = new SqlCommand("SP_Email_ItemOrder_Insert", connection);

                   cmdInsert.Transaction = tran;
                   cmdInsert.CommandType = CommandType.StoredProcedure;
                   cmdInsert.Parameters.Add("@Item_Code", SqlDbType.NVarChar, 50, "ItemCode");
                   cmdInsert.Parameters.Add("@Color_Name", SqlDbType.NVarChar, 200, "ColorName");
                   cmdInsert.Parameters.Add("@Size_Name", SqlDbType.NVarChar, 200, "SizeName");
                   cmdInsert.Parameters.Add("@Quantity", SqlDbType.Int, 50, "Quantity");
                   cmdInsert.Parameters.Add("@Order_No", SqlDbType.NVarChar, 50, "OrderNo");
                   cmdInsert.Parameters.Add("@Store_Name", SqlDbType.NVarChar, 50, "StoreName");
                   cmdInsert.Parameters.Add("@Email_Date", SqlDbType.DateTime, 50, "EmailDate");
                   cmdInsert.Parameters.Add("@Subject", SqlDbType.NVarChar, 200, "EmailSubject");
                   cmdInsert.Parameters.Add("@Email_From", SqlDbType.NVarChar, 100, "EmailFrom");
                   cmdInsert.Parameters.Add("@Remark", SqlDbType.NVarChar, 200, "Remark");
                   //cmdInsert.Parameters.Add("@IsImported", SqlDbType.Int, 50, "IsImported");
                   cmdInsert.Parameters.Add("@Created_Date", SqlDbType.DateTime, 50, "CreatedDate");

                   da.InsertCommand = cmdInsert;
                   da.Update(dt);
                   #endregion

                   #region Product_Quantity_Update
                   String code = String.Empty;
                   String color = String.Empty;
                   String size = String.Empty;
                   for (int i = 0; i < dt.Rows.Count; i++)
                   {
                       //GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString());
                       GetError(dt.Rows[i]["EmailSubject"].ToString(), dt.Rows[i]["EmailDate"].ToString(), dt.Rows[i]["EmailFrom"].ToString(), dt.Rows[i]["EmailIndex"].ToString());

                       if (dt.Rows[i]["StoreName"].ToString().Equals("racket"))
                       {
                           code = dt.Rows[i]["ItemCode"].ToString();
                       }
                       else
                       {
                           string fullcode = dt.Rows[i]["ItemCode"].ToString();
                           fullcode = fullcode.Replace("(", "");
                           fullcode = fullcode.Replace(")", "");
                           fullcode = fixedCode(fullcode);
                           if (!IsCode(fullcode))
                           {
                               color = dt.Rows[i]["ItemCode"].ToString().Substring(dt.Rows[i]["ItemCode"].ToString().Length - 4, 4);
                               size = dt.Rows[i]["ItemCode"].ToString().Substring(dt.Rows[i]["ItemCode"].ToString().Length - 8, 4);
                               code = dt.Rows[i]["ItemCode"].ToString().Remove(dt.Rows[i]["ItemCode"].ToString().Length - 8);
                           }
                           else
                           {
                               code = dt.Rows[i]["ItemCode"].ToString();
                           }
                       }


                       SqlCommand cmd = new SqlCommand("SP_Item_Update", connection);

                       cmd.Transaction = tran;
                       cmd.CommandType = CommandType.StoredProcedure;
                       cmd.Parameters.AddWithValue("@StoreName", dt.Rows[i]["StoreName"].ToString());
                       cmd.Parameters.AddWithValue("@Item_Code", code);
                       cmd.Parameters.AddWithValue("@Color", color);
                       cmd.Parameters.AddWithValue("@Size", size);
                       cmd.Parameters.AddWithValue("@Quantity", int.Parse(dt.Rows[i]["Quantity"].ToString()));
                       cmd.Parameters.AddWithValue("@Updated_Date", DateTime.Now);
                       cmd.Parameters.AddWithValue("@Order_Flag", 1);
                       cmd.Parameters.AddWithValue("@ColorName", dt.Rows[i]["ColorName"].ToString());
                       cmd.Parameters.AddWithValue("@SizeName", dt.Rows[i]["SizeName"].ToString());
                       cmd.ExecuteNonQuery();

                   }
                   #endregion

                   tran.Commit();
                   return true;
               }
               catch (Exception errMessage)
               {
                   tran.Rollback();
                   if (dtError != null)
                   {
                       int count = dtError.Rows.Count;
                       CreateCSV(dtError);
                   }
                   string errorMessage = errMessage.Message;
                   if (errorEmailInfo != null)
                   {
                       if (!string.IsNullOrEmpty(errorEmailInfo[0].ToString()))
                       {
                           errorMessage = String.Format("Subject : {0}  DateSent : {1}  SentFrom : {2}" + Environment.NewLine + "Error Message : {3}", errorEmailInfo[0].ToString(), errorEmailInfo[1].ToString(), errorEmailInfo[2].ToString(), errMessage.Message);
                       }
                   }
                   SendErrMessage(errorMessage); // Email error messages only (Quit reading emails).
                   return false;
               }
               finally
               {
                   connection.Close();
               }
           }
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

           if (File.Exists(attatchmentPath + "Attatchment.csv"))
           {
               System.Net.Mail.Attachment attachment;
               attachment = new System.Net.Mail.Attachment(attatchmentPath + "Attatchment.csv");
               mm.Attachments.Add(attachment);
           }

           client.Send(mm);
           #endregion
           #region Create Log File
           //if (File.Exists(errLogPath + "log.txt"))
           //{
           //    File.Delete(errLogPath + "log.txt");
           //}

           //if (dtError.Rows.Count > 0)
           //{
           //    for (int i = 0; i < dtError.Rows.Count; i++)
           //    {
           //        //string errorMessage = "From :" + dtError.Rows[i]["EmailFrom"] + Environment.NewLine + "Subject :" + dtError.Rows[i]["EmailSubject"] + Environment.NewLine +
           //        //    "Date :" + dtError.Rows[i]["EmailDate"] + Environment.NewLine + errMessage + Environment.NewLine + "--------------------" + Environment.NewLine;
           //        File.AppendAllText(errLogPath + "log.txt", errMessage);
           //    }
           //}
           #endregion
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
                   //CreateCSV(dt, writer);
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
       private static void GetError(string subject, string dateSent, string from, string messageNumber)
       {
           errorEmailInfo[0] = subject;
           errorEmailInfo[1] = dateSent;
           errorEmailInfo[2] = from;
           errorEmailInfo[3] = messageNumber;
       }

       public static DataTable GetDataFromEmail(string emailFrom, string emailSubject, DateTime emailDate, string emailBody, int EmailIndex)
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

               GetError(emailSubject, emailDate.ToString(), emailFrom, EmailIndex.ToString());
               if (emailFrom.Equals("rakuten"))
               {
                   #region Rakuten
                   String line;
                   int count = 0;
                   int j = 0;
                   Boolean start = false;
                   String[] str = new String[30];
                   using (StringReader fileReader = new StringReader(emailBody))
                   {
                       while ((line = fileReader.ReadLine()) != null)
                       {
                           if (line.Contains("ショップ名 ："))
                           {
                               str = line.Split('：');
                               storeName = str[1];
                           }
                           if (line.Contains("********"))
                               break;
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
                                   }
                               }
                               if (line.Contains("---------"))
                               {
                                   count = 1;
                               }
                           }
                           if (line.Contains("[商品]"))
                           {
                               count++;
                               start = true;
                           }
                           if (line.Contains("受注番号"))
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
                           if (line.Contains("サイズ") || line.Contains("厚さ") || line.Contains("ゲージ"))
                           {
                               str = line.Split(':');
                               if (!String.IsNullOrWhiteSpace(sizeName))
                               {
                                   sizeName += ",";
                               }
                               sizeName += str[str.Length - 1];
                           }
                           if (line.Contains("カラー"))
                           {
                               str = line.Split(':');
                               if (!String.IsNullOrWhiteSpace(colorName))
                               {
                                   colorName += ",";
                               }
                               colorName += str[str.Length - 1];
                           }
                           if (line.Contains("価格") && line.Contains("x"))
                           {
                               str = line.Split('x');
                               int index = str[1].IndexOf('(');
                               string str1 = str[1].Substring(0, index);
                               if (!String.IsNullOrWhiteSpace(Quantity))
                                   Quantity += ",";
                               Quantity += str1;

                               for (int k = 0; k < str.Length; k++)
                                   str[k] = null;
                           }
                       }
                   }

                   if ((storeName.Contains("ラケットプラザ")))
                   {
                       storeName = "ラケットプラザ楽天";
                   }
                   else if (storeName.Contains(" テニス・バドミントン"))
                   {
                       storeName = "ラックピース楽天";
                   }
                   else if (storeName.Contains("スポーツプラザ"))
                   {
                       storeName = "スポーツプラザ楽天";
                   }
                   else if (storeName.Contains("ベースボールプラザ"))
                   {
                       storeName = "ベースボールプラザ楽天";
                   }

                   #endregion
               }
               else if (emailFrom.Equals("rakuten_phone"))
               {
                   #region Rakuten Phone
                   String line;
                   int count = 0;
                   int j = 0;
                   Boolean start = false;
                   using (StringReader fileReader = new StringReader(emailBody))
                   {
                       while ((line = fileReader.ReadLine()) != null)
                       {
                           String[] str = new String[30];
                           if (line.Contains("受注番号"))
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
                           if (line.Contains("※上記の番号にﾘﾝｸが") || line.Contains("ｸｰﾎﾟﾝによる値引き分は､ﾎﾟｲﾝﾄ付与対象外となっております｡"))
                           {
                               line = fileReader.ReadLine();
                           }
                           if (line.Contains("[ｼｮｯﾌﾟ情報]"))
                           {
                               line = line.Replace("[ｼｮｯﾌﾟ情報]", "");
                               line = line.TrimStart();
                               str = line.Split(' ');
                               storeName = str[0];
                               break;
                           }
                           if (line.Contains("[商品]"))
                           {
                               count++;
                               start = true;
                           }
                           if (start)
                           {
                               if (line.Contains("×"))
                               {
                                   String[] str1 = line.Split('×');
                                   str1 = str1[str1.Length - 2].Split(')');
                                   str1 = str1[str1.Length - 2].Split('(');


                                   if (!String.IsNullOrWhiteSpace(ItemCode))
                                       ItemCode += ",";
                                   ItemCode += str1[str1.Length - 1].ToString();

                                   str1 = line.Split('×');

                                   str1 = str1[str1.Length - 1].Split(' ');


                                   if (!String.IsNullOrWhiteSpace(Quantity))
                                       Quantity += ",";
                                   Quantity += str1[0];
                               }

                               if (line.Contains("ｻｲｽﾞ"))
                               {
                                   String[] str1 = line.Split(':');
                                   if (!String.IsNullOrWhiteSpace(sizeName))
                                       sizeName += ",";
                                   sizeName += str1[str1.Length - 2].Replace(" ｶﾗｰ", "");
                               }

                               if (line.Contains("ｶﾗｰ"))
                               {
                                   String[] str1 = line.Split(':');
                                   if (!String.IsNullOrWhiteSpace(colorName))
                                       colorName += ",";
                                   str1 = str1[str1.Length - 1].Split(' ');
                                   colorName += str1[0];
                               }
                           }
                       }
                   }

                   if (storeName.Contains("ｽﾎﾟｰﾂﾌﾟﾗｻﾞ"))
                   {
                       storeName = "スポーツプラザ楽天";
                   }
                   else if (storeName.Contains("ﾍﾞｰｽﾎﾞｰﾙﾌﾟﾗｻﾞ"))
                   {
                       storeName = "ベースボールプラザ楽天";
                   }
                   else if (storeName.Contains("ﾃﾆｽ･ﾊﾞﾄﾞﾐﾝﾄﾝ") || storeName.Contains("Luckpiece"))
                   {
                       storeName = "ラックピース楽天";
                   }
                   else if (storeName.Contains("ﾗｹｯﾄﾌﾟﾗｻﾞ"))
                   {
                       storeName = "ラケットプラザ楽天";
                   }

                   #endregion
               }
               else if (emailFrom.Equals("yahoo"))
               {
                   #region Yahoo

                   String line;
                   int count = 0;
                   int j = 0;
                   String[] str = new String[20];
                   using (StringReader fileReader = new StringReader(emailBody))
                   {
                       while ((line = fileReader.ReadLine()) != null)
                       {
                           j = 0;
                           if (line.Contains("ご注文番号"))
                           {
                               str = line.Split('-');
                               OrderNo = str[1].ToString();
                               str = new String[20];
                           }
                           if (count == 1)
                           {
                               String[] data = line.Split(' ');
                               for (int i = 0; i < data.Length; i++)
                               {
                                   if (!String.IsNullOrWhiteSpace(data[i]))
                                   {
                                       str[j] = data[i];
                                       j++;
                                   }
                               }
                               for (int i = 0; i < str.Length; i++)
                               {
                                   if (i < str.Length - 3 && !String.IsNullOrWhiteSpace(str[i]) && !String.IsNullOrWhiteSpace(str[i + 1]) && !String.IsNullOrWhiteSpace(str[i + 2]))
                                   {
                                       if (Regex.IsMatch(str[i], @"\d"))
                                       {
                                           if (IsNumeric(str[i + 1]))
                                           {
                                               if (Regex.IsMatch(str[i + 2], @"\d") && str[i + 2].Contains("円"))
                                               {
                                                   if (!String.IsNullOrWhiteSpace(ItemCode))
                                                   {
                                                       ItemCode += ",";
                                                       sizeName += ",";
                                                       colorName += ",";
                                                   }

                                                   ItemCode += str[i];
                                                   sizeName += "";
                                                   colorName += "";

                                                   if (!String.IsNullOrWhiteSpace(Quantity))
                                                       Quantity += ",";
                                                   Quantity += str[i + 1];

                                                   if (!String.IsNullOrWhiteSpace(Price))
                                                       Price += ",";
                                                   Price += str[i + 2];

                                                   if (!String.IsNullOrWhiteSpace(ItemName))
                                                   {
                                                       ItemName += ",";
                                                   }
                                                   for (int l = 0; l < i; l++)
                                                   {
                                                       ItemName += str[l];
                                                   }

                                                   for (int k = 0; k < str.Length - 1; k++)
                                                   {
                                                       str[k] = null;
                                                   }
                                                   break;
                                               }
                                           }
                                       }
                                   }
                               }
                           }
                           if (line.Contains("-------------------------------------------"))
                               count++;
                       }
                   }
                   storeName = "Yahoo";
                   #endregion
               }
               else if (emailFrom.Equals("yahooNew"))
               {
                   emailFrom = "yahoo";
                   #region Yahoo

                   String line;
                   Boolean start = false;
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
                               String firstchar = line.Substring(0, 1);
                               if (firstchar.Equals("（"))
                               {
                                   line = fileReader.ReadLine();
                                   if (line.Contains("/"))
                                   {
                                       str = line.Split('/');
                                       if (!String.IsNullOrWhiteSpace(ItemCode))
                                           ItemCode += ",";
                                       ItemCode += str[1];
                                   }
                               }
                           }
                           if (line.Contains("商品情報"))
                           {
                               start = true;
                           }
                           if (line.Contains("サイズ") || line.Contains("厚さ"))
                           {
                               str = line.Split('：');
                               if (!String.IsNullOrWhiteSpace(sizeName))
                                   sizeName += ",";
                               sizeName += str[1];
                           }

                           if (line.Contains("カラー"))
                           {
                               str = line.Split('：');
                               if (!String.IsNullOrWhiteSpace(colorName))
                                   colorName += ",";
                               colorName += str[1];
                           }

                           if (line.Contains("円×"))
                           {
                               str = line.Split('＝');
                               str = str[0].Split('×');

                               if (!String.IsNullOrWhiteSpace(Quantity))
                                   Quantity += ",";
                               Quantity += str[1];
                           }
                           if (line.Contains("注文ID"))
                           {
                               str = line.Split('：');
                               storeName = str[1].Split('-')[0];
                               OrderNo = str[1].Split('-')[1];
                           }
                       }
                   }

                   if (storeName.Contains("racket"))
                   {
                       storeName = "ラケットプラザヤフー";
                   }
                   else if (storeName.Contains("honpo"))
                   {
                       storeName = "卓球本舗ヤフー";
                   }
                   else if (storeName.Contains("bb"))
                   {
                       storeName = "ベースボールプラザヤフー";
                   }
                   else if (storeName.Contains("luckpiece"))
                   {
                       storeName = "ラックピースヤフー";
                   }
                   else if (storeName.Contains("sportsplaza"))
                   {
                       storeName = "スポーツプラザヤフー";
                   }
                   #endregion
               }
               else if (emailFrom.Equals("ponpare"))
               {
                   #region Ponpare
                   String line;
                   int count = 0;
                   int j = 0;
                   Boolean start = false;
                   String[] str = new String[30];
                   using (StringReader fileReader = new StringReader(emailBody))
                   {
                       while ((line = fileReader.ReadLine()) != null)
                       {
                           if (line.Contains("********"))
                               break;
                           if (line.Contains("ショップ名："))
                           {
                               str = line.Split('：');
                               storeName = str[1];
                           }
                           if (start)
                           {
                               if (count == 1 && (line.Contains("]") || line.Contains("）（")))
                               {
                                   if (line.Contains("]"))
                                   {
                                       str = line.Split(']');
                                   }
                                   if (line.Contains("）（"))
                                   {
                                       str = Regex.Split(line, "）（");
                                   }
                                   if (!String.IsNullOrWhiteSpace(str[str.Length - 1]))
                                   {
                                       string tmpItemCode = string.Empty;
                                       if (!String.IsNullOrWhiteSpace(ItemCode))
                                           ItemCode += ",";
                                       if (str[1].Contains("|"))
                                       {
                                           str = str[1].Split('|');
                                           tmpItemCode = Regex.Replace(str[2], @"\s+", "");
                                       }
                                       else
                                       {
                                           tmpItemCode = Regex.Replace(str[1], @"\s+", "");
                                       }
                                       if (!String.IsNullOrWhiteSpace(tmpItemCode))
                                       {
                                           if (!line.Contains("）（"))
                                           {
                                               tmpItemCode = tmpItemCode.Remove(0, 1);
                                           }
                                           tmpItemCode = tmpItemCode.Remove(tmpItemCode.Length - 1, 1);
                                           ItemCode += tmpItemCode;
                                           start = false;
                                       }
                                   }
                               }
                               else if (line.Contains("（"))
                               {
                                   if (!String.IsNullOrWhiteSpace(ItemCode))
                                       ItemCode += ",";

                                   str = line.Split('（');
                                   string tempItemCode = str[1].Remove(str[1].Length - 1, 1);
                                   ItemCode += tempItemCode;
                                   start = false;
                               }

                               if (line.Contains("---------"))
                               {
                                   count = 1;
                               }
                           }
                           if (line.Contains("[注文番号]"))
                           {
                               str = line.Split('：');
                               OrderNo = str[1].Trim();
                           }
                           if (line.Contains("[商品]"))
                           {
                               count++;
                               start = true;
                           }
                           if (line.Contains("価格"))
                           {
                               str = line.Split('×');
                               int index = str[1].IndexOf('(');
                               string str1 = str[1].Substring(0, index);
                               if (!String.IsNullOrWhiteSpace(Quantity))
                                   Quantity += ",";
                               Quantity += str1;

                               for (int k = 0; k < str.Length; k++)
                                   str[k] = null;
                           }
                           if ((line.Contains("サイズ:") || line.Contains("ゲージ") || line.Contains("厚さ"))) //size && !line.Contains("～")
                           {
                               str = line.Split(':');
                               if (!String.IsNullOrWhiteSpace(ponpareSize))
                                   ponpareSize = string.Empty;
                               if (str[1].Contains("℃"))
                               {
                                   string[] arr = new string[2];
                                   ponpareSize = str[1].Substring(str[1].Length - 5);
                                   ponpareSize = ponpareSize.Remove(ponpareSize.Length - 1);
                               }
                               else if (line.Contains("～"))
                               {
                                   str = line.Split('～');
                                   ponpareSize = str[1].Split(new char[] { '(', ')' })[1];
                               }
                               else
                               {
                                   ponpareSize = str[1].Split(new char[] { '(', ')' })[1];
                               }
                               // ponpare provides size seperately
                               if (!String.IsNullOrWhiteSpace(ItemCode))
                                   ItemCode += ponpareSize;


                               //Get size Name
                               str = line.Split(':');
                               String code = str[1].GetLast(6);
                               str[1] = str[1].Replace(code, "");
                               if (!String.IsNullOrWhiteSpace(sizeName))
                               {
                                   sizeName += ",";
                               }
                               sizeName += str[1];
                           }

                           if (line.Contains("カラー:")) //color
                           {
                               str = line.Split(':');
                               if (!String.IsNullOrWhiteSpace(ponpareColor))
                                   ponpareColor = string.Empty;
                               if (str[1].Contains(")("))
                               {
                                   string color = string.Empty;
                                   color = str[1].GetLast(5);
                                   color = color.TrimEnd(')');
                                   if (!String.IsNullOrWhiteSpace(ItemCode))
                                       ItemCode += color;
                               }
                               else if (str[1].Contains("）("))
                               {
                                   str = str[1].Split('）');
                                   ponpareColor = str[1].Split(new char[] { '(', ')' })[1];
                                   ItemCode += ponpareColor;
                               }
                               else
                               {
                                   ponpareColor = GetLast(str[1], 5).Remove(GetLast(str[1], 5).Length - 1);

                                   //ponpareColor = str[1].Split(new char[] { '(', ')' })[1];
                                   // ponpare provides size seperately
                                   if (!String.IsNullOrWhiteSpace(ItemCode))
                                       ItemCode += ponpareColor;
                               }

                               //get color Name
                               str = line.Split(':');
                               String code = str[1].GetLast(6);
                               str[1] = str[1].Replace(code, "");
                               if (!String.IsNullOrWhiteSpace(colorName))
                               {
                                   colorName += ",";
                               }
                               colorName += str[1];


                           }
                       }
                   }

                   if (storeName.Contains("ラケットプラザ　ポンパレモール店"))
                   {
                       storeName = "ラケットプラザポンパレ";
                   }

                   #endregion
               }
               else if (emailFrom.Equals("amazon"))
               {
                   #region Amazon
                   String line;
                   int count = 0;
                   int j = 0;
                   Boolean start = false;
                   String[] str = new String[30];
                   using (StringReader fileReader = new StringReader(emailBody))
                   {
                       while ((line = fileReader.ReadLine()) != null)
                       {
                           if (line.Contains("********"))
                               break;
                           if (start)
                           {
                               if (count == 1 && line.Contains("SKU:"))
                               {
                                   str = line.Split(':');

                                   if (!String.IsNullOrWhiteSpace(str[str.Length - 1]))
                                   {
                                       if (!String.IsNullOrWhiteSpace(ItemCode))
                                       {
                                           ItemCode += ",";
                                           sizeName += ",";
                                           colorName += ",";
                                       }

                                       ItemCode += str[str.Length - 1].Trim();
                                       sizeName += "";
                                       colorName += "";
                                       count = 0;
                                   }
                               }
                               if (line.Contains("---------"))
                               {
                                   count = 1;
                               }
                           }
                           if (line.Contains("注文番号:"))
                           {
                               str = line.Split(':');
                               OrderNo = str[1].Trim();
                           }
                           if (line.Contains("商品:"))
                           {
                               count++;
                               start = true;
                           }
                           if (line.Contains("数量:"))
                           {
                               str = line.Split(':');
                               //int index = str[1].IndexOf('(');
                               //string str1 = str[1].Substring(0, index);
                               string str1 = str[1].ToString();
                               if (!String.IsNullOrWhiteSpace(Quantity))
                                   Quantity += ",";
                               Quantity += str1;

                               for (int k = 0; k < str.Length; k++)
                                   str[k] = null;
                           }
                       }
                   }
                   storeName = "Amazon";
                   #endregion
               }
               else if (emailFrom.Equals("racket"))
               {
                   #region Racket
                   // Racket returns it's own table
                   // because Racket contains additional fields
                   DataTable dtRacket = new DataTable();
                   dtRacket.Columns.Add("EmailFrom", typeof(String));
                   dtRacket.Columns.Add("EmailSubject", typeof(String));
                   dtRacket.Columns.Add("EmailDate", typeof(DateTime));
                   dtRacket.Columns.Add("StoreName", typeof(String));
                   dtRacket.Columns.Add("ItemCode", typeof(String));
                   dtRacket.Columns.Add("Quantity", typeof(String));
                   dtRacket.Columns.Add("ItemName", typeof(String));
                   dtRacket.Columns.Add("SizeName", typeof(String));
                   dtRacket.Columns.Add("ColorName", typeof(String));
                   dtRacket.Columns.Add("EmailIndex", typeof(int));
                   String line;
                   int j = 0;
                   String[] str = new String[30];
                   using (StringReader fileReader = new StringReader(emailBody))
                   {
                       while ((line = fileReader.ReadLine()) != null)
                       {
                           if (line.Contains("********"))
                               break;
                           if (line.Contains("【商品コード】"))
                           {
                               dtRacket.Rows.Add();
                               str = line.Split('】');
                               dtRacket.Rows[j]["ItemCode"] = str[str.Length - 1].Trim();
                           }
                           else if (line.Contains("【商品名】"))
                           {
                               str = line.Split('】');
                               dtRacket.Rows[j]["ItemName"] = str[str.Length - 1].Trim();
                           }
                           else if (line.Contains("cps: "))
                           {
                               str = line.Split(':');
                               String size = str[2].Replace("カラー", " ");
                               dtRacket.Rows[j]["SizeName"] = size.Trim();

                               int times = str[3].ToCharArray().Where(x => x == '(').Count();
                               String color = String.Empty;
                               if (times == 1)
                               {
                                   str = str[3].Split('(');
                                   color = str[0];
                               }
                               else if (times == 2)
                               {
                                   str = str[3].Split('(');
                                   color = str[0] + "(" + str[1];
                               }
                               dtRacket.Rows[j]["ColorName"] = color;
                           }
                           else if (line.Contains("【数量】"))
                           {
                               str = line.Split('】');
                               String quantity = str[1].Remove(str[1].Length - 2);
                               dtRacket.Rows[j]["Quantity"] = quantity;
                               dtRacket.Rows[j]["EmailFrom"] = emailFrom;
                               dtRacket.Rows[j]["EmailSubject"] = emailSubject;
                               dtRacket.Rows[j]["EmailDate"] = emailDate;
                               dtRacket.Rows[j]["StoreName"] = "ラケットプラザ";
                               dtRacket.Rows[j]["EmailIndex"] = EmailIndex;
                               j++;
                           }
                       }
                   }
                   return dtRacket;
                   #endregion
               }
               String[] arrCode = ItemCode.Split(',');
               String[] arrQuantity = Quantity.Split(',');
               String[] arrSizeName = sizeName.Split(',');
               String[] arrColorName = colorName.Split(',');
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

               for (int i = 0; i < arrCode.Length; i++)
               {
                   dt.Rows.Add(emailFrom, emailSubject, emailDate, storeName, arrCode[i], arrQuantity[i], EmailIndex, OrderNo, arrSizeName[i], arrColorName[i]);
               }

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
        public void ReadEmail(string mailServer, int port, bool ssl, string login, string password)
        {

            if (ssl)
                Client.ConnectSsl(mailServer, 143);
            else
                Client.Connect(mailServer, 143);
            Client.Login(login, password);
        }

        public IEnumerable<Message> GetAllMails(string mailBox)
        {
            return GetMails(mailBox, "ALL").Cast<Message>();
        }

        public IEnumerable<Message> GetUnreadMails(string mailBox)
        {
            return GetMails(mailBox, "UNSEEN").Cast<Message>();

        }

        private MessageCollection GetMails(string mailBox, string searchPhrase)
        {
          
                Mailbox mails = Client.SelectMailbox(mailBox);
                int totalcount = mails.MessageCount;
                MessageCollection messages = mails.SearchParse("UNSEEN SINCE 02-Sep-2015");//+ DateTime.Now.ToString("dd-MMM-yyyy")); 
                int mailcount = messages.Count;
                int index = totalcount - mailcount;
                Checkindex(totalcount, mailcount, index);
                InsertIndex(index,mailcount);
                return messages;
                
          
        }

        public void InsertIndex(int counter,int totalcount) 
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string date = "'" + DateTime.Now.ToString() + "'";
            string sql = string.Format("Insert Into Email_ReadIndex (Count,Total_Count,Date) Values ({0},{1},{2})", counter, totalcount,System.DateTime.Now);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandType = CommandType.Text;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }
        public static bool Checkindex(int total,int mail,int index) 
        {
            if (total == (mail + index))
            {

                return true;  
            }
            else
                return false;
        
        }
    }
}
