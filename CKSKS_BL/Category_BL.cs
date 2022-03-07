using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
    public class Category_BL
    {
        Category_DL cdl;
        public Category_BL()
        {
            cdl = new Category_DL();
        }

        public DataTable SelectForTreeview1(int i)
        {
            return cdl.SelectForTreeview1(i);
        }

        public DataTable SelectForTreeview(int i)
        {
            return cdl.SelectForTreeview(i);
        }

        public DataTable ForTreeviewSearch(int i)
        {
            return cdl.ForTreeview(i);
        }

        public void CategoryInsert(int childID, String text, String CID, String RID, String RCNo, String Yahoo, String Wowma,String Tennis, String JID, String JCNo, int Serial, String pathdesc)
        {
            cdl.CategoryInsert(childID, text, CID, RID, RCNo, Yahoo, Wowma,Tennis, JID, JCNo, Serial, pathdesc);
        }

        public void Insert_NewRakuten_Category(string path)
        {
            cdl.Insert_NewRakuten_Category(path);
        }

        public DataTable GetRootPath(string path)
        {
            return cdl.GetRootPath(path);
        }

        public void CategoryUpdate(int childID, String text, String CID, String RID, String RCNo, String Yahoo,String Wowma, String Tennis, String JID, String JCNo, int Serial, String path)
        {
            cdl.CategoryUpdate(childID, text, CID, RID, RCNo, Yahoo, Wowma, Tennis, JID, JCNo, Serial, path);
        }

        public void UpdateSerial(DataTable dt)
        {
            cdl.UpdateSerialno(dt);
        }

        public DataTable SelectAllForSerial(int serialno, int parid)
        {
            return cdl.SelectAllForSerial(serialno, parid);
        }

        public void SerialNoUpdate(int serialno, int id)
        {
            cdl.CatalogSerialnoUpdate(serialno, id);
        }

        public void CategoryDelete(int id)
        {
            cdl.Delete(id);
        }

        public DataTable SelectForCatalogID(int i)
        {
            return cdl.SelectForCatalogID(i);
        }

        public bool Check(String CID)
        {
            int count = cdl.check(CID);
            if (count > 0)
                return true;
            else
                return false;
        }

        public DataTable Search(String CID, String Desc, String start, String enddesc, String fourdesc, String fivedesc)
        {
            return cdl.Search(CID, Desc, start, enddesc, fourdesc, fivedesc);
        }

        public DataTable SearchShopTree(String cID, String cname)
        {
            return cdl.SearchTree(cID, cname);
        }

        public DataTable ExCSV(int catid, int ctrl)
        {
            return cdl.ExCSV(catid, ctrl);
        }

        public DataTable Get_CategoryID(String Id)
        {
            return cdl.GetDirectory_ID(Id);
        }

        public DataTable EXsearchcsv(int id)
        {
            return cdl.Excsvsearch(id);
        }

        public DataTable GetCategoryID(string CategoryNo)
        {
            return cdl.GetCategoryID(CategoryNo);
        }

        public DataTable GetAutoCategoryID()
        {
            return cdl.GetCategoryIDAuto();
        }

        public DataTable getAllParentsbyID(int CID)
        {
            return cdl.getAllParentsbyCID(CID);
        }

        public void Pathupdate(DataTable dt)
        {
            cdl.UpdatePath(dt);
        }

        public DataTable getAllPID(String PID)
        {
            return cdl.getpath(PID);
        }
    }
}
