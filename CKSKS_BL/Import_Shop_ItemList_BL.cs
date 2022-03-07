using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;
using ORS_RCM_DL;
using System.Transactions;


namespace ORS_RCM_BL
{
    public class Import_Shop_ItemList_BL
    {

        Import_ShopItem_List_Dl importshDl;

        public Import_Shop_ItemList_BL()
        {
          importshDl = new Import_ShopItem_List_Dl();

        }

        public DataTable SelectAll()
        {
            return importshDl.Select();

        }

        public DataTable Import_ShopItemSearch(string itemcode)
        {
            return importshDl.Import_ShopItem_Search(itemcode);
        }

    }
}
