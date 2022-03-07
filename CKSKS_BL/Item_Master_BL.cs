using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using ORS_RCM_Common;
using System.Data;
using System.Transactions;

namespace ORS_RCM_BL
{
    public class Item_Master_BL
    {
        Item_Master_DL imDL;

        public Item_Master_BL()
        {
            imDL = new Item_Master_DL();
        }

        public DataTable GetItemSaleDescription(String id)
        {
            return imDL.GetItemSaleDescription(id);
        }

        public Boolean Update(Item_Master_Entity ime)
        {
            return imDL.Update(ime);
        }

        public DataTable BindSaleUnit()
        {
            return imDL.BindSaleUnit();
        }

        public DataTable BindORSTag()
        {
            return imDL.BindORSTag();
        }

        public DataTable BindContentunit1()
        {
            return imDL.BindContentUnit1();
        }

        public DataTable BindContentunit2()
        {
            return imDL.BindContentUnit2();
        }

        public int CheckExistsItemCode(string itemcode)
        {
            return imDL.CheckExistsItemCode(itemcode);
        }

        public int SaveEdit(Item_Master_Entity ime, string option)
        {
            int itemID = imDL.SaveEdit( ime, option);

            if (itemID > 0)
            {
                imDL.MonotaroSaveEdit(ime,itemID);
                return itemID;
            }
            else 
            {
                return 0;
            }

        }

        public Item_Master_Entity SelectMallByID(int id)
        {
            return imDL.SelectMallByID(id);
        }

        public Item_Master_Entity SelectByID(int id)
        {
            return imDL.SelectByID(id);
        }

        public DataTable SelectAll(Item_Master_Entity ime, int pageIndex, int pageSize, int option,int search)
        {
            return imDL.SelectAll(ime, pageIndex, pageSize, option,search);
        }
        public DataTable Monotaro_SelectAll(Item_Master_Entity ime, int pageIndex, int pageSize, int option, int search)
        {
            return imDL.Monotaro_SelectAll (ime, pageIndex, pageSize, option, search);
        }

        public DataTable SelectAllByStatus(Item_Master_Entity ime, int pageIndex, int pageSize, int option, int search)
        {
            return imDL.SelectAllByStatus(ime, pageIndex, pageSize, option, search);
        }

        public DataTable SelectAllExport(Item_Master_Entity ime, int pageIndex, int pageSize, int option, int search)
        {
            return imDL.ExportSelectAll(ime, pageIndex, pageSize, option, search);
        }
        public DataTable Search(Item_Master_Entity ime, int LastId, int option, int PageSize)
        {
            return imDL.Search(ime, LastId, option, PageSize);
        }

        public DataTable SelectAll_ItemView3(Item_Master_Entity ime, int pageIndex, int pageSize, int option)
        {
            return imDL.SelectAll_ItemView3(ime, pageIndex, pageSize, option);
        }

        public DataTable SelectAll(Item_Master_Entity ime)
        {
            return imDL.SelectAll(ime);
        }

        public DataTable SelectAll()
        {
            return imDL.SelectAll();
        }

        public DataTable ItemView3_SelectAll()
        {
            return imDL.ItemView3_SelectAll();
        }

        public DataTable SelectItemImage(String id)
        {
            return imDL.SelectItemImage(id);
        }

        public void ItemImageInsert(String id,String Image)
        {
            imDL.ItemImageInsert(id, Image);
        }

        public int SelectItemID(string Item_Code)
        {
            return imDL.SelectItemID(Item_Code);
        }

        public DataTable SelectByItemCode(string ItemCode)
        {
            return imDL.SelectByItemCode(ItemCode);
        }

        public string SelectByItemCode(int Item_ID)
        {
            return imDL.SelectByItemCode(Item_ID);
        }

        public DataTable SearchItemCode(string code)
        {
            return imDL.SearchItemCode(code);
        }

        public DataTable GetShopList(string str)
        {
            return imDL.GetShopList(str);
        }

        public DataTable GetShopList(string str,string mall_name)
        {
            return imDL.GetShopList(str,mall_name);
        }

        public DataTable SelectByItemDataForRakuten(int shop_ID, string itemIDList, string option)
        {
            return imDL.SelectByItemDataForRakuten(shop_ID, itemIDList, option);
        }

        public DataTable SelectByItemDataForYahoo(string itemIDList , string option )
        {
            return imDL.SelectByItemDataForYahoo(itemIDList, option);
        }

        public DataTable SelectByItemDataForYahoo(string itemIDList, string option, int Shop_ID)
        {
            return imDL.SelectByItemDataForYahoo(itemIDList, option, Shop_ID);
        }

        public DataTable SelectByItemDataForPonpare(int shop_ID, string itemIDList, string option)
        {
            return imDL.SelectByItemDataForPonpare(shop_ID, itemIDList, option);
        }

