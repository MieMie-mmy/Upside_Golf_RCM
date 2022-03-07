using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;


namespace ORS_RCM_DL
{
    public class Details_Yahoo_Exhibation_DL
    {
        

        public Details_of_exhibition_Yahoo_Entity SelectByID(int ID)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_DetailsofYahoo_Exhibition", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;



                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", ID);
                DataTable dt = new DataTable();

                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                Details_of_exhibition_Yahoo_Entity detailY = new Details_of_exhibition_Yahoo_Entity();
                if (dt.Rows.Count > 0)
                {

                   
                    detailY.Shop_Name = dt.Rows[0]["Shop_Name"].ToString();

                   // detailY.Sale_Price = Convert.ToInt32(dt.Rows[0]["Sale_Price"].ToString());

                    detailY.Ship_Weight = dt.Rows[0]["Weight"].ToString();

               //  detailY.Product_State = (int)dt.Rows[0]["Product_State"];

                   // detailY.Additional1 = dt.Rows[0]["Additional1"].ToString();

                    //detailY.Additional2 = dt.Rows[0]["Additional2"].ToString();

                   // detailY.Additional3 = dt.Rows[0]["Additional3"].ToString();

                  //  detailY.Item_Code = dt.Rows[0]["Item_Code"].ToString();

                 //   detailY.Item_Name = dt.Rows[0]["Item_Name"].ToString();

                    detailY.Taxable = dt.Rows[0]["Taxable"].ToString();

                    detailY.NoofPurchase = dt.Rows[0]["NoofPurchase"].ToString();

                //    detailY.JAN_Code = dt.Rows[0]["JAN_Code"].ToString();


                    detailY.Template = dt.Rows[0]["Template"].ToString();

                  //  detailY.Brand_Code_Yahoo = dt.Rows[0]["Brand_Code"].ToString();
                    if (!DBNull.Value.Equals(dt.Rows[0]["Release_Date"]))
                    {
                        detailY.Release_Date = Convert.ToDateTime((dt.Rows[0]["Release_Date"]).ToString());
                    }
                    detailY.Listing_Japan = dt.Rows[0]["Listing_Japan"].ToString();

                }
                return detailY;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataTable SelectbyItemID(int ItemID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SP_Yahoo_Exhibition_SelectbyItemID", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@Item_ID", ItemID);
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

        public DataTable SelectByExhibitionData(string str, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_ExhibitionDetailForYahoo", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
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
    }

}


        


      




 
                   




                





   
    