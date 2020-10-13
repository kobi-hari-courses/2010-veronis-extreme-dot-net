using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    public class Shape
    {
        public double Area { get; }

        public ConsoleColor Color { get; internal set; }

    }

    public class Circle: Shape
    {
        public double Radius { get; private set; }

        public Circle SetRadius(double radius)
        {
            Radius = radius;
            return this;
        }
    }

    public class Rectangle: Shape
    {

    }
}
