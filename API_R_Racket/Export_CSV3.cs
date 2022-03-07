using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using CKSKS_BL;

namespace API_R_Painttool
{
    public class Export_CSV3
    {
        int start, end;
        string line = string.Empty;
        string replaceword = string.Empty;
        string html = string.Empty;
        string itemcode = string.Empty;
        DataTable dtvalue;

        Shop_Template_BL shopTemplate;
        Item_Master_BL master = new Item_Master_BL();
        Item_BL item = new Item_BL();
        Item_Image_BL imageBL = new Item_Image_BL();

        public DataTable ModifyTable(DataTable dtItem, int shopID)
        {
            DataTable dtFinal = new DataTable();

            if (dtItem.Rows.Count > 0)
            {

                if (dtItem.Columns.Contains("Item_Description_PC")) // Item_Description_PC (rakuten)
                {
                    string itemtemp = dtItem.Rows[0]["Item_Description_PC"].ToString();
                    if (itemtemp.Contains("zett"))
                    {
                        dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Item_Description_PC", "0"));
                    }
                    else
                    {
                        dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Item_Description_PC", "1"));
                    }  
                    //dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Item_Description_PC"));
                }
                if (dtItem.Columns.Contains("Sale_Description_PC")) // Sale_Description_PC (rakuten)
                {
                    string saletemp = dtItem.Rows[0]["Sale_Description_PC"].ToString();
                    if (saletemp.Contains("zett"))
                    {
                        dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Sale_Description_PC", "0"));
                    }
                    else
                    {
                        dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Sale_Description_PC", "1"));
                    }      
                    //dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Sale_Description_PC"));
                }
                //if (dtItem.Columns.Contains("R_Sale_Description_PC")) // Sale_Description_PC (rakuten)
                //{
                //    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "R_Sal_eDescription_PC"));
                //}
                if (dtItem.Columns.Contains("Smart_Template")) // Smart_Template (rakuten)
                {
                    string smarttemp = dtItem.Rows[0]["Smart_Template"].ToString();
                    if (smarttemp.Contains("zett"))
                    {
                        dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Smart_Template", "0"));
                    }
                    else
                    {
                        dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Smart_Template", "1"));
                    }  
                    //dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Smart_Template"));
                }
                if (dtItem.Columns.Contains("Explanation")) // Merchandise_Information (yahoo)
                {
                    dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Explanation", "1"));
                    //dtFinal = MergeTable(ChangeTemplate(dtItem, shopID, "Explanation"));
                }

                return dtFinal;
            }
            else
            {
                return null;
            }
        }

        public DataTable ChangeTemplate(DataTable dt, int shopID, string columnName,string option)
        //public DataTable ChangeTemplate(DataTable dt, int shopID, string columnName)
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
                    if (html.Contains("[[Sale_Price]]"))
                    {
                        //if()
                        //TemplateDatailChange(html, shopID,dr,columns);
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

                        //string[] tempzett = null;
                        //string[] temp = null;
                        //string strtempzett = null;
                        //string strtemp = null;
                        //for (int i = 0; i < templateID.Length; i++)
                        //{
                        //    //if (stringToCheck.Contains(templateID[i]))
                        //    if (templateID[i].Contains("zett"))
                        //    {
                        //        strtempzett = templateID[i] + ",";
                        //    }
                        //    else
                        //    {
                        //        strtemp = templateID[i] + ",";
                        //    }
                        //}
                        //if (strtempzett != null)
                        //{
                        //    tempzett = strtempzett.TrimEnd(',').Split(',');
                        //}
                        //if (strtemp != null)
                        //{
                        //    temp = strtemp.TrimEnd(',').Split(',');
                        //}

                        //if (tempzett != null)
                        //{
                        //    dtvalue = GetTemplateZettDescription(tempzett, itemcode, columnName);  //Get from ShopTemplate Table
                        //}
                        //else
                        //{
                        //    dtvalue = GetTemplateDescription(temp, shopID);  //Get from ShopTemplate Table
                        //}


                        #region ShopTemplate
                        //if (option == "0")
                        //{
                        //    dtvalue = GetTemplateZettDescription(templateID, itemcode, columnName);  //Get from ShopTemplate Table
                        //}
                        //else
                        //{
                        //    dtvalue = GetTemplateDescription(templateID, shopID);  //Get from ShopTemplate Table
                        //}

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
                            //else
                            //{
                            //    html = html.Replace("[", "").Replace("]", "");
                            //}
                        }
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

