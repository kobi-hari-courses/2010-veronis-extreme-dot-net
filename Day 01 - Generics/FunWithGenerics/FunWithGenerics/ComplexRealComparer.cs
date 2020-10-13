using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    public class ComplexRealComparer : IComparer<Complex>, IEqualityComparer<Complex>
    {
        public int Compare(Complex x, Complex y)
        {
            //if (x.Real > y.Real) return 1;
            //if (x.Real < x.Real) return -1;
            //return 0;

            return x.Real.CompareTo(y.Real);
        }

        public bool Equals(Complex x, Complex y)
        {
            return Compare(x, y) == 0;
        }

        public int GetHashCode(Complex obj)
        {
            return obj.Real.GetHashCode();
        }
    }
}
