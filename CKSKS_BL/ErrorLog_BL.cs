// -----------------------------------------------------------------------
// <copyright file="ErrorLog_BL.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ORS_RCM_BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ORS_RCM_DL;
    using System.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ErrorLog_BL
    {
        ErrorLog_DL errdl;

        public ErrorLog_BL()
        {
            errdl = new ErrorLog_DL();
        }

        //public Boolean InsertErrorLog(DataTable dt,int errLog)
        //{
        //    return errdl.InsertErrorLog(dt,errLog);
        //}

        public Boolean InsertErrorLog_Category(DataTable dt, int errLog)
        {
            return errdl.InsertErrorLog_Category(dt, errLog);
        }

        public Boolean InsertErrorLog_Categoryxml(DataTable dt)
        {
            try
            {
                return errdl.InsertErrorLog_ProductXML(dt);
            }
            catch (Exception ex) 
            { throw ex; }
        }
        public Boolean InsertErrorLog_Productxml(DataTable dt)
        {
            try
            {
                return errdl.InsertErrorLog_ProductXML(dt);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
