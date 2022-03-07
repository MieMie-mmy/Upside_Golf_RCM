using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
   public  class User_entity
    {
        private int id;
       private string username;
       private string  loginID;
       private string password;
       private int status;
       private  DateTime createdDate;
       private DateTime  updatedDate;
    
       public int ID 
       {
           get { return id; }
           set { id = value; }
       }

       public string User_Name
       {
           get { return username;  }
           set { username= value; }
       }
       public string Login_ID
       {
           get { return loginID; }
           set { loginID = value; }
       }

       public string Password
       {
           get { return password;  }
           set {  password= value; }
       
       }

       public int  Status
       {
           get { return status; }
           set { status= value; }

       }

       public DateTime Created_Date
       {
           get { return createdDate; }
           set { createdDate = value; }

       }


       public DateTime  Updated_Date
       {
           get { return updatedDate; }
           set { updatedDate =value; }

       }

        }
    }

