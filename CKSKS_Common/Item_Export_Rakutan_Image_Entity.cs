using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Item_Export_Rakutan_Image_Entity
    {
        private int id;
        private string folder_Name;
        private int file_Count;
        private int active;
        private int shop_id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Folder_Name
        {
            get { return folder_Name; }
            set { folder_Name = value; }
        }

        public int File_Count
        {
            get { return file_Count; }
            set { file_Count = value; }
        }

        public int Active
        {
            get { return active; }
            set { active = value; }
        }

        public int Shop_ID
        {
            get { return shop_id; }
            set { shop_id = value; }
        }
    }
}
