using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Item_Entity
    {
        private int id;
        private string sale_Code = string.Empty;
        private string jan_Code = string.Empty;
        private string item_Code = string.Empty;
        private string color_Code = string.Empty;
        private string color_Name = string.Empty;
        private string size_Code = string.Empty;
        private string size_Name = string.Empty;
        private int original_Quantity = 0;
        private int quantity = 0;
        private int indicated_Price = 0;
        private int sale_Price = 0;
        private string csv_FileName = string.Empty;
        private string item_Description = string.Empty;
        private int isUploaded = 0;
        private DateTime? created_Date = DateTime.Now;
        private DateTime? updated_Date = DateTime.Now;
        private int updated_By = 0;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Sale_Code
        {
            get { return sale_Code; }
            set { sale_Code = value; }
        }
        public string JAN_Code
        {
            get { return jan_Code; }
            set { jan_Code = value; }
        }
        public string Item_Code
        {
            get { return item_Code; }
            set { item_Code = value; }
        }
        public string Color_Code
        {
            get { return color_Code; }
            set { color_Code = value; }
        }
        public string Color_Name
        {
            get { return color_Name; }
            set { color_Name = value; }
        }
        public string Size_Code
        {
            get { return size_Code; }
            set { size_Code = value; }
        }
        public string Size_Name
        {
            get { return size_Name; }
            set { size_Name = value; }
        }
        public int Original_Quantity
        {
            get { return original_Quantity; }
            set { original_Quantity = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public int Indicated_Price
        {
            get { return indicated_Price; }
            set { indicated_Price = value; }
        }
        public int Sale_Price
        {
            get { return sale_Price; }
            set { sale_Price = value; }
        }
        public string CSV_FileName
        {
            get { return csv_FileName; }
            set { csv_FileName = value; }
        }
        public string Item_Description
        {
            get { return item_Description; }
            set { item_Description = value; }
        }
        public int IsUploaded
        {
            get { return isUploaded; }
            set { isUploaded = value; }
        }
        public DateTime? Created_Date
        {
            get { return created_Date; }
            set { created_Date = value; }
        }
        public DateTime? Updated_Date
        {
            get { return updated_Date; }
            set { updated_Date = value; }
        }
        public int Updated_By
        {
            get { return updated_By; }
            set { updated_By = value; }
        }
        
    }
}
