using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
    public class Exhibition_List_DL
    {
        public Exhibition_List_DL() { }

        public DataTable SelectAll_Item(string list, string shop, string mall, string code, int ctrl, string shopid, string API,
             string error, string exhibitor, string bcheck, string proname, string catInfo, string brname, string comname, string copname,
             string claname, string year, string season, string remark, DateTime? exdate1, DateTime? exdate2)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Exhibition_SelectAll", connectionString);
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

        public void DeleteUpdateOrder(string list)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Delete_UpdateOrder", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@IDList", list);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.ExecuteNonQuery();
                da.SelectCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public DataTable SelectShop()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("Select * from Shop", connectionString);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Connection.Open();
                da.SelectCommand.ExecuteNonQuery();
                da.Fill(dt);
                return dt;

            }
            catch (Exception ex) { throw ex; }


        }

        public DataTable SelectbyID()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Exhibition_SelectExstatus", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
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

        public DataTable Selectexhibitor()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Exhibition_List_SelectExhibitor", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Connection.Open();
                DataTable dt = new DataTable();
                da.Fill(dt);

                da.SelectCommand.Connection.Close();
                return dt;

            }
            catch (Exception ex) { throw ex; }

        }

        public void ChangeIsGeneratedCSVFlag(int Exhibit_ID, int Item_ID, int Shop_ID)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_ChangeFlag_ByMall", connectionstring);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Parameters.AddWithValue("@Exhibit_ID", Exhibit_ID);
                cmd.Parameters.AddWithValue("@Item_ID", Item_ID);
                cmd.Parameters.AddWithValue("@Shop_ID", Shop_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectJishaData(int shopid, string str, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //SqlDataAdapter da = new SqlDataAdapter("SP_ExhibitionDetailForJisha", connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectForJisha", connectionString);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                //da.SelectCommand.Parameters.AddWithValue("@strString",str);
                da.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", int.Parse(str));
                da.SelectCommand.Parameters.AddWithValue("@Shop_ID", shopid);
                da.SelectCommand.Parameters.AddWithValue("@option", option);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.ExecuteNonQuery();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                return dt;

            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable Selecterror(string itemcode, int shopid, int checktype, int exhibition_id)
        { //added by hlz.
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_ExhibitionError_SelectAll", connectionString);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@itemcode", itemcode);
                da.SelectCommand.Parameters.AddWithValue("@shopid", shopid);
                da.SelectCommand.Parameters.AddWithValue("@checktype", checktype);
                da.SelectCommand.Parameters.AddWithValue("@exhibition_id", exhibition_id);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.ExecuteNonQuery();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                return dt;

            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable Exhibition_Search(Exhibition_Entity ee, string list, int option, int pageindex, int pagesize)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("Exhibition_Search", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                if (!string.IsNullOrWhiteSpace(list))
                    cmd.Parameters.AddWithValue("@list", list);
                else cmd.Parameters.AddWithValue("@list", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.Item_Code))
                    cmd.Parameters.AddWithValue("@Item_Code", ee.Item_Code);
                else cmd.Parameters.AddWithValue("@Item_Code", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.Item_Name))
                    cmd.Parameters.AddWithValue("@Item_Name", ee.Item_Name);
                else cmd.Parameters.AddWithValue("@Item_Name", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.CatalogInfo))
                    cmd.Parameters.AddWithValue("@Cat_Info", ee.CatalogInfo);
                else cmd.Parameters.AddWithValue("@Cat_Info", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.BrandName))
                    cmd.Parameters.AddWithValue("@Brand_Name", ee.BrandName);
                else cmd.Parameters.AddWithValue("@Brand_Name", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.CompanyName))
                    cmd.Parameters.AddWithValue("@Company_Name", ee.CompanyName);
                else cmd.Parameters.AddWithValue("@Company_Name", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.CompetitionName))
                    cmd.Parameters.AddWithValue("@Competition_Name", ee.CompetitionName);
                else cmd.Parameters.AddWithValue("@Competition_Name", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.ClassName))
                    cmd.Parameters.AddWithValue("@Class_Name", ee.ClassName);
                else cmd.Parameters.AddWithValue("@Class_Name", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.Year))
                    cmd.Parameters.AddWithValue("@Year", ee.Year);
                else cmd.Parameters.AddWithValue("@Year", DBNull.Value);

                //if (!string.IsNullOrWhiteSpace(ee.StartDate))
                cmd.Parameters.AddWithValue("@StartDate", ee.StartDate);
                //else cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);

                //if (!string.IsNullOrWhiteSpace(ee.EndDate))
                cmd.Parameters.AddWithValue("@EndDate", ee.EndDate);
                //else cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.Season))
                    cmd.Parameters.AddWithValue("@Season", ee.Season);
                else cmd.Parameters.AddWithValue("@Season", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.Remark))
                    cmd.Parameters.AddWithValue("@Remark", ee.Remark);
                else cmd.Parameters.AddWithValue("@Remark", DBNull.Value);

                if (ee.ExhibitedUser == -1)
                    cmd.Parameters.AddWithValue("@Exhibitor", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Exhibitor", ee.ExhibitedUser);

                if (ee.Mall.Equals("ショップ選択"))
                    ee.Mall = String.Empty;
                if (!string.IsNullOrWhiteSpace(ee.Mall))
                    cmd.Parameters.AddWithValue("@MallID", ee.Mall);
                else
                    cmd.Parameters.AddWithValue("@MallID", DBNull.Value);

                if (ee.ApiCheck == -1)
                    cmd.Parameters.AddWithValue("@APICheck", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@APICheck", ee.ApiCheck);
                if (ee.ErrorCheck == -1)
                    cmd.Parameters.AddWithValue("@ErrorCheck", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ErrorCheck", ee.ErrorCheck);
                if (ee.ExhibitionCheck == -1)
                    cmd.Parameters.AddWithValue("@ExhibitionCheck", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ExhibitionCheck", ee.ExhibitionCheck);

                if (!string.IsNullOrWhiteSpace(ee.Instructionno))
                    cmd.Parameters.AddWithValue("@Instructionno", ee.Instructionno);
                else cmd.Parameters.AddWithValue("@Instructionno", DBNull.Value);

                cmd.Parameters.AddWithValue("@Option", option);
                cmd.Parameters.AddWithValue("@PageIndex", pageindex);
                //cmd.Parameters.AddWithValue("@chkerror", chkerror);
                //cmd.Parameters.AddWithValue("@chknotcheck", chknotcheck);
                //cmd.Parameters.AddWithValue("@chkrecovery", chkrecovery);
                cmd.Parameters.AddWithValue("@PageSize", pagesize);


                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            { throw ex; }
        }
        public DataTable DownloadExhibition_Search(Exhibition_Entity ee, string list, int option, int pageindex, int pagesize)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Download", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                if (!string.IsNullOrWhiteSpace(list))
                    cmd.Parameters.AddWithValue("@list", list);
                else cmd.Parameters.AddWithValue("@list", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.Item_Code))
                    cmd.Parameters.AddWithValue("@Item_Code", ee.Item_Code);
                else cmd.Parameters.AddWithValue("@Item_Code", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.Item_Name))
                    cmd.Parameters.AddWithValue("@Item_Name", ee.Item_Name);
                else cmd.Parameters.AddWithValue("@Item_Name", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.CatalogInfo))
                    cmd.Parameters.AddWithValue("@Cat_Info", ee.CatalogInfo);
                else cmd.Parameters.AddWithValue("@Cat_Info", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.BrandName))
                    cmd.Parameters.AddWithValue("@Brand_Name", ee.BrandName);
                else cmd.Parameters.AddWithValue("@Brand_Name", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.CompanyName))
                    cmd.Parameters.AddWithValue("@Company_Name", ee.CompanyName);
                else cmd.Parameters.AddWithValue("@Company_Name", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.CompetitionName))
                    cmd.Parameters.AddWithValue("@Competition_Name", ee.CompetitionName);
                else cmd.Parameters.AddWithValue("@Competition_Name", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.ClassName))
                    cmd.Parameters.AddWithValue("@Class_Name", ee.ClassName);
                else cmd.Parameters.AddWithValue("@Class_Name", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.Year))
                    cmd.Parameters.AddWithValue("@Year", ee.Year);
                else cmd.Parameters.AddWithValue("@Year", DBNull.Value);

                //if (!string.IsNullOrWhiteSpace(ee.StartDate))
                cmd.Parameters.AddWithValue("@StartDate", ee.StartDate);
                //else cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);

                //if (!string.IsNullOrWhiteSpace(ee.EndDate))
                cmd.Parameters.AddWithValue("@EndDate", ee.EndDate);
                //else cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.Season))
                    cmd.Parameters.AddWithValue("@Season", ee.Season);
                else cmd.Parameters.AddWithValue("@Season", DBNull.Value);

                if (!string.IsNullOrWhiteSpace(ee.Remark))
                    cmd.Parameters.AddWithValue("@Remark", ee.Remark);
                else cmd.Parameters.AddWithValue("@Remark", DBNull.Value);

                if (ee.ExhibitedUser == -1)
                    cmd.Parameters.AddWithValue("@Exhibitor", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Exhibitor", ee.ExhibitedUser);

                if (ee.Mall.Equals("ショップ選択"))
                    ee.Mall = String.Empty;
                if (!string.IsNullOrWhiteSpace(ee.Mall))
                    cmd.Parameters.AddWithValue("@MallID", ee.Mall);
                else
                    cmd.Parameters.AddWithValue("@MallID", DBNull.Value);

                if (ee.ApiCheck == -1)
                    cmd.Parameters.AddWithValue("@APICheck", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@APICheck", ee.ApiCheck);
                if (ee.ErrorCheck == -1)
                    cmd.Parameters.AddWithValue("@ErrorCheck", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ErrorCheck", ee.ErrorCheck);
                if (ee.ExhibitionCheck == -1)
                    cmd.Parameters.AddWithValue("@ExhibitionCheck", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ExhibitionCheck", ee.ExhibitionCheck);

                if (!string.IsNullOrWhiteSpace(ee.Instructionno))
                    cmd.Parameters.AddWithValue("@Instructionno", ee.Instructionno);
                else cmd.Parameters.AddWithValue("@Instructionno", DBNull.Value);

                cmd.Parameters.AddWithValue("@Option", option);
                cmd.Parameters.AddWithValue("@PageIndex", pageindex);
                cmd.Parameters.AddWithValue("@PageSize", pagesize);


                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();

                return dt;
            }
            catch (Exception ex)
            { throw ex; }
        }
        public DataTable Selecterror(string itemcode, int shopid, int checktype)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_ExhibitionError_SelectAll", connectionString);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@itemcode", itemcode);
                da.SelectCommand.Parameters.AddWithValue("@shopid", shopid);
                da.SelectCommand.Parameters.AddWithValue("@checktype", checktype);

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
          string claname, string year, string season, string remark, DateTime? exdate1, DateTime? exdate2, int pageIndex, int pageSize)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Exhibition_SelectAllPaging", connectionString);
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
        ///////24/12/2014 AAM/////////

        public int Exhibition_List_Insert(int item_ID, int user_id,string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Exhibition_List_Insert", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", item_ID);
                cmd.Parameters.AddWithValue("@User_ID", user_id);
                cmd.Parameters.AddWithValue("@Option", option);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int eid = Convert.ToInt32(cmd.Parameters["@result"].Value);
                return eid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertMonotaroShop(int item_ID, string flag)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_Monotaro_Shop", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", item_ID);
                cmd.Parameters.AddWithValue("@Option", flag);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeFlagForSoko(int item_ID, int flag)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Change_Flag_Soko", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", item_ID);
                cmd.Parameters.AddWithValue("@Flag_ID", flag);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean CheckSKSStatus(string list)
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_CheckSKSStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@list", list);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();
                if (dt.Rows.Count > 0)
                { return true; }
                else
                { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable GetItemData(string list, int shop_id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_List_CollectData_New", conn);
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

        public int Exhibition_List_DeleteInsert(int item_ID, int user_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //SqlCommand cmd = new SqlCommand("SP_Exhibition_Item_Master_DeleteInsert", connectionString);
                SqlCommand cmd = new SqlCommand("SP_Exhibition_List_Insert", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", item_ID);
                cmd.Parameters.AddWithValue("@User_ID", user_id);
                cmd.Parameters.AddWithValue("@Option", "delete");
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int eid = Convert.ToInt32(cmd.Parameters["@result"].Value);
                return eid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exhibition_Item_Shop_Insert(int EitemID, int itemID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Exhibition_Item_Shop_Insert", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@EItem_ID", EitemID);
                cmd.Parameters.AddWithValue("@Item_ID", itemID);
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

        public DataTable SelectByExhibitionData(int shop_ID, string str, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                //SqlDataAdapter sda = new SqlDataAdapter("SP_ExhibitionDetailForJisha", connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectForJisha", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", shop_ID);
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", int.Parse(str));
                //sda.SelectCommand.Parameters.AddWithValue("@strString", str);
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

        public void SaveLogExhibition(DataTable dt, string list, int shop_id)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();
                    SqlConnection conn = new SqlConnection(DataConfig.connectionString);
                    SqlCommand cmd = new SqlCommand("SP_Log_Exhibition_Insert_Painttool");
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

        public void ChangeFlag(string list, int shop_ID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DataConfig.connectionString);
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

        public DataTable SelectLogExhibitionItem(int shop_id, int mall_id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectByShop_JPainttool", connectionstring);
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

        public DataTable SelectByItemDataForYahoo(string itemIDList, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_ExhibitionDetailForYahoo", connectionString);
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

        public DataTable SelectByItemDataForYahoo(string itemIDList, string option, int Shop_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Log_Exhibition_Item_SelectForYahoo", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Exhibit_ID", Convert.ToInt32(itemIDList));
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

        public void ChangeStatus(string Item_Code, string Ctrl_ID, int Shop_ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Exhibition_Item_Shop_ChangeStatus", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_Code", Item_Code);
                cmd.Parameters.AddWithValue("@Ctrl_ID", Ctrl_ID);
                cmd.Parameters.AddWithValue("@Shop_ID", Shop_ID);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void SaveExhibitionItemShopExportedCSVInfo(int Exhibit_ID, int shopID, string csvName)
        // {
        //     try
        //     {
        //         SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
        //         //string sqlQuery = String.Format("UPDATE Exhibition_Item_Shop SET CSV_FileName = '{0}', CSV_CreatedDate = '{1}' WHERE Exhibit_Item_ID IN (SELECT max(ID) FROM Exhibition_Item_Master WHERE Item_ID = {2} ) AND Shop_ID = {3}", csvName, DateTime.Now.Date.ToString(), itemID, shopID);
        //         string sqlQuery = String.Format("UPDATE Exhibition_Item_Shop SET CSV_FileName = '{0}', CSV_CreatedDate = '{1}' WHERE Exhibit_Item_ID = {2} AND Shop_ID = {3}", csvName, DateTime.Now.Date.ToString(), Exhibit_ID, shopID);
        //         SqlCommand cmd = new SqlCommand(sqlQuery, connectionString);
        //         cmd.CommandType = CommandType.Text;
        //         cmd.CommandTimeout = 0;
        //         cmd.Connection.Open();
        //         cmd.ExecuteNonQuery();
        //         cmd.Connection.Close();
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception(ex.Message);
        //     }
        // }

        public void SaveExhibitionItemShopExportedCSVInfo(int Exhibit_ID, int shopID, string csvName, string ctrl_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                //string sqlQuery = String.Format("UPDATE Exhibition_Item_Shop SET CSV_FileName = '{0}', CSV_CreatedDate = '{1}' WHERE Exhibit_Item_ID IN (SELECT max(ID) FROM Exhibition_Item_Master WHERE Item_ID = {2} ) AND Shop_ID = {3}", csvName, DateTime.Now.Date.ToString(), itemID, shopID);
                //string sqlQuery = String.Format("UPDATE Exhibition_Item_Shop SET CSV_FileName = '{0}', CSV_CreatedDate = '{1}' , Ctrl_ID = '{4}' WHERE Exhibit_Item_ID = {2} AND Shop_ID = {3}", csvName, DateTime.Now.Date.ToString(), Exhibit_ID, shopID,ctrl_id);
                SqlCommand cmd = new SqlCommand("SP_Exhibition_Item_Shop_UpdateInfo", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Exhibit_ID", Exhibit_ID);
                cmd.Parameters.AddWithValue("@Shop_ID", shopID);
                cmd.Parameters.AddWithValue("@CSV_Name", csvName);
                cmd.Parameters.AddWithValue("@Ctrl_ID", ctrl_id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //for export
        public DataTable SelectExportdata(string listdata)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Exhibition_ExportData", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@list", listdata);

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


        public int Quick_Exhibition_List_Insert(string item_code, int user_id)
        {
            try
            {
                int eid;
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Quick_Exhibition_List_Insert", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_Code", item_code);
                cmd.Parameters.AddWithValue("@User_ID", user_id);
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                if (cmd.Parameters["@result"].Value != DBNull.Value)
                {
                    eid = Convert.ToInt32(cmd.Parameters["@result"].Value);
                }
                else
                {
                    eid = -1;
                }

                return eid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll_Not_Quick_Exhibition()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_SelectAll_Not_Quick_Exhibition", connectionString);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex) { throw ex; }


        }

        public DataTable CollectItem(string xml, int filetype)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_CollectItemCode", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@type", filetype);
                cmd.Parameters.Add("@Xml", SqlDbType.Xml).Value = xml;
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

        public DataTable CollectItem_Jisha(string xml, int filetype)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_CollectItemCode_Jisha", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@type", filetype);
                cmd.Parameters.Add("@Xml", SqlDbType.Xml).Value = xml;
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


        public DataTable CollectItemCode(string xml, int filetype)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_CollectItemCodePonpare", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@type", filetype);
                cmd.Parameters.Add("@Xml", SqlDbType.Xml).Value = xml;
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

        public DataTable CollectItemCode_Yahoo(string xml, int filetype)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_CollectItemCodeYahoo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@type", filetype);
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
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
    }
}
