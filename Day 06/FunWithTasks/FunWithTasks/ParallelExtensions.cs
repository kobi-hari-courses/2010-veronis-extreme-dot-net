using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunWithTasks
{
    public static class ParallelExtensions
    {
        public static ParallelQuery<TSource> WithProgressReporting<TSource>(
            this ParallelQuery<TSource> source,
            long itemsCount, 
            IProgress<int> progress)
        {
            int countShared = 0;
            int lastReportedValue = 0;
            object mutex = new object();

            return source.Select(item =>
            {
                lock(mutex)
                {
                    countShared++;
                    var progressValue = (int)((double)countShared / itemsCount * 100);
                    if (progressValue > lastReportedValue)
                    {
                        lastReportedValue = progressValue;
                        progress.Report(progressValue);
                    }
                }

                return item;
            });
        }
    }
}
