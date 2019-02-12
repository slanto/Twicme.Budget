using System;
using System.IO;
using System.Net;
using Microsoft.Extensions.CommandLineUtils;
using Twicme.Budget.Store;

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

                var yearOption = config.Option("-y |--year", "year", CommandOptionType.SingleValue);
                var monthOption = config.Option("-m |--month", "month [1-12]", CommandOptionType.SingleValue);
                var currencyOption =
                    config.Option("-c |--currency", "currency [PLN, USD]", CommandOptionType.SingleValue);

                config.OnExecute(() =>
                {
                    if (!yearOption.HasValue() || !monthOption.HasValue() || !currencyOption.HasValue())
                    {
                        config.ShowHelp();
                        return 1;
                    }

                    int.TryParse(monthOption.Value(), out var monthIndex);
                    int.TryParse(yearOption.Value(), out var year);
                    var currency = currencyOption.Value();

                    var service = new CreateBudgetService(new ConsoleLog());
                    return service.Execute(year, monthIndex, currency);
                });
            }, false);    
        }
    }
}