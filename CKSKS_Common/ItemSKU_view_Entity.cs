using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
   public  class ItemSKU_view_Entity
    {


       private string size_code;
       private string  size_name;
       private string color_code;
       private string color_name;

       public string Size_Code
       {
           get { return size_code; }
           set { size_code = value; }
       }
      
       public string Size_Name
       {
           get { return size_name; }
           set { size_name = value; }
       }

       public string Color_Code
       {
           get { return color_code; }
           set { color_code = value; }
       }

       public string Color_Name
       {
           get { return color_name; }
           set {color_name= value; }
       }

    }
}
