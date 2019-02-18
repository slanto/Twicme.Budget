namespace Twicme.Budget.Cli
{
    public sealed class BudgetFilesFactory : IBudgetFilesFactory
    {
        public (BudgetFile plannedBudgetFile, BudgetFile realBudgetFile) Create(YearOption yearOption, MonthOption monthOption,
            CurrencyOption currencyOption)
        {
            var month = Month.Create(yearOption.Value, MonthName.Create(monthOption.Value));
            var currency = Currency.Create(currencyOption.Value);

            var budget = new Budget(month, currency);

            return (plannedBudgetFile: new BudgetFile(budget, FileName.Planned(budget)),
                realBudgetFile: new BudgetFile(budget, FileName.Real(budget)));
        }
    }
}