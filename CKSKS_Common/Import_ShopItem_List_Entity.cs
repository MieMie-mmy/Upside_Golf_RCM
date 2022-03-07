using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
   public  class Import_ShopItem_List_Entity
    {
        private int id;
       private int shopID;
       private string shop;
       private string shopname;
       private int mallID;
       private string ctrl_ID;
       private string  item_AdminCode;
       private string  item_code;
       private string  item_Name;
       private string point_Code;
       private string  point_term;
       private DateTime createdDate;

       public int ID 
       {
           get { return id; }
           set { id = value; }
       }

       public int Shop_ID
       {
           get { return shopID; }
           set { shopID = value; }
       }

       public string Shop
       {
           get { return shop; }
           set { shop = value; }
       }

       public  string Shop_Name
       {
           get { return shopname; }
           set { shopname = value; }
       }
       public int MallID
       {
           get { return mallID; }
           set { mallID = value; }
       }

       public string Ctrl_ID
       {
           get { return ctrl_ID; }
           set { ctrl_ID = value; }
       }

       public string Item_AdminCode
       {
           get { return item_AdminCode; }
           set { item_AdminCode = value; }
       
       }

       public string  Item_Code
       {
           get { return  item_code; }
           set {  item_code = value; }
       
       }

       public string  Item_Name
       {
           get { return item_Name; }
           set { item_Name = value; }
       }


       public string Point_Code
       {
           get { return point_Code; }
           set { point_Code= value; }
       
       }

       public string Point_Term
       {
           get { return point_term; }
           set {point_term= value; }
       }

       public DateTime Created_Date
       {
           get { return  createdDate; }
           set { createdDate= value; }
       
       }
    }
}
   