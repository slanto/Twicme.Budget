using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli
{
    class Program
    {
        const string HelpFlagTemplate = "-? |-h |--help";
        
        // app create -y 2019 -m 5 -c pln -> creates budget for May in 2019 in PLN
        // app list -> displays all available budgets ordered by year and month descending:
        // 1. 2019-05
        // 2. 2019-04
        // 3. 2018-01
        
        // app budget plan 2019-05 -> loads planned budged for May 2019
        // app budget plan add revenue -a 124.55 -t salary -d "my wife's salary"
        // app budget plan add expense -a 124.55 -t car -d "petrol"
            
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