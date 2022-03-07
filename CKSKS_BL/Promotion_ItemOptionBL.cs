using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using System.Data;

namespace ORS_RCM_BL
{
    public class Promotion_ItemOptionBL
    {
        Promotion_ItemOptionDL optionDL;

        public Promotion_ItemOptionBL()
        {
            optionDL = new Promotion_ItemOptionDL();
        }

        public bool Save(int promotionID ,DataTable dtOption)
        {
            if (optionDL.Delete(promotionID))
            {
                return optionDL.Save(dtOption);
            }
            else
            {
                return false;
            }
        }

        public DataTable SelectByPID(int pid)
        {
            return optionDL.SelectByPID(pid);
        }

        public DataTable SelectAll()
        {
            return optionDL.SelectAll();
        }
    }
}
