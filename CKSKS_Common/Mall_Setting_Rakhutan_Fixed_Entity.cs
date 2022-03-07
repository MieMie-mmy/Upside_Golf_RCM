using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;



namespace ORS_RCM_Common
{
   public  class Mall_Setting_Rakhutan_Fixed_Entity
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

       private string shop_id;
       public string Shop_Name
       {
           get { return shop_id;  }
           set { shop_id = value; }
       }

       private  string mallname;
       public string Mall_Name
       {
           get { return mallname; }
           set { mallname = value; }
       }


       private  string tagid;
       public string  TagID 
       {
           get { return tagid; }
           set { tagid = value; }
       }

       private int consumption_tax;
       public int Comsumption_Tax
       {
           get { return consumption_tax; } 
           set { consumption_tax = value; } 
       }

       private string shipping_Category1;
       public string ShipCat1 
       { 
           get { return shipping_Category1; } 
           set { shipping_Category1 = value; }
       }

       private string shipping_Category2;
       public string ShipCat2
       {
           get { return shipping_Category2; }
           set { shipping_Category2 = value; }
       }

       private string orderinfo;
       public string OrderInfo 
       {
           get { return orderinfo; }
           set { orderinfo = value; }
       }

       private int orderbutton;
       public int Orderbuttton 
       {
           get {return orderbutton; }
           set { orderbutton = value; }
       }

       private int requestbutton;
       public int Requestbutton 
       {
           get { return requestbutton; }
           set { requestbutton = value; }
       }

       private int productbutton;
       public int ProductInquerybutton 
       {
           get {  return productbutton; }
           set { productbutton = value; }
       }

       private int commingsoonbutton;
       public int Comingsoonbut 
       {
           get { return commingsoonbutton; }
           set { commingsoonbutton = value; }
       }

       private int mobile;
       public int Mobile 
       {
           get { return mobile; }
           set { mobile = value; }
       }

       private int expandcope;
       public int Expandcope 
       {
           get { return expandcope; }
           set { expandcope = value; }
       }

       private string animation;
       public string Animation 
       {
           get { return animation; }
           set { animation = value; }
       }

       private string acceptno;
       public string AcceptNo 
       {
           get { return acceptno; }
           set { acceptno = value; }
       }

       private int stocktype;
       public int Stocktype 
       {
           get { return stocktype; }
           set { stocktype = value; }
       }
       private int stockquantity;
       public int Stockquantity 
       {
           get { return stockquantity; }
           set { stockquantity = value; }
       }

       private string stocknodisplay;
       public string StocknoDisplay 
       {
           get { return stocknodisplay; }
           set { stocknodisplay = value; }
       }

       private string hozitemname;
       public string Hozitemname 
       {
           get { return hozitemname; }
           set { hozitemname = value; }
       }

       private string  vername;
       public string  VarItemname 
       {
           get { return vername; }
           set { vername = value; }
       }

       private string remainstock;
       public string Remainstock 
       {
           get { return remainstock; }
           set { remainstock = value; }
       }

       private string RACno;
       public string RACNO 
       {
           get { return RACno; }
           set { RACno = value; }
       }

       private string catid;
       public string CatID 
       {
           get { return catid; }
           set { catid = value; }
       }

       private int flagback;
       public int Flagback 
       {
           get { return flagback; }
           set { flagback = value; }
       }

       private int orderrecpt;
       public int Orderrecpt 
       {
           get { return orderrecpt; }
           set { orderrecpt = value; }
       }

       private string delctrno;
       public string DelctrNo 
       {
           get { return delctrno; }
           set { delctrno = value; }
       }


       private string delctrno_outofstock;
       public string DelctrNo_outofstock
       {
           get { return delctrno_outofstock; }
           set { delctrno_outofstock = value; }
       }

       
       //private DateTime? orderrelease=null;
       //public DateTime? Orderrelease 
       //{
       //    get { return orderrelease; }
       //    set { orderrelease = value; }
       //}


       private DateTime? orderrelease;
       public DateTime ?Orderrelease
       {
           get { return orderrelease; }
           set { orderrelease = value; }
       }





       private String headfooter;
       public String Headfooter 
       {
           get { return headfooter; }
           set { headfooter = value; }
       }

       private  String displayorder;
       public  String  Displayorder 
       {
           get { return  displayorder; }
           set { displayorder = value; }
       }

       private String common1;
       public String Commondesc1
       {
           get { return common1; }
           set { common1 = value; }
       
       }
       private String common2;
       public String Commondesc2
       {
           get { return common2; }
           set { common2 = value; }

       }

       private int reviewtax;
       public int Reviewtax 
       {
           get { return reviewtax; }
           set { reviewtax = value; }
       
       }


    



       private String oversea;
       public String Oversea 
       {
           get { return oversea; }
           set { oversea = value; }
       }

       private String chartlink;
       public String Chartlink 
       {
           get { return chartlink; }
           set { chartlink = value; }
       }

       private String drugdesc;
       public String Drugdesc 
       {
           get { return drugdesc; }
           set { drugdesc = value; }
       }

       private String drugnote;
       public String Drugnote 
       {
           get { return drugnote; }
           set { drugnote = value; }
       }
    }
}
