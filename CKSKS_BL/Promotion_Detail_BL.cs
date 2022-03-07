using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;
namespace ORS_RCM_BL
{
  public  class Promotion_Detail_BL
    {
      Promotion_Detail_DL pdl;

      public Promotion_Detail_BL()
        {
            pdl = new Promotion_Detail_DL(); 
        }

      public DataTable CamapignRakuten(string list, int sid, string option) 
      {
          return pdl.GetdataforRakuten(list,sid,option);
      }
      public DataTable PointRakuten(string list, int sid, int option)
      {
          return pdl.GetdataforPointRakuten(list, sid, option);
      }
      public DataTable DeliveryRakuten(string list, int sid, int option)
      {
          return pdl.GetdataforDeliveryRakuten(list, sid, option);
      }
      public DataTable CamapignPonpare(string list, int sid, string option)
      {
          return pdl.GetdataforPonpare(list, sid, option);
      }
      public DataTable DeliveryJisha(string list, int sid, int option)
      {
          return pdl.GetdataforJisha(list, sid, option);
      }
      public DataTable PointPonpare(string list, int sid, int option)
      {
          return pdl.GetdataforPointPonpare(list, sid, option);
      }
      public DataTable DeliveryPonpare(string list, int sid, int option)
      {
          return pdl.GetdataforDeliveryPonpare(list, sid, option);
      }
    }
}
