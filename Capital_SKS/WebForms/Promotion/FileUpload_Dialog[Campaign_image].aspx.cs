using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ORS_RCM_BL;
using System.Data;
using System.Drawing;

namespace ORS_RCM.WebForms.Promotion
{
    public partial class FileUpload_Dialog_Campaign_image_ : System.Web.UI.Page
    {
        DataTable dt;
        DataTable dtImage;

        Promotion_BL promotionBL;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt = Session["ImageList"] as DataTable;

              
               

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image1"].ToString()))
                        {
                            FileUpload1.Enabled = false;
                            FileUpload1.Width = 85;
                            lblFileName1.Visible = true;
                            lblFileName1.Text = dt.Rows[0]["Item_Image1"] + "";
                            Label1.Visible = false;
                            btnDelete1.Visible = true;
                        }

                    //}
                    //if (dt.Rows.Count > 1)
                    //{

                        if (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image2"].ToString()))
                        {
                            FileUpload2.Enabled = false;
                            FileUpload2.Width = 85;
                            lblFileName2.Visible = true;
                            lblFileName2.Text = dt.Rows[0]["Item_Image2"] + "";
                            Label2.Visible = false;
                            btnDelete2.Visible = true;
                        }
                    //}

                    //if (dt.Rows.Count > 2)
                    //{
                        if (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image3"].ToString()))
                        {
                            FileUpload3.Enabled = false;
                            FileUpload3.Width = 85;
                            lblFileName3.Visible = true;
                            lblFileName3.Text = dt.Rows[0]["Item_Image3"] + "";
                            Label3.Visible = false;
                            btnDelete3.Visible = true;
                        }

                    //}

                    //if (dt.Rows.Count > 3)
                    //{
                        if (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image4"].ToString()))
                        {
                            FileUpload4.Enabled = false;
                            FileUpload4.Width = 85;
                            lblFileName4.Visible = true;
                            lblFileName4.Text = dt.Rows[0]["Item_Image4"] + "";
                            Label4.Visible = false;
                            btnDelete4.Visible = true;
                        //}

                    }


                    //if (dt.Rows.Count > 4)
                    //{

                        if (!string.IsNullOrWhiteSpace(dt.Rows[0]["Item_Image5"].ToString()))
                        {
                            FileUpload5.Enabled = false;
                            FileUpload5.Width = 85;
                            lblFileName5.Visible = true;
                            lblFileName5.Text = dt.Rows[0]["Item_Image5"] + "";
                            Label5.Visible = false;
                            btnDelete5.Visible = true;
                        }

                    }
                }
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {  
            try
            {

                dt = Session["ImageList"] as DataTable;
       
                if (dt == null)
                {

                    dt = new DataTable();
                    dt.Columns.Add("Item_Image1", typeof(String));

                    dt.Columns.Add("Item_Image2", typeof(String));

                    dt.Columns.Add("Item_Image3", typeof(String));

                    dt.Columns.Add("Item_Image4", typeof(String));

                    dt.Columns.Add("Item_Image5", typeof(String));
                }
                else
                {
                    dt = Session["ImageList"] as DataTable;
                    if (dt != null)
                    {
                        RemoveNullColumnFromDataTable(dt);
                    }
                }
                if (dt.Rows.Count == 0)
                {

                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                    if (FileUpload1.HasFile)
                    {

                      
                        dr["Item_Image1"] = FileUpload1.FileName;


                            FileUpload1.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload1.FileName);
                      
                    }

                    if (FileUpload2.HasFile)
                    {
                       

                           dr["Item_Image2"] = FileUpload2.FileName;
                            //dt.Rows.Add(dr);

                            FileUpload2.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload2.FileName);
                      
                    }

                    if (FileUpload3.HasFile)
                    {
                   

                        dr["Item_Image3"] = FileUpload3.FileName;
                            //dt.Rows.Add(dr);

                            FileUpload3.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload3.FileName);
                      
                    }

                    if (FileUpload4.HasFile)
                    {
                      

                        dr["Item_Image4"] = FileUpload4.FileName;

                       

                            FileUpload4.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload4.FileName);
                       
                    }

                    if (FileUpload5.HasFile)
                    {
                       

                        dr["Item_Image5"] = FileUpload5.FileName;


                            //dt.Rows.Add(dr);

                            FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                       
                    }

                    Session["ImageList"] = dt;

                }

                else
                {

                    if (FileUpload1.HasFile)
                    {

                           if (dt.Rows.Count > 0)
                        {
                            dt.Rows[0]["Item_Image1"] = FileUpload1.FileName;


                            FileUpload1.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload1.FileName);
                        }
                    }

