using System;
using System.Collections.Immutable;

namespace Twicme.Budget
{
    public class Budget
    {
        public ImmutableList<Revenue> PlannedRevenues { get; }
        public ImmutableList<Revenue> RealRevenues { get; }
        public ImmutableList<Expense> PlannedExpenses { get; }
        public ImmutableList<Expense> RealExpenses { get; }
        
        public Month Month { get; }
        public uint Year { get; }
        public DateTimeOffset Created { get; }

        public Balance RevenueBalance => new Balance(RealRevenues.Sum(), PlannedRevenues.Sum());
        public Balance ExpenseBalance => new Balance(RealExpenses.Sum(), PlannedExpenses.Sum());

        public Budget(Month month, uint year, 
            ImmutableList<Revenue> plannedRevenues, ImmutableList<Revenue> realRevenues,
            ImmutableList<Expense> plannedExpenses, ImmutableList<Expense> realExpenses)
        {
            Month = month;
            Year = year;
            Created = DateTimeOffset.UtcNow;
            
            PlannedRevenues = plannedRevenues;
            RealRevenues = realRevenues;
            PlannedExpenses = plannedExpenses;
            RealExpenses = realExpenses;
        }
    }
}