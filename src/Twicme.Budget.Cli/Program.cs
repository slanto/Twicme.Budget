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

            var createCommand = app.Command("create", config =>
            {
                config.Name = "create";
                config.Description = "create budget providing year, month and currency";
                config.HelpOption(HelpFlagTemplate);

                var yearOption = config.Option("-y |--year", "year", CommandOptionType.SingleValue);
                var monthOption = config.Option("-m |--month", "month", CommandOptionType.SingleValue);
                var currencyOption = config.Option("-c |--currency", "currency", CommandOptionType.SingleValue);
                
                config.OnExecute(() =>
                {
                    if (!yearOption.HasValue() || !monthOption.HasValue() || !currencyOption.HasValue())
                    {
                        config.ShowHelp();
                        return 1;
                    }

                    var budget = new Budget(
                        Month.Create(int.Parse(yearOption.Value()), MonthName.Create(monthOption.Value())),
                        Currency.Create(currencyOption.Value()));
                    
                    Console.WriteLine($"Budget {budget} created");
                    return 0; 
                });
            }, false);
                
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

            return app.Execute(args);
        }
    }
}