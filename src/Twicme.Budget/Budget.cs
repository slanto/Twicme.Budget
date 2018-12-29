using System;
using System.Collections.Generic;
using System.Linq;

namespace Twicme.Budget
{
    public class Budget
    {
        private readonly Revenue[] _plannedRevenues;
        private readonly Revenue[] _realRevenues;
        private readonly Expense[] _plannedExpenses;
        private readonly Expense[] _realExpenses;
        public Month Month { get; }
        public uint Year { get; }
        public DateTimeOffset Created { get; }

        public Money RevenueBalance =>
            Money.CreateZloty(Total(_realRevenues.Select(r => r.Money)) - Total(_plannedRevenues.Select(r=>r.Money)));

        public Money ExpenseBalance =>
            Money.CreateZloty(Total(_realExpenses.Select(r => r.Money)) - Total(_plannedExpenses.Select(r => r.Money)));

        public Budget(Month month, uint year, 
            Revenue[] plannedRevenues, Revenue[] realRevenues,
            Expense[] plannedExpenses, Expense[] realExpenses)
        {
            Month = month;
            Year = year;
            Created = DateTimeOffset.UtcNow;
            
            _plannedRevenues = plannedRevenues;
            _realRevenues = realRevenues;
            _plannedExpenses = plannedExpenses;
            _realExpenses = realExpenses;
        }

        private static decimal Total(IEnumerable<Money> money) => money.Sum(m=>m.Amount);
    }
}