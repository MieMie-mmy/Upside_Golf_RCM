using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Upside_Golf_RCM_Common;
using Upside_Golf_RCM_DL;
using System.Data;

namespace Upside_Golf_RCM_BL
{
   public class Item_SKUView_BL
    {
       Item_SKU_View_DL itSKUdl;

       public Item_SKUView_BL()
        {
            itSKUdl = new  Item_SKU_View_DL ();
        }

       public DataTable Search(string itemcode)
       {
           itSKUdl = new Item_SKU_View_DL();
           return itSKUdl.View(itemcode);



       }

    }
}
