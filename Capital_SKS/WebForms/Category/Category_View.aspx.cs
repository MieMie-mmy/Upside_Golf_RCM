/* 
Created By              : Kay Thi Aung
Created Date          : 20/06/2014
Updated By             :
Updated Date         :

 Tables using: Category
    -
    -
    -
*/

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;
using System.IO;
using System.Text;
using System.Configuration;
using System.Net;
using System.Text.RegularExpressions;

namespace ORS_RCM
{
    public partial class Category_View : System.Web.UI.Page
    {
        Category_BL cbl;
        String[] ids = new String[100];
        public int index = 0; public int parid;
        String CSVpath = ConfigurationManager.AppSettings["ExportCSVPath"].ToString();
        String[] searchcatdesc = new String[5]; String[] searchpath = new String[5];
        string fdesc = string.Empty; string sdesc = string.Empty; string tdesc = string.Empty;
        string fourdesc = string.Empty; string fivedesc = string.Empty;
        DataTable Maindt = new DataTable();
  
               
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    cbl = new Category_BL();
                    //  ParentNode();
                    parendnote();
                    gvtreeview.DataSource = cbl.SelectForTreeview(1);
                    gvtreeview.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void parendnote() 
        {
            try
            {
                tvCategory.Nodes.Clear();
                cbl = new Category_BL();
                DataTable dt = cbl.SelectForTreeview(0);
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        TreeNode parentNode = new TreeNode();

                        parentNode.Text = dt.Rows[i]["Description"].ToString();
                        parentNode.Value = dt.Rows[i]["ID"].ToString();
                        parentNode.SelectAction = TreeNodeSelectAction.Select;


                        tvCategory.Nodes.Add(parentNode);
                        Childnode(parentNode);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }        
        }

