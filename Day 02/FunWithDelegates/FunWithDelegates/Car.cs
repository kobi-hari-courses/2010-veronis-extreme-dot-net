using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithDelegates
{
    public class Car
    {
        public int Speed { get; set; }

        public void Accelerate(int accelaration)
        {
            Speed += accelaration;
            Console.WriteLine($"The current speed is {Speed}");
        }

        public void Deccelerate(int accelaration)
        {
            Speed -= accelaration;
            Console.WriteLine($"The current speed is {Speed}");
        }


        public static Car GenerateANewCar()
        {
            return new Car();
        }
    }
}
