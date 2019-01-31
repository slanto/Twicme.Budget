namespace Twicme.Budget.Store
{
    public class ProtobufBudget : Budget
    {
        private readonly Budget _budget;

        public ProtobufBudget(Budget budget) 
            : base(budget.Month, budget.BaseCurrency, budget.CreatedOn, budget.Moneys)
        {
            _budget = budget;
        }
    }
}