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
               
                config.OnExecute(() => Execute(config, new BudgetFilesFactory(), new ConsoleLog()));
            }, false);    
        }

        private static int Execute(CommandLineApplication config, IBudgetFilesFactory budgetFilesFactory, ILog log)
        {
            var yearOption = YearOption.Create(config);
            var monthOption = MonthOption.Create(config);
            var currencyOption = CurrencyOption.Create(config);
                    
            if (yearOption.NotExists || monthOption.NotExists || currencyOption.NotExists)
            {
                config.ShowHelp();
                return ErrorCode.Value;
            }

            var (plan, real) = budgetFilesFactory.Create(yearOption, monthOption, currencyOption);
            
            if (plan.NotExists || real.NotExists)
            {
                plan.Save();
                real.Save();
                
                log.Write($"Budget {plan.Budget} has been created.");
                return OkCode.Value;
            }

            log.Write($"Budget {plan.Budget} already exists.");
            return ErrorCode.Value;
        }
    }
}