using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithResources
{
    public class AsyncMutex
    {
        private IDisposable _currentToken = null;
        private object _mutex = new object();
        private Queue<TaskCompletionSource<IDisposable>> _queue = new Queue<TaskCompletionSource<IDisposable>>();

        public Task<IDisposable> Lock()
        {
            lock (_mutex)
            {
                if (_currentToken == null)
                {
                    // 1. There is no current token, and the lock is free
                    var token = Disposables.Call(Release);
                    _currentToken = token;
                    return Task.FromResult<IDisposable>(token);
                } else
                {
                    // 2. The lock is busy
                    var tcs = new TaskCompletionSource<IDisposable>();
                    _queue.Enqueue(tcs);
                    return tcs.Task;
                }
            }
        }

        private void Release()
        {
            lock(_mutex)
            {
                if (_queue.Any())
                {
                    // somebody is awaiting for a token

                    var tcs = _queue.Dequeue();
                    var token = Disposables.Call(Release);
                    _currentToken = token;
                    tcs.SetResult(token);

                } else
                {
                    // nobody is waiting for a token
                    _currentToken = null;
                }
            }
        }
    }
}
