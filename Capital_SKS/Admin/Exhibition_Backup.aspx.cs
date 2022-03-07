using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace ORS_RCM.Admin
{
    public partial class Exhibition_Backup : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindShop();
                //btnRestore.Visible = false;
            }
        }

        public void BindShop()
        {
            SqlConnection connectionString = con;
            SqlDataAdapter da = new SqlDataAdapter("Select ID,Shop_Name From Shop", connectionString);
            da.SelectCommand.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            da.SelectCommand.Connection.Open();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                {
                    chlShop.DataSource = dt;
                    chlShop.DataTextField = "Shop_Name";
                    chlShop.DataValueField = "ID";
                    chlShop.DataBind();
                }
          
        }
         
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //btnRestore.Visible = true;
            string ItemCode = txtItemCode.Text;
            String[] str1 = (ItemCode.Split(','));
            for (int i = 0; i < str1.Length; i++)
            {
                string code = str1[i].Trim();
                DataTable dt1 = SelectID(code);     //select ID from Item_Master
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    int itemid = Convert.ToInt32(dt1.Rows[0]["ID"].ToString());
                    DataTable dt = SelectData(itemid);
                    Insert(dt, 0);          //Insert into Temp_Item_Shop
                    Delete(itemid);         //Delete from Item_Shop
                    foreach (ListItem item in chlShop.Items)
                    {
                        if (item.Selected)
                        {
                            int shopid = Convert.ToInt32(item.Value);
                            Update(itemid, shopid);     //Insert into Item_Shop
                            //DataTable dt1 = SelectData1(code, shopid);
                            //Update(dt1);

                        }
                    }
                    GlobalUI.MessageBox("Backup Successfully");
                }
                else
                {
                    GlobalUI.MessageBox("Please Enter Correct Item Code!!!");
                }
            }
        }

        public void Insert(DataTable dt, int option)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    SqlDataAdapter da = new SqlDataAdapter("Insert_Item_Shop_Data", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Connection.Open();
                    da.SelectCommand.Parameters.AddWithValue("@id", dt.Rows[k]["Item_ID"]);
                    da.SelectCommand.Parameters.AddWithValue("@code", dt.Rows[k]["Item_Code"]);
                    da.SelectCommand.Parameters.AddWithValue("@sid", dt.Rows[k]["Shop_ID"]);
                    da.SelectCommand.Parameters.AddWithValue("@ctrl", dt.Rows[k]["Ctrl_ID"]);
                    da.SelectCommand.Parameters.AddWithValue("@status", dt.Rows[k]["Export_Status"]);
                    da.SelectCommand.Parameters.AddWithValue("@estatus", dt.Rows[k]["Exhibition_Status"]);
                    da.SelectCommand.Parameters.AddWithValue("@api", dt.Rows[k]["API_Check"]);
                    da.SelectCommand.Parameters.AddWithValue("@bcheck", dt.Rows[k]["Batch_Check"]);
                    da.SelectCommand.Parameters.AddWithValue("@echeck", dt.Rows[k]["Error_Check"]);
                    da.SelectCommand.Parameters.AddWithValue("@csv", dt.Rows[k]["CSV_FileName"]);
                    da.SelectCommand.Parameters.AddWithValue("@import", dt.Rows[k]["CSV_CreatedDate"]);
                    da.SelectCommand.Parameters.AddWithValue("@export", dt.Rows[k]["CSV_ExportedDate"]);
                    da.SelectCommand.Parameters.AddWithValue("@option", 0);
                    da.SelectCommand.ExecuteNonQuery();
                    da.SelectCommand.Connection.Close();
                }
            }
        }

        public void InsertItemShop(DataTable dt, int option)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    SqlDataAdapter da = new SqlDataAdapter("Insert_Item_Shop_Data", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Connection.Open();
                    da.SelectCommand.Parameters.AddWithValue("@id", dt.Rows[k]["Item_ID"]);
                    da.SelectCommand.Parameters.AddWithValue("@code", dt.Rows[k]["Item_Code"]);
                    da.SelectCommand.Parameters.AddWithValue("@sid", dt.Rows[k]["Shop_ID"]);
                    da.SelectCommand.Parameters.AddWithValue("@ctrl", dt.Rows[k]["Ctrl_ID"]);
                    da.SelectCommand.Parameters.AddWithValue("@status", dt.Rows[k]["Export_Status"]);
                    da.SelectCommand.Parameters.AddWithValue("@estatus", dt.Rows[k]["Exhibition_Status"]);
                    da.SelectCommand.Parameters.AddWithValue("@api", dt.Rows[k]["API_Check"]);
                    da.SelectCommand.Parameters.AddWithValue("@bcheck", dt.Rows[k]["Batch_Check"]);
                    da.SelectCommand.Parameters.AddWithValue("@echeck", dt.Rows[k]["Error_Check"]);
                    da.SelectCommand.Parameters.AddWithValue("@csv", dt.Rows[k]["CSV_FileName"]);
                    da.SelectCommand.Parameters.AddWithValue("@import", dt.Rows[k]["CSV_CreatedDate"]);
                    da.SelectCommand.Parameters.AddWithValue("@export", dt.Rows[k]["CSV_ExportedDate"]);
                    da.SelectCommand.Parameters.AddWithValue("@option", 1);
                    da.SelectCommand.ExecuteNonQuery();
                    da.SelectCommand.Connection.Close();
                }
            }
        }

        public void Update(int itemid, int shopid)
        {
            SqlDataAdapter da = new SqlDataAdapter("INSERT INTO Item_Shop (Item_ID,Shop_ID) VALUES (@id,@sid)", con);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Connection.Open();
            da.SelectCommand.Parameters.AddWithValue("@id", itemid);
            da.SelectCommand.Parameters.AddWithValue("@sid", shopid);
            da.SelectCommand.ExecuteNonQuery();
            da.SelectCommand.Connection.Close();
        }

        //public int SelectItemID(string code)
        //{
        //    SqlConnection connectionString = con;
        //    SqlDataAdapter da = new SqlDataAdapter("Select ID From Item_Master where Item_Code=@code", connectionString);
        //    da.SelectCommand.CommandType = CommandType.Text;
        //    da.SelectCommand.Connection.Open();
        //    da.SelectCommand.Parameters.AddWithValue("@code", code);
        //    int id = (int)da.SelectCommand.ExecuteScalar();
        //    da.SelectCommand.Connection.Close();
        //    return id;
        //}

        public DataTable SelectID(string code)
        {
            DataTable dt=new DataTable();
            SqlConnection connectionString = con;
            SqlDataAdapter da = new SqlDataAdapter("Select ID From Item_Master where Item_Code=@code", connectionString);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Connection.Open();
            da.SelectCommand.Parameters.AddWithValue("@code", code);
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            return dt;
        }

        public void Delete(int itemid)
        {
            SqlDataAdapter da = new SqlDataAdapter("Delete From Item_Shop where Item_ID=@id", con);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Connection.Open();
            da.SelectCommand.Parameters.AddWithValue("@id", itemid);
            da.SelectCommand.ExecuteNonQuery();
            da.SelectCommand.Connection.Close();
        }

        public DataTable SelectData(int itemid)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Item_Shop where Item_ID=@id", con);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Connection.Open();
            da.SelectCommand.Parameters.AddWithValue("@id", itemid);
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            return dt;
        }

        public DataTable SelectItemShopData(int itemid)
        {
            DataTable dt = new DataTable();
            SqlConnection connectionString = con;
            SqlDataAdapter da = new SqlDataAdapter("Select * From Temp_Item_Shop where Item_ID=@id", connectionString);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Connection.Open();
            da.SelectCommand.Parameters.AddWithValue("@id", itemid);
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            return dt;
        }

        public DataTable SelectItemShopData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Temp_Item_Shop", con);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Connection.Open();
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            return dt;
        }

        public DataTable SelectData1(string code, int shopid)
        {
            DataTable dt = new DataTable();
            SqlConnection connectionString = con;
            SqlDataAdapter da = new SqlDataAdapter("Select * From Temp_Item_Shop where Item_Code=@code and Shop_ID=@sid", connectionString);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Connection.Open();
            da.SelectCommand.Parameters.AddWithValue("@code", code);
            da.SelectCommand.Parameters.AddWithValue("@sid", shopid);
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            return dt;
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            Delete();
            DataTable dt = SelectItemShopData();
            InsertItemShop(dt, 1);
            TruncateTempItemShop();
            GlobalUI.MessageBox("Restore Successfully");
        }

        public void Delete()
        {
            SqlDataAdapter da = new SqlDataAdapter("Delete From Item_Shop WHERE Item_ID IN (SELECT Distinct Item_ID From Temp_Item_Shop)", con);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Connection.Open();
            da.SelectCommand.ExecuteNonQuery();
            da.SelectCommand.Connection.Close();
        }

        public void TruncateTempItemShop()
        {
            SqlCommand cmd = new SqlCommand("TRUNCATE TABLE Temp_Item_Shop", con);
            cmd.CommandType = CommandType.Text;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        //public void DeleteItemShop(string code)
        //{
        //    SqlConnection connectionString = con;
        //    SqlDataAdapter da = new SqlDataAdapter("Delete From Item_Shop where Item_Code=@code", connectionString);
        //    da.SelectCommand.CommandType = CommandType.Text;
        //    da.SelectCommand.Connection.Open();
        //    da.SelectCommand.Parameters.AddWithValue("@code", code);
        //    da.SelectCommand.ExecuteNonQuery();
        //    da.SelectCommand.Connection.Close();
        //}
    }
}