using System;

namespace Twicme.Budget
{
    public sealed class Revenue : Money
    {
        public RevenueType Type { get; }
        public string Description { get; }
        public DateTimeOffset Created { get; }
        
        public Revenue(Money money, RevenueType type,string description) : base(money.Amount, money.Currency)
        {
            Type = type;
            Description = description;
            Created = DateTimeOffset.UtcNow;
        }
    }
}