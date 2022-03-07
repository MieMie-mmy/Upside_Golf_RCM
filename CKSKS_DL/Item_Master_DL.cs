/* 
Created By              : Aye Aye Mon
Created Date          : 02/07/2014
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
using System.Text;
using ORS_RCM_Common;
using System.Data.SqlClient;
using System.Data;

namespace ORS_RCM_DL
{
    public class Item_Master_DL
    {
        public Item_Master_DL()
        { 
        
        }

        public DataTable GetItemSaleDescription(String id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                String query = "Select Sale_Description_PC,Item_Description_PC From Item_Master Where ID=" + id;
                SqlCommand cmd = new SqlCommand(query, connection);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BindSaleUnit()
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                String query = "Select Sales_unit from Sales_Unit where Sales_unit is not null";
                SqlCommand cmd = new SqlCommand(query, connection);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BindORSTag()
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                String query = "Select Name from Tag_Information where Name is not null";
                SqlCommand cmd = new SqlCommand(query, connection);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BindContentUnit1()
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                String query = "Select distinct Contents_unit_1 from Sales_Unit where Contents_unit_1 is not null";
                SqlCommand cmd = new SqlCommand(query, connection);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BindContentUnit2()
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                String query = "Select Contents_unit_2 from Sales_Unit where Contents_unit_2 is not null";
                SqlCommand cmd = new SqlCommand(query, connection);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveEdit(Item_Master_Entity ime, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Master_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ID", ime.ID);
                cmd.Parameters.AddWithValue("@Ctrl_ID", ime.Ctrl_ID);
                cmd.Parameters.AddWithValue("@Product_Code", ime.Product_Code);
                cmd.Parameters.AddWithValue("@Item_Code", ime.Item_Code);
                cmd.Parameters.AddWithValue("@Item_Name", ime.Item_Name);
                cmd.Parameters.AddWithValue("@List_Price", ime.List_Price);
                cmd.Parameters.AddWithValue("@Sale_Price", ime.Sale_Price);
                cmd.Parameters.AddWithValue("@Jisha_Price", ime.Jisha_Price);

                cmd.Parameters.AddWithValue("@RakutenPrice", ime.RakutenPrice);
                cmd.Parameters.AddWithValue("@YahooPrice", ime.YahooPrice);
                cmd.Parameters.AddWithValue("@WowmaPrice", ime.WowmaPrice);
                cmd.Parameters.AddWithValue("@JishaPrice", ime.JishaPrice);
                cmd.Parameters.AddWithValue("@TennisPrice", ime.TennisPrice);

                cmd.Parameters.AddWithValue("@Release_Date", ime.Release_Date);
                cmd.Parameters.AddWithValue("@Post_Available_Date", ime.Post_Available_Date);
                cmd.Parameters.AddWithValue("@Year", ime.Year);
                cmd.Parameters.AddWithValue("@Season", ime.Season);
                cmd.Parameters.AddWithValue("@Brand_Code", ime.Brand_Code);
                cmd.Parameters.AddWithValue("@Brand_Name", ime.Brand_Name);
                cmd.Parameters.AddWithValue("@Competition_Name", ime.Competition_Name);
                cmd.Parameters.AddWithValue("@Class_Name", ime.Class_Name);
                cmd.Parameters.AddWithValue("@Catalog_Information", ime.Catalog_Information);
                cmd.Parameters.AddWithValue("@Merchandise_Information", ime.Merchandise_Information);
                cmd.Parameters.AddWithValue("@Zett_Item_Description", ime.Zett_Item_Description);
                cmd.Parameters.AddWithValue("@Zett_Sale_Description", ime.Zett_Sale_Description);
                cmd.Parameters.AddWithValue("@Item_Description_PC", ime.Item_Description_PC);
                cmd.Parameters.AddWithValue("@Sale_Description_PC", ime.Sale_Description_PC);
                cmd.Parameters.AddWithValue("@Smart_Template", ime.Smart_Template);
                cmd.Parameters.AddWithValue("@Rakuten_CategoryID", ime.Rakuten_CategoryID);
                cmd.Parameters.AddWithValue("@Yahoo_CategoryID", ime.Yahoo_CategoryID);
                cmd.Parameters.AddWithValue("@Ponpare_CategoryID", ime.Wowma_CategoryID);
                //cmd.Parameters.AddWithValue("@Tennis_CategoryID", ime.Tennis_CategoryID);
                cmd.Parameters.AddWithValue("@Additional_2", ime.Additional_2);
                cmd.Parameters.AddWithValue("@Additional_3", ime.Additional_3);
                cmd.Parameters.AddWithValue("@Delivery_Charges", ime.Delivery_Charges);
                if (ime.Extra_Shipping != 0)
                    cmd.Parameters.AddWithValue("@Extra_Shipping", ime.Extra_Shipping);
                else
                    cmd.Parameters.AddWithValue("@Extra_Shipping", DBNull.Value);
                cmd.Parameters.AddWithValue("@Maker_Code", ime.Maker_Code);
                cmd.Parameters.AddWithValue("@Shipping_Flag", ime.Shipping_Flag);
                cmd.Parameters.AddWithValue("@Warehouse_Specified",ime.Warehouse_Specified);
                cmd.Parameters.AddWithValue("@BlackMarket_Password", ime.BlackMarket_Password);
                cmd.Parameters.AddWithValue("@DoublePrice_Ctrl_No", ime.DoublePrice_Ctrl_No);
                cmd.Parameters.AddWithValue("@Active", ime.Active);
                cmd.Parameters.AddWithValue("@InactiveComment", ime.InactiveComment);
                cmd.Parameters.AddWithValue("@Updated_By", ime.Updated_By);
                cmd.Parameters.AddWithValue("@Created_Date", System.DateTime.Now);
                cmd.Parameters.AddWithValue("@Updated_Date", System.DateTime.Now);
                cmd.Parameters.AddWithValue("@Option", option);
                cmd.Parameters.AddWithValue("@Yahoo_url",ime.Yahoo_url); //for sks-593
                cmd.Parameters.AddWithValue("@skucheck", ime.Skucheck);
                cmd.Parameters.AddWithValue("@SalesUnit", ime.SalesUnit);
                cmd.Parameters.AddWithValue("@TagInformation", ime.TagInformation);
                cmd.Parameters.AddWithValue("@ContentQuantityNo1", ime.ContentQuantityNo1);
                cmd.Parameters.AddWithValue("@ContentQuantityNo2", ime.ContentQuantityNo2);
                cmd.Parameters.AddWithValue("@ContentUnit1", ime.ContentUnit1);
                cmd.Parameters.AddWithValue("@ContentUnit2", ime.ContentUnit2);
                cmd.Parameters.AddWithValue("@PC_CatchCopy",ime.PC_CatchCopy);
                cmd.Parameters.AddWithValue("@PC_CatchCopy_Mobile", ime.PC_CatchCopy_Mobile);
                cmd.Parameters.AddWithValue("@Cost", ime.Cost);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (id > 0)
                {
                    return id;
                }
                return id;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int MonotaroSaveEdit(Item_Master_Entity ime,int itemID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Monotoro_Item_Master_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Maker_Name", ime.Maker_Name);
                cmd.Parameters.AddWithValue("@Comment", ime.Comment);
                cmd.Parameters.AddWithValue("@Selling_Price", ime.Selling_Price);
                cmd.Parameters.AddWithValue("@Purchase_Price", ime.Purchase_Price);
                cmd.Parameters.AddWithValue("@SellBy", ime.SellBy);
                cmd.Parameters.AddWithValue("@Selling_Rank", ime.Selling_Rank);
                cmd.Parameters.AddWithValue("@Delivery_Days", ime.Delivery_Days);
                cmd.Parameters.AddWithValue("@KSMDelivery_Type", ime.KSMDelivery_Type);
                cmd.Parameters.AddWithValue("@KSMDelivery_Days", ime.KSMDelivery_Days);
                cmd.Parameters.AddWithValue("@Nation_Wide", ime.Nation_Wide);
                cmd.Parameters.AddWithValue("@Hokkaido", ime.Hokkaido);
                cmd.Parameters.AddWithValue("@Okinawa", ime.Okinawa);
                cmd.Parameters.AddWithValue("@Remote_Island", ime.Remote_Island);
                cmd.Parameters.AddWithValue("@Undelivered_Area", ime.Undelivered_Area);
                cmd.Parameters.AddWithValue("@Dangerous_Goods_Contents", ime.Dangerous_Goods_Contents);
                cmd.Parameters.AddWithValue("@Delivery_Method", ime.Delivery_Method);
                cmd.Parameters.AddWithValue("@Delivery_Type", ime.Delivery_Type);
                cmd.Parameters.AddWithValue("@Delivery_Fees", ime.Delivery_Fees);
                cmd.Parameters.AddWithValue("@KSM_Avaliable", ime.KSM_Avaliable);
                cmd.Parameters.AddWithValue("@Returnable_Item", ime.Returnable_Item);
                cmd.Parameters.AddWithValue("@NoApplicable_Law", ime.NoApplicable_Law);
                cmd.Parameters.AddWithValue("@Sales_Permission", ime.Sales_Permission);
                cmd.Parameters.AddWithValue("@Law", ime.Law);
                cmd.Parameters.AddWithValue("@Dangoods_Class", ime.Dangoods_Class);
                cmd.Parameters.AddWithValue("@Dangoods_Name", ime.Dangoods_Name);
                cmd.Parameters.AddWithValue("@Risk_Rating", ime.Risk_Rating);
                cmd.Parameters.AddWithValue("@Dangoods_Nature", ime.Dangoods_Nature);
                cmd.Parameters.AddWithValue("@Fire_Law", ime.Fire_Law);
                cmd.Parameters.AddWithValue("@Item_ID", itemID);
                cmd.Parameters.AddWithValue("@Day_Ship", ime.Day_Ship);
                cmd.Parameters.AddWithValue("@Warehouse_Code", ime.Warehouse_Code);
                cmd.Parameters.AddWithValue("@Return_Necessary", ime.Retrun_Necessary);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (id > 0)
                {
                    return id;
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeExportStatus(string list,int status)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Change_ShopSKUStatus", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@list", list);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Item_Master_Entity SelectMallByID(int id)  
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                //string quary = "SELECT * FROM Item_Master WHERE ID=@ID";
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectByID", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ID", id);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                Item_Master_Entity ime = new Item_Master_Entity();
                if (dt != null && dt.Rows.Count > 0)
                {
                    //if (!DBNull.Value.Equals(dt.Rows[0]["Rakuten_CategoryID"]))
                    //    ime.Rakuten_CategoryID = int.Parse(dt.Rows[0]["Rakuten_CategoryID"].ToString());
                    //if (!DBNull.Value.Equals(dt.Rows[0]["Yahoo_CategoryID"]))
                    //    ime.Yahoo_CategoryID = int.Parse(dt.Rows[0]["Yahoo_CategoryID"].ToString());
                    //if (!DBNull.Value.Equals(dt.Rows[0]["Ponpare_CategoryID"]))
                    //    ime.Ponpare_CategoryID = int.Parse(dt.Rows[0]["Ponpare_CategoryID"].ToString());
                    ime.Rakuten_CategoryID = dt.Rows[0]["Rakuten_CategoryID"].ToString();
                    ime.Yahoo_CategoryID = dt.Rows[0]["Yahoo_CategoryID"].ToString();
                    ime.Wowma_CategoryID = dt.Rows[0]["Wowma_CategoryID"].ToString();
                    //ime.Tennis_CategoryID = dt.Rows[0]["Tennis_CategoryID"].ToString();

                    ime.Rakuten_CategoryName = dt.Rows[0]["Rakuten_CategoryName"].ToString();
                    ime.Yahoo_CategoryName = dt.Rows[0]["Yahoo_CategoryName"].ToString();
                    ime.Wowma_CategoryName = dt.Rows[0]["Wowma_CategoryName"].ToString();
                    //ime.Tennis_CategoryName = dt.Rows[0]["Tennis_CategoryName"].ToString();   hhw

                    //ime.Sale_Code = dt.Rows[0]["Sale_Code"].ToString();

                    //if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Cost"].ToString()))
                    //    ime.Cost = int.Parse(dt.Rows[0]["Cost"].ToString());

                    //ime.Brand_Code_Yahoo = dt.Rows[0]["Brand_Code_Yahoo"].ToString();
                }
                return ime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Item_Master_Entity SelectByID(int id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                //string quary = "SELECT * FROM Item_Master WHERE ID=@ID";
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectByID", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ID", id);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                Item_Master_Entity ime = new Item_Master_Entity();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ime.ID = int.Parse(dt.Rows[0]["ID"].ToString());
                    ime.Ctrl_ID = dt.Rows[0]["Ctrl_ID"].ToString();
                    ime.Item_Code = dt.Rows[0]["Item_Code"].ToString();
                    ime.Item_Name = dt.Rows[0]["Item_Name"].ToString();
                    ime.Product_Code = dt.Rows[0]["Product_Code"].ToString();
                    if (dt.Rows[0]["Release_Date"] != DBNull.Value)
                    {
                        ime.Release_Date = Convert.ToDateTime(dt.Rows[0]["Release_Date"].ToString());
                        //ime.Release_Date =  DateTime.ParseExact(dt.Rows[0]["Release_Date"].ToString(), "yyyy / MM/ dd", null);
                    }
                    if (!DBNull.Value.Equals(dt.Rows[0]["Post_Available_Date"]))
                    {
                        ime.Post_Available_Date = Convert.ToDateTime(dt.Rows[0]["Post_Available_Date"].ToString());
                        //ime.Post_Available_Date = DateTime.ParseExact(dt.Rows[0]["Post_Available_Date"].ToString(), "yyyy / MM/ dd", null);
                    }
                    ime.Season = dt.Rows[0]["Season"].ToString();
                    ime.Brand_Code = dt.Rows[0]["Brand_Code"].ToString();
                    ime.Brand_Name = dt.Rows[0]["Brand_Name"].ToString();
                    ime.Competition_Name = dt.Rows[0]["Competition_Name"].ToString();
                    ime.Class_Name = dt.Rows[0]["Class_Name"].ToString();
                    ime.Catalog_Information = dt.Rows[0]["Catalog_Information"].ToString();
                    ime.Merchandise_Information = dt.Rows[0]["Merchandise_Information"].ToString();
                    ime.Item_Description_PC = dt.Rows[0]["Item_Description_PC"].ToString();
                    ime.Sale_Description_PC = dt.Rows[0]["Sale_Description_PC"].ToString();
                    ime.Zett_Item_Description = dt.Rows[0]["Zett_Item_Description"].ToString();
                    ime.Zett_Sale_Description = dt.Rows[0]["Zett_Sale_Description"].ToString();
                    ime.Smart_Template = dt.Rows[0]["Smart_Template"].ToString();
                    ime.Additional_2 = dt.Rows[0]["Additional_2"].ToString();
                    ime.Additional_3 = dt.Rows[0]["Additional_3"].ToString();
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["List_Price"].ToString()))
                        ime.List_Price = int.Parse(dt.Rows[0]["List_Price"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Cost"].ToString()))
                        ime.Cost = int.Parse(dt.Rows[0]["Cost"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Sale_Price"].ToString()))
                        ime.Sale_Price = int.Parse(dt.Rows[0]["Sale_Price"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Jisha_Price"].ToString()))
                        ime.Jisha_Price = int.Parse(dt.Rows[0]["Jisha_Price"].ToString());

                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["RakutenPrice"].ToString()))
                        ime.RakutenPrice = int.Parse(dt.Rows[0]["RakutenPrice"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["YahooPrice"].ToString()))
                        ime.YahooPrice = int.Parse(dt.Rows[0]["YahooPrice"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["WowmaPrice"].ToString()))
                        ime.WowmaPrice = int.Parse(dt.Rows[0]["WowmaPrice"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["JishaPrice"].ToString()))
                        ime.JishaPrice = int.Parse(dt.Rows[0]["JishaPrice"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["TennisPrice"].ToString()))
                        ime.TennisPrice = int.Parse(dt.Rows[0]["TennisPrice"].ToString());
                    ime.Year = dt.Rows[0]["Year"].ToString();
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Shipping_Flag"].ToString()))
                        ime.Shipping_Flag = int.Parse(dt.Rows[0]["Shipping_Flag"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Special_Flag"].ToString()))
                        ime.Special_Flag = int.Parse(dt.Rows[0]["Special_Flag"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Warehouse_Specified"].ToString()))
                        ime.Warehouse_Specified = int.Parse(dt.Rows[0]["Warehouse_Specified"].ToString());
                    ime.BlackMarket_Password = dt.Rows[0]["BlackMarket_Password"].ToString();
                    ime.DoublePrice_Ctrl_No = dt.Rows[0]["DoublePrice_Ctrl_No"].ToString();
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Extra_Shipping"].ToString()))
                        ime.Extra_Shipping = int.Parse(dt.Rows[0]["Extra_Shipping"].ToString());
                    else ime.Extra_Shipping = 0;
                    //if (!DBNull.Value.Equals(dt.Rows[0]["Rakuten_CategoryID"]))
                    //    ime.Rakuten_CategoryID = int.Parse(dt.Rows[0]["Rakuten_CategoryID"].ToString());
                    //if (!DBNull.Value.Equals(dt.Rows[0]["Yahoo_CategoryID"]))
                    //    ime.Yahoo_CategoryID = int.Parse(dt.Rows[0]["Yahoo_CategoryID"].ToString());
                    //if (!DBNull.Value.Equals(dt.Rows[0]["Ponpare_CategoryID"]))
                    //    ime.Ponpare_CategoryID = int.Parse(dt.Rows[0]["Ponpare_CategoryID"].ToString());
                    ime.Maker_Code = dt.Rows[0]["Maker_Code"].ToString();
                    ime.Rakuten_CategoryID = dt.Rows[0]["Rakuten_CategoryID"].ToString();
                    ime.Yahoo_CategoryID = dt.Rows[0]["Yahoo_CategoryID"].ToString();
                    ime.Wowma_CategoryID = dt.Rows[0]["Wowma_CategoryID"].ToString();
                    //ime.Tennis_CategoryID = dt.Rows[0]["Tennis_CategoryID"].ToString();

                    ime.Rakuten_CategoryName = dt.Rows[0]["Rakuten_CategoryName"].ToString();
                    ime.Yahoo_CategoryName = dt.Rows[0]["Yahoo_CategoryName"].ToString();
                    ime.Wowma_CategoryName = dt.Rows[0]["Wowma_CategoryName"].ToString();
                    //ime.Tennis_CategoryName = dt.Rows[0]["Tennis_CategoryName"].ToString();

                    ime.Rakuten_CategoryPath = dt.Rows[0]["Rakuten_MallCategoryPath"].ToString();
                    ime.Yahoo_CategoryPath = dt.Rows[0]["Yahoo_MallCategoryPath"].ToString();
                    ime.Wowma_CategoryPath = dt.Rows[0]["Wowma_MallCategoryPath"].ToString();
                    //ime.Tennis_CategoryPath = dt.Rows[0]["Tennis_MallCategoryPath"].ToString();

                    ime.Yahoo_url = dt.Rows[0]["URL_Yahoo"].ToString(); // Yahoo URL sks-593
                    ime.SalesUnit = dt.Rows[0]["Sales_unit"].ToString();
                    ime.TagInformation = dt.Rows[0]["Tag_Info"].ToString();
                    ime.ContentQuantityNo1 = dt.Rows[0]["Content_quantity_number_1"].ToString();
                    ime.ContentQuantityNo2 = dt.Rows[0]["Content_quantity_number_2"].ToString();
                    ime.ContentUnit1 = dt.Rows[0]["Contents_unit_1"].ToString();
                    ime.ContentUnit2 = dt.Rows[0]["Contents_unit_2"].ToString();
                    ime.PC_CatchCopy = dt.Rows[0]["PC_CatchCopy"].ToString();
                    ime.PC_CatchCopy_Mobile = dt.Rows[0]["PC_CatchCopy_Mobile"].ToString();
                    ime.Maker_Name = dt.Rows[0]["Maker_Name"].ToString();
                    ime.Comment = dt.Rows[0]["Item_Details_Registration_Comment"].ToString();
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Market_Selling_Price"].ToString()))
                    ime.Selling_Price =int.Parse(dt.Rows[0]["Market_Selling_Price"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Purchase_Price"].ToString()))
                    ime.Purchase_Price = int.Parse(dt.Rows[0]["Purchase_Price"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Sell_By"].ToString()))
                    ime.SellBy = int.Parse(dt.Rows[0]["Sell_By"].ToString());
                    ime.Selling_Rank = dt.Rows[0]["Selling_Rank"].ToString();
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Delivery_Day"].ToString()))
                    ime.Delivery_Days = int.Parse(dt.Rows[0]["Delivery_Day"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Customer_Delivery_Type"].ToString()))
                    ime.KSMDelivery_Type = int.Parse(dt.Rows[0]["Customer_Delivery_Type"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Customer_Delivery_Day"].ToString()))
                    ime.KSMDelivery_Days = int.Parse(dt.Rows[0]["Customer_Delivery_Day"].ToString());
                    ime.Nation_Wide = dt.Rows[0]["Nation_Wide"].ToString(); // Yahoo URL sks-593
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Hokkaido"].ToString()))
                    ime.Hokkaido = int.Parse(dt.Rows[0]["Hokkaido"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Okinawa"].ToString()))
                    ime.Okinawa = int.Parse(dt.Rows[0]["Okinawa"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Remote_Island"].ToString()))
                    ime.Remote_Island = int.Parse(dt.Rows[0]["Remote_Island"].ToString());
                    ime.Undelivered_Area = dt.Rows[0]["Undelivered_Area"].ToString();
                    ime.Dangerous_Goods_Contents = dt.Rows[0]["Dangerous_Goods_Contents"].ToString();
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Delivery_Method"].ToString()))
                    ime.Delivery_Method = int.Parse(dt.Rows[0]["Delivery_Method"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Delivery_Type"].ToString()))
                    ime.Delivery_Type = int.Parse(dt.Rows[0]["Delivery_Type"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Cash_On_Delivery_Fee"].ToString()))
                    ime.Delivery_Fees = int.Parse(dt.Rows[0]["Cash_On_Delivery_Fee"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Customer_Assembly"].ToString()))
                    ime.KSM_Avaliable = int.Parse(dt.Rows[0]["Customer_Assembly"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Return_Type"].ToString()))
                    ime.Returnable_Item = int.Parse(dt.Rows[0]["Return_Type"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Applicable_Law"].ToString()))
                    ime.NoApplicable_Law = int.Parse(dt.Rows[0]["Applicable_Law"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Sales_Permission"].ToString()))
                    ime.Sales_Permission = int.Parse(dt.Rows[0]["Sales_Permission"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Laws_And_Regulation"].ToString()))
                    ime.Law = int.Parse(dt.Rows[0]["Laws_And_Regulation"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Dangarous_Goods"].ToString()))
                    ime.Dangoods_Class = int.Parse(dt.Rows[0]["Dangarous_Goods"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Dangarous_Goods_Name"].ToString()))
                    ime.Dangoods_Name = int.Parse(dt.Rows[0]["Dangarous_Goods_Name"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Risk_Rating"].ToString()))
                    ime.Risk_Rating = int.Parse(dt.Rows[0]["Risk_Rating"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Dangerous_Goods_Nature"].ToString()))
                    ime.Dangoods_Nature = int.Parse(dt.Rows[0]["Dangerous_Goods_Nature"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Fire_Service_Law"].ToString()))
                    ime.Fire_Law = int.Parse(dt.Rows[0]["Fire_Service_Law"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Days_Ship"].ToString()))
                        ime.Day_Ship = int.Parse(dt.Rows[0]["Days_Ship"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Warehouse_Code"].ToString()))
                        ime.Warehouse_Code = int.Parse(dt.Rows[0]["Warehouse_Code"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Return_Necessary"].ToString()))
                        ime.Retrun_Necessary  = int.Parse(dt.Rows[0]["Return_Necessary"].ToString());
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["cost_rate"].ToString()))
                        ime.CostRate = dt.Rows[0]["cost_rate"].ToString();
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["discount_rate"].ToString()))
                        ime.DiscountRate = dt.Rows[0]["discount_rate"].ToString();
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["profit_rate"].ToString()))
                        ime.ProfitRate = dt.Rows[0]["profit_rate"].ToString();
                }
                return ime;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectItemImage(String id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Item_Image WHERE Item_ID=" + id, connectionString);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ItemImageInsert(String id, String Image)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("INSERT INTO Item_Image(Item_ID,Image_Name,Image_Type) VALUES(" + id + ",'" + Image + "',0)", connectionString);
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean Update(Item_Master_Entity ime)
        {
            try 
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Master_Update", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ID", ime.ID);
                cmd.Parameters.AddWithValue("@Item_Code", ime.Item_Code);
                cmd.Parameters.AddWithValue("@Rakuten_CategoryID", ime.Rakuten_CategoryID);
                cmd.Parameters.AddWithValue("@Yahoo_CategoryID", ime.Yahoo_CategoryID);
                cmd.Parameters.AddWithValue("@Ponpare_CategoryID", ime.Wowma_CategoryID);
                //cmd.Parameters.AddWithValue("@Tennis_CategoryID", ime.Tennis_CategoryID);
                cmd.Parameters.AddWithValue("@Item_Description_PC", ime.Item_Description_PC);
                cmd.Parameters.AddWithValue("@Sale_Description_PC", ime.Sale_Description_PC);
                cmd.Parameters.AddWithValue("@Rakuten_evidence", ime.Rakuten_evidence);
                cmd.Parameters.AddWithValue("@Cloudshop_mode", ime.Cloudshop_mode);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public int CheckExistsItemCode(string ItemCode)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Check_ItemCode_Exists", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", ItemCode);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["ID"]);
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(Item_Master_Entity ime, int pageIndex, int pageSize,int option,int search)
        {
            try
            {
                if (search == 1)
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter("SP_Item_View_Search", connectionString);
                    sda.SelectCommand.CommandTimeout = 0;
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //txtImageFileName field not found
                    //CategoryName field not found
                    if (!String.IsNullOrWhiteSpace(ime.Item_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Item_Name", ime.Item_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Item_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Item_Code))
                        sda.SelectCommand.Parameters.AddWithValue("@Item_Code", ime.Item_Code);
                    else sda.SelectCommand.Parameters.AddWithValue("@Item_Code", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Catalog_Information))
                        sda.SelectCommand.Parameters.AddWithValue("@Catalog_Information", ime.Catalog_Information);
                    else sda.SelectCommand.Parameters.AddWithValue("@Catalog_Information", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Brand_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Brand_Name", ime.Brand_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Brand_Name", DBNull.Value);


                    if (!String.IsNullOrWhiteSpace(ime.Competition_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Competition_Name", ime.Competition_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Competition_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Year))
                        sda.SelectCommand.Parameters.AddWithValue("@Year", ime.Year);
                    else sda.SelectCommand.Parameters.AddWithValue("@Year", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Season))
                        sda.SelectCommand.Parameters.AddWithValue("@Season", ime.Season);
                    else sda.SelectCommand.Parameters.AddWithValue("@Season", DBNull.Value);

                    sda.SelectCommand.Parameters.AddWithValue("@Special_Flag", ime.Special_Flag);
                    sda.SelectCommand.Parameters.AddWithValue("@Reservation_Flag", ime.Reservation_Flag);
                    sda.SelectCommand.Parameters.AddWithValue("@Export_Status", ime.Export_Status);
                    sda.SelectCommand.Parameters.AddWithValue("@Updated_By", ime.Updated_By);

                    if (!String.IsNullOrWhiteSpace(ime.Ctrl_ID))
                        sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", ime.Ctrl_ID);
                    else sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Color_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Color_Name", ime.Color_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Color_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Image_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Image_Name", ime.Image_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Image_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Cate_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Cate_Name", ime.Cate_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Cate_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.IdList))
                        sda.SelectCommand.Parameters.AddWithValue("@IdList", ime.IdList);
                    else sda.SelectCommand.Parameters.AddWithValue("@IdList", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.RemoveList))
                        sda.SelectCommand.Parameters.AddWithValue("@RemoveList", ime.RemoveList);
                    else sda.SelectCommand.Parameters.AddWithValue("@RemoveList", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.MasterKeyword))
                        sda.SelectCommand.Parameters.AddWithValue("@MasterKeyword", ime.MasterKeyword);
                    else sda.SelectCommand.Parameters.AddWithValue("@MasterKeyword", DBNull.Value);

                    sda.SelectCommand.Parameters.AddWithValue("@Price",ime.Price);
                    sda.SelectCommand.Parameters.AddWithValue("@PageIndex", pageIndex);
                    sda.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                    sda.SelectCommand.Parameters.AddWithValue("@Option", option);
                    sda.SelectCommand.Parameters.AddWithValue("@ShopID", ime.ShopID);
                    sda.SelectCommand.Connection.Open();
                    sda.Fill(dt);
                    sda.SelectCommand.Connection.Close();

                    return dt;
                }
                else
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter("SP_Item_View_Select", connectionString);
                    sda.SelectCommand.CommandTimeout = 0;
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.AddWithValue("@PageIndex", pageIndex);
                    sda.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                    sda.SelectCommand.Parameters.AddWithValue("@Option", option);
                    sda.SelectCommand.Connection.Open();
                    sda.Fill(dt);
                    sda.SelectCommand.Connection.Close();

                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Monotaro_SelectAll(Item_Master_Entity ime, int pageIndex, int pageSize, int option, int search)
        {
            try
            {
                if (search == 1)
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter("SP_Monotaro_Item_View_Search", connectionString);
                    sda.SelectCommand.CommandTimeout = 0;
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //txtImageFileName field not found
                    //CategoryName field not found
                    if (!String.IsNullOrWhiteSpace(ime.Item_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Item_Name", ime.Item_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Item_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Item_Code))
                        sda.SelectCommand.Parameters.AddWithValue("@Item_Code", ime.Item_Code);
                    else sda.SelectCommand.Parameters.AddWithValue("@Item_Code", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Catalog_Information))
                        sda.SelectCommand.Parameters.AddWithValue("@Catalog_Information", ime.Catalog_Information);
                    else sda.SelectCommand.Parameters.AddWithValue("@Catalog_Information", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Brand_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Brand_Name", ime.Brand_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Brand_Name", DBNull.Value);


                    if (!String.IsNullOrWhiteSpace(ime.Competition_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Competition_Name", ime.Competition_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Competition_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Year))
                        sda.SelectCommand.Parameters.AddWithValue("@Year", ime.Year);
                    else sda.SelectCommand.Parameters.AddWithValue("@Year", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Season))
                        sda.SelectCommand.Parameters.AddWithValue("@Season", ime.Season);
                    else sda.SelectCommand.Parameters.AddWithValue("@Season", DBNull.Value);

                    sda.SelectCommand.Parameters.AddWithValue("@Special_Flag", ime.Special_Flag);
                    sda.SelectCommand.Parameters.AddWithValue("@Reservation_Flag", ime.Reservation_Flag);
                    sda.SelectCommand.Parameters.AddWithValue("@Export_Status", ime.Export_Status);
                    sda.SelectCommand.Parameters.AddWithValue("@Updated_By", ime.Updated_By);

                    if (!String.IsNullOrWhiteSpace(ime.Ctrl_ID))
                        sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", ime.Ctrl_ID);
                    else sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Color_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Color_Name", ime.Color_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Color_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Image_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Image_Name", ime.Image_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Image_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Cate_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Cate_Name", ime.Cate_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Cate_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.IdList))
                        sda.SelectCommand.Parameters.AddWithValue("@IdList", ime.IdList);
                    else sda.SelectCommand.Parameters.AddWithValue("@IdList", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.RemoveList))
                        sda.SelectCommand.Parameters.AddWithValue("@RemoveList", ime.RemoveList);
                    else sda.SelectCommand.Parameters.AddWithValue("@RemoveList", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.MasterKeyword))
                        sda.SelectCommand.Parameters.AddWithValue("@MasterKeyword", ime.MasterKeyword);
                    else sda.SelectCommand.Parameters.AddWithValue("@MasterKeyword", DBNull.Value);

                    sda.SelectCommand.Parameters.AddWithValue("@Price", ime.Price);
                    sda.SelectCommand.Parameters.AddWithValue("@PageIndex", pageIndex);
                    sda.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                    sda.SelectCommand.Parameters.AddWithValue("@Option", option);
                    sda.SelectCommand.Parameters.AddWithValue("@ShopID", ime.ShopID);
                    sda.SelectCommand.Parameters.AddWithValue("@RStatus", ime.Ready);
                    sda.SelectCommand.Connection.Open();
                    sda.Fill(dt);
                    sda.SelectCommand.Connection.Close();

                    return dt;
                }
                else
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter("SP_Item_View_Select", connectionString);
                    sda.SelectCommand.CommandTimeout = 0;
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.AddWithValue("@PageIndex", pageIndex);
                    sda.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                    sda.SelectCommand.Parameters.AddWithValue("@Option", option);
                    sda.SelectCommand.Connection.Open();
                    sda.Fill(dt);
                    sda.SelectCommand.Connection.Close();

                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SelectAllByStatus(Item_Master_Entity ime, int pageIndex, int pageSize, int option, int search)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Status_Search", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (!String.IsNullOrWhiteSpace(ime.Item_Code))
                    sda.SelectCommand.Parameters.AddWithValue("@Item_Code", ime.Item_Code);
                else sda.SelectCommand.Parameters.AddWithValue("@Item_Code", DBNull.Value);
                sda.SelectCommand.Parameters.AddWithValue("@Export_Status", ime.Export_Status);
                if (!String.IsNullOrWhiteSpace(ime.Ctrl_ID))
                    sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", ime.Ctrl_ID);
                else sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", DBNull.Value);
                sda.SelectCommand.Parameters.AddWithValue("@PageIndex", pageIndex);
                sda.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                sda.SelectCommand.Parameters.AddWithValue("@Option", option);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ExportSelectAll(Item_Master_Entity ime, int pageIndex, int pageSize, int option, int search)
        {
            try
            {
                if (search == 1)
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter("SP_ItemView_ExportSelectData", connectionString);
                    //SqlCommand cmd = new SqlCommand("SP_Item_View_SelectAll_Quick", connectionString);
                    sda.SelectCommand.CommandTimeout = 0;
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //txtImageFileName field not found
                    //CategoryName field not found
                    if (!String.IsNullOrWhiteSpace(ime.Item_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Item_Name", ime.Item_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Item_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Item_Code))
                        sda.SelectCommand.Parameters.AddWithValue("@Item_Code", ime.Item_Code);
                    else sda.SelectCommand.Parameters.AddWithValue("@Item_Code", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Catalog_Information))
                        sda.SelectCommand.Parameters.AddWithValue("@Catalog_Information", ime.Catalog_Information);
                    else sda.SelectCommand.Parameters.AddWithValue("@Catalog_Information", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Brand_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Brand_Name", ime.Brand_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Brand_Name", DBNull.Value);


                    if (!String.IsNullOrWhiteSpace(ime.Competition_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Competition_Name", ime.Competition_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Competition_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Year))
                        sda.SelectCommand.Parameters.AddWithValue("@Year", ime.Year);
                    else sda.SelectCommand.Parameters.AddWithValue("@Year", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Season))
                        sda.SelectCommand.Parameters.AddWithValue("@Season", ime.Season);
                    else sda.SelectCommand.Parameters.AddWithValue("@Season", DBNull.Value);

                    sda.SelectCommand.Parameters.AddWithValue("@Special_Flag", ime.Special_Flag);
                    sda.SelectCommand.Parameters.AddWithValue("@Reservation_Flag", ime.Reservation_Flag);
                    sda.SelectCommand.Parameters.AddWithValue("@Export_Status", ime.Export_Status);
                    sda.SelectCommand.Parameters.AddWithValue("@Updated_By", ime.Updated_By);

                    if (!String.IsNullOrWhiteSpace(ime.Ctrl_ID))
                        sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", ime.Ctrl_ID);
                    else sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Color_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Color_Name", ime.Color_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Color_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Image_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Image_Name", ime.Image_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Image_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.Cate_Name))
                        sda.SelectCommand.Parameters.AddWithValue("@Cate_Name", ime.Cate_Name);
                    else sda.SelectCommand.Parameters.AddWithValue("@Cate_Name", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.IdList))
                        sda.SelectCommand.Parameters.AddWithValue("@IdList", ime.IdList);
                    else sda.SelectCommand.Parameters.AddWithValue("@IdList", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.RemoveList))
                        sda.SelectCommand.Parameters.AddWithValue("@RemoveList", ime.RemoveList);
                    else sda.SelectCommand.Parameters.AddWithValue("@RemoveList", DBNull.Value);

                    if (!String.IsNullOrWhiteSpace(ime.MasterKeyword))
                        sda.SelectCommand.Parameters.AddWithValue("@MasterKeyword", ime.MasterKeyword);
                    else sda.SelectCommand.Parameters.AddWithValue("@MasterKeyword", DBNull.Value);

                    sda.SelectCommand.Parameters.AddWithValue("@PageIndex", pageIndex);
                    sda.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                    sda.SelectCommand.Parameters.AddWithValue("@Option", option);

                    sda.SelectCommand.Connection.Open();
                    sda.Fill(dt);
                    sda.SelectCommand.Connection.Close();

                    return dt;
                }
                else
                {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter("SP_Item_View_Select", connectionString);
                    sda.SelectCommand.CommandTimeout = 0;
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.AddWithValue("@PageIndex", pageIndex);
                    sda.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                    sda.SelectCommand.Parameters.AddWithValue("@Option", option);
                    sda.SelectCommand.Connection.Open();
                    sda.Fill(dt);
                    sda.SelectCommand.Connection.Close();

                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Search(Item_Master_Entity ime,int LastId,int option,int PageSize)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SELECTALL", connectionString);
                //SqlCommand cmd = new SqlCommand("SP_Item_View_SelectAll_Quick", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                //txtImageFileName field not found
                //CategoryName field not found
                if (!String.IsNullOrWhiteSpace(ime.Item_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Item_Name", ime.Item_Name);
                else sda.SelectCommand.Parameters.AddWithValue("@Item_Name", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Item_Code))
                    sda.SelectCommand.Parameters.AddWithValue("@Item_Code", ime.Item_Code);
                else sda.SelectCommand.Parameters.AddWithValue("@Item_Code", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Catalog_Information))
                    sda.SelectCommand.Parameters.AddWithValue("@Catalog_Information", ime.Catalog_Information);
                else sda.SelectCommand.Parameters.AddWithValue("@Catalog_Information", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Brand_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Brand_Name", ime.Brand_Name);
                else sda.SelectCommand.Parameters.AddWithValue("@Brand_Name", DBNull.Value);


                if (!String.IsNullOrWhiteSpace(ime.Competition_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Competition_Name", ime.Competition_Name);
                else sda.SelectCommand.Parameters.AddWithValue("@Competition_Name", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Year))
                    sda.SelectCommand.Parameters.AddWithValue("@Year", ime.Year);
                else sda.SelectCommand.Parameters.AddWithValue("@Year", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Season))
                    sda.SelectCommand.Parameters.AddWithValue("@Season", ime.Season);
                else sda.SelectCommand.Parameters.AddWithValue("@Season", DBNull.Value);

                sda.SelectCommand.Parameters.AddWithValue("@Special_Flag", ime.Special_Flag);
                sda.SelectCommand.Parameters.AddWithValue("@Reservation_Flag", ime.Reservation_Flag);
                sda.SelectCommand.Parameters.AddWithValue("@Export_Status", ime.Export_Status);
                sda.SelectCommand.Parameters.AddWithValue("@Updated_By", ime.Updated_By);

                if (!String.IsNullOrWhiteSpace(ime.Ctrl_ID))
                    sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", ime.Ctrl_ID);
                else sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Color_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Color_Name", ime.Color_Name);
                else sda.SelectCommand.Parameters.AddWithValue("@Color_Name", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Image_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Image_Name", ime.Image_Name);
                else sda.SelectCommand.Parameters.AddWithValue("@Image_Name", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.IdList))
                    sda.SelectCommand.Parameters.AddWithValue("@IdList", ime.IdList);
                else sda.SelectCommand.Parameters.AddWithValue("@IdList", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.RemoveList))
                    sda.SelectCommand.Parameters.AddWithValue("@RemoveList", ime.RemoveList);
                else sda.SelectCommand.Parameters.AddWithValue("@RemoveList", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.MasterKeyword))
                    sda.SelectCommand.Parameters.AddWithValue("@MasterKeyword", ime.MasterKeyword);
                else sda.SelectCommand.Parameters.AddWithValue("@MasterKeyword", DBNull.Value);

                sda.SelectCommand.Parameters.AddWithValue("@PageSize", PageSize);
                sda.SelectCommand.Parameters.AddWithValue("@LastID", option);
                sda.SelectCommand.Parameters.AddWithValue("@Option", option);

                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll_ItemView3(Item_Master_Entity ime, int pageIndex, int pageSize, int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_View3_SelectAll_Quick", connectionString);
                //SqlCommand cmd = new SqlCommand("SP_Item_View_SelectAll_Quick", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                //txtImageFileName field not found
                //CategoryName field not found
                if (!String.IsNullOrWhiteSpace(ime.Item_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Item_Name", ime.Item_Name);
                else sda.SelectCommand.Parameters.AddWithValue("@Item_Name", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Item_Code))
                    sda.SelectCommand.Parameters.AddWithValue("@Item_Code", ime.Item_Code);
                else sda.SelectCommand.Parameters.AddWithValue("@Item_Code", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Catalog_Information))
                    sda.SelectCommand.Parameters.AddWithValue("@Catalog_Information", ime.Catalog_Information);
                else sda.SelectCommand.Parameters.AddWithValue("@Catalog_Information", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Brand_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Brand_Name", ime.Brand_Name);
                else sda.SelectCommand.Parameters.AddWithValue("@Brand_Name", DBNull.Value);


                if (!String.IsNullOrWhiteSpace(ime.Competition_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Competition_Name", ime.Competition_Name);
                else sda.SelectCommand.Parameters.AddWithValue("@Competition_Name", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Year))
                    sda.SelectCommand.Parameters.AddWithValue("@Year", ime.Year);
                else sda.SelectCommand.Parameters.AddWithValue("@Year", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Season))
                    sda.SelectCommand.Parameters.AddWithValue("@Season", ime.Season);
                else sda.SelectCommand.Parameters.AddWithValue("@Season", DBNull.Value);

                sda.SelectCommand.Parameters.AddWithValue("@Special_Flag", ime.Special_Flag);
                sda.SelectCommand.Parameters.AddWithValue("@Reservation_Flag", ime.Reservation_Flag);
                sda.SelectCommand.Parameters.AddWithValue("@Export_Status", ime.Export_Status);
                sda.SelectCommand.Parameters.AddWithValue("@Updated_By", ime.Updated_By);

                if (!String.IsNullOrWhiteSpace(ime.Ctrl_ID))
                    sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", ime.Ctrl_ID);
                else sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Color_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Color_Name", ime.Color_Name);
                else sda.SelectCommand.Parameters.AddWithValue("@Color_Name", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Image_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Image_Name", ime.Image_Name);
                else sda.SelectCommand.Parameters.AddWithValue("@Image_Name", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.IdList))
                    sda.SelectCommand.Parameters.AddWithValue("@IdList", ime.IdList);
                else sda.SelectCommand.Parameters.AddWithValue("@IdList", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.RemoveList))
                    sda.SelectCommand.Parameters.AddWithValue("@RemoveList", ime.RemoveList);
                else sda.SelectCommand.Parameters.AddWithValue("@RemoveList", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.MasterKeyword))
                    sda.SelectCommand.Parameters.AddWithValue("@MasterKeyword", ime.MasterKeyword);
                else sda.SelectCommand.Parameters.AddWithValue("@MasterKeyword", DBNull.Value);

                sda.SelectCommand.Parameters.AddWithValue("@PageIndex", pageIndex);
                sda.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                sda.SelectCommand.Parameters.AddWithValue("@Option", option);

                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(Item_Master_Entity ime)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_Item_Master_SELECTALL", connectionString);
                cmd.CommandTimeout = 0;

                //txtImageFileName field not found
                //CategoryName field not found
                if(!String.IsNullOrWhiteSpace(ime.Item_Name))
                    cmd.Parameters.AddWithValue("@Item_Name", ime.Item_Name);
                else cmd.Parameters.AddWithValue("@Item_Name", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Item_Code))
                    cmd.Parameters.AddWithValue("@Item_Code", ime.Item_Code);
                else cmd.Parameters.AddWithValue("@Item_Code", DBNull.Value);

                if(!String.IsNullOrWhiteSpace(ime.Catalog_Information))
                    cmd.Parameters.AddWithValue("@Catalog_Information", ime.Catalog_Information);
                else cmd.Parameters.AddWithValue("@Catalog_Information", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Brand_Name))
                    cmd.Parameters.AddWithValue("@Brand_Name", ime.Brand_Name);
                else cmd.Parameters.AddWithValue("@Brand_Name", DBNull.Value);


                if (!String.IsNullOrWhiteSpace(ime.Competition_Name))
                    cmd.Parameters.AddWithValue("@Competition_Name", ime.Competition_Name);
                else cmd.Parameters.AddWithValue("@Competition_Name", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Year))
                    cmd.Parameters.AddWithValue("@Year", ime.Year);
                else cmd.Parameters.AddWithValue("@Year", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Season))
                    cmd.Parameters.AddWithValue("@Season", ime.Season);
                else cmd.Parameters.AddWithValue("@Season", DBNull.Value);

                cmd.Parameters.AddWithValue("@Special_Flag", ime.Special_Flag);
                cmd.Parameters.AddWithValue("@Reservation_Flag", ime.Reservation_Flag);
                cmd.Parameters.AddWithValue("@Export_Status", ime.Export_Status);

                if (!String.IsNullOrWhiteSpace(ime.Ctrl_ID))
                    cmd.Parameters.AddWithValue("@Ctrl_ID", ime.Ctrl_ID);
                else cmd.Parameters.AddWithValue("@Ctrl_ID", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Color_Name))
                    cmd.Parameters.AddWithValue("@Color_Name", ime.Color_Name);
                else cmd.Parameters.AddWithValue("@Color_Name", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.Image_Name))
                    cmd.Parameters.AddWithValue("@Image_Name", ime.Image_Name);
                else cmd.Parameters.AddWithValue("@Image_Name", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.IdList))
                    cmd.Parameters.AddWithValue("@IdList", ime.IdList);
                else cmd.Parameters.AddWithValue("@IdList", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.RemoveList))
                    cmd.Parameters.AddWithValue("@RemoveList", ime.RemoveList);
                else cmd.Parameters.AddWithValue("@RemoveList", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.MasterKeyword))
                    cmd.Parameters.AddWithValue("@MasterKeyword", ime.MasterKeyword);
                else cmd.Parameters.AddWithValue("@MasterKeyword", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ime.PersonInCharge) && !ime.PersonInCharge.Contains("--Select--"))
                    cmd.Parameters.AddWithValue("@ContactPerson", ime.PersonInCharge);
                else if (String.IsNullOrWhiteSpace(ime.PersonInCharge) || ime.PersonInCharge.Contains("--Select--"))
                    cmd.Parameters.AddWithValue("@ContactPerson", 0);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ItemView3_SelectAll()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_Item_Master_SELECTALL", connectionString);
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string quary = "SELECT ROW_NUMBER() OVER (ORDER BY Item_Master.ID) AS 'No',*,(SELECT Top 1 Image_Name  FROM Item_Image WHERE Item_ID = Item_Master.ID  AND Item_Image.Image_Type =0)as Image_Name FROM Item_Master";

                SqlDataAdapter sda = new SqlDataAdapter(quary, connectionString);
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.CommandTimeout = 0;

                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SelectItemID(string ItemCode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Master_SelectItemCode", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_Code", ItemCode);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                if (cmd.Parameters["@result"].Value != DBNull.Value)
                    return Convert.ToInt32(cmd.Parameters["@result"].Value);
                else return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectValueByItemID(int id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = String.Format("SELECT Item_Code AS 商品番号,Item_Name,List_Price,Sale_Price,Item_Description_PC AS PC用商品説明文 ,Sale_Description_PC AS PC用販売説明文  FROM Item_Master WHERE ID = '{0}'", id);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectByItemCode(string ItemCode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = String.Format("SELECT * FROM Item_Master WHERE Item_Code = '{0}'",ItemCode);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SelectByItemCode(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = String.Format("SELECT Item_Code FROM Item_Master WITH (NOLOCK) WHERE ID = '{0}'", Item_ID);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Item_Code"].ToString();
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SearchItemCode(string code)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SELECT ID,Item_Code,Item_Name FROM Item_Master Where Item_Code LIKE '" + "%" + code + "%'";
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(query, connectionString);
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;

            }catch(Exception ex)
            {
                throw ex;
            }

        }

        // Get Shop List by Export Item ID
        public DataTable GetShopList(string str , string mall_name)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetShopList_ByMall", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@strString", str);
                sda.SelectCommand.Parameters.AddWithValue("@mall_name", mall_name);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetShopList(string str)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetShopList", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@strString", str);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  

        public int CheckImport_ShopItem(int shop_ID, string item_Code)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Import_ShopItem_Check", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_ID);
                cmd.Parameters.AddWithValue("@Item_Code", item_Code);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return Convert.ToInt32(cmd.Parameters["@result"].Value);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int CheckImport_ShopItemInventory(int shop_ID, string item_AdminCode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Import_ShopItem_Inventory_Check", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_ID);
                cmd.Parameters.AddWithValue("@Item_AdminCode", item_AdminCode);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return Convert.ToInt32(cmd.Parameters["@result"].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int CheckImport_ShopItemCategory(int shop_ID, string item_AdminCode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Import_ShopItem_Category_Check", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Shop_ID", shop_ID);
                cmd.Parameters.AddWithValue("@Item_AdminCode", item_AdminCode);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return Convert.ToInt32(cmd.Parameters["@result"].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectByItemDataForRakuten(int shop_ID, string str, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetDataForRakuten", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_ID);
                sda.SelectCommand.Parameters.AddWithValue("@strString", str);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectByItemDataForYahoo(string itemIDList, string option,int Shop_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetDataForYahoo", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ItemID", itemIDList);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", Shop_ID);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectByItemDataForYahoo(string itemIDList, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetDataForYahoo", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ItemID", itemIDList);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectByItemDataForPonpare(int shop_ID, string str, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetDataForPonpare", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_ID);
                sda.SelectCommand.Parameters.AddWithValue("@strString", str);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectByItemDataForAmazon(string strItemID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetDataForAmazon", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@strItemID", strItemID);

                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectByItemDataForJisha(int shop_ID, string str, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetDataForJisha", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_ID);
                sda.SelectCommand.Parameters.AddWithValue("@strString", str);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Search(string Item_Name,
                                                    string Item_Code,
                                                    string Image_Name,
                                                    string Catalog_Infromation,
                                                    string Brand_Name,
                                                    string Category_Name,
                                                    string Competition_Name,
                                                    string Color_Name,
                                                    string Year,
                                                    string Season,
                                                    string Export_Status,
                                                    string Ctrl_ID,
                                                    string Special_Flag,
                                                    string Reservation_Flag,
                                                    string Person,
                                                    string Keyword)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //string query = "SELECT ID,Item_Code,Item_Name FROM Item_Master Where Item_Code LIKE '" + "%" + code + "%'";
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_Search", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                if(String.IsNullOrWhiteSpace(Item_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Item_Name", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Item_Name", Item_Name);

                if (String.IsNullOrWhiteSpace(Item_Code))
                    sda.SelectCommand.Parameters.AddWithValue("@Item_Code", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);

                if (String.IsNullOrWhiteSpace(Image_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Image_Name", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Image_Name", Image_Name);

                if (String.IsNullOrWhiteSpace(Catalog_Infromation))
                    sda.SelectCommand.Parameters.AddWithValue("@Catalog_Information", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Catalog_Information", Catalog_Infromation);

                if (String.IsNullOrWhiteSpace(Brand_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Brand_Name", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Brand_Name", Brand_Name);

                if (String.IsNullOrWhiteSpace(Category_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Category_Name", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Category_Name", Category_Name);

                if (String.IsNullOrWhiteSpace(Competition_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Competition_Name", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Competition_Name", Competition_Name);

                if (String.IsNullOrWhiteSpace(Color_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Color_Name", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Color_Name", Color_Name);

                if (String.IsNullOrWhiteSpace(Year))
                    sda.SelectCommand.Parameters.AddWithValue("@Year", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Year", Year);

                if (String.IsNullOrWhiteSpace(Season))
                    sda.SelectCommand.Parameters.AddWithValue("@Season", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Season", Season);

                if (String.IsNullOrWhiteSpace(Export_Status))
                    Export_Status = "0";
                sda.SelectCommand.Parameters.AddWithValue("@Export_Status",Convert.ToInt32(Export_Status));

                if (String.IsNullOrWhiteSpace(Ctrl_ID))
                    sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Ctrl_ID", Ctrl_ID);

                if (String.IsNullOrWhiteSpace(Special_Flag))
                    Special_Flag = "0";
                sda.SelectCommand.Parameters.AddWithValue("@Special_Flag", Convert.ToInt32(Special_Flag));

                if (String.IsNullOrWhiteSpace(Reservation_Flag))
                    Reservation_Flag = "0";
                sda.SelectCommand.Parameters.AddWithValue("@Reservation_Flag", Convert.ToInt32(Reservation_Flag));

                if (Person=="--Select--")
                    Person = "0";
                sda.SelectCommand.Parameters.AddWithValue("@Person", Convert.ToInt32(Person));

                if (String.IsNullOrWhiteSpace(Keyword))
                    sda.SelectCommand.Parameters.AddWithValue("@Keyword", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Keyword", Keyword);

                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SearchItemViewQuick(string Item_Name, string Item_Code, int pageIndex, int pageSize, int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_View_Quick_Search", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;


                if (String.IsNullOrWhiteSpace(Item_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Item_Name", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Item_Name", Item_Name);

                if (String.IsNullOrWhiteSpace(Item_Code))
                    sda.SelectCommand.Parameters.AddWithValue("@Item_Code", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);

                sda.SelectCommand.Parameters.AddWithValue("@PageIndex", pageIndex);
                sda.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                sda.SelectCommand.Parameters.AddWithValue("@Option",option);

                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
        public DataTable SearchForViewQuick(string Item_Name, string Item_Code)
        {
            try
            {
                //string query = "SELECT ID,Item_Code,Item_Name FROM Item_Master Where Item_Code LIKE '" + "%" + code + "%'";
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SearchForViewQuick", DataConfig.GetConnectionString());
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (String.IsNullOrWhiteSpace(Item_Name))
                    sda.SelectCommand.Parameters.AddWithValue("@Item_Name", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Item_Name", Item_Name);

                if (String.IsNullOrWhiteSpace(Item_Code))
                    sda.SelectCommand.Parameters.AddWithValue("@Item_Code", DBNull.Value);
                else sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);

                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } */


        //public Boolean ChangeStatus(string itemIDList , int User_ID)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_Item_Master_ChangeStatus", DataConfig.GetConnectionString());
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@itemIDList", itemIDList);
        //        cmd.Parameters.AddWithValue("@User_ID", User_ID);
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();

        //        return true;
        //    }catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public Boolean ChangeStatusConsole(int Item_ID, int Export_Status)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Master_ChangeStatus", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", Item_ID);
                cmd.Parameters.AddWithValue("@Export_Status", Export_Status);
                cmd.Parameters.AddWithValue("@User_ID", 0);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean ChangeStatusForm(int Item_ID, int Export_Status,int User_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Master_ChangeStatus", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", Item_ID);
                cmd.Parameters.AddWithValue("@Export_Status", Export_Status);
                cmd.Parameters.AddWithValue("@User_ID", User_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean ChangeCtrl_ID(string itemIDList)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Master_ChangeCtrl_ID", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@itemIDList", itemIDList);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean ChangeExport_Status(string Item_Code)  //Export_Status=3
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string quary = "UPDATE Item_Master SET Export_Status=3 WHERE Item_Code = @Item_Code";
                SqlCommand cmd = new SqlCommand(quary, connectionString);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_Code", Item_Code);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetListPrice(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //string quary = "SELECT List_Price FROM Item_Master WHERE Item_Code = @Item_Code";
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "List_Price");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["List_Price"].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSalePrice(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //string quary = "SELECT Sale_Price FROM Item_Master WHERE Item_Code = @Item_Code";
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "Sale_Price");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Sale_Price"].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetBrandName(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "Brand_Name");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Brand_Name"].ToString();
                }
                else
                   return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSalesUnit(string Item_Code,string option)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option",option);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][option].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetZettItemDescription(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //string quary = "SELECT Zett_Item_Description FROM Item_Master WHERE Item_Code = @Item_Code";
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "Zett_Item_Description");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Zett_Item_Description"].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetZettSaleDescription(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //string quary = "SELECT Zett_Sale_Description FROM Item_Master WHERE Item_Code = @Item_Code";
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "Zett_Sale_Description");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Zett_Sale_Description"].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetTemplateValue(string tmpprice,int shopID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Master_SelectPrice", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@template_price", tmpprice);
                cmd.Parameters.AddWithValue("@ShopID", shopID);
                cmd.Connection.Open();

                string simpleValue = cmd.ExecuteScalar().ToString();
                cmd.Connection.Close();
                return simpleValue;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetGroupNo()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetGroupNo", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt.Rows[0]["Group_No"].ToString();
                
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteItem(string Item_Code)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_DeleteFromSKS_ByItemCode", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item_Code", Item_Code);
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean IsPost_Available_Date(int Item_ID)
        {
            try
            {
                //return imDL.IsPost_Available_Date(Item_ID);
                //SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //SqlDataAdapter sda = new SqlDataAdapter("SP_Check_Post_Available_Date", connectionString);
                //sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                //sda.SelectCommand.CommandTimeout = 0;
                //sda.SelectCommand.Connection.Open();
                ////sda.Fill(dt);
                //sda.SelectCommand.Connection.Close();
                //int value;
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Check_Post_Available_Date", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", Item_ID);
                cmd.Connection.Open();
                int value =(int) cmd.ExecuteScalar();
                cmd.Connection.Close();
                if (value == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetUnsetDailyDelivery(string ItemCode, int flag)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_SetUnsetDailyDelivery", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Code", ItemCode);
                cmd.Parameters.AddWithValue("@flag", flag);
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BindDailyFlag(string ItemCode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_SelectDailyFlag", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Code", ItemCode);
                DataTable dt = new DataTable();
                cmd.CommandTimeout = 0;
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                sqlData.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeExportStatusToPink(string Item_Code,int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_ChangeExportStatusToPink", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@item_Code", Item_Code);
                cmd.Parameters.AddWithValue("@Option", option);
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectForExportStatusChange(string IDList)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Select_Data_To_Change_Status", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item_ID", IDList);
                DataTable dt = new DataTable();
                cmd.CommandTimeout = 0;
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                sqlData.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectChangeStatusItem(string list, int pageIndex, int pageSize)
        {
            try
            {
                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter("SP_Select_Change_Status_Item", connectionString);
                    sda.SelectCommand.CommandTimeout = 0;
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    if (!String.IsNullOrWhiteSpace(list))
                        sda.SelectCommand.Parameters.AddWithValue("@list", list);
                    else sda.SelectCommand.Parameters.AddWithValue("@list", DBNull.Value);
                    sda.SelectCommand.Parameters.AddWithValue("@PageIndex", pageIndex);
                    sda.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                    sda.SelectCommand.Connection.Open();
                    sda.Fill(dt);
                    sda.SelectCommand.Connection.Close();
                    return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ItemUpdateInventory(string itemcode,int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Update_Inventory", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item_Code",itemcode);
                cmd.Parameters.AddWithValue("@Option", option);
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertItemInventory(string itemcode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Check_Inventory", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item_Code", itemcode);
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BindMonotaro(string tablename)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("Select * From "+tablename, connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CheckRequiredData(int Item_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Check_Monotaro_Data", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_ID", Item_ID);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertItemImportPD(DataTable dt)
        {
            try
            {
                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);

                SqlCommand cmd = new SqlCommand("SP_ItemImport_Product_Directory_XML", connectionString);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean InsertCSV(DataTable dt)
        {
            try
            {              
                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);

                SqlCommand cmd = new SqlCommand("SP_Product_Directory_Import", connectionString);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
        public Boolean UpdateSaleDecCSV(DataTable dt)
        {
            try
            {
                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);

                SqlCommand cmd = new SqlCommand("SP_SaleDecPc_Import", connectionString);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable GetPrices(string itemcode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetPrices", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", itemcode);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
