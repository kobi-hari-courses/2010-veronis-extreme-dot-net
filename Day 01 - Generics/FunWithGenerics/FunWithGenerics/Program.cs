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
            var t = new Wrapper<int>(42);

            var t2 = new Wrapper<string>("hello");

            t2.DoSomething<double>();


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



            Console.ReadLine();


        }
    }
}
