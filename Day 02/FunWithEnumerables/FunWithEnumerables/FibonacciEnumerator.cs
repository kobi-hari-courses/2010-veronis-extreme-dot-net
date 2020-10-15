using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithEnumerables
{
    public class FibonacciSequence: IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            return new FibonacciEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class FibonacciEnumerator : IEnumerator<int>, IDisposable
        {
            int i = 1;
            int j = 1;

            int current = 1;
            int counter = 0;

            public int Current
            {
                get
                {
                    return current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
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
