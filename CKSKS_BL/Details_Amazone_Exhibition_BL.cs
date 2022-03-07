using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
   public class Details_Amazone_Exhibition_BL
    {
       Details_Amazone_Exhibition_DL dama;
       public Details_Amazone_Exhibition_BL() 
       {
          dama = new Details_Amazone_Exhibition_DL();
       }

       public DataTable SelectbyID(int id) 
       {
           return dama.SelectbyID(id);
       }

       public DataTable SelectByExhibitionData(string itemIDList)
       {
           return dama.SelectByExhibitionData(itemIDList);
       }


    }
}
