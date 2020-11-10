using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedRxNet
{
    class Program
    {
        private static Random _random = new Random();

        static void Main(string[] args)
        {
            CombiningExample();
            Console.ReadLine();
        }

        private static IObservable<int> _generateRandomSequence()
        {
            return Observable
                .Interval(TimeSpan.FromSeconds(2))
                .Select(_ => _random.Next(1, 100));
        }

        private static IObservable<int> _generateIntervalSequenceHot()
        {
            var res = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(i => (int)i)
                .Publish();

            res.Connect();
            return res;
        }

        public static async void HotObservableBecomesCold()
        {
            var o1 = _generateIntervalSequenceHot()
                .Replay();

            o1.Connect();

            await Task.Delay(3000);

            o1.SubscribeToConsole("source", ConsoleColor.Red);
        }

        public static void ColdObservableBecomesHot()
        {
            var o1 = _generateRandomSequence()
                .Publish()
                .RefCount();

            var o2 = o1
                .Select(i => i * 2);

            o1.SubscribeToConsole("source", ConsoleColor.Green);
            o2.SubscribeToConsole("double", ConsoleColor.Red);

        }

        public static void DoExample()
        {
            var o1 = _generateRandomSequence();

            var o2 = o1
                .Do(val => RxExtensions.Writeline($"DO 1: {val}", ConsoleColor.Cyan))
                .Select(i => i * 2)
                .Do(val => RxExtensions.Writeline($"DO 2: {val}", ConsoleColor.Red));

            o2.Subscribe();
            o2.Subscribe();


            //            o2.SubscribeToConsole("select", ConsoleColor.Magenta);
        }

        public static void AggregateExample()
        {
            var o1 = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Select(i => (int)i)
                .Take(5);

            o1.SubscribeToConsole("source", ConsoleColor.Red);

            var o2 = o1
                .Aggregate(0, (ov, nv) => ov + nv);

            var o3 = o1
                .Scan(0, (ov, nv) => ov + nv);


            o2.SubscribeToConsole("aggregate", ConsoleColor.Green);
            o3.SubscribeToConsole("scan", ConsoleColor.Blue);

        }


        public static void StocksExample()
        {
            var market = new StocksMarket();
            market.GetObservable()
                .Select(val => ToConsoleString(val))
                .SubscribeToConsole("market", ConsoleColor.Red);

            var result = market
                .GetObservable()
                .SelectMany(many => many)
                .GroupBy(tpl => tpl.stock)
                .Select(obs => obs
                                .DistinctUntilChanged()
                                .Buffer(2, 1)
                                .Select(list => (stock: list[0].stock, previousValue: list[0].value, value: list[1].value))
                                .Where(tpl => Math.Abs(tpl.previousValue - tpl.value) > 5)
                                )
                .Merge();

            result.SubscribeToConsole("result", ConsoleColor.Green);

        }

        private static string ToConsoleString(IEnumerable<(string stock, int value)> source) 
        {
            var strings = source
                .Select(pair => $"[{pair.stock}:{pair.value}]".PadRight(12));

            return string.Join("", strings);
        }

        public static void CombiningExample()
        {
            var randoms = _generateRandomSequence()
                .Publish()
                .RefCount();

            var o2 = Observable
                .Interval(TimeSpan.FromSeconds(3));

            var zipped = Observable.Zip(randoms, o2, (x, y) => x + y);
            var combined = Observable.CombineLatest(randoms, o2, (x, y) => x + y);
            var withLatest = randoms.WithLatestFrom(o2, (x, y) => x + y);

            randoms.SubscribeMarble("random", ConsoleColor.Red);
            o2.SubscribeMarble("o2", ConsoleColor.Magenta);
            zipped.SubscribeMarble("zipped", ConsoleColor.Blue);
            combined.SubscribeMarble("combined", ConsoleColor.Green);
            withLatest.SubscribeMarble("with latest", ConsoleColor.Yellow);
        }

    }
}
