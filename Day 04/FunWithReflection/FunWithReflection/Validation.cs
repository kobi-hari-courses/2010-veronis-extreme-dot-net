using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FunWithReflection
{
    public static class Validation
    {
        // In order for a method to be considered a validation method of type T, it has to fulfil 4 conditions
        // 1. It has to be static
        // 2. It has to return a string
        // 3. It has to accept exactly one parameter of type T
        //      - It accepts exactly one parameter
        //      - The one parameter, is of type T
        // 4. Are decorated by the ValidationMethod attribute


        private static IEnumerable<(MethodInfo method, string name)> _findValidationMethodsForType(Type type)
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(mi => mi.IsStatic)
                .Where(mi => mi.ReturnType == typeof(string))
                .Where(mi => mi.GetParameters().Length == 1 && mi.GetParameters()[0].ParameterType == type)
                .Select(mi => (method: mi, attribute: mi.GetCustomAttribute<ValidationMethodAttribute>()))
                .Where(tpl => tpl.attribute != null)
                .Select(tpl => (
                        method: tpl.method,
                        name: (string.IsNullOrWhiteSpace(tpl.attribute.Name) ? tpl.method.Name : tpl.attribute.Name)));
        }

        public static IEnumerable<string> ValidationErrors<T>(this T source)
        {
            var methods = _findValidationMethodsForType(typeof(T));

            foreach (var tpl in methods)
            {
                var result = tpl.method.Invoke(null, new object[] { source }) as string;

                if (!string.IsNullOrWhiteSpace(result))
                {
                    yield return $"Validator: {tpl.name}, Message: {result}";
                }                
            }
        }

        public static void Validate<T>(this T source)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Validating {source.GetType().Name}");

            var errors = source.ValidationErrors();

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var err in errors)
            {
                Console.WriteLine(err);
            }
        }
        
    }
}
