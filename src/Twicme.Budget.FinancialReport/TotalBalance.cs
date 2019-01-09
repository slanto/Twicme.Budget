using System.Collections.Generic;

namespace Twicme.Budget.FinancialReport
{
    public class TotalBalance : ValueObject<TotalBalance>, IMoney
    {
        private readonly Budget _budget;
        
        public TotalBalance(Budget budget)
        {
            _budget = budget;
        }

        public Amount Amount => _budget.Moneys.Sum(_budget.BaseCurrency);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
        }
    }
}