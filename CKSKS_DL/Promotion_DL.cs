using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_Common;
using System.Data.SqlClient;
using System.Data;

namespace ORS_RCM_DL
{
    public class Promotion_DL
    {
        

        public Promotion_DL() { }

        public DataTable PromotionSearch(Campaign_Entity ce, int pgindex, String psize,int option)
        {
            SqlConnection con = new SqlConnection(DataConfig.connectionString);
            try
            {
                String storeProcedureName = String.Empty;
                if (option == 1)
                    storeProcedureName = "SP_CampaignPromotion_EqualSearch";
                else storeProcedureName = "SP_CampaignPromotion_LikeSearch";
                SqlCommand cmd = new SqlCommand(storeProcedureName, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                if(!String.IsNullOrWhiteSpace(ce.Campaign_ID))
                    cmd.Parameters.AddWithValue("@campaignID", ce.Campaign_ID);
                else cmd.Parameters.AddWithValue("@campaignID", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ce.Promotion_Name))
                    cmd.Parameters.AddWithValue("@campaignName", ce.Promotion_Name);
                else cmd.Parameters.AddWithValue("@campaignName", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ce.Campaign_Guideline))
                    cmd.Parameters.AddWithValue("@campaignType", ce.Campaign_Guideline);
                else cmd.Parameters.AddWithValue("@campaignType", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ce.Shop_Name))
                    cmd.Parameters.AddWithValue("@campaignShop", ce.Shop_Name);
                else cmd.Parameters.AddWithValue("@campaignShop", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ce.Subjects))
                    cmd.Parameters.AddWithValue("@subject", ce.Subjects);
                else cmd.Parameters.AddWithValue("@subject", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ce.Target_Brand))
                    cmd.Parameters.AddWithValue("@targetBrand", ce.Target_Brand);
                else cmd.Parameters.AddWithValue("@targetBrand", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ce.Instruction_No))
                    cmd.Parameters.AddWithValue("@instructionNo", ce.Instruction_No);
                else cmd.Parameters.AddWithValue("@instructionNo", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ce.Priority))
                    cmd.Parameters.AddWithValue("@priority", ce.Priority);
                else cmd.Parameters.AddWithValue("@priority", DBNull.Value);

                if (!String.IsNullOrWhiteSpace(ce.Remark))
                    cmd.Parameters.AddWithValue("@remark", ce.Remark);
                else cmd.Parameters.AddWithValue("@remark", DBNull.Value);

                cmd.Parameters.AddWithValue("@isPublic", ce.IsPublic);
                cmd.Parameters.AddWithValue("@isPresent", ce.IsPresent);

                if (!String.IsNullOrWhiteSpace(ce.Item))
                    cmd.Parameters.AddWithValue("@itemCode", ce.Item);
                else cmd.Parameters.AddWithValue("@itemCode", DBNull.Value);

                cmd.Parameters.AddWithValue("@startDate", ce.Period_From);
                cmd.Parameters.AddWithValue("@endDate", ce.Period_To);
                cmd.Parameters.AddWithValue("@PageIndex", pgindex);
                cmd.Parameters.AddWithValue("@PageSize", psize);
                cmd.Connection.Open();
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int SaveUpdate(Campaign_Entity pe , string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_Promotion_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@ID", pe.ID);
                //cmd.Parameters.AddWithValue("@Promotion_Name", pe.Promotion_Name);
                cmd.Parameters.AddWithValue("@campaign_name", pe.Promotion_Name);
                cmd.Parameters.AddWithValue("@campaignID", pe.Campaign_ID);
                cmd.Parameters.AddWithValue("@Campaign_Guideline", pe.Campaign_Guideline);
                //cmd.Parameters.AddWithValue("@Brand_Name", pe.Brand_Name);
                cmd.Parameters.AddWithValue("@Period_From", pe.Period_From);
                cmd.Parameters.AddWithValue("@Period_To", pe.Period_To);
                cmd.Parameters.AddWithValue("@Period_StartTime", pe.Period_StartTime);
                cmd.Parameters.AddWithValue("@Period_EndTime", pe.Period_EndTime);
                cmd.Parameters.AddWithValue("@Campaign_TypeID", pe.Campaign_TypeID);
                cmd.Parameters.AddWithValue("@Product_Decoration", pe.Product_Decoration);
                cmd.Parameters.AddWithValue("@PC_Copy_Decoration", pe.PC_Copy_Decoration);
                cmd.Parameters.AddWithValue("@Smart_Copy_Decoration", pe.Smart_Copy_Decoration);
                cmd.Parameters.AddWithValue("@Secret_ID", pe.Secret_ID);
                cmd.Parameters.AddWithValue("@Secret_Password", pe.Secret_Password);
                cmd.Parameters.AddWithValue("@Product_DescriptionX", pe.PC_Campaign1);
                cmd.Parameters.AddWithValue("@Product_DescriptionY", pe.PC_Campaign2);
                cmd.Parameters.AddWithValue("@Sale_DescriptionX", pe.Smart_Campaign1);
                cmd.Parameters.AddWithValue("@Sale_DescriptionY", pe.Smart_Campaign2);
                cmd.Parameters.AddWithValue("@Priority", pe.Priority);
                cmd.Parameters.AddWithValue("@Status", pe.Status);
                cmd.Parameters.AddWithValue("@IsPromotionClose", pe.IsPromotionClose);
                cmd.Parameters.AddWithValue("@Created_Date", System.DateTime.Now);
                cmd.Parameters.AddWithValue("@Updated_Date", System.DateTime.Now);
                //cmd.Parameters.AddWithValue("@Product_Decoration", pe.Product_Decoration);
                //cmd.Parameters.AddWithValue("@Copy_Decoration", pe.Copy_Decoration);
                cmd.Parameters.AddWithValue("@Option", option);

            

                cmd.Parameters.AddWithValue("@campaignurl_pc", pe.CampaignUrl_PC);

                cmd.Parameters.AddWithValue("@campaignurl_smart", pe.CampaignUrl_Smart);

                cmd.Parameters.AddWithValue("@remark", pe.Remark);

                cmd.Parameters.AddWithValue("@production_detail", pe.Production_Detail);

                cmd.Parameters.AddWithValue("@subjects", pe.Subjects);

                cmd.Parameters.AddWithValue("@target_brand", pe.Target_Brand);


                cmd.Parameters.AddWithValue("@instruction_no", pe.Instruction_No);


                cmd.Parameters.AddWithValue("@application_method", pe.Application_Method);



                cmd.Parameters.AddWithValue("@gift_content", pe.Present_Contents);
                cmd.Parameters.AddWithValue("@gift_way", pe.Present_Method);


                cmd.Parameters.AddWithValue("@production_target", pe.Production_Target);


                cmd.Parameters.AddWithValue("@isgift", pe.IsPresent);


                cmd.Parameters.AddWithValue("@ispublic",pe.IsPublic);

                cmd.Parameters.AddWithValue("@blackmarket", pe.Black_market_Setting);


                cmd.Parameters.AddWithValue("@Mail_Magazine_Event1", pe.Mail_Magazine_Event1);

                cmd.Parameters.AddWithValue("@Mail_Magazine_Event2", pe.Mail_Magazine_Event2);

                cmd.Parameters.AddWithValue("@Mail_Magazine_Event3", pe.Mail_Magazine_Event3);



                cmd.Parameters.AddWithValue("@Campaign_Image1",pe.Campaign_Image1 );
                cmd.Parameters.AddWithValue("@Campaign_Image2", pe.Campaign_Image2);
                cmd.Parameters.AddWithValue("@Campaign_Image3", pe.Campaign_Image3);
                cmd.Parameters.AddWithValue("@Campaign_Image4",pe.Campaign_Image4);
                cmd.Parameters.AddWithValue("@Campaign_Image5",pe.Campaign_Image5);

    
                 cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable SelectAll()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "Select *,ROW_NUMBER() OVER (ORDER BY ID DESC) AS 'No' from Promotion WITH (NOLOCK)";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
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
        public bool Check_CampaignIDexisted(string ID)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                SqlConnection connection = new SqlConnection(DataConfig.connectionString);
                cmd.CommandText = "SELECT count (Campaign_ID) from Promotion where  Campaign_ID=N" + "'" + ID + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Connection.Open();
                int count = (int)cmd.ExecuteScalar();
                cmd.Connection.Close();

                if (count > 0)
                {
                    return true;  //Already Exist in Promotion table
                }
                else
                {
                    return false; //Not exist in Promotion  table
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CampaignID_alreadyUpdate(string CampaignID, int? ID)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SP_Duplicate_Campaign_ID_Check", connectionString);
            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                sda.SelectCommand.Parameters.AddWithValue("@Campaign_ID", CampaignID);
                sda.SelectCommand.Parameters.AddWithValue("@ID", ID);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                return dt;
                
           }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                sda.SelectCommand.Connection.Close();
                sda.Dispose();
            }
        }
        

        public bool Insert(DataTable dt)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("Campaign_Image_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Parameters.Add("@Campaign_Image1 ", SqlDbType.NVarChar, 50, "Image_Name");
                cmd.Parameters.Add("@Campaign_Image2 ", SqlDbType.NVarChar, 50, "Image_Name");
                cmd.Parameters.Add("@Campaign_Image3 ", SqlDbType.NVarChar, 50, "Image_Name");
                cmd.Parameters.Add("@Campaign_Image4", SqlDbType.NVarChar, 50, "Image_Name");
                cmd.Parameters.Add("@Campaign_Image5", SqlDbType.NVarChar, 50, "Image_Name");
                sda.InsertCommand = cmd;
                sda.UpdateCommand = cmd;
                cmd.Connection.Open();
                sda.Update(dt);
                cmd.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public DataTable SelectForAddCSV(int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string query = "SP_CampaignCSV_Selectdata";
                //string query = "Select *,ROW_NUMBER() OVER (ORDER BY ID) AS 'No' from Promotion WHERE Period_From > GETDATE() AND Export_Status=0";
                //string query = "SELECT Distinct Promotion.*,Promotion_Shop.Shop_ID "
                //    + "FROM Promotion INNER JOIN Promotion_Shop ON Promotion.ID = Promotion_Shop.Promotion_ID "
                //    + "WHERE Period_From > GETDATE()";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@option",option);
                
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

        public DataTable SelectForRemoveCSV()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string query = "Select *,ROW_NUMBER() OVER (ORDER BY ID) AS 'No' from Promotion WHERE Period_To < GETDATE() AND Export_Status=0";
                //string query = "SELECT Promotion.*,Promotion_Shop.Shop_ID "
                //    + "FROM Promotion INNER JOIN Promotion_Shop ON Promotion.ID = Promotion_Shop.Promotion_ID "
                //    + "WHERE Period_From < GETDATE()";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
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

        public DataTable GetShopNamesByID(int ID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "Select Shop.Shop_Code,Shop.Shop_ID,Shop.Mall_ID,Shop.Shop_Name,Promotion.*,ROW_NUMBER() OVER (ORDER BY Promotion.ID) AS 'No' from Promotion WITH (NOLOCK)"
                    + "INNER JOIN Promotion_Shop WITH (NOLOCK) ON Promotion.ID = Promotion_Shop.Promotion_ID "
                        + "INNER JOIN Shop WITH (NOLOCK) ON Promotion_Shop.Shop_ID = Shop.ID "
                        + "WHERE Promotion.ID = " + ID;
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.Text;

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

        public Campaign_Entity SelectByID(int pid)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string quary = "SELECT * FROM Promotion WITH (NOLOCK) WHERE ID=@ID";
                SqlDataAdapter sda = new SqlDataAdapter(quary, connectionString);
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.Parameters.AddWithValue("@ID", pid);
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                Campaign_Entity pe = new Campaign_Entity();
                if (dt != null && dt.Rows.Count > 0)
                {
                    pe.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    pe.Promotion_Name = dt.Rows[0]["Promotion_Name"].ToString();
                    pe.Campaign_Guideline = dt.Rows[0]["Campaign_Guideline"].ToString();

                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Campaign_ID"].ToString()))
                    {
                    pe.Campaign_ID = dt.Rows[0]["Campaign_ID"].ToString();
                    }
                    pe.CampaignUrl_PC = dt.Rows[0]["CampaignUrl_PC"].ToString();

                    pe.CampaignUrl_Smart = dt.Rows[0]["CampaignUrl_Smart"].ToString();

                    pe.Remark = dt.Rows[0]["Remark"].ToString();


                    pe.Production_Detail = dt.Rows[0]["Production_Detail"].ToString();


                    pe.Subjects = dt.Rows[0]["Subjects"].ToString();


                    pe.Instruction_No = dt.Rows[0]["Instruction_No"].ToString();



                    pe.Application_Method = dt.Rows[0]["Application_Method"].ToString();


                    pe.Present_Contents = dt.Rows[0]["Present_Contents"].ToString();


                    pe.Present_Method = dt.Rows[0]["Present_Method"].ToString();


                    if (dt.Rows[0]["IsPresent"].ToString().Equals("False"))
                    {

                        pe.IsPresent = false;

                    }

                    else
                    {
                        pe.IsPresent = true;

                    }


                    pe.Production_Target = dt.Rows[0]["Production_Target"].ToString();
                                 
                    pe.Related_Info_Ref = dt.Rows[0]["Related_Info_Ref"].ToString();

                    pe.Product_Decoration = dt.Rows[0]["Product_Decoration"].ToString();
                    
                    pe.PC_Copy_Decoration = dt.Rows[0]["PC_Copy_Decoration"].ToString();
                    
                    pe.Campaign_Image1 = dt.Rows[0]["Campaign_Image1"].ToString();

                     pe.Campaign_Image2 = dt.Rows[0]["Campaign_Image2"].ToString();
                    
                     pe.Campaign_Image3 = dt.Rows[0]["Campaign_Image3"].ToString();


                    pe.Campaign_Image4 = dt.Rows[0]["Campaign_Image4"].ToString();


                    pe.Campaign_Image5 = dt.Rows[0]["Campaign_Image5"].ToString();
                    
                    

                    if (dt.Rows[0]["IsPublic"].ToString().Equals("False"))

                    {

                        pe.IsPublic = false;
                    }

                    else
                    {
                        pe.IsPublic = true;

                    }

                    if (dt.Rows[0]["Black_market_Setting"].ToString().Equals("False"))
                    {
                        pe.Black_market_Setting =false ;
                    }

                    else
                    {
                        pe.Black_market_Setting = true;
                        
                    }

                    pe.Smart_Copy_Decoration = dt.Rows[0]["Smart_Copy_Decoration"].ToString();

                    pe.Target_Brand = dt.Rows[0]["Target_Brand"].ToString();
                    if (DBNull.Value != dt.Rows[0]["Period_From"])
                        pe.Period_From = Convert.ToDateTime(dt.Rows[0]["Period_From"]);
                    if (DBNull.Value != dt.Rows[0]["Period_To"])
                        pe.Period_To = Convert.ToDateTime(dt.Rows[0]["Period_To"]);
                    pe.Period_StartTime = dt.Rows[0]["Period_StartTime"].ToString();
                    pe.Period_EndTime = dt.Rows[0]["Period_EndTime"].ToString();

                    if (!String.IsNullOrEmpty(dt.Rows[0]["Campaign_TypeID"].ToString()))
                    {
                        pe.Campaign_TypeID = dt.Rows[0]["Campaign_TypeID"].ToString();
                    }
                    //pe.Rakuten_MagnificationID = Convert.ToInt32(dt.Rows[0]["Rakuten_MagnificationID"]);
                    //pe.Yahoo_MagnificationID = Convert.ToInt32(dt.Rows[0]["Yahoo_MagnificationID"]);
                    //pe.Ponpare_MagnificationID = Convert.ToInt32(dt.Rows[0]["Ponpare_MagnificationID"]);
                    pe.Secret_ID = dt.Rows[0]["Secret_ID"].ToString();
                    pe.Secret_Password = dt.Rows[0]["Secret_Password"].ToString();
                    pe.PC_Campaign1 = dt.Rows[0]["PC_Campaign1"].ToString();
                    pe.PC_Campaign2 = dt.Rows[0]["PC_Campaign2"].ToString();
                    pe.Smart_Campaign1 = dt.Rows[0]["Smart_Campaign1"].ToString();
                    pe.Smart_Campaign2 = dt.Rows[0]["Smart_Campaign2"].ToString();
                    pe.Priority = dt.Rows[0]["Priority"].ToString();
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Status"].ToString()))
                        pe.Status = Convert.ToInt32(dt.Rows[0]["Status"]);
                    else pe.Status = 0;
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["IsPromotionClose"].ToString()))
                        pe.IsPromotionClose = Convert.ToInt32(dt.Rows[0]["IsPromotionClose"]);
                    else pe.IsPromotionClose = 0;
                    //pe.Product_Decoration = dt.Rows[0]["Product_Decoration"].ToString();
                    //pe.Copy_Decoration = dt.Rows[0]["Copy_Decoration"].ToString();




                    pe.Mail_Magazine_Event1 = dt.Rows[0]["Mail_Magazine_Event1"].ToString();

                    pe.Mail_Magazine_Event2 = dt.Rows[0]["Mail_Magazine_Event2"].ToString();

                    pe.Mail_Magazine_Event3 = dt.Rows[0]["Mail_Magazine_Event3"].ToString();
                }
                return pe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///Temporay Commented by EP
        //public DataTable SearchPromotion(string promotionName, string shopName, string CampaingeID,string Campaing_TypeID, string Target_Brand, int? Holding_Period, string status)
        //{
        //    try
        //    {
        //        if (Campaing_TypeID == "")
        //        {
        //            Campaing_TypeID = null;
        //        }
        //        //int stname = Convert.ToInt32(shopName);
        //        SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
        //        SqlDataAdapter da = new SqlDataAdapter("SP_Promotion_Search", connectionString);
        //        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        da.SelectCommand.CommandTimeout = 0;
        //        DataTable dt = new DataTable();
        //        da.SelectCommand.Parameters.AddWithValue("@Promotion_Name", promotionName);
        //        //if (shopName == "ショップ選択")
        //        //da.SelectCommand.Parameters.AddWithValue("@Shop_Name", "");
        //        //else
        //        da.SelectCommand.Parameters.AddWithValue("@Shop_Name", shopName);
        //        da.SelectCommand.Parameters.AddWithValue("@Campaing_TypeID",Campaing_TypeID);
        //        da.SelectCommand.Parameters.AddWithValue("@Campaing_TypeID", Campaing_TypeID);
        //        da.SelectCommand.Parameters.AddWithValue("@target_brand", Target_Brand);
        //        da.SelectCommand.Parameters.AddWithValue("@Holding_Period", Holding_Period);

        //        da.SelectCommand.Connection.Open();
        //        da.Fill(dt);
        //        da.SelectCommand.Connection.Close();

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

       //public DataTable Search(string CampaignID,string promotionName, string shopName, string Campaing_TypeID, string Target_Brand,int? holding_period)

        public DataTable Search(string CampaignID,string promotionName,String shopName,string Target_Brand, DateTime? repeatFrom, DateTime? repeatTo, string instructionNo,string campaignTypeID,string priority,string remark,string subject,string item_code,string option1,string option2)
       
       {
            try
            {
                //if (Campaing_TypeID == "")
                //{
                //    Campaing_TypeID = null;
                //}
                //int stname = Convert.ToInt32(shopName);
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_PromotionView_Search", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();

                if ((!String.IsNullOrWhiteSpace(CampaignID)))
                {

                    da.SelectCommand.Parameters.AddWithValue("@Campaign_ID", CampaignID);

                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Campaign_ID", DBNull.Value);

                }


                if ((!String.IsNullOrWhiteSpace(promotionName)) && (promotionName!=""))
                {

                    da.SelectCommand.Parameters.AddWithValue("@Promotion_Name", promotionName);

                }

                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Promotion_Name", DBNull.Value);
                }

                if ((!String.IsNullOrWhiteSpace(shopName)) && (shopName!=""))
                {
            
                    da.SelectCommand.Parameters.AddWithValue("@Shop_ID", shopName);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Shop_ID", DBNull.Value);
                }

                if ((!String.IsNullOrWhiteSpace(campaignTypeID)) && (campaignTypeID!=""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Campaing_TypeID", campaignTypeID);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Campaing_TypeID", DBNull.Value);
                }


                if ((!String.IsNullOrWhiteSpace(Target_Brand)) && (Target_Brand!=""))
                {
                da.SelectCommand.Parameters.AddWithValue("@Brand_Name", Target_Brand);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Brand_Name", DBNull.Value);
                }

                if (repeatFrom!=null)
                {
                    da.SelectCommand.Parameters.AddWithValue("@repeatfrom", repeatFrom);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@repeatfrom", DBNull.Value);
                }
                if (repeatTo!= null)
                {
                    da.SelectCommand.Parameters.AddWithValue("@repeatto", repeatTo);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@repeatto", DBNull.Value);
                }

                if((!String.IsNullOrWhiteSpace(instructionNo))&&(instructionNo!=""))
                {
                da.SelectCommand.Parameters.AddWithValue("@InstructionNo", instructionNo);
                }
                else
                {
                       da.SelectCommand.Parameters.AddWithValue("@InstructionNo",DBNull.Value);
                }


                if ((!String.IsNullOrWhiteSpace(priority)) && (priority!=""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@priority", priority);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@priority", DBNull.Value);
                }



                if ((!String.IsNullOrWhiteSpace(remark)) && (remark!=""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@remark", remark);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@remark", DBNull.Value);
                }



                if ((!String.IsNullOrWhiteSpace(subject)) && (subject!=""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@subject", subject);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@subject", DBNull.Value);
                }




                if ((!String.IsNullOrWhiteSpace(item_code)) && (item_code!= ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@item_code", item_code);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@item_code", DBNull.Value);
                }

                if ((!String.IsNullOrWhiteSpace(option1)) && (option1 != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@option1", option1);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@option1", DBNull.Value);
                }


                if ((!String.IsNullOrWhiteSpace(option2)) && (option2 != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@option2", option2);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@option2", DBNull.Value);
                }





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


        public DataTable SearchbyGifts(string CampaignID, string promotionName, String shopName, string Target_Brand, DateTime? repeatFrom, DateTime? repeatTo, string instructionNo, string campaignTypeID, string priority, string remark, string subject,int option)
        {
            try
            {
                //if (Campaing_TypeID == "")
                //{
                //    Campaing_TypeID = null;
                //}
                //int stname = Convert.ToInt32(shopName);
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_Promotion_Search", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();

                if ((!String.IsNullOrWhiteSpace(CampaignID) && (CampaignID != "")))
                {

                    da.SelectCommand.Parameters.AddWithValue("@Campaign_ID", CampaignID);

                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Campaign_ID", DBNull.Value);

                }


                if ((!String.IsNullOrWhiteSpace(promotionName)) && (promotionName != ""))
                {

                    da.SelectCommand.Parameters.AddWithValue("@Promotion_Name", promotionName);

                }

                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Promotion_Name", DBNull.Value);
                }

                if ((!String.IsNullOrWhiteSpace(shopName)) && (shopName != ""))
                {

                    da.SelectCommand.Parameters.AddWithValue("@Shop_ID", shopName);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Shop_ID", DBNull.Value);
                }

                if ((!String.IsNullOrWhiteSpace(campaignTypeID)) && (campaignTypeID != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Campaing_TypeID", campaignTypeID);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Campaing_TypeID", DBNull.Value);
                }


                if ((!String.IsNullOrWhiteSpace(Target_Brand)) && (Target_Brand != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Brand_Name", Target_Brand);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Brand_Name", DBNull.Value);
                }

                if (repeatFrom != null)
                {
                    da.SelectCommand.Parameters.AddWithValue("@repeatfrom", repeatFrom);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@repeatfrom", DBNull.Value);
                }
                if (repeatTo != null)
                {
                    da.SelectCommand.Parameters.AddWithValue("@repeatto", repeatTo);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@repeatto", DBNull.Value);
                }

                if ((!String.IsNullOrWhiteSpace(instructionNo)) && (instructionNo != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@InstructionNo", instructionNo);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@InstructionNo", DBNull.Value);
                }


                if ((!String.IsNullOrWhiteSpace(priority)) && (priority != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@priority", priority);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@priority", DBNull.Value);
                }



                if ((!String.IsNullOrWhiteSpace(remark)) && (remark != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@remark", remark);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@remark", DBNull.Value);
                }



                if ((!String.IsNullOrWhiteSpace(subject)) && (subject != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@subject", subject);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@subject", DBNull.Value);
                }

                da.SelectCommand.Parameters.AddWithValue("@Option",option);

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



        public DataTable Searchby_ValidItem(string CampaignID, string promotionName, String shopName, string Target_Brand, DateTime? repeatFrom, DateTime? repeatTo, string instructionNo, string campaignTypeID, string priority, string remark, string subject,string item_code,string option1,string option2)
        {
            try
            {
                //if (Campaing_TypeID == "")
                //{
                //    Campaing_TypeID = null;
                //}
                //int stname = Convert.ToInt32(shopName);
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("SP_PromotionView_byItem_Valid", connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();

                if ((!String.IsNullOrWhiteSpace(CampaignID) && (CampaignID != "")))
                {

                    da.SelectCommand.Parameters.AddWithValue("@Campaign_ID", CampaignID);

                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Campaign_ID", DBNull.Value);

                }


                if ((!String.IsNullOrWhiteSpace(promotionName)) && (promotionName != ""))
                {

                    da.SelectCommand.Parameters.AddWithValue("@Promotion_Name", promotionName);

                }

                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Promotion_Name", DBNull.Value);
                }

                if ((!String.IsNullOrWhiteSpace(shopName)) && (shopName != ""))
                {

                    da.SelectCommand.Parameters.AddWithValue("@Shop_ID", shopName);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Shop_ID", DBNull.Value);
                }

                if ((!String.IsNullOrWhiteSpace(campaignTypeID)) && (campaignTypeID != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Campaing_TypeID", campaignTypeID);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Campaing_TypeID", DBNull.Value);
                }


                if ((!String.IsNullOrWhiteSpace(Target_Brand)) && (Target_Brand != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Brand_Name", Target_Brand);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@Brand_Name", DBNull.Value);
                }

                if (repeatFrom != null)
                {
                    da.SelectCommand.Parameters.AddWithValue("@repeatfrom", repeatFrom);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@repeatfrom", DBNull.Value);
                }
                if (repeatTo != null)
                {
                    da.SelectCommand.Parameters.AddWithValue("@repeatto", repeatTo);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@repeatto", DBNull.Value);
                }

                if ((!String.IsNullOrWhiteSpace(instructionNo)) && (instructionNo != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@InstructionNo", instructionNo);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@InstructionNo", DBNull.Value);
                }


                if ((!String.IsNullOrWhiteSpace(priority)) && (priority != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@priority", priority);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@priority", DBNull.Value);
                }



                if ((!String.IsNullOrWhiteSpace(remark)) && (remark != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@remark", remark);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@remark", DBNull.Value);
                }



                if ((!String.IsNullOrWhiteSpace(subject)) && (subject != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@subject", subject);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@subject", DBNull.Value);
                }

                if ((!String.IsNullOrWhiteSpace(item_code)) && (item_code != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@item_code", item_code);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@item_code", DBNull.Value);
                }


                if ((!String.IsNullOrWhiteSpace(option1)) && (option1 != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@option1", option1);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@option1", DBNull.Value);
                }


                if ((!String.IsNullOrWhiteSpace(option2)) && (option2 != ""))
                {
                    da.SelectCommand.Parameters.AddWithValue("@option2", option2);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@option2", DBNull.Value);
                }





                

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

        public DataTable GetPromotionForRakuten(string str, string option,int shop_id) 
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_CamPromotion_Rakuten ", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@strString", str);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
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
        public DataTable GetPromotionForJisha(string str, string option, int shop_id)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
               
                SqlDataAdapter sda = new SqlDataAdapter("SP_CamPromotion_Jisha ", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@strString", str);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
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
        public DataTable GetPromotionForPonpare(string str, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_CamPromotion_Ponpare", connectionString);
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

        public DataTable GetPromotionForYahoo(string str, string option,string shopID)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_CampaingPromotion_GetDataForYahoo", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@PromotionID", str);
                sda.SelectCommand.Parameters.AddWithValue("@option", option);
                if(!String.IsNullOrWhiteSpace(shopID))
                sda.SelectCommand.Parameters.AddWithValue("@shopid",Int32.Parse( shopID));
                else
                    sda.SelectCommand.Parameters.AddWithValue("@shopid", DBNull.Value);
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

        public DataTable GetRakutenPromotionByCampaingTypeID(string str, string option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_Promotion_GetDataForRakuten", connectionString);
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
        

        public DataTable SelectAllProAttachment(int ID, int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SP_SelectAlldata_CampaingPromotion_Attachment";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Parameters.AddWithValue("@ID",ID);
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


        public DataTable Select_CampaignAll()
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                string query = "SP_PromotionView_ALL";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
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

 //for Consoles Point
        public DataTable Adddata(int option)
        {
            try
            {

                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string query = "SP_Promotion_Point_ExportcsvSelectall";


                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@option", option);

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
        public DataTable GetMallID(int shopid)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string query = "Select Shop_Name,Mall_ID,ID FROM Shop WHERE ID =" + shopid;

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
        public  int Exhibition_List_Insert(int item_ID, string Itemcode, int shopid, string itemname, int csvtype, int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Exhibition_Promotion_Insert", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_ID", item_ID);
                cmd.Parameters.AddWithValue("@Item_Code", Itemcode);
                cmd.Parameters.AddWithValue("@option", option);
                cmd.Parameters.AddWithValue("@shopid", shopid);
                cmd.Parameters.AddWithValue("@itemname", itemname);
                cmd.Parameters.AddWithValue("@csvtype", csvtype);
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
        public  DataTable GetdatafromMaster(string itemcode)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();

                string query = "SP_SelectPromotionItemMaster";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@itemcode", itemcode);
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
        public DataTable GetdataforRakuten(string list, int sid, int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();

                string query = "Getdata_PromotionPoint_Rakuten";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@strString", list);
                da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
                da.SelectCommand.Parameters.AddWithValue("@option", option);
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
        public  DataTable Removedata(int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();

                string query = "SP_Promotion_Point_RemoveExportcsvSelectall";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@option", option);
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
        public  DataTable SelectSecondCSVdata(int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();

                string query = "SP_SelectPointCSVData_RakutenPonpare";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@option", option);
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
        public void ChangeExportStatusFlag(int status, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string query = "UPDATE Promotion_Point SET "
                + "Export_Status = " + status
                + "WHERE ID = " + dr["ID"].ToString();
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public void Insert_ItemOption_For_New(DataTable dt,int pid,int shopid)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string query = "Insert into Promotion_ItemOption_Log(Promotion_ID,Option_Value,Option_Name,Ctrl_ID,Shop_ID)Values(" + pid + ",'" + dr["Option_Value"] + "','" + dr["Option_Name"] + "','" + "d" + "',"+shopid +")";
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public void Delete_ItemOption_For_New(int pid,int shopid)
        {
                string query = "Delete from Promotion_ItemOption_Log where Promotion_ID="+pid+"and Ctrl_ID='d'"+"and Shop_ID="+shopid;
                SqlConnection con = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
        }

        public DataTable SelectOptionValue(int pid)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();
                string query = "Select * FROM Promotion_ItemOption where Promotion_ID="+ pid + "and Ctrl_ID='n'";
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

        public bool InsertItemExportQ(string filename, int ftype, int sid, int isexp, int exptype)
        {
            SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Item_ExportQ_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionString;
                cmd.Parameters.AddWithValue("@File_Name", filename);
                cmd.Parameters.AddWithValue("@File_Type", ftype);
                cmd.Parameters.AddWithValue("@ShopID", sid);
                cmd.Parameters.AddWithValue("@IsExport", isexp);
                cmd.Parameters.AddWithValue("@Export_Type", exptype);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       public DataTable GetdataforYahoo(string list, int sid, int option)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                DataTable dt = new DataTable();

                string query = "Getdata_PromotionPoint_Yahoo";
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ItemID", list);
                da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
                da.SelectCommand.Parameters.AddWithValue("@option", option);
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
       public DataTable GetdataforPonpare(string list, int sid, int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();

               string query = "Getdata_PromotionPoint_Ponpare";
               SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.AddWithValue("@strString", list);
               da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
               da.SelectCommand.Parameters.AddWithValue("@option", option);
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
       public void CamRExbImportXmlInsert(string xml)
       {
           SqlConnection connection = new SqlConnection(DataConfig.connectionString);
           SqlCommand cmd = new SqlCommand("SP_RakutenPromotionPoint_Logdata_Insert", connection);
           try
           {
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
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
       public void Exhibition_Log_Update(int item_ID, string Itemcode, int shopid, string itemname,string pc,string mobile)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlCommand cmd = new SqlCommand("SP_PointItemName_logUpdate", connectionString);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Parameters.AddWithValue("@Item_ID", item_ID);
               cmd.Parameters.AddWithValue("@Item_Code", Itemcode);
               cmd.Parameters.AddWithValue("@Mobile_Catch_Copy", mobile);
               cmd.Parameters.AddWithValue("@shopid", shopid);
               cmd.Parameters.AddWithValue("@Item_Name", itemname);
               cmd.Parameters.AddWithValue("@PC_Catch_Copy", pc);
              
               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               cmd.Connection.Close();
             
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public int Exhibition_Prolog_Insert(int item_ID, string Itemcode, int shopid, string itemname, int csvtype, int option,string ctrl,string purl,int pmag,string pperiod,string pccatch,string mcatch,string pcatch)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlCommand cmd = new SqlCommand("SP_Ponpare_PointPromotionlog_Insert", connectionString);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Parameters.AddWithValue("@Item_ID", item_ID);
               cmd.Parameters.AddWithValue("@Item_Code", Itemcode);
               cmd.Parameters.AddWithValue("@option", option);
               cmd.Parameters.AddWithValue("@Shop_ID", shopid);
               cmd.Parameters.AddWithValue("@Item_Name", itemname);
               cmd.Parameters.AddWithValue("@Promotion_CSVtype", csvtype);
               cmd.Parameters.AddWithValue("@Ctrl_ID",ctrl);
               cmd.Parameters.AddWithValue("@Product_URL", purl);
               cmd.Parameters.AddWithValue("@Point_Magnification", pmag);
               cmd.Parameters.AddWithValue("@Point_Period", pperiod);
                cmd.Parameters.AddWithValue("@PC_Catch_Copy",pccatch);
               cmd.Parameters.AddWithValue("@Mobile_Catch_Copy", mcatch);
               cmd.Parameters.AddWithValue("@Ponpare_PC_Catch", pcatch);
               cmd.Parameters.AddWithValue("@Promotion_type", 1);
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
       public int PointYahoolog_Insert(int item_ID, string Itemcode, int shopid, string itemname, int csvtype,DataTable dt,int promotiontype)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlCommand cmd = new SqlCommand("SP_Yahoo_PointPromotionlog_Insert", connectionString);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Parameters.AddWithValue("@Item_ID", item_ID);
               cmd.Parameters.AddWithValue("@Item_Code", Itemcode);
               cmd.Parameters.AddWithValue("@Shop_ID", shopid);
               cmd.Parameters.AddWithValue("@Item_Name", itemname);
               cmd.Parameters.AddWithValue("@Promotion_CSVtype", csvtype);
               cmd.Parameters.AddWithValue("@path", dt.Rows[0]["path"].ToString());
               cmd.Parameters.AddWithValue("@Sub_code", dt.Rows[0]["sub-code"].ToString());
               cmd.Parameters.AddWithValue("@Original_price", dt.Rows[0]["original-price"].ToString());
               if (!String.IsNullOrWhiteSpace(dt.Rows[0]["price"].ToString()))
               cmd.Parameters.AddWithValue("@Price",(int) dt.Rows[0]["price"]);
               else
               cmd.Parameters.AddWithValue("@Price",DBNull.Value);
               if (!String.IsNullOrWhiteSpace(dt.Rows[0]["sale-price"].ToString()))
               cmd.Parameters.AddWithValue("@Sale_price",(int) dt.Rows[0]["sale-price"]);
               else
                cmd.Parameters.AddWithValue("@Sale_price",DBNull.Value);
               cmd.Parameters.AddWithValue("@Options", dt.Rows[0]["options"].ToString());
               cmd.Parameters.AddWithValue("@Headline", dt.Rows[0]["headline"].ToString());
               cmd.Parameters.AddWithValue("@Caption", dt.Rows[0]["caption"].ToString());
               cmd.Parameters.AddWithValue("@Abstract", dt.Rows[0]["abstract"].ToString());
               cmd.Parameters.AddWithValue("@Explanation", dt.Rows[0]["explanation"].ToString());
               cmd.Parameters.AddWithValue("@Additional1", dt.Rows[0]["additional1"].ToString());
               cmd.Parameters.AddWithValue("@Additional2", dt.Rows[0]["additional2"].ToString());
               cmd.Parameters.AddWithValue("@Additional3", dt.Rows[0]["additional3"].ToString());
               cmd.Parameters.AddWithValue("@Relevant_links", dt.Rows[0]["relevant-links"].ToString());
               if (!String.IsNullOrWhiteSpace(dt.Rows[0]["ship-weight"].ToString()))
               cmd.Parameters.AddWithValue("@Ship_weight",(int) dt.Rows[0]["ship-weight"]);
               else
                cmd.Parameters.AddWithValue("@Ship_weight",DBNull.Value);
               if (!String.IsNullOrWhiteSpace(dt.Rows[0]["taxable"].ToString()))
               cmd.Parameters.AddWithValue("@Taxable",Convert.ToInt32(dt.Rows[0]["taxable"].ToString()));
               else
                cmd.Parameters.AddWithValue("@Taxable", DBNull.Value);
               cmd.Parameters.AddWithValue("@Release_date", dt.Rows[0]["release-date"].ToString());
               cmd.Parameters.AddWithValue("@Temporary_point_term", dt.Rows[0]["temporary-point-term"].ToString());
               cmd.Parameters.AddWithValue("@Point_code", dt.Rows[0]["point-code"].ToString());
               cmd.Parameters.AddWithValue("@Meta_key", dt.Rows[0]["meta-key"].ToString());
               cmd.Parameters.AddWithValue("@Meta_desc", dt.Rows[0]["meta-desc"].ToString());
               if (!String.IsNullOrWhiteSpace(dt.Rows[0]["template"].ToString()))
               cmd.Parameters.AddWithValue("@Template", Convert.ToInt32(dt.Rows[0]["template"].ToString()));
               else
                cmd.Parameters.AddWithValue("@Template",DBNull.Value);
               
               cmd.Parameters.AddWithValue("@Sale_period_start",dt.Rows[0]["sale-period-start"].ToString());
              
               
               cmd.Parameters.AddWithValue("@Sale_period_end", dt.Rows[0]["sale-period-end"].ToString());
               cmd.Parameters.AddWithValue("@Sale_limit", dt.Rows[0]["sale-limit"].ToString());
               cmd.Parameters.AddWithValue("@Sp_code", dt.Rows[0]["sp-code"].ToString());
               
               cmd.Parameters.AddWithValue("@Brand_code",dt.Rows[0]["brand-code"].ToString());
            
            
               cmd.Parameters.AddWithValue("@Person_code",dt.Rows[0]["person-code"].ToString());
           
               cmd.Parameters.AddWithValue("@Yahoo_product_code", dt.Rows[0]["yahoo-product-code"].ToString());
               cmd.Parameters.AddWithValue("@Product_code", dt.Rows[0]["product-code"].ToString());
               cmd.Parameters.AddWithValue("@Jan", dt.Rows[0]["jan"].ToString());
               cmd.Parameters.AddWithValue("@Isbn", dt.Rows[0]["isbn"].ToString());
               if (!String.IsNullOrWhiteSpace(dt.Rows[0]["delivery"].ToString()))
               cmd.Parameters.AddWithValue("@Delivery", Convert.ToInt32(dt.Rows[0]["delivery"].ToString()));
               else
               cmd.Parameters.AddWithValue("@Delivery",DBNull.Value);
               cmd.Parameters.AddWithValue("@Astk_code", dt.Rows[0]["astk-code"].ToString());
               cmd.Parameters.AddWithValue("@Condition", dt.Rows[0]["condition"].ToString());
               cmd.Parameters.AddWithValue("@Taojapan", dt.Rows[0]["taojapan"].ToString());
               cmd.Parameters.AddWithValue("@Product_category",dt.Rows[0]["product-category"].ToString());
               cmd.Parameters.AddWithValue("@Spec1", dt.Rows[0]["spec1"].ToString());
               cmd.Parameters.AddWithValue("@Spec2", dt.Rows[0]["spec2"].ToString());
               cmd.Parameters.AddWithValue("@Spec3", dt.Rows[0]["spec3"].ToString());
               cmd.Parameters.AddWithValue("@Spec4", dt.Rows[0]["spec4"].ToString());
               cmd.Parameters.AddWithValue("@Spec5",dt.Rows[0]["spec5"].ToString());
               if (!String.IsNullOrWhiteSpace(dt.Rows[0]["display"].ToString()))
               cmd.Parameters.AddWithValue("@Display",(int)dt.Rows[0]["display"]);
               else
               cmd.Parameters.AddWithValue("@Display",DBNull.Value);
               cmd.Parameters.AddWithValue("@Promotion_Type", promotiontype);
               
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
        //Delivery
       public  DataTable GetdataforDeliveryRakuten(string list, int sid, int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();

               string query = "SP_GetData_PromotionDeliveryRakuten";
               SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.AddWithValue("@strString", list);
               da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
               da.SelectCommand.Parameters.AddWithValue("@option", option);
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
       public  DataTable RemoveCSVdataforDeliveryRakuten(string list, int sid, int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();

               string query = "SP_RemoveData_PromotionDeliveryRakuten";
               SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.AddWithValue("@strString", list);
               da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
               da.SelectCommand.Parameters.AddWithValue("@option", option);
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
       public  DataTable Selectdeliverydata(int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();

               string query = "SP_PromotionDelivery_SelectAll";
               SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.AddWithValue("@option", option);
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
       public  DataTable GetdataforDeliveryYahoo(string list, int sid, int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();

               string query = "SP_GetData_PromotionDeliveryYahoo";
               SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.AddWithValue("@strString", list);
               da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
               da.SelectCommand.Parameters.AddWithValue("@option", option);
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
       public  void ChangeDeliveryExportStatusFlag(int status, DataTable dt)
       {
           foreach (DataRow dr in dt.Rows)
           {
               string query = "UPDATE Promotion_Delivery SET "
               + "Export_Status = " + status
               + "WHERE ID = " + dr["ID"].ToString();
               SqlConnection con = new SqlConnection(DataConfig.connectionString);
               SqlCommand cmd = new SqlCommand(query, con);
               cmd.CommandType = CommandType.Text;

               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               cmd.Connection.Close();
           }
       }
       public DataTable GetdataforDelveryPonpare(string list, int sid, int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();

               string query = "SP_GetData_PromotionDeliveryPonpare";
               SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.AddWithValue("@strString", list);
               da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
               da.SelectCommand.Parameters.AddWithValue("@option", option);
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
       public  DataTable GetdataforDeliveryJisha(string list, int sid, int option)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               DataTable dt = new DataTable();

               string query = "SP_GetData_PromotionDeliveryJiaha";
               SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.AddWithValue("@strString", list);
               da.SelectCommand.Parameters.AddWithValue("@shopid", sid);
               da.SelectCommand.Parameters.AddWithValue("@option", option);
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
       public void ChangeDeliveryExportFlag(int status,string id,int opt)
       {
          
               string query = "SP_DeliveryExportstatus_flagchange";
               SqlConnection con = new SqlConnection(DataConfig.connectionString);
               SqlCommand cmd = new SqlCommand(query, con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ID",id);
               cmd.Parameters.AddWithValue("@status",status);
               cmd.Parameters.AddWithValue("@option",opt);
               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               cmd.Connection.Close();
          
       }
        //updated date 18/06/2015
       public int Rakuten_Deliverylog_Insert(int csvtype,string ictrlid,string demagno,string sctrlId,string stype,string svalue,string sname,DataTable dt)
       {
           try
           {
               int eid = 0;
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   SqlCommand cmd = new SqlCommand("SP_Rakuten_DeliverylogInsert", connectionString);
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.CommandTimeout = 0;
                   cmd.Parameters.AddWithValue("@Ctrl_ID", ictrlid);
                   cmd.Parameters.AddWithValue("@Item_Code", dt.Rows[i]["商品管理番号（商品URL）"].ToString());
                   cmd.Parameters.AddWithValue("@Product_URL", dt.Rows[i]["商品管理番号（商品URL）"].ToString());
                   cmd.Parameters.AddWithValue("@Delivery_Management_No", demagno);
                   cmd.Parameters.AddWithValue("@Checkname", sname);
                   cmd.Parameters.AddWithValue("@Checkvalue", svalue);
                   cmd.Parameters.AddWithValue("@Cat_ctrlID", dt.Rows[i]["コントロールカラム"].ToString());
                   cmd.Parameters.AddWithValue("@Display_Category", dt.Rows[i]["表示先カテゴリ"].ToString());
                   if (dt.Columns.Contains("優先度"))
                   cmd.Parameters.AddWithValue("@Priority", dt.Rows[i]["優先度"].ToString());
                   else
                    cmd.Parameters.AddWithValue("@Priority",DBNull.Value);
                   if (dt.Columns.Contains("1ページ複数形式"))
                   cmd.Parameters.AddWithValue("@Page_Format", dt.Rows[i]["1ページ複数形式"].ToString());
                   else
                       cmd.Parameters.AddWithValue("@Page_Format", DBNull.Value);
                   cmd.Parameters.AddWithValue("@Category_Setno", dt.Rows[i]["カテゴリセット管理番号"].ToString());
                   cmd.Parameters.AddWithValue("@Category_Setname", dt.Rows[i]["カテゴリセット名"].ToString());
                   cmd.Parameters.AddWithValue("@Promotion_CSVtype", csvtype);
                   cmd.Parameters.AddWithValue("@shopid",Convert.ToInt32(dt.Rows[i]["Shop_ID"].ToString()));
                   cmd.Parameters.AddWithValue("@selectCtrl_ID", sctrlId);
                   cmd.Parameters.AddWithValue("@Choice_Type", stype);

                   cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                   cmd.Connection.Open();
                   cmd.ExecuteNonQuery();
                   cmd.Connection.Close();
                   eid = Convert.ToInt32(cmd.Parameters["@result"].Value);
               }
               return eid;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public int Ponpare_Deliverylog_Insert(int csvtype,DataTable dt,string choicetype,string shopcategory,string catctrl)
       {
           try
           {
               int eid = 0;
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   SqlCommand cmd = new SqlCommand("SP_Ponpare_DeliverylogInsert", connectionString);
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.CommandTimeout = 0;

                   cmd.Parameters.AddWithValue("@Item_Code", dt.Rows[i]["商品管理ID（商品URL）"].ToString());
                   cmd.Parameters.AddWithValue("@Shop_Category", shopcategory);
                   cmd.Parameters.AddWithValue("@selectCtrl_ID", dt.Rows[i]["コントロールカラム"].ToString());
                   cmd.Parameters.AddWithValue("@Checkname", dt.Rows[i]["購入オプション名"].ToString());
                   cmd.Parameters.AddWithValue("@Checkvalue", dt.Rows[i]["オプション項目名"].ToString());
                   cmd.Parameters.AddWithValue("@Cat_ctrlID", catctrl);
                   cmd.Parameters.AddWithValue("@Display_Category", choicetype);
                   cmd.Parameters.AddWithValue("@Choice_Type", dt.Rows[i]["選択肢タイプ"].ToString());
                   cmd.Parameters.AddWithValue("@Promotion_CSVtype", csvtype);
                   cmd.Parameters.AddWithValue("@shopid",Convert.ToInt32(dt.Rows[i]["Shop_ID"].ToString()));
                  

                   cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                   cmd.Connection.Open();
                   cmd.ExecuteNonQuery();
                   cmd.Connection.Close();
                   eid = Convert.ToInt32(cmd.Parameters["@result"].Value);
               }
               return eid;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        //Campaign
       public  string GetItem_Code(int opt, string id)
       {
           try
           {
               DataTable dt = new DataTable();
               SqlConnection connectionstring = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter sda = new SqlDataAdapter("SP_Select_CampaingPromotionItemCode", connectionstring);
               sda.SelectCommand.CommandType = CommandType.StoredProcedure;
               sda.SelectCommand.Connection.Open();
               sda.SelectCommand.Parameters.AddWithValue("@option", opt);
               sda.SelectCommand.Parameters.AddWithValue("@ids", id);
               sda.Fill(dt);
               sda.SelectCommand.Connection.Close();
               return dt.Rows[0]["Item_Code"].ToString();
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       public  string GetItem_ID(int opt, string ids)
       {
           try
           {
               DataTable dt = new DataTable();
               SqlConnection connectionstring = new SqlConnection(DataConfig.connectionString);
               SqlDataAdapter sda = new SqlDataAdapter("SP_Select_CampaingPromotionItemCode", connectionstring);
               sda.SelectCommand.CommandType = CommandType.StoredProcedure;
               sda.SelectCommand.Connection.Open();
               sda.SelectCommand.Parameters.AddWithValue("@option", opt);
               sda.SelectCommand.Parameters.AddWithValue("@ids", ids);
               sda.Fill(dt);
               sda.SelectCommand.Connection.Close();
               return dt.Rows[0]["Item_ID"].ToString();
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       public  int Exhibition_List_Insert(string item_ID, string Itemcode, int csv, string pid, string sid)
       {
           try
           {
               SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
               SqlCommand cmd = new SqlCommand("SP_Exhibition_Promotion_CampaingInsert", connectionString);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Parameters.AddWithValue("@Item_ID", item_ID);
               cmd.Parameters.AddWithValue("@Item_Code", Itemcode);
               cmd.Parameters.AddWithValue("@csvtype", csv);
               cmd.Parameters.AddWithValue("@pid", pid);
               cmd.Parameters.AddWithValue("@shopid", sid);
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
       public void ChangeExportStatusFlags(int status, DataTable dt)
       {
           foreach (DataRow dr in dt.Rows)
           {
               string query = "UPDATE Promotion SET "
               + "Export_Status = " + status
               + "WHERE ID = " + dr["ID"].ToString();
               SqlConnection con = new SqlConnection(DataConfig.connectionString);
               SqlCommand cmd = new SqlCommand(query, con);
               cmd.CommandType = CommandType.Text;

               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               cmd.Connection.Close();
           }
       }
       public void CampaignExportStatusFlags(int status,string id,int opt)
       {
         
               string query = "SP_CampaignExportstatus_flagchange";
               SqlConnection con = new SqlConnection(DataConfig.connectionString);
               SqlCommand cmd = new SqlCommand(query, con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ID",id);
               cmd.Parameters.AddWithValue("@status",status);
               cmd.Parameters.AddWithValue("@option",opt);
               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               cmd.Connection.Close();
         
       }
      public void ChangePromotionStatusFlags(int IsProstatus, DataTable dt)
       {
           foreach (DataRow dr in dt.Rows)
           {
               string query = "UPDATE Promotion SET "
               + "IsPromotionClose=" + IsProstatus
               + "WHERE ID = " + dr["ID"].ToString();
               SqlConnection con = new SqlConnection(DataConfig.connectionString);
               SqlCommand cmd = new SqlCommand(query, con);
               cmd.CommandType = CommandType.Text;

               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               cmd.Connection.Close();
           }
       }
        //updated date 18/06/2015
      public int Rakuten_Campaignlog_Insert(string item_ID, string Itemcode, int csv, string pid, string sid,DataTable dt,string selectctrl,string choicetype)
      {
          try
          {
              int eid = 0;
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              if (dt != null && dt.Rows.Count > 0)
              {
                  for (int i = 0; i < dt.Rows.Count; i++)
                  {
                      SqlCommand cmd = new SqlCommand("SP_Rakuten_CampainglogInsert", connectionString);
                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.CommandTimeout = 0;
                      //cmd.Parameters.AddWithValue("@Item_ID", dt.Rows[i]["Item_ID"].ToString());
                      cmd.Parameters.AddWithValue("@Item_Code", dt.Rows[i]["商品番号"].ToString());
                      cmd.Parameters.AddWithValue("@Promotion_CSVtype", csv);
                      cmd.Parameters.AddWithValue("@pid",Convert.ToInt32(dt.Rows[i]["PID"].ToString()));
                      cmd.Parameters.AddWithValue("@shopid", Convert.ToInt32(dt.Rows[i]["Shop_ID"].ToString()));
                   
                          cmd.Parameters.AddWithValue("@Ctrl_ID", dt.Rows[i]["コントロールカラム"].ToString());
                          cmd.Parameters.AddWithValue("@Product_URL", dt.Rows[i]["商品管理番号（商品URL）"].ToString());
                          cmd.Parameters.AddWithValue("@Item_Name", dt.Rows[i]["商品名"].ToString());
                          cmd.Parameters.AddWithValue("@PC_Catch_Copy", dt.Rows[i]["PC用キャッチコピー"].ToString());
                          cmd.Parameters.AddWithValue("@Mobile_Catch_Copy", dt.Rows[i]["モバイル用キャッチコピー"].ToString());
                          cmd.Parameters.AddWithValue("@Itemdescription_Smartphone", dt.Rows[i]["スマートフォン用商品説明文"].ToString());
                          cmd.Parameters.AddWithValue("@Itemdescription_PC ", dt.Rows[i]["PC用商品説明文"].ToString());
                          cmd.Parameters.AddWithValue("@Saledescription_PC", dt.Rows[i]["PC用販売説明文"].ToString());
                          cmd.Parameters.AddWithValue("@BlackMarket_Password", dt.Rows[i]["闇市パスワード"].ToString());
                   
                      cmd.Parameters.AddWithValue("@selectCtrl_ID ", selectctrl);
                      cmd.Parameters.AddWithValue("@Choice_Type", choicetype);
                      cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                      cmd.Connection.Open();
                      cmd.ExecuteNonQuery();
                      cmd.Connection.Close();
                      eid = Convert.ToInt32(cmd.Parameters["@result"].Value);
                  }
              }//if
              //else
              //{
              //    SqlCommand cmd = new SqlCommand("SP_Rakuten_CampainglogInsert", connectionString);
              //    cmd.CommandType = CommandType.StoredProcedure;
              //    cmd.CommandTimeout = 0;
              //    //cmd.Parameters.AddWithValue("@Item_ID", item_ID);
              //    cmd.Parameters.AddWithValue("@Item_Code", Itemcode);
              //    cmd.Parameters.AddWithValue("@Promotion_CSVtype", csv);
              //    cmd.Parameters.AddWithValue("@pid", pid);
              //    cmd.Parameters.AddWithValue("@shopid", sid);
               
              //      cmd.Parameters.AddWithValue("@Ctrl_ID", DBNull.Value);
              //        cmd.Parameters.AddWithValue("@Product_URL", DBNull.Value);
              //        cmd.Parameters.AddWithValue("@Item_Name", DBNull.Value);
              //        cmd.Parameters.AddWithValue("@PC_Catch_Copy", DBNull.Value);
              //        cmd.Parameters.AddWithValue("@Mobile_Catch_Copy", DBNull.Value);
              //        cmd.Parameters.AddWithValue("@Itemdescription_Smartphone", DBNull.Value);
              //        cmd.Parameters.AddWithValue("@Itemdescription_PC ", DBNull.Value);
              //        cmd.Parameters.AddWithValue("@Saledescription_PC", DBNull.Value);
              //        cmd.Parameters.AddWithValue("@BlackMarket_Password", DBNull.Value);
                
              //    cmd.Parameters.AddWithValue("@selectCtrl_ID ", selectctrl);
              //    cmd.Parameters.AddWithValue("@Choice_Type", choicetype);
              //    cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
              //    cmd.Connection.Open();
              //    cmd.ExecuteNonQuery();
              //    cmd.Connection.Close();
              //    eid = Convert.ToInt32(cmd.Parameters["@result"].Value);
              //}
              return eid;
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      public int Yahoo_Campaignlog_Insert(int csvtype, DataTable dt, int promotiontype,int option)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              int eid = 0;
              for (int i = 0; i < dt.Rows.Count; i++)
              {
                  SqlCommand cmd = new SqlCommand("SP_Yahoo_CampaignPromotionlog_Insert", connectionString);
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.CommandTimeout = 0;

                  cmd.Parameters.AddWithValue("@Item_Code", dt.Rows[i]["code"].ToString());
                  cmd.Parameters.AddWithValue("@Shop_ID", dt.Rows[i]["Shop_ID"].ToString());
                  cmd.Parameters.AddWithValue("@Item_Name", dt.Rows[i]["name"].ToString());
                  cmd.Parameters.AddWithValue("@Promotion_CSVtype", csvtype);
                  cmd.Parameters.AddWithValue("@path", dt.Rows[i]["path"].ToString());
                  cmd.Parameters.AddWithValue("@Sub_code", dt.Rows[i]["sub-code"].ToString());
                  cmd.Parameters.AddWithValue("@Original_price", dt.Rows[i]["original-price"].ToString());
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["price"].ToString()))
                      cmd.Parameters.AddWithValue("@Price", (int)dt.Rows[i]["price"]);
                  else
                      cmd.Parameters.AddWithValue("@Price", DBNull.Value);
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["sale-price"].ToString()))
                      cmd.Parameters.AddWithValue("@Sale_price", (int)dt.Rows[i]["sale-price"]);
                  else
                      cmd.Parameters.AddWithValue("@Sale_price", DBNull.Value);
                  cmd.Parameters.AddWithValue("@Options", dt.Rows[i]["options"].ToString());
                  cmd.Parameters.AddWithValue("@Headline", dt.Rows[i]["headline"].ToString());
                  cmd.Parameters.AddWithValue("@Caption", dt.Rows[i]["caption"].ToString());
                  cmd.Parameters.AddWithValue("@Abstract", dt.Rows[i]["abstract"].ToString());
                  cmd.Parameters.AddWithValue("@Explanation", dt.Rows[i]["explanation"].ToString());
                  cmd.Parameters.AddWithValue("@Additional1", dt.Rows[i]["additional1"].ToString());
                  cmd.Parameters.AddWithValue("@Additional2", dt.Rows[i]["additional2"].ToString());
                  cmd.Parameters.AddWithValue("@Additional3", dt.Rows[i]["additional3"].ToString());
                  cmd.Parameters.AddWithValue("@Relevant_links", dt.Rows[i]["relevant-links"].ToString());
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["ship-weight"].ToString()))
                      cmd.Parameters.AddWithValue("@Ship_weight", (int)dt.Rows[i]["ship-weight"]);
                  else
                      cmd.Parameters.AddWithValue("@Ship_weight", DBNull.Value);
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["taxable"].ToString()))
                      cmd.Parameters.AddWithValue("@Taxable", Convert.ToInt32(dt.Rows[i]["taxable"].ToString()));
                  else
                      cmd.Parameters.AddWithValue("@Taxable", DBNull.Value);
                  cmd.Parameters.AddWithValue("@Release_date", dt.Rows[i]["release-date"].ToString());
                  cmd.Parameters.AddWithValue("@Temporary_point_term", dt.Rows[i]["temporary-point-term"].ToString());
                  cmd.Parameters.AddWithValue("@Point_code", dt.Rows[i]["point-code"].ToString());
                  cmd.Parameters.AddWithValue("@Meta_key", dt.Rows[i]["meta-key"].ToString());
                  cmd.Parameters.AddWithValue("@Meta_desc", dt.Rows[i]["meta-desc"].ToString());
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["template"].ToString()))
                      cmd.Parameters.AddWithValue("@Template", Convert.ToInt32(dt.Rows[i]["template"].ToString()));
                  else
                      cmd.Parameters.AddWithValue("@Template", DBNull.Value);

                  cmd.Parameters.AddWithValue("@Sale_period_start", dt.Rows[i]["sale-period-start"].ToString());


                  cmd.Parameters.AddWithValue("@Sale_period_end", dt.Rows[i]["sale-period-end"].ToString());
                  cmd.Parameters.AddWithValue("@Sale_limit", dt.Rows[i]["sale-limit"].ToString());
                  cmd.Parameters.AddWithValue("@Sp_code", dt.Rows[i]["sp-code"].ToString());

                  cmd.Parameters.AddWithValue("@Brand_code", dt.Rows[i]["brand-code"].ToString());


                  cmd.Parameters.AddWithValue("@Person_code", dt.Rows[i]["person-code"].ToString());

                  cmd.Parameters.AddWithValue("@Yahoo_product_code", dt.Rows[i]["yahoo-product-code"].ToString());
                  cmd.Parameters.AddWithValue("@Product_code", dt.Rows[i]["product-code"].ToString());
                  cmd.Parameters.AddWithValue("@Jan", dt.Rows[i]["jan"].ToString());
                  cmd.Parameters.AddWithValue("@Isbn", dt.Rows[i]["isbn"].ToString());
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["delivery"].ToString()))
                      cmd.Parameters.AddWithValue("@Delivery", Convert.ToInt32(dt.Rows[i]["delivery"].ToString()));
                  else
                      cmd.Parameters.AddWithValue("@Delivery", DBNull.Value);
                  cmd.Parameters.AddWithValue("@Astk_code", dt.Rows[i]["astk-code"].ToString());
                  cmd.Parameters.AddWithValue("@Condition", dt.Rows[i]["condition"].ToString());
                  cmd.Parameters.AddWithValue("@Taojapan", dt.Rows[i]["taojapan"].ToString());
                  cmd.Parameters.AddWithValue("@Product_category", dt.Rows[i]["product-category"].ToString());
                  cmd.Parameters.AddWithValue("@Spec1", dt.Rows[i]["spec1"].ToString());
                  cmd.Parameters.AddWithValue("@Spec2", dt.Rows[i]["spec2"].ToString());
                  cmd.Parameters.AddWithValue("@Spec3", dt.Rows[i]["spec3"].ToString());
                  cmd.Parameters.AddWithValue("@Spec4", dt.Rows[i]["spec4"].ToString());
                  cmd.Parameters.AddWithValue("@Spec5", dt.Rows[i]["spec5"].ToString());
                  if (!String.IsNullOrWhiteSpace(dt.Rows[i]["display"].ToString()))
                      cmd.Parameters.AddWithValue("@Display", (int)dt.Rows[i]["display"]);
                  else
                      cmd.Parameters.AddWithValue("@Display", DBNull.Value);
                  cmd.Parameters.AddWithValue("@Promotion_Type", promotiontype);
                  cmd.Parameters.AddWithValue("@option", option);
                  cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                  cmd.Connection.Open();
                  cmd.ExecuteNonQuery();
                  cmd.Connection.Close();
                  eid = Convert.ToInt32(cmd.Parameters["@result"].Value);
              }
              return eid;
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      public int Ponpare_Campaignlog_Insert( int csv, DataTable dt, string selectctrl, string choicetype)
      {
          try
          {
              int eid = 0;
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              if (dt != null && dt.Rows.Count > 0)
              {
                  for (int i = 0; i < dt.Rows.Count; i++)
                  {
                      SqlCommand cmd = new SqlCommand("SP_Ponpare_CampainglogInsert", connectionString);
                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.CommandTimeout = 0;

                      cmd.Parameters.AddWithValue("@Item_Code", dt.Rows[i]["商品ID"].ToString());
                      cmd.Parameters.AddWithValue("@Promotion_CSVtype", csv);
                      cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(dt.Rows[i]["PID"].ToString()));
                      cmd.Parameters.AddWithValue("@shopid", Convert.ToInt32(dt.Rows[i]["Shop_ID"].ToString()));

                      cmd.Parameters.AddWithValue("@Ctrl_ID", dt.Rows[i]["コントロールカラム"].ToString());
                      cmd.Parameters.AddWithValue("@Product_URL", dt.Rows[i]["商品管理ID（商品URL）"].ToString());
                      cmd.Parameters.AddWithValue("@Item_Name", dt.Rows[i]["商品名"].ToString());
                      cmd.Parameters.AddWithValue("@PC_Catch_Copy", dt.Rows[i]["キャッチコピー"].ToString());
                      cmd.Parameters.AddWithValue("@Mobile_Catch_Copy", dt.Rows[i]["商品説明(スマートフォン用)"].ToString());
                      cmd.Parameters.AddWithValue("@Itemdescription_Smartphone", dt.Rows[i]["商品説明(2)"].ToString());
                      cmd.Parameters.AddWithValue("@Itemdescription_PC ", dt.Rows[i]["商品説明(1)"].ToString());
                     

                      cmd.Parameters.AddWithValue("@selectCtrl_ID ", selectctrl);
                      cmd.Parameters.AddWithValue("@Choice_Type", choicetype);
                      cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                      cmd.Connection.Open();
                      cmd.ExecuteNonQuery();
                      cmd.Connection.Close();
                      eid = Convert.ToInt32(cmd.Parameters["@result"].Value);
                  }
              }//if
            
              return eid;
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
       //updated date 16/06/2015
      
      public void ChangestatusFlag(int status, string ID, int option)
      {
          
              string query = "SP_PointExportstatus_flagchange";
              SqlConnection con = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand(query, con);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("@status",status);
              cmd.Parameters.AddWithValue("@ID",ID);
              cmd.Parameters.AddWithValue("@option",option);
              cmd.Connection.Open();
              cmd.ExecuteNonQuery();
              cmd.Connection.Close();
       
      }


      public int SelectPitemID(string ItemCode,String Shop_ID)
      {
          try
          {
              SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
              SqlCommand cmd = new SqlCommand("SP_Campaign_SelectItemCode", connectionString);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
              cmd.Parameters.AddWithValue("@Item_Code", ItemCode);
              cmd.Parameters.AddWithValue("@Shop_ID", Shop_ID);
              cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
              cmd.Connection.Open();
              cmd.ExecuteNonQuery();
              cmd.Connection.Close();

              if (cmd.Parameters["@result"].Value != DBNull.Value)
                  return Convert.ToInt32(cmd.Parameters["@result"].Value);
              else return 0;
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
    }
}
