/* 
Created By              : Kyaw Thet Paing
Created Date          : 20/06/2014
Updated By             :
Updated Date         :

 Tables using: 
    -
    -
    -
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;   
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Configuration;

namespace ORS_RCM
{
    public partial class FileUpload_Dialog : System.Web.UI.Page
    {
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
        DataTable dt;
        DataTable dtImage;
        string unicode = @"/[\u3000-\u303F]|[\u3040-\u309F]|[\u30A0-\u30FF]|[\uFF00-\uFFEF]|[\u4E00-\u9FAF]|[\u2605-\u2606]|[\u2190-\u2195]|\u203B/g";
        string imagePath = ConfigurationManager.AppSettings["ItemImageSave"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt = Session["ImageList_" + Item_Code] as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("Image_Type=0");
                    if (dr.Count() > 0)
                    {
                         dtImage = dt.Select("Image_Type=0").CopyToDataTable();
                         if (dtImage.Rows.Count > 0)
                         {
                             for (int m = 0; m < dtImage.Rows.Count; m++)
                             {
                                 switch (dtImage.Rows[m]["SN"].ToString())
                                 {
                                     case "1":
                                         if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                         {
                                             FileUpload1.Enabled = false;
                                             FileUpload1.Width = 85;
                                             lblFileName1.Visible = true;
                                             lblFileName1.Text = dtImage.Rows[m]["Image_Name"] + "";
                                             Label1.Visible = false;
                                             btnDelete1.Visible = true;
                                         }
                                         break;
                                     case "2":
                                         if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                         {
                                             FileUpload2.Enabled = false;
                                             FileUpload2.Width = 85;
                                             lblFileName2.Visible = true;
                                             lblFileName2.Text = dtImage.Rows[m]["Image_Name"] + "";
                                             Label2.Visible = false;
                                             btnDelete2.Visible = true;
                                         }
                                         break;
                                     case "3":
                                         if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                         {
                                             FileUpload3.Enabled = false;
                                             FileUpload3.Width = 85;
                                             lblFileName3.Visible = true;
                                             lblFileName3.Text = dtImage.Rows[m]["Image_Name"] + "";
                                             Label3.Visible = false;
                                             btnDelete3.Visible = true;
                                         }
                                         break;
                                     case "4":
                                         if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                         {
                                             FileUpload4.Enabled = false;
                                             FileUpload4.Width = 85;
                                             lblFileName4.Visible = true;
                                             lblFileName4.Text = dtImage.Rows[m]["Image_Name"] + "";
                                             Label4.Visible = false;
                                             btnDelete4.Visible = true;
                                         }
                                         break;
                                     case "5":
                                         if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                         {
                                             FileUpload5.Enabled = false;
                                             FileUpload5.Width = 85;
                                             lblFileName5.Visible = true;
                                             lblFileName5.Text = dtImage.Rows[m]["Image_Name"] + "";
                                             Label5.Visible = false;
                                             btnDelete5.Visible = true;
                                         }
                                         break;
                                    case "6":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload6.Enabled = false;
                                            FileUpload6.Width = 85;
                                            lblFileName6.Visible = true;
                                            lblFileName6.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label6.Visible = false;
                                            btnDelete6.Visible = true;
                                        }
                                        break;

                                    case "7":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload7.Enabled = false;
                                            FileUpload7.Width = 85;
                                            lblFileName7.Visible = true;
                                            lblFileName7.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label7.Visible = false;
                                            btnDelete7.Visible = true;
                                        }
                                        break;

                                    case "8":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload8.Enabled = false;
                                            FileUpload8.Width = 85;
                                            lblFileName8.Visible = true;
                                            lblFileName8.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label8.Visible = false;
                                            btnDelete8.Visible = true;
                                        }
                                        break;
                                    case "9":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload9.Enabled = false;
                                            FileUpload9.Width = 85;
                                            lblFileName9.Visible = true;
                                            lblFileName9.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label9.Visible = false;
                                            btnDelete9.Visible = true;
                                        }
                                        break;
                                    case "10":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload10.Enabled = false;
                                            FileUpload10.Width = 85;
                                            lblFileName10.Visible = true;
                                            lblFileName10.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label10.Visible = false;
                                            btnDelete10.Visible = true;
                                        }
                                        break;

                                    case "11":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload11.Enabled = false;
                                            FileUpload11.Width = 85;
                                            lblFileName11.Visible = true;
                                            lblFileName11.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label11.Visible = false;
                                            btnDelete11.Visible = true;
                                        }
                                        break;
                                    case "12":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload12.Enabled = false;
                                            FileUpload12.Width = 85;
                                            lblFileName12.Visible = true;
                                            lblFileName12.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label12.Visible = false;
                                            btnDelete12.Visible = true;
                                        }
                                        break;
                                    case "13":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload13.Enabled = false;
                                            FileUpload13.Width = 85;
                                            lblFileName13.Visible = true;
                                            lblFileName13.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label13.Visible = false;
                                            btnDelete13.Visible = true;
                                        }
                                        break;
                                    case "14":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload14.Enabled = false;
                                            FileUpload14.Width = 85;
                                            lblFileName14.Visible = true;
                                            lblFileName14.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label14.Visible = false;
                                            btnDelete14.Visible = true;
                                        }
                                        break;
                                    case "15":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload15.Enabled = false;
                                            FileUpload15.Width = 85;
                                            lblFileName15.Visible = true;
                                            lblFileName15.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label15.Visible = false;
                                            btnDelete15.Visible = true;
                                        }
                                        break;
                                    case "16":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload16.Enabled = false;
                                            FileUpload16.Width = 85;
                                            lblFileName16.Visible = true;
                                            lblFileName16.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label16.Visible = false;
                                            btnDelete16.Visible = true;
                                        }
                                        break;
                                    case "17":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload17.Enabled = false;
                                            FileUpload17.Width = 85;
                                            lblFileName17.Visible = true;
                                            lblFileName17.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label17.Visible = false;
                                            btnDelete17.Visible = true;
                                        }
                                        break;
                                    case "18":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload18.Enabled = false;
                                            FileUpload18.Width = 85;
                                            lblFileName18.Visible = true;
                                            lblFileName18.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label18.Visible = false;
                                            btnDelete18.Visible = true;
                                        }
                                        break;
                                    case "19":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload19.Enabled = false;
                                            FileUpload19.Width = 85;
                                            lblFileName19.Visible = true;
                                            lblFileName19.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label19.Visible = false;
                                            btnDelete19.Visible = true;
                                        }
                                        break;
                                    case "20":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            FileUpload20.Enabled = false;
                                            FileUpload20.Width = 85;
                                            lblFileName20.Visible = true;
                                            lblFileName20.Text = dtImage.Rows[m]["Image_Name"] + "";
                                            Label20.Visible = false;
                                            btnDelete20.Visible = true;
                                        }
                                        break;


                                }
                            }
                         }
                        /*
                        if (dtImage != null && dtImage.Rows.Count > 0)
                        {
                            if (dtImage.Rows.Count > 0 && !string.IsNullOrWhiteSpace(dtImage.Rows[0]["Image_Name"].ToString()))
                            {
                                FileUpload1.Enabled = false;
                                FileUpload1.Width = 85;
                                lblFileName1.Visible = true;
                                lblFileName1.Text = dtImage.Rows[0]["Image_Name"] + "";
                                Label1.Visible = false;
                                btnDelete1.Visible = true;
                            }
                            if (dtImage.Rows.Count > 1 && !string.IsNullOrWhiteSpace(dtImage.Rows[1]["Image_Name"].ToString()))
                            {
                                FileUpload2.Enabled = false;
                                FileUpload2.Width = 85;
                                lblFileName2.Visible = true;
                                lblFileName2.Text = dtImage.Rows[1]["Image_Name"] + "";
                                Label2.Visible = false;
                                btnDelete2.Visible = true;
                            }
                            if (dtImage.Rows.Count > 2 && !string.IsNullOrWhiteSpace(dtImage.Rows[2]["Image_Name"].ToString()))
                            {
                                FileUpload3.Enabled = false;
                                FileUpload3.Width = 85;
                                lblFileName3.Visible = true;
                                lblFileName3.Text = dtImage.Rows[2]["Image_Name"] + "";
                                Label3.Visible = false;
                                btnDelete3.Visible = true;
                            }
                            if (dtImage.Rows.Count > 3 && !string.IsNullOrWhiteSpace(dtImage.Rows[3]["Image_Name"].ToString()))
                            {
                                FileUpload4.Enabled = false;
                                FileUpload4.Width = 85;
                                lblFileName4.Visible = true;
                                lblFileName4.Text = dtImage.Rows[3]["Image_Name"] + "";
                                Label4.Visible = false;
                                btnDelete4.Visible = true;
                            }
                            if (dtImage.Rows.Count > 4 && !string.IsNullOrWhiteSpace(dtImage.Rows[4]["Image_Name"].ToString()))
                            {
                                FileUpload5.Enabled = false;
                                FileUpload5.Width = 85;
                                lblFileName5.Visible = true;
                                lblFileName5.Text = dtImage.Rows[4]["Image_Name"] + "";
                                Label5.Visible = false;
                                btnDelete5.Visible = true;
                            }
                        }
                         */
                    }
                }
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable dt = new DataTable();
                DataTable dt = Session["ImageList_" + Item_Code] as DataTable;
                if (dt == null)
                {
                    dt = new DataTable();
                    dt.Columns.Add("ID", typeof(Int16));
                    dt.Columns.Add("Image_Name", typeof(String));
                    dt.Columns.Add("Image_Type", typeof(String));
                    dt.Columns.Add("SN", typeof(Int16));
                }
                else
                {
                    dt = Session["ImageList_" + Item_Code] as DataTable;
                    if (dt != null && dt.Rows.Count>0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["Image_Name"].Equals(""))
                            {
                                dr.Delete();
                            }
                        }
                        dt.AcceptChanges();
                    }
                }
                /*
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile PostedFile = Request.Files[i];
                    if (PostedFile.ContentLength > 0)
                    {
                        DataRow dr = dt.NewRow();
                        string FileName = System.IO.Path.GetFileName(PostedFile.FileName);
                        dr["ID"] = i + 1;
                        dr["Image_Name"] = FileName;
                        if (Request.QueryString["Image_Type"].Equals("0"))
                        {
                            dr["Image_Type"] = "0";
                        }
                        else if (Request.QueryString["Image_Type"].Equals("1"))
                        {
                            dr["Image_Type"] = "1";
                        }
                        dt.Rows.Add(dr);

                        PostedFile.SaveAs(Server.MapPath("~/Item_Image/") + FileName);
                    }
                }
                */
                if (FileUpload1.HasFile)
                {

                    DataRow dr = dt.NewRow();
                    dr["ID"] = 1;
                    dr["Image_Name"] = FileUpload1.FileName;
                    lblFileName1.Text = FileUpload1.FileName;
                    dr["SN"] = 1;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }
                    //if (Regex.IsMatch(lblFileName1.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}

                    //if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName1.Text, "[0-9,a-z\\-]-[1-5].jpg"))
                    //{
                    //    lblFileName1.Text = "先に登録されていた場合";
                    //    GlobalUI.MessageBox("Invalid Image Name");
                    //    return;
                    //}
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName1.Text, Item_Code + "-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName1.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                   
                    //if (lblFileName1.Text.Length > 4)
                    //{
                    //    if (!lblFileName1.Text.ToLower().Contains(".gif") && !lblFileName1.Text.ToLower().Contains(".jpg") && !lblFileName1.Text.ToLower().Contains(".png"))
                    //    {
                    //        GlobalUI.MessageBox("Invalid extension of photo in ライブラリ画像 first field ! ");
                    //        return ;
                    //    }
                    //}
                    //else
                    //{
                    //    GlobalUI.MessageBox("Invalid name of photo in ライブラリ画像 first field ! ");
                    //    return;
                    //}

                    
                    if (lblFileName1.Text.Length > 24)
                    {
                        lblFileName1.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        FileUpload1.SaveAs(imagePath + FileUpload1.FileName);
                    }
                }

                if (FileUpload2.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 2;
                    dr["Image_Name"] = FileUpload2.FileName;
                    lblFileName2.Text = FileUpload2.FileName;
                    dr["SN"] = 2;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }


                    //if (Regex.IsMatch(lblFileName2.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName2.Text.Length > 24)
                    {
                        lblFileName2.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    //if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName2.Text, "[0-9,a-z\\-]-[1-5].jpg"))
                    //{
                    //    lblFileName2.Text = "先に登録されていた場合";
                    //    GlobalUI.MessageBox("Invalid Image Name");
                    //    return;
                    //}
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName2.Text,Item_Code + "-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName2.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        FileUpload2.SaveAs(imagePath + FileUpload2.FileName);
                    }
                }

                if (FileUpload3.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 3;
                    dr["Image_Name"] = FileUpload3.FileName;
                    lblFileName3.Text = FileUpload3.FileName;
                    dr["SN"] = 3;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName3.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName3.Text.Length > 24)
                    {
                        lblFileName3.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    //if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName3.Text, "[0-9,a-z\\-]-[1-5].jpg"))
                    //{
                    //    lblFileName3.Text = "先に登録されていた場合";
                    //    GlobalUI.MessageBox("Invalid Image Name");
                    //    return;
                    //}
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName3.Text,Item_Code + "-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName3.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        FileUpload3.SaveAs(imagePath + FileUpload3.FileName);
                    }
                }

                if (FileUpload4.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 4;
                    dr["Image_Name"] = FileUpload4.FileName;
                    lblFileName4.Text = FileUpload4.FileName;
                    dr["SN"] = 4;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName4.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName4.Text.Length > 24)
                    {
                        lblFileName4.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    //if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName4.Text, "[0-9,a-z\\-]-[1-5].jpg"))
                    //{
                    //    lblFileName4.Text = "先に登録されていた場合";
                    //    GlobalUI.MessageBox("Invalid Image Name");
                    //    return;
                    //}
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName4.Text,Item_Code + "-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName4.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        FileUpload4.SaveAs(imagePath + FileUpload4.FileName);
                    }
                }

                if (FileUpload5.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 5;
                    dr["Image_Name"] = FileUpload5.FileName;
                    lblFileName5.Text = FileUpload5.FileName;
                    dr["SN"] = 5;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName5.Text.Length > 24)
                    {
                        lblFileName5.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    //if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName5.Text, "[0-9,a-z\\-]-[1-5].jpg"))
                    //{
                    //    lblFileName5.Text = "先に登録されていた場合";
                    //    GlobalUI.MessageBox("Invalid Image Name");
                    //    return;
                    //}
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName5.Text,Item_Code + "-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName5.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        FileUpload5.SaveAs(imagePath + FileUpload5.FileName);
                    }
                }

                if (FileUpload6.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 6;
                    dr["Image_Name"] = FileUpload6.FileName;
                    lblFileName6.Text = FileUpload6.FileName;
                    dr["SN"] = 6;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName6.Text.Length > 24)
                    {
                        lblFileName6.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName6.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName6.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload6.SaveAs(imagePath + FileUpload6.FileName);
                    }
                }

                if (FileUpload7.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 7;
                    dr["Image_Name"] = FileUpload7.FileName;
                    lblFileName7.Text = FileUpload7.FileName;
                    dr["SN"] = 7;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName7.Text.Length > 24)
                    {
                        lblFileName7.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName7.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName7.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload7.SaveAs(imagePath + FileUpload7.FileName);
                    }
                }

                if (FileUpload8.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 8;
                    dr["Image_Name"] = FileUpload8.FileName;
                    lblFileName8.Text = FileUpload8.FileName;
                    dr["SN"] = 8;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName8.Text.Length > 24)
                    {
                        lblFileName8.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName8.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName8.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload8.SaveAs(imagePath + FileUpload8.FileName);
                    }
                }

                if (FileUpload9.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 9;
                    dr["Image_Name"] = FileUpload9.FileName;
                    lblFileName9.Text = FileUpload9.FileName;
                    dr["SN"] = 9;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName9.Text.Length > 24)
                    {
                        lblFileName9.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName9.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName9.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload9.SaveAs(imagePath + FileUpload9.FileName);
                    }
                }

                if (FileUpload10.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 10;
                    dr["Image_Name"] = FileUpload10.FileName;
                    lblFileName10.Text = FileUpload10.FileName;
                    dr["SN"] = 10;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName10.Text.Length > 24)
                    {
                        lblFileName10.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName10.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName10.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload10.SaveAs(imagePath + FileUpload10.FileName);
                    }
                }

                if (FileUpload11.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 11;
                    dr["Image_Name"] = FileUpload11.FileName;
                    lblFileName11.Text = FileUpload11.FileName;
                    dr["SN"] = 11;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName11.Text.Length > 24)
                    {
                        lblFileName11.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName11.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName11.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload11.SaveAs(imagePath + FileUpload11.FileName);
                    }
                }

                if (FileUpload12.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 12;
                    dr["Image_Name"] = FileUpload12.FileName;
                    lblFileName12.Text = FileUpload12.FileName;
                    dr["SN"] = 12;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName12.Text.Length > 24)
                    {
                        lblFileName12.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName12.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName12.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload12.SaveAs(imagePath + FileUpload12.FileName);
                    }
                }

                if (FileUpload13.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 13;
                    dr["Image_Name"] = FileUpload13.FileName;
                    lblFileName13.Text = FileUpload13.FileName;
                    dr["SN"] = 13;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName13.Text.Length > 24)
                    {
                        lblFileName13.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName13.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName13.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload13.SaveAs(imagePath + FileUpload13.FileName);
                    }
                }

                if (FileUpload14.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 14;
                    dr["Image_Name"] = FileUpload14.FileName;
                    lblFileName14.Text = FileUpload14.FileName;
                    dr["SN"] = 14;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName14.Text.Length > 24)
                    {
                        lblFileName14.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName14.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName14.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload14.SaveAs(imagePath + FileUpload14.FileName);
                    }
                }

                if (FileUpload15.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 15;
                    dr["Image_Name"] = FileUpload15.FileName;
                    lblFileName15.Text = FileUpload15.FileName;
                    dr["SN"] = 15;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName15.Text.Length > 24)
                    {
                        lblFileName15.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName15.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName15.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload15.SaveAs(imagePath + FileUpload15.FileName);
                    }
                }

                if (FileUpload16.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 16;
                    dr["Image_Name"] = FileUpload16.FileName;
                    lblFileName16.Text = FileUpload16.FileName;
                    dr["SN"] = 16;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName16.Text.Length > 24)
                    {
                        lblFileName16.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName16.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName16.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload16.SaveAs(imagePath + FileUpload16.FileName);
                    }
                }

                if (FileUpload17.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 17;
                    dr["Image_Name"] = FileUpload17.FileName;
                    lblFileName17.Text = FileUpload17.FileName;
                    dr["SN"] = 17;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName17.Text.Length > 24)
                    {
                        lblFileName17.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName17.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName17.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload17.SaveAs(imagePath + FileUpload17.FileName);
                    }
                }

                if (FileUpload18.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 18;
                    dr["Image_Name"] = FileUpload18.FileName;
                    lblFileName18.Text = FileUpload18.FileName;
                    dr["SN"] = 18;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName18.Text.Length > 24)
                    {
                        lblFileName18.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName18.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName18.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload18.SaveAs(imagePath + FileUpload18.FileName);
                    }
                }

                if (FileUpload19.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 19;
                    dr["Image_Name"] = FileUpload19.FileName;
                    lblFileName19.Text = FileUpload19.FileName;
                    dr["SN"] = 19;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName19.Text.Length > 24)
                    {
                        lblFileName19.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName19.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName19.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload19.SaveAs(imagePath + FileUpload19.FileName);
                    }
                }

                if (FileUpload20.HasFile)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = 20;
                    dr["Image_Name"] = FileUpload20.FileName;
                    lblFileName20.Text = FileUpload20.FileName;
                    dr["SN"] = 20;
                    if (Request.QueryString["Image_Type"].Equals("0"))
                    {
                        dr["Image_Type"] = "0";
                    }

                    //if (Regex.IsMatch(lblFileName5.Text, unicode))
                    //{
                    //    GlobalUI.MessageBox("Not allowed unicode in image name");
                    //}
                    if (lblFileName20.Text.Length > 24)
                    {
                        lblFileName20.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(lblFileName20.Text, "[0-9,a-z\\-]-[0-1]?[0-9]|20.jpg"))
                    {
                        lblFileName20.Text = "先に登録されていた場合";
                        GlobalUI.MessageBox("Invalid Image Name");
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        // FileUpload5.SaveAs(Server.MapPath("~/Item_Image/") + FileUpload5.FileName);
                        FileUpload20.SaveAs(imagePath + FileUpload20.FileName);
                    }
                }

                dt.DefaultView.Sort = "Image_Type,SN ASC";
                dt = dt.DefaultView.ToTable();

                Session["ImageList_" + Item_Code] = dt;
                //if ((lblFileName1.Text.Length > 20) || (lblFileName2.Text.Length > 20) || (lblFileName3.Text.Length > 20) || (lblFileName4.Text.Length > 20) || (lblFileName5.Text.Length > 20))
                //{
                //    GlobalUI.MessageBox("画像ファイル名は20文字までです");
                //}

                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack();window.close()", true);
             
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
    
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack();window.close()", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["Exception"] = ex.ToString();
        //        Response.Redirect("~/CustomErrorPage.aspx?");
        //    }
        //}

        protected Boolean checkExtension(String ext)
        {
            try
            {
                if (ext.Equals(".jpg") || ext.Equals(".jpeg"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
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
        protected void btnDelete6_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName6.Text);
                FileUpload6.Enabled = true;
                FileUpload6.Width = 210;
                Label6.Visible = true;
                lblFileName6.Text = "";
                btnDelete6.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete7_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName7.Text);
                FileUpload7.Enabled = true;
                FileUpload7.Width = 210;
                Label7.Visible = true;
                lblFileName7.Text = "";
                btnDelete7.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete8_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName8.Text);
                FileUpload8.Enabled = true;
                FileUpload8.Width = 210;
                Label8.Visible = true;
                lblFileName8.Text = "";
                btnDelete8.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete9_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName9.Text);
                FileUpload9.Enabled = true;
                FileUpload9.Width = 210;
                Label9.Visible = true;
                lblFileName9.Text = "";
                btnDelete9.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete10_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName10.Text);
                FileUpload10.Enabled = true;
                FileUpload10.Width = 210;
                Label10.Visible = true;
                lblFileName10.Text = "";
                btnDelete10.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete11_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName11.Text);
                FileUpload11.Enabled = true;
                FileUpload11.Width = 210;
                Label11.Visible = true;
                lblFileName11.Text = "";
                btnDelete11.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete12_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName12.Text);
                FileUpload12.Enabled = true;
                FileUpload12.Width = 210;
                Label12.Visible = true;
                lblFileName12.Text = "";
                btnDelete12.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete13_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName13.Text);
                FileUpload13.Enabled = true;
                FileUpload13.Width = 210;
                Label13.Visible = true;
                lblFileName13.Text = "";
                btnDelete13.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete14_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName14.Text);
                FileUpload14.Enabled = true;
                FileUpload14.Width = 210;
                Label14.Visible = true;
                lblFileName14.Text = "";
                btnDelete14.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete15_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName15.Text);
                FileUpload15.Enabled = true;
                FileUpload15.Width = 210;
                Label15.Visible = true;
                lblFileName15.Text = "";
                btnDelete15.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete16_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName16.Text);
                FileUpload16.Enabled = true;
                FileUpload16.Width = 210;
                Label16.Visible = true;
                lblFileName16.Text = "";
                btnDelete16.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete17_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName17.Text);
                FileUpload17.Enabled = true;
                FileUpload17.Width = 210;
                Label17.Visible = true;
                lblFileName17.Text = "";
                btnDelete17.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete18_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName18.Text);
                FileUpload18.Enabled = true;
                FileUpload18.Width = 210;
                Label18.Visible = true;
                lblFileName18.Text = "";
                btnDelete18.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete19_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName19.Text);
                FileUpload19.Enabled = true;
                FileUpload19.Width = 210;
                Label19.Visible = true;
                lblFileName19.Text = "";
                btnDelete19.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnDelete20_Click(object sender, EventArgs e)
        {
            try
            {
                Delete(lblFileName20.Text);
                FileUpload20.Enabled = true;
                FileUpload20.Width = 210;
                Label20.Visible = true;
                lblFileName20.Text = "";
                btnDelete20.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void Delete(string Image_Name)
        {
            dt = Session["ImageList_"+Item_Code] as DataTable;
            
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    if (dr["Image_Name"].ToString() == Image_Name.ToString())
                    {
                        dr.Delete();
                        //dt.Rows.Remove(dr);
                        //dr["Image_Name"] = null;
                        dt.AcceptChanges();
                        Session["ImageList_" + Item_Code] = dt;
                        break;
                    }
                }

            }
        }

    }
}