using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_Common
{
    public class Mall_Setting_Rakhutan_Default_Entity
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int shopid;
        public int ShopID
        {
            get { return shopid; }
            set { shopid = value; }
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
            get { return mallname; }
            set { mallname = value; }
        }




        private int post;
        public int Postage
        {
            get { return post; }
            set { post = value; }
        }

        private string extrashop;
        public string ExtraShop
        {
            get { return extrashop; }
            set { extrashop = value; }
        }

        private int delivery;
        public int Delivery_Charges
        {
            get { return delivery; }
            set { delivery = value; }

        }

        private int warehouse;
        public int Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; }
        }

        private int searchhide;
        public int Searchhide
        {
            get { return searchhide; }
            set { searchhide = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private int dualprice;
        public int DualPrice
        {
            get { return dualprice; }
            set { dualprice = value; }
        }

        private string featureItem;
        public string Featured_Item
        {
            get { return featureItem; }
            set { featureItem = value; }

        }


    }

}