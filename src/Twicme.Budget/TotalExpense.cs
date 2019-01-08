using System.Collections.Generic;
using System.Linq;

namespace Twicme.Budget
{
    public class TotalExpense
    {
        public Budget Budget { get; }

        public TotalExpense(Budget budget)
        {
            Budget = budget;
        }
        
        public Amount Value => Budget.Expenses().Sum(Budget.BaseCurrency); 
    }
}