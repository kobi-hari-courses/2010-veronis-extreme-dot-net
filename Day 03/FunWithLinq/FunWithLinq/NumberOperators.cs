using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithLinq
{
    public static class NumberOperators
    {
        //public static IEnumerable<int> MultiplyBy10(this IEnumerable<int> source)
        //{
        //    foreach (var num in source)
        //    {
        //        yield return num * 10;
        //    }
        //}

        //public static IEnumerable<int> Add15(this IEnumerable<int> source)
        //{
        //    foreach (var num in source)
        //    {
        //        yield return num + 15;
        //    }
        //}

        public static IEnumerable<K> Map<T, K>(this IEnumerable<T> source, Func<T, K> projection)
        {
            foreach (var num in source)
            {
                yield return projection(num);
            }
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item)) 
                    yield return item;
            }
        }

        public static IEnumerable<int> Digits(this int i)
        {
            var res = i.ToString()
                .Select(c => (int)c - (int)'0');

            return res;
        }


    }
}
