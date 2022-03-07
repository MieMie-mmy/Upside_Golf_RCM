// -----------------------------------------------------------------------
// <copyright file="Item_Option_BL.cs" company="Microsoft">
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
    using System.Transactions;
    using ORS_RCM_Common;

    /// <summary>
    ///  
    /// </summary>
    public class Item_Option_BL
    {
        public Item_Option_DL ItemOptionDL;
        
        public Item_Option_BL()
        {
            ItemOptionDL = new Item_Option_DL();
        }
        
        /// <summary>
        /// Test 1
        /// </summary>
        /// <param name="dt">Datatable</param>
        /// <returns></returns>
        public int InsertCSV(DataTable dt)
        {
            DataTable dtErr = new DataTable();
            DataTable dtOK = new DataTable();
            DataRow[] dr = dt.Select("Type=2");
            if (dr.Count() > 0)
                dtOK = dt.Select("Type=2").CopyToDataTable();
            dr = dt.Select("Type=1 OR Type=0");
            if (dr.Count() > 0)
                dtErr = dt.Select("Type=1 OR Type=0").CopyToDataTable();

            int userid =(int) dt.Rows[0]["user_ID"];
            using (TransactionScope scope = new TransactionScope())
            {
                Item_ImportLog_BL iilbl = new Item_ImportLog_BL();
                int logID = iilbl.ImportLogInsert(Convert.ToInt32(EnumBase.Import_Type.ItemOption), dt.Rows.Count, dtErr.Rows.Count,userid);

                String xml = DataTableToXML(dtOK);

                if (ItemOptionDL.InsertCSV(xml))
                {
                    ItemOptionDL.InsertItemImportItemLog(dtOK, logID);
                    ErrorLog_BL errbl = new ErrorLog_BL();
                    //if (errbl.InsertErrorLog(dtErr,logID))
                    if (errbl.InsertErrorLog_Category(dtErr, logID))
                    {
                        scope.Complete();
                        return logID;
                    }
                }
                return -1;
            }
        }

        protected String DataTableToXML(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            return result;
        }

        /// <summary>
        /// check Itemcode is exist in itemMaster
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Check_ItemCode(DataTable dt)
        {
            return ItemOptionDL.Check_ItemCode(dt);
        }

        public DataTable GetErrorTable()
        {
            return ItemOptionDL.GetErrorTable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Item_ID"></param>
        /// <param name="OList"></param>
        /// <returns></returns>
        public bool Insert(int Item_ID, DataTable OList)
        {
            if (ItemOptionDL.Delete(Item_ID))
            {
                if (!ContainColumn("Item_ID", OList))
                {
                    OList.Columns.Add(new DataColumn("Item_ID", typeof(Int32)));
                }
                
                foreach (DataRow row in OList.Rows)
                {
                    row["Item_ID"] = Item_ID;
                }
                if (ItemOptionDL.Insert(OList))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public Boolean Insert_Update(int id, String v1, String n1, String v2, String n2, String v3, String n3)
        {
            return ItemOptionDL.Insert_Update(id, v1, n1, v2, n2, v3, n3);
        }

        public DataTable SelectByItemID(int Item_ID)
        {
            return ItemOptionDL.SelectByItemID(Item_ID);
        }

        public bool DeleteByID(int id)
        {
            return ItemOptionDL.DeleteByID(id);
        }

        /// <summary>
        /// check the column is exist in datatable
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool ContainColumn(string columnName, DataTable table)
        {
            DataColumnCollection columns = table.Columns;

            if (columns.Contains(columnName))
            {
                return true;
            }
            else
                return false;
        }

    }
}
