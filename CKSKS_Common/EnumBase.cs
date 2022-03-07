using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class EnumBase
    {
     

        public enum Save
        {
            Insert = 0,
            Update = 1
        }

        public enum Import_Type
        { 
            ItemMaster=0,
            ItemSKU=1,
            ItemInventory=2,
            ItemCategory=3,
            ItemOption=4

        }

        public enum Import_ErrorLogType
        { 
            Physical_Err=0,
            Logical_Err=1
        }

        public enum Item_ImageType
        { 
            Item_Image=0,
            Library_Image=1
        }

        public enum Item_ExportQ_FileType
        {
            Item=0,
            Category=1,
            Inventory=2,
            Image=3
        }

        public enum Mall
        { 
            Yahoo=1,
            Rakutan=2,
            Ponpare=3,
            Amazon=4
        }
    }
}
