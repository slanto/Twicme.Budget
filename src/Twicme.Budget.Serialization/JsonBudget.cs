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

        public JsonContent Content =>
            new JsonContent(
                JsonConvert.SerializeObject(_budget.ToBudgetModel(),
                    Formatting.Indented,
                    new StringDecimalConverter()));
    }
}