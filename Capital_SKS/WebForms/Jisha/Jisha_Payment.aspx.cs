using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Collections;
using System.Collections.Specialized;
using System.Data.SqlClient;
using ORS_RCM_BL;
using ORS_RCM_Common;

namespace ORS_RCM.WebForms.Jisha
{
    public partial class Jisha_Payment : System.Web.UI.Page
    {

        ArrayList Order_table;

        private DataTable dtAddToCart
        {
            get
            {
                if (Session["AddToCart"] != null)
                    return (DataTable)Session["AddToCart"];
                else
                    return null;
            }
            set
            {
                Session["AddToCart"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            
            if (Page.IsPostBack)
            {
                return;
            }

            if (!IsPostBack)
            {
                //populate month
                string[] Month = new string[] { "", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
                ddlMonth.DataSource = Month;
                ddlMonth.DataBind();
                //pre-select one for testing
                ddlMonth.SelectedIndex = 4;

                //populate year
                ddlYear.Items.Add("");
                int Year = DateTime.Now.Year;
                for (int i = 0; i < 10; i++)
                {
                    ddlYear.Items.Add((Year + i).ToString());
                }
                //pre-select one for testing
                ddlYear.SelectedIndex = 3;
                lblCreditValidator.Text = "";

                
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string uri = string.Empty;
            string parameters = string.Empty;
            string HtmlResult = string.Empty;
            string orderID = string.Empty;
            string shopPass = string.Empty;
            string amount = string.Empty;
            string shopID = string.Empty;

            UpdateAddToCartSession();

            if (rdoCreditCard.Checked)
            {
                if (ValidateCreditCardNo())
                {
                    #region Transaction Registration
                    //orderID = "O021";
                    amount = "2000";
                    shopID = "tshop00016301";
                    shopPass = "hp2mq3ny";
                    uri = "https://pt01.mul-pay.jp/payment/EntryTran.idPass";
                    parameters = "ShopID=" + shopID
                        + "&ShopPass=" + shopPass
                        + "&OrderID=" + dtAddToCart.Rows[0]["Order_No"].ToString()
                        + "&JobCd=CAPTURE"
                        + "&Amount=" + amount;
                    HtmlResult = HttpPost(uri, parameters);

                    DataTable dtTransactionInfo = ShowTransactionInfo(GetCollection(HtmlResult));
                    #endregion

                    #region Transaction Execution
                    uri = "https://pt01.mul-pay.jp//payment/ExecTran.idPass";
                    parameters = string.Empty;
                    parameters = "AccessID=" + (string)dtTransactionInfo.Rows[0]["Value"]
                        + "&AccessPass=" + (string)dtTransactionInfo.Rows[1]["Value"]
                        + "&OrderID=" + dtAddToCart.Rows[0]["Order_No"].ToString()
                        + "&Method=1"
                        + "&CardNo=" + Request["txtFirstCreditNo"] + Request["txtSecondCreditNo"] + Request["txtThirdCreditNo"] + Request["txtFourthCreditNo"] // txtFirstCreditNo.Text + txtSecondCreditNo.Text + txtThirdCreditNo.Text + txtFourthCreditNo.Text
                        + "&Expire=" + ddlMonth.SelectedValue + ddlYear.SelectedValue.Substring(2, 2);
                    HtmlResult = HttpPost(uri, parameters);

                    DataTable dtResult = ShowTransactionInfo(GetCollection(HtmlResult));
                    
                    #endregion
                    
                    if (ValidateTransaction(dtResult) && (ddlPaymentMethod.SelectedItem.Value.Length >= 0) )
                    {
                        InsertJishaCreditCardPayment(dtResult);
                        InsertJishaOrderNo();
                        Response.Redirect(String.Format("~/WebForms/Jisha/Order_Completed_Form.aspx?PaymentMode={0}&SelectedValue={1}", "CreditCard",ddlPaymentMethod.SelectedValue));
                    }
                }

            }
            else if (rdoCashOn.Checked)
            {
                InsertJishaOrderNo();
                Response.Redirect(String.Format("~/WebForms/Jisha/Order_Completed_Form.aspx?PaymentMode={0}", "CashOn"));
              
            }
            else
            {
                InsertJishaOrderNo();
                Response.Redirect(String.Format("~/WebForms/Jisha/Order_Completed_Form.aspx?PaymentMode={0}", "CreditCard"));
            }
            
        }

        /// <summary>
        /// Send parameters to the provided uri.
        /// </summary>
        /// <param name="uri">GMO Payment Gateway's URI.</paparam>
        /// <param name="parameters">Parameters provided by Merchant.</param>
        string HttpPost(string uri, string parameters)
        {
            string HtmlResult = string.Empty;
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                HtmlResult = wc.UploadString(uri, parameters);
            }
            return HtmlResult;
        }

        /// <summary>
        /// Get NameValueCollection from the html response.
        /// </summary>
        /// <param name="transactionInfo">Reponse string returned by HttpPost.</param>
        private NameValueCollection GetCollection(string transactionInfo)
        {
            //place the responses into collection
            NameValueCollection TransactionCollection =
            new System.Collections.Specialized.NameValueCollection();
            string[] ArrayReponses = transactionInfo.Split('&');

            for (int i = 0; i < ArrayReponses.Length; i++)
            {
                string[] Temp = ArrayReponses[i].Split('=');
                TransactionCollection.Add(Temp[0], Temp[1]);
            }
            return TransactionCollection;
        }

        /// <summary>
        /// Put NameValueCollection into DataTable.
        /// </summary>
        /// <param name="collection">NameValueCollection of HttpPost response.</param>
        private DataTable ShowTransactionInfo(NameValueCollection collection)
        {
            //string TransactionInfo = "";
            DataTable dt = new DataTable(); 
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            foreach (string key in collection.AllKeys)
            {
                //TransactionInfo += "<br /><span class=\"bold-text\">" +
                //    key + ":</span> " + collection[key];
                dt.Rows.Add(key, collection[key]);
            }
            //return TransactionInfo;
            return dt;
        }

        private void InsertJishaCreditCardPayment(DataTable dt)
        {
            Jisha_Credit_Card_Payment_BL jishaCreditCardBL = new Jisha_Credit_Card_Payment_BL();
            Jisha_Credit_Card_Entity jishaCreditCardInfo = new Jisha_Credit_Card_Entity();

            if (dt != null && ValidateTransaction(dt))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Name"].ToString() == "OrderID")
                    {
                        jishaCreditCardInfo.OrderID = dr["Value"].ToString();
                    }
                    if (dr["Name"].ToString() == "ACS")
                    {
                        jishaCreditCardInfo.ACS = dr["Value"].ToString();
                    }
                    if (dr["Name"].ToString() == "Forward")
                    {
                        jishaCreditCardInfo.Forward = dr["Value"].ToString();
                    }
                    if (dr["Name"].ToString() == "Method")
                    {
                        jishaCreditCardInfo.Method = int.Parse(dr["Value"].ToString());
                    }
                    if (dr["Name"].ToString() == "PayTimes")
                    {
                        jishaCreditCardInfo.PayTimes = dr["Value"].ToString();
                    }
                    if (dr["Name"].ToString() == "Approve")
                    {
                        jishaCreditCardInfo.Approve = dr["Value"].ToString();
                    }
                    if (dr["Name"].ToString() == "TranID")
                    {
                        jishaCreditCardInfo.TranID = dr["Value"].ToString();
                    }
                    if (dr["Name"].ToString() == "TranDate")
                    {
                        string date = dr["Value"].ToString().Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":");
                        jishaCreditCardInfo.TranDate = DateTime.Parse(date);
                    }
                    if (dr["Name"].ToString() == "Check_String")
                    {
                        jishaCreditCardInfo.CheckString = dr["Value"].ToString();
                    }
                }
                jishaCreditCardInfo.CreatedDate = DateTime.Now;
                jishaCreditCardBL.Insert(jishaCreditCardInfo);
            }
        }

