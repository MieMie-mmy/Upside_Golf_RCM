using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
  public  class Jisha_Order_BL
    {

      Jisha_Order_DL jiOrder_dl;
      Jisha_Item_Master_DL JishaDL;

      public  Jisha_Order_BL()
        {
            jiOrder_dl = new  Jisha_Order_DL();
            JishaDL = new Jisha_Item_Master_DL();
        }

   
      public  String Insert(Jisha_Order_Entity jentity ,DataTable dt)
      {
          string Order_ID = jiOrder_dl.Save(jentity);
          if (Order_ID != null)
          {

              if (!ContainColumn("Order_ID", dt))
              {
                  dt.Columns.Add(new DataColumn("Order_ID", typeof(String)));
              }
              if (!ContainColumn("Size_Code", dt))
              {
                  dt.Columns.Add(new DataColumn("Size_Code", typeof(String)));
              }
              if (!ContainColumn("Color_Code", dt))
              {
                  dt.Columns.Add(new DataColumn("Color_Code", typeof(String)));
              }
              foreach (DataRow row in dt.Rows)
              {
                  row["Order_ID"] = Order_ID;
              }

              JishaDL.Insert(dt);
              return  "Save Successful !";
          }
          else
          {
              return "Save Fail !";
          }
      }

      public DataTable SelectByOrderID(String OrderID)
      {
          return jiOrder_dl.SelectByOrderID(OrderID);
      }

      public DataTable SelectAll()
      {
          return jiOrder_dl.SelectAll();
      }


      public DataTable SelectDivision()
      {
          jiOrder_dl = new Jisha_Order_DL();
          return jiOrder_dl.SelectPrefecture();

      }

      public DataTable Selected_Division(int i)
      {
          jiOrder_dl = new Jisha_Order_DL();
          return jiOrder_dl.SelectPrefectureValue(i);

      }




      public bool ContainColumn(string columnName, DataTable table)
      {
          DataColumnCollection columns = table.Columns;

          if (columns.Contains(columnName))
          {
              return true;
          }
          else
              return false;
      }



    }
}
