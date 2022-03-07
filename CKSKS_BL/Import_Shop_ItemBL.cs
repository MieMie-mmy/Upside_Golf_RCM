using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;
using System.IO;
using System.Transactions;

namespace ORS_RCM_BL
{
    //-----------------------------------------------------------------------------------------------------
    public class Import_Shop_ItemBL
    {
        Import_Shop_ItemDL isdl;
        //-----------------------------------------------------------------------------------------------------
        public Import_Shop_ItemBL()
        {
            isdl = new Import_Shop_ItemDL();
        }

        //-----------------------------------------------------------------------------------------------------
        public DataTable Getftp(int mallid)
        {
            return isdl.Getftp(mallid);
        }

        //-----------------------------------------------------------------------------------------------------
        public void InsertRakutenItem(string xml)
        {
            isdl.InsertRakutenItem(xml);
        }

        public void InsertInventoryData(DataTable dt, String shopid,DateTime dtime)
        {
            isdl.InsertInventoryData(dt, shopid,dtime);
        }

        public void InsertCategoryData(DataTable dt, String shopid,DateTime dtime)
        {
            isdl.InsertCategoryData(dt, shopid,dtime);
        }

        //-----------------------------------------------------------------------------------------------------
        public DataTable checkShop(String shopid)
        {
            return isdl.checkShop(shopid);
        }

        public void InsertRakutenData(DataTable dt, String shopid, DateTime dtime)
        {
            DataColumn dc = new DataColumn("ShopID", typeof(Int32));
            dc.DefaultValue = shopid;
            dt.Columns.Add(dc);

            dt = ChangeHeaderName(dt);
            int count = dt.Rows.Count;
            do
            {
                DataTable dtTemp = dt.Rows.Cast<DataRow>().Take(10000).CopyToDataTable();
                String xml = DataTableToXml(dtTemp);
                isdl.InsertRakutenData(xml, dtime);
                count = 0;
                while (count < 10000)
                {
                    if (dt.Rows.Count > 0)
                        dt.Rows.RemoveAt(0);
                    else break;
                    count++;
                }
            } while (dt.Rows.Count > 0);
        }

        public DataTable ChangeHeaderName(DataTable dt)
        {
            dt.Columns["商品番号"].ColumnName = "Item_Code";
            dt.Columns["コントロールカラム"].ColumnName = "Ctrl_ID";
            dt.Columns["商品管理番号（商品URL）"].ColumnName = "Item_AdminCode";
            dt.Columns["商品名"].ColumnName = "Item_Name";
            if (dt.Columns.Contains("ポイント変倍率"))
                dt.Columns["ポイント変倍率"].ColumnName = "Point_Code";
            else dt.Columns.Add("ポイント変倍率", typeof(String));
            if (dt.Columns.Contains("ポイント変倍率適用期間"))
                dt.Columns["ポイント変倍率適用期間"].ColumnName = "Point_Term";
            else dt.Columns.Add("ポイント変倍率適用期間", typeof(String));

            return dt;
        }

        public String DataTableToXml(DataTable dt)
        {

            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            return result;
        }

        //-----------------------------------------------------------------------------------------------------
        public void DeleteRakutenData(String shopID,DateTime dt)
        {
            isdl.DeleteRakutenData(shopID,dt);
        }

        //-----------------------------------------------------------------------------------------------------
        public void DeleteRakutenBackup(int shopid)
        {
            isdl.DeleteRakutenBackup(shopid);
        }

        //-----------------------------------------------------------------------------------------------------
        public void InsertCsv(String xml, String StoreProdecure)
        {
            isdl.InsertCsv(xml, StoreProdecure);
        }

        //-----------------------------------------------------------------------------------------------------
        public DataTable GetShopByMallID(String mallID)
        {
            return isdl.GetShopByMallID(mallID);
        }

        public DataTable GetMallByShopID(String shopID)
        {
            return isdl.GetMallByShopID(shopID);
        }

        public DataTable GetAllShop()
        {
            return isdl.GetAllShop();
        }

        //-----------------------------------------------------------------------------------------------------
        public DataTable checkCategory(String shopid)
        {
            return isdl.checkCategory(shopid);
        }

        //-----------------------------------------------------------------------------------------------------
        public void DeleteCategoryBackup(int shopid)
        {
            isdl.DeleteCategoryBackup(shopid);
        }

        //-----------------------------------------------------------------------------------------------------
        public void InsertShopCategory(string xml)
        {
            isdl.InsertShopCategory(xml);
        }

        //-----------------------------------------------------------------------------------------------------
        public void DeleteCategoryData(String shopID,DateTime dt)
        {
            isdl.DeleteCategoryData(shopID,dt);
        }

        //-----------------------------------------------------------------------------------------------------
        public DataTable checkInventory(String shopid)
        {
            return isdl.checkInventory(shopid);
        }

        //-----------------------------------------------------------------------------------------------------
        public void InsertInventory(string xml)
        {
            isdl.InsertInventory(xml);
        }

        //-----------------------------------------------------------------------------------------------------
        public void DeleteInventoryBackup(int shopid)
        {
            isdl.DeleteInventoryBackup(shopid);
        }

        //-----------------------------------------------------------------------------------------------------
        public void DeleteInventoryData(String shopID,DateTime dt)
        {
            isdl.DeleteInventoryData(shopID,dt);
        }
    }
}


