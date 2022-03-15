using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Upside_Golf_RCM_DL;
using System.Data;

namespace Upside_Golf_RCM_BL
{
    public class Jisha_OrderDetail_BL
    {
        Jisha_OrderDetail_DL joddl;
        public Jisha_OrderDetail_BL()
        {
            joddl = new Jisha_OrderDetail_DL();
        }

        public DataTable SelectAll()
        {
            return joddl.SelectAll();
        }
    }
}
