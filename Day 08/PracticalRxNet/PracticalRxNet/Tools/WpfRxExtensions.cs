using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PracticalRxNet.Tools
{
    public static class WpfRxExtensions
    {
        public static IObservable<Unit> ObserveClick(this Button source)
        {
            return Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => source.Click += h,
                h => source.Click -= h
                )
                .Select(ep => Unit.Default);
        }

        public static IObservable<Unit> ObserveUnloaded(this FrameworkElement source)
        {
            return Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => source.Unloaded += h,
                h => source.Unloaded -= h
                )
                .Select(ep => Unit.Default)
                .Take(1);
        }

        public static IObservable<string> ObserveTestCahnged(this TextBox source)
        {
            return Observable.FromEventPattern<TextChangedEventHandler, TextChangedEventArgs>(
                h => source.TextChanged += h,
                h => source.TextChanged -= h
                )
                .Select(ep => (ep.EventArgs.Source as TextBox).Text)
                .TakeUntil(source.ObserveUnloaded());
        }
    }
}
