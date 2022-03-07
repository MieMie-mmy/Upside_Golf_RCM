// -----------------------------------------------------------------------
// <copyright file="ShopCategoryBL.cs" company="Microsoft">
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
    public class ShopCategoryBL
    {
        public ShopCategoryDL scdl;

        public ShopCategoryBL()
        {
            scdl = new ShopCategoryDL();
        }

        public Boolean InsertCSV(DataTable dt)
        {
            return scdl.InsertCSV(dt);
        }

        public Boolean UpdateCSV(DataTable dt)
        {
            return scdl.UpdateCSV(dt);
        }

     
    }
}
