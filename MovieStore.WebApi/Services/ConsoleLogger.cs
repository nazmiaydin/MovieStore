using System;

namespace MovieStore.WebApi.Services
{
    public class ConsoleLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[ConsoleLogger] --> " + message);
        }
    }
}