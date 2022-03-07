using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Web;


namespace ORS_RCM_BL
{
   public  class Import_Item_Data_BL
    {
      Import_Item_Data_DL impdl;

       public Import_Item_Data_BL()
       {
           impdl = new Import_Item_Data_DL();
       }

       public string  SaleDescInsert(DataTable dt)
       {
           return impdl.SaleDescInsert(dt);
       }

       public DataTable  itemid(string itemcode) 
       {
        return    impdl.SelectItemID(itemcode);
       }

       public void ImageInsert(DataTable dt,int count) 
       {
           impdl.ImageInsert(dt,count);
       }

       public  string ToXml(DataTable ds)
       {
           Encoding enc=Encoding.GetEncoding("shift-jis");
           using (var memoryStream = new MemoryStream())
           {
               using (TextWriter streamWriter = new StreamWriter(memoryStream,enc ))
               {
                   var xmlSerializer = new XmlSerializer(typeof(DataTable));
                   xmlSerializer.Serialize(streamWriter, ds);
                   //return Encoding.UTF8.GetString(memoryStream.ToArray());
                   return enc.GetString(memoryStream.ToArray());
               }
           }
       }

       public void CreateXmlforItemImport(DataTable dt)
       {
           dt.TableName = "test";
           System.IO.StringWriter writer = new System.IO.StringWriter();
           dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
           string result = writer.ToString();
           result = result.Replace("&#", "$CapitalSports$");
           //result = System.Security.SecurityElement.Escape(result);
           //string result = ToXml(dt);

           //StringReader stReader = new StringReader(result);
           //String str = stReader.ReadLine();

           //result = result.Replace(str, "");

           //result = result.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("'", "&apos;");
           //result = WebUtility.HtmlEncode(result); 
           //if (result.Contains("％"))
           //    result.Replace("％", "&#37");
           //if (result.Contains("@"))
           //    output = Regex.Replace(result, "@", "&#64");
           // if (result.Contains("~"))
           //     output = Regex.Replace(result, "~", "");
           // if (result.Contains("#"))
           //     output = Regex.Replace(result, "#", "");
           // if (result.Contains("$"))
           //     output = Regex.Replace(result, "$", "&#36;");
           // if (result.Contains("%"))
           //     output = Regex.Replace(result, "%", "");
           // if (result.Contains("&"))
           //     output = Regex.Replace(result, "&", "");
           // //if (result.Contains("*"))
           // //    output = Regex.Replace(result, "*", "");
           // if (result.Contains(","))
           //     output = Regex.Replace(result, ",", "");
           // if (result.Contains("."))
           //     output = Regex.Replace(result, ".", "");
           // if (result.Contains("!"))
           //     output = Regex.Replace(result, "!", "");
           impdl.ItemImportXmlInsert(result);
       }


       public static string CleanInvalidXmlChars(string text)
       {
           // From xml spec valid chars: 
           // #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF]     
           // any Unicode character, excluding the surrogate blocks, FFFE, and FFFF. 
           string re = @"[^\x09\x0A\x0D\x20-\uD7FF\uE000-\uFFFD\u10000-\u10FFFF]";
           return Regex.Replace(text, re, "");
       }
       public  string RemoveInvalidXmlChars(string text)
       {
           var validXmlChars = text.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray();
           return new string(validXmlChars);
       }

       public  string EscapeXmlString(string value)
       {
           if (value.Contains("%"))
           {
               return System.Security.SecurityElement.Escape(value);
           }
           else { return value; }
       }

       public DataTable LogData(int ID) 
       {
       return impdl.SelectLogData(ID);
       }
       public  DataTable ckitemcode(string itcode) 
       {
           return impdl.CheckItemCode(itcode);
       }

       public void Imagedatainsert(DataTable dts) 
       {
           impdl.InsertImageData(dts);
       }


       public void XmlforItem_Image(DataTable dt)
       {
           
           dt.TableName = "test";
           System.IO.StringWriter writer = new System.IO.StringWriter();
           dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
           string result = writer.ToString();
           impdl.Item_Import_Image(result);
           
       }
       public void XmlforRakutenItem_Image(DataTable dt)
       {

           dt.TableName = "test";
           System.IO.StringWriter writer = new System.IO.StringWriter();
           dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
           string result = writer.ToString();
           impdl.RakutenItem_Import_Image(result);

       }
       public void XmlforLibrary_Image(DataTable dt)
       {

           dt.TableName = "test";
           System.IO.StringWriter writer = new System.IO.StringWriter();
           dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
           string result = writer.ToString();
           impdl.Item_Import_LibraryImage(result);

       }

       public void XmlforLatestImage(DataTable dt)
       {

           dt.TableName = "test";
           System.IO.StringWriter writer = new System.IO.StringWriter();
           dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
           string result = writer.ToString();
           impdl.LatestdataImage(result);

       }

       public void XmlforItem_Shop(DataTable dt)
       {

           dt.TableName = "test";
           System.IO.StringWriter writer = new System.IO.StringWriter();
           dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
           string result = writer.ToString();
           impdl.Item_Shop_Import(result);

       }
       public DataTable SmartLogData(int ID)
       {
           return impdl.SmartSelectLogData(ID);
       }
       public void Library_Image_Only(DataTable dt)
       {

           dt.TableName = "test";
           System.IO.StringWriter writer = new System.IO.StringWriter();
           dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
           string result = writer.ToString();
           impdl.Import_LibraryImage_Only(result);

       }
       public void RelatedItem_Only(DataTable dt)
       {

           dt.TableName = "test";
           System.IO.StringWriter writer = new System.IO.StringWriter();
           dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
           string result = writer.ToString();
           impdl.Import_RelateItem_Only(result);

       }
       public DataTable ImageLogSelectall(int ID) 
       {
           return impdl.ImageSelectLogData(ID);
       }
       public DataTable ImageErrorLogSelectall(int ID)//for error log
       {
           return impdl.ImageSelectErrorLogData(ID);
       }
       public void RelatedItem_OnlyWithCtrl(DataTable dt)
       {

           dt.TableName = "test";
           System.IO.StringWriter writer = new System.IO.StringWriter();
           dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
           string result = writer.ToString();
           impdl.Import_RelateItem_Onlywithctrl(result);

       }
       public void Library_Image_OnlyWithCtrl(DataTable dt)
       {

           dt.TableName = "test";
           System.IO.StringWriter writer = new System.IO.StringWriter();
           dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
           string result = writer.ToString();
           impdl.Import_LibraryImage_Onlywithctrl(result);

       }
       public void XmlforItem_ShopWithCtrl(DataTable dt)
       {

           dt.TableName = "test";
           System.IO.StringWriter writer = new System.IO.StringWriter();
           dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
           string result = writer.ToString();
           impdl.Item_Shop_Importwithctrl(result);

       }
       //update by KT
       public DataTable SelectLibraryimagedata(int ID)
       {
           return impdl.LibraryImageLogData(ID);
       }
       public DataTable SelectErrorLibraryimagelogdata(int ID)//for errorlog
       {
           return impdl.LibraryImageLogErrorData(ID);
       }
    }
}
