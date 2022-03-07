using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace ORS_RCM_Common
{
    public class Mall_Setting_Ponpare_Default_Entity
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int shopid;
        public int Shop_ID
        {
            get { return shopid; }
            set { shopid = value; }
        }

        private int status;
        public int Sale_Status 
        {
            get { return status; }
            set { status = value; }
        }

        private int post;
        public int Post 
        {
            get { return post; }
            set { post = value; }
        }


        private string ship;
        public string Ship 
        {
            get { return ship; }
            set { ship = value; }
        }

        private int delivery;
        public int Delivery 
        {
            get { return delivery; }
            set { delivery = value; }
        }

        private string password;
        public string  Password
        {
            get { return password; }
            set { password = value; }
    }
        private string malldesc;
        public string MallDesc 
        {
            get { return malldesc; }
            set { malldesc = value; }
        
        }
        private string shopname;
        public string Shopname
        {
            get { return shopname; }
            set { shopname = value; }

        }
    }
}
