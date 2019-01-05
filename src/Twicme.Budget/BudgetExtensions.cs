using System.Linq;
using System.Threading;

namespace Twicme.Budget
{
    public static class BudgetExtensions
    {
        public static Budget Add(this Budget budget, IMoney money) =>
            new Budget(budget.Month, budget.Year, budget.Moneys.Add(money));

        

        public static Budget WithRevenue(this Budget budget, Revenue revenue) => budget.Add(revenue);
        public static Budget WithExpense(this Budget budget, Expense expense) => budget.Add(expense);
    }
}