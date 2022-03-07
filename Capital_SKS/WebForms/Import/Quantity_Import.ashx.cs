using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Transactions;

namespace ORS_RCM.WebForms.Import
{
    /// <summary>
    /// Summary description for Quantity_Import
    /// </summary>
    public class Quantity_Import : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");

            //set the content type, you can either return plain text, html text, or json or other type    
            //context.Response.ContentType = "text/plain";

            //deserialize the object
            //UserInfo objUser = Deserialize<UserInfo>(context);

            //now we print out the value, we check if it is null or not
            //if (objUser != null)
            //{

            //    string vc_user_shohin = objUser.Name;

               //context.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:51768/WebForms/Import/sample.aspx");
                string connect = ConfigurationManager.ConnectionStrings["MakerConnectionString"].ToString();
                SqlConnection connMaker = new SqlConnection(connect);
                SqlDataAdapter da = new SqlDataAdapter("SELECT vc_user_shohin,vc_usize,vc_ucolor,vm_size,vm_color,no_commit_zaiko,no_maker_zaiko, no_dealer_zaiko  from Maker_Information where vc_user_shohin='ass-bau300'", connMaker);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandTimeout = 0;
                DataTable sdt = new DataTable();
                da.SelectCommand.Connection.Open();
                da.Fill(sdt);
                da.SelectCommand.Connection.Close();

                //string json = JsonConvert.SerializeObject(sdt); // Serialization
                //context.Response.Write(json);
                if(sdt.Rows.Count>0)
                {
                    //Quanity_Import(sdt);
                    Inventory(sdt);
                }
            //}
            else
            {
                context.Response.Write("Sorry something goes wrong.");
            }
        }

        public void Inventory(DataTable dt)
        {
            //  DataTable dTable = InventoryCSVToTable(Invpath, filename);
            int count = dt.Rows.Count;
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                Timeout = TimeSpan.MaxValue
            };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                do
                {
                    DataTable dtTemp = dt.Rows.Cast<DataRow>().Take(50000).CopyToDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        //XmlforItem_Import_ItemLog(dtok);
                        CreateXmlforInventory(dt);
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
            //return id;

        }

        public void CreateXmlforInventory(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            result = result.Replace("&#", "$CapitalSports$");
            InvXmlInsert(result);
        }

        public void InvXmlInsert(string xml)
        {
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                SqlConnection connectionString = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand("SP_Item_ThreeQuantity_Update", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public bool Quanity_Import(DataTable dt)
        //{
        //    try
        //    {
        //        string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        //        SqlConnection connectionString = new SqlConnection(connection);
        //        SqlCommand cmd = new SqlCommand("SP_Item_ThreeQuantity_Update", connectionString);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;
        //        SqlDataAdapter sda = new SqlDataAdapter();
        //        string st = dt.Rows[0]["vc_user_shohin"].ToString();
               
        //        cmd.Parameters.Add("@Item_Code", SqlDbType.NVarChar, 100, "vc_user_shohin");
        //        cmd.Parameters.Add("@Size_Code", SqlDbType.NVarChar, 100, "vc_usize");
        //        cmd.Parameters.Add("@Color_Code", SqlDbType.NVarChar, 100, "vc_ucolor");
        //        cmd.Parameters.Add("@Quantity", SqlDbType.Int, 50, "no_commit_zaiko");
        //        cmd.Parameters.Add("@Jisha_Quantity", SqlDbType.Int, 50, "no_dealer_zaiko");
        //        cmd.Parameters.Add("@Maker_Quantity", SqlDbType.Int, 50, "no_maker_zaiko");
        //        //sda.InsertCommand = cmd;
        //        sda.UpdateCommand = cmd;
        //        cmd.Connection.Open();
        //        sda.Update(dt);
        //        cmd.Connection.Close();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}

        //public T Deserialize<T>(HttpContext context)
        //{
        //    //read the json string
        //    string jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();

        //    //cast to specified objectType
        //    var obj = (T)new JavaScriptSerializer().Deserialize<T>(jsonData);

        //    //return the object
        //    return obj;
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}