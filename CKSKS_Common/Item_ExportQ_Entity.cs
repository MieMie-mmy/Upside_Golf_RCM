using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Item_ExportQ_Entity
    {
        private int id;
        private string file_Name;
        private int file_Type;
        private int shopID;
        private int isExport;
        private int export_Type;
        private DateTime created_Date;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string File_Name
        {
            get { return file_Name; }
            set { file_Name = value; }
        }
        public int File_Type
        {
            get { return file_Type; }
            set { file_Type = value; }
        }
        public int ShopID
        {
            get { return shopID; }
            set { shopID = value; }
        }
        public int IsExport
        {
            get { return isExport; }
            set { isExport = value; }
        }
        public int Export_Type
        {
            get { return export_Type; }
            set { export_Type = value; }
        }
        public DateTime Created_Date
        {
            get { return created_Date; }
            set { created_Date = value; }
        }
    }
}
