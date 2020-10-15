using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithDelegates
{
    public class Printer
    {

        public event EventHandler<int> PrintingDone;

        public string Name { get; set; }

        public Printer()
        {
            PrintingDone = (s, e) => { };
        }

        public void Print(Document doc, int copies)
        {
            // here we have an implementation of a printer printing the documents

            PrintingDone?.Invoke(this, doc.PagesCount * copies);
        }
    }

    public class Document
    {
        public int PagesCount { get; set; }
    }
}
