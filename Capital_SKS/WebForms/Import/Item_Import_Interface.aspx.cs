/* 
Created By              : Kyaw Thet Paing
Created Date          :02/09/2014
Updated By             :
Updated Date         :

 Tables using: Item_ImportLog
    -
    -
    -
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_BL;
using System.Configuration;
using Limilabs.FTP.Client;
using ORS_RCM.WebForms.Import;

namespace ORS_RCM
{
	public partial class Item_Import_Interface : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (Request.QueryString["User"] != null && Request.QueryString["Password"] != null)
				{
					LogInBL LogBL = new LogInBL();
					String loginId = Request.QueryString["User"].ToString();
					String pass = Request.QueryString["Password"].ToString();


					DataTable dt = LogBL.logincheck(loginId);

					if (dt != null && dt.Rows.Count > 0)
					{
                        int userID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                        String localPath = ConfigurationManager.AppSettings["ImportItem"].ToString();
                        String ftpHost = ConfigurationManager.AppSettings["Item_Master_Import_FTP"];
                        String ftpUserName = ConfigurationManager.AppSettings["Item_Master_Import_FTPUserName"];
                        String ftpPassword = ConfigurationManager.AppSettings["Item_Master_Import_FTPPassword"];

                       String ftpPath = ftpHost ;

                       FtpWebRequest ftpRequest;
						ftpRequest = (FtpWebRequest)WebRequest.Create(ftpPath);
						ftpRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
						ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
						FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                        using (StreamReader streamReader = new StreamReader(ftpResponse.GetResponseStream()))
                        {
                            String[] filelist = streamReader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            ftpResponse.Close();
                            for (int i = 0; i < filelist.Count(); i++)
                            {
                                if (check(filelist[i]))
                                {
                                    FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(ftpPath + filelist[i]);
                                    requestFileDownload.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                                    requestFileDownload.KeepAlive = false;
                                    requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;
                                    FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();

                                    using (Stream responseStream = responseFileDownload.GetResponseStream())
                                    { 
                                    using (FileStream writeStream = new FileStream(localPath + filelist[i], FileMode.Create))
                                    {
                                        int Length = 2048;
                                        Byte[] buffer = new Byte[Length];
                                        int bytesRead = responseStream.Read(buffer, 0, Length);

                                        while (bytesRead > 0)
                                        {
                                            writeStream.Write(buffer, 0, bytesRead);
                                            bytesRead = responseStream.Read(buffer, 0, Length);
                                        }
                                        writeStream.Close();
                                    }
                                    responseStream.Close();
                                        responseFileDownload.Close();
                                       
                                    }
                                    

                                    String path = localPath + Path.GetFileName(filelist[i]);
                                    //String csv = Path.GetFileName(filelist[2]);

                                    DataTable dtItem = GlobalUI.CSVToTable(path);
                                    dtItem = GlobalUI.Remove_Doublecode(dtItem);

                                    //Item Master column
                                    String[] ItemMasterColumn = { 
						"販売管理番号", "商品番号", "商品名", "定価", "販売価格", 
						"原価", "発売日", "掲載可能日", "年度", "シーズン", 
						"ブランドコード", "ブランド名", "ヤフーブランドコード", "競技コード", "競技名", 
						"分類コード","分類名","仕入先名","カタログ情報","特記フラグ",
						"予約フラグ","指示書番号","承認日","備考","メーカー商品コード"};

                                    if (CheckColumn(ItemMasterColumn, dtItem))
                                    {
                                        dtItem = ChangeItemMasterHeader(dtItem);
                                        DataColumn newcol = new DataColumn("チェック", typeof(String));
                                        newcol.DefaultValue = "";
                                        dtItem.Columns.Add(newcol);
                                        DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
                                        newColumn.DefaultValue = 6;
                                        dtItem.Columns.Add(newColumn);

                                        DataColumn dc = new DataColumn("エラー内容", typeof(String));
                                        dc.DefaultValue = "";
                                        dtItem.Columns.Add(dc);

                                        String[] colTypeCheck = { "List_Price", "Sale_Price", "Cost", "Special_Flag", "Reservation_Flag" };
                                        DataTable dterrcheck = checkIntType(dtItem, colTypeCheck, 0);

                                        String[] colLengthCheck2 = { "Product_Code" };
                                        dterrcheck = CheckLength(dterrcheck, colLengthCheck2, 100, 0);

                                        String[] colLengthCheck3 = { "Item_AdminCode" };
                                        dterrcheck = CheckLength(dterrcheck, colLengthCheck3, 1300, 0);

                                        String[] colLengthCheck4 = { "Item_Code" };
                                        dterrcheck = CheckLength(dterrcheck, colLengthCheck4, 32, 0);

                                        String[] colLengthCheck5 = { "Item_Name" };
                                        dterrcheck = CheckLength(dterrcheck, colLengthCheck5, 255, 0);

                                        String[] colLengthCheck6 = { "Brand_Code", "Competition_Code", "Classification_Code" };
                                        dterrcheck = CheckLength(dterrcheck, colLengthCheck6, 4, 0);

                                        String[] colLengthCheck7 = { "Season" };
                                        dterrcheck = CheckLength(dterrcheck, colLengthCheck7, 40, 0);

                                        String[] colLengthCheck8 = { "Brand_Name", "Competition_Name", "Class_Name", "Company_Name" };
                                        dterrcheck = CheckLength(dterrcheck, colLengthCheck8, 200, 0);

                                        String[] colLengthCheck9 = { "Brand_Code_Yahoo" };
                                        dterrcheck = CheckLength(dterrcheck, colLengthCheck9, 6, 0);

                                        String[] colLengthCheck10 = { "Catalog_Information", "Remarks" };
                                        dterrcheck = CheckLength(dterrcheck, colLengthCheck10, 3000, 0);

                                        String[] colLengthCheck11 = { "Instruction_No" };
                                        dterrcheck = CheckLength(dterrcheck, colLengthCheck11, 4000, 0);

                                        String[] colLengthCheck12 = { "Year" };
                                        dterrcheck = CheckLength(dterrcheck, colLengthCheck12, 20, 0);
                                        //String[] colLengthCheck = {"Item_Code", "Year", "Season", "Brand_Name", "Brand_Code",
                                        //                "Brand_Code_Yahoo", "Competition_Code", "Competition_Name", "Class_Code", 
                                        //                "Class_name","Company_name","Instruction_No"};//max length nvarchar 50                                                
                                        //dterrcheck = CheckLength(dterrcheck, colLengthCheck, 50, 0);

                                        //String[] colLengthCheck1 = { "Item_Name", "Catalog_Info", "Remarks" };//max length nvarchar 200
                                        //dterrcheck = CheckLength(dterrcheck, colLengthCheck1, 200, 0);

                                        String[] colDateCheck = { "Release_Date", "Post_Available_Date", "Approve_Date" };
                                        dterrcheck = CheckDate(dterrcheck, colDateCheck, 0);

                                        if (!dterrcheck.Columns.Contains("User_ID"))
                                        {
                                            DataColumn dcUser = new DataColumn();
                                            dcUser.DefaultValue = userID;
                                            dcUser.ColumnName = "User_ID";
                                            dterrcheck.Columns.Add(dcUser);
                                        }

                                        Import_Item_BL itbl = new Import_Item_BL();
                                        Item_ImportLog_BL ItemImbl = new Item_ImportLog_BL();
                                        if (dterrcheck.Rows.Count > 0)
                                            itbl.Itemmaster(dterrcheck);

                                        goto Found;
                                    }

                                    String[] SkuColumn = { "販売管理番号", "商品番号", "カラー名", "サイズ名", "カラーコード", "サイズコード", "カラー正式名称", "サイズ正式名称", "JANコード" };
                                    if (CheckColumn(SkuColumn, dtItem))
                                    {
                                        dtItem = ChangeSKUHeader(dtItem);
                                        DataColumn newcol = new DataColumn("チェック", typeof(String));
                                        newcol.DefaultValue = "";
                                        dtItem.Columns.Add(newcol);
                                        DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
                                        newColumn.DefaultValue = 6;
                                        dtItem.Columns.Add(newColumn);

                                        DataColumn dc = new DataColumn("エラー内容", typeof(String));
                                        dc.DefaultValue = "";
                                        dtItem.Columns.Add(dc);

                                        String[] colCheckLength = { "Item_Code", "Color_Code", "Size_Code", "JAN_Code" };
                                        DataTable dterr = CheckLength(dtItem, colCheckLength, 50, 1);

                                        String[] colCheckLength1 = { "Color_Name", "Size_Name", "Color_Name_Official", "Size_Name_Official" };
                                        dterr = CheckLength(dterr, colCheckLength1, 200, 1);

                                        if (!dterr.Columns.Contains("User_ID"))
                                        {
                                            DataColumn dcUser = new DataColumn();
                                            dcUser.DefaultValue = userID;
                                            dcUser.ColumnName = "User_ID";
                                            dterr.Columns.Add(dcUser);
                                        }

                                        Import_Item_BL itbl = new Import_Item_BL();
                                        Item_ImportLog_BL ItemImbl = new Item_ImportLog_BL();
                                        if (dterr.Rows.Count > 0)
                                            itbl.SKU(dterr);
                                        goto Found;
                                    }

                                    String[] InventoryColumn = { "販売管理番号", "商品番号", "在庫数" };
                                    if (CheckColumn(InventoryColumn, dtItem))
                                    {
                                        dtItem = ChangeInventoryHeader(dtItem);
                                        DataColumn newcol = new DataColumn("チェック", typeof(String));
                                        newcol.DefaultValue = "";
                                        dtItem.Columns.Add(newcol);
                                        DataColumn newColumn = new DataColumn("Type", typeof(System.Int32));
                                        newColumn.DefaultValue = 6;
                                        dtItem.Columns.Add(newColumn);

                                        DataColumn dc = new DataColumn("エラー内容", typeof(String));
                                        dc.DefaultValue = "";
                                        dtItem.Columns.Add(dc);

                                        String[] colCheckLength = { "Item_Code" };
                                        DataTable dterr = CheckLength(dtItem, colCheckLength, 50, 2);

                                        String[] colCheckType = { "Quantity" };
                                        dterr = checkIntType(dterr, colCheckType, 2);

                                        if (!dterr.Columns.Contains("User_ID"))
                                        {
                                            DataColumn dcUser = new DataColumn();
                                            dcUser.DefaultValue = userID;
                                            dcUser.ColumnName = "User_ID";
                                            dterr.Columns.Add(dcUser);
                                        }

                                        Import_Item_BL itbl = new Import_Item_BL();
                                        Item_ImportLog_BL ItemImbl = new Item_ImportLog_BL();
                                        if (dterr.Rows.Count > 0)
                                            itbl.Inventory(dterr);
                                    }

                                Found: ;

                                //using (Ftp client = new Ftp())
                                //    {
                                //        String ftpPath1 = ftpPath.Replace("ftp://","");
                                //        ftpPath1 = ftpPath1.Replace("//", "");
                                //        client.Connect(ftpPath1);
                                //        client.Login(ftpUserName, ftpPassword);
                                //        string filename = filelist[i].Replace(".csv", "");
                                //        string nowdate = DateTime.Now.ToString();
                                //        nowdate = nowdate.Replace("/", "-");
                                //        nowdate = nowdate.Replace(" ", "-");
                                //        nowdate = nowdate.Replace(":", "-");
                                //        client.Rename(filelist[i], "/Item_Import_Finished/"+ filename+nowdate+".csv");

                                //        client.Close();
                                //    }
                                    

                                    //Export to Finish path
                                    FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpHost + "/Item_Import_Finished/" + filelist[i]);
                                    ftp.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                                    ftp.KeepAlive = false;
                                    ftp.UseBinary = true;
                                    ftp.Method = WebRequestMethods.Ftp.UploadFile;
                                    using (FileStream fs = File.OpenRead(localPath + filelist[i]))
                                    { 
                                        byte[] buffer1 = new byte[fs.Length];
                                        fs.Read(buffer1, 0, buffer1.Length);
                                        using (Stream ftpstream = ftp.GetRequestStream())
                                        {
                                            ftpstream.Write(buffer1, 0, buffer1.Length);
                                        }
                                    }

                                    //Delete
                                    FtpWebRequest ftpRequestDelete = (FtpWebRequest)WebRequest.Create(ftpHost + filelist[i]);
                                    ftpRequestDelete.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                                    ftpRequestDelete.Method = WebRequestMethods.Ftp.DeleteFile;
                                    ftpRequestDelete.KeepAlive = false;
                                    using (FtpWebResponse response = (FtpWebResponse)ftpRequestDelete.GetResponse())
                                    { }
                                   
                                    lblErrorMsg.Text = "Imported Successfully!";

                                    ////Upload
                                    //string filename = Path.GetFileName(filelist[i]);
                                    //string ftpfullpath = ftpHost + "/Item_Import_Finished/"+filelist[i];
                                    //FtpWebRequest ftpUpload = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                                    //ftpUpload.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

                                    //ftpUpload.KeepAlive = false;
                                    //ftpUpload.UseBinary = true;
                                    //ftpUpload.Method = WebRequestMethods.Ftp.UploadFile;

                                    //FileStream fsUpload = File.OpenRead(localPath+filelist[i] );
                                    //byte[] bufferUpload = new byte[fsUpload.Length];
                                    //fsUpload.Read(bufferUpload, 0, bufferUpload.Length);
                                    //fsUpload.Close();

                                    //Stream ftpstreamUpload = ftpUpload.GetRequestStream();
                                    //ftpstreamUpload.Write(bufferUpload, 0, bufferUpload.Length);
                                    //ftpstreamUpload.Close();
                                }
                            }
                        }
                        
					}
					else
					{
						lblErrorMsg.Text = "User Name and Password Wrong!";
					}
				}
				else
				{
					lblErrorMsg.Text = "Please Enter User Name & Password!";
				}
			}
			catch (Exception ex)
			{
				Session["Exception"] = ex.ToString();
				Response.Redirect("~/CustomErrorPage.aspx?");
			}
		}

		public DataTable ChangeInventoryHeader(DataTable dt)
		{
			try
			{
				dt.Columns["商品番号"].ColumnName = "Item_Code";
				dt.Columns["販売管理番号"].ColumnName = "Item_AdminCode";
				dt.Columns["在庫数"].ColumnName = "Quantity";
				return dt;
			}
			catch (Exception ex)
			{
				Session["Exception"] = ex.ToString();
				Response.Redirect("~/CustomErrorPage.aspx?");
				return dt;
			}

		}

		public DataTable ChangeSKUHeader(DataTable dt)
		{
			try
			{
				dt.Columns["販売管理番号"].ColumnName = "Item_AdminCode";
				dt.Columns["商品番号"].ColumnName = "Item_Code";
				dt.Columns["カラー名"].ColumnName = "Color_Name";
				dt.Columns["サイズ名"].ColumnName = "Size_Name";
				dt.Columns["カラーコード"].ColumnName = "Color_Code";
				dt.Columns["サイズコード"].ColumnName = "Size_Code";
				dt.Columns["カラー正式名称"].ColumnName = "Color_Name_Official";
				dt.Columns["サイズ正式名称"].ColumnName = "Size_Name_Official";
				dt.Columns["JANコード"].ColumnName = "JAN_Code";
				return dt;
			}
			catch (Exception ex)
			{
				Session["Exception"] = ex.ToString();
				Response.Redirect("~/CustomErrorPage.aspx?");
				return dt;
			}
		}

		public DataTable CheckDate(DataTable dt, String[] col, int type)
		{
			try
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					for (int j = 0; j < col.Length; j++)
					{
						try
						{
                            if (!String.IsNullOrWhiteSpace(dt.Rows[i][col[j]].ToString()))
                            {
                                DateTime dateTime = Convert.ToDateTime(dt.Rows[i][col[j]].ToString());
                                DateTime dtMin = DateTime.MinValue;
                                DateTime dtMax = DateTime.MaxValue;

                                dtMin = new DateTime(1753, 1, 1);
                                dtMax = new DateTime(9999, 12, 31, 23, 59, 59, 997);

                                if (dateTime < dtMin || dateTime > dtMax)
                                {
                                    dt.Rows[i]["チェック"] = "エラー";
                                    dt.Rows[i]["エラー内容"] = EngToJpHeader(col[j].ToString(), type) + "のフォーマットが不正です。";
                                    dt.Rows[i]["Type"] = 5;
                                }
                            }
						}
						catch (Exception)
						{
							dt.Rows[i]["チェック"] = "エラー";
							dt.Rows[i]["エラー内容"] = EngToJpHeader(col[j].ToString(), type) + "のフォーマットが不正です。";
							dt.Rows[i]["Type"] = 5;
						}
					}
				}
				return dt;
			}
			catch (Exception ex)
			{
				Session["Exception"] = ex.ToString();
				Response.Redirect("~/CustomErrorPage.aspx?");
				return dt;
			}
		}

		public DataTable CheckLength(DataTable dt, String[] col, int length, int type)
		{
			try
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					for (int j = 0; j < col.Length; j++)
					{
						Encoding enc = Encoding.GetEncoding(932);
                        int byteLength = enc.GetByteCount(dt.Rows[i][col[j]].ToString());
                        if (byteLength > length)
                        {
							dt.Rows[i]["チェック"] = "エラー";
							dt.Rows[i]["エラー内容"] = EngToJpHeader(col[j], type) + "-Greater than " + length + " Bytes";
							dt.Rows[i]["Type"] = 5;
						}
					}
				}
				return dt;
			}
			catch (Exception ex)
			{
				Session["Exception"] = ex.ToString();
				Response.Redirect("~/CustomErrorPage.aspx?");
				return dt;
			}
		}

		public DataTable checkIntType(DataTable dt, String[] col, int type)
		{
			try
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					for (int j = 0; j < col.Length; j++)
					{
						try
						{
							if (!String.IsNullOrWhiteSpace(dt.Rows[i][col[j]].ToString()))
								Convert.ToInt32(dt.Rows[i][col[j]].ToString());
						}
						catch (Exception)
						{
							dt.Rows[i]["チェック"] = "エラー";
							dt.Rows[i]["エラー内容"] = EngToJpHeader(col[j], type) + "のフォーマットが不正です。";
							dt.Rows[i]["Type"] = 5;
						}
					}
				}
				return dt;
			}
			catch (Exception ex)
			{
				Session["Exception"] = ex.ToString();
				Response.Redirect("~/CustomErrorPage.aspx?");
				return dt;
			}
		}

		//get header name english to jp
		//type 0=Item Master
		//type 1=SKU
		//type 2=Inventory
		public String EngToJpHeader(String Eng, int type)
		{
			if (type == 0)
			{
				switch (Eng)//Item Master
				{
					case "Item_AdminCode": return "販売管理番号";
					case "Item_Code": return "商品番号";
					case "Item_Name": return "商品名";
					case "List_Price": return "定価";
					case "Sale_Price": return "販売価格";
					case "Cost": return "原価";
					case "Release_Date": return "発売日";
					case "Post_Available_Date": return "掲載可能日";
					case "YEAR": return "年度";
					case "Season": return "シーズン";
					case "Brand_Code": return "ブランドコード";
					case "Brand_Name": return "ブランド名";
					case "Brand_Code_Yahoo": return "ヤフーブランドコード";
					case "Competition_Code": return "競技コード";
					case "Competition_Name": return "競技名";
                    case "Classification_Code": return "分類コード";
					case "Class_name": return "分類名";
					case "Company_name": return "仕入先名";
					case "Catalog_Info": return "カタログ情報";
					case "Special_Flag": return "特記フラグ";
					case "Reservation_Flag": return "予約フラグ";
					case "Instruction_No": return "指示書番号";
					case "Approve_Date": return "承認日";
					case "Remarks": return "備考";
					default: return "";
				}
			}

			else if (type == 1)//SKU
			{
				switch (Eng)
				{
					case "Item_AdminCode": return "販売管理番号";
					case "Item_Code": return "商品番号";
					case "Color_Name": return "カラー名";
					case "Size_Name": return "サイズ名";
					case "Color_Code": return "カラーコード";
					case "Size_Code": return "サイズコード";
					case "Color_Name_Official": return "カラー正式名称";
					case "Size_Name_Official": return "サイズ正式名称";
					case "JAN_Code": return "JANコード";
				}
			}
			else if (type == 2)//Inventory
			{
				switch (Eng)
				{
					case "Item_Code": return "商品番号";
					case "Item_AdminCode": return "販売管理番号";
					case "Original_Quantity": return "在庫数";
				}
			}
			return "";
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
			dt.Columns["年度"].ColumnName = "Year";
			dt.Columns["シーズン"].ColumnName = "Season";
			dt.Columns["ブランドコード"].ColumnName = "Brand_Code";
			dt.Columns["ブランド名"].ColumnName = "Brand_Name";
			dt.Columns["ヤフーブランドコード"].ColumnName = "Brand_Code_Yahoo";
			dt.Columns["競技コード"].ColumnName = "Competition_Code";
			dt.Columns["競技名"].ColumnName = "Competition_Name";
            dt.Columns["分類コード"].ColumnName = "Classification_Code";
			dt.Columns["分類名"].ColumnName = "Class_Name";
			dt.Columns["仕入先名"].ColumnName = "Company_Name";
            dt.Columns["カタログ情報"].ColumnName = "Catalog_Information";
			dt.Columns["特記フラグ"].ColumnName = "Special_Flag";
			dt.Columns["予約フラグ"].ColumnName = "Reservation_Flag";
			dt.Columns["指示書番号"].ColumnName = "Instruction_No";
			dt.Columns["承認日"].ColumnName = "Approve_Date";
			dt.Columns["備考"].ColumnName = "Remarks";
			dt.Columns["メーカー商品コード"].ColumnName = "Product_Code";            
			return dt;
		}

		public Boolean CheckColumn(String[] colName, DataTable dt)
		{
			DataColumnCollection col = dt.Columns;
			for (int i = 0; i < colName.Length; i++)
			{
				if (!col.Contains(colName[i]))
					return false;
			}
			return true;
		}

		private bool check(String str)
		{
			if (str.Contains(".csv"))
				return true;
			else
				return false;
		}

		public void CreateDir(String path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
	}
}