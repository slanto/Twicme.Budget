using System;
using Microsoft.Extensions.CommandLineUtils;
using Twicme.Budget.Cli.Options;

namespace Twicme.Budget.Cli.CommandBuilders
{
    public class AddCommandBuilder
    {
        private readonly CommandLineApplication _application;
        
        public AddCommandBuilder(CommandLineApplication application)
        {
            _application = application;
        }
        
        public CommandLineApplication Build()
        {
            return _application.Command("add", config =>
            {
                config.Name = "add";
                config.Description = "add revenue or expense to budget";
                config.HelpOption(HelpFlagTemplate.Value);
                
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

            var (plannedBudgetFile, realBudgetFile) = budgetFilesFactory.Create(yearOption, monthOption, currencyOption);
            
            if (realBudgetFile.NotExists)
            {
                log.Write($"Budget {realBudgetFile.Budget} does not exit. Created new budget first.");
                return ErrorCode.Value;
            }

            var budgetFile = realBudgetFile.Load();

            decimal amount = 10;
            Category category = Category.CarAndTransport;
            var description = new Description("description");

            budgetFile.Budget.WithExpense(new Money(Amount.Create(amount, Currency.Create(currencyOption.Value)),
                category,
                DateTimeOffset.UtcNow, description));

            return OkCode.Value;
        }
    }
}