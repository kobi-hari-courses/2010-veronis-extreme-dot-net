using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    public struct Complex: IComparable<Complex>, IEquatable<Complex>
    {
        public double Real { get; }

        public double Imaginary { get; }

        public double DistanceFromOrigin { get; }

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
            DistanceFromOrigin = Math.Sqrt(real * real + imaginary * imaginary);
        }

        public int CompareTo(Complex other)
        {
            //if (this.DistanceFromOrigin > other.DistanceFromOrigin) return 1;
            //if (this.DistanceFromOrigin < other.DistanceFromOrigin) return - 1;

            //return 0;

            return this.DistanceFromOrigin.CompareTo(other.DistanceFromOrigin);
        }

        public bool Equals(Complex other)
        {
            return this.CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is Complex)
            {
                return Equals((Complex)obj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return DistanceFromOrigin.GetHashCode();
        }
    }
}
