using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ORS_RCM_DL;
using ORS_RCM_Common;

namespace ORS_RCM_BL
{
    public class Item_Export_Rakutan_Image_BL
    {
        Item_Export_Rakutan_Image_DL rakutenImageDL;
        public Item_Export_Rakutan_Image_BL()
        {
            rakutenImageDL = new Item_Export_Rakutan_Image_DL();
        }

        public void Insert(Item_Export_Rakutan_Image_Entity rakutenImageInfo)
        {
            rakutenImageDL.Insert(rakutenImageInfo);
        }

        public DataTable GetFolderList()
        {
            return rakutenImageDL.GetFolderList();
        }

        public void Update(Item_Export_Rakutan_Image_Entity rakutenImageInfo)
        {
            rakutenImageDL.Update(rakutenImageInfo);
        }
    }
}
