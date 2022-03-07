using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;
using ORS_RCM_Common;

namespace ORS_RCM_BL
{
    public class Shop_Template_BL
    {
        public Shop_Template_DL shop_TempDL;

        public Shop_Template_BL()
        {
            shop_TempDL = new Shop_Template_DL();
        }

        public void Insert(DataTable dt)
        {
            shop_TempDL.Insert(dt);
        }

        public void Update(DataTable dt)
        {
            shop_TempDL.Update(dt);
        }

        public DataTable GetTemplateDescription(string[] templateID, int shopID)
        {
            return shop_TempDL.GetTemplateDescription(templateID, shopID);
        }

        public DataTable GetTemplateZettDescription(string[] templateID, string itemcode, string columnName)
        {
            return shop_TempDL.GetTemplateZettDescription(templateID, itemcode, columnName);
        }
    }
}
