using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithReflection
{
    public class Car
    {
        private static int _counter = 0;
        public static int Counter => _counter;

        public string Make { get; set; }

        public string Model { get; set; }

        public int Speed { get; set; }

        public string FullName
        {
            get
            {
                return Make + " " + Model;
            }
        }

        public void SpeedUp(int acceleration)
        {
            Speed += acceleration;
        }

        public void Stop()
        {
            Speed = 0;
        }

        public string GetSummary()
        {
            return $"{Make} {Model} running at {Speed} mph";
        }

        public void SetSpeedInAnotherScale(double speed, double scaleRatio)
        {
            Speed = (int)(speed / scaleRatio);
        }

        public Car()
        {
            _counter++;
        }
    }
}
