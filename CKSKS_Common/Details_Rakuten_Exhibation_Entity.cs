using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Details_Rakuten_Exhibation_Entity
    {
       private int id;
       private string shopID;
       private string tag_id;
       private string shopname;
       private int consumptionTax;
       private string Stockdate;

       public string StockDate 
       {
           get { return Stockdate; }
           set { Stockdate = value; }
       
       }

       public int ID
       {
           get { return id; }
           set { id = value; }
       }

       public string Shop_ID
       {
           get { return shopID; }
           set { shopID = value; }
       }


       public string Shop_Name
       {
           get { return shopname; }
           set { shopname = value; }
       }
       public string Tag_ID
       {
           get { return tag_id; }
           set { tag_id = value; }
       }

       public  int Consumption_Tax
       {
           get { return consumptionTax; }
           set { consumptionTax = value; }

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


        

       private string  extra_shopping;
       public string Extra_Shopping
       {
           get { return extra_shopping;  }
           set { extra_shopping = value; }
       }



       private string orderinfo;
       public string OrderInfo 
       {
           get { return orderinfo; }
           set { orderinfo = value; }
       }

       private string product_information;
       public string Product_Information	
       {
           get { return product_information; }
           set { product_information = value; }
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
       private DateTime? orderrelease=null;
       public DateTime? Orderrelease 
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
 

       private int shopid;
       public int ShopID
       {
           get { return shopid; }
           set { shopid = value; }
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

       private string blackMarket_password;
       public string BlackMarket_Password
       {
           get { return blackMarket_password; }
           set { blackMarket_password = value; }
       }


       private  string size_chartlink;
       public  string Size_Chartlink
       {
           get { return size_chartlink; }
           set { size_chartlink= value; }
       }

       private string drug_description;
       public string Drug_Description
       {
           get { return drug_description; }
           set { drug_description= value; }

       }


       private string drug_Note;
       public string Drug_Note
       {
           get { return drug_Note; }
           set { drug_Note = value; }

       }






       private int dualprice;
       public int DualPrice 
       {
           get { return dualprice; }
           set { dualprice = value; }
       }
    }

      
       
    }



