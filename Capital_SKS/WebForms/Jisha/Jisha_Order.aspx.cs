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

namespace ORS_RCM.WebForms.Jisha
{
    public partial class Jisha_Order : System.Web.UI.Page
    {
        Jisha_Order_Entity  jiOrder_ent;

        Jisha_Order_BL   jiOrder_bl;

        
        DataTable dt = new DataTable();
        Jisha_Delivery_Charge_BL jishaDeliveryChargeBL;
        
        ArrayList OrderList;

        public static int i = 0;


        public DataTable AddToCart
        {
            get
            {
                if (Session["AddToCart"] != null)
                {
                    DataTable dt = (DataTable)Session["AddToCart"];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        decimal totalQuantity = 0M;
        decimal totalAmount = 0M;


         public string PaymentMode
        {
            get
            {
                if (Request.QueryString["PaymentMode"] != null)
                    return Request.QueryString["PaymentMode"];
                return null;
            }
        }


        
        protected void Page_Load(object sender, EventArgs e)
        {
        
            if (!IsPostBack)
            {
                gvCart.DataSource = AddToCart;
                gvCart.DataBind();
                BindPrefecture();
                BindPrefecture2();

             
            }
        }

        //public Jisha_Order_Entity AddData()
        //{
        //    jiOrder_ent = new Jisha_Order_Entity();

          
        //    //if (!String.IsNullOrWhiteSpace(txtOrder_id.Text))
        //    //{
        //    //    jiOrder_ent.Order_ID = txtOrder_id.Text;

        //    //}

        //    //if (txtOrder_date.Text == "")
        //    //{

        //    //    jiOrder_ent.Order_Date = System.DateTime.Now;
        //    //}

        //    //else   if (txtOrder_date.Text != string.Empty)
        //    //{

        //    //    jiOrder_ent.Order_Date = Convert.ToDateTime(txtOrder_date.Text);
        //    //}

        //    jiOrder_ent.Bill_LastName = txtBill_LastName.Text;


        //    if (!String.IsNullOrWhiteSpace(txtBill_FirstName.Text))
        //    {
        //        jiOrder_ent.Bill_FirstName = txtBill_FirstName.Text;
        //    }

        //    if (!String.IsNullOrWhiteSpace(txtBillLastName_Kana.Text))
        //    {
        //        jiOrder_ent.Bill_LastName_Kana = txtBillLastName_Kana.Text;
        //    }

        //    if (!String.IsNullOrWhiteSpace(txtBillFirstName_Kana.Text))
        //    {
        //        jiOrder_ent.Bill_FirstName_Kana = txtBillFirstName_Kana.Text;
        //    }

        //        //if (!String.IsNullOrWhiteSpace(txtBill_MailAddress.Text))
        //        //{
        //        //    jiOrder_ent.Bill_MailAddress = txtBill_MailAddress.Text;
        //        //}

        //        //if (!String.IsNullOrWhiteSpace(txtBill_ZipCode.Text))
        //        //{
        //        //    jiOrder_ent.Bill_ZipCode = txtBill_ZipCode.Text;
        //        //}


        //    if (!String.IsNullOrWhiteSpace(txtBill_Prefecture.Text))
        //    {
        //        jiOrder_ent.Bill_Prefecture = txtBill_Prefecture.Text;
        //    }

        //    if (!String.IsNullOrWhiteSpace(txtBill_City.Text))
        //    {
        //        jiOrder_ent.Bill_City = txtBill_City.Text;
        //    }

        //    if (!String.IsNullOrWhiteSpace(txtBill_Address1.Text))
        //    {
        //        jiOrder_ent.Bill_Address1 = txtBill_Address1.Text;
        //    }

        //    if (!String.IsNullOrWhiteSpace(txtBillAddress2.Text))
        //    {
        //        jiOrder_ent.Bill_Address2 = txtBillAddress2.Text;
        //    }

        //    //if (!String.IsNullOrWhiteSpace(txtBill_Phoneno.Text))
        //    //{
        //    //    jiOrder_ent.Bill_PhoneNo = txtBill_Phoneno.Text;
        //    //}

        //    //if (!String.IsNullOrWhiteSpace(txtBill_EMGphone.Text))
        //    //{
        //    //    jiOrder_ent.Bill_Emg_PhoneNo = txtBill_EMGphone.Text;
        //    //}


        //    if (rdoDiff.Checked == true)
        //    {

        //        if (!String.IsNullOrWhiteSpace(txtShip_LastName.Text))
        //        {
        //            jiOrder_ent.Ship_LastName = txtShip_LastName.Text;
        //        }


        //        if (!String.IsNullOrWhiteSpace(txtShip_FirstName.Text))
        //        {
        //            jiOrder_ent.Ship_FirstName = txtShip_FirstName.Text;
        //        }

        //        if (!String.IsNullOrWhiteSpace(txtShip_LastName_Kana.Text))
        //        {
        //            jiOrder_ent.Ship_LastName_Kana = txtShip_LastName_Kana.Text;
        //        }


        //        if (!String.IsNullOrWhiteSpace(txtShipfirstName_Kana.Text))
        //        {
        //            jiOrder_ent.Ship_FirstName_Kana = txtShipfirstName_Kana.Text;
        //        }

        //        if (!String.IsNullOrWhiteSpace(txtShip_ZipCode.Text))
        //        {
        //            jiOrder_ent.Ship_ZipCode = txtShip_ZipCode.Text;
        //        }


        //        if (!String.IsNullOrWhiteSpace(txtShip_Prefecture.Text))
        //        {
        //            jiOrder_ent.Ship_Prefecture = txtShip_Prefecture.Text;
        //        }


        //        if (!String.IsNullOrWhiteSpace(txtShip_City.Text))
        //        {
        //            jiOrder_ent.Ship_City = txtShip_City.Text;
        //        }


        //        if (!String.IsNullOrWhiteSpace(txtShip_Address1.Text))
        //        {
        //            jiOrder_ent.Ship_Address1 = txtShip_Address1.Text;
        //        }

        //        if (!String.IsNullOrWhiteSpace(txtShip_Address2.Text))
        //        {
        //            jiOrder_ent.Ship_Address2 = txtShip_Address1.Text;
        //        }

        //        //if (!String.IsNullOrWhiteSpace(txtShip_Phone.Text))
        //        //{
        //        //    jiOrder_ent.Ship_PhoneNo = txtShip_Phone.Text;
        //        //}

        //    }

        //    else
        //    {
                
        //            jiOrder_ent.Ship_LastName = txtBill_LastName.Text;
                
        //            jiOrder_ent.Ship_FirstName = txtBill_FirstName.Text;
                
         
        //            jiOrder_ent.Ship_LastName_Kana = txtBillLastName_Kana.Text;
            


               
        //            jiOrder_ent.Ship_FirstName_Kana = txtBillFirstName_Kana.Text;
             
              
        //            jiOrder_ent.Ship_ZipCode = txtBill_ZipCode.Text;
             
        //            jiOrder_ent.Ship_Prefecture = txtBill_Prefecture.Text;
              
        //            jiOrder_ent.Ship_City = txtBill_City.Text;
              
        //            jiOrder_ent.Ship_Address1 = txtBill_Address1.Text;
            
        //            jiOrder_ent.Ship_Address2 = txtBillAddress2.Text;
              
        //            //jiOrder_ent.Ship_PhoneNo = txtBill_Phoneno.Text;
        //        }

        //         if (!String.IsNullOrWhiteSpace(txtPayment_Method.Text))
        //        {
        //            jiOrder_ent.Payment_Method = txtPayment_Method.Text;
        //        }

        //        if (!String.IsNullOrWhiteSpace(txtOrder_Comment.Text))
        //        {
        //            jiOrder_ent.Order_Comment = txtOrder_Comment.Text;
        //        }

        //        if (!String.IsNullOrWhiteSpace(txtGroupType.Text))
        //        {
        //            jiOrder_ent.Group_Type = txtGroupType.Text;
        //        }

        //        if (!String.IsNullOrWhiteSpace(txtGroup_Name.Text))
        //        {
        //            jiOrder_ent.Group_Name = txtGroup_Name.Text;
        //        }

        //        if (!String.IsNullOrWhiteSpace(txtMail_Magzaine.Text))
        //        {
        //            jiOrder_ent.MailMagazine = txtMail_Magzaine.Text;
        //        }

        //        if (!String.IsNullOrWhiteSpace(txtUse_Point.Text))
        //        {
        //            jiOrder_ent.Use_Point = Convert.ToInt32(txtUse_Point.Text.ToString());
        //        }

        //        if (!String.IsNullOrWhiteSpace(txtSub_Total.Text))
        //        {
        //            jiOrder_ent.Sub_Total = Convert.ToInt32(txtSub_Total.Text);
        //        }

        //        //if (!String.IsNullOrWhiteSpace(txtShip_Charge.Text))
        //        //{
        //        //    jiOrder_ent.Ship_ChargeID = Convert.ToInt32(txtShip_Charge.Text);
        //        //}

        //        if (!String.IsNullOrWhiteSpace(txtTax.Text))
        //        {
        //            jiOrder_ent.Tax = Convert.ToInt32(txtTax.Text);
        //        }



        //        if (!String.IsNullOrWhiteSpace(txtPay_Charge.Text))
        //        {
        //            jiOrder_ent.Delivery_ChargeID = Convert.ToInt32(txtPay_Charge.Text);
        //        }

        //        if (!String.IsNullOrWhiteSpace(txtTotal.Text))
        //        {
        //            jiOrder_ent.Total = Convert.ToInt32(txtTotal.Text);
        //        }
        //                    return jiOrder_ent;
      

        //    }        



        private void BindPrefecture()
        {

            jiOrder_bl = new Jisha_Order_BL();
            DataTable dt = jiOrder_bl.SelectDivision();
             ddlPrefecture.DataSource = dt;
             ddlPrefecture.DataValueField = "ID";
             ddlPrefecture.DataTextField = "Division";
             ddlPrefecture.DataBind();
        }


        private void BindPrefecture2()
        {

            jiOrder_bl = new Jisha_Order_BL();
            DataTable dt = jiOrder_bl.SelectDivision();
            ddl_Prefecture2.DataSource = dt;
            ddl_Prefecture2.DataValueField = "ID";
            ddl_Prefecture2.DataTextField = "Division";
            ddl_Prefecture2.DataBind();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            
            string message = "Hello! Mudassar.";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("<script type = 'text/javascript'>");

            sb.Append("window.onload=function(){");

            sb.Append("alert('");

            sb.Append(message);

            sb.Append("')};");

            sb.Append("</script>");

            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());




                    jiOrder_bl= new Jisha_Order_BL();
                    jiOrder_ent = new Jisha_Order_Entity();
                                          
                    
                     OrderList = new ArrayList();   
                             
                    OrderList.Add(txtBill_LastName.Text);//0

                    OrderList.Add(txtBill_FirstName.Text);//1

                     OrderList.Add(txtBillLastName_Kana.Text);//2

                     OrderList.Add(txtBillFirstName_Kana.Text);//3

                     OrderList.Add(txtBill_MailAddress.Text);//4

                     OrderList.Add(txtMailAddressConfirm.Text);//5

                     string PostalCode = txtBill_ZipCode1.Text + "-" + txtBill_ZipCode2.Text;
                     string[] str = PostalCode.Split('-');

                     OrderList.Add(PostalCode);//6

                     OrderList.Add(ddlPrefecture.SelectedValue);//7


                     OrderList.Add(txtBill_City.Text);//8

                     OrderList.Add(txtBill_Address1.Text);//9

                     OrderList.Add(txtBillAddress2.Text);//10

                     string Bill_phone_no = txtBillphone1.Text + "-" + txtBillphone2.Text + "-" + txtBillphone3.Text;
                       str = Bill_phone_no.Split('-');

                     OrderList.Add(Bill_phone_no);//11


                     string Bill_EMGphone = txtBill_EMGphone1.Text + "-" + txtBill_EMGphone2.Text + "-" + txtBill_EMGphone3.Text;
                     str = Bill_EMGphone.Split('-');
            

                     OrderList.Add(Bill_EMGphone);//12

                       if(rdoDiff.Checked==true)
                     {

                         string Ship_phone_no = txtShPhone1+ "-" + txtShPhone2.Text + "-" + txtShPhone3.Text;
                         str = Ship_phone_no.Split('-');
                         
                         OrderList.Add(txtShip_LastName.Text);//13
                         OrderList.Add(txtShip_FirstName.Text);//14

                         OrderList.Add(txtShip_LastName_Kana.Text);//15

                         OrderList.Add(txtShipfirstName_Kana.Text);//16

                         string Ship_PostalCode=  txtShip_ZipCode1.Text+ "-" + txtShip_ZipCode2.Text;
                         str = Ship_PostalCode.Split('-');

                         OrderList.Add(Ship_PostalCode);//17

                         OrderList.Add(ddlPrefecture.SelectedValue);//18

                         OrderList.Add(txtShip_City.Text);//19

                         OrderList.Add(txtShip_Address1.Text);//20

                         OrderList.Add(txtShip_Address2.Text);//21

                         OrderList.Add(Ship_phone_no);//22
                         
                    }

                     else if(rdoDiff.Checked==false)
                     {
                         OrderList.Add(txtBill_LastName.Text);//13

                         OrderList.Add(txtBill_FirstName.Text);//14

                         OrderList.Add(txtBillLastName_Kana.Text);//15

                         OrderList.Add(txtBillFirstName_Kana.Text);//16

                         //OrderList.Add(txtBill_MailAddress.Text);//

                         OrderList.Add(PostalCode);//17

                         OrderList.Add(ddl_Prefecture2.SelectedValue);//18

                         OrderList.Add(txtBill_City.Text);//19

                         OrderList.Add(txtBill_Address1.Text);//20

                         OrderList.Add(txtBillAddress2.Text);//21

                         OrderList.Add(Bill_phone_no);//22
                   
            
                     }

                     //OrderList.Add(txtPayment_Method.Text);//24

                     //OrderList.Add(txtOrder_Comment.Text);//25

                     //OrderList.Add(txtGroupType.Text);//26

                     //OrderList.Add(txtGroup_Name.Text);//27

                     //OrderList.Add(txtMail_Magzaine.Text);//28

                     //OrderList.Add(txtUse_Point.Text);//29

                     //OrderList.Add(txtSub_Total.Text);//30

                     //OrderList.Add(txtCod_ChargeID.Text);//23

                     //OrderList.Add(txtTax.Text);//32

                     //OrderList.Add(txtPay_Charge.Text);//33

                     //OrderList.Add(txtTotal.Text);//34                     

                     Session["Order_table"] = OrderList;
                              
                    Response.Redirect("~/WebForms/Jisha/Jisha_Payment.aspx");                    
             
                }        

        protected void rdoDiff_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDiff.Checked == true)
            {
                hideBox.Visible = true;
            }          

        }

