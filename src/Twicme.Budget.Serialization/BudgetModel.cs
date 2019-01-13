using System.Collections.Generic;

namespace Twicme.Budget.Store
{
    public class BudgetModel
    {
        public BudgetModel(IEnumerable<MoneyModel> moneys, uint year, string month, string currency)
        {
            Moneys = moneys;
            Year = year;
            Month = month;
            Currency = currency;
        }

        public IEnumerable<MoneyModel> Moneys { get; } 
        public uint Year { get; }
        public string Month { get; }
        public string Currency { get; }
    }
}