using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Yahoo_API;
using System.Net;
using System.Xml;
using Ionic.Zip;
using System.Collections.Specialized;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
namespace API_Y_Painttool
{
    public class Program
    {
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static string ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        static string BakExportCSVPath = ConfigurationManager.AppSettings["BakExportCSVPath"].ToString();
        static string ItemImage = ConfigurationManager.AppSettings["ItemImage"].ToString();
        static string Rpath = ConfigurationManager.AppSettings["RPath"].ToString();
        static string exhibitionerror = "";
        static string deleteerror = "";
        static int exhibitid = 0;
        static int itemid = 0;

        static SqlConnection GetConnection()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }  

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "API Yahoo PaintandTool";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ConsoleWriteLine_Tofile("API Yahoo PaintandTool : " + DateTime.Now);
                string list = SelectExhibitionItemID();
                ConsoleWriteLine_Tofile("1.SelectExhibitionItemID : " + list);
                if (!string.IsNullOrWhiteSpace(list))
                {
                    string[] code = list.Split(',');
                    foreach (string itemcode in code)
                    {
                        DataTable dtItemMaster = GetItemData(itemcode, 2);
                        ConsoleWriteLine_Tofile("3.GetItemData : " + DateTime.Now);
                        if (dtItemMaster != null && dtItemMaster.Rows.Count > 0)
                        {
                            Export_CSV3 export = new Export_CSV3();
                            DataTable dtItem = export.ModifyTable(dtItemMaster, 2);
                            ConsoleWriteLine_Tofile("4.ChangeTemplate : " + DateTime.Now);
                            SaveLogExhibition(dtItem, itemcode, 2);
                            ConsoleWriteLine_Tofile("5.SaveLogExhibition : " + DateTime.Now);
                            DataTable dtImage = SelectLogExhibitionImage(2, itemcode);
                            ConsoleWriteLine_Tofile("6.GetImageList : " + DateTime.Now);
                            DataTable dtSelect = SelectLogExhibitionSelect(Convert.ToInt16(dtItemMaster.Rows[0]["Shop_ID"]), 2, itemcode);
                            ConsoleWriteLine_Tofile("7.GetSelectData : " + DateTime.Now);
                            UploadItem(dtImage, dtItem, dtSelect);
                            ConsoleWriteLine_Tofile("8.UploadDataToAPI : " + DateTime.Now);
                            AddExportedDate(2, Convert.ToInt32(dtItemMaster.Rows[0]["Exhibit_ID"]));
                            ChangeFlag(itemcode , 2);
                        }
                    }
                    ChangeReflectionFlag();
                    //ChangeFlag(list, 2);
                    ConsoleWriteLine_Tofile("9.ChangeFlag : " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "YahooAPIRacket" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        #region Delete
        static void UploadDeleteItem(DataTable deletelist)
        {
            try
            {
                YahooAPI yahooAPI = new YahooAPI();
                if (yahooAPI.YahooAPIAuth(Rpath).ToString() != "success")
                {
                    return;
                }
                if (deletelist != null && deletelist.Rows.Count > 0)
                {
                    for (int i = 0; i < deletelist.Rows.Count; i++)
                    {
                    deletelabel:
                        deleteerror = DeleteToYahooAPI(deletelist.Rows[i]["Item_Code"].ToString());
                        if (deleteerror.Contains("反映またはアップロード中のため更新ができません。"))
                        {
                            goto deletelabel;
                        }
                        if (deleteerror != "OK")
                        {
                            if (Regex.IsMatch(deleteerror, "[0-9,A-Z,a-z\\-]"))
                            {
                                deleteerror = Regex.Replace(deleteerror, "[0-9,A-Z,a-z\\-]", "");
                            }
                            CheckErrorExists(2, Convert.ToInt32(deletelist.Rows[i]["Exhibit_ID"]), deleteerror);
                        }
                        else
                        {
                            CheckErrorExists(2, Convert.ToInt32(deletelist.Rows[i]["Exhibit_ID"]), "OK");
                        }
                        SaveItemShopAPIInfo(Convert.ToInt32(deletelist.Rows[i]["Exhibit_ID"]), 2, "APIdatadel$" + DateTime.Now.ToString("ddMMyyyy_HHmmss"), "d");
                        ChangeFlagDeleteItem(Convert.ToInt32(deletelist.Rows[i]["Item_ID"]));
                        AddExportedDate(2, Convert.ToInt32(deletelist.Rows[i]["Exhibit_ID"]));
                        ChangeCtrl_ID(Convert.ToInt32(deletelist.Rows[i]["Item_ID"]), 2,"d");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region API
        static void UploadItem(DataTable dtImage, DataTable dtItem, DataTable dtSelect)
        {
            try
            {
                YahooAPI yahooAPI = new YahooAPI();
                if (yahooAPI.YahooAPIAuth(Rpath).ToString() != "success")
                {
                    return;
                }
                if (dtImage != null && dtImage.Rows.Count > 0)
                {
                    CreateUploadImage(dtImage);
                    ImageZip(2);
                img:
                    string imageresult = UploadImageToYahooAPI();
                    if (imageresult.Contains("反映またはアップロード中のため更新ができません。"))
                    {
                        goto img;
                    }
                    else if (imageresult == "OK" || imageresult.Contains("OK"))
                    {
                        exhibitionerror = "";
                    }
                    else
                    {
                        exhibitionerror = imageresult;
                    }
                    if (File.Exists(ExportCSVPath + "img.zip"))
                    {
                        File.Delete(ExportCSVPath + "img.zip");
                    }
                }
                System.Threading.Thread.Sleep(3000);
                if (dtItem != null & dtItem.Rows.Count > 0)
                {
                labelctrl:
                    string ctrlid = CheckNewUpdateItemCode(dtItem.Rows[0]["Item_Code"].ToString());
                    if (ctrlid != "n" && ctrlid != "u")
                    {
                        goto labelctrl;
                    }
                    else
                    {
                        itemid = Convert.ToInt32(dtItem.Rows[0]["Item_ID"]);
                        exhibitid = Convert.ToInt32(dtItem.Rows[0]["Exhibit_ID"]);
                        Dictionary<string, string> yahooItemRefInfo = new Dictionary<string, string>();
                        yahooItemRefInfo["Path"] = dtItem.Rows[0]["Y_Path"].ToString(); ;
                        yahooItemRefInfo["Name"] = dtItem.Rows[0]["Y_Item_Name"].ToString();
                        yahooItemRefInfo["Item_code"] = dtItem.Rows[0]["Item_Code"].ToString();
                        yahooItemRefInfo["Subcodes"] = dtItem.Rows[0]["Y_SubCode"].ToString();
                        yahooItemRefInfo["OriginalPrice"] = dtItem.Rows[0]["Y_List_Price"].ToString();
                        yahooItemRefInfo["Price"] = dtItem.Rows[0]["Sale_Price"].ToString();
                        yahooItemRefInfo["SalePrice"] = "";
                        yahooItemRefInfo["Options"] = dtItem.Rows[0]["options"].ToString();
                        yahooItemRefInfo["Headline"] = "";
                        yahooItemRefInfo["Abstract"] = "";
                        yahooItemRefInfo["Explanation"] = dtItem.Rows[0]["Explanation"].ToString();
                        yahooItemRefInfo["Additional1"] = dtItem.Rows[0]["Sale_Description_PC"].ToString();
                        yahooItemRefInfo["Additional2"] = "";
                        yahooItemRefInfo["Additional3"] = "";
                        yahooItemRefInfo["Relevant_links"] = dtItem.Rows[0]["Y_Relevant_links"].ToString();
                        yahooItemRefInfo["Caption"] = dtItem.Rows[0]["Item_Description_PC"].ToString();
                        yahooItemRefInfo["ShipWeight"] = dtItem.Rows[0]["Y_Special_Flag"].ToString();
                        yahooItemRefInfo["Taxable"] = dtItem.Rows[0]["Taxable"].ToString();
                        yahooItemRefInfo["ReleaseDate"] = "";
                        yahooItemRefInfo["PointCode"] = dtItem.Rows[0]["Y_Point_Code"].ToString();
                        yahooItemRefInfo["MetaDesc"] = "";
                        yahooItemRefInfo["Template"] = "";
                        yahooItemRefInfo["SalePeriodStart"] = "";
                        yahooItemRefInfo["SalePeriodEnd"] = "";
                        yahooItemRefInfo["SaleLimit"] = dtItem.Rows[0]["YF_NoofPurchases"].ToString();
                        yahooItemRefInfo["SpCode"] = "";
                        yahooItemRefInfo["BrandCode"] = dtItem.Rows[0]["Brand_Code_Yahoo"].ToString();
                        yahooItemRefInfo["YahooProductCode"] = "";
                        yahooItemRefInfo["ProductCode"] = dtItem.Rows[0]["Product_Code"].ToString();
                        yahooItemRefInfo["Jan"] = "";
                        yahooItemRefInfo["Delivery"] = dtItem.Rows[0]["Y_Delivery"].ToString();
                        yahooItemRefInfo["AstkCode"] = "0";
                        yahooItemRefInfo["Condition"] = dtItem.Rows[0]["Condition"].ToString();
                        yahooItemRefInfo["Product_category"] = dtItem.Rows[0]["Yahoo_CategoryID"].ToString();
                        yahooItemRefInfo["Spec1"] = dtItem.Rows[0]["Spec1"].ToString();
                        yahooItemRefInfo["Spec2"] = dtItem.Rows[0]["Spec2"].ToString();
                        yahooItemRefInfo["Spec3"] = dtItem.Rows[0]["Spec3"].ToString();
                        yahooItemRefInfo["Spec4"] = dtItem.Rows[0]["Spec4"].ToString();
                        yahooItemRefInfo["Spec5"] = dtItem.Rows[0]["Spec5"].ToString();
                        yahooItemRefInfo["Display"] = dtItem.Rows[0]["Display"].ToString();
                        yahooItemRefInfo["SpAdditional"] = dtItem.Rows[0]["Smart_Template"].ToString();
                        yahooItemRefInfo["original_price_evidence"] = dtItem.Rows[0]["Y_URL"].ToString();
                        yahooItemRefInfo["subcode_param"] = dtItem.Rows[0]["subcode_param"].ToString();
                        //yahooItemRefInfo["postage_set"] = "2";
                        ConsoleWriteLine_Tofile1("Item Code : " + dtItem.Rows[0]["Item_Code"].ToString());
                        ConsoleWriteLine_Tofile1("SaleDescriptionPC : " + dtItem.Rows[0]["Sale_Description_PC"].ToString());
                        ConsoleWriteLine_Tofile1("ItemDescriptionPC : " + dtItem.Rows[0]["Item_Description_PC"].ToString());
                        ConsoleWriteLine_Tofile1("Explanation : " + dtItem.Rows[0]["Explanation"].ToString());
                        ConsoleWriteLine_Tofile1("Finished : " + DateTime.Now);
                    label:
                        string result = UploadToYahooAPI(yahooItemRefInfo);
                        if (result.Contains("反映またはアップロード中のため更新ができません。"))
                        {
                            goto label;
                        }
                        else if (result == "OK" || result.Contains("OK"))
                        {
                            exhibitionerror += "";
                        }
                        else
                        {
                            exhibitionerror += result;
                        }
                        SaveItemShopAPIInfo(exhibitid, 2, "APIdataadd$" + DateTime.Now.ToString("ddMMyyyy_HHmmss"), ctrlid);
                    }
                }
                System.Threading.Thread.Sleep(3000);
                if (dtSelect != null && dtSelect.Rows.Count > 0)
                {
                    exhibitid = Convert.ToInt32(dtSelect.Rows[0]["Exhibit_ID"]);
                    string subcode = "", quantity = "";
                    Dictionary<string, string> yahooItemRefInfo = new Dictionary<string, string>();
                    foreach (DataRow quantitylist in dtSelect.Rows)
                    {
                        if (!String.IsNullOrWhiteSpace(quantitylist["sub-code"].ToString()))
                        {
                            subcode += quantitylist["code"].ToString() + ":" + quantitylist["sub-code"].ToString() + ",";
                        }
                        else
                        {
                            subcode += quantitylist["code"].ToString();
                        }
                        quantity += quantitylist["quantity"].ToString() + ",";
                    }
                    subcode = subcode.TrimEnd(',');
                    quantity = quantity.TrimEnd(',');
                    yahooItemRefInfo["Item_code"] = subcode;
                    yahooItemRefInfo["Quantity"] = quantity;
                inventory:
                    string result = UploadToInventoryYahooAPI(yahooItemRefInfo);
                    if (result.Contains("反映またはアップロード中のため更新ができません。"))
                    {
                        goto inventory;
                    }
                    else if ((result == "OK" || result.Contains(dtSelect.Rows[0]["code"].ToString())) && (String.IsNullOrWhiteSpace(exhibitionerror)))
                    {
                        exhibitionerror = "OK";
                        ChangeCtrl_ID(itemid, 2, "u");
                    }
                    else
                    {
                        if (result == "OK" || result.Contains(dtSelect.Rows[0]["code"].ToString()))
                        {
                            result = "";
                            exhibitionerror += result;
                        }
                    }
                }
                CheckErrorExists(2, exhibitid, exhibitionerror);
                ChangeIsGeneratedCSVFlag(itemid, exhibitid, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static string ChangeReflectionFlag()
        {
            try
            {
                YahooAPI yahooAPI = new YahooAPI();
                string access_token = yahooAPI.GetAccessToken();
                string postData = string.Format("seller_id={0}&mode={1}&reserve_time={2}", "paintandtool", "1", "");
                var webRequest = (HttpWebRequest)WebRequest.Create("https://circus.shopping.yahooapis.jp/ShoppingWebService/V1/reservePublish");
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(yahooAPI.GetAppID() + ":" + yahooAPI.GetSecretKey()));
                webRequest.Headers.Add("Authorization", "Bearer " + access_token);
                using (var s = webRequest.GetRequestStream())
                using (var sw = new StreamWriter(s, new UTF8Encoding(false)))
                    sw.Write(postData.ToString());
                using (var webResponse = webRequest.GetResponse())
                {
                    var responseStream = webResponse.GetResponseStream();
                    if (responseStream == null)
                        return null;
                    using (var reader = new StreamReader(responseStream))
                    {
                        var response = reader.ReadToEnd();
                        XmlDocument xd = new XmlDocument();
                        xd.LoadXml(response);
                        response = xd.InnerText;
                        return response.ToString();
                    }
                }
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

        static string CheckNewUpdateItemCode(string itemcode)
        {
            try
            {
                YahooAPI yahooAPI = new YahooAPI();
                string access_token = yahooAPI.GetAccessToken();
                var webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://shopping.yahooapis.jp/ShoppingWebService/V1/itemLookup?appid={0}&itemcode={1}", yahooAPI.GetAppID(), "paintandtool_" + itemcode));
                webRequest.Method = "GET";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(yahooAPI.GetAppID() + ":" + yahooAPI.GetSecretKey()));
                webRequest.Headers.Add("Authorization", "Bearer " + access_token);
                using (var webResponse = webRequest.GetResponse())
                {
                    var responseStream = webResponse.GetResponseStream();
                    if (responseStream == null)
                        return null;
                    using (var reader = new StreamReader(responseStream))
                    {
                        var response = reader.ReadToEnd();
                        XmlDocument xd = new XmlDocument();
                        xd.LoadXml(response);
                        response = xd.InnerText;
                        if (response.ToString().Contains("https://store.shopping.yahoo.co.jp/paintandtool/" + itemcode + ".html"))
                        {
                            return "u";
                        }
                        else
                        {
                            return "n";
                        }
                    }
                }
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
            finally
            {

            }
        }

        static string UploadToYahooAPI(Dictionary<string, string> yahooItemRefInfo)
        {
            try
            {
                YahooAPI yahooAPI = new YahooAPI();
                string access_token = yahooAPI.GetAccessToken();
                string postData = string.Format("seller_id={0}&item_code={1}&path={2}&name={3}&product_category={4}&original_price={5}&price={6}" +
                        "&sale_price={7}&headline={8}&caption={9}&abstract={10}" +
                        "&explanation={11}&additional1={12}&additional2={13}&additional3={14}&sp_additional={15}&relevant_links={16}&ship_weight={17}&taxable={18}&release_date={19}" +
                        "&sale_period_start={20}&sale_period_end={21}&sale_limit={22}&sp_code={23}&point_code={24}&meta_desc={25}&display={26}&brand_code={27}" +
                        "&yahoo_product_code={28}&product_code={29}&jan={30}&delivery={31}&astk_code={32}&condition={33}&spec1={34}&spec2={35}&spec3={36}&spec4={37}&spec5={38}&options={39}&subcodes={40}&original_price_evidence={41}&subcode_param={42}",//&postage_set={43}
                        "paintandtool", yahooItemRefInfo["Item_code"].ToString(), yahooItemRefInfo["Path"].ToString(), yahooItemRefInfo["Name"].ToString(), yahooItemRefInfo["Product_category"].ToString(),
                        yahooItemRefInfo["OriginalPrice"].ToString(), yahooItemRefInfo["Price"].ToString(),
                        yahooItemRefInfo["SalePrice"].ToString(), yahooItemRefInfo["Headline"].ToString(),
                        yahooItemRefInfo["Caption"].ToString(), yahooItemRefInfo["Abstract"].ToString(), yahooItemRefInfo["Explanation"].ToString(), yahooItemRefInfo["Additional1"].ToString(),
                        yahooItemRefInfo["Additional2"].ToString(), yahooItemRefInfo["Additional3"].ToString(), yahooItemRefInfo["SpAdditional"].ToString(), yahooItemRefInfo["Relevant_links"].ToString(),
                        yahooItemRefInfo["ShipWeight"].ToString(), yahooItemRefInfo["Taxable"].ToString(), yahooItemRefInfo["ReleaseDate"].ToString(), yahooItemRefInfo["SalePeriodStart"].ToString(),
                        yahooItemRefInfo["SalePeriodEnd"].ToString(), yahooItemRefInfo["SaleLimit"].ToString(), yahooItemRefInfo["SpCode"].ToString(), yahooItemRefInfo["PointCode"].ToString(),
                        yahooItemRefInfo["MetaDesc"].ToString(), yahooItemRefInfo["Display"].ToString(), yahooItemRefInfo["BrandCode"].ToString(), yahooItemRefInfo["YahooProductCode"].ToString(),
                        yahooItemRefInfo["ProductCode"].ToString(), yahooItemRefInfo["Jan"].ToString(), yahooItemRefInfo["Delivery"].ToString(), yahooItemRefInfo["AstkCode"].ToString(),
                        yahooItemRefInfo["Condition"].ToString(), yahooItemRefInfo["Spec1"].ToString(), yahooItemRefInfo["Spec2"].ToString(), yahooItemRefInfo["Spec3"].ToString(),
                        yahooItemRefInfo["Spec4"].ToString(), yahooItemRefInfo["Spec5"].ToString(), yahooItemRefInfo["Options"].ToString(), yahooItemRefInfo["Subcodes"].ToString(), yahooItemRefInfo["original_price_evidence"].ToString(), yahooItemRefInfo["subcode_param"].ToString(), "2");
                var webRequest = (HttpWebRequest)WebRequest.Create("https://circus.shopping.yahooapis.jp/ShoppingWebService/V1/editItem");
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(yahooAPI.GetAppID() + ":" + yahooAPI.GetSecretKey()));
                webRequest.Headers.Add("Authorization", "Bearer " + access_token);
                using (var s = webRequest.GetRequestStream())
                using (var sw = new StreamWriter(s, new UTF8Encoding(false)))
                    sw.Write(postData.ToString());
                using (var webResponse = webRequest.GetResponse())
                {
                    var responseStream = webResponse.GetResponseStream();
                    if (responseStream == null)
                        return null;
                    using (var reader = new StreamReader(responseStream))
                    {
                        var response = reader.ReadToEnd();
                        XmlDocument xd = new XmlDocument();
                        xd.LoadXml(response);
                        response = xd.InnerText;
                        return response.ToString();
                    }
                }
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
            finally
            {
                yahooItemRefInfo["Status"] = "NG";
            }
        }

        static string UploadImageToYahooAPI()
        {
            try
            {
                string filepath = ExportCSVPath + "img.zip";
                string[] files = new string[1];
                files[0] = filepath;
                YahooAPI yahooAPI = new YahooAPI();
                string access_token = yahooAPI.GetAccessToken();
                string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://circus.shopping.yahooapis.jp/ShoppingWebService/V1/uploadItemImagePack?seller_id=paintandtool");
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.Method = "POST";
                request.KeepAlive = true;
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(yahooAPI.GetAppID() + ":" + yahooAPI.GetSecretKey()));
                request.Headers.Add("Authorization", "Bearer " + access_token);
                Stream memStream = new System.IO.MemoryStream();
                var boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                var endBoundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--");
                string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";
                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" + "Content-Type: application/octet-stream\r\n\r\n";
                for (int i = 0; i < files.Length; i++)
                {
                    memStream.Write(boundarybytes, 0, boundarybytes.Length);
                    var header = string.Format(headerTemplate, "file", files[i]);
                    var headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                    memStream.Write(headerbytes, 0, headerbytes.Length);
                    using (var fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read))
                    {
                        var buffer = new byte[1024];
                        var bytesRead = 0;
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            memStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
                memStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                request.ContentLength = memStream.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    memStream.Position = 0;
                    byte[] tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();
                    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                }
                using (var response = request.GetResponse())
                {
                    Stream stream2 = response.GetResponseStream();
                    StreamReader reader2 = new StreamReader(stream2);
                    var result = reader2.ReadToEnd();
                    XmlDocument xd = new XmlDocument();
                    xd.LoadXml(result);
                    result = xd.InnerText;
                    return result.ToString();
                }
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

        static string UploadToInventoryYahooAPI(Dictionary<string, string> yahooItemRefInfo)
        {
            try
            {
                YahooAPI yahooAPI = new YahooAPI();
                string access_token = yahooAPI.GetAccessToken();
                string postData = string.Format("seller_id={0}&item_code={1}&quantity={2}",
                        "paintandtool", yahooItemRefInfo["Item_code"].ToString(), yahooItemRefInfo["Quantity"].ToString());
                var webRequest = (HttpWebRequest)WebRequest.Create("https://circus.shopping.yahooapis.jp/ShoppingWebService/V1/setStock");
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(yahooAPI.GetAppID() + ":" + yahooAPI.GetSecretKey()));
                webRequest.Headers.Add("Authorization", "Bearer " + access_token);
                using (var s = webRequest.GetRequestStream())
                using (var sw = new StreamWriter(s, new UTF8Encoding(false)))
                    sw.Write(postData.ToString());
                using (var webResponse = webRequest.GetResponse())
                {
                    var responseStream = webResponse.GetResponseStream();
                    if (responseStream == null)
                        return null;
                    using (var reader = new StreamReader(responseStream))
                    {
                        var response = reader.ReadToEnd();
                        XmlDocument xd = new XmlDocument();
                        xd.LoadXml(response);
                        response = xd.InnerText;
                        return response.ToString();
                    }
                }
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
            finally
            {
                yahooItemRefInfo["Status"] = "NG";
            }
        }

        static string DeleteToYahooAPI(string deletelist)
        {
            try
            {
                YahooAPI yahooAPI = new YahooAPI();
                string access_token = yahooAPI.GetAccessToken();
                string postData = string.Format("seller_id={0}&item_code={1}", "paintandtool", deletelist);
                var webRequest = (HttpWebRequest)WebRequest.Create("Https://circus.shopping.yahooapis.jp/ShoppingWebService/V1/deleteItem");
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(yahooAPI.GetAppID() + ":" + yahooAPI.GetSecretKey()));
                webRequest.Headers.Add("Authorization", "Bearer " + access_token);
                using (var s = webRequest.GetRequestStream())
                using (var sw = new StreamWriter(s, new UTF8Encoding(false)))
                    sw.Write(postData.ToString());
                using (var webResponse = webRequest.GetResponse())
                {
                    var responseStream = webResponse.GetResponseStream();
                    if (responseStream == null)
                        return null;
                    using (var reader = new StreamReader(responseStream))
                    {
                        var response = reader.ReadToEnd();
                        XmlDocument xd = new XmlDocument();
                        xd.LoadXml(response);
                        response = xd.InnerText;
                        return response.ToString();
                    }
                }
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
        #endregion

        #region ChangeFlag
        static void ChangeFlag(string list, int shop_ID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_ChangeFlagByShop_API", conn);
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

        static void ChangeIsGeneratedCSVFlag(int Item_ID, int Exhibit_ID, int Shop_ID)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_ChangeFlag_ByMall_API", connectionstring);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Parameters.AddWithValue("@Item_ID", Item_ID);
                cmd.Parameters.AddWithValue("@Exhibit_ID", Exhibit_ID);
                cmd.Parameters.AddWithValue("@Shop_ID", Shop_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ChangeFlagDeleteItem(int itemid)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_DeleteItem_ChangeFlag", connectionstring);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Parameters.AddWithValue("@Item_ID", itemid);
                cmd.Parameters.AddWithValue("@Shop_ID", 2);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ChangeCtrl_ID(int Item_ID, int Shop_ID,string ctrlid)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Shop_ChangeCtrl_ID_API", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", Item_ID);
                cmd.Parameters.AddWithValue("@Shop_ID", Shop_ID);
                cmd.Parameters.AddWithValue("@Ctrl_ID", ctrlid);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SelectData
        static string SelectExhibitionItemID()
        {
            try
            {
                DataTable dt = new DataTable();
                string result = "";
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_List_SelectItem_IDList_New", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", 2);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["Item_ID"].ToString();
                }
                return result;
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
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_List_CollectData_New_API", conn);
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

        static DataTable SelectLogExhibitionSelect(int shop_id, int mall_id, string itemcode)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Select_SelectByShop_Yahoo_API", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID", mall_id);
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", itemcode);
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

        static DataTable SelectLogExhibitionImage(int shop_id, string itemcode)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetImageList_Select_By_Mall_API", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", itemcode);
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

        static DataTable SelectDeleteItem()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Select_Delete_Item_For_Yahoo", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", 2);
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
        #endregion

        #region SaveData
        static void AddExportedDate(int shopID, int exhibitid)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Exhibition_Item_Shop_ChangeFlag_API", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Shop_ID", shopID);
                cmd.Parameters.AddWithValue("@Exhibit_ID", exhibitid);
                cmd.CommandTimeout = 0;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            try
            {
                StreamWriter sw = new StreamWriter(consoleWriteLinePath + "YahooRacketAPI.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
                sw.AutoFlush = true;
                Console.SetOut(sw);
                Console.WriteLine(traceText);
                sw.Close();
                sw.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ConsoleWriteLine_Tofile1(string traceText)
        {
            try
            {
                StreamWriter sw = new StreamWriter(consoleWriteLinePath + "YahooRacketTemplate.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
                sw.AutoFlush = true;
                Console.SetOut(sw);
                Console.WriteLine(traceText);
                sw.Close();
                sw.Dispose();
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

        static void SaveItemShopAPIInfo(int Exhibit_ID, int shopID, string csvName, string ctrl_id)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Exhibition_Item_Shop_UpdateInfo", connectionstring);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Exhibit_ID", Exhibit_ID);
                cmd.Parameters.AddWithValue("@Shop_ID", shopID);
                cmd.Parameters.AddWithValue("@CSV_Name", csvName);
                cmd.Parameters.AddWithValue("@Ctrl_ID", ctrl_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void CheckErrorExists(int shopid, int exhibitid, string result)
        {
            try
            {
                SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand("SP_Yahoo_Error_Check", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Shop_ID", Convert.ToInt32(shopid));
                cmd.Parameters.AddWithValue("@Exhibit_ID", Convert.ToInt32(exhibitid));
                cmd.Parameters.AddWithValue("@Result", result);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Image
        static void CreateUploadImage(DataTable dt)
        {
            try
            {
                //Create new folder for upload image
                String folderpath = ExportCSVPath + "\\PaintandToolImage\\";
                if (!Directory.Exists(folderpath))
                    Directory.CreateDirectory(folderpath);

                //Upload Image
                DataRow[] dr;
                #region Image
                dr = dt.Select("Image_Type=0");
                if (dr.Count() > 0)
                {
                    DataTable dtImage = dt.Select("Image_Type=0").CopyToDataTable();
                    for (int i = 0; i < dtImage.Rows.Count; i++)
                    {
                        string str = ItemImage + dtImage.Rows[i]["Image_Name"].ToString();
                        //Save image into folder
                        if (Directory.Exists(folderpath))
                        {
                            if (File.Exists(str))
                            {
                                if (!File.Exists(folderpath + dtImage.Rows[i]["Image_Name"].ToString()))
                                {
                                    if (i == 0)
                                        File.Copy(str, folderpath + dtImage.Rows[i]["Item_Code"].ToString() + ".jpg");
                                    else
                                        File.Copy(str, folderpath + dtImage.Rows[i]["Item_Code"].ToString() + "_" + i + ".jpg");
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ImageZip(int shop_id)
        {
            try
            {
                //Image Zip
                string date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                String pathName = ExportCSVPath + "\\PaintandToolImage\\";
                string[] fileNames = Directory.GetFiles(pathName);
                if (fileNames != null && fileNames.Length > 0)
                {
                    using (ZipFile zipfile = new ZipFile())
                    {
                        zipfile.AddFiles(fileNames, "");
                        zipfile.Save(ExportCSVPath + "img.zip");
                        zipfile.Save(BakExportCSVPath + "img$" + date + ".zip");
                    }
                }
                DeleteFilesFromDirectory();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void DeleteFilesFromDirectory()
        {
            try
            {
                String path = ExportCSVPath + "\\PaintandToolImage\\";
                string[] files = Directory.GetFiles(path);
                foreach (string pathFile in files)
                {
                    var file = new FileInfo(pathFile);
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(pathFile);
                }
                if (Directory.Exists(path))
                {
                    Directory.Delete(path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
    }
}
