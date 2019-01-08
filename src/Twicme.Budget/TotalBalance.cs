namespace Twicme.Budget
{
    public class TotalBalance
    {
        public Amount Value => _budget.Moneys.Sum(_budget.BaseCurrency);
        
        private readonly Budget _budget;
        public TotalBalance(Budget budget)
        {
            _budget = budget;
        }
    }
}