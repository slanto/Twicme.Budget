namespace Twicme.Budget.Cli
{
    public interface IBudgetFilesFactory
    {
        (BudgetFile plannedBudgetFile, BudgetFile realBudgetFile) Create(YearOption yearOption, MonthOption monthOption,
            CurrencyOption currencyOption);
    }
}