        protected void rdoSame_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSame.Checked == true)
            {
                hideBox.Visible = false;
            }
                   

        }

        protected void gvCart_RowDataBound(object sender, GridViewRowEventArgs e)
        {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblQuantity = (Label)e.Row.FindControl("lblQuantity");
                    Label lblAmount = (Label)e.Row.FindControl("lblAmount");

                    decimal Qty = Decimal.Parse(lblQuantity.Text);
                    decimal Amt = Decimal.Parse(lblAmount.Text);

                    totalQuantity += Qty;
                    totalAmount += Amt;
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {        DataTable dt = GetChargeCondition(i, totalAmount);
                        Label lblTotalQuantity = (Label)e.Row.FindControl("lblTotalQuantity");
                        Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
                        Label lblShippingAmount = (Label)e.Row.FindControl("lblShippingAmount");

                        lblTotalQuantity.Text = totalQuantity.ToString();
                        lblTotalAmount.Text = totalAmount.ToString();

                     //   lblShippingAmount.Text = String.Format("送料: {0} 円", dt.Rows[0]["Delivery_Amount"].ToString());

                         }

                }
         
             private DataTable GetChargeCondition(int divisionID,decimal totalAmount)
        
        {
            DataTable dtDelivery = new DataTable();
            DataTable dtCod = new DataTable();
            DataTable dtTmp = new DataTable();
            dtTmp.Columns.Add("Delivery_Amount",typeof(decimal));
            dtTmp.Columns.Add("COD_Amount", typeof(decimal));

            jishaDeliveryChargeBL = new Jisha_Delivery_Charge_BL();

            decimal deliveryAmount = 0;
            decimal codAmount = 0;
           

            dtDelivery = jishaDeliveryChargeBL.SelectDeliveryChargeByDivisionID(divisionID);
            if (dtDelivery.Rows.Count > 0)
            {
                dtDelivery.DefaultView.Sort = "Priority DESC";
                dtDelivery = dtDelivery.DefaultView.ToTable();

                foreach (DataRow dr in dtDelivery.Rows)
                {
                    if (dr["Charge_Condition"].ToString() != "" && int.Parse(dr["Charge_Condition"].ToString()) <= totalAmount)
                    {
                        deliveryAmount = int.Parse(dr["Charge"].ToString());
                        break;
                    }
                }
            }

            if (PaymentMode == "CashOn")
            {
                dtCod = jishaDeliveryChargeBL.SelectCODChargeByDivisionID(divisionID);
                if (dtCod.Rows.Count > 0)
                {
                    dtCod.DefaultView.Sort = "Priority DESC";
                    dtCod = dtCod.DefaultView.ToTable();

                    foreach (DataRow dr in dtCod.Rows)
                    {
                        if (dr["Charge_Condition"].ToString() != "" && int.Parse(dr["Charge_Condition"].ToString()) <= totalAmount)
                        {
                            codAmount = int.Parse(dr["Charge"].ToString());
                            break;
                        }
                    }
                }
            }

            dtTmp.Rows.Add(deliveryAmount, codAmount);
            dtTmp.AcceptChanges();

            return dtTmp;
        }

             protected void btnback_Click(object sender, EventArgs e)
             {


                 //RequiredFieldValidator2.Enabled = false;
                 //RequiredFieldValidator3.Enabled = false;
                 //RequiredFieldValidator4.Enabled = false;
                 //RequiredFieldValidator5.Enabled = false;
                 //RequiredFieldValidator61.Enabled = false;
                 //RequiredFieldValidator7.Enabled = false;
                 //RequiredFieldValidator9.Enabled = false;
                 //RequiredFieldValidator10.Enabled = false;
                 //RequiredFieldValidator11.Enabled = false;
                 //RequiredFieldValidator12.Enabled = false;
                 //RequiredFieldValidator13.Enabled = false;
                 Response.Redirect("~/WebForms/Jisha/Jisha_Shopping_Cart.aspx");
             }

        }
    }
     
       