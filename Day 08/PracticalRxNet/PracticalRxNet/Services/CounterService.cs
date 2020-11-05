using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalRxNet.Services
{
    public class CounterService
    {
        public static CounterService Instace { get; } = new CounterService();

        private int _counter = 0;
        private ISubject<int> _subject = new BehaviorSubject<int>(0);

        private CounterService()
        {
        }

        public void Increment()
        {
            var value = Interlocked.Increment(ref _counter);
            _subject.OnNext(value);
        }

        public void Decrement()
        {
            var value = Interlocked.Decrement(ref _counter);
            _subject.OnNext(value);
        }

        public IObservable<int> GetCounter()
        {
            return _subject.AsObservable();
        }

    }
}
