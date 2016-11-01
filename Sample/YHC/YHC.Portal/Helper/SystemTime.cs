namespace YHC.Portal.Helper
{
    using System;

    public static class SystemTime
    {
        /// <summary>
        /// ��ǰʱ��(UTCʱ��)
        /// </summary>
        public static Func<DateTime> UTCNow = () => DateTime.UtcNow;

        /// <summary>
        /// IBC��ʱ�䣬������IBC��ѯ�����ÿ��ж�
        /// </summary>
        public static Func<DateTime> IBC_NullTime = () => new DateTime(1991, 1, 1);
    }

    /// <summary>
    /// ����ʱ��
    /// </summary>
    public static class ConfigTime
    {
        /// <summary>
        /// ��ǰʱ��(����ʱ����ʱ��)
        /// </summary>
        public static Func<DateTime> Now = () =>
            {
                return
                    (new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour,
                                  DateTime.UtcNow.Minute, DateTime.UtcNow.Second, DateTime.UtcNow.Millisecond));
                        //.AddMinutes(ConfigHelper.TimeZone);
            };
        /// <summary>
        /// ����(����ʱ����ʱ��)
        /// </summary>
        public static Func<DateTime> Today = () => Now().Date;
    }
}