using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
     public class Item_ExportField_Entity
    {
         private int id;
         public int ID 
         {
             get { return id; }
             set { id = value; }
         }
         private string name;
         public string Name 
         {
             get { return name; }
             set { name = value; }
         }

         private string field;
         public string Field 
         {
             get { return field; }
             set { field = value; }
         }

         private int status;
         public int Status 
         {
             get { return status; }
             set { status = value; }
         }
    }
}
