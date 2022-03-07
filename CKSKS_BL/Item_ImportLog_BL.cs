using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
     
   public  class Item_ImportLog_BL
    {
       Item_ImportLog_DL Imdl;
       public Item_ImportLog_BL(){
         Imdl = new Item_ImportLog_DL();
       }

       public  int  ImportLogInsert(int type,int reccount,int errCount,int userid) 
        {
           return Imdl.ImportLogInsert(type,reccount,errCount,userid);
        }

        public DataTable ImportLogSelectAll() 
        {
            return Imdl.ImportLogSelectAll();
        }

        public DataTable BindTagID_Error()
        {
            //dtTagValue.TableName = "test";
            //System.IO.StreamWriter sw = new System.IO.StreamWriter();
            //dtTagValue.WriteXml(sw, XmlWriteMode.WriteSchema, false);
            //string xml = sw.ToString();
            DataTable dt = Imdl.BindTagID_Error();
            return dt;
        }

        public void ChangeFlag()
        {
            Imdl.ChangeFlag();
        }

        public DataTable ItemMasterLogSelectAll(int ctrl,int id)
        {
            return Imdl.ItemmasterLogSelectAll(ctrl,id);
        }

        public DataTable ItemErrotLogSelectAll(int ctrl, int id) //For ErrorLog
        {
            return Imdl.ErrorLogSelectAll(ctrl, id);
        }

        public void ItemImportLogInsert(DataTable dt) 
        {
            Imdl.ItemImportLogInsert(dt);
        }

        public void ErrorLogInsertXML(string xml) 
        {
            Imdl.ErrorLogXmlInsert(xml);
        }

        public void Item_Import_ItemLogXML(string xml)
        {
            Imdl.Item_Import_ItemLog(xml);
        }

        public void Monotaro_Item_Import_ItemLogXML(string xml)
        {
            Imdl.Monotaro_Item_Import_ItemLogXML(xml);
        }
        public void Delivery_Item_Import_ItemLogXML(string xml)
        {
            Imdl.Delivery_Update_XML(xml);
        }
        public void SmartItem_Import_ItemLogXML(string xml)
        {
            Imdl.SmartItem_Import_ItemLog(xml);
        }

        public void SmartTemplate_ErrorLog_Insert(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();

            Imdl.SmartTemplate_ErrorLog_Insert(result);
        }

        //Call GetOptionlog function from DL
        //Updated by kyaw thet paing
        public DataTable GetOptionLog(string id)
        {
            return Imdl.GetOptionLog(id);
        }
        public DataTable GetOptionErrorsLog(string id)
        {
            return Imdl.GetOptionErrorLog(id);
        }
        //Call GetCategorylog function from DL
        //Updated by kyaw thet paing
        public DataTable GetCategoryLog(string id)
        {
            return Imdl.GetCategoryLog(id);
        }
        public DataTable GetCategoryErrorLog(string id)// For Errorlog
        {
            return Imdl.GetCategoryErrorLog(id);
        }
       //For System error log view
        public DataTable SystemlogSelectAll(int pageIndex,string userid,string status,string detail)
        {
            return Imdl.SystemLogSelectAll(pageIndex, userid, status, detail);
        }
        public DataTable GetProductLog(string id)
        {
            return Imdl.GetProductLog(id);
        }


       //For Exhibition_Flag_Change.aspx
        public void Update_Exhibited_ErrorItemCodeValue(string itemcode , int shop)
        {
            Imdl.Update_Exhibited_ErrorItemCodeValue(itemcode, shop);
        }
    }
}
