using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Upside_Golf_RCM_DL;

namespace Upside_Golf_RCM_BL
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
