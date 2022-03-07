using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;

namespace ORS_RCM_BL
{
    public class System_ErrorLogView_BL
    {
        System_ErrorLogView_DL errDl;
        public void UpdateStatus(string id,string status)
        {
            errDl = new System_ErrorLogView_DL();
            errDl.UpdateStatus(id, status);
        }
    }
}
