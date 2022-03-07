using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;

namespace ORS_RCM.WebForms.Jisha
{
    public partial class Jisha_Order_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Jisha_OrderDetail_BL jodbl = new Jisha_OrderDetail_BL();
            gvOrder.DataSource = jodbl.SelectAll();
            gvOrder.DataBind();

            if (Request.QueryString["OrderID"] != null)
            {
                String orderID = Request.QueryString["OrderID"].ToString();
                lblOrderID.Text = orderID;

                Jisha_Order_BL jobl = new Jisha_Order_BL();
                DataTable dt = jobl.SelectByOrderID(orderID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    lblBillLastName.Text = dt.Rows[0]["Bill_LastName"].ToString();
                    lblBillFirstName.Text = dt.Rows[0]["Bill_FirstName"].ToString();
                    lblBillLastNameKana.Text = dt.Rows[0]["Bill_LastName_Kana"].ToString();
                    lblBillFirstNameKana.Text = dt.Rows[0]["Bill_FirstName_Kana"].ToString();
                    lblBillMailAddress.Text = dt.Rows[0]["Bill_MailAddress"].ToString();
                    lblBillZipCode.Text = dt.Rows[0]["Bill_ZipCode"].ToString();
                    lblBillPrefecture.Text = dt.Rows[0]["Bill_Prefecture"].ToString();
                    lblBillCity.Text = dt.Rows[0]["Bill_City"].ToString();
                    lblBillAddress1.Text = dt.Rows[0]["Bill_Address1"].ToString();
                    lblBillAddress2.Text = dt.Rows[0]["Bill_Address2"].ToString();
                    lblBillPhoneNo1.Text = dt.Rows[0]["Bill_PhoneNo"].ToString();
                    lblBillPhoneNo2.Text = dt.Rows[0]["Bill_Emg_PhoneNo"].ToString();

                    lblShipLastName.Text = dt.Rows[0]["Ship_LastName"].ToString();
                    lblShipFirstName.Text = dt.Rows[0]["Ship_FirstName"].ToString();
                    lblShipLastNameKana.Text = dt.Rows[0]["Ship_LastName_Kana"].ToString();
                    lblShipFirstNameKana.Text = dt.Rows[0]["Ship_FirstName_Kana"].ToString();
                    lblShipMailAddress.Text = dt.Rows[0]["Ship_MailAddress"].ToString();
                    lblShipZipCode.Text = dt.Rows[0]["Ship_ZipCode"].ToString();
                    lblShipPrefecture.Text = dt.Rows[0]["Ship_Prefecture"].ToString();
                    lblShipCity.Text = dt.Rows[0]["Ship_City"].ToString();
                    lblShipAddress1.Text = dt.Rows[0]["Ship_Address1"].ToString();
                    lblShipAddress2.Text = dt.Rows[0]["Ship_Address2"].ToString();
                    lblShipPhoneNo1.Text = dt.Rows[0]["Ship_PhoneNo"].ToString();
                    //lblShipPhoneNo2.Text = dt.Rows[0]["Ship_Emg_PhoneNo"].ToString();

                    lblOrderDate.Text = dt.Rows[0]["Order_Date"].ToString();
                    lblPaymentMethod.Text = dt.Rows[0]["Payment_Method"].ToString();
                    lblSettleID.Text = dt.Rows[0]["Settle_ID"].ToString();
                    lblAutNo.Text = dt.Rows[0]["Auth_No"].ToString();
                    lblComment.Text = dt.Rows[0]["Order_Comment"].ToString();
                    lblGroupType.Text = dt.Rows[0]["Group_Type"].ToString();
                    lblGroupName.Text = dt.Rows[0]["Group_Name"].ToString();
                    lblMailMagazine.Text = dt.Rows[0]["MailMagazine"].ToString();
                    lblSubTotal.Text = dt.Rows[0]["Sub_Total"].ToString() + "円";
                    lblShipCharge.Text = dt.Rows[0]["COD_Charge_Amount"].ToString() + "円";
                    lblDeliveryCharge.Text = dt.Rows[0]["Delivery_Charge_Amount"].ToString() + "円";
                    lblTax.Text = dt.Rows[0]["Tax"].ToString() + "円";
                    lblTotal.Text = dt.Rows[0]["Total"].ToString() + "円";
                }
            }
        }
    }
}