using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Smart_Template_Entity
    {
       
            private int id;
            private string templateid;
            private int status;
            private string  template_description;
            private string templatename;
          

            public int ID
            {
                get { return id; }
                set { id = value; }
            }

          
            public string Template_ID
            {
                get { return templateid; }
                set { templateid = value; }
            }

            public int  Status
            {
                get { return  status; }
                set { status = value; }

            }

            public string Templatename 
            {
                get { return templatename; }
                set { templatename = value; }
            }
       

        }
    }

