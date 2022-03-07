using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
   public  class Details_Ponpare_Exhibation_Entity
    {

       private int id;
       public int ID
       {
           get { return id; }
           set { id = value; }
       }

       private int shop_id;
       public int Shop_ID
       {
           get { return shop_id; }
           set { shop_id = value; }
       }

       private string  shop_name;
       public  string Shop_Name
       {
           get { return shop_name; }
           set { shop_name = value; }
       }


       private int status;
       public int Sale_Status
       {
           get { return status; }
           set { status = value; }
       }

       private int consumptionTax;
       public int Consumption_Tax
       {
           get { return consumptionTax; }
           set { consumptionTax = value; }

       }
       private int postage;
       public int Postage
       {
           get { return postage; }
           set { postage = value; }
       }

       private string  extra_shipping;
       public string Extra_Shipping
       {
           get { return extra_shipping; }
           set { extra_shipping = value; }
       }

       private int  deliveryCharges;
       public  int Delivery_Charges
       {
           get { return deliveryCharges; }
           set { deliveryCharges = value; }
       }

       private string secretpassword;
       public string SaleSecret_Password
       {
           get { return secretpassword; }
           set { secretpassword = value; }
       }

       private string shipping_group1;
       public string Shipping_Group1
       {
           get { return shipping_group1; }
           set { shipping_group1 = value; }
       }

       private string shipping_group2;
       public string Shipping_Group2
       {
           get { return shipping_group2; }
           set { shipping_group2 = value; }
       }

       private string expand_code;
       public string Expand_Cope
       {
           get { return expand_code; }
           set { expand_code = value; }
       }


       private string order_button;
       public string Order_Button
       {
           get { return order_button; }
           set { order_button = value; }
       }

       private string inquiry_button;
       public string Inquiry_Button
       {
           get { return inquiry_button; }
           set { inquiry_button = value; }
       }

       private string numofAcceptances;
       public string NoofAcceptances
       {
           get { return numofAcceptances; }
           set { numofAcceptances = value; }
       }

       private string stock_type;
       public string Stock_Type
       {
           get { return stock_type; }
           set { stock_type = value; }
       }

       private string stock_quantity;
       public string Stock_Quantity
       {
           get { return stock_quantity; }
           set { stock_quantity = value; }
       }

       private string stock_display;
       public string Stock_Display
       {
           get { return stock_display; }
           set { stock_display = value; }
       }

       private string  horizontal_itemName;
       public string Horizontal_ItemName
       {
           get { return horizontal_itemName; }
           set { horizontal_itemName = value; }
       }

       private string vertical_Itemname;
       public string Vertical_ItemName
       {
           get { return vertical_Itemname; }
           set { vertical_Itemname = value; }
       }

       private string remainingstock;
       public string Remaining_Stock
       {
           get { return remainingstock; }
           set { remainingstock= value; }
       }

       private string  jan_code;
       public string JAN_Code
       {
           get { return jan_code;  }
           set {  jan_code = value; }
       }


    }
}
