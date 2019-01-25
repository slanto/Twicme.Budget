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
            budget.Revenues().Select(MoneyModel).ToImmutableList();

        private static MoneyModel MoneyModel(Money revenue) =>
            new MoneyModel(revenue.Created, revenue.Amount.Value, revenue.Amount.Currency.Symbol,
                revenue.Category.Name, revenue.Description);
         
        public static Budget ToBudget(this BudgetModel model)
        {
            var moneys = model.Moneys.Select(m =>
                new Money(Amount.Create(m.Amount, Currency.Create(m.Currency)),
                    Category.Create(m.Type), m.Created,  m.Description)).ToImmutableList();
                    
            return new Budget(Month.Create(model.Month), model.Year, 
                Currency.Create(model.Currency),
                model.Created, moneys);
        }
    }
}