using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_Common
{
    public class Mall_Category_Entity
    {
        private int id;
        public int ID 
        {
            get { return id; }
            set { id = value; }
        }

        private int mallid;
        public int MallID 
        {
            get { return mallid; }
            set { mallid = value; }
        }

        private string cID;
        public string CategoryID 
        {
            get { return cID; }
            set { cID = value; }
        }


        private string cName;
        public string CategoryName 
        {
            get { return cName; }
            set { cName = value; }
        }
    }
}
