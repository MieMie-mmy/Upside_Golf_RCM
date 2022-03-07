/* 
Created By              : Kay Thi Aung
Created Date          : 18/06/2014
Updated By             :
Updated Date         :

 Tables using:  Shop
    -
    -
    -
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
  public   class Shop_DL
    {
      

     public Shop_DL() {  }

      public bool Insert(Shop_Entity sentity)
      {
          SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
          SqlCommand cmd = new SqlCommand();
          try
          {
              cmd.CommandText = "SP_Shop_Insert";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Connection = connectionString;
              cmd.Parameters.AddWithValue("@shopid", sentity.ShopID);
              cmd.Parameters.AddWithValue("@shopname", sentity.ShopName);
              cmd.Parameters.AddWithValue("@mall", sentity.MallOpen);
              cmd.Parameters.AddWithValue("@ftphost", sentity.FTPhost);
              cmd.Parameters.AddWithValue("@ftpacc", sentity.FTPacc);
              cmd.Parameters.AddWithValue("@ftppass", sentity.FTPpass);
              cmd.Parameters.AddWithValue("@shp", sentity.shpURL);
              cmd.Parameters.AddWithValue("@img", sentity.imgURL);
              if (sentity.Shipping_Condition != 0)
              {
                  cmd.Parameters.AddWithValue("@freeshipping",Convert.ToInt32(sentity.Shipping_Condition));
              }
              else
              {
                  cmd.Parameters.AddWithValue("@freeshipping", DBNull.Value);
              }
              cmd.Parameters.AddWithValue("@status", sentity.Status);
              //updated date 15/06/2015
              cmd.Parameters.AddWithValue("@libftphost", sentity.Libftphost);
              cmd.Parameters.AddWithValue("@libftpacc", sentity.Libftpacc);
              cmd.Parameters.AddWithValue("@libftppass", (sentity.Libftppass));
              cmd.Parameters.AddWithValue("@libftpdirectory", sentity.Libdirectory);
              cmd.Parameters.AddWithValue("@categorycheck", sentity.Categorycheck1);
              cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
              cmd.Connection.Open();
              cmd.ExecuteNonQuery();
              cmd.Connection.Close();
              int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
              if (id > 0)
              {
                  return true;
              }
              return false;
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }

      }

      public bool  Update(Shop_Entity sentity)
      {
          SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
          SqlCommand cmd = new SqlCommand();
          try
          {
              cmd.CommandText = "SP_Shop_Update";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Connection = connectionString;
              cmd.CommandTimeout = 0;
              cmd.Parameters.AddWithValue("@ID",sentity.ID);
              cmd.Parameters.AddWithValue("@shopid", sentity.ShopID);
              cmd.Parameters.AddWithValue("@shopname", sentity.ShopName);
              cmd.Parameters.AddWithValue("@mall", sentity.MallOpen);
              cmd.Parameters.AddWithValue("@ftphost", sentity.FTPhost);
              cmd.Parameters.AddWithValue("@ftpacc", sentity.FTPacc);
              cmd.Parameters.AddWithValue("@ftppass", sentity.FTPpass);
              cmd.Parameters.AddWithValue("@shp", sentity.shpURL);
              cmd.Parameters.AddWithValue("@img", sentity.imgURL);
              cmd.Parameters.AddWithValue("@freeshipping",(sentity.Shipping_Condition));
              cmd.Parameters.AddWithValue("@status", sentity.Status);
              //updated date 15/06/2015
              cmd.Parameters.AddWithValue("@libftphost", sentity.Libftphost);
              cmd.Parameters.AddWithValue("@libftpacc", sentity.Libftpacc);
              cmd.Parameters.AddWithValue("@libftppass", (sentity.Libftppass));
              cmd.Parameters.AddWithValue("@libftpdirectory", sentity.Libdirectory);
              cmd.Parameters.AddWithValue("@categorycheck", sentity.Categorycheck1);
              cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
              cmd.Connection.Open();
              cmd.ExecuteNonQuery();
              cmd.Connection.Close();
              int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
              if (id > 0)
              {
                  return true;
              }
              return false;
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }

      }

      public DataTable Search( string name,string mall)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter();
              SqlCommand cmdSelect = new SqlCommand("SP_Shop_Search", connectionString);
              da.SelectCommand = cmdSelect;
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              if (String.IsNullOrWhiteSpace(name)) 
              {
                  cmdSelect.Parameters.AddWithValue("@shopname", DBNull.Value);
              }
              else
              cmdSelect.Parameters.AddWithValue("@shopname",name);

              if (String.IsNullOrWhiteSpace(mall)) 
              {
                  cmdSelect.Parameters.AddWithValue("@mall",DBNull.Value);
              }
              else
              cmdSelect.Parameters.AddWithValue("@mall",mall);
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

      public Shop_Entity SelctByID(int ID) 
      {
          SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
          SqlDataAdapter sda = new SqlDataAdapter("SP_Shop_SelectByID", connectionString);
       try
       {
           sda.SelectCommand.CommandType = CommandType.StoredProcedure;
           sda.SelectCommand.CommandTimeout = 0;
           sda.SelectCommand.Parameters.AddWithValue("@ID", ID);

           DataTable dt = new DataTable();

           sda.SelectCommand.Connection.Open();
           sda.Fill(dt);
           sda.SelectCommand.Connection.Close();
           Shop_Entity sentity = new Shop_Entity();
                if (dt.Rows.Count > 0)
                {
                    sentity.ID = (dt.Rows[0]["ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["ID"]);
                    if (dt.Rows[0]["Shop_ID"] != DBNull.Value)
                    {
                        sentity.ShopID = dt.Rows[0]["Shop_ID"].ToString();
                    }

                    if (dt.Rows[0]["Shop_Name"] != DBNull.Value)
                    {
                        sentity.ShopName = dt.Rows[0]["Shop_Name"].ToString();
                    }

                    if (dt.Rows[0]["Mall_ID"] != DBNull.Value)
                    {
                        sentity.MallOpen = dt.Rows[0]["Mall_ID"].ToString();
                    }

                    if (dt.Rows[0]["FTP_Account"] != DBNull.Value)
                    {
                        sentity.FTPacc = dt.Rows[0]["FTP_Account"].ToString();
                    }

                    if (dt.Rows[0]["FTP_Host"] != DBNull.Value)
                    {
                        sentity.FTPhost = dt.Rows[0]["FTP_Host"].ToString();
                    }

                    if (dt.Rows[0]["FTP_Password"] != DBNull.Value)
                    {
                        sentity.FTPpass = dt.Rows[0]["FTP_Password"].ToString();
                    }

                    if (dt.Rows[0]["Shop_URL"] != DBNull.Value)
                    {
                        sentity.shpURL = dt.Rows[0]["Shop_URL"].ToString();
                    }

                    if (dt.Rows[0]["Image_URL"] != DBNull.Value)
                    {
                        sentity.imgURL = dt.Rows[0]["Image_URL"].ToString();
                    }

                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Status"].ToString()))
                    {
                        sentity.Status = (int)dt.Rows[0]["Status"];
                    }
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Shipping_Condition"].ToString()))
                    {
                        sentity.Shipping_Condition = Convert.ToInt32(dt.Rows[0]["Shipping_Condition"].ToString());
                    }
                    else
                        sentity.Shipping_Condition = 0;

                }

                //updated date 15/06/2015
                if (dt.Rows[0]["LibraryFTP_Host"] != DBNull.Value)
                {
                    sentity.Libftphost = dt.Rows[0]["LibraryFTP_Host"].ToString();
                }

                if (dt.Rows[0]["LibraryFTP_Account"] != DBNull.Value)
                {
                    sentity.Libftpacc = dt.Rows[0]["LibraryFTP_Account"].ToString();
                }

                if (dt.Rows[0]["LibraryFTP_Password"] != DBNull.Value)
                {
                    sentity.Libftppass = dt.Rows[0]["LibraryFTP_Password"].ToString();
                }

                if (dt.Rows[0]["LibraryImage_Directory"] != DBNull.Value)
                {
                    sentity.Libdirectory = dt.Rows[0]["LibraryImage_Directory"].ToString();
                }

                sentity.Categorycheck1 = (dt.Rows[0]["Category_Flag"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["Category_Flag"]);

                return sentity;
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
              //string cmdstr = "select * from Shop";
              SqlDataAdapter sda = new SqlDataAdapter("SP_Shop_SelectMallListByShop", connectionString);
              sda.SelectCommand.CommandType = CommandType.StoredProcedure;
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

        public DataTable SelectAll_URL()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                //string cmdstr = "select * from Shop";
                SqlDataAdapter sda = new SqlDataAdapter("SP_Shop_SelectMall_URL", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
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

        public DataTable SelectShopAndMall()
      {
        try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              //string sqlQuery = "SELECT Shop.ID, Code_Setup.Code_Description AS Mall_Name, Shop.Shop_Name, Shop.Mall_ID FROM Shop INNER JOIN "
              //    + "Code_Setup ON Shop.Mall_ID = Code_Setup.Code_ID WHERE Code_Setup.Code_Type = 1";
              string sqlQuery = "SELECT * FROM Shop WITH (NOLOCK)";
              DataTable dt = new DataTable();
              SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, connectionString);
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

      public string SelectProductPageURL(int Shop_ID)
      {
          try
          {
              DataTable dt = new DataTable();
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter sda = new SqlDataAdapter("SP_Shop_Product_Page_URL", connectionString);
              //SqlCommand cmd = new SqlCommand(quary, connectionString);
              sda.SelectCommand.CommandType = CommandType.StoredProcedure;
              sda.SelectCommand.CommandTimeout = 0;
              sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", Shop_ID);
              sda.SelectCommand.Connection.Open();
              sda.Fill(dt);
              sda.SelectCommand.Connection.Close();
              if (dt != null && dt.Rows.Count > 0)
              {
                  return dt.Rows[0]["Product_Page_URL"].ToString();
              }
              else
                  return "";
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

    }
}
