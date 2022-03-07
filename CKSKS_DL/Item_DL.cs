/* 
Created By              : Aye Aye Mon
Created Date          : 19/06/2014
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
    public class Item_DL
    {
        public Item_DL() { }

        public int SaveEdit(Item_Entity item, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ItemID", item.ID);
                cmd.Parameters.AddWithValue("@Item_Code", item.Item_Code);
                cmd.Parameters.AddWithValue("@Sale_Code", item.Sale_Code);
                cmd.Parameters.AddWithValue("@JAN_Code", item.JAN_Code);
                cmd.Parameters.AddWithValue("@Color_Code", item.Color_Code);
                cmd.Parameters.AddWithValue("@Color_Name", item.Color_Name);
                cmd.Parameters.AddWithValue("@Size_Code", item.Size_Code);
                cmd.Parameters.AddWithValue("@Size_Name", item.Size_Name);
                cmd.Parameters.AddWithValue("@Original_Quantity", item.Original_Quantity);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@Indicated_Price", item.Indicated_Price);
                cmd.Parameters.AddWithValue("@Sale_Price", item.Sale_Price);
                cmd.Parameters.AddWithValue("@CSV_FileName", item.CSV_FileName);
                cmd.Parameters.AddWithValue("@Item_Description", item.Item_Description);
                cmd.Parameters.AddWithValue("@IsUploaded", item.IsUploaded);
                cmd.Parameters.AddWithValue("@Created_Date", item.Created_Date);
                cmd.Parameters.AddWithValue("@Updated_Date", item.Updated_Date);
                cmd.Parameters.AddWithValue("@Updated_By", item.Updated_By);
                cmd.Parameters.AddWithValue("@Option", option);
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

        public Item_Entity SelectByID(int id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string quary = "SELECT * FROM Item WITH (NOLOCK) WHERE ID=@ID";
                SqlDataAdapter sda = new SqlDataAdapter(quary, connectionString);
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ID", id);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                Item_Entity item = new Item_Entity();
                if (dt != null && dt.Rows.Count >= 0)
                {
                    item.ID = int.Parse(dt.Rows[0]["ID"].ToString());
                    item.Item_Code = dt.Rows[0]["Item_Code"].ToString();
                    item.Sale_Code = dt.Rows[0]["Sale_Code"].ToString();
                    item.JAN_Code = dt.Rows[0]["JAN_Code"].ToString();
                    item.Color_Code = dt.Rows[0]["Color_Code"].ToString();
                    item.Color_Name = dt.Rows[0]["Color_Name"].ToString();
                    item.Size_Code = dt.Rows[0]["Size_Code"].ToString();
                    item.Size_Name = dt.Rows[0]["Size_Name"].ToString();
                    item.Original_Quantity = int.Parse(dt.Rows[0]["Original_Quantity"].ToString());
                    item.Quantity = int.Parse(dt.Rows[0]["Quantity"].ToString());
                    item.Indicated_Price = int.Parse(dt.Rows[0]["Indicated_Price"].ToString());
                    item.Sale_Price = int.Parse(dt.Rows[0]["Sale_Price"].ToString());
                    item.CSV_FileName = dt.Rows[0]["CSV_FileName"].ToString();
                    item.Item_Description = dt.Rows[0]["Item_Description"].ToString();
                    item.IsUploaded = int.Parse(dt.Rows[0]["IsUploaded"].ToString());
                    item.Created_Date = Convert.ToDateTime(dt.Rows[0]["Created_Date"].ToString());
                    item.Updated_Date = Convert.ToDateTime(dt.Rows[0]["Updated_Date"].ToString());
                    item.Updated_By = int.Parse(dt.Rows[0]["Updated_By"].ToString());
                }
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable Search(string itemcode, string salecode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmdSelect = new SqlCommand("SP_Item_Search", connectionString);
                da.SelectCommand = cmdSelect;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;

                cmdSelect.Parameters.AddWithValue("@itemcode", itemcode);
                cmdSelect.Parameters.AddWithValue("@salecode", salecode);
                DataTable dt = new DataTable();

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

        public DataTable SelectAll()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("Select * from Item_Master WITH (NOLOCK)", connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandTimeout = 0;

                DataTable dt = new DataTable();
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex) { throw ex; }

        }

        public DataTable SelectAllItem()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //string sqlQuery = "SELECT ID,Item_Code,Color_Name,Size_Name,Quantity,Updated_Date FROM Item ";
                string sqlQuery = "SP_Item_Master_Select_All";
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;

                DataTable dt = new DataTable();
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex) { throw ex; }

        }

        public void UpdateQuantity(string id, string quantity, string jisha_quantity)
        {
            SqlConnection sqlConnection = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter Adaptor = new SqlDataAdapter();
            //Updated by aam , add Order_Flag = 1 for Inventory Upload
            SqlCommand cmd = new SqlCommand("SP_SKS_Quantity_Update", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            if (!String.IsNullOrWhiteSpace(quantity))
                cmd.Parameters.AddWithValue("@Quantity", quantity);
            else
                cmd.Parameters.AddWithValue("@Quantity", 999);
            cmd.Parameters.AddWithValue("@jisha_quantity", jisha_quantity);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void UpdateItem(DataTable dt)
        {
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SqlConnection sqlConnection = new SqlConnection(DataConfig.connectionString);
                    SqlDataAdapter Adaptor = new SqlDataAdapter();
                    //Updated by aam , add Order_Flag = 1 for Inventory Upload
                    SqlCommand cmd = new SqlCommand("UPDATE Item SET Quantity= @Quantity , Order_Flag = 1,Stock_Change_Flag=1 WHERE ID = @ID", sqlConnection);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@Quantity", int.Parse(dr["Quantity"].ToString()));
                    cmd.Parameters.AddWithValue("@ID", int.Parse(dr["ID"].ToString()));
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectItemMasterData(String itemcode)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                String query = "Select Item_Name,List_Price,Sale_Price,Cost,Competition_Name,Year,Season From Item_Master WITH (NOLOCK) Where Item_Code='" + itemcode + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable SearchItem(string ItemCode, string proname, string procode, string catInfo, string bname, string compname, string year, string season, string jan, int pageindex, int pagesize, int option, Boolean isSearch)
        {
            try
            {
                if (isSearch)
                {

                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    //string sqlQuery = String.Format("SELECT * from Item where Item_Code LIKE '%{0}%'", ItemCode);
                    string sqlQuery = String.Empty;

                    if (option == 1)
                        sqlQuery = String.Format("SP_Item_StockView_LikeSearch");

                    else if (option == 2)
                        sqlQuery = String.Format("SP_Item_StockView_EqualSearch");

                    SqlDataAdapter da = new SqlDataAdapter(sqlQuery, connectionString);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.CommandTimeout = 0;

                    if (!string.IsNullOrWhiteSpace(ItemCode))
                        da.SelectCommand.Parameters.AddWithValue("@itemcode", ItemCode);
                    else
                        da.SelectCommand.Parameters.AddWithValue("@itemcode", DBNull.Value);
                    if (!string.IsNullOrWhiteSpace(proname))
                        da.SelectCommand.Parameters.AddWithValue("@proname", proname);
                    else
                        da.SelectCommand.Parameters.AddWithValue("@proname", DBNull.Value);
                    if (!string.IsNullOrWhiteSpace(procode))
                        da.SelectCommand.Parameters.AddWithValue("@procode", procode);
                    else
                        da.SelectCommand.Parameters.AddWithValue("@procode", DBNull.Value);
                    if (!string.IsNullOrWhiteSpace(catInfo))
                        da.SelectCommand.Parameters.AddWithValue("@catInfo", catInfo);
                    else
                        da.SelectCommand.Parameters.AddWithValue("@catInfo", DBNull.Value);
                    if (!string.IsNullOrWhiteSpace(bname))
                        da.SelectCommand.Parameters.AddWithValue("@brandname", bname);
                    else
                        da.SelectCommand.Parameters.AddWithValue("@brandname", DBNull.Value);
                    if (!string.IsNullOrWhiteSpace(compname))
                        da.SelectCommand.Parameters.AddWithValue("@competname", compname);
                    else
                        da.SelectCommand.Parameters.AddWithValue("@competname", DBNull.Value);
                    if (!string.IsNullOrWhiteSpace(year))
                        da.SelectCommand.Parameters.AddWithValue("@year", year);
                    else
                        da.SelectCommand.Parameters.AddWithValue("@year", DBNull.Value);
                    if (!string.IsNullOrWhiteSpace(season))
                        da.SelectCommand.Parameters.AddWithValue("@season", season);
                    else
                        da.SelectCommand.Parameters.AddWithValue("@season", DBNull.Value);
                    if (!string.IsNullOrWhiteSpace(jan))
                        da.SelectCommand.Parameters.AddWithValue("@jancode", jan);
                    else
                        da.SelectCommand.Parameters.AddWithValue("@jancode", DBNull.Value);
                    da.SelectCommand.Parameters.AddWithValue("@PageIndex", pageindex);
                    da.SelectCommand.Parameters.AddWithValue("@PageSize", pagesize);
                    da.SelectCommand.Parameters.AddWithValue("@Option", option);
                    DataTable dt = new DataTable();
                    da.SelectCommand.Connection.Open();
                    da.Fill(dt);
                    da.SelectCommand.Connection.Close();
                    return dt;
                }
                else
                {
                    SqlConnection con = new SqlConnection(DataConfig.connectionString);
                    SqlCommand cmd = new SqlCommand("SP_Item_StockView_Select", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageIndex", pageindex);
                    cmd.Parameters.AddWithValue("@PageSize", pagesize);
                    cmd.Parameters.AddWithValue("@Option", option);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    cmd.Connection.Open();
                    da.Fill(dt);
                    cmd.Connection.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ddlPerson_in_Charge()
        {
            try
            {
                SqlConnection sqlCnn = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("Select * From  [User] WITH (NOLOCK)", sqlCnn);
                cmd.CommandTimeout = 0;
                sqlCnn.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                sqlCnn.Close();

                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ddlShop_Status()
        {
            try
            {
                SqlConnection sqlCnn = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("Select * From  Shop", sqlCnn);
                cmd.CommandTimeout = 0;
                sqlCnn.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                sqlCnn.Close();

                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetData(String query)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable SelectSKU(string ItemCode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_SelectSKU", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@ItemCode", ItemCode);
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

        public DataTable SelectRelatedItem(string ItemCode1, string ItemCode2, string ItemCode3, string ItemCode4, string ItemCode5)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Related_ItemPreview", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code1", ItemCode1);
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code2", ItemCode2);
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code3", ItemCode3);
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code4", ItemCode4);
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code5", ItemCode5);
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

        public DataTable SelectSKUSize(string ItemCode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_SelectSKUSize", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@ItemCode", ItemCode);
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

        public DataTable SelectSKUColor(string ItemCode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_SelectSKUColor", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@ItemCode", ItemCode);
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

        public Boolean DeleteItems(string itemlist)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand("SKU_Item_Delete", connectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@ID", itemlist);
            try
            {
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

        public DataTable GetSKUHeader(string Item_Code)
        {
            try
            {

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SELECT DISTINCT Size_Code,Size_Name FROM Item WITH (NOLOCK) WHERE Item.Ctrl_ID='n' AND Item_Code='" + Item_Code + "'";
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

        public DataTable GetSKUSide(string Item_Code)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SELECT DISTINCT Color_Code,Color_Name FROM Item WITH (NOLOCK) WHERE Item.Ctrl_ID='n' AND Item_Code='" + Item_Code + "'";
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

        public DataTable GetSKUQuantity(string Item_Code)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SELECT DISTINCT Item_Code,Quantity,Size_Code,Size_Name,Color_Code,Color_Name FROM Item WITH (NOLOCK) WHERE Item.Ctrl_ID='n' AND Item_Code='" + Item_Code + "' and Size_Code!='-' and Color_Code!='-' ORDER BY Size_Code";
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.CommandType = CommandType.Text;

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

        public string GetSKUSizeName(string Item_Code)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //string query = "SELECT DISTINCT Item_Code,Quantity,Size_Code,Size_Name,Color_Code,Color_Name FROM Item WHERE Item.Ctrl_ID='n' AND Item_Code='" + Item_Code + "' ORDER BY Size_Code";
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetSKUSizeColorName", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "size");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Size_Name"].ToString().TrimEnd(',');
                }
                else return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSKUColorName(string Item_Code)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //string query = "SELECT DISTINCT Item_Code,Quantity,Size_Code,Size_Name,Color_Code,Color_Name FROM Item WHERE Item.Ctrl_ID='n' AND Item_Code='" + Item_Code + "' ORDER BY Size_Code";
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetSKUSizeColorName", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "color");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Color_Name"].ToString().TrimEnd(',');
                }
                else return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BindColor(string category_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetSKUSizeColorBrand", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@RakutenCategoryID", category_id);
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable BindgdvDetailSKU(string category_id, string Item_Code)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetSKUDetailFromItem", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@RakutenCategoryID", category_id);
                sda.SelectCommand.Parameters.AddWithValue("@ItemCode", Item_Code);
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateSKU(string id, string sku, int option)
        {
            try
            {
                SqlConnection sqlcon = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_SKU_Update", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@sku", sku);
                cmd.Parameters.AddWithValue("@option", option);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSKU(string id, string sku, int option)
        {
            try
            {
                SqlConnection sqlcon = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_SKU_Delete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@sku", sku);
                cmd.Parameters.AddWithValue("@option", option);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectItemData(string Item_Code)
        {
            try
            {
                SqlConnection sqlcon = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_SKU_SelectItemData", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!String.IsNullOrWhiteSpace(Item_Code))
                {
                    cmd.Parameters.AddWithValue("@Item_Code", Item_Code);
                }
                else
                    cmd.Parameters.AddWithValue("@Item_Code", DBNull.Value);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                sqlData.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertUpdateSKU(DataTable dt, string Item_Code)
        {
            SqlConnection connection = new SqlConnection(DataConfig.connectionString);
            if (!string.IsNullOrWhiteSpace(Item_Code))
            {
                SqlCommand sqlcmd = new SqlCommand("SP_DeleteItem_ForSKUInsert", connection);
                try
                {
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@itemCode", Item_Code);
                    sqlcmd.Connection.Open();
                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.Connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlCommand cmd = new SqlCommand("SP_InsertUpdateSKU_Info", connection);
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    //cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                    cmd.Parameters.AddWithValue("@itemCode", dt.Rows[i]["Item_Code"].ToString());
                    cmd.Parameters.AddWithValue("@sizeName", dt.Rows[i]["Size_Name"].ToString());
                    cmd.Parameters.AddWithValue("@colorName", dt.Rows[i]["Color_Name"].ToString());
                    cmd.Parameters.AddWithValue("@sizeCode", dt.Rows[i]["Size_Code"].ToString());
                    cmd.Parameters.AddWithValue("@colorCode", dt.Rows[i]["Color_Code"].ToString());
                    cmd.Parameters.AddWithValue("@janCode", dt.Rows[i]["JAN_Code"].ToString());
                    cmd.Parameters.AddWithValue("@inventoryFlag", dt.Rows[i]["Inventory_Flag"].ToString());
                    cmd.Parameters.AddWithValue("@qty", dt.Rows[i]["Quantity"].ToString());
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
            }
        }

        public void DeleteSKUOption(string Item_Code)
        {
            try
            {
                SqlConnection sqlcon = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_DeleteSKUOption", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item_Code", Item_Code);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public DataSet BindItem(string brandname, string itemcode, int pagesize, int pageindex)
        //{
        //    try
        //    {
        //        SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
        //        // DataTable dt = new DataTable();
        //        SqlDataAdapter sda = new SqlDataAdapter("SP_SelectItemSettingNo", connectionString);
        //        sda.SelectCommand.CommandTimeout = 0;
        //        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        if (!String.IsNullOrWhiteSpace(itemcode))
        //            sda.SelectCommand.Parameters.AddWithValue("@ItemCode", itemcode);
        //        else
        //            sda.SelectCommand.Parameters.AddWithValue("@ItemCode", DBNull.Value);
        //        if (!String.IsNullOrWhiteSpace(brandname))
        //            sda.SelectCommand.Parameters.AddWithValue("@Brandname", brandname);
        //        else
        //            sda.SelectCommand.Parameters.AddWithValue("@Brandname", DBNull.Value);
        //        sda.SelectCommand.Parameters.AddWithValue("@pagesize", pagesize);
        //        sda.SelectCommand.Parameters.AddWithValue("@pageindex", pageindex);
        //        sda.SelectCommand.Parameters.AddWithValue("@option", "2");
        //        sda.SelectCommand.Connection.Open();
        //        DataSet ds = new DataSet();
        //        sda.Fill(ds);
        //        sda.SelectCommand.Connection.Close();
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public DataSet BindItem(string itemcode,string itemname,string brandname, int pagesize, int pageindex,string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                // DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_SelectItemSettingNo", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (!String.IsNullOrWhiteSpace(itemcode))
                    sda.SelectCommand.Parameters.AddWithValue("@ItemCode", itemcode);
                else
                    sda.SelectCommand.Parameters.AddWithValue("@ItemCode", DBNull.Value);
                if (!String.IsNullOrWhiteSpace(itemname))
                    sda.SelectCommand.Parameters.AddWithValue("@ItemName", itemname);
                else
                    sda.SelectCommand.Parameters.AddWithValue("@ItemName", DBNull.Value);
                if (!String.IsNullOrWhiteSpace(brandname))
                    sda.SelectCommand.Parameters.AddWithValue("@Brandname", brandname);
                else
                    sda.SelectCommand.Parameters.AddWithValue("@Brandname", DBNull.Value);
                sda.SelectCommand.Parameters.AddWithValue("@pagesize", pagesize);
                sda.SelectCommand.Parameters.AddWithValue("@pageindex", pageindex);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
                sda.SelectCommand.Connection.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                sda.SelectCommand.Connection.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet BindItemName(string itemname)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                // DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_SelectItemSettingNo1", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (!String.IsNullOrWhiteSpace(itemname))
                    sda.SelectCommand.Parameters.AddWithValue("@ItemName", itemname);
                else
                    sda.SelectCommand.Parameters.AddWithValue("@ItemName", DBNull.Value);
                sda.SelectCommand.Connection.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                sda.SelectCommand.Connection.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet BindBrandName(string brandname)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                // DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_SelectItemSettingNo2", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (!String.IsNullOrWhiteSpace(brandname))
                    sda.SelectCommand.Parameters.AddWithValue("@Brandname ", brandname);
                else
                    sda.SelectCommand.Parameters.AddWithValue("@Brandname ", DBNull.Value);
                sda.SelectCommand.Connection.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                sda.SelectCommand.Connection.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet BindItemExport(string itemcode,int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                // DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_SelectItemSettingNo_forExport", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (!String.IsNullOrWhiteSpace(itemcode))
                    sda.SelectCommand.Parameters.AddWithValue("@ItemCode", itemcode);
                else
                    sda.SelectCommand.Parameters.AddWithValue("@ItemCode", DBNull.Value);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
                sda.SelectCommand.Connection.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                sda.SelectCommand.Connection.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //public DataSet BindPageloadItem(string brandname, string itemcode, int pagesize, int pageindex, string option)
        //{
        //    try
        //    {
        //        SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
        //        // DataTable dt = new DataTable();
        //        SqlDataAdapter sda = new SqlDataAdapter("SP_SelectItemSettingNo", connectionString);
        //        sda.SelectCommand.CommandTimeout = 0;
        //        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        sda.SelectCommand.Parameters.AddWithValue("@pagesize", pagesize);
        //        sda.SelectCommand.Parameters.AddWithValue("@pageindex", pageindex);
        //        sda.SelectCommand.Parameters.AddWithValue("@option", option);
        //        sda.SelectCommand.Connection.Open();
        //        DataSet ds = new DataSet();
        //        sda.Fill(ds);
        //        sda.SelectCommand.Connection.Close();
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        public DataSet BindAllItem(int pagesize, int pageindex)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                // DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_SelectItemSettingNoAll", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@pagesize", pagesize);
                sda.SelectCommand.Parameters.AddWithValue("@pageindex", pageindex);
                sda.SelectCommand.Connection.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                sda.SelectCommand.Connection.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet BindPageloadItem(string itemcode,int pagesize, int pageindex, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                // DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_SelectItemSettingNo", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@pagesize", pagesize);
                sda.SelectCommand.Parameters.AddWithValue("@pageindex", pageindex);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
                sda.SelectCommand.Connection.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                sda.SelectCommand.Connection.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable DDLRShipping()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_SelectRSetting", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
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

        public DataTable DDLYShipping()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_SelectYSetting", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
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
        public void SKU_Save(String xml)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_SKUSaveForDelivery";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ShippingNo_Select(string sno,int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Delivery_SettingNo_ShippingNo_Select", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@ShippingNo", sno);
                sda.SelectCommand.Parameters.Add("@option", option);
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

        public void UpdateStockFlag(int ID,int Flag)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update Item set Stock_Change_Flag=" + Flag + "where ID=" + ID;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
}

