// -----------------------------------------------------------------------
// <copyright file="Item_Step1_BL.cs" company="Capital Knowledge">
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
    using ORS_RCM_Common;
    using System.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Item_Step1_BL
    {
        public Item_Step1_DL isdl;
        public Item_Step1_BL()
        {
            isdl = new Item_Step1_DL();
        }

        public Boolean Item_Step1_Update(Item_Step1_Entity ise,DataTable dt)
        {
            return isdl.Item_Step1_Update(ise,dt);
        }
    }
}
