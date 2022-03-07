using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
  public  class Shop_Template_Entity
    {
       public int shop_id=0;
        public string  template_description;
        public int tempId;

        public int TempID 
        {
            get { return tempId; }
            set { tempId = value; }
        
        }
        public int Shop_ID
        {
           
            get { return shop_id; }
            set { shop_id = value; }
        }



     

        public string Template_Description
        {
            get { return template_description; }
            set { template_description = value; }
        }




    }
}
