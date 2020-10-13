using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    public interface IFoo<in  T>
    {
    }

    public class Foo<T>: IFoo<T>
    {
    }
}
