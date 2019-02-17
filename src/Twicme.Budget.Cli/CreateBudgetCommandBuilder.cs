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
               
                config.OnExecute(() =>
                {
                    var yearOption = YearOption.Create(config);
                    var monthOption = MonthOption.Create(config);
                    var currencyOption = CurrencyOption.Create(config);
                    
                    if (yearOption.NotExists || monthOption.NotExists || currencyOption.NotExists)
                    {
                        config.ShowHelp();
                        return 1;
                    }

                    var service = new CreateBudgetService(new ConsoleLog());
                    return service.Execute(yearOption.Value, monthOption.Value, currencyOption.Value);
                });
            }, false);    
        }
    }
}