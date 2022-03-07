/* 
Created By              : Eephyo
Created Date          : 18/07/2014
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
   public  class Item_Information_DL
    {

       public Item_Information_DL() { }

       public DataTable ItemSearch(string itemcode,string salecode,string competition_name,
                                                           string brand_name,string classname, string jancode,string season,string year)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmdSelect = new SqlCommand("SP_ItemInform_SearchbyText_data", connectionString);
                da.SelectCommand = cmdSelect;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                cmdSelect.Parameters.AddWithValue("@Item_Code", itemcode);
                cmdSelect.Parameters.AddWithValue("@salecode", salecode);
                cmdSelect.Parameters.AddWithValue("@Competition_Name", competition_name);
                cmdSelect.Parameters.AddWithValue("@Brand_name", brand_name);
                cmdSelect.Parameters.AddWithValue("@Class_Name", classname);
                cmdSelect.Parameters.AddWithValue("@jan_code", jancode);
                cmdSelect.Parameters.AddWithValue("@season", season);
                cmdSelect.Parameters.AddWithValue("@year",year);
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

       public DataTable ShowItemview()
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter();
               SqlCommand cmdSelect = new SqlCommand("SP_ItemInform_SearchAll", connectionString);
                da.SelectCommand.CommandTimeout = 0;             
               da.SelectCommand = cmdSelect;
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
             
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

       public DataTable SelectShop()
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlCommand cmd = new SqlCommand("SELECT Shop.Shop_Name,Item_Shop.Item_ID FROM Shop INNER JOIN Item_Shop ON Shop.ID=Item_Shop.Shop_ID", connectionString);
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               da.SelectCommand.CommandTimeout = 0;
               DataTable dt = new DataTable();
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

       public DataTable SelectShop(int  id) 
       {
           try 
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter("SP_ItemView2_SelectItemShop", connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.Parameters.AddWithValue("@itemid", id);
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

       public DataTable SearchItem(Item_Master_Entity ime) 
       {
           try {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter("SP_ItemView2_Search", connectionString);
               DataTable dt = new DataTable();
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               da.SelectCommand.CommandTimeout = 0;
               if (String.IsNullOrWhiteSpace(ime.Item_Code)) 
               {
                   da.SelectCommand.Parameters.AddWithValue("@itemno",DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@itemno", ime.Item_Code);

               if (String.IsNullOrWhiteSpace(ime.Brand_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@brandname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@brandname", ime.Brand_Name);

               if (String.IsNullOrWhiteSpace(ime.Catalog_Information))
               {
                   da.SelectCommand.Parameters.AddWithValue("@catinfo", DBNull.Value);
               }
               else
               da.SelectCommand.Parameters.AddWithValue("@catinfo", ime.Catalog_Information);

               da.SelectCommand.Parameters.AddWithValue("@skustatus",ime.Export_Status);

               if (String.IsNullOrWhiteSpace(ime.ProductName))
               {
                   da.SelectCommand.Parameters.AddWithValue("@productname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@productname",ime.ProductName);              
                if (String.IsNullOrWhiteSpace(ime.Product_Code))
                {
                   da.SelectCommand.Parameters.AddWithValue("@productcode", DBNull.Value);
                }
               else
               da.SelectCommand.Parameters.AddWithValue("@productcode",ime.Product_Code);

               if (String.IsNullOrWhiteSpace(ime.Company_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@companyname", DBNull.Value);
               }
               else
               da.SelectCommand.Parameters.AddWithValue("@companyname",ime.Company_Name);
               if (String.IsNullOrWhiteSpace(ime.Competition_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@competitionname", DBNull.Value);
               }
               else
               da.SelectCommand.Parameters.AddWithValue("@competitionname",ime.Competition_Name);

               if (String.IsNullOrWhiteSpace(ime.Class_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@classname", DBNull.Value);
               }
               else
               da.SelectCommand.Parameters.AddWithValue("@classname",ime.Class_Name);

               da.SelectCommand.Parameters.AddWithValue("@specialflag",ime.Special_Flag);

               da.SelectCommand.Parameters.AddWithValue("@reservationflag", ime.Reservation_Flag);

               if (String.IsNullOrWhiteSpace(ime.Year))
               {
                   da.SelectCommand.Parameters.AddWithValue("@year", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@year", ime.Year);

               if (String.IsNullOrWhiteSpace(ime.Season))
               {
                   da.SelectCommand.Parameters.AddWithValue("@season", DBNull.Value);
               }
               else
               da.SelectCommand.Parameters.AddWithValue("@season",ime.Season);

               if (String.IsNullOrWhiteSpace(ime.Ctrl_ID))
               {
                   da.SelectCommand.Parameters.AddWithValue("@shopstatus", DBNull.Value);
               }
               else
               da.SelectCommand.Parameters.AddWithValue("@shopstatus",ime.Ctrl_ID);

               if (String.IsNullOrWhiteSpace(ime.Remark))
               {
                   da.SelectCommand.Parameters.AddWithValue("@remark", DBNull.Value);
               }
               else
                da.SelectCommand.Parameters.AddWithValue("@remark",ime.Remark);

               if (String.IsNullOrWhiteSpace(ime.JanCode))
               {
                   da.SelectCommand.Parameters.AddWithValue("@jancode", DBNull.Value);
               }
               else
                da.SelectCommand.Parameters.AddWithValue("@jancode",ime.JanCode);

               if (String.IsNullOrWhiteSpace(ime.Sale_Code))
               {
                   da.SelectCommand.Parameters.AddWithValue("@salecode", DBNull.Value);
               }
               else
               da.SelectCommand.Parameters.AddWithValue("@salecode",ime.Sale_Code);

               if (String.IsNullOrWhiteSpace(ime.PersonInCharge))
               {
                   da.SelectCommand.Parameters.AddWithValue("@personincharge", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@personincharge", ime.PersonInCharge);
               if (String.IsNullOrWhiteSpace(ime.InstructionNo))
               {
                   da.SelectCommand.Parameters.AddWithValue("@instructionno", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@instructionno", ime.InstructionNo);

               da.SelectCommand.Parameters.AddWithValue("@dateofapproval", ime.FromDate);
               da.SelectCommand.Parameters.AddWithValue("@dateapp2", ime.ToDate);
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

       public DataTable SearchItem2(Item_Master_Entity ime, int pageIndex, int pageSize, int option,int search)
       {
           try
           {
               if (search == 1)
               {
                   SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                   SqlDataAdapter da = new SqlDataAdapter("SP_Item_View2_Search_Paging", connectionString);
                   DataTable dt = new DataTable();
                   da.SelectCommand.CommandType = CommandType.StoredProcedure;
                   da.SelectCommand.CommandTimeout = 0;
                   if (String.IsNullOrWhiteSpace(ime.Item_Code))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@itemno", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@itemno", ime.Item_Code);

                   if (String.IsNullOrWhiteSpace(ime.Brand_Name))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@brandname", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@brandname", ime.Brand_Name);

                   if (String.IsNullOrWhiteSpace(ime.Catalog_Information))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@catinfo", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@catinfo", ime.Catalog_Information);

                   da.SelectCommand.Parameters.AddWithValue("@skustatus", ime.Export_Status);

                   if (String.IsNullOrWhiteSpace(ime.ProductName))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@productname", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@productname", ime.ProductName);

                   if (String.IsNullOrWhiteSpace(ime.Product_Code))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@productcode", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@productcode", ime.Product_Code);

                   if (String.IsNullOrWhiteSpace(ime.Company_Name))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@companyname", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@companyname", ime.Company_Name);
                   if (String.IsNullOrWhiteSpace(ime.Competition_Name))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@competitionname", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@competitionname", ime.Competition_Name);

                   if (String.IsNullOrWhiteSpace(ime.Class_Name))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@classname", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@classname", ime.Class_Name);

                   da.SelectCommand.Parameters.AddWithValue("@specialflag", ime.Special_Flag);

                   da.SelectCommand.Parameters.AddWithValue("@reservationflag", ime.Reservation_Flag);

                   if (String.IsNullOrWhiteSpace(ime.Year))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@year", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@year", ime.Year);

                   if (String.IsNullOrWhiteSpace(ime.Season))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@season", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@season", ime.Season);

                   if (String.IsNullOrWhiteSpace(ime.Ctrl_ID))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@shopstatus", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@shopstatus", ime.Ctrl_ID);

                   if (String.IsNullOrWhiteSpace(ime.Remark))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@remark", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@remark", ime.Remark);

                   if (String.IsNullOrWhiteSpace(ime.JanCode))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@jancode", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@jancode", ime.JanCode);

                   if (String.IsNullOrWhiteSpace(ime.Sale_Code))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@salecode", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@salecode", ime.Sale_Code);

                   if (String.IsNullOrWhiteSpace(ime.PersonInCharge))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@personincharge", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@personincharge", ime.PersonInCharge);
                   if (String.IsNullOrWhiteSpace(ime.InstructionNo))
                   {
                       da.SelectCommand.Parameters.AddWithValue("@instructionno", DBNull.Value);
                   }
                   else
                       da.SelectCommand.Parameters.AddWithValue("@instructionno", ime.InstructionNo);

                   da.SelectCommand.Parameters.AddWithValue("@dateofapproval", ime.FromDate);
                   da.SelectCommand.Parameters.AddWithValue("@dateapp2", ime.ToDate);
                   da.SelectCommand.Parameters.AddWithValue("@PageIndex ", pageIndex);
                   da.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                   da.SelectCommand.Parameters.AddWithValue("@Option", option);
                   da.SelectCommand.Connection.Open();
                   da.Fill(dt);
                   da.SelectCommand.Connection.Close();
                   return dt;
               }
               else
               {
                   SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                   SqlDataAdapter da = new SqlDataAdapter("SP_Item_View2_Select", connectionString);
                   DataTable dt = new DataTable();
                   da.SelectCommand.CommandType = CommandType.StoredProcedure;
                   da.SelectCommand.CommandTimeout = 0;
                   da.SelectCommand.Parameters.AddWithValue("@PageIndex ", pageIndex);
                   da.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                   da.SelectCommand.Parameters.AddWithValue("@Option", option);
                   da.SelectCommand.Connection.Open();
                   da.Fill(dt);
                   da.SelectCommand.Connection.Close();
                   return dt;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public DataTable ItemView2_Search(Item_Master_Entity ime, int pageIndex, int pageSize, int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter("Item_View2_Search_New", connectionString);
               DataTable dt = new DataTable();
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               if (String.IsNullOrWhiteSpace(ime.Item_Code))
               {
                   da.SelectCommand.Parameters.AddWithValue("@itemno", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@itemno", ime.Item_Code);

               if (String.IsNullOrWhiteSpace(ime.Brand_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@brandname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@brandname", ime.Brand_Name);

               if (String.IsNullOrWhiteSpace(ime.Catalog_Information))
               {
                   da.SelectCommand.Parameters.AddWithValue("@catinfo", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@catinfo", ime.Catalog_Information);

               da.SelectCommand.Parameters.AddWithValue("@skustatus", ime.Export_Status);

               if (String.IsNullOrWhiteSpace(ime.ProductName))
               {
                   da.SelectCommand.Parameters.AddWithValue("@productname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@productname", ime.ProductName);

               if (String.IsNullOrWhiteSpace(ime.Product_Code))
               {
                   da.SelectCommand.Parameters.AddWithValue("@productcode", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@productcode", ime.Product_Code);

               if (String.IsNullOrWhiteSpace(ime.Company_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@companyname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@companyname", ime.Company_Name);
               if (String.IsNullOrWhiteSpace(ime.Competition_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@competitionname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@competitionname", ime.Competition_Name);

               if (String.IsNullOrWhiteSpace(ime.Class_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@classname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@classname", ime.Class_Name);


               da.SelectCommand.Parameters.AddWithValue("@specialflag", ime.Special_Flag);

               da.SelectCommand.Parameters.AddWithValue("@reservationflag", ime.Reservation_Flag);

               if (String.IsNullOrWhiteSpace(ime.Year))
               {
                   da.SelectCommand.Parameters.AddWithValue("@year", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@year", ime.Year);

               if (String.IsNullOrWhiteSpace(ime.Season))
               {
                   da.SelectCommand.Parameters.AddWithValue("@season", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@season", ime.Season);

               if (String.IsNullOrWhiteSpace(ime.Ctrl_ID))
               {
                   da.SelectCommand.Parameters.AddWithValue("@shopstatus", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@shopstatus", ime.Ctrl_ID);

               if (String.IsNullOrWhiteSpace(ime.Remark))
               {
                   da.SelectCommand.Parameters.AddWithValue("@remark", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@remark", ime.Remark);

               if (String.IsNullOrWhiteSpace(ime.JanCode))
               {
                   da.SelectCommand.Parameters.AddWithValue("@jancode", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@jancode", ime.JanCode);

               if (String.IsNullOrWhiteSpace(ime.Sale_Code))
               {
                   da.SelectCommand.Parameters.AddWithValue("@salecode", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@salecode", ime.Sale_Code);

               if (String.IsNullOrWhiteSpace(ime.PersonInCharge))
               {
                   da.SelectCommand.Parameters.AddWithValue("@personincharge", -1);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@personincharge", ime.PersonInCharge);
               if (String.IsNullOrWhiteSpace(ime.InstructionNo))
               {
                   da.SelectCommand.Parameters.AddWithValue("@instructionno", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@instructionno", ime.InstructionNo);
               da.SelectCommand.Parameters.AddWithValue("@Price", ime.Price);
               da.SelectCommand.Parameters.AddWithValue("@dateofapproval", ime.FromDate);
               da.SelectCommand.Parameters.AddWithValue("@dateapp2", ime.ToDate);
               da.SelectCommand.Parameters.AddWithValue("@PageIndex ", pageIndex);
               da.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
               da.SelectCommand.Parameters.AddWithValue("@Option", option);
               da.SelectCommand.Parameters.AddWithValue("@ShopID", ime.ShopID);
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
       /*
       public DataTable ItemView2_PageLoad(Item_Master_Entity ime, int pageIndex, int pageSize, int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter("Item_View2_PageLoad", connectionString);
               DataTable dt = new DataTable();
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               if (String.IsNullOrWhiteSpace(ime.Item_Code))
               {
                   da.SelectCommand.Parameters.AddWithValue("@itemno", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@itemno", ime.Item_Code);

               if (String.IsNullOrWhiteSpace(ime.Brand_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@brandname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@brandname", ime.Brand_Name);

               if (String.IsNullOrWhiteSpace(ime.Catalog_Information))
               {
                   da.SelectCommand.Parameters.AddWithValue("@catinfo", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@catinfo", ime.Catalog_Information);

               da.SelectCommand.Parameters.AddWithValue("@skustatus", ime.Export_Status);

               if (String.IsNullOrWhiteSpace(ime.ProductName))
               {
                   da.SelectCommand.Parameters.AddWithValue("@productname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@productname", ime.ProductName);

               if (String.IsNullOrWhiteSpace(ime.Product_Code))
               {
                   da.SelectCommand.Parameters.AddWithValue("@productcode", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@productcode", ime.Product_Code);

               if (String.IsNullOrWhiteSpace(ime.Company_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@companyname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@companyname", ime.Company_Name);
               if (String.IsNullOrWhiteSpace(ime.Competition_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@competitionname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@competitionname", ime.Competition_Name);

               if (String.IsNullOrWhiteSpace(ime.Class_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@classname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@classname", ime.Class_Name);


               da.SelectCommand.Parameters.AddWithValue("@specialflag", ime.Special_Flag);

               da.SelectCommand.Parameters.AddWithValue("@reservationflag", ime.Reservation_Flag);

               if (String.IsNullOrWhiteSpace(ime.Year))
               {
                   da.SelectCommand.Parameters.AddWithValue("@year", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@year", ime.Year);

               if (String.IsNullOrWhiteSpace(ime.Season))
               {
                   da.SelectCommand.Parameters.AddWithValue("@season", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@season", ime.Season);

               if (String.IsNullOrWhiteSpace(ime.Ctrl_ID))
               {
                   da.SelectCommand.Parameters.AddWithValue("@shopstatus", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@shopstatus", ime.Ctrl_ID);

               if (String.IsNullOrWhiteSpace(ime.Remark))
               {
                   da.SelectCommand.Parameters.AddWithValue("@remark", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@remark", ime.Remark);

               if (String.IsNullOrWhiteSpace(ime.JanCode))
               {
                   da.SelectCommand.Parameters.AddWithValue("@jancode", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@jancode", ime.JanCode);

               if (String.IsNullOrWhiteSpace(ime.Sale_Code))
               {
                   da.SelectCommand.Parameters.AddWithValue("@salecode", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@salecode", ime.Sale_Code);

               if (String.IsNullOrWhiteSpace(ime.PersonInCharge))
               {
                   da.SelectCommand.Parameters.AddWithValue("@personincharge", -1);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@personincharge", ime.PersonInCharge);
               if (String.IsNullOrWhiteSpace(ime.InstructionNo))
               {
                   da.SelectCommand.Parameters.AddWithValue("@instructionno", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@instructionno", ime.InstructionNo);

               da.SelectCommand.Parameters.AddWithValue("@dateofapproval", ime.FromDate);
               da.SelectCommand.Parameters.AddWithValue("@dateapp2", ime.ToDate);
               da.SelectCommand.Parameters.AddWithValue("@PageIndex ", pageIndex);
               da.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
               da.SelectCommand.Parameters.AddWithValue("@Option", option);
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
       */
       public DataTable LikeSearchItem(Item_Master_Entity ime, int pageIndex, int pageSize, int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter da = new SqlDataAdapter("SP_Item_View2_LikeSearch", connectionString);
               DataTable dt = new DataTable();
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.CommandTimeout = 0;
               if (String.IsNullOrWhiteSpace(ime.Item_Code))
               {
                   da.SelectCommand.Parameters.AddWithValue("@itemno", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@itemno", ime.Item_Code);

               if (String.IsNullOrWhiteSpace(ime.Brand_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@brandname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@brandname", ime.Brand_Name);

               if (String.IsNullOrWhiteSpace(ime.Catalog_Information))
               {
                   da.SelectCommand.Parameters.AddWithValue("@catinfo", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@catinfo", ime.Catalog_Information);

               da.SelectCommand.Parameters.AddWithValue("@skustatus", ime.Export_Status);

               if (String.IsNullOrWhiteSpace(ime.ProductName))
               {
                   da.SelectCommand.Parameters.AddWithValue("@productname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@productname", ime.ProductName);

               if (String.IsNullOrWhiteSpace(ime.Product_Code))
               {
                   da.SelectCommand.Parameters.AddWithValue("@productcode", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@productcode", ime.Product_Code);

               if (String.IsNullOrWhiteSpace(ime.Company_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@companyname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@companyname", ime.Company_Name);
               if (String.IsNullOrWhiteSpace(ime.Competition_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@competitionname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@competitionname", ime.Competition_Name);

               if (String.IsNullOrWhiteSpace(ime.Class_Name))
               {
                   da.SelectCommand.Parameters.AddWithValue("@classname", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@classname", ime.Class_Name);

               da.SelectCommand.Parameters.AddWithValue("@specialflag", ime.Special_Flag);

               da.SelectCommand.Parameters.AddWithValue("@reservationflag", ime.Reservation_Flag);

               if (String.IsNullOrWhiteSpace(ime.Year))
               {
                   da.SelectCommand.Parameters.AddWithValue("@year", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@year", ime.Year);

               if (String.IsNullOrWhiteSpace(ime.Season))
               {
                   da.SelectCommand.Parameters.AddWithValue("@season", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@season", ime.Season);

               if (String.IsNullOrWhiteSpace(ime.Ctrl_ID))
               {
                   da.SelectCommand.Parameters.AddWithValue("@shopstatus", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@shopstatus", ime.Ctrl_ID);

               if (String.IsNullOrWhiteSpace(ime.Remark))
               {
                   da.SelectCommand.Parameters.AddWithValue("@remark", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@remark", ime.Remark);

               if (String.IsNullOrWhiteSpace(ime.JanCode))
               {
                   da.SelectCommand.Parameters.AddWithValue("@jancode", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@jancode", ime.JanCode);

               if (String.IsNullOrWhiteSpace(ime.Sale_Code))
               {
                   da.SelectCommand.Parameters.AddWithValue("@salecode", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@salecode", ime.Sale_Code);

               if (String.IsNullOrWhiteSpace(ime.PersonInCharge))
               {
                   da.SelectCommand.Parameters.AddWithValue("@personincharge", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@personincharge", ime.PersonInCharge);
               if (String.IsNullOrWhiteSpace(ime.InstructionNo))
               {
                   da.SelectCommand.Parameters.AddWithValue("@instructionno", DBNull.Value);
               }
               else
                   da.SelectCommand.Parameters.AddWithValue("@instructionno", ime.InstructionNo);
                //if (String.IsNullOrWhiteSpace(ime.InstructionNo))
                //{
                //    da.SelectCommand.Parameters.AddWithValue("@instructionno", DBNull.Value);
                //}
                //else
                //    da.SelectCommand.Parameters.AddWithValue("@instructionno", ime.InstructionNo);
                //if (String.IsNullOrWhiteSpace(ime.InstructionNo))
                //{
                //    da.SelectCommand.Parameters.AddWithValue("@instructionno", DBNull.Value);
                //}
                //else
                //    da.SelectCommand.Parameters.AddWithValue("@instructionno", ime.InstructionNo);
                da.SelectCommand.Parameters.AddWithValue("@dateofapproval", ime.FromDate);
               da.SelectCommand.Parameters.AddWithValue("@dateapp2", ime.ToDate);
               da.SelectCommand.Parameters.AddWithValue("@PageIndex ", pageIndex);
               da.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
               da.SelectCommand.Parameters.AddWithValue("@Option", option);
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

       public DataTable SearchItem_View2_Data(string search, string item, int pageindex, int pagesize, out int totalrowcount)
       {
           DataSet ds = new DataSet();
           try
           {
               SqlConnection con = new SqlConnection(DataConfig.connectionString);
               SqlCommand cmd = new SqlCommand("SP_SearchItem_View2_Data", con);
               cmd.CommandType = CommandType.StoredProcedure;
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               if (pageindex == 0)
               {
                   pageindex = 1;
               }
               else
               {
                   pageindex = (pageindex / pagesize);
               }
               cmd.Parameters.AddWithValue("@search", search);
               if (!string.IsNullOrWhiteSpace(item))
                   cmd.Parameters.AddWithValue("@item", item);
               else
                   cmd.Parameters.AddWithValue("@item", DBNull.Value);
               cmd.Parameters.AddWithValue("@pageindex", pageindex);
               cmd.Parameters.AddWithValue("@pagesize", pagesize);
               cmd.Connection.Open();
               da.Fill(ds);
               totalrowcount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
               cmd.Connection.Close();
               return ds.Tables[0];
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        public String Get_Lot_Number(string Item_Code)
        {
            try
            {

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Get_LotNumber", connectionString);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.ExecuteNonQuery();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();
                return dt.Rows[0]["Wowma_lotNumber"].ToString();
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
