using PracticalRxNet.Services;
using PracticalRxNet.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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
    /// Interaction logic for CounterWriter.xaml
    /// </summary>
    public partial class CounterWriter : UserControl
    {
        public CounterWriter()
        {
            InitializeComponent();

            btnPlus
                .ObserveClick()
                .Subscribe(_ => CounterService.Instace.Increment());

            btnMinus
                .ObserveClick()
                .Subscribe(_ => CounterService.Instace.Decrement());
        }

        //private void btnPlus_Click(object sender, RoutedEventArgs e)
        //{
        //    CounterService.Instace.Increment();
        //}

        //private void btnMinus_Click(object sender, RoutedEventArgs e)
        //{
        //    CounterService.Instace.Decrement();
        //}
    }
}
