using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Newtonsoft.Json;

namespace Twicme.Budget.Store
{
    public class JsonBudget
    {
        private readonly Budget _budget;

        public JsonBudget(Budget budget)
        {
            _budget = budget;
        }

        public string ToJson() => JsonConvert.SerializeObject(ToBudgetModel(_budget), Formatting.Indented);
        public static Budget ToBudget(string value) => ToBudget(JsonConvert.DeserializeObject<BudgetModel>(value));

        private static BudgetModel ToBudgetModel(Budget budget)
        {
            IEnumerable<MoneyModel> moneys =
                budget.Revenues()
                    .Select(m => new MoneyModel(m.Created, m.Amount.Value, m.Amount.Currency.Symbol, m.Type.Name,
                        m.Description))
                    .Concat(
                        budget.Expenses().Select(m => new MoneyModel(m.Created, m.Amount.Value,
                            m.Amount.Currency.Symbol, m.Type.Name, m.Description)))
                    .ToList();

            return new BudgetModel(moneys, budget.Year, budget.Month.Name, budget.BaseCurrency.Symbol, budget.Created);
        }

        private static Budget ToBudget(BudgetModel model)
        {
            var expenses = model.Moneys.Where(m => m.Amount < 0).Select(m =>
                new Expense(Amount.Create(m.Amount, Currency.Create(m.Currency)),
                    ExpenseType.Create(m.Type), m.Created,  m.Description));
            
            var revenues = model.Moneys.Where(m => m.Amount >= 0).Select(m =>
                new Revenue(Amount.Create(m.Amount, Currency.Create(m.Currency)),
                    RevenueType.Create(m.Type), m.Created,  m.Description));
            
            var moneys = expenses
                .Concat<IMoney>(revenues).ToImmutableList();
                    
            return new Budget(Month.Create(model.Month), model.Year, Currency.Create(model.Currency),
                moneys, model.Created);
        }
    }
}