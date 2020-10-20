using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithLinq
{
    class Program
    {
        static void Main(string[] args)
        {

            ZipExample();
            Console.ReadLine();
        }

        #region How LINQ is constructed

        public static void UsingOurOwnOperatorsExample()
        {
            var myNumbers = GenerateSomeNumbers()
                .Map(n => n * 10)
                .Map(n => n + 15)
                .Filter(n => n % 3 == 0)
                .Map(n => n.ToString());

            var myStrings = GenerateSomeStrings()
                .Filter(s => s.Length > 4)
                .Map(s => s.ToLower())
                .Map(s => string.Join("", s.Reverse()));

            foreach (var num in myNumbers)
            {
                Console.WriteLine(num);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;

            foreach (var str in myStrings)
            {
                Console.WriteLine(str);
            }
        }

        public static void QuerySyntaxExample()
        {

            var query1 = GenerateSomeNumbers()
                .Where(n => n > 40)
                .Select(n => n.ToString());

            var query2 = from n in GenerateSomeNumbers()
                         where n > 40
                         select n.ToString();

        }

        public static IEnumerable<int> Inifinte()
        {
            var i = 0;
            while (true)
            {
                yield return i;
                i += 1;
            }
        }

        public static IEnumerable<int> GenerateSomeNumbers()
        {
            yield return 123;  // 6
            yield return 154;  // 10
            yield return 142; // 7
            yield return 233; // 8
            yield return 25;  // 7
            yield return 35;  // 8
            yield return 262; // 10
            yield return 100; // 1
            yield break;
        }

        public static IEnumerable<string> GenerateSomeStrings()
        {
            yield return "Hello";
            yield return "World";
            yield return "How";
            yield return "Are";
            yield return "You";

        }


        #endregion

        #region LINQ Operators

        public static void BasicOperatorsExample()
        {
            // Select
            // Where
            // OrderBy 

            var numbers = GenerateSomeNumbers();
            numbers = numbers.OrderBy(n => n);

            var amount = numbers.Count();

            var first = numbers.Last();
            var list = numbers.ToList();
            var array = numbers.ToArray();
            var hashset = numbers.ToHashSet();
            var numbersByString = numbers.ToDictionary(num => num.ToString());

            var numbersBySumOfDigits = numbers.ToLookup(num => num.Digits().Sum());


            foreach (var num in numbers)
            {
                Console.WriteLine(num);
            }

            //Console.WriteLine(string.Join(", ", numbers));

            numbers = GenerateSomeNumbers()
                .OrderBy(n => n.Digits().Sum())
                .ThenBy(n => n % 10);

            Console.WriteLine(string.Join(", ", numbers));

        }

        #endregion

        #region GroupBy Demo

        public static void GroupByExample()
        {
            var cars = Reader.CarsFromCsv("Data/cars.csv");

            var manufacturers = cars
                .Select(c => c.Make)
                .Distinct();

            var carsByManufacturers = cars
                .GroupBy(c => c.Make);

            var alternativeSyntax = from car in cars
                                    group car by car.Make into make
                                    select make;

            foreach (IGrouping<string, Car> group in carsByManufacturers)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(group.Key);

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var car in group)
                {
                    Console.WriteLine(car.Model);
                }
            }

            var carsAgain = carsByManufacturers.SelectMany(group => group);
        }

        public static void MostEfficientOfEachManufacturer()
        {
            var cars = Reader.CarsFromCsv("Data/cars.csv");

            var bestCars = cars
                .GroupBy(c => c.Make)
                .Select(group => group
                                    .OrderByDescending(car => car.CombinedFE)
                                    .First())
                .Select(car => new
                {
                    Name = $"{car.Make} {car.Model}", 
                    Efficiency = car.CombinedFE
                });


            foreach (var car in bestCars)
            {
                Console.WriteLine($"{car.Name} {car.Efficiency}");
            }


        }

        #endregion

        #region Aggregation operators

        public static void AggregationExample()
        {
            var cars = Reader
                .CarsFromCsv("Data/cars.csv")
                .Where(car => car.Make == "Porsche");

            var lowestFE = cars.Min(car => car.CombinedFE);

            var sumOfFe = cars.Sum(car => car.CombinedFE);

            var carWithLowestCombinedFe = cars.Aggregate((c1, c2) => (c1.CombinedFE > c2.CombinedFE) ? c2 : c1);

            var sumOfCarFe = cars.Aggregate(0, (sum, car) => sum + car.CombinedFE);

            var avgCombinedFw = cars.Aggregate(
                new { Sum = 0, Count = 0 },
                (acc, car) => new { Sum = acc.Sum + car.CombinedFE, Count = acc.Count + 1 },
                acc => (double)acc.Sum / acc.Count);




        }

        #endregion

        #region Zip Examples

        public static void ZipExample()
        {
            var l1 = new List<int> { 1, 5, 10, 20, 15, 6, 16 };

            var pairs = l1
                .Zip(l1.Skip(1), (i1, i2) => (i1, i2))
                .Where((_, index) => index % 2 == 0);

            foreach (var pair in pairs)
            {
                Console.WriteLine(pair);
            }

        }

        #endregion

    }
}
