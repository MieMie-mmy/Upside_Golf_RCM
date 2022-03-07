using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_Common
{
   public class Promotion_Delivery_Entity
    {
        private int id;
        private string itemcode;
        private string itemname;
        private string brandname;
        private string shopname;
        private string shipping;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Itemcode
        {
            get { return itemcode; }
            set { itemcode = value; }
        }

        public string Itemname
        {
            get { return itemname; }
            set { itemname = value; }
        }

        public string Brandname
        {
            get { return brandname; }
            set { brandname = value; }
        }

        public string Shopnmae
        {
            get { return shopname; }
            set { shopname = value; }
        }

        public string Shipping
        {
            get { return shipping; }
            set { shipping = value; }
        }


        private DateTime? rperiod = null;
        public DateTime? Rperiodto
        {
            get { return rperiod; }
            set { rperiod = value; }
        }

        private DateTime? rperiodfrom = null;
        public DateTime? Rperiodfrom
        {
            get { return rperiodfrom; }
            set { rperiodfrom = value; }
        }

        private int Rp;
        public int RP
        {
            get { return Rp; }
            set { Rp = value; }
        }
        private string rstart;
        public string Rstart
        {
            get { return rstart; }
            set { rstart = value; }
        }

        private string rend;
        public string Rend
        {
            get { return rend; }
            set { rend = value; }
        }

        private DateTime? rpperiodfrom = null;
        public DateTime? RPointperiodfrom
        {
            get { return rpperiodfrom; }
            set { rpperiodfrom = value; }
        }

        private DateTime? rpperiod = null;
        public DateTime? RPointperiodto
        {
            get { return rpperiod; }
            set { rpperiod = value; }
        }

    }
}
