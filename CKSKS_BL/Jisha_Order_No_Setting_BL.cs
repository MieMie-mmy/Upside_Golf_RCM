using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ORS_RCM_DL;
using ORS_RCM_Common;

namespace ORS_RCM_BL
{
    public class Jisha_Order_No_Setting_BL
    {
        Jisha_Order_No_Setting_DL jishaOrderNoDL;

        public Jisha_Order_No_Setting_BL()
        {
            jishaOrderNoDL = new Jisha_Order_No_Setting_DL();
        }

        public DataTable SelectByCurrentDate()
        {
            return jishaOrderNoDL.SelectByCurrentDate();
        }

        public void Insert(Jisha_Order_No_Setting_Entity jishaOrderNoInfo)
        {
            jishaOrderNoDL.Insert(jishaOrderNoInfo);
        }
    }
}
