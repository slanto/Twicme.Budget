namespace Twicme.Budget
{
    public static class BudgetExtensions
    {
        public static Budget AddPlannedRevenue(this Budget budget, Revenue revenue) =>
            new Budget(budget.Month, budget.Year, budget.PlannedRevenues.Add(revenue), budget.RealRevenues,
                budget.PlannedExpenses, budget.RealExpenses);

    }
}