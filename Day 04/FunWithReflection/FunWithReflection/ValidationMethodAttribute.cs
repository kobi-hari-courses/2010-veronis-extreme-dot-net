using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithReflection
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ValidationMethodAttribute: Attribute
    {
        public string Name { get; set; }

        public ValidationMethodAttribute(string name = "")
        {
            Name = name;
        }
    }
}
