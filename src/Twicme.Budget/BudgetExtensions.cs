using System.Linq;
using System.Threading;

namespace Twicme.Budget
{
    public static class BudgetExtensions
    {
        public static Budget Add(this Budget budget, IMoney money) =>
            new Budget(budget.Month, budget.Year, budget.Moneys.Add(money));

        public static Amount Balance(this Budget budget)
        {
            var currency = budget.Moneys.First().Amount.Currency;
            
            Contracts.Require(budget.Moneys.All(v => v.Amount.Currency == currency),
                "Sum is only possible for amount in the same currency");

            return budget.Moneys.Aggregate(Amount.Create(0, currency),
                (current, value) => current + value.Amount);
        }

        public static Budget WithRevenue(this Budget budget, Revenue revenue) => budget.Add(revenue);
        public static Budget WithExpense(this Budget budget, Expense expense) => budget.Add(expense);
    }
}