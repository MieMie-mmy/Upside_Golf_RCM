using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
    public class Jisha_Tax_Setting_BL
    {
        Jisha_Tax_Setting_DL jishaTaxSettingDL;
        public Jisha_Tax_Setting_BL()
        {
            jishaTaxSettingDL = new Jisha_Tax_Setting_DL();
        }

        public DataTable SelectLatestJishaTaxSetting()
        {
            return jishaTaxSettingDL.SelectLatestJishaTaxSetting();
        }
    }
}
