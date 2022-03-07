using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_Common
{
    public  class Jisha_Delivery_Charge_Entity
    {
        private int id;
        public int ID 
        {
            get { return id; }
            set { id = value; }
        }

        private string chargename;
        public string Chargename 
        {
            get { return chargename; }
            set { chargename = value; }
        }


        private int chargetype;
        public int Chargetype 
        {
            get { return chargetype; }
            set { chargetype = value; }
        }

        private string chargecondition;
        public string Chargecondition 
        {
            get { return chargecondition; }
            set { chargecondition = value; }
        }

        private int delivertyfee;
        public int Delivertyfee 
        {
            get { return delivertyfee; }
            set { delivertyfee = value; }
        
        }

        private int priority;
        public int Priority 
        {
            get { return priority; }
            set { priority = value; }
        }

        private int status;
        public int Status 
        {
            get { return status; }
            set { status = value; }
        }



    }
}
