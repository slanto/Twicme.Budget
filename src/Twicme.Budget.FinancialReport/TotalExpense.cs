using System;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget.FinancialReport
{
    public class TotalExpense
    {
        public Amount Value => _expenses.Sum(_currency);

        private readonly Currency _currency;
        private ImmutableList<Expense> _expenses;
        
        public TotalExpense(Budget budget)
        {
            _currency = budget.BaseCurrency;
            _expenses = budget.Expenses();
        }

        public TotalExpense For(Func<Expense, bool> predicate)
        {
            _expenses = _expenses.Where(predicate).ToImmutableList();
            return this;
        }
    }
}