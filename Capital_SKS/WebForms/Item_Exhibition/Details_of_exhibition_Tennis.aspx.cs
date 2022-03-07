using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;
using ORS_RCM_BL;
using Capital_SKS.WebForms.Item;
using System;

namespace Capital_SKS.WebForms.Item_Exhibition
{
    public partial class Details_of_exhibition_Tennis : System.Web.UI.Page
    {

        public int Shop_ID
        {
            get
            {
                if (Request.QueryString["Shop_ID"] != null)
                {
                    return int.Parse(Request.QueryString["Shop_ID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Item_ID
        {
            get
            {
                if (Request.QueryString["Item_ID"] != null)
                {
                    return int.Parse(Request.QueryString["Item_ID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }
        Details_of_exhibition_Tennis_BL tcbl = new Details_of_exhibition_Tennis_BL();
        Exhibition_List_BL ehb;
        public string list; string itemcode;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    DataTable dterror = new DataTable();
                    Exhibition_List_BL ehb;
                    tcbl = new Details_of_exhibition_Tennis_BL();
                    int id = Shop_ID;
                    hfshopid.Value = Convert.ToString(id);
                    int itemid = Item_ID;
                    string itid = Convert.ToString(itemid);
                    string Ctrl_ID = GetData1(itemid, id);
                    if (Ctrl_ID != "d" || Ctrl_ID == "")
                    {
                        DataTable dts = tcbl.SelectByExhibitionData(id, itid, "itemselect");        //select COLUMNS from SP_Log_Exhibition_Select_SelectForRakuten where Exhibit_ID=itemid and Shop_ID=id ORDER BY ...
                        gvSKU.DataSource = dts;
                        gvSKU.DataBind();

                        //////string ids = Convert.ToString(id);
                        //////DataTable dtc = tcbl.SelectByExhibitionData(id, itid, "itemcat");       //select COLUMNS from SP_Log_Exhibition_Category_SelectForRakuten where Exhibit_ID=itemid and Shop_ID=id 
                        //////gvSKU.DataSource = dtc;
                        //////gvSKU.DataBind();
                    }


                    ehb = new Exhibition_List_BL();
                    itemcode = lblProductCode.Text;

                    dterror = ehb.Selectexerror(itemcode, id, 2, itemid);
                    if (dterror != null && dterror.Rows.Count > 0)
                    {
                        for (int i = 0; i < dterror.Rows.Count; i++)
                            lblitemerror.Text += dterror.Rows[i]["Error_Description"].ToString() + "\n\n";
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        public string GetData1(int itemid, int shopid)
        {
            try
            {
                string Ctrl_ID = String.Empty;
                int ids = Shop_ID;

                string itid = Convert.ToString(itemid);
                DataTable dt = tcbl.SelectByExhibitionData(ids, itid, "item");
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["コントロールカラム"].ToString() != "d")
                    {
                        Ctrl_ID = dt.Rows[0]["コントロールカラム"].ToString(); // to return value;
                        lblcontrol.Text = dt.Rows[0]["コントロールカラム"].ToString();
                        lblProductCode.Text = dt.Rows[0]["商品グループコード"].ToString();
                        lblProductName.Text = dt.Rows[0]["商品名"].ToString();
                        lblSellPrice.Text = dt.Rows[0]["販売価格"].ToString();
                        lblListPrice.Text = dt.Rows[0]["定価"].ToString();
                        txtDescription.Text = dt.Rows[0]["文言(HTML)"].ToString();
                        lblCategory.Text = dt.Rows[0]["カテゴリID"].ToString();
                        lblBrandName.Text = dt.Rows[0]["メーカー・ブランド"].ToString();
                        lblOptTitle.Text = dt.Rows[0]["オプションタイトル"].ToString();
                        lblOptChoice.Text = dt.Rows[0]["オプション選択肢"].ToString();
                        lblShipFlag.Text = dt.Rows[0]["送料込フラグ"].ToString();
                        lblMail.Text = dt.Rows[0]["メール便可否"].ToString();
                        lblPick.Text = dt.Rows[0]["店舗引取可否"].ToString();
                        lblDeliveryCD.Text = dt.Rows[0]["直送方法CD"].ToString();
                        lblExhibit.Text = dt.Rows[0]["出品フラグ"].ToString();
                        lblReleaseDate.Text = dt.Rows[0]["掲載開始日"].ToString();
                        lblmainFile.Text = dt.Rows[0]["商品メインファイル名"].ToString();
                        lblDetailFile.Text = dt.Rows[0]["明細表示画像ファイル名"].ToString();
                        lblDisplayFile.Text = dt.Rows[0]["ポップ表示ファイル名"].ToString();
                        lblImg1.Text = dt.Rows[0]["画像１ファイル名"].ToString();
                        lblImg2.Text = dt.Rows[0]["画像２ファイル名"].ToString();
                        lblImg3.Text = dt.Rows[0]["画像３ファイル名"].ToString();
                        lblImg4.Text = dt.Rows[0]["画像４ファイル名"].ToString();
                        lblImg5.Text = dt.Rows[0]["画像５ファイル名"].ToString();
                        lblImg6.Text = dt.Rows[0]["画像６ファイル名"].ToString();
                        lblImg7.Text = dt.Rows[0]["画像７ファイル名"].ToString();
                        lblImg8.Text = dt.Rows[0]["画像８ファイル名"].ToString();
                        lblImg9.Text = dt.Rows[0]["画像９ファイル名"].ToString();
                        lblImg10.Text = dt.Rows[0]["画像１０ファイル名"].ToString();
                        lblImg11.Text = dt.Rows[0]["画像１１ファイル名"].ToString();
                        lblImg12.Text = dt.Rows[0]["画像１２ファイル名"].ToString();
                        lblImg13.Text = dt.Rows[0]["画像１３ファイル名"].ToString();
                        lblImg14.Text = dt.Rows[0]["画像１４ファイル名"].ToString();
                        lblImg15.Text = dt.Rows[0]["画像１５ファイル名"].ToString();
                        lblImg16.Text = dt.Rows[0]["画像１６ファイル名"].ToString();
                        lblImg17.Text = dt.Rows[0]["画像１７ファイル名"].ToString();
                        lblImg18.Text = dt.Rows[0]["画像１８ファイル名"].ToString();
                        lblImg19.Text = dt.Rows[0]["画像１９ファイル名"].ToString();
                        lblImg20.Text = dt.Rows[0]["画像２０ファイル名"].ToString();
                        lblTag.Text = dt.Rows[0]["タグ情報"].ToString();
                        lblTagSearch.Text = dt.Rows[0]["検索タグ情報"].ToString();
                        lblCatchCopy.Text = dt.Rows[0]["キャッチコピー"].ToString();
                        lblPoint.Text = dt.Rows[0]["ポイント率"].ToString();
                        lblDiscount.Text = dt.Rows[0]["割引率"].ToString();
                    }
                    else
                    {
                        Ctrl_ID = dt.Rows[0]["コントロールカラム"].ToString(); // to return value;
                        lblcontrol.Text = dt.Rows[0]["コントロールカラム"].ToString();
                        lblProductCode.Text = dt.Rows[0]["商品グループコード"].ToString();
                    }
                }
                //}
                return Ctrl_ID;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
        }



        protected void gvSKU_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                tcbl = new Details_of_exhibition_Tennis_BL();
                ehb = new Exhibition_List_BL();
                int id = Int16.Parse(hfshopid.Value);
                int itemid = Item_ID;
                string itid = Convert.ToString(itemid);
                gvSKU.PageIndex = e.NewPageIndex;
                DataTable dts = tcbl.SelectByExhibitionData(id, itid, "itemselect");
                gvSKU.DataSource = dts;
                gvSKU.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}