using System;

namespace Twicme.Budget.Cli
{
    public class ConsoleLog : ILog
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}