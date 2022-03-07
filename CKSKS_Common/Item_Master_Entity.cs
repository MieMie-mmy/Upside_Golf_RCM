using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Item_Master_Entity
    {
        #region Private Variables
        private int id=0;
        private int updated_by = 0;
        private string ctrl_ID = string.Empty;
        private string sale_Code=String.Empty;
        private string product_Code = String.Empty;
        private string item_Code = String.Empty;
        private string item_AdminCode = String.Empty;
        private string item_Name = String.Empty;
        private int list_Price = 0;
        private int jisha_Price = 0;
        private int sale_Price = 0;
        private int cost=0;
        private DateTime? release_Date;
        private DateTime? post_Available_Date;
        private string year = String.Empty;
        private string season = String.Empty;
        private string brand_Name = String.Empty;
        private string brand_Code = String.Empty;
        private string brand_Code_Yahoo = String.Empty;
        private string competition_Code = String.Empty;
        private string competition_Name = String.Empty;
        private string classification_Code = String.Empty;
        private string class_Name = String.Empty;
        private string company_Name = String.Empty;
        private string catalog_Information = String.Empty;
        private int special_Flag = -1;
        private int reservation_Flag = -1;
        private int shipping_Flag = 0;
        private string merchandise_Information = String.Empty;
        private string item_Description_PC = String.Empty;
        private string item_Description_Mobile = String.Empty;
        private string item_Description_Phone = String.Empty;
        private string sale_Description_PC = String.Empty;
        private string zett_Item_Description = String.Empty;
        private string zett_Sale_Description = String.Empty;
        private string smart_Template = String.Empty;
        private string rakuten_CategoryID = String.Empty;
        private string yahoo_CategoryID = String.Empty;
        private string wowma_CategoryID = String.Empty;
        //private string tennis_CategoryID = String.Empty;
        private string additional_1 = String.Empty;
        private string additional_2 = String.Empty;
        private string additional_3 = String.Empty;
        private int postage = 0;
        private int extra_Shipping = 0;
        private string maker_code = String .Empty ;
        private int delivery_Charges = 0;
        private int warehouse_Specified = 0;
        private string salesunit = String.Empty;
        private string tagInformation = String.Empty;
        private string contentquantityno1 = String.Empty;
        private string contentunit1 = String.Empty;
        private string contentquantityno2 = String.Empty;
        private string contentunit2 = String.Empty;
        private string blackMarket_Password = String.Empty;
        private string doublePrice_Ctrl_No = String.Empty;
        private string rakuten_CategoryName = String.Empty;
        private string yahoo_CategoryName = String.Empty;
        private string wowma_CategoryName = String.Empty;
        //private string tennis_CategoryName = String.Empty;
        private string rakuten_CategoryPath = String.Empty;
        private string yahoo_CategoryPath = String.Empty;
        private string wowma_CategoryPath = String.Empty;
        //private string tennis_CategoryPath = String.Empty;
        private string cate_Name = String.Empty;
        private string catchCopy = String.Empty;
        private string catchCopyMobile = String.Empty;
        private string yahoo_url = String.Empty;
        private string makername = String.Empty;
        private string comment = String.Empty;
        private int sellingprice = 0;
        private int purchaseprice = 0;
        private int sellby = 0;
        private string sellingrank = String.Empty;
        private int deliverydays = 0;
        private int ksmdeliverytype = 0;
        private int ksmdeliverydays = 0;
        private string nationwide = null;
        private int hokkaido = 0;
        private int okinawa = 0;
        private int remoteisland = 0;
        private string undeliveredarea = String.Empty;
        private string dangerousgoodscontents = String.Empty;
        private int deliverymethod = 0;
        private int deliverytype = 0;
        private int deliveryfees = 0;
        private int ksmavaliable = 0;
        private int returnableitem = 0;
        private int noapplicablelaw = 0;
        private int salespermission = 0;
        private int law = 0;
        private int danggoodsclass = 0;
        private int danggoodsname = 0;
        private int riskrating = 0;
        private int danggoodsnature = 0;
        private int firelaw = 0;
        private int shopid = 0;
        private int ready = 0;
        private int dayship = 0;
        private int warehouse_code = 0;
        private int retrun_necessary = 0;
        private int rakutenprice = 0;
        private int yahooprice = 0;
        private int wowmaprice = 0;
        private int jishaprice = 0;
        private int tennisprice = 0;

        private String personInCharge = String.Empty;
        public String PersonInCharge
        {
            get { return personInCharge; }
            set { personInCharge = value; }
        }

        private DateTime? fromDate=null;
        public DateTime? FromDate
        {
            get{return fromDate;}
            set{fromDate=value;}
        }

        private DateTime? toDate = null;
        public DateTime? ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }

        private String remark = String.Empty;
        public String Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private String janCode = String.Empty;
        public String JanCode
        {
            get { return janCode; }
            set { janCode = value; }
        }

        private String productName = String.Empty;
        public String ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private String masterKeyword = String.Empty;
        public String MasterKeyword
        {
            get { return masterKeyword; }
            set { masterKeyword = value; }
        }

        private String removeList = String.Empty;
        public String RemoveList
        {
            get { return removeList; }
            set { removeList = value; }
        }

        private String idList = String.Empty;
        public String IdList
        {
            get { return idList; }
            set { idList = value; }
        }

        private String color_Name = String.Empty;
        public String Color_Name
        {
            get { return color_Name; }
            set { color_Name = value; }
        }

        private String image_Name = String.Empty;
        public String Image_Name
        {
            get { return image_Name; }
            set { image_Name = value; }
        }

        private int export_status = -1;
        public int Export_Status
        {
            get { return export_status; }
            set { export_status = value; }
        }

        #endregion

        private int rakuten_evidence = 0;
        public int Rakuten_evidence
        {
            get { return rakuten_evidence; }
            set { rakuten_evidence = value; }
        }

        private int active = 0;
        public int Active
        {
            get { return active; }
            set { active = value; }
        }

        private string inactivecomment = String.Empty;
        public string InactiveComment
        {
            get { return inactivecomment; }
            set { inactivecomment = value; }
        }

        private int cloudshop_mode = 0;
        public int Cloudshop_mode
        {
            get { return cloudshop_mode; }
            set { cloudshop_mode = value; }
        }

        #region Public Properties
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Ctrl_ID
        {
            get { return ctrl_ID; }
            set { ctrl_ID = value; }
        }
        public string Sale_Code
        {
            get { return sale_Code; }
            set { sale_Code = value; }
        }

        public int Updated_By
        {
            get { return updated_by; }
            set { updated_by = value; }
        }


        public string Product_Code
        {
            get { return product_Code; }
            set { product_Code = value; }
        }
        public string Item_Code
        {
            get { return item_Code; }
            set { item_Code = value; }
        }
        public string Item_AdminCode
        {
            get { return item_AdminCode; }
            set { item_AdminCode = value; }
        }
        public string Item_Name
        {
            get { return item_Name; }
            set { item_Name = value; }
        }


        public string PC_CatchCopy
        {
            get { return catchCopy; }
            set { catchCopy = value; }
        }
        public string PC_CatchCopy_Mobile
        {
            get { return catchCopyMobile; }
            set { catchCopyMobile = value; }
        }
        public int List_Price
        {
            get { return list_Price; }
            set { list_Price = value; }
        }
        public int Sale_Price
        {
            get { return sale_Price; }
            set { sale_Price = value; }
        }
        public int Jisha_Price
        {
            get { return jisha_Price; }
            set { jisha_Price = value; }
        }
        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public DateTime? Release_Date
        {
            get { return release_Date; }
            set { release_Date = value; }
        }
        public DateTime? Post_Available_Date
        {
            get { return post_Available_Date; }
            set { post_Available_Date = value; }
        }
        public string Year
        {
            get { return year; }
            set { year = value; }
        }
        public string Season
        {
            get { return season; }
            set { season = value; }
        }
        public string Brand_Name
        {
            get { return brand_Name; }
            set { brand_Name = value; }
        }
        public string Brand_Code
        {
            get { return brand_Code; }
            set { brand_Code = value; }
        }
        public string Brand_Code_Yahoo
        {
            get { return brand_Code_Yahoo; }
            set { brand_Code_Yahoo = value; }
        }
        public string Competition_Code
        {
            get { return competition_Code; }
            set { competition_Code = value; }
        }
        public string Competition_Name
        {
            get { return competition_Name; }
            set { competition_Name = value; }
        }
        public string Classification_Code
        {
            get { return classification_Code; }
            set { classification_Code = value; }
        }
        public string Class_Name
        {
            get { return class_Name; }
            set { class_Name = value; }
        }
        public string Company_Name
        {
            get { return company_Name; }
            set { company_Name = value; }
        }
        public string Catalog_Information
        {
            get { return catalog_Information; }
            set { catalog_Information = value; }
        }
        public int Special_Flag
        {
            get { return special_Flag; }
            set { special_Flag = value; }
        }
        public int Reservation_Flag
        {
            get { return reservation_Flag; }
            set { reservation_Flag = value; }
        }
        public int Shipping_Flag
        {
            get { return shipping_Flag; }
            set { shipping_Flag = value; }
        }
        public string Merchandise_Information
        {
            get { return merchandise_Information; }
            set { merchandise_Information = value; }
        }
        public string Item_Description_PC
        {
            get { return item_Description_PC; }
            set { item_Description_PC = value; }
        }
        public string Item_Description_Mobile
        {
            get { return item_Description_Mobile; }
            set { item_Description_Mobile = value; }
        }
        public string Item_Description_Phone
        {
            get { return item_Description_Phone; }
            set { item_Description_Phone = value; }
        }
        public string Sale_Description_PC
        {
            get { return sale_Description_PC; }
            set { sale_Description_PC = value; }
        }
        public string Zett_Item_Description
        {
            get { return zett_Item_Description; }
            set { zett_Item_Description = value; }
        }
        public string Zett_Sale_Description
        {
            get { return zett_Sale_Description; }
            set { zett_Sale_Description = value; }
        }
        public string Smart_Template
        {
            get { return smart_Template; }
            set { smart_Template = value; }
        }
        public string Rakuten_CategoryID
        {
            get { return rakuten_CategoryID; }
            set { rakuten_CategoryID = value; }
        }
        public string Yahoo_CategoryID
        {
            get { return yahoo_CategoryID; }
            set { yahoo_CategoryID = value; }
        }
        public string Wowma_CategoryID
        {
            get { return wowma_CategoryID; }
            set { wowma_CategoryID = value; }
        }
        //public string Tennis_CategoryID
        //{
        //    get { return tennis_CategoryID; }
        //    set { tennis_CategoryID = value; }
        //}
        public string Additional_1
        {
            get { return additional_1; }
            set { additional_1 = value; }
        }
        public string Additional_2
        {
            get { return additional_2; }
            set { additional_2 = value; }
        }
        public string Additional_3
        {
            get { return additional_3; }
            set { additional_3 = value; }
        }
        public int Postage
        {
            get { return postage; }
            set { postage = value; }
        }
        public int Delivery_Charges
        {
            get { return delivery_Charges; }
            set { delivery_Charges = value; }
        }
        public int Warehouse_Specified
        {
            get { return warehouse_Specified; }
            set { warehouse_Specified = value; }
        }
        public int Extra_Shipping
        {
            get { return extra_Shipping; }
            set { extra_Shipping = value; }
        }
        public string Maker_Code
        {
            get{return maker_code;}
            set {maker_code = value;}
        }
        public string BlackMarket_Password
        {
            get { return blackMarket_Password; }
            set { blackMarket_Password = value; }
        }
        public string DoublePrice_Ctrl_No
        {
            get { return doublePrice_Ctrl_No; }
            set { doublePrice_Ctrl_No = value; }
        }


        public string Rakuten_CategoryName
        {
            get { return rakuten_CategoryName; }
            set { rakuten_CategoryName = value; }
        }
        public string Yahoo_CategoryName
        {
            get { return yahoo_CategoryName; }
            set { yahoo_CategoryName = value; }
        }
        public string Wowma_CategoryName
        {
            get { return wowma_CategoryName; }
            set { wowma_CategoryName = value; }
        }

        //public string Tennis_CategoryName
        //{
        //    get { return tennis_CategoryName; }
        //    set { tennis_CategoryName = value; }
        //}
        public string Rakuten_CategoryPath
        {
            get { return rakuten_CategoryPath; }
            set { rakuten_CategoryPath = value; }
        }
        public string Yahoo_CategoryPath
        {
            get { return yahoo_CategoryPath; }
            set { yahoo_CategoryPath = value; }
        }
        public string Wowma_CategoryPath
        {
            get { return wowma_CategoryPath; }
            set { wowma_CategoryPath= value; }
        }

        //public string Tennis_CategoryPath
        //{
        //    get { return tennis_CategoryPath; }
        //    set { tennis_CategoryPath = value; }
        //}
        public string Cate_Name
        {
            get { return cate_Name; }
            set { cate_Name = value; }
        }
        #endregion

        private String instructionno;
        public String InstructionNo 
        {
            get { return instructionno; }
            set { instructionno = value; }
        }

        public string Yahoo_url
        {
            get { return yahoo_url; }
            set { yahoo_url = value; }
        }

        private int skucheck = 0;
        public int Skucheck
        {
            get { return skucheck; }
            set { skucheck = value; }
        }
        private int price = 0;
        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        
        public string SalesUnit
        {
            get { return salesunit;}
            set { salesunit= value; }
        }

        public string TagInformation
        {
            get { return tagInformation; }
            set { tagInformation = value; }
        }

        public string ContentQuantityNo1
        {
            get { return contentquantityno1;}
            set { contentquantityno1=value; }
        }

        public string ContentUnit1
        {
            get { return contentunit1;}
            set { contentunit1=value;}
        }

        public string ContentQuantityNo2
        {
            get { return contentquantityno2;}
            set { contentquantityno2=value;}
        }

        public string ContentUnit2
        {
            get { return contentunit2;}
            set { contentunit2 = value; }
        }


        public string Maker_Name
        {
            get { return makername; }
            set { makername = value; }
        }
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        public int Selling_Price
        {
            get { return sellingprice; }
            set { sellingprice = value; }
        }
        public int Purchase_Price
        {
            get { return purchaseprice; }
            set { purchaseprice = value; }
        }
        public int SellBy
        {
            get { return sellby; }
            set { sellby = value; }
        }
        public string Selling_Rank
        {
            get { return sellingrank ; }
            set { sellingrank  = value; }
        }
        public int Delivery_Days
        {
            get { return deliverydays ; }
            set { deliverydays  = value; }
        }
        public int KSMDelivery_Days
        {
            get { return ksmdeliverydays ; }
            set { ksmdeliverydays  = value; }
        }
        public int KSMDelivery_Type
        {
            get { return ksmdeliverytype; }
            set { ksmdeliverytype = value; }
        }
        public string Nation_Wide
        {
            get { return nationwide ; }
            set { nationwide  = value; }
        }
        public int Hokkaido 
        {
            get { return hokkaido ; }
            set { hokkaido  = value; }
        }
        public int Okinawa 
        {
            get { return okinawa ; }
            set { okinawa  = value; }
        }
        public int Remote_Island
        {
            get { return remoteisland ; }
            set { remoteisland  = value; }
        }
        public string Undelivered_Area
        {
            get { return undeliveredarea ; }
            set { undeliveredarea  = value; }
        }
        public string Dangerous_Goods_Contents
        {
            get { return dangerousgoodscontents ; }
            set { dangerousgoodscontents  = value; }
        }
        public int Delivery_Method
        {
            get { return deliverymethod; }
            set { deliverymethod = value; }
        }
        public int Delivery_Type
        {
            get { return deliverytype; }
            set { deliverytype = value; }
        }
        public int Delivery_Fees
        {
            get { return deliveryfees; }
            set { deliveryfees = value; }
        }
        public int KSM_Avaliable
        {
            get { return ksmavaliable; }
            set { ksmavaliable = value; }
        }
        public int Returnable_Item
        {
            get { return returnableitem; }
            set { returnableitem = value; }
        }
        public int NoApplicable_Law
        {
            get { return noapplicablelaw; }
            set { noapplicablelaw = value; }
        }
        public int Sales_Permission
        {
            get { return salespermission; }
            set { salespermission = value; }
        }
        public int Law
        {
            get { return law; }
            set { law = value; }
        }
        public int Dangoods_Class
        {
            get { return danggoodsclass; }
            set { danggoodsclass = value; }
        }
        public int Dangoods_Name
        {
            get { return danggoodsname; }
            set { danggoodsname = value; }
        }
        public int Risk_Rating
        {
            get { return riskrating; }
            set { riskrating = value; }
        }
        public int Dangoods_Nature
        {
            get { return danggoodsnature; }
            set { danggoodsnature = value; }
        }
        public int Fire_Law
        {
            get { return firelaw; }
            set { firelaw = value; }
        }

        private string costrate = String.Empty;
        public string CostRate
        {
            get { return costrate; }
            set { costrate = value; }
        }

        private string discountrate = String.Empty;
        public string DiscountRate
        {
            get { return discountrate; }
            set { discountrate = value; }
        }

        private string profitrate = String.Empty;
        public string ProfitRate
        {
            get { return profitrate; }
            set { profitrate = value; }
        }

        public int ShopID
        {
            get { return shopid; }
            set { shopid = value; }
        }

        public int Ready
        {
            get { return ready; }
            set { ready = value; }
        }
        public int Day_Ship
        {
            get { return dayship ; }
            set { dayship = value; }
        }
        public int Retrun_Necessary
        {
            get { return retrun_necessary; }
            set { retrun_necessary = value; }
        }
        public int Warehouse_Code
        {
            get { return warehouse_code; }
            set { warehouse_code = value; }
        }

        public int RakutenPrice
        {
            get { return rakutenprice; }
            set { rakutenprice = value; }
        }
        public int YahooPrice
        {
            get { return yahooprice; }
            set { yahooprice = value; }
        }
        public int WowmaPrice
        {
            get { return wowmaprice; }
            set { wowmaprice = value; }
        }
        public int JishaPrice
        {
            get { return jishaprice; }
            set { jishaprice = value; }
        }
        public int TennisPrice
        {
            get { return tennisprice; }
            set { tennisprice = value; }
        }
    }
}
