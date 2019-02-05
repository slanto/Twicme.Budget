using System;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget.FinancialReport
{
    public class TotalExpense
    {
        private readonly Budget _plannedBudget;
        private readonly Budget _actualBudget;

        private ImmutableList<Money> _plannedExpenses;
        private ImmutableList<Money> _actualExpenses;
        public TotalExpense(Budget budget)
        {
            _plannedBudget = budget;
            _plannedExpenses = budget.Expenses();
        }

        public TotalExpense(Budget plannedBudget, Budget actualBudget)
        {
            _plannedBudget = plannedBudget;
            _actualBudget = actualBudget;

            _plannedExpenses = _plannedBudget.Expenses();
            _actualExpenses = _actualBudget.Expenses();
        }

        public Amount Amount =>
            _actualExpenses == null
                ? _plannedExpenses.Sum(_plannedBudget.BaseCurrency)
                : _actualExpenses.Sum(_actualBudget.BaseCurrency) -
                  _plannedExpenses.Sum(_plannedBudget.BaseCurrency);
        
        public TotalExpense For(Func<Money, bool> predicate)
        {
            _plannedExpenses = _plannedExpenses.Where(predicate).ToImmutableList();
            _actualExpenses = _actualExpenses?.Where(predicate).ToImmutableList();
            
            return this;
        }
    }
}