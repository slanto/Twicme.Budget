namespace Twicme.Budget.Cli
{
    public interface IBudgetFilesFactory
    {
        (BudgetFile plan, BudgetFile real) Create(YearOption yearOption, MonthOption monthOption,
            CurrencyOption currencyOption);
    }
}