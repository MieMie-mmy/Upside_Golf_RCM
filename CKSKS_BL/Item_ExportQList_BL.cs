using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
   public  class Item_ExportQList_BL
   {
       Item_Export_QList_DL Idl;
       public Item_ExportQList_BL()
       { Idl = new Item_Export_QList_DL(); }

       public DataTable SelectAll(string brand, string cat, string itno, string itname, DateTime? postdate, DateTime? avadate,string supplier) 
       {
           return Idl.SelectAll(brand,cat,itno,itname,postdate,avadate,supplier);
       }
       
       public DataTable SelectAllData()
       {
           return Idl.SelectAllData();
       }

    }
}
