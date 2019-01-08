namespace Twicme.Budget.FinancialReport
{
    public class TotalRevenue
    {
        public Amount Value => _budget.Revenues().Sum(_budget.BaseCurrency);
        
        private readonly Budget _budget;
        
        public TotalRevenue(Budget budget)
        {
            _budget = budget;
        }
    }
}