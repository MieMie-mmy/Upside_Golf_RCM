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
    public class Promotion_Delivery_BL
    {
        Promotion_Delivery_DL pdl;
        public Promotion_Delivery_BL()
        {
            pdl = new Promotion_Delivery_DL();
        }

        public DataTable SelectallDataEqual(Promotion_Delivery_Entity pde, int opt, int pgindex, int psize)
        {
            return pdl.SelectAllEqual(pde, opt,pgindex,psize);
        }

        public DataTable SelectallData(Promotion_Delivery_Entity pde, int opt, int pgindex, int psize)
        {
            return pdl.SelectAll(pde, opt,pgindex,psize);
        }

        public DataTable DeliveryEqualSearch(Promotion_Delivery_Entity pde, int opt, int pgindex, int psize)
        {
            return pdl.DeliveryEqualSearch(pde, opt, pgindex, psize);
        }

        public DataTable SelectIsDelivery(Promotion_Delivery_Entity pde, int opt, int pgindex, int psize)
        {
            return pdl.SelectIsDelivery(pde, opt, pgindex, psize);
        }
        public DataTable Search(Promotion_Delivery_Entity pde,int shpdl)
        {
            return pdl.Search(pde,shpdl);
        }

        public int Save(DataTable dt)
        {
            return pdl.SaveUpdate(dt);
        }
    }
}
