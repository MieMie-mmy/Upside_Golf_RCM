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

namespace EmailReaderFromDB
{
    class Program
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static string connectionString1 = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        static int readCount = Convert.ToInt32(ConfigurationManager.AppSettings["readCount"]);
        static int cur_count = 0;
        static int maxIndex = 0;
        static int count = 0;
        static string deleteID = "";
        static int errorID = 0;
        public static string[] errorEmailInfo = new string[6];
        public static DataTable dtError = new DataTable();

        static void Main(string[] args)
        {
            DataTable dt = getMailData();
            RetrieveInfo(dt, readCount);
        }

        static DataTable getMailData()
        {

            DataTable dt = new DataTable();
            SqlConnection sqlcon = new SqlConnection(connectionString1);
            string query = "Select Top(" + readCount + ") * From Email_Data Where IsError = 0";
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            sqlcon.Open();
            da.Fill(dt);
            sqlcon.Close();
            return dt;
        }

        private static void RetrieveInfo(DataTable dt, int counter)
        {
            try
            {
                String CheckRakuTan = "【楽天市場】注文内容ご確認（自動配信メール）";
                String CheckRakuTanFrom = "order@rakuten.co.jp";
                //String CheckRakuTanPhone = "【楽天】注文内容ご確認(携帯)";
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
                    cur_count = i + 1;
                    //GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["Email_Date"].ToString(), dt.Rows[i]["Email_From"].ToString(), dt.Rows[i]["ID"].ToString(), dt.Rows[i]["StoreName"].ToString(), dt.Rows[i]["OrderNo"].ToString());
                    DateTime emailDate = DateTime.Parse(dt.Rows[i]["Email_Date"].ToString());
                    string emailBody = dt.Rows[i]["Email_Body"].ToString();
                    int id = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                    string userEmail = dt.Rows[i]["User_Email"].ToString();
                    string subject = dt.Rows[i]["Subject"].ToString();

                    errorID = id; // get id to change error flag when error occurs

                    #region Rakuten
                    if (String.Equals(dt.Rows[i]["Email_From"].ToString(), CheckRakuTanFrom) && String.Equals(dt.Rows[i]["Subject"].ToString(), CheckRakuTan))
                    {
                        DataTable dtEmailInfo = GetDataFromEmail("rakuten", CheckRakuTan, emailDate, emailBody, id, userEmail);
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
                    //else if (String.Equals(dt.Rows[i]["From"].ToString(), CheckRakuTanFrom) && String.Equals(dt.Rows[i]["Subject"].ToString(), CheckRakuTanPhone))
                    //{
                    //    GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString());
                    //    DateTime dateSent = DateTime.Parse(dt.Rows[i]["DateSent"].ToString());
                    //    DataTable dtEmailInfo = GetDataFromEmail("rakuten_phone", dt.Rows[i]["Subject"].ToString(), dateSent, dt.Rows[i]["MessageBody"].ToString(), int.Parse(dt.Rows[i]["MessageNumber"].ToString()), dt.Rows[i]["User_Email"].ToString());
                    //    if (dtEmailInfo.Rows.Count > 0)
                    //    {
                    //        if (dtEmailArchive == null)
                    //        {
                    //            dtEmailArchive = dtEmailInfo;
                    //        }
                    //        else
                    //        {
                    //            dtEmailArchive.Merge(dtEmailInfo);
                    //        }
                    //    }
                    //}
                    #endregion
                    #region Yahoo
                    else if (dt.Rows[i]["Subject"].ToString().Contains("でのご注文:"))
                    {
                        DataTable dtEmailInfo = GetDataFromEmail("yahoo", subject, emailDate, emailBody, id, userEmail);
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
                        DataTable dtEmailInfo = GetDataFromEmail("yahooNew", subject, emailDate, emailBody, id, userEmail);
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
                    else if (String.Equals(dt.Rows[i]["Email_From"].ToString(), CheckPonpareFrom) && String.Equals(dt.Rows[i]["Subject"].ToString(), CheckPonpareSubject))
                    {
                        DataTable dtEmailInfo = GetDataFromEmail("ponpare", subject, emailDate, emailBody, id, userEmail);
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
                    else if (dt.Rows[i]["Email_From"].ToString().Contains(CheckAmazonFrom) && dt.Rows[i]["Subject"].ToString().Contains(CheckAmazonSubject))
                    {
                        if (!String.IsNullOrEmpty(dt.Rows[i]["MessageBody"].ToString()))
                        {
                            DataTable dtEmailInfo = GetDataFromEmail("amazon", subject, emailDate, emailBody, id, userEmail);
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
                    else if (dt.Rows[i]["Email_From"].ToString().Contains(CheckRacket) && dt.Rows[i]["Subject"].ToString().Contains(CheckRacketSubject))
                    {
                        DataTable dtEmailInfo = GetDataFromEmail("racket", subject, emailDate, emailBody, id, userEmail);
                        if (dtEmailInfo.Rows.Count > 0)
                        {
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
                    else
                        deleteID = string.IsNullOrWhiteSpace(deleteID) ? dt.Rows[i]["ID"].ToString() : deleteID + "," + dt.Rows[i]["ID"].ToString();
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

                    DataColumn dc = new DataColumn("UpdatedDate", typeof(DateTime));
                    dc.DefaultValue = DateTime.Now;
                    dtEmailArchive.Columns.Add(dc);

                    dc = new DataColumn("IsUploaded", typeof(System.Int32));
                    dc.DefaultValue = 1;
                    dtEmailArchive.Columns.Add(dc);

                    result = Insert(dtEmailArchive, counter);
                }

                MoveReadEmail();
            }
            catch (Exception errMessage)
            {
                UpdateErrorFlag(errMessage.Message);
            }
        }

        private static void MoveReadEmail()
        {
            SqlConnection con = new SqlConnection(connectionString1);

            string query = "INSERT INTO Email_Data_Delete( Email_DataID, Subject, Email_Host, Email_From, Email_Body, Email_Date, User_Email, ReplyTo, DisplayName, Updated_Date)" + Environment.NewLine;
            query += "SELECT ID,Subject,Email_Host, Email_From, Email_Body, Email_Date, User_Email, ReplyTo, DisplayName, Updated_Date Subject FROM Email_Data" + Environment.NewLine;
            query += "WHERE ID IN (SELECT TOP " + readCount + " ID FROM Email_Data)" + Environment.NewLine;
            query += "AND IsError = 0" + Environment.NewLine + Environment.NewLine;

            query += "DELETE FROM Email_Data" + Environment.NewLine;
            query += "WHERE ID IN (SELECT TOP " + readCount + " ID FROM Email_Data)" + Environment.NewLine;
            query += "AND IsError = 0" + Environment.NewLine;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private static void UpdateErrorFlag(string message)
        {
            SqlConnection con = new SqlConnection(connectionString1);
            string query = "Update Email_Data Set IsError = 1, ErrMsg = N'" + message + "' WHERE ID = " + errorID;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
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
                    Boolean start = false;
                    using (StringReader fileReader = new StringReader(emailBody))
                    {
                        while ((line = fileReader.ReadLine()) != null)
                        {
                            String[] str = new String[30];

                            if (line.Contains("[送付先]"))
                            {
                                customer_name = line.Replace("[送付先]", "").Split('様')[0] + "様";
                                break;
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
                                    if (!String.IsNullOrWhiteSpace(sale_price))
                                        sale_price += "/";
                                    sale_price += "価格 " + str1[str1.Length - 1].Replace("円", "(円)"); // added by aam
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
                    Boolean start = false;
                    String[] str = new String[30];
                    using (StringReader fileReader = new StringReader(emailBody))
                    {
                        while ((line = fileReader.ReadLine()) != null)
                        {
                            if (line.Contains("[送付先]")) //added by aam
                            {
                                customer_name = line.Replace("[送付先]：", "");
                            }

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
                                            if (str[1].Contains("）』"))
                                            {
                                                str = Regex.Split(str[1], "）』");
                                            }
                                            if (str[1].Contains("】"))
                                            {
                                                str = Regex.Split(str[1], "】");
                                            }
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
                                            count = 0;
                                        }
                                    }
                                }
                                else if (line.Contains("】（"))
                                {
                                    if (!String.IsNullOrWhiteSpace(ItemCode))
                                        ItemCode += ",";

                                    str = Regex.Split(line, "】（");
                                    string tempItemCode = str[1].Remove(str[1].Length - 1, 1);
                                    ItemCode += tempItemCode;
                                    count = 0;
                                }
                                if (line.Contains("---"))
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
                                if (!String.IsNullOrWhiteSpace(sale_price))
                                    sale_price += "/";
                                sale_price += str[0].ToString();// added by aam

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
                    Boolean start = false;
                    String[] str = new String[30];
                    using (StringReader fileReader = new StringReader(emailBody))
                    {
                        while ((line = fileReader.ReadLine()) != null)
                        {
                            if (line.Contains("- - - - - - - - - -"))
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
                            if (line.Contains("価格"))
                            {
                                str = line.Split(':');
                                string str1 = str[1].Replace("￥", "");
                                if (!String.IsNullOrWhiteSpace(sale_price))
                                    sale_price += "/";
                                sale_price += str1.Trim();
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
                    dtRacket.Columns.Add("Customer_Email", typeof(string));     //added by nandar 25/01/2016
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
                                string code = str[1];
                                String outputString = Regex.Replace(code, @"[^a-z0-9\s-]", String.Empty);
                                dtRacket.Rows[j]["ItemCode"] = outputString;
                                //dtRacket.Rows[j]["ItemCode"] = str[str.Length - 1].Trim();
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
                                dtRacket.Rows[j]["Customer_Email"] = customer_email;            ///added by nandar 25/01/2016

                                j++;
                            }
                        }
                    }
                    return dtRacket;
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
                        //Array.Resize(ref arrCode, arrCode.Length - 1);
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
                    catch (Exception ex)
                    {
                        UpdateErrorFlag(ex.Message);
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

        public static string GetLast(string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
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
                                //GetError(dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["DateSent"].ToString(), dt.Rows[i]["From"].ToString(), dt.Rows[i]["MessageNumber"].ToString());
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

                                //cmd.ExecuteNonQuery();
                                SqlParameter parmOUT = new SqlParameter("@IsSendEmail", SqlDbType.Int);
                                parmOUT.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(parmOUT);
                                cmd.ExecuteNonQuery();

                                //success_count++;
                                int returnVALUE = (int)cmd.Parameters["@IsSendEmail"].Value;
                                if (returnVALUE == 1)
                                {
                                    if (dtCountGroup.Rows[i]["EmailFrom"].ToString() != "amazon")
                                    {
                                        SendEmail(dtCountGroup.Rows[i]["EmailFrom"].ToString(), dtCountGroup.Rows[i]["StoreName"].ToString(), code, dtCountGroup.Rows[i]["SizeName"].ToString(), dtCountGroup.Rows[i]["ColorName"].ToString(), dtCountGroup.Rows[i]["Quantity"].ToString(), dtCountGroup.Rows[i]["Customer_Email"].ToString(), dtCountGroup.Rows[i]["Customer_Name"].ToString(), dtCountGroup.Rows[i]["Sale_Price"].ToString(), dtCountGroup.Rows[i]["OrderNo"].ToString(), dtCountGroup.Rows[i]["EmailDate"].ToString(), color, size);
                                    }
                                }
                                // return returnVALUE;

                                DataTable dtQuantity = GetQuantityJisha(code, color, size);
                                if (dtQuantity.Rows.Count > 0)
                                {
                                    string MailInformation = "";
                                    MailInformation += "Item_Code:" + dtCountGroup.Rows[i]["ItemCode"].ToString() + "," + ", Remaining Quantity : " + dtQuantity.Rows[0]["Quantity"].ToString() + ", Remaining Jisha Quantity : " + dtQuantity.Rows[0]["Jisha_Quantity"].ToString() + "Order No:" + dtCountGroup.Rows[i]["OrderNo"].ToString() + "," + "Email Date:" + Convert.ToDateTime(dtCountGroup.Rows[i]["EmailDate"].ToString());
                                    ConsoleWriteLine_Tofile3("Quantity Information : " + MailInformation);
                                }
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
                                //cmdInsert.Parameters.Add("@IsImported", SqlDbType.Int, 50, "IsImported");
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
                                //CreateCSV(dtError);
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

        public static void SendEmail(string emailfrom, string Shop_Name, string Item_Code,
        string Size_Name, string Color_Name, string Num, string customer_email, string customer_name, string sale_price, string Order_No, string emaildate, string color, string size)
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
                    //customer_email = "";
                    msg.To.Add(customer_email);
                    // msg.CC.Add("357d0d77505b76c4fe93b66332ecdb2dsa@pc.fw.rakuten.ne.jp");
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
                    // msg.CC.Add("357d0d77505b76c4fe93b66332ecdb2dsa@pc.fw.rakuten.ne.jp");
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
                    //msg.CC.Add("order@capitalsports.jp");
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

                /*  #region Send Email // updated by ETZ for sks-394 2016/01/27
                if (emailfrom.Contains("yahoo"))
                {
                    MailMessage msg = new MailMessage();

                    msg.From = new MailAddress("office@capitalsports.jp");
                    msg.To.Add("order@capitalsports.jp");
                    msg.CC.Add("order@capitalsports.jp");
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
                    msg.To.Add(customer_email);
                    msg.CC.Add("357d0d77505b76c4fe93b66332ecdb2dsa@pc.fw.rakuten.ne.jp");
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
               
                #endregion*/

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
                //DataTable dtQuantityInfo = GetQuantityJisha(Item_Code,color,size);
                string MailInformation = "";
                MailInformation += "Item_Code:" + Item_Code + "," + "Customer_Name:" + customer_name + ", Order No:" + Order_No + ",Customer Email:" + customer_email + ",Email From: " + emailfrom + "," + "Email Date:" + emaildate + ",Send Date:" + System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                ConsoleWriteLine_Tofile2("Send Mail Information : " + MailInformation);

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
            catch (Exception ex)
            {
                throw ex;
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

        static DataTable GetQuantityJisha(string Item_Code, string Color_Code, string Size_Code)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                //SqlCommand cmd = new SqlCommand("SP_GetQuantityInfo",conn);
                // cmd.CommandType = CommandType.StoredProcedure;
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
            catch (Exception)
            {
                string colorcode = code.Substring(code.Length - 1, 1);
                string sizecode = code.Substring(code.Length - 2, 1);
                colorcode = GenerateZero(colorcode);
                sizecode = GenerateZero(sizecode);
                code = code.Substring(0, code.Length - 2);
                return code + sizecode + colorcode;
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

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "EmailReader_ConsoleWriteLine.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
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

        static void ConsoleWriteLine_Tofile2(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Send_Email_Information.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
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
