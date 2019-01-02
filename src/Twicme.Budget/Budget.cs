using System;

namespace Twicme.Budget
{
    public class Budget
    {
        public MoneyCollection<Revenue> PlannedRevenues;
        private readonly MoneyCollection<Revenue> _realRevenues;
        private readonly MoneyCollection<Expense> _plannedExpenses;
        private readonly MoneyCollection<Expense> _realExpenses;
        
        public Month Month { get; }
        public uint Year { get; }
        public DateTimeOffset Created { get; }

        public Balance RevenueBalance => new Balance(_realRevenues.Sum(), PlannedRevenues.Sum());
        public Balance ExpenseBalance => new Balance(_realExpenses.Sum(), _plannedExpenses.Sum());

        public Budget(Month month, uint year, 
            MoneyCollection<Revenue> plannedRevenues, MoneyCollection<Revenue> realRevenues,
            MoneyCollection<Expense> plannedExpenses, MoneyCollection<Expense> realExpenses)
        {
            Month = month;
            Year = year;
            Created = DateTimeOffset.UtcNow;
            
            PlannedRevenues = plannedRevenues;
            _realRevenues = realRevenues;
            _plannedExpenses = plannedExpenses;
            _realExpenses = realExpenses;
        }

        public void AddPlannedRevenue(Revenue newRevenue)
        {
            PlannedRevenues.Add(newRevenue);
        }
    }
}