                    if (FileUpload2.HasFile)
                    {
                        //DataRow dr = dt.NewRow();  
                        if (dt.Rows.Count > 0)
                        {
                            dt.Rows[0]["Item_Image2"] = FileUpload2.FileName;
                            //dt.Rows.Add(dr);

                            FileUpload2.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload2.FileName);
                        }
                    }

                    if (FileUpload3.HasFile)
                    {
                        //DataRow dr = dt.NewRow();
                        if (dt.Rows.Count > 0)
                        {
                            dt.Rows[0]["Item_Image3"] = FileUpload3.FileName;
                            //dt.Rows.Add(dr);

                            FileUpload3.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload3.FileName);
                        }
                    }

                    if (FileUpload4.HasFile)
                    {
                        //DataRow dr = dt.NewRow();
                        if (dt.Rows.Count > 0)
                        {
                            dt.Rows[0]["Item_Image4"] = FileUpload4.FileName;

                            //dt.Rows.Add(dr);

                            FileUpload4.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload4.FileName);
                        }
                    }

                    if (FileUpload5.HasFile)
                    {
                        //DataRow dr = dt.NewRow();
                        if (dt.Rows.Count > 0)
                        {
                            dt.Rows[0]["Item_Image5"] = FileUpload5.FileName;


                            //dt.Rows.Add(dr);

                            FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        }
                    }
                }
                Session["ImageList"] = dt;
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack();window.close()", true);
            }

            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
      
     protected void btnDelete1_Click(object sender, EventArgs e)
        {

            try
            {
                Delete(lblFileName1.Text);
                FileUpload1.Enabled = true;
                FileUpload1.Width = 210;
                Label1.Visible = true;
                lblFileName1.Text = "";
                btnDelete1.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }


        }

     protected void btnDelete2_Click(object sender, EventArgs e)
     {
         try
         {
               Delete(lblFileName2.Text);
               FileUpload2.Enabled = true;
               FileUpload2.Width = 210;
               Label2.Visible = true;
               lblFileName2.Text = "";
               btnDelete2.Visible = false;
           }
           catch (Exception ex)
           {
               Session["Exception"] = ex.ToString();
               Response.Redirect("~/CustomErrorPage.aspx?");
           }

        }

        protected void btnDelete3_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName3.Text);
                FileUpload3.Enabled = true;
                FileUpload3.Width = 210;
                Label3.Visible = true;
                lblFileName3.Text = "";
                btnDelete3.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        protected void btnDelete4_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName4.Text);
                FileUpload4.Enabled = true;
                FileUpload4.Width = 210;
                Label4.Visible = true;
                lblFileName4.Text = "";
                btnDelete4.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        protected void btnDelete5_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName5.Text);
                FileUpload5.Enabled = true;
                FileUpload5.Width = 210;
                Label5.Visible = true;
                lblFileName5.Text = "";
                btnDelete5.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }


        public void Delete(string Item_Image)
        {
            dt = Session["ImageList"] as DataTable;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Item_Image1"].ToString() == Item_Image.ToString())
                    {
                        dr["Item_Image1"] = "";
                        dt.AcceptChanges();
                        Session["ImageList"] = dt;
                        break;
                    }
                    if (dr["Item_Image2"].ToString() == Item_Image.ToString())
                    {
                        dr["Item_Image2"] = "";
                        dt.AcceptChanges();
                        Session["ImageList"] = dt;
                        break;
                    }
                    if (dr["Item_Image3"].ToString() == Item_Image.ToString())
                    {
                        dr["Item_Image3"] = "";
                        dt.AcceptChanges();
                        Session["ImageList"] = dt;
                        break;
                    }
                    if (dr["Item_Image4"].ToString() == Item_Image.ToString())
                    {
                        dr["Item_Image4"] = "";
                        dt.AcceptChanges();
                        Session["ImageList"] = dt;
                        break;
                    }

                    if (dr["Item_Image5"].ToString() == Item_Image.ToString())
                    {
                        dr["Item_Image5"] = "";
                        dt.AcceptChanges();
                        Session["ImageList"] = dt;
                        break;
                    }
                }}
        }

       public static void RemoveNullColumnFromDataTable(DataTable dt)
        {
            //for (int i = dt.Rows.Count - 1; i >= 0; i--)
            //{
            //    DataRow dr = dt.NewRow();
            //    if (dr["Item_Image1"] == DBNull.Value)
            //        dr.Delete();
            //    if (dr["Item_Image2"] == DBNull.Value)
            //        dt.Rows[i].Delete();
            //    if (dr["Item_Image3"] == DBNull.Value)
            //        dr.Delete();
            //    if (dr["Item_Image4"] == DBNull.Value)
            //        dr.Delete();
            //    if (dr["Item_Image5"] == DBNull.Value)
            //        dr.Delete();
            //              }
            //dt.AcceptChanges();
        }

        }
    }
