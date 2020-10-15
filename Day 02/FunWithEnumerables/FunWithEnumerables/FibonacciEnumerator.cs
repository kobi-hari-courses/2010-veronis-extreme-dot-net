using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithEnumerables
{
    public class FibonacciSequence: IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new FibonacciEnumerator();
        }

        private class FibonacciEnumerator : IEnumerator, IDisposable
        {
            int i = 1;
            int j = 1;

            int current = 1;
            int counter = 0;

            public object Current
            {
                get
                {
                    return current;
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (counter < 2)
                {
                    current = 1;
                    counter++;
                    return true;
                }
                else
                {
                    var temp = current;
                    current = current + j;
                    i = j;
                    j = temp;
                    counter++;
                    return (current < 100);
                }
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }



    }

}
