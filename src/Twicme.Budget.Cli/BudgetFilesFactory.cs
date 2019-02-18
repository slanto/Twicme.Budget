namespace Twicme.Budget.Cli
{
    public sealed class BudgetFilesFactory : IBudgetFilesFactory
    {
        public (BudgetFile plan, BudgetFile real) Create(YearOption yearOption, MonthOption monthOption,
            CurrencyOption currencyOption)
        {
            var month = Month.Create(yearOption.Value, MonthName.Create(monthOption.Value));
            var currency = Currency.Create(currencyOption.Value);

            var budget = new Budget(month, currency);

            return (plan: new BudgetFile(budget, FileName.Planned(budget)),
                real: new BudgetFile(budget, FileName.Real(budget)));
        }
    }
}