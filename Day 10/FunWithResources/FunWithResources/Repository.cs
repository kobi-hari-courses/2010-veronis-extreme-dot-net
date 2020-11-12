using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithResources
{
    public class Repository : IDisposable
    {
        public bool IsDisposed { get; private set; }

        private DisposableObject _internalDisposable = new DisposableObject();

        public List<string> GetDate()
        {
            Validate();
            return new List<string>
            {
                "Hello",
                "World",
                "How are you"
            };
        }


        public void Validate()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(nameof(Repository));
        }

        public void OnDisposing(bool isDuringFinalize)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;

                if (!isDuringFinalize)
                {
                    _internalDisposable.Dispose();
                    _internalDisposable = null;
                }
                Console.WriteLine("Disposing the repository");
                GC.SuppressFinalize(this);
            }
        }

        public void Dispose()
        {
            OnDisposing(false);
        }

        ~Repository()
        {
            OnDisposing(true);
        }


    }
}
