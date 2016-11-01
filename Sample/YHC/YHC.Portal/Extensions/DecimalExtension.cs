namespace YHC.Portal.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Diagnostics;

    public static class DecimalExtension
    {
        [DebuggerStepThrough]
        public static string ToStringG0(this decimal target)
        {
            return target.ToString("G0");
        }

        [DebuggerStepThrough]
        public static string ToStringF2(this decimal target)
        {
            return target.ToString("F2");
        }

        [DebuggerStepThrough]
        public static string ToStringN0(this decimal target)
        {
            return target.ToString("N0");
        }

        [DebuggerStepThrough]
        public static string ToStringN2(this decimal target)
        {
            return target.ToString("N2");
        }

        public static string ToStringD2(this decimal target)
        {
            string str = target.ToString("N2");
            int index = str.IndexOf('.');
            return index == -1 ? "00" : str.Substring(index+1);
        }

        [DebuggerStepThrough]
        public static string ToStringOfRates(this decimal target, Int32 percent)
        {
            decimal t = Math.Round(target * 100000, 0);

            return (t / (percent * 10 )).ToString();
        }

        /// <summary>
        /// 元换算成分
        /// </summary>
        /// <param name="target">元，内部会四舍五入两位小数</param>
        /// <returns>分</returns>
        [DebuggerStepThrough]
        public static Int64 ToCents(this decimal target)
        {
            return (Int64)(Math.Round(target, 2) * 100);
        }

        /// <summary>
        /// 元换算成分，并且补齐12位，左边补0
        /// </summary>
        /// <param name="target">元，内部会四舍五入两位小数</param>
        /// <returns>分</returns>
        [DebuggerStepThrough]
        public static String ToCents12Bit(this decimal target)
        {
            Int64 cent = (Int64)(Math.Round(target, 2) * 100);
            String str = cent.ToString();
            Int32 len = str.Length;

            Int32 remain = 12 - len;
            String zero = "";
            for (Int32 i = 1; i <= remain; i++)
            {
                zero += "0";
            }

            String result = zero + str;
            return result;
        }

        /// <summary>
        /// 四舍五入，1.015=>1.02 , 5.045=>5.05
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Decimal RoundOfChina(this decimal target)
        {
            return Decimal.Parse(target.ToString("F2"));
        }
    }
}
