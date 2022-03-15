﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Upside_Golf_RCM_DL;
using System.Data;
using Upside_Golf_RCM_Common;

namespace Upside_Golf_RCM_BL
{
    public class Template_Detail_BL
    {
        Template_Detail_DL templateDL;

        public Template_Detail_BL()
        {
            templateDL = new Template_Detail_DL();
        }

        public DataTable SelectByItemCode(string Item_Code)
        {
            return templateDL.SelectByItemCode(Item_Code);
        }

        public void Update(string item_code , Template_Detail_Entity tde)
        {
            templateDL.Update(item_code,tde);
        }

    }
}
