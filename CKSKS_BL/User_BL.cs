using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ORS_RCM_Common;
using ORS_RCM_DL;
using System.Data;

namespace ORS_RCM_BL
{
    public class User_BL
    {
        User_DL udl;

        public User_BL()
        {
            udl = new User_DL();
        }

        public String Insert(User_entity usentity)
        {

            if (udl.Insert(usentity))
            {
                return "Save Successful !";
            }
            else
            {
                return "Save Fail !";
            }
        }

        public string Update(User_entity usentity)
        {
            if (udl.Update(usentity))
            {
                return "Update Successful !";
            }
            else
            {
                return "Update Fail !";
            }
        }

        public DataTable Search(string name, string loginID, string status)
        {
            return udl.Search(name,loginID,status);
        }



        public DataTable Searchzero(string name, string loginID)
        {
            return udl.Searchzero(name, loginID);
        }



        public User_entity SelectByID(int id)
        {
            udl = new User_DL();
            return  udl.SelectByID(id);
        }

        public DataSet SelectAll()
        {
            udl = new User_DL();
            return  udl.SelectAll();
        }


        public DataTable SelectAllByOne()
        {
            udl = new User_DL();
            return udl.SelectAllByOne();
        }

        public User_entity SelectZeroAll(int id)
        {
            udl = new User_DL();
            return udl.SelectZeroAll(id);
        }


    }
}