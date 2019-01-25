using System;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget.FinancialReport
{
    public class TotalExpense : IMoney
    {
        private readonly Currency _currency;
        private ImmutableList<Money> _expenses;
        
        public TotalExpense(Budget budget)
        {
            _currency = budget.BaseCurrency;
            _expenses = budget.Expenses();
        }

        public TotalExpense For(Func<Money, bool> predicate)
        {
            _expenses = _expenses.Where(predicate).ToImmutableList();
            return this;
        }

        public Amount Amount => _expenses.Sum(_currency);
    }
}