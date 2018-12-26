using System;
using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Budget
    {
        public Month Month { get; }
        public uint Year { get; }
        public DateTimeOffset Created { get; }
        
        public IReadOnlyList<Revenue> PlannedRevenues { get; }
        public IReadOnlyList<Expense> PlannedExpenses { get; }
        
        public IReadOnlyList<Revenue> RealRevenues { get; }
        public IReadOnlyList<Expense> RealExpenses { get; }
        
        public Budget(IClock clock, Month month, uint year)
        {
            Month = month;
            Year = year;
            Created = clock.UtcNow;
            PlannedRevenues = new List<Revenue>();
            PlannedExpenses = new List<Expense>();
            
            RealRevenues = new List<Revenue>();
            RealExpenses = new List<Expense>();
        }
        
        public Budget(Month month, uint year) : this(new Clock(), month, year)
        {
        }
    }
}