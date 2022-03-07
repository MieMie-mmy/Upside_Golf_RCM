using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_DL
{
    public class Jisha_Item_Master_DL
    {
        public Jisha_Item_Master_DL() { }

        public DataTable SelectAll()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connection = JsDataConfig.GetConnectionString();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT *,substring( SUBSTRING(Image_Name,charindex(Image_Name,'1'),99),0,charindex(',',SUBSTRING(Image_Name,charindex(Image_Name,'1'),99))) as Image_Name1 FROM Jisha_Item_Master", connection);
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

        public DataTable SelectByItemID(int Item_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connection = JsDataConfig.GetConnectionString();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT *,substring( SUBSTRING(Image_Name,charindex(Image_Name,'1'),99),0,charindex(',',SUBSTRING(Image_Name,charindex(Image_Name,'1'),99))) as Image_Name1 FROM Jisha_Item_Master WHERE ID=" + Item_ID, connection);
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

        //Jisha_Item Table
        public DataTable GetSKUHeader(string Item_Code)
        {
            try
            {
                SqlConnection connection = JsDataConfig.GetConnectionString();
                string query = "SELECT DISTINCT Size_Code,Size_Name FROM Jisha_Item WHERE Item_Code='" + Item_Code + "'";
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(query, connection);

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
                SqlConnection connection = JsDataConfig.GetConnectionString();
                string query = "SELECT DISTINCT Item_Code,Quantity,Size_Code,Size_Name,Color_Code,Color_Name FROM Jisha_Item WHERE Item_Code='" + Item_Code + "'";
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(query, connection);


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

        public DataTable GetSKU(string Item_Code)
        {
            try
            {
                SqlConnection connection = JsDataConfig.GetConnectionString();
                string query = "SELECT DISTINCT Item_Code,Quantity,Size_Code,Color_Code,Color_Name FROM Jisha_Item WHERE Item_Code='" + Item_Code + "'";
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(query, connection);

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

        //Jisha_Item_Option Table
        public DataTable GetJishaItemOption(string Item_Code)
        {
            try
            {
                SqlConnection connection = JsDataConfig.GetConnectionString();
                string query = "SELECT * FROM Jisha_Item_Option WHERE Item_Code='" + Item_Code + "'";
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(query, connection);

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

        //Jisha_OrderDetail Table
        public void Insert(DataTable dt)
        {
            try
            {
                SqlConnection con = JsDataConfig.GetConnectionString();
                SqlCommand cmd = new SqlCommand("SP_Jisha_OrderDetail_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Parameters.Add("@Order_ID", SqlDbType.NVarChar, 200, "Order_ID");
                cmd.Parameters.Add("@Item_Code", SqlDbType.NVarChar, 200, "Item_Code");
                cmd.Parameters.Add("@Item_Name", SqlDbType.NVarChar, 200, "Item_Name");
                cmd.Parameters.Add("@Size_Code", SqlDbType.NVarChar, 200, "Size_Code");
                cmd.Parameters.Add("@Color_Code", SqlDbType.NVarChar, 200, "Color_Code");
                cmd.Parameters.Add("@Quantity", SqlDbType.Int, 200, "Quantity");
                cmd.Parameters.Add("@Price", SqlDbType.Int, 200, "Price");
                sda.InsertCommand = cmd;
                sda.UpdateCommand = cmd;
                cmd.Connection.Open();
                sda.Update(dt);
                cmd.Connection.Close();
                //return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Jisha_Item_Category Table
        public DataTable SelectItemByCategory(string Category_No)
        {
            try
            {
                SqlConnection connection = JsDataConfig.GetConnectionString();
                string query = "SELECT Jisha_Item_Master.*,substring( SUBSTRING(Jisha_Item_Master.Image_Name,charindex(Jisha_Item_Master.Image_Name,'1'),99),0,charindex(',',SUBSTRING(Jisha_Item_Master.Image_Name,charindex(Jisha_Item_Master.Image_Name,'1'),99))) as Image_Name1 "
                    + "FROM Category INNER JOIN Jisha_Item_Category ON Category.Category_ID = Jisha_Item_Category.Category_No INNER JOIN Jisha_Item_Master ON Jisha_Item_Category.Item_Code = Jisha_Item_Master.Item_Code "
                    + String.Format("WHERE Jisha_Item_Category.Category_No = '{0}'",Category_No);
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(query, connection);
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

        public DataTable SelectByCategoryPath(string Item_Code)
        {
            try
            {
                SqlConnection connection = JsDataConfig.GetConnectionString();
                DataTable dt = new DataTable();
                //string quary = String.Format("SELECT Category.*,Jisha_Item_Category.* FROM Category INNER JOIN Jisha_Item_Category ON Category.Category_ID = Jisha_Item_Category.Category_No WHERE Item_Code='{0}'", Item_Code);
                string quary = String.Format("SELECT * FROM Jisha_Item_Category WHERE Item_Code='{0}'", Item_Code);
                SqlDataAdapter sda = new SqlDataAdapter(quary,connection);
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
    }
}
