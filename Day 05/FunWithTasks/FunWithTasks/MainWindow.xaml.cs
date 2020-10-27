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

namespace FunWithTasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string a = "";

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

            try
            {
                Debug.WriteLine("A");   // now
                progressBar.IsIndeterminate = true;
                txtStatus.Text = "Please Wait";
                btnGo.IsEnabled = false;
                btnCancel.IsEnabled = true;

                task = PrimesCalculator.GetAllPrimesAsync(0, 250000);

                Debug.WriteLine("B");   // now
                var results = await task;
                Debug.WriteLine("C");   // later

                lstResults.ItemsSource = results;
                txtStatus.Text = "Done";

                Debug.WriteLine("D");// later

                return results.Count;
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
            }

            return 0;
        }



        private void btnGo_Click_old(object sender, RoutedEventArgs e)
        {
            progressBar.IsIndeterminate = true;
            txtStatus.Text = "Please Wait";
            btnGo.IsEnabled = false;
            btnCancel.IsEnabled = true;
            var task = PrimesCalculator.GetAllPrimesAsync(0, 250000);

            var tcont = task.ContinueWith(_ =>
            {
                try
                {
                    var results = task.Result;

                    lstResults.ItemsSource = results;
                    txtStatus.Text = "Done";
                    progressBar.IsIndeterminate = false;
                    btnGo.IsEnabled = true;
                    btnCancel.IsEnabled = false;

                }
                catch (AggregateException ex)
                {


                }

                catch (InvalidOperationException ex)
                {

                }
                return "Hello";

            }, TaskScheduler.FromCurrentSynchronizationContext());

        }
    }
}
