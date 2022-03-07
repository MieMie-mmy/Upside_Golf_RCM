using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;


namespace ORS_RCM_DL
{
    public class Promotion_Delivery_DL
    {
        public Promotion_Delivery_DL() { }

        public DataTable SelectAll(Promotion_Delivery_Entity pde, int option, int pindex, int psize)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SP_Promotion_Delivery_LikeSearch";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;

                if(!String.IsNullOrWhiteSpace(pde.Itemcode))
                    da.SelectCommand.Parameters.AddWithValue("@itemcode", pde.Itemcode);
                else 
                    da.SelectCommand.Parameters.AddWithValue("@itemcode", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(pde.Itemname))
                    da.SelectCommand.Parameters.AddWithValue("@itemname",pde.Itemname);
                else
                    da.SelectCommand.Parameters.AddWithValue("@itemname", DBNull.Value);

                if(!String.IsNullOrWhiteSpace(pde.Shopnmae))
                    da.SelectCommand.Parameters.AddWithValue("@shopname",pde.Shopnmae);
                else
                    da.SelectCommand.Parameters.AddWithValue("@shopname", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(pde.Brandname))
                    da.SelectCommand.Parameters.AddWithValue("@brandname", pde.Brandname);
                else
                    da.SelectCommand.Parameters.AddWithValue("@brandname", DBNull.Value);
                if (pde.Shipping == "あり")
                {
                    pde.Shipping = "1";
                }
                else
                    pde.Shipping = "0";
                da.SelectCommand.Parameters.AddWithValue("@shipping",Convert.ToInt32(pde.Shipping));
                da.SelectCommand.Parameters.AddWithValue("@PageIndex", pindex);
                da.SelectCommand.Parameters.AddWithValue("@PageSize", psize);
                da.SelectCommand.Parameters.AddWithValue("@option", option);
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

        public DataTable SelectAllEqual(Promotion_Delivery_Entity pdel, int option, int pindex, int psize)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SP_Promotion_Delivery_EqualSearch";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                if (!String.IsNullOrWhiteSpace(pdel.Itemcode))
                    da.SelectCommand.Parameters.AddWithValue("@itemcode", pdel.Itemcode);
                else
                    da.SelectCommand.Parameters.AddWithValue("@itemcode", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(pdel.Itemname))
                    da.SelectCommand.Parameters.AddWithValue("@itemname", pdel.Itemname);
                else
                    da.SelectCommand.Parameters.AddWithValue("@itemname", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(pdel.Shopnmae))
                    da.SelectCommand.Parameters.AddWithValue("@shopname", pdel.Shopnmae);
                else
                    da.SelectCommand.Parameters.AddWithValue("@shopname", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(pdel.Brandname))
                    da.SelectCommand.Parameters.AddWithValue("@brandname", pdel.Brandname);
                else
                    da.SelectCommand.Parameters.AddWithValue("@brandname", DBNull.Value);
                if (pdel.Shipping == "あり")
                {
                    pdel.Shipping = "1";
                }
                else
                    pdel.Shipping = "0";
                da.SelectCommand.Parameters.AddWithValue("@shipping", Convert.ToInt32(pdel.Shipping));
                da.SelectCommand.Parameters.AddWithValue("@PageIndex", pindex);
                da.SelectCommand.Parameters.AddWithValue("@PageSize", psize);
                da.SelectCommand.Parameters.AddWithValue("@option", option);
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

        public DataTable SelectIsDelivery(Promotion_Delivery_Entity pde, int option, int pindex, int psize)
        { 
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SP_Promotion_Delivery_IsDeliverySearch";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                if (!String.IsNullOrWhiteSpace(pde.Itemcode))
                    da.SelectCommand.Parameters.AddWithValue("@itemcode", pde.Itemcode);
                else
                    da.SelectCommand.Parameters.AddWithValue("@itemcode", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(pde.Itemname))
                    da.SelectCommand.Parameters.AddWithValue("@itemname", pde.Itemname);
                else
                    da.SelectCommand.Parameters.AddWithValue("@itemname", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(pde.Shopnmae))
                    da.SelectCommand.Parameters.AddWithValue("@shopname", pde.Shopnmae);
                else
                    da.SelectCommand.Parameters.AddWithValue("@shopname", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(pde.Brandname))
                    da.SelectCommand.Parameters.AddWithValue("@brandname", pde.Brandname);
                else
                    da.SelectCommand.Parameters.AddWithValue("@brandname", DBNull.Value);

                da.SelectCommand.Parameters.AddWithValue("@shipping", Convert.ToInt32(pde.Shipping));
                da.SelectCommand.Parameters.AddWithValue("@PageIndex", pindex);
                da.SelectCommand.Parameters.AddWithValue("@PageSize", psize);
                da.SelectCommand.Parameters.AddWithValue("@option", option);
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


        public DataTable DeliveryEqualSearch(Promotion_Delivery_Entity pde, int option, int pindex, int psize)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SP_Promotion_Delivery_IsDeliveryEqualSearch";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                if (!String.IsNullOrWhiteSpace(pde.Itemcode))
                    da.SelectCommand.Parameters.AddWithValue("@itemcode", pde.Itemcode);
                else
                    da.SelectCommand.Parameters.AddWithValue("@itemcode", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(pde.Itemname))
                    da.SelectCommand.Parameters.AddWithValue("@itemname", pde.Itemname);
                else
                    da.SelectCommand.Parameters.AddWithValue("@itemname", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(pde.Shopnmae))
                    da.SelectCommand.Parameters.AddWithValue("@shopname", pde.Shopnmae);
                else
                    da.SelectCommand.Parameters.AddWithValue("@shopname", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(pde.Brandname))
                    da.SelectCommand.Parameters.AddWithValue("@brandname", pde.Brandname);
                else
                    da.SelectCommand.Parameters.AddWithValue("@brandname", DBNull.Value);
                if (pde.Shipping == "あり")
                {
                    pde.Shipping = "1";
                }
                else
                    pde.Shipping = "0";
                da.SelectCommand.Parameters.AddWithValue("@shipping", Convert.ToInt32(pde.Shipping));
                da.SelectCommand.Parameters.AddWithValue("@PageIndex", pindex);
                da.SelectCommand.Parameters.AddWithValue("@PageSize", psize);
                da.SelectCommand.Parameters.AddWithValue("@option", option);
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


        public DataTable Search(Promotion_Delivery_Entity pdel,int shpdel)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SP_Select_Delivery";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@shipping",shpdel);
                da.SelectCommand.CommandTimeout = 0;
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

        public int SaveUpdate(DataTable dt)
        {

            try
            {
                int id = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                    SqlCommand cmd = new SqlCommand("SP_Promotion_Delivery_SaveUpdate", connectionString);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@Item_Code", dt.Rows[i]["Item_Code"].ToString());
                    cmd.Parameters.Add("@Rsdate", dt.Rows[i]["Delivery_StartDate"].ToString());
                    cmd.Parameters.Add("@Redate", dt.Rows[i]["Delivery_EndDate"].ToString());
                    cmd.Parameters.Add("@Rstime", dt.Rows[i]["Delivery_StartTime"].ToString());
                    cmd.Parameters.Add("@Retime", dt.Rows[i]["Delivery_EndTime"].ToString());
                    cmd.Parameters.Add("@IsDelivery", dt.Rows[i]["IsDelivery"].ToString());
;                   cmd.Parameters.Add("@ShopID", dt.Rows[i]["Shop_ID"].ToString());
                    cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                }
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
