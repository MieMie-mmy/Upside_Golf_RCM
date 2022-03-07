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
    public partial class Order_Completed_Form : System.Web.UI.Page
    {
        ArrayList Order_table;
        Jisha_Order_Entity jiOrder_ent;
        Jisha_Order_BL jiOrder_bl;
        DataTable dt = new DataTable();
        Jisha_Delivery_Charge_BL jishaDeliveryChargeBL;
        Jisha_Tax_Setting_BL jishaTaxSettingBL;

        decimal finalAmount = 0;

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
        public string PaymentMode
        {
            get
            {
                if (Request.QueryString["PaymentMode"] != null)
                    return Request.QueryString["PaymentMode"];
                return null;
            }
        }
        decimal totalQuantity = 0M;
        decimal totalAmount = 0M;

        int i = 0;
        string v;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                //v = Request.QueryString["SelectedValue"];


                gvCart.DataSource = AddToCart;
                gvCart.DataBind();
                BindArrayList();
                GetDivision_data();
            }
        }

        private void GetDivision_data()
        {
            DataTable dt = new DataTable();

            Jisha_Order_BL jiOrder_bl = new Jisha_Order_BL();
            if (Session["Order_table"] != null)
            {
                Order_table = (ArrayList)Session["Order_table"];
                int i = int.Parse((Order_table[7]).ToString());
                dt = jiOrder_bl.Selected_Division(i);
                {

                    lblPrefecture.Text = dt.Rows[0]["Division"].ToString();
                }
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
            {
                if (Session["Order_table"] != null)
                {
                    Order_table = (ArrayList)Session["Order_table"];
                    int i = int.Parse((Order_table[7]).ToString());

                    DataTable dt = GetChargeCondition(i, totalAmount);

                    Label lblTotalQuantity = (Label)e.Row.FindControl("lblTotalQuantity");
                    Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
                    Label lblCODAmount = (Label)e.Row.FindControl("lblCODAmount");
                    Label lblshipping = (Label)e.Row.FindControl("lblShippingAmount");

                    lblTotalQuantity.Text = totalQuantity.ToString();

                    lblshipping.Text = String.Format("送料: {0} 円", dt.Rows[0]["Delivery_Amount"].ToString());
                    lblCODAmount.Text = dt.Rows[0]["COD_Amount"].ToString();
                    finalAmount = totalAmount + decimal.Parse(dt.Rows[0]["Delivery_Amount"].ToString()) + decimal.Parse(dt.Rows[0]["COD_Amount"].ToString());



                    lblTotalAmount.Text = finalAmount.ToString();
                    //lblAveragePrice.Text = (totalPrice / totalItems).ToString("F");
                }
            }
        }

        private decimal GetFinalAmount()
        {
            DataTable dt = AddToCart;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    totalAmount += decimal.Parse(dr["Amount"].ToString());
                }
                Order_table = (ArrayList)Session["Order_table"];
                int i = int.Parse((Order_table[7]).ToString());
                DataTable dtChargeCondition = GetChargeCondition(i, totalAmount);
                finalAmount = totalAmount + decimal.Parse(dtChargeCondition.Rows[0]["Delivery_Amount"].ToString()) + decimal.Parse(dtChargeCondition.Rows[0]["COD_Amount"].ToString());
                return finalAmount;
            }

            return 0;
        }

        public void BindArrayList()
        {

            if (Session["Order_table"] != null)
            {
                Order_table = (ArrayList)Session["Order_table"];


                lbl_Name.Text = Order_table[1].ToString() + Order_table[0].ToString();


                lbl_Phonetic.Text = Order_table[3].ToString() + Order_table[2].ToString();



                lblPostal_Code.Text = Order_table[6].ToString();


                lbl_City.Text = Order_table[8].ToString();

                lbl_Address.Text = Order_table[9].ToString();
                lblAddress_2.Text = Order_table[10].ToString();



                lblPhone_no.Text = Order_table[11].ToString();



                lblHand_Phone.Text = Order_table[12].ToString();

                lblemail_add.Text = Order_table[4].ToString();

            }
            else
            {
                Order_table = new ArrayList();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            jiOrder_bl = new Jisha_Order_BL();
            jiOrder_ent = new Jisha_Order_Entity();

            if (Session["Order_table"] != null)
            {
                Order_table = (ArrayList)Session["Order_table"];
                jiOrder_ent.Bill_LastName = Order_table[0].ToString();
                jiOrder_ent.Bill_FirstName = Order_table[1].ToString();
                jiOrder_ent.Bill_LastName_Kana = Order_table[2].ToString();
                jiOrder_ent.Bill_FirstName_Kana = Order_table[3].ToString();
                jiOrder_ent.Bill_MailAddress = Order_table[4].ToString();
                jiOrder_ent.Bill_ZipCode = Order_table[6].ToString();
                jiOrder_ent.Bill_Prefecture = Order_table[7].ToString();
                jiOrder_ent.Bill_City = Order_table[8].ToString();
                jiOrder_ent.Bill_Address1 = Order_table[9].ToString();
                jiOrder_ent.Bill_Address2 = Order_table[10].ToString();
                jiOrder_ent.Bill_PhoneNo = Order_table[11].ToString();
                jiOrder_ent.Bill_Emg_PhoneNo = Order_table[12].ToString();
                jiOrder_ent.Ship_LastName = Order_table[13].ToString();
                jiOrder_ent.Ship_FirstName = Order_table[14].ToString();
                jiOrder_ent.Ship_LastName_Kana = Order_table[15].ToString();
                jiOrder_ent.Ship_FirstName_Kana = Order_table[16].ToString();
                jiOrder_ent.Ship_ZipCode = Order_table[17].ToString();
                jiOrder_ent.Ship_Prefecture = Order_table[18].ToString();
                jiOrder_ent.Ship_City = Order_table[19].ToString();
                jiOrder_ent.Ship_Address1 = Order_table[20].ToString();
                jiOrder_ent.Ship_Address2 = Order_table[21].ToString();
                jiOrder_ent.Ship_PhoneNo = Order_table[22].ToString();


                if (Request.QueryString["SelectedValue"] != null)
                {
                    v = Request.QueryString["SelectedValue"];
                    jiOrder_ent.Payment_Method = v;

                }
                else
                {
                    jiOrder_ent.Payment_Method = "";
                }

                int i = Convert.ToInt32(Order_table[7].ToString());

                DataTable dt = GetChargeCondition(i, GetFinalAmount());

                jiOrder_ent.COD_Charge_Amount = Convert.ToInt32(dt.Rows[0]["COD_Amount"].ToString());
                jiOrder_ent.Delivery_ChargeID = Convert.ToInt32(dt.Rows[0]["Delivery_AmountID"].ToString());
                jiOrder_ent.COD_ChargeID = Convert.ToInt32(0);
                jiOrder_ent.Delivery_Charge_Amount = Convert.ToInt32(dt.Rows[0]["Delivery_Amount"].ToString());

                jiOrder_ent.Tax = CalculateTax();

                dt = AddToCart;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        totalAmount += decimal.Parse(dr["Amount"].ToString());





                    }
                    jiOrder_ent.Sub_Total = Convert.ToInt32(totalAmount);


                    Order_table = (ArrayList)Session["Order_table"];
                    i = int.Parse((Order_table[7]).ToString());
                    DataTable dtChargeCondition = GetChargeCondition(i, totalAmount);
                    finalAmount = totalAmount + decimal.Parse(dtChargeCondition.Rows[0]["Delivery_Amount"].ToString()) + decimal.Parse(dtChargeCondition.Rows[0]["COD_Amount"].ToString());

                    jiOrder_ent.Total = Convert.ToInt32(finalAmount);

                    if (Session["AddToCart"] != null)
                    {
                        dt = (DataTable)Session["AddToCart"];
                        jiOrder_bl.Insert(jiOrder_ent, dt);
                    }

                    Session.Remove("AddToCart");
                    Session.Remove("Order_table");
                }
            }
        }

        private DataTable GetChargeCondition(int divisionID, decimal totalAmount)
        {
            DataTable dtDelivery = new DataTable();
            DataTable dtCod = new DataTable();
            DataTable dtTmp = new DataTable();
            dtTmp.Columns.Add("Delivery_AmountID", typeof(decimal));
            dtTmp.Columns.Add("Delivery_Amount", typeof(decimal));
            dtTmp.Columns.Add("COD_Amount", typeof(decimal));
            dtTmp.Columns.Add("COD_AmountID", typeof(decimal));

            jishaDeliveryChargeBL = new Jisha_Delivery_Charge_BL();

            decimal deliveryAmount = 0;
            decimal codAmount = 0;
            int deliveryID = 0;
            int codID = 0;

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
                        deliveryID = int.Parse(dr["ID"].ToString());
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
                            codID = int.Parse(dr["ID"].ToString());

                            break;
                        }
                    }
                }
            }

            dtTmp.Rows.Add(deliveryID, deliveryAmount, codAmount, codID);
            dtTmp.AcceptChanges();

            return dtTmp;
        }

        private decimal CalculateTax()
        {
            jishaTaxSettingBL = new Jisha_Tax_Setting_BL();

            DataTable dt = jishaTaxSettingBL.SelectLatestJishaTaxSetting();
            int taxPercentage = 0;
            decimal tax = 0;

            if (dt.Rows.Count > 0)
            {
                taxPercentage = int.Parse(dt.Rows[0]["Tax_Percentage"].ToString());
            }
            //finalAmount = GetFinalAmount();


            dt = AddToCart;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    totalAmount = decimal.Parse(dr["Amount"].ToString());




                    tax = totalAmount * taxPercentage / 100;
                }
                totalAmount = 0;
                return tax;
            }
            return 0;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/WebForms/Jisha/Jisha_Payment.aspx");
        }


    }

}

    



