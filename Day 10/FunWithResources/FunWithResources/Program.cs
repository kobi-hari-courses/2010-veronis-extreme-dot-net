using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FunWithResources
{
    class Program
    {

        static void Main(string[] args)
        {
            RunInParallel();
            Console.ReadLine();
        }


        #region IDisposable Example

        public static void DisposablesExample()
        {
            using (Repository r = new Repository(), r2 = new Repository())
            using (IDisposable g = new Repository())
            {


            }
        }

        public static void DisposableColorChangerExample()
        {
            Console.WriteLine("This is the original color");

            using (ChangeColor(ConsoleColor.Green))
            {
                Console.WriteLine("This should be green");

                using (ChangeColor(ConsoleColor.Red))
                {
                    Console.WriteLine("This should be red");
                }

                Console.WriteLine("This should be green again");
            }

            Console.WriteLine("We are back to the original color");

        }

        public static async void CounterDisposableExample()
        {
            var c = new Component();
            c.SomethingChanged += (s, e) =>
            {
                Console.WriteLine("Something changed in the component");
            };

            var t1 = c.DoSomeLongChange();

            await Task.Delay(2000);

            var t2 = c.DoSomeLongChange();

            await Task.WhenAll(t1, t2);
        }

        public static IDisposable ChangeColor(ConsoleColor color)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = color;

            return Disposables.Call(() => Console.ForegroundColor = current);
        }

        #endregion

        #region GC Example

        static IdentifiedObject StaticObject { get; set; }

        static WeakReference WeakStaticObject { get; set; }

        public static void CreateSomeData()
        {
            IdentifiedObject o1 = new IdentifiedObject("1");
            IdentifiedObject o2 = new IdentifiedObject("2");

            StaticObject = new IdentifiedObject("3");
            StaticObject.Next = o2;

            WeakStaticObject = new WeakReference(StaticObject);
        }

        public static void GcExample()
        {
            CreateSomeData();

            Console.WriteLine("Object 3 generation = " + GC.GetGeneration(StaticObject));

            GC.Collect();
            GC.WaitForPendingFinalizers();


            Console.WriteLine("After first garbage collection");
            Console.ReadLine();

            Console.WriteLine("Object 3 generation = " + GC.GetGeneration(StaticObject));

            Console.WriteLine("Weak reference is alive: " + WeakStaticObject.IsAlive);
            if (WeakStaticObject.IsAlive)
            {
                Console.WriteLine(WeakStaticObject.Target);
            }

            StaticObject = null;

            Console.WriteLine("Weak reference is alive: " + WeakStaticObject.IsAlive);
            if (WeakStaticObject.IsAlive)
            {
                Console.WriteLine(WeakStaticObject.Target);
            }

            Console.ReadLine();

            GC.Collect();
            GC.WaitForPendingFinalizers();


            Console.WriteLine("After second garbage collection");

            Console.WriteLine("Weak reference is alive: " + WeakStaticObject.IsAlive);
            if (WeakStaticObject.IsAlive)
            {
                Console.WriteLine(WeakStaticObject.Target);
                Console.WriteLine("Object 3 generation = " + GC.GetGeneration(WeakStaticObject.Target));
            }


            Console.ReadLine();

        }


        #endregion

        #region Async Lock example

        public static async void RunInParallel()
        {
            var t1 = SomeTask.DoSomething("1");
            var t2 = SomeTask.DoSomething("2");
            var t3 = CountToTen();

            await Task.WhenAll(t1, t2, t3);
        }

        public static async Task CountToTen()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("I = " + i);
                await Task.Delay(1000);
            }
        }

        #endregion

    }
}
