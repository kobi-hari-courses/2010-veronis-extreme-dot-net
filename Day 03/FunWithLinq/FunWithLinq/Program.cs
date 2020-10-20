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

            BasicOperatorsExample();
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
    }
}
