using System;
using System.Collections.Generic;

namespace Twicme.Budget.Store
{
    public class BudgetModel
    {
        public BudgetModel(IEnumerable<MoneyModel> moneys, int year, string month, string currency,
            DateTimeOffset createdOn)
        {
            Moneys = moneys;
            Year = year;
            Month = month;
            Currency = currency;
            CreatedOn = createdOn;
        }

        public IEnumerable<MoneyModel> Moneys { get; } 
        public int Year { get; }
        public string Month { get; }
        public string Currency { get; }
        public DateTimeOffset CreatedOn { get; }
    }
}