using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UHFReader28demomain
{
    /// <summary>
    /// 日志操作对象，提供将软件运行过程中一些状况记录入日志的操作接口；
    /// </summary>
    public class Log
    {
        public static string PATH;

        public static bool WriteLog(string log)
        {
            return write(log, "Log");
        }

        public static bool WriteError(string error)
        {
            return write(error, "Error");
        }

        public static bool WriteException(Exception exception)
        {
            if (exception.InnerException != null)
            {
                write("InnerException: " + exception.InnerException.ToString(), "Error");
            }

            if (exception.Message != null)
            {
                write("Message: " + exception.Message.ToString(), "Error");
            }

            if (exception.Source != null)
            {
                write("Source: " + exception.Source.ToString(), "Error");
            }

            if (exception.StackTrace != null)
            {
                write("StackTrace :" + exception.StackTrace.ToString(), "Error");
            }

            if (exception.TargetSite != null)
            {
                write("TargetSite :" + exception.TargetSite.ToString(), "Error");
            }
            write("-------------------------------------------------------------------------", "Error");

            return true;
        }

        private static bool write(string text, string writeType)
        {
            StreamWriter f = null;
            try
            {
                string path = PATH + @"\Log";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += @"\" + DateTime.Now.ToString("yyyyMMdd") + writeType + ".txt";
                if (File.Exists(path))
                {
                    f = File.AppendText(path);
                }
                else
                {
                    f = File.CreateText(path);
                }
                f.WriteLine(text);
                return true;

            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return false;
            }
            finally
            {
                if (f != null)
                {
                    f.Close();
                }
            }
        }
    }
}
