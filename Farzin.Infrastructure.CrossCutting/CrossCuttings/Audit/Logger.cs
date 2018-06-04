using Farzin.Infrastructure.CrossCutting.Helpers;
using System;
using System.IO;

namespace Farzin.Infrastructure.CrossCutting.Audit
{
    public class Logger
    {
        public static void Log(string s)
        {
            File.AppendAllText(AppConfig.Settings.LogFile, DateTime.Now.ToShamsiDateTimeString() + "\t" + s + "\r\n");
        }
        public static void Log(Exception ex)
        {
            var prefix1 = "ERROR: ";
            var prefix2 = "INNER: ";
            bool isInner = false;

            do
            {
                Log((isInner ? prefix2 : prefix1) + ex.Message);
                ex = ex.InnerException;
                if (!isInner) isInner = true;
            }
            while (ex != null);
        }

    }
}
