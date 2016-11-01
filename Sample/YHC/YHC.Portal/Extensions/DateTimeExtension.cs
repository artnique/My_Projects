namespace YHC.Portal.Extensions
{
    using YHC.Portal.Helper;
    using System;
    using System.Diagnostics;

    public static class DateTimeExtension
    {
        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);

        /// <summary>
        /// 获取UTC时间
        /// </summary>
        /// <param name="target"></param>
        /// <param name="timeZone">时间的原始时区</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        private static DateTime GetUtcDateTime(this DateTime target,int timeZone)
        {
            return (new DateTimeOffset(target, new TimeSpan(0, timeZone, 0))).UtcDateTime;
        }
        /// <summary>
        /// UTC0时间 转换成 配置时间
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static DateTime UtcToConfigTime(this DateTime target)
        {
            return UtcToConfigTime(target,ConfigHelper.TimeZone);
        }

        /// <summary>
        /// UTC0时间 转换成 配置时间
        /// </summary>
        /// <param name="timeZone"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static DateTime UtcToConfigTime(DateTime dateTime,int timeZone)
        {
            //为最大 最小 时间再加减 分钟 会出错
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                return dateTime;
            }
            return dateTime.AddMinutes(timeZone);
        }

        /// <summary>
        /// UTC0时间 转换成 配置时间
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static DateTime? UtcToConfigTime(this DateTime? target)
        {
            return UtcToConfigTime(target, ConfigHelper.TimeZone);
        }

        /// <summary>
        /// UTC0时间 转换成 配置时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static DateTime? UtcToConfigTime(DateTime? dateTime ,int timeZone)
        {
            if (dateTime.HasValue)
            {
                //为最大 最小 时间再加减 分钟 会出错
                if (dateTime.Value == DateTime.MinValue || dateTime.Value == DateTime.MaxValue)
                {
                    dateTime = dateTime.Value;
                }
                else
                {
                    dateTime = dateTime.Value.AddMinutes(timeZone);
                }
            }
            return dateTime;
        }
        
        /// <summary>
        /// 配置时间 转换成 UTC0时间
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime ConfigTimeToUtc(this DateTime target)
        {
            return target.GetUtcDateTime(ConfigHelper.TimeZone);
        }

        /// <summary>
        /// 配置时间 转换成 UTC0时间
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime? ConfigTimeToUtc(this DateTime? target)
        {
            if (target.HasValue)
            {
                target = target.Value.GetUtcDateTime(ConfigHelper.TimeZone);
            }
            return target;
        }
        /// <summary>
        /// 配置时间 转换成 UTC0时间 加 1 秒
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime ConfigTimeToUtcEnd(this DateTime target)
        {
            return target.ConfigTimeToUtc().AddSeconds(1);
        }

        [DebuggerStepThrough]
        public static bool IsValid(this DateTime target)
        {
            return (target >= MinDate) && (target <= MaxDate);
        }

        /// <summary>
        /// 返回格式yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static String ToFormatEndSeconds(this DateTime target)
        {
            return target.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回格式yyyy-MM-dd
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static String ToFormatEndDays(this DateTime target)
        {
            return target.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回格式yyyy-MM-dd<br/>HH:mm:ss，加了换行
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static String ToFormatEndSecondsWithBR(this DateTime target)
        {
            return target.ToString("yyyy-MM-dd<br/>HH:mm:ss");
        }

        /// <summary>
        /// 返回以00:00:00开始的日期(从配置的时间转换成UTC0)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime ConfigTimeToUtcDayStart(this DateTime target)
        {
            return (Convert.ToDateTime(target.ToString("yyyy-MM-dd 00:00:00"))).ConfigTimeToUtc();
        }
 
        /// <summary>
        /// 返回加一天以00:00:00格式的日期(从配置的时间转换成UTC0)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime ConfigTimeToUtcDayEnd(this DateTime target)
        {
            return (Convert.ToDateTime(target.AddDays(1).ToString("yyyy-MM-dd 00:00:00"))).ConfigTimeToUtc();
        }

        /// <summary>
        /// 返回当天的最晚时间23:59:59:999
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime ToSameDayEnd(this DateTime target)
        {
            return target.Date.AddDays(1).AddMilliseconds(-1);
        } 

        /// <summary>
        /// 将本地时间转为UTC时间
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime ToUTC(this DateTime target)
        {
            return TimeZoneInfo.ConvertTimeToUtc(target);
        }

        /// <summary>
        /// 格式化TimeSpan为00.00:00:00.000
        /// </summary>
        /// <param name="span">需要格式化的TimeSpan</param>
        /// <param name="showSign">是否显示正负号</param>
        /// <returns>格式化后的字符串</returns>
        public static string Format(this TimeSpan span, bool showSign)
        {
            string sign = String.Empty;
            if (showSign && (span > TimeSpan.Zero))
                sign = "+";

            return string.Format("{0}{1}天{2}小时{3}分钟{4}秒{5}毫秒", sign,
                span.Days.ToString("00"),
                span.Hours.ToString("00"),
                span.Minutes.ToString("00"),
                span.Seconds.ToString("00"),
                span.Milliseconds.ToString("000"));
            //return string.Format("{0}{1}.{2}:{3}:{4}.{5}", sign,
            //   span.Days.ToString("00"),
            //   span.Hours.ToString("00"),
            //   span.Minutes.ToString("00"),
            //   span.Seconds.ToString("00"),
            //   span.Milliseconds.ToString("000"));
        }
    }
}