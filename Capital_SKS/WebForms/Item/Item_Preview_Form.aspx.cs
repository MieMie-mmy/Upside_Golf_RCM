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
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;

namespace ORS_RCM.WebForms.Item
{
	public partial class Item_Preview_Form : System.Web.UI.Page
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
				if (Session["CategoryList"] != null)
				{
					DataTable dt = (DataTable)Session["CategoryList"];
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

                        DataTable dt = imbl.SelectValueByItemID(ItemID);
                        if (dt.Rows.Count > 0)
                        {
                            String itemcode = dt.Rows[0]["Item_Code"] as String;
                            lblItemcode.Text = itemcode;
                            lblProduct_Name.Text = dt.Rows[0]["Item_Name"] as String;
                            lblPrice.Text = dt.Rows[0]["Sale_Price"].ToString();
                            lblListPrice.Text = dt.Rows[0]["List_Price"].ToString();
                            //BindSKU(itemcode);
                            DisplaySKU(itemcode);
                        }

                        SelectByItemID(ItemID);                //Select From Item_Image Table
                        ItemPhoto_List();//Item Photo list 1
                        BindLibraryPhoto();
                        ItemPhoto_List2();//Item Photo list2

                        SetSelectedRelatedItem(ItemID);   //Select From Item_Related_Item Table
                    }
                    else
                    {
                        SetCategoryData(); // shopcategory
                        SKUdt();
                        ItemPhoto_List();//Item Photo list 1
                        BindLibraryPhoto();
                        ItemPhoto_List2();//Item Photo list2
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
                Session["CategoryList"] = dt;
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
                extract = 0; int cat = 0;
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
                gvCategory.DataSource = CategoryList;
                gvCategory.DataBind();
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
                Session["ImageList"] = dtImage;
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
                            case 1: dt = item.Select_RelatedItem(dt.Rows[0]["Related_ItemCode"].ToString(),"","", "", "");
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

        public void BindItemPhoto2()
        {

        }

		public void ItemPhoto_List2()
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

                    dlphotoList2.DataSource = dt;
                    dlphotoList2.DataBind();
                }

                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Item_ID", typeof(int));
                    dt.Columns.Add("Image_Name", typeof(string));
                    dt.Columns.Add("Image_Type", typeof(int));

