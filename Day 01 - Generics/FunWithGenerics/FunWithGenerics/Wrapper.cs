using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    public class Wrapper<T>
    {
        public T Value { get; set; }

        public Wrapper(T value)
        {
            Value = value;
        }

        public void DoSomething<K>()
        {
            Console.WriteLine("The type parameter of the class is " + typeof(T).Name);
            Console.WriteLine("The type perameter of the method is " + typeof(K).Name);

        }
    }
}
