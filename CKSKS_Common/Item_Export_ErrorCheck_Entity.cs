using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Item_Export_ErrorCheck_Entity
    {
        private int id=0;
        private int shopId;
        private string itemCode;
        private string errorDescription;
        private int checkType;
        private DateTime createdDate;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public int ShopID
        {
            get { return shopId; }
            set { shopId = value; }
        }
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        public string ErrorDescription
        {
            get { return errorDescription; }
            set { errorDescription = value; }
        }
        public int CheckType
        {
            get { return checkType; }
            set { checkType = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
    }
}
