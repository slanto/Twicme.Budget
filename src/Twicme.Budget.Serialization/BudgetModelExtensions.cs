using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget.Store
{
    public static class BudgetModelExtensions
    {
        public static BudgetModel BudgetModel(this Budget budget) =>
            new BudgetModel(budget.MoneyModels(),
                budget.Year,
                budget.Month.Name,
                budget.BaseCurrency.Symbol,
                budget.Created);

        private static IEnumerable<MoneyModel> MoneyModels(this Budget budget) =>
            budget.Revenues().Select(MoneyModel).Concat(
                budget.Expenses().Select(MoneyModel)).ToImmutableList();

        private static MoneyModel MoneyModel(Revenue revenue) =>
            new MoneyModel(revenue.Created, revenue.Amount.Value, revenue.Amount.Currency.Symbol,
                revenue.Type.Name, revenue.Description);
        
        private static MoneyModel MoneyModel(Expense expense) =>
            new MoneyModel(expense.Created, expense.Amount.Value, expense.Amount.Currency.Symbol,
                expense.Type.Name, expense.Description);
        
        public static Budget ToBudget(this BudgetModel model)
        {
            var expenses = model.Moneys.Where(m => m.Amount < 0).Select(m =>
                new Expense(Amount.Create(m.Amount, Currency.Create(m.Currency)),
                    ExpenseType.Create(m.Type), m.Created,  m.Description));
            
            var revenues = model.Moneys.Where(m => m.Amount >= 0).Select(m =>
                new Revenue(Amount.Create(m.Amount, Currency.Create(m.Currency)),
                    RevenueType.Create(m.Type), m.Created,  m.Description));
            
            var moneys = expenses
                .Concat<IMoney>(revenues).ToImmutableList();
                    
            return new Budget(Month.Create(model.Month), model.Year, 
                Currency.Create(model.Currency),
                model.Created, moneys);
        }
    }
}