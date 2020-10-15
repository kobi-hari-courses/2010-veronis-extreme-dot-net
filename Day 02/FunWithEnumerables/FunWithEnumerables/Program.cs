using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithEnumerables
{
    class Program
    {
        static void Main(string[] args)
        {
            var fibonacci = new FibonacciSequence();


            var enumerator = fibonacci.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }

            enumerator.Dispose();


            Console.ReadLine();

        }
    }
}
