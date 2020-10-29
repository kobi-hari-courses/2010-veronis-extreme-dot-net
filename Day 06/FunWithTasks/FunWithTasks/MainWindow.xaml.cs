using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FunWithTasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private class UserRangeResolver : IRangeResolver
        {
            private MainWindow _host;
            private TaskCompletionSource<(int start, int finish)> _tcs = null;

            public UserRangeResolver(MainWindow host)
            {
                _host = host;
            }

            public Task<(int start, int finish)> GetRange(CancellationToken ct)
            {
                _tcs = new TaskCompletionSource<(int start, int finish)>();
                _host.panelRange.Visibility = Visibility.Visible;
                _host.btnRangeOk.Click += BtnRangeOk_Click;

                ct.Register(() =>
                {
                    _tcs?.TrySetCanceled();
                    _cleanup();
                });

                return _tcs.Task;
            }

            private void _cleanup()
            {
                _host.panelRange.Visibility = Visibility.Collapsed;
                _host.btnRangeOk.Click -= BtnRangeOk_Click;
                _tcs = null;
            }

            private void BtnRangeOk_Click(object sender, RoutedEventArgs e)
            {
                var start = int.Parse(_host.txtFrom.Text);
                var finish = int.Parse(_host.txtTo.Text);
                _tcs?.TrySetResult((start, finish));

                _cleanup();
            }
        }

        private CancellationTokenSource _cts = null;

        public MainWindow()
        {
            InitializeComponent();


            var g = new Grid();
        }

        private async void btnGo_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("1");   // now

            var count = await _run();

            Debug.WriteLine("2");   // later
        }

        private async Task<int> _run()
        {
            Task<List<int>> task = null;
            _cts = new CancellationTokenSource();
            var rangeResolver = new UserRangeResolver(this);

            var progress = new Progress<int>(val =>
            {
                progressBar.Value = val;
            });


            try
            {
                Debug.WriteLine("A");   // now
                //progressBar.IsIndeterminate = true;
                progressBar.Value = 0;
                btnGo.IsEnabled = false;
                btnCancel.IsEnabled = true;
                txtStatus.Text = "Resolving Range";

                var range = await rangeResolver.GetRange(_cts.Token);
                txtStatus.Text = $"Running on range: {range.start}..{range.finish}";

                task = PrimesCalculator.GetAllPrimesAsync(range.start, range.finish, _cts.Token, progress);

                Debug.WriteLine("B");   // now
                var results = await task;
                Debug.WriteLine("C");   // later

                lstResults.ItemsSource = results;
                txtStatus.Text = "Done";

                Debug.WriteLine("D");// later

                return results.Count;
            }
            catch (OperationCanceledException)
            {
                txtStatus.Foreground = Brushes.Orange;
                txtStatus.Text = "Operation Cancelled";
            }
            catch (Exception ex)
            {
                txtStatus.Foreground = Brushes.Red;
                txtStatus.Text = ex.Message;
            }
            finally
            {
                progressBar.IsIndeterminate = false;
                btnGo.IsEnabled = true;
                btnCancel.IsEnabled = false;
                progressBar.Value = 100;
                _cts = null;
            }

            return 0;
        }



        private void btnGo_Click_old(object sender, RoutedEventArgs e)
        {
            //progressBar.IsIndeterminate = true;
            //txtStatus.Text = "Please Wait";
            //btnGo.IsEnabled = false;
            //btnCancel.IsEnabled = true;
            //var task = PrimesCalculator.GetAllPrimesAsync(0, 250000);

            //var tcont = task.ContinueWith(_ =>
            //{
            //    try
            //    {
            //        var results = task.Result;

            //        lstResults.ItemsSource = results;
            //        txtStatus.Text = "Done";
            //        progressBar.IsIndeterminate = false;
            //        btnGo.IsEnabled = true;
            //        btnCancel.IsEnabled = false;

            //    }
            //    catch (AggregateException ex)
            //    {


            //    }

            //    catch (InvalidOperationException ex)
            //    {

            //    }
            //    return "Hello";

            //}, TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }

        }


        #region Demonstration methods

        public async void DelayExample()
        {
            await Task.Delay(2000);

            // this is not really what happens behind the scenes:
            //var delay = Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(2000);
            //});
        }

        public Task<int> CalcSomething()
        {
            return Task.FromResult(42);
        }

        public Task<string> GetStringFromServer()
        {
            // if the value is stored in cache
            // return it now
            return Task.FromResult("cached value");

            // otherwise return a task that ends when the value comes from the server           
        }

        public Task<int> CreateErrorTask()
        {
            return Task.FromException<int>(new InvalidOperationException("Oy vey"));
        }

        public Task<int> CreateCancelledTask()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();
            return Task.FromCanceled<int>(cts.Token);
        }

        public async Task CalculateTwoThingsAtTheSameTime()
        {
            var mutex = new object();

            // we get here on time 0
            var t1 = PrimesCalculator.GetAllPrimesAsync(0, 200000);         // takes about 5 seconds
            var t2 = PrimesCalculator.GetAllPrimesAsync(200001, 300000);    // takes about 3 seconds

            // we get here, also, on time 0

            var tall = Task.WhenAll(t1, t2);

            var res = await tall;

        }

        public Task CombineTasksOfDifferentType()
        {
            var t1 = Task.FromResult(12);
            var t2 = Task.FromResult("Hello");

            var tobj1 = t1.ContinueWith(x => (object)x.Result);
            var tobj2 = t2.ContinueWith(x => (object)x.Result);

            var tall = Task.WhenAll(tobj1, tobj2);

            return tall;
        }

        public async Task RaceTheTasks()
        {
            var cts = new CancellationTokenSource();

            try
            {
                // we get here on time 0
                var t1 = PrimesCalculator.GetAllPrimesAsync(0, 200000, cts.Token);         // takes about 5 seconds
                var t2 = PrimesCalculator.GetAllPrimesAsync(200001, 300000, cts.Token);    // takes about 3 seconds

                var tany = Task.WhenAny(t1, t2);

                var firstTask = await tany;
                var result = firstTask.Result;

                cts.Cancel();
            }
            catch(OperationCanceledException)
            {

            }
        }


        #endregion
    }
}
