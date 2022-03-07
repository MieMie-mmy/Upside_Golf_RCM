using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace ORS_RCM_BL
{
    public class Jisha_Import_BL
    {
        Jisha_Import_DL JishaDL;

        public Jisha_Import_BL()
        {
            JishaDL = new Jisha_Import_DL();
        }

        public void Jisha_Item_Master_Xml(DataTable dt)
        {
            dt.TableName = "test";

            System.IO.StringWriter writer = new System.IO.StringWriter();

            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);

            string result = writer.ToString();

            JishaDL.Jisha_Item_Master_Import_Xml(result);
        }

        public void Jisha_Item_Xml(DataTable dt)
        {
            dt.TableName = "test";

            System.IO.StringWriter writer = new System.IO.StringWriter();

            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);

            string result = writer.ToString();

            JishaDL.Jisha_Item_Import_Xml(result);
        }

        public void Jisha_Item_Category_Xml(DataTable dt)
        {
            dt.TableName = "test";

            System.IO.StringWriter writer = new System.IO.StringWriter();

            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);

            string result = writer.ToString();

            JishaDL.Jisha_Item_Category_Import_Xml(result);
        }

        
    }
}
