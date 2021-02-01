using System;
using System.Linq;

namespace homework_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args[0].Select(c => c.ToString()).ToArray();
            var eventPublisher = new EventPublisher();
            eventPublisher.Subscribe(Console.WriteLine);
            var result = new Transport(eventPublisher).Execute(input);
            Console.WriteLine(result);
        }
    }
}
