namespace YHC.Portal.Helper
{
    using System;

    public static class SystemTime
    {
        /// <summary>
        /// 当前时间(UTC时间)
        /// </summary>
        public static Func<DateTime> UTCNow = () => DateTime.UtcNow;

        /// <summary>
        /// IBC空时间，用来做IBC查询条件置空判断
        /// </summary>
        public static Func<DateTime> IBC_NullTime = () => new DateTime(1991, 1, 1);
    }

    /// <summary>
    /// 配置时间
    /// </summary>
    public static class ConfigTime
    {
        /// <summary>
        /// 当前时间(配置时区的时间)
        /// </summary>
        public static Func<DateTime> Now = () =>
            {
                return
                    (new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour,
                                  DateTime.UtcNow.Minute, DateTime.UtcNow.Second, DateTime.UtcNow.Millisecond));
                        //.AddMinutes(ConfigHelper.TimeZone);
            };
        /// <summary>
        /// 今天(配置时区的时间)
        /// </summary>
        public static Func<DateTime> Today = () => Now().Date;
    }
}