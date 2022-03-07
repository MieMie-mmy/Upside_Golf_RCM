using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;

namespace ORS_RCM.WebForms.Item
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ImageDialog : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        int id = Convert.ToInt32(Request.QueryString["ID"].ToString());
                        Item_Image_BL imbl = new Item_Image_BL();
                        DataTable dt = imbl.SelectItemPhotoByItemID(id, 0);

                        if (dt.Rows.Count > 0)
                        {
                            if (!dt.Rows[0]["Image_Name"].ToString().Equals(""))
                            {
                                upl1.Enabled = false;
                                upl1.Width = 85;
                                lblImage1.Visible = true;
                                lblImage1.Text = dt.Rows[0]["Image_Name"].ToString();
                                btnDelete1.Visible = true;
                            }
                        }

                        if (dt.Rows.Count > 1)
                        {
                            if (!dt.Rows[1]["Image_Name"].ToString().Equals(""))
                            {
                                upl2.Enabled = false;
                                upl2.Width = 85;
                                lblImage2.Visible = true;
                                lblImage2.Text = dt.Rows[1]["Image_Name"].ToString();
                                btnDelete2.Visible = true;
                            }
                        }

                        if (dt.Rows.Count > 2)
                        {
                            if (!dt.Rows[2]["Image_Name"].ToString().Equals(""))
                            {
                                upl3.Enabled = false;
                                upl3.Width = 85;
                                lblImage3.Visible = true;
                                lblImage3.Text = dt.Rows[2]["Image_Name"].ToString();
                                btnDelete3.Visible = true;
                            }
                        }

                        if (dt.Rows.Count > 3)
                        {
                            if (!dt.Rows[3]["Image_Name"].ToString().Equals(""))
                            {
                                upl4.Enabled = false;
                                upl4.Width = 85;
                                lblImage4.Visible = true;
                                lblImage4.Text = dt.Rows[3]["Image_Name"].ToString();
                                btnDelete4.Visible = true;
                            }
                        }

                        if (dt.Rows.Count > 4)
                        {
                            if (!dt.Rows[4]["Image_Name"].ToString().Equals(""))
                            {
                                upl5.Enabled = false;
                                upl5.Width = 85;
                                lblImage5.Visible = true;
                                lblImage5.Text = dt.Rows[4]["Image_Name"].ToString();
                                btnDelete5.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Item_Master_BL imbl = new Item_Master_BL();
                Item_Image_BL imageBL = new Item_Image_BL();
                if (Request.QueryString["ID"] != null)
                {
                    String id = Request.QueryString["ID"].ToString();

                    imageBL.DeletebyItemID(id, 0);

                    String FileName;
                    if (upl1.HasFile)
                    {
                        FileName = upl1.FileName;
                        upl1.SaveAs(Server.MapPath("~/Item_Image/") + FileName);
                    }
                    else if (lblImage1.Visible)
                    {
                        FileName = lblImage1.Text;
                        upl1.SaveAs(Server.MapPath("~/Item_Image/") + FileName);
                    }
                    else FileName = "";
                    imbl.ItemImageInsert(id, FileName);
                    

                    if (upl2.HasFile)
                    {
                        FileName = upl2.FileName;
                        upl2.SaveAs(Server.MapPath("~/Item_Image/") + FileName);
                    }
                    else if (lblImage2.Visible)
                    {
                        FileName = lblImage2.Text;
                        upl2.SaveAs(Server.MapPath("~/Item_Image/") + FileName);
                    }
                    else FileName = "";
                    imbl.ItemImageInsert(id, FileName);

                    if (upl3.HasFile)
                    {
                        FileName = upl3.FileName;
                        upl3.SaveAs(Server.MapPath("~/Item_Image/") + FileName);
                    }
                    else if (lblImage3.Visible)
                    {
                        FileName = lblImage3.Text;
                        upl3.SaveAs(Server.MapPath("~/Item_Image/") + FileName);
                    }
                    else FileName = "";
                    imbl.ItemImageInsert(id, FileName);

                    if (upl4.HasFile)
                    {
                        FileName = upl4.FileName;
                        upl4.SaveAs(Server.MapPath("~/Item_Image/") + FileName);
                    }
                    else if (lblImage4.Visible)
                    {
                        FileName = lblImage4.Text;
                        upl4.SaveAs(Server.MapPath("~/Item_Image/") + FileName);
                    }
                    else FileName = "";
                    imbl.ItemImageInsert(id, FileName);

                    if (upl5.HasFile)
                    {
                        FileName = upl5.FileName;
                        upl5.SaveAs(Server.MapPath("~/Item_Image/") + FileName);
                    }
                    else if (lblImage5.Visible)
                    {
                        FileName = lblImage5.Text;
                        upl5.SaveAs(Server.MapPath("~/Item_Image/") + FileName);
                    }
                    else FileName = "";
                    imbl.ItemImageInsert(id, FileName);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CloseWindow()", true);
                }  
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
                upl1.Enabled = true;
                upl1.Width = 188;
                lblImage1.Visible = false;
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
                upl2.Enabled = true;
                upl2.Width = 188;
                lblImage2.Visible = false;
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
                upl3.Enabled = true;
                upl3.Width = 188;
                lblImage3.Visible = false;
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
                upl4.Enabled = true;
                upl4.Width = 188;
                lblImage4.Visible = false;
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
                upl5.Enabled = true;
                upl5.Width = 188;
                lblImage5.Visible = false;
                btnDelete5.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}