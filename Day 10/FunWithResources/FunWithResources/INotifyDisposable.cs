using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithResources
{
    public interface INotifyDisposable: IDisposable
    {
        bool IsDisposed { get; }
        event EventHandler Disposing;
    }
}
