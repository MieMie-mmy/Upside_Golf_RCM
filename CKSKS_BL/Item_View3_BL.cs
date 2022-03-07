using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using System.Data;

namespace ORS_RCM_BL
{
    public class Item_View3_BL
    {
        Item_View3_DL ivdl;
        public Item_View3_BL()
        {
            ivdl = new Item_View3_DL();
        }

        public Boolean Change_DataComplete(String ItemID)
        {
            return ivdl.Change_DataComplete(ItemID);
        }

        public DataTable GetMallByShopID(String shopID)
        {
            return ivdl.GetMallByShopID(shopID);
        }
    }
}
