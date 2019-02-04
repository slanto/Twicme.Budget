using System;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget.FinancialReport
{
    public class TotalRevenue
    {
        private readonly Budget _plannedBudget;
        private readonly Budget _actualBudget;

        private ImmutableList<Money> _plannedRevenues;
        private ImmutableList<Money> _actualRevenues;
        public TotalRevenue(Budget budget)
        {
            _plannedBudget = budget;
            _plannedRevenues = budget.Revenues();
        }

        public TotalRevenue(Budget plannedBudget, Budget actualBudget)
        {
            _plannedBudget = plannedBudget;
            _actualBudget = actualBudget;

            _plannedRevenues = _plannedBudget.Revenues();
            _actualRevenues = _actualBudget.Revenues();
        }

        public Amount Amount =>
            _actualRevenues == null
                ? _plannedRevenues.Sum(_plannedBudget.BaseCurrency)
                : _actualRevenues.Sum(_actualBudget.BaseCurrency) -
                  _plannedRevenues.Sum(_plannedBudget.BaseCurrency);
        
        public TotalRevenue For(Func<Money, bool> predicate)
        {
            _plannedRevenues = _plannedRevenues.Where(predicate).ToImmutableList();
            _actualRevenues = _actualRevenues?.Where(predicate).ToImmutableList();
            
            return this;
        }
    }
}