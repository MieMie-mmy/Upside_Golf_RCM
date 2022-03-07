using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORS_RCM_DL;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace ORS_RCM_BL
{
    public class Log_Exhibition_BL
    {
        Log_Exhibition_DL Log_Exhibit_DL;

        public Log_Exhibition_BL()
        {
            Log_Exhibit_DL = new Log_Exhibition_DL();
        }

        public void SaveLogExhibition(DataTable dt, string list, int shop_id)
        {

            try
            {

                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                Log_Exhibit_DL.SaveLogExhibition(result, list, shop_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveLogExhibitionItem(DataTable dt)
        {
            try
            {
                dt.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();

                Log_Exhibit_DL.SaveLogExhibitionItem(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveLogExhibitionSelect(string list)
        {
            try
            {
                Log_Exhibit_DL.SaveLogExhibitionSelect(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveLogExhibitionSelect(string list, int shop_id)
        {
            try
            {
                Log_Exhibit_DL.SaveLogExhibitionSelect(list, shop_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveLogExhibitionCategory(string list)
        {
            try
            {
                Log_Exhibit_DL.SaveLogExhibitionCategory(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveLogExhibitionCategory(string list, int shop_id)
        {
            try
            {
                Log_Exhibit_DL.SaveLogExhibitionCategory(list, shop_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeIsGeneratedCSVFlag(int Exhibit_ID, int Item_ID, int Shop_ID)
        {
            try
            {
                Log_Exhibit_DL.ChangeFlagLogExhition(Exhibit_ID, Item_ID, Shop_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void SaveExhibitionData(DataTable dt, int shop_id, string filename)
        {
            try
            {
                if (filename.Contains("item-cat"))
                {
                    dt.Columns["商品管理番号（商品URL）"].ColumnName = "Item_Code";
                    dt.Columns["1ページ複数形式"].ColumnName = "Multiple_Format";

                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();

                    Log_Exhibit_DL.SaveExhibitionCategoryFile(result, shop_id);
                }
                else if (filename.Contains("item"))
                {
                    dt.Columns["ヘッダー・フッター・レフトナビ"].ColumnName = "Header_Footer";
                    dt.Columns["共通説明文（小）"].ColumnName = "Common_Description1";
                    dt.Columns["共通説明文（大）"].ColumnName = "Common_Description2";

                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();

                    Log_Exhibit_DL.SaveExhibitionItemFile(result, shop_id);
                }
                else if (filename.Contains("select"))
                {
                    dt.Columns["商品管理番号（商品URL）"].ColumnName = "Item_Code";
                    dt.Columns["Select/Checkbox用項目名"].ColumnName = "Option_Name";
                    dt.Columns["Select/Checkbox用選択肢"].ColumnName = "Option_Value";

                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();

                    Log_Exhibit_DL.SaveExhibitionSelectFile(result, shop_id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExhibitionDeleteData(int eid)
        {
            try
            {
                Log_Exhibit_DL.SaveExhibitionDeleteData(eid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExhibitionDataYahoo(DataTable dt, int shop_id, string filename)
        {
            try
            {
                if (filename.Contains("data_add"))
                {
                    //dt.Columns["ヘッダー・フッター・レフトナビ"].ColumnName = "Header_Footer";
                    //dt.Columns["共通説明文（小）"].ColumnName = "Common_Description1";
                    //dt.Columns["共通説明文（大）"].ColumnName = "Common_Description2";

                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();

                    Log_Exhibit_DL.SaveExhibitionItemFileYahoo(result, shop_id);
                }
                else if (filename.Contains("quantity"))
                {
                    //dt.Columns["商品管理番号（商品URL）"].ColumnName = "Item_Code";
                    //dt.Columns["Select/Checkbox用項目名"].ColumnName = "Option_Name";
                    //dt.Columns["Select/Checkbox用選択肢"].ColumnName = "Option_Value";

                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();

                    Log_Exhibit_DL.SaveExhibitionSelectFileYahoo(result, shop_id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExhibitionDataPonpare(DataTable dt, int shop_id, string filename)
        {
            try
            {
                if (filename.Contains("category"))
                {
                    //dt.Columns["1ページ複数形式"].ColumnName = "Multiple_Format";

                    dt.Columns["商品管理ID（商品URL）"].ColumnName = "Item_Code";

                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();

                    Log_Exhibit_DL.SaveExhibitionCategoryFilePonpare(result, shop_id);
                }
                else if (filename.Contains("item"))
                {
                    dt.Columns["独自送料グループ(1)"].ColumnName = "Shipping_Group1";
                    dt.Columns["独自送料グループ(2)"].ColumnName = "Shipping_Group2";
                    dt.Columns["商品説明(1)"].ColumnName = "Item_Description_PC";
                    dt.Columns["商品説明(2)"].ColumnName = "Sale_Description_PC";
                    dt.Columns["商品説明(テキストのみ)"].ColumnName = "Product_Description";
                    dt.Columns["商品説明(スマートフォン用)"].ColumnName = "Smart_Template";

                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();

                    Log_Exhibit_DL.SaveExhibitionItemFilePonpare(result, shop_id);
                }
                else if (filename.Contains("option"))
                {
                    //dt.Columns["Select/Checkbox用項目名"].ColumnName = "Option_Name";
                    //dt.Columns["Select/Checkbox用選択肢"].ColumnName = "Option_Value";
                    dt.Columns["商品管理ID（商品URL）"].ColumnName = "Item_Code";

                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();

                    Log_Exhibit_DL.SaveExhibitionSelectFilePonpare(result, shop_id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExhibitionDataJisha(DataTable dt, int shop_id, string filename)
        {
            try
            {
                if (filename.Contains("item-cat"))
                {
                    dt.Columns["商品管理番号（商品URL）"].ColumnName = "Item_Code";
                    //dt.Columns["1ページ複数形式"].ColumnName = "Multiple_Format";

                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();

                    Log_Exhibit_DL.SaveExhibitionCategoryFileJisha(result, shop_id);
                }
                else if (filename.Contains("item"))
                {
                    dt.Columns["ヘッダー・フッター・レフトナビ"].ColumnName = "Header_Footer";
                    dt.Columns["共通説明文（小）"].ColumnName = "Common_Description1";
                    dt.Columns["共通説明文（大）"].ColumnName = "Common_Description2";

                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();

                    Log_Exhibit_DL.SaveExhibitionItemFileJisha(result, shop_id);
                }
                else if (filename.Contains("select"))
                {
                    dt.Columns["商品管理番号（商品URL）"].ColumnName = "Item_Code";
                    dt.Columns["Select/Checkbox用項目名"].ColumnName = "Option_Name";
                    dt.Columns["Select/Checkbox用選択肢"].ColumnName = "Option_Value";

                    dt.TableName = "test";
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    string result = writer.ToString();

                    Log_Exhibit_DL.SaveExhibitionSelectFileJisha(result, shop_id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectLogExhibitionRakutenData(int eid, int sid, string option)
        {
            try
            {
                return Log_Exhibit_DL.SelectLogExhibitionRakutenData(eid, sid, option);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectLogExhibitionPonpareData(int eid, int sid, string option)
        {
            try
            {
                return Log_Exhibit_DL.SelectLogExhibitionPonpareData(eid, sid, option);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectLogExhibitionYahooData(int eid, int sid, string option)
        {
            try
            {
                return Log_Exhibit_DL.SelectLogExhibitionYahooData(eid, sid, option);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectLogExhibitionJishaData(int eid, int sid, string option)
        {
            try
            {
                return Log_Exhibit_DL.SelectLogExhibitionJishaData(eid, sid, option);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeDailyDeliveryFlag(DataTable dtItemCode)
        {
            try
            {
                dtItemCode.TableName = "test";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dtItemCode.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();
                Log_Exhibit_DL.ChangeDailyDeliveryFlag(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
