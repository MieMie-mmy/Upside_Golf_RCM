using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using System.Data;

namespace ORS_RCM_BL
{
    public class Exhibition_Error_BL
    {
        Exhibition_Error_DL exErrdl;

        public Exhibition_Error_BL()
        {
            exErrdl = new Exhibition_Error_DL();
        }

        public DataTable SelectExhibitionError()
        {
            return exErrdl.SelectExhibitionError();
        }
        public DataTable SelectExhibitionInfo()
        {
            return exErrdl.SelectExhibitionInfo();

        }

        public DataTable SelectSalePrice()
        {
            return exErrdl.SelectSalePrice();
        }
        public DataTable SelectOrderCount()
        {
            return exErrdl.SelectOrderCount();
        }
        public DataTable SelectWaitingItemCode()
        {
            return exErrdl.SelectWaitingItemCode();
        }
        
    }   
}
