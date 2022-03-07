using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Jisha_Credit_Card_Entity
    {
        private int id;
        private string orderID;
        private string acs;
        private string forward;
        private int method;
        private string payTimes;
        private string approve;
        private string tranID;
        private DateTime tranDate;
        private string checkString;
        private DateTime createdDate;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        public string ACS
        {
            get { return acs; }
            set { acs = value; }
        }

        public string Forward
        {
            get { return forward; }
            set { forward = value; }
        }

        public int Method
        {
            get { return method; }
            set { method = value; }
        }

        public string PayTimes
        {
            get { return payTimes; }
            set { payTimes = value; }
        }

        public string Approve
        {
            get { return approve; }
            set { approve = value; }
        }

        public string TranID
        {
            get { return tranID; }
            set { tranID = value; }
        }

        public DateTime TranDate
        {
            get { return tranDate; }
            set { tranDate = value; }
        }

        public string CheckString
        {
            get { return checkString; }
            set { checkString = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
    }
}
