using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_Common
{
   public  class Email_Magazine_Entity
    {
            private int id;
            private string mail_magazine_id ;
            private string mail_magazine_name;
            private int shop_id;
            private string target_shop;
            private string delivery_time;
            private string title1;
            private int mall1;
            private string item_code1;
            private string item_page_url1;
            private string category_page_url1;
            private string title2;
            private int mall2;
            private string item_code2;
            private string item_page_url2;
            private string category_page_url2;
            private string mail;
            private string mail_magazine;
            private string mail_magazine_html;
            private string mail_magazine_text;
            private string shopname;
            private string campaign1;
            private string campaign2;
            private string campaign3;
            private string campaign4;
            private string campaign5;
            private string campaign6;
            private string campaign7;
            private string campaign8;
            private string campaign9;
            private string campaign10;
            private string campaign;
            private string campaignurl1;
            private string campaignurl2;
            private string campaignurl3;
            private string campaignurl4;
            private string campaignurl5;
            private string campaignurl6;
            private string campaignurl7;
            private string campaignurl8;
            private string campaignurl9;
            private string campaignurl10;
            private string promotion1;
            private string promotion2;
            private string promotion3;
            private string promotion4;
            private string promotion5;
            private string promotion6;
            private string promotion7;
            private string promotion8;
            private string promotion9;
            private string promotion10;
            private string mail_magazine_event11;
            private string mail_magazine_event21;
            private string mail_magazine_event31;
            private string mail_magazine_event12;
            private string mail_magazine_event22;
            private string mail_magazine_event32;
            private string mail_magazine_event13;
            private string mail_magazine_event23;
            private string mail_magazine_event33;
            private string mail_magazine_event14;
            private string mail_magazine_event24;
            private string mail_magazine_event34;
            private string mail_magazine_event15;
            private string mail_magazine_event25;
            private string mail_magazine_event35;
            private string mail_magazine_event16;
            private string mail_magazine_event26;
            private string mail_magazine_event36;
            private string mail_magazine_event17;
            private string mail_magazine_event27;
            private string mail_magazine_event37;
            private string mail_magazine_event18;
            private string mail_magazine_event28;
            private string mail_magazine_event38;
            private string mail_magazine_event19;
            private string mail_magazine_event29;
            private string mail_magazine_event39;
            private string mail_magazine_event110;
            private string mail_magazine_event210;
            private string mail_magazine_event310;
   
          
            public int ID
            {
                get { return id; }
                set { id = value; }
            }

            public string Mail_Magazine_ID
          {
              get { return mail_magazine_id; }
              set { mail_magazine_id = value; }
           }

            public string Mail_Magazine_Name
          {
              get { return mail_magazine_name; }
              set { mail_magazine_name = value; }
           }

            public int Shop_ID
            {
                get { return shop_id; }
                set { shop_id = value; }
            }
            private DateTime? delivery_date = null;
            public DateTime? Delivery_Date
            {
                get { return delivery_date; }
                set { delivery_date = value; }
            }
            public string Delivery_Time
            {
                get { return delivery_time; }
                set { delivery_time = value; }
            }

            public string Shopname
            {
                get { return shopname; }
                set { shopname = value; }
            }
            
            public string Target_Shop
            {
                get { return target_shop; }
                set { target_shop = value; }
            }
            public string Title1
            {
                get { return title1; }
                set { title1 = value; }
            }
            public int Mall1
            {
                get { return mall1; }
                set { mall1 = value; }
            }
            public string Item_Code1
            {
                get { return item_code1; }
                set { item_code1 = value; }
            }
            public string Item_PageURL1
            {
                get { return item_page_url1; }
                set { item_page_url1 = value; }
            }
            public string Category_PageURL1
            {
                get { return category_page_url1; }
                set { category_page_url1 = value; }
            }
            public string Title2
            {
                get { return title2; }
                set { title2 = value; }
            }
            public int Mall2
            {
                get { return mall2; }
                set { mall2 = value; }
            }
            public string Item_Code2
            {
                get { return item_code2; }
                set { item_code2 = value; }
            }
            public string Item_PageURL2
            {
                get { return item_page_url2; }
                set { item_page_url2 = value; }
            }
            public string Category_PageURL2
            {
                get { return category_page_url2; }
                set { category_page_url2 = value; }
            }
            public string Mail
            {
                get { return mail; }
                set { mail = value; }
            }
            public string Mail_Magazine
            {
                get { return mail_magazine; }
                set { mail_magazine = value; }
            }
            public string Mail_Magazine_HTML
            {
                get { return mail_magazine_html; }
                set { mail_magazine_html = value; }
            }
            public string Mail_Magazine_Text
            {
                get { return mail_magazine_text; }
                set { mail_magazine_text= value; }
            }

            public string Campaign
            {
                get { return campaign; }
                set { campaign= value; }
            }
            public string Campaign1
            {
                get { return campaign1; }
                set { campaign1 = value; }
            }
            public string Campaign2
            {
                get { return campaign2; }
                set { campaign2 = value; }
            }
            public string Campaign3
            {
                get { return campaign3; }
                set { campaign3 = value; }
            }
            public string Campaign4
            {
                get { return campaign4; }
                set { campaign4 = value; }
            }
            public string Campaign5
            {
                get { return campaign5; }
                set { campaign5 = value; }
            }
            public string Campaign6
            {
                get { return campaign6; }
                set { campaign6 = value; }
            }
            public string Campaign7
            {
                get { return campaign7; }
                set { campaign7 = value; }
            }
            public string Campaign8
            {
                get { return campaign8; }
                set { campaign8 = value; }
            }
            public string Campaign9
            {
                get { return campaign9; }
                set { campaign9 = value; }
            }
            public string Campaign10
            {
                get { return campaign10; }
                set { campaign10 = value; }
            }

            public string CampaignURL1
            {
                get { return campaignurl1; }
                set { campaignurl1 = value; }
            }

            public string CampaignURL2
            {
                get { return campaignurl2; }
                set { campaignurl2 = value; }
            }

            public string CampaignURL3
            {
                get { return campaignurl3; }
                set { campaignurl3 = value; }
            }
            
            public string CampaignURL4
            {
                get { return campaignurl4; }
                set { campaignurl4 = value; }
            }
            public string CampaignURL5
            {
                get { return campaignurl5; }
                set { campaignurl5= value; }
            }
            public string CampaignURL6
            {
                get { return campaignurl6; }
                set { campaignurl6 = value; }
            }
            public string CampaignURL7
            {
                get { return campaignurl7; }
                set { campaignurl7 = value; }
            }
            public string CampaignURL8
            {
                get { return campaignurl8; }
                set { campaignurl8 = value; }
            }
            public string CampaignURL9
            {
                get { return campaignurl9; }
                set { campaignurl9 = value; }
            }
            public string CampaignURL10
            {
                get { return campaignurl10; }
                set { campaignurl10 = value; }
            }

            public string Promotion1
            {
                get { return promotion1; }
                set { promotion1 = value; }
            }
            public string Promotion2
            {
                get { return promotion2; }
                set { promotion2 = value; }
            }
            public string Promotion3
            {
                get { return promotion3; }
                set { promotion3 = value; }
            }
            public string Promotion4
            {
                get { return promotion4; }
                set { promotion4 = value; }
            }
            public string Promotion5
            {
                get { return promotion5; }
                set { promotion5 = value; }
            }
            public string Promotion6
            {
                get { return promotion6; }
                set { promotion7 = value; }
            }
            public string Promotion7
            {
                get { return promotion7; }
                set { promotion7 = value; }
            }
            public string Promotion8
            {
                get { return promotion8; }
                set { promotion8 = value; }
            }   
                public string Promotion9
            {
                get { return promotion9; }
                set { promotion9 = value; }
            }
           
               public string Promotion10
                {
                    get { return promotion10; }
                    set { promotion10 = value; }
               }

               public string Mail_Magazine_Event11
               {
                   get { return mail_magazine_event11; }
                   set { mail_magazine_event11 = value; }
               }
               public string Mail_Magazine_Event21
               {
                   get { return mail_magazine_event21; }
                   set { mail_magazine_event21 = value; }
               }
               public string Mail_Magazine_Event31
               {
                   get { return mail_magazine_event31; }
                   set { mail_magazine_event31 = value; }
               }
               public string Mail_Magazine_Event12
               {
                   get { return mail_magazine_event12; }
                   set { mail_magazine_event12 = value; }
               }
               public string Mail_Magazine_Event22
               {
                   get { return mail_magazine_event22; }
                   set { mail_magazine_event22 = value; }
               }
               public string Mail_Magazine_Event32
               {
                   get { return mail_magazine_event32; }
                   set { mail_magazine_event32 = value; }
               }
               public string Mail_Magazine_Event13
               {
                   get { return mail_magazine_event13; }
                   set { mail_magazine_event13 = value; }
               }
               public string Mail_Magazine_Event23
               {
                   get { return mail_magazine_event23; }
                   set { mail_magazine_event23 = value; }
               }
               public string Mail_Magazine_Event33
               {
                   get { return mail_magazine_event33; }
                   set { mail_magazine_event33 = value; }
               }
               public string Mail_Magazine_Event14
               {
                   get { return mail_magazine_event14; }
                   set { mail_magazine_event14 = value; }
               }
               public string Mail_Magazine_Event24
               {
                   get { return mail_magazine_event24; }
                   set { mail_magazine_event24 = value; }
               }
               public string Mail_Magazine_Event34
               {
                   get { return mail_magazine_event34; }
                   set { mail_magazine_event34 = value; }
               }
               public string Mail_Magazine_Event15
               {
                   get { return mail_magazine_event15; }
                   set { mail_magazine_event15 = value; }
               }
               public string Mail_Magazine_Event25
               {
                   get { return mail_magazine_event25; }
                   set { mail_magazine_event25 = value; }
               }
               public string Mail_Magazine_Event35
               {
                   get { return mail_magazine_event35; }
                   set { mail_magazine_event35 = value; }
               }
               public string Mail_Magazine_Event16
               {
                   get { return mail_magazine_event16; }
                   set { mail_magazine_event16 = value; }
               }
               public string Mail_Magazine_Event26
               {
                   get { return mail_magazine_event26; }
                   set { mail_magazine_event26 = value; }
               }
               public string Mail_Magazine_Event36
               {
                   get { return mail_magazine_event36; }
                   set { mail_magazine_event36 = value; }
               }
               public string Mail_Magazine_Event17
               {
                   get { return mail_magazine_event17; }
                   set { mail_magazine_event17 = value; }
               }
               public string Mail_Magazine_Event27
               {
                   get { return mail_magazine_event27; }
                   set { mail_magazine_event27 = value; }
               }
               public string Mail_Magazine_Event37
               {
                   get { return mail_magazine_event37; }
                   set { mail_magazine_event37 = value; }
               }
               public string Mail_Magazine_Event18
               {
                   get { return mail_magazine_event18; }
                   set { mail_magazine_event18 = value; }
               }
               public string Mail_Magazine_Event28
               {
                   get { return mail_magazine_event28; }
                   set { mail_magazine_event28 = value; }
               }
               public string Mail_Magazine_Event38
               {
                   get { return mail_magazine_event38; }
                   set { mail_magazine_event38 = value; }
               }
               public string Mail_Magazine_Event19
               {
                   get { return mail_magazine_event19; }
                   set { mail_magazine_event19 = value; }
               }
               public string Mail_Magazine_Event29
               {
                   get { return mail_magazine_event29; }
                   set { mail_magazine_event29 = value; }
               }
               public string Mail_Magazine_Event39
               {
                   get { return mail_magazine_event39; }
                   set { mail_magazine_event39 = value; }
               }
               public string Mail_Magazine_Event110
               {
                   get { return mail_magazine_event110; }
                   set { mail_magazine_event110 = value; }
               }
               public string Mail_Magazine_Event210
               {
                   get { return mail_magazine_event210; }
                   set { mail_magazine_event210 = value; }
               }
               public string Mail_Magazine_Event310
               {
                   get { return mail_magazine_event310; }
                   set { mail_magazine_event310 = value; }
               }
    }
}
