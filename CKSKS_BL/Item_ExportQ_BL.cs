using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using ORS_RCM_Common;

namespace ORS_RCM_BL
{
    public class Item_ExportQ_BL
    {
        Item_ExportQ_DL ieDL;

        public Item_ExportQ_BL()
        {
            ieDL = new Item_ExportQ_DL();
        }

        public bool Save(Item_ExportQ_Entity ie)
        {
            if (ieDL.Save(ie))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ChangeIsExportFlag()
        {
            ieDL.ChangeIsExportFlag();
        }

    }
}
