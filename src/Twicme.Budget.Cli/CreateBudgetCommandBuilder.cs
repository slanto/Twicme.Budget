using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli
{
    public class CreateBudgetCommandBuilder
    {
        private readonly CommandLineApplication _application;
        
        private const string HelpFlagTemplate = "-? |-h |--help";
        
        public CreateBudgetCommandBuilder(CommandLineApplication application)
        {
            _application = application;
        }
        
        public CommandLineApplication Build()
        {
            return _application.Command("create", config =>
            {
                config.Name = "create";
                config.Description = "create budget providing year, month and currency";
                config.HelpOption(HelpFlagTemplate);

                var yearOption = config.Option("-y |--year", "year, e.g. 2019", CommandOptionType.SingleValue);
                var monthOption = config.Option("-m |--month", "month, e.g. February", CommandOptionType.SingleValue);
                var currencyOption = config.Option("-c |--currency", "currency e.g. PLN or USD", CommandOptionType.SingleValue);
                
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
                    
                    Console.WriteLine($"Budget {budget} created.");
                    return 0; 
                });
            }, false);    
        }
    }
}