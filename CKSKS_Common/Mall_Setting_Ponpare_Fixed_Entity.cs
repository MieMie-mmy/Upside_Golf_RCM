using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_Common
{
    public class Mall_Setting_Ponpare_Fixed_Entity
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

        private int contax;
        public int Comtax 
        {
            get { return contax; }
            set { contax = value; }
        }

        private string shipg1;
        public string Shipg1 
        {
            get { return shipg1; }
            set { shipg1 = value; }
        }

        private string shipg2;
        public string Shipg2 
        {
            get { return shipg2; }
            set { shipg2 = value; }
        }

        private int expandcope;
        public int Expandcope 
        {
            get { return  expandcope; }
            set { expandcope = value; }
        }


        private int orderbut;
        public int Orderbut 
        {
            get { return orderbut; }
            set { orderbut = value; }
        }

        private int inqbut;
        public int Inqbut 
        {
            get { return inqbut; }
            set { inqbut = value; }
        }

        private string  noaccept;
        public string  NoAccept 
        {
            get { return noaccept; }
            set { noaccept = value; }
        }

        private int stocktype;
        public int Stocktype 
        {
            get { return stocktype; }
            set { stocktype = value; }
        }

        private string stockquantity;
        public string Stockquantity 
        {
            get { return stockquantity; }
            set { stockquantity = value; }
        }

        private int stockdisplay;
        public int Stockdisplay 
        {
            get { return stockdisplay; }
            set { stockdisplay = value; }
        }

        private string hitemname;
        public string Hitemname 
        {
            get { return hitemname; }
            set { hitemname = value; }
        }

        private string vitemname;
        public string Vitemname 
        {
            get { return vitemname; }
            set { vitemname = value; }
        }

        private string remaing;
        public string Remaining 
        {
            get { return remaing; }
            set { remaing = value; }
        }

        private string jan;
        public string Jan 
        {
            get { return jan; }
            set { jan = value; }
        }

        private string shopname;
        public string ShopName
        {
            get { return shopname; }
            set { shopname = value; }
        }

        private string mallname;
        public string MallName 
        {
            get { return mallname; }
            set { mallname = value; }
        }
    }
}
