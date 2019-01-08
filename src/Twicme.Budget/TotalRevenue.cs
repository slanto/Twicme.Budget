namespace Twicme.Budget
{
    public class TotalRevenue
    {
        private readonly Budget _budget;

        public Amount Value => _budget.Revenues().Sum(_budget.BaseCurrency);
        public TotalRevenue(Budget budget)
        {
            _budget = budget;
        }
    }
}