using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithResources
{
    public class IdentifiedObject
    {
        public string Id { get; }

        public IdentifiedObject Next { get; set; }

        public IdentifiedObject(string id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"Id object: {Id}";
        }

        ~IdentifiedObject()
        {
            Console.WriteLine($"Destructing Identified object: {Id}");
        }
    }
}
