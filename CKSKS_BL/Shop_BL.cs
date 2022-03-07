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
    public class Shop_BL
    {
        Shop_DL shdl;
        
        public  Shop_BL()
        {
        shdl = new Shop_DL();
        }

          public String Insert(Shop_Entity shentity)
        {
           
            if (shdl.Insert(shentity))
            {
                return "Save Successful !";
            }
            else
            {
                return "Save Fail !";
            }
        }


          public string Update(Shop_Entity shentity)
        {
            if (shdl.Update(shentity))
            {
                return "Update Successful !";
            }
            else
            {
                return "Update Fail !";
            }
        }


          public DataTable Search(string name,string mall)
          {
              return shdl.Search(name,mall);
          }

          public Shop_Entity  SelectByID(int id)
          {
             shdl = new Shop_DL();
              return  shdl.SelctByID(id);
          }


          public DataTable SelectAll()
          {
              shdl = new Shop_DL();
              return shdl.SelectAll();
          }
        public DataTable SelectAll_URL()
        {
            shdl = new Shop_DL();
            return shdl.SelectAll_URL();
        }
        public DataTable SelectShopAndMall()
          {
              shdl = new Shop_DL();
              return shdl.SelectShopAndMall();
          }

          public string SelectProductPageURL(int Shop_ID)
          {
              return shdl.SelectProductPageURL(Shop_ID);
          }


    }
}
