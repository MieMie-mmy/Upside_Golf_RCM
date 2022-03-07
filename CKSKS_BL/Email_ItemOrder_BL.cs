using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ORS_RCM_DL;
using ORS_RCM_DL;
using ORS_RCM_Common;

namespace ORS_RCM_BL
{
    public class Email_ItemOrder_BL
    {
        Email_ItemOrder_DL emailDL;

        public Email_ItemOrder_BL()
        {
            emailDL = new Email_ItemOrder_DL();
        }

        public DataTable SearchItem(string shopName, string itemNumber, DateTime? fromDate, DateTime? toDate, int option)
        {
            return emailDL.SearchItem(shopName,itemNumber,fromDate,toDate,option);
        }


        public DataTable Search_ItemSeparatedOrderlist(ItemSeparated_OrderList_Entity ise,int pgindex, String psize,int option)
        {
            return emailDL.Search_ItemSeparatedOrderList(ise, pgindex, psize,option);
        }



        public DataTable Search_SKUSeparatedOrderlist(SKUSeparated_OrderList_Entity skuent, int pgindex, String psize, int option)
        {
            return emailDL.Search_SKUSeparatedOrderList(skuent, pgindex, psize, option);
        }



        public DataTable SearchSalelist(Sale_ListScreen_Entity se, int pgindex, String psize)
        {
            return emailDL.SearchSaleorderlist(se, pgindex,psize);
        }



        public DataTable SelectAll()
        {
            return emailDL.SelectAll();
        }

        public DataTable SelectShop() 
        {
            return emailDL.SelectShop();
        }
    }
}
