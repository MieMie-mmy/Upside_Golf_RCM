using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
  public   class Mall_Setting_Yahoo_Fixed_Entity
    {
      private int id;
  
       public int ID 
       {
           get { return id; }
           set { id = value; }
       }

      private int shopID;
      public  int  Shop_ID
      {
          get { return shopID; }
           set { shopID= value; }
    
       }


      private string shopname;
      public  string Shop_Name
      {
          get { return shopname;  }
          set { shopname = value; }


      }


      private string  mallname;
      public string  Mall_Name
      {
          get { return mallname; }
          set { mallname = value; }


      }
    private  string  special_Price;
      public  string Special_Price
       {
           get { return special_Price; }
           set { special_Price = value; }
       }

       private string word_comment;
       public string  Word_Comment
       {
           get { return  word_comment; }
           set { word_comment= value; }
       }

       private int  taxable;
       public int  Taxable
       {
           get { return  taxable; }
           set { taxable= value; }
       
       }

       //private  DateTime  release_date;
       //public   DateTime  Release_Date 
       //{
       //    get { return  release_date; }
       //    set { release_date = value; }
       //}


       private DateTime? release_date;
       public DateTime? Release_Date 
       {
           get { return release_date; }
           set { release_date= value; }
       }



       private string provisional_period;
       public  string  Provisional_Period 
       {
           get { return provisional_period; }
           set { provisional_period = value; }
       }

       private string template;
       public string Template
       {
           get { return template; }
           set { template= value; }
       }

       private string  numofPurchases;
       public  string  NoofPurchases
       {
           get { return  numofPurchases; }
           set { numofPurchases = value; }
       }


       private int product_State;
       public int  Product_State
       {
           get { return  product_State; }
           set { product_State = value; }
       }


      private string  listing_Japan;
       public  string   Listing_Japan
       {
           get { return  listing_Japan; }
           set { listing_Japan= value; }
       }
    }
}

    

