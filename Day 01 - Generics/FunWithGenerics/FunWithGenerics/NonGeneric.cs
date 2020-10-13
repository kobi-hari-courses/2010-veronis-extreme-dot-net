using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    public class NonGeneric
    {
        public void Log<T>(T arg1)
        {
            T var1 = default(T);

            Console.WriteLine($"arg1 = {arg1.ToString()}, var1 = {var1.ToString()}");
        }
    }
}
