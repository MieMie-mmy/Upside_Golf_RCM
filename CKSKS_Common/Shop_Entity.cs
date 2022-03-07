using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ORS_RCM_Common
{
   public class Shop_Entity
    {
       private int id;
       private string shopID;
       private string shopname;
       private string shopopen;
       private string ftphost;
       private string ftpacc;
       private string ftppass;
       private string shpurl;
       private string imgurl;
       private  int freeshipping;
       private int status;

       private string libftphost;
       private string libftpacc;
       private string libftppass;
       private string libdirectory;

       public int ID 
       {
           get { return id; }
           set { id = value; }
       }

       public string ShopID
       {
           get { return shopID; }
           set { shopID = value; }
       }
       public string ShopName
       {
           get { return shopname; }
           set { shopname = value; }
       }

       public string MallOpen 
       {
           get { return shopopen; }
           set { shopopen = value; }
       
       }

       public string FTPhost 
       {
           get { return ftphost; }
           set { ftphost = value; }
       
       }

       public string FTPacc
       {
           get { return ftpacc; }
           set { ftpacc = value; }
       }


       public string FTPpass 
       {
           get { return ftppass; }
           set { ftppass = value; }
       
       }

       public string shpURL
       {
           get { return shpurl; }
           set { shpurl = value; }

       }
       public string imgURL
       {
           get { return imgurl; }
           set { imgurl = value; }
       }

       public int Status 
       {
           get { return status; }
           set { status = value; }
       
       }

       public int Shipping_Condition
       {
           get { return freeshipping; }
           set { freeshipping = value; }
       }

       //updated date 15/06/2015
       public string Libftphost
       {
           get { return libftphost; }
           set { libftphost = value; }

       }
       public string Libftpacc
       {
           get { return libftpacc; }
           set { libftpacc = value; }
       }
       public string Libftppass
       {
           get { return libftppass; }
           set { libftppass = value; }
       }

       public string Libdirectory
       {
           get { return libdirectory; }
           set { libdirectory = value; }
       }

       private int Categorycheck = 0;
       public int Categorycheck1
       {
           get { return Categorycheck; }
           set { Categorycheck = value; }
       }
    }
}
