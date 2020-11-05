using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithRxOperators
{
    public static class OurExtensions
    {
        private static object _mutex = new object();
        private static Stopwatch _stopwatch;

        static OurExtensions()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public static IDisposable SubscribeConsole<T>(this IObservable<T> source, string id, ConsoleColor color)
        {
            return source.Subscribe(
                onNext: val => WriteInColor($"{_stopwatch.ElapsedMilliseconds} {id} Next {val}", color),
                onCompleted: () => WriteInColor($"{_stopwatch.ElapsedMilliseconds} {id} Completed", color),
                onError: err => WriteInColor($"{_stopwatch.ElapsedMilliseconds} {id} Error {err.Message}", color));
        }

        public static IObservable<int> CreateRandom()
        {
            var rnd = new Random();

            return Observable.Generate(0, i => true, i => i, _ => rnd.Next(0, 20), _ => TimeSpan.FromSeconds(1));
        }

        public static void WriteInColor(string txt, ConsoleColor color)
        {
            lock(_mutex)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(txt);
            }
        }
    }
}
