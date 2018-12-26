using System;
using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Budget
    {
        public Month Month { get; }
        public uint Year { get; }
        public DateTimeOffset Created { get; }

        public Balance Plan { get; }
        public Balance Fact { get; }
        
        public Budget(IClock clock, Month month, uint year)
        {
            Month = month;
            Year = year;
            Created = clock.UtcNow;
            Plan = new Balance();
            Fact = new Balance();
        }
        
        public Budget(Month month, uint year) : this(new Clock(), month, year)
        {
        }
    }

    public class Balance
    {
        public IReadOnlyList<Revenue> Revenues { get; }
        public IReadOnlyList<Expense> Expenses { get; }

        public Balance()
        {
            Revenues = new List<Revenue>();
            Expenses = new List<Expense>();
        }
    }
}