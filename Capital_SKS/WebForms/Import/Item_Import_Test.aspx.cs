/* 
Created By              : Kyaw Thet Paing
Created Date          : 
Updated By             :
Updated Date         :
 Tables using           : Item_Master
                                     -
                                     -
                                  
 *                                  
 * Storedprocedure using:
 *            
 
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Data;
using ORS_RCM_BL;
using System.Transactions;
using System.Data.SqlClient;
using System.Configuration;

namespace ORS_RCM.WebForms.Import
{
    public partial class Item_Import_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public DataTable ChangeItemMasterHeader(DataTable dt)
        {
            dt.Columns["販売管理番号"].ColumnName = "Item_AdminCode";
            dt.Columns["商品番号"].ColumnName = "Item_Code";
            dt.Columns["商品名"].ColumnName = "Item_Name";
            dt.Columns["定価"].ColumnName = "List_Price";
            dt.Columns["販売価格"].ColumnName = "Sale_Price";
            dt.Columns["原価"].ColumnName = "Cost";
            dt.Columns["発売日"].ColumnName = "Release_Date";
            dt.Columns["掲載可能日"].ColumnName = "Post_Available_Date";
            dt.Columns["年度"].ColumnName = "YEAR";
            dt.Columns["シーズン"].ColumnName = "Season";
            dt.Columns["ブランドコード"].ColumnName = "Brand_Code";
            dt.Columns["ブランド名"].ColumnName = "Brand_Name";
            dt.Columns["ヤフーブランドコード"].ColumnName = "Brand_Code_Yahoo";
            dt.Columns["競技コード"].ColumnName = "Competition_Code";
            dt.Columns["競技名"].ColumnName = "Competition_Name";
            dt.Columns["分類コード"].ColumnName = "Class_Code";
            dt.Columns["分類名"].ColumnName = "Class_name";
            dt.Columns["仕入先名"].ColumnName = "Company_name";
            dt.Columns["カタログ情報"].ColumnName = "Catalog_Info";
            dt.Columns["特記フラグ"].ColumnName = "Special_Flag";
            dt.Columns["予約フラグ"].ColumnName = "Reservation_Flag";
            dt.Columns["指示書番号"].ColumnName = "Instruction_No";
            dt.Columns["承認日"].ColumnName = "Approve_Date";
            dt.Columns["備考"].ColumnName = "Remarks";
            dt.Columns["メーカー商品コード"].ColumnName = "Product_Code";
            return dt;
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            String localPath = @"C:\Import_Item\";
            if (uplFile.HasFile)
            {
                String filename = Path.GetFileName(uplFile.PostedFile.FileName);
                uplFile.SaveAs( localPath + filename);
                String path = localPath + Path.GetFileName(filename);
                DataTable dtItem = GlobalUI.CSVToTable(path);
                dtItem = GlobalUI.Remove_Doublecode(dtItem);
                dtItem = ChangeItemMasterHeader(dtItem);
                var option = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                    Timeout=TimeSpan.FromHours(24)
                };

                TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,option);
                using (scope)
                {
                    //do{
                        //DataTable dtTemp=dtItem.Rows.Cast<DataRow>().Take(30000).CopyToDataTable();

                        dtItem.TableName = "test";
                        System.IO.StringWriter writer = new System.IO.StringWriter();
                        dtItem.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                        string xml = writer.ToString();

                        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        SqlConnection connection = new SqlConnection(connectionString);

                        SqlCommand cmd = new SqlCommand("SP_ItemMaster_Import_XML", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        //int count = 0;
                        //while (count < 30000)
                        //{
                        //    if (dtItem.Rows.Count > 0)
                        //        dtItem.Rows.RemoveAt(0);
                        //    else break;
                        //    count++;
                        //}
                    //}while(dtItem.Rows.Count>0);
                    scope.Complete();
                }
                
            }
        }
    }
}