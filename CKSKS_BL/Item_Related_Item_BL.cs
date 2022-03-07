using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using System.Data;

namespace ORS_RCM_BL
{
    public class Item_Related_Item_BL
    {
        Item_Related_Item_DL ItemRelatedDL;

        public Item_Related_Item_BL()
        {
            ItemRelatedDL = new Item_Related_Item_DL();
        }

        public bool Insert(int Item_ID, DataTable ItemRelatedList)
        {
            if (ItemRelatedDL.Delete(Item_ID))
            {
                if (!ContainColumn("Item_ID", ItemRelatedList))
                {
                    ItemRelatedList.Columns.Add(new DataColumn("Item_ID", typeof(Int32)));
                }

                foreach (DataRow row in ItemRelatedList.Rows)
                {
                    row["Item_ID"] = Item_ID;
                }
                if (ItemRelatedDL.Insert(ItemRelatedList))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public DataTable SelectRelatedValuebyItemID(int Item_ID)
        {
            return ItemRelatedDL.SelectRelatedValuebyItemID(Item_ID);
        }

        public DataTable SelectByItemID(int Item_ID)
        {
            return ItemRelatedDL.SelectByItemID(Item_ID);
        }

        public DataTable SelectRelatedCode(string Item_Code)
        {
            return ItemRelatedDL.SelectRelatedCode(Item_Code);
        }

        public bool ContainColumn(string columnName, DataTable table)
        {
            DataColumnCollection columns = table.Columns;

            if (columns.Contains(columnName))
            {
                return true;
            }
            else
                return false;
        }

    }
}
