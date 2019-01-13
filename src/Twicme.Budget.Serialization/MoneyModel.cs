using System;

namespace Twicme.Budget.Store
{
    public class MoneyModel
    {
        public MoneyModel(DateTimeOffset created, decimal amount, string currency, string type, string description)
        {
            Created = created;
            Amount = amount;
            Currency = currency;
            Type = type;
            Description = description;
        }

        public DateTimeOffset Created { get; }
        public decimal Amount { get; }
        public string Currency { get; }
        public string Type { get; }
        public string Description { get; }
    }
}