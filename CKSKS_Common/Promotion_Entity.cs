using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Promotion_Entity
    {
        #region Private Variables
        private int id;
        private string promotion_Name;

        private string campaign_name;

        private string campaignID;
        private string campaignurl_pc;
        private string campaignurl_smart;
        private string remark;
        private string production_detail;
        private string subjects;
        private string target_brand;
        private string instruction_no;
        private string application_method;
        private string gift_content;
        private string gift_way;
        private string production_target;

        private string related_information;


        private string product_decoration;

        private string pc_Copy;


        private string smart_Copy;

        private string shop;





        private string mail_magazine_event1;

        private string mail_magzaine_event2;

        private string mail_magzaine_event3;

        private  Boolean   isgift;


        private Boolean  ispublic;

        private Boolean  blackmarket;



        private string priority;


      private  string  campaign_image1;
      private string campaign_image2;
      private string campaign_image3;
      private string campaign_image4;
      private string campaign_image5;
	









        private string campaign_Guideline;
        private string brand_Name;
        private DateTime? period_From;
        private DateTime? period_To;
        private string period_StartTime;
        private string period_EndTime;
        private string campaign_TypeID;
        private int rakuten_MagnificationID;
        private int yahoo_MagnificationID;
        private int ponpare_MagnificationID;
        private string secret_ID;
        private string secret_Password;
        private string product_DescriptionX;
        private string product_DescriptionY;
        private string sale_DescriptionX;
        private string sale_DescriptionY;
    
        private int status;
        private int isPromotionClose;
        private string product_Decoration;
        private string copy_Decoration;
        #endregion

        #region Public Properties
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        //public string Promotion_Name
        //{
        //    get { return promotion_Name; }
        //    set { promotion_Name = value; }
        //}

        public string Campaign_Guideline
        {
            get { return campaign_Guideline; }
            set { campaign_Guideline = value; }
        }
        //public string Brand_Name
        //{
        //    get { return brand_Name; }
        //    set { brand_Name = value; }
        //}
        public DateTime? Period_From
        {
            get { return period_From; }
            set { period_From = value; }
        }
        public DateTime? Period_To
        {
            get { return period_To; }
            set { period_To = value; }
        }
        public string Period_StartTime
        {
            get { return period_StartTime; }
            set { period_StartTime = value; }
        }
        public string Period_EndTime
        {
            get { return period_EndTime; }
            set { period_EndTime = value; }
        }
        public string Campaign_TypeID
        {
            get { return campaign_TypeID; }
            set { campaign_TypeID = value; }
        }
        public int Rakuten_MagnificationID
        {
            get { return rakuten_MagnificationID; }
            set { rakuten_MagnificationID = value; }
        }
        public int Yahoo_MagnificationID
        {
            get { return yahoo_MagnificationID; }
            set { yahoo_MagnificationID = value; }
        }
        public int Ponpare_MagnificationID
        {
            get { return ponpare_MagnificationID; }
            set { ponpare_MagnificationID = value; }
        }
        public string Secret_ID
        {
            get { return secret_ID; }
            set { secret_ID = value; }
        }
        public string Secret_Password
        {
            get { return secret_Password; }
            set { secret_Password = value; }
        }
        public string  PC_Campaign1
        {
            get { return product_DescriptionX; }
            set { product_DescriptionX = value; }
        }
        public string PC_Campaign2
        {
            get { return product_DescriptionY; }
            set { product_DescriptionY = value; }
        }
        public string Smart_Campaign1
        {
            get { return sale_DescriptionX; }
            set { sale_DescriptionX = value; }
        }
        public string Smart_Campaign2
        {
            get { return sale_DescriptionY; }
            set { sale_DescriptionY = value; }
        }
    
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public int IsPromotionClose
        {
            get { return isPromotionClose; }
            set { isPromotionClose = value; }
        }
        public string Product_Decoration
        {
            get { return product_Decoration; }
            set { product_Decoration = value; }
        }
        public string Copy_Decoration
        {
            get { return copy_Decoration; }
            set { copy_Decoration = value; }
        }




        public string Promotion_Name
        {
            get { return campaign_name; }
            set { campaign_name = value; }
        }
        
        public string Campaign_ID
        {
            get { return campaignID; }
            set { campaignID = value; }
        }


        public string CampaignUrl_PC
        {
            get { return campaignurl_pc; }
            set { campaignurl_pc = value; }
        }


        public string CampaignUrl_Smart
        {
            get { return campaignurl_smart; }
            set { campaignurl_smart = value; }
        }

        public string  Remark
        {
            get { return  remark; }
            set {  remark= value; }
        }



        public string Production_Detail
        {
            get { return production_detail; }
            set {production_detail = value; }
        }


        public string Subjects
        {
            get { return subjects; }
            set { subjects = value; }
        }


        public string Target_Brand
        {
            get { return target_brand; }
            set { target_brand = value; }
        }


        public string Instruction_No
        {
            get { return instruction_no; }
            set { instruction_no = value; }
        }

        public string Application_Method
        {
            get { return application_method; }
            set { application_method = value; }
        }


        public string Present_Contents
        {
            get { return gift_content; }
            set { gift_content = value; }
        }

        
        public string Present_Method
        {
            get { return gift_way; }
            set { gift_way = value; }
        }


        public string Production_Target
        {
            get { return production_target; }
            set { production_target = value; }
        }


        public Boolean IsPresent
        {
            get { return isgift; }
            set { isgift= value; }
        }




        public  Boolean IsPublic
        {
            get { return ispublic; }
            set { ispublic = value; }
        }



        public Boolean  Black_market_Setting
          {
             get { return  blackmarket; }
             set {  blackmarket = value; }
           }




        public String Related_Info_Ref
        {
            get { return  related_information; }
            set { related_information = value; }
        }





        public String PC_Copy_Decoration
        {

            get { return pc_Copy; }
             
            set {pc_Copy=value; }

         }

        public String Smart_Copy_Decoration
        {

            get { return smart_Copy; }

            set { smart_Copy = value; }

        }




        public String Priority
        {
            get { return priority; }
            set { priority = value; }
        }



        public String Mail_Magazine_Event1
        {

            get { return mail_magazine_event1; }

            set { mail_magazine_event1 = value; }


        }

        public String Mail_Magazine_Event2
        {

            get { return mail_magzaine_event2; }

            set { mail_magzaine_event2 = value; }


        }

        public String Mail_Magazine_Event3
        {

            get { return  mail_magzaine_event3; }

            set {mail_magzaine_event3 = value; }


        }

        //private string campaign_image1;




        public String Campaign_Image1
        {

            get { return campaign_image1; }

            set { campaign_image1 = value; }


        }

        public String Campaign_Image2
        {

            get { return campaign_image2; }

            set { campaign_image2 = value; }


        }
        public String Campaign_Image3
        {

            get { return campaign_image3; }

            set { campaign_image3 = value; }


        }
        public String Campaign_Image4
        {

            get { return campaign_image4; }

            set { campaign_image4 = value; }


        }
        public String Campaign_Image5
        {

            get { return campaign_image5; }

            set { campaign_image5 = value; }


        }


        public String Shop
        {

            get { return shop; }

            set { shop= value; }


        }


        #endregion

    }
}
