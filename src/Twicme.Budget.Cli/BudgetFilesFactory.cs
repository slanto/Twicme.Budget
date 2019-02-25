using Twicme.Budget.Cli.Options;

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

            var realBudgetFileName = new FileName($"budget-{budget.Month.Year}-{budget.Month.MonthName.Index}");
            var plannedBudgetFileName = new FileName($"budget-{budget.Month.Year}-{budget.Month.MonthName.Index}-plan");

            return (plannedBudgetFile: new BudgetFile(budget, plannedBudgetFileName),
                realBudgetFile: new BudgetFile(budget, realBudgetFileName));
        }
    }
}