                    dt.Rows.Add(0, 0, "no_image.jpg", 0);
                    dlphotoList2.DataSource = dt;
                    dlphotoList2.DataBind();


                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
		}

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

                    dlItemPhoto.DataSource = dt;
                    dlItemPhoto.DataBind();

                    

                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Item_ID", typeof(int));
                    dt.Columns.Add("Image_Name", typeof(string));
                    dt.Columns.Add("Image_Type", typeof(int));

                    dt.Rows.Add(0, 0, "no_image.jpg", 0);
                    dlItemPhoto.DataSource = dt;
                    dlItemPhoto.DataBind();
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

                lblItemcode.Text = myDatatable[0].ToString();
                lblProduct_Name.Text = myDatatable[1].ToString();
                lblPrice.Text = myDatatable[3].ToString();
                lblListPrice.Text = myDatatable[2].ToString();
                Sale_Literal.Text = myDatatable[4].ToString();
                LiteralItemDescription.Text = myDatatable[5].ToString();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
		}

        public void DisplaySKU(string Item_Code)
        {
            Item_BL item = new Item_BL();
            string html = "<table cellspacing=\"1\" cellpadding=\"4\" border=\"1\"><tbody><tr><th>&nbsp;</th>";
            DataTable dtSKUHeader = item.GetSKUHeader(Item_Code);
            DataTable dtSKUSide = item.GetSKUSide(Item_Code);
            DataTable dtSKUQuantity = item.GetSKUQuantity(Item_Code);
            /*
            if (dtSKUHeader != null && dtSKUHeader.Rows.Count > 0 && dtSKUQuantity != null && dtSKUQuantity.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSKUHeader.Rows)
                {
                    html += String.Format("<td class=\"inventory_choice_name\" align=\"center\">{0}<br/><span class=\"inventory_choice_name\">{1}</span></td>", dr["Size_Name"].ToString(), "");
                }
                html += "</tr>";
                foreach (DataRow drQty in dtSKUQuantity.Rows)
                {
                    html += "<tr>";
                    //html += String.Format("<td>{0}<br/><span>{1}</span></td>", drQty["Color_Name"].ToString(), drQty["Color_Code"].ToString());
                    html += String.Format("<td class=\"inventory_choice_name\" align=\"center\">{0}<br/><span class=\"inventory_choice_name\">{1}</span></td>", drQty["Color_Name"].ToString(), "");
                    foreach (DataRow drSize in dtSKUHeader.Rows)
                    {
                        if (drSize["Size_Code"].ToString() == drQty["Size_Code"].ToString() && !string.IsNullOrWhiteSpace(drQty["Quantity"].ToString()) && Convert.ToInt32(drQty["Quantity"])>0)
                        {
                            //  html += String.Format("<td>&nbsp;<input type='radio' name='radio' value='{0},{1},{2}'/></td>", drQty["Quantity"], drQty["Size_Name"], drQty["Color_Name"]);

                            html += String.Format("<td class=\"inventory\" align=\"center\"><span class=\"inventory_soldout\">&nbsp;<input type='radio' name='radio' value='{0},{1},{2},{3},{4}'/></span></td>", Convert.ToInt32(drQty["Quantity"]), drQty["Size_Code"], drQty["Size_Name"], drQty["Color_Code"], drQty["Color_Name"]);
                        }
                        else
                        {
                            html += String.Format("<td class=\"inventory\" align=\"center\"><span class=\"inventory_soldout\">{0}</span></td>", "&nbsp;X");
                        }
                    }
                    html += "</tr>";
                }
                html += "</tbody></table>";

                divSKUTable.InnerHtml = html;
            }*/
            if (dtSKUHeader != null && dtSKUHeader.Rows.Count > 0 && dtSKUQuantity != null && dtSKUQuantity.Rows.Count > 0 && dtSKUSide != null && dtSKUSide.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSKUHeader.Rows)
                {
                    html += String.Format("<td class=\"inventory_choice_name\" align=\"center\">{0}<br/><span class=\"inventory_choice_name\">{1}</span></td>", dr["Size_Name"].ToString(), "");
                }
                html += "</tr>";
                foreach (DataRow drColor in dtSKUSide.Rows)
                {
                    html += "<tr>";

                    DataRow[] drSelect = dtSKUQuantity.Select("Color_Code=" + drColor["Color_Code"].ToString());

                    if (drSelect.Count() > 0)
                    {
                        DataTable dtItem = dtSKUQuantity.Select("Color_Code=" + drColor["Color_Code"].ToString()).CopyToDataTable();

                        html += String.Format("<td class=\"inventory_choice_name\" align=\"center\">{0}<br/><span class=\"inventory_choice_name\">{1}</span></td>", drColor["Color_Name"].ToString(), "");

                        foreach (DataRow drQty in dtItem.Rows)
                         {
                            if (!string.IsNullOrWhiteSpace(drQty["Quantity"].ToString()) && Convert.ToInt16(drQty["Quantity"]) > 0 )
                            {
                                html += String.Format("<td class=\"inventory\" align=\"center\"><span class=\"inventory_soldout\">&nbsp;<input type='radio' name='radio' value='{0},{1},{2},{3},{4}'/></span></td>", Convert.ToInt32(drQty["Quantity"]), drQty["Size_Code"], drQty["Size_Name"], drQty["Color_Code"], drQty["Color_Name"]);
                            }
                            else
                            {
                                html += String.Format("<td class=\"inventory\" align=\"center\"><span class=\"inventory_soldout\">{0}</span></td>", "&nbsp;X");
                            }
                        }
                        html += "</tr>";
                    }
                }
                html += "</tbody></table>";

                divSKUTable.InnerHtml = html;
            }
            else
            {
                //Label3.Visible = false;
                //txtQuantity.Visible = false;
                //btnAdd.Visible = false;
            }
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

		//protected void gvSKU_RowDataBound(object sender, GridViewRowEventArgs e)
		//{
		//  //  //if (e.Row.RowType == DataControlRowType.DataRow)
		//  //  //{
		//  //      for (int i = 0; i < gvSKU.Rows.Count; i++)
		//  //      {
		//  //          for (int j = 0; j < gvSKU.Columns.Count; j++)
		//  //          {
		//  //              if (!string.IsNullOrWhiteSpace(gvSKU.Rows[i].Cells[j].Text))
		//  //              {
		//  //                  gvSKU.Rows[i].Cells[j].Text = "o";
		//  //              }
		//  //              else
		//  //              {
		//  //                  gvSKU.Rows[i].Cells[j].Text = "x";
		//  //              }
		//  //          }
		//  //      }
		//  ////  }
		//    if (e.Row.RowType == DataControlRowType.DataRow)
		//    {
		//        for (int i = 0; i < gvSKU.Rows.Count; i++)
		//        {
		//            for (int j = 0; j < e.Row.Cells.Count; j++)
		//            {
		//                if (!string.IsNullOrWhiteSpace(gvSKU.Rows[i].Cells[j].Text) || gvSKU.Rows[i].Cells[j].Text !="0") // you can check for anything
		//                {
		//                    gvSKU.Rows[i].Cells[j].Text = "o";
		//                }
		//                else
		//                {
		//                    gvSKU.Rows[i].Cells[j].Text = "x";
		//                }
		//            }
		//        }
		//    }
		//}

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
	}
}

