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
        
        public Money RevenueBalance => Money.CreateZloty(TotalRevenue(_realRevenues) - TotalRevenue(_plannedRevenues));
        public Money ExpenseBalance => Money.CreateZloty(TotalExpense(_realExpenses) - TotalExpense(_plannedExpenses));

        public Budget(IClock clock, Month month, uint year, 
            Revenue[] plannedRevenues, Revenue[] realRevenues,
            Expense[] plannedExpenses, Expense[] realExpenses)
        {
            Month = month;
            Year = year;
            Created = clock.UtcNow;
            
            _plannedRevenues = plannedRevenues;
            _realRevenues = realRevenues;
            _plannedExpenses = plannedExpenses;
            _realExpenses = realExpenses;
        }

        private static decimal TotalRevenue(IEnumerable<Revenue> revenues) => revenues.Sum(v => v.Money.Amount);
        private static decimal TotalExpense(IEnumerable<Expense> expense) => expense.Sum(v => v.Money.Amount);
    }
}