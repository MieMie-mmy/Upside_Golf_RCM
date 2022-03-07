using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Campaign_Entity
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

        private String shop;

        private String shop_Name;

        private string mail_magazine_event1;

        private string mail_magzaine_event2;

        private string mail_magzaine_event3;

        private Boolean isgift;


        private Boolean ispublic;

        private Boolean blackmarket;



        private string priority;


        private string campaign_image1;
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
        public string PC_Campaign1
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

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }



        public string Production_Detail
        {
            get { return production_detail; }
            set { production_detail = value; }
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
            set { isgift = value; }
        }




        public Boolean IsPublic
        {
            get { return ispublic; }
            set { ispublic = value; }
        }



        public Boolean Black_market_Setting
        {
            get { return blackmarket; }
            set { blackmarket = value; }
        }




        public String Related_Info_Ref
        {
            get { return related_information; }
            set { related_information = value; }
        }





        public String PC_Copy_Decoration
        {

            get { return pc_Copy; }

            set { pc_Copy = value; }

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

            get { return mail_magzaine_event3; }

            set { mail_magzaine_event3 = value; }


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


        public  String Shop
        {

            get { return shop; }

            set { shop = value; }


        }

        public String Shop_Name
        {

            get { return shop_Name; }

            set { shop_Name = value; }


        }


        #endregion

        #region option
        private String optionValue1;
        public String OptionValue1
        {
            get { return optionValue1; }
            set { optionValue1 = value; }
        }

        private String optionValue2;
        public String OptionValue2
        {
            get { return optionValue2; }
            set { optionValue2 = value; }
        }

        private String optionValue3;
        public String OptionValue3
        {
            get { return optionValue3; }
            set { optionValue3 = value; }
        }

        private String optionName1;
        public String OptionName1
        {
            get { return optionName1; }
            set { optionName1 = value; }
        }

        private String optionName2;
        public String OptionName2
        {
            get { return optionName2; }
            set { optionName2 = value; }
        }

        private String optionName3;
        public String OptionName3
        {
            get { return optionName3; }
            set { optionName3 = value; }
        }
        #endregion

        private String gold_attach1;
        public String Gold_attach1
        {
            get { return gold_attach1; }
            set { gold_attach1 = value; }
        }

        private String gold_attach2;
        public String Gold_attach2
        {
            get { return gold_attach2; }
            set { gold_attach2 = value; }
        }

        private String gold_attach3;
        public String Gold_attach3
        {
            get { return gold_attach3; }
            set { gold_attach3 = value; }
        }

        private String gold_attach4;
        public String Gold_attach4
        {
            get { return gold_attach4; }
            set { gold_attach4 = value; }
        }

        private String cabinet_attach1;
        public String Cabinet_attach1
        {
            get { return cabinet_attach1; }
            set { cabinet_attach1 = value; }
        }

        private String cabinet_attach2;
        public String Cabinet_attach2
        {
            get { return cabinet_attach2; }
            set { cabinet_attach2 = value; }
        }

        private String cabinet_attach3;
        public String Cabinet_attach3
        {
            get { return cabinet_attach3; }
            set { cabinet_attach3 = value; }
        }

        private String cabinet_attach4;
        public String Cabinet_attach4
        {
            get { return cabinet_attach4; }
            set { cabinet_attach4 = value; }
        }

        private String geocities_attach1;
        public String Geocities_attach1
        {
            get { return geocities_attach1; }
            set { geocities_attach1 = value; }
        }

        private String geocities_attach2;
        public String Geocities_attach2
        {
            get { return geocities_attach2; }
            set { geocities_attach2 = value; }
        }

        private String geocities_attach3;
        public String Geocities_attach3
        {
            get { return geocities_attach3; }
            set { geocities_attach3 = value; }
        }

        private String geocities_attach4;
        public String Geocities_attach4
        {
            get { return geocities_attach4; }
            set { geocities_attach4 = value; }
        }


        private String  ponpare_attach1;
        public String Ponpare_attach1
        {
            get { return ponpare_attach1; }
            set { ponpare_attach1 = value; }
        }

        private String ponpare_attach2;
        public String Ponpare_attach2
        {
            get { return ponpare_attach2; }
            set { ponpare_attach2 = value; }
        }

        private String ponpare_attach3;
        public String Ponpare_attach3
        {
            get { return ponpare_attach3; }
            set { ponpare_attach3 = value; }
        }

        private String ponpare_attach4;
        public String Ponpare_attach4
        {
            get { return ponpare_attach4; }
            set {ponpare_attach4 = value; }
        }




        private String image1;
        public String Image1
        {
            get { return image1; }
            set { image1 = value; }
        }

        private String image2;
        public String Image2
        {
            get { return image2; }
            set { image2= value; }
        }

        private String image3;
        public String Image3
        {
            get { return image3; }
            set { image3 = value; }
        }

        private String image4;
        public String Image4
        {
            get { return image4; }
            set { image4= value; }
        }

        private String image5;
        public String Image5
        {
            get { return image5; }
            set { image5= value; }
        }


        private String  item;
        public String Item
        {
            get { return item; }
            set {item= value; }
        }

        private String item_memo;
        public String Item_Memo
        {
            get { return item_memo; }
            set { item_memo = value; }
        }

     
    }
}
