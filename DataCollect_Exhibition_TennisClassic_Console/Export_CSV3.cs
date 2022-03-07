using ORS_RCM_BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataCollect_Exhibition_TennisClassic_Console
{
    class Export_CSV3
    {


        static String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        int start, end, startP, endP;
        string line = string.Empty;
        string strline = string.Empty;
        string replaceword = string.Empty;
        string comment = string.Empty;
        string html = string.Empty;
        string itemcode = string.Empty;
        DataTable dtvalue;
        DataTable dtName = new DataTable();
        string st = string.Empty;
        Shop_Template_BL shopTemplate;
        Item_Master_BL master = new Item_Master_BL();
        Item_BL item = new Item_BL();
        Item_Image_BL imageBL = new Item_Image_BL();
        int MallID = 8;

        public DataTable ModifyTable(DataTable dtItem, int shopID)
        {
            DataTable dtFinal = new DataTable();
            if (dtItem.Rows.Count > 0)
            {
                //if (dtItem.Columns.Contains("Item_Name"))
                //{
                //    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Item_Name"));
                //}
                if (dtItem.Columns.Contains("Item_Description_PC")) // Item_Description_PC (rakuten)
                {
                    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Item_Description_PC"));
                }
                if (dtItem.Columns.Contains("Sale_Description_PC")) // Sale_Description_PC (rakuten)
                {
                    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Sale_Description_PC"));
                }
                if (dtItem.Columns.Contains("Smart_Template")) // Smart_Template (rakuten)
                {
                    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Smart_Template"));
                }
                if (dtItem.Columns.Contains("Explanation")) // Merchandise_Information (yahoo)
                {
                    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Explanation"));
                }
                return dtFinal;
            }
            else
            {
                return null;
            }
        }

        public DataTable ChangeTemplate(DataTable dt, int shopID, string columnName)
        {
            DataColumnCollection columns = dt.Columns;
            if (columns.Contains(columnName))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    html = dr[columnName].ToString();
                    #region itemcode
                    if (columns.Contains("Item_Code"))  //For yahoo
                    {
                        itemcode = dr["Item_Code"].ToString();
                    }
                    #endregion
                    int index = 0;
                    //@"\[([^]]*)\]"
                    while (Regex.IsMatch(html, @"\[\[([^]]*)\]\]"))
                    {
                        #region Break While Loop
                        int value = ++index;
                        if (value >= 10)
                        {
                            break;
                        }
                        #endregion
                        string[] templateID = GetTemplateID(html);  // Read [[ ]] words
                        if (templateID.Length > 0 && String.IsNullOrWhiteSpace(templateID[0].ToString())) // Remove unformat [[ ]] 
                        {
                            html = html.Replace("[", "").Replace("]", "");
                        }
                        #region ShopTemplate
                        dtvalue = new DataTable();
                        dtvalue = GetTemplateDescription(templateID, shopID);  //Get from ShopTemplate Table
                        if (dtvalue != null && dtvalue.Rows.Count > 0)
                        {
                            var replacements = new Dictionary<string, string>();
                            for (int i = 0; i < templateID.Count(); i++)
                            {
                                string Key = templateID[i].ToString();
                                string Value = "";
                                DataRow[] drtemplate = dtvalue.Select("Template_ID='" + Key + "'");
                                if (drtemplate.Count() > 0)  // exist replace value
                                {
                                    Value = dtvalue.Select("Template_ID='" + Key + "'").CopyToDataTable().Rows[0]["Template_Description"].ToString();
                                    if (!replacements.Keys.Contains(Key))
                                    {
                                        replacements.Add(Key, Value);
                                    }
                                }
                                else //  not exist replace value
                                {
                                    if (!replacements.Keys.Contains(Key) && !Key.Contains("IF") &&
                                        !Key.Contains("基本情報") && !Key.Contains("詳細情報") &&
                                        !Key.Contains("product.product_id") && !Key.Contains("商品名") &&
                                        !Key.Contains("商品番号") && !Key.Contains("ブランド名") &&
                                        !Key.Contains("サイズ正式名称") && !Key.Contains("カラー正式名称") &&
                                        !Key.Contains("ゼット用項目（PC商品説明文）") && !Key.Contains("ゼット用項目（PC販売説明文）") &&
                                        !Key.Contains("関連商品") && !Key.Contains("テクノロジー画像") &&
                                        !Key.Contains("キャンペーン画像") && !Key.Contains("商品ページURL") &&
                                        !Key.Contains("定価") && !Key.Contains("販売価格"))
                                    {
                                        replacements.Add(Key, " ");
                                    }
                                }
                            }
                            // Replace
                            foreach (var replacement in replacements)
                            {
                                html = html.SafeReplace(replacement.Key, replacement.Value, true);
                            }
                        }
                        else  // change value is not exists.
                        {
                            string format;
                            if (templateID.Length > 0)
                            {
                                for (int h = 0; h < templateID.Length; h++)
                                {
                                    if (!templateID[h].ToString().Contains("IF") &&
                                        !templateID[h].ToString().Contains("基本情報") && !templateID[h].ToString().Contains("詳細情報") &&
                                        !templateID[h].ToString().Contains("product.product_id") && !templateID[h].ToString().Contains("商品名") &&
                                        !templateID[h].ToString().Contains("商品番号") && !templateID[h].ToString().Contains("ブランド名") &&
                                        !templateID[h].ToString().Contains("サイズ正式名称") && !templateID[h].ToString().Contains("カラー正式名称") &&
                                        !templateID[h].ToString().Contains("ゼット用項目（PC商品説明文）") && !templateID[h].ToString().Contains("ゼット用項目（PC販売説明文）") &&
                                        !templateID[h].ToString().Contains("関連商品") && !templateID[h].ToString().Contains("テクノロジー画像") &&
                                        !templateID[h].ToString().Contains("キャンペーン画像") && !templateID[h].ToString().Contains("商品ページURL") &&
                                        !templateID[h].ToString().Contains("定価") && !templateID[h].ToString().Contains("販売価格"))
                                    {
                                        format = string.Format(@"\[\[{0}\]\]", templateID[h].ToString());
                                        if (Regex.IsMatch(html, format))
                                        {
                                            html = Regex.Replace(html, format, "");
                                        }
                                    }
                                }
                            }
                        }
                        #endregion


                        #region Item_Name
                        if (columnName == "Item_Name")
                        {
                            dtName = GetRelatedName(itemcode, shopID);
                        }
                        if ((columnName == "Item_Name") || (columnName == "Catch_copy_PC"))
                            st = html;
                        #region ブランド
                        if (html.Contains("ブランド") && html.IndexOf("[[IF setVal=ブランド]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=ブランド]]");
                            startP = st.IndexOf("[[IF setVal=ブランド]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Brand_Name"].ToString()))
                            {
                                html = html.SafeReplace("ブランド", dtName.Rows[0]["Brand_Name"].ToString(), true);
                                st = st.SafeReplace("ブランド", dtName.Rows[0]["Brand_Name"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=ブランド]]").Length);
                            }
                            else
                            {
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=ブランド]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region 競技
                        if (html.Contains("競技") && html.IndexOf("[[IF setVal=競技]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=競技]]");
                            startP = st.IndexOf("[[IF setVal=競技]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Competition_Name"].ToString()))
                            {
                                html = html.SafeReplace("競技", dtName.Rows[0]["Competition_Name"].ToString(), true);
                                st = st.SafeReplace("競技", dtName.Rows[0]["Competition_Name"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=競技]]").Length);
                            }
                            else
                            {
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=競技]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region 分類
                        if (html.Contains("分類") && html.IndexOf("[[IF setVal=分類]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=分類]]");
                            startP = st.IndexOf("[[IF setVal=分類]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Class_Name"].ToString()))
                            {
                                html = html.SafeReplace("分類", dtName.Rows[0]["Class_Name"].ToString(), true);
                                st = st.SafeReplace("分類", dtName.Rows[0]["Class_Name"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=分類]]").Length);
                            }
                            else
                            {
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=分類]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region 発売日
                        if (html.Contains("発売日") && html.IndexOf("[[IF setVal=発売日]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=発売日]]");
                            startP = st.IndexOf("[[IF setVal=発売日]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Release_Date"].ToString()))
                            {
                                html = html.SafeReplace("発売日", dtName.Rows[0]["Release_Date"].ToString(), true);
                                st = st.SafeReplace("発売日", dtName.Rows[0]["Release_Date"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=発売日]]").Length);
                            }
                            else
                            {
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=発売日]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region 商品名
                        if (html.Contains("商品名") && html.IndexOf("[[IF setVal=商品名]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=商品名]]");
                            startP = st.IndexOf("[[IF setVal=商品名]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Item_Name"].ToString()))
                            {
                                html = html.SafeReplace("商品名", dtName.Rows[0]["Item_Name"].ToString(), true);
                                st = st.SafeReplace("商品名", dtName.Rows[0]["Item_Name"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=商品名]]").Length);
                            }
                            else
                            {
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=商品名]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region 競技分類1
                        if (html.Contains("競技分類1") && html.IndexOf("[[IF setVal=競技分類1]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=競技分類1]]");
                            startP = st.IndexOf("[[IF setVal=競技分類1]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Search_Word1"].ToString()))
                            {
                                html = html.SafeReplace("競技分類1", dtName.Rows[0]["Search_Word1"].ToString(), true);
                                st = st.SafeReplace("競技分類1", dtName.Rows[0]["Search_Word1"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=競技分類1]]").Length);
                            }
                            else
                            {
                                start = html.IndexOf(" [[IF setVal=競技分類1]]");
                                startP = st.IndexOf(" [[IF setVal=競技分類1]]");
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=競技分類1]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region 競技分類2
                        if (html.Contains("競技分類2") && html.IndexOf("[[IF setVal=競技分類2]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=競技分類2]]");
                            startP = st.IndexOf("[[IF setVal=競技分類2]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Search_Word2"].ToString()))
                            {
                                html = html.SafeReplace("競技分類2", dtName.Rows[0]["Search_Word2"].ToString(), true);
                                st = st.SafeReplace("競技分類2", dtName.Rows[0]["Search_Word2"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=競技分類2]]").Length);
                            }
                            else
                            {
                                start = html.IndexOf(" [[IF setVal=競技分類2]]");
                                startP = st.IndexOf(" [[IF setVal=競技分類2]]");
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=競技分類2]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region 競技分類3
                        if (html.Contains("競技分類3") && html.IndexOf("[[IF setVal=競技分類3]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=競技分類3]]");
                            startP = st.IndexOf("[[IF setVal=競技分類3]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Search_Word3"].ToString()))
                            {
                                html = html.SafeReplace("競技分類3", dtName.Rows[0]["Search_Word3"].ToString(), true);
                                st = st.SafeReplace("競技分類3", dtName.Rows[0]["Search_Word3"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=競技分類3]]").Length);
                            }
                            else
                            {
                                start = html.IndexOf(" [[IF setVal=競技分類3]]");
                                startP = st.IndexOf(" [[IF setVal=競技分類3]]");
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=競技分類3]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region 競技分類4
                        if (html.Contains("競技分類4") && html.IndexOf("[[IF setVal=競技分類4]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=競技分類4]]");
                            startP = st.IndexOf("[[IF setVal=競技分類4]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Search_Word4"].ToString()))
                            {
                                html = html.SafeReplace("競技分類4", dtName.Rows[0]["Search_Word4"].ToString(), true);
                                st = st.SafeReplace("競技分類4", dtName.Rows[0]["Search_Word4"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=競技分類4]]").Length);
                            }
                            else
                            {
                                start = html.IndexOf(" [[IF setVal=競技分類4]]");
                                startP = st.IndexOf(" [[IF setVal=競技分類4]]");
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=競技分類4]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region 検索ワード1
                        if (html.Contains("検索ワード1") && html.IndexOf("[[IF setVal=検索ワード1]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=検索ワード1]]");
                            startP = st.IndexOf("[[IF setVal=検索ワード1]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Search_Word5"].ToString()))
                            {
                                html = html.SafeReplace("検索ワード1", dtName.Rows[0]["Search_Word5"].ToString(), true);
                                st = st.SafeReplace("検索ワード1", dtName.Rows[0]["Search_Word5"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=検索ワード1]]").Length);
                            }
                            else
                            {
                                start = html.IndexOf(" [[IF setVal=検索ワード1]]");
                                startP = st.IndexOf(" [[IF setVal=検索ワード1]]");
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=検索ワード1]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region 検索ワード2
                        if (html.Contains("検索ワード2") && html.IndexOf("[[IF setVal=検索ワード2]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=検索ワード2]]");
                            startP = st.IndexOf("[[IF setVal=検索ワード2]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Search_Word6"].ToString()))
                            {
                                html = html.SafeReplace("検索ワード2", dtName.Rows[0]["Search_Word6"].ToString(), true);
                                st = st.SafeReplace("検索ワード2", dtName.Rows[0]["Search_Word6"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=検索ワード2]]").Length);
                            }
                            else
                            {
                                start = html.IndexOf(" [[IF setVal=検索ワード2]]");
                                startP = st.IndexOf(" [[IF setVal=検索ワード2]]");
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=検索ワード2]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region 検索ワード3
                        if (html.Contains("検索ワード3") && html.IndexOf("[[IF setVal=検索ワード3]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=検索ワード3]]");
                            startP = st.IndexOf("[[IF setVal=検索ワード3]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Search_Word7"].ToString()))
                            {
                                html = html.SafeReplace("検索ワード3", dtName.Rows[0]["Search_Word7"].ToString(), true);
                                st = st.SafeReplace("検索ワード3", dtName.Rows[0]["Search_Word7"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=検索ワード3]]").Length);
                            }
                            else
                            {
                                start = html.IndexOf(" [[IF setVal=検索ワード3]]");
                                startP = st.IndexOf(" [[IF setVal=検索ワード3]]");
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=検索ワード3]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region 検索ワード4
                        if (html.Contains("検索ワード4") && html.IndexOf("[[IF setVal=検索ワード4]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=検索ワード4]]");
                            startP = st.IndexOf("[[IF setVal=検索ワード4]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Search_Word8"].ToString()))
                            {
                                html = html.SafeReplace("検索ワード4", dtName.Rows[0]["Search_Word8"].ToString(), true);
                                st = st.SafeReplace("検索ワード4", dtName.Rows[0]["Search_Word8"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=検索ワード4]]").Length);
                            }
                            else
                            {
                                start = html.IndexOf(" [[IF setVal=検索ワード4]]");
                                startP = st.IndexOf(" [[IF setVal=検索ワード4]]");
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=検索ワード4]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region PMポイント
                        if (html.Contains("PMポイント") && html.IndexOf("[[IF setVal=PMポイント]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=PMポイント]]");
                            startP = st.IndexOf("[[IF setVal=PMポイント]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Point"].ToString()))
                            {
                                html = html.SafeReplace("PMポイント", "【ポイント" + dtName.Rows[0]["Point"].ToString() + "倍】", true);
                                st = st.SafeReplace("PMポイント", "【ポイント" + dtName.Rows[0]["Point"].ToString() + "倍】", true);
                                st = st.Remove(startP, ("[[IF setVal=PMポイント]]").Length);
                            }
                            else
                            {
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=PMポイント]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region PMクーポン
                        if (html.Contains("PMクーポン") && html.IndexOf("[[IF setVal=PMクーポン]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=PMクーポン]]");
                            startP = st.IndexOf("[[IF setVal=PMクーポン]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Coupon"].ToString()))
                            {
                                html = html.SafeReplace("PMクーポン", dtName.Rows[0]["Coupon"].ToString(), true);
                                st = st.SafeReplace("PMクーポン", dtName.Rows[0]["Coupon"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=PMクーポン]]").Length);
                            }
                            else
                            {
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=PMクーポン]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region PMプレゼント
                        if (html.Contains("PMプレゼント") && html.IndexOf("[[IF setVal=PMプレゼント]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=PMプレゼント]]");
                            startP = st.IndexOf("[[IF setVal=PMプレゼント]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Present"].ToString()))
                            {
                                html = html.SafeReplace("PMプレゼント", dtName.Rows[0]["Present"].ToString(), true);
                                st = st.SafeReplace("PMプレゼント", dtName.Rows[0]["Present"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=PMポイント]]").Length);
                            }
                            else
                            {
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=PMプレゼント]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region PMRPCキャッチコピー
                        if (html.Contains("PMRPCキャッチコピー") && html.IndexOf("[[IF setVal=PMRPCキャッチコピー]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=PMRPCキャッチコピー]]");
                            startP = st.IndexOf("[[IF setVal=PMRPCキャッチコピー]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["PC_CatchCopy_Rakuten"].ToString()))
                            {
                                html = html.SafeReplace("PMRPCキャッチコピー", dtName.Rows[0]["PC_CatchCopy_Rakuten"].ToString(), true);
                                st = st.SafeReplace("PMRPCキャッチコピー", dtName.Rows[0]["PC_CatchCopy_Rakuten"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=PMRPCキャッチコピー]]").Length);
                            }
                            else
                            {
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=PMRPCキャッチコピー]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region PMRモバイル用キャッチコピー
                        if (html.Contains("PMRモバイル用キャッチコピー") && html.IndexOf("[[IF setVal=PMRモバイル用キャッチコピー]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=PMRモバイル用キャッチコピー]]");
                            startP = st.IndexOf("[[IF setVal=PMRモバイル用キャッチコピー]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["CatchPhraseMobile_Rakuten"].ToString()))
                            {
                                html = html.SafeReplace("PMRモバイル用キャッチコピー", dtName.Rows[0]["CatchPhraseMobile_Rakuten"].ToString(), true);
                                st = st.SafeReplace("PMRモバイル用キャッチコピー", dtName.Rows[0]["CatchPhraseMobile_Rakuten"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=PMRモバイル用キャッチコピー]]").Length);
                            }
                            else
                            {
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=PMRモバイル用キャッチコピー]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region PMR商品名
                        if (html.Contains("PMR商品名") && html.IndexOf("[[IF setVal=PMR商品名]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=PMR商品名]]");
                            startP = st.IndexOf("[[IF setVal=PMR商品名]]");
                            if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["Product_Name_Rakuten"].ToString()))
                            {
                                html = html.SafeReplace("PMR商品名", dtName.Rows[0]["Product_Name_Rakuten"].ToString(), true);
                                st = st.SafeReplace("PMR商品名", dtName.Rows[0]["Product_Name_Rakuten"].ToString(), true);
                                st = st.Remove(startP, ("[[IF setVal=PMR商品名]]").Length);
                            }
                            else
                            {
                                end = html.IndexOf("[[/IF]]", start);
                                endP = st.IndexOf("[[/IF]]", startP);
                                if (end != -1)
                                {
                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                    strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                    html = html.Remove(start, line.Length);
                                    st = st.Remove(startP, strline.Length);
                                }
                                else
                                {
                                    html = html.Remove(start, ("[[IF setVal=PMR商品名]]").Length);
                                }
                            }
                        }
                        #endregion

                        #region PMスマートフォン用商品説明文
                        if (html.Contains("PMスマートフォン用商品説明文") && html.IndexOf("[[IF setVal=PMスマートフォン用商品説明文]]") != -1 && dtName != null)
                        {
                            start = html.IndexOf("[[IF setVal=PMスマートフォン用商品説明文]]");
                            startP = st.IndexOf("[[IF setVal=PMスマートフォン用商品説明文]]");
                            if (st != string.Empty)
                            {
                                if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["ItemDescription_SmartPhone_Rakuten"].ToString()))
                                {
                                    html = html.SafeReplace("PMスマートフォン用商品説明文", dtName.Rows[0]["ItemDescription_SmartPhone_Rakuten"].ToString(), true);
                                    st = st.SafeReplace("PMスマートフォン用商品説明文", dtName.Rows[0]["ItemDescription_SmartPhone_Rakuten"].ToString(), true);
                                    st = st.Remove(startP, ("[[IF setVal=PMスマートフォン用商品説明文]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    endP = st.IndexOf("[[/IF]]", startP);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        strline = st.Substring(startP, endP - startP) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                        st = st.Remove(startP, strline.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=PMスマートフォン用商品説明文]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(dtName.Rows[0]["ItemDescription_SmartPhone_Rakuten"].ToString()))
                                {
                                    html = html.SafeReplace("PMスマートフォン用商品説明文", dtName.Rows[0]["ItemDescription_SmartPhone_Rakuten"].ToString(), true);
                                    html = html.Remove(start, ("[[IF setVal=PMスマートフォン用商品説明文]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=PMスマートフォン用商品説明文]]").Length);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Item_Name Length
                        //if (dr["Item_Name"].ToString() == "[[Item Name Setting]]")
                        //{
                        //    DataTable dtPriority = GetItem_NamePriorityDataList(shopID);
                        //    st = st.Replace("[[/IF]]", "");
                        //    int len = System.Text.Encoding.UTF8.GetByteCount(st);
                        //    while (len > 255)
                        //    {
                        //        if (dtPriority.Rows.Count > 0)
                        //        {
                        //            string priorityname = dtPriority.Rows[dtPriority.Rows.Count - 1]["Priority_Name"].ToString();
                        //            if (priorityname == "[[商品名]]")
                        //            {
                        //                len = 255;
                        //            }
                        //            else
                        //            {
                        //                priorityname = priorityname.Replace("[[", "").Replace("]]", "");
                        //                if (html.Contains("[[IF setVal=" + priorityname + "]]"))
                        //                {
                        //                    start = html.IndexOf("[[IF setVal=" + priorityname + "]]");
                        //                    end = html.IndexOf("[[/IF]]", start);
                        //                    if (end != -1)
                        //                    {
                        //                        int first = html.IndexOf("[[IF setVal=" + priorityname + "]]") + ("[[IF setVal=" + priorityname + "]]").Length;
                        //                        string last = html.Substring(first, end - first);
                        //                        line = html.Substring(start, end - start) + "[[/IF]]";
                        //                        html = html.Remove(start, line.Length);
                        //                        if (st.Contains(last))
                        //                        {
                        //                            if (priorityname == "PMポイント")
                        //                            {
                        //                                string point = "【ポイント" + last + "倍】";
                        //                                st = st.Remove(st.IndexOf(point), point.Length);
                        //                            }
                        //                            else
                        //                                st = st.Remove(st.IndexOf(last), last.Length);
                        //                        }
                        //                        len = System.Text.Encoding.UTF8.GetByteCount(st);
                        //                    }
                        //                    else
                        //                    {
                        //                        html = html.Remove(start, ("[[IF setVal=" + priorityname + "]]").Length);
                        //                    }
                        //                    dtPriority.Rows[dtPriority.Rows.Count - 1].Delete();
                        //                    dtPriority.AcceptChanges();
                        //                }
                        //                else
                        //                {
                        //                    dtPriority.Rows[dtPriority.Rows.Count - 1].Delete();
                        //                    dtPriority.AcceptChanges();
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            len = 255;
                        //        }
                        //    }
                        //    html = st;
                        //    st = string.Empty;
                        //}
                        #endregion
                        #region Catch_copy_PC
                        if (columnName == "Catch_copy_PC")
                        {
                            st = st.Replace("[[/IF]]", "");
                            html = st;
                            st = string.Empty;
                        }
                        #endregion
                        #endregion

                        #region Change Template_Detail
                        if (!string.IsNullOrWhiteSpace(itemcode) && !string.IsNullOrWhiteSpace(html) && (html.Contains("基本情報") || html.Contains("詳細情報")))
                        {
                            Template_Detail_BL template = new Template_Detail_BL();
                            dtvalue = new DataTable();
                            dtvalue = template.SelectByItemCode(itemcode);
                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                if (html.Contains("基本情報"))
                                {
                                    #region 基本情報
                                    for (int t = 20; t >= 1; t--)
                                    {
                                        if (html.Contains("[[IF setVal=基本情報" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=基本情報" + t + "]]");
                                            if (!string.IsNullOrWhiteSpace(dtvalue.Rows[0]["Template" + t].ToString()))
                                            {
                                                html = html.SafeReplace("基本情報" + t, dtvalue.Rows[0]["Template" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true)
                                                                  .SafeReplace("基本情報内容" + t, dtvalue.Rows[0]["Template_Content" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true);
                                                html = html.Remove(start, ("[[IF setVal=基本情報" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=基本情報" + t + "]]").Length);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("基本情報" + t, dtvalue.Rows[0]["Template" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true)
                                                              .SafeReplace("基本情報内容" + t, dtvalue.Rows[0]["Template_Content" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true);
                                        }
                                    }
                                    #endregion
                                }
                                else if (html.Contains("詳細情報"))
                                {
                                    #region 詳細情報
                                    for (int t = 4; t >= 1; t--)
                                    {
                                        if (html.Contains("[[IF setVal=詳細情報" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=詳細情報" + t + "]]");
                                            if (!string.IsNullOrWhiteSpace(dtvalue.Rows[0]["Detail_Template" + t].ToString()))
                                            {
                                                html = html.SafeReplace("詳細情報" + t, dtvalue.Rows[0]["Detail_Template" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true)
                                                                  .SafeReplace("詳細情報内容" + t, dtvalue.Rows[0]["Detail_Template_Content" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true);
                                                html = html.Remove(start, ("[[IF setVal=詳細情報" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=詳細情報" + t + "]]").Length);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("詳細情報" + t, dtvalue.Rows[0]["Detail_Template" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true)
                                                              .SafeReplace("詳細情報内容" + t, dtvalue.Rows[0]["Detail_Template_Content" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true);
                                        }
                                    }
                                    #endregion
                                }
                            }
                            else //dtTemplateDetail == null
                            {
                                if (html.Contains("基本情報"))
                                {
                                    #region 基本情報
                                    for (int k = 20; k >= 1; k--)
                                    {
                                        if (html.Contains("[[IF setVal=基本情報" + k + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=基本情報" + k + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=基本情報" + k + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("基本情報" + k, "", true).SafeReplace("基本情報内容" + k, "", true);
                                        }
                                    }
                                    #endregion
                                }
                                else if (html.Contains("詳細情報"))
                                {
                                    #region 詳細情報
                                    for (int k = 4; k >= 1; k--)
                                    {
                                        if (html.Contains("[[IF setVal=詳細情報" + k + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=詳細情報" + k + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=詳細情報" + k + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("詳細情報" + k, "", true).SafeReplace("詳細情報内容" + k, "", true);
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion

                        #region product.product_id
                        if (html.Contains("product.product_id"))
                        {
                            html = html.SafeReplace("product.product_id", itemcode, true);
                        }
                        #endregion
                        #region 商品名
                        if (html.Contains("商品名"))
                        {
                            if (columns.Contains("Item_Name"))
                            {
                                replaceword = dr["Item_Name"].ToString();
                            }
                            if (html.Contains("[[IF setVal=商品名]]"))
                            {
                                start = html.IndexOf("[[IF setVal=商品名]]");
                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("商品名", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=商品名]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=商品名]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("商品名", replaceword, true);
                            }
                        }
                        #endregion

                        #region 商品番号
                        if (html.Contains("商品番号"))
                        {
                            replaceword = itemcode;  // select from DataBase
                            if (html.Contains("[[IF setVal=商品番号]]"))
                            {
                                start = html.IndexOf("[[IF setVal=商品番号]]");
                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("商品番号", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=商品番号]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=商品番号]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("商品番号", replaceword, true);
                            }
                        }
                        #endregion

                        #region ブランド名
                        if (html.Contains("ブランド名"))
                        {
                            replaceword = master.GetBrandName(itemcode);  // select from DataBase
                            if (html.Contains("[[IF setVal=ブランド名]]"))
                            {
                                start = html.IndexOf("[[IF setVal=ブランド名]]");
                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("ブランド名", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=ブランド名]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=ブランド名]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("ブランド名", replaceword, true);
                            }
                        }
                        #endregion

                        #region 定価
                        if (html.Contains("定価"))
                        {
                            replaceword = master.GetListPrice(itemcode);  // select from DataBase
                            if (html.Contains("[[IF setVal=定価]]"))
                            {
                                start = html.IndexOf("[[IF setVal=定価]]");
                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("定価", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=定価]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=定価]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("定価", replaceword, true);
                            }
                        }
                        #endregion

                        #region 販売価格
                        if (html.Contains("販売価格"))
                        {
                            replaceword = master.GetSalePrice(itemcode);  // select from DataBase
                            if (html.Contains("[[IF setVal=販売価格]]"))
                            {
                                start = html.IndexOf("[[IF setVal=販売価格]]");
                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("販売価格", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=販売価格]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=販売価格]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("販売価格", replaceword, true);
                            }
                        }
                        #endregion

                        #region サイズ正式名称
                        if (html.Contains("サイズ正式名称"))
                        {
                            replaceword = item.GetSKUSizeName(itemcode); // select from DataBase
                            if (html.Contains("[[IF setVal=サイズ正式名称]]"))
                            {
                                start = html.IndexOf("[[IF setVal=サイズ正式名称]]");
                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("サイズ正式名称", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=サイズ正式名称]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=サイズ正式名称]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("サイズ正式名称", replaceword, true);
                            }
                        }
                        #endregion

                        #region カラー正式名称
                        if (html.Contains("カラー正式名称"))
                        {
                            replaceword = item.GetSKUColorName(itemcode); // select from DataBase
                            if (html.Contains("[[IF setVal=カラー正式名称]]"))
                            {
                                start = html.IndexOf("[[IF setVal=カラー正式名称]]");
                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("カラー正式名称", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=カラー正式名称]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=カラー正式名称]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("カラー正式名称", replaceword, true);
                            }
                        }
                        #endregion

                        #region ゼット用項目（PC商品説明文）
                        //if (html.Contains("ゼット用項目（PC商品説明文）"))
                        //{
                        //    replaceword = master.GetZettItemDescription(itemcode);  // select from DataBase
                        //    if (html.Contains("[[IF setVal=ゼット用項目（PC商品説明文）]]"))
                        //    {
                        //        start = html.IndexOf("[[IF setVal=ゼット用項目（PC商品説明文）]]");
                        //        if (!string.IsNullOrWhiteSpace(replaceword))
                        //        {
                        //            html = html.SafeReplace("ゼット用項目（PC商品説明文）", replaceword, true);
                        //            html = html.Remove(start, ("[[IF setVal=ゼット用項目（PC商品説明文）]]").Length);
                        //        }
                        //        else
                        //        {
                        //            end = html.IndexOf("[[/IF]]", start);
                        //            if (end != -1) // If contain [[/IF]]
                        //            {
                        //                line = html.Substring(start, end - start) + "[[/IF]]";
                        //                html = html.Remove(start, line.Length);
                        //            }
                        //            else
                        //            {
                        //                html = html.Remove(start, ("[[IF setVal=ゼット用項目（PC商品説明文）]]").Length);
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        html = html.SafeReplace("ゼット用項目（PC商品説明文）", replaceword, true);
                        //    }
                        //}
                        if (html.Contains("[[PC商品説明文]]"))
                        {
                            replaceword = master.GetZettItemDescription(itemcode);  // select from DataBase

                            html = html.SafeReplace("PC商品説明文", replaceword, true);
                        }
                        #endregion

                        #region ゼット用項目（PC販売説明文）
                        //if (html.Contains("ゼット用項目（PC販売説明文）"))
                        //{
                        //    replaceword = master.GetZettSaleDescription(itemcode);  // select from DataBase
                        //    if (html.Contains("[[IF setVal=ゼット用項目（PC販売説明文）]]"))
                        //    {
                        //        start = html.IndexOf("[[IF setVal=ゼット用項目（PC販売説明文）]]");
                        //        if (!string.IsNullOrWhiteSpace(replaceword))
                        //        {
                        //            html = html.SafeReplace("ゼット用項目（PC販売説明文）", replaceword, true);
                        //            html = html.Remove(start, ("[[IF setVal=ゼット用項目（PC販売説明文）]]").Length);
                        //        }
                        //        else
                        //        {
                        //            end = html.IndexOf("[[/IF]]", start);
                        //            if (end != -1) // If contain [[/IF]]
                        //            {
                        //                line = html.Substring(start, end - start) + "[[/IF]]";
                        //                html = html.Remove(start, line.Length);
                        //            }
                        //            else
                        //            {
                        //                html = html.Remove(start, ("[[IF setVal=ゼット用項目（PC販売説明文）]]").Length);
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        html = html.SafeReplace("ゼット用項目（PC販売説明文）", replaceword, true);
                        //    }
                        //}
                        if (html.Contains("[[PC販売説明文]]"))
                        {
                            replaceword = master.GetZettSaleDescription(itemcode);  // select from DataBase

                            html = html.SafeReplace("PC販売説明文", replaceword, true);
                        }
                        #endregion

                        #region 関連商品
                        if (html.Contains("関連商品"))
                        {
                            Item_Related_Item_BL relateditemBL = new Item_Related_Item_BL();
                            dtvalue = new DataTable();
                            dtvalue = relateditemBL.SelectRelatedCode(itemcode);
                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                for (int t = 1; t <= 5; t++)
                                {
                                    DataRow[] drvalue = dtvalue.Select("SN=" + t + "");
                                    if (drvalue.Count() > 0)
                                    {
                                        DataTable dtItem = dtvalue.Select("SN=" + t + "").CopyToDataTable();
                                        replaceword = dtItem.Rows[0]["Related_ItemCode"].ToString();
                                        //comment = dtItem.Rows[0]["Related_ItemComment"].ToString();
                                        if (html.Contains("[[IF setVal=関連商品" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=関連商品" + t + "]]");
                                            //if (!string.IsNullOrWhiteSpace(comment))
                                            //{
                                            //    html = html.SafeReplace("Related_ItemComment" + t, comment, true);
                                            //}
                                            if (!string.IsNullOrWhiteSpace(replaceword))
                                            {
                                                html = html.SafeReplace("関連商品_商品番号" + t, dtItem.Rows[0]["Related_ItemCode"].ToString() + ".html", true).SafeReplace("関連商品_商品名" + t, dtItem.Rows[0]["Item_Name"].ToString(), true).SafeReplace("関連商品_商品画像" + t, dtItem.Rows[0]["Image_Name"].ToString(), true).SafeReplace("関連商品_販売価格" + t, dtItem.Rows[0]["Sale_Price"].ToString(), true);
                                                html = html.Remove(start, ("[[IF setVal=関連商品" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                #region Delete Line
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=関連商品" + t + "]]").Length);
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("関連商品_商品番号" + t, dtItem.Rows[0]["Related_ItemCode"].ToString() + ".html", true).SafeReplace("関連商品_商品名" + t, dtItem.Rows[0]["Item_Name"].ToString(), true).SafeReplace("関連商品_商品画像" + t, dtItem.Rows[0]["Image_Name"].ToString(), true).SafeReplace("関連商品_販売価格" + t, dtItem.Rows[0]["Sale_Price"].ToString(), true);//.SafeReplace("Related_ItemComment" + t, dtItem.Rows[0]["Related_ItemComment"].ToString(), true);
                                        }
                                    }
                                    else
                                    {
                                        #region Delete Line
                                        if (html.Contains("[[IF setVal=関連商品" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=関連商品" + t + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=関連商品" + t + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("関連商品_商品番号" + t, "", true).SafeReplace("関連商品_商品名" + t, "", true).SafeReplace("関連商品_商品画像" + t, "", true).SafeReplace("関連商品_販売価格" + t, "", true);//.SafeReplace("Related_ItemComment" + t, "", true);
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region Delete All Line
                                for (int t = 1; t <= 5; t++)
                                {
                                    if (html.Contains("[[IF setVal=関連商品" + t + "]]"))
                                    {
                                        start = html.IndexOf("[[IF setVal=関連商品" + t + "]]");
                                        end = html.IndexOf("[[/IF]]", start);
                                        if (end != -1)
                                        {
                                            line = html.Substring(start, end - start) + "[[/IF]]";
                                            html = html.Remove(start, line.Length);
                                        }
                                        else
                                        {
                                            html = html.Remove(start, ("[[IF setVal=関連商品" + t + "]]").Length);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("関連商品_商品番号" + t, "", true).SafeReplace("関連商品_商品名" + t, "", true).SafeReplace("関連商品_商品画像" + t, "", true).SafeReplace("関連商品_販売価格" + t, "", true);//.SafeReplace("Related_ItemComment" + t, "", true);
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region 商品ページURL
                        if (html.Contains("商品ページURL"))
                        {
                            Shop_BL shop = new Shop_BL();
                            replaceword = shop.SelectProductPageURL(shopID);  // select from DataBase
                            if (html.Contains("[[IF setVal=商品ページURL]]"))
                            {
                                start = html.IndexOf("[[IF setVal=商品ページURL]]");
                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("商品ページURL", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=商品ページURL]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=商品ページURL]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("商品ページURL", replaceword, true);
                            }
                        }
                        #endregion

                        #region 商品画像
                        if (html.Contains("商品画像"))
                        {
                            dtvalue = new DataTable();
                            dtvalue = imageBL.SelectImageName(itemcode, 0);
                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                //for (int t = 1; t <= 6; t++)
                                for (int t = 1; t <= 20; t++)
                                {
                                    DataRow[] drvalue = dtvalue.Select("SN=" + t + "");
                                    if (drvalue.Count() > 0)
                                    {
                                        DataTable dtItem = dtvalue.Select("SN=" + t + "").CopyToDataTable();
                                        replaceword = dtItem.Rows[0]["Image_Name"].ToString();
                                        if (html.Contains("[[IF setVal=商品画像" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=商品画像" + t + "]]");

                                            if (!string.IsNullOrWhiteSpace(replaceword))
                                            {
                                                html = html.SafeReplace("商品画像" + t, replaceword, true);
                                                html = html.Remove(start, ("[[IF setVal=商品画像" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                #region Delete Line
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=商品画像" + t + "]]").Length);
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("商品画像" + t, replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        #region Delete Line
                                        if (html.Contains("[[IF setVal=商品画像" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=商品画像" + t + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=商品画像" + t + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("商品画像" + t, "", true);
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region Delete All Line
                                //for (int t = 1; t <= 6; t++)
                                for (int t = 1; t <= 20; t++)
                                {
                                    if (html.Contains("[[IF setVal=商品画像" + t + "]]"))
                                    {
                                        start = html.IndexOf("[[IF setVal=商品画像" + t + "]]");
                                        end = html.IndexOf("[[/IF]]", start);
                                        if (end != -1)
                                        {
                                            line = html.Substring(start, end - start) + "[[/IF]]";
                                            html = html.Remove(start, line.Length);
                                        }
                                        else
                                        {
                                            html = html.Remove(start, ("[[IF setVal=商品画像" + t + "]]").Length);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("商品画像" + t, "", true);
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region テクノロジー画像
                        if (html.Contains("テクノロジー画像"))
                        {
                            dtvalue = new DataTable();
                            dtvalue = imageBL.SelectImageName(itemcode, 1);
                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                for (int t = 1; t <= 6; t++)
                                {
                                    DataRow[] drvalue = dtvalue.Select("SN=" + t + "");
                                    if (drvalue.Count() > 0)
                                    {
                                        DataTable dtItem = dtvalue.Select("SN=" + t + "").CopyToDataTable();
                                        replaceword = dtItem.Rows[0]["Image_Name"].ToString();
                                        if (html.Contains("[[IF setVal=テクノロジー画像" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=テクノロジー画像" + t + "]]");
                                            if (!string.IsNullOrWhiteSpace(replaceword))
                                            {
                                                html = html.SafeReplace("テクノロジー画像" + t, replaceword, true);
                                                html = html.Remove(start, ("[[IF setVal=テクノロジー画像" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                #region Delete Line
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=テクノロジー画像" + t + "]]").Length);
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("テクノロジー画像" + t, replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        #region Delete Line
                                        if (html.Contains("[[IF setVal=テクノロジー画像" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=テクノロジー画像" + t + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=テクノロジー画像" + t + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("テクノロジー画像" + t, "", true);
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region Delete All Line
                                for (int t = 1; t <= 6; t++)
                                {
                                    if (html.Contains("[[IF setVal=テクノロジー画像" + t + "]]"))
                                    {
                                        start = html.IndexOf("[[IF setVal=テクノロジー画像" + t + "]]");
                                        end = html.IndexOf("[[/IF]]", start);
                                        if (end != -1)
                                        {
                                            line = html.Substring(start, end - start) + "[[/IF]]";
                                            html = html.Remove(start, line.Length);
                                        }
                                        else
                                        {
                                            html = html.Remove(start, ("[[IF setVal=テクノロジー画像" + t + "]]").Length);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("テクノロジー画像" + t, "", true);
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion


                        #region 動画

                        if (html.Contains("動画"))
                        {
                            dtvalue = new DataTable();
                            dtvalue = SelectVideoName(itemcode, MallID);
                            int vidsize = 0;
                            if (dtvalue.Rows.Count > 0)
                            {
                                if (!String.IsNullOrWhiteSpace(dtvalue.Rows[0]["Video_Size"].ToString()))
                                {
                                    vidsize = Convert.ToInt32(dtvalue.Rows[0]["Video_Size"]);
                                    if (vidsize > 1)
                                    {
                                        vidsize = (350 * vidsize) - 50;
                                    }
                                    else
                                    {
                                        vidsize = 350;
                                    }
                                }
                            }
                            if (html.Contains("[[VideoSize]]"))
                            {
                                html = html.SafeReplace("VideoSize", vidsize.ToString(), true);
                            }
                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                for (int t = 1; t <= 5; t++)
                                {
                                    DataRow[] drvalue = dtvalue.Select("SN=" + t + "");
                                    if (drvalue.Count() > 0)
                                    {
                                        DataTable dtItem = dtvalue.Select("SN=" + t + "").CopyToDataTable();
                                        replaceword = dtItem.Rows[0]["Video_Name"].ToString();
                                        if (html.Contains("[[IF setVal=動画" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=動画" + t + "]]");

                                            if (!string.IsNullOrWhiteSpace(replaceword))
                                            {
                                                html = html.SafeReplace("動画" + t, replaceword, true);
                                                html = html.Remove(start, ("[[IF setVal=動画" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                #region Delete Line
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=動画" + t + "]]").Length);
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("動画" + t, replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        #region Delete Line
                                        if (html.Contains("[[IF setVal=動画" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=動画" + t + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=動画" + t + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("動画" + t, "", true);
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region Delete All Line
                                for (int t = 1; t <= 5; t++)
                                {
                                    if (html.Contains("[[IF setVal=動画" + t + "]]"))
                                    {
                                        start = html.IndexOf("[[IF setVal=動画" + t + "]]");
                                        end = html.IndexOf("[[/IF]]", start);
                                        if (end != -1)
                                        {
                                            line = html.Substring(start, end - start) + "[[/IF]]";
                                            html = html.Remove(start, line.Length);
                                        }
                                        else
                                        {
                                            html = html.Remove(start, ("[[IF setVal=動画" + t + "]]").Length);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("動画" + t, "", true);
                                    }
                                }
                                #endregion
                            }
                        }

                        #endregion


                        #region キャンペーン画像
                        if (html.Contains("キャンペーン画像"))
                        {
                            dtvalue = new DataTable();
                            dtvalue = imageBL.SelectImageName(itemcode, 2);
                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                for (int t = 1; t <= 5; t++)
                                {
                                    DataRow[] drvalue = dtvalue.Select("SN=" + t + "");
                                    if (drvalue.Count() > 0)
                                    {
                                        DataTable dtItem = dtvalue.Select("SN=" + t + "").CopyToDataTable();
                                        replaceword = dtItem.Rows[0]["Image_Name"].ToString();
                                        if (html.Contains("[[IF setVal=キャンペーン画像" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=キャンペーン画像" + t + "]]");

                                            if (!string.IsNullOrWhiteSpace(replaceword))
                                            {
                                                html = html.SafeReplace("キャンペーン画像" + t, replaceword, true);
                                                html = html.Remove(start, ("[[IF setVal=キャンペーン画像" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                #region Delete Line
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=キャンペーン画像" + t + "]]").Length);
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("キャンペーン画像" + t, replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        #region Delete Line
                                        if (html.Contains("[[IF setVal=キャンペーン画像" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=キャンペーン画像" + t + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=キャンペーン画像" + t + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("キャンペーン画像" + t, "", true);
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region Delete All Line
                                for (int t = 1; t <= 5; t++)
                                {
                                    if (html.Contains("[[IF setVal=キャンペーン画像" + t + "]]"))
                                    {
                                        start = html.IndexOf("[[IF setVal=キャンペーン画像" + t + "]]");
                                        end = html.IndexOf("[[/IF]]", start);
                                        if (end != -1)
                                        {
                                            line = html.Substring(start, end - start) + "[[/IF]]";
                                            html = html.Remove(start, line.Length);
                                        }
                                        else
                                        {
                                            html = html.Remove(start, ("[[IF setVal=キャンペーン画像" + t + "]]").Length);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("キャンペーン画像" + t, "", true);
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region Saleprice
                        if (html.Contains("[[IF saleprice"))
                        {
                            start = html.IndexOf("[[IF saleprice");
                            int index1 = html.IndexOf("]][[/IF]]");
                            string match2 = html.Substring(start, html.IndexOf("]][[/IF]]") - start);

                            match2 = match2.Replace("[[IF ", "");
                            int sale_price = Convert.ToInt32(dr["Sale_Price"].ToString());
                            if (match2.Contains(">="))
                            {
                                string[] str = match2.Split(new string[] { ">=" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (sale_price >= tmp_Price)
                                {
                                    html = html.Replace(str1, master.GetTemplateValue(match2, 1));
                                    //html = html.Remove(start, str1.Length);

                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            else if (match2.Contains("<="))
                            {
                                string[] str = match2.Split(new string[] { "<=" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (sale_price <= tmp_Price)
                                {
                                    html = html.Replace(str1, master.GetTemplateValue(match2, 1));
                                    //html = html.Remove(start, str.Length);

                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            else if (match2.Contains(">"))
                            {
                                string[] str = match2.Split(new string[] { ">" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (sale_price > tmp_Price)
                                {
                                    html = html.Replace(str1, master.GetTemplateValue(match2, 1));
                                    //html = html.Remove(start, str1.Length);
                                    // html = html.Replace(str1, "");

                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                string[] str = match2.Split(new string[] { "<" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (sale_price < tmp_Price)
                                {
                                    html = html.Replace(str1, master.GetTemplateValue(match2, 1));
                                    //html = html.Remove(start, str.Length);

                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            //start = html.IndexOf("[[IF setVal=price]]");
                            //string str = "[[IF setVal=price]]";
                            //int sale_price = Convert.ToInt32(dr["Sale_Price"].ToString());
                            //if (str.Contains("price"))
                            //{
                            // string tmpPrice = master.GetTemplateValue("price", 1);
                            //    //int tmpPrice = 8000;
                            //    if (sale_price > Convert.ToInt32(tmpPrice))
                            //    {
                            //        html = html.SafeReplace("price", "img_1", true);
                            //        html = html.Remove(start, str.Length);
                            //    }
                            //    else
                            //    {
                            //        end = html.IndexOf("[[/IF]]", start);
                            //        if (end != -1)
                            //        {
                            //            line = html.Substring(start, end - start) + "[[/IF]]";
                            //            html = html.Remove(start, line.Length);
                            //        }
                            //        else
                            //        {
                            //            html = html.Remove(start, ("[[IF setVal=price]]").Length);
                            //        }
                            //    }
                            //}
                        }
                        #endregion

                        #region ListPrice
                        if (html.Contains("[[IF listprice"))
                        {
                            start = html.IndexOf("[[IF listprice");
                            int index1 = html.IndexOf("]][[/IF]]");
                            string match2 = html.Substring(start, html.IndexOf("]][[/IF]]") - start);
                            match2 = match2.Replace("[[IF ", "");
                            int list_price = Convert.ToInt32(dr["R_List_Price"].ToString());
                            if (match2.Contains(">="))
                            {
                                string[] str = match2.Split(new string[] { ">=" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (list_price >= tmp_Price)
                                {
                                    html = html.SafeReplace(str1, master.GetTemplateValue(match2, 1), true);
                                    // html = html.Remove(start, str.Length);

                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            else if (match2.Contains("<="))
                            {
                                string[] str = match2.Split(new string[] { "<=" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (list_price <= tmp_Price)
                                {
                                    html = html.SafeReplace(str1, master.GetTemplateValue(match2, 1), true);
                                    //html = html.Remove(start, str.Length);

                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            else if (match2.Contains(">"))
                            {
                                string[] str = match2.Split(new string[] { ">" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (list_price > tmp_Price)
                                {
                                    html = html.Replace(str1, master.GetTemplateValue(match2, 1));
                                    //html = html.Remove(start, str1.Length);
                                    //html = html.Replace(str1, "");
                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                string[] str = match2.Split(new string[] { "<" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (list_price < tmp_Price)
                                {
                                    html = html.SafeReplace(str1, master.GetTemplateValue(match2, 1), true);
                                    // html = html.Remove(start, str.Length);

                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }

                        }
                        #endregion
                    }
                    html = html.Replace("[[/IF]]", "");
                    dr[columnName] = html;
                }
            }
            return dt;
        }
        public DataTable SelectVideoName(string Item_Code, int Mall_ID)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_SelectVideoName", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Mall_ID", Mall_ID);
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
        public DataTable GetTemplateDescription(string[] templateID, int shopID)
        {
            shopTemplate = new ORS_RCM_BL.Shop_Template_BL();
            DataTable dt = shopTemplate.GetTemplateDescription(templateID, shopID);
            if (dt.Rows.Count > 0 && dt != null)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        public string[] GetTemplateID(string html)
        {
            //@"\[([^]]*)\]"
            ICollection<string> matches =
                Regex.Matches(html.Replace(Environment.NewLine, ""), @"\[\[([^]]*)\]\]")
                .Cast<Match>()
                .Select(x => x.Groups[1].Value)
                .ToList();
            string temp = "";
            if (matches.Count == 0)
            {
                html = html.Replace("[", "").Replace("]", "");
            }
            foreach (string match in matches)
            {
                temp += match + ',';
            }
            temp = temp.Replace("[", "");
            string[] templateID = temp.TrimEnd(',').TrimStart('[').Split(',');
            return templateID;
        }

        private static DataTable MergeTable(DataTable dt)
        {
            DataTable dtTmp = new DataTable();
            if (dt.Rows.Count > 0)
            {
                dtTmp.Merge(dt);
            }
            return dtTmp;
        }

        public DataTable GetRelatedName(string Item_Code, int shop_id)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_GetFormatItemName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@itemcode", Item_Code);
                cmd.Parameters.AddWithValue("@shop_id", shop_id);
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

        #region Item_NamePrioritySetting
        public DataTable GetItem_NamePriorityDataList(int shopID)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("select Priority_Name,Status from Item_Name_PrioritySetting where Shop_ID=" + shopID, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
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
        #endregion

    }


    public static class StringExtensions
    {
        public static string SafeReplace(this string input, string find, string replace, bool matchWholeWord)
        {
            string textToFind = matchWholeWord ? string.Format(@"\[\[({0})\]\]", find) : " ";
            if (Regex.IsMatch(input, textToFind))
            {
                return Regex.Replace(input, textToFind, replace.Trim());
            }
            else
            {
                return input;
            }
        }
    }
}
