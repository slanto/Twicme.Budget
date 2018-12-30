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
            _realRevenues.Select(r => r.Money).Sum().Balance(_plannedRevenues.Select(r=>r.Money).Sum());
        
        public Money ExpenseBalance =>
            _realExpenses.Select(r => r.Money).Sum().Balance(_plannedExpenses.Select(r => r.Money).Sum());

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
    }
}