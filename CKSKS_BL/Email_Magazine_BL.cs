using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using ORS_RCM_Common;
using System.Data;

namespace ORS_RCM_BL
{
   public  class Email_Magazine_BL
    {
        Email_Magazine_DL emdl;

        public Email_Magazine_BL()
        {
            emdl = new Email_Magazine_DL();
        }

        public DataTable CreateXmlforEmailMagazine(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            DataTable dtResult = emdl.EmailMagazineXmlInsert(result);
            return dtResult;

        }

        public int Check_MalID(int lid,string mid)
        {
            return emdl.Check_MalID(lid,mid);
        
        }

        public String Insert(Email_Magazine_Entity eme)
        {
            if (emdl.Insert(eme))
            {
                return "Save Successful !";
            }
            else
            {
                return "Save Fail !";
            }
        }

        public String Update(Email_Magazine_Entity eme)
        {
            if (emdl.Update(eme))
            {
                return "Update Successful !";
            }
            else
            {
                return "Update Fail !";
            }
        }

        public DataTable SelectAll()
        {
            return emdl.SelectAll();
        }

        public DataTable Search(Email_Magazine_Entity eme,int pgindex, int psize)
        {
            return emdl.Search(eme, pgindex,psize);
        }

        public Email_Magazine_Entity SelectByID(int id)
        {
            emdl = new Email_Magazine_DL();
            return emdl.SelectByID(id);
        }

        public DataTable SelectByPID(int pid)
        {
            emdl = new Email_Magazine_DL();
            return emdl.SelectByPID(pid);
        }

        public DataTable SelectByCName(String cid)
        {
            emdl = new Email_Magazine_DL();
            return emdl.SelectByCName(cid);
        }
    }
}
