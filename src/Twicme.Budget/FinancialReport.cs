using System.Collections.Generic;
using System.Linq;

namespace Twicme.Budget
{
    public class FinancialReport
    {
        public Budget Budget { get; }

        public FinancialReport(Budget budget)
        {
            Budget = budget;
        }
        
        private Amount Sum(IEnumerable<IMoney> moneys, Currency currency) => 
            moneys.Aggregate(currency.Zero(), (amount, revenue) => amount + revenue.Amount);
        
        public Amount TotalRevenue() => Budget.Revenues().Sum(Budget.BaseCurrency);
        public Amount TotalExpense() => Budget.Expenses().Sum(Budget.BaseCurrency);
        public Amount TotalBalance() => Budget.Moneys.Sum(Budget.BaseCurrency);
    }
}