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
               
                config.OnExecute(() => Execute(config, new ConsoleLog()));
            }, false);    
        }

        private int Execute(CommandLineApplication config, ILog log)
        {
            var yearOption = YearOption.Create(config);
            var monthOption = MonthOption.Create(config);
            var currencyOption = CurrencyOption.Create(config);
                    
            if (yearOption.NotExists || monthOption.NotExists || currencyOption.NotExists)
            {
                config.ShowHelp();
                return 1;
            }
            
            var month = Month.Create(yearOption.Value, MonthName.Create(monthOption.Value));
            var baseCurrency = Currency.Create(currencyOption.Value);
            
            var budget = new Budget(month, baseCurrency);

            var plannedBudgetFile = new BudgetFile(budget, FileName.Planned(budget));
            var realBudgetFile = new BudgetFile(budget, FileName.Real(budget));
            
            if (plannedBudgetFile.NotExists() || realBudgetFile.NotExists())
            {
                plannedBudgetFile.Save();
                realBudgetFile.Save();
                
                log.Write($"Budget {budget} has been created.");
                return 0;
            }

            log.Write($"Budget {budget} already exists.");
            return 1;
        }
    }
}