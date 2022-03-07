using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace GenerateYahooRefreshToken
{
    public class Program
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static void Main(string[] args)
        {
            try
            {
                string url = "https://auth.login.yahoo.co.jp/yconnect/v1/authorization?response_type=code&client_id=dj0zaiZpPUtHcHZEcmhHTWZXbiZzPWNvbnN1bWVyc2VjcmV0Jng9MjU-&redirect_uri=http://store.shopping.yahoo.co.jp/paintandtool/&state=xyz";
                string code = RedirectPath(url);
                //string code = "kabc4mrb";
                if (!String.IsNullOrWhiteSpace(code))
                {
                    string result = GetNewRefreshToken(code);
                    if (result == "success")
                    {
                        Console.Write("Refresh Token Generate Successfully!!!");
                    }
                    else
                    {
                        Console.Write("Refresh Token Generate Error!!!" + result);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void LoginToYahooMallSetting()
        {
            try
            {
                string url = "https://auth.login.yahoo.co.jp/yconnect/v1/authorization?response_type=code&client_id=dj0zaiZpPUtHcHZEcmhHTWZXbiZzPWNvbnN1bWVyc2VjcmV0Jng9MjU-&redirect_uri=http://store.shopping.yahoo.co.jp/paintandtool/&state=xyz";
                string code = RedirectPath(url);
                if (!String.IsNullOrWhiteSpace(code))
                {
                    string result = GetNewRefreshToken(code);
                    if (result == "success")
                    {
                        Console.Write("Refresh Token Generate Successfully!!!");
                    }
                    else
                    {
                        Console.Write("Refresh Token Generate Error!!!" + result);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string RedirectPath(string url)
        {
            try
            {
                string line = "";
                //FirefoxProfile profile = new FirefoxProfile();
                FirefoxOptions option = new FirefoxOptions();
                using (IWebDriver firefox = new FirefoxDriver(option))
                {
                    string username = "myanmarkojima2"; string password = "kojima0667831348";
                    firefox.Url = url;
                    string title = firefox.Title;
                    firefox.FindElement(By.Name("login")).SendKeys(username);
                    firefox.FindElement(By.Name("btnNext")).Submit();
                    Thread.Sleep(3000);
                    firefox.FindElement(By.Name("passwd")).SendKeys(password);
                    firefox.FindElement(By.Name("btnNext")).Submit();
                    Thread.Sleep(3000);
                    string redirecturl = firefox.Url;
                    if (redirecturl.Contains("code"))
                    {
                        int start = redirecturl.IndexOf("code");
                        int end = redirecturl.IndexOf("&state=xyz", start);
                        line = redirecturl.Substring(start, end - start);
                        if (line.Contains("code="))
                        {
                            line = line.Replace("code=", "");
                        }
                    }
                    firefox.Close();
                }
                return line;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetNewRefreshToken(string code)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                DataTable dt = GetAPIData();
                string AppID = dt.Rows[0]["APIKey"].ToString();
                string SecretKey = dt.Rows[0]["APISecret"].ToString();
                string RefreshToken = dt.Rows[0]["Refresh_Token"].ToString();
                var postData = HttpUtility.ParseQueryString(string.Empty);
                postData.Add(new NameValueCollection
                {
                    { "grant_type", "authorization_code" },
                    { "redirect_uri", "http://store.shopping.yahoo.co.jp/paintandtool/" },
                    {"code",code}
                });
                var webRequest = (HttpWebRequest)WebRequest.Create("https://auth.login.yahoo.co.jp/yconnect/v1/token");
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(AppID + ":" + SecretKey));
                webRequest.Headers.Add("Authorization", "Basic " + encoded);
                using (var s = webRequest.GetRequestStream())
                using (var sw = new StreamWriter(s))
                    sw.Write(postData.ToString());
                using (var webResponse = webRequest.GetResponse())
                {
                    var responseStream = webResponse.GetResponseStream();
                    using (var reader = new StreamReader(responseStream))
                    {
                        var response = reader.ReadToEnd();
                        var json = JObject.Parse(response);
                        string accessToken = "";
                        accessToken = json.Value<string>("access_token");
                        RefreshToken = json.Value<string>("refresh_token");
                        InsertRefreshTokenToShop(RefreshToken);
                    }
                }
                return "success";
            }
            catch (WebException e)
            {
                string error = "";
                if (e.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)e.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            error = reader.ReadToEnd();
                            //TODO: use JSON.net to parse this string and look at the error message
                            if (error.Contains("Error"))
                            {
                                XmlDocument xd = new XmlDocument();
                                xd.LoadXml(error);
                                error = xd.InnerText;
                            }
                        }
                    }
                }
                return error;
            }
        }

        public static DataTable GetAPIData()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Shop where Mall_ID =2", con);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertRefreshTokenToShop(string refreshtoken)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Update_Refresh_Token", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RefreshToken", refreshtoken);
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
