/*
 * Created By Inaoka
 * Created Date 2015/04/16
 * Updated By
 * Updated Date
 *
 * Description:
 * take a Yahoo Delivery records
 * update a yahoo Delivery astk-code,RecordUpdated fields
 * create a Yahoo Delivery records
 * upload a Yahoo Delivery records
 * 
 * 
 * take a Yahoo Point records
 * update a yahoo Point point-code,RecordUpdated fields
 * create a Yahoo Point records
 * upload a Yahoo Point records
 * 
 * 
 * use 
 */

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Net;


namespace Cls_YahooAPI
{


    public class YahooAPI
    {

        public string GetAccessToken(){
           return Properties.Settings.Default["AccessToken"].ToString ();
        }

        public string GetAppID()
        {
            return Properties.Settings.Default["AppID"].ToString();
        }

        public string GetSecretKey()
        {
            return Properties.Settings.Default["SecretKey"].ToString();
        }

        public string YahooAPIAuth()
        {
            try
            {
                string AppID = Properties.Settings.Default.AppID;
                string SecretKey = Properties.Settings.Default.SecretKey;
                //settings = settings.ReadXML("YahooAPIsettings.xml");

                string RefreshToken = Properties.Settings.Default.RefreshToken;

                var postData = HttpUtility.ParseQueryString(string.Empty);
                postData.Add(new NameValueCollection
                {
                    { "grant_type", "refresh_token" },
                    { "refresh_token", RefreshToken }
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
                    //                    if (responseStream == null)
                    //                        return Tuple.Create("", "");

                    using (var reader = new StreamReader(responseStream))
                    {
                        var response = reader.ReadToEnd();
                        var json = JObject.Parse(response);
                        string accessToken = "";
                        //                        string expires_in = "";

                        accessToken = json.Value<string>("access_token");
                        //HttpContext.Current.Session["accessToken"] = accessToken;

                        //settings = settings.ReadXML("YahooAPIsettings.xml");
                        //string refa = settings.RefreshToken;
                        //settings.AccessToken = accessToken;
                        //settings.WriteXML("YahooAPIsettings.xml", settings);
                        Properties.Settings.Default["AccessToken"] = accessToken;
                        Properties.Settings.Default.Save();

                        //expires_in = json.Value<string>("expires_in");
                        //HttpContext.Current.Session["expires_in"] = expires_in;

                        //                      return Tuple.Create(accessToken, expires_in);
                    }
                }
                return "success";
            }
            catch (WebException ex)
            {
                //System.Net.HttpWebResponse errres =
                //            (System.Net.HttpWebResponse)ex.Response;

                //object objResponse = ex.Response as HttpWebResponse;
                return "ng";
                //           return Tuple.Create("error", e.ToString());

            }
        }

        static Dictionary<string, string> yahooItemRefInfo = new Dictionary<string, string>(){
        {"Status", "NG"},    
        {"ItemCode", ""},
            {"Path", ""},
            {"Name", ""},
            {"ProductCategory", ""}
        };


        public Dictionary<string, string> GetItemInformation(string seller_id, string item_code)
        {
            try
            {
                string access_token = Properties.Settings.Default.AccessToken;
                var webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://circus.shopping.yahooapis.jp/ShoppingWebService/V1/getItem?seller_id={0}&item_code={1}", seller_id, item_code));
                //var webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:50608/XMLFile1.xml");
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Headers.Add("Authorization", "Bearer " + access_token);
                WebResponse response = webRequest.GetResponse();

                Stream dataStream = response.GetResponseStream();

                XmlReader reader = XmlReader.Create(dataStream);

                reader.MoveToContent();

                while (reader.ReadToFollowing("Result"))
                {
                    ParseItemInformation(reader.ReadSubtree());
                }

                return yahooItemRefInfo;
            }
            catch (WebException e)
            {
                return yahooItemRefInfo;

                //if (e.Status == WebExceptionStatus.ProtocolError)
                //{
                //    response = (HttpWebResponse)e.Response;
                //    if ((int)response.StatusCode == 404)
                //    {

                //    }
                //    //Console.Write("Errorcode: {0}", (int)response.StatusCode);
                //}
                //else
                //{
                //    //Console.Write("Error: {0}", e.Status);
                //}
                //  return e.ToString();

            }
            finally
            {

                //if (response != null)
                //{
                //    response.Close();
                //}
            }

        }

        static void ParseItemInformation(XmlReader reader)
        {
            string pattern = "";
            string replacement = "";
            //            Match match;

            reader.ReadToFollowing("ItemCode");
            yahooItemRefInfo["ItemCode"] = reader.ReadElementContentAsString("ItemCode", reader.NamespaceURI).ToString();
            //string ItemCode = reader.ReadElementContentAsString("ItemCode", reader.NamespaceURI)
            reader.ReadToFollowing("PathList");
            //            yahooItemRefInfo["Path"] = reader.ReadInnerXml().ToString();
            //            match = Regex.Match(reader.ReadInnerXml().ToString(), ".*CDATA[(.*?)].*");
            //           yahooItemRefInfo["Path"] = match.Value.Join("") .ToString ();//Regex.Replace(reader.ReadInnerXml().ToString(), @"\[(.*?)\]", "$1");
//            yahooItemRefInfo["Path"] = Regex.Replace(reader.ReadInnerXml().ToString(), @".*CDATA\[(.*?)\].*", "$1", RegexOptions.Multiline).Replace(" ", "");
            yahooItemRefInfo["Path"] = Regex.Replace(reader.ReadInnerXml().ToString(), @".*CDATA\[(.*?)\].*", "$1", RegexOptions.Multiline).TrimEnd();
            //yahooItemRefInfo["Path"] = Regex.Replace(yahooItemRefInfo["Path"].Replace("][", "\r\n"), @"\[\]", "");
            //yahooItemRefInfo["Path"] = yahooItemRefInfo["Path"].Replace("\n", "\r\n");
            //            yahooItemRefInfo["Path"] = reader.ReadElementContentAsString();
            //            Console.WriteLine(reader.ReadInnerXml());
            //reader.ReadElementContentAsString("PathList", reader.NamespaceURI);
            //reader.ReadToFollowing("Path");
            //yahooItemRefInfo["Path"] = reader.ReadElementContentAsString("Path", reader.NamespaceURI).ToString()+"\n";
            //reader.ReadToFollowing("Path");S
            //yahooItemRefInfo["Path"] += reader.ReadElementContentAsString("Path", reader.NamespaceURI).ToString() + "\n";
            //           reader.ReadToFollowing("Path");
            //           yahooItemRefInfo["Path"] += reader.ReadToFollowing("Path"S) ? reader.ReadElementContentAsString("Path", reader.NamespaceURI).ToString() : "";

            reader.ReadToFollowing("Name");
            yahooItemRefInfo["Name"] = reader.ReadElementContentAsString("Name", reader.NamespaceURI).ToString();
            //string Name = reader.ReadElementContentAsString("Name", reader.NamespaceURI)
            //string Name = reader.ReadElementContentAsString("Name", reader.NamespaceURI);
            //Console.WriteLine("{0}\n\t{1}", title, Name);

            reader.ReadToFollowing("ProductCategory");
            yahooItemRefInfo["ProductCategory"] = reader.ReadElementContentAsString("ProductCategory", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("OriginalPrice");
            yahooItemRefInfo["OriginalPrice"] = reader.ReadElementContentAsString("OriginalPrice", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Price");
            yahooItemRefInfo["Price"] = reader.ReadElementContentAsString("Price", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("SalePrice");
            yahooItemRefInfo["SalePrice"] = reader.ReadElementContentAsString("SalePrice", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Quantity");
            yahooItemRefInfo["Quantity"] = reader.ReadElementContentAsString("Quantity", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Headline");
            yahooItemRefInfo["Headline"] = reader.ReadElementContentAsString("Headline", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Caption");
            yahooItemRefInfo["Caption"] = reader.ReadElementContentAsString("Caption", reader.NamespaceURI).ToString();
            yahooItemRefInfo["Caption"] = Regex.Replace(yahooItemRefInfo["Caption"].ToString(), "<![CDATA[(.*)]]", "$1");

            reader.ReadToFollowing("Abstract");
            yahooItemRefInfo["Abstract"] = reader.ReadElementContentAsString("Abstract", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Explanation");
            yahooItemRefInfo["Explanation"] = reader.ReadElementContentAsString("Explanation", reader.NamespaceURI).ToString();
            yahooItemRefInfo["Explanation"] = Regex.Replace(yahooItemRefInfo["Explanation"].ToString(), "<![CDATA[(.*)]]", "$1");

            reader.ReadToFollowing("Additional1");
            yahooItemRefInfo["Additional1"] = reader.ReadElementContentAsString("Additional1", reader.NamespaceURI).ToString();
            yahooItemRefInfo["Additional1"] = Regex.Replace(yahooItemRefInfo["Additional1"].ToString(), "<![CDATA[(.*)]]", "$1");

            reader.ReadToFollowing("Additional2");
            yahooItemRefInfo["Additional2"] = reader.ReadElementContentAsString("Additional2", reader.NamespaceURI).ToString();
            yahooItemRefInfo["Additional2"] = Regex.Replace(yahooItemRefInfo["Additional2"].ToString(), "<![CDATA[(.*)]]", "$1");

            reader.ReadToFollowing("Additional3");
            yahooItemRefInfo["Additional3"] = reader.ReadElementContentAsString("Additional3", reader.NamespaceURI).ToString();
            yahooItemRefInfo["Additional3"] = Regex.Replace(yahooItemRefInfo["Additional3"].ToString(), "<![CDATA[(.*)]]", "$1");

            reader.ReadToFollowing("SpAdditional");
            yahooItemRefInfo["SpAdditional"] = reader.ReadElementContentAsString("SpAdditional", reader.NamespaceURI).ToString();
            yahooItemRefInfo["SpAdditional"] = Regex.Replace(yahooItemRefInfo["SpAdditional"].ToString(), "<![CDATA[(.*)]]", "$1");

            reader.ReadToFollowing("RelevantLinks");
            pattern = @"<ItemCode>(.*?)</ItemCode>";
            MatchCollection matchCollection = Regex.Matches(reader.ReadInnerXml().ToString(), pattern);
            List<string> relevantLinks = new List<string>();

            foreach (Match match in matchCollection)
            {
                relevantLinks.Add(match.Groups[1].Value);

            }

            yahooItemRefInfo["RelevantLinks"] = string.Join(",", relevantLinks); //relevantLinks.Join('|');
            reader.ReadToFollowing("CartRelatedItems");
            yahooItemRefInfo["CartRelatedItems"] = reader.ReadElementContentAsString("CartRelatedItems", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("ShipWeight");
            yahooItemRefInfo["ShipWeight"] = reader.ReadElementContentAsString("ShipWeight", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Taxable");
            yahooItemRefInfo["Taxable"] = reader.ReadElementContentAsString("Taxable", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("ReleaseDate");
            yahooItemRefInfo["ReleaseDate"] = reader.ReadElementContentAsString("ReleaseDate", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("PointCode");
            yahooItemRefInfo["PointCode"] = reader.ReadElementContentAsString("PointCode", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("MetaKey");
            yahooItemRefInfo["MetaKey"] = reader.ReadElementContentAsString("MetaKey", reader.NamespaceURI).ToString();
            yahooItemRefInfo["MetaKey"] = Regex.Replace(yahooItemRefInfo["MetaKey"].ToString(), "<![CDATA[(.*)]]", "$1");

            reader.ReadToFollowing("MetaDesc");
            yahooItemRefInfo["MetaDesc"] = reader.ReadElementContentAsString("MetaDesc", reader.NamespaceURI).ToString();
            yahooItemRefInfo["MetaDesc"] = Regex.Replace(yahooItemRefInfo["MetaDesc"].ToString(), "<![CDATA[(.*)]]", "$1");

            reader.ReadToFollowing("HiddenFlag");
            yahooItemRefInfo["HiddenFlag"] = reader.ReadElementContentAsString("HiddenFlag", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Id");
            yahooItemRefInfo["Id"] = reader.ReadElementContentAsString("Id", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Password");
            yahooItemRefInfo["Password"] = reader.ReadElementContentAsString("Password", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Display");
            yahooItemRefInfo["Display"] = reader.ReadElementContentAsString("Display", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("TemplateId");
            yahooItemRefInfo["TemplateId"] = reader.ReadElementContentAsString("TemplateId", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("TemplateName");
            yahooItemRefInfo["TemplateName"] = reader.ReadElementContentAsString("TemplateName", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("SalePeriodStart");
            yahooItemRefInfo["SalePeriodStart"] = reader.ReadElementContentAsString("SalePeriodStart", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("SalePeriodEnd");
            yahooItemRefInfo["SalePeriodEnd"] = reader.ReadElementContentAsString("SalePeriodEnd", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("SaleLimit");
            yahooItemRefInfo["SaleLimit"] = reader.ReadElementContentAsString("SaleLimit", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("SpCode");
            yahooItemRefInfo["SpCode"] = reader.ReadElementContentAsString("SpCode", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("BrandCode");
            yahooItemRefInfo["BrandCode"] = reader.ReadElementContentAsString("BrandCode", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("YahooProductCode");
            yahooItemRefInfo["YahooProductCode"] = reader.ReadElementContentAsString("YahooProductCode", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("ProductCode");
            yahooItemRefInfo["ProductCode"] = reader.ReadElementContentAsString("ProductCode", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Jan");
            yahooItemRefInfo["Jan"] = reader.ReadElementContentAsString("Jan", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Delivery");
            yahooItemRefInfo["Delivery"] = reader.ReadElementContentAsString("Delivery", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Image");
            yahooItemRefInfo["Image"] = reader.ReadElementContentAsString("Image", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("LibImage1");
            yahooItemRefInfo["LibImage1"] = reader.ReadElementContentAsString("LibImage1", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("LibImage2");
            yahooItemRefInfo["LibImage2"] = reader.ReadElementContentAsString("LibImage2", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("LibImage3");
            yahooItemRefInfo["LibImage3"] = reader.ReadElementContentAsString("LibImage3", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("LibImage4");
            yahooItemRefInfo["LibImage4"] = reader.ReadElementContentAsString("LibImage4", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("LibImage5");
            yahooItemRefInfo["LibImage5"] = reader.ReadElementContentAsString("LibImage5", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("AstkCode");
            yahooItemRefInfo["AstkCode"] = reader.ReadElementContentAsString("AstkCode", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Condition");
            yahooItemRefInfo["Condition"] = reader.ReadElementContentAsString("Condition", reader.NamespaceURI).ToString();
            reader.ReadToFollowing("Spec1");
            yahooItemRefInfo["Spec1"] = reader.ReadInnerXml().ToString();
            pattern = @"<SpecId>(.*?)</SpecId>";
            matchCollection = Regex.Matches(yahooItemRefInfo["Spec1"], pattern, RegexOptions.Multiline);
            List<string> spec1 = new List<string>();

            foreach (Match match in matchCollection)
            {
                spec1.Add(match.Groups[1].Value);

            }
            pattern = @"<SpecValue>(.*?)</SpecValue>";
            matchCollection = Regex.Matches(yahooItemRefInfo["Spec1"], pattern, RegexOptions.Multiline);

            foreach (Match match in matchCollection)
            {
                spec1.Add(match.Groups[1].Value);

            }

            yahooItemRefInfo["Spec1"] = string.Join(":", spec1);

            reader.ReadToFollowing("Spec2");
            yahooItemRefInfo["Spec2"] = reader.ReadInnerXml().ToString();
            pattern = @"<SpecId>(.*?)</SpecId>";
            matchCollection = Regex.Matches(yahooItemRefInfo["Spec2"], pattern, RegexOptions.Multiline);
            List<string> spec2 = new List<string>();

            foreach (Match match in matchCollection)
            {
                spec2.Add(match.Groups[1].Value);

            }
            pattern = @"<SpecValue>(.*?)</SpecValue>";
            matchCollection = Regex.Matches(yahooItemRefInfo["Spec2"], pattern, RegexOptions.Multiline);

            foreach (Match match in matchCollection)
            {
                spec2.Add(match.Groups[1].Value);

            }

            yahooItemRefInfo["Spec2"] = string.Join(":", spec2);


            reader.ReadToFollowing("Spec3");
            yahooItemRefInfo["Spec3"] = reader.ReadInnerXml().ToString();
            pattern = @"<SpecId>(.*?)</SpecId>";
            matchCollection = Regex.Matches(yahooItemRefInfo["Spec3"], pattern, RegexOptions.Multiline);
            List<string> spec3 = new List<string>();

            foreach (Match match in matchCollection)
            {
                spec3.Add(match.Groups[1].Value);

            }
            pattern = @"<SpecValue>(.*?)</SpecValue>";
            matchCollection = Regex.Matches(yahooItemRefInfo["Spec3"], pattern, RegexOptions.Multiline);

            foreach (Match match in matchCollection)
            {
                spec3.Add(match.Groups[1].Value);

            }

            yahooItemRefInfo["Spec3"] = string.Join(":", spec3);


            reader.ReadToFollowing("Spec4");
            yahooItemRefInfo["Spec4"] = reader.ReadInnerXml().ToString();
            pattern = @"<SpecId>(.*?)</SpecId>";
            matchCollection = Regex.Matches(yahooItemRefInfo["Spec4"], pattern, RegexOptions.Multiline);
            List<string> spec4 = new List<string>();

            foreach (Match match in matchCollection)
            {
                spec4.Add(match.Groups[1].Value);

            }
            pattern = @"<SpecValue>(.*?)</SpecValue>";
            matchCollection = Regex.Matches(yahooItemRefInfo["Spec4"], pattern, RegexOptions.Multiline);

            foreach (Match match in matchCollection)
            {
                spec4.Add(match.Groups[1].Value);

            }

            yahooItemRefInfo["Spec4"] = string.Join(":", spec4);


            reader.ReadToFollowing("Spec5");
            yahooItemRefInfo["Spec5"] = reader.ReadInnerXml().ToString();
            pattern = @"<SpecId>(.*?)</SpecId>";
            matchCollection = Regex.Matches(yahooItemRefInfo["Spec5"], pattern, RegexOptions.Multiline);
            List<string> spec5 = new List<string>();

            foreach (Match match in matchCollection)
            {
                spec5.Add(match.Groups[1].Value);

            }
            pattern = @"<SpecValue>(.*?)</SpecValue>";
            matchCollection = Regex.Matches(yahooItemRefInfo["Spec5"], pattern, RegexOptions.Multiline);

            foreach (Match match in matchCollection)
            {
                spec5.Add(match.Groups[1].Value);

            }

            yahooItemRefInfo["Spec5"] = string.Join(":", spec5);

            reader.ReadToFollowing("Options");
            yahooItemRefInfo["Options"] = reader.ReadInnerXml().ToString();
            yahooItemRefInfo["Options"] = Regex.Replace(yahooItemRefInfo["Options"].ToString(), @".*name=""(.*?)"".*", "$1", RegexOptions.Multiline);
            string[] tmpOptions = yahooItemRefInfo["Options"].Split('\n');
            //            System.Collections.Generic.HashSet<string> tmpTmpOptions = new System.Collections.Generic.HashSet<string>(yahooItemRefInfo["Options"].Split('\n'));

            //配列に変換する
            //            string[] tmpOptions = new string[tmpTmpOptions.Count];
            //            tmpTmpOptions.CopyTo(tmpOptions, 0);

            //List<string> option = new List<string>();
            //Hashtable option = new Hashtable();
            Dictionary<int, string> option = new Dictionary<int, string>();
            string tmpString = "";
            string options = "";
            bool optionNameFlg = true;
            bool optionValueFirstFlg = true;
            int z = 0;
            for (int i = 0; i < tmpOptions.Length; i++)
            {
                tmpOptions[i] = tmpOptions[i].Replace(" ", "");
                if (tmpOptions[i] != "")
                {

                    //take a option name
                    if (optionNameFlg == true)
                    {
                        options += tmpOptions[i];
                        optionNameFlg = false;
                        optionValueFirstFlg = true;
                    }

                    else if (0 <= tmpOptions[i].IndexOf("</Option>"))
                    {
                        options += "|";
                        z++;
                        optionNameFlg = true;
                    }
                    else if (optionValueFirstFlg == true)
                    {
                        options += "#" + tmpOptions[i];
                        optionValueFirstFlg = false;
                    }
                    else
                    {
                        //if (tmpOptions[i] == "─")
                        //{


                        //}
                        if (tmpString != tmpOptions[i])
                        {
                            options += "," + tmpOptions[i];
                        }
                    }
                    tmpString = tmpOptions[i];
                }
            }

            yahooItemRefInfo["Options"] = options;

            reader.ReadToFollowing("SubCodes");

            yahooItemRefInfo["SubCodes"] = Regex.Replace(reader.ReadInnerXml().ToString().Replace("\n", "").Replace("</SubCode>", "\n"), @".*code=""([^""]*)"".*name=""([^""]*)"".*value=""([^""]*)"".*name=""([^""]*)"".*value=""([^""]*)"".*", "$2:$3#$4:$5=$1|", RegexOptions.Multiline).Replace(" ", "".Replace(" ", "")).Trim('|');
            //yahooItemRefInfo["SubCodes"] = reader.ReadInnerXml().ToString();
            //string[] tmpOptions = yahooItemRefInfo["Options"].Split('\n');

            //            reader.ReadToFollowing("Inscriptions");
            //          yahooItemRefInfo["Inscriptions"] = reader.ReadInnerXml().ToString();


            reader.ReadToFollowing("ShowStock");
            yahooItemRefInfo["ShowStock"] = reader.ReadInnerXml().ToString();

            yahooItemRefInfo["Status"] = "OK";
        }
    }
}
