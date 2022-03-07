using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
  public  class Option_Entity
    {

        private int id; private int id2; private int id3;
    
        private string  groupname= string.Empty;

        private string  optionName = string.Empty;
        private string  optionvalue = string.Empty;

        private string  optionName2 = string.Empty;
        private string   optionvalue2 = string.Empty;

        private string  optionName3 = string.Empty;
        private string optionvalue3= string.Empty;


        private int type1;
        private int type2 ;
        private int type3 ;
        //private string optionGroup2 = string.Empty;
        //private string optionGroup3 = string.Empty;


        public int  ID
        {
            get { return id; }
            set { id = value; }
        }
        public int ID2 
        {
            get { return id2; }
            set { id2 = value; }
        }
        public int ID3
        {
            get { return id3; }
            set { id3 = value; }
        }
        public int Type1 { get { return type1; } set { type1 = value; } }
        public int Type2 { get { return type2; } set { type2 = value; } }
        public int  Type3 { get { return type3; } set { type3 = value; } }
     
        public string Option_GroupName
        {
            get { return groupname; }
            set { groupname = value; }
        }
        public string  Option_Name
        {
            get { return optionName; }
            set { optionName = value; }
        }

        public string Option_Value
        {
            get { return optionvalue; }
            set { optionvalue = value; }
        }


        public string Option_Name2
        {
            get { return optionName2; }
            set { optionName2 = value; }
        }
        public string Option_Value2
        {
            get { return optionvalue2; }
            set { optionvalue2 = value; }
        }

        public string Option_Name3
        {
            get { return optionName3; }
            set { optionName3 = value; }
        }
        public string Option_Value3
        {
            get { return optionvalue3; }
            set { optionvalue3 = value; }
        }


        //public string Option_GroupName2
        //{

        //    get { return optionGroup2; }
        //    set { optionGroup2 = value; }

        //}

        //public string Option_GroupName3
        //{
        //    get { return optionGroup3; }
        //    set { optionGroup3 = value; }

        //}



    }
}
