using Newtonsoft.Json;

namespace Twicme.Budget.Store
{
    public static class JsonContentExtensions
    {
        public static Budget ToBudget(this JsonContent content) =>
            JsonConvert.DeserializeObject<BudgetModel>(
                    content.Value, new StringDecimalConverter())
                .ToBudget();
    }
}