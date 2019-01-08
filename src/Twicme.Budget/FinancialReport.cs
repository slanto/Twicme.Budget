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
        
        public Amount TotalExpense() => Budget.Expenses().Sum(Budget.BaseCurrency); 
    }
}