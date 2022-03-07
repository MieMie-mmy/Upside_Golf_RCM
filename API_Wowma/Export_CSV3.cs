using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ORS_RCM_BL;

namespace API_Wowma
{
    public class Export_CSV3
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
        string last = string.Empty;
        Item_Master_BL master = new Item_Master_BL();
        string Pname = string.Empty;

        public DataTable ModifyTable(DataTable dtItem, int shopID)
        {
            DataTable dtFinal = new DataTable();

            if (dtItem.Rows.Count > 0)
            {
                //if (dtItem.Columns.Contains("Item_Name"))
                //{
                //    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Item_Name"));
                //}

                if (dtItem.Columns.Contains("Item_Description_PC")) // Item_Description_PC (wowma)
                {
                    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Item_Description_PC"));
                }

                if (dtItem.Columns.Contains("Sale_Description_PC")) // Sale_Description_PC (wowma)
                {
                    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Sale_Description_PC"));
                }

                if (dtItem.Columns.Contains("Smart_Template")) // Smart_Template (wowma)
                {
                    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Smart_Template"));
                }

                if (dtItem.Columns.Contains("Explanation")) // Merchandise_Information (yahoo)
                {
                    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Explanation"));
                }

                if (dtItem.Columns.Contains("Catch_copy_PC"))
                {
                    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Catch_copy_PC"));
                }

                if (dtItem.Columns.Contains("Catch_copy_Mobile"))
                {
                    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Catch_copy_Mobile"));
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
            replaceword = "";
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
                    if (html.Contains("[[Sale_Price]]"))
                    {

                    }
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

                        dtvalue = new DataTable();

                        //to check whether zett or not : 25/10/2017


                        #region ShopTemplate


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
                                if ((columnName == "Catch_copy_Mobile") || (columnName == "Catch_copy_PC"))
                                {
                                }
                                else
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
                            //else
                            //{
                            //    html = html.Replace("[", "").Replace("]", "");
                            //}
                        }
                        #endregion

                        #region Item_Name
                        if ((columnName == "Item_Name") || (columnName == "Catch_copy_PC") || (columnName == "Catch_copy_Mobile"))
                        {
                            dtName = GetRelatedName(itemcode, shopID);
                        }
                        if ((columnName == "Item_Name") || (columnName == "Catch_copy_PC") || (columnName == "Catch_copy_Mobile"))
                            st = html;
                        #region ブランド
                        if (html.Contains("ブランド") && html.IndexOf("[[IF setVal=ブランド]]") != -1 && dtName != null && dtName.Rows.Count > 0)
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
                        if (html.Contains("競技") && html.IndexOf("[[IF setVal=競技]]") != -1 && dtName != null && dtName.Rows.Count > 0)
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

                        #region 分類
                        if (html.Contains("分類") && html.IndexOf("[[IF setVal=分類]]") != -1 && dtName != null && dtName.Rows.Count > 0)
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

                        #region 商品名
                        if (html.Contains("商品名") && html.IndexOf("[[IF setVal=商品名]]") != -1 && dtName != null && dtName.Rows.Count > 0)
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
                        if (html.Contains("競技分類1") && html.IndexOf("[[IF setVal=競技分類1]]") != -1 && dtName != null && dtName.Rows.Count > 0)
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
                        if (html.Contains("競技分類2") && html.IndexOf("[[IF setVal=競技分類2]]") != -1 && dtName != null && dtName.Rows.Count > 0)
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
                        if (html.Contains("競技分類3") && html.IndexOf("[[IF setVal=競技分類3]]") != -1 && dtName != null && dtName.Rows.Count > 0)
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
                        if (html.Contains("競技分類4") && html.IndexOf("[[IF setVal=競技分類4]]") != -1 && dtName != null && dtName.Rows.Count > 0)
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
                        if (html.Contains("検索ワード1") && html.IndexOf("[[IF setVal=検索ワード1]]") != -1 && dtName != null && dtName.Rows.Count > 0)
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
                        if (html.Contains("検索ワード2") && html.IndexOf("[[IF setVal=検索ワード2]]") != -1 && dtName != null && dtName.Rows.Count > 0)
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
                        if (html.Contains("検索ワード3") && html.IndexOf("[[IF setVal=検索ワード3]]") != -1 && dtName != null && dtName.Rows.Count > 0)
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
                        if (html.Contains("検索ワード4") && html.IndexOf("[[IF setVal=検索ワード4]]") != -1 && dtName != null && dtName.Rows.Count > 0)
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
                                startP = st.IndexOf("[[IF setVal=検索ワード4]]");
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
                        if (dr["Item_Name"].ToString() == "[[Item Name Setting]]")
                        {
                            DataTable dtPriority = GetItem_NamePriorityDataList(shopID);
                            st = st.Replace("[[/IF]]", "");
                            DataRow[] dtrow = dtPriority.Select("Priority_Name = '[[ブランド]]' OR Priority_Name = '[[分類]]' OR Priority_Name = '[[競技]]'");
                            if (dtrow.Length > 0)
                            {
                                var Mins = dtrow.AsEnumerable().Min(row => Convert.ToInt32(row["Status"]));
                                for (int i = 0; i < dtrow.Length; i++)
                                {
                                    if (dtrow[i]["Status"].ToString() == Mins.ToString())
                                    {
                                        Pname = dtrow[i]["Priority_Name"].ToString().Replace("[[", "").Replace("]]", "");
                                    }
                                }
                            }
                            //  int lengthtest =  ASCIIEncoding.Unicode.GetByteCount(st);

                            // byteLength = ime.Item_Name.Length;
                            // int len = System.Text.Encoding.UTF8.GetByteCount(st);
                            //int len = System.Text.ASCIIEncoding.Unicode.GetByteCount(st);
                            //  len = System.Text.ASCIIEncoding.ASCII.GetByteCount(st);
                            //    int len = st.Length;
                            Encoding stUnicode = Encoding.GetEncoding(932);
                            byte[] stunicodeBytes = stUnicode.GetBytes(st);
                            int len = stunicodeBytes.Length;
                            while (len > 256)
                            {
                                if (dtPriority.Rows.Count > 0)
                                {
                                    string priorityname = dtPriority.Rows[dtPriority.Rows.Count - 1]["Priority_Name"].ToString();
                                    priorityname = priorityname.Replace("[[", "").Replace("]]", "");
                                    if (html.Contains("[[IF setVal=" + priorityname + "]]"))
                                    {

                                        start = html.IndexOf("[[IF setVal=" + priorityname + "]]");
                                        end = html.IndexOf("[[/IF]]", start);
                                        if (end != -1)
                                        {
                                            if (priorityname == Pname)
                                            {
                                                int first = html.IndexOf("[[IF setVal=" + priorityname + "]]") + ("[[IF setVal=" + priorityname + "]]").Length;
                                                last = html.Substring(first, end - first);
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                                if (html.Contains("[  ]"))
                                                {
                                                    html = html.Replace("[  ]", "");
                                                }
                                                else if (html.Contains("[ ]"))
                                                {
                                                    html = html.Replace("[ ]", "");
                                                }
                                                else if (html.Contains("[]"))
                                                {
                                                    html = html.Replace("[]", "");
                                                }
                                            }
                                            else
                                            {
                                                int first = html.IndexOf("[[IF setVal=" + priorityname + "]]") + ("[[IF setVal=" + priorityname + "]]").Length;
                                                last = html.Substring(first, end - first);
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            if (st.Contains(last))
                                            {
                                                if (priorityname == "PMポイント")
                                                {
                                                    string point = "【ポイント" + last + "倍】";
                                                    st = st.Remove(st.IndexOf(point), point.Length);
                                                }
                                                else if (priorityname == Pname)
                                                {
                                                    st = st.Remove(st.IndexOf(last), last.Length);
                                                    if (st.Contains("[  ]"))
                                                    {
                                                        st = st.Replace("[  ]", "");
                                                    }
                                                    else if (st.Contains("[ ]"))
                                                    {
                                                        st = st.Replace("[ ]", "");
                                                    }
                                                    else if (st.Contains("[]"))
                                                    {
                                                        st = st.Replace("[]", "");
                                                    }
                                                }
                                                else
                                                    st = st.Remove(st.IndexOf(last), last.Length);

                                            }
                                            // len = st.Length;
                                            // len = System.Text.Encoding.UTF8.GetByteCount(st); 
                                            //Encoding nav = Encoding.GetEncoding(1252);
                                            Encoding unicode = Encoding.GetEncoding(932);
                                            byte[] unicodeBytes = unicode.GetBytes(st);
                                            len = unicodeBytes.Length;
                                        }
                                        else
                                        {
                                            html = html.Remove(start, ("[[IF setVal=" + priorityname + "]]").Length);
                                        }
                                        dtPriority.Rows[dtPriority.Rows.Count - 1].Delete();
                                        dtPriority.AcceptChanges();
                                    }
                                    else
                                    {
                                        dtPriority.Rows[dtPriority.Rows.Count - 1].Delete();
                                        dtPriority.AcceptChanges();
                                    }
                                }
                                else
                                {
                                    len = 256;
                                }
                            }
                            //st = st.Replace("[]","");

                            if (string.IsNullOrWhiteSpace(st))
                            {
                                html = dtName.Rows[0]["Item_Name"].ToString();
                            }
                            else
                            {
                                html = st;
                            }
                            st = string.Empty;
                        }
                        #endregion
                        #region Catch_copy_Mobile/PC
                        if ((columnName == "Catch_copy_Mobile") || (columnName == "Catch_copy_PC"))
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

                            dtvalue = new DataTable();
                            dtvalue = SelectByItemCode(itemcode);

                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                if (html.Contains("基本情報"))
                                {
                                    #region 基本情報
                                    for (int t = 20; t >= 1; t--)
                                    {
                                        //html = html.SafeReplace("基本情報" + j, dtTemplateDetail.Rows[0]["Template" + j].ToString(), true).SafeReplace("基本情報内容" + j, dtTemplateDetail.Rows[0]["Template_Content" + j].ToString(), true);
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
                                                //html = html.Remove(start, ("[[IF setVal=基本情報" + j + "]]").Length);
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

                        #region 販売単位
                        if (html.Contains("販売単位"))
                        {
                            replaceword = master.GetSalesUnit(itemcode, "Sales_unit");  // select from DataBase
                            if (html.Contains("[[IF setVal=販売単位]]"))
                            {
                                start = html.IndexOf("[[IF setVal=販売単位]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    replaceword = "1" + replaceword;
                                    html = html.SafeReplace("販売単位", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=販売単位]]").Length);
                                    //html = html.Remove(start, ("[[IF setVal=内容量数1]]").Length);
                                    if (html.Contains("内容量数1"))
                                    {
                                        replaceword = master.GetSalesUnit(itemcode, "Content_quantity_number_1");

                                        if (!string.IsNullOrWhiteSpace(replaceword))
                                        {
                                            string word = master.GetSalesUnit(itemcode, "Contents_unit_2");
                                            if (!string.IsNullOrWhiteSpace(word))
                                            {
                                                replaceword = "(" + "1" + word + replaceword;
                                                html = html.SafeReplace("内容量数1", replaceword, true);
                                            }
                                            else
                                            {
                                                replaceword = "/" + replaceword;
                                                html = html.SafeReplace("内容量数1", replaceword, true);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("内容量数1", replaceword, true);

                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("内容量数1", replaceword, true);
                                    }
                                    if (html.Contains("内容量単位1"))
                                    {
                                        replaceword = master.GetSalesUnit(itemcode, "Contents_unit_1");
                                        if (!string.IsNullOrWhiteSpace(replaceword))
                                        {
                                            string word = master.GetSalesUnit(itemcode, "Contents_unit_2");
                                            if (!String.IsNullOrWhiteSpace(word))
                                            {
                                                replaceword = replaceword + ")";
                                                html = html.SafeReplace("内容量単位1", replaceword, true);
                                            }
                                            else
                                            {
                                                html = html.SafeReplace("内容量単位1", replaceword, true);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("内容量単位1", replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("内容量単位1", replaceword, true);
                                    }
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
                                        html = html.Remove(start, ("[[IF setVal=販売単位]]</td></tr>").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("販売単位", replaceword, true);
                            }
                        }
                        #endregion

                        #region 内容量数2
                        if (html.Contains("内容量数2"))
                        {
                            //html = html.SafeReplace("商品番号", itemcode, true);
                            replaceword = master.GetSalesUnit(itemcode, "Content_quantity_number_2");  // select from DataBase 
                            if (html.Contains("[[IF setVal=内容量数2]]"))
                            {
                                start = html.IndexOf("[[IF setVal=内容量数2]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    replaceword = "/" + replaceword;
                                    html = html.SafeReplace("内容量数2", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=内容量数2]]").Length);
                                    if (html.Contains("内容量単位2"))
                                    {
                                        replaceword = master.GetSalesUnit(itemcode, "Contents_unit_2");
                                        if (!string.IsNullOrWhiteSpace(replaceword))
                                        {
                                            replaceword = replaceword + "入り";
                                            html = html.SafeReplace("内容量単位2", replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("内容量単位2", replaceword, true);
                                    }
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
                                        html = html.Remove(start, ("[[IF setVal=内容量数2]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("内容量数2", replaceword, true);
                            }
                        }
                        #endregion

                        #region 2
                        if (html.Contains("その他商品説明"))
                        {
                            //html = html.SafeReplace("商品番号", itemcode, true);
                            replaceword = master.GetSalesUnit(itemcode, "Content_quantity_number_2");  // select from DataBase
                            if (html.Contains("[[IF setVal=内容量数2]]"))
                            {
                                start = html.IndexOf("[[IF setVal=内容量数2]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("内容量数2", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=内容量数2]]").Length);
                                    if (html.Contains("内容量単位2"))
                                    {
                                        replaceword = master.GetSalesUnit(itemcode, "Contents_unit_2");
                                        if (!string.IsNullOrWhiteSpace(replaceword))
                                        {
                                            html = html.SafeReplace("内容量単位2", replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("内容量単位2", replaceword, true);
                                    }
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
                                        html = html.Remove(start, ("[[IF setVal=内容量数2]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("内容量数2", replaceword, true);
                            }
                        }
                        #endregion
                        //html = html.Replace("[[/IF]]", "");
                        html = html.Replace("[[IF setVal=内容量数1]]", "");
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
                                string itemName = dr["Item_Name"].ToString();
                                string[] arr = itemName.Split(new string[] { "倍】", "【ポイント" }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string itemname in arr)
                                {
                                    int i;
                                    if (!int.TryParse(itemname, out i))
                                    {
                                        replaceword = itemname;
                                    }
                                }
                                // replaceword = arr[1].ToString();
                                //replaceword = dr["Item_Name"].ToString();
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
                            //html = html.SafeReplace("商品番号", itemcode, true);
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
                            replaceword = GetBrandName(itemcode);  // select from DataBase

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
                            replaceword = GetListPrice(itemcode);  // select from DataBase

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
                            replaceword = GetSalePrice(itemcode);  // select from DataBase

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
                            replaceword = GetSKUSizeName(itemcode); // select from DataBase

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
                            replaceword = GetSKUColorName(itemcode); // select from DataBase

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
                        if (html.Contains("[[PC商品説明文]]"))
                        {
                            replaceword = GetZettItemDescription(itemcode);  // select from DataBase

                            html = html.SafeReplace("PC商品説明文", replaceword, true);
                        }
                        #endregion

                        #region ゼット用項目（PC販売説明文）

                        if (html.Contains("[[PC販売説明文]]"))
                        {
                            replaceword = GetZettSaleDescription(itemcode);  // select from DataBase

                            html = html.SafeReplace("PC販売説明文", replaceword, true);
                        }
                        #endregion

                        #region 関連商品
                        if (html.Contains("関連商品"))
                        {

                            dtvalue = new DataTable();
                            dtvalue = SelectRelatedCode(itemcode);
                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                for (int t = 1; t <= 5; t++)
                                {
                                    DataRow[] drvalue = dtvalue.Select("SN=" + t + "");
                                    if (drvalue.Count() > 0)
                                    {
                                        DataTable dtItem = dtvalue.Select("SN=" + t + "").CopyToDataTable();
                                        replaceword = "/item/" + dtvalue.Rows[0]["Wowma_lotNumber"].ToString();

                                        if (html.Contains("[[IF setVal=関連商品" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=関連商品" + t + "]]");

                                            if (!string.IsNullOrWhiteSpace(replaceword))
                                            {
                                                //edit
                                                html = html.SafeReplace("関連商品_商品番号" + t, replaceword, true).SafeReplace("関連商品_商品名" + t, dtItem.Rows[0]["Item_Name"].ToString(), true).SafeReplace("関連商品_商品画像" + t, "https://image.wowma.jp/39998948/" + dtItem.Rows[0]["Image_Name"].ToString(), true).SafeReplace("関連商品_販売価格" + t, dtItem.Rows[0]["Sale_Price"].ToString(), true);
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
                                            html = html.SafeReplace("関連商品_商品番号" + t, replaceword, true).SafeReplace("関連商品_商品名" + t, dtItem.Rows[0]["Item_Name"].ToString(), true).SafeReplace("関連商品_商品画像" + t, dtItem.Rows[0]["Image_Name"].ToString(), true).SafeReplace("関連商品_販売価格" + t, dtItem.Rows[0]["Sale_Price"].ToString(), true);
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
                                            html = html.SafeReplace("関連商品_商品番号" + t, "", true).SafeReplace("関連商品_商品名" + t, "", true).SafeReplace("関連商品_商品画像" + t, "", true).SafeReplace("関連商品_販売価格" + t, "", true);
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
                                        html = html.SafeReplace("関連商品_商品番号" + t, "", true).SafeReplace("関連商品_商品名" + t, "", true).SafeReplace("関連商品_商品画像" + t, "", true).SafeReplace("関連商品_販売価格" + t, "", true);
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region 商品ページURL
                        if (html.Contains("商品ページURL"))
                        {

                            replaceword = SelectProductPageURL(shopID);  // select from DataBase

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
                            dtvalue = SelectImageName(itemcode, 0);
                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                for (int t = 1; t <= 6; t++)
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
                                for (int t = 1; t <= 6; t++)
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
                            dtvalue = SelectImageName(itemcode, 1);
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

                        #region キャンペーン画像
                        if (html.Contains("キャンペーン画像"))
                        {
                            dtvalue = new DataTable();
                            dtvalue = SelectImageName(itemcode, 2);
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
                                    html = html.Replace(str1, GetTemplateValue(match2, 1));
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
                                    html = html.Replace(str1, GetTemplateValue(match2, 1));
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
                                    html = html.Replace(str1, GetTemplateValue(match2, 1));
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
                                    html = html.Replace(str1, GetTemplateValue(match2, 1));
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
                                    html = html.SafeReplace(str1, GetTemplateValue(match2, 1), true);
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
                                    html = html.SafeReplace(str1, GetTemplateValue(match2, 1), true);
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
                                    html = html.Replace(str1, GetTemplateValue(match2, 1));
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
                                    html = html.SafeReplace(str1, GetTemplateValue(match2, 1), true);
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

        public string GetZettSaleDescription(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "Zett_Sale_Description");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Zett_Sale_Description"].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetListPrice(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "List_Price");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["List_Price"].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSalePrice(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "Sale_Price");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Sale_Price"].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectRelatedCode(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);

                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_RelatedItem_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                //sda.SelectCommand.Parameters.AddWithValue("@Shop_ID", Shop_ID);
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
        public string GetTemplateValue(string tmpprice, int shopID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Item_Master_SelectPrice", connectionstring);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@template_price", tmpprice);
                cmd.Parameters.AddWithValue("@ShopID", shopID);
                cmd.Connection.Open();

                string simpleValue = cmd.ExecuteScalar().ToString();
                cmd.Connection.Close();
                return simpleValue;

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
                SqlConnection connectionstring = new SqlConnection(connectionString);
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

        public string GetBrandName(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "Brand_Name");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Brand_Name"].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTemplateDescription(string[] templateID, int shopID)
        {

            DataTable dt = GetTemplateDescript(templateID, shopID);
            if (dt.Rows.Count > 0 && dt != null)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetTemplateDescript(string[] templateID, int shopID)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                for (int i = 0; i < templateID.Count(); i++)
                {
                    //string query = String.Format("SELECT * FROM Shop_Template WHERE Template_ID = {0} AND Shop_ID = {1}", templateID[i], shopID);
                    string query = "SELECT Smart_Template.Template_ID, Shop_Template.Template_Description FROM Shop_Template LEFT OUTER JOIN "
                        + "Smart_Template ON Shop_Template.Template_ID = Smart_Template.ID "
                        + String.Format("WHERE Smart_Template.Template_ID = '{0}'", templateID[i])
                        + " AND Shop_Template.Shop_ID = " + shopID;
                    SqlDataAdapter da = new SqlDataAdapter(query, connectionstring);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.SelectCommand.CommandTimeout = 0;

                    da.SelectCommand.Connection.Open();
                    da.Fill(dt);
                    da.SelectCommand.Connection.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetTemplateZettDescription(string[] templateID, string itemcode, string columnName)
        {

            DataTable dt = GetTemplateZettDescript(templateID, itemcode, columnName);
            if (dt.Rows.Count > 0 && dt != null)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetTemplateZettDescript(string[] templateID, string itemcode, string columnName)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                for (int i = 0; i < templateID.Count(); i++)
                {
                    SqlCommand cmd = new SqlCommand("SP_GetTemplateZettDescription", connectionstring);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@templateID", templateID[i]);
                    cmd.Parameters.AddWithValue("@itemcode", itemcode);
                    cmd.Parameters.AddWithValue("@columnName", columnName);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Connection.Open();
                    da.Fill(dt);
                    cmd.Connection.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSKUColorName(string Item_Code)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);

                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetSKUSizeColorName", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "color");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Color_Name"].ToString().TrimEnd(',');
                }
                else return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectImageName(string Item_Code, int ImageType)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_SelectImageName", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@ImageType", ImageType);
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

        public DataTable SelectByItemCode(string Item_Code)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);
                SqlDataAdapter da = new SqlDataAdapter("Select * From Template_Detail WITH (NOLOCK) Where Item_Code=N'" + Item_Code + "'", connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandTimeout = 0;

                DataTable dt = new DataTable();
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex) { throw ex; }

        }

        public string GetSKUSizeName(string Item_Code)
        {
            try
            {
                SqlConnection connectionstring = new SqlConnection(connectionString);

                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SP_GetSKUSizeColorName", connectionString);
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "size");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Size_Name"].ToString().TrimEnd(',');
                }
                else return "";
            }
            catch (Exception ex)
            {
                throw ex;
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

            //templateID = templateID.Where(w => w != templateID[templateID.Count() - 1]).ToArray();

            return templateID;
        }

        public string GetZettItemDescription(string Item_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection connectionstring = new SqlConnection(connectionString);

                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Master_SelectForTemplate", connectionString);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Parameters.AddWithValue("@Item_Code", Item_Code);
                sda.SelectCommand.Parameters.AddWithValue("@Option", "Zett_Item_Description");
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Zett_Item_Description"].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
