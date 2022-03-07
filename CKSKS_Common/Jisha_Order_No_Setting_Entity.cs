using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Jisha_Order_No_Setting_Entity
    {
        private DateTime date;
        private int orderNo;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public int OrderNo
        {
            get { return orderNo; }
            set { orderNo = value; }
        }
    }
}
