using System.Reflection;

namespace ReflectionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            object? instance;
            var assembly = Assembly.Load("CustomPrinter");

            foreach(var type in assembly.GetTypes())
            {
                Console.WriteLine($"Type of assembly: {type.Name}");
                Console.WriteLine("---------------------------------------");

                if (type.Name.Contains("Attribute"))
                {
                    continue;
                }

                // use second param array if constructor takes params
                instance = Activator.CreateInstance(type);

                foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)) {
                    Console.WriteLine($"The field is: {field.Name}");
                    field.SetValue(instance, "New Name Set");
                }

                Console.WriteLine("---------------------------------------");

                foreach (var method in type.GetMethods(
                    BindingFlags.NonPublic | 
                    BindingFlags.Public | 
                    BindingFlags.Instance | 
                    BindingFlags.DeclaredOnly)
                    .Where(x => !x.IsSpecialName)) {
                    Console.WriteLine($"The method is: {method.Name}");

                    if (method.GetParameters().Length > 0)
                    {
                        method.Invoke(instance, new[] { "Another new name can be set here" });
                    }
                    else if (method.ReturnType.Name != "Void")
                    {
                        var returnedValue = method.Invoke(instance, null);
                        Console.WriteLine($"This is the returned value from the method: {returnedValue}");
                    }
                    else
                    {
                        method.Invoke(instance, null);
                    }
                }

                Console.WriteLine("---------------------------------------");

                foreach (var prop in type.GetProperties()) {
                    Console.WriteLine($"The method is: {prop.Name}");
                    var propValue = prop.GetValue(instance);

                    Console.WriteLine($"This is the prop value from {prop.Name}: {propValue}");
                }

                Console.WriteLine("---------------------------------------");
            }
        }
    }
}

