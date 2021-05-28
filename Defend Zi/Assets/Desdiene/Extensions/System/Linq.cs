using System;
using System.Collections.Generic;
using System.Linq;

namespace Desdiene.Extensions.System
{
    public static class Linq
    {
        public static TSource FirstOrDefault2<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            if (source.Count() > 0) return source.First();
            else return defaultValue;
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource> defaultValueGetter)
        {
            if (defaultValueGetter is null) throw new ArgumentNullException(nameof(defaultValueGetter));

            if (source.Count() > 0) return source.First();
            else return defaultValueGetter.Invoke();
        }
    }
}
