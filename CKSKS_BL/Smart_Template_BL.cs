using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using System.Data;
using System.Transactions;
using ORS_RCM_Common;


namespace ORS_RCM_BL
{
    public class Smart_Template_BL
    {
        public Smart_Template_DL smart_TempDL;

        public void Insert_SmartTemplate(DataTable dt)
        {
            //dt.Columns["ゼット用項目（PC商品説明文）"].ColumnName = "PC商品説明文"; 
            //dt.Columns["ゼット用項目（PC販売説明文）"].ColumnName = "PC販売説明文";

            //writen by inaoka 2015/04/19 
            try
            {
                if (dt.Columns.Contains("ゼット用項目（PC商品説明文）"))
                {
                    dt.Columns["ゼット用項目（PC商品説明文）"].ColumnName = "PC商品説明文";
                }

                if (dt.Columns.Contains("ゼット用項目（PC販売説明文）"))
                {
                    dt.Columns["ゼット用項目（PC販売説明文）"].ColumnName = "PC販売説明文";
                }


                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();
                smart_TempDL = new Smart_Template_DL();
                smart_TempDL.Insert_SmartTemplate(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Smart_Template_BL()
        {
            smart_TempDL = new Smart_Template_DL();
        }

        public DataTable GetShopTable()
        {
            return smart_TempDL.GetShopList();
        }

        public DataTable GetTextbox()
        {
            return smart_TempDL.GetTextboxList();
        }


        public int Insert(Smart_Template_Entity smtTEntity)
        {

            return smart_TempDL.Insert(smtTEntity);
        }

        public bool Deletedata(int id)
        {
            if (smart_TempDL.Delete(id) == true)
                return true;
            else
                return false;
        }

        public DataTable SelectbyID(int id)
        {
            return smart_TempDL.SelectbyID(id);
        }

        public DataTable SelectByTemplateID(string templateID)
        {
            return smart_TempDL.SelectByTemplateID(templateID);
        }


    }
}
