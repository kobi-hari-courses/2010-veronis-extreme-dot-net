using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithResources
{
    public class Component: DisposableBase
    {
        private Connection _connection;

        private CounterDisposable _counter;
        private int _i = 0;

        public event EventHandler SomethingChanged;

        public Component()
        {
            _connection = new Connection()
                .DisposedBy(this);

            _counter = new CounterDisposable(() => SomethingChanged?.Invoke(this, EventArgs.Empty));
        }

        public async Task DoSomeLongChange()
        {
            Console.WriteLine("Do Some Long Change starts");

            using (_counter.Use())
            {
                _i++;
                await Task.Delay(3000);
                _i++;
            }
            Console.WriteLine("Do Some Long Change ends");
        }

    }
}
