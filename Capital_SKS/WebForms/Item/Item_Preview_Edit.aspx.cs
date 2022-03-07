using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

namespace ORS_RCM.WebForms.Item
{
    public partial class Item_Preview_Edit : System.Web.UI.Page
    {
        Category_BL cbl;
        public int index = 0;
        public String[] ids = new String[100];
        public int extract = 0;
        public String[] ex = new String[6];
        string treepath = string.Empty;
        DataTable dt = new DataTable();
        public String[] cx = new String[100];
        string catpath = string.Empty;

        Item_Category_BL itemCategoryBL;

        ArrayList myDatatable;

        public int ItemID
        {
            get
            {
                if (Request.QueryString["ID"] != null)
                {
                    return Convert.ToInt32(Request.QueryString["ID"]);
                }
                else
                {
                    return 0;
                }
            }
        }

        public DataTable CategoryList
        {
            get
            {
                if (Session["CategoryList_"+Item_Code] != null)
                {
                    DataTable dt = (DataTable)Session["CategoryList_"+Item_Code];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable ImageList
        {
            get
            {
                if (Session["ImageList_"+Item_Code] != null)
                {
                    DataTable dt = (DataTable)Session["ImageList_"+Item_Code];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public string Item_Code
        {
            get
            {
                if (Request.QueryString["Item_Code"] != null)
                {
                    return Request.QueryString["Item_Code"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (ItemID != 0)
                    {

                        Item_Master_BL imbl = new Item_Master_BL();
                        //int ItemID = imbl.SelectItemID(ItemCode);
                        SetSelectedCategory(ItemID);
                        SetCategoryData();

                        DataTable dtv = imbl.SelectValueByItemID(ItemID);
                        Export_CSV3 export = new Export_CSV3();
                        DataTable dt = export.ModifyTable(dtv, 8);  // DataTable dtv and Shop_ID='sportsplaza'
                        SelectByItemID(ItemID);                //Select From Item_Image Table
                        if (dt.Rows.Count > 0)
                        {
                            String itemcode = dt.Rows[0]["商品番号"] as String;
                            lblItemcode.Text = itemcode;
                            lblProduct_Name.Text = dt.Rows[0]["Item_Name"] as String;
                            lblPrice.Text = dt.Rows[0]["Sale_Price"].ToString();
                            lblListPrice.Text = dt.Rows[0]["List_Price"].ToString();

                            //Literal_Sale_Description.Text = dt.Rows[0]["PC用販売説明文"].ToString();
                            Literal_Sale_Description.Text = ChangeLocalImagePath(dt.Rows[0]["PC用販売説明文"].ToString());
                            Literal_Item_Description.Text = dt.Rows[0]["PC用商品説明文"].ToString();
                            //BindSKU(itemcode);
                            DisplaySKU(itemcode);
                        }
                        ItemPhoto_List();//Item Photo list 1
                        BindLibraryPhoto();
                       // ItemPhoto_List2();//Item Photo list2

                        SetSelectedRelatedItem(ItemID);   //Select From Item_Related_Item Table
                    }
                    else
                    {
                        SetCategoryData(); // shopcategory
                        SKUdt();
                        ItemPhoto_List();//Item Photo list 1
                        BindLibraryPhoto();
                        //ItemPhoto_List2();//Item Photo list2
                        BindArrayList();
                        RelatedItemdt();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected String getCategory(String id, String category, String serial, String result)
        {
            try
            {
                Category_BL cbl = new Category_BL();
                DataTable dt = cbl.SelectForCatalogID(Convert.ToInt32(id));
                if (dt.Rows.Count > 0)
                {
                    if (String.IsNullOrWhiteSpace(category))
                    {
                        category = dt.Rows[0]["Description"].ToString();
                        serial = dt.Rows[0]["Category_SN"].ToString();
                    }
                    else
                    {
                        category = dt.Rows[0]["Description"].ToString() + ">>" + category;
                        serial = dt.Rows[0]["Category_SN"].ToString() + ">>" + serial;
                    }

                    if (!dt.Rows[0]["ParentID"].ToString().Equals("0"))
                    {
                        result = getCategory(dt.Rows[0]["ParentID"].ToString(), category, serial, result);
                        String[] strarr = new String[2];
                        strarr = result.Split(',');
                        category = strarr[0];
                        serial = strarr[1];
                    }
                }
                return category + "," + serial;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return string.Empty;
            }
        }

        public void SetSelectedCategory(int itemID)
        {
            try
            {
                itemCategoryBL = new Item_Category_BL();

                DataTable dt = itemCategoryBL.SelectByItemID(itemID);

                //dt.Columns.Add("Category_SN", typeof(String));
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        treepath = string.Empty; catpath = string.Empty;
                        dt.Rows[i]["CName"] = ShowHierarchy(int.Parse(dt.Rows[i]["CID"].ToString()));
                        if (!string.IsNullOrWhiteSpace(dt.Rows[i]["Category_SN"].ToString()))
                        {
                            dt.Rows[i]["Category_SN"] = int.Parse(dt.Rows[i]["Category_SN"].ToString());
                        }
                        else
                        {
                            dt.Rows[i]["Category_SN"] = 0;
                        }

                        dt.AcceptChanges();
                    }
                }
                Session["CategoryList_"+Item_Code] = dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public string ShowHierarchy(int CID)
        {
            try
            {
                //Array.Clear(ids, 0, 100);
                index = 0;
                int i = 0;
                extract = 0; 
                ids[index++] = CID.ToString();
                GetCategory(CID);

                while (!String.IsNullOrWhiteSpace(ids[i]) && ids[i].ToString() != "1")
                {
                    DataTable dts = cbl.SelectForCatalogID(Convert.ToInt32(ids[i++]));
                    if (dts != null && dts.Rows.Count > 0)
                    {
                        ex[extract++] = dts.Rows[0]["Description"].ToString();
                        // lblparnode.Text += dts.Rows[0]["Description"].ToString() + ",";
                    }
                }

                for (int x = extract - 1; x >= 0; x--)
                {
                    if (x > 0)
                    {
                        treepath += ex[x] + "\\";
                    }
                    else if (x == 0)
                    {
                        treepath += ex[x];
                    }
                }

                return treepath;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }

        }

        public void SetCategoryData()
        {
            try
            {
                ArrayList ar = new ArrayList();
                if (CategoryList != null && CategoryList.Rows.Count >0)
                {
                    DataTable dt = CategoryList as DataTable;
                    
                    ar.AddRange(dt.Rows[0]["CName"].ToString().Split('\\'));

                    //dlCategory.DataSource = ar;
                    //dlCategory.DataBind();

                    gvCategory.DataSource = CategoryList;
                    gvCategory.DataBind();
                }
                
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void GetCategory(int id)
        {
            try
            {
                cbl = new Category_BL();
                DataTable dt = cbl.SelectForCatalogID(id);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ids[index++] = dt.Rows[i]["ParentID"].ToString();
                    GetCategory(Convert.ToInt32(dt.Rows[i]["ParentID"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void SelectByItemID(int itemID)
        {
            try
            {
                Item_Image_BL itemImageBL = new Item_Image_BL();
                DataTable dtImage = itemImageBL.SelectByItemID(itemID);
                Session["ImageList_"+Item_Code] = dtImage;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void SetSelectedRelatedItem(int ItemID)
        {
            try
            {
                Item_Related_Item_BL ItemRelatedBL = new Item_Related_Item_BL();
                DataTable dt = ItemRelatedBL.SelectByItemID(ItemID);
                Item_BL item = new Item_BL();
                if (dt != null && dt.Rows.Count > 0)
                {
                    switch (dt.Rows.Count)
                    {
                        case 1: dt = item.Select_RelatedItem(dt.Rows[0]["Related_ItemCode"].ToString(), "", "", "", "");
                            break;
                        case 2: dt = item.Select_RelatedItem(dt.Rows[0]["Related_ItemCode"].ToString(), dt.Rows[1]["Related_ItemCode"].ToString(), "", "", "");
                            break;
                        case 3: dt = item.Select_RelatedItem(dt.Rows[0]["Related_ItemCode"].ToString(), dt.Rows[1]["Related_ItemCode"].ToString(), dt.Rows[2]["Related_ItemCode"].ToString(), "", "");
                            break;
                        case 4: dt = item.Select_RelatedItem(dt.Rows[0]["Related_ItemCode"].ToString(), dt.Rows[1]["Related_ItemCode"].ToString(), dt.Rows[2]["Related_ItemCode"].ToString(), dt.Rows[3]["Related_ItemCode"].ToString(), "");
                            break;
                        case 5: dt = item.Select_RelatedItem(dt.Rows[0]["Related_ItemCode"].ToString(), dt.Rows[1]["Related_ItemCode"].ToString(), dt.Rows[2]["Related_ItemCode"].ToString(), dt.Rows[3]["Related_ItemCode"].ToString(), dt.Rows[4]["Related_ItemCode"].ToString());
                            break;
                    }

                }
                RelatedItemList.DataSource = dt;
                RelatedItemList.DataBind();

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
            //Session["Related_Item_List"] = dt;
        }

        //public void ItemPhoto_List2()
        //{
        //    try
        //    {
        //        if (ImageList != null)
        //        {
        //            DataTable dtTmp = ImageList as DataTable;

        //            DataTable dt = new DataTable();
        //            dt.Columns.Add("ID", typeof(int));
        //            dt.Columns.Add("Item_ID", typeof(int));
        //            dt.Columns.Add("Image_Name", typeof(string));
        //            dt.Columns.Add("Image_Type", typeof(int));

        //            foreach (DataRow dr in dtTmp.Rows)
        //            {
        //                if (!String.IsNullOrEmpty(dr["Image_Name"].ToString()) && dr["Image_Type"].ToString() == "0")
        //                {
        //                    dt.Rows.Add((int)dr["ID"], (int)dr["Item_ID"], (string)dr["Image_Name"], (int)dr["Image_Type"]);
        //                }
        //            }

        //            dlphotoList2.DataSource = dt;
        //            dlphotoList2.DataBind();
        //        }

        //        else
        //        {
        //            DataTable dt = new DataTable();
        //            dt.Columns.Add("ID", typeof(int));
        //            dt.Columns.Add("Item_ID", typeof(int));
        //            dt.Columns.Add("Image_Name", typeof(string));
        //            dt.Columns.Add("Image_Type", typeof(int));

        //            dt.Rows.Add(0, 0, "no_image.jpg", 0);
        //            dlphotoList2.DataSource = dt;
        //            dlphotoList2.DataBind();


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["Exception"] = ex.ToString();
        //        Response.Redirect("~/CustomErrorPage.aspx?");
        //    }
        //}

        public void ItemPhoto_List()
        {
            try
            {
                if (ImageList != null)
                {
                    DataTable dtTmp = ImageList as DataTable;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Item_ID", typeof(int));
                    dt.Columns.Add("Image_Name", typeof(string));
                    dt.Columns.Add("Image_Type", typeof(int));


                    foreach (DataRow dr in dtTmp.Rows)
                    {
                        if (!String.IsNullOrEmpty(dr["Image_Name"].ToString()) && dr["Image_Type"].ToString() == "0")
                        {
                            dt.Rows.Add((int)dr["ID"], (int)dr["Item_ID"], (string)dr["Image_Name"], (int)dr["Image_Type"]);
                        }
                    }

                    //dlItemPhoto.DataSource = dt;
                    //dlItemPhoto.DataBind();

                    dlItemPhoto1.DataSource = dt;
                    dlItemPhoto1.DataBind();

                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Item_ID", typeof(int));
                    dt.Columns.Add("Image_Name", typeof(string));
                    dt.Columns.Add("Image_Type", typeof(int));

                    dt.Rows.Add(0, 0, "no_image.jpg", 0);
                    //dlItemPhoto.DataSource = dt;
                    //dlItemPhoto.DataBind();
                    dlItemPhoto1.DataSource = dt;
                    dlItemPhoto1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void SetLibraryPhoto(String image)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(image))
                {
                    lblLibararyPhoto.Visible = false;
                    //image = "no_image.jpg";
                    //lblLibararyPhoto.ImageUrl = "~/Item_Image/" + image;
                }
                else
                {
                    lblLibararyPhoto.Visible = true;
                    lblLibararyPhoto.ImageUrl = "~/Item_Image/" + image;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void BindLibraryPhoto()
        {
            try
            {
                if (Session["myDatatable"] != null)
                {
                    myDatatable = (ArrayList)Session["myDatatable"];
                    string image = myDatatable[11].ToString();

                    SetLibraryPhoto(image);
                }
                else if (ImageList != null)
                {
                    DataTable dt = ImageList as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow[] dr = dt.Select("Image_Type=1");
                        if (dr.Count() > 0)
                        {
                            DataTable dtLib = dt.Select("Image_Type=1").CopyToDataTable();
                            SetLibraryPhoto(dtLib.Rows[0]["Image_Name"].ToString());
                        }
                    }
                }

                //DataTable dt = ImageList as DataTable;

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void BindArrayList()
        {
            try
            {
                if (Session["myDatatable"] != null)
                {
                    myDatatable = (ArrayList)Session["myDatatable"];
                }
                else
                {
                    myDatatable = new ArrayList();
                }

                DataTable dtv = new DataTable();
                dtv.Columns.Add("商品番号");
                dtv.Columns.Add("PC用販売説明文");
                dtv.Columns.Add("PC用商品説明文");
                dtv.Rows.Add(myDatatable[0].ToString(), myDatatable[5].ToString(),myDatatable[4].ToString());
                dtv.AcceptChanges();
                Export_CSV3 export = new Export_CSV3();
                DataTable dt = export.ModifyTable(dtv, 1);

                lblItemcode.Text = myDatatable[0].ToString();
                lblProduct_Name.Text = myDatatable[1].ToString();
                lblPrice.Text = myDatatable[3].ToString();
                lblListPrice.Text = myDatatable[2].ToString();
                if (dtv != null && dtv.Rows.Count > 0)
                {
                    //Literal_Sale_Description.Text = myDatatable[4].ToString();
                    //Literal_Item_Description.Text = myDatatable[5].ToString();
                    Literal_Sale_Description.Text = ChangeLocalImagePath(dt.Rows[0]["PC用販売説明文"].ToString());
                    Literal_Item_Description.Text = dt.Rows[0]["PC用商品説明文"].ToString();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void Old_DisplaySKU(string Item_Code)
        {
            Item_BL item = new Item_BL();
            string html = "<table cellspacing=\"1\" cellpadding=\"4\" border=\"0\"><tbody><tr><td class=\"inventory_choice_name\" align=\"center\">&nbsp;</td>";
            DataTable dtSKUHeader = item.GetSKUHeader(Item_Code);
            DataTable dtSKUSide = item.GetSKUSide(Item_Code);
            DataTable dtSKUQuantity = item.GetSKUQuantity(Item_Code);

            if (dtSKUHeader != null && dtSKUHeader.Rows.Count > 0 && dtSKUQuantity != null && dtSKUQuantity.Rows.Count > 0 && dtSKUSide != null && dtSKUSide.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSKUHeader.Rows)
                {
                    //html += String.Format("<td class=\"inventory_choice_name\" align=\"center\">{0}<br/><span class=\"inventory_choice_name\">{1}</span></td>", dr["Size_Name"].ToString(), "");
                    html += String.Format("<td nowrap=\"\" class=\"inventory_choice_name\" align=\"center\"><span class=\"inventory_choice_name\">{0}</span></td>", dr["Size_Name"].ToString(), "");
                }
                html += "</tr>";
                foreach (DataRow drColor in dtSKUSide.Rows)
                {
                    html += "<tr>";

                    DataRow[] drSelect = dtSKUQuantity.Select("Color_Code=" + drColor["Color_Code"].ToString());

                    if (drSelect.Count() > 0)
                    {
                        DataTable dtItem = dtSKUQuantity.Select("Color_Code=" + drColor["Color_Code"].ToString()).CopyToDataTable();

                        //html += String.Format("<td class=\"inventory_choice_name\" align=\"center\">{0}<br/><span class=\"inventory_choice_name\">{1}</span></td>", drColor["Color_Name"].ToString(), "");
                        html += String.Format("<td nowrap=\"\" class=\"inventory_choice_name\" align=\"center\"><span class=\"inventory_choice_name\">{0}</span></td>", drColor["Color_Name"].ToString(), "");

                        foreach (DataRow drQty in dtItem.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(drQty["Quantity"].ToString()) && Convert.ToInt16(drQty["Quantity"]) > 0)
                            {
                                //html += String.Format("<td class=\"inventory\" align=\"center\"><span class=\"inventory_soldout\">&nbsp;<input type='radio' name='radio' value='{0},{1},{2},{3},{4}'/></span></td>", Convert.ToInt32(drQty["Quantity"]), drQty["Size_Code"], drQty["Size_Name"], drQty["Color_Code"], drQty["Color_Name"]);
                                html += String.Format("<td class=\"inventory\" align=\"center\"><input value='{0},{1},{2},{3},{4}' type=\"radio\" name=\"inventory_id\"><font size=\"3\" color=\"#FF0000\"></font></td>", Convert.ToInt32(drQty["Quantity"]), drQty["Size_Code"], drQty["Size_Name"], drQty["Color_Code"], drQty["Color_Name"]);
                            }
                            else
                            {
                                //html += String.Format("<td class=\"inventory\" align=\"center\"><span class=\"inventory_soldout\">{0}</span></td>", "&nbsp;X");
                                html += String.Format("<td class=\"inventory\" align=\"center\"><span class=\"inventory_soldout\">{0}</span></td>", "&nbsp;X");
                            }
                        }
                        html += "</tr>";
                    }
                }
                html += "</tbody></table>";

                divSKUTable.InnerHtml = html;
            }
        }

        protected void DisplaySKU(string Item_Code)
        {
            Item_BL item = new Item_BL();
            string html = "<table class=\"listTable\"><tbody><tr><th>&nbsp;</th>";
            DataTable dtSKUHeader = item.GetSKUHeader(Item_Code);
            DataTable dtSKUQuantity = item.GetSKUQuantity(Item_Code);

            foreach (DataRow dr in dtSKUHeader.Rows)
            {
                html += String.Format("<th>{0}<span></span></th>", dr["Size_Name"].ToString(), dr["Size_Code"].ToString());
            }
            html += "</tr>";

            string criColor = "";
            dtSKUQuantity.DefaultView.Sort = "Color_Code ASC";
            dtSKUQuantity = dtSKUQuantity.DefaultView.ToTable();
            string[] arr = new string[3];

            DataTable dtTmp = dtSKUQuantity;
            dtTmp.DefaultView.Sort = "Size_Code ASC";
            dtTmp = dtTmp.DefaultView.ToTable();

            foreach (DataRow drQty in dtSKUQuantity.Rows)
            {
                string colorcode = drQty["Color_Code"].ToString();
                DataRow[] drTmp = dtTmp.Select("Color_Code = '" + colorcode + "'");

                if (criColor != drQty["Color_Code"].ToString())
                {
                    html += "<tr>";
                    html += String.Format("<td>{0}<span></span></td>", drQty["Color_Name"].ToString(), drQty["Color_Code"].ToString());

                    int i = 0;
                    foreach (DataRow drSize in dtSKUHeader.Rows)
                    {
                        if (drSize["Size_Code"].ToString() == drTmp[i]["Size_Code"].ToString())
                        {
                            if (drTmp[i]["Quantity"].ToString() != "0" && !string.IsNullOrWhiteSpace(drTmp[i]["Quantity"].ToString()))
                            {
                                html += String.Format("<td class=\"inventory\" align=\"center\"><input type='radio' name='radio' value='{0},{1},{2},{3},{4}'/></td>", drTmp[i]["Quantity"].ToString(), drTmp[i]["Size_Code"].ToString(), drQty["Size_Name"], drQty["Color_Code"], drQty["Color_Name"]);
                            }
                            else
                            {
                                html += String.Format("<td class=\"inventory\" align=\"center\"><span class=\"inventory_soldout\">{0}</span></td>", "X");
                            }
                            //html += String.Format("<td>{0}</td>", drTmp[i]["Quantity"].ToString());
                            if (i < drTmp.Count() - 1)
                            {
                                i++;
                            }
                        }
                        else
                        {
                            html += String.Format("<td class=\"inventory\" align=\"center\"><span class=\"inventory_soldout\">{0}</span></td>", "X");
                        }
                    }
                    html += "</tr>";
                }

                criColor = drQty["Color_Code"].ToString();
            }

            html += "</tbody></table>";

            divSKUTable.InnerHtml = html;
        }

        public void SKUdt()
        {
            try
            {
                DataTable dt = new DataTable();

                if (Session["myDatatable"] != null)
                {
                    myDatatable = (ArrayList)Session["myDatatable"];
                }
                else
                {
                    myDatatable = new ArrayList();
                }

                string Itemcode = myDatatable[0].ToString();
                //BindSKU(Itemcode);
                DisplaySKU(Itemcode);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void RelatedItemdt()
        {
            try
            {
                DataTable dt = new DataTable();

                if (Session["myDatatable"] != null)
                {
                    myDatatable = (ArrayList)Session["myDatatable"];
                }
                else
                {
                    myDatatable = new ArrayList();
                }


                string relatedItem1 = myDatatable[6].ToString(),
                   relatedItem2 = myDatatable[7].ToString(),
                   relatedItem3 = myDatatable[8].ToString(),
                   relatedItem4 = myDatatable[9].ToString(),
                   relatedItem5 = myDatatable[10].ToString();

                Item_BL item = new Item_BL();

                dt = item.Select_RelatedItem(relatedItem1, relatedItem2, relatedItem3, relatedItem4, relatedItem5);

                RelatedItemList.DataSource = dt;
                RelatedItemList.DataBind();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public bool IsNumber(String str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ChangeLocalImagePath(string dtColumn)
        {
            DataTable dtTmp = ImageList as DataTable;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Item_ID", typeof(int));
            dt.Columns.Add("Image_Name", typeof(string));
            dt.Columns.Add("Image_Type", typeof(int));
            if (dtTmp != null)
            {
                foreach (DataRow dr in dtTmp.Rows)
                {
                    if (!String.IsNullOrEmpty(dr["Image_Name"].ToString()) && dr["Image_Type"].ToString() == "0")
                    {
                        dt.Rows.Add((int)dr["ID"], (int)dr["Item_ID"], (string)dr["Image_Name"], (int)dr["Image_Type"]);
                    }
                }
            }
            if (dtColumn.Contains("<!-- 販売説明文トップ商品画像表示 -->"))
            {
                ICollection<string> matches = Regex.Matches(dtColumn.Replace(Environment.NewLine, ""), @"src=\""(.+?)\""")
                                                                    .Cast<Match>()
                                                                    .Select(x => x.Groups[1].Value)
                                                                    .ToList();
                foreach(DataRow dr in dt.Rows)
                {
                    if (matches.Count != 0)
                    {
                        foreach (string match in matches)
                        {
                            if (!string.IsNullOrWhiteSpace(match) && match.Contains(dr["Image_Name"].ToString()))
                            {
                                dtColumn = dtColumn.Replace(match, "../../Item_Image/" + dr["Image_Name"].ToString());
                            }
                        }
                    }
                }
             }

            return dtColumn;
        }

        protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //foreach (TableCell cell in e.Row.Cells)
            //{
            //    cell.Text = cell.Text.Replace("￥", ">");
            //}
        }

    }
}