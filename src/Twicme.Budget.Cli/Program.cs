using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli
{
    class Program
    {
        const string HelpFlagTemplate = "-? |-h |--help";
        
        static void Main(string[] args)
        {
            try
            {
                var result = Run(args);
                Environment.Exit(result);
            }
            catch (Exception e)
            {
                var message = e.GetBaseException().Message;
                Console.Error.WriteLine(message);
            }
        }

        private static int Run(string[] args)
        {
            var app = new CommandLineApplication();
            app.HelpOption(HelpFlagTemplate);
            
            var budgetCommand = app.Command("budget", config =>
            {
                config.Name = "budget";
                config.Description = "Budget description";
                config.HelpOption(HelpFlagTemplate);
                
                config.OnExecute(() =>
                {
                    config.ShowHelp();
                    return 0;
                });
                
            }, false);
            
            budgetCommand.Command("create", config =>
            {
                config.Name = "create";
                config.Description = "Create budget";
                config.HelpOption(HelpFlagTemplate);
                
                config.OnExecute(() =>
                {
                    Console.WriteLine("Budget created");
                    return 0;
                });
            }, false);

            

            return app.Execute(args);

        }
    }
}