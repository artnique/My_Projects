using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;

namespace YHC.Portal.Helper
{
    public static class Logger
    {
        /// <summary>
        /// 记录致命的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Fatal(string message)
        {
            Check.Argument.IsNotEmpty(message, "message");

            GetLog().Fatal(message);
        }

        /// <summary>
        /// 记录致命的日志信息
        /// </summary>
        /// <param name="exception">日志信息</param>
        public static void Fatal(Exception exception)
        {
            GetLog().Fatal(exception);
        }

        /// <summary>
        /// 记录异常日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Error(string message)
        {
            Check.Argument.IsNotEmpty(message, "message");

            GetLog().Error(message);
        }

        /// <summary>
        /// 记录异常日志信息
        /// </summary>
        /// <param name="exception">日志信息</param>
        public static void Error(Exception exception)
        {
            GetLog().Error(exception);
        }

        /// <summary>
        /// 记录警告日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Warn(string message)
        {
            Check.Argument.IsNotEmpty(message, "message");

            GetLog().Warn(message);
        }

        /// <summary>
        /// 记录警告日志信息
        /// </summary>
        /// <param name="exception">日志信息</param>
        public static void Warn(Exception exception)
        {
            GetLog().Warn(exception);
        }

        /// <summary>
        /// 记录提示性的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Info(string message)
        {
            Check.Argument.IsNotEmpty(message, "message");

            GetLog().Info(message);
        }

        /// <summary>
        /// 记录提示性的日志信息
        /// </summary>
        /// <param name="exception">日志信息</param>
        public static void Info(Exception exception)
        {
            GetLog().Info(exception);
        }

        /// <summary>
        /// 记录跟踪的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Trace(string message)
        {
            Check.Argument.IsNotEmpty(message, "message");

            GetLog().Debug(message);
        }

        /// <summary>
        /// 记录跟踪的日志信息
        /// </summary>
        /// <param name="exception">日志信息</param>
        public static void Trace(Exception exception)
        {
            GetLog().Debug(exception);
        }

        private static ILog GetLog()
        {
            StackTrace stackTrace = new StackTrace(3);
            StackFrame stackFrame = stackTrace.GetFrame(0);
            MethodBase methodBase = stackFrame.GetMethod();

            // ApplicationRollingFile在.config文件中配置的名称
            return LogManager.GetLogger("ApplicationRollingFile");
        }
    }
}