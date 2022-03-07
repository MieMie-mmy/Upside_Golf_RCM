using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ORS_RCM_DL;
using ORS_RCM_Common;

namespace ORS_RCM_BL
{
    public class Item_Export_ErrorCheck_BL
    {
        Item_Export_ErrorCheck_DL Item_Export_ErrorCheckDL = new Item_Export_ErrorCheck_DL();
        public void Insert(Item_Export_ErrorCheck_Entity itemExportErrorCheck, string option)
        {
            Item_Export_ErrorCheckDL.Insert(itemExportErrorCheck, option);
        }

        public DataTable Get_NewCreated_ItemData()
        {
            return Item_Export_ErrorCheckDL.Get_NewCreated_ItemData();
        }

        public Boolean CheckItem_Code(string item_code)
        {
            return Item_Export_ErrorCheckDL.CheckItem_Code(item_code);
        }

        public Boolean CheckUpdateItem_Code(string item_code, int id)
        {
            return Item_Export_ErrorCheckDL.CheckUpdateItem_Code(item_code, id);
        }

        public void Insert_Created_ItemData(string item_code, string item_name)
        {
            Item_Export_ErrorCheckDL.Insert_Created_ItemData(item_code, item_name);
        }

        public void Delete_Created_ItemData(int id)
        {
            Item_Export_ErrorCheckDL.Delete_Created_ItemData(id);
        }

        public void Update_Created_ItemData(int id, string item_code, string item_name)
        {
            Item_Export_ErrorCheckDL.Update_Created_ItemData(id, item_code, item_name);
        }
    }
}
