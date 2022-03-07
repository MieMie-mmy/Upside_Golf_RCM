using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using CKSKS_BL;
using CKSKS_Common;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace Change_Delete_Status
{
    public class Program
    {
        static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        static string APIKeyForSportplaza = "SL280598_CHeLsFvJoav6flWe";
        static string APISecretForSportplaza = "SP280598_oYoh8AusSjOtJuzV";
        static int ShopIDForSportplaza = 8;

        static void Main(string[] args)
        {
            Console.Title = "Check Deleted Item Code";
            try
            {
                  DataTable dt = GetGetDeletedList();

                    foreach (DataRow dr in dt.Rows)
                    {
                        GetItemInformation(APISecretForSportplaza, APIKeyForSportplaza, ShopIDForSportplaza, dr["item_code"].ToString());
                    }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DataTable GetGetDeletedList()
        {
            try
            {

                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(ConnectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetDeletedItemCode", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;

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
        public static string GetItemInformation(string Secret, string Key, int shop_ID,string item_code)
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://api.rms.rakuten.co.jp/es/1.0/item/get?itemUrl={0}", item_code));
                //var webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:53915/XMLFile1.xml");
                webRequest.Method = "GET";
                //webRequest.ContentType = "text/xml";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(Secret + ":" + Key));
                webRequest.Headers.Add("Authorization", "ESA " + encoded);

                //結果をうけとってDOMオブジェクトにする
                WebResponse webRes = webRequest.GetResponse();

                XmlDocument resultXml = new XmlDocument();

                using (StreamReader reader = new StreamReader(webRes.GetResponseStream()))
                {
                    resultXml.Load(reader);
                }

                //結果XML中の[word]タグのリストを取得する
                XmlNodeList wordList = resultXml.GetElementsByTagName("message");
                ConsoleWriteLine_Tofile(wordList[0].InnerText);

                if (wordList[0].InnerText != "OK")
                {
                    return string.Format("NG:{0}", item_code);
                }

                #region item exists check
                //結果XML中の[word]タグのリストを取得する
                wordList = resultXml.GetElementsByTagName("code");
                ConsoleWriteLine_Tofile(wordList[0].InnerText);

                //Item exists
                if (wordList[0].InnerText == "N000")
                {

                  ChangeCtrlID(item_code, shop_ID);
                  ConsoleWriteLine_Tofile("item exists  " + item_code);
                }
                //Item not exists
                else if (wordList[0].InnerText == "C001")
                {
                    ConsoleWriteLine_Tofile("item not exists  " + item_code);
                }

                ConsoleWriteLine_Tofile(string.Format("Progress:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                return "done";
                #endregion
            }
            catch (WebException e)
            {
                throw e;
            }
            finally
            {
            }

        }

        public static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter("C:/TraceLog/Deleted_Item_Log.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        public static void ChangeCtrlID(string itemCode, int shopID)
        {

            SqlCommand cmd = new SqlCommand("SP_ChangeControlStatusForCheckingDeletedItemCode", GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("Item_Code", itemCode);
            cmd.Parameters.AddWithValue("ShopID", shopID);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
                 
        }
        private static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                return connection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
