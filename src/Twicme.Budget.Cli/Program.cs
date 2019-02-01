using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "budget";
            app.Description = "Budget Cli application.";
 
            app.HelpOption("-?|-h|--help");

            app.Execute(args);
        }
    }
}