        private void InsertJishaOrderNo()
        {
            Jisha_Order_No_Setting_BL jishaOrderNoBL = new Jisha_Order_No_Setting_BL();
            Jisha_Order_No_Setting_Entity jishaOrderNoInfo = new Jisha_Order_No_Setting_Entity();

            jishaOrderNoInfo.OrderNo = int.Parse(dtAddToCart.Rows[0]["Order_No"].ToString());
            jishaOrderNoInfo.Date = DateTime.Now;

            jishaOrderNoBL.Insert(jishaOrderNoInfo);
        }

        private bool ValidateCreditCardNo()
        {
            if (Request["txtFourthCreditNo"] == "")
            {
                lblCreditValidator.Text = "Please provide correct Credit Card Number!";
                return false;
            }
            if (Request["txtSecondCreditNo"] == "")
            {
                lblCreditValidator.Text = "Please provide correct Credit Card Number!";
                return false;
            }
            if (Request["txtThirdCreditNo"] == "")
            {
                lblCreditValidator.Text = "Please provide correct Credit Card Number!";
                return false;
            }
            if (Request["txtFourthCreditNo"] == "")
            {
                lblCreditValidator.Text = "Please provide correct Credit Card Number!";
                return false;
            }
            lblCreditValidator.Text = "";
            return true;
        }

        public void MessageBox(string message)
        {
            if (message == "Saving Successful ! " || message == "Updating Successful ! ")
            {
                Session["CatagoryList"] = null;
                object referrer = ViewState["UrlReferrer"];
                string url = (string)referrer;
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }
            else
            {
                string cleanMessage = message.Replace("'", "\\'");
                string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
                Page page = HttpContext.Current.CurrentHandler as Page;
                if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                {
                    page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                }
            }
        }

        private bool ValidateTransaction(DataTable dt)
        {
            if (dt.Rows[0]["Name"].ToString() == "ErrCode")
            {
                MessageBox("Transaction Failed!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Create new Order_ID column for AddToCartSession
        /// </summary>
        /// <returns></returns>
        private void UpdateAddToCartSession()
        {
            if (!dtAddToCart.Columns.Contains("Order_No"))
            {
                dtAddToCart.Columns.Add("Order_No", typeof(int));    
            }

            Jisha_Order_No_Setting_BL jishaOrderNo = new Jisha_Order_No_Setting_BL();
            DataTable dt = jishaOrderNo.SelectByCurrentDate();
            Int64 orderID = 0;
            int realOrderID = 0;
            string date = "";
            if (dt.Rows.Count > 0)
            {
                date = DateTime.Now.ToShortDateString().Replace("/", "");
                realOrderID = int.Parse(dt.Rows[0]["Order_No"].ToString()) + 1;
                date = "1" + date.Substring(2, date.Length - 2) + realOrderID.ToString();
                orderID = Int64.Parse(date);
            }
            else
            {
                date = DateTime.Now.ToShortDateString().Replace("/","");
                realOrderID = 1;
                date = "1" + date.Substring(2, date.Length - 2) + realOrderID.ToString();
                orderID = int.Parse(date);
            }

            foreach (DataRow dr in dtAddToCart.Rows)
            {
                dr["Order_No"] = realOrderID;
            }
            dtAddToCart.AcceptChanges();

            //return orderID;
        }
    }
}