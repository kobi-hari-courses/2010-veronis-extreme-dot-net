using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedRxNet
{
    public static class RxExtensions
    {
        private static object _mutex = new object();
        private static Stopwatch _stopwatch = Stopwatch.StartNew();

        public static IDisposable SubscribeToConsole<T>(this IObservable<T> source, string prefix, ConsoleColor color)
        {
            return source.Subscribe(
                onNext: val => Writeline($"{prefix} next: {val.ToString()}", color),
                onCompleted: () => Writeline($"{prefix} completed", color),
                onError: err => Writeline($"{prefix} error: {err.Message}", color)
                );
        }

        public static void Writeline(string txt, ConsoleColor color)
        {
            lock(_mutex)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{_stopwatch.ElapsedMilliseconds} {txt}");
            }
        }
    }
}
