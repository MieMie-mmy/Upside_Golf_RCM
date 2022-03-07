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

namespace ORS_RCM
{
    public partial class Item_Preview : System.Web.UI.Page
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
                if (Session["table"] != null)
                {
                    DataTable dt = (DataTable)Session["table"];
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

                    //SetCategoryData();

                    //BindPhotoList();

                    //BindArrayList();

                    //DataTable dtImage = ImageList as DataTable;
                    //Item_Image_BL itemImageBL = new Item_Image_BL();
                    //dtImage = SetLibraryPhoto(dtImage);



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
                        category = dt.Rows[0]["Description"].ToString() + " >> " + category;
                        serial = dt.Rows[0]["Category_SN"].ToString() + " >> " + serial;
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
                return String.Empty;
            }
        }


        public void SetCategoryData()
        {
            int rowIndex = 0;
            //gvCategore.DataSource = CategoryList;
            //gvCategore.DataBind();
            //if (CategoryList != null && CategoryList.Rows.Count > 0)
            //{
            //    for (int i = rowIndex; i < CategoryList.Rows.Count; i++)
            //    {
            //        Label lblID = (Label)gvCategore.Rows[rowIndex].Cells[1].FindControl("lblID");
            //        //Label lblValue = (Label)gvCatagories.Rows[rowIndex].Cells[1].FindControl("lblCTGName");
            //        TextBox txtValue = (TextBox)gvCategore.Rows[rowIndex].Cells[1].FindControl("txtCTGName");
            //        lblID.Text = CategoryList.Rows[i]["CID"].ToString();
            //        //lblValue.Text = CategoryList.Rows[i]["CName"].ToString();
            //        txtValue.Text = CategoryList.Rows[i]["CName"].ToString();
            //        rowIndex++;
            //    }
            }

        }

        //public void SetSelectedCategory(int itemID)
        //{
        //    itemCategoryBL = new Item_Category_BL();

        //    DataTable dt = itemCategoryBL.SelectByItemID(itemID);
        //    dt.Columns.Add("Serial_No", typeof(String));
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            treepath = string.Empty; catpath = string.Empty;
        //            dt.Rows[i]["CName"] = ShowHierarchy(int.Parse(dt.Rows[i]["CID"].ToString()));
        //            dt.Rows[i]["Serial_No"] = ShowSLHierarchy(int.Parse(dt.Rows[i]["CID"].ToString()));
        //            dt.AcceptChanges();
        //        }
        //    }
        //    Session["CategoryList"] = dt;
        //}


        //public string ShowHierarchy(int CID)
        //{
        //    //Array.Clear(ids, 0, 100);
        //    index = 0;
        //    int i = 0;
        //    extract = 0; int cat = 0;
        //    ids[index++] = CID.ToString();
        //    GetCategory(CID);

        //    while (!String.IsNullOrWhiteSpace(ids[i]))
        //    {
        //        DataTable dts = cbl.SelectForCatalogID(Convert.ToInt32(ids[i++]));
        //        if (dts != null && dts.Rows.Count > 0)
        //        {
        //            ex[extract++] = dts.Rows[0]["Description"].ToString();
        //            // lblparnode.Text += dts.Rows[0]["Description"].ToString() + ",";
        //        }
        //    }

        //    for (int x = extract - 1; x >= 0; x--)
        //    {
        //        if (x > 0)
        //        {
        //            treepath += ex[x] + "  >>  ";
        //        }
        //        else if (x == 0)
        //        {
        //            treepath += ex[x];
        //        }
        //    }

        //    return treepath;

        //}

        //public string ShowSLHierarchy(int CID)
        //{
        //    //Array.Clear(ids, 0, 100);
        //    index = 0;
        //    int i = 0;
        //    int cat = 0;
        //    ids[index++] = CID.ToString();
        //    GetCategory(CID);

        //    while (!String.IsNullOrWhiteSpace(ids[i]))
        //    {
        //        DataTable dts = cbl.SelectForCatalogID(Convert.ToInt32(ids[i++]));
        //        if (dts != null && dts.Rows.Count > 0)
        //        {
        //            cx[cat++] = dts.Rows[0]["Category_SN"].ToString();
        //        }
        //    }

        //    for (int y = cat - 1; y >= 0; y--)
        //    {
        //        if (y > 0)
        //        {
        //            catpath += cx[y] + ">>";

        //        }
        //        else if (y == 0)
        //        {
        //            catpath += cx[y];
        //        }
        //    }

        //    return catpath;
        //}

        //public void GetCategory(int id)
        //{
        //    cbl = new Category_BL();
        //    DataTable dt = cbl.SelectForCatalogID(id);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        ids[index++] = dt.Rows[i]["ParentID"].ToString();
        //        GetCategory(Convert.ToInt32(dt.Rows[i]["ParentID"].ToString()));
        //    }
        //}




        //public void BindPhotoList()
        //{
        //    if (ImageList != null)
        //    {
        //        DataTable dt = ImageList as DataTable;
        //        DataRow[] dr = dt.Select("Image_Type='0'");
        //        if (dr.Length > 0)
        //        {
        //            DataTable dtt = dt.Select("Image_Type='0'").CopyToDataTable();
        //            for (int i = 0; i < dtt.Rows.Count; i++)
        //            {

        //                String image = dtt.Rows[i]["Image_Name"].ToString();
        //                //String image2= dtt.Rows[1]["Image_Name"].ToString();
        //                //String image3= dtt.Rows[2]["Image_Name"].ToString();
        //                image1.ImageUrl = "~/Item_Image/" + image;
        //                //image2.ImageUrl = "~/Item_Image/" + image2;
        //                //image3.ImageUrl = "~/Item_Image/" + image3;

        //                dlPhoto.DataSource = dt.Select("Image_Type='0'").CopyToDataTable();
        //                dlPhoto.DataBind();
        //            }

        //        }
        //        else
        //        {

        //            lblmessage.Visible = true;
        //        }

        //        dr = dt.Select("Image_Type='1'");
        //        if (dr.Length > 0)
        //        {
        //            DataTable dtLibrary = dt.Select("Image_Type='1'").CopyToDataTable();
        //        }
        //    }
        //}




        //public DataTable SetLibraryPhoto(DataTable dt)
        //{
        //    if (dt == null) // not exist ImageList
        //    {
        //        dt = new DataTable();
        //        dt.Columns.Add(new DataColumn("Item_ID", typeof(int)));
        //        dt.Columns.Add(new DataColumn("Image_Name", typeof(string)));
        //        dt.Columns.Add(new DataColumn("Image_Type", typeof(int)));

        //        DataRow dr1 = dt.NewRow();


        //        return dt;
        //    }
        //    else   //exist ImageList
        //    {
        //        DataTable dtCopy = dt.Copy();
        //        for (int i = 0; i < dtCopy.Rows.Count; i++)
        //        {
        //            if (dtCopy.Rows[i]["Image_Type"].ToString() == "1")
        //            {
        //                dt.Rows[i].Delete();

        //            }
        //        }
        //        dt.AcceptChanges();

        //        DataRow dr1 = dt.NewRow();
                //dr1["Image_Name"] = image1;
                //dr1["Image_Type"] = 1;
                //dt.Rows.Add(dr1);

                //DataRow dr2 = dt.NewRow();
                //dr2["Image_Name"] = image2;
                //dr2["Image_Type"] = 1;
                //dt.Rows.Add(dr2);

                //DataRow dr3 = dt.NewRow();
                ////dr3["Image_Name"] = image3;
                //dr3["Image_Type"] = 1;
                //dt.Rows.Add(dr3);


        //        return dt;
        //    }
        //}

        //public void BindArrayList()
        //{
        //    ArrayList myDatatable;
        //    if (Session["myDatatable"] != null)
        //    {
        //        myDatatable = (ArrayList)Session["myDatatable"];
        //    }
        //    else
        //    {
        //        myDatatable = new ArrayList();
        //    }

        //    Sale_Literal.Text = myDatatable[4].ToString();




        }

//    }
//}

