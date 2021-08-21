using System;
using System.Collections.Generic;
using System.Linq;

namespace Desdiene.Extensions.System.Linq
{
    public static class LinqExt
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        /// <summary>
        /// Получить первый элемент коллекции, если ее размер > 0, либо другой элемен, переданный в параметры.
        /// </summary>
        public static TSource FirstOrElse<TSource>(this IEnumerable<TSource> source, TSource elseValue)
        {
            if (source.Count() > 0) return source.First();
            else return elseValue;
        }

        /// <summary>
        /// Получить первый элемент коллекции, если ее размер > 0, либо другой элемен, переданный в параметры.
        /// </summary>
        public static TSource FirstOrElse<TSource>(this IEnumerable<TSource> source, Func<TSource> elseValueGetter)
        {
            if (elseValueGetter is null) throw new ArgumentNullException(nameof(elseValueGetter));

            if (source.Count() > 0) return source.First();
            else return elseValueGetter.Invoke();
        }
    }
}
