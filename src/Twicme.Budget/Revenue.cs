using System;

namespace Twicme.Budget
{
    public sealed class Revenue : Amount
    {
        public RevenueType Type { get; }
        public string Description { get; }
        public DateTimeOffset Created { get; }
        
        public Revenue(Amount amount, RevenueType type, string description = null) : base(amount.Value, amount.Currency)
        {
            Type = type;
            Description = description;
            Created = DateTimeOffset.UtcNow;
        }
    }
}