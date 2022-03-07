
/* 
Created By              : Ei Thinzar Zaw
Created Date          : 23/09/2015
Updated By             :
Updated Date         :
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace Test
{
    class Program
    {
        public static string path = Convert.ToString(ConfigurationManager.AppSettings["ErrorPath"]);
        public static int dateOption = Convert.ToInt32(ConfigurationManager.AppSettings["DateOption"]);
        public static void Main(string[] args)
        {
            DeleteFiles();
        }

        public static FileInfo[] GetFiles()
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] files = d.GetFiles("*.csv");
            return files;
        }

        public static void DeleteFiles()
        {
            try
            {
                FileInfo[] files = GetFiles();
                int fileDiff = 0;
                DateTime fileCreateDate;
                foreach (FileInfo file in files)
                {
                    fileCreateDate = File.GetCreationTime(path + file);

                    fileDiff = DifferentDate(fileCreateDate, DateTime.Now);

                    if (fileDiff > dateOption)
                    {

                        File.Delete(path + file);
                        //    Process Process = new Process();

                        //    Process.StartInfo.FileName = "cmd.exe";
                        //    Process.StartInfo.Arguments = "/C DEL /Q /F \"" + path + file + "\"";
                        //    Process.StartInfo.CreateNoWindow = true;
                        //    Process.StartInfo.UseShellExecute = false;
                        //    Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                        //    Process.Start();

                        //    Process.WaitForExit();

                        //    while (!Process.HasExited)
                        //    {
                        //        Console.Write("Error");
                        //    }
                    }

                }
                Console.WriteLine("Successfully delete error file.");
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static int DifferentDate(DateTime createDate, DateTime sysDate)
        {
            int dateDiff = 0;
            TimeSpan dDiff = sysDate.Subtract(createDate);
            dateDiff = dDiff.Days;
            return dateDiff;
        }
    }
}
