using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithVariance
{
    public class MyComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            throw new NotImplementedException();
        }
    }
}
