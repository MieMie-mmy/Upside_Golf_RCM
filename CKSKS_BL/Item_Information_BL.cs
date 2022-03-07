using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_Common;
using ORS_RCM_DL;
using System.Data;
using System.Data.SqlClient;


namespace ORS_RCM_BL
{
   public class Item_Information_BL
    {

        Item_Information_DL iteminfo_dl;
        int totalrowcount;

        public Item_Information_BL()
        {
            iteminfo_dl = new Item_Information_DL();
        }

        public DataTable AllItemView()
        {
            return iteminfo_dl.ShowItemview();
        }

        public DataTable SearchItem(string itemcode,string salecode,string competition_name,
                                                             string brand_name,string classname,string jancode,
                                                             string season,string year)
        {
            return iteminfo_dl.ItemSearch(itemcode,salecode,competition_name,brand_name,classname,jancode,season,year);
        }

        public DataTable SearchbyItem(Item_Master_Entity ime)
        {
            return iteminfo_dl.SearchItem(ime);
        }

        public DataTable SearchbyItem2(Item_Master_Entity ime,int pgindex,int psize,int option,int search)
        {
            return iteminfo_dl.SearchItem2(ime,pgindex,psize,option,search);
        }
        public DataTable SearchbyItemlike(Item_Master_Entity ime, int pgindex, int psize, int option)
        {
            return iteminfo_dl.LikeSearchItem(ime, pgindex, psize, option);
        }

        public DataTable ItemView2_Search(Item_Master_Entity ime, int pageIndex, int pageSize, int option)
        {
            return iteminfo_dl.ItemView2_Search(ime, pageIndex, pageSize, option);
        }

        //public DataTable ItemView2_PageLoad(Item_Master_Entity ime, int pageIndex, int pageSize, int option)
        //{
        //    return iteminfo_dl.ItemView2_PageLoad(ime, pageIndex, pageSize, option);
        //}

        public DataTable SelectShop()
        {
            return iteminfo_dl.SelectShop();
        }

        public DataTable SelctShop(int id) 
        {
            return iteminfo_dl.SelectShop(id);
        }

        public DataTable SearchItem_View2_Data(string search, string item, int pagesize, int startIndex)
        {
            return iteminfo_dl.SearchItem_View2_Data(search, item, startIndex, pagesize, out totalrowcount);
        }

        public int TotalRowCount(string search, string item, int pagesize)
        {
            return totalrowcount;
        }
        public String Get_Lot_Number(string Item_Code)
        {
            return iteminfo_dl.Get_Lot_Number(Item_Code);
        }
    }
}
