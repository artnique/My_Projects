namespace YHC.Portal.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Diagnostics;

    public static class IntExtension
    {
        [DebuggerStepThrough]
        public static string ToStringN0(this int target)
        {
            return target.ToString("N0");
        }
    }
}
