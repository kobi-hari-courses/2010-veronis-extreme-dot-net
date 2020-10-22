using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FunWithExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> func = i => i * (2 + 3) - 20;

            Expression<Func<int, int>> exp = i => i * (2 + 3) - 20;


            Console.WriteLine(func);
            Console.WriteLine(exp);

            var res = func.Invoke(10);

            var res2 = exp.Compile().Invoke(10);

            Console.ReadLine();


        }
    }
}
