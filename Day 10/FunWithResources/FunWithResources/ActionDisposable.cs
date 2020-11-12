using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithResources
{
    public class ActionDisposable: DisposableBase
    {
        private Action _action;

        public ActionDisposable(Action action)
        {
            _action = action;
        }

        protected override void OnDisposing(bool isFinalizing)
        {
            base.OnDisposing(isFinalizing);

            if (!isFinalizing)
            {
                _action?.Invoke();
            }
        }
    }
}
