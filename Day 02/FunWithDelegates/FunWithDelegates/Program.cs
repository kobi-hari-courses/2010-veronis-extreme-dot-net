using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithDelegates
{
    public delegate void CarSpeedChanger(int change);

    public delegate int FunctionThatReturnsAnInt(int op1);

    class Program
    {
        static void Main(string[] args)
        {
            EventsDemo();
            Console.ReadLine();
        }

        #region C# 1

        public static void CSharp1Demo()
        {
            var c = new Car();
            c.Speed = 65;

            var del = new CarSpeedChanger(c.Accelerate);
            ChangeSomethingAboutACar(del);
        }

        // C# 1
        public static void ChangeSomethingAboutACar(CarSpeedChanger speedChanger)
        {
            var car = speedChanger.Target as Car;
            Console.WriteLine($"The original speed of the car was {car.Speed}");

            // here were are going to call the function
            speedChanger(20);
            Console.WriteLine($"The new speed of the car is {car.Speed}");

        }

        #endregion

        #region C# 2 

        public static void CSharp2Demo()
        {
            var c = new Car();
            c.Speed = 65;

            // var del = new CarSpeedChanger(c.Accelerate);
            ChangeSomethingAboutACar(c.Accelerate);

            // anonymous function
            ChangeSomethingAboutACar(delegate (int change)
            {
                c.Speed += 2 * change;
            });

        }

        #endregion

        #region C# 3

        public static void CSharp3Demo()
        {
            var c = new Car();
            c.Speed = 65;

            // var del = new CarSpeedChanger(c.Accelerate);
            ChangeSomethingAboutACar(c.Accelerate);

            // anonymous function
            ChangeSomethingAboutACar(change => c.Speed += 2 * change);

            DoSomethingWithTheNewDelegate(j => j + 4);

        }

        public static void DoSomethingWithTheNewDelegate(FunctionThatReturnsAnInt func)
        {
            var i = 0;
            var res = func(i);

            Console.WriteLine($"The result is: {res}");
        }

        #endregion

        #region C# 4

        public static void DoSomethingWithTFunc(Func<int, int, int> func)
        {
            int arg1 = 10;
            int arg2 = 20;

            var res = func(arg1, arg2);

            Console.WriteLine($"The result is: {res}");
        }

        public static void DoSomethingWithTAction(Action<string, int> action)
        {
            string arg1 = "hello";
            int arg2 = 20;

            action(arg1, arg2);
        }

        #endregion

        #region Multicast Delegates

        public static void MulticastDemo()
        {
            Func<int, int> myAction = i =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The integer is " + i);
                return 50;
            };

            Func<int, int> myAction2 = i =>
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("The integer is " + i);
                return 60;
            };

            var runBoth = myAction;
            runBoth += myAction2;
            runBoth += myAction2;
            runBoth += myAction;

            runBoth.GetInvocationList();

            var res = runBoth(42);
        }

        #endregion

        #region Events Demo

        public static void EventsDemo()
        {
            var d = new Document() { PagesCount = 3 };

            var p = new Printer();

            p.Print(d, 2);


            p.PrintingDone += (s, i) =>
            {
                Printer printer = s as Printer;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Printer {printer.Name} finished printing {i} pages");
            };


            p.PrintingDone += (s, i) =>
            {
                Printer printer = s as Printer;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Printer {printer.Name} finished printing {i} pages");
            };



        }
        #endregion


    }
}
