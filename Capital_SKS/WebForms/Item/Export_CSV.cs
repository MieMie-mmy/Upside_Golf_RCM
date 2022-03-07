/* 
Created By              :Aye Aye Mon
Created Date          :15/07/2014
Updated By             :
Updated Date         :

 Tables using: 
    -
    -
    -
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ORS_RCM_BL;
using System.Data;
using System.IO;
using System.Text;
using ORS_RCM_Common;
using Ionic.Zip;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ORS_RCM.WebForms.Item
{
    public class Export_CSV:System.Web.UI.Page
    {
        //Export_CSV2 export = new Export_CSV2();
        Export_CSV4 export = new Export_CSV4();

        Item_BL ibl = new Item_BL();
        Item_Master_BL imbl = new Item_Master_BL();
        Item_Image_BL iibl = new Item_Image_BL();

        DataTable dtImage = new DataTable();
        DataTable dtShopList = new DataTable(); 
        DataTable dtItemMaster = new DataTable();
        DataTable dtItemSelect = new DataTable();
        DataTable dtItemCat = new DataTable();

        /// <summary>
        /// to export item,category,inventory csvs and image file to related shop.
        /// </summary>
        /// <param name="ItemList">user choosed items  from item view form to export csv</param>
        public Export_CSV(string ItemList , int User_ID)
        {
            dtShopList = imbl.GetShopList(ItemList);  // Get related ShopID List  of Items.

            if (dtShopList != null && dtShopList.Rows.Count > 0)
            {
                for (int i = 0; i < dtShopList.Rows.Count; i++)
                {
                    int shop_id = int.Parse(dtShopList.Rows[i]["Shop_ID"].ToString());

                    switch (dtShopList.Rows[i]["Mall_ID"].ToString())  //Check Mall
                    {
                        case "1":
                            PrepareRakutenData(shop_id, ItemList, User_ID);
                            export.RakutenFilterSKU(dtItemSelect, dtItemMaster, dtItemCat, shop_id);
                            break;

                        case "2":
                            PrepareYahooData(shop_id, ItemList, User_ID);
                            export.YahooFilterSKU(dtItemSelect, dtItemMaster, dtItemCat, shop_id);
                            break;

                        case "3":
                            PreparePonpareData(shop_id, ItemList, User_ID);
                            export.PonpareFilterSKU(dtItemSelect, dtItemMaster, dtItemCat, shop_id);
                            break;

                        case "4":
                            PrepareAmazonData(shop_id, ItemList, User_ID);
                            export.AmazonFilterSKU(dtItemSelect, dtItemMaster, dtItemCat, shop_id);
                            break;

                        case "5":
                            PrepareJishaData(shop_id, ItemList, User_ID);
                            export.JiShaFilterSKU(dtItemSelect, dtItemMaster, dtItemCat, shop_id);
                            break;
                    }
                }
            }
          
            //After exporting item,

            //ibl.DeleteItems(ItemList); //Delete Item Ctrl_ID=d
            //imbl.ChangeStatus(ItemList, User_ID); //Change Export_Status in Item_Master table
            }

        public void PrepareRakutenData(int shop_id, string ItemList ,int User_ID)
        {
            // Get Item Info in Selected Item List by each shop ID
            dtItemMaster = imbl.SelectByItemDataForRakuten(shop_id, ItemList, "item");  
            dtItemSelect = imbl.SelectByItemDataForRakuten(shop_id, ItemList, "itemselect");
            dtItemCat = imbl.SelectByItemDataForRakuten(shop_id, ItemList, "itemcat");
            dtImage = iibl.GetImageList(shop_id, ItemList);  // Get Image Name by Selected Item

            if (dtImage.Rows.Count > 0)
            {
                CreateRakutenUpoladImage(dtImage, shop_id);
            }

            //check with Import_ShopItem table to change Control Status
            if (dtItemMaster != null && dtItemMaster.Rows.Count > 0)
            {
                for (int j = 0; j < dtItemMaster.Rows.Count; j++)
                {
                    int result = imbl.CheckImport_ShopItem(shop_id, dtItemMaster.Rows[j]["商品番号"].ToString());
                    if (result > 0) //exist
                    {
                        dtItemMaster.Rows[j]["コントロールカラム"] = "u";
                        dtItemMaster.Rows[j]["IsSKU"] ="1";
                        imbl.ChangeStatusForm(Convert.ToInt32(dtItemMaster.Rows[j]["ID"]), 1, User_ID); //Change Export_Status in Item_Master table
                        ChangeFlag(Convert.ToInt32(dtItemMaster.Rows[j]["IsSKU"]), dtItemMaster.Rows[j]["商品番号"].ToString());
                    }
                    else
                    {
                        dtItemMaster.Rows[j]["コントロールカラム"] = "n";
                        dtItemMaster.Rows[j]["IsSKU"] ="0";
                        imbl.ChangeStatusForm(Convert.ToInt32(dtItemMaster.Rows[j]["ID"]), 4, User_ID); 
                        ChangeFlag(Convert.ToInt32(dtItemMaster.Rows[j]["IsSKU"]), dtItemMaster.Rows[j]["商品番号"].ToString());
                    }
                }
            }

            Export_CSV3 exportCSV3 = new Export_CSV3();
            DataTable dtModified = exportCSV3.ModifyTable(dtItemMaster, shop_id);
            if (dtModified != null)
            {
                dtItemMaster = dtModified;
            }
        }

        public void PrepareYahooData(int shop_id, string ItemList, int User_ID)
        {
            dtItemMaster = imbl.SelectByItemDataForYahoo(ItemList, "item",shop_id);
            dtItemSelect = imbl.SelectByItemDataForYahoo(ItemList, "quantity", shop_id);
            dtImage = iibl.GetImageList(shop_id, ItemList); 

            //check with Import_ShopItem table to change Control Status
            if (dtItemMaster != null && dtItemMaster.Rows.Count > 0)
            {
                for (int j = 0; j < dtItemMaster.Rows.Count; j++)
                {
                    int result = imbl.CheckImport_ShopItem(shop_id, dtItemMaster.Rows[j]["code"].ToString());
                    if (result > 0) //exist
                    {
                        imbl.ChangeStatusForm(Convert.ToInt32(dtItemMaster.Rows[j]["ID"]), 1, User_ID); //Change Export_Status in Item_Master table
                        dtItemMaster.Rows[j]["コントロールカラム"] = "u";
                    }
                    else
                    {
                        imbl.ChangeStatusForm(Convert.ToInt32(dtItemMaster.Rows[j]["ID"]), 4, User_ID); //Change Export_Status in Item_Master table
                        dtItemMaster.Rows[j]["コントロールカラム"] = "n";
                    }
                    //Filter for Image
                    CreateUploadImage(dtImage,dtItemMaster.Rows[j]["code"].ToString());
                }
                ImageZip(shop_id);
            }

            //Change Template
            Export_CSV3 exportCSV3 = new Export_CSV3();
            DataTable dtModified = exportCSV3.ModifyTable(dtItemMaster, shop_id);
            if (dtModified != null)
            {
                dtItemMaster = dtModified;
            }

        }

        public void PreparePonpareData(int shop_id, string ItemList, int User_ID)
        {
            // Get Item Info in Selected Item List by each shop ID
            dtItemMaster = imbl.SelectByItemDataForPonpare(shop_id, ItemList, "item");
            dtItemSelect = imbl.SelectByItemDataForPonpare(shop_id, ItemList, "option");
            dtItemCat = imbl.SelectByItemDataForPonpare(shop_id, ItemList, "category");
            dtImage = iibl.GetImageList(shop_id, ItemList);  // Get Image Name by Selected Item

            if (dtImage.Rows.Count > 0)
            {
                CreateRakutenUpoladImage(dtImage, shop_id);
            }

            //check with Import_ShopItem table to change Control Status
            if (dtItemMaster != null && dtItemMaster.Rows.Count > 0)
            {
                for (int j = 0; j < dtItemMaster.Rows.Count; j++)
                {
                    int result = imbl.CheckImport_ShopItem(shop_id, dtItemMaster.Rows[j]["商品ID"].ToString());
                    if (result > 0) //exist
                    {
                        dtItemMaster.Rows[j]["コントロールカラム"] = "u";
                        dtItemMaster.Rows[j]["IsSKU"] = "1";
                        imbl.ChangeStatusForm(Convert.ToInt32(dtItemMaster.Rows[j]["ID"]), 1, User_ID); //Change Export_Status in Item_Master table
                        ChangeFlagForPonpare(Convert.ToInt32(dtItemMaster.Rows[j]["IsSKU"]), dtItemMaster.Rows[j]["商品ID"].ToString());
                    }
                    else
                    {
                        dtItemMaster.Rows[j]["コントロールカラム"] = "n";
                        dtItemMaster.Rows[j]["IsSKU"] = "0";
                        imbl.ChangeStatusForm(Convert.ToInt32(dtItemMaster.Rows[j]["ID"]), 4, User_ID); //Change Export_Status in Item_Master table
                        ChangeFlagForPonpare(Convert.ToInt32(dtItemMaster.Rows[j]["IsSKU"]), dtItemMaster.Rows[j]["商品ID"].ToString());
                    }
                }
            }

            Export_CSV3 exportCSV3 = new Export_CSV3();
            DataTable dtModified = exportCSV3.ModifyTable(dtItemMaster, shop_id);
            if (dtModified != null)
            {
                dtItemMaster = dtModified;
            }
        }

        public void PrepareAmazonData(int shop_id, string ItemList, int User_ID)
        {
            dtItemMaster = imbl.SelectByItemDataForAmazon(ItemList);
            //check with Import_ShopItem table to change Control Status
            if (dtItemMaster != null && dtItemMaster.Rows.Count > 0)
            {
                for (int j = 0; j < dtItemMaster.Rows.Count; j++)
                {
                    int result = imbl.CheckImport_ShopItem(shop_id, dtItemMaster.Rows[j]["sku"].ToString());
                    if (result > 0) //exist
                    {
                        imbl.ChangeStatusForm(Convert.ToInt32(dtItemMaster.Rows[j]["ID"]), 1, User_ID); //Change Export_Status in Item_Master table
                        dtItemMaster.Rows[j]["コントロールカラム"] = "u";
                    }
                    else
                    {
                        imbl.ChangeStatusForm(Convert.ToInt32(dtItemMaster.Rows[j]["ID"]), 4, User_ID); //Change Export_Status in Item_Master table
                        dtItemMaster.Rows[j]["コントロールカラム"] = "n";
                    }
                }
            }
           // imbl.ChangeStatus(Convert.ToInt32(dtItemMaster.Rows[j]["ID"]), 1, User_ID); //Change Export_Status in Item_Master table

        }

        public void PrepareJishaData(int shop_id, string ItemList, int User_ID)
        {
            // Get Item Info in Selected Item List by each shop ID
            dtItemMaster = imbl.SelectByItemDataForJisha(shop_id, ItemList, "item");
            dtItemSelect = imbl.SelectByItemDataForJisha(shop_id, ItemList, "itemselect");
            dtItemCat = imbl.SelectByItemDataForJisha(shop_id, ItemList, "itemcat");
            dtImage = iibl.GetImageList(shop_id, ItemList);  // Get Image Name by Selected Item

            if (dtImage.Rows.Count > 0)
            {
                CreateRakutenUpoladImage(dtImage, shop_id);
            }

            //check with Import_ShopItem table to change Control Status
            if (dtItemMaster != null && dtItemMaster.Rows.Count > 0)
            {
                for (int j = 0; j < dtItemMaster.Rows.Count; j++)
                {
                    int result = imbl.CheckImport_ShopItem(shop_id, dtItemMaster.Rows[j]["商品番号"].ToString());
                    if (result > 0) //exist
                    {
                        dtItemMaster.Rows[j]["コントロールカラム"] = "u";
                        dtItemMaster.Rows[j]["IsSKU"] = "1";
                        imbl.ChangeStatusForm(Convert.ToInt32(dtItemMaster.Rows[j]["ID"]), 1, User_ID); //Change Export_Status in Item_Master table
                        ChangeFlag(Convert.ToInt32(dtItemMaster.Rows[j]["IsSKU"]), dtItemMaster.Rows[j]["商品番号"].ToString());
                    }
                    else
                    {
                        dtItemMaster.Rows[j]["コントロールカラム"] = "n";
                        dtItemMaster.Rows[j]["IsSKU"] = "0";
                        imbl.ChangeStatusForm(Convert.ToInt32(dtItemMaster.Rows[j]["ID"]), 4, User_ID);
                        ChangeFlag(Convert.ToInt32(dtItemMaster.Rows[j]["IsSKU"]), dtItemMaster.Rows[j]["商品番号"].ToString());
                    }
                }
            }

            Export_CSV3 exportCSV3 = new Export_CSV3();
            DataTable dtModified = exportCSV3.ModifyTable(dtItemMaster, shop_id);
            if (dtModified != null)
            {
                dtItemMaster = dtModified;
            }
        }

        public void ChangeFlag(int SKU_Flag, string Item_Code)
        {
            if (dtItemSelect != null && dtItemSelect.Rows.Count > 0)
            {
                foreach (DataRow dr in dtItemSelect.Rows)
                {
                    if (dr["商品管理番号（商品URL）"].ToString() == Item_Code)
                    {
                        dr["IsSKU"] = SKU_Flag;
                    }
                }
            }

            if (dtItemCat != null && dtItemCat.Rows.Count > 0)
            {
                foreach (DataRow dr in dtItemCat.Rows)
                {
                    if (dr["商品管理番号（商品URL）"].ToString() == Item_Code)
                    {
                        dr["IsSKU"] = SKU_Flag;
                    }
                }
            }

        }

        public void ChangeFlagForPonpare(int SKU_Flag, string Item_Code)
        {
            if (dtItemSelect != null && dtItemSelect.Rows.Count > 0)
            {
                foreach (DataRow dr in dtItemSelect.Rows)
                {
                    if (dr["商品管理ID（商品URL）"].ToString() == Item_Code)
                    {
                        dr["IsSKU"] = SKU_Flag;
                    }
                }
            }

            if (dtItemCat != null && dtItemCat.Rows.Count > 0)
            {
                foreach (DataRow dr in dtItemCat.Rows)
                {
                    if (dr["商品管理ID（商品URL）"].ToString() == Item_Code)
                    {
                        dr["IsSKU"] = SKU_Flag;
                    }
                }
            }

        }

        public void CreateUploadImage(DataTable dt , string Item_Code)
        { 
            //Create new folder for upload image
            String folderpath = "~/Export_CSV/Image/";
            if (!Directory.Exists(Server.MapPath(folderpath)))
                Directory.CreateDirectory(Server.MapPath(folderpath));

            String folder2path = "~/Export_CSV/LibImage/";
            if (!Directory.Exists(Server.MapPath(folder2path)))
                Directory.CreateDirectory(Server.MapPath(folder2path));

            //Upload Image
            DataRow[] dr ;

             #region Image
                dr = dt.Select("Item_Code='" + Item_Code + "' AND Image_Type=0");
                if (dr.Count() > 0)
                {
                    DataTable dtImage = dt.Select("Item_Code='" + Item_Code + "' AND Image_Type=0").CopyToDataTable();
                    for (int i = 0; i < dtImage.Rows.Count; i++)
                    {
                      
                        string str = "~/Item_Image/" + dtImage.Rows[i]["Image_Name"].ToString();

                        //Save image into folder
                        if (Directory.Exists(Server.MapPath(folderpath)))
                        {
                            if (File.Exists(Server.MapPath(str)))
                            {
                                if (!File.Exists(Server.MapPath("~/Export_CSV/Image/") + dtImage.Rows[i]["Image_Name"].ToString()))
                                {
                                    if (i == 0)
                                        File.Copy(Server.MapPath(str), Server.MapPath("~/Export_CSV/Image/") + dtImage.Rows[i]["Item_Code"].ToString() + ".jpg");
                                    else
                                        File.Copy(Server.MapPath(str), Server.MapPath("~/Export_CSV/Image/") + dtImage.Rows[i]["Item_Code"].ToString() + "_" + i + ".jpg");
                                }
                            }
                        }
                    }
                }
                #endregion

             #region Library Image
                dr = dt.Select("Item_Code='" + Item_Code + "' AND Image_Type=1");
                if (dr.Count() > 0)
                {
                    DataTable dtImage = dt.Select("Item_Code='" + Item_Code + "' AND Image_Type=1").CopyToDataTable();
                    for (int i = 0; i < dtImage.Rows.Count; i++)
                    {
                        string str = "~/Item_Image/"+ dtImage.Rows[i]["Image_Name"].ToString();

                        //Save image into folder
                        if (Directory.Exists(Server.MapPath(folder2path)))
                        {
                            if (File.Exists(Server.MapPath(str)))
                            {
                                if (!File.Exists(Server.MapPath("~/Export_CSV/LibImage/") + dtImage.Rows[i]["Image_Name"].ToString()))
                                    File.Copy(Server.MapPath(str), Server.MapPath("~/Export_CSV/LibImage/") + dtImage.Rows[i]["Image_Name"].ToString());
                            }
                        }
                    }
                }
            #endregion

            }

        public void ImageZip(int shop_id)
        {
            //Image Zip
                string date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                String pathName = "~/Export_CSV/Image/";
                string[] fileNames = Directory.GetFiles(Server.MapPath(pathName));

                if (fileNames != null && fileNames.Length > 0)
                {
                    using (ZipFile zipfile = new ZipFile())
                    {
                        zipfile.AddFiles(fileNames, "");
                        zipfile.Save(Server.MapPath("~/Export_CSV/img$" +shop_id +"_" + date + ".zip"));
                        SaveItem_ExportQ("img$" + shop_id + "_" + date + ".zip", 3, shop_id, 0, 1);
                    }
                }

                //Library Image Zip
                pathName = "~/Export_CSV/LibImage/";
                fileNames = Directory.GetFiles(Server.MapPath(pathName));
                if (fileNames != null && fileNames.Length > 0)
                {
                    using (ZipFile zipfile = new ZipFile())
                    {
                        zipfile.AddFiles(fileNames, "");
                        zipfile.Save(Server.MapPath("~/Export_CSV/lib_img$" + shop_id + "_" + date + ".zip"));
                        SaveItem_ExportQ("lib_img$" + shop_id + "_" + date + ".zip", 3, shop_id, 0, 1);
                    }
                }

            DeleteFilesFromDirectory(); 
        }

        public void CreateRakutenUpoladImage(DataTable dt, int shop_id)
        {
            if (dt.Rows.Count  > 0)
            {
                string path = "";
                string folderName = "";
                string image_name = "";
               
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Image_Name"].ToString() != "")
                    {
                        image_name = dr["Image_Name"].ToString();
                        folderName = dr["Folder_Name"].ToString();
                        path = "~/Export_CSV/item_img/" + folderName + "/";
                        
                        if (!Directory.Exists(Server.MapPath(path)))
                            Directory.CreateDirectory(Server.MapPath(path));

                        //string localPath = "/Item_Image/";
                        //string imgPath = Server.MapPath(localPath) + dr["Image_Name"].ToString();

                        //Save image into folder
                        if (Directory.Exists(Server.MapPath(path)))
                        {
                            if (File.Exists(Server.MapPath("~/Item_Image/") + image_name))
                            {
                                if (!File.Exists(Server.MapPath(path) + image_name))
                                    File.Copy(Server.MapPath("~/Item_Image/") + image_name, Server.MapPath(path) + image_name);
                            }
                        }
                    }
                }

                if (Directory.Exists(Server.MapPath("~/Export_CSV/item_img/")))
                {
                    using (ZipFile zipfile = new ZipFile())
                    {
                        zipfile.AddDirectory(Server.MapPath("~/Export_CSV/item_img/"), "item_img");
                        zipfile.Save(Server.MapPath("~/Export_CSV/" + folderName + "$" + shop_id + ".zip"));
                        SaveItem_ExportQ(folderName + "$" +shop_id + ".zip", 3, shop_id, 0, 1);
                    }

                    //var dir = new DirectoryInfo(Server.MapPath("~/Export_CSV/SKU/"));
                    //dir.Delete(true);

                    DeleteDirectory(Server.MapPath("~/Export_CSV/item_img/"), true);
                }
            }
        }

        public static void DeleteDirectory(string path, bool recursive)
        {
            // Delete all files and sub-folders?
            if (recursive)
            {
                // Yep... Let's do this
                var subfolders = Directory.GetDirectories(path);
                foreach (var s in subfolders)
                {
                    DeleteDirectory(s, recursive);
                }
            }

            // Get all files of the folder
            var files = Directory.GetFiles(path);
            foreach (var f in files)
            {
                // Get the attributes of the file
                var attr = File.GetAttributes(f);

                // Is this file marked as 'read-only'?
                if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // Yes... Remove the 'read-only' attribute, then
                    File.SetAttributes(f, attr ^ FileAttributes.ReadOnly);
                }

                // Delete the file
                File.Delete(f);
            }

            // When we get here, all the files of the folder were
            // already deleted, so we just delete the empty folder
            Directory.Delete(path);
        }

        public void SaveItem_ExportQ(string FileName, int FileType, int ShopID, int IsExport, int Export_Type)
        {
            Item_ExportQ_Entity ie = new Item_ExportQ_Entity();
            Item_ExportQ_BL ieBL = new Item_ExportQ_BL();

            ie.File_Name = FileName;
            ie.File_Type = FileType;
            ie.ShopID = ShopID;
            ie.IsExport = IsExport;
            ie.Export_Type = Export_Type;

            ieBL.Save(ie);
        }

        public void DeleteFilesFromDirectory()
        {
            String path = "~/Export_CSV/Image/";
            //String path = @"C:/MyData/Projects/ORS_RCM/ORS_RCM/Export_CSV/";

            string[] files = Directory.GetFiles(Server.MapPath(path));
            foreach (string pathFile in files)
            {
                var file = new FileInfo(pathFile);
                file.Attributes = FileAttributes.Normal;
                File.Delete(pathFile);
            }
            if (Directory.Exists(Server.MapPath(path)))
            {
                Directory.Delete(Server.MapPath(path));
            }

            path = "~/Export_CSV/LibImage/";
            files = Directory.GetFiles(Server.MapPath(path));
            foreach (string pathFile in files)
            {
                var file = new FileInfo(pathFile);
                file.Attributes = FileAttributes.Normal;
                File.Delete(pathFile);
            }
            if (Directory.Exists(Server.MapPath(path)))
            {
                Directory.Delete(Server.MapPath(path));
            }

        }

        #region Cancel
        //if (dtItemSelect != null && dtItemSelect.Rows.Count > 0)
        //{
        //        for (int j = 0; j < dtItemSelect.Rows.Count; j++)
        //        {
        //        //    if (dtItemMaster.Rows[j]["IsSKU"].ToString() == "0") // No Change
        //        //    {
        //        //        
        //                int result = imbl.CheckImport_ShopItemInventory(shop_id, dtItemSelect.Rows[j]["Item_AdminCode"].ToString());
        //                if (result > 0)  // Exist in Import_ShopItem_Inventory
        //                {
        //                    dtItemSelect.Rows[j]["IsSKU_Update"] = "1";
        //                    //dtItemSelect.Rows[j]["コントロールカラム"] = "u";
        //                    //dtItemSelect.Rows[j]["項目選択肢用コントロールカラム"] = "u";
        //                }
        //                else
        //                {
        //                    dtItemSelect.Rows[j]["IsSKU_Update"] = "0";
        //                    //dtItemSelect.Rows[j]["コントロールカラム"] = "n";
        //                    //dtItemSelect.Rows[j]["項目選択肢用コントロールカラム"] = "n";
        //                }
        //            }
        //            //else if (dtItemMaster.Rows[j]["IsSKU"].ToString() == "1")
        //            //{
        //            //    dtItemSelect.Rows[j]["IsSKU_Update"] = "1";
        //            //    //dtItemSelect.Rows[j+1]["コントロールカラム"] = dtItemSelect.Rows[j]["コントロールカラム"];
        //            //}
        //            //else if (dtItemSelect.Rows[j]["IsSKU"].ToString() == "2")
        //            //{
        //            //    dtItemSelect.Rows[j]["IsSKU_Update"] = "1";
        //            //    //dtItemSelect.Rows[j+1]["コントロールカラム"] = dtItemSelect.Rows[j]["コントロールカラム"];
        //            //}
        //        }

        //if (dtItemCat != null && dtItemCat.Rows.Count > 0)
        //{
        //        for (int j = 0; j < dtItemCat.Rows.Count; j++)
        //        {
        //            if (dtItemCat.Rows[j]["IsSKU"].ToString() == "0") // No Change
        //            {
        //                int result = imbl.CheckImport_ShopItemCategory(shop_id, dtItemCat.Rows[j]["Item_AdminCode"].ToString());
        //                if (result > 0)  // Exist in Import_ShopItem_Category
        //                {
        //                    dtItemCat.Rows[j]["コントロールカラム"] = "u";
        //                }
        //                else
        //                {
        //                    dtItemCat.Rows[j]["コントロールカラム"] = "n";
        //                }

        //            }
        //        }
        //    }
        #endregion
    }
}