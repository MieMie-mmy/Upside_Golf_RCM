/* 
Created By              : Kay Thi Aung
Created Date          : 27/06/2014
Updated By             :
Updated Date         :

 Tables using:  Mall_Setting_Ponpare_Fixed,Shop,Code_Setup
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
using ORS_RCM_Common;
using ORS_RCM_BL;

namespace ORS_RCM
{
    public partial class Mall_Setting_Ponpare_Fixed : System.Web.UI.Page
    {
        Mall_Setting_Ponpare_Fixed_BL pbl;
        Mall_Setting_Ponpare_Fixed_Entity pentity;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                if (Request.QueryString["Shop_ID"] != null)
                {

                    pbl = new Mall_Setting_Ponpare_Fixed_BL();

                    int id = int.Parse(Request.QueryString["Shop_ID"].ToString());

                    pentity = pbl.SelectByID(id);
                    GetData(pentity);
                }
                else
                {
                    SetData();

                }
            }

            else 
            {
                btnpopup.Visible = false;
                btnsave.Visible = true;
                Confirm();
            
            }
            

        }
        protected void Confirm()
        {
            if (Request.QueryString["Shop_ID"] != null)
            {
                btnsave.Text = "更新";
            }
            else
            {
                btnsave.Text = "登録";
            }


            lblshopname.Visible = true;
            lblmall.Visible= true;
            rdoconsumptiontax1.Visible = false;
            rdoconsumptiontax2.Visible = false;
            rdoexpancope1.Visible = false;
            rdoexpancope2.Visible = false;

            rdoinquery1.Visible = false;
            rdoinquery2.Visible = false;

            rdoorder1.Visible = false;
            rdoorder2.Visible = false;
            rdostockdisplay.Visible = false;

            rdostocktype1.Visible = false;
            rdostocktype2.Visible = false;
            rdostocktype3.Visible = false;


            txtshipg1.Visible = false;
            txtshipg2.Visible = false;
            txtstockquantity.Visible = false;
            txtvitemname.Visible = false;
            txthitemname.Visible = false;
            txtjancode.Visible = false;
            txtnoaccept.Visible = false;
            txtremainstock.Visible = false;


            lblcontax.Visible = true;
            lblexpcope.Visible = true;
            lblinquery.Visible = true;
            lbljancode.Visible = true;
            lblmall.Visible = true;
            lblnoacc.Visible = true;
            lblorder.Visible = true;
            lblrestock.Visible = true;
            lblship1.Visible = true;
            lblship2.Visible = true;
            lblstdisplay.Visible = true;
            lblstockqty.Visible = true;
            lblviname.Visible = true;
            lbthname.Visible = true;
            lblstotype.Visible = true;

            lblship1.Text = txtshipg1.Text;
            lblship2.Text = txtshipg2.Text;
            lblnoacc.Text = txtnoaccept.Text;
            lblstockqty.Text = txtstockquantity.Text;
            lbthname.Text = txthitemname.Text;
            lblviname.Text = txtvitemname.Text;
            lblrestock.Text = txtremainstock.Text;
            lbljancode.Text = txtjancode.Text;

            if (rdoconsumptiontax1.Checked)
                lblcontax.Text = "消費税込み";
            else if (rdoconsumptiontax2.Checked)
                lblcontax.Text = "消費税別";

            if (rdoexpancope1.Checked)
                lblexpcope.Text = "対応しない ";
            else if (rdoexpancope2.Checked)
                lblexpcope.Text = "対応する";

            if (rdoorder1.Checked)
                lblorder.Text = "注文ボタンをつけない";
            else if (rdoorder2.Checked)
                lblorder.Text = "注文ボタンをつける";

             if (rdoinquery1.Checked)
                 lblinquery.Text = "お問合せリンクをつけない";
             else if (rdoinquery2.Checked)
                 lblinquery.Text = "お問合せリンクをつける";

             //int type =Int32.Parse( rdostocktype1.SelectedValue.ToString());
             //switch (type) 
             //{
             //    case 1:
             //        lblstotype.Text = "通常在庫 ";break;
             //    case 2:
             //        lblstotype.Text = "SKU在庫 "; break;
             //    case 3:
             //        lblstotype.Text = "設定なし"; break;

             //}


             if (rdostocktype1.Checked)
             {
          
                  lblstotype.Text = "通常在庫 ";

             }

             else if (rdostocktype2.Checked)
             {
                 lblstotype.Text = "SKU在庫";
             }
             else
             {
                 lblstotype.Text = "設定なし";

             }
             int display = Int32.Parse(rdostockdisplay.SelectedValue.ToString());
             switch (display) 
             {
                 case 1:
                     lblstdisplay.Text = "在庫数を表示";break;
                 case 2:
                     lblstdisplay.Text = "在庫数が●個以下になったら「△」の印を表示 "; break;
                 case 3:
                     lblstdisplay.Text = "在庫数を表示しない"; break;
                 case 4:
                     lblstdisplay.Text = "在庫設定しない"; break;
             
             }
        }
        protected void SetData() 
        {
            pentity = new Mall_Setting_Ponpare_Fixed_Entity();
            if (Request.QueryString["Shop_ID"] != null) 
            {
                pentity.Shop_ID = int.Parse(Request.QueryString["Shop_ID"].ToString());
            
            }


            if (rdoconsumptiontax1.Checked)
            {
                pentity.Comtax = 2;

            }
            else 
            {
                pentity.Comtax = 1;

            }
                 

        
            pentity.Shipg1 = txtshipg1.Text;
            pentity.Shipg2 = txtshipg2.Text;


            if (rdoexpancope1.Checked)
            {
                //pentity.Expandcope = rdoexpancope.SelectedIndex;
                pentity.Expandcope = 0;

            }

            else if (rdoexpancope2.Checked)
            {
                pentity.Expandcope = 1;

            }


            if (rdoinquery1.Checked)
               {
          
                pentity.Inqbut = 0;

               }

               else if(rdoinquery2.Checked)

               {

                   pentity.Inqbut = 1;

               }


                if (rdoorder1.Checked)
                {
                    pentity.Orderbut = 0;
                }

              else if(rdoorder2.Checked)
                {

                    pentity.Orderbut = 1;
                 }

                if (!String.IsNullOrWhiteSpace(txtnoaccept.Text))
                {
                    pentity.NoAccept = txtnoaccept.Text;

                }
                else { pentity.NoAccept = null; }
          

               if(rdostocktype1.Checked)
               {

                pentity.Stocktype = 1;
               }

             else if(rdostocktype2.Checked)
               {
                   pentity.Stocktype = 2;
               }

               else if (rdostocktype3.Checked)
               { pentity.Stocktype = 3;}

                pentity.Stockdisplay = rdostockdisplay.SelectedIndex;

                if (!String.IsNullOrWhiteSpace(txtstockquantity.Text))
                {
                    pentity.Stockquantity = txtstockquantity.Text;
                }
          
               else { pentity.Stockquantity = null; }


            pentity.Hitemname = txthitemname.Text;
            pentity.Vitemname = txtvitemname.Text;
            pentity.Remaining = txtremainstock.Text;
            pentity.Jan = txtjancode.Text;
            
        }


        protected void GetData(Mall_Setting_Ponpare_Fixed_Entity pentity)
        {
            //rdoconsumptiontax.SelectedIndex = pentity.Comtax;

            if (Request.QueryString["Shop_ID"] != null)
            {


                if (pentity.Comtax == 2)
                {

                    rdoconsumptiontax1.Checked = true;

                }

                else if (pentity.Comtax == 1)
                {

                    rdoconsumptiontax2.Checked = true;
                    //rdoconsumptiontax1.Checked=false;
                }



                txtshipg1.Text = pentity.Shipg1;
                txtshipg2.Text = pentity.Shipg2;

                if (pentity.Expandcope == 0)
                {

                    rdoexpancope1.Checked = true;
                }

                else
                {

                    rdoexpancope2.Checked = true;
                    rdoexpancope1.Checked = false;

                }


                if (pentity.Inqbut == 0)
                {

                    rdoinquery1.Checked = true;
                }

                else if (pentity.Inqbut == 1)
                {
                    rdoinquery2.Checked = true;
                    rdoinquery1.Checked = false;

                }


                if (pentity.Orderbut == 0)
                {
                    rdoorder1.Checked = true;

                }

                else
                {
                    rdoorder2.Checked = true;

                    rdoorder1.Checked = false;

                }


                txtnoaccept.Text = Convert.ToString(pentity.NoAccept);

                if (pentity.Stocktype == 1)
                {

                    rdostocktype1.Checked = true;

                }

                else if (pentity.Stocktype == 2)
                {

                    rdostocktype2.Checked = true;
                    rdostocktype1.Checked = false;
                    rdostocktype3.Checked = false;

                }

                else if (pentity.Stocktype == 3)
                {

                    rdostocktype3.Checked = true;
                    rdostocktype2.Checked = false;
                    rdostocktype1.Checked = false;
                }
                rdostockdisplay.SelectedIndex = pentity.Stockdisplay;
                txtstockquantity.Text = Convert.ToString(pentity.Stockquantity);
                txthitemname.Text = pentity.Hitemname;
                txtvitemname.Text = pentity.Vitemname;
                txtremainstock.Text = pentity.Remaining;
                txtjancode.Text = pentity.Jan;

                lblmall.Text = pentity.MallName;
                lblshopname.Text = pentity.ShopName;


            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            //if (btnsave.Text.Equals("確認画面へ"))
            //{

            //    btnsave.Text = "登録";

            //}
            //else if (btnsave.Text.Equals("登録"))
            //{
                Save();
          //  }    
        }

        protected void Save() 
        {
            pbl = new Mall_Setting_Ponpare_Fixed_BL();
            if (Request.QueryString["Shop_ID"] != null)
            {
                String result = null;


                SetData();
                pentity.ID = int.Parse(Request.QueryString["Shop_ID"].ToString());


                result = pbl.Update(pentity);
                if (result == "Update Successful !")
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
                else { GlobalUI.MessageBox("Update Fail!"); }
            }





            else
            {
                SetData();
                String result = pbl.Insert(pentity);
                if (result == "Save Successful !")
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
                else { GlobalUI.MessageBox("Save Fail!"); }
            }
        
        }
    }
}