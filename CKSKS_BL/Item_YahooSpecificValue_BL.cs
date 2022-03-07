using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using System.Data;

namespace ORS_RCM_BL
{
    public class Item_YahooSpecificValue_BL
    {
        Item_YahooSpecificValue_DL ItemYahooSpecificValueDL;

        public Item_YahooSpecificValue_BL()
        {
            ItemYahooSpecificValueDL = new Item_YahooSpecificValue_DL();
        }

        public bool Insert(int Item_ID, DataTable dtItemYahooSpecificValue)
        {
            if (ItemYahooSpecificValueDL.Delete(Item_ID))
            {
            if (!ContainColumn("Item_ID", dtItemYahooSpecificValue))
                {
                    dtItemYahooSpecificValue.Columns.Add(new DataColumn("Item_ID", typeof(Int32)));
                }

            foreach (DataRow row in dtItemYahooSpecificValue.Rows)
                {
                    row["Item_ID"] = Item_ID;
                }
            if (ItemYahooSpecificValueDL.Insert(dtItemYahooSpecificValue))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public DataTable SelectByItemID(int Item_ID)
        {
            return ItemYahooSpecificValueDL.SelectByItemID(Item_ID);
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
