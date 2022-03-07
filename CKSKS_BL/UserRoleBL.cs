using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;
using ORS_RCM_Common;
using System.Transactions;

namespace ORS_RCM_BL
{
    public class UserRoleBL
    {

        UserRoleDL user;

        public UserRoleBL()
        {
            user = new UserRoleDL();
        }


        public DataTable MenuSelectAll()
        {
            return user.MenuSelectAll();
        }


        public DataTable SelectByID(int id)
        {
            return user.UserRoleSelectByID(id);
        }

        public void Insert(DataTable userInfo, int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (user.UserRoleDelete(id) == 0)
                {
                    user.UserRoleInsert(userInfo);
                    scope.Complete();
                }
            }
        }

        public bool CanRead(int userID, string pageCode)
        {
            return user.CanRead(userID, pageCode);
        }

        public bool CanSave(int userID, string pageCode)
        {
            return user.CanSave(userID, pageCode);
        }

        public bool CanDelete(int userID, string pageCode)
        {
            return user.CanDelete(userID, pageCode);
        }
        public string SelectName(int id)
        {
            return user.SelectName(id);

        }

        public void Insert(string name, string ID, string pass, int userid)
        {
            //  return
            user.UserInsert(name, ID, pass, userid);
        }

        public DataTable SelectUserRoleID(int MenuID)
        {
            return user.SelectbyUserRoleID(MenuID);
        }

        public DataTable SelectUserInfo(int ID)
        {
            return user.SelectbyUserInfo(ID);
        }

        public int UserInsert(User_entity usentity)
        {
            return user.Insert(usentity);
        }



        public DataTable Duplicate_loginID(string login_ID,int? ID)
        {
            return user.SelectbyDuplicate(login_ID,ID);

        }


        public DataTable Userdt(string login)
        {
            return user.Usertable(login);

        }

    }
    }
     

     