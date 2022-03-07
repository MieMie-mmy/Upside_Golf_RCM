using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
  public   class Promotion_Point_DL
    {
      public Promotion_Point_DL() { }


      public DataTable SelectAll(Promotion_Point_Entity pime, int option, int pindex, int psize) 
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              //string query = "SP_Promotion_PointSelectAll";
              string query = "SP_Promotion_Point_LikeSearch";
              SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@itemcode",pime.Itemcode);
              da.SelectCommand.Parameters.AddWithValue("@itemname",pime.Itemname);
              da.SelectCommand.Parameters.AddWithValue("@shopname",pime.Shopnmae);
              da.SelectCommand.Parameters.AddWithValue("@brandname",pime.Brandname);
              da.SelectCommand.Parameters.AddWithValue("@year",pime.Year);
              da.SelectCommand.Parameters.AddWithValue("@season",pime.Season);
              da.SelectCommand.Parameters.AddWithValue("@competitionname",pime.Competationname);
              da.SelectCommand.Parameters.AddWithValue("@instructionno",pime.InstructionNo);
              da.SelectCommand.Parameters.AddWithValue("@claffication", pime.Claffication);
              da.SelectCommand.Parameters.AddWithValue("@Rpmg", pime.RP);
              da.SelectCommand.Parameters.AddWithValue("@Ypmg", pime.YP);
              da.SelectCommand.Parameters.AddWithValue("@Ppmg", pime.PP);
              da.SelectCommand.Parameters.AddWithValue("@Rperiodfrom", pime.Rperiodfrom);
              da.SelectCommand.Parameters.AddWithValue("@Rperiodto", pime.Rperiodto);
              da.SelectCommand.Parameters.AddWithValue("@Yperiodfrom", pime.Yperiodfrom);
              da.SelectCommand.Parameters.AddWithValue("@Yperiodto", pime.Yperiodto);
              da.SelectCommand.Parameters.AddWithValue("@Pperiodfrom", pime.Pperiodfrom);
              da.SelectCommand.Parameters.AddWithValue("@Pperiodto", pime.Pperiodto);
              da.SelectCommand.Parameters.AddWithValue("@shopstatus", pime.Shopstatus);
              da.SelectCommand.Parameters.AddWithValue("@PageIndex", pindex);
              da.SelectCommand.Parameters.AddWithValue("@PageSize", psize);
              da.SelectCommand.Parameters.AddWithValue("@option",option);
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
      public DataTable SelectAllEqual(Promotion_Point_Entity pime, int option,int pindex,int psize)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              string query = "SP_Promotion_Point_EqualSearch";
              SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@itemcode", pime.Itemcode);
              da.SelectCommand.Parameters.AddWithValue("@itemname", pime.Itemname);
              da.SelectCommand.Parameters.AddWithValue("@shopname", pime.Shopnmae);
              da.SelectCommand.Parameters.AddWithValue("@brandname", pime.Brandname);
              da.SelectCommand.Parameters.AddWithValue("@year", pime.Year);
              da.SelectCommand.Parameters.AddWithValue("@season", pime.Season);
              da.SelectCommand.Parameters.AddWithValue("@competitionname", pime.Competationname);
              da.SelectCommand.Parameters.AddWithValue("@instructionno", pime.InstructionNo);
              da.SelectCommand.Parameters.AddWithValue("@claffication", pime.Claffication);
              da.SelectCommand.Parameters.AddWithValue("@Rpmg", pime.RP);
              da.SelectCommand.Parameters.AddWithValue("@Ypmg", pime.YP);
              da.SelectCommand.Parameters.AddWithValue("@Ppmg", pime.PP);
              da.SelectCommand.Parameters.AddWithValue("@Rperiodfrom", pime.Rperiodfrom);
              da.SelectCommand.Parameters.AddWithValue("@Rperiodto", pime.Rperiodto);
              da.SelectCommand.Parameters.AddWithValue("@Yperiodfrom", pime.Yperiodfrom);
              da.SelectCommand.Parameters.AddWithValue("@Yperiodto", pime.Yperiodto);
              da.SelectCommand.Parameters.AddWithValue("@Pperiodfrom", pime.Pperiodfrom);
              da.SelectCommand.Parameters.AddWithValue("@Pperiodto", pime.Pperiodto);
              da.SelectCommand.Parameters.AddWithValue("@shopstatus", pime.Shopstatus);
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
      public int  SaveUpdate(DataTable dt) 
      {

          try
          {
              int id = 0;
              for (int i = 0; i < dt.Rows.Count; i++)
              {
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["ShopID"].ToString()))
                  {
                      SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                      SqlCommand cmd = new SqlCommand("SP_Promotion_Point_SaveUpdate", connectionString);
                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.CommandTimeout = 0;
 
                      cmd.Parameters.AddWithValue("@Item_Code", dt.Rows[i]["Item_Code"].ToString());
                      cmd.Parameters.AddWithValue("@Rpoint", dt.Rows[i]["Rakuten_Point"]);
                      cmd.Parameters.AddWithValue("@Ypoint", dt.Rows[i]["Yahoo_Point"]);
                      cmd.Parameters.AddWithValue("@Ppoint", dt.Rows[i]["Ponpare_Point"]);
                      cmd.Parameters.AddWithValue("@Rsdate", dt.Rows[i]["Rakuten_StartDate"].ToString());
                      cmd.Parameters.AddWithValue("@Redate", dt.Rows[i]["Rakuten_EndDate"].ToString());
                      cmd.Parameters.AddWithValue("@Ysdate", dt.Rows[i]["Yahoo_StartDate"].ToString());
                      cmd.Parameters.AddWithValue("@Yedate", dt.Rows[i]["Yahoo_EndDate"].ToString());
                      cmd.Parameters.AddWithValue("@Psdate", dt.Rows[i]["Ponpare_StartDate"].ToString());
                      cmd.Parameters.AddWithValue("@Pedate", dt.Rows[i]["Ponpare_EndDate"].ToString());
                      cmd.Parameters.AddWithValue("@Rstime", dt.Rows[i]["Rakuten_StartTime"].ToString());
                      cmd.Parameters.AddWithValue("@Retime", dt.Rows[i]["Rakuten_EndTime"].ToString());
                      cmd.Parameters.AddWithValue("@Ystime", dt.Rows[i]["Yahoo_StartTime"].ToString());
                      cmd.Parameters.AddWithValue("@Yetime", dt.Rows[i]["Yahoo_EndTime"].ToString());
                      cmd.Parameters.AddWithValue("@Pstime", dt.Rows[i]["Ponpare_StartTime"].ToString());
                      cmd.Parameters.AddWithValue("@Petime", dt.Rows[i]["Ponpare_EndTime"].ToString());
                      cmd.Parameters.AddWithValue("@ShopID", dt.Rows[i]["ShopID"]);
                      cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;

                      cmd.Connection.Open();

                      cmd.ExecuteNonQuery();
                      cmd.Connection.Close();
                      id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                  }
              }
              return id;
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }
      }

      //updated date13/07/2015
      public DataTable SelectAllCheckdata(Promotion_Point_Entity pime, int option)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              string query = "SP_Point_Pageselectall";
              SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@itemcode", pime.Itemcode);
              da.SelectCommand.Parameters.AddWithValue("@itemname", pime.Itemname);
              da.SelectCommand.Parameters.AddWithValue("@shopname", pime.Shopnmae);
              da.SelectCommand.Parameters.AddWithValue("@brandname", pime.Brandname);
              da.SelectCommand.Parameters.AddWithValue("@year", pime.Year);
              da.SelectCommand.Parameters.AddWithValue("@season", pime.Season);
              da.SelectCommand.Parameters.AddWithValue("@competitionname", pime.Competationname);
              da.SelectCommand.Parameters.AddWithValue("@instructionno", pime.InstructionNo);
              da.SelectCommand.Parameters.AddWithValue("@claffication", pime.Claffication);
              da.SelectCommand.Parameters.AddWithValue("@Rpmg", pime.RP);
              da.SelectCommand.Parameters.AddWithValue("@Ypmg", pime.YP);
              da.SelectCommand.Parameters.AddWithValue("@Ppmg", pime.PP);
              da.SelectCommand.Parameters.AddWithValue("@Rperiodfrom", pime.Rperiodfrom);
              da.SelectCommand.Parameters.AddWithValue("@Rperiodto", pime.Rperiodto);
              da.SelectCommand.Parameters.AddWithValue("@Yperiodfrom", pime.Yperiodfrom);
              da.SelectCommand.Parameters.AddWithValue("@Yperiodto", pime.Yperiodto);
              da.SelectCommand.Parameters.AddWithValue("@Pperiodfrom", pime.Pperiodfrom);
              da.SelectCommand.Parameters.AddWithValue("@Pperiodto", pime.Pperiodto);
              da.SelectCommand.Parameters.AddWithValue("@shopstatus", pime.Shopstatus);
           
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
      public void DeleteByPromotionID(int promotionID)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              string query = "DELETE FROM Promotion_Point_Shop WHERE Promotion_Point_ID = " + promotionID;
              SqlCommand cmd = new SqlCommand(query, connectionString);
              cmd.CommandType = CommandType.Text;
              cmd.CommandTimeout = 0;
              cmd.Connection.Open();
              cmd.ExecuteNonQuery();
              cmd.Connection.Close();
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }
      }
      public void Insert(DataTable dt,int id)
      {
          try
          {
            
              DataColumn newcols = new DataColumn("PromotionID", typeof(int));
              newcols.DefaultValue = id;
              dt.Columns.Add(newcols);
              for (int y = 0; y < dt.Rows.Count; y++)
              {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand("SP_PromotionPoint_Shop_Insert", connectionString);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              
                  cmd.Parameters.Add("@PromotionID",(int)dt.Rows[y]["PromotionID"]);
                  cmd.Parameters.Add("@ShopId", (int)dt.Rows[y]["ShopID"]);
                  cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                
                  cmd.Connection.Open();
               
                  cmd.ExecuteNonQuery();
                  cmd.Connection.Close();
              }
              dt.Columns.Remove("PromotionID");
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }

      }
      public DataTable SelectbyID(string itemID)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              string query = "SP_Promotion_Point_SelectbyID";
              SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@list", itemID);
            
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

      public DataTable PopupSelectall(Promotion_Point_Entity pime, int option)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              string query = "SP_Promotionpopup_Selectall";
              SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@itemcode", pime.Itemcode);
              da.SelectCommand.Parameters.AddWithValue("@itemname", pime.Itemname);
              if (!String.IsNullOrWhiteSpace(pime.Shopnmae))
              da.SelectCommand.Parameters.AddWithValue("@shopstatus", pime.Shopnmae);
              else
                  da.SelectCommand.Parameters.AddWithValue("@shopstatus",DBNull.Value);
              da.SelectCommand.Parameters.AddWithValue("@brandname", pime.Brandname);
              da.SelectCommand.Parameters.AddWithValue("@year", pime.Year);
              da.SelectCommand.Parameters.AddWithValue("@season", pime.Season);
              da.SelectCommand.Parameters.AddWithValue("@competitionname", pime.Competationname);
              da.SelectCommand.Parameters.AddWithValue("@instructionno", pime.InstructionNo);
              da.SelectCommand.Parameters.AddWithValue("@claffication", pime.Claffication);
             
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

      //for exhibition

      public DataTable SelectAll_Item(string list, string shop, string mall, string code, int ctrl, string shopid, string API,
          string error, string exhibitor, string bcheck, string proname, string catInfo, string brname, string comname, string copname,
          string claname, string year, string season, string remark, DateTime? exdate1, DateTime? exdate2)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter("SP_PromotionEhibition_SelectAll", connectionString);
              DataTable dt = new DataTable();
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@list", list);
              if (!String.IsNullOrWhiteSpace(shop))
              {
                  da.SelectCommand.Parameters.AddWithValue("@shop", shop);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@shop", null);
              }
              if (!String.IsNullOrWhiteSpace(mall))
              {
                  da.SelectCommand.Parameters.AddWithValue("@mall", mall);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@mall", null);
              }
              if (!String.IsNullOrWhiteSpace(code))
              {
                  da.SelectCommand.Parameters.AddWithValue("@itemcode", code);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@itemcode", null);
              }
              if (!String.IsNullOrWhiteSpace(shopid))
              {
                  int sid = Convert.ToInt32(shopid);
                  da.SelectCommand.Parameters.AddWithValue("@Shop_ID", sid);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@Shop_ID", null);
              }
              if (!String.IsNullOrWhiteSpace(API))
              {
                  int api = Convert.ToInt32(API);
                  da.SelectCommand.Parameters.AddWithValue("@API", api);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@API", null);
              }
              if (!String.IsNullOrWhiteSpace(error))
              {
                  int er = Convert.ToInt32(error);
                  da.SelectCommand.Parameters.AddWithValue("@error", er);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@error", null);
              }
              if (!String.IsNullOrWhiteSpace(exhibitor))
              {
                  int exr = Convert.ToInt32(exhibitor);
                  da.SelectCommand.Parameters.AddWithValue("@exhibitor", exr);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@exhibitor", null);
              }
              if (!String.IsNullOrWhiteSpace(bcheck))
              {
                  int bc = Convert.ToInt32(bcheck);
                  da.SelectCommand.Parameters.AddWithValue("@blackcheck", bc);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@blackcheck", null);
              }
              if (!String.IsNullOrWhiteSpace(proname))
              {

                  da.SelectCommand.Parameters.AddWithValue("@productname", proname);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@productname", null);
              }
              if (!String.IsNullOrWhiteSpace(catInfo))
              {

                  da.SelectCommand.Parameters.AddWithValue("@catlogInfo", catInfo);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@catlogInfo", null);
              }
              if (!String.IsNullOrWhiteSpace(brname))
              {

                  da.SelectCommand.Parameters.AddWithValue("@brandname", brname);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@brandname", null);
              }
              if (!String.IsNullOrWhiteSpace(comname))
              {

                  da.SelectCommand.Parameters.AddWithValue("@companyname", comname);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@companyname", null);
              }
              if (!String.IsNullOrWhiteSpace(copname))
              {

                  da.SelectCommand.Parameters.AddWithValue("@comptname", copname);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@comptname", null);
              }
              if (!String.IsNullOrWhiteSpace(claname))
              {

                  da.SelectCommand.Parameters.AddWithValue("@classname", claname);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@classname", null);
              }
              if (!String.IsNullOrWhiteSpace(year))
              {

                  da.SelectCommand.Parameters.AddWithValue("@year", year);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@year", null);
              }
              if (!String.IsNullOrWhiteSpace(season))
              {

                  da.SelectCommand.Parameters.AddWithValue("@season", season);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@season", null);
              }
              if (!String.IsNullOrWhiteSpace(remark))
              {

                  da.SelectCommand.Parameters.AddWithValue("@remark", remark);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@remark", null);
              }
              da.SelectCommand.Parameters.AddWithValue("@ctrl", ctrl);
              //if (!String.IsNullOrWhiteSpace(remark))
              //{

              da.SelectCommand.Parameters.AddWithValue("@exdate1", exdate1);
              //  }
              //else
              //{
              //    da.SelectCommand.Parameters.AddWithValue("@exdate1", null);
              //}
              //if (!String.IsNullOrWhiteSpace(remark))
              //{

              da.SelectCommand.Parameters.AddWithValue("@exdate2", exdate2);
              //  }
              //else
              //{
              //    da.SelectCommand.Parameters.AddWithValue("@exdate2", null);
              //}

              da.SelectCommand.Connection.Open();
              da.SelectCommand.ExecuteNonQuery();
              da.Fill(dt);
              da.SelectCommand.Connection.Close();
              return dt;

          }
          catch (Exception ex)
          { throw ex; }

      }

      public DataTable SelectMall(int Id)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter("SP_Exhibitionlist_Selectmall", connectionString);
              DataTable dt = new DataTable();
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@ID", Id);
              da.SelectCommand.Connection.Open();
              da.SelectCommand.ExecuteNonQuery();
              da.Fill(dt);
              da.SelectCommand.Connection.Close();
              return dt;

          }
          catch (Exception ex)
          { throw ex; }

      }

      public DataTable SelectAll_Itempaging(string list, string shop, string mall, string code, int ctrl, string shopid, string API,
         string error, string exhibitor, string bcheck, string proname, string catInfo, string brname, string comname, string copname,
         string claname, string year, string season, string remark, DateTime? exdate1, DateTime? exdate2,int pageIndex,int pageSize)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlDataAdapter da = new SqlDataAdapter("SP_PromotionEhibition_SelectAllPaging", connectionString);
              DataTable dt = new DataTable();
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.CommandTimeout = 0;
              da.SelectCommand.Parameters.AddWithValue("@list", list);
              if (!String.IsNullOrWhiteSpace(shop))
              {
                  da.SelectCommand.Parameters.AddWithValue("@shop", shop);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@shop", null);
              }
              if (!String.IsNullOrWhiteSpace(mall))
              {
                  da.SelectCommand.Parameters.AddWithValue("@mall", mall);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@mall", null);
              }
              if (!String.IsNullOrWhiteSpace(code))
              {
                  da.SelectCommand.Parameters.AddWithValue("@itemcode", code);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@itemcode", null);
              }
              if (!String.IsNullOrWhiteSpace(shopid))
              {
                  int sid = Convert.ToInt32(shopid);
                  da.SelectCommand.Parameters.AddWithValue("@Shop_ID", sid);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@Shop_ID", null);
              }
              if (!String.IsNullOrWhiteSpace(API))
              {
                  int api = Convert.ToInt32(API);
                  da.SelectCommand.Parameters.AddWithValue("@API", api);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@API", null);
              }
              if (!String.IsNullOrWhiteSpace(error))
              {
                  int er = Convert.ToInt32(error);
                  da.SelectCommand.Parameters.AddWithValue("@error", er);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@error", null);
              }
              if (!String.IsNullOrWhiteSpace(exhibitor))
              {
                  int exr = Convert.ToInt32(exhibitor);
                  da.SelectCommand.Parameters.AddWithValue("@exhibitor", exr);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@exhibitor", null);
              }
              if (!String.IsNullOrWhiteSpace(bcheck))
              {
                  int bc = Convert.ToInt32(bcheck);
                  da.SelectCommand.Parameters.AddWithValue("@blackcheck", bc);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@blackcheck", null);
              }
              if (!String.IsNullOrWhiteSpace(proname))
              {

                  da.SelectCommand.Parameters.AddWithValue("@productname", proname);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@productname", null);
              }
              if (!String.IsNullOrWhiteSpace(catInfo))
              {

                  da.SelectCommand.Parameters.AddWithValue("@catlogInfo", catInfo);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@catlogInfo", null);
              }
              if (!String.IsNullOrWhiteSpace(brname))
              {

                  da.SelectCommand.Parameters.AddWithValue("@brandname", brname);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@brandname", null);
              }
              if (!String.IsNullOrWhiteSpace(comname))
              {

                  da.SelectCommand.Parameters.AddWithValue("@companyname", comname);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@companyname", null);
              }
              if (!String.IsNullOrWhiteSpace(copname))
              {

                  da.SelectCommand.Parameters.AddWithValue("@comptname", copname);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@comptname", null);
              }
              if (!String.IsNullOrWhiteSpace(claname))
              {

                  da.SelectCommand.Parameters.AddWithValue("@classname", claname);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@classname", null);
              }
              if (!String.IsNullOrWhiteSpace(year))
              {

                  da.SelectCommand.Parameters.AddWithValue("@year", year);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@year", null);
              }
              if (!String.IsNullOrWhiteSpace(season))
              {

                  da.SelectCommand.Parameters.AddWithValue("@season", season);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@season", null);
              }
              if (!String.IsNullOrWhiteSpace(remark))
              {

                  da.SelectCommand.Parameters.AddWithValue("@remark", remark);
              }
              else
              {
                  da.SelectCommand.Parameters.AddWithValue("@remark", null);
              }
              da.SelectCommand.Parameters.AddWithValue("@ctrl", ctrl);
              //if (!String.IsNullOrWhiteSpace(remark))
              //{

              da.SelectCommand.Parameters.AddWithValue("@exdate1", exdate1);
              //  }
              //else
              //{
              //    da.SelectCommand.Parameters.AddWithValue("@exdate1", null);
              //}
              //if (!String.IsNullOrWhiteSpace(remark))
              //{

              da.SelectCommand.Parameters.AddWithValue("@exdate2", exdate2);
              da.SelectCommand.Parameters.AddWithValue("@PageIndex", pageIndex);
              da.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);

              da.SelectCommand.Connection.Open();
              da.SelectCommand.ExecuteNonQuery();
              da.Fill(dt);
              da.SelectCommand.Connection.Close();
              return dt;

          }
          catch (Exception ex)
          { throw ex; }

      }
    }
}
