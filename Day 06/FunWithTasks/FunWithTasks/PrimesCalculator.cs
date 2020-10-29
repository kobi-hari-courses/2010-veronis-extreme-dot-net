using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FunWithTasks
{
    public static class PrimesCalculator
    {
        private static bool _isPrime(int number)
        {
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        private static IEnumerable<int> _getAllPrimes(int start, int finish, CancellationToken ct, IProgress<int> progress)
        {
            int lastReportedValue = -1;

            for (int i = start; i <= finish; i++)
            {
                ct.ThrowIfCancellationRequested();

                if (progress != null)
                {
                    var progressValue = (int)(((double)i - start) / (finish - start) * 100);
                    if (progressValue > lastReportedValue)
                    {
                        lastReportedValue = progressValue;
                        progress.Report(progressValue);
                    }
                }

                //if (i == 100000)
                //    throw new InvalidOperationException("Becuase i feel like it, ha ha ha");

                if (_isPrime(i)) yield return i;
            }
        }

        private static IEnumerable<int> _getAllPrimesLinq(int start, int finish, CancellationToken ct, IProgress<int> progress)
        {
            return Enumerable.Range(start, finish - start + 1)
                .AsParallel()
                .AsOrdered()
                .WithCancellation(ct)
                .WithProgressReporting(finish - start + 1, progress)
                .Where(val => _isPrime(val));
        }

        public static List<int> GetAllPrimes(int start, int finish, CancellationToken ct, IProgress<int> progress, bool parallel = false)
        {
            if (parallel)
                return _getAllPrimesLinq(start, finish, ct, progress).ToList();
            else
                return _getAllPrimes(start, finish, ct, progress).ToList();
        }

        public static Task<List<int>> GetAllPrimesAsync(int start, int finish, 
            CancellationToken ct = default, IProgress<int> progress = null,
            bool parallel = false
            )
        {
            return Task.Factory.StartNew(() => GetAllPrimes(start, finish, ct, progress, parallel));
        }
    }
}
