using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FunWithReflection
{
    class Program
    {
        static void Main(string[] args)
        {

            ValidationWithAttributesExample();
            Console.ReadLine();

        }


        static void BasicReflectionExample()
        {
            var car1 = new Car();
            var car2 = new Car();

            Console.WriteLine("Please enter the name of the type");
            var typeName = Console.ReadLine();

            Console.WriteLine("Type Full Name:");
            // var type = typeof(Person);
            var type = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name.EndsWith(typeName));

            Console.WriteLine(type.FullName);

            Console.WriteLine("Properties:");

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            Console.ForegroundColor = ConsoleColor.Yellow;

            foreach (var prop in properties)
            {
                var setMethod = prop.SetMethod;
                var getMethod = prop.GetMethod;

                var isStatic = getMethod.IsStatic ? "Static" : "Instance";
                var accessability = setMethod == null ? "Read Only" : "Read / Write";

                Console.WriteLine($"{prop.PropertyType.Name} {prop.DeclaringType.Name}.{prop.Name}  ({accessability}, {isStatic})");
            }

            var methods = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => !method.IsSpecialName)
                .Where(method => method.DeclaringType == type);

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var method in methods)
            {
                var parameters = method.GetParameters();

                var parameterString = string.Join(", ", parameters
                    .Select(prm => $"{prm.ParameterType} {prm.Name}"));

                Console.WriteLine($"{method.ReturnType} {method.Name} ({parameterString})");
            }

            Console.ForegroundColor = ConsoleColor.Red;

            var instance = Activator.CreateInstance(type);
            foreach (var prop in properties)
            {
                var name = prop.Name;
                var propertyValue = prop.GetMethod.Invoke(instance, new object[] { });

                Console.WriteLine($"{name} = {propertyValue}");
            }

        }

        static void ReflectionWithGenericExample()
        {
            Console.WriteLine("Please enter the name of the type");
            var typeName = Console.ReadLine();

            Console.WriteLine("Type Full Name:");
            // var type = typeof(Person);
            var type = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name.EndsWith(typeName));

            var openType = typeof(List<>);

            var closedListType = openType.MakeGenericType(type);

            //var thisWillCauseAnError = Activator.CreateInstance(openType);

            var listInstance = Activator.CreateInstance(closedListType);

            for (int i = 0; i < 5; i++)
            {
                var itemInstance = Activator.CreateInstance(type);

                var addMethod = listInstance
                    .GetType()
                    .GetMethod("Add");

                addMethod.Invoke(listInstance, new[] { itemInstance });
            }



        }

        static void ValidationWithAttributesExample()
        {
            var c1 = new Car
            {
                Make = "Mazda",
                Model = "2",
                Speed = 60
            };

            var c2 = new Car
            {
                Make = "",
                Model = "",
                Speed = 240
            };

            var c3 = new Car
            {
                Make = "Toyota",
                Model = null,
                Speed = -10
            };

            c1.Validate();
            c2.Validate();
            c3.Validate();
        }
    }
}
