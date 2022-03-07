using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
    public class Delivery_BL
    {
        Delivery_DL dbl;
        public Delivery_BL()
        {
            dbl = new Delivery_DL();
        }

        public DataSet SelectAll()
        {
            return dbl.SelectAll();
        }

        public bool Insert(int yshippingno, int rshippingno, string ordersetting, int userid)
        {
            return dbl.Insert(yshippingno, rshippingno, ordersetting, userid);
        }

        public bool Update(int yshippingno, int rshippingno, string ordersetting, int userid, int id)
        {
            return dbl.Update(yshippingno, rshippingno, ordersetting, userid, id);
        }

        public void Delete(int id)
        {
            dbl.Delete(id);
        }

        public int CheckExistingData(int yshippingno, int rshippingno, int option)
        {
            return dbl.CheckExistingData(yshippingno, rshippingno, option);
        }
        public int SelectShippingNumber(int yshippingno, int rshippingno, int option, int id)
        {
            return dbl.SelectShippingNumber(yshippingno, rshippingno, option, id);
        }

    }
}
