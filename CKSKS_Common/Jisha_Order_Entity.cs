using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Jisha_Order_Entity
    {
        #region Private Variables

        private int id;

        private string order_id;

        private DateTime orderDate;

        private string bill_lastName;

        private string bill_firstName;

        private string bill_lastname_kana;

        private string bill_firstname_kana;

        private string bill_mail_Address;

        private string bill_zipCode;

        private string bill_prefecture;

        private string bill_city;



        private string bill_address1;

        private string bill_address2;

        private string bill_phoneNo;

        private string bill_Emg_Phoneno;

        private string ship_lastname;

        private string ship_firstname;

        private string ship_lastName_kana;

        private string ship_firstName_kana;

        private string ship_zipcode;

        private string ship_prefecture;

        private string ship_city;

        private string ship_address1;

        private string ship_address2;

        private string ship_phoneno;

        private string pay_method;

        private string order_comment;

        private string group_Type;

        private string group_name;

        private string mail_magazine;

        private int usepoint;

        private int sub_total;

        private int ship_charge;

        private int cod_chargeId;

     

        private int pay_charge;

        


        private int code_chargeAmount;

        private int delivery_chargeAmount;

        private Decimal tax; 

        private int total;


      

        private DateTime createdDate;


        #endregion

        #region Public Properties


        public int ID
        {
            get { return id; }
            set { id = value; }
        }
                
        public  string Order_ID
        {
            get { return order_id; }
            set { order_id = value; }

        }

        public DateTime Order_Date
         {
             get { return orderDate; }
             set { orderDate = value; }
         }


        public string Bill_LastName
        {
            get { return bill_lastName; }
            set { bill_lastName = value; }

        }


        public string Bill_FirstName
        {
            get { return bill_firstName;  }
            set { bill_firstName= value; }

        }

        public string Bill_LastName_Kana
        {
            get { return bill_lastname_kana; }
            set { bill_lastname_kana= value; }

        }

        public string Bill_FirstName_Kana
        {
            get { return bill_firstname_kana; }
            set { bill_firstname_kana = value; }

        }

        public string Bill_MailAddress
        {
            get { return bill_mail_Address;  }
            set { bill_mail_Address= value; }

        }


        public string Bill_ZipCode
        {
            get { return bill_zipCode; }
            set { bill_zipCode = value; }

        }


        public string Bill_Prefecture
        {
            get { return bill_prefecture; }
            set { bill_prefecture= value; }

        }

        public string Bill_City
        {
            get { return bill_city; }
            set { bill_city = value; }

        }


        public string Bill_Address1
        {
            get { return bill_address1; }
            set { bill_address1 = value; }

        }


        public string Bill_Address2
        {
            get { return bill_address2; }
            set { bill_address2 = value; }

        }


        public string Bill_PhoneNo
        {
            get { return bill_phoneNo; }
            set { bill_phoneNo = value; }

        }


        public string Bill_Emg_PhoneNo
        {
            get { return bill_Emg_Phoneno; }
            set { bill_Emg_Phoneno = value; }

        }

        public string Ship_LastName
        {
            get { return ship_lastname; }
            set { ship_lastname = value; }

        }


        public string Ship_FirstName
        {
            get { return ship_firstname; }
            set { ship_firstname = value; }

        }

        public string Ship_LastName_Kana
        {
            get { return ship_lastName_kana; }
            set { ship_lastName_kana = value; }

        }

        public string Ship_FirstName_Kana
        {
            get { return ship_firstName_kana; }
            set { ship_firstName_kana = value; }

        }

        public string Ship_ZipCode
        {
            get { return ship_zipcode; }
            set { ship_zipcode = value; }

        }


        public string Ship_Prefecture
        {

            get { return ship_prefecture; }
            set { ship_prefecture= value; }

        }

        public string Ship_City
        {

            get { return ship_city; }
            set { ship_city = value; }

        }

        public string Ship_Address1
        {

            get { return ship_address1; }
            set { ship_address1 = value; }
       }


        public string Ship_Address2
        {

            get { return ship_address2; }
            set { ship_address2 = value; }
        }

        public string Ship_PhoneNo
        {

            get { return ship_phoneno; }
            set { ship_phoneno = value; }
        }

        public string Payment_Method
        {

            get { return pay_method; }
            set {pay_method= value; }
        }

        public string Order_Comment
        {

            get { return order_comment; }
            set { order_comment = value; }
        }

        public string Group_Type
        {

            get { return group_Type; }
            set { group_Type = value; }
        }


        public string Group_Name
        {

            get { return group_name; }
            set { group_name = value; }
        }


        public string MailMagazine
        {

            get { return mail_magazine; }
            set { mail_magazine = value; }
        }

        public int  Use_Point
        {

            get { return usepoint; }
            set { usepoint = value; }
        }

            public int Sub_Total
        {

            get { return sub_total; }
            set { sub_total = value; }


        }


        public int COD_ChargeID
       {
            get { return cod_chargeId; }
            set {cod_chargeId= value; }
             }

            public int COD_Charge_Amount
            {

                get { return code_chargeAmount; }
                set { code_chargeAmount= value; }
            }

          

        public int Delivery_ChargeID
        {
           get { return pay_charge; }
            set { pay_charge = value; }
        }

        public int Delivery_Charge_Amount
        {

            get { return delivery_chargeAmount; }
            set { delivery_chargeAmount = value; }


        }
        public Decimal Tax
        {
            get { return tax; }
            set { tax = value; }
        }
        public int Total
        {
            get { return total;  }
            set { total = value; }
        }


        


        public DateTime Created_Date
        {
            get { return createdDate; }
            set { createdDate=value; }
        }
        #endregion


    }

}










    
