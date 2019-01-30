using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget.Store
{
    public static class BudgetModelExtensions
    {
        public static BudgetModel BudgetModel(this Budget budget) =>
            new BudgetModel(budget.MoneyModels(),
                budget.Month.Year,
                budget.Month.MonthName.Name,
                budget.BaseCurrency.Symbol,
                budget.CreatedOn);

        private static IEnumerable<MoneyModel> MoneyModels(this Budget budget) =>
            budget.Moneys.Select(MoneyModel).ToImmutableList();

        private static MoneyModel MoneyModel(Money money) =>
            new MoneyModel(money.Created, money.Amount.Value, money.Amount.Currency.Symbol,
                money.Category.Name, money.Description.Content);
         
        public static Budget ToBudget(this BudgetModel model)
        {
            var moneys = model.Moneys.Select(m =>
                new Money(Amount.Create(m.Amount, Currency.Create(m.Currency)),
                    Category.Create(m.Type), m.Created,  new Description(m.Description))).ToImmutableList();
                    
            return new Budget(Month.Create(model.Year, MonthName.Create(model.Month)),
                Currency.Create(model.Currency),
                model.CreatedOn, moneys);
        }
    }
}