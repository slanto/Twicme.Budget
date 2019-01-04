using System;

namespace Twicme.Budget
{
    public sealed class Revenue : IMoney
    {
        public Amount Amount { get; }
        public RevenueType Type { get; }
        public string Description { get; }
        public DateTimeOffset Created { get; }
        
        public Revenue(Amount amount, RevenueType type, string description = null)
        {
            Amount = amount;
            Type = type;
            Description = description;
            Created = DateTimeOffset.UtcNow;
        }
    }
}