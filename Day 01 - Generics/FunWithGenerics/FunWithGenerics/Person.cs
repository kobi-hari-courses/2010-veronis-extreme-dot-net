using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    public class Person: IComparable<Person>
    {
        public string Id { get; set; }

        public string FullName { get; set; }
        public int Age { get; set; }

        public int CompareTo(Person other)
        {
            return this.FullName.CompareTo(other.FullName);
        }

        public Person()
        {

        }

        public Person(string fullName)
        {
            FullName = fullName;
        }
    }
}
