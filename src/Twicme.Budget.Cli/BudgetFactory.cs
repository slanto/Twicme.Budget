using Twicme.Budget.Cli.Options;

namespace Twicme.Budget.Cli
{
    public sealed class BudgetFactory : IBudgetFactory
    {
        private readonly YearOption _yearOption;
        private readonly MonthOption _monthOption;
        private readonly CurrencyOption _currencyOption;

        public BudgetFactory(YearOption yearOption, MonthOption monthOption,
            CurrencyOption currencyOption)
        {
            _yearOption = yearOption;
            _monthOption = monthOption;
            _currencyOption = currencyOption;
        }
        
        public Budget Create()
        {
            var month = Month.Create(_yearOption.Value, MonthName.Create(_monthOption.Value));
            var currency = Currency.Create(_currencyOption.Value);

            return new Budget(month, currency);
           
        }
    }
}