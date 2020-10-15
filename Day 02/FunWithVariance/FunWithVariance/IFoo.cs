using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithVariance
{
    public interface IFoo<in T1, out T2, T3>
    {
        void Bar1(T1 arg1);

        T2 Bar2(T1 arg1);

        T2 Bar3(int arg1);

        T3 Bar4(T3 arg);
    }
}
