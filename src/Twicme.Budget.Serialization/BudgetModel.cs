using System;
using System.Collections.Generic;

namespace Twicme.Budget.Store
{
    public class BudgetModel
    {
        public BudgetModel(IEnumerable<MoneyModel> moneys, uint year, string month, string currency,
            DateTimeOffset created)
        {
            Moneys = moneys;
            Year = year;
            Month = month;
            Currency = currency;
            Created = created;
        }

        public IEnumerable<MoneyModel> Moneys { get; } 
        public uint Year { get; }
        public string Month { get; }
        public string Currency { get; }
        public DateTimeOffset Created { get; }
    }
}