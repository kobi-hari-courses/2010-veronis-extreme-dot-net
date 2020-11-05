using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithRxOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            var onSource = OurExtensions
                .CreateRandom()
                .Publish()
                .RefCount();

            var onControl = Observable.Timer(TimeSpan.FromSeconds(5));

            onSource.SubscribeConsole("source", ConsoleColor.White);
            onControl.SubscribeConsole("control", ConsoleColor.Green);

            SKipUntilExample(onSource, onControl);
            TakeUntilExample(onSource, onControl);

            Console.ReadLine();
        }

        static void SelectExample(IObservable<int> source)
        {
            var res = source.Select(i => i * i);
            res.SubscribeConsole("select", ConsoleColor.Red);
        }

        static void WhereExample(IObservable<int> source)
        {
            var res = source.Where(i => i % 3 == 0);
            res.SubscribeConsole("where", ConsoleColor.Blue);
        }

        static void DistinctExample(IObservable<int> source)
        {
            var res = source.Distinct();
            res.SubscribeConsole("distinct", ConsoleColor.Yellow);
        }

        static void DistinctUntilChangedExample(IObservable<int> source)
        {
            var res = source.DistinctUntilChanged();
            res.SubscribeConsole("distinct until changed", ConsoleColor.Magenta);
        }

        static void SKipUntilExample(IObservable<int> source, IObservable<long> control)
        {
            var res = source.SkipUntil(control);
            res.SubscribeConsole("skip until", ConsoleColor.Red);
        }

        static void TakeUntilExample(IObservable<int> source, IObservable<long> control)
        {
            var res = source.TakeUntil(control);
            res.SubscribeConsole("take until", ConsoleColor.Blue);
        }

    }
}
