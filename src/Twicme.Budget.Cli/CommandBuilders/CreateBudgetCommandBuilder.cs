using Microsoft.Extensions.CommandLineUtils;
using Twicme.Budget.Cli.Options;

namespace Twicme.Budget.Cli.CommandBuilders
{
    public class CreateBudgetCommandBuilder
    {
        private readonly CommandLineApplication _application;
        
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
                config.HelpOption(HelpFlagTemplate.Value);          
               
                config.OnExecute(() => Execute(config, new BudgetFactory(), new ConsoleLog()));
            }, false);    
        }

        private static int Execute(CommandLineApplication config, IBudgetFactory budgetFactory, ILog log)
        {
            var yearOption = YearOption.Create(config);
            var monthOption = MonthOption.Create(config);
            var currencyOption = CurrencyOption.Create(config);
                    
            if (yearOption.NotExists || monthOption.NotExists || currencyOption.NotExists)
            {
                config.ShowHelp();
                return ErrorCode.Value;
            }

            var (plannedBudgetFile, realBudgetFile) = budgetFactory.Create(yearOption, monthOption, currencyOption);
            
            if (plannedBudgetFile.NotExists || realBudgetFile.NotExists)
            {
                plannedBudgetFile.Save();
                realBudgetFile.Save();
                
                log.Write($"Budget {plannedBudgetFile.Budget} has been created.");
                return OkCode.Value;
            }
            
            log.Write($"Budget {plannedBudgetFile.Budget} already exists.");
            return ErrorCode.Value;
        }
    }
}