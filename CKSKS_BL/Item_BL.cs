using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Upside_Golf_RCM_Common;
using Upside_Golf_RCM_DL;
using System.Data;
using System.Data.SqlClient;

namespace Upside_Golf_RCM_BL
{
    public class Item_BL
    {
        Item_DL itemDL;

        public Item_BL()
        {
            itemDL = new Item_DL();
        }

        public int SaveEdit(Item_Entity item, string option)
        {
            int itemID = itemDL.SaveEdit(item, option);

            if (itemID > 0)
            {
                return itemID;
            }
            else 
            {
                return 0;
            }

        }

        public Item_Entity SelectByID(int id)
        {
            return itemDL.SelectByID(id);
        }

        public DataTable Search(string itemcode,string salecode) 
        {
            return itemDL.Search(itemcode,salecode);
        }

        public DataTable SelectAll() 
        {
            return itemDL.SelectAll();
        
        }

        public DataTable SelectAllItem()
        {
            return itemDL.SelectAllItem();
        }

        public void UpdateItem(DataTable dt)
        {
            itemDL.UpdateItem(dt);
        }
       
        public void UpdateQuantity(string id,string quantity,string jisha_quantity)
        {
            itemDL.UpdateQuantity(id, quantity, jisha_quantity);
        }

        public DataTable SearchItem(string ItemCode, string proname, string procode, string catInfo, string bname, string compname, string year, string season, string jan,int pindex,int psize,int option,Boolean isSearch)
        {
            return itemDL.SearchItem(ItemCode, proname, procode, catInfo, bname, compname, year, season, jan,pindex,psize,option,isSearch);
        }

        public DataTable SelectItemMasterData(String itemcode)
        {
            return itemDL.SelectItemMasterData(itemcode);
        }

        public DataTable  Get_ddlData(String query)
        {
            return itemDL.GetData(query);
          }

        public DataTable bindDDL()
        {
             return itemDL.ddlPerson_in_Charge();


        }

        public DataTable bindDDforShopstatus()
        {
            return itemDL.ddlShop_Status();


        }

        public DataTable SelectSKU(string ItemCode)
        {
            return itemDL.SelectSKU(ItemCode);
        }

        public DataTable Select_RelatedItem(string ItemCode1, string ItemCode2, string ItemCode3, string ItemCode4, string ItemCode5)
        {
            return itemDL.SelectRelatedItem(ItemCode1, ItemCode2, ItemCode3, ItemCode4, ItemCode5);
        }

        public DataTable SelectSKUSize(string ItemCode)
        {
            return itemDL.SelectSKUSize(ItemCode);
        }

        public DataTable SelectSKUColor(string ItemCode)
        {
            return itemDL.SelectSKUColor(ItemCode);
        }

        public Boolean DeleteItems(string itemlist)
        {
            return itemDL.DeleteItems(itemlist);
        }

        public DataTable GetSKUHeader(string Item_Code)
        {
            return itemDL.GetSKUHeader(Item_Code);
        }

        public DataTable GetSKUSide(string Item_Code)
        {
            return itemDL.GetSKUSide(Item_Code);
        }

        public DataTable GetSKUQuantity(string Item_Code)
        {
            return itemDL.GetSKUQuantity(Item_Code);
        }

        public string GetSKUSizeName(string Item_Code)
        {
            return itemDL.GetSKUSizeName(Item_Code);
        }

        public string GetSKUColorName(string Item_Code)
        {
            return itemDL.GetSKUColorName(Item_Code);
        }

        public DataTable BindColor(string category_id)
        {
          return  itemDL.BindColor(category_id);
           
        }

        public DataTable BindgdvDetailSKU(string category_id, string Item_Code)
        {
            return itemDL.BindgdvDetailSKU(category_id, Item_Code);

        }

        public void UpdateSKU(string id, string sku,int option)
        {
            itemDL.UpdateSKU(id,sku,option);
        }
        //public DataTable  ShippingNo_Select()
        //{
        //    return itemDL.ShippingNo_Select();
        //}
        public void DeleteSKU(string id, string sku, int option)
        {
            itemDL.DeleteSKU(id,sku,option);
        }

        public DataTable SelectItemData(string Item_Code)
        {
            return itemDL.SelectItemData(Item_Code);
        }

        public void InsertUpdateSKU(DataTable dtSKU, string Item_Code)
        {
            //dtSKU.TableName = "test";
            //System.IO.StringWriter writer = new System.IO.StringWriter();
            //dtSKU.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            //string result = writer.ToString();
            itemDL.InsertUpdateSKU(dtSKU, Item_Code);
        }

        public void DeleteSKUOption(String Item_Code)
        {
            itemDL.DeleteSKUOption(Item_Code);
        }
        //public DataSet BindItem(string brandname, string itemcode, int pagesize, int pageindex)
        //{
        //    return itemDL.BindItem(brandname, itemcode, pagesize, pageindex);
        //}
        //public DataSet BindItem(string itemcode, int pagesize, int pageindex)
        //{
        //    return itemDL.BindItem(itemcode, pagesize, pageindex);
        //}
        public DataSet BindItem(string itemcode,string itemname,string brandname, int pagesize, int pageindex,string option)
        {
            return itemDL.BindItem(itemcode,itemname,brandname, pagesize, pageindex,option);
        }
        public DataSet BindItemName(string itemname)
        {
            return itemDL.BindItemName(itemname);
        }
        public DataSet BindBrandName(string brandname)
        {
            return itemDL.BindBrandName(brandname);
        }
        public DataSet BindPageloadItem(string itemcode, int pagesize, int pageindex, string option)
        {
            return itemDL.BindPageloadItem(itemcode, pagesize, pageindex, option);
        }
        public DataSet BindAllItem(int pagesize, int pageindex)
        {
            return itemDL.BindAllItem(pagesize, pageindex);
        }

        public DataSet BindItemExport(string itemcode, int option)
        {
            return itemDL.BindItemExport(itemcode,option);
        }
        public DataTable DDLRShipping()
        {
            return itemDL.DDLRShipping();
        }

        public DataTable DDLYShipping()
        {
            return itemDL.DDLYShipping();
        }
        public DataTable ShippingNo_Select(string sno,int option)
        {
            return itemDL.ShippingNo_Select(sno,option);
        }
        public void SKU_Save(DataTable dtSKU)
        {
            dtSKU.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dtSKU.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            itemDL.SKU_Save(result);
        }

        public void UpdateStockFlag(int ID,int flag)
        {
            itemDL.UpdateStockFlag(ID,flag);
        }
        
    }
}