        protected void Childnode(TreeNode parentNode) 
        {
            try
            {
                DataTable dt = new DataTable();

                if (parentNode.Value == "1")
                {
                    dt = cbl.SelectForTreeview(Convert.ToInt32(parentNode.Value));

                }
                else
                {
                    dt = cbl.SelectForTreeview1(Convert.ToInt32(parentNode.Value));

                }
                if (dt.Rows.Count > 0)
                {
                    int i = 0; string cid = dt.Rows[i]["ParentID"].ToString();
                    while (i < dt.Rows.Count)
                    {
                        TreeNode childNode = new TreeNode();

                        if (cid == "1")
                        {

                            childNode.Text = dt.Rows[i]["Description"].ToString();
                            childNode.Value = dt.Rows[i]["ID"].ToString();
                            childNode.SelectAction = TreeNodeSelectAction.Select;
                            parentNode.ChildNodes.Add(childNode);
                            childNode.Collapse();
                            parentNode.Expand();
                        }
                        else
                        {

                            childNode.Text = "";
                            childNode.Value = null;
                            childNode.SelectAction = TreeNodeSelectAction.Select;
                            parentNode.ChildNodes.Add(childNode);

                        }

                        cid = dt.Rows[i]["ParentID"].ToString();



                        if (!String.IsNullOrWhiteSpace(childNode.Value))
                            Childnode(childNode);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    /// <summary>
    /// to bind treeview
    /// </summary>
        protected void ParentNode()
        {
            try
            {
                tvCategory.Nodes.Clear();
                cbl = new Category_BL();
                DataTable dt = cbl.SelectForTreeview(0);
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        TreeNode parentNode = new TreeNode();

                        parentNode.Text = dt.Rows[i]["Description"].ToString();
                        parentNode.Value = dt.Rows[i]["ID"].ToString();
                        parentNode.SelectAction = TreeNodeSelectAction.Select;


                        tvCategory.Nodes.Add(parentNode);
                        AddChildNode(parentNode);
                        i++;
                    }
                }
                //   tvCategory.CollapseAll();
                tvCategory.ExpandAll();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void AddChildNode(TreeNode parentNode)
        {
            try
            {
                cbl = new Category_BL();
                DataTable dt = cbl.SelectForTreeview(Convert.ToInt32(parentNode.Value));
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Text = dt.Rows[i]["Description"].ToString();
                        childNode.Value = dt.Rows[i]["ID"].ToString();
                        childNode.SelectAction = TreeNodeSelectAction.Select;
                        parentNode.ChildNodes.Add(childNode);
                        AddChildNode(childNode);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                
               
                if (!String.IsNullOrWhiteSpace(txtdesc.Text))
                {
                    ViewState["IDdt"] = null;
                    tvCategory.Nodes.Clear();
                    cbl = new Category_BL();
                    Item_Category_BL icbl = new Item_Category_BL();
                    string description =txtdesc.Text.Trim();
                    string replaceWith = " ";
                    string line2 = Regex.Replace(description, @"　", replaceWith);//for jp space
                    searchcatdesc = null; 
                    searchcatdesc = line2.Split(' ');
                  
                  for (int y = 0; y < searchcatdesc.Count(); y++) 
                  {
                      if (y == 0)
                          fdesc = searchcatdesc[y];
                      else if (y == 1)
                          sdesc = searchcatdesc[y];
                      else if (y == 2)
                          tdesc = searchcatdesc[y];
                      else if (y == 3)
                          fourdesc = searchcatdesc[y];
                      else if (y == 4)
                          fivedesc = searchcatdesc[y];
                      else
                          break;
                  }
                  DataTable dt = cbl.Search(txtcatid.Text.Trim(), fdesc.Trim(), sdesc.Trim(), tdesc.Trim(), fourdesc.Trim(), fivedesc.Trim());
                    //DataTable Maindt = new DataTable();
                    Maindt.Columns.Add("ID", typeof(int));
                    Maindt.Columns.Add("ParentID", typeof(int));
                    Maindt.Columns.Add("Description", typeof(String));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            Maindt.Merge(icbl.getAllParentsbyName(Convert.ToInt32(dt.Rows[j]["ParentID"].ToString()), fdesc.Trim(),sdesc.Trim(),tdesc.Trim(),fourdesc.Trim(),fivedesc.Trim()));
                       }
                    }

                    Maindt = RemoveDuplicateRows(Maindt, "ID");

                    DataRow[] dr = Maindt.Select("ParentID=1");
                    if (dr.Count() > 0)
                    {
                        DataTable dtParent = Maindt.Select("ParentID=1").CopyToDataTable();
                        for (int i = 0; i < dtParent.Rows.Count; i++)
                        {
                         
                                TreeNode parentNode = new TreeNode();

                                parentNode.Text = dtParent.Rows[i]["Description"].ToString();
                                parentNode.Value = dtParent.Rows[i]["ID"].ToString();
                                parentNode.SelectAction = TreeNodeSelectAction.Select;

                                tvCategory.Nodes.Add(parentNode);
                                AddChildNoteSearch(parentNode, Maindt);
                       
                        }
                    }

                    gvtreeview.DataSource = Maindt;
                    gvtreeview.DataBind();
                    ViewState["IDdt"] = Maindt;
                }
                else
                {
                   // ParentNode();
                    parendnote();
                    gvtreeview.DataSource = cbl.SelectForTreeview(1);
                    gvtreeview.DataBind();
                 
                }
                //tvCategory.Nodes.Clear();
               
            }
            catch (Exception ex)
            {

                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void AddChildNoteSearch(TreeNode parentNode, DataTable Maindt)
        {
            try
            {
                DataRow[] dr = Maindt.Select("ParentID=" + parentNode.Value);
                if (dr.Count() > 0)
                {
                    int i = 0;
                    DataTable dt = Maindt.Select("ParentID=" + parentNode.Value).CopyToDataTable();
                    while (i < dt.Rows.Count)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Text = dt.Rows[i]["Description"].ToString();
                        childNode.Value = dt.Rows[i]["ID"].ToString();
                        childNode.SelectAction = TreeNodeSelectAction.Select;
                        parentNode.ChildNodes.Add(childNode);
                        AddChildNoteSearch(childNode, Maindt);
                        i++;
                        childNode.Collapse();
                        parentNode.Expand();
                      
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
       
        protected void Getdata(int id)
        {
            try
            {
                cbl = new Category_BL();
                DataTable dt = cbl.SelectForTreeview(id);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ids[index++] = dt.Rows[i]["ParentID"].ToString();
                    Getdata(Convert.ToInt32(dt.Rows[i]["ID"].ToString()));
                }
            }
            catch (Exception ex)
            {   
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void GetCategory(int id)
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

        protected void btnNew_Click(object sender, EventArgs e)
       {
           try
           {
               Response.Redirect("Category.aspx");
           }
           catch (Exception ex)
           {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
           }
       }

        protected void btnAdd_Click(object sender, EventArgs e)
       {
           try
           {
               if (!String.IsNullOrWhiteSpace(hfhidid.Value))
               {
                   int ID = Int16.Parse(hfhidid.Value);
                   Response.Redirect("Category.aspx?ID=" + ID,false);
               }
               else
               {
                   GlobalUI.MessageBox("Please select the category!");
               }
           }
           catch (Exception ex)
           {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
           }
       }

        /// <summary>
        /// to get selected node ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvCategory_SelectedNodeChanged(object sender, EventArgs e)
       {
           try
           {
               cbl = new Category_BL();
               int ID;
               hfhidid.Value = tvCategory.SelectedNode.Value;
               hfhidtext.Value = tvCategory.SelectedNode.Text;
               ID = Int16.Parse(hfhidid.Value);
               gvtreeview.DataSource = cbl.SelectForTreeview(ID);
               gvtreeview.DataBind();
               #region
               //if (hfhidid.Value == "1")
               //{
               //     ID = Int16.Parse(hfhidid.Value);
               //    Response.Redirect("~/Category.aspx?ID=" + ID + "&Parent=" + 0);
               //}
               #endregion
           }
           catch (Exception ex)
           {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
           }
       }

        protected void btnupdatenode_Click(object sender, EventArgs e)
       {
           try
           {
               if (!String.IsNullOrWhiteSpace(hfhidid.Value))
               {
                   int ID = Int16.Parse(hfhidid.Value);
                   string text = hfhidtext.Value;
                   Response.Redirect("Category.aspx?ID=" + ID + "&text=" + text, false);
               }
               else 
               {
                   GlobalUI.MessageBox("Please select the category!");
               }
           }
           catch (Exception ex)
           {
               Session["Exception"] = ex.ToString();
               Response.Redirect("~/CustomErrorPage.aspx?");
           }
       }

        protected void btnImport_Click(object sender, EventArgs e)
       {
           try
           {
               if (upl1.HasFile)
               {
                   String[] validFileTypes = { "csv" };
                   if (CheckFile(validFileTypes))
                   {
                       upl1.SaveAs(Server.MapPath("~/Import_CSV/" + upl1.FileName));
                       Response.Redirect("~/WebForms/Import/ShopCategoryImport.aspx?FileName=" + upl1.FileName,false);
                    }
               }
           }
           catch (Exception ex)
           {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
           }
       }

        protected Boolean CheckFile(String[] validFileTypes)
       {
           try
           {
               string ext = System.IO.Path.GetExtension(upl1.PostedFile.FileName);
               bool isValidFile = false;
               for (int i = 0; i < validFileTypes.Length; i++)
               {
                   if (ext == "." + validFileTypes[i])
                   {
                       isValidFile = true;
                       break;
                   }
               }
               return isValidFile;
           }
           catch (Exception ex)
           {
               Session["Exception"] = ex.ToString();
               Response.Redirect("~/CustomErrorPage.aspx?");
               return false;
           }
       }

        /// <summary>
        /// Serial number update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnupdate_Click(object sender, EventArgs e)
       {
           try
           {
               cbl = new Category_BL();
               DataTable dt = new DataTable();
               dt.Columns.Add("ID", typeof(int));
               dt.Columns.Add("Category_SN", typeof(int));
               for (int i = 0; i < gvtreeview.Rows.Count; i++)
               {
                   int ID = Convert.ToInt32(gvtreeview.DataKeys[i].Values[0]);
                   TextBox txtserial = (TextBox)gvtreeview.Rows[i].Cells[1].FindControl("txtserial");
                   string serial = txtserial.Text;
                   if (!String.IsNullOrEmpty(serial))
                   {
                       dt.Rows.Add(ID, int.Parse(serial));
                   }
               }
               cbl.UpdateSerial(dt);
               GlobalUI.MessageBox("Update Successful!");
                //ParentNode();
                parendnote();
               if (!String.IsNullOrWhiteSpace(hfhidid.Value))
               {
                   int id = Int16.Parse(hfhidid.Value);
                   gvtreeview.DataSource = cbl.SelectForTreeview(id);
                   gvtreeview.DataBind();
               }
               else
               {
                   gvtreeview.DataSource = cbl.SelectForTreeview(1);
                   gvtreeview.DataBind();
               }
           }
           catch (Exception ex)
           {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
           }           
       }

        protected void btnExport_Click(object sender, EventArgs e)
       {
           try
           {
               cbl = new Category_BL();
               DataTable dt;
               DataTable dtexcsv = new DataTable(); DataTable dtcsv = new DataTable();
               dtexcsv.Columns.Add("コントロールカラム", typeof(string));
               dtexcsv.Columns.Add("カテゴリID", typeof(string));
               dtexcsv.Columns.Add("パス名", typeof(string));
               dtexcsv.Columns.Add("親カテゴリID", typeof(string));
               if (!String.IsNullOrWhiteSpace(txtdesc.Text.Trim()))
               {
                   String[] searchcatdesc = null;
                     string description =txtdesc.Text.Trim();
                   
                   string replaceWith = " ";
                   string line2 = Regex.Replace(description, @"　", replaceWith);
                   searchcatdesc = line2.Split(' ');
                   for (int y = 0; y < searchcatdesc.Count(); y++)
                   {
                       if (y == 0)
                           fdesc = searchcatdesc[y];
                       else if (y == 1)
                           sdesc = searchcatdesc[y];
                       else if (y == 2)
                           tdesc = searchcatdesc[y];
                       else if (y == 3)
                           fourdesc = searchcatdesc[y];
                       else if (y == 4)
                           fivedesc = searchcatdesc[y];
                       else
                           break;
                   }
                   DataTable dts = cbl.Search(txtcatid.Text.Trim(), fdesc.Trim(), sdesc.Trim(), tdesc.Trim(), fourdesc.Trim(), fivedesc.Trim());
                   for (int i = 0; i < dts.Rows.Count; i++)
                   {


                       int id = (int)dts.Rows[i]["ID"];
                       int parentID = (int)dts.Rows[i]["ParentID"];
                       dtcsv = cbl.EXsearchcsv(id);
                       DataTable csv = cbl.SelectForCatalogID(parentID);
                       string str = csv.Rows[0]["Category_ID"].ToString();
                       if (dtcsv != null && dtcsv.Rows.Count > 0)
                       {
                           for (int y = 0; y < dtcsv.Rows.Count; y++)
                           {
                               dtcsv.Rows[y]["親カテゴリID"] = str;
                           }
                       }
                       string[] colname = { "コントロールカラム", "カテゴリID", "パス名", "親カテゴリID" };
                       CopyColumns(dtcsv, dtexcsv, colname);
                   }
                   if (dtexcsv != null && dtexcsv.Rows.Count > 0)

                       using (StreamWriter writer = new StreamWriter(Server.MapPath(CSVpath + "Category.csv"), false, Encoding.GetEncoding(932)))
                       {

                           dtexcsv.AcceptChanges();
                           WriteDataTable(dtexcsv, writer, true);
                           lnkdownload.Text = "Category.csv";
                           GlobalUI.MessageBox("Successful Export CSV!");
                       }
                   else
                   {
                       GlobalUI.MessageBox("There is no  new data to export csv!");
                       lnkdownload.Text = String.Empty;
                   }
               }
               else
               {
                   dt = cbl.ExCSV(0, 0);
                   if (dt != null && dt.Rows.Count > 0)
                   {
                       for (int t = 0; t < dt.Rows.Count; t++)
                       {
                           parid = (int)dt.Rows[t]["ParentID"];
                           DataTable csv = cbl.SelectForCatalogID(parid);
                           if (csv != null && csv.Rows.Count > 0)
                           {
                               string str = csv.Rows[0]["Category_ID"].ToString();
                               dt.Rows[t]["親カテゴリID"] = str;
                           }
                       }

                       using (StreamWriter writer = new StreamWriter(Server.MapPath(CSVpath + "Category.csv"), false, Encoding.GetEncoding(932)))
                       {
                           dt.Columns.Remove("ParentID");
                           dt.AcceptChanges();
                           WriteDataTable(dt, writer, true);
                           lnkdownload.Text = "Category.csv";
                           GlobalUI.MessageBox("Successful Export CSV!");
                       }
                   }
                   else
                   {
                       GlobalUI.MessageBox("There is no new data to export csv!");
                       lnkdownload.Text = String.Empty;
                   }

               }
           }
           catch (Exception ex)
           {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
           }
       }

        private void CopyColumns(DataTable source, DataTable dest, params string[] columns)
       {
           try
           {
               foreach (DataRow sourcerow in source.Rows)
               {

                   DataRow destRow = dest.NewRow();
                   foreach (string colname in columns)
                       //for (int i = 0; i < columns.Length; i++)
                       //{
                       //    string colname = columns[i];
                       //    if (!String.IsNullOrWhiteSpace(colname))
                       //    {

                       destRow[colname] = sourcerow[colname];

                   //    }
                   //}
                   dest.Rows.Add(destRow);
               }
           }
           catch (Exception ex)
           {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
           }
       }

        public void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
       {
           try
           {
               if (includeHeaders)
               {
                   List<string> headerValues = new List<string>();

                   foreach (DataColumn column in sourceTable.Columns)
                   {
                       headerValues.Add(QuoteValue(column.ColumnName.ToUpper()));
                   }
                   StringBuilder builder = new StringBuilder();
                   writer.WriteLine(String.Join(",", headerValues.ToArray()));
               }

               string[] items = null;
               foreach (DataRow row in sourceTable.Rows)
               {
                   items = row.ItemArray.Select(o => QuoteValue(o.ToString())).ToArray();
                   writer.WriteLine(String.Join(",", items));
               }

               writer.Flush();
           }
           catch (Exception ex)
           {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
           }
       }

        private string QuoteValue(string value)
       {
           try
           {
               return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
           }
           catch (Exception ex)
           {
               Session["Exception"] = ex.ToString();
               Response.Redirect("~/CustomErrorPage.aspx?");
               return String.Empty;
           }
       }

        protected void Download(string filecheck)
       {
           //try
           //{
               if (File.Exists(Server.MapPath(filecheck)))
               {
                   string filename = lnkdownload.Text;
                   WebClient req = new WebClient();
                   HttpResponse response = HttpContext.Current.Response;
                   response.Clear();
                   response.ClearContent();
                   response.ClearHeaders();
                   response.Buffer = true;
                   response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                   //response.AddHeader("Content-Disposition","attachment;filename=\""+filecheck+"\"");
                   response.ContentType = "application/octet-stream";
                   byte[] data = req.DownloadData(Server.MapPath(filecheck));
                   response.BinaryWrite(data);
                   response.End();
               }
               else
               {
                   GlobalUI.MessageBox("File doesn't exist!");
               }

           }
           //catch (Exception ex) 
           //{
           //        Session["Exception"] = ex.ToString();
           //        Response.Redirect("~/CustomErrorPage.aspx?");
           //}
       //}
    
        protected void lnkdownload_Click1(object sender, EventArgs e)
       {
           //try
           //{
               Download( CSVpath+ lnkdownload.Text);
           //}
           //catch (Exception ex)
           //{
           //        Session["Exception"] = ex.ToString();
                   Response.Redirect("~/CustomErrorPage.aspx?");
           //}
       }

 
        protected void SelectChildnode(TreeNode parentNode)
       {
           try
           {
               DataTable dt = new DataTable();
               string pid = parentNode.Value.ToString();

               dt = cbl.SelectForTreeview1(Convert.ToInt32(parentNode.Value));
               if (dt.Rows.Count > 0)
               {
                   TreeNode childNode = new TreeNode();
                   childNode.Text = "";
                   childNode.Value = null;
                   childNode.SelectAction = TreeNodeSelectAction.Select;
                   parentNode.ChildNodes.Add(childNode);

               }
           }
           catch (Exception ex)
           {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
           }
        }
    

        protected void tvCategory_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            try
            {
                if (e.Node.Parent != null && !String.IsNullOrWhiteSpace(e.Node.Value.ToString()))
                {
                    string Desc = null;
                    int str = Int32.Parse(e.Node.Value);
                    e.Node.ChildNodes.Clear();
                    cbl = new Category_BL();
                     Item_Category_BL icbl = new Item_Category_BL();
                    if (!String.IsNullOrWhiteSpace(txtdesc.Text.Trim()))
                    {
                       
                      
                        int PID =0;
                        if (ViewState["IDdt"] != null)
                        {
                            Maindt = ViewState["IDdt"] as DataTable;
                            if (Maindt != null && Maindt.Rows.Count > 0)
                            {
                                for (int y = 0; y < Maindt.Rows.Count; y++)
                                {
                                    if (Convert.ToInt32(Maindt.Rows[y]["ParentID"].ToString()) == str)
                                    {
                                        PID = Convert.ToInt32(Maindt.Rows[y]["ID"].ToString());
                                        Desc = Maindt.Rows[y]["Description"].ToString();
                                        TreeNode childNode = new TreeNode();
                                        childNode.Text = Desc;
                                        childNode.Value = Convert.ToString(PID);
                                        childNode.SelectAction = TreeNodeSelectAction.Select;
                                        e.Node.ChildNodes.Add(childNode);
                                        SelectChildnode(childNode);
                                    }
                                }
                            }


                            //TreeNode childNode = new TreeNode();
                            //childNode.Text = Desc;
                            //childNode.Value = Convert.ToString(PID);
                            //childNode.SelectAction = TreeNodeSelectAction.Select;
                            //e.Node.ChildNodes.Add(childNode);
                            //SelectChildnode(childNode);

                         
                        }//viewstate(Mltiple search)
                        else
                        {
                            DataTable dts = cbl.SelectForTreeview(Convert.ToInt32(str));
                            if (dts.Rows.Count > 0)
                            {
                                int i = 0;
                                while (i < dts.Rows.Count)
                                {
                                    TreeNode childNode = new TreeNode();
                                    childNode.Text = dts.Rows[i]["Description"].ToString();
                                    childNode.Value = dts.Rows[i]["ID"].ToString();
                                    childNode.SelectAction = TreeNodeSelectAction.Select;
                                    e.Node.ChildNodes.Add(childNode);
                                    SelectChildnode(childNode);

                                    i++;
                                }
                            }
                        }
                       
                    }
                    else //not search
                    {
                        DataTable dt = cbl.SelectForTreeview(Convert.ToInt32(str));
                        if (dt.Rows.Count > 0)
                        {
                            int i = 0;
                            while (i < dt.Rows.Count)
                            {
                                TreeNode childNode = new TreeNode();
                                childNode.Text = dt.Rows[i]["Description"].ToString();
                                childNode.Value = dt.Rows[i]["ID"].ToString();
                                childNode.SelectAction = TreeNodeSelectAction.Select;
                                e.Node.ChildNodes.Add(childNode);
                                SelectChildnode(childNode);

                                i++;
                            }
                        }
                    }//else
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

    }
}