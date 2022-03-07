using Ionic.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace API_Wowma
{
    class Program
    {
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static String ExportCSVPath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        static String Bak_ExportCSVPath = ConfigurationManager.AppSettings["BakExportCSVPath"].ToString();
        static String ItemImage = ConfigurationManager.AppSettings["ItemImage"].ToString();
        static string CtrlID = "";
        static DataTable dtOption;
        static DataTable dtCategory;
        static DataTable dtAPIKey;
        static int count = 0;
        static string errCode1 = string.Empty;
        static string errCode2 = string.Empty;
        static string itemcode1 = string.Empty;
        static string exhibitionerror = string.Empty;
        public string apiKey = ConfigurationManager.AppSettings["apiKey"].ToString();
        public string shopID = ConfigurationManager.AppSettings["shopID"].ToString();

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "API Wowma Racket";
                dtAPIKey = GetAPIKey(4);
                string list = SelectExhibitionItemID();
                //string list = "348701";
                ConsoleWriteLine_Tofile("1.SelectExhibitionItemID : " + list);
                string sku = ExportCSVPath + "4" + "/item_img/";
                if (!string.IsNullOrWhiteSpace(list))
                {
                    string[] code = list.Split(',');

                    //  Collect Images for all ItemCode
                    foreach (string itemcode in code)
                    {
                        DataTable dtImage = SelectLogExhibitionImage(4, itemcode);
                        if (dtImage != null && dtImage.Rows.Count > 0)
                        {

                            string path = "";
                            string folderName = "";
                            string image_name = "";
                            // string sku = ExportCSVPath + "26" + "/item_img/";
                            foreach (DataRow drImage in dtImage.Rows)
                            {
                                if (drImage["Image_Name"].ToString() != "")
                                    {
                                        image_name = drImage["Image_Name"].ToString();
                                        folderName = drImage["Folder_Name"].ToString();
                                        path = sku + "images" + "/";
                                        if (!Directory.Exists(path))
                                            Directory.CreateDirectory(path);
                                        //  Save image into folder
                                        if (Directory.Exists(path))
                                        {
                                            if (File.Exists(ItemImage + image_name))
                                            {
                                                if (!File.Exists(path + image_name))
                                                    File.Copy(ItemImage + image_name, path + image_name);
                                            }
                                        }
                                    }
                            }
                        }
                    }


                    if (Directory.Exists(sku + "images" + "/"))
                    {
                        using (ZipFile zipfile = new ZipFile())
                        {
                            String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                            zipfile.AddDirectory(sku);
                            zipfile.Save(ExportCSVPath + "4" + "/images" + "$" + "4" + "_" + date + ".zip");
                            zipfile.Save(Bak_ExportCSVPath + "images" + "$" + "4" + "_" + date + ".zip");
                            SaveItem_ExportQ("images" + "$" + "4" + "_" + date + ".zip", 3, 4, 0, 1);
                        }

                        Directory.Delete(sku + "images", true);
                    }



                    foreach (string itemcode in code)
                    {
                        itemcode1 = string.Empty;
                        itemcode1 = itemcode;
                        CheckItemCodeForNewUpdate(itemcode);
                        ConsoleWriteLine_Tofile("2.CheckItemCode for " + itemcode + " : " + DateTime.Now);

                        DataTable dtItemMaster = GetItemData(itemcode, 4);
                        if ((dtItemMaster != null) && dtItemMaster.Rows.Count > 0)
                        {
                            ConsoleWriteLine_Tofile("3.GetItemData : " + DateTime.Now);

                            DataRow[] drwarehouse = dtItemMaster.Select("Warehouse_Specified=2");
                            if (drwarehouse.Count() > 0)
                            {
                                DataTable dtWarehouseItem = dtItemMaster.Select("Warehouse_Specified=2").CopyToDataTable();
                                SaveLogWarehouseItem(dtWarehouseItem);
                                UploadWarehouse(dtWarehouseItem, itemcode);

                                SaveItemShopAPIInfo(Convert.ToInt32(dtWarehouseItem.Rows[0]["Exhibit_ID"]), 4, "APIitem$" + DateTime.Now.ToString("ddMMyyyy_HHmmss"), CtrlID);      //UPDATE Exhibition_Item_Shop 
                                ChangeIsGeneratedCSVFlag(Convert.ToInt32(itemcode), Convert.ToInt32(dtWarehouseItem.Rows[0]["Exhibit_ID"]), 4);      //update Log table IsGeneratedCSV to 1 //Update Item_Master table Export_Status='4' and Ctrl_ID='w'or'u' //update Exhibition_Item_Shop table WarehouseExhibition_Flag
                                ChangeCtrl_ID(Convert.ToInt32(dtWarehouseItem.Rows[0]["Exhibit_ID"]), Convert.ToInt32(dtWarehouseItem.Rows[0]["Item_ID"]), 4, CtrlID);       //update Item_Shop and Item_Master set Ctrl_ID='d'/'u'/'w', Export_Status=4
                                AddExportedDate(4, Convert.ToInt32(dtWarehouseItem.Rows[0]["Exhibit_ID"]));
                            }                           
                            else
                            {
                                Export_CSV3 export = new Export_CSV3();
                                DataTable dtItem = export.ModifyTable(dtItemMaster, 4);
                                //DataTable dtItem = dtItemMaster;
                                ConsoleWriteLine_Tofile("4.ChangeTemplate : " + DateTime.Now);

                                SaveLogExhibition(dtItem, itemcode, 4);
                                ConsoleWriteLine_Tofile("5.SaveLogExhibition : " + DateTime.Now);

                                DataTable dtImage = SelectLogExhibitionImage(4, itemcode);
                                ConsoleWriteLine_Tofile("6.GetImageList : " + DateTime.Now);

                                DataTable dtSelect = SelectLogExhibitionSelect(4, 4, itemcode);

                                DataTable dtInventory = new DataTable();
                                DataRow[] dr = dtSelect.Select("選択肢タイプ = 'i'");
                                if (dr.Count() > 0)
                                    dtInventory = dtSelect.Select("選択肢タイプ = 'i'").CopyToDataTable();
                                dtOption = new DataTable();
                                dr = dtSelect.Select("選択肢タイプ = 's'");

                                if (dr.Count() > 0)
                                    dtOption = dtSelect.Select("選択肢タイプ = 's'").CopyToDataTable();
                                dtCategory = SelectLogExhibitionCategory(4, 4, itemcode);


                                if (CtrlID.Equals("n"))
                                {
                                    UploadItem(dtItem, dtInventory, dtCategory, dtOption, dtImage, itemcode);
                                    SaveNewItemHistory(dtItemMaster.Rows[0]["Exhibit_ID"].ToString(), 4);
                                }
                                else
                                {
                                    UpdateItem(dtItem, dtInventory, dtCategory, dtOption, dtImage, itemcode);
                                }
                                SaveItemShopAPIInfo(Convert.ToInt32(dtItemMaster.Rows[0]["Exhibit_ID"]), 4, "APIitem$" + DateTime.Now.ToString("ddMMyyyy_HHmmss"), CtrlID);
                                ChangeIsGeneratedCSVFlag(Convert.ToInt32(itemcode), Convert.ToInt32(dtItem.Rows[0]["Exhibit_ID"]), 4);
                                ChangeCtrl_ID(Convert.ToInt32(dtItemMaster.Rows[0]["Exhibit_ID"]), Convert.ToInt32(dtItem.Rows[0]["Item_ID"]), 4, CtrlID);
                                AddExportedDate(4, Convert.ToInt32(dtItemMaster.Rows[0]["Exhibit_ID"]));
                            }
                        }
                    }
                    ChangeFlag(list, 4);            //update Exhibition_Item_Master set IsW/.._Collect=2
                    ConsoleWriteLine_Tofile("7.ChangeFlag : " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                //insert to system error log
                string err = "(16 進数値 0x08) は無効な文字です。";
                if (ex.ToString().Contains(err))
                {
                    Change_Wowma_ErrorCode_Flag(itemcode1);
                }
                Save_SYS_Errorlog(ex.ToString());
            }
        }
        static DataTable SelectExhibitionBlock(string Exhibit_ID)
        {
            DataTable dtBlock = new DataTable();
            string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(con);
            SqlDataAdapter sda = new SqlDataAdapter("SP_Get_Exhibition_Block", conn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.CommandTimeout = 0;
            sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", Exhibit_ID);
            sda.SelectCommand.Connection.Open();
            sda.Fill(dtBlock);
            sda.SelectCommand.Connection.Close();
            return dtBlock;
        }
        static void UploadItem(DataTable dtItem, DataTable dtInventory, DataTable dtCategory, DataTable dtOption, DataTable dtImage, string itemCode)
        {
            #region Image
            //if (dtImage != null && dtImage.Rows.Count > 0)
            //{
            //    string path = "";
            //    string folderName = "";
            //    string image_name = "";
            //    string sku = ExportCSVPath + "26" + "/item_img/";
            //    foreach (DataRow drImage in dtImage.Rows)
            //    {
            //        if (drImage["Image_Name"].ToString() != "")
            //        {
            //            image_name = drImage["Image_Name"].ToString();
            //            folderName = drImage["Folder_Name"].ToString();
            //            path = sku + "images" + "/";
            //            if (!Directory.Exists(path))
            //                Directory.CreateDirectory(path);
            //          //  Save image into folder
            //            if (Directory.Exists(path))
            //            {
            //                if (File.Exists(ItemImage + image_name))
            //                {
            //                    if (!File.Exists(path + image_name))
            //                        File.Copy(ItemImage + image_name, path + image_name);
            //                }
            //            }
            //        }
            //    }
            //    //if (Directory.Exists(sku))
            //    //{
            //    //    using (ZipFile zipfile = new ZipFile())
            //    //    {
            //    //        String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            //    //        zipfile.AddDirectory(sku);
            //    //        zipfile.Save(ExportCSVPath + "26" + "/" + folderName + "$" + "26" + "_" + date + ".zip");
            //    //        zipfile.Save(Bak_ExportCSVPath + folderName + "$" + "26" + "_" + date + ".zip");
            //    //        SaveItem_ExportQ(folderName + "$" + "26" + "_" + date + ".zip", 3, 26, 0, 1);
            //    //    }
            //    //    Directory.Delete(sku + folderName, true);
            //    //}
            //}
            #endregion


            //string xmlString = GetXMl_NewUpdate(dtItem, dtInventory, dtCategory, dtOption, itemCode, 0).Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");
            string xmlString = GetXMl_NewUpdate(dtItem, dtImage, dtInventory, dtCategory, dtOption, itemCode, 0);
            SaveXMLLog(xmlString, dtItem.Rows[0]["Item_Code"].ToString(), 4);
            int exhibitid = Convert.ToInt32(dtItem.Rows[0]["Exhibit_ID"]);
            string apiKey = dtAPIKey.Rows[0]["APIKey"].ToString();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://api.manager.wowma.jp/wmshopapi/registerItemInfo"));
            byte[] bytes;
            bytes = System.Text.Encoding.UTF8.GetBytes(xmlString);
            request.ContentType = "application/xml; charset=utf-8";
            request.ContentLength = bytes.Length;
            request.Method = "POST";

            request.Headers.Add("Authorization", "Bearer " + apiKey);

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    XmlDocument xDoc = new XmlDocument();
                    SaveAPI_Exhibition_Log(dtItem.Rows[0]["Item_Code"].ToString(), responseStr, Convert.ToInt32(dtItem.Rows[0]["Shop_ID"]));
                    xDoc.LoadXml(responseStr);
                    responseStr = xDoc.InnerText;
                    XmlNodeList list1 = xDoc.GetElementsByTagName("status");
                    if (list1[0].InnerText.Equals("0"))
                    {
                        exhibitionerror = "OK";

                        CheckErrorExists(4, exhibitid, exhibitionerror);
                        XmlNodeList wordList = xDoc.GetElementsByTagName("lotNumber");
                        if (wordList.Count != 0)
                        {
                            if (wordList[0].ChildNodes.Item(0) == null) { }
                            else
                            {

                                string lotNumber = wordList[0].ChildNodes.Item(0).InnerText.Trim();
                                if (lotNumber != null)
                                {
                                    InsertLotNumber(lotNumber, itemCode);
                                }
                            }
                        }
                    }

                    else
                    {
                        list1 = xDoc.GetElementsByTagName("message");
                        exhibitionerror = list1[0].InnerText;

                        if (!exhibitionerror.Contains("該当の商品は現在更新ができません。"))
                        {
                            CheckErrorExists(4, exhibitid, exhibitionerror);
                        }

                    }
                }
            }
            catch (WebException e)
            {
                using (WebResponse response1 = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response1;
                    using (Stream data = response1.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        if (text.Contains("message"))
                        {
                            XmlDocument xd = new XmlDocument();
                            xd.LoadXml(text);
                            text = xd.InnerText;
                            CheckErrorExists(4, exhibitid, text);
                        }
                    }
                }
            }
        }
        static void CheckErrorExists(int shopid, int exhibitid, string result)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Wowma_Error_Check", connectionstring);
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


        static void UpdateItem(DataTable dtItem, DataTable dtInventory, DataTable dtCategory, DataTable dtOption, DataTable dtImage, string itemCode)
        {
            #region Image
            //if (dtImage != null && dtImage.Rows.Count > 0)
            //{
            //    string path = "";
            //    string folderName = "";
            //    string image_name = "";
            //    string sku = ExportCSVPath + "26" +"/item_img/";
            //    foreach (DataRow drImage in dtImage.Rows)
            //    {
            //        if (drImage["Image_Name"].ToString() != "")
            //        {
            //            image_name = drImage["Image_Name"].ToString();
            //            //folderName = drImage["Folder_Name"].ToString();
            //         //   path = sku + folderName + "/";
            //            path = sku + "images/";
            //            if (!Directory.Exists(path))
            //                Directory.CreateDirectory(path);
            //            //Save image into folder
            //            if (Directory.Exists(path))
            //            {
            //                if (File.Exists(ItemImage + image_name))
            //                {
            //                    if (!File.Exists(path + image_name))
            //                        File.Copy(ItemImage + image_name, path + image_name);
            //                }
            //            }
            //        }
            //    }
            //    if (Directory.Exists(sku))
            //    {
            //        using (ZipFile zipfile = new ZipFile())
            //        {
            //            String date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            //            zipfile.AddDirectory(sku);
            //            zipfile.Save(ExportCSVPath + "26" + "/images" + "$" + "26" + "_" + date + ".zip");
            //            zipfile.Save(Bak_ExportCSVPath + "images" + "$" + "26" + "_" + date + ".zip");
            //            SaveItem_ExportQ("images" + "$" + "26" + "_" + date + ".zip", 3, 26, 0, 1);
            //        }

            //        Directory.Delete(sku + "images", true);
            //    }
            //}
            #endregion
         
            string xmlString = GetXMlUpdate(dtItem, dtImage, dtInventory, dtCategory, dtOption, itemCode, 1);
            SaveXMLLog(xmlString, dtItem.Rows[0]["Item_Code"].ToString(), 4);
            int exhibitid = Convert.ToInt32(dtItem.Rows[0]["Exhibit_ID"]);
            string apiKey = dtAPIKey.Rows[0]["APIKey"].ToString();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://api.manager.wowma.jp/wmshopapi/updateItemInfo"));
            byte[] bytes;
            bytes = System.Text.Encoding.UTF8.GetBytes(xmlString);
            request.ContentType = "application/xml; charset=utf-8";
            request.ContentLength = bytes.Length;
            request.Method = "POST";

            request.Headers.Add("Authorization", "Bearer " + apiKey);

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);

            requestStream.Close();
            HttpWebResponse response;

            try
            {

                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    XmlDocument xDoc = new XmlDocument();
                    SaveAPI_Exhibition_Log(dtItem.Rows[0]["Item_Code"].ToString(), responseStr, Convert.ToInt32(dtItem.Rows[0]["Shop_ID"]));
                    xDoc.LoadXml(responseStr);

                    XmlNodeList list1 = xDoc.GetElementsByTagName("status");
                    if (list1[0].InnerText.Equals("0"))
                    {
                        exhibitionerror = "OK";
                        CheckErrorExists(4, exhibitid, exhibitionerror);
                        XmlNodeList wordList = xDoc.GetElementsByTagName("lotNumber");
                        if (wordList.Count != 0)
                        {
                            if (wordList[0].ChildNodes.Item(0) == null) { }
                            else
                            {

                                string lotNumber = wordList[0].ChildNodes.Item(0).InnerText.Trim();
                                if (lotNumber != null)
                                {
                                    InsertLotNumber(lotNumber, itemCode);
                                }
                            }
                        }
                    }

                    else
                    {
                        list1 = xDoc.GetElementsByTagName("message");
                        exhibitionerror = list1[0].InnerText;
                        CheckErrorExists(4, exhibitid, exhibitionerror);

                    }

                }

            }
            catch (WebException e)
            {
                using (WebResponse response1 = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response1;
                    using (Stream data = response1.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        if (text.Contains("message"))
                        {
                            XmlDocument xd = new XmlDocument();
                            xd.LoadXml(text);
                            text = xd.InnerText;
                            CheckErrorExists(4, exhibitid, text);
                        }

                    }
                }
            }
        }

        static void UpdateImageBlockItem(DataTable dtItem, DataTable dtImage)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = Encoding.UTF8;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;
            XmlWriter writer = XmlWriter.Create(memoryStream, xmlWriterSettings);


            writer.WriteStartElement("request");
            writer.WriteElementString("shopId", "58726125");

            writer.WriteStartElement("updateItem");
            writer.WriteElementString("itemCode", dtItem.Rows[0]["Item_Code"].ToString());//商品番号
            writer.WriteElementString("itemPrice", dtItem.Rows[0]["Sale_Price"].ToString());//販売価格
            for (int i = 0; i < dtImage.Rows.Count; i++)
            {
                writer.WriteStartElement("images");
                writer.WriteElementString("imageUrl", "https://image.wowma.jp/58726125/images/" + dtImage.Rows[i]["Image_Name"].ToString());//項目選択肢別在庫用縦軸選択肢子番号
                                                                                                                                            // writer.WriteElementString("imageName", dtImage.Rows[i]["Image_Name"].ToString());
                writer.WriteElementString("imageSeq", Convert.ToString(i + 1));
                writer.WriteEndElement();
            }
            for (int N = dtImage.Rows.Count + 1; N < 21; N++)
            {
                writer.WriteStartElement("images");
                writer.WriteElementString("imageUrl", "NULL");
                // writer.WriteElementString("imageName", "NULL");
                writer.WriteElementString("imageSeq", Convert.ToString(N));
                writer.WriteEndElement();
            }
            writer.WriteElementString("limitedPasswd", "NULL"); //capital0013
            writer.WriteElementString("saleStatus", dtItem.Rows[0]["Warehouse_Specified"].ToString());//倉庫指定
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.Flush();
            string xmlString = Encoding.UTF8.GetString(memoryStream.ToArray());

            //   xmlString = GetXMl_NewUpdate(dtItem, dtImage, dtInventory, dtCategory, dtOption, itemCode, 1);
            SaveXMLLog(xmlString, dtItem.Rows[0]["Item_Code"].ToString(), 26);
            int exhibitid = Convert.ToInt32(dtItem.Rows[0]["Exhibit_ID"]);
            string apiKey = dtAPIKey.Rows[0]["APIKey"].ToString();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://api.manager.wowma.jp/wmshopapi/updateItemInfo"));
            byte[] bytes;
            bytes = System.Text.Encoding.UTF8.GetBytes(xmlString);
            request.ContentType = "application/xml; charset=utf-8";
            request.ContentLength = bytes.Length;
            request.Method = "POST";

            request.Headers.Add("Authorization", "Bearer " + apiKey);

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);

            requestStream.Close();
            HttpWebResponse response;

            try
            {

                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    XmlDocument xDoc = new XmlDocument();
                    SaveAPI_Exhibition_Log(dtItem.Rows[0]["Item_Code"].ToString(), responseStr, Convert.ToInt32(dtItem.Rows[0]["Shop_ID"]));
                    xDoc.LoadXml(responseStr);

                    XmlNodeList list1 = xDoc.GetElementsByTagName("status");
                    if (list1[0].InnerText.Equals("0"))
                    {
                        exhibitionerror = "OK";
                        CheckErrorExists(4, exhibitid, exhibitionerror);

                    }

                    else
                    {
                        list1 = xDoc.GetElementsByTagName("message");
                        exhibitionerror = list1[0].InnerText;
                        CheckErrorExists(4, exhibitid, exhibitionerror);

                    }

                }

            }
            catch (WebException e)
            {
                using (WebResponse response1 = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response1;
                    using (Stream data = response1.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        if (text.Contains("message"))
                        {
                            XmlDocument xd = new XmlDocument();
                            xd.LoadXml(text);
                            text = xd.InnerText;
                            CheckErrorExists(4, exhibitid, text);
                        }

                    }
                }
            }
        }
        static string GetXMlUpdate(DataTable dtItem, DataTable dtImage, DataTable dtInventory, DataTable dtCategory, DataTable dtOption, string itemCode, int mode)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = Encoding.UTF8;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;
            XmlWriter writer = XmlWriter.Create(memoryStream, xmlWriterSettings);

          
            writer.WriteStartElement("request");
            writer.WriteElementString("shopId", "58726125");
            #region Item
            if (mode == 0)
                writer.WriteStartElement("registerItem");
            else
                writer.WriteStartElement("updateItem");

            string Item_Name = dtItem.Rows[0]["Item_Name"].ToString();
            Item_Name = Regex.Replace(Item_Name, @"[;'""]|[ ]{2}", " ");
            writer.WriteElementString("itemName", Item_Name);//商品名
            writer.WriteElementString("itemCode", dtItem.Rows[0]["Item_Code"].ToString());//商品番号
            writer.WriteElementString("itemPrice", dtItem.Rows[0]["Sale_Price"].ToString());//販売価格
            writer.WriteElementString("sellMethodSegment", dtItem.Rows[0]["販売方法区分"].ToString());
            writer.WriteElementString("taxSegment", dtItem.Rows[0]["Wo_Consumption_Tax"].ToString());//消費税
            writer.WriteElementString("postageSegment", dtItem.Rows[0]["Wo_Special_Flag"].ToString());//送料
            writer.WriteElementString("stockRequestConfig", dtItem.Rows[0]["Wo_stockRequest"].ToString());//入荷リクエスト設定   
           // writer.WriteElementString("reducedTax", dtItem.Rows[0]["Item_TaxRate"].ToString());//入荷リクエスト設定
            string description = "<a href =\"/bep/m/mblmyp07_regist?exhibiter_id=58726125\">Paint&Toolをお気に入りの店として登録</a><br><a href=\"/user/58726125/plus/\">ショップトップページへ</a>";

            writer.WriteElementString("description", description); //PC用販売説明文  PC sales description
                                                                   //  writer.WriteElementString("description", dtItem.Rows[0]["Sale_Description_PC"].ToString()); //PC用販売説明文         
                                                                   // writer.WriteElementString("descriptionForSP", dtItem.Rows[0]["Smart_Template"].ToString());//スマートフォン用商品説明文
                                                                   // writer.WriteElementString("descriptionForPC", dtItem.Rows[0]["Item_Description_PC"].ToString());//PC用商品説明文

            writer.WriteElementString("descriptionForSP", dtItem.Rows[0]["Smart_Template"].ToString());//スマートフォン用商品説明文
            writer.WriteElementString("descriptionForPC", dtItem.Rows[0]["Item_Description_PC"].ToString());//PC用商品説明文


            //miemie searchkeywords
            #region SearchKeyWord
            //String Search_Word = dtItem.Rows[0]["Search_Word"].ToString().Trim();
            //string[] search = Search_Word.Split(' ');
            //if (search.Length > 0)
            //{
            //    for (int i = 0; i < search.Length; i++)
            //    {
            //        if (i < 3)
            //        {
            //            int seq = i + 1;
            //            writer.WriteStartElement("searchKeywords");
            //            writer.WriteElementString("searchKeyword", search[i].ToString());
            //            writer.WriteElementString("searchKeywordSeq", seq.ToString());
            //            writer.WriteEndElement();
            //        }
            //    }
            //}
            //for (int N = search.Length + 1; N < 4; N++)
            //{
            //    writer.WriteStartElement("searchKeywords");
            //    writer.WriteElementString("searchKeyword", "");
            //    writer.WriteElementString("searchKeywordSeq", N.ToString());
            //    writer.WriteEndElement();
            //}
            #endregion

            writer.WriteElementString("categoryId", dtItem.Rows[0]["Wo_CategoryID"].ToString());
            writer.WriteElementString("itemModel", dtItem.Rows[0]["Model_No"].ToString());
            writer.WriteElementString("limitedPasswd", "NULL");   //capital0013  
            writer.WriteElementString("saleStatus", dtItem.Rows[0]["Warehouse_Specified"].ToString());//倉庫指定


            #region Delivery
            if (!String.IsNullOrWhiteSpace("2"))
            {
               DataTable dtWowDelivery = GetDelivery_MethodID("2");
                for (int i = 0; i < dtWowDelivery.Rows.Count; i++)
                {
                    writer.WriteStartElement("deliveryMethod");
                    writer.WriteElementString("deliveryMethodId", dtWowDelivery.Rows[i]["Delivery_Method_ID"].ToString());//項目選択肢別在庫用縦軸選択肢子番号 "58726125"
                    writer.WriteElementString("deliveryMethodSeq", Convert.ToString(i + 1));
                    writer.WriteEndElement();
                }
                if (mode != 0)
                {
                    for (int N = dtWowDelivery.Rows.Count + 1; N < 6; N++)
                    {
                        writer.WriteStartElement("deliveryMethod");
                        writer.WriteElementString("deliveryMethodId", "NULL");
                        writer.WriteElementString("deliveryMethodSeq", Convert.ToString(N));
                        writer.WriteEndElement();
                    }
                }
            }

            #endregion

            #region Option
            if (dtOption.Rows.Count > 0)
            {
                dtOption.Columns["Select/Checkbox用項目名"].ColumnName = "OptionName";
                dtOption.Columns["Select/Checkbox用選択肢"].ColumnName = "OptionValue";
           
                int T = 0;
                for (int i = 0; i < dtOption.Rows.Count; i++)
                {
                    writer.WriteStartElement("itemOptions");
                    writer.WriteElementString("itemOptionTitle", dtOption.Rows[i]["OptionName"].ToString());
                    DataTable dtTemp = dtOption.Select("OptionName = '" + dtOption.Rows[i]["OptionName"].ToString() + "'").CopyToDataTable();
                    String str = "";

                    for (int j = 0; j < dtTemp.Rows.Count; j++)
                    {
                        string strTemp = dtTemp.Rows[j]["OptionValue"].ToString() + "\\r\\n";
                        str = string.Concat(str, strTemp);

                    }

                    writer.WriteElementString("itemOption", str);
                    writer.WriteElementString("itemOptionSeq", Convert.ToString(++T));
                    i += dtTemp.Rows.Count - 1;
                    writer.WriteEndElement();
                }
                if (mode != 0)
                {
                    int count = dtOption.AsEnumerable().Select(r => r.Field<string>("OptionName")).Distinct().Count();
                    for (int N = count + 1; N < 21; N++)
                    {
                        writer.WriteStartElement("itemOptions");
                        writer.WriteElementString("itemOptionTitle", "NULL");
                        writer.WriteElementString("itemOption", "NULL");
                        writer.WriteElementString("itemOptionSeq", Convert.ToString(N));
                        writer.WriteEndElement();
                    }
                }

            }
            else
            {
                if (mode != 0)
                {
                    for (int N = 1; N < 21; N++)
                    {
                        writer.WriteStartElement("itemOptions");
                        writer.WriteElementString("itemOptionTitle", "NULL");
                        writer.WriteElementString("itemOption", "NULL");
                        writer.WriteElementString("itemOptionSeq", Convert.ToString(N));
                        writer.WriteEndElement();
                    }
                }
            }
            #endregion

            writer.WriteEndElement();



            #endregion


            #region Inventory
            if (mode == 0)
                writer.WriteStartElement("registerStock");
            else
                writer.WriteStartElement("updateStock");


            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["Wo_Stock_Type"].ToString()))
            {
                if (dtItem.Rows[0]["Wo_Stock_Type"].ToString() == "1")
                {
                    writer.WriteElementString("stockSegment", dtItem.Rows[0]["Wo_Stock_Type"].ToString());//在庫管理方法
                    DataTable dtInventories = dtInventory.Copy();
                    writer.WriteElementString("stockCount", dtInventory.Rows[0]["項目選択肢別在庫用在庫数"].ToString());//在庫数
                }
                else
                {
                    writer.WriteElementString("stockSegment", dtItem.Rows[0]["Wo_Stock_Type"].ToString());//在庫タイプ

                    writer.WriteElementString("choicesStockHorizontalItemName", dtItem.Rows[0]["Wo_Horizontal_ItemName"].ToString());//項目選択肢別在庫用横軸項目名

                    DataTable dtInventories = dtInventory.Copy();
                    dtInventories = RemoveDuplicateRows(dtInventories, "項目選択肢別在庫用横軸選択肢子番号");


                    for (int i = 0; i < dtInventories.Rows.Count; i++)
                    {
                        writer.WriteStartElement("choicesStockHorizontals");
                        writer.WriteElementString("choicesStockHorizontalCode", dtInventories.Rows[i]["項目選択肢別在庫用横軸選択肢子番号"].ToString());//項目選択肢別在庫用横軸選択肢子番号
                        writer.WriteElementString("choicesStockHorizontalName", dtInventories.Rows[i]["項目選択肢別在庫用横軸選択肢"].ToString());//項目選択肢別在庫用横軸選択肢                
                        writer.WriteElementString("choicesStockHorizontalSeq", Convert.ToString(i + 1));
                        writer.WriteEndElement();
                    }

                    writer.WriteElementString("choicesStockVerticalItemName", dtItem.Rows[0]["Wo_Vertical_ItemName"].ToString());//項目選択肢別在庫用縦軸項目名

                    DataTable dtVerticalInventories = dtInventory.Copy();
                    dtVerticalInventories = RemoveDuplicateRows(dtVerticalInventories, "項目選択肢別在庫用縦軸選択肢子番号");
                    for (int i = 0; i < dtVerticalInventories.Rows.Count; i++)
                    {
                        writer.WriteStartElement("choicesStockVerticals");
                        writer.WriteElementString("choicesStockVerticalCode", dtVerticalInventories.Rows[i]["項目選択肢別在庫用縦軸選択肢子番号"].ToString());//項目選択肢別在庫用縦軸選択肢子番号
                        writer.WriteElementString("choicesStockVerticalName", dtVerticalInventories.Rows[i]["項目選択肢別在庫用縦軸選択肢"].ToString());//項目選択肢別在庫用縦軸選択肢
                        writer.WriteElementString("choicesStockVerticalSeq", Convert.ToString(i + 1));
                        writer.WriteEndElement();
                    }


                    for (int i = 0; i < dtInventory.Rows.Count; i++)
                    {
                        writer.WriteStartElement("choicesStocks");
                        writer.WriteElementString("choicesStockHorizontalCode", dtInventory.Rows[i]["項目選択肢別在庫用横軸選択肢子番号"].ToString());//項目選択肢別在庫用横軸選択肢子番号
                        writer.WriteElementString("choicesStockVerticalCode", dtInventory.Rows[i]["項目選択肢別在庫用縦軸選択肢子番号"].ToString());//項目選択肢別在庫用縦軸選択肢子番号
                        if (Convert.ToInt32(dtInventory.Rows[i]["項目選択肢別在庫用在庫数"]) >= 1000)
                        {
                            writer.WriteElementString("choicesStockCount", "999");
                        }
                        else
                        {
                            writer.WriteElementString("choicesStockCount", dtInventory.Rows[i]["項目選択肢別在庫用在庫数"].ToString());
                        }
                        // writer.WriteElementString("choicesStockShippingDayId", dtInventory.Rows[i]["在庫あり時納期管理番号"].ToString());

                        writer.WriteEndElement();
                    }

                    writer.WriteElementString("choicesStockUpperDescription", "在庫下説明");
                    writer.WriteElementString("choicesStockLowerDescription", "在庫下説明");
                    writer.WriteElementString("displayChoicesStockSegment", "1");
                    writer.WriteElementString("displayChoicesStockThreshold", "");
                    writer.WriteElementString("displayBackorderMessage", "Message");
                }
            }


            #endregion

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            return Encoding.UTF8.GetString(memoryStream.ToArray());

        }

        static string GetXMl_NewUpdate(DataTable dtItem, DataTable dtImage, DataTable dtInventory, DataTable dtCategory, DataTable dtOption, string itemCode, int mode)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = Encoding.UTF8;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;
            XmlWriter writer = XmlWriter.Create(memoryStream, xmlWriterSettings);

          
            writer.WriteStartElement("request");
            writer.WriteElementString("shopId", "58726125");
           
            if (mode == 0)
                writer.WriteStartElement("registerItem");
            else
                writer.WriteStartElement("updateItem");
            #region Item
            string Item_Name = dtItem.Rows[0]["Item_Name"].ToString();
            Item_Name = Regex.Replace(Item_Name, @"[;'""]|[ ]{2}", " ");
            writer.WriteElementString("itemName", Item_Name);//商品名
            writer.WriteElementString("itemCode", dtItem.Rows[0]["Item_Code"].ToString());//商品番号
            writer.WriteElementString("itemPrice", dtItem.Rows[0]["Sale_Price"].ToString());//販売価格
            writer.WriteElementString("sellMethodSegment", dtItem.Rows[0]["販売方法区分"].ToString());

            writer.WriteElementString("taxSegment", dtItem.Rows[0]["Wo_Consumption_Tax"].ToString());//消費税
            writer.WriteElementString("postageSegment", dtItem.Rows[0]["Wo_Special_Flag"].ToString());//送料

            writer.WriteElementString("stockRequestConfig", dtItem.Rows[0]["Wo_stockRequest"].ToString());//入荷リクエスト設定       
            //writer.WriteElementString("reducedTax", dtItem.Rows[0]["Item_TaxRate"].ToString());//入荷リクエスト設定
            string description = "<a href =\"/bep/m/mblmyp07_regist?exhibiter_id=58726125\">Paint&Toolをお気に入りの店として登録</a><br><a href=\"/user/58726125/plus/\">ショップトップページへ</a>";

            writer.WriteElementString("description", description); //PC用販売説明文  PC sales description
                                                                   //  writer.WriteElementString("description", dtItem.Rows[0]["Sale_Description_PC"].ToString()); //PC用販売説明文         
                                                                   //writer.WriteElementString("descriptionForSP", dtItem.Rows[0]["Smart_Template"].ToString());//スマートフォン用商品説明文
                                                                   //writer.WriteElementString("descriptionForPC", dtItem.Rows[0]["Item_Description_PC"].ToString());//PC用商品説明文
            writer.WriteElementString("descriptionForSP", dtItem.Rows[0]["Smart_Template"].ToString());//スマートフォン用商品説明文
            writer.WriteElementString("descriptionForPC", dtItem.Rows[0]["Item_Description_PC"].ToString());//PC用商品説明文

            //miemie searchkeywords
            #region SearchKeyWord
            //String Search_Word = dtItem.Rows[0]["Search_Word"].ToString().Trim();
            //string[] search = Search_Word.Split(' ');
            //if (search.Length > 0)
            //{
            //    for (int i = 0; i < search.Length; i++)
            //    {
            //        if (i < 3)
            //        {
            //            int seq = i + 1;
            //            writer.WriteStartElement("searchKeywords");
            //            writer.WriteElementString("searchKeyword", search[i].ToString());
            //            writer.WriteElementString("searchKeywordSeq", seq.ToString());
            //            writer.WriteEndElement();
            //        }
            //    }
            //}
            //for (int N = search.Length + 1; N < 4; N++)
            //{
            //    writer.WriteStartElement("searchKeywords");
            //    writer.WriteElementString("searchKeyword", "");
            //    writer.WriteElementString("searchKeywordSeq", N.ToString());
            //    writer.WriteEndElement();
            //}
            #endregion

            for (int i = 0; i < dtImage.Rows.Count; i++)
            {
                writer.WriteStartElement("images");
                writer.WriteElementString("imageUrl", "https://image.wowma.jp/58726125/images/" + dtImage.Rows[i]["Image_Name"].ToString());//項目選択肢別在庫用縦軸選択肢子番号
                                                                                                                                            // writer.WriteElementString("imageName", dtImage.Rows[i]["Image_Name"].ToString());
                writer.WriteElementString("imageSeq", Convert.ToString(i + 1));
                writer.WriteEndElement();
            }
            for (int N = dtImage.Rows.Count + 1; N < 21; N++)
            {
                writer.WriteStartElement("images");
                writer.WriteElementString("imageUrl", "NULL");
                // writer.WriteElementString("imageName", "NULL");
                writer.WriteElementString("imageSeq", Convert.ToString(N));
                writer.WriteEndElement();
            }



            writer.WriteElementString("categoryId", dtItem.Rows[0]["Wo_CategoryID"].ToString());
            writer.WriteElementString("itemModel", dtItem.Rows[0]["Model_No"].ToString());
            writer.WriteElementString("limitedPasswd", "NULL");   //capital0013  
            writer.WriteElementString("saleStatus", dtItem.Rows[0]["Warehouse_Specified"].ToString());//倉庫指定

            #region Delivery
            if (!String.IsNullOrWhiteSpace("2"))
            {
                DataTable dtWowDelivery = GetDelivery_MethodID("2");
                for (int i = 0; i < dtWowDelivery.Rows.Count; i++)
                {
                    writer.WriteStartElement("deliveryMethod");
                    writer.WriteElementString("deliveryMethodId", dtWowDelivery.Rows[i]["Delivery_Method_ID"].ToString());//項目選択肢別在庫用縦軸選択肢子番号"58726125"
                    writer.WriteElementString("deliveryMethodSeq", Convert.ToString(i + 1));
                    writer.WriteEndElement();
                }
                if (mode != 0)
                {
                    for (int N = dtWowDelivery.Rows.Count + 1; N < 6; N++)
                    {
                        writer.WriteStartElement("deliveryMethod");
                        writer.WriteElementString("deliveryMethodId", "NULL");
                        writer.WriteElementString("deliveryMethodSeq", Convert.ToString(N));
                        writer.WriteEndElement();
                    }
                }
            }
            #endregion

            #region Option
            if (dtOption.Rows.Count > 0)
            {
                dtOption.Columns["Select/Checkbox用項目名"].ColumnName = "OptionName";
                dtOption.Columns["Select/Checkbox用選択肢"].ColumnName = "OptionValue";


                int T = 0;
                for (int i = 0; i < dtOption.Rows.Count; i++)
                {
                    writer.WriteStartElement("itemOptions");
                    writer.WriteElementString("itemOptionTitle", dtOption.Rows[i]["OptionName"].ToString());
                    DataTable dtTemp = dtOption.Select("OptionName = '" + dtOption.Rows[i]["OptionName"].ToString() + "'").CopyToDataTable();
                    String str = "";

                    for (int j = 0; j < dtTemp.Rows.Count; j++)
                    {
                        string strTemp = dtTemp.Rows[j]["OptionValue"].ToString() + "\\r\\n";
                        str = string.Concat(str, strTemp);

                    }

                    writer.WriteElementString("itemOption", str);
                    writer.WriteElementString("itemOptionSeq", Convert.ToString(++T));
                    i += dtTemp.Rows.Count - 1;
                    writer.WriteEndElement();
                }
                if (mode != 0)
                {
                    int count = dtOption.AsEnumerable().Select(r => r.Field<string>("OptionName")).Distinct().Count();
                    for (int N = count + 1; N < 21; N++)
                    {
                        writer.WriteStartElement("itemOptions");
                        writer.WriteElementString("itemOptionTitle", "NULL");
                        writer.WriteElementString("itemOption", "NULL");
                        writer.WriteElementString("itemOptionSeq", Convert.ToString(N));
                        writer.WriteEndElement();
                    }
                }

            }
            else
            {
                if (mode != 0)
                {
                    for (int N = 1; N < 21; N++)
                    {
                        writer.WriteStartElement("itemOptions");
                        writer.WriteElementString("itemOptionTitle", "NULL");
                        writer.WriteElementString("itemOption", "NULL");
                        writer.WriteElementString("itemOptionSeq", Convert.ToString(N));
                        writer.WriteEndElement();
                    }
                }
            }
            #endregion

            writer.WriteEndElement();



            #endregion


            #region Inventory
            if (mode == 0)
                writer.WriteStartElement("registerStock");
            else
                writer.WriteStartElement("updateStock");

            if (!string.IsNullOrWhiteSpace(dtItem.Rows[0]["Wo_Stock_Type"].ToString()))
            {
                if (dtItem.Rows[0]["Wo_Stock_Type"].ToString() == "1")
                {
                    writer.WriteElementString("stockSegment", dtItem.Rows[0]["Wo_Stock_Type"].ToString());//在庫管理方法
                    DataTable dtInventories = dtInventory.Copy();
                    writer.WriteElementString("stockCount", dtInventory.Rows[0]["項目選択肢別在庫用在庫数"].ToString());//在庫数
                }
                else
                {
                    writer.WriteElementString("stockSegment", dtItem.Rows[0]["Wo_Stock_Type"].ToString());//在庫タイプ

                    writer.WriteElementString("choicesStockHorizontalItemName", dtItem.Rows[0]["Wo_Horizontal_ItemName"].ToString());//項目選択肢別在庫用横軸項目名

                    DataTable dtInventories = dtInventory.Copy();
                    dtInventories = RemoveDuplicateRows(dtInventories, "項目選択肢別在庫用横軸選択肢子番号");


                    for (int i = 0; i < dtInventories.Rows.Count; i++)
                    {
                        writer.WriteStartElement("choicesStockHorizontals");
                        writer.WriteElementString("choicesStockHorizontalCode", dtInventories.Rows[i]["項目選択肢別在庫用横軸選択肢子番号"].ToString());//項目選択肢別在庫用横軸選択肢子番号
                        writer.WriteElementString("choicesStockHorizontalName", dtInventories.Rows[i]["項目選択肢別在庫用横軸選択肢"].ToString());//項目選択肢別在庫用横軸選択肢                
                        writer.WriteElementString("choicesStockHorizontalSeq", Convert.ToString(i + 1));
                        writer.WriteEndElement();
                    }

                    writer.WriteElementString("choicesStockVerticalItemName", dtItem.Rows[0]["Wo_Vertical_ItemName"].ToString());//項目選択肢別在庫用縦軸項目名

                    DataTable dtVerticalInventories = dtInventory.Copy();
                    dtVerticalInventories = RemoveDuplicateRows(dtVerticalInventories, "項目選択肢別在庫用縦軸選択肢子番号");
                    for (int i = 0; i < dtVerticalInventories.Rows.Count; i++)
                    {
                        writer.WriteStartElement("choicesStockVerticals");
                        writer.WriteElementString("choicesStockVerticalCode", dtVerticalInventories.Rows[i]["項目選択肢別在庫用縦軸選択肢子番号"].ToString());//項目選択肢別在庫用縦軸選択肢子番号
                        writer.WriteElementString("choicesStockVerticalName", dtVerticalInventories.Rows[i]["項目選択肢別在庫用縦軸選択肢"].ToString());//項目選択肢別在庫用縦軸選択肢
                        writer.WriteElementString("choicesStockVerticalSeq", Convert.ToString(i + 1));
                        writer.WriteEndElement();
                    }


                    for (int i = 0; i < dtInventory.Rows.Count; i++)
                    {
                        writer.WriteStartElement("choicesStocks");
                        writer.WriteElementString("choicesStockHorizontalCode", dtInventory.Rows[i]["項目選択肢別在庫用横軸選択肢子番号"].ToString());//項目選択肢別在庫用横軸選択肢子番号
                        writer.WriteElementString("choicesStockVerticalCode", dtInventory.Rows[i]["項目選択肢別在庫用縦軸選択肢子番号"].ToString());//項目選択肢別在庫用縦軸選択肢子番号
                        if (Convert.ToInt32(dtInventory.Rows[i]["項目選択肢別在庫用在庫数"]) >= 1000)
                        {
                            writer.WriteElementString("choicesStockCount", "999");
                        }
                        else
                        {
                            writer.WriteElementString("choicesStockCount", dtInventory.Rows[i]["項目選択肢別在庫用在庫数"].ToString());
                        }
                        // writer.WriteElementString("choicesStockShippingDayId", dtInventory.Rows[i]["在庫あり時納期管理番号"].ToString());

                        writer.WriteEndElement();
                    }

                    writer.WriteElementString("choicesStockUpperDescription", "在庫下説明");
                    writer.WriteElementString("choicesStockLowerDescription", "在庫下説明");
                    writer.WriteElementString("displayChoicesStockSegment", "1");
                    writer.WriteElementString("displayChoicesStockThreshold", "");
                    writer.WriteElementString("displayBackorderMessage", "Message");
                }                
            }
                


            #endregion


            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            return Encoding.UTF8.GetString(memoryStream.ToArray());

        }

        public static void SaveItem_ExportQ(string FileName, int FileType, int ShopID, int IsExport, int Export_Type)
        {
            SqlConnection connectionstring = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Item_ExportQ_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionstring;
                cmd.Parameters.AddWithValue("@File_Name", FileName);
                cmd.Parameters.AddWithValue("@File_Type", FileType);
                cmd.Parameters.AddWithValue("@ShopID", ShopID);
                cmd.Parameters.AddWithValue("@IsExport", IsExport);
                cmd.Parameters.AddWithValue("@Export_Type", Export_Type);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        static DataTable SelectLogExhibitionSelect(int shop_id, int mall_id, string itemID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Select_SelectByShop_Wowma_API", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID", mall_id);
                sda.SelectCommand.Parameters.AddWithValue("@itemID", itemID);
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

        static DataTable SelectLogExhibitionCategory(int shop_id, int mall_id, string itemID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Category_SelectByShop_Wowma_API", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID", mall_id);
                sda.SelectCommand.Parameters.AddWithValue("@itemID", itemID);
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
        static DataTable GetItemData(string list, int shop_id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_List_CollectData_Wowma_API", conn);
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
        public static void SaveLogWarehouseItem(DataTable dtWarehouse)
        {
            try
            {
                dtWarehouse.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dtWarehouse.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_SaveLogWarehouseItem_Wowma", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@xml", SqlDbType.Xml).Value = result;
                cmd.Parameters.AddWithValue("@shopID", "4");
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveLogImageCatItem(DataTable dt)
        {
            try
            {
                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Log_ImageCat_Insert_Wowma_API", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@xml", SqlDbType.Xml).Value = result;
                //cmd.Parameters.AddWithValue("@itemcode", dtWarehouse.Rows[0]["Item_Code"].ToString());
                //cmd.Parameters.AddWithValue("@itemname", dtWarehouse.Rows[0]["Item_Name"].ToString());
                //cmd.Parameters.AddWithValue("@saleprice", dtWarehouse.Rows[0]["Sale_Price"].ToString());
                //cmd.Parameters.AddWithValue("@warehouseFlag", dtWarehouse.Rows[0]["Warehouse_Specified"].ToString());
                cmd.Parameters.AddWithValue("@shopID", "4");
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static void UploadWarehouse(DataTable dtwarehouse, string itemCode)
        {
            string xmlString = GetXMl_WarehouseUpdate(dtwarehouse);
            SaveXMLLog(xmlString, dtwarehouse.Rows[0]["Item_Code"].ToString(), 26);
            string apiSecret = dtAPIKey.Rows[0]["APISecret"].ToString();

            int exhibitid = Convert.ToInt32(dtwarehouse.Rows[0]["Exhibit_ID"]);
            string apiKey = dtAPIKey.Rows[0]["APIKey"].ToString();
            string link = "";
            if (CtrlID.Equals("n"))
            {
                link = "https://api.manager.wowma.jp/wmshopapi/registerItemInfo";
            }
            else
            {
                link = "https://api.manager.wowma.jp/wmshopapi/updateItemInfo";
            }
            // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://api.manager.wowma.jp/wmshopapi/updateItemInfo"));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format(link));
            byte[] bytes;
            bytes = System.Text.Encoding.UTF8.GetBytes(xmlString);
            request.ContentType = "application/xml; charset=utf-8";
            request.ContentLength = bytes.Length;
            request.Method = "POST";

            request.Headers.Add("Authorization", "Bearer " + apiKey);

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);

            requestStream.Close();
            HttpWebResponse response;

            try
            {

                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(responseStr);
                    SaveAPI_Exhibition_Log(dtwarehouse.Rows[0]["Item_Code"].ToString(), responseStr, 4);
                    XmlNodeList list1 = xDoc.GetElementsByTagName("status");
                    if (list1[0].InnerText.Equals("0"))
                    {
                        exhibitionerror = "OK";
                        CheckErrorExists(4, exhibitid, exhibitionerror);
                        XmlNodeList wordList = xDoc.GetElementsByTagName("lotNumber");
                        if (wordList.Count != 0)
                        {
                            if (wordList[0].ChildNodes.Item(0) == null) { }
                            else
                            {

                                string lotNumber = wordList[0].ChildNodes.Item(0).InnerText.Trim();
                                if (lotNumber != null)
                                {
                                    InsertLotNumber(lotNumber, itemCode);
                                }
                            }
                        }
                    }

                    else
                    {
                        list1 = xDoc.GetElementsByTagName("message");
                        exhibitionerror = list1[0].InnerText;
                        if (!(exhibitionerror.Contains("該当の商品は現在更新ができません。") || exhibitionerror.Contains("対象のデータが存在しません。")))
                        {
                            CheckErrorExists(4, exhibitid, exhibitionerror);
                        }


                    }

                }

            }
            catch (WebException e)
            {
                using (WebResponse response1 = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response1;
                    using (Stream data = response1.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        if (text.Contains("message"))
                        {
                            XmlDocument xd = new XmlDocument();
                            xd.LoadXml(text);
                            text = xd.InnerText;
                            if (text.Contains("該当の商品は現在更新ができません。") || text.Contains("対象のデータが存在しません。"))
                            {
                                text = "OK";
                                CheckErrorExists(4, exhibitid, text);
                            }
                            else
                            {
                                CheckErrorExists(4, exhibitid, text);
                            }

                            //CheckErrorExists(26, exhibitid, text);
                        }

                    }
                }
            }
        }

        static string GetXMl_WarehouseUpdate(DataTable dtwarehouse)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = Encoding.UTF8;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;
            XmlWriter writer = XmlWriter.Create(memoryStream, xmlWriterSettings);

            //#region Item
            writer.WriteStartElement("request");
            writer.WriteElementString("shopId", "58726125");
            if (CtrlID.Equals("n"))
            {
                writer.WriteStartElement("registerItem");
            }
            else
            {

                writer.WriteStartElement("updateItem");
            }
            if (CtrlID.Equals("n"))
            {
                writer.WriteElementString("itemName", dtwarehouse.Rows[0]["Item_Name"].ToString());//商品名
            }

            writer.WriteElementString("itemCode", dtwarehouse.Rows[0]["Item_Code"].ToString());//商品番号
            writer.WriteElementString("itemPrice", dtwarehouse.Rows[0]["Sale_Price"].ToString());//販売価格

            if (CtrlID.Equals("n"))
            {
                writer.WriteElementString("taxSegment", dtwarehouse.Rows[0]["Wo_Consumption_Tax"].ToString());//消費税
                writer.WriteElementString("postageSegment", dtwarehouse.Rows[0]["Wo_Special_Flag"].ToString());//送料設定
                string description = "<a href =\"/bep/m/mblmyp07_regist?exhibiter_id=58726125\">Paint&Toolをお気に入りの店として登録</a><br><a href=\"/user/58726125/plus/\">ショップトップページへ</a>";

                writer.WriteElementString("description", description); //商品説明（共通）
                writer.WriteElementString("categoryId", dtwarehouse.Rows[0]["Wo_CategoryID"].ToString()); //カテゴリID

            }
            writer.WriteElementString("limitedPasswd", "NULL"); //capital0013
            writer.WriteElementString("saleStatus", dtwarehouse.Rows[0]["Warehouse_Specified"].ToString());//販売ステータス
            writer.WriteEndElement();
            if (CtrlID.Equals("n"))
            {
                writer.WriteStartElement("registerStock");
                writer.WriteElementString("stockSegment", "1");//在庫管理方法
                writer.WriteElementString("stockCount", "0");//在庫数
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.Flush();
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        static void SaveNewItemHistory(string exhibitID, int shopID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_SaveNewItemHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@exhibitID", exhibitID);
                cmd.Parameters.AddWithValue("@shopID", shopID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void CheckItemCodeForNewUpdate(string ItemList)
        {
            string itemID = ItemList.Trim();
            DataTable dt = GetData(itemID, 4);

            if (dt.Rows.Count > 0)
            {
                string apiKey = dt.Rows[0]["APIKey"].ToString();
                string apiSecret = dt.Rows[0]["APISecret"].ToString();
                string itemCode = dt.Rows[0]["Item_Code"].ToString();
                var webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://api.manager.wowma.jp/wmshopapi/searchItemInfo?shopId={0}&itemCode={1}", "58726125", itemCode));
                webRequest.Method = "GET";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Headers.Add("Authorization", "Bearer " + apiKey);


                WebResponse webRes = webRequest.GetResponse();
                XmlDocument resultXml = new XmlDocument();
                using (StreamReader reader = new StreamReader(webRes.GetResponseStream()))
                {
                    resultXml.Load(reader);
                }


                
                XmlNodeList wordList = resultXml.GetElementsByTagName("itemCode");

                if (wordList.Count == 0)
                {
                    CtrlID = "n";
                }
                else
                {

                    string str = wordList[0].ChildNodes.Item(0).InnerText.Trim();
                    CtrlID = "u";
                }
            }
        }

        static DataTable GetData(String ItemID, int ShopID)
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand("SELECT S.APIKey,S.APISecret,S.ID AS ShopID,S.Shop_ID AS ShopUrl,IM.Item_Code,IM.ID from Shop S,Item_Master IM WHERE IM.ID ='" + ItemID + "' AND S.ID =" + ShopID, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        static string SelectExhibitionItemID()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_List_SelectItem_IDList_New", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", 4);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt.Rows[0]["Item_ID"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region change flag

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

        static void ChangeCtrl_ID(int Exhibit_ID, int Item_ID, int Shop_ID, string ctrlid)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Shop_ChangeCtrl_ID_API_Wowma", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Exhibit_ID", Exhibit_ID);
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


        static void SaveItemShopAPIInfo(int Exhibit_ID, int shopID, string csvName, string ctrl_id)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Exhibition_Item_Shop_UpdateInfo_API", connectionstring);
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

        #endregion
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
                    SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_Insert_Wowma_API");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Connection = conn;
                    result = result.Replace("&#", "$ORS$");
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

        public static void SaveAPI_Exhibition_Log(string item_code, string msg, int Shop_ID)
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_API_Exhibition_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item_Code", item_code);
                cmd.Parameters.AddWithValue("@Error_Msg", msg);
                cmd.Parameters.AddWithValue("@Shop_ID", Shop_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            { throw ex; }
        }

        static void SaveXMLLog(string xml, string Item_Code, int shop_id)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_XML_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@xml", xml);
                cmd.Parameters.AddWithValue("@itemcode", Item_Code);
                cmd.Parameters.AddWithValue("@shopID", shop_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static DataTable GetAPIKey(int shopID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetAPIKey", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shopID);
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

        static DataTable GetDelivery_MethodID(string Delivery_SetName)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Select_WowDeliveryMethodID", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Delivery_SetName_ID", Delivery_SetName);
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

        public static void DeleteDirectory(string path, bool recursive)
        {
            // Delete all files and sub-folders?
            if (recursive)
            {
                // Yep... Let's do this
                var subfolders = Directory.GetDirectories(path);
                foreach (var s in subfolders)
                {
                    DeleteDirectory(s, recursive);
                }
            }

            // Get all files of the folder
            var files = Directory.GetFiles(path);
            foreach (var f in files)
            {
                // Get the attributes of the file
                var attr = File.GetAttributes(f);

                // Is this file marked as 'read-only'?
                if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // Yes... Remove the 'read-only' attribute, then
                    File.SetAttributes(f, attr ^ FileAttributes.ReadOnly);
                }
                // Delete the file
                File.Delete(f);
            }
            // When we get here, all the files of the folder were
            // already deleted, so we just delete the empty folder   
            Directory.Delete(path);
        }

        static DataTable RemoveDuplicateRows(DataTable table, string DistinctColumn)
        {
            try
            {
                ArrayList UniqueRecords = new ArrayList();
                ArrayList DuplicateRecords = new ArrayList();

                // Check if records is already added to UniqueRecords otherwise,
                // Add the records to DuplicateRecords
                foreach (DataRow dRow in table.Rows)
                {
                    if (UniqueRecords.Contains(dRow[DistinctColumn]))
                        DuplicateRecords.Add(dRow);
                    else
                        UniqueRecords.Add(dRow[DistinctColumn]);
                }

                // Remove duplicate rows from DataTable added to DuplicateRecords
                foreach (DataRow dRow in DuplicateRecords)
                {
                    table.Rows.Remove(dRow);
                }

                // Return the clean DataTable which contains unique records.
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static void Save_SYS_Errorlog(string error)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", -1);
            cmd.Parameters.AddWithValue("@ErrorDetail", "WowmaAPIRacket" + error);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        static void Change_Wowma_ErrorCode_Flag(string itemcode)
        {

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_ChangeCategoryFlag", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Itemcode", itemcode);
                cmd.Parameters.AddWithValue("@Shop_ID", 26);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "API_WowmaRacket.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
        static void InsertLotNumber(string lotNumber, string Item_Code)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_InsertLotNumber", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@lotNumber", lotNumber);
                cmd.Parameters.AddWithValue("@Item_Code", Item_Code);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}