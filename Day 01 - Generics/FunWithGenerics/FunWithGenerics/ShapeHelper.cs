using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    public static class ShapeHelper
    {
        // extension method
        public static T SetColor<T>(this T shape, ConsoleColor color)
            where T : Shape
        {
            shape.Color = color;
            return shape;
        }
    }
}
