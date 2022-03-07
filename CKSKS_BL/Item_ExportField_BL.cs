using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;
using ORS_RCM_Common;

namespace ORS_RCM_BL
{
     public class Item_ExportField_BL
    {
         Item_ExportField_DL itdl;
         public Item_ExportField_BL()
         {
             itdl = new Item_ExportField_DL();
     }

         public int Insert(Item_ExportField_Entity item)
         {
             return itdl.Insert(item);
         }

         public void Update(DataTable dt)
         {
              itdl.Update(dt);
         }

         public DataTable SelectAll()
         {
             return itdl.SelectAll();
         }

         public DataTable SelectUser()
         {
             return itdl.SelectAllUser();
         }

         public DataTable SelectAllData(string list) 
         {
             return itdl.SelectAllData(list);
         }

         public DataTable ExportCSV(string str,string option ) 
         {
             return itdl.Exportcsv(str,option);
         }

         public DataTable Selectoption_Cat(string str,int ctrl)
         {
             return itdl.SelectOption_Cat(str,ctrl);
         }

         public DataTable SmartCSV(string ids,string str)
         {
             return itdl.Smartcsv(ids,str);
         }
         public DataTable SmartXanaxCSV(string ids, string str)
         {
             return itdl.XanaxSmartcsv(ids, str);
         }
         public DataTable SelectRakutenImagesetting(string str)
         {
             return itdl.SelectRakutenImageID(str);
         }
         public DataTable SelectRakutenTagIDInfo(string str)
         {
             return itdl.SelectRakutenTagIDInfo(str);
         }
         
         public DataTable SmartCSVImage( string str)
         {
             return itdl.SmartcsvRelateitemImage(str);
         }
         public DataTable ExportCSVShop(string str)
         {
             return itdl.ExportcsvShop(str);
         }

         public void SmartUpdate(DataTable dt)
         {
             itdl.SmartTUpdate(dt);
         }
         public DataTable SmartSelectAll()
         {
             return itdl.STSelectAll();
         }

         public DataTable STSelectAllData(string list)
         {
             return itdl.SmartSelectAllData(list);
         }
         public int Smartinsert(Item_ExportField_Entity item)
         {
             return itdl.SmartInsert(item);
         }

         public DataTable GetManualData()
         {
             return itdl.GetManualData();
         }
    }
}
