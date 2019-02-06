using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();

            var budget = app.Command("budget", config =>
            {
                config.OnExecute(() =>
                {
                    config.ShowHelp();
                    return 1;
                });
                config.HelpOption("-?|-h|--help");
            });
            
            budget.Command("create", config =>
            {
                config.OnExecute(() =>
                {
                    config.Description = "create budget";
                    Console.WriteLine("Budget created");
                    return 0;
                });
            });

            app.HelpOption("-?|-h|--help");

            var result = app.Execute(args);
            Environment.Exit(result);
        }
    }
}