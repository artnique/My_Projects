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
        /// ��ȡUTCʱ��
        /// </summary>
        /// <param name="target"></param>
        /// <param name="timeZone">ʱ���ԭʼʱ��</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        private static DateTime GetUtcDateTime(this DateTime target,int timeZone)
        {
            return (new DateTimeOffset(target, new TimeSpan(0, timeZone, 0))).UtcDateTime;
        }
        /// <summary>
        /// UTC0ʱ�� ת���� ����ʱ��
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static DateTime UtcToConfigTime(this DateTime target)
        {
            return UtcToConfigTime(target,ConfigHelper.TimeZone);
        }

        /// <summary>
        /// UTC0ʱ�� ת���� ����ʱ��
        /// </summary>
        /// <param name="timeZone"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static DateTime UtcToConfigTime(DateTime dateTime,int timeZone)
        {
            //Ϊ��� ��С ʱ���ټӼ� ���� �����
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                return dateTime;
            }
            return dateTime.AddMinutes(timeZone);
        }

        /// <summary>
        /// UTC0ʱ�� ת���� ����ʱ��
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static DateTime? UtcToConfigTime(this DateTime? target)
        {
            return UtcToConfigTime(target, ConfigHelper.TimeZone);
        }

        /// <summary>
        /// UTC0ʱ�� ת���� ����ʱ��
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static DateTime? UtcToConfigTime(DateTime? dateTime ,int timeZone)
        {
            if (dateTime.HasValue)
            {
                //Ϊ��� ��С ʱ���ټӼ� ���� �����
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
        /// ����ʱ�� ת���� UTC0ʱ��
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime ConfigTimeToUtc(this DateTime target)
        {
            return target.GetUtcDateTime(ConfigHelper.TimeZone);
        }

        /// <summary>
        /// ����ʱ�� ת���� UTC0ʱ��
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
        /// ����ʱ�� ת���� UTC0ʱ�� �� 1 ��
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
        /// ���ظ�ʽyyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static String ToFormatEndSeconds(this DateTime target)
        {
            return target.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// ���ظ�ʽyyyy-MM-dd
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static String ToFormatEndDays(this DateTime target)
        {
            return target.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// ���ظ�ʽyyyy-MM-dd<br/>HH:mm:ss�����˻���
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static String ToFormatEndSecondsWithBR(this DateTime target)
        {
            return target.ToString("yyyy-MM-dd<br/>HH:mm:ss");
        }

        /// <summary>
        /// ������00:00:00��ʼ������(�����õ�ʱ��ת����UTC0)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime ConfigTimeToUtcDayStart(this DateTime target)
        {
            return (Convert.ToDateTime(target.ToString("yyyy-MM-dd 00:00:00"))).ConfigTimeToUtc();
        }
 
        /// <summary>
        /// ���ؼ�һ����00:00:00��ʽ������(�����õ�ʱ��ת����UTC0)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime ConfigTimeToUtcDayEnd(this DateTime target)
        {
            return (Convert.ToDateTime(target.AddDays(1).ToString("yyyy-MM-dd 00:00:00"))).ConfigTimeToUtc();
        }

        /// <summary>
        /// ���ص��������ʱ��23:59:59:999
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime ToSameDayEnd(this DateTime target)
        {
            return target.Date.AddDays(1).AddMilliseconds(-1);
        } 

        /// <summary>
        /// ������ʱ��תΪUTCʱ��
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime ToUTC(this DateTime target)
        {
            return TimeZoneInfo.ConvertTimeToUtc(target);
        }

        /// <summary>
        /// ��ʽ��TimeSpanΪ00.00:00:00.000
        /// </summary>
        /// <param name="span">��Ҫ��ʽ����TimeSpan</param>
        /// <param name="showSign">�Ƿ���ʾ������</param>
        /// <returns>��ʽ������ַ���</returns>
        public static string Format(this TimeSpan span, bool showSign)
        {
            string sign = String.Empty;
            if (showSign && (span > TimeSpan.Zero))
                sign = "+";

            return string.Format("{0}{1}��{2}Сʱ{3}����{4}��{5}����", sign,
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