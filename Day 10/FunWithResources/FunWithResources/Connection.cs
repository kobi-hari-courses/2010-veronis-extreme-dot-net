using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithResources
{
    public class Connection: DisposableBase
    {
        protected override void OnDisposing(bool isFinalizing)
        {
            base.OnDisposing(isFinalizing);
            Console.WriteLine("We are now closing the connection");
        }
    }
}
