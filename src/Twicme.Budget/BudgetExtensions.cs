using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget
{
    public static class BudgetExtensions
    {
        public static Budget Add(this Budget budget, IMoney money)
        {
            Contracts.Require(money.Amount.Currency == budget.BaseCurrency,
                $"It is only possible to add money to budget in its base currency: {budget.BaseCurrency}");

            return new Budget(budget.Month, budget.Year, budget.BaseCurrency, budget.Created, budget.Moneys.Add(money));
        }

        public static Budget WithRevenue(this Budget budget, Revenue revenue) => budget.Add(revenue);
        public static Budget WithExpense(this Budget budget, Expense expense) => budget.Add(expense);

        public static ImmutableList<Revenue> Revenues(this Budget budget) =>
            budget.Moneys.Where(m => m.IsRevenue()).Select(m => m.AsRevenue()).ToImmutableList();

        public static ImmutableList<Expense> Expenses(this Budget budget) =>
            budget.Moneys.Where(m => m.IsExpense()).Select(m => m.AsExpense()).ToImmutableList();
    }
}