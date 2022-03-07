using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Sale_ListScreen_Entity
    {
        #region Private Variables

        private int id;

        private string shopname;

        //private string email_date;

        private string quantity;

        private Decimal amount;

        private DateTime?  fromdate;
        private DateTime? todate;

        #endregion

        #region Public Properties
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public String Store_Name
        {
            get { return shopname; }
            set { shopname = value; }
        }


        public String OrderQty
        {
            get { return quantity; }
            set { quantity = value; }
        }

         public  Decimal  Amount
        {
            get { return amount; }
            set { amount = value; }
        }


         public DateTime? fromDate
         {
             get { return fromdate; }
             set { fromdate= value; }
         }

         public DateTime? toDate
         {
             get { return  todate; }
             set { todate = value; }
         }

        #endregion
    }




       
    }



    