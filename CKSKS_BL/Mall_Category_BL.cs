using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;
using ORS_RCM_Common;

namespace ORS_RCM_BL
{
    public class Mall_Category_BL
    {
        Mall_Category_DL mdl;
        public Mall_Category_BL() 
        {
            mdl = new Mall_Category_DL();
        }

        public DataTable Search(string mallid, string cname1, string cname2, string cname3)
        {
            return mdl.SearchDesc(mallid, cname1, cname2, cname3);
        }


        public DataTable Mall_CategoryExport(int mid)
        {
            return mdl.ExportMall(mid);
        }


        /// <summary>
        /// Category Export For Jisha From Category
        /// </summary>
        /// <returns></returns>
        public DataTable  CategoryExportFor_Jisha()
        {
            return mdl.ExportCategory();
        }
       

        public Boolean InsertCSV(DataTable dt,int id)
        {
            return mdl.InsertCSV(dt,id);
        }

        public Boolean InsertMall_Category_XML(String xml)
        {
            return mdl.InsertMall_Category_XML(xml);
        }

        public DataTable SelectByMallID(int mallID, int pageindex, int pagesize, int option,string c1,string c2,string c3)
        {
            return mdl.SelectByMallID(mallID, pageindex, pagesize, option,c1,c2,c3);
        }

        public DataTable SelectByMallID(int mallID)
        {
            return mdl.SelectByMallID(mallID);
        }

        public DataTable SelectByID(int ID)
        {
            return mdl.SelectByID(ID);
        }

        public DataTable SearchCat(int mallid, string cid , string cname1, string cname2, string cname3)
        {
            return mdl.SearchCategoID(mallid,cid, cname1, cname2, cname3);
        }


    }
}
