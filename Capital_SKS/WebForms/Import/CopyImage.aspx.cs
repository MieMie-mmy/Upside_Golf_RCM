using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Collections;

namespace ORS_RCM.WebForms.Import
{
    public partial class CopyImage : System.Web.UI.Page
    {
        //String conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        string sourceimg = "C:/MyData/Software_Dev/Project_Source/ORS_RCM/Item_Image/";
        string desimg = "C:/Ponpare_Image/";
        string txtPath = "C:/MyData/temp.txt";

        //string sourceimg = ConfigurationManager.AppSettings["SourceFolderImagePath"].ToString();
        //string desimg = ConfigurationManager.AppSettings["DestinationFolderImagePath"].ToString();
        //string txtPath = ConfigurationManager.AppSettings["ImageTxtLogPath"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string imglist = TextBox1.Text;
            string[] str = imglist.Split(',');
         
            int leng = TextBox1.MaxLength;
            for (int i = 0; i < str.Length; i++)
            {
               
                string path = sourceimg + str[i];
                string des = desimg + str[i];
                if (File.Exists(path))
                {
                    File.Copy(path, des, true);
                }
                else
                {
                    
                    TextWriter twrite = new StreamWriter(txtPath, true);
                    twrite.Write(str[i] + ",");
                    twrite.Close();

                }

            }
            string myStringVariable = string.Empty;

            myStringVariable = "Successfully Copy!";

            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);


        }

        //public static void copy(string sourcePath,string targetPath)
        //{
        //    string sourceDir = @"C:\image";
        //    string backupDir = @"C:/New folder (2)";


        //    try
        //    {
        //        string[] picList = Directory.GetFiles(sourceDir, "*.jpg");
        //        string[] txtList = Directory.GetFiles(sourceDir, "*.txt");

        //        // Copy picture files. 
        //        foreach (string f in picList)
        //        {
        //            // Remove path from the file name. 
        //            string fName = f.Substring(sourceDir.Length + 1);


        //            // Use the Path.Combine method to safely append the file name to the path. 
        //            // Will overwrite if the destination file already exists.
        //            File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
        //        }

        //        // Copy text files. 
        //        foreach (string f in txtList)
        //        {

        //            // Remove path from the file name. 
        //            string fName = f.Substring(sourceDir.Length + 1);

        //            try
        //            {
        //                // Will not overwrite if the destination file already exists.
        //                File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName));
        //            }

        //            // Catch exception if the file was already copied. 
        //            catch (IOException copyError)
        //            {
        //                Console.WriteLine(copyError.Message);
        //            }
        //        }

        //        // Delete source files that were copied. 
        //        //foreach (string f in txtList)
        //        //{
        //        //    File.Delete(f);
        //        //}
        //        //foreach (string f in picList)
        //        //{
        //        //    File.Delete(f);
        //        //}
        //    }

        //    catch (DirectoryNotFoundException dirNotFound)
        //    {
        //        Console.WriteLine(dirNotFound.Message);
        //    }
        //}
    }
}