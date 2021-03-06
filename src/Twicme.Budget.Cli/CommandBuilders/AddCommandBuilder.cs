using System;
using Microsoft.Extensions.CommandLineUtils;
using Twicme.Budget.Cli.Options;

namespace Twicme.Budget.Cli.CommandBuilders
{
    public class AddCommandBuilder
    {
        private readonly CommandLineApplication _application;

        private readonly YearOption _yearOption;
        private readonly MonthOption _monthOption;
        private readonly CurrencyOption _currencyOption;
        private readonly AmountOption _amountOption;
        private readonly CategoryOption _categoryOption;
        private readonly DescriptionOption _descriptionOption;

        private bool OptionsNotExist => _yearOption.NotExists ||
                                        _monthOption.NotExists ||
                                        _currencyOption.NotExists ||
                                        _amountOption.NotExists ||
                                        _categoryOption.NotExists ||
                                        _descriptionOption.NotExists;
        
        public AddCommandBuilder(CommandLineApplication application)
        {
            _application = application;
            
            _yearOption = YearOption.Create(application);
            _monthOption = MonthOption.Create(application);
            _currencyOption = CurrencyOption.Create(application);
            _amountOption = AmountOption.Create(application);
            _categoryOption = CategoryOption.Create(application);
            _descriptionOption = DescriptionOption.Create(application);
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
        
        private int Execute(CommandLineApplication config, IBudgetFilesFactory budgetFilesFactory, ILog log)
        {
            if (OptionsNotExist)
            {
                config.ShowHelp();
                return ErrorCode.Value;
            }

            var (plannedBudgetFile, realBudgetFile) = budgetFilesFactory.Create(_yearOption, _monthOption, _currencyOption);
            
            if (realBudgetFile.NotExists)
            {
                log.Write($"Budget {realBudgetFile.Budget} does not exit. Created new budget first.");
                return ErrorCode.Value;
            }

            var budgetFile = realBudgetFile.Load();

            var category = Category.Create(_categoryOption.Value);
            var description = new Description(_descriptionOption.Value);
            var currency = Currency.Create(_currencyOption.Value);
            var amount = Amount.Create(_amountOption.Value, currency);
      
            budgetFile.Budget.WithExpense(new Money(amount, category, DateTimeOffset.UtcNow, description));
            budgetFile.Save();

            return OkCode.Value;
        }
    }
}