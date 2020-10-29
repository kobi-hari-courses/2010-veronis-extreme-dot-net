using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunWithTasks
{
    public class DelayedRangeResolver : IRangeResolver
    {
        public async Task<(int start, int finish)> GetRange(CancellationToken ct)
        {
            await Task.Delay(1500, ct);
            await Task.Delay(1500, ct);
            return (start: 100000, finish: 300000);
        }
    }
}
