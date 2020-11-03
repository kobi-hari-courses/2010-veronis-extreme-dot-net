using PracticalRxNet.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticalRxNet.Views
{
    /// <summary>
    /// Interaction logic for CounterReader.xaml
    /// </summary>
    public partial class CounterReader : UserControl
    {
        private IDisposable _subscription;

        public CounterReader()
        {
            InitializeComponent();

            _subscription = CounterService.Instace
                .GetCounter()
                .Subscribe(val =>
                {
                    Debug.WriteLine($"Counter.OnNext({val}");
                    counterLabel.Text = val.ToString();
                });

            this.Unloaded += CounterReader_Unloaded;
        }

        private void CounterReader_Unloaded(object sender, RoutedEventArgs e)
        {
            _subscription.Dispose();
        }
    }
}
