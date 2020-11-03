using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace IntroductionToRx
{
    class Program
    {
        private static Stopwatch _stopwatch;

        public static event EventHandler<long> OnNumber;

        static async Task Main(string[] args)
        {
            _stopwatch = Stopwatch.StartNew();
            var observer1 = CreateObserver(1, ConsoleColor.White);
            var observer2 = CreateObserver(2, ConsoleColor.Green);
            var observable = CreateFromTask();

            observable.Subscribe(observer1);

            await Task.Delay(3500);

            observable.Subscribe(observer2);

            //OnNumber.Invoke(null, 50);
            //await Task.Delay(1000);
            //OnNumber.Invoke(null, 100);
            //await Task.Delay(1000);
            //OnNumber.Invoke(null, 200);

            Console.ReadLine();
        }

        public static IObserver<long> CreateObserver(int id, ConsoleColor color)
        {
            return Observer.Create<long>(
                onNext: val =>
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{_stopwatch.ElapsedMilliseconds} Observer {id}, Next: {val}");
                }, 
                onCompleted: () =>
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{_stopwatch.ElapsedMilliseconds} Observer {id}, Complete");
                },
                onError: err =>
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{_stopwatch.ElapsedMilliseconds} Observer {id}, Error: {err}");
                });
        }

        public static IObservable<long> CreateIntervalObservable()
        {
            return Observable.Interval(TimeSpan.FromSeconds(1));
        }

        public static IObservable<long> CreateFromSingleItem()
        {
            return Observable.Return<long>(42);
        }

        public static IObservable<long> CreateFromMultipleItems()
        {
            return Observable.Range(10, 5)
                .Select(i => (long)i);
        }

        public static IObservable<long> CreateOBservableThatNeverEmitsAnything()
        {
            return Observable.Never<long>();
        }

        public static IObservable<long> CreateEmpty()
        {
            return Observable.Empty<long>();
        }

        public static IObservable<long> CreateErrorObservable()
        {
            return Observable.Throw<long>(new InvalidOperationException("Oy Vey"));
        }
        public static IObservable<long> CreateObservableFromEvent()
        {
            var res = Observable.FromEventPattern<long>(
                h => Program.OnNumber += h,
                h => Program.OnNumber -= h
                );

            return res.Select(e => e.EventArgs);
        }

        public static IObservable<long> CreateFromGenerate()
        {
            return Observable.Generate(1,
                num => num < 100,
                num => num * 2,
                num => (long)num, 
                num => TimeSpan.FromSeconds(2)
                );
        }

        public static IObservable<long> CreateFromTask()
        {
            var task = Task
                .Delay(3000)
                .ContinueWith(_ => (long)42);

            return task.ToObservable();

        }

        public static IObservable<long> CreateCustomObservable()
        {
            return Observable.Create<long>(observer =>
            {
                observer.OnNext(42);
                Task.Delay(2000).ContinueWith(t => observer.OnNext(100));
                Task.Delay(6000).ContinueWith(t => observer.OnNext(200));
                Task.Delay(9000).ContinueWith(t => observer.OnCompleted());
                return Disposable.Empty;
            });                
        }

        public static IObservable<long> CreateCustomSubject()
        {
            var res = new Subject<long>();

            res.OnNext(42);
            Task.Delay(2000).ContinueWith(t => res.OnNext(100));
            Task.Delay(6000).ContinueWith(t => res.OnNext(200));
            Task.Delay(9000).ContinueWith(t => res.OnCompleted());

            return res;
        }

        public static IObservable<long> CreateCustomBehaviorSubject()
        {
            var res = new BehaviorSubject<long>(42);

            Task.Delay(2000).ContinueWith(t => res.OnNext(100));
            Task.Delay(6000).ContinueWith(t => res.OnNext(200));
            Task.Delay(9000).ContinueWith(t => res.OnCompleted());

            return res;
        }

    }
}
