using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect_Exhibition_TennisClassic_Console
{
    class Program
    {
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static DataTable dtImage = new DataTable();
        static DataTable dtItem = new DataTable();
        static DataTable dtSelect = new DataTable();
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Tennis Data Collect";
                ConsoleWriteLine_Tofile("Tennis Data Collect : " + DateTime.Now);

                string list = SelectExhibitionItemID();
                ConsoleWriteLine_Tofile("1.SelectExhibitionItemID : " + list);

                if (!string.IsNullOrWhiteSpace(list))
                {
                    DataTable dtItemMaster = GetItemData(list, 6);
                    ConsoleWriteLine_Tofile("2.GetItemData : " + DateTime.Now);

                    Export_CSV3 export = new Export_CSV3();
                    DataTable dt1 = export.ModifyTable(dtItemMaster, 6);
                    ConsoleWriteLine_Tofile("3.ChangeTemplate : " + DateTime.Now);

                    SaveLogExhibition(dt1, list, 6);
                    ConsoleWriteLine_Tofile("4.SaveLogExhibition : " + DateTime.Now);
                }
                ChangeFlag(list, 6);
                SaveTennisLogData(list);// Import_Tennis_Item_Code
                ConsoleWriteLine_Tofile("5.ChangeFlag : " + DateTime.Now);
                Exhibition_TennisClassic_Console();
            }

            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "Data Collect Tennis Console" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }

        }
        public static void Exhibition_TennisClassic_Console()
        {
            try
            {
                DataTable dtShopList = GetShopList();
                if (dtShopList != null && dtShopList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtShopList.Rows)
                    {
                        dtImage = SelectLogExhibitionImage(int.Parse(dr["ID"].ToString()));
                        dtItem = SelectLogExhibitionItem(int.Parse(dr["ID"].ToString()), int.Parse(dr["Mall_ID"].ToString()));
                        dtSelect = SelectLogExhibitionSelect(int.Parse(dr["ID"].ToString()), int.Parse(dr["Mall_ID"].ToString()));
                        // dtCategory = SelectLogExhibitionCategory(int.Parse(dr["ID"].ToString()), int.Parse(dr["Mall_ID"].ToString()));
                        Export_CSV4 export = new Export_CSV4();
                        export.TennisFilterSKU(dtImage, dtSelect, dtItem, int.Parse(dr["ID"].ToString()));
                    }
                }


            }
            catch (Exception ex)
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "Data Collect TennisClassic Console" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
        static DataTable SelectLogExhibitionItem(int shop_id, int mall_id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectByShop_Tennis", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID", mall_id);
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
        static DataTable SelectLogExhibitionSelect(int shop_id, int mall_id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Select_SelectByShop_Tennis", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID", mall_id);
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

        static DataTable SelectLogExhibitionImage(int shop_id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetImageList_Select_By_Mall", connectionstring);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
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

        static DataTable GetShopList()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                string quary = "SELECT ID,Mall_ID FROM Shop WHERE Mall_ID = 7 and ID=6";
                SqlDataAdapter sda = new SqlDataAdapter(quary, connectionstring);
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

        static void SaveTennisLogData(string list)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_Update_Import_Shop", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@List", list);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static void ChangeFlag(string list, int shop_ID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_ChangeFlagByShop", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@List", list);
                cmd.Parameters.AddWithValue("@Shop_ID", shop_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static void SaveLogExhibition(DataTable dt, string list, int shop_id)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();
                    SqlConnection conn = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_Insert_TennisClassic");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Connection = conn;
                    result = result.Replace("&#", "$CapitalSports$");
                    cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
                    cmd.Parameters.AddWithValue("@strString", list);
                    cmd.Parameters.AddWithValue("@Shop_ID", shop_id);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveLogWarehouseItem(DataTable dtWarehouse)
        {
            try
            {
                dtWarehouse.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dtWarehouse.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_SaveLogWarehouseItem_Tennis", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@xml", SqlDbType.Xml).Value = result;

                cmd.Parameters.AddWithValue("@shopID", "29");
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static DataTable GetItemData(string list, int shop_id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_List_CollectData_New_Tennis", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@ItemID", list);
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_id);
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
        static string SelectExhibitionItemID()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_List_SelectItem_IDList_New", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", 6);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt.Rows[0]["Item_ID"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "DataCollectTennis_ConsoleWriteLine29.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
    }
}
