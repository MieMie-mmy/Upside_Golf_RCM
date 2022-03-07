using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;
using ORS_RCM_DL;
using System.Transactions;

namespace ORS_RCM_BL
{
    public class Item_Category_BL
    {

        Item_Category_DL itemCategoryDL;

        public Item_Category_BL()
        {
            itemCategoryDL = new Item_Category_DL();
        }

        public Boolean InsertUpdate(int ItemID, int CatID)
        {
            return itemCategoryDL.InsertUpdate(ItemID, CatID);
        }

        public DataTable getAllParentsbyName(int  CategoryName,String Catdesc,String sdesc,String tdesc,String fouthdesc,String fithdesc)
        {
            return itemCategoryDL.getAllParentsbyName(CategoryName,Catdesc,sdesc,tdesc,fouthdesc,fithdesc);
        }

        public DataTable getAllParentsbyCode(String CCode)
        {
            return itemCategoryDL.getAllParentsbyCode(CCode);
        }
        public int InsertCSV(DataTable dt)
        {
            //return itemCategoryDL.InsertCSV(dt);
          
                DataTable dtErr = new DataTable();
                DataTable dtOK = new DataTable();
                DataRow[] dr = dt.Select("Type=2");
                if (dr.Count() > 0)
                    dtOK = dt.Select("Type=2").CopyToDataTable();
                dr = dt.Select("Type=1 OR Type=0");
                if (dr.Count() > 0)
                    dtErr = dt.Select("Type=1 OR Type=0").CopyToDataTable();
                int userid = (int)dt.Rows[0]["user_ID"];
                int count = dtOK.Rows.Count;
                var option = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                    Timeout = TimeSpan.MaxValue
                };
                TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                using (scope)
                {
                    Item_ImportLog_BL iilbl = new Item_ImportLog_BL();

                    int logID = iilbl.ImportLogInsert(Convert.ToInt32(EnumBase.Import_Type.ItemCategory), dt.Rows.Count, dtErr.Rows.Count, userid);

                    #region
                    //if (itemCategoryDL.InsertCSV(dtOK))
                    //{

                    //    DataColumn newColumn = new DataColumn("Item_Import_LogID", typeof(System.Int32));
                    //    newColumn.DefaultValue = logID;
                    //    dtOK.Columns.Add(newColumn);
                    //    itemCategoryDL.InsertItemImportItemLog1(dtOK);
                    //    //itemCategoryDL.InsertItemImportItemLog(dtOK, logID);
                    //    ErrorLog_BL errbl = new ErrorLog_BL();
                    //    DataColumn newColumns = new DataColumn("errLog", typeof(System.Int32));
                    //    newColumns.DefaultValue = logID;
                    //    dtErr.Columns.Add(newColumns);
                    //    if (errbl.InsertErrorLog_Categoryxml(dtErr))
                    //    {
                    //        scope.Complete();
                    //        return logID;
                    //   }
                    //}
                    #endregion

                    DataColumn newColumn = new DataColumn("Item_Import_LogID", typeof(System.Int32));
                    newColumn.DefaultValue = logID;
                    dtOK.Columns.Add(newColumn);
                    itemCategoryDL.InsertItemImportItemLog1(dtOK);

                    ErrorLog_BL errbl = new ErrorLog_BL();
                    DataColumn newColumns = new DataColumn("errLog", typeof(System.Int32));
                    newColumns.DefaultValue = logID;
                    dtErr.Columns.Add(newColumns);
                    errbl.InsertErrorLog_Categoryxml(dtErr);
                    do
                    {
                        DataTable dtTemp = dtOK.Rows.Cast<DataRow>().Take(30000).CopyToDataTable();


                        itemCategoryDL.InsertCSV(dtTemp);


                        count = 0;
                        while (count < 30000)
                        {
                            if (dtOK.Rows.Count > 0)
                                dtOK.Rows.RemoveAt(0);
                            else break;
                            count++;
                        }
                    } while (dtOK.Rows.Count > 0);

                    scope.Complete();
                    return logID;
                }
                // return  -1;
            
            }
       

        public bool Insert(int Item_ID, DataTable CList)
        {
            if (itemCategoryDL.Delete(Item_ID))
            {
                if (!ContainColumn("Item_ID", CList))
                {
                    CList.Columns.Add(new DataColumn("Item_ID", typeof(Int32)));
                }
                if (!ContainColumn("Ctrl_ID", CList))
                {
                    CList.Columns.Add(new DataColumn("Ctrl_ID", typeof(string)));
                }
                foreach (DataRow row in CList.Rows)
                {
                    row["Item_ID"] = Item_ID;
                    row["Ctrl_ID"] = "n";
                }
                if (itemCategoryDL.Insert(CList))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public int CheckDescription(string check,int sn,int option,int parentid,string path)
        {
            return itemCategoryDL.CheckDescription(check,sn,option,parentid,path);
        }

        public DataTable CheckCategory(int Item_ID, DataTable dtcat)
        {
            return itemCategoryDL.CheckCategory(Item_ID, dtcat);
        }

        public DataTable SelectByItemID(int Item_ID)
        {
            return itemCategoryDL.SelectByItemID(Item_ID);
        }

        public DataTable SelectAllRootByItemID(int Item_ID)
        {
            return itemCategoryDL.SelectAllRootByItemID(Item_ID);
        }

        public void ItemImportItemlog(DataTable dt) 
        {
            itemCategoryDL.InsertItem_Import_ItemLog(dt);
        }

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
