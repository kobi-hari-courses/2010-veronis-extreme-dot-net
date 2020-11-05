using PracticalRxNet.Models;
using PracticalRxNet.Services;
using PracticalRxNet.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Remoting.Channels;
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
    /// Interaction logic for ColorsExample.xaml
    /// </summary>
    public partial class ColorsExample : UserControl
    {
        public ColorsExample()
        {
            InitializeComponent();

            var service = ColorsService.Instance;

            IObservable<string> onSearch  // we are going to get this observable from the text box
                = txtSearch.ObserveTestCahnged();

            var onSearchResult  // this observable holds the latest search results
                = onSearch
                    .Throttle(TimeSpan.FromSeconds(1))
                    .Select(keyword => service.Search(keyword))
                    .Switch()
                    .ObserveOnDispatcher();

            onSearchResult.Subscribe(res => lstResults.ItemsSource = res);

            Observable.Merge(
                onSearch.Select(_ => true),
                onSearchResult.Select(_ => false)
                )
                .DistinctUntilChanged()
                .ObserveOnDispatcher()
                .Subscribe(res => progressBar.IsIndeterminate = res);
        }
    }
}

