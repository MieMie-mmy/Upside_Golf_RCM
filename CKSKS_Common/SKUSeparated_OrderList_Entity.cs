using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class SKUSeparated_OrderList_Entity
    {
        #region Private Variables

        private int id;

        private String item_code;

        private String item_name;


        private String size;

        private String color;


        private String brandname;

        private String competname;

        private String year;

        private String season;

        private String shop;

        private DateTime? period_From;

        private DateTime? period_To;

        private String image_name;

        private int updated_by = 0;

        #endregion



        #region Public Properties


        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        public String Item_Code
        {
            get { return item_code; }
            set { item_code = value; }
        }


        public String Item_Name
        {
            get { return item_name; }
            set { item_name = value; }
        }


        public String Size
        {
            get { return size; }
            set { size = value; }
        }


        public String Color
        {
            get { return color;}
            set { color = value; }
        }


        public int Updated_By
        {
            get { return updated_by; }
            set { updated_by = value; }
        }




        public String Brand_Name
        {
            get { return brandname; }
            set { brandname = value; }
        }


        public String Competition_Name
        {
            get { return competname; }

            set { competname = value; }
        }

        public String Year
        {
            get { return year; }
            set { year = value; }
        }


        public String Season
        {
            get { return season; }
            set { season = value; }
        }


        public String Shop
        {
            get { return shop; }
            set { shop = value; }

        }


        public DateTime? fromDate
        {
            get { return period_From; }
            set { period_From = value; }
        }


        public DateTime? toDate
        {
            get { return period_To; }
            set { period_To = value; }
        }

        public String Image_Name
        {
            get { return image_name; }
            set { image_name = value; }
        }

        #endregion

    
    }
}
