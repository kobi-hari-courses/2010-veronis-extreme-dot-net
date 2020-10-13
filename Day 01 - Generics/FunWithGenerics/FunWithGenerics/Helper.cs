using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    public class Helper
    {
        public static T GetLargest<T>(T first, T second)
            where T : IComparable<T>
        {
            if (first.CompareTo(second) > 0) return first;
            return second;
        }

        public static T ValueOrDefault<T>(T source)
            where T : class, new()
        {
            if (source != null) return source;
            return new T();
        }

        public static T SetColor<T>(T shape, ConsoleColor color)
            where T : Shape

        {
            shape.Color = color;
            return shape;
        }

        public static void OnlyArray<T>(T[] source)
        {

        }
    }
}
