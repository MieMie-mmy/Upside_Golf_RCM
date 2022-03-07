using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_Common;
using ORS_RCM_DL;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_BL
{
    public class Option_BL
    {
         Option_DL   optdl;

        
        public Option_BL()
        {
            optdl = new Option_DL();
        }


        public String Insert(Option_Entity optentity)
        {

            if (optdl.checkGpName(optentity.Option_GroupName,optentity.ID.ToString()))
            {
                return "group name already exists";
            }
            else if (String.IsNullOrWhiteSpace(optentity.Option_GroupName))
            {
                return "fill group name";
            }
            else
            {
                if (optdl.Insert(optentity))
                {
                    return "Save Successful !";
                }
                else
                {
                    return "Save Fail !";
                }
            }
        }

        public String Edit(Option_Entity optentity)
        {

            if (optdl.Edit(optentity))
            {
                return "Save Successful !";
            }
            else
            {
                return "Save Fail !";
            }
        }


        public String Insertnew(Option_Entity optentity)
        {

            if (optdl.InsertNew(optentity))
            {
                return "Save Successful !";
            }
            else
            {
                return "Save Fail !";
            }
        }



        public DataTable Search()
        {
            return optdl.Search();
        }





    }


    }

