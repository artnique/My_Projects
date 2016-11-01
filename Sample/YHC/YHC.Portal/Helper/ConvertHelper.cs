using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Helper
{
    public class ConvertHelper
    {
        /// <summary>
        /// 字符串转int，转换失败返回 0;
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int StringToInt(string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }
    }
}