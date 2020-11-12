using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FunWithResources
{
    public class DisposableBase : INotifyDisposable
    {
        public bool IsDisposed { get; private set; }
        public event EventHandler Disposing;

        protected void Validate([CallerMemberName]string callerName = "")
        {
            if (IsDisposed) throw new ObjectDisposedException($"{GetType().Name}.{callerName}");
        }

        private void _dispose(bool isFinalizing)
        {
            if (IsDisposed) return;
            IsDisposed = true;

            OnDisposing(isFinalizing);

            if (!isFinalizing)
            {
                Disposing?.Invoke(this, EventArgs.Empty);
                Disposing = null;

                GC.SuppressFinalize(this);
            }

        }

        protected virtual void OnDisposing(bool isFinalizing)
        {
        }

        public void Dispose()
        {
            _dispose(false);
        }

        ~DisposableBase()
        {
            _dispose(true);
        }
    }
}
