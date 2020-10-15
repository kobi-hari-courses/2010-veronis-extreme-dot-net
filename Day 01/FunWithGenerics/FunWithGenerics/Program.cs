using FunWithGenerics.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    class Program
    {
        static void Main(string[] args)
        {
            // var t = new Wrapper<int>(42);

            var t = Wrapper.From(42);

            var t2 = new Wrapper<string>("hello");

            t2.DoSomething<double>();


            //            var tcomplicated = new Wrapper<Dictionary<int, double>>(new Dictionary<int, double>());

            var tcomplicated = Wrapper.From(new Dictionary<int, double>());


            var p1 = new Person()
            {
                Id = "0123123123123", 
                FullName = "John Lennon", 
                Age = 42
            };

            var p2 = new Person()
            {
                Id = "654654654654654",
                FullName = "Paul McCartney",
                Age = 42
            };

            var p3 = new Person()
            {
                Id = "98798798798",
                FullName = "George Harrison",
                Age = 42
            };

            var p4 = new Person()
            {
                Id = "001013214855",
                FullName = "Richard Starkey",
                Age = 42
            };

            var l = new List<Person>()
            {
                p1, p2, p3, p4
            };

            var myFavorite = l[2];

            var d = new Dictionary<string, Person>();
            d.Add(p1.Id, p1);
            d.Add(p2.Id, p2);
            d.Add(p3.Id, p3);
            d.Add(p4.Id, p4);

            myFavorite = d["001013214855"];

            var c1 = new Complex(5, 0);
            var c2 = new Complex(3, 4);

            var isC1Bigger = c1.CompareTo(c2) > 0;

            var clargest = Helper.GetLargest(c1, c2);
            var plargest = Helper.GetLargest(p1, p2);

            var personNotNull = Helper.ValueOrDefault<Person>(p1);

            Circle c = new Circle()
                .SetColor(ConsoleColor.Green)
                .SetRadius(10);

            ShapeHelper.SetColor(c, ConsoleColor.Red).SetRadius(10);
            c.SetColor(ConsoleColor.Red);

            //            var twoSeconds = TimeSpan.FromSeconds(2);
            var twoSeconds = 2.Seconds();

            if (12.NotEquals(15))
            {
                Console.WriteLine("12 Is not equal to 15");
            }

            Console.ReadLine();


        }

        static void FunWithCovariance()
        {
            IFoo<Base> ifbase = new Foo<Base>();
            IFoo<Sub> ifsub = new Foo<Sub>();

            Base b = new Sub();
            IFoo<Sub> ifbase2 = new Foo<Base>();

            DoSomethingWithBase(new Base());
            DoSomethingWithBase(new Sub());

            //DoSomethingWithSub(new Base());
            DoSomethingWithSub(new Sub());

            Base b2 = MethodThatReturnsBase();
            //Sub s2 = MethodThatReturnsBase();


            IEnumerable<Base> ib = new List<Sub>();

            Base b3;
            b3 = MethodWithOut();
        }


        public static void DoSomethingWithBase(Base b)
        {

        }

        public static void DoSomethingWithSub(Sub b)
        {

        }

        public static Base MethodThatReturnsBase()
        {
            return new Base();
        }

        public static Sub MethodWithOut()
        {
            return new Sub();
        }


    }
}
