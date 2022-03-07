using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using System.Data;

namespace ORS_RCM_BL
{
    public class Yahoo_SpecName_BL
    {
        Yahoo_SpecName_DL YahooSpecificNameDL;

        public Yahoo_SpecName_BL()
        {
            YahooSpecificNameDL = new Yahoo_SpecName_DL();
        }

        public DataTable SelectByYahooMallCategoryID(string MallCategoryID)
        {
            return YahooSpecificNameDL.SelectByYahooMallCategoryID(MallCategoryID);
        }

        public Boolean InsertUpdateYahooSpec(String xml)
        {
            return YahooSpecificNameDL.InsertUpdateYahooSpec(xml);
        }

        public DataTable DisplayYahooSpecificValue(int ymallID)
        {
            return YahooSpecificNameDL.DisplayYahooSpecificValue(ymallID);
        }


    }
}
