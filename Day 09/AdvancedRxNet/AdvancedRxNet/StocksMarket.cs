using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedRxNet
{
    public class StocksMarket
    {
        private ImmutableDictionary<string, int> _stocks;

        private IEnumerable<string> _stockNames()
        {
            yield return "ABC";
            yield return "MNOP";
            yield return "QRST";
            yield return "FG";
            yield return "YZ";
            yield return "UV";
            yield return "ACE";
            yield return "JSF";
        }

        private async Task _runUpdateLoop()
        {
            var rnd = new Random();

            while(true)
            {
                await Task.Delay(rnd.Next(1000, 8000));
                var stock = _stocks.Keys.ElementAt(rnd.Next(_stocks.Count));
                var change = rnd.Next(-10, 10);

                _stocks = _stocks.SetItem(stock, _stocks[stock] + change);
            }
        }

        public StocksMarket() 
        {
            Random random = new Random();
            _stocks = _stockNames().ToImmutableDictionary(x => x, _ => random.Next(50));
            var t = _runUpdateLoop(); 
        }

        public IObservable<IEnumerable<(string stock, int value)>> GetObservable()
        {
            return Observable.Interval(TimeSpan.FromSeconds(2))
                .Select(_ => _stocks.Select(pair => (stock: pair.Key, value: pair.Value)));
        }

    }
}
