using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Mall_Setting_Yahoo_Default_Entity
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int  shop_id;
        public int Shop_ID
        {
            get { return shop_id; }
            set { shop_id= value; }
        }


        private string shopname;
        public string Shop_Name
        {
            get { return shopname; }
            set { shopname = value; }


        }

        private string mallname;
        public string Mall_Name
        {
            get { return  mallname; }
            set { mallname = value; }


        }




        private string weight;
        public string Weight
        {
            get { return weight; }
            set { weight = value; }
        }
   }
}
