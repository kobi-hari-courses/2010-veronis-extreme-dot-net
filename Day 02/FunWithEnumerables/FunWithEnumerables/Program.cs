using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithEnumerables
{
    class Program
    {
        private static Random rand = new Random();


        static void Main(string[] args)
        {
            var sequence = GetSomeRandomNumbers(10).ToList();



            //var enumerator = fibonacci.GetEnumerator();

            //while (enumerator.MoveNext())
            //{
            //    Console.WriteLine(enumerator.Current);
            //}

            //enumerator.Dispose();

            foreach (var current in sequence)
            {
                Console.WriteLine(current);
            }

            Console.ForegroundColor= ConsoleColor.Red;

            foreach (var current in sequence)
            {
                Console.WriteLine(current);
            }


            Console.ReadLine();

        }

        public static IEnumerable<int> GetFibonacci(int max)
        {
            int i = 1;
            int j = 1;
            int current = 1;

            yield return 1;
            yield return 1;

            while (current < max)
            {
                var temp = current;
                current = temp + j;
                i = j;
                j = temp;

                yield return current;
            }

        }

        public static IEnumerable<int> GetSomeRandomNumbers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return rand.Next(100);
            }
        }
    }
}
