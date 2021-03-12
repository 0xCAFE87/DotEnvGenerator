using DotEnvConfig;
using System;

namespace SourceGenerator.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Config();
            Console.WriteLine("A String: " + config.A_String);
            Console.WriteLine("An int: " + config.An_Integer);
            Console.WriteLine("A boolean: " + config.A_Bool);
            Console.WriteLine("A double: " + config.A_Double);
        

        }
    }
}
