using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
  public  class Jisha_Order_DL
    {

      public Jisha_Order_DL() { }

      public DataTable SelectByOrderID(String OrderID)
      {
          String query = "SELECT * FROM Jisha_Order WHERE Order_ID='" + OrderID+"'";
          SqlCommand cmd = new SqlCommand(query, JsDataConfig.GetConnectionString());
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          DataTable dt = new DataTable();
          cmd.Connection.Open();
          da.Fill(dt);
          cmd.Connection.Close();
          return dt;
      }

      public DataTable SelectAll()
      {
          SqlCommand cmd = new SqlCommand("SELECT * FROM Jisha_Order", JsDataConfig.GetConnectionString());
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          DataTable dt = new DataTable();
          cmd.Connection.Open();
          da.Fill(dt);
          cmd.Connection.Close();
          return dt;
      }


      public DataTable SelectPrefecture()
      {
          SqlCommand cmd = new SqlCommand("SELECT * FROM  Jisha_Division", JsDataConfig.GetConnectionString());
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          DataTable dt = new DataTable();
          cmd.Connection.Open();
          da.Fill(dt);
          cmd.Connection.Close();
          return dt;
      }


      public DataTable SelectPrefectureValue(int i)
      {
          SqlCommand cmd = new SqlCommand("SELECT Division  FROM  Jisha_Division where ID=" + i, JsDataConfig.GetConnectionString());
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          DataTable dt = new DataTable();
          cmd.Connection.Open();
          da.Fill(dt);
          cmd.Connection.Close();
          return dt;
      }
    

      




      public string Save(Jisha_Order_Entity  jisha_order)
      {
          //string Order_ID = null;
          SqlCommand cmd = new SqlCommand();
           try
          {
              //Order_ID = jisha_order.Order_ID;
              cmd.CommandText = "SP_Jisha_Order_Insert";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.CommandTimeout = 0;
               cmd.Connection = JsDataConfig.GetConnectionString();
              String i = GetOrderID().ToString();
              cmd.Parameters.AddWithValue("@Order_ID", i);
              cmd.Parameters.AddWithValue("@Order_Date", System.DateTime.Now.ToString());
              cmd.Parameters.AddWithValue("@Bill_LastName", jisha_order.Bill_LastName);
              cmd.Parameters.AddWithValue("@Bill_FirstName", jisha_order.Bill_FirstName);
              cmd.Parameters.AddWithValue("@Bill_LastName_Kana", jisha_order.Bill_LastName_Kana);
              cmd.Parameters.AddWithValue("@Bill_FirstName_Kana", jisha_order.Bill_FirstName_Kana);
              cmd.Parameters.AddWithValue("@Bill_MailAddress", jisha_order.Bill_MailAddress);
              cmd.Parameters.AddWithValue("@Bill_ZipCode", jisha_order.Bill_ZipCode);
              cmd.Parameters.AddWithValue("@Bill_Prefecture", jisha_order.Bill_Prefecture);
              cmd.Parameters.AddWithValue("@Bill_City", jisha_order.Bill_City);
              cmd.Parameters.AddWithValue("@Bill_Address1", jisha_order.Bill_Address1);
              cmd.Parameters.AddWithValue("@Bill_Address2", jisha_order.Bill_Address2);
              cmd.Parameters.AddWithValue("@Bill_PhoneNo", jisha_order.Bill_PhoneNo);
              cmd.Parameters.AddWithValue("@Bill_Emg_PhoneNo", jisha_order.Bill_Emg_PhoneNo);
              cmd.Parameters.AddWithValue("@Ship_LastName", jisha_order.Ship_LastName);
              cmd.Parameters.AddWithValue("@Ship_FirstName", jisha_order.Ship_FirstName);

              cmd.Parameters.AddWithValue("@Ship_LastName_Kana", jisha_order.Ship_LastName_Kana);

              cmd.Parameters.AddWithValue("@Ship_FirstName_Kana", jisha_order.Ship_FirstName_Kana);
              cmd.Parameters.AddWithValue("@Ship_ZipCode", jisha_order.Ship_ZipCode);

              cmd.Parameters.AddWithValue("@Ship_Prefecture", jisha_order.Ship_Prefecture);

              cmd.Parameters.AddWithValue("@Ship_City", jisha_order.Ship_City);

              cmd.Parameters.AddWithValue("@Ship_Address1", jisha_order.Ship_Address1);

              cmd.Parameters.AddWithValue("@Ship_Address2", jisha_order.Ship_Address2);

              cmd.Parameters.AddWithValue("@Ship_PhoneNo", jisha_order.Ship_PhoneNo);

              cmd.Parameters.AddWithValue("@Payment_Method", jisha_order.Payment_Method);


              //cmd.Parameters.AddWithValue("@Order_Comment", jisha_order.Order_Comment);

              //cmd.Parameters.AddWithValue("@Group_Type", jisha_order.Group_Type);

              //cmd.Parameters.AddWithValue("@Group_Name", jisha_order.Group_Name);


              //cmd.Parameters.AddWithValue("@MailMagazine", jisha_order.MailMagazine);

              //cmd.Parameters.AddWithValue("@Use_Point", jisha_order.Use_Point);

              cmd.Parameters.AddWithValue("@Sub_Total", jisha_order.Sub_Total);

              cmd.Parameters.AddWithValue("@COD_ChargeID", jisha_order.COD_ChargeID);
              cmd.Parameters.AddWithValue("@COD_Charge_Amount", jisha_order.COD_Charge_Amount);


              cmd.Parameters.AddWithValue("@Tax", jisha_order.Tax);

              cmd.Parameters.AddWithValue("@Delivery_ChargeID", jisha_order.Delivery_ChargeID);
              cmd.Parameters.AddWithValue("@Delivery_Charge_Amount", jisha_order.Delivery_Charge_Amount);

              cmd.Parameters.AddWithValue("@Total", jisha_order.Total);

               cmd.Parameters.AddWithValue("@Created_Date", System.DateTime.Now.ToString());

               
                cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                int id = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (id > 0)
                {
                    return id.ToString();
                }
                return null;
            }
           catch
               (Exception ex)
           { throw ex; }

        }

      public int GetOrderID()
      {
          try
          {
              DataTable dt = new DataTable();
              String query = "Select TOP 1 * From Jisha_Order_No_Setting order by ID desc";
              SqlDataAdapter sda = new SqlDataAdapter(query, JsDataConfig.GetConnectionString());
              sda.SelectCommand.CommandType = CommandType.Text;
              sda.SelectCommand.CommandTimeout = 0;
              sda.SelectCommand.Connection.Open();
              sda.Fill(dt);
              sda.SelectCommand.Connection.Close();
              if (dt != null && dt.Rows.Count > 0)
              {
                  if (!string.IsNullOrWhiteSpace(dt.Rows[0]["Order_No"].ToString()))
                  return Convert.ToInt32(dt.Rows[0]["Order_No"]);
              }
              return 0;
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

      }

    }

