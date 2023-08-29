using System;

namespace CustomPrinter
{
    public class CustomPrinter
    {
        private string _name;

        public void Print()
        {
            Console.WriteLine("Printing from the custom printer project");
        }

        public string GetName()
        {
            return _name;
        }

        public void PrintName()
        {
            Console.WriteLine($"The name is {_name}");
        }

        public void Print(string name)
        {
            Console.WriteLine($"The name passed in is {name}");
        }

        private void PrintInternal()
        {
            Console.WriteLine($"The internal  name is {_name}");
        }

        public string Name => _name;

        public static string NameStatic => "It is static";
    }
}