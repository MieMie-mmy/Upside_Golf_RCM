using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_DL;
using System.IO;
using System.Transactions;

namespace ORS_RCM_BL
{

    public class Import_Item_BL
    {
        Import_Item_DL Imdl;
        Item_ImportLog_BL ItemImbl;
        public Import_Item_BL()
        {
        Imdl = new Import_Item_DL(); 
        }

        public  void CreateXmlforItemmaster(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            Imdl.ItemMasterXmlInsert(result);
        }

        public void XmlforErrorLog(DataTable dt)
        {
            ItemImbl = new Item_ImportLog_BL();
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            ItemImbl.ErrorLogInsertXML(result);
        }

        public void XmlforItem_Import_ItemLog(DataTable dt) 
        {
            ItemImbl = new Item_ImportLog_BL();
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer,XmlWriteMode.WriteSchema,false);
            string result = writer.ToString();
            result = result.Replace("&#", "$CapitalSports$");
            ItemImbl.Item_Import_ItemLogXML(result);    
        }

        public void XmlforMonotaro_Import_ItemLog(DataTable dt)
        {
            ItemImbl = new Item_ImportLog_BL();
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            result = result.Replace("&#", "$CapitalSports$");
            ItemImbl.Monotaro_Item_Import_ItemLogXML(result);    
        }
        public void XmlforDelivery_Import_ItemLog(DataTable dt)
        {
            ItemImbl = new Item_ImportLog_BL();
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            result = result.Replace("&#", "$CapitalSports$");
            ItemImbl.Delivery_Item_Import_ItemLogXML(result);
        }
        
        
        public void Import_RakutenSetting(String xml)
        {
            Imdl = new Import_Item_DL();
            Imdl.Import_RakutenSetting(xml);
        }

        public int Itemmaster(DataTable dt ) 
        {
            ItemImbl = new Item_ImportLog_BL();
            int id;
            int count = dt.Rows.Count;
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,//without lock
                Timeout=TimeSpan.MaxValue
            };

            TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,option);
                 using (scope)
                    {   
                        DataRow[] drs = dt.Select("Type = 5");//get error record
                        int userid = 1;
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Columns.Contains("User_ID"))
                                userid = Convert.ToInt32(dt.Rows[0]["User_ID"].ToString());
                        }

                        int errcount = drs.Count();
                        id = ItemImbl.ImportLogInsert(0, count, errcount, userid);//insert to error log table and return log id
                                                                 
                        DataColumn newColumn = new DataColumn("Item_Import_LogID", typeof(System.String));
                        newColumn.DefaultValue = id;

                        if (!dt.Columns.Contains("Item_Import_LogID"))
                            dt.Columns.Add(newColumn);//add log id to  datatable
                 
                        DataTable dtok = new DataTable();
                        DataTable dterror = new DataTable();
                        DataRow[] dr = dt.Select("Type = 5");//error record
                         if(dr.Count()>0)
                         {
                             dterror = dt.Select("Type=5").CopyToDataTable();//error record table
                             if (dterror != null && dterror.Rows.Count > 0)
                             {
                                 for (int i = 0; i < dterror.Rows.Count; i++)
                                 {
                                     dterror.Rows[i]["Type"] = 0;
                                 }
                             }
                             XmlforErrorLog(dterror);//insert error record                     
                         }

                         do
                         {
                             //insert 50000 record in 1 time
                             DataTable dtTemp = dt.Rows.Cast<DataRow>().Take(50000).CopyToDataTable();

                             dr = dtTemp.Select("Type = 6");//check ok record
                             if (dr.Count() > 0)
                             {
                                 dtok = dtTemp.Select("Type=6").CopyToDataTable();
                                 XmlforItem_Import_ItemLog(dtok);//insert to log table
                                 CreateXmlforItemmaster(dtok);//insert to item master table
                             }

                             //delete inserte finished 50000 record
                             count = 0;
                             while (count < 50000)
                             {
                                 if (dt.Rows.Count > 0)
                                     dt.Rows.RemoveAt(0);
                                 else break;
                                 count++;
                             }
                         } while (dt.Rows.Count > 0);

                        scope.Complete();
                    }

                 return id;
        
        }

        public  void CreateXmlforSKU(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            Imdl.Insert(result);
        }

        public void CreateXmlforDelivery(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            Imdl.DeliveryUpdate (result);
        }

        public void CreateXmlforMonotarou(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            Imdl.MonotaroInsert(result);
        }

        public DataTable ItemInsertTagID(string Item_Code, string Size_Name, string Color_Name, string Rakuten_CategoryID, string Tag_Name1, string Tag_Name2, string Tag_Name3, string Tag_Name4, string Tag_Name5, string Tag_Name6, string Tag_Name7, string Tag_Name8)
        {
            Imdl = new Import_Item_DL();
            return Imdl.ItemInsertTagID(Item_Code, Size_Name, Color_Name, Rakuten_CategoryID, Tag_Name1, Tag_Name2, Tag_Name3, Tag_Name4, Tag_Name5, Tag_Name6, Tag_Name7, Tag_Name8);
        }
        public int SKU(DataTable dt) 
       {
           ItemImbl = new Item_ImportLog_BL();
        
           int id;
           int count = dt.Rows.Count;
           var option = new TransactionOptions
           {
               IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,//without lock
               Timeout = TimeSpan.MaxValue
           };
           using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
           {
               DataRow[] drs = dt.Select("Type = 5");//get error record

               int userid = 1;
               if (dt.Rows.Count > 0)
               {
                   if (dt.Columns.Contains("User_ID"))
                       userid = Convert.ToInt32(dt.Rows[0]["User_ID"].ToString());
               }

               int errcount = drs.Count();
               id = ItemImbl.ImportLogInsert(1, count, errcount, userid);//insert to error log table and return log id
              
               DataColumn newColumn = new DataColumn("Item_Import_LogID", typeof(System.String));
                newColumn.DefaultValue = id;
                dt.Columns.Add(newColumn);
                DataTable dtok = new DataTable();
                DataTable dterror = new DataTable();
                DataRow[] dr = dt.Select("Type = 5");//error record table
                if (dr.Count() > 0)
                {
                    dterror = dt.Select("Type=5").CopyToDataTable();
                    if (dterror != null && dterror.Rows.Count > 0)
                    {
                        for (int i = 0; i < dterror.Rows.Count; i++)
                        {
                            dterror.Rows[i]["Type"] = 1;
                        }
                    }
                 
                    XmlforErrorLog(dterror);//insert error record
                }

                do{
                    //insert 50000 record in 1 time
                    DataTable dtTemp=dt.Rows.Cast<DataRow>().Take(50000).CopyToDataTable();

                    dr = dtTemp.Select("Type = 6");//check ok record
                    if (dr.Count() > 0)
                    {
                        dtok = dtTemp.Select("Type=6").CopyToDataTable();
                        XmlforItem_Import_ItemLog(dtok);//insert to log table
                        CreateXmlforSKU(dtok);//insert to item table
                    }

                    //delete inserte finished 50000 record
                    count = 0;
                    while (count < 50000)
                    {
                        if (dt.Rows.Count > 0)
                            dt.Rows.RemoveAt(0);
                        else break;
                        count++;
                    }
                } while (dt.Rows.Count > 0);
              
               scope.Complete();
           }
           return id;
       }

        public int Monotaro(DataTable dt)
        {
            ItemImbl = new Item_ImportLog_BL();

            int id;
            int count = dt.Rows.Count;
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,//without lock
                Timeout = TimeSpan.MaxValue
            };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                DataRow[] drs = dt.Select("Type = 5");//get error record
                int userid = 1;
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("User_ID"))
                        userid = Convert.ToInt32(dt.Rows[0]["User_ID"].ToString());
                }
                int errcount = drs.Count();
                id = ItemImbl.ImportLogInsert(9, count, errcount, userid);//insert to error log table and return log id
                DataColumn newColumn = new DataColumn("Item_Import_LogID", typeof(System.String));
                newColumn.DefaultValue = id;
                dt.Columns.Add(newColumn);
                DataTable dtok = new DataTable();
                DataTable dterror = new DataTable();
                DataRow[] dr = dt.Select("Type = 5");//error record table
                if (dr.Count() > 0)
                {
                    dterror = dt.Select("Type=5").CopyToDataTable();
                    if (dterror != null && dterror.Rows.Count > 0)
                    {
                        for (int i = 0; i < dterror.Rows.Count; i++)
                        {
                            dterror.Rows[i]["Type"] = 9;
                        }
                    }
                    XmlforErrorLog(dterror);//insert error record
                }
                do
                {
                    //insert 50000 record in 1 time
                    DataTable dtTemp = dt.Rows.Cast<DataRow>().Take(50000).CopyToDataTable();
                    dr = dtTemp.Select("Type = 6");//check ok record
                    if (dr.Count() > 0)
                    {
                        dtok = dtTemp.Select("Type=6").CopyToDataTable();
                        XmlforMonotaro_Import_ItemLog(dtok);//insert to log table
                        CreateXmlforMonotarou(dtok);//insert to item table
                    }
                    //delete inserte finished 50000 record
                    count = 0;
                    while (count < 50000)
                    {
                        if (dt.Rows.Count > 0)
                            dt.Rows.RemoveAt(0);
                        else break;
                        count++;
                    }
                } while (dt.Rows.Count > 0);

                scope.Complete();
            }
            return id;

        }
        public int Inventory(DataTable dt)
        {
            ItemImbl = new Item_ImportLog_BL();
            //  DataTable dTable = InventoryCSVToTable(Invpath, filename);
            int id;
            int count = dt.Rows.Count;
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                Timeout = TimeSpan.MaxValue
            };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                DataRow[] drs = dt.Select("Type = 5");
                int userid = 1;
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("User_ID"))
                        userid = Convert.ToInt32(dt.Rows[0]["User_ID"].ToString());
                }

                int errcount = drs.Count();
                id = ItemImbl.ImportLogInsert(2, count, errcount, userid);

                DataColumn newColumn = new DataColumn("Item_Import_LogID", typeof(System.String));

                newColumn.DefaultValue = id;

                dt.Columns.Add(newColumn);

                DataTable dtok = new DataTable();
                DataTable dterror = new DataTable();
                DataRow[] dr = dt.Select("Type = 5");
                if (dr.Count() > 0)
                {
                    dterror = dt.Select("Type=5").CopyToDataTable();
                    if (dterror != null && dterror.Rows.Count > 0)
                    {
                        for (int i = 0; i < dterror.Rows.Count; i++)
                        {
                            dterror.Rows[i]["Type"] = 2;
                        }
                    }
                    XmlforErrorLog(dterror);
                }


                do
                {
                    DataTable dtTemp = dt.Rows.Cast<DataRow>().Take(50000).CopyToDataTable();

                    dr = dtTemp.Select("Type = 6");
                    if (dr.Count() > 0)
                    {
                        dtok = dtTemp.Select("Type=6").CopyToDataTable();
                        XmlforItem_Import_ItemLog(dtok);
                        for (int i = 0; i < dtok.Rows.Count; i++)
                        {
                            if (String.IsNullOrWhiteSpace(dtok.Rows[i]["Quantity"].ToString()))
                            {

                                dtok.Rows[i]["Quantity"] = 999;
                            }
                        }
                    
                        CreateXmlforInventory(dtok);
                    }

                    count = 0;
                    while (count < 50000)
                    {
                        if (dt.Rows.Count > 0)
                            dt.Rows.RemoveAt(0);
                        else break;
                        count++;
                    }
                } while (dt.Rows.Count > 0);

                //CreateXmlforInventory(dt);
                scope.Complete();
            }
            return id;

        }

        public int Delivery(DataTable dt)
        {
            ItemImbl = new Item_ImportLog_BL();

            int id;
            int count = dt.Rows.Count;
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,//without lock
                Timeout = TimeSpan.MaxValue
            };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                DataRow[] drs = dt.Select("Type = 5");//get error record

                int userid = 1;
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("User_ID"))
                        userid = Convert.ToInt32(dt.Rows[0]["User_ID"].ToString());
                }

                int errcount = drs.Count();
                id = ItemImbl.ImportLogInsert(1, count, errcount, userid);//insert to error log table and return log id

                DataColumn newColumn = new DataColumn("Item_Import_LogID", typeof(System.String));
                newColumn.DefaultValue = id;
                dt.Columns.Add(newColumn);
                DataTable dtok = new DataTable();
                DataTable dterror = new DataTable();
                DataRow[] dr = dt.Select("Type = 5");//error record table
                if (dr.Count() > 0)
                {
                    dterror = dt.Select("Type=5").CopyToDataTable();
                    if (dterror != null && dterror.Rows.Count > 0)
                    {
                        for (int i = 0; i < dterror.Rows.Count; i++)
                        {
                            dterror.Rows[i]["Type"] = 1;
                        }
                    }

                    XmlforErrorLog(dterror);//insert error record
                }

                do
                {
                    //insert 50000 record in 1 time
                    DataTable dtTemp = dt.Rows.Cast<DataRow>().Take(50000).CopyToDataTable();

                    dr = dtTemp.Select("Type = 6");//check ok record
                    if (dr.Count() > 0)
                    {
                        dtok = dtTemp.Select("Type=6").CopyToDataTable();
                        //XmlforDelivery_Import_ItemLog(dtok);//insert to log table
                        CreateXmlforDelivery(dtok);//insert to item table
                    }

                    //delete inserte finished 50000 record
                    count = 0;
                    while (count < 50000)
                    {
                        if (dt.Rows.Count > 0)
                            dt.Rows.RemoveAt(0);
                        else break;
                        count++;
                    }
                } while (dt.Rows.Count > 0);

                scope.Complete();
            }
            return id;
        }
      
        public DataTable InventoryCSVToTable(string filePath, string fileName)
        {          
                StreamReader oStreamReader = new StreamReader(filePath, Encoding.GetEncoding(932));
                 DataTable oDataTable = null;
                int RowCount = 0;
                string[] ColumnNames = null;
                string[] oStreamDataValues = null;
                //using while loop read the stream data till end
                while (!oStreamReader.EndOfStream)
                {
                    String oStreamRowData = oStreamReader.ReadLine().Trim();
                    oStreamRowData += ",CSV_FileName";
                    if (oStreamRowData.Length > 0)
                    {
                        oStreamDataValues = oStreamRowData.Split(',');
                        //Bcoz the first row contains column names, we will poluate 
                        //the column name by
                        //reading the first row and RowCount-0 will be true only once
                        if (RowCount == 0)
                        {
                            RowCount = 1;
                            ColumnNames = oStreamRowData.Split(',');

                            oDataTable = new DataTable();
                            DataColumn oDataColumn;
                            //using foreach looping through all the column names
                            foreach (string csvcolumn in ColumnNames)
                            {

                                if (csvcolumn == "Original_Quantity")
                                {
                                    oDataColumn = new DataColumn(csvcolumn, typeof(string));
                                    //setting the default value of int to newly created column
                                   // oDataColumn.DefaultValue = 0;
                                }
                                else
                                {
                                    oDataColumn = new DataColumn(csvcolumn, typeof(string));
                                    //setting the default value of empty.string to newly created column
                                  //  oDataColumn.DefaultValue = string.Empty;
                                }
                                //adding the newly created column to the table
                                oDataTable.Columns.Add(oDataColumn);
                            }
                        }
                        else
                        {
                            // ColumnNames = oStreamRowData.Split(',');
                            //creates a new DataRow with the same schema as of the oDataTable 
                            DataRow oDataRow = oDataTable.NewRow();

                            //using foreach looping through all the column names
                            for (int i = 0; i < ColumnNames.Length; i++)
                            {
                                if (ColumnNames[i] == "Original_Quantity")
                                {
                                    oStreamDataValues[i] = oStreamDataValues[i].Replace("\"", "");//Remove double code(") in value
                                    if (!String.IsNullOrWhiteSpace(oStreamDataValues[i].ToString()))
                                    {
                                        try
                                        {
                                            //oDataRow[ColumnNames[i]] = oStreamDataValues[i] == null ?  0 : int.Parse(oStreamDataValues[i].ToString());
                                            oDataRow[ColumnNames[i]] = oStreamDataValues[i] ==null?null: oStreamDataValues[i].ToString();
                                        }
                                        catch (Exception ex)
                                        {
                                            oStreamDataValues[i] = oStreamDataValues[i].ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    oStreamDataValues[i] = oStreamDataValues[i].Replace("\"", "");//Remove double code(") in value
                                    oDataRow[ColumnNames[i]] = oStreamDataValues[i] == null ? string.Empty : oStreamDataValues[i].ToString();
                                }
                                if (ColumnNames[i] == "CSV_FileName")
                                {
                                    oStreamDataValues[i] = oStreamDataValues[i].Replace("\"", "");//Remove double code(") in value
                                    if (!String.IsNullOrWhiteSpace(fileName))
                                    {
                                        oDataRow[ColumnNames[i]] = fileName;
                                    }
                                }
                            }
                            //adding the newly created row with data to the oDataTable  
                            oDataTable.Rows.Add(oDataRow);
                        }
                    }
                }
                //close the oStreamReader object
                oStreamReader.Close();
                //release all the resources used by the oStreamReader object
                oStreamReader.Dispose();

                return oDataTable;
            
        }

        public void CreateXmlforInventory(DataTable dt)
        {

            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            Imdl.InvXmlInsert(result);
        }

        public void XmlforSmart_Import_ItemLog(DataTable dt)
        {
            ItemImbl = new Item_ImportLog_BL();
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();

            ItemImbl.SmartItem_Import_ItemLogXML(result);
        }

        //written by aam
        public void SportsPlaza_Rakuten_Item_Category_Import_Xml(String xml)
        {
            Imdl.SportsPlaza_Rakuten_Item_Category_Import_Xml(xml);
        }

        public DataTable Check_SportsPlaza_Rakuten_Item_Category(string option)
        {
            return Imdl.Check_SportsPlaza_Rakuten_Item_Category(option);
        }

        public void Delete_SportsPlaza_Rakuten_Item_Category()
        {
            Imdl.Delete_SportsPlaza_Rakuten_Item_Category();
        }

        public void QuantityAdjust(DataTable dt)
        {
            ItemImbl = new Item_ImportLog_BL();
            dt.TableName = "test";               
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            Imdl.UpdateQuantity(result);
        }

        public DataTable ItemCode_Check(String ItemCode)
        {
           return Imdl.ItemCode_Check(ItemCode);
        }
        public DataSet ErrorCountTagID(String Rakuten_CategoryID, string Tag_Name1, string Tag_Name2, string Tag_Name3, string Tag_Name4, string Tag_Name5, string Tag_Name6, string Tag_Name7, string Tag_Name8)
        {
            return Imdl.ErrorCountTagID(Rakuten_CategoryID, Tag_Name1, Tag_Name2, Tag_Name3, Tag_Name4, Tag_Name5, Tag_Name6, Tag_Name7, Tag_Name8);
        }
        public DataTable ItemAdminCode_Select()
        {
            return Imdl.ItemAdminCode_Select();
        }
    }
}
