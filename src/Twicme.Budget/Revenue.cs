using System;

namespace Twicme.Budget
{
    public class Revenue
    {
        public Money Money { get; }
        public RevenueType Type { get; }
        public string Description { get; }
        public DateTimeOffset Created { get; }
        
        public Revenue(Money money, RevenueType type, IClock clock, string description)
        {
            Money = money;
            Type = type;
            Description = description;
            Created = clock.UtcNow;
        }
        
        public Revenue(Money money, RevenueType type, IClock clock) : this(money, type, clock, string.Empty)
        {
        }
    }
}