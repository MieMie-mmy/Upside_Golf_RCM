using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;
using ORS_RCM_BL;
using System.Configuration;
using System.Transactions;
using System.Globalization;
using System.Collections;


namespace ORS_RCM
{
    public partial class Campaign_promotion_Confirm : System.Web.UI.Page
    {
        Promotion_Entity promotion;
        Campaign_Entity campaign;
        Promotion_BL promotionBL;
        Promotion_Item_BL promotionItemBL;
        Promotion_ItemOptionBL optionBL;
        Promotion_Attatchment_BL promotionAttatchmentBL;
        Shop_BL shopBL;
        Promotion_Shop_BL promotionShopBL;
        string imagePath = ConfigurationManager.AppSettings["ItemImage"].ToString();
        string promotionAttatchmentPath = ConfigurationManager.AppSettings["PromotionAttatchment"].ToString();
        ArrayList myDatatable;
        string ControlID = "";
        public DataTable ItemList
        {
            get
            {
                if (Session["ItemCode"] != null)
                {

                    DataTable dt = (DataTable)Session["ItemCode"];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public int PID
        {
            get
            {
                if (Request.QueryString["ID"] != null)
                {
                    return int.Parse(Request.QueryString["ID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        public string Status
        {
            get
            {
                if (Request.QueryString["status"] != null)
                {
                    return Request.QueryString["status"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        public DataTable ImageList
        {
            get
            {
                if (Session["ImageList"] != null)
                {
                    DataTable dt = (DataTable)Session["ImageList"];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable Option
        {
            get
            {
                if (Session["Option"] != null)
                {
                    DataTable dt = (DataTable)Session["Option"];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindPhotoList();
                    String backpage = string.Empty;
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                        backpage = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["UrlReferrer"] = backpage;
                    }
                    Campaign_Entity ce1 = Session["Campaign_entity"] as Campaign_Entity;
                    Promotion_Entity pe = new Promotion_Entity();
                    Confirm(ce1);
                    gvitem.DataSource = ItemList;
                    gvitem.DataBind();
                    SetAttatchment(ce1);
                }
                else
                {
                    BindPhotoList();
                }
            }

            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void Confirm(Campaign_Entity ce1)
        {
            fuRakuten_Gold1.Visible = false;
            fuRakuten_Gold2.Visible = false;
            fuRakuten_Gold3.Visible = false;
            fuRakuten_Gold4.Visible = false;
            fuRakuten_Cabinet1.Visible = false;
            fuRakuten_Cabinet2.Visible = false;
            fuRakuten_Cabinet3.Visible = false;
            fuRakuten_Cabinet4.Visible = false;
            fuPonpare1.Visible = false;
            fuPonpare2.Visible = false;
            fuPonpare3.Visible = false;
            fuPonpare4.Visible = false;
            fuYahoo1.Visible = false;
            fuYahoo2.Visible = false;
            fuYahoo3.Visible = false;
            fuYahoo4.Visible = false;
            lblshippingno.Text = ce1.Item;
            lblOp1.Text = ce1.OptionName1;
            lblOp2.Text = ce1.OptionName2;
            lblOp3.Text = ce1.OptionName3;
            lblOpVal1.Text = ce1.OptionValue1;
            lblOpVal2.Text = ce1.OptionValue2;
            lblOpVal3.Text = ce1.OptionValue3;
            if (ce1.IsPromotionClose == 1)
            {
                lblproclose.Text = "強制的に終了する";
            }
            else
            {
                lblproclose.Text = "なし";
            }
            lblCampaignID.Text = ce1.Campaign_ID;
            lblCamName.Text = ce1.Promotion_Name;
            if (ce1.Campaign_TypeID == "0")
            {
                lblCampaingtype.Text = "商品別ポイント";
            }
            else if (ce1.Campaign_TypeID == "1")
            {
                lblCampaingtype.Text = "商品別ポイント";
            }
            else if (ce1.Campaign_TypeID == "2")
            {
                lblCampaingtype.Text = "商品別クーポン";
            }
            else if (ce1.Campaign_TypeID == "3")
            {
                lblCampaingtype.Text = "送料";
            }
            else if (ce1.Campaign_TypeID == "4")
            {
                lblCampaingtype.Text = "即日出荷";
            }
            else if (ce1.Campaign_TypeID == "5")
            {
                lblCampaingtype.Text = "予約";
            }
            else if (ce1.Campaign_TypeID == "6")
            {
                lblCampaingtype.Text = "事前告知";
            }
            else if (ce1.Campaign_TypeID == "7")
            {
                lblCampaingtype.Text = "シークレットセール";
            }
            else
            {
                lblCampaingtype.Text = "プレゼントキャンペーン";
            }

            lbltxtCam_Guideline.Text = ce1.Campaign_Guideline;

            lblemailMagzine1.Text = ce1.Mail_Magazine_Event1;

            lblemailMagzine2.Text = ce1.Mail_Magazine_Event2;

            lblemailMagzine3.Text = ce1.Mail_Magazine_Event3;

            lblInstructionNo.Text = ce1.Instruction_No;

            lblCampaignpc.Text = ce1.CampaignUrl_PC;

            lblCampaign_smart.Text = ce1.CampaignUrl_Smart;

            lblpc_campaign1.Text = ce1.PC_Campaign1;

            lblpc_campaign2.Text = ce1.PC_Campaign2;

            lblpcCatchCopy.Text = ce1.PC_Copy_Decoration;

            lblbrand_name.Text = ce1.Target_Brand;

            lblTargetshop.Text = ce1.Shop_Name;

            lblsmart1.Text = ce1.Smart_Campaign1;

            lblsmart2.Text = ce1.Smart_Campaign2;

            lblshippingno.Text = ce1.Item;

            lblProduction_target.Text = ce1.Production_Target;

            lblItem_memo.Text = ce1.Item_Memo;

            lblGift_Contents.Text = ce1.Present_Contents;

            lblGiftway.Text = ce1.Present_Method;

            lblSubject.Text = ce1.Subjects;

            lblPeriod_from.Text = Convert.ToString(ce1.Period_From);

            lblperiod_to.Text = Convert.ToString(ce1.Period_To);

            lblStart_time.Text = Convert.ToString(ce1.Period_StartTime);

            lblEndTime.Text = Convert.ToString(ce1.Period_EndTime);

            if (!String.IsNullOrWhiteSpace(ce1.Gold_attach1))
            {
                string newPath = ce1.Gold_attach1;
                string[] val = newPath.Split('\\');
                string FileName1 = val[val.Length - 1];
                lblRakuten_Gold1.Text = FileName1;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Gold_attach2))
            {
                string newPath = ce1.Gold_attach2;
                string[] val1 = newPath.Split('\\');
                string FileName2 = val1[val1.Length - 1];
                lblRakuten_Gold2.Text = FileName2;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Gold_attach3))
            {
                string newPath = ce1.Gold_attach3;
                string[] val2 = newPath.Split('\\');
                string FileName3 = val2[val2.Length - 1];
                lblRakuten_Gold3.Text = FileName3;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Gold_attach4))
            {
                string newPath = ce1.Gold_attach4;
                string[] val3 = newPath.Split('\\');
                string FileName4 = val3[val3.Length - 1];
                lblRakuten_Gold4.Text = FileName4;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Cabinet_attach1))
            {
                string newPath = ce1.Cabinet_attach1;
                string[] val4 = newPath.Split('\\');
                string FileName5 = val4[val4.Length - 1];
                lblRakuten_Cabinet1.Text = FileName5;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Cabinet_attach2))
            {
                string newPath = ce1.Cabinet_attach2;
                string[] val5 = newPath.Split('\\');
                string FileName6 = val5[val5.Length - 1];
                lblRakuten_Cabinet2.Text = FileName6;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Cabinet_attach3))
            {
                string newPath = ce1.Cabinet_attach3;
                string[] val6 = newPath.Split('\\');
                string FileName7 = val6[val6.Length - 1];
                lblRakuten_Cabinet3.Text = FileName7;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Cabinet_attach4))
            {
                string newPath = ce1.Cabinet_attach4;
                string[] val7 = newPath.Split('\\');
                string FileName8 = val7[val7.Length - 1];
                lblRakuten_Cabinet4.Text = FileName8;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Geocities_attach1))
            {
                string newPath = ce1.Geocities_attach1;
                string[] val8 = newPath.Split('\\');
                string FileName9 = val8[val8.Length - 1];
                lblYahoo1.Text = FileName9;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Geocities_attach2))
            {
                string newPath = ce1.Geocities_attach2;
                string[] val9 = newPath.Split('\\');
                string FileName10 = val9[val9.Length - 1];
                lblYahoo2.Text = FileName10;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Geocities_attach3))
            {
                string newPath = ce1.Geocities_attach3;
                string[] val10 = newPath.Split('\\');
                string FileName11 = val10[val10.Length - 1];
                lblYahoo3.Text = FileName11;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Geocities_attach4))
            {
                string newPath = ce1.Geocities_attach4;
                string[] val11 = newPath.Split('\\');
                string FileName12 = val11[val11.Length - 1];
                lblYahoo4.Text = FileName12;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Ponpare_attach1))
            {
                string newPath = ce1.Ponpare_attach1;
                string[] val12 = newPath.Split('\\');
                string FileName13 = val12[val12.Length - 1];
                lblPonpare1.Text = FileName13;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Ponpare_attach2))
            {
                string newPath = ce1.Ponpare_attach2;
                string[] val13 = newPath.Split('\\');
                string FileName14 = val13[val13.Length - 1];
                lblPonpare2.Text = FileName14;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Ponpare_attach3))
            {
                string newPath = ce1.Ponpare_attach3;
                string[] val14 = newPath.Split('\\');
                string FileName15 = val14[val14.Length - 1];
                lblPonpare3.Text = FileName15;
            }
            if (!String.IsNullOrWhiteSpace(ce1.Ponpare_attach4))
            {
                string newPath = ce1.Ponpare_attach4;
                string[] val15 = newPath.Split('\\');
                string FileName16 = val15[val15.Length - 1];
                lblPonpare4.Text = FileName16;
            }
            lblPriority.Text = ce1.Priority;

            lblproduction_detail.Text = ce1.Production_Detail;

            lblProduction_target.Text = ce1.Production_Target;

            lblApplicationMethod.Text = ce1.Application_Method;

            lblproduct_namedecoration.Text = ce1.Product_Decoration;

            if (ce1.IsPublic == false)
            {
                lblPublic.Text = "非公開";
            }
            else
            {
                lblPublic.Text = "公開";
            }

            if (ce1.IsPresent == true)
            {
                lblchkGift.Text = "あり";
            }
            else
            {
                lblchkGift.Text = "なし";
            }

            if (ce1.Black_market_Setting == true)
            {
                lblBlackmarket.Text = "あり";
            }

            if (ce1.Black_market_Setting == false)
            {
                lblBlackmarket.Text = "なし";
            }

            lblSecretID.Text = ce1.Secret_ID;

            lblSecPassword.Text = ce1.Secret_Password;

            lblsmart_Catchcopy.Text = ce1.Smart_Copy_Decoration;

            lblRemark.Text = ce1.Remark;

            lblRelatedInformation.Text = ce1.Related_Info_Ref;

            if (ce1.Status == 0)
            { lblstatus.Text = "開催前"; }

            else if (ce1.Status == 1)
            { lblstatus.Text = "開催中"; }

            else if (ce1.Status == 2)
            { lblstatus.Text = "終了"; }

            else
            {
                lblstatus.Text = "中止";
            }

            if (ce1.IsPromotionClose == 0)
            {
                lblproclose.Text = "False";
            }
            else
            {
                lblproclose.Text = "強制的に終了する";
            }

            lblRemark.Text = ce1.Remark;

            if (ce1.Status == 0)
            {
                lblstatus.Text = "開催前";
            }

            else if (ce1.Status == 1)
            {
                lblstatus.Text = "開催中";
            }
            else if (ce1.Status == 2)
            {
                lblstatus.Text = "終了";
            }
            else
            {
                lblstatus.Text = "中止";
            }

            if (ImageList != null && (ImageList.Rows.Count > 0))
            {
                DataTable dtImage = ImageList as DataTable;
                if (dtImage.Rows.Count > 0)
                {
                    ce1.Campaign_Image1 = dtImage.Rows[0]["Item_Image1"] + "";
                }
                if (dtImage.Rows.Count > 0)
                {
                    ce1.Campaign_Image2 = dtImage.Rows[0]["Item_Image2"] + "";
                }
                if (dtImage.Rows.Count > 0)
                {
                    ce1.Campaign_Image3 = dtImage.Rows[0]["Item_Image3"] + "";
                }
                if (dtImage.Rows.Count > 0)
                {
                    ce1.Campaign_Image4 = dtImage.Rows[0]["Item_Image4"] + "";
                }
                if (dtImage.Rows.Count > 0)
                {
                    ce1.Campaign_Image5 = dtImage.Rows[0]["Item_Image5"] + "";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            promotionBL = new Promotion_BL();
            promotionItemBL = new Promotion_Item_BL();
            optionBL = new Promotion_ItemOptionBL();
            Promotion_Entity pe = new Promotion_Entity();
            if (PID == 0)
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    if ((!String.IsNullOrWhiteSpace(lblCampaignID.Text)) && (promotionBL.Record_CampaginIDexisted(lblCampaignID.Text)))
                    {
                        MessageBox("Campaign_ID already exists.");
                    }
                    else
                    {
                        promotionBL = new Promotion_BL();
                        Campaign_Entity ce1 = Session["Campaign_entity"] as Campaign_Entity;
                        int promotionID = promotionBL.SaveUpdate(ce1, "Save");

                        string[] arr = lblshippingno.Text.Split(',');
                        arr = arr.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        DataTable dt = GETItemCode();
                        if (dt != null && dt.Rows.Count > 0)
                            promotionItemBL.Insert_CampaignPromotion_Item(promotionID, dt);
                        if (promotionID > 0)
                        {
                            SaveUpdateOption(promotionID);
                            SavePromotionAttatchments(promotionID);
                            SaveShops(promotionID);
                            MessageBox("Saving Successful ! ");
                        }
                        tran.Complete();
                        Session.Remove("ItemCode");
                        Session.Remove("ImageList");
                        Session["ImageList"] = null;
                        Session["ItemCode"] = null;
                    }
                }
            }
            else
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    promotionBL = new Promotion_BL();
                    Campaign_Entity ce1 = Session["Campaign_entity"] as Campaign_Entity;
                    int promotionID = promotionBL.SaveUpdate(ce1, "Update");
                    promotionBL = new Promotion_BL();
                    DataTable dtduplicate = promotionBL.Duplicate_CampaignID(lblCampaignID.Text, promotionID);
                    if (dtduplicate.Rows.Count == 1)
                    {
                        GlobalUI.MessageBox("Campaign_ID already existed");
                    }
                    else
                    {
                        string[] arr = lblshippingno.Text.Split(',');
                        arr = arr.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        promotionItemBL.Insert_Promotion_Item(promotionID, arr);
                        SaveUpdateOption(promotionID);
                        SavePromotionAttatchments(promotionID);
                        SaveShops(promotionID);
                        MessageBox("Updating Successful ! ");
                        tran.Complete();
                        Session.Remove("ItemCode");
                        Session.Remove("ImageList");
                        Session["ImageList"] = null;
                        Session["ItemCode"] = null;
                    }
                }
            }
        }

