using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunWithResources
{
    public class CounterDisposable
    {
        private int _counter = 0;
        private Action _action;
        private object _mutex = new object();

        public CounterDisposable(Action action)
        {
            _action = action;
        }

        public IDisposable Use()
        {
            Interlocked.Increment(ref _counter);
            return Disposables.Call(() =>
            {
                var shouldCallAction = false;

                lock(_mutex)
                {
                    _counter--;
                    shouldCallAction = (_counter == 0);
                }

                if (shouldCallAction) _action?.Invoke();
            });
        }
    }
}
