using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithVariance
{
    class Program
    {
        static void Main(string[] args)
        {
            var lfood = new List<Food>();
            lfood.Add(new Pasta());
            var lfruit = new List<Fruit>();
            var lapples = new List<Apple>();

            var cFood = new MyComparer<Food>();
            var cfruit = new MyComparer<Fruit>();
            var cApple = new MyComparer<Apple>();

            // UseEnumerable(lfood);
            UseEnumerable(lfruit);
            UseEnumerable(lapples); // its ok, becasuse IEnumerable<T> is contravairant

            // UseCollection(lfood);
            UseCollection(lfruit);
            // UseCollection(lapples);

            UseComparer(cFood);
            UseComparer(cfruit);
            // UseComparer(cApple);    // not ok, because comprarer of apples cannot take banana as parameter
        }

        public static void UseEnumerable(IEnumerable<Fruit> fruits)
        {
        }

        public static void UseCollection(ICollection<Fruit> fruits)
        {
            fruits.Add(new Banana());
        }

        public static void UseComparer(IComparer<Fruit> comparer)
        {
            comparer.Compare(new Apple(), new Banana());
        }


    }
}