                        #region product.product_id
                        if (html.Contains("product.product_id"))
                        {
                            //if (columns.Contains("code"))  //For yahoo
                            //{
                            //    html = html.SafeReplace("product.product_id", itemcode, true);
                            //}
                            //else if (columns.Contains("商品番号"))  //For rakuten
                            //{
                            //    html = html.SafeReplace("product.product_id", itemcode, true);
                            //}
                            //else if (columns.Contains("商品ID"))  //For ponpare
                            //{
                            //    html = html.SafeReplace("product.product_id", itemcode, true);
                            //}
                            html = html.SafeReplace("product.product_id", itemcode, true);
                        }
                        #endregion

                        #region 商品名
                        if (html.Contains("商品名"))
                        {
                            //if (columns.Contains("name"))  //For yahoo
                            //{
                            //    replaceword = dr["name"].ToString();
                            //    //html = html.SafeReplace("商品名", dr["name"].ToString(), true);
                            //}
                            //else if (columns.Contains("商品名")) // For rakuten , ponpare
                            //{
                            //    replaceword = dr["商品名"].ToString();
                            //    //html = html.SafeReplace("商品名", dr["商品名"].ToString(), true);
                            //}

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
                        if (html.Contains("ゼット用項目（PC商品説明文）"))
                        {
                            replaceword = master.GetZettItemDescription(itemcode);  // select from DataBase
                            if (html.Contains("[[IF setVal=ゼット用項目（PC商品説明文）]]"))
                            {
                                start = html.IndexOf("[[IF setVal=ゼット用項目（PC商品説明文）]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("ゼット用項目（PC商品説明文）", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=ゼット用項目（PC商品説明文）]]").Length);
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
                                        html = html.Remove(start, ("[[IF setVal=ゼット用項目（PC商品説明文）]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("ゼット用項目（PC商品説明文）", replaceword, true);
                            }
                        }
                        #endregion

                        #region ゼット用項目（PC販売説明文）
                        if (html.Contains("ゼット用項目（PC販売説明文）"))
                        {
                            replaceword = master.GetZettSaleDescription(itemcode);  // select from DataBase
                            if (html.Contains("[[IF setVal=ゼット用項目（PC販売説明文）]]"))
                            {
                                start = html.IndexOf("[[IF setVal=ゼット用項目（PC販売説明文）]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("ゼット用項目（PC販売説明文）", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=ゼット用項目（PC販売説明文）]]").Length);
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
                                        html = html.Remove(start, ("[[IF setVal=ゼット用項目（PC販売説明文）]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("ゼット用項目（PC販売説明文）", replaceword, true);
                            }
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
                                        if (html.Contains("[[IF setVal=関連商品" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=関連商品" + t + "]]");

                                            if (!string.IsNullOrWhiteSpace(replaceword))
                                            {
                                                html = html.SafeReplace("関連商品_商品番号" + t, dtItem.Rows[0]["Related_ItemCode"].ToString(), true).SafeReplace("関連商品_商品名" + t, dtItem.Rows[0]["Item_Name"].ToString(), true).SafeReplace("関連商品_商品画像" + t, dtItem.Rows[0]["Image_Name"].ToString(), true).SafeReplace("関連商品_販売価格" + t, dtItem.Rows[0]["Sale_Price"].ToString(), true);
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
                                            html = html.SafeReplace("関連商品_商品番号" + t, dtItem.Rows[0]["Related_ItemCode"].ToString(), true).SafeReplace("関連商品_商品名" + t, dtItem.Rows[0]["Item_Name"].ToString(), true).SafeReplace("関連商品_商品画像" + t, dtItem.Rows[0]["Image_Name"].ToString(), true).SafeReplace("関連商品_販売価格" + t, dtItem.Rows[0]["Sale_Price"].ToString(), true);
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
                            int index1= html.IndexOf("]][[/IF]]");
                            string match2 = html.Substring(start, html.IndexOf("]][[/IF]]") - start);
                            
                            match2 = match2.Replace("[[IF ","");
                            int sale_price = Convert.ToInt32(dr["Sale_Price"].ToString());
                            if (match2.Contains(">="))
                            {
                                string[] str = match2.Split(new string[] { ">=" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price =Convert.ToInt32(str[1].ToString());
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
                            else if(match2.Contains("<="))
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
                                        html = html.Remove(start, ("[[IF "+match2+"]]").Length);
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

        public DataTable GetTemplateDescription(string[] templateID, int shopID)
        {
            shopTemplate = new CKSKS_BL.Shop_Template_BL();
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

        public DataTable GetTemplateZettDescription(string[] templateID, string itemcode, string columnName)
        {
            shopTemplate = new CKSKS_BL.Shop_Template_BL();
            DataTable dt = shopTemplate.GetTemplateZettDescription(templateID, itemcode, columnName);
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

            //templateID = templateID.Where(w => w != templateID[templateID.Count() - 1]).ToArray();

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
