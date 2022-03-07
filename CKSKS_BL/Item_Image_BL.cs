using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using System.Data;
using System.Transactions;

namespace ORS_RCM_BL
{
    public class Item_Image_BL
    {
        Item_Image_DL itemImageDL;

        public Item_Image_BL()
        {
            itemImageDL = new Item_Image_DL();
        }

        public bool Insert(int Item_ID, DataTable ImageList  )
        {
            if (itemImageDL.Delete(Item_ID) )
            {
                if (!ContainColumn("Item_ID", ImageList))
                {
                    ImageList.Columns.Add(new DataColumn("Item_ID", typeof(Int32)));
                }
                foreach (DataRow row in ImageList.Rows)
                {
                    row["Item_ID"] = Item_ID;
                }
                if (itemImageDL.Insert(ImageList))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public DataTable SelectByItemID(int Item_ID)
        {
            return itemImageDL.SelectByItemID(Item_ID);
        }

        public DataTable SelectItemPhotoByItemID(int Item_ID,int type)
        {
            return itemImageDL.SelectItemPhotoByItemID(Item_ID,type);
        }

        public bool DeleteImage(int imageID)
        {
            return itemImageDL.DeleteImage(imageID);
        }

        public bool ContainColumn(string columnName, DataTable table)
        {
        DataColumnCollection columns = table.Columns;

        if (columns.Contains(columnName))
        {
            return true;
        }
        else
            return false;
        }

        public DataTable GetImageList(int shop_id,string StringItemID)
        {
            return itemImageDL.GetImageList(shop_id,StringItemID);
        }

        public DataTable SelectImageList(int ItemID)
        {
            return itemImageDL.SelectImageList(ItemID);
        }

        public DataTable SelectAllWithItemID()
        {
            itemImageDL = new Item_Image_DL();
            return itemImageDL.SelectAllWithItemID();
        }

        public void DeletebyItemID(String ItemID,int ImageType)
        {
            itemImageDL = new Item_Image_DL();
            itemImageDL.DeletebyItemID(ItemID, ImageType);
        }

        public Boolean UpdateImage(int ItemID, String img1, String img2, String img3, String img4, String img5, String img6)
        {
            itemImageDL = new Item_Image_DL();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    itemImageDL.DeleteLibraryImage(ItemID);

                    itemImageDL.Update(ItemID,img1);
                    itemImageDL.Update(ItemID,img2);
                    itemImageDL.Update(ItemID,img3);
                    itemImageDL.Update(ItemID,img4);
                    itemImageDL.Update(ItemID,img5);
                    itemImageDL.Update(ItemID, img6);

                    scope.Complete();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable SelectImageName(string Item_Code , int ImageType)
        {
            return itemImageDL.SelectImageName(Item_Code, ImageType);
        }

    }
}
