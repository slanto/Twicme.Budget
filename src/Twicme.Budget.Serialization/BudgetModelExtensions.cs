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
                budget.MonthName.Name,
                budget.BaseCurrency.Symbol,
                budget.Created);

        private static IEnumerable<MoneyModel> MoneyModels(this Budget budget) =>
            budget.Moneys.Select(MoneyModel).ToImmutableList();

        private static MoneyModel MoneyModel(Money money) =>
            new MoneyModel(money.Created, money.Amount.Value, money.Amount.Currency.Symbol,
                money.Category.Name, money.Description);
         
        public static Budget ToBudget(this BudgetModel model)
        {
            var moneys = model.Moneys.Select(m =>
                new Money(Amount.Create(m.Amount, Currency.Create(m.Currency)),
                    Category.Create(m.Type), m.Created,  m.Description)).ToImmutableList();
                    
            return new Budget(MonthName.Create(model.Month), model.Year, 
                Currency.Create(model.Currency),
                model.Created, moneys);
        }
    }
}