using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CodChallange
{
    public class Common
    {
        public void writeLog(string text)
        {
            try
            {
                //locally
                string folderpath = System.Web.HttpContext.Current.Server.MapPath("~/Files/Errorlog");


                String filePath = folderpath + @"Errorlog\" + DateTime.Now.ToString("yyyyMMdd") + "error.txt";
                StreamWriter writer = new StreamWriter(filePath, true);
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", " + text);
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                writeLog(ex.ToString());
            }
        }
    }
}