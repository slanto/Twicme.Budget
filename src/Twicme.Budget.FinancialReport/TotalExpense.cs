namespace Twicme.Budget.FinancialReport
{
    public class TotalExpense
    {
        public Amount Value => _budget.Expenses().Sum(_budget.BaseCurrency);

        private readonly Budget _budget;

        public TotalExpense(Budget budget)
        {
            _budget = budget;
        }
    }
}