        public DataTable SelectByItemDataForAmazon(string itemIDList)
        {
            return imDL.SelectByItemDataForAmazon(itemIDList);
        }

        public DataTable SelectByItemDataForJisha(int shop_ID, string itemIDList, string option)
        {
            return imDL.SelectByItemDataForJisha(shop_ID, itemIDList, option);
        }

        public int CheckImport_ShopItem(int shop_ID, string item_Code)
        {
            return imDL.CheckImport_ShopItem(shop_ID, item_Code);
        }

        public int CheckImport_ShopItemInventory(int shop_ID, string item_AdminCode)
        {
            return imDL.CheckImport_ShopItemInventory(shop_ID, item_AdminCode);
        }

        public DataTable SelectValueByItemID(int id)
        {
            return imDL.SelectValueByItemID(id);
        }

        public int CheckImport_ShopItemCategory(int shop_ID, string item_AdminCode)
        {
            return imDL.CheckImport_ShopItemCategory(shop_ID, item_AdminCode);
        }

        public DataTable Search( string Item_Name,
                                                    string Item_Code,
                                                    string Image_Name,
                                                    string Catalog_Infromation,
                                                    string Brand_Name,
                                                    string Category_Name,
                                                    string Competition_Name,
                                                    string Color_Name,
                                                    string Year,
                                                    string Season,
                                                    string Export_Status,
                                                    string Ctrl_ID,
                                                    string Special_Flag,
                                                    string Reservation_Flag,
                                                    string Person,
                                                    string Keyword)
        {
            return imDL.Search(Item_Name,
                                            Item_Code,
                                            Image_Name,
                                            Catalog_Infromation,
                                            Brand_Name,
                                            Category_Name,
                                            Competition_Name,
                                            Color_Name,
                                            Year,
                                            Season,
                                            Export_Status,
                                            Ctrl_ID,
                                            Special_Flag,
                                            Reservation_Flag,
                                            Person,
                                            Keyword);
    }

        public DataTable SearchForViewQuick(string Item_Name, string Item_Code, int pageIndex, int pageSize, int option)
        {
            return imDL.SearchItemViewQuick(Item_Name, Item_Code, pageIndex, pageSize, option);
        }

        public Boolean ChangeStatusForm(int Item_ID , int Export_Status,int User_ID)
        {
            return imDL.ChangeStatusForm(Item_ID, Export_Status, User_ID);
        }

        public Boolean ChangeStatusConsole(int Item_ID, int Export_Status)
        {
            return imDL.ChangeStatusConsole(Item_ID, Export_Status);
        }

        public Boolean ChangeCtrl_ID(string itemIDList)
        {
            return imDL.ChangeCtrl_ID(itemIDList);
        }

        public Boolean ChangeExport_Status(string Item_Code)
        {
            return imDL.ChangeExport_Status(Item_Code);
        }

        public string GetBrandName(string Item_Code)
        {
            return imDL.GetBrandName(Item_Code);
        }

        public string GetListPrice(string Item_Code)
        {
            return imDL.GetListPrice(Item_Code);
        }

        public string GetSalesUnit(string Item_Code,string option)
        {
            return imDL.GetSalesUnit(Item_Code,option);
        }

        public string GetSalePrice(string Item_Code)
        {
            return imDL.GetSalePrice(Item_Code);
        }

        public string GetZettItemDescription(string Item_Code)
        {
            return imDL.GetZettItemDescription(Item_Code);
        }

        public string GetZettSaleDescription(string Item_Code)
        {
            return imDL.GetZettSaleDescription(Item_Code);
        }

        public string GetTemplateValue(string price,int shopID)
        {
            return imDL.GetTemplateValue(price, shopID);
        }

        public string GetGroupNo()
        {
            return imDL.GetGroupNo();
        }

        public void DeleteItem(string Item_Code)
        {
            imDL.DeleteItem(Item_Code);
        }

        public void SetUnsetDailyDelivery(string ItemCode, int flag)
        {
            imDL.SetUnsetDailyDelivery(ItemCode, flag);
        }

        public DataTable BindDailyFlag(string ItemCode)
        {
            DataTable dt=imDL.BindDailyFlag(ItemCode);
            return dt;
        }

