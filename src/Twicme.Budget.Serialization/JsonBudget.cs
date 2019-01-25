using Newtonsoft.Json;

namespace Twicme.Budget.Store
{
    public class JsonBudget : Budget
    {
        private readonly Budget _budget;

        public JsonBudget(Budget budget) : base(budget.MonthName, budget.Year, budget.BaseCurrency, budget.Created,
            budget.Moneys)
        {
            _budget = budget;
        }

        public JsonContent Content =>
            new JsonContent(
                JsonConvert.SerializeObject(_budget.BudgetModel(),
                    Formatting.Indented,
                    new StringDecimalConverter()));
    }
}