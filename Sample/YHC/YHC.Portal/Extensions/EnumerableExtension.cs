using System.Linq;

namespace YHC.Portal.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class EnumerableExtension
    {
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        public static bool HasValue<T>(this IEnumerable<T> enumerable)
        {
            return (enumerable != null && enumerable.Any());
        }
    }
}