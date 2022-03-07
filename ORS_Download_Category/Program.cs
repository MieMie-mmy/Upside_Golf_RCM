using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
using ClosedXML.Excel;

namespace ORS_Download_Category
{
    public class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        static SqlConnection GetConnection()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }  

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "ORS_Download_Category";
                DataTable dtAPIkey = GetAPIKey();
                GetCategory(dtAPIkey);
            }
            catch(Exception ex)
            {
                String connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SP_Insert_SYS_Error_Log", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", -1);
                cmd.Parameters.AddWithValue("@ErrorDetail", "SKS Inventory" + ex.ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public static void GetCategory(DataTable dtAPIKey)
        {
            if (dtAPIKey != null && dtAPIKey.Rows.Count > 0)
            {
                for (int i = 0; i < dtAPIKey.Rows.Count; i++)
                {
                    string apiSecret = dtAPIKey.Rows[i]["APISecret"].ToString();
                    string apiKey = dtAPIKey.Rows[i]["APIKey"].ToString();
                    int shopid = Convert.ToInt16(dtAPIKey.Rows[i]["ID"]);
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.rms.rakuten.co.jp/es/1.0/categoryapi/shop/categorysets/get");
                    request.Method = "GET";
                    String encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(apiSecret + ":" + apiKey));
                    request.Headers.Add("Authorization", "ESA " + encoded);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    string xml = string.Empty;
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        xml = reader.ReadToEnd();
                    }
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(xml);
                    XmlNode node = xDoc.SelectSingleNode("result/categorysetsGetResult/categorySetList");
                    if (node != null)
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml(new XmlNodeReader(node));
                        string categoryId = string.Empty;
                        System.Data.DataTable dt = ds.Tables[0];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            string catsetid = dt.Rows[j]["categorySetId"].ToString();
                            GetCategories(apiSecret, apiKey, catsetid, shopid);
                        }
                    }
                }
            }
        }

        public static  void GetCategories(string secret, string key, string CatsetNo, int shopid)
        {
            if (CatsetNo != "-9999")
            {
                string path = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.rms.rakuten.co.jp/es/1.0/categoryapi/shop/categories/get?categorySetManageNumber=" + CatsetNo);
                request.Method = "GET";
                String encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(secret + ":" + key));
                request.Headers.Add("Authorization", "ESA " + encoded);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string xml = string.Empty;
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    xml = reader.ReadToEnd();
                }
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xml);
                XmlNode node = xDoc.SelectSingleNode("result/categoriesGetResult/categoryList");
                if (node != null)
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(new XmlNodeReader(node));
                    string categoryId = string.Empty;
                    System.Data.DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        String[] colTypeCheck = { "categoryLevel" };
                        System.Data.DataTable dtcopy = AddNewColumn(dt, shopid);
                        System.Data.DataTable dtcheck = CheckCategory(dtcopy, colTypeCheck);
                        UpdateCategory(dtcheck, CatsetNo);
                        path = Export(dtcheck, CatsetNo, shopid);
                    }
                }
            }
        }

        public static  void UpdateCategory(DataTable dt, string catsetno)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_Update_Category_Path_Import_XML", connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = result;
                cmd.Parameters.AddWithValue("@CatSetNo", catsetno);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        public static  DataTable AddNewColumn(DataTable dt, int shopid)
        {
            DataColumn newcol = new DataColumn("categorypath", typeof(String));
            newcol.DefaultValue = "";
            dt.Columns.Add(newcol);//add check column to datatable that show error or not
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.AutoIncrement = true;
            column.ColumnName = "No";
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            DataColumn colnew = new DataColumn("shopid", typeof(int));
            dt.Columns.Add(colnew);
            System.Data.DataTable dtcopy = new System.Data.DataTable("table");
            dtcopy.Columns.Add(column);
            dtcopy.Merge(dt);
            foreach (DataRow d in dtcopy.Rows)
            {
                d["shopid"] = shopid;
            }
            return dtcopy;
        }

        public static DataTable CheckCategory(DataTable dt, String[] col)
        {
            try
            {
                System.Data.DataTable dtcat = new System.Data.DataTable();
                System.Data.DataTable dtreal = new System.Data.DataTable();
                System.Data.DataTable dtreal1 = new System.Data.DataTable();
                System.Data.DataTable dtreal2 = new System.Data.DataTable();
                System.Data.DataTable dtreal3 = new System.Data.DataTable();
                System.Data.DataTable dtnext = new System.Data.DataTable();
                System.Data.DataTable dtnext1 = new System.Data.DataTable();
                System.Data.DataTable dtnext2 = new System.Data.DataTable();
                System.Data.DataTable dtnext3 = new System.Data.DataTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int n = i;
                    for (int j = 0; j < col.Length; j++)
                    {
                        int countno = Convert.ToInt16(dt.Rows[i][col[j]]);
                        if (countno == 1)
                        {
                            dt.Rows[i]["categorypath"] = dt.Rows[i]["name"] + "\\";
                        }
                        else
                        {
                            dtcat = dt.AsEnumerable().OrderBy(o => o.Field<int>("No")).Take(n).CopyToDataTable();
                            if (countno == 2)
                            {
                                int count = countno - 1;
                                DataRow[] drcat = dtcat.Select("categoryLevel =" + count);
                                if (drcat.Count() > 0)
                                {
                                    dtreal = dtcat.Select("categoryLevel =" + count).CopyToDataTable();
                                    dtnext = dtreal.AsEnumerable().OrderByDescending(o => o.Field<int>("No")).Take(1).CopyToDataTable();
                                    dt.Rows[i]["categorypath"] = dtnext.Rows[0]["name"] + "\\" + dt.Rows[i]["name"] + "\\";
                                }
                            }
                            else if (countno == 3)
                            {
                                int count = countno - 2;
                                DataRow[] drcat = dtcat.Select("categoryLevel =" + count);
                                if (drcat.Count() > 0)
                                {
                                    dtreal = dtcat.Select("categoryLevel =" + count).CopyToDataTable();
                                    dtnext = dtreal.AsEnumerable().OrderByDescending(o => o.Field<int>("No")).Take(1).CopyToDataTable();
                                }
                                count = countno - 1;
                                DataRow[] drcat1 = dtcat.Select("categoryLevel =" + count);
                                if (drcat1.Count() > 0)
                                {
                                    dtreal1 = dtcat.Select("categoryLevel =" + count).CopyToDataTable();
                                    dtnext1 = dtreal1.AsEnumerable().OrderByDescending(o => o.Field<int>("No")).Take(1).CopyToDataTable();
                                    dt.Rows[i]["categorypath"] = dtnext.Rows[0]["name"] + "\\" + dtnext1.Rows[0]["name"] + "\\" + dt.Rows[i]["name"] + "\\";
                                }
                            }
                            else if (countno == 4)
                            {
                                int count = countno - 3;
                                DataRow[] drcat = dtcat.Select("categoryLevel =" + count);
                                if (drcat.Count() > 0)
                                {
                                    dtreal = dtcat.Select("categoryLevel =" + count).CopyToDataTable();
                                    dtnext = dtreal.AsEnumerable().OrderByDescending(o => o.Field<int>("No")).Take(1).CopyToDataTable();
                                }
                                count = countno - 2;
                                DataRow[] drcat1 = dtcat.Select("categoryLevel =" + count);
                                if (drcat1.Count() > 0)
                                {
                                    dtreal1 = dtcat.Select("categoryLevel =" + count).CopyToDataTable();
                                    dtnext1 = dtreal1.AsEnumerable().OrderByDescending(o => o.Field<int>("No")).Take(1).CopyToDataTable();
                                }
                                count = countno - 1;
                                DataRow[] drcat2 = dtcat.Select("categoryLevel =" + count);
                                if (drcat2.Count() > 0)
                                {
                                    dtreal2 = dtcat.Select("categoryLevel =" + count).CopyToDataTable();
                                    dtnext2 = dtreal2.AsEnumerable().OrderByDescending(o => o.Field<int>("No")).Take(1).CopyToDataTable();
                                    dt.Rows[i]["categorypath"] = dtnext.Rows[0]["name"] + "\\" + dtnext1.Rows[0]["name"] + "\\" + dtnext2.Rows[0]["name"] + "\\" + dt.Rows[i]["name"] + "\\";
                                }
                            }
                            else if (countno == 5)
                            {
                                int count = countno - 4;
                                DataRow[] drcat = dtcat.Select("categoryLevel =" + count);
                                if (drcat.Count() > 0)
                                {
                                    dtreal = dtcat.Select("categoryLevel =" + count).CopyToDataTable();
                                    dtnext = dtreal.AsEnumerable().OrderByDescending(o => o.Field<int>("No")).Take(1).CopyToDataTable();
                                }
                                count = countno - 3;
                                DataRow[] drcat1 = dtcat.Select("categoryLevel =" + count);
                                if (drcat1.Count() > 0)
                                {
                                    dtreal1 = dtcat.Select("categoryLevel =" + count).CopyToDataTable();
                                    dtnext1 = dtreal1.AsEnumerable().OrderByDescending(o => o.Field<int>("No")).Take(1).CopyToDataTable();
                                }
                                count = countno - 2;
                                DataRow[] drcat2 = dtcat.Select("categoryLevel =" + count);
                                if (drcat2.Count() > 0)
                                {
                                    dtreal2 = dtcat.Select("categoryLevel =" + count).CopyToDataTable();
                                    dtnext2 = dtreal2.AsEnumerable().OrderByDescending(o => o.Field<int>("No")).Take(1).CopyToDataTable();
                                }
                                count = countno - 1;
                                DataRow[] drcat3 = dtcat.Select("categoryLevel =" + count);
                                if (drcat3.Count() > 0)
                                {
                                    dtreal3 = dtcat.Select("categoryLevel =" + count).CopyToDataTable();
                                    dtnext3 = dtreal3.AsEnumerable().OrderByDescending(o => o.Field<int>("No")).Take(1).CopyToDataTable();
                                    dt.Rows[i]["categorypath"] = dtnext.Rows[0]["name"] + "\\" + dtnext1.Rows[0]["name"] + "\\" + dtnext2.Rows[0]["name"] + "\\" + dtnext3.Rows[0]["name"] + "\\" + dt.Rows[i]["name"] + "\\";
                                }
                            }
                        }
                    }
                }
                return dt;
            }
            catch (Exception)
            {
                return dt;
            }
        }

        public static string Export(DataTable dt, string catSetNo, int shopid)
        {
            string path = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                RemoveColumn(dt);
                path = CreateFile(dt, catSetNo, shopid);
            }
            return path;
        }

        public static void RemoveColumn(DataTable dt)
        {
            dt.Columns.Remove("No");
        }

        public static string CreateFile(DataTable dt, string catSetNo, int shopid)
        {
            string physicalPath = null;
            string realfilename = catSetNo + "$" + shopid;
            realfilename = realfilename.Replace(".xlsx", "");
            if (dt != null && dt.Rows.Count > 0)
            {
                physicalPath = Directory.GetCurrentDirectory();
                physicalPath = physicalPath.Replace("bin\\Debug", "");
                if (!Directory.Exists(physicalPath + "Excel_Export\\"))
                {
                    Directory.CreateDirectory(physicalPath + "Excel_Export\\");
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Sheet1");
                    wb.SaveAs(physicalPath + "Excel_Export\\" + realfilename + ".xlsx");
                }
            }
            return physicalPath + "Excel_Export\\";
        }


        public static DataTable GetAPIKey()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("Select * From Shop where Mall_ID=1", conn);
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
