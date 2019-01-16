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

        public string ToJson() => JsonConvert.SerializeObject(_budget.ToBudgetModel(), Formatting.Indented);
        public static Budget ToBudget(string value) => JsonConvert.DeserializeObject<BudgetModel>(value).ToBudget();
    }
}