        public Boolean IsPost_Available_Date(int Item_ID)
        {
            try
            {
                imDL = new Item_Master_DL();
               return imDL.IsPost_Available_Date(Item_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeExportStatusToPink(string itemCode,int option)
        {
            imDL = new Item_Master_DL();
            imDL.ChangeExportStatusToPink(itemCode,option);
        }

        public DataTable SelectForExportStatusChange(string Item_ID)
        {
            return imDL.SelectForExportStatusChange(Item_ID);
        }

        public void ChangeExportStatus(string list, int status)
        {
             imDL.ChangeExportStatus(list, status);
        }

        public DataTable SelectChangeStatusItem(string list, int pageIndex, int pageSize)
        {
            return imDL.SelectChangeStatusItem(list, pageIndex, pageSize);
        }

        public void ItemUpdateInventory(string itemcode,int option)
        {
            imDL.ItemUpdateInventory(itemcode,option);
        }

        public void InsertItemInventory(string itemcode)
        {
            imDL.InsertItemInventory(itemcode);
        }

        public DataTable BindMonotaro(string tablename)
        {
            return imDL.BindMonotaro(tablename);
        }

        public DataTable CheckRequiredData(int Item_ID)
        {
            return imDL.CheckRequiredData(Item_ID);
        }
        public int InsertCSV(DataTable dt)
        {
            DataTable dtErr = new DataTable();
            DataTable dtOK = new DataTable();
            DataRow[] dr = dt.Select("Type=2");
            if (dr.Count() > 0)
                dtOK = dt.Select("Type=2").CopyToDataTable();
            dr = dt.Select("Type=1 OR Type=0");
            if (dr.Count() > 0)
                dtErr = dt.Select("Type=1 OR Type=0").CopyToDataTable();
            int userid = (int)dt.Rows[0]["user_ID"];
            int count = dtOK.Rows.Count;
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                Timeout = TimeSpan.MaxValue
            };
            TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
            using (scope)
            {
                Item_ImportLog_BL iilbl = new Item_ImportLog_BL();
                int logID = iilbl.ImportLogInsert(Convert.ToInt32(EnumBase.Import_Type.ItemCategory), dt.Rows.Count, dtErr.Rows.Count, userid);
                DataColumn newColumn = new DataColumn("Item_Import_LogID", typeof(System.Int32));
                newColumn.DefaultValue = logID;
                dtOK.Columns.Add(newColumn);
                imDL.InsertItemImportPD(dtOK);

                ErrorLog_BL errbl = new ErrorLog_BL();
                DataColumn newColumns = new DataColumn("errLog", typeof(System.Int32));
                newColumns.DefaultValue = logID;
                dtErr.Columns.Add(newColumns);
                errbl.InsertErrorLog_Productxml(dtErr);
                do
                {
                    DataTable dtTemp = dtOK.Rows.Cast<DataRow>().Take(30000).CopyToDataTable();


                    imDL.InsertCSV(dtTemp);


                    count = 0;
                    while (count < 30000)
                    {
                        if (dtOK.Rows.Count > 0)
                            dtOK.Rows.RemoveAt(0);
                        else break;
                        count++;
                    }
                } while (dtOK.Rows.Count > 0);

                scope.Complete();
                return logID;
            }
        }
    public int UpdateSaleDecCSV(DataTable dt)
        {  
            DataTable dtErr = new DataTable();
            DataTable dtOK = new DataTable();
            DataRow[] dr = dt.Select("Type=2");
            if (dr.Count() > 0)
                dtOK = dt.Select("Type=2").CopyToDataTable();
            dr = dt.Select("Type=1 OR Type=0");
            if (dr.Count() > 0)
                dtErr = dt.Select("Type=1 OR Type=0").CopyToDataTable();
            int userid = (int)dt.Rows[0]["user_ID"];
            int count = dtOK.Rows.Count;
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                Timeout = TimeSpan.MaxValue
            };
            TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
            using (scope)
            {
               Item_ImportLog_BL iilbl = new Item_ImportLog_BL();
                int logID = iilbl.ImportLogInsert(Convert.ToInt32(EnumBase.Import_Type.ItemCategory), dt.Rows.Count, dtErr.Rows.Count, userid);
                DataColumn newColumn = new DataColumn("Item_Import_LogID", typeof(System.Int32));
                newColumn.DefaultValue = logID;
                dtOK.Columns.Add(newColumn);
                imDL.InsertItemImportPD(dtOK);

                ErrorLog_BL errbl = new ErrorLog_BL();
                DataColumn newColumns = new DataColumn("errLog", typeof(System.Int32));
                newColumns.DefaultValue = logID;
                dtErr.Columns.Add(newColumns);
                errbl.InsertErrorLog_Productxml(dtErr);
                do
                {
                    DataTable dtTemp = dtOK.Rows.Cast<DataRow>().Take(30000).CopyToDataTable();


                    imDL.UpdateSaleDecCSV(dtTemp);


                    count = 0;
                    while (count < 30000)
                    {
                        if (dtOK.Rows.Count > 0)
                            dtOK.Rows.RemoveAt(0);
                        else break;
                        count++;
                    }
                } while (dtOK.Rows.Count > 0);

                scope.Complete();
                return logID;
            }
        }
        public DataTable GetPrices(String itemcode)
        {
            return imDL.GetPrices(itemcode);
        }
    }
}
