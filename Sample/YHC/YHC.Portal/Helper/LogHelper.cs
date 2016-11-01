using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Helper
{
    public class LogHelper
    {
        public static void Error(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        public static void Info(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Info(msg);
        }
    }
}