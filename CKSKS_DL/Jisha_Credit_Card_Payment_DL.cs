using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
   public class Jisha_Credit_Card_Payment_DL
    {
       public void Insert(Jisha_Credit_Card_Entity jishaCreditCardInfo)
       {
           try
           {
               SqlConnection con = JsDataConfig.GetConnectionString();
               SqlCommand cmd = new SqlCommand("SP_Jisha_CreditCard_Payment_Insert", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandTimeout = 0;
               cmd.Parameters.Add("@Order_ID", jishaCreditCardInfo.OrderID);
               cmd.Parameters.Add("@ACS", jishaCreditCardInfo.ACS);
               cmd.Parameters.Add("@Forward", jishaCreditCardInfo.Forward);
               cmd.Parameters.Add("@Method", jishaCreditCardInfo.Method);
               cmd.Parameters.Add("@PayTimes", jishaCreditCardInfo.PayTimes);
               cmd.Parameters.Add("@Approve", jishaCreditCardInfo.Approve);
               cmd.Parameters.Add("@TranID", jishaCreditCardInfo.TranID);
               cmd.Parameters.Add("@Tran_Date", jishaCreditCardInfo.TranDate);
               cmd.Parameters.Add("@Check_String", jishaCreditCardInfo.CheckString);
               cmd.Parameters.Add("@Created_Date", jishaCreditCardInfo.CreatedDate);
               cmd.Connection.Open();
               cmd.ExecuteNonQuery();
               cmd.Connection.Close();
               //return true;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
