/* 
Created By              : Kay Thi Aung
Created Date          : 19/6/2014
Updated By             :Ei Thinzar Zaw
Updated Date         :20/10/2015

 Tables using: Category
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
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;
using System.Text.RegularExpressions;
using System.Collections;
using System.Text;

namespace ORS_RCM
{
    public partial class Category : System.Web.UI.Page
    {
        Category_BL cbl;
        String descpath = String.Empty;
        String pid = String.Empty;
        DataTable dtmain = new DataTable();
        public int index = 0;
        public String[] ids = new String[100];
        public int extract = 0;
        public String[] ex = new String[600];
        String strcheck = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //After Save Successful or Update Successful , Back to pervious page
                    #region BackPage ViewState
                    String backpage = string.Empty;
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                        backpage = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["UrlReferrer"] = backpage;
                    }
                    #endregion
                    if (Request.QueryString["ID"] != null && Request.QueryString["text"] != null)
                    {
                        txtcidadd.Visible = true;
                        cbl = new Category_BL();
                        head.InnerText = "ショップ内カテゴリ編集";
                        hidID.Value = Request.QueryString["ID"].ToString();
                        int ID = Convert.ToInt32(hidID.Value);
                        int node = Convert.ToInt32(hidID.Value);
                        DataTable dt = cbl.SelectForCatalogID(node);// to bind textbox
                        if (dt != null || dt.Rows.Count > 0)
                        {
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["ParentID"].ToString()))
                            {
                                hfparentid.Value = dt.Rows[0]["ParentID"].ToString();
                            }
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Category_SN"].ToString()))
                            {
                                hfoldserialno.Value = dt.Rows[0]["Category_SN"].ToString();
                            }
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Category_ID"].ToString()))
                            {
                                txtcidadd.Text = dt.Rows[0]["Category_ID"].ToString();
                                hfCatID.Value = txtcidadd.Text;
                            }
                            else
                            {
                                txtcidadd.Text = "";
                            }
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Description"].ToString()))
                            {
                                txtnodeadd.Text = dt.Rows[0]["Description"].ToString();
                            }
                            else { txtnodeadd.Text = ""; }
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Rakutan_DirectoryID"].ToString()))
                            {
                                txtrakuten.Text = dt.Rows[0]["Rakutan_DirectoryID"].ToString();
                            }
                            else { txtrakuten.Text = ""; }

                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Rakutan_CategoryNo"].ToString()))
                            {
                                txtRcatno.Text = dt.Rows[0]["Rakutan_CategoryNo"].ToString();
                            }
                            else { txtRcatno.Text = ""; }
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Yahoo_CategoryID"].ToString()))
                            {
                                txtyahoo.Text = dt.Rows[0]["Yahoo_CategoryID"].ToString();
                            }
                            else { txtyahoo.Text = ""; }
                            //if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Ponpare_CategoryID"].ToString()))
                            //{
                            //    txtponpare.Text = dt.Rows[0]["Ponpare_CategoryID"].ToString();
                            //}
                            //else { txtponpare.Text = ""; }
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Wowma_CategoryID"].ToString()))
                            {
                                txtwowma.Text = dt.Rows[0]["Wowma_CategoryID"].ToString();
                            }
                            else { txtwowma.Text = ""; }
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Tennis_CategoryID"].ToString()))
                            {
                                txtTennis.Text = dt.Rows[0]["Tennis_CategoryID"].ToString();
                            }
                            else { txtTennis.Text = ""; }
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Category_SN"].ToString()))
                            {
                                txtserialno.Text = dt.Rows[0]["Category_SN"].ToString();
                            }
                            else { txtserialno.Text = ""; }
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Rakutan_CategoryNo"].ToString()))
                            {
                                txtRcatno.Text = dt.Rows[0]["Rakutan_CategoryNo"].ToString();
                            }
                            else { txtRcatno.Text = ""; }
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Jisha_DirectoryID"].ToString()))
                            {
                                txtjisha.Text = dt.Rows[0]["Jisha_DirectoryID"].ToString();
                            }
                            else { txtjisha.Text = ""; }
                            if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Jisha_CategoryName"].ToString()))
                            {
                                txtJcatno.Text = dt.Rows[0]["Jisha_CategoryName"].ToString();
                            }
                            else { txtJcatno.Text = ""; }
                        }
                        btnSave.Visible = false;
                        btnupdate.Visible = false;//for popup
                    }
                    else if (Request.QueryString["ID"] != null && Request.QueryString["Parent"] != null)
                    {
                        int serialno; int newserialno;
                        cbl = new Category_BL();
                        hidID.Value = Request.QueryString["ID"].ToString();
                        int ID = Convert.ToInt32(hidID.Value);
                        DataTable dt = cbl.SelectForTreeview(ID);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                serialno = Int16.Parse(dt.Rows[j]["Category_SN"].ToString());
                                hfnewserialno.Value = Convert.ToString(serialno);
                            }
                            newserialno = Int32.Parse(hfnewserialno.Value) + 1;
                            hfnewserialno.Value = Convert.ToString(newserialno);
                        }
                        else
                        {
                            hfnewserialno.Value = "1";
                        }
                        txtserialno.Text = hfnewserialno.Value;
                    }

                    else if (Request.QueryString["ID"] != null)
                    {
                        catid.Style.Add("display", "none");
                        head.InnerText = "ショップ内カテゴリ登録項目定義";
                        cbl = new Category_BL();
                        int serialno; int newserialno;
                        hidID.Value = Request.QueryString["ID"].ToString();
                        int ID = Convert.ToInt32(hidID.Value);

                        #region for Rakutan_CategoryNo

                        DataTable dtcatsub = cbl.SelectForCatalogID(ID);
                        if (dtcatsub != null && dtcatsub.Rows.Count > 0)
                        {
                            if (!String.IsNullOrWhiteSpace(dtcatsub.Rows[0]["Rakutan_CategoryNo"].ToString()))
                            {
                                if (ID != 1)
                                {                                    
                                    txtRcatno.Text = dtcatsub.Rows[0]["Rakutan_CategoryNo"].ToString();
                                    txtRcatno.Enabled = false;
                                }
                                else
                                {
                                    txtRcatno.Text = dtcatsub.Rows[0]["Rakutan_CategoryNo"].ToString();
                                    txtRcatno.Enabled = true;

                                }

                            }
                            else
                            {
                                if (ID != 1)
                                {
                                    txtRcatno.Text = "";
                                    txtRcatno.Enabled = false;
                                }
                                else
                                {
                                    txtRcatno.Text = "";
                                    txtRcatno.Enabled = true;
                                }
                            }
                        }
                        #endregion

                        #region for serialno
                        DataTable dtserial = cbl.SelectForCatalogID(ID);
                        int id = Int16.Parse(dtserial.Rows[0]["ID"].ToString());
                        DataTable dt = cbl.SelectForTreeview(id);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                serialno = Int16.Parse(dt.Rows[j]["Category_SN"].ToString());
                                hfnewserialno.Value = Convert.ToString(serialno);
                            }
                            newserialno = Int32.Parse(hfnewserialno.Value) + 1;
                            hfnewserialno.Value = Convert.ToString(newserialno);
                        }
                        else
                        {
                            hfnewserialno.Value = "1";
                        }
                        #endregion
                        txtserialno.Text = hfnewserialno.Value;
                    }
                }

                else
                {
                    btnpopup.Visible = false;
                    Confirm();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void Confirm()
        {
            try
            {
                String str = String.Empty;
                str = CheckLength(txtnodeadd.Text, txtrakuten.Text, txtyahoo.Text, txtwowma.Text,txtTennis.Text, txtRcatno.Text, txtjisha.Text, txtJcatno.Text);
                if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than 40 bytes!"); }
                else
                {
                    lblcidadd.Visible = true;
                    lbldesc.Visible = true;
                    lblrakuten.Visible = true;
                    lblRcatno.Visible = true;
                    lblyahoo.Visible = true;
                    //lblponpare.Visible = true;
                    lblwowma.Visible = true;
                    lblTennis.Visible = true;
                    lbljisha.Visible = true;
                    lblJcatno.Visible = true;
                    txtcidadd.Visible = false;
                    txtnodeadd.Visible = false;
                    txtrakuten.Visible = false;
                    txtRcatno.Visible = false;
                    txtyahoo.Visible = false;
                    //txtponpare.Visible = false;
                    txtwowma.Visible = false;
                    txtwowma.Visible = false;
                    txtTennis.Visible = false;
                    txtserialno.Visible = false;
                    txtjisha.Visible = false;
                    txtJcatno.Visible = false;
                    lblcidadd.Text = txtcidadd.Text;
                    lbldesc.Text = txtnodeadd.Text;
                    lblrakuten.Text = txtrakuten.Text;
                    lblRcatno.Text = txtRcatno.Text;
                    lblyahoo.Text = txtyahoo.Text;
                    //lblponpare.Text = txtponpare.Text;
                    lblwowma.Text = txtwowma.Text;
                    lblTennis.Text = txtTennis.Text;
                    lbljisha.Text = txtjisha.Text;
                    lblJcatno.Text = txtJcatno.Text;
                    lblseano.Text = txtserialno.Text;
                    if (Request.QueryString["ID"] != null && Request.QueryString["text"] != null)
                    {
                        btnupdate.Visible = true;
                        btnupdate.Text = "更新";
                        head.InnerText = "ショップ内カテゴリ編集 確認";
                    }
                    else if (Request.QueryString["ID"] != null)
                    {
                        btnSave.Visible = true;
                        btnSave.Text = "登録";
                        head.InnerText = "ショップ内カテゴリ登録項目 確認";
                    }
                }//length
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

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
                tvCategory.CollapseAll();
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

        protected void btntreecontrol_Click(object sender, EventArgs e)
        {
            try
            {
                if (btntreecontrol.Text == "Expand")
                {
                    tvCategory.ExpandAll();
                    btntreecontrol.Text = "Collapse";
                }
                else
                {
                    tvCategory.CollapseAll();
                    btntreecontrol.Text = "Expand";
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                cbl = new Category_BL();
                if (String.IsNullOrWhiteSpace(hidID.Value.ToString()))
                {
                    if (!String.IsNullOrWhiteSpace(txtcidadd.Text))
                    {
                        if (cbl.Check(txtcidadd.Text.Trim()) != true)
                        {
                            cbl = new Category_BL();
                            String pardescription = txtnodeadd.Text;
                            String cid = txtcidadd.Text;
                            String RID = txtrakuten.Text;
                            String RCNo = txtRcatno.Text;
                            String Yahoo = txtyahoo.Text;
                            //String Ponpare = txtponpare.Text;
                            String Wowma = txtwowma.Text;
                            String Tennis = txtTennis.Text;
                            String JID = txtjisha.Text;
                            String JCNo = txtJcatno.Text;
                            int Serial = Convert.ToInt32(txtserialno.Text);
                            int id = 0;
                            cbl.CategoryInsert(id, pardescription, cid, RID, RCNo, Yahoo,Wowma,Tennis, JID, JCNo, Serial, null);
                            lbltreenode.Text = pardescription;
                            btntreecontrol.Text = "Expand";
                        }
                        else
                        {
                            GlobalUI.MessageBox("Record Already Exist!");
                        }
                    }
                    else { GlobalUI.MessageBox("Not Allow Empty String!"); }
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(txtcidadd.Text))
                    {
                        cbl = new Category_BL();
                        if (cbl.Check(txtcidadd.Text.Trim()) != true)
                        {
                            int childID = Convert.ToInt32(hidID.Value);
                            String Text = txtnodeadd.Text;
                            String cid = txtcidadd.Text;
                            String RID = txtrakuten.Text;
                            String RCNo = txtRcatno.Text;
                            String Yahoo = txtyahoo.Text;
                            //String Ponpare = txtponpare.Text;
                            String Wowma = txtwowma.Text;
                            String Tennis = txtTennis.Text;
                            String JID = txtjisha.Text;
                            String JCNo = txtJcatno.Text;
                            int Serial = Convert.ToInt32(txtserialno.Text);
                            cbl.CategoryUpdate(childID, Text, cid, RID, RCNo, Yahoo, Wowma,Tennis, JID, JCNo, Serial, null);
                            lbltreenode.Text = Text;
                            btntreecontrol.Text = "Expand";
                        }
                        else
                        { GlobalUI.MessageBox("Record Already Exist!"); }
                    }
                    else
                    {
                        GlobalUI.MessageBox("Not Allow Empty String!");
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                cbl = new Category_BL();
                int ID = Convert.ToInt32(hidID.Value);
                ids[index++] = ID.ToString();
                Getdata(ID);
                int i = 0;
                while (!String.IsNullOrWhiteSpace(ids[i]))
                {
                    cbl.CategoryDelete(Convert.ToInt32(ids[i++]));
                }
                ParentNode();
                lbltreenode.Text = String.Empty;
                txtnodeadd.Text = String.Empty;
                txtcidadd.Text = String.Empty;
                btntreecontrol.Text = "Expand";
                GlobalUI.MessageBox("Delete Successful!");
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void tvCategory_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                cbl = new Category_BL();
                lblparnode.Text = String.Empty;
                txtnodeadd.Text = tvCategory.SelectedNode.Text;
                hidID.Value = tvCategory.SelectedNode.Value;
                int ID = Convert.ToInt32(hidID.Value);
                ids[index++] = ID.ToString();
                GetCategory(ID);
                int i = 0;
                while (!String.IsNullOrWhiteSpace(ids[i]))
                {
                    DataTable dts = cbl.SelectForCatalogID(Convert.ToInt32(ids[i++]));
                    if (dts != null && dts.Rows.Count > 0)
                    {
                        if (dts.Rows.Count > 0)
                        {
                            ex[extract++] = dts.Rows[0]["Description"].ToString();
                        }
                    }
                }
                for (int x = extract - 1; x >= 0; x--)
                {
                    if (x >= 0)
                    {
                        lblparnode.Text += ex[x] + ",";
                    }
                }
                int node = Convert.ToInt32(tvCategory.SelectedNode.Value);
                DataTable dt = cbl.SelectForCatalogID(node);
                if (dt != null || dt.Rows.Count > 0)
                {
                    if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Category_ID"].ToString()))
                    {
                        txtcidadd.Text = dt.Rows[0]["Category_ID"].ToString();
                    }
                    else
                    {
                        txtcidadd.Text = "";
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

        protected void btnchild_Click(object sender, EventArgs e)
        {
            try
            {
                cbl = new Category_BL();
                if (!String.IsNullOrWhiteSpace(hidID.Value.ToString()))
                {
                    if (!String.IsNullOrWhiteSpace(txtcidadd.Text))
                    {
                        if (cbl.Check(txtcidadd.Text.Trim()) != true)
                        {
                            cbl = new Category_BL();
                            int childID = Convert.ToInt32(hidID.Value);
                            String desc = txtnodeadd.Text;
                            String CID = txtcidadd.Text;
                            String RID = txtrakuten.Text;
                            String RCNo = txtRcatno.Text;
                            String Yahoo = txtyahoo.Text;
                            //String Ponpare = txtponpare.Text;
                            String Wowma = txtwowma.Text;
                            String Tennis = txtTennis.Text;
                            String JID = txtjisha.Text;
                            String JCNo = txtJcatno.Text;
                            int Serial = Convert.ToInt32(txtserialno.Text);
                            cbl.CategoryInsert(childID, desc, CID, RID, RCNo, Yahoo,Wowma,Tennis, JID, JCNo, Serial, null);
                            lbltreenode.Text = desc;
                            ParentNode();
                            txtnodeadd.Text = desc;
                            txtnodeadd.Text = "";
                            btntreecontrol.Text = "Expand";
                            GlobalUI.MessageBox("Save Successful!");
                        }
                        else { GlobalUI.MessageBox("Record Already Exist!"); }
                    }
                    else
                    {
                        GlobalUI.MessageBox("Not Allow Empty String!");
                    }
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(txtcidadd.Text))
                    {
                        if (cbl.Check(txtcidadd.Text.Trim()) != true)
                        {
                            cbl = new Category_BL();
                            String pardescription = txtnodeadd.Text;
                            String CID = txtcidadd.Text;
                            int id = 0;
                            String RID = txtrakuten.Text;
                            String RCNo = txtRcatno.Text;
                            String Yahoo = txtyahoo.Text;
                            //String Ponpare = txtponpare.Text;
                            String Wowma = txtwowma.Text;
                            String Tennis =txtTennis.Text;
                            String JID = txtjisha.Text;
                            String JCNo = txtJcatno.Text;
                            int Serial = Convert.ToInt32(txtserialno.Text);
                            cbl.CategoryInsert(id, pardescription, CID, RID, RCNo, Yahoo,Wowma,Tennis, JID, JCNo, Serial, null);
                            lbltreenode.Text = pardescription;
                            ParentNode();
                            txtnodeadd.Text = pardescription;
                            txtnodeadd.Text = "";
                            btntreecontrol.Text = "Expand";
                            string result = "Save Successful";
                            if (result == "Save Successful")
                            {
                                object referrer = ViewState["UrlReferrer"];
                                string url = (string)referrer;
                                string script = "window.onload = function(){ alert('";
                                script += result;
                                script += "');";
                                script += "window.location = '";
                                script += url;
                                script += "'; }";
                                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                            }
                        }
                        else
                        {
                            GlobalUI.MessageBox("Not Allow Empty String!");
                        }
                    }
                    else
                    {
                        GlobalUI.MessageBox("Not Allow Empty String!");
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void check()
        {
            try
            {
                bool result = cbl.Check(txtcidadd.Text.Trim());
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cbl = new Category_BL();
                if (btnSave.Text.Equals("確認画面へ"))
                { btnSave.Text = "登録"; }
                else if (btnSave.Text.Equals("登録"))
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        hidID.Value = Request.QueryString["ID"].ToString();
                        DataTable dtcat = cbl.GetAutoCategoryID();
                        if (dtcat != null && dtcat.Rows.Count > 0)
                        {
                            int catno = Convert.ToInt32(dtcat.Rows[0]["Category_ID"].ToString());
                            if (catno < 10000)
                            {
                                txtcidadd.Text = "10000";
                            }
                            else
                            {
                                int autono = catno + 1;
                                txtcidadd.Text = Convert.ToString(autono);
                            }
                        }
                        if (!String.IsNullOrWhiteSpace(txtcidadd.Text))
                        {
                            if (!String.IsNullOrWhiteSpace(txtnodeadd.Text.Trim()))
                            {
                                if ((Regex.IsMatch(txtrakuten.Text.Trim(), @"^[-,0-9]+$") || String.IsNullOrWhiteSpace(txtrakuten.Text)) && (Regex.IsMatch(txtyahoo.Text.Trim(), @"^\d+$") || String.IsNullOrWhiteSpace(txtyahoo.Text))
                                   // && (Regex.IsMatch(txtponpare.Text.Trim(), @"^[-,0-9]+$") || String.IsNullOrWhiteSpace(txtponpare.Text))
                                    && (Regex.IsMatch(txtwowma.Text.Trim(), @"^[-,0-9]+$") || String.IsNullOrWhiteSpace(txtwowma.Text))
                                    && (Regex.IsMatch(txtRcatno.Text.Trim(), @"^[-,0-9]+$") || String.IsNullOrWhiteSpace(txtRcatno.Text))
                                       && (Regex.IsMatch(txtjisha.Text.Trim(), @"^[-,0-9]+$") || String.IsNullOrWhiteSpace(txtjisha.Text)) && (Regex.IsMatch(txtJcatno.Text.Trim(), @"^\d+$") || String.IsNullOrWhiteSpace(txtJcatno.Text)))
                                {
                                    if (cbl.Check(txtcidadd.Text.Trim()) != true)
                                    {
                                        int Serial;
                                        cbl = new Category_BL();
                                        int childID = Convert.ToInt32(hidID.Value);
                                        String desc = txtnodeadd.Text;
                                        String CID = txtcidadd.Text;
                                        String RID = txtrakuten.Text;
                                        String RCNo = txtRcatno.Text;
                                        String Yahoo = txtyahoo.Text;
                                       // String Ponpare = txtponpare.Text;
                                        String Wowma = txtwowma.Text;
                                        String Tennis = txtTennis.Text;
                                        String JID = txtjisha.Text;
                                        String JCNo = txtJcatno.Text;
                                        String str = String.Empty;
                                        str = CheckLength(desc, RID, Yahoo,Wowma,Tennis, RCNo, JID, JCNo);
                                        if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than 40 bytes!"); }
                                        else
                                        {
                                            if (!String.IsNullOrWhiteSpace(txtserialno.Text))
                                            {
                                                Serial = Convert.ToInt32(txtserialno.Text);
                                            }
                                            else
                                            {
                                                Serial = Convert.ToInt32(hfnewserialno.Value);//for autogenerate serial no
                                            }
                                            //updated  date 18/05/15 for Path 
                                            String path = String.Empty;
                                            if (!String.IsNullOrWhiteSpace(hidID.Value))
                                            {
                                                int ID = Convert.ToInt32(hidID.Value);
                                                DataTable dtpath = cbl.getAllParentsbyID(ID);
                                                if (dtpath != null && dtpath.Rows.Count > 0)
                                                {
                                                    for (int y = dtpath.Rows.Count - 1; y >= 0; y--)
                                                    {
                                                        if ((Convert.ToInt32(dtpath.Rows[y]["ParentID"])) != 0)
                                                        {
                                                            descpath += dtpath.Rows[y]["Description"].ToString() + "\\";
                                                            path = descpath;
                                                        }
                                                        path += desc + "\\";
                                                    }
                                                    descpath = String.Empty;

                                                    if (dtpath.Rows.Count == 1)
                                                    {
                                                        if (dtpath.Rows[0]["ParentID"].ToString() == "0")
                                                        {
                                                            path = desc + "\\";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    path = desc;
                                                }
                                            }
                                            DataTable dtPath = cbl.GetRootPath(path);//added by ETZ
                                            if (dtPath.Rows.Count == 0)
                                            {
                                                cbl.CategoryInsert(childID, desc, CID, RID, RCNo, Yahoo,Wowma,Tennis, JID, JCNo, Serial, path);
                                                //cbl.Insert_NewRakuten_Category(path);
                                                lbltreenode.Text = desc;
                                                txtnodeadd.Text = desc;
                                                string result = "Save Successful";
                                                if (result == "Save Successful")
                                                {
                                                    object referrer = ViewState["UrlReferrer"];
                                                    string url = (string)referrer;
                                                    string script = "window.onload = function(){ alert('";
                                                    script += result;
                                                    script += "');";
                                                    script += "window.location = '";
                                                    script += url;
                                                    script += "'; }";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                                                }
                                            }
                                            else
                                            {
                                                GlobalUI.MessageBox("Category Path is Already Exist!");
                                            }
                                        }//length
                                    }
                                    else { GlobalUI.MessageBox("Record Already Exist!"); }
                                }//if check number or not
                                else { GlobalUI.MessageBox("*** Please enter only numbers or empty!"); }//if check number or not
                            }
                            else { GlobalUI.MessageBox("Please enter the category name!"); }//txtnodeadd
                        }
                        else//txtcid
                        {
                            GlobalUI.MessageBox("Not Allow Empty String!");
                        }
                    }
                    else
                    {
                        //fro autogenerate category number 14/5/15
                        DataTable dtcat = cbl.GetAutoCategoryID();
                        if (dtcat != null && dtcat.Rows.Count > 0)
                        {
                            int catno = Convert.ToInt32(dtcat.Rows[0]["Category_ID"].ToString());
                            if (catno < 10000)
                            {
                                txtcidadd.Text = "10000";
                            }
                            else
                            {
                                int autono = catno + 1;
                                txtcidadd.Text = Convert.ToString(autono);
                            }
                        }
                        if (!String.IsNullOrWhiteSpace(txtcidadd.Text))
                        {
                            if (cbl.Check(txtcidadd.Text.Trim()) != true)
                            {
                                cbl = new Category_BL();
                                String pardescription = txtnodeadd.Text;
                                String CID = txtcidadd.Text;
                                String RID = txtrakuten.Text;
                                String RCNo = txtRcatno.Text;
                                String Yahoo = txtyahoo.Text;
                                //String Ponpare = txtponpare.Text;
                                String Wowma = txtwowma.Text;
                                String Tennis = txtTennis.Text;
                                String JID = txtjisha.Text;
                                String JCNo = txtJcatno.Text;
                                int Serial = Convert.ToInt32(txtserialno.Text);
                                int id = 0;
                                String str = String.Empty;
                                str = CheckLength(pardescription, RID, Yahoo,Wowma, Tennis, RCNo, JID, JCNo);
                                if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than 40 bytes!"); }
                                else
                                {
                                    //updated  date 18/05/15 for Path 
                                    String path = String.Empty;
                                    if (!String.IsNullOrWhiteSpace(hidID.Value))
                                    {
                                        int ID = Convert.ToInt32(hidID.Value);
                                        DataTable dtpath = cbl.getAllParentsbyID(ID);
                                        if (dtpath != null && dtpath.Rows.Count > 0)
                                        {
                                            for (int y = dtpath.Rows.Count - 1; y >= 0; y--)
                                            {
                                                if ((Convert.ToInt32(dtpath.Rows[y]["ParentID"])) != 0)
                                                {
                                                    descpath += dtpath.Rows[y]["Description"].ToString() + "\\";
                                                    path = descpath;
                                                }
                                            }
                                            descpath = String.Empty;

                                            if (dtpath.Rows.Count == 1)
                                            {
                                                if (dtpath.Rows[0]["ParentID"].ToString() == "0")
                                                {
                                                    path = pardescription + "\\";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            path = pardescription;
                                        }
                                    }
                                    cbl.CategoryInsert(id, pardescription, CID, RID, RCNo, Yahoo,Wowma,Tennis, JID, JCNo, Serial, path);
                                    cbl.Insert_NewRakuten_Category(path);
                                    lbltreenode.Text = pardescription;
                                    txtnodeadd.Text = pardescription;
                                    string result = "Save Successful";
                                    if (result == "Save Successful")
                                    {
                                        object referrer = ViewState["UrlReferrer"];
                                        string url = (string)referrer;
                                        string script = "window.onload = function(){ alert('";
                                        script += result;
                                        script += "');";
                                        script += "window.location = '";
                                        script += url;
                                        script += "'; }";
                                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                                    }
                                }//length
                            }
                            else
                            {
                                GlobalUI.MessageBox("Not Allow Empty String!");
                            }
                        }
                        else
                        {
                            GlobalUI.MessageBox("Not Allow Empty String!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Session["Exception"] = ex.ToString();
                    Response.Redirect("~/CustomErrorPage.aspx?");
                }
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnupdate.Text.Equals("確認画面へ"))
                { btnupdate.Text = "登録"; }
                else if (btnupdate.Text.Equals("更新"))
                {
                    if (!String.IsNullOrWhiteSpace(txtcidadd.Text))
                    {
                        if (!String.IsNullOrWhiteSpace(txtnodeadd.Text.Trim()))
                        {
                            if ((Regex.IsMatch(txtrakuten.Text.Trim(), @"^[-,0-9]+$") || String.IsNullOrWhiteSpace(txtrakuten.Text)) && (Regex.IsMatch(txtyahoo.Text.Trim(), @"^\d+$") || String.IsNullOrWhiteSpace(txtyahoo.Text))
                                  //&& (Regex.IsMatch(txtponpare.Text.Trim(), @"^[-,0-9]+$") || String.IsNullOrWhiteSpace(txtponpare.Text))
                                  && (Regex.IsMatch(txtwowma.Text.Trim(), @"^[-,0-9]+$") || String.IsNullOrWhiteSpace(txtwowma.Text))
                                  && (Regex.IsMatch(txtTennis.Text.Trim(), @"^[-,0-9]+$") || String.IsNullOrWhiteSpace(txtTennis.Text))
                                  && (Regex.IsMatch(txtRcatno.Text.Trim(), @"^[-,0-9]+$") || String.IsNullOrWhiteSpace(txtRcatno.Text))
                                         && (Regex.IsMatch(txtjisha.Text.Trim(), @"^[-,0-9]+$") || String.IsNullOrWhiteSpace(txtjisha.Text)))
                            {
                                cbl = new Category_BL();
                                if (hfCatID.Value != txtcidadd.Text.Trim())
                                {
                                    if (cbl.Check(txtcidadd.Text.Trim()) != true)
                                    {
                                        #region
                                        String dpath = String.Empty;
                                        int childID = Convert.ToInt32(hidID.Value);
                                        String Text = txtnodeadd.Text;
                                        String cid = txtcidadd.Text;
                                        String RID = txtrakuten.Text;
                                        String RCNo = txtRcatno.Text;
                                        String Yahoo = txtyahoo.Text;
                                       // String Ponpare = txtponpare.Text;
                                        String Wowma = txtwowma.Text;
                                        String Tennis = txtTennis.Text;
                                        String JID = txtjisha.Text;
                                        String JCNo = txtJcatno.Text;
                                        String str = String.Empty;
                                        str = CheckLength(Text, RID, Yahoo,Wowma,Tennis, RCNo, JID, JCNo);
                                        if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than 40 bytes!"); }
                                        else
                                        {
                                            int Serial = Convert.ToInt32(txtserialno.Text);
                                            int parid = Convert.ToInt32(hfparentid.Value);
                                            DataTable dt = cbl.SelectAllForSerial(Serial, parid);
                                            if (dt.Rows.Count > 0 && dt != null)
                                            {
                                                if (dt.Rows[0]["Category_SN"].ToString() == txtserialno.Text.Trim())
                                                {
                                                    int id = (int)dt.Rows[0]["ID"];
                                                    hfnewupdate.Value = hfoldserialno.Value;//textbox old serialno
                                                    int serialno = Convert.ToInt32(hfnewupdate.Value);
                                                    cbl.SerialNoUpdate(serialno, id);
                                                }
                                                if (dt.Rows[0]["ParentID"].ToString() == "1")
                                                {
                                                    dpath = Text + "\\";
                                                }
                                            }
                                            if (!String.IsNullOrWhiteSpace(dpath))
                                                cbl.CategoryUpdate(childID, Text, cid, RID, RCNo, Yahoo, Wowma,Tennis, JID, JCNo, Serial, dpath);
                                            else
                                                cbl.CategoryUpdate(childID, Text, cid, RID, RCNo, Yahoo, Wowma,Tennis, JID, JCNo, Serial, null);
                                            lbltreenode.Text = Text;
                                            String path = String.Empty;
                                            DataTable dtp2desc = new DataTable();
                                            DataTable dtmerge = new DataTable();
                                            Pathupdate(childID);
                                            string result = "Update Successful";
                                            if (result == "Update Successful")
                                            {
                                                object referrer = ViewState["UrlReferrer"];
                                                string url = (string)referrer;
                                                string script = "window.onload = function(){ alert('";
                                                script += result;
                                                script += "');";
                                                script += "window.location = '";
                                                script += url;
                                                script += "'; }";
                                                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                                            }
                                        }//length
                                    }
                                    else
                                    { GlobalUI.MessageBox("Record Already Exist!"); }
                                    #endregion
                                }
                                else
                                {
                                    String dpath = String.Empty;
                                    int childID = Convert.ToInt32(hidID.Value);
                                    String Text = txtnodeadd.Text;
                                    String cid = txtcidadd.Text;
                                    String RID = txtrakuten.Text;
                                    String RCNo = txtRcatno.Text;
                                    String Yahoo = txtyahoo.Text;
                                   // String Ponpare = txtponpare.Text;
                                    String Wowma = txtwowma.Text;
                                    String Tennis = txtTennis.Text;
                                    String JID = txtjisha.Text;
                                    String JCNo = txtJcatno.Text;
                                    String str = String.Empty;
                                    str = CheckLength(Text, RID, Yahoo, Wowma,Tennis, RCNo, JID, JCNo);
                                    if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than 40 bytes!"); }
                                    else
                                    {
                                        int Serial = 0;
                                        if (!String.IsNullOrWhiteSpace(txtserialno.Text))
                                            Serial = Convert.ToInt32(txtserialno.Text);
                                        int parid = Convert.ToInt32(hfparentid.Value);
                                        DataTable dt = cbl.SelectAllForSerial(Serial, parid);
                                        if (dt.Rows.Count > 0 && dt != null)
                                        {
                                            if (dt.Rows[0]["Category_SN"].ToString() == txtserialno.Text.Trim())
                                            {
                                                int id = (int)dt.Rows[0]["ID"];
                                                hfnewupdate.Value = hfoldserialno.Value;//textbox old serialno
                                                int serialno = Convert.ToInt32(hfnewupdate.Value);
                                                cbl.SerialNoUpdate(serialno, id);
                                            }
                                            if (dt.Rows[0]["ParentID"].ToString() == "1")
                                            {
                                                dpath = Text + "\\";
                                            }
                                        }
                                        if (!String.IsNullOrWhiteSpace(dpath))
                                            cbl.CategoryUpdate(childID, Text, cid, RID, RCNo, Yahoo,Wowma, Tennis, JID, JCNo, Serial, dpath);
                                        else
                                            cbl.CategoryUpdate(childID, Text, cid, RID, RCNo, Yahoo, Wowma, Tennis, JID, JCNo, Serial, null);
                                        lbltreenode.Text = Text;
                                        String path = String.Empty;
                                        DataTable dtp2desc = new DataTable();
                                        DataTable dtmerge = new DataTable();
                                        Pathupdate(childID);
                                        string result = "Update Successful";
                                        if (result == "Update Successful")
                                        {
                                            object referrer = ViewState["UrlReferrer"];
                                            string url = (string)referrer;
                                            string script = "window.onload = function(){ alert('";
                                            script += result;
                                            script += "');";
                                            script += "window.location = '";
                                            script += url;
                                            script += "'; }";
                                            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                                        }
                                    }//length
                                }
                            }//if check number or not
                            else { GlobalUI.MessageBox("*** Please enter only numbers or empty!"); }//if check number or not
                        }//check txtcnodeadd
                        else { GlobalUI.MessageBox("Please enter the category name!"); }//check txtcnodeadd
                    }//check txtcid
                    else
                    {
                        GlobalUI.MessageBox("Not Allow Empty String!");
                    }//check txtcid
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

        protected void Getdatas(DataTable dtdata)
        {
            try
            {
                if (dtmain.Columns.Contains("ID") && dtmain.Columns.Contains("ParentID") && dtmain.Columns.Contains("Description")) { }
                else
                {
                    dtmain.Columns.Add("ID", typeof(int));
                    dtmain.Columns.Add("ParentID", typeof(int));
                    dtmain.Columns.Add("Description", typeof(String));
                }
                cbl = new Category_BL();
                DataTable dts = new DataTable();
                pid = null;
                for (int x = 0; x < dtdata.Rows.Count; x++)
                {
                    if (dtdata.Rows[x]["ID"].ToString() != "1")
                        pid += dtdata.Rows[x]["ID"].ToString() + ",";
                }
                if (pid.ToString().Equals(strcheck)) { return; }
                else
                {
                    dts = cbl.getAllPID(pid);
                    strcheck = null;
                    strcheck = pid;
                    dts = RemoveDuplicateRows(dts, "ID");
                    if (dts != null && dts.Rows.Count > 0)
                    {
                        dtmain.Merge(dts);
                        dtmain = RemoveDuplicateRows(dtmain, "ID");
                        Getdatas(dts);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void Pathupdate(int ID)
        {
            cbl = new Category_BL();
            DataTable dtp2desc = new DataTable();
            DataTable dtmerge = new DataTable();
            DataTable dt = cbl.getAllPID(Convert.ToString(ID));
            if (dt != null && dt.Rows.Count > 0)
            {
                Getdatas(dt);
                if (dtmain != null && dtmain.Rows.Count > 0)
                {
                    dtmain = RemoveDuplicateRows(dtmain, "ID");
                    for (int y = 0; y < dtmain.Rows.Count; y++)
                    {
                        int id = Convert.ToInt32(dtmain.Rows[y]["ID"].ToString());
                        if (id != 1)
                        {
                            DataTable dtpdesc = cbl.SelectForTreeview(id);
                            if (dtpdesc != null && dtpdesc.Rows.Count > 0)
                            {
                                for (int z = 0; z < dtpdesc.Rows.Count; z++)
                                {
                                    int IDss = Convert.ToInt32(dtpdesc.Rows[z]["ID"].ToString());
                                    DataTable dtpath = cbl.getAllParentsbyID(IDss);
                                    if (dtpath != null && dtpath.Rows.Count > 0)
                                    {
                                        for (int d = dtpath.Rows.Count - 1; d >= 0; d--)
                                        {
                                            if ((Convert.ToInt32(dtpath.Rows[d]["ParentID"])) != 0)
                                            {
                                                descpath += dtpath.Rows[d]["Description"].ToString() + "\\";
                                                dtpdesc.Rows[z]["Path"] = descpath;
                                            }
                                        }
                                        descpath = String.Empty;
                                    }//if
                                    dtpdesc.AcceptChanges();
                                    dtp2desc = cbl.SelectForTreeview(ID);
                                    if (dtp2desc != null && dtp2desc.Rows.Count > 0)
                                    {
                                        for (int h = 0; h < dtp2desc.Rows.Count; h++)
                                        {
                                            int IDs = Convert.ToInt32(dtp2desc.Rows[h]["ID"].ToString());
                                            DataTable dtpaths = cbl.getAllParentsbyID(IDs);
                                            if (dtpaths != null && dtpaths.Rows.Count > 0)
                                            {
                                                for (int t = dtpaths.Rows.Count - 1; t >= 0; t--)
                                                {
                                                    if ((Convert.ToInt32(dtpaths.Rows[t]["ParentID"])) != 0)
                                                    {
                                                        descpath += dtpaths.Rows[t]["Description"].ToString() + "\\";
                                                        dtp2desc.Rows[h]["Path"] = descpath;
                                                    }
                                                }
                                                descpath = String.Empty;
                                            }
                                            dtp2desc.AcceptChanges();
                                        }//if
                                        dtmerge.Merge(dtp2desc);
                                        dtp2desc = null;
                                    }
                                }//   
                                dtpdesc.Merge(dtmerge);
                                cbl.Pathupdate(dtpdesc);
                            }//
                        }//if condition
                    }
                }//check dtmain
            }//if
            else
            {
                DataTable dtpathtest = cbl.SelectForCatalogID(ID);
                DataTable dtmdes = new DataTable();
                if (dtpathtest != null && dtpathtest.Rows.Count > 0)
                {
                    for (int h = 0; h < dtpathtest.Rows.Count; h++)
                    {
                        int IDs = Convert.ToInt32(dtpathtest.Rows[h]["ID"].ToString());
                        DataTable dtpathds = cbl.getAllParentsbyID(IDs);
                        if (dtpathds != null && dtpathds.Rows.Count > 0)
                        {
                            for (int t = dtpathds.Rows.Count - 1; t >= 0; t--)
                            {
                                if ((Convert.ToInt32(dtpathds.Rows[t]["ParentID"])) != 0)
                                {
                                    descpath += dtpathds.Rows[t]["Description"].ToString() + "\\";
                                    dtpathtest.Rows[h]["Path"] = descpath;
                                }
                            }
                            descpath = String.Empty;
                        }
                        dtpathtest.AcceptChanges();
                    }//if
                    dtmdes.Merge(dtpathtest);
                    dtpathtest = null;
                }
                cbl.Pathupdate(dtmdes);
            }
            dtmain = null;
        }

        protected string CheckLength(string cname, string RID, string YID, string WID, string TID, string Rsetno, string JID, string Jsetno)
        {
            try
            {
                string msg = string.Empty; int byteLength = 0;
                Encoding enc = Encoding.GetEncoding(932);
                byteLength = enc.GetByteCount(cname);
                if (byteLength > 40)
                {
                    msg += "カテゴリ名" + ",";
                }
                byteLength = enc.GetByteCount(RID);
                if (byteLength > 40)
                {
                    msg += "楽天ディレクトリID" + ",";
                }
                byteLength = enc.GetByteCount(YID);
                if (byteLength > 40)
                {
                    msg += "ヤフーカテゴリID " + ",";
                }
                //byteLength = enc.GetByteCount(PID);
                //if (byteLength > 40)
                //{
                //    msg += "ポンパレカテゴリID" + ",";
                //}
                if (byteLength > 40)
                {
                    msg += "WowmaカテゴリID" + ",";
                }
                if (byteLength > 40)
                {
                    msg += "TennisClassicカテゴリID" + ",";
                }
                byteLength = enc.GetByteCount(Rsetno);
                if (byteLength > 40)
                {
                    msg += "楽天カテゴリセット番号" + ",";
                }
                byteLength = enc.GetByteCount(JID);
                if (byteLength > 40)
                {
                    msg += "ディレクトリID" + ",";
                }
                byteLength = enc.GetByteCount(Jsetno);
                if (byteLength > 40)
                {
                    msg += "カテゴリセット番号" + ",";
                }
                return msg;
            }
            catch (Exception ex)
            {
                string str = string.Empty;
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return str;
            }
        }
    }
}