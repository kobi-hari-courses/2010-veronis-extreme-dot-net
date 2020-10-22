using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithReflection
{
    public static class CarValidation
    {
        [ValidationMethod("Positive Speed")]
        public static string SpeedIsPositive(Car car)
        {
            if (car.Speed >= 0) return null;

            return $"car speed is negative: {car.Speed}";
        }

        [ValidationMethod("Negative Speed")]
        public static string SpeedIsLessThan200(Car car)
        {
            if (car.Speed < 200) return null;

            return $"car speed is too high: {car.Speed}";
        }

        [ValidationMethod]
        public static string HasMake(Car car)
        {
            if (!string.IsNullOrWhiteSpace(car.Make)) return null;

            return $"car has no make";
        }

        [ValidationMethod]
        public static string HasModel(Car car)
        {
            if (!string.IsNullOrWhiteSpace(car.Model)) return null;

            return $"car has no model";
        }

    }
}
