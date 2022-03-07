using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using System.Data;

namespace ORS_RCM_BL
{
    public class Jisha_Item_Master_BL
    {
        Jisha_Item_Master_DL JishaDL;
        public Jisha_Item_Master_BL()
        {
            JishaDL = new Jisha_Item_Master_DL();
        }

        //Jisha_Item_Master Table
        public DataTable SelectAll()
        {
            return JishaDL.SelectAll();
        }

        public DataTable SelectByItemID(int Item_ID)
        {
            return JishaDL.SelectByItemID(Item_ID);
        }

        //Jisha_Item Table
        public DataTable GetSKUHeader(string Item_Code)
        {
            return JishaDL.GetSKUHeader(Item_Code);
        }

        public DataTable GetSKUQuantity(string Item_Code)
        {
            return JishaDL.GetSKUQuantity(Item_Code);
        }

        //Jisha_OrderDetail Table
        public void Insert(DataTable dt)
        {
            if (!ContainColumn("Order_ID", dt))
            {
                dt.Columns.Add(new DataColumn("Order_ID", typeof(String)));
            }
            JishaDL.Insert(dt);
        }

        //Jisha_Item_Option Table
        public DataTable GetJishaItemOption(string Item_Code)
        {
            return JishaDL.GetJishaItemOption(Item_Code);
        }

        //Jisha_Item_Category Table
        public DataTable SelectItemByCategory(string Category_No)
        {
            return JishaDL.SelectItemByCategory(Category_No);
        }

        public DataTable SelectByCategoryPath(string Item_Code)
        {
            return JishaDL.SelectByCategoryPath(Item_Code);
        }

        //Check
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