        public void SaveShops(int promotionID)
        {
            promotionShopBL = new Promotion_Shop_BL();
            Campaign_Entity ce1 = Session["Campaign_entity"] as Campaign_Entity;
            DataTable dt = new DataTable();
            dt.Columns.Add("PromotionID", typeof(int));
            dt.Columns.Add("ShopID", typeof(int));

            if (!String.IsNullOrWhiteSpace(ce1.Shop))
            {
                string s = ce1.Shop;
                string[] values = s.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                    dt.Rows.Add(promotionID, int.Parse(values[i]));
                }

                promotionShopBL.Insert(dt);
            }
        }

        public void SaveUpdateOption(int promotionID)
        {
            optionBL = new Promotion_ItemOptionBL();
            optionBL.Save(promotionID, GetOption(promotionID));
        }

        public DataTable GetOption(int promotionID)
        {
            if (Session["myDatatable"] != null)
            {
                myDatatable = (ArrayList)Session["myDatatable"];
                lblOp1.Text = myDatatable[0].ToString();
                lblOp2.Text = myDatatable[1].ToString();
                lblOp3.Text = myDatatable[22].ToString();
                lblOpVal1.Text = myDatatable[23].ToString();
                lblOpVal2.Text = myDatatable[24].ToString();
                lblOpVal3.Text = myDatatable[25].ToString();
            }
            DataTable dt = new DataTable();
            DataRow dr1, dr2, dr3 = null;
            //define the columns
            dt.Columns.Add(new DataColumn("Promotion_ID", typeof(int)));
            dt.Columns.Add(new DataColumn("Option_Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Option_Value", typeof(string)));
            //create new row
            dr1 = dt.NewRow();
            dr2 = dt.NewRow();
            dr3 = dt.NewRow();
            //add values to each rows
            if (PID == 0)
            {
                dr1["Promotion_ID"] = promotionID;
            }
            else
            {
                dr1["Promotion_ID"] = PID;
            }
            dr1["Option_Name"] = lblOp1.Text;
            dr1["Option_Value"] = lblOpVal1.Text;
            //add the row to DataTable
            dt.Rows.Add(dr1);
            if (PID == 0)
            {
                dr2["Promotion_ID"] = promotionID;
            }
            else
            {
                dr2["Promotion_ID"] = PID;
            }
            dr2["Option_Name"] = lblOp2.Text;
            dr2["Option_Value"] = lblOpVal2.Text;
            dt.Rows.Add(dr2);
            if (PID == 0)
            {
                dr3["Promotion_ID"] = promotionID;
            }
            else
            {
                dr3["Promotion_ID"] = PID;
            }
            dr3["Option_Name"] = lblOp3.Text;
            dr3["Option_Value"] = lblOpVal3.Text;
            dt.Rows.Add(dr3);
            return dt;
        }

        protected DataTable GETItemCode()
        {
            promotionItemBL = new Promotion_Item_BL();
            DataTable dtM = new DataTable();
            dtM = promotionItemBL.SelectByPromotionID(PID);
            if (gvitem.Rows.Count > 0)
            {
                for (int y = 0; y < gvitem.Rows.Count; y++)
                {
                    Label itemcode = gvitem.Rows[y].FindControl("lblitem") as Label;
                    DataRow dr = dtM.NewRow();
                    dtM.Rows.Add(dr);
                    dtM.Rows[y]["Item_Code"] = itemcode.Text;
                }
                dtM.Merge(ItemList);
                dtM = RemoveDuplicateRows(dtM, "Item_Code");
                gvitem.DataSource = dtM;
                gvitem.DataBind();
                return dtM;
            }
            else { return null; }
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            try
            {
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();

                //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                //And add duplicate item value in arraylist.
                foreach (DataRow drow in dTable.Rows)
                {
                    if (hTable.Contains(drow[colName]))
                        duplicateList.Add(drow);
                    else
                        hTable.Add(drow[colName], string.Empty);
                }

                //Removing a list of duplicate items from datatable.
                foreach (DataRow dRow in duplicateList)
                    dTable.Rows.Remove(dRow);

                //Datatable which contains unique records will be return as output.
                return dTable;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            }
        }

        public void BindPhotoList()
        {
            promotion = new Promotion_Entity();
            promotionBL = new Promotion_BL();
            if (ImageList != null)
            {
                DataTable dt = ImageList as DataTable;
                #region Item Image
                #region Set value
                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image1"].ToString()))
                    {
                        Image1.ImageUrl = imagePath + dt.Rows[0]["Item_Image1"] + "";
                        hlImage1.NavigateUrl = imagePath + dt.Rows[0]["Item_Image1"] + "";
                    }
                    else
                    {
                        Image1.ImageUrl = "";
                        hlImage1.NavigateUrl = "";
                    }
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image2"].ToString()))
                    {
                        Image2.ImageUrl = imagePath + dt.Rows[0]["Item_Image2"] + "";
                        hlImage2.NavigateUrl = imagePath + dt.Rows[0]["Item_Image2"];
                    }
                    else
                    {
                        Image2.ImageUrl = "";
                        hlImage2.NavigateUrl = "";
                    }
                }
                if (!(dt.Rows[0]["Item_Image3"].ToString() == "Item_Image3"))
                {
                    Image3.ImageUrl = imagePath + dt.Rows[0]["Item_Image3"] + "";
                    hlImage3.NavigateUrl = imagePath + dt.Rows[0]["Item_Image3"];
                }
                else
                {
                    Image3.ImageUrl = "";
                    hlImage3.NavigateUrl = "";
                }
                if ((!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image4"].ToString())))
                {
                    Image4.ImageUrl = imagePath + dt.Rows[0]["Item_Image4"] + "";
                    hlImage4.NavigateUrl = imagePath + dt.Rows[0]["Item_Image4"];
                }
                else
                {
                    Image4.ImageUrl = "";
                    hlImage4.NavigateUrl = "";
                }
                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image5"].ToString()))
                {
                    Image5.ImageUrl = imagePath + dt.Rows[0]["Item_Image5"] + "";
                    hlImage5.NavigateUrl = imagePath + dt.Rows[0]["Item_Image5"];
                }
                else
                {
                    Image5.ImageUrl = "";
                    hlImage5.NavigateUrl = "";
                }
                #endregion
            }
            else
            {
                Image1.ImageUrl = "";
                Image2.ImageUrl = "";
                Image3.ImageUrl = "";
                Image4.ImageUrl = "";
                Image5.ImageUrl = "";
                hlImage1.NavigateUrl = "";
                hlImage2.NavigateUrl = "";
                hlImage3.NavigateUrl = "";
                hlImage4.NavigateUrl = "";
                hlImage5.NavigateUrl = "";
            }
                #endregion
            #region
            #endregion
        }

        public void SavePromotionAttatchments(int promotionID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Promotion_ID", typeof(int));
            dt.Columns.Add("File_Name", typeof(string));
            dt.Columns.Add("File_Type", typeof(int));
            string fullAttatchmentPath = Server.MapPath(promotionAttatchmentPath);
            if (fuRakuten_Gold1.HasFile)
            {
                fuRakuten_Gold1.SaveAs(fullAttatchmentPath + fuRakuten_Gold1.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Gold1.FileName, 0);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Gold1.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Gold1.Text, 0);
            }
            if (fuRakuten_Gold2.HasFile)
            {
                fuRakuten_Gold2.SaveAs(fullAttatchmentPath + fuRakuten_Gold2.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Gold2.FileName, 0);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Gold2.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Gold2.Text, 0);
            }
            if (fuRakuten_Gold3.HasFile)
            {
                fuRakuten_Gold3.SaveAs(fullAttatchmentPath + fuRakuten_Gold3.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Gold3.FileName, 0);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Gold3.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Gold3.Text, 0);
            }
            if (fuRakuten_Gold4.HasFile)
            {
                fuRakuten_Gold4.SaveAs(fullAttatchmentPath + fuRakuten_Gold4.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Gold4.FileName, 0);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Gold4.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Gold4.Text, 0);
            }
            if (fuRakuten_Cabinet1.HasFile)
            {
                fuRakuten_Cabinet1.SaveAs(fullAttatchmentPath + fuRakuten_Cabinet1.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Cabinet1.FileName, 1);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Cabinet1.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Cabinet1.Text, 1);
            }
            if (fuRakuten_Cabinet2.HasFile)
            {
                fuRakuten_Cabinet2.SaveAs(fullAttatchmentPath + fuRakuten_Cabinet2.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Cabinet2.FileName, 1);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Cabinet2.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Cabinet2.Text, 1);
            }
            if (fuRakuten_Cabinet3.HasFile)
            {
                fuRakuten_Cabinet3.SaveAs(fullAttatchmentPath + fuRakuten_Cabinet3.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Cabinet3.FileName, 1);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Cabinet3.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Cabinet3.Text, 1);
            }
            if (fuRakuten_Cabinet4.HasFile)
            {
                fuRakuten_Cabinet4.SaveAs(fullAttatchmentPath + fuRakuten_Cabinet4.FileName);
                dt.Rows.Add(0, promotionID, fuRakuten_Cabinet4.FileName, 1);
            }
            else if (!String.IsNullOrEmpty(lblRakuten_Cabinet4.Text))
            {
                dt.Rows.Add(0, promotionID, lblRakuten_Cabinet4.Text, 1);
            }
            if (fuYahoo1.HasFile)
            {
                fuYahoo1.SaveAs(fullAttatchmentPath + fuYahoo1.FileName);
                dt.Rows.Add(0, promotionID, fuYahoo1.FileName, 2);
            }
            else if (!String.IsNullOrEmpty(lblYahoo1.Text))
            {
                dt.Rows.Add(0, promotionID, lblYahoo1.Text, 2);
            }
            if (fuYahoo2.HasFile)
            {
                fuYahoo2.SaveAs(fullAttatchmentPath + fuYahoo2.FileName);
                dt.Rows.Add(0, promotionID, fuYahoo2.FileName, 2);
            }
            else if (!String.IsNullOrEmpty(lblYahoo2.Text))
            {
                dt.Rows.Add(0, promotionID, lblYahoo2.Text, 2);
            }
            if (fuYahoo3.HasFile)
            {
                fuYahoo3.SaveAs(fullAttatchmentPath + fuYahoo3.FileName);
                dt.Rows.Add(0, promotionID, fuYahoo3.FileName, 2);
            }
            else if (!String.IsNullOrEmpty(lblYahoo3.Text))
            {
                dt.Rows.Add(0, promotionID, lblYahoo3.Text, 2);
            }
            if (fuYahoo4.HasFile)
            {
                fuYahoo4.SaveAs(fullAttatchmentPath + fuYahoo4.FileName);
                dt.Rows.Add(0, promotionID, fuYahoo4.FileName, 2);
            }
            else if (!String.IsNullOrEmpty(lblYahoo4.Text))
            {
                dt.Rows.Add(0, promotionID, lblYahoo4.Text, 2);
            }
            if (fuPonpare1.HasFile)
            {
                fuPonpare1.SaveAs(fullAttatchmentPath + fuPonpare1.FileName);
                dt.Rows.Add(0, promotionID, fuPonpare1.FileName, 3);
            }
            else if (!String.IsNullOrEmpty(lblPonpare1.Text))
            {
                dt.Rows.Add(0, promotionID, lblPonpare1.Text, 3);
            }
            if (fuPonpare2.HasFile)
            {
                fuPonpare2.SaveAs(fullAttatchmentPath + fuPonpare2.FileName);
                dt.Rows.Add(0, promotionID, fuPonpare2.FileName, 3);
            }
            else if (!String.IsNullOrEmpty(lblPonpare2.Text))
            {
                dt.Rows.Add(0, promotionID, lblPonpare2.Text, 3);
            }
            if (fuPonpare3.HasFile)
            {
                fuPonpare3.SaveAs(fullAttatchmentPath + fuPonpare3.FileName);
                dt.Rows.Add(0, promotionID, fuPonpare3.FileName, 3);
            }
            else if (!String.IsNullOrEmpty(lblPonpare3.Text))
            {
                dt.Rows.Add(0, promotionID, lblPonpare3.Text, 3);
            }
            if (fuPonpare4.HasFile)
            {
                fuPonpare4.SaveAs(fullAttatchmentPath + fuPonpare4.FileName);
                dt.Rows.Add(0, promotionID, fuPonpare4.FileName, 3);
            }
            else if (!String.IsNullOrEmpty(lblPonpare4.Text))
            {
                dt.Rows.Add(0, promotionID, lblPonpare4.Text, 3);
            }
            promotionAttatchmentBL = new Promotion_Attatchment_BL();
            promotionAttatchmentBL.InsertUpdate(dt);
        }

        public void MessageBox(string message)
        {
            if (message == "Saving Successful ! " || message == "Updating Successful ! ")
            {
                object referrer = ViewState["UrlReferrer"];
                string url = "Campaign_promotion_View.aspx";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }
            else
            {
                string cleanMessage = message.Replace("'", "\\'");
                string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
                Page page = HttpContext.Current.CurrentHandler as Page;
                if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                {
                    page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                }
            }
        }

        public void SetAttatchment(Campaign_Entity ce1)
        {
            promotionAttatchmentBL = new Promotion_Attatchment_BL();
            DataTable dt = promotionAttatchmentBL.SelectByPromotionID(PID);
            #region RakutenGold
            DataRow[] rowsRakutenGold = dt.Select("File_Type = 0");
            for (int i = 0; i < rowsRakutenGold.Length; i++)
            {
                if (i == 0)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Gold_attach1))
                    {
                        string newPath = ce1.Gold_attach1;
                        string[] val = newPath.Split('\\');
                        string FileName1 = val[val.Length - 1];
                        lblRakuten_Gold1.Text = FileName1;
                    }
                    else
                    {
                        lblRakuten_Gold1.Text = rowsRakutenGold[i]["File_Name"].ToString();
                        fuRakuten_Gold1.Width = 40;
                    }
                }

                if (i == 1)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Gold_attach2))
                    {
                        string newPath = ce1.Gold_attach2;
                        string[] val1 = newPath.Split('\\');
                        string FileName2 = val1[val1.Length - 1];
                        lblRakuten_Gold2.Text = FileName2;
                    }
                    else
                    {
                        lblRakuten_Gold2.Text = rowsRakutenGold[i]["File_Name"].ToString();
                        fuRakuten_Gold2.Width = 40;
                    }
                }
                if (i == 2)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Gold_attach3))
                    {
                        string newPath = ce1.Gold_attach3;
                        string[] val2 = newPath.Split('\\');
                        string FileName3 = val2[val2.Length - 1];
                        lblRakuten_Gold3.Text = FileName3;
                    }
                    else
                    {
                        lblRakuten_Gold3.Text = rowsRakutenGold[i]["File_Name"].ToString();
                        fuRakuten_Gold3.Width = 40;
                    }
                }

                if (i == 3)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Gold_attach4))
                    {
                        string newPath = ce1.Gold_attach4;
                        string[] val3 = newPath.Split('\\');
                        string FileName4 = val3[val3.Length - 1];
                        lblRakuten_Gold4.Text = FileName4;
                    }
                    else
                    {
                        lblRakuten_Gold4.Text = rowsRakutenGold[i]["File_Name"].ToString();
                        fuRakuten_Gold4.Width = 40;
                    }
                }
            }
            #endregion
            #region RakutenCabinet
            DataRow[] rowsRakutenCabinet = dt.Select("File_Type = 1");
            for (int i = 0; i < rowsRakutenCabinet.Length; i++)
            {
                if (i == 0)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Cabinet_attach1))
                    {
                        string newPath = ce1.Cabinet_attach1;
                        string[] val4 = newPath.Split('\\');
                        string FileName5 = val4[val4.Length - 1];
                        lblRakuten_Cabinet1.Text = FileName5;
                    }
                    else
                    {
                        lblRakuten_Cabinet1.Text = rowsRakutenCabinet[i]["File_Name"].ToString();
                        fuRakuten_Cabinet1.Width = 40;
                    }
                }
                if (i == 1)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Cabinet_attach2))
                    {
                        string newPath = ce1.Cabinet_attach2;
                        string[] val5 = newPath.Split('\\');
                        string FileName6 = val5[val5.Length - 1];
                        lblRakuten_Cabinet2.Text = FileName6;
                    }
                    else
                    {
                        lblRakuten_Cabinet2.Text = rowsRakutenCabinet[i]["File_Name"].ToString();
                        fuRakuten_Cabinet2.Width = 40;
                    }
                }
                if (i == 2)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Cabinet_attach3))
                    {
                        string newPath = ce1.Cabinet_attach3;
                        string[] val6 = newPath.Split('\\');
                        string FileName7 = val6[val6.Length - 1];
                        lblRakuten_Cabinet3.Text = FileName7;
                    }
                    else
                    {
                        lblRakuten_Cabinet3.Text = rowsRakutenCabinet[i]["File_Name"].ToString();
                        fuRakuten_Cabinet3.Width = 40;
                    }
                }
                if (i == 3)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Cabinet_attach4))
                    {
                        string newPath = ce1.Cabinet_attach4;
                        string[] val7 = newPath.Split('\\');
                        string FileName8 = val7[val7.Length - 1];
                        lblRakuten_Cabinet4.Text = FileName8;
                    }
                    else
                    {

                        lblRakuten_Cabinet4.Text = rowsRakutenCabinet[i]["File_Name"].ToString();
                        fuRakuten_Cabinet4.Width = 40;
                    }
                }
            }
            #endregion
            #region Yahoo
            DataRow[] rowsYahoo = dt.Select("File_Type = 2");
            for (int i = 0; i < rowsYahoo.Length; i++)
            {
                if (i == 0)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Geocities_attach1))
                    {
                        string newPath = ce1.Geocities_attach1;
                        string[] val8 = newPath.Split('\\');
                        string FileName9 = val8[val8.Length - 1];
                        lblYahoo1.Text = FileName9;
                    }
                    else
                    {
                        lblYahoo1.Text = rowsYahoo[i]["File_Name"].ToString();
                        fuYahoo1.Width = 40;
                    }
                }
                if (i == 1)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Geocities_attach2))
                    {
                        string newPath = ce1.Geocities_attach2;
                        string[] val9 = newPath.Split('\\');
                        string FileName10 = val9[val9.Length - 1];
                        lblYahoo2.Text = FileName10;
                    }
                    else
                    {
                        lblYahoo2.Text = rowsYahoo[i]["File_Name"].ToString();
                        fuYahoo2.Width = 40;
                    }
                }
                if (i == 2)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Geocities_attach3))
                    {
                        string newPath = ce1.Geocities_attach3;
                        string[] val10 = newPath.Split('\\');
                        string FileName11 = val10[val10.Length - 1];
                        lblYahoo3.Text = FileName11;
                    }
                    else
                    {
                        lblYahoo3.Text = rowsYahoo[i]["File_Name"].ToString();
                        fuYahoo3.Width = 40;
                    }
                }
                if (i == 3)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Geocities_attach4))
                    {
                        string newPath = ce1.Geocities_attach4;
                        string[] val11 = newPath.Split('\\');
                        string FileName12 = val11[val11.Length - 1];
                        lblYahoo4.Text = FileName12;
                    }

                    else
                    {
                        lblYahoo4.Text = rowsYahoo[i]["File_Name"].ToString();
                        fuYahoo4.Width = 40;
                    }
                }
            }
            #endregion
            #region Ponpare
            DataRow[] rowsPonpare = dt.Select("File_Type = 3");
            for (int i = 0; i < rowsPonpare.Length; i++)
            {
                if (i == 0)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Ponpare_attach1))
                    {
                        string newPath = ce1.Ponpare_attach1;
                        string[] val12 = newPath.Split('\\');
                        string FileName13 = val12[val12.Length - 1];
                        lblPonpare1.Text = FileName13;
                    }
                    else
                    {
                        lblPonpare1.Text = rowsPonpare[i]["File_Name"].ToString();
                        fuPonpare1.Width = 40;
                    }
                }
                if (i == 1)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Ponpare_attach2))
                    {
                        string newPath = ce1.Ponpare_attach2;
                        string[] val13 = newPath.Split('\\');
                        string FileName14 = val13[val13.Length - 1];
                        lblPonpare2.Text = FileName14;
                    }
                    else
                    {
                        lblPonpare2.Text = rowsPonpare[i]["File_Name"].ToString();
                        fuPonpare2.Width = 40;
                    }
                }
                if (i == 2)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Ponpare_attach3))
                    {
                        string newPath = ce1.Ponpare_attach3;
                        string[] val14 = newPath.Split('\\');
                        string FileName15 = val14[val14.Length - 1];
                        lblPonpare3.Text = FileName15;
                    }
                    else
                    {
                        lblPonpare3.Text = rowsPonpare[i]["File_Name"].ToString();
                        fuPonpare3.Width = 40;
                    }
                }
                if (i == 3)
                {
                    if (!String.IsNullOrWhiteSpace(ce1.Ponpare_attach4))
                    {
                        string newPath = ce1.Ponpare_attach4;
                        string[] val15 = newPath.Split('\\');
                        string FileName16 = val15[val15.Length - 1];
                        lblPonpare4.Text = FileName16;
                    }
                    else
                    {
                        lblPonpare4.Text = rowsPonpare[i]["File_Name"].ToString();
                        fuPonpare4.Width = 40;
                    }
                }
            }
            #endregion
        }
    }
}
    
