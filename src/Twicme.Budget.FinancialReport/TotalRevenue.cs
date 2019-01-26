using System;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget.FinancialReport
{
    public class TotalRevenue
    {
        private readonly Currency _currency;
        private ImmutableList<Money> _revenues;

        public TotalRevenue(Budget budget)
        {
            _currency = budget.BaseCurrency;
            _revenues = budget.Revenues();
        }

        public Amount Amount => _revenues.Sum(_currency);
        
        public TotalRevenue For(Func<Money, bool> predicate)
        {
            _revenues = _revenues.Where(predicate).ToImmutableList();
            return this;
        }
    }
}