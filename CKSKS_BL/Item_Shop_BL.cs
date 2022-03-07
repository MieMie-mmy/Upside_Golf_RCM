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
    public class Item_Shop_BL
    {
        Item_Shop_DL itemShopDL;

        Item_Master_DL itemMasterDL;

        public Item_Shop_BL()
        {
            itemShopDL = new Item_Shop_DL();
        }

        //updated by aam
        public void Insert(DataTable dt, int ItemID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (itemShopDL.DeleteAll(ItemID) == 0)
                {
                    if (dt.Rows.Count > 0)
                        itemShopDL.Insert(dt);
                    scope.Complete();
                }
            }
        }

        public void DeleteAll(int Item_ID)
        {
            itemShopDL.DeleteAll(Item_ID);
        }

        public void Check_ItemShopForAmazon(int ItemID,int flag,int userid)
        {
            itemShopDL.Check_ItemShopForAmazon(ItemID,flag,userid);
        }

        public DataTable SelectByItemID(int Item_ID)
        {
            itemShopDL = new Item_Shop_DL();
            return itemShopDL.SelectByItemID(Item_ID);
        }

        public DataTable CheckItemCodeURL(int Item_ID)
        {
            return itemShopDL.CheckItemCodeURL(Item_ID);
        }

        public DataTable SelectItemCodeURL(string Item_Code)
        {
            itemShopDL = new Item_Shop_DL();
            return itemShopDL.SelectItemCodeURL(Item_Code);
        }

        public DataTable SelectShopID(int Item_ID)
        {
            itemShopDL = new Item_Shop_DL();
            return itemShopDL.SelectShopID(Item_ID);
        }

        public DataTable SelectMallID(int Item_ID)
        {
            itemShopDL = new Item_Shop_DL();
            return itemShopDL.SelectMallID(Item_ID);
        }

        public DataTable SelectCode(int Item_ID)
        {
            itemShopDL = new Item_Shop_DL();
            return itemShopDL.SelectCode(Item_ID);
        }

        public Boolean ChangeStatus(string Item_Code,string Ctrl_ID, int Shop_ID)
        {
            return itemShopDL.ChangeStatus(Item_Code, Ctrl_ID, Shop_ID);
        }

        public Boolean ChangeCtrl_ID(string Item_IDList, int Shop_ID)
        {
            return itemShopDL.ChangeCtrl_ID(Item_IDList, Shop_ID);
        }

        public void SaveItemShopExportedCSVInfo(int itemID, int shopID, string csvName)
        {
            itemShopDL.SaveItemShopExportedCSVInfo(itemID, shopID, csvName);
        }

        public void InsertItemCodeURL(DataTable dt, int ItemID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (itemShopDL.DeleteAllItemCodeURL(ItemID) == 0)
                {
                    if (dt.Rows.Count > 0)
                        itemShopDL.InsertAllItemCodeURL(dt);
                    scope.Complete();
                }
            }
        }